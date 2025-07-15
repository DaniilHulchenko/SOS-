<ToolboxBitmapAttribute(GetType(MaskedTextBox))> _
Public Class sosDateBox
    Inherits MaskedTextBox

    Public Property Value() As String
        Get
            If (Me.Text.Replace(".", "").Replace("/", "").Trim.Length > 0) Then
                Return Me.Text
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            If value = Nothing Then
                Me.Text = ""
            Else
                Me.Text = value.ToString
            End If
        End Set
    End Property

    Public ReadOnly Property IsValid() As Boolean
        Get
            If (IsDate(Me.Value) OrElse Me.Value.Length = 0) Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

    Public ReadOnly Property ValeurDate() As Date
        Get
            If (IsDate(Me.Value)) Then
                Return CDate(Me.Value)
            Else
                Return Nothing
            End If
        End Get
    End Property

    Public Sub New()
        Me.Mask = "00/00/0000"
        Me.ValidatingType = GetType(Date)
    End Sub
End Class
