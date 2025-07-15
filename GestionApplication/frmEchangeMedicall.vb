Imports SosMedecins.SmartRapport.Systeme
Imports SosMedecins.Connexion.FormatSql
Imports SosMedecins.SmartRapport.DAL

Public Class frmEchangeMedicall
    'Private _strConnexion As SosMedecins.Connexion.AccesDonnees = SosMedecins.SmartRapport.DAL.Variables.ConnexionBase
    Private _intNbFichesImportees As Int32
    Private _lngCodeAppelDepart As Long = 0
    Private _lngCodeAppelFin As Long = 0

    Private _strErreurs As String = String.Empty

    Private Sub btnQuitter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuitter.Click
        Me.Close()
    End Sub

    Private Sub btnExecute_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExecute.Click
        btnExecute.Enabled = False
        prgTaitement.Value = 0
        prgTaitement.Maximum = 5

        ' parametrage du Web Service de paris
        System.Net.ServicePointManager.CertificatePolicy = New TrustAllCertificatePolicy()

        OutilsExt.wsvc = New MCC_Web.WSRecup_040()
        OutilsExt.wsvc.Url = VariablesApplicatives.WebService.AdresseMcc

        If (OutilsExt.Cookies Is Nothing) Then
            OutilsExt.Cookies = New System.Net.CookieContainer()
        End If

        OutilsExt.wsvc.CookieContainer = OutilsExt.Cookies

        If (OutilsExt.wsvc.Authentification(VariablesApplicatives.WebService.UserMcc, VariablesApplicatives.WebService.PasswordMcc)) Then

            _strErreurs = String.Empty
            ' lancement des taches dans un autre thread
            bgwTraitement.RunWorkerAsync()
        Else
            Dim z_objErreur As New Utilitaires.GestionErreur("Vous n'êtes pas identifié sur le Serveur Web MediCall Concept." & vbCrLf & vbCrLf & "Contacter le service informatique.")
            z_objErreur.Show()
        End If
    End Sub

    Private Function RappatriementDonnees() As Boolean
        Dim z_lngCodeAppel As Nullable(Of Long)

        Dim z_blnConnect As Boolean = Variables.ConnexionBase.OpenBDD()
        Try
            z_lngCodeAppel = CType(Convert.ToUInt64(SosMedecins.SmartRapport.DAL.Variables.ConnexionBase.ExecuteScalar(RequetesSelect.tableoperation.NumAppelFin)), Nullable(Of Long))

            If Not z_lngCodeAppel.HasValue Then
                z_lngCodeAppel = 0
            End If

            Try
                Dim z_dstDonnees As DataSet = OutilsExt.wsvc.GetAppelsTermines(CType(z_lngCodeAppel, Long), OutilsExt.ParamAppli.intService, True)

                StockeActes(z_dstDonnees)
                Return True
            Catch ex As Exception
                _strErreurs &= vbCrLf & vbCrLf & "Impossible de se connecter au serveur Web MediCall." & vbCrLf & vbTab & ex.Message
                Return False
            End Try

        Catch ex As Exception
            _strErreurs &= vbCrLf & vbCrLf & "RappatriementDonnees" & vbCrLf & vbTab & ex.Message

            Return False
        Finally
            If (z_blnConnect) Then
                Variables.ConnexionBase.CloseBDD()
            End If
        End Try
    End Function

    ' Exportation des patients télé-alarmes qui n'existent pas chez MediCall
    Private Function ExportationPatientTA() As Boolean
        '// Sélection des patients pas encore exportée
        Dim z_strSql As String
        Dim z_dsPatients As New DataSet
        Dim z_dtbPersonne As DataTable = Nothing
        Dim z_lngIdAbonnement As Long
        Dim z_lngIdpatient As Long
        Dim z_lngNouveauIdPatient As Long
        Dim z_strRemarque As String
        Dim z_drw As DataRow
        Dim z_strRetour As String
        Dim z_dtbAbonnementcle As DataTable
        Dim z_dtbAbonnementurgence As DataTable
        Dim z_dtbAbonnementdossier As DataTable

        Dim z_blnConnect As Boolean = Variables.ConnexionBase.OpenBDD()
        Try
            z_dsPatients.Tables.Add(Variables.ConnexionBase.ExecuteSql(Nothing, RequetesSelect.ta_abonnement.Export))

            InitialiseProgressbar(prgDetail, z_dsPatients.Tables(0).Rows.Count)

            For Each z_drwPatient As DataRow In z_dsPatients.Tables(0).Rows
                '
                z_lngIdAbonnement = CType(z_drwPatient("IdAbonnement"), Long)
                z_lngIdpatient = CType(z_drwPatient("IdPatient"), Long)
                z_lngNouveauIdPatient = -1

                ' On récupere la remarque à ajouter 
                z_strRemarque = ""

                ' Selection Abonnement Cle
                z_strSql = SosMedecins.SmartRapport.DAL.RequetesSelect.ta_abonnementcle.idAbonnement
                z_strSql = z_strSql.Replace("%IdAbonnement%", z_lngIdAbonnement.ToString)
                z_dtbAbonnementcle = Variables.ConnexionBase.ExecuteSql(Nothing, z_strSql)
                ' Selection Abonnement Urgence
                z_strSql = SosMedecins.SmartRapport.DAL.RequetesSelect.ta_abonnementurgence.IdAbonnement
                z_strSql = z_strSql.Replace("%IdAbonnement%", z_lngIdAbonnement.ToString)
                z_dtbAbonnementurgence = Variables.ConnexionBase.ExecuteSql(Nothing, z_strSql)
                ' Selection Abonnement Dossier
                z_strSql = SosMedecins.SmartRapport.DAL.RequetesSelect.ta_abonnementdossier.IdAbonnement
                z_strSql = z_strSql.Replace("%IdAbonnement%", z_lngIdAbonnement.ToString)
                z_dtbAbonnementdossier = Variables.ConnexionBase.ExecuteSql(Nothing, z_strSql)
                ' entre le commentaire
                If (z_dtbAbonnementcle.Rows.Count > 0) Then
                    z_strRemarque = "Clé n°" & z_dtbAbonnementcle.Rows(0)("NumeroCle").ToString()

                    If (z_dtbAbonnementcle.Rows(0)("Commentaire").ToString() <> "") Then
                        z_strRemarque &= " - " & z_dtbAbonnementcle.Rows(0)("Commentaire").ToString()
                    End If
                    z_strRemarque &= "\r\n"
                End If

                ' entre les probleme medicaux et les medicaments
                If (z_dtbAbonnementdossier.Rows.Count > 0) Then
                    z_strRemarque &= "                    LES PROBLEMES MEDICAUX - "
                    ' problemes medicaux
                    For Each z_drw In z_dtbAbonnementdossier.Rows
                        z_strRemarque &= z_drw("TD_PbMedicaux").ToString()
                        z_strRemarque &= " / "
                    Next
                    z_strRemarque = z_strRemarque.Remove(z_strRemarque.Length - 2, 2)
                    z_strRemarque &= "\n"
                    z_strRemarque &= "    LES TRAITEMENTS: "
                    ' medicaments
                    For Each z_drw In z_dtbAbonnementdossier.Rows
                        z_strRemarque += z_drw("TD_Traitements").ToString()
                        z_strRemarque += " / "
                    Next
                    z_strRemarque = z_strRemarque.Remove(z_strRemarque.Length - 2, 2)
                    z_strRemarque += "\n"
                End If

                If (z_dtbAbonnementurgence.Rows.Count > 0) Then
                    z_strRemarque &= "    EN URGENCE APPELER: "
                    For Each z_drw In z_dtbAbonnementurgence.Rows
                        z_strRemarque &= z_drw("Nom").ToString() & " " & z_drw("Prenom").ToString() & " " & z_drw("Telephone").ToString() & "/ " & z_drw("Tel2").ToString() & "/ " & z_drw("Tel3").ToString()
                        z_strRemarque &= " / "
                    Next
                    z_strRemarque = z_strRemarque.Remove(z_strRemarque.Length - 2, 2)
                End If

                ' run the query and insert the info to dataset
                z_dtbPersonne = Nothing

                z_strSql = "SELECT pe.* from ta_abonnement ta1 inner join ta_abonnementlieufacture ta2 on ta1.IdAbonnement = ta2.TF_IdAbonnement inner join tablepatient pa on pa.IdPAtient = ta1.IdPAtient inner join tablepersonne pe on pe.IdPErsonne = pa.IdPersonne where ta1.IdAbonnement = '" & z_lngIdAbonnement.ToString() & "'"
                z_dtbPersonne = Variables.ConnexionBase.ExecuteSql(Nothing, z_strSql)

                ' on regarde si le patient a un numéro négatif, si oui il faut d'abord le créer à medicall
                If (z_lngIdpatient < 0) Then
                    '        //verify the petient has been found and insert the information to the different vars
                    If (z_dtbPersonne.Rows.Count = 1) Then
                        ' transfer the information a Medicall et recuperer le Id Patient
                        If (z_dtbPersonne.Rows(0)("DateNaissance") Is DBNull.Value OrElse z_dtbPersonne.Rows(0)("DateNaissance").ToString = "") Then
                            _strErreurs &= "Patient : " & z_dtbPersonne.Rows(0)("Nom").ToString() & " " & z_dtbPersonne.Rows(0)("Prenom").ToString() & " - " & z_dtbPersonne.Rows(0)("IdPErsonne").ToString() & " - Erreur date de naissance" & vbCrLf
                            z_strRetour = "ERR - Date de naissance"
                        Else
                            z_strRetour = OutilsExt.wsvc.CreatePatient( _
                                                   z_dtbPersonne.Rows(0)("Nom").ToString(), _
                                                   z_dtbPersonne.Rows(0)("Prenom").ToString(), _
                                                   z_dtbPersonne.Rows(0)("Tel").ToString(), _
                                                   CType(z_dtbPersonne.Rows(0)("DateNaissance"), Date), _
                                                   CType(z_dtbPersonne.Rows(0)("Sexe"), Char), _
                                                   "GENEVE", _
                                                   z_dtbPersonne.Rows(0)("Commune").ToString(), _
                                                   z_dtbPersonne.Rows(0)("CodePostal").ToString(), _
                                                   z_dtbPersonne.Rows(0)("Rue").ToString(), _
                                                   z_dtbPersonne.Rows(0)("NumeroDansRue").ToString(), _
                                                   CType(z_dtbPersonne.Rows(0)("Longitude"), Long), _
                                                   CType(z_dtbPersonne.Rows(0)("Latitude"), Long), _
                                                   z_dtbPersonne.Rows(0)("Batiment").ToString(), _
                                                   z_dtbPersonne.Rows(0)("Escalier").ToString(), _
                                                   z_dtbPersonne.Rows(0)("Digicode").ToString(), _
                                                   z_dtbPersonne.Rows(0)("Etage").ToString(), _
                                                   False, _
                                                   z_dtbPersonne.Rows(0)("Internom").ToString(), _
                                                   z_dtbPersonne.Rows(0)("Porte").ToString(), _
                                                   "")

                            If (z_strRetour.IndexOf("ERR") = -1) Then
                                z_lngNouveauIdPatient = Long.Parse(z_strRetour)

                                Variables.ConnexionBase.ExecuteSqlSansRetour("update ta_abonnement set IdPatient = " & z_lngNouveauIdPatient & " WHERE IdPatient = " & z_lngIdpatient)
                                Variables.ConnexionBase.ExecuteSqlSansRetour("update tablepatient set IdPatient = " & z_lngNouveauIdPatient & " WHERE IdPatient = " & z_lngIdpatient)
                                Variables.ConnexionBase.ExecuteSqlSansRetour("update fac_destinatairefacture set IdPatient = " & z_lngNouveauIdPatient & " WHERE IdPatient = " & z_lngIdpatient)
                                Variables.ConnexionBase.ExecuteSqlSansRetour("update patientjointdoc set IdPatient = " & z_lngNouveauIdPatient & " WHERE IdPatient = " & z_lngIdpatient)
                                Variables.ConnexionBase.ExecuteSqlSansRetour("update tableactes set IndicePatient = " & z_lngNouveauIdPatient & " WHERE IndicePatient = " & z_lngIdpatient)
                                Variables.ConnexionBase.ExecuteSqlSansRetour("update tableconsultations set IndicePatient = " & z_lngNouveauIdPatient & " WHERE IndicePatient = " & z_lngIdpatient)
                                Variables.ConnexionBase.ExecuteSqlSansRetour("update tablepatientmedttt set IdPatient = " & z_lngNouveauIdPatient & " WHERE IdPatient = " & z_lngIdpatient)
                                Variables.ConnexionBase.ExecuteSqlSansRetour("update tablerapprochementpatient set IdPatient_Parent = " & z_lngNouveauIdPatient & " WHERE IdPatient_Parent = " & z_lngIdpatient)
                                Variables.ConnexionBase.ExecuteSqlSansRetour("update tablerapprochementpatient set IdPatient_Enfant = " & z_lngNouveauIdPatient & " WHERE IdPatient_Enfant = " & z_lngIdpatient)
                                Variables.ConnexionBase.ExecuteSqlSansRetour("update patient_remarque set IdPatient = " & z_lngNouveauIdPatient & " WHERE IdPatient = " & z_lngIdpatient)
                            Else
                                _strErreurs &= "Patient : " & z_dtbPersonne.Rows(0)("Nom").ToString() & " " & z_dtbPersonne.Rows(0)("Prenom").ToString() & " - " & z_dtbPersonne.Rows(0)("IdPErsonne").ToString() & " - " & z_strRetour & vbCrLf
                            End If
                        End If
                    Else
                        _strErreurs &= "Patient : " & z_lngIdpatient & " - Aucune personne attribué" & vbCrLf
                        z_strRetour = "ERR"
                    End If
                Else
                    z_lngNouveauIdPatient = z_lngIdpatient
                    If (z_dtbPersonne.Rows(0)("DateNaissance") Is DBNull.Value OrElse z_dtbPersonne.Rows(0)("DateNaissance").ToString = "") Then
                        _strErreurs &= "Patient : " & z_dtbPersonne.Rows(0)("Nom").ToString() & " " & z_dtbPersonne.Rows(0)("Prenom").ToString() & " - " & z_dtbPersonne.Rows(0)("IdPErsonne").ToString() & " - Erreur date de naissance" & vbCrLf
                        z_strRetour = "ERR - Date de naissance"
                    Else
                        z_strRetour = OutilsExt.wsvc.UpdatePatient( _
                                    z_lngIdpatient, _
                                    z_dtbPersonne.Rows(0)("Nom").ToString(), _
                                    z_dtbPersonne.Rows(0)("Prenom").ToString(), _
                                    z_dtbPersonne.Rows(0)("Tel").ToString(), _
                                    CType(z_dtbPersonne.Rows(0)("DateNaissance"), Date), _
                                    "GENEVE", _
                                    z_dtbPersonne.Rows(0)("Commune").ToString(), _
                                    z_dtbPersonne.Rows(0)("CodePostal").ToString(), _
                                    z_dtbPersonne.Rows(0)("Rue").ToString(), _
                                    z_dtbPersonne.Rows(0)("NumeroDansRue").ToString(), _
                                    CType(z_dtbPersonne.Rows(0)("Longitude"), Int64), _
                                    CType(z_dtbPersonne.Rows(0)("Latitude"), Int64), _
                                    z_dtbPersonne.Rows(0)("Batiment").ToString(), _
                                    z_dtbPersonne.Rows(0)("Escalier").ToString(), _
                                    z_dtbPersonne.Rows(0)("Digicode").ToString(), _
                                    z_dtbPersonne.Rows(0)("Etage").ToString(), _
                                    False, _
                                    z_dtbPersonne.Rows(0)("Internom").ToString(), _
                                    z_dtbPersonne.Rows(0)("Porte").ToString(), _
                                    "")

                        If (z_strRetour.IndexOf("ERR") > -1) Then
                            'Throw New Exception(z_strRetour)
                            _strErreurs &= "Patient : " & z_dtbPersonne.Rows(0)("Nom").ToString() & " " & z_dtbPersonne.Rows(0)("Prenom").ToString() & " - " & z_lngIdpatient.ToString() & " - " & z_strRetour & vbCrLf
                        End If
                    End If
                End If

                If (z_lngNouveauIdPatient > -1 AndAlso z_strRetour.ToUpper.IndexOf("ERR") = -1) Then
                    z_strRetour = OutilsExt.wsvc.AddRemToPatient(z_lngNouveauIdPatient, "TA", z_strRemarque, "GENEVE")

                    If (z_strRetour.ToUpper.IndexOf("ERR") = -1) Then
                        ' on flag l'export comme 'fait'
                        Variables.ConnexionBase.ExecuteSqlSansRetour("update ta_abonnement set ExportMCC = 1 where IdPatient = " & z_lngNouveauIdPatient.ToString())
                    Else
                        _strErreurs &= "Patient : " & z_dtbPersonne.Rows(0)("Nom").ToString() & " " & z_dtbPersonne.Rows(0)("Prenom").ToString() & " - " & z_lngIdpatient.ToString() & " - " & z_strRetour & vbCrLf
                    End If
                End If

                AugmenteProgressbar(prgDetail)
            Next
            Return True

        Catch ex As Exception
            _strErreurs &= vbCrLf & vbCrLf & "ExportationPatientTA - Abonnement " & z_lngIdAbonnement & " - " & vbCrLf & vbTab & ex.Message
            Return False
        Finally
            If (z_blnConnect) Then
                Variables.ConnexionBase.CloseBDD()
            End If
        End Try
    End Function

    Private Function MiseAJourMedecins() As Boolean
        Try
            Dim z_arrMedecins() As String = OutilsExt.wsvc.GetListeMedecins(SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.intService)
            Dim z_strValeur() As String
            Dim z_strSql As String

            Dim z_blnConnect As Boolean = Variables.ConnexionBase.OpenBDD()
            Try
                InitialiseProgressbar(prgDetail, z_arrMedecins.Length)

                For Each z_strLigne As String In z_arrMedecins
                    z_strValeur = Split(z_strLigne, ";")

                    If Variables.ConnexionBase.ExecuteScalar(RequetesSelect.tablemedecin.CodeIntervenant.Replace("%CodeIntervenant", Format_Nombre(z_strValeur(0))).ToString) Is Nothing Then
                        z_strSql = RequetesInsert.tablemedecin.Complet
                        z_strSql = z_strSql.Replace("%CodeIntervenant", Format_Nombre(z_strValeur(0)))
                        z_strSql = z_strSql.Replace("%Nom", Format_String(z_strValeur(1)))
                        z_strSql = z_strSql.Replace("%Initiale", Format_String(z_strValeur(2)))
                        z_strSql = z_strSql.Replace("%Service", Format_Nombre(z_strValeur(3)))
                        z_strSql = z_strSql.Replace("%Archive", Format_Nombre(z_strValeur(4)))
                        z_strSql = z_strSql.Replace("%Mail", Format_String(z_strValeur(5)))
                        z_strSql = z_strSql.Replace("%NoGeneve", Format_String(z_strValeur(1).Substring(0, z_strValeur(1).LastIndexOf(" "))))
                        z_strSql = z_strSql.Replace("%PrenomGeneve", Format_String(z_strValeur(1).Substring(z_strValeur(1).LastIndexOf(" ") + 1)))

                        Variables.ConnexionBase.ExecuteSqlSansRetour(z_strSql)
                    Else
                        z_strSql = RequetesUpdate.tablemedecin.Complet
                        z_strSql = z_strSql.Replace("%CodeIntervenant", Format_Nombre(z_strValeur(0)))
                        z_strSql = z_strSql.Replace("%Nom", Format_String(z_strValeur(1)))
                        z_strSql = z_strSql.Replace("%Initiale", Format_String(z_strValeur(2)))
                        z_strSql = z_strSql.Replace("%Archive", Format_Nombre(z_strValeur(4)))

                        Variables.ConnexionBase.ExecuteSqlSansRetour(z_strSql)
                    End If
                    AugmenteProgressbar(prgDetail)
                Next
            Catch ex As Exception
                _strErreurs &= "Erreur de mise à jour des medecins : " & ex.Message & vbCrLf
            Finally
                If (z_blnConnect) Then
                    Variables.ConnexionBase.CloseBDD()
                End If
            End Try

            Return True
        Catch ex As Exception
            _strErreurs &= vbCrLf & vbCrLf & "MiseAJourMedecins" & vbCrLf & vbTab & ex.Message
            Return False
        End Try
    End Function

    ' **************************************************
    ' Dataset 1 contenant les consultations : 
    ' Table 1 : Fiches d'appels
    ' Table 2 : Fiches Consultations
    ' Table 3 : Fiches Patient
    ' DataSet 2 contenant les factures :
    '**************************************************
    Private Sub StockeActes(ByVal p_dst As DataSet)
        If Not (p_dst Is Nothing) Then
            InitialiseProgressbar(prgDetail, p_dst.Tables(0).Rows.Count + p_dst.Tables(1).Rows.Count + p_dst.Tables(2).Rows.Count)

            If (AjouteActes(p_dst.Tables(0))) Then
                If (AjouteConsultation(p_dst.Tables(1))) Then
                    'Insertion des fiches de patient / ou modification 
                    For Each z_drw As DataRow In p_dst.Tables(2).Rows
                        MiseAJourPatient(z_drw)

                        AugmenteProgressbar(prgDetail)
                    Next
                End If
            End If
        End If
    End Sub

    Private Function AjouteActes(ByVal p_dtActes As DataTable) As Boolean
        Dim z_strSql As String = ""
        Dim z_lngNumeroAppel As Long = -1
        Dim z_dalFonction As New Fonction()

        _intNbFichesImportees = 0

        Dim z_blnConnect As Boolean = Variables.ConnexionBase.OpenBDD()
        Try
            ' Insertion des fiches d'appels :
            For Each z_drw As DataRow In p_dtActes.Rows

                z_lngNumeroAppel = Long.Parse(z_drw.ItemArray(0).ToString())
                If _lngCodeAppelDepart > z_lngNumeroAppel OrElse _lngCodeAppelDepart = 0 Then
                    _lngCodeAppelDepart = z_lngNumeroAppel
                End If
                If _lngCodeAppelFin < z_lngNumeroAppel Then
                    _lngCodeAppelFin = z_lngNumeroAppel
                End If

                z_strSql = "INSERT INTO tableactes ( "
                z_strSql &= "Num, IndicePatient, Tel, DAP, DTR, DRC, DSL, DFI, CodeMotif1, CodeMotif2, Urgence, DelaiIndique, CommentaireTransmis, CommentaireFichier,"
                z_strSql &= "AnnulationAppel, DAN, MotifAnnulation, DevenirAnnulation, ComplementInfo, Motif1, Motif2, OrigineAppel, CodeIntervenant, DelaiArrivee,"
                z_strSql &= "DelaiInterv, ExportSmartOk"
                z_strSql &= ") Values ("
                z_strSql &= Format_String(z_drw("Num").ToString())
                z_strSql &= "," & Format_String(z_drw("IndicePatient").ToString())
                z_strSql &= "," & Format_String(z_drw("Tel").ToString())
                z_strSql &= "," & Format_DateHeure(z_drw("DAP").ToString())
                z_strSql &= "," & Format_DateHeure(z_drw("DTR").ToString())
                z_strSql &= "," & Format_DateHeure(z_drw("DRC").ToString())
                z_strSql &= "," & Format_DateHeure(z_drw("DSL").ToString())
                z_strSql &= "," & Format_DateHeure(z_drw("DFI").ToString())
                z_strSql &= "," & Format_String(z_drw("CodeMotif1").ToString())
                z_strSql &= "," & Format_String(z_drw("CodeMotif2").ToString())
                z_strSql &= "," & Format_String(z_drw("Urgence").ToString())
                z_strSql &= "," & Format_String(z_drw("DelaiIndique").ToString())
                z_strSql &= "," & Format_String(z_drw("CommentaireTransmis").ToString())
                z_strSql &= "," & Format_String(z_drw("CommentaireFichier").ToString())
                z_strSql &= "," & Format_String(z_drw("AnnulationAppel").ToString())
                z_strSql &= "," & Format_DateHeure(z_drw("DAN").ToString())
                z_strSql &= "," & Format_String(z_drw("MotifAnnulation").ToString())
                z_strSql &= "," & Format_String(z_drw("DevenirAnnulation").ToString())
                z_strSql &= "," & Format_String(z_drw("ComplementInfo").ToString())
                z_strSql &= "," & Format_String(z_drw("Motif1").ToString())
                z_strSql &= "," & Format_String(z_drw("Motif2").ToString())
                z_strSql &= "," & Format_String(z_drw("OrigineAppel").ToString())
                z_strSql &= "," & Format_String(z_drw("CodeIntervenant").ToString())
                z_strSql &= "," & Format_String(z_drw("DelaiArrivee").ToString())
                z_strSql &= "," & Format_String(z_drw("DelaiInterv").ToString())
                z_strSql &= "," & Format_String("0")
                z_strSql &= ")"

                If Not (z_dalFonction.CodeAppelExiste(z_lngNumeroAppel)) Then
                    Variables.ConnexionBase.ExecuteSqlSansRetour(z_strSql)
                    _intNbFichesImportees += 1
                End If
                AugmenteProgressbar(prgDetail)
            Next
        Catch ex As Exception
            _strErreurs &= vbCrLf & vbCrLf & "AjouteActes" & vbCrLf & vbTab & ex.Message
            Return False
        Finally
            If (z_blnConnect) Then
                Variables.ConnexionBase.CloseBDD()
            End If
        End Try

        Return True
    End Function

    Private Function AjouteConsultation(ByVal p_dtConsultation As DataTable) As Boolean
        Dim z_strSql As String = ""
        Dim z_dalFonction As New Fonction()

        Dim z_blnConnect As Boolean = Variables.ConnexionBase.OpenBDD()
        Try
            ' Insertion des fiches de consultations :
            For Each z_drw As DataRow In p_dtConsultation.Rows
                z_strSql = "INSERT INTO tableconsultations ("
                z_strSql &= "NConsultation, CodeAppel, IndicePatient, IdDiag1, IdDiag2, diag1, diag2, Hono, Reglement, actes, devenir, PriseEnChargePatient, LibCisp, "
                z_strSql &= "Traitements, TraitementLibre, gestes, CommentaireLibre, EnvoiDocument, ListeIndexServiceExt, ListeIndexMt, TensionHaute, TensionBasse, "
                z_strSql &= "O2, Pulsations, Temperature, ECGDecrypte"
                z_strSql &= ") values ("
                z_strSql &= Format_String(z_drw("NConsultation").ToString())
                z_strSql &= "," & Format_String(z_drw("CodeAppel").ToString())
                z_strSql &= "," & Format_String(z_drw("IndicePatient").ToString())
                z_strSql &= "," & Format_String(z_drw("IdDiag1").ToString())
                z_strSql &= "," & Format_String(z_drw("IdDiag2").ToString())
                z_strSql &= "," & Format_String(z_drw("diag1").ToString())
                z_strSql &= "," & Format_String(z_drw("diag2").ToString())
                z_strSql &= "," & Format_String(z_drw("Hono").ToString())
                z_strSql &= "," & Format_String(z_drw("Reglement").ToString())
                z_strSql &= "," & Format_String(z_drw("actes").ToString())
                z_strSql &= "," & Format_String(z_drw("devenir").ToString())
                z_strSql &= "," & Format_String(z_drw("PriseEnChargePatient").ToString())
                z_strSql &= "," & Format_String(z_drw("LibCisp").ToString())
                z_strSql &= "," & Format_String(z_drw("Traitements").ToString())
                z_strSql &= "," & Format_String(z_drw("TraitementLibre").ToString())
                z_strSql &= "," & Format_String(z_drw("gestes").ToString())
                z_strSql &= "," & Format_String(z_drw("CommentaireLibre").ToString())
                z_strSql &= "," & Format_String(z_drw("EnvoiDocument").ToString())
                z_strSql &= "," & Format_String(z_drw("ListeIndexServiceExt").ToString())
                z_strSql &= "," & Format_String(z_drw("ListeIndexMt").ToString())
                z_strSql &= "," & Format_String(z_drw("TensionHaute").ToString())
                z_strSql &= "," & Format_String(z_drw("TensionBasse").ToString())
                z_strSql &= "," & Format_String(z_drw("O2").ToString())
                z_strSql &= "," & Format_String(z_drw("Pulsations").ToString())
                z_strSql &= "," & Format_String(z_drw("Temperature").ToString())
                z_strSql &= "," & Format_String(z_drw("ECGDecrypte").ToString())
                z_strSql &= ")"

                If (z_drw("NConsultation").ToString() <> System.DBNull.Value.ToString() _
                    AndAlso z_drw("NConsultation").ToString().Trim() <> "" _
                    AndAlso Not z_dalFonction.NumeroConsultationExiste(CType(z_drw("NConsultation"), Long))) Then

                    Variables.ConnexionBase.ExecuteSqlSansRetour(z_strSql)

                End If
                AugmenteProgressbar(prgDetail)
            Next
        Catch ex As Exception
            _strErreurs &= vbCrLf & vbCrLf & "AjouteConsultation" & vbCrLf & vbTab & ex.Message
            Return False
        Finally
            If (z_blnConnect) Then
                Variables.ConnexionBase.CloseBDD()
            End If
        End Try

        Return True
    End Function

    Private Function TransfertDictee() As Boolean
        '
        Try
            Dim z_strFichiers() As String = System.IO.Directory.GetFiles(VariablesApplicatives.Chemins.DicteeDepart)
            Dim z_strInfoFichier As System.IO.FileInfo

            InitialiseProgressbar(prgDetail, z_strFichiers.Length)

            For Each z_strFichier As String In z_strFichiers
                z_strInfoFichier = New System.IO.FileInfo(z_strFichier)
                System.IO.File.Copy(z_strFichier, VariablesApplicatives.Chemins.DicteeArrivee & "\" & z_strInfoFichier.Name, True)
                System.IO.File.Delete(z_strFichier)
                AugmenteProgressbar(prgDetail)
            Next

            Return True
        Catch ex As Exception
            _strErreurs &= vbCrLf & "Erreur du Transfert de Dictee : " & ex.Message & vbCrLf
            Return False
        End Try
    End Function

#Region " Mise à jour Base de données "
    Private Function InsertionOperation() As Boolean
        Dim z_blnConnect As Boolean = Variables.ConnexionBase.OpenBDD()
        Try
            Dim z_strSql As String = RequetesInsert.tableoperation.Complet
            z_strSql = z_strSql.Replace("%DateOp", Format_DateHeure(Now.ToString()))
            z_strSql = z_strSql.Replace("%NumFicheMin", _lngCodeAppelDepart.ToString())
            z_strSql = z_strSql.Replace("%NumFicheMax", _lngCodeAppelFin.ToString())

            SosMedecins.SmartRapport.DAL.Variables.ConnexionBase.ExecuteSqlSansRetour(z_strSql)
            Return True
        Catch ex As Exception
            _strErreurs &= vbCrLf & vbCrLf & "InsertionOperation" & vbCrLf & vbTab & ex.Message
            Return False
        Finally
            If (z_blnConnect) Then
                Variables.ConnexionBase.CloseBDD()
            End If
        End Try
    End Function

    Public Function MiseAJourPatient(ByVal row As DataRow) As Boolean
        Dim z_lngNumPatient As Long = CType(row("IndicePatient"), Long)
        Dim z_lngNumPersonne As Nullable(Of Long)
        Dim z_strReqPers As String
        Dim z_strReqPat As String
        Dim z_dalFonction As New SosMedecins.SmartRapport.DAL.Fonction()

        Dim z_blnConnect As Boolean = Variables.ConnexionBase.OpenBDD()
        Try
            z_lngNumPersonne = CType(Variables.ConnexionBase.ExecuteScalar(RequetesSelect.tablepatient.IdPersonne.Replace("%IdPatient", z_lngNumPatient.ToString())), Nullable(Of Long))

            If Not z_lngNumPersonne.HasValue Then
                ' Création de la personne :
                z_lngNumPersonne = z_dalFonction.Compteur("tablepersonne", "IdPersonne", Fonction.DirectionCompteur.Maximum)

                z_strReqPers = "INSERT INTO tablepersonne "
                z_strReqPers &= " (IdPersonne,Tel,Nom,Prenom,NumAdresse,CodePostal,Departement,Commune,Rue,NumeroDansRue,Batiment,Escalier,Etage,Digicode,Internom,Porte,Longitude,Latitude,DateNaissance,Sexe,Age,UniteAge,TexteSup,Adm_CodePostal,Adm_Commune,Adm_Rue,Adm_NumeroDansRue,Adm_Batiment)"
                z_strReqPers &= " values ( " _
                            & Format_String(z_lngNumPersonne.ToString) _
                            & "," & Format_String(row("Tel").ToString()) _
                            & "," & Format_String(row("Nom").ToString()) _
                            & "," & Format_String(row("Prenom").ToString()) _
                            & "," & Format_String(row("NumAdresse").ToString()) _
                            & "," & Format_String(row("CodePostal").ToString()) _
                            & "," & Format_String(row("Departement").ToString()) _
                            & "," & Format_String(row("Commune").ToString()) _
                            & "," & Format_String(row("Rue").ToString()) _
                            & "," & Format_String(row("NumeroDansRue").ToString()) _
                            & "," & Format_String(row("Batiment").ToString()) _
                            & "," & Format_String(row("Escalier").ToString()) _
                            & "," & Format_String(row("Etage").ToString()) _
                            & "," & Format_String(row("Digicode").ToString()) _
                            & "," & Format_String(row("InterNom").ToString()) _
                            & "," & Format_String(row("Porte").ToString()) _
                            & "," & Format_String(row("Longitude").ToString()) _
                            & "," & Format_String(row("Latitude").ToString()) _
                            & "," & Format_Date(row("DateNaissance").ToString()) _
                            & "," & Format_String(row("Sexe").ToString()) _
                            & "," & Format_String(row("Age").ToString()) _
                            & "," & Format_String(row("UniteAge").ToString()) _
                            & "," & Format_String(row("TexteSup").ToString()) _
                            & "," & Format_String(row("CodePostal").ToString()) _
                            & "," & Format_String(row("Commune").ToString()) _
                            & "," & Format_String(row("Rue").ToString()) _
                            & "," & Format_String(row("NumeroDansRue").ToString()) _
                            & "," & Format_String(row("Batiment").ToString()) _
                            & ")"

                ' Insertion du patient :
                z_strReqPat = "INSERT INTO tablepatient (IdPatient,IdPersonne,SuiviPatient,IdAbonnement,TypeAbonnement,TexteAbonnement,Approuve) "
                z_strReqPat &= " values (" _
                            & Format_String(z_lngNumPatient.ToString()) _
                            & ", " & Format_String(z_lngNumPersonne.ToString()) _
                            & ", " & Format_String(row("SuiviPatient").ToString()) _
                            & ", " & Format_String(row("IdAbonnement").ToString()) _
                            & ", " & Format_String(row("TypeAbonnement").ToString()) _
                            & ", " & Format_String(row("TexteAbonnement").ToString()) _
                            & ", 1)"
            Else
                If (Variables.ConnexionBase.ExecuteScalar(RequetesSelect.tablepersonne.IdPersonne.Replace("%IdPersonne", z_lngNumPersonne.ToString())) IsNot Nothing) Then
                    ' On met à jour la personne 
                    ' on crée à nouveau cette personne
                    z_strReqPers = "update tablepersonne set" _
                            & " Tel = " & Format_String(row("Tel").ToString()) _
                            & ", Nom = " & Format_String(row("Nom").ToString()) _
                            & ", Prenom = " & Format_String(row("Prenom").ToString()) _
                            & ", NumAdresse = " & Format_String(row("NumAdresse").ToString()) _
                            & ", CodePostal = " & Format_String(row("CodePostal").ToString()) _
                            & ", Departement = " & Format_String(row("Departement").ToString()) _
                            & ", Commune = " & Format_String(row("Commune").ToString()) _
                            & ", Rue = " & Format_String(row("Rue").ToString()) _
                            & ", NumeroDansRue = " & Format_String(row("NumeroDansRue").ToString()) _
                            & ", Batiment = " & Format_String(row("Batiment").ToString()) _
                            & ", Escalier = " & Format_String(row("Escalier").ToString()) _
                            & ", Etage = " & Format_String(row("Etage").ToString()) _
                            & ", Digicode = " & Format_String(row("Digicode").ToString()) _
                            & ", Internom = " & Format_String(row("InterNom").ToString()) _
                            & ", Porte = " & Format_String(row("Porte").ToString()) _
                            & ", Longitude = " & Format_String(row("Longitude").ToString()) _
                            & ", Latitude = " & Format_String(row("Latitude").ToString()) _
                            & ", DateNaissance = " & Format_Date(row("DateNaissance").ToString()) _
                            & ", Adm_NumeroDansRue = " & Format_String(row("NumeroDansRue").ToString()) _
                            & ", Adm_Batiment = " & Format_String(row("Batiment").ToString()) _
                            & ", Sexe = " & Format_String(row("Sexe").ToString()) _
                            & ", Age = " & Format_String(row("Age").ToString()) _
                            & ", UniteAge = " & Format_String(row("UniteAge").ToString()) _
                            & ", TexteSup = " & Format_String(row("TexteSup").ToString()) _
                            & " WHERE IdPersonne = " & z_lngNumPersonne.ToString

                    ' on met à jour le patient
                    z_strReqPat = "UPDATE tablepatient set" _
                            & " SuiviPatient = " & Format_String(row("SuiviPatient").ToString()) _
                            & " WHERE IdPatient = " & z_lngNumPatient
                Else
                    ' on crée à nouveau cette personne
                    z_strReqPers = "INSERT INTO tablepersonne ( " _
                            & "IdPersonne,Tel,Nom,Prenom,NumAdresse,CodePostal,Departement,Commune,Rue,NumeroDansRue,Batiment,Escalier,Etage,Digicode,Internom,Porte,Longitude,Latitude,DateNaissance,Sexe,Age,UniteAge,TexteSup,Adm_CodePostal,Adm_Commune,Adm_Rue,Adm_NumeroDansRue,Adm_Batiment)" _
                            & " values ( " _
                            & Format_String(z_lngNumPersonne.ToString()) _
                            & ", " & Format_String(row("Tel").ToString()) _
                            & ", " & Format_String(row("Nom").ToString()) _
                            & ", " & Format_String(row("Prenom").ToString()) _
                            & ", " & Format_String(row("NumAdresse").ToString()) _
                            & ", " & Format_String(row("CodePostal").ToString()) _
                            & ", " & Format_String(row("Departement").ToString()) _
                            & ", " & Format_String(row("Commune").ToString()) _
                            & ", " & Format_String(row("Rue").ToString()) _
                            & ", " & Format_String(row("NumeroDansRue").ToString()) _
                            & ", " & Format_String(row("Batiment").ToString()) _
                            & ", " & Format_String(row("Escalier").ToString()) _
                            & ", " & Format_String(row("Etage").ToString()) _
                            & ", " & Format_String(row("Digicode").ToString()) _
                            & ", " & Format_String(row("InterNom").ToString()) _
                            & ", " & Format_String(row("Porte").ToString()) _
                            & ", " & Format_String(row("Longitude").ToString()) _
                            & ", " & Format_String(row("Latitude").ToString()) _
                            & ", " & Format_Date(row("DateNaissance").ToString()) _
                            & ", " & Format_String(row("Sexe").ToString()) _
                            & ", " & Format_String(row("Age").ToString()) _
                            & ", " & Format_String(row("UniteAge").ToString()) _
                            & ", " & Format_String(row("TexteSup").ToString()) _
                            & ", " & Format_String(row("CodePostal").ToString()) _
                            & ", " & Format_String(row("Commune").ToString()) _
                            & ", " & Format_String(row("Rue").ToString()) _
                            & ", " & Format_String(row("NumeroDansRue").ToString()) _
                            & ", " & Format_String(row("Batiment").ToString()) _
                            & ")"

                    ' on met à jour le patient
                    z_strReqPat = "UPDATE tablepatient set" _
                            & " SuiviPatient = " & Format_String(row("SuiviPatient").ToString()) _
                            & " WHERE IdPatient = " & z_lngNumPatient

                End If
            End If
            Variables.ConnexionBase.ExecuteSqlSansRetour(z_strReqPers)
            Variables.ConnexionBase.ExecuteSqlSansRetour(z_strReqPat)
        Catch ex As Exception
            _strErreurs &= vbCrLf & vbCrLf & "MiseAJourPatient - " & z_lngNumPatient.ToString & " - " & z_lngNumPersonne.ToString & vbCrLf & vbTab & ex.Message
            Return False
        Finally
            If (z_blnConnect) Then
                Variables.ConnexionBase.CloseBDD()
            End If
        End Try

        Return True
    End Function

    ' Procédure d'export des données
    Private Function ExportRemarquables() As Boolean

        Dim z_blnConnect As Boolean = Variables.ConnexionBase.OpenBDD()
        Try
            Dim z_strSql As String = "SELECT * from patient_remarque WHERE Export = 0 and Archive = 0 and IdPatient >0"
            Dim z_dst As New DataSet
            Dim z_lngIdPatient As Long = 0
            Dim z_strResponse As String

            z_dst.Tables.Add(Variables.ConnexionBase.ExecuteSql(Nothing, z_strSql))

            For Each z_drw As DataRow In z_dst.Tables(0).Rows
                z_lngIdPatient = Long.Parse(z_drw("IdPatient").ToString())
                ' Mise a jour par WebService
                If (z_drw("Medical").ToString().Length > 0) Then
                    z_strResponse = OutilsExt.wsvc.AddRemToPatient(z_lngIdPatient, "RM", z_drw("Medical").ToString(), "GENEVE")
                Else
                    z_strResponse = OutilsExt.wsvc.RemoveRemToPatient(z_lngIdPatient, "RM")
                End If

                If z_strResponse.IndexOf("ERR") = -1 Then
                    Dim z_strRemarqueEconomique As String = z_drw("Economique").ToString()
                    If (z_drw("Encaisse").ToString = "1") Then
                        z_strRemarqueEconomique &= vbCrLf + "Faire encaissement sur place"
                    End If
                    If (z_drw("Cession").ToString = "1") Then
                        z_strRemarqueEconomique &= vbCrLf + "Faire signer une cession de créance"
                    End If

                    If (z_strRemarqueEconomique.Length > 0) Then
                        z_strResponse = SosMedecins.SmartRapport.Systeme.OutilsExt.wsvc.AddRemToPatient(z_lngIdPatient, "RE", z_strRemarqueEconomique, "GENEVE")
                    Else
                        z_strResponse = OutilsExt.wsvc.RemoveRemToPatient(z_lngIdPatient, "RE")
                    End If
                End If
                'verifie que response = OK
                If (z_strResponse.IndexOf("ERR") = -1) Then
                    'on flag l'export comme 'fait'
                    Variables.ConnexionBase.ExecuteSqlSansRetour("update patient_remarque set Export = 1 where IdPatient = " + z_lngIdPatient.ToString)
                Else
                    _strErreurs &= "Remarque patient : " & z_lngIdPatient.ToString & " - " & z_strResponse & vbCrLf
                    'Throw New Exception(z_strResponse)
                End If
            Next

            Return True
        Catch ex As Exception
            _strErreurs &= vbCrLf & vbCrLf & "ExportRemarquables" & vbCrLf & vbTab & ex.Message
            Return False
        Finally
            If (z_blnConnect) Then
                Variables.ConnexionBase.CloseBDD()
            End If
        End Try
    End Function
#End Region

#Region " Gestion du MultiThread "
    Private Sub bgwTraitement_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgwTraitement.DoWork
        Dim z_blnRetour As Boolean = True
        '
        SetLblTraitement("Récupération des appels")
        If Not RappatriementDonnees() Then
            z_blnRetour = False
        End If
        AugmenteProgressbar(prgTaitement)
        '
        SetLblTraitement("Exportation des patients")
        If Not ExportationPatientTA() Then
            z_blnRetour = False
        End If
        AugmenteProgressbar(prgTaitement)
        '
        SetLblTraitement("Mise à jour des remarquables")
        If Not ExportRemarquables() Then
            z_blnRetour = False
        End If
        AugmenteProgressbar(prgTaitement)
        ' 
        SetLblTraitement("Mise à jour des Medecins")
        If Not MiseAJourMedecins() Then
            z_blnRetour = False
        End If
        AugmenteProgressbar(prgTaitement)
        ' 
        SetLblTraitement("Transfert des dictées")
        If Not TransfertDictee() Then
            z_blnRetour = False
        End If
        AugmenteProgressbar(prgTaitement)

        If z_blnRetour Then
            SetColorTraitement(False)
            If (_intNbFichesImportees = 0) Then
                SetLblTraitement("Traitement terminé - Aucune nouvelle fiche importée.")
            Else
                If (InsertionOperation()) Then
                    SetLblTraitement("Traitement terminé - " & _intNbFichesImportees.ToString() & " nouvelles fiches importées")
                End If
            End If
        Else
            SetLblTraitement("Erreur lors des échanges.")
            SetColorTraitement(True)
        End If
    End Sub

    Delegate Sub SetLblTraitementCallBack(ByVal p_strChaine As String)
    Private Sub SetLblTraitement(ByVal p_strChaine As String)
        If lblTraitement.InvokeRequired Then
            Dim d As New SetLblTraitementCallBack(AddressOf SetLblTraitement)
            Me.Invoke(d, p_strChaine)
        Else
            lblTraitement.Text = p_strChaine & " - " & prgTaitement.Value & "/" & prgTaitement.Maximum
        End If
    End Sub

    Delegate Sub SetColorTraitementCallBack(ByVal p_blnColor As Boolean)
    Private Sub SetColorTraitement(ByVal p_blnColor As Boolean)
        If lblTraitement.InvokeRequired Then
            Dim d As New SetColorTraitementCallBack(AddressOf SetColorTraitement)
            Me.Invoke(d, p_blnColor)
        Else
            If (p_blnColor) Then
                lblTraitement.ForeColor = Color.Red
            Else
                lblTraitement.ForeColor = System.Drawing.SystemColors.ControlText
            End If
        End If
    End Sub

    Delegate Sub AugmenteProgressbarCallBack(ByVal p_prg As System.Windows.Forms.ProgressBar)
    Private Sub AugmenteProgressbar(ByVal p_prg As System.Windows.Forms.ProgressBar)
        If p_prg.InvokeRequired Then
            Dim d As New AugmenteProgressbarCallBack(AddressOf AugmenteProgressbar)
            Me.Invoke(d, p_prg)
        Else
            p_prg.Value += 1
        End If
    End Sub

    Delegate Sub InitialiseProgressbarCallBack(ByVal p_prg As System.Windows.Forms.ProgressBar, ByVal p_intMax As Integer)
    Private Sub InitialiseProgressbar(ByVal p_prg As System.Windows.Forms.ProgressBar, ByVal p_intMax As Integer)
        If p_prg.InvokeRequired Then
            Dim d As New InitialiseProgressbarCallBack(AddressOf InitialiseProgressbar)
            Me.Invoke(d, p_prg, p_intMax)
        Else
            p_prg.Value = 0
            p_prg.Minimum = 0
            p_prg.Maximum = p_intMax
        End If
    End Sub

    Private Sub bgwTraitement_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwTraitement.RunWorkerCompleted
        If (_strErreurs.Length > 0) Then
            Dim z_objErreur As New Utilitaires.GestionErreur(_strErreurs)
            z_objErreur.Show(Utilitaires.frmErreur.Type.Information)
        End If
    End Sub
#End Region

    Private Sub btnDepannage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDepannage.Click
        Dim z_frmDepannage As New frmDepannageEchange

        If (z_frmDepannage.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            RecuperationParticuliere(z_frmDepannage.Debut, z_frmDepannage.Fin)
        End If

        z_frmDepannage.Dispose()
        z_frmDepannage = Nothing
    End Sub

    Private Sub RecuperationParticuliere(ByVal z_strDebut As String, ByVal z_strFin As String)
        Try
            ' parametrage du Web Service de paris
            System.Net.ServicePointManager.CertificatePolicy = New TrustAllCertificatePolicy()

            OutilsExt.wsvc = New MCC_Web.WSRecup_040()
            OutilsExt.wsvc.Url = VariablesApplicatives.WebService.AdresseMcc

            If (OutilsExt.Cookies Is Nothing) Then
                OutilsExt.Cookies = New System.Net.CookieContainer()
            End If

            OutilsExt.wsvc.CookieContainer = OutilsExt.Cookies

            If (OutilsExt.wsvc.Authentification(VariablesApplicatives.WebService.UserMcc, VariablesApplicatives.WebService.PasswordMcc)) Then
                Dim z_dstDonnees As DataSet = OutilsExt.wsvc.GetAppelsTerminesBetweenTwoIndex(CType(z_strDebut, Long), CType(z_strFin, Long), OutilsExt.ParamAppli.intService, False)

                StockeActes(z_dstDonnees)
            Else
                Dim z_objErreur As New Utilitaires.GestionErreur("Vous n'êtes pas identifié sur le Serveur Web MediCall Concept." & vbCrLf & vbCrLf & "Contacter le service informatique.")
                z_objErreur.Show()
            End If



        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Erreur de transfert")
            _strErreurs &= vbCrLf & vbCrLf & "RecuperationParticuliere" & vbCrLf & vbTab & ex.Message
        End Try
    End Sub

End Class