//using FicheConsultation.Properties;
using ImportSosGeneve.Properties;
using MySql.Data.MySqlClient;
using SosMedecins.SmartRapport.DAL;
using SosMedecins.SmartRapport.GestionApplication;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace ImportSosGeneve.Commun
{
    public partial class FicheConsult : Form
    {
        private int NumVisite;
        public int numVisite
        {
            get { return NumVisite; }
            set { NumVisite = value; }
        }

       // public int NumAppel = 0;
        public string DroitsUtilisateur = "Secretaire";

        private DataSet dsFiche = new DataSet();
       
        //pour savoir si la fiche est verrouillé (lecture seule)
        private int LectureSeule = 0;
        private string Verrou = "";
       

        public FicheConsult()
        {
            InitializeComponent();

            //création des colonnes pour les listView
            listViewActesTech.Columns.Add("Id", 1);     //colonne invisible
            listViewActesTech.Columns.Add("NumAppel", 1);     //colonne invisible
            listViewActesTech.Columns.Add("IdTMM", 1);     //colonne invisible
            listViewActesTech.Columns.Add("Qte", 20);
            listViewActesTech.Columns.Add("Libelle", 150);
            listViewActesTech.Columns.Add("Code", 80);
            listViewActesTech.Columns.Add("Categorie", 1);     //colonne invisible
            listViewActesTech.View = View.Details;
            listViewActesTech.HeaderStyle = ColumnHeaderStyle.None;

            listViewMat.Columns.Add("Id", 1);     //colonne invisible
            listViewMat.Columns.Add("NumAppel", 1);     //colonne invisible
            listViewMat.Columns.Add("IdTMM", 1);     //colonne invisible
            listViewMat.Columns.Add("Qte", 20);
            listViewMat.Columns.Add("Libelle", 150);
            listViewMat.Columns.Add("Code", 80);
            listViewMat.Columns.Add("Categorie", 1);     //colonne invisible
            listViewMat.View = View.Details;
            listViewMat.HeaderStyle = ColumnHeaderStyle.None;

            listViewMedic.Columns.Add("Id", 1);     //colonne invisible
            listViewMedic.Columns.Add("NumAppel", 1);     //colonne invisible
            listViewMedic.Columns.Add("IdTMM", 1);     //colonne invisible
            listViewMedic.Columns.Add("Qte", 20);
            listViewMedic.Columns.Add("Libelle", 150);
            listViewMedic.Columns.Add("Code", 80);
            listViewMedic.Columns.Add("Categorie", 1);     //colonne invisible
            listViewMedic.View = View.Details;
            listViewMedic.HeaderStyle = ColumnHeaderStyle.None;

            //En fonction deys droits
            if (VariablesApplicatives.Utilisateurs.Droits == VariablesApplicatives.Utilisateurs.CodeDroits.Secretaire)               
            {
                groupBoxSecretariat.Visible = true;
                groupBoxFacturation.Visible = false;
                DroitsUtilisateur = "Secretaire";
            }
            else
            {
                groupBoxFacturation.Visible = true;
                groupBoxSecretariat.Visible = false;
                DroitsUtilisateur = "Facturation";
            }
        }


        private void FicheConsult_Load(object sender, EventArgs e)
        {
            //on affiche le numéro d'appel
            lblNumVisite.Text = NumVisite.ToString();
            lNumFacture.Text = "F: " + RecupNumFacture(NumVisite);

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
            dsFiche.Tables.Add("Visite");
            dsFiche.Tables.Add("ListeAMM");           

            //on récupère les infos de la fiche
            RecupFiche();
            RecupAMM();

            //on charge les champs
            ChargeChamps();
            ChargeAMM();

            //on met les textbox en plus grand
            TailleTextBox();

            //si la fiche est en lecture seule (Facturation terminée) ou Vérouillée par un autre utilisateur
            if (LectureSeule == 1)
            {
                MessageBox.Show("Cette fiche est en lecture seule, vous ne pouvez pas lui apporter de modifications", "Fiche en lecture seule", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnValider.Enabled = false;
                btnValider.BackgroundImage = Resources._lock;
                btnAjouterActesTech.Enabled = false;
                btnAjouterMateriel.Enabled = false;
                btnAjouterMedic.Enabled = false;
                groupBoxSecretariat.Enabled = false;
                groupBoxFacturation.Enabled = false;
            }
            else if (Verrou != "")
            {
                MessageBox.Show("Cette fiche est verrouillée par " + Verrou + ", vous ne pouvez pas lui apporter de modifications", "Fiche verrouillée", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnValider.Enabled = false;
                btnValider.BackgroundImage = Resources._lock;
                btnAjouterActesTech.Enabled = false;
                btnAjouterMateriel.Enabled = false;
                btnAjouterMedic.Enabled = false;
                groupBoxSecretariat.Enabled = false;
                groupBoxFacturation.Enabled = false;
            }
            else
            {
                VerrouilleFiche(NumVisite);
            }
        }


        //pour récupérer les infos de la fiche
        private void RecupFiche()
        {
            //Chaine de connection
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base_Fiche"].ToString();
            MySqlConnection dbConnection = new MySqlConnection(connex);

            dbConnection.Open();

            MySqlCommand cmd = new MySqlCommand();

            cmd.Connection = dbConnection;

            try
            {                                                        
                String requete1 = "SELECT * FROM visite v INNER JOIN correspondance c ON v.Num_Appel = c.Num_Appel";
                requete1 += "                             INNER JOIN assurances a ON v.Num_Appel = a.Num_Appel";
                requete1 += "                             INNER JOIN structconsult s ON v.Num_Appel = s.Num_Appel";
                requete1 += "      WHERE v.Num_Appel = " + NumVisite;

                //on execute les requête
                cmd.CommandText = requete1;
                dsFiche.Tables["Visite"].Clear();
                dsFiche.Tables["Visite"].Load(cmd.ExecuteReader());
            }
            catch (Exception e)
            {
                MessageBox.Show("Impossible de récupérer la fiche de consultation " + e.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        //Actes techniques, Medicaments, Materiels
        private void RecupAMM()
        {
            //Chaine de connection
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base_Fiche"].ToString();
            MySqlConnection dbConnection = new MySqlConnection(connex);

            dbConnection.Open();

            MySqlCommand cmd = new MySqlCommand();

            cmd.Connection = dbConnection;

            try
            {
                //requête pour récupérer la liste des médicaments/materiel/actes techniques
                string requete = "SELECT * FROM listeamm l";
                requete += "      WHERE l.Num_Appel = " + NumVisite;

                cmd.CommandText = requete;
                dsFiche.Tables["ListeAMM"].Clear();
                dsFiche.Tables["ListeAMM"].Load(cmd.ExecuteReader());
            }
            catch (Exception e)
            {
                MessageBox.Show("Impossible de récupérer les AMM " + e.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        //Actes techniques, Medicaments, Materiels
        private void ChargeAMM()
        {
            listViewActesTech.BeginUpdate();
            listViewMat.BeginUpdate();
            listViewMedic.BeginUpdate();
            listViewActesTech.Items.Clear();
            listViewMat.Items.Clear();
            listViewMedic.Items.Clear();

            //liste médicaments/actes technique/materiel
            for (int i = 0; i < dsFiche.Tables["ListeAMM"].Rows.Count; i++)
            {
                if (dsFiche.Tables["ListeAMM"].Rows[i]["Categorie"].ToString() == "Actes Techniques")
                {
                    ListViewItem item = new ListViewItem(dsFiche.Tables["ListeAMM"].Rows[i]["Id"].ToString());
                    item.SubItems.Add(dsFiche.Tables["ListeAMM"].Rows[i]["Num_Appel"].ToString());
                    item.SubItems.Add(dsFiche.Tables["ListeAMM"].Rows[i]["IdTMM"].ToString());
                    item.SubItems.Add(dsFiche.Tables["ListeAMM"].Rows[i]["Qte"].ToString());
                    item.SubItems.Add(dsFiche.Tables["ListeAMM"].Rows[i]["Libelle"].ToString());
                    item.SubItems.Add(dsFiche.Tables["ListeAMM"].Rows[i]["Code"].ToString());
                    item.SubItems.Add(dsFiche.Tables["ListeAMM"].Rows[i]["Categorie"].ToString());
                    listViewActesTech.Items.Add(item);
                }
                if (dsFiche.Tables["ListeAMM"].Rows[i]["Categorie"].ToString() == "Materiel")
                {
                    ListViewItem item = new ListViewItem(dsFiche.Tables["ListeAMM"].Rows[i]["Id"].ToString());
                    item.SubItems.Add(dsFiche.Tables["ListeAMM"].Rows[i]["Num_Appel"].ToString());
                    item.SubItems.Add(dsFiche.Tables["ListeAMM"].Rows[i]["IdTMM"].ToString());
                    item.SubItems.Add(dsFiche.Tables["ListeAMM"].Rows[i]["Qte"].ToString());
                    item.SubItems.Add(dsFiche.Tables["ListeAMM"].Rows[i]["Libelle"].ToString());
                    item.SubItems.Add(dsFiche.Tables["ListeAMM"].Rows[i]["Code"].ToString());
                    item.SubItems.Add(dsFiche.Tables["ListeAMM"].Rows[i]["Categorie"].ToString());
                    listViewMat.Items.Add(item);
                }
                if (dsFiche.Tables["ListeAMM"].Rows[i]["Categorie"].ToString() == "Medicament")
                {
                    ListViewItem item = new ListViewItem(dsFiche.Tables["ListeAMM"].Rows[i]["Id"].ToString());
                    item.SubItems.Add(dsFiche.Tables["ListeAMM"].Rows[i]["Num_Appel"].ToString());
                    item.SubItems.Add(dsFiche.Tables["ListeAMM"].Rows[i]["IdTMM"].ToString());
                    item.SubItems.Add(dsFiche.Tables["ListeAMM"].Rows[i]["Qte"].ToString());
                    item.SubItems.Add(dsFiche.Tables["ListeAMM"].Rows[i]["Libelle"].ToString());
                    item.SubItems.Add(dsFiche.Tables["ListeAMM"].Rows[i]["Code"].ToString());
                    item.SubItems.Add(dsFiche.Tables["ListeAMM"].Rows[i]["Categorie"].ToString());
                    listViewMedic.Items.Add(item);
                }

            }    //Fin for

            listViewActesTech.EndUpdate();
            listViewMat.EndUpdate();
            listViewMedic.EndUpdate();
        }

        //pour charger tout les champs depuis le DataSet
        private void ChargeChamps()
        {
            //on récupère le nom du médecin depuis la base SmartRapport
            lblNomMed.Text = RecupNomMedecin(dsFiche.Tables["Visite"].Rows[0]["CodeMedecin"].ToString());

            //Date de la visite
            datePickerDateVisite.Value = DateTime.Parse(dsFiche.Tables["Visite"].Rows[0]["DateC"].ToString());

            //Date de départ
            if(dsFiche.Tables["Visite"].Rows[0]["HDepart"].ToString() != "")
            {
                datePickerDepart.Value = DateTime.Parse(dsFiche.Tables["Visite"].Rows[0]["HDepart"].ToString());
            }
            else
            {
                datePickerDepart.Value = DateTime.Parse("01.01.2000 00:00:00");
            }

            //Date d'arrivée
            if (dsFiche.Tables["Visite"].Rows[0]["HArrivee"].ToString() != "")
            {
                datePickerArrivee.Value = DateTime.Parse(dsFiche.Tables["Visite"].Rows[0]["HArrivee"].ToString());
            }
            else
            {
                datePickerArrivee.Value = DateTime.Parse("01.01.2000 00:00:00");
            }

            //Consultation multiple
            if (dsFiche.Tables["Visite"].Rows[0]["ConsultMult"].ToString() == "1")
            {
                checkBoxConsMultiple.Checked = true;
            }
            else
            {
                checkBoxConsMultiple.Checked = false;
            }

            //Nom du patient
            tbxNomPatient.Text = dsFiche.Tables["Visite"].Rows[0]["NomPatient"].ToString();

            //prénom du patient
            tbxPrenomPatient.Text = dsFiche.Tables["Visite"].Rows[0]["PrenomPatient"].ToString();

            //date de naissance du patient
            datePickerNaissPatient.Value = DateTime.Parse(dsFiche.Tables["Visite"].Rows[0]["DateNaiss"].ToString());

            //sexe du patient
            if (dsFiche.Tables["Visite"].Rows[0]["Sexe"].ToString() == "H")
            {
                radioButtonHomme.Checked = true;
                radioButtonFemme.Checked = false;
            }
            else if(dsFiche.Tables["Visite"].Rows[0]["Sexe"].ToString() == "F")
            {
                radioButtonHomme.Checked = false;
                radioButtonFemme.Checked = true;
            }
            else
            {
                radioButtonHomme.Checked = false;
                radioButtonFemme.Checked = false;
            }

            //numéro de rue du patient
            tbxNumRuePatient.Text = dsFiche.Tables["Visite"].Rows[0]["Num_Rue"].ToString();

            //adresse du patient
            tbxRuePatient.Text = dsFiche.Tables["Visite"].Rows[0]["Adr1"].ToString();

            //complément d'adresse du patient
            if(dsFiche.Tables["Visite"].Rows[0]["Adr2"].ToString() != "")
            {
                tbxAdressePatient2.Text = dsFiche.Tables["Visite"].Rows[0]["Adr2"].ToString();
            }

            //commune du patient
            tbxCommunePatient.Text = dsFiche.Tables["Visite"].Rows[0]["Commune"].ToString();

            //cp du patient
            tbxCpPatient.Text = dsFiche.Tables["Visite"].Rows[0]["CodePostal"].ToString();

            //pays du patient
            tbxPaysPatient.Text = dsFiche.Tables["Visite"].Rows[0]["Pays"].ToString();

            //tel du patient
            tbxTelPatient.Text = dsFiche.Tables["Visite"].Rows[0]["Tel_Patient"].ToString();

            //rue de facturation
            if(dsFiche.Tables["Visite"].Rows[0]["AdresseFact"].ToString() != "")
            {
                tbxRueFact.Text = dsFiche.Tables["Visite"].Rows[0]["AdresseFact"].ToString();
            }

            //code postal de facturation
            if (dsFiche.Tables["Visite"].Rows[0]["CpFact"].ToString() != "")
            {
                tbxCpFact.Text = dsFiche.Tables["Visite"].Rows[0]["CpFact"].ToString();
            }

            //Commune de factuation
            if (dsFiche.Tables["Visite"].Rows[0]["CommuneFact"].ToString() != "")
            {
                tbxCommuneFact.Text = dsFiche.Tables["Visite"].Rows[0]["CommuneFact"].ToString();
            }

            //pays de facturation
            if (dsFiche.Tables["Visite"].Rows[0]["PaysFact"].ToString() != "")
            {
                tbxPaysFact.Text = dsFiche.Tables["Visite"].Rows[0]["PaysFact"].ToString();
            }

            //remarque
            if(dsFiche.Tables["Visite"].Rows[0]["Remarque"].ToString() != "")
            {
                tbxRemarque.Text = dsFiche.Tables["Visite"].Rows[0]["Remarque"].ToString();
            }           
            
            //Bon de police (0 = désactivé, 1 = Jour, 2 = Nuit, 3 = soir)
            if (dsFiche.Tables["Visite"].Rows[0]["Bon_Police"].ToString() == "1")
            {
                checkBoxPoliceJour.Checked = true;
                checkBoxPoliceSoir.Checked = false;
                checkBoxPoliceNuit.Checked = false;
            }
                       

            if (dsFiche.Tables["Visite"].Rows[0]["Bon_Police"].ToString() == "2")
            {
                checkBoxPoliceJour.Checked = false;
                checkBoxPoliceSoir.Checked = false;
                checkBoxPoliceNuit.Checked = true;
            }

            if (dsFiche.Tables["Visite"].Rows[0]["Bon_Police"].ToString() == "3")
            {
                checkBoxPoliceJour.Checked = false;
                checkBoxPoliceSoir.Checked = true;
                checkBoxPoliceNuit.Checked = false;
            }

            //sans taxe d'urgence
            if (dsFiche.Tables["Visite"].Rows[0]["SsTaxeUrg"].ToString() == "1")
            {
                checkBoxSsTaxeUrg.Checked = true;
            }
            else if(dsFiche.Tables["Visite"].Rows[0]["SsTaxeUrg"].ToString() == "0")
            {
                checkBoxSsTaxeUrg.Checked = false;
            }

            //cas maladie
            if (dsFiche.Tables["Visite"].Rows[0]["Cas_Maladie"].ToString() == "1")
            {
                checkBoxCasMaladie.Checked = true;
            }
            else if (dsFiche.Tables["Visite"].Rows[0]["Cas_Maladie"].ToString() == "0")
            {
                checkBoxCasMaladie.Checked = false;
            }

            //cas accident
            if (dsFiche.Tables["Visite"].Rows[0]["Cas_Accident"].ToString() == "1")
            {
                checkBoxCasAccident.Checked = true;
            }
            else if (dsFiche.Tables["Visite"].Rows[0]["Cas_Accident"].ToString() == "0")
            {
                checkBoxCasAccident.Checked = false;
            }

            //cas militaire
            if (dsFiche.Tables["Visite"].Rows[0]["Cas_Militaire"].ToString() == "1")
            {
                checkBoxCasMilitaire.Checked = true;
            }
            else if (dsFiche.Tables["Visite"].Rows[0]["Cas_Militaire"].ToString() == "0")
            {
                checkBoxCasMilitaire.Checked = false;
            }

            //cas Police
            if (dsFiche.Tables["Visite"].Rows[0]["Cas_Police"].ToString() == "1")
            {
                checkBoxCasPolice.Checked = true;
            }
            else if (dsFiche.Tables["Visite"].Rows[0]["Cas_Police"].ToString() == "0")
            {
                checkBoxCasPolice.Checked = false;
            }

            //Ass Inv
            if (dsFiche.Tables["Visite"].Rows[0]["Ass_Inv"].ToString() == "1")
            {
                checkBoxAssInv.Checked = true;
            }
            else if (dsFiche.Tables["Visite"].Rows[0]["Ass_Inv"].ToString() == "0")
            {
                checkBoxAssInv.Checked = false;
            }

            //Rmcas
            if (dsFiche.Tables["Visite"].Rows[0]["Rmcas"].ToString() == "1")
            {
                checkBoxRmcas.Checked = true;
            }
            else if (dsFiche.Tables["Visite"].Rows[0]["Rmcas"].ToString() == "0")
            {
                checkBoxRmcas.Checked = false;
            }

            //Rabais 10%
            if (dsFiche.Tables["Visite"].Rows[0]["Rabais_10pc"].ToString() == "1")
            {
                checkBoxRabais10.Checked = true;
            }
            else if (dsFiche.Tables["Visite"].Rows[0]["Rabais_10pc"].ToString() == "0")
            {
                checkBoxRabais10.Checked = false;
            }
          
            //spc
            if (dsFiche.Tables["Visite"].Rows[0]["Spc"].ToString() == "1")
            {
                checkBoxSpc.Checked = true;
            }
            else if (dsFiche.Tables["Visite"].Rows[0]["Spc"].ToString() == "0")
            {
                checkBoxSpc.Checked = false;
            }

            //sans facture
            if (dsFiche.Tables["Visite"].Rows[0]["SansFacture"].ToString() == "1")
            {
                checkBoxSansFacture.Checked = true;
            }
            else if (dsFiche.Tables["Visite"].Rows[0]["SansFacture"].ToString() == "0")
            {
                checkBoxSansFacture.Checked = false;
            }

            //hg/cass
            if (dsFiche.Tables["Visite"].Rows[0]["Hg_Cass"].ToString() == "1")
            {
                checkBoxHg.Checked = true;
            }
            else if (dsFiche.Tables["Visite"].Rows[0]["Hg_Cass"].ToString() == "0")
            {
                checkBoxHg.Checked = false;
            }

            //Autre
            if (dsFiche.Tables["Visite"].Rows[0]["Autre"].ToString() != "")
            {
                tbxAutre.Text = dsFiche.Tables["Visite"].Rows[0]["Autre"].ToString();
            }

            //tuteur / assistant social
            if (dsFiche.Tables["Visite"].Rows[0]["Tuteur_Assist"].ToString() != "")
            {
                tbxAssSocial.Text = dsFiche.Tables["Visite"].Rows[0]["Autre"].ToString();
            }

            //Assurance
            if (dsFiche.Tables["Visite"].Rows[0]["Assurance"].ToString() != "")
            {
                tbxAssurance.Text = dsFiche.Tables["Visite"].Rows[0]["Assurance"].ToString();
            }

            //Encaissé sur place
            if (dsFiche.Tables["Visite"].Rows[0]["Encaisse_Place"].ToString() == "0")
            {
                checkBoxEncaissSurPlace.Checked = false;
            }
            else if(dsFiche.Tables["Visite"].Rows[0]["Encaisse_Place"].ToString() == "1")
            {
                checkBoxEncaissSurPlace.Checked = true;
            }

            //Règlement encaissés sur place
            tBoxMontant.Text = string.Format("{0:0.00}", dsFiche.Tables["Visite"].Rows[0]["MontantEncaiss"].ToString());

            if (dsFiche.Tables["Visite"].Rows[0]["TypePaiement"].ToString() == "Carte")
                rBCarte.Checked = true;
            else if (dsFiche.Tables["Visite"].Rows[0]["TypePaiement"].ToString() == "Especes")
                rBEspece.Checked = true;

            //numéro de carte d'assuré
            if (dsFiche.Tables["Visite"].Rows[0]["NumCarteAssure"].ToString() != "")
            {
                tbxNumCarteAssur.Text = dsFiche.Tables["Visite"].Rows[0]["NumCarteAssure"].ToString();
            }

            //certificat médical pour maladie/accident
            if (dsFiche.Tables["Visite"].Rows[0]["CertifMaladie"].ToString() == "1")
            {
                radioButtonMaladie.Checked = true;
                radioButtonAccident.Checked = false;
                datePickerCertifMedDu.Enabled = true;
                datePickerCertifMedAu.Enabled = true;
                datePickerCertifMedDu.Value = DateTime.Parse(dsFiche.Tables["Visite"].Rows[0]["DateDebCertif"].ToString());
                datePickerCertifMedAu.Value = DateTime.Parse(dsFiche.Tables["Visite"].Rows[0]["DateFinCertif"].ToString());
            }
            else if (dsFiche.Tables["Visite"].Rows[0]["CertifAccident"].ToString() == "1")
            {
                radioButtonMaladie.Checked = false;
                radioButtonAccident.Checked = true;
                datePickerCertifMedDu.Enabled = true;
                datePickerCertifMedAu.Enabled = true;
                datePickerCertifMedDu.Value = DateTime.Parse(dsFiche.Tables["Visite"].Rows[0]["DateDebCertif"].ToString());
                datePickerCertifMedAu.Value = DateTime.Parse(dsFiche.Tables["Visite"].Rows[0]["DateFinCertif"].ToString());
            }
            else
            {
                radioButtonMaladie.Checked = false;
                radioButtonAccident.Checked = false;
                datePickerCertifMedDu.Enabled = false;
                datePickerCertifMedAu.Enabled = false;
            }

            //date début certificat médical
            if (dsFiche.Tables["Visite"].Rows[0]["DateDebCertif"].ToString() != "")
            {
                datePickerCertifMedDu.Value = DateTime.Parse(dsFiche.Tables["Visite"].Rows[0]["DateDebCertif"].ToString());
            }

            //date fin certificat médical
            if (dsFiche.Tables["Visite"].Rows[0]["DateFinCertif"].ToString() != "")
            {
                datePickerCertifMedAu.Value = DateTime.Parse(dsFiche.Tables["Visite"].Rows[0]["DateFinCertif"].ToString());
            }

            //nom du médecin traitant
            if (dsFiche.Tables["Visite"].Rows[0]["NomMedTraitant"].ToString() != "")
            {
                tbxNomMedTraitant.Text = dsFiche.Tables["Visite"].Rows[0]["NomMedTraitant"].ToString();
            }

            //Prénom du médecin traitant
            if (dsFiche.Tables["Visite"].Rows[0]["PrenomMedTraitant"].ToString() != "")
            {
                tbxPrenomMedTraitant.Text = dsFiche.Tables["Visite"].Rows[0]["PrenomMedTraitant"].ToString();
            }

            //organiser un ta
            if (dsFiche.Tables["Visite"].Rows[0]["ORGTA"].ToString() == "0")
            {
                checkBoxOrgTa.Checked = false;
            }
            else if (dsFiche.Tables["Visite"].Rows[0]["ORGTA"].ToString() == "1")
            {
                checkBoxOrgTa.Checked = true;
            }

            //organisation aide à domicile
            if (dsFiche.Tables["Visite"].Rows[0]["ORGAideDom"].ToString() == "0")
            {
                checkBoxOrgAideDomic.Checked = false;
            }
            else if (dsFiche.Tables["Visite"].Rows[0]["ORGAideDom"].ToString() == "1")
            {
                checkBoxOrgAideDomic.Checked = true;
            }

            //Durée de la consultation
            if (dsFiche.Tables["Visite"].Rows[0]["15mn"].ToString() == "1")
            {
                radioButton15Min.Checked = true;
            }
            else if(dsFiche.Tables["Visite"].Rows[0]["20mn"].ToString() == "1")
            {
                radioButton20Min.Checked = true;
            }
            else if (dsFiche.Tables["Visite"].Rows[0]["25mn"].ToString() == "1")
            {
                radioButton25Min.Checked = true;
            }
            else if (dsFiche.Tables["Visite"].Rows[0]["30mn"].ToString() == "1")
            {
                radioButton30Min.Checked = true;
            }
            else if (dsFiche.Tables["Visite"].Rows[0]["35mn"].ToString() == "1")
            {
                radioButton35Min.Checked = true;
            }
            else if (dsFiche.Tables["Visite"].Rows[0]["40mn"].ToString() == "1")
            {
                radioButton40Min.Checked = true;
            }
            else if (dsFiche.Tables["Visite"].Rows[0]["Autre_Duree"].ToString() != "")
            {
                numericUpDownAutreDuree.Value = Convert.ToInt32(dsFiche.Tables["Visite"].Rows[0]["Autre_Duree"]);
                radioButtonAutreDuree.Checked = true;
            }

            //psychothérapie
            numericUpDownPsycho.Value = Convert.ToInt32(dsFiche.Tables["Visite"].Rows[0]["PsychoMn"]);

            //réanimation
            numericUpDownRea.Value = Convert.ToInt32(dsFiche.Tables["Visite"].Rows[0]["ReaMn"]);

            //transport
            numericUpDownTransport.Value = Convert.ToInt32(dsFiche.Tables["Visite"].Rows[0]["TransportMn"]);

            //examen cadavérique
            numericUpDownExaCadav.Value = Convert.ToInt32(dsFiche.Tables["Visite"].Rows[0]["ExaCadavMn"]);

            //état de la facturation ou du secretariat
            if (DroitsUtilisateur == "Secretaire")
            {
                String EtatSecr = dsFiche.Tables["Visite"].Rows[0]["Etat_Secretariat"].ToString();
                switch (EtatSecr)
                {
                    case "0": rbSecrATraiter.Checked = true; break;                   
                    case "1": rbSecrEnAttente.Checked = true; break;
                    case "2": rbSecrTermine.Checked = true; break;
                }
            }
            else
            {
                String EtatFact = dsFiche.Tables["Visite"].Rows[0]["Etat_Facturation"].ToString();
                switch (EtatFact)
                {
                    case "0": rbFactATraiter.Checked = true; break;
                    case "1": rbFactEnAttente.Checked = true; break;
                    case "2": rbFactTermine.Checked = true; break;
                }
            }
                                                                       
            //on affiche le titre de la form avec le nom + prenom du patient + date visite
            this.Text = "Fiche de " + dsFiche.Tables["Visite"].Rows[0]["NomPatient"].ToString() + " " +
                dsFiche.Tables["Visite"].Rows[0]["PrenomPatient"].ToString() +
                " pour la visite du " + dsFiche.Tables["Visite"].Rows[0]["DateC"].ToString();

            //pour savoir si la fiche est verrouillée ou en lecture seule (facturation terminée)
            LectureSeule = int.Parse(dsFiche.Tables["Visite"].Rows[0]["LectureSeule"].ToString());
            Verrou = dsFiche.Tables["Visite"].Rows[0]["Verrou"].ToString();

            //si la fiche est en lecture seule on l'affiche en haut
            if (LectureSeule == 1)            
                this.Text += " (Lecture seule)";            
            else if (Verrou != "")
                this.Text += " (Lecture seule), Vérouillée par " + Verrou;
        }

        private string AppliquerModif()
        {
            string retour = "KO";

            //on met tout les champs dans des variables pour les passer dans la requête
            string HArrivee = convertDateMaria(datePickerArrivee.Value.ToString(), "MariaDb");
            string HDepart = convertDateMaria(datePickerDepart.Value.ToString(), "MariaDb");
            string ConsultMult = "0";
            
            if(checkBoxConsMultiple.Checked == true)
                ConsultMult = "1";

            string Remarque = tbxRemarque.Text.Replace("'", "''").Replace(",", ",,");
            string NomPatient = tbxNomPatient.Text.Replace("'", "''");
            string PrenomPatient = tbxPrenomPatient.Text.Replace("'", "''");
            string DateNaiss = convertDateMaria(datePickerNaissPatient.Value.ToString(), "MariaDb");
            string Sexe = "";
            
            if (radioButtonHomme.Checked == true) Sexe = "H";
            else if (radioButtonFemme.Checked == true) Sexe = "F";
            
            string NumRue = tbxNumRuePatient.Text;
            string Adr1 = tbxRuePatient.Text.Replace("'", "''").Replace(",", ",,");
            string Adr2 = tbxAdressePatient2.Text.Replace("'", "''").Replace(",", ",,");
            string CodePostal = tbxCpPatient.Text;
            string Commune = tbxCommunePatient.Text;
            string Pays = tbxPaysPatient.Text;
            string Tel_Patient = tbxTelPatient.Text;
            string AdresseFact = tbxRueFact.Text.Replace("'", "''").Replace(",", ",,");
            string CpFact = tbxCpFact.Text;
            string CommuneFact = tbxCommuneFact.Text;
            string PaysFact = tbxPaysFact.Text;
            string BonPolice = "0";
            
            if (checkBoxPoliceJour.Checked == true) BonPolice = "1";            
            if (checkBoxPoliceNuit.Checked == true) BonPolice = "2";
            if (checkBoxPoliceSoir.Checked == true) BonPolice = "3";
            
            string SsTaxeUrg = "0";
            
            if (checkBoxSsTaxeUrg.Checked == true) SsTaxeUrg = "1";

            string EtatSecr = "0";
            string EtatFact = "0";

            if (DroitsUtilisateur == "Secretaire")
            {
                if (rbSecrATraiter.Checked == true) EtatSecr = "0";
                else if (rbSecrEnAttente.Checked == true) EtatSecr = "1";
                else if (rbSecrTermine.Checked == true) EtatSecr = "2";
            }
            else
            {
                if (rbFactATraiter.Checked == true) EtatFact = "0";
                else if (rbFactEnAttente.Checked == true) EtatFact = "1";
                else if (rbFactTermine.Checked == true)
                {
                    EtatFact = "2";
                    LectureSeule = 1;
                }
            }
            

            string d15mn = "0";            
            if (radioButton15Min.Checked == true) d15mn = "1";            
            string d20mn = "0";            
            if (radioButton20Min.Checked == true) d20mn = "1";            
            string d25mn = "0";            
            if (radioButton25Min.Checked == true) d25mn = "1";            
            string d30mn = "0";            
            if (radioButton30Min.Checked == true) d30mn = "1";            
            string d35mn = "0";            
            if (radioButton35Min.Checked == true) d35mn = "1";            
            string d40mn = "0";            
            if (radioButton40Min.Checked == true) d40mn = "1";            
            string autreDuree = "";            
            if (radioButtonAutreDuree.Checked == true) autreDuree = numericUpDownAutreDuree.Value.ToString();
            
            string Psychomn = numericUpDownPsycho.Value.ToString();
            string Reamn = numericUpDownRea.Value.ToString();
            string Transportmn = numericUpDownTransport.Value.ToString();
            string ExaCadavmn = numericUpDownExaCadav.Value.ToString();
            string NomMedTraitant = tbxNomMedTraitant.Text.Replace("'", "''");
            string PrenomMedTraitant = tbxPrenomMedTraitant.Text.Replace("'", "''");
            string ORGTA = "0";
            
            if (checkBoxOrgTa.Checked == true) ORGTA = "1";
            
            string ORGAideDom = "0";            
            if (checkBoxOrgAideDomic.Checked == true) ORGAideDom = "1";
            string Cas_Maladie = "0";
            if (checkBoxCasMaladie.Checked == true) Cas_Maladie = "1";
            string Cas_Accident = "0";
            if (checkBoxCasAccident.Checked == true) Cas_Accident = "1";
            string Cas_Militaire = "0";
            if (checkBoxCasMilitaire.Checked == true) Cas_Militaire = "1";
            string Cas_Police = "0";
            if (checkBoxCasPolice.Checked == true) Cas_Police = "1";
            string Ass_Inv = "0";
            if (checkBoxAssInv.Checked == true) Ass_Inv = "1";
            string RMCAS = "0";
            if (checkBoxRmcas.Checked == true) RMCAS = "1";
            string Rabais_10Pc = "0";
            if (checkBoxRabais10.Checked == true) Rabais_10Pc = "1";
            string SPC = "0";
            if (checkBoxSpc.Checked == true) SPC = "1";
            string SansFacture = "0";
            if (checkBoxSansFacture.Checked == true) SansFacture = "1";
            string HG_CASS = "0";
            if (checkBoxHg.Checked == true) HG_CASS = "1";
            string Encaisse_Place = "0";
            if (checkBoxEncaissSurPlace.Checked == true) Encaisse_Place = "1";
            
            string Autre = tbxAutre.Text.Replace("'", "''");
            string Tuteur_Assist = tbxAssSocial.Text.Replace("'", "''");
            string Assurance = tbxAssurance.Text.Replace("'", "''");
            string NumCarteAssure = tbxNumCarteAssur.Text;
            string CertifMaladie = "0";
           
            if (radioButtonMaladie.Checked == true) CertifMaladie = "1";
            string CertifAccident = "0";
            
            if (radioButtonAccident.Checked == true) CertifAccident = "1";
            string DateDebCertif = "";
            string DateFinCertif = "";
            
            if(radioButtonAccident.Checked || radioButtonMaladie.Checked)
            {
                DateDebCertif = convertDateMaria(datePickerCertifMedDu.Value.ToString(), "MariaDb");
                DateFinCertif = convertDateMaria(datePickerCertifMedAu.Value.ToString(), "MariaDb");
            }

            //Règlement encaissés sur place
            string Montant = "0.00";
            string TypePaiement = "";

            //Si c'est bien une valeur
            if (Decimal.TryParse(tBoxMontant.Text, out decimal value))
                if (value > 0)    //et qu'elle est > 0
                {
                    Montant = string.Format("{0:0.00}", tBoxMontant.Text);

                    if (rBCarte.Checked)
                        TypePaiement = "Carte";
                    else if (rBEspece.Checked)
                        TypePaiement = "Especes";
                }
                else
                    Montant = "0.00";
            else
                Montant = "0.00";
                               
            //Chaine de connection
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base_Fiche"].ToString();
            MySqlConnection dbConnection = new MySqlConnection(connex);

            dbConnection.Open();

            MySqlCommand cmd = new MySqlCommand();

            cmd.Connection = dbConnection;

            //On déclare ici les requetes (à cause de la transaction)
            string requete1 = "";
            string requete2 = "";
            string requete3 = "";
            string requete4 = "";

            try
            {
                //Dans une transaction, on créé les enreg pour visite, structconsult, correspondance, assurances                

                MySqlTransaction trans;   //Déclaration d'une transaction

                //Def des requettes
                //*****visite***
                //Maj de la visite                                                                   
                requete1 = "UPDATE visite SET HArrivee = '" + HArrivee + "', HDepart = '" + HDepart + "', ConsultMult = " + ConsultMult;
                requete1 += ", Remarque = '" + Remarque + "', NomPatient = '" + NomPatient + "', PrenomPatient = '" + PrenomPatient;
                requete1 += "', DateNaiss = '" + DateNaiss + "', Sexe = '" + Sexe + "', Num_Rue = '" + NumRue + "', Adr1 = '" + Adr1;
                requete1 += "', Adr2 = '" + Adr2 + "', CodePostal = '" + CodePostal + "', Commune = '" + Commune + "', Pays = '" + Pays;
                requete1 += "', Tel_Patient = '" + Tel_Patient + "', AdresseFact = '" + AdresseFact + "', CpFact = '" + CpFact;
                requete1 += "', CommuneFact = '" + CommuneFact + "', PaysFact = '" + PaysFact + "', Bon_Police = " + BonPolice + ", SsTaxeUrg = " + SsTaxeUrg;

                //En fonction des droits de la personne
                if (DroitsUtilisateur == "Secretaire")
                    requete1 += ", Etat_Secretariat = " + EtatSecr;
                else
                {
                    //Si c'est la facturation et que c'est terminé, on met en lecture seule
                    if (EtatFact == "2")
                    {
                        requete1 += ", Etat_Facturation = " + EtatFact + ", LectureSeule = 1";
                        
                        /* 
                         * Not using Facture functions anymore, need only status of the consultation
                         * HARD CODED CHANGE to make consultations show GREEN LED                   
                         */
                        Variables.ConnexionBase.ExecuteSqlSansRetour("update tableconsultations set FactureGeneree=1 WHERE NConsultation = " + NumVisite);
                    }
                    else
                    {
                        requete1 += ", Etat_Facturation = " + EtatFact;
                    }
                }

                requete1 += " WHERE Num_Appel = " + NumVisite;

                //****structconsult***
                requete2 = "UPDATE structconsult SET 15mn = " + d15mn + ", 20mn = " + d20mn + ", 25mn = " + d25mn;
                requete2 += ", 30mn = " + d30mn + ", 35mn = " + d35mn + ", 40mn = " + d40mn + ", Autre_Duree = '" + autreDuree + "', PsychoMn = " + Psychomn;
                requete2 += ", ReaMn = " + Reamn + ", TransportMn = " + Transportmn + ", ExaCadavMn = " + ExaCadavmn;
                requete2 += " WHERE Num_Appel = " + NumVisite;

                //****correspondance***
                requete3 = "UPDATE correspondance SET NomMedTraitant = '" + NomMedTraitant + "', PrenomMedTraitant = '" + PrenomMedTraitant;
                requete3 += "', ORGTA = " + ORGTA + ", ORGAideDom = " + ORGAideDom;
                requete3 += " WHERE Num_Appel = " + NumVisite;

                //****assurances*****
                requete4 = "UPDATE assurances SET Cas_Maladie = " + Cas_Maladie + ", Cas_Accident = " + Cas_Accident;
                requete4 += ", Cas_Militaire = " + Cas_Militaire + ", Cas_Police = " + Cas_Police;
                requete4 += ", Ass_Inv = " + Ass_Inv + ", RMCAS = " + RMCAS + ", Rabais_10Pc = " + Rabais_10Pc;
                requete4 += ", SPC = " + SPC + ", SansFacture = " + SansFacture + ", HG_CASS = " + HG_CASS;
                requete4 += ", Autre = '" + Autre + "', Tuteur_Assist = '" + Tuteur_Assist + "', Assurance = '" + Assurance;
                requete4 += "', Encaisse_Place = " + Encaisse_Place + ", NumCarteAssure = '" + NumCarteAssure;                               
                requete4 += "', CertifMaladie = " + CertifMaladie + ", CertifAccident = " + CertifAccident;

                if (DateDebCertif != "")
                    requete4 += ", DateDebCertif = '" + DateDebCertif + "'";
                if (DateFinCertif != "")
                    requete4 += ", DateFinCertif = '" + DateFinCertif + "'";

                requete4 += ", MontantEncaiss = " + Montant + ", TypePaiement = '" + TypePaiement + "'";
               
                requete4 += " WHERE Num_Appel = " + NumVisite;

                //Ouverture d'une transaction
                trans = dbConnection.BeginTransaction();
                try
                {
                    //on execute les requettes                                       
                    cmd.CommandText = requete1; cmd.ExecuteNonQuery();
                    cmd.CommandText = requete2; cmd.ExecuteNonQuery();
                    cmd.CommandText = requete3; cmd.ExecuteNonQuery();
                    cmd.CommandText = requete4; cmd.ExecuteNonQuery();

                    //on valide la transaction
                    trans.Commit();

                    retour = "OK";
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    MessageBox.Show("Erreur lors de la maj de la visite. " + e.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            catch (Exception e)
            {
                MessageBox.Show("Erreur lors de la maj de la visite. " + e.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
       

        //Pour récupérer le nom du médecin depuis SmartRapport
        private string RecupNomMedecin(string CodeMed)
        {
            string Retour = "";

            //Chaine de connection
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);

            dbConnection.Open();
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = dbConnection;

            DataTable dtResult = new DataTable();

            try
            {
                //requête pour récupérer la fiche                                                                 
                String SqlStr0 = " SELECT Nom FROM tablemedecin ";
                SqlStr0 += " WHERE CodeIntervenant = " + CodeMed;

                //on execute la requête
                cmd.CommandText = SqlStr0;
                dtResult.Load(cmd.ExecuteReader());

                if (dtResult.Rows.Count > 0)
                {
                    Retour = dtResult.Rows[0]["Nom"].ToString();
                }
                else
                    Retour = "Aucun médecin";
            }
            catch (Exception e)
            {
                MessageBox.Show("Impossible de récupérer le nom du médecin " + e.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //fermeture des connexions
                if (dbConnection.State == ConnectionState.Open)
                {
                    dbConnection.Close();
                }
            }

            return Retour;
        }

        
        //Retourne le n° de facture de cette consultation, si il y en a une de faite
        private string RecupNumFacture(int NumVisite)
        {
            string NumFacture = "";

            //Chaine de connection
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);

            dbConnection.Open();
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = dbConnection;

            DataTable dtResult = new DataTable();

            try
            {
                //requête pour récupérer la fiche                                                                 
                String SqlStr0 = "SELECT f.NFacture from facture f INNER JOIN factureconsultation f2 ON f2.NFacture = f.NFacture ";
                SqlStr0 += " WHERE f2.NConsultation = " + NumVisite;

                //on execute la requête
                cmd.CommandText = SqlStr0;
                dtResult.Load(cmd.ExecuteReader());

                if (dtResult.Rows.Count > 0)
                {
                    NumFacture = dtResult.Rows[0]["NFacture"].ToString();
                }
                else
                    NumFacture = "";
            }
            catch (Exception e)
            {
                MessageBox.Show("Impossible de récupérer le nom du médecin " + e.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //fermeture des connexions
                if (dbConnection.State == ConnectionState.Open)
                {
                    dbConnection.Close();
                }
            }

            return NumFacture;
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

        //pour agrandir la hauteur des textbox (sinon le texte est coupé)
        private void TailleTextBox()
        {
            tbxNomPatient.AutoSize = false;
            tbxNomPatient.Size = new System.Drawing.Size(325, 22);

            tbxNumRuePatient.AutoSize = false;
            tbxNumRuePatient.Size = new System.Drawing.Size(72, 22);

            tbxRuePatient.AutoSize = false;
            tbxRuePatient.Size = new System.Drawing.Size(228, 22);

            tbxAdressePatient2.AutoSize = false;
            tbxAdressePatient2.Size = new System.Drawing.Size(325, 22);

            tbxCommunePatient.AutoSize = false;
            tbxCommunePatient.Size = new System.Drawing.Size(149, 22);

            tbxCpPatient.AutoSize = false;
            tbxCpPatient.Size = new System.Drawing.Size(149, 22);

            tbxPaysPatient.AutoSize = false;
            tbxPaysPatient.Size = new System.Drawing.Size(149, 22);

            tbxTelPatient.AutoSize = false;
            tbxTelPatient.Size = new System.Drawing.Size(149, 22);

            tbxRueFact.AutoSize = false;
            tbxRueFact.Size = new System.Drawing.Size(325, 22);

            tbxCommuneFact.AutoSize = false;
            tbxCommuneFact.Size = new System.Drawing.Size(149, 22);

            tbxCpFact.AutoSize = false;
            tbxCpFact.Size = new System.Drawing.Size(149, 22);

            tbxPaysFact.AutoSize = false;
            tbxPaysFact.Size = new System.Drawing.Size(149, 22);

            tbxAssSocial.AutoSize = false;
            tbxAssSocial.Size = new System.Drawing.Size(325, 22);

            tbxAssurance.AutoSize = false;
            tbxAssurance.Size = new System.Drawing.Size(325, 22);

            tbxAvs.AutoSize = false;
            tbxAvs.Size = new System.Drawing.Size(325, 22);

            tbxNumCarteAssur.AutoSize = false;
            tbxNumCarteAssur.Size = new System.Drawing.Size(325, 22);

            tbxNomMedTraitant.AutoSize = false;
            tbxNomMedTraitant.Size = new System.Drawing.Size(325, 22);

            tbxPrenomMedTraitant.AutoSize = false;
            tbxPrenomMedTraitant.Size = new System.Drawing.Size(325, 22);

            tbxAutre.AutoSize = false;
            tbxAutre.Size = new System.Drawing.Size(149, 22);
        }

        #region bouton

        private void btnAjouterActesTech_Click(object sender, EventArgs e)
        {
            AjoutElements ajouterActesTech = new AjoutElements();
            ajouterActesTech.NumAppel = NumVisite;
            ajouterActesTech.Type = "Actes Techniques";
            ajouterActesTech.ShowDialog();

            //maj de l'affichage
            RecupAMM();
            ChargeAMM();
        }

        private void btnAjouterMateriel_Click(object sender, EventArgs e)
        {
            AjoutElements ajouterActesTech = new AjoutElements();
            ajouterActesTech.NumAppel = NumVisite;
            ajouterActesTech.Type = "Materiel";
            ajouterActesTech.ShowDialog();

            //maj de l'affichage
            RecupAMM();
            ChargeAMM();
        }

        private void btnAjouterMedic_Click(object sender, EventArgs e)
        {
            AjoutElements ajouterActesTech = new AjoutElements();
            ajouterActesTech.NumAppel = NumVisite;
            ajouterActesTech.Type = "Medicament";
            ajouterActesTech.ShowDialog();

            //maj de l'affichage
            RecupAMM();
            ChargeAMM();
        }

        private void btnFermer_Click(object sender, System.EventArgs e)
        {           
            this.Close();            
        }
        #endregion

        private void FicheConsult_FormClosing(object sender, FormClosingEventArgs e)
        {
            //On dévérouille la fiche
            DeverrouilleFiche(NumVisite);
        }

      
        private void checkBoxPoliceJour_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPoliceJour.Checked == true)
            {
                checkBoxPoliceSoir.Checked = false;
                checkBoxPoliceNuit.Checked = false;
            }
        }

        private void checkBoxPoliceSoir_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPoliceSoir.Checked == true)
            {
                checkBoxPoliceJour.Checked = false;
                checkBoxPoliceNuit.Checked = false;
            }
        }

        private void checkBoxPoliceNuit_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPoliceNuit.Checked == true)
            {
                checkBoxPoliceJour.Checked = false;
                checkBoxPoliceSoir.Checked = false;
            }
        }

        private void btnValider_Click(object sender, EventArgs e)
        {
            string retour = AppliquerModif();

            if(retour == "KO")
            {
                MessageBox.Show("Erreur lors de l'enregistrement des modifiations", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Les modifications ont été enregistrées", "Enregistré", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //Si on est passé en lecture seule, on verouille le Valider
            if (LectureSeule == 1)
            {
                btnValider.BackgroundImage = Resources._lock;
            }

        }

        private void radioButtonMaladie_CheckedChanged(object sender, EventArgs e)
        {
            datePickerCertifMedDu.Enabled = true;
            datePickerCertifMedAu.Enabled = true;
        }

        private void radioButtonAccident_CheckedChanged(object sender, EventArgs e)
        {
            datePickerCertifMedDu.Enabled = true;
            datePickerCertifMedAu.Enabled = true;
        }


        //Vérouillage de la fiche à l'ouverture par un utilisateur
        private void VerrouilleFiche(int NumVisite)
        {
            //Chaine de connection
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base_Fiche"].ToString();
            MySqlConnection dbConnection = new MySqlConnection(connex);

            dbConnection.Open();

            MySqlCommand cmd = new MySqlCommand();

            cmd.Connection = dbConnection;

            //On déclare ici les requetes (à cause de la transaction)
            string SqlStr0 = "";           

            try
            {
                //Dans une transaction, on créé les enreg pour visite, structconsult, correspondance, assurances                
                MySqlTransaction trans;   //Déclaration d'une transaction

                //Maj de la visite                                                                   
                SqlStr0 = "UPDATE visite SET Verrou = '" + VariablesApplicatives.Utilisateurs.NomUtilisateur + "'";
                SqlStr0 += " WHERE Num_Appel = " + NumVisite;

                //Ouverture d'une transaction
                trans = dbConnection.BeginTransaction();
                try
                {
                    //on execute les requettes                                       
                    cmd.CommandText = SqlStr0; cmd.ExecuteNonQuery();
                   
                    //on valide la transaction
                    trans.Commit();

                    //On affecte l'utilisateur au verrou
                    Verrou = VariablesApplicatives.Utilisateurs.NomUtilisateur;
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    MessageBox.Show("Erreur lors du vérouillage de la visite. " + e.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            catch (Exception e)
            {
                MessageBox.Show("Erreur lors du vérouillage de la visite. " + e.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
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


        private void DeverrouilleFiche(int NumVisite)
        {
            //Si l'utilisateur actuel a posé le verrou, on déverouille
            if (Verrou == VariablesApplicatives.Utilisateurs.NomUtilisateur)
            {
                //Chaine de connection
                string connex = ConfigurationManager.ConnectionStrings["Connection_Base_Fiche"].ToString();
                MySqlConnection dbConnection = new MySqlConnection(connex);

                dbConnection.Open();

                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = dbConnection;

                //On déclare ici les requetes (à cause de la transaction)
                string SqlStr0 = "";

                try
                {
                    //Dans une transaction, on créé les enreg pour visite, structconsult, correspondance, assurances                
                    MySqlTransaction trans;   //Déclaration d'une transaction

                    //Maj de la visite                                                                   
                    SqlStr0 = "UPDATE visite SET Verrou = ''";
                    SqlStr0 += " WHERE Num_Appel = " + NumVisite;

                    //Ouverture d'une transaction
                    trans = dbConnection.BeginTransaction();
                    try
                    {
                        //on execute les requettes                                       
                        cmd.CommandText = SqlStr0; cmd.ExecuteNonQuery();

                        //on valide la transaction
                        trans.Commit();
                    }
                    catch (Exception e)
                    {
                        trans.Rollback();
                        MessageBox.Show("Erreur lors du dévérouillage de la visite. " + e.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                catch (Exception e)
                {
                    MessageBox.Show("Erreur lors du dévérouillage de la visite. " + e.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        }

        private void bImprime_Click(object sender, EventArgs e)
        {
            //on charge la fenêtre des utilisateurs
            FImpFicheConsult fImpFicheConsult = new FImpFicheConsult();
            //les paramètres
            fImpFicheConsult.Num_Appel = NumVisite;
            fImpFicheConsult.NomMedecin = lblNomMed.Text;

            fImpFicheConsult.ShowDialog();
            fImpFicheConsult.Dispose();     //On détruit la form
        }

        private void tbxNumCarteAssur_KeyDown(object sender, KeyEventArgs e)
        {
            //Pour le copier/coller
            if (e.KeyCode == Keys.C && e.Modifiers == Keys.Control)
                tbxNumCarteAssur.Copy();
        }

      
    }
}


//A faire: