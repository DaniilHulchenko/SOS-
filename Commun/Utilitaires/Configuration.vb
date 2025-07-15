Imports System
Imports System.Xml
Imports System.Reflection
Imports System.Windows.Forms

Public Class Configuration

    Private Shared _strNomFichier As String

    Public Sub New()
        _strNomFichier = Application.StartupPath + "\" + Assembly.GetEntryAssembly.GetName.Name + ".exe.config"
    End Sub

    Public Sub New(ByVal p_strNom As String)
        _strNomFichier = Application.StartupPath + "\" + p_strNom
    End Sub

    Public Shared Function GetValue(ByVal p_strElement As String) As String
        Dim z_xmlDoc As XmlDocument
        Dim z_xmlNoeud As XmlNodeList

        z_xmlDoc = New XmlDocument
        z_xmlDoc.Load(_strNomFichier)

        z_xmlNoeud = z_xmlDoc.SelectNodes("/configuration/appSettings")
        For Each z_xml As XmlNode In z_xmlNoeud.Item(0)
            If z_xml.Name = "add" Then
                If z_xml.Attributes("key").Value = p_strElement Then
                    Return z_xml.Attributes("value").Value
                End If
            End If
        Next

        Return Nothing
    End Function

    Public Shared Function SetValue(ByVal p_strElement As String, ByVal p_strValeur As String, Optional ByVal p_blnCreation As Boolean = True) As Boolean
        Dim z_xmlDoc As XmlDocument
        Dim z_xmlNoeud As XmlNodeList
        Dim z_xmlNoeudTouve As XmlNode = Nothing
        Dim z_xmlNoeudCree As XmlElement

        z_xmlDoc = New XmlDocument
        z_xmlDoc.Load(_strNomFichier)

        z_xmlNoeud = z_xmlDoc.SelectNodes("/configuration/appSettings")
        For Each z_xml As XmlNode In z_xmlNoeud.Item(0)
            If z_xml.Name = "add" Then
                If z_xml.Attributes("key").Value = p_strElement Then
                    z_xmlNoeudTouve = z_xml
                    Exit For
                End If
            End If
        Next

        If z_xmlNoeudTouve IsNot Nothing Then
            z_xmlNoeudTouve.Attributes("value").Value = p_strValeur
            z_xmlDoc.Save(_strNomFichier)
            Return True
        Else
            If p_blnCreation Then
                z_xmlDoc.SelectNodes("/configuration/appSettings")
                z_xmlNoeudCree = z_xmlDoc.CreateElement("add")
                z_xmlNoeudCree.SetAttribute("key", p_strElement)
                z_xmlNoeudCree.SetAttribute("value", p_strValeur)
                z_xmlNoeud(0).AppendChild(z_xmlNoeudCree)
                z_xmlDoc.Save(_strNomFichier)
                Return True
            Else
                Return False
            End If
        End If
    End Function
End Class
