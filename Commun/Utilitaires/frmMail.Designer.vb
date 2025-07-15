<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMail
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMail))
        Me.groupBox1 = New System.Windows.Forms.GroupBox
        Me.label1 = New System.Windows.Forms.Label
        Me.txtObjet = New System.Windows.Forms.TextBox
        Me.txtSujet = New System.Windows.Forms.TextBox
        Me.txtMail = New System.Windows.Forms.TextBox
        Me.label3 = New System.Windows.Forms.Label
        Me.label2 = New System.Windows.Forms.Label
        Me.btnAnnuler = New System.Windows.Forms.Button
        Me.btnValider = New System.Windows.Forms.Button
        Me.groupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'groupBox1
        '
        Me.groupBox1.Controls.Add(Me.label1)
        Me.groupBox1.Controls.Add(Me.txtObjet)
        Me.groupBox1.Controls.Add(Me.txtSujet)
        Me.groupBox1.Controls.Add(Me.txtMail)
        Me.groupBox1.Controls.Add(Me.label3)
        Me.groupBox1.Controls.Add(Me.label2)
        Me.groupBox1.Location = New System.Drawing.Point(12, 12)
        Me.groupBox1.Name = "groupBox1"
        Me.groupBox1.Size = New System.Drawing.Size(460, 240)
        Me.groupBox1.TabIndex = 3
        Me.groupBox1.TabStop = False
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Location = New System.Drawing.Point(6, 22)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(51, 13)
        Me.label1.TabIndex = 0
        Me.label1.Text = "Adresse :"
        '
        'txtObjet
        '
        Me.txtObjet.Location = New System.Drawing.Point(80, 71)
        Me.txtObjet.Multiline = True
        Me.txtObjet.Name = "txtObjet"
        Me.txtObjet.Size = New System.Drawing.Size(374, 160)
        Me.txtObjet.TabIndex = 5
        '
        'txtSujet
        '
        Me.txtSujet.Location = New System.Drawing.Point(81, 45)
        Me.txtSujet.Name = "txtSujet"
        Me.txtSujet.Size = New System.Drawing.Size(373, 20)
        Me.txtSujet.TabIndex = 3
        '
        'txtMail
        '
        Me.txtMail.Location = New System.Drawing.Point(81, 19)
        Me.txtMail.Name = "txtMail"
        Me.txtMail.Size = New System.Drawing.Size(373, 20)
        Me.txtMail.TabIndex = 1
        '
        'label3
        '
        Me.label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label3.Location = New System.Drawing.Point(6, 74)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(54, 24)
        Me.label3.TabIndex = 4
        Me.label3.Text = "Objet :"
        '
        'label2
        '
        Me.label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label2.Location = New System.Drawing.Point(6, 48)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(54, 24)
        Me.label2.TabIndex = 2
        Me.label2.Text = "Sujet :"
        '
        'btnAnnuler
        '
        Me.btnAnnuler.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAnnuler.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.btnAnnuler.BackgroundImage = Global.SosMedecins.Utilitaires.My.Resources.Resources.Annuler
        Me.btnAnnuler.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnAnnuler.Location = New System.Drawing.Point(394, 260)
        Me.btnAnnuler.Name = "btnAnnuler"
        Me.btnAnnuler.Size = New System.Drawing.Size(80, 48)
        Me.btnAnnuler.TabIndex = 5
        Me.btnAnnuler.UseVisualStyleBackColor = False
        '
        'btnValider
        '
        Me.btnValider.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnValider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnValider.Image = Global.SosMedecins.Utilitaires.My.Resources.Resources.E_mail
        Me.btnValider.Location = New System.Drawing.Point(308, 260)
        Me.btnValider.Name = "btnValider"
        Me.btnValider.Size = New System.Drawing.Size(80, 48)
        Me.btnValider.TabIndex = 4
        Me.btnValider.UseVisualStyleBackColor = False
        '
        'frmMail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnAnnuler
        Me.ClientSize = New System.Drawing.Size(486, 320)
        Me.Controls.Add(Me.groupBox1)
        Me.Controls.Add(Me.btnAnnuler)
        Me.Controls.Add(Me.btnValider)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmMail"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Envoi de mail"
        Me.groupBox1.ResumeLayout(False)
        Me.groupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents groupBox1 As System.Windows.Forms.GroupBox
    Private WithEvents label1 As System.Windows.Forms.Label
    Private WithEvents txtObjet As System.Windows.Forms.TextBox
    Private WithEvents txtSujet As System.Windows.Forms.TextBox
    Private WithEvents txtMail As System.Windows.Forms.TextBox
    Private WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents label2 As System.Windows.Forms.Label
    Private WithEvents btnAnnuler As System.Windows.Forms.Button
    Private WithEvents btnValider As System.Windows.Forms.Button
End Class
