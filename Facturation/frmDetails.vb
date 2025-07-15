Imports SosMedecins.SmartRapport.DAL
Imports SosMedecins.Connexion

Public Class frmDetails
    'Private _drw As DataRow
    Private _bseFacture As New BindingSource
    Private _bseFactureEtat As New BindingSource
    Private _dstElementsFacture As New dstElementsFacture
    Private _lngIdPatient As Long

    Public Enum Mode
        Facture
        Patient
    End Enum

    Public Sub New(ByVal p_enumMode As Mode, ByVal p_lngIdentifiant As Long)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Recuperation des numero de patients
        Dim z_dalTablePatient As New TablePatient
        Dim z_dalTableFacture As New TableFacture
        Dim z_dalTableFactureEtat As New TableFactureEtat
        Dim z_dstPatients As New dstPatients
        Dim z_strListeIdPatient As String

        Select Case p_enumMode
            Case Mode.Facture
                ' chargement
                Dim z_tarFacture As New dstElementsFactureTableAdapters.factureTableAdapter
                z_tarFacture.FillByNFacture(_dstElementsFacture.facture, p_lngIdentifiant)

                Dim z_tarFactureEtat As New dstElementsFactureTableAdapters.facture_etatsTableAdapter
                z_tarFactureEtat.FillByNFacture(_dstElementsFacture.facture_etats, p_lngIdentifiant)

                _lngIdPatient = CType(_dstElementsFacture.facture.Rows(0)(_dstElementsFacture.facture.IndicePatientColumn.ColumnName), Long)

                Dim z_tarPatients As New dstPatientsTableAdapters.tablepatientTableAdapter
                z_tarPatients.Fill(z_dstPatients.tablepatient, _lngIdPatient)

            Case Mode.Patient
                _lngIdPatient = p_lngIdentifiant
                ' chargement du dataset
                z_strListeIdPatient = z_dalTablePatient.ListePatientIdentique(_lngIdPatient)
                z_dalTableFacture.SelectFactureByIdPatient(_dstElementsFacture.facture, z_strListeIdPatient)
                z_dalTableFactureEtat.SelectFactureByIdPatient(_dstElementsFacture.facture_etats, z_strListeIdPatient)

                Dim z_tarPatients As New dstPatientsTableAdapters.tablepatientTableAdapter
                z_tarPatients.Fill(z_dstPatients.tablepatient, _lngIdPatient)
        End Select

        Dim z_tarFactureType As New dstElementsFactureTableAdapters.facture_typeTableAdapter
        z_tarFactureType.Fill(_dstElementsFacture.facture_type)

        Dim z_tarFactureMoyen As New dstElementsFactureTableAdapters.facture_moyenTableAdapter
        z_tarFactureMoyen.Fill(_dstElementsFacture.facture_moyen)

        Dim z_tarUtilisateurs As New dstElementsFactureTableAdapters.tableutilisateurTableAdapter
        z_tarUtilisateurs.Fill(_dstElementsFacture.tableutilisateur)
        ' Recherche des informations Patients

        Dim z_drwPatient As dstPatients.tablepatientRow = DirectCast(z_dstPatients.tablepatient.Rows(0), dstPatients.tablepatientRow)

        txtNom.Text = z_drwPatient.Nom
        txtPrenom.Text = z_drwPatient.Prenom
        If (z_drwPatient.Item(z_dstPatients.tablepatient.DateNaissanceColumn) Is DBNull.Value) Then
            dtbDateNaissance.Value = Nothing
        Else
            dtbDateNaissance.Value = z_drwPatient.DateNaissance.ToString
        End If
        ' Add any initialization after the InitializeComponent() call.
        _bseFacture.DataSource = _dstElementsFacture
        _bseFacture.DataMember = _dstElementsFacture.facture.TableName

        _bseFactureEtat.DataSource = _bseFacture
        _bseFactureEtat.DataMember = _dstElementsFacture.Relations.Item("facture_facture_etats").RelationName

        '
        dgvListe.AutoGenerateColumns = False
        dgvListe.Columns.AddRange(ColumnDataGridList())
        dgvListe.DataSource = _bseFacture

        If (_bseFacture.Count > 0) Then
            ' tri
            dgvListe.Sort(dgvListe.Columns(_dstElementsFacture.facture.DateCreationColumn.ColumnName), System.ComponentModel.ListSortDirection.Ascending)
            ' Somme
            txtSolde.Text = FormatCurrency(_dstElementsFacture.facture.Compute("SUM(Solde)", "").ToString, 2)
            txtTotal.Text = FormatCurrency(_dstElementsFacture.facture.Compute("SUM(TotalFacture)", "").ToString, 2)
            lblNbFacture.Text = _dstElementsFacture.facture.Compute("COUNT(NFacture)", "").ToString & " Factures."

            btnModificationSolde.Enabled = True
            btnPaiement.Enabled = True
        End If
        '
        dgvDetail.AutoGenerateColumns = False
        dgvDetail.Columns.AddRange(ColumnDataGridDetails())
        dgvDetail.DataSource = _bseFactureEtat
        If (_bseFactureEtat.IsSorted) Then
            dgvDetail.Sort(dgvDetail.Columns(_dstElementsFacture.facture_etats.DateEtatColumn.ColumnName), System.ComponentModel.ListSortDirection.Ascending)
        End If

        txtTotalDetail.DataBindings.Add("Text", _bseFacture, _dstElementsFacture.facture.TotalFactureColumn.ColumnName, True, DataSourceUpdateMode.Never, Nothing, "C")
        txtSoldeDetail.DataBindings.Add("Text", _bseFacture, _dstElementsFacture.facture.SoldeColumn.ColumnName, True, DataSourceUpdateMode.Never, Nothing, "C")

    End Sub

    Private Sub btnPaiement_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPaiement.Click
        Dim z_drv As dstElementsFacture.factureRow = DirectCast(DirectCast(_bseFacture.Current, DataRowView).Row, dstElementsFacture.factureRow)
        Dim z_frmPaiement As New frmPaiement(_dstElementsFacture, z_drv.NFacture)

        z_frmPaiement.Mode = Utilitaires.Structures.ModeAccess.Ajout

        z_frmPaiement.ShowDialog()

        z_frmPaiement.Dispose()
        z_frmPaiement = Nothing
    End Sub

    Private Sub btnDecompte_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDecompte.Click
        Me.Cursor = Cursors.WaitCursor

        Dim z_dalTablePatient As New TablePatient
        Dim z_strListeIdPatient As String
        z_strListeIdPatient = z_dalTablePatient.ListePatientIdentique(_lngIdPatient)

        Dim z_rpt As New SosMedecins.SmartRapport.EtatsCrystal.Facture_Decompte()
        SosMedecins.SmartRapport.EtatsCrystal.Fonctions.ChangeConnection(z_rpt, Variables.ConnexionBase)
        Dim z_frm As New SosMedecins.SmartRapport.EtatsCrystal.frmCrystalReportViewer()

        Dim myParameterFields As CrystalDecisions.Shared.ParameterFields = z_rpt.ParameterFields
        Dim myParameterField As CrystalDecisions.Shared.ParameterField = myParameterFields("IdPatients")

        myParameterField.CurrentValues.Clear()
        For Each z_strId As String In Split(z_strListeIdPatient, ",")
            Dim ParameterDiscreteValue As New CrystalDecisions.Shared.ParameterDiscreteValue()
            ParameterDiscreteValue.Value = z_strId

            myParameterField.CurrentValues.Add(ParameterDiscreteValue)
        Next

        z_frm.ReportSource = z_rpt

        Me.Cursor = Cursors.Default
        ' Affichage
        z_frm.ShowDialog()
        ' liberation des elements
        z_rpt.Dispose()
        z_frm.Dispose()
        z_frm = Nothing
    End Sub

#Region " Datagrid List "
    Private Function ColumnDataGridList() As DataGridViewColumn()
        ' Origine active ou non
        Dim z_clnNumeroConsultation As New System.Windows.Forms.DataGridViewTextBoxColumn
        z_clnNumeroConsultation.DataPropertyName = _dstElementsFacture.facture.NConsultationColumn.ColumnName
        z_clnNumeroConsultation.HeaderText = "N° Consultation"
        z_clnNumeroConsultation.Name = _dstElementsFacture.facture.NConsultationColumn.ColumnName
        z_clnNumeroConsultation.ReadOnly = True
        z_clnNumeroConsultation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells

        Dim z_clnDate As New System.Windows.Forms.DataGridViewTextBoxColumn
        z_clnDate.DataPropertyName = _dstElementsFacture.facture.DateConsultationColumn.ColumnName
        z_clnDate.HeaderText = "Date"
        z_clnDate.Name = _dstElementsFacture.facture.DateConsultationColumn.ColumnName
        z_clnDate.ReadOnly = True
        z_clnDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        z_clnDate.DefaultCellStyle.Format = "dd/MM/yyyy"
        z_clnDate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        Dim z_clnMedecin As New System.Windows.Forms.DataGridViewTextBoxColumn
        z_clnMedecin.DataPropertyName = _dstElementsFacture.facture.NomMedecinSosColumn.ColumnName
        z_clnMedecin.HeaderText = "Médecin"
        z_clnMedecin.Name = _dstElementsFacture.facture.NomMedecinSosColumn.ColumnName
        z_clnMedecin.ReadOnly = True
        z_clnMedecin.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells

        Dim z_clnNumeroFacture As New System.Windows.Forms.DataGridViewTextBoxColumn
        z_clnNumeroFacture.DataPropertyName = _dstElementsFacture.facture.NFactureColumn.ColumnName
        z_clnNumeroFacture.HeaderText = "N° Facture"
        z_clnNumeroFacture.Name = _dstElementsFacture.facture.NFactureColumn.ColumnName
        z_clnNumeroFacture.ReadOnly = True
        z_clnNumeroFacture.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells

        Dim z_clnDateFacture As New System.Windows.Forms.DataGridViewTextBoxColumn
        z_clnDateFacture.DataPropertyName = _dstElementsFacture.facture.DateCreationColumn.ColumnName
        z_clnDateFacture.HeaderText = "Date Facture"
        z_clnDateFacture.Name = _dstElementsFacture.facture.DateCreationColumn.ColumnName
        z_clnDateFacture.ReadOnly = True
        z_clnDateFacture.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        z_clnDateFacture.DefaultCellStyle.Format = "dd/MM/yyyy"
        z_clnDateFacture.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        Dim z_clnMontant As New System.Windows.Forms.DataGridViewTextBoxColumn
        z_clnMontant.DataPropertyName = _dstElementsFacture.facture.TotalFactureColumn.ColumnName
        z_clnMontant.HeaderText = "Montant"
        z_clnMontant.Name = _dstElementsFacture.facture.TotalFactureColumn.ColumnName
        z_clnMontant.ReadOnly = True
        z_clnMontant.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        z_clnMontant.DefaultCellStyle.Format = "#,##0.00"
        z_clnMontant.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        Dim z_clnSolde As New System.Windows.Forms.DataGridViewTextBoxColumn
        z_clnSolde.DataPropertyName = _dstElementsFacture.facture.SoldeColumn.ColumnName
        z_clnSolde.HeaderText = "Solde"
        z_clnSolde.Name = _dstElementsFacture.facture.SoldeColumn.ColumnName
        z_clnSolde.ReadOnly = True
        z_clnSolde.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        z_clnSolde.DefaultCellStyle.Format = "#,##0.00"
        z_clnSolde.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        '
        Dim z_clnCommentaire As New System.Windows.Forms.DataGridViewTextBoxColumn
        z_clnCommentaire.DataPropertyName = _dstElementsFacture.facture.CommentaireColumn.ColumnName
        z_clnCommentaire.HeaderText = "Commentaires"
        z_clnCommentaire.Name = _dstElementsFacture.facture.CommentaireColumn.ColumnName
        z_clnCommentaire.ReadOnly = True
        z_clnCommentaire.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill

        Return New System.Windows.Forms.DataGridViewColumn() {z_clnNumeroConsultation, z_clnDate, z_clnMedecin, z_clnNumeroFacture, z_clnDateFacture, z_clnMontant, z_clnSolde, z_clnCommentaire}
    End Function
#End Region

#Region " Datagrid Details "
    Private Function ColumnDataGridDetails() As DataGridViewColumn()
        ' 
        Dim z_clnOperation As New System.Windows.Forms.DataGridViewTextBoxColumn
        z_clnOperation.DataPropertyName = _dstElementsFacture.facture_etats.EtatColumn.ColumnName
        z_clnOperation.HeaderText = "Opération"
        z_clnOperation.Name = _dstElementsFacture.facture_etats.EtatColumn.ColumnName
        z_clnOperation.ReadOnly = True
        z_clnOperation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        '
        Dim z_clnDateOperation As New System.Windows.Forms.DataGridViewTextBoxColumn
        z_clnDateOperation.DataPropertyName = _dstElementsFacture.facture_etats.DateEtatColumn.ColumnName
        z_clnDateOperation.HeaderText = "Date Opération"
        z_clnDateOperation.Name = _dstElementsFacture.facture_etats.DateEtatColumn.ColumnName
        z_clnDateOperation.ReadOnly = True
        z_clnDateOperation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        z_clnDateOperation.DefaultCellStyle.Format = "dd/MM/yyyy"
        z_clnDateOperation.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        '
        Dim z_clnDateEcriture As New System.Windows.Forms.DataGridViewTextBoxColumn
        z_clnDateEcriture.DataPropertyName = _dstElementsFacture.facture_etats.DateOpColumn.ColumnName
        z_clnDateEcriture.HeaderText = "Date Ecriture"
        z_clnDateEcriture.Name = _dstElementsFacture.facture_etats.DateOpColumn.ColumnName
        z_clnDateEcriture.ReadOnly = True
        z_clnDateEcriture.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        z_clnDateEcriture.DefaultCellStyle.Format = "dd/MM/yyyy"
        z_clnDateEcriture.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        '
        Dim z_clnMontant As New System.Windows.Forms.DataGridViewTextBoxColumn
        z_clnMontant.DataPropertyName = _dstElementsFacture.facture_etats.MontantColumn.ColumnName
        z_clnMontant.HeaderText = "Montant"
        z_clnMontant.Name = _dstElementsFacture.facture_etats.MontantColumn.ColumnName
        z_clnMontant.ReadOnly = True
        z_clnMontant.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        z_clnMontant.DefaultCellStyle.Format = "#,##0.00"
        z_clnMontant.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        '
        Dim z_clnMoyen As New System.Windows.Forms.DataGridViewTextBoxColumn
        z_clnMoyen.DataPropertyName = _dstElementsFacture.facture_etats.MoyenColumn.ColumnName
        z_clnMoyen.HeaderText = "Moyen"
        z_clnMoyen.Name = _dstElementsFacture.facture_etats.MoyenColumn.ColumnName
        z_clnMoyen.ReadOnly = True
        z_clnMoyen.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        '
        Dim z_clnIndication As New System.Windows.Forms.DataGridViewTextBoxColumn
        z_clnIndication.DataPropertyName = _dstElementsFacture.facture_etats.Param2Column.ColumnName
        z_clnIndication.HeaderText = "Indication"
        z_clnIndication.Name = _dstElementsFacture.facture_etats.Param2Column.ColumnName
        z_clnIndication.ReadOnly = True
        z_clnIndication.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        '
        Dim z_clnUtilisateur As New System.Windows.Forms.DataGridViewTextBoxColumn
        z_clnUtilisateur.DataPropertyName = _dstElementsFacture.facture_etats.CodeUtilisateurColumn.ColumnName
        z_clnUtilisateur.HeaderText = "Initiateur"
        z_clnUtilisateur.Name = _dstElementsFacture.facture_etats.CodeUtilisateurColumn.ColumnName
        z_clnUtilisateur.ReadOnly = True
        z_clnUtilisateur.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        '
        Dim z_clnCommentaire As New System.Windows.Forms.DataGridViewTextBoxColumn
        z_clnCommentaire.DataPropertyName = _dstElementsFacture.facture_etats.CommentaireEtatColumn.ColumnName
        z_clnCommentaire.HeaderText = "Commentaires"
        z_clnCommentaire.Name = _dstElementsFacture.facture_etats.CommentaireEtatColumn.ColumnName
        z_clnCommentaire.ReadOnly = True
        z_clnCommentaire.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill

        Return New System.Windows.Forms.DataGridViewColumn() {z_clnOperation, z_clnDateOperation, z_clnDateEcriture, z_clnMontant, z_clnMoyen, z_clnUtilisateur, z_clnCommentaire}
    End Function

    Private Sub dgvDetail_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles dgvDetail.CellFormatting
        Dim z_dgv As System.Windows.Forms.DataGridView = CType(sender, System.Windows.Forms.DataGridView)

        Select Case z_dgv.Columns(e.ColumnIndex).Name
            Case _dstElementsFacture.facture_etats.EtatColumn.ColumnName
                Dim currentRow As DataRowView = TryCast(z_dgv.Rows(e.RowIndex).DataBoundItem, DataRowView)
                Dim z_drwChildRows As DataRow() = currentRow.Row.GetChildRows("facture_etats_facture_type")
                e.Value = z_drwChildRows(0).Item("Libelle").ToString

            Case _dstElementsFacture.facture_etats.MoyenColumn.ColumnName
                Dim currentRow As DataRowView = TryCast(z_dgv.Rows(e.RowIndex).DataBoundItem, DataRowView)
                Dim z_drwChildRows As DataRow() = currentRow.Row.GetChildRows("facture_etats_facture_moyen")
                e.Value = z_drwChildRows(0).Item("Libelle").ToString

            Case _dstElementsFacture.facture_etats.CodeUtilisateurColumn.ColumnName
                Dim currentRow As DataRowView = TryCast(z_dgv.Rows(e.RowIndex).DataBoundItem, DataRowView)
                Dim z_drwChildRows As DataRow() = currentRow.Row.GetChildRows("facture_etats_tableutilisateur")
                e.Value = z_drwChildRows(0).Item("Nom").ToString
        End Select
    End Sub

    Private Sub dgvDetail_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvDetail.CellDoubleClick
        If (e.RowIndex > -1) Then
            Modification()
        End If
    End Sub

    Private Sub dgvDetail_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvDetail.SelectionChanged
        Dim z_drv As dstElementsFacture.facture_etatsRow = DirectCast(DirectCast(_bseFactureEtat.Current, DataRowView).Row, dstElementsFacture.facture_etatsRow)

        If (_dstElementsFacture.facture_type.FindByEtat(z_drv.Etat).Paiement = True) Then
            btnModifierDetail.Enabled = True
            btnSupprimerDetail.Enabled = True
        Else
            btnModifierDetail.Enabled = False
            btnSupprimerDetail.Enabled = False
        End If

    End Sub
#End Region

    Private Sub btnModifierDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModifierDetail.Click
        Modification()
    End Sub

    Private Sub btnSupprimerDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSupprimerDetail.Click
        Dim z_drv As dstElementsFacture.facture_etatsRow = DirectCast(DirectCast(_bseFactureEtat.Current, DataRowView).Row, dstElementsFacture.facture_etatsRow)

        Dim z_strTexte As String = "Etes-vous sur de vouloir supprimer la ligne d'un montant de " & z_drv.Montant

        If (MessageBox.Show(z_strTexte, "Suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes) Then

            Dim z_frmPaiement As New frmPaiement(_dstElementsFacture, z_drv.Compteur)

            z_frmPaiement.Mode = Utilitaires.Structures.ModeAccess.Suppression

            z_frmPaiement.SupprimeEtat(z_drv.Compteur)

            z_frmPaiement.Dispose()
            z_frmPaiement = Nothing
            '
            txtSolde.Text = FormatCurrency(_dstElementsFacture.facture.Compute("SUM(Solde)", "").ToString, 2)
        End If
    End Sub

    Private Sub Modification()
        Dim z_drv As dstElementsFacture.facture_etatsRow = DirectCast(DirectCast(_bseFactureEtat.Current, DataRowView).Row, dstElementsFacture.facture_etatsRow)

        If (_dstElementsFacture.facture_type.FindByEtat(z_drv.Etat).Paiement = True) Then
            Dim z_frmPaiement As New frmPaiement(_dstElementsFacture, z_drv.Compteur)

            z_frmPaiement.Mode = Utilitaires.Structures.ModeAccess.Modification

            z_frmPaiement.ShowDialog()

            z_frmPaiement.Dispose()
            z_frmPaiement = Nothing
            '
            txtSolde.Text = FormatCurrency(_dstElementsFacture.facture.Compute("SUM(Solde)", "").ToString, 2)
        End If
    End Sub

    Private Sub btnModificationSolde_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModificationSolde.Click

        Dim z_drv As dstElementsFacture.factureRow = DirectCast(DirectCast(_bseFacture.Current, DataRowView).Row, dstElementsFacture.factureRow)
        Dim z_frmPaiement As New frmPaiement(_dstElementsFacture, z_drv.NFacture)

        Dim z_frmModificationSolde As New frmModificationSolde(_dstElementsFacture, z_drv.NFacture)
        If (z_frmModificationSolde.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            '
            txtSolde.Text = FormatCurrency(_dstElementsFacture.facture.Compute("SUM(Solde)", "").ToString, 2)
        End If

        z_frmModificationSolde.Dispose()
        z_frmModificationSolde = Nothing
    End Sub

    
End Class