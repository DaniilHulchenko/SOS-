<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmParamImpression
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmParamImpression))
        Me.btnImprimer = New System.Windows.Forms.Button
        Me.btnAnnuler = New System.Windows.Forms.Button
        Me.lblCopie = New System.Windows.Forms.Label
        Me.lblPage = New System.Windows.Forms.Label
        Me.rbnToute = New System.Windows.Forms.RadioButton
        Me.rbnSelection = New System.Windows.Forms.RadioButton
        Me.grpPage = New System.Windows.Forms.GroupBox
        Me.lblA = New System.Windows.Forms.Label
        Me.nudA = New System.Windows.Forms.NumericUpDown
        Me.nudDe = New System.Windows.Forms.NumericUpDown
        Me.nudCopie = New System.Windows.Forms.NumericUpDown
        Me.grpCopie = New System.Windows.Forms.GroupBox
        Me.grpPage.SuspendLayout()
        CType(Me.nudA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudDe, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudCopie, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCopie.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnImprimer
        '
        Me.btnImprimer.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnImprimer.Location = New System.Drawing.Point(104, 189)
        Me.btnImprimer.Name = "btnImprimer"
        Me.btnImprimer.Size = New System.Drawing.Size(75, 23)
        Me.btnImprimer.TabIndex = 0
        Me.btnImprimer.Text = "Imprimer"
        Me.btnImprimer.UseVisualStyleBackColor = True
        '
        'btnAnnuler
        '
        Me.btnAnnuler.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnAnnuler.Location = New System.Drawing.Point(185, 189)
        Me.btnAnnuler.Name = "btnAnnuler"
        Me.btnAnnuler.Size = New System.Drawing.Size(75, 23)
        Me.btnAnnuler.TabIndex = 1
        Me.btnAnnuler.Text = "Annuler"
        Me.btnAnnuler.UseVisualStyleBackColor = True
        '
        'lblCopie
        '
        Me.lblCopie.AutoSize = True
        Me.lblCopie.Location = New System.Drawing.Point(6, 21)
        Me.lblCopie.Name = "lblCopie"
        Me.lblCopie.Size = New System.Drawing.Size(94, 13)
        Me.lblCopie.TabIndex = 2
        Me.lblCopie.Text = "Nombre de copie :"
        '
        'lblPage
        '
        Me.lblPage.AutoSize = True
        Me.lblPage.Enabled = False
        Me.lblPage.Location = New System.Drawing.Point(67, 80)
        Me.lblPage.Name = "lblPage"
        Me.lblPage.Size = New System.Drawing.Size(32, 13)
        Me.lblPage.TabIndex = 3
        Me.lblPage.Text = "Page"
        '
        'rbnToute
        '
        Me.rbnToute.AutoSize = True
        Me.rbnToute.Checked = True
        Me.rbnToute.Location = New System.Drawing.Point(9, 25)
        Me.rbnToute.Name = "rbnToute"
        Me.rbnToute.Size = New System.Drawing.Size(106, 17)
        Me.rbnToute.TabIndex = 4
        Me.rbnToute.TabStop = True
        Me.rbnToute.Text = "Toutes les pages"
        Me.rbnToute.UseVisualStyleBackColor = True
        '
        'rbnSelection
        '
        Me.rbnSelection.AutoSize = True
        Me.rbnSelection.Location = New System.Drawing.Point(9, 48)
        Me.rbnSelection.Name = "rbnSelection"
        Me.rbnSelection.Size = New System.Drawing.Size(106, 17)
        Me.rbnSelection.TabIndex = 5
        Me.rbnSelection.Text = "Suivant séléction"
        Me.rbnSelection.UseVisualStyleBackColor = True
        '
        'grpPage
        '
        Me.grpPage.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpPage.Controls.Add(Me.lblA)
        Me.grpPage.Controls.Add(Me.nudA)
        Me.grpPage.Controls.Add(Me.nudDe)
        Me.grpPage.Controls.Add(Me.rbnToute)
        Me.grpPage.Controls.Add(Me.lblPage)
        Me.grpPage.Controls.Add(Me.rbnSelection)
        Me.grpPage.Location = New System.Drawing.Point(13, 70)
        Me.grpPage.Name = "grpPage"
        Me.grpPage.Size = New System.Drawing.Size(247, 108)
        Me.grpPage.TabIndex = 6
        Me.grpPage.TabStop = False
        Me.grpPage.Text = "Etendue de page"
        '
        'lblA
        '
        Me.lblA.AutoSize = True
        Me.lblA.Enabled = False
        Me.lblA.Location = New System.Drawing.Point(163, 80)
        Me.lblA.Name = "lblA"
        Me.lblA.Size = New System.Drawing.Size(13, 13)
        Me.lblA.TabIndex = 8
        Me.lblA.Text = "à"
        '
        'nudA
        '
        Me.nudA.Enabled = False
        Me.nudA.Location = New System.Drawing.Point(182, 78)
        Me.nudA.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudA.Name = "nudA"
        Me.nudA.Size = New System.Drawing.Size(52, 20)
        Me.nudA.TabIndex = 7
        Me.nudA.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'nudDe
        '
        Me.nudDe.Enabled = False
        Me.nudDe.Location = New System.Drawing.Point(105, 78)
        Me.nudDe.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudDe.Name = "nudDe"
        Me.nudDe.Size = New System.Drawing.Size(52, 20)
        Me.nudDe.TabIndex = 6
        Me.nudDe.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'nudCopie
        '
        Me.nudCopie.Location = New System.Drawing.Point(106, 19)
        Me.nudCopie.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.nudCopie.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudCopie.Name = "nudCopie"
        Me.nudCopie.Size = New System.Drawing.Size(46, 20)
        Me.nudCopie.TabIndex = 7
        Me.nudCopie.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'grpCopie
        '
        Me.grpCopie.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpCopie.Controls.Add(Me.lblCopie)
        Me.grpCopie.Controls.Add(Me.nudCopie)
        Me.grpCopie.Location = New System.Drawing.Point(12, 12)
        Me.grpCopie.Name = "grpCopie"
        Me.grpCopie.Size = New System.Drawing.Size(248, 52)
        Me.grpCopie.TabIndex = 8
        Me.grpCopie.TabStop = False
        '
        'frmParamImpression
        '
        Me.AcceptButton = Me.btnImprimer
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnAnnuler
        Me.ClientSize = New System.Drawing.Size(272, 224)
        Me.ControlBox = False
        Me.Controls.Add(Me.grpCopie)
        Me.Controls.Add(Me.grpPage)
        Me.Controls.Add(Me.btnAnnuler)
        Me.Controls.Add(Me.btnImprimer)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmParamImpression"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Paramétrage"
        Me.grpPage.ResumeLayout(False)
        Me.grpPage.PerformLayout()
        CType(Me.nudA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudDe, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudCopie, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCopie.ResumeLayout(False)
        Me.grpCopie.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnImprimer As System.Windows.Forms.Button
    Friend WithEvents btnAnnuler As System.Windows.Forms.Button
    Friend WithEvents lblCopie As System.Windows.Forms.Label
    Friend WithEvents lblPage As System.Windows.Forms.Label
    Friend WithEvents rbnToute As System.Windows.Forms.RadioButton
    Friend WithEvents rbnSelection As System.Windows.Forms.RadioButton
    Friend WithEvents grpPage As System.Windows.Forms.GroupBox
    Friend WithEvents lblA As System.Windows.Forms.Label
    Friend WithEvents nudA As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudDe As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudCopie As System.Windows.Forms.NumericUpDown
    Friend WithEvents grpCopie As System.Windows.Forms.GroupBox
End Class
