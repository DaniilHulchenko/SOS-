using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace ImportSosGeneve
{
    public partial class FAjoutDocumentsFacture : Form
    {
        DataTable DtRechercheDoc = new DataTable();
        public string Facture;
        public string filename;

        public FAjoutDocumentsFacture(string NumFacture)
        {
            InitializeComponent();

            Facture = NumFacture;
            filename = "";
            label6.Text = "Pas de fichier choisi.";

            //Si on a une facture
            if (Facture != "-1")
            {
                //On désactive la recherche qui passe par le menu
                label8.Visible = false;
                tNumFacture.Visible = false;
                bCherche.Visible = false;
                bCherche.Enabled = false;

                ChargeListe(Facture);
            }
            else
            {
                //On ré-active la recherche qui passe par le menu
                label8.Visible = true;
                tNumFacture.Visible = true;
                bCherche.Visible = true;
                bCherche.Enabled = true;
                
                //Affichage des infos patients
                label7.Text = "Pas de facture choisie";
                label1.Text = "";                
                label2.Text = "";
            }
            
        }


        private void bCherche_Click(object sender, EventArgs e)
        {
            if (tNumFacture.Text != "")
            {
                Facture = tNumFacture.Text;
                ChargeListe(Facture);
            }
        }

        private void ChargeListe(string NumFacture)
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
                string sqlstr0 = "SELECT tp.IdPatient, pe.Nom + ' ' + pe.Prenom as personne, pe.DateNaissance";
                sqlstr0 += " FROM TablePersonne pe, TablePatient tp, tableconsultations tc, factureconsultation fc";
                sqlstr0 += " WHERE pe.IdPersonne = tp.IdPersonne";
                sqlstr0 += " and tp.IdPatient = tc.IndicePatient";
                sqlstr0 += " and tc.NConsultation = fc.NConsultation";
                sqlstr0 += " and fc.NFacture = @NumFacture";

                //Ajout des parametres
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("NumFacture", long.Parse(NumFacture));
                cmd.CommandText = sqlstr0;

                DataTable dtPatient = new DataTable();
                dtPatient.Load(cmd.ExecuteReader());


                if (dtPatient.Rows.Count > 0)
                {
                    //Affichage des infos patients
                    label7.Text = "Facture n° " + NumFacture + ", conserne";
                    
                    label1.Text = "Patient(e) : " + dtPatient.Rows[0]["personne"].ToString();

                    //DateTime DateNaiss = DateTime.Parse(dtPatient.Rows[0]["DateNaissance"].ToString()); // ERROR

                    label2.Text = "né(e) le : " + dtPatient.Rows[0]["DateNaissance"].ToString();

                    //On met par défaut "cession de créance" dans la ListTypeDoc....pour aller plus vite
                    ListTypeDoc.Text = "Cession de creance";

                    //Puis on affiche la liste des docs
                    string sqlstr1 = "SELECT UrlJointDoc, NFacture FROM facturejointdoc ";
                    sqlstr1 += " WHERE NFacture = @NumFacture";

                    //Ajout des parametres
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("NumFacture", long.Parse(NumFacture));
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

                            string typeDoc = Doc.Substring(0, Doc.IndexOf('_'));
                            string DateDoc = Doc.Substring(Doc.IndexOf('_') + 1, (Doc.Length - Doc.IndexOf('_') - 1));

                            DateDoc = DateDoc.Substring(DateDoc.IndexOf('_') + 1, 10);                      

                            listBoxDoc.Items.Add(typeDoc + " du " + DateDoc);

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
                MessageBox.Show("Erreur : " + e.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                //on récupère le chemin dans le datatable
                //string CheminDoc = DtRechercheDoc.Rows[index][0].ToString().Replace("|", "\\");
                string CheminDoc = SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.Path_DocFacture + DtRechercheDoc.Rows[index][0].ToString();

                //Puis on affiche le doc
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo.FileName = CheminDoc;
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
                    //on récupère le chemin dans le datatable
                    string CheminDoc = SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.Path_DocFacture + DtRechercheDoc.Rows[index][0].ToString();

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
                        string sqlstr0 = "DELETE FROM facturejointdoc WHERE UrlJointDoc= @url and NFacture = @Facture";

                        //Ajout des parametres
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("url", DtRechercheDoc.Rows[index][0].ToString());
                        cmd.Parameters.AddWithValue("Facture", DtRechercheDoc.Rows[index][1].ToString());
                        cmd.CommandText = sqlstr0;

                        //Execution de la requette
                        cmd.ExecuteNonQuery();

                        //Puis on efface le doc (si pas d'erreur)
                        System.IO.File.Delete(CheminDoc);

                        //On valide la transaction
                        transaction.Commit();
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
                    ChargeListe(Facture);
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
            openFileDialog1.InitialDirectory = SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.DocumentScannee;
            openFileDialog1.Filter = "Fichier pdf | *.pdf";            
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

                string cheminDest = SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.Path_DocFacture;    //Chemin des documents joints aux factures Smartrapport sur le SDBSOS (192.168.0.8)

                string dated = DateTime.Today.ToString("dd.MM.yyyy");

                string Extention = Path.GetExtension(filename);

                string filenameDest = ListTypeDoc.Text + "_" + Facture + "_" + dated + "_" + rnd.Next(1000, 9999).ToString() + Extention;
                string filedest = cheminDest + filenameDest;
               
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
                    string sqlstr0 = "INSERT INTO facturejointdoc (UrlJointDoc, NFacture) VALUES (@Fichier, @NFacture)";

                    //Ajout des parametres
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("Fichier", filenameDest);
                    cmd.Parameters.AddWithValue("NFacture", Facture);
                    cmd.CommandText = sqlstr0;

                    //Execution de la requette
                    cmd.ExecuteNonQuery();

                    //Puis on ajoute le doc (si pas d'erreur)
                    System.IO.File.Copy(filename, filedest);

                    //Puis on supprime le fichier source du scan
                    System.IO.File.Delete(filename);

                    //On valide la transaction
                    transaction.Commit();
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
                ChargeListe(Facture);
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

        private void tNumFacture_MouseClick(object sender, MouseEventArgs e)
        {
            //On vide le contenu
            tNumFacture.Text = "";
            Facture = "";    //On réinitialise le n° de facture

            //Affichage des infos patients
            label7.Text = "Pas de facture choisie";
            label1.Text = "";
            label2.Text = "";

            listBoxDoc.Items.Clear();
        }

        private void tNumFacture_KeyDown(object sender, KeyEventArgs e)
        {
            //Si on presse entrée
            if (e.KeyCode == Keys.Return)
            {
                bCherche_Click(sender,e);
            }
        }
                    
    }
}


//A faire: