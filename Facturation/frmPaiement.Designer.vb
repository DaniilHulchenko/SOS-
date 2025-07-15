<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPaiement
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
        Me.grpEntete = New System.Windows.Forms.GroupBox()
        Me.dbxDateSal = New SosMedecins.Controls.sosDateBox()
        Me.dbxDate = New SosMedecins.Controls.sosDateBox()
        Me.lblDatePaiment = New System.Windows.Forms.Label()
        Me.lblDateSalaire = New System.Windows.Forms.Label()
        Me.groupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtMontant = New SosMedecins.Controls.sosTextBox()
        Me.lblType = New System.Windows.Forms.Label()
        Me.cbxType = New System.Windows.Forms.ComboBox()
        Me.lblMontant = New System.Windows.Forms.Label()
        Me.cbxMoyen = New System.Windows.Forms.ComboBox()
        Me.lblMoyen = New System.Windows.Forms.Label()
        Me.txtPayeCommentaire = New System.Windows.Forms.TextBox()
        Me.lblCommentaire = New System.Windows.Forms.Label()
        Me.btnAnnuler = New System.Windows.Forms.Button()
        Me.btnValider = New System.Windows.Forms.Button()
        Me.errErreur = New SosMedecins.Controls.sosErrorProvider()
        Me.grpEntete.SuspendLayout()
        Me.groupBox1.SuspendLayout()
        CType(Me.errErreur, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grpEntete
        '
        Me.grpEntete.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpEntete.Controls.Add(Me.dbxDateSal)
        Me.grpEntete.Controls.Add(Me.dbxDate)
        Me.grpEntete.Controls.Add(Me.lblDatePaiment)
        Me.grpEntete.Controls.Add(Me.lblDateSalaire)
        Me.grpEntete.Location = New System.Drawing.Point(12, 12)
        Me.grpEntete.Name = "grpEntete"
        Me.grpEntete.Size = New System.Drawing.Size(407, 57)
        Me.grpEntete.TabIndex = 0
        Me.grpEntete.TabStop = False
        '
        'dbxDateSal
        '
        Me.dbxDateSal.Location = New System.Drawing.Point(301, 19)
        Me.dbxDateSal.Mask = "00/00/0000"
        Me.dbxDateSal.Name = "dbxDateSal"
        Me.dbxDateSal.Size = New System.Drawing.Size(73, 20)
        Me.dbxDateSal.TabIndex = 3
        Me.dbxDateSal.ValidatingType = GetType(Date)
        Me.dbxDateSal.Value = ""
        '
        'dbxDate
        '
        Me.dbxDate.Location = New System.Drawing.Point(87, 19)
        Me.dbxDate.Mask = "00/00/0000"
        Me.dbxDate.Name = "dbxDate"
        Me.dbxDate.Size = New System.Drawing.Size(71, 20)
        Me.dbxDate.TabIndex = 1
        Me.dbxDate.ValidatingType = GetType(Date)
        Me.dbxDate.Value = ""
        '
        'lblDatePaiment
        '
        Me.lblDatePaiment.AutoSize = True
        Me.lblDatePaiment.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDatePaiment.Location = New System.Drawing.Point(6, 22)
        Me.lblDatePaiment.Name = "lblDatePaiment"
        Me.lblDatePaiment.Size = New System.Drawing.Size(75, 14)
        Me.lblDatePaiment.TabIndex = 0
        Me.lblDatePaiment.Text = "Date Paiment :"
        '
        'lblDateSalaire
        '
        Me.lblDateSalaire.AutoSize = True
        Me.lblDateSalaire.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDateSalaire.Location = New System.Drawing.Point(199, 23)
        Me.lblDateSalaire.Name = "lblDateSalaire"
        Me.lblDateSalaire.Size = New System.Drawing.Size(96, 14)
        Me.lblDateSalaire.TabIndex = 2
        Me.lblDateSalaire.Text = "Date Pour Salaire :"
        Me.lblDateSalaire.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'groupBox1
        '
        Me.groupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.groupBox1.Controls.Add(Me.txtMontant)
        Me.groupBox1.Controls.Add(Me.lblType)
        Me.groupBox1.Controls.Add(Me.cbxType)
        Me.groupBox1.Controls.Add(Me.lblMontant)
        Me.groupBox1.Controls.Add(Me.cbxMoyen)
        Me.groupBox1.Controls.Add(Me.lblMoyen)
        Me.groupBox1.Controls.Add(Me.txtPayeCommentaire)
        Me.groupBox1.Controls.Add(Me.lblCommentaire)
        Me.groupBox1.Location = New System.Drawing.Point(13, 75)
        Me.groupBox1.Name = "groupBox1"
        Me.groupBox1.Size = New System.Drawing.Size(406, 167)
        Me.groupBox1.TabIndex = 1
        Me.groupBox1.TabStop = False
        '
        'txtMontant
        '
        Me.txtMontant.Espace = False
        Me.txtMontant.Location = New System.Drawing.Point(86, 73)
        Me.txtMontant.Name = "txtMontant"
        Me.txtMontant.Negatif = True
        Me.txtMontant.PartieDecimale = 2
        Me.txtMontant.PartieEntiere = 10
        Me.txtMontant.Saisie = SosMedecins.Controls.sosTextBox.TypeSaisie.SaisieDecimal
        Me.txtMontant.Size = New System.Drawing.Size(71, 20)
        Me.txtMontant.TabIndex = 5
        Me.txtMontant.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblType
        '
        Me.lblType.AutoSize = True
        Me.lblType.Location = New System.Drawing.Point(6, 22)
        Me.lblType.Name = "lblType"
        Me.lblType.Size = New System.Drawing.Size(31, 13)
        Me.lblType.TabIndex = 0
        Me.lblType.Text = "Type"
        '
        'cbxType
        '
        Me.cbxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbxType.FormattingEnabled = True
        Me.cbxType.Location = New System.Drawing.Point(86, 19)
        Me.cbxType.Name = "cbxType"
        Me.cbxType.Size = New System.Drawing.Size(156, 21)
        Me.cbxType.TabIndex = 1
        '
        'lblMontant
        '
        Me.lblMontant.AutoSize = True
        Me.lblMontant.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMontant.Location = New System.Drawing.Point(6, 76)
        Me.lblMontant.Name = "lblMontant"
        Me.lblMontant.Size = New System.Drawing.Size(51, 14)
        Me.lblMontant.TabIndex = 4
        Me.lblMontant.Text = "Montant :"
        '
        'cbxMoyen
        '
        Me.cbxMoyen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbxMoyen.Items.AddRange(New Object() {"CDM", "SBVR", "CCP", "CS", "BM", "AT", "AUCUN"})
        Me.cbxMoyen.Location = New System.Drawing.Point(86, 46)
        Me.cbxMoyen.Name = "cbxMoyen"
        Me.cbxMoyen.Size = New System.Drawing.Size(156, 21)
        Me.cbxMoyen.TabIndex = 3
        '
        'lblMoyen
        '
        Me.lblMoyen.AutoSize = True
        Me.lblMoyen.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMoyen.Location = New System.Drawing.Point(6, 47)
        Me.lblMoyen.Name = "lblMoyen"
        Me.lblMoyen.Size = New System.Drawing.Size(45, 14)
        Me.lblMoyen.TabIndex = 2
        Me.lblMoyen.Text = "Moyen :"
        Me.lblMoyen.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtPayeCommentaire
        '
        Me.txtPayeCommentaire.Location = New System.Drawing.Point(86, 99)
        Me.txtPayeCommentaire.Multiline = True
        Me.txtPayeCommentaire.Name = "txtPayeCommentaire"
        Me.txtPayeCommentaire.Size = New System.Drawing.Size(308, 56)
        Me.txtPayeCommentaire.TabIndex = 7
        '
        'lblCommentaire
        '
        Me.lblCommentaire.AutoSize = True
        Me.lblCommentaire.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCommentaire.Location = New System.Drawing.Point(5, 102)
        Me.lblCommentaire.Name = "lblCommentaire"
        Me.lblCommentaire.Size = New System.Drawing.Size(75, 14)
        Me.lblCommentaire.TabIndex = 6
        Me.lblCommentaire.Text = "Commentaire :"
        '
        'btnAnnuler
        '
        Me.btnAnnuler.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAnnuler.BackgroundImage = Global.SosMedecins.SmartRapport.Facturation.My.Resources.Resources.Fermer
        Me.btnAnnuler.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnAnnuler.Location = New System.Drawing.Point(343, 248)
        Me.btnAnnuler.Name = "btnAnnuler"
        Me.btnAnnuler.Size = New System.Drawing.Size(76, 34)
        Me.btnAnnuler.TabIndex = 3
        Me.btnAnnuler.UseVisualStyleBackColor = False
        '
        'btnValider
        '
        Me.btnValider.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnValider.BackgroundImage = Global.SosMedecins.SmartRapport.Facturation.My.Resources.Resources.Valider
        Me.btnValider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnValider.Location = New System.Drawing.Point(261, 248)
        Me.btnValider.Name = "btnValider"
        Me.btnValider.Size = New System.Drawing.Size(76, 34)
        Me.btnValider.TabIndex = 2
        Me.btnValider.UseVisualStyleBackColor = False
        '
        'errErreur
        '
        Me.errErreur.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.errErreur.ContainerControl = Me
        Me.errErreur.MessageErreur = ""
        '
        'frmPaiement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.CadetBlue
        Me.ClientSize = New System.Drawing.Size(431, 294)
        Me.ControlBox = False
        Me.Controls.Add(Me.grpEntete)
        Me.Controls.Add(Me.groupBox1)
        Me.Controls.Add(Me.btnAnnuler)
        Me.Controls.Add(Me.btnValider)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPaiement"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Paiement"
        Me.grpEntete.ResumeLayout(False)
        Me.grpEntete.PerformLayout()
        Me.groupBox1.ResumeLayout(False)
        Me.groupBox1.PerformLayout()
        CType(Me.errErreur, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents grpEntete As System.Windows.Forms.GroupBox
    Private WithEvents lblDatePaiment As System.Windows.Forms.Label
    Private WithEvents lblDateSalaire As System.Windows.Forms.Label
    Private WithEvents groupBox1 As System.Windows.Forms.GroupBox
    Private WithEvents lblType As System.Windows.Forms.Label
    Private WithEvents cbxType As System.Windows.Forms.ComboBox
    Private WithEvents lblMontant As System.Windows.Forms.Label
    Private WithEvents cbxMoyen As System.Windows.Forms.ComboBox
    Private WithEvents lblMoyen As System.Windows.Forms.Label
    Private WithEvents txtPayeCommentaire As System.Windows.Forms.TextBox
    Private WithEvents lblCommentaire As System.Windows.Forms.Label
    Private WithEvents btnAnnuler As System.Windows.Forms.Button
    Private WithEvents btnValider As System.Windows.Forms.Button
    Friend WithEvents txtMontant As SosMedecins.Controls.sosTextBox
    Friend WithEvents dbxDate As SosMedecins.Controls.sosDateBox
    Friend WithEvents dbxDateSal As SosMedecins.Controls.sosDateBox
    Friend WithEvents errErreur As SosMedecins.Controls.sosErrorProvider
End Class
