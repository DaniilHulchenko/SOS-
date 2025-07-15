using System;
using System.IO;
using System.Data;
using SosMedecins.SmartRapport.EtatsCrystal;

namespace ImportSosGeneve
{	
	// **********************************************************
	// Données partagées actuellement
	// **********************************************************
	public class Donnees
	{
		public static long[] NumFiche;
        public static SosMedecins.SmartRapport.DAL.dstRapport MonDtRapport;
        public static SosMedecins.SmartRapport.DAL.dstAssurances MonAssurance;
        public static SosMedecins.SmartRapport.DAL.dstFacturesEncMed MesFactures;
        public static SosMedecins.SmartRapport.DAL.dstTaFacture MesFactures_TA;
        public static SosMedecins.SmartRapport.DAL.dstStopRappels StopRappelTA;
        public static SosMedecins.SmartRapport.DAL.dstPrestations MesPres;
        public static SosMedecins.SmartRapport.DAL.dstDestination MonDtDestination;
        public static SosMedecins.SmartRapport.DAL.dstCorps MonDtCorps;
        public static SosMedecins.SmartRapport.DAL.dsEtiquettes EtiquetteTA;
        public static SosMedecins.SmartRapport.DAL.GestionTA gestionTA;
		public static Destinataire[] MesDestinataires;
        public static RapportPatient MonEtatRapport;        
        //public static EtatDebiteurs facennmedecins;
        public static EtatDebiteursTrie EtatDebiteursMedecins;  
        public static EtatPrestation Etatprestation;
		public static SansRapport MonSansRapport;
		public static DataSet MonDataSetAppels;	
		public static bool SaveRapport;
		public static RubriqueRapport[] MonCorpsDeRapport;			
	}
	// **********************************************************
	public class BoardClip
	{
		public static int TypeRapport=-1;
        public static SosMedecins.SmartRapport.DAL.dstCorps.CorpsDataTable TableCorps = null;
	}
	// **********************************************************
	// Outils Accessibles de n'importe quelle classe
	// **********************************************************
    public class OutilsExt
    {
        public static Attente.Attente AttentActuelle = null;
        public static MySql OutilsSql = null;
        public static Parametrage ParamAppli = null;
    }
}
