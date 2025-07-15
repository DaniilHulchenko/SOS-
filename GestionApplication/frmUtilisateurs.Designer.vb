<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUtilisateurs
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUtilisateurs))
        Me.grpDetail = New System.Windows.Forms.GroupBox()
        Me.lblDateDerConnexion = New System.Windows.Forms.Label()
        Me.dtpDateDerConnexion = New System.Windows.Forms.DateTimePicker()
        Me.lblDroits = New System.Windows.Forms.Label()
        Me.cbxDroits = New System.Windows.Forms.ComboBox()
        Me.chkArchive = New System.Windows.Forms.CheckBox()
        Me.lblEAN = New System.Windows.Forms.Label()
        Me.lblCommentaire = New System.Windows.Forms.Label()
        Me.lblPrenomGeneve = New System.Windows.Forms.Label()
        Me.lblConcordat = New System.Windows.Forms.Label()
        Me.lblMail = New System.Windows.Forms.Label()
        Me.lblNom = New System.Windows.Forms.Label()
        Me.lblInitiales = New System.Windows.Forms.Label()
        Me.lblNomGeneve = New System.Windows.Forms.Label()
        Me.lblMotDePasse = New System.Windows.Forms.Label()
        Me.lblCodeUtilisateur = New System.Windows.Forms.Label()
        Me.txtNom = New System.Windows.Forms.TextBox()
        Me.txtEAN = New System.Windows.Forms.TextBox()
        Me.txtPrenomGeneve = New System.Windows.Forms.TextBox()
        Me.txtNomGeneve = New System.Windows.Forms.TextBox()
        Me.txtCommentaire = New System.Windows.Forms.TextBox()
        Me.TextBox5 = New System.Windows.Forms.TextBox()
        Me.txtMail = New System.Windows.Forms.TextBox()
        Me.txtInitiales = New System.Windows.Forms.TextBox()
        Me.txtMotDePasse = New System.Windows.Forms.TextBox()
        Me.txtCodeUtilisateur = New System.Windows.Forms.TextBox()
        Me.btnAnnuler = New System.Windows.Forms.Button()
        Me.btnEnregistrer = New System.Windows.Forms.Button()
        Me.dgvUtilisateurs = New System.Windows.Forms.DataGridView()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.RadioButton3 = New System.Windows.Forms.RadioButton()
        Me.btnDesactiver = New System.Windows.Forms.Button()
        Me.btnModifier = New System.Windows.Forms.Button()
        Me.btnActiver = New System.Windows.Forms.Button()
        Me.grpDetail.SuspendLayout()
        CType(Me.dgvUtilisateurs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grpDetail
        '
        Me.grpDetail.Controls.Add(Me.lblDateDerConnexion)
        Me.grpDetail.Controls.Add(Me.dtpDateDerConnexion)
        Me.grpDetail.Controls.Add(Me.lblDroits)
        Me.grpDetail.Controls.Add(Me.cbxDroits)
        Me.grpDetail.Controls.Add(Me.chkArchive)
        Me.grpDetail.Controls.Add(Me.lblEAN)
        Me.grpDetail.Controls.Add(Me.lblCommentaire)
        Me.grpDetail.Controls.Add(Me.lblPrenomGeneve)
        Me.grpDetail.Controls.Add(Me.lblConcordat)
        Me.grpDetail.Controls.Add(Me.lblMail)
        Me.grpDetail.Controls.Add(Me.lblNom)
        Me.grpDetail.Controls.Add(Me.lblInitiales)
        Me.grpDetail.Controls.Add(Me.lblNomGeneve)
        Me.grpDetail.Controls.Add(Me.lblMotDePasse)
        Me.grpDetail.Controls.Add(Me.lblCodeUtilisateur)
        Me.grpDetail.Controls.Add(Me.txtNom)
        Me.grpDetail.Controls.Add(Me.txtEAN)
        Me.grpDetail.Controls.Add(Me.txtPrenomGeneve)
        Me.grpDetail.Controls.Add(Me.txtNomGeneve)
        Me.grpDetail.Controls.Add(Me.txtCommentaire)
        Me.grpDetail.Controls.Add(Me.TextBox5)
        Me.grpDetail.Controls.Add(Me.txtMail)
        Me.grpDetail.Controls.Add(Me.txtInitiales)
        Me.grpDetail.Controls.Add(Me.txtMotDePasse)
        Me.grpDetail.Controls.Add(Me.txtCodeUtilisateur)
        Me.grpDetail.Controls.Add(Me.btnAnnuler)
        Me.grpDetail.Controls.Add(Me.btnEnregistrer)
        Me.grpDetail.Location = New System.Drawing.Point(12, 288)
        Me.grpDetail.Name = "grpDetail"
        Me.grpDetail.Size = New System.Drawing.Size(549, 291)
        Me.grpDetail.TabIndex = 0
        Me.grpDetail.TabStop = False
        '
        'lblDateDerConnexion
        '
        Me.lblDateDerConnexion.AutoSize = True
        Me.lblDateDerConnexion.Location = New System.Drawing.Point(17, 259)
        Me.lblDateDerConnexion.Name = "lblDateDerConnexion"
        Me.lblDateDerConnexion.Size = New System.Drawing.Size(105, 13)
        Me.lblDateDerConnexion.TabIndex = 26
        Me.lblDateDerConnexion.Text = "Derniére connexion :"
        '
        'dtpDateDerConnexion
        '
        Me.dtpDateDerConnexion.Enabled = False
        Me.dtpDateDerConnexion.Location = New System.Drawing.Point(138, 259)
        Me.dtpDateDerConnexion.Name = "dtpDateDerConnexion"
        Me.dtpDateDerConnexion.Size = New System.Drawing.Size(148, 20)
        Me.dtpDateDerConnexion.TabIndex = 25
        '
        'lblDroits
        '
        Me.lblDroits.AutoSize = True
        Me.lblDroits.Location = New System.Drawing.Point(14, 229)
        Me.lblDroits.Name = "lblDroits"
        Me.lblDroits.Size = New System.Drawing.Size(40, 13)
        Me.lblDroits.TabIndex = 24
        Me.lblDroits.Text = "Droits :"
        '
        'cbxDroits
        '
        Me.cbxDroits.Enabled = False
        Me.cbxDroits.FormattingEnabled = True
        Me.cbxDroits.Location = New System.Drawing.Point(86, 229)
        Me.cbxDroits.Name = "cbxDroits"
        Me.cbxDroits.Size = New System.Drawing.Size(163, 21)
        Me.cbxDroits.TabIndex = 23
        '
        'chkArchive
        '
        Me.chkArchive.AutoSize = True
        Me.chkArchive.Enabled = False
        Me.chkArchive.Location = New System.Drawing.Point(426, 23)
        Me.chkArchive.Name = "chkArchive"
        Me.chkArchive.Size = New System.Drawing.Size(62, 17)
        Me.chkArchive.TabIndex = 22
        Me.chkArchive.Text = "Archive"
        Me.chkArchive.UseVisualStyleBackColor = True
        '
        'lblEAN
        '
        Me.lblEAN.AutoSize = True
        Me.lblEAN.Location = New System.Drawing.Point(6, 133)
        Me.lblEAN.Name = "lblEAN"
        Me.lblEAN.Size = New System.Drawing.Size(63, 13)
        Me.lblEAN.TabIndex = 21
        Me.lblEAN.Text = "Code EAN :"
        '
        'lblCommentaire
        '
        Me.lblCommentaire.AutoSize = True
        Me.lblCommentaire.Location = New System.Drawing.Point(6, 205)
        Me.lblCommentaire.Name = "lblCommentaire"
        Me.lblCommentaire.Size = New System.Drawing.Size(74, 13)
        Me.lblCommentaire.TabIndex = 20
        Me.lblCommentaire.Text = "Commentaire :"
        '
        'lblPrenomGeneve
        '
        Me.lblPrenomGeneve.AutoSize = True
        Me.lblPrenomGeneve.Location = New System.Drawing.Point(235, 99)
        Me.lblPrenomGeneve.Name = "lblPrenomGeneve"
        Me.lblPrenomGeneve.Size = New System.Drawing.Size(84, 13)
        Me.lblPrenomGeneve.TabIndex = 19
        Me.lblPrenomGeneve.Text = "Prénom Geneve"
        '
        'lblConcordat
        '
        Me.lblConcordat.AutoSize = True
        Me.lblConcordat.Location = New System.Drawing.Point(322, 129)
        Me.lblConcordat.Name = "lblConcordat"
        Me.lblConcordat.Size = New System.Drawing.Size(62, 13)
        Me.lblConcordat.TabIndex = 18
        Me.lblConcordat.Text = "Concordat :"
        '
        'lblMail
        '
        Me.lblMail.AutoSize = True
        Me.lblMail.Location = New System.Drawing.Point(48, 179)
        Me.lblMail.Name = "lblMail"
        Me.lblMail.Size = New System.Drawing.Size(32, 13)
        Me.lblMail.TabIndex = 17
        Me.lblMail.Text = "Mail :"
        '
        'lblNom
        '
        Me.lblNom.AutoSize = True
        Me.lblNom.Location = New System.Drawing.Point(42, 51)
        Me.lblNom.Name = "lblNom"
        Me.lblNom.Size = New System.Drawing.Size(35, 13)
        Me.lblNom.TabIndex = 16
        Me.lblNom.Text = "Nom :"
        '
        'lblInitiales
        '
        Me.lblInitiales.AutoSize = True
        Me.lblInitiales.Location = New System.Drawing.Point(431, 99)
        Me.lblInitiales.Name = "lblInitiales"
        Me.lblInitiales.Size = New System.Drawing.Size(48, 13)
        Me.lblInitiales.TabIndex = 15
        Me.lblInitiales.Text = "Initiales :"
        '
        'lblNomGeneve
        '
        Me.lblNomGeneve.AutoSize = True
        Me.lblNomGeneve.Location = New System.Drawing.Point(6, 99)
        Me.lblNomGeneve.Name = "lblNomGeneve"
        Me.lblNomGeneve.Size = New System.Drawing.Size(74, 13)
        Me.lblNomGeneve.TabIndex = 14
        Me.lblNomGeneve.Text = "Nom geneve :"
        '
        'lblMotDePasse
        '
        Me.lblMotDePasse.AutoSize = True
        Me.lblMotDePasse.Location = New System.Drawing.Point(242, 51)
        Me.lblMotDePasse.Name = "lblMotDePasse"
        Me.lblMotDePasse.Size = New System.Drawing.Size(77, 13)
        Me.lblMotDePasse.TabIndex = 13
        Me.lblMotDePasse.Text = "Mot de passe :"
        '
        'lblCodeUtilisateur
        '
        Me.lblCodeUtilisateur.AutoSize = True
        Me.lblCodeUtilisateur.Location = New System.Drawing.Point(42, 22)
        Me.lblCodeUtilisateur.Name = "lblCodeUtilisateur"
        Me.lblCodeUtilisateur.Size = New System.Drawing.Size(38, 13)
        Me.lblCodeUtilisateur.TabIndex = 12
        Me.lblCodeUtilisateur.Text = "Code :"
        '
        'txtNom
        '
        Me.txtNom.Location = New System.Drawing.Point(86, 48)
        Me.txtNom.Name = "txtNom"
        Me.txtNom.ReadOnly = True
        Me.txtNom.Size = New System.Drawing.Size(123, 20)
        Me.txtNom.TabIndex = 11
        '
        'txtEAN
        '
        Me.txtEAN.Location = New System.Drawing.Point(86, 133)
        Me.txtEAN.Name = "txtEAN"
        Me.txtEAN.ReadOnly = True
        Me.txtEAN.Size = New System.Drawing.Size(123, 20)
        Me.txtEAN.TabIndex = 10
        '
        'txtPrenomGeneve
        '
        Me.txtPrenomGeneve.Location = New System.Drawing.Point(325, 96)
        Me.txtPrenomGeneve.Name = "txtPrenomGeneve"
        Me.txtPrenomGeneve.ReadOnly = True
        Me.txtPrenomGeneve.Size = New System.Drawing.Size(100, 20)
        Me.txtPrenomGeneve.TabIndex = 9
        '
        'txtNomGeneve
        '
        Me.txtNomGeneve.Location = New System.Drawing.Point(86, 96)
        Me.txtNomGeneve.Name = "txtNomGeneve"
        Me.txtNomGeneve.ReadOnly = True
        Me.txtNomGeneve.Size = New System.Drawing.Size(123, 20)
        Me.txtNomGeneve.TabIndex = 8
        '
        'txtCommentaire
        '
        Me.txtCommentaire.Location = New System.Drawing.Point(86, 202)
        Me.txtCommentaire.Name = "txtCommentaire"
        Me.txtCommentaire.ReadOnly = True
        Me.txtCommentaire.Size = New System.Drawing.Size(457, 20)
        Me.txtCommentaire.TabIndex = 7
        '
        'TextBox5
        '
        Me.TextBox5.Location = New System.Drawing.Point(390, 126)
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.ReadOnly = True
        Me.TextBox5.Size = New System.Drawing.Size(100, 20)
        Me.TextBox5.TabIndex = 6
        '
        'txtMail
        '
        Me.txtMail.Location = New System.Drawing.Point(86, 176)
        Me.txtMail.Name = "txtMail"
        Me.txtMail.ReadOnly = True
        Me.txtMail.Size = New System.Drawing.Size(206, 20)
        Me.txtMail.TabIndex = 5
        '
        'txtInitiales
        '
        Me.txtInitiales.Location = New System.Drawing.Point(485, 96)
        Me.txtInitiales.Name = "txtInitiales"
        Me.txtInitiales.ReadOnly = True
        Me.txtInitiales.Size = New System.Drawing.Size(34, 20)
        Me.txtInitiales.TabIndex = 4
        '
        'txtMotDePasse
        '
        Me.txtMotDePasse.Location = New System.Drawing.Point(325, 48)
        Me.txtMotDePasse.Name = "txtMotDePasse"
        Me.txtMotDePasse.ReadOnly = True
        Me.txtMotDePasse.Size = New System.Drawing.Size(100, 20)
        Me.txtMotDePasse.TabIndex = 3
        '
        'txtCodeUtilisateur
        '
        Me.txtCodeUtilisateur.Location = New System.Drawing.Point(86, 19)
        Me.txtCodeUtilisateur.Name = "txtCodeUtilisateur"
        Me.txtCodeUtilisateur.ReadOnly = True
        Me.txtCodeUtilisateur.Size = New System.Drawing.Size(75, 20)
        Me.txtCodeUtilisateur.TabIndex = 2
        '
        'btnAnnuler
        '
        Me.btnAnnuler.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAnnuler.Enabled = False
        Me.btnAnnuler.Location = New System.Drawing.Point(446, 253)
        Me.btnAnnuler.Name = "btnAnnuler"
        Me.btnAnnuler.Size = New System.Drawing.Size(97, 32)
        Me.btnAnnuler.TabIndex = 1
        Me.btnAnnuler.Text = "Annuler"
        Me.btnAnnuler.UseVisualStyleBackColor = True
        '
        'btnEnregistrer
        '
        Me.btnEnregistrer.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnEnregistrer.Enabled = False
        Me.btnEnregistrer.Location = New System.Drawing.Point(363, 253)
        Me.btnEnregistrer.Name = "btnEnregistrer"
        Me.btnEnregistrer.Size = New System.Drawing.Size(77, 32)
        Me.btnEnregistrer.TabIndex = 0
        Me.btnEnregistrer.Text = "Enregistrer"
        Me.btnEnregistrer.UseVisualStyleBackColor = True
        '
        'dgvUtilisateurs
        '
        Me.dgvUtilisateurs.AllowUserToAddRows = False
        Me.dgvUtilisateurs.AllowUserToDeleteRows = False
        Me.dgvUtilisateurs.AllowUserToResizeRows = False
        Me.dgvUtilisateurs.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvUtilisateurs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgvUtilisateurs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvUtilisateurs.Location = New System.Drawing.Point(12, 41)
        Me.dgvUtilisateurs.MultiSelect = False
        Me.dgvUtilisateurs.Name = "dgvUtilisateurs"
        Me.dgvUtilisateurs.ReadOnly = True
        Me.dgvUtilisateurs.RowHeadersVisible = False
        Me.dgvUtilisateurs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvUtilisateurs.Size = New System.Drawing.Size(549, 203)
        Me.dgvUtilisateurs.TabIndex = 1
        '
        'RadioButton1
        '
        Me.RadioButton1.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Checked = True
        Me.RadioButton1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.RadioButton1.Location = New System.Drawing.Point(12, 12)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(38, 23)
        Me.RadioButton1.TabIndex = 3
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "Actif"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'RadioButton2
        '
        Me.RadioButton2.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.RadioButton2.Location = New System.Drawing.Point(56, 12)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(46, 23)
        Me.RadioButton2.TabIndex = 4
        Me.RadioButton2.Text = "Inactif"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'RadioButton3
        '
        Me.RadioButton3.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioButton3.AutoSize = True
        Me.RadioButton3.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.RadioButton3.Location = New System.Drawing.Point(108, 12)
        Me.RadioButton3.Name = "RadioButton3"
        Me.RadioButton3.Size = New System.Drawing.Size(41, 23)
        Me.RadioButton3.TabIndex = 5
        Me.RadioButton3.Text = "Tous"
        Me.RadioButton3.UseVisualStyleBackColor = True
        '
        'btnDesactiver
        '
        Me.btnDesactiver.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnDesactiver.Location = New System.Drawing.Point(494, 250)
        Me.btnDesactiver.Name = "btnDesactiver"
        Me.btnDesactiver.Size = New System.Drawing.Size(67, 32)
        Me.btnDesactiver.TabIndex = 6
        Me.btnDesactiver.Text = "Désactiver"
        Me.btnDesactiver.UseVisualStyleBackColor = True
        '
        'btnModifier
        '
        Me.btnModifier.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnModifier.Location = New System.Drawing.Point(315, 250)
        Me.btnModifier.Name = "btnModifier"
        Me.btnModifier.Size = New System.Drawing.Size(77, 32)
        Me.btnModifier.TabIndex = 7
        Me.btnModifier.Text = "Modifier"
        Me.btnModifier.UseVisualStyleBackColor = True
        '
        'btnActiver
        '
        Me.btnActiver.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnActiver.Location = New System.Drawing.Point(398, 250)
        Me.btnActiver.Name = "btnActiver"
        Me.btnActiver.Size = New System.Drawing.Size(90, 32)
        Me.btnActiver.TabIndex = 8
        Me.btnActiver.Text = "Activer"
        Me.btnActiver.UseVisualStyleBackColor = True
        '
        'frmUtilisateurs
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.CadetBlue
        Me.ClientSize = New System.Drawing.Size(573, 591)
        Me.Controls.Add(Me.btnActiver)
        Me.Controls.Add(Me.btnModifier)
        Me.Controls.Add(Me.btnDesactiver)
        Me.Controls.Add(Me.RadioButton3)
        Me.Controls.Add(Me.RadioButton2)
        Me.Controls.Add(Me.RadioButton1)
        Me.Controls.Add(Me.dgvUtilisateurs)
        Me.Controls.Add(Me.grpDetail)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmUtilisateurs"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Gestion des utilisateurs"
        Me.grpDetail.ResumeLayout(False)
        Me.grpDetail.PerformLayout()
        CType(Me.dgvUtilisateurs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grpDetail As System.Windows.Forms.GroupBox
    Friend WithEvents dgvUtilisateurs As System.Windows.Forms.DataGridView
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton2 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton3 As System.Windows.Forms.RadioButton
    Friend WithEvents btnAnnuler As System.Windows.Forms.Button
    Friend WithEvents btnEnregistrer As System.Windows.Forms.Button
    Friend WithEvents btnDesactiver As System.Windows.Forms.Button
    Friend WithEvents btnModifier As System.Windows.Forms.Button
    Friend WithEvents btnActiver As System.Windows.Forms.Button
    Friend WithEvents lblEAN As System.Windows.Forms.Label
    Friend WithEvents lblCommentaire As System.Windows.Forms.Label
    Friend WithEvents lblPrenomGeneve As System.Windows.Forms.Label
    Friend WithEvents lblConcordat As System.Windows.Forms.Label
    Friend WithEvents lblMail As System.Windows.Forms.Label
    Friend WithEvents lblNom As System.Windows.Forms.Label
    Friend WithEvents lblInitiales As System.Windows.Forms.Label
    Friend WithEvents lblNomGeneve As System.Windows.Forms.Label
    Friend WithEvents lblMotDePasse As System.Windows.Forms.Label
    Friend WithEvents lblCodeUtilisateur As System.Windows.Forms.Label
    Friend WithEvents txtNom As System.Windows.Forms.TextBox
    Friend WithEvents txtEAN As System.Windows.Forms.TextBox
    Friend WithEvents txtPrenomGeneve As System.Windows.Forms.TextBox
    Friend WithEvents txtNomGeneve As System.Windows.Forms.TextBox
    Friend WithEvents txtCommentaire As System.Windows.Forms.TextBox
    Friend WithEvents TextBox5 As System.Windows.Forms.TextBox
    Friend WithEvents txtMail As System.Windows.Forms.TextBox
    Friend WithEvents txtInitiales As System.Windows.Forms.TextBox
    Friend WithEvents txtMotDePasse As System.Windows.Forms.TextBox
    Friend WithEvents txtCodeUtilisateur As System.Windows.Forms.TextBox
    Friend WithEvents chkArchive As System.Windows.Forms.CheckBox
    Friend WithEvents lblDateDerConnexion As System.Windows.Forms.Label
    Friend WithEvents dtpDateDerConnexion As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblDroits As System.Windows.Forms.Label
    Friend WithEvents cbxDroits As System.Windows.Forms.ComboBox

End Class
