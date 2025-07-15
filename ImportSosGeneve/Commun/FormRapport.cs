using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImportSosGeneve.Commun
{
    public partial class FormRapport : Form
    {
        private int NumVisite;
        public int numVisite
        {
            get { return NumVisite; }
            set { NumVisite = value; }
        }
      
        private DataSet dsRapport = new DataSet();
     
        public FormRapport()
        {
            InitializeComponent();
        }

        private void FormRapport_Load(object sender, EventArgs e)
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

            //création des tables dans la dataset
            dsRapport.Tables.Add("Rapport");
            dsRapport.Tables.Add("Corps");

            //on récupère les infos de la fiche
            RecupRapport();

            //Si on a des enregistrements de retournés on rempli les champs
            if (dsRapport.Tables["Rapport"].Rows.Count > 0)
                RempliChampsRapport();
            else
            {
                MessageBox.Show("Il n'y a pas de rapport pour cette consultation", "Rapport", MessageBoxButtons.OK, MessageBoxIcon.Information);                
                this.Close();
            }
        }


        //pour récupérer les infos de la fiche
        private void RecupRapport()
        {
            //Chaine de connection
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);

            dbConnection.Open();

            SqlCommand cmd = new SqlCommand();

            cmd.Connection = dbConnection;

            try
            {
                string SqlStr0 = "SELECT r.NRapport, r.NConsultation, r.DateRapport, r.RapReference, r.RapScribe, r.RapConcerne, r.RapSignature,";
                SqlStr0 += " d.RapDestinataire, d.RapBonjour, d.RapIntroduction, d.RapSalutation ";
                SqlStr0 += " FROM tablerapports r INNER JOIN tablerapportdestine d ON d.NRapport = r.NRapport";
                SqlStr0 += " WHERE r.NConsultation = " + NumVisite;
              
                //on execute la requête
                cmd.CommandText = SqlStr0;               
                dsRapport.Tables["Rapport"].Clear();
                dsRapport.Tables["Rapport"].Load(cmd.ExecuteReader());

                string SqlStr1 = "SELECT r.NRapport, r.NConsultation, c.IdCategorie, c.Valeur";
                SqlStr1 += " FROM tablerapports r INNER JOIN tablerapportcorps c ON c.NRapport = r.NRapport";
                SqlStr1 += " WHERE r.NConsultation = " + NumVisite;
                SqlStr1 += " ORDER BY c.IdCategorie";
               
                //on execute la requête
                cmd.CommandText = SqlStr1;
                dsRapport.Tables["Corps"].Clear();
                dsRapport.Tables["Corps"].Load(cmd.ExecuteReader());
            }
            catch (Exception e)
            {
                MessageBox.Show("Impossible de récupérer le rapport pour cette consultation " + e.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        //On rempli les champs de la Form
        private void RempliChampsRapport()
        {
            //on affiche le numéro d'appel
            lblNumVisite.Text = "N° de consultation " + NumVisite.ToString();
            lNumRapport.Text = "N° de rapport " + dsRapport.Tables["Rapport"].Rows[0]["NRapport"].ToString();
            tboxConcerne.Text = dsRapport.Tables["Rapport"].Rows[0]["RapConcerne"].ToString();
            tBoxDestine.Text = dsRapport.Tables["Rapport"].Rows[0]["RapDestinataire"].ToString();
            lDateR.Text = "Fait le " + dsRapport.Tables["Rapport"].Rows[0]["DateRapport"].ToString()
                            + " par " + dsRapport.Tables["Rapport"].Rows[0]["RapScribe"].ToString();

            tboxContenu.Text = dsRapport.Tables["Rapport"].Rows[0]["RapIntroduction"].ToString();
           
            string TitreRubrique = "";

            //On rempli chaque cathégorie
            for (int i = 0; i < dsRapport.Tables["Corps"].Rows.Count; i++)
            {                
                //On met le titre de la cathégorie
                switch(int.Parse(dsRapport.Tables["Corps"].Rows[i]["IdCategorie"].ToString()))
                {
                    case 1: TitreRubrique = "Anamnese"; break;
                    case 2: TitreRubrique = "Diagnostic"; break;
                    case 3: TitreRubrique = "Diagnostic différentiel"; break;
                    case 4: TitreRubrique = "Examen clinique"; break;
                    case 5: TitreRubrique = "Attitude clinique"; break;
                    case 6: TitreRubrique = "Recherche de l'antigène streptococcique du groupe A"; break;
                    case 7: TitreRubrique = "Glycémie capillaire"; break;
                    case 8: TitreRubrique = "Test de grossesse urinaire"; break;
                    case 9: TitreRubrique = "Laboratoire"; break;
                    case 10: TitreRubrique = "ECG"; break;
                    case 11: TitreRubrique = "Attitude clinique"; break;
                    case 12: TitreRubrique = "Examen clinique"; break;
                    case 13: TitreRubrique = "Allégations et plaintes"; break;
                    case 14: TitreRubrique = "Conclusions"; break;                    
                    case 15: TitreRubrique = "Recherche de l'antigène streptococcique du groupe A"; break;
                    case 16: TitreRubrique = "Glycémie capillaire"; break;
                    case 17: TitreRubrique = "Test de grossesse urinaire"; break;
                    case 18: TitreRubrique = "Laboratoire"; break;
                    case 19: TitreRubrique = "ECG"; break;
                    case 20: TitreRubrique = "Comburtest"; break;
                    case 21: TitreRubrique = "Comburtest"; break;
                    default: TitreRubrique = ""; break;
                }

                //Puis on ajoute la rubrique
                tboxContenu.Text += "\r\n \r\n" + TitreRubrique + " :\r\n";
                tboxContenu.Text += dsRapport.Tables["Corps"].Rows[i]["Valeur"].ToString();
            }

            //Puis les salutations
            tboxContenu.Text += "\r\n \r\n" + dsRapport.Tables["Rapport"].Rows[0]["RapSalutation"].ToString();

            //Le médecin qui a vu le patient
            tboxContenu.Text += "\r\n \r\n" + dsRapport.Tables["Rapport"].Rows[0]["RapSignature"].ToString();

            //On désélectionne le champs (qui l'est par défaut)
            tboxConcerne.Select(0, 0);
        }

      
    }
}
