using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using SosMedecins.SmartRapport.DAL;
using SosMedecins.SmartRapport.GestionApplication;
using System.Net;
using System.Net.Http;
using System.IO;
using System.Xml;


namespace ImportSosGeneve
{
	public class FIP : System.Windows.Forms.Form
	{
		#region Déclaration des variables

		// Déclaration des variables globales
		private GradientCellType Gradient1 = new GradientCellType();
		private GradientCellType Gradient2 = new GradientCellType();
        private CtrlDest m_CtrlDest = null;
        private DataRow _drwPatientRemarque = null;

		public frmGeneral m_frmgeneral=null;
		public CtrlDest.TypeOuverture m_TypeOuvertureDestinataire = CtrlDest.TypeOuverture.DefautFacture;
		public DateTime DateDebut;
		public DateTime DateFin;
		public enum TypeOuverture{Normal,Facturation,Patient,PoliceTarmed};
		public Dest DestinataireRetour10=null;
		public CtrlDest.TypeOuverture m_TypeOuvertureDestinataire10 = CtrlDest.TypeOuverture.ListeFacture;

        public bool TropLong;

		// Controles du formulaire : 

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox chkNom;
		private System.Windows.Forms.CheckBox chkPrenom;
		private System.Windows.Forms.CheckBox chkTelephone;
		private System.Windows.Forms.CheckBox chkDtNaissance;
		private System.Windows.Forms.CheckBox chkCommune;
		private System.Windows.Forms.CheckBox chkRue;
		private System.Windows.Forms.RadioButton rdAdrAdm;
		private System.Windows.Forms.RadioButton rdAdrInter;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.CheckBox chkNp;
		private System.Windows.Forms.TextBox Find_Nom;
        private System.Windows.Forms.TextBox Find_Prenom;
		private System.Windows.Forms.TextBox Find_DtNaissance;
		private System.Windows.Forms.TextBox Find_Rue;
		private System.Windows.Forms.TextBox Find_Np;
        private System.Windows.Forms.TextBox Find_Commune;
		private FarPoint.Win.Spread.FpSpread fpListe;
		private FarPoint.Win.Spread.SheetView fpListe_Sheet1;
		private System.Windows.Forms.Label LbNbResultat;
		private System.Windows.Forms.TabControl tabFip;
		private System.Windows.Forms.TabPage tbFiche;
        private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Label LblIndice;
        private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox TxtNom;
        private System.Windows.Forms.TextBox TxtPrenom;
		private System.Windows.Forms.TextBox TxtDtNaissance;
		private System.Windows.Forms.ComboBox CbSexe;
		private System.Windows.Forms.TextBox TxtNum;
		private System.Windows.Forms.TextBox TxtAdresse1;
		private System.Windows.Forms.TextBox TxtNp;
		private System.Windows.Forms.TextBox TxtCommune;
		private System.Windows.Forms.TextBox TxtCommune_Adm;
		private System.Windows.Forms.TextBox TxtNp_Adm;
		private System.Windows.Forms.TextBox TxtRue_AdmAdresse1;
        private System.Windows.Forms.TextBox TxtNum_Adm;
		private System.Windows.Forms.TabPage tbRem;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Label lblRemarqueEconomique;
        private System.Windows.Forms.RichTextBox TxtRemEco;
		private System.Windows.Forms.RichTextBox TxtRemMed;
        private System.Windows.Forms.Label LblRemarqueMedical;
		private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtDepartement;
		private System.Windows.Forms.TextBox txtEscalier;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.TextBox txtEtage;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.TextBox txtDigicode;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.TextBox txtPorte;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.TextBox txtInterphone;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.TextBox txtBatiment_Adm;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label LblStatut;
        private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.TabPage tbMed;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.LinkLabel lnkAjoutMedTT;
		private System.Windows.Forms.LinkLabel lnkSupMedTT;
		private System.Windows.Forms.Panel pan_MedTT;
		private System.Windows.Forms.ListBox lstMedTT;
		private System.Windows.Forms.TextBox txtDtDeces;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TabPage tbFactu;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.Label LblDestinataireFacture;
		private System.Windows.Forms.LinkLabel lnkChangerDestinataireFacture;
		private System.Windows.Forms.ListView lwDestinataireDate;
		private System.Windows.Forms.Label label27;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.Label label28;
		private System.Windows.Forms.TextBox TxtChez;
		private System.Windows.Forms.Button JointDoc;
		private System.Windows.Forms.TextBox Find_Num;
		private System.Windows.Forms.CheckBox chkNum;
		private System.Windows.Forms.Button btFacture10;
		private System.Windows.Forms.Label lbDestinataire10;
        private System.Windows.Forms.Label label29;
        private Button btnRecherche;
        private Button btnSavePatient;
        private Button btnListeAppels;
        private Button btnRapports;
        private Button btnFactures;
        private GroupBox grpContentieux;
        private CheckBox chkCessionCreance;
        private CheckBox chkSurPlace;
        private ComboBox CBRoute_adm1;
        private Label label7;
        private Label label67;
        private Label label8;
        private Label label9;
        private TextBox TxtRue_AdmAdresse2;
        private Label label64;
        private Label label66;
        private Label label12;
        private Label label30;
        private Label label11;
        private ComboBox CBRoute1;
        private Label label14;
        private Label label13;
        private Label label15;
        private TextBox TxtAdresse2;
        private MaskedTextBox EMaskTel1;
        private Button bautre;
        private Button BAnalyses;
        private Button BECG;
        private Label label31;
        private Label label17;
        private TextBox tBoxNumAVS;
        private TextBox tBoxNunAssure;
        private TextBox TxtMail;
        private Label label32;
        private Button btAppels2;
        private TextBox Find_Tel;
        private Button btInfoPatient;
        private Label label33;
        private TextBox tBNumCarte;
		private System.ComponentModel.IContainer components;

        //Pour recuperer les infos de la carte
        public static string NomAssure = "", PrenomAssure = "", DateNaissanceAssure = "", AVSAssure = "", GenreAssure = "";
        public static string NumAssure = "";
        private Button bFermer;
        private Label label34;
        private TextBox txtAdmPays;
        public static string TypedOuverture = "";

		#endregion

		#region Construction / Destruction du formulaire

		// Constructeur
		public FIP(frmGeneral frmparent)
		{	
			this.m_frmgeneral = frmparent;

			InitializeComponent();
			InitializeListe();
			AffichagePatient(null);
		}

        // Constructeur pour policeTarmed
        public FIP(long IndicePatient, FIP.TypeOuverture typeOuverture)
        {            
            InitializeComponent();
            InitializeListe();

            TypedOuverture = typeOuverture.ToString();

            if (typeOuverture == TypeOuverture.PoliceTarmed)
            {                                
                DataSet ds = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet("SELECT pe.*,pa.IdPatient,pa.SuiviPatient,pa.TypeDestinataireFacture,pa.IdDestinataireFacture from tablepersonne pe left join tablepatient pa on pa.IdPersonne = pe.IdPersonne WHERE pa.IdPatient =" + IndicePatient);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count == 1)
                {
                    AffichagePatient(ds.Tables[0].Rows[0]);
                    btnListeAppels.Enabled = false;
                    btnFactures.Enabled = false;
                    btnFactures.Enabled = false;
                }

                tabFip.SelectedIndex = 3;                                    
            }            
         //   AffichagePatient(null);
        }



		// Constructeur
		public FIP(frmGeneral frmparent,long IndicePatient,FIP.TypeOuverture typeOuverture)
		{	
			this.m_frmgeneral = frmparent;

			InitializeComponent();

			InitializeListe();

			if(typeOuverture==TypeOuverture.Facturation)
			{
				DataSet ds = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet("SELECT pe.*,pa.IdPatient,pa.SuiviPatient,pa.TypeDestinataireFacture,pa.IdDestinataireFacture from tablepersonne pe left join tablepatient pa on pa.IdPersonne = pe.IdPersonne WHERE pa.IdPatient =" + IndicePatient);
				if(ds!=null && ds.Tables.Count>0 && ds.Tables[0].Rows.Count==1)
				{                   
					AffichagePatient(ds.Tables[0].Rows[0]);
					btnListeAppels.Enabled = false;
					btnFactures.Enabled =false;
					btnFactures.Enabled = false;
				}
			}
			else if(typeOuverture==TypeOuverture.Patient)
			{
				this.Cursor = Cursors.WaitCursor;
				DataSet ds = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet("SELECT pe.*,pa.IdPatient,pa.SuiviPatient,pa.TypeDestinataireFacture,pa.IdDestinataireFacture from tablepersonne pe left join tablepatient pa on pa.IdPersonne = pe.IdPersonne WHERE pa.IdPatient = " + IndicePatient);

				// Traitement du dataset pour y insérer les patients doublons et les grouper par doublons
				TraitementDoublons(ds);
				ListePatient(ds);
				TriDoublon();

				// Tri des données par groupe de doublons
				//TriDoublons();

				this.Cursor = Cursors.Default;
			}            

			if(typeOuverture==TypeOuverture.Facturation)
			{
				tabFip.SelectedIndex=3;
			}
			else tabFip.SelectedIndex=0;
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

		#endregion

		#region Code généré par le Concepteur Windows Form
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FIP));
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btAppels2 = new System.Windows.Forms.Button();
            this.btnRecherche = new System.Windows.Forms.Button();
            this.Find_Num = new System.Windows.Forms.TextBox();
            this.chkNum = new System.Windows.Forms.CheckBox();
            this.Find_Commune = new System.Windows.Forms.TextBox();
            this.Find_Np = new System.Windows.Forms.TextBox();
            this.Find_Rue = new System.Windows.Forms.TextBox();
            this.Find_DtNaissance = new System.Windows.Forms.TextBox();
            this.Find_Tel = new System.Windows.Forms.TextBox();
            this.Find_Prenom = new System.Windows.Forms.TextBox();
            this.Find_Nom = new System.Windows.Forms.TextBox();
            this.rdAdrInter = new System.Windows.Forms.RadioButton();
            this.rdAdrAdm = new System.Windows.Forms.RadioButton();
            this.chkCommune = new System.Windows.Forms.CheckBox();
            this.chkNp = new System.Windows.Forms.CheckBox();
            this.chkRue = new System.Windows.Forms.CheckBox();
            this.chkDtNaissance = new System.Windows.Forms.CheckBox();
            this.chkTelephone = new System.Windows.Forms.CheckBox();
            this.chkPrenom = new System.Windows.Forms.CheckBox();
            this.chkNom = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.LbNbResultat = new System.Windows.Forms.Label();
            this.fpListe = new FarPoint.Win.Spread.FpSpread();
            this.fpListe_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.tabFip = new System.Windows.Forms.TabControl();
            this.tbFiche = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label33 = new System.Windows.Forms.Label();
            this.tBNumCarte = new System.Windows.Forms.TextBox();
            this.btInfoPatient = new System.Windows.Forms.Button();
            this.TxtMail = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.tBoxNumAVS = new System.Windows.Forms.TextBox();
            this.tBoxNunAssure = new System.Windows.Forms.TextBox();
            this.bautre = new System.Windows.Forms.Button();
            this.BAnalyses = new System.Windows.Forms.Button();
            this.BECG = new System.Windows.Forms.Button();
            this.EMaskTel1 = new System.Windows.Forms.MaskedTextBox();
            this.txtDtDeces = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.CbSexe = new System.Windows.Forms.ComboBox();
            this.TxtDtNaissance = new System.Windows.Forms.TextBox();
            this.TxtPrenom = new System.Windows.Forms.TextBox();
            this.TxtNom = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.LblIndice = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.TxtAdresse2 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.CBRoute1 = new System.Windows.Forms.ComboBox();
            this.TxtChez = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.txtPorte = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txtInterphone = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txtEtage = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtDigicode = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txtEscalier = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtDepartement = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.TxtCommune = new System.Windows.Forms.TextBox();
            this.TxtNp = new System.Windows.Forms.TextBox();
            this.TxtAdresse1 = new System.Windows.Forms.TextBox();
            this.TxtNum = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label34 = new System.Windows.Forms.Label();
            this.txtAdmPays = new System.Windows.Forms.TextBox();
            this.label66 = new System.Windows.Forms.Label();
            this.TxtRue_AdmAdresse2 = new System.Windows.Forms.TextBox();
            this.label64 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label67 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.CBRoute_adm1 = new System.Windows.Forms.ComboBox();
            this.txtBatiment_Adm = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.TxtNum_Adm = new System.Windows.Forms.TextBox();
            this.TxtCommune_Adm = new System.Windows.Forms.TextBox();
            this.TxtNp_Adm = new System.Windows.Forms.TextBox();
            this.TxtRue_AdmAdresse1 = new System.Windows.Forms.TextBox();
            this.tbRem = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.grpContentieux = new System.Windows.Forms.GroupBox();
            this.chkCessionCreance = new System.Windows.Forms.CheckBox();
            this.chkSurPlace = new System.Windows.Forms.CheckBox();
            this.TxtRemMed = new System.Windows.Forms.RichTextBox();
            this.LblRemarqueMedical = new System.Windows.Forms.Label();
            this.TxtRemEco = new System.Windows.Forms.RichTextBox();
            this.lblRemarqueEconomique = new System.Windows.Forms.Label();
            this.tbMed = new System.Windows.Forms.TabPage();
            this.pan_MedTT = new System.Windows.Forms.Panel();
            this.lstMedTT = new System.Windows.Forms.ListBox();
            this.lnkSupMedTT = new System.Windows.Forms.LinkLabel();
            this.lnkAjoutMedTT = new System.Windows.Forms.LinkLabel();
            this.label23 = new System.Windows.Forms.Label();
            this.tbFactu = new System.Windows.Forms.TabPage();
            this.label29 = new System.Windows.Forms.Label();
            this.lbDestinataire10 = new System.Windows.Forms.Label();
            this.btFacture10 = new System.Windows.Forms.Button();
            this.label27 = new System.Windows.Forms.Label();
            this.lwDestinataireDate = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lnkChangerDestinataireFacture = new System.Windows.Forms.LinkLabel();
            this.LblDestinataireFacture = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.LblStatut = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnFactures = new System.Windows.Forms.Button();
            this.btnRapports = new System.Windows.Forms.Button();
            this.btnListeAppels = new System.Windows.Forms.Button();
            this.btnSavePatient = new System.Windows.Forms.Button();
            this.bFermer = new System.Windows.Forms.Button();
            this.JointDoc = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpListe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpListe_Sheet1)).BeginInit();
            this.tabFip.SuspendLayout();
            this.tbFiche.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tbRem.SuspendLayout();
            this.panel3.SuspendLayout();
            this.grpContentieux.SuspendLayout();
            this.tbMed.SuspendLayout();
            this.pan_MedTT.SuspendLayout();
            this.tbFactu.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(72, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(480, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "FIP";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btAppels2);
            this.groupBox1.Controls.Add(this.btnRecherche);
            this.groupBox1.Controls.Add(this.Find_Num);
            this.groupBox1.Controls.Add(this.chkNum);
            this.groupBox1.Controls.Add(this.Find_Commune);
            this.groupBox1.Controls.Add(this.Find_Np);
            this.groupBox1.Controls.Add(this.Find_Rue);
            this.groupBox1.Controls.Add(this.Find_DtNaissance);
            this.groupBox1.Controls.Add(this.Find_Tel);
            this.groupBox1.Controls.Add(this.Find_Prenom);
            this.groupBox1.Controls.Add(this.Find_Nom);
            this.groupBox1.Controls.Add(this.rdAdrInter);
            this.groupBox1.Controls.Add(this.rdAdrAdm);
            this.groupBox1.Controls.Add(this.chkCommune);
            this.groupBox1.Controls.Add(this.chkNp);
            this.groupBox1.Controls.Add(this.chkRue);
            this.groupBox1.Controls.Add(this.chkDtNaissance);
            this.groupBox1.Controls.Add(this.chkTelephone);
            this.groupBox1.Controls.Add(this.chkPrenom);
            this.groupBox1.Controls.Add(this.chkNom);
            this.groupBox1.Location = new System.Drawing.Point(8, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(624, 144);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Critères de recherche";
            // 
            // btAppels2
            // 
            this.btAppels2.BackgroundImage = global::ImportSosGeneve.Properties.Resources.bRechercheAppel;
            this.btAppels2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btAppels2.FlatAppearance.BorderSize = 0;
            this.btAppels2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btAppels2.Location = new System.Drawing.Point(552, 12);
            this.btAppels2.Name = "btAppels2";
            this.btAppels2.Size = new System.Drawing.Size(53, 52);
            this.btAppels2.TabIndex = 75;
            this.toolTip1.SetToolTip(this.btAppels2, "Voir les appels de ce patient");
            this.btAppels2.UseVisualStyleBackColor = true;
            this.btAppels2.Click += new System.EventHandler(this.btnListeAppels_Click);
            // 
            // btnRecherche
            // 
            this.btnRecherche.BackgroundImage = global::ImportSosGeneve.Properties.Resources.bLoupeBleu;
            this.btnRecherche.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnRecherche.FlatAppearance.BorderSize = 0;
            this.btnRecherche.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRecherche.Location = new System.Drawing.Point(485, 12);
            this.btnRecherche.Name = "btnRecherche";
            this.btnRecherche.Size = new System.Drawing.Size(54, 53);
            this.btnRecherche.TabIndex = 20;
            this.toolTip1.SetToolTip(this.btnRecherche, "Rechercher");
            this.btnRecherche.UseVisualStyleBackColor = true;
            this.btnRecherche.Click += new System.EventHandler(this.btnRecherche_Click);
            // 
            // Find_Num
            // 
            this.Find_Num.Location = new System.Drawing.Point(552, 97);
            this.Find_Num.Name = "Find_Num";
            this.Find_Num.Size = new System.Drawing.Size(66, 20);
            this.Find_Num.TabIndex = 15;
            // 
            // chkNum
            // 
            this.chkNum.Location = new System.Drawing.Point(462, 98);
            this.chkNum.Name = "chkNum";
            this.chkNum.Size = new System.Drawing.Size(95, 16);
            this.chkNum.TabIndex = 14;
            this.chkNum.TabStop = false;
            this.chkNum.Text = "Rue Numéro ";
            // 
            // Find_Commune
            // 
            this.Find_Commune.Location = new System.Drawing.Point(364, 118);
            this.Find_Commune.Name = "Find_Commune";
            this.Find_Commune.Size = new System.Drawing.Size(136, 20);
            this.Find_Commune.TabIndex = 17;
            this.Find_Commune.TextChanged += new System.EventHandler(this.Find_Commune_TextChanged);
            this.Find_Commune.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Find_Rue_KeyUp);
            // 
            // Find_Np
            // 
            this.Find_Np.Location = new System.Drawing.Point(364, 95);
            this.Find_Np.Name = "Find_Np";
            this.Find_Np.Size = new System.Drawing.Size(80, 20);
            this.Find_Np.TabIndex = 13;
            this.Find_Np.TextChanged += new System.EventHandler(this.Find_Np_TextChanged);
            this.Find_Np.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Find_Rue_KeyUp);
            // 
            // Find_Rue
            // 
            this.Find_Rue.Location = new System.Drawing.Point(364, 71);
            this.Find_Rue.Name = "Find_Rue";
            this.Find_Rue.Size = new System.Drawing.Size(254, 20);
            this.Find_Rue.TabIndex = 11;
            this.Find_Rue.TextChanged += new System.EventHandler(this.Find_Rue_TextChanged);
            this.Find_Rue.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Find_Rue_KeyUp);
            // 
            // Find_DtNaissance
            // 
            this.Find_DtNaissance.Location = new System.Drawing.Point(135, 92);
            this.Find_DtNaissance.Name = "Find_DtNaissance";
            this.Find_DtNaissance.Size = new System.Drawing.Size(99, 20);
            this.Find_DtNaissance.TabIndex = 7;
            this.Find_DtNaissance.TextChanged += new System.EventHandler(this.Find_DtNaissance_TextChanged);
            this.Find_DtNaissance.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Find_Rue_KeyUp);
            // 
            // Find_Tel
            // 
            this.Find_Tel.Location = new System.Drawing.Point(94, 68);
            this.Find_Tel.Name = "Find_Tel";
            this.Find_Tel.Size = new System.Drawing.Size(140, 20);
            this.Find_Tel.TabIndex = 5;
            this.Find_Tel.TextChanged += new System.EventHandler(this.Find_Tel_TextChanged);
            this.Find_Tel.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Find_Rue_KeyUp);
            // 
            // Find_Prenom
            // 
            this.Find_Prenom.Location = new System.Drawing.Point(135, 45);
            this.Find_Prenom.Name = "Find_Prenom";
            this.Find_Prenom.Size = new System.Drawing.Size(99, 20);
            this.Find_Prenom.TabIndex = 3;
            this.Find_Prenom.TextChanged += new System.EventHandler(this.Find_Prenom_TextChanged);
            this.Find_Prenom.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Find_Rue_KeyUp);
            // 
            // Find_Nom
            // 
            this.Find_Nom.Location = new System.Drawing.Point(135, 22);
            this.Find_Nom.Name = "Find_Nom";
            this.Find_Nom.Size = new System.Drawing.Size(99, 20);
            this.Find_Nom.TabIndex = 1;
            this.Find_Nom.TextChanged += new System.EventHandler(this.Find_Nom_TextChanged);
            this.Find_Nom.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Find_Rue_KeyUp);
            // 
            // rdAdrInter
            // 
            this.rdAdrInter.Location = new System.Drawing.Point(256, 48);
            this.rdAdrInter.Name = "rdAdrInter";
            this.rdAdrInter.Size = new System.Drawing.Size(144, 16);
            this.rdAdrInter.TabIndex = 9;
            this.rdAdrInter.Text = "Adresse d\'intervention";
            // 
            // rdAdrAdm
            // 
            this.rdAdrAdm.Checked = true;
            this.rdAdrAdm.Location = new System.Drawing.Point(256, 24);
            this.rdAdrAdm.Name = "rdAdrAdm";
            this.rdAdrAdm.Size = new System.Drawing.Size(144, 16);
            this.rdAdrAdm.TabIndex = 8;
            this.rdAdrAdm.TabStop = true;
            this.rdAdrAdm.Text = "Adresse administrative";
            // 
            // chkCommune
            // 
            this.chkCommune.Location = new System.Drawing.Point(256, 120);
            this.chkCommune.Name = "chkCommune";
            this.chkCommune.Size = new System.Drawing.Size(89, 18);
            this.chkCommune.TabIndex = 16;
            this.chkCommune.TabStop = false;
            this.chkCommune.Text = "Commune";
            // 
            // chkNp
            // 
            this.chkNp.Location = new System.Drawing.Point(256, 96);
            this.chkNp.Name = "chkNp";
            this.chkNp.Size = new System.Drawing.Size(98, 16);
            this.chkNp.TabIndex = 12;
            this.chkNp.TabStop = false;
            this.chkNp.Text = "Numéro postal";
            // 
            // chkRue
            // 
            this.chkRue.Location = new System.Drawing.Point(256, 72);
            this.chkRue.Name = "chkRue";
            this.chkRue.Size = new System.Drawing.Size(120, 16);
            this.chkRue.TabIndex = 10;
            this.chkRue.TabStop = false;
            this.chkRue.Text = "Rue";
            // 
            // chkDtNaissance
            // 
            this.chkDtNaissance.Location = new System.Drawing.Point(16, 96);
            this.chkDtNaissance.Name = "chkDtNaissance";
            this.chkDtNaissance.Size = new System.Drawing.Size(120, 16);
            this.chkDtNaissance.TabIndex = 6;
            this.chkDtNaissance.TabStop = false;
            this.chkDtNaissance.Text = "Date de naissance";
            // 
            // chkTelephone
            // 
            this.chkTelephone.Location = new System.Drawing.Point(16, 72);
            this.chkTelephone.Name = "chkTelephone";
            this.chkTelephone.Size = new System.Drawing.Size(120, 16);
            this.chkTelephone.TabIndex = 4;
            this.chkTelephone.TabStop = false;
            this.chkTelephone.Text = "Téléphone";
            // 
            // chkPrenom
            // 
            this.chkPrenom.Location = new System.Drawing.Point(16, 48);
            this.chkPrenom.Name = "chkPrenom";
            this.chkPrenom.Size = new System.Drawing.Size(120, 16);
            this.chkPrenom.TabIndex = 2;
            this.chkPrenom.TabStop = false;
            this.chkPrenom.Text = "Prénom";
            // 
            // chkNom
            // 
            this.chkNom.Location = new System.Drawing.Point(16, 24);
            this.chkNom.Name = "chkNom";
            this.chkNom.Size = new System.Drawing.Size(120, 16);
            this.chkNom.TabIndex = 0;
            this.chkNom.TabStop = false;
            this.chkNom.Text = "Nom";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.LbNbResultat);
            this.groupBox2.Controls.Add(this.fpListe);
            this.groupBox2.Location = new System.Drawing.Point(8, 192);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(624, 626);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Résultats";
            // 
            // LbNbResultat
            // 
            this.LbNbResultat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LbNbResultat.Location = new System.Drawing.Point(6, 549);
            this.LbNbResultat.Name = "LbNbResultat";
            this.LbNbResultat.Size = new System.Drawing.Size(384, 24);
            this.LbNbResultat.TabIndex = 8;
            // 
            // fpListe
            // 
            this.fpListe.AccessibleDescription = "";
            this.fpListe.AllowDragDrop = true;
            this.fpListe.AllowDrop = true;
            this.fpListe.BackColor = System.Drawing.Color.Silver;
            this.fpListe.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.fpListe.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpListe.Location = new System.Drawing.Point(6, 19);
            this.fpListe.Name = "fpListe";
            this.fpListe.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpListe_Sheet1});
            this.fpListe.Size = new System.Drawing.Size(612, 601);
            this.fpListe.TabIndex = 7;
            this.fpListe.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpListe.DragDrop += new System.Windows.Forms.DragEventHandler(this.fpListe_DragDrop);
            this.fpListe.DragEnter += new System.Windows.Forms.DragEventHandler(this.fpListe_DragEnter);
            this.fpListe.MouseDown += new System.Windows.Forms.MouseEventHandler(this.fpListe_MouseDown);
            this.fpListe.MouseUp += new System.Windows.Forms.MouseEventHandler(this.fpListe_MouseUp);
            // 
            // fpListe_Sheet1
            // 
            this.fpListe_Sheet1.Reset();
            this.fpListe_Sheet1.SheetName = "Sheet1";
            // 
            // tabFip
            // 
            this.tabFip.Controls.Add(this.tbFiche);
            this.tabFip.Controls.Add(this.tbRem);
            this.tabFip.Controls.Add(this.tbMed);
            this.tabFip.Controls.Add(this.tbFactu);
            this.tabFip.Location = new System.Drawing.Point(638, 40);
            this.tabFip.Name = "tabFip";
            this.tabFip.SelectedIndex = 0;
            this.tabFip.Size = new System.Drawing.Size(526, 709);
            this.tabFip.TabIndex = 4;
            // 
            // tbFiche
            // 
            this.tbFiche.Controls.Add(this.panel1);
            this.tbFiche.Location = new System.Drawing.Point(4, 22);
            this.tbFiche.Name = "tbFiche";
            this.tbFiche.Size = new System.Drawing.Size(518, 683);
            this.tbFiche.TabIndex = 0;
            this.tbFiche.Text = "Fiche";
            this.tbFiche.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.CadetBlue;
            this.panel1.Controls.Add(this.label33);
            this.panel1.Controls.Add(this.tBNumCarte);
            this.panel1.Controls.Add(this.btInfoPatient);
            this.panel1.Controls.Add(this.TxtMail);
            this.panel1.Controls.Add(this.label32);
            this.panel1.Controls.Add(this.label31);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.tBoxNumAVS);
            this.panel1.Controls.Add(this.tBoxNunAssure);
            this.panel1.Controls.Add(this.bautre);
            this.panel1.Controls.Add(this.BAnalyses);
            this.panel1.Controls.Add(this.BECG);
            this.panel1.Controls.Add(this.EMaskTel1);
            this.panel1.Controls.Add(this.txtDtDeces);
            this.panel1.Controls.Add(this.label25);
            this.panel1.Controls.Add(this.CbSexe);
            this.panel1.Controls.Add(this.TxtDtNaissance);
            this.panel1.Controls.Add(this.TxtPrenom);
            this.panel1.Controls.Add(this.TxtNom);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.LblIndice);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(518, 683);
            this.panel1.TabIndex = 4;
            // 
            // label33
            // 
            this.label33.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label33.Location = new System.Drawing.Point(3, 144);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(66, 18);
            this.label33.TabIndex = 87;
            this.label33.Text = "N° de carte:";
            this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tBNumCarte
            // 
            this.tBNumCarte.BackColor = System.Drawing.Color.GhostWhite;
            this.tBNumCarte.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tBNumCarte.Location = new System.Drawing.Point(69, 142);
            this.tBNumCarte.Name = "tBNumCarte";
            this.tBNumCarte.Size = new System.Drawing.Size(187, 20);
            this.tBNumCarte.TabIndex = 25;
            // 
            // btInfoPatient
            // 
            this.btInfoPatient.BackgroundImage = global::ImportSosGeneve.Properties.Resources.bVoirBleu;
            this.btInfoPatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btInfoPatient.FlatAppearance.BorderSize = 0;
            this.btInfoPatient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btInfoPatient.Location = new System.Drawing.Point(261, 135);
            this.btInfoPatient.Name = "btInfoPatient";
            this.btInfoPatient.Size = new System.Drawing.Size(36, 30);
            this.btInfoPatient.TabIndex = 26;
            this.toolTip1.SetToolTip(this.btInfoPatient, "Informations de ce patient");
            this.btInfoPatient.UseVisualStyleBackColor = true;
            this.btInfoPatient.Click += new System.EventHandler(this.btInfoPatient_Click);
            // 
            // TxtMail
            // 
            this.TxtMail.BackColor = System.Drawing.Color.GhostWhite;
            this.TxtMail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtMail.Location = new System.Drawing.Point(67, 106);
            this.TxtMail.Name = "TxtMail";
            this.TxtMail.Size = new System.Drawing.Size(331, 20);
            this.TxtMail.TabIndex = 24;
            // 
            // label32
            // 
            this.label32.Location = new System.Drawing.Point(10, 108);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(54, 20);
            this.label32.TabIndex = 83;
            this.label32.Text = "Email :";
            // 
            // label31
            // 
            this.label31.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label31.Location = new System.Drawing.Point(6, 175);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(58, 18);
            this.label31.TabIndex = 82;
            this.label31.Text = "N° d\'AVS:";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label17
            // 
            this.label17.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label17.Location = new System.Drawing.Point(254, 175);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(66, 18);
            this.label17.TabIndex = 81;
            this.label17.Text = "N° d\'assuré:";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tBoxNumAVS
            // 
            this.tBoxNumAVS.BackColor = System.Drawing.Color.GhostWhite;
            this.tBoxNumAVS.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tBoxNumAVS.Location = new System.Drawing.Point(68, 175);
            this.tBoxNumAVS.Name = "tBoxNumAVS";
            this.tBoxNumAVS.Size = new System.Drawing.Size(173, 20);
            this.tBoxNumAVS.TabIndex = 27;
            // 
            // tBoxNunAssure
            // 
            this.tBoxNunAssure.BackColor = System.Drawing.Color.GhostWhite;
            this.tBoxNunAssure.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tBoxNunAssure.Location = new System.Drawing.Point(326, 173);
            this.tBoxNunAssure.Name = "tBoxNunAssure";
            this.tBoxNunAssure.Size = new System.Drawing.Size(173, 20);
            this.tBoxNunAssure.TabIndex = 28;           
            // 
            // bautre
            // 
            this.bautre.BackgroundImage = global::ImportSosGeneve.Properties.Resources.bautre_on;
            this.bautre.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.bautre.Location = new System.Drawing.Point(226, 625);
            this.bautre.Name = "bautre";
            this.bautre.Size = new System.Drawing.Size(53, 44);
            this.bautre.TabIndex = 78;
            this.toolTip1.SetToolTip(this.bautre, "Autre");
            this.bautre.UseVisualStyleBackColor = true;
            this.bautre.Click += new System.EventHandler(this.bautre_Click);
            // 
            // BAnalyses
            // 
            this.BAnalyses.BackgroundImage = global::ImportSosGeneve.Properties.Resources.bchimie_on;
            this.BAnalyses.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BAnalyses.Location = new System.Drawing.Point(167, 625);
            this.BAnalyses.Name = "BAnalyses";
            this.BAnalyses.Size = new System.Drawing.Size(53, 44);
            this.BAnalyses.TabIndex = 77;
            this.toolTip1.SetToolTip(this.BAnalyses, "Analyses Lab");
            this.BAnalyses.UseVisualStyleBackColor = true;
            this.BAnalyses.Click += new System.EventHandler(this.BAnalyses_Click);
            // 
            // BECG
            // 
            this.BECG.BackgroundImage = global::ImportSosGeneve.Properties.Resources.becg_on;
            this.BECG.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BECG.Location = new System.Drawing.Point(108, 625);
            this.BECG.Name = "BECG";
            this.BECG.Size = new System.Drawing.Size(53, 44);
            this.BECG.TabIndex = 76;
            this.toolTip1.SetToolTip(this.BECG, "ECG");
            this.BECG.UseVisualStyleBackColor = true;
            this.BECG.Click += new System.EventHandler(this.BECG_Click);
            // 
            // EMaskTel1
            // 
            this.EMaskTel1.BackColor = System.Drawing.Color.GhostWhite;
            this.EMaskTel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EMaskTel1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.EMaskTel1.Location = new System.Drawing.Point(75, 54);
            this.EMaskTel1.Mask = "################";
            this.EMaskTel1.Name = "EMaskTel1";
            this.EMaskTel1.Size = new System.Drawing.Size(130, 20);
            this.EMaskTel1.TabIndex = 21;
            // 
            // txtDtDeces
            // 
            this.txtDtDeces.BackColor = System.Drawing.Color.White;
            this.txtDtDeces.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDtDeces.Location = new System.Drawing.Point(300, 80);
            this.txtDtDeces.Name = "txtDtDeces";
            this.txtDtDeces.Size = new System.Drawing.Size(86, 20);
            this.txtDtDeces.TabIndex = 23;
            // 
            // label25
            // 
            this.label25.Location = new System.Drawing.Point(211, 83);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(83, 15);
            this.label25.TabIndex = 20;
            this.label25.Text = "Date de décès :";
            // 
            // CbSexe
            // 
            this.CbSexe.BackColor = System.Drawing.Color.GhostWhite;
            this.CbSexe.Items.AddRange(new object[] {
            "H",
            "F"});
            this.CbSexe.Location = new System.Drawing.Point(427, 30);
            this.CbSexe.Name = "CbSexe";
            this.CbSexe.Size = new System.Drawing.Size(57, 21);
            this.CbSexe.TabIndex = 20;
            // 
            // TxtDtNaissance
            // 
            this.TxtDtNaissance.BackColor = System.Drawing.Color.GhostWhite;
            this.TxtDtNaissance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtDtNaissance.Location = new System.Drawing.Point(108, 80);
            this.TxtDtNaissance.Name = "TxtDtNaissance";
            this.TxtDtNaissance.Size = new System.Drawing.Size(86, 20);
            this.TxtDtNaissance.TabIndex = 22;
            // 
            // TxtPrenom
            // 
            this.TxtPrenom.BackColor = System.Drawing.Color.GhostWhite;
            this.TxtPrenom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPrenom.Location = new System.Drawing.Point(273, 30);
            this.TxtPrenom.Name = "TxtPrenom";
            this.TxtPrenom.Size = new System.Drawing.Size(126, 20);
            this.TxtPrenom.TabIndex = 19;
            // 
            // TxtNom
            // 
            this.TxtNom.BackColor = System.Drawing.Color.GhostWhite;
            this.TxtNom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtNom.Location = new System.Drawing.Point(75, 30);
            this.TxtNom.Name = "TxtNom";
            this.TxtNom.Size = new System.Drawing.Size(130, 20);
            this.TxtNom.TabIndex = 18;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(433, 10);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(37, 18);
            this.label10.TabIndex = 12;
            this.label10.Text = "Sexe :";
            // 
            // LblIndice
            // 
            this.LblIndice.Location = new System.Drawing.Point(64, 8);
            this.LblIndice.Name = "LblIndice";
            this.LblIndice.Size = new System.Drawing.Size(120, 16);
            this.LblIndice.TabIndex = 11;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.TxtAdresse2);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label30);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.CBRoute1);
            this.groupBox3.Controls.Add(this.TxtChez);
            this.groupBox3.Controls.Add(this.label28);
            this.groupBox3.Controls.Add(this.txtPorte);
            this.groupBox3.Controls.Add(this.label21);
            this.groupBox3.Controls.Add(this.txtInterphone);
            this.groupBox3.Controls.Add(this.label22);
            this.groupBox3.Controls.Add(this.txtEtage);
            this.groupBox3.Controls.Add(this.label19);
            this.groupBox3.Controls.Add(this.txtDigicode);
            this.groupBox3.Controls.Add(this.label20);
            this.groupBox3.Controls.Add(this.txtEscalier);
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Controls.Add(this.txtDepartement);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.TxtCommune);
            this.groupBox3.Controls.Add(this.TxtNp);
            this.groupBox3.Controls.Add(this.TxtAdresse1);
            this.groupBox3.Controls.Add(this.TxtNum);
            this.groupBox3.Location = new System.Drawing.Point(7, 206);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(492, 245);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Adresse d\'intervention";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(39, 90);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(65, 13);
            this.label15.TabIndex = 105;
            this.label15.Text = "Complément";
            // 
            // TxtAdresse2
            // 
            this.TxtAdresse2.Location = new System.Drawing.Point(113, 87);
            this.TxtAdresse2.Name = "TxtAdresse2";
            this.TxtAdresse2.Size = new System.Drawing.Size(287, 20);
            this.TxtAdresse2.TabIndex = 5;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(110, 64);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(47, 14);
            this.label14.TabIndex = 103;
            this.label14.Text = "Nom rue :";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(19, 44);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(66, 13);
            this.label13.TabIndex = 102;
            this.label13.Text = "Route, rue...";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(404, 65);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(19, 13);
            this.label12.TabIndex = 101;
            this.label12.Text = "N°";
            // 
            // label30
            // 
            this.label30.Location = new System.Drawing.Point(170, 127);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(66, 13);
            this.label30.TabIndex = 100;
            this.label30.Text = "Localité :";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(31, 127);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(42, 13);
            this.label11.TabIndex = 98;
            this.label11.Text = "NPA :";
            // 
            // CBRoute1
            // 
            this.CBRoute1.BackColor = System.Drawing.SystemColors.Window;
            this.CBRoute1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CBRoute1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.CBRoute1.FormattingEnabled = true;
            this.CBRoute1.Items.AddRange(new object[] {
            "Route",
            "Rue",
            "Avenue",
            "Boulevard",
            "Place",
            "Passage",
            "Sentier",
            "Square",
            "Chemin",
            "Allée",
            "Cité",
            "Cours",
            "Impasse",
            "Quai"});
            this.CBRoute1.Location = new System.Drawing.Point(8, 60);
            this.CBRoute1.Name = "CBRoute1";
            this.CBRoute1.Size = new System.Drawing.Size(96, 21);
            this.CBRoute1.TabIndex = 2;
            // 
            // TxtChez
            // 
            this.TxtChez.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtChez.Location = new System.Drawing.Point(110, 19);
            this.TxtChez.Name = "TxtChez";
            this.TxtChez.Size = new System.Drawing.Size(190, 20);
            this.TxtChez.TabIndex = 1;
            // 
            // label28
            // 
            this.label28.Location = new System.Drawing.Point(60, 22);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(44, 15);
            this.label28.TabIndex = 22;
            this.label28.Text = "Chez :";
            // 
            // txtPorte
            // 
            this.txtPorte.Location = new System.Drawing.Point(367, 212);
            this.txtPorte.Name = "txtPorte";
            this.txtPorte.Size = new System.Drawing.Size(66, 20);
            this.txtPorte.TabIndex = 13;
            this.txtPorte.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPorte_KeyDown);
            this.txtPorte.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPorte_KeyPress);
            // 
            // label21
            // 
            this.label21.Location = new System.Drawing.Point(309, 214);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(46, 13);
            this.label21.TabIndex = 20;
            this.label21.Text = "Porte :";
            // 
            // txtInterphone
            // 
            this.txtInterphone.Location = new System.Drawing.Point(367, 190);
            this.txtInterphone.Name = "txtInterphone";
            this.txtInterphone.Size = new System.Drawing.Size(110, 20);
            this.txtInterphone.TabIndex = 12;
            this.txtInterphone.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInterphone_KeyDown);
            this.txtInterphone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtInterphone_KeyPress);
            // 
            // label22
            // 
            this.label22.Location = new System.Drawing.Point(274, 192);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(89, 14);
            this.label22.TabIndex = 18;
            this.label22.Text = "interphone nom:";
            // 
            // txtEtage
            // 
            this.txtEtage.Location = new System.Drawing.Point(186, 211);
            this.txtEtage.Name = "txtEtage";
            this.txtEtage.Size = new System.Drawing.Size(74, 20);
            this.txtEtage.TabIndex = 11;
            this.txtEtage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEtage_KeyDown);
            this.txtEtage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEtage_KeyPress);
            // 
            // label19
            // 
            this.label19.Location = new System.Drawing.Point(147, 215);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(51, 13);
            this.label19.TabIndex = 16;
            this.label19.Text = "Etage";
            // 
            // txtDigicode
            // 
            this.txtDigicode.Location = new System.Drawing.Point(186, 188);
            this.txtDigicode.Name = "txtDigicode";
            this.txtDigicode.Size = new System.Drawing.Size(74, 20);
            this.txtDigicode.TabIndex = 10;
            this.txtDigicode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDigicode_KeyDown);
            this.txtDigicode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDigicode_KeyPress);
            // 
            // label20
            // 
            this.label20.Location = new System.Drawing.Point(138, 193);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(49, 13);
            this.label20.TabIndex = 14;
            this.label20.Text = "Digicode";
            // 
            // txtEscalier
            // 
            this.txtEscalier.Location = new System.Drawing.Point(60, 192);
            this.txtEscalier.Name = "txtEscalier";
            this.txtEscalier.Size = new System.Drawing.Size(66, 20);
            this.txtEscalier.TabIndex = 9;
            this.txtEscalier.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEscalier_KeyDown);
            this.txtEscalier.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEscalier_KeyPress);
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(8, 195);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(76, 13);
            this.label18.TabIndex = 12;
            this.label18.Text = "Escalier";
            // 
            // txtDepartement
            // 
            this.txtDepartement.BackColor = System.Drawing.Color.White;
            this.txtDepartement.Location = new System.Drawing.Point(243, 150);
            this.txtDepartement.Name = "txtDepartement";
            this.txtDepartement.Size = new System.Drawing.Size(144, 20);
            this.txtDepartement.TabIndex = 8;
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(179, 154);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(54, 16);
            this.label16.TabIndex = 8;
            this.label16.Text = "Canton :";
            // 
            // TxtCommune
            // 
            this.TxtCommune.BackColor = System.Drawing.Color.GhostWhite;
            this.TxtCommune.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCommune.Location = new System.Drawing.Point(242, 120);
            this.TxtCommune.Name = "TxtCommune";
            this.TxtCommune.Size = new System.Drawing.Size(235, 20);
            this.TxtCommune.TabIndex = 7;
            // 
            // TxtNp
            // 
            this.TxtNp.BackColor = System.Drawing.Color.GhostWhite;
            this.TxtNp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtNp.Location = new System.Drawing.Point(79, 120);
            this.TxtNp.Name = "TxtNp";
            this.TxtNp.Size = new System.Drawing.Size(66, 20);
            this.TxtNp.TabIndex = 6;
            // 
            // TxtAdresse1
            // 
            this.TxtAdresse1.BackColor = System.Drawing.Color.GhostWhite;
            this.TxtAdresse1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtAdresse1.Location = new System.Drawing.Point(163, 61);
            this.TxtAdresse1.Name = "TxtAdresse1";
            this.TxtAdresse1.Size = new System.Drawing.Size(235, 20);
            this.TxtAdresse1.TabIndex = 3;
            // 
            // TxtNum
            // 
            this.TxtNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtNum.Location = new System.Drawing.Point(429, 61);
            this.TxtNum.Name = "TxtNum";
            this.TxtNum.Size = new System.Drawing.Size(34, 20);
            this.TxtNum.TabIndex = 4;
            this.TxtNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(4, 82);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 15);
            this.label6.TabIndex = 4;
            this.label6.Text = "Date de naissance :";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(8, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 24);
            this.label5.TabIndex = 3;
            this.label5.Text = "Téléphone :";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(211, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 24);
            this.label4.TabIndex = 2;
            this.label4.Text = "Prénom :";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(8, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 24);
            this.label3.TabIndex = 1;
            this.label3.Text = "Nom :";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 24);
            this.label2.TabIndex = 0;
            this.label2.Text = "Indice :";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label34);
            this.groupBox4.Controls.Add(this.txtAdmPays);
            this.groupBox4.Controls.Add(this.label66);
            this.groupBox4.Controls.Add(this.TxtRue_AdmAdresse2);
            this.groupBox4.Controls.Add(this.label64);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.label67);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.CBRoute_adm1);
            this.groupBox4.Controls.Add(this.txtBatiment_Adm);
            this.groupBox4.Controls.Add(this.label24);
            this.groupBox4.Controls.Add(this.TxtNum_Adm);
            this.groupBox4.Controls.Add(this.TxtCommune_Adm);
            this.groupBox4.Controls.Add(this.TxtNp_Adm);
            this.groupBox4.Controls.Add(this.TxtRue_AdmAdresse1);
            this.groupBox4.Location = new System.Drawing.Point(7, 456);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(492, 153);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Adresse de Facturation";
            // 
            // label34
            // 
            this.label34.Location = new System.Drawing.Point(204, 126);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(42, 13);
            this.label34.TabIndex = 101;
            this.label34.Text = "Pays :";
            // 
            // txtAdmPays
            // 
            this.txtAdmPays.Location = new System.Drawing.Point(250, 123);
            this.txtAdmPays.Name = "txtAdmPays";
            this.txtAdmPays.Size = new System.Drawing.Size(37, 20);
            this.txtAdmPays.TabIndex = 100;
            // 
            // label66
            // 
            this.label66.Location = new System.Drawing.Point(182, 97);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(51, 13);
            this.label66.TabIndex = 99;
            this.label66.Text = "Localité :";
            // 
            // TxtRue_AdmAdresse2
            // 
            this.TxtRue_AdmAdresse2.Location = new System.Drawing.Point(109, 59);
            this.TxtRue_AdmAdresse2.Name = "TxtRue_AdmAdresse2";
            this.TxtRue_AdmAdresse2.Size = new System.Drawing.Size(283, 20);
            this.TxtRue_AdmAdresse2.TabIndex = 4;
            // 
            // label64
            // 
            this.label64.Location = new System.Drawing.Point(38, 97);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(42, 13);
            this.label64.TabIndex = 97;
            this.label64.Text = "NPA :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(38, 62);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 13);
            this.label9.TabIndex = 96;
            this.label9.Text = "Complément";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(404, 36);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(19, 13);
            this.label8.TabIndex = 95;
            this.label8.Text = "N°";
            // 
            // label67
            // 
            this.label67.Location = new System.Drawing.Point(107, 35);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(54, 14);
            this.label67.TabIndex = 94;
            this.label67.Text = "Nom rue :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 13);
            this.label7.TabIndex = 93;
            this.label7.Text = "Route, rue...";
            // 
            // CBRoute_adm1
            // 
            this.CBRoute_adm1.BackColor = System.Drawing.SystemColors.Window;
            this.CBRoute_adm1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CBRoute_adm1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.CBRoute_adm1.FormattingEnabled = true;
            this.CBRoute_adm1.Items.AddRange(new object[] {
            "Route",
            "Rue",
            "Avenue",
            "Boulevard",
            "Place",
            "Passage",
            "Sentier",
            "Square",
            "Chemin",
            "Allée",
            "Cité",
            "Cours",
            "Impasse",
            "lotissement",
            "Quai"});
            this.CBRoute_adm1.Location = new System.Drawing.Point(4, 33);
            this.CBRoute_adm1.Name = "CBRoute_adm1";
            this.CBRoute_adm1.Size = new System.Drawing.Size(96, 21);
            this.CBRoute_adm1.TabIndex = 1;
            // 
            // txtBatiment_Adm
            // 
            this.txtBatiment_Adm.Location = new System.Drawing.Point(84, 123);
            this.txtBatiment_Adm.Name = "txtBatiment_Adm";
            this.txtBatiment_Adm.Size = new System.Drawing.Size(103, 20);
            this.txtBatiment_Adm.TabIndex = 7;
            // 
            // label24
            // 
            this.label24.Location = new System.Drawing.Point(18, 127);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(54, 16);
            this.label24.TabIndex = 24;
            this.label24.Text = "Bâtiment";
            // 
            // TxtNum_Adm
            // 
            this.TxtNum_Adm.Location = new System.Drawing.Point(429, 29);
            this.TxtNum_Adm.Name = "TxtNum_Adm";
            this.TxtNum_Adm.Size = new System.Drawing.Size(34, 20);
            this.TxtNum_Adm.TabIndex = 3;
            this.TxtNum_Adm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TxtCommune_Adm
            // 
            this.TxtCommune_Adm.Location = new System.Drawing.Point(251, 90);
            this.TxtCommune_Adm.Name = "TxtCommune_Adm";
            this.TxtCommune_Adm.Size = new System.Drawing.Size(227, 20);
            this.TxtCommune_Adm.TabIndex = 6;
            // 
            // TxtNp_Adm
            // 
            this.TxtNp_Adm.Location = new System.Drawing.Point(84, 94);
            this.TxtNp_Adm.Name = "TxtNp_Adm";
            this.TxtNp_Adm.Size = new System.Drawing.Size(70, 20);
            this.TxtNp_Adm.TabIndex = 5;
            // 
            // TxtRue_AdmAdresse1
            // 
            this.TxtRue_AdmAdresse1.Location = new System.Drawing.Point(167, 29);
            this.TxtRue_AdmAdresse1.Name = "TxtRue_AdmAdresse1";
            this.TxtRue_AdmAdresse1.Size = new System.Drawing.Size(225, 20);
            this.TxtRue_AdmAdresse1.TabIndex = 2;
            // 
            // tbRem
            // 
            this.tbRem.Controls.Add(this.panel3);
            this.tbRem.Location = new System.Drawing.Point(4, 22);
            this.tbRem.Name = "tbRem";
            this.tbRem.Size = new System.Drawing.Size(518, 683);
            this.tbRem.TabIndex = 1;
            this.tbRem.Text = "Remarques";
            this.tbRem.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.CadetBlue;
            this.panel3.Controls.Add(this.grpContentieux);
            this.panel3.Controls.Add(this.TxtRemMed);
            this.panel3.Controls.Add(this.LblRemarqueMedical);
            this.panel3.Controls.Add(this.TxtRemEco);
            this.panel3.Controls.Add(this.lblRemarqueEconomique);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(518, 683);
            this.panel3.TabIndex = 0;
            // 
            // grpContentieux
            // 
            this.grpContentieux.Controls.Add(this.chkCessionCreance);
            this.grpContentieux.Controls.Add(this.chkSurPlace);
            this.grpContentieux.Location = new System.Drawing.Point(8, 6);
            this.grpContentieux.Name = "grpContentieux";
            this.grpContentieux.Size = new System.Drawing.Size(368, 44);
            this.grpContentieux.TabIndex = 66;
            this.grpContentieux.TabStop = false;
            // 
            // chkCessionCreance
            // 
            this.chkCessionCreance.AutoSize = true;
            this.chkCessionCreance.Location = new System.Drawing.Point(179, 19);
            this.chkCessionCreance.Name = "chkCessionCreance";
            this.chkCessionCreance.Size = new System.Drawing.Size(176, 17);
            this.chkCessionCreance.TabIndex = 1;
            this.chkCessionCreance.Text = "Faire signer cession de créance";
            this.chkCessionCreance.UseVisualStyleBackColor = true;
            // 
            // chkSurPlace
            // 
            this.chkSurPlace.AutoSize = true;
            this.chkSurPlace.Location = new System.Drawing.Point(10, 19);
            this.chkSurPlace.Name = "chkSurPlace";
            this.chkSurPlace.Size = new System.Drawing.Size(143, 17);
            this.chkSurPlace.TabIndex = 0;
            this.chkSurPlace.Text = "Faire encaisser sur place";
            this.chkSurPlace.UseVisualStyleBackColor = true;
            // 
            // TxtRemMed
            // 
            this.TxtRemMed.Location = new System.Drawing.Point(8, 286);
            this.TxtRemMed.Name = "TxtRemMed";
            this.TxtRemMed.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.TxtRemMed.Size = new System.Drawing.Size(368, 368);
            this.TxtRemMed.TabIndex = 4;
            this.TxtRemMed.Text = "";
            // 
            // LblRemarqueMedical
            // 
            this.LblRemarqueMedical.AutoSize = true;
            this.LblRemarqueMedical.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblRemarqueMedical.Location = new System.Drawing.Point(5, 261);
            this.LblRemarqueMedical.Name = "LblRemarqueMedical";
            this.LblRemarqueMedical.Size = new System.Drawing.Size(144, 13);
            this.LblRemarqueMedical.TabIndex = 3;
            this.LblRemarqueMedical.Text = "Remarque médicale :";
            // 
            // TxtRemEco
            // 
            this.TxtRemEco.Location = new System.Drawing.Point(8, 80);
            this.TxtRemEco.Name = "TxtRemEco";
            this.TxtRemEco.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.TxtRemEco.Size = new System.Drawing.Size(368, 138);
            this.TxtRemEco.TabIndex = 1;
            this.TxtRemEco.Text = "";
            // 
            // lblRemarqueEconomique
            // 
            this.lblRemarqueEconomique.AutoSize = true;
            this.lblRemarqueEconomique.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemarqueEconomique.Location = new System.Drawing.Point(8, 62);
            this.lblRemarqueEconomique.Name = "lblRemarqueEconomique";
            this.lblRemarqueEconomique.Size = new System.Drawing.Size(164, 13);
            this.lblRemarqueEconomique.TabIndex = 0;
            this.lblRemarqueEconomique.Text = "Remarque économique :";
            // 
            // tbMed
            // 
            this.tbMed.Controls.Add(this.pan_MedTT);
            this.tbMed.Location = new System.Drawing.Point(4, 22);
            this.tbMed.Name = "tbMed";
            this.tbMed.Size = new System.Drawing.Size(518, 683);
            this.tbMed.TabIndex = 2;
            this.tbMed.Text = "Médecins Traitant";
            this.tbMed.UseVisualStyleBackColor = true;
            // 
            // pan_MedTT
            // 
            this.pan_MedTT.BackColor = System.Drawing.Color.CadetBlue;
            this.pan_MedTT.Controls.Add(this.lstMedTT);
            this.pan_MedTT.Controls.Add(this.lnkSupMedTT);
            this.pan_MedTT.Controls.Add(this.lnkAjoutMedTT);
            this.pan_MedTT.Controls.Add(this.label23);
            this.pan_MedTT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pan_MedTT.Enabled = false;
            this.pan_MedTT.Location = new System.Drawing.Point(0, 0);
            this.pan_MedTT.Name = "pan_MedTT";
            this.pan_MedTT.Size = new System.Drawing.Size(518, 683);
            this.pan_MedTT.TabIndex = 0;
            // 
            // lstMedTT
            // 
            this.lstMedTT.Location = new System.Drawing.Point(8, 48);
            this.lstMedTT.Name = "lstMedTT";
            this.lstMedTT.Size = new System.Drawing.Size(368, 134);
            this.lstMedTT.TabIndex = 4;
            this.lstMedTT.DoubleClick += new System.EventHandler(this.lstMedTT_DoubleClick);
            // 
            // lnkSupMedTT
            // 
            this.lnkSupMedTT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lnkSupMedTT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lnkSupMedTT.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkSupMedTT.Location = new System.Drawing.Point(216, 200);
            this.lnkSupMedTT.Name = "lnkSupMedTT";
            this.lnkSupMedTT.Size = new System.Drawing.Size(112, 16);
            this.lnkSupMedTT.TabIndex = 3;
            this.lnkSupMedTT.TabStop = true;
            this.lnkSupMedTT.Text = "Supprimer";
            this.lnkSupMedTT.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lnkSupMedTT.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSupMedTT_LinkClicked);
            // 
            // lnkAjoutMedTT
            // 
            this.lnkAjoutMedTT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lnkAjoutMedTT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lnkAjoutMedTT.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkAjoutMedTT.Location = new System.Drawing.Point(40, 200);
            this.lnkAjoutMedTT.Name = "lnkAjoutMedTT";
            this.lnkAjoutMedTT.Size = new System.Drawing.Size(112, 16);
            this.lnkAjoutMedTT.TabIndex = 2;
            this.lnkAjoutMedTT.TabStop = true;
            this.lnkAjoutMedTT.Text = "Ajouter";
            this.lnkAjoutMedTT.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lnkAjoutMedTT.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkAjoutMedTT_LinkClicked);
            // 
            // label23
            // 
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(8, 16);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(272, 40);
            this.label23.TabIndex = 0;
            this.label23.Text = "Liste des médecins traitants associés : ";
            // 
            // tbFactu
            // 
            this.tbFactu.BackColor = System.Drawing.Color.CadetBlue;
            this.tbFactu.Controls.Add(this.label29);
            this.tbFactu.Controls.Add(this.lbDestinataire10);
            this.tbFactu.Controls.Add(this.btFacture10);
            this.tbFactu.Controls.Add(this.label27);
            this.tbFactu.Controls.Add(this.lwDestinataireDate);
            this.tbFactu.Controls.Add(this.lnkChangerDestinataireFacture);
            this.tbFactu.Controls.Add(this.LblDestinataireFacture);
            this.tbFactu.Controls.Add(this.label26);
            this.tbFactu.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbFactu.Location = new System.Drawing.Point(4, 22);
            this.tbFactu.Name = "tbFactu";
            this.tbFactu.Size = new System.Drawing.Size(518, 683);
            this.tbFactu.TabIndex = 3;
            this.tbFactu.Text = "Facturation";
            // 
            // label29
            // 
            this.label29.Location = new System.Drawing.Point(8, 475);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(152, 24);
            this.label29.TabIndex = 7;
            this.label29.Text = "Destinataire Facture 10%";
            // 
            // lbDestinataire10
            // 
            this.lbDestinataire10.Location = new System.Drawing.Point(16, 528);
            this.lbDestinataire10.Name = "lbDestinataire10";
            this.lbDestinataire10.Size = new System.Drawing.Size(144, 72);
            this.lbDestinataire10.TabIndex = 6;
            // 
            // btFacture10
            // 
            this.btFacture10.Location = new System.Drawing.Point(8, 608);
            this.btFacture10.Name = "btFacture10";
            this.btFacture10.Size = new System.Drawing.Size(120, 32);
            this.btFacture10.TabIndex = 5;
            this.btFacture10.Text = "Ajouter Destinataire Facture 10%";
            this.btFacture10.Click += new System.EventHandler(this.btFacture10_Click);
            // 
            // label27
            // 
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(12, 285);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(360, 24);
            this.label27.TabIndex = 4;
            this.label27.Text = "Destinataires par date :";
            // 
            // lwDestinataireDate
            // 
            this.lwDestinataireDate.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lwDestinataireDate.HideSelection = false;
            this.lwDestinataireDate.Location = new System.Drawing.Point(11, 312);
            this.lwDestinataireDate.Name = "lwDestinataireDate";
            this.lwDestinataireDate.Size = new System.Drawing.Size(368, 160);
            this.lwDestinataireDate.TabIndex = 3;
            this.lwDestinataireDate.UseCompatibleStateImageBehavior = false;
            this.lwDestinataireDate.View = System.Windows.Forms.View.Details;
            this.lwDestinataireDate.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lwDestinataireDate_MouseUp);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Type";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Nom";
            this.columnHeader2.Width = 140;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Debut";
            this.columnHeader3.Width = 80;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Fin";
            this.columnHeader4.Width = 80;
            // 
            // lnkChangerDestinataireFacture
            // 
            this.lnkChangerDestinataireFacture.Location = new System.Drawing.Point(16, 136);
            this.lnkChangerDestinataireFacture.Name = "lnkChangerDestinataireFacture";
            this.lnkChangerDestinataireFacture.Size = new System.Drawing.Size(59, 20);
            this.lnkChangerDestinataireFacture.TabIndex = 2;
            this.lnkChangerDestinataireFacture.TabStop = true;
            this.lnkChangerDestinataireFacture.Text = "Changer";
            this.lnkChangerDestinataireFacture.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkChangerDestinataireFacture_LinkClicked);
            // 
            // LblDestinataireFacture
            // 
            this.LblDestinataireFacture.Location = new System.Drawing.Point(16, 56);
            this.LblDestinataireFacture.Name = "LblDestinataireFacture";
            this.LblDestinataireFacture.Size = new System.Drawing.Size(352, 72);
            this.LblDestinataireFacture.TabIndex = 1;
            // 
            // label26
            // 
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(8, 24);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(360, 24);
            this.label26.TabIndex = 0;
            this.label26.Text = "Destinataire par défaut de la facture :";
            // 
            // LblStatut
            // 
            this.LblStatut.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblStatut.ForeColor = System.Drawing.Color.Red;
            this.LblStatut.Location = new System.Drawing.Point(652, 798);
            this.LblStatut.Name = "LblStatut";
            this.LblStatut.Size = new System.Drawing.Size(268, 24);
            this.LblStatut.TabIndex = 68;
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 5000;
            this.toolTip1.InitialDelay = 350;
            this.toolTip1.ReshowDelay = 100;
            // 
            // btnFactures
            // 
            this.btnFactures.BackgroundImage = global::ImportSosGeneve.Properties.Resources.Argent;
            this.btnFactures.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnFactures.Location = new System.Drawing.Point(926, 756);
            this.btnFactures.Name = "btnFactures";
            this.btnFactures.Size = new System.Drawing.Size(53, 44);
            this.btnFactures.TabIndex = 76;
            this.toolTip1.SetToolTip(this.btnFactures, "Voir les Factures de ce patient");
            this.btnFactures.UseVisualStyleBackColor = true;
            this.btnFactures.Click += new System.EventHandler(this.btnFactures_Click);
            // 
            // btnRapports
            // 
            this.btnRapports.BackgroundImage = global::ImportSosGeneve.Properties.Resources.DocLoupe;
            this.btnRapports.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnRapports.Location = new System.Drawing.Point(867, 756);
            this.btnRapports.Name = "btnRapports";
            this.btnRapports.Size = new System.Drawing.Size(53, 44);
            this.btnRapports.TabIndex = 75;
            this.toolTip1.SetToolTip(this.btnRapports, "Voir les rapports de ce patient");
            this.btnRapports.UseVisualStyleBackColor = true;
            this.btnRapports.Click += new System.EventHandler(this.btnRapports_Click);
            // 
            // btnListeAppels
            // 
            this.btnListeAppels.BackgroundImage = global::ImportSosGeneve.Properties.Resources.Telephone;
            this.btnListeAppels.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnListeAppels.Location = new System.Drawing.Point(808, 756);
            this.btnListeAppels.Name = "btnListeAppels";
            this.btnListeAppels.Size = new System.Drawing.Size(53, 44);
            this.btnListeAppels.TabIndex = 74;
            this.toolTip1.SetToolTip(this.btnListeAppels, "Voir les appels de ce patient");
            this.btnListeAppels.UseVisualStyleBackColor = true;
            this.btnListeAppels.Click += new System.EventHandler(this.btnListeAppels_Click);
            // 
            // btnSavePatient
            // 
            this.btnSavePatient.BackgroundImage = global::ImportSosGeneve.Properties.Resources.Sauvegarde;
            this.btnSavePatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSavePatient.Location = new System.Drawing.Point(983, 756);
            this.btnSavePatient.Name = "btnSavePatient";
            this.btnSavePatient.Size = new System.Drawing.Size(53, 44);
            this.btnSavePatient.TabIndex = 73;
            this.toolTip1.SetToolTip(this.btnSavePatient, "Enregistrer la fiche");
            this.btnSavePatient.UseVisualStyleBackColor = true;
            this.btnSavePatient.Click += new System.EventHandler(this.btnSavePatient_Click);
            // 
            // bFermer
            // 
            this.bFermer.BackgroundImage = global::ImportSosGeneve.Properties.Resources.exit;
            this.bFermer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bFermer.FlatAppearance.BorderSize = 0;
            this.bFermer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bFermer.Location = new System.Drawing.Point(1114, 755);
            this.bFermer.Name = "bFermer";
            this.bFermer.Size = new System.Drawing.Size(46, 46);
            this.bFermer.TabIndex = 77;
            this.toolTip1.SetToolTip(this.bFermer, "Fermer");
            this.bFermer.UseVisualStyleBackColor = true;
            this.bFermer.Click += new System.EventHandler(this.bFermer_Click);
            // 
            // JointDoc
            // 
            this.JointDoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.JointDoc.Location = new System.Drawing.Point(649, 755);
            this.JointDoc.Name = "JointDoc";
            this.JointDoc.Size = new System.Drawing.Size(145, 40);
            this.JointDoc.TabIndex = 72;
            this.JointDoc.Text = "Documents";
            this.JointDoc.UseVisualStyleBackColor = false;
            this.JointDoc.Click += new System.EventHandler(this.button1_Click);
            // 
            // FIP
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.ClientSize = new System.Drawing.Size(1176, 830);
            this.Controls.Add(this.bFermer);
            this.Controls.Add(this.btnFactures);
            this.Controls.Add(this.btnRapports);
            this.Controls.Add(this.btnListeAppels);
            this.Controls.Add(this.btnSavePatient);
            this.Controls.Add(this.JointDoc);
            this.Controls.Add(this.tabFip);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LblStatut);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FIP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fiche Patient";
            this.Closed += new System.EventHandler(this.FIP_Closed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FIP_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FIP_FormClosed);
            this.Load += new System.EventHandler(this.FIP_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpListe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpListe_Sheet1)).EndInit();
            this.tabFip.ResumeLayout(false);
            this.tbFiche.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tbRem.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.grpContentieux.ResumeLayout(false);
            this.grpContentieux.PerformLayout();
            this.tbMed.ResumeLayout(false);
            this.pan_MedTT.ResumeLayout(false);
            this.tbFactu.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		#region Mise en place des controles

		private void InitializeListe()
		{
			fpListe_Sheet1.RowCount=0;
			fpListe_Sheet1.ColumnCount=10;

			fpListe_Sheet1.RowHeaderVisible = false;
			fpListe_Sheet1.ColumnHeaderVisible = true;

			fpListe_Sheet1.Columns[0].Width = 0;
			fpListe_Sheet1.Columns[1].Width = 120;
			fpListe_Sheet1.Columns[2].Width = 80;
			fpListe_Sheet1.Columns[3].Width = 80;
			fpListe_Sheet1.Columns[4].Width = 100;
			fpListe_Sheet1.Columns[5].Width = 60;
			fpListe_Sheet1.Columns[6].Width = 200;
			fpListe_Sheet1.Columns[7].Width = 150;
			fpListe_Sheet1.Columns[7].Width = 10;

			fpListe_Sheet1.ColumnHeader.Cells[0,0].Text = "Indice";
			fpListe_Sheet1.ColumnHeader.Cells[0,1].Text = "Nom";
			fpListe_Sheet1.ColumnHeader.Cells[0,2].Text = "Prenom";
			fpListe_Sheet1.ColumnHeader.Cells[0,3].Text = "Né le";
			fpListe_Sheet1.ColumnHeader.Cells[0,4].Text = "Telephone";
			fpListe_Sheet1.ColumnHeader.Cells[0,5].Text = "NP";
			fpListe_Sheet1.ColumnHeader.Cells[0,6].Text = "Rue";
			fpListe_Sheet1.ColumnHeader.Cells[0,7].Text = "Commune";
			fpListe_Sheet1.ColumnHeader.Cells[0,9].Text = "Date Décès";

			//couleur des lignes
            //Gradient1.BottomColor = Color.Orange;
			//Gradient1.TopColor = Color.Azure;
			//Gradient2.BottomColor = Color.Yellow;
			//Gradient2.TopColor = Color.Azure;

            //couleur des lignes
            Gradient1.TopColor = Color.White;
            Gradient1.BottomColor = Color.CadetBlue;
            Gradient2.TopColor = Color.White;
            Gradient2.BottomColor = Color.LightGoldenrodYellow;
            
		}

		private void Find_Nom_TextChanged(object sender, System.EventArgs e)
		{
			chkNom.Checked = (Find_Nom.Text!="");
		}

		private void Find_Prenom_TextChanged(object sender, System.EventArgs e)
		{
			chkPrenom.Checked = (Find_Prenom.Text!="");
		}

		private void Find_Tel_TextChanged(object sender, System.EventArgs e)
		{
			chkTelephone.Checked = (Find_Tel.Text!="");
		}

		private void Find_DtNaissance_TextChanged(object sender, System.EventArgs e)
		{
			chkDtNaissance.Checked = (Find_DtNaissance.Text!="");
		}

		private void Find_Rue_TextChanged(object sender, System.EventArgs e)
		{
			chkRue.Checked = (Find_Rue.Text!="");
		}

		private void Find_Np_TextChanged(object sender, System.EventArgs e)
		{
			chkNp.Checked = (Find_Np.Text!="");
		}

		private void Find_Commune_TextChanged(object sender, System.EventArgs e)
		{
			chkCommune.Checked = (Find_Commune.Text!="");
		}

		#endregion

		#region Recherche de patient

		private void btnRecherche_Click(object sender, System.EventArgs e)
		{
			// Fabrication de la requete de filtre : 
            string strRequete = " WHERE pa.IdPersonne = pe.IdPersonne and ";

			string strClause1 = " 1=1 ";
			string strClause2 = " 1=1 ";
			string strClause3 = " 1=1 ";
			string strClause4 = " 1=1 ";
			string strClause5 = " 1=1 ";
			string strClause6 = " 1=1 ";
			string strClause7 = " 1=1 ";
			string strClause8 = " 1=1 ";
			//string strClause9 = " 1=1 ";

			bool ReqPossible = false;

			if(chkNom.Checked)
			{
				strClause1 = " Nom Like '%" + Find_Nom.Text.Replace("'","''") + "%' ";
				ReqPossible = true;
			}
			if(chkPrenom.Checked)
			{
				strClause2 = " Prenom Like '%" + Find_Prenom.Text.Replace("'","''") + "%' ";
				ReqPossible = true;
			}
			if(chkTelephone.Checked)
			{
				strClause3 = " replace(Tel,' ','') Like '%" + Find_Tel.Text.Replace(" ","") + "%' ";
				ReqPossible = true;
			}
			if(chkDtNaissance.Checked)
			{
				DateTime dt = DateTime.Parse(Find_DtNaissance.Text);
                strClause4 = " DateNaissance = " + SosMedecins.Connexion.FormatSql.Format_Date(dt.ToString("yyyy-MM-dd"));
                
                ReqPossible = true;
			}

			if(rdAdrInter.Checked)
			{
				if(chkRue.Checked)
				{
					strClause5 = " Rue Like '%" + Find_Rue.Text.Replace("'","''") + "%' ";
					ReqPossible = true;
				}
				if(chkNp.Checked)
				{
					strClause6 = " CodePostal Like '%" + Find_Np.Text.Replace("'","''") + "%' ";
					ReqPossible = true;
				}
				if(chkCommune.Checked)
				{
					strClause7 = " Commune Like '%" + Find_Commune.Text.Replace("'","''") + "%' ";
					ReqPossible = true;
				}
				//NumAdresse
				if(chkNum.Checked)
				{
					strClause8 = " NumeroDansRue = '" + Find_Num.Text.Replace("'","''") + "' ";
					ReqPossible = true;
				}
			}
			else if(rdAdrAdm.Checked)
			{
				if(chkRue.Checked)
				{
					strClause5 = " Adm_Rue Like '%" + Find_Rue.Text.Replace("'","''") + "%' ";
					ReqPossible = true;
				}
				if(chkNp.Checked)
				{
					strClause6 = " Adm_CodePostal Like '%" + Find_Np.Text.Replace("'","''") + "%' ";
					ReqPossible = true;
				}
				if(chkCommune.Checked)
				{
					strClause7 = " Adm_Commune Like '%" + Find_Commune.Text.Replace("'","''") + "%' ";
					ReqPossible = true;
				}
				if(chkNum.Checked)
				{
					strClause8 = " Adm_NumeroDansRue = '" + Find_Num.Text.Replace("'","''") + "' ";
					ReqPossible = true;
				}
			}

			if(!ReqPossible)
			{
				MessageBox.Show("Il faut sélectionner au moins un critère.");
				return;
			}
			
			this.Cursor = Cursors.WaitCursor;
            strRequete += strClause1 + " AND " + strClause2 + " AND " + strClause3 + " AND " + strClause4 + " AND " + strClause5 + " AND " + strClause6 + " AND " + strClause7 + " AND " + strClause8;
			//string sql ="SELECT pe.*,pa.IdPatient,pa.SuiviPatient,pa.TypeDestinataireFacture,pa.IdDestinataireFacture from tablepersonne pe left join tablepatient pa on pa.IdPersonne = pe.IdPersonne " + strRequete;
			//DataSet ds = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet("SELECT pe.*,pa.IdPatient,pa.SuiviPatient,pa.TypeDestinataireFacture,pa.IdDestinataireFacture from tablepersonne pe left join tablepatient pa on pa.IdPersonne = pe.IdPersonne " + strRequete +" ORDER BY pe.Nom ASC, pe.Prenom ASC");

            string sql = "SELECT pe.*,pa.IdPatient,pa.SuiviPatient,pa.TypeDestinataireFacture,pa.IdDestinataireFacture";
            sql += "from tablepersonne pe, tablepatient pa " + strRequete;
            DataSet ds = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet("SELECT pe.*,pa.IdPatient,pa.SuiviPatient,pa.TypeDestinataireFacture,pa.IdDestinataireFacture from tablepersonne pe, tablepatient pa " + strRequete + " ORDER BY pe.Nom ASC, pe.Prenom ASC");


			// Traitement du dataset pour y insérer les patients doublons et les grouper par doublons
			TraitementDoublons(ds);
			ListePatient(ds);
			TriDoublon();			

			this.Cursor = Cursors.Default;
		}

		private void ListePatient(DataSet ds)
		{
			fpListe.Sheets[0].RowCount=0;
			fpListe.Sheets[0].Columns[-1].AllowAutoSort = true;
			fpListe.Tag = ds;

			if(ds!=null)
			{
                //if(ds.Tables[0].Columns.IndexOf("RemEco")==-1)
                //    ds.Tables[0].Columns.Add(new DataColumn("RemEco",typeof(string)));
                //if(ds.Tables[0].Columns.IndexOf("RemMed")==-1)
                //    ds.Tables[0].Columns.Add(new DataColumn("RemMed",typeof(string)));
				
				for(int i=0;i<ds.Tables[0].Rows.Count;i++)
				{
					int nb = fpListe_Sheet1.RowCount++;
					Color dece = Color.FromArgb(137,154,219);	
					string dec = ds.Tables[0].Rows[i]["DateDeces"].ToString();
					if(nb%2==0 && ds.Tables[0].Rows[i]["DateDeces"].ToString()=="")
						fpListe_Sheet1.Rows[nb].CellType = Gradient1;
					else if (nb%2!=0 && ds.Tables[0].Rows[i]["DateDeces"].ToString()=="")
						fpListe_Sheet1.Rows[nb].CellType = Gradient2;
					else
						fpListe_Sheet1.Rows[nb].BackColor = dece;
					fpListe_Sheet1.Cells[nb,0].Text = ds.Tables[0].Rows[i]["IdPersonne"].ToString();
					fpListe_Sheet1.Cells[nb,1].Text = ds.Tables[0].Rows[i]["Nom"].ToString();
					fpListe_Sheet1.Cells[nb,2].Text = ds.Tables[0].Rows[i]["Prenom"].ToString();
					if(ds.Tables[0].Rows[i]["DateNaissance"].ToString()!="")
						fpListe_Sheet1.Cells[nb,3].Text = DateTime.Parse(ds.Tables[0].Rows[i]["DateNaissance"].ToString()).ToString("dd/MM/yyyy");
					fpListe_Sheet1.Cells[nb,4].Text = ds.Tables[0].Rows[i]["Tel"].ToString();
					fpListe_Sheet1.Cells[nb,5].Text = ds.Tables[0].Rows[i]["Adm_CodePostal"].ToString();
					fpListe_Sheet1.Cells[nb,6].Text = ds.Tables[0].Rows[i]["Adm_Rue"].ToString();
					fpListe_Sheet1.Cells[nb,7].Text = ds.Tables[0].Rows[i]["Adm_Commune"].ToString();
					fpListe_Sheet1.Cells[nb,8].Text = ds.Tables[0].Rows[i]["Doublon"].ToString();
					fpListe_Sheet1.Cells[nb,9].Text = ds.Tables[0].Rows[i]["DateDeces"].ToString();
					fpListe_Sheet1.Rows[nb].Tag = ds.Tables[0].Rows[i];
				}

				LbNbResultat.Text = ds.Tables[0].Rows.Count + " enregistrement(s) trouvé(s)";
			}
		}

		// Traitement des patients qui sont en doublons
		private void TraitementDoublons(DataSet ds)
		{
			
            if(ds!=null)
			{
                int intdoublon = -1;
				if(ds.Tables[0].Columns.IndexOf("Doublon")==-1)
					ds.Tables[0].Columns.Add(new DataColumn("Doublon",typeof(int)));
				// Pour chaque ligne on regarde s'il n'y a aucun doublons
				// qui n'appartiendrait pas déjà au dataset
				for(int i=0;i < ds.Tables[0].Rows.Count;i++)
				{	
					int m_intDoublon = -1;

					if(ds.Tables[0].Rows[i]["Doublon"].ToString()!="" && ds.Tables[0].Rows[i]["Doublon"].ToString()!="-1")
						m_intDoublon = int.Parse(ds.Tables[0].Rows[i]["Doublon"].ToString());
					else
					{
						intdoublon++;
						m_intDoublon = intdoublon;
					}

                    //MessageBox.Show(i.ToString() + " et " +ds.Tables[0].Rows[i]["IdPatient"].ToString());

                    long IdPatient = long.Parse(ds.Tables[0].Rows[i]["IdPatient"].ToString());
					string[][] strdoublons = OutilsExt.OutilsSql.ExecuteCommandeAvecTabString("SELECT IdPatient_Enfant,IdPatient_Parent from tablerapprochementpatient Where IdPatient_Enfant = " + IdPatient + " Or IdPatient_Parent = " + IdPatient);
					if(strdoublons!=null)
					{
						// pour chaque ligne considérée comme doublons, on recherche l'indice à retrouver
						foreach(string[] s in strdoublons)
						{
							long IdPatientDoublon = -1;
							if(s[0]!=IdPatient.ToString())
								IdPatientDoublon = long.Parse(s[0]);
							else if(s[1]!=IdPatient.ToString())
								IdPatientDoublon = long.Parse(s[1]);							

							// on regarde d'abord s'il existe deja dans le dataset
							int PatientRetrouve = RetrouvePatient(ds,IdPatientDoublon);
							if(PatientRetrouve==-1)
							{
								// il n'existe pas, on le rappatrie
								DataSet dsDoublons = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet("SELECT pe.*,pa.IdPatient,pa.SuiviPatient,pa.TypeDestinataireFacture,pa.IdDestinataireFacture from tablepersonne pe left join tablepatient pa on pa.IdPersonne = pe.IdPersonne Where IdPatient = " + IdPatientDoublon);
								if(dsDoublons!=null && dsDoublons.Tables.Count>0 && dsDoublons.Tables[0].Rows.Count>0)
								{
									// on ajoute le patient dans le listing :
									DataRow newRow= ds.Tables[0].NewRow();
									newRow.ItemArray = dsDoublons.Tables[0].Rows[0].ItemArray;
									newRow["Doublon"] = m_intDoublon;									
									ds.Tables[0].Rows.Add(newRow);
								}
							}	
							else
							{
								ds.Tables[0].Rows[PatientRetrouve]["Doublon"] = m_intDoublon;
							}
						}						
					}
					
					ds.Tables[0].Rows[i]["Doublon"] = m_intDoublon;
				}
			}
		}

		private int RetrouvePatient(DataSet ds,long IdPatient)
		{
			for(int i=0;i<ds.Tables[0].Rows.Count;i++)
			{
				if(ds.Tables[0].Rows[i]["IdPatient"].ToString()==IdPatient.ToString())
					return i;
			}

			return -1;
		}

		private void TriDoublon()
		{
			fpListe_Sheet1.SortRows(8,true,true);

			for(int i=0;i<fpListe_Sheet1.RowCount-1;i++)
			{
				// Si il y a un doublon on l'encadre
				if(fpListe_Sheet1.Cells[i,8].Text==fpListe_Sheet1.Cells[i+1,8].Text)
				{	
					Random rand = new Random(int.Parse(fpListe_Sheet1.Cells[i,8].Text));
					int Arg1 = rand.Next(50,255);
					int Arg2 = rand.Next(50,255);
					int Arg3 = rand.Next(50,250);
					Color color= Color.FromArgb(Arg1,Arg2,Arg3);	
					Color dece = Color.FromArgb(0,0,255);	

					fpListe_Sheet1.Rows[i].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
					string dec = fpListe_Sheet1.Cells[i,9].Text.ToString();
					if (fpListe_Sheet1.Cells[i,9].Text.ToString() =="")
						fpListe_Sheet1.Rows[i].BackColor = color;
					else
						fpListe_Sheet1.Rows[i].BackColor = dece;

					for(int j=i+1;j<fpListe_Sheet1.RowCount;j++)
					{
						if(fpListe_Sheet1.Cells[i,8].Text==fpListe_Sheet1.Cells[j,8].Text)
						{
							fpListe_Sheet1.Rows[j].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
							if (fpListe_Sheet1.Cells[j,9].Text =="")
								fpListe_Sheet1.Rows[j].BackColor = color;
							else
								fpListe_Sheet1.Rows[j].BackColor = dece;

						}
						else
							break;
					}
				}
			}
		}

		#endregion		

		#region Affichage d'une fiche patient

		private void AffichagePatient(DataRow rowPatient)
		{
			this.Cursor = Cursors.WaitCursor;
			tabFip.Tag = rowPatient;
			LblStatut.Text  = "";

            AfficheFip(rowPatient);
            if (rowPatient != null)
            {
                AfficheRem(rowPatient);
                AfficheMedTT(rowPatient);
            }


			this.Cursor  = Cursors.Default;
		}		

		//On vide les controles si pas de patient sélectionnés
        private void AfficheFip(DataRow rowPatient)
		{
            //Dans tout les cas, on désactive les boutons ECG, Analyses et Autre
            BECG.Enabled = false;
            BECG.BackgroundImage = ImportSosGeneve.Properties.Resources.becg_off;

            BAnalyses.Enabled = false;
            BAnalyses.BackgroundImage = ImportSosGeneve.Properties.Resources.bchimie_off;

            bautre.Enabled = false;
            bautre.BackgroundImage = ImportSosGeneve.Properties.Resources.bautre_off;

            if(rowPatient==null)
			{
				this.Text = "Fiche patient";
				panel1.BackColor = Color.CadetBlue;
				LblIndice.Text  = "";
                TxtPrenom.Text = "";
				TxtNom.Text = "";				
                EMaskTel1.Text = "";
                TxtDtNaissance.Text = "";
				CbSexe.Text = "";

                tBNumCarte.Text = "";
                tBoxNunAssure.Text = "";
                tBoxNumAVS.Text = "";
				
				TxtAdresse1.Text = "";
                TxtNum.Text = "";
                TxtNp.Text = "";
                TxtCommune.Text = "";
				
				TxtRue_AdmAdresse1.Text ="";
                TxtNum_Adm.Text = "";
                TxtRue_AdmAdresse2.Text = "";
				TxtNp_Adm.Text = "";
				TxtCommune_Adm.Text ="";
				txtDepartement.Text = "";
                txtAdmPays.Text = "";
				TxtChez.Text = "";
				
				txtBatiment_Adm.Text = "";
				txtDigicode.Text = "";
				txtPorte.Text = "";
				txtInterphone.Text = "";
				txtEscalier.Text = "";
				txtEtage.Text = "";

				LblDestinataireFacture.Text = "";
				LblDestinataireFacture.Tag=null;

                txtDtDeces.Text = "";
                TxtMail.Text = "";
			}
			else
			{
				//sinon on affecte les controles
                this.Text = "Fiche patient : " +  rowPatient["IdPatient"].ToString();
				panel1.BackColor = Color.CadetBlue;
				                
                //TxtCommentaire.Text = rowPatient["TexteSup"].ToString();
				LblIndice.Text  = rowPatient["IdPatient"].ToString();
				TxtPrenom.Text = rowPatient["Prenom"].ToString();
				TxtNom.Text = rowPatient["Nom"].ToString();				
                EMaskTel1.Text = rowPatient["Tel"].ToString();

				if(rowPatient["DateNaissance"].ToString()!="")
					TxtDtNaissance.Text = DateTime.Parse( rowPatient["DateNaissance"].ToString()).ToString("dd/MM/yyyy");
				else
					TxtDtNaissance.Text = "";
				CbSexe.Text = rowPatient["Sexe"].ToString();

                TxtMail.Text = rowPatient["Email"].ToString();

                tBNumCarte.Text = rowPatient["Num_Carte"].ToString();
                tBoxNunAssure.Text = rowPatient["Num_Assure"].ToString();
                tBoxNumAVS.Text = rowPatient["Num_AVS"].ToString();

                //Si la rue en minuscule contient route, rue etc...
                //alors on affiche Route ou rue dans le CBRoute1
                if ((rowPatient["Rue"].ToString().ToLower().IndexOf("route")) > -1)
                {
                    CBRoute1.Text = "Route";
                }
                else if ((rowPatient["Rue"].ToString().ToLower().IndexOf("rue")) > -1)
                {
                    // MessageBox.Show("on est dedans");
                    CBRoute1.Text = "Rue";
                }
                else if ((rowPatient["Rue"].ToString().ToLower().IndexOf("avenue")) > -1)
                {
                    CBRoute1.Text = "Avenue";
                }
                else if ((rowPatient["Rue"].ToString().ToLower().IndexOf("boulevard")) > -1)
                {
                    CBRoute1.Text = "Boulevard";
                }
                else if ((rowPatient["Rue"].ToString().ToLower().IndexOf("place")) > -1)
                {
                    CBRoute1.Text = "Place";
                }
                else if ((rowPatient["Rue"].ToString().ToLower().IndexOf("passage")) > -1)
                {
                    CBRoute1.Text = "Passage";
                }
                else if ((rowPatient["Rue"].ToString().ToLower().IndexOf("sentier")) > -1)
                {
                    CBRoute1.Text = "Sentier";
                }
                else if ((rowPatient["Rue"].ToString().ToLower().IndexOf("square")) > -1)
                {
                    CBRoute1.Text = "Square";
                }
                else if ((rowPatient["Rue"].ToString().ToLower().IndexOf("chemin")) > -1)
                {
                    CBRoute1.Text = "Chemin";
                }
                else if ((rowPatient["Rue"].ToString().ToLower().IndexOf("allée")) > -1)
                {
                    CBRoute1.Text = "Allée";
                }
                else if ((rowPatient["Rue"].ToString().ToLower().IndexOf("cité")) > -1)
                {
                    CBRoute1.Text = "Cité";
                }
                else if ((rowPatient["Rue"].ToString().ToLower().IndexOf("cours")) > -1)
                {
                    CBRoute1.Text = "Cours";
                }
                else if ((rowPatient["Rue"].ToString().ToLower().IndexOf("impasse")) > -1)
                {
                    CBRoute1.Text = "Impasse";
                }
                else if ((rowPatient["Rue"].ToString().ToLower().IndexOf("quai")) > -1)
                {
                    CBRoute1.Text = "Quai";
                }
                    else if ((rowPatient["Rue"].ToString().ToLower().IndexOf("quai")) > -1)
                {
                    CBRoute1.Text = "Quai";
                }
                else
                {
                    CBRoute1.Text = "";        //c'est aucun de ces choix, donc on met CBRoute1 à blanc
                }
                
                
                TxtAdresse1.Text = rowPatient["Rue"].ToString();

                //Réaffectation du champs TexteSup pour le complément d'adresse
                TxtAdresse2.Text = rowPatient["TexteSup"].ToString();
                
                TxtNum.Text = rowPatient["NumeroDansRue"].ToString();
                TxtChez.Text = rowPatient["Chez"].ToString();
                TxtNp.Text = rowPatient["CodePostal"].ToString();
                TxtCommune.Text = rowPatient["Commune"].ToString();



                //Idem que CBRoute ci-dessus
                if ((rowPatient["Adm_Rue"].ToString().ToLower().IndexOf("route")) > -1)
                {
                    CBRoute_adm1.Text = "Route";
                }
                else if ((rowPatient["Adm_Rue"].ToString().ToLower().IndexOf("rue")) > -1)
                {
                    // MessageBox.Show("on est dedans");
                    CBRoute_adm1.Text = "Rue";
                }
                else if ((rowPatient["Adm_Rue"].ToString().ToLower().IndexOf("avenue")) > -1)
                {
                    CBRoute_adm1.Text = "Avenue";
                }
                else if ((rowPatient["Adm_Rue"].ToString().ToLower().IndexOf("boulevard")) > -1)
                {
                    CBRoute_adm1.Text = "Boulevard";
                }
                else if ((rowPatient["Adm_Rue"].ToString().ToLower().IndexOf("place")) > -1)
                {
                    CBRoute_adm1.Text = "Place";
                }
                else if ((rowPatient["Adm_Rue"].ToString().ToLower().IndexOf("passage")) > -1)
                {
                    CBRoute_adm1.Text = "Passage";
                }
                else if ((rowPatient["Adm_Rue"].ToString().ToLower().IndexOf("sentier")) > -1)
                {
                    CBRoute_adm1.Text = "Sentier";
                }
                else if ((rowPatient["Adm_Rue"].ToString().ToLower().IndexOf("square")) > -1)
                {
                    CBRoute_adm1.Text = "Square";
                }
                else if ((rowPatient["Adm_Rue"].ToString().ToLower().IndexOf("chemin")) > -1)
                {
                    CBRoute_adm1.Text = "Chemin";
                }
                else if ((rowPatient["Adm_Rue"].ToString().ToLower().IndexOf("allée")) > -1)
                {
                    CBRoute_adm1.Text = "Allée";
                }
                else if ((rowPatient["Adm_Rue"].ToString().ToLower().IndexOf("cité")) > -1)
                {
                    CBRoute_adm1.Text = "Cité";
                }
                else if ((rowPatient["Adm_Rue"].ToString().ToLower().IndexOf("cours")) > -1)
                {
                    CBRoute_adm1.Text = "Cours";
                }
                else if ((rowPatient["Adm_Rue"].ToString().ToLower().IndexOf("impasse")) > -1)
                {
                    CBRoute_adm1.Text = "Impasse";
                }
                else if ((rowPatient["Adm_Rue"].ToString().ToLower().IndexOf("quai")) > -1)
                {
                    CBRoute_adm1.Text = "Quai";
                }
                else
                {
                    CBRoute_adm1.Text = "";        //c'est aucun de ces choix, donc on met CBRoute_adm1 à blanc
                }

                
              	
				TxtRue_AdmAdresse1.Text =rowPatient["Adm_Rue"].ToString();
                TxtNum_Adm.Text = rowPatient["Adm_NumeroDansRue"].ToString();
                
                //Réaffectation du champs ListeNoire pour le complément d'adresse
                TxtRue_AdmAdresse2.Text = rowPatient["ListeNoire"].ToString();
				
                TxtNp_Adm.Text = rowPatient["Adm_CodePostal"].ToString();
				TxtCommune_Adm.Text =rowPatient["Adm_Commune"].ToString();
				txtAdmPays.Text = rowPatient["Adm_Pays"].ToString();
                //TxtDossier.Text = rowPatient["SuiviPatient"].ToString();
                txtDepartement.Text = rowPatient["Departement"].ToString();
				txtBatiment_Adm.Text = rowPatient["Adm_Batiment"].ToString();
				//txtBatiment.Text = rowPatient["Batiment"].ToString();
				txtInterphone.Text = rowPatient["Internom"].ToString();
				txtPorte.Text = rowPatient["Porte"].ToString();
				txtEtage.Text = rowPatient["Etage"].ToString();
				//txtEscalier.Text = rowPatient["Escalier"].ToString();
				txtDigicode.Text = rowPatient["Digicode"].ToString();

				if(rowPatient["DateDeces"].ToString()!=System.DBNull.Value.ToString() && rowPatient["DateDeces"].ToString()!="")
				{
					txtDtDeces.Text = DateTime.Parse(rowPatient["DateDeces"].ToString()).ToString("dd/MM/yyyy");
					panel1.BackColor = Color.CadetBlue;
				}
                else
                    txtDtDeces.Text = "";

				//on verifie s'il y a des documents ECG, Analyses, autre...en ne prennant que l'occurence
                //la plus récente
                string cheminSource = SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.Path_DocumentsSmartRapport;

                //ECG
                string[][] retour1 = OutilsExt.OutilsSql.ExecuteCommandeAvecTabString("SELECT jd.UrlJointDoc from patientjointdoc jd where jd.UrlJointDoc like '%ECG%' and jd.IdPatient = " + rowPatient["IdPatient"].ToString());
                if (retour1 != null && retour1.Length > 0)
                {
                    //On a quelque chose, donc on prend la derniere valeur de la requette 
                    BECG.Tag = cheminSource + retour1[retour1.Length - 1][0].ToString();
                    BECG.Enabled = true;
                    BECG.BackgroundImage = ImportSosGeneve.Properties.Resources.becg_on;   //On met l'image active
                }

                //Analyses                
                string[][] retour2 = OutilsExt.OutilsSql.ExecuteCommandeAvecTabString("SELECT jd.UrlJointDoc from patientjointdoc jd where jd.UrlJointDoc like '%Analyse%' and jd.IdPatient = " + rowPatient["IdPatient"].ToString());
                if (retour2 != null && retour2.Length > 0)
                {
                    //On a quelque chose, donc on prend la derniere valeur de la requette 
                    BAnalyses.Tag = cheminSource + retour2[retour2.Length - 1][0].ToString();
                    BAnalyses.Enabled = true;
                    BAnalyses.BackgroundImage = ImportSosGeneve.Properties.Resources.bchimie_on;  //On met l'image active
                }

                //Autre
                 string[][] retour3 = OutilsExt.OutilsSql.ExecuteCommandeAvecTabString("SELECT jd.UrlJointDoc from patientjointdoc jd where jd.UrlJointDoc like '%Autre%' and jd.IdPatient = " + rowPatient["IdPatient"].ToString());
                if (retour3 != null && retour3.Length > 0)
                {
                    //On a quelque chose, donc on prend la derniere valeur de la requette 
                    bautre.Tag = cheminSource + retour3[retour3.Length - 1][0].ToString();
                    bautre.Enabled = true;
                    bautre.BackgroundImage = ImportSosGeneve.Properties.Resources.bautre_on; //On met l'image active
                }

                MiseEnPlaceDestinataireFacture(rowPatient);

			}
		}

		private void MiseEnPlaceDestinataireFacture(DataRow rowPatient)
		{
			lwDestinataireDate.Items.Clear();
			LblDestinataireFacture.Tag=null;
			LblDestinataireFacture.Text="";

			if(rowPatient==null)
			{
				return;
			}
			else
			{
				string[][] retour = OutilsExt.OutilsSql.ExecuteCommandeAvecTabString("SELECT TypeDestinataire,IdDestinataire,DateDebut,DateFin from fac_destinatairefacture where IdPatient = " + rowPatient["IdPatient"].ToString());
                if (retour != null)
                {
                    for (int i = 0; i < retour.Length; i++)
                    {
                        Dest destFac = new Dest();
                        destFac.m_TypeDestinataire = (CtrlDest.TypeDestinataire)int.Parse(retour[i][0]);
                        destFac.CodeDestinataireFacture = int.Parse(retour[i][1]);
                        if (destFac.m_TypeDestinataire == CtrlDest.TypeDestinataire.Idem)
                            destFac.AdresseDestinataire = OutilsExt.OutilsSql.GetAdresseFromPatient(int.Parse(rowPatient["IdPatient"].ToString()));
                        else if (destFac.m_TypeDestinataire == CtrlDest.TypeDestinataire.Hotel)
                            destFac.AdresseDestinataire = OutilsExt.OutilsSql.GetAdresseFromHotel(destFac.CodeDestinataireFacture);
                        else if (destFac.m_TypeDestinataire == CtrlDest.TypeDestinataire.Assurance)
                            destFac.AdresseDestinataire = OutilsExt.OutilsSql.GetAdresseFromAssurance(destFac.CodeDestinataireFacture);
                        else if (destFac.m_TypeDestinataire == CtrlDest.TypeDestinataire.Commissariat)
                            destFac.AdresseDestinataire = OutilsExt.OutilsSql.GetAdresseFromCommissariat();
                        else if (destFac.m_TypeDestinataire == CtrlDest.TypeDestinataire.Tiers)
                            destFac.AdresseDestinataire = OutilsExt.OutilsSql.GetAdresseFromTiers(destFac.CodeDestinataireFacture);
                        destFac.DateDebut = DateTime.Parse(retour[i][2]);
                        destFac.DateFin = DateTime.Parse(retour[i][3]);
                        AjouteDestinataireFactureInListView(destFac);
                    }

                }
               // else
               // {

                    // si ce n'est pas le case, on utilise le destinataire par défaut :
                    string[][] retour2 = OutilsExt.OutilsSql.ExecuteCommandeAvecTabString("SELECT TypeDestinataireFacture,IdDestinataireFacture from tablepatient where IdPatient = " + rowPatient["IdPatient"].ToString());
                    // par défaut le destinataire est défini dans la tablepatient :
                    Dest dest = new Dest();
                    if (retour2 != null && retour2.Length == 1)
                    {
                        dest.m_TypeDestinataire = (CtrlDest.TypeDestinataire)int.Parse(retour2[0][0]);
                        dest.CodeDestinataireFacture = int.Parse(retour2[0][1]);
                    }
                    // par défaut le destinataire est défini dans la table :
                    else
                    {
                        dest.m_TypeDestinataire = CtrlDest.TypeDestinataire.Idem;
                        dest.CodeDestinataireFacture = 0;
                    }

                    if (dest.m_TypeDestinataire == CtrlDest.TypeDestinataire.Idem)
                        dest.AdresseDestinataire = OutilsExt.OutilsSql.GetAdresseFromPatient(int.Parse(rowPatient["IdPatient"].ToString()));
                    else if (dest.m_TypeDestinataire == CtrlDest.TypeDestinataire.Hotel)
                        dest.AdresseDestinataire = OutilsExt.OutilsSql.GetAdresseFromHotel(dest.CodeDestinataireFacture);
                    else if (dest.m_TypeDestinataire == CtrlDest.TypeDestinataire.Assurance)
                        dest.AdresseDestinataire = OutilsExt.OutilsSql.GetAdresseFromAssurance(dest.CodeDestinataireFacture);
                    else if (dest.m_TypeDestinataire == CtrlDest.TypeDestinataire.Commissariat)
                        dest.AdresseDestinataire = OutilsExt.OutilsSql.GetAdresseFromCommissariat();
                    else if (dest.m_TypeDestinataire == CtrlDest.TypeDestinataire.Tiers)
                        dest.AdresseDestinataire = OutilsExt.OutilsSql.GetAdresseFromTiers(dest.CodeDestinataireFacture);

                    LblDestinataireFacture.Tag = dest;
                    LblDestinataireFacture.Text = dest.AdresseDestinataire;
                    //find if destinataire 10% exist
                    string[][] retour10 = OutilsExt.OutilsSql.ExecuteCommandeAvecTabString("SELECT TypeDestinataire,IdDestinataire,DateDebut,DateFin from fac_destinatairefacture10 where IdPatient = " + rowPatient["IdPatient"].ToString());
                    if (retour10 != null && retour10.Length == 1)
                    {
                        Dest destFac = new Dest();
                        destFac.m_TypeDestinataire = (CtrlDest.TypeDestinataire)int.Parse(retour10[0][0]);
                        destFac.CodeDestinataireFacture = int.Parse(retour10[0][1]);
                        destFac.AdresseDestinataire = OutilsExt.OutilsSql.GetAdresseFromTiers(destFac.CodeDestinataireFacture);
                        destFac.DateDebut = DateTime.Parse(retour10[0][2]);
                        destFac.DateFin = DateTime.Parse(retour10[0][3]);
                        lbDestinataire10.Text = destFac.AdresseDestinataire;
                        lbDestinataire10.Tag = destFac;
                    }
                }
			//}
		}

		private void AfficheRem(DataRow rowPatient)
		{
            // recuperation des remarques
            Patient_Remarque z_dalPatientRemarque = new Patient_Remarque();
            DataTable z_dtb = z_dalPatientRemarque.Select(rowPatient["IdPatient"].ToString());
            // 
            if (z_dtb != null && z_dtb.Rows.Count > 0)
            {
                _drwPatientRemarque = z_dtb.Rows[0];
                TxtRemEco.Text = _drwPatientRemarque["Economique"].ToString();
                TxtRemMed.Text = _drwPatientRemarque["Medical"].ToString();

                chkSurPlace.Checked = (Convert.ToInt32(_drwPatientRemarque["Encaisse"]) == 1);
                chkCessionCreance.Checked = (Convert.ToInt32(_drwPatientRemarque["Cession"]) == 1);
            }
            else
            {
                _drwPatientRemarque = z_dtb.NewRow();
                _drwPatientRemarque["IdPatient"] = rowPatient["IdPatient"].ToString();
                _drwPatientRemarque["Economique"] = string.Empty;
                _drwPatientRemarque["Medical"] = string.Empty;
                _drwPatientRemarque["Encaisse"] = 0;
                _drwPatientRemarque["Cession"] = 0;

                z_dtb.Rows.Add(_drwPatientRemarque);
                TxtRemEco.Text = "";
                TxtRemMed.Text = "";
                chkSurPlace.Checked = false;
                chkCessionCreance.Checked = false;
            }
        }

		private void AfficheMedTT(DataRow rowPatient)
		{
			if(rowPatient==null)
			{
				pan_MedTT.Enabled = false;
				lstMedTT.Items.Clear();
			}
			else
			{
				pan_MedTT.Enabled = true;
				lstMedTT.Items.Clear();
				DataSet ds = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet("SELECT m.* from tablepatientmedttt p inner join medecinsville m on m.Num = p.idMedecin WHERE p.IdPatient = " + rowPatient["IdPatient"].ToString());
				if(ds!=null)
				{
					for(int i=0;i<ds.Tables[0].Rows.Count;i++)
					{
						AjouteMedTT(ds.Tables[0].Rows[i]);						
					}
				}
			}
		}

		private void fpListe_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			FarPoint.Win.Spread.Model.CellRange range = fpListe.GetCellFromPixel(0,0,e.X,e.Y);

			if(range.Row==-1 || range.Column==-1) return;

			// Affichage de la fiche
			if(e.Button==MouseButtons.Left)
			{
				DataRow rw = (DataRow)fpListe_Sheet1.Rows[range.Row].Tag;
				AffichagePatient(rw);
			}
		}

		#endregion		

		#region Sauvegarde d'enregistrements
        //On affecte le contenu des boites à l'enregistrement
		public void btnSavePatient_Click(object sender, System.EventArgs e)
		{
            int Id = 0;
           
            Console.WriteLine(EMaskTel1.Text.Replace("-", "").Replace(" ", ""));

            if(tabFip.Tag==null) return;
				DataRow rowPatient = (DataRow)tabFip.Tag;
                
				// Controle de ce qui est saisi : 
				bool Controle = ControleSaisie();
				if(!Controle)
				{
					MessageBox.Show("Tous les champs marqués en violet doivent être saisis!");
					return;
				}

				// Sauvegarde de la fip / données administratives
				rowPatient["Nom"] = TxtNom.Text;
				rowPatient["Prenom"] = TxtPrenom.Text;
                
                if (EMaskTel1.Text.IndexOf('+') == -1)
                    rowPatient["Tel"] = "+" + EMaskTel1.Text.Replace("-", "").Replace(" ", "");
                else
                    rowPatient["Tel"] = EMaskTel1.Text.Replace("-", "").Replace(" ", "");

                rowPatient["DateNaissance"] = TxtDtNaissance.Text;
				rowPatient["Sexe"] = CbSexe.Text;
                rowPatient["Num_Carte"] = tBNumCarte.Text;
                rowPatient["Num_Assure"] = tBoxNunAssure.Text;
                rowPatient["Num_AVS"] = tBoxNumAVS.Text;                            
                rowPatient["Email"] = TxtMail.Text;

				rowPatient["NumeroDansRue"] = TxtNum.Text;
				
                //on test le contenu de la chaine Adresse1
                //Si elle ne contient pas route, rue... (le contenu de CBRoute1),
                //alors on affecte le contenu de CBRoute1
                if ((TxtAdresse1.Text.ToLower().IndexOf("route") > -1) ||
                   (TxtAdresse1.Text.ToLower().IndexOf("rue") > -1) ||
                   (TxtAdresse1.Text.ToLower().IndexOf("avenue") > -1) ||
                   (TxtAdresse1.Text.ToLower().IndexOf("boulevard") > -1) ||
                   (TxtAdresse1.Text.ToLower().IndexOf("place") > -1) ||
                   (TxtAdresse1.Text.ToLower().IndexOf("passage") > -1) ||
                   (TxtAdresse1.Text.ToLower().IndexOf("sentier") > -1) ||
                   (TxtAdresse1.Text.ToLower().IndexOf("square") > -1) ||
                   (TxtAdresse1.Text.ToLower().IndexOf("chemin") > -1) ||
                   (TxtAdresse1.Text.ToLower().IndexOf("allée") > -1) ||
                   (TxtAdresse1.Text.ToLower().IndexOf("cité") > -1) ||
                   (TxtAdresse1.Text.ToLower().IndexOf("cours") > -1) ||
                   (TxtAdresse1.Text.ToLower().IndexOf("impasse") > -1) ||
                   (TxtAdresse1.Text.ToLower().IndexOf("quai") > -1))
                {
                    rowPatient["Rue"] = TxtAdresse1.Text;
                }
                else   //Sinon on ne le réaffecte pas (il y est déja)
                {
                    rowPatient["Rue"] = CBRoute1.Text + " " + TxtAdresse1.Text;
                }

                //Réaffectation du champ TexteSup pour l'adresse2
                rowPatient["TexteSup"] = TxtAdresse2.Text;
				rowPatient["CodePostal"] = TxtNp.Text;
				rowPatient["Commune"] = TxtCommune.Text;
				rowPatient["Departement"] = txtDepartement.Text;
				//rowPatient["Batiment"] = txtBatiment.Text;
				rowPatient["Adm_NumeroDansRue"] = TxtNum_Adm.Text;

                //on test le contenu de la chaine AdmAdresse1
                //Si elle ne contient pas route, rue... (le contenu de CBRoute_adm1),
                //alors on affecte le contenu de CBRoute_adm1
                if ((TxtRue_AdmAdresse1.Text.ToLower().IndexOf("route") > -1) ||
                   (TxtRue_AdmAdresse1.Text.ToLower().IndexOf("rue") > -1) ||
                   (TxtRue_AdmAdresse1.Text.ToLower().IndexOf("avenue") > -1) ||
                   (TxtRue_AdmAdresse1.Text.ToLower().IndexOf("boulevard") > -1) ||
                   (TxtRue_AdmAdresse1.Text.ToLower().IndexOf("place") > -1) ||
                   (TxtRue_AdmAdresse1.Text.ToLower().IndexOf("passage") > -1) ||
                   (TxtRue_AdmAdresse1.Text.ToLower().IndexOf("sentier") > -1) ||
                   (TxtRue_AdmAdresse1.Text.ToLower().IndexOf("square") > -1) ||
                   (TxtRue_AdmAdresse1.Text.ToLower().IndexOf("chemin") > -1) ||
                   (TxtRue_AdmAdresse1.Text.ToLower().IndexOf("allée") > -1) ||
                   (TxtRue_AdmAdresse1.Text.ToLower().IndexOf("cité") > -1) ||
                   (TxtRue_AdmAdresse1.Text.ToLower().IndexOf("cours") > -1) ||
                   (TxtRue_AdmAdresse1.Text.ToLower().IndexOf("impasse") > -1) ||
                   (TxtRue_AdmAdresse1.Text.ToLower().IndexOf("lotissement") > -1) ||
                   (TxtRue_AdmAdresse1.Text.ToLower().IndexOf("quai") > -1))
                {
                    rowPatient["Adm_Rue"] = TxtRue_AdmAdresse1.Text;
                }
                else   //Sinon on ne le réaffecte pas (il y est déja)
                {

                    rowPatient["Adm_Rue"] = CBRoute_adm1.Text + " " + TxtRue_AdmAdresse1.Text;
                    //MessageBox.Show("on passe");  
                }            
            
                rowPatient["ListeNoire"] = TxtRue_AdmAdresse2.Text;
				rowPatient["Adm_CodePostal"] = TxtNp_Adm.Text;
				rowPatient["Adm_Commune"] = TxtCommune_Adm.Text;
                rowPatient["Adm_Pays"] = txtAdmPays.Text;
                rowPatient["Adm_Batiment"] = txtBatiment_Adm.Text;
				rowPatient["Escalier"] = txtEscalier.Text;
				rowPatient["Etage"] = txtEtage.Text;
				rowPatient["Digicode"] = txtDigicode.Text;
				rowPatient["Internom"] = txtInterphone.Text;
				rowPatient["Porte"] = txtPorte.Text;
				//rowPatient["TexteSup"] = TxtCommentaire.Text;
				//rowPatient["SuiviPatient"] = TxtDossier.Text;
				rowPatient["Chez"] = TxtChez.Text;

				if(txtDtDeces.Text!="")
				{
					rowPatient["DateDeces"] = DateTime.Parse(txtDtDeces.Text).ToString("dd/MM/yyyy");
				}
				else
					rowPatient["DateDeces"] = "";

				bool reussite1 = OutilsExt.OutilsSql.SauvegardePatientComplet(rowPatient);

                mouchard.evenement("Modification de la FIP pour " + TxtNom.Text.ToString() + " " + TxtPrenom.Text.ToString() + ": Modification du nom ou adresse.", VariablesApplicatives.Utilisateurs.NomUtilisateur.ToString());  //log            

				bool reussite2 = true;
				// Sauvegarde des remarques économiques et médicales
                if (_drwPatientRemarque["Economique"].ToString().ToLower() != TxtRemEco.Text.ToLower()
                    || _drwPatientRemarque["Medical"].ToString().ToLower() != TxtRemMed.Text.ToLower()
                    || Convert.ToInt32(_drwPatientRemarque["Encaisse"]) != Convert.ToInt32(chkSurPlace.Checked)
                    || Convert.ToInt32(_drwPatientRemarque["Cession"]) != Convert.ToInt32(chkCessionCreance.Checked))
                {
                    _drwPatientRemarque["Economique"] = TxtRemEco.Text;
                    _drwPatientRemarque["Medical"] = TxtRemMed.Text;
//************************ A voir pour encaisse/cessions de creance........

                    if (chkSurPlace.Checked)
                    {
                        _drwPatientRemarque["Encaisse"] = 1;
                    }
                    else
                    {
                        _drwPatientRemarque["Encaisse"] = 0;
                    }
        
                    if (chkCessionCreance.Checked)
                    {
                        _drwPatientRemarque["Cession"] = 1;
                    }
                    else
                    {
                        _drwPatientRemarque["Cession"] = 0;
                    }
//*****************************                    
                    Patient_Remarque z_dalPatientRemarque = new Patient_Remarque();
                    if (_drwPatientRemarque.RowState == DataRowState.Modified)
                    {
                        z_dalPatientRemarque.UpdateRemarque(_drwPatientRemarque, SosMedecins.SmartRapport.GestionApplication.VariablesApplicatives.Utilisateurs.Identifiant);
                        mouchard.evenement("Modification de la FIP pour " + TxtNom.Text.ToString() + " " + TxtPrenom.Text.ToString() + ": Modification des remarques", VariablesApplicatives.Utilisateurs.NomUtilisateur.ToString());  //log            
                    }
                    else
                    {
                        z_dalPatientRemarque.InsertRemarque(_drwPatientRemarque, SosMedecins.SmartRapport.GestionApplication.VariablesApplicatives.Utilisateurs.Identifiant);
                        mouchard.evenement("Modification de la FIP pour " + TxtNom.Text.ToString() + " " + TxtPrenom.Text.ToString() + ": Ajout de remarque.", VariablesApplicatives.Utilisateurs.NomUtilisateur.ToString());  //log
                    }
                }
                //
				OutilsExt.OutilsSql.ExecuteCommandeSansRetour("DELETE FROM tablepatientmedTTT WHERE IdPatient = " + rowPatient["IdPatient"].ToString());
				for(int i=0;i<lstMedTT.Items.Count;i++)
				{
					DataRow rowMed = (DataRow)((ListItem)lstMedTT.Items[i]).objValue;
					string reqInsertMedTTT = "INSERT INTO tablepatientmedTTT (IdPatient,IdMedecin) values (" + rowPatient["IdPatient"].ToString() + "," + rowMed["Num"].ToString() + ")";
					OutilsExt.OutilsSql.ExecuteCommandeSansRetour(reqInsertMedTTT);

                    mouchard.evenement("Modification de la FIP pour " + TxtNom.Text.ToString() + " " + TxtPrenom.Text.ToString() + ": Ajout du med. traitant " + rowMed["Num"].ToString(), VariablesApplicatives.Utilisateurs.NomUtilisateur.ToString());  //log
				}

				// Sauvegarde du destinataire de la facture
				Dest dest= (Dest)LblDestinataireFacture.Tag;
				bool reussite3=OutilsExt.OutilsSql.ExecuteCommandeSansRetour("update tablepatient set TypeDestinataireFacture = " + (int)dest.m_TypeDestinataire +  " ,IdDestinataireFacture = " + dest.CodeDestinataireFacture + " Where IdPatient = " + rowPatient["IdPatient"].ToString());

                mouchard.evenement("Modification de la FIP pour " + TxtNom.Text.ToString() + " " + TxtPrenom.Text.ToString() + ": Modif du destinataire de la facture... " + dest.CodeDestinataireFacture.ToString(), VariablesApplicatives.Utilisateurs.NomUtilisateur.ToString());  //log

				// Sauvegarde des destinataires datés de la facture
				OutilsExt.OutilsSql.ExecuteCommandeSansRetour("DELETE From fac_destinatairefacture Where IdPatient = " + rowPatient["IdPatient"].ToString());
				for(int i=0;i<lwDestinataireDate.Items.Count;i++)
				{
					Dest destFac = (Dest)lwDestinataireDate.Items[i].Tag;
					//*******************************************
                    //on prend le plus grand chiffre

                    string[][]retour = OutilsExt.OutilsSql.ExecuteCommandeAvecTabString("SELECT max(Id) from fac_destinatairefacture");

                    if (retour != null && retour.Length != 0 && retour[0][0] != "")
                    {
                        Id = int.Parse(retour[0][0]) + 1;
                    }
                    else
                    {
                        Id = 1;
                    }
                    
                    
                    //*******************************************              


                    OutilsExt.OutilsSql.ExecuteCommandeSansRetour("insert into fac_destinatairefacture (Id, IdPatient,TypeDestinataire,IdDestinataire,DateDebut,DateFin) values (" + Id.ToString() + "," + rowPatient["IdPatient"].ToString() + "," + (int)destFac.m_TypeDestinataire + "," + destFac.CodeDestinataireFacture + ",'" + OutilsExt.OutilsSql.DateFormatMySql(destFac.DateDebut) + "','" + OutilsExt.OutilsSql.DateFormatMySql(destFac.DateFin) + "')");
				}
				// Sauvegarde de destinataire 10% datés de la facture
				if (lbDestinataire10.Tag != null)
				{
					Dest destFac = (Dest)lbDestinataire10.Tag;
					OutilsExt.OutilsSql.ExecuteCommandeSansRetour("DELETE From fac_destinatairefacture10 Where IdPatient = " + rowPatient["IdPatient"].ToString());
					OutilsExt.OutilsSql.ExecuteCommandeSansRetour("insert into fac_destinatairefacture10 (IdPatient,TypeDestinataire,IdDestinataire,DateDebut,DateFin) values (" + rowPatient["IdPatient"].ToString() + "," + (int)destFac.m_TypeDestinataire + "," + destFac.CodeDestinataireFacture + ",'" + OutilsExt.OutilsSql.DateFormatMySql(destFac.DateDebut) + "','" + OutilsExt.OutilsSql.DateFormatMySql(destFac.DateFin) + "')");
				}
				if(reussite1 && reussite2 && reussite3)				
					LblStatut.Text = "Sauvegarde réussie";
				else
					LblStatut.Text = "Erreur lors de la sauvegarde";
		}

		private bool ControleSaisie()
		{
			if(TxtNom.Text=="")
				return false;
			if(TxtPrenom.Text=="")
				return false;
            if (EMaskTel1.Text == "")
				return false;			
			if(CbSexe.Text=="")
				return false;
			if(TxtAdresse1.Text=="")
				return false;
			if(TxtCommune.Text=="")
				return false;

			return true;
		}

		#endregion	

		#region Evenements du formulaire

		private void btnListeAppels_Click(object sender, System.EventArgs e)
		{
			if(tabFip.Tag!=null)
			{
				this.Cursor = Cursors.WaitCursor;
				DataRow row = (DataRow)tabFip.Tag;

				// on recherche si plusieurs patients sont rapprochés à celui sélectionné : 
				DataRow[] rows = ((DataSet)fpListe.Tag).Tables[0].Select("Doublon=" + row["Doublon"].ToString());
				if(rows!=null && rows.Length>1)
                {
					int[] Patients = new int[rows.Length];
					for(int i=0;i<rows.Length;i++)
					{
						Patients[i] = int.Parse(rows[i]["IdPatient"].ToString());
					}
					this.m_frmgeneral.AfficheAppelsByPatient(Patients);
				}
				else
					this.m_frmgeneral.AfficheAppelsByPatient(int.Parse(row["IdPatient"].ToString()));
				this.Cursor = Cursors.Default;
				this.Hide();
				this.Text = this.TxtNom.Text +" "+ this.TxtPrenom.Text;
				this.m_frmgeneral.AjouteFenetreCachee(this);
			}
		}

		private void FIP_Closed(object sender, System.EventArgs e)
		{
			this.Dispose();
		}

		private void btnRapports_Click(object sender, System.EventArgs e)
		{
			if(tabFip.Tag!=null)
			{
				this.Cursor = Cursors.WaitCursor;
				DataRow row = (DataRow)tabFip.Tag;

				// on recherche si plusieurs patients sont rapprochés à celui sélectionné : 
				DataRow[] rows = ((DataSet)fpListe.Tag).Tables[0].Select("Doublon=" + row["Doublon"].ToString());
				if(rows!=null && rows.Length>1)
				{
					int[] Patients = new int[rows.Length];
					for(int i=0;i<rows.Length;i++)
					{
						Patients[i] = int.Parse(rows[i]["IdPatient"].ToString());
					}
					this.m_frmgeneral.AfficheRapportsByPatient(Patients);
				}
				else
					this.m_frmgeneral.AfficheRapportsByPatient(int.Parse(row["IdPatient"].ToString()));

				this.Cursor = Cursors.Default;
				this.Hide();
				this.m_frmgeneral.AjouteFenetreCachee(this);
			}
		}

		private void lnkAjoutMedTT_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			frmListeEffecteur frm = new frmListeEffecteur();
			frm.ShowDialog();
			AjouteMedTT(frm.RowSelected);
			frm.Dispose();
			frm=null;
		}

		private void lstMedTT_DoubleClick(object sender, System.EventArgs e)
		{
			if(lstMedTT.SelectedIndex>-1)
			{
				ListItem item = (ListItem)lstMedTT.SelectedItem;
				DataRow row = (DataRow)item.objValue;
				frmListeEffecteur frm = new frmListeEffecteur(frmListeEffecteur.TypeListe.MedecinVille, row);
				frm.ShowDialog();
				row = frm.RowSelected;
				frm.Dispose();
				frm=null;				
			}
		}

		private void AjouteMedTT( DataRow row)
		{
			if(row!=null)
			{
				ListItem item = new ListItem(row,row["Nom"].ToString()  + " "  + row["Prenom"].ToString() + " [" + row["Commune"].ToString() + "]");
				lstMedTT.Items.Add(item);
			}
		}

		private void lnkSupMedTT_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			if(lstMedTT.SelectedIndex>-1)
			{
				lstMedTT.Items.RemoveAt(lstMedTT.SelectedIndex);		
			}
		}

		private void lnkChangerDestinataireFacture_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			if(m_CtrlDest!=null)
			{
				tbFactu.Controls.Remove(m_CtrlDest);
				m_CtrlDest.Dispose();
				m_CtrlDest=null;
			}

			if(tabFip.Tag==null) return;
			Dest dest = (Dest)LblDestinataireFacture.Tag;
			m_CtrlDest = new CtrlDest(this,dest,"");
			tbFactu.Controls.Add(m_CtrlDest);
			m_CtrlDest.Top = lnkChangerDestinataireFacture.Top ;
			m_CtrlDest.Left = 100;
		}

		public void DisposeCtrlDest(Dest dest,CtrlDest.TypeOuverture mTypeOuverture)
		{
			if(m_CtrlDest!=null)
			{
				tbFactu.Controls.Remove(m_CtrlDest);
				m_CtrlDest.Dispose();
				m_CtrlDest=null;
			}

			if(dest!=null && mTypeOuverture==CtrlDest.TypeOuverture.DefautFacture)
			{
				if(dest.m_TypeDestinataire==CtrlDest.TypeDestinataire.Idem)
				{
					dest.CodeDestinataireFacture=0;
					DataRow rowPatient = (DataRow)tabFip.Tag;
					dest.AdresseDestinataire = rowPatient["Nom"].ToString() + " " + rowPatient["Prenom"].ToString() + WorkedString.GetAdresseFormatee(rowPatient["Chez"].ToString(),rowPatient["Adm_Rue"].ToString(),rowPatient["Adm_NumeroDansrue"].ToString(),rowPatient["Adm_CodePostal"].ToString(),rowPatient["Adm_Commune"].ToString());
				}
				
				LblDestinataireFacture.Tag=dest;
				LblDestinataireFacture.Text = dest.AdresseDestinataire;
			}
			else if(dest!=null && mTypeOuverture==CtrlDest.TypeOuverture.ListeFacture)
			{
				AjouteDestinataireFactureInListView(dest);
			}
		}

		private void AjouteDestinataireFactureInListView(Dest dest)
		{
			ListViewItem item = new ListViewItem(dest.m_TypeDestinataire.ToString());
			item.Tag=dest;
			item.SubItems.Add(dest.AdresseDestinataire);
			item.SubItems.Add(dest.DateDebut.ToString("dd/MM/yyyy"));
			item.SubItems.Add(dest.DateFin.ToString("dd/MM/yyyy"));
			lwDestinataireDate.Items.Add(item);
		}

		private void lwDestinataireDate_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(e.Button==MouseButtons.Left)
			{
				ContextMenu menu = new ContextMenu();
				MenuItem item = new MenuItem("Ajouter destinataire",new System.EventHandler(MenuAjoutDestinataireFacture_Click));
				menu.MenuItems.Add(item);
				if(lwDestinataireDate.SelectedIndices.Count>0)
				{
					item = new MenuItem("Supprimer destinataire",new System.EventHandler(MenuAjoutDestinataireFacture_Click));
					menu.MenuItems.Add(item);
					item = new MenuItem("Modifier destinataire",new System.EventHandler(MenuAjoutDestinataireFacture_Click));
					menu.MenuItems.Add(item);
				}
				item = new MenuItem("Supprimer tous les destinataires",new System.EventHandler(MenuAjoutDestinataireFacture_Click));
				menu.MenuItems.Add(item);
				menu.Show(lwDestinataireDate,new Point(e.X,e.Y));
			}

		}

		private void MenuAjoutDestinataireFacture_Click(System.Object sender,EventArgs e)
		{
			MenuItem item = (MenuItem)sender;
			switch(item.Text)
			{
				case "Ajouter destinataire":
					if(m_CtrlDest!=null)
					{
						tbFactu.Controls.Remove(m_CtrlDest);
						m_CtrlDest.Dispose();
						m_CtrlDest=null;
					}
					if(tabFip.Tag==null) return;
					m_CtrlDest = new CtrlDest(this,CtrlDest.TypeOuverture.ListeFacture);
					tbFactu.Controls.Add(m_CtrlDest);
					m_CtrlDest.Top = lwDestinataireDate.Top + lwDestinataireDate.Height + 10;
					m_CtrlDest.Left = 130;
					break;
				case "Modifier destinataire":
					if(m_CtrlDest!=null)
					{
						tbFactu.Controls.Remove(m_CtrlDest);
						m_CtrlDest.Dispose();
						m_CtrlDest=null;
					}
					if(tabFip.Tag==null) return;
					DataRow m_SelectedRow = (DataRow)tabFip.Tag;
					Dest dest = GetDestinataire();
                    string[][] retour = OutilsExt.OutilsSql.ExecuteCommandeAvecTabString("SELECT TypeDestinataire,IdDestinataire,DateDebut,DateFin from fac_destinatairefacture where IdPatient = " + int.Parse(m_SelectedRow["IdPatient"].ToString()));
					//string[][] retour = OutilsExt.OutilsSql.ExecuteCommandeAvecTabString("SELECT Id, TypeDestinataire,IdDestinataire,DateDebut,DateFin from fac_destinatairefacture where IdPatient = " + int.Parse(m_SelectedRow["IdPatient"].ToString()));
					dest.DateDebut = DateTime.Parse(retour[0][2].ToString());
					dest.DateFin = DateTime.Parse(retour[0][3].ToString());
					frmSelectionDate selectDate = new frmSelectionDate(dest.DateDebut, dest.DateFin);
					selectDate.ShowDialog();
					dest.DateDebut = selectDate.dt1.Value;
					dest.DateFin = selectDate.dt2.Value;
                    OutilsExt.OutilsSql.ExecuteCommandeSansRetour("update fac_destinatairefacture set DateDebut = '" + OutilsExt.OutilsSql.DateFormatMySql(dest.DateDebut) 
                                            + "', DateFin = '" + OutilsExt.OutilsSql.DateFormatMySql(dest.DateFin) + "'  WHERE IdPatient = " + int.Parse(m_SelectedRow["IdPatient"].ToString())); 
                          //                  + " and Id= " + retour[0][0].ToString());

					selectDate.Dispose();
					selectDate=null;

                    //on rafraichi la liste
                    MiseEnPlaceDestinataireFacture(m_SelectedRow);
                 	
					break;
				case "Supprimer destinataire":
					lwDestinataireDate.Items.RemoveAt(lwDestinataireDate.SelectedIndices[0]);
					break;
				case "Supprimer tous les destinataires":
					lwDestinataireDate.Items.Clear();
					break;
				default:
					break;
			}
		}

		public Dest GetDestinataire()
		{
			if(tabFip.Tag!=null)
			{
				DataRow m_SelectedRow = (DataRow)tabFip.Tag;
				Dest dest = new Dest();
				dest.m_TypeDestinataire = CtrlDest.TypeDestinataire.AutrePrive;
				dest.CodeDestinataireFacture = int.Parse(m_SelectedRow["IdPersonne"].ToString());
				dest.AdresseDestinataire = OutilsExt.OutilsSql.GetAdresseFromPatient(dest.CodeDestinataireFacture);

				return dest;
			}
			else
				return null;
		}

		private void Find_Rue_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Enter)
			{
				btnRecherche_Click(null,null);
			}
		}

		private void fpListe_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			FarPoint.Win.Spread.Model.CellRange range = fpListe.GetCellFromPixel(0,0,e.X,e.Y);
			if(range.Row>-1 && e.Button == MouseButtons.Right)
			{
				fpListe_Sheet1.SetActiveCell(range.Row,1);
				DataRow row = (DataRow)fpListe_Sheet1.Rows[range.Row].Tag;
				if(row!=null)
				{
					string data = row["IdPatient"].ToString() + ";" + row["Nom"].ToString() + " " + row["Prenom"].ToString();
					fpListe.DoDragDrop(data,DragDropEffects.Move);
				}
			}			
		}

		private void fpListe_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
		{
			e.Effect = DragDropEffects.Move;		
		}

		private void fpListe_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
		{
			if(e.Data.GetDataPresent(typeof(string)))
			{
				Point pt = fpListe.PointToClient(new Point(e.X,e.Y));
				FarPoint.Win.Spread.Model.CellRange range = fpListe.GetCellFromPixel(0,0,pt.X,pt.Y);
				if(range.Row>-1)
				{
					// Patient dans lequel on copie le doublon : 
					DataRow row  =(DataRow)fpListe_Sheet1.Rows[range.Row].Tag;
                    long Indice1 = long.Parse(row["IdPatient"].ToString());
					string Nom1 = row["Nom"].ToString() + " " + row["Prenom"].ToString();

					// Patient d'origine : 
					string data = e.Data.GetData(DataFormats.Text).ToString();
					long Indice2 = long.Parse(data.Split(';')[0]);
					string Nom2 = data.Split(';')[1];

					DialogResult result = MessageBox.Show("Voulez-vous rapprocher le patient : \r\n" + Indice2 + " - " + Nom2 + "\r\n au patient :\r\n" + Indice1 + " - " + Nom1 + "?","Rapprochement patient",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button1);
					if(result==DialogResult.Yes)
					{
						OutilsExt.OutilsSql.ExecuteCommandeSansRetour("DELETE from tablerapprochementpatient Where (IdPatient_Parent = " + Indice1 + " And IdPatient_Enfant = " + Indice2 + ") Or (IdPatient_Parent = " + Indice2 + " And IdPatient_Enfant = " + Indice1 + ")");
						OutilsExt.OutilsSql.ExecuteCommandeSansRetour("INSERT INTO tablerapprochementpatient (IdPatient_Parent,IdPatient_Enfant) values (" + Indice1 + "," + Indice2 + ")");

						TraitementDoublons((DataSet)fpListe.Tag);
						ListePatient((DataSet)fpListe.Tag);
						TriDoublon();
					}
				}
			}
		}

		private void btnFactures_Click(object sender, System.EventArgs e)
		{
			if(tabFip.Tag!=null)
			{
                DataRow row = (DataRow)tabFip.Tag;

                this.Cursor = Cursors.Default;

               // SosMedecins.SmartRapport.Facturation.frmDetails z_frmDetails = new SosMedecins.SmartRapport.Facturation.frmDetails(SosMedecins.SmartRapport.Facturation.frmDetails.Mode.Patient, long.Parse(row["IdPatient"].ToString()));
                Facture.frmDetails z_frmDetails = new Facture.frmDetails(Facture.frmDetails.Mode.Patient, long.Parse(row["IdPatient"].ToString()));

                this.Hide();
                this.m_frmgeneral.AjouteFenetreCachee(this);

                z_frmDetails.ShowDialog();
                
			}
		}

		private void FIP_Load(object sender, System.EventArgs e)
		{
            Cursor.Current = Cursors.Default;
            Find_Nom.Focus();
		}

		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
            //Form1 docJ = new Form1(LblIndice.Text);
            //Si on a sélectionné un patient on affiche la form sinon on affiche un message    
            if (LblIndice.Text != "")
            {
                FAjoutDocuments docJ = new FAjoutDocuments(LblIndice.Text);
                docJ.ShowDialog();

                docJ.Dispose();
                docJ = null;
            }
            else
            {
                MessageBox.Show("Veuillez selectionner un patient", "Ajout de document", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
		}

		private void btFacture10_Click(object sender, System.EventArgs e)
		{
			frmTiers frm  = new frmTiers(m_TypeOuvertureDestinataire10);
			frm.ShowDialog();
			DestinataireRetour10 = frm.GetDestinataire();
			lbDestinataire10.Tag = DestinataireRetour10;
			lbDestinataire10.Text = DestinataireRetour10.AdresseDestinataire;
			frm.Dispose();
			frm=null;
        }

        private void BECG_Click(object sender, EventArgs e)
        {
            //On affiche le dernier ECG
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = BECG.Tag.ToString();
            proc.Start();
        }

        private void BAnalyses_Click(object sender, EventArgs e)
        {//On affiche les dernières analyses
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = BAnalyses.Tag.ToString();
            proc.Start();
        }

        private void bautre_Click(object sender, EventArgs e)
        {//On affiche les derniers autres documents
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = bautre.Tag.ToString();
            proc.Start();
        }

        private void btInfoPatient_Click(object sender, EventArgs e)
        {
            //On recherche les infos de ce patient Si on a bien les 20 caractere du n° de la carte ET que ça commence par 80
            if (tBNumCarte.Text.Length == 20 && tBNumCarte.Text.StartsWith("80"))
            {                                
               
                //On initialise les variables de retour
                NomAssure = "";
                PrenomAssure = "";
                DateNaissanceAssure = "";
                AVSAssure = "";
                GenreAssure = "";
                NumAssure = "";               
                
                //On affiche la forme de la réponse et on passe en paramètre le n° de carte
                FInfoAssure fInfoAssure = new FInfoAssure(tBNumCarte.Text);
                fInfoAssure.ShowDialog();

                //Si on a exporté les données de retour
                if (NomAssure != "")
                {
                    //On affecte les nouvelles valeurs
                    TxtNom.Text = NomAssure;
                    TxtPrenom.Text = PrenomAssure;
                    TxtDtNaissance.Text = DateNaissanceAssure; 
                    tBoxNumAVS.Text = AVSAssure;
                    tBoxNunAssure.Text = NumAssure;

                    if (GenreAssure == "F")
                        CbSexe.Text = "F";
                    else CbSexe.Text = "H";
                }                                     

                fInfoAssure.Dispose();  
            }                                                          

        }

        #region controle de saisie
        private void txtEscalier_KeyDown(object sender, KeyEventArgs e)
        {
            TropLong = false;

            if (txtEscalier.Text.Length > 20)
            {
                if (e.KeyCode != Keys.Back)
                {
                    TropLong = true;
                    Console.WriteLine("KO");
                }                
            }
        }

        private void txtEscalier_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Si c'est trop long
            if (TropLong == true)
            {
                //On stop l'entrée du caractère dans le controle
                e.Handled = true;
            }
        }

       
        private void txtDigicode_KeyDown(object sender, KeyEventArgs e)
        {
            TropLong = false;

            if (txtDigicode.Text.Length > 20)
            {
                if (e.KeyCode != Keys.Back)
                {
                    TropLong = true;
                    Console.WriteLine("KO");
                }
            }
        }

        private void txtDigicode_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Si c'est trop long
            if (TropLong == true)
            {
                //On stop l'entrée du caractère dans le controle
                e.Handled = true;
            }
        }

        private void txtInterphone_KeyDown(object sender, KeyEventArgs e)
        {
            TropLong = false;

            if (txtInterphone.Text.Length > 100)
            {
                if (e.KeyCode != Keys.Back)
                {
                    TropLong = true;
                    Console.WriteLine("KO");
                }                
            }
        }

        private void txtInterphone_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Si c'est trop long
            if (TropLong == true)
            {
                //On stop l'entrée du caractère dans le controle
                e.Handled = true;
            }
        }

        private void txtEtage_KeyDown(object sender, KeyEventArgs e)
        {
            TropLong = false;

            if (txtEtage.Text.Length > 20)
            {
                if (e.KeyCode != Keys.Back)
                {
                    TropLong = true;
                    Console.WriteLine("KO");
                }                
            }
        }

        private void txtEtage_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Si c'est trop long
            if (TropLong == true)
            {
                //On stop l'entrée du caractère dans le controle
                e.Handled = true;
            }
        }

        private void txtPorte_KeyDown(object sender, KeyEventArgs e)
        {
            TropLong = false;

            if (txtPorte.Text.Length > 15)
            {
                if (e.KeyCode != Keys.Back)
                {
                    TropLong = true;
                    Console.WriteLine("KO");
                }                
            }
        }
       

        private void txtPorte_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Si c'est trop long
            if (TropLong == true)
            {
                //On stop l'entrée du caractère dans le controle
                e.Handled = true;
            }
        }

        #endregion

        private void bFermer_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        //On ferme mais avant on renvoi les données vers pour policeTarmed
        private void FIP_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (TypedOuverture == "PoliceTarmed")
            {
                ImportSosGeneve.Facture.FPoliceVersTarmed.DestinataireFacture = LblDestinataireFacture.Text;               
            }
        }

        //Après la fermeture, on oubli pas de réinitialiser TypedOuverture
        private void FIP_FormClosed(object sender, FormClosedEventArgs e)
        {
            TypedOuverture = "";
        }       

    }
}


/*A faire:
//Mettre le webservice en asynchrone

 tester controle de saisie
*/