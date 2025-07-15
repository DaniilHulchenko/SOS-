using System;
using System.Collections.Generic;
using System.Text;

namespace SosMedecins.SmartRapport.DAL
{
    public class RequetesUpdate
    {
        public class facture
        {
            public static string Solde = "update facture set Solde = 0 WHERE NFacture = %NFacture";
        }

        public class facture_arrangement
        {
            public static string Nfacture = "Update facture_arrangement Set Commentaire = %Commentaire% Where NFacture = %NFacture%";
        }

        public class facture_status
        {
            //Domi 30/03/2011           
            //public static string Encaissee = "update facture_status set FacDateAcquittee= %FacDateAcquittee%, FacDateEncaissee= %FacDateEncaissee%, FacDate1Rappel = %FacDate1Rappel%, FacDateContentieux = %FacDateContentieux% , FacDateCession = %FacDateCession%, CessionEnvoi = %CessionEnvoi%, CessionRecu = %CessionRecu%, RenvFact10p = %RenvFact10p%, FactFranchise = %FactFranchise%, PatientIndelicat = %PatientIndelicat%, PoursuiteDate = %PoursuiteDate%, LimiteStopRappel = %LimiteStopRappel% WHERE NFacture = %NFacture%";
            public static string Encaissee = "update facture_status set FacDateAcquittee= %FacDateAcquittee%, FacDateEncaissee= %FacDateEncaissee%, FacDate1Rappel = %FacDate1Rappel%, FacDateContentieux = %FacDateContentieux% , FacDateCession = %FacDateCession%, CessionEnvoi = %CessionEnvoi%, CessionRecu = %CessionRecu%, RenvFact10p = %RenvFact10p%, FactFranchise = %FactFranchise%, PatientIndelicat = %PatientIndelicat%, LimiteStopRappel = %LimiteStopRappel% WHERE NFacture = %NFacture%";
            public static string Solde = "update facture set Solde = 0 WHERE NFacture = %NFacture";
        }

        public class Patient_Remarque
        {
            public static string Complet = "UPDATE patient_remarque SET Encaisse = %Encaisse%, Export = %Export%, Cession = %Cession%, DateValidite = %DateValidite%, IdUtilisateur = %IdUtilisateur% WHERE IdPatient = %IdPatient%";
        }

        public class ta_abonnement
        {
            public static string ArchiveTrue = "update ta_abonnement set Archive = 1 WHERE IdAbonnement = %Abonnement";
            public static string ArchiveFalse = "update ta_abonnement set Archive = 0 WHERE IdAbonnement = %Abonnement";
        }

        public class tablemedecin
        {
            public static string Complet = "update tablemedecin set Nom = %Nom, Initiale = %Initiale, Archive = %Archive WHERE CodeIntervenant = %CodeIntervenant";
        }
    }
}
