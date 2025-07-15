<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmParamImprimante
    Inherits Genilink.Controles.gFRMModal

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
        Me.gcbxImprimantes = New Genilink.Controles.gComboBox
        Me.glblImprimante = New Genilink.Controles.gLabel
        Me.gcmdOk = New Genilink.Controles.gButton
        Me.gcmdAnnuler = New Genilink.Controles.gButton
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
        Me.gcmdOk.Location = New System.Drawing.Point(117, 128)
        Me.gcmdOk.Name = "gcmdOk"
        Me.gcmdOk.Size = New System.Drawing.Size(75, 23)
        Me.gcmdOk.TabIndex = 3
        Me.gcmdOk.Text = "Ok"
        Me.gcmdOk.UseVisualStyleBackColor = True
        '
        'gcmdAnnuler
        '
        Me.gcmdAnnuler.Location = New System.Drawing.Point(210, 128)
        Me.gcmdAnnuler.Name = "gcmdAnnuler"
        Me.gcmdAnnuler.Size = New System.Drawing.Size(75, 23)
        Me.gcmdAnnuler.TabIndex = 4
        Me.gcmdAnnuler.Text = "Annuler"
        Me.gcmdAnnuler.UseVisualStyleBackColor = True
        '
        'frmParamImprimante
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(300, 161)
        Me.Controls.Add(Me.gcmdAnnuler)
        Me.Controls.Add(Me.gcmdOk)
        Me.Controls.Add(Me.glblImprimante)
        Me.Controls.Add(Me.gcbxImprimantes)
        Me.Name = "frmParamImprimante"
        Me.Text = "Paramétrage de l'imprimante"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents gcbxImprimantes As Genilink.Controles.gComboBox
    Friend WithEvents glblImprimante As Genilink.Controles.gLabel
    Friend WithEvents gcmdOk As Genilink.Controles.gButton
    Friend WithEvents gcmdAnnuler As Genilink.Controles.gButton

End Class
