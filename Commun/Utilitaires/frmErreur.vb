Public Class frmErreur

    Public Enum Type
        Erreur
        Information
    End Enum

    Private _Type As Type = Type.Erreur
    Public Property Gestion() As Type
        Get
            Return _Type
        End Get
        Set(ByVal value As Type)
            Select Case value
                Case Type.Erreur
                    pctSignal.BackgroundImage = iltImages.Images(0)
                    Me.Text = "Erreur d'application"
                Case Type.Information
                    pctSignal.BackgroundImage = iltImages.Images(1)
                    Me.Text = "Information"
            End Select

            _Type = value
        End Set
    End Property

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Gestion = _Type
    End Sub

    Public Sub New(ByVal p_strMessage As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        txtErreur.Text = p_strMessage

        Gestion = _Type
    End Sub

    Private Sub btnFermer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFermer.Click
        Me.Close()
    End Sub

    Private Sub btnMail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMail.Click
        ' Ouvrir la Connexion 
        Dim z_mail As New Mail()
        'Dim z_objUtilitaire As New SosMedecins.Utilitaires.LectureXml()

        'z_mail.AjouteDestinataire(z_objUtilitaire.GetValue("Parametrage", "strMailAdresse"))
        z_mail.AjouteDestinataire("smartrapports@sos-medecins.ch")
        z_mail.Message = txtErreur.Text & vbCrLf & vbCrLf & txtCommentaire.Text
        z_mail.Sujet = Me.Text & " - " & System.Windows.Forms.Application.ProductName
        ' serveur SMTP
        z_mail.Envoi()
        Me.Close()
    End Sub
End Class