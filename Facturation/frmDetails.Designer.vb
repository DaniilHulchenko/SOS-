<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDetails
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDetails))
        Me.grpIdentification = New System.Windows.Forms.GroupBox()
        Me.dtbDateNaissance = New SosMedecins.Controls.sosDateBox()
        Me.lblDateNaissance = New System.Windows.Forms.Label()
        Me.txtPrenom = New SosMedecins.Controls.sosTextBox()
        Me.lblPrenom = New System.Windows.Forms.Label()
        Me.LblNom = New System.Windows.Forms.Label()
        Me.txtNom = New System.Windows.Forms.TextBox()
        Me.btnDecompte = New System.Windows.Forms.Button()
        Me.dgvListe = New System.Windows.Forms.DataGridView()
        Me.dgvDetail = New System.Windows.Forms.DataGridView()
        Me.grpListe = New System.Windows.Forms.GroupBox()
        Me.lblNbFacture = New System.Windows.Forms.Label()
        Me.txtSolde = New System.Windows.Forms.TextBox()
        Me.txtTotal = New System.Windows.Forms.TextBox()
        Me.lblSolde = New System.Windows.Forms.Label()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.grpDetails = New System.Windows.Forms.GroupBox()
        Me.btnModificationSolde = New System.Windows.Forms.Button()
        Me.btnSupprimerDetail = New System.Windows.Forms.Button()
        Me.btnModifierDetail = New System.Windows.Forms.Button()
        Me.lblSoldeDetail = New System.Windows.Forms.Label()
        Me.lblTotalDetail = New System.Windows.Forms.Label()
        Me.txtSoldeDetail = New System.Windows.Forms.TextBox()
        Me.txtTotalDetail = New System.Windows.Forms.TextBox()
        Me.btnPaiement = New System.Windows.Forms.Button()
        Me.ttpPrincipal = New System.Windows.Forms.ToolTip(Me.components)
        Me.grpIdentification.SuspendLayout()
        CType(Me.dgvListe, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpListe.SuspendLayout()
        Me.grpDetails.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpIdentification
        '
        Me.grpIdentification.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpIdentification.Controls.Add(Me.dtbDateNaissance)
        Me.grpIdentification.Controls.Add(Me.lblDateNaissance)
        Me.grpIdentification.Controls.Add(Me.txtPrenom)
        Me.grpIdentification.Controls.Add(Me.lblPrenom)
        Me.grpIdentification.Controls.Add(Me.LblNom)
        Me.grpIdentification.Controls.Add(Me.txtNom)
        Me.grpIdentification.Controls.Add(Me.btnDecompte)
        Me.grpIdentification.Location = New System.Drawing.Point(12, 12)
        Me.grpIdentification.Name = "grpIdentification"
        Me.grpIdentification.Size = New System.Drawing.Size(789, 66)
        Me.grpIdentification.TabIndex = 1
        Me.grpIdentification.TabStop = False
        '
        'dtbDateNaissance
        '
        Me.dtbDateNaissance.Location = New System.Drawing.Point(481, 19)
        Me.dtbDateNaissance.Mask = "00/00/0000"
        Me.dtbDateNaissance.Name = "dtbDateNaissance"
        Me.dtbDateNaissance.ReadOnly = True
        Me.dtbDateNaissance.Size = New System.Drawing.Size(83, 20)
        Me.dtbDateNaissance.TabIndex = 8
        Me.dtbDateNaissance.ValidatingType = GetType(Date)
        Me.dtbDateNaissance.Value = ""
        '
        'lblDateNaissance
        '
        Me.lblDateNaissance.AutoSize = True
        Me.lblDateNaissance.Location = New System.Drawing.Point(379, 22)
        Me.lblDateNaissance.Name = "lblDateNaissance"
        Me.lblDateNaissance.Size = New System.Drawing.Size(96, 13)
        Me.lblDateNaissance.TabIndex = 7
        Me.lblDateNaissance.Text = "Date de naissance"
        '
        'txtPrenom
        '
        Me.txtPrenom.Affichage = SosMedecins.Controls.sosTextBox.TypeAffichage.PremiereLettreMajuscule
        Me.txtPrenom.Location = New System.Drawing.Point(242, 19)
        Me.txtPrenom.Name = "txtPrenom"
        Me.txtPrenom.ReadOnly = True
        Me.txtPrenom.Size = New System.Drawing.Size(131, 20)
        Me.txtPrenom.TabIndex = 6
        '
        'lblPrenom
        '
        Me.lblPrenom.AutoSize = True
        Me.lblPrenom.Location = New System.Drawing.Point(193, 22)
        Me.lblPrenom.Name = "lblPrenom"
        Me.lblPrenom.Size = New System.Drawing.Size(43, 13)
        Me.lblPrenom.TabIndex = 5
        Me.lblPrenom.Text = "Prénom"
        '
        'LblNom
        '
        Me.LblNom.AutoSize = True
        Me.LblNom.Location = New System.Drawing.Point(6, 22)
        Me.LblNom.Name = "LblNom"
        Me.LblNom.Size = New System.Drawing.Size(29, 13)
        Me.LblNom.TabIndex = 4
        Me.LblNom.Text = "Nom"
        '
        'txtNom
        '
        Me.txtNom.Location = New System.Drawing.Point(41, 19)
        Me.txtNom.Name = "txtNom"
        Me.txtNom.ReadOnly = True
        Me.txtNom.Size = New System.Drawing.Size(146, 20)
        Me.txtNom.TabIndex = 1
        '
        'btnDecompte
        '
        Me.btnDecompte.Image = Global.SosMedecins.SmartRapport.Facturation.My.Resources.Resources.Decomptes
        Me.btnDecompte.Location = New System.Drawing.Point(738, 16)
        Me.btnDecompte.Name = "btnDecompte"
        Me.btnDecompte.Size = New System.Drawing.Size(45, 44)
        Me.btnDecompte.TabIndex = 0
        Me.ttpPrincipal.SetToolTip(Me.btnDecompte, "Décompte")
        Me.btnDecompte.UseVisualStyleBackColor = True
        '
        'dgvListe
        '
        Me.dgvListe.AllowUserToAddRows = False
        Me.dgvListe.AllowUserToDeleteRows = False
        Me.dgvListe.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.dgvListe.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvListe.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvListe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvListe.Location = New System.Drawing.Point(6, 19)
        Me.dgvListe.MultiSelect = False
        Me.dgvListe.Name = "dgvListe"
        Me.dgvListe.RowHeadersVisible = False
        Me.dgvListe.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvListe.Size = New System.Drawing.Size(777, 254)
        Me.dgvListe.TabIndex = 2
        '
        'dgvDetail
        '
        Me.dgvDetail.AllowUserToAddRows = False
        Me.dgvDetail.AllowUserToDeleteRows = False
        Me.dgvDetail.AllowUserToResizeRows = False
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.LightSteelBlue
        Me.dgvDetail.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvDetail.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDetail.Location = New System.Drawing.Point(6, 19)
        Me.dgvDetail.MultiSelect = False
        Me.dgvDetail.Name = "dgvDetail"
        Me.dgvDetail.RowHeadersVisible = False
        Me.dgvDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvDetail.Size = New System.Drawing.Size(777, 254)
        Me.dgvDetail.TabIndex = 3
        '
        'grpListe
        '
        Me.grpListe.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpListe.Controls.Add(Me.lblNbFacture)
        Me.grpListe.Controls.Add(Me.txtSolde)
        Me.grpListe.Controls.Add(Me.txtTotal)
        Me.grpListe.Controls.Add(Me.lblSolde)
        Me.grpListe.Controls.Add(Me.lblTotal)
        Me.grpListe.Controls.Add(Me.dgvListe)
        Me.grpListe.Location = New System.Drawing.Point(12, 84)
        Me.grpListe.Name = "grpListe"
        Me.grpListe.Size = New System.Drawing.Size(789, 319)
        Me.grpListe.TabIndex = 4
        Me.grpListe.TabStop = False
        Me.grpListe.Text = "Liste des Factures"
        '
        'lblNbFacture
        '
        Me.lblNbFacture.Location = New System.Drawing.Point(6, 279)
        Me.lblNbFacture.Name = "lblNbFacture"
        Me.lblNbFacture.Size = New System.Drawing.Size(428, 17)
        Me.lblNbFacture.TabIndex = 7
        Me.lblNbFacture.Text = "Label1"
        '
        'txtSolde
        '
        Me.txtSolde.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSolde.Location = New System.Drawing.Point(667, 279)
        Me.txtSolde.Name = "txtSolde"
        Me.txtSolde.ReadOnly = True
        Me.txtSolde.Size = New System.Drawing.Size(116, 20)
        Me.txtSolde.TabIndex = 6
        Me.txtSolde.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotal
        '
        Me.txtTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotal.Location = New System.Drawing.Point(505, 279)
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.ReadOnly = True
        Me.txtTotal.Size = New System.Drawing.Size(116, 20)
        Me.txtTotal.TabIndex = 5
        Me.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblSolde
        '
        Me.lblSolde.AutoSize = True
        Me.lblSolde.Location = New System.Drawing.Point(627, 282)
        Me.lblSolde.Name = "lblSolde"
        Me.lblSolde.Size = New System.Drawing.Size(34, 13)
        Me.lblSolde.TabIndex = 4
        Me.lblSolde.Text = "Solde"
        '
        'lblTotal
        '
        Me.lblTotal.AutoSize = True
        Me.lblTotal.Location = New System.Drawing.Point(468, 282)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(31, 13)
        Me.lblTotal.TabIndex = 3
        Me.lblTotal.Text = "Total"
        '
        'grpDetails
        '
        Me.grpDetails.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpDetails.Controls.Add(Me.btnModificationSolde)
        Me.grpDetails.Controls.Add(Me.btnSupprimerDetail)
        Me.grpDetails.Controls.Add(Me.btnModifierDetail)
        Me.grpDetails.Controls.Add(Me.lblSoldeDetail)
        Me.grpDetails.Controls.Add(Me.lblTotalDetail)
        Me.grpDetails.Controls.Add(Me.txtSoldeDetail)
        Me.grpDetails.Controls.Add(Me.txtTotalDetail)
        Me.grpDetails.Controls.Add(Me.btnPaiement)
        Me.grpDetails.Controls.Add(Me.dgvDetail)
        Me.grpDetails.Location = New System.Drawing.Point(12, 409)
        Me.grpDetails.Name = "grpDetails"
        Me.grpDetails.Size = New System.Drawing.Size(789, 329)
        Me.grpDetails.TabIndex = 5
        Me.grpDetails.TabStop = False
        Me.grpDetails.Text = "Details"
        '
        'btnModificationSolde
        '
        Me.btnModificationSolde.Enabled = False
        Me.btnModificationSolde.Image = Global.SosMedecins.SmartRapport.Facturation.My.Resources.Resources.Gomme
        Me.btnModificationSolde.Location = New System.Drawing.Point(406, 279)
        Me.btnModificationSolde.Name = "btnModificationSolde"
        Me.btnModificationSolde.Size = New System.Drawing.Size(44, 42)
        Me.btnModificationSolde.TabIndex = 14
        Me.ttpPrincipal.SetToolTip(Me.btnModificationSolde, "Modification du solde de la facture")
        Me.btnModificationSolde.UseVisualStyleBackColor = True
        '
        'btnSupprimerDetail
        '
        Me.btnSupprimerDetail.Enabled = False
        Me.btnSupprimerDetail.Image = CType(resources.GetObject("btnSupprimerDetail.Image"), System.Drawing.Image)
        Me.btnSupprimerDetail.Location = New System.Drawing.Point(106, 279)
        Me.btnSupprimerDetail.Name = "btnSupprimerDetail"
        Me.btnSupprimerDetail.Size = New System.Drawing.Size(44, 42)
        Me.btnSupprimerDetail.TabIndex = 13
        Me.ttpPrincipal.SetToolTip(Me.btnSupprimerDetail, "Supprimer un paiement")
        Me.btnSupprimerDetail.UseVisualStyleBackColor = True
        '
        'btnModifierDetail
        '
        Me.btnModifierDetail.Enabled = False
        Me.btnModifierDetail.Image = Global.SosMedecins.SmartRapport.Facturation.My.Resources.Resources.Loupe
        Me.btnModifierDetail.Location = New System.Drawing.Point(56, 279)
        Me.btnModifierDetail.Name = "btnModifierDetail"
        Me.btnModifierDetail.Size = New System.Drawing.Size(44, 42)
        Me.btnModifierDetail.TabIndex = 12
        Me.ttpPrincipal.SetToolTip(Me.btnModifierDetail, "Modifier un paiement")
        Me.btnModifierDetail.UseVisualStyleBackColor = True
        '
        'lblSoldeDetail
        '
        Me.lblSoldeDetail.AutoSize = True
        Me.lblSoldeDetail.Location = New System.Drawing.Point(627, 282)
        Me.lblSoldeDetail.Name = "lblSoldeDetail"
        Me.lblSoldeDetail.Size = New System.Drawing.Size(34, 13)
        Me.lblSoldeDetail.TabIndex = 11
        Me.lblSoldeDetail.Text = "Solde"
        '
        'lblTotalDetail
        '
        Me.lblTotalDetail.AutoSize = True
        Me.lblTotalDetail.Location = New System.Drawing.Point(468, 282)
        Me.lblTotalDetail.Name = "lblTotalDetail"
        Me.lblTotalDetail.Size = New System.Drawing.Size(31, 13)
        Me.lblTotalDetail.TabIndex = 10
        Me.lblTotalDetail.Text = "Total"
        '
        'txtSoldeDetail
        '
        Me.txtSoldeDetail.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSoldeDetail.Location = New System.Drawing.Point(667, 279)
        Me.txtSoldeDetail.Name = "txtSoldeDetail"
        Me.txtSoldeDetail.ReadOnly = True
        Me.txtSoldeDetail.Size = New System.Drawing.Size(116, 20)
        Me.txtSoldeDetail.TabIndex = 9
        Me.txtSoldeDetail.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotalDetail
        '
        Me.txtTotalDetail.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalDetail.Location = New System.Drawing.Point(505, 279)
        Me.txtTotalDetail.Name = "txtTotalDetail"
        Me.txtTotalDetail.ReadOnly = True
        Me.txtTotalDetail.Size = New System.Drawing.Size(116, 20)
        Me.txtTotalDetail.TabIndex = 8
        Me.txtTotalDetail.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btnPaiement
        '
        Me.btnPaiement.Enabled = False
        Me.btnPaiement.Image = Global.SosMedecins.SmartRapport.Facturation.My.Resources.Resources.plus
        Me.btnPaiement.Location = New System.Drawing.Point(6, 278)
        Me.btnPaiement.Name = "btnPaiement"
        Me.btnPaiement.Size = New System.Drawing.Size(44, 42)
        Me.btnPaiement.TabIndex = 5
        Me.ttpPrincipal.SetToolTip(Me.btnPaiement, "Ajouter un paiement")
        Me.btnPaiement.UseVisualStyleBackColor = True
        '
        'frmDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.CadetBlue
        Me.ClientSize = New System.Drawing.Size(813, 745)
        Me.Controls.Add(Me.grpDetails)
        Me.Controls.Add(Me.grpListe)
        Me.Controls.Add(Me.grpIdentification)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Name = "frmDetails"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Factures"
        Me.grpIdentification.ResumeLayout(False)
        Me.grpIdentification.PerformLayout()
        CType(Me.dgvListe, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpListe.ResumeLayout(False)
        Me.grpListe.PerformLayout()
        Me.grpDetails.ResumeLayout(False)
        Me.grpDetails.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpIdentification As System.Windows.Forms.GroupBox
    Friend WithEvents dgvListe As System.Windows.Forms.DataGridView
    Friend WithEvents dgvDetail As System.Windows.Forms.DataGridView
    Friend WithEvents grpListe As System.Windows.Forms.GroupBox
    Friend WithEvents grpDetails As System.Windows.Forms.GroupBox
    Friend WithEvents txtSolde As System.Windows.Forms.TextBox
    Friend WithEvents txtTotal As System.Windows.Forms.TextBox
    Friend WithEvents lblSolde As System.Windows.Forms.Label
    Friend WithEvents lblTotal As System.Windows.Forms.Label
    Friend WithEvents lblSoldeDetail As System.Windows.Forms.Label
    Friend WithEvents lblTotalDetail As System.Windows.Forms.Label
    Friend WithEvents txtSoldeDetail As System.Windows.Forms.TextBox
    Friend WithEvents txtTotalDetail As System.Windows.Forms.TextBox
    Friend WithEvents btnPaiement As System.Windows.Forms.Button
    Friend WithEvents btnDecompte As System.Windows.Forms.Button
    Friend WithEvents btnSupprimerDetail As System.Windows.Forms.Button
    Friend WithEvents btnModifierDetail As System.Windows.Forms.Button
    Friend WithEvents ttpPrincipal As System.Windows.Forms.ToolTip
    Friend WithEvents btnModificationSolde As System.Windows.Forms.Button
    Friend WithEvents txtPrenom As SosMedecins.Controls.sosTextBox
    Friend WithEvents lblPrenom As System.Windows.Forms.Label
    Friend WithEvents LblNom As System.Windows.Forms.Label
    Friend WithEvents txtNom As System.Windows.Forms.TextBox
    Friend WithEvents dtbDateNaissance As SosMedecins.Controls.sosDateBox
    Friend WithEvents lblDateNaissance As System.Windows.Forms.Label
    Friend WithEvents lblNbFacture As System.Windows.Forms.Label
End Class
