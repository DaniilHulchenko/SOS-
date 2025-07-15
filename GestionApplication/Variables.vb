Imports System
Imports System.Collections.Generic
Imports System.Text

Public Class Variables

    Public Shared blnVersionDev As Boolean = False

    Public Class Utilisateurs
        Public Enum CodeDroits
            Secretaire = 1
            Medecin = 2
            Chef = 4
            Admin = 10
            Comptable = 5
        End Enum

        Public Shared Identifiant As String = ""
        Public Shared NomUtilisateur As String = ""
        Public Shared Droits As CodeDroits = CodeDroits.Comptable
        Public Shared Initiale As String = ""
        Public Shared EMail As String = ""
    End Class

    Public Shared Function Sauvegarde() As Boolean
        Return True
    End Function

    Public Shared Sub Chargement()
        Dim z_objUtilitaire As New SosMedecins.Utilitaires.LectureXml()
        ' base de donnees
        SosMedecins.SmartRapport.DAL.Variables.InfoConnexion.NomServeur = z_objUtilitaire.GetValue("Parametrage", "db_Serveur8")
        SosMedecins.SmartRapport.DAL.Variables.InfoConnexion.BaseDonnees = z_objUtilitaire.GetValue("Parametrage", "db_Base")
        SosMedecins.SmartRapport.DAL.Variables.InfoConnexion.Utilisateur = z_objUtilitaire.GetValue("Parametrage", "db_User")
        SosMedecins.SmartRapport.DAL.Variables.InfoConnexion.MotDePasse = z_objUtilitaire.GetValue("Parametrage", "db_Pass")
    End Sub
End Class
