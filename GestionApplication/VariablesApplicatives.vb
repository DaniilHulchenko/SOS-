Imports System
Imports System.Collections.Generic
Imports System.Text
Public Class VariablesApplicatives
    Public Shared blnVersionDev As Boolean = False

    Public Class Utilisateurs
        Public Enum CodeDroits
            Secretaire = 1
            Medecin = 2
            Chef = 4
            Comptable = 5
            Admin = 10
        End Enum

        Public Shared Identifiant As String = ""
        Public Shared NomUtilisateur As String = ""
        Public Shared Droits As CodeDroits = CodeDroits.Comptable
        Public Shared Initiale As String = ""
        Public Shared EMail As String = ""
    End Class

    Public Class WebService
        '  Public Shared AdresseMcc As String = "https://www.medicallconcept.fr/WsGeneve/WsRecup_040.asmx"
        ' Public Shared UserMcc As String = "import104"
        ' Public Shared PasswordMcc As String = "ghtys1"
    End Class

    Public Class Chemins
        Public Shared DocumentScannee As String = "\\192.168.0.60\Scans\Scan_2eme_etage"
        Public Shared StockageDocument As String = "\\192.168.0.60\Scans\Scan_2eme_etage"
        Public Shared StockageFacture As String = "\\192.168.0.8\SRData\DocumentsSmartRapport\Factures\"
    End Class

    Public Shared Function Sauvegarde() As Boolean
        Return True
    End Function

    Public Shared Sub Chargement()
        Dim z_objUtilitaire As New SosMedecins.Utilitaires.LectureXml()
        ' base de donnees

#If DEBUG Then
        SosMedecins.SmartRapport.DAL.Variables.InfoConnexion.NomServeur = "(localdb)\Local"
        SosMedecins.SmartRapport.DAL.Variables.InfoConnexion.Utilisateur = "sa"
        SosMedecins.SmartRapport.DAL.Variables.InfoConnexion.MotDePasse = "root"
#Else
        SosMedecins.SmartRapport.DAL.Variables.InfoConnexion.NomServeur = "192.168.0.8"
        SosMedecins.SmartRapport.DAL.Variables.InfoConnexion.Utilisateur = "dhr"
        SosMedecins.SmartRapport.DAL.Variables.InfoConnexion.MotDePasse = "Blender%31416"
#End If
        SosMedecins.SmartRapport.DAL.Variables.InfoConnexion.BaseDonnees = "SmartRapport"

    End Sub
End Class
