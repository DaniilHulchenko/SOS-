<ToolboxBitmapAttribute(GetType(CrystalDecisions.Windows.Forms.CrystalReportViewer))> _
Public Class CrystalReportViewer

    Public Event Close()

#Region " Bouton Export "
    <Category("Sos Medecin"), _
    DefaultValue(True), _
    Description("Affiche ou cache le bouton d'export")> _
    Property AfficheBoutonExport() As Boolean
        Get
            Return tsbExport.Visible
        End Get
        Set(ByVal value As Boolean)
            tsbExport.Visible = value
        End Set
    End Property

    <Category("Sos Medecin"), _
    DefaultValue(False), _
    Description("Active ou désactive le bouton d'export")> _
    Property EnabledBoutonExport() As Boolean
        Get
            Return tsbExport.Enabled
        End Get
        Set(ByVal value As Boolean)
            tsbExport.Enabled = value
        End Set
    End Property

    Private Sub tsbExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbExport.Click
        crvReport.ExportReport()
    End Sub
#End Region

#Region " Bouton Print "
    <Category("Sos Medecin"), _
    DefaultValue(True), _
    Description("Affiche ou cache le bouton d'impression")> _
    Property AfficheBoutonPrint() As Boolean
        Get
            Return tsbPrint.Visible
        End Get
        Set(ByVal value As Boolean)
            tsbPrint.Visible = value
        End Set
    End Property

    <Category("Sos Medecin"), _
    DefaultValue(False), _
    Description("Active ou désactive le bouton d'impression")> _
    Property EnabledBoutonPrint() As Boolean
        Get
            Return tsbPrint.Enabled
        End Get
        Set(ByVal value As Boolean)
            tsbPrint.Enabled = value
        End Set
    End Property

    Private _AfficheParametrageImprimante As Boolean = True
    <Category("Sos Medecin"), _
    DefaultValue(True), _
    Description("Affiche ou cache la fenetre de parametrage avant l'impression")> _
    Property AfficheParametrageImprimante() As Boolean
        Get
            Return _AfficheParametrageImprimante
        End Get
        Set(ByVal value As Boolean)
            _AfficheParametrageImprimante = value
        End Set
    End Property

    Private Sub tsbPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbPrint.Click
        If (ReportSource IsNot Nothing) Then
            If (_AfficheParametrageImprimante) Then
                Dim z_frmParamImpression As New frmParamImpression
                Dim context As New CrystalDecisions.Shared.ReportPageRequestContext()
                z_frmParamImpression.NombreDePageMaximum = ReportSource.FormatEngine.GetLastPageNumber(context)

                If z_frmParamImpression.ShowDialog() = DialogResult.OK Then
                    Me.Cursor = Cursors.WaitCursor

                    ReportSource.PrintOptions.PrinterName = _PrinterName
                    ReportSource.PrintToPrinter(z_frmParamImpression.NombreCopie, False, z_frmParamImpression.PageDebut, z_frmParamImpression.PageFin)

                    Me.Cursor = Cursors.Default
                End If
                z_frmParamImpression.Dispose()
            Else
                ReportSource.PrintToPrinter(1, False, 0, 0)
            End If
        End If
    End Sub

    Private _PrinterName As String = ""
    <System.ComponentModel.Browsable(False), _
    Category("Sos Medecin"), _
    DefaultValue(GetType(String), ""), _
    Description("Nom de l'imprimante par default")> _
    Property PrinterName() As String
        Get
            Return _PrinterName
        End Get
        Set(ByVal value As String)
            _PrinterName = value
            If _PrinterName.Length > 0 Then
                tslImprimante.Text = value
            Else
                Dim z_prnDefault As New System.Drawing.Printing.PrinterSettings
                tslImprimante.Text = z_prnDefault.PrinterName
            End If
        End Set
    End Property
#End Region

#Region " Bouton Param Imprimante "
    <Category("Sos Medecin"), _
    DefaultValue(True), _
    Description("Affiche ou cache le bouton de paramétrage d'imprimante")> _
    Property AfficheBoutonParamImprimante() As Boolean
        Get
            Return tsbParamImprimante.Visible
        End Get
        Set(ByVal value As Boolean)
            tsbParamImprimante.Visible = value
        End Set
    End Property

    <Category("Sos Medecin"), _
    DefaultValue(False), _
    Description("Active ou désactive le bouton de paramétrage d'imprimante")> _
    Property EnabledBoutonParamImprimante() As Boolean
        Get
            Return tsbParamImprimante.Enabled
        End Get
        Set(ByVal value As Boolean)
            tsbParamImprimante.Enabled = value
        End Set
    End Property

    Private Sub tsbParamImprimante_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbParamImprimante.Click
        Dim z_frm As New frmParamImprimante
        z_frm.PrinterName = PrinterName
        z_frm.ShowDialog()
        If (z_frm.DialogResult = DialogResult.OK) Then
            PrinterName = z_frm.PrinterName
        End If
        z_frm.Dispose()
    End Sub
#End Region

#Region " Bouton Refresh "
    <Category("Sos Medecin"), _
    DefaultValue(True), _
    Description("Affiche ou cache le bouton Refresh")> _
    Property AfficheBoutonRefresh() As Boolean
        Get
            Return tsbRefresh.Visible
        End Get
        Set(ByVal value As Boolean)
            tsbRefresh.Visible = value
        End Set
    End Property

    <Category("Sos Medecin"), _
    DefaultValue(False), _
    Description("Active ou désactive le bouton Refresh")> _
    Property EnabledBoutonRefresh() As Boolean
        Get
            Return tsbRefresh.Enabled
        End Get
        Set(ByVal value As Boolean)
            tsbRefresh.Enabled = value
        End Set
    End Property

    Private Sub tsbRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbRefresh.Click
        crvReport.RefreshReport()
        tstPage.Text = crvReport.GetCurrentPageNumber.ToString
    End Sub
#End Region

#Region " Bouton Explorer "
    <Category("Sos Medecin"), _
    DefaultValue(True), _
    Description("Affiche ou cache le bouton d'exploration de groupe")> _
    Property AfficheBoutonExplorer() As Boolean
        Get
            Return tsbExplorer.Visible
        End Get
        Set(ByVal value As Boolean)
            tsbExplorer.Visible = value
        End Set
    End Property

    <Category("Sos Medecin"), _
    DefaultValue(False), _
    Description("Active ou désactive le bouton d'exploration de groupe")> _
    Property EnabledBoutonExplorer() As Boolean
        Get
            Return tsbExplorer.Enabled
        End Get
        Set(ByVal value As Boolean)
            tsbExplorer.Enabled = value
        End Set
    End Property
    <Obsolete>
    Private Sub tsbExplorer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbExplorer.Click
        If crvReport.DisplayGroupTree Then
            crvReport.DisplayGroupTree = False
        Else
            crvReport.DisplayGroupTree = True
        End If
    End Sub
#End Region

#Region " Boutons navigation "
    <Category("Sos Medecin"), _
    DefaultValue(True), _
    Description("Affiche ou cache les boutons de navigation")> _
    Property AfficheNavigation() As Boolean
        Get
            Return tsbFirstPage.Visible
        End Get
        Set(ByVal value As Boolean)
            tsbFirstPage.Visible = value
            tsbPreviousPage.Visible = value
            tsbNextPage.Visible = value
            tsbLastPage.Visible = value
            tstPage.Visible = value
        End Set
    End Property

    <Category("Sos Medecin"), _
    DefaultValue(False), _
    Description("Active ou désactive les boutons de navigation")> _
    Property EnabledNavigation() As Boolean
        Get
            Return tsbFirstPage.Enabled
        End Get
        Set(ByVal value As Boolean)
            tsbFirstPage.Enabled = value
            tsbPreviousPage.Enabled = value
            tsbNextPage.Enabled = value
            tsbLastPage.Enabled = value
            tstPage.Enabled = value
        End Set
    End Property

    Private Sub tsbFirstPage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbFirstPage.Click
        crvReport.ShowFirstPage()
        tstPage.Text = crvReport.GetCurrentPageNumber.ToString
    End Sub

    Private Sub tsbPreviousPage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbPreviousPage.Click
        crvReport.ShowPreviousPage()
        tstPage.Text = crvReport.GetCurrentPageNumber.ToString
    End Sub

    Private Sub tsbNextPage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbNextPage.Click
        crvReport.ShowNextPage()
        tstPage.Text = crvReport.GetCurrentPageNumber.ToString
    End Sub

    Private Sub tsbLastPage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbLastPage.Click
        crvReport.ShowLastPage()
        tstPage.Text = crvReport.GetCurrentPageNumber.ToString
    End Sub

    Private Sub tstPage_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tstPage.KeyPress
        If (Asc(e.KeyChar) = 13) Then
            If IsNumeric(tstPage.Text.Trim) Then
                crvReport.ShowNthPage(CType(tstPage.Text, Int32))
                tstPage.Text = crvReport.GetCurrentPageNumber.ToString
                tstPage.SelectionStart = 0
                tstPage.SelectionLength = tstPage.Text.Length
            End If
        End If
    End Sub
#End Region

#Region " Bouton recherche "
    <Category("Sos Medecin"), _
    DefaultValue(True), _
    Description("Affiche ou cache le bouton de recherche")> _
    Property AfficheBoutonRecherche() As Boolean
        Get
            Return tsbRecherche.Visible
        End Get
        Set(ByVal value As Boolean)
            tsbRecherche.Visible = value
            tstRecherche.Visible = value
        End Set
    End Property

    <Category("Sos Medecin"), _
    DefaultValue(False), _
    Description("Active ou désactive le bouton de recherche")> _
    Property EnabledBoutonRecherche() As Boolean
        Get
            Return tsbRecherche.Enabled
        End Get
        Set(ByVal value As Boolean)
            tsbRecherche.Enabled = value
            tstRecherche.Enabled = value
        End Set
    End Property

    Private Sub tsbRecherche_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbRecherche.Click
        If (tstRecherche.Text.Length > 0) Then
            crvReport.SearchForText(tstRecherche.Text)
        End If
    End Sub
#End Region

#Region " Bouton Zoom "
    <Category("Sos Medecin"), _
    DefaultValue(True), _
    Description("Affiche ou cache le bouton de zoom")> _
    Property AfficheBoutonZoom() As Boolean
        Get
            Return tsbZoom.Visible
        End Get
        Set(ByVal value As Boolean)
            tsbZoom.Visible = value
        End Set
    End Property

    <Category("Sos Medecin"), _
    DefaultValue(False), _
    Description("Active ou désactive le bouton de zoom")> _
    Property EnabledBoutonZoom() As Boolean
        Get
            Return tsbZoom.Enabled
        End Get
        Set(ByVal value As Boolean)
            tsbZoom.Enabled = value
        End Set
    End Property

    Private Sub tsbZoom_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbZoom.ButtonClick
        If Not (tsbZoom.IsOnDropDown) Then
            tsbZoom.ShowDropDown()
        End If
    End Sub

    Private Sub tsmLargueurDePage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmLargueurDePage.Click
        crvReport.Zoom(1)
    End Sub

    Private Sub tsmTouteLaPage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmTouteLaPage.Click
        crvReport.Zoom(2)
    End Sub

    Private Sub tsm200_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsm200.Click
        crvReport.Zoom(200)
    End Sub

    Private Sub tsm150_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsm150.Click
        crvReport.Zoom(150)
    End Sub

    Private Sub tsm100_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsm100.Click
        crvReport.Zoom(100)
    End Sub

    Private Sub tsm50_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsm50.Click
        crvReport.Zoom(50)
    End Sub
#End Region

#Region " Bouton Close "

    <Category("Sos Medecin"), _
    DefaultValue(False), _
    Description("Affiche ou cache le bouton Close")> _
    Property AfficheBoutonClose() As Boolean
        Get
            Return tsbClose.Visible
        End Get
        Set(ByVal value As Boolean)
            tsbClose.Visible = value
        End Set
    End Property

    Private Sub tsbClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbClose.Click
        RaiseEvent Close()
    End Sub
#End Region

    Private _ReportSource As CrystalDecisions.CrystalReports.Engine.ReportDocument = Nothing
    Property ReportSource() As CrystalDecisions.CrystalReports.Engine.ReportDocument
        Get
            Return CType(crvReport.ReportSource, CrystalDecisions.CrystalReports.Engine.ReportDocument)
        End Get
        Set(ByVal value As CrystalDecisions.CrystalReports.Engine.ReportDocument)
            If (value Is Nothing) Then
                EnabledBoutonExport = False
                EnabledBoutonPrint = False
                EnabledNavigation = False
                EnabledBoutonRefresh = False
                EnabledBoutonExplorer = False
                EnabledBoutonRecherche = False
                EnabledBoutonZoom = False
                EnabledBoutonParamImprimante = False
            Else
                EnabledBoutonExport = True
                EnabledBoutonPrint = True
                EnabledNavigation = True
                EnabledBoutonRefresh = True
                EnabledBoutonExplorer = True
                EnabledBoutonRecherche = True
                EnabledBoutonZoom = True
                EnabledBoutonParamImprimante = True
            End If
            crvReport.ReportSource = value
            tstPage.Text = "1"
            crvReport.Zoom(1)
        End Set
    End Property

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Dim z_prnDefault As New System.Drawing.Printing.PrinterSettings
        _PrinterName = z_prnDefault.PrinterName
        tslImprimante.Text = _PrinterName
    End Sub
End Class
