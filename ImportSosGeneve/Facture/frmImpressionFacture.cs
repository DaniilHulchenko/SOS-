using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Net.Mail;      //Pour l'envoi des Email
using System.Net.Mime;
using CrystalDecisions.Shared;
using System.Configuration;
using System.Data.SqlClient;      //Pour l'export des rapports
using ImportSosGeneve.Properties;
using SosMedecins.SmartRapport.EtatsCrystal;
using Codecrete.SwissQRBill.Generator;

//################A remplacer a terme par FImpFacture...Plus adapté######Domi 10.05.2021

namespace ImportSosGeneve
{
	public class frmImpressionFacture : System.Windows.Forms.Form
	{
		private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
		private System.ComponentModel.Container components = null;
		private DataRow m_RowFacture=null;
		private System.Windows.Forms.Button btnEnvoi;
		private System.Windows.Forms.TextBox txtCommentaire;
        private SosMedecins.SmartRapport.DAL.dstFactureImpr dsFacture = null;
       // private Bvr m_rptFacture = null;   //BVR Avec QR
        private FactureBVR_QR m_rptFacture_QR = null;                

        int duplic;

		public frmImpressionFacture(DataRow rowFacture, int duplicata)
		{
			InitializeComponent();
			m_RowFacture = rowFacture;
			duplic = duplicata;            
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImpressionFacture));
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.btnEnvoi = new System.Windows.Forms.Button();
            this.txtCommentaire = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = -1;
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer1.DisplayBackgroundEdge = false;
            this.crystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Right;
            this.crystalReportViewer1.Location = new System.Drawing.Point(176, 0);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.Size = new System.Drawing.Size(961, 794);
            this.crystalReportViewer1.TabIndex = 0;
            // 
            // btnEnvoi
            // 
            this.btnEnvoi.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnEnvoi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnvoi.Location = new System.Drawing.Point(33, 31);
            this.btnEnvoi.Name = "btnEnvoi";
            this.btnEnvoi.Size = new System.Drawing.Size(80, 59);
            this.btnEnvoi.TabIndex = 1;
            this.btnEnvoi.Text = "Marquer comme envoyé";
            this.btnEnvoi.UseVisualStyleBackColor = true;
            this.btnEnvoi.Click += new System.EventHandler(this.btnEnvoi_Click);
            // 
            // txtCommentaire
            // 
            this.txtCommentaire.Location = new System.Drawing.Point(33, 112);
            this.txtCommentaire.Name = "txtCommentaire";
            this.txtCommentaire.Size = new System.Drawing.Size(80, 20);
            this.txtCommentaire.TabIndex = 2;
            // 
            // frmImpressionFacture
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(1137, 794);
            this.Controls.Add(this.txtCommentaire);
            this.Controls.Add(this.btnEnvoi);
            this.Controls.Add(this.crystalReportViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmImpressionFacture";
            this.Text = "Impression des factures";
            this.Load += new System.EventHandler(this.frmImpressionFacture_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
        #endregion


        private void AffichageFacture(SosMedecins.SmartRapport.DAL.dstFactureImpr m_dtFacture)
        {
            FormatteFacture(m_dtFacture);
            string s = SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.StrInvoicePrinter;

            //QRCode
            m_rptFacture_QR = new FactureBVR_QR();
            m_rptFacture_QR.SetDataSource(m_dtFacture);
            crystalReportViewer1.ReportSource = m_rptFacture_QR;

            m_rptFacture_QR.PrintOptions.PrinterName = s;
            //m_rptFacture.PrintToPrinter(1,false,0,0);
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
                
                //*****Domi 16.01.2014
                if (m_dtFacture.FactureImpression[i]["EAN"].ToString() != System.DBNull.Value.ToString())
                    m_dtFacture.FactureImpression[i].EANMedecin = "Four. de prestation: " + m_dtFacture.FactureImpression[i]["EAN"].ToString();
                else
                    m_dtFacture.FactureImpression[i].EANMedecin = "";

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
              
				// Pour chaque prestation ou matériel, on recherche le libellé :
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
					else // C'est une prestation, on recherche le libellé et la valeur du point M et du point T
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
                
                if (m_dtFacture.FactureImpression[0].StrPlus5 == null || m_dtFacture.FactureImpression[0].StrPlus5 != "10")
                {                  
                    FactureXML45 MyFact = new FactureXML45(OutilsExt.OutilsSql, m_dtFacture.FactureImpression[0].NFacture.ToString(), "Buffer", "Printer");
                    // formattage des montants : 
                    m_dtFacture.FactureImpression[i].Montant1 = int.Parse(WorkedString.FormatMontantArrondi(Total).Substring(0, WorkedString.FormatMontantArrondi(Total).IndexOfAny(new char[] { ',', '.' })));
                    m_dtFacture.FactureImpression[i].Montant2 = int.Parse(WorkedString.FormatMontantArrondi(Total).Substring(WorkedString.FormatMontantArrondi(Total).IndexOfAny(new char[] { ',', '.' }) + 1));
                }
                else
                {                    
                    FactureXML45 MyFact = new FactureXML45(OutilsExt.OutilsSql, m_dtFacture.FactureImpression[0].NFacture.ToString(), "Buffer", "Printer");

                    // formattage des montants : 
                    float Total10 = Total / 10;
                    m_dtFacture.FactureImpression[i].Montant1 = int.Parse(WorkedString.FormatMontantArrondi(Total10).Substring(0, WorkedString.FormatMontantArrondi(Total10).IndexOfAny(new char[] { ',', '.' })));
                    m_dtFacture.FactureImpression[i].Montant2 = int.Parse(WorkedString.FormatMontantArrondi(Total10).Substring(WorkedString.FormatMontantArrondi(Total10).IndexOfAny(new char[] { ',', '.' }) + 1));
                }
                                                              
                //Edite les éléments pour créer le Coding Line & Reference Number
                string reference_number = CalculRefNumber(m_dtFacture.FactureImpression[0].NFacture.ToString(), m_dtFacture.FactureImpression[0].CodeIntervenant.ToString());
                string Coding_Line = CodingLine(reference_number, m_dtFacture.FactureImpression[i].Montant1.ToString());

                m_dtFacture.FactureImpression[i].StrCode1 = reference_number;
                m_dtFacture.FactureImpression[i].StrCode2 = Coding_Line;

                string MontantTotal = dmontant.ToString();

                //On veut un QRCode              
                string DestFact = m_dtFacture.FactureImpression[i].AdresseDestinataire;
                string NomDestinataire = "";
                string AdresseDestinataire = " ";
                string CPVille = " ";

                string[] DecomposeAdr = DestFact.Split('\n');

                if (DecomposeAdr.Length > 1)
                    NomDestinataire = DecomposeAdr[0];

                if (DecomposeAdr.Length > 2)
                    AdresseDestinataire = DecomposeAdr[1];
                if (DecomposeAdr.Length > 3)
                    CPVille = DecomposeAdr[2];

                //On passe les paramettre du bulletin (dont le QRCode)
                Bill bill = new Bill
                {
                    //Créditeur
                    Account = "CH7630000002120014992",
                    Creditor = new Address
                    {
                        Name = "Sos Medecins Cite Calvin SA",
                        AddressLine1 = "Rue Louis Favre, 43",
                        AddressLine2 = "1201 Genève",
                        CountryCode = "CH"
                    },

                    //Paiement
                    Amount = decimal.Parse(MontantTotal),
                    Currency = "CHF",

                    //Débiteur
                    Debtor = new Address
                    {
                        Name = m_dtFacture.FactureImpression[i].CiviliteDestinataire + " " + NomDestinataire,
                        AddressLine1 = AdresseDestinataire,
                        AddressLine2 = CPVille,
                        CountryCode = "CH"
                    },

                    //Référence du paiement
                    Reference = reference_number.Replace(" ", ""),
                    UnstructuredMessage = "",

                    //On défini le format de sortie, ici png, et suelement le QRCode, pas le bulletin
                    Format = new BillFormat
                    {
                        Language = Language.FR,
                        GraphicsFormat = GraphicsFormat.PNG,
                        OutputSize = OutputSize.QrCodeOnly
                    }
                };

                //Génération de la facture dans un [] de byte que l'on met dans StrPlus7
                try
                {
                    byte[] QRCodeRetour = QRBill.Generate(bill);
                    m_dtFacture.FactureImpression[i].StrPlus7 = QRCodeRetour;
                }
                catch (Exception e)
                {
                    MessageBox.Show("Erreur dans le QRCode : " + e.Message);
                }               
            }
        }

		private void frmImpressionFacture_Load(object sender, System.EventArgs e)
		{
			ChargementEtat("");
		}

		private void btnEnvoi_Click(object sender, System.EventArgs e)
		{
			if(dsFacture != null)
			{
				this.Cursor = Cursors.WaitCursor;
				OutilsExt.OutilsSql.EnregistreEnvoiFacture(dsFacture.FactureImpression[0].NFacture,txtCommentaire.Text);
				this.Cursor = Cursors.Default;
				MessageBox.Show("Enregistrement de l'envoi OK");
			}
		}

		public void ChargementEtat(string TypeSortie)
		{
            if (m_RowFacture != null)
            {
                dsFacture = new SosMedecins.SmartRapport.DAL.dstFactureImpr();
                switch (duplic)
                {
                    case 0:
                        // Chargement de la facture dans un dataset typé : 
                        string Sqlstr = "SELECT f.NFacture,f.NAccident,f.TypeDestinataire,f.CodeDestinataire,f.DateCreation as 'DateFacture',f.TTT,f.AdresseDestinataire as 'AdresseDestinataire',";
                        Sqlstr += " tc.NConsultation,tc.CodeAppel,tc.IndicePatient,m.NomGeneve as 'NomMedecin',m.PrenomGeneve as 'PrenomMedecin', m.NIF,";
                        Sqlstr += " m.EAN, m.Concordat as 'NConcordat',a.CodeIntervenant,a.DAP,a.DSL,pe.Nom as 'NomPatient',pe.Prenom as 'PrenomPatient',";
                        Sqlstr += " pe.IdPersonne as 'NPersonne', pe.Sexe,pe.DateNaissance,fp.TypePrest,fp.Ordre,fp.Indice as 'Position',fp.Points, ";
                        Sqlstr += " fp.Prix,fp.Qte ";
                        Sqlstr += " FROM facture f LEFT JOIN facture_prest fp ON fp.NFacture = f.NFacture ";
                        Sqlstr += "                INNER JOIN factureconsultation c ON c.NFacture = f.NFacture";
                        Sqlstr += "                INNER JOIN tableconsultations tc on tc.NConsultation = c.NConsultation";
                        Sqlstr += "                INNER JOIN tableactes a on a.Num = tc.CodeAppel";
                        Sqlstr += "                INNER JOIN tablemedecin m on m.CodeIntervenant = a.CodeIntervenant";
                        Sqlstr += "                INNER JOIN tablepatient pa on pa.IdPatient = tc.IndicePatient";
                        Sqlstr += "                INNER JOIN tablepersonne pe on pe.IdPErsonne = pa.IdPersonne";
                        Sqlstr += " WHERE f.NFacture = " + m_RowFacture["NFacture"].ToString() + " AND c.Principale = 1 ORDER BY fp.Ordre";

                        OutilsExt.OutilsSql.RemplitDataTable(dsFacture.Tables[0], Sqlstr);
                        
                        if (dsFacture.Tables.Count > 0 && dsFacture.Tables[0].Rows.Count > 0)
                        {
                            dsFacture.Tables[0].Rows[0]["StrPlus5"] = "";
                           // AffichageFacture(this.m_Facture);
                        }
                        break;
                    case 1:
                        // Chargement de la facture dans un dataset typé : 
                        string Sqlstr1 = " SELECT f.NFacture,f.NAccident,f.TypeDestinataire,f.CodeDestinataire,f.DateCreation as 'DateFacture',f.TTT,";
                        Sqlstr1 += " (pe.Nom +' '+pe.Prenom+'\r\n'+pe.Adm_Rue+' '+pe.Adm_NumeroDansRue+'\r\n'+pe.Adm_CodePostal+' '+pe.Adm_Commune) as 'AdresseDestinataire',";
                        Sqlstr1 += " tc.NConsultation,tc.CodeAppel,tc.IndicePatient,m.NomGeneve as 'NomMedecin',m.PrenomGeneve as 'PrenomMedecin',m.NIF,";
                        Sqlstr1 += " m.EAN,m.Concordat as 'NConcordat',a.CodeIntervenant,a.DAP,a.DSL,pe.Nom as 'NomPatient',pe.Prenom as 'PrenomPatient',";
                        Sqlstr1 += " pe.IdPersonne as 'NPersonne', pe.Sexe,pe.DateNaissance,fp.TypePrest,fp.Ordre,fp.Indice as 'Position',fp.Points,";
                        Sqlstr1 += " fp.Prix, fp.Qte ";
                        Sqlstr1 += " FROM facture f LEFT JOIN fac_pres_police fp ON fp.NFacture = f.NFacture";
                        Sqlstr1 += "                INNER JOIN factureconsultation c ON c.NFacture = f.NFacture";
                        Sqlstr1 += "                INNER JOIN tableconsultations tc ON tc.NConsultation = c.NConsultation";
                        Sqlstr1 += "                INNER JOIN tableactes a ON a.Num = tc.CodeAppel";
                        Sqlstr1 += "                INNER JOIN tablemedecin m ON m.CodeIntervenant = a.CodeIntervenant";
                        Sqlstr1 += "                INNER JOIN tablepatient pa ON pa.IdPatient = tc.IndicePatient";
                        Sqlstr1 += "                INNER JOIN tablepersonne pe ON pe.IdPErsonne = pa.IdPersonne";
                        Sqlstr1 += " WHERE f.NFacture = " + m_RowFacture["NFacture"].ToString() + " AND c.Principale = 1 ORDER BY fp.Ordre";

                        OutilsExt.OutilsSql.RemplitDataTable(dsFacture.Tables[0], Sqlstr1);
                        string address = dsFacture.Tables[0].Rows[0]["AdresseDestinataire"].ToString();
                        if (dsFacture.Tables.Count > 0 && dsFacture.Tables[0].Rows.Count > 0)
                        {
                            dsFacture.Tables[0].Rows[0]["StrPlus5"] = "";
                            //AffichageFacture(this.m_Facture);
                        }
                        break;
                    case 2:
                        // Chargement de la facture dans un dataset typé : 
                        OutilsExt.OutilsSql.RemplitDataTable(dsFacture.Tables[0], "SELECT f.NFacture,f.NAccident,f.TypeDestinataire,f.CodeDestinataire,f.DateCreation as 'DateFacture',f.TTT,f.AdresseDestinataire as 'AdresseDestinataire',tc.NConsultation,tc.CodeAppel,tc.IndicePatient,m.NomGeneve as 'NomMedecin',m.PrenomGeneve as 'PrenomMedecin',m.NIF, m.EAN,m.Concordat as 'NConcordat',a.CodeIntervenant,a.DAP,a.DSL,pe.Nom as 'NomPatient',pe.Prenom as 'PrenomPatient',pe.IdPersonne as 'NPersonne', pe.Sexe,pe.DateNaissance,fp.TypePrest,fp.Ordre,fp.Indice as 'Position',fp.Points,fp.Prix,fp.Qte  from facture f left join facture_prest fp on fp.NFacture = f.NFacture inner join factureconsultation c on c.NFacture = f.NFacture inner join tableconsultations tc on tc.NConsultation = c.NConsultation inner join tableactes a on a.Num = tc.CodeAppel inner join tablemedecin m on m.CodeIntervenant = a.CodeIntervenant inner join tablepatient pa on pa.IdPatient = tc.IndicePatient inner join tablepersonne pe on pe.IdPErsonne = pa.IdPersonne WHERE f.NFacture = " + m_RowFacture["NFacture"].ToString() + " AND c.Principale = 1 ORDER BY fp.Ordre");
                        if (dsFacture.Tables.Count > 0 && dsFacture.Tables[0].Rows.Count > 0)
                        {
                            dsFacture.Tables[0].Rows[0]["StrPlus5"] = "Duplicata";
                           // AffichageFacture(this.m_Facture);
                        }
                        break;
                    case 3:
                        // Chargement de la facture dans un dataset typé : 
                        OutilsExt.OutilsSql.RemplitDataTable(dsFacture.Tables[0], "SELECT f.NFacture,f.NAccident,f.TypeDestinataire,f.CodeDestinataire,f.DateCreation as 'DateFacture',f.TTT,f.AdresseDestinataire2 as 'AdresseDestinataire',tc.NConsultation,tc.CodeAppel,tc.IndicePatient,m.NomGeneve as 'NomMedecin',m.PrenomGeneve as 'PrenomMedecin',m.NIF, m.EAN,m.Concordat as 'NConcordat',a.CodeIntervenant,a.DAP,a.DSL,pe.Nom as 'NomPatient',pe.Prenom as 'PrenomPatient',pe.IdPersonne as 'NPersonne', pe.Sexe,pe.DateNaissance,fp.TypePrest,fp.Ordre,fp.Indice as 'Position',fp.Points,fp.Prix,fp.Qte  from facture f left join facture_prest fp on fp.NFacture = f.NFacture inner join factureconsultation c on c.NFacture = f.NFacture inner join tableconsultations tc on tc.NConsultation = c.NConsultation inner join tableactes a on a.Num = tc.CodeAppel inner join tablemedecin m on m.CodeIntervenant = a.CodeIntervenant inner join tablepatient pa on pa.IdPatient = tc.IndicePatient inner join tablepersonne pe on pe.IdPErsonne = pa.IdPersonne WHERE f.NFacture = " + m_RowFacture["NFacture"].ToString() + " AND c.Principale = 1 ORDER BY fp.Ordre");
                        if (dsFacture.Tables.Count > 0 && dsFacture.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < dsFacture.Tables[0].Rows.Count; i++)
                            {
                                dsFacture.Tables[0].Rows[i]["StrPlus5"] = "10";
                                // Quelle est la date de consultation à prendre en compte? Sur les lieux? appel?
                                string strDateConsult = "";
                                if (dsFacture.Tables[0].Rows[0]["DSL"].ToString() != System.DBNull.Value.ToString())
                                {
                                    strDateConsult = OutilsExt.OutilsSql.DateFormatMySql(DateTime.Parse(dsFacture.Tables[0].Rows[0]["DSL"].ToString()));
                                }
                                else
                                {
                                    strDateConsult = OutilsExt.OutilsSql.DateFormatMySql(DateTime.Parse(dsFacture.Tables[0].Rows[0]["DAP"].ToString()));
                                }
                                string[][] retour10 = OutilsExt.OutilsSql.ExecuteCommandeAvecTabString("SELECT TypeDestinataire,IdDestinataire from fac_destinatairefacture10 where IdPatient = " + dsFacture.Tables[0].Rows[0]["IndicePatient"].ToString() + " AND DateDebut<= '" + strDateConsult + "' AND DateFin >= '" + strDateConsult + "'");
                                if (retour10 != null && retour10.Length == 1)
                                    dsFacture.Tables[0].Rows[i]["StrPlus4"] = "Service";
                                else
                                    dsFacture.Tables[0].Rows[i]["StrPlus4"] = "Patient";
                            }
                            //AffichageFacture(this.m_Facture);
                        }
                        break;
                }
                
                //Si c'est envoi par email, on affiche le logo
                if (TypeSortie == "Email")
                {
                    dsFacture.Tables[0].Rows[0]["StrPlus6"] = "Affiche Logo";   //Champs du Dts pour afficher/cacher le logo                                       
                }
                else   //On envoi pas par Email...Donc pas de logo et pas de payé...Pour l'instant....
                {
                    dsFacture.Tables[0].Rows[0]["StrPlus6"] = "";                           
                }
                
                AffichageFacture(dsFacture);
            }
		}

		public void AutoPrint()
		{
            CrystalUtility.PrintReport(m_rptFacture_QR, 1, SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.StrInvoicePrinter);
            OutilsExt.OutilsSql.EnregistreEnvoiFacture(dsFacture.FactureImpression[0].NFacture, txtCommentaire.Text);  
		}
               

        //Domi le 19.10.2017
        public void EnvoiEmail()
        {
            string piecejointe = "";
            string MessageMail = "";
            string AdresseEmail = "";
            
            //On recupère l'Email de la personne

            AdresseEmail = OutilsExt.OutilsSql.EmailPersonne(Int64.Parse(dsFacture.FactureImpression[0].NPersonne.ToString()));

            if (AdresseEmail != "")    //Si on a une adresse Email
            {
                
                //On créer un pdf de la facture dans le rep Temp de l'utilisateur
                string DateFacture = dsFacture.FactureImpression[0].DateFacture.ToString("dd.MM.yyyy");
                string FichierFacture = "";

                //On desactive ou on active certaines sections de la facture si elle est est payée ou non Domi 24.10.2017

                if (double.Parse(m_RowFacture["Solde"].ToString()) == 0)
                {
                    m_rptFacture_QR.ReportFooterSection1.SectionFormat.EnableSuppress = true;     //On desactive la partie BVR
                    m_rptFacture_QR.ReportFooterSection2.SectionFormat.EnableSuppress = false;             //On active la partie Tampon payé
                }
                else
                {
                    m_rptFacture_QR.ReportFooterSection1.SectionFormat.EnableSuppress = false;
                    m_rptFacture_QR.ReportFooterSection2.SectionFormat.EnableSuppress = true;
                }

                FichierFacture = exportReport(m_rptFacture_QR, "Facture_Sos_Medecins_Du_" + dsFacture.FactureImpression[0].DateFacture.ToString("dd.MM.yyyy"), "pdf");

                if (FichierFacture != "Erreur")   //Si l'export c'est bien passé
                {
                    piecejointe = System.IO.Path.GetTempPath() + FichierFacture;    //Répertoire temp de l'utilisateur
                    MessageMail =  "Bonjour, <BR>";
                    MessageMail += "Nous vous prions de trouver en pièce jointe votre facture pour l'intervention de SOS Médecins le " + dsFacture.FactureImpression[0].DAP.ToString();
                    MessageMail += "<BR> En vous remerciant<BR><BR> ";
                    MessageMail += "SOS Médecins";

                    //On envoi le mail
                    if (SendMail(MessageMail, AdresseEmail, piecejointe))
                    {
                      OutilsExt.OutilsSql.EnregistreEnvoiFacture(dsFacture.FactureImpression[0].NFacture, txtCommentaire.Text);
                    }

                }
            }
            else //Pas d'adresse Mail pour cette personne...Le signaler
            {
               string Message = "Le patient " + dsFacture.FactureImpression[0].NomPatient.ToString() + " " + dsFacture.FactureImpression[0].PrenomPatient.ToString() + "\r\n";
               Message += "facture n° " + dsFacture.FactureImpression[0].NFacture.ToString() + "\r\n";
               Message += "n'a pas d'adresse Email ou une adresse mail incorrecte alors que l'envoi est par Email";                              
               MessageBox.Show(Message);
            }            
        }


        //###################################### TRAITEMENT DE LA LIGNE DE CODAGE #################################
        // Methode pour completer les chaines avec des 0 ( exemple 135 donnera 00135 si longueur est à 5)
        private String Complete(String Chaine, int longueur)
        {
            int nbCara = longueur - Chaine.Length;
            String ChaineFinale = "";
            if (nbCara >= 0)
            {
                for (int i = 1; i < nbCara + 1; i++)
                {
                    ChaineFinale = ChaineFinale + "0";
                }
                ChaineFinale = ChaineFinale + Chaine;
            }
            return ChaineFinale;
        }


        static public string Modulo10(string P_serie)
        {
            int[][] tableau = new int[10][];

            for (int t = 0; t < 10; t++)
                tableau[t] = new int[10];

            int k = P_serie.Length + 1;
            int[] report = new int[k];

            report[0] = 0;

            tableau[0][0] = 0;
            tableau[0][1] = 9;
            tableau[0][2] = 4;
            tableau[0][3] = 6;
            tableau[0][4] = 8;
            tableau[0][5] = 2;
            tableau[0][6] = 7;
            tableau[0][7] = 1;
            tableau[0][8] = 3;
            tableau[0][9] = 5;

            for (int i = 1; i < 10; i++)
                for (int j = 0; j < 10; j++)
                {
                    tableau[i][j] = tableau[i - 1][(j + 1) % 10];
                }

            for (int c = 0; c < k - 1; c++)
            {
                int chiffre = Convert.ToInt32(String.Format("{0}", P_serie[c]));
                report[c + 1] = tableau[report[c]][chiffre];
            }

            return String.Format("{0}", (10 - report[k - 1]) % 10);
        }


        //Recupere le reference number d'une facture (utile pour la ligne de codage)
        public String CalculRefNumber(string RefFacture, string Code_intervenant)
        {
            string CodeFinal = "";
            string FactCompl = Complete(RefFacture, 8);
            string IntervCompl = Complete(Code_intervenant, 6);

            CodeFinal = "1" + Complete(FactCompl + IntervCompl, 19);  //1 pour Sos; 2 pour Ab TA; 3 pour Ta Mat;                   
            string str2 = CodeFinal + Modulo10(CodeFinal);

            str2 = FormatgrandNombre(str2, 5);

            str2 = "00 0000" + str2;    //N° d'identification BVR du client....Rien pour PosteFinance, seulement pour les banques

            return str2;
        }


        //Calcule de la ligne de codage
        public String CodingLine(string reference_number, string TotalFacture)
        {
            reference_number = reference_number.ToString().Replace(" ", "");

            double TotalFactureF = double.Parse(TotalFacture);
            TotalFactureF = TotalFactureF * 100;
            string str1 = TotalFactureF.ToString("000000");

            str1 = "010000" + str1;
            str1 = str1 + Modulo10(str1);
            str1 = str1 + ">" + reference_number + "+ 010306272>";    //SOS

            return str1;
        }

        //On format pour l'affichage (int64 est trop petit!)
        private string FormatgrandNombre(string GrandNombre, int GrouperPar)
        {
            string NombreFinal = "";

            for (int i = GrandNombre.Length; i >= 0; i -= GrouperPar)
            {
                if (i < GrouperPar)
                    NombreFinal = GrandNombre.Substring(0, i) + " " + NombreFinal;
                else
                    NombreFinal = GrandNombre.Substring(i - GrouperPar, GrouperPar) + " " + NombreFinal;    //On part de la fin vers le début
            }

            NombreFinal = NombreFinal.TrimEnd();   //On enleve le blanc à la fin

            return NombreFinal;
        }

        //###########################################################################################################



        //********************Fonction pour exporter un etat crystal en divers format de fichier dans le temp de l'utilisateur *************************
        protected string exportReport(CrystalDecisions.CrystalReports.Engine.ReportClass RapportChoisi, string NomDuFichier, string Extension)
        {
            try
            {
                string tempDir = System.IO.Path.GetTempPath();  //Répertoire de l'utilisateur
                string tempFileName = tempDir + NomDuFichier + ".";

                ExportFormatType FormatExport = ExportFormatType.PortableDocFormat;  //On defini un valeur par défaut pour la variable FormatExport

                switch (Extension)
                {
                    case "pdf": FormatExport = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat; tempFileName += "pdf"; break;
                    case "doc": FormatExport = CrystalDecisions.Shared.ExportFormatType.WordForWindows; tempFileName += "doc"; break;
                    case "xls": FormatExport = CrystalDecisions.Shared.ExportFormatType.Excel; tempFileName += "xls"; break;
                    case "htm": FormatExport = CrystalDecisions.Shared.ExportFormatType.HTML40; tempFileName += "htm"; break;
                    case "xml": FormatExport = CrystalDecisions.Shared.ExportFormatType.Xml; tempFileName += "xml"; break;
                }

                //MemoryStream FluxMem = (MemoryStream)RapportChoisi.ExportToStream(FormatExport);

                //Si plantage utiliser la ligne suivante
                Stream FluxMem = RapportChoisi.ExportToStream(FormatExport);


                if (FluxMem.Length == 0) return ("Erreur");

                try
                {
                    //Créer un FileStream object pour écrir un flux dans un fichier           
                    using (FileStream fileStream = System.IO.File.Create(tempFileName, (int)FluxMem.Length))
                    {
                        //Charge le tableau de bytes[] avec le flux de donnée
                        byte[] bytesInStream = new byte[FluxMem.Length];
                        FluxMem.Read(bytesInStream, 0, (int)bytesInStream.Length);

                        //Utilise le FileStream object pour écrire dans le fichier spécifié
                        fileStream.Write(bytesInStream, 0, bytesInStream.Length);
                    }
                }
                catch (System.IO.IOException e)
                {
                    Console.WriteLine("Erreur à la création du fichier :" + e.Message);
                    MessageBox.Show("Erreur à la création du fichier :" + e.Message);
                    return ("Erreur");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur :" + e.Message);
            }

            return (NomDuFichier + "." + Extension);         //Tout est ok retourne 0
        }


        //Génération du QR Code
        /*private byte[] GenereQrCode(SwissQrCode.Contact DemominationCompte, SwissQrCode.Iban SOSQRIban, SwissQrCode.Reference RefPaiement, SwissQrCode.AdditionalInformation Message,
                                        SwissQrCode.Currency Monnaie, decimal Montant)
        {
            //On génère le SwissQR Code
            SwissQrCode generator = new SwissQrCode(SOSQRIban, Monnaie, DemominationCompte, RefPaiement, Message, null, Montant, null, null);

            //Recup de la chaine
            string payload = generator.ToString();
            //Pour le logo croix suisse
            Bitmap logo = new Bitmap(Resources.CroixSuisse_7mm);

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(payload, QRCodeGenerator.ECCLevel.M);    //Level M pour le swiss
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeAsBitmap = qrCode.GetGraphic(20, Color.Black, Color.White, logo, 14, 1, false);  //taille des point en px, couleurs, logo, taille logo, taille marge, pas de bordure

            //On retourne cette image dans un tableau de byte pour la stocker dans le dataSet
            MemoryStream ms = new MemoryStream();
            qrCodeAsBitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            byte[] byteImage = ms.ToArray();

            return byteImage;
        }*/


        //******************Pour l'envoi des Mails****************************
        public bool SendMail(string message, string destinataires, string pieceJointe1)
        {
            //Envoi de mail
            try
            {
                MailMessage Message1 = new MailMessage();
                Message1.From = new MailAddress("facturation@sos-medecins.ch");                    

                foreach (var adresse in destinataires.Split(';'))   //Pour ajouter plusieurs adresses
                {
                    if (adresse != "")
                        Message1.To.Add(new MailAddress(adresse));
                }

                /*Pour TEST*/
                //  Message1.To.Add(new MailAddress("dmercier@sos-Medecins.ch"));

                Message1.Subject = "Facture";
                Message1.IsBodyHtml = true;     //Tres important defini le type de corps du message...Ici en HTML                

                Message1.Body = message;
                // Message1.BodyEncoding = Encoding.GetEncoding("iso-8859-1");

                Message1.Attachments.Add(new Attachment(pieceJointe1));  //Envoi d'une piece jointe (ici les contracts)
               

                SmtpClient client = new SmtpClient("mail.infomaniak.com", 587);

                client.EnableSsl = false;
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("facturation@sos-medecins.ch", "fact102017");  //login et pass du compte SMTP

                Console.WriteLine("Envoi d'un message de {0} en utilisant le SMTP {1} port {2}.", Message1.To.ToString(), client.Host, client.Port);
                client.Send(Message1);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'envoi du mail...L'erreur est : " + ex.ToString());
                return (false);
            }

            return (true);
        }

       


        //*******************************************************************************************************





    }
}
