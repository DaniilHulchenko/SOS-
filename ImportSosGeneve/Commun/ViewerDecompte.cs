using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.Shared;
using SosMedecins.SmartRapport.EtatsCrystal;

namespace ImportSosGeneve.Commun

{
    public partial class ViewerDecompte : Form
    {
       
        public ViewerDecompte()
        {
            InitializeComponent();
        }
      
        public ViewerDecompte(String idPatient, String DateDebut1, string DateFin1, int destinataire, String typeFacture)
        {
            InitializeComponent();
           // reportDocument1.SetParameterValue(0,idPatient);
            int id = int.Parse(idPatient);
            DateTime Debut = DateTime.Parse(DateDebut1);
            DateTime Fin = DateTime.Parse(DateFin1);
            int destinataireFact = destinataire;
            string typedeFacture = typeFacture;

            FactureDecompte facturedecRep = new FactureDecompte();

            //On déclare les discretvalue
            ParameterDiscreteValue DiscreteIdPatient = new ParameterDiscreteValue();
            ParameterDiscreteValue DiscreteDateDeb = new ParameterDiscreteValue();
            ParameterDiscreteValue DiscreteDateFin = new ParameterDiscreteValue();           
            ParameterDiscreteValue DiscreteDestinataire = new ParameterDiscreteValue();
            ParameterDiscreteValue DiscreteValFactureType = new ParameterDiscreteValue();

            DiscreteIdPatient.Value = id;
            DiscreteDateDeb.Value = Debut;
            DiscreteDateFin.Value = Fin;
            DiscreteDestinataire.Value = destinataireFact;
            DiscreteValFactureType.Value = typedeFacture;

            facturedecRep.SetParameterValue("IdPatients", DiscreteIdPatient);
            facturedecRep.SetParameterValue("Debut", DiscreteDateDeb);
            facturedecRep.SetParameterValue("Fin", DiscreteDateFin);
            facturedecRep.SetParameterValue("Destinataire", DiscreteDestinataire);
            facturedecRep.SetParameterValue("TypeFacture", DiscreteValFactureType);

            crystalReportViewer1.ReportSource = facturedecRep;
                       
            crystalReportViewer1.LogOnInfo[0].ConnectionInfo.UserID = "dhr";
            crystalReportViewer1.LogOnInfo[0].ConnectionInfo.Password = "Blender%31416";
            crystalReportViewer1.LogOnInfo[0].ConnectionInfo.ServerName = "192.168.0.8";
            crystalReportViewer1.LogOnInfo[0].ConnectionInfo.DatabaseName = "SmartRapport";                                                             
        }     

    }
}