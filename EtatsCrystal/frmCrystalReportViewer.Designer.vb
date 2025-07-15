<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCrystalReportViewer
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCrystalReportViewer))
        Me.crvPrincipal = New SosMedecins.SmartRapport.EtatsCrystal.CrystalReportViewer()
        Me.SuspendLayout()
        '
        'crvPrincipal
        '
        Me.crvPrincipal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.crvPrincipal.Location = New System.Drawing.Point(0, 0)
        Me.crvPrincipal.Name = "crvPrincipal"
        'Me.crvPrincipal.PrinterName = "\\SQL-DATA\Canon2-standard"
        Me.crvPrincipal.ReportSource = Nothing        
        Me.crvPrincipal.Size = New System.Drawing.Size(747, 769)
        Me.crvPrincipal.TabIndex = 0
        '
        'frmCrystalReportViewer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(747, 769)
        Me.Controls.Add(Me.crvPrincipal)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCrystalReportViewer"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Rapport Crystal"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents crvPrincipal As SosMedecins.SmartRapport.EtatsCrystal.CrystalReportViewer
End Class
