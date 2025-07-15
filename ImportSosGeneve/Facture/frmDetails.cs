using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SosMedecins.SmartRapport.EtatsCrystal;
using SosMedecins.SmartRapport.GestionApplication;
using System.Threading;
using SosMedecins.SmartRapport.DAL;
using System.Text.RegularExpressions;

namespace ImportSosGeneve.Facture
{
    public partial class frmDetails : Form
    {       
        private BindingSource _bseFacture = new BindingSource();
        private BindingSource _bseFactureEtat = new BindingSource();
        private dstElementsFacture _dstElementsFacture = new dstElementsFacture();

        private long _lngIdPatient;
        public enum Mode
        {
            Facture,
            Patient
        }

        public int VarIdpatient;

        public frmDetails(Mode p_enumMode, long p_lngIdentifiant)
        {
            // This call is required by the Windows Form Designer.
            InitializeComponent();
            // Recuperation des numero de patients
            TablePatient z_dalTablePatient = new TablePatient();
            TableFacture z_dalTableFacture = new TableFacture();
            TableFactureEtat z_dalTableFactureEtat = new TableFactureEtat();
            dstPatients z_dstPatients = new dstPatients();
            string z_strListeIdPatient = null;

            switch (p_enumMode)
            {
                case Mode.Facture:
                    // chargement
                    SosMedecins.SmartRapport.DAL.dstElementsFactureTableAdapters.factureTableAdapter z_tarFacture = new SosMedecins.SmartRapport.DAL.dstElementsFactureTableAdapters.factureTableAdapter();
                  
                    z_tarFacture.FillByNFacture(_dstElementsFacture.facture, p_lngIdentifiant);

                    SosMedecins.SmartRapport.DAL.dstElementsFactureTableAdapters.facture_etatsTableAdapter z_tarFactureEtat = new SosMedecins.SmartRapport.DAL.dstElementsFactureTableAdapters.facture_etatsTableAdapter();
                    z_tarFactureEtat.FillByNFacture(_dstElementsFacture.facture_etats, p_lngIdentifiant);

                  
                    _lngIdPatient = Convert.ToInt64(_dstElementsFacture.facture[0]["IndicePatient"]);
                    SosMedecins.SmartRapport.DAL.dstPatientsTableAdapters.tablepatientTableAdapter z_tarPatients = new SosMedecins.SmartRapport.DAL.dstPatientsTableAdapters.tablepatientTableAdapter();
                    z_tarPatients.Fill(z_dstPatients.tablepatient, _lngIdPatient);

                    break;
                case Mode.Patient:
                    _lngIdPatient = p_lngIdentifiant;
                    // chargement du dataset
                    z_strListeIdPatient = z_dalTablePatient.ListePatientIdentique(_lngIdPatient);
                    z_dalTableFacture.SelectFactureByIdPatient(_dstElementsFacture.facture, z_strListeIdPatient);
                    z_dalTableFactureEtat.SelectFactureByIdPatient(_dstElementsFacture.facture_etats, z_strListeIdPatient);

                    SosMedecins.SmartRapport.DAL.dstPatientsTableAdapters.tablepatientTableAdapter z_tarPatients1 = new SosMedecins.SmartRapport.DAL.dstPatientsTableAdapters.tablepatientTableAdapter();
                    z_tarPatients1.Fill(z_dstPatients.tablepatient, _lngIdPatient);
                    break;
            }

            SosMedecins.SmartRapport.DAL.dstElementsFactureTableAdapters.facture_typeTableAdapter z_tarFactureType = new SosMedecins.SmartRapport.DAL.dstElementsFactureTableAdapters.facture_typeTableAdapter();
            z_tarFactureType.Fill(_dstElementsFacture.facture_type);

            SosMedecins.SmartRapport.DAL.dstElementsFactureTableAdapters.facture_moyenTableAdapter z_tarFactureMoyen = new SosMedecins.SmartRapport.DAL.dstElementsFactureTableAdapters.facture_moyenTableAdapter();
            z_tarFactureMoyen.Fill(_dstElementsFacture.facture_moyen);

            SosMedecins.SmartRapport.DAL.dstElementsFactureTableAdapters.tableutilisateurTableAdapter z_tarUtilisateurs = new SosMedecins.SmartRapport.DAL.dstElementsFactureTableAdapters.tableutilisateurTableAdapter();
            z_tarUtilisateurs.Fill(_dstElementsFacture.tableutilisateur);
            
            // Recherche des informations Patients
            dstPatients.tablepatientRow z_drwPatient = (dstPatients.tablepatientRow)z_dstPatients.tablepatient.Rows[0];

            txtNom.Text = z_drwPatient.Nom;
            txtPrenom.Text = z_drwPatient.Prenom;
            if (z_drwPatient.IsDateNaissanceNull())
            {
                dtbDateNaissance.Value = null;
            }
            else
            {
                dtbDateNaissance.Value = z_drwPatient.DateNaissance.ToString();
            }
            // Add any initialization after the InitializeComponent() call.
            _bseFacture.DataSource = _dstElementsFacture;
            _bseFacture.DataMember = _dstElementsFacture.facture.TableName;
           
            _bseFactureEtat.DataSource = _bseFacture;
                      
            DataRelation  relation = new DataRelation("facture_facture_etats", _dstElementsFacture.Tables["facture_etats"].Columns["NFacture"], _dstElementsFacture.Tables["facture"].Columns["NFacture"]);
           
            _bseFactureEtat.DataMember = relation.RelationName;
            //
            dgvListe.AutoGenerateColumns = false;
            dgvListe.Columns.AddRange(ColumnDataGridList());
            dgvListe.DataSource = _bseFacture;

            if ((_bseFacture.Count > 0))
            {
                // tri
                dgvListe.Sort(dgvListe.Columns[_dstElementsFacture.facture.DateCreationColumn.ColumnName], System.ComponentModel.ListSortDirection.Ascending);
                // Somme              
                txtSolde.Text= String.Format("{0:0.00}", _dstElementsFacture.facture.Compute("SUM(Solde)", ""));
                txtTotal.Text = String.Format("{0:0.00}", _dstElementsFacture.facture.Compute("SUM(TotalFacture)", ""));
                lblNbFacture.Text = _dstElementsFacture.facture.Compute("COUNT(NFacture)", "").ToString() + " Factures.";
                btnModificationSolde.Enabled = true;
                btnModifierDetail.Enabled = true;
            }
            //         
            dgvDetail.AutoGenerateColumns = false;
            dgvDetail.Columns.AddRange(ColumnDataGridDetails());
            dgvDetail.DataSource = _bseFactureEtat;
          
            if ((_bseFactureEtat.IsSorted))
            {          
                dgvDetail.Sort(dgvDetail.Columns[_dstElementsFacture.facture_etats.DateEtatColumn.ColumnName], ListSortDirection.Ascending);                               
            }

            txtTotalDetail.DataBindings.Add("Text", _bseFacture, _dstElementsFacture.facture.TotalFactureColumn.ColumnName, true, DataSourceUpdateMode.Never, null, "C");
            txtSoldeDetail.DataBindings.Add("Text", _bseFacture, _dstElementsFacture.facture.SoldeColumn.ColumnName, true, DataSourceUpdateMode.Never, null, "C");
        }

        private void btnPaiement_Click(System.Object sender, System.EventArgs e)
        {
            dstElementsFacture.factureRow z_drv = (dstElementsFacture.factureRow)((DataRowView)_bseFacture.Current).Row;
            frmPaiement z_frmPaiement = new frmPaiement(_dstElementsFacture, z_drv.NFacture);

            z_frmPaiement.Mode = Structures.ModeAccess.Ajout;

            z_frmPaiement.ShowDialog();

            z_frmPaiement.Dispose();
            z_frmPaiement = null;
        }

        private void btnDecompte_Click(System.Object sender, System.EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
           
            string DtDebut = dtDebut.ToString();
            string DtFin = dtFin.ToString();
            int destinataire=0;
            string Fin1 = "";//DtFin.ToString();
            string Debut1 = ""; // DtDebut.ToString();
           
            int i = 0;
            Regex modele = new Regex(@"\d{2}.\d{2}.\d{4}");
            MatchCollection résultat_dbt = modele.Matches(DtDebut);
            MatchCollection résultat_Fin = modele.Matches(DtFin);
                       
            Debut1 = résultat_dbt[i].Value;
            Fin1 = résultat_Fin[i].Value;
           
            TablePatient z_dalTablePatient = new TablePatient();
            string z_strListeIdPatient = null;
            z_strListeIdPatient = z_dalTablePatient.ListePatientIdentique(_lngIdPatient);
           
            if (cbDestinataire.Text=="Privé")
            { destinataire = 0; }
            else
                if (cbDestinataire.Text=="Assurance")
                { destinataire = 2; }
                else
                    if (cbDestinataire.Text=="Tiers")
                    { destinataire = 4; }

            Commun.ViewerDecompte viewerdecompte = new Commun.ViewerDecompte(z_strListeIdPatient.ToString(), Debut1, Fin1, destinataire, cbTypeFacture.Text);
           
            this.Cursor = Cursors.Default;
            
            viewerdecompte.ShowDialog();
           
            viewerdecompte.Dispose();
            viewerdecompte = null;
        }

        #region " Datagrid List "
        private DataGridViewColumn[] ColumnDataGridList()
        {
            // Origine active ou non
            System.Windows.Forms.DataGridViewTextBoxColumn z_clnNumeroConsultation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            z_clnNumeroConsultation.DataPropertyName = _dstElementsFacture.facture.NConsultationColumn.ColumnName;
            z_clnNumeroConsultation.HeaderText = "N° Consultation";
            z_clnNumeroConsultation.Name = _dstElementsFacture.facture.NConsultationColumn.ColumnName;
            z_clnNumeroConsultation.ReadOnly = true;
            z_clnNumeroConsultation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;

            System.Windows.Forms.DataGridViewTextBoxColumn z_clnDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            z_clnDate.DataPropertyName = _dstElementsFacture.facture.DateConsultationColumn.ColumnName;
            z_clnDate.HeaderText = "Date";
            z_clnDate.Name = _dstElementsFacture.facture.DAPColumn.ColumnName;
            z_clnDate.ReadOnly = true;
            z_clnDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            z_clnDate.DefaultCellStyle.Format = "dd/MM/yyyy";
            z_clnDate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            System.Windows.Forms.DataGridViewTextBoxColumn z_clnMedecin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            z_clnMedecin.DataPropertyName = _dstElementsFacture.facture.NomMedecinSosColumn.ColumnName;
            z_clnMedecin.HeaderText = "Médecin";
            z_clnMedecin.Name = _dstElementsFacture.facture.NomMedecinSosColumn.ColumnName;
            z_clnMedecin.ReadOnly = true;
            z_clnMedecin.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;

            System.Windows.Forms.DataGridViewTextBoxColumn z_clnNumeroFacture = new System.Windows.Forms.DataGridViewTextBoxColumn();
            z_clnNumeroFacture.DataPropertyName = _dstElementsFacture.facture.NFactureColumn.ColumnName;
            z_clnNumeroFacture.HeaderText = "N° Facture";
            z_clnNumeroFacture.Name = _dstElementsFacture.facture.NFactureColumn.ColumnName;
            z_clnNumeroFacture.ReadOnly = true;
            z_clnNumeroFacture.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;

            System.Windows.Forms.DataGridViewTextBoxColumn z_clnDateFacture = new System.Windows.Forms.DataGridViewTextBoxColumn();
            z_clnDateFacture.DataPropertyName = _dstElementsFacture.facture.DateCreationColumn.ColumnName;
            z_clnDateFacture.HeaderText = "Date Facture";
            z_clnDateFacture.Name = _dstElementsFacture.facture.DateCreationColumn.ColumnName;
            z_clnDateFacture.ReadOnly = true;
            z_clnDateFacture.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            z_clnDateFacture.DefaultCellStyle.Format = "dd/MM/yyyy";
            z_clnDateFacture.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            System.Windows.Forms.DataGridViewTextBoxColumn z_clnMontant = new System.Windows.Forms.DataGridViewTextBoxColumn();
            z_clnMontant.DataPropertyName = _dstElementsFacture.facture.TotalFactureColumn.ColumnName;
            z_clnMontant.HeaderText = "Montant";
            z_clnMontant.Name = _dstElementsFacture.facture.TotalFactureColumn.ColumnName;
            z_clnMontant.ReadOnly = true;
            z_clnMontant.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            z_clnMontant.DefaultCellStyle.Format = "#,##0.00";
            z_clnMontant.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            System.Windows.Forms.DataGridViewTextBoxColumn z_clnSolde = new System.Windows.Forms.DataGridViewTextBoxColumn();
            z_clnSolde.DataPropertyName = _dstElementsFacture.facture.SoldeColumn.ColumnName;
            z_clnSolde.HeaderText = "Solde";
            z_clnSolde.Name = _dstElementsFacture.facture.SoldeColumn.ColumnName;
            z_clnSolde.ReadOnly = true;
            z_clnSolde.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            z_clnSolde.DefaultCellStyle.Format = "#,##0.00";
            z_clnSolde.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //
            System.Windows.Forms.DataGridViewTextBoxColumn z_clnCommentaire = new System.Windows.Forms.DataGridViewTextBoxColumn();
            z_clnCommentaire.DataPropertyName = _dstElementsFacture.facture.CommentaireColumn.ColumnName;
            z_clnCommentaire.HeaderText = "Commentaires";
            z_clnCommentaire.Name = _dstElementsFacture.facture.CommentaireColumn.ColumnName;
            z_clnCommentaire.ReadOnly = true;
            z_clnCommentaire.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;

            return new System.Windows.Forms.DataGridViewColumn[] {
			z_clnNumeroConsultation,
			z_clnDate,
			z_clnMedecin,
			z_clnNumeroFacture,
			z_clnDateFacture,
			z_clnMontant,
			z_clnSolde,
			z_clnCommentaire
		};
        }
        #endregion

        #region " Datagrid Details "
        private DataGridViewColumn[] ColumnDataGridDetails()
        {
            // 
            System.Windows.Forms.DataGridViewTextBoxColumn z_clnOperation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            z_clnOperation.DataPropertyName = _dstElementsFacture.facture_etats.EtatColumn.ColumnName;
            z_clnOperation.HeaderText = "Opération";
            z_clnOperation.Name = _dstElementsFacture.facture_etats.EtatColumn.ColumnName;
            z_clnOperation.ReadOnly = true;
            z_clnOperation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            //
            System.Windows.Forms.DataGridViewTextBoxColumn z_clnDateOperation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            z_clnDateOperation.DataPropertyName = _dstElementsFacture.facture_etats.DateEtatColumn.ColumnName;
            z_clnDateOperation.HeaderText = "Date Opération";
            z_clnDateOperation.Name = _dstElementsFacture.facture_etats.DateEtatColumn.ColumnName;
            z_clnDateOperation.ReadOnly = true;
            z_clnDateOperation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            z_clnDateOperation.DefaultCellStyle.Format = "dd/MM/yyyy";
            z_clnDateOperation.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //
            System.Windows.Forms.DataGridViewTextBoxColumn z_clnDateEcriture = new System.Windows.Forms.DataGridViewTextBoxColumn();
            z_clnDateEcriture.DataPropertyName = _dstElementsFacture.facture_etats.DateOpColumn.ColumnName;
            z_clnDateEcriture.HeaderText = "Date Ecriture";
            z_clnDateEcriture.Name = _dstElementsFacture.facture_etats.DateOpColumn.ColumnName;
            z_clnDateEcriture.ReadOnly = true;
            z_clnDateEcriture.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            z_clnDateEcriture.DefaultCellStyle.Format = "dd/MM/yyyy";
            z_clnDateEcriture.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //
            System.Windows.Forms.DataGridViewTextBoxColumn z_clnMontant = new System.Windows.Forms.DataGridViewTextBoxColumn();
            z_clnMontant.DataPropertyName = _dstElementsFacture.facture_etats.MontantColumn.ColumnName;
            z_clnMontant.HeaderText = "Montant";
            z_clnMontant.Name = _dstElementsFacture.facture_etats.MontantColumn.ColumnName;
            z_clnMontant.ReadOnly = true;
            z_clnMontant.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            z_clnMontant.DefaultCellStyle.Format = "#,##0.00";
            z_clnMontant.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //
            System.Windows.Forms.DataGridViewTextBoxColumn z_clnMoyen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            z_clnMoyen.DataPropertyName = _dstElementsFacture.facture_etats.MoyenColumn.ColumnName;
            z_clnMoyen.HeaderText = "Moyen";
            z_clnMoyen.Name = _dstElementsFacture.facture_etats.MoyenColumn.ColumnName;
            z_clnMoyen.ReadOnly = true;
            z_clnMoyen.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            //
            System.Windows.Forms.DataGridViewTextBoxColumn z_clnIndication = new System.Windows.Forms.DataGridViewTextBoxColumn();
            z_clnIndication.DataPropertyName = _dstElementsFacture.facture_etats.Param2Column.ColumnName;
            z_clnIndication.HeaderText = "Indication";
            z_clnIndication.Name = _dstElementsFacture.facture_etats.Param2Column.ColumnName;
            z_clnIndication.ReadOnly = true;
            z_clnIndication.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            //
            System.Windows.Forms.DataGridViewTextBoxColumn z_clnUtilisateur = new System.Windows.Forms.DataGridViewTextBoxColumn();
            z_clnUtilisateur.DataPropertyName = _dstElementsFacture.facture_etats.CodeUtilisateurColumn.ColumnName;
            z_clnUtilisateur.HeaderText = "Initiateur";
            z_clnUtilisateur.Name = _dstElementsFacture.facture_etats.CodeUtilisateurColumn.ColumnName;
            z_clnUtilisateur.ReadOnly = true;
            z_clnUtilisateur.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            //
            System.Windows.Forms.DataGridViewTextBoxColumn z_clnCommentaire = new System.Windows.Forms.DataGridViewTextBoxColumn();
            z_clnCommentaire.DataPropertyName = _dstElementsFacture.facture_etats.CommentaireEtatColumn.ColumnName;
            z_clnCommentaire.HeaderText = "Commentaires";
            z_clnCommentaire.Name = _dstElementsFacture.facture_etats.CommentaireEtatColumn.ColumnName;
            z_clnCommentaire.ReadOnly = true;
            z_clnCommentaire.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;

            return new System.Windows.Forms.DataGridViewColumn[] {
			z_clnOperation,
			z_clnDateOperation,
			z_clnDateEcriture,
			z_clnMontant,
			z_clnMoyen,
			z_clnUtilisateur,
			z_clnCommentaire
		};
        }

        private void dgvDetail_CellFormatting(object sender, System.Windows.Forms.DataGridViewCellFormattingEventArgs e)
        {
            DataGridView z_dgv = (System.Windows.Forms.DataGridView)sender;
         
            if (z_dgv.Columns[e.ColumnIndex].Name == _dstElementsFacture.facture_etats.EtatColumn.ColumnName)
            {
                DataRowView currentRow = z_dgv.Rows[e.RowIndex].DataBoundItem as DataRowView;
                DataRow[] z_drwChildRows = currentRow.Row.GetChildRows("facture_etats_facture_type");
                e.Value = z_drwChildRows[0]["Libelle"].ToString();
            }
            else
                if (z_dgv.Columns[e.ColumnIndex].Name == _dstElementsFacture.facture_etats.MoyenColumn.ColumnName)
                {
                    DataRowView currentRow = z_dgv.Rows[e.RowIndex].DataBoundItem as DataRowView;
                    DataRow[] z_drwChildRows = currentRow.Row.GetChildRows("facture_etats_facture_moyen");
                    e.Value = z_drwChildRows[0]["Libelle"].ToString();
                }
                else
                    if (z_dgv.Columns[e.ColumnIndex].Name == _dstElementsFacture.facture_etats.CodeUtilisateurColumn.ColumnName)
                    {
                        DataRowView currentRow = z_dgv.Rows[e.RowIndex].DataBoundItem as DataRowView;
                        DataRow[] z_drwChildRows = currentRow.Row.GetChildRows("facture_etats_tableutilisateur");
                        e.Value = z_drwChildRows[0]["Nom"].ToString();
                    }

        }

        private void dgvDetail_SelectionChanged(object sender, System.EventArgs e)
        {
            dstElementsFacture.facture_etatsRow z_drv =  (dstElementsFacture.facture_etatsRow)((DataRowView)_bseFactureEtat.Current).Row;

            if ((_dstElementsFacture.facture_type.FindByEtat(z_drv.Etat).Paiement == true))
            {
                btnModifierDetail.Enabled = true;
                btnSupprimerDetail.Enabled = true;
            }
            else
            {
                btnModifierDetail.Enabled = false;
                btnSupprimerDetail.Enabled = false;
            }
        }
        #endregion

        private void btnModifierDetail_Click(System.Object sender, System.EventArgs e)
        {
            Modification();
        }

        private void btnSupprimerDetail_Click(System.Object sender, System.EventArgs e)
        {
            dstElementsFacture.facture_etatsRow z_drv = (dstElementsFacture.facture_etatsRow)((DataRowView)_bseFactureEtat.Current).Row;

            string z_strTexte = "Etes-vous sur de vouloir supprimer la ligne d'un montant de " + z_drv.Montant;


            if ((MessageBox.Show(z_strTexte, "Suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes))
            {
                frmPaiement z_frmPaiement = new frmPaiement(_dstElementsFacture, z_drv.Compteur);

                z_frmPaiement.Mode = Structures.ModeAccess.Suppression;

                z_frmPaiement.SupprimeEtat(z_drv.Compteur);

                z_frmPaiement.Dispose();
                z_frmPaiement = null;
                //
                txtSolde.Text = String.Format("{0:0.00}",_dstElementsFacture.facture.Compute("SUM(Solde)", ""));
            }

            if (VariablesApplicatives.Utilisateurs.Identifiant.ToString() == "A001")
                mouchard.evenement("Suppression d'un paiement n° " + z_drv.Compteur.ToString() + "d'un montant de " + z_drv.Montant.ToString() + " pour la facture...n° " + z_drv.NFacture.ToString(), VariablesApplicatives.Utilisateurs.NomUtilisateur.ToString());  //log

        }

        private void Modification()
        {
            dstElementsFacture.facture_etatsRow z_drv = (dstElementsFacture.facture_etatsRow)((DataRowView)_bseFactureEtat.Current).Row;

            if ((_dstElementsFacture.facture_type.FindByEtat(z_drv.Etat).Paiement == true))
            {
                frmPaiement z_frmPaiement = new frmPaiement(_dstElementsFacture, z_drv.Compteur);

                z_frmPaiement.Mode = Structures.ModeAccess.Modification;

                z_frmPaiement.ShowDialog();

                z_frmPaiement.Dispose();
                z_frmPaiement = null;
                //
                txtSolde.Text = String.Format("{0:0.00}", _dstElementsFacture.facture.Compute("SUM(Solde)", ""));
            }
        }


        private void btnModificationSolde_Click(System.Object sender, System.EventArgs e)
        {
            dstElementsFacture.factureRow z_drv = (dstElementsFacture.factureRow)((DataRowView)_bseFacture.Current).Row;
            frmPaiement z_frmPaiement = new frmPaiement(_dstElementsFacture, z_drv.NFacture);

            frmModificationSolde z_frmModificationSolde = new frmModificationSolde(_dstElementsFacture, z_drv.NFacture);
            if ((z_frmModificationSolde.ShowDialog() == System.Windows.Forms.DialogResult.OK))
            {
                //
                txtSolde.Text =String.Format("{0:0.0}" ,_dstElementsFacture.facture.Compute("SUM(Solde)", ""));              
            }

            z_frmModificationSolde.Dispose();
            z_frmModificationSolde = null;

            if (VariablesApplicatives.Utilisateurs.Identifiant.ToString() == "A001")
                mouchard.evenement("Modification du solde de la facture n° " + z_drv.NFacture.ToString() + ". Nouveau solde: " + z_drv.Solde.ToString(), VariablesApplicatives.Utilisateurs.NomUtilisateur.ToString());  //log

        }



        private void dgvListe_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {           
            string id2 = dgvListe.SelectedRows[0].Cells[0].Value.ToString();
          
            DataGridViewSelectedRowCollection rows = dgvListe.SelectedRows;
            foreach (DataGridViewRow row in rows)
            {
                DataRow myRow = (row.DataBoundItem as DataRowView).Row;

                frmGeneral frm = new frmGeneral();
                FacturePatient facturepatient = new FacturePatient(myRow);

                facturepatient.Show();
            }
        }

        private void dgvDetail_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Modification();
        }

        private void bQuitter_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}