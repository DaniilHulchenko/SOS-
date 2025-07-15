Imports SosMedecins.Connexion




Public Class frmArrangement
    Private _dtbArrangement As DataTable
    Private _strNFacture As String


    Public Sub New(ByVal p_dtbArrangement As DataTable, ByVal p_strNFacture As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _dtbArrangement = p_dtbArrangement
        _strNFacture = p_strNFacture

        If (_dtbArrangement.Rows.Count > 0) Then
            txtDate.Text = CType(_dtbArrangement.Rows(0)("DateCreation"), Date).ToString("dd.MM.yyyy")
            txtUser.Text = _dtbArrangement.Rows(0)("Nom").ToString()
            txtCommentaire.Text = _dtbArrangement.Rows(0)("Commentaire").ToString()
        Else
            txtDate.Text = DateTime.Now.ToString("dd.MM.yyyy")
            txtUser.Tag = SosMedecins.SmartRapport.GestionApplication.VariablesApplicatives.Utilisateurs.Identifiant
            txtUser.Text = SosMedecins.SmartRapport.GestionApplication.VariablesApplicatives.Utilisateurs.NomUtilisateur
            txtCommentaire.Text = ""
        End If
    End Sub

    Private Sub btnAnnuler_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnnuler.Click
        Me.Close()
    End Sub

    Private Sub btnValider_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnValider.Click
        Dim z_strSql As String
        If (_dtbArrangement.Rows.Count > 0) Then
            _dtbArrangement.Rows(0)("Commentaire") = txtCommentaire.Text
            ' faire update base
            z_strSql = SosMedecins.SmartRapport.DAL.RequetesUpdate.facture_arrangement.Nfacture
            z_strSql = z_strSql.Replace("%NFacture%", FormatSql.Format_Nombre(_strNFacture.ToString()))
            z_strSql = z_strSql.Replace("%Commentaire%", FormatSql.Format_String(_dtbArrangement.Rows(0)("Commentaire").ToString()))
        Else
            _dtbArrangement.Rows.Add(_dtbArrangement.NewRow())
            _dtbArrangement.Rows(0)("NFacture") = _strNFacture
            _dtbArrangement.Rows(0)("DateCreation") = txtDate.Text
            _dtbArrangement.Rows(0)("CodeUtilisateur") = txtUser.Tag
            _dtbArrangement.Rows(0)("Nom") = txtUser.Text
            _dtbArrangement.Rows(0)("Commentaire") = txtCommentaire.Text
            '  faire insert base
            z_strSql = SosMedecins.SmartRapport.DAL.RequetesInsert.facture_arrangement.Complet
            z_strSql = z_strSql.Replace("%NFacture%", FormatSql.Format_Nombre(_strNFacture.ToString()))
            z_strSql = z_strSql.Replace("%DateCreation%", FormatSql.Format_Date(_dtbArrangement.Rows(0)("DateCreation").ToString()))
            z_strSql = z_strSql.Replace("%CodeUtilisateur%", FormatSql.Format_String(_dtbArrangement.Rows(0)("CodeUtilisateur").ToString()))
            z_strSql = z_strSql.Replace("%Commentaire%", FormatSql.Format_String(_dtbArrangement.Rows(0)("Commentaire").ToString()))
        End If
        If (MiseAJour(z_strSql)) Then
            _dtbArrangement.AcceptChanges()
        Else
            _dtbArrangement.RejectChanges()
        End If
        Me.Close()
    End Sub

    Private Sub btnSupprimer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSupprimer.Click
        If (MessageBox.Show("Etes-vous sur de vouloir supprimer cet arrangement ?", "Suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then
            '
            _dtbArrangement.Rows(0).Delete()

            If (MiseAJour(SosMedecins.SmartRapport.DAL.RequetesDelete.facture_arrangement.Ligne.Replace("%NFacture%", FormatSql.Format_Nombre(_strNFacture.ToString())))) Then
                _dtbArrangement.AcceptChanges()
            Else
                _dtbArrangement.RejectChanges()
            End If
            '
            Me.Close()
        End If
    End Sub

    Private Function MiseAJour(ByVal p_strSql As String) As Boolean
        Dim z_blnRetour As Boolean = False

        Dim z_blnConnect As Boolean = SosMedecins.SmartRapport.DAL.Variables.ConnexionBase.OpenBDD()
        Try
            SosMedecins.SmartRapport.DAL.Variables.ConnexionBase.ExecuteSqlSansRetour(p_strSql)
            z_blnRetour = True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If (z_blnConnect) Then
                SosMedecins.SmartRapport.DAL.Variables.ConnexionBase.CloseBDD()
            End If
        End Try
        Return z_blnRetour
    End Function
End Class