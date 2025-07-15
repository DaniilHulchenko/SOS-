using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImportSosGeneve.Commun
{
    public partial class AjoutElements : Form
    {
        public int NumAppel = 0;
        public string Type = "";
        DataSet dsListes = new DataSet();
        ListViewItem itemListe;

        //pour savoir si des modifications sont en cours
        bool Modif = false;

        bool btnAnnulerPress = false;

        bool btnValiderPress = false;

        public AjoutElements()
        {
            InitializeComponent();

            //création des colonnes pour les listView
            listViewListe.Columns.Add("Id", 1);     //colonne invisible
            listViewListe.Columns.Add("Libelle", 180);
            listViewListe.Columns.Add("Code", 80);
            listViewListe.View = View.Details;
            listViewListe.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;

            listViewAjout.Columns.Add("Qte", 25);
            listViewAjout.Columns.Add("Libelle", 175);
            listViewAjout.Columns.Add("Code", 80);
            listViewAjout.Columns.Add("Id", 1);     //colonne invisible
            listViewAjout.View = View.Details;
            listViewAjout.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
        }

        private void AjouterActesTech_Load(object sender, EventArgs e)
        {
            //*****Gestion des écrans ****** On affiche la form sur la même que FicheConsult
            Screen[] screens = Screen.AllScreens;           //On recupère tout les écrans
            int NbEcran = Screen.AllScreens.Length;         //on récupère le nombre d'ecran
            int EcranFicheConsult = 0;

            if (NbEcran > 1)
            {
                //on regarde sur quel écran est la form FicheConsult
                var FormFicheConsult = Application.OpenForms["FicheConsult"];
                if (FormFicheConsult != null)
                {
                    for (int i = 0; i < NbEcran; i++)
                    {
                        Point formTopLeft = new Point(FormFicheConsult.Left, FormFicheConsult.Top);   //On récupère le point haut à gauche

                        if (screens[i].WorkingArea.Contains(formTopLeft))    //Pour pouvoir localiser l'écran
                        {
                            EcranFicheConsult = i;          //C'est celui là!
                        }
                        // else
                        //   MessageBox.Show("Form principale sur l'écran " + screens[i].Primary + screens[i].DeviceName);                       
                    }
                }

                //on met la position de démarrage en manuel
                this.StartPosition = FormStartPosition.Manual;

                //On affiche la forme sur le même écran     
                this.Location = Screen.AllScreens[EcranFicheConsult].WorkingArea.Location;
            }
            else  //Pas de 2eme écran
            {
                //on met la position de démarrage en manuel
                this.StartPosition = FormStartPosition.Manual;

                this.Location = new Point(0, 0);
            }

            AfficheListeElements(dsListes);
            AfficheListeAjout(dsListes);
        }

        //pour afficher la liste des éléments
        public void AfficheListeElements(DataSet dataSet)
        {
            //Chaine de connection
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base_Fiche"].ToString();
            MySqlConnection dbConnection = new MySqlConnection(connex);

            dbConnection.Open();

            MySqlCommand cmd = new MySqlCommand();

            cmd.Connection = dbConnection;

            try
            {
                String requete1 = "SELECT Id, Libelle, Code FROM tecmatmedic t";
                requete1 += "      WHERE t.Categorie = " + "'" + Type + "'";

                //on execute la requête
                cmd.CommandText = requete1;
                dataSet.Tables.Add("ListeMedocs");
                dataSet.Tables["ListeMedocs"].Load(cmd.ExecuteReader());

                listViewListe.BeginUpdate();
                listViewListe.Items.Clear();

                for (int i = 0; i < dataSet.Tables["ListeMedocs"].Rows.Count; i++)
                {
                    itemListe = new ListViewItem(dataSet.Tables["ListeMedocs"].Rows[i]["Id"].ToString());
                    itemListe.SubItems.Add(dataSet.Tables["ListeMedocs"].Rows[i]["Libelle"].ToString());
                    itemListe.SubItems.Add(dataSet.Tables["ListeMedocs"].Rows[i]["Code"].ToString());
                    listViewListe.Items.Add(itemListe);
                }

                listViewListe.EndUpdate();
            }
            catch (Exception e)
            {
                MessageBox.Show("Impossible de récupérer la liste des éléments" + e.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //fermeture des connexions
                if (dbConnection.State == ConnectionState.Open)
                {
                    dbConnection.Close();
                }
            }
        }

        //pour afficher la liste des éléments déjà ajoutés
        private void AfficheListeAjout (DataSet dataSet)
        {
            //Chaine de connection
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base_Fiche"].ToString();
            MySqlConnection dbConnection = new MySqlConnection(connex);

            dbConnection.Open();

            MySqlCommand cmd = new MySqlCommand();

            cmd.Connection = dbConnection;

            try
            {
                String requete1 = "SELECT Qte, Libelle, Code, IdTMM FROM listeamm l";
                requete1 += "      WHERE l.Num_Appel = " + NumAppel;
                requete1 += "      AND l.Categorie = " + "'" + Type + "'";

                //on execute les requête
                cmd.CommandText = requete1;
                dataSet.Tables.Add("ListeMedocsAjout");
                dataSet.Tables["ListeMedocsAjout"].Load(cmd.ExecuteReader());

                listViewAjout.BeginUpdate();
                listViewAjout.Items.Clear();

                for (int i = 0; i < dataSet.Tables["ListeMedocsAjout"].Rows.Count; i++)
                {
                    itemListe = new ListViewItem(dataSet.Tables["ListeMedocsAjout"].Rows[i]["Qte"].ToString());
                    itemListe.SubItems.Add(dataSet.Tables["ListeMedocsAjout"].Rows[i]["Libelle"].ToString());
                    itemListe.SubItems.Add(dataSet.Tables["ListeMedocsAjout"].Rows[i]["Code"].ToString());
                    itemListe.SubItems.Add(dataSet.Tables["ListeMedocsAjout"].Rows[i]["IdTMM"].ToString());
                    listViewAjout.Items.Add(itemListe);
                }

                listViewAjout.EndUpdate();
            }
            catch (Exception e)
            {
                MessageBox.Show("Impossible de récupérer la liste des ajouts " + e.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //fermeture des connexions
                if (dbConnection.State == ConnectionState.Open)
                {
                    dbConnection.Close();
                }
            }
        }

        //pour ajouter un élément dans la liste
        private void AjoutListe(){
            //verif si un item est sélectionné
            if (listViewListe.SelectedItems.Count > 0)
            {
                //contenu de chaque item
                String[] row = { "1", listViewListe.SelectedItems[0].SubItems[1].Text, listViewListe.SelectedItems[0].SubItems[2].Text, 
                    listViewListe.SelectedItems[0].SubItems[0].Text };

                //si il y a plus d'un enregistrement
                if (listViewAjout.Items.Count > 0)
                {
                    bool ajoute = false;
                    //on boucle sur chaque item de la liste
                    for (int i = 0; i < listViewAjout.Items.Count; i++)
                    {
                        //si il existe déjà dans la liste on ajoute 1
                        if (listViewAjout.Items[i].SubItems[1].Text == listViewListe.SelectedItems[0].SubItems[1].Text)
                        {
                            int qte = 0;
                            qte = Int32.Parse(listViewAjout.Items[i].SubItems[0].Text);
                            listViewAjout.Items[i].Text = (qte + 1).ToString();
                            ajoute = true;
                        }
                    }

                    if (!ajoute)
                    {
                        //sinon on l'ajoute dans la liste
                        ListViewItem item = new ListViewItem(row);
                        listViewAjout.Items.Add(item);
                    }
                }
                else
                {
                    //on ajoute l'item dans la liste
                    ListViewItem item = new ListViewItem(row);
                    listViewAjout.Items.Add(item);
                }

                //une modif a été faite
                Modif = true;
            }
        }

        //pour supprimer un élément de la liste
        private void SupprListe(bool doubleClic)
        {
            //verif si un item est sélectionné
            if (listViewAjout.SelectedItems.Count > 0)
            {
                //si il y en a qu'un on le supprime de la liste (si on double clic on le supprime de la liste peut importe le nombre)
                if(listViewAjout.SelectedItems[0].SubItems[0].Text == "1" || doubleClic)
                {
                    listViewAjout.SelectedItems[0].Remove();
                }
                //sinon on fait -1
                else
                {
                    int qte = 0;
                    qte = Int32.Parse(listViewAjout.SelectedItems[0].SubItems[0].Text);
                    listViewAjout.SelectedItems[0].SubItems[0].Text = (qte - 1).ToString();
                }

                //une modif a été faite
                Modif = true;
            }
        }

        private void AppliquerModifs()
        {
            for (int i = 0; i < listViewAjout.Items.Count; i++)
            {
                string qte = listViewAjout.Items[i].SubItems[0].Text;
                string libelle = listViewAjout.Items[i].SubItems[1].Text;
                string code = listViewAjout.Items[i].SubItems[2].Text;
                string idElement = listViewAjout.Items[i].SubItems[3].Text;
                bool existe = ChercheListeBase(idElement);

                //si il existe on le modifie
                if (existe)
                {
                    MajQte(idElement, qte);
                }
                //sinon on l'ajoute
                else
                {
                    AjouterElement(idElement, libelle, code, qte);
                }
            }

            //si il y a moin d'enregistrement dans la liste que dans la base
            //c'est qu'un enregistrement a été supprimé
            if(listViewAjout.Items.Count < dsListes.Tables["ListeMedocsAjout"].Rows.Count)
            {
                VerifChampBase();
            }
        }

        
        private void VerifChampBase()
        {
            DataTable dt = new DataTable();

            //Chaine de connection
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base_Fiche"].ToString();
            MySqlConnection dbConnection = new MySqlConnection(connex);

            dbConnection.Open();

            MySqlCommand cmd = new MySqlCommand();

            cmd.Connection = dbConnection;

            try
            {
                String requete1 = "SELECT * FROM listeamm l";
                requete1 += "      WHERE l.Num_Appel = " + NumAppel;
                requete1 += "      AND l.Categorie = " + "'" + Type + "'";

                //on execute la requête
                cmd.CommandText = requete1;
                dt.Load(cmd.ExecuteReader());                

                //on boucle sur un champ de la base
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    bool trouve = false;
                    
                    //et on boucle sur chaque item de la liste pout voir si le champs de la base est dans la liste
                    for(int i2 = 0; i2 < listViewAjout.Items.Count; i2++)
                    {
                        if (listViewAjout.Items[i2].SubItems[3].Text == dt.Rows[i]["IdTMM"].ToString())
                        {
                            //l'enregistrement a été trouvé
                            trouve = true;
                        }                     
                    }

                    //si on a pas trouvé l'enregistrement
                    if (!trouve)
                        SupprChampBase(dt.Rows[i]["IdTMM"].ToString()); //on supprime le champ de la base
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Impossible de vérifier les champs " + e.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //fermeture des connexions
                if (dbConnection.State == ConnectionState.Open)
                {
                    dbConnection.Close();
                }
            }
        }

        //pour supprimer un champs de la base
        private void SupprChampBase(string id)
        {
            //Chaine de connection
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base_Fiche"].ToString();
            MySqlConnection dbConnection = new MySqlConnection(connex);

            dbConnection.Open();

            MySqlCommand cmd = new MySqlCommand();

            cmd.Connection = dbConnection;

            try
            {
                //on supprime le champ de la base
                String requete1 = "DELETE FROM listeamm ";
                requete1 += " WHERE Num_Appel = '" + NumAppel + "'";
                requete1 += " AND IdTMM = '" + id + "'";

                //on execute la requête
                cmd.CommandText = requete1;
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("Impossible de supprimer le champ de la base " + e.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //fermeture des connexions
                if (dbConnection.State == ConnectionState.Open)
                {
                    dbConnection.Close();
                }
            }
        }

        //pour mettre à jour la quantité dans la base
        private void MajQte(string idElement, string qte)
        {
            //Chaine de connection
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base_Fiche"].ToString();
            MySqlConnection dbConnection = new MySqlConnection(connex);

            dbConnection.Open();

            MySqlCommand cmd = new MySqlCommand();

            cmd.Connection = dbConnection;

            try
            {
                String requete1 = "UPDATE listeamm";
                requete1 += "      SET Qte = '" + qte + "'";
                requete1 += "      WHERE IdTMM = '" + idElement + "'";
                requete1 += "      AND Num_Appel = '" + NumAppel + "'";

                //on execute les requête
                cmd.CommandText = requete1;
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("Impossible de récupérer la liste des ajouts " + e.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //fermeture des connexions
                if (dbConnection.State == ConnectionState.Open)
                {
                    dbConnection.Close();
                }

                btnValiderPress = true;
            }
        }

        //pour ajouter le nouvel élément dans la base
        private void AjouterElement(string idTMM, string libelle, string code, string qte)
        {
            //Chaine de connection
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base_Fiche"].ToString();
            MySqlConnection dbConnection = new MySqlConnection(connex);

            dbConnection.Open();

            MySqlCommand cmd = new MySqlCommand();

            cmd.Connection = dbConnection;

            try
            {
                String requete1 = "INSERT INTO listeamm (Num_Appel, IdTMM, Libelle, Code, Categorie, Qte)";
                requete1 += "      VALUES ('" + NumAppel + "', '" + idTMM + "', '" + libelle + "', '" + code + "', '" + Type + "', '" + qte + "')";

                //on execute les requête
                cmd.CommandText = requete1;
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("Impossible d'ajouter les nouveau éléments dans la base " + e.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //fermeture des connexions
                if (dbConnection.State == ConnectionState.Open)
                {
                    dbConnection.Close();
                }

                btnValiderPress = true;
            }
        }

        //pour voir si l'élément de la liste existe déjà dans la base
        private bool ChercheListeBase(string idElement)
        {
            bool retour = false;

            DataTable dt = new DataTable();

            //Chaine de connection
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base_Fiche"].ToString();
            MySqlConnection dbConnection = new MySqlConnection(connex);

            dbConnection.Open();

            MySqlCommand cmd = new MySqlCommand();

            cmd.Connection = dbConnection;

            try
            {
                String requete1 = "SELECT IdTMM FROM listeamm l";
                requete1 += "      WHERE l.Num_Appel = " + NumAppel;

                //on execute les requête
                cmd.CommandText = requete1;
                dt.Load(cmd.ExecuteReader());

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["IdTMM"].ToString() == idElement) 
                    {
                        retour = true;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Impossible de vérfier l'élément dans la base " + e.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //fermeture des connexions
                if (dbConnection.State == ConnectionState.Open)
                {
                    dbConnection.Close();
                }
            }
            return retour;
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            AjoutListe();
        }

        private void listViewListe_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            AjoutListe();
        }

        private void btnRetirer_Click(object sender, EventArgs e)
        {
            SupprListe(false);
        }

        private void listViewAjout_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            SupprListe(true);
        }

        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            if (Modif && !btnValiderPress)
            {
                DialogResult result = MessageBox.Show("Annuler les modifications et quitter ?", "Annuler et quitter ?", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    btnAnnulerPress = true;
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }

        private void AjoutElements_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Modif && !btnAnnulerPress && !btnValiderPress)
            {
                DialogResult result = MessageBox.Show("Annuler les modifications et quitter ?", "Annuler et quitter ?", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void btnValider_Click(object sender, EventArgs e)
        {
            AppliquerModifs();
            this.Close();
        }
    }
}
