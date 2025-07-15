using MySql.Data.MySqlClient;
using SosMedecins.SmartRapport.GestionApplication;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace ImportSosGeneve.Commun
{
    public partial class FRechFicheConsult : Form
    {       
        private DataTable dtFichesVisite = new DataTable();
        private string DroitsUtilisateur = "Secretaire";

        public FRechFicheConsult()
        {
            InitializeComponent();

            //On fixe la taille du panel1 (ou il y a les champs de recherche et les boutons) 
            splitContainer1.FixedPanel = FixedPanel.Panel1;

            //Def des colonnes du Listeview                     
            listView1.Columns.Add("N° de Visite", 85);
            listView1.Columns.Add("Date Visite", 80);            
            listView1.Columns.Add("Nom/Prénom", 250);            
            listView1.Columns.Add("Medecin", 200);
            listView1.Columns.Add("Etat", 120);
            listView1.Columns.Add("Verrouillée par", 120);

            listView1.View = View.Details;    //Pour afficher les subItems   
           
            //En fonction deys droits
            if (VariablesApplicatives.Utilisateurs.Droits == VariablesApplicatives.Utilisateurs.CodeDroits.Secretaire)                           
                DroitsUtilisateur = "Secretaire";            
            else if (VariablesApplicatives.Utilisateurs.Droits == VariablesApplicatives.Utilisateurs.CodeDroits.Admin)
                 DroitsUtilisateur = "Admin";
            else
                 DroitsUtilisateur = "Facturation";            
        }

        private void FRechFicheConsult_Load(object sender, EventArgs e)
        {
            dTimePDeb.Value = DateTime.Now.AddMonths(-1);
            dTimePFin.Value = DateTime.Now;

            //*****Gestion des écrans ******
            Screen[] screens = Screen.AllScreens;           //On recupère tout les écrans
            int NbEcran = Screen.AllScreens.Length;         //on récupère le nombre d'ecran
            int EcranSecondaire = 0;

            if(NbEcran > 1)
            {
                //on regarde sur quel écran est la form principale
                var FormfrmGeneral = Application.OpenForms["frmGeneral"];
                if (FormfrmGeneral != null)
                {
                    for (int i = 0; i < NbEcran; i++)
                    {
                        Point formTopLeft = new Point(FormfrmGeneral.Left, FormfrmGeneral.Top);   //On récupère le point haut à gauche

                        if (!screens[i].WorkingArea.Contains(formTopLeft))    //Pour pouvoir localiser l'écran
                        {
                            EcranSecondaire = i;          //Si ce n'est PAS celui là....c'est qu'il est libre!
                        }
                       // else
                         //   MessageBox.Show("Form principale sur l'écran " + screens[i].Primary + screens[i].DeviceName);                       
                    }
                }

                //on met la position de démarrage en manuel
                this.StartPosition = FormStartPosition.Manual;

                //On affiche la forme sur le 2eme écran (celui ou n'est pas la form principale)               
                this.Location = Screen.AllScreens[EcranSecondaire].WorkingArea.Location;
            }
            else  //Pas de 2eme écran
            {
                //on met la position de démarrage en manuel
                this.StartPosition = FormStartPosition.Manual;

                this.Location = new Point(0, 0);
            }           
        }

        private void tBoxMedecin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                //On affiche la boite de recherche des médecins
                FRechMedecin fRechMedecin = new FRechMedecin();

                //On récupères les valeurs
                if (fRechMedecin.ShowDialog() == DialogResult.OK)
                {
                    lCodeMedecin.Text = fRechMedecin.codeMedecin;
                    tBoxMedecin.Text = fRechMedecin.nomMedecin;
                }
                else
                {
                    lCodeMedecin.Text = "-1";
                    tBoxMedecin.Text = "";
                }

                fRechMedecin.Dispose();
            }
        }

        private void bRecherche_Click(object sender, EventArgs e)
        {
            //En fonction de ce que l'on a rempli, on lance une recherche
            RechercheFicheSelonCriteres();

            //Puis on affecte les champs                    
            //On vide la liste pour la rafraichir                
            listView1.BeginUpdate();
            listView1.Items.Clear();

            for (int i = 0; i < dtFichesVisite.Rows.Count; i++)
            {
                ListViewItem item = new ListViewItem(dtFichesVisite.Rows[i]["Num_Appel"].ToString());               
                item.SubItems.Add(convertDateMaria(dtFichesVisite.Rows[i]["DateC"].ToString(), "Texte").Remove(10, 9));               
                item.SubItems.Add(dtFichesVisite.Rows[i]["NomPatient"].ToString() + " " + dtFichesVisite.Rows[i]["PrenomPatient"].ToString());

                //Pour le nom du médecin
                item.SubItems.Add(RetourNomMed(dtFichesVisite.Rows[i]["CodeMedecin"].ToString()));

                if (DroitsUtilisateur == "Secretaire")
                {
                    switch (dtFichesVisite.Rows[i]["Etat_Secretariat"].ToString())
                    {
                        case "0": item.SubItems.Add("A traiter");break;                       
                        case "1": item.SubItems.Add("En attente"); break;
                        case "2": item.SubItems.Add("Terminée"); break;
                       default: item.SubItems.Add("A traiter"); break;
                    }
                }
                else if (DroitsUtilisateur == "Facturation")
                {
                    switch (dtFichesVisite.Rows[i]["Etat_Facturation"].ToString())
                    {
                        case "0": item.SubItems.Add("A traiter"); break;                       
                        case "1": item.SubItems.Add("En attente"); break;
                        case "2": item.SubItems.Add("Terminée"); break;
                        default: item.SubItems.Add("A traiter"); break;
                    }
                }
                else if (DroitsUtilisateur == "Admin")
                {
                    switch (dtFichesVisite.Rows[i]["Etat_Secretariat"].ToString())
                    {
                        case "0": item.SubItems.Add("A traiter"); break;
                        case "1": item.SubItems.Add("En attente"); break;
                        case "2": item.SubItems.Add("Terminée"); break;
                        default: item.SubItems.Add("A traiter"); break;
                    }
                }

                item.SubItems.Add(dtFichesVisite.Rows[i]["Verrou"].ToString());     //Indique le nom de la personne qui a vérouillé la fiche

                listView1.Items.Add(item);
            }

            listView1.EndUpdate();  //Rafraichi le controle  
        }


        //Recherche selon les critères rempli
        private void RechercheFicheSelonCriteres()
        {
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base_Fiche"].ToString();
            MySqlConnection dbConnection = new MySqlConnection(connex);

            dbConnection.Open();
            MySqlCommand cmd = new MySqlCommand();

            cmd.Connection = dbConnection;

            string sqlstr0 = "SELECT * FROM visite ";                       
            sqlstr0 += " WHERE Date(DateC) >= Date('" + convertDateMaria(dTimePDeb.Value.ToString(), "MariaDb") + "')";
            sqlstr0 += " AND Date(DateC) <= Date('" + convertDateMaria(dTimePFin.Value.ToString(), "MariaDb") + "')";

            if (tBNumConsult.Text != "")
                sqlstr0 += " AND Num_Appel = '" + tBNumConsult.Text + "'";

            if (lCodeMedecin.Text != "-1")
                sqlstr0 += " AND CodeMedecin = '" + lCodeMedecin.Text + "'";

            //Pour les secrétaires
            if (DroitsUtilisateur == "Secretaire")
            {
                if (rB0.Checked)
                    sqlstr0 += " AND Etat_Secretariat = 0";
                else if (rB2.Checked)
                    sqlstr0 += " AND Etat_Secretariat = 1";
                else if (rB3.Checked)
                    sqlstr0 += " AND Etat_Secretariat = 2";
            }
            else if (DroitsUtilisateur == "Facturation")    //Pour les autres (4)
            {
                //terminé pour le secrétarait médical
                if (rB0.Checked)
                    sqlstr0 += " AND Etat_Secretariat = 2 AND Etat_Facturation = 0";                
                else if (rB2.Checked)
                    sqlstr0 += " AND Etat_Secretariat = 2 AND Etat_Facturation = 1";
                else if (rB3.Checked)
                    sqlstr0 += " AND Etat_Secretariat = 2 AND Etat_Facturation = 2";
            }
            if (DroitsUtilisateur == "Admin")
            {
                if (rB0.Checked)
                    sqlstr0 += " AND Etat_Secretariat = 0";
                else if (rB2.Checked)
                    sqlstr0 += " AND Etat_Secretariat = 1";
                else if (rB3.Checked)
                    sqlstr0 += " AND Etat_Secretariat = 2";
            }

            sqlstr0 += " ORDER BY Date(DateC) DESC";

            try
            {
                cmd.CommandText = sqlstr0;

                dtFichesVisite.Rows.Clear();
                dtFichesVisite.Load(cmd.ExecuteReader());    //on execute              
            }
            catch (Exception e)
            {
                MessageBox.Show("Erreur lors de la recherche :" + e.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        //Fonction qui retourne le nom du médecin à partir de son CodeMedecin
        private string RetourNomMed(string CodeMedecin)
        {
            string Medecin = "Inconnu";

            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);

            dbConnection.Open();
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = dbConnection;

            try
            {
                string sqlstr0 = "SELECT Nom FROM tablemedecin ";
                sqlstr0 += " WHERE CodeIntervenant = '" + CodeMedecin + "'";
                                  
                cmd.CommandText = sqlstr0;

                DataTable dtMedecin = new DataTable();
                dtMedecin.Load(cmd.ExecuteReader());    //on execute    
                
                if (dtMedecin.Rows.Count > 0)                
                    Medecin = dtMedecin.Rows[0]["Nom"].ToString();                 
            }
            catch (Exception e)
            {
                MessageBox.Show("Erreur lors de la recherche :" + e.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //fermeture des connexions
                if (dbConnection.State == ConnectionState.Open)
                {
                    dbConnection.Close();
                }
            }

            return Medecin;
        }

        private void bFermer_Click(object sender, EventArgs e)
        {
            //On ferme la form
            this.Close();
        }

        //Fonction qui converti la date vers MariaDB (yyyy-MM-dd HH:mm:ss) et vis versa
        public static string convertDateMaria(string DateAconvertir, string Sens)
        {
            string Retour = "";

            if (DateAconvertir != "")
            {
                if (Sens == "MariaDb")
                {
                    try
                    {
                        DateTime MyDate = DateTime.Parse(DateAconvertir);
                        Retour = MyDate.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    catch (Exception e)
                    {
                        System.Windows.Forms.MessageBox.Show("Erreur lors de la convertion de la date " + DateAconvertir + " :" + e.Message, "Erreur", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    }
                }
                else if (Sens == "Texte")
                {
                    try
                    {
                        DateTime MyDate = DateTime.Parse(DateAconvertir);
                        MyDate.ToString("dd.MM.yyyy HH:mm:ss");
                        Retour = MyDate.ToString("dd.MM.yyyy HH:mm:ss");
                    }
                    catch (Exception e)
                    {
                        System.Windows.Forms.MessageBox.Show("Erreur lors de la convertion de la date " + DateAconvertir + " :" + e.Message, "Erreur", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    }
                }
            }

            return Retour;
        }

        //On trie quand on clique sur la colonne
        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            this.listView1.ListViewItemSorter = new ListViewTriRechFiche(e.Column);
        }

        //On ramene la fiche selectionnée
        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            //On affiche la Fiche si elle n'est pas déjà ouverte
            string Num_Appel = listView1.SelectedItems[0].Text;

            //on regarde si on a des Fiches consult ouverte et si oui, s'il y en a une avec ce n° de consult                     
            int Existe = 0;

            foreach (Form form in Application.OpenForms)      //Liste les formes ouvertes
            {
                if (form.Name == "FicheConsult")  //S'il y en a une qui s'appelle FicheConsult
                {
                    //On regarde si elle a un control nommé lblNumVisite
                    Control[] lbl = form.Controls.Find("lblNumVisite", true);

                    string NumVisite = "";

                    if (lbl.Length > 0)   //Si on en a un (Et il doit y en avoir un!)
                    {
                        NumVisite = lbl[0].Text;   //Récup du text du label qui a le n° de visite                    
                    }                   

                    //On compare pour voir si ça correspond à une déjà ouverte
                    if (NumVisite == Num_Appel.ToString())
                    {
                        Existe = 1;
                        //Elle est déjà ouverte, on la ramère au premier plan et on lui donne le focus
                        form.Activate();
                    }
                }
            }

            //Si la fiche n'existe pas déjà, on la créer
            if (Existe == 0)
            {
                FicheConsult ficheConsult = new FicheConsult();
                ficheConsult.numVisite = int.Parse(Num_Appel);
                ficheConsult.Show();
            }           
        }
    }

    //Pour implémenter le tri en cliquant sur le titre des colonnes
    class ListViewTriRechFiche : IComparer
    {
        private int col;
        public ListViewTriRechFiche()
        {
            col = 0;
        }
        public ListViewTriRechFiche(int column)
        {
            col = column;
        }
        public int Compare(object x, object y)
        {
            return String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
        }
    }



}



//A faire:
