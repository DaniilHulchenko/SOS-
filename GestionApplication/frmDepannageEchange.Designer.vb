<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDepannageEchange
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
        Me.btnExecuter = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.lblFin = New System.Windows.Forms.Label
        Me.txtFin = New System.Windows.Forms.TextBox
        Me.txtDebut = New System.Windows.Forms.TextBox
        Me.lblDebut = New System.Windows.Forms.Label
        Me.lblMotdePasse = New System.Windows.Forms.Label
        Me.txtMotDePasse = New System.Windows.Forms.TextBox
        Me.btnClose = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnExecuter
        '
        Me.btnExecuter.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExecuter.Image = Global.SosMedecins.SmartRapport.GestionApplication.My.Resources.Resources.Actualiser
        Me.btnExecuter.Location = New System.Drawing.Point(152, 125)
        Me.btnExecuter.Name = "btnExecuter"
        Me.btnExecuter.Size = New System.Drawing.Size(38, 38)
        Me.btnExecuter.TabIndex = 1
        Me.btnExecuter.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblFin)
        Me.GroupBox1.Controls.Add(Me.txtFin)
        Me.GroupBox1.Controls.Add(Me.txtDebut)
        Me.GroupBox1.Controls.Add(Me.lblDebut)
        Me.GroupBox1.Controls.Add(Me.lblMotdePasse)
        Me.GroupBox1.Controls.Add(Me.txtMotDePasse)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(220, 103)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'lblFin
        '
        Me.lblFin.AutoSize = True
        Me.lblFin.Location = New System.Drawing.Point(6, 74)
        Me.lblFin.Name = "lblFin"
        Me.lblFin.Size = New System.Drawing.Size(21, 13)
        Me.lblFin.TabIndex = 4
        Me.lblFin.Text = "Fin"
        '
        'txtFin
        '
        Me.txtFin.Location = New System.Drawing.Point(89, 71)
        Me.txtFin.Name = "txtFin"
        Me.txtFin.Size = New System.Drawing.Size(112, 20)
        Me.txtFin.TabIndex = 5
        '
        'txtDebut
        '
        Me.txtDebut.Location = New System.Drawing.Point(89, 45)
        Me.txtDebut.Name = "txtDebut"
        Me.txtDebut.Size = New System.Drawing.Size(112, 20)
        Me.txtDebut.TabIndex = 3
        '
        'lblDebut
        '
        Me.lblDebut.AutoSize = True
        Me.lblDebut.Location = New System.Drawing.Point(6, 48)
        Me.lblDebut.Name = "lblDebut"
        Me.lblDebut.Size = New System.Drawing.Size(36, 13)
        Me.lblDebut.TabIndex = 2
        Me.lblDebut.Text = "Debut"
        '
        'lblMotdePasse
        '
        Me.lblMotdePasse.AutoSize = True
        Me.lblMotdePasse.Location = New System.Drawing.Point(6, 22)
        Me.lblMotdePasse.Name = "lblMotdePasse"
        Me.lblMotdePasse.Size = New System.Drawing.Size(77, 13)
        Me.lblMotdePasse.TabIndex = 0
        Me.lblMotdePasse.Text = "Mot de passe :"
        '
        'txtMotDePasse
        '
        Me.txtMotDePasse.Location = New System.Drawing.Point(89, 19)
        Me.txtMotDePasse.Name = "txtMotDePasse"
        Me.txtMotDePasse.Size = New System.Drawing.Size(112, 20)
        Me.txtMotDePasse.TabIndex = 1
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackgroundImage = Global.SosMedecins.SmartRapport.GestionApplication.My.Resources.Resources.Annuler
        Me.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnClose.Location = New System.Drawing.Point(196, 125)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(38, 38)
        Me.btnClose.TabIndex = 2
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'frmDepannageEchange
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(246, 175)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnExecuter)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmDepannageEchange"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Depannage Echange MCC"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnExecuter As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblFin As System.Windows.Forms.Label
    Friend WithEvents txtFin As System.Windows.Forms.TextBox
    Friend WithEvents txtDebut As System.Windows.Forms.TextBox
    Friend WithEvents lblDebut As System.Windows.Forms.Label
    Friend WithEvents lblMotdePasse As System.Windows.Forms.Label
    Friend WithEvents txtMotDePasse As System.Windows.Forms.TextBox
End Class
