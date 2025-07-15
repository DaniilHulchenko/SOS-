using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using ImportSosGeneve;
using System.Configuration;
using MySql.Data.MySqlClient;
using SosMedecins.SmartRapport.GestionApplication;

namespace RecupDataEpos1
{
    public partial class FImportEpos : Form
    {
        public FImportEpos()
        {
            InitializeComponent();
        }

        private void FImportEpos_Load(object sender, EventArgs e)
        {
            //on desactive le timer de fermeture
            timer1.Enabled = false;

            pictureBox1.Visible = false;          
        }


        //************************************************

        //Fonction qui calcule l'âge
        public int CalculeAge(DateTime anniversaire)
        {
            DateTime now = DateTime.Today;
            int age = now.Year - anniversaire.Year;
            if (anniversaire > now.AddYears(-age))
                age--;
            return age;
        }


        //Gestion des Tels
        private void AjouteTel(string IdPersonne, string tel)
        {
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);
            
            try
            {
                dbConnection.Open();

                SqlCommand cmd = dbConnection.CreateCommand();
                cmd.Connection = dbConnection;

                //Gestion des n° de Tel...On regarde s'il existe dans la table Tel_Personne                                                                       
                string sqlstr0 = "SELECT NumPersonne, NumTel";
                sqlstr0 += " FROM Tel_Personne";
                sqlstr0 += " WHERE NumPersonne = @IdPersonne";                
                sqlstr0 += " AND NumTel = @Tel";

                cmd.CommandText = sqlstr0;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("IdPersonne", IdPersonne);
                cmd.Parameters.AddWithValue("Tel", tel);

                DataTable Telephone = new DataTable();
                Telephone.Load(cmd.ExecuteReader());

                //Si on ne l'a pas trouvé, on l'ajoute à la table
                if (Telephone.Rows.Count == 0)
                {
                    sqlstr0 = "INSERT INTO Tel_Personne";
                    sqlstr0 += " VALUES(@NumPersonne, @Tel, GetDate())";
                
                    cmd.CommandText = sqlstr0;

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("NumPersonne", IdPersonne);
                    cmd.Parameters.AddWithValue("Tel", tel);

                    cmd.ExecuteReader();
                }

            }
            catch(Exception e)
            {
                Console.WriteLine(" Erreur : " + e.Message);
            }                                                                          
        }

      

        private void bImport_Click(object sender, EventArgs e)
        {
            if (ExportRegulVersSmart() == "OK")
            {
                //Affichage de l'émoji
                pictureBox1.Visible = true;
                pictureBox1.Image = imageList1.Images[0];   //Tout est ok, puis on ferme

                timer1.Enabled = true;
            }
            else
            {
                pictureBox1.Visible = true;
                pictureBox1.Image = imageList1.Images[3];   //C'est pas ok
            }

        }


        //Création dans Base SmartRapport les enreg dans tablePersonne et tablePatient (si necessaire), ainsi que dans les tableconsultations et tableactes
        public string ExportRegulVersSmart()
        {
            string retour = "KO";

            //déclaration et initialisation des variables
            string SqlStr0, SqlStr1, SqlStr2, SqlStr3, sqlstrCommentaire = "";

            CultureInfo MaCultureInfo = new CultureInfo("fr-FR");

            //Pametres de la progressBar1
            progressBar1.Minimum = 0;
            progressBar1.Value = 0;

            //On désactive le bouton fermer
            bFermer.Enabled = false;
            label1.Text = "Connexion en cours.....";

            //Connexion à la base SmartRapport
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);

            dbConnection.Open();

            DataSet dsAppels = new DataSet();
            dsAppels = RetourneAppels();

            //S'il y a des enregistrements...
            try
            {
                if (dsAppels.Tables["Appels"].Rows.Count > 0)
                {
                    Int32 NumP = -1;
                    Int32 Numpatient = -1;

                    //Il y a des enreg. à récupérer donc on initialise la valeur max de la progressBar1
                    progressBar1.Maximum = dsAppels.Tables["Appels"].Rows.Count;

                    //on modifie le label1
                    label1.Text = "Rapatriement des données...";

                    //tant qu'il y a des enregistrements renvoyé par le ws
                    for (int i = 0; i < dsAppels.Tables["Appels"].Rows.Count; i++)
                    {
                        //on avance la progressBar1
                        progressBar1.Value = i + 1;

                        retour = "KO";    //pour la maj d'Appel par la suite

                        //on remet les chaines à blanc
                        SqlStr0 = "";
                        SqlStr1 = "";
                        SqlStr2 = "";
                        SqlStr3 = "";

                        NumP = Int32.Parse(dsAppels.Tables["Appels"].Rows[i]["Num_Personne"].ToString());

                        //Nvlle personne
                        if (NumP == -1)
                        {
                            //Création d'une nouvelle personne, on la créer
                            NumP = NvxNum("tablepersonne");    //Récup du nvx n° de personne                          

                            SqlStr0 = "INSERT INTO tablepersonne";
                            SqlStr0 += " (IdPersonne, Tel, Nom, Prenom, NumAdresse, CodePostal, Departement, Commune, Rue, NumeroDansRue, ";
                            SqlStr0 += " Batiment, Escalier, Etage, Digicode, Internom, Porte, Longitude, Latitude, DateNaissance, DateDeces, ";
                            SqlStr0 += " Sexe, Age, UniteAge, TexteSup, ListeNoire, Adm_Batiment, Adm_NumeroDansRue, Adm_Rue, Adm_CodePostal, ";
                            SqlStr0 += " Adm_Commune, Adm_Pays, Chez, Email, IdUnilab)";
                            SqlStr0 += " VALUES (" + NumP.ToString() + ",'" + dsAppels.Tables["Appels"].Rows[i]["Tel_Patient"].ToString() + "','" + dsAppels.Tables["Appels"].Rows[i]["Nom"].ToString().Replace("'", "''") + "','";
                            SqlStr0 += dsAppels.Tables["Appels"].Rows[i]["Prenom"].ToString().Replace("'", "''") + "',0,'" + dsAppels.Tables["Appels"].Rows[i]["CodePostal"].ToString() + "','','" + dsAppels.Tables["Appels"].Rows[i]["Commune"].ToString().Replace("'", "''") + "','";
                            SqlStr0 += dsAppels.Tables["Appels"].Rows[i]["Adr1"].ToString().Replace("'", "''") + "','" + dsAppels.Tables["Appels"].Rows[i]["Num_Rue"].ToString() + "','";
                            SqlStr0 += "','','" + dsAppels.Tables["Appels"].Rows[i]["Etage"].ToString().Replace("'", "''") + "','" + dsAppels.Tables["Appels"].Rows[i]["Digicode"].ToString().Replace("'", "''") + "','";
                            SqlStr0 += dsAppels.Tables["Appels"].Rows[i]["InterphoneNom"].ToString().Replace("'", "''") + "','";
                            SqlStr0 += dsAppels.Tables["Appels"].Rows[i]["Porte"].ToString().Replace("'", "''") + "',0,0,";

                            //Test pour la date
                            if (dsAppels.Tables["Appels"].Rows[i]["DateNaissance"].ToString() != "")
                            {
                                SqlStr0 += "'" + dsAppels.Tables["Appels"].Rows[i]["DateNaissance"].ToString() + "',";
                            }
                            else SqlStr0 += "Default,";

                            //On détermine l'âge
                            if (dsAppels.Tables["Appels"].Rows[i]["DateNaissance"].ToString() != "")
                            {
                                DateTime DateNaiss;
                                int age;

                                if (DateTime.TryParse(dsAppels.Tables["Appels"].Rows[i]["DateNaissance"].ToString(), out DateNaiss))
                                {
                                    age = CalculeAge(DateNaiss);
                                    SqlStr0 += "Default,'" + dsAppels.Tables["Appels"].Rows[i]["Sexe"].ToString() + "'," + age.ToString() + ",'A','";
                                }
                                else SqlStr0 += "Default,'" + dsAppels.Tables["Appels"].Rows[i]["Sexe"].ToString() + "'," + "Null" + ",'A','";
                            }
                            else
                            {
                                SqlStr0 += "Default,'" + dsAppels.Tables["Appels"].Rows[i]["Sexe"].ToString() + "'," + "Null" + ",'A','";
                            }

                            SqlStr0 += dsAppels.Tables["Appels"].Rows[i]["Adr2"].ToString().Replace("'", "''") + "','','','";

                            //On recherche dans le DataSet, table Adrfacturation, les enreg, correspondants s'ils existent
                            string expression = "Num_Appel = " + dsAppels.Tables["Appels"].Rows[i]["Num_Appel"].ToString();
                            DataRow[] foundRows;
                           
                            //Pour l'adresse de facturation
                            foundRows = dsAppels.Tables["adrfacturation"].Select(expression);

                            //Pour l'adresse de facturation                                                       
                            if (foundRows.Length != 0)   //Si on en a une                         
                            {
                                string Pays = "";
                                switch (foundRows[0]["Pays"].ToString().ToLower())
                                {
                                    case "allemagne": Pays = "DE"; break;
                                    case "angleterre": Pays = "GB"; break;
                                    case "belgique": Pays = "BE"; break;
                                    case "espagne": Pays = "ES"; break;
                                    case "france": Pays = "FR"; break;
                                    case "italie": Pays = "IT"; break;
                                    case "portugal": Pays = "PT"; break;
                                    case "suisse": Pays = "CH"; break;
                                    default: Pays = "CH"; break;
                                }

                                SqlStr0 += foundRows[0]["Num_Rue"].ToString().Replace("'", "''").TrimEnd() + "','" + foundRows[0]["Adr1"].ToString().Replace("'", "''").TrimEnd() + "','";
                                SqlStr0 += foundRows[0]["CodePostal"].ToString().Replace("'", "''").TrimEnd() + "','" + foundRows[0]["Commune"].ToString().Replace("'", "''").TrimEnd() + "','" + Pays + "','";
                                SqlStr0 += foundRows[0]["Nom"].ToString().Replace("'", "''") + " " + foundRows[0]["Prenom"].ToString().Replace("'", "''") + "','";
                            }
                            else
                            {
                                string Pays = "";
                                switch (dsAppels.Tables["Appels"].Rows[i]["Pays"].ToString().ToLower())
                                {
                                    case "france": Pays = "FR"; break;
                                    case "suisse": Pays = "CH"; break;
                                    default: Pays = "CH"; break;
                                }

                                SqlStr0 += dsAppels.Tables["Appels"].Rows[i]["Num_Rue"].ToString().Replace("'", "''").TrimEnd() + "','" + dsAppels.Tables["Appels"].Rows[i]["Adr1"].ToString().Replace("'", "''").TrimEnd() + "','";
                                SqlStr0 += dsAppels.Tables["Appels"].Rows[i]["CodePostal"].ToString().Replace("'", "''").TrimEnd() + "','" + dsAppels.Tables["Appels"].Rows[i]["Commune"].ToString().Replace("'", "''").TrimEnd() + "','" + Pays + "','','";
                            }

                            SqlStr0 += dsAppels.Tables["Appels"].Rows[i]["Email"].ToString().Replace("'", "''") + "','" + dsAppels.Tables["Appels"].Rows[i]["IdUnilab"].ToString() + "')";

                            //Création dans tablepatient
                            Numpatient = NvxNum("tablepatient");    //Récup du nvx n° de patient
                            SqlStr1 = "INSERT INTO tablepatient";
                            SqlStr1 += " (IdPatient, IdPersonne, SuiviPatient, IdAbonnement, TypeAbonnement, TexteAbonnement, TypeDestinataireFacture, ";
                            SqlStr1 += " IdDestinataireFacture, Approuve)";
                            SqlStr1 += " VALUES (" + Numpatient.ToString() + "," + NumP.ToString() + ",'',0,'','',0,0,0)";
                        }
                        else   //la Personne existe
                        {
                            //On met à jour l'enregistrement Personne Sauf si c'est un TA!
                            //tableactes et tableconsultations eventuellement, on modifie les infos de tablepersonne SAUF pour les TA                                    
                            if (VerifTA(NumP) == "KO")
                            {
                                //on définit la requette pour la Maj de la tablepersonne
                                SqlStr0 = "UPDATE tablepersonne ";
                                SqlStr0 += " SET Tel = '" + dsAppels.Tables["Appels"].Rows[i]["Tel_Patient"].ToString() + "',";   //Dernier Tel_Patient connu
                                SqlStr0 += " CodePostal = '" + dsAppels.Tables["Appels"].Rows[i]["CodePostal"].ToString() + "',";
                                SqlStr0 += " Commune = '" + dsAppels.Tables["Appels"].Rows[i]["Commune"].ToString().Replace("'", "''") + "',";
                                SqlStr0 += " Rue = '" + dsAppels.Tables["Appels"].Rows[i]["Adr1"].ToString().Replace("'", "''") + "',";
                                SqlStr0 += " TexteSup = '" + dsAppels.Tables["Appels"].Rows[i]["Adr2"].ToString().Replace("'", "''") + "',";
                                SqlStr0 += " NumeroDansRue = '" + dsAppels.Tables["Appels"].Rows[i]["Num_Rue"].ToString() + "',";
                                SqlStr0 += " Etage = '" + dsAppels.Tables["Appels"].Rows[i]["Etage"].ToString().Replace("'", "''") + "',";
                                SqlStr0 += " Digicode = '" + dsAppels.Tables["Appels"].Rows[i]["Digicode"].ToString().Replace("'", "''") + "',";
                                SqlStr0 += " Porte = '" + dsAppels.Tables["Appels"].Rows[i]["Porte"].ToString().Replace("'", "''") + "',";

                                //Test pour la date
                                if (dsAppels.Tables["Appels"].Rows[i]["DateNaissance"].ToString() != "")
                                {
                                    SqlStr0 += " DateNaissance = '" + dsAppels.Tables["Appels"].Rows[i]["DateNaissance"].ToString() + "',";
                                    SqlStr0 += " Sexe = '" + dsAppels.Tables["Appels"].Rows[i]["Sexe"].ToString() + "',";

                                    //On détermine l'âge
                                    if (dsAppels.Tables["Appels"].Rows[i]["DateNaissance"].ToString() != "")
                                    {
                                        DateTime DateNaiss;
                                        int age;
                                        if (DateTime.TryParse(dsAppels.Tables["Appels"].Rows[i]["DateNaissance"].ToString(), out DateNaiss))
                                        {
                                            age = CalculeAge(DateNaiss);
                                            SqlStr0 += " Age = '" + age.ToString() + "',";
                                        }
                                        else SqlStr0 += " Age = '0',";
                                    }
                                    else
                                        SqlStr0 += " Age = '0',";
                                }

                                SqlStr0 += " UniteAge = 'A'";
                                SqlStr0 += ", Email = '" + dsAppels.Tables["Appels"].Rows[i]["Email"].ToString() + "'";
                                SqlStr0 += ", IdUnilab = '" + dsAppels.Tables["Appels"].Rows[i]["IdUnilab"].ToString() + "'";

                                //Pour le n° d'assuré
                                if (dsAppels.Tables["Appels"].Rows[i]["NumCarte"].ToString().Length > 0 && 
                                                    dsAppels.Tables["Appels"].Rows[i]["NumCarte"].ToString().Substring(0,1).TrimStart(' ') == "8")
                                    SqlStr0 += ", Num_Carte = '" + dsAppels.Tables["Appels"].Rows[i]["NumCarte"].ToString().Trim(' ') + "'";

                                SqlStr0 += " Where IdPersonne = '" + NumP + "'";

                                //Récupération du N° de patient pour la suite
                                Numpatient = RecupNumPatient(NumP);

                            }    //Sinon c'est un TA donc pas de modif de la tablepersonne Sauf de l'IdUnilab
                            else
                            {
                                //on définit la requette pour la Maj de la tablepersonne IdUnilab
                                SqlStr0 = "UPDATE tablepersonne ";
                                SqlStr0 += " SET IdUnilab = '" + dsAppels.Tables["Appels"].Rows[i]["IdUnilab"].ToString() + "'";                                
                                SqlStr0 += " Where IdPersonne = '" + NumP + "'";
                            }

                            //On ajoute eventuellement les n° de Tel à la table Tel_Personne
                            AjouteTel(NumP.ToString(), dsAppels.Tables["Appels"].Rows[i]["Tel_Patient"].ToString());

                            //Récupération du N° de patient pour la suite
                            Numpatient = RecupNumPatient(NumP);

                        }    //Fin de la personne existe


                        //Puis Création dans TableActes et TableConsultation              
                        //On créé l'acte
                        //Recup d'un nvx num tableacte
                        Int32 NumActe = NvxNum("tableactes");

                        SqlStr2 = "INSERT INTO tableactes";
                        SqlStr2 += " (Num, IndicePatient, Tel, DAP, DTR, DRC, DSL, DFI, ";
                        SqlStr2 += " Urgence, AnnulationAppel, DAN, MotifAnnulation, Motif1, Motif2, ";
                        SqlStr2 += " OrigineAppel, CodeIntervenant, Service)";
                        SqlStr2 += " VALUES (" + NumActe.ToString() + "," + Numpatient.ToString() + ",'";

                        //Si pas de tel, alors on converti en 0
                        if (dsAppels.Tables["Appels"].Rows[i]["Tel_Patient"].ToString() == "")
                            SqlStr2 += "+0000000000',";
                        else
                            SqlStr2 += dsAppels.Tables["Appels"].Rows[i]["Tel_Patient"].ToString() + "',";

                        //Traitement des heures de l'appel
                        //DateTime avec valeurs null             
                        DBNull Value = DBNull.Value;
                        DateTime? DAP = null;
                        DateTime? DTR = null;
                        DateTime? DRC = null;
                        DateTime? DSL = null;
                        DateTime? DFI = null;
                        DateTime? DAN = null;
                       
                        int NumMedecin = 0;
                        int AnnulationAppel = 0;

                        //On recherche dans le DataSet, table suiviappel, les enreg, correspondants s'ils existent
                        string expression1 = "Num_Appel = " + dsAppels.Tables["Appels"].Rows[i]["Num_Appel"].ToString();
                        string sortOrder1 = "DateOp";
                        DataRow[] foundRows1;

                        foundRows1 = dsAppels.Tables["Suiviappel"].Select(expression1, sortOrder1);

                        foreach (DataRow row in foundRows1)
                        {
                            switch (row["Type_Operation"].ToString())
                            {
                                case "Création": DAP = DateTime.Parse(row["DateOp"].ToString()); break;
                                case "Attribution": DTR = DateTime.Parse(row["DateOp"].ToString());
                                    NumMedecin = int.Parse(row["CodeMedecin"].ToString()); break;

                                case "Acquitement": DRC = DateTime.Parse(row["DateOp"].ToString()); break;
                                case "Début de visite": DSL = DateTime.Parse(row["DateOp"].ToString()); break;
                                case "Terminée": DFI = DateTime.Parse(row["DateOp"].ToString()); break;
                                case "Annulée": DAN = DateTime.Parse(row["DateOp"].ToString()); AnnulationAppel = 1; break;
                            }
                        }

                        //SqlStr2 += "'" + DAP + "','" + DTR + "','" + DRC + "','" + DSL + "','" + DFI + "',";
                        SqlStr2 += "@DAP,@DTR,@DRC,@DSL,@DFI,";

                        //Recup des motifs
                        // SqlStr2 += "'" + dsAppels.Tables["Appels"].Rows[i]["Urgence"].ToString() + "','" + AnnulationAppel + "','" + DAN + "',";
                        SqlStr2 += "'" + dsAppels.Tables["Appels"].Rows[i]["Urgence"].ToString() + "','" + AnnulationAppel + "',@DAN,";
                        SqlStr2 += "'" + LibelleMotif(dsAppels.Tables["Appels"].Rows[i]["IdMotifAnnulation"].ToString()) + "',";

                        //Pour les conseils Téléphoniques, on met 'ConsTel' au Lieu de 'CONSEIL TELEPHONIQUE' renvoyé par la régul
                        string LibelleMotif1 = LibelleMotif(dsAppels.Tables["Appels"].Rows[i]["IdMotif1"].ToString());
                        
                        if (LibelleMotif1 == "CONSEIL TELEPHONIQUE")
                            SqlStr2 += "'ConsTel',";                        
                        else
                            SqlStr2 += "'" + LibelleMotif1.Replace("'", "''") + "',";

                        
                        SqlStr2 += "'" + LibelleMotif(dsAppels.Tables["Appels"].Rows[i]["IdMotif2"].ToString()).Replace("'", "''") + "',";
                        SqlStr2 += "'" + dsAppels.Tables["Appels"].Rows[i]["Provenance"].ToString() + "','" + NumMedecin + "','GVA')";


                        //*******On créé la consultation *****************************************
                        string PriseEnChargePatient = "Médecin";   //Pour le traitement du type de consult => Médecin ou infirmière

                        if((bool)dsAppels.Tables["Appels"].Rows[i]["EmailInfEnvoye"] is true)
                        {
                            PriseEnChargePatient = "Infirmière";
                        }

                        //Recup du num visite (tableAppel) car la consultation a le même n°                                 
                        SqlStr3 = "INSERT INTO tableconsultations";
                        SqlStr3 += " (NConsultation, CodeAppel, IndicePatient,EnvoiDocument, PriseEnChargePatient, Morphine, Pethidine, Fentanyl, Autre_stup, Autre_stup_qte)";
                        SqlStr3 += " VALUES (" + dsAppels.Tables["Appels"].Rows[i]["Num_Appel"].ToString() + "," + NumActe.ToString() + ",";
                        SqlStr3 +=               Numpatient.ToString() + ",'N','" + PriseEnChargePatient + "', 0,0,0,'',0)";


                        //*************************Recup des commentaires de l'appel uniquement pour les conseils Tel et les Rdv******************************                                                
                        if (LibelleMotif(dsAppels.Tables["Appels"].Rows[i]["IdMotif1"].ToString()) == "CONSEIL TELEPHONIQUE"
                             || LibelleMotif(dsAppels.Tables["Appels"].Rows[i]["IdMotif2"].ToString()) == "CONSEIL TELEPHONIQUE" 
                             || dsAppels.Tables["Appels"].Rows[i]["Commentaire"].ToString().Contains("RDV") == true)
                        {
                            string commentaire = "";

                            //Recup des commentaires
                            if (dsAppels.Tables["Appels"].Rows[i]["Commentaire"].ToString().Length > 0)
                            {
                                if (dsAppels.Tables["Appels"].Rows[i]["Commentaire"].ToString().Length > 249)
                                    commentaire = dsAppels.Tables["Appels"].Rows[i]["Commentaire"].ToString().Replace("/", "").Replace("'", "''").Substring(0, 249);
                                else
                                    commentaire = dsAppels.Tables["Appels"].Rows[i]["Commentaire"].ToString().Replace("/", "").Replace("'", "''");

                                sqlstrCommentaire = "INSERT INTO CommentAppel ";
                                sqlstrCommentaire += " (Num_Appel, Nom, Prenom, CommentaireAppel) ";
                                sqlstrCommentaire += " VALUES (" + dsAppels.Tables["Appels"].Rows[i]["Num_Appel"].ToString() + ",'" + dsAppels.Tables["Appels"].Rows[i]["Nom"].ToString().Replace("'", "''");
                                sqlstrCommentaire += "','" + dsAppels.Tables["Appels"].Rows[i]["Prenom"].ToString().Replace("'", "''") + "','" + commentaire + "')";
                            }
                        }

                        //***********************Fin commentaires appel********************************


                        //Validation dans une transaction                      
                        SqlTransaction trans;   //On déclare une transaction

                        SqlCommand cmd = new SqlCommand();

                        //Ouverture de la transaction
                        trans = dbConnection.BeginTransaction();

                        cmd.Connection = dbConnection;
                        cmd.Transaction = trans;

                        try
                        {
                            //on execute les requettes                                       
                            if (SqlStr0 != "")
                            {
                                cmd.CommandText = SqlStr0; cmd.ExecuteNonQuery();
                            }

                            if (SqlStr1 != "")                    //Si création d'un nvx patient 
                            {
                                cmd.CommandText = SqlStr1; cmd.ExecuteNonQuery();
                            }

                            //Pour tableacte pour les dates null
                            cmd.CommandText = SqlStr2;
                            cmd.Parameters.Clear();
                            
                            if (DAP == null)
                                cmd.Parameters.AddWithValue("DAP", DBNull.Value);
                            else 
                                cmd.Parameters.AddWithValue("DAP", DAP);

                            if (DTR == null)
                                cmd.Parameters.AddWithValue("DTR", DBNull.Value);
                            else
                                cmd.Parameters.AddWithValue("DTR", DTR);

                            if (DRC == null)
                                cmd.Parameters.AddWithValue("DRC", DBNull.Value);
                            else
                                cmd.Parameters.AddWithValue("DRC", DRC);

                            if (DSL == null)
                                cmd.Parameters.AddWithValue("DSL", DBNull.Value);
                            else
                                cmd.Parameters.AddWithValue("DSL", DSL);

                            if (DFI == null)
                                cmd.Parameters.AddWithValue("DFI", DBNull.Value);
                            else
                                cmd.Parameters.AddWithValue("DFI", DFI);

                            if (DAN == null)
                                cmd.Parameters.AddWithValue("DAN", DBNull.Value);
                            else 
                                cmd.Parameters.AddWithValue("DAN", DAN);

                            cmd.ExecuteNonQuery();
                            cmd.CommandText = SqlStr3; cmd.ExecuteNonQuery();
                            //pour dev   trans.Rollback();

                            //Commentaires (dans le cas des Conseils téléphoniques)
                           // if (LibelleMotif(dsAppels.Tables["Appels"].Rows[i]["IdMotif1"].ToString()) == "ConsTel" || LibelleMotif(dsAppels.Tables["Appels"].Rows[i]["IdMotif2"].ToString()) == "ConsTel")                            
                            if (sqlstrCommentaire != "")
                            {
                                cmd.CommandText = sqlstrCommentaire; cmd.ExecuteNonQuery();                               
                            }

                            //on valide la transaction
                            trans.Commit();

                            retour = "OK";
                        }
                        catch (Exception e)
                        {
                            trans.Rollback();
                            MessageBox.Show("Erreur lors de la création de la visite dans SmartRapport. " + e.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        //Maj de la table Appels (Exporté)
                        if (retour == "OK")
                        {
                            MajAppelExport(dsAppels.Tables["Appels"].Rows[i]["Num_Appel"].ToString(), NumP);

                            string NomPatient = dsAppels.Tables["Appels"].Rows[i]["Nom"].ToString() + " " + dsAppels.Tables["Appels"].Rows[i]["Prenom"].ToString();

                            //Copie des fichiers Constats et maj de la base SmartRapport
                            IntegrationConstats(dsAppels.Tables["Appels"].Rows[i]["Num_Appel"].ToString(), Numpatient.ToString(), NomPatient);
                        }

                    }   //Fin boucle For

                }   //Fin d'il y des appels
                else   //Pas de visite à récupérer
                {
                    retour = "OK";
                }

                //On copie également les Cartes AVS quelque soit la visite
                CopieCartesAVS();

                //ainsi que les dictées....Sauf si le fichier existe déjà sur le serveur
                CopieDictees();
            }
            catch (Exception e)
            {
                MessageBox.Show("Erreur " + e.Message);
                retour = "KO";

                Console.WriteLine(e.Message);
                //on reactive le bouton Fermer
                bFermer.Enabled = true;
            }
            finally
            {
                //fermeture des connexions
                if (dbConnection.State == ConnectionState.Open)
                {
                    dbConnection.Close();
                }
            }

            //on reactive le bouton Fermer
            bFermer.Enabled = true;
            bImport.Enabled = false;
            //on modifie le label1
            label1.Text = "Récupération terminée.";

            return retour;
        }


        //Retourne les éléments des visites terminées mais non exportées
        private static DataSet RetourneAppels()
        {
            DataSet DSResult = new DataSet();

            //Chaine de connection... ici on attaque MariaDB            
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base_Regul"].ToString();           
            MySqlConnection MConnection = new MySqlConnection(connex);

            MConnection.Open();
            MySqlCommand cmd = new MySqlCommand();

            cmd.Connection = MConnection;

            try
            {
                //Appels
                string sqlstr0 = "SELECT * FROM appels ";
                sqlstr0 += " WHERE Termine = 1 AND Export = 0 ";
                sqlstr0 += " AND (IdMotifAnnulation is null OR IdMotifAnnulation = '')";                
                sqlstr0 += " AND CodeMedecin not in (1402, 1006) ";

                cmd.CommandText = sqlstr0;
                cmd.Parameters.Clear();

                DSResult.Tables.Add("Appels");
                DSResult.Tables["Appels"].Load(cmd.ExecuteReader());    //on execute

                //AdrFacturation
                sqlstr0 = "SELECT * FROM adrfacturation WHERE Num_Appel in (SELECT Num_Appel FROM appels WHERE Termine = 1 AND Export = 0 ";
                sqlstr0 += "                                                AND (IdMotifAnnulation is null OR IdMotifAnnulation = '')";                
                sqlstr0 += "                                                AND CodeMedecin not in (1402, 1006)) ";

                cmd.CommandText = sqlstr0;
                cmd.Parameters.Clear();

                DSResult.Tables.Add("adrfacturation");
                DSResult.Tables["adrfacturation"].Load(cmd.ExecuteReader());    //on execute

                //SuiviAppel
                sqlstr0 = "SELECT * FROM suiviappel WHERE Num_Appel in (SELECT Num_Appel FROM appels WHERE Termine = 1 AND Export = 0 ";
                sqlstr0 += "                                            AND (IdMotifAnnulation is null OR IdMotifAnnulation = '')";
                sqlstr0 += "                                            AND CodeMedecin not in (1402, 1006)) ";                
                sqlstr0 += " ORDER BY Num_Appel, DateOp ";

                cmd.CommandText = sqlstr0;
                cmd.Parameters.Clear();

                DSResult.Tables.Add("SuiviAppel");
                DSResult.Tables["SuiviAppel"].Load(cmd.ExecuteReader());    //on execute               
            }
            catch (Exception e)
            {
                MessageBox.Show("Erreur lors de la récupération des Appels. " + e.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Console.WriteLine("Erreur lors de la récupération de la visite. " + e.Message);
                DSResult = null;
            }
            finally
            {
                //fermeture des connexions
                if (MConnection.State == System.Data.ConnectionState.Open)
                {
                    MConnection.Close();
                }
            }

            return DSResult;
        }



        //Retourne un nvx n° de personne
        private static Int32 NvxNum(string table)
        {
            Int32 NMax = -1;

            //On recherche le plus grand id dans SmartRapport            
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);

            dbConnection.Open();
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = dbConnection;
            try
            {
                string sqlstr0 = "";

                //IdPersonne
                switch (table)
                {
                    case "tablepersonne": sqlstr0 = "SELECT MAX(IdPersonne) FROM tablepersonne"; break;
                    case "tablepatient": sqlstr0 = "SELECT MAX(IdPatient) FROM tablepatient"; break;
                    case "tableactes": sqlstr0 = "SELECT MAX(Num) FROM tableactes"; break;
                }

                cmd.CommandText = sqlstr0;

                DataTable dtNum = new DataTable();
                dtNum.Load(cmd.ExecuteReader());    //on execute

                NMax = Int32.Parse(dtNum.Rows[0][0].ToString()) + 1;
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Erreur lors de la récupération du n° de " + table + ": " + e.Message, "Erreur", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                //fermeture des connexions
                if (dbConnection.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Close();
                }
            }

            return NMax;
        }


        //On récupère le num Patient dans table Patient de SmartRapport
        private static Int32 RecupNumPatient(Int32 Pers)
        {
            int Retour = -1;

            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);

            dbConnection.Open();
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = dbConnection;

            try
            {
                //Table Status_Visite
                string sqlstr0 = "SELECT IdPatient FROM tablepatient WHERE IdPersonne = @Personne";
                cmd.CommandText = sqlstr0;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("Personne", Pers);

                DataTable dtPatient = new DataTable();
                dtPatient.Load(cmd.ExecuteReader());    //on execute

                if (dtPatient.Rows.Count > 0)   //Si on a un enregistrement
                {
                    if (dtPatient.Rows[0][0] != DBNull.Value)
                        Retour = Int32.Parse(dtPatient.Rows[0][0].ToString());
                    else Retour = -1;
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Erreur lors de la recherche du n° dans tablepatient :" + e.Message, "Erreur", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                //fermeture des connexions
                if (dbConnection.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Close();
                }
            }

            return Retour;
        }


        //On vérifie si c'est un TA
        public static string VerifTA(Int32 NumP)
        {
            string retour = "KO";

            //On recherche Si c'est un TA
            //Chaine de connection...
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);

            dbConnection.Open();
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = dbConnection;

            try
            {
                //TablePatient
                string sqlstr0 = "SELECT TypeAbonnement FROM tablepatient WHERE IdPersonne = @NumPers";
                cmd.CommandText = sqlstr0;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("NumPers", NumP);

                DataTable dtTa = new DataTable();
                dtTa.Load(cmd.ExecuteReader());    //on execute

                if (dtTa.Rows.Count > 0)
                {
                    if (dtTa.Rows[0][0].ToString() == "TA")
                        retour = "OK";
                    else retour = "KO";
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Erreur lors de la vérification du TA." + e.Message, "Erreur", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                retour = "KO";
            }
            finally
            {
                //fermeture des connexions
                if (dbConnection.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Close();
                }
            }

            return retour;
        }


        //Remonte le libellé du motif à partir du code (Base Regul)
        public static string LibelleMotif(string CodeMotif)
        {
            string Retour = "";

            if (CodeMotif != "")
            {
                string connex = ConfigurationManager.ConnectionStrings["Connection_Base_Regul"].ToString();
                MySqlConnection dbConnection = new MySqlConnection(connex);

                dbConnection.Open();
                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = dbConnection;

                try
                {
                    //Table Motif
                    string sqlstr0 = "SELECT LibelleMotif FROM motif WHERE idMotif = @Motif";
                    cmd.CommandText = sqlstr0;

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("Motif", CodeMotif);

                    DataTable Result = new DataTable();
                    Result.Load(cmd.ExecuteReader());    //on execute

                    if (Result.Rows.Count > 0)   //Si on a un enregistrement
                    {
                        if (Result.Rows[0][0] != DBNull.Value)
                            Retour = Result.Rows[0][0].ToString();
                    }
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show("Erreur lors de la recherche du libellé dans la table Motif:" + e.Message, "Erreur", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                finally
                {
                    //fermeture des connexions
                    if (dbConnection.State == System.Data.ConnectionState.Open)
                    {
                        dbConnection.Close();
                    }
                }
            }
            return Retour;
        }



        //Mise à jour de l'Appel: Export = 1 ainsi que le n° de personne (au cas ou c'était -1)
        public static string MajAppelExport(string Num_Appel, Int32 NumPersonne)
        {
            string Retour = "KO";

            //Chaine de connection... ici on attaque MariaDB
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base_Regul"].ToString();
            MySqlConnection dbConnection = new MySqlConnection(connex);

            dbConnection.Open();
            MySqlCommand cmd = new MySqlCommand();

            cmd.Connection = dbConnection;

            //On déclare ici la requete (à cause de la transaction)
            string SqlStr0 = "";

            //Puis dans une transaction
            MySqlTransaction trans;

            //Ouverture d'une transaction
            trans = dbConnection.BeginTransaction();
            cmd.Transaction = trans;

            try
            {
                //Maj status_visite (status AQ, Date)
                SqlStr0 = "UPDATE appels SET Num_Personne = " + NumPersonne + ", Export = 1 WHERE Num_Appel = " + Num_Appel;

                //on execute les requettes                                       
                cmd.CommandText = SqlStr0; cmd.ExecuteNonQuery();

                //on valide la transaction
                trans.Commit();

                Retour = "OK";
            }
            catch (Exception e)
            {
                trans.Rollback();
                Retour = "KO";
                MessageBox.Show("Erreur lors de l'exportation de la visite n°. " + Num_Appel + "  ." + e.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //fermeture des connexions
                if (dbConnection.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Close();
                }
            }

            return Retour;
        }

        //On integre les constats dans la bases et les photos sur le serveur
        public static string IntegrationConstats(string Num_Appel, string NumPatient, string NomPatient)
        {
            string Retour = "KO";

            //Chemin des photos constat sur le serveur Linux
            string CheminConstats = SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.Path_Constat;

            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);

            dbConnection.Open();
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = dbConnection;

            try
            {
                //On cherche tout les fichiers commençant par le n° de consultation
                string[] Fichiers = Directory.GetFiles(CheminConstats, Num_Appel + "*");

                Console.WriteLine("Nombre de fichiers trouvés " + Fichiers.Length);

                //Puis pour chaque fichiers trouvés on les mets dans le dossier des documents de SmartRapport                
                string cheminDest = SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.Dest_Path_Constat;

                int NbPhotos = 0;

                foreach (string Fichier in Fichiers)
                {
                    NbPhotos++;

                    string dated = DateTime.Today.ToString("dd.MM.yyyy");
                    string Extention = Path.GetExtension(Fichier);

                    //Nouveau nom du fichier
                    string Destination = cheminDest + Num_Appel + "_" + "Constat" + "_" + dated + "_" + NbPhotos.ToString() + Extention;
                    string NomFichier = Num_Appel + "_" + "Constat" + "_" + dated + "_" + NbPhotos.ToString() + Extention;

                    //Puis dans une transaction
                    SqlTransaction trans;
                    trans = dbConnection.BeginTransaction();
                    cmd.Transaction = trans;

                    string SqlStr0 = "INSERT INTO patientjointdoc (UrlJointDoc, IdPatient) VALUES ('" + NomFichier + "', " + NumPatient + ")";

                    try
                    {
                        //on execute la requette                                       
                        cmd.CommandText = SqlStr0; cmd.ExecuteNonQuery();

                        //on valide la transaction
                        trans.Commit();

                        //Puis on ajoute le doc (si pas d'erreur)
                        if (File.Exists(Destination) == false)
                            System.IO.File.Copy(Fichier, Destination, false);

                        Retour = "OK";
                    }
                    catch (Exception a)
                    {
                        Console.WriteLine("Erreur : " + a.Message);
                        MessageBox.Show("Erreur lors d'ajout du document....l'erreur est : " + a.Message);
                        trans.Rollback();
                        Retour = "KO";
                    }

                    //Puis on efface le fichier origine
                    System.IO.File.Delete(Fichier);

                    mouchard.evenement("Importation d'une photo constat N° de Consult: " + NomPatient.ToString().Replace("'", "''"), VariablesApplicatives.Utilisateurs.NomUtilisateur.ToString());  //log
                }       //Fin de la liste des fichiers                
            }
            catch (Exception e)
            {
                MessageBox.Show("Erreur lors de la lectures des photos constats " + e.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Retour = "KO";
            }
            finally
            {
                //fermeture des connexions
                if (dbConnection.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Close();
                }
            }
           
            return Retour;
        }

        //Copie la carte AVS vers le QNAP
        public static void CopieCartesAVS()
        {
            //Chemin des photos Cartes AVS sur le serveur Linux
            string CheminCartesAVS = SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.Path_carteAVS;
            string CheminDest = SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.Dest_Path_carteAVS;

            string NomFichier = "";
            string Destination = "";

            //On cherche tout les fichiers du repertoire
            string[] Fichiers = Directory.GetFiles(CheminCartesAVS);

            Console.WriteLine("Nombre de fichiers trouvés " + Fichiers.Length);

            try
            {
                //Copie et écrase (s'ils existent déjà), tout les fichiers du répertoire
                foreach (string Fichier in Fichiers)
                {
                    NomFichier = System.IO.Path.GetFileName(Fichier);
                    Destination = System.IO.Path.Combine(CheminDest, NomFichier);

                    if (File.Exists(Destination) == false)
                        System.IO.File.Copy(Fichier, Destination, false);
                }
            }
            catch (IOException e)
            {
                MessageBox.Show("Erreur lors de la copie des photos de carte AVS vers le serveur " + e.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //Puis on efface les fichiers du répertoire
            try
            {
                foreach (string Fichier in Fichiers)
                {
                    System.IO.File.Delete(Fichier);
                }
            }
            catch (IOException e)
            {
                MessageBox.Show("Erreur lors de la suppression des photos de carte AVS du serveur " + e.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Copie les dictées vers le QNAP
        public static void CopieDictees()
        {
            //Chemin des fichiers audio (Dictées) sur le serveur Linux
            string CheminDictees = SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.Path_Dictee;
            string CheminDest = SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.Dest_Path_Dictee;
           
            string NomFichier = "";
            string Destination = "";

            //On cherche tout les fichiers du repertoire
            string[] Fichiers = Directory.GetFiles(CheminDictees);

            Console.WriteLine("Nombre de fichiers trouvés " + Fichiers.Length);

            try
            {
                //Copie (s'ils n'existent pas déjà), tout les fichiers audio du répertoire
                foreach (string Fichier in Fichiers)
                {
                    NomFichier = System.IO.Path.GetFileName(Fichier);
                    Destination = System.IO.Path.Combine(CheminDest, NomFichier);

                    if (File.Exists(Destination) == false)
                        System.IO.File.Copy(Fichier, Destination, false);
                }
            }
            catch (IOException e)
            {
                MessageBox.Show("Erreur lors de la copie des dictées vers le serveur " + e.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //Puis on efface les fichiers du répertoire
            try
            {
                foreach (string Fichier in Fichiers)
                {
                    System.IO.File.Delete(Fichier);
                }
            }
            catch (IOException e)
            {
                MessageBox.Show("Erreur lors de la suppression des dictées du serveur " + e.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //On ferme la forme
            Close();
        }

        private void bFermer_Click(object sender, EventArgs e)
        {
            Close();
        }      
    } 
}




//A faire: