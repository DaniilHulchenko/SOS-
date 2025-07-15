using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace ImportSosGeneve
{
	public class frmImpressionDecompte : System.Windows.Forms.Form
	{
		private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
		private System.ComponentModel.Container components = null;


		private DataRow m_RowFacture=null;
        private SosMedecins.SmartRapport.DAL.dstFactureImpr m_Facture = null;
        private SosMedecins.SmartRapport.EtatsCrystal.Rapass m_rptFacture = null;
		

		public frmImpressionDecompte(DataRow rowFacture)
		{
			InitializeComponent();
			this.m_RowFacture = rowFacture;			
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImpressionDecompte));
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
            this.crystalReportViewer1.Location = new System.Drawing.Point(2, 0);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.Size = new System.Drawing.Size(1114, 794);
            this.crystalReportViewer1.TabIndex = 0;
            // 
            // frmImpressionDecompte
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.ClientSize = new System.Drawing.Size(1116, 794);
            this.Controls.Add(this.crystalReportViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmImpressionDecompte";
            this.Text = "Impression des factures";
            this.Load += new System.EventHandler(this.frmImpressionFacture_Load);
            this.ResumeLayout(false);

		}
		#endregion


        private void AffichageFacture(SosMedecins.SmartRapport.DAL.dstFactureImpr m_dtFacture)
		{
			FormatteFacture(m_dtFacture);

            m_rptFacture = new SosMedecins.SmartRapport.EtatsCrystal.Rapass();
			m_rptFacture.SetDataSource(m_dtFacture);
			crystalReportViewer1.ReportSource = m_rptFacture;
		
			crystalReportViewer1.RefreshReport();
			crystalReportViewer1.Zoom(2);
		}

        private void FormatteFacture(SosMedecins.SmartRapport.DAL.dstFactureImpr m_dtFacture)
		{
			// Calcul du total de la facture
			Single Total = 0;
			for(int i=0;i<m_dtFacture.Tables[0].Rows.Count;i++)
			{
				if(!m_dtFacture.FactureImpression[i].IsPrixNull())
					Total+= Single.Parse(m_dtFacture.FactureImpression[i].Prix.ToString());
			}

			for(int i=0;i<m_dtFacture.Tables[0].Rows.Count;i++)
			{
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
				
				// Informations du médecin
				m_dtFacture.FactureImpression[i].StrMedecin = "Dr " + WorkedString.FormatePreNom(m_dtFacture.FactureImpression[i].PrenomMedecin) + " " + WorkedString.FormateNom(m_dtFacture.FactureImpression[i].NomMedecin);
				if(!m_dtFacture.FactureImpression[i].IsNConcordatNull() && m_dtFacture.FactureImpression[i].NConcordat!="")
					m_dtFacture.FactureImpression[i].NConcordat = "Concordat : " + m_dtFacture.FactureImpression[i].NConcordat;
				else
					m_dtFacture.FactureImpression[i].NConcordat="";
				if(m_dtFacture.FactureImpression[i]["NIF"].ToString()!=System.DBNull.Value.ToString())
					m_dtFacture.FactureImpression[i].StrPlus1 = "NIF : " + m_dtFacture.FactureImpression[i]["NIF"].ToString();
				else
					m_dtFacture.FactureImpression[i].StrPlus1="";

				// Informations sur la facture :
				m_dtFacture.FactureImpression[i].StrPlus3 = m_dtFacture.FactureImpression[i].NFacture + "/" + m_dtFacture.FactureImpression[i].NConsultation + "/" + m_dtFacture.FactureImpression[i].CodeIntervenant;
				if(m_dtFacture.FactureImpression[i]["Sexe"].ToString()!=System.DBNull.Value.ToString())
					m_dtFacture.FactureImpression[i].CivilitePatient = WorkedString.GetSexeFormate(m_dtFacture.FactureImpression[i]["Sexe"].ToString()) + " ";
				m_dtFacture.FactureImpression[i].Concerne = m_dtFacture.FactureImpression[i].CivilitePatient + WorkedString.FormatePreNom(m_dtFacture.FactureImpression[i].PrenomPatient) + " " + WorkedString.FormateNom(m_dtFacture.FactureImpression[i].NomPatient);
				if(m_dtFacture.Tables[0].Rows[i]["DateNaissance"].ToString()!="")
					m_dtFacture.FactureImpression[i].Concerne+=", " + DateTime.Parse(m_dtFacture.Tables[0].Rows[i]["DateNaissance"].ToString()).Year;
				// Si c'est un accident , 
				if(m_dtFacture.FactureImpression[i]["NAccident"].ToString()!=System.DBNull.Value.ToString())
					m_dtFacture.FactureImpression[i].StrPlus2 = "Accident n° " + m_dtFacture.FactureImpression[i]["NAccident"].ToString();
				
				// Mise en place des compliments
				string strDate="";
				if(!m_dtFacture.FactureImpression[i].IsDSLNull())
					strDate = m_dtFacture.FactureImpression[i].DSL.ToShortDateString();
				else
					strDate= m_dtFacture.FactureImpression[i].DAP.ToShortDateString();
				m_dtFacture.FactureImpression[i].Compliments = "Le docteur " + m_dtFacture.FactureImpression[i].NomMedecin + " vous présente ses compliments et vous adresse sa note d'honoraires pour le traitement (" + WorkedString.GetTTTFormatte(int.Parse(m_dtFacture.FactureImpression[i].TTT)) + ") du " + strDate + ", en remplacement de votre médecin traitant.";

				// Pour chaque rpestation ou matériel, on recherche le libellé :
				if(m_dtFacture.Tables[0].Rows[i]["TypePrest"].ToString()!="")
				{
					// Recherche du libelle,quantite de matériel ou prestation
					// C'est un matériel, on recherche uniquement le libelle
					if(m_dtFacture.Tables[0].Rows[i]["TypePrest"].ToString()=="2")
					{
						Facture_Materiel mat= Facture_Materiel.GetMateriel(Statiques_Data.TabMateriel,m_dtFacture.FactureImpression[i].Position);
						if(mat!=null)
						{
							m_dtFacture.FactureImpression[i].Prestation = mat.Libelle;
						}
					}
					else // C'est une prestation, on recherche le libellé et la valeur du point M
						if(m_dtFacture.Tables[0].Rows[i]["TypePrest"].ToString()=="1")
					{
						Facture_Prestation  prest= Facture_Prestation.GetFacture_PrestationByNPrestation(Statiques_Data.TabPrestations,m_dtFacture.FactureImpression[i].Position);
						if(prest!=null)
						{
							m_dtFacture.FactureImpression[i].Prestation = prest.Libelle;							
						}
					}
					m_dtFacture.FactureImpression[i].Position = m_dtFacture.Tables[0].Rows[i]["Qte"].ToString() + " x " + m_dtFacture.FactureImpression[i].Position;
				}

				// Affichage du total de la facture
				m_dtFacture.FactureImpression[i].StrTotal = "Total :   Fr.         " + WorkedString.FormatMontantArrondi(Total);

				// Numéro en bas du bordereau + Numéro de référence :
				double dmontant = Total*100;
				long lmontant = (long)dmontant;

                //m_dtFacture.FactureImpression[i].StrCode2 = MyFact.GetCodingLine();      
                //m_dtFacture.FactureImpression[i].StrCode1 = MyFact.GetReferenceNumber(); 
				
                //FactureXML44 MyFact = new FactureXML44(OutilsExt.OutilsSql,m_dtFacture.FactureImpression[i].NFacture.ToString(),"","imprimante");				
                FactureXML45 MyFact = new FactureXML45(OutilsExt.OutilsSql,m_dtFacture.FactureImpression[i].NFacture.ToString(),"","imprimante");				
                string RefNumber = MyFact.CalculRefNumber(m_dtFacture.FactureImpression[i].NFacture.ToString(), m_dtFacture.FactureImpression[i].CodeIntervenant.ToString());
                string CodingLine = MyFact.CodingLine(RefNumber, WorkedString.FormatMontantArrondi(Total)); 
                m_dtFacture.FactureImpression[i].StrCode1 = RefNumber;
                m_dtFacture.FactureImpression[i].StrCode2 = CodingLine.ToString().Replace("&gt;", ">");                   
                //*****************************

				// formattage des montants : 
				m_dtFacture.FactureImpression[i].Montant1 = int.Parse( WorkedString.FormatMontantArrondi(Total).Substring(0,WorkedString.FormatMontantArrondi(Total).IndexOfAny(new char[] {',','.'})));
				m_dtFacture.FactureImpression[i].Montant2 = int.Parse( WorkedString.FormatMontantArrondi(Total).Substring(WorkedString.FormatMontantArrondi(Total).IndexOfAny(new char[] {',','.'})+1));
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
				OutilsExt.OutilsSql.RemplitDataTable(m_Facture.Tables[0],"SELECT f.NFacture,f.NAccident,f.TypeDestinataire,f.CodeDestinataire,f.DateCreation as 'DateFacture',f.TTT,f.AdresseDestinataire as 'AdresseDestinataire',tc.NConsultation,tc.CodeAppel,tc.IndicePatient,m.NomGeneve as 'NomMedecin',m.PrenomGeneve as 'PrenomMedecin',m.NIF,m.Concordat as 'NConcordat',a.CodeIntervenant,a.DAP,a.DSL,pe.Nom as 'NomPatient',pe.Prenom as 'PrenomPatient',pe.IdPersonne as 'NPersonne', pe.Sexe,pe.DateNaissance,fp.TypePrest,fp.Ordre,fp.Indice as 'Position',fp.Points,fp.Prix,fp.Qte  from facture f left join facture_prest fp on fp.NFacture = f.NFacture inner join factureconsultation c on c.NFacture = f.NFacture inner join tableconsultations tc on tc.NConsultation = c.NConsultation inner join tableactes a on a.Num = tc.CodeAppel inner join tablemedecin m on m.CodeIntervenant = a.CodeIntervenant inner join tablepatient pa on pa.IdPatient = tc.IndicePatient inner join tablepersonne pe on pe.IdPErsonne = pa.IdPersonne WHERE f.NFacture = " + m_RowFacture["NFacture"].ToString() + " AND c.Principale = 1 ORDER BY fp.Ordre");
				if(this.m_Facture.Tables.Count>0 && this.m_Facture.Tables[0].Rows.Count>0)
				{
					AffichageFacture(this.m_Facture);
				}
			}
		}

		
	}
}
