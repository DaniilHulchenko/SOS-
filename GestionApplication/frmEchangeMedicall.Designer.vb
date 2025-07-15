<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEchangeMedicall
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEchangeMedicall))
        Me.prgTaitement = New System.Windows.Forms.ProgressBar
        Me.grpTraitement = New System.Windows.Forms.GroupBox
        Me.prgDetail = New System.Windows.Forms.ProgressBar
        Me.lblTraitement = New System.Windows.Forms.Label
        Me.bgwTraitement = New System.ComponentModel.BackgroundWorker
        Me.ttp = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnDepannage = New System.Windows.Forms.Button
        Me.btnQuitter = New System.Windows.Forms.Button
        Me.btnExecute = New System.Windows.Forms.Button
        Me.grpTraitement.SuspendLayout()
        Me.SuspendLayout()
        '
        'prgTaitement
        '
        Me.prgTaitement.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.prgTaitement.Location = New System.Drawing.Point(6, 55)
        Me.prgTaitement.Name = "prgTaitement"
        Me.prgTaitement.Size = New System.Drawing.Size(468, 15)
        Me.prgTaitement.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.prgTaitement.TabIndex = 2
        '
        'grpTraitement
        '
        Me.grpTraitement.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpTraitement.Controls.Add(Me.prgDetail)
        Me.grpTraitement.Controls.Add(Me.lblTraitement)
        Me.grpTraitement.Controls.Add(Me.prgTaitement)
        Me.grpTraitement.Location = New System.Drawing.Point(12, 12)
        Me.grpTraitement.Name = "grpTraitement"
        Me.grpTraitement.Size = New System.Drawing.Size(480, 99)
        Me.grpTraitement.TabIndex = 3
        Me.grpTraitement.TabStop = False
        Me.grpTraitement.Text = "Traitement"
        '
        'prgDetail
        '
        Me.prgDetail.Location = New System.Drawing.Point(6, 78)
        Me.prgDetail.Name = "prgDetail"
        Me.prgDetail.Size = New System.Drawing.Size(468, 15)
        Me.prgDetail.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.prgDetail.TabIndex = 4
        '
        'lblTraitement
        '
        Me.lblTraitement.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTraitement.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTraitement.Location = New System.Drawing.Point(6, 27)
        Me.lblTraitement.Name = "lblTraitement"
        Me.lblTraitement.Size = New System.Drawing.Size(468, 25)
        Me.lblTraitement.TabIndex = 3
        Me.lblTraitement.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'bgwTraitement
        '
        '
        'btnDepannage
        '
        Me.btnDepannage.Image = Global.SosMedecins.SmartRapport.GestionApplication.My.Resources.Resources.Depannage
        Me.btnDepannage.Location = New System.Drawing.Point(12, 117)
        Me.btnDepannage.Name = "btnDepannage"
        Me.btnDepannage.Size = New System.Drawing.Size(38, 38)
        Me.btnDepannage.TabIndex = 4
        Me.btnDepannage.UseVisualStyleBackColor = True
        '
        'btnQuitter
        '
        Me.btnQuitter.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnQuitter.BackgroundImage = Global.SosMedecins.SmartRapport.GestionApplication.My.Resources.Resources.Annuler
        Me.btnQuitter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnQuitter.Location = New System.Drawing.Point(412, 117)
        Me.btnQuitter.Name = "btnQuitter"
        Me.btnQuitter.Size = New System.Drawing.Size(80, 38)
        Me.btnQuitter.TabIndex = 1
        Me.ttp.SetToolTip(Me.btnQuitter, "Fermer la fenêtre")
        Me.btnQuitter.UseVisualStyleBackColor = True
        '
        'btnExecute
        '
        Me.btnExecute.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExecute.Image = Global.SosMedecins.SmartRapport.GestionApplication.My.Resources.Resources.Actualiser
        Me.btnExecute.Location = New System.Drawing.Point(312, 117)
        Me.btnExecute.Name = "btnExecute"
        Me.btnExecute.Size = New System.Drawing.Size(94, 38)
        Me.btnExecute.TabIndex = 0
        Me.ttp.SetToolTip(Me.btnExecute, "Lancer l'échange")
        Me.btnExecute.UseVisualStyleBackColor = True
        '
        'frmEchangeMedicall
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(504, 163)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnDepannage)
        Me.Controls.Add(Me.grpTraitement)
        Me.Controls.Add(Me.btnQuitter)
        Me.Controls.Add(Me.btnExecute)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmEchangeMedicall"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Echange Avec Medicall"
        Me.grpTraitement.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnExecute As System.Windows.Forms.Button
    Friend WithEvents btnQuitter As System.Windows.Forms.Button
    Friend WithEvents prgTaitement As System.Windows.Forms.ProgressBar
    Friend WithEvents grpTraitement As System.Windows.Forms.GroupBox
    Friend WithEvents lblTraitement As System.Windows.Forms.Label
    Friend WithEvents bgwTraitement As System.ComponentModel.BackgroundWorker
    Friend WithEvents ttp As System.Windows.Forms.ToolTip
    Friend WithEvents prgDetail As System.Windows.Forms.ProgressBar
    Friend WithEvents btnDepannage As System.Windows.Forms.Button
End Class
