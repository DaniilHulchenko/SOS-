using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImportSosGeneve.Facture
{
    public partial class FPoliceVersTarmed : Form
    {
        private long NumPatient;
        public static string DestinataireFacture;
        public static string TarifFacture;
               
        public FPoliceVersTarmed(long IdPatient)
        {
            InitializeComponent();
            NumPatient = IdPatient;
        }

        private void bCherche_Click(object sender, EventArgs e)
        {
            //On récupère l'adresse de destinaire de la facture
            FIP m_fip = new FIP(NumPatient, FIP.TypeOuverture.PoliceTarmed);
            m_fip.ShowDialog();
            m_fip.Dispose();
            
            //On rempli le champs adresse
            tBAdresseDest.Text = DestinataireFacture;           
        }               

        private void bFermer_Click(object sender, EventArgs e)
        {
            //Pour le tarif
            if (rBCantonal.Checked)
                TarifFacture = "Cantonal";
            else TarifFacture = "Federal";

            //Avant de fermer, on renvoi les valeurs
            CtrlFacturation.AdrPourPoliceTarmed = DestinataireFacture;
            CtrlFacturation.TarifPourPoliceTarmed = TarifFacture;

            Close();
        }

        
    }
}
