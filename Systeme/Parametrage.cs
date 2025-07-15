using System;
using System.IO;

	/// <summary>
	/// Description résumée de Parametrage.
	/// </summary>
	[Serializable() ]
	public class Parametrage
	{
		public string Cache = @"\Cache\";
        public string db_Type = "SQLSERVER";
		public int intService = 104;
		public string RaisonSociale = "SOS Médecins Genève";
		public string CommuneEditionRapports = "Genève";
        public string strMailAdresse = "smartrapports@sos-medecins.ch";
        public string strMailPassWord = "Med%31416";
        public string strMailSmtp = "mail.sos-medecins.ch";
		public string CheminXmlFacture = @"C:\Factures\ATRAITER\";
		public string CheminSauvXmlFacture = @"\\192.168.0.30\SOS_Public\LINA fichiers transmis MediData\";
		public string StrReportPrinter = "";
        public string StrInvoicePrinter = "Facture_Lina sur SDBSOS";
        public string DocumentScannee = @"\\192.168.0.60\Scans\Scan_2eme_etage\";                             
        
        public string Path_Log_Postfinance = @"\\192.168.0.4\Compta\BVR\logs_SOS\";               
        public string Path_ExpressScribe1 = @"C:\Program Files\NCH Software\Scribe\scribe.exe";   
        public string Path_ExpressScribe2 = @"C:\Program Files (x86)\NCH Software\Scribe\scribe.exe";  
    
        //public string PathFacturation_TA = @"\\DOMSECOND\DocumentsSmartRapport\Facturation_TA\";
        //public string Path_Facturation_TA = @"\\DOMSECOND\DocumentsSmartRapport\Facturation_TA\";
              
        //###################################### Tout activer et remplacer ce qui est dessus##################################
        //Chemin sur le SWSLINUX
		public string Path_carteAVS = @"\\192.168.0.7\DataRegul\CartesAVS\";
        public string Path_Dictee = @"\\192.168.0.7\DataRegul\Dictee\";
        public string Path_Constat = @"\\192.168.0.7\DataRegul\Constat\";

		//Chemin sur le SDBSOS
		public string Path_DocumentsSmartRapport = @"\\192.168.0.8\SRData\DocumentsSmartRapport\";
		public string Dest_Path_carteAVS = @"\\192.168.0.8\SRData\DataRegul\CartesAVS\";
		public string Dest_Path_Dictee = @"\\192.168.0.8\SRData\DataRegul\Dictee\";
		public string Dest_Path_Constat = @"\\192.168.0.8\SRData\DocumentsSmartRapport\";
		public string Path_DocFacture = @"\\192.168.0.8\SRData\DocumentsSmartRapport\Factures\";
		public string Path_Facturation = @"\\192.168.0.8\SRData\DocumentsSmartRapport\Facturation\";

		//Paramètres Médidata Prod
		public string X_CLIENT_ID_SOS = "1000003361";
		public string Authorization_SOS ="Basic S1hKemk5MmlnQWd5SmZicC45VHhzUlBUZkVBMDIwNkNf";
		
		//Pour Tests
		//public string X_CLIENT_ID_SOS = "1000001720";
		//public string Authorization_SOS = "Basic YURiVHpYdU5yZndxTFcwVjplT09VNFJKTm1IUENoanVR";
	
		public string Path_FileXMLerror = @"C:\Factures\ATRAITER\";
		public string Path_PDFfactureImpayees = @"C:\Factures\Impayees\PDF\";
		public string Path_XMLfactureImpayees = @"C:\Factures\Impayees\XML\";


	//###############################################################################################################

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

		public static void SauvegardeParametrage(Parametrage parametrage,string chemin)
		{
			// Serialisation des parametres
			System.Xml.Serialization.XmlSerializer oXMLserialiser = new System.Xml.Serialization.XmlSerializer(typeof(Parametrage));
			System.IO.StreamWriter oSw = new System.IO.StreamWriter(chemin);
			oXMLserialiser.Serialize(oSw, parametrage); 				
			oSw.Close(); 
		}


        //public Utilisateur m_Utilisateur = new Utilisateur();
        
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