<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CrystalReportViewer
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CrystalReportViewer))
        Me.tspNavigation = New System.Windows.Forms.ToolStrip()
        Me.tsbExport = New System.Windows.Forms.ToolStripButton()
        Me.tss1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbRefresh = New System.Windows.Forms.ToolStripButton()
        Me.tss2 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbExplorer = New System.Windows.Forms.ToolStripButton()
        Me.tss3 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbFirstPage = New System.Windows.Forms.ToolStripButton()
        Me.tsbPreviousPage = New System.Windows.Forms.ToolStripButton()
        Me.tstPage = New System.Windows.Forms.ToolStripTextBox()
        Me.tsbNextPage = New System.Windows.Forms.ToolStripButton()
        Me.tsbLastPage = New System.Windows.Forms.ToolStripButton()
        Me.tss4 = New System.Windows.Forms.ToolStripSeparator()
        Me.tstRecherche = New System.Windows.Forms.ToolStripTextBox()
        Me.tsbRecherche = New System.Windows.Forms.ToolStripButton()
        Me.tsbZoom = New System.Windows.Forms.ToolStripSplitButton()
        Me.tsmLargueurDePage = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmTouteLaPage = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsm200 = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsm150 = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsm100 = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsm50 = New System.Windows.Forms.ToolStripMenuItem()
        Me.tss5 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbPrint = New System.Windows.Forms.ToolStripButton()
        Me.tss6 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbParamImprimante = New System.Windows.Forms.ToolStripButton()
        Me.tslImprimante = New System.Windows.Forms.ToolStripLabel()
        Me.tsbClose = New System.Windows.Forms.ToolStripButton()
        Me.crvReport = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.tspNavigation.SuspendLayout()
        Me.SuspendLayout()
        '
        'tspNavigation
        '
        Me.tspNavigation.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tspNavigation.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbExport, Me.tss1, Me.tsbRefresh, Me.tss2, Me.tsbExplorer, Me.tss3, Me.tsbFirstPage, Me.tsbPreviousPage, Me.tstPage, Me.tsbNextPage, Me.tsbLastPage, Me.tss4, Me.tstRecherche, Me.tsbRecherche, Me.tsbZoom, Me.tss5, Me.tsbPrint, Me.tss6, Me.tsbParamImprimante, Me.tslImprimante, Me.tsbClose, Me.ToolStripButton1})
        Me.tspNavigation.Location = New System.Drawing.Point(0, 0)
        Me.tspNavigation.Name = "tspNavigation"
        Me.tspNavigation.Size = New System.Drawing.Size(662, 25)
        Me.tspNavigation.TabIndex = 0
        Me.tspNavigation.Text = "ToolStrip1"
        '
        'tsbExport
        '
        Me.tsbExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbExport.Enabled = False
        Me.tsbExport.Image = CType(resources.GetObject("tsbExport.Image"), System.Drawing.Image)
        Me.tsbExport.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbExport.Name = "tsbExport"
        Me.tsbExport.Size = New System.Drawing.Size(23, 22)
        Me.tsbExport.Text = "Exporter"
        Me.tsbExport.ToolTipText = "Exporter le rapport"
        '
        'tss1
        '
        Me.tss1.Name = "tss1"
        Me.tss1.Size = New System.Drawing.Size(6, 25)
        '
        'tsbRefresh
        '
        Me.tsbRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbRefresh.Enabled = False
        Me.tsbRefresh.Image = CType(resources.GetObject("tsbRefresh.Image"), System.Drawing.Image)
        Me.tsbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbRefresh.Name = "tsbRefresh"
        Me.tsbRefresh.Size = New System.Drawing.Size(23, 22)
        Me.tsbRefresh.Text = "ToolStripButton3"
        Me.tsbRefresh.ToolTipText = "Réafficher le rapport"
        '
        'tss2
        '
        Me.tss2.Name = "tss2"
        Me.tss2.Size = New System.Drawing.Size(6, 25)
        '
        'tsbExplorer
        '
        Me.tsbExplorer.Checked = True
        Me.tsbExplorer.CheckOnClick = True
        Me.tsbExplorer.CheckState = System.Windows.Forms.CheckState.Checked
        Me.tsbExplorer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbExplorer.Enabled = False
        Me.tsbExplorer.Image = CType(resources.GetObject("tsbExplorer.Image"), System.Drawing.Image)
        Me.tsbExplorer.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbExplorer.Name = "tsbExplorer"
        Me.tsbExplorer.Size = New System.Drawing.Size(23, 22)
        Me.tsbExplorer.Text = "Activer / Désactiver l'arborescence de groupe"
        '
        'tss3
        '
        Me.tss3.Name = "tss3"
        Me.tss3.Size = New System.Drawing.Size(6, 25)
        '
        'tsbFirstPage
        '
        Me.tsbFirstPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbFirstPage.Enabled = False
        Me.tsbFirstPage.Image = CType(resources.GetObject("tsbFirstPage.Image"), System.Drawing.Image)
        Me.tsbFirstPage.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbFirstPage.Name = "tsbFirstPage"
        Me.tsbFirstPage.Size = New System.Drawing.Size(23, 22)
        Me.tsbFirstPage.Text = "Aller à la première page"
        '
        'tsbPreviousPage
        '
        Me.tsbPreviousPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbPreviousPage.Enabled = False
        Me.tsbPreviousPage.Image = CType(resources.GetObject("tsbPreviousPage.Image"), System.Drawing.Image)
        Me.tsbPreviousPage.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbPreviousPage.Name = "tsbPreviousPage"
        Me.tsbPreviousPage.Size = New System.Drawing.Size(23, 22)
        Me.tsbPreviousPage.Text = "Aller à la page précédente"
        '
        'tstPage
        '
        Me.tstPage.Enabled = False
        Me.tstPage.Name = "tstPage"
        Me.tstPage.Size = New System.Drawing.Size(40, 25)
        '
        'tsbNextPage
        '
        Me.tsbNextPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbNextPage.Enabled = False
        Me.tsbNextPage.Image = CType(resources.GetObject("tsbNextPage.Image"), System.Drawing.Image)
        Me.tsbNextPage.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbNextPage.Name = "tsbNextPage"
        Me.tsbNextPage.Size = New System.Drawing.Size(23, 22)
        Me.tsbNextPage.Text = "Aller à la page suivante"
        '
        'tsbLastPage
        '
        Me.tsbLastPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbLastPage.Enabled = False
        Me.tsbLastPage.Image = CType(resources.GetObject("tsbLastPage.Image"), System.Drawing.Image)
        Me.tsbLastPage.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbLastPage.Name = "tsbLastPage"
        Me.tsbLastPage.Size = New System.Drawing.Size(23, 22)
        Me.tsbLastPage.Text = "Aller à la dernière page"
        '
        'tss4
        '
        Me.tss4.Name = "tss4"
        Me.tss4.Size = New System.Drawing.Size(6, 25)
        '
        'tstRecherche
        '
        Me.tstRecherche.Enabled = False
        Me.tstRecherche.Name = "tstRecherche"
        Me.tstRecherche.Size = New System.Drawing.Size(100, 25)
        '
        'tsbRecherche
        '
        Me.tsbRecherche.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbRecherche.Enabled = False
        Me.tsbRecherche.Image = CType(resources.GetObject("tsbRecherche.Image"), System.Drawing.Image)
        Me.tsbRecherche.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbRecherche.Name = "tsbRecherche"
        Me.tsbRecherche.Size = New System.Drawing.Size(23, 22)
        Me.tsbRecherche.Text = "Rechercher le texte"
        '
        'tsbZoom
        '
        Me.tsbZoom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbZoom.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmLargueurDePage, Me.tsmTouteLaPage, Me.tsm200, Me.tsm150, Me.tsm100, Me.tsm50})
        Me.tsbZoom.Enabled = False
        Me.tsbZoom.Image = CType(resources.GetObject("tsbZoom.Image"), System.Drawing.Image)
        Me.tsbZoom.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbZoom.Name = "tsbZoom"
        Me.tsbZoom.Size = New System.Drawing.Size(32, 22)
        Me.tsbZoom.Text = "Zoom"
        '
        'tsmLargueurDePage
        '
        Me.tsmLargueurDePage.Name = "tsmLargueurDePage"
        Me.tsmLargueurDePage.Size = New System.Drawing.Size(166, 22)
        Me.tsmLargueurDePage.Text = "Largueur de page"
        '
        'tsmTouteLaPage
        '
        Me.tsmTouteLaPage.Name = "tsmTouteLaPage"
        Me.tsmTouteLaPage.Size = New System.Drawing.Size(166, 22)
        Me.tsmTouteLaPage.Text = "Toute la page"
        '
        'tsm200
        '
        Me.tsm200.Name = "tsm200"
        Me.tsm200.Size = New System.Drawing.Size(166, 22)
        Me.tsm200.Text = "200 %"
        '
        'tsm150
        '
        Me.tsm150.Name = "tsm150"
        Me.tsm150.Size = New System.Drawing.Size(166, 22)
        Me.tsm150.Text = "150 %"
        '
        'tsm100
        '
        Me.tsm100.Name = "tsm100"
        Me.tsm100.Size = New System.Drawing.Size(166, 22)
        Me.tsm100.Text = "100 %"
        '
        'tsm50
        '
        Me.tsm50.Name = "tsm50"
        Me.tsm50.Size = New System.Drawing.Size(166, 22)
        Me.tsm50.Text = "50 %"
        '
        'tss5
        '
        Me.tss5.Name = "tss5"
        Me.tss5.Size = New System.Drawing.Size(6, 25)
        '
        'tsbPrint
        '
        Me.tsbPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbPrint.Enabled = False
        Me.tsbPrint.Image = CType(resources.GetObject("tsbPrint.Image"), System.Drawing.Image)
        Me.tsbPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbPrint.Name = "tsbPrint"
        Me.tsbPrint.Size = New System.Drawing.Size(23, 22)
        Me.tsbPrint.Text = "Imprimer"
        Me.tsbPrint.ToolTipText = "Imprimer le rapport"
        '
        'tss6
        '
        Me.tss6.Name = "tss6"
        Me.tss6.Size = New System.Drawing.Size(6, 25)
        '
        'tsbParamImprimante
        '
        Me.tsbParamImprimante.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbParamImprimante.Enabled = False
        Me.tsbParamImprimante.Image = CType(resources.GetObject("tsbParamImprimante.Image"), System.Drawing.Image)
        Me.tsbParamImprimante.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbParamImprimante.Name = "tsbParamImprimante"
        Me.tsbParamImprimante.Size = New System.Drawing.Size(23, 22)
        Me.tsbParamImprimante.Text = "ToolStripButton1"
        Me.tsbParamImprimante.ToolTipText = "Paramétrer l'imprimante"
        '
        'tslImprimante
        '
        Me.tslImprimante.Name = "tslImprimante"
        Me.tslImprimante.Size = New System.Drawing.Size(69, 22)
        Me.tslImprimante.Text = "Imprimante"
        '
        'tsbClose
        '
        Me.tsbClose.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.tsbClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbClose.Image = CType(resources.GetObject("tsbClose.Image"), System.Drawing.Image)
        Me.tsbClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbClose.Name = "tsbClose"
        Me.tsbClose.Size = New System.Drawing.Size(23, 22)
        Me.tsbClose.Text = "Fermer"
        Me.tsbClose.Visible = False
        '
        'crvReport
        '
        Me.crvReport.ActiveViewIndex = -1
        Me.crvReport.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.crvReport.Cursor = System.Windows.Forms.Cursors.Default
        Me.crvReport.DisplayToolbar = False
        Me.crvReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.crvReport.Location = New System.Drawing.Point(0, 25)
        Me.crvReport.Name = "crvReport"
        Me.crvReport.Size = New System.Drawing.Size(662, 417)
        Me.crvReport.TabIndex = 1
        Me.crvReport.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        Me.crvReport.ToolPanelWidth = 100
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton1.Text = "ToolStripButton1"
        '
        'CrystalReportViewer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.crvReport)
        Me.Controls.Add(Me.tspNavigation)
        Me.Name = "CrystalReportViewer"
        Me.Size = New System.Drawing.Size(662, 442)
        Me.tspNavigation.ResumeLayout(False)
        Me.tspNavigation.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tsbExport As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents tss2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbExplorer As System.Windows.Forms.ToolStripButton
    Friend WithEvents tss3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbFirstPage As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbPreviousPage As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbNextPage As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbLastPage As System.Windows.Forms.ToolStripButton
    Friend WithEvents tss4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tstPage As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents tsbRecherche As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbZoom As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents tss5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsm200 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsm100 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmLargueurDePage As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmTouteLaPage As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsm150 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsm50 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsbParamImprimante As System.Windows.Forms.ToolStripButton
    Friend WithEvents tss1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tss6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tslImprimante As System.Windows.Forms.ToolStripLabel
    Friend WithEvents tsbClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents crvReport As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents tspNavigation As System.Windows.Forms.ToolStrip
    Friend WithEvents tstRecherche As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton

End Class
