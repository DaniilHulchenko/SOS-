Public NotInheritable Class frmSplash

    'TODO: This form can easily be set as the splash screen for the application by going to the "Application" tab
    '  of the Project Designer ("Properties" under the "Project" menu).
    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Dim f As New SosMedecins.Utilitaires.Fader(Me, 1800)
        f.TimeToWait = 2000

        AddHandler f.atEnd, AddressOf Lancement
        f.start()
    End Sub

    Private Sub Lancement(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.Hide()
        'Dim f As New Form1()
        'f.Show()
    End Sub

    Private Sub frmSplash_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Set up the dialog text at runtime according to the application's assembly information.  

        'TODO: Customize the application's assembly information in the "Application" pane of the project 
        '  properties dialog (under the "Project" menu).
        Me.Visible = True
        'Application title
        If My.Application.Info.Title <> "" Then
            lblApplication.Text = My.Application.Info.Title
        Else
            'If the application title is missing, use the application name, without the extension
            lblApplication.Text = System.IO.Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
        End If

        'Format the version information using the text set into the Version control at design time as the
        lblVersion.Text = System.String.Format("Version {0}.{1:00}", My.Application.Info.Version.Major, My.Application.Info.Version.Minor)
    End Sub

End Class
