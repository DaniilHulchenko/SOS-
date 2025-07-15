<ToolboxBitmapAttribute(GetType(ErrorProvider))> _
Public Class uErrorProvider
    Inherits ErrorProvider

    Private _strMessageErreur As String = String.Empty
    <System.ComponentModel.Browsable(False)> _
    Public Property MessageErreur() As String
        Get
            Return _strMessageErreur
        End Get
        Set(ByVal value As String)
            _strMessageErreur = value
        End Set
    End Property

    Public Sub GenereErreur(ByVal p_ctr As Control, ByVal p_strErreur As String, Optional ByVal p_blnIntegreMessage As Boolean = True)
        If (_strMessageErreur = String.Empty) Then
            p_ctr.Focus()
            If (p_ctr.GetType.BaseType Is GetType(TextBox)) Then
                Dim z_obj As TextBox = CType(p_ctr, TextBox)

                z_obj.SelectAll()
            End If
        End If
        ' on affiche l'erreur à l'ecran
        SetError(p_ctr, p_strErreur)
        If (p_blnIntegreMessage) Then
            _strMessageErreur &= p_strErreur & vbCrLf
        End If
    End Sub

    Public Overloads Sub Clear()
        MyBase.Clear()
        _strMessageErreur = String.Empty
    End Sub


End Class

