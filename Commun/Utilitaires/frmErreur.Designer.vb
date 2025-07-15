<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmErreur
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmErreur))
        Me.txtErreur = New System.Windows.Forms.TextBox
        Me.btnFermer = New System.Windows.Forms.Button
        Me.btnMail = New System.Windows.Forms.Button
        Me.pctSignal = New System.Windows.Forms.PictureBox
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.iltImages = New System.Windows.Forms.ImageList(Me.components)
        Me.txtCommentaire = New System.Windows.Forms.TextBox
        Me.lblCommentaire = New System.Windows.Forms.Label
        CType(Me.pctSignal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtErreur
        '
        Me.txtErreur.BackColor = System.Drawing.Color.FloralWhite
        Me.txtErreur.Location = New System.Drawing.Point(68, 12)
        Me.txtErreur.Multiline = True
        Me.txtErreur.Name = "txtErreur"
        Me.txtErreur.ReadOnly = True
        Me.txtErreur.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtErreur.Size = New System.Drawing.Size(376, 125)
        Me.txtErreur.TabIndex = 0
        Me.txtErreur.TabStop = False
        '
        'btnFermer
        '
        Me.btnFermer.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnFermer.BackgroundImage = CType(resources.GetObject("btnFermer.BackgroundImage"), System.Drawing.Image)
        Me.btnFermer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnFermer.Location = New System.Drawing.Point(382, 238)
        Me.btnFermer.Name = "btnFermer"
        Me.btnFermer.Size = New System.Drawing.Size(62, 46)
        Me.btnFermer.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.btnFermer, "Fermer")
        Me.btnFermer.UseVisualStyleBackColor = True
        '
        'btnMail
        '
        Me.btnMail.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnMail.BackgroundImage = CType(resources.GetObject("btnMail.BackgroundImage"), System.Drawing.Image)
        Me.btnMail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnMail.Location = New System.Drawing.Point(314, 238)
        Me.btnMail.Name = "btnMail"
        Me.btnMail.Size = New System.Drawing.Size(62, 46)
        Me.btnMail.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.btnMail, "Envoyer un Email")
        Me.btnMail.UseVisualStyleBackColor = True
        '
        'pctSignal
        '
        Me.pctSignal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.pctSignal.Location = New System.Drawing.Point(12, 15)
        Me.pctSignal.Name = "pctSignal"
        Me.pctSignal.Size = New System.Drawing.Size(48, 121)
        Me.pctSignal.TabIndex = 3
        Me.pctSignal.TabStop = False
        '
        'iltImages
        '
        Me.iltImages.ImageStream = CType(resources.GetObject("iltImages.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.iltImages.TransparentColor = System.Drawing.Color.Transparent
        Me.iltImages.Images.SetKeyName(0, "Erreur.png")
        Me.iltImages.Images.SetKeyName(1, "Information.png")
        '
        'txtCommentaire
        '
        Me.txtCommentaire.Location = New System.Drawing.Point(12, 169)
        Me.txtCommentaire.Multiline = True
        Me.txtCommentaire.Name = "txtCommentaire"
        Me.txtCommentaire.Size = New System.Drawing.Size(432, 63)
        Me.txtCommentaire.TabIndex = 4
        '
        'lblCommentaire
        '
        Me.lblCommentaire.AutoSize = True
        Me.lblCommentaire.Location = New System.Drawing.Point(12, 153)
        Me.lblCommentaire.Name = "lblCommentaire"
        Me.lblCommentaire.Size = New System.Drawing.Size(73, 13)
        Me.lblCommentaire.TabIndex = 5
        Me.lblCommentaire.Text = "Commentaires"
        '
        'frmErreur
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(456, 295)
        Me.Controls.Add(Me.lblCommentaire)
        Me.Controls.Add(Me.txtCommentaire)
        Me.Controls.Add(Me.pctSignal)
        Me.Controls.Add(Me.btnMail)
        Me.Controls.Add(Me.btnFermer)
        Me.Controls.Add(Me.txtErreur)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmErreur"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Erreur d'application"
        CType(Me.pctSignal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtErreur As System.Windows.Forms.TextBox
    Friend WithEvents btnFermer As System.Windows.Forms.Button
    Friend WithEvents btnMail As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents pctSignal As System.Windows.Forms.PictureBox
    Friend WithEvents iltImages As System.Windows.Forms.ImageList
    Friend WithEvents txtCommentaire As System.Windows.Forms.TextBox
    Friend WithEvents lblCommentaire As System.Windows.Forms.Label
End Class
