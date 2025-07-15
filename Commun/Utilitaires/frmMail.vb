Public Class frmMail

    Public ReadOnly Property Sujet() As String
        Get
            Return txtSujet.Text
        End Get
    End Property

    Public ReadOnly Property Message() As String
        Get
            Return txtObjet.Text
        End Get
    End Property

    Public ReadOnly Property Destinataire() As String
        Get
            Return txtMail.Text
        End Get
    End Property

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
    End Sub

    Public Sub New(ByVal p_strSujet As String, ByVal p_strMessage As String, ByVal p_objDestinataire As System.Net.Mail.MailAddressCollection)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        For Each z_objNomDest As System.Net.Mail.MailAddress In p_objDestinataire
            If (txtMail.Text.Length <= 0) Then
                txtMail.Text = z_objNomDest.Address
            Else
                txtMail.Text = ";" + z_objNomDest.Address
            End If
        Next

        txtSujet.Text = p_strSujet
        txtObjet.Text = p_strMessage
    End Sub

    Private Sub btnAnnuler_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnnuler.Click
        Me.Close()
    End Sub

    Private Sub btnValider_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnValider.Click
        DialogResult = Windows.Forms.DialogResult.OK
    End Sub
End Class