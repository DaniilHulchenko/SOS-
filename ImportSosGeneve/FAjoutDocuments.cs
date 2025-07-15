using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using SosMedecins.SmartRapport.GestionApplication;

namespace ImportSosGeneve
{
    public partial class FAjoutDocuments : Form
    {
        DataTable DtRechercheDoc = new DataTable();
        public string Patient;
        public string filename;

        public FAjoutDocuments(string Id)
        {
            InitializeComponent();

            Patient = Id;
            filename = "";
            label6.Text = "Pas de fichier choisi.";
            ChargeListe(Id);
        }

        private void ChargeListe(string Id)
        {
            //On recherche la personne
            string connex = System.Configuration.ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);         //Chaine de connexion récupéré dans appconfig

            //On ouvre la connexion
            dbConnection.Open();

            SqlCommand cmd = dbConnection.CreateCommand();
            cmd.Connection = dbConnection;

            try
            {
                //On defini la requette
                string sqlstr0 = "SELECT pa.IdPatient, pe.Nom + ' ' + pe.Prenom as personne, pe.DateNaissance FROM TablePersonne pe, TablePatient pa";
                sqlstr0 += " WHERE pe.IdPersonne = pa.IdPersonne AND pa.IdPatient = @IdPatient";

                //Ajout des parametres
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("IdPatient", long.Parse(Id));
                cmd.CommandText = sqlstr0;

                DataTable dtPatient = new DataTable();
                dtPatient.Load(cmd.ExecuteReader());

                if (dtPatient.Rows.Count > 0)
                {
                    //Affichage des infos patients
                    label1.Text = "Patient(e) : " + dtPatient.Rows[0]["personne"].ToString();

                    DateTime DateNaiss = DateTime.Parse(dtPatient.Rows[0]["DateNaissance"].ToString());

                    label2.Text = "né(e) le : " + DateNaiss.ToString("dd.MM.yyyy");

                    //Puis on affiche la liste des docs
                    string sqlstr1 = "SELECT UrlJointDoc, IdPatient FROM patientjointdoc ";
                    sqlstr1 += " WHERE IdPatient = @IdPatient";

                    //Ajout des parametres
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("IdPatient", long.Parse(Id));
                    cmd.CommandText = sqlstr1;

                    DtRechercheDoc.Clear();
                    DtRechercheDoc.Load(cmd.ExecuteReader());


                    if (DtRechercheDoc.Rows.Count != 0)
                    {
                        listBoxDoc.BeginUpdate();       //Pour stopper le rafraichissement
                        listBoxDoc.Items.Clear();

                        int index = 0;

                        foreach (DataRow Row in DtRechercheDoc.Rows)
                        {
                            //On décompose le chemin du document
                            string Doc = DtRechercheDoc.Rows[index][0].ToString();

                            string typeDoc = Doc.Substring(Doc.IndexOf('_') + 1, (Doc.Length - Doc.IndexOf('_') - 1));
                            string typeDoc1 = typeDoc.Substring(0, typeDoc.IndexOf('_'));

                            String DateDoc = typeDoc.Substring(typeDoc.IndexOf('_') + 1, 10);

                            listBoxDoc.Items.Add(typeDoc1 + " du " + DateDoc);

                            index++;
                        }

                        listBoxDoc.EndUpdate();     //Fin de la mise à jour, on réactive le rafraîchissement                                          
                    }
                    else
                    {
                        listBoxDoc.Items.Clear();    //Sinon on efface la liste

                        //et on désactive les boutons
                        bViualiser.ImageIndex = 3;
                        bSupprimer.ImageIndex = 5;

                        bViualiser.Enabled = false;
                        bSupprimer.Enabled = false;
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur : " + e.Message);
            }
            finally
            {
                //fermeture de la connexion
                if (dbConnection.State == System.Data.ConnectionState.Open)
                    dbConnection.Close();
            }

        }

      

        //Visualiser le doc
        private void bViualiser_Click(object sender, EventArgs e)
        {
            //On recherche le chemin (On regarde si on a pas cliqué dans le vide dans la liste....index -1)
            int index = listBoxDoc.SelectedIndex;

            if (index != -1)
            {               
                //on récupère le chemin
                string CheminDoc = SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.Path_DocumentsSmartRapport;   //Chemin des documents Smartrapport sur le SDBSOS (192.168.0.8)                    
                string Fichier = DtRechercheDoc.Rows[index][0].ToString();                                                  

                //Puis on affiche le doc
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo.FileName = CheminDoc + Fichier;
                proc.Start();
            }
        }

        //Supprimer le doc
        private void bSupprimer_Click(object sender, EventArgs e)
        {
            //On recherche le chemin (On regarde si on a pas cliqué dans le vide dans la liste....index -1)
            int index = listBoxDoc.SelectedIndex;

            if (index != -1)
            {
                //On demande une confirmation de la suppression
                if (MessageBox.Show("Voulez-vous effacer ce document?", "Suppression de " + listBoxDoc.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //on récupère le chemin
                    string CheminDoc = SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.Path_DocumentsSmartRapport;   //Chemin des documents Smartrapport sur le SDBSOS (192.168.0.8)                    
                    string Fichier = DtRechercheDoc.Rows[index][0].ToString();
                  
                    //On commence par effacer dans la base l'enregistrement
                    string connex = System.Configuration.ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
                    SqlConnection dbConnection = new SqlConnection(connex);         //Chaine de connexion récupéré dans appconfig

                    //On ouvre la connexion
                    dbConnection.Open();

                    SqlCommand cmd = dbConnection.CreateCommand();

                    //On démarre une transaction locale
                    SqlTransaction transaction;
                    transaction = dbConnection.BeginTransaction("Suppression_Doc");

                    try
                    {
                        cmd.Connection = dbConnection;
                        cmd.Transaction = transaction;

                        //On defini la requette
                        string sqlstr0 = "DELETE FROM patientjointdoc WHERE UrlJointDoc= @url and IdPatient = @Patient";

                        //Ajout des parametres
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("url", Fichier);
                        cmd.Parameters.AddWithValue("Patient", DtRechercheDoc.Rows[index][1].ToString());
                        cmd.CommandText = sqlstr0;

                        //Execution de la requette
                        cmd.ExecuteNonQuery();

                        //Puis on efface le doc (si pas d'erreur)
                        System.IO.File.Delete(CheminDoc + Fichier);

                        //On valide la transaction
                        transaction.Commit();

                        mouchard.evenement("Suppression du document " + Fichier + " pour le " + label1.Text, VariablesApplicatives.Utilisateurs.NomUtilisateur.ToString());  //log            
                    }
                    catch (Exception a)
                    {
                        Console.WriteLine("Erreur : " + a.Message);
                        MessageBox.Show("Erreur lors de la suppression du document....l'erreur est : " + a.Message);
                        transaction.Rollback();
                    }
                    finally
                    {
                        //fermeture de la connexion
                        if (dbConnection.State == System.Data.ConnectionState.Open)
                            dbConnection.Close();
                    }

                    //On rafraichi la liste
                    ChargeListe(Patient);
                }
            }
        }

        private void ListTypeDoc_TextChanged(object sender, EventArgs e)
        {
            //si le champs type n'est pas vide, on active le bouton Ajouter s'il ne l'est pas déjà
            if (ListTypeDoc.Text != "")
            {
                bAjout.Enabled = true;
                bAjout.ImageIndex = 6;
            }
            else
            {
                bAjout.Enabled = false;
                bAjout.ImageIndex = 7;
            }
        }

        private void bAjout_Click(object sender, EventArgs e)
        {
            //On va chercher le fichier à ajouter                                   
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Tous les fichiers (*.*) | *.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filename = openFileDialog1.FileName;
                label6.Text = Path.GetFileName(filename);

                //on active le bouton valider et verifier
                bVerifier.Enabled = true;                
                bValideAjout.Enabled = true;
                bVerifier.ImageIndex = 2;
                bValideAjout.ImageIndex = 0;
            }
            else
            {
                //on désactive le bouton valider et vérifier
                bValideAjout.Enabled = false;
                bVerifier.Enabled = false;
                bValideAjout.ImageIndex = 1;
                bVerifier.ImageIndex = 3;
            }

        }


        private void bVerifier_Click(object sender, EventArgs e)
        {
            //On vérifie le document avant de l'ajouter                        
            if (filename != "")
            {               
                //On affiche le doc
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo.FileName = filename;
                proc.Start();
            }
        }


        private void bValideAjout_Click(object sender, EventArgs e)
        {

            if (filename != "")
            {
                //On commence par ajouter dans la base, l'enregistrement
                System.Random rnd = new System.Random(DateTime.Now.Millisecond);     //Pour avoir un chiffre entre 1000 et 9999

                string cheminDest = SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.Path_DocumentsSmartRapport;   //Chemin des documents Smartrapport sur le SDBSOS (192.168.0.8)

                string dated = DateTime.Today.ToString("dd.MM.yyyy");

                string Extention = Path.GetExtension(filename);

                int nbAleatoire = rnd.Next(1000, 9999);

                string Destination = cheminDest + Patient + "_" + ListTypeDoc.Text + "_" + dated + "_" + nbAleatoire.ToString() + Extention;
                string NomFichier = Patient + "_" + ListTypeDoc.Text + "_" + dated + "_" + nbAleatoire.ToString() + Extention;               

                string connex = System.Configuration.ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
                SqlConnection dbConnection = new SqlConnection(connex);         //Chaine de connexion récupéré dans appconfig

                //On ouvre la connexion
                dbConnection.Open();

                SqlCommand cmd = dbConnection.CreateCommand();

                //On démarre une transaction locale
                SqlTransaction transaction;
                transaction = dbConnection.BeginTransaction("Ajout_Doc");

                try
                {
                    cmd.Connection = dbConnection;
                    cmd.Transaction = transaction;

                    //On defini la requette
                    string sqlstr0 = "INSERT INTO patientjointdoc (UrlJointDoc, IdPatient) VALUES (@Fichier, @Patient)";

                    //Ajout des parametres
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("Fichier", NomFichier);
                    cmd.Parameters.AddWithValue("Patient", Patient);
                    cmd.CommandText = sqlstr0;

                    //Execution de la requette
                    cmd.ExecuteNonQuery();

                    //Puis on ajoute le doc (si pas d'erreur)
                    System.IO.File.Copy(filename, Destination, false);

                    //On valide la transaction
                    transaction.Commit();

                    mouchard.evenement("Ajout du document " + NomFichier + " pour le " + label1.Text, VariablesApplicatives.Utilisateurs.NomUtilisateur.ToString());  //log            
                }
                catch (Exception a)
                {
                    Console.WriteLine("Erreur : " + a.Message);
                    MessageBox.Show("Erreur lors d'ajout du document....l'erreur est : " + a.Message);
                    transaction.Rollback();
                }
                finally
                {
                    //fermeture de la connexion
                    if (dbConnection.State == System.Data.ConnectionState.Open)
                        dbConnection.Close();
                    filename = "";    //On remet la variable filemane à vide
                    label6.Text = "Pas de fichier choisi.";
                    ListTypeDoc.Text = "";

                    //Puis on désactive le bouton pour eviter de le ressaisir
                    bAjout.ImageIndex = 7;
                    bVerifier.ImageIndex = 3;
                    bValideAjout.ImageIndex = 1;

                    bAjout.Enabled = false;
                    bVerifier.Enabled = false;
                    bValideAjout.Enabled = false;
                }

                //On rafraichi la liste
                ChargeListe(Patient);
            }       //Fin de filename != ""

        }

        private void bFermer_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void listBoxDoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxDoc.SelectedIndex != -1)         //Si on a choisi quelque chose...
            {
                //activation des boutons
                bViualiser.ImageIndex = 2;
                bSupprimer.ImageIndex = 4;

                bViualiser.Enabled = true;
                bSupprimer.Enabled = true;
            }
            else
            {
                //désactivation des boutons
                bViualiser.ImageIndex = 3;
                bSupprimer.ImageIndex = 5;

                bViualiser.Enabled = false;
                bSupprimer.Enabled = false;
            }
        }

        


    }
}


//A faire:
//tester