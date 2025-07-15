Public Class FormatSql

#Region " Format de Données "         '-----------------------------------------------------------------------
    Public Shared Function Format_Date(ByVal p_strDate As String) As String
        If (p_strDate.Length > 0) Then
            Dim z_dte As Date = CType(p_strDate, Date)

            Return "'" & z_dte.ToString("yyyyMMdd") & "'"
        Else
            Return "NULL "
        End If
    End Function

    Public Shared Function Format_DateHeure(ByVal p_strDate As String) As String
        If (p_strDate.Length > 0) Then
            Dim z_dte As Date
            z_dte = CType(p_strDate, Date)

            Return "'" & z_dte.ToString("yyyyMMdd HH:mm:ss") & "'"
        Else
            Return "NULL "
        End If
    End Function

    '--- Retourne une chaine formaté horaire pour enregistrement SQL
    Public Shared Function Format_Heure(ByVal p_strDate As String) As String
        If (p_strDate.Length > 0) Then
            Dim z_dte As DateTime
            z_dte = CType(p_strDate, Date)

            Return "'" & z_dte.ToString("HH:mm") & "'"
        Else
            Return "NULL "
        End If
    End Function

    '--- Retourne une chaine formaté sans ' pour enregistrement SQL
    Public Shared Function Format_String(ByVal p_str As String) As String
        p_str = p_str.Replace("'", "''")

        Return "'" & p_str & "'"
    End Function

    Public Shared Function Format_Bit(ByVal p_str As String) As String
        If (p_str.Length > 0) Then
            Return p_str
        Else
            Return "0 "
        End If
    End Function

    Public Shared Function Format_BoolToBit(ByVal p_bln As Boolean) As Byte
        If p_bln = False Then
            Return 0
        Else
            Return 1
        End If
    End Function

    Public Shared Function Format_Nombre(ByVal p_str As String) As String
        If (p_str.Length > 0) Then
            Return p_str.Replace(",", ".")
        Else
            Return "NULL "
        End If
    End Function
#End Region

End Class
