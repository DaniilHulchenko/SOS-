using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using SosMedecins.SmartRapport.EtatsCrystal;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace ImportSosGeneve.Commun
{
    public partial class viewerStats144 : Form
    {
        public viewerStats144()
        {
            InitializeComponent();

            SOS144 SOS1441 = new SOS144();    //on créer l'instance du rapport
            
            TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
            TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
            ConnectionInfo crConnectionInfo = new ConnectionInfo();

            Tables CrTables ;


            //crConnectionInfo.ServerName = "YOUR SERVER NAME";
            //crConnectionInfo.DatabaseName = "YOUR DATABASE NAME";
            crConnectionInfo.UserID = "sa";
            crConnectionInfo.Password = "prazine";

            CrTables = SOS1441.Database.Tables ;
            foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
            {
                crtableLogoninfo = CrTable.LogOnInfo;
                crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                CrTable.ApplyLogOnInfo(crtableLogoninfo);
            }

            crystalReportViewerStats1.ReportSource = SOS1441;

        }

        private void viewerStats144_Load(object sender, EventArgs e)
        {
            crystalReportViewerStats1.RefreshReport();            
        }
    }
}
