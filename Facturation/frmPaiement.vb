Imports SosMedecins.SmartRapport.DAL
Imports SosMedecins.SmartRapport.Connexion
Imports SosMedecins.Utilitaires.Structures
Imports SosMedecins.Connexion

Public Class frmPaiement
    Private _drwFacture As dstElementsFacture.factureRow
    Private _drwFactureEtat As dstElementsFacture.facture_etatsRow
    Private _dstElementsFacture As dstElementsFacture

    Private _lngIdentifiant As Long

    Private _stuMode As SosMedecins.Utilitaires.Structures.ModeAccess
    Public Property Mode() As SosMedecins.Utilitaires.Structures.ModeAccess
        Get
            Return _stuMode
        End Get
        Set(ByVal value As SosMedecins.Utilitaires.Structures.ModeAccess)
            _stuMode = value
        End Set
    End Property

    Public Sub New(ByVal p_dstElementsFacture As dstElementsFacture, ByVal p_lngIdentifiant As Long)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Dim z_dvw As DataView
        _dstElementsFacture = p_dstElementsFacture
        _lngIdentifiant = p_lngIdentifiant
        '
        z_dvw = New DataView(_dstElementsFacture.facture_moyen)
        z_dvw.Sort = _dstElementsFacture.facture_moyen.OrdreColumn.ColumnName

        cbxMoyen.DataSource = z_dvw
        cbxMoyen.DisplayMember = _dstElementsFacture.facture_moyen.LibelleColumn.ColumnName
        cbxMoyen.ValueMember = _dstElementsFacture.facture_moyen.CodeColumn.ColumnName
        ' 
        z_dvw = New DataView(_dstElementsFacture.facture_type)
        z_dvw.RowFilter = _dstElementsFacture.facture_type.PaiementColumn.ColumnName & " = True"

        cbxType.DataSource = z_dvw
        cbxType.DisplayMember = _dstElementsFacture.facture_type.LibelleColumn.ColumnName
        cbxType.ValueMember = _dstElementsFacture.facture_type.EtatColumn.ColumnName
    End Sub

    Private Sub frmPaiement_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Select Case _stuMode
            Case ModeAccess.Ajout
                _drwFacture = _dstElementsFacture.facture.FindByNFacture(_lngIdentifiant)

                dbxDate.Value = DateTime.Now.ToString
                txtMontant.Text = FormatNumber(_drwFacture.Solde.ToString(), 2)

                cbxType.SelectedValue = 6
            Case ModeAccess.Modification
                _drwFactureEtat = _dstElementsFacture.facture_etats.FindByCompteur(_lngIdentifiant)
                _drwFacture = _dstElementsFacture.facture.FindByNFacture(_drwFactureEtat.NFacture)

                dbxDate.Value = _drwFactureEtat.DateEtat.ToString

                If (_drwFactureEtat.Item(_dstElementsFacture.facture_etats.DatePayeColumn.ColumnName) Is DBNull.Value) Then
                    dbxDateSal.Value = Nothing
                Else
                    dbxDateSal.Value = _drwFactureEtat.DatePaye.ToString
                End If

                cbxType.SelectedValue = _drwFactureEtat.Etat
                cbxMoyen.SelectedValue = _drwFactureEtat.Moyen

                txtMontant.Text = FormatNumber(_drwFactureEtat.Montant.ToString, 2)

                txtPayeCommentaire.Text = _drwFactureEtat.CommentaireEtat
        End Select
    End Sub

    Private Sub btnAnnuler_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnnuler.Click
        DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub btnValider_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnValider.Click
        Dim z_strRequete As String
        Dim z_fltNouveauSolde As Double
        Dim z_dalTableFacture As New TableFacture
        Dim z_dalTableFactureEtat As New TableFactureEtat

        If (Verification()) Then
            Dim z_blnConnect As Boolean = Variables.ConnexionBase.OpenBDD()
            Variables.ConnexionBase.BeginTransaction()

            Try
                Select Case _stuMode
                    Case ModeAccess.Ajout
                        ' 
                        z_fltNouveauSolde = _drwFacture.Solde - CType(txtMontant.Text, Double)
                        ' Table Facture -----------------------------------------------------------------------------------------------------
                        _drwFacture.Solde = z_dalTableFacture.UpdateSolde(_drwFacture.NFacture, z_fltNouveauSolde, dbxDate.Value)

                        ' Table Facture Etat ------------------------------------------------------------------------------------------------
                        z_dalTableFactureEtat.Insert(_drwFacture.NFacture, cbxType.SelectedValue.ToString(), dbxDate.Value, txtPayeCommentaire.Text, cbxMoyen.SelectedValue.ToString(), SosMedecins.SmartRapport.GestionApplication.VariablesApplicatives.Utilisateurs.Identifiant, txtMontant.Text, dbxDateSal.Value)

                        '
                        _drwFactureEtat = _dstElementsFacture.facture_etats.Newfacture_etatsRow

                        MiseAJourValeurFactureEtat()

                        _dstElementsFacture.facture_etats.Rows.Add(_drwFactureEtat)

                    Case ModeAccess.Modification
                        ' le montant a ete changé
                        If (_drwFactureEtat.Montant.ToString() <> txtMontant.Text) Then
                            z_fltNouveauSolde = _drwFacture.Solde - CType(txtMontant.Text, Double) + _drwFactureEtat.Montant
                            ' Table Facture -----------------------------------------------------------------------------------------------------
                            _drwFacture.Solde = z_dalTableFacture.UpdateSolde(_drwFacture.NFacture, z_fltNouveauSolde, dbxDate.Value)
                        End If

                        ' Table Facture Etat ------------------------------------------------------------------------------------------------
                        z_strRequete = "Update facture_etats SET "
                        z_strRequete &= "Etat = " & FormatSql.Format_Nombre(cbxType.SelectedValue.ToString())
                        z_strRequete &= ", DateEtat = " & FormatSql.Format_Date(dbxDate.Value.ToString)
                        z_strRequete &= ", CommentaireEtat = " & FormatSql.Format_String(txtPayeCommentaire.Text)
                        z_strRequete &= ", Moyen = " & FormatSql.Format_Nombre(cbxMoyen.SelectedValue.ToString())
                        z_strRequete &= ", CodeUtilisateur = " & FormatSql.Format_String(SosMedecins.SmartRapport.GestionApplication.VariablesApplicatives.Utilisateurs.Identifiant)
                        z_strRequete &= ", Montant = " & FormatSql.Format_Nombre(txtMontant.Text)
                        z_strRequete &= ", DatePaye = " & FormatSql.Format_Date(dbxDateSal.Value.ToString())
                        z_strRequete &= " WHERE Compteur = " & FormatSql.Format_Nombre(_drwFactureEtat.Compteur.ToString())

                        Variables.ConnexionBase.ExecuteSqlSansRetour(z_strRequete)
                        '
                        MiseAJourValeurFactureEtat()
                End Select
                ' Valide les modifications -----------------------------------------------------------------------------------------------
                _dstElementsFacture.AcceptChanges()
                Variables.ConnexionBase.Commit()
                DialogResult = DialogResult.OK

            Catch ex As Exception
                _dstElementsFacture.RejectChanges()
                Variables.ConnexionBase.RollBack()
                DialogResult = DialogResult.Abort

                Throw New Exception(ex.Message)
            Finally
                If (z_blnConnect) Then
                    Variables.ConnexionBase.CloseBDD()
                End If
            End Try

            z_dalTableFacture = Nothing
            ' FIN
            Me.Close()
        End If
    End Sub

    Private Function Verification() As Boolean
        errErreur.Clear()
        If Not (dbxDate.IsValid) Then
            errErreur.GenereErreur(dbxDate, "Date Incorrecte !")
        End If

        If Not (dbxDateSal.IsValid) Then
            errErreur.GenereErreur(dbxDateSal, "Date Incorrecte !")
        End If

        If Not IsNumeric(txtMontant.Text) Then
            errErreur.GenereErreur(txtMontant, "Montant Incorrect !")
        End If

        Return errErreur.IsValid
    End Function

    Private Sub MiseAJourValeurFactureEtat()
        _drwFactureEtat.NFacture = _drwFacture.NFacture
        _drwFactureEtat.Etat = CType(cbxType.SelectedValue, Short)
        If dbxDate.Value.Length > 0 Then
            _drwFactureEtat.DateEtat = dbxDate.ValeurDate
        Else
            _drwFactureEtat.Item(_dstElementsFacture.facture_etats.DateEtatColumn.ColumnName) = DBNull.Value
        End If
        _drwFactureEtat.DateOp = DateTime.Now
        _drwFactureEtat.CommentaireEtat = txtPayeCommentaire.Text
        _drwFactureEtat.Param1 = ""
        _drwFactureEtat.Param2 = ""
        _drwFactureEtat.CodeUtilisateur = SosMedecins.SmartRapport.GestionApplication.VariablesApplicatives.Utilisateurs.Identifiant
        _drwFactureEtat.Montant = CType(txtMontant.Text, Double)
        _drwFactureEtat.Moyen = CType(cbxMoyen.SelectedValue, Short)
        If dbxDateSal.Value.Length > 0 Then
            _drwFactureEtat.DatePaye = dbxDateSal.ValeurDate
        Else
            _drwFactureEtat.Item(_dstElementsFacture.facture_etats.DatePayeColumn.ColumnName) = DBNull.Value
        End If
    End Sub

    Public Sub SupprimeEtat(ByVal p_lngIdentifiant As Long)
        _drwFactureEtat = _dstElementsFacture.facture_etats.FindByCompteur(_lngIdentifiant)
        _drwFacture = _dstElementsFacture.facture.FindByNFacture(_drwFactureEtat.NFacture)

        Dim z_strRequete As String
        z_strRequete = "Delete from  facture_etats Where "
        z_strRequete &= " Compteur = " & FormatSql.Format_Nombre(p_lngIdentifiant.ToString())

        Dim z_blnConnect As Boolean = Variables.ConnexionBase.OpenBDD()
        Variables.ConnexionBase.BeginTransaction()

        Try
            Variables.ConnexionBase.ExecuteSqlSansRetour(z_strRequete)

            Dim z_dblNouveauSolde As Double = _drwFacture.Solde + _drwFactureEtat.Montant

            Dim z_dalTableFacture As New TableFacture
            _drwFacture.Solde = z_dalTableFacture.UpdateSolde(_drwFacture.NFacture, z_dblNouveauSolde, Now.ToString())
            z_dalTableFacture = Nothing

            Variables.ConnexionBase.Commit()

            _drwFactureEtat.Delete()
            _dstElementsFacture.AcceptChanges()
        Catch ex As Exception
            _dstElementsFacture.RejectChanges()
            Variables.ConnexionBase.RollBack()

            Throw New Exception(ex.Message)
        Finally
            If (z_blnConnect) Then
                Variables.ConnexionBase.CloseBDD()
            End If
        End Try
    End Sub
End Class