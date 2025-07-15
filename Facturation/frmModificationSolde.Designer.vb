<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmModificationSolde
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
        Me.btnValider = New System.Windows.Forms.Button()
        Me.btnAnnuler = New System.Windows.Forms.Button()
        Me.lblAvertissement = New System.Windows.Forms.Label()
        Me.grpSolde = New System.Windows.Forms.GroupBox()
        Me.txtNouveauSolde = New SosMedecins.Controls.sosTextBox()
        Me.txtAncienSolde = New System.Windows.Forms.TextBox()
        Me.LblNouveauSolde = New System.Windows.Forms.Label()
        Me.lblAncienSolde = New System.Windows.Forms.Label()
        Me.grpSolde.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnValider
        '
        Me.btnValider.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnValider.Enabled = False
        Me.btnValider.Image = Global.SosMedecins.SmartRapport.Facturation.My.Resources.Resources.Valider
        Me.btnValider.Location = New System.Drawing.Point(97, 163)
        Me.btnValider.Name = "btnValider"
        Me.btnValider.Size = New System.Drawing.Size(44, 42)
        Me.btnValider.TabIndex = 2
        Me.btnValider.UseVisualStyleBackColor = True
        '
        'btnAnnuler
        '
        Me.btnAnnuler.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAnnuler.BackgroundImage = Global.SosMedecins.SmartRapport.Facturation.My.Resources.Resources.Fermer
        Me.btnAnnuler.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnAnnuler.Location = New System.Drawing.Point(147, 163)
        Me.btnAnnuler.Name = "btnAnnuler"
        Me.btnAnnuler.Size = New System.Drawing.Size(44, 42)
        Me.btnAnnuler.TabIndex = 3
        Me.btnAnnuler.UseVisualStyleBackColor = True
        '
        'lblAvertissement
        '
        Me.lblAvertissement.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblAvertissement.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAvertissement.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAvertissement.ForeColor = System.Drawing.Color.DarkRed
        Me.lblAvertissement.Location = New System.Drawing.Point(12, 9)
        Me.lblAvertissement.Name = "lblAvertissement"
        Me.lblAvertissement.Size = New System.Drawing.Size(179, 55)
        Me.lblAvertissement.TabIndex = 0
        Me.lblAvertissement.Text = "Label1"
        Me.lblAvertissement.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'grpSolde
        '
        Me.grpSolde.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpSolde.Controls.Add(Me.txtNouveauSolde)
        Me.grpSolde.Controls.Add(Me.txtAncienSolde)
        Me.grpSolde.Controls.Add(Me.LblNouveauSolde)
        Me.grpSolde.Controls.Add(Me.lblAncienSolde)
        Me.grpSolde.Location = New System.Drawing.Point(12, 75)
        Me.grpSolde.Name = "grpSolde"
        Me.grpSolde.Size = New System.Drawing.Size(179, 82)
        Me.grpSolde.TabIndex = 1
        Me.grpSolde.TabStop = False
        Me.grpSolde.Text = "Soldes"
        '
        'txtNouveauSolde
        '
        Me.txtNouveauSolde.Espace = False
        Me.txtNouveauSolde.Location = New System.Drawing.Point(65, 50)
        Me.txtNouveauSolde.Name = "txtNouveauSolde"
        Me.txtNouveauSolde.PartieDecimale = 2
        Me.txtNouveauSolde.PartieEntiere = 10
        Me.txtNouveauSolde.Saisie = SosMedecins.Controls.sosTextBox.TypeSaisie.SaisieDecimal
        Me.txtNouveauSolde.Size = New System.Drawing.Size(102, 20)
        Me.txtNouveauSolde.TabIndex = 3
        '
        'txtAncienSolde
        '
        Me.txtAncienSolde.Enabled = False
        Me.txtAncienSolde.Location = New System.Drawing.Point(65, 24)
        Me.txtAncienSolde.Name = "txtAncienSolde"
        Me.txtAncienSolde.Size = New System.Drawing.Size(102, 20)
        Me.txtAncienSolde.TabIndex = 1
        '
        'LblNouveauSolde
        '
        Me.LblNouveauSolde.AutoSize = True
        Me.LblNouveauSolde.Location = New System.Drawing.Point(6, 53)
        Me.LblNouveauSolde.Name = "LblNouveauSolde"
        Me.LblNouveauSolde.Size = New System.Drawing.Size(51, 13)
        Me.LblNouveauSolde.TabIndex = 2
        Me.LblNouveauSolde.Text = "Nouveau"
        '
        'lblAncienSolde
        '
        Me.lblAncienSolde.AutoSize = True
        Me.lblAncienSolde.Location = New System.Drawing.Point(6, 27)
        Me.lblAncienSolde.Name = "lblAncienSolde"
        Me.lblAncienSolde.Size = New System.Drawing.Size(40, 13)
        Me.lblAncienSolde.TabIndex = 0
        Me.lblAncienSolde.Text = "Ancien"
        '
        'frmModificationSolde
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.CadetBlue
        Me.ClientSize = New System.Drawing.Size(203, 217)
        Me.ControlBox = False
        Me.Controls.Add(Me.grpSolde)
        Me.Controls.Add(Me.lblAvertissement)
        Me.Controls.Add(Me.btnAnnuler)
        Me.Controls.Add(Me.btnValider)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmModificationSolde"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Modification du Solde"
        Me.grpSolde.ResumeLayout(False)
        Me.grpSolde.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnValider As System.Windows.Forms.Button
    Friend WithEvents btnAnnuler As System.Windows.Forms.Button
    Friend WithEvents lblAvertissement As System.Windows.Forms.Label
    Friend WithEvents grpSolde As System.Windows.Forms.GroupBox
    Friend WithEvents txtAncienSolde As System.Windows.Forms.TextBox
    Friend WithEvents LblNouveauSolde As System.Windows.Forms.Label
    Friend WithEvents lblAncienSolde As System.Windows.Forms.Label
    Friend WithEvents txtNouveauSolde As SosMedecins.Controls.sosTextBox
End Class
