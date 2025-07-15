using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ImportSosGeneve
{
    public partial class frmRelances : Form
    {
        SosMedecins.SmartRapport.DAL.dstRelances _dstRelances;

        public frmRelances()
        {
            InitializeComponent();
            //
            foreach ( string z_Imp in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                cbxImprimantes.Items.Add(z_Imp);
            }
            System.Drawing.Printing.PrinterSettings z_prnDefault = new System.Drawing.Printing.PrinterSettings();
            cbxImprimantes.SelectedItem = SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.StrInvoicePrinter;

            _dstRelances = new SosMedecins.SmartRapport.DAL.dstRelances();

            ChargementDonnees();

            this.Cursor = Cursors.Default;
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkIncident_CheckedChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (chkIncident.Checked)
            {
                lblNumFacture.Enabled = true;
                txtNumFacture.Enabled = true;
                lblDateRelance.Enabled = true;
                dtpDateRelance.Enabled = true;                
                lblFactureUnique.Enabled = true;
                txtNumFactureUnique.Enabled = true;
                btnActualiseFactureUnique.Enabled = true;
                btnFactureActualise.Enabled = true;

                ChargementDonnees();
            }
            else
            {
                lblNumFacture.Enabled = false;
                txtNumFacture.Enabled = false;
                lblDateRelance.Enabled = false;
                dtpDateRelance.Enabled = false;                
                lblFactureUnique.Enabled = false;
                txtNumFactureUnique.Enabled = false;
                btnActualiseFactureUnique.Enabled = false;
                btnFactureActualise.Enabled = false;

                ChargementDonnees();
            }
            this.Cursor = Cursors.Default;
        }

        private void btnActualise_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            ChargementDonnees();
            this.Cursor = Cursors.Default;
        }

        private void btnActualiseFactureUnique_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            ChargementDonnees();
            this.Cursor = Cursors.Default;
        }

        private void btnValider_Click(object sender, EventArgs e)
        {
            if (cbxImprimantes.SelectedItem == null)
            {
                MessageBox.Show("Vous devez selectionner une imprimante !", "Erreur imprimante", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                this.Cursor = Cursors.WaitCursor;
                string z_strSql;
                // bloque les choix
                btnValider.Enabled = false;
                cbxImprimantes.Enabled = false;
                chkApercu.Enabled = false;
                lblImprimante.Enabled = false;
                lblNumFacture.Enabled = false;
                txtNumFacture.Enabled = false;
                lblDateRelance.Enabled = false;
                dtpDateRelance.Enabled = false;
                chkIncident.Enabled = false;
                lblFactureUnique.Enabled = false;
                txtNumFactureUnique.Enabled = false;
                btnActualiseFactureUnique.Enabled = false;
                btnFactureActualise.Enabled = false;
                //
                lblProgression.Text = "Préparation des factures.";
                prgTache.Minimum = 0;
                prgTache.Maximum = _dstRelances.Factures.Rows.Count;
                prgTache.Value = 0;
                
                //********************Domi 01/04/2011 VOIR LA GESTION DE CES DATES ICI ******************************************

                //Mettre date à l'impression des cessions de créances (Facture_status, nvx champs)

                /* foreach (DataRow z_drw in _dstRelances.Factures.Rows)
                 {
                     if (chkIncident.Checked)
                     {
                         if (z_drw[_dstRelances.Factures.DateContentieuxColumn.ColumnName] == DBNull.Value)
                         {
                             z_drw[_dstRelances.Factures.ModeColumn.ColumnName] = 1;
                         }
                         else
                         {
                             z_drw[_dstRelances.Factures.ModeColumn.ColumnName] = 2;
                         }
                     }
                     else
                     {
                         if (z_drw[_dstRelances.Factures.DateRelanceColumn.ColumnName] == DBNull.Value)
                         {
                             z_drw[_dstRelances.Factures.ModeColumn.ColumnName] = 1;
                         }
                         else
                         {
                             z_drw[_dstRelances.Factures.ModeColumn.ColumnName] = 2;
                         }
                     }
                     prgTache.Value += 1;
                 
                 }*/
                // Mise a jour de la base de données
                lblProgression.Text = "Mise à jour de la base de données.";
                prgTache.Minimum = 0;
                prgTache.Maximum = _dstRelances.Factures.Rows.Count;
                prgTache.Value = 0;

                Boolean z_blnConnect = SosMedecins.SmartRapport.DAL.Variables.ConnexionBase.OpenBDD();
                try
                {
                    SosMedecins.SmartRapport.DAL.Variables.ConnexionBase.BeginTransaction();
                    foreach (DataRow z_drw in _dstRelances.Factures.Rows)
                    {
                        if (!chkIncident.Checked)
                        {
                            z_strSql = "UPDATE facture_status SET ";

                            //En fonction de ce que l'on imprime
                            //Si ce sont les cessions de créances, on met la date du jour pour l'envoi des cessions
                            if (RB0.Checked)
                            {
                                z_strSql += " CessionEnvoi = " + SosMedecins.Connexion.FormatSql.Format_Date(DateTime.Now.ToString());
                                //z_strSql += " CessionEnvoi = '02.05.2011'" ;
                            }
                            else  //Sinon c'est la date d'envoi des rappels (Relance 100%P, 10%P, 100% assAR, 100%P AR)
                            {
                                z_strSql += " FacDate1Rappel = " + SosMedecins.Connexion.FormatSql.Format_Date(DateTime.Now.ToString());
                            }

                            z_strSql += " Where NFacture = " + SosMedecins.Connexion.FormatSql.Format_Nombre(z_drw[_dstRelances.Factures.NFactureColumn.ColumnName].ToString());

                            
                            SosMedecins.SmartRapport.DAL.Variables.ConnexionBase.ExecuteSqlSansRetour(z_strSql);
                            //******************************************************************

                            //On signal au médecin de faire signer une cession de créance la prochaine fois...

                            if (RB0.Checked)
                            {
                                z_strSql = "SELECT IdPatient FROM patient_remarque WHERE IdPatient = " + SosMedecins.Connexion.FormatSql.Format_Nombre(z_drw[_dstRelances.Factures.IdPatientColumn.ColumnName].ToString());

                                if (SosMedecins.SmartRapport.DAL.Variables.ConnexionBase.ExecuteScalar(z_strSql) == null)
                                {
                                    // insert
                                    z_strSql = "INSERT INTO patient_remarque (IdPatient, Encaisse, Export, Archive, Cession, DateValidite, IdUtilisateur )";
                                    z_strSql += " VALUES ( ";
                                    z_strSql += SosMedecins.Connexion.FormatSql.Format_Nombre(z_drw[_dstRelances.Factures.IdPatientColumn.ColumnName].ToString());
                                    z_strSql += ", 0";
                                    z_strSql += ", 0";
                                    z_strSql += ", 0";
                                    z_strSql += ", 1";
                                    z_strSql += ", " + SosMedecins.Connexion.FormatSql.Format_Date(DateTime.Now.ToString("dd.MM.yyyy"));
                                    z_strSql += ", " + SosMedecins.Connexion.FormatSql.Format_String(SosMedecins.SmartRapport.GestionApplication.VariablesApplicatives.Utilisateurs.Identifiant);
                                    z_strSql += " )";
                                    SosMedecins.SmartRapport.DAL.Variables.ConnexionBase.ExecuteSqlSansRetour(z_strSql);
                                }
                                else
                                {
                                    // Update
                                    z_strSql = "UPDATE patient_remarque SET ";
                                    z_strSql += "Encaisse = 0 ";
                                    z_strSql += ", Export = 0 ";
                                    z_strSql += ", Archive = 0 ";
                                    z_strSql += ", Cession = 1 ";
                                    z_strSql += ", DateValidite = " + SosMedecins.Connexion.FormatSql.Format_Date(DateTime.Now.ToString("dd.MM.yyyy"));
                                    z_strSql += ", IdUtilisateur = " + SosMedecins.Connexion.FormatSql.Format_String(SosMedecins.SmartRapport.GestionApplication.VariablesApplicatives.Utilisateurs.Identifiant);
                                    z_strSql += " WHERE IdPatient = " + SosMedecins.Connexion.FormatSql.Format_Nombre(z_drw[_dstRelances.Factures.IdPatientColumn.ColumnName].ToString());
                                    SosMedecins.SmartRapport.DAL.Variables.ConnexionBase.ExecuteSqlSansRetour(z_strSql);
                                }
                            }
                        }
                        prgTache.Value += 1;
                    }
                    SosMedecins.SmartRapport.DAL.Variables.ConnexionBase.Commit();
                }
                catch (Exception ex)
                {
                    this.Cursor = Cursors.Default;
                    SosMedecins.SmartRapport.DAL.Variables.ConnexionBase.RollBack();
                    throw new Exception(ex.Message);
                    //MessageBox.Show(ex.Message.ToString());
                }
                finally
                {
                    if (z_blnConnect)
                    {
                        SosMedecins.SmartRapport.DAL.Variables.ConnexionBase.CloseBDD();
                    }
                }       
                // Preparation du rapport

                //*******Domi 01/04/2011*************************
                //en fonction de ce que l'on veut imprimer.....
                if (RB0.Checked)
                {
                    //impression des cessions de créances
                    SosMedecins.SmartRapport.EtatsCrystal.CessionDeCreance z_rpt = new SosMedecins.SmartRapport.EtatsCrystal.CessionDeCreance();
                    z_rpt.PrintOptions.PrinterName = String.Format("{0}", cbxImprimantes.SelectedItem);
                    z_rpt.SetDataSource(_dstRelances);

                    // Impression direct
                    if (chkApercu.Checked)
                    {
                        // Affichage Ecran
                        SosMedecins.SmartRapport.EtatsCrystal.frmCrystalReportViewer z_frm = new SosMedecins.SmartRapport.EtatsCrystal.frmCrystalReportViewer();
                        z_frm.AfficheParametrageImprimante = false;
                        z_frm.ReportSource = z_rpt;

                        z_frm.PrinterName = String.Format("{0}", cbxImprimantes.SelectedItem);
                        z_frm.ShowDialog();
                        z_frm.Dispose();
                    }
                    else
                    {
                        z_rpt.PrintToPrinter(1, false, 0, 0);
                    }
                    z_rpt.Dispose();

                }
                else
                {
                    if (RB4.Checked)
                    {
                        //impression des relances Assurances AR
                        SosMedecins.SmartRapport.EtatsCrystal.RelancesAssuranceRecom z_rpt = new SosMedecins.SmartRapport.EtatsCrystal.RelancesAssuranceRecom();
                        z_rpt.PrintOptions.PrinterName = String.Format("{0}", cbxImprimantes.SelectedItem);
                        z_rpt.SetDataSource(_dstRelances);

                        // Impression direct
                        if (chkApercu.Checked)
                        {
                            // Affichage Ecran
                            SosMedecins.SmartRapport.EtatsCrystal.frmCrystalReportViewer z_frm = new SosMedecins.SmartRapport.EtatsCrystal.frmCrystalReportViewer();
                            z_frm.AfficheParametrageImprimante = false;
                            z_frm.ReportSource = z_rpt;

                            z_frm.PrinterName = String.Format("{0}", cbxImprimantes.SelectedItem);
                            z_frm.ShowDialog();
                            z_frm.Dispose();
                        }
                        else
                        {
                            z_rpt.PrintToPrinter(1, false, 0, 0);
                        }
                        z_rpt.Dispose();


                    }
                    else //Dans tout les autres cas
                    {
                        //impression des relances 10% Patient, Franchises, Indélicats 100% AR, cession non reçu 100% Patient AR                                
                        SosMedecins.SmartRapport.EtatsCrystal.Relances z_rpt = new SosMedecins.SmartRapport.EtatsCrystal.Relances();
                        z_rpt.PrintOptions.PrinterName = String.Format("{0}", cbxImprimantes.SelectedItem);
                        z_rpt.SetDataSource(_dstRelances);

                        // Impression direct
                        if (chkApercu.Checked)
                        {
                            // Affichage Ecran
                            SosMedecins.SmartRapport.EtatsCrystal.frmCrystalReportViewer z_frm = new SosMedecins.SmartRapport.EtatsCrystal.frmCrystalReportViewer();
                            z_frm.AfficheParametrageImprimante = false;
                            z_frm.ReportSource = z_rpt;

                            z_frm.PrinterName = String.Format("{0}", cbxImprimantes.SelectedItem);
                            z_frm.ShowDialog();
                            z_frm.Dispose();
                        }
                        else
                        {
                            z_rpt.PrintToPrinter(1, false, 0, 0);
                        }
                        z_rpt.Dispose();

                    }

                }

                // bloque les choix
                btnValider.Enabled = true;
                cbxImprimantes.Enabled = true;
                chkApercu.Enabled = true;
                lblImprimante.Enabled = true;
                lblNumFacture.Enabled = true;
                txtNumFacture.Enabled = true;
                lblDateRelance.Enabled = true;
                dtpDateRelance.Enabled = true;
                chkIncident.Enabled = true;
                lblFactureUnique.Enabled = true;
                txtNumFactureUnique.Enabled = true;
                btnActualiseFactureUnique.Enabled = true;
                btnFactureActualise.Enabled = true;
                //
                this.Cursor = Cursors.Default;               
            }
        }

        private void ChargementDonnees()
        {            
            _dstRelances.Factures.Clear();
            //
            DateTime z_dteMaximum = DateTime.Now;
            DateTime dteMinimum = DateTime.Now;
            z_dteMaximum = z_dteMaximum.AddMonths(-4);  //la date system -4 mois
            dteMinimum = dteMinimum.AddYears(-4);    //On enleve 4 ans
            dteMinimum = dteMinimum.AddMonths(-6);   //On enleve encore 6 mois  => donc -4ans et 6mois
            
            string z_strSql;
            z_strSql = "SELECT facture.NFacture as NFacture";
            z_strSql += ", facture.DateCreation as DateCreation";
            z_strSql += ", TotalFacture as TotalFacture";
            z_strSql += ", Solde as Solde";
            z_strSql += ", AdresseDestinataire as AdresseDestinataire";
            z_strSql += ", tableactes.CodeIntervenant as CodeIntervenant";
            z_strSql += ", FacDate1Rappel as DateRelance";
            z_strSql += ", FacDateContentieux as DateContentieux";
            
            if ((RB1.Checked == true) || (RB2.Checked == true))
            {
                z_strSql += ", 1 as Mode";        //Mode = type de document => sert pour afficher ou non des champs dans l'etat ici 1=pas recommandé
            }
            else
            {
                if ((RB3.Checked == true) || (RB5.Checked == true))
                {
                    z_strSql += ", 2 as Mode";    //Mode = type de document => sert pour afficher ou non des champs dans l'etat ici 2= Recommandé
                }
                else
                {
                    z_strSql += ", 0 as Mode";
                }

            }    
            
            z_strSql += ", tableconsultations.IndicePatient as IdPatient";

            z_strSql += ", TableMedecin.Nom as Medecin, Tableactes.DSL as DateIntervention";

            z_strSql += ", tablepersonne.Nom as NomPatient, tablepersonne.Prenom as PrenomPatient";
           
            
            z_strSql += " FROM ((((((tableconsultations INNER JOIN (facture INNER JOIN factureconsultation ON facture.NFacture = factureconsultation.NFacture)";
            z_strSql += " ON tableconsultations.NConsultation = factureconsultation.NConsultation)";
            z_strSql += " INNER JOIN tableactes ON tableconsultations.CodeAppel = tableactes.Num)";
            z_strSql += " INNER JOIN tablepatient ON tableconsultations.IndicePatient = tablepatient.IdPatient)";
            z_strSql += " INNER JOIN tablepersonne ON tablepatient.Idpersonne = tablepersonne.idpersonne)";
            z_strSql += " INNER JOIN Tablemedecin ON tableactes.CodeIntervenant = TableMedecin.CodeIntervenant)";
            z_strSql += " INNER JOIN facture_status ON facture.NFacture = facture_status.NFacture)";
            z_strSql += " LEFT JOIN facture_arrangement ON facture.NFacture = facture_arrangement.NFacture";
            z_strSql += " WHERE FacDateAnnulee is null";
            z_strSql += " and FacDateEncaissee is null";
            z_strSql += " and typedestinataire = 0";
            z_strSql += " and facture_arrangement.DateCreation is null";
            z_strSql += " and facture_status.FacDateCession is null";

            if (chkIncident.Checked)
            {
                if (txtNumFacture.Text.Length > 0)
                {
                    z_strSql += " and facture.NFacture > " + txtNumFacture.Text;
                }
                //z_strSql += " and (FacDate1Rappel = " + SosMedecins.Connexion.FormatSql.Format_Date(dtpDateRelance.Value.ToString());                 //**** Domi 05/04/2011
                //z_strSql += " or FacDateContentieux = " + SosMedecins.Connexion.FormatSql.Format_Date(dtpDateRelance.Value.ToString()) + ")";             

                if (txtNumFactureUnique.Text.Length > 0)
                {
                    z_strSql += " and facture.NFacture = " + txtNumFactureUnique.Text;
                }
            }
            else
            {
                z_strSql += " and TotalFacture = Solde";
                z_strSql += " and Solde > 0";
                //z_strSql += " and FacDateContentieux is null";   //***** Domi 05/04/2011
            }
            
            //*************** Domi 01/04/2011
            if (RB0.Checked)      //On veut selectionner les cessions de créances
            {
                z_strSql += " and facture_status.CessionRecu is null ";
                z_strSql += " and facture_status.CessionEnvoi is null ";
                z_strSql += " and facture_status.FactFranchise = 0";
                z_strSql += " and facture_status.RenvFact10p = 0";
                z_strSql += " and facture_status.PatientIndelicat = 0";
                z_strSql += " and facture.DateCreation between '" + dteMinimum.ToString("yyyyMMdd") + "' and '" + z_dteMaximum.ToString("yyyyMMdd") + "'";   //ici le 01 pour le 1er jour du mois
            }

            if (RB1.Checked)      //On veut selectionner les relances 10% patient
            {
                z_strSql += " and facture_status.RenvFact10p = 1";
                dteMinimum = dteMinimum.AddMonths(-2);
                z_strSql += " and facture.DateCreation between '" + dteMinimum.ToString("yyyyMMdd") + "' and '" + z_dteMaximum.ToString("yyyyMMdd") + "'";   //ici le 01 pour le 1er jour du mois
            }

            if (RB2.Checked)    //On veut selectionner les patients en franchise
            {
                z_strSql += " and facture_status.FactFranchise = 1";
                dteMinimum = dteMinimum.AddMonths(-2);
                z_strSql += " and facture.DateCreation between '" + dteMinimum.ToString("yyyyMMdd") + "' and '" + z_dteMaximum.ToString("yyyyMMdd") + "'";   //ici le 01 pour le 1er jour du mois
            }

            if (RB3.Checked)    //On veut selectionner les relances 100% patient indélicats en Recommandé
            {
                z_strSql += " and facture_status.PatientIndelicat = 1";
                dteMinimum = dteMinimum.AddMonths(-2);
                z_strSql += " and facture.DateCreation between '" + dteMinimum.ToString("yyyyMMdd") + "' and '" + z_dteMaximum.ToString("yyyyMMdd") + "'";   //ici le 01 pour le 1er jour du mois
            }

            if (RB4.Checked)    //On veut selectionner les relances 100% Assurance en recommandé
            {
                z_strSql += " and facture_status.CessionRecu is not null and (facture_status.RenvFact10p = 0 or facture_status.RenvFact10p is null) and (facture_status.FactFranchise = 0 or facture_status.FactFranchise is null) and (facture_status.PatientIndelicat = 0 or facture_status.PatientIndelicat is null)";
                dteMinimum = dteMinimum.AddMonths(-2);
                z_dteMaximum = z_dteMaximum.AddMonths(-2);  //on enlève 2 mois de plus => donc date systeme -6 mois
                z_strSql += " and facture.DateCreation between '" + dteMinimum.ToString("yyyyMMdd") + "' and '" + z_dteMaximum.ToString("yyyyMMdd") + "'";   //ici le 01 pour le 1er jour du mois
            }

            if (RB5.Checked)    //Cession non reçu, relance 100% patient recommandé
            {
                z_dteMaximum = z_dteMaximum.AddMonths(-2);  //on enlève 2 mois de plus => donc date systeme -6 mois
                z_strSql += "  and facture_status.CessionRecu is null and facture.DateCreation between '" + dteMinimum.ToString("yyyyMMdd") + "' and '" + z_dteMaximum.ToString("yyyyMMdd") + "'";               
            }

            //z_strSql += " and idpatient in (4592007, 4573161)";   //Pour test
            z_strSql += " order by AdresseDestinataire, tableconsultations.IndicePatient";
            

            Boolean z_blnConnect = SosMedecins.SmartRapport.DAL.Variables.ConnexionBase.OpenBDD();
            try
            {
                SosMedecins.SmartRapport.DAL.Variables.ConnexionBase.ExecuteSql(_dstRelances.Factures, z_strSql);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
               // MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                if (z_blnConnect)
                {
                    SosMedecins.SmartRapport.DAL.Variables.ConnexionBase.CloseBDD();
                }
            }
            LblTexte.Text = "Vous allez envoyer " + _dstRelances.Factures.Rows.Count.ToString() + " relances.";
        }


        private Boolean SoldeNumberEntered;
        private void txtSoldeMini_KeyDown(object sender, KeyEventArgs e)
        {
            // Initialize the flag to false.
            SoldeNumberEntered = false;

            // Determine whether the keystroke is a number from the top of the keyboard.
            if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9)
            {
                // Determine whether the keystroke is a number from the keypad.
                if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)
                {
                    // Determine whether the keystroke is a backspace.
                    if (e.KeyCode != Keys.Back)
                    {
                        // A non-numerical keystroke was pressed.
                        // Set the flag to true and evaluate in KeyPress event.
                        SoldeNumberEntered = true;
                    }
                }
            }
        }

        private void txtSoldeMini_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check for the flag being set in the KeyDown event.
            if (SoldeNumberEntered == true)
            {
                // Stop the character from being entered into the control since it is non-numerical.
                e.Handled = true;
            }
        }

        private void RB0_Click(object sender, EventArgs e)
        {
            //On relance la requette de sélection
            this.Cursor = Cursors.WaitCursor;
            ChargementDonnees();
            this.Cursor = Cursors.Default;
        }


        private void RB1_Click(object sender, EventArgs e)
        {
            //On relance la requette de sélection
            this.Cursor = Cursors.WaitCursor;
            ChargementDonnees();
            this.Cursor = Cursors.Default;
        }

        private void RB2_Click(object sender, EventArgs e)
        {
            //On relance la requette de sélection
            this.Cursor = Cursors.WaitCursor;
            ChargementDonnees();
            this.Cursor = Cursors.Default;
        }

        private void RB3_Click(object sender, EventArgs e)
        {
            //On relance la requette de sélection
            this.Cursor = Cursors.WaitCursor;
            ChargementDonnees();
            this.Cursor = Cursors.Default;
        }

        private void RB4_Click(object sender, EventArgs e)
        {
            //On relance la requette de sélection
            this.Cursor = Cursors.WaitCursor;
            ChargementDonnees();
            this.Cursor = Cursors.Default;
        }

        private void RB5_Click(object sender, EventArgs e)
        {
            //On relance la requette de sélection
            this.Cursor = Cursors.WaitCursor;
            ChargementDonnees();
            this.Cursor = Cursors.Default;
        }

      

       
    }
}