using System;
using System.Collections;

namespace ImportSosGeneve
{
	public class Facturation
	{
		public enum Envoi{Privé=1,Tiers=2,Assurance=3};
		public enum TTT{Maladie=1,Accident=2,Examen=3};
		public enum TypeAssurance{Maladie=1,Accident=2};
		public enum Sortie{Aucune=0,Imprimante=1,Xml=2,Email=3}

        public static Hashtable EtatsFacture = new Hashtable();		

		public static void RemplirEtatFacture()
		{
			EtatsFacture = new Hashtable();	
			EtatsFacture.Add(0,"Duplicata");
			EtatsFacture.Add(1,"Modification");
			EtatsFacture.Add(2,"Création");
			EtatsFacture.Add(3,"Annulation");
			EtatsFacture.Add(4,"Ré-édition");
			EtatsFacture.Add(5,"Envoi");
			EtatsFacture.Add(6,"Paiement");
			EtatsFacture.Add(7,"BV Manuel");
			EtatsFacture.Add(9,"Facture 10%");
		}		
	}

	public class Facture_Prestation
	{
		public string NPrestation;
		public string Libelle;
		public float PrestPoints;
		public float PrestPointsT;
		public bool bMajoration;
		public bool bHorsMajoration;

		public Facture_Prestation()
		{
		}
		public Facture_Prestation(string nprestation,string libelle,float prestPoints,float prestPointsT,bool majoration,bool horsmajoration)
		{
			this.NPrestation = nprestation;
			this.Libelle = libelle;
			this.PrestPoints = prestPoints;
			this.PrestPointsT = prestPointsT;
			this.bMajoration = majoration;
			this.bHorsMajoration = horsmajoration;
		}

		public static Facture_Prestation GetFacture_PrestationByNPrestation(Facture_Prestation[] tableau,string NPrestation)
		{
			if(tableau==null) return null;
			for(int i=0;i<tableau.Length;i++)
				if(tableau[i].NPrestation==NPrestation)
					return tableau[i];

			return null;
		}
	}

	public class Facture_Tarifs
	{
		public int intType;
		public string LibelleType;
		public float ValeurPoints;
		public Facturation.Sortie TypeSortie;
		
		public Facture_Tarifs(int type,string libelle,float valeurpoints, int typesortie)
		{
			this.TypeSortie=(Facturation.Sortie)typesortie;
			this.intType = type;
			this.LibelleType=libelle;
			this.ValeurPoints = valeurpoints;
		}
	}

	public class Facture_DocJoint
	{
		public int TypeDoc;
		public string LibelleTypeDoc;		
		
		public Facture_DocJoint(int type,string libelle)
		{
			this.TypeDoc=type;
			this.LibelleTypeDoc = libelle;
			
		}
	}

	public class Facture_Document
	{
		public Facture_DocJoint DocJoint=null;
		public string LibelleTypeDoc;		
		
		public Facture_Document(Facture_DocJoint TypeDoc,string chemin)
		{
			this.DocJoint=TypeDoc;
			this.LibelleTypeDoc = chemin;
			
		}
	}

	public class Facture_Materiel
	{
		public string NMateriel;
		public string Libelle;
		public float Prix;
        public string Num_Materiel;
		
		public Facture_Materiel()
		{
		}
		public Facture_Materiel(string nmateriel,string libelle,float prix, string num_materiel)
		{
			this.NMateriel = nmateriel;
			this.Libelle = libelle;
			this.Prix = prix;
            this.Num_Materiel = num_materiel;
		}

		public static Facture_Materiel GetMateriel(Facture_Materiel[] tableau,string code)
		{
			if(tableau==null) return null;
			foreach(Facture_Materiel mat in tableau)
				if(mat.NMateriel==code)         
					return mat;
			return null;
		}
	}

	public class Facture_PrestationLiee:Facture_Prestation
	{
		public int Quantite=1;
		public string NPrestationLiee;
		public string LibelleLie;
		public int Ordre;
		public int Type;

		public Facture_PrestationLiee(Facture_Prestation prestation, int quantite,string libellelie,int Ordre,int Type)
		{
			this.NPrestation = prestation.NPrestation;
			this.Libelle = prestation.Libelle;
			this.PrestPoints = prestation.PrestPoints;
			this.PrestPointsT = prestation.PrestPointsT;
			this.bMajoration = prestation.bMajoration;
			this.bHorsMajoration = prestation.bHorsMajoration;
			this.Quantite = quantite;
			this.Ordre = Ordre;
			this.Type = Type;
		}
	}
}
