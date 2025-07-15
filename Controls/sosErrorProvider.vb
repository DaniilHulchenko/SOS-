<ToolboxBitmapAttribute(GetType(ErrorProvider))> _
Public Class sosErrorProvider
    Inherits ErrorProvider

    Private _MessageErreur As String = String.Empty
    <System.ComponentModel.Browsable(False)> _
    Public Property MessageErreur() As String
        Get
            Return _MessageErreur
        End Get
        Set(ByVal value As String)
            _MessageErreur = value
        End Set
    End Property

    Public ReadOnly Property IsValid() As Boolean
        Get
            If _MessageErreur.Length > 0 Then
                Return False
            Else
                Return True
            End If
        End Get
    End Property

    Public Sub GenereErreur(ByVal p_ctr As Control, ByVal p_strErreur As String, Optional ByVal p_blnIntegreMessage As Boolean = True)
        If (_MessageErreur = String.Empty) Then
            p_ctr.Focus()
            If (p_ctr.GetType.BaseType Is GetType(TextBox)) Then
                Dim z_obj As TextBox = CType(p_ctr, TextBox)

                z_obj.SelectAll()
            End If
        End If
        ' on affiche l'erreur à l'ecran
        SetError(p_ctr, p_strErreur)
        If (p_blnIntegreMessage) Then
            _MessageErreur &= p_strErreur & vbCrLf
        End If
    End Sub

    Public Overloads Sub Clear()
        MyBase.Clear()
        _MessageErreur = String.Empty
    End Sub

    Public Sub New()
        Me.BlinkStyle = ErrorBlinkStyle.NeverBlink
    End Sub
End Class
