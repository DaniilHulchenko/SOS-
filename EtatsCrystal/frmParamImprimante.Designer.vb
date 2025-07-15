<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmParamImprimante
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmParamImprimante))
        Me.gcbxImprimantes = New System.Windows.Forms.ComboBox
        Me.glblImprimante = New System.Windows.Forms.Label
        Me.gcmdOk = New System.Windows.Forms.Button
        Me.gcmdAnnuler = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'gcbxImprimantes
        '
        Me.gcbxImprimantes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.gcbxImprimantes.FormattingEnabled = True
        Me.gcbxImprimantes.Location = New System.Drawing.Point(76, 12)
        Me.gcbxImprimantes.Name = "gcbxImprimantes"
        Me.gcbxImprimantes.Size = New System.Drawing.Size(209, 21)
        Me.gcbxImprimantes.TabIndex = 1
        '
        'glblImprimante
        '
        Me.glblImprimante.AutoSize = True
        Me.glblImprimante.Location = New System.Drawing.Point(12, 15)
        Me.glblImprimante.Name = "glblImprimante"
        Me.glblImprimante.Size = New System.Drawing.Size(58, 13)
        Me.glblImprimante.TabIndex = 2
        Me.glblImprimante.Text = "Imprimante"
        '
        'gcmdOk
        '
        Me.gcmdOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gcmdOk.Location = New System.Drawing.Point(129, 50)
        Me.gcmdOk.Name = "gcmdOk"
        Me.gcmdOk.Size = New System.Drawing.Size(75, 23)
        Me.gcmdOk.TabIndex = 3
        Me.gcmdOk.Text = "Ok"
        Me.gcmdOk.UseVisualStyleBackColor = True
        '
        'gcmdAnnuler
        '
        Me.gcmdAnnuler.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gcmdAnnuler.Location = New System.Drawing.Point(210, 50)
        Me.gcmdAnnuler.Name = "gcmdAnnuler"
        Me.gcmdAnnuler.Size = New System.Drawing.Size(75, 23)
        Me.gcmdAnnuler.TabIndex = 4
        Me.gcmdAnnuler.Text = "Annuler"
        Me.gcmdAnnuler.UseVisualStyleBackColor = True
        '
        'frmParamImprimante
        '
        Me.ClientSize = New System.Drawing.Size(300, 84)
        Me.Controls.Add(Me.gcmdAnnuler)
        Me.Controls.Add(Me.gcmdOk)
        Me.Controls.Add(Me.glblImprimante)
        Me.Controls.Add(Me.gcbxImprimantes)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmParamImprimante"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Paramétrage de l'imprimante"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents gcbxImprimantes As System.Windows.Forms.ComboBox
    Friend WithEvents glblImprimante As System.Windows.Forms.Label
    Friend WithEvents gcmdOk As System.Windows.Forms.Button
    Friend WithEvents gcmdAnnuler As System.Windows.Forms.Button

End Class
