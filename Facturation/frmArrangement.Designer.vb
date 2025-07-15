<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmArrangement
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmArrangement))
        Me.ttpBouton = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnSupprimer = New System.Windows.Forms.Button()
        Me.btnValider = New System.Windows.Forms.Button()
        Me.btnAnnuler = New System.Windows.Forms.Button()
        Me.grpPrincipal = New System.Windows.Forms.GroupBox()
        Me.txtCommentaire = New System.Windows.Forms.TextBox()
        Me.txtUser = New System.Windows.Forms.TextBox()
        Me.txtDate = New System.Windows.Forms.TextBox()
        Me.lblCommentaire = New System.Windows.Forms.Label()
        Me.lblPar = New System.Windows.Forms.Label()
        Me.lblLe = New System.Windows.Forms.Label()
        Me.grpPrincipal.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnSupprimer
        '
        Me.btnSupprimer.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSupprimer.Image = CType(resources.GetObject("btnSupprimer.Image"), System.Drawing.Image)
        Me.btnSupprimer.Location = New System.Drawing.Point(12, 167)
        Me.btnSupprimer.Name = "btnSupprimer"
        Me.btnSupprimer.Size = New System.Drawing.Size(34, 34)
        Me.btnSupprimer.TabIndex = 6
        Me.ttpBouton.SetToolTip(Me.btnSupprimer, "Supprimer")
        Me.btnSupprimer.UseVisualStyleBackColor = True
        '
        'btnValider
        '
        Me.btnValider.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnValider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnValider.Image = Global.SosMedecins.SmartRapport.Facturation.My.Resources.Resources.Valider
        Me.btnValider.Location = New System.Drawing.Point(210, 167)
        Me.btnValider.Name = "btnValider"
        Me.btnValider.Size = New System.Drawing.Size(34, 34)
        Me.btnValider.TabIndex = 5
        Me.ttpBouton.SetToolTip(Me.btnValider, "Valider")
        Me.btnValider.UseVisualStyleBackColor = True
        '
        'btnAnnuler
        '
        Me.btnAnnuler.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAnnuler.BackgroundImage = Global.SosMedecins.SmartRapport.Facturation.My.Resources.Resources.Fermer
        Me.btnAnnuler.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnAnnuler.Location = New System.Drawing.Point(250, 167)
        Me.btnAnnuler.Name = "btnAnnuler"
        Me.btnAnnuler.Size = New System.Drawing.Size(34, 34)
        Me.btnAnnuler.TabIndex = 4
        Me.ttpBouton.SetToolTip(Me.btnAnnuler, "Annuler")
        Me.btnAnnuler.UseVisualStyleBackColor = True
        '
        'grpPrincipal
        '
        Me.grpPrincipal.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpPrincipal.Controls.Add(Me.txtCommentaire)
        Me.grpPrincipal.Controls.Add(Me.txtUser)
        Me.grpPrincipal.Controls.Add(Me.txtDate)
        Me.grpPrincipal.Controls.Add(Me.lblCommentaire)
        Me.grpPrincipal.Controls.Add(Me.lblPar)
        Me.grpPrincipal.Controls.Add(Me.lblLe)
        Me.grpPrincipal.Location = New System.Drawing.Point(12, 12)
        Me.grpPrincipal.Name = "grpPrincipal"
        Me.grpPrincipal.Size = New System.Drawing.Size(272, 146)
        Me.grpPrincipal.TabIndex = 7
        Me.grpPrincipal.TabStop = False
        '
        'txtCommentaire
        '
        Me.txtCommentaire.Location = New System.Drawing.Point(79, 71)
        Me.txtCommentaire.Multiline = True
        Me.txtCommentaire.Name = "txtCommentaire"
        Me.txtCommentaire.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtCommentaire.Size = New System.Drawing.Size(183, 69)
        Me.txtCommentaire.TabIndex = 5
        '
        'txtUser
        '
        Me.txtUser.Location = New System.Drawing.Point(79, 45)
        Me.txtUser.Name = "txtUser"
        Me.txtUser.ReadOnly = True
        Me.txtUser.Size = New System.Drawing.Size(183, 20)
        Me.txtUser.TabIndex = 4
        '
        'txtDate
        '
        Me.txtDate.Location = New System.Drawing.Point(79, 19)
        Me.txtDate.Name = "txtDate"
        Me.txtDate.ReadOnly = True
        Me.txtDate.Size = New System.Drawing.Size(183, 20)
        Me.txtDate.TabIndex = 3
        '
        'lblCommentaire
        '
        Me.lblCommentaire.AutoSize = True
        Me.lblCommentaire.Location = New System.Drawing.Point(6, 74)
        Me.lblCommentaire.Name = "lblCommentaire"
        Me.lblCommentaire.Size = New System.Drawing.Size(67, 13)
        Me.lblCommentaire.TabIndex = 2
        Me.lblCommentaire.Text = "Arrangement"
        '
        'lblPar
        '
        Me.lblPar.AutoSize = True
        Me.lblPar.Location = New System.Drawing.Point(6, 48)
        Me.lblPar.Name = "lblPar"
        Me.lblPar.Size = New System.Drawing.Size(23, 13)
        Me.lblPar.TabIndex = 1
        Me.lblPar.Text = "Par"
        '
        'lblLe
        '
        Me.lblLe.AutoSize = True
        Me.lblLe.Location = New System.Drawing.Point(6, 22)
        Me.lblLe.Name = "lblLe"
        Me.lblLe.Size = New System.Drawing.Size(19, 13)
        Me.lblLe.TabIndex = 0
        Me.lblLe.Text = "Le"
        '
        'frmArrangement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.CadetBlue
        Me.ClientSize = New System.Drawing.Size(296, 213)
        Me.Controls.Add(Me.grpPrincipal)
        Me.Controls.Add(Me.btnSupprimer)
        Me.Controls.Add(Me.btnValider)
        Me.Controls.Add(Me.btnAnnuler)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmArrangement"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "frmArrangement"
        Me.grpPrincipal.ResumeLayout(False)
        Me.grpPrincipal.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents ttpBouton As System.Windows.Forms.ToolTip
    Private WithEvents grpPrincipal As System.Windows.Forms.GroupBox
    Private WithEvents txtCommentaire As System.Windows.Forms.TextBox
    Private WithEvents txtUser As System.Windows.Forms.TextBox
    Private WithEvents txtDate As System.Windows.Forms.TextBox
    Private WithEvents lblCommentaire As System.Windows.Forms.Label
    Private WithEvents lblPar As System.Windows.Forms.Label
    Private WithEvents lblLe As System.Windows.Forms.Label
    Private WithEvents btnSupprimer As System.Windows.Forms.Button
    Private WithEvents btnValider As System.Windows.Forms.Button
    Private WithEvents btnAnnuler As System.Windows.Forms.Button
End Class
