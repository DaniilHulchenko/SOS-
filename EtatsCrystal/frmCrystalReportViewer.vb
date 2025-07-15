Public Class frmCrystalReportViewer

#Region " Bouton Export "
    <Category("Sos Medecin"), _
    DefaultValue(True), _
    Description("Affiche ou cache le bouton d'export")> _
    Property AfficheBoutonExport() As Boolean
        Get
            Return crvPrincipal.AfficheBoutonExport
        End Get
        Set(ByVal value As Boolean)
            crvPrincipal.AfficheBoutonExport = value
        End Set
    End Property

    <Category("Sos Medecin"), _
    DefaultValue(False), _
    Description("Active ou désactive le bouton d'export")> _
    Property EnabledBoutonExport() As Boolean
        Get
            Return crvPrincipal.EnabledBoutonExport
        End Get
        Set(ByVal value As Boolean)
            crvPrincipal.EnabledBoutonExport = value
        End Set
    End Property
#End Region

#Region " Bouton Print "
    <Category("Sos Medecin"), _
    DefaultValue(True), _
    Description("Affiche ou cache le bouton d'impression")> _
    Property AfficheBoutonPrint() As Boolean
        Get
            Return crvPrincipal.AfficheBoutonPrint
        End Get
        Set(ByVal value As Boolean)
            crvPrincipal.AfficheBoutonPrint = value
        End Set
    End Property

    <Category("Sos Medecin"), _
    DefaultValue(False), _
    Description("Active ou désactive le bouton d'impression")> _
    Property EnabledBoutonPrint() As Boolean
        Get
            Return crvPrincipal.EnabledBoutonPrint
        End Get
        Set(ByVal value As Boolean)
            crvPrincipal.EnabledBoutonPrint = value
        End Set
    End Property

    <Category("Sos Medecin"), _
    DefaultValue(True), _
    Description("Affiche ou cache la fenetre de parametrage avant l'impression")> _
    Property AfficheParametrageImprimante() As Boolean
        Get
            Return crvPrincipal.AfficheParametrageImprimante
        End Get
        Set(ByVal value As Boolean)
            crvPrincipal.AfficheParametrageImprimante = value
        End Set
    End Property

    Private _PrinterName As String = ""
    <System.ComponentModel.Browsable(False), _
    Category("Sos Medecin"), _
    DefaultValue(GetType(String), ""), _
    Description("Nom de l'imprimante par default")> _
    Property PrinterName() As String
        Get
            Return crvPrincipal.PrinterName
        End Get
        Set(ByVal value As String)
            crvPrincipal.PrinterName = value
        End Set
    End Property
#End Region

#Region " Bouton Param Imprimante "
    <Category("Sos Medecin"), _
    DefaultValue(True), _
    Description("Affiche ou cache le bouton de paramétrage d'imprimante")> _
    Property AfficheBoutonParamImprimante() As Boolean
        Get
            Return crvPrincipal.AfficheBoutonParamImprimante
        End Get
        Set(ByVal value As Boolean)
            crvPrincipal.AfficheBoutonParamImprimante = value
        End Set
    End Property

    <Category("Sos Medecin"), _
    DefaultValue(False), _
    Description("Active ou désactive le bouton de paramétrage d'imprimante")> _
    Property EnabledBoutonParamImprimante() As Boolean
        Get
            Return crvPrincipal.EnabledBoutonParamImprimante
        End Get
        Set(ByVal value As Boolean)
            crvPrincipal.EnabledBoutonParamImprimante = value
        End Set
    End Property
#End Region

#Region " Bouton Refresh "
    <Category("Sos Medecin"), _
    DefaultValue(True), _
    Description("Affiche ou cache le bouton Refresh")> _
    Property AfficheBoutonRefresh() As Boolean
        Get
            Return crvPrincipal.AfficheBoutonRefresh
        End Get
        Set(ByVal value As Boolean)
            crvPrincipal.AfficheBoutonRefresh = value
        End Set
    End Property

    <Category("Sos Medecin"), _
    DefaultValue(False), _
    Description("Active ou désactive le bouton Refresh")> _
    Property EnabledBoutonRefresh() As Boolean
        Get
            Return crvPrincipal.EnabledBoutonRefresh
        End Get
        Set(ByVal value As Boolean)
            crvPrincipal.EnabledBoutonRefresh = value
        End Set
    End Property
#End Region

#Region " Bouton Explorer "
    <Category("Sos Medecin"), _
    DefaultValue(True), _
    Description("Affiche ou cache le bouton d'exploration de groupe")> _
    Property AfficheBoutonExplorer() As Boolean
        Get
            Return crvPrincipal.AfficheBoutonExplorer
        End Get
        Set(ByVal value As Boolean)
            crvPrincipal.AfficheBoutonExplorer = value
        End Set
    End Property

    <Category("Sos Medecin"), _
    DefaultValue(False), _
    Description("Active ou désactive le bouton d'exploration de groupe")> _
    Property EnabledBoutonExplorer() As Boolean
        Get
            Return crvPrincipal.EnabledBoutonExplorer
        End Get
        Set(ByVal value As Boolean)
            crvPrincipal.EnabledBoutonExplorer = value
        End Set
    End Property
#End Region

#Region " Boutons navigation "
    <Category("Sos Medecin"), _
    DefaultValue(True), _
    Description("Affiche ou cache les boutons de navigation")> _
    Property AfficheNavigation() As Boolean
        Get
            Return crvPrincipal.AfficheNavigation
        End Get
        Set(ByVal value As Boolean)
            crvPrincipal.AfficheNavigation = value
        End Set
    End Property

    <Category("Sos Medecin"), _
    DefaultValue(False), _
    Description("Active ou désactive les boutons de navigation")> _
    Property EnabledNavigation() As Boolean
        Get
            Return crvPrincipal.EnabledNavigation
        End Get
        Set(ByVal value As Boolean)
            crvPrincipal.EnabledNavigation = value
        End Set
    End Property
#End Region

#Region " Bouton recherche "
    <Category("Sos Medecin"), _
    DefaultValue(True), _
    Description("Affiche ou cache le bouton de recherche")> _
    Property AfficheBoutonRecherche() As Boolean
        Get
            Return crvPrincipal.AfficheBoutonRecherche
        End Get
        Set(ByVal value As Boolean)
            crvPrincipal.AfficheBoutonRecherche = value
        End Set
    End Property

    <Category("Sos Medecin"), _
    DefaultValue(False), _
    Description("Active ou désactive le bouton de recherche")> _
    Property EnabledBoutonRecherche() As Boolean
        Get
            Return crvPrincipal.EnabledBoutonRecherche
        End Get
        Set(ByVal value As Boolean)
            crvPrincipal.EnabledBoutonRecherche = value
        End Set
    End Property
#End Region

#Region " Bouton Zoom "
    <Category("Sos Medecin"), _
    DefaultValue(True), _
    Description("Affiche ou cache le bouton de zoom")> _
    Property AfficheBoutonZoom() As Boolean
        Get
            Return crvPrincipal.AfficheBoutonZoom
        End Get
        Set(ByVal value As Boolean)
            crvPrincipal.AfficheBoutonZoom = value
        End Set
    End Property

    <Category("Sos Medecin"), _
    DefaultValue(False), _
    Description("Active ou désactive le bouton de zoom")> _
    Property EnabledBoutonZoom() As Boolean
        Get
            Return crvPrincipal.EnabledBoutonZoom
        End Get
        Set(ByVal value As Boolean)
            crvPrincipal.EnabledBoutonZoom = value
        End Set
    End Property
#End Region

#Region " Bouton Close "
    <Category("Sos Medecin"), _
    DefaultValue(False), _
    Description("Affiche ou cache le bouton Close")> _
    Property AfficheBoutonClose() As Boolean
        Get
            Return crvPrincipal.AfficheBoutonClose
        End Get
        Set(ByVal value As Boolean)
            crvPrincipal.AfficheBoutonClose = value
        End Set
    End Property
#End Region

    Private _ReportSource As CrystalDecisions.CrystalReports.Engine.ReportDocument = Nothing
    Property ReportSource() As CrystalDecisions.CrystalReports.Engine.ReportDocument
        Get
            Return crvPrincipal.ReportSource
        End Get
        Set(ByVal value As CrystalDecisions.CrystalReports.Engine.ReportDocument)
            crvPrincipal.ReportSource = value
        End Set
    End Property
End Class