using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SosMedecins.SmartRapport.DAL;

namespace ImportSosGeneve.Facture
{
    public partial class frmModificationSolde : Form
    {
        private dstElementsFacture.factureRow _drwFacture;
        public frmModificationSolde(dstElementsFacture p_dstElementsFacture, long p_lngNFacture)
        {
            // This call is required by the Windows Form Designer.
            InitializeComponent();

            // Add any initialization after the InitializeComponent() call.
            //lblAvertissement.Text = "Attention !!!" + Constants.vbCrLf + " vous allez modifier le solde de la facture";
            _drwFacture = p_dstElementsFacture.facture.FindByNFacture(p_lngNFacture);
            txtAncienSolde.Text = _drwFacture.Solde.ToString();
        }

        private void btnAnnuler_Click(System.Object sender, System.EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }


        private void btnValider_Click(System.Object sender, System.EventArgs e)
        {
            TableFacture z_dalTableFacture = new TableFacture();
            _drwFacture.Solde = z_dalTableFacture.UpdateSolde(_drwFacture.NFacture, Convert.ToDouble(txtNouveauSolde.Text), DateTime.Now.ToString());
            z_dalTableFacture = null;

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void txtNouveauSolde_TextChanged(System.Object sender, System.EventArgs e)
        {
            if (txtNouveauSolde.Text.Length > 0)
            {
                btnValider.Enabled = true;
            }
        }
    }
}