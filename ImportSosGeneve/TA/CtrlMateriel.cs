using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace ImportSosGeneve
{
    public partial class CtrlMateriel : UserControl
    {
        private int Etat = 0;   //Etat = 0 -> Lecture, 1 -> Ajout, 2 -> Modification
        private int HS = 0;     //Etat du matériel

        //vid -> utilisé dans la requête pour passer les enregistrements sur la page de gauche
        private string vid;

        //on déclare la table Materiel
        DataTable Materiel = new DataTable();

        public CtrlMateriel()
        {
            InitializeComponent();
        }
        
        private void btnAjouter_Click(object sender, EventArgs e)//on appuie sur le bouton ajouter
        {            
            //on Passe en ajout (1)
            Etat = 1;
            
            //on parametre l'état des boutons
            btnAjouter.Enabled = false;
            btnAjouter.Visible = false;            
            btnModifier.Enabled = false;
            btnModifier.Visible = false;
            
            //on active les textbox
            activeControles();
            
            //on les vide
            VideChamps();

            //on désactive la partie recherche
            splitContainer2.Panel1.Enabled = false;
        }

        private void btnValider_Click(object sender, EventArgs e)  //on appuie sur le bouton valider
        {
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base_IP"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);

            dbConnection.Open();

            SqlCommand cmd = dbConnection.CreateCommand();

            SqlTransaction transaction;

            transaction = dbConnection.BeginTransaction();

            cmd.Connection = dbConnection;
            cmd.Transaction = transaction;

            if(Etat == 1)    //si on est en ajout
            {                                                                                            
                try
                {
                    string sqlstr0 = "";

                    //pour ajouter un médaillon quand on ajoute un boitier
                    if(cbLibelle.Text == "LUNA 3G SL" || cbLibelle.Text == "LUNA 3G" || cbLibelle.Text == "LUNA 4")       
                    {
                        sqlstr0 = "INSERT INTO TA_Materiel (VID, Libelle, ContactID, Num_tel_Sim, Type_tarif, Prix_Achat, DateReception)";
                        sqlstr0 += " VALUES";
                        sqlstr0 += " (@VID, @Libelle, @ContactID, @Num_tel_Sim, @Type_tarif, @Prix_Achat, @DateReception),";
                        sqlstr0 += " ('" + tbxVIDm.Text + "', 'Médaillon radio M4', @ContactID, null, 'M4', 0.00, @DateReception)";
                    }
                    else
                    {
                        sqlstr0 = "INSERT INTO TA_Materiel (TA_Materiel.VID, TA_Materiel.Libelle, TA_Materiel.ContactID, TA_Materiel.Num_tel_Sim, TA_Materiel.Type_tarif, TA_Materiel.Prix_Achat, TA_Materiel.DateReception)";
                        sqlstr0 += " VALUES";
                        sqlstr0 += " (@VID, @Libelle, @ContactID, @Num_tel_Sim, @Type_tarif, @Prix_Achat, @DateReception)";                  
                    }
                        
                    cmd.CommandText = sqlstr0;

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("VID", tbxVID.Text);
                    cmd.Parameters.AddWithValue("Libelle", cbLibelle.Text);
                    
                    if (tbxContactID.Text != "")
                        cmd.Parameters.AddWithValue("ContactID", "0003" + tbxContactID.Text);                       
                    else
                        cmd.Parameters.AddWithValue("ContactID", "00000000");                       

                    cmd.Parameters.AddWithValue("Num_tel_Sim", tbxTel.Text);
                    cmd.Parameters.AddWithValue("Type_tarif", cbTypeTarif.Text);
                    cmd.Parameters.AddWithValue("Prix_Achat", decimal.Parse(tbxPrixAchat.Text));
                    cmd.Parameters.AddWithValue("DateReception", DateTime.Now.ToString());                                             
                      
                    cmd.ExecuteNonQuery();

                    transaction.Commit();
                    MessageBox.Show("Matériel enregistrés avec succès");
                 }
                 catch (Exception ex)
                 {
                    Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                    Console.WriteLine("  Message: {0}", ex.Message);

                    transaction.Rollback();
                 }
            } //Fin d'on est en ajout
            else if(Etat == 2)   //Sinon on est en modif
            {
                try
                {                                                           
                    string sqlstr0 = "UPDATE TA_Materiel ";
                    sqlstr0 += " SET Libelle = @Libelle, ContactID = @ContactID, Num_tel_Sim = @Num_tel_Sim, ";                                       
                    sqlstr0 += " Type_tarif = @Type_tarif, Prix_Achat = @Prix_Achat,";
                    sqlstr0 += " DateHS = case when DateHS is null then @DateJ else DateHS end";
                    sqlstr0 += " WHERE VID = @VID";
                                                              
                    cmd.CommandText = sqlstr0;

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("VID", tbxVID.Text);
                    cmd.Parameters.AddWithValue("Libelle", cbLibelle.Text);
                    cmd.Parameters.AddWithValue("ContactID", tbxContactID.Text);                       
                    cmd.Parameters.AddWithValue("Num_tel_Sim", tbxTel.Text);
                    cmd.Parameters.AddWithValue("Type_tarif", cbTypeTarif.Text);
                    cmd.Parameters.AddWithValue("Prix_Achat", decimal.Parse(tbxPrixAchat.Text));
                    
                    if(HS == 1)                    
                        cmd.Parameters.AddWithValue("DateJ", DateTime.Now);
                    else
                        cmd.Parameters.AddWithValue("DateJ", DBNull.Value);
                                                           
                    cmd.ExecuteNonQuery();

                    transaction.Commit();
                    MessageBox.Show("Modifications enregistrés avec succès");
                 }
                 catch (Exception ex)
                 {
                    Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                    Console.WriteLine("  Message: {0}", ex.Message);

                    transaction.Rollback();
                 }
            }  //Fin d'on est en modif
                          
            //on change Etat pour le mettre en Lecture (0)
            Etat = 0;

            //On remet l'etat du materiel à 0 => Bon
            HS = 0;

            //on modifie l'état des boutons
            btnAjouter.Enabled = true;
            btnAjouter.Visible = true;
                
            btnModifier.Enabled = true;
            btnModifier.Visible = true;

            //on désactive et on vide les textbox
            VideChamps();
            desactiveControles();

            //pour la recherche
            tbxRecherche.Text = "";

            //on vide le listview
            listView1.Items.Clear();    
                       
            //on réactive la partie recherche
            splitContainer2.Panel1.Enabled = true;
            rBVID.Checked = true;

            //on désactive et on vide les combobox
            cbTypeTarif.Enabled = false;
            cbLibelle.Enabled = false;
            cbTypeTarif.Text = "";
            cbLibelle.Text = "";

            if (dbConnection.State == ConnectionState.Open)
                dbConnection.Close();            

            //On rafraichi le stock
            Stock();
        }

        private void desactiveControles()
        {
            //on désactive et on vide les textbox
            tbxContactID.Enabled = false;
            tbxVID.Enabled = false;
            tbxTel.Enabled = false;
            cbTypeTarif.Enabled = false;
            tbxPrixAchat.Enabled = false;
            cbxDateHS.Enabled = false;
            cbLibelle.Enabled = false;
        }

        private void activeControles()
        {
            //on désactive et on vide les textbox
            tbxContactID.Enabled = true;
            tbxVID.Enabled = true;
            tbxTel.Enabled = true;
            cbTypeTarif.Enabled = true;
            tbxPrixAchat.Enabled = true;
            cbxDateHS.Enabled = true;
            cbLibelle.Enabled = true;
        }

        private void VideChamps()
        {
            tbxContactID.Text = "";
            tbxVID.Text = "";
            tbxTel.Text = "";
            tbxPrixAchat.Text = "";
            tbxVIDm.Text = "";
            cbxDateHS.Checked = false;
        }

     
        private void btnAnnuler_Click(object sender, EventArgs e)//on appuie sur le bouton annuler
        {            
            if (Etat == 2) //si on était en modification
            {                                                
                //on change l'état des boutons
                btnAjouter.Enabled = true;
                btnAjouter.Visible = true;                
                btnModifier.Enabled = true;
                btnModifier.Visible = true;
                
                //on désactive les textbox/combobox/checkbox
                desactiveControles();

                //on réactive la partie recherche
                splitContainer2.Panel1.Enabled = true;
                rBVID.Checked = true;

                //Et on reselectionne l'enreg que l'on voulait modifer
                int Selection = listView1.SelectedIndices[0];

                if (listView1.Items.Count != 0)                    
                    listView1.Items[Selection].Selected = true; 
            }
            else if (Etat == 1)         //si on était en ajout
            {
                //on change l'état des boutons
                btnAjouter.Enabled = true;
                btnAjouter.Visible = true;                
                btnModifier.Enabled = true;
                btnModifier.Visible = true;

                VideChamps();
                desactiveControles();
                                
                //on réactive la partie recherche
                splitContainer2.Panel1.Enabled = true;
                rBVID.Checked = true;
            }

            Etat = 0;   //On met l'état en lecture     
            
            //On rafraichi le stock
            Stock();
        }

        private void btnRecherche_Click(object sender, EventArgs e)//pour rechercher des appereils
        {
            if (tbxRecherche.Text != "")
            {
                //on vide le listview
                listView1.Items.Clear();                

                //on se connecte à la base
                string connex = ConfigurationManager.ConnectionStrings["Connection_Base_IP"].ToString();
                SqlConnection dbConnection = new SqlConnection(connex);
                                      
                try
                {
                    dbConnection.Open();

                    SqlCommand cmd = dbConnection.CreateCommand();
                    cmd.Connection = dbConnection;

                    string sqlstr0 = "SELECT Libelle, VID, ContactID";
                    sqlstr0 += " FROM TA_Materiel";

                    //En fonction de ce qui est coché
                    if (rBVID.Checked)                                   
                        sqlstr0 += " WHERE VID LIKE @recherche";
                    else if (rBLibelle.Checked)
                        sqlstr0 += " WHERE Libelle LIKE '%' + @recherche";
                    else if (rBContactID.Checked)
                        sqlstr0 += " WHERE ContactID LIKE '%' + @recherche";

                    cmd.CommandText = sqlstr0;

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("recherche", tbxRecherche.Text + "%");

                    DataTable listMat = new DataTable();
                    listMat.Load(cmd.ExecuteReader());

                    for (int i = 0; i < listMat.Rows.Count; i++)//on charge les resultat dans le listview
                    {
                        //on met les resultat dans les colonnes correspondantes dans le listview
                        ListViewItem item1 = new ListViewItem(listMat.Rows[i]["Libelle"].ToString());
                        item1.SubItems.Add(listMat.Rows[i]["VID"].ToString());
                        item1.SubItems.Add(listMat.Rows[i]["ContactID"].ToString());

                        listView1.Items.Add(item1);
                    }

                    //si aucun résultat n'est trouvé on affiche un msg
                    if (listMat.Rows.Count == 0)
                    {
                        listView1.Items.Add("Aucun Résultat");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(" Erreur : " + ex.Message);
                }

                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.Items.Count > 0)
            {
                //On récupère le VID qui a été selectionné dans le listview
                if (listView1.SelectedItems.Count > 0)//si un item est selectionné
                {
                    //on met l'item de la colonne 2 du listview qui a été selectionné dans vid
                    vid = listView1.SelectedItems[0].SubItems[1].Text;
                }

                string connex = ConfigurationManager.ConnectionStrings["Connection_Base_IP"].ToString();
                SqlConnection dbConnection = new SqlConnection(connex);
                try
                {
                    SqlCommand cmd = dbConnection.CreateCommand();
                    cmd.Connection = dbConnection;

                    string sqlstr0 = "SELECT ContactID, VID, Num_tel_Sim, Type_tarif, Prix_Achat, Libelle, DateHS";
                    sqlstr0 += " FROM TA_Materiel";
                    sqlstr0 += " WHERE VID = @VID";

                    cmd.CommandText = sqlstr0;

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("VID", vid);

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);

                    Materiel.Clear();

                    sda.Fill(Materiel);

                    tbxContactID.Text = Materiel.Rows[0]["ContactID"].ToString();
                    tbxVID.Text = Materiel.Rows[0]["VID"].ToString();
                    tbxTel.Text = Materiel.Rows[0]["Num_tel_Sim"].ToString();
                    cbTypeTarif.Text = Materiel.Rows[0]["Type_tarif"].ToString();
                    tbxPrixAchat.Text = Materiel.Rows[0]["Prix_Achat"].ToString();
                    cbLibelle.Text = Materiel.Rows[0]["Libelle"].ToString();

                    //si datehs est nul on ne coche pas le checkbox
                    if (Materiel.Rows[0]["DateHS"].ToString() == "")
                    {
                        cbxDateHS.Checked = false;
                    }
                    else //sinon on le coche
                    {
                        cbxDateHS.Checked = true;
                    }

                    tbxVIDm.Enabled = false;
                    tbxTel.Enabled = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(" Erreur : " + ex.Message);
                }            
            }
        }

        private void btnModifier_Click(object sender, EventArgs e)//quand on appuie sur le bouton modifier
        {
            if (tbxVID.Text != "")
            {
                //on change Etat pour le mettre en Modification (2)
                Etat = 2;

                //on change l'état des boutons
                btnAjouter.Enabled = false;
                btnAjouter.Visible = false;
                btnModifier.Enabled = false;
                btnModifier.Visible = false;

                activeControles();

                //on désactive la partie recherche
                splitContainer2.Panel1.Enabled = false;

                if (cbLibelle.Text == "LUNA 3G SL" || cbLibelle.Text == "LUNA 3G" || cbLibelle.Text == "LUNA 4")
                {
                    tbxVIDm.Enabled = true;
                    tbxTel.Enabled = true;
                }
            }
        }

        private void cbLibelle_DropDown(object sender, EventArgs e)//quand on déroule le libellé
        {
            //on se connecte à la base
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base_IP"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);
            
            try
            {
                dbConnection.Open();

                SqlCommand cmd = dbConnection.CreateCommand();
                cmd.Connection = dbConnection;

                string sqlstr0 = "SELECT DISTINCT(Libelle) FROM TA_Materiel";
                
                cmd.CommandText = sqlstr0;

                DataTable libelle = new DataTable();
                libelle.Load(cmd.ExecuteReader());

                cbLibelle.Items.Clear();
                
                for (int i = 0; i < libelle.Rows.Count; i++)//on charge les resultat dans le combobox
                {                    
                    cbLibelle.Items.Add(libelle.Rows[i]["Libelle"].ToString());
                }                
            }
            catch (Exception ex)
            {
                Console.WriteLine(" Erreur : " + ex.Message);
            }

            if (dbConnection.State == ConnectionState.Open)
                dbConnection.Close();            
        }

        private void cbTypeTarif_DropDown(object sender, EventArgs e)
        {
            //on se connecte à la base
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base_IP"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);
            try
            {
                dbConnection.Open();

                SqlCommand cmd = dbConnection.CreateCommand();
                cmd.Connection = dbConnection;

                string sqlstr0 = "SELECT id FROM ta_tarif WHERE id not in ('A', 'M', 'S', 'T', 'MeM', 'MeT', 'MeS', 'MeA')";
        
                cmd.CommandText = sqlstr0;

                DataTable Tarif = new DataTable();
                Tarif.Load(cmd.ExecuteReader());

                cbTypeTarif.Items.Clear();

                for (int i = 0; i < Tarif.Rows.Count; i++)//on charge les resultat dans le combobox
                {
                    cbTypeTarif.Items.Add(Tarif.Rows[i]["id"].ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(" Erreur : " + ex.Message);
            }

            dbConnection.Close();
        }

        private void cbLibelle_TextChanged(object sender, EventArgs e)
        {
            //Si on ajoute une box, il faut lui attribuer systématiquement un médaillon
            if (cbLibelle.Text == "LUNA 3G SL" || cbLibelle.Text == "LUNA 3G" || cbLibelle.Text == "LUNA 4")
            {
                //on débloque les textbox pour ajouter le VID du medaillon et le numero de tel
                tbxVIDm.Enabled = true;
                tbxTel.Enabled = true;
            }
            else
            {
                //sinon les textbox pour ajouter le VID du medaillon et le numero de tel son désactivé
                tbxVIDm.Enabled = false;
                tbxTel.Enabled = false;
            }            
        }

        private void tbxRecherche_KeyPress(object sender, KeyPressEventArgs e)
        {
            //quand on appuie sur entrer on fait l'action du bouton rechercher
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;   // j'ai mis cette ligne sinon sa fait "Ding" quand on appuie sur entrer
                btnRecherche_Click(sender, e);  //on fait l'action du bouton rechercher
            }
        }

       
        private void UserControl1_Load(object sender, EventArgs e)
        {
            //On initialise les contrôles
            VideChamps();
            desactiveControles();

            //on active la partie recherche
            splitContainer2.Panel1.Enabled = true;            
            rBContactID.Checked = true;

            //On affiche le stock
            Stock();
        }


        private void Stock()
        {
            //On affiche le stock disponible
            //on se connecte à la base
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base_IP"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);
            try
            {
                dbConnection.Open();

                SqlCommand cmd = dbConnection.CreateCommand();
                cmd.Connection = dbConnection;

                string sqlstr0 = @"SELECT count(*) as nb, Libelle FROM ta_Materiel
                                   WHERE (IdAbonnement is null OR IdAbonnement = '')
                                   AND DateHS is Null
                                   GROUP BY Libelle
                                   order by nb";

                cmd.CommandText = sqlstr0;

                DataTable Stock = new DataTable();
                Stock.Load(cmd.ExecuteReader());

                LStock.Text = "";  

                for (int i = 0; i < Stock.Rows.Count; i++)//on charge les resultat dans LStock
                {
                    LStock.Text += Stock.Rows[i][0].ToString() + "    " + Stock.Rows[i][1].ToString() + "\r\n\r\n";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(" Erreur : " + ex.Message);
            }

            dbConnection.Close();
        }

        private void cbxDateHS_CheckedChanged(object sender, EventArgs e)
        {
            //Si on est en Modification (2)
            if (Etat == 2)
            {
                if (cbxDateHS.Checked)
                {
                    //on met une date la date du jour dans HS
                    HS = 1;
                }
            }
        }
     
      
    }
}
