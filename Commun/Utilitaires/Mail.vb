Public Class Mail

#Region " Property "
    Private _mail As System.Net.Mail.MailMessage = Nothing

    Public Property Sujet() As String
        Get
            Return _mail.Subject
        End Get
        Set(ByVal value As String)
            _mail.Subject = value
        End Set
    End Property

    Public Property Message() As String
        Get
            Return _mail.Body
        End Get
        Set(ByVal value As String)
            _mail.Body = value
        End Set
    End Property

    Public Sub AjouteDestinataire(ByVal p_strAdresseMail As String)
        _mail.To.Add(p_strAdresseMail)
    End Sub

    Public Sub AjouteDestinataireCc(ByVal p_strAdresseMailCC As String)
        _mail.CC.Add(p_strAdresseMailCC)
    End Sub

    Public Sub EnleveDestinataire(ByVal p_strAdresseMail As String)
        _mail.To.Remove(New System.Net.Mail.MailAddress(p_strAdresseMail))
    End Sub

    Public Sub EnleveTousDestinataire()
        _mail.To.Clear()
    End Sub

    Public Sub JoindrePiece(ByVal p_strCheminPiece As String)
        _mail.Attachments.Add(New System.Net.Mail.Attachment(p_strCheminPiece))
    End Sub
#End Region

    Public Sub New()
        _mail = New System.Net.Mail.MailMessage()
        _mail.From = New System.Net.Mail.MailAddress("smartrapports@sos-medecins.ch")
    End Sub

    Public Sub New(ByVal p_strAdresseDepart As String)
        _mail = New System.Net.Mail.MailMessage()
        _mail.From = New System.Net.Mail.MailAddress(p_strAdresseDepart)
    End Sub

    Public Function Envoi() As Boolean
        If (_mail Is Nothing) Then
            Return False
        End If

        _mail.IsBodyHtml = False
        ' Ouvrir la Connexion 
        Dim z_mailClient As New System.Net.Mail.SmtpClient()
        z_mailClient.Host = "mail.sos-medecins.ch"
        z_mailClient.Credentials = New System.Net.NetworkCredential("smartrapports@sos-medecins.ch", "Med%31416")
        z_mailClient.Send(_mail)

        Return True
    End Function

    Public Sub Show()
        Dim z_frm As New frmMail(_mail.Subject, _mail.Body, _mail.To)

        If (z_frm.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            Message = z_frm.Message
            Sujet = z_frm.Sujet

            EnleveTousDestinataire()
            Dim z_strAdresses As String() = z_frm.Destinataire.Split(";".ToCharArray())
            For Each z_str As String In z_strAdresses
                AjouteDestinataire(z_str)
            Next

            If (Envoi()) Then
                Windows.Forms.MessageBox.Show("Envoi par mail réussi.", "Résultat", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
            Else
                Windows.Forms.MessageBox.Show("Echec lors de l'envoi du mail.", "Résultat", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
            End If
        End If
    End Sub


End Class
