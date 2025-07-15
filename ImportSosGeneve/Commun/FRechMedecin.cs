using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace ImportSosGeneve.Commun
{
    public partial class FRechMedecin : Form
    {
        //Pour retourner le résultat de la recherche
        private string CodeMedecin;
        public string codeMedecin
        {
            get { return CodeMedecin; }
            set { CodeMedecin = value; }
        }

        private string NomMedecin;
        public string nomMedecin
        {
            get { return NomMedecin; }
            set { NomMedecin = value; }
        }

        private DataTable dtMedecin = new DataTable();

        public FRechMedecin()
        {
            InitializeComponent();

            //Pour la liste des médecins "Actifs"
            listView1.Columns.Add("Code Médecin", 1);         //Colonne invisible
            listView1.Columns.Add("Médecin", 200);
            listView1.View = View.Details;    //Pour afficher les subItems  

            CodeMedecin = "-1";
            NomMedecin = "";
        }

        private void FRechMedecin_Load(object sender, EventArgs e)
        {
            //On vide la liste pour la rafraichir                
            listView1.BeginUpdate();
            listView1.Items.Clear();

            dtMedecin = ChargeListeMedecins();

            for (int i = 0; i < dtMedecin.Rows.Count; i++)
            {
                ListViewItem item = new ListViewItem(dtMedecin.Rows[i]["CodeIntervenant"].ToString());
                item.SubItems.Add(dtMedecin.Rows[i]["Nom"].ToString());
                listView1.Items.Add(item);
            }

            listView1.EndUpdate();  //Rafraichi le controle        
        }

        private void tBMedecin_TextChanged(object sender, EventArgs e)
        {
            //on lance une recherche avec ce qui a été entré
            string SqlSelect = "Nom like '" + tBMedecin.Text.Replace("'", "''") + "%'";
            string Trie = "Nom";

            if (dtMedecin.Select(SqlSelect, Trie).Any())    //Si on a quelque chose
            {
                DataTable dtMedTrie = dtMedecin.Select(SqlSelect, Trie).CopyToDataTable();

                //On vide la liste pour la rafraichir                
                listView1.BeginUpdate();
                listView1.Items.Clear();

                for (int i = 0; i < dtMedTrie.Rows.Count; i++)
                {
                    ListViewItem item = new ListViewItem(dtMedTrie.Rows[i]["CodeIntervenant"].ToString());
                    item.SubItems.Add(dtMedTrie.Rows[i]["Nom"].ToString());
                    listView1.Items.Add(item);
                }

                listView1.EndUpdate();  //Rafraichi le controle  
            }
        }

        //On sélectionne la ligne puis on ferme la form
        private void listView1_DoubleClick(object sender, EventArgs e)
        {            
            //On peuple les variables       
            CodeMedecin = listView1.SelectedItems[0].Text;
            NomMedecin = listView1.SelectedItems[0].SubItems[1].Text;

            //On détermine la réponse de retour
            DialogResult = DialogResult.OK;

            //Puis on ferme la form
            this.Close();
        }

        private void tBMedecin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //On selectionne la première ligne de la liste (s'il y a qqchose), puis on ferme
                if (listView1.Items.Count > 0)
                {
                    //On peuple les variables       
                    CodeMedecin = listView1.Items[0].Text;
                    NomMedecin = listView1.Items[0].SubItems[1].Text;

                    //On détermine la réponse de retour
                    DialogResult = DialogResult.OK;
                }
                else
                    DialogResult = DialogResult.Cancel;

                //Puis on ferme la form
                this.Close();
            }
        }

        private void bExit_Click(object sender, EventArgs e)
        {
            //On ferme en envoyant cancel
            DialogResult = DialogResult.Cancel;

            //Puis on ferme la form
            this.Close();
        }


        //Charge la liste des médecins selon le critère (tous, seulement les actifs, non garde, en garde)
        private static DataTable ChargeListeMedecins()
        {
            DataTable dtMedecin = new DataTable();

            //On charge la liste des Smartphones
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);

            dbConnection.Open();
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = dbConnection;          

            try
            {
                string sqlstr0 = "SELECT Nom, CodeIntervenant FROM tablemedecin WHERE CodeIntervenant not in (1400, 2554, 2570, 2697) AND Desactive = 0 ORDER BY Nom";
                             
                cmd.CommandText = sqlstr0;

                dtMedecin.Load(cmd.ExecuteReader());    //on execute               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors du chargement des médecins :" + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //fermeture des connexions
                if (dbConnection.State == ConnectionState.Open)
                {
                    dbConnection.Close();
                }
            }

            return dtMedecin;
        }



    }
}
