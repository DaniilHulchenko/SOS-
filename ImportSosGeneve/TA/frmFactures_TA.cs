using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using SosMedecins.SmartRapport.EtatsCrystal;
using System.Data;
using CrystalDecisions.Shared;

namespace ImportSosGeneve
{
    /// <summary>
    /// Description résumée de frmFactures_TA.
    /// </summary>
    public class frmFactures_TA : Form
    {
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
       
        private Container components = null;

        public frmFactures_TA(SosMedecins.SmartRapport.DAL.dstTaFacture m_FacEnc, string type, string mois, string annee)
        {           
            InitializeComponent();

            //En fonction du type de facture (BVR ou QRcode)
            if (type == "Factures" || type == "RappelsTA")
            {
                TA_Facture m_rptFacture = new TA_Facture();

                //Ajout d'un champs de parametre dans l'état Crystal
                ParameterFields paramFields = new ParameterFields();
                ParameterField paramField = new ParameterField();
                ParameterDiscreteValue discreteVal = new ParameterDiscreteValue();
                paramField.ParameterFieldName = "Rappel";    //C'est le champs parametre "Rappel" de l'état crystal

                if (type == "Factures")
                {
                    discreteVal.Value = "";
                }
                else if (type == "RappelsTA")
                {
                    discreteVal.Value = "RAPPEL";
                }

                //On passe maintenant la DiscreteValue en parametre
                paramField.CurrentValues.Add(discreteVal);
                //Et on l'ajoute à la collection...Pas le choix
                paramFields.Add(paramField);

                m_rptFacture.SetDataSource(m_FacEnc);
                crystalReportViewer1.ParameterFieldInfo = paramFields;
                crystalReportViewer1.ReportSource = m_rptFacture;               
            }
            else if (type == "Factures_QR" || type == "RappelsTA_QR")
            {

                TA_Facture_QR m_rptFacture_QR = new TA_Facture_QR();

                //Ajout d'un champs de parametre dans l'état Crystal
                ParameterFields paramFields = new ParameterFields();
                ParameterField paramField = new ParameterField();
                ParameterDiscreteValue discreteVal = new ParameterDiscreteValue();
                paramField.ParameterFieldName = "Rappel";    //C'est le champs parametre "Rappel" de l'état crystal

                if (type == "Factures_QR")
                {
                    discreteVal.Value = "";
                }
                else if (type == "RappelsTA_QR")
                {
                    discreteVal.Value = "RAPPEL";
                }

                //On passe maintenant la DiscreteValue en parametre
                paramField.CurrentValues.Add(discreteVal);
                //Et on l'ajoute à la collection...Pas le choix
                paramFields.Add(paramField);

                m_rptFacture_QR.SetDataSource(m_FacEnc);
                crystalReportViewer1.ParameterFieldInfo = paramFields;
                crystalReportViewer1.ReportSource = m_rptFacture_QR;                
            }
            else if (type == "Materiel" || type == "RappelTaMat")
            {
                TA_FactureMat rptFactMat = new TA_FactureMat();

                //Ajout d'un champs de parametre dans l'état Crystal
                ParameterFields paramFields = new ParameterFields();
                ParameterField paramField = new ParameterField();
                ParameterDiscreteValue discreteVal = new ParameterDiscreteValue();
                paramField.ParameterFieldName = "Rappel";    //C'est le champs parametre "Rappel" de l'état crystal

                if (type == "Materiel")
                {
                    discreteVal.Value = "";
                }
                else if (type == "RappelTaMat")
                {
                    discreteVal.Value = "RAPPEL";
                }

                //On passe maintenant la DiscreteValue en parametre
                paramField.CurrentValues.Add(discreteVal);
                //Et on l'ajoute à la collection...Pas le choix
                paramFields.Add(paramField);

                rptFactMat.SetDataSource(m_FacEnc);
                crystalReportViewer1.ReportSource = rptFactMat;
                crystalReportViewer1.ParameterFieldInfo = paramFields;
            }
            else if (type == "Materiel_QR" || type == "RappelTaMat_QR")
            {
                TA_FactureMat_QR rptFactMat_QR = new TA_FactureMat_QR();

                //Ajout d'un champs de parametre dans l'état Crystal
                ParameterFields paramFields = new ParameterFields();
                ParameterField paramField = new ParameterField();
                ParameterDiscreteValue discreteVal = new ParameterDiscreteValue();
                paramField.ParameterFieldName = "Rappel";    //C'est le champs parametre "Rappel" de l'état crystal

                if (type == "Materiel_QR")
                {
                    discreteVal.Value = "";
                }
                else if (type == "RappelTaMat_QR")
                {
                    discreteVal.Value = "RAPPEL";
                }

                //On passe maintenant la DiscreteValue en parametre
                paramField.CurrentValues.Add(discreteVal);
                //Et on l'ajoute à la collection...Pas le choix
                paramFields.Add(paramField);

                rptFactMat_QR.SetDataSource(m_FacEnc);
                crystalReportViewer1.ReportSource = rptFactMat_QR;
                crystalReportViewer1.ParameterFieldInfo = paramFields;
            }
            else if (type == "FacEncaissée")
            {
                FacturesTA_Enc m_rptFacturesEnc = new FacturesTA_Enc();
                m_rptFacturesEnc.SetDataSource(m_FacEnc);
                m_rptFacturesEnc.SummaryInfo.ReportTitle = "Factures TELEALARME Encaissée pour le mois de " + mois + " " + annee;
                crystalReportViewer1.ReportSource = m_rptFacturesEnc;
            }

            crystalReportViewer1.Zoom(2);
        }


        public frmFactures_TA(DataSet dtsFact, string type, string mois, string Annee)
        {           
            InitializeComponent();

            if (type == "Materiel" || type == "RappelTaMat")
            {
                TA_FactureMat rptFactMat = new TA_FactureMat();

                //Ajout d'un champs de parametre dans l'état Crystal
                ParameterFields paramFields = new ParameterFields();
                ParameterField paramField = new ParameterField();
                ParameterDiscreteValue discreteVal = new ParameterDiscreteValue();
                paramField.ParameterFieldName = "Rappel";    //C'est le champs parametre "Rappel" de l'état crystal

                if (type == "Materiel")
                {
                    discreteVal.Value = "";
                }
                else if (type == "RappelTaMat")
                {
                    discreteVal.Value = "RAPPEL";
                }

                //On passe maintenant la DiscreteValue en parametre
                paramField.CurrentValues.Add(discreteVal);
                //Et on l'ajoute à la collection...Pas le choix
                paramFields.Add(paramField);
                                                                
                rptFactMat.SetDataSource(dtsFact);
                crystalReportViewer1.ReportSource = rptFactMat;
                crystalReportViewer1.ParameterFieldInfo = paramFields;                               
            }
            else if (type == "Materiel_QR" || type == "RappelTaMat_QR")
            {
                TA_FactureMat_QR rptFactMat_QR = new TA_FactureMat_QR();

                //Ajout d'un champs de parametre dans l'état Crystal
                ParameterFields paramFields = new ParameterFields();
                ParameterField paramField = new ParameterField();
                ParameterDiscreteValue discreteVal = new ParameterDiscreteValue();
                paramField.ParameterFieldName = "Rappel";    //C'est le champs parametre "Rappel" de l'état crystal

                if (type == "Materiel_QR")
                {
                    discreteVal.Value = "";
                }
                else if (type == "RappelTaMat_QR")
                {
                    discreteVal.Value = "RAPPEL";
                }
               

                //On passe maintenant la DiscreteValue en parametre
                paramField.CurrentValues.Add(discreteVal);
                //Et on l'ajoute à la collection...Pas le choix
                paramFields.Add(paramField);

                rptFactMat_QR.SetDataSource(dtsFact);
                crystalReportViewer1.ReportSource = rptFactMat_QR;
                crystalReportViewer1.ParameterFieldInfo = paramFields;
            }
            else if (type == "FacEncaissée")
            {
                FacturesTA_Enc m_rptFacturesEnc = new FacturesTA_Enc();
                m_rptFacturesEnc.SetDataSource(dtsFact);
                m_rptFacturesEnc.SummaryInfo.ReportTitle = "Factures TELEALARME Encaissée pour le mois de " + mois + " " + Annee;
                crystalReportViewer1.ReportSource = m_rptFacturesEnc;
            }

            crystalReportViewer1.Zoom(2);
        }
        
        // Nettoyage des ressources utilisées.
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form       
       
        private void InitializeComponent()
        {
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = -1;
            this.crystalReportViewer1.BorderStyle = BorderStyle.FixedSingle;
            this.crystalReportViewer1.Cursor = Cursors.Default;
            this.crystalReportViewer1.Location = new Point(0, 0);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.Size = new Size(936, 984);
            this.crystalReportViewer1.TabIndex = 0;
            // 
            // frmFactures_TA
            // 
            this.AutoScaleBaseSize = new Size(5, 13);
            this.BackColor = Color.CadetBlue;
            this.ClientSize = new Size(936, 990);
            this.Controls.Add(this.crystalReportViewer1);
            this.Name = "frmFactures_TA";
            this.Text = "Impression des factures TA";
            this.ResumeLayout(false);

        }
        #endregion
    }
}
