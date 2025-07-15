using System;
using System.Collections.Generic;
using System.Text;

namespace SosMedecins.SmartRapport.DAL
{
    public class RequetesInsert
    {
        public class facture
        {
            public static string Vide = "INSERT INTO facture (NFacture, DateCreation) values ( %NFacture, '%DateCreation')";
        }

        public class facture_arrangement
        {
            public static string Complet = "INSERT INTO facture_arrangement ( NFacture , DateCreation , CodeUtilisateur , Commentaire ) VALUES ( %NFacture% , %DateCreation% , %CodeUtilisateur% , %Commentaire% ) ";
        }

        public class factureconsultation
        {
            public static string Complet = "INSERT INTO factureconsultation (NFacture, NConsultation, Principale) values ( %NFacture, %NConsultation, 1 )";
        }

        public class facture_status
        {
            public static string Vide = "INSERT INTO facture_status (NFacture) VALUES (%Max)";
        }

        public class Patient_Remarque
        {
            public static string Complet = "INSERT INTO patient_remarque (IdPatient, Encaisse, Export, Cession, DateValidite, IdUtilisateur ) VALUES (%IdPatient%, %Encaisse%, %Export%, %Cession%, %DateValidite%, %IdUtilisateur%)";
        }

        public class tablemedecin
        {
            public static string Complet = "INSERT INTO tablemedecin (CodeIntervenant, Nom, Initiale, Service, Archive, Mail, NomGeneve, PrenomGeneve) values (%CodeIntervenant, %Nom, %Initiale, %Service, %Archive, %Mail, %NoGeneve, %PrenomGeneve)";
        }

        public class tableoperation
        {
            public static string Complet = "INSERT INTO tableoperation (DerniereConnexion, NumAppelDepart, NumAppelFin) values (%DateOp, '%NumFicheMin', '%NumFicheMax')";

        }

    }
}
