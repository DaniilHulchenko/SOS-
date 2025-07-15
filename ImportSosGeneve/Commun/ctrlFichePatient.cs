using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ImportSosGeneve
{
    public partial class ctrlFichePatient : UserControl
    {
        private BindingSource _bseDonneesAppel = new BindingSource();


        public BindingSource bseDonneesAppel
        {
            get
            {
                return _bseDonneesAppel;
            }
            set
            {
                _bseDonneesAppel = value;
                // affectation des champs
                dgvListe.DataSource = _bseDonneesAppel;
                if (txtPatient_Nom.DataBindings.Count == 0)
                {
                    ChargeDonnees();
                }
            }
        }

        public ctrlFichePatient()
        {
            InitializeComponent();

            //dgvListe.DataSource = _bseDonneesAppel;
            dgvListe.AutoGenerateColumns = false;
            if (dgvListe.Columns.Count <= 0)
            {
                dgvListe.AlternatingRowsDefaultCellStyle.BackColor = Color.LightSteelBlue;
                dgvListe.RowsDefaultCellStyle.BackColor = Color.WhiteSmoke;
                // Creation des colonnes
                DataGridViewTextBoxColumn clnIndex = new DataGridViewTextBoxColumn();
                clnIndex.DataPropertyName = "NConsultation";
                clnIndex.HeaderText = "Index";
                clnIndex.Name = "NConsultation";
                clnIndex.ReadOnly = true;
                clnIndex.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

                DataGridViewTextBoxColumn clnDateAppel = new DataGridViewTextBoxColumn();
                clnDateAppel.DataPropertyName = "DAP";
                clnDateAppel.HeaderText = "Date Appel";
                clnDateAppel.Name = "DAP";
                clnDateAppel.ReadOnly = true;
                clnDateAppel.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

                DataGridViewTextBoxColumn clnNomMedecinSos = new DataGridViewTextBoxColumn();
                clnNomMedecinSos.DataPropertyName = "NomMedecinSos";
                clnNomMedecinSos.HeaderText = "Medecins";
                clnNomMedecinSos.Name = "NomMedecinSos";
                clnNomMedecinSos.ReadOnly = true;
                clnNomMedecinSos.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                DataGridViewTextBoxColumn clnNomPatient = new DataGridViewTextBoxColumn();
                clnNomPatient.DataPropertyName = "NomPatient";
                clnNomPatient.HeaderText = "Patient";
                clnNomPatient.Name = "NomPatient";
                clnNomPatient.ReadOnly = true;
                clnNomPatient.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

                DataGridViewTextBoxColumn clnAdresse = new DataGridViewTextBoxColumn();
                clnAdresse.DataPropertyName = "";
                clnAdresse.HeaderText = "Adresse";
                clnAdresse.Name = "Adresse";
                clnAdresse.ReadOnly = true;
                clnAdresse.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                DataGridViewTextBoxColumn clnOrigine = new DataGridViewTextBoxColumn();
                clnOrigine.DataPropertyName = "OrigineAppel";
                clnOrigine.HeaderText = "Origine";
                clnOrigine.Name = "OrigineAppel";
                clnOrigine.ReadOnly = true;
                clnOrigine.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

                DataGridViewTextBoxColumn clnEtat = new DataGridViewTextBoxColumn();
                clnEtat.DataPropertyName = "";
                clnEtat.HeaderText = "Etat";
                clnEtat.Name = "Etat";
                clnEtat.ReadOnly = true;
                clnEtat.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

                // Non visible
                DataGridViewTextBoxColumn clnCommune = new DataGridViewTextBoxColumn();
                clnCommune.DataPropertyName = "Commune";
                clnCommune.HeaderText = "Commune";
                clnCommune.Name = "Commune";
                clnCommune.ReadOnly = true;
                clnCommune.Visible = false;

                DataGridViewTextBoxColumn clnRue = new DataGridViewTextBoxColumn();
                clnRue.DataPropertyName = "Rue";
                clnRue.HeaderText = "Rue";
                clnRue.Name = "Rue";
                clnRue.ReadOnly = true;
                clnRue.Visible = false;

                DataGridViewTextBoxColumn clnNumeroDansRue = new DataGridViewTextBoxColumn();
                clnNumeroDansRue.DataPropertyName = "NumeroDansRue";
                clnNumeroDansRue.HeaderText = "NumeroDansRue";
                clnNumeroDansRue.Name = "NumeroDansRue";
                clnNumeroDansRue.ReadOnly = true;
                clnNumeroDansRue.Visible = false;

                DataGridViewTextBoxColumn clnModifie = new DataGridViewTextBoxColumn();
                clnModifie.DataPropertyName = "Modifie";
                clnModifie.HeaderText = "Modifie";
                clnModifie.Name = "Modifie";
                clnModifie.ReadOnly = true;
                clnModifie.Visible = false;

                DataGridViewTextBoxColumn clnRapportGenere = new DataGridViewTextBoxColumn();
                clnRapportGenere.DataPropertyName = "RapportGenere";
                clnRapportGenere.HeaderText = "RapportGenere";
                clnRapportGenere.Name = "RapportGenere";
                clnRapportGenere.ReadOnly = true;
                clnRapportGenere.Visible = false;

                DataGridViewTextBoxColumn clnFactureGeneree = new DataGridViewTextBoxColumn();
                clnFactureGeneree.DataPropertyName = "FactureGeneree";
                clnFactureGeneree.HeaderText = "FactureGeneree";
                clnFactureGeneree.Name = "FactureGeneree";
                clnFactureGeneree.ReadOnly = true;
                clnFactureGeneree.Visible = false;

                dgvListe.Columns.AddRange(new DataGridViewColumn[] { clnIndex, clnDateAppel, clnNomMedecinSos, clnNomPatient, clnAdresse, clnOrigine, clnEtat, clnCommune, clnRue, clnNumeroDansRue, clnModifie, clnRapportGenere, clnFactureGeneree });
            }
        }


        private void ChargeDonnees()
        {
            txtPatient_Nom.DataBindings.Add("Text", _bseDonneesAppel, "NomPatient");
            txtPatient_Prenom.DataBindings.Add("Text", _bseDonneesAppel, "PrenomPatient");
            txtPatient_Age.DataBindings.Add("Text", _bseDonneesAppel, "Age");
            txtPatient_UniteAge.DataBindings.Add("Text", _bseDonneesAppel, "UniteAge");
            txtPatient_Sexe.DataBindings.Add("Text", _bseDonneesAppel, "Sexe");
            txtPatient_Num.DataBindings.Add("Text", _bseDonneesAppel, "NumeroDansRue");
            txtPatient_Adresse.DataBindings.Add("Text", _bseDonneesAppel, "Rue");
            txtPatient_Commune.DataBindings.Add("Text", _bseDonneesAppel, "Commune");
            txtPatient_Tel.DataBindings.Add("Text", _bseDonneesAppel, "TelPatient");
            txtPatient_Longitude.DataBindings.Add("Text", _bseDonneesAppel, "Longitude");
            txtPatient_Latitude.DataBindings.Add("Text", _bseDonneesAppel, "Latitude");
            txtPatient_Escalier.DataBindings.Add("Text", _bseDonneesAppel, "Escalier");
            txtPatient_Etage.DataBindings.Add("Text", _bseDonneesAppel, "Etage");
            txtPatient_Bat.DataBindings.Add("Text", _bseDonneesAppel, "Batiment");
            txtPatient_Digicode.DataBindings.Add("Text", _bseDonneesAppel, "Digicode");
            txtPatient_Internom.DataBindings.Add("Text", _bseDonneesAppel, "InterNom");
            txtPatient_Porte.DataBindings.Add("Text", _bseDonneesAppel, "Porte");
            txtPatient_Commentaire.DataBindings.Add("Text", _bseDonneesAppel, "TexteSup");
            txtPatient_AdmBatiment.DataBindings.Add("Text", _bseDonneesAppel, "Adm_Batiment");
            txtPatient_AdmNum.DataBindings.Add("Text", _bseDonneesAppel, "Adm_NumeroDansRue");
            txtPatient_AdmRue.DataBindings.Add("Text", _bseDonneesAppel, "Adm_Rue");
            txtPatient_AdmCodePostal.DataBindings.Add("Text", _bseDonneesAppel, "Adm_CodePostal");
            txtPatient_AdmCommune.DataBindings.Add("Text", _bseDonneesAppel, "Adm_Commune");
            // txtAppel_Commentaire.Text = row["CommentaireTransmis"].ToString() + "//" + row["ComplementInfo"].ToString();
            txtAppel_Commentaire.DataBindings.Add("Text", _bseDonneesAppel, "CommentaireTransmis");
            txtBilan_Reglement.DataBindings.Add("Text", _bseDonneesAppel, "Reglement");
            txtBilan_Hono.DataBindings.Add("Text", _bseDonneesAppel, "Hono");
            txtBilan_Diag1.DataBindings.Add("Text", _bseDonneesAppel, "Diag1");
            txtBilan_Diag2.DataBindings.Add("Text", _bseDonneesAppel, "Diag2");
            txtBilan_Devenir.DataBindings.Add("Text", _bseDonneesAppel, "Devenir");
            txtBilan_Pec.DataBindings.Add("Text", _bseDonneesAppel, "libCisp");
            txtBilan_Provenance.DataBindings.Add("Text", _bseDonneesAppel, "PriseEnChargePatient");
            txtBilan_Traitement.DataBindings.Add("Text", _bseDonneesAppel, "TraitementLibre");
            txtBilan_DestinatairesMt.DataBindings.Add("Text", _bseDonneesAppel, "ListeIndexMt");
            txtBilan_DestinatairesSe.DataBindings.Add("Text", _bseDonneesAppel, "ListeIndexServiceExt");
            txtBilan_Commentaire.DataBindings.Add("Text", _bseDonneesAppel, "CommentaireLibre");

            //if (row["FaxType"].ToString() == "0")
            //{
            //    txtBilan_Fax.Text = "Evaluation avec le/la patient(e)pour soins à domicile";
            //    txtBilan_Fax.Tag = row["FaxCommentaire"].ToString();
            //}
            //else if (row["FaxType"].ToString() == "1")
            //{
            //    txtBilan_Fax.Text = "Le/la patient(e) a été hospitalisé(e)";
            //    txtBilan_Fax.Tag = row["FaxCommentaire"].ToString();
            //}
            //else if (row["FaxType"].ToString() == "2")
            //{
            //    txtBilan_Fax.Text = "Le/la patient(e) est décédé(e)";
            //    txtBilan_Fax.Tag = row["FaxCommentaire"].ToString();
            //}
            //else
            //    txtBilan_Fax.Text = "";
            //toolTip1.SetToolTip(txtBilan_Fax, row["FaxCommentaire"].ToString());
            Binding bdgDateNaiss = new Binding("Text", _bseDonneesAppel, "DateNaissance");
            bdgDateNaiss.Format += new ConvertEventHandler(Format_BindingDate);
            txtDateNaissance.DataBindings.Add(bdgDateNaiss);

            TxtCP.DataBindings.Add("Text", _bseDonneesAppel, "CodePostal");
            //txtPatient_Suivi.Text = row["SuiviPatient"].ToString().Replace("\r\n", "|¤").Replace("\n", "|¤").Replace("|¤", "\r\n");
            txtPatient_Suivi.DataBindings.Add("Text", _bseDonneesAppel, "SuiviPatient");


            Binding bdgDeces = new Binding("Checked", _bseDonneesAppel, "Deces");
            bdgDeces.Format += new ConvertEventHandler(Format_BindingCheck);
            chkBilan_Deces.DataBindings.Add(bdgDeces);
        }

        private void Format_BindingDate(Object sender, ConvertEventArgs e)
        {
            if (e.Value.ToString() != string.Empty)
                e.Value = DateTime.Parse(e.Value.ToString()).ToString("dd/MM/yyyy");
            else
                e.Value = string.Empty;
        }

        private void Format_BindingCheck(Object sender, ConvertEventArgs e)
        {
            if (e.Value.ToString() == "1")
                e.Value = true;
            else
                e.Value = false;
        }

        private void picSaveFiche_Click(object sender, EventArgs e)
        {

        }
    }
}
