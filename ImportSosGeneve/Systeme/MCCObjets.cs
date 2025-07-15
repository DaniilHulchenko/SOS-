using System;
using System.Collections;

namespace ImportSosGeneve
{
	public class Statiques_Data
	{
		public static string[] TabCommune;
		public static Diag1[] TabDiag1;
		public static Diag2[] TabDiag2;
		public static PriseEnCharge[] TabPriseEnCharge;
		public static Devenir[] TabDevenir;
		public static Provenance[] TabProvenance;

		public static ActeGeneve[] TabActeGeneve;
		public static Assurance_SS[] TabAssurance_SS;
		public static MaterielGeneve[] TabMaterielGeneve;
		public static MedicamentGeneve[] TabMedicamentGeneve;
		public static AssuranceGeneve[] TabAssuranceGeneve;
		public static TarifDivers TabTarifsDivers;

		public static Facture_Prestation[] TabPrestations;
		public static Facture_Materiel[] TabMateriel;

	}

	public class Constantes
	{
		public const int MODIF_FICHE=0;
		public const int CREATION_RAPPORT=1;
		public const int MODIF_RAPPORT=2;
		public const int ACCORDE_VISA=3;
		public const int REFUSE_VISA=4;
		public const int MODIF_PATIENT=5;
		public const int MODIF_CONSULT=6;
		public const int SUPP_RAPPORT=7;
		public const int ENVOI_RAPPORT=8;
		public const int DESTINATAIRE_MED = 20;
		public const int DESTINATAIRE_HOSP = 21;
		public const int ENVOIMAIL = 30;
		public const int ENVOIFAX = 31;
		public const int ENVOIAUCUN = 32;
		public const int ENVOICOURIER = 33;
		public const int MODIF_TA = 40;

		public const int CREATION_FACTURE=50;
		public const int MODIFICATION_FACTURE=51;
		public const int SUPPRESSION_FACTURE=52;

	}

	public class Diag1
	{
		public int Code;
		public string Libelle;

		public Diag1(int code,string libelle)
		{
			this.Code  = code;
			this.Libelle = libelle;
		}

		public static Diag1 GetDiag1ByCode(Diag1[] tableau,int code)
		{
			foreach(Diag1 d in tableau)
				if(d.Code == code)
					return d;
			return null;
		}
		public static Diag1 GetDiag1ByLibelle(Diag1[] tableau,string libelle)
		{
			foreach(Diag1 d in tableau)
				if(d.Libelle == libelle)
					return d;
			return null;
		}
		public static Diag1[] GetDiag1Contenant (string contenant)
		{
			if(Statiques_Data.TabDiag1==null) return null;
			else
			{
				if(contenant=="*") return (Diag1[])Statiques_Data.TabDiag1.Clone();

				ArrayList tab =  new ArrayList();
				foreach(Diag1 d in Statiques_Data.TabDiag1)
					if(d.Libelle.ToLower().IndexOf(contenant.ToLower(),0,d.Libelle.Length)>-1)
						tab.Add(d);
				return (Diag1[])tab.ToArray(typeof(Diag1));
			}
		}
	}

	public class Diag2
	{
		public int Diag1_;
		public int Code;
		public string Libelle;

		public Diag2(int diag1, int code,string libelle)
		{
			this.Diag1_ = diag1;
			this.Code  = code;
			this.Libelle = libelle;
		}

		public static Diag2 GetDiag2ByCode(Diag2[] tableau,int code)
		{
			foreach(Diag2 d in tableau)
				if(d.Code == code)
					return d;
			return null;
		}
		public static Diag2 GetDiag2ByLibelle(Diag2[] tableau,string libelle)
		{
			foreach(Diag2 d in tableau)
				if(d.Libelle == libelle)
					return d;
			return null;
		}
		public static Diag2[] GetDiag2ByDiag1 (Diag1 diag1)
		{
			if(diag1==null) return null;
			if(Statiques_Data.TabDiag2==null || Statiques_Data.TabDiag1==null) return null;
			else
			{
				ArrayList tab =  new ArrayList();
				foreach(Diag2 d in Statiques_Data.TabDiag2)
					if(d.Diag1_==diag1.Code)
						tab.Add(d);
				return (Diag2[])tab.ToArray(typeof(Diag2));
			}
		}
		public static Diag2[] GetDiag2Contenant (Diag1 diag1,string contenant)
		{
			if(diag1==null) return null;
			if(Statiques_Data.TabDiag2==null || Statiques_Data.TabDiag1==null) return null;
			else
			{
				if(contenant=="*") return (Diag2[])GetDiag2ByDiag1(diag1).Clone();

				ArrayList tab =  new ArrayList();
				foreach(Diag2 d in Statiques_Data.TabDiag2)
					if(d.Diag1_==diag1.Code && d.Libelle.ToLower().IndexOf(contenant.ToLower(),0,d.Libelle.Length)>-1)
						tab.Add(d);
				return (Diag2[])tab.ToArray(typeof(Diag2));
			}
		}
	}

	public class ModeReglement
	{
		public int Code;
		public string Libelle;

		public ModeReglement(int code,string libelle)
		{
			this.Code  = code;
			this.Libelle = libelle;
		}

		public static ModeReglement GetModeReglementByCode(ModeReglement[] tableau,int code)
		{
			foreach(ModeReglement d in tableau)
				if(d.Code == code)
					return d;
			return null;
		}
		public static ModeReglement GetModeReglementByLibelle(ModeReglement[] tableau,string libelle)
		{
			foreach(ModeReglement d in tableau)
				if(d.Libelle == libelle)
					return d;
			return null;
		}
		public static ModeReglement[] GetModeReglementContenant (string contenant)
		{			
			ArrayList tab =  new ArrayList();
			return (ModeReglement[])tab.ToArray(typeof(ModeReglement));		
		}
	}

	public class PriseEnCharge
	{
		public int Code;
		public string Libelle;

		public PriseEnCharge(int code,string libelle)
		{
			this.Code  = code;
			this.Libelle = libelle;
		}

		public static PriseEnCharge GetPriseEnChargeByCode(PriseEnCharge[] tableau,int code)
		{
			foreach(PriseEnCharge d in tableau)
				if(d.Code == code)
					return d;
			return null;
		}
		public static PriseEnCharge GetPriseEnChargeByLibelle(PriseEnCharge[] tableau,string libelle)
		{
			foreach(PriseEnCharge d in tableau)
				if(d.Libelle == libelle)
					return d;
			return null;
		}
		public static PriseEnCharge[] GetPriseEnChargeContenant (string contenant)
		{
			if(Statiques_Data.TabPriseEnCharge==null) return null;
			else
			{
				if(contenant=="*") return (PriseEnCharge[])Statiques_Data.TabPriseEnCharge.Clone();

				ArrayList tab =  new ArrayList();
				foreach(PriseEnCharge d in Statiques_Data.TabPriseEnCharge)
					if(d.Libelle.ToLower().IndexOf(contenant.ToLower(),0,d.Libelle.Length)>-1)
						tab.Add(d);
				return (PriseEnCharge[])tab.ToArray(typeof(PriseEnCharge));
			}
		}
	}

	public class Devenir
	{
		public int Code;
		public string Libelle;

		public Devenir(int code,string libelle)
		{
			this.Code  = code;
			this.Libelle = libelle;
		}

		public static Devenir GetDevenirByCode(Devenir[] tableau,int code)
		{
			foreach(Devenir d in tableau)
				if(d.Code == code)
					return d;
			return null;
		}
		public static Devenir GetDevenirByLibelle(Devenir[] tableau,string libelle)
		{
			foreach(Devenir d in tableau)
				if(d.Libelle == libelle)
					return d;
			return null;
		}
		public static Devenir[] GetDevenirContenant (string contenant)
		{
			if(Statiques_Data.TabDevenir==null) return null;
			else
			{
				if(contenant=="*") return (Devenir[])Statiques_Data.TabDevenir.Clone();

				ArrayList tab =  new ArrayList();
				foreach(Devenir d in Statiques_Data.TabDevenir)
					if(d.Libelle.ToLower().IndexOf(contenant.ToLower(),0,d.Libelle.Length)>-1)
						tab.Add(d);
				return (Devenir[])tab.ToArray(typeof(Devenir));
			}
		}
	}

	public class Provenance
	{
		public int Code;
		public string Libelle;

		public Provenance(int code,string libelle)
		{
			this.Code  = code;
			this.Libelle = libelle;
		}

		public static Provenance GetProvenanceByCode(Provenance[] tableau,int code)
		{
			foreach(Provenance d in tableau)
				if(d.Code == code)
					return d;
			return null;
		}
		public static Provenance GetProvenanceByLibelle(Provenance[] tableau,string libelle)
		{
			foreach(Provenance d in tableau)
				if(d.Libelle == libelle)
					return d;
			return null;
		}
		public static Provenance[] GetProvenanceContenant (string contenant)
		{
			if(Statiques_Data.TabProvenance==null) return null;
			else
			{
				if(contenant=="*") return (Provenance[])Statiques_Data.TabProvenance.Clone();

				ArrayList tab =  new ArrayList();
				foreach(Provenance d in Statiques_Data.TabProvenance)
					if(d.Libelle.ToLower().IndexOf(contenant.ToLower(),0,d.Libelle.Length)>-1)
						tab.Add(d);
				return (Provenance[])tab.ToArray(typeof(Provenance));
			}
		}
	}	

	public class Dest
	{
		public int CodeDestinataireFacture;
		public CtrlDest.TypeDestinataire m_TypeDestinataire;
		public string AdresseDestinataire;
		public DateTime DateDebut;
		public DateTime DateFin;

		public Dest(){}
	}

}
