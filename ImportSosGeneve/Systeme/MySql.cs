using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using SosMedecins.SmartRapport.DAL;
using SosMedecins.SmartRapport.GestionApplication;
using SosMedecins.Connexion;
using System.Configuration;
using System.Data.SqlClient;

namespace ImportSosGeneve
{
	/// <summary>
	/// Description résumée de MySql.
	/// </summary>
	public class MySql
	{
		//private OdbcConnection Cn=null;
		private SqlConnection Cn = null;
		private string m_strLogFile="";
		private int premierRappelAss = 45;   //Pour les Assurances (TP, Type_envoi = 3) 1er rappel à 45 jours (changé de 45 à 60 jour le 21.12.2021 reppassé à 45 le 13.07.2022)
		private int premierRappelTG = 60;   //1er rappel à 60 jours (TG, Type envoi = 1 ou 2)
		private int deuxiemeRappel = 30;    //2eme à 30 jours 
		private int troixiemeRappel = 30;   //3eme à 30 jours (seulement pour les ass TP, Type_envoi = 3)

		#region Construction / Destruction de la classe

		public MySql(Parametrage param,string LogFile)
		{	
			this.Cn = new SqlConnection();

			//Si on est en Debug on se connect sur la base test
//#if DEBUG
//			SosMedecins.SmartRapport.DAL.Variables.InfoConnexion.NomServeur = @"192.168.0.8";

//			//Origine			
//			this.Cn.ConnectionString = "Data Source=" + SosMedecins.SmartRapport.DAL.Variables.InfoConnexion.NomServeur + ";Initial Catalog=" + SosMedecins.SmartRapport.DAL.Variables.InfoConnexion.BaseDonnees + ";Persist Security Info=true; User ID=" + SosMedecins.SmartRapport.DAL.Variables.InfoConnexion.Utilisateur + ";Password=" + SosMedecins.SmartRapport.DAL.Variables.InfoConnexion.MotDePasse + ";";
//#else
//			this.Cn.ConnectionString = "Data Source=" + SosMedecins.SmartRapport.DAL.Variables.InfoConnexion.NomServeur + ";Initial Catalog=" + SosMedecins.SmartRapport.DAL.Variables.InfoConnexion.BaseDonnees + ";Persist Security Info=true; User ID=" + SosMedecins.SmartRapport.DAL.Variables.InfoConnexion.Utilisateur + ";Password=" + SosMedecins.SmartRapport.DAL.Variables.InfoConnexion.MotDePasse + ";";
           
			this.m_strLogFile = LogFile;
			try
			{
				if(System.IO.File.Exists(m_strLogFile))
					System.IO.File.Delete(m_strLogFile);
			}
			catch (Exception Erreur)
			{
			  MessageBox.Show("Erreur :" + Erreur.Message);
            }

//#endif

		}

		public void Dispose()
		{
			if(this.Cn.State==ConnectionState.Open)
				Cn.Close();
			this.Cn.Dispose();
			this.Cn=null;
		}

//#endregion

#region Ouverture / Fermeture de la base de données

		public bool OuvertureBase()
		{
			try
			{
				if(this.Cn.State==System.Data.ConnectionState.Closed)
				{
					this.Cn.Open();
					return true;
				}
				else
					return false;
			}
			catch(Exception ex)
			{
				LogErreur(DateTime.Now,"Ouverture base",ex.Message);
				return false;
			}
		}

		private void LogErreur(DateTime dt,string Procedure,string ErrorMessage)
		{
			System.IO.StreamWriter writer =  System.IO.File.AppendText(m_strLogFile);
			writer.WriteLine(dt.ToString() + "\t" + Procedure + "\t" + ErrorMessage);
			writer.Close();
		}

		public void VerificationStateSql()
		{
			if(this.Cn.State!=ConnectionState.Open) 
			{
				Cn.Close();
				Cn.Open();
			}
		}

		public bool FermetureBase()
		{
			if(this.Cn.State!=System.Data.ConnectionState.Closed)
			{
				this.Cn.Close();
				return true;
			}
			else
				return false;
		}

#endregion

#region Méthodes primitives de Requetes Sql

		public bool ExecuteCommandeSansRetour(string Requete)
		{
			VerificationStateSql();
			
			SqlCommand commande = new SqlCommand(Requete, Cn);
			commande.ExecuteNonQuery();
			return true;				
		}

        public object ExecuteScalar(String p_strSql)
        {
            object z_strValeurRetour  = null;
           
			SqlCommand commande = new SqlCommand(p_strSql, Cn);
			z_strValeurRetour = commande.ExecuteScalar();

            return z_strValeurRetour;
        }

		public DataSet ExecuteCommandeAvecDataSet(string Requete)
		{
			DataSet m_DataSet = new DataSet();
			VerificationStateSql();
			
			SqlCommand commande = new SqlCommand(Requete, Cn);
			DataTable dt = m_DataSet.Tables.Add();
			
			SqlDataReader reader = commande.ExecuteReader();

			for (int i=0;i<reader.FieldCount;i++)
				if(reader.GetName(i).ToLower()=="daterapport" )
					dt.Columns.Add(new DataColumn(reader.GetName(i),typeof(DateTime)));
				else if((reader.GetName(i)=="NbAppel"))
					dt.Columns.Add(new DataColumn(reader.GetName(i),typeof(int)));
				else
					dt.Columns.Add(new DataColumn(reader.GetName(i),typeof(string)));
			while(reader.Read())
			{
				DataRow row = dt.NewRow();
				for(int i=0;i<reader.FieldCount;i++)
				{
					try
					{
						if(!reader[i].ToString().Equals(System.DBNull.Value) && !reader[i].ToString().Equals(""))
							row[i] = reader[i].ToString();
						else
						{
							if(reader.GetName(i).ToLower()=="modifie" || reader.GetName(i).ToLower()=="rapportgenere" || reader.GetName(i).ToLower()=="facturegeneree")
								row[i]=0;
							else
                                row[i] = DBNull.Value; 
                                //row[i] = ""; 
						}
					}
					catch
					{
                        row[i] = DBNull.Value; 
                        //row[i]="";
					}
				}
				dt.Rows.Add(row);
			}

            reader.Close();
			reader=null;
			return m_DataSet;				
		}

		private DataTable ExecuteCommandeAvecDataTable(string Requete)
		{
			VerificationStateSql();			
			SqlCommand commande = new SqlCommand(Requete, Cn);

			DataTable dt = new DataTable();		
			SqlDataReader reader = commande.ExecuteReader();

			for (int i=0;i<reader.FieldCount;i++)
				if(reader.GetName(i).ToLower()=="daterapport")
					dt.Columns.Add(new DataColumn(reader.GetName(i),typeof(DateTime)));
				else
					dt.Columns.Add(new DataColumn(reader.GetName(i),typeof(string)));
			while(reader.Read())
			{
				DataRow row = dt.NewRow();
				for(int i=0;i<reader.FieldCount;i++)
				{
					try
					{
						if(!reader[i].ToString().Equals(System.DBNull.Value) && !reader[i].ToString().Equals(""))
						{
							row[i] = reader[i].ToString().Replace("\r\n","|¤").Replace("\n","|¤").Replace("|¤","\r\n");
						}
						else
                            row[i] = DBNull.Value; 
                            //row[i] = "";
					}
					catch
					{
                        row[i] = DBNull.Value; 
                        //row[i]="";
				    }
				}
				dt.Rows.Add(row);
			}

			reader.Close();
			reader=null;	
			return dt;
		}

		public string[][] ExecuteCommandeAvecTabString(string Requete)
		{
			ArrayList tab = new ArrayList();
			VerificationStateSql();			

			SqlCommand commande = new SqlCommand(Requete, Cn);
			SqlDataReader reader = commande.ExecuteReader();

			while (reader.Read())
			{
				string[] ligne = new string[reader.FieldCount];
				
				for(int i=0;i<reader.FieldCount;i++)
				{
					try
					{
						if(!reader[i].ToString().Equals(System.DBNull.Value) && !reader[i].ToString().Equals(""))
							ligne[i]= reader[i].ToString();
						else
                            ligne[i] = DBNull.Value.ToString(); 
                            //ligne[i]= "";
					}
					catch
					{
                        ligne[i] = DBNull.Value.ToString(); 
                        //ligne[i]="";
					}
				}
				tab.Add(ligne);
			}

			reader.Close();
			reader=null;
			return (string[][])tab.ToArray(typeof(string[]));				
		}	
        
        //TA
        public void RemplitDataTableTA(DataTable table, string Requete)
        {
            int j = 0;
            int i = 0;
            try
            {				
				SqlDataAdapter myAdapter = new SqlDataAdapter(Requete, Cn);

				myAdapter.Fill(table);
                for (j = 0; j < table.Rows.Count; j++)
                    for (i = 0; i < table.Rows[j].ItemArray.Length; i++)
                    {
                        try
                        {
                            if (table.Rows[j][i] == System.DBNull.Value)
                                if ((i != 21) || (i != 22) || (i != 23) || (i != 25) || (i != 28) || (i != 29) || (i != 33))                                
                                table.Rows[j][i] = DBNull.Value;
                        }
                        catch
                        {
                            table.Rows[j][i] = DBNull.Value;
                        }
                    }
            }
            catch (Exception ex)
            {
                // Erreur ignorée a reprendre
                MessageBox.Show("!! Erreur dans un champ de la table patient : " + ex.Message);                
            }
        }
		
		public void RemplitDataTable(DataTable table,string Requete)
		{
			//int j=0;
			//int i=0;
            try
            {
				SqlDataAdapter myAdapter = new SqlDataAdapter(Requete, Cn);
				
				myAdapter.Fill(table);

				for (int j = 0; j < table.Rows.Count; j++)
				{
					DataRow row = table.Rows[j];
					for (int i = 0; i < row.ItemArray.Length; i++)
					{
						object value = row[i];
						if (Convert.IsDBNull(value))
						{
							if (i != row.Table.Columns["DAP"].Ordinal && i != row.Table.Columns["DateEnvoi"].Ordinal)
							{
								row[i] = "";
							}
						}
					}
				}
			
			}
            catch (Exception ex)
            {
                // Erreur ignorée a reprendre
               // MessageBox.Show("!! Erreur dans un champ de la table: " + ex.Message);
                Console.WriteLine("Erreur dans un champ de la table: "+ ex.Message);
            }
		}

		//public OdbcDataReader getEnregistrement(string requete)
		public SqlDataReader getEnregistrement(string requete)
		{			
			SqlCommand myCommand = new SqlCommand(requete, Cn);			
			return myCommand.ExecuteReader();
		}
		
		public string DateFormatMySql(DateTime date)
		{
            string z_strRetour = "NULL";
            if (date != null) 
            {
                z_strRetour = date.ToString("yyyyMMdd HH:mm:ss"); 
            }
            return z_strRetour;
        }

		public bool Sql_ValueInChampsOfTable(string TableName,string Champs,string Valeur)
		{
			bool retour = true;

			string Requete = "SELECT * from " + TableName + " Where " + Champs + " = '" + Valeur.Replace("'","''") + "'" ;
			
			SqlCommand commande = new SqlCommand(Requete, Cn);
			SqlDataReader reader = commande.ExecuteReader();

			retour = false;
			if(reader.Read())
				retour = true;
			else
				retour = false;
			reader.Close();
			reader=null;
			return retour;
		}
#endregion

#region Importation des Données MediCall Concept

		public bool InsertFactu(DataSet ds)
		{
			string Requete = "";
			if(ds==null)
				return false;

			for(int i=1;i<ds.Tables.Count;i++)
			{
				for(int m=0;m<ds.Tables[i].Rows.Count;m++)
				{
					long NFacture = long.Parse(ds.Tables[i].Rows[m]["NFacture"].ToString());

					Requete = "INSERT INTO " + ds.Tables[i].TableName + " " ;
					string champs = "(";
					string values= " values (";					
					
					for(int j=0;j<ds.Tables[i].Columns.Count;j++)
					{
						champs+=ds.Tables[i].Columns[j].ColumnName.Replace(" ","") + ",";
						values+="'" + ds.Tables[i].Rows[m].ItemArray[j].ToString().Replace("'","''") + "',";
					}				
					
					if(champs.Length>0) champs = champs.Remove(champs.Length-1,1) + ")";
					if(values.Length>0) values = values.Remove(values.Length-1,1) + ")";
					
					if(!Sql_ValueInChampsOfTable(ds.Tables[i].TableName,"NFacture",NFacture.ToString()))
					{
						ExecuteCommandeSansRetour(Requete + champs  + values);
					}						
				}
			}

			return true;
		}
		#endregion

		#region Extraction des données statiques dans les tables
        public string [][] CodeIntervenant(string Nom)
        {
            string[][] result = this.ExecuteCommandeAvecTabString("SELECT CodeIntervenant,Mail from tablemedecin WHERE Nom = " +"'"+ Nom +"'" );
            
            return result;
        }

		public string[][] MailMedecinCodeIntervenant(int CodeMedecin)
		{
			string[][] result = this.ExecuteCommandeAvecTabString("SELECT CodeIntervenant,Mail from tablemedecin WHERE CodeIntervenant = " + "'" + CodeMedecin + "'");

			return result;
		}

		public string[][] ListeMedecins()
		{
			string ClauseWhere = "";
            if (VariablesApplicatives.Utilisateurs.Droits == VariablesApplicatives.Utilisateurs.CodeDroits.Medecin) ClauseWhere = " AND CodeIntervenant = " + VariablesApplicatives.Utilisateurs.Identifiant; 
			string[][] result = this.ExecuteCommandeAvecTabString("SELECT CodeIntervenant,Nom from tablemedecin WHERE tablemedecin.Desactive = 0" + ClauseWhere + " order by Nom");
			return result;
		}
        public string[][] ListeMedecinsSOS()
        {
            string ClauseWhere = "";
            if (VariablesApplicatives.Utilisateurs.Droits == VariablesApplicatives.Utilisateurs.CodeDroits.Medecin) ClauseWhere = " AND CodeIntervenant = " + VariablesApplicatives.Utilisateurs.Identifiant;
            string[][] result = this.ExecuteCommandeAvecTabString("SELECT Nom, CodeIntervenant from tablemedecin WHERE tablemedecin.Desactive = 0" + ClauseWhere + " order by Nom");
            string[] retour = new string[result.Length];
            //for (int i = 0; i < result.Length; i++)
            //retour[i]= result[i][0];
            //return retour;
            return result;
        }
		public DataSet ListeMedVille(string contenant, string PrenomMed, bool actif)
		{
			//Est ce un medecin actif ? (Pas en retraite)
			int MedActif = 0;
			
			if (actif == true)
				MedActif = 1;
		
			DataSet dt = this.ExecuteCommandeAvecDataSet("SELECT * FROM medecinsville WHERE Active = " + MedActif + " AND nom LIKE '" + contenant.Replace("'", "''") + "%' AND prenom LIKE '" + PrenomMed.Replace("'", "''") + "%' OR destinataire LIKE '" + contenant.Replace("'", "''") + "%' ORDER BY nom");
			return dt;
		}
		public string[] GetMedecinTTT(int IdMedecin)
		{
			string[][] retour = this.ExecuteCommandeAvecTabString("SELECT Nom,Prenom from medecinsville where Num = " + IdMedecin );
			if(retour!=null && retour.Length==1)
				return retour[0];
			else
				return null;
		}
		public DataSet HotelPolice()
		{
			DataSet dt = this.ExecuteCommandeAvecDataSet("SELECT * from commissariat");
			return dt;
		}

		public string[] ListeMotifs()
		{			
			string[][] result = this.ExecuteCommandeAvecTabString("SELECT distinct motif1 from tableactes order by motif1");
			string[] retour = new string[result.Length];
			for(int i=0;i<result.Length;i++)
				retour[i]=result[i][0];
			return retour;
		} 

		public Diag1[] ListeDiag1()
		{
			string[][] result = this.ExecuteCommandeAvecTabString("SELECT IdDiag1,Libelle from tablediag1 order by Libelle");
			Diag1[] retour = new Diag1[result.Length];
			for(int i=0;i<result.Length;i++)
			{
				retour[i] = new Diag1(int.Parse(result[i][0]),result[i][1]);
			}
			return retour;
		}

		public string[] ListeCommunes()
		{
			string[][] result = this.ExecuteCommandeAvecTabString("SELECT Distinct Commune from tablepersonne order by Commune asc");
			string[] retour = new string[result.Length];
			for(int i=0;i<result.Length;i++)
			{
				retour[i] = result[i][0];
			}
			return retour;
		}

		public string[] ListeOrigine()
		{
			string[][] result = this.ExecuteCommandeAvecTabString("SELECT distinct OrigineAppel from tableactes order by OrigineAppel");
			string[] retour = new string[result.Length];
			for(int i=0;i<result.Length;i++)
				retour[i]=result[i][0];
			return retour;
		}

		public Diag2[] ListeDiag2()
		{
			string[][] result = this.ExecuteCommandeAvecTabString("SELECT IdDiag1,IdDiag2,Libelle from tablediag2 order by IdDiag1 asc,Libelle asc");
			Diag2[] retour = new Diag2[result.Length];
			for(int i=0;i<result.Length;i++)
			{
				retour[i] = new Diag2(int.Parse(result[i][0]),int.Parse(result[i][1]),result[i][2]);
			}
			return retour;
		}

		public ModeReglement[] ListeModeReglement()
		{
			string[][] result = this.ExecuteCommandeAvecTabString("SELECT Code,Libelle from tablereglements order by Libelle asc");
			ModeReglement[] retour = new ModeReglement[result.Length];
			for(int i=0;i<result.Length;i++)
			{
				retour[i] = new ModeReglement(int.Parse(result[i][0]),result[i][1]);
			}
			return retour;
		}

		public PriseEnCharge[] ListePriseEnCharge()
		{
			string[][] result = this.ExecuteCommandeAvecTabString("SELECT Code,Libelle from tablepriseenchargepatient order by Libelle asc");
			PriseEnCharge[] retour = new PriseEnCharge[result.Length];
			for(int i=0;i<result.Length;i++)
			{
				retour[i] = new PriseEnCharge(int.Parse(result[i][0]),result[i][1]);
			}
			return retour;
		}

		public Devenir[] ListeDevenir()
		{
			string[][] result = this.ExecuteCommandeAvecTabString("SELECT Code,Libelle from tabledevenirpatient order by Libelle asc");
			Devenir[] retour = new Devenir[result.Length];
			for(int i=0;i<result.Length;i++)
			{
				retour[i] = new Devenir(int.Parse(result[i][0]),result[i][1]);
			}
			return retour;
		}

		public Provenance[] ListeProvenance()
		{
            string[][] result = this.ExecuteCommandeAvecTabString(RequetesSelect.tableprovenance.Order_Libelle);
            
            Provenance[] retour = new Provenance[result.Length];
			for(int i=0;i<result.Length;i++)
			{
				retour[i] = new Provenance(int.Parse(result[i][0]),result[i][1]);
			}
			return retour;
		}

		public ActeGeneve[] ListeActesGeneve()
		{
			string[][] result = this.ExecuteCommandeAvecTabString("SELECT IdActe,LibelleActe,Tarif from actes order by LibelleActe asc");
			ActeGeneve[] retour = new ActeGeneve[result.Length];
			for(int i=0;i<result.Length;i++)
			{
				retour[i] = new ActeGeneve(int.Parse(result[i][0]),result[i][1],float.Parse(result[i][2]));
			}
			return retour;
		}

		public Assurance_SS[] ListeAssurance_SS()
		{
			string[][] result = this.ExecuteCommandeAvecTabString("SELECT IdService,LibelleService from assurances_servicesc order by LibelleService asc");
			Assurance_SS[] retour = new Assurance_SS[result.Length];
			for(int i=0;i<result.Length;i++)
			{
				retour[i] = new Assurance_SS(int.Parse(result[i][0]),result[i][1]);
			}
			return retour;
		}

		public MaterielGeneve[] ListeMaterielGeneve()
		{
			string[][] result = this.ExecuteCommandeAvecTabString("SELECT IdMateriel,Libelle,Tarif,Reference from materiel order by Libelle asc");
			MaterielGeneve[] retour = new MaterielGeneve[result.Length];
			for(int i=0;i<result.Length;i++)
			{
				retour[i] = new MaterielGeneve(int.Parse(result[i][0]),result[i][1],result[i][3],float.Parse(result[i][2]));
			}
			return retour;
		}

		public MedicamentGeneve[] ListeMedicamentGeneve()
		{
			string[][] result = this.ExecuteCommandeAvecTabString("SELECT IdMedicament,Libelle,Tarif,Reference from medicament order by Libelle asc");
			MedicamentGeneve[] retour = new MedicamentGeneve[result.Length];
			for(int i=0;i<result.Length;i++)
			{
				retour[i] = new MedicamentGeneve(int.Parse(result[i][0]),result[i][1],result[i][3],float.Parse(result[i][2]));
			}
			return retour;
		}

		public AssuranceGeneve[] ListeAssuranceGeneve()
		{
			string[][] result = this.ExecuteCommandeAvecTabString("SELECT IdAssurance,LibelleAssurance from tableassurances order by LibelleAssurance asc");
			AssuranceGeneve[] retour = new AssuranceGeneve[result.Length];
			for(int i=0;i<result.Length;i++)
			{
				retour[i] = new AssuranceGeneve(int.Parse(result[i][0]),result[i][1]);
			}
			return retour;
		}

		public RubriqueRapport[] ListeRubriqueRapport(int TypeRapport)
		{
			string[][] result = this.ExecuteCommandeAvecTabString("SELECT TypeRapport,IdCategorie,LibelleCategorie,Ordre,Dependance,Fixe from tablecategoriedansrapport where TypeRapport=" + TypeRapport + " order by Ordre asc");
			RubriqueRapport[] retour = new RubriqueRapport[result.Length];
			for(int i=0;i<result.Length;i++)
			{
				retour[i] = new RubriqueRapport(int.Parse(result[i][0]),int.Parse(result[i][1]),result[i][2],int.Parse(result[i][3]),int.Parse(result[i][4]),int.Parse(result[i][5]));
			}
			return retour;
		}

		public TarifDivers ListeTarifDivers()
		{
			TarifDivers ta = new TarifDivers();
			string[][] result = this.ExecuteCommandeAvecTabString("SELECT tarif from tarifdivers where libelletarif = 'rapportinterne'");
			if(result.Length==1)
				ta.TarifRapportInterne = float.Parse(result[0][0]);
			result = this.ExecuteCommandeAvecTabString("SELECT tarif from tarifdivers where libelletarif = 'bonadmission'");
			if(result.Length==1)
				ta.TarifBonAdmission = float.Parse(result[0][0]);
			result = this.ExecuteCommandeAvecTabString("SELECT tarif from tarifdivers where libelletarif = 'petitrapport'");
			if(result.Length==1)
				ta.TarifPetitRapport = float.Parse(result[0][0]);
			result = this.ExecuteCommandeAvecTabString("SELECT tarif from tarifdivers where libelletarif = 'moyenrapport'");
			if(result.Length==1)
				ta.TarifMoyenRapport = float.Parse(result[0][0]);
			result = this.ExecuteCommandeAvecTabString("SELECT tarif from tarifdivers where libelletarif = 'grandrapport'");
			if(result.Length==1)
				ta.TarifGrandRapport = float.Parse(result[0][0]);
			result = this.ExecuteCommandeAvecTabString("SELECT tarif from tarifdivers where libelletarif = 'policejour'");
			if(result.Length==1)
				ta.TarifPoliceJour = float.Parse(result[0][0]);
			result = this.ExecuteCommandeAvecTabString("SELECT tarif from tarifdivers where libelletarif = 'policenuit'");
			if(result.Length==1)
				ta.TarifPoliceNuit = float.Parse(result[0][0]);
			
			return ta;
		}

		#endregion

		#region Opération sur les fiches d'appels

		#region Operations sur les patients

		public bool SauvegardePersonne(DataRow row)
		{
			string DtNaissance="";
			if(row["DateNaissance"].ToString()!=System.DBNull.Value.ToString())
				DtNaissance = "'" + DateFormatMySql(DateTime.Parse(row["DateNaissance"].ToString())) + "'";
			else
				DtNaissance = "NULL";

			string ReqPers = "update tablepersonne set Tel='" + row["Tel"].ToString() + "',";
			ReqPers += "Nom='" + row["Nom"].ToString().Replace("'","''") + "',Prenom='" + row["Prenom"].ToString().Replace("'","''") + "',NumAdresse='" + row["NumAdresse"].ToString().Replace("'","''") + "'";
			ReqPers+= ",CodePostal='" + row["CodePostal"].ToString().Replace("'","''") + "',Departement='" + row["Departement"].ToString().Replace("'","''") + "'";
			ReqPers+= ",Commune='" + row["Commune"].ToString().Replace("'","''") + "',Rue='" + row["Rue"].ToString().Replace("'","''") + "'";
			ReqPers +=",NumeroDansRue='" + row["NumeroDansRue"].ToString().Replace("'","''") + "',Batiment='" + row["Batiment"].ToString().Replace("'","''") + "'";
			ReqPers += ",Escalier='" + row["Escalier"].ToString().Replace("'","''") + "',Etage='" + row["Etage"].ToString().Replace("'","''") + "'";
			ReqPers += ",Digicode='" + row["Digicode"].ToString().Replace("'","''") + "',Internom='" + row["InterNom"].ToString().Replace("'","''") + "'";
			ReqPers += ",Porte='" + row["Porte"].ToString().Replace("'","''") + "',Longitude='" + row["Longitude"].ToString().Replace("'","''") + "'";
			ReqPers += ",Latitude='" + row["Latitude"].ToString().Replace("'","''") + "',DateNaissance=" + DtNaissance;
            ReqPers += ",Sexe='" + row["Sexe"].ToString().Replace("'", "''") + "',Age='" + row["Age"].ToString().Replace("'", "''") + "',UniteAge='" + row["UniteAge"].ToString().Replace("'", "''") + "',TexteSup='" + row["TexteSup"].ToString().Replace("'", "''") + "',Chez='" + row["Chez"].ToString().Replace("'", "''") + "', Email='" + row["Email"].ToString() + "' WHERE IdPersonne = " + row["IdPersonne"].ToString();
			
			SqlCommand commande = new SqlCommand(ReqPers, Cn);
			commande.ExecuteNonQuery();
			return true;
		}

		public bool SauvegardePersonneComplete(DataRow row)
		{
			string DtNaissance="NULL";
			string DtDeces="";
			if(row["DateNaissance"].ToString()!=System.DBNull.Value.ToString())
				DtNaissance = "'" + DateFormatMySql(DateTime.Parse(row["DateNaissance"].ToString())) + "'";
			else
				DtNaissance = "NULL";
			try
			{
				if(row["DateDeces"].ToString()!=System.DBNull.Value.ToString())
					DtDeces = "'" + DateFormatMySql(DateTime.Parse(row["DateDeces"].ToString())) + "'";
				else
					DtDeces = "NULL";
			}
			catch
			{
			}

			string ReqPers = "update tablepersonne set Tel='" + row["Tel"].ToString() + "',";
			ReqPers += "Nom='" + row["Nom"].ToString().Replace("'","''") + "',Prenom='" + row["Prenom"].ToString().Replace("'","''") + "',NumAdresse='" + row["NumAdresse"].ToString().Replace("'","''") + "'";
			ReqPers+= ",CodePostal='" + row["CodePostal"].ToString().Replace("'","''") + "',Departement='" + row["Departement"].ToString().Replace("'","''") + "'";
			ReqPers+= ",Commune='" + row["Commune"].ToString().Replace("'","''") + "',Rue='" + row["Rue"].ToString().Replace("'","''") + "'";
			ReqPers +=",NumeroDansRue='" + row["NumeroDansRue"].ToString().Replace("'","''") + "',Batiment='" + row["Batiment"].ToString().Replace("'","''") + "'";
			ReqPers +=",Adm_NumeroDansRue='" + row["Adm_NumeroDansRue"].ToString().Replace("'","''") + "',Adm_Rue='" + row["Adm_Rue"].ToString().Replace("'","''") + "'";
			ReqPers +=",Adm_CodePostal='" + row["Adm_CodePostal"].ToString().Replace("'","''") + "',Adm_Commune='" + row["Adm_Commune"].ToString().Replace("'","''") + "',Adm_Pays='" + row["Adm_Pays"].ToString() + "',Adm_Batiment='" + row["Adm_Batiment"].ToString().Replace("'","''") + "'";
			ReqPers += ",Escalier='" + row["Escalier"].ToString().Replace("'","''") + "',Etage='" + row["Etage"].ToString().Replace("'","''") + "'";
			ReqPers += ",Digicode='" + row["Digicode"].ToString().Replace("'","''") + "',Internom='" + row["InterNom"].ToString().Replace("'","''") + "'";
			ReqPers += ",Porte='" + row["Porte"].ToString().Replace("'","''") + "',Longitude='" + row["Longitude"].ToString().Replace("'","''") + "'";
			ReqPers += ",Latitude='" + row["Latitude"].ToString().Replace("'","''") + "',DateNaissance=" + DtNaissance;
			ReqPers += ",Sexe='" + row["Sexe"].ToString().Replace("'","''") + "',Age='" + row["Age"].ToString().Replace("'","''") + "',UniteAge='" + row["UniteAge"].ToString().Replace("'","''") + "'";
            ReqPers += ",TexteSup='" + row["TexteSup"].ToString().Replace("'", "''") + "',DateDeces=" + DtDeces + ",Chez='" + row["Chez"].ToString().Replace("'", "''") + "'";
            ReqPers += ",Num_Assure='" + row["Num_Assure"].ToString().Replace("'", "''") + "',Num_AVS='" + row["Num_AVS"].ToString().Replace("'", "''") + "'";
            ReqPers += ",Email='" + row["Email"].ToString() + "', Num_Carte='" + row["Num_Carte"].ToString().Replace("'", "''") + "'";
            ReqPers += " WHERE IdPersonne = " + row["IdPersonne"].ToString();
			
			SqlCommand commande = new SqlCommand(ReqPers, Cn);
			commande.ExecuteNonQuery();
			return true;
		}

		public bool SauvegardePatient(DataRow row)
		{
			SauvegardePersonne(row);
			
			// on met à jour le patient
			string ReqPat = "UPDATE tablepatient set SuiviPatient = '" + row["SuiviPatient"].ToString().Replace("'","''") + "' WHERE IdPatient = " + row["IdPatient"].ToString();
			
			SqlCommand commande = new SqlCommand(ReqPat, Cn);
			commande.ExecuteNonQuery();
			return true;
		}

        public bool SauvegardePatientComplet(DataRow row)
        {
            SauvegardePersonneComplete(row);

            // on met à jour le patient
            string ReqPat = "UPDATE tablepatient set SuiviPatient = '" + row["SuiviPatient"].ToString().Replace("'", "''") + "' WHERE IdPatient = " + row["IdPatient"].ToString();
           
			SqlCommand commande = new SqlCommand(ReqPat, Cn);
			commande.ExecuteNonQuery();
            return true;
        }

		public DataRow RecupereStructurePatient()
		{
			DataTable tb = ExecuteCommandeAvecDataTable("SELECT pa.IdPatient,pa.IdPersonne,pa.SuiviPatient,pa.IdAbonnement,pa.TypeAbonnement,pa.TexteAbonnement,pe.Nom as 'Nom' ,pe.Prenom as 'Prenom',pe.Tel as 'Tel',pe.NumAdresse,pe.CodePostal,pe.Departement,pe.Commune,pe.Rue,pe.NumeroDansRue,pe.Batiment,pe.Etage,pe.Escalier,pe.Digicode,pe.Internom,pe.Porte,pe.longitude,pe.Latitude,pe.DateNaissance,pe.sexe,pe.Age,pe.UniteAge,pe.TexteSup,pe.StopRappelTA,pe.ListeNoire from tablepatient pa  left join tablepersonne pe on pe.IdPersonne = pa.IdPersonne Where 0=1");
			return tb.NewRow();
		}

		public bool SauvegardeAssurance(DataRow row, int New)
		{
			if (New == 0)
			{
				// on met à jour lâssurance
				string ReqAssurance = "UPDATE assurances SET NAssurance= '" + row["NAssurance"].ToString().Replace("'","''") + "', NAdresse= '" + row["NAdresse"].ToString().Replace("'","''") + "',  AssApprouve= '" + row["AssApprouve"].ToString().Replace("'","''") + "', AssNom= '" + row["AssNom"].ToString().Replace("'","''") + "', AssTelephone= '" + row["AssTelephone"].ToString().Replace("'","''") + "', AssFax= '" + row["AssFax"].ToString().Replace("'","''") + "',AssAdresseTexte= '" + row["AssAdresseTexte"].ToString().Replace("'","''") + "',AssService= '" + row["AssService"].ToString().Replace("'","''") + "', AssCpostale= '" + row["AssCpostale"].ToString().Replace("'","''") + "', AssExtLocalite= '" + row["AssExtLocalite"].ToString().Replace("'","''") + "', AssContact= '" + row["AssContact"].ToString().Replace("'","''") + "',NCaisse= '" + row["NCaisse"].ToString().Replace("'","''") + "', AssCommentaire= '" + row["AssCommentaire"].ToString().Replace("'","''") + "' WHERE NAssurance = " + row["NAssurance"].ToString();
				//bool reous = ExecuteCommandeSansRetour(ReqAssurance);
				
				SqlCommand commande = new SqlCommand(ReqAssurance, Cn);
				commande.ExecuteNonQuery();
				return true;
			}
			else if (New == 1)
			{
				// on met à jour lâssurance
				
				string ReqAssurance = "INSERT INTO assurances (NAssurance, NAdresse, AssAdresseTexte, AssNom, AssService, AssTelephone, AssFax, AssCpostale, AssExtLocalite, AssContact, AssApprouve, AssCommentaire, NCaisse) VALUES ('"+ row["NAssurance"].ToString().Replace("'","''")+"','"+row["NAdresse"].ToString().Replace("'","''")+"','"+  row["AssAdresseTexte"].ToString().Replace("'","''")+"','"+  row["AssNom"].ToString().Replace("'","''")+"','"+  row["AssService"].ToString().Replace("'","''")+"','"+  row["AssTelephone"].ToString()+"','"+  row["AssFax"].ToString().Replace("'","''")+"','"+ row["AssCpostale"].ToString().Replace("'","''")+"','"+  row["AssExtLocalite"].ToString().Replace("'","''")+"','"+  row["AssContact"].ToString().Replace("'","''")+"','"+ row["AssApprouve"].ToString().Replace("'","''")+"','"+ row["AssCommentaire"].ToString().Replace("'","''")+"','"+ row["NCaisse"].ToString().Replace("'","''")+"')";
				
				SqlCommand commande = new SqlCommand(ReqAssurance, Cn);
				commande.ExecuteNonQuery();
				return true;
			}
			else return false;
		}

		#endregion

		#region Recherche d'index libres et d'existence de fiches

		public bool Sql_PersonneExiste(long IdPersonne)
		{
			string Requete = "SELECT IdPersonne from tablepersonne Where IdPersonne = " + IdPersonne ;
			
			SqlCommand commande = new SqlCommand(Requete, Cn);
			SqlDataReader reader = commande.ExecuteReader();

			bool Existe = false;
			if(reader.Read())
				Existe = true;
			else
			{
				
				Existe = false;
			}
			reader.Close();
			reader=null;
			return Existe;				
		}

		public bool Sql_PatientExiste(long IdPatient)
		{
			string Requete = "SELECT IdPatient from tablepatient Where IdPatient = " + IdPatient ;
			
			SqlCommand commande = new SqlCommand(Requete, Cn);
			SqlDataReader reader = commande.ExecuteReader();

			bool Existe = false;
			if(reader.Read())
				Existe = true;
			else
				Existe = false;
			
			reader.Close();
			reader=null;
			return Existe;				
		}
		#endregion

		#endregion		
				
		#region Opérations sur les rapports

		#region Rubrique Destinataire

		public DataRow AjouteMedecinVille()
		{
			string[][] retour = ExecuteCommandeAvecTabString("SELECT max(num) from medecinsville");
			long max = 0;
			if(!(retour==null || retour.Length==0 || retour[0][0]==System.DBNull.Value.ToString()))
				max = long.Parse(retour[0][0]) + 1;
            ExecuteCommandeSansRetour("INSERT INTO medecinsville(num) values (" + max + ")");
			return ExecuteCommandeAvecDataSet("SELECT * from medecinsville WHERE Num = " + max).Tables[0].Rows[0];
		}
		public void SupprimerMedecinVille(long Num)
		{
			 ExecuteCommandeSansRetour("delete from medecinsville where num = " + Num);
		}

       /* public bool SauvegardeEnvoiMail(SosMedecins.SmartRapport.DAL.dstRapport.RapportRow tb)
		{
			if(tb!=null)
			{
				bool reussite = false;
				// Sauvegarde du rapport
				reussite = ExecuteCommandeSansRetour("INSERT INTO tablemailmt(DateMail,Destinataire,NRapport,Sujet,Objet) values ('" + DateFormatMySql(DateTime.Now) + "','" + "christophe@medicallconcept.com" + "','" + tb.NRapport + "','" + "test" + "','" + "test" + "')");
				
				return reussite;
			}
			else
				return false;
		}*/

		#endregion 

		#region Création / Sauvegarde de rapport

		public long CreationRapport(string strScribe,long NConsult)
		{
			string RapEntete = "";
			string RapSignature = "";

            RapEntete = SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.CommuneEditionRapports + ", le " + DateTime.Now.ToString().Split(' ')[0];
			string[][] retour = ExecuteCommandeAvecTabString("SELECT max(NRapport) from tablerapports");
			long max = 0;
			if(!(retour==null || retour.Length==0 || retour[0][0]==System.DBNull.Value.ToString()))
				max = long.Parse(retour[0][0]) + 1;

            string reference = "R" + max + "/" + VariablesApplicatives.Utilisateurs.Initiale;

			string Requete = "INSERT INTO tablerapports(NRapport,NConsultation,DateRapport,RapScribe,RapSignature,RapEntete,TypeRapport,RapReference, Vise, Commentaire) ";
			Requete+=" values ('" + max + "','" + NConsult + "','" + DateFormatMySql(DateTime.Now) + "','" + strScribe.Replace("'","''") + "','" + RapSignature.Replace("'","''") + "','" +  RapEntete.Replace("'","''") + "',1,'" + reference + "', 0, '')";

			bool resultat = ExecuteCommandeSansRetour(Requete);
			ExecuteCommandeSansRetour("DELETE FROM tablerapportcorps WHERE NRapport = " + max);
			ExecuteCommandeSansRetour("DELETE FROM tablerapportdestine WHERE NRapport = " + max);
            if (resultat)
            {
                Fonction z_objFonctionDal = new Fonction();
                z_objFonctionDal.EnregistreModification(NConsult.ToString(), VariablesApplicatives.Utilisateurs.Identifiant, DateTime.Now, Constantes.CREATION_RAPPORT, "");
            }
			return max;
		}

        public bool SauvegardeRapport(dstRapport.RapportRow tb, dstCorps Corps, dstDestination Destination, string CodeUtilisateur, string xml, string Commentaire)
		{
			if(tb!=null)
			{
				if(xml==null || xml=="") 
					xml="NULL";
				else
					xml = "'" + xml.Replace("'","''") + "'"; 

				//string Code = RandomPassword.Generate();

				bool reussite = false;
				// Sauvegarde du rapport
				reussite = ExecuteCommandeSansRetour("UPDATE tablerapports set TypeRapport = " + tb.TypeRapport + ",RapEnTete='" + tb.RapEnTete.Replace("'","''") + "',RapCopie = " + tb.RapCopie + ",RapConcerne = '" + tb.RapConcerne.Replace("'","''") + "',RapSignature = '" + tb.RapSignature.Replace("'","''") + "' WHERE NRapport = " + tb.NRapport);
				ExecuteCommandeSansRetour("DELETE FROM tablerapportcorps WHERE NRapport = " + tb.NRapport);
				ExecuteCommandeSansRetour("DELETE FROM tablerapportdestine WHERE NRapport = " + tb.NRapport + " AND RapEnvoye = 0");

				// Sauvegarde du corps du rapport
				for(int i=0;i<Corps.Corps.Count;i++)
				{
					ExecuteCommandeSansRetour("INSERT INTO tablerapportcorps (NRapport,IdCategorie,Valeur) values ('" + tb.NRapport + "','" + Corps.Corps[i].IdCategorie + "','" + Corps.Corps[i].ValeurCategorie.Replace("'","''") + "')");
				}
				// on supprime tous les médecins traitants du patient, comme cela on va les mettre à jour : 
				ExecuteCommandeSansRetour("DELETE FROM tablepatientmedTTT WHERE IdPatient = " + tb.CodePatient);
				// Sauvegarde des destinataires
				for(int i=0;i<Destination.Destination.Count;i++)
				{
					string[][] RapportDestineExistant = OutilsExt.OutilsSql.ExecuteCommandeAvecTabString("SELECT CodeDestinataire from tablerapportdestine where NRapport = " + tb.NRapport + " AND CodeDestinataire = " + Destination.Destination[i].CodeDestinataire);
                    if (RapportDestineExistant == null || RapportDestineExistant.Length == 0)
                    {
                        string destinatairesauve = Destination.Destination[i].TypeDestinataire;
                        if (destinatairesauve.Length > 10)
                        {
                            destinatairesauve = destinatairesauve.Substring(0, 10);
                        }
                        ExecuteCommandeSansRetour("INSERT INTO tablerapportdestine (NRapport,TypeDestinataire,CodeDestinataire,RapDestinataire,RapBonjour,RapIntroduction,RapSalutation,RapModeEnvoi,Nom,Logo,mail,Copie) values ('" + tb.NRapport + "','" + destinatairesauve + "','" + Destination.Destination[i].CodeDestinataire + "','" + Destination.Destination[i].RapDestinataire.Replace("'", "''") + "','" + Destination.Destination[i].RapBonjour.Replace("'", "''") + "','" + Destination.Destination[i].RapIntroduction.Replace("'", "''") + "','" + Destination.Destination[i].RapSalutation.Replace("'", "''") + "','" + Destination.Destination[i].RapModeEnvoi + "','" + Destination.Destination[i].Nom.Replace("'", "''") + "'," + Destination.Destination[i].Logo + ",''," + Destination.Destination[i].Copie + ")");
                    }

					// on réinsere le médecin traitant du patient :
					if(Destination.Destination[i].TypeDestinataire==Destinataire.TypeDestinataire.MedecinTraitant.ToString() || Destination.Destination[i].TypeDestinataire=="MedecinTra")
					{
						ExecuteCommandeSansRetour("insert into tablepatientmedttt (IdPatient,IdMedecin) values (" + tb.CodePatient + "," + Destination.Destination[i].CodeDestinataire + ")");
					}
				}               

				if(!reussite) return false;
                reussite = ExecuteCommandeSansRetour("UPDATE tableconsultations set RapportGenere = 1, Morphine = " + tb.Morphine + ", Pethidine = " + tb.Pethidine + ", Dormicum = " + tb.Dormicum + ", Methadone = " + tb.Methadone + ", Autre_stup = '" + tb.Autre_stup + "', Autre_stup_qte = " + tb.Autre_stup_qte + ", Auteur = " + tb.Auteur + ", Type_long_rapport = " + tb.Type_long_rapport + "  WHERE NConsultation = " + tb.NConsultation);
   
				if(!reussite) return false;
				// Sauvegarde de la modification
                Fonction z_objFonctionDal = new Fonction();
                z_objFonctionDal.EnregistreModification(tb.NConsultation.ToString(), VariablesApplicatives.Utilisateurs.Identifiant, DateTime.Now, Constantes.MODIF_RAPPORT, Commentaire);
				return reussite;
			}
			else
				return false;
		}

		#endregion

		#region Visa - Envois - Reprises

		public void DemandeVisa(long NRapport,bool Valeur)
		{
			int val = 0;
			if(Valeur) val = 1;

			ExecuteCommandeSansRetour("update tablerapports set Vise=0,BonPourReprise=0, aViser = " + val + ",Vise=0 WHERE Nrapport = " + NRapport);
		}

		public void SuppressionRapport(long NConsult,long NRapport)
		{
			ExecuteCommandeSansRetour("update tableconsultations set Rapportgenere = 0 WHERE NConsultation = " + NConsult);
			ExecuteCommandeSansRetour("delete from tablerapports WHERE Nrapport = " + NRapport);
			ExecuteCommandeSansRetour("delete from tablerapportdestine WHERE Nrapport = " + NRapport);
			ExecuteCommandeSansRetour("delete from tablerapportcorps WHERE Nrapport = " + NRapport);

            Fonction z_objFonctionDal = new Fonction();
            z_objFonctionDal.EnregistreModification(NConsult.ToString(), VariablesApplicatives.Utilisateurs.Identifiant, DateTime.Now, Constantes.SUPP_RAPPORT, "");
	    }

		public void DemandeCorrection(long NRapport,bool Valeur)
		{
			int val = 0;
			if(Valeur) val = 1;

			ExecuteCommandeSansRetour("update tablerapports set ACorriger= " + val +  " WHERE Nrapport = " + NRapport);
		}

        //Accord du visa
        public void VisaSurRapport(SosMedecins.SmartRapport.DAL.dstRapport.RapportRow row, bool Valeur)
		{
			int val = 0;
			row["RapSignature"]="";

            //MessageBox.Show(VariablesApplicatives.Utilisateurs.Identifiant.ToString());
            if(Valeur) 
			{
				val = 1;
				if(row["NomMedecinSos"].ToString().Split(' ').Length == 1)
				{
					row["RapSignature"] = "Docteur " + row["NomMedecinSos"].ToString().ToUpper();
				}
				else if(row["NomMedecinSos"].ToString().Split(' ').Length == 2)
				{
					row["RapSignature"] = "Docteur " + row["NomMedecinSos"].ToString().Split(' ')[1].Substring(0,1).ToUpper() + row["NomMedecinSos"].ToString().Split(' ')[1].Remove(0,1).ToLower() + " " + row["NomMedecinSos"].ToString().Split(' ')[0].ToUpper();
				}
				else
					row["RapSignature"] = "Docteur " + row["NomMedecinSos"].ToString().ToUpper();
			}
			            
            //on vise le rapport
            ExecuteCommandeSansRetour("update tablerapports set  Vise = " + val + ",AViser = 0,BonPourReprise = 0,RapSignature = '" + row["RapSignature"].ToString().Replace("'", "''") + "', Medecin_viseur = '" + VariablesApplicatives.Utilisateurs.Identifiant.ToString() + "'  WHERE Nrapport = " + row["NRapport"].ToString());

            //Puis on met à jour la tablerapportdestine avec une date d'envoi pour ne plus les envoyer******A activer en octobre 2014*******  Domi le 24.06.2014
            //ExecuteCommandeSansRetour("update tablerapportdestine set RapEnvoye = 1, DateEnvoi =  '" + DateTime.Now + "'  WHERE Nrapport = " + row["NRapport"].ToString());
		}
		public void BonPourReprise(long NRapport,bool Valeur)
		{
			int val = 0;
			if(Valeur) val = 1;

			ExecuteCommandeSansRetour("update tablerapports set BonPourReprise = " + val + ",AViser = 0,Vise = 0  WHERE Nrapport = " + NRapport);
		}		

		public bool SetRapportEnvoye(long NRapport,int CodeDestinataire,bool Valeur)
		{
			int Val = 0;
			if(Valeur) Val=1;

			if(Val==0)
				return ExecuteCommandeSansRetour("update tablerapportdestine set RapEnvoye = " + Val + " WHERE NRapport = " + NRapport + " AND CodeDestinataire = " + CodeDestinataire);
			else
				return ExecuteCommandeSansRetour("update tablerapportdestine set RapEnvoye = " + Val + ",DateEnvoi = '" + DateFormatMySql(DateTime.Now) + "' WHERE NRapport = " + NRapport + " AND CodeDestinataire = " + CodeDestinataire);			
		}

		public string[][] ListeRapportAViser()
		{			
			string[][] result = this.ExecuteCommandeAvecTabString("SELECT r.NRapport,a.DAP,m.Nom as 'NomMedecinSos',pe.Nom as 'NomPatient',r.NConsultation from tablerapports r inner join tableconsultations c on c.NConsultation = r.NConsultation inner join tableactes a on a.Num = c.CodeAppel inner join tablepatient pa on pa.IdPatient = c.IndicePatient inner join tablepersonne pe on pe.IdPersonne = pa.IdPErsonne inner join tablemedecin m on m.CodeIntervenant = a.CodeIntervenant WHERE r.TypeRapport <> 3 AND r.AViser = 1 AND r.Vise = 0 AND r.ACorriger=0 AND r.Archive=0 ORDER BY NRapport");
			return result;
		}

		public string[][] ListeRapportACorriger()
		{			
			string[][] result = this.ExecuteCommandeAvecTabString("SELECT r.NRapport,a.DAP,m.Nom as 'NomMedecinSos',pe.Nom as 'NomPatient',r.NConsultation from tablerapports r inner join tableconsultations c on c.NConsultation = r.NConsultation inner join tableactes a on a.Num = c.CodeAppel inner join tablepatient pa on pa.IdPatient = c.IndicePatient inner join tablepersonne pe on pe.IdPersonne = pa.IdPErsonne inner join tablemedecin m on m.CodeIntervenant = a.CodeIntervenant WHERE r.Vise = 0 AND r.ACorriger=1 AND r.Archive = 0");
			return result;
		}

		public string[][] ListeSansRapport()
		{			
			string[][] result = this.ExecuteCommandeAvecTabString("SELECT r.NRapport,a.DAP,m.Nom as 'NomMedecinSos',pe.Nom as 'NomPatient',r.NConsultation,r.DateEnvoi from tablerapports r inner join tableconsultations c on c.NConsultation = r.NConsultation inner join tableactes a on a.Num = c.CodeAppel inner join tablepatient pa on pa.IdPatient = c.IndicePatient inner join tablepersonne pe on pe.IdPersonne = pa.IdPErsonne inner join tablemedecin m on m.CodeIntervenant = a.CodeIntervenant WHERE r.TypeRapport = 3 AND r.Archive = 0");
			return result;
		}

		public string[][] ListeRapportReprise()
		{			
			string[][] result = this.ExecuteCommandeAvecTabString("SELECT r.NRapport,a.DAP,m.Nom as 'NomMedecinSos',pe.Nom as 'NomPatient',r.NConsultation from tablerapports r inner join tableconsultations c on c.NConsultation = r.NConsultation  inner join tableactes a on a.Num = c.CodeAppel inner join tablepatient pa on pa.IdPatient = c.IndicePatient inner join tablepersonne pe on pe.IdPersonne = pa.IdPErsonne inner join tablemedecin m on m.CodeIntervenant = a.CodeIntervenant WHERE r.BonPourReprise = 1 AND r.Vise = 0 AND r.Archive = 0");
			return result;
		}		

		public string[][] ListeRapportPourEnvoi()
		{
			string[][] result = this.ExecuteCommandeAvecTabString("SELECT r.NRapport,a.DAP,m.Nom as 'NomMedecinSos',pe.Nom as 'NomPatient',r.NConsultation,d.CodeDestinataire,d.Nom from tablerapports r inner join tableconsultations c on c.NConsultation = r.NConsultation inner join tableactes a on a.Num = c.CodeAppel inner join tablepatient pa on pa.IdPatient = c.IndicePatient inner join tablepersonne pe on pe.IdPersonne = pa.IdPersonne inner join tablemedecin m on m.CodeIntervenant = a.CodeIntervenant left join tablerapportdestine d on d.NRapport  = r.NRapport WHERE r.BonPourReprise = 0 AND r.Vise = 1 AND d.RapEnvoye = 0 AND r.Archive=0");
			return result;
		}

		public string[][] ListeRapportPourEnvoi(DateTime date1,DateTime date2, string modeenvoi)
		{
			date1 = DateTime.Parse(date1.ToString().Split(' ')[0] + " 00:00:00");
			date2 = DateTime.Parse(date2.ToString().Split(' ')[0] + " 23:59:59");

			string[][] result = this.ExecuteCommandeAvecTabString("SELECT r.NRapport,a.DAP,m.Nom as 'NomMedecinSos',pe.Nom as 'NomPatient',r.NConsultation,d.CodeDestinataire,d.Nom from tablerapports r inner join tableconsultations c on c.NConsultation = r.NConsultation inner join tableactes a on a.Num = c.CodeAppel inner join tablepatient pa on pa.IdPatient = c.IndicePatient inner join tablepersonne pe on pe.IdPersonne = pa.IdPersonne inner join tablemedecin m on m.CodeIntervenant = a.CodeIntervenant left join tablerapportdestine d on d.NRapport  = r.NRapport WHERE r.BonPourReprise = 0 AND r.Vise = 1 AND d.RapEnvoye = 0 AND r.Archive=0 and d.RapModeEnvoi = '" + modeenvoi + "' and DAP >= '" + DateFormatMySql(date1) + "' and DAP <= '" + DateFormatMySql(date2) + "'");
			return result;
		}


        public string GetMailFromMedecin(int Code)
		{
			string[][] retour  = ExecuteCommandeAvecTabString("SELECT email from medecinsville where num = " + Code);
			if(retour!=null && retour.Length>0 && retour[0][0]!="")
				return retour[0][0];
			else
				return "";
		}
        
        public string GetFaxFromMedecin(int Code)
		{
			string[][] retour  = ExecuteCommandeAvecTabString("SELECT Fax from medecinsville where num = " + Code);
			if(retour!=null && retour.Length>0 && retour[0][0]!="")
				return "0041" + retour[0][0].Remove(0,1);
			else
				return "";
		}

		public string GetNomFromMedecin(int Code)
		{
			string[][] retour  = ExecuteCommandeAvecTabString("SELECT (Nom+' '+Prenom) from medecinsville where num = " + Code);
			if(retour!=null && retour.Length>0 && retour[0][0]!="")
				return retour[0][0];
			else
				return "";
		}
		public string GetFaxFromCommissariat()
		{
			string[][] retour  = ExecuteCommandeAvecTabString("SELECT Fax from commissariat");
			if(retour!=null && retour.Length>0 && retour[0][0]!="")
				return "0041" + retour[0][0].Remove(0,1);
			else
				return "";
		}

		public void SetRapportAViser(long NRapport,bool Valeur)
		{
			int val = 0;
			if(Valeur) val = 1;

			ExecuteCommandeSansRetour("update tablerapports set aviser = " + val + ",BonPourReprise=0,Vise=0 WHERE NRapport = " + NRapport);
		}	

		public void EnvoiDeSansRapport(long IdRapport)
		{
			ExecuteCommandeSansRetour("update tablerapports set DateEnvoi = '" + DateFormatMySql(DateTime.Now) + "' WHERE NRapport = " + IdRapport);
		}

		#endregion

		#region Etat du rapport
		
		public ArrayList EtatRapport(long IdRapport)
		{
			ArrayList liste = new ArrayList();

            string z_strSql = SosMedecins.SmartRapport.DAL.RequetesSelect.tablemodifications.NRapportType;
            z_strSql = z_strSql.Replace("%Type%", Constantes.CREATION_RAPPORT.ToString());
            z_strSql = z_strSql.Replace("%NRapport%", IdRapport.ToString());

            string[][] val = ExecuteCommandeAvecTabString(z_strSql);
			if(val.Length>0) 
				liste.Add(new string[] {val[0][0],val[0][1]});
			else 
				liste.Add(null);
            //
            z_strSql = SosMedecins.SmartRapport.DAL.RequetesSelect.tablemodifications.NRapportType;
            z_strSql = z_strSql.Replace("%Type%", Constantes.MODIF_RAPPORT.ToString());
            z_strSql = z_strSql.Replace("%NRapport%", IdRapport.ToString());

            val = ExecuteCommandeAvecTabString(z_strSql);
            if (val.Length > 0) 
				liste.Add(new string[] {val[0][0],val[0][1]});
			else 
				liste.Add(null);

            // rapport visée
            z_strSql = SosMedecins.SmartRapport.DAL.RequetesSelect.tablerapports.Vise;
            z_strSql = z_strSql.Replace("%NRapport%", IdRapport.ToString());

            val = ExecuteCommandeAvecTabString(z_strSql);
			if(val.Length>0) 
				liste.Add(new string[] {val[0][0],val[0][1]});
			else 
				liste.Add(null);
			return liste;
		}

		#endregion

		#endregion	
	
		#region Opération Sql sur Télé-Alarme


		public DataSet NouveauAbonnement(long IdPersonne, long IdPatient)
		{
			int max = -1;
			string[][] retour = ExecuteCommandeAvecTabString("SELECT max(IdAbonnement) from ta_abonnement");
			if(retour!=null && retour.Length!=0 && retour[0][0]!="")
			{
				max = int.Parse(retour[0][0]) +1;
			}
			else
			{
				max = 1;
			}

			if(max>-1)
			{
				long Patient =-1;
				long Personne =-1;

				if(IdPersonne==-1)
				{

					retour = ExecuteCommandeAvecTabString("SELECT max(IdPersonne) from tablepersonne");
					if(retour!=null && retour.Length!=0 && retour[0][0]!="")
					{
						Personne = int.Parse(retour[0][0]) +1;
					}
					else
					{
						Personne = 1;
					}
				}

				if(IdPatient==-1)
				{

					retour = ExecuteCommandeAvecTabString("SELECT min(IdPatient) from tablepatient");
					if(retour!=null && retour.Length!=0 && retour[0][0]!="")
					{
						Patient = int.Parse(retour[0][0]) -1;
					}
					else
					{
						Patient = -1;
					}

					if(Patient>=0) Patient = -1;
				}

				bool reussite;

				// Soit on crée un nouveau patient sans avoir recherché s'il existait dans la base locale
				// soit on ne l'a pas trouvé
				// Dans ce cas on recrée une personne puis un patient
				if(IdPersonne==-1 && IdPatient==-1)
				{
					reussite = ExecuteCommandeSansRetour("INSERT INTO ta_abonnement(IdAbonnement,IdPatient,DateCreationAbonnement) values (" + max + "," + Patient + ",'" + DateFormatMySql(DateTime.Now) + "')");
					reussite = ExecuteCommandeSansRetour("INSERT INTO tablepersonne(IdPersonne) values (" + Personne + ")");
					reussite = ExecuteCommandeSansRetour("INSERT INTO tablepatient(IdPatient,IdPersonne,IdAbonnement,TypeAbonnement,Approuve) values (" + Patient + "," + Personne + "," + max + ",'TA',0)");
				}
				// on n'a pas retrouvé cette personne dans la base locale par contre on souhaite importer un patient référencé dans la base medicall
				else if(IdPersonne==-1 && IdPatient>0)
				{
					reussite = ExecuteCommandeSansRetour("INSERT INTO tablepersonne(IdPersonne) values (" + Personne + ")");
					reussite = ExecuteCommandeSansRetour("INSERT INTO tablepatient(IdPatient,IdPersonne,IdAbonnement,TypeAbonnement,Approuve) values (" + IdPatient + "," + Personne + "," + max + ",'TA',0)");
					reussite = ExecuteCommandeSansRetour("INSERT INTO ta_abonnement(IdAbonnement,IdPatient,DateCreationAbonnement) values (" + max + "," + IdPatient + ",'" + DateFormatMySql(DateTime.Now) + "')");
				}
				// on a trouvé le patient dans la base locale, on va donc le transformer en abonné TéléAlarme
				else
				{
					reussite = ExecuteCommandeSansRetour("update tablepatient set IdAbonnement = " + max + ",TypeAbonnement='TA',TexteAbonnement='',Approuve=0 WHERE IdPatient = " + IdPatient);
					reussite = ExecuteCommandeSansRetour("INSERT INTO ta_abonnement(IdAbonnement,IdPatient,DateCreationAbonnement) values (" + max + "," + IdPatient + ",'" + DateFormatMySql(DateTime.Now) + "')");
				}
				reussite = ExecuteCommandeSansRetour("INSERT INTO ta_abonnementlieufacture(TF_IdAbonnement) values (" + max + ")");
				reussite = ExecuteCommandeSansRetour("INSERT INTO ta_abonnementdossier(IdAbonnement) values (" + max + ")");
				reussite = ExecuteCommandeSansRetour("INSERT INTO ta_abonnementcle(IdAbonnement) values (" + max + ")");
				if(reussite)
				{
                    return RecupereAbonnement(max, 0);      //Domi  07.11.2013 (2ème argument dans la fct)
				}
				else
					return null;
			}
			else
				return null;
		}

        public DataSet RecupereAbonnement(int IdAbonnement, int TypeArchive)
		{
            string FiltreSupp = "";
            if (TypeArchive == 1)
                FiltreSupp = " AND Archive = 0 ";            
            if (TypeArchive == 2)
                FiltreSupp = " AND Archive = 1 ";
          

            DataTable dt1 = ExecuteCommandeAvecDataTable(@"SELECT ta1.IdAbonnement,ta1.DateCreationAbonnement,ta1.DateDebutFacturation,ta1.Commentaire,ta1.IdContrat,
                                                           ta1.ClePresente,ta1.Faxfsasd,ta1.DossierBleu,ta1.Periodicite,ta1.Ordre,ta1.Archive,ta1.Export, ta1.ExportMcc,
                                                           ta1.N_TA, ta2.*,pa.TypeAbonnement,pa.TexteAbonnement,pa.IdPatient,pe.IdPersonne,pe.Tel,pe.Nom,pe.PRenom,pe.NumAdresse,pe.CodePostal,
                                                           pe.Commune,pe.Rue,pe.NumeroDansRue,pe.Batiment,pe.Escalier,pe.Etage,pe.Digicode,pe.Internom,pe.Porte,pe.DateNaissance,pe.Sexe,pe.Age,
                                                           pe.UniteAge,pe.Longitude, pe.Latitude, pe.TexteSup,pe.StopRappelTA, ta1.ActiverFacture 
                                                           FROM ta_abonnement ta1 inner join ta_abonnementlieufacture ta2 on ta1.IdAbonnement = ta2.TF_IdAbonnement inner join tablepatient pa on pa.IdPAtient = ta1.IdPAtient
                                                           inner join tablepersonne pe on pe.IdPErsonne = pa.IdPersonne  
                                                           WHERE ta1.IdAbonnement = '" + IdAbonnement + "'" + FiltreSupp);
			DataTable dt2= ExecuteCommandeAvecDataTable("SELECT * from ta_abonnementcontacts  where idabonnement = " + IdAbonnement);
			DataTable dt3= ExecuteCommandeAvecDataTable("SELECT * from ta_abonnementurgence  where idabonnement = " + IdAbonnement);
			DataTable dt4= ExecuteCommandeAvecDataTable("SELECT * from ta_abonnementdossier  where idabonnement = " + IdAbonnement);
			DataTable dt5= ExecuteCommandeAvecDataTable("SELECT * from ta_abonnementcle  where idabonnement = " + IdAbonnement);
			DataTable dt6= ExecuteCommandeAvecDataTable("SELECT * from ta_abonnementjournal  where idabonnement = " + IdAbonnement + " ORDER BY DateOp DESC" );
                     

            //Pour les factures:
            string Requete = @"SELECT f.NFacture as NumFacture, CONVERT(VARCHAR,f.Date_facture,104) as Date_facture, f.Montant, CONVERT(VARCHAR,f.Début_période,104) as Début_période,
                                        CONVERT(VARCHAR,f.Fin_période,104) as Fin_période, CONVERT(VARCHAR,f.Payé,104) as Date_Paiement, 
                                        CASE f.Acquité WHEN 0 THEN 'NON' ELSE 'OUI' END AS Acquite, Moyen as MoyenPaiement, TypeFacture = 'Abonnement'
                               FROM ta_abonnement ab, 
                               ta_factures f LEFT OUTER JOIN (SELECT NumFacture as NumFactureOp, Montant, MoyenPaiement FROM TA_Factures_Op) AS Operations ON f.NFacture = NumFactureOp
                               WHERE ab.IdAbonnement = f.Idabonnement
                               AND ab.Archive=0 
                               AND ab.Idabonnement = " + IdAbonnement +
                               @"UNION
                               SELECT fm.NumFacture, CONVERT(VARCHAR,fm.DateFacture,104) as Date_facture, fm.TotalFacture as Montant, Début_période = null,
                                      Fin_période = null, CONVERT(VARCHAR,fm.DateAcquitementFact,104) as Date_Paiement,
                                        CASE WHEN fm.DateAcquitementFact is null THEN 'NON' ELSE 'OUI' END AS Acquite, MoyenPaiement, TypeFacture = 'Matériel'
                               FROM ta_abonnement ab,
                               TA_FactMat fm LEFT OUTER JOIN (SELECT NumFacture as NumFactureOp, Montant, MoyenPaiement FROM TA_FactMat_Op) AS Operations ON fm.NumFacture = NumFactureOp
                               WHERE ab.IdAbonnement = fm.Idabonnement 
                               AND ab.Archive=0 
                               AND ab.Idabonnement = " + IdAbonnement + 
                               "ORDER BY NumFacture DESC";
    
             DataTable dt7 = ExecuteCommandeAvecDataTable(Requete);
            
           /* DataTable dt7 = ExecuteCommandeAvecDataTable(@"SELECT f.NFacture, f.Date_facture, f.Montant, f.Début_période, f.Fin_période, f.Payé, f.Moyen, f.Acquité,
                                                         f.SBVR, f.Remarque, f.Idabonnement 
                                                         FROM ta_factures f, ta_abonnement ab 
                                                         WHERE ab.IdAbonnement = f.Idabonnement 
                                                         AND ab.Archive=0 AND ab.Idabonnement= " + IdAbonnement + " ORDER BY f.Nfacture DESC");        
            */
			if(dt1!=null && dt2!=null && dt3!=null && dt4!=null && dt5!=null && dt6!=null)
			{
				DataSet ds = new DataSet();
				ds.Tables.Add(dt1);
				ds.Tables.Add(dt2);
				ds.Tables.Add(dt3);
				ds.Tables.Add(dt4);
				ds.Tables.Add(dt5);
				ds.Tables.Add(dt6);
				if(dt7!=null)
					ds.Tables.Add(dt7);
				return ds;
			}
			else
				return null;
		}

		public DataSet TrouveAbonnementByNom(string Nom,int TypeArchive)
		{
			string FiltreSupp = "";
			if(TypeArchive==1)
				FiltreSupp = " AND Archive = 0 ";
			if(TypeArchive==2)
				FiltreSupp = " AND Archive = 1 ";

            DataTable dt1 = ExecuteCommandeAvecDataTable("SELECT ta1.IdAbonnement,ta1.DateCreationAbonnement,ta1.DateDebutFacturation,ta1.Commentaire,ta1.IdContrat,ta1.ClePresente,ta1.Faxfsasd,ta1.DossierBleu,ta1.Periodicite,ta1.Ordre,ta1.Archive ,ta1.Export,ta1.ExportMcc ,ta1.N_TA, ta1.ActiverFacture, ta2.*,pa.TypeAbonnement,pa.TexteAbonnement,pa.IdPatient,pe.IdPersonne,pe.Tel,pe.Nom,pe.PRenom,pe.NumAdresse,pe.CodePostal,pe.Commune,pe.Rue,pe.NumeroDansRue,pe.Batiment,pe.Escalier,pe.Etage,pe.Digicode,pe.Internom,pe.Porte,pe.DateNaissance,pe.Sexe,pe.Age,pe.UniteAge,pe.TexteSup,pe.StopRappelTA from ta_abonnement ta1 inner join ta_abonnementlieufacture ta2 on ta1.IdAbonnement = ta2.TF_IdAbonnement inner join tablepatient pa on pa.IdPAtient = ta1.IdPAtient inner join tablepersonne pe on pe.IdPErsonne = pa.IdPersonne   where pa.TypeAbonnement='TA' and pe.Nom like '%" + Nom + "%'" + FiltreSupp);
			DataTable dt2= ExecuteCommandeAvecDataTable("SELECT tc.* from ta_abonnementcontacts tc inner join ta_abonnement ta on ta.IdAbonnement = tc.IdAbonnement  inner join tablepatient pa on pa.IdPAtient = ta.IdPAtient inner join tablepersonne pe on pe.IdPErsonne = pa.IdPersonne   where pa.TypeAbonnement='TA' and pe.Nom like '%" + Nom + "%'");
			DataTable dt3= ExecuteCommandeAvecDataTable("SELECT tu.* from ta_abonnementurgence tu inner join ta_abonnement ta on ta.IdAbonnement = tu.IdAbonnement  inner join tablepatient pa on pa.IdPAtient = ta.IdPAtient inner join tablepersonne pe on pe.IdPErsonne = pa.IdPersonne   where pa.TypeAbonnement='TA' and pe.Nom like '%" + Nom + "%'");
			DataTable dt4= ExecuteCommandeAvecDataTable("SELECT td.* from ta_abonnementdossier td inner join ta_abonnement ta on ta.IdAbonnement = td.IdAbonnement  inner join tablepatient pa on pa.IdPAtient = ta.IdPAtient inner join tablepersonne pe on pe.IdPErsonne = pa.IdPersonne  where pa.TypeAbonnement='TA' and pe.Nom like '%" + Nom + "%'");
			DataTable dt5= ExecuteCommandeAvecDataTable("SELECT te.* from ta_abonnementcle te inner join ta_abonnement ta on ta.IdAbonnement = te.IdAbonnement  inner join tablepatient pa on pa.IdPAtient = ta.IdPAtient inner join tablepersonne pe on pe.IdPErsonne = pa.IdPersonne  where pa.TypeAbonnement='TA' and pe.Nom like '%" + Nom + "%'");
			DataTable dt6= ExecuteCommandeAvecDataTable("SELECT tj.* from ta_abonnementjournal tj inner join ta_abonnement ta on ta.IdAbonnement = tj.IdAbonnement  inner join tablepatient pa on pa.IdPAtient = ta.IdPAtient inner join tablepersonne pe on pe.IdPErsonne = pa.IdPersonne where  pa.TypeAbonnement='TA' and pe.Nom like '%" + Nom + "%'");
			if(dt1!=null && dt1.Rows.Count>=1 && dt2!=null && dt3!=null && dt4!=null && dt5!=null && dt6!=null)
			{
				DataSet ds = new DataSet();
				ds.Tables.Add(dt1);
				ds.Tables.Add(dt2);
				ds.Tables.Add(dt3);
				ds.Tables.Add(dt4);
				ds.Tables.Add(dt5);
				ds.Tables.Add(dt6);
				return ds;
			}
			else
				return null;
		}

		public DataSet TrouveAbonnementByContrat(string IdContrat,int TypeArchive)
		{
			string FiltreSupp = "";
			if(TypeArchive==1)
				FiltreSupp = " AND Archive = 0 ";
			if(TypeArchive==2)
				FiltreSupp = " AND Archive = 1 ";

            DataTable dt1 = ExecuteCommandeAvecDataTable("SELECT ta1.IdAbonnement,ta1.DateCreationAbonnement,ta1.DateDebutFacturation,ta1.Commentaire,ta1.IdContrat,ta1.ClePresente,ta1.Faxfsasd,ta1.DossierBleu,ta1.Periodicite,ta1.Ordre,ta1.Archive ,ta1.Export,ta1.ExportMcc, ta1.N_TA, ta1.ActiverFacture, ta2.*,pa.TypeAbonnement,pa.TexteAbonnement,pa.IdPatient,pe.IdPersonne,pe.Tel,pe.Nom,pe.PRenom,pe.NumAdresse,pe.CodePostal,pe.Commune,pe.Rue,pe.NumeroDansRue,pe.Batiment,pe.Escalier,pe.Etage,pe.Digicode,pe.Internom,pe.Porte,pe.DateNaissance,pe.Sexe,pe.Age,pe.UniteAge,pe.TexteSup,pe.StopRappelTA from ta_abonnement ta1 inner join ta_abonnementlieufacture ta2 on ta1.IdAbonnement = ta2.TF_IdAbonnement inner join tablepatient pa on pa.IdPAtient = ta1.IdPAtient inner join tablepersonne pe on pe.IdPErsonne = pa.IdPersonne  where ta1.IdContrat = '" + IdContrat + "'" + FiltreSupp + " ORDER BY pe.Nom ASC, pe.Prenom ASC");
			
			if(dt1!=null)
			{
				DataSet ds = new DataSet();
				ds.Tables.Add(dt1);
                return ds;
			}
			else
				return null;			
		}
		public DataSet TrouveAbonnementByTel(string Tel,int TypeArchive)
		{
			string FiltreSupp = "";
			if(TypeArchive==1)
				FiltreSupp = " AND Archive = 0 ";
			if(TypeArchive==2)
				FiltreSupp = " AND Archive = 1 ";

            DataTable dt1 = ExecuteCommandeAvecDataTable("SELECT ta1.IdAbonnement,ta1.DateCreationAbonnement,ta1.DateDebutFacturation,ta1.Commentaire,ta1.IdContrat,ta1.ClePresente,ta1.Faxfsasd,ta1.DossierBleu,ta1.Periodicite,ta1.Ordre,ta1.Archive, ta1.Export,ta1.ExportMcc ,ta1.N_TA, ta1.ActiverFacture, ta2.*,pa.TypeAbonnement,pa.TexteAbonnement,pa.IdPatient,pe.IdPersonne,pe.Tel,pe.Nom,pe.PRenom,pe.NumAdresse,pe.CodePostal,pe.Commune,pe.Rue,pe.NumeroDansRue,pe.Batiment,pe.Escalier,pe.Etage,pe.Digicode,pe.Internom,pe.Porte,pe.DateNaissance,pe.Sexe,pe.Age,pe.UniteAge,pe.TexteSup,pe.StopRappelTA from ta_abonnement ta1 inner join ta_abonnementlieufacture ta2 on ta1.IdAbonnement = ta2.TF_IdAbonnement inner join tablepatient pa on pa.IdPAtient = ta1.IdPAtient inner join tablepersonne pe on pe.IdPErsonne = pa.IdPersonne   where pa.TypeAbonnement = 'TA' and pe.Tel = '" + Tel + "'" + FiltreSupp + " ORDER BY pe.Nom ASC, pe.Prenom ASC");
			DataTable dt2= ExecuteCommandeAvecDataTable("SELECT tc.* from ta_abonnementcontacts tc inner join ta_abonnement ta on ta.IdAbonnement = tc.IdAbonnement inner join tablepatient pa on pa.IdPAtient = ta.IdPAtient inner join tablepersonne pe on pe.IdPErsonne = pa.IdPersonne    where pa.TypeAbonnement = 'TA' and pe.Tel = '" + Tel + "'");
			DataTable dt3= ExecuteCommandeAvecDataTable("SELECT tu.* from ta_abonnementurgence tu inner join ta_abonnement ta on ta.IdAbonnement = tu.IdAbonnement inner join tablepatient pa on pa.IdPAtient = ta.IdPAtient inner join tablepersonne pe on pe.IdPErsonne = pa.IdPersonne    where pa.TypeAbonnement = 'TA' and pe.Tel = '" + Tel + "'");
			DataTable dt4= ExecuteCommandeAvecDataTable("SELECT td.* from ta_abonnementdossier td inner join ta_abonnement ta on ta.IdAbonnement = td.IdAbonnement  inner join tablepatient pa on pa.IdPAtient = ta.IdPAtient inner join tablepersonne pe on pe.IdPErsonne = pa.IdPersonne   where pa.TypeAbonnement = 'TA' and pe.Tel = '" + Tel + "'");
			DataTable dt5= ExecuteCommandeAvecDataTable("SELECT te.* from ta_abonnementcle te inner join ta_abonnement ta on ta.IdAbonnement = te.IdAbonnement inner join tablepatient pa on pa.IdPAtient = ta.IdPAtient inner join tablepersonne pe on pe.IdPErsonne = pa.IdPersonne    where pa.TypeAbonnement = 'TA' and pe.Tel = '" + Tel + "'");
			DataTable dt6= ExecuteCommandeAvecDataTable("SELECT tj.* from ta_abonnementjournal tj inner join ta_abonnement ta on ta.IdAbonnement = tj.IdAbonnement  inner join tablepatient pa on pa.IdPAtient = ta.IdPAtient inner join tablepersonne pe on pe.IdPErsonne = pa.IdPersonne    where pa.TypeAbonnement = 'TA' and pe.Tel = '" + Tel + "'");
			if(dt1!=null && dt1.Rows.Count>=1 && dt2!=null && dt3!=null && dt4!=null && dt5!=null && dt6!=null)
			{
				DataSet ds = new DataSet();
				ds.Tables.Add(dt1);
				ds.Tables.Add(dt2);
				ds.Tables.Add(dt3);
				ds.Tables.Add(dt4);
				ds.Tables.Add(dt5);
				ds.Tables.Add(dt6);
				return ds;
			}
			else
				return null;
		}


        //**********************Domi 09/03/2011

        public DataSet TrouveAbonnementByDateNaiss(string DateNaiss, int TypeArchive)
        {
            string FiltreSupp = "";
            if (TypeArchive == 1)
                FiltreSupp = " AND Archive = 0 ";
            if (TypeArchive == 2)
                FiltreSupp = " AND Archive = 1 ";

            DataTable dt1 = ExecuteCommandeAvecDataTable("SELECT ta1.IdAbonnement,ta1.DateCreationAbonnement,ta1.DateDebutFacturation,ta1.Commentaire,ta1.IdContrat,ta1.ClePresente,ta1.Faxfsasd,ta1.DossierBleu,ta1.Periodicite,ta1.Ordre,ta1.Archive, ta1.Export,ta1.ExportMcc ,ta1.N_TA, ta1.ActiverFacture, ta2.*,pa.TypeAbonnement,pa.TexteAbonnement,pa.IdPatient,pe.IdPersonne,pe.Tel,pe.Nom,pe.PRenom,pe.NumAdresse,pe.CodePostal,pe.Commune,pe.Rue,pe.NumeroDansRue,pe.Batiment,pe.Escalier,pe.Etage,pe.Digicode,pe.Internom,pe.Porte,pe.DateNaissance,pe.Sexe,pe.Age,pe.UniteAge,pe.TexteSup,pe.StopRappelTA from ta_abonnement ta1 inner join ta_abonnementlieufacture ta2 on ta1.IdAbonnement = ta2.TF_IdAbonnement inner join tablepatient pa on pa.IdPAtient = ta1.IdPAtient inner join tablepersonne pe on pe.IdPErsonne = pa.IdPersonne   where pa.TypeAbonnement = 'TA' and pe.DateNaissance = '" + DateNaiss + "'" + FiltreSupp + " ORDER BY pe.Nom ASC, pe.Prenom ASC");
            DataTable dt2 = ExecuteCommandeAvecDataTable("SELECT tc.* from ta_abonnementcontacts tc inner join ta_abonnement ta on ta.IdAbonnement = tc.IdAbonnement inner join tablepatient pa on pa.IdPAtient = ta.IdPAtient inner join tablepersonne pe on pe.IdPErsonne = pa.IdPersonne    where pa.TypeAbonnement = 'TA' and pe.DateNaissance = '" + DateNaiss + "'");
            DataTable dt3 = ExecuteCommandeAvecDataTable("SELECT tu.* from ta_abonnementurgence tu inner join ta_abonnement ta on ta.IdAbonnement = tu.IdAbonnement inner join tablepatient pa on pa.IdPAtient = ta.IdPAtient inner join tablepersonne pe on pe.IdPErsonne = pa.IdPersonne    where pa.TypeAbonnement = 'TA' and pe.DateNaissance = '" + DateNaiss + "'");
            DataTable dt4 = ExecuteCommandeAvecDataTable("SELECT td.* from ta_abonnementdossier td inner join ta_abonnement ta on ta.IdAbonnement = td.IdAbonnement  inner join tablepatient pa on pa.IdPAtient = ta.IdPAtient inner join tablepersonne pe on pe.IdPErsonne = pa.IdPersonne   where pa.TypeAbonnement = 'TA' and pe.DateNaissance = '" + DateNaiss + "'");
            DataTable dt5 = ExecuteCommandeAvecDataTable("SELECT te.* from ta_abonnementcle te inner join ta_abonnement ta on ta.IdAbonnement = te.IdAbonnement inner join tablepatient pa on pa.IdPAtient = ta.IdPAtient inner join tablepersonne pe on pe.IdPErsonne = pa.IdPersonne    where pa.TypeAbonnement = 'TA' and pe.DateNaissance = '" + DateNaiss + "'");
            DataTable dt6 = ExecuteCommandeAvecDataTable("SELECT tj.* from ta_abonnementjournal tj inner join ta_abonnement ta on ta.IdAbonnement = tj.IdAbonnement  inner join tablepatient pa on pa.IdPAtient = ta.IdPAtient inner join tablepersonne pe on pe.IdPErsonne = pa.IdPersonne    where pa.TypeAbonnement = 'TA' and pe.DateNaissance = '" + DateNaiss + "'");
            if (dt1 != null && dt1.Rows.Count >= 1 && dt2 != null && dt3 != null && dt4 != null && dt5 != null && dt6 != null)
            {
                DataSet ds = new DataSet();
                ds.Tables.Add(dt1);
                ds.Tables.Add(dt2);
                ds.Tables.Add(dt3);
                ds.Tables.Add(dt4);
                ds.Tables.Add(dt5);
                ds.Tables.Add(dt6);
                return ds;
            }
            else
                return null;
        }

     
        public DataSet TrouveAbonnementByNFacture(string Nfacture, int TypeArchive)
        {
            string FiltreSupp = "";
            if (TypeArchive == 1)
                FiltreSupp = " AND Archive = 0 ";
            if (TypeArchive == 2)
                FiltreSupp = " AND Archive = 1 ";
            DataTable dt1 = ExecuteCommandeAvecDataTable("SELECT ta1.IdAbonnement,ta1.DateCreationAbonnement,ta1.DateDebutFacturation,ta1.Commentaire,ta1.IdContrat,ta1.ClePresente,ta1.Faxfsasd,ta1.DossierBleu,ta1.Periodicite,ta1.Ordre,ta1.Archive,ta1.Export,ta1.ExportMcc, ta1.N_TA, ta2.*,pa.TypeAbonnement,pa.TexteAbonnement,pa.IdPatient,pe.IdPersonne,pe.Tel,pe.Nom,pe.PRenom,pe.NumAdresse,pe.CodePostal,pe.Commune,pe.Rue,pe.NumeroDansRue,pe.Batiment,pe.Escalier,pe.Etage,pe.Digicode,pe.Internom,pe.Porte,pe.DateNaissance,pe.Sexe,pe.Age,pe.UniteAge,pe.TexteSup,pe.StopRappelTA from ta_abonnement ta1 inner join ta_abonnementlieufacture ta2 on ta1.IdAbonnement = ta2.TF_IdAbonnement inner join ta_abonnementcle tcle on tcle.IdAbonnement = ta1.IdAbonnement  inner join tablepatient pa on pa.IdPAtient = ta1.IdPAtient inner join tablepersonne pe on pe.IdPErsonne = pa.IdPersonne inner join ta_factures taf on ta1.IdAbonnement = taf.Idabonnement  where NFacture = '" + Nfacture + "'" + FiltreSupp + " ORDER BY pe.Nom ASC, pe.Prenom ASC");
            if (dt1 != null)
            {
                DataSet ds = new DataSet();
                ds.Tables.Add(dt1);
                return ds;
            }
            else
                return null;
        }

		public DataSet TrouveAbonnementByCle(string Cle,int TypeArchive)
		{
			string FiltreSupp = "";
			if(TypeArchive==1)
				FiltreSupp = " AND Archive = 0 ";
			if(TypeArchive==2)
				FiltreSupp = " AND Archive = 1 ";

            DataTable dt1 = ExecuteCommandeAvecDataTable("SELECT ta1.IdAbonnement,ta1.DateCreationAbonnement,ta1.DateDebutFacturation,ta1.Commentaire,ta1.IdContrat,ta1.ClePresente,ta1.Faxfsasd,ta1.DossierBleu,ta1.Periodicite,ta1.Ordre,ta1.Archive,ta1.Export,ta1.ExportMcc, ta1.N_TA, ta1.ActiverFacture, ta2.*,pa.TypeAbonnement,pa.TexteAbonnement,pa.IdPatient,pe.IdPersonne,pe.Tel,pe.Nom,pe.PRenom,pe.NumAdresse,pe.CodePostal,pe.Commune,pe.Rue,pe.NumeroDansRue,pe.Batiment,pe.Escalier,pe.Etage,pe.Digicode,pe.Internom,pe.Porte,pe.DateNaissance,pe.Sexe,pe.Age,pe.UniteAge,pe.TexteSup,pe.StopRappelTA from ta_abonnement ta1 inner join ta_abonnementlieufacture ta2 on ta1.IdAbonnement = ta2.TF_IdAbonnement inner join ta_abonnementcle tcle on tcle.IdAbonnement = ta1.IdAbonnement  inner join tablepatient pa on pa.IdPAtient = ta1.IdPAtient inner join tablepersonne pe on pe.IdPErsonne = pa.IdPersonne   where NumeroCle = '" + Cle + "'" + FiltreSupp + " ORDER BY pe.Nom ASC, pe.Prenom ASC");
			DataTable dt2= ExecuteCommandeAvecDataTable("SELECT tc.* from ta_abonnementcontacts tc  inner join ta_abonnementcle tce on tc.IdAbonnement = tce.IdAbonnement  where NumeroCle = '" + Cle + "'");
			DataTable dt3= ExecuteCommandeAvecDataTable("SELECT tu.* from ta_abonnementurgence tu  inner join ta_abonnementcle tce on tu.IdAbonnement = tce.IdAbonnement  where NumeroCle = '" + Cle + "'");
			DataTable dt4= ExecuteCommandeAvecDataTable("SELECT td.* from ta_abonnementdossier td  inner join ta_abonnementcle tce on td.IdAbonnement = tce.IdAbonnement  where NumeroCle = '" + Cle + "'");
			DataTable dt5= ExecuteCommandeAvecDataTable("SELECT te.* from ta_abonnementcle te   where NumeroCle = '" + Cle + "'");
			DataTable dt6= ExecuteCommandeAvecDataTable("SELECT tj.* from ta_abonnementjournal tj inner join ta_abonnementcle te on te.IdAbonnement = tj.IdAbonnement  where NumeroCle = '" + Cle + "'");

            if(dt1!=null && dt1.Rows.Count>=1 && dt2!=null && dt3!=null && dt4!=null && dt5!=null && dt6!=null)
			{
				DataSet ds = new DataSet();
				ds.Tables.Add(dt1);
				ds.Tables.Add(dt2);
				ds.Tables.Add(dt3);
				ds.Tables.Add(dt4);
				ds.Tables.Add(dt5);
				ds.Tables.Add(dt6);
				return ds;
			}
			else
				return null;
		}


		public DataSet TrouveAbonnementFactBloque(int TypeArchive)
		{
			string FiltreSupp = "";
			if (TypeArchive == 1)
				FiltreSupp = " AND Archive = 0 ";
			if (TypeArchive == 2)
				FiltreSupp = " AND Archive = 1 ";

			DataTable dt1 = ExecuteCommandeAvecDataTable("SELECT ta1.IdAbonnement,ta1.DateCreationAbonnement,ta1.DateDebutFacturation,ta1.Commentaire,ta1.IdContrat,ta1.ClePresente,ta1.Faxfsasd,ta1.DossierBleu,ta1.Periodicite,ta1.Ordre,ta1.Archive ,ta1.Export,ta1.ExportMcc ,ta1.N_TA, ta1.ActiverFacture, ta2.*,pa.TypeAbonnement,pa.TexteAbonnement,pa.IdPatient," +
															"pe.IdPersonne,pe.Tel,pe.Nom,pe.PRenom,pe.NumAdresse,pe.CodePostal,pe.Commune,pe.Rue,pe.NumeroDansRue,pe.Batiment,pe.Escalier,pe.Etage,pe.Digicode,pe.Internom,pe.Porte,pe.DateNaissance,pe.Sexe,pe.Age,pe.UniteAge,pe.TexteSup,pe.StopRappelTA " +
															"FROM ta_abonnement ta1 INNER JOIN ta_abonnementlieufacture ta2 on ta1.IdAbonnement = ta2.TF_IdAbonnement " +
															"INNER JOIN tablepatient pa on pa.IdPAtient = ta1.IdPAtient " +
															"INNER JOIN tablepersonne pe on pe.IdPErsonne = pa.IdPersonne " +
															"WHERE pa.TypeAbonnement='TA' AND ta1.ActiverFacture = 0 " + FiltreSupp);

			DataTable dt2 = ExecuteCommandeAvecDataTable("SELECT tc.* FROM ta_abonnementcontacts tc " +
														 "INNER JOIN ta_abonnement ta on ta.IdAbonnement = tc.IdAbonnement " +
														 "INNER JOIN tablepatient pa on pa.IdPAtient = ta.IdPAtient " +
														 "INNER JOIN tablepersonne pe on pe.IdPErsonne = pa.IdPersonne " +
														 "WHERE pa.TypeAbonnement='TA' AND ta.ActiverFacture = 0 ");

			DataTable dt3 = ExecuteCommandeAvecDataTable("SELECT tu.* FROM ta_abonnementurgence tu " +
														 "INNER JOIN ta_abonnement ta on ta.IdAbonnement = tu.IdAbonnement " +
														 "INNER JOIN tablepatient pa on pa.IdPAtient = ta.IdPAtient " +
														 "INNER JOIN tablepersonne pe on pe.IdPErsonne = pa.IdPersonne " +
														 "WHERE pa.TypeAbonnement='TA' AND ta.ActiverFacture = 0 ");
			
			DataTable dt4 = ExecuteCommandeAvecDataTable("SELECT td.* FROM ta_abonnementdossier td " +
														 "INNER JOIN ta_abonnement ta on ta.IdAbonnement = td.IdAbonnement " +
														 "INNER JOIN tablepatient pa on pa.IdPAtient = ta.IdPAtient " +
														 "INNER JOIN tablepersonne pe on pe.IdPErsonne = pa.IdPersonne " +
														 "WHERE pa.TypeAbonnement='TA' AND ta.ActiverFacture = 0 ");
			
			DataTable dt5 = ExecuteCommandeAvecDataTable("SELECT te.* FROM ta_abonnementcle te " +
														 "INNER JOIN ta_abonnement ta on ta.IdAbonnement = te.IdAbonnement " +
														 "INNER JOIN tablepatient pa on pa.IdPAtient = ta.IdPAtient " +
														 "INNER JOIN tablepersonne pe on pe.IdPErsonne = pa.IdPersonne " +
														 "WHERE pa.TypeAbonnement='TA' AND ta.ActiverFacture = 0 ");

			DataTable dt6 = ExecuteCommandeAvecDataTable("SELECT tj.* FROM ta_abonnementjournal tj " +
														 "INNER JOIN ta_abonnement ta on ta.IdAbonnement = tj.IdAbonnement " +
														 "INNER JOIN tablepatient pa on pa.IdPAtient = ta.IdPAtient " +
														 "INNER JOIN tablepersonne pe on pe.IdPErsonne = pa.IdPersonne " +
														 "WHERE  pa.TypeAbonnement='TA' AND ta.ActiverFacture = 0 ");

			if (dt1 != null && dt1.Rows.Count >= 1 && dt2 != null && dt3 != null && dt4 != null && dt5 != null && dt6 != null)
			{
				DataSet ds = new DataSet();
				ds.Tables.Add(dt1);
				ds.Tables.Add(dt2);
				ds.Tables.Add(dt3);
				ds.Tables.Add(dt4);
				ds.Tables.Add(dt5);
				ds.Tables.Add(dt6);
				return ds;
			}
			else
				return null;
		}


		public bool SauvegardeAbonnement(DataSet ds,bool PremiereFois)
		{
			if(ds==null || ds.Tables.Count!=6 && ds.Tables.Count!=7) return false;

			string Requete2 = "update ta_abonnementlieufacture set ";
			string values2= "";

			string Requete5 = "update ta_abonnementdossier set ";
			string values5= "";

			for(int j=0;j<ds.Tables[0].Columns.Count;j++)
			{		
				if(ds.Tables[0].Columns[j].ColumnName.ToLower()=="idabonnement" ||  ds.Tables[0].Columns[j].ColumnName.ToLower().IndexOf("tf")!=0 ) continue;
				
				if(ds.Tables[0].Columns[j].ColumnName.ToLower().IndexOf("date")==0)
				{
					values2+=ds.Tables[0].Columns[j].ColumnName.Replace(" ","") + "=";
					values2+="'" + DateFormatMySql(DateTime.Parse(ds.Tables[0].Rows[0].ItemArray[j].ToString().Replace("'","''"))) + "',";
				}
				else
				{
					values2+=ds.Tables[0].Columns[j].ColumnName.Replace(" ","") + "=";
					values2+="'" + ds.Tables[0].Rows[0].ItemArray[j].ToString().Replace("'","''") + "',";
				}
			}

			for(int j=0;j<ds.Tables[3].Columns.Count;j++)
			{		
				if(ds.Tables[3].Columns[j].ColumnName.ToLower()=="idabonnement") continue;
				
				values5+=ds.Tables[3].Columns[j].ColumnName.Replace(" ","") + "=";
				values5+="'" + ds.Tables[3].Rows[0].ItemArray[j].ToString().Replace("'","''") + "',";				
			}
			
			if(values2.Length>0) values2 = values2.Remove(values2.Length-1,1);
			if(values5.Length>0) values5 = values5.Remove(values5.Length-1,1);

			long index = long.Parse(ds.Tables[0].Rows[0]["IdAbonnement"].ToString());		

	
			string IdPer = ds.Tables[0].Rows[0]["IdPersonne"].ToString();
            bool retour = ExecuteCommandeSansRetour("update ta_abonnement set DateDebutFacturation = '" + DateFormatMySql(DateTime.Parse(ds.Tables[0].Rows[0]["DateDebutFacturation"].ToString())) + "',IdContrat='" + ds.Tables[0].Rows[0]["IdContrat"].ToString() + "',ClePresente='" + ds.Tables[0].Rows[0]["ClePresente"].ToString() + "',FaxFsasd='" + ds.Tables[0].Rows[0]["FaxFsasd"].ToString() + "',DossierBleu='" + ds.Tables[0].Rows[0]["DossierBleu"].ToString() + "',Periodicite='" + ds.Tables[0].Rows[0]["Periodicite"].ToString() + "',Ordre='" + ds.Tables[0].Rows[0]["Ordre"].ToString() + "',Export='" + ds.Tables[0].Rows[0]["Export"].ToString() + "',ExportMcc='" + ds.Tables[0].Rows[0]["ExportMcc"].ToString() + "' , Archive='" + ds.Tables[0].Rows[0]["Archive"].ToString() + "',IdPatient = '" + ds.Tables[0].Rows[0]["IdPatient"].ToString() + "', N_TA = '" + ds.Tables[0].Rows[0]["N_TA"].ToString() + "', ActiverFacture = '" + ds.Tables[0].Rows[0]["ActiverFacture"].ToString() + "' WHERE IdAbonnement = '" + index + "'");
            //MessageBox.Show(ds.Tables[0].Rows[0]["StopRappelTA"].ToString());
            if (ds.Tables[0].Rows[0]["StopRappelTA"].ToString() == "''" || ds.Tables[0].Rows[0]["StopRappelTA"] == DBNull.Value)
                retour = ExecuteCommandeSansRetour("update tablepersonne set DateNaissance = '" + DateFormatMySql(DateTime.Parse(ds.Tables[0].Rows[0]["DateNaissance"].ToString())) + "',Tel='" + ds.Tables[0].Rows[0]["Tel"].ToString() + "',Nom='" + ds.Tables[0].Rows[0]["Nom"].ToString().Replace("'", "''") + "',Prenom='" + ds.Tables[0].Rows[0]["Prenom"].ToString().Replace("'", "''") + "',NumAdresse='" + ds.Tables[0].Rows[0]["NumAdresse"].ToString().Replace("'", "''") + "',CodePostal = '" + ds.Tables[0].Rows[0]["CodePostal"].ToString().Replace("'", "''") + "',Commune='" + ds.Tables[0].Rows[0]["Commune"].ToString().Replace("'", "''") + "',Rue='" + ds.Tables[0].Rows[0]["Rue"].ToString().Replace("'", "''") + "',NumeroDansRue='" + ds.Tables[0].Rows[0]["NumeroDansRue"].ToString().Replace("'", "''") + "',Batiment='" + ds.Tables[0].Rows[0]["Batiment"].ToString().Replace("'", "''") + "',Escalier='" + ds.Tables[0].Rows[0]["Escalier"].ToString().Replace("'", "''") + "',Etage='" + ds.Tables[0].Rows[0]["Etage"].ToString().Replace("'", "''") + "',Digicode='" + ds.Tables[0].Rows[0]["Digicode"].ToString().Replace("'", "''") + "',Internom='" + ds.Tables[0].Rows[0]["Internom"].ToString().Replace("'", "''") + "',Porte='" + ds.Tables[0].Rows[0]["Porte"].ToString().Replace("'", "''") + "',Sexe='" + ds.Tables[0].Rows[0]["Sexe"].ToString().Replace("'", "''") + "',Age='" + ds.Tables[0].Rows[0]["Age"].ToString().Replace("'", "''") + "',UniteAge='" + ds.Tables[0].Rows[0]["UniteAge"].ToString().Replace("'", "''") + "',StopRappelTA= null ,TexteSup='" + ds.Tables[0].Rows[0]["TexteSup"].ToString().Replace("'", "''") + "' WHERE IdPersonne = '" + ds.Tables[0].Rows[0]["IdPersonne"].ToString() + "'");	
            else
            retour = ExecuteCommandeSansRetour("update tablepersonne set DateNaissance = '" + DateFormatMySql(DateTime.Parse(ds.Tables[0].Rows[0]["DateNaissance"].ToString())) + "',Tel='" + ds.Tables[0].Rows[0]["Tel"].ToString() + "',Nom='" + ds.Tables[0].Rows[0]["Nom"].ToString().Replace("'", "''") + "',Prenom='" + ds.Tables[0].Rows[0]["Prenom"].ToString().Replace("'", "''") + "',NumAdresse='" + ds.Tables[0].Rows[0]["NumAdresse"].ToString().Replace("'", "''") + "',CodePostal = '" + ds.Tables[0].Rows[0]["CodePostal"].ToString().Replace("'", "''") + "',Commune='" + ds.Tables[0].Rows[0]["Commune"].ToString().Replace("'", "''") + "',Rue='" + ds.Tables[0].Rows[0]["Rue"].ToString().Replace("'", "''") + "',NumeroDansRue='" + ds.Tables[0].Rows[0]["NumeroDansRue"].ToString().Replace("'", "''") + "',Batiment='" + ds.Tables[0].Rows[0]["Batiment"].ToString().Replace("'", "''") + "',Escalier='" + ds.Tables[0].Rows[0]["Escalier"].ToString().Replace("'", "''") + "',Etage='" + ds.Tables[0].Rows[0]["Etage"].ToString().Replace("'", "''") + "',Digicode='" + ds.Tables[0].Rows[0]["Digicode"].ToString().Replace("'", "''") + "',Internom='" + ds.Tables[0].Rows[0]["Internom"].ToString().Replace("'", "''") + "',Porte='" + ds.Tables[0].Rows[0]["Porte"].ToString().Replace("'", "''") + "',Sexe='" + ds.Tables[0].Rows[0]["Sexe"].ToString().Replace("'", "''") + "',Age='" + ds.Tables[0].Rows[0]["Age"].ToString().Replace("'", "''") + "',UniteAge='" + ds.Tables[0].Rows[0]["UniteAge"].ToString().Replace("'", "''") + "',StopRappelTA=" + ds.Tables[0].Rows[0]["StopRappelTA"].ToString() +",TexteSup='" + ds.Tables[0].Rows[0]["TexteSup"].ToString().Replace("'", "''") + "' WHERE IdPersonne = '" + ds.Tables[0].Rows[0]["IdPersonne"].ToString() + "'");	

            retour = ExecuteCommandeSansRetour(Requete2 +  values2 + " WHERE TF_IdAbonnement = '" + index + "'");
			retour = ExecuteCommandeSansRetour(Requete5 +  values5 + " WHERE IdAbonnement = '" + index + "'");
			retour = ExecuteCommandeSansRetour("DELETE FROM ta_abonnementcontacts where idAbonnement = " + index);

			for(int p=0;p<ds.Tables[1].Rows.Count;p++)
			{
				string requete4 = "insert into ta_abonnementcontacts(IdAbonnement,Lien,Nom,Prenom,Telephone,NumeroRue,Rue,Np,Localite,Tel2,Tel3)";
				requete4+=" values ('" + index +  "','" + ds.Tables[1].Rows[p]["Lien"].ToString().Replace("'","''") +  "','" + ds.Tables[1].Rows[p]["Nom"].ToString().Replace("'","''") +  "','" + ds.Tables[1].Rows[p]["Prenom"].ToString().Replace("'","''") +  "','" + ds.Tables[1].Rows[p]["Telephone"].ToString().Replace("'","''") +  "','" + ds.Tables[1].Rows[p]["NumeroRue"].ToString().Replace("'","''") +  "','" + ds.Tables[1].Rows[p]["Rue"].ToString().Replace("'","''") +  "','" + ds.Tables[1].Rows[p]["Np"].ToString().Replace("'","''") +  "','" + ds.Tables[1].Rows[p]["Localite"].ToString().Replace("'","''") +  "','" + ds.Tables[1].Rows[p]["Tel2"].ToString().Replace("'","''") +  "','" + ds.Tables[1].Rows[p]["Tel3"].ToString().Replace("'","''") +  "')";
				ExecuteCommandeSansRetour(requete4);
			}		
	
			retour = ExecuteCommandeSansRetour("DELETE FROM ta_abonnementurgence where idAbonnement = " + index);
			
			for(int p=0;p<ds.Tables[2].Rows.Count;p++)
			{
				string requete7 = "insert into ta_abonnementurgence(IdAbonnement,Lien,Nom,Prenom,Telephone,NumeroRue,Rue,Np,Localite,Tel2,Tel3)";
				requete7+=" values ('" + index +  "','" + ds.Tables[2].Rows[p]["Lien"].ToString().Replace("'","''") +  "','" + ds.Tables[2].Rows[p]["Nom"].ToString().Replace("'","''") +  "','" + ds.Tables[2].Rows[p]["Prenom"].ToString().Replace("'","''") +  "','" + ds.Tables[2].Rows[p]["Telephone"].ToString().Replace("'","''") +  "','" + ds.Tables[2].Rows[p]["NumeroRue"].ToString().Replace("'","''") +  "','" + ds.Tables[2].Rows[p]["Rue"].ToString().Replace("'","''") +  "','" + ds.Tables[2].Rows[p]["Np"].ToString().Replace("'","''") +  "','" + ds.Tables[2].Rows[p]["Localite"].ToString().Replace("'","''") +  "','" + ds.Tables[2].Rows[p]["Tel2"].ToString().Replace("'","''") +  "','" + ds.Tables[2].Rows[p]["Tel3"].ToString().Replace("'","''") +  "')";
				ExecuteCommandeSansRetour(requete7);
			}	

			retour = ExecuteCommandeSansRetour("update ta_abonnementcle set NumeroCle = '" + ds.Tables[4].Rows[0]["NumeroCle"].ToString() + "',Commentaire='" + ds.Tables[4].Rows[0]["Commentaire"].ToString().Replace("'","''") + "',DateAttribution = '" + DateFormatMySql(DateTime.Now) + "' WHERE IdAbonnement = " + index);

            Fonction z_objFonctionDal = new Fonction();
            z_objFonctionDal.EnregistreModification(index.ToString(), VariablesApplicatives.Utilisateurs.Identifiant, DateTime.Now, Constantes.MODIF_TA, "");
			return retour;
		}

		public bool SupprimeAbonnement(int IdAbonnement)
		{
            return ExecuteCommandeSansRetour(RequetesUpdate.ta_abonnement.ArchiveTrue.Replace("%Abonnement", IdAbonnement.ToString()));
        }
		public bool deSupprimeAbonnement(int IdAbonnement)
		{
            return ExecuteCommandeSansRetour(RequetesUpdate.ta_abonnement.ArchiveFalse.Replace("%Abonnement", IdAbonnement.ToString()));
        }

		public bool SupprimeLigneJournal(int IdLigne)
		{
            return ExecuteCommandeSansRetour(RequetesDelete.ta_abonnementjournal.Ligne.Replace("%Ligne", IdLigne.ToString()));
		}

		public int InsereLigneJournal(int IdAbonnement,string TypeOp,string TexteDe,string TexteA, DateTime DateOp,string ICE,string NbCle,string Commentaire)
		{
			bool reussite =  ExecuteCommandeSansRetour("insert into ta_abonnementjournal (IdAbonnement,TypeOp,EnvoiDe,TexteA,DateOp,NbCle,ICE,Commentaire) values ('" + IdAbonnement + "','" + TypeOp + "','" + TexteDe.Replace("'","''") + "','" + TexteA.Replace("'","''") + "','" + DateFormatMySql(DateOp) + "','" + ICE.Replace("'","''") + "','" + NbCle.Replace("'","''") + "','" + Commentaire.Replace("'","''") + "')");
			if(!reussite) return -1;

            string[][] ret = ExecuteCommandeAvecTabString(RequetesSelect.ta_abonnementjournal.MaxIdAbonnement.Replace("%IdAbonnement", IdAbonnement.ToString()));
            
            if(ret!=null && ret.Length==1)
			{
				return int.Parse(ret[0][0]);
			}
			else
				return -1;
		}

		public string NomSurCle(string cle)
		{
			string[][] retour  =  ExecuteCommandeAvecTabString("SELECT (pe.Nom +' '+pe.Prenom) as 'Nom' from ta_abonnementcle c inner join ta_abonnement a on a.IdAbonnement = c.IdAbonnement inner join tablepatient pa on pa.IdPatient = a.IdPatient inner join tablepersonne pe on pe.IdPersonne = pa.IdPersonne WHERE c.NumeroCle = '" + cle + "' AND a.Archive = 0");
			if(retour!=null && retour.Length>0)
			{
				return retour[0][0];
			}
			else
				return "";
		}

		public DataTable RecupereStructureContactVierge()
		{
            return ExecuteCommandeAvecDataTable(RequetesSelect.ta_abonnementcontacts.IdAbonnementFaux);
        }

		public DataTable RecupereFacturesTA()
		{
            return ExecuteCommandeAvecDataTable(RequetesSelect.ta_Factures.IdAbonnementFaux);
        }

		public DataRow[] RecupereFacturesTAPaye(long N_Facture, long IdAbon)
		{
            DataSet ds = ExecuteCommandeAvecDataSet(RequetesSelect.ta_Factures.NFacture_Idabonnement.Replace("%NFacture", N_Facture.ToString()).Replace("%Idabonnement", IdAbon.ToString()));
            if (ds != null && ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 0)
                ds = ExecuteCommandeAvecDataSet(RequetesSelect.ta_Factures.NFacture_NTA.Replace("%NFacture", N_Facture.ToString()).Replace("%NTA", IdAbon.ToString()));

			int count = ds.Tables[0].Rows.Count;
			if(ds!=null && ds.Tables.Count==1 && ds.Tables[0].Rows.Count>=1)
			{
				DataRow[] rows = new DataRow[ds.Tables[0].Rows.Count];
				IEnumerator enumerator = ds.Tables[0].Rows.GetEnumerator();
				int i=0;
				while(enumerator.MoveNext())
				{
					rows[i] = (DataRow)enumerator.Current;
					i++;
				}

				return rows;
			}
			else
				return null;
		}

		public DataRow[] RecupereFacturesTAPayeMan(long N_Facture)
		{
            DataSet ds = ExecuteCommandeAvecDataSet(RequetesSelect.ta_Factures.NFacture.Replace("%NFacture", N_Facture.ToString()));

				int count = ds.Tables[0].Rows.Count;
				if(ds!=null && ds.Tables.Count==1 && ds.Tables[0].Rows.Count>=1)
				{
					DataRow[] rows = new DataRow[ds.Tables[0].Rows.Count];
					IEnumerator enumerator = ds.Tables[0].Rows.GetEnumerator();
					int i=0;
					while(enumerator.MoveNext())
					{
						rows[i] = (DataRow)enumerator.Current;
						i++;
					}

					return rows;
				}
				else
					return null;
		}

		public DataTable RecupereStructureUrgenceVierge()
		{
            return ExecuteCommandeAvecDataTable(RequetesSelect.ta_abonnementurgence.IdAbonnementFaux);
        }

		public DataSet RecupereFacturesEnccaissee(DateTime Debut, DateTime Fin)
		{
			string fin1 = Fin.ToShortDateString();
			string Debut1 = Debut.ToShortDateString();
			Debut1 = Debut1.ToString().Replace(".","-");
			fin1 = fin1.ToString().Replace(".","-");

			string[] day = Debut1.Split('-');
			Debut1 = day[2].ToString() + "-" + day[1].ToString() + "-" + day[0].ToString();
			day = fin1.Split('-');
			fin1 = day[2].ToString()+"-"+day[1].ToString()+"-"+day[0].ToString();

            string sql = RequetesSelect.factureconsultation.NFactureParDate.Replace("%FacDateAcquitteeDebut", Debut1).Replace("%FacDateAcquitteeFin", fin1);
			DataSet dt = ExecuteCommandeAvecDataSet(sql);
			return dt;
		}

		public DataSet RecupereCodeMedeins()
		{
            return ExecuteCommandeAvecDataSet(RequetesSelect.tablemedecin.Order_Nom);
		}

		public DataSet RecupereFacturesEnccaisseeParMedecin(DateTime Debut, DateTime Fin)
		{
			string fin1 = Fin.ToShortDateString();
			string Debut1 = Debut.ToShortDateString();
			Debut1 = Debut1.ToString().Replace(".","-");
			fin1 = fin1.ToString().Replace(".","-");
			string[] day = Debut1.Split('-');
			Debut1 = day[2].ToString()+"-"+day[1].ToString()+"-"+day[0].ToString();
			day = fin1.Split('-');
			fin1 = day[2].ToString()+"-"+day[1].ToString()+"-"+day[0].ToString();
			string sql = "SELECT Sum(facture_etats.Montant), tablemedecin.Nom AS NomMEd ";
			sql = sql + "FROM factureconsultation INNER JOIN facture_status ON factureconsultation.NFacture = facture_status.NFacture INNER JOIN facture_etats ON facture_status.NFacture = facture_etats.NFacture INNER JOIN facture ON facture_etats.NFacture = facture.NFacture INNER JOIN tableconsultations ON factureconsultation.NConsultation = tableconsultations.NConsultation INNER JOIN tableactes ON tableconsultations.CodeAppel = tableactes.Num INNER JOIN tablemedecin ON tableactes.CodeIntervenant = tablemedecin.CodeIntervenant INNER JOIN tablepatient ON tableconsultations.IndicePatient = tablepatient.IdPatient INNER JOIN tablepersonne ON tablepatient.IdPersonne = tablepersonne.IdPersonne GROUP BY facture.NFacture, facture_status.FacDateEnvoyee, factureconsultation.NConsultation, tableactes.Num, tableactes.CodeIntervenant, tablemedecin.Nom, tableactes.IndicePatient, facture.Tarif, facture_status.FacDateAnnulee, facture.TotalFacture, facture.DateCreation, facture_status.FacDateAcquittee, facture_etats.Etat, facture_etats.Montant, tablepersonne.Tel, tablepersonne.Nom, tablepersonne.Prenom, facture.AdresseDestinataire ";
			sql = sql + "HAVING facture.NFacture>44 AND facture_status.FacDateEnvoyee Is Not Null AND tableactes.CodeIntervenant<>2536 AND facture_status.FacDateAnnulee Is Null AND facture.TotalFacture>0 AND facture_status.FacDateAcquittee >'" + Debut1 +"' AND facture_status.FacDateAcquittee <'" + fin1 +"' AND facture_etats.Etat=6 GROUP BY tableactes.CodeIntervenant";
			DataSet dt = ExecuteCommandeAvecDataSet(sql);
			return dt;
		}
		

		#endregion

		#region Operations sur les factures

		#region Elaboration d'une facture
		
		// Factures attachées à une consultation
		// Renvoie le numéro de facture
		// ou -1 si pas de facture faite
		public long NFactureByConsult(long nConsultation)
		{
            string[][] retour = ExecuteCommandeAvecTabString(RequetesSelect.factureconsultation.Nfacture.Replace("%NConsultation",nConsultation.ToString()));

            if(retour!=null && retour.Length>0)
			{
				return long.Parse(retour[0][0]);
			}
			else
				return -1;
		}

		// Vérification si consultation multiple, on renvoie les consultations liées à un numéro de facture
		public long[] NConsultationByNFacture(long nFacture)
		{
            string[][] retour = ExecuteCommandeAvecTabString(RequetesSelect.factureconsultation.NConsultation.Replace("%NFacture", nFacture.ToString()));

			long[] Num = new long[retour.Length];
			for(int i=0;i<retour.Length;i++)
				Num[i]=long.Parse(retour[i][0]);
			return Num;
		}

		// Création d'une nouvelle facture et récupération de l'enregistrement qui lui correspond
		public DataRow GetNewFactureWithNConsult(long NConsult)
		{
			long Max = 1;
            string[][] retour = ExecuteCommandeAvecTabString(RequetesSelect.facture.MaxNfacture);

            if (retour != null && retour.Length == 1 && retour[0][0] != "")
			{
				Max = int.Parse(retour[0][0]) +1;
			}

            ExecuteCommandeSansRetour(RequetesInsert.facture.Vide.Replace("%NFacture", Max.ToString()).Replace("%DateCreation", DateFormatMySql(DateTime.Now)));
            ExecuteCommandeSansRetour(RequetesInsert.facture_status.Vide.Replace("%Max", Max.ToString()));
            ExecuteCommandeSansRetour(RequetesInsert.factureconsultation.Complet.Replace("%NFacture", Max.ToString()).Replace("%NConsultation", NConsult.ToString()));
            EnregistreEtatFacture(Max, 2, DateTime.Now, "", "", "", 0, DateTime.Now);
			
			//Puis dans tablemodification, on met la création de facture
			Fonction z_objFonctionDal = new Fonction();
			z_objFonctionDal.EnregistreModification(NConsult.ToString(), VariablesApplicatives.Utilisateurs.Identifiant, DateTime.Now, Constantes.CREATION_FACTURE, "");

			return RecuperationFacturesByNFacture(Max)[0];            
		}

		#endregion

		#region Affichage des factures

		public DataRow[] RecuperationFacturesByIdPatient(int IdPatient)
		{
			//Domi 30/03/2011
			//string StrClause = "SELECT f.NFacture,c.NConsultation,(pe.nom+' '+pe.prenom) as 'NomPersonne',f.DateCreation,fs.DateImpression,f.TypeEnvoi,f.Tarif,f.TTT,f.TypeAssurance,f.TypeSortie,f.NAccident,f.DateAccident,f.RefPatient,f.FlagConcerne,f.Commentaire,f.TotalFacture,f.Solde, f.TypeDestinataire,f.CodeDestinataire,f.AdresseDestinataire,f.AdresseDestinataire2, f.FactNum_AVS,TypeDocJoint,UrlDocJoint,fs.FacDateAnnulee,fs.FacDateAcquittee,fs.FacDate1Rappel,fs.FacDate2Rappel,fs.FacDateRappel10,fs.FacDateContentieux,fs.FacDateEncaissee,fs.FacDateDuplicata,fs.FacDateEnvoyee, fs.FacDateImpression10, fs.CessionEnvoi, fs.CessionRecu, fs.RenvFact10p, fs.FactFranchise, fs.PatientIndelicat, fs.PoursuiteDate";
			string StrClause = " SELECT f.NFacture,c.NConsultation,(pe.nom+' '+pe.prenom) as 'NomPersonne',f.DateCreation,fs.DateImpression,f.TypeEnvoi,f.Tarif,f.TTT," +
				                      " f.TypeAssurance,f.TypeSortie,f.NAccident,f.DateAccident,f.RefPatient,f.FlagConcerne,f.Commentaire,f.TotalFacture,f.Solde," +
									  " f.TypeDestinataire,f.CodeDestinataire,f.AdresseDestinataire,f.AdresseDestinataire2, f.FactNum_AVS,TypeDocJoint,UrlDocJoint," +
									  " fs.FacDateAnnulee,fs.FacDateAcquittee,fs.FacDate1Rappel,fs.FacDate2Rappel,fs.FacDate3Rappel,fs.FacDateContentieux,fs.FacDateEncaissee," +
									  " fs.FacDateDuplicata,fs.FacDateEnvoyee, fs.FacDateImpression10, fs.CessionEnvoi, fs.CessionRecu, fs.RenvFact10p, fs.FactFranchise," +
									  " fs.PatientIndelicat, fs.PoursuiteDate, fs.LimiteStopRappel" +
			                          " FROM facture f left join factureconsultation c on c.NFacture = f.NFacture, " +
									  "      tableconsultations co, tablepatient pa, tablepersonne pe, facture_status fs " +
                                      " WHERE pa.IdPatient = " + IdPatient + 
									  " and co.NConsultation = c.NConsultation and pa.IdPatient = co.IndicePatient and pe.IdPersonne = pa.IdPersonne" +
									  " and fs.NFacture = f.NFacture";

            //*********************
			
			DataSet ds = ExecuteCommandeAvecDataSet(StrClause);


			if(ds!=null && ds.Tables.Count==1 && ds.Tables[0].Rows.Count>=0)
			{
				DataRow[] rows = new DataRow[ds.Tables[0].Rows.Count];
				IEnumerator enumerator = ds.Tables[0].Rows.GetEnumerator();
				int i=0;
				while(enumerator.MoveNext())
				{
					rows[i] = (DataRow)enumerator.Current;
					i++;
				}

				return rows;
			}
			else
				return null;
		}

		public long[] RecuperationNFacturesByRange(long Debut,long Fin,int TypeSortie)
		{
			string[][] retour =null;

			if(TypeSortie==-1)
				retour = ExecuteCommandeAvecTabString("SELECT f.NFacture from facture f inner join facture_status fs on fs.NFacture = f.NFacture Where f.NFacture>=" + Debut + " And f.NFacture <=" + Fin + " And fs.FacDateEnvoyee is null And f.TotalFacture !=0 Order by NFacture");
			else if(TypeSortie==1 || TypeSortie==2 || TypeSortie==3)
				retour = ExecuteCommandeAvecTabString("SELECT f.NFacture from facture f inner join facture_status fs on fs.NFacture = f.NFacture Where f.NFacture>=" + Debut + " And f.NFacture <=" + Fin + " And fs.FacDateEnvoyee is null And f.TotalFacture !=0 And f.TypeSortie = " + TypeSortie + " Order by NFacture");

			if(retour!=null)
			{
				long[] ret = new long[retour.Length];
				for(int i=0;i<retour.Length;i++)
				{
					ret[i] = long.Parse(retour[i][0]);
				}
				return ret;
			}
			else
				return new long[0];
		}


		public long[] RecupNFactByRangeAvRappel(long Debut, long Fin)
		{
			string[][] retour = null;
			
			string Sql0 = @"SELECT DISTINCT(f.NFacture) from facture f inner join facture_status fs on fs.NFacture = f.NFacture ";
			Sql0 += " Where f.NFacture >= " + Debut + " And f.NFacture <= " + Fin ;
			Sql0 += " And fs.FacDateEnvoyee is null ";
			Sql0 += " And f.TotalFacture != 0 ";
			Sql0 += " And f.TypeSortie = 2 ";
			//Sql0 += " AND f.CodeDestinataire != 239";    //Sauf Assura
			Sql0 += " UNION ";   //1er rappel en fonction du type envoi (voir en haut declaration)
			Sql0 += " SELECT DISTINCT(f.NFacture) ";     
			Sql0 += " from facture f inner join facture_status fs on fs.NFacture = f.NFacture ";
			Sql0 += "					   Left join facture_etats fe on fe.NFacture = f.NFacture  ";
			Sql0 += " Where f.NFacture >=" + Debut + " And f.NFacture <= " + Fin;
			Sql0 += " AND fs.FacDateEnvoyee < CASE WHEN f.TypeEnvoi = 1 THEN DATEADD(day, -"+ premierRappelTG +", GETDATE())";
			Sql0 += " 							   WHEN f.TypeEnvoi = 2 THEN DATEADD(day, -"+ premierRappelTG + ", GETDATE())";
			Sql0 += "							   WHEN f.TypeEnvoi = 3 THEN DATEADD(day, -"+ premierRappelAss +", GETDATE())";
 		  	Sql0 += "						  END"; 			
			Sql0 += " AND fs.FacDate1Rappel is null";
			Sql0 += " AND (fs.LimiteStopRappel is null OR fs.LimiteStopRappel < GETDATE())";      //rappels stoppé jusqu'au...
			Sql0 += " AND f.Solde > 10"; 
			Sql0 += " And fs.FacDateAcquittee is null ";
			Sql0 += " AND fs.FacDateCession is null ";
			Sql0 += " AND fs.FacDateContentieux is null ";
			//Sql0 += " AND f.CodeDestinataire != 239";    //Sauf Assura
			Sql0 += " AND fe.CommentaireEtat = 'Fichier xml..Medidata' ";   
			
			Sql0 += " UNION ";  //2eme rappel à 30 jours après le 1er rappel (pour tout le monde)
			Sql0 += " SELECT DISTINCT(f.NFacture) from facture f inner join facture_status fs on fs.NFacture = f.NFacture ";    
			Sql0 += "                                            Left join facture_etats fe on fe.NFacture = f.NFacture ";
			Sql0 += " Where f.NFacture >= " + Debut + " And f.NFacture <= " + Fin;
			Sql0 += " And fs.FacDate1Rappel < DATEADD(day, -" + deuxiemeRappel +", GETDATE())";
			Sql0 += " AND fs.FacDate2Rappel is null";
			Sql0 += " AND (fs.LimiteStopRappel is null OR fs.LimiteStopRappel < GETDATE())";     //rappels stoppé jusqu'au...
			Sql0 += " AND f.Solde > 10";
			Sql0 += " And fs.FacDateAcquittee is null ";
			Sql0 += " AND fs.FacDateCession is null ";
			Sql0 += " AND fs.FacDateContentieux is null ";
			//Sql0 += " AND f.CodeDestinataire != 239";   //Sauf Assura
			Sql0 += " AND fe.CommentaireEtat = 'Fichier xml..Medidata'";

			/*Sql0 += " UNION ";     //3eme rappel à 30 jours (après le 2eme) et seulement pour les assurances
			Sql0 += " SELECT DISTINCT(f.NFacture) from facture f inner join facture_status fs on fs.NFacture = f.NFacture ";    
			Sql0 += "                                            Left join facture_etats fe on fe.NFacture = f.NFacture "; 
			Sql0 += " Where f.NFacture >= " + Debut + " And f.NFacture <= " + Fin;
			Sql0 += " And fs.FacDate2Rappel < DATEADD(day, -" + troixiemeRappel + ", GETDATE())";
			Sql0 += " AND fs.FacDate3Rappel is null";
			Sql0 += " AND (fs.LimiteStopRappel is null OR fs.LimiteStopRappel < GETDATE())";     //rappels stoppé jusqu'au...
			Sql0 += " AND f.Solde > 10";
			Sql0 += " And fs.FacDateAcquittee is null ";
			Sql0 += " AND fs.FacDateCession is null ";
			Sql0 += " AND fs.FacDateContentieux is null ";
			Sql0 += " AND f.TypeEnvoi = 3 ";        //			
			//Sql0 += " AND f.CodeDestinataire != 239";   //Sauf Assura
			Sql0 += " AND fe.CommentaireEtat = 'Fichier xml..Medidata'";*/
			Sql0 += " Order by NFacture";

			retour = ExecuteCommandeAvecTabString(Sql0);

			if (retour != null)
			{
				long[] ret = new long[retour.Length];
				for (int i = 0; i < retour.Length; i++)
				{
					ret[i] = long.Parse(retour[i][0]);
				}
				return ret;
			}
			else
				return new long[0];
		}


		public long RecuperationNFacturesEnvoiDebut()
		{
            string[][] retour = ExecuteCommandeAvecTabString(RequetesSelect.facture.MinNfactureNonEnvoye);
			if(retour!=null && retour.Length>0 && retour[0][0]!=System.DBNull.Value.ToString())
				return long.Parse(retour[0][0]);
			else
				return -1;
		}

		public long RecuperationNFacturesEnvoiFin()
		{
            string[][] retour = ExecuteCommandeAvecTabString(RequetesSelect.facture.MaxNfactureNonEnvoye);

			if(retour!=null && retour.Length>0 && retour[0][0]!=System.DBNull.Value.ToString())
				return long.Parse(retour[0][0]);
			else
				return -1;
		}

		public DataRow[] RecuperationFacturesByNFacture(long NFacture)
		{
			DataSet ds = ExecuteCommandeAvecDataSet(RequetesSelect.facture.Tout.Replace("%NFacture" , NFacture.ToString()));

			if(ds!=null && ds.Tables.Count==1 && ds.Tables[0].Rows.Count>=1)
			{
				DataRow[] rows = new DataRow[ds.Tables[0].Rows.Count];
				IEnumerator enumerator = ds.Tables[0].Rows.GetEnumerator();
				int i=0;
				while(enumerator.MoveNext())
				{
					rows[i] = (DataRow)enumerator.Current;
					i++;
				}

				return rows;
			}
			else
				return null;
		}

		public DataRow[] RecuperationMedByNFacture(long NFacture)
		{
			//string sql = "SELECT tableactes.CodeIntervenant, facture.NFacture FROM ((facture INNER JOIN factureconsultation ON facture.NFacture = factureconsultation.NFacture) INNER JOIN tableconsultations ON factureconsultation.NConsultation = tableconsultations.NConsultation) INNER JOIN tableactes ON tableconsultations.CodeAppel = tableactes.Num where facture.NFacture = " + NFacture;
			DataSet ds = ExecuteCommandeAvecDataSet("SELECT tableactes.CodeIntervenant, facture.NFacture " +
				                                    " FROM ((facture INNER JOIN factureconsultation ON facture.NFacture = factureconsultation.NFacture)" +
													               " INNER JOIN tableconsultations ON factureconsultation.NConsultation = tableconsultations.NConsultation)" +
																   " INNER JOIN tableactes ON tableconsultations.CodeAppel = tableactes.Num " +
																   " where facture.NFacture = " + NFacture);
			if(ds!=null && ds.Tables.Count==1 && ds.Tables[0].Rows.Count>=1)
			{
				DataRow[] rows = new DataRow[ds.Tables[0].Rows.Count];
				IEnumerator enumerator = ds.Tables[0].Rows.GetEnumerator();
				int i=0;
				while(enumerator.MoveNext())
				{
					rows[i] = (DataRow)enumerator.Current;
					i++;
				}

				return rows;
			}
			else
				return null;
		}

		public DataRow[] RecuperationFacturesByNConsult(long NConsult)
		{
			//Domi 30/03/2011
			//Remettre qd gestion rappel 10%
			//DataSet ds = ExecuteCommandeAvecDataSet("SELECT f.NFacture,c.NConsultation,(pe.nom+' '+pe.prenom) as 'NomPersonne',f.DateCreation,fs.DateImpression,f.TypeEnvoi,f.Tarif,f.TTT,f.TypeAssurance,f.TypeSortie,f.NAccident,f.DateAccident,f.RefPatient,f.FlagConcerne,f.Commentaire,f.TotalFacture,f.Solde,f.TypeDestinataire,f.CodeDestinataire,f.AdresseDestinataire,f.AdresseDestinataire2,f.FactNum_AVS,TypeDocJoint,UrlDocJoint,fs.FacDateAnnulee,fs.FacDateAcquittee,fs.FacDate1Rappel,fs.FacDate2Rappel,fs.FacDateRappel10,fs.FacDateContentieux,fs.FacDateEncaissee,fs.FacDateDuplicata,fs.FacDateEnvoyee, fs.FacDateImpression10, fs.CessionEnvoi, fs.CessionRecu, fs.RenvFact10p, fs.FactFranchise, fs.PatientIndelicat, fs.PoursuiteDate from facture f inner join facture_status fs on fs.NFacture = f.NFacture left join factureconsultation c on c.NFacture = f.NFacture inner join tableconsultations co on co.NConsultation = c.NConsultation inner join tablepatient pa on pa.IdPatient = co.IndicePatient inner join tablepersonne pe on pe.IdPersonne=  pa.IdPersonne where c.NConsultation = " + NConsult + " Order By f.NFacture DESC");
			DataSet ds = ExecuteCommandeAvecDataSet("SELECT f.NFacture,c.NConsultation,(pe.nom+' '+pe.prenom) as 'NomPersonne',f.DateCreation,fs.DateImpression,f.TypeEnvoi," +
													" f.Tarif,f.TTT,f.TypeAssurance,f.TypeSortie,f.NAccident,f.DateAccident,f.RefPatient,f.FlagConcerne,f.Commentaire," +
													" f.TotalFacture,f.Solde,f.TypeDestinataire,f.CodeDestinataire,f.AdresseDestinataire,f.AdresseDestinataire2,f.FactNum_AVS," +
													" TypeDocJoint,UrlDocJoint,fs.FacDateAnnulee,fs.FacDateAcquittee,fs.FacDate1Rappel,fs.FacDate2Rappel,fs.FacDate3Rappel," +
													" fs.FacDateContentieux,fs.FacDateEncaissee,fs.FacDateDuplicata,fs.FacDateEnvoyee, fs.FacDateImpression10, fs.CessionEnvoi," +
													" fs.CessionRecu, fs.RenvFact10p, fs.FactFranchise, fs.PatientIndelicat, fs.PoursuiteDate, fs.LimiteStopRappel " +
													" from facture f inner join facture_status fs on fs.NFacture = f.NFacture " +
													                     " left join factureconsultation c on c.NFacture = f.NFacture " +
																		 " inner join tableconsultations co on co.NConsultation = c.NConsultation " +
																		 " inner join tablepatient pa on pa.IdPatient = co.IndicePatient " +
																		 " inner join tablepersonne pe on pe.IdPersonne = pa.IdPersonne " +
																		 " where c.NConsultation = " + NConsult + " Order By f.NFacture DESC");
			//***************************

			if (ds!=null && ds.Tables.Count==1 && ds.Tables[0].Rows.Count>=1)
			{
				DataRow[] rows = new DataRow[ds.Tables[0].Rows.Count];
				IEnumerator enumerator = ds.Tables[0].Rows.GetEnumerator();
				int i=0;
				while(enumerator.MoveNext())
				{
					rows[i] = (DataRow)enumerator.Current;
					i++;
				}

				return rows;
			}
			else
				return null;
		}

        //Retourne le facteur scalaire interne en fonction des titre des médecins
        public double Val_Scale_factor_mt(long NConsultation)
        {
			/*string sql0 = @"select CASE WHEN CHARINDEX( 'Médecin praticien', tm.Commentaire) <> 0 
                                        THEN CASE WHEN ta.DAP < '01.01.2018' then '1' else '0.93' END
                                        ELSE '1' END AS Coefficient       
                                        from tableconsultations tc, tableactes ta, tablemedecin tm
                                        where tc.CodeAppel = ta.Num
                                        and tm.CodeIntervenant = ta.CodeIntervenant
                                        and tc.Nconsultation = ";*/
			string sql0 = @"select CASE WHEN MedInterne = 0 
                                        THEN CASE WHEN ta.DAP < '01.01.2018' then '1' else '0.93' END
                                        ELSE '1' END AS Coefficient       
                                        from tableconsultations tc, tableactes ta, tablemedecin tm
                                        where tc.CodeAppel = ta.Num
                                        and tm.CodeIntervenant = ta.CodeIntervenant
                                        and tc.Nconsultation = ";

			string[][] retour = this.ExecuteCommandeAvecTabString(sql0 + NConsultation);
              if (retour != null && retour.Length > 0)
                  return Convert.ToDouble(retour[0][0]);
              else
                  return 0.93;
           // return 0.93;
        }


        //Retourne le facteur scalaire interne en fonction des titre des médecins ET du Tarmed (LAMAL ou autre)
        public string[] Val_Scale_factor_mt_Tarmed(long NConsultation)
        {			
			string sql0 = @"SELECT CASE WHEN MedInterne = 0 
                                               THEN CASE WHEN f.Tarif in (3,4,5) and (f.TTT = 1 OR f.TTT = 2)
                                                         THEN '1'
                                                         ELSE CASE WHEN ta.DAP < '01.01.2018' 
                                                                   THEN '1' 
                                                                   ELSE '0.93' END
                                                              END
                                               ELSE '1' END AS Coefficient,
                                          CASE WHEN ta.DAP > '31.12.2017' 
                                               THEN CASE WHEN f.Tarif in (3,4,5) and (f.TTT = 1 OR f.TTT = 2)
                                                         THEN 'LAA-AM-AI' 
                                                         ELSE 'LAMAL' END                                                         
                                               ELSE 'LAA-AM-AI' END AS TypeTarmed   
                                    FROM facture f, factureconsultation fc, tableconsultations tc, tableactes ta, tablemedecin tm
                                    WHERE f.NFacture = fc.NFacture
                                    AND fc.NConsultation = tc.NConsultation
                                    AND tc.CodeAppel = ta.Num
                                    AND tm.CodeIntervenant = ta.CodeIntervenant
                                    AND fc.NConsultation =" + NConsultation;

			string[][] retour = this.ExecuteCommandeAvecTabString(sql0);
            string[] Resultat = new string[2]; 
            
            if (retour != null && retour.Length > 0)
            {
                Console.WriteLine(retour[0][1].ToString());
                Resultat[0] = retour[0][0].ToString();
                Resultat[1] = retour[0][1].ToString();
                return Resultat;
            }
            else
            {
                Resultat[0] = "0.93";
                Resultat[1] = "LAMAL";
                return Resultat;
            }            
        }

        //Retourne le facteur scalaire interne en fonction des titre des médecins ET du Tarmed (LAMAL ou autre)
        //Avec le N° de Consult ET Facture
        public string[] Val_Scale_factor_mt_Tarmed_AvFact(long NConsultation, long NFacture)
        {			
			string sql0 = @"SELECT CASE WHEN MedInterne = 0 
                                               THEN CASE WHEN f.Tarif in (3,4,5) and (f.TTT = 1 OR f.TTT = 2)
                                                         THEN '1'
                                                         ELSE CASE WHEN ta.DAP < '01.01.2018' 
                                                                   THEN '1' 
                                                                   ELSE '0.93' END
                                                              END
                                               ELSE '1' END AS Coefficient,
                                          CASE WHEN ta.DAP > '31.12.2017' 
                                               THEN CASE WHEN f.Tarif in (3,4,5) and (f.TTT = 1 OR f.TTT = 2)
                                                         THEN 'LAA-AM-AI' 
                                                         ELSE 'LAMAL' END                                                         
                                               ELSE 'LAA-AM-AI' END AS TypeTarmed   
                                    FROM facture f, factureconsultation fc, tableconsultations tc, tableactes ta, tablemedecin tm
                                    WHERE f.NFacture = fc.NFacture
                                    AND fc.NConsultation = tc.NConsultation
                                    AND tc.CodeAppel = ta.Num
                                    AND tm.CodeIntervenant = ta.CodeIntervenant
                                    AND fc.NConsultation =" + NConsultation + @"
                                    AND f.NFacture =" + NFacture;

			string[][] retour = this.ExecuteCommandeAvecTabString(sql0);
            string[] Resultat = new string[2]; 
            
            if (retour != null && retour.Length > 0)
            {
                Console.WriteLine(retour[0][1].ToString());
                Resultat[0] = retour[0][0].ToString();
                Resultat[1] = retour[0][1].ToString();
                return Resultat;
            }
            else
            {
                Resultat[0] = "0.93";
                Resultat[1] = "LAMAL";
                return Resultat;
            }            
        }
        


        //Retourne le facteur scalaire interne en fonction des titre des médecins ET du Tarmed (LAMAL ou autre)
        public string[] Quel_Tarmed_PListe(long NConsultation)
        {
            string sql0 = @"SELECT CASE WHEN ta.DAP > '31.12.2017' 
                                        THEN 'LAMAL'                                                          
                                        ELSE 'LAA-AM-AI' END AS TypeTarmed   
                                    FROM tableconsultations tc, tableactes ta, tablemedecin tm
                                    WHERE tc.CodeAppel = ta.Num
                                    AND tm.CodeIntervenant = ta.CodeIntervenant
                                    AND tc.NConsultation =" + NConsultation;
           
            string[][] retour = this.ExecuteCommandeAvecTabString(sql0);
            string[] Resultat = new string[1];

            if (retour != null && retour.Length > 0)
            {
                Console.WriteLine(retour[0][0].ToString());
                Resultat[0] = retour[0][0].ToString();                
                return Resultat;
            }
            else
            {                
                Resultat[0] = "LAMAL";
                return Resultat;
            }
        }

        //Retourne l'Email d'une personne à partir de Npersonne
        public string EmailPersonne(long NPersonne)
        {
            string sql0 = @"select pe.Email from tablePersonne pe where pe.IdPersonne = ";

            string[][] retour = ExecuteCommandeAvecTabString(sql0 + NPersonne);
            if (retour != null && retour.Length > 0)
                return retour[0][0].ToString();
            else
                return "";            
        }

		public long RecuperationConsultationPrincipaleByNFacture(long NFacture)
		{
            string[][] retour = ExecuteCommandeAvecTabString(RequetesSelect.factureconsultation.NConsultationPrincipale.Replace("%NFacture", NFacture.ToString()));
			if(retour==null || retour.Length==0)
				return -1;
			else
				return int.Parse(retour[0][0]);
		}

		public string[][] RecuperationPrestationsByNFacture(long NFacture, string TarmedVersion)
		{
            DataSet ds = ExecuteCommandeAvecDataSet(RequetesSelect.facture_prest.NFacture_fac_tarif.Replace("%NFacture", NFacture.ToString()));

			string[][] retour = new string[ds.Tables[0].Rows.Count][];
			for(int i=0;i<ds.Tables[0].Rows.Count;i++)
			{
				retour[i] = new string[11];
				retour[i][0] = ds.Tables[0].Rows[i]["TypePrest"].ToString();
				retour[i][1] = ds.Tables[0].Rows[i]["Indice"].ToString();
				retour[i][2] = "";
				string[][] libelle = null;
				if(retour[i][0]=="1")    //Si Tarmed
				{
                    libelle = ExecuteCommandeAvecTabString(RequetesSelect.fac_prestations.NPrestation.Replace("%NPrestation", retour[i][1]).Replace("%TarmedVersion", TarmedVersion));
					if(libelle!=null && libelle.Length==1)
						retour[i][2] = libelle[0][0];
				}
				else if(retour[i][0]=="2")   //Si materiel
				{
                    libelle = ExecuteCommandeAvecTabString(RequetesSelect.fac_tablemateriel.MatLibelle.Replace("%Nt_Materiel", retour[i][1]));					
                    if(libelle!=null && libelle.Length==1)
						retour[i][2] = libelle[0][0];
                    
                    //Puis on change le code Materiel par le Num_Materiel
                   // retour[i][1] = ds.Tables[0].Rows[i]["Num_Materiel"].ToString();
				}
				retour[i][3] = ds.Tables[0].Rows[i]["Qte"].ToString();
				retour[i][4] = ds.Tables[0].Rows[i]["Prix"].ToString();
				if(retour[i][0]=="1" && libelle!=null && libelle.Length==1)
				{
					retour[i][5] = libelle[0][1];
					retour[i][6] = libelle[0][2];
				}
				else if(retour[i][0]=="2")
				{
					retour[i][5] = "";
					retour[i][6] = "";
				}
				retour[i][7] = ds.Tables[0].Rows[i]["Cote"].ToString();
				retour[i][8] = ds.Tables[0].Rows[i]["libelle"].ToString();

				// ajorations, hors-majorations
				if(retour[i][0]=="1" && libelle!=null && libelle.Length==1)
				{
					retour[i][9] = libelle[0][3];
					retour[i][10] = libelle[0][4];
				}
				else if(retour[i][0]=="2")
				{
					retour[i][9] = "0";
					retour[i][10] = "0";
				}
			}	
			return retour;
		}

		#endregion


        #region Operations sur la consultation

        public void EnregistreModification(long nConsultation, string CodeUtilisateur, DateTime datemodif, int TypeModif, string Commentaire)
        {
            ExecuteCommandeSansRetour("INSERT INTO tablemodifications (NConsultation,CodeUtilisateur,DateModif,Type,Commentaire) values ('" + nConsultation + "','" + CodeUtilisateur.Replace("'", "''") + "','" + DateFormatMySql(datemodif) + "'," + (int)TypeModif + ",'" + Commentaire.Replace("'", "''") + "')");
        }


        public bool EnregistreFiche(DataRow row, string Commentaire)
        {
            if (row == null) return false;
            row["Modifie"] = 1;

            // Enregistrement de la personne :
            string dtNaissance = row["DateNaissance"].ToString();
            if (dtNaissance == "")
                dtNaissance = "NULL";
            else
                dtNaissance = "'" + DateFormatMySql(DateTime.Parse(dtNaissance)) + "'";
            string Requete1 = "update tablepersonne set ";
            Requete1 += "Tel = '" + row["TelPatient"].ToString().Replace("'", "''") + "',Nom = '" + row["NomPatient"].ToString().Replace("'", "''") + "',Prenom='" + row["PrenomPatient"].ToString().Replace("'", "''") + "',Commune='" + row["Commune"].ToString().Replace("'", "''") + "',Rue='" + row["Rue"].ToString().Replace("'", "''") + "',NumeroDansRue='" + row["NumeroDansRue"].ToString().Replace("'", "''") + "',";
            Requete1 += "Batiment='" + row["Batiment"].ToString().Replace("'", "''") + "',CodePostal = '" + row["CodePostal"].ToString().Replace("'", "''") + "',Escalier='" + row["Escalier"].ToString().Replace("'", "''") + "',Etage='" + row["Etage"].ToString().Replace("'", "''") + "',Digicode='" + row["Digicode"].ToString().Replace("'", "''") + "',InterNom='" + row["InterNom"].ToString().Replace("'", "''") + "',Porte='" + row["Porte"].ToString().Replace("'", "''") + "',Longitude='" + row["Longitude"].ToString().Replace("'", "''") + "',Latitude='" + row["Latitude"].ToString().Replace("'", "''") + "',";
            Requete1 += "DateNaissance=" + dtNaissance + ",Sexe='" + row["Sexe"].ToString().Replace("'", "''") + "',Age='" + row["Age"].ToString().Replace("'", "''") + "',UniteAge='" + row["UniteAge"].ToString().Replace("'", "''") + "',TexteSup='" + row["TexteSup"].ToString().Replace("'", "''") + "',ListeNoire='" + row["ListeNoire"].ToString().Replace("'", "''") + "',Adm_Batiment='" + row["Adm_Batiment"].ToString().Replace("'", "''") + "',Adm_CodePostal = '" + row["Adm_CodePostal"].ToString().Replace("'", "''") + "',Adm_NumeroDansRue = '" + row["Adm_NumeroDansRue"].ToString().Replace("'", "''") + "',Adm_Rue = '" + row["Adm_Rue"].ToString().Replace("'", "''") + "',Adm_Commune = '" + row["Adm_Commune"].ToString().Replace("'", "''") + "'";
            Requete1 += " WHERE IdPersonne = " + row["IdPersonne"].ToString();

            // Enregistrement du patient :
            string Requete2 = "update tablepatient set SuiviPatient = '" + row["SuiviPatient"].ToString().Replace("'", "''") + "',Approuve=0 WHERE IdPAtient = " + row["IndicePatient"].ToString();

            // Enregistrement du compterendu de la consultation :
            string Requete3 = "update tableconsultations set Modifie=1, CommentaireLibre ='" + row["CommentaireLibre"].ToString().Replace("'", "''") + "' ,Deces ='" + row["Deces"].ToString() + "',TraitementLibre='" + row["TraitementLibre"].ToString().Replace("'", "''") + "',Traitements='" + row["Traitements"].ToString().Replace("'", "''") + "'";
            Requete3 += " WHERE CodeAppel = " + row["Num"].ToString() + " AND IndicePatient = " + row["IndicePatient"].ToString();

            // Si décès de la personne, enregistrement de sa date de décès
            if (row["Deces"].ToString() == "1")
            {
                string Requete4 = "update tablepersonne set DateDeces ='" + DateFormatMySql(DateTime.Parse(row["DAP"].ToString())) + "' WHERE IdPersonne = " + row["IdPersonne"].ToString();
                ExecuteCommandeSansRetour(Requete4);
            }


            try
            {
                bool reussite = ExecuteCommandeSansRetour(Requete1);
                reussite = ExecuteCommandeSansRetour(Requete2);
                reussite = ExecuteCommandeSansRetour(Requete3);
                EnregistreModification(long.Parse(row["NConsultation"].ToString()), "", DateTime.Now, Constantes.MODIF_CONSULT, Commentaire);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        #endregion

        #region Sauvegarde d'une facture

        public bool SauvegardeFacture(DataRow[] Factures, string[][] lignes)
        {
            if (Factures != null && Factures.Length > 0)
            {
                try
                {
                    string strDateAcc = "";
                    if (Factures[0]["DateAccident"].ToString() != System.DBNull.Value.ToString())
                        strDateAcc = "'" + DateFormatMySql(DateTime.Parse(Factures[0]["DateAccident"].ToString())) + "'";
                    else
                        strDateAcc = "NULL";
                    string requete = "update facture set TypeEnvoi=" + Factures[0]["TypeEnvoi"].ToString() + ",Tarif=" + Factures[0]["Tarif"].ToString() + ",TTT=" + Factures[0]["TTT"].ToString() + ",TypeAssurance=" + Factures[0]["TypeAssurance"].ToString();
                    requete += ",TypeSortie=" + Factures[0]["TypeSortie"].ToString() + ",NAccident='" + Factures[0]["NAccident"].ToString().Replace("'", "''") + "',DateAccident=" + strDateAcc + ",RefPatient='" + Factures[0]["RefPatient"].ToString().Replace("'", "''") + "',FlagConcerne=" + Factures[0]["FlagConcerne"].ToString();
                    requete += ",Commentaire='" + Factures[0]["Commentaire"].ToString().Replace("'", "''") + "',TotalFacture= '" + Factures[0]["TotalFacture"].ToString().Replace(",", ".") + "',Solde= '" + Factures[0]["TotalFacture"].ToString().Replace(",", ".") + "',TypeDocJoint=" + Factures[0]["TypeDocJoint"].ToString() + ",TypeDestinataire=" + Factures[0]["TypeDestinataire"].ToString() + ",CodeDestinataire=" + Factures[0]["CodeDestinataire"].ToString() + ",AdresseDestinataire='" + Factures[0]["AdresseDestinataire"].ToString().Replace("'", "''") + ",FactNum_AVS='" + Factures[0]["FactNum_AVS"].ToString().Replace("'", "''") + "' WHERE NFacture = " + Factures[0]["NFacture"].ToString();

                    bool reussite = ExecuteCommandeSansRetour(requete);

                    bool reussitebis = true;
                    ExecuteCommandeSansRetour("delete from facture_prest where NFacture = " + Factures[0]["NFacture"].ToString());
                    for (int i = 0; i < lignes.Length; i++)
                    {
                        if (!ExecuteCommandeSansRetour("insert into facture_prest (NFacture,TypePrest,Indice,Qte,Points,Prix,Cote,TypeTarif,Ordre) values (" + Factures[0]["NFacture"].ToString() + "," + lignes[i][0] + ",'" + lignes[i][1] + "'," + lignes[i][2] + ",'" + lignes[i][6].Replace(",", ".") + "','" + lignes[i][3].Replace(",", ".") + "','" + lignes[i][4].Replace("'", "''") + "'," + lignes[i][5] + "," + i + ")"))
                            reussitebis = false;
                    }

                    //EnregistreEtatFacture(long.Parse(Factures[0]["NFacture"].ToString()),1,DateTime.Now,"","","",0);

                    return reussite && reussitebis;
                }
                catch (Exception exc1)
                {
                    Console.WriteLine(exc1.Message);
                    return false;
                }
            }
            else
                return false;
        }

        public bool SauvegardeFacture(DataRow[] Factures, string[][] lignes, string[] AutresConsultation)
        {
            bool reussite = SauvegardeFacture(Factures, lignes);

            if (reussite)
            {
                ExecuteCommandeSansRetour("DELETE FROM factureconsultations where NFacture = " + Factures[0]["NFacture"].ToString() + " AND Principale = 0");
                if (AutresConsultation != null)
                {
                    foreach (string s in AutresConsultation)
                    {
                        ExecuteCommandeSansRetour("INSERT INTO factureconsultations(NFacture,NConsultation,Principale) values (" + Factures[0]["NFacture"].ToString() + "," + s + ",0)");
                        ExecuteCommandeSansRetour("update tableconsultations set FactureGeneree = 1 WHERE NConsultation = " + s);
                    }
                }
            }
            return reussite;
        }

        public void SupprimeFacture(long NConsultation, long NFacture)
        {
            ExecuteCommandeSansRetour("update tableconsultations set FactureGeneree = 0 WHERE NConsultation = " + NConsultation);
            ExecuteCommandeSansRetour("delete from facture_prest where NFacture = " + NFacture);
            ExecuteCommandeSansRetour("delete from factureconsultation where NFacture = " + NFacture);
            ExecuteCommandeSansRetour("delete from facture_status where NFacture = " + NFacture);
            ExecuteCommandeSansRetour("delete from facture where NFacture = " + NFacture);
            EnregistreModification(NConsultation, "", DateTime.Now, Constantes.SUPPRESSION_FACTURE, "");
        }

        #endregion

		#region Retour d'adresses de destinataire
		
		public string GetAdresseFromHotel(int NHotel)
		{
			string[][] strHotel = OutilsExt.OutilsSql.ExecuteCommandeAvecTabString("select h.hotnom,h.hotCpostale,h.hotextlocalite,a.numdansrue,a.rue,a.np,a.commune from hotels h inner join adresses a on a.NAdresse = h.NAdresse WHERE NHotel = " + NHotel);
			if(strHotel!=null && strHotel.Length==1)
			{
				if(strHotel[0][1].Trim()!="") strHotel[0][1]+=" ";
				if(strHotel[0][2]!="")
					return strHotel[0][0] + "\r\n" + strHotel[0][4] + " " + strHotel[0][3] + "\r\n" + strHotel[0][1]  + strHotel[0][2];
				else
					return strHotel[0][0] + "\r\n" + strHotel[0][4] + " " + strHotel[0][3] + "\r\n" + strHotel[0][1]  + strHotel[0][5] + " " + strHotel[0][6];
			}
			else
				return "";
		}
		public string GetAdresseFromCommissariat()
		{
			string[][] strcommissariat = OutilsExt.OutilsSql.ExecuteCommandeAvecTabString("select numerorue,rue,np,commune from commissariat");
			if(strcommissariat!=null && strcommissariat.Length==1)
			{
				return "Hôtel de police" + "\r\n" + strcommissariat[0][1] + " " + strcommissariat[0][0] + "\r\n" + strcommissariat[0][2] + " " + strcommissariat[0][3];					
			}
			else
				return "";
		}
		public string GetAdresseFromPatient(int idPatient)
		{
			string[][] strPatient = OutilsExt.OutilsSql.ExecuteCommandeAvecTabString("select Nom,Prenom,Adm_NumeroDansRue,Adm_Rue,Adm_CodePostal,Adm_Commune,Chez,Sexe from tablepersonne pe inner join tablepatient pa on pa.IdPersonne = pe.IdPersonne WHERE pa.IdPatient = " + idPatient);
			if(strPatient!=null && strPatient.Length==1)
			{
				string sexe = strPatient[0][7];
				sexe = WorkedString.GetSexeFormate(sexe);
				return sexe + " " +  WorkedString.FormatePreNom(strPatient[0][1]) + " "  + strPatient[0][0].ToUpper() +  WorkedString.GetAdresseFormatee(strPatient[0][6],strPatient[0][3],strPatient[0][2],strPatient[0][4],strPatient[0][5]);
			}
			else
				return "";
		}
		public string GetAdresseFromAssurance(int NAssurance)
		{
			string[][] strAssurance = OutilsExt.OutilsSql.ExecuteCommandeAvecTabString("select s.Assnom,s.assCpostale,s.assextlocalite,a.numdansrue,a.rue,a.np,a.commune, s.NAdresse from assurances s inner join adresses a on a.NAdresse = s.NAdresse WHERE s.NAssurance=" + NAssurance);
			int num = int.Parse(strAssurance[0][7].ToString());
			if(strAssurance!=null && strAssurance.Length==1 && int.Parse(strAssurance[0][7].ToString())>0)
			{
				if(strAssurance[0][1].Trim()!="") strAssurance[0][1]+=" ";
				if(strAssurance[0][2]!="")
					return strAssurance[0][0] + "\r\n" + strAssurance[0][4] + " " + strAssurance[0][3] + "\r\n" + strAssurance[0][1] + strAssurance[0][2];
				else
					return strAssurance[0][0] + "\r\n" + strAssurance[0][4] + " " + strAssurance[0][3] + "\r\n" + strAssurance[0][1] + strAssurance[0][5] + " " + strAssurance[0][6];
			}
				//AssAdresseTexte, Assnom,
			else
			{
				strAssurance = OutilsExt.OutilsSql.ExecuteCommandeAvecTabString("select s.Assnom,s.assCpostale,s.assextlocalite,s.AssAdresseTexte from assurances s WHERE s.NAssurance=" + NAssurance);
				if(strAssurance!=null && strAssurance.Length==1)
				{
					if(strAssurance[0][2]!="" && strAssurance[0][1]!="" && strAssurance[0][3]!="")
						return strAssurance[0][0] + "\r\n" + strAssurance[0][3] + "\r\n" + strAssurance[0][1] + " " +strAssurance[0][2];
					else
						return "";
				}
				else
					return "";
			}
		}
		public string GetAdresseFromTiers(int NTiers)
		{
			string[][] strTiers = OutilsExt.OutilsSql.ExecuteCommandeAvecTabString("select s.TiersNom,s.TiersCpostale,s.Tiersextlocalite,a.numdansrue,a.rue,a.np,a.commune from Tiers s inner join adresses a on a.NAdresse = s.NAdresse WHERE NTiers=" + NTiers);
			if(strTiers!=null && strTiers.Length==1)
			{
				if(strTiers[0][1].Trim()!="") strTiers[0][1]+=" ";
				if(strTiers[0][2]!="")
					return strTiers[0][0] + "\r\n" + strTiers[0][4] + " " + strTiers[0][3] + "\r\n" + strTiers[0][1] + strTiers[0][2];
				else if (strTiers[0][1].Trim()!="")
					return strTiers[0][0] + "\r\n" + strTiers[0][1] + "\r\n" + strTiers[0][4] + " " + strTiers[0][3] + "\r\n" + strTiers[0][5] + " " + strTiers[0][6];
				else
					return strTiers[0][0] + "\r\n" + strTiers[0][4] + " " + strTiers[0][3] + "\r\n" + strTiers[0][5] + " " + strTiers[0][6];

			}
			else
				return "";
		}		

		#endregion

		#region Etats de facture

		// Enregistrement d'un nouvel état sur la facture 
		//0: Duplicata, 2: création , 3: Annulation, 4: Réédition, 5:Envoi, 6:Encaissement auto, 9: 10%
		public void EnregistreEtatFacture(long NFacture,int Etat,DateTime DateEtat,string Commentaire,string Param1,string Param2,float Montant,DateTime DateSal)
		{
			if (Etat == 2)  //création
                ExecuteCommandeSansRetour("DELETE FROM facture_etats WHERE NFacture='" + NFacture + "' AND Etat='2'");
            ExecuteCommandeSansRetour("insert into facture_etats (NFacture,Etat,DateEtat,DateOp,CommentaireEtat,Param1,Param2,CodeUtilisateur,Montant,DatePaye) values (" + NFacture + "," + Etat + ",'" + DateFormatMySql(DateEtat) + "','" + DateFormatMySql(DateTime.Now) + "','" + Commentaire.Replace("'", "''") + "','" + Param1.Replace("'", "''") + "','" + Param2.Replace("'", "''") + "','" + VariablesApplicatives.Utilisateurs.Identifiant.Replace("'", "''") + "'," + Montant.ToString().Replace(",", ".") + ",'" + DateFormatMySql(DateSal) + "')");

		}

		// Enregistrement d'un nouveau duplicata
		public void EnregistreDuplicata(long NFacture,string Commentaire,string Demande,string Envoye)
		{
			EnregistreEtatFacture(NFacture,0,DateTime.Now,Commentaire,Demande,Envoye,0,DateTime.Now);
			ExecuteCommandeSansRetour("update facture_status set FacDateDuplicata = '" + DateFormatMySql(DateTime.Now) + "' WHERE NFacture = " + NFacture );
		}

		// Enregistrement d'un nouveau paiement
        public void EnregistrePaiement(long NFacture, float Montant, float Total, string Commentaire, string Moyen, DateTime DatePay, DateTime DateSal)
        {
            EnregistreEtatFacture(NFacture, 6, DatePay, Commentaire, Moyen, "", Montant, DateSal);
            //find the solde
            float Solde = 1000;
            string[][] solde = OutilsExt.OutilsSql.ExecuteCommandeAvecTabString("SELECT Solde from facture WHERE facture.NFacture =" + NFacture);
            if (solde != null && solde.Length != 0 && solde[0][0] != "")
            {
                Solde = float.Parse(solde[0][0]) - Total;
            }
            if (Solde < 1)
            {
                ExecuteCommandeSansRetour("update facture set Solde = 0 WHERE NFacture = " + NFacture);
                ExecuteCommandeSansRetour("update facture_status set FacDateAcquittee = '" + DateFormatMySql(DatePay) + "' WHERE NFacture = " + NFacture);
                SosMedecins.SmartRapport.DAL.Fonction z_objFonction = new SosMedecins.SmartRapport.DAL.Fonction();
                z_objFonction.EncaissementSurPlace(NFacture);
            }
            else
                ExecuteCommandeSansRetour("update facture set Solde = Solde - " + Total + " WHERE NFacture = " + NFacture);
        }

		// Enregistrement de l'annulation d'une facture
		public void EnregistreAnnulation(long NFacture,string Commentaire,bool Reedition)
		{
			if(Reedition)
				EnregistreEtatFacture(NFacture,4,DateTime.Now,Commentaire,"","",0,DateTime.Now);    
			else
				EnregistreEtatFacture(NFacture,3,DateTime.Now,Commentaire,"","",0,DateTime.Now);

			ExecuteCommandeSansRetour("update facture_status set FacDateAnnulee = '" + DateFormatMySql(DateTime.Now) + "' WHERE NFacture = " + NFacture);
			ExecuteCommandeSansRetour("update facture set Solde = '0' WHERE NFacture = " + NFacture);

		}

		// Enregistrement de l'envoi d'une facture
		public void EnregistreEnvoiFacture(long NFacture,string Commentaire)
		{
			EnregistreEtatFacture(NFacture,5,DateTime.Now,Commentaire,"","",0,DateTime.Now);
			ExecuteCommandeSansRetour("update facture_status set FacDateEnvoyee = '" + DateFormatMySql(DateTime.Now) + "' WHERE NFacture = " + NFacture);
		}

		//Maj du nb facture indépendant
		public void MajCptFactureIndependant(int CodeMedecin, int nbFactInd)
		{			
			ExecuteCommandeSansRetour("update tablemedecin set DateMajCpt = '" + DateFormatMySql(DateTime.Now) + "', CptFactM = " + nbFactInd + 
									  " WHERE CodeIntervenant = " + CodeMedecin);
		}

		public void EnregEnvoiFactMedidata(long NFacture, int CreerRappelNum, int RenvFact10p, string Commentaire)
		{
			//EnregistreEtatFacture(NFacture, 5,DateTime.Now, Commentaire, "", "", 0, DateTime.Now);
			string SQL0 = "";

			if (RenvFact10p == 1)      //10%
			{
				EnregistreEtatFacture(NFacture, 9, DateTime.Now, "10 pourcent " + Commentaire, "", "", 0, DateTime.Now);
				SQL0 = "update facture_status set FacDateImpression10 = '" + DateFormatMySql(DateTime.Now) + "', FacDateEnvoyee = '" + DateFormatMySql(DateTime.Now) + "' WHERE NFacture = " + NFacture;
			}
			else if (CreerRappelNum == 0)
			{
				EnregistreEtatFacture(NFacture, 5, DateTime.Now, Commentaire, "", "", 0, DateTime.Now);
				SQL0 = "update facture_status set FacDateEnvoyee = '" + DateFormatMySql(DateTime.Now) + "' WHERE NFacture = " + NFacture;
			}
			else if (CreerRappelNum == 1)        //1er rappel
			{
				EnregistreEtatFacture(NFacture, 5, DateTime.Now, "1er rappel " + Commentaire, "", "", 0, DateTime.Now);
				SQL0 = "update facture_status set FacDate1Rappel = '" + DateFormatMySql(DateTime.Now) + "' WHERE NFacture = " + NFacture;
			}
			else if (CreerRappelNum == 2)   //2eme rappel
			{
				EnregistreEtatFacture(NFacture, 5, DateTime.Now, "2eme rappel " + Commentaire, "", "", 0, DateTime.Now);
				SQL0 = "update facture_status set FacDate2Rappel = '" + DateFormatMySql(DateTime.Now) + "' WHERE NFacture = " + NFacture;
			}
			else if (CreerRappelNum == 3)   //3eme rappel
			{
				EnregistreEtatFacture(NFacture, 5, DateTime.Now, "3eme rappel " + Commentaire, "", "", 0, DateTime.Now);
				SQL0 = "update facture_status set FacDate3Rappel = '" + DateFormatMySql(DateTime.Now) + "' WHERE NFacture = " + NFacture;
			}
			else if (CreerRappelNum == 4)   //Poursuite
			{
				EnregistreEtatFacture(NFacture, 5, DateTime.Now, "Mise en poursuite " + Commentaire, "", "", 0, DateTime.Now);
				SQL0 = "update facture_status set PoursuiteDate = '" + DateFormatMySql(DateTime.Now) + "' WHERE NFacture = " + NFacture;
			}

			//Sinon pas de rappel ou pas de 10%, donc requette standard

			ExecuteCommandeSansRetour(SQL0);
		}

		public string[][] GetEtatsFacture(long NFacture)
		{
			string[][] tab =  ExecuteCommandeAvecTabString("SELECT Etat,DateOp,DateEtat,Montant,Param1,Param2,u.Nom ,CommentaireEtat from facture_etats f inner join tableutilisateur u on f.CodeUtilisateur = u.CodeUtilisateur WHERE f.NFacture = " + NFacture);
			for(int i=0;i<tab.Length;i++)
			{
				int Etat = int.Parse(tab[i][0]);
                tab[i][0] = GetLibelleEtat(Etat);
			}
			return tab;
		}

		public string GetLibelleEtat(int Etat)
		{
			Facturation.RemplirEtatFacture();
			return Facturation.EtatsFacture[Etat].ToString();
		}

		#endregion

		#region Impression des factures

		public DataRow[] InfosMedecinsByTableauFactures(long[] NFacture)
		{
			// on fabrique un tableau avec tous les médecins
			DataTable dt = ExecuteCommandeAvecDataTable(RequetesSelect.tablemedecin.ArchiveFaux);
            
            dt.Columns.Add(new DataColumn("NbFacture",typeof(int)));
			dt.Columns.Add(new DataColumn("Numeros",typeof(string)));
			for(int i=0;i<dt.Rows.Count;i++)
			{
				dt.Rows[i]["NbFacture"] = 0;
				dt.Rows[i]["Numeros"] = "";
			}
            
			foreach(long nfac in NFacture)
			{
				string[][] ret = ExecuteCommandeAvecTabString(" select a.CodeIntervenant from factureconsultation fc inner join tableconsultations c on c.NConsultation = fc.NConsultation inner join tableactes a on a.Num = c.CodeAppel inner join tablemedecin m on m.CodeIntervenant=a.CodeIntervenant where nfacture = " + nfac);
				if(ret!=null && ret.Length==1)
				{
					long CodeIntervenant = long.Parse( ret[0][0]);
					for(int i=0;i<dt.Rows.Count;i++)
					{
						if(dt.Rows[i]["CodeIntervenant"].ToString()==CodeIntervenant.ToString())
						{
							dt.Rows[i]["NbFacture"] = int.Parse(dt.Rows[i]["NbFacture"].ToString()) + 1;
							dt.Rows[i]["Numeros"]+=nfac + ";";
							break;
						}
					}
				}
			}

			DataRow[] rows = dt.Select("NbFacture>0","Independant ASC");
			return rows;	
		}

		static public string Modulo10(string P_serie)
		{
			int[][] tableau = new int[10][];

			for (int t = 0; t < 10; t++)
				tableau[t] = new int[10];

			int k = P_serie.Length+1;
			int[] report = new int[k];
			report[0] = 0;

            tableau[0][0] = 0;
			tableau[0][1] = 9;
			tableau[0][2] = 4;
			tableau[0][3] = 6;
			tableau[0][4] = 8;
			tableau[0][5] = 2;
			tableau[0][6] = 7;
			tableau[0][7] = 1;
			tableau[0][8] = 3;
			tableau[0][9] = 5;

			for(int i=1; i<10; i++)
				for(int j=0; j<10; j++)
				{
					tableau[i][j] = tableau[i-1][(j+1)%10];
				}

			for (int c = 0; c < k - 1; c++)
			{
				int chiffre = Convert.ToInt32(String.Format("{0}", P_serie[c]));
				report[c + 1] = tableau[report[c]][chiffre];
			}

            return String.Format("{0}", (10 - report[k-1]) % 10);
		}

		#endregion

		#endregion
        
	}
}
