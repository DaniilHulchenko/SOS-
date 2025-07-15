using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace ImportSosGeneve
{
	public class frmFacHisto : System.Windows.Forms.Form
	{
		#region Déclaration des variables

		// Variables perso
		private DataRow m_RowSelected=null;
		//private DataTable m_Rows=null;
		private frmGeneral m_frmgeneral=null;
		private int[] IdPatients = null;


		// Variables controles du formulaire

		private System.Windows.Forms.GroupBox groupH;
		private System.Windows.Forms.TextBox txtDateFin;
		private System.Windows.Forms.TextBox txtDatedu;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox cbMEd;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.CheckBox chk2rap;
		private System.Windows.Forms.CheckBox chk1rap;
		private System.Windows.Forms.CheckBox chk3Rap;
		private FarPoint.Win.Spread.FpSpread fpHistoriqueFacture;
		private FarPoint.Win.Spread.SheetView fpHistoriqueFacture_Sheet1;
		private FarPoint.Win.Spread.SheetView fpHistoriqueFacture_Sheet2;
		private System.Windows.Forms.TextBox txtNConsult;
		private System.Windows.Forms.TextBox txtNFac;
		private System.Windows.Forms.Button btnZoom;
		private System.Windows.Forms.Button btDupliacata;
		private System.Windows.Forms.Button btAnnuler;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Button btPaiement;
        private System.Windows.Forms.TextBox TxtTotalFacture;
		private System.Windows.Forms.Label label20;

		#endregion

		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Button btdecompte;
		private System.Windows.Forms.TextBox TxtSoldeFacture;
		private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btRapportFac;
        private Label label4;
        private Label label8;
		private System.ComponentModel.IContainer components;
		

		#region Construction / Destruction du formulaire

		// Constructeur sans paramètre
		public frmFacHisto(frmGeneral frmgeneral)
		{
			this.m_frmgeneral = frmgeneral;

			InitializeComponent();
            InitializeControls();		
			InitializeData();
		}

		// Constructeur avec patients en paramètre
		public frmFacHisto(frmGeneral frmgeneral,int[] Patients)
		{
			this.m_frmgeneral = frmgeneral;
			

			InitializeComponent();
			InitializeControls();		
			InitializeData();

			if(Patients!=null)
			{
				IdPatients = Patients;
				
				foreach(int intPatient in Patients)
				{
					DataRow[] rows = OutilsExt.OutilsSql.RecuperationFacturesByIdPatient(intPatient);
					for(int i=0;i<rows.Length;i++)
					{
						int nb= fpHistoriqueFacture_Sheet1.RowCount++;
						ChargementLigneFacture(rows[i],nb);
						
					}
				}
				
				
			}
		}

		// Constructeur avec paramètreq
		public frmFacHisto(frmGeneral frmgeneral,DataRow FactureByDefault)
		{
			this.m_frmgeneral = frmgeneral;

			InitializeComponent();
			InitializeControls();		
			InitializeData();

			if(FactureByDefault!=null)
			{
				// On charge la facture passée en paramètre puis on simule le clic sur celle-ci
                DataRow[] rows = OutilsExt.OutilsSql.RecuperationFacturesByNFacture(long.Parse(FactureByDefault["NFacture"].ToString()));
				if(rows!=null && rows.Length==1)
				{
					fpHistoriqueFacture_Sheet1.RowCount=1;
					ChargementLigneFacture(rows[0],0);
					AfficheHistoFacture(rows[0]);
				}
			}
		}

        // Destruction des objets du formulaire
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFacHisto));
            this.groupH = new System.Windows.Forms.GroupBox();
            this.btRapportFac = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.chk3Rap = new System.Windows.Forms.CheckBox();
            this.chk2rap = new System.Windows.Forms.CheckBox();
            this.chk1rap = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbMEd = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDateFin = new System.Windows.Forms.TextBox();
            this.txtDatedu = new System.Windows.Forms.TextBox();
            this.txtNFac = new System.Windows.Forms.TextBox();
            this.txtNConsult = new System.Windows.Forms.TextBox();
            this.fpHistoriqueFacture = new FarPoint.Win.Spread.FpSpread();
            this.fpHistoriqueFacture_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.fpHistoriqueFacture_Sheet2 = new FarPoint.Win.Spread.SheetView();
            this.TxtTotalFacture = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btdecompte = new System.Windows.Forms.Button();
            this.btPaiement = new System.Windows.Forms.Button();
            this.btAnnuler = new System.Windows.Forms.Button();
            this.btDupliacata = new System.Windows.Forms.Button();
            this.btnZoom = new System.Windows.Forms.Button();
            this.TxtSoldeFacture = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupH.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpHistoriqueFacture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpHistoriqueFacture_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpHistoriqueFacture_Sheet2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupH
            // 
            this.groupH.BackColor = System.Drawing.Color.Transparent;
            this.groupH.Controls.Add(this.btRapportFac);
            this.groupH.Controls.Add(this.pictureBox1);
            this.groupH.Controls.Add(this.chk3Rap);
            this.groupH.Controls.Add(this.chk2rap);
            this.groupH.Controls.Add(this.chk1rap);
            this.groupH.Controls.Add(this.label5);
            this.groupH.Controls.Add(this.cbMEd);
            this.groupH.Controls.Add(this.label4);
            this.groupH.Controls.Add(this.label3);
            this.groupH.Controls.Add(this.label2);
            this.groupH.Controls.Add(this.label1);
            this.groupH.Controls.Add(this.txtDateFin);
            this.groupH.Controls.Add(this.txtDatedu);
            this.groupH.Controls.Add(this.txtNFac);
            this.groupH.Controls.Add(this.txtNConsult);
            this.groupH.Location = new System.Drawing.Point(8, 8);
            this.groupH.Name = "groupH";
            this.groupH.Size = new System.Drawing.Size(904, 72);
            this.groupH.TabIndex = 13;
            this.groupH.TabStop = false;
            // 
            // btRapportFac
            // 
            this.btRapportFac.Location = new System.Drawing.Point(776, 16);
            this.btRapportFac.Name = "btRapportFac";
            this.btRapportFac.Size = new System.Drawing.Size(120, 40);
            this.btRapportFac.TabIndex = 18;
            this.btRapportFac.Text = "Imprimer Rapport Factures";
            this.btRapportFac.Click += new System.EventHandler(this.btRapportFac_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(680, 48);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(72, 16);
            this.pictureBox1.TabIndex = 17;
            this.pictureBox1.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox1, "Vider les critères");
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // chk3Rap
            // 
            this.chk3Rap.BackColor = System.Drawing.Color.Transparent;
            this.chk3Rap.Location = new System.Drawing.Point(576, 48);
            this.chk3Rap.Name = "chk3Rap";
            this.chk3Rap.Size = new System.Drawing.Size(104, 16);
            this.chk3Rap.TabIndex = 16;
            this.chk3Rap.Text = "3 éme rappel";
            this.chk3Rap.UseVisualStyleBackColor = false;
            // 
            // chk2rap
            // 
            this.chk2rap.Location = new System.Drawing.Point(576, 32);
            this.chk2rap.Name = "chk2rap";
            this.chk2rap.Size = new System.Drawing.Size(96, 16);
            this.chk2rap.TabIndex = 15;
            this.chk2rap.Text = "2 éme rappel";
            // 
            // chk1rap
            // 
            this.chk1rap.Location = new System.Drawing.Point(576, 16);
            this.chk1rap.Name = "chk1rap";
            this.chk1rap.Size = new System.Drawing.Size(80, 16);
            this.chk1rap.TabIndex = 14;
            this.chk1rap.Text = "1 er rappel";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(349, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 16);
            this.label5.TabIndex = 13;
            this.label5.Text = "Médecin";
            // 
            // cbMEd
            // 
            this.cbMEd.Location = new System.Drawing.Point(352, 32);
            this.cbMEd.Name = "cbMEd";
            this.cbMEd.Size = new System.Drawing.Size(208, 21);
            this.cbMEd.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(263, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 16);
            this.label4.TabIndex = 11;
            this.label4.Text = "au";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(5, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "N°Consult.";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(91, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 16);
            this.label2.TabIndex = 9;
            this.label2.Text = "N° Fac.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(182, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Du";
            // 
            // txtDateFin
            // 
            this.txtDateFin.Location = new System.Drawing.Point(266, 32);
            this.txtDateFin.Name = "txtDateFin";
            this.txtDateFin.Size = new System.Drawing.Size(80, 20);
            this.txtDateFin.TabIndex = 7;
            // 
            // txtDatedu
            // 
            this.txtDatedu.Location = new System.Drawing.Point(180, 32);
            this.txtDatedu.Name = "txtDatedu";
            this.txtDatedu.Size = new System.Drawing.Size(80, 20);
            this.txtDatedu.TabIndex = 6;
            // 
            // txtNFac
            // 
            this.txtNFac.Location = new System.Drawing.Point(94, 32);
            this.txtNFac.Name = "txtNFac";
            this.txtNFac.Size = new System.Drawing.Size(80, 20);
            this.txtNFac.TabIndex = 5;
            this.txtNFac.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtNFax_KeyUp);
            // 
            // txtNConsult
            // 
            this.txtNConsult.Location = new System.Drawing.Point(8, 32);
            this.txtNConsult.Name = "txtNConsult";
            this.txtNConsult.Size = new System.Drawing.Size(80, 20);
            this.txtNConsult.TabIndex = 4;
            this.txtNConsult.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtNConsult_KeyUp);
            // 
            // fpHistoriqueFacture
            // 
            this.fpHistoriqueFacture.AccessibleDescription = "";
            this.fpHistoriqueFacture.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.fpHistoriqueFacture.Location = new System.Drawing.Point(8, 88);
            this.fpHistoriqueFacture.Name = "fpHistoriqueFacture";
            this.fpHistoriqueFacture.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpHistoriqueFacture_Sheet1,
            this.fpHistoriqueFacture_Sheet2});
            this.fpHistoriqueFacture.Size = new System.Drawing.Size(904, 272);
            this.fpHistoriqueFacture.TabIndex = 18;
            this.fpHistoriqueFacture.MouseUp += new System.Windows.Forms.MouseEventHandler(this.fpHistoriqueFacture_MouseUp);
            this.fpHistoriqueFacture.SetActiveViewport(0, -1, -1);
            this.fpHistoriqueFacture.SetActiveViewport(1, -1, -1);
            // 
            // fpHistoriqueFacture_Sheet1
            // 
            this.fpHistoriqueFacture_Sheet1.Reset();
            this.fpHistoriqueFacture_Sheet1.SheetName = "Factures filtrées";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpHistoriqueFacture_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            fpHistoriqueFacture_Sheet1.ColumnCount = 12;
            fpHistoriqueFacture_Sheet1.ColumnHeader.RowCount = 2;
            fpHistoriqueFacture_Sheet1.RowCount = 0;
            fpHistoriqueFacture_Sheet1.RowHeader.ColumnCount = 0;
            this.fpHistoriqueFacture_Sheet1.ActiveColumnIndex = -1;
            this.fpHistoriqueFacture_Sheet1.ActiveRowIndex = -1;
            this.fpHistoriqueFacture_Sheet1.Models = ((FarPoint.Win.Spread.SheetView.DocumentModels)(resources.GetObject("fpHistoriqueFacture_Sheet1.Models")));
            this.fpHistoriqueFacture_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // fpHistoriqueFacture_Sheet2
            // 
            this.fpHistoriqueFacture_Sheet2.Reset();
            this.fpHistoriqueFacture_Sheet2.SheetName = "Historique d\'une facture";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpHistoriqueFacture_Sheet2.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            fpHistoriqueFacture_Sheet2.ColumnCount = 8;
            fpHistoriqueFacture_Sheet2.RowCount = 0;
            fpHistoriqueFacture_Sheet2.RowHeader.ColumnCount = 0;
            this.fpHistoriqueFacture_Sheet2.ActiveColumnIndex = -1;
            this.fpHistoriqueFacture_Sheet2.ActiveRowIndex = -1;
            this.fpHistoriqueFacture_Sheet2.Models = ((FarPoint.Win.Spread.SheetView.DocumentModels)(resources.GetObject("fpHistoriqueFacture_Sheet2.Models")));
            this.fpHistoriqueFacture_Sheet2.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // TxtTotalFacture
            // 
            this.TxtTotalFacture.Font = new System.Drawing.Font("Comic Sans MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtTotalFacture.ForeColor = System.Drawing.Color.Black;
            this.TxtTotalFacture.Location = new System.Drawing.Point(728, 392);
            this.TxtTotalFacture.Name = "TxtTotalFacture";
            this.TxtTotalFacture.Size = new System.Drawing.Size(80, 28);
            this.TxtTotalFacture.TabIndex = 57;
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(813, 400);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(32, 16);
            this.label20.TabIndex = 59;
            this.label20.Text = "SFr.";
            // 
            // btdecompte
            // 
            this.btdecompte.BackgroundImage = global::ImportSosGeneve.Properties.Resources.Decompte;
            this.btdecompte.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btdecompte.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btdecompte.Location = new System.Drawing.Point(258, 371);
            this.btdecompte.Name = "btdecompte";
            this.btdecompte.Size = new System.Drawing.Size(56, 56);
            this.btdecompte.TabIndex = 60;
            this.toolTip1.SetToolTip(this.btdecompte, "Decompte de facture");
            this.btdecompte.UseVisualStyleBackColor = false;
            this.btdecompte.Click += new System.EventHandler(this.btdecompte_Click);
            // 
            // btPaiement
            // 
            this.btPaiement.BackgroundImage = global::ImportSosGeneve.Properties.Resources.Argent;
            this.btPaiement.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btPaiement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btPaiement.Location = new System.Drawing.Point(196, 371);
            this.btPaiement.Name = "btPaiement";
            this.btPaiement.Size = new System.Drawing.Size(56, 56);
            this.btPaiement.TabIndex = 23;
            this.toolTip1.SetToolTip(this.btPaiement, "Paiement isolé");
            this.btPaiement.UseVisualStyleBackColor = false;
            this.btPaiement.Click += new System.EventHandler(this.btPaiement_Click);
            // 
            // btAnnuler
            // 
            this.btAnnuler.BackgroundImage = global::ImportSosGeneve.Properties.Resources.Fermer;
            this.btAnnuler.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btAnnuler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btAnnuler.Location = new System.Drawing.Point(320, 371);
            this.btAnnuler.Name = "btAnnuler";
            this.btAnnuler.Size = new System.Drawing.Size(56, 56);
            this.btAnnuler.TabIndex = 21;
            this.toolTip1.SetToolTip(this.btAnnuler, "Annulation/Ré-édition de facture");
            this.btAnnuler.UseVisualStyleBackColor = false;
            this.btAnnuler.Click += new System.EventHandler(this.btAnnuler_Click);
            // 
            // btDupliacata
            // 
            this.btDupliacata.BackgroundImage = global::ImportSosGeneve.Properties.Resources.Dupilicata;
            this.btDupliacata.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btDupliacata.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btDupliacata.Location = new System.Drawing.Point(72, 371);
            this.btDupliacata.Name = "btDupliacata";
            this.btDupliacata.Size = new System.Drawing.Size(56, 56);
            this.btDupliacata.TabIndex = 20;
            this.toolTip1.SetToolTip(this.btDupliacata, "Duplicata d\'une facture");
            this.btDupliacata.UseVisualStyleBackColor = false;
            this.btDupliacata.Click += new System.EventHandler(this.btDupliacata_Click);
            // 
            // btnZoom
            // 
            this.btnZoom.BackgroundImage = global::ImportSosGeneve.Properties.Resources.Zoom;
            this.btnZoom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnZoom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZoom.Location = new System.Drawing.Point(10, 371);
            this.btnZoom.Name = "btnZoom";
            this.btnZoom.Size = new System.Drawing.Size(56, 56);
            this.btnZoom.TabIndex = 19;
            this.toolTip1.SetToolTip(this.btnZoom, "Affichage d\'une facture");
            this.btnZoom.UseVisualStyleBackColor = false;
            this.btnZoom.Click += new System.EventHandler(this.btnZoom_Click);
            // 
            // TxtSoldeFacture
            // 
            this.TxtSoldeFacture.Font = new System.Drawing.Font("Comic Sans MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSoldeFacture.ForeColor = System.Drawing.Color.Black;
            this.TxtSoldeFacture.Location = new System.Drawing.Point(536, 392);
            this.TxtSoldeFacture.Name = "TxtSoldeFacture";
            this.TxtSoldeFacture.Size = new System.Drawing.Size(80, 28);
            this.TxtSoldeFacture.TabIndex = 61;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(616, 400);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 16);
            this.label6.TabIndex = 63;
            this.label6.Text = "SFr.";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(459, 392);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 28);
            this.label7.TabIndex = 64;
            this.label7.Text = "SOLDE:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.White;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(650, 392);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 28);
            this.label8.TabIndex = 66;
            this.label8.Text = "TOTAL :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmFacHisto
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.ClientSize = new System.Drawing.Size(920, 438);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.TxtSoldeFacture);
            this.Controls.Add(this.TxtTotalFacture);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btdecompte);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.btPaiement);
            this.Controls.Add(this.btAnnuler);
            this.Controls.Add(this.btDupliacata);
            this.Controls.Add(this.btnZoom);
            this.Controls.Add(this.fpHistoriqueFacture);
            this.Controls.Add(this.groupH);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmFacHisto";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Historique Filtrées";
            this.groupH.ResumeLayout(false);
            this.groupH.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpHistoriqueFacture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpHistoriqueFacture_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpHistoriqueFacture_Sheet2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		#endregion

		#region Mise en place des controles

		// Initialisation de tous les controles
		private void InitializeControls()
		{
			InitializeRecherche();
			InitializeSpread1();
		}

        // Initialisation des controles de la zone de recherche
		private void InitializeRecherche()
		{
			txtNConsult.Text="";
			txtNFac.Text="";
			txtDatedu.Text="";
			txtDateFin.Text="";

			// Remplit la liste des médecins : 
			FillCbMed();
		}

		// Initialisation du spread de factures filtrées
		private void InitializeSpread1()
		{
			// Vidage des lignes
			fpHistoriqueFacture_Sheet1.RowCount=0;

			// Premieres lignes fusionnées
			fpHistoriqueFacture_Sheet1.Models.ColumnHeaderSpan.Add(0,0,1,3);
			fpHistoriqueFacture_Sheet1.Models.ColumnHeaderSpan.Add(0,3,1,1);
			fpHistoriqueFacture_Sheet1.Models.ColumnHeaderSpan.Add(0,4,1,7);
			// En-tete de rubriques de colonne
			fpHistoriqueFacture_Sheet1.ColumnHeader.Cells[0,0].Text="Consultation";
			fpHistoriqueFacture_Sheet1.ColumnHeader.Cells[0,0,0,4].BackColor = Color.LightSteelBlue;
			fpHistoriqueFacture_Sheet1.ColumnHeader.Cells[0,0,0,3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
			fpHistoriqueFacture_Sheet1.ColumnHeader.Cells[0,4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
			fpHistoriqueFacture_Sheet1.ColumnHeader.Cells[0,3].Text="Patient";
			fpHistoriqueFacture_Sheet1.ColumnHeader.Cells[0,4].Text="Facture";

			// Largeurs de colonnes
			fpHistoriqueFacture_Sheet1.Columns[0].Width=8*fpHistoriqueFacture.Width/100;
			fpHistoriqueFacture_Sheet1.Columns[1].Width=8*fpHistoriqueFacture.Width/100;
			fpHistoriqueFacture_Sheet1.Columns[2].Width=17*fpHistoriqueFacture.Width/100;
			fpHistoriqueFacture_Sheet1.Columns[3].Width=25*fpHistoriqueFacture.Width/100;
			fpHistoriqueFacture_Sheet1.Columns[4].Width=8*fpHistoriqueFacture.Width/100;
			fpHistoriqueFacture_Sheet1.Columns[5].Width=8*fpHistoriqueFacture.Width/100;
			fpHistoriqueFacture_Sheet1.Columns[6].Width=8*fpHistoriqueFacture.Width/100;
			fpHistoriqueFacture_Sheet1.Columns[7].Width=5*fpHistoriqueFacture.Width/100;
			fpHistoriqueFacture_Sheet1.Columns[8].Width=5*fpHistoriqueFacture.Width/100;
			fpHistoriqueFacture_Sheet1.Columns[9].Width=5*fpHistoriqueFacture.Width/100;
			fpHistoriqueFacture_Sheet1.Columns[10].Width=5*fpHistoriqueFacture.Width/100;
			fpHistoriqueFacture_Sheet1.Columns[11].Width=150*fpHistoriqueFacture.Width/100;

			// En-tetes de colonnes
			fpHistoriqueFacture_Sheet1.ColumnHeader.Cells[1,0].Text="n°";
			fpHistoriqueFacture_Sheet1.ColumnHeader.Cells[1,1].Text="Date";
			fpHistoriqueFacture_Sheet1.ColumnHeader.Cells[1,2].Text="Médecin";
			fpHistoriqueFacture_Sheet1.ColumnHeader.Cells[1,3].Text="Nom,Prénom";
			fpHistoriqueFacture_Sheet1.ColumnHeader.Cells[1,4].Text="n°";
			fpHistoriqueFacture_Sheet1.ColumnHeader.Cells[1,5].Text="Montant";
			fpHistoriqueFacture_Sheet1.ColumnHeader.Cells[1,6].Text="Solde";
			fpHistoriqueFacture_Sheet1.ColumnHeader.Cells[1,7].Text="Acq";
			fpHistoriqueFacture_Sheet1.ColumnHeader.Cells[1,8].Text="Dup";
			fpHistoriqueFacture_Sheet1.ColumnHeader.Cells[1,9].Text="Annul";
			fpHistoriqueFacture_Sheet1.ColumnHeader.Cells[1,10].Text="Man";
			fpHistoriqueFacture_Sheet1.ColumnHeader.Cells[1,11].Text="Commentaire";
		}
		#endregion

		#region Chargement de données

		#region Dans les recherches

		private void InitializeData()
		{
			FillCbMed();
		}

		// On remplit la liste avec les médecins Sos
		private void FillCbMed()
		{
			cbMEd.Items.Clear();
            string[][] tabMed = OutilsExt.OutilsSql.ExecuteCommandeAvecTabString("SELECT CodeIntervenant,Nom from tablemedecin order by Nom");
			foreach(string[] s in tabMed)
			{
				ListItem item = new ListItem(s[0],s[1]);
				cbMEd.Items.Add(item);
			}
		}

		private void FillSpreadFactures(DataRow[] rowsFacture)
		{
			if(rowsFacture==null) return;

			fpHistoriqueFacture_Sheet1.RowCount=0;
			
			for(int i=0;i<rowsFacture.Length;i++)
			{
				int nb = fpHistoriqueFacture_Sheet1.RowCount++;

				ChargementLigneFacture(rowsFacture[i],nb);				
			}
		}

		private void ChargementLigneFacture(DataRow rowFacture,int nb)
		{
			if(nb%2==0)
				fpHistoriqueFacture_Sheet1.Rows[nb].BackColor = Color.PaleGreen;
			else
				fpHistoriqueFacture_Sheet1.Rows[nb].BackColor = Color.White;

			// on recupere la consultation qui va avec chaque facture :
			long NConsult = long.Parse( rowFacture["NConsultation"].ToString());
			long NFacture = long.Parse( rowFacture["NFacture"].ToString());
		

			fpHistoriqueFacture_Sheet1.Rows[nb].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
			fpHistoriqueFacture_Sheet1.Cells[nb,0,nb,9].Locked = true;
			fpHistoriqueFacture_Sheet1.Cells[nb,6,nb,9].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

			DataSet ds = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet("SELECT m.Nom as 'NomMedecinSos',c.IndicePatient,a.DAP,a.DSL,pe.Nom as 'NomPatient' ,pe.Prenom as 'PrenomPatient' from tableactes a left join tableconsultations c    on c.CodeAppel = a.Num left join tablemedecin m on m.CodeIntervenant = a.CodeIntervenant left join tablepatient pa on pa.IdPatient = c.IndicePatient left join tablepersonne pe on pe.IdPersonne = pa.IdPersonne WHERE c.NConsultation = " + NConsult);
			if(ds==null || ds.Tables.Count==0 || ds.Tables[0].Rows.Count==0) return;
                
			fpHistoriqueFacture_Sheet1.Cells[nb,0].Text = NConsult.ToString();
			fpHistoriqueFacture_Sheet1.Rows[nb].Tag = rowFacture;

			if(ds.Tables[0].Rows[0]["DSL"].ToString()!=System.DBNull.Value.ToString())
				fpHistoriqueFacture_Sheet1.Cells[nb,1].Text = DateTime.Parse(ds.Tables[0].Rows[0]["DSL"].ToString()).ToString("dd/MM/yyyy");
			else
				fpHistoriqueFacture_Sheet1.Cells[nb,1].Text = DateTime.Parse(ds.Tables[0].Rows[0]["DAP"].ToString()).ToString("dd/MM/yyyy");

			fpHistoriqueFacture_Sheet1.Cells[nb,2].Text = ds.Tables[0].Rows[0]["NomMedecinSos"].ToString();
			fpHistoriqueFacture_Sheet1.Cells[nb,3].Text = ds.Tables[0].Rows[0]["NomPatient"].ToString() + " " + ds.Tables[0].Rows[0]["PrenomPatient"].ToString();
			fpHistoriqueFacture_Sheet1.Cells[nb,4].Text = NFacture.ToString();

			fpHistoriqueFacture_Sheet1.Cells[nb,5].Text = string.Format("{0:0.##}", float.Parse(rowFacture["TotalFacture"].ToString()));
            fpHistoriqueFacture_Sheet1.Cells[nb, 6].Text = string.Format("{0:0.##}", float.Parse(rowFacture["Solde"].ToString()));

			FarPoint.Win.Spread.CellType.CheckBoxCellType chk1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
			fpHistoriqueFacture_Sheet1.Cells[nb,7].CellType = chk1;
			FarPoint.Win.Spread.CellType.CheckBoxCellType chk2 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
			fpHistoriqueFacture_Sheet1.Cells[nb,8].CellType = chk2;
			FarPoint.Win.Spread.CellType.CheckBoxCellType chk3 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
			fpHistoriqueFacture_Sheet1.Cells[nb,9].CellType = chk3;
			FarPoint.Win.Spread.CellType.CheckBoxCellType chk4 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
			fpHistoriqueFacture_Sheet1.Cells[nb,10].CellType = chk4;
				
			if(rowFacture["FacDateAcquittee"].ToString()!=System.DBNull.Value.ToString())
				fpHistoriqueFacture_Sheet1.Cells[nb,7].Value=true;
			if(rowFacture["FacDateDuplicata"].ToString()!=System.DBNull.Value.ToString())
				fpHistoriqueFacture_Sheet1.Cells[nb,8].Value=true;
			if(rowFacture["FacDateAnnulee"].ToString()!=System.DBNull.Value.ToString())
				fpHistoriqueFacture_Sheet1.Cells[nb,9].Value=true;
			

								
			fpHistoriqueFacture_Sheet1.Cells[nb,11].Text = rowFacture["Commentaire"].ToString();

			CalculeSoldeTotal();
			
		}

		private void CalculeSoldeTotal()
		{
			//calcul le total et le solde des factures de patient
			float Total = 0;
            float Solde = 0;
			int cb = fpHistoriqueFacture_Sheet1.RowCount;
			for(int i=0;i<fpHistoriqueFacture_Sheet1.RowCount;i++)
			{
                Total += float.Parse(fpHistoriqueFacture_Sheet1.Cells[i, 5].Text);
                Solde += float.Parse(fpHistoriqueFacture_Sheet1.Cells[i, 6].Text);
			}
            TxtTotalFacture.Text = string.Format("{0:0.##}",Total);
            TxtSoldeFacture.Text = string.Format("{0:0.##}", Solde);
		}

		#endregion

		#endregion

		#region Recherche de factures
	
		private void txtNConsult_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Enter && txtNConsult.Text!="")
			{
                DataRow[] rows = OutilsExt.OutilsSql.RecuperationFacturesByNConsult(long.Parse(txtNConsult.Text));
				FillSpreadFactures(rows);
			}
		}

		private void txtNFax_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Enter && txtNFac.Text!="")
			{
                DataRow[] rows = OutilsExt.OutilsSql.RecuperationFacturesByNFacture(long.Parse(txtNFac.Text));
				FillSpreadFactures(rows);
			}
		}

		#endregion
		
		private void btnZoom_Click(object sender, System.EventArgs e)
		{
			if(m_RowSelected!=null)
			{
				ZoomSurFacture(long.Parse(m_RowSelected["NConsultation"].ToString()));
				this.Hide();
			}
		}

		private void ZoomSurFacture(long NConsultation)
		{
			m_frmgeneral.OuvreFactureFromHistoFac(this,NConsultation);
		}

		private void fpHistoriqueFacture_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			FarPoint.Win.Spread.Model.CellRange range = fpHistoriqueFacture.GetCellFromPixel(0,0,e.X,e.Y);

			if(fpHistoriqueFacture.ActiveSheetIndex==0)
			{
				if(range.Row>-1)
				{
					m_RowSelected = (DataRow)fpHistoriqueFacture_Sheet1.Rows[range.Row].Tag;
					fpHistoriqueFacture_Sheet1.ActiveRowIndex = range.Row;
					fpHistoriqueFacture_Sheet1.AddSelection(range.Row,0,1,10);

					AfficheHistoFacture(m_RowSelected);
				}
			}
		}

		private void AfficheHistoFacture(System.Data.DataRow row)
		{
			fpHistoriqueFacture_Sheet2.RowCount = 0;
			string[][] histo = OutilsExt.OutilsSql.GetEtatsFacture(long.Parse(row["NFacture"].ToString()));
			if(histo!=null)
			{
				foreach(string[] s in histo)
				{
					int nb = fpHistoriqueFacture_Sheet2.RowCount++;
					fpHistoriqueFacture_Sheet2.Rows[nb].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
					fpHistoriqueFacture_Sheet2.Cells[nb,0].Text = s[0];
					fpHistoriqueFacture_Sheet2.Cells[nb,1].Text = DateTime.Parse(s[1]).ToString("dd/MM/yyyy");
					fpHistoriqueFacture_Sheet2.Cells[nb,2].Text = DateTime.Parse(s[2]).ToString("dd/MM/yyyy");
                    fpHistoriqueFacture_Sheet2.Cells[nb, 3].Text = string.Format("{0:0.##}", float.Parse(s[3].ToString()));
					fpHistoriqueFacture_Sheet2.Cells[nb,4].Text = s[4];
					fpHistoriqueFacture_Sheet2.Cells[nb,5].Text = s[5];
					fpHistoriqueFacture_Sheet2.Cells[nb,6].Text = s[6];
					fpHistoriqueFacture_Sheet2.Cells[nb,7].Text = s[7];
				}
            }
		}

		private void btDupliacata_Click(object sender, System.EventArgs e)
		{
			if(m_RowSelected!=null)
			{
				frmFacDuplicata DuplicFacture = new frmFacDuplicata(m_RowSelected);
				DuplicFacture.ShowDialog();
				bool bOk = DuplicFacture.bOk;
				DuplicFacture.Dispose();
				DuplicFacture=null;
				if(bOk)
				{
                    DataRow[] rows = OutilsExt.OutilsSql.RecuperationFacturesByNFacture(long.Parse(m_RowSelected["NFacture"].ToString()));
					if(rows!=null && rows.Length==1)
					{
						ChargementLigneFacture(rows[0],fpHistoriqueFacture_Sheet1.ActiveRowIndex);
						AfficheHistoFacture(rows[0]);
					}
				}
			}			
		}

		private void btAnnuler_Click(object sender, System.EventArgs e)
		{		
			if(m_RowSelected!=null)
			{
                SosMedecins.SmartRapport.Facturation.frmFacAnnuler AnnulFacture = new SosMedecins.SmartRapport.Facturation.frmFacAnnuler(m_RowSelected);
				//frmFacAnnuler AnnulFacture = new frmFacAnnuler(m_RowSelected);
				AnnulFacture.ShowDialog();
				bool bOk = AnnulFacture.bOk;
				AnnulFacture.Dispose();
				AnnulFacture=null;
				if(bOk)
				{
                    DataRow[] rows = OutilsExt.OutilsSql.RecuperationFacturesByNFacture(long.Parse(m_RowSelected["NFacture"].ToString()));
					if(rows!=null && rows.Length==1)
					{
						ChargementLigneFacture(rows[0],fpHistoriqueFacture_Sheet1.ActiveRowIndex);
						AfficheHistoFacture(rows[0]);
					}
				}
			}		
		}

		private void btPaiement_Click(object sender, System.EventArgs e)
		{
			if(m_RowSelected!=null)
			{
				frmFacPaiement paiement = new frmFacPaiement(m_RowSelected);

				if(paiement.ShowDialog() == DialogResult.OK)
				{
                    DataRow[] rows = OutilsExt.OutilsSql.RecuperationFacturesByNFacture(long.Parse(m_RowSelected["NFacture"].ToString()));
					if(rows!=null && rows.Length==1)
					{
						ChargementLigneFacture(rows[0],fpHistoriqueFacture_Sheet1.ActiveRowIndex);
						AfficheHistoFacture(rows[0]);
					}
				}
                paiement.Dispose();
                paiement = null;
            }		
		}

		private void pictureBox1_Click(object sender, System.EventArgs e)
		{
			txtNConsult.Text="";
			txtNFac.Text="";
			txtDatedu.Text="";
			txtDateFin.Text="";
			cbMEd.Text="";
			cbMEd.SelectedIndex=-1;
			chk1rap.Checked = false;
			chk2rap.Checked = false;
			chk3Rap.Checked = false;
			fpHistoriqueFacture_Sheet1.RowCount=0;
			fpHistoriqueFacture_Sheet2.RowCount=0;
		}

		private void btdecompte_Click(object sender, System.EventArgs e)
		{
            this.Cursor = Cursors.WaitCursor;
			//set month name and year
			DateTime DtDebut = DateTime.Now;
            string fin1 = "";
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

			string year = DtDebut.Year.ToString();
			string id = "";
			// Chargement des factures dans un dataset typé :
			if(IdPatients!=null)
			{
				
				id = " AND (IdPatient = ";
				foreach(int IntPatients in IdPatients)
				{
					id = id + "'" + IntPatients.ToString() + "' or IdPatient =";
				}
				id = id.Substring(0,id.Length-15)+")";
				
			}
			string sql = "";
			if (txtDatedu.Text != "" && txtDateFin.Text != "")
			{
			    fin1 = txtDateFin.Text.ToString();
				string Debut1 = txtDatedu.Text.ToString();
				Debut1 = Debut1.Replace(".","-");
				fin1 = fin1.Replace(".","-");
				string[] day = Debut1.Split('-');
				Debut1 = day[2].ToString()+"-"+day[1].ToString()+"-"+day[0].ToString();
				day = fin1.Split('-');
				fin1 = day[2].ToString()+"-"+day[1].ToString()+"-"+day[0].ToString();
                ImportSosGeneve.Donnees.MesFactures = new SosMedecins.SmartRapport.DAL.dstFacturesEncMed();

                sql ="SELECT facture.NFacture, tableactes.DAP, facture_etats.DateOp AS DateOp, facture_etats.Montant, facture.TotalFacture, facture.Solde AS SoldeFacture, facture_etats.Etat, tablemedecin.Nom AS 'NomMED', tablepersonne.Sexe AS Sex, (tablepersonne.Prenom + ' ' + tablepersonne.Nom) AS NomPatient, facture.AdresseDestinataire, facture_status.FacDateAcquittee, facture_status.FacDateEnvoyee, facture_status.FacDateAnnulee, tablemedecin.CodeIntervenant, tablepatient.IdPatient , tablepersonne.Adm_Rue, tablepersonne.Adm_NumeroDansRue, tablepersonne.Adm_CodePostal, tablepersonne.Adm_Commune, tablepersonne.Chez ";
                sql = sql + " FROM (((((tableactes INNER JOIN ((facture_status INNER JOIN factureconsultation ON facture_status.NFacture = factureconsultation.NFacture) INNER JOIN tableconsultations ON factureconsultation.NConsultation = tableconsultations.NConsultation) ON tableactes.Num = tableconsultations.CodeAppel) INNER JOIN tablemedecin ON tableactes.CodeIntervenant = tablemedecin.CodeIntervenant) INNER JOIN tablepatient ON tableconsultations.IndicePatient = tablepatient.IdPatient) INNER JOIN tablepersonne ON tablepatient.IdPersonne = tablepersonne.IdPersonne) INNER JOIN facture ON facture_status.NFacture = facture.NFacture) LEFT JOIN facture_etats ON facture.NFacture = facture_etats.NFacture ";
				sql = sql + " GROUP BY facture.NFacture, tableactes.DAP, facture_etats.Montant, facture.TotalFacture, facture.Solde, facture_etats.Etat, tablemedecin.Nom, tablepersonne.Sexe, (tablepersonne.Prenom + ' ' + tablepersonne.Nom), facture.AdresseDestinataire, facture_status.FacDateAcquittee, facture_status.FacDateEnvoyee, facture_status.FacDateAnnulee, tablemedecin.CodeIntervenant, tablepatient.IdPatient ";
                sql = sql + ", tablepersonne.Adm_Rue, tablepersonne.Adm_NumeroDansRue, tablepersonne.Adm_CodePostal, tablepersonne.Adm_Commune, tablepersonne.Chez";
                sql = sql + " HAVING (((facture.NFacture)>44) AND ((facture.TotalFacture)>0) AND ((SoldeFacture)>0) AND ((facture_etats.Etat)=2 Or (facture_etats.Etat)=6) AND ((facture_status.FacDateAcquittee) Is Null) AND ((facture_status.FacDateEnvoyee) Is Not Null) AND ((facture_status.FacDateAnnulee) Is Null) AND ((tablemedecin.CodeIntervenant)<>2536)) AND tableactes.DAP >'" + Debut1 + "' AND tableactes.DAP <'" + fin1 + "' " + id + " ORDER BY facture.NFacture";
			}
			else
			{
                ImportSosGeneve.Donnees.MesFactures = new SosMedecins.SmartRapport.DAL.dstFacturesEncMed();

                sql = "SELECT facture.NFacture as NFacture, tableactes.DAP, max(facture_etats.DateOp) AS DateOp, facture_etats.Montant, facture.TotalFacture, facture.Solde AS SoldeFacture, facture_etats.Etat, tablemedecin.Nom AS 'NomMED', tablepersonne.Sexe AS Sex, (tablepersonne.Prenom + ' '+ tablepersonne.Nom) AS NomPatient, facture.AdresseDestinataire, facture_status.FacDateAcquittee, facture_status.FacDateEnvoyee, facture_status.FacDateAnnulee, tablemedecin.CodeIntervenant, tablepatient.IdPatient, tablepersonne.Adm_Rue, tablepersonne.Adm_NumeroDansRue, tablepersonne.Adm_CodePostal, tablepersonne.Adm_Commune, tablepersonne.Chez ";
                sql = sql + " FROM tableactes, facture_status, factureconsultation, tableconsultations, tablemedecin, tablepatient, tablepersonne, facture, facture_etats";
                sql = sql + " Where facture_status.NFacture = factureconsultation.NFacture";
                sql = sql + " AND factureconsultation.NConsultation = tableconsultations.NConsultation";
                sql = sql + " AND tableactes.Num = tableconsultations.CodeAppel";
                sql = sql + " AND tableactes.CodeIntervenant = tablemedecin.CodeIntervenant";
                sql = sql + " AND tableconsultations.IndicePatient = tablepatient.IdPatient";
                sql = sql + " AND tablepatient.IdPersonne = tablepersonne.IdPersonne";
                sql = sql + " AND facture_status.NFacture = facture.NFacture";
                sql = sql + " AND facture.NFacture = facture_etats.NFacture";
                sql = sql + " AND facture.NFacture>44";
                sql = sql + " AND facture.TotalFacture>0";
                sql = sql + " AND Solde>0";
                sql = sql + " AND (facture_etats.Etat=2 Or facture_etats.Etat=6)";
                sql = sql + " AND facture_status.FacDateAcquittee Is Null";
                sql = sql + " AND facture_status.FacDateEnvoyee Is Not Null";
                sql = sql + " AND facture_status.FacDateAnnulee Is Null";
                sql = sql + " AND tablemedecin.CodeIntervenant<>2536";
                sql = sql + id ;
                sql = sql + " GROUP BY facture.NFacture, tableactes.DAP, facture_etats.Montant, facture.TotalFacture, facture.Solde, facture_etats.Etat, tablemedecin.Nom, tablepersonne.Sexe, (tablepersonne.Prenom + ' ' + tablepersonne.Nom), facture.AdresseDestinataire, facture_status.FacDateAcquittee, facture_status.FacDateEnvoyee, facture_status.FacDateAnnulee, tablemedecin.CodeIntervenant, tablepatient.IdPatient ";
                sql = sql + ", tablepersonne.Adm_Rue, tablepersonne.Adm_NumeroDansRue, tablepersonne.Adm_CodePostal, tablepersonne.Adm_Commune, tablepersonne.Chez";
                sql = sql + " ORDER BY facture.NFacture";
            }
			
			OutilsExt.OutilsSql.RemplitDataTable(ImportSosGeneve.Donnees.MesFactures.Tables[0], sql);
			for(int i=0;i<ImportSosGeneve.Donnees.MesFactures.Tables[0].Rows.Count;i++)
			{	
				ImportSosGeneve.Donnees.MesFactures.Tables[0].Rows[i]["StrDateAppel"] = ImportSosGeneve.Donnees.MesFactures.Tables[0].Rows[i]["DAP"].ToString();
				ImportSosGeneve.Donnees.MesFactures.Tables[0].Rows[i]["N_Facture"] = ImportSosGeneve.Donnees.MesFactures.Tables[0].Rows[i]["NFacture"].ToString();
			}
            this.Cursor = Cursors.Default;

            if(ImportSosGeneve.Donnees.MesFactures.Tables.Count>0 && ImportSosGeneve.Donnees.MesFactures.Tables[0].Rows.Count>0)
			{
				frmFacturesEncMed imprFacture = new frmFacturesEncMed(ImportSosGeneve.Donnees.MesFactures, mois, year,fin1,null,null,"Decompte");
				imprFacture.ShowDialog();
				imprFacture.Dispose();
				imprFacture=null;
			}
		}

        private void btRapportFac_Click(object sender, System.EventArgs e)
		{
			//set month name and year
			DateTime DtDebut = DateTime.Now;
            string fin1 = "";
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

			string year = DtDebut.Year.ToString();
			string id = "";
			// Chargement des factures dans un dataset typé :
			if(IdPatients!=null)
			{
				id = " AND (IdPatient = ";
				foreach(int IntPatients in IdPatients)
				{
					id = id + "'" + IntPatients.ToString() + "' or IdPatient =";
				}
				id = id.Substring(0,id.Length-15)+")";
			}
			string sql = "";
			if (txtDatedu.Text != "" && txtDateFin.Text != "")
			{
			    fin1 = txtDateFin.Text.ToString();
				string Debut1 = txtDatedu.Text.ToString();
				Debut1 = Debut1.Replace(".","-");
				fin1 = fin1.Replace(".","-");
				string[] day = Debut1.Split('-');
				Debut1 = day[2].ToString()+"-"+day[1].ToString()+"-"+day[0].ToString();
				day = fin1.Split('-');
				fin1 = day[2].ToString()+"-"+day[1].ToString()+"-"+day[0].ToString();
                ImportSosGeneve.Donnees.MesFactures = new SosMedecins.SmartRapport.DAL.dstFacturesEncMed();
				sql ="SELECT facture.NFacture, tableactes.DAP, Max(facture_etats.DateOp) AS DateOp, facture_etats.Montant, facture.TotalFacture, facture.Solde AS SoldeFacture, facture_etats.Etat, tablemedecin.Nom AS 'NomMED', tablepersonne.Sexe AS Sex, (tablepersonne.Prenom + ' ' + tablepersonne.Nom) AS NomPatient, facture.AdresseDestinataire, facture_status.FacDateAcquittee, facture_status.FacDateEnvoyee, facture_status.FacDateAnnulee, tablemedecin.CodeIntervenant, tablepatient.IdPatient , tablepersonne.Adm_Rue, tablepersonne.Adm_NumeroDansRue, tablepersonne.Adm_CodePostal, tablepersonne.Adm_Commune, tablepersonne.Chez ";
				sql = sql + "FROM (((((tableactes INNER JOIN ((facture_status INNER JOIN factureconsultation ON facture_status.NFacture = factureconsultation.NFacture) INNER JOIN tableconsultations ON factureconsultation.NConsultation = tableconsultations.NConsultation) ON tableactes.Num = tableconsultations.CodeAppel) INNER JOIN tablemedecin ON tableactes.CodeIntervenant = tablemedecin.CodeIntervenant) INNER JOIN tablepatient ON tableactes.IndicePatient = tablepatient.IdPatient) INNER JOIN tablepersonne ON tablepatient.IdPersonne = tablepersonne.IdPersonne) INNER JOIN facture ON facture_status.NFacture = facture.NFacture) LEFT JOIN facture_etats ON facture.NFacture = facture_etats.NFacture ";
                sql = sql + "GROUP BY facture.NFacture, tableactes.DAP, facture_etats.Montant, facture.TotalFacture, facture.Solde, facture_etats.Etat, tablemedecin.Nom, tablepersonne.Sexe, (tablepersonne.Prenom+' '+tablepersonne.Nom), facture.AdresseDestinataire, facture_status.FacDateAcquittee, facture_status.FacDateEnvoyee, facture_status.FacDateAnnulee, tablemedecin.CodeIntervenant, tablepatient.IdPatient ";
                sql = sql + ", tablepersonne.Adm_Rue, tablepersonne.Adm_NumeroDansRue, tablepersonne.Adm_CodePostal, tablepersonne.Adm_Commune, tablepersonne.Chez";
                sql = sql + "HAVING (((facture.NFacture)>44) AND ((facture.TotalFacture)>0) AND ((facture_etats.Etat)=2 Or (facture_etats.Etat)=6) AND ((facture_status.FacDateEnvoyee) Is Not Null) AND ((facture_status.FacDateAnnulee) Is Null) AND ((tablemedecin.CodeIntervenant)<>2536)) AND tableactes.DAP >'" + Debut1 + "' AND tableactes.DAP <'" + fin1 + "' " + id + " ORDER BY facture.NFacture";
			}
			else
			{
                ImportSosGeneve.Donnees.MesFactures = new SosMedecins.SmartRapport.DAL.dstFacturesEncMed();

                sql ="SELECT facture.NFacture, tableactes.DAP, Max(facture_etats.DateOp) AS DateOp, facture_etats.Montant, facture.TotalFacture, facture.Solde AS SoldeFacture, facture_etats.Etat, tablemedecin.Nom AS 'NomMED', tablepersonne.Sexe AS Sex, (tablepersonne.Prenom+' '+tablepersonne.Nom) AS NomPatient, facture.AdresseDestinataire, facture_status.FacDateAcquittee, facture_status.FacDateEnvoyee, facture_status.FacDateAnnulee, tablemedecin.CodeIntervenant, tablepatient.IdPatient, tablepersonne.Adm_Rue, tablepersonne.Adm_NumeroDansRue, tablepersonne.Adm_CodePostal, tablepersonne.Adm_Commune, tablepersonne.Chez ";
				sql = sql + "FROM (((((tableactes INNER JOIN ((facture_status INNER JOIN factureconsultation ON facture_status.NFacture = factureconsultation.NFacture) INNER JOIN tableconsultations ON factureconsultation.NConsultation = tableconsultations.NConsultation) ON tableactes.Num = tableconsultations.CodeAppel) INNER JOIN tablemedecin ON tableactes.CodeIntervenant = tablemedecin.CodeIntervenant) INNER JOIN tablepatient ON tableactes.IndicePatient = tablepatient.IdPatient) INNER JOIN tablepersonne ON tablepatient.IdPersonne = tablepersonne.IdPersonne) INNER JOIN facture ON facture_status.NFacture = facture.NFacture) LEFT JOIN facture_etats ON facture.NFacture = facture_etats.NFacture ";
				sql = sql + " GROUP BY facture.NFacture, tableactes.DAP, facture_etats.Montant, facture.TotalFacture, facture.Solde, facture_etats.Etat, tablemedecin.Nom, tablepersonne.Sexe, (tablepersonne.Prenom+' '+tablepersonne.Nom), facture.AdresseDestinataire, facture_status.FacDateAcquittee, facture_status.FacDateEnvoyee, facture_status.FacDateAnnulee, tablemedecin.CodeIntervenant, tablepatient.IdPatient ";
                sql = sql + ", tablepersonne.Adm_Rue, tablepersonne.Adm_NumeroDansRue, tablepersonne.Adm_CodePostal, tablepersonne.Adm_Commune, tablepersonne.Chez";
				sql = sql + " HAVING (((facture.NFacture)>44) AND ((facture.TotalFacture)>0) AND ((facture_etats.Etat)=2 Or (facture_etats.Etat)=6) AND ((facture_status.FacDateEnvoyee) Is Not Null) AND ((facture_status.FacDateAnnulee) Is Null) AND ((tablemedecin.CodeIntervenant)<>2536)) "+ id +" ORDER BY facture.NFacture";
			}

            OutilsExt.OutilsSql.RemplitDataTable(ImportSosGeneve.Donnees.MesFactures.Tables[0], sql);
			for(int i=0;i<ImportSosGeneve.Donnees.MesFactures.Tables[0].Rows.Count;i++)
			{	
				ImportSosGeneve.Donnees.MesFactures.Tables[0].Rows[i]["StrDateAppel"] = ImportSosGeneve.Donnees.MesFactures.Tables[0].Rows[i]["DAP"].ToString();
				ImportSosGeneve.Donnees.MesFactures.Tables[0].Rows[i]["N_Facture"] = ImportSosGeneve.Donnees.MesFactures.Tables[0].Rows[i]["NFacture"].ToString();
			}

            if(ImportSosGeneve.Donnees.MesFactures.Tables.Count>0 && ImportSosGeneve.Donnees.MesFactures.Tables[0].Rows.Count>0)
			{
				frmFacturesEncMed imprFacture = new frmFacturesEncMed(ImportSosGeneve.Donnees.MesFactures, mois, year,fin1,null,null, "Decompte");
				imprFacture.ShowDialog();
				imprFacture.Dispose();
				imprFacture=null;
			}
		}		
	}
}
