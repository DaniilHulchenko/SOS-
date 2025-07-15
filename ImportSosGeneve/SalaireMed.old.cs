using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using FarPoint.Win.Spread.CellType;
using System.Text.RegularExpressions;


namespace ImportSosGeneve
{
	/// <summary>
	/// Description résumée de SalaireMed.
	/// </summary>
	public class SalaireMed : System.Windows.Forms.Form
	{
		private System.Windows.Forms.DateTimePicker dtDebut;
		private System.Windows.Forms.DateTimePicker dtFin;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button btTotalPerMed;
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet1;
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet2;
        private FarPoint.Win.Spread.SheetView fpSpread1_Sheet3;
        private FarPoint.Win.Spread.FpSpread fpSpread1;
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		/// 
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button btPrest;
		private System.Windows.Forms.Button btTotalSalaire;
		private System.Windows.Forms.Button btTypePaiment;
		private System.Windows.Forms.Button btDebiteurs;
		private System.Windows.Forms.ComboBox cbTri;
		private System.Windows.Forms.Label lblTri;
		private System.Windows.Forms.ComboBox cbMedecin;
		private System.Windows.Forms.Label lblMedecin;
        private GroupBox grpCritere;
        private Button buttonListeMedec;
        private Label label4;
        private DateTimePicker dateJusqueAu;
        private GroupBox grpAction;

        public SalaireMed()
		{
			//
			// Requis pour la prise en charge du Concepteur Windows Forms
			//
			InitializeComponent();
			
			/// Variable nécessaire au Excel.

			//
			// TODO : ajoutez le code du constructeur après l'appel à InitializeComponent
			//
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SalaireMed));
            this.dtDebut = new System.Windows.Forms.DateTimePicker();
            this.dtFin = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btTotalPerMed = new System.Windows.Forms.Button();
            this.fpSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.fpSpread1_Sheet2 = new FarPoint.Win.Spread.SheetView();
            this.fpSpread1_Sheet3 = new FarPoint.Win.Spread.SheetView();
            this.fpSpread1 = new FarPoint.Win.Spread.FpSpread();
            this.button2 = new System.Windows.Forms.Button();
            this.btPrest = new System.Windows.Forms.Button();
            this.btTotalSalaire = new System.Windows.Forms.Button();
            this.btTypePaiment = new System.Windows.Forms.Button();
            this.btDebiteurs = new System.Windows.Forms.Button();
            this.cbTri = new System.Windows.Forms.ComboBox();
            this.lblTri = new System.Windows.Forms.Label();
            this.cbMedecin = new System.Windows.Forms.ComboBox();
            this.lblMedecin = new System.Windows.Forms.Label();
            this.grpCritere = new System.Windows.Forms.GroupBox();
            this.grpAction = new System.Windows.Forms.GroupBox();
            this.buttonListeMedec = new System.Windows.Forms.Button();
            this.dateJusqueAu = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
            this.grpCritere.SuspendLayout();
            this.grpAction.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtDebut
            // 
            this.dtDebut.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtDebut.Location = new System.Drawing.Point(161, 19);
            this.dtDebut.Name = "dtDebut";
            this.dtDebut.Size = new System.Drawing.Size(88, 20);
            this.dtDebut.TabIndex = 0;
            this.dtDebut.Value = new System.DateTime(2012, 1, 1, 0, 0, 0, 0);
            // 
            // dtFin
            // 
            this.dtFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFin.Location = new System.Drawing.Point(318, 19);
            this.dtFin.Name = "dtFin";
            this.dtFin.Size = new System.Drawing.Size(88, 20);
            this.dtFin.TabIndex = 1;
            this.dtFin.Value = new System.DateTime(2012, 1, 31, 0, 0, 0, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(120, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = " Du ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(293, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "au";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(142, 93);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(232, 32);
            this.button1.TabIndex = 4;
            this.button1.Text = "Factures Encaissée Par Médecin";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btTotalPerMed
            // 
            this.btTotalPerMed.Location = new System.Drawing.Point(136, 90);
            this.btTotalPerMed.Name = "btTotalPerMed";
            this.btTotalPerMed.Size = new System.Drawing.Size(232, 32);
            this.btTotalPerMed.TabIndex = 7;
            this.btTotalPerMed.Text = "Etat Debiteurs Par Medecin";
            this.btTotalPerMed.Click += new System.EventHandler(this.btTotalPerMed_Click);
            // 
            // fpSpread1_Sheet1
            // 
            this.fpSpread1_Sheet1.Reset();
            this.fpSpread1_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet1.Models = ((FarPoint.Win.Spread.SheetView.DocumentModels)(resources.GetObject("fpSpread1_Sheet1.Models")));
            // 
            // fpSpread1_Sheet2
            // 
            this.fpSpread1_Sheet2.Reset();
            this.fpSpread1_Sheet2.SheetName = "Sheet2";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread1_Sheet2.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            fpSpread1_Sheet2.ColumnCount = 2;
            this.fpSpread1_Sheet2.ActiveSkin = FarPoint.Win.Spread.DefaultSkins.Professional2;
            this.fpSpread1_Sheet2.Models = ((FarPoint.Win.Spread.SheetView.DocumentModels)(resources.GetObject("fpSpread1_Sheet2.Models")));
            this.fpSpread1_Sheet2.PrintInfo.BestFitCols = true;
            this.fpSpread1_Sheet2.PrintInfo.BestFitRows = true;
            this.fpSpread1_Sheet2.PrintInfo.Header = "Factures Encaissée - Mois ";
            this.fpSpread1_Sheet2.PrintInfo.Margin.Bottom = 200;
            this.fpSpread1_Sheet2.PrintInfo.Margin.Footer = 100;
            this.fpSpread1_Sheet2.PrintInfo.Margin.Header = 100;
            this.fpSpread1_Sheet2.PrintInfo.Margin.Left = 150;
            this.fpSpread1_Sheet2.PrintInfo.Margin.Right = 100;
            this.fpSpread1_Sheet2.PrintInfo.Margin.Top = 100;
            this.fpSpread1_Sheet2.PrintInfo.ShowColor = true;
            this.fpSpread1_Sheet2.PrintInfo.ShowPrintDialog = true;
            // 
            // fpSpread1_Sheet3
            // 
            this.fpSpread1_Sheet3.Reset();
            this.fpSpread1_Sheet3.SheetName = "Sheet3";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpSpread1_Sheet3.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            fpSpread1_Sheet3.ColumnCount = 3;
            this.fpSpread1_Sheet3.Models = ((FarPoint.Win.Spread.SheetView.DocumentModels)(resources.GetObject("fpSpread1_Sheet3.Models")));
            this.fpSpread1_Sheet3.PrintInfo.BestFitCols = true;
            this.fpSpread1_Sheet3.PrintInfo.BestFitRows = true;
            this.fpSpread1_Sheet3.PrintInfo.Margin.Bottom = 120;
            this.fpSpread1_Sheet3.PrintInfo.Margin.Footer = 50;
            this.fpSpread1_Sheet3.PrintInfo.Margin.Header = 50;
            this.fpSpread1_Sheet3.PrintInfo.Margin.Left = 120;
            this.fpSpread1_Sheet3.PrintInfo.Margin.Right = 120;
            this.fpSpread1_Sheet3.PrintInfo.Margin.Top = 120;
            this.fpSpread1_Sheet3.PrintInfo.ShowPrintDialog = true;
            this.fpSpread1_Sheet3.PrintInfo.ShowRowHeader = FarPoint.Win.Spread.PrintHeader.Hide;
            // 
            // fpSpread1
            // 
            this.fpSpread1.AccessibleDescription = "";
            this.fpSpread1.AllowDragDrop = true;
            this.fpSpread1.BackColor = System.Drawing.Color.DarkGray;
            this.fpSpread1.EditModePermanent = true;
            this.fpSpread1.EditModeReplace = true;
            this.fpSpread1.Location = new System.Drawing.Point(16, 120);
            this.fpSpread1.Name = "fpSpread1";
            this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSpread1_Sheet1,
            this.fpSpread1_Sheet2,
            this.fpSpread1_Sheet3});
            this.fpSpread1.Size = new System.Drawing.Size(208, 104);
            this.fpSpread1.TabIndex = 9;
            this.fpSpread1.Visible = false;
            fpSpread1.ActiveSheetIndex = 1;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(761, 466);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(128, 40);
            this.button2.TabIndex = 12;
            this.button2.Text = "Fermer";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btPrest
            // 
            this.btPrest.Location = new System.Drawing.Point(141, 131);
            this.btPrest.Name = "btPrest";
            this.btPrest.Size = new System.Drawing.Size(232, 32);
            this.btPrest.TabIndex = 13;
            this.btPrest.Text = "Statistique Prestation";
            this.btPrest.Click += new System.EventHandler(this.btPrest_Click);
            // 
            // btTotalSalaire
            // 
            this.btTotalSalaire.Location = new System.Drawing.Point(648, 319);
            this.btTotalSalaire.Name = "btTotalSalaire";
            this.btTotalSalaire.Size = new System.Drawing.Size(43, 32);
            this.btTotalSalaire.TabIndex = 14;
            this.btTotalSalaire.Text = "Total Encaissée Par Médecin";
            this.btTotalSalaire.Visible = false;
            this.btTotalSalaire.Click += new System.EventHandler(this.btTotalSalaire_Click);
            // 
            // btTypePaiment
            // 
            this.btTypePaiment.Location = new System.Drawing.Point(141, 169);
            this.btTypePaiment.Name = "btTypePaiment";
            this.btTypePaiment.Size = new System.Drawing.Size(232, 32);
            this.btTypePaiment.TabIndex = 15;
            this.btTypePaiment.Text = "Transfert d\'Honoraire";
            this.btTypePaiment.Click += new System.EventHandler(this.btTypePaiment_Click);
            // 
            // btDebiteurs
            // 
            this.btDebiteurs.Location = new System.Drawing.Point(136, 141);
            this.btDebiteurs.Name = "btDebiteurs";
            this.btDebiteurs.Size = new System.Drawing.Size(232, 32);
            this.btDebiteurs.TabIndex = 16;
            this.btDebiteurs.Text = "Total Debiteurs";
            this.btDebiteurs.Visible = false;
            this.btDebiteurs.Click += new System.EventHandler(this.btDebiteurs_Click);
            // 
            // cbTri
            // 
            this.cbTri.Items.AddRange(new object[] {
            "Date Consultation",
            "Nom Patient",
            "N. Facture"});
            this.cbTri.Location = new System.Drawing.Point(648, 25);
            this.cbTri.Name = "cbTri";
            this.cbTri.Size = new System.Drawing.Size(144, 21);
            this.cbTri.TabIndex = 17;
            // 
            // lblTri
            // 
            this.lblTri.AutoSize = true;
            this.lblTri.Location = new System.Drawing.Point(604, 28);
            this.lblTri.Name = "lblTri";
            this.lblTri.Size = new System.Drawing.Size(38, 13);
            this.lblTri.TabIndex = 18;
            this.lblTri.Text = "Tri Par";
            // 
            // cbMedecin
            // 
            this.cbMedecin.Location = new System.Drawing.Point(648, 52);
            this.cbMedecin.Name = "cbMedecin";
            this.cbMedecin.Size = new System.Drawing.Size(144, 21);
            this.cbMedecin.TabIndex = 19;
            // 
            // lblMedecin
            // 
            this.lblMedecin.AutoSize = true;
            this.lblMedecin.Location = new System.Drawing.Point(569, 55);
            this.lblMedecin.Name = "lblMedecin";
            this.lblMedecin.Size = new System.Drawing.Size(73, 13);
            this.lblMedecin.TabIndex = 20;
            this.lblMedecin.Text = "Nom Medecin";
            // 
            // grpCritere
            // 
            this.grpCritere.Controls.Add(this.label4);
            this.grpCritere.Controls.Add(this.dateJusqueAu);
            this.grpCritere.Controls.Add(this.btTotalPerMed);
            this.grpCritere.Controls.Add(this.btDebiteurs);
            this.grpCritere.Location = new System.Drawing.Point(12, 12);
            this.grpCritere.Name = "grpCritere";
            this.grpCritere.Size = new System.Drawing.Size(541, 203);
            this.grpCritere.TabIndex = 21;
            this.grpCritere.TabStop = false;
            // 
            // grpAction
            // 
            this.grpAction.Controls.Add(this.button1);
            this.grpAction.Controls.Add(this.btPrest);
            this.grpAction.Controls.Add(this.btTypePaiment);
            this.grpAction.Controls.Add(this.dtDebut);
            this.grpAction.Controls.Add(this.label2);
            this.grpAction.Controls.Add(this.label1);
            this.grpAction.Controls.Add(this.dtFin);
            this.grpAction.Location = new System.Drawing.Point(12, 213);
            this.grpAction.Name = "grpAction";
            this.grpAction.Size = new System.Drawing.Size(541, 248);
            this.grpAction.TabIndex = 22;
            this.grpAction.TabStop = false;
            // 
            // buttonListeMedec
            // 
            this.buttonListeMedec.Location = new System.Drawing.Point(648, 202);
            this.buttonListeMedec.Name = "buttonListeMedec";
            this.buttonListeMedec.Size = new System.Drawing.Size(103, 32);
            this.buttonListeMedec.TabIndex = 17;
            this.buttonListeMedec.Text = "Envoi Par Mail";
            this.buttonListeMedec.UseVisualStyleBackColor = true;
            this.buttonListeMedec.Click += new System.EventHandler(this.buttonListeMedec_Click);
            // 
            // dateJusqueAu
            // 
            this.dateJusqueAu.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateJusqueAu.Location = new System.Drawing.Point(144, 38);
            this.dateJusqueAu.Name = "dateJusqueAu";
            this.dateJusqueAu.Size = new System.Drawing.Size(88, 20);
            this.dateJusqueAu.TabIndex = 22;
            this.dateJusqueAu.Value = new System.DateTime(2012, 1, 1, 0, 0, 0, 0);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(71, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "Date : au";
            // 
            // SalaireMed
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.Tan;
            this.ClientSize = new System.Drawing.Size(901, 518);
            this.ControlBox = false;
            this.Controls.Add(this.btTotalSalaire);
            this.Controls.Add(this.buttonListeMedec);
            this.Controls.Add(this.grpAction);
            this.Controls.Add(this.grpCritere);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.cbTri);
            this.Controls.Add(this.lblTri);
            this.Controls.Add(this.cbMedecin);
            this.Controls.Add(this.lblMedecin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Location = new System.Drawing.Point(100, 200);
            this.Name = "SalaireMed";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Salaire Médecin";
            this.Load += new System.EventHandler(this.SalaireMed_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
            this.grpCritere.ResumeLayout(false);
            this.grpCritere.PerformLayout();
            this.grpAction.ResumeLayout(false);
            this.grpAction.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
            this.Cursor = Cursors.WaitCursor;
            SosMedecins.SmartRapport.EtatsCrystal.Facture_Encaissement_Medecins z_rpt = new SosMedecins.SmartRapport.EtatsCrystal.Facture_Encaissement_Medecins();
            SosMedecins.SmartRapport.EtatsCrystal.Fonctions.ChangeConnection(z_rpt, SosMedecins.SmartRapport.DAL.Variables.ConnexionBase);
            SosMedecins.SmartRapport.EtatsCrystal.frmCrystalReportViewer z_frm = new SosMedecins.SmartRapport.EtatsCrystal.frmCrystalReportViewer();

            z_frm.ReportSource = z_rpt;

            z_rpt.SetParameterValue("DateDebut", DateTime.Parse(dtDebut.Text.ToString()));
            z_rpt.SetParameterValue("DateFin", DateTime.Parse(dtFin.Text.ToString()));

            this.Cursor = Cursors.Default;
            // Affichage
            z_frm.ShowDialog();
            // liberation des elements
            z_rpt.Dispose();
            z_frm.Dispose();
            z_frm = null;
		}

		private void btTotalPerMed_Click(object sender, System.EventArgs e)
		{
			//Vars for Crystal Report
			//DateTime DtDebut = DateTime.Parse(dtDebut.Text.ToString());
			//DateTime DtFin = DateTime.Parse(dtFin.Text.ToString());
            DateTime DateJusqueAu = DateTime.Parse(dateJusqueAu.Text.ToString());
           // Console.WriteLine("la date est : {0}",dtDebut);
           // Console.WriteLine("la date est : {0}", DtFin);
             //hassan
            // DateTime DtDebut =;
             //DateTime DtFin = ;
             string jusque = DateJusqueAu.ToShortDateString();
			// string Debut1 = DtDebut.ToShortDateString();
           // string Debut1 = DateJusqueAu.ToShortDateString();
             int i = 0;
            Regex modèle = new Regex(@"\d{2}.\d{2}.\d{4}");
            MatchCollection résultat_dbt = modèle.Matches(jusque);
            //MatchCollection résultat_Fin = modèle.Matches(fin1);
            //Console.WriteLine("la date  formaté  : {0}", résultat_dbt.Count);
            Console.WriteLine("la date  formaté  : {0}", résultat_dbt[i].Value);
            jusque = résultat_dbt[i].Value;
           // fin1 = résultat_Fin[i].Value;
            //hassan
             //string fin1 = "01.09.2008";
           //  string Debut1 = "01.08.2008";
		//	Debut1 = Debut1.ToString().Replace(".","-");
		//	fin1 = fin1.ToString().Replace(".","-");
          //  Debut1 = résultat_dbt.ToString().Replace(".", "-");
         //   fin1 = résultat_Fin.ToString().Replace(".", "-");
           // Console.WriteLine("la fin est : {0}", fin1);
		//	string[] day = Debut1.Split('-');
		//	Debut1 = day[2].ToString()+"-"+day[1].ToString()+"-"+day[0].ToString();
		//	day = fin1.Split('-');
		//	fin1 = day[2].ToString()+"-"+day[1].ToString()+"-"+day[0].ToString();
          //  Console.WriteLine("la fin est : {0}", fin1);
			//set month name and year
			string mois = "";
            switch (DateJusqueAu.Month)
			{
				case 1:
					mois = "Janvier";
					break;
				case 2:
					mois = "Février";
					break;
				case 3:
					mois = "Mars";
					break;
				case 4:
					mois = "Avril";
					break;
				case 5:
					mois = "Mai";
					break;
				case 6:
					mois = "Juin";
					break;
				case 7:
					mois = "Juillet";
					break;
				case 8:
					mois = "Août";
					break;
				case 9:
					mois = "Septembre";
					break;
				case 10:
					mois = "Octobre";
					break;
				case 11:
					mois = "Novembre";
					break;
				case 12:
					mois = "Décembre";
					break;
			}

            string year = DateJusqueAu.Year.ToString();
			// tri les factures par
			string tri = "";
			if (cbTri.Text == "Date Consultation")
				tri = "FacDateEnvoyee";
			else if (cbTri.Text == "Nom Patient")
					 tri = "NomPatient";
			else if (cbTri.Text == "N. Facture")
				tri = "facture.NFacture";
			else
				tri = "FacDateEnvoyee";
            //ListeMedecinscs lm = new ListeMedecinscs();
            
			//Pour medecin 
			string nom = "";
			if (cbMedecin.Text != "")
				nom = "AND tablemedecin.Nom = '"+cbMedecin.Text +"'";

			//sql debiteur
            ImportSosGeneve.Donnees.MesFactures = new SosMedecins.SmartRapport.DAL.dstFacturesEncMed();

            string sql = "SELECT tablemedecin.Nom AS NomMED, tablemedecin.Mail, facture.NFacture, tableactes.DAP AS FacDateEnvoyee, facture.Solde AS TotalFacture, (tablepersonne.Nom+' '+tablepersonne.Prenom) as NomPatient, facture.AdresseDestinataire, tablepersonne.Tel ";
		 	sql = sql+ "FROM (tablepersonne INNER JOIN tablepatient ON tablepersonne.IdPersonne = tablepatient.IdPersonne) INNER JOIN (facture INNER JOIN ((((factureconsultation INNER JOIN facture_status ON factureconsultation.NFacture = facture_status.NFacture) INNER JOIN tableconsultations ON factureconsultation.NConsultation = tableconsultations.NConsultation) INNER JOIN tableactes ON tableconsultations.CodeAppel = tableactes.Num) INNER JOIN tablemedecin ON tableactes.CodeIntervenant = tablemedecin.CodeIntervenant) ON facture.NFacture = facture_status.NFacture) ON tablepatient.IdPatient = tableconsultations.IndicePatient ";
          //  sql = sql + "WHERE (((facture.NFacture)>44) AND ((facture_status.FacDateEnvoyee) Is Not Null And (facture_status.FacDateEnvoyee)<'01.09.2008') AND ((tableactes.CodeIntervenant)<>2536) AND ((facture_status.FacDateAnnulee) Is Null) AND ((facture.Solde)>1) AND ((facture_status.FacDateAcquittee)>'01.09.2008' Or (facture_status.FacDateAcquittee) Is Null)) " + nom + " ORDER BY tablemedecin.Nom, " + tri.ToString();
            //hassan
             sql = sql + "WHERE (((facture.NFacture)>44) AND ((facture_status.FacDateEnvoyee) Is Not Null And (facture_status.FacDateEnvoyee)<'" + jusque +"') AND ((tableactes.CodeIntervenant)<>2536) AND ((facture_status.FacDateAnnulee) Is Null) AND ((facture.Solde)>1) AND ((facture_status.FacDateAcquittee)< = '" + jusque +"' Or (facture_status.FacDateAcquittee) Is Null)) "+nom+" ORDER BY tablemedecin.Nom, " +tri.ToString();
            OutilsExt.OutilsSql.RemplitDataTable(ImportSosGeneve.Donnees.MesFactures.Tables[0], sql);

            if(ImportSosGeneve.Donnees.MesFactures.Tables.Count>0 && ImportSosGeneve.Donnees.MesFactures.Tables[0].Rows.Count>0)
			{
				frmFacturesEncMed imprDebiteurs = new frmFacturesEncMed(ImportSosGeneve.Donnees.MesFactures, mois, year,jusque, "Debiteurs");
				imprDebiteurs.ShowDialog();
				imprDebiteurs.Dispose();
				imprDebiteurs=null;
			}
		}

		private void SalaireMed_Load(object sender, System.EventArgs e)
		{
			//
			int month = 0;
			int year = DateTime.Now.Year;
			if (DateTime.Now.Month == 1)
			{
				month = 12;
				year = year -1;
			}
			else
				month = DateTime.Now.Month -1;
			
			dtDebut.Text = "01." + month.ToString()+"."+year.ToString();
			dtFin.Text = "01." + DateTime.Now.Month +"." + DateTime.Now.Year;

			cbMedecin.Items.Clear();
            string[][] ListeNoms = OutilsExt.OutilsSql.ListeMedecins();
			foreach(string[] s in ListeNoms)
			{
				ListItem item = new ListItem(s[0],s[1]);
				cbMedecin.Items.Add(item);
			}
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void btPrest_Click(object sender, System.EventArgs e)
		{
			DateTime DtDebut = DateTime.Parse(dtDebut.Text.ToString());
			DateTime DtFin = DateTime.Parse(dtFin.Text.ToString());
			
            //string pourvoirFormatsql = SosMedecins.Connexion.FormatSql.Format_Date(dtDebut.Text.ToString());

			//set month name and year
			string mois = "";
			switch(DtDebut.Month)
			{
				case 1:
					mois = "Janvier";
					break;
				case 2:
					mois = "Février";
					break;
				case 3:
					mois = "Mars";
					break;
				case 4:
					mois = "Avril";
					break;
				case 5:
					mois = "Mai";
					break;
				case 6:
					mois = "Juin";
					break;
				case 7:
					mois = "Juillet";
					break;
				case 8:
					mois = "Août";
					break;
				case 9:
					mois = "Septembre";
					break;
				case 10:
					mois = "Octobre";
					break;
				case 11:
					mois = "Novembre";
					break;
				case 12:
					mois = "Décembre";
					break;
			}
           //*********************************voir ici***************************

			string year = DtDebut.Year.ToString();
			
			//sql Prestation
            ImportSosGeneve.Donnees.MesPres = new SosMedecins.SmartRapport.DAL.dstPrestations();
			
			string sql ="SELECT tablemedecin.Nom As NomMED, facture_prest.TypeTarif, fac_prestations.NPrestation, fac_prestations.PrestLibelle, Sum(facture_prest.Qte) AS SumQua, Sum(facture_prest.Prix) AS SumPrix, tablemedecin.CodeIntervenant, ";
            //sql += " If (facture_prest.TypeTarif=3,'USAGE',If(facture_prest.TypeTarif=4,'POLICE',If(facture_prest.TypeTarif=5,'TARMED FEDERAL',If(facture_prest.TypeTarif=6,'TARMED CANTONAL','AUTRE')))) AS Tarif ";
            sql += "Case facture_prest.TypeTarif ";
            sql += "WHEN 3 THEN 'USAGE' ";
            sql += "WHEN 4 THEN 'POLICE' ";
            sql += "WHEN 5 THEN 'TARMED FEDERAL' ";
            sql += "WHEN 6 THEN 'TARMED CANTONAL' ";
            sql += "ELSE 'AUTRE' ";
            sql += "END AS Tarif ";
            sql += "FROM ((((factureconsultation INNER JOIN tableconsultations ON factureconsultation.NConsultation = tableconsultations.NConsultation) INNER JOIN tableactes ON tableconsultations.CodeAppel = tableactes.Num) INNER JOIN tablemedecin ON tableactes.CodeIntervenant = tablemedecin.CodeIntervenant) INNER JOIN facture ON factureconsultation.NFacture = facture.NFacture) INNER JOIN (fac_prestations INNER JOIN facture_prest ON fac_prestations.NPrestation = facture_prest.Indice) ON facture.NFacture = facture_prest.NFacture ";

            sql += "WHERE (((facture.NFacture)>44) AND ((facture.DateCreation)>'" + DtDebut + "' And (facture.DateCreation)<='" + DtFin + "')) ";
            //sql += "WHERE (((facture.NFacture)>44) AND ((facture.DateCreation)>'"+ Debut1 +"' And (facture.DateCreation)<'"+ fin1 +"')) ";
            //sql += "WHERE (((facture.NFacture)>44) AND ((facture.DateCreation)>'01.04.2009' And (facture.DateCreation)<'01.05.2009')) ";
			sql += "GROUP BY tablemedecin.Nom, facture_prest.TypeTarif, fac_prestations.NPrestation, fac_prestations.PrestLibelle, tablemedecin.CodeIntervenant ";
			sql += "HAVING (((facture_prest.TypeTarif)=3 Or (facture_prest.TypeTarif)=4 Or (facture_prest.TypeTarif)=5 Or (facture_prest.TypeTarif)=6) AND ((tablemedecin.CodeIntervenant)<>2536))";

            DataSet dt1 = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet(sql);
			int cont = dt1.Tables[0].Rows.Count;
			//enter the info to the data set and send the information to report
            OutilsExt.OutilsSql.RemplitDataTable(ImportSosGeneve.Donnees.MesPres.Tables[0], sql);
			
			int count = ImportSosGeneve.Donnees.MesPres.Tables[0].Rows.Count;
			if(ImportSosGeneve.Donnees.MesPres.Tables.Count>0 && ImportSosGeneve.Donnees.MesPres.Tables[0].Rows.Count>0)
			{
				frmPrestations imprPrestation = new frmPrestations(ImportSosGeneve.Donnees.MesPres, mois, year);
				imprPrestation.ShowDialog();
				imprPrestation.Dispose();
				imprPrestation=null;
			}
		}

		private void btTotalSalaire_Click(object sender, System.EventArgs e)
		{
			//send the info for recuperer the bills 
			DateTime DtDebut = DateTime.Parse(dtDebut.Text.ToString());
			DateTime DtFin = DateTime.Parse(dtFin.Text.ToString());

			string fin1 = DtFin.ToShortDateString();
			string Debut1 = DtDebut.ToShortDateString();

			Debut1 = Debut1.ToString().Replace(".","-");
			fin1 = fin1.ToString().Replace(".","-");
			string[] day = Debut1.Split('-');
			Debut1 = day[2].ToString()+"-"+day[1].ToString()+"-"+day[0].ToString();
			day = fin1.Split('-');
			fin1 = day[2].ToString()+"-"+day[1].ToString()+"-"+day[0].ToString();

            DataSet dt2 = OutilsExt.OutilsSql.RecupereCodeMedeins();
			// Chargement des factures dans un dataset typé :
			string sql = "SELECT facture.NFacture, tableactes.DAP, facture_etats.DatePaye as DateOp, facture_etats.Montant as Montant, facture_status.FacDateEnvoyee, factureconsultation.NConsultation, tableactes.Num, tableactes.CodeIntervenant, tablemedecin.Nom, tableactes.IndicePatient, facture.Tarif, facture_status.FacDateAnnulee, facture_status.FacDateEncaissee, facture.TotalFacture, facture.DateCreation, facture_etats.Etat, facture_status.FacDateAcquittee ";
			sql = sql + "FROM factureconsultation INNER JOIN facture_status ON factureconsultation.NFacture = facture_status.NFacture INNER JOIN facture_etats ON facture_status.NFacture = facture_etats.NFacture INNER JOIN facture ON facture_etats.NFacture = facture.NFacture INNER JOIN tableconsultations ON factureconsultation.NConsultation = tableconsultations.NConsultation INNER JOIN tableactes ON tableconsultations.CodeAppel = tableactes.Num INNER JOIN tablemedecin ON tableactes.CodeIntervenant = tablemedecin.CodeIntervenant INNER JOIN tablepatient ON tableconsultations.IndicePatient = tablepatient.IdPatient INNER JOIN tablepersonne ON tablepatient.IdPersonne = tablepersonne.IdPersonne";
            sql = sql + " WHERE facture.NFacture>44 AND facture_status.FacDateEnvoyee Is Not Null AND facture_status.FacDateEncaissee Is Null AND tableactes.CodeIntervenant<>2536 AND facture_status.FacDateAnnulee Is Null AND facture.TotalFacture>0 AND facture_etats.DatePaye>'" + Debut1 + "' AND facture_etats.DatePaye <'" + fin1 + "' AND facture_etats.Etat=6";

            DataSet dt1 = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet(sql);
			int count = dt1.Tables[0].Rows.Count;
						
			//calculate the total amount of bills 
			float Total = 0;
			string code = "";
			string nom = "";
			DateTime date = DateTime.Now;
			float MontantTotal = 0;
			DataRow[] rows=new DataRow[0];
			int j=0;

            for(int c=0;c<dt2.Tables[0].Rows.Count;c++)
			{
				code = dt2.Tables[0].Rows[c]["CodeIntervenant"].ToString();
				nom = dt2.Tables[0].Rows[c]["Nom"].ToString();
				Total = 0;
				//char ch = '"';
				rows = dt1.Tables[0].Select("CodeIntervenant='"+code+"'");
				//if le medecin a des factures encaissée
				if (rows.Length>0)
				{
					//Calculer le somme total pour le medecin
					for(int i=0;i<rows.Length;i++)
					{
						Total = Total + float.Parse(rows[i]["Montant"].ToString());
					}

					if (Total>0)
					{
						fpSpread1.Sheets[1].Cells[j,0].Value= nom;
						fpSpread1.Sheets[1].Cells[j,1].Value= Total.ToString();
						j=j+1;
						MontantTotal = MontantTotal + Total;

					}
				}
			}
			fpSpread1.Sheets[1].Cells[j,0].Value= "Montant Total";
			fpSpread1.Sheets[1].Cells[j,1].Value= MontantTotal.ToString();

			//edit month for header
			string mois = "";
			switch(DtDebut.Month)
			{
				case 1:
					mois = "Janvier";
					break;
				case 2:
					mois = "Février";
					break;
				case 3:
					mois = "Mars";
					break;
				case 4:
					mois = "Avril";
					break;
				case 5:
					mois = "Mai";
					break;
				case 6:
					mois = "Juin";
					break;
				case 7:
					mois = "Juillet";
					break;
				case 8:
					mois = "Août";
					break;
				case 9:
					mois = "Septembre";
					break;
				case 10:
					mois = "Octobre";
					break;
				case 11:
					mois = "Novembre";
					break;
				case 12:
					mois = "Décembre";
					break;
			}
			// Create PrintInfo object and set properties
			FarPoint.Win.Spread.PrintInfo PrintSettings = new FarPoint.Win.Spread.PrintInfo();

            PrintSettings.ShowPrintDialog = true;
			//PrintSettings.ShowRowHeaders = false;
			PrintSettings.BestFitCols =  true;
			PrintSettings.ShowBorder = true;
			PrintSettings.ShowGrid = true;
			//set header and footer
			char ch = '"';
			fpSpread1.Sheets[1].PrintInfo.Header = "/fz"+ ch + 15 + ch + "/fb1/fu1/cTotal Factures Encaissée - /n/cMois " + mois.ToUpper()+ " " + DtDebut.Year.ToString();
			
			fpSpread1.Sheets[1].PrintInfo.Footer = DateTime.Now.ToString()+ "                                                  " + " Page /p";
			fpSpread1.Sheets[1].PrintInfo.FirstPageNumber = 1;
			fpSpread1.Sheets[1].PrintInfo.Preview = true;
			fpSpread1.PrintSheet(1);
		}

        private void btTypePaiment_Click(object sender, System.EventArgs e)
        {
            //send the info for recuperer the bills 
            DateTime DtDebut = DateTime.Parse(dtDebut.Text.ToString());
            DateTime DtFin = DateTime.Parse(dtFin.Text.ToString());

            string fin1 = DtFin.ToShortDateString();
            string Debut1 = DtDebut.ToShortDateString();
            Debut1 = Debut1.ToString().Replace(".", "-");
            fin1 = fin1.ToString().Replace(".", "-");
            string[] day = Debut1.Split('-');
            Debut1 = day[2].ToString() + "-" + day[1].ToString() + "-" + day[0].ToString();
            day = fin1.Split('-');
            fin1 = day[2].ToString() + "-" + day[1].ToString() + "-" + day[0].ToString();

            //set month name and year
            string mois = "";
            switch (DtDebut.Month)
            {
                case 1:
                    mois = "Janvier";
                    break;
                case 2:
                    mois = "Février";
                    break;
                case 3:
                    mois = "Mars";
                    break;
                case 4:
                    mois = "Avril";
                    break;
                case 5:
                    mois = "Mai";
                    break;
                case 6:
                    mois = "Juin";
                    break;
                case 7:
                    mois = "Juillet";
                    break;
                case 8:
                    mois = "Août";
                    break;
                case 9:
                    mois = "Septembre";
                    break;
                case 10:
                    mois = "Octobre";
                    break;
                case 11:
                    mois = "Novembre";
                    break;
                case 12:
                    mois = "Décembre";
                    break;
            }

            string year = DtDebut.Year.ToString();
            //Dataset
            ImportSosGeneve.Donnees.MesFactures = new SosMedecins.SmartRapport.DAL.dstFacturesEncMed();

            //SQL pour Honoraires
            //string sql = "SELECT facture_etats.Param1 AS Tel, facture_etats.DateEtat AS DateOp, facture_etats.Etat, Count(facture_etats.NFacture) AS NFacture, Sum(facture_etats.Montant) AS Montant";
            //sql = sql + " FROM facture_etats INNER JOIN facture_status ON facture_etats.NFacture = facture_status.NFacture";
            //sql = sql + " WHERE facture_etats.DateEtat >= " + SosMedecins.Connexion.FormatSql.Format_Date(Debut1) + " And facture_etats.DateEtat < " + SosMedecins.Connexion.FormatSql.Format_Date(fin1);
            //sql = sql + " And facture_etats.Param1<>'annulée' AND facture_etats.Etat=6";
            //sql = sql + " GROUP BY facture_etats.Param1, facture_etats.Etat, facture_etats.DateEtat";

            string sql = "SELECT facture_moyen.Libelle AS Tel, facture_etats.DateEtat AS DateOp, facture_etats.Etat, Count(facture_etats.NFacture) AS NFacture, Sum(facture_etats.Montant) AS Montant";
            sql = sql + " FROM facture_etats INNER JOIN facture_status ON facture_etats.NFacture = facture_status.NFacture INNER JOIN facture_moyen ON facture_etats.Moyen = facture_moyen.Code";
            sql = sql + " WHERE facture_etats.DateEtat >= " + SosMedecins.Connexion.FormatSql.Format_Date(Debut1) + " And facture_etats.DateEtat <= " + SosMedecins.Connexion.FormatSql.Format_Date(fin1);
            sql = sql + " And facture_etats.Param1<>'annulée' AND facture_etats.Etat=6";
            sql = sql + " GROUP BY facture_moyen.Libelle, facture_etats.Etat, facture_etats.DateEtat";

            // Chargement des factures dans un dataset typé :
            OutilsExt.OutilsSql.RemplitDataTable(ImportSosGeneve.Donnees.MesFactures.Tables[0], sql);
            //string nom = ImportSosGeneve.Donnees.MesFactures.Tables[0].Rows[0]["NomMED"].ToString();
            int count = ImportSosGeneve.Donnees.MesFactures.Tables[0].Rows.Count;

            //Ouvrir le report
            if (ImportSosGeneve.Donnees.MesFactures.Tables.Count > 0 && ImportSosGeneve.Donnees.MesFactures.Tables[0].Rows.Count > 0)
            {
                frmFacturesEncMed imprHonoraire = new frmFacturesEncMed(ImportSosGeneve.Donnees.MesFactures, mois, year,fin1,"Honoraire");
                imprHonoraire.ShowDialog();
                imprHonoraire.Dispose();
                imprHonoraire = null;
            }
        }

		private void btDebiteurs_Click(object sender, System.EventArgs e)
		{
			// Debiteur

			//Vars for Crystal Report
			DateTime DtDebut = DateTime.Parse(dtDebut.Text.ToString());
			DateTime DtFin = DateTime.Parse(dtFin.Text.ToString());
			
			string fin1 = DtFin.ToShortDateString();
			string Debut1 = DtDebut.ToShortDateString();
			Debut1 = Debut1.ToString().Replace(".","-");
			fin1 = fin1.ToString().Replace(".","-");
			string[] day = Debut1.Split('-');
			Debut1 = day[2].ToString()+"-"+day[1].ToString()+"-"+day[0].ToString();
			day = fin1.Split('-');
			fin1 = day[2].ToString()+"-"+day[1].ToString()+"-"+day[0].ToString();
			//set month name and year
			string mois = "";
			switch(DtFin.Month)
			{
				case 1:
					mois = "Janvier";
					break;
				case 2:
					mois = "Février";
					break;
				case 3:
					mois = "Mars";
					break;
				case 4:
					mois = "Avril";
					break;
				case 5:
					mois = "Mai";
					break;
				case 6:
					mois = "Juin";
					break;
				case 7:
					mois = "Juillet";
					break;
				case 8:
					mois = "Août";
					break;
				case 9:
					mois = "Septembre";
					break;
				case 10:
					mois = "Octobre";
					break;
				case 11:
					mois = "Novembre";
					break;
				case 12:
					mois = "Décembre";
					break;
			}

			string year = DtFin.Year.ToString();
			//sql debiteur
            ImportSosGeneve.Donnees.MesFactures = new SosMedecins.SmartRapport.DAL.dstFacturesEncMed();
			
			string sql ="SELECT (tablepersonne.Nom+' '+tablepersonne.Prenom) as NomPatient, tableactes.DAP, tablemedecin.Nom AS NomMED, facture.NFacture, facture.TotalFacture,facture.Solde, facture.AdresseDestinataire, tablepersonne.Tel ";
			sql = sql+ "FROM (tablepersonne INNER JOIN tablepatient ON tablepersonne.IdPersonne = tablepatient.IdPersonne) INNER JOIN (facture INNER JOIN ((((factureconsultation INNER JOIN facture_status ON factureconsultation.NFacture = facture_status.NFacture) INNER JOIN tableconsultations ON factureconsultation.NConsultation = tableconsultations.NConsultation) INNER JOIN tableactes ON tableconsultations.CodeAppel = tableactes.Num) INNER JOIN tablemedecin ON tableactes.CodeIntervenant = tablemedecin.CodeIntervenant) ON facture.NFacture = facture_status.NFacture) ON tablepatient.IdPatient = tableconsultations.IndicePatient ";
			sql = sql + "WHERE (((facture.NFacture)>44) AND ((facture_status.FacDateEnvoyee) Is Not Null And (facture_status.FacDateEnvoyee)<'" + fin1 +"') AND (tableactes.DAP >'" + Debut1 +"') AND (tableactes.DAP <'" + fin1 +"') AND ((tableactes.CodeIntervenant)<>2536) AND ((facture_status.FacDateAnnulee) Is Null) AND ((facture_status.FacDateEncaissee) Is Null) AND ((facture.Solde)>20) AND ((facture_status.FacDateAcquittee)>'" + fin1 +"' Or (facture_status.FacDateAcquittee) Is Null)) ORDER BY tablepersonne.Nom ,tablepersonne.Prenom";

            OutilsExt.OutilsSql.RemplitDataTable(ImportSosGeneve.Donnees.MesFactures.Tables[0], sql);
			if(ImportSosGeneve.Donnees.MesFactures.Tables.Count>0 && ImportSosGeneve.Donnees.MesFactures.Tables[0].Rows.Count>0)
			{
				frmFacturesEncMed imprDebiteurs = new frmFacturesEncMed(ImportSosGeneve.Donnees.MesFactures, mois, year, fin1,"TotalDebiteurs");
				imprDebiteurs.ShowDialog();
				imprDebiteurs.Dispose();
				imprDebiteurs=null;
			}
		}

        private void buttonListeMedec_Click(object sender, EventArgs e)
        {
            ListeMedecinscs lm = new ListeMedecinscs();
            lm.ShowDialog();
        }
		
	}
}
