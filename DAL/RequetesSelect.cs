using System;
using System.Collections.Generic;
using System.Text;

namespace SosMedecins.SmartRapport.DAL
{
    public class RequetesSelect
    {
        public class facture
        {
            public static string MaxNfacture = "SELECT Max(Nfacture) FROM facture";

            //*********************Domi 30/03/2011 Mdif le 29.07.2019
            //public static string Tout = "SELECT f.NFacture, c.NConsultation, co.CodeAppel, (pe.nom+' '+pe.prenom) as 'NomPersonne', f.DateCreation, fs.DateImpression, f.TypeEnvoi, f.Tarif, f.TTT, f.TypeAssurance, f.TypeSortie, f.NAccident, f.DateAccident, f.RefPatient, f.FlagConcerne, f.Commentaire, f.TotalFacture, f.Solde, f.TypeDestinataire, f.CodeDestinataire,f.AdresseDestinataire,f.AdresseDestinataire2, f.FactNum_AVS, f.Num_Session ,pe.Num_Assure, pe.Num_AVS, TypeDocJoint,UrlDocJoint,fs.FacDateAnnulee,fs.FacDateAcquittee,fs.FacDate1Rappel,fs.FacDate2Rappel,fs.FacDateRappel10,fs.FacDateContentieux,fs.FacDateEncaissee,fs.FacDateDuplicata,fs.FacDateEnvoyee, fs.FacDateImpression10, fs.FacDateCession, fs.CessionEnvoi, fs.CessionRecu, fs.RenvFact10p, fs.FactFranchise, fs.PatientIndelicat, fs.PoursuiteDate FROM facture f inner join facture_status fs on fs.NFacture = f.NFacture left join factureconsultation c on c.NFacture = f.NFacture inner join tableconsultations co on co.NConsultation = c.NConsultation inner join tablepatient pa on pa.IdPatient = co.IndicePatient inner join tablepersonne pe on pe.IdPersonne = pa.IdPersonne WHERE f.NFacture = %NFacture";
            public static string Tout = "SELECT f.NFacture, c.NConsultation, co.CodeAppel, (pe.nom+' '+pe.prenom) as 'NomPersonne', f.DateCreation, fs.DateImpression," +
                                        " f.TypeEnvoi, f.Tarif, f.TTT, f.TypeAssurance, f.TypeSortie, f.NAccident, f.DateAccident, f.RefPatient, f.FlagConcerne, f.Commentaire," +
                                        " f.TotalFacture, f.Solde, f.TypeDestinataire, f.CodeDestinataire,f.AdresseDestinataire,f.AdresseDestinataire2, f.FactNum_AVS," +
                                        " f.Num_Session ,pe.Num_Assure, pe.Num_AVS, TypeDocJoint,UrlDocJoint,fs.FacDateAnnulee,fs.FacDateAcquittee,fs.FacDate1Rappel," +
                                        " fs.FacDate2Rappel,fs.FacDate3Rappel,fs.FacDateContentieux,fs.FacDateEncaissee,fs.FacDateDuplicata,fs.FacDateEnvoyee," +
                                        " fs.FacDateImpression10, fs.FacDateCession, fs.CessionEnvoi, fs.CessionRecu, fs.RenvFact10p, fs.FactFranchise, fs.PatientIndelicat," +
                                        " fs.PoursuiteDate, fs.LimiteStopRappel " +
                                        " FROM facture f inner join facture_status fs on fs.NFacture = f.NFacture" +
                                                       " left join factureconsultation c on c.NFacture = f.NFacture " +
                                                       " inner join tableconsultations co on co.NConsultation = c.NConsultation " +
                                                       " inner join tablepatient pa on pa.IdPatient = co.IndicePatient " +
                                                       " inner join tablepersonne pe on pe.IdPersonne = pa.IdPersonne " +
                                                       " WHERE f.NFacture = %NFacture";

            //**********************************
            public static string MinNfactureNonEnvoye = "SELECT min(f.NFacture) FROM facture f inner join facture_status s on f.NFacture = s.NFacture WHERE s.FacDateEnvoyee is null";
            public static string MaxNfactureNonEnvoye = "SELECT max(f.NFacture) FROM facture f inner join facture_status s on f.NFacture = s.NFacture WHERE s.FacDateEnvoyee is null";
        }

        public class facture_arrangement
        {
            public static string Nfacture = "Select facture_arrangement.*, Nom from facture_arrangement , tableutilisateur where facture_arrangement.CodeUtilisateur = tableutilisateur.CodeUtilisateur And NFacture = %NFacture% ";
        }

        public class factureconsultation
        {
            public static string Nfacture = "SELECT NFacture from factureconsultation where NConsultation = %NConsultation Order By NFacture DESC";
            public static string NConsultationPrincipale = "SELECT NConsultation from factureconsultation WHERE NFacture = %NFacture AND Principale = 1";
            public static string NConsultation = "SELECT NConsultation from factureconsultation WHERE NFacture = %NFacture";
            public static string NFactureParDate = "SELECT facture.NFacture, facture_status.FacDateEnvoyee, tableactes.CodeIntervenant, tablemedecin.Nom, facture_status.FacDateAnnulee, facture.TotalFacture, facture.DateCreation, facture_status.FacDateAcquittee, facture_etats.Etat, facture_etats.Montant as Montant FROM factureconsultation INNER JOIN facture_status ON factureconsultation.NFacture = facture_status.NFacture INNER JOIN facture_etats ON facture_status.NFacture = facture_etats.NFacture INNER JOIN facture ON facture_etats.NFacture = facture.NFacture INNER JOIN tableconsultations ON factureconsultation.NConsultation = tableconsultations.NConsultation INNER JOIN tableactes ON tableconsultations.CodeAppel = tableactes.Num INNER JOIN tablemedecin ON tableactes.CodeIntervenant = tablemedecin.CodeIntervenant GROUP BY facture.NFacture, facture_status.FacDateEnvoyee, factureconsultation.NConsultation, tableactes.Num, tableactes.CodeIntervenant, tablemedecin.Nom, tableactes.IndicePatient, facture.Tarif, facture_status.FacDateAnnulee, facture.TotalFacture, facture.DateCreation, facture_status.FacDateAcquittee, facture_etats.Etat, facture_etats.Montant HAVING facture.NFacture>44 AND facture_status.FacDateEnvoyee Is Not Null AND tableactes.CodeIntervenant<>2536 AND facture_status.FacDateAnnulee Is Null AND facture.TotalFacture>0 AND facture_status.FacDateAcquittee >'%FacDateAcquitteeDebut' AND facture_status.FacDateAcquittee <'%FacDateAcquitteeFin' AND facture_etats.Etat=6 ORDER BY tableactes.CodeIntervenant";
        }

        public class fac_tablemateriel
        {
            public static string MatLibelle = "SELECT MatLibelle FROM fac_tablemateriel WHERE Nt_Materiel = '%Nt_Materiel'";
        }

        public class fac_prestations
        {            
            public static string NPrestation = "SELECT PrestLibelle, PrestPointM, PrestPointT, PrestMajor, PrestHorsMajor FROM Tarmed where NPrestation = '%NPrestation' AND TarmedVersion = '%TarmedVersion'";
        }

        public class facture_moyen
        {
            public static string Complet = "SELECT Code, Libelle FROM facture_moyen ORDER BY Ordre ASC";
        }

        public class facture_prest
        {
            public static string NFacture_fac_tarif = @"SELECT f.NFacture, f.TypePrest, f.Indice, f.Qte, f.Prix, f.Cote, t.libelle,
                                                                 fm.Num_Materiel  
                                                        FROM facture_prest f LEFT JOIN fac_tarif t ON f.TypeTarif = t.id
                                                                             LEFT JOIN fac_tablemateriel fm ON fm.Nt_materiel = f.Indice 
                                                        WHERE NFacture = %NFacture ORDER BY Ordre ASC";
        }

        public class facture_type
        {
            public static string Complet = "SELECT Etat, Libelle FROM facture_type ORDER BY Ordre ASC";
        }
        
        public class Patient_Remarque
        {
            public static string IdPatient = "Select * from Patient_Remarque Where idpatient = %IdPatient%";
        }

        public class ta_abonnement
        {
            public static string IdAbonnement = "SELECT IdPatient FROM ta_abonnement WHERE ta_abonnement.IdAbonnement = '%IdAbonnement'";
            public static string Export = "SELECT IdPatient, IdAbonnement  FROM ta_abonnement WHERE ExportMcc = 0 AND  Archive = 0 AND Export = 1";
        }

        public class ta_abonnementcle
        {
            public static string idAbonnement = "select top(1) NumeroCle,Commentaire from ta_abonnementcle where idabonnement = %IdAbonnement% order by dateattribution desc";
        }

        public class ta_abonnementcontacts
        {
            public static string IdAbonnementFaux = "SELECT * from ta_abonnementcontacts WHERE IdAbonnement = -1";
        }

        public class ta_abonnementdossier
        {
            public static string IdAbonnement = "select TD_Traitements,TD_PbMedicaux from ta_abonnementdossier where idabonnement = %IdAbonnement%";
        }

        public class ta_abonnementjournal
        {
            public static string MaxIdAbonnement = "SELECT max(id) FROM ta_abonnementjournal WHERE idAbonnement = %IdAbonnement";
        }

        public class ta_abonnementurgence
        {
            public static string IdAbonnementFaux = "SELECT * FROM ta_abonnementurgence WHERE IdAbonnement = -1";
            public static string IdAbonnement = "SELECT Nom,Prenom,Telephone, Tel2, Tel3 FROM ta_abonnementurgence WHERE idabonnement = %IdAbonnement% ORDER BY Nom ASC";
        }

        public class ta_Factures
        {
            public static string IdAbonnementFaux = "SELECT * FROM ta_Factures WHERE IdAbonnement = -1";
            public static string NFacture = "SELECT * FROM ta_Factures WHERE NFacture = '%NFacture'";
            public static string NFacture_Idabonnement = "SELECT * FROM ta_Factures WHERE NFacture = '%NFacture' and Idabonnement = '%Idabonnement'";
            public static string NFacture_NTA = "SELECT * FROM ta_Factures WHERE NFacture = '%NFacture' and NTA = '%NTA'";
        }

        public class tableactes
        {
            public static string Num = "SELECT Num FROM tableactes WHERE Num = %Num";
        }

        public class tableconsultations
        {
            public static string NConsultation = "SELECT NConsultation FROM tableconsultations WHERE NConsultation = %NConsultation";
        }

        public class tablemedecin
        {
            public static string Order_Nom = "SELECT CodeIntervenant, Nom, Desactive FROM tablemedecin WHERE Desactive = 0 and CodeIntervenant <> 2536 and CodeIntervenant <> 1160 order by Nom";
            public static string ArchiveFaux = "SELECT CodeIntervenant,NomGeneve,PrenomGeneve,Independant FROM tablemedecin Where Archive = 0";
            public static string CodeIntervenant = "SELECT * FROM tablemedecin WHERE CodeIntervenant = %CodeIntervenant";
        }

        public class tablemodifications
        {
            public static string NRapportType = "SELECT  TOP(1) u.Nom,m.DateModif from tablemodifications m inner join tableutilisateur u on u.CodeUtilisateur=m.CodeUtilisateur inner join tablerapports rr on rr.NConsultation = m.NConsultation WHERE Type = %Type% and NRapport = %NRapport% order by id desc";
        }

        public class tableoperation
        {
            public static string DerniereConnexion = "SELECT TOP(1) NumAppelDepart, NumAppelFin FROM tableoperation order by numappelfin desc, DerniereConnexion desc";
            public static string NumAppelFin = "SELECT TOP(1) numappelfin FROM tableoperation ORDER BY numappelfin desc, derniereconnexion desc";
        }

        public class tablepatient
        {
            public static string IdPersonne = "SELECT IdPersonne FROM tablepatient WHERE IdPatient = %IdPatient";
        }

        //public class tablepatientabonne
        //{
        //    public static string IndicePatientTypeAbonnement = "SELECT TOP(1) TexteAbonnement from tablepatientabonne where IndicePatient = %IndicePatient% AND TypeAbonnement = '%TypeAbonnement%' AND Approuve = 1 AND Archive = 0 ORDER BY DateTexte DESC";
        //}

        public class tablepersonne
        {
            public static string IdPersonne = "SELECT IdPersonne FROM tablepersonne WHERE IdPersonne = %IdPersonne";
        }

        public class tableprovenance
        {
            public static string Order_Libelle = "SELECT Code,Libelle FROM tableprovenance ORDER BY Libelle asc";
        }

        public class tablerapports
        {
            public static string Vise = "SELECT * from tablerapports WHERE Vise = 1 and NRapport = %NRapport%";
        }

        public class tableutilisateur
        {
            public static string logon = "SELECT * FROM tableutilisateur WHERE CodeUtilisateur = %1 AND Pass = %2";
        }
    }
}
