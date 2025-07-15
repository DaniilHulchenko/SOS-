using MySql.Data.MySqlClient;
using SosMedecins.SmartRapport.DAL;
using SosMedecins.SmartRapport.EtatsCrystal;
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
    public partial class FImpFicheConsult : Form
    {
        EtatFicheConsult EtatFicheConsult1 = new EtatFicheConsult();  //On déclare l'Etat principal (il y a 1 sous rapport lié avec L_date)
        dstFicheConsult dtsFicheConsult = new dstFicheConsult();        //On déclare le DataSet de l'etat
        public int Num_Appel = -1;
        public string NomMedecin = "";

        public FImpFicheConsult()
        {
            InitializeComponent();
        }

        private void FImpFicheConsult_FormClosing(object sender, FormClosingEventArgs e)
        {
            EtatFicheConsult1.Dispose();
        }

        private void FImpFicheConsult_Load(object sender, EventArgs e)
        {
            //*****Gestion des écrans ******
            Screen[] screens = Screen.AllScreens;           //On recupère tout les écrans
            int NbEcran = Screen.AllScreens.Length;         //on récupère le nombre d'ecran
            int EcranSecondaire = 0;

            if (NbEcran > 1)
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
            //********************Fin gestion des écrans***************

            //Chaine de connection
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base_Fiche"].ToString();
            MySqlConnection dbConnection = new MySqlConnection(connex);

            dbConnection.Open();

            MySqlCommand cmd = new MySqlCommand();

            cmd.Connection = dbConnection;

            //On déclare ici les requetes (à cause de la transaction)
            string sqlstr0, sqlstr1, sqlstr2, sqlstr3, sqlstr4 = "";

            try
            {                
                //on charge le dataSet                                          
                sqlstr0 = "SELECT * FROM visite";             
                sqlstr0 += " WHERE Num_Appel = '" + Num_Appel + "'";

                sqlstr1 = "SELECT * FROM structconsult ";
                sqlstr1 += " WHERE Num_Appel = '" + Num_Appel + "'";

                sqlstr2 = "SELECT * FROM correspondance ";
                sqlstr2 += " WHERE Num_Appel = '" + Num_Appel + "'";

                sqlstr3 = "SELECT * FROM assurances ";
                sqlstr3 += " WHERE Num_Appel = '" + Num_Appel + "'";

                sqlstr4 = "SELECT * FROM listeamm ";
                sqlstr4 += " WHERE Num_Appel = '" + Num_Appel + "'";

                cmd.CommandText = sqlstr0; dtsFicheConsult.Tables["visite"].Load(cmd.ExecuteReader());
                cmd.CommandText = sqlstr1; dtsFicheConsult.Tables["structconsult"].Load(cmd.ExecuteReader());
                cmd.CommandText = sqlstr2; dtsFicheConsult.Tables["correspondance"].Load(cmd.ExecuteReader());
                cmd.CommandText = sqlstr3; dtsFicheConsult.Tables["assurances"].Load(cmd.ExecuteReader());
                cmd.CommandText = sqlstr4; dtsFicheConsult.Tables["listeamm"].Load(cmd.ExecuteReader());

                //On affiche le premier enregistrement
                if (dtsFicheConsult.Tables["visite"].Rows.Count > 0)
                {
                    //On parametre l'imression                                      
                    EtatFicheConsult1.SetDataSource(dtsFicheConsult);
                    EtatFicheConsult1.DataDefinition.FormulaFields["NomMedecin"].Text = "'" + NomMedecin + "'";

                    //On alloue le DataSet au sousRapport
                    // GrilleHoraire1.Subreports[0].SetDataSource(DSImpGrille.Tables["GrilleHebergement"]);                   
                    crystalReportViewer1.ReportSource = EtatFicheConsult1;                  

                    crystalReportViewer1.Show();
                }
            }
            catch (Exception a)
            {
                Console.WriteLine("Erreur : " + a.Message);
            }
            finally
            {
                // Fermeture de la connexion
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
        }
    }
}
