using System;
using System.Collections;

namespace ImportSosGeneve
{
	public class RubriqueRapport
	{
		public int TypeRapport;
		public int IdCategorie;
		public string Libelle;
		public int Ordre;
		public string Valeur;
		public int Dependance;
		public int Fixe;

		public RubriqueRapport(int TypeRapport,int IdCategorie,string Libelle,int Ordre,int dependance,int fixe)
		{
			this.TypeRapport= TypeRapport;
			this.IdCategorie = IdCategorie;
			this.Libelle = Libelle;
			this.Ordre  =Ordre;
			this.Dependance = dependance;
			this.Fixe = fixe;
		}

		public RubriqueRapport[] ModifieValeurDansRubrique(RubriqueRapport[] tab,int Id,string NouvelleValeur)
		{
			foreach(RubriqueRapport r in tab)
				if(r.IdCategorie==Id)
					r.Valeur = NouvelleValeur;

			return tab;
		}

		public static RubriqueRapport RetrouveRubrique(RubriqueRapport[] tab,int Id)
		{
			foreach(RubriqueRapport r in tab)
				if(r.IdCategorie==Id)
					return r;

			return null;
		}

		public static RubriqueRapport[] RetrouveRubriqueDependantes(RubriqueRapport[] tab,int Id)
		{
			ArrayList tableau = new ArrayList();

			foreach(RubriqueRapport r in tab)
				if(r.Dependance==Id)
					tableau.Add(r);

			return (RubriqueRapport[])tableau.ToArray(typeof(RubriqueRapport));
		}
	}

	public class CategorieDestinataire
	{
		public int Id;
		public string Libelle;

		public CategorieDestinataire(int Id,string Libelle)
		{
			this.Id = Id ;
			this.Libelle = Libelle;
		}
	}
}