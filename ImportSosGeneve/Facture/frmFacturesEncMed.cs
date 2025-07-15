using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace ImportSosGeneve
{
	/// <summary>
	/// Description résumée de frmFacturesEncMed.
	/// </summary>
	public class frmFacturesEncMed : System.Windows.Forms.Form
	{
		private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
		private SosMedecins.SmartRapport.EtatsCrystal.FacEncMed m_rptFacture=null;
        private SosMedecins.SmartRapport.EtatsCrystal.EtatDebiteursTrie m_rptDebiteur = null;
        private SosMedecins.SmartRapport.EtatsCrystal.EtatHonoraire m_rptHonor = null;
        private SosMedecins.SmartRapport.EtatsCrystal.EtatDecompte m_rptDecompte = null;
        private SosMedecins.SmartRapport.EtatsCrystal.TotalDebiteurs_Pat m_rptDebPat = null;
        private SosMedecins.SmartRapport.EtatsCrystal.FacEncMed FacEncMed1;
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

        public frmFacturesEncMed(SosMedecins.SmartRapport.DAL.dstFacturesEncMed m_FacEnc, string mois, string year,string date,string DateAppel_de,string DateAppel_Au, string etat)
		{
			//
			// Requis pour la prise en charge du Concepteur Windows Forms
			//
			InitializeComponent();

			//
			// TODO : ajoutez le code du constructeur après l'appel à InitializeComponent
			//
			if (etat == "Factures")
			{
                m_rptFacture = new SosMedecins.SmartRapport.EtatsCrystal.FacEncMed();
				m_rptFacture.SetDataSource(m_FacEnc);
				m_rptFacture.SummaryInfo.ReportTitle = "Factures Encaissée - Mois " + mois + " " + year;
				crystalReportViewer1.ReportSource = m_rptFacture;
			}
			else if (etat == "Debiteurs")
			{
                m_rptDebiteur = new SosMedecins.SmartRapport.EtatsCrystal.EtatDebiteursTrie();
				m_rptDebiteur.SetDataSource(m_FacEnc);
				m_rptDebiteur.SummaryInfo.ReportTitle = "Etat des débiteurs au " + date+"\nAppels facturés du " + DateAppel_de + " au " + DateAppel_Au ;
                //m_rptDebiteur.SummaryInfo.ReportComments = "Date d'appel entre :" + DateAppel_de + "Au" + DateAppel_Au;
				crystalReportViewer1.ReportSource = m_rptDebiteur;
			}
			else if (etat == "Honoraire")
			{
                m_rptHonor = new SosMedecins.SmartRapport.EtatsCrystal.EtatHonoraire();
				m_rptHonor.SetDataSource(m_FacEnc);
				m_rptHonor.SummaryInfo.ReportTitle = "Transfert d'honoraire dans compta pour " + mois + " " + year;
				crystalReportViewer1.ReportSource = m_rptHonor;
			}
			else if (etat == "Decompte")
			{
                string s = SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.StrReportPrinter;
                m_rptDecompte = new SosMedecins.SmartRapport.EtatsCrystal.EtatDecompte();
				m_rptDecompte.SetDataSource(m_FacEnc);
				
				m_rptDecompte.SummaryInfo.ReportTitle = "Relevé de compte";
				crystalReportViewer1.ReportSource = m_rptDecompte;
				//m_rptDecompte.PrintOptions.PrinterName = s;
				
				//m_rptDecompte.PrintToPrinter(1,false,0,0);
				//crystalReportViewer1.PrintReport();
				//CrystalUtility.PrintReport(m_rptDecompte,1, OutilsExt.ParamAppli.StrReportPrinter);
			}
			else if (etat == "TotalDebiteurs")
			{
                m_rptDebPat = new SosMedecins.SmartRapport.EtatsCrystal.TotalDebiteurs_Pat();
				m_rptDebPat.SetDataSource(m_FacEnc);
				m_rptDebPat.SummaryInfo.ReportTitle = "Total Debiteurs Par Patient pour " + mois + " " + year;
				crystalReportViewer1.ReportSource = m_rptDebPat;
			}
			
			
			crystalReportViewer1.RefreshReport();
			//crystalReportViewer1.PrintReport();
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
            this.FacEncMed1 = new SosMedecins.SmartRapport.EtatsCrystal.FacEncMed();
            this.SuspendLayout();
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = 0;
            this.crystalReportViewer1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer1.Location = new System.Drawing.Point(0, 0);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.ReportSource = this.FacEncMed1;
            this.crystalReportViewer1.Size = new System.Drawing.Size(1000, 984);
            this.crystalReportViewer1.TabIndex = 0;
            // 
            // FacEncMed1
            // 
            this.FacEncMed1.FileName = "rassdk://C:\\Users\\dominique\\AppData\\Local\\Temp\\temp_50d00dfc-4ee3-448e-9d72-088e3" +
    "1fd1d1a.rpt";
            // 
            // frmFacturesEncMed
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.ClientSize = new System.Drawing.Size(952, 982);
            this.Controls.Add(this.crystalReportViewer1);
            this.Name = "frmFacturesEncMed";
            this.Text = "frmFacturesEncMed";
            this.ResumeLayout(false);

		}
		#endregion
	}
}
