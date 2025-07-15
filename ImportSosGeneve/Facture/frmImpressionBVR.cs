using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace ImportSosGeneve
{
	public class frmImpressionBVR : System.Windows.Forms.Form
	{
		private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
		private System.ComponentModel.Container components = null;

		private DataRow m_RowFacture=null;
        private SosMedecins.SmartRapport.DAL.dstFactureImpr m_Facture = null;
        private SosMedecins.SmartRapport.EtatsCrystal.BVRMAN m_rptFacture = null;		
		// Données sur le paiement des bv : 
		long[] m_IndexFactures;
		DateTime m_DtPaiement;
		int m_NbBv;
		float[] m_MontantBv;
		int m_DelaiMois;

		public frmImpressionBVR(DataRow rowFacture,long[] IndexDesFactures,DateTime Dt1Paiement,int NbBv,float[] MontantBv,int DelaiMois)
		{
			InitializeComponent();
			this.m_RowFacture = rowFacture;	
		
			m_IndexFactures = IndexDesFactures;
			m_DtPaiement = Dt1Paiement;
			m_NbBv = NbBv;
			m_MontantBv = MontantBv;
			m_DelaiMois = DelaiMois;
		}

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImpressionBVR));
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = -1;
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer1.DisplayBackgroundEdge = false;
            this.crystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Right;
            this.crystalReportViewer1.Location = new System.Drawing.Point(3, 0);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.Size = new System.Drawing.Size(1113, 794);
            this.crystalReportViewer1.TabIndex = 0;
            // 
            // frmImpressionBVR
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.ClientSize = new System.Drawing.Size(1116, 794);
            this.Controls.Add(this.crystalReportViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmImpressionBVR";
            this.Text = "Impression des BVR";
            this.Load += new System.EventHandler(this.frmImpressionFacture_Load);
            this.ResumeLayout(false);

		}
		#endregion


        private void AffichageFacture(SosMedecins.SmartRapport.DAL.dstFactureImpr m_dtFacture)
		{
			FormatteFacture(m_dtFacture);

            m_rptFacture = new SosMedecins.SmartRapport.EtatsCrystal.BVRMAN();
			m_rptFacture.SetDataSource(m_dtFacture);
			crystalReportViewer1.ReportSource = m_rptFacture;
			crystalReportViewer1.RefreshReport();
			crystalReportViewer1.Zoom(2);
		}

        private void FormatteFacture(SosMedecins.SmartRapport.DAL.dstFactureImpr m_dtFacture)
		{
			// Au départ on n'a qu'une ligne de facturation : 
			// on va en ajouter autant qu'il y a de bv - 1 (celle qui existe deja)
			object[] Items = m_dtFacture.Tables[0].Rows[0].ItemArray;
			
			for(int i=1;i<m_NbBv;i++)
			{
				DataRow myRow = m_dtFacture.Tables[0].NewRow();
				myRow.ItemArray = Items;
				m_dtFacture.Tables[0].Rows.Add(myRow);
			}

			// Pour chaque ligne on ajoute les éléments différents d'un bv à l'autre :
       		for(int i=0;i<m_dtFacture.Tables[0].Rows.Count;i++)
			{
				// Numéro du bv
				m_dtFacture.FactureImpression[i].MontantBV = m_MontantBv[i];
				m_dtFacture.FactureImpression[i].dateBV = m_DtPaiement.AddMonths(m_DelaiMois*i);

				m_dtFacture.FactureImpression[i].Montant1 = int.Parse( WorkedString.FormatMontantArrondi(m_MontantBv[i]).Substring(0,WorkedString.FormatMontantArrondi(m_MontantBv[i]).IndexOfAny(new char[] {',','.'})));
				m_dtFacture.FactureImpression[i].Montant2 = int.Parse( WorkedString.FormatMontantArrondi(m_MontantBv[i]).Substring(WorkedString.FormatMontantArrondi(m_MontantBv[i]).IndexOfAny(new char[] {',','.'})+1));

				m_dtFacture.FactureImpression[i].NsFactures = "";
				// Les numéros de facture : 
				foreach(long l in m_IndexFactures)
					m_dtFacture.FactureImpression[i].NsFactures += l + ", ";

				m_dtFacture.FactureImpression[i].NsFactures = m_dtFacture.FactureImpression[i].NsFactures.Remove(m_dtFacture.FactureImpression[i].NsFactures.Length-2,2);
				// Informations du destinataire
				if(m_dtFacture.FactureImpression[i]["TypeDestinataire"].ToString()!=System.DBNull.Value.ToString() && int.Parse(m_dtFacture.FactureImpression[i]["TypeDestinataire"].ToString())==(int)CtrlDest.TypeDestinataire.Idem)
				{
					string[][] retourSexe = OutilsExt.OutilsSql.ExecuteCommandeAvecTabString("SELECT Sexe from tablepersonne where idpersonne = " + m_dtFacture.FactureImpression[i]["CodeDestinataire"].ToString());
					if(retourSexe!=null && retourSexe.Length==1)
					{
						m_dtFacture.FactureImpression[i].CiviliteDestinataire = WorkedString.GetSexeFormate(retourSexe[0][0]);
					}
					else
					{
						m_dtFacture.FactureImpression[i].CiviliteDestinataire = "";
					}
				}
				else
				{
					m_dtFacture.FactureImpression[i].CiviliteDestinataire = "";
				}
				string[] m_tabAdr = m_dtFacture.FactureImpression[i].AdresseDestinataire.Split('\n');
				m_dtFacture.FactureImpression[i].AdresseDestinataire="";
				if(m_tabAdr.Length>1)
				{
					m_dtFacture.FactureImpression[i].NomDestinataire = m_tabAdr[0];
					for(int k=1;k<m_tabAdr.Length;k++)
						m_dtFacture.FactureImpression[i].AdresseDestinataire += m_tabAdr[k] + "\n";
				}
				else
				{
					m_dtFacture.FactureImpression[i].NomDestinataire="";
					m_dtFacture.FactureImpression[i].AdresseDestinataire = m_tabAdr[0];
				}
				// Informations sur la facture :
				if(m_dtFacture.FactureImpression[i]["Sexe"].ToString()!=System.DBNull.Value.ToString())
					m_dtFacture.FactureImpression[i].CivilitePatient = WorkedString.GetSexeFormate(m_dtFacture.FactureImpression[i]["Sexe"].ToString()) + " ";
				m_dtFacture.FactureImpression[i].Concerne = m_dtFacture.FactureImpression[i].CivilitePatient + WorkedString.FormatePreNom(m_dtFacture.FactureImpression[i].PrenomPatient) + " " + WorkedString.FormateNom(m_dtFacture.FactureImpression[i].NomPatient);
				if(m_dtFacture.Tables[0].Rows[i]["DateNaissance"].ToString()!="")
					m_dtFacture.FactureImpression[i].Concerne+=", " + DateTime.Parse(m_dtFacture.Tables[0].Rows[i]["DateNaissance"].ToString()).Year;

                             
                FactureXML45 MyFact = new FactureXML45(OutilsExt.OutilsSql, m_dtFacture.FactureImpression[i].NFacture.ToString(), "", "imprimante", "Normal");
				
				
				//ICI voir le type de bvr à imprimer				

                string RefNumber = MyFact.CalculRefNumber(m_dtFacture.FactureImpression[i].NFacture.ToString(), m_dtFacture.FactureImpression[i].CodeIntervenant.ToString());
                string CodingLine = MyFact.CodingLine(RefNumber, WorkedString.FormatMontantArrondi(m_MontantBv[i]));
                m_dtFacture.FactureImpression[i].StrCode1 = RefNumber;
                m_dtFacture.FactureImpression[i].StrCode2 = CodingLine;
                //m_dtFacture.FactureImpression[i].StrCode2 = CodingLine.ToString().Replace("&gt;", ">");
                
			}
		}

		private void frmImpressionFacture_Load(object sender, System.EventArgs e)
		{
			ChargementEtat();
		}

		public void ChargementEtat()
		{
			if(m_RowFacture!=null)
			{
				// Chargement de la facture dans un dataset typé : 
                this.m_Facture = new SosMedecins.SmartRapport.DAL.dstFactureImpr();
				OutilsExt.OutilsSql.RemplitDataTable(m_Facture.Tables[0],"SELECT f.NFacture,f.NAccident,f.TypeDestinataire,f.CodeDestinataire,f.DateCreation as 'DateFacture',f.TTT,f.AdresseDestinataire as 'AdresseDestinataire',tc.NConsultation,tc.CodeAppel,tc.IndicePatient,m.NomGeneve as 'NomMedecin',m.PrenomGeneve as 'PrenomMedecin',m.NIF,m.Concordat as 'NConcordat',a.CodeIntervenant,a.DAP,a.DSL,pe.Nom as 'NomPatient',pe.Prenom as 'PrenomPatient',pe.IdPersonne as 'NPersonne', pe.Sexe,pe.DateNaissance from facture f inner join factureconsultation c on c.NFacture = f.NFacture inner join tableconsultations tc on tc.NConsultation = c.NConsultation inner join tableactes a on a.Num = tc.CodeAppel inner join tablemedecin m on m.CodeIntervenant = a.CodeIntervenant inner join tablepatient pa on pa.IdPatient = tc.IndicePatient inner join tablepersonne pe on pe.IdPErsonne = pa.IdPersonne WHERE f.NFacture = " + m_RowFacture["NFacture"].ToString() + " AND c.Principale = 1");
				if(this.m_Facture.Tables.Count>0 && this.m_Facture.Tables[0].Rows.Count>0)
				{
					AffichageFacture(this.m_Facture);
				}
			}
		}

		public void AutoPrint()
		{
			System.Drawing.Printing.PrintDocument prtdoc = new System.Drawing.Printing.PrintDocument();
			string strDefaultPrinter = prtdoc.PrinterSettings.PrinterName;
		}
	}
}
