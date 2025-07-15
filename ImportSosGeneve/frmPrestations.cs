using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using SosMedecins.SmartRapport.EtatsCrystal;

namespace ImportSosGeneve
{
	/// <summary>
	/// Description résumée de frmPrestations.
	/// </summary>
	public class frmPrestations : System.Windows.Forms.Form
	{
		private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
		private EtatPrestation m_rptPrest=null;
		private EtatPrestation EtatPrestation1;
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

        public frmPrestations(SosMedecins.SmartRapport.DAL.dstPrestations dt1, string mois, string year)
		{
			//
			// Requis pour la prise en charge du Concepteur Windows Forms
			//
			InitializeComponent();

			//
			// TODO : ajoutez le code du constructeur après l'appel à InitializeComponent
			//
			m_rptPrest = new EtatPrestation();
			m_rptPrest.SetDataSource(dt1);
			m_rptPrest.SummaryInfo.ReportTitle = "Prestations facturées au mois de " + mois + " " + year;
			crystalReportViewer1.ReportSource = m_rptPrest;

			crystalReportViewer1.RefreshReport();
			crystalReportViewer1.Zoom(2);	
		}

		/// <summary>
		/// Nettoyage des ressources utilisées.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Code généré par le Concepteur Windows Form
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.EtatPrestation1 = new SosMedecins.SmartRapport.EtatsCrystal.EtatPrestation();
            this.SuspendLayout();
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = 0;
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer1.Location = new System.Drawing.Point(8, 0);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.ReportSource = this.EtatPrestation1;
            this.crystalReportViewer1.Size = new System.Drawing.Size(1111, 794);
            this.crystalReportViewer1.TabIndex = 0;
            this.crystalReportViewer1.Load += new System.EventHandler(this.crystalReportViewer1_Load);
            // 
            // EtatPrestation1
            // 
            this.EtatPrestation1.FileName = "rassdk://C:\\Users\\Dominique.SOSDOM\\AppData\\Local\\Temp\\temp_1b6664dd-9186-4851-b9c" +
                "2-c1b9aac1be2c.rpt";
            // 
            // frmPrestations
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(1121, 794);
            this.Controls.Add(this.crystalReportViewer1);
            this.Name = "frmPrestations";
            this.Text = "frmPrestations";
            this.ResumeLayout(false);

		}
		#endregion

		private void crystalReportViewer1_Load(object sender, System.EventArgs e)
		{
		
		}
	}
}
