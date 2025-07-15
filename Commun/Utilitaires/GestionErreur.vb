Imports System.Windows.Forms
Imports System.Threading

Public Class GestionErreur
    Inherits Exception


#Region " Declarations de variable " ' --------------------------------------------------------------------------
    Private _message As String

    Public Overrides ReadOnly Property Message() As String
        Get
            Return _message
        End Get
    End Property
#End Region

#Region " Constructeur " '-------------------------------------------------------------------------------------------
    Public Sub New()
        MyBase.New()

        _message = String.Format("le {0} à {1}" & vbCrLf, System.DateTime.Now.ToString("dddd dd MMMM yyyy"), System.DateTime.Now.ToString("HH:mm:ss"))
    End Sub

    Public Sub New(ByVal p_Message As String)
        MyBase.New()

        _message = String.Format("le {0} à {1} par {2}" & vbCrLf & vbCrLf & "{3}", System.DateTime.Now.ToString("dddd dd MMMM yyyy"), System.DateTime.Now.ToString("HH:mm:ss"), Environment.UserName, p_Message)
    End Sub

    Public Sub New(ByVal p_Message As String, ByVal e As Exception)
        MyBase.New()

        _message = String.Format("{0}" & vbCrLf & vbCrLf & "{1}", System.DateTime.Now, p_Message)
        _message = _message & vbCrLf & e.Message.ToString
    End Sub
#End Region

    Public Sub Show()
        Show(frmErreur.Type.Erreur)
    End Sub

    Public Sub Show(ByVal p_enumType As frmErreur.Type)
        Dim z_frm As New frmErreur(_message)
        z_frm.Gestion = p_enumType
        z_frm.ShowDialog()
        z_frm.Dispose()
    End Sub

    Public Sub OnThreadException(ByVal sender As Object, ByVal t As ThreadExceptionEventArgs)
        Dim result As DialogResult = System.Windows.Forms.DialogResult.Cancel
        Try
            _message = String.Format("le {0} à {1} par {2}" & vbCrLf & vbCrLf & "{3}", System.DateTime.Now.ToString("dddd dd MMMM yyyy"), System.DateTime.Now.ToString("HH:mm:ss"), Environment.UserName, t.Exception.Message)
            _message &= vbCrLf & vbCrLf & "Trace : " & vbCrLf & vbCrLf & t.Exception.StackTrace

            Show()
        Catch
            Try
                MessageBox.Show("Fatal Error", "Fatal Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Stop)
            Finally
                Application.Exit()
            End Try
        End Try
    End Sub

End Class
