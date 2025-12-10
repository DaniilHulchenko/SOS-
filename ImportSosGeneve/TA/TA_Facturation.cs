using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Globalization;
using System.Configuration;
using System.Data.SqlClient;
using SosMedecins.SmartRapport.GestionApplication;
//using static QRCoder.PayloadGenerator;
//using QRCoder;
using ImportSosGeneve.Properties;
using Codecrete.SwissQRBill.Generator;

namespace ImportSosGeneve.TA
{
	/// <summary>
	/// Description résumée de TA_Facturation.
	/// </summary>
	public class TA_Facturation : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btCreation;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btImprimer;
		private System.Windows.Forms.DateTimePicker dtDebut;
        private System.Windows.Forms.Button btFermer;
        private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btcreationFactMan;
		private System.Windows.Forms.TextBox tbNAbonnement;
		private System.Windows.Forms.TextBox tbMontant;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.DateTimePicker dtDebutFac;
		private System.Windows.Forms.DateTimePicker dtFinFac;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button btimprFactMan;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.DateTimePicker dtpMoisRapport;
		private System.Windows.Forms.Button btImprFractEncaiss;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox tbNFacture;
		private System.Windows.Forms.Button btImprimeFac;
		private System.Windows.Forms.Button btEncFac;
		private System.Windows.Forms.Button btAnnuleFac;
		private System.Windows.Forms.ComboBox cbMoyenChoix;
        private System.Windows.Forms.Label label9;
        private TextBox textBoxMontant;
        private Label label11;
        private Button bImpFactureMat;
        private Label label12;
        private ComboBox cBoxTarif;
        private SplitContainer splitContainer1;
        private SplitContainer splitContainer3;
        private SplitContainer splitContainer2;
        private Label label13;
        private DateTimePicker dTPDateEncaissement;
        private TextBox tBoxNumFact1;
        private Label label14;
        private TextBox tBoxNumFact2;
        private Label label15;
        private Label label16;
        private PictureBox pictureBox1;
        private ImageList imageList1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private SplitContainer splitContainer4;
        private PictureBox pictureBox4;
        private Button btImpRappelsStopes;
        private Label label19;
        private DateTimePicker dTPrappelstopDeb;
        private DateTimePicker dTPrappelstopFin;
        private Label label17;
        private Label label18;
        private Label label20;
        private Label label21;
        private RadioButton rB2;
        private RadioButton rB1;
        private Label label10;
        private CheckBox cBoxActiveImpr;
        private Button bExit;
        private RadioButton rBMateriel;
        private RadioButton rBAbonnement;
        private SplitContainer splitContainer5;
        private Button bimpFactManMat;
        private Label label24;
        private Button bcreatFactMatMan;
        private PictureBox pictureBox5;
        private TextBox tAbonMatMan;
        private Label label23;
        private Label label22;
        private Button bRappels;
        private Label label25;
        private Button bImpFactCaution;
        private Label label26;
        private Button bCreerFactCaution;
        private PictureBox pictureBox6;
        private TextBox tBoxCaution;
        private Label label27;
        private Label label28;
        private IContainer components;

		public TA_Facturation()
		{			
			// Requis pour la prise en charge du Concepteur Windows Forms			
			InitializeComponent();                                  						
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TA_Facturation));
            this.btCreation = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btImprimer = new System.Windows.Forms.Button();
            this.dtDebut = new System.Windows.Forms.DateTimePicker();
            this.label12 = new System.Windows.Forms.Label();
            this.cBoxTarif = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtFinFac = new System.Windows.Forms.DateTimePicker();
            this.dtDebutFac = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.tbMontant = new System.Windows.Forms.TextBox();
            this.btcreationFactMan = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbNAbonnement = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btimprFactMan = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpMoisRapport = new System.Windows.Forms.DateTimePicker();
            this.btImprFractEncaiss = new System.Windows.Forms.Button();
            this.tbNFacture = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btImprimeFac = new System.Windows.Forms.Button();
            this.btEncFac = new System.Windows.Forms.Button();
            this.btAnnuleFac = new System.Windows.Forms.Button();
            this.cbMoyenChoix = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxMontant = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.bImpFactureMat = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.tBoxNumFact1 = new System.Windows.Forms.TextBox();
            this.rB2 = new System.Windows.Forms.RadioButton();
            this.rB1 = new System.Windows.Forms.RadioButton();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.tBoxNumFact2 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.cBoxActiveImpr = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label16 = new System.Windows.Forms.Label();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.label10 = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.bImpFactCaution = new System.Windows.Forms.Button();
            this.label26 = new System.Windows.Forms.Label();
            this.bCreerFactCaution = new System.Windows.Forms.Button();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.tBoxCaution = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.bimpFactManMat = new System.Windows.Forms.Button();
            this.label24 = new System.Windows.Forms.Label();
            this.bcreatFactMatMan = new System.Windows.Forms.Button();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.tAbonMatMan = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.label25 = new System.Windows.Forms.Label();
            this.bRappels = new System.Windows.Forms.Button();
            this.bExit = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.dTPrappelstopDeb = new System.Windows.Forms.DateTimePicker();
            this.dTPrappelstopFin = new System.Windows.Forms.DateTimePicker();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.btImpRappelsStopes = new System.Windows.Forms.Button();
            this.rBMateriel = new System.Windows.Forms.RadioButton();
            this.rBAbonnement = new System.Windows.Forms.RadioButton();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.dTPDateEncaissement = new System.Windows.Forms.DateTimePicker();
            this.label13 = new System.Windows.Forms.Label();
            this.btFermer = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).BeginInit();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // btCreation
            // 
            this.btCreation.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btCreation.Location = new System.Drawing.Point(209, 51);
            this.btCreation.Name = "btCreation";
            this.btCreation.Size = new System.Drawing.Size(136, 77);
            this.btCreation.TabIndex = 4;
            this.btCreation.Text = "Creation de toute les Factures";
            this.btCreation.UseVisualStyleBackColor = false;
            this.btCreation.Click += new System.EventHandler(this.btCreation_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(12, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "Date Debut de période:";
            // 
            // btImprimer
            // 
            this.btImprimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btImprimer.Location = new System.Drawing.Point(622, 43);
            this.btImprimer.Name = "btImprimer";
            this.btImprimer.Size = new System.Drawing.Size(143, 68);
            this.btImprimer.TabIndex = 10;
            this.btImprimer.Text = "Impr. toutes les factures d\'abonnement";
            this.btImprimer.UseVisualStyleBackColor = false;
            this.btImprimer.Click += new System.EventHandler(this.btImprimer_Click);
            // 
            // dtDebut
            // 
            this.dtDebut.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtDebut.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtDebut.Location = new System.Drawing.Point(26, 74);
            this.dtDebut.Name = "dtDebut";
            this.dtDebut.Size = new System.Drawing.Size(120, 26);
            this.dtDebut.TabIndex = 1;
            this.dtDebut.ValueChanged += new System.EventHandler(this.dtDebut_ValueChanged);
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(320, 46);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(101, 20);
            this.label12.TabIndex = 13;
            this.label12.Text = "Périodicité :";
            // 
            // cBoxTarif
            // 
            this.cBoxTarif.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cBoxTarif.FormattingEnabled = true;
            this.cBoxTarif.Items.AddRange(new object[] {
            "M",
            "T",
            "S",
            "A",
            "MeM",
            "MeT",
            "MeS",
            "MeA"});
            this.cBoxTarif.Location = new System.Drawing.Point(334, 69);
            this.cBoxTarif.Name = "cBoxTarif";
            this.cBoxTarif.Size = new System.Drawing.Size(73, 28);
            this.cBoxTarif.TabIndex = 12;
            this.cBoxTarif.Text = "T";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(639, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 20);
            this.label6.TabIndex = 7;
            this.label6.Text = "Date de Fin :";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(457, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(127, 20);
            this.label5.TabIndex = 5;
            this.label5.Text = "Date de Début :";
            // 
            // dtFinFac
            // 
            this.dtFinFac.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtFinFac.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFinFac.Location = new System.Drawing.Point(631, 71);
            this.dtFinFac.Name = "dtFinFac";
            this.dtFinFac.Size = new System.Drawing.Size(125, 26);
            this.dtFinFac.TabIndex = 8;
            // 
            // dtDebutFac
            // 
            this.dtDebutFac.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtDebutFac.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtDebutFac.Location = new System.Drawing.Point(452, 71);
            this.dtDebutFac.Name = "dtDebutFac";
            this.dtDebutFac.Size = new System.Drawing.Size(118, 26);
            this.dtDebutFac.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(191, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Montant :";
            // 
            // tbMontant
            // 
            this.tbMontant.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbMontant.Location = new System.Drawing.Point(175, 69);
            this.tbMontant.Name = "tbMontant";
            this.tbMontant.Size = new System.Drawing.Size(96, 26);
            this.tbMontant.TabIndex = 4;
            this.tbMontant.Text = "37.5";
            this.tbMontant.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbMontant.TextChanged += new System.EventHandler(this.tbMontant_TextChanged);
            // 
            // btcreationFactMan
            // 
            this.btcreationFactMan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btcreationFactMan.Location = new System.Drawing.Point(24, 113);
            this.btcreationFactMan.Name = "btcreationFactMan";
            this.btcreationFactMan.Size = new System.Drawing.Size(189, 36);
            this.btcreationFactMan.TabIndex = 9;
            this.btcreationFactMan.Text = "Créer la Facture";
            this.btcreationFactMan.Click += new System.EventHandler(this.btcreationFactMan_Click);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(416, 22);
            this.label3.TabIndex = 0;
            this.label3.Text = "Création d\'une facture abonnement manuellement";
            // 
            // tbNAbonnement
            // 
            this.tbNAbonnement.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbNAbonnement.Location = new System.Drawing.Point(14, 69);
            this.tbNAbonnement.Name = "tbNAbonnement";
            this.tbNAbonnement.Size = new System.Drawing.Size(99, 26);
            this.tbNAbonnement.TabIndex = 2;
            this.tbNAbonnement.Text = "-1";
            this.tbNAbonnement.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbNAbonnement.TextChanged += new System.EventHandler(this.tbNAbonnement_TextChanged);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "ID Abonnement :";
            // 
            // btimprFactMan
            // 
            this.btimprFactMan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btimprFactMan.Location = new System.Drawing.Point(24, 164);
            this.btimprFactMan.Name = "btimprFactMan";
            this.btimprFactMan.Size = new System.Drawing.Size(189, 36);
            this.btimprFactMan.TabIndex = 10;
            this.btimprFactMan.Text = "Imprimer la Facture";
            this.btimprFactMan.Click += new System.EventHandler(this.btimprFactMan_Click);
            // 
            // label7
            // 
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(16, 11);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(517, 24);
            this.label7.TabIndex = 5;
            this.label7.Text = "Impression de la liste des factures encaissées pour le mois de:";
            // 
            // dtpMoisRapport
            // 
            this.dtpMoisRapport.CustomFormat = "MMMM yyyy";
            this.dtpMoisRapport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpMoisRapport.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpMoisRapport.Location = new System.Drawing.Point(552, 9);
            this.dtpMoisRapport.Name = "dtpMoisRapport";
            this.dtpMoisRapport.Size = new System.Drawing.Size(168, 26);
            this.dtpMoisRapport.TabIndex = 6;
            this.dtpMoisRapport.Value = new System.DateTime(2018, 11, 15, 0, 0, 0, 0);
            // 
            // btImprFractEncaiss
            // 
            this.btImprFractEncaiss.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btImprFractEncaiss.Location = new System.Drawing.Point(278, 45);
            this.btImprFractEncaiss.Name = "btImprFractEncaiss";
            this.btImprFractEncaiss.Size = new System.Drawing.Size(192, 36);
            this.btImprFractEncaiss.TabIndex = 7;
            this.btImprFractEncaiss.Text = "Imprimer la liste";
            this.btImprFractEncaiss.UseVisualStyleBackColor = false;
            this.btImprFractEncaiss.Click += new System.EventHandler(this.button3_Click);
            // 
            // tbNFacture
            // 
            this.tbNFacture.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbNFacture.Location = new System.Drawing.Point(56, 44);
            this.tbNFacture.Name = "tbNFacture";
            this.tbNFacture.Size = new System.Drawing.Size(132, 26);
            this.tbNFacture.TabIndex = 2;
            this.tbNFacture.WordWrap = false;
            this.tbNFacture.Click += new System.EventHandler(this.tbNFacture_Click);
            this.tbNFacture.TextChanged += new System.EventHandler(this.tbNFacture_TextChanged);
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(14, 15);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(207, 26);
            this.label8.TabIndex = 0;
            this.label8.Text = "Encaisser la facture n° :";
            // 
            // btImprimeFac
            // 
            this.btImprimeFac.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btImprimeFac.Location = new System.Drawing.Point(453, 22);
            this.btImprimeFac.Name = "btImprimeFac";
            this.btImprimeFac.Size = new System.Drawing.Size(156, 36);
            this.btImprimeFac.TabIndex = 13;
            this.btImprimeFac.Text = "Imprimer la facture";
            this.btImprimeFac.Click += new System.EventHandler(this.btImprimeFac_Click);
            // 
            // btEncFac
            // 
            this.btEncFac.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btEncFac.Location = new System.Drawing.Point(26, 270);
            this.btEncFac.Name = "btEncFac";
            this.btEncFac.Size = new System.Drawing.Size(176, 41);
            this.btEncFac.TabIndex = 11;
            this.btEncFac.Text = "Encaisser la facture";
            this.btEncFac.Click += new System.EventHandler(this.btEncFac_Click);
            // 
            // btAnnuleFac
            // 
            this.btAnnuleFac.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btAnnuleFac.Location = new System.Drawing.Point(453, 74);
            this.btAnnuleFac.Name = "btAnnuleFac";
            this.btAnnuleFac.Size = new System.Drawing.Size(156, 36);
            this.btAnnuleFac.TabIndex = 14;
            this.btAnnuleFac.Text = "Annuler la facture";
            this.btAnnuleFac.Click += new System.EventHandler(this.btAnnuleFac_Click);
            // 
            // cbMoyenChoix
            // 
            this.cbMoyenChoix.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMoyenChoix.Items.AddRange(new object[] {
            "CCP SOS",
            "BVR",
            "SBVR",
            "CS",
            "BVR Med",
            "BCG",
            "Compensation",
            "Pertes et profits",
            "Caisse",
            "Opérations diverses",
            "BVR SOS"});
            this.cbMoyenChoix.Location = new System.Drawing.Point(56, 121);
            this.cbMoyenChoix.Name = "cbMoyenChoix";
            this.cbMoyenChoix.Size = new System.Drawing.Size(132, 28);
            this.cbMoyenChoix.TabIndex = 9;
            this.cbMoyenChoix.TextChanged += new System.EventHandler(this.cbMoyenChoix_TextChanged);
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(13, 96);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(241, 24);
            this.label9.TabIndex = 8;
            this.label9.Text = "avec pour moyen de paiement:";
            // 
            // textBoxMontant
            // 
            this.textBoxMontant.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxMontant.Location = new System.Drawing.Point(281, 124);
            this.textBoxMontant.Name = "textBoxMontant";
            this.textBoxMontant.Size = new System.Drawing.Size(133, 26);
            this.textBoxMontant.TabIndex = 19;
            this.textBoxMontant.WordWrap = false;
            this.textBoxMontant.TextChanged += new System.EventHandler(this.textBoxMontant_TextChanged);
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(270, 98);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(166, 19);
            this.label11.TabIndex = 20;
            this.label11.Text = "Montant à encaisser:";
            // 
            // bImpFactureMat
            // 
            this.bImpFactureMat.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bImpFactureMat.Location = new System.Drawing.Point(622, 126);
            this.bImpFactureMat.Name = "bImpFactureMat";
            this.bImpFactureMat.Size = new System.Drawing.Size(143, 64);
            this.bImpFactureMat.TabIndex = 21;
            this.bImpFactureMat.Text = "Impr. toutes les factures Matériel";
            this.bImpFactureMat.UseVisualStyleBackColor = false;
            this.bImpFactureMat.Click += new System.EventHandler(this.bImpFactureMat_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer3);
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer4);
            this.splitContainer1.Panel2.Controls.Add(this.btFermer);
            this.splitContainer1.Size = new System.Drawing.Size(1273, 980);
            this.splitContainer1.SplitterDistance = 791;
            this.splitContainer1.TabIndex = 22;
            // 
            // splitContainer3
            // 
            this.splitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 757);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.tBoxNumFact1);
            this.splitContainer3.Panel1.Controls.Add(this.rB2);
            this.splitContainer3.Panel1.Controls.Add(this.rB1);
            this.splitContainer3.Panel1.Controls.Add(this.pictureBox3);
            this.splitContainer3.Panel1.Controls.Add(this.btAnnuleFac);
            this.splitContainer3.Panel1.Controls.Add(this.tBoxNumFact2);
            this.splitContainer3.Panel1.Controls.Add(this.label15);
            this.splitContainer3.Panel1.Controls.Add(this.label14);
            this.splitContainer3.Panel1.Controls.Add(this.btImprimeFac);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.btImprFractEncaiss);
            this.splitContainer3.Panel2.Controls.Add(this.label7);
            this.splitContainer3.Panel2.Controls.Add(this.dtpMoisRapport);
            this.splitContainer3.Size = new System.Drawing.Size(791, 223);
            this.splitContainer3.SplitterDistance = 122;
            this.splitContainer3.TabIndex = 1;
            // 
            // tBoxNumFact1
            // 
            this.tBoxNumFact1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tBoxNumFact1.Location = new System.Drawing.Point(188, 10);
            this.tBoxNumFact1.Name = "tBoxNumFact1";
            this.tBoxNumFact1.Size = new System.Drawing.Size(120, 26);
            this.tBoxNumFact1.TabIndex = 4;
            this.tBoxNumFact1.TextChanged += new System.EventHandler(this.tBoxNumFact1_TextChanged);
            // 
            // rB2
            // 
            this.rB2.AutoSize = true;
            this.rB2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rB2.Location = new System.Drawing.Point(324, 66);
            this.rB2.Name = "rB2";
            this.rB2.Size = new System.Drawing.Size(100, 24);
            this.rB2.TabIndex = 27;
            this.rB2.TabStop = true;
            this.rB2.Text = "Mat./Caut.";
            this.rB2.UseVisualStyleBackColor = true;
            // 
            // rB1
            // 
            this.rB1.AutoSize = true;
            this.rB1.Checked = true;
            this.rB1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rB1.Location = new System.Drawing.Point(324, 37);
            this.rB1.Name = "rB1";
            this.rB1.Size = new System.Drawing.Size(119, 24);
            this.rB1.TabIndex = 26;
            this.rB1.TabStop = true;
            this.rB1.Text = "Abonnement";
            this.rB1.UseVisualStyleBackColor = true;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::ImportSosGeneve.Properties.Resources.smiley;
            this.pictureBox3.Location = new System.Drawing.Point(634, 19);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(135, 92);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 25;
            this.pictureBox3.TabStop = false;
            // 
            // tBoxNumFact2
            // 
            this.tBoxNumFact2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tBoxNumFact2.Location = new System.Drawing.Point(187, 73);
            this.tBoxNumFact2.Name = "tBoxNumFact2";
            this.tBoxNumFact2.Size = new System.Drawing.Size(120, 26);
            this.tBoxNumFact2.TabIndex = 6;
            this.tBoxNumFact2.TextChanged += new System.EventHandler(this.tBoxNumFact2_TextChanged);
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(3, 81);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(195, 26);
            this.label15.TabIndex = 5;
            this.label15.Text = "Annuler la facture n° :";
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(1, 13);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(194, 26);
            this.label14.TabIndex = 3;
            this.label14.Text = "Imprimer la facture n° :";
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.cBoxActiveImpr);
            this.splitContainer2.Panel1.Controls.Add(this.pictureBox1);
            this.splitContainer2.Panel1.Controls.Add(this.label16);
            this.splitContainer2.Panel1.Controls.Add(this.bImpFactureMat);
            this.splitContainer2.Panel1.Controls.Add(this.btCreation);
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            this.splitContainer2.Panel1.Controls.Add(this.btImprimer);
            this.splitContainer2.Panel1.Controls.Add(this.dtDebut);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer5);
            this.splitContainer2.Size = new System.Drawing.Size(791, 757);
            this.splitContainer2.SplitterDistance = 200;
            this.splitContainer2.TabIndex = 0;
            // 
            // cBoxActiveImpr
            // 
            this.cBoxActiveImpr.AutoSize = true;
            this.cBoxActiveImpr.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cBoxActiveImpr.Location = new System.Drawing.Point(209, 147);
            this.cBoxActiveImpr.Name = "cBoxActiveImpr";
            this.cBoxActiveImpr.Size = new System.Drawing.Size(196, 24);
            this.cBoxActiveImpr.TabIndex = 24;
            this.cBoxActiveImpr.Text = "Factures déjà générées";
            this.cBoxActiveImpr.UseVisualStyleBackColor = true;
            this.cBoxActiveImpr.CheckedChanged += new System.EventHandler(this.cBoxActiveImpr_CheckedChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ImportSosGeneve.Properties.Resources.smiley;
            this.pictureBox1.Location = new System.Drawing.Point(396, 51);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(134, 93);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 23;
            this.pictureBox1.TabStop = false;
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(8, 7);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(287, 26);
            this.label16.TabIndex = 22;
            this.label16.Text = "Génération de toutes les factures";
            // 
            // splitContainer5
            // 
            this.splitContainer5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer5.Location = new System.Drawing.Point(0, 0);
            this.splitContainer5.Name = "splitContainer5";
            this.splitContainer5.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.Controls.Add(this.label3);
            this.splitContainer5.Panel1.Controls.Add(this.btimprFactMan);
            this.splitContainer5.Panel1.Controls.Add(this.label10);
            this.splitContainer5.Panel1.Controls.Add(this.btcreationFactMan);
            this.splitContainer5.Panel1.Controls.Add(this.cBoxTarif);
            this.splitContainer5.Panel1.Controls.Add(this.pictureBox4);
            this.splitContainer5.Panel1.Controls.Add(this.label4);
            this.splitContainer5.Panel1.Controls.Add(this.tbMontant);
            this.splitContainer5.Panel1.Controls.Add(this.dtDebutFac);
            this.splitContainer5.Panel1.Controls.Add(this.tbNAbonnement);
            this.splitContainer5.Panel1.Controls.Add(this.label2);
            this.splitContainer5.Panel1.Controls.Add(this.dtFinFac);
            this.splitContainer5.Panel1.Controls.Add(this.label5);
            this.splitContainer5.Panel1.Controls.Add(this.label12);
            this.splitContainer5.Panel1.Controls.Add(this.label6);
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.Controls.Add(this.bImpFactCaution);
            this.splitContainer5.Panel2.Controls.Add(this.label26);
            this.splitContainer5.Panel2.Controls.Add(this.bCreerFactCaution);
            this.splitContainer5.Panel2.Controls.Add(this.pictureBox6);
            this.splitContainer5.Panel2.Controls.Add(this.tBoxCaution);
            this.splitContainer5.Panel2.Controls.Add(this.label27);
            this.splitContainer5.Panel2.Controls.Add(this.label28);
            this.splitContainer5.Panel2.Controls.Add(this.bimpFactManMat);
            this.splitContainer5.Panel2.Controls.Add(this.label24);
            this.splitContainer5.Panel2.Controls.Add(this.bcreatFactMatMan);
            this.splitContainer5.Panel2.Controls.Add(this.pictureBox5);
            this.splitContainer5.Panel2.Controls.Add(this.tAbonMatMan);
            this.splitContainer5.Panel2.Controls.Add(this.label23);
            this.splitContainer5.Panel2.Controls.Add(this.label22);
            this.splitContainer5.Size = new System.Drawing.Size(791, 553);
            this.splitContainer5.SplitterDistance = 213;
            this.splitContainer5.TabIndex = 28;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(424, 138);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(185, 56);
            this.label10.TabIndex = 27;
            this.label10.Text = "Et le n° de facture est  :";
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::ImportSosGeneve.Properties.Resources.smiley;
            this.pictureBox4.Location = new System.Drawing.Point(287, 122);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(118, 78);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 26;
            this.pictureBox4.TabStop = false;
            // 
            // bImpFactCaution
            // 
            this.bImpFactCaution.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bImpFactCaution.Location = new System.Drawing.Point(172, 256);
            this.bImpFactCaution.Name = "bImpFactCaution";
            this.bImpFactCaution.Size = new System.Drawing.Size(189, 36);
            this.bImpFactCaution.TabIndex = 36;
            this.bImpFactCaution.Text = "Imprimer Fact. caution";
            this.bImpFactCaution.Click += new System.EventHandler(this.bImpFactCaution_Click);
            // 
            // label26
            // 
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(572, 219);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(185, 56);
            this.label26.TabIndex = 38;
            this.label26.Text = "Et le n° de facture est  :";
            // 
            // bCreerFactCaution
            // 
            this.bCreerFactCaution.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bCreerFactCaution.Location = new System.Drawing.Point(172, 200);
            this.bCreerFactCaution.Name = "bCreerFactCaution";
            this.bCreerFactCaution.Size = new System.Drawing.Size(189, 36);
            this.bCreerFactCaution.TabIndex = 35;
            this.bCreerFactCaution.Text = "Créer Fact. caution";
            this.bCreerFactCaution.Click += new System.EventHandler(this.bCreerFactCaution_Click);
            // 
            // pictureBox6
            // 
            this.pictureBox6.Image = global::ImportSosGeneve.Properties.Resources.smiley;
            this.pictureBox6.Location = new System.Drawing.Point(427, 205);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(118, 78);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox6.TabIndex = 37;
            this.pictureBox6.TabStop = false;
            // 
            // tBoxCaution
            // 
            this.tBoxCaution.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tBoxCaution.Location = new System.Drawing.Point(18, 208);
            this.tBoxCaution.Name = "tBoxCaution";
            this.tBoxCaution.Size = new System.Drawing.Size(99, 26);
            this.tBoxCaution.TabIndex = 34;
            this.tBoxCaution.Text = "-1";
            this.tBoxCaution.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tBoxCaution.Leave += new System.EventHandler(this.tBoxCaution_Leave);
            // 
            // label27
            // 
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(5, 184);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(135, 20);
            this.label27.TabIndex = 33;
            this.label27.Text = "ID Abonnement :";
            // 
            // label28
            // 
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(2, 159);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(382, 22);
            this.label28.TabIndex = 32;
            this.label28.Text = "Création d\'une facture de CAUTION matériel";
            // 
            // bimpFactManMat
            // 
            this.bimpFactManMat.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bimpFactManMat.Location = new System.Drawing.Point(175, 102);
            this.bimpFactManMat.Name = "bimpFactManMat";
            this.bimpFactManMat.Size = new System.Drawing.Size(189, 36);
            this.bimpFactManMat.TabIndex = 29;
            this.bimpFactManMat.Text = "Imprimer la Facture";
            this.bimpFactManMat.Click += new System.EventHandler(this.bimpFactManMat_Click);
            // 
            // label24
            // 
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(575, 65);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(185, 56);
            this.label24.TabIndex = 31;
            this.label24.Text = "Et le n° de facture est  :";
            // 
            // bcreatFactMatMan
            // 
            this.bcreatFactMatMan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bcreatFactMatMan.Location = new System.Drawing.Point(175, 46);
            this.bcreatFactMatMan.Name = "bcreatFactMatMan";
            this.bcreatFactMatMan.Size = new System.Drawing.Size(189, 36);
            this.bcreatFactMatMan.TabIndex = 28;
            this.bcreatFactMatMan.Text = "Créer la Facture";
            this.bcreatFactMatMan.Click += new System.EventHandler(this.bcreatFactMatMan_Click);
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = global::ImportSosGeneve.Properties.Resources.smiley;
            this.pictureBox5.Location = new System.Drawing.Point(430, 51);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(118, 78);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox5.TabIndex = 30;
            this.pictureBox5.TabStop = false;
            // 
            // tAbonMatMan
            // 
            this.tAbonMatMan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tAbonMatMan.Location = new System.Drawing.Point(21, 54);
            this.tAbonMatMan.Name = "tAbonMatMan";
            this.tAbonMatMan.Size = new System.Drawing.Size(99, 26);
            this.tAbonMatMan.TabIndex = 4;
            this.tAbonMatMan.Text = "-1";
            this.tAbonMatMan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tAbonMatMan.Leave += new System.EventHandler(this.tAbonMatMan_Leave);
            // 
            // label23
            // 
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(8, 30);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(135, 20);
            this.label23.TabIndex = 3;
            this.label23.Text = "ID Abonnement :";
            // 
            // label22
            // 
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(5, 5);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(382, 22);
            this.label22.TabIndex = 1;
            this.label22.Text = "Création d\'une facture matériel manuellement";
            // 
            // splitContainer4
            // 
            this.splitContainer4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.label25);
            this.splitContainer4.Panel1.Controls.Add(this.bRappels);
            this.splitContainer4.Panel1.Controls.Add(this.bExit);
            this.splitContainer4.Panel1.Controls.Add(this.label21);
            this.splitContainer4.Panel1.Controls.Add(this.label20);
            this.splitContainer4.Panel1.Controls.Add(this.label19);
            this.splitContainer4.Panel1.Controls.Add(this.dTPrappelstopDeb);
            this.splitContainer4.Panel1.Controls.Add(this.dTPrappelstopFin);
            this.splitContainer4.Panel1.Controls.Add(this.label17);
            this.splitContainer4.Panel1.Controls.Add(this.label18);
            this.splitContainer4.Panel1.Controls.Add(this.btImpRappelsStopes);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.btEncFac);
            this.splitContainer4.Panel2.Controls.Add(this.label8);
            this.splitContainer4.Panel2.Controls.Add(this.rBMateriel);
            this.splitContainer4.Panel2.Controls.Add(this.rBAbonnement);
            this.splitContainer4.Panel2.Controls.Add(this.tbNFacture);
            this.splitContainer4.Panel2.Controls.Add(this.pictureBox2);
            this.splitContainer4.Panel2.Controls.Add(this.dTPDateEncaissement);
            this.splitContainer4.Panel2.Controls.Add(this.label13);
            this.splitContainer4.Panel2.Controls.Add(this.textBoxMontant);
            this.splitContainer4.Panel2.Controls.Add(this.label9);
            this.splitContainer4.Panel2.Controls.Add(this.cbMoyenChoix);
            this.splitContainer4.Panel2.Controls.Add(this.label11);
            this.splitContainer4.Size = new System.Drawing.Size(478, 980);
            this.splitContainer4.SplitterDistance = 452;
            this.splitContainer4.TabIndex = 18;
            // 
            // label25
            // 
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(5, 156);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(197, 47);
            this.label25.TabIndex = 57;
            this.label25.Text = "Imprimer la liste des rappels  TA stopés:";
            // 
            // bRappels
            // 
            this.bRappels.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bRappels.Location = new System.Drawing.Point(262, 271);
            this.bRappels.Name = "bRappels";
            this.bRappels.Size = new System.Drawing.Size(189, 36);
            this.bRappels.TabIndex = 54;
            this.bRappels.Text = "Imprimer les rappels TA";
            this.bRappels.UseVisualStyleBackColor = false;
            this.bRappels.Click += new System.EventHandler(this.bRappels_Click);
            // 
            // bExit
            // 
            this.bExit.BackColor = System.Drawing.Color.Transparent;
            this.bExit.BackgroundImage = global::ImportSosGeneve.Properties.Resources.exit;
            this.bExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bExit.FlatAppearance.BorderSize = 0;
            this.bExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bExit.ImageIndex = 0;
            this.bExit.Location = new System.Drawing.Point(411, 1);
            this.bExit.Name = "bExit";
            this.bExit.Size = new System.Drawing.Size(60, 60);
            this.bExit.TabIndex = 53;
            this.bExit.UseVisualStyleBackColor = false;
            this.bExit.Click += new System.EventHandler(this.bExit_Click);
            // 
            // label21
            // 
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label21.Location = new System.Drawing.Point(5, 289);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(249, 42);
            this.label21.TabIndex = 25;
            this.label21.Text = "Entre: aujourd\'hui - 2ans et 3 mois et jusqu\'à - 4 mois";
            // 
            // label20
            // 
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(5, 263);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(218, 26);
            this.label20.TabIndex = 24;
            this.label20.Text = "Imprimer les rappels TA:";
            // 
            // label19
            // 
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(7, 18);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(258, 26);
            this.label19.TabIndex = 23;
            this.label19.Text = "Imprimer les rappels stopés :";
            // 
            // dTPrappelstopDeb
            // 
            this.dTPrappelstopDeb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dTPrappelstopDeb.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dTPrappelstopDeb.Location = new System.Drawing.Point(18, 74);
            this.dTPrappelstopDeb.Name = "dTPrappelstopDeb";
            this.dTPrappelstopDeb.Size = new System.Drawing.Size(118, 26);
            this.dTPrappelstopDeb.TabIndex = 20;
            // 
            // dTPrappelstopFin
            // 
            this.dTPrappelstopFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dTPrappelstopFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dTPrappelstopFin.Location = new System.Drawing.Point(192, 74);
            this.dTPrappelstopFin.Name = "dTPrappelstopFin";
            this.dTPrappelstopFin.Size = new System.Drawing.Size(125, 26);
            this.dTPrappelstopFin.TabIndex = 22;
            // 
            // label17
            // 
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(22, 51);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(127, 20);
            this.label17.TabIndex = 19;
            this.label17.Text = "Depuis le :";
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(204, 51);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(102, 20);
            this.label18.TabIndex = 21;
            this.label18.Text = "jusqu\'au :";
            // 
            // btImpRappelsStopes
            // 
            this.btImpRappelsStopes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btImpRappelsStopes.Location = new System.Drawing.Point(262, 156);
            this.btImpRappelsStopes.Name = "btImpRappelsStopes";
            this.btImpRappelsStopes.Size = new System.Drawing.Size(189, 36);
            this.btImpRappelsStopes.TabIndex = 18;
            this.btImpRappelsStopes.Text = "Imprimer la liste";
            this.btImpRappelsStopes.UseVisualStyleBackColor = false;
            this.btImpRappelsStopes.Click += new System.EventHandler(this.btImpRappelsStopes_Click);
            // 
            // rBMateriel
            // 
            this.rBMateriel.AutoSize = true;
            this.rBMateriel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rBMateriel.Location = new System.Drawing.Point(63, 196);
            this.rBMateriel.Name = "rBMateriel";
            this.rBMateriel.Size = new System.Drawing.Size(142, 24);
            this.rBMateriel.TabIndex = 26;
            this.rBMateriel.Text = "Facture Matériel";
            this.rBMateriel.UseVisualStyleBackColor = true;
            // 
            // rBAbonnement
            // 
            this.rBAbonnement.AutoSize = true;
            this.rBAbonnement.Checked = true;
            this.rBAbonnement.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rBAbonnement.Location = new System.Drawing.Point(63, 170);
            this.rBAbonnement.Name = "rBAbonnement";
            this.rBAbonnement.Size = new System.Drawing.Size(176, 24);
            this.rBAbonnement.TabIndex = 25;
            this.rBAbonnement.TabStop = true;
            this.rBAbonnement.Text = "Facture abonnement";
            this.rBAbonnement.UseVisualStyleBackColor = true;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::ImportSosGeneve.Properties.Resources.smiley;
            this.pictureBox2.Location = new System.Drawing.Point(281, 218);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(134, 93);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 24;
            this.pictureBox2.TabStop = false;
            // 
            // dTPDateEncaissement
            // 
            this.dTPDateEncaissement.CustomFormat = "dd.MM.yyyy";
            this.dTPDateEncaissement.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dTPDateEncaissement.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dTPDateEncaissement.Location = new System.Drawing.Point(281, 47);
            this.dTPDateEncaissement.Name = "dTPDateEncaissement";
            this.dTPDateEncaissement.Size = new System.Drawing.Size(133, 26);
            this.dTPDateEncaissement.TabIndex = 21;
            this.dTPDateEncaissement.Value = new System.DateTime(2018, 11, 15, 0, 0, 0, 0);
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(292, 23);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(107, 19);
            this.label13.TabIndex = 22;
            this.label13.Text = "à la Date du:";
            // 
            // btFermer
            // 
            this.btFermer.BackgroundImage = global::ImportSosGeneve.Properties.Resources.Fermer;
            this.btFermer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btFermer.Location = new System.Drawing.Point(364, 596);
            this.btFermer.Name = "btFermer";
            this.btFermer.Size = new System.Drawing.Size(72, 36);
            this.btFermer.TabIndex = 15;
            this.btFermer.Click += new System.EventHandler(this.btFermer_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "smiley 1ok.png");
            this.imageList1.Images.SetKeyName(1, "Smiley_2ok.png");
            this.imageList1.Images.SetKeyName(2, "Smiley_Embete1.png");
            this.imageList1.Images.SetKeyName(3, "Smiley_pasOk.png");
            this.imageList1.Images.SetKeyName(4, "Smiley_reflechi1.png");
            // 
            // TA_Facturation
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.ClientSize = new System.Drawing.Size(1273, 980);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "TA_Facturation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Facturation TA";
            this.Load += new System.EventHandler(this.TA_Facturation_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel1.PerformLayout();
            this.splitContainer5.Panel2.ResumeLayout(false);
            this.splitContainer5.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).EndInit();
            this.splitContainer5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		private void btFermer_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
    


//#################### CREATION FACTURE ################## A mettre dans une transaction ####################################
		private void btCreation_Click(object sender, System.EventArgs e)		
        {
            string RetourAb = CreationFactureAbonnement();
            string RetourMat = CreationFactureMateriel();

            if (RetourAb == "OK" && RetourMat == "OK")
            {
                btImprimer.Enabled = true;
                bImpFactureMat.Enabled = true;
                pictureBox1.Visible = true;
                pictureBox1.Image = imageList1.Images[1];
            }
            else
            {
                btImprimer.Enabled = false;
                bImpFactureMat.Enabled = false;
                pictureBox1.Visible = true;
                pictureBox1.Image = imageList1.Images[3];
            }
           
		}

        private string CreationFactureAbonnement()
        {
            //Creation des factures TA et Médicalerte
            Cursor.Current = Cursors.WaitCursor;
         
            // Uses the default calendar of the InvariantCulture.
            Calendar myCal = CultureInfo.InvariantCulture.Calendar;
            //var declaration
            DateTime DtDebut = DateTime.Parse(dtDebut.Text.ToString());
            DateTime DtFin = DateTime.Parse(dtDebut.Text.ToString());
            DateTime DtFinFac;
            DataSet dsAbo = null;
            DataSet dsFac = null;
            int id;
            int N_Ta = 0;
            int NCle = 0;

            //Selection de tout les abonnements actifs
            string sql = "SELECT IdAbonnement, DateDebutFacturation , Periodicite, Archive, Ordre, N_TA ";
            sql += " FROM ta_abonnement ";
            sql += " WHERE DateDebutFacturation < '" + OutilsExt.OutilsSql.DateFormatMySql(DtDebut) + "' ";
            sql += " AND Archive=0 AND (Periodicite = 'S' OR Periodicite = 'T' OR Periodicite = 'A' OR Periodicite = 'M' ";
            sql += " OR Periodicite = 'MeM' OR Periodicite = 'MeT' OR Periodicite = 'MeS' OR Periodicite = 'MeA' )";
            sql += " AND ActiverFacture = 1";
            sql += " ORDER BY IdAbonnement";

            dsAbo = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet(sql);

            //Pour tout les abonnements retournés, on regarde la datefin de la dernière facture et on en créer une si besoin.
            try
            {
                for (int i = 0; i < dsAbo.Tables[0].Rows.Count; i++)
                {
                    //On renseigne la facture avec les infos de la personne
                    id = int.Parse(dsAbo.Tables[0].Rows[i]["IdAbonnement"].ToString());

                    if (int.Parse(dsAbo.Tables[0].Rows[i]["N_TA"].ToString()) > 0)
                        N_Ta = int.Parse(dsAbo.Tables[0].Rows[i]["N_TA"].ToString());

                    string sqlFac = "SELECT tab.IdAbonnement, taf.NCles, taf.Fin_période";
                    sqlFac += " FROM ta_abonnement tab, ta_factures taf";
                    sqlFac += " WHERE tab.IdAbonnement = taf.Idabonnement";
                    sqlFac += " AND (((tab.IdAbonnement)='" + id.ToString() + "'))";
                    sqlFac += " ORDER BY taf.Fin_période DESC";

                    dsFac = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet(sqlFac);

                    if (dsFac.Tables[0].Rows.Count > 0)
                    {
                        //verify Fin_periode in last bill, if need to create new bill 
                        DtFinFac = DateTime.Parse(dsFac.Tables[0].Rows[0]["Fin_période"].ToString());
                    }
                    else
                        DtFinFac = DateTime.Now;

                    //find the N_Cle 
                    string[][] cle = OutilsExt.OutilsSql.ExecuteCommandeAvecTabString("SELECT NumeroCle from ta_abonnementcle WHERE IdAbonnement ='" + id.ToString() + "'");

                    if (cle != null && cle.Length != 0 && cle[0][0] != "")
                    {
                        NCle = int.Parse(cle[0][0]);
                    }

                    //On regarde s'il y a une facture pour patient, si oui, en faut il une nouvelle?
                    if ((dsFac.Tables[0].Rows.Count > 0 && DtFinFac < DtDebut) || dsFac.Tables[0].Rows.Count == 0)
                    {
                        //Creation d'une nvlle facture
                        //on met la date de fin en Fct de la périodicité
                        switch (dsAbo.Tables[0].Rows[i]["Periodicite"].ToString())
                        {
                            case "M":
                            case "MeM": DtFin = myCal.AddMonths(DtDebut, 1); DtFin = myCal.AddDays(DtFin, -1); break;

                            case "T":
                            case "MeT": DtFin = myCal.AddMonths(DtDebut, 3); DtFin = myCal.AddDays(DtFin, -1); break;

                            case "S":
                            case "MeS": DtFin = myCal.AddMonths(DtDebut, 6); DtFin = myCal.AddDays(DtFin, -1); break;

                            case "A":
                            case "MeA": DtFin = myCal.AddMonths(DtDebut, 12); DtFin = myCal.AddDays(DtFin, -1); break;
                        }


                        //Récup du dernier n° de facture
                        int max = -1;

                        string[][] retour = OutilsExt.OutilsSql.ExecuteCommandeAvecTabString("SELECT max(NFacture) from ta_factures");

                        if (retour != null && retour.Length != 0 && retour[0][0] != "")
                        {
                            max = int.Parse(retour[0][0]) + 1;
                        }
                        else
                        {
                            max = 1;
                        }

                        //Montant à payer
                        decimal MontantMensuel = 0;
                        decimal Total = 0;

                        string tarif = dsAbo.Tables[0].Rows[i]["Periodicite"].ToString();

                        //On prépare la requette
                        string ReqSql = " SELECT Tarif, Total = CASE ";
                        ReqSql += "                                WHEN '" + tarif + "' IN ('MeM','M') THEN Tarif ";
                        ReqSql += "                                WHEN '" + tarif + "' in ('MeT','T') THEN Tarif *3";
                        ReqSql += "                                WHEN '" + tarif + "' in ('MeS','S') THEN Tarif *6";
                        ReqSql += "                                WHEN '" + tarif + "' in ('MeA','A') THEN Tarif *12";
                        ReqSql += "                             END ";
                        ReqSql += " FROM ta_tarif";
                        ReqSql += " WHERE id = case ";
                        ReqSql += "               WHEN '" + tarif + "' in ('MeM','MeT','MeS', 'MeA') THEN 'MeM'";
                        ReqSql += "               WHEN '" + tarif + "' in ('M','T','S','A') THEN 'M'";
                        ReqSql += "            END";

                        retour = OutilsExt.OutilsSql.ExecuteCommandeAvecTabString(ReqSql);


                        if (retour != null && retour.Length != 0 && retour[0][0] != "")
                        {
                            MontantMensuel = decimal.Parse(retour[0][0]);
                            Total = decimal.Parse(retour[0][1]);
                        }

                        try
                        {
                            string ReqSql2 = "INSERT INTO ta_factures ";
                            ReqSql2 += " (NFacture,IdAbonnement, NTA, NCles, Date_facture, Montant, Début_période, Fin_période,";
                            ReqSql2 += " [1_er_rappel], [2_nd_rappel], Payé, Moyen, Acquité, Tarif_mensuel, Imprimé, SBVR, Remarque, Solde, Id_tarif)";
                            ReqSql2 += " VALUES ('" + max + "', '" + id.ToString() + "','" + N_Ta.ToString() + "', '" + NCle.ToString() + "', getDate(),";
                            ReqSql2 += "'" + Total.ToString() + "', '" + OutilsExt.OutilsSql.DateFormatMySql(DtDebut) + "', '";
                            ReqSql2 += OutilsExt.OutilsSql.DateFormatMySql(DtFin) + "', NULL, NULL, NULL, NULL, 0, ";
                            ReqSql2 += MontantMensuel.ToString() + ", 0, 0, NULL," + Total.ToString() + ",'" + dsAbo.Tables[0].Rows[i]["Periodicite"].ToString() + "')";

                            OutilsExt.OutilsSql.ExecuteCommandeSansRetour(ReqSql2);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Erreur lors de la création de la facture " + max.ToString() + " Pour l'abonnement " + id.ToString() + " \r\nLe message est :" + ex.Message);
                            return ("KO");
                        }
                        

                        //On met a jour la table pour l'envoi des documents
                        JointDocFacture(id);
                    }
                }       //Fin boucle for

                Cursor.Current = Cursors.Default;
                return("OK");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Validation: {0}", ex.GetType());
                Console.WriteLine("  Message: {0}", ex.Message);
                MessageBox.Show("Erreur lors de la récupération des infos pour les factures \r\nLe message est :" + ex.Message);
                
                Cursor.Current = Cursors.Default;
                return ("KO");
            }                        
        }


        //On créé les factures matériels
        private string CreationFactureMateriel()
        {
            Cursor.Current = Cursors.WaitCursor;

            string retour = "KO";
            string retourCaution = "KO";

            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dbConnection;

                //On recherche tous les nvx matériels affectés sauf ceux dont le prix d'achat est = 0 (matos inclus) et type tarif = R (remplacement)             
              /*  string sqlstr0 = "SELECT tm.IdAbonnement";
                sqlstr0 += " FROM ta_materiel tm, ta_tarif t, ta_abonnement ta ";
                sqlstr0 += " WHERE tm.Type_tarif = t.id ";
                sqlstr0 += " AND tm.idAbonnement is not null ";
                sqlstr0 += " AND tm.idAbonnement = ta.IdAbonnement ";
                sqlstr0 += " AND ta.ActiverFacture = 1";
                sqlstr0 += " AND tm.Prix_Achat <> 0 ";
                sqlstr0 += " AND tm.Type_tarif <> 'R' ";
                sqlstr0 += " AND tm.VID not in (SELECT RefProduit FROM TA_FactMat_Detail) ";
                sqlstr0 += " GROUP BY tm.IdAbonnement";*/

                //On exclu certains abonnements (les 1er et test car il y a eu des ratés à la mise en place)
                string sqlstr0 = " SELECT DISTINCT(IdAbonnement)";
                sqlstr0 += " FROM (";
                sqlstr0 += " SELECT m.IdAbonnement, m.VID";
                sqlstr0 += " FROM TA_Materiel m INNER JOIN ta_abonnement a ON a.IdAbonnement = m.IdAbonnement";
                sqlstr0 += " WHERE (m.idAbonnement IS NOT NULL AND m.idAbonnement <> '')";
                sqlstr0 += " AND m.Prix_Achat <> 0";
                sqlstr0 += " AND m.Type_tarif <> 'R'"; 
                sqlstr0 += " AND m.IdAbonnement <> 0";
                sqlstr0 += " AND a.ActiverFacture = 1";
                sqlstr0 += " EXCEPT";
                sqlstr0 += " SELECT fm.IdAbonnement, d.RefProduit as VID";
                sqlstr0 += " FROM TA_FactMat fm INNER JOIN TA_FactMat_Detail d ON fm.NumFacture = d.NumFacture";
                sqlstr0 += " ) AS nvx";
                                
                cmd.CommandText = sqlstr0;

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable NvxMatos = new DataTable();

                da.Fill(NvxMatos);

                //Pour chaque nouvel abonnement, on génère la facture matériel Et la facture de Caution 
                if (NvxMatos.Rows.Count > 0)
                {
                    for (int i = 0; i < NvxMatos.Rows.Count; i++)
                    {
                        if (GenereFactureMat(NvxMatos.Rows[i]["IdAbonnement"].ToString()) != "-1")
                            retour = "OK";
                        else retour = "KO";

                        if (GenereFactureCaution(NvxMatos.Rows[i]["IdAbonnement"].ToString()) != "-1")
                            retourCaution = "OK";
                        else retourCaution = "KO";   //Pour debug
                    }
                }
                else
                    retour = "OK";

                Cursor.Current = Cursors.Default;                
            }
            catch (Exception ex)
            {
                Console.WriteLine(" Erreur : " + ex.Message);                              
                Cursor.Current = Cursors.Default;
                retour = "KO";
            }

            return (retour);      
        }

               
        //pour Genération de la facture Matériel
        private string GenereFactureMat(string IdAbon)
        {
            //On recherche tout les matériels alloués pour cet abonnement
          //  Cursor.Current = Cursors.WaitCursor;
            string Retour = "-1";

            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);

            //On ouvre la connexion
            dbConnection.Open();

            SqlCommand cmd = dbConnection.CreateCommand();

            try
            {               
                cmd.Connection = dbConnection;

                //On recherche tous les nvx abonnements Médicalerte et ou Matériels                                              
                /*string sqlstr0 = "SELECT tm.IdAbonnement, tm.VID, t.LibelleTarif, t.Tarif ";
                sqlstr0 += " FROM ta_materiel tm, ta_tarif t";
                sqlstr0 += " WHERE tm.Type_tarif = t.id ";
                sqlstr0 += " AND tm.idAbonnement is not null ";                               
                sqlstr0 += " AND tm.Prix_Achat <> 0 ";
                sqlstr0 += " AND tm.Type_tarif <> 'R' ";
                sqlstr0 += " AND tm.IdAbonnement = @IdAbonnement ";
                sqlstr0 += " AND tm.VID not in (SELECT RefProduit FROM TA_FactMat_Detail) ";*/
               
                string sqlstr0 = " SELECT nvx.IdAbonnement, nvx.VID, t.Tarif, t.LibelleTarif";
                sqlstr0 += " FROM (";
                sqlstr0 += " SELECT m.IdAbonnement, m.VID";
                sqlstr0 += " FROM TA_Materiel m INNER JOIN ta_abonnement a ON a.IdAbonnement = m.IdAbonnement";
                sqlstr0 += " WHERE (m.idAbonnement IS NOT NULL AND m.idAbonnement <> '')";
                sqlstr0 += " AND m.Prix_Achat <> 0";
                sqlstr0 += " AND m.Type_tarif <> 'R'"; 
                sqlstr0 += " AND m.IdAbonnement <> 0";
                sqlstr0 += " AND m.IdAbonnement =  @IdAbonnement";
                sqlstr0 += " AND a.ActiverFacture = 1";
                sqlstr0 += " EXCEPT";
                sqlstr0 += " SELECT fm.IdAbonnement, d.RefProduit as VID";
                sqlstr0 += " FROM TA_FactMat fm INNER JOIN TA_FactMat_Detail d ON fm.NumFacture = d.NumFacture";
                sqlstr0 += " WHERE fm.IdAbonnement =  @IdAbonnement";
                sqlstr0 += " ) AS nvx INNER JOIN TA_Materiel mat ON nvx.VID = mat.VID INNER JOIN ta_tarif t ON mat.Type_tarif = t.id";

                cmd.CommandText = sqlstr0;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("IdAbonnement", IdAbon);
                               
                DataTable dtMateriel = new DataTable();

                dtMateriel.Load(cmd.ExecuteReader());    //On execute
              
                //On doit au moins avoir 1 matos
                if (dtMateriel.Rows.Count > 0)
                {
                    //On fait la somme du matériel facturable
                    decimal TotalFacture = 0;

                    for (int i = 0; i < dtMateriel.Rows.Count; i++ )
                    {
                        TotalFacture += decimal.Parse(dtMateriel.Rows[i]["Tarif"].ToString());
                    }

                    //Puis on recherche le + grand n° de facture
                    sqlstr0 = "SELECT MAX(NumFacture) FROM TA_FactMat";
                    cmd.CommandText = sqlstr0;

                    DataTable dtMaxFacture = new DataTable();

                    dtMaxFacture.Load(cmd.ExecuteReader());    //On execute;

                    int NumFacture = 0;

                    if (dtMaxFacture.Rows.Count > 0 && dtMaxFacture.Rows[0][0] != DBNull.Value)
                    {
                        NumFacture = Int32.Parse(dtMaxFacture.Rows[0][0].ToString()) + 1;
                    }
                    else
                        NumFacture = 1;
                                                                                                   
                    //Dans une transaction, on génère la facture, ainsi que le détail de la facture
                    //On ouvre la connexion
                    //dbConnection.Open();                   

                    //On démarre une transaction locale
                    SqlTransaction transaction;
                    transaction = dbConnection.BeginTransaction();

                    try
                    {
                        cmd.Connection = dbConnection;
                        cmd.Transaction = transaction;

                        //On defini la requette
                        string sqlstr1 = "INSERT INTO TA_FactMat (NumFacture, IdAbonnement, DateFacture, TotalFacture, Solde)";
                        sqlstr1 += " VALUES (@NumFacture, @IdAbonnement, getdate(), @TotalFacture, @Solde)";

                        //Ajout des parametres
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("NumFacture", NumFacture);
                        cmd.Parameters.AddWithValue("IdAbonnement", IdAbon);
                        cmd.Parameters.AddWithValue("TotalFacture", TotalFacture);
                        cmd.Parameters.AddWithValue("Solde", TotalFacture);
                        cmd.CommandText = sqlstr1;

                        //Execution de la requette
                        cmd.ExecuteNonQuery();

                        //Puis pour chaque matériel, ajout une ligne dans le détail de la facture
                        for (int i = 0; i < dtMateriel.Rows.Count; i++)
                        {
                            //On defini la requette... pour l'instant on ne gère pas la réduction et la qté (par defaut 0 et 1)
                            string sqlstr2 = "INSERT INTO TA_FactMat_Detail (NumFacture, RefProduit, Libelle, PrixUnitaire, Reduction, Qte, PrixTotal)";
                            sqlstr2 += " VALUES (@NumFacture, @RefProduit, @Libelle, @PU, 0, 1, @PrixTotal)";

                            //Ajout des parametres
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("NumFacture", NumFacture);
                            cmd.Parameters.AddWithValue("RefProduit", dtMateriel.Rows[i]["VID"].ToString());
                            cmd.Parameters.AddWithValue("Libelle", dtMateriel.Rows[i]["LibelleTarif"].ToString());
                            cmd.Parameters.AddWithValue("PU", dtMateriel.Rows[i]["Tarif"].ToString());
                            cmd.Parameters.AddWithValue("PrixTotal", dtMateriel.Rows[i]["Tarif"].ToString());
                            cmd.CommandText = sqlstr2;

                            //Execution de la requette
                            cmd.ExecuteNonQuery();
                        }
                                                
                        //On valide la transaction
                        transaction.Commit();

                        Retour = NumFacture.ToString();

                        mouchard.evenement("Création d'une facture Matériel " + NumFacture, VariablesApplicatives.Utilisateurs.NomUtilisateur.ToString());  //log            
                    }
                    catch (Exception a)
                    {
                        Console.WriteLine("Erreur : " + a.Message);
                        MessageBox.Show("Erreur lors de la création de la facture n°" + NumFacture + " ....l'erreur est : " + a.Message);
                        transaction.Rollback();
                        Retour = "-1";
                    }
                    finally
                    {
                        //fermeture de la connexion
                        if (dbConnection.State == ConnectionState.Open)
                            dbConnection.Close();                       
                    }
                }              
            }
            catch (Exception ex)
            {
                Console.WriteLine(" Erreur : " + ex.Message);
                Retour = "-1";
            }
            finally
            {
                //fermeture de la connexion
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();                
            }

            return (Retour);
        }


        //pour Genération de la facture de caution du matériel
        private string GenereFactureCaution(string IdAbon)
        {
            //On recherche tout les matériels alloués pour cet abonnement
            //  Cursor.Current = Cursors.WaitCursor;
            string Retour = "-1";

            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);

            //On ouvre la connexion
            dbConnection.Open();

            SqlCommand cmd = dbConnection.CreateCommand();

            try
            {
                cmd.Connection = dbConnection;
                       
                //**** Selectionner les nouveaux abonnements qui n'ont pas de caution ****/
                //On récupère la caution 
                string sqlstr0 = " SELECT nvx.IdAbonnement, nvx.VID, t.Tarif, mat.Type_Tarif, t.LibelleTarif";
                sqlstr0 += " FROM (";
                sqlstr0 += "        SELECT m.IdAbonnement, m.VID";
                sqlstr0 += "        FROM TA_Materiel m INNER JOIN ta_abonnement a ON a.IdAbonnement = m.IdAbonnement";
                sqlstr0 += "        WHERE (m.idAbonnement IS NOT NULL AND m.idAbonnement <> '')";
                sqlstr0 += "        AND m.Prix_Achat <> 0";
                //sqlstr0 += "        AND m.Type_tarif <> 'R'";
                sqlstr0 += "        AND m.Type_tarif not in ('R', 'M4', 'LprR', 'DCVIB', 'TiR')";
                sqlstr0 += "        AND m.IdAbonnement <> 0";                
                sqlstr0 += "        AND m.IdAbonnement =  @IdAbonnement";
                sqlstr0 += "        AND a.ActiverFacture = 1";
                sqlstr0 += "    ) AS nvx INNER JOIN TA_Materiel mat ON nvx.VID = mat.VID ";
                sqlstr0 += "             INNER JOIN ta_tarif t ON mat.Type_tarif = t.id";
                sqlstr0 += " WHERE nvx.IdAbonnement NOT IN ( SELECT fm.IdAbonnement";
                sqlstr0 += "                                 FROM TA_FactMat fm INNER JOIN TA_FactMat_Detail d ON fm.NumFacture = d.NumFacture";
                sqlstr0 += "                                 WHERE fm.IdAbonnement =  @IdAbonnement";
                sqlstr0 += "                                 AND d.RefProduit = 'CAUT')";
               
                cmd.CommandText = sqlstr0;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("IdAbonnement", IdAbon);

                DataTable dtMateriel = new DataTable();

                dtMateriel.Load(cmd.ExecuteReader());    //On execute

                //On doit au moins avoir 1 matos
                if (dtMateriel.Rows.Count > 0)
                {
                    string TypeCaution = "";
                    
                    //On regarde quel type de matériel on a                    
                    for (int i = 0; i < dtMateriel.Rows.Count; i++)
                    {
                        if (dtMateriel.Rows[i]["Type_Tarif"].ToString() == "L3GSL")
                            TypeCaution = "CTL3GSL";  
                        else if (dtMateriel.Rows[i]["Type_Tarif"].ToString() == "L4")
                            TypeCaution = "CTL4";
                    }

                    //On a un boitier 3G ou 4G donc on fait une caution
                    if (TypeCaution != "")
                    {
                        //On récupère le tarif correspondant à la caution
                        sqlstr0 = "SELECT Tarif, LibelleTarif FROM ta_tarif WHERE id = '" + TypeCaution + "'";
                        cmd.CommandText = sqlstr0;
                        DataTable dtCaution = new DataTable();

                        dtCaution.Load(cmd.ExecuteReader());    //On execute;

                        decimal TotalFacture = 0;
                        string Libelle = "";

                        if(dtCaution.Rows.Count > 0)
                        {
                            TotalFacture = decimal.Parse(dtCaution.Rows[0]["Tarif"].ToString());
                            Libelle = dtCaution.Rows[0]["LibelleTarif"].ToString();
                        }

                        //Puis on recherche le + grand n° de facture
                        sqlstr0 = "SELECT MAX(NumFacture) FROM TA_FactMat";
                        cmd.CommandText = sqlstr0;

                        DataTable dtMaxFacture = new DataTable();

                        dtMaxFacture.Load(cmd.ExecuteReader());    //On execute;

                        int NumFacture = 0;

                        if (dtMaxFacture.Rows.Count > 0 && dtMaxFacture.Rows[0][0] != DBNull.Value)
                        {
                            NumFacture = Int32.Parse(dtMaxFacture.Rows[0][0].ToString()) + 1;
                        }
                        else
                            NumFacture = 1;

                        //Dans une transaction, on génère la facture, ainsi que le détail de la facture
                        //On ouvre la connexion
                        //dbConnection.Open();                   

                        //On démarre une transaction locale
                        SqlTransaction transaction;
                        transaction = dbConnection.BeginTransaction();

                        try
                        {
                            cmd.Connection = dbConnection;
                            cmd.Transaction = transaction;

                            //On defini la requette
                            string sqlstr1 = "INSERT INTO TA_FactMat (NumFacture, IdAbonnement, DateFacture, TotalFacture, Solde)";
                            sqlstr1 += " VALUES (@NumFacture, @IdAbonnement, getdate(), @TotalFacture, @Solde)";

                            //Ajout des parametres
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("NumFacture", NumFacture);
                            cmd.Parameters.AddWithValue("IdAbonnement", IdAbon);
                            cmd.Parameters.AddWithValue("TotalFacture", TotalFacture);
                            cmd.Parameters.AddWithValue("Solde", TotalFacture);
                            cmd.CommandText = sqlstr1;

                            //Execution de la requette
                            cmd.ExecuteNonQuery();

                            //Puis on ajoute une ligne dans le détail de la facture                           
                            string sqlstr2 = "INSERT INTO TA_FactMat_Detail (NumFacture, RefProduit, Libelle, PrixUnitaire, Reduction, Qte, PrixTotal)";
                            sqlstr2 += " VALUES (@NumFacture, @RefProduit, @Libelle, @PU, 0, 1, @PrixTotal)";

                            //Ajout des parametres
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("NumFacture", NumFacture);
                            cmd.Parameters.AddWithValue("RefProduit", "CAUT");
                            cmd.Parameters.AddWithValue("Libelle", Libelle);
                            cmd.Parameters.AddWithValue("PU", TotalFacture);
                            cmd.Parameters.AddWithValue("PrixTotal", TotalFacture);
                            cmd.CommandText = sqlstr2;

                            //Execution de la requette
                            cmd.ExecuteNonQuery();

                            //On valide la transaction
                            transaction.Commit();

                            Retour = NumFacture.ToString();

                            mouchard.evenement("Création d'une facture de caution matériel" + NumFacture, VariablesApplicatives.Utilisateurs.NomUtilisateur.ToString());  //log            
                        }
                        catch (Exception a)
                        {
                            Console.WriteLine("Erreur : " + a.Message);
                            MessageBox.Show("Erreur lors de la création de la facture n°" + NumFacture + " ....l'erreur est : " + a.Message);
                            transaction.Rollback();
                            Retour = "-1";
                        }
                        finally
                        {
                            //fermeture de la connexion
                            if (dbConnection.State == ConnectionState.Open)
                                dbConnection.Close();
                        }
                    }    //Fin de type caution != ""
                }
                else
                {
                    Retour = "-1";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(" Erreur : " + ex.Message);
                Retour = "-1";
            }
            finally
            {
                //fermeture de la connexion
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }

            return (Retour);
        }



        //#################################################### FIN CREATION DES FACTURES ##############################################################


        private void TA_Facturation_Load(object sender, EventArgs e)
		{
			int month = DateTime.Now.Month +1;
			int monthRap = DateTime.Now.Month -1;
			int year = DateTime.Now.Year;
			if (month < 13)
				dtDebut.Text = "01." + month.ToString() +"." + DateTime.Now.Year;
			else
			{
				year = year +1;
				dtDebut.Text = "01.01."+ year.ToString();
			}

            if (dtpMoisRapport.Enabled == true)
                dtpMoisRapport.Value = DateTime.Parse("01." + DateTime.Now.AddMonths(-1).Month + "." + DateTime.Now.Year);
           
            //On initialise les boutons les pictures box pour la generation des factures
            btImprimer.Enabled = false;
            bImpFactureMat.Enabled = false;
            pictureBox1.Visible = false;

            //On initialise les boutons et boites text pour l'encaissement des factures
            tbNFacture.Text = "";
            dTPDateEncaissement.Text = DateTime.Now.ToString();
            cbMoyenChoix.Text = "";
            textBoxMontant.Text = "";
            btEncFac.Enabled = false;
            pictureBox2.Visible = false;

            //On initialise les boutons Imprimer et Annuler la facture
            tBoxNumFact1.Text = "";
            tBoxNumFact2.Text = "";
            btImprimeFac.Enabled = false;
            btAnnuleFac.Enabled = false;
            pictureBox3.Visible = false;

            //on initialise les boutons de la création d'une facture Abonnement manuellement
            tbNAbonnement.Text = "";
            tbMontant.Text = "";
            cBoxTarif.Text = "";
            pictureBox4.Visible = false;
            btcreationFactMan.Enabled = false;
            btimprFactMan.Enabled = false;
            label10.Visible = false;

            //on initialise les boutons de la création d'une facture Matériel manuellement
            tAbonMatMan.Text = "";            
            pictureBox5.Visible = false;
            bcreatFactMatMan.Enabled = false;
            bimpFactManMat.Enabled = false;
            label24.Visible = false;

            //on initialise les boutons de la création d'une facture de caution matériel
            tBoxCaution.Text = "";
            pictureBox6.Visible = false;
            bCreerFactCaution.Enabled = false;           
            label26.Visible = false;
            bImpFactCaution.Enabled = false;
        }


        private void dtDebut_ValueChanged(object sender, EventArgs e)
        {
            //On initialise les boutons
            btImprimer.Enabled = false;
            bImpFactureMat.Enabled = false;
            pictureBox1.Visible = false;
        }


        private void tbNFacture_Click(object sender, EventArgs e)
        {
            //On initialise les boutons et boites text pour l'encaissement des factures
            tbNFacture.Text = "";
            dTPDateEncaissement.Text = DateTime.Now.ToString();
            cbMoyenChoix.Text = "";
            textBoxMontant.Text = "";
            pictureBox2.Visible = false;
        }

		
		private void btImprimer_Click(object sender, System.EventArgs e)
		{
            //Imprimer toutes les factures
            Cursor.Current = Cursors.WaitCursor;            

            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);

            //dbConnection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = dbConnection; 

			//Vars
			DateTime DtDebut = DateTime.Parse(dtDebut.Text.ToString());
			DateTime DtFin = DateTime.Parse(dtDebut.Text.ToString());
			
			
			//requette pour selectionner les factures à imprimer      
            string sqlstr0 = "SELECT p.Sexe, (p.Prenom+' '+p.Nom) as Nom, taf.NFacture as N_Facture, taf.NCles as N_Cle,";
            sqlstr0 += " taf.Date_facture, taf.Montant, taf.Début_période, taf.Fin_période, taf.Imprimé, tab.Ordre, taf.Remarque, tal.TF_Sexe,";
            sqlstr0 += " (tal.TF_Nom+' '+tal.TF_Prenom) as TF_Nom, tal.TF_NumeroPostal as NP, tal.TF_Localite,";
            //sqlstr0 += " tal.TF_Adresse, p.IdPersonne, tp.IdPatient, tab.IdAbonnement, tab.IdPatient, taf.IdAbonnement,";
            sqlstr0 += " tal.TF_Adresse, p.IdPersonne, tp.IdPatient, tab.IdAbonnement,";
            //sqlstr0 += " tal.TF_IdAbonnement, tp.IdPersonne, taf.Tarif_mensuel, taf.Id_tarif as IdTarif";
            sqlstr0 += " tal.TF_IdAbonnement, taf.Tarif_mensuel, taf.Acquité as Etat, taf.Id_tarif as IdTarif";
            sqlstr0 += " FROM tablepersonne p, tablepatient tp, ta_abonnement tab, ta_factures taf, ta_abonnementlieufacture tal";
            sqlstr0 += " WHERE tab.IdAbonnement = taf.IdAbonnement";
            sqlstr0 += " AND tp.IdPatient = tab.IdPatient";
            sqlstr0 += " AND p.IdPersonne = tp.IdPersonne";
            sqlstr0 += " AND taf.IdAbonnement = tal.TF_IdAbonnement";
            sqlstr0 += " AND taf.Imprimé=0";
            sqlstr0 += " AND taf.Solde <> 0";
            sqlstr0 += " ORDER BY Nom";

            cmd.CommandText = sqlstr0;
                      
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //DataSet dsAbo = null;
            Donnees.MesFactures_TA = new SosMedecins.SmartRapport.DAL.dstTaFacture();
            
            da.Fill(Donnees.MesFactures_TA.Tables[0]);
      			
            foreach (DataRow z_drw in Donnees.MesFactures_TA.Tables[0].Rows)
            {
                PrepareFacture(z_drw, "1er Envoi");
            }
			           
            btImprimer.Visible = false;	

			if(Donnees.MesFactures_TA.Tables.Count>0 && Donnees.MesFactures_TA.Tables[0].Rows.Count>0)
			{
                frmFactures_TA imprFactures = new frmFactures_TA(Donnees.MesFactures_TA, "Factures_QR", "", "");
                imprFactures.ShowDialog();
                imprFactures.Dispose();                
            }
            else
            {
                MessageBox.Show("Aucune facture à imprimer.", "Impression Facture", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            Cursor.Current = Cursors.Default;
		}
									

       
        private void button3_Click(object sender, System.EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            //ImportSosGeneve.Donnees.MesFactures_TA = new SosMedecins.SmartRapport.DAL.dstTaFacture();

            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);

            //On ouvre la connexion
            //dbConnection.Open();

            SqlCommand cmd = dbConnection.CreateCommand();

            DateTime dtDebutRap = DateTime.Parse("01." + dtpMoisRapport.Value.Month + "." + dtpMoisRapport.Value.Year);
            DateTime DtFin = dtDebutRap.AddMonths(1);
            

            //Pour le titre du rapport
            string mois = "";
            switch (dtDebutRap.Month)
            {
                case 1: mois = "Janvier"; break;
                case 2: mois = "Février"; break;
                case 3: mois = "Mars"; break;
                case 4: mois = "Avril"; break;
                case 5: mois = "Mai"; break;
                case 6: mois = "Juin"; break;
                case 7: mois = "Juillet"; break;
                case 8: mois = "Août"; break;
                case 9: mois = "Septembre"; break;
                case 10: mois = "Octobre"; break;
                case 11: mois = "Novembre"; break;
                case 12: mois = "Décembre"; break;
            }

            string year = dtDebutRap.Year.ToString();

            try
            {                               			
                cmd.Connection = dbConnection;

                //On recherche toute les factures payées pour le mois en question
                string sqlstr0 = "SELECT NFacture as N_Facture, NCles as N_Cle, Date_facture, Montant as TotalFacture, Début_période, Fin_période, Imprimé, Remarque, Payé as FacDateAcquittee ";
                sqlstr0 += "FROM ta_factures WHERE Payé >= @DateDebut And Payé < @DateFin";

                cmd.CommandText = sqlstr0;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("DateDebut", dtDebutRap);
                cmd.Parameters.AddWithValue("DateFin", DtFin);

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet dsListeEncaissement = new DataSet();
                dsListeEncaissement.Tables.Add("TA_Factures");
                da.Fill(dsListeEncaissement.Tables["TA_Factures"]);   //On charge le dataset

                Cursor.Current = Cursors.Default;
                
                //On passe à la fenêtre de l'etat Crystal
                if (dsListeEncaissement.Tables["TA_Factures"].Rows.Count > 0)
                {                   
                    frmFactures_TA imprFactures = new frmFactures_TA(dsListeEncaissement, "FacEncaissée", mois, year);
                    imprFactures.ShowDialog();
                    imprFactures.Dispose();
                    imprFactures = null;
                }
                else
                {
                    MessageBox.Show("Il n'y a aucune facture encaissée pour le mois de " + dtDebutRap.Month.ToString() + " " + dtDebutRap.Year.ToString(),
                                        "Liste des factures encaissées", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Erreur lors de la recherche des factures TA encaissées", ex.Message);
            }
        }

		private void btImprimeFac_Click(object sender, System.EventArgs e)
		{
            //Pour imprimer une seule facture
            int id = int.Parse(tBoxNumFact1.Text.ToString());

            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = dbConnection;
             
            //En fonction des rb coché on selection ou abonnement ou matériel
            if (rB1.Checked)
            {
                //Abonnement
                //requette pour selectionner les factures à imprimer          
                string sqlstr0 = "SELECT p.Sexe, (p.Prenom+' '+p.Nom) as Nom, taf.NFacture as N_Facture, taf.NCles as N_Cle,";
                sqlstr0 += " taf.Date_facture, taf.Montant, taf.Début_période, taf.Fin_période, taf.Imprimé, tab.Ordre, taf.Remarque, tal.TF_Sexe,";
                sqlstr0 += " (tal.TF_Nom+' '+tal.TF_Prenom) as TF_Nom, tal.TF_NumeroPostal as NP, tal.TF_Localite,";
                sqlstr0 += " tal.TF_Adresse, p.IdPersonne, tp.IdPatient, tab.IdAbonnement, tab.IdPatient, taf.IdAbonnement,";
                sqlstr0 += " tal.TF_IdAbonnement, tp.IdPersonne, taf.Tarif_mensuel, taf.Id_tarif as IdTarif";
                sqlstr0 += " FROM tablepersonne p, tablepatient tp, ta_abonnement tab, ta_factures taf, ta_abonnementlieufacture tal";
                sqlstr0 += " WHERE tab.IdAbonnement = taf.IdAbonnement";
                sqlstr0 += " AND tp.IdPatient = tab.IdPatient";
                sqlstr0 += " AND p.IdPersonne = tp.IdPersonne";
                sqlstr0 += " AND taf.IdAbonnement = tal.TF_IdAbonnement";
                sqlstr0 += " AND taf.NFacture = '" + id.ToString() + "'";

                cmd.CommandText = sqlstr0;

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                Donnees.MesFactures_TA = new SosMedecins.SmartRapport.DAL.dstTaFacture();

                da.Fill(Donnees.MesFactures_TA.Tables[0]);

                PrepareFacture(Donnees.MesFactures_TA.Tables[0].Rows[0], "1er Envoi");

                if (Donnees.MesFactures_TA.Tables.Count > 0 && Donnees.MesFactures_TA.Tables[0].Rows.Count > 0)
                {
                   frmFactures_TA imprFactures = new frmFactures_TA(Donnees.MesFactures_TA, "Factures_QR", "", "");
                   imprFactures.ShowDialog();
                   imprFactures.Dispose();                   
                }
                else
                {
                    MessageBox.Show("Aucune facture Abonnement à imprimer", "Facture abonnement", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else  //Matériel
            {
                //requette pour selectionner les factures à imprimer                
                string sqlstr0 = "SELECT  p.Sexe, (p.Prenom+' '+p.Nom) as Nom, ";
                sqlstr0 += " tlf.TF_Sexe, (tlf.TF_Nom+' '+tlf.TF_Prenom) as TF_Nom, tlf.TF_Adresse, tlf.TF_NumeroPostal, tlf.TF_Localite,";
                sqlstr0 += " fm.NumFacture, fm.DateFacture, fm.Date_1er_rappel, fm.Date_2nd_Rappel,";
                sqlstr0 += " fm.Totalfacture, fm.solde, fm.DateAcquitementFact, fm.DateAnnulation, fm.DateImpression, fm.IdAbonnement,";
                sqlstr0 += " '' as Reference_number, '' as Coding_Line, ta.Ordre AS OrdrePermanent";
                sqlstr0 += " FROM tablepersonne p, tablepatient pa, ta_abonnement ta, ta_abonnementlieufacture tlf,  TA_FactMat fm";
                sqlstr0 += " WHERE p.IdPersonne = pa.IdPersonne";
                sqlstr0 += " AND pa.IdPatient = ta.IdPatient";
                sqlstr0 += " AND ta.IdAbonnement = tlf.TF_IdAbonnement";
                sqlstr0 += " AND ta.IdAbonnement = fm.IdAbonnement";               
                sqlstr0 += " AND fm.NumFacture = '" + id.ToString() + "'";

                cmd.CommandText = sqlstr0;

                /* SqlDataAdapter da = new SqlDataAdapter(cmd);
                 DataSet dtsFactMat = new DataSet();
                 dtsFactMat.Tables.Add("FactureMat");
                 da.Fill(dtsFactMat.Tables["FactureMat"]); */ //On charge le DataSet (On pourra modifier le comptenu)
                
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                Donnees.MesFactures_TA = new SosMedecins.SmartRapport.DAL.dstTaFacture();
                da.Fill(Donnees.MesFactures_TA.Tables["FactureMat"]);                

                //Puis on selectionne les lignes détail pour la facture
                sqlstr0 = "SELECT fd.IdDetail, fd.NumFacture, fd.RefProduit, fd.Libelle, fd.PrixUnitaire, fd.Reduction, fd.Qte, fd.PrixTotal";
                sqlstr0 += " FROM TA_FactMat fm, TA_FactMat_Detail fd";
                sqlstr0 += " WHERE fm.NumFacture = fd.NumFacture";
                //sqlstr0 += " AND fm.DateImpression is null";
                sqlstr0 += " AND fm.NumFacture = '" + id.ToString() + "'";
                sqlstr0 += " ORDER BY fd.IdDetail";

                cmd.CommandText = sqlstr0;

                /*SqlDataAdapter da1 = new SqlDataAdapter(cmd);
                dtsFactMat.Tables.Add("FactureDetail");                
                da1.Fill(dtsFactMat.Tables["FactureDetail"]);*/
               
                SqlDataAdapter da1 = new SqlDataAdapter(cmd);                             
                da1.Fill(Donnees.MesFactures_TA.Tables["FactureDetail"]);

                foreach (DataRow row in Donnees.MesFactures_TA.Tables["FactureMat"].Rows)
                {
                    PrepareFactureMat(row, "Facture");
                }

                // if (dtsFactMat.Tables["FactureMat"].Rows.Count > 0)
                if (Donnees.MesFactures_TA.Tables["FactureMat"].Rows.Count > 0)               
                {                   
                    frmFactures_TA imprFacturesMat = new frmFactures_TA(Donnees.MesFactures_TA, "Materiel_QR", null, null);
                    imprFacturesMat.ShowDialog();
                    imprFacturesMat.Dispose();                                              
                }
                else
                {
                    MessageBox.Show("Aucune facture matériel à imprimer","Facture Matériel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }                
            } //Fin de fact Matériel
		}


        private void btEncFac_Click(object sender, System.EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            string NFacture = tbNFacture.Text.ToString();
            dTPDateEncaissement.Format = DateTimePickerFormat.Custom;
            dTPDateEncaissement.CustomFormat = "dd.MM.yyyy";
            
            //string dtPay = dTPDateEncaissement.Text.ToString();
            //string year = dtPay.Substring(0, 2);
            //string month = dtPay.Substring(2, 2);
            //string day = dtPay.Substring(4, 2);
            string moyen = cbMoyenChoix.Text.ToString();
            decimal Montant = 0;

            try
            {
                Montant = decimal.Parse(textBoxMontant.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Le montant n'est pas correct : " + ex.Message);
                return;
            }

            string TypeFacture = "";

            //Matériel ou abonnement?
            if (rBAbonnement.Checked)
                TypeFacture = "TA Abonnement";
            else
                TypeFacture = "TA Matériel";

            //on vérifie si la factrue existe          
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);       

            dbConnection.Open();

            SqlCommand cmd = dbConnection.CreateCommand();
           
            //requette pour selectionner les factures à imprimer                
            string sqlstr0 = "";

            if (TypeFacture == "TA Abonnement")
            {
                sqlstr0 = "SELECT * FROM ta_factures WHERE NFacture = @NumFact";
            }
            else
            {
                sqlstr0 = "SELECT * FROM Ta_FactMat WHERE Numfacture = @NumFact";
            }

            cmd.CommandText = sqlstr0;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("NumFact", NFacture.ToString());

            DataSet dtsFact = new DataSet();
            dtsFact.Tables.Add("Facture");
            dtsFact.Tables["Facture"].Load(cmd.ExecuteReader());

            //da.Fill(dtsFact.Tables["Facture"]);  //On charge le DataSet (On pourra modifier le comptenu)

            if (dtsFact.Tables["Facture"].Rows != null && dtsFact.Tables["Facture"].Rows.Count > 0)
            {
                SqlTransaction transaction;             //Pour l'insert avec transaction
                transaction = dbConnection.BeginTransaction("transac");     //Démarre une transaction locale
                
                try
                {
                    cmd.Connection = dbConnection;
                    cmd.Transaction = transaction;    //On affecte la transaction


                    decimal MontantImput = Montant;
                    decimal MontantImputRestant = 0;
                    decimal Solde = 0;
                    string Filtre = "";

                    //On met à jour cette facture TA
                    decimal SoldeFacture = decimal.Parse(dtsFact.Tables[0].Rows[0]["Solde"].ToString());
                    decimal MontantOp = 0;

                    //Mise à jour du Solde de la facture                                        
                    if (MontantImput >= SoldeFacture)
                    {
                        //Montant à imputer >= au solde de la facture donc on solde la facture                                                    
                        MontantImputRestant = MontantImput - SoldeFacture;
                        MontantOp = SoldeFacture;
                        Solde = 0;

                        if (TypeFacture == "TA Abonnement")
                            //facture soldée (TA Abonnement)
                            Filtre = " SET Acquité = 1, Moyen = '" + moyen + "', SBVR = 1, Solde = @Solde, Payé = '" + dTPDateEncaissement.Value + "'";
                        else
                            //facture soldée (TA Mat)
                            Filtre = " SET DateAcquitementFact = '" + dTPDateEncaissement.Value + "', Solde = @Solde";
                    }
                    else //Donc pas assez pour solder la facture
                    {
                        MontantImputRestant = 0;
                        MontantOp = MontantImput;
                        Solde = SoldeFacture - MontantOp;

                        if (TypeFacture == "TA Abonnement")
                            //Facture Non soldée   Solde = SoldeFacture - MontantOp  (TA Abonnement)                                                 
                            Filtre = " SET Acquité = 0, Moyen = '"+ moyen + "', SBVR = 1, Solde = @Solde";
                        else
                            //Facture Non soldée   Solde = SoldeFacture - MontantOp  (TA Materiel)                                                 
                            Filtre = " SET Solde = @Solde";
                    }

                    //Maj des tables
                    string sqlstr1 = "";

                    if (TypeFacture == "TA Abonnement")
                    {
                        sqlstr1 = "UPDATE ta_factures";
                        sqlstr1 += Filtre;
                        sqlstr1 += " WHERE NFacture = @NumFacture";
                    }
                    else
                    {
                        sqlstr1 = "UPDATE TA_FactMat";
                        sqlstr1 += Filtre;
                        sqlstr1 += " WHERE NumFacture = @NumFacture";
                    }

                    cmd.CommandText = sqlstr1;

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("NumFacture", NFacture.ToString());
                    cmd.Parameters.AddWithValue("Solde", Solde);

                    cmd.ExecuteNonQuery();

                    //Puis ajout d'une ligne dans les op
                    string sqlstr2 = "";
                    if (TypeFacture == "TA Abonnement")
                    {
                        sqlstr2 = "INSERT INTO TA_Factures_Op";
                        sqlstr2 += " (NumFacture, DateEncaissement, Montant, MoyenPaiement)";
                        sqlstr2 += " VALUES(@NFacture, @DtEncaissement, @Montant, @Moyen)";
                    }
                    else
                    {
                        sqlstr2 = "INSERT INTO TA_FactMat_Op";
                        sqlstr2 += " (NumFacture, DateEncaissement, Montant, MoyenPaiement)";
                        sqlstr2 += " VALUES(@NFacture, @DtEncaissement, @Montant, @Moyen)";
                    }

                    cmd.CommandText = sqlstr2;

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("NFacture", NFacture.ToString());
                    cmd.Parameters.AddWithValue("DtEncaissement", dTPDateEncaissement.Value);
                    cmd.Parameters.AddWithValue("Montant", MontantOp.ToString());
                    cmd.Parameters.AddWithValue("Moyen", moyen);

                    cmd.ExecuteNonQuery();

                    Cursor.Current = Cursors.Default;
                    pictureBox2.Image = imageList1.Images[0];
                    pictureBox2.Visible = true;

                    //On commit la transaction                    
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur " + ex, NFacture.ToString());

                    //On essaie de faire un Rollback
                    try
                    {
                        transaction.Rollback();      //On fait un rollback
                    }
                    catch (Exception ex2)
                    {
                        //On gère ici toute kes erreurs qui ont pu survenir pour empêcher le Rollback...
                        //comme par exemple une connexion fermée...
                        Console.WriteLine("Rollback Exeption Type: {0}", ex2.GetType());
                        Console.WriteLine("   Message: {0}", ex2.Message);
                    }
                }
                finally
                {
                    if (dbConnection.State == System.Data.ConnectionState.Open)
                        dbConnection.Close();
                }
            }
            else
            {
                Cursor.Current = Cursors.Default;
                pictureBox2.Image = imageList1.Images[3];
                pictureBox2.Visible = true;
            }
        }

		private void btAnnuleFac_Click(object sender, System.EventArgs e)
		{
            //On annule une facture
            string NFacture = tBoxNumFact2.Text.ToString();
            if (rB1.Checked)
            {
                //Facture abonnement
                DataRow[] rows = OutilsExt.OutilsSql.RecupereFacturesTAPayeMan(long.Parse(NFacture.ToString()));
                
                if (rows != null && rows.Length == 1)
                {
                    //Maj de la table                    
                    DialogResult result = MessageBox.Show("Une facture Abonnement d'un montant de " + rows[0]["Montant"].ToString() + " datant du " 
                                          + rows[0]["Date_facture"].ToString() + " a été trouvée est ce bien cette facture?", "Facture abonnement trouvée", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        string z_strRequete = "UPDATE ta_factures SET ta_factures.Moyen = 'Annulée' , dateAnnulation = Getdate() WHERE NFacture = '" + NFacture + "'";

                        OutilsExt.OutilsSql.ExecuteCommandeSansRetour(z_strRequete);

                        pictureBox3.Image = imageList1.Images[0];
                        pictureBox3.Visible = true;
                    }
                }
                else
                {
                    MessageBox.Show("Aucune facture avec ce n° n'a été trouvée...Peut être une facture Matériel?", "Annulation de facture", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    pictureBox3.Image = imageList1.Images[3];
                    pictureBox3.Visible = true;
                }
            }
            else    //Facture Matériel
            {
                //On regarde si elle existe               
                string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
                SqlConnection dbConnection = new SqlConnection(connex);
            
                dbConnection.Open();

                SqlCommand cmd = dbConnection.CreateCommand();
           
                //requette pour selectionner les factures à imprimer                
                string sqlstr0 = "";

                sqlstr0 = "SELECT * FROM Ta_FactMat WHERE Numfacture = '" + NFacture + "'";
            
                cmd.CommandText = sqlstr0;
           
                DataTable dtFact = new DataTable();           
                dtFact.Load(cmd.ExecuteReader());          

                if (dtFact.Rows != null && dtFact.Rows.Count > 0)
                {
                     //Maj de la table                    
                    DialogResult result = MessageBox.Show("Une facture Matériel d'un montant de " + dtFact.Rows[0]["TotalFacture"].ToString() + " datant du " 
                                          + dtFact.Rows[0]["DateFacture"].ToString() + " a été trouvée est ce bien cette facture?", "Facture matériel trouvée", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        //Maj de la table Materiel et de la table Ta_FactMat_Op
                        string SqlStr0 = "UPDATE TA_FactMat SET DateAnnulation = Getdate() WHERE NumFacture = '" + NFacture + "'";

                        OutilsExt.OutilsSql.ExecuteCommandeSansRetour(SqlStr0);

                        string SqlStr1 = "INSERT INTO TA_FactMat_Op";
                        SqlStr1 += " (NumFacture, DateEncaissement, Montant, MoyenPaiement)";
                        SqlStr1 += " VALUES('" + NFacture + "',  Getdate(), 0, 'Annulée')";

                        OutilsExt.OutilsSql.ExecuteCommandeSansRetour(SqlStr1);

                        pictureBox3.Image = imageList1.Images[0];
                        pictureBox3.Visible = true;
                    }
                }
                else
                {
                    MessageBox.Show("Aucune facture n'a été trouvée avec ce n°...Peut être une facture abonnement?", "Annulation de facture", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    pictureBox3.Image = imageList1.Images[3];
                    pictureBox3.Visible = true;
                }
            }  //Fin de materiel
            
		}

	

        private void PrepareFacture(DataRow p_drw, string typeFacture)
        {
            //Montant de la facture                  
            //Nouvelle formule
            string strMyFac = p_drw["N_Facture"].ToString();
            float floId = float.Parse(p_drw["Idabonnement"].ToString());
            string strCode1 = float.Parse(p_drw["Montant"].ToString()).ToString("0.00");            

            string reference_number = CalculRefNumber("2", strMyFac, p_drw["Idabonnement"].ToString());
            string Coding_Line = CodingLine(reference_number, strCode1);            
           
            p_drw["StrCode2"] = reference_number;
            p_drw["StrCode1"] = Coding_Line;

            // set titre patient
            if (p_drw["Sexe"].ToString() == "F")
                p_drw["Sexe"] = "Madame";
            else
                p_drw["Sexe"] = "M.";
            // set titre destinateur de facture
            if (p_drw["TF_Nom"].ToString() == p_drw["Nom"].ToString())
                p_drw["TF_Sexe"] = p_drw["Sexe"];
            else if (p_drw["TF_Sexe"].ToString() == "F")
                p_drw["TF_Sexe"] = "Madame";
            else if (p_drw["TF_Sexe"].ToString() == "A")
                p_drw["TF_Sexe"] = "";
            else
                p_drw["TF_Sexe"] = "M.";
            
            //On formate la date
            DateTime DateDeb;
            DateTime DateFin;

            DateDeb = DateTime.ParseExact(p_drw["Début_période"].ToString(), "dd.MM.yyyy hh:mm:ss", CultureInfo.InvariantCulture);
            DateFin = DateTime.ParseExact(p_drw["Fin_période"].ToString(), "dd.MM.yyyy hh:mm:ss", CultureInfo.InvariantCulture);

            p_drw["StrDateDebut"] = DateDeb.ToString("dd MMMM yyyy");
            p_drw["StrDateFin"] = DateFin.ToString("dd MMMM yyyy"); ;

            //enter date debut et date fin comme string
           // p_drw["idAbon"] = p_drw["Idabonnement"].ToString();
            p_drw["NCle"] = p_drw["N_Cle"].ToString();
            p_drw["TF_NumeroPostal"] = p_drw["NP"].ToString();

            //On met un QRCode  ###### Ancienne Librairie
            /*SwissQrCode.Contact DemominationCompte = SwissQrCode.Contact.WithStructuredAddress("Sos Medecins Cite Calvin SA", "1201", "Geneve", "CH");
            SwissQrCode.Iban SOSQRIban = new SwissQrCode.Iban("CH2430000001120014992", PayloadGenerator.SwissQrCode.Iban.IbanType.QrIban);
            //SwissQrCode.Iban SOSQRIban = new SwissQrCode.Iban("CH9030000002177617705", PayloadGenerator.SwissQrCode.Iban.IbanType.QrIban);   //Pour Test

            SwissQrCode.Reference RefPaiement = new SwissQrCode.Reference(SwissQrCode.Reference.ReferenceType.QRR, reference_number.Replace(" ", ""), SwissQrCode.Reference.ReferenceTextType.QrReference);

            SwissQrCode.AdditionalInformation MessageSurBvr = new SwissQrCode.AdditionalInformation("", "");
            SwissQrCode.Currency Monnaie = SwissQrCode.Currency.CHF;

            decimal amount = decimal.Parse(p_drw["Montant"].ToString());

            byte[] QRCodeRetour = GenereQrCode(DemominationCompte, SOSQRIban, RefPaiement, MessageSurBvr, Monnaie, amount);*/
            //********************

            decimal amount = decimal.Parse(p_drw["Montant"].ToString());

            //On passe les paramettre du bulletin (dont le QRCode)
            Bill bill = new Bill
            {
                //Créditeur                
                Account = "CH2430000001120014992",
                Creditor = new Address
                {
                    Name = "Sos Medecins Cite Calvin SA",
                    AddressLine1 = "Rue Louis Favre, 43",
                    AddressLine2 = "1201 Genève",
                    CountryCode = "CH"
                },

                //Paiement
                Amount = decimal.Parse(p_drw["Montant"].ToString()),
                Currency = "CHF",

                //Débiteur
                Debtor = new Address
                {
                    Name = p_drw["TF_Sexe"].ToString() + " " + p_drw["TF_Nom"].ToString(),
                    AddressLine1 = p_drw["TF_Adresse"].ToString(),
                    AddressLine2 = p_drw["NP"].ToString() + " " + p_drw["TF_Localite"].ToString(),
                    CountryCode = "CH"
                },

                //Référence du paiement
                Reference = reference_number.Replace(" ", ""),
                UnstructuredMessage = "Abonnement " + p_drw["TF_IdAbonnement"].ToString(),


                //On défini le format de sortie, ici png, et suelement le QRCode, pas le bulletin
                Format = new BillFormat
                {
                    Language = Language.FR,
                    GraphicsFormat = GraphicsFormat.PNG,
                    OutputSize = OutputSize.QrCodeOnly
                }
            };

            //Génération de la facture dans un [] de byte que l'on met dans QRCode
            try
            {
                byte[] QRCodeRetour = QRBill.Generate(bill);
                p_drw["QRCode"] = QRCodeRetour;
            }
            catch (Exception e)
            {
                MessageBox.Show("Erreur dans le QRCode : " + e.Message);
            }
            
            //Mise à jour de la facture
            if (typeFacture == "RappelsTA")
            {                
                //Mise à jour de la date des rappels (soit dans 1er rappel soit dans second rappel)
                string sqlstr0 = "UPDATE ta_factures SET [1_er_rappel] = CASE WHEN [1_er_rappel] is null THEN getdate() ";
                       sqlstr0 += "                                           ELSE [1_er_rappel] END, ";
                       sqlstr0 += "                      [2_nd_rappel] = CASE WHEN [1_er_rappel] is null THEN null ";
                       sqlstr0 += "                                             ELSE getdate() END ";
                       sqlstr0 += " WHERE NFacture = '" + strMyFac + "'";
                    
                OutilsExt.OutilsSql.ExecuteCommandeSansRetour(sqlstr0);

                OutilsExt.OutilsSql.InsereLigneJournal(int.Parse(p_drw["idAbonnement"].ToString()), "Rappel Abon.", "facturation", "", DateTime.Today, VariablesApplicatives.Utilisateurs.NomUtilisateur.ToString(), "", "Envoi de rappel pour la facture " + p_drw["N_Facture"].ToString());                      
            }
            else
            {
                //Mise à jour de la facture => "Imprimé" si ce n'est pas le cas
                if (p_drw["Imprimé"].ToString() != "1")
                    OutilsExt.OutilsSql.ExecuteCommandeSansRetour("update ta_factures set ta_factures.Imprimé=1 where ta_factures.NFacture = '" + strMyFac + "'");
            }
        }

             
        //************************************Pour la ligne de codage ***************************************************
        //utilisé pour l'instant uniquement avec facture Mat Ta
        
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
        public String CalculRefNumber(string TypeFacture, string RefFacture, string IdAbonnement)
        {            
            string CodeFinal = "";
            string FactCompl = Complete(RefFacture, 8);
            string IntervCompl = Complete(IdAbonnement, 6);

            CodeFinal = TypeFacture + Complete(FactCompl + IntervCompl, 19);  //1 pour Sos; 2 pour Ab TA; 3 pour Ta Mat;                   
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
            str1 = str1 + ">" + reference_number + "+ 010221601>";   //TA
            
            //str1 = str1 + "&gt;" + Complete16caracteres(reference_number) + "+ 010306272&gt;";

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

       
        //############ Facture Matériel #############################################
        private void bImpFactureMat_Click(object sender, EventArgs e)
        {
            //On imprime les facture Materiel           
            Cursor.Current = Cursors.WaitCursor;          

            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = dbConnection;
           
            //requette pour selectionner les factures à imprimer                
            string sqlstr0 = "SELECT  p.Sexe, (p.Prenom+' '+p.Nom) as Nom, ";
            sqlstr0 += " tlf.TF_Sexe, (tlf.TF_Nom+' '+tlf.TF_Prenom) as TF_Nom, tlf.TF_Adresse, tlf.TF_NumeroPostal, tlf.TF_Localite,";
            sqlstr0 += " fm.NumFacture, fm.DateFacture, fm.Date_1er_rappel, fm.Date_2nd_Rappel,";
            sqlstr0 += " fm.Totalfacture, fm.solde, fm.DateAcquitementFact, fm.DateAnnulation, fm.DateImpression, fm.IdAbonnement,";
            sqlstr0 += " '' as Reference_number, '' as Coding_Line, ta.Ordre AS OrdrePermanent";                        
            sqlstr0 += " FROM tablepersonne p, tablepatient pa, ta_abonnement ta, ta_abonnementlieufacture tlf,  TA_FactMat fm";
            sqlstr0 += " WHERE p.IdPersonne = pa.IdPersonne";
            sqlstr0 += " AND pa.IdPatient = ta.IdPatient";
            sqlstr0 += " AND ta.IdAbonnement = tlf.TF_IdAbonnement";
            sqlstr0 += " AND ta.IdAbonnement = fm.IdAbonnement";
            sqlstr0 += " AND fm.DateImpression is null";

            cmd.CommandText = sqlstr0;         

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            Donnees.MesFactures_TA = new SosMedecins.SmartRapport.DAL.dstTaFacture();
            da.Fill(Donnees.MesFactures_TA.Tables["FactureMat"]);   //On charge le DataSet (On pourra modifier le comptenu)

            //Puis on selectionne les lignes détail pour les factures concernées          
            sqlstr0 = "SELECT fd.IdDetail, fd.NumFacture, fd.RefProduit, fd.Libelle, fd.PrixUnitaire, fd.Reduction, fd.Qte, fd.PrixTotal";
            sqlstr0 += " FROM TA_FactMat fm, TA_FactMat_Detail fd";
            sqlstr0 += " WHERE fm.NumFacture = fd.NumFacture";
            sqlstr0 += " AND fm.DateImpression is null";
            sqlstr0 += " ORDER BY fd.IdDetail";
                              
            cmd.CommandText = sqlstr0;

            SqlDataAdapter da1 = new SqlDataAdapter(cmd);            
            da1.Fill(Donnees.MesFactures_TA.Tables["FactureDetail"]);

            foreach (DataRow row in Donnees.MesFactures_TA.Tables["FactureMat"].Rows)
            {
                PrepareFactureMat(row, "Facture");
            }
         
            if (Donnees.MesFactures_TA.Tables["FactureMat"].Rows.Count > 0)
            {
               frmFactures_TA imprFacturesMat = new frmFactures_TA(Donnees.MesFactures_TA, "Materiel_QR", null, null);
               imprFacturesMat.ShowDialog();
               imprFacturesMat.Dispose();                
            }
            else
            {
                MessageBox.Show("Aucune facture à imprimer.", "Impression Facture Matériel", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            Cursor.Current = Cursors.Default;
        }


        private void PrepareFactureMat(DataRow p_drw, string TypeFacture)
        {
            //Montant de la facture         
            string NumFact = p_drw["NumFacture"].ToString();
            float floId = float.Parse(p_drw["Idabonnement"].ToString());
            string strCode1 = float.Parse(p_drw["Totalfacture"].ToString()).ToString("0.00");

            string reference_number = CalculRefNumber("3", NumFact, p_drw["Idabonnement"].ToString());
            string Coding_Line = CodingLine(reference_number, strCode1);

            p_drw["Reference_number"] = reference_number;
            p_drw["Coding_Line"] = Coding_Line;

            //titre patient
            if (p_drw["Sexe"].ToString() == "F")
                p_drw["Sexe"] = "Madame";
            else
                p_drw["Sexe"] = "M.";
            
            //titre destinataire de facture
            if (p_drw["TF_Nom"].ToString() == p_drw["Nom"].ToString())
                p_drw["TF_Sexe"] = p_drw["Sexe"];
            else if (p_drw["TF_Sexe"].ToString() == "F")
                p_drw["TF_Sexe"] = "Madame";
            else if (p_drw["TF_Sexe"].ToString() == "A")
                p_drw["TF_Sexe"] = "";
            else
                p_drw["TF_Sexe"] = "M.";

            /*
            //On defini le QRCode
            SwissQrCode.Contact DemominationCompte = SwissQrCode.Contact.WithStructuredAddress("Sos Medecins Cite Calvin SA", "1201", "Geneve", "CH");
            SwissQrCode.Iban SOSQRIban = new SwissQrCode.Iban("CH2430000001120014992", PayloadGenerator.SwissQrCode.Iban.IbanType.QrIban);
            //SwissQrCode.Iban SOSQRIban = new SwissQrCode.Iban("CH9030000002177617705", PayloadGenerator.SwissQrCode.Iban.IbanType.QrIban);   //Pour Test

            SwissQrCode.Reference RefPaiement = new SwissQrCode.Reference(SwissQrCode.Reference.ReferenceType.QRR, reference_number.Replace(" ", ""), SwissQrCode.Reference.ReferenceTextType.QrReference);

            SwissQrCode.AdditionalInformation MessageSurBvr = new SwissQrCode.AdditionalInformation("", "");
            SwissQrCode.Currency Monnaie = SwissQrCode.Currency.CHF;

            decimal amount = decimal.Parse(p_drw["Totalfacture"].ToString());

            byte[] QRCodeRetour = GenereQrCode(DemominationCompte, SOSQRIban, RefPaiement, MessageSurBvr, Monnaie, amount);

            */
            decimal amount = decimal.Parse(p_drw["Totalfacture"].ToString());

            //On passe les paramettre du bulletin (dont le QRCode)
            Bill bill = new Bill
            {
                //Créditeur
                Account = "CH2430000001120014992",
                Creditor = new Address
                {
                    Name = "Sos Medecins Cite Calvin SA",
                    AddressLine1 = "Rue Louis Favre, 43",
                    AddressLine2 = "1201 Genève",
                    CountryCode = "CH"
                },

                //Paiement
                Amount = decimal.Parse(p_drw["Totalfacture"].ToString()),
                Currency = "CHF",

                //Débiteur
                Debtor = new Address
                {
                    Name = p_drw["TF_Sexe"].ToString() + " " + p_drw["TF_Nom"].ToString(),
                    AddressLine1 = p_drw["TF_Adresse"].ToString(),
                    AddressLine2 = p_drw["TF_NumeroPostal"].ToString() + " " + p_drw["TF_Localite"].ToString(),
                    CountryCode = "CH"
                },

                //Référence du paiement
                Reference = reference_number.Replace(" ", ""),
                UnstructuredMessage = "Abonnement " + p_drw["Idabonnement"].ToString(),

                //On défini le format de sortie, ici png, et suelement le QRCode, pas le bulletin
                Format = new BillFormat
                {
                    Language = Language.FR,
                    GraphicsFormat = GraphicsFormat.PNG,
                    OutputSize = OutputSize.QrCodeOnly
                }
            };

            //Génération de la facture dans un [] de byte que l'on met dans QRCode            
            try
            {
                byte[] QRCodeRetour = QRBill.Generate(bill);
                p_drw["QRCode"] = QRCodeRetour;
            }
            catch (Exception e)
            {
                MessageBox.Show("Erreur dans le QRCode : " + e.Message);
            }


            //Mise à jour des dates de la facture
            if (TypeFacture == "Rappel")
            {
                //Mise à jour de la date des rappels (soit dans 1er rappel soit dans second rappel)
                string sqlstr0 = "UPDATE Ta_FactMat SET Date_1er_Rappel = CASE WHEN Date_1er_Rappel is null THEN getdate() ";
                       sqlstr0 += "                                            ELSE Date_1er_Rappel END, ";
                       sqlstr0 += "                     Date_2nd_Rappel = CASE WHEN Date_1er_Rappel is null THEN null ";
                       sqlstr0 += "                                            ELSE getdate() END ";
                       sqlstr0 += " WHERE NumFacture = '" + NumFact + "'";
                    
                OutilsExt.OutilsSql.ExecuteCommandeSansRetour(sqlstr0);

                OutilsExt.OutilsSql.InsereLigneJournal(int.Parse(p_drw["idAbonnement"].ToString()), "Rappel Mat.", "facturation", "", DateTime.Today, VariablesApplicatives.Utilisateurs.NomUtilisateur.ToString(), "", "Envoi de rappel pour la facture Matériel " + p_drw["NumFacture"].ToString());                                        
            }
            else
            {
              //Mise a jour date impression
              if (p_drw["DateImpression"] == DBNull.Value)
                  OutilsExt.OutilsSql.ExecuteCommandeSansRetour("update Ta_FactMat set DateImpression=getdate() where NumFacture = '" + NumFact + "'");
            }                      
        }

        private void tBoxNumFact1_TextChanged(object sender, EventArgs e)
        {
            //s'il y a quelque chose dans le champs, on active le bouton
            if (tBoxNumFact1.Text.Length != 0)
            {
                btImprimeFac.Enabled = true;                
            }
            else
            {
                btImprimeFac.Enabled = false;
                pictureBox2.Visible = false;
            }
        }

        private void tBoxNumFact2_TextChanged(object sender, EventArgs e)
        {
            //s'il y a quelque chose dans le champs, on active le bouton
            if (tBoxNumFact2.Text.Length != 0)
            {
                btAnnuleFac.Enabled = true;
            }
            else
            {
                btAnnuleFac.Enabled = false;
                pictureBox3.Visible = false;
            }
        }

        private void tbNAbonnement_TextChanged(object sender, EventArgs e)
        {
            //s'il y a quelque chose dans LES champs, on active le bouton création
            if (tbNAbonnement.Text.Length != 0 && tbMontant.Text.Length != 0)
            {
                btcreationFactMan.Enabled = true;
            }
            else
            {
                btcreationFactMan.Enabled = false;
                label10.Visible = false;
                btimprFactMan.Enabled = false;
                pictureBox4.Visible = false;
            }
        }

        private void tbMontant_TextChanged(object sender, EventArgs e)
        {
            //s'il y a quelque chose dans LES champs, on active le bouton création
            if (tbNAbonnement.Text.Length != 0 && tbMontant.Text.Length != 0)
            {
                btcreationFactMan.Enabled = true;
            }
            else
            {
                label10.Visible = false;
                btcreationFactMan.Enabled = false;
                btimprFactMan.Enabled = false;
                pictureBox4.Visible = false;
            }
        }

        private void btcreationFactMan_Click(object sender, EventArgs e)
        {           
            //Dates des périodes de la facture
            DateTime DtDebut = DateTime.Parse(dtDebutFac.Text.ToString());
            DateTime DtFin = DateTime.Parse(dtFinFac.Text.ToString());
            decimal Montant = 0;
            decimal MontantMensuel = 0;


            //On test si la date de fin est <= à la date de début
            if (DtFin <= DtDebut)
            {
                MessageBox.Show("Attention, la date de fin de période doit être postérieure à la date de début !!!", "Dates de la période de facturation",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                Montant = decimal.Parse(tbMontant.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Le montant d'abonnement n'est pas correct ", ex.Message);
                return;
            }

            if (cBoxTarif.Text == "")
            {
                MessageBox.Show("Veuillez choisir une périodicité dans la liste", "Périodicité",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                //On détermine le tarif mensuel
                switch (cBoxTarif.Text)
                {
                    case "M":
                    case "MeM": MontantMensuel = Montant; break;

                    case "T":
                    case "MeT": MontantMensuel = Montant / 3; break;

                    case "S":
                    case "MeS": MontantMensuel = Montant / 6; break;

                    case "A":
                    case "MeA": MontantMensuel = Montant / 12; break;
                }
            }


            //Recup du dernier n° de facture
            int max = -1;

            string[][] retour = OutilsExt.OutilsSql.ExecuteCommandeAvecTabString("SELECT max(NFacture) from ta_factures");
            if (retour != null && retour.Length != 0 && retour[0][0] != "")
            {
                max = int.Parse(retour[0][0]) + 1;
            }
            else
            {
                max = 1;
            }
            
            //On cherche le N_TA et la Clé
            int NCle = -1;
            //int IdAbo = -1;
            int N_TA = -1;


            string[][] id = OutilsExt.OutilsSql.ExecuteCommandeAvecTabString("SELECT NumeroCle from ta_abonnementcle WHERE IdAbonnement ='" + tbNAbonnement.Text.ToString() + "' order by DateAttribution desc");
            if (id != null && id.Length != 0 && id[0][0] != "")
            {
                NCle = int.Parse(id[0][0]);
            }

            string[][] NTA = OutilsExt.OutilsSql.ExecuteCommandeAvecTabString("SELECT N_TA from ta_abonnement WHERE idAbonnement ='" + tbNAbonnement.Text.ToString() + "'");
            if (NTA != null && NTA.Length != 0 && NTA[0][0] != "")
            {
                N_TA = int.Parse(NTA[0][0]);
            }



            //On créer une nouvelle facture ABONNEMENT et on met le tag imprimé
            OutilsExt.OutilsSql.ExecuteCommandeSansRetour("INSERT INTO ta_factures (NFacture,IdAbonnement, NTA, NCles, Date_facture, Montant, Début_période, Fin_période, [1_er_rappel], [2_nd_rappel], Payé, Moyen, Acquité, Tarif_mensuel, Imprimé, SBVR, Remarque, Solde, id_tarif) VALUES ('" + max + "', '" + tbNAbonnement.Text.ToString() + "','" + N_TA.ToString() + "', '" + NCle + "', '" + OutilsExt.OutilsSql.DateFormatMySql(DateTime.Now) + "', '" + tbMontant.Text.ToString() + "', '" + OutilsExt.OutilsSql.DateFormatMySql(DtDebut) + "', '" + OutilsExt.OutilsSql.DateFormatMySql(DtFin) + "', NULL, NULL, NULL, NULL, 0, '" + MontantMensuel.ToString() +"', 1, 0, NULL, '" + tbMontant.Text.ToString() + "', '" + cBoxTarif.Text + "')");

            //On attribue le n° de NFacture au tag du bouton print
            btimprFactMan.Tag = max.ToString();          
            btimprFactMan.Visible = true;    
       
            //On indique le n° de facture
            label10.Text = "Et le n° de facture est : " + max.ToString();
            label10.Visible = true;
            pictureBox4.Image = imageList1.Images[0];
            pictureBox4.Visible = true;                             
        }

        private void btimprFactMan_Click(object sender, EventArgs e)
        {
            //On imprime la facture nouvellement créée           
            int id = int.Parse(btimprFactMan.Tag.ToString());
            
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = dbConnection;

            //requette pour selectionner la facture à imprimer      
            string sqlstr0 = "SELECT p.Sexe, (p.Prenom+' '+p.Nom) as Nom, taf.NFacture as N_Facture, taf.NCles as N_Cle,";
            sqlstr0 += " taf.Date_facture, taf.Montant, taf.Début_période, taf.Fin_période, taf.Imprimé, tab.Ordre, taf.Remarque, tal.TF_Sexe,";
            sqlstr0 += " (tal.TF_Nom+' '+tal.TF_Prenom) as TF_Nom, tal.TF_NumeroPostal as NP, tal.TF_Localite,";
            sqlstr0 += " tal.TF_Adresse, p.IdPersonne, tp.IdPatient, tab.IdAbonnement, tab.IdPatient, taf.IdAbonnement,";
            sqlstr0 += " tal.TF_IdAbonnement, tp.IdPersonne, taf.Tarif_mensuel, taf.Id_tarif as IdTarif";
            sqlstr0 += " FROM tablepersonne p, tablepatient tp, ta_abonnement tab, ta_factures taf, ta_abonnementlieufacture tal";
            sqlstr0 += " WHERE tab.IdAbonnement = taf.IdAbonnement";
            sqlstr0 += " AND tp.IdPatient = tab.IdPatient";
            sqlstr0 += " AND p.IdPersonne = tp.IdPersonne";
            sqlstr0 += " AND taf.IdAbonnement = tal.TF_IdAbonnement";
            sqlstr0 += " AND taf.Imprimé=0";
            //sqlstr0 += " AND taf.IdAbonnement = '" + id.ToString() + "'";
            sqlstr0 += " AND taf.NFacture = '" + id.ToString() + "'";

            cmd.CommandText = sqlstr0;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            Donnees.MesFactures_TA = new SosMedecins.SmartRapport.DAL.dstTaFacture();

            da.Fill(Donnees.MesFactures_TA.Tables[0]);
                
            foreach (DataRow z_drw in Donnees.MesFactures_TA.Tables[0].Rows)
            {
                PrepareFacture(z_drw, "1er Envoi");
            }

            btimprFactMan.Enabled = false;
                       
            if (Donnees.MesFactures_TA.Tables.Count > 0 && Donnees.MesFactures_TA.Tables[0].Rows.Count > 0)
            {
               frmFactures_TA imprFactures = new frmFactures_TA(Donnees.MesFactures_TA, "Factures_QR", "", "");
               imprFactures.ShowDialog();
               imprFactures.Dispose();               
            }
            else
            {
                MessageBox.Show("Aucune facture à imprimer", "Impression Facture Abonnement Manuel", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btImpRappelsStopes_Click(object sender, EventArgs e)
        {
            //Affiche, imprime la liste des rappels stoppés
            Cursor.Current = Cursors.WaitCursor;           

            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = dbConnection;

            //Vars
            DateTime DtDebut = dTPrappelstopDeb.Value;
            DateTime DtFin = dTPrappelstopFin.Value;


            //requette pour selectionner les factures à imprimer      
            String sqlstr0 = "SELECT p.Prenom, p.Nom, taf.NCles as N_Cle, p.Sexe, tab.IdAbonnement, p.StopRappelTA";
            sqlstr0 += " FROM ta_factures taf, ta_Abonnement tab, tablepersonne p, tablepatient tp, ta_abonnementlieufacture tal";
            sqlstr0 += " WHERE tp.IdPatient = tab.IdPatient ";
            sqlstr0 += " AND p.IdPersonne= tp.IdPersonne ";
            sqlstr0 += " AND taf.Idabonnement = tab.IdAbonnement ";
            sqlstr0 += " AND  taf.IdAbonnement = tal.TF_IdAbonnement";
            sqlstr0 += " AND p.StopRappelTA is not null ";
            sqlstr0 += " AND p.StopRappelTA between '" + DtDebut.ToString("dd.MM.yyyy") + "' AND '" + DtFin.ToString("dd.MM.yyyy") + "'";
            sqlstr0 += " GROUP BY p.Nom, p.Prenom, taf.NCles, p.Sexe, tab.IdAbonnement, p.StopRappelTA";

            cmd.CommandText = sqlstr0;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            Donnees.StopRappelTA = new SosMedecins.SmartRapport.DAL.dstStopRappels();

            da.Fill(Donnees.StopRappelTA.Tables[0]);

            if (Donnees.StopRappelTA.Tables.Count > 0 && Donnees.StopRappelTA.Tables[0].Rows.Count > 0)
            {
                frmStopRappelsTA imprStopRappelsTA = new frmStopRappelsTA(Donnees.StopRappelTA);
                imprStopRappelsTA.ShowDialog();
                imprStopRappelsTA.Dispose();              
            }
            else
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Pas de rappels TA stopés à imprimer.", "Impression Rappel TA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            Cursor.Current = Cursors.Default;                         
        }

        private void tbNFacture_TextChanged(object sender, EventArgs e)
        {
            //s'il y a quelque chose dans LES champs, on active le bouton création
            if (tbNFacture.Text.Length != 0 && cbMoyenChoix.Text != "" && textBoxMontant.Text != "")
            {
                btEncFac.Enabled = true;
            }
            else
            {
                btEncFac.Enabled = false;                
                pictureBox2.Visible = false;
            }
        }

        private void cbMoyenChoix_TextChanged(object sender, EventArgs e)
        {
            //s'il y a quelque chose dans LES champs, on active le bouton création
            if (tbNFacture.Text.Length != 0 && cbMoyenChoix.Text != "" && textBoxMontant.Text != "")
            {
                btEncFac.Enabled = true;
            }
            else
            {
                btEncFac.Enabled = false;
                pictureBox2.Visible = false;
            }
        }

        private void textBoxMontant_TextChanged(object sender, EventArgs e)
        {
            //s'il y a quelque chose dans LES champs, on active le bouton création
            if (tbNFacture.Text.Length != 0 && cbMoyenChoix.Text != "" && textBoxMontant.Text != "")
            {
                btEncFac.Enabled = true;
            }
            else
            {
                btEncFac.Enabled = false;
                pictureBox2.Visible = false;
            }
        }
      

        private void cBoxActiveImpr_CheckedChanged(object sender, EventArgs e)
        {
            //On réactive les boutons
            if (cBoxActiveImpr.Checked)
            {
                btImprimer.Enabled = true;
                bImpFactureMat.Enabled = true;                
            }
            else
            {
                btImprimer.Enabled = false;
                bImpFactureMat.Enabled = false;
            }

        }

        private void bExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bcreatFactMatMan_Click(object sender, EventArgs e)
        {            
            if (tAbonMatMan.Text != "")
            {
                Cursor.Current = Cursors.WaitCursor;

                string NumFacture = GenereFactureMat(tAbonMatMan.Text);

                if (NumFacture != "-1")
                {
                    //On active le bouton d'impression 
                    //On attribue le n° de NFacture au tag du bouton print
                    bimpFactManMat.Tag = NumFacture;
                   // bimpFactManMat.Visible = true;
                    bimpFactManMat.Enabled = true;

                    //On indique le n° de facture
                    label24.Text = "Et le n° de facture est : " + NumFacture;
                    label24.Visible = true;
                    pictureBox5.Image = imageList1.Images[0];
                    pictureBox5.Visible = true;                                                        
                }                   
                else
                {
                    //Pas de nvx Materiel pour cet abonnement
                    MessageBox.Show("Il n'y a pas de nouveau matériel à facturer sur cet abonnement", "Facture Matériel",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    pictureBox5.Image = imageList1.Images[2];
                    pictureBox5.Visible = true;
                    bimpFactManMat.Enabled = false;
                }

                Cursor.Current = Cursors.Default;                               
            }
            else MessageBox.Show("Veuillez entrer un n° d'abonnenent TA.", "Création Facture Matériel Manuelle", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void bimpFactManMat_Click(object sender, EventArgs e)
        {
            //On imprime la facture nouvellement créée           
            int id = int.Parse(bimpFactManMat.Tag.ToString());           

            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = dbConnection;

            //requette pour selectionner les factures à imprimer                
            string sqlstr0 = "SELECT p.Sexe, (p.Prenom+' '+p.Nom) as Nom, ";
            sqlstr0 += " tlf.TF_Sexe, (tlf.TF_Nom+' '+tlf.TF_Prenom) as TF_Nom, tlf.TF_Adresse, tlf.TF_NumeroPostal, tlf.TF_Localite,";
            sqlstr0 += " fm.NumFacture, fm.DateFacture, fm.Date_1er_rappel, fm.Date_2nd_Rappel,";
            sqlstr0 += " fm.Totalfacture, fm.solde, fm.DateAcquitementFact, fm.DateAnnulation, fm.DateImpression, fm.IdAbonnement,";
            sqlstr0 += " '' as Reference_number, '' as Coding_Line, ta.Ordre AS OrdrePermanent ";
            sqlstr0 += " FROM tablepersonne p, tablepatient pa, ta_abonnement ta, ta_abonnementlieufacture tlf,  TA_FactMat fm";
            sqlstr0 += " WHERE p.IdPersonne = pa.IdPersonne";
            sqlstr0 += " AND pa.IdPatient = ta.IdPatient";
            sqlstr0 += " AND ta.IdAbonnement = tlf.TF_IdAbonnement";
            sqlstr0 += " AND ta.IdAbonnement = fm.IdAbonnement";
            sqlstr0 += " AND fm.NumFacture = '" + id.ToString() + "'";

            cmd.CommandText = sqlstr0;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            Donnees.MesFactures_TA = new SosMedecins.SmartRapport.DAL.dstTaFacture();
            da.Fill(Donnees.MesFactures_TA.Tables["FactureMat"]);          

            //Puis on selectionne les lignes détail pour la facture
            sqlstr0 = "SELECT fd.IdDetail, fd.NumFacture, fd.RefProduit, fd.Libelle, fd.PrixUnitaire, fd.Reduction, fd.Qte, fd.PrixTotal";
            sqlstr0 += " FROM TA_FactMat fm, TA_FactMat_Detail fd";
            sqlstr0 += " WHERE fm.NumFacture = fd.NumFacture";
            //sqlstr0 += " AND fm.DateImpression is null";
            sqlstr0 += " AND fm.NumFacture = '" + id.ToString() + "'";
            sqlstr0 += " ORDER BY fd.IdDetail";

            cmd.CommandText = sqlstr0;

            SqlDataAdapter da1 = new SqlDataAdapter(cmd);
            
            da1.Fill(Donnees.MesFactures_TA.Tables["FactureDetail"]);

            foreach (DataRow row in Donnees.MesFactures_TA.Tables["FactureMat"].Rows)
            {
                PrepareFactureMat(row, "Facture");
            }

            if (Donnees.MesFactures_TA.Tables["FactureMat"].Rows.Count > 0)
            {
              frmFactures_TA imprFacturesMat = new frmFactures_TA(Donnees.MesFactures_TA, "Materiel_QR", null, null);
              imprFacturesMat.ShowDialog();
              imprFacturesMat.Dispose();                
            }
            else
            {
                MessageBox.Show("Aucune facture matériel à imprimer", "Impression Facture Mat", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }                           
        }

        private void tAbonMatMan_Leave(object sender, EventArgs e)
        {
            if (tAbonMatMan.Text != "-1" || tAbonMatMan.Text != "")
            {
                bcreatFactMatMan.Enabled = true;
            }
            else bcreatFactMatMan.Enabled = false;
        }

        //Impressions de tout les rappels
        private void bRappels_Click(object sender, EventArgs e)
        {
            ImprimeToutRappelsTA();
            ImprimeToutRappelsTaMat();
        }


        private void ImprimeToutRappelsTA()
        {
            //Imprimer toutes les factures
            Cursor.Current = Cursors.WaitCursor;

            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);

            //dbConnection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = dbConnection;
            
            //*****************************************
            DateTime DtDebut = DateTime.Now.AddMonths(-3).AddYears(-2);
            DateTime DtFin = DateTime.Now.AddMonths(-4);

            //requette pour selectionner les factures à imprimer (on prend le solde > 1 pour montant de la facture)    
            string sqlstr0 = "SELECT p.Sexe, (p.Prenom+' '+p.Nom) as Nom, taf.NFacture as N_Facture, taf.NCles as N_Cle,";
            sqlstr0 += " taf.Date_facture, taf.Solde as Montant, taf.Début_période, taf.Fin_période, taf.Imprimé, tab.Ordre, taf.Remarque, tal.TF_Sexe,";
            sqlstr0 += " (tal.TF_Nom+' '+tal.TF_Prenom) as TF_Nom, tal.TF_NumeroPostal as NP, tal.TF_Localite,";
            //sqlstr0 += " tal.TF_Adresse, p.IdPersonne, tp.IdPatient, tab.IdAbonnement as idAbonnement, tab.IdPatient, taf.IdAbonnement,";
            sqlstr0 += " tal.TF_Adresse, p.IdPersonne, tp.IdPatient, tab.IdAbonnement,";
            //sqlstr0 += " tal.TF_IdAbonnement, tp.IdPersonne, taf.Tarif_mensuel, taf.Id_tarif as IdTarif";
            sqlstr0 += " tal.TF_IdAbonnement, taf.Tarif_mensuel, taf.Acquité as Etat, taf.Id_tarif as IdTarif";
            sqlstr0 += " FROM tablepersonne p, tablepatient tp, ta_abonnement tab, ta_factures taf, ta_abonnementlieufacture tal";
            sqlstr0 += " WHERE tab.IdAbonnement = taf.IdAbonnement";
            sqlstr0 += " AND tp.IdPatient = tab.IdPatient";
            sqlstr0 += " AND p.IdPersonne = tp.IdPersonne";
            sqlstr0 += " AND taf.IdAbonnement = tal.TF_IdAbonnement";
            sqlstr0 += " AND taf.[Acquité] = 0";
            sqlstr0 += " AND tab.Archive = 0";
            sqlstr0 += " AND taf.DateAnnulation is null";
            sqlstr0 += " AND p.StopRappelTA is null";
            sqlstr0 += " AND taf.Solde >= 1";
            sqlstr0 += " AND taf.Date_facture between '" + DtDebut + "' and '" + DtFin + "'";
            sqlstr0 += " AND (taf.[1_er_rappel] is null OR taf.[2_nd_rappel] is null) ";
            sqlstr0 += " ORDER BY Nom";

            cmd.CommandText = sqlstr0;


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            
            Donnees.MesFactures_TA = new SosMedecins.SmartRapport.DAL.dstTaFacture();
            da.Fill(Donnees.MesFactures_TA.Tables[0]);

            foreach (DataRow z_drw in Donnees.MesFactures_TA.Tables[0].Rows)
            {
                PrepareFacture(z_drw, "RappelsTA");
            }

            if (Donnees.MesFactures_TA.Tables.Count > 0 && Donnees.MesFactures_TA.Tables[0].Rows.Count > 0)
            {
               frmFactures_TA imprFactures = new frmFactures_TA(Donnees.MesFactures_TA, "RappelsTA_QR", "", "");
               imprFactures.ShowDialog();
               imprFactures.Dispose();                
            }
            else
            {
                MessageBox.Show("Aucune facture Abonnement à imprimer.", "Impression Facture", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            Cursor.Current = Cursors.Default;
        }


        private void ImprimeToutRappelsTaMat()
        {         
            //On imprime les facture Materiel                                   
            Cursor.Current = Cursors.WaitCursor;          

            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = dbConnection;

            DateTime DtDebut = DateTime.Now.AddMonths(-3).AddYears(-2);
            DateTime DtFin = DateTime.Now.AddMonths(-4);
           
            //requette pour selectionner les factures à imprimer  (on prend le solde > 1 pour montant de la facture)              
            string sqlstr0 = "SELECT  p.Sexe, (p.Prenom+' '+p.Nom) as Nom, ";
            sqlstr0 += " tlf.TF_Sexe, (tlf.TF_Nom+' '+tlf.TF_Prenom) as TF_Nom, tlf.TF_Adresse, tlf.TF_NumeroPostal, tlf.TF_Localite,";
            sqlstr0 += " fm.NumFacture, fm.DateFacture, fm.Date_1er_rappel, fm.Date_2nd_Rappel,";
            sqlstr0 += " fm.solde AS Totalfacture, fm.solde, fm.DateAcquitementFact, fm.DateAnnulation, fm.DateImpression, fm.IdAbonnement,";
            sqlstr0 += " '' as Reference_number, '' as Coding_Line, ta.Ordre AS OrdrePermanent";
            sqlstr0 += " FROM tablepersonne p INNER JOIN tablepatient pa ON p.IdPersonne = pa.IdPersonne";
            sqlstr0 += "                      INNER JOIN ta_abonnement ta ON pa.IdPatient = ta.IdPatient";
            sqlstr0 += "                      INNER JOIN ta_abonnementlieufacture tlf ON ta.IdAbonnement = tlf.TF_IdAbonnement";
            sqlstr0 += "                      INNER JOIN TA_FactMat fm ON ta.IdAbonnement = fm.IdAbonnement ";            
            sqlstr0 += " WHERE fm.DateImpression is not null";
            sqlstr0 += " AND fm.DateAnnulation is null";
            sqlstr0 += " AND p.StopRappelTA is null";
            sqlstr0 += " AND ta.Archive = 0";
            sqlstr0 += " AND fm.Solde >= 1";                     
            sqlstr0 += " AND fm.DateFacture between '" + DtDebut + "' and '" + DtFin + "'";
            sqlstr0 += " AND (fm.Date_1er_Rappel is null OR fm.Date_2nd_Rappel is null) ";
            sqlstr0 += " ORDER BY Nom";

            cmd.CommandText = sqlstr0;

            SqlDataAdapter da = new SqlDataAdapter(cmd);          

            Donnees.MesFactures_TA = new SosMedecins.SmartRapport.DAL.dstTaFacture();
            da.Fill(Donnees.MesFactures_TA.Tables["FactureMat"]);
   
            //Puis on selectionne les lignes détail pour les factures concernées          
            sqlstr0 = "SELECT fd.IdDetail, fd.NumFacture, fd.RefProduit, fd.Libelle, fd.PrixUnitaire, fd.Reduction, fd.Qte, fd.PrixTotal";
            sqlstr0 += " FROM TA_FactMat fm INNER JOIN TA_FactMat_Detail fd ON fm.NumFacture = fd.NumFacture";
            sqlstr0 += "                    INNER JOIN ta_abonnement ta ON ta.IdAbonnement = fm.IdAbonnement ";  
            sqlstr0 += "                    INNER JOIN tablepatient pa ON pa.IdPatient = ta.IdPatient";
            sqlstr0 += "                    INNER JOIN tablepersonne p ON p.IdPersonne = pa.IdPersonne";
            sqlstr0 += " WHERE fm.DateImpression is not null";
            sqlstr0 += " AND fm.DateAnnulation is null";
            sqlstr0 += " AND p.StopRappelTA is null";
            sqlstr0 += " AND ta.Archive = 0";
            sqlstr0 += " AND fm.TotalFacture = fm.Solde";
            sqlstr0 += " AND (fm.Date_1er_Rappel is null OR fm.Date_2nd_Rappel is null) ";
            sqlstr0 += " AND fm.DateFacture between '" + DtDebut + "' and '" + DtFin + "'";                                   
                              
            cmd.CommandText = sqlstr0;

            SqlDataAdapter da1 = new SqlDataAdapter(cmd);
            da.Fill(Donnees.MesFactures_TA.Tables["FactureDetail"]);

            foreach (DataRow row in Donnees.MesFactures_TA.Tables["FactureMat"].Rows)
            {
                PrepareFactureMat(row, "Rappel");
            }

            //btImprimer.Visible = false;

            if (Donnees.MesFactures_TA.Tables.Count > 0 && Donnees.MesFactures_TA.Tables["FactureMat"].Rows.Count > 0)
            {               
                frmFactures_TA imprFacturesMat = new frmFactures_TA(Donnees.MesFactures_TA, "RappelTaMat_QR", null, null);
                imprFacturesMat.ShowDialog();
                imprFacturesMat.Dispose();                
            }
            else
            {
                MessageBox.Show("Aucune facture rappel Matériel à imprimer.", "Impression Facture Rappel Matériel", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            Cursor.Current = Cursors.Default;        
        }

     

        //********************Pour le document à joindre à la facture (A virer après)***********************
        private void JointDocFacture(int IdAbonnement)
        {
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);

            //On ouvre la connexion
            dbConnection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = dbConnection;

            try
            {
                string sqlstr0 = "UPDATE TA_DocAEnvoyer ";
                sqlstr0 += " SET DateEnvoi = getDate()";
                sqlstr0 += " WHERE DateEnvoi is null AND Id = " + IdAbonnement;

                cmd.CommandText = sqlstr0;

                //Execution de la requette
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la mise a jour de la table pour l'envoi du document pour l'abonnement " + IdAbonnement.ToString() + " \r\nLe message est :" + ex.Message);
            }
        }

        private void bCreerFactCaution_Click(object sender, EventArgs e)
        {
            if (tBoxCaution.Text != "")
            {
                Cursor.Current = Cursors.WaitCursor;

                string NumFacture = GenereFactureCaution(tBoxCaution.Text);

                if (NumFacture != "-1")
                {
                    //On active le bouton d'impression 
                    //On attribue le n° de NFacture au tag du bouton print
                    bImpFactCaution.Tag = NumFacture;
                    // bimpFactManMat.Visible = true;
                    bImpFactCaution.Enabled = true;

                    //On indique le n° de facture
                    label26.Text = "Et le n° de facture est : " + NumFacture;
                    label26.Visible = true;
                    pictureBox6.Image = imageList1.Images[0];
                    pictureBox6.Visible = true;
                }
                else
                {
                    //Pas de nvx Materiel pour cet abonnement
                    MessageBox.Show("Il n'y a pas de nouveau matériel à cautionner sur cet abonnement.", "Facture de caution",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);

                    pictureBox6.Image = imageList1.Images[2];
                    pictureBox6.Visible = true;
                    bimpFactManMat.Enabled = false;
                }

                Cursor.Current = Cursors.Default;
            }
            else MessageBox.Show("Veuillez entrer un n° d'abonnenent TA.", "Création facture de caution matériel manuelle", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void bImpFactCaution_Click(object sender, EventArgs e)
        {
            //On imprime la facture nouvellement créée           
            int id = int.Parse(bImpFactCaution.Tag.ToString());

            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = dbConnection;

            //requette pour selectionner les factures à imprimer                
            string sqlstr0 = "SELECT p.Sexe, (p.Prenom+' '+p.Nom) as Nom, ";
            sqlstr0 += " tlf.TF_Sexe, (tlf.TF_Nom+' '+tlf.TF_Prenom) as TF_Nom, tlf.TF_Adresse, tlf.TF_NumeroPostal, tlf.TF_Localite,";
            sqlstr0 += " fm.NumFacture, fm.DateFacture, fm.Date_1er_rappel, fm.Date_2nd_Rappel,";
            sqlstr0 += " fm.Totalfacture, fm.solde, fm.DateAcquitementFact, fm.DateAnnulation, fm.DateImpression, fm.IdAbonnement,";
            sqlstr0 += " '' as Reference_number, '' as Coding_Line, ta.Ordre AS OrdrePermanent ";
            sqlstr0 += " FROM tablepersonne p, tablepatient pa, ta_abonnement ta, ta_abonnementlieufacture tlf,  TA_FactMat fm";
            sqlstr0 += " WHERE p.IdPersonne = pa.IdPersonne";
            sqlstr0 += " AND pa.IdPatient = ta.IdPatient";
            sqlstr0 += " AND ta.IdAbonnement = tlf.TF_IdAbonnement";
            sqlstr0 += " AND ta.IdAbonnement = fm.IdAbonnement";
            sqlstr0 += " AND fm.NumFacture = '" + id.ToString() + "'";

            cmd.CommandText = sqlstr0;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            Donnees.MesFactures_TA = new SosMedecins.SmartRapport.DAL.dstTaFacture();
            da.Fill(Donnees.MesFactures_TA.Tables["FactureMat"]);

            //Puis on selectionne les lignes détail pour la facture
            sqlstr0 = "SELECT fd.IdDetail, fd.NumFacture, fd.RefProduit, fd.Libelle, fd.PrixUnitaire, fd.Reduction, fd.Qte, fd.PrixTotal";
            sqlstr0 += " FROM TA_FactMat fm, TA_FactMat_Detail fd";
            sqlstr0 += " WHERE fm.NumFacture = fd.NumFacture";
            //sqlstr0 += " AND fm.DateImpression is null";
            sqlstr0 += " AND fm.NumFacture = '" + id.ToString() + "'";
            sqlstr0 += " ORDER BY fd.IdDetail";

            cmd.CommandText = sqlstr0;

            SqlDataAdapter da1 = new SqlDataAdapter(cmd);

            da1.Fill(Donnees.MesFactures_TA.Tables["FactureDetail"]);

            foreach (DataRow row in Donnees.MesFactures_TA.Tables["FactureMat"].Rows)
            {
                PrepareFactureMat(row, "Facture");
            }

            if (Donnees.MesFactures_TA.Tables["FactureMat"].Rows.Count > 0)
            {
                frmFactures_TA imprFacturesMat = new frmFactures_TA(Donnees.MesFactures_TA, "Materiel_QR", null, null);
                imprFacturesMat.ShowDialog();
                imprFacturesMat.Dispose();
            }
            else
            {
                MessageBox.Show("Aucune facture de caution matériel à imprimer", "Impression Facture caution", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void tBoxCaution_Leave(object sender, EventArgs e)
        {
            if (tBoxCaution.Text != "-1" || tBoxCaution.Text != "")
            {
                bCreerFactCaution.Enabled = true;
            }
            else bCreerFactCaution.Enabled = false;
        }
    }
}

//A faire
