using System;
using System.IO;

namespace ImportSosGeneve
{
	/// <summary>
	/// Description résumée de Parametrage.
	/// </summary>
	[Serializable() ]
	public class Parametrage
	{
		public string Cache = "\\Cache\\";
		public string db_Type = "MYSQL";
		public string db_Serveur = "192.6.35.125";
		public string db_Base = "basegeneve";
		public string db_User = "pchignon";
		public string db_Pass = "shosoi";
		public string ws_Url =  "https://62.212.121.238/WsGeneve/WsRecup_030.asmx";
		public int intService = 104;
		public string RaisonSociale = "SOS Médecins Genève";
		public string CommuneEditionRapports = "Genève";
		public string strMailEmetteur="christophe@medicallconcept.com";
		public string strMailSmtp = "www.medicallconcept.com";
		public int intMailPort = 25;
		public string CheminXmlFacture = "C:\\Factures\\";
		public string StrReportPrinter = "";
		public string StrInvoicePrinter = "";

		[NonSerialized()]
		private System.Drawing.Font m_PoliceDefault = new System.Drawing.Font("Times New Roman",12,System.Drawing.FontStyle.Regular);
		public System.Drawing.Font PoliceDefault()
		{
			return m_PoliceDefault;
		}

		public Parametrage()
		{			
		}

		public Sauvegarde m_Sauvegarde= new Sauvegarde();
		[NonSerialized()]
		public Utilisateur m_Utilisateur = new Utilisateur();

		public static void SauvegardeParametrage(Parametrage parametrage,string chemin)
		{
			// Serialisation des parametres
			System.Xml.Serialization.XmlSerializer oXMLserialiser = new System.Xml.Serialization.XmlSerializer(typeof(Parametrage));
			System.IO.StreamWriter oSw = new System.IO.StreamWriter(chemin);
			oXMLserialiser.Serialize(oSw, parametrage); 				
			oSw.Close(); 
		}

		public static Parametrage ChargeParametrage(string chemin)
		{
			Parametrage parametrage=null;

			// Récupération du fichier de paramètres
			if(File.Exists(chemin))
			{
				System.Xml.Serialization.XmlSerializer mySerializer = new System.Xml.Serialization.XmlSerializer(typeof(Parametrage));
				System.IO.FileStream myFileStream = new System.IO.FileStream(chemin, System.IO.FileMode.Open);
				parametrage = (Parametrage)mySerializer.Deserialize(myFileStream);
				myFileStream.Close(); 
			}

			return parametrage;
		}
	}

	[Serializable() ]
	public class Sauvegarde
	{
		public Sauvegarde()
		{
		}

		public string RepertoireSource="";
		public string RepertoireDestination="";
		public DateTime DateDerniereSauvegarde;
	}
	
	public class Utilisateur
	{
		public enum CodeDroits{Secretaire=1,Medecin=2,Chef=4,Admin=10,Comptable=5}

		public Utilisateur()
		{
		}

		public string Code;
		public string Pass;
		public string Nom;
		public Utilisateur.CodeDroits Droits;
		public string Initiale;
		public string EMail="";
	}	
}
