<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class uscGestion
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(uscGestion))
        Me.tspPrincipal = New System.Windows.Forms.ToolStrip
        Me.tsbEnregistrer = New System.Windows.Forms.ToolStripButton
        Me.tsbImprimer = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton3 = New System.Windows.Forms.ToolStripButton
        Me.grpArrangement = New System.Windows.Forms.GroupBox
        Me.lblArrangementPar = New System.Windows.Forms.Label
        Me.lblArrangementLe = New System.Windows.Forms.Label
        Me.txtArrangementUser = New System.Windows.Forms.TextBox
        Me.txtArrangementDate = New System.Windows.Forms.TextBox
        Me.txtArrangement = New System.Windows.Forms.TextBox
        Me.btnArrangement = New System.Windows.Forms.Button
        Me.tspPrincipal.SuspendLayout()
        Me.grpArrangement.SuspendLayout()
        Me.SuspendLayout()
        '
        'tspPrincipal
        '
        Me.tspPrincipal.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tspPrincipal.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tspPrincipal.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbEnregistrer, Me.tsbImprimer, Me.ToolStripButton3})
        Me.tspPrincipal.Location = New System.Drawing.Point(0, 0)
        Me.tspPrincipal.Name = "tspPrincipal"
        Me.tspPrincipal.Size = New System.Drawing.Size(1007, 39)
        Me.tspPrincipal.TabIndex = 1
        Me.tspPrincipal.Text = "ToolStrip1"
        '
        'tsbEnregistrer
        '
        Me.tsbEnregistrer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbEnregistrer.Image = Global.SosMedecins.SmartRapport.Facturation.My.Resources.Resources.Sauvegarde
        Me.tsbEnregistrer.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbEnregistrer.Name = "tsbEnregistrer"
        Me.tsbEnregistrer.Size = New System.Drawing.Size(36, 36)
        Me.tsbEnregistrer.Text = "ToolStripButton1"
        Me.tsbEnregistrer.ToolTipText = "Enregistrer la facture"
        '
        'tsbImprimer
        '
        Me.tsbImprimer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbImprimer.Image = Global.SosMedecins.SmartRapport.Facturation.My.Resources.Resources.Imprimer
        Me.tsbImprimer.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbImprimer.Name = "tsbImprimer"
        Me.tsbImprimer.Size = New System.Drawing.Size(36, 36)
        Me.tsbImprimer.Text = "Imprimer"
        Me.tsbImprimer.ToolTipText = "Imprimer la facture"
        '
        'ToolStripButton3
        '
        Me.ToolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton3.Image = CType(resources.GetObject("ToolStripButton3.Image"), System.Drawing.Image)
        Me.ToolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton3.Name = "ToolStripButton3"
        Me.ToolStripButton3.Size = New System.Drawing.Size(36, 36)
        Me.ToolStripButton3.Text = "ToolStripButton3"
        '
        'grpArrangement
        '
        Me.grpArrangement.Controls.Add(Me.lblArrangementPar)
        Me.grpArrangement.Controls.Add(Me.lblArrangementLe)
        Me.grpArrangement.Controls.Add(Me.txtArrangementUser)
        Me.grpArrangement.Controls.Add(Me.txtArrangementDate)
        Me.grpArrangement.Controls.Add(Me.txtArrangement)
        Me.grpArrangement.Controls.Add(Me.btnArrangement)
        Me.grpArrangement.Location = New System.Drawing.Point(0, 354)
        Me.grpArrangement.Name = "grpArrangement"
        Me.grpArrangement.Size = New System.Drawing.Size(920, 56)
        Me.grpArrangement.TabIndex = 65
        Me.grpArrangement.TabStop = False
        Me.grpArrangement.Text = "Arrangement"
        '
        'lblArrangementPar
        '
        Me.lblArrangementPar.AutoSize = True
        Me.lblArrangementPar.Location = New System.Drawing.Point(201, 22)
        Me.lblArrangementPar.Name = "lblArrangementPar"
        Me.lblArrangementPar.Size = New System.Drawing.Size(23, 13)
        Me.lblArrangementPar.TabIndex = 5
        Me.lblArrangementPar.Text = "Par"
        '
        'lblArrangementLe
        '
        Me.lblArrangementLe.AutoSize = True
        Me.lblArrangementLe.Location = New System.Drawing.Point(54, 22)
        Me.lblArrangementLe.Name = "lblArrangementLe"
        Me.lblArrangementLe.Size = New System.Drawing.Size(19, 13)
        Me.lblArrangementLe.TabIndex = 4
        Me.lblArrangementLe.Text = "Le"
        '
        'txtArrangementUser
        '
        Me.txtArrangementUser.Location = New System.Drawing.Point(230, 19)
        Me.txtArrangementUser.Name = "txtArrangementUser"
        Me.txtArrangementUser.ReadOnly = True
        Me.txtArrangementUser.Size = New System.Drawing.Size(119, 20)
        Me.txtArrangementUser.TabIndex = 3
        '
        'txtArrangementDate
        '
        Me.txtArrangementDate.Location = New System.Drawing.Point(80, 19)
        Me.txtArrangementDate.Name = "txtArrangementDate"
        Me.txtArrangementDate.ReadOnly = True
        Me.txtArrangementDate.Size = New System.Drawing.Size(103, 20)
        Me.txtArrangementDate.TabIndex = 2
        '
        'txtArrangement
        '
        Me.txtArrangement.Location = New System.Drawing.Point(355, 19)
        Me.txtArrangement.Multiline = True
        Me.txtArrangement.Name = "txtArrangement"
        Me.txtArrangement.ReadOnly = True
        Me.txtArrangement.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtArrangement.Size = New System.Drawing.Size(476, 30)
        Me.txtArrangement.TabIndex = 1
        '
        'btnArrangement
        '
        Me.btnArrangement.BackColor = System.Drawing.SystemColors.Control
        Me.btnArrangement.Enabled = False
        Me.btnArrangement.Location = New System.Drawing.Point(837, 19)
        Me.btnArrangement.Name = "btnArrangement"
        Me.btnArrangement.Size = New System.Drawing.Size(77, 31)
        Me.btnArrangement.TabIndex = 0
        Me.btnArrangement.Text = "Ajouter"
        Me.btnArrangement.UseVisualStyleBackColor = False
        '
        'uscGestion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.grpArrangement)
        Me.Controls.Add(Me.tspPrincipal)
        Me.Name = "uscGestion"
        Me.Size = New System.Drawing.Size(1007, 764)
        Me.tspPrincipal.ResumeLayout(False)
        Me.tspPrincipal.PerformLayout()
        Me.grpArrangement.ResumeLayout(False)
        Me.grpArrangement.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tspPrincipal As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbEnregistrer As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbImprimer As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton3 As System.Windows.Forms.ToolStripButton
    Private WithEvents grpArrangement As System.Windows.Forms.GroupBox
    Private WithEvents lblArrangementPar As System.Windows.Forms.Label
    Private WithEvents lblArrangementLe As System.Windows.Forms.Label
    Private WithEvents txtArrangementUser As System.Windows.Forms.TextBox
    Private WithEvents txtArrangementDate As System.Windows.Forms.TextBox
    Private WithEvents txtArrangement As System.Windows.Forms.TextBox
    Private WithEvents btnArrangement As System.Windows.Forms.Button

End Class
