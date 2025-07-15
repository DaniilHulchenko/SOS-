Public Class GestionFichier

    Private _prg As System.Windows.Forms.ProgressBar
    Public Property Progress() As System.Windows.Forms.ProgressBar
        Get
            Return _prg
        End Get
        Set(ByVal value As System.Windows.Forms.ProgressBar)
            _prg = value
        End Set
    End Property

    Public Sub DeplaceFichierRepertoire(ByVal p_strCheminSource As String, ByVal p_strCheminDestination As String)
        Dim z_strFichiers() As String = System.IO.Directory.GetFiles(p_strCheminSource)
        Dim z_strInfoFichier As System.IO.FileInfo

        _prg.Minimum = 0
        _prg.Maximum = z_strFichiers.Length
        _prg.Value = 0

        For Each z_strFichier As String In z_strFichiers
            z_strInfoFichier = New System.IO.FileInfo(z_strFichier)
            System.IO.File.Copy(z_strFichier, p_strCheminDestination & "\" & z_strInfoFichier.Name, True)
            System.IO.File.Delete(z_strFichier)
            _prg.Value += 1
        Next
    End Sub
End Class
