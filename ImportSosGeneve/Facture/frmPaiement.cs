using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Collections;
using System.Diagnostics;
using SosMedecins.SmartRapport.DAL;
using SosMedecins.Connexion;
using SosMedecins.SmartRapport.GestionApplication;

namespace ImportSosGeneve.Facture
{
    public partial class frmPaiement : Form
    {
        private dstElementsFacture.factureRow _drwFacture;
        private dstElementsFacture.facture_etatsRow _drwFactureEtat;

        private dstElementsFacture _dstElementsFacture;

        private long _lngIdentifiant;
        private ImportSosGeneve.Facture.Structures.ModeAccess _stuMode;
        public ImportSosGeneve.Facture.Structures.ModeAccess Mode
        {
            get { return _stuMode; }
            set { _stuMode = value; }
        }

        public frmPaiement(dstElementsFacture p_dstElementsFacture, long p_lngIdentifiant)
        {
            Load += frmPaiement_Load;
            // This call is required by the Windows Form Designer.
            InitializeComponent();

            // Add any initialization after the InitializeComponent() call.
            DataView z_dvw = null;
            _dstElementsFacture = p_dstElementsFacture;
            _lngIdentifiant = p_lngIdentifiant;
            
            //******Domi 19.02.2015**************
            //Rajouter en période calme une ligne dans la table facture_moyen  |15|     |0|
            //faire avec Toad un update de toutes les lignes table facture_etats (UPDATE facture_etats SET Moyen = 15 where Moyen = 1)
            //Vérifier ensuite avec stéphane si une requete utilise le champs moyen = 1.....le remplacer par 15 le cas échéant.
            z_dvw = new DataView(_dstElementsFacture.facture_moyen);
            z_dvw.Sort = _dstElementsFacture.facture_moyen.OrdreColumn.ColumnName;

            cbxMoyen.DataSource = z_dvw;
            cbxMoyen.DisplayMember = _dstElementsFacture.facture_moyen.LibelleColumn.ColumnName;
            cbxMoyen.ValueMember = _dstElementsFacture.facture_moyen.CodeColumn.ColumnName;
            // 
            z_dvw = new DataView(_dstElementsFacture.facture_type);
            z_dvw.RowFilter = _dstElementsFacture.facture_type.PaiementColumn.ColumnName + " = True";

            cbxType.DataSource = z_dvw;
            cbxType.DisplayMember = _dstElementsFacture.facture_type.LibelleColumn.ColumnName;
            cbxType.ValueMember = _dstElementsFacture.facture_type.EtatColumn.ColumnName;
        }


        private void frmPaiement_Load(object sender, System.EventArgs e)
        {
            switch (_stuMode)
            {
                case ImportSosGeneve.Facture.Structures.ModeAccess.Ajout:
                    _drwFacture = _dstElementsFacture.facture.FindByNFacture(_lngIdentifiant);

                    dbxDate.Value = DateTime.Now.ToString();

                    txtMontant.Text = decimal.Parse(_drwFacture.Solde.ToString()).ToString("0.00");

                    cbxType.SelectedValue = 6;
                    break;
                case ImportSosGeneve.Facture.Structures.ModeAccess.Modification:
                    _drwFactureEtat = _dstElementsFacture.facture_etats.FindByCompteur(_lngIdentifiant);
                    _drwFacture = _dstElementsFacture.facture.FindByNFacture(_drwFactureEtat.NFacture);

                    dbxDate.Value = _drwFactureEtat.DateEtat.ToString();

                    if ((object.ReferenceEquals((_dstElementsFacture.facture_etats.DatePayeColumn.ColumnName), DBNull.Value)))
                    {
                        dbxDateSal.Value = null;
                    }
                    else
                    {
                        dbxDateSal.Value = _drwFactureEtat.DatePaye.ToString();
                    }

                    cbxType.SelectedValue = _drwFactureEtat.Etat;
                    cbxMoyen.SelectedValue = _drwFactureEtat.Moyen;

                    txtMontant.Text = decimal.Parse(_drwFactureEtat.Montant.ToString()).ToString("0.00");

                    txtPayeCommentaire.Text = _drwFactureEtat.CommentaireEtat;
                    break;
            }
        }

        private void btnAnnuler_Click(System.Object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnValider_Click(System.Object sender, System.EventArgs e)
        {
            string z_strRequete = null;
            double z_fltNouveauSolde = 0;
            TableFacture z_dalTableFacture = new TableFacture();
            TableFactureEtat z_dalTableFactureEtat = new TableFactureEtat();
            
            if ((Verification()))
            {
                bool z_blnConnect = Variables.ConnexionBase.OpenBDD();
                Variables.ConnexionBase.BeginTransaction();

                try
                {
                    switch (_stuMode)
                    {
                        case ImportSosGeneve.Facture.Structures.ModeAccess.Ajout:
                            // 
                            z_fltNouveauSolde = _drwFacture.Solde - Convert.ToDouble(txtMontant.Text);
                            // Table Facture -----------------------------------------------------------------------------------------------------
                            _drwFacture.Solde = z_dalTableFacture.UpdateSolde(_drwFacture.NFacture, z_fltNouveauSolde, dbxDate.Value);

                            // Table Facture Etat ------------------------------------------------------------------------------------------------
                            z_dalTableFactureEtat.Insert(_drwFacture.NFacture, cbxType.SelectedValue.ToString(), dbxDate.Value, txtPayeCommentaire.Text, cbxMoyen.SelectedValue.ToString(), SosMedecins.SmartRapport.GestionApplication.VariablesApplicatives.Utilisateurs.Identifiant, txtMontant.Text, dbxDateSal.Value);

                            //
                            _drwFactureEtat = _dstElementsFacture.facture_etats.Newfacture_etatsRow();
                           //_drwFactureEtat = _dstElementsFacture.facture_etats.Newfacture_etatsRow
                            MiseAJourValeurFactureEtat();

                            _dstElementsFacture.facture_etats.Rows.Add(_drwFactureEtat);

                            break;
                        case ImportSosGeneve.Facture.Structures.ModeAccess.Modification:
                            // le montant a ete changé
                            if ((_drwFactureEtat.Montant.ToString() != txtMontant.Text))
                            {
                                z_fltNouveauSolde = _drwFacture.Solde - Convert.ToDouble(txtMontant.Text) + _drwFactureEtat.Montant;
                                // Table Facture -----------------------------------------------------------------------------------------------------
                                _drwFacture.Solde = z_dalTableFacture.UpdateSolde(_drwFacture.NFacture, z_fltNouveauSolde, dbxDate.Value);
                            }

                            // Table Facture Etat ------------------------------------------------------------------------------------------------
                            z_strRequete = "Update facture_etats SET ";
                            z_strRequete += "Etat = " + FormatSql.Format_Nombre(cbxType.SelectedValue.ToString());
                            z_strRequete += ", DateEtat = " + FormatSql.Format_Date(dbxDate.Value.ToString());
                            z_strRequete += ", CommentaireEtat = " + FormatSql.Format_String(txtPayeCommentaire.Text);
                            z_strRequete += ", Moyen = " + FormatSql.Format_Nombre(cbxMoyen.SelectedValue.ToString());
                            z_strRequete += ", CodeUtilisateur = " + FormatSql.Format_String(SosMedecins.SmartRapport.GestionApplication.VariablesApplicatives.Utilisateurs.Identifiant);
                            z_strRequete += ", Montant = " + FormatSql.Format_Nombre(txtMontant.Text);
                            z_strRequete += ", DatePaye = " + FormatSql.Format_Date(dbxDateSal.Value.ToString());
                            z_strRequete += " WHERE Compteur = " + FormatSql.Format_Nombre(_drwFactureEtat.Compteur.ToString());

                            Variables.ConnexionBase.ExecuteSqlSansRetour(z_strRequete);
                            //
                            MiseAJourValeurFactureEtat();
                            break;
                    }
                    // Valide les modifications -----------------------------------------------------------------------------------------------
                    _dstElementsFacture.AcceptChanges();
                    Variables.ConnexionBase.Commit();

                    if (VariablesApplicatives.Utilisateurs.Identifiant.ToString() == "A001")
                        mouchard.evenement("Ajout d'un paiement n° " + _drwFactureEtat.Compteur.ToString() + " pour la facture...n° " + _drwFactureEtat.NFacture.ToString(), VariablesApplicatives.Utilisateurs.NomUtilisateur.ToString());  //log

                    DialogResult = DialogResult.OK;

                }
                catch (Exception ex)
                {
                    _dstElementsFacture.RejectChanges();
                    Variables.ConnexionBase.RollBack();
                    DialogResult = DialogResult.Abort;

                    throw new Exception(ex.Message);
                }
                finally
                {
                    if ((z_blnConnect))
                    {
                        Variables.ConnexionBase.CloseBDD();
                    }
                }

                z_dalTableFacture = null;
                // FIN
                this.Close();
            }
        }

        private bool Verification()
        {
            SosMedecins.Controls.sosErrorProvider errErreur = new SosMedecins.Controls.sosErrorProvider();
            errErreur.Clear();
            if (!(dbxDate.IsValid))
            {
                errErreur.GenereErreur(dbxDate, "Date Incorrecte !",true);
                //errErreur.GenereErreur
            }

            if (!(dbxDateSal.IsValid))
            {
                errErreur.GenereErreur(dbxDateSal, "Date Incorrecte !",true);
            }

            if (!Information.IsNumeric(txtMontant.Text))
            {
                errErreur.GenereErreur(txtMontant, "Montant Incorrect !",true);
            }

            return errErreur.IsValid;
        }

        private void MiseAJourValeurFactureEtat()
        {
            _drwFactureEtat.NFacture = _drwFacture.NFacture;
            _drwFactureEtat.Etat = Convert.ToInt16(cbxType.SelectedValue);
            if (dbxDate.Value.Length > 0)
            {
                _drwFactureEtat.DateEtat = dbxDate.ValeurDate;
            }
            else
            {
                _dstElementsFacture.facture_etats.DateEtatColumn.ColumnName = DBNull.Value.ToString();
            }
            _drwFactureEtat.DateOp = DateTime.Now;
            _drwFactureEtat.CommentaireEtat = txtPayeCommentaire.Text;
            _drwFactureEtat.Param1 = "";
            _drwFactureEtat.Param2 = "";
            _drwFactureEtat.CodeUtilisateur = SosMedecins.SmartRapport.GestionApplication.VariablesApplicatives.Utilisateurs.Identifiant;
            _drwFactureEtat.Montant = Convert.ToDouble(txtMontant.Text);
            _drwFactureEtat.Moyen = Convert.ToInt16(cbxMoyen.SelectedValue);
            if (dbxDateSal.Value.Length > 0)
            {
                _drwFactureEtat.DatePaye = dbxDateSal.ValeurDate;
            }
            else
            {
               // _dstElementsFacture.facture_etats.DatePayeColumn.ColumnName = DBNull.Value.ToString();              
                _dstElementsFacture.facture_etats.DatePayeColumn.ColumnName = DateTime.Now.ToString();              
            }
        }

        public void SupprimeEtat(long p_lngIdentifiant)
        {
            _drwFactureEtat = _dstElementsFacture.facture_etats.FindByCompteur(_lngIdentifiant);
            _drwFacture = _dstElementsFacture.facture.FindByNFacture(_drwFactureEtat.NFacture);

            string z_strRequete = null;
            z_strRequete = "Delete from  facture_etats Where ";
            z_strRequete += " Compteur = " + FormatSql.Format_Nombre(p_lngIdentifiant.ToString());

            bool z_blnConnect = Variables.ConnexionBase.OpenBDD();
            Variables.ConnexionBase.BeginTransaction();

            try
            {
                Variables.ConnexionBase.ExecuteSqlSansRetour(z_strRequete);

                double z_dblNouveauSolde = _drwFacture.Solde + _drwFactureEtat.Montant;

                TableFacture z_dalTableFacture = new TableFacture();
                _drwFacture.Solde = z_dalTableFacture.UpdateSolde(_drwFacture.NFacture, z_dblNouveauSolde, DateAndTime.Now.ToString());
                z_dalTableFacture = null;

                Variables.ConnexionBase.Commit();

                _drwFactureEtat.Delete();
                _dstElementsFacture.AcceptChanges();
            }
            catch (Exception ex)
            {
                _dstElementsFacture.RejectChanges();
                Variables.ConnexionBase.RollBack();

                throw new Exception(ex.Message);
            }
            finally
            {
                if ((z_blnConnect))
                {
                    Variables.ConnexionBase.CloseBDD();
                }
            }
        }
    }
}