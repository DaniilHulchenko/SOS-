using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ImportSosGeneve
{
    public partial class frmParametrages : Form
    {
        public frmParametrages()
        {
            InitializeComponent();
        }

        private void frmParametrages_onLoad(object sender, EventArgs e)
        {
            txtServeur.Text = SosMedecins.SmartRapport.DAL.Variables.ConnexionBase.ServerName;
            txtBase.Text = SosMedecins.SmartRapport.DAL.Variables.ConnexionBase.Database;
            txtLogin.Text = SosMedecins.SmartRapport.DAL.Variables.InfoConnexion.Utilisateur;
            txtPass.Text = SosMedecins.SmartRapport.DAL.Variables.InfoConnexion.MotDePasse;
            
            //Chargement des imprimantes dans la liste des Imprimantes : Sélection de l'imprimante par défaut :
            System.Drawing.Printing.PrintDocument prtdoc = new System.Drawing.Printing.PrintDocument();
            string strDefaultPrinter = prtdoc.PrinterSettings.PrinterName;
            foreach (String strPrinter in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                cbInvoicePrinter.Items.Add(strPrinter);
                cbReportPrinter.Items.Add(strPrinter);
                if (strPrinter.ToLower() == SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.StrReportPrinter.ToLower())
                {
                    cbInvoicePrinter.SelectedIndex = cbInvoicePrinter.Items.IndexOf(strPrinter);
                    cbReportPrinter.SelectedIndex = cbReportPrinter.Items.IndexOf(strPrinter);
                }
            }
            prtdoc.Dispose();
            prtdoc = null;
        }

        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnValider_Click(object sender, EventArgs e)
        {
            SosMedecins.SmartRapport.DAL.Variables.InfoConnexion.NomServeur = txtServeur.Text;
            SosMedecins.SmartRapport.DAL.Variables.InfoConnexion.BaseDonnees = txtBase.Text;
            SosMedecins.SmartRapport.DAL.Variables.InfoConnexion.Utilisateur = txtLogin.Text;
            SosMedecins.SmartRapport.DAL.Variables.InfoConnexion.MotDePasse = txtPass.Text;

            SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.StrInvoicePrinter = cbInvoicePrinter.Text;
            SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.StrReportPrinter = cbReportPrinter.Text;

            Parametrage.SauvegardeParametrage(SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli, Application.StartupPath + "\\" + "Config.xml");

         //   SosMedecins.SmartRapport.DAL.Variables.ConnexionBase = new SosMedecins.Connexion.AccessMySql("Database=" + SosMedecins.SmartRapport.DAL.Variables.InfoConnexion.BaseDonnees + ";Data Source=" + SosMedecins.SmartRapport.DAL.Variables.InfoConnexion.NomServeur + ";User Id=" + SosMedecins.SmartRapport.DAL.Variables.InfoConnexion.Utilisateur + ";Password=" + SosMedecins.SmartRapport.DAL.Variables.InfoConnexion.MotDePasse);

            OutilsExt.OutilsSql = new MySql(SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli, Application.StartupPath + "\\Error.log");
            
            this.Close();
        }
    }
}