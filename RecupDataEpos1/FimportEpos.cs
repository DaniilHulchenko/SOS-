using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;

namespace RecupDataEpos1
{
    public partial class FImportEpos : Form
    {
        public FImportEpos()
        {
            InitializeComponent();
        }

        private void B1_Click(object sender, EventArgs e)
        {
            //déclaration et initialisation des variables
            string NewTelephone, Telephone, sqlstr0, sqlstr1, sqlstr2, sqlstr3, sqlstr4, sqlstr5 = "";
            long Nvx_Num_Personne, Nvx_Num_Patient, Nvx_Num_Actes, Nvx_Num_Consultations = 0;
            CultureInfo MaCultureInfo = new CultureInfo("fr-FR");
            long Longitude, Latitude = 0;
            
            //Pametres de la progressBar1
            progressBar1.Minimum = 0;
            progressBar1.Value = 0;

            //On désactive le bouton fermer
            B2.Enabled = false;
            label1.Text = "Connexion en cours.....";
            
            //Création de l'instance du webservice d'EPOS
            WebServiceEpos.Service Ws = new WebServiceEpos.Service();

            //Déclaration d'un DataTable (c'est ce qui est renvoyé par le webservice)
            DataTable dt = new DataTable();

            //Récup des donnes du ws dans le DataTable
            dt = Ws.GetRapportNonTransmis();  
           
            //S'il y a des enregistrements...
            try
            {
                if (dt.Rows.Count > 0)
                {
                    //Il y a des enreg. à récupérer donc on initialise la valeur max de la progressBar1
                    progressBar1.Maximum = dt.Rows.Count;

                    //Chaine de connection
                    //SqlConnection dbConnection = new SqlConnection("Data Source=192.168.0.32;Initial Catalog=SmartRapport;User Id=sa; Password=prazine");
                    SqlConnection dbConnection = new SqlConnection("Data Source=INFO-DOMI1\\SQLEXPRESS;Initial Catalog=BaseTest;User Id=sa; Password=prazine");
                    
                    //on ouvre la connexion
                    dbConnection.Open();

                    //déclaration d'une transaction
                    SqlTransaction trans;

                    //on modifie le label1
                    label1.Text = "Rapatriement des données...";

                    //MessageBox.Show("nb enreg. " + dt.Rows.Count.ToString());

                    //tant qu'il y a des enregistrements renvoyé par le ws
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //on avance la progressBar1
                        progressBar1.Value = i + 1;
   
                        //on remet les chaines à blanc
                        sqlstr1 = "";
                        sqlstr2 = "";
                        sqlstr3 = "";
                        sqlstr4 = "";
                        sqlstr5 = "";

                        //si on a une valeur IdPatient <> -1 (cad connu de la base)
                        if (int.Parse(dt.Rows[i]["IDpatient"].ToString()) != -1)
                        {
                            //on recherche dans la base le patient critère: idPatient
                            //on définit la requette
                            sqlstr1 = "SELECT IdPatient, IdPersonne";
                            sqlstr1 += " From tablepatient";
                            sqlstr1 += " Where IdPatient = " + dt.Rows[i]["IDpatient"].ToString();
                            //MessageBox.Show(sqlstr1);       
                            //On passe les parametres query et connection
                            SqlDataAdapter Query1 = new SqlDataAdapter(sqlstr1, dbConnection);
                           
                            //on déclare le DataSet pour recevoir les diverses données
                            DataSet DSResult = new DataSet();

                            //on déclare une table pour cet ensemble de donnée
                            DSResult.Tables.Add("Patient");

                            //on execute
                            Query1.Fill(DSResult, "Patient");
                          
                            //si trouvé
                            if (DSResult.Tables["Patient"].Rows.Count > 0)
                            {
                                //Le patient existe donc on ne créé que des enregistrements pour les tables
                                //tableactes et tableconsultations eventuellement, on modifie les infos de tablepersonne

                                //MessageBox.Show("existe 1" + i.ToString());
                                
                                //on définit la requette pour la Maj de la tablepersonne
                                sqlstr2 = " UPDATE tablepersonne";
                                
                                //on format le telephone (xxx xxx xx xx)
                                NewTelephone = dt.Rows[i]["Tel_Patient"].ToString().Substring(0,3) +" "+ dt.Rows[i]["Tel_Patient"].ToString().Substring(3,3) + " "+
                                dt.Rows[i]["Tel_Patient"].ToString().Substring(6,2) +" "+ dt.Rows[i]["Tel_Patient"].ToString().Substring(8,2);
 
                                sqlstr2 += " SET Tel = '" + NewTelephone + "',";
                                sqlstr2 += " Nom = '" + dt.Rows[i]["Patient"].ToString() + "',";
                                sqlstr2 += " Prenom = '" + dt.Rows[i]["Prenom"].ToString() + "',";
                                sqlstr2 += " CodePostal = '" + dt.Rows[i]["Cpos"].ToString() + "',";
                                sqlstr2 += " Commune = '" + dt.Rows[i]["Nom_Commune"].ToString() + "',";
                                sqlstr2 += " Rue = '" + dt.Rows[i]["Rue"].ToString() + "',";
                                sqlstr2 += " TexteSup = '" + dt.Rows[i]["Rue1"].ToString() + "',";
                                sqlstr2 += " NumeroDansRue = '" + dt.Rows[i]["Voie_Numero"].ToString() + "',";
                                sqlstr2 += " Batiment = '" + dt.Rows[i]["Batiment"].ToString() + "',";
                                sqlstr2 += " Etage = '" + dt.Rows[i]["Etage"].ToString() + "',";
                                sqlstr2 += " Digicode = '" + dt.Rows[i]["Digicode"].ToString() + "',";
                                sqlstr2 += " Internom = '" + dt.Rows[i]["Inter_Nom"].ToString() + "',";
                                sqlstr2 += " Porte = '" + dt.Rows[i]["Porte"].ToString() + "',";
                                
                                //Test pour la date
                               if (dt.Rows[i]["Date_Naissance"].ToString() != "")
                                {
                                   sqlstr2 += " DateNaissance = '" + dt.Rows[i]["Date_Naissance"].ToString() + "',";
                                }

                                //sqlstr2 += " DateNaissance = '" + dt.Rows[i]["Date_Naissance"]. + "',";

                                sqlstr2 += " Sexe = '" + dt.Rows[i]["Sexe"].ToString() + "',";
                                sqlstr2 += " Age = '" + dt.Rows[i]["Age"].ToString() + "',";
                                sqlstr2 += " UniteAge = '" + dt.Rows[i]["Age_Unite"].ToString() + "'";
                                sqlstr2 += " Where IdPersonne = " + DSResult.Tables["Patient"].Rows[0]["IdPersonne"].ToString();
                                //MessageBox.Show(sqlstr2);

                                //On ajoute un enregistrement dans la tableactes
                                //Auparavent, on cherche le plus grand n° d'actes
                                
                                //On passe les parametres query et connection
                                SqlDataAdapter Query2 = new SqlDataAdapter("SELECT Max(Num) From Tableactes", dbConnection);

                                //on ajoute une table à cet ensemble de donnée pour y mettre le résultat
                                DSResult.Tables.Add("MaxTableActes");                        

                                //on execute
                                Query2.Fill(DSResult, "MaxTableActes");
                                
                                //on determine le nouveau n° de l'acte
                                Nvx_Num_Actes = int.Parse(DSResult.Tables["MaxTableActes"].Rows[0][0].ToString()) + 1;


                                //On créé l'acte
                                sqlstr3 = "INSERT INTO tableactes";
                                sqlstr3 += " (Num, IndicePatient, Tel, DAP, DTR, DRC, DSL, DFI, CodeMotif1, CodeMotif2, ";
                                sqlstr3 += " Urgence, DelaiIndique, CommentaireTransmis, CommentaireFichier, ";
                                sqlstr3 += " AnnulationAppel, DAN, MotifAnnulation, DevenirAnnulation, ComplementInfo, Motif1, Motif2, ";
                                sqlstr3 += " OrigineAppel, CodeIntervenant, DelaiArrivee, DelaiInterv, ExportSmartOk)";
                                sqlstr3 += " VALUES (" + Nvx_Num_Actes.ToString() + ","+ dt.Rows[i]["IdPatient"].ToString() + ",'";
                                sqlstr3 +=  NewTelephone +"',";

                                //Test pour la dateAppel (DAP)
                                if (dt.Rows[i]["Date_Appel"].ToString() != "")
                                {
                                    sqlstr3 += "'" + dt.Rows[i]["Date_Appel"].ToString() + "',";
                                }
                                else sqlstr3 += "Default,";

                                //Test pour la dateTransMedecin (DTR)
                                if (dt.Rows[i]["Date_Trans_Medecin"].ToString() != "")
                                {
                                    sqlstr3 += "'" + dt.Rows[i]["Date_Trans_Medecin"].ToString() + "',";
                                }
                                else sqlstr3 += "Default,";

                                //Test pour la date Acquittement (DRC)
                                if (dt.Rows[i]["Date_Acquittement"].ToString() != "")
                                {
                                    sqlstr3 += "'" + dt.Rows[i]["Date_Acquittement"].ToString() + "',";
                                }
                                else sqlstr3 += "Default,";


                                //Test pour la date Sur Place (DSL)
                                if (dt.Rows[i]["Date_Entree_Visite"].ToString() != "")
                                {
                                    sqlstr3 += "'" + dt.Rows[i]["Date_Entree_Visite"].ToString() + "',";
                                }
                                else sqlstr3 += "Default,";

                                //Test pour la date Fin de visite (DFI)
                                if (dt.Rows[i]["Date_Fin_Visite"].ToString() != "")
                                {
                                    sqlstr3 += "'" + dt.Rows[i]["Date_Fin_Visite"].ToString() + "',";
                                }
                                else sqlstr3 += "Default,";

                                sqlstr3 += " Default,Default,Default,";
                                sqlstr3 += "Default,Default,Default,Default,Default,Default,Default,Default,'";
                                sqlstr3 += dt.Rows[i]["Motif_Lib1"].ToString() + "','";
                                sqlstr3 += dt.Rows[i]["Motif_Lib2"].ToString() + "',";
                                sqlstr3 += "Default,";

                                sqlstr3 += dt.Rows[i]["IDMedecin"].ToString() + ",";                                
                                sqlstr3 += " Default,Default,Default)";
                                //MessageBox.Show(sqlstr3);

                               
                                //On ajoute un enregistrement dans la tableconsultations
                                //Auparavent, on cherche le plus grand n° de la table consultation

                                //On passe les parametres query et connection
                                SqlDataAdapter Query3 = new SqlDataAdapter("SELECT Max(NConsultation) From Tableconsultations", dbConnection);

                                //on déclare le DataSet qui va recevoir la donnée
                                //DataSet DSResult3 = new DataSet();

                                //on ajoute une table à cet ensemble de donnée pour y mettre le résultat
                                DSResult.Tables.Add("MaxTableConsultations");

                                //on execute
                                Query3.Fill(DSResult, "MaxTableConsultations");

                                //on determine le nouveau n° de la consultation
                                Nvx_Num_Consultations = int.Parse(DSResult.Tables["MaxTableConsultations"].Rows[0][0].ToString()) + 1;

                                //On créé la consultation
                                sqlstr4 = "INSERT INTO tableconsultations";
                                sqlstr4 += " (NConsultation, CodeAppel, IndicePatient, idDiag1, idDiag2, diag1, diag2, Hono, Reglement, actes, devenir, ";
                                sqlstr4 += " PriseEnChargePatient, LibCisp, Traitements, TraitementLibre, ";
                                sqlstr4 += " gestes, CommentaireLibre, EnvoiDocument, ListeIndexServiceExt, ListeIndexMt, TensionHaute, ";
                                sqlstr4 += " TensionBasse, O2, Pulsations, Temperature, Deces, Modifie, RapportGenere, FactureGeneree, Approuve, ";
                                sqlstr4 += " ExportSmartOk, ECGDecrypte, ImportECG, ParametresCliniques, RegulationCorrecte, AdresseCorrecte)";
                                sqlstr4 += " VALUES (" + Nvx_Num_Consultations.ToString() + "," + Nvx_Num_Actes.ToString() + ","+ dt.Rows[i]["IdPatient"].ToString() + ",";
                                sqlstr4 += " -1, -1, Default, Default, 0, Default, Default, Default, Default, Default, Default, Default, Default, Default, ";
                                sqlstr4 += " 'N', Default, Default, Default, Default, Default, Default, Default, Default, Default,";
                                sqlstr4 += " Default, Default, Default, Default, Default, Default, Default, Default, Default);";

                                //MessageBox.Show(sqlstr4);
                                //Ouverture d'une transaction
                                trans = dbConnection.BeginTransaction();                                
                                
                                //On execute les requettes et on valide la transaction
                                try
                                {
                                    //on execute les requettes
                                    //Ouverture d'une transaction
                                    new SqlCommand(sqlstr2, dbConnection, trans).ExecuteNonQuery();
                                    new SqlCommand(sqlstr3, dbConnection, trans).ExecuteNonQuery();                                   
                                    new SqlCommand(sqlstr4, dbConnection, trans).ExecuteNonQuery();
                                    
                                    //on valide la transaction
                                    trans.Commit();

                                    //on envoie la confirmation du webService
                                    //Création de l'instance du webservice d'EPOS
                                    WebServiceEpos.Service Ws2 = new WebServiceEpos.Service();

                                   
                                    //Récup des donnes du ws dans le DataTable
                                    Ws2.AcquittementReceptionRapport(int.Parse(dt.Rows[i]["Numero"].ToString()), int.Parse(dt.Rows[i]["IDpatient"].ToString()));

                                }
                                catch (SqlException SqlError)
                                {
                                    trans.Rollback();
                                    MessageBox.Show("Erreur :" + SqlError.Message);
                                    //on reactive le bouton Fermer
                                    B2.Enabled = true;
                                }
                                
                            }
                            else
                            {
                                //sinon il n'existe pas, donc on créé la personne dans les tables:
                                //tablepersonne, tablepatient, tableactes, tableconsultations
                                //MessageBox.Show("n'existe pas 1 " + i.ToString());
                                //On ajoute un enregistrement dans la tablepersonne
                                //Auparavent, on cherche le plus grand n° de personne                    
                                SqlDataAdapter Query0 = new SqlDataAdapter("SELECT Max(Num) From Tablepersonne", dbConnection);
                           
                                //on déclare le DataSet pour recevoir les diverses données
                                //DataSet DSResult = new DataSet();
 
                                //on ajoute une table à cet ensemble de donnée pour y mettre le résultat
                                DSResult.Tables.Add("MaxTablePersonne");    
     
                                //on execute
                                Query0.Fill(DSResult, "MaxTablePersonne");
 
                                //on determine le nouveau n° de Personne
                                Nvx_Num_Personne = int.Parse(DSResult.Tables["MaxTablePersonne"].Rows[0][0].ToString()) + 1;
                               
                                //On créé la personne
                                //on format le telephone (xxx xxx xx xx)
                                NewTelephone = dt.Rows[i]["Tel_Patient"].ToString().Substring(0,3) +" "+ dt.Rows[i]["Tel_Patient"].ToString().Substring(3,3) + " "+
                                dt.Rows[i]["Tel_Patient"].ToString().Substring(6,2) +" "+ dt.Rows[i]["Tel_Patient"].ToString().Substring(8,2);

                                //La Longitude et la latitude (* par 100 000)
                                Longitude = int.Parse(dt.Rows[i]["Longitude"].ToString()) * 100000;
                                Latitude = int.Parse(dt.Rows[i]["Latitude"].ToString()) * 100000;

                                sqlstr2 = "INSERT INTO tablepersonne";
                                sqlstr2 += " (IdPersonne, Tel, Nom, Prenom, NumAdresse, CodePostal, Departement, Commune, Rue, NumeroDansRue, ";
                                sqlstr2 += " Batiment, Escalier, Etage, Digicode, Internom, Porte, Longitude, Latitude, DateNaissance, DateDeces, ";
                                sqlstr2 += " Sexe, Age, UniteAge, TexteSup, ListeNoire, Adm_Batiment, Adm_NumeroDansRue, Adm_Rue, Adm_CodePostal, ";
                                sqlstr2 += " Adm_Commune, Chez)";
                                sqlstr2 += " VALUES (" + Nvx_Num_Personne.ToString() + ",'" + NewTelephone + "','" + dt.Rows[i]["Patient"].ToString() + "','";
                                sqlstr2 += dt.Rows[i]["Prenom"].ToString() + "',0,'" + dt.Rows[i]["Cpos"].ToString() + "','','" + dt.Rows[i]["Nom_Commune"].ToString() + "','";
                                sqlstr2 += dt.Rows[i]["Rue"].ToString() + "','" + dt.Rows[i]["Voie_Numero"].ToString() + "','" + dt.Rows[i]["Batiment"].ToString() + "','','";
                                sqlstr2 += dt.Rows[i]["Etage"].ToString() + "','" + dt.Rows[i]["Digicode"].ToString() + "','";
                                sqlstr2 += dt.Rows[i]["Inter_Nom"].ToString() + "','" + dt.Rows[i]["Porte"].ToString() + "',";
                                sqlstr2 += Longitude.ToString() + "," + Latitude.ToString() + ",";

                                //Test pour la date
                                if (dt.Rows[i]["Date_Naissance"].ToString() != "")
                                {
                                    sqlstr2 += "'" + dt.Rows[i]["Date_Naissance"].ToString() + "',";
                                }
                                else sqlstr2 += "Default,";

                                sqlstr2 += "Default,'" + dt.Rows[i]["Sexe"].ToString() + "'," + dt.Rows[i]["Age"].ToString() + ",'" + dt.Rows[i]["Age_Unite"].ToString() + "','";
                                sqlstr2 += dt.Rows[i]["Rue1"].ToString() + "','" + dt.Rows[i]["Rue1"].ToString() + "','";
                                sqlstr2 += dt.Rows[i]["Batiment"].ToString() + "','" + dt.Rows[i]["Voie_Numero"].ToString() + "','";
                                sqlstr2 += dt.Rows[i]["Rue"].ToString() + "','" + dt.Rows[i]["Cpos"].ToString() + "','";
                                sqlstr2 += dt.Rows[i]["Nom_Commune"].ToString() + "',Default)";
                                //MessageBox.Show(sqlstr2);

                                //On ajoute un enregistrement dans la tablepatient
                                //Auparavent, on cherche le plus grand n° de patient
                                SqlDataAdapter Query2 = new SqlDataAdapter("SELECT Max(IdPatient) From Tablepatient", dbConnection);
                                
                                //on ajoute une table à cet ensemble de donnée pour y mettre le résultat
                                DSResult.Tables.Add("MaxTablePatient");

                                //on execute
                                Query2.Fill(DSResult, "MaxTablePatient");

                                //on determine le nouveau n° de patient
                                Nvx_Num_Patient = int.Parse(DSResult.Tables["MaxTablePatient"].Rows[0][0].ToString()) + 1;

                                //On créé le patient
                                sqlstr3 = "INSERT INTO tablepatient";
                                sqlstr3 += " (IdPatient, IdPersonne, SuiviPatient, IdAbonnement, TypeAbonnement, TexteAbonnement, TypeDestinataireFacture, ";
                                sqlstr3 += " IdDestinataireFacture, Approuve)";
                                sqlstr3 += " VALUES (" + Nvx_Num_Patient.ToString()+ "," + Nvx_Num_Personne.ToString() + ",'',0,'','',0,0,0)";
                                //MessageBox.Show(sqlstr3);

                                //On ajoute un enregistrement dans la tableactes
                                //On passe les parametres query et connection
                                SqlDataAdapter Query3 = new SqlDataAdapter("SELECT Max(Num) From Tableactes", dbConnection);

                                //on ajoute une table à cet ensemble de donnée pour y mettre le résultat
                                DSResult.Tables.Add("MaxTableActes");                        

                                //on execute
                                Query3.Fill(DSResult, "MaxTableActes");
                                
                                //on determine le nouveau n° de l'acte
                                Nvx_Num_Actes = int.Parse(DSResult.Tables["MaxTableActes"].Rows[0][0].ToString()) + 1;


                                //On créé l'acte
                                sqlstr4 = "INSERT INTO tableactes";
                                sqlstr4 += " (Num, IndicePatient, Tel, DAP, DTR, DRC, DSL, DFI, CodeMotif1, CodeMotif2, ";
                                sqlstr4 += " Urgence, DelaiIndique, CommentaireTransmis, CommentaireFichier, ";
                                sqlstr4 += " AnnulationAppel, DAN, MotifAnnulation, DevenirAnnulation, ComplementInfo, Motif1, Motif2, ";
                                sqlstr4 += " OrigineAppel, CodeIntervenant, DelaiArrivee, DelaiInterv, ExportSmartOk)";
                                sqlstr4 += " VALUES (" + Nvx_Num_Actes.ToString() + "," + Nvx_Num_Patient.ToString() + ",'";
                               
                               //on format le telephone (xxx xxx xx xx)
                                NewTelephone = dt.Rows[i]["Tel_Patient"].ToString().Substring(0,3) +" "+ dt.Rows[i]["Tel_Patient"].ToString().Substring(3,3) + " "+
                                dt.Rows[i]["Tel_Patient"].ToString().Substring(6,2) +" "+ dt.Rows[i]["Tel_Patient"].ToString().Substring(8,2);
                                
                                sqlstr4 +=  NewTelephone +"',";
                 
                                //Test pour la dateAppel (DAP)
                                if (dt.Rows[i]["Date_Appel"].ToString() != "")
                                {
                                    sqlstr4 += "'" + dt.Rows[i]["Date_Appel"].ToString() + "',";
                                }
                                else sqlstr4 += "Default,";

                                //Test pour la dateTransMedecin (DTR)
                                if (dt.Rows[i]["Date_Trans_Medecin"].ToString() != "")
                                {
                                    sqlstr4 += "'" + dt.Rows[i]["Date_Trans_Medecin"].ToString() + "',";
                                }
                                else sqlstr4 += "Default,";

                                //Test pour la date Acquittement (DRC)
                                if (dt.Rows[i]["Date_Acquittement"].ToString() != "")
                                {
                                    sqlstr4 += "'" + dt.Rows[i]["Date_Acquittement"].ToString() + "',";
                                }
                                else sqlstr4 += "Default,";


                                //Test pour la date Sur Place (DSL)
                                if (dt.Rows[i]["Date_Entree_Visite"].ToString() != "")
                                {
                                    sqlstr4 += "'" + dt.Rows[i]["Date_Entree_Visite"].ToString() + "',";
                                }
                                else sqlstr4 += "Default,";

                                //Test pour la date Fin de visite (DFI)
                                if (dt.Rows[i]["Date_Fin_Visite"].ToString() != "")
                                {
                                    sqlstr4 += "'" + dt.Rows[i]["Date_Fin_Visite"].ToString() + "',";
                                }
                                else sqlstr4 += "Default,";

                                sqlstr4 += " Default,Default,Default,";
                                sqlstr4 += "Default,Default,Default,Default,Default,Default,Default,Default,'";
                                sqlstr4 += dt.Rows[i]["Motif_Lib1"].ToString() + "','";
                                sqlstr4 += dt.Rows[i]["Motif_Lib2"].ToString() + "',";
                                sqlstr4 += "Default,";

                                sqlstr4 += dt.Rows[i]["IDMedecin"].ToString() + ",";                                
                                sqlstr4 += " Default,Default,Default)";
                                //MessageBox.Show(sqlstr4);

                                //On ajoute un enregistrement dans la tableconsultations
                                //Auparavent, on cherche le plus grand n° de la table consultation

                                //On passe les parametres query et connection
                                SqlDataAdapter Query4 = new SqlDataAdapter("SELECT Max(NConsultation) From Tableconsultations", dbConnection);

                                //on ajoute une table à cet ensemble de donnée pour y mettre le résultat
                                DSResult.Tables.Add("MaxTableConsultations");

                                //on execute
                                Query4.Fill(DSResult, "MaxTableConsultations");

                                //on determine le nouveau n° de la consultation
                                Nvx_Num_Consultations = int.Parse(DSResult.Tables["MaxTableConsultations"].Rows[0][0].ToString()) + 1;

                                //On créé la consultation
                                sqlstr5 = "INSERT INTO tableconsultations";
                                sqlstr5 += " (NConsultation, CodeAppel, IndicePatient, idDiag1, idDiag2, diag1, diag2, Hono, Reglement, actes, devenir, ";
                                sqlstr5 += " PriseEnChargePatient, LibCisp, Traitements, TraitementLibre, ";
                                sqlstr5 += " gestes, CommentaireLibre, EnvoiDocument, ListeIndexServiceExt, ListeIndexMt, TensionHaute, ";
                                sqlstr5 += " TensionBasse, O2, Pulsations, Temperature, Deces, Modifie, RapportGenere, FactureGeneree, Approuve, ";
                                sqlstr5 += " ExportSmartOk, ECGDecrypte, ImportECG, ParametresCliniques, RegulationCorrecte, AdresseCorrecte)";
                                sqlstr5 += " VALUES (" + Nvx_Num_Consultations.ToString() + "," + Nvx_Num_Actes.ToString() + "," + Nvx_Num_Patient.ToString() + ",";
                                sqlstr5 += " -1, -1, Default, Default, 0, Default, Default, Default, Default, Default, Default, Default, Default, Default, ";
                                sqlstr5 += " 'N', Default, Default, Default, Default, Default, Default, Default, Default, Default,";
                                sqlstr5 += " Default, Default, Default, Default, Default, Default, Default, Default, Default);";

                                //MessageBox.Show(sqlstr5);
                                //Ouverture d'une transaction
                                trans = dbConnection.BeginTransaction();                                
                                
                                //On execute les requettes et on valide la transaction
                                try
                                {
                                    //on execute les requettes
                                    //Ouverture d'une transaction
                                    new SqlCommand(sqlstr2, dbConnection, trans).ExecuteNonQuery();
                                    new SqlCommand(sqlstr3, dbConnection, trans).ExecuteNonQuery();
                                    new SqlCommand(sqlstr4, dbConnection, trans).ExecuteNonQuery();
                                    new SqlCommand(sqlstr5, dbConnection, trans).ExecuteNonQuery();
                                  
                                    //on valide la transaction
                                    trans.Commit();

                                    //on envoie la confirmation du webService
                                    //Création de l'instance du webservice d'EPOS
                                    WebServiceEpos.Service Ws2 = new WebServiceEpos.Service();

                                   
                                    //Récup des donnes du ws dans le DataTable
                                    Ws2.AcquittementReceptionRapport(int.Parse(dt.Rows[i]["Numero"].ToString()), int.Parse(Nvx_Num_Patient.ToString()));

                                }
                                catch (SqlException SqlError)
                                {
                                    trans.Rollback();
                                    MessageBox.Show("Erreur :" + SqlError.Message);
                                    //on reactive le bouton Fermer
                                    B2.Enabled = true;
                                }

                            }
                        }
                      else
                        {      
                            //on recherche QUAND MEME! dans la base le patient critère: Nom, prenom, tel
                            //MessageBox.Show("Quand meme....");
                            //on format le telephone (xxx xxx xx xx)
                            NewTelephone = dt.Rows[i]["Tel_Patient"].ToString().Substring(0, 3) + " " + dt.Rows[i]["Tel_Patient"].ToString().Substring(3, 3) + " " +
                            dt.Rows[i]["Tel_Patient"].ToString().Substring(6, 2) + " " + dt.Rows[i]["Tel_Patient"].ToString().Substring(8, 2);
 
                            //on définit la requette
                            sqlstr0 = "SELECT pa.IdPatient, pa.IdPersonne";
                            sqlstr0 += " From tablepatient pa, tablepersonne pe";
                            sqlstr0 += " Where pa.IdPersonne = pe.IdPersonne";
                            sqlstr0 += " and pe.Nom = '" + dt.Rows[i]["Patient"].ToString() + "'";
                            sqlstr0 += " and pe.Prenom = '" + dt.Rows[i]["Prenom"].ToString() + "'";
                            sqlstr0 += " and pe.Tel = '" + NewTelephone + "'";

                            //MessageBox.Show(sqlstr1);       
                            //On passe les parametres query et connection
                            SqlDataAdapter Query1 = new SqlDataAdapter(sqlstr0, dbConnection);

                            //on déclare le DataSet pour recevoir les diverses données
                            DataSet DSResult = new DataSet();

                            //on déclare une table pour cet ensemble de donnée
                            DSResult.Tables.Add("Patient");

                            //on execute
                            Query1.Fill(DSResult, "Patient");
                          
                            //si trouvé
                            if (DSResult.Tables["Patient"].Rows.Count > 0)
                            {

                                //MAJ : Le patient existe donc on ne créé que des enregistrements pour les tables
                                //tableactes et tableconsultations eventuellement, on modifie les infos de tablepersonne

                                //MessageBox.Show("existe 2 " + i.ToString());

                                //on définit la requette pour la Maj de la tablepersonne
                                sqlstr2 = " UPDATE tablepersonne";
                                sqlstr2 += " SET Tel = '" + NewTelephone + "',";
                                sqlstr2 += " Nom = '" + dt.Rows[i]["Patient"].ToString() + "',";
                                sqlstr2 += " Prenom = '" + dt.Rows[i]["Prenom"].ToString() + "',";
                                sqlstr2 += " CodePostal = '" + dt.Rows[i]["Cpos"].ToString() + "',";
                                sqlstr2 += " Commune = '" + dt.Rows[i]["Nom_Commune"].ToString() + "',";
                                sqlstr2 += " Rue = '" + dt.Rows[i]["Rue"].ToString() + "',";
                                sqlstr2 += " TexteSup = '" + dt.Rows[i]["Rue1"].ToString() + "',";
                                sqlstr2 += " NumeroDansRue = '" + dt.Rows[i]["Voie_Numero"].ToString() + "',";
                                sqlstr2 += " Batiment = '" + dt.Rows[i]["Batiment"].ToString() + "',";
                                sqlstr2 += " Etage = '" + dt.Rows[i]["Etage"].ToString() + "',";
                                sqlstr2 += " Digicode = '" + dt.Rows[i]["Digicode"].ToString() + "',";
                                sqlstr2 += " Internom = '" + dt.Rows[i]["Inter_Nom"].ToString() + "',";
                                sqlstr2 += " Porte = '" + dt.Rows[i]["Porte"].ToString() + "',";

                                //Test pour la date
                                if (dt.Rows[i]["Date_Naissance"].ToString() != "")
                                {
                                    sqlstr2 += " DateNaissance = '" + dt.Rows[i]["Date_Naissance"].ToString() + "',";
                                }

                                //sqlstr2 += " DateNaissance = '" + dt.Rows[i]["Date_Naissance"]. + "',";

                                sqlstr2 += " Sexe = '" + dt.Rows[i]["Sexe"].ToString() + "',";
                                sqlstr2 += " Age = '" + dt.Rows[i]["Age"].ToString() + "',";
                                sqlstr2 += " UniteAge = '" + dt.Rows[i]["Age_Unite"].ToString() + "'";
                                sqlstr2 += " Where IdPersonne = " + DSResult.Tables["Patient"].Rows[0]["IdPersonne"].ToString();
                                //MessageBox.Show(sqlstr2);

                                //On ajoute un enregistrement dans la tableactes
                                //Auparavent, on cherche le plus grand n° d'actes

                                //On passe les parametres query et connection
                                SqlDataAdapter Query2 = new SqlDataAdapter("SELECT Max(Num) From Tableactes", dbConnection);

                                //on ajoute une table à cet ensemble de donnée pour y mettre le résultat
                                DSResult.Tables.Add("MaxTableActes");

                                //on execute
                                Query2.Fill(DSResult, "MaxTableActes");

                                //on determine le nouveau n° de l'acte
                                Nvx_Num_Actes = int.Parse(DSResult.Tables["MaxTableActes"].Rows[0][0].ToString()) + 1;


                                //On créé l'acte
                                sqlstr3 = "INSERT INTO tableactes";
                                sqlstr3 += " (Num, IndicePatient, Tel, DAP, DTR, DRC, DSL, DFI, CodeMotif1, CodeMotif2, ";
                                sqlstr3 += " Urgence, DelaiIndique, CommentaireTransmis, CommentaireFichier, ";
                                sqlstr3 += " AnnulationAppel, DAN, MotifAnnulation, DevenirAnnulation, ComplementInfo, Motif1, Motif2, ";
                                sqlstr3 += " OrigineAppel, CodeIntervenant, DelaiArrivee, DelaiInterv, ExportSmartOk)";
                                sqlstr3 += " VALUES (" + Nvx_Num_Actes.ToString() + "," + DSResult.Tables["Patient"].Rows[0]["IdPatient"].ToString() + ",'";
                                sqlstr3 += NewTelephone + "',";

                                //Test pour la dateAppel (DAP)
                                if (dt.Rows[i]["Date_Appel"].ToString() != "")
                                {
                                    sqlstr3 += "'" + dt.Rows[i]["Date_Appel"].ToString() + "',";
                                }
                                else sqlstr3 += "Default,";

                                //Test pour la dateTransMedecin (DTR)
                                if (dt.Rows[i]["Date_Trans_Medecin"].ToString() != "")
                                {
                                    sqlstr3 += "'" + dt.Rows[i]["Date_Trans_Medecin"].ToString() + "',";
                                }
                                else sqlstr3 += "Default,";

                                //Test pour la date Acquittement (DRC)
                                if (dt.Rows[i]["Date_Acquittement"].ToString() != "")
                                {
                                    sqlstr3 += "'" + dt.Rows[i]["Date_Acquittement"].ToString() + "',";
                                }
                                else sqlstr3 += "Default,";


                                //Test pour la date Sur Place (DSL)
                                if (dt.Rows[i]["Date_Entree_Visite"].ToString() != "")
                                {
                                    sqlstr3 += "'" + dt.Rows[i]["Date_Entree_Visite"].ToString() + "',";
                                }
                                else sqlstr3 += "Default,";

                                //Test pour la date Fin de visite (DFI)
                                if (dt.Rows[i]["Date_Fin_Visite"].ToString() != "")
                                {
                                    sqlstr3 += "'" + dt.Rows[i]["Date_Fin_Visite"].ToString() + "',";
                                }
                                else sqlstr3 += "Default,";

                                sqlstr3 += " Default,Default,Default,";
                                sqlstr3 += "Default,Default,Default,Default,Default,Default,Default,Default,'";
                                sqlstr3 += dt.Rows[i]["Motif_Lib1"].ToString() + "','";
                                sqlstr3 += dt.Rows[i]["Motif_Lib2"].ToString() + "',";
                                sqlstr3 += "Default,";

                                sqlstr3 += dt.Rows[i]["IDMedecin"].ToString() + ",";
                                sqlstr3 += " Default,Default,Default)";
                                //MessageBox.Show(sqlstr3);


                                //On ajoute un enregistrement dans la tableconsultations
                                //Auparavent, on cherche le plus grand n° de la table consultation

                                //On passe les parametres query et connection
                                SqlDataAdapter Query3 = new SqlDataAdapter("SELECT Max(NConsultation) From Tableconsultations", dbConnection);

                                //on déclare le DataSet qui va recevoir la donnée
                                //DataSet DSResult3 = new DataSet();

                                //on ajoute une table à cet ensemble de donnée pour y mettre le résultat
                                DSResult.Tables.Add("MaxTableConsultations");

                                //on execute
                                Query3.Fill(DSResult, "MaxTableConsultations");

                                //on determine le nouveau n° de la consultation
                                Nvx_Num_Consultations = int.Parse(DSResult.Tables["MaxTableConsultations"].Rows[0][0].ToString()) + 1;

                                //On créé la consultation
                                sqlstr4 = "INSERT INTO tableconsultations";
                                sqlstr4 += " (NConsultation, CodeAppel, IndicePatient, idDiag1, idDiag2, diag1, diag2, Hono, Reglement, actes, devenir, ";
                                sqlstr4 += " PriseEnChargePatient, LibCisp, Traitements, TraitementLibre, ";
                                sqlstr4 += " gestes, CommentaireLibre, EnvoiDocument, ListeIndexServiceExt, ListeIndexMt, TensionHaute, ";
                                sqlstr4 += " TensionBasse, O2, Pulsations, Temperature, Deces, Modifie, RapportGenere, FactureGeneree, Approuve, ";
                                sqlstr4 += " ExportSmartOk, ECGDecrypte, ImportECG, ParametresCliniques, RegulationCorrecte, AdresseCorrecte)";
                                sqlstr4 += " VALUES (" + Nvx_Num_Consultations.ToString() + "," + Nvx_Num_Actes.ToString() + "," + DSResult.Tables["Patient"].Rows[0]["IdPatient"].ToString() + ",";
                                sqlstr4 += " -1, -1, Default, Default, 0, Default, Default, Default, Default, Default, Default, Default, Default, Default, ";
                                sqlstr4 += " 'N', Default, Default, Default, Default, Default, Default, Default, Default, Default,";
                                sqlstr4 += " Default, Default, Default, Default, Default, Default, Default, Default, Default);";

                                //MessageBox.Show(sqlstr4);
                                //Ouverture d'une transaction
                                trans = dbConnection.BeginTransaction();

                                //On execute les requettes et on valide la transaction
                                try
                                {
                                    //on execute les requettes
                                    //Ouverture d'une transaction
                                    new SqlCommand(sqlstr2, dbConnection, trans).ExecuteNonQuery();
                                    new SqlCommand(sqlstr3, dbConnection, trans).ExecuteNonQuery();
                                    new SqlCommand(sqlstr4, dbConnection, trans).ExecuteNonQuery();

                                    //on valide la transaction
                                    trans.Commit();

                                    //on envoie la confirmation du webService
                                    //Création de l'instance du webservice d'EPOS
                                    WebServiceEpos.Service Ws2 = new WebServiceEpos.Service();


                                    //Récup des donnes du ws dans le DataTable
                                    Ws2.AcquittementReceptionRapport(int.Parse(dt.Rows[i]["Numero"].ToString()), int.Parse(DSResult.Tables["Patient"].Rows[0]["IdPatient"].ToString()));

                                }
                                catch (SqlException SqlError)
                                {
                                    trans.Rollback();
                                    MessageBox.Show("Erreur :" + SqlError.Message);
                                    //on reactive le bouton Fermer
                                    B2.Enabled = true;
                                }
                            }
                            else
                            {
                                //CREATION: sinon il n'existe pas, donc on créé la personne dans les tables:
                                //tablepersonne, tablepatient, tableactes, tableconsultations
                                //MessageBox.Show("n'existe Pas 2 ");
                                //Auparavent, on cherche le plus grand n° de personne                    
                                SqlDataAdapter Query0 = new SqlDataAdapter("SELECT Max(IdPersonne) From Tablepersonne", dbConnection);

                                //on ajoute une table à cet ensemble de donnée pour y mettre le résultat
                                DSResult.Tables.Add("MaxTablePersonne");

                                //on execute
                                Query0.Fill(DSResult, "MaxTablePersonne");

                                //on determine le nouveau n° de Personne
                                Nvx_Num_Personne = int.Parse(DSResult.Tables["MaxTablePersonne"].Rows[0][0].ToString()) + 1;

                                //La Longitude et la latitude (* par 100 000)
                                Longitude = int.Parse(dt.Rows[i]["Longitude"].ToString()) * 100000;
                                Latitude = int.Parse(dt.Rows[i]["Latitude"].ToString())* 100000;

                                //On créé la personne
                                sqlstr2 = "INSERT INTO tablepersonne";
                                sqlstr2 += " (IdPersonne, Tel, Nom, Prenom, NumAdresse, CodePostal, Departement, Commune, Rue, NumeroDansRue, ";
                                sqlstr2 += " Batiment, Escalier, Etage, Digicode, Internom, Porte, Longitude, Latitude, DateNaissance, DateDeces, ";
                                sqlstr2 += " Sexe, Age, UniteAge, TexteSup, ListeNoire, Adm_Batiment, Adm_NumeroDansRue, Adm_Rue, Adm_CodePostal, ";
                                sqlstr2 += " Adm_Commune, Chez)";
                                sqlstr2 += " VALUES (" + Nvx_Num_Personne.ToString() + ",'" + NewTelephone + "','" + dt.Rows[i]["Patient"].ToString() + "','";
                                sqlstr2 += dt.Rows[i]["Prenom"].ToString() + "',0,'" + dt.Rows[i]["Cpos"].ToString() + "','','" + dt.Rows[i]["Nom_Commune"].ToString() + "','";
                                sqlstr2 += dt.Rows[i]["Rue"].ToString() + "','" + dt.Rows[i]["Voie_Numero"].ToString() + "','" + dt.Rows[i]["Batiment"].ToString() + "','','";
                                sqlstr2 += dt.Rows[i]["Etage"].ToString() + "','" + dt.Rows[i]["Digicode"].ToString() + "','";
                                sqlstr2 += dt.Rows[i]["Inter_Nom"].ToString() + "','" + dt.Rows[i]["Porte"].ToString() + "',";
                                sqlstr2 += Longitude.ToString() + "," + Latitude.ToString() + ",";

                                //Test pour la date
                                if (dt.Rows[i]["Date_Naissance"].ToString() != "")
                                {
                                    sqlstr2 += "'" + dt.Rows[i]["Date_Naissance"].ToString() + "',";
                                }
                                else sqlstr2 += "Default,";

                                sqlstr2 += "Default,'" + dt.Rows[i]["Sexe"].ToString() + "'," + dt.Rows[i]["Age"].ToString() + ",'" + dt.Rows[i]["Age_Unite"].ToString() + "','";
                                sqlstr2 += dt.Rows[i]["Rue1"].ToString() + "','" + dt.Rows[i]["Rue1"].ToString() + "','";
                                sqlstr2 += dt.Rows[i]["Batiment"].ToString() + "','" + dt.Rows[i]["Voie_Numero"].ToString() + "','";
                                sqlstr2 += dt.Rows[i]["Rue"].ToString() + "','" + dt.Rows[i]["Cpos"].ToString() + "','";
                                sqlstr2 += dt.Rows[i]["Nom_Commune"].ToString() + "',Default)";
                                //MessageBox.Show(sqlstr2);
                                //On ajoute un enregistrement dans la tablepatient
                                //Auparavent, on cherche le plus grand n° de patient
                                SqlDataAdapter Query2 = new SqlDataAdapter("SELECT Max(IdPatient) From Tablepatient", dbConnection);

                                //on ajoute une table à cet ensemble de donnée pour y mettre le résultat
                                DSResult.Tables.Add("MaxTablePatient");

                                //on execute
                                Query2.Fill(DSResult, "MaxTablePatient");

                                //on determine le nouveau n° de patient
                                Nvx_Num_Patient = int.Parse(DSResult.Tables["MaxTablePatient"].Rows[0][0].ToString()) + 1;

                                //On créé le patient
                                sqlstr3 = "INSERT INTO tablepatient";
                                sqlstr3 += " (IdPatient, IdPersonne, SuiviPatient, IdAbonnement, TypeAbonnement, TexteAbonnement, TypeDestinataireFacture, ";
                                sqlstr3 += " IdDestinataireFacture, Approuve)";
                                sqlstr3 += " VALUES (" + Nvx_Num_Patient.ToString() + "," + Nvx_Num_Personne.ToString() + ",'',0,'','',0,0,0)";
                                //MessageBox.Show(sqlstr3);

                                //On ajoute un enregistrement dans la tableactes
                                //On passe les parametres query et connection
                                SqlDataAdapter Query3 = new SqlDataAdapter("SELECT Max(Num) From Tableactes", dbConnection);

                                //on ajoute une table à cet ensemble de donnée pour y mettre le résultat
                                DSResult.Tables.Add("MaxTableActes");

                                //on execute
                                Query3.Fill(DSResult, "MaxTableActes");

                                //on determine le nouveau n° de l'acte
                                Nvx_Num_Actes = int.Parse(DSResult.Tables["MaxTableActes"].Rows[0][0].ToString()) + 1;


                                //On créé l'acte
                                sqlstr4 = "INSERT INTO tableactes";
                                sqlstr4 += " (Num, IndicePatient, Tel, DAP, DTR, DRC, DSL, DFI, CodeMotif1, CodeMotif2, ";
                                sqlstr4 += " Urgence, DelaiIndique, CommentaireTransmis, CommentaireFichier, ";
                                sqlstr4 += " AnnulationAppel, DAN, MotifAnnulation, DevenirAnnulation, ComplementInfo, Motif1, Motif2, ";
                                sqlstr4 += " OrigineAppel, CodeIntervenant, DelaiArrivee, DelaiInterv, ExportSmartOk)";
                                sqlstr4 += " VALUES (" + Nvx_Num_Actes.ToString() + "," + Nvx_Num_Patient.ToString() + ",'";

                                //on format le telephone (xxx xxx xx xx)
                                NewTelephone = dt.Rows[i]["Tel_Patient"].ToString().Substring(0, 3) + " " + dt.Rows[i]["Tel_Patient"].ToString().Substring(3, 3) + " " +
                                dt.Rows[i]["Tel_Patient"].ToString().Substring(6, 2) + " " + dt.Rows[i]["Tel_Patient"].ToString().Substring(8, 2);

                                sqlstr4 += NewTelephone + "',";

                                //Test pour la dateAppel (DAP)
                                if (dt.Rows[i]["Date_Appel"].ToString() != "")
                                {
                                    sqlstr4 += "'" + dt.Rows[i]["Date_Appel"].ToString() + "',";
                                }
                                else sqlstr4 += "Default,";

                                //Test pour la dateTransMedecin (DTR)
                                if (dt.Rows[i]["Date_Trans_Medecin"].ToString() != "")
                                {
                                    sqlstr4 += "'" + dt.Rows[i]["Date_Trans_Medecin"].ToString() + "',";
                                }
                                else sqlstr4 += "Default,";

                                //Test pour la date Acquittement (DRC)
                                if (dt.Rows[i]["Date_Acquittement"].ToString() != "")
                                {
                                    sqlstr4 += "'" + dt.Rows[i]["Date_Acquittement"].ToString() + "',";
                                }
                                else sqlstr4 += "Default,";


                                //Test pour la date Sur Place (DSL)
                                if (dt.Rows[i]["Date_Entree_Visite"].ToString() != "")
                                {
                                    sqlstr4 += "'" + dt.Rows[i]["Date_Entree_Visite"].ToString() + "',";
                                }
                                else sqlstr4 += "Default,";

                                //Test pour la date Fin de visite (DFI)
                                if (dt.Rows[i]["Date_Fin_Visite"].ToString() != "")
                                {
                                    sqlstr4 += "'" + dt.Rows[i]["Date_Fin_Visite"].ToString() + "',";
                                }
                                else sqlstr4 += "Default,";

                                sqlstr4 += " Default,Default,Default,";
                                sqlstr4 += "Default,Default,Default,Default,Default,Default,Default,Default,'";
                                sqlstr4 += dt.Rows[i]["Motif_Lib1"].ToString() + "','";
                                sqlstr4 += dt.Rows[i]["Motif_Lib2"].ToString() + "',";
                                sqlstr4 += "Default,";

                                sqlstr4 += dt.Rows[i]["IDMedecin"].ToString() + ",";
                                sqlstr4 += " Default,Default,Default)";
                                //MessageBox.Show(sqlstr4);

                                //On ajoute un enregistrement dans la tableconsultations
                                //Auparavent, on cherche le plus grand n° de la table consultation

                                //On passe les parametres query et connection
                                SqlDataAdapter Query4 = new SqlDataAdapter("SELECT Max(NConsultation) From Tableconsultations", dbConnection);

                                //on ajoute une table à cet ensemble de donnée pour y mettre le résultat
                                DSResult.Tables.Add("MaxTableConsultations");

                                //on execute
                                Query4.Fill(DSResult, "MaxTableConsultations");

                                //on determine le nouveau n° de la consultation
                                Nvx_Num_Consultations = int.Parse(DSResult.Tables["MaxTableConsultations"].Rows[0][0].ToString()) + 1;

                                //On créé la consultation
                                sqlstr5 = "INSERT INTO tableconsultations";
                                sqlstr5 += " (NConsultation, CodeAppel, IndicePatient, idDiag1, idDiag2, diag1, diag2, Hono, Reglement, actes, devenir, ";
                                sqlstr5 += " PriseEnChargePatient, LibCisp, Traitements, TraitementLibre, ";
                                sqlstr5 += " gestes, CommentaireLibre, EnvoiDocument, ListeIndexServiceExt, ListeIndexMt, TensionHaute, ";
                                sqlstr5 += " TensionBasse, O2, Pulsations, Temperature, Deces, Modifie, RapportGenere, FactureGeneree, Approuve, ";
                                sqlstr5 += " ExportSmartOk, ECGDecrypte, ImportECG, ParametresCliniques, RegulationCorrecte, AdresseCorrecte)";
                                sqlstr5 += " VALUES (" + Nvx_Num_Consultations.ToString() + "," + Nvx_Num_Actes.ToString() + "," + Nvx_Num_Patient.ToString() + ",";
                                sqlstr5 += " -1, -1, Default, Default, 0, Default, Default, Default, Default, Default, Default, Default, Default, Default, ";
                                sqlstr5 += " 'N', Default, Default, Default, Default, Default, Default, Default, Default, Default,";
                                sqlstr5 += " Default, Default, Default, Default, Default, Default, Default, Default, Default);";

                                //MessageBox.Show(sqlstr5);
                                //Ouverture d'une transaction
                                trans = dbConnection.BeginTransaction();

                                //On execute les requettes et on valide la transaction
                                try
                                {
                                    //on execute les requettes
                                    //Ouverture d'une transaction
                                    new SqlCommand(sqlstr2, dbConnection, trans).ExecuteNonQuery();
                                    new SqlCommand(sqlstr3, dbConnection, trans).ExecuteNonQuery();
                                    new SqlCommand(sqlstr4, dbConnection, trans).ExecuteNonQuery();
                                    new SqlCommand(sqlstr5, dbConnection, trans).ExecuteNonQuery();

                                    //on valide la transaction
                                    trans.Commit();

                                    //on envoie la confirmation du webService
                                    //Création de l'instance du webservice d'EPOS
                                    WebServiceEpos.Service Ws2 = new WebServiceEpos.Service();

                                    //Récup des donnes du ws dans le DataTable
                                    Ws2.AcquittementReceptionRapport(int.Parse(dt.Rows[i]["Numero"].ToString()), int.Parse(Nvx_Num_Patient.ToString()));

                                }
                                catch (SqlException SqlError)
                                {
                                    trans.Rollback();
                                    MessageBox.Show("Erreur :" + SqlError.Message);
                                    //on reactive le bouton Fermer
                                    B2.Enabled = true;
                                }
                            }

                        }


                    }
                    
                    //On ferme les connctions
                    dbConnection.Close();

                    //on remet à blanc la chaine de connection
                    dbConnection = null;

                }
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show("Aucun rapport à récupérer");
                //on reactive le bouton Fermer
                B2.Enabled = true;
            }

            //on reactive le bouton Fermer
            B2.Enabled = true;
            B1.Enabled = false;
            //on modifie le label1
            label1.Text = "Transaction terminé.";


        }

        private void button1_Click(object sender, EventArgs e)
        {
            //On ferme la forme
            Close();
        }
    }
}