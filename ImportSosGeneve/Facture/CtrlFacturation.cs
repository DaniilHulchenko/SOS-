using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.Odbc;
using System.Windows.Forms;
using SosMedecins.SmartRapport.GestionApplication;
using SosMedecins.Connexion;
using SosMedecins.SmartRapport.DAL;
using System.Data.SqlClient;
using System.Configuration;

using System.IO;
using ImportSosGeneve.Facture;
//using System.Drawing.Drawing2D;

namespace ImportSosGeneve
{
	// **********************************************************
	// Controle de formulaire de facturation
	// **********************************************************
	public class CtrlFacturation : System.Windows.Forms.UserControl
	{

		#region Déclaration des variables

		// Variables globales
        private bool chargeListe = false;
                
        private frmGeneral _frmgeneral=null;
		// Data Row contenant l'appel
		private DataRow m_datarowAppel = null;
        private DataRow[] m_FactureActuelle=null;
        private DataRow _drwFacture = null;

        private Hashtable DocumentEnCours = new Hashtable();
		// Séparateur décimal : 
		private System.Globalization.NumberFormatInfo FF = new System.Globalization.NumberFormatInfo();

		private GradientCellType Gradient1 = new GradientCellType();
		private GradientCellType Gradient2 = new GradientCellType();
		//private string s;
		private CtrlDest.TypeOuverture m_TypeOuverture = 0;
		public Dest DestinataireRetour=null;
		private bool ChargementFactureTermine = false;
        
        // ligne patient_remarque
        private DataTable _dtbPatient_Remarque;

        private Double scale_factor_mt = 1;   //Facteur scalaire scale_factor_mt
        private String TarmedVersion = "LAMAL";

        public static string AdrPourPoliceTarmed = "";
        public static string TarifPourPoliceTarmed = "";
        #endregion

        #region Variables controles du formulaire
        private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label lblDateRappel;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.GroupBox groupBox7;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.Label label25;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.ComboBox cbEnvoi;
		private System.Windows.Forms.ComboBox cbTTT;
		private System.Windows.Forms.ComboBox cbTypeAss;
		private System.Windows.Forms.TextBox txtAccident;
		private System.Windows.Forms.TextBox txtDateAcc;
		private System.Windows.Forms.TextBox txtRef;
		private System.Windows.Forms.ComboBox cbTarif;
		private System.Windows.Forms.GroupBox gpC;
		private System.Windows.Forms.CheckBox chkFlagConcerne;
		private System.Windows.Forms.ComboBox cbSortie;
		private System.Windows.Forms.GroupBox gpA;
		private System.Windows.Forms.RadioButton rdNConsultation;
		private System.Windows.Forms.RadioButton rdNFacture;
		private System.Windows.Forms.TextBox txtNConsultation;
		private System.Windows.Forms.TextBox txtNFacture;
		private System.Windows.Forms.TextBox txtTypeDestinataire;
		private System.Windows.Forms.TextBox txtNomMedecin;
		private System.Windows.Forms.TextBox txtNomPatient;
		private System.Windows.Forms.GroupBox gpD;
        private System.Windows.Forms.CheckBox chkEncaisse;
        private System.Windows.Forms.CheckBox chkAnnule;
		private System.Windows.Forms.CheckBox chkAcquitte;
		private System.Windows.Forms.CheckBox chkEnvoye;
		private System.Windows.Forms.GroupBox gpB;
		private System.Windows.Forms.TextBox txtDestinataire;
		private System.Windows.Forms.GroupBox gpE;
		private System.Windows.Forms.TextBox TxtDateRappel;
		private System.Windows.Forms.TextBox txtDateImpression;
		private System.Windows.Forms.TextBox txtDateCreation;
		private System.Windows.Forms.TextBox TxtTotalFacture;
		private System.Windows.Forms.RadioButton rdMat;
		private System.Windows.Forms.RadioButton rdPrest;
		private System.Windows.Forms.TextBox txtCote;
		private System.Windows.Forms.TextBox txtQte;
		private System.Windows.Forms.TextBox txtCoeff;
		private System.Windows.Forms.PictureBox btnPrestationOk;
		private System.Windows.Forms.PictureBox btnSave;
        private System.Windows.Forms.PictureBox btnDelete;
		private System.Windows.Forms.PictureBox btnNew;
		private System.Windows.Forms.PictureBox btnHisto;
		private System.Windows.Forms.GroupBox gpF;
		private System.Windows.Forms.GroupBox gpG;
		private System.Windows.Forms.PictureBox btnQuitter;
		private System.Windows.Forms.PictureBox btnImpr;
		private System.Windows.Forms.GroupBox gpI;
		private System.Windows.Forms.PictureBox btnNouveauDestinataire;
		private System.Windows.Forms.GroupBox gpFonctions;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.ListBox lstConsultationsPourFacture;
		private System.Windows.Forms.PictureBox BtnFindFacture;
		private FarPoint.Win.Spread.FpSpread fpFactures;
		private System.Windows.Forms.TextBox txtCommentaire;
		private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private FarPoint.Win.Spread.SheetView fpFactures_Sheet1;
		private System.Windows.Forms.ComboBox cbDocJoint;
		private System.Windows.Forms.TextBox txtPrix;
		private System.Windows.Forms.TextBox txtLibelle;
		private System.Windows.Forms.TextBox TxtCode;
		private System.Windows.Forms.ListView lwResultats;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.Label lbStatusOp;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TextBox txtdateN;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Button btDupPolice;
		private System.Windows.Forms.TextBox txtDateEnc;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Button b10Poucent;
		private System.Windows.Forms.TextBox tbDestinataire10;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.TextBox txtDateImpression10;
        private GroupBox grpArrangement;
        private TextBox txtArrangement;
        private Button btnArrangement;
        private Label lblArrangementPar;
        private Label lblArrangementLe;
        private TextBox txtArrangementUser;
        private TextBox txtArrangementDate;
        private GroupBox grpContentieux;
        private CheckBox chkCessionCreance;
        private CheckBox chkSurPlace;
        private GroupBox grpRappels;
        private TextBox TxtDateContentieux;
        private GroupBox grpCeder;
        private CheckBox chkCession;
        private SosMedecins.Controls.sosDateBox dbxCession;
        private Label labelInfoRapport;
        private PictureBox btnModif;
        private PictureBox PBoxCession;
        private GroupBox groupBox1;
        private CheckBox CBoxIndelicat;
        private CheckBox CBoxRenvoiFranchise;
        private CheckBox CBoxRenvoiFact10;
        private CheckBox CBCessionRecu;
        private TextBox TBoxDateReceptionCession;
        private TextBox TBoxDateEnvoiCession;
        private Label label17;
        private TextBox TBDateStopRappel;
        private Label label23;
        private CheckBox CBEffacerDateSession;
        private Label label27;
        private TextBox txtAVS;
        private ZoomImageViewer zoomImageViewer1;
        private Button bRotationImage;
        private Button bAjoutDoc;
        private ImageList imageList1;
        private Button bDuplicata;
        private TextBox TxtSolde;
        private Label label28;
        private Label label20;
        private Label lblDateContentieux;
		//private FarPoint.Win.Spread.Model.CellRange cr;
		
		#endregion

		#region Construction / Destruction de la classe

		// *************************************************
		// Constructeur
		// *************************************************
		public CtrlFacturation(frmGeneral frm)
		{
			InitializeComponent();             	            
			InitializeCtrlNavig();
			this._frmgeneral = frm;	
			InitializeControls();	
			BlocageControles(true);
			InitializeData();

            this.zoomImageViewer1.MouseWheel += new MouseEventHandler(zoomImageViewer1_MouseWheel);       //Domi 03.04.2013...Déclaration d'un évènement _MouseWheel
		}

        // *************************************************
        // Constructeur
        // *************************************************

        //Si une facture existe on va l'afficher, sinon on la créer ICI
        public CtrlFacturation(frmGeneral frm,DataRow rowConsultation)
		{
			InitializeComponent();
			InitializeCtrlNavig();
			this._frmgeneral = frm;	
			this.m_datarowAppel = rowConsultation;
			InitializeControls();	
			InitializeData();

            this.zoomImageViewer1.MouseWheel += new MouseEventHandler(zoomImageViewer1_MouseWheel);      //Domi 03.04.2013...Déclaration d'un évènement _MouseWheel                                     

            //On recupère le facteur scalaire Scale_factor_mt du médecin mais attention a changer par la suite en fct du tarif
            double Retour = OutilsExt.OutilsSql.Val_Scale_factor_mt(long.Parse(rowConsultation["NConsultation"].ToString()));   //Domi 06.10.2017

            scale_factor_mt = Retour;
            TarmedVersion = "LAMAL";   //Par défaut

			// Y a-t-il une facture faite?
            //Si non on bloque tous les controles jusqu'au clic sur "Nouvelle"  Sinon c'est oui on l'affiche, 
			long NFacture =  OutilsExt.OutilsSql.NFactureByConsult(long.Parse(rowConsultation["NConsultation"].ToString()));
			
            //Pas de facture existante pour cette consultation donc on en créer une de toute pièce
            if(NFacture == -1)
			{
				btnNew.Visible = true;
				//btnSave.Visible = true;
				btnHisto.Visible = false;
               // btnModif.Visible = false;
				btnDelete.Visible = false;
				BlocageControles(true);

                //appel automatique du bouton de création
				btnNew_Click(null,null);                
			}
			else
			{				
				//elle existe déjà
                btnNew.Visible = false;

				//btnSave.Visible = false;
               // btnModif.Visible = true;
				btnHisto.Visible = true;
				btnDelete.Visible = true;

                DataRow[] z_drw = OutilsExt.OutilsSql.RecuperationFacturesByNFacture(NFacture);
                if (z_drw != null && z_drw.Length > 0)
                {
                    _drwFacture = z_drw[0];                   
                }
                else
                {
                    _drwFacture = null;
                }

                MiseEnPlaceConsultation(m_datarowAppel);
                MiseEnPlaceFacture(_drwFacture);
				BlocageControles(false);
			}
		}


        // *************************************************
        // Constructeur
        // *************************************************
        public CtrlFacturation(DataRow rowConsultation)
        {
            InitializeComponent();
            InitializeCtrlNavig();

            this.m_datarowAppel = rowConsultation;
            InitializeControls();
            InitializeData();

            this.zoomImageViewer1.MouseWheel += new MouseEventHandler(zoomImageViewer1_MouseWheel);       //Domi 03.04.2013...Déclaration d'un évènement _MouseWheel

            //On recupère le facteur scalaire Scale_factor_mt du médecin mais attention a changer par la suite en fct du tarif
            double Retour = OutilsExt.OutilsSql.Val_Scale_factor_mt(long.Parse(rowConsultation["NConsultation"].ToString()));   //Domi 06.10.2017
            
            scale_factor_mt = Retour;
            TarmedVersion = "LAMAL";

            // Y a-t-il une facture faite?
            // Si oui on l'affiche, sinon on bloque tous les controles jusqu'au clic sur "Nouvelle"
           // long NFacture = OutilsExt.OutilsSql.NFactureByConsult(long.Parse(rowConsultation["NConsultation"].ToString()));
            long NFacture = long.Parse(rowConsultation["NFacture"].ToString());
            
            if (NFacture == -1)
            {
                btnNew.Visible = true;
                //btnSave.Visible = true;
                btnHisto.Visible = false;
                // btnModif.Visible = false;
                btnDelete.Visible = false;
                BlocageControles(true);
                btnNew_Click(null, null);
            }
            else
            {
                btnNew.Visible = false;
                //btnSave.Visible = false;
                // btnModif.Visible = true;
                btnHisto.Visible = true;
                btnDelete.Visible = true;

                DataRow[] z_drw = OutilsExt.OutilsSql.RecuperationFacturesByNFacture(NFacture);
                if (z_drw != null && z_drw.Length > 0)
                {
                    _drwFacture = z_drw[0];                   
                }
                else
                {
                    _drwFacture = null;
                }
                MiseEnPlaceConsultation(m_datarowAppel);
                MiseEnPlaceFacture(_drwFacture);
                BlocageControles(false);
            }
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

		#region Mise en place des controles

		#region Code généré par le Concepteur de composants
		/// <summary> 
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CtrlFacturation));
            this.cbEnvoi = new System.Windows.Forms.ComboBox();
            this.cbTarif = new System.Windows.Forms.ComboBox();
            this.cbTTT = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gpC = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.chkFlagConcerne = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbDocJoint = new System.Windows.Forms.ComboBox();
            this.txtRef = new System.Windows.Forms.TextBox();
            this.cbSortie = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbTypeAss = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAccident = new System.Windows.Forms.TextBox();
            this.txtDateAcc = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.gpA = new System.Windows.Forms.GroupBox();
            this.lstConsultationsPourFacture = new System.Windows.Forms.ListBox();
            this.label9 = new System.Windows.Forms.Label();
            this.BtnFindFacture = new System.Windows.Forms.PictureBox();
            this.rdNConsultation = new System.Windows.Forms.RadioButton();
            this.rdNFacture = new System.Windows.Forms.RadioButton();
            this.txtNConsultation = new System.Windows.Forms.TextBox();
            this.txtNFacture = new System.Windows.Forms.TextBox();
            this.txtTypeDestinataire = new System.Windows.Forms.TextBox();
            this.txtNomMedecin = new System.Windows.Forms.TextBox();
            this.txtNomPatient = new System.Windows.Forms.TextBox();
            this.gpD = new System.Windows.Forms.GroupBox();
            this.chkEncaisse = new System.Windows.Forms.CheckBox();
            this.chkAnnule = new System.Windows.Forms.CheckBox();
            this.chkAcquitte = new System.Windows.Forms.CheckBox();
            this.chkEnvoye = new System.Windows.Forms.CheckBox();
            this.gpB = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtdateN = new System.Windows.Forms.TextBox();
            this.txtDestinataire = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.btnNouveauDestinataire = new System.Windows.Forms.PictureBox();
            this.gpE = new System.Windows.Forms.GroupBox();
            this.label27 = new System.Windows.Forms.Label();
            this.txtAVS = new System.Windows.Forms.TextBox();
            this.txtDateImpression10 = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.txtDateImpression = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtDateCreation = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtDateEnc = new System.Windows.Forms.TextBox();
            this.TxtDateRappel = new System.Windows.Forms.TextBox();
            this.lblDateRappel = new System.Windows.Forms.Label();
            this.gpF = new System.Windows.Forms.GroupBox();
            this.label28 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.TxtSolde = new System.Windows.Forms.TextBox();
            this.txtCommentaire = new System.Windows.Forms.TextBox();
            this.TxtTotalFacture = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.lwResultats = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TxtCode = new System.Windows.Forms.TextBox();
            this.txtPrix = new System.Windows.Forms.TextBox();
            this.txtLibelle = new System.Windows.Forms.TextBox();
            this.fpFactures = new FarPoint.Win.Spread.FpSpread();
            this.fpFactures_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.txtCoeff = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.gpG = new System.Windows.Forms.GroupBox();
            this.rdMat = new System.Windows.Forms.RadioButton();
            this.rdPrest = new System.Windows.Forms.RadioButton();
            this.btnPrestationOk = new System.Windows.Forms.PictureBox();
            this.txtCote = new System.Windows.Forms.TextBox();
            this.txtQte = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.gpFonctions = new System.Windows.Forms.GroupBox();
            this.btnModif = new System.Windows.Forms.PictureBox();
            this.btnSave = new System.Windows.Forms.PictureBox();
            this.btnDelete = new System.Windows.Forms.PictureBox();
            this.btnNew = new System.Windows.Forms.PictureBox();
            this.btnHisto = new System.Windows.Forms.PictureBox();
            this.gpI = new System.Windows.Forms.GroupBox();
            this.btnQuitter = new System.Windows.Forms.PictureBox();
            this.btnImpr = new System.Windows.Forms.PictureBox();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.lbStatusOp = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.bRotationImage = new System.Windows.Forms.Button();
            this.TBDateStopRappel = new System.Windows.Forms.TextBox();
            this.btDupPolice = new System.Windows.Forms.Button();
            this.b10Poucent = new System.Windows.Forms.Button();
            this.tbDestinataire10 = new System.Windows.Forms.TextBox();
            this.grpArrangement = new System.Windows.Forms.GroupBox();
            this.lblArrangementPar = new System.Windows.Forms.Label();
            this.lblArrangementLe = new System.Windows.Forms.Label();
            this.txtArrangementUser = new System.Windows.Forms.TextBox();
            this.txtArrangementDate = new System.Windows.Forms.TextBox();
            this.txtArrangement = new System.Windows.Forms.TextBox();
            this.btnArrangement = new System.Windows.Forms.Button();
            this.grpContentieux = new System.Windows.Forms.GroupBox();
            this.chkCessionCreance = new System.Windows.Forms.CheckBox();
            this.chkSurPlace = new System.Windows.Forms.CheckBox();
            this.grpRappels = new System.Windows.Forms.GroupBox();
            this.TxtDateContentieux = new System.Windows.Forms.TextBox();
            this.lblDateContentieux = new System.Windows.Forms.Label();
            this.grpCeder = new System.Windows.Forms.GroupBox();
            this.dbxCession = new SosMedecins.Controls.sosDateBox();
            this.chkCession = new System.Windows.Forms.CheckBox();
            this.labelInfoRapport = new System.Windows.Forms.Label();
            this.PBoxCession = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CBEffacerDateSession = new System.Windows.Forms.CheckBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.TBoxDateEnvoiCession = new System.Windows.Forms.TextBox();
            this.TBoxDateReceptionCession = new System.Windows.Forms.TextBox();
            this.CBoxIndelicat = new System.Windows.Forms.CheckBox();
            this.CBoxRenvoiFranchise = new System.Windows.Forms.CheckBox();
            this.CBoxRenvoiFact10 = new System.Windows.Forms.CheckBox();
            this.CBCessionRecu = new System.Windows.Forms.CheckBox();
            this.zoomImageViewer1 = new ImportSosGeneve.ZoomImageViewer();
            this.bAjoutDoc = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.bDuplicata = new System.Windows.Forms.Button();
            this.gpC.SuspendLayout();
            this.gpA.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BtnFindFacture)).BeginInit();
            this.gpD.SuspendLayout();
            this.gpB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnNouveauDestinataire)).BeginInit();
            this.gpE.SuspendLayout();
            this.gpF.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpFactures)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpFactures_Sheet1)).BeginInit();
            this.gpG.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrestationOk)).BeginInit();
            this.gpFonctions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnModif)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnHisto)).BeginInit();
            this.gpI.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnQuitter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnImpr)).BeginInit();
            this.grpArrangement.SuspendLayout();
            this.grpContentieux.SuspendLayout();
            this.grpRappels.SuspendLayout();
            this.grpCeder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PBoxCession)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbEnvoi
            // 
            this.cbEnvoi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEnvoi.Items.AddRange(new object[] {
            "Privé",
            "Tiers",
            "Assurance"});
            this.cbEnvoi.Location = new System.Drawing.Point(8, 24);
            this.cbEnvoi.Name = "cbEnvoi";
            this.cbEnvoi.Size = new System.Drawing.Size(112, 22);
            this.cbEnvoi.TabIndex = 8;
            this.cbEnvoi.SelectedIndexChanged += new System.EventHandler(this.cbEnvoi_SelectedIndexChanged);
            // 
            // cbTarif
            // 
            this.cbTarif.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTarif.Items.AddRange(new object[] {
            "TarmedCant",
            "TarmedFed",
            "Cadre",
            "Usage"});
            this.cbTarif.Location = new System.Drawing.Point(128, 24);
            this.cbTarif.Name = "cbTarif";
            this.cbTarif.Size = new System.Drawing.Size(120, 22);
            this.cbTarif.TabIndex = 9;
            this.cbTarif.SelectedIndexChanged += new System.EventHandler(this.cbTarif_SelectedIndexChanged);
            // 
            // cbTTT
            // 
            this.cbTTT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTTT.Items.AddRange(new object[] {
            "Maladie",
            "Accident",
            "Examen"});
            this.cbTTT.Location = new System.Drawing.Point(8, 64);
            this.cbTTT.Name = "cbTTT";
            this.cbTTT.Size = new System.Drawing.Size(96, 22);
            this.cbTTT.TabIndex = 13;
            this.cbTTT.SelectedIndexChanged += new System.EventHandler(this.cbTTT_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "Envoi :";
            // 
            // gpC
            // 
            this.gpC.BackColor = System.Drawing.Color.CadetBlue;
            this.gpC.Controls.Add(this.label16);
            this.gpC.Controls.Add(this.chkFlagConcerne);
            this.gpC.Controls.Add(this.label8);
            this.gpC.Controls.Add(this.cbDocJoint);
            this.gpC.Controls.Add(this.txtRef);
            this.gpC.Controls.Add(this.cbSortie);
            this.gpC.Controls.Add(this.label7);
            this.gpC.Controls.Add(this.label6);
            this.gpC.Controls.Add(this.cbTypeAss);
            this.gpC.Controls.Add(this.label5);
            this.gpC.Controls.Add(this.label4);
            this.gpC.Controls.Add(this.label3);
            this.gpC.Controls.Add(this.txtAccident);
            this.gpC.Controls.Add(this.txtDateAcc);
            this.gpC.Controls.Add(this.label2);
            this.gpC.Controls.Add(this.label1);
            this.gpC.Controls.Add(this.cbEnvoi);
            this.gpC.Controls.Add(this.cbTarif);
            this.gpC.Controls.Add(this.cbTTT);
            this.gpC.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpC.ForeColor = System.Drawing.SystemColors.MenuText;
            this.gpC.Location = new System.Drawing.Point(448, 0);
            this.gpC.Name = "gpC";
            this.gpC.Size = new System.Drawing.Size(256, 190);
            this.gpC.TabIndex = 3;
            this.gpC.TabStop = false;
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(128, 116);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(72, 16);
            this.label16.TabIndex = 19;
            this.label16.Text = "Réf Patient :";
            // 
            // chkFlagConcerne
            // 
            this.chkFlagConcerne.Location = new System.Drawing.Point(158, 166);
            this.chkFlagConcerne.Name = "chkFlagConcerne";
            this.chkFlagConcerne.Size = new System.Drawing.Size(80, 16);
            this.chkFlagConcerne.TabIndex = 17;
            this.chkFlagConcerne.Text = "Concerne";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(8, 166);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 16);
            this.label8.TabIndex = 16;
            this.label8.Text = "Doc.joint: :";
            // 
            // cbDocJoint
            // 
            this.cbDocJoint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDocJoint.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDocJoint.ItemHeight = 14;
            this.cbDocJoint.Location = new System.Drawing.Point(56, 160);
            this.cbDocJoint.Name = "cbDocJoint";
            this.cbDocJoint.Size = new System.Drawing.Size(96, 22);
            this.cbDocJoint.TabIndex = 15;
            // 
            // txtRef
            // 
            this.txtRef.Location = new System.Drawing.Point(128, 131);
            this.txtRef.Name = "txtRef";
            this.txtRef.Size = new System.Drawing.Size(120, 20);
            this.txtRef.TabIndex = 14;
            this.txtRef.TextChanged += new System.EventHandler(this.txtRef_TextChanged);
            // 
            // cbSortie
            // 
            this.cbSortie.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSortie.Items.AddRange(new object[] {
            "Imprimante",
            "Xml",
            "Email"});
            this.cbSortie.Location = new System.Drawing.Point(8, 131);
            this.cbSortie.Name = "cbSortie";
            this.cbSortie.Size = new System.Drawing.Size(112, 22);
            this.cbSortie.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(8, 112);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 16);
            this.label7.TabIndex = 12;
            this.label7.Text = "Sortie :";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 93);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(106, 16);
            this.label6.TabIndex = 16;
            this.label6.Text = "Type d\' assurance :";
            // 
            // cbTypeAss
            // 
            this.cbTypeAss.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTypeAss.Items.AddRange(new object[] {
            "Maladie",
            "Accident"});
            this.cbTypeAss.Location = new System.Drawing.Point(120, 91);
            this.cbTypeAss.Name = "cbTypeAss";
            this.cbTypeAss.Size = new System.Drawing.Size(128, 22);
            this.cbTypeAss.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(184, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 16);
            this.label5.TabIndex = 12;
            this.label5.Text = "N° Accident";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.label4.Location = new System.Drawing.Point(128, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "Tarif :";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(104, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 16);
            this.label3.TabIndex = 11;
            this.label3.Text = "Date Accident";
            // 
            // txtAccident
            // 
            this.txtAccident.Location = new System.Drawing.Point(176, 64);
            this.txtAccident.Name = "txtAccident";
            this.txtAccident.Size = new System.Drawing.Size(72, 20);
            this.txtAccident.TabIndex = 15;
            // 
            // txtDateAcc
            // 
            this.txtDateAcc.Location = new System.Drawing.Point(104, 64);
            this.txtDateAcc.Name = "txtDateAcc";
            this.txtDateAcc.Size = new System.Drawing.Size(72, 20);
            this.txtDateAcc.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 16);
            this.label2.TabIndex = 10;
            this.label2.Text = "TTT :";
            // 
            // gpA
            // 
            this.gpA.BackColor = System.Drawing.Color.CadetBlue;
            this.gpA.Controls.Add(this.lstConsultationsPourFacture);
            this.gpA.Controls.Add(this.label9);
            this.gpA.Controls.Add(this.BtnFindFacture);
            this.gpA.Controls.Add(this.rdNConsultation);
            this.gpA.Controls.Add(this.rdNFacture);
            this.gpA.Controls.Add(this.txtNConsultation);
            this.gpA.Controls.Add(this.txtNFacture);
            this.gpA.Location = new System.Drawing.Point(0, 0);
            this.gpA.Name = "gpA";
            this.gpA.Size = new System.Drawing.Size(216, 190);
            this.gpA.TabIndex = 0;
            this.gpA.TabStop = false;
            // 
            // lstConsultationsPourFacture
            // 
            this.lstConsultationsPourFacture.Location = new System.Drawing.Point(8, 120);
            this.lstConsultationsPourFacture.Name = "lstConsultationsPourFacture";
            this.lstConsultationsPourFacture.Size = new System.Drawing.Size(168, 56);
            this.lstConsultationsPourFacture.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(8, 96);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(192, 16);
            this.label9.TabIndex = 4;
            this.label9.Text = "Autres consultations pour la facture :";
            // 
            // BtnFindFacture
            // 
            this.BtnFindFacture.Image = ((System.Drawing.Image)(resources.GetObject("BtnFindFacture.Image")));
            this.BtnFindFacture.Location = new System.Drawing.Point(8, 64);
            this.BtnFindFacture.Name = "BtnFindFacture";
            this.BtnFindFacture.Size = new System.Drawing.Size(32, 24);
            this.BtnFindFacture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.BtnFindFacture.TabIndex = 60;
            this.BtnFindFacture.TabStop = false;
            this.BtnFindFacture.Click += new System.EventHandler(this.BtnFindFacture_Click);
            // 
            // rdNConsultation
            // 
            this.rdNConsultation.Location = new System.Drawing.Point(104, 16);
            this.rdNConsultation.Name = "rdNConsultation";
            this.rdNConsultation.Size = new System.Drawing.Size(80, 24);
            this.rdNConsultation.TabIndex = 1;
            this.rdNConsultation.Text = "N° Consult";
            // 
            // rdNFacture
            // 
            this.rdNFacture.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdNFacture.Location = new System.Drawing.Point(8, 16);
            this.rdNFacture.Name = "rdNFacture";
            this.rdNFacture.Size = new System.Drawing.Size(88, 24);
            this.rdNFacture.TabIndex = 0;
            this.rdNFacture.Text = "N° Facture";
            // 
            // txtNConsultation
            // 
            this.txtNConsultation.Enabled = false;
            this.txtNConsultation.Location = new System.Drawing.Point(104, 40);
            this.txtNConsultation.Name = "txtNConsultation";
            this.txtNConsultation.Size = new System.Drawing.Size(96, 20);
            this.txtNConsultation.TabIndex = 3;
            this.txtNConsultation.TextChanged += new System.EventHandler(this.txtNConsultation_TextChanged);
            this.txtNConsultation.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtNConsultation_KeyUp);
            // 
            // txtNFacture
            // 
            this.txtNFacture.Enabled = false;
            this.txtNFacture.Location = new System.Drawing.Point(8, 40);
            this.txtNFacture.Name = "txtNFacture";
            this.txtNFacture.Size = new System.Drawing.Size(88, 20);
            this.txtNFacture.TabIndex = 2;
            this.txtNFacture.TextChanged += new System.EventHandler(this.txtNFacture_TextChanged);
            this.txtNFacture.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtNConsultation_KeyUp);
            // 
            // txtTypeDestinataire
            // 
            this.txtTypeDestinataire.Enabled = false;
            this.txtTypeDestinataire.Location = new System.Drawing.Point(90, 98);
            this.txtTypeDestinataire.Name = "txtTypeDestinataire";
            this.txtTypeDestinataire.Size = new System.Drawing.Size(80, 20);
            this.txtTypeDestinataire.TabIndex = 7;
            this.txtTypeDestinataire.Text = "Idem";
            // 
            // txtNomMedecin
            // 
            this.txtNomMedecin.Location = new System.Drawing.Point(56, 16);
            this.txtNomMedecin.Name = "txtNomMedecin";
            this.txtNomMedecin.Size = new System.Drawing.Size(176, 20);
            this.txtNomMedecin.TabIndex = 1;
            // 
            // txtNomPatient
            // 
            this.txtNomPatient.Location = new System.Drawing.Point(56, 40);
            this.txtNomPatient.Name = "txtNomPatient";
            this.txtNomPatient.Size = new System.Drawing.Size(176, 20);
            this.txtNomPatient.TabIndex = 3;
            // 
            // gpD
            // 
            this.gpD.BackColor = System.Drawing.Color.CadetBlue;
            this.gpD.Controls.Add(this.chkEncaisse);
            this.gpD.Controls.Add(this.chkAnnule);
            this.gpD.Controls.Add(this.chkAcquitte);
            this.gpD.Controls.Add(this.chkEnvoye);
            this.gpD.Location = new System.Drawing.Point(704, 0);
            this.gpD.Name = "gpD";
            this.gpD.Size = new System.Drawing.Size(116, 118);
            this.gpD.TabIndex = 7;
            this.gpD.TabStop = false;
            // 
            // chkEncaisse
            // 
            this.chkEncaisse.AutoSize = true;
            this.chkEncaisse.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEncaisse.Location = new System.Drawing.Point(10, 67);
            this.chkEncaisse.Name = "chkEncaisse";
            this.chkEncaisse.Size = new System.Drawing.Size(70, 18);
            this.chkEncaisse.TabIndex = 7;
            this.chkEncaisse.Text = "Encaissé";
            // 
            // chkAnnule
            // 
            this.chkAnnule.AutoSize = true;
            this.chkAnnule.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAnnule.Location = new System.Drawing.Point(10, 91);
            this.chkAnnule.Name = "chkAnnule";
            this.chkAnnule.Size = new System.Drawing.Size(60, 18);
            this.chkAnnule.TabIndex = 3;
            this.chkAnnule.Text = "Annulé";
            this.chkAnnule.CheckedChanged += new System.EventHandler(this.chkAnnule_CheckedChanged);
            // 
            // chkAcquitte
            // 
            this.chkAcquitte.AutoSize = true;
            this.chkAcquitte.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAcquitte.Location = new System.Drawing.Point(10, 43);
            this.chkAcquitte.Name = "chkAcquitte";
            this.chkAcquitte.Size = new System.Drawing.Size(66, 18);
            this.chkAcquitte.TabIndex = 1;
            this.chkAcquitte.Text = "Acquitté";
            // 
            // chkEnvoye
            // 
            this.chkEnvoye.AutoSize = true;
            this.chkEnvoye.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEnvoye.Location = new System.Drawing.Point(10, 19);
            this.chkEnvoye.Name = "chkEnvoye";
            this.chkEnvoye.Size = new System.Drawing.Size(62, 18);
            this.chkEnvoye.TabIndex = 0;
            this.chkEnvoye.Text = "Envoyé";
            // 
            // gpB
            // 
            this.gpB.BackColor = System.Drawing.Color.CadetBlue;
            this.gpB.Controls.Add(this.label12);
            this.gpB.Controls.Add(this.txtdateN);
            this.gpB.Controls.Add(this.txtDestinataire);
            this.gpB.Controls.Add(this.label15);
            this.gpB.Controls.Add(this.label14);
            this.gpB.Controls.Add(this.label13);
            this.gpB.Controls.Add(this.txtNomPatient);
            this.gpB.Controls.Add(this.txtNomMedecin);
            this.gpB.Controls.Add(this.txtTypeDestinataire);
            this.gpB.Controls.Add(this.btnNouveauDestinataire);
            this.gpB.Location = new System.Drawing.Point(208, 0);
            this.gpB.Name = "gpB";
            this.gpB.Size = new System.Drawing.Size(240, 190);
            this.gpB.TabIndex = 1;
            this.gpB.TabStop = false;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(8, 68);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(104, 16);
            this.label12.TabIndex = 4;
            this.label12.Text = "Date de naissance";
            // 
            // txtdateN
            // 
            this.txtdateN.Location = new System.Drawing.Point(112, 64);
            this.txtdateN.Name = "txtdateN";
            this.txtdateN.Size = new System.Drawing.Size(120, 20);
            this.txtdateN.TabIndex = 5;
            // 
            // txtDestinataire
            // 
            this.txtDestinataire.BackColor = System.Drawing.SystemColors.Window;
            this.txtDestinataire.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDestinataire.Location = new System.Drawing.Point(10, 124);
            this.txtDestinataire.Multiline = true;
            this.txtDestinataire.Name = "txtDestinataire";
            this.txtDestinataire.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDestinataire.Size = new System.Drawing.Size(224, 64);
            this.txtDestinataire.TabIndex = 8;
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(8, 98);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(72, 16);
            this.label15.TabIndex = 6;
            this.label15.Text = "Destinataire: :";
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(8, 16);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(48, 16);
            this.label14.TabIndex = 0;
            this.label14.Text = "Médecin :";
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(8, 40);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(40, 16);
            this.label13.TabIndex = 2;
            this.label13.Text = "Patient :";
            // 
            // btnNouveauDestinataire
            // 
            this.btnNouveauDestinataire.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnNouveauDestinataire.Image = ((System.Drawing.Image)(resources.GetObject("btnNouveauDestinataire.Image")));
            this.btnNouveauDestinataire.Location = new System.Drawing.Point(176, 91);
            this.btnNouveauDestinataire.Name = "btnNouveauDestinataire";
            this.btnNouveauDestinataire.Size = new System.Drawing.Size(56, 32);
            this.btnNouveauDestinataire.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnNouveauDestinataire.TabIndex = 55;
            this.btnNouveauDestinataire.TabStop = false;
            this.btnNouveauDestinataire.Click += new System.EventHandler(this.btnNouveauDestinataire_Click);
            // 
            // gpE
            // 
            this.gpE.BackColor = System.Drawing.Color.CadetBlue;
            this.gpE.Controls.Add(this.label27);
            this.gpE.Controls.Add(this.txtAVS);
            this.gpE.Controls.Add(this.txtDateImpression10);
            this.gpE.Controls.Add(this.label21);
            this.gpE.Controls.Add(this.label18);
            this.gpE.Controls.Add(this.txtDateImpression);
            this.gpE.Controls.Add(this.label11);
            this.gpE.Controls.Add(this.txtDateCreation);
            this.gpE.Controls.Add(this.label10);
            this.gpE.Controls.Add(this.txtDateEnc);
            this.gpE.Location = new System.Drawing.Point(932, 326);
            this.gpE.Name = "gpE";
            this.gpE.Size = new System.Drawing.Size(198, 177);
            this.gpE.TabIndex = 9;
            this.gpE.TabStop = false;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(17, 31);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(49, 14);
            this.label27.TabIndex = 44;
            this.label27.Text = "N° AVS :";
            // 
            // txtAVS
            // 
            this.txtAVS.Location = new System.Drawing.Point(72, 25);
            this.txtAVS.Name = "txtAVS";
            this.txtAVS.Size = new System.Drawing.Size(120, 20);
            this.txtAVS.TabIndex = 43;
            // 
            // txtDateImpression10
            // 
            this.txtDateImpression10.Enabled = false;
            this.txtDateImpression10.Location = new System.Drawing.Point(112, 150);
            this.txtDateImpression10.Name = "txtDateImpression10";
            this.txtDateImpression10.Size = new System.Drawing.Size(80, 20);
            this.txtDateImpression10.TabIndex = 42;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(21, 153);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(72, 14);
            this.label21.TabIndex = 41;
            this.label21.Text = "Facture 10%";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(21, 94);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(72, 14);
            this.label18.TabIndex = 40;
            this.label18.Text = "Encaissé le:";
            // 
            // txtDateImpression
            // 
            this.txtDateImpression.Enabled = false;
            this.txtDateImpression.Location = new System.Drawing.Point(112, 124);
            this.txtDateImpression.Name = "txtDateImpression";
            this.txtDateImpression.Size = new System.Drawing.Size(80, 20);
            this.txtDateImpression.TabIndex = 35;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(21, 128);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(80, 14);
            this.label11.TabIndex = 34;
            this.label11.Text = "Imprimée le :";
            // 
            // txtDateCreation
            // 
            this.txtDateCreation.Enabled = false;
            this.txtDateCreation.Location = new System.Drawing.Point(93, 58);
            this.txtDateCreation.Name = "txtDateCreation";
            this.txtDateCreation.Size = new System.Drawing.Size(99, 20);
            this.txtDateCreation.TabIndex = 33;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(18, 61);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(60, 14);
            this.label10.TabIndex = 32;
            this.label10.Text = "Créée le :";
            // 
            // txtDateEnc
            // 
            this.txtDateEnc.Location = new System.Drawing.Point(112, 91);
            this.txtDateEnc.Name = "txtDateEnc";
            this.txtDateEnc.Size = new System.Drawing.Size(80, 20);
            this.txtDateEnc.TabIndex = 39;
            // 
            // TxtDateRappel
            // 
            this.TxtDateRappel.Location = new System.Drawing.Point(151, 13);
            this.TxtDateRappel.Name = "TxtDateRappel";
            this.TxtDateRappel.Size = new System.Drawing.Size(91, 20);
            this.TxtDateRappel.TabIndex = 37;
            // 
            // lblDateRappel
            // 
            this.lblDateRappel.AutoSize = true;
            this.lblDateRappel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateRappel.Location = new System.Drawing.Point(62, 16);
            this.lblDateRappel.Name = "lblDateRappel";
            this.lblDateRappel.Size = new System.Drawing.Size(83, 14);
            this.lblDateRappel.TabIndex = 36;
            this.lblDateRappel.Text = "Date de rappel :";
            // 
            // gpF
            // 
            this.gpF.BackColor = System.Drawing.Color.CadetBlue;
            this.gpF.Controls.Add(this.label28);
            this.gpF.Controls.Add(this.label20);
            this.gpF.Controls.Add(this.TxtSolde);
            this.gpF.Controls.Add(this.txtCommentaire);
            this.gpF.Controls.Add(this.TxtTotalFacture);
            this.gpF.Controls.Add(this.label19);
            this.gpF.Location = new System.Drawing.Point(0, 196);
            this.gpF.Name = "gpF";
            this.gpF.Size = new System.Drawing.Size(963, 63);
            this.gpF.TabIndex = 10;
            this.gpF.TabStop = false;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.ForeColor = System.Drawing.Color.DarkRed;
            this.label28.Location = new System.Drawing.Point(819, 26);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(49, 16);
            this.label28.TabIndex = 60;
            this.label28.Text = "Solde";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.DarkRed;
            this.label20.Location = new System.Drawing.Point(630, 26);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(82, 16);
            this.label20.TabIndex = 59;
            this.label20.Text = "Total Fact.";
            // 
            // TxtSolde
            // 
            this.TxtSolde.BackColor = System.Drawing.Color.White;
            this.TxtSolde.Font = new System.Drawing.Font("Comic Sans MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSolde.ForeColor = System.Drawing.Color.Black;
            this.TxtSolde.Location = new System.Drawing.Point(868, 19);
            this.TxtSolde.Name = "TxtSolde";
            this.TxtSolde.Size = new System.Drawing.Size(89, 28);
            this.TxtSolde.TabIndex = 58;
            this.TxtSolde.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtCommentaire
            // 
            this.txtCommentaire.BackColor = System.Drawing.SystemColors.Window;
            this.txtCommentaire.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCommentaire.Location = new System.Drawing.Point(80, 13);
            this.txtCommentaire.Multiline = true;
            this.txtCommentaire.Name = "txtCommentaire";
            this.txtCommentaire.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCommentaire.Size = new System.Drawing.Size(544, 47);
            this.txtCommentaire.TabIndex = 57;
            // 
            // TxtTotalFacture
            // 
            this.TxtTotalFacture.BackColor = System.Drawing.Color.White;
            this.TxtTotalFacture.Font = new System.Drawing.Font("Comic Sans MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtTotalFacture.ForeColor = System.Drawing.Color.Black;
            this.TxtTotalFacture.Location = new System.Drawing.Point(713, 19);
            this.TxtTotalFacture.Name = "TxtTotalFacture";
            this.TxtTotalFacture.Size = new System.Drawing.Size(94, 28);
            this.TxtTotalFacture.TabIndex = 41;
            this.TxtTotalFacture.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label19
            // 
            this.label19.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(8, 13);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(80, 16);
            this.label19.TabIndex = 33;
            this.label19.Text = "Commentaire :";
            // 
            // groupBox7
            // 
            this.groupBox7.BackColor = System.Drawing.Color.CadetBlue;
            this.groupBox7.Controls.Add(this.lwResultats);
            this.groupBox7.Controls.Add(this.TxtCode);
            this.groupBox7.Controls.Add(this.txtPrix);
            this.groupBox7.Controls.Add(this.txtLibelle);
            this.groupBox7.Controls.Add(this.fpFactures);
            this.groupBox7.Controls.Add(this.txtCoeff);
            this.groupBox7.Controls.Add(this.label25);
            this.groupBox7.Controls.Add(this.label24);
            this.groupBox7.Controls.Add(this.label22);
            this.groupBox7.Controls.Add(this.gpG);
            this.groupBox7.Controls.Add(this.btnPrestationOk);
            this.groupBox7.Controls.Add(this.txtCote);
            this.groupBox7.Controls.Add(this.txtQte);
            this.groupBox7.Controls.Add(this.label26);
            this.groupBox7.Location = new System.Drawing.Point(3, 371);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(920, 386);
            this.groupBox7.TabIndex = 12;
            this.groupBox7.TabStop = false;
            // 
            // lwResultats
            // 
            this.lwResultats.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lwResultats.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lwResultats.FullRowSelect = true;
            this.lwResultats.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lwResultats.HideSelection = false;
            this.lwResultats.Location = new System.Drawing.Point(80, 248);
            this.lwResultats.MultiSelect = false;
            this.lwResultats.Name = "lwResultats";
            this.lwResultats.Size = new System.Drawing.Size(392, 112);
            this.lwResultats.TabIndex = 59;
            this.lwResultats.UseCompatibleStateImageBehavior = false;
            this.lwResultats.View = System.Windows.Forms.View.Details;
            this.lwResultats.Visible = false;
            this.lwResultats.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lwResultats_KeyUp);
            this.lwResultats.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lwResultats_MouseUp);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 74;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Width = 220;
            // 
            // TxtCode
            // 
            this.TxtCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TxtCode.Location = new System.Drawing.Point(80, 360);
            this.TxtCode.Name = "TxtCode";
            this.TxtCode.Size = new System.Drawing.Size(168, 20);
            this.TxtCode.TabIndex = 55;
            this.TxtCode.TextChanged += new System.EventHandler(this.TxtCode_TextChanged);
            this.TxtCode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TxtCode_KeyUp);
            // 
            // txtPrix
            // 
            this.txtPrix.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtPrix.Location = new System.Drawing.Point(742, 360);
            this.txtPrix.Name = "txtPrix";
            this.txtPrix.Size = new System.Drawing.Size(48, 20);
            this.txtPrix.TabIndex = 54;
            // 
            // txtLibelle
            // 
            this.txtLibelle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtLibelle.Location = new System.Drawing.Point(258, 360);
            this.txtLibelle.Name = "txtLibelle";
            this.txtLibelle.Size = new System.Drawing.Size(408, 20);
            this.txtLibelle.TabIndex = 53;
            // 
            // fpFactures
            // 
            this.fpFactures.AccessibleDescription = "";
            this.fpFactures.AllowDragDrop = true;
            this.fpFactures.AllowDrop = true;
            this.fpFactures.BackColor = System.Drawing.Color.Honeydew;
            this.fpFactures.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
            this.fpFactures.Location = new System.Drawing.Point(-3, 4);
            this.fpFactures.Name = "fpFactures";
            this.fpFactures.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpFactures_Sheet1});
            this.fpFactures.Size = new System.Drawing.Size(920, 315);
            this.fpFactures.TabIndex = 0;
            this.fpFactures.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpFactures.DragDrop += new System.Windows.Forms.DragEventHandler(this.fpFactures_DragDrop_1);
            this.fpFactures.DragEnter += new System.Windows.Forms.DragEventHandler(this.fpFactures_DragEnter_1);
            this.fpFactures.MouseDown += new System.Windows.Forms.MouseEventHandler(this.fpFactures_MouseDown_1);
            this.fpFactures.MouseUp += new System.Windows.Forms.MouseEventHandler(this.fpFactures_MouseUp);
            this.fpFactures.SetActiveViewport(0, -1, -1);
            // 
            // fpFactures_Sheet1
            // 
            this.fpFactures_Sheet1.Reset();
            this.fpFactures_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpFactures_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            fpFactures_Sheet1.ColumnCount = 11;
            fpFactures_Sheet1.RowCount = 0;
            this.fpFactures_Sheet1.ActiveColumnIndex = -1;
            this.fpFactures_Sheet1.ActiveRowIndex = -1;
            this.fpFactures_Sheet1.GrayAreaBackColor = System.Drawing.Color.OldLace;
            this.fpFactures_Sheet1.Models = ((FarPoint.Win.Spread.SheetView.DocumentModels)(resources.GetObject("fpFactures_Sheet1.Models")));
            this.fpFactures_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.ReadOnly;
            // 
            // txtCoeff
            // 
            this.txtCoeff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtCoeff.Location = new System.Drawing.Point(704, 360);
            this.txtCoeff.Name = "txtCoeff";
            this.txtCoeff.Size = new System.Drawing.Size(32, 20);
            this.txtCoeff.TabIndex = 57;
            this.txtCoeff.Text = "1.00";
            // 
            // label25
            // 
            this.label25.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label25.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(704, 344);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(32, 16);
            this.label25.TabIndex = 46;
            this.label25.Text = "coeff.";
            // 
            // label24
            // 
            this.label24.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label24.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(672, 344);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(24, 16);
            this.label24.TabIndex = 45;
            this.label24.Text = "Qté.";
            // 
            // label22
            // 
            this.label22.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label22.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(800, 344);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(40, 16);
            this.label22.TabIndex = 43;
            this.label22.Text = "Côté.";
            // 
            // gpG
            // 
            this.gpG.BackColor = System.Drawing.Color.CadetBlue;
            this.gpG.Controls.Add(this.rdMat);
            this.gpG.Controls.Add(this.rdPrest);
            this.gpG.Location = new System.Drawing.Point(5, 325);
            this.gpG.Name = "gpG";
            this.gpG.Size = new System.Drawing.Size(72, 50);
            this.gpG.TabIndex = 13;
            this.gpG.TabStop = false;
            // 
            // rdMat
            // 
            this.rdMat.Location = new System.Drawing.Point(8, 32);
            this.rdMat.Name = "rdMat";
            this.rdMat.Size = new System.Drawing.Size(48, 16);
            this.rdMat.TabIndex = 15;
            this.rdMat.Text = "Mat.";
            this.rdMat.CheckedChanged += new System.EventHandler(this.rdMat_CheckedChanged);
            // 
            // rdPrest
            // 
            this.rdPrest.Checked = true;
            this.rdPrest.Location = new System.Drawing.Point(8, 8);
            this.rdPrest.Name = "rdPrest";
            this.rdPrest.Size = new System.Drawing.Size(56, 24);
            this.rdPrest.TabIndex = 14;
            this.rdPrest.TabStop = true;
            this.rdPrest.Text = "Prest.";
            this.rdPrest.CheckedChanged += new System.EventHandler(this.rdPrest_CheckedChanged);
            // 
            // btnPrestationOk
            // 
            this.btnPrestationOk.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.btnPrestationOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrestationOk.Image = ((System.Drawing.Image)(resources.GetObject("btnPrestationOk.Image")));
            this.btnPrestationOk.Location = new System.Drawing.Point(848, 352);
            this.btnPrestationOk.Name = "btnPrestationOk";
            this.btnPrestationOk.Size = new System.Drawing.Size(64, 32);
            this.btnPrestationOk.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnPrestationOk.TabIndex = 50;
            this.btnPrestationOk.TabStop = false;
            this.toolTip1.SetToolTip(this.btnPrestationOk, "Enregistrer la prestation");
            this.btnPrestationOk.Click += new System.EventHandler(this.btnPrestationOk_Click);
            // 
            // txtCote
            // 
            this.txtCote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtCote.Location = new System.Drawing.Point(800, 360);
            this.txtCote.Name = "txtCote";
            this.txtCote.Size = new System.Drawing.Size(40, 20);
            this.txtCote.TabIndex = 58;
            // 
            // txtQte
            // 
            this.txtQte.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtQte.Location = new System.Drawing.Point(672, 360);
            this.txtQte.Name = "txtQte";
            this.txtQte.Size = new System.Drawing.Size(24, 20);
            this.txtQte.TabIndex = 56;
            this.txtQte.Text = "1";
            // 
            // label26
            // 
            this.label26.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label26.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(744, 344);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(40, 16);
            this.label26.TabIndex = 47;
            this.label26.Text = "Prix :";
            // 
            // gpFonctions
            // 
            this.gpFonctions.BackColor = System.Drawing.Color.LightSteelBlue;
            this.gpFonctions.Controls.Add(this.btnModif);
            this.gpFonctions.Controls.Add(this.btnSave);
            this.gpFonctions.Controls.Add(this.btnDelete);
            this.gpFonctions.Controls.Add(this.btnNew);
            this.gpFonctions.Controls.Add(this.btnHisto);
            this.gpFonctions.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpFonctions.ForeColor = System.Drawing.Color.DodgerBlue;
            this.gpFonctions.ImeMode = System.Windows.Forms.ImeMode.On;
            this.gpFonctions.Location = new System.Drawing.Point(1073, 3);
            this.gpFonctions.Name = "gpFonctions";
            this.gpFonctions.Size = new System.Drawing.Size(84, 217);
            this.gpFonctions.TabIndex = 17;
            this.gpFonctions.TabStop = false;
            this.gpFonctions.Text = "Fonctions";
            // 
            // btnModif
            // 
            this.btnModif.BackColor = System.Drawing.Color.AliceBlue;
            this.btnModif.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnModif.Image = global::ImportSosGeneve.Properties.Resources.modif2;
            this.btnModif.Location = new System.Drawing.Point(0, 52);
            this.btnModif.Name = "btnModif";
            this.btnModif.Size = new System.Drawing.Size(80, 40);
            this.btnModif.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnModif.TabIndex = 63;
            this.btnModif.TabStop = false;
            this.toolTip1.SetToolTip(this.btnModif, "Modifier la facture");
            this.btnModif.Click += new System.EventHandler(this.btnModif_Click);
            // 
            // btnSave
            // 
            this.btnSave.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(0, 137);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 40);
            this.btnSave.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnSave.TabIndex = 62;
            this.btnSave.TabStop = false;
            this.toolTip1.SetToolTip(this.btnSave, "Sauvegarder la facture");
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.Location = new System.Drawing.Point(0, 93);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 40);
            this.btnDelete.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnDelete.TabIndex = 60;
            this.btnDelete.TabStop = false;
            this.toolTip1.SetToolTip(this.btnDelete, "Effacer la facture");
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.Cornsilk;
            this.btnNew.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnNew.Image = ((System.Drawing.Image)(resources.GetObject("btnNew.Image")));
            this.btnNew.Location = new System.Drawing.Point(0, 16);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(80, 40);
            this.btnNew.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnNew.TabIndex = 58;
            this.btnNew.TabStop = false;
            this.toolTip1.SetToolTip(this.btnNew, "Nouvelle facture");
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnHisto
            // 
            this.btnHisto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnHisto.Image = ((System.Drawing.Image)(resources.GetObject("btnHisto.Image")));
            this.btnHisto.Location = new System.Drawing.Point(0, 177);
            this.btnHisto.Name = "btnHisto";
            this.btnHisto.Size = new System.Drawing.Size(80, 40);
            this.btnHisto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnHisto.TabIndex = 57;
            this.btnHisto.TabStop = false;
            this.toolTip1.SetToolTip(this.btnHisto, "Historique de la facture");
            this.btnHisto.Click += new System.EventHandler(this.btnHisto_Click);
            // 
            // gpI
            // 
            this.gpI.Controls.Add(this.btnQuitter);
            this.gpI.Controls.Add(this.btnImpr);
            this.gpI.Location = new System.Drawing.Point(789, 775);
            this.gpI.Name = "gpI";
            this.gpI.Size = new System.Drawing.Size(128, 48);
            this.gpI.TabIndex = 57;
            this.gpI.TabStop = false;
            // 
            // btnQuitter
            // 
            this.btnQuitter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.btnQuitter.Image = ((System.Drawing.Image)(resources.GetObject("btnQuitter.Image")));
            this.btnQuitter.Location = new System.Drawing.Point(56, 8);
            this.btnQuitter.Name = "btnQuitter";
            this.btnQuitter.Size = new System.Drawing.Size(64, 36);
            this.btnQuitter.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnQuitter.TabIndex = 49;
            this.btnQuitter.TabStop = false;
            this.toolTip1.SetToolTip(this.btnQuitter, "Quitter le module facturation");
            this.btnQuitter.Click += new System.EventHandler(this.btnQuitter_Click);
            // 
            // btnImpr
            // 
            this.btnImpr.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.btnImpr.Image = ((System.Drawing.Image)(resources.GetObject("btnImpr.Image")));
            this.btnImpr.Location = new System.Drawing.Point(8, 8);
            this.btnImpr.Name = "btnImpr";
            this.btnImpr.Size = new System.Drawing.Size(43, 36);
            this.btnImpr.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnImpr.TabIndex = 48;
            this.btnImpr.TabStop = false;
            this.toolTip1.SetToolTip(this.btnImpr, "Impression de la facture");
            this.btnImpr.Click += new System.EventHandler(this.btnImpr_Click);
            // 
            // lbStatusOp
            // 
            this.lbStatusOp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStatusOp.Location = new System.Drawing.Point(5, 760);
            this.lbStatusOp.Name = "lbStatusOp";
            this.lbStatusOp.Size = new System.Drawing.Size(309, 19);
            this.lbStatusOp.TabIndex = 58;
            this.lbStatusOp.Text = "Label";
            // 
            // bRotationImage
            // 
            this.bRotationImage.FlatAppearance.BorderSize = 0;
            this.bRotationImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bRotationImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bRotationImage.Image = global::ImportSosGeneve.Properties.Resources.icone_Rotation;
            this.bRotationImage.Location = new System.Drawing.Point(1004, 687);
            this.bRotationImage.Name = "bRotationImage";
            this.bRotationImage.Size = new System.Drawing.Size(62, 59);
            this.bRotationImage.TabIndex = 74;
            this.bRotationImage.Tag = "";
            this.bRotationImage.Text = "90°";
            this.toolTip1.SetToolTip(this.bRotationImage, "Rotation de l\'image");
            this.bRotationImage.UseVisualStyleBackColor = true;
            this.bRotationImage.Click += new System.EventHandler(this.bRotationImage_Click);
            // 
            // TBDateStopRappel
            // 
            this.TBDateStopRappel.Location = new System.Drawing.Point(152, 159);
            this.TBDateStopRappel.Name = "TBDateStopRappel";
            this.TBDateStopRappel.Size = new System.Drawing.Size(83, 20);
            this.TBDateStopRappel.TabIndex = 43;
            this.toolTip1.SetToolTip(this.TBDateStopRappel, "Stop les rappels jusqu\'à la date affichée");
            // 
            // btDupPolice
            // 
            this.btDupPolice.BackColor = System.Drawing.SystemColors.Control;
            this.btDupPolice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btDupPolice.Location = new System.Drawing.Point(664, 763);
            this.btDupPolice.Name = "btDupPolice";
            this.btDupPolice.Size = new System.Drawing.Size(114, 60);
            this.btDupPolice.TabIndex = 59;
            this.btDupPolice.Text = "Police, Ass. Internationnale, Vaccins";
            this.btDupPolice.UseVisualStyleBackColor = false;
            this.btDupPolice.Click += new System.EventHandler(this.btDupPolice_Click);
            // 
            // b10Poucent
            // 
            this.b10Poucent.BackColor = System.Drawing.SystemColors.Control;
            this.b10Poucent.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.b10Poucent.Location = new System.Drawing.Point(563, 765);
            this.b10Poucent.Name = "b10Poucent";
            this.b10Poucent.Size = new System.Drawing.Size(69, 36);
            this.b10Poucent.TabIndex = 60;
            this.b10Poucent.Text = "Facture 10%";
            this.b10Poucent.UseVisualStyleBackColor = false;
            this.b10Poucent.Click += new System.EventHandler(this.b10Poucent_Click);
            // 
            // tbDestinataire10
            // 
            this.tbDestinataire10.BackColor = System.Drawing.Color.CadetBlue;
            this.tbDestinataire10.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDestinataire10.Location = new System.Drawing.Point(320, 763);
            this.tbDestinataire10.Multiline = true;
            this.tbDestinataire10.Name = "tbDestinataire10";
            this.tbDestinataire10.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbDestinataire10.Size = new System.Drawing.Size(224, 60);
            this.tbDestinataire10.TabIndex = 62;
            // 
            // grpArrangement
            // 
            this.grpArrangement.Controls.Add(this.lblArrangementPar);
            this.grpArrangement.Controls.Add(this.lblArrangementLe);
            this.grpArrangement.Controls.Add(this.txtArrangementUser);
            this.grpArrangement.Controls.Add(this.txtArrangementDate);
            this.grpArrangement.Controls.Add(this.txtArrangement);
            this.grpArrangement.Controls.Add(this.btnArrangement);
            this.grpArrangement.Location = new System.Drawing.Point(3, 265);
            this.grpArrangement.Name = "grpArrangement";
            this.grpArrangement.Size = new System.Drawing.Size(920, 55);
            this.grpArrangement.TabIndex = 64;
            this.grpArrangement.TabStop = false;
            this.grpArrangement.Text = "Arrangement";
            // 
            // lblArrangementPar
            // 
            this.lblArrangementPar.AutoSize = true;
            this.lblArrangementPar.Location = new System.Drawing.Point(201, 18);
            this.lblArrangementPar.Name = "lblArrangementPar";
            this.lblArrangementPar.Size = new System.Drawing.Size(23, 13);
            this.lblArrangementPar.TabIndex = 5;
            this.lblArrangementPar.Text = "Par";
            // 
            // lblArrangementLe
            // 
            this.lblArrangementLe.AutoSize = true;
            this.lblArrangementLe.Location = new System.Drawing.Point(55, 18);
            this.lblArrangementLe.Name = "lblArrangementLe";
            this.lblArrangementLe.Size = new System.Drawing.Size(19, 13);
            this.lblArrangementLe.TabIndex = 4;
            this.lblArrangementLe.Text = "Le";
            // 
            // txtArrangementUser
            // 
            this.txtArrangementUser.Location = new System.Drawing.Point(230, 15);
            this.txtArrangementUser.Name = "txtArrangementUser";
            this.txtArrangementUser.ReadOnly = true;
            this.txtArrangementUser.Size = new System.Drawing.Size(119, 20);
            this.txtArrangementUser.TabIndex = 3;
            // 
            // txtArrangementDate
            // 
            this.txtArrangementDate.Location = new System.Drawing.Point(80, 15);
            this.txtArrangementDate.Name = "txtArrangementDate";
            this.txtArrangementDate.ReadOnly = true;
            this.txtArrangementDate.Size = new System.Drawing.Size(103, 20);
            this.txtArrangementDate.TabIndex = 2;
            // 
            // txtArrangement
            // 
            this.txtArrangement.Location = new System.Drawing.Point(355, 13);
            this.txtArrangement.Multiline = true;
            this.txtArrangement.Name = "txtArrangement";
            this.txtArrangement.ReadOnly = true;
            this.txtArrangement.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtArrangement.Size = new System.Drawing.Size(476, 34);
            this.txtArrangement.TabIndex = 1;
            // 
            // btnArrangement
            // 
            this.btnArrangement.BackColor = System.Drawing.SystemColors.Control;
            this.btnArrangement.Enabled = false;
            this.btnArrangement.Location = new System.Drawing.Point(837, 13);
            this.btnArrangement.Name = "btnArrangement";
            this.btnArrangement.Size = new System.Drawing.Size(77, 31);
            this.btnArrangement.TabIndex = 0;
            this.btnArrangement.Text = "Ajouter";
            this.btnArrangement.UseVisualStyleBackColor = false;
            this.btnArrangement.Click += new System.EventHandler(this.btnArrangement_Click);
            // 
            // grpContentieux
            // 
            this.grpContentieux.Controls.Add(this.chkCessionCreance);
            this.grpContentieux.Controls.Add(this.chkSurPlace);
            this.grpContentieux.Location = new System.Drawing.Point(3, 326);
            this.grpContentieux.Name = "grpContentieux";
            this.grpContentieux.Size = new System.Drawing.Size(353, 39);
            this.grpContentieux.TabIndex = 65;
            this.grpContentieux.TabStop = false;
            // 
            // chkCessionCreance
            // 
            this.chkCessionCreance.AutoSize = true;
            this.chkCessionCreance.Location = new System.Drawing.Point(159, 15);
            this.chkCessionCreance.Name = "chkCessionCreance";
            this.chkCessionCreance.Size = new System.Drawing.Size(176, 17);
            this.chkCessionCreance.TabIndex = 1;
            this.chkCessionCreance.Text = "Faire signer cession de créance";
            this.chkCessionCreance.UseVisualStyleBackColor = true;
            // 
            // chkSurPlace
            // 
            this.chkSurPlace.AutoSize = true;
            this.chkSurPlace.Location = new System.Drawing.Point(11, 13);
            this.chkSurPlace.Name = "chkSurPlace";
            this.chkSurPlace.Size = new System.Drawing.Size(143, 17);
            this.chkSurPlace.TabIndex = 0;
            this.chkSurPlace.Text = "Faire encaisser sur place";
            this.chkSurPlace.UseVisualStyleBackColor = true;
            // 
            // grpRappels
            // 
            this.grpRappels.Controls.Add(this.TxtDateContentieux);
            this.grpRappels.Controls.Add(this.lblDateContentieux);
            this.grpRappels.Controls.Add(this.lblDateRappel);
            this.grpRappels.Controls.Add(this.TxtDateRappel);
            this.grpRappels.Location = new System.Drawing.Point(362, 326);
            this.grpRappels.Name = "grpRappels";
            this.grpRappels.Size = new System.Drawing.Size(561, 39);
            this.grpRappels.TabIndex = 66;
            this.grpRappels.TabStop = false;
            // 
            // TxtDateContentieux
            // 
            this.TxtDateContentieux.Location = new System.Drawing.Point(381, 13);
            this.TxtDateContentieux.Name = "TxtDateContentieux";
            this.TxtDateContentieux.Size = new System.Drawing.Size(91, 20);
            this.TxtDateContentieux.TabIndex = 39;
            // 
            // lblDateContentieux
            // 
            this.lblDateContentieux.AutoSize = true;
            this.lblDateContentieux.Location = new System.Drawing.Point(266, 16);
            this.lblDateContentieux.Name = "lblDateContentieux";
            this.lblDateContentieux.Size = new System.Drawing.Size(109, 13);
            this.lblDateContentieux.TabIndex = 38;
            this.lblDateContentieux.Text = "Date de contentieux :";
            // 
            // grpCeder
            // 
            this.grpCeder.Controls.Add(this.dbxCession);
            this.grpCeder.Controls.Add(this.chkCession);
            this.grpCeder.Location = new System.Drawing.Point(704, 126);
            this.grpCeder.Name = "grpCeder";
            this.grpCeder.Size = new System.Drawing.Size(116, 64);
            this.grpCeder.TabIndex = 69;
            this.grpCeder.TabStop = false;
            // 
            // dbxCession
            // 
            this.dbxCession.Location = new System.Drawing.Point(10, 37);
            this.dbxCession.Mask = "00/00/0000";
            this.dbxCession.Name = "dbxCession";
            this.dbxCession.ReadOnly = true;
            this.dbxCession.Size = new System.Drawing.Size(102, 20);
            this.dbxCession.TabIndex = 71;
            this.dbxCession.ValidatingType = typeof(System.DateTime);
            this.dbxCession.Value = "";
            // 
            // chkCession
            // 
            this.chkCession.AutoSize = true;
            this.chkCession.Location = new System.Drawing.Point(9, 14);
            this.chkCession.Name = "chkCession";
            this.chkCession.Size = new System.Drawing.Size(57, 17);
            this.chkCession.TabIndex = 69;
            this.chkCession.Text = "Cédée";
            this.chkCession.UseVisualStyleBackColor = true;
            this.chkCession.CheckedChanged += new System.EventHandler(this.chkCession_CheckedChanged);
            // 
            // labelInfoRapport
            // 
            this.labelInfoRapport.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInfoRapport.ForeColor = System.Drawing.Color.Blue;
            this.labelInfoRapport.Location = new System.Drawing.Point(929, 293);
            this.labelInfoRapport.Name = "labelInfoRapport";
            this.labelInfoRapport.Size = new System.Drawing.Size(201, 27);
            this.labelInfoRapport.TabIndex = 70;
            // 
            // PBoxCession
            // 
            this.PBoxCession.Image = global::ImportSosGeneve.Properties.Resources.cession;
            this.PBoxCession.Location = new System.Drawing.Point(973, 201);
            this.PBoxCession.Name = "PBoxCession";
            this.PBoxCession.Size = new System.Drawing.Size(78, 58);
            this.PBoxCession.TabIndex = 71;
            this.PBoxCession.TabStop = false;
            this.PBoxCession.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.CadetBlue;
            this.groupBox1.Controls.Add(this.CBEffacerDateSession);
            this.groupBox1.Controls.Add(this.label23);
            this.groupBox1.Controls.Add(this.TBDateStopRappel);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.TBoxDateEnvoiCession);
            this.groupBox1.Controls.Add(this.TBoxDateReceptionCession);
            this.groupBox1.Controls.Add(this.CBoxIndelicat);
            this.groupBox1.Controls.Add(this.CBoxRenvoiFranchise);
            this.groupBox1.Controls.Add(this.CBoxRenvoiFact10);
            this.groupBox1.Controls.Add(this.CBCessionRecu);
            this.groupBox1.Location = new System.Drawing.Point(822, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(244, 185);
            this.groupBox1.TabIndex = 72;
            this.groupBox1.TabStop = false;
            // 
            // CBEffacerDateSession
            // 
            this.CBEffacerDateSession.AutoSize = true;
            this.CBEffacerDateSession.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CBEffacerDateSession.Location = new System.Drawing.Point(114, 31);
            this.CBEffacerDateSession.Name = "CBEffacerDateSession";
            this.CBEffacerDateSession.Size = new System.Drawing.Size(87, 18);
            this.CBEffacerDateSession.TabIndex = 45;
            this.CBEffacerDateSession.Text = "Effacer Date";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(2, 163);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(145, 13);
            this.label23.TabIndex = 44;
            this.label23.Text = "Stopper les rappels jusqu\'au :";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(4, 13);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(105, 13);
            this.label17.TabIndex = 42;
            this.label17.Text = "Cession envoyée le :";
            // 
            // TBoxDateEnvoiCession
            // 
            this.TBoxDateEnvoiCession.Location = new System.Drawing.Point(25, 29);
            this.TBoxDateEnvoiCession.Name = "TBoxDateEnvoiCession";
            this.TBoxDateEnvoiCession.ReadOnly = true;
            this.TBoxDateEnvoiCession.Size = new System.Drawing.Size(76, 20);
            this.TBoxDateEnvoiCession.TabIndex = 41;
            // 
            // TBoxDateReceptionCession
            // 
            this.TBoxDateReceptionCession.Location = new System.Drawing.Point(116, 56);
            this.TBoxDateReceptionCession.Name = "TBoxDateReceptionCession";
            this.TBoxDateReceptionCession.Size = new System.Drawing.Size(62, 20);
            this.TBoxDateReceptionCession.TabIndex = 40;
            // 
            // CBoxIndelicat
            // 
            this.CBoxIndelicat.AutoSize = true;
            this.CBoxIndelicat.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CBoxIndelicat.Location = new System.Drawing.Point(14, 133);
            this.CBoxIndelicat.Name = "CBoxIndelicat";
            this.CBoxIndelicat.Size = new System.Drawing.Size(100, 18);
            this.CBoxIndelicat.TabIndex = 7;
            this.CBoxIndelicat.Text = "Patient Indélicat";
            // 
            // CBoxRenvoiFranchise
            // 
            this.CBoxRenvoiFranchise.AutoSize = true;
            this.CBoxRenvoiFranchise.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CBoxRenvoiFranchise.Location = new System.Drawing.Point(14, 109);
            this.CBoxRenvoiFranchise.Name = "CBoxRenvoiFranchise";
            this.CBoxRenvoiFranchise.Size = new System.Drawing.Size(127, 18);
            this.CBoxRenvoiFranchise.TabIndex = 3;
            this.CBoxRenvoiFranchise.Text = "Facture en franchise";
            // 
            // CBoxRenvoiFact10
            // 
            this.CBoxRenvoiFact10.AutoSize = true;
            this.CBoxRenvoiFact10.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CBoxRenvoiFact10.Location = new System.Drawing.Point(14, 84);
            this.CBoxRenvoiFact10.Name = "CBoxRenvoiFact10";
            this.CBoxRenvoiFact10.Size = new System.Drawing.Size(102, 18);
            this.CBoxRenvoiFact10.TabIndex = 1;
            this.CBoxRenvoiFact10.Text = "Renvoyer solde";
            // 
            // CBCessionRecu
            // 
            this.CBCessionRecu.AutoSize = true;
            this.CBCessionRecu.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CBCessionRecu.Location = new System.Drawing.Point(14, 60);
            this.CBCessionRecu.Name = "CBCessionRecu";
            this.CBCessionRecu.Size = new System.Drawing.Size(96, 18);
            this.CBCessionRecu.TabIndex = 0;
            this.CBCessionRecu.Text = "Cession reçue";
            // 
            // zoomImageViewer1
            // 
            this.zoomImageViewer1.AutoScroll = true;
            this.zoomImageViewer1.AutoScrollMargin = new System.Drawing.Size(240, 164);
            this.zoomImageViewer1.BackColor = System.Drawing.Color.CadetBlue;
            this.zoomImageViewer1.Image = null;
            this.zoomImageViewer1.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            this.zoomImageViewer1.Location = new System.Drawing.Point(932, 509);
            this.zoomImageViewer1.Name = "zoomImageViewer1";
            this.zoomImageViewer1.Size = new System.Drawing.Size(240, 164);
            this.zoomImageViewer1.TabIndex = 73;
            this.zoomImageViewer1.Text = "zoomImageViewer1";
            this.zoomImageViewer1.Zoom = 1F;
            this.zoomImageViewer1.MouseEnter += new System.EventHandler(this.zoomImageViewer1_MouseEnter);
            // 
            // bAjoutDoc
            // 
            this.bAjoutDoc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bAjoutDoc.Enabled = false;
            this.bAjoutDoc.FlatAppearance.BorderSize = 0;
            this.bAjoutDoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bAjoutDoc.ImageIndex = 0;
            this.bAjoutDoc.ImageList = this.imageList1;
            this.bAjoutDoc.Location = new System.Drawing.Point(1070, 225);
            this.bAjoutDoc.Name = "bAjoutDoc";
            this.bAjoutDoc.Size = new System.Drawing.Size(84, 54);
            this.bAjoutDoc.TabIndex = 75;
            this.bAjoutDoc.UseVisualStyleBackColor = true;
            this.bAjoutDoc.Click += new System.EventHandler(this.bAjout_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "tromboneOff.png");
            this.imageList1.Images.SetKeyName(1, "tromboneOn.png");
            // 
            // bDuplicata
            // 
            this.bDuplicata.BackColor = System.Drawing.SystemColors.Control;
            this.bDuplicata.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bDuplicata.Location = new System.Drawing.Point(662, 826);
            this.bDuplicata.Name = "bDuplicata";
            this.bDuplicata.Size = new System.Drawing.Size(116, 36);
            this.bDuplicata.TabIndex = 76;
            this.bDuplicata.Text = "Police/tarmed";
            this.bDuplicata.UseVisualStyleBackColor = false;
            this.bDuplicata.Click += new System.EventHandler(this.bDuplicata_Click);
            // 
            // CtrlFacturation
            // 
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.Controls.Add(this.bDuplicata);
            this.Controls.Add(this.bAjoutDoc);
            this.Controls.Add(this.bRotationImage);
            this.Controls.Add(this.zoomImageViewer1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.PBoxCession);
            this.Controls.Add(this.labelInfoRapport);
            this.Controls.Add(this.grpCeder);
            this.Controls.Add(this.grpRappels);
            this.Controls.Add(this.gpE);
            this.Controls.Add(this.grpContentieux);
            this.Controls.Add(this.grpArrangement);
            this.Controls.Add(this.tbDestinataire10);
            this.Controls.Add(this.b10Poucent);
            this.Controls.Add(this.btDupPolice);
            this.Controls.Add(this.lbStatusOp);
            this.Controls.Add(this.gpI);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.gpF);
            this.Controls.Add(this.gpB);
            this.Controls.Add(this.gpD);
            this.Controls.Add(this.gpA);
            this.Controls.Add(this.gpC);
            this.Controls.Add(this.gpFonctions);
            this.Name = "CtrlFacturation";
            this.Size = new System.Drawing.Size(1177, 900);
            this.gpC.ResumeLayout(false);
            this.gpC.PerformLayout();
            this.gpA.ResumeLayout(false);
            this.gpA.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BtnFindFacture)).EndInit();
            this.gpD.ResumeLayout(false);
            this.gpD.PerformLayout();
            this.gpB.ResumeLayout(false);
            this.gpB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnNouveauDestinataire)).EndInit();
            this.gpE.ResumeLayout(false);
            this.gpE.PerformLayout();
            this.gpF.ResumeLayout(false);
            this.gpF.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpFactures)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpFactures_Sheet1)).EndInit();
            this.gpG.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnPrestationOk)).EndInit();
            this.gpFonctions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnModif)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnHisto)).EndInit();
            this.gpI.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnQuitter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnImpr)).EndInit();
            this.grpArrangement.ResumeLayout(false);
            this.grpArrangement.PerformLayout();
            this.grpContentieux.ResumeLayout(false);
            this.grpContentieux.PerformLayout();
            this.grpRappels.ResumeLayout(false);
            this.grpRappels.PerformLayout();
            this.grpCeder.ResumeLayout(false);
            this.grpCeder.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PBoxCession)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void InitializeControls()
		{
			rdNFacture.Checked = true;
			txtNFacture.Text = "";
			txtNConsultation.Text = "";

			txtNomMedecin.Text = "";
			txtNomPatient.Text = "";
			txtTypeDestinataire.Text  ="";
			txtDestinataire.Text = "";
			txtdateN.Text="";

			cbEnvoi.Text = "";
			cbTypeAss.Text ="";
			cbTarif.Text = "";
			cbTTT.Text="";
			txtDateAcc.Text ="";
			txtDateEnc.Text ="";
			txtAccident.Text="";
			txtDateAcc.Text="";
			txtRef.Text="";
            txtAVS.Text = "";
			cbSortie.Text="";
			cbDocJoint.Text="";
			chkFlagConcerne.Checked =false;

			chkAcquitte.Checked = false;
			chkAnnule.Checked = false;
			chkEncaisse.Checked = false;
			chkEnvoye.Checked = false;
            chkCession.Checked = false;
            dbxCession.Text = "";

            chkSurPlace.Checked = false;
            chkCessionCreance.Checked = false;

            TxtDateContentieux.Text = "";
			txtDateCreation.Text= "";
			txtDateImpression.Text = "";
			TxtDateRappel.Text = "";

			txtCommentaire.Text="";
			TxtTotalFacture.Text = "";
            TxtSolde.Text = "";             

            //Domi 30/03/2011 initialisation des contrôles

            TBoxDateEnvoiCession.Text = "";
            CBCessionRecu.Checked = false;
            CBEffacerDateSession.Checked = false;
            CBoxRenvoiFact10.Checked = false;
            CBoxRenvoiFranchise.Checked = false;
            CBoxIndelicat.Checked = false;
            TBoxDateReceptionCession.Text = "";
            TBDateStopRappel.Text = "";
            bAjoutDoc.ImageIndex = 0;  //Désactivé
            bAjoutDoc.Enabled = false;
	                    
            //***************

			rdPrest.Checked = true;
			
			InitializeDetailFacture();
			InitializeSaisieDetail();
		}

		private void InitializeDetailFacture()
		{
			fpFactures_Sheet1.RowCount=0;
			fpFactures_Sheet1.ColumnCount=11;
			fpFactures_Sheet1.RowHeaderVisible = false;
			fpFactures_Sheet1.ColumnHeaderVisible = true;

			fpFactures_Sheet1.Columns[4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
			fpFactures_Sheet1.Columns[5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
			fpFactures_Sheet1.Columns[6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
		

			fpFactures_Sheet1.AlternatingRows[0].BackColor = Color.Aquamarine;
			FF.NumberDecimalSeparator=".";
		}

		private void InitializeSaisieDetail()
		{
			txtLibelle.Text="";
			txtQte.Text="";
			txtCote.Text="";
			txtCoeff.Text="";
			txtPrix.Text="";
			TxtCode.Text="";
			lwResultats.Visible = false;
			TxtCode.Focus();
		}

		private void InitializeCtrlNavig()
		{
			CtrlNavigation m_ctrlnavig = new CtrlNavigation();
			this.gpA.Controls.Add(m_ctrlnavig);
			m_ctrlnavig.Top = BtnFindFacture.Top;
			m_ctrlnavig.Left = BtnFindFacture.Left + BtnFindFacture.Width + 30;
			this.gpA.Controls.SetChildIndex(m_ctrlnavig,0);

			m_ctrlnavig.BtnPrev_Click+=new EventHandler(Navig_BtnPrevClick);
			m_ctrlnavig.BtnNext_Click+=new EventHandler(Navig_BtnNextClick);
			m_ctrlnavig.BtnLast_Click+=new EventHandler(Navig_BtnLastClick);
		}
     
		// Clic sur le bouton de navigation arriere
		private void Navig_BtnPrevClick(object sender, EventArgs e)
		{
            chargeListe = false;

            if (_drwFacture != null)
			{
                long NFacture = long.Parse(_drwFacture["NFacture"].ToString());
				// on recherche le numéro de facture juste avant
                string[][] retour = OutilsExt.OutilsSql.ExecuteCommandeAvecTabString("SELECT TOP(1) c.NFacture,c.NConsultation from factureconsultation c WHERE c.NFacture < " + NFacture + " And c.Principale = 1 order by c.NFacture DESC");
				if(retour!=null && retour.Length==1)
				{
					long PrevFac = long.Parse(retour[0][0]);
					long PrevCons = long.Parse(retour[0][1]);
					AfficheFactureTotale(PrevFac,PrevCons);
				}
			}
		}
		// Clic sur le bouton de navigation avant
		private void Navig_BtnNextClick(object sender, EventArgs e)
		{
            if (_drwFacture != null)
			{
                long NFacture = long.Parse(_drwFacture["NFacture"].ToString());
				// on recherche le numéro de facture juste avant
                string[][] retour = OutilsExt.OutilsSql.ExecuteCommandeAvecTabString("SELECT TOP(1) c.NFacture,c.NConsultation from factureconsultation c WHERE c.NFacture > " + NFacture + " And c.NConsultation<>0 And c.Principale = 1 order by c.NFacture ASC");
				if(retour!=null && retour.Length==1)
				{
					long PrevFac = long.Parse(retour[0][0]);
					long PrevCons = long.Parse(retour[0][1]);
					AfficheFactureTotale(PrevFac,PrevCons);
				}
			}
		}
		// Clic sur le bouton de navigation vers la fin
		private void Navig_BtnLastClick(object sender, EventArgs e)
		{
            string[][] retour = OutilsExt.OutilsSql.ExecuteCommandeAvecTabString("SELECT TOP(1) c.NFacture,c.NConsultation from factureconsultation c WHERE c.Principale = 1 order by c.NFacture DESC");
			if(retour!=null && retour.Length==1)
			{
					long LastFac = long.Parse(retour[0][0]);
					long LastCons = long.Parse(retour[0][1]);
					AfficheFactureTotale(LastFac,LastCons);					
			}			
		}

		private void AfficheFactureTotale(long NFacture,long NConsult)
		{
			_frmgeneral.AfficheAppelsByConsult((int)NConsult);	
			m_datarowAppel = Donnees.MonDataSetAppels.Tables[0].Rows[0];	
			 btnNew.Visible = false;
			// btnSave.Visible = false;
			 btnHisto.Visible = true;
            // btnModif.Visible = true;
			 btnDelete.Visible = true;
                        
            DataRow[] z_drw = OutilsExt.OutilsSql.RecuperationFacturesByNFacture(NFacture);
            if (z_drw != null && z_drw.Length > 0)
            {                                                
                _drwFacture = z_drw[0];
            }
            else
            {
                _drwFacture = null;
            }

			MiseEnPlaceConsultation(m_datarowAppel);
            MiseEnPlaceFacture(_drwFacture);
			BlocageControles(false);
		}

		private void PreparationPourRechercheConsult()
		{
			txtNConsultation.Enabled = true;
			txtNFacture.Enabled = true;
			rdNConsultation.Checked = true;
			txtNConsultation.Focus();
		}

		private void BlocageControles(bool p_blnActif)
		{
			gpA.Enabled = true;
            gpB.Enabled = !p_blnActif;
            gpC.Enabled = !p_blnActif;
            gpD.Enabled = !p_blnActif;
            gpE.Enabled = !p_blnActif;
            gpF.Enabled = !p_blnActif;
            gpG.Enabled = !p_blnActif;
            grpContentieux.Enabled = !p_blnActif;
            grpCeder.Enabled = !p_blnActif;
            gpI.Enabled = true;
			gpFonctions.Enabled = true;
            grpRappels.Enabled = !p_blnActif;
		}		

		#endregion

		#region Initialisation des données statiques
		
		private void InitializeData()
		{
			ChargementFactureTermine = false;

			// Les Types d'envoi : 
			cbEnvoi.Items.Clear();
			cbEnvoi.Items.Add(new ListItem((int)Facturation.Envoi.Privé,Facturation.Envoi.Privé.ToString()));
			cbEnvoi.Items.Add(new ListItem((int)Facturation.Envoi.Tiers,Facturation.Envoi.Tiers.ToString()));
			cbEnvoi.Items.Add(new ListItem((int)Facturation.Envoi.Assurance,Facturation.Envoi.Assurance.ToString()));
			
            if( cbEnvoi.SelectedIndex.ToString() == "tiers") 
                MessageBox.Show("tiers");
			
            //Les Types de tarifs : 
			cbTarif.Items.Clear();
			string[][] tarifs = OutilsExt.OutilsSql.ExecuteCommandeAvecTabString("SELECT id,libelle,prixpoint,typesortie from fac_tarif");
			if(tarifs!=null)
			{
				for(int i=0;i<tarifs.Length;i++)
				{
					Facture_Tarifs tar = new Facture_Tarifs(int.Parse(tarifs[i][0]),tarifs[i][1],float.Parse(tarifs[i][2]),int.Parse(tarifs[i][3]));
					ListItem item = new ListItem(tar,tar.LibelleType);
					cbTarif.Items.Add(item);
				}
			}
			// Les Types de TTT : 
			cbTTT.Items.Clear();
			cbTTT.Items.Add(new ListItem((int)Facturation.TTT.Maladie,Facturation.TTT.Maladie.ToString()));
			cbTTT.Items.Add(new ListItem((int)Facturation.TTT.Accident,Facturation.TTT.Accident.ToString()));
			cbTTT.Items.Add(new ListItem((int)Facturation.TTT.Examen,Facturation.TTT.Examen.ToString()));
			// Les Types d'assurance : 
			cbTypeAss.Items.Clear();
			cbTypeAss.Items.Add(new ListItem((int)Facturation.TypeAssurance.Accident,Facturation.TypeAssurance.Accident.ToString()));
			cbTypeAss.Items.Add(new ListItem((int)Facturation.TypeAssurance.Maladie,Facturation.TypeAssurance.Maladie.ToString()));
			// Les Types de sortie : 
			cbSortie.Items.Clear();
			cbSortie.Items.Add(new ListItem((int)Facturation.Sortie.Aucune,Facturation.Sortie.Aucune.ToString()));
			cbSortie.Items.Add(new ListItem((int)Facturation.Sortie.Imprimante,Facturation.Sortie.Imprimante.ToString()));
			cbSortie.Items.Add(new ListItem((int)Facturation.Sortie.Xml,Facturation.Sortie.Xml.ToString()));
            cbSortie.Items.Add(new ListItem((int)Facturation.Sortie.Email, Facturation.Sortie.Email.ToString()));
			
            // Les Types de documents joints : 
			cbDocJoint.Items.Clear();
			
            string[][] typedocs = OutilsExt.OutilsSql.ExecuteCommandeAvecTabString("SELECT id,txtedocj from fac_typedocj");
			
            if(typedocs!=null)
			{
				for(int i=0;i<typedocs.Length;i++)
				{
					Facture_DocJoint doc = new Facture_DocJoint(int.Parse(typedocs[i][0]),typedocs[i][1]);
					ListItem item = new ListItem(doc,doc.LibelleTypeDoc);
					cbDocJoint.Items.Add(item);
				}
			}	
		}
       
        //Rechargement de la liste des prestations en fonction du Tarmed
        private void ChargementComboPrestation(string TarmedVersion)   //Tarmed= LAA-AM-AI ou LAMAL
		{
            if (chargeListe == true)
            {
                lwResultats.Items.Clear();

                //Domi 28.03.2018
                string requete = "SELECT NPrestation, PrestLibelle, PrestPointM, PrestPointT, PrestMajor, PrestHorsMajor";
                requete += " FROM Tarmed";

                string DateAppel = "";
                //On regarde si le champs n'existe pas (on vient de l'historique de la facture)
               
                if (m_datarowAppel.Table.Columns.Contains("DSL") && m_datarowAppel["DSL"].ToString() != "")               
                    DateAppel = m_datarowAppel["DSL"].ToString();
                else
                    DateAppel = m_datarowAppel["DateConsultation"].ToString();

                if (DateTime.Parse(DateAppel) < DateTime.Parse("01.01.2018"))
                {
                    requete += " WHERE DateDebut <= '" + DateAppel + "'";
                    requete += " AND isNull(DateFin,'01.01.2030') >= '" + DateAppel + "'";
                    requete += " AND TarmedVersion = 'LAA-AM-AI'";
                }
                else
                {
                    if (TarmedVersion == "LAA-AM-AI")
                    {
                        requete += " WHERE DateDebut <= '" + DateAppel + "'";
                        requete += " AND isNull(DateFin,'31.12.2017') = '31.12.2017'";
                        requete += " AND TarmedVersion = 'LAA-AM-AI'";
                    }
                    else
                    {
                        requete += " WHERE DateDebut <= '" + DateAppel + "'";
                        requete += " AND isNull(DateFin,'01.01.2030') >= '" + DateAppel + "'";
                        requete += " AND TarmedVersion = 'LAMAL'";
                    }
                }

                requete += " order by NPrestation";

                string[][] retour = OutilsExt.OutilsSql.ExecuteCommandeAvecTabString(requete);

                Statiques_Data.TabPrestations = new Facture_Prestation[retour.Length];
                for (int i = 0; i < retour.Length; i++)
                {
                    string[] tab = retour[i];

                    bool bMajor = false;
                    bool bHorsMajor = false;
                    if (tab[4] == "1")
                        bMajor = true;
                    if (tab[5] == "1")
                        bHorsMajor = true;
                    Statiques_Data.TabPrestations[i] = new Facture_Prestation(tab[0], tab[1], float.Parse(tab[2]), float.Parse(tab[3]), bMajor, bHorsMajor);
                }


                foreach (Facture_Prestation prest in Statiques_Data.TabPrestations)
                {
                    ListViewItem item = new ListViewItem(prest.NPrestation);
                    item.Tag = prest;
                    item.SubItems.Add(prest.Libelle);
                    item.SubItems.Add(prest.PrestPoints.ToString());
                    if (lwResultats.Items.Count % 2 == 0)
                        item.BackColor = Color.White;
                    else
                        item.BackColor = Color.PaleGreen;
                    lwResultats.Items.Add(item);
                }

                Console.WriteLine(m_datarowAppel[9].ToString());
            }
		}


        //Recherche d'un code SANS rechargement de la liste des prestations
        private void ChargementComboPrestationCode(string Code)
		{                       
            lwResultats.Items.Clear();
			foreach(Facture_Prestation prest in Statiques_Data.TabPrestations)
			{
				if(prest.NPrestation.IndexOf(Code)>-1)
				{
					ListViewItem item = new ListViewItem(prest.NPrestation);
					item.Tag = prest;
					item.SubItems.Add(prest.Libelle);
					item.SubItems.Add(prest.PrestPoints.ToString());
					if(lwResultats.Items.Count%2==0)
						item.BackColor = Color.White;
					else
						item.BackColor  = Color.PaleGreen;
					lwResultats.Items.Add(item);
				}
			}
		}

		private void ChargementComboMateriel()
		{
			lwResultats.Items.Clear();
			foreach(Facture_Materiel mat in Statiques_Data.TabMateriel)
			{
				ListViewItem item = new ListViewItem(mat.NMateriel);
				item.Tag = mat;
				item.SubItems.Add(mat.Libelle);
				item.SubItems.Add(mat.Prix.ToString());
				if(lwResultats.Items.Count%2==0)
					item.BackColor = Color.White;
				else
					item.BackColor  = Color.PaleGreen;
				lwResultats.Items.Add(item);
			}
		}

		private void ChargementComboMateriel(string Texte)
		{
			lwResultats.Items.Clear();
			foreach(Facture_Materiel mat in Statiques_Data.TabMateriel)
			{
				if(mat.NMateriel.IndexOf(Texte)>-1)
				{
					bool Quit = false;

					if(mat.NMateriel==Texte)
					{
						lwResultats.Items.Clear();
						Quit=true;
					}

					ListViewItem item = new ListViewItem(mat.NMateriel);
					item.Tag = mat;
					item.SubItems.Add(mat.Libelle);
					item.SubItems.Add(mat.Prix.ToString());
					if(lwResultats.Items.Count%2==0)
						item.BackColor = Color.White;
					else
						item.BackColor  = Color.PaleGreen;
					lwResultats.Items.Add(item);	
				
					if(Quit) break;
				}
			}
		}

		#endregion	

		#region Initialisation des données dynamiques

		//Partie mise en place d'une facture générée       
		private void MiseEnPlaceFacture(DataRow rowFacture)
		{
			ChargementFactureTermine = false;

            if (rowFacture != null)
            {
                this.Cursor = Cursors.WaitCursor;

                lstConsultationsPourFacture.Items.Clear();
                lstConsultationsPourFacture.Items.Add(rowFacture["NConsultation"].ToString() + " - " + rowFacture["NomPersonne"].ToString());

                // chargement des arrangements
                ChargeArrangement(rowFacture);
                
                DocumentEnCours.Clear();
                txtNFacture.Enabled = false;
                txtNConsultation.Enabled = false;
                txtNFacture.Text = rowFacture["NFacture"].ToString();
                if (rowFacture["TypeEnvoi"].ToString() != System.DBNull.Value.ToString())
                {
                    SelectionTypeEnvoi(int.Parse(rowFacture["TypeEnvoi"].ToString()));
                }
                               
                if (rowFacture["Tarif"].ToString() != System.DBNull.Value.ToString())
                {
                    SelectionTarif(int.Parse(rowFacture["Tarif"].ToString()));
                }
                if (rowFacture["TTT"].ToString() != System.DBNull.Value.ToString())
                {
                    SelectionTTT(int.Parse(rowFacture["TTT"].ToString()));
                }                            

                if (rowFacture["TypeAssurance"].ToString() != System.DBNull.Value.ToString())
                {
                    SelectionTypeAssurance(int.Parse(rowFacture["TypeAssurance"].ToString()));
                }
                if (rowFacture["TypeSortie"].ToString() != System.DBNull.Value.ToString())
                {
                    SelectionSortie(int.Parse(rowFacture["TypeSortie"].ToString()));
                }
                if (rowFacture["DateAccident"].ToString() != System.DBNull.Value.ToString())
                {
                    txtDateAcc.Text = DateTime.Parse(rowFacture["DateAccident"].ToString()).ToString("dd/MM/yyyy");
                }
                if (rowFacture["NAccident"].ToString() != System.DBNull.Value.ToString())
                {
                    txtAccident.Text = rowFacture["NAccident"].ToString();
                }

                //Domi 07.12.2012
                if (rowFacture["RefPatient"].ToString() != System.DBNull.Value.ToString())
                {
                    txtRef.Text = rowFacture["RefPatient"].ToString();
                }
                else if (rowFacture["Num_Assure"].ToString() != System.DBNull.Value.ToString())
                {
                    txtRef.Text = rowFacture["Num_Assure"].ToString();
                }

                if (rowFacture["FactNum_AVS"].ToString() != System.DBNull.Value.ToString())
                {
                    txtAVS.Text = rowFacture["FactNum_AVS"].ToString();
                }
                else if (rowFacture["Num_AVS"].ToString() != System.DBNull.Value.ToString())
                {
                    txtAVS.Text = rowFacture["Num_AVS"].ToString();
                }
                //*******
                //Domi 02.04.2013
                //Recherche d'une image carte AVS à partir de la consultation                                                
                string PathSource = SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.Dest_Path_carteAVS;                       

                try
                {
                    //on defini le chemin avec le nom complet du fichier 
                    String ImageCarteAVS = PathSource + rowFacture["NConsultation"].ToString() + ".jpg";

                    if (File.Exists(ImageCarteAVS))     //Si le fichier existe
                    {
                        FileInfo fInfo = new FileInfo(ImageCarteAVS);
                        if (fInfo.Length > 0)       //...et si sa taille est > 0 octet
                        {
                            zoomImageViewer1.Zoom = .3F;     //On défini la taille de l'image par défaut
                            zoomImageViewer1.Image = Image.FromFile(ImageCarteAVS);   //on affiche l'image de la carte AVS                          
                        }
                    }
                    else
                    {
                        zoomImageViewer1.Zoom = .8F;     //On défini la taille de l'image par défaut
                        zoomImageViewer1.Image = ImportSosGeneve.Properties.Resources.nocarte_avs;   //on affiche l'image de la carte AVS        
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Erreur du chargement de l'image: " + e);
                }

                //***************

                if (rowFacture["DateImpression"].ToString() != System.DBNull.Value.ToString())
                {
                    txtDateImpression.Text = DateTime.Parse(rowFacture["DateImpression"].ToString()).ToString("dd/MM/yyyy");
                }
                if (rowFacture["DateCreation"].ToString() != System.DBNull.Value.ToString())
                {
                    txtDateCreation.Text = DateTime.Parse(rowFacture["DateCreation"].ToString()).ToString("dd/MM/yyyy");
                }
                if (rowFacture["FacDateImpression10"].ToString() != System.DBNull.Value.ToString())
                {
                    txtDateImpression10.Text = DateTime.Parse(rowFacture["FacDateImpression10"].ToString()).ToString("dd/MM/yyyy");
                }

                // Qu'en est-il des rappels de la facture??
                if (rowFacture["FacDate1Rappel"].ToString() != DBNull.Value.ToString())
                {
                    TxtDateRappel.Text = DateTime.Parse(rowFacture["FacDate1Rappel"].ToString()).ToString("dd/MM/yyyy");
                }

                // Coche les différents états de la facture : 
                if (rowFacture["DateImpression"].ToString() != DBNull.Value.ToString())
                    chkEnvoye.Checked = true;
                if (rowFacture["FacDateAnnulee"].ToString() != DBNull.Value.ToString())
                    chkAnnule.Checked = true;
                if (rowFacture["FacDateAcquittee"].ToString() != DBNull.Value.ToString())
                {
                    chkAcquitte.Checked = true;
                    chkCession.Enabled = false;
                }
                else
                    chkCession.Enabled = true;

                // Date de contentieux
                if (rowFacture["FacDateContentieux"].ToString() != System.DBNull.Value.ToString())
                {
                    TxtDateContentieux.Text = DateTime.Parse(rowFacture["FacDateContentieux"].ToString()).ToString("dd/MM/yyyy");
                }
                else TxtDateContentieux.Text = "";
                // Date d'encaissement
                if (rowFacture["FacDateEncaissee"].ToString() != System.DBNull.Value.ToString())
                {
                    chkEncaisse.Checked = true;
                    txtDateEnc.Text = DateTime.Parse(rowFacture["FacDateEncaissee"].ToString()).ToString("dd/MM/yyyy");
                }
                else txtDateEnc.Text = "";


                // Date d'encaissement
                if (rowFacture["FacDateCession"].ToString() != System.DBNull.Value.ToString())
                {
                    chkCession.Checked = true;
                    dbxCession.Value = rowFacture["FacDateCession"].ToString();
                }
                else
                {
                    chkCession.Checked = false;
                    dbxCession.Value = "";
                }
                //
                if (rowFacture["FacDateEnvoyee"].ToString() != DBNull.Value.ToString())
                {
                    // La facture a été envoyée, on ne peut plus la modifier directement
                    btnDelete.Visible = false;
                    // btnModif.Visible = false;
                    // btnSave.Visible = false;
                    chkEnvoye.Checked = true;
                }

                if (rowFacture["TypeDocJoint"].ToString() != System.DBNull.Value.ToString())
                {
                    for (int i = 0; i < cbDocJoint.Items.Count; i++)
                    {
                        ListItem item = (ListItem)cbDocJoint.Items[i];
                        Facture_DocJoint jt = (Facture_DocJoint)item.objValue;
                        if (jt.TypeDoc == int.Parse(rowFacture["TypeDocJoint"].ToString()))
                            cbDocJoint.SelectedIndex = i;
                    }
                }

                if (rowFacture["FlagConcerne"].ToString() != System.DBNull.Value.ToString() && rowFacture["FlagConcerne"].ToString() == "1")
                {
                    chkFlagConcerne.Checked = true;
                }
                else
                    chkFlagConcerne.Checked = false;

                txtCommentaire.Text = rowFacture["Commentaire"].ToString();
                TxtTotalFacture.Text = string.Format("{0:0.00}", float.Parse(rowFacture["TotalFacture"].ToString()));
                TxtSolde.Text = string.Format("{0:0.00}", float.Parse(rowFacture["Solde"].ToString()));
                tbDestinataire10.Text = rowFacture["AdresseDestinataire2"].ToString();
                             
                //Domi 01/04/2011 Recup des données Facture_Status
                if (rowFacture["CessionEnvoi"].ToString() != System.DBNull.Value.ToString())  //si on a une date
                {
                    TBoxDateEnvoiCession.Text = DateTime.Parse(rowFacture["CessionEnvoi"].ToString()).ToString("dd/MM/yyyy");
                }

                if (rowFacture["CessionRecu"].ToString() != System.DBNull.Value.ToString())  //si on a une date
                {
                    CBCessionRecu.Checked = true;
                    TBoxDateReceptionCession.Text = DateTime.Parse(rowFacture["CessionRecu"].ToString()).ToString("dd/MM/yyyy");
                }
                else
                {
                    CBCessionRecu.Checked = false;
                    TBoxDateReceptionCession.Text = "";
                }


                if (rowFacture["RenvFact10p"].ToString() == "1") //Renvoi Facture 10%
                {
                    CBoxRenvoiFact10.Checked = true;
                }
                else CBoxRenvoiFact10.Checked = false;


                if (rowFacture["FactFranchise"].ToString() == "1") //Renvoi Facture en franchise
                {
                    CBoxRenvoiFranchise.Checked = true;
                }
                else CBoxRenvoiFranchise.Checked = false;


                if (rowFacture["PatientIndelicat"].ToString() == "1") //Patient Indélicat (déjà remboursé par ass mais ne nous paie pas)
                {
                    CBoxIndelicat.Checked = true;
                }
                else CBoxIndelicat.Checked = false;

                if (rowFacture["LimiteStopRappel"].ToString() != System.DBNull.Value.ToString())  //si on a une date de LimiteStopRappel
                {
                    TBDateStopRappel.Text = DateTime.Parse(rowFacture["LimiteStopRappel"].ToString()).ToString("dd/MM/yyyy");
                }

                //On ajuste la variable scale_factor_mt et Tarmed maintenant qu'on a une facture                    
                //string[] Resultat = OutilsExt.OutilsSql.Val_Scale_factor_mt_Tarmed(long.Parse(rowFacture["NConsultation"].ToString()));   //Domi 06.10.2017
                string[] Resultat = OutilsExt.OutilsSql.Val_Scale_factor_mt_Tarmed_AvFact(long.Parse(rowFacture["NConsultation"].ToString()),long.Parse(rowFacture["NFacture"].ToString()));   //Domi 06.10.2017
                                
                scale_factor_mt = Convert.ToDouble(Resultat[0].ToString());
                TarmedVersion = Resultat[1].ToString();

                // Recuperation des prestations : 
                string[][] prestations = OutilsExt.OutilsSql.RecuperationPrestationsByNFacture(long.Parse(rowFacture["NFacture"].ToString()), TarmedVersion);
                for (int i = 0; i < prestations.Length; i++)
                {
                    int nb = fpFactures_Sheet1.RowCount++;

                    fpFactures_Sheet1.Rows[nb].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                    fpFactures_Sheet1.Cells[nb, 0].Text = prestations[i][0];
                    fpFactures_Sheet1.Cells[nb, 1].Text = prestations[i][1];
                    fpFactures_Sheet1.Cells[nb, 2].Text = prestations[i][2];
                    fpFactures_Sheet1.Cells[nb, 3].Text = prestations[i][3];
                    fpFactures_Sheet1.Cells[nb, 4].Text = WorkedString.FormatMontantArrondi(float.Parse(prestations[i][4]));
                    fpFactures_Sheet1.Cells[nb, 5].Text = prestations[i][5];
                    fpFactures_Sheet1.Cells[nb, 5].Tag = prestations[i][5];
                    fpFactures_Sheet1.Cells[nb, 6].Text = prestations[i][6];
                    fpFactures_Sheet1.Cells[nb, 7].Text = prestations[i][7];
                    fpFactures_Sheet1.Cells[nb, 8].Text = prestations[i][8];
                    fpFactures_Sheet1.Cells[nb, 9].Text = prestations[i][9];
                    fpFactures_Sheet1.Cells[nb, 10].Text = prestations[i][10];
                }               
                
                //On recharge la liste des Tarifs
                chargeListe = true;
                ChargementComboPrestation(TarmedVersion); 
               
 
                CalculeMajorations();

                Dest dest = new Dest();
                if (rowFacture["TypeDestinataire"].ToString() != "" && rowFacture["CodeDestinataire"].ToString() != "")
                {
                    dest.m_TypeDestinataire = (CtrlDest.TypeDestinataire)int.Parse(rowFacture["TypeDestinataire"].ToString());
                    dest.CodeDestinataireFacture = int.Parse(rowFacture["CodeDestinataire"].ToString());
                    dest.AdresseDestinataire = rowFacture["AdresseDestinataire"].ToString();

                    txtTypeDestinataire.Tag = dest;
                    txtTypeDestinataire.Text = dest.m_TypeDestinataire.ToString();
                    txtDestinataire.Text = dest.AdresseDestinataire;
                }
                else
                    MiseEnPlaceDestinataire(m_datarowAppel);

                // Donnees table patient_remarque
                Boolean z_blnConnect = SosMedecins.SmartRapport.DAL.Variables.ConnexionBase.OpenBDD();
                try
                {
                    _dtbPatient_Remarque = SosMedecins.SmartRapport.DAL.Variables.ConnexionBase.ExecuteSql(null, SosMedecins.SmartRapport.DAL.RequetesSelect.Patient_Remarque.IdPatient.Replace("%IdPatient%", m_datarowAppel["IndicePatient"].ToString()));

                    if (_dtbPatient_Remarque.Rows.Count > 0)
                    {
                        chkSurPlace.Checked = (Convert.ToInt32(_dtbPatient_Remarque.Rows[0]["Encaisse"]) == 1);
                        chkCessionCreance.Checked = (Convert.ToInt32(_dtbPatient_Remarque.Rows[0]["Cession"]) == 1);
                    }
                    else
                    {
                        chkSurPlace.Checked = false;
                        chkCessionCreance.Checked = false;
                    }
                }
                catch (Exception ex)
                {
                    this.Cursor = Cursors.Default;
                    throw new Exception(ex.Message);
                    //MessageBox.Show(ex.Message.ToString());
                }
                finally
                {
                    if (z_blnConnect)
                    {
                        SosMedecins.SmartRapport.DAL.Variables.ConnexionBase.CloseBDD();
                    }
                }

                //On active l'ajout de documents
                bAjoutDoc.ImageIndex = 1;
                bAjoutDoc.Enabled = true;

                ChargementFactureTermine = true;
                this.Cursor = Cursors.Default;
            }
            else
            {
                //Pas de facture...on désactive l'ajout de documents
                bAjoutDoc.ImageIndex = 0;
                bAjoutDoc.Enabled = false;
            }
		}

		private void SelectionTypeEnvoi(int type)
		{
			cbEnvoi.SelectedIndex=-1;
			for(int i=0;i<cbEnvoi.Items.Count;i++)
			{
				ListItem item = (ListItem)cbEnvoi.Items[i];
				if((int)item.objValue==type)
				{
					cbEnvoi.SelectedIndex=i;
					return;
				}
			}
		}
		private void SelectionTypeAssurance(int type)
		{
			cbTypeAss.SelectedIndex=-1;
			for(int i=0;i<cbTypeAss.Items.Count;i++)
			{
				ListItem item = (ListItem)cbTypeAss.Items[i];
				if((int)item.objValue==type)
				{
					cbTypeAss.SelectedIndex=i;
					return;
				}
			}
		}
		private void SelectionTTT(int type)
		{			            
            cbTTT.SelectedIndex=-1;
			for(int i=0;i<cbTTT.Items.Count;i++)
			{
				ListItem item = (ListItem)cbTTT.Items[i];
				if((int)item.objValue==type)
				{
					cbTTT.SelectedIndex=i;
					return;
				}
			}
		}
		private void SelectionSortie(int type)
		{
			cbSortie.SelectedIndex=-1;
			for(int i=0;i<cbSortie.Items.Count;i++)
			{
				ListItem item = (ListItem)cbSortie.Items[i];
				if((int)item.objValue==type)
				{
					cbSortie.SelectedIndex=i;
					return;
				}
			}
		}
		private void SelectionTarif(int type)
		{			            
            cbTarif.SelectedIndex=-1;
			for(int i=0;i<cbTarif.Items.Count;i++)
			{
				ListItem item = (ListItem)cbTarif.Items[i];
				if(((Facture_Tarifs)item.objValue).intType==type)
				{
					cbTarif.SelectedIndex=i;
					return;
				}
			}
		}

		// Mise en place d'une consultation
		private void MiseEnPlaceConsultation(DataRow rowConsultation)
		{
			this.Cursor = Cursors.WaitCursor;

			ChargementFactureTermine = false;
            chargeListe = false;        //On empêche le chargement inutile de la liste des prestations Tarmed

			InitializeControls();
			InitializeData();

			txtNFacture.Text = "";
			txtNFacture.Enabled = false;
			txtNConsultation.Enabled = false;
			DocumentEnCours.Clear();
			txtNConsultation.Text=rowConsultation["Nconsultation"].ToString();
			txtNomMedecin.Text = rowConsultation["NomMedecinSos"].ToString();
			string DtNaissance = rowConsultation["DateNaissance"].ToString();
			if(DtNaissance!="")
				DtNaissance = DateTime.Parse(rowConsultation["DateNaissance"].ToString()).ToString("dd/MM/yyyy");
			txtNomPatient.Text = rowConsultation["NomPatient"].ToString() + " " + rowConsultation["PrenomPatient"].ToString() ;
			txtdateN.Text=  DtNaissance ;
			txtDateCreation.Text=DateTime.Now.ToString();
			txtDateImpression.Text="";
			TxtDateRappel.Text="";
			txtTypeDestinataire.Text="";
			txtTypeDestinataire.Tag=null;
			txtDestinataire.Text="";

			MiseEnPlaceDestinataire(rowConsultation);
            //REvoir ici les controles pour eviter un chargement cbTTT, etc....
			// Valeurs par défaut dans les combo :
			cbEnvoi.SelectedIndex=0;
			cbTTT.SelectedIndex=0;
			cbTypeAss.SelectedIndex=1;
			cbSortie.SelectedIndex=2;
			cbDocJoint.SelectedIndex=-1;
			cbDocJoint.Text="";
			
            //########## ICI  #######Vérifier la date pour prendre le bon tarmed!!!!! 10.04.2018       
            if ((cbTarif.Text == "Tarmed fédéral" && (cbTTT.Text == "Maladie" || cbTTT.Text == "Accident")) || cbTarif.Text == "Police" || cbTarif.Text == "Usage")
            {
                //ChargementComboPrestation("LAA-AM-AI");    //LAA-AM-AI
                TarmedVersion = "LAA-AM-AI";
                scale_factor_mt = 1;               
            }
            else
            {
               //Sinon c'est LAMAL Mais on regarde la date de la visite
               string[] Resultat = OutilsExt.OutilsSql.Quel_Tarmed_PListe(long.Parse(txtNConsultation.Text));   //Domi 10.04.2018

               TarmedVersion = Resultat[0].ToString();

                if (TarmedVersion == "LAA-AM-AI")
                    scale_factor_mt = 1;
                else
                {
                    //en fonction du médecin
                    scale_factor_mt = OutilsExt.OutilsSql.Val_Scale_factor_mt(long.Parse(txtNConsultation.Text));   //Domi 16.06.2022
                    //scale_factor_mt = 0.93;
                }

              // ChargementComboPrestation(TarmedVersion);    
            }
                        
			ChargementFactureTermine = true;

            //on récupère les infos concernant le rapport à partir du n° de consult
                string[][] inforapport = OutilsExt.OutilsSql.ExecuteCommandeAvecTabString("SELECT Auteur,Type_long_rapport from tableconsultations WHERE NConsultation = " + txtNConsultation.Text);
            

                string Auteur = "";
                string Type_rapport = "";

                //si on a quelque chose, et en fonction des résultats, on défini la chaine
                if (inforapport != null && inforapport.Length == 1)
                {
                    switch (inforapport[0][0])
                    {
                        case "0":
                            Auteur = "Anne";
                            break;
                        case "1":
                            Auteur = "Aurore";
                            break;
                        case "2":
                            Auteur = "Catherine";
                            break;
                        case "3":
                            Auteur = "Chantal";
                            break;
                        case "4":
                            Auteur = "Suzanne";
                            break;
                        case "5":
                            Auteur = "Roseline";
                            break;
                        case "6":
                            Auteur = "Adrienne";
                            break;
                        case "7":
                            Auteur = "Audrey";
                            break;
                        case "8":
                            Auteur = "Lina";
                        break;
                    default:
                            Auteur = "";
                            break;
                    }

                    switch (inforapport[0][1])
                    {
                        case "0":
                            Type_rapport = "Petit rapport fait par ";
                            break;
                        case "1":
                            Type_rapport = "Moyen rapport fait par ";
                            break;
                        case "2":
                            Type_rapport = "Grand rapport fait par ";
                            break;
                        default:
                            Type_rapport = "";
                            break;
                    }
                    //on défini la chaine
                    labelInfoRapport.Text = Type_rapport + Auteur;
                }
                else labelInfoRapport.Text = "";   //sinon on a rien

                //***07.02.2011*************************
                //Affichage d'une alerte pour signaler que le patient doit avoir signé une cession de créance
                //on récupère les infos concernant le rapport à partir du n° de consult
                string[][] AlerteCession = OutilsExt.OutilsSql.ExecuteCommandeAvecTabString("SELECT pr.cession from patient_remarque pr, tableconsultations tc WHERE pr.Idpatient = tc.IndicePatient and tc.NConsultation = " + txtNConsultation.Text);

                //string Cession = "";

                //si on a un enregistrement
                if (AlerteCession != null && AlerteCession.Length == 1)
                {
                    if (AlerteCession[0][0] == "1")
                        PBoxCession.Visible = true;             //on active le logo Cession de Créance
                    else PBoxCession.Visible = false;           //logo désactivé
                }
                else PBoxCession.Visible = false;//logo désactivé
              

			this.Cursor = Cursors.Default;
		}

		private void MiseEnPlaceDestinataire(DataRow rowConsultation)
		{			
			Dest dest = new Dest();
			
			//Date de consultation à prendre en compte
			string strDateConsult = "";

            if (rowConsultation["DAP"].ToString() != DBNull.Value.ToString())
            {
                strDateConsult = OutilsExt.OutilsSql.DateFormatMySql(DateTime.Parse(rowConsultation["DAP"].ToString()));
            }
            else
            {
                strDateConsult = rowConsultation["DateConsultation"].ToString();
            }
           /* if (rowConsultation["DSL"].ToString()!= DBNull.Value.ToString())
			{
				strDateConsult = OutilsExt.OutilsSql.DateFormatMySql(DateTime.Parse(rowConsultation["DSL"].ToString()));
			}
			else
			{
				strDateConsult = OutilsExt.OutilsSql.DateFormatMySql(DateTime.Parse(rowConsultation["DAP"].ToString()));
			}*/

			// On verifie s'il y a un destinataire spécialement
			// pour la date en cours,
			string[][] retour = OutilsExt.OutilsSql.ExecuteCommandeAvecTabString("SELECT TypeDestinataire,IdDestinataire from fac_destinatairefacture where IdPatient = " + rowConsultation["IndicePatient"].ToString() + " AND DateDebut<= '" + strDateConsult + "' AND DateFin >= '" + strDateConsult + "'");
			if(retour!=null && retour.Length>0)
			{
				dest.m_TypeDestinataire= (CtrlDest.TypeDestinataire)int.Parse(retour[0][0]);
				dest.CodeDestinataireFacture = int.Parse(retour[0][1]);
			}
			else
			{
				// si ce n'est pas le case, on utilise le destinataire par défaut :
				string[][] retour2 = OutilsExt.OutilsSql.ExecuteCommandeAvecTabString("SELECT TypeDestinataireFacture,IdDestinataireFacture from tablepatient where IdPatient = " + rowConsultation["IndicePatient"].ToString());
				// par défaut le destinataire est déifini dans la tablepatient :
				if(retour2!=null && retour2.Length==1)
				{
					dest.m_TypeDestinataire= (CtrlDest.TypeDestinataire)int.Parse(retour2[0][0]);
					dest.CodeDestinataireFacture = int.Parse(retour2[0][1]);
				}
					// par défaut le destinataire est déifini dans la table :
				else
				{
					dest.m_TypeDestinataire = CtrlDest.TypeDestinataire.Idem;
					dest.CodeDestinataireFacture=0;
				}
			}

			if(dest.m_TypeDestinataire==CtrlDest.TypeDestinataire.Idem)
			{
				cbEnvoi.SelectedIndex=0;
				dest.AdresseDestinataire = OutilsExt.OutilsSql.GetAdresseFromPatient(int.Parse(rowConsultation["IndicePatient"].ToString()));
			}
			else if(dest.m_TypeDestinataire==CtrlDest.TypeDestinataire.Hotel)
			{
				cbEnvoi.SelectedIndex=1;
				dest.AdresseDestinataire = OutilsExt.OutilsSql.GetAdresseFromHotel(dest.CodeDestinataireFacture);
			}
			else if(dest.m_TypeDestinataire==CtrlDest.TypeDestinataire.Assurance)
			{
				cbEnvoi.SelectedIndex=2;
				dest.AdresseDestinataire = OutilsExt.OutilsSql.GetAdresseFromAssurance(dest.CodeDestinataireFacture);
			}
			else if(dest.m_TypeDestinataire==CtrlDest.TypeDestinataire.Commissariat)
				dest.AdresseDestinataire = OutilsExt.OutilsSql.GetAdresseFromCommissariat();
			else if(dest.m_TypeDestinataire==CtrlDest.TypeDestinataire.Tiers)
			{
				cbEnvoi.SelectedIndex=1;
				dest.AdresseDestinataire = OutilsExt.OutilsSql.GetAdresseFromTiers(dest.CodeDestinataireFacture);
			}
			
			txtTypeDestinataire.Tag = dest;
			txtTypeDestinataire.Text = dest.m_TypeDestinataire.ToString();
			txtDestinataire.Text=dest.AdresseDestinataire;
		}

		#endregion

		#region Evenements

		#region Evenements sur le formulaire

		public void EnvoiCommande(Keys keyBtn)
		{
			if(keyBtn==Keys.F7)
			{
				if(rdMat.Checked)
					rdPrest.Checked = true;
				else if(rdPrest.Checked)
					rdMat.Checked =true;
				return;
			}
			else if(keyBtn==Keys.F10)
			{
                if (btnSave.Visible) btnSave_Click(null, null);
                else if (btnModif.Visible) btnModif_Click(null, null);
			}
			else if(keyBtn==Keys.F1)
			{
				BtnFindFacture_Click(null,null);
			}
			else if(keyBtn==Keys.F12)
			{
				btnQuitter_Click(null,null);
			}

			// si c'est la touche entrée et que l'on est sur les prestations, on recherche les
			// prestations liées
			
			if(rdPrest.Checked && keyBtn==Keys.F9 && TxtCode.Text!="" && TxtCode.Tag==null && lwResultats.Items.Count==0)
			{
				ValidationPrestationsLiees(GetPrestationsLieesByString(TxtCode.Text));
			}
			else if(keyBtn==Keys.F9 && TxtCode.Text!="" && TxtCode.Tag!=null)
			{
				btnPrestationOk_Click(null,null);
			}		
			else if(keyBtn==Keys.F9 && TxtCode.Text!="" && TxtCode.Tag==null && lwResultats.Items.Count==1)
			{
				if(lwResultats.Items.Count==1)
				{
					lwResultats.Visible = true;
					lwResultats.Focus();
					lwResultats.Items[0].Selected = true;
					SelectionMaterielOuPrestation();
					btnPrestationOk_Click(null,null);
					lwResultats.Visible = false;
				}
			}
		}

		private void cbEnvoi_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(m_datarowAppel!=null && ChargementFactureTermine)
			{
				if(cbEnvoi.SelectedIndex>-1)
				{
					ListItem item = (ListItem)cbEnvoi.SelectedItem;
					if(cbEnvoi.Text.ToString() == "Tiers" ) 
					{

						frmTiers frm  = new frmTiers(m_TypeOuverture);
						frm.ShowDialog();
						DestinataireRetour = frm.GetDestinataire();
						frm.Dispose();
						frm=null;
					}
					
					if(cbEnvoi.Text.ToString() == "Assurance" ) 
					{
						Dest dest = new Dest();
						// Quelle est la date de consultation à prendre en compte? Sur les lieux? appel?
						string strDateConsult = "";
						if(m_datarowAppel["DSL"].ToString()!=System.DBNull.Value.ToString())
						{
							strDateConsult = OutilsExt.OutilsSql.DateFormatMySql(DateTime.Parse(m_datarowAppel["DSL"].ToString()));
						}
						else
						{
							strDateConsult = OutilsExt.OutilsSql.DateFormatMySql(DateTime.Parse(m_datarowAppel["DAP"].ToString()));
						}
						// On verifie s'il y a un destinataire 10 %
						// pour la date en cours,
						string[][] retour10 = OutilsExt.OutilsSql.ExecuteCommandeAvecTabString("SELECT TypeDestinataire,IdDestinataire from fac_destinatairefacture10 where IdPatient = " + m_datarowAppel["IndicePatient"].ToString() + " AND DateDebut<= '" + strDateConsult + "' AND DateFin >= '" + strDateConsult + "'");
						if(retour10!=null && retour10.Length==1)
						{
							dest.m_TypeDestinataire= (CtrlDest.TypeDestinataire)int.Parse(retour10[0][0]);
							dest.CodeDestinataireFacture = int.Parse(retour10[0][1]);
							dest.AdresseDestinataire = OutilsExt.OutilsSql.GetAdresseFromTiers(dest.CodeDestinataireFacture);
							tbDestinataire10.Text = dest.AdresseDestinataire;
						}
						else
						{
							// si ce n'est pas le case, on utilise le destinataire par défaut :
							string[][] retour2 = OutilsExt.OutilsSql.ExecuteCommandeAvecTabString("SELECT TypeDestinataireFacture,IdDestinataireFacture from tablepatient where IdPatient = " + m_datarowAppel["IndicePatient"].ToString());
							// par défaut le destinataire est déifini dans la tablepatient :
							if(retour2!=null && retour2.Length==1)
							{
								dest.m_TypeDestinataire= (CtrlDest.TypeDestinataire)int.Parse(retour2[0][0]);
								dest.CodeDestinataireFacture = int.Parse(retour2[0][1]);
								dest.AdresseDestinataire = OutilsExt.OutilsSql.GetAdresseFromPatient(int.Parse(m_datarowAppel["IndicePatient"].ToString()));
								tbDestinataire10.Text = dest.AdresseDestinataire;

							}
								// par défaut le destinataire est déifini dans la table :
							else
							{
								dest.m_TypeDestinataire = CtrlDest.TypeDestinataire.Idem;
								dest.CodeDestinataireFacture=0;
								dest.AdresseDestinataire = OutilsExt.OutilsSql.GetAdresseFromPatient(int.Parse(m_datarowAppel["IndicePatient"].ToString()));
								tbDestinataire10.Text = dest.AdresseDestinataire;
							}
						}
						frmAssurance frm  = new frmAssurance(m_TypeOuverture);
						frm.ShowDialog();
						DestinataireRetour = frm.GetDestinataire();
						frm.Dispose();
						frm=null;
					}										
				}		
			}
		}

		#endregion

		#region Evenements par controles

		private void txtNFacture_TextChanged(object sender, System.EventArgs e)
		{
			if(txtNFacture.Text!="")
				rdNFacture.Checked = true;
			else if(txtNConsultation.Text!="")
				rdNConsultation.Checked = true;
		}

		private void txtNConsultation_TextChanged(object sender, System.EventArgs e)
		{
            if (txtNConsultation.Text != "")
            {
                rdNConsultation.Checked = true;
                //On initialise la variable scale_factor_mt (on ne sait jamais)
               // scale_factor_mt = OutilsExt.OutilsSql.Val_Scale_factor_mt(long.Parse(txtNConsultation.Text));
            }
            else if (txtNFacture.Text != "")
                rdNFacture.Checked = true;
		}

		private void rdPrest_CheckedChanged(object sender, System.EventArgs e)
		{
            if (rdPrest.Checked)
            {
                //On recharge la liste des prestations en fonction des cbTarif, cbTTT et cbTypeAss
                if ((cbTarif.Text == "Tarmed fédéral" && (cbTTT.Text == "Maladie" || cbTTT.Text == "Accident")) || cbTarif.Text == "Police" || cbTarif.Text == "Usage")
                {
                    ChargementComboPrestation("LAA-AM-AI");    //LAA-AM-AI
                    scale_factor_mt = 1;
                }
                else
                {
                    //Sinon c'est LAMAL et Mais on regarde la date de la visite
                    if (txtNConsultation.Text != "")
                    {
                        string[] Resultat = OutilsExt.OutilsSql.Quel_Tarmed_PListe(long.Parse(txtNConsultation.Text));   //Domi 10.04.2018

                        TarmedVersion = Resultat[0].ToString();
                    }

                    if (TarmedVersion == "LAA-AM-AI")
                        scale_factor_mt = 1;
                    else
                    {
                        //en fonction du médecin
                        scale_factor_mt = OutilsExt.OutilsSql.Val_Scale_factor_mt(long.Parse(txtNConsultation.Text));   //Domi 16.06.2022
                        //scale_factor_mt = 0.93;    //valeur par defaut
                    }
                     
                    ChargementComboPrestation(TarmedVersion);    
                                        
                    //ChargementComboPrestation("LAMAL");    //LAMAL
                    //scale_factor_mt = OutilsExt.OutilsSql.Val_Scale_factor_mt(long.Parse(txtNConsultation.Text));
                }
            }
			TxtCode.Focus();
		}

		private void rdMat_CheckedChanged(object sender, System.EventArgs e)
		{
			if(rdMat.Checked)
				ChargementComboMateriel();
			TxtCode.Focus();
		}

		private void cbTarif_SelectedIndexChanged(object sender, System.EventArgs e)
		{
            if (chargeListe == true)
            {
                if (cbTarif.SelectedIndex > -1)
                {
                    ListItem item = (ListItem)cbTarif.SelectedItem;
                    Facture_Tarifs tarif = (Facture_Tarifs)item.objValue;
                    cbSortie.SelectedIndex = (int)tarif.TypeSortie;

                    //On recharge la liste des prestations...Ainsi que le Coeff médecin
                    if ((cbTarif.Text == "Tarmed fédéral" && (cbTTT.Text == "Maladie" || cbTTT.Text == "Accident")) || cbTarif.Text == "Police" || cbTarif.Text == "Usage")
                    {
                        ChargementComboPrestation("LAA-AM-AI"); //LAA-AM-LAI
                        scale_factor_mt = 1;
                    }
                    else
                    {   //Sinon c'est LAMAL et Mais on regarde la date de la visite

                        string[] Resultat = OutilsExt.OutilsSql.Quel_Tarmed_PListe(long.Parse(txtNConsultation.Text));   //Domi 10.04.2018

                        TarmedVersion = Resultat[0].ToString();

                        if (TarmedVersion == "LAA-AM-AI")
                            scale_factor_mt = 1;
                        else
                        {
                            //en fonction du médecin
                            scale_factor_mt = OutilsExt.OutilsSql.Val_Scale_factor_mt(long.Parse(txtNConsultation.Text));   //Domi 16.06.2022
                            //scale_factor_mt = 0.93;
                        }

                        ChargementComboPrestation(TarmedVersion);
                    }
                }
            }
		}

		private void btnNouveauDestinataire_Click(object sender, System.EventArgs e)
		{
			FIP m_fip = new FIP(this._frmgeneral,long.Parse(m_datarowAppel["IndicePatient"].ToString()),FIP.TypeOuverture.Facturation);
			m_fip.ShowDialog();
			m_fip.Dispose();
			m_fip=null;
			MiseEnPlaceDestinataire(m_datarowAppel);
		}		

		private void txtNConsultation_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Enter)
				BtnFindFacture_Click(null,null);
		}

		private void lwResultats_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Enter)
			{
				SelectionMaterielOuPrestation();			
			}
			else if(e.KeyCode==Keys.Escape)
			{
				lwResultats.Visible = false;
				TxtCode.Focus();
			}
		}

		private void lwResultats_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(e.Button==MouseButtons.Left)
			{
				SelectionMaterielOuPrestation();
			}
		}	

		private void TxtCode_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			TxtCode.Tag=null;
			lwResultats.Visible = false;

		    if(e.KeyCode==Keys.Down)
			{
				lwResultats.Visible = true;
				lwResultats.Focus();
				lwResultats.Items[0].Selected = true;
			}
			else if(e.KeyCode==Keys.Escape)
			{
				lwResultats.Visible = false;
			}
			else 
			{
				if(rdMat.Checked)
				{
					ChargementComboMateriel(TxtCode.Text);
				}
				else if(rdPrest.Checked)
				{					
                    ChargementComboPrestationCode(TxtCode.Text);
				}	

				if( lwResultats.Items.Count==1 && lwResultats.Items[0].Text==TxtCode.Text)
				{
					lwResultats.Visible = true;
					lwResultats.Focus();
					lwResultats.Items[0].Selected = true;
					SelectionMaterielOuPrestation();
					lwResultats.Visible = false;
					TxtCode.Focus();
					TxtCode.SelectionLength=0;
					TxtCode.SelectionStart = TxtCode.Text.Length;
				}
			}			
		}

		#endregion

		#endregion

		#region Aide à la saisie des prestations		
	
		private Facture_PrestationLiee[] GetPrestationsLieesByString(string Saisie)
		{
			string[][] retour = OutilsExt.OutilsSql.ExecuteCommandeAvecTabString("SELECT p.NPrestation,p.PrestLOrdre,p.PrestLType,p.PrestLLibelle,p.PrestQte from fac_prestations_liees p where p.NPrestLiee='" + Saisie.Replace("'","''") + "' ORDER BY p.PrestLOrdre");
			if(retour!=null)
			{
				Facture_PrestationLiee[] Prestations = new Facture_PrestationLiee[retour.Length];
				for(int i=0;i<retour.Length;i++)
				{
					Facture_Prestation prest = Facture_Prestation.GetFacture_PrestationByNPrestation(Statiques_Data.TabPrestations,retour[i][0]);
					if (prest != null)                    
                        Prestations[i] = new Facture_PrestationLiee(prest,int.Parse(retour[i][4]),retour[i][3],int.Parse(retour[i][1]),int.Parse(retour[i][2]));
                    else 
                    {
                        MessageBox.Show("Cette prestation Liée ne correspond pas à ce Tarmed.", "Positions Tarmed", MessageBoxButtons.OK, 
		                                  MessageBoxIcon.Exclamation);
                        return null;
                    }
				}
				return Prestations;
			}
			else
				return null;
		}	
	
		private void SelectionMaterielOuPrestation()
		{
			if(lwResultats.SelectedIndices.Count>0)
			{
				ListViewItem item = (ListViewItem)lwResultats.SelectedItems[0];
				if(item.Tag.GetType()==typeof(Facture_Prestation))
				{					                    
                    Facture_Prestation prest = (Facture_Prestation)item.Tag;
					TxtCode.Text = prest.NPrestation;
					TxtCode.Tag = prest;
					txtLibelle.Text = prest.Libelle;
					txtQte.Text = "1";
					
                    switch(prest.NPrestation)
                    {
                        case "00.9020":
                        case "00.9021":
                        case "00.9022":                        
                        case "020":
                        case "030":
                        case "040":
                        case "050":
                        case "060":
                        case "070":
                        case "080":
                        case "090":
                        case "069":
                        case "067":
                            {
                                txtCoeff.Text = String.Format("{0:n}", 1);
                                Double Prix = Math.Round(prest.PrestPoints + prest.PrestPointsT, 2);        //19.01.2018                               
                                txtPrix.Text = Prix.ToString();
                                break;
                            }
                        default:
                            {
                                txtCoeff.Text = String.Format("{0:n}", scale_factor_mt);
                                Double Prix = Math.Round((prest.PrestPoints * scale_factor_mt) + prest.PrestPointsT,2);        //06.10.2017
                                //Double Prix = prest.PrestPoints + prest.PrestPointsT ;
                                txtPrix.Text = Prix.ToString();
                                break;
                            }
                    }
                                     					
				}
				else if(item.Tag.GetType()==typeof(Facture_Materiel))
				{
					Facture_Materiel mat = (Facture_Materiel)item.Tag;
					TxtCode.Tag = mat;
					TxtCode.Text =mat.NMateriel;
					txtLibelle.Text  = mat.Libelle;
					txtQte.Text = "1";
					txtCoeff.Text="1";
					txtPrix.Text = mat.Prix.ToString();					
				}

				lwResultats.Visible = false;
				txtQte.Focus();
			}
		}

		#region Opérations sur le détail des prestations facturées

		private void TxtCode_TextChanged(object sender, System.EventArgs e)
		{
			if(TxtCode.Text=="")
				lwResultats.Visible = false;
		}

		private void fpFactures_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			FarPoint.Win.Spread.Model.CellRange		range = fpFactures.GetCellFromPixel(0,0,e.X,e.Y);
			if(range.Row>-1 && range.Column>-1)
			{
				// Bouton droit = menu contextuel
				if(e.Button == MouseButtons.Right)
				{
					fpFactures_Sheet1.SetActiveCell(range.Row,range.Column);
					ContextMenu menu = new ContextMenu();
					MenuItem mnu = new MenuItem("Supprimer la prestation");
					mnu.Click+=new EventHandler(ContextMenu_Detail_Click);
					menu.MenuItems.Add(mnu);
					mnu = new MenuItem("Supprimer Tout");
					mnu.Click+=new EventHandler(ContextMenu_Detail_Click);
					menu.MenuItems.Add(mnu);
					menu.Show(fpFactures,new Point(e.X,e.Y));
				}
			}		
		}
		#endregion
	
		

		#endregion

		#region Evenements de fonctionnalités du controle

		//On veut générer la facture pour la consultation courante :
		public void btnNew_Click(object sender, EventArgs e)
		{
			if(m_datarowAppel != null)
			{
                //Si le médecin est "Indépendant", et s'il a fait moins de 5 factures dans le mois en cour,
                //On informe qu'il faut lui faire une facture avec son RCC
                int nbFact = nbFactureIndependant(int.Parse(m_datarowAppel["CodeIntervenant"].ToString()));
                
                if (nbFact >= 0 && nbFact < 5)
                {                 
                    MessageBox.Show("Attention, ce médecin n'a fait que " + nbFact + " factures en tant qu'indépendant ce mois ci." 
                                    + "\r\nPenser à lui en faire d'autre, en sortie IMPRIMANTE.", 
                                    "Facturer en tant qu'indépendant", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                MiseEnPlaceConsultation(m_datarowAppel);
				BlocageControles(false);
                _drwFacture = OutilsExt.OutilsSql.GetNewFactureWithNConsult(long.Parse(txtNConsultation.Text));
               
                //On recupère le facteur scalaire Scale_factor_mt du médecin mais attention a changer par la suite en fct du tarif
                double Retour = OutilsExt.OutilsSql.Val_Scale_factor_mt(long.Parse(txtNConsultation.Text));   //Domi 29.03.2018
                scale_factor_mt = Retour;
                TarmedVersion = "LAMAL";    //Par défaut

                MiseEnPlaceFacture(_drwFacture);
			}
		}
       

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			// Cas où c'est une nouvelle facture  :
            if (txtNConsultation.Text != "" && txtNFacture.Text != "" && !txtNConsultation.Enabled && !txtNFacture.Enabled && _drwFacture != null)
			{
                //Vient elle d'être créée?               
                int Etatfacure = Constantes.CREATION_FACTURE;
                
                bool factureExiste = FactureDejaCreee(txtNFacture.Text);

                //Dans ce cas c'est une modification
                if (factureExiste == true)
                {
                    Etatfacure = Constantes.MODIFICATION_FACTURE;
                    //on audite
                    if (VariablesApplicatives.Utilisateurs.Identifiant.ToString() == "A001")
                        mouchard.evenement("Modification de la facture...n° " + txtNFacture.Text, VariablesApplicatives.Utilisateurs.NomUtilisateur.ToString());  //log)
                }
                else
                {
                    //on audite
                    if (VariablesApplicatives.Utilisateurs.Identifiant.ToString() == "A001")
                        mouchard.evenement("Création de la facture...n° " + txtNFacture.Text, VariablesApplicatives.Utilisateurs.NomUtilisateur.ToString());  //log)
                }
                
                
                this.Cursor = Cursors.WaitCursor;
				string[][] Lignes = SaveElementOfFrame();
               
                // Procédure de sauvegarde :
                bool reussite = SauvegardeFacture(Lignes);

                Fonction z_objFonctionDal = new Fonction();
                z_objFonctionDal.EnregistreModification(_drwFacture["NConsultation"].ToString(), VariablesApplicatives.Utilisateurs.Identifiant, DateTime.Now, Etatfacure, "");
                // mise en base
                this.Cursor = Cursors.Default;

                InitializeControls();
                InitializeData();
                PreparationPourRechercheConsult();
                // fin de la procedure
                lbStatusOp.Text = "Facture n°" + _drwFacture["NFacture"].ToString() + " sauvegardée avec succès";
			}
        }
       

        private string[][] SaveElementOfFrame()
		{
			// Sauvegarde des données générales de la facture
			if(cbEnvoi.SelectedIndex>-1)
                _drwFacture["TypeEnvoi"] = ((ListItem)cbEnvoi.SelectedItem).objValue.ToString();
			else
                _drwFacture["TypeEnvoi"] = "-1";
			if(cbTypeAss.SelectedIndex>-1)
                _drwFacture["TypeAssurance"] = ((ListItem)cbTypeAss.SelectedItem).objValue.ToString();
			else
                _drwFacture["TypeAssurance"] = "-1";
			if(cbSortie.SelectedIndex>-1)
                _drwFacture["TypeSortie"] = ((ListItem)cbSortie.SelectedItem).objValue.ToString();
			else
                _drwFacture["TypeSortie"] = "-1";
			if(cbTTT.SelectedIndex>-1)
                _drwFacture["TTT"] = ((ListItem)cbTTT.SelectedItem).objValue.ToString();
			else
                _drwFacture["TTT"] = "-1";
			if(cbTarif.SelectedIndex>-1)
                _drwFacture["Tarif"] = ((Facture_Tarifs)((ListItem)cbTarif.SelectedItem).objValue).intType.ToString();
			else
                _drwFacture["Tarif"] = "-1";

            _drwFacture["Commentaire"] = txtCommentaire.Text;
            _drwFacture["RefPatient"] = txtRef.Text;
            _drwFacture["FactNum_AVS"] = txtAVS.Text;
            
            //****Domi 05.04.2013   On met egalement à jour les N° AVS et Assurance dans la table Personne
            _drwFacture["Num_Assure"] = txtRef.Text;
            _drwFacture["Num_AVS"] = txtAVS.Text;            
            //*****

            _drwFacture["NAccident"] = txtAccident.Text;
			if(txtDateAcc.Text!="")
                _drwFacture["DateAccident"] = DateTime.Parse(txtDateAcc.Text).ToString("dd/MM/yyyy");
			else
                _drwFacture["DateAccident"] = "";


			if(cbDocJoint.SelectedIndex>-1)
                _drwFacture["TypeDocJoint"] = ((Facture_DocJoint)((ListItem)cbDocJoint.SelectedItem).objValue).TypeDoc;
			else
                _drwFacture["TypeDocJoint"] = "0";

            string[][] Lignes = null;

            //Si cette facture n'a pas été envoyée
            //if (_drwFacture["FacDateEnvoyee"] != DBNull.Value)
            if (chkEnvoye.Checked == false)
            {
                _drwFacture["TotalFacture"] = TxtTotalFacture.Text;

                Lignes = new string[fpFactures_Sheet1.RowCount][];

                // Sauvegarde du détail des prestations effectuées : 
                for (int i = 0; i < fpFactures_Sheet1.RowCount; i++)
                {
                    Lignes[i] = new string[7];
                    Lignes[i][0] = fpFactures_Sheet1.Cells[i, 0].Text;
                    Lignes[i][1] = fpFactures_Sheet1.Cells[i, 1].Text;
                    Lignes[i][2] = fpFactures_Sheet1.Cells[i, 3].Text;
                    Lignes[i][3] = fpFactures_Sheet1.Cells[i, 4].Text;
                    Lignes[i][4] = fpFactures_Sheet1.Cells[i, 7].Text;
                    Lignes[i][5] = "-1";
                    Lignes[i][6] = fpFactures_Sheet1.Cells[i, 5].Text;
                    for (int j = 0; j < cbTarif.Items.Count; j++)
                        if (((Facture_Tarifs)((ListItem)cbTarif.SelectedItem).objValue).LibelleType == fpFactures_Sheet1.Cells[i, 8].Text)
                            Lignes[i][5] = ((Facture_Tarifs)((ListItem)cbTarif.SelectedItem).objValue).intType.ToString();
                }

                // mise à jour du solde  
                if (!chkAcquitte.Checked)  //si cette facture n'est pas acquitée
                {
                    //On regarde si elle a déjà été envoyée (1er envoi, ou solde restant (10%))
                    //Si c'est PAS le cas et qu'envoi solde n'est pas coché, on met a jour le solde, sinon on touche à rien
                    if (DejaEnvoye(_drwFacture["NFacture"].ToString()) == false && CBoxRenvoiFact10.Checked == false)
                        _drwFacture["Solde"] = _drwFacture["TotalFacture"];
                }
                else
                {
                    _drwFacture["Solde"] = 0;
                }                                                                                  
            }

			Dest dest = (Dest)txtTypeDestinataire.Tag;

            _drwFacture["TypeDestinataire"] = (int)dest.m_TypeDestinataire;
            _drwFacture["CodeDestinataire"] = dest.CodeDestinataireFacture;
            _drwFacture["AdresseDestinataire"] = txtDestinataire.Text;
            _drwFacture["AdresseDestinataire2"] = tbDestinataire10.Text;
            //
            if (chkCession.Checked == true)
            {
                _drwFacture["FacDateCession"] = dbxCession.ValeurDate;
            }
            else
            {
                _drwFacture["FacDateCession"] = DBNull.Value;
            }

            // date encaissement
            if (txtDateEnc.Text.Length > 0)
                _drwFacture["FacDateEncaissee"] = DateTime.Parse(txtDateEnc.Text).ToString("dd/MM/yyyy");
            else
            {
                if (chkEncaisse.Checked == true)
                {
                    _drwFacture["FacDateEncaissee"] = DateTime.Now.ToString("dd/MM/yyyy");
                }
            }
            //
            if (TxtDateRappel.Text.Length > 0)
                _drwFacture["FacDate1Rappel"] = DateTime.Parse(TxtDateRappel.Text).ToString("dd/MM/yyyy");
            else
                _drwFacture["FacDate1Rappel"] = "";

            if (TxtDateContentieux.Text.Length > 0)
                _drwFacture["FacDateContentieux"] = DateTime.Parse(TxtDateContentieux.Text).ToString("dd/MM/yyyy");
            else
                _drwFacture["FacDateContentieux"] = "";

            //Domi 01/04/2011 Sauvegarde des données générales de la facture suite...

            if (CBEffacerDateSession.Checked)       //Si on veut effacer la date d'envoi de cession
            {
                _drwFacture["CessionEnvoi"] = DBNull.Value;      
            }
                   
            if (CBCessionRecu.Checked)
            {
                if (TBoxDateReceptionCession.Text.Length == 0)     //si on a PAS déjà une date
                {
                    //On met la date du jour
                    _drwFacture["CessionRecu"] = DateTime.Now.ToString("dd/MM/yyyy");
                }
            }
            else _drwFacture["CessionRecu"] = DBNull.Value;


            if (CBoxRenvoiFact10.Checked)      //Renvoi Facture 10%
            {
                _drwFacture["RenvFact10p"] = 1;
            }
            else _drwFacture["RenvFact10p"] = 0;


            if (CBoxRenvoiFranchise.Checked)    //Renvoi Facture en franchise
            {
                _drwFacture["FactFranchise"] = 1;
            }
            else _drwFacture["FactFranchise"] = 0;


            if (CBoxIndelicat.Checked)          //Patient Indélicat (déjà remboursé par assurance mais ne nous paie pas)
            {
                _drwFacture["PatientIndelicat"] = 1;
            }
            else _drwFacture["PatientIndelicat"] = 0;

            if (TBDateStopRappel.Text.Length > 0)      //Rappels bloqués jusqu'au...
                _drwFacture["LimiteStopRappel"] = DateTime.Parse(TBDateStopRappel.Text).ToString("dd/MM/yyyy");
            else
                _drwFacture["LimiteStopRappel"] = DBNull.Value;          

			return Lignes;
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			// Cas où c'est une nouvelle facture  :
            if (txtNConsultation.Text != "" && txtNFacture.Text != "" && !txtNConsultation.Enabled && !txtNFacture.Enabled && _drwFacture != null)
			{
				DialogResult result = MessageBox.Show(" Etes-vous certain de vouloir supprimer cette facture ? ","Suppression",MessageBoxButtons.YesNo,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button2);
				if(result==DialogResult.Yes)
				{
                    Fonction z_objFonctionDal = new Fonction();
                    z_objFonctionDal.SupprimeFacture (txtNConsultation.Text , txtNFacture.Text);
                    z_objFonctionDal.EnregistreModification (txtNConsultation.Text, VariablesApplicatives.Utilisateurs.Identifiant, DateTime.Now, Constantes.SUPPRESSION_FACTURE, string.Empty);
                    z_objFonctionDal = null;

					this._frmgeneral.ChargementHistoriqueFiche(long.Parse(txtNConsultation.Text));
					InitializeControls();
					MiseEnPlaceConsultation(m_datarowAppel);			
					 btnNew.Visible = true;
					// btnSave.Visible = true;
                    // btnModif.Visible = false;
					 btnHisto.Visible = false;
					 btnDelete.Visible = false;
					BlocageControles(true);
				}
			}
		}


		private void btnQuitter_Click(object sender, System.EventArgs e)
		{
			_frmgeneral.m_frmActualFactu.CloseFactu();
		}

		#endregion

		#region Opérations sur la facture

		private void btnPrestationOk_Click(object sender, System.EventArgs e)
		{
			if(TxtCode.Text=="" || TxtCode.Tag==null)
			{
				MessageBox.Show("Attention, aucun code n'est sélectionné");
				return;
			}
			if(txtQte.Text=="")
			{
				MessageBox.Show("Attention, aucune quantité saisie");
				return;
			}
			
			ValidationNouvelleLigne();
		}

		private void ValidationPrestationsLiees(Facture_PrestationLiee[] tableau)
		{
			if(tableau==null) return ;

            //Vérification du nombre de prestations déjà effectué pour ce patient
            for (int i = 0; i < tableau.Length; i++)
            {
                //Quantité valide?                                          
                int nbPrest = VerifNbPrest(tableau[i].NPrestation, tableau[i].Quantite, m_datarowAppel["IndicePatient"].ToString());

                if (nbPrest < 0)
                {
                    //Négatif donc trop de prestations pour cette consult
                    MessageBox.Show("Veuillez enlever " + Math.Abs(nbPrest) + " pour la quantité \r\n de la position " + tableau[i].NPrestation, "Trop de quantité pour la consultation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else if (nbPrest > 0)   //Positif, donc trop pour le trimestre
                {
                    MessageBox.Show("Veuillez enlever cette position car vous dépassez de " + nbPrest + "\r\n le nombre maximum (par trimestre) de la position " + tableau[i].NPrestation, "Trop de quantité par trimestre", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }


                //On vérifie également la position en fonction de l'age. (sauf pour le tarif Fédéral....ancien Tarmed)
                if (TarmedVersion == "LAMAL")
                {
                    string Verif = VerifPositionAge(tableau[i].NPrestation, m_datarowAppel["IndicePatient"].ToString(), DateTime.Parse(m_datarowAppel["DSL"].ToString()));

                    if (Verif == "KO")
                    {
                        MessageBox.Show("La position " + tableau[i].NPrestation + " n'est pas adaptée à l'âge de ce patient", "Position non appropriée", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
            }
            //C'est ok, on continu....


			for(int i=0;i<tableau.Length;i++)
			{
				int nb = fpFactures_Sheet1.RowCount++;

                double Prix = 0;

                //On regarde si on a des polices ou assurances internationnales
                switch (tableau[i].NPrestation)             //19.01.2018
                {
                    case "00.9020":
                    case "00.9021":
                    case "00.9022":
                    case "020":
                    case "030":
                    case "040":
                    case "050":
                    case "060":
                    case "070":
                    case "080":
                    case "090":
                    case "069":
                    case "067":
                        {
                            txtCoeff.Text = String.Format("{0:n}", 1);
                            Prix = (tableau[i].PrestPoints + tableau[i].PrestPointsT) * tableau[i].Quantite;
                            break;
                        }
                    default:
                        {
                            if (txtCoeff.Text == "") txtCoeff.Text = string.Format("{0:n}", scale_factor_mt);
                            Prix = ((tableau[i].PrestPoints * scale_factor_mt) + tableau[i].PrestPointsT) * tableau[i].Quantite;
                            break;
                        }
                }                        
				
                ListItem item = (ListItem)cbTarif.SelectedItem;
				Facture_Tarifs tarif = (Facture_Tarifs)item.objValue;
				Prix = Prix * tarif.ValeurPoints;
				Prix = Math.Round(Prix,2);

				fpFactures_Sheet1.Rows[nb].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
				fpFactures_Sheet1.Cells[nb,0].Text = "1";					
				fpFactures_Sheet1.Cells[nb,1].Text = tableau[i].NPrestation;
				fpFactures_Sheet1.Cells[nb,2].Text = tableau[i].Libelle;
				fpFactures_Sheet1.Cells[nb,3].Text = tableau[i].Quantite.ToString().Replace(",",".");
				//fpFactures_Sheet1.Cells[nb,4].Text = WorkedString.FormatMontantArrondi(Prix);
                fpFactures_Sheet1.Cells[nb, 4].Text = Prix.ToString().Replace(",", ".");
				fpFactures_Sheet1.Cells[nb,5].Text = tableau[i].PrestPoints.ToString().Replace(",",".");
				fpFactures_Sheet1.Cells[nb,5].Tag = tableau[i].PrestPoints.ToString().Replace(",",".");
				fpFactures_Sheet1.Cells[nb,6].Text = tableau[i].PrestPointsT.ToString().Replace(",",".");
				fpFactures_Sheet1.Cells[nb,7].Text = txtCote.Text;
				fpFactures_Sheet1.Cells[nb,8].Text = cbTarif.Text;
				if(tableau[i].bMajoration)
					fpFactures_Sheet1.Cells[nb,9].Text = "1";
				else
					fpFactures_Sheet1.Cells[nb,9].Text = "0";
				if(tableau[i].bHorsMajoration)
					fpFactures_Sheet1.Cells[nb,10].Text = "1";
				else
					fpFactures_Sheet1.Cells[nb,10].Text = "0";

				fpFactures_Sheet1.Cells[nb,8].Text = cbTarif.Text;				
			}

			CalculeTotalFacture();

			InitializeSaisieDetail();
		}

		private void ValidationNouvelleLigne()
		{
			if(TxtCode.Text!="")
			{				                                
                object item = TxtCode.Tag;
				if(item==null) return;

                //Vérification du nombre de prestations déjà effectué pour ce patient
                //Quantité valide?
                int Qte;
                bool result = int.TryParse(txtQte.Text, out Qte);
                if (result)
                {                   
                    int nbPrest = VerifNbPrest(TxtCode.Text, Qte, m_datarowAppel["IndicePatient"].ToString());

                    if(nbPrest < 0)
                    {
                        //Négatif donc trop de prestations pour cette consult
                        MessageBox.Show("Veuillez enlever " + Math.Abs(nbPrest) + " pour la quantité \r\n de la position " + TxtCode.Text, "Trop de quantité pour la consultation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else if (nbPrest > 0)   //Positif, donc trop pour le trimestre
                    {
                        MessageBox.Show("Veuillez enlever cette position car vous dépassez de " + nbPrest + "\r\n le nombre maximum (par trimestre) de la position " + TxtCode.Text, "Trop de quantité par trimestre", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }                    
                }

                //On vérifie également la position en fonction de l'age seulement si c'est LAMAL
                if (TarmedVersion == "LAMAL")
                {
                    string Verif = VerifPositionAge(TxtCode.Text, m_datarowAppel["IndicePatient"].ToString(), DateTime.Parse(m_datarowAppel["DSL"].ToString()));

                    if (Verif == "KO")
                    {
                        MessageBox.Show("La position " + TxtCode.Text + " n'est pas adaptée à l'âge de ce patient", "Position non appropriée", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
             
                //C'est ok, on continu....

				int nb = fpFactures_Sheet1.RowCount++;
				fpFactures_Sheet1.Rows[nb].CellType = new FarPoint.Win.Spread.CellType.TextCellType();

				ListItem itemtarif = (ListItem)cbTarif.SelectedItem;
				Facture_Tarifs tarif = (Facture_Tarifs)itemtarif.objValue;
				
				if(item.GetType()==typeof(Facture_Prestation))
				{
					Facture_Prestation prest = (Facture_Prestation)item;

                    double Prix = 0;

                    //On distingue les prestations ordinaires de la police et des ass. internationales  //19.01.2018                    
                    switch (prest.NPrestation)
                    {
                        case "00.9020":
                        case "00.9021":
                        case "00.9022":
                        case "020":
                        case "030":
                        case "040":
                        case "050":
                        case "060":
                        case "070":
                        case "080":
                        case "090":                       
                            {
                                txtCoeff.Text = String.Format("{0:n}", 1);
                                Prix = ((prest.PrestPoints + prest.PrestPointsT) * int.Parse(txtQte.Text) * tarif.ValeurPoints);        //19.01.2018
                                break;
                            }
                        case "069":   //Les vaccins
                        case "067":
                            {
                                txtCoeff.Text = String.Format("{0:n}", 1);
                                Prix = ((prest.PrestPoints + prest.PrestPointsT) * int.Parse(txtQte.Text));        //19.01.2018                           
                                break;
                            }
                        default:
                            {
                                txtCoeff.Text = String.Format("{0:n}", scale_factor_mt);
                                Prix = (((prest.PrestPoints * scale_factor_mt) + prest.PrestPointsT) * int.Parse(txtQte.Text) * tarif.ValeurPoints);        //06.10.2017                                                               
                                break;
                            }
                    }
                                                                              
                    //if (txtCoeff.Text == "") txtCoeff.Text = string.Format("{0:n}", scale_factor_mt);       //Domi 06.10.2017                 
                    //double Prix = (((prest.PrestPoints * scale_factor_mt) + prest.PrestPointsT) * int.Parse(txtQte.Text) * tarif.ValeurPoints);
					
					Prix = Math.Round(Prix,2);

					fpFactures_Sheet1.Cells[nb,0].Text = "1";			//Le type de prestation, pour le calcul des tarifs		
					fpFactures_Sheet1.Cells[nb,1].Text = prest.NPrestation;
					fpFactures_Sheet1.Cells[nb,2].Text = prest.Libelle;
					fpFactures_Sheet1.Cells[nb,3].Text = txtQte.Text.Replace(",",".");
					//fpFactures_Sheet1.Cells[nb,4].Text = WorkedString.FormatMontantArrondi(Prix);
                    fpFactures_Sheet1.Cells[nb, 4].Text = Prix.ToString().Replace(",", ".");
                    fpFactures_Sheet1.Cells[nb,5].Text = prest.PrestPoints.ToString().Replace(",",".");
					fpFactures_Sheet1.Cells[nb,5].Tag = prest.PrestPoints.ToString().Replace(",",".");
					fpFactures_Sheet1.Cells[nb,6].Text = prest.PrestPointsT.ToString().Replace(",",".");
					fpFactures_Sheet1.Cells[nb,7].Text = txtCote.Text;
					fpFactures_Sheet1.Cells[nb,8].Text = cbTarif.Text;

					if(prest.bMajoration)
						fpFactures_Sheet1.Cells[nb,9].Text = "1";
					else
						fpFactures_Sheet1.Cells[nb,9].Text = "0";
					if(prest.bHorsMajoration)
						fpFactures_Sheet1.Cells[nb,10].Text = "1";
					else
						fpFactures_Sheet1.Cells[nb,10].Text = "0";
				}
				else if(item.GetType()==typeof(Facture_Materiel))
				{
                    double Prix = int.Parse(txtQte.Text) * float.Parse(txtPrix.Text);  //Domi 13.03.2018
					//Prix = Prix * tarif.ValeurPoints * float.Parse(txtCoeff.Text);
					//Prix = Prix * float.Parse(txtCoeff.Text);
					Prix = Math.Round(Prix,2);

					Facture_Materiel mat = (Facture_Materiel)item;
					fpFactures_Sheet1.Cells[nb,0].Text = "2";                    //Le type de prestation, pour le calcul des tarifs		
                    fpFactures_Sheet1.Cells[nb,1].Text = mat.NMateriel;
					fpFactures_Sheet1.Cells[nb,2].Text = mat.Libelle;
					fpFactures_Sheet1.Cells[nb,3].Text = txtQte.Text.Replace(",",".");
					//fpFactures_Sheet1.Cells[nb,4].Text = WorkedString.FormatMontantArrondi(Prix);
                    fpFactures_Sheet1.Cells[nb, 4].Text = Prix.ToString().Replace(",", ".");
					fpFactures_Sheet1.Cells[nb,5].Text = "0";
					fpFactures_Sheet1.Cells[nb,6].Text = "0";
					fpFactures_Sheet1.Cells[nb,7].Text = txtCote.Text;
					fpFactures_Sheet1.Cells[nb,8].Text = cbTarif.Text;	
					fpFactures_Sheet1.Cells[nb,9].Text = "0";
					fpFactures_Sheet1.Cells[nb,10].Text = "0";
				}				
			}

			CalculeTotalFacture();
			InitializeSaisieDetail();
		}

		private void CalculeTotalFacture()
		{
			CalculeMajorations();

			double Total = 0;
			
			for(  int i=0;i<fpFactures_Sheet1.RowCount;i++)
			{	
				if(fpFactures_Sheet1.Cells[i,4].Text=="") fpFactures_Sheet1.Cells[i,4].Text="0";				
                double ligne = double.Parse(fpFactures_Sheet1.Cells[i, 4].Text.Replace(",", "."));
				Total+=ligne;
			}

            if (Total >= 1)
            {
                Total = Math.Round(Total, 2);
                Total = Total * 100;
                string strTotal = Total.ToString();
                TxtTotalFacture.Text = strTotal.Substring(0, strTotal.Length - 2) + "." + strTotal.Substring(strTotal.Length - 2, 2);
                TxtSolde.Text = TxtTotalFacture.Text;
            }
            else
            {
                TxtTotalFacture.Text = Math.Round(Total, 2).ToString().Replace(",", ".");
                TxtSolde.Text = TxtTotalFacture.Text;
            }
		}

		private void CalculeMajorations()
		{
			ListItem item = (ListItem)cbTarif.SelectedItem;
			Facture_Tarifs tarif = (Facture_Tarifs)item.objValue;

            // on parcourt chaque ligne à la recherche d'une prestation majorée :
			for(int i=0;i<fpFactures_Sheet1.RowCount;i++)
			{

                Console.WriteLine(fpFactures_Sheet1.Cells[i, 1].Text);
                Console.WriteLine("i0: " + fpFactures_Sheet1.Cells[i, 0].Text + " i9 : " + fpFactures_Sheet1.Cells[i, 9].Text);
                Console.WriteLine("i10: " + fpFactures_Sheet1.Cells[i, 9].Text + " i5: " + fpFactures_Sheet1.Cells[i, 5].Text);
               
                // Est-ce une prestation?majorée?
				if(fpFactures_Sheet1.Cells[i,0].Text=="1" && fpFactures_Sheet1.Cells[i,9].Text=="1")
				{
					
                    double percent = double.Parse(fpFactures_Sheet1.Cells[i, 5].Tag.ToString().Replace(",", "."));                    
                    double Points = 0;
                    double amount = 0;
                    double SommePoints = 0;
                    double SommeAmount = 0;

					// Dans ce cas on parcourt toutes les lignes précédentes à la recherche de prestations pouvant être majorées
					// donc "Hors majoration" = 0
					for(int j=0;j<i;j++)
					{                       
                        //on calcule pour chaque ligne majoré le total
                        if(fpFactures_Sheet1.Cells[j,10].Text=="0" && fpFactures_Sheet1.Cells[j,5].Text!="")
						{                                                                                   
                            Points = double.Parse(fpFactures_Sheet1.Cells[j, 5].Text.Replace(",", ".")) * int.Parse(fpFactures_Sheet1.Cells[j, 3].Text);
                            amount = Math.Round(Points * int.Parse(fpFactures_Sheet1.Cells[i, 3].Text) * percent * tarif.ValeurPoints * scale_factor_mt, 2);

                            SommeAmount += amount;
                            SommePoints += Points;
                        }
					}
					
                    //On complète les champs
                    fpFactures_Sheet1.Cells[i, 5].Text = Math.Round(SommePoints, 2).ToString();
					// le prix : 					
                    fpFactures_Sheet1.Cells[i, 4].Text = Math.Round(SommeAmount, 2).ToString(); 
				}
			}
		}		

		#endregion		

		#region Menus et menus contextuels

		private void ContextMenu_Detail_Click(object sender,EventArgs e)
		{
			MenuItem mnu = (MenuItem)sender;
			switch(mnu.Text)
			{
				case "Supprimer la prestation":
					fpFactures_Sheet1.ActiveRow.Remove();
					break;
				case "Supprimer Tout":
					DialogResult result = MessageBox.Show("Confirmer la suppresion de tout le tableau?","Suppression",MessageBoxButtons.YesNo, MessageBoxIcon.Warning,MessageBoxDefaultButton.Button2);
					if(result==DialogResult.Yes)
					{
						fpFactures_Sheet1.RowCount=0;
					}
					break;
				default:
					break;
			}

			CalculeTotalFacture();
		}

		#endregion		

		#region Recherche de facture 

		private void BtnFindFacture_Click(object sender, System.EventArgs e)
		{
			if(!txtNConsultation.Enabled)
			{
				txtNConsultation.Enabled = true;
				txtNFacture.Enabled = true;

				if(rdNFacture.Checked) txtNFacture.Focus();
				else if(rdNConsultation.Checked) txtNConsultation.Focus();
			}
			else
			{
				txtNConsultation.Enabled = false;
				txtNFacture.Enabled = false;

                if(rdNConsultation.Checked && txtNConsultation.Text!=""  ) 
				{
                    // Recherche d'une facture par le numéro de consultation
					_frmgeneral.AfficheAppelsByConsult(int.Parse(txtNConsultation.Text));

					if(Donnees.MonDataSetAppels==null || Donnees.MonDataSetAppels.Tables[0].Rows.Count==0)
					{
						MessageBox.Show("Aucune consultation pour ce numéro");
						return;
					}
					else
					{
						m_datarowAppel = Donnees.MonDataSetAppels.Tables[0].Rows[0];
					}

					long NFacture =  OutilsExt.OutilsSql.NFactureByConsult(long.Parse(txtNConsultation.Text));
					if(NFacture>-1)
					{				
						btnNew.Visible = false;
                        // btnModif.Visible = true;
						// btnSave.Visible = false;
						btnHisto.Visible = true;
					   	btnDelete.Visible = true;                                        

                        DataRow[] z_drw = OutilsExt.OutilsSql.RecuperationFacturesByNFacture(NFacture);
                        if (z_drw != null && z_drw.Length > 0)
                        {
                            _drwFacture = z_drw[0];
                        }
                        else
                        {
                            _drwFacture = null;
                        }
                        MiseEnPlaceConsultation(m_datarowAppel);
                        MiseEnPlaceFacture(_drwFacture);
						BlocageControles(false);
					}
					else
					{
						DialogResult result = MessageBox.Show("La facture n'est pas encore éditée pour cette consultation. Le faire maintenant?","Facturation",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button1);
						if(result==DialogResult.Yes)
						{
							 btnNew.Visible = true;
							// btnSave.Visible = true;
                            // btnModif.Visible = false;                         
						 	btnHisto.Visible = false;
							btnDelete.Visible = false;
							BlocageControles(true);
							btnNew_Click(null,null);
						}
					}
				}
				else if(rdNFacture.Checked && txtNFacture.Text!="")
				{
					long NConsult = OutilsExt.OutilsSql.RecuperationConsultationPrincipaleByNFacture(long.Parse(txtNFacture.Text));
					if(NConsult==-1)
					{
						MessageBox.Show("Cette facture n'existe pas");
						return;
					}
					else
					{
						// Recherche d'une facture par le numéro de consultation
						_frmgeneral.AfficheAppelsByConsult((int)NConsult);			
						m_datarowAppel = Donnees.MonDataSetAppels.Tables[0].Rows[0];						
						long NFacture =  long.Parse(txtNFacture.Text);
						 btnNew.Visible = false;
                       //  btnModif.Visible = true;
						// btnSave.Visible = false;                      
						 btnHisto.Visible = true;
						 btnDelete.Visible = true;
                        DataRow[] z_drw = OutilsExt.OutilsSql.RecuperationFacturesByNFacture(NFacture);
                        if (z_drw != null && z_drw.Length > 0)
                        {
                            _drwFacture = z_drw[0];
                        }
                        else
                        {
                            _drwFacture = null;
                        }
                        MiseEnPlaceConsultation(m_datarowAppel);
                        MiseEnPlaceFacture(_drwFacture);
						BlocageControles(false);		
                        //BlocageControles(true);
					}
				}
			}
		}


		#endregion

		#region En cours de travail...

		private void btnImpr_Click(object sender, System.EventArgs e)
		{
            if (int.Parse(_drwFacture["TypeSortie"].ToString()) == (int)Facturation.Sortie.Imprimante)
			{
                //frmImpressionFacture imprFacture = new frmImpressionFacture(_drwFacture, 0);
                FImpFacture fImpFacture = new FImpFacture(Int64.Parse(_drwFacture["NFacture"].ToString()), "Imprimante", "Normale","","");
                fImpFacture.ShowDialog();
                fImpFacture.Dispose();
                fImpFacture = null;
			}
		}


        private void btDupPolice_Click(object sender, System.EventArgs e)
        {
            //Impression des factures destinées à la police et aux assurances
            FImpFacture fImpFacture = new FImpFacture(Int64.Parse(_drwFacture["NFacture"].ToString()), "Imprimante", "Police Assurances","","");
            fImpFacture.ShowDialog();
            fImpFacture.Dispose();
            fImpFacture = null;
        }

        private void b10Poucent_Click(object sender, EventArgs e)
        {
            //update database of printing bill 10%
            OutilsExt.OutilsSql.ExecuteCommandeSansRetour("insert into facture_etats (NFacture,Etat,DateEtat,DateOp,CommentaireEtat,Param1,Param2,CodeUtilisateur,Montant,DatePaye) values (" + _drwFacture["NFacture"] + ",9,'" + OutilsExt.OutilsSql.DateFormatMySql(DateTime.Now) + "','" + OutilsExt.OutilsSql.DateFormatMySql(DateTime.Now) + "','Impression solde Facture','','','" + VariablesApplicatives.Utilisateurs.Identifiant + "','','')");
            OutilsExt.OutilsSql.ExecuteCommandeSansRetour("update facture_status set FacDateImpression10 = '" + OutilsExt.OutilsSql.DateFormatMySql(DateTime.Now) + "' WHERE NFacture = '" + _drwFacture["NFacture"] + "'");

            //Impression de la facture            
            //frmImpressionFacture imprFacture = new frmImpressionFacture(_drwFacture, 3);
            FImpFacture fImpFacture = new FImpFacture(Int64.Parse(_drwFacture["NFacture"].ToString()), "Imprimante", "10%","","");
            fImpFacture.ShowDialog();
            fImpFacture.Dispose();
            fImpFacture = null;
        }

        //Impression/refection des factures police pour les patients et les assurances
        //On refait une facture Forfaitaire avec les tarifs tarmed (elle sera stockée nulle part)
        private void bDuplicata_Click(object sender, EventArgs e)
        {
            if (int.Parse(_drwFacture["TypeSortie"].ToString()) == (int)Facturation.Sortie.Imprimante)
            {
                //On affiche la forme pour demander l'adresse et le tarif fédéral/cantonal
                FPoliceVersTarmed fPoliceVersTarmed = new FPoliceVersTarmed(long.Parse(m_datarowAppel["IndicePatient"].ToString()));
                fPoliceVersTarmed.ShowDialog();                                
                fPoliceVersTarmed.Dispose();

                Console.WriteLine(AdrPourPoliceTarmed);
                Console.WriteLine(TarifPourPoliceTarmed);
                
                //frmImpressionFacture imprFacture = new frmImpressionFacture(_drwFacture, 0);
                FImpFacture fImpFacture = new FImpFacture(Int64.Parse(_drwFacture["NFacture"].ToString()), "Imprimante", "PoliceVersTarmed", TarifPourPoliceTarmed, AdrPourPoliceTarmed);
                fImpFacture.ShowDialog();
                fImpFacture.Dispose();
                fImpFacture = null;

                //On réinitialise les variables
                AdrPourPoliceTarmed = "";
                TarifPourPoliceTarmed = "";
            }
        }


	 	private void btnHisto_Click(object sender, System.EventArgs e)
	 	{
            if (_drwFacture != null)
			{
                //frmFacHisto histo = new frmFacHisto(this._frmgeneral, _drwFacture);
                //histo.ShowDialog();

                frmDetails DetailPaiements = new frmDetails(frmDetails.Mode.Facture, long.Parse(_drwFacture["Nfacture"].ToString()));
                DetailPaiements.ShowDialog();

                //On recharge la facture
                BtnFindFacture_Click(sender, e);
            }
		}
        //btnModif_Click
        private void btnModif_Click(object sender, EventArgs e)
        {
            if (_drwFacture != null)
            {
                SosMedecins.SmartRapport.Facturation.frmFacAnnuler frmfacAnnuler = new SosMedecins.SmartRapport.Facturation.frmFacAnnuler(_drwFacture);
                frmfacAnnuler.ShowDialog();
                //on audite
                if (VariablesApplicatives.Utilisateurs.Identifiant.ToString() == "A001")
                    mouchard.evenement("Modification de la facture...n° " + txtNFacture.Text, VariablesApplicatives.Utilisateurs.NomUtilisateur.ToString());  //log)
            }
        }

		#endregion								

		#region Déplacer des lignes du spread

		private void fpFactures_MouseDown_1(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			FarPoint.Win.Spread.Model.CellRange range = fpFactures.GetCellFromPixel(0,0,e.X,e.Y);
			if(range.Row>-1)
			{
				if(e.Button == MouseButtons.Left)
				{
					fpFactures.DoDragDrop(range.Row.ToString(),DragDropEffects.Move);
				}
			}
		}

		private void fpFactures_DragEnter_1(object sender, System.Windows.Forms.DragEventArgs e)
		{
			if(e.Data.GetDataPresent(typeof(string))) e.Effect=DragDropEffects.Move;
		}

		private void fpFactures_DragDrop_1(object sender, System.Windows.Forms.DragEventArgs e)
		{
			
			Point p = fpFactures.PointToClient(new Point(e.X, e.Y));
			FarPoint.Win.Spread.Model.CellRange range = fpFactures.GetCellFromPixel(0,0,p.X, p.Y);
			
			if(range.Row>-1 && e.Data.GetDataPresent(typeof(string)))
			{
				int OriginalRow = int.Parse(e.Data.GetData(typeof(string)).ToString());
				fpFactures_Sheet1.Rows.Add(range.Row,1);
				fpFactures_Sheet1.MoveRange(OriginalRow+1,0,range.Row,0,1,fpFactures_Sheet1.ColumnCount,false);
				fpFactures_Sheet1.RemoveRows(OriginalRow+1,1);
			}			 				
		}

		#endregion			

		

        #region Arrangement

        private DataTable _dtbArrangement;

        private void ChargeArrangement(DataRow p_drwFacture)
        {
            Boolean z_blnConnect = SosMedecins.SmartRapport.DAL.Variables.ConnexionBase.OpenBDD();
            try
            {
                _dtbArrangement = SosMedecins.SmartRapport.DAL.Variables.ConnexionBase.ExecuteSql(null, SosMedecins.SmartRapport.DAL.RequetesSelect.facture_arrangement.Nfacture.Replace("%NFacture%", p_drwFacture["NFacture"].ToString()));
            }
            finally
            {
                if (z_blnConnect)
                {
                    SosMedecins.SmartRapport.DAL.Variables.ConnexionBase.CloseBDD();
                }
            }
            AfficheArrangement();
        }

        private void AfficheArrangement()
        {
            btnArrangement.Enabled = true;
            if (_dtbArrangement.Rows.Count > 0)
            {
                btnArrangement.Text = "Modifier";

                txtArrangementDate.Text = ((DateTime)_dtbArrangement.Rows[0]["DateCreation"]).ToString("dd.MM.yyyy");
                txtArrangementUser.Tag = _dtbArrangement.Rows[0]["CodeUtilisateur"].ToString();
                txtArrangementUser.Text = _dtbArrangement.Rows[0]["Nom"].ToString();
                txtArrangement.Text = _dtbArrangement.Rows[0]["Commentaire"].ToString();
            }
            else
            {
                btnArrangement.Text = "Ajouter";
                
                txtArrangementDate.Text = "";
                txtArrangementUser.Tag = "";
                txtArrangementUser.Text = "";
                txtArrangement.Text = "";
            }
        }

        private void btnArrangement_Click(object sender, EventArgs e)
        {
            SosMedecins.SmartRapport.Facturation.frmArrangement z_frmArrangement = new SosMedecins.SmartRapport.Facturation.frmArrangement(_dtbArrangement, _drwFacture["NFacture"].ToString());
            //
            z_frmArrangement.ShowDialog();
            z_frmArrangement.Dispose();
            z_frmArrangement = null;
            //
            AfficheArrangement();
        }
        #endregion

        #region Sauvegarde
        //public bool SauvegardeFacture(DataRow Factures, string[][] lignes)
        public bool SauvegardeFacture(string[][] lignes)
        {
            bool MajAvs = false;
            int Nb_Session = 1;   //Pour le nombre de session
            
            // Verification
            if (_drwFacture == null)
            {
                return false;
            }
            if (_drwFacture["TTT"].ToString() == "2" && _drwFacture["DateAccident"].ToString() == "")
            {
                MessageBox.Show("Entrez Date Accident");
                return false;
            }

            //****S'il existe des factures pour ce patient après celle ci (celle ci est donc en MAJ), on ne modifie pas les n° AVS et Assuré ****Domi 03.10.2013              
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);        //Chaine de connection récupérée dans le app.config

            dbConnection.Open();

            SqlCommand cmd = dbConnection.CreateCommand();
            cmd.Connection = dbConnection;       //On passe les parametres query et connection

            try
            {                
                string Query0 = "Select f.NFacture From facture f, factureconsultation fc, tableconsultations tc";
                Query0 += " WHERE f.NFacture = fc.NFacture";
                Query0 += " and fc.NConsultation = tc.NConsultation";                
                Query0 += " and tc.IndicePatient = (Select pa.IdPatient From tablePatient pa, tableconsultations tc";
                Query0 += "                         where tc.IndicePatient = pa.IdPatient"; 
                Query0 += "                         and tc.NConsultation = " + _drwFacture["NConsultation"].ToString() + ")";
                Query0 += " and DateCreation > '" + _drwFacture["DateCreation"].ToString() + "'";

                cmd.CommandText = Query0;
                 DataSet DSResult = new DataSet();       //on déclare le DataSet pour recevoir les diverses données

                DSResult.Tables.Add("Resultat");         //on déclare une table pour cet ensemble de donnée
                //on execute
                DSResult.Tables["Resultat"].Load(cmd.ExecuteReader());

                //si on a pas de facture après, on met à jour (NVLLE Facture)
                if (DSResult.Tables["Resultat"].Rows.Count == 0)
                {
                    MajAvs = true;                                     
                }

                //****************On compte le nombre de visite de ce patient dans la journée pour le nb de seances*************   
                Query0 = "SELECT count(*) ";
                Query0 += " FROM tableactes a inner join (SELECT ta.IndicePatient, ta.DAP, ta.Num";
                Query0 += "                               FROM tableactes ta, tableconsultations tc";
                Query0 += "                               WHERE ta.Num = tc.CodeAppel";
                Query0 += "                               AND tc.NConsultation = " + _drwFacture["NConsultation"].ToString() + ") as Dt ON a.IndicePatient = Dt.IndicePatient";
                Query0 += "                   inner join tableconsultations c ON c.CodeAppel = a.Num";
                Query0 += "                   inner join factureconsultation fc ON fc.NConsultation = c.NConsultation";
                Query0 += " WHERE CONVERT(Date,a.DAP) = CONVERT(Date, Dt.DAP)";
                Query0 += " AND Dt.Num <> a.Num";

                cmd.CommandText = Query0;
                DataTable dtResult = new DataTable();

                dtResult.Load(cmd.ExecuteReader());

                //si on a déjà des visites pour la journée pour ce patient, on incrémente les session
                if ((int)dtResult.Rows[0][0] > 0)
                {
                    Nb_Session = (int)dtResult.Rows[0][0] + 1;
                }
                else Nb_Session = 1;

            }
            catch (Exception a)
            {
                Console.WriteLine("Erreur : " + a.Message);
            }
            finally
            {
                // Fermeture de la connexion
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
        
            //***********************                                   
            Boolean z_blnRetour = false;
            string z_strSql;
            // Mise a jour de la facture
            Boolean z_blnConnect = Variables.ConnexionBase.OpenBDD();
            try
            {
                Variables.ConnexionBase.BeginTransaction();

                string strDateAcc = "";
                strDateAcc = FormatSql.Format_Date(_drwFacture["DateAccident"].ToString());
                // Facture ----------------------------------------------------------------------------------------------------------------
                z_strSql = "update facture set TypeEnvoi=" + _drwFacture["TypeEnvoi"].ToString() + ",Tarif=" + _drwFacture["Tarif"].ToString() + ",TTT=" + _drwFacture["TTT"].ToString() + ",TypeAssurance=" + _drwFacture["TypeAssurance"].ToString();
                z_strSql += ",TypeSortie=" + _drwFacture["TypeSortie"].ToString() + ",NAccident='" + _drwFacture["NAccident"].ToString().Replace("'", "''") + "',DateAccident=" + strDateAcc + ",RefPatient='" + _drwFacture["RefPatient"].ToString().Replace("'", "''") + "',FlagConcerne=" + _drwFacture["FlagConcerne"].ToString();
                z_strSql += ",Commentaire='" + _drwFacture["Commentaire"].ToString().Replace("'", "''") + "',TotalFacture= '" + _drwFacture["TotalFacture"].ToString().Replace(",", ".") + "',Solde= '" + _drwFacture["Solde"].ToString().Replace(",", ".") + "',TypeDocJoint=" + _drwFacture["TypeDocJoint"].ToString();
                z_strSql += ",TypeDestinataire=" + _drwFacture["TypeDestinataire"].ToString() + ",CodeDestinataire=" + _drwFacture["CodeDestinataire"].ToString() + ",AdresseDestinataire='" + _drwFacture["AdresseDestinataire"].ToString().Replace("'", "''");
                z_strSql += "',AdresseDestinataire2='" + _drwFacture["AdresseDestinataire2"].ToString().Replace("'", "''") + "',FactNum_AVS='" + _drwFacture["FactNum_AVS"].ToString().Replace("'", "''") + "'";
                z_strSql += ",Num_Session=" + Nb_Session;
                z_strSql += " WHERE NFacture = " + _drwFacture["NFacture"].ToString();

                Variables.ConnexionBase.ExecuteSqlSansRetour(z_strSql);
                
                //On met à jour le solde de la facture s'il y a une date de facture encaissée
                if (_drwFacture["FacDateEncaissee"].ToString() != DBNull.Value.ToString())
                {
                    z_strSql = RequetesUpdate.facture.Solde;
                    z_strSql = z_strSql.Replace("%NFacture", _drwFacture["NFacture"].ToString());

                    Variables.ConnexionBase.ExecuteSqlSansRetour(z_strSql);
                }
                // Facture_Statut ---------------------------------------------------------------------------------------------------------
                z_strSql = RequetesUpdate.facture_status.Encaissee;               
                z_strSql = z_strSql.Replace("%FacDateAcquittee%", FormatSql.Format_Date(_drwFacture["FacDateAcquittee"].ToString()));
                z_strSql = z_strSql.Replace("%FacDateEncaissee%", FormatSql.Format_Date(_drwFacture["FacDateEncaissee"].ToString()));
                z_strSql = z_strSql.Replace("%FacDate1Rappel%", FormatSql.Format_Date(_drwFacture["FacDate1Rappel"].ToString()));
                z_strSql = z_strSql.Replace("%FacDateContentieux%", FormatSql.Format_Date(_drwFacture["FacDateContentieux"].ToString()));

                z_strSql = z_strSql.Replace("%FacDateCession%", FormatSql.Format_Date(_drwFacture["FacDateCession"].ToString()));
                
                
                z_strSql = z_strSql.Replace("%NFacture%", FormatSql.Format_Nombre(_drwFacture["NFacture"].ToString()));

                //Domi 01/04/2011        
                //Date d'envoi de cession s'il y a lieu
                 z_strSql = z_strSql.Replace("%CessionEnvoi%", FormatSql.Format_Date(_drwFacture["CessionEnvoi"].ToString()));   //affectation des champs du dataset aux variables de la requete
               
                //Date de reception de la cession
                z_strSql = z_strSql.Replace("%CessionRecu%", FormatSql.Format_Date(_drwFacture["CessionRecu"].ToString()));   
               
                z_strSql = z_strSql.Replace("%RenvFact10p%", _drwFacture["RenvFact10p"].ToString());

                z_strSql = z_strSql.Replace("%FactFranchise%", _drwFacture["FactFranchise"   ].ToString());

                z_strSql = z_strSql.Replace("%PatientIndelicat%", _drwFacture["PatientIndelicat"].ToString());

                //z_strSql = z_strSql.Replace("%PoursuiteDate%", FormatSql.Format_Date(_drwFacture["PoursuiteDate"].ToString()));

                z_strSql = z_strSql.Replace("%LimiteStopRappel%", FormatSql.Format_Date(_drwFacture["LimiteStopRappel"].ToString()));

                //************************************************************

                Variables.ConnexionBase.ExecuteSqlSansRetour(z_strSql);
                // patient_Remarque -------------------------------------------------------------------------------------------------------
                // Verification des coches faire en caissé sur place et faire signer cession de créance
                if (_drwFacture["FacDateEncaissee"].ToString() != System.DBNull.Value.ToString())
                {
                    SosMedecins.SmartRapport.DAL.Fonction z_objFonction = new SosMedecins.SmartRapport.DAL.Fonction();
                    z_objFonction.EncaissementSurPlace(long.Parse(_drwFacture["NFacture"].ToString()));
                }
                // Facture Prestation -----------------------------------------------------------------------------------------------------
                if (lignes != null)
                {
                    Variables.ConnexionBase.ExecuteSqlSansRetour("delete from facture_prest where NFacture = " + _drwFacture["NFacture"].ToString());
                    for (int i = 0; i < lignes.Length; i++)
                    {
                        Variables.ConnexionBase.ExecuteSqlSansRetour("insert into facture_prest (NFacture,TypePrest,Indice,Qte,Points,Prix,Cote,TypeTarif,Ordre) values (" + _drwFacture["NFacture"].ToString() + "," + lignes[i][0] + ",'" + lignes[i][1] + "'," + lignes[i][2] + ",'" + lignes[i][6].Replace(",", ".") + "','" + lignes[i][3].Replace(",", ".") + "','" + lignes[i][4].Replace("'", "''") + "'," + lignes[i][5] + "," + i + ")");
                    }
                    if (lignes.Length > 0)
                       {
                        if (lignes[0][1] == "030")
                        {
                            Variables.ConnexionBase.ExecuteSqlSansRetour("delete from fac_pres_police where NFacture = " + _drwFacture["NFacture"].ToString());
                            Variables.ConnexionBase.ExecuteSqlSansRetour("insert into fac_pres_police (NFacture,TypePrest,Indice,Qte,Points,Prix,Cote,TypeTarif,Ordre) values (" + _drwFacture["NFacture"].ToString() + ",1,'00.0060',1, 9.57, 17.4, '0', 0, 6)");
                            Variables.ConnexionBase.ExecuteSqlSansRetour("insert into fac_pres_police (NFacture,TypePrest,Indice,Qte,Points,Prix,Cote,TypeTarif,Ordre) values (" + _drwFacture["NFacture"].ToString() + ",1,'00.0070',4, 9.57, 69.6, '0', 1, 6)");
                            Variables.ConnexionBase.ExecuteSqlSansRetour("insert into fac_pres_police (NFacture,TypePrest,Indice,Qte,Points,Prix,Cote,TypeTarif,Ordre) values (" + _drwFacture["NFacture"].ToString() + ",1,'00.0080',1, 4.78, 8.7, '0', 2, 6)");
                            Variables.ConnexionBase.ExecuteSqlSansRetour("insert into fac_pres_police (NFacture,TypePrest,Indice,Qte,Points,Prix,Cote,TypeTarif,Ordre) values (" + _drwFacture["NFacture"].ToString() + ",1,'00.0090',4, 9.57, 69.6, '0', 3, 6)");
                        }
                        else if (lignes[0][1] == "040")
                        {
                            Variables.ConnexionBase.ExecuteSqlSansRetour("delete from fac_pres_police where NFacture = " + _drwFacture["NFacture"].ToString());
                            Variables.ConnexionBase.ExecuteSqlSansRetour("insert into fac_pres_police (NFacture,TypePrest,Indice,Qte,Points,Prix,Cote,TypeTarif,Ordre) values (" + _drwFacture["NFacture"].ToString() + ",1,'00.0060',1, 9.57, 17.4, '0', 0, 6)");
                            Variables.ConnexionBase.ExecuteSqlSansRetour("insert into fac_pres_police (NFacture,TypePrest,Indice,Qte,Points,Prix,Cote,TypeTarif,Ordre) values (" + _drwFacture["NFacture"].ToString() + ",1,'00.0070',6, 9.57, 104.45, '0', 1, 6)");
                            Variables.ConnexionBase.ExecuteSqlSansRetour("insert into fac_pres_police (NFacture,TypePrest,Indice,Qte,Points,Prix,Cote,TypeTarif,Ordre) values (" + _drwFacture["NFacture"].ToString() + ",1,'00.0080',1, 4.78, 8.7, '0', 2, 6)");
                            Variables.ConnexionBase.ExecuteSqlSansRetour("insert into fac_pres_police (NFacture,TypePrest,Indice,Qte,Points,Prix,Cote,TypeTarif,Ordre) values (" + _drwFacture["NFacture"].ToString() + ",1,'00.0090',3, 9.57, 52.2, '0', 3, 6)");
                        }
                    }
                }
                
                //Mise à jour Tablepersonne ******Domi 05.04.2013 maj le 03.10.2013
                if (MajAvs == true)
                {
                    //Preparation de le requette
                    string Query1 = "update tablepersonne set Num_Assure = '" + _drwFacture["Num_Assure"].ToString();
                    Query1 += "', Num_AVS = '" + _drwFacture["Num_AVS"].ToString();
                    Query1 += "' WHERE IdPersonne = (Select pe.IdPersonne From tablepersonne pe, tablePatient pa, tableconsultations tc";
                    Query1 += "                     where pe.IdPersonne = pa.IdPersonne and tc.IndicePatient = pa.IdPatient and tc.NConsultation = " + _drwFacture["NConsultation"].ToString() + ")";

                    Variables.ConnexionBase.ExecuteSqlSansRetour(Query1);
                }                             
                //***************

           
                // Consultation
                Variables.ConnexionBase.ExecuteSqlSansRetour("update tableconsultations set FactureGeneree=1 WHERE NConsultation = " + _drwFacture["NConsultation"].ToString());
                // Sauvegarde Patient_Remarque
                if (_dtbPatient_Remarque.Rows.Count > 0)
                {
                    if (Convert.ToInt32(_dtbPatient_Remarque.Rows[0]["Encaisse"]) != Convert.ToInt32(chkSurPlace.Checked))
                    {
                        if (chkSurPlace.Checked)
                        {
                            _dtbPatient_Remarque.Rows[0]["Encaisse"] = 1;
                        }
                        else
                        {
                            _dtbPatient_Remarque.Rows[0]["Encaisse"] = 0;
                        }
                    }
                    if (Convert.ToInt32(_dtbPatient_Remarque.Rows[0]["Cession"]) != Convert.ToInt32(chkCessionCreance.Checked))
                    {
                        if (chkCessionCreance.Checked)
                        {
                            _dtbPatient_Remarque.Rows[0]["Cession"] = 1;
                        }
                        else
                        {
                            _dtbPatient_Remarque.Rows[0]["Cession"] = 0;
                        }
                    }
                    // update dans la table si modif
                    if (_dtbPatient_Remarque.GetChanges() != null)
                    {
                        SauvegardePatient_Remarque(SosMedecins.SmartRapport.DAL.RequetesUpdate.Patient_Remarque.Complet);
                    }
                }
                else
                {
                    if (chkSurPlace.Checked || chkCessionCreance.Checked)
                    {
                        // insert dans la table
                        SauvegardePatient_Remarque(SosMedecins.SmartRapport.DAL.RequetesInsert.Patient_Remarque.Complet);
                    }
                }
                SosMedecins.SmartRapport.DAL.Variables.ConnexionBase.Commit();

                z_blnRetour = true;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                SosMedecins.SmartRapport.DAL.Variables.ConnexionBase.RollBack();
                throw new Exception(ex.Message);
                //MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                if (z_blnConnect)
                {
                    SosMedecins.SmartRapport.DAL.Variables.ConnexionBase.CloseBDD();
                }
            }
            return z_blnRetour;
        }

        private void SauvegardePatient_Remarque(string p_strSql)
        {
            p_strSql = p_strSql.Replace("%IdPatient%", FormatSql.Format_Nombre(m_datarowAppel["IndicePatient"].ToString()));
            p_strSql = p_strSql.Replace("%Encaisse%", FormatSql.Format_Nombre(Convert.ToInt32(chkSurPlace.Checked).ToString()));
            p_strSql = p_strSql.Replace("%Export%", FormatSql.Format_Nombre("0"));
            p_strSql = p_strSql.Replace("%Cession%", FormatSql.Format_Nombre(Convert.ToInt32(chkCessionCreance.Checked).ToString()));
            p_strSql = p_strSql.Replace("%DateValidite%", FormatSql.Format_Date(DateTime.Now.ToString("dd.MM.yyyy")));
            p_strSql = p_strSql.Replace("%IdUtilisateur%", FormatSql.Format_String(SosMedecins.SmartRapport.GestionApplication.VariablesApplicatives.Utilisateurs.Identifiant));

            Boolean z_blnConnect = SosMedecins.SmartRapport.DAL.Variables.ConnexionBase.OpenBDD();
            try
            {
                SosMedecins.SmartRapport.DAL.Variables.ConnexionBase.ExecuteSqlSansRetour(p_strSql);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                throw new Exception(ex.Message);
                //MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                if ( z_blnConnect ) 
                {
                    SosMedecins.SmartRapport.DAL.Variables.ConnexionBase.CloseBDD();
                }
            }
        }        
        #endregion

        #region Action
        private void chkCession_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCession.Checked)
            {
                dbxCession.Text = DateTime.Now.ToString("dd.MM.yyyy");
                dbxCession.ReadOnly = false;
            }
            else
            {
                dbxCession.Text = "";
                dbxCession.ReadOnly = true;
            }
        }
        #endregion

        private void chkAnnule_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtRef_TextChanged(object sender, EventArgs e)
        {

        }


        //*******************Domi 05.04.2013 ************Zoom sur l'image de la carte AVS
        //gestion de la roulette de la souris
        private void zoomImageViewer1_MouseWheel(object sender, MouseEventArgs e)   
        {
            if (e.Delta < 0)  //sens de la roulette
            {
                zoomImageViewer1.Zoom += (float).1; //On augmente le zoom
            }
            else
            {
                if (zoomImageViewer1.Zoom > 0.2)     //On diminue le Zoom jusqu'à 0.2 max (après il n'y a plus d'image)     
                    zoomImageViewer1.Zoom -= (float).1;
            }
        }

        //Penser à donner le focus à la fenètre
        private void zoomImageViewer1_MouseEnter(object sender, EventArgs e)
        {
            if (zoomImageViewer1.Focused == false)
            {
                zoomImageViewer1.Focus();
            }
        }

        //Tourner l'image si elle est à l'envert
        private void bRotationImage_Click(object sender, EventArgs e)
        {
            Image img = zoomImageViewer1.Image;
            img.RotateFlip(RotateFlipType.Rotate90FlipNone);
            zoomImageViewer1.Image = img;
        }

        //Ajout d'un document à la facture
        private void bAjout_Click(object sender, EventArgs e)
        {
            FAjoutDocumentsFacture docJ = new FAjoutDocumentsFacture(txtNFacture.Text);
            docJ.ShowDialog();

            docJ.Dispose();
            docJ = null;
        }

        private void cbTTT_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chargeListe == true)
            {
                if (cbTTT.SelectedIndex > -1)
                {
                    //On recharge la liste des prestations...Ainsi que le Coeff médecin
                    if ((cbTarif.Text == "Tarmed fédéral" && (cbTTT.Text == "Maladie" || cbTTT.Text == "Accident")) || cbTarif.Text == "Police" || cbTarif.Text == "Usage")
                    {
                        ChargementComboPrestation("LAA-AM-AI"); //LAA-AM-LAI
                        TarmedVersion = "LAA-AM-AI";
                        scale_factor_mt = 1;
                    }
                    else
                    {
                        ChargementComboPrestation("LAMAL");    //LAMAL
                        TarmedVersion = "LAMAL";
                        scale_factor_mt = OutilsExt.OutilsSql.Val_Scale_factor_mt(long.Parse(txtNConsultation.Text));
                    }
                }
            }
        }

 
        //Fct° qui vérifie si une position n'a pas dépassée le nombre de prescriptions
        private int VerifNbPrest(string Position, int Quantite, string IndicePatient)
        {
            int NbRetour = 0;
            
            if (txtNConsultation.Text != "" && Position != "" && Quantite > 0)
            {
                int NbPsceance = 0;
                int NbP3Mois = 0;
                                
                //On charge les positions qui nous interresses
                switch (Position)
                {
                    case "00.0070": NbPsceance = 6; NbP3Mois = 0; break;
                    case "00.0075": NbPsceance = 6; NbP3Mois = 0; break;
                    case "00.0076": NbPsceance = 6; NbP3Mois = 0; break;
                    case "00.0120": NbPsceance = 4; NbP3Mois = 0; break;
                    case "00.0125": NbPsceance = 4; NbP3Mois = 0; break;
                    case "00.0126": NbPsceance = 4; NbP3Mois = 0; break;  
                    case "00.0131": NbPsceance = 0; NbP3Mois = 60; break;
                    case "00.0141": NbPsceance = 0; NbP3Mois = 30; break;
                    case "00.0161": NbPsceance = 0; NbP3Mois = 60; break;                    
                    case "00.0415": NbPsceance = 3; NbP3Mois = 6; break;
                    case "00.0416": NbPsceance = 6; NbP3Mois = 12; break;
                    case "00.0417": NbPsceance = 6; NbP3Mois = 12; break;
                    case "00.0610": NbPsceance = 3; NbP3Mois = 6; break;
                    case "00.0615": NbPsceance = 6; NbP3Mois = 12; break;
                    case "00.0616": NbPsceance = 6; NbP3Mois = 12; break;
                    case "00.0520": NbPsceance = 12; NbP3Mois = 0; break;

                    default: NbPsceance = 0; NbP3Mois = 0; break;
                }
                  
                //On recherche le nb de fois que la position a été utilisée selon les critères (NbPseance, NbP3Mois)
                if (NbPsceance == 0 && NbP3Mois == 0)
                {
                    NbRetour = 0;   //aucun traitement à faire c'est ok
                    return NbRetour;
                }
                else
                {
                    if (NbPsceance != 0 && Quantite > NbPsceance)

                    //if (Quantite > NbPsceance)
                    {
                        //On retourne un négatif (NbPsceance - Quantité) qui signifiera "nb par sceance dépassée".
                        NbRetour = NbPsceance - Quantite;
                        return NbRetour;
                    }

                    //On commence par regarder le nombre max de prestation autorisé pour la prestation
                    if (NbP3Mois != 0)
                    {
                        //on est passé donc on vérifie pour 3 mois si besoins
                        string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
                        SqlConnection dbConnection = new SqlConnection(connex);        //Chaine de connection récupérée dans le app.config

                        try
                        {
                            dbConnection.Open();

                            SqlCommand cmd = dbConnection.CreateCommand();
                            cmd.Connection = dbConnection;       //On passe les parametres query et connection

                            string Query0 = "SELECT COUNT(*), SUM(fp.Qte) From facture_prest fp, facture f, factureconsultation fc, tableconsultations tc, tableactes ta";
                            Query0 += " WHERE fp.NFacture = f.NFacture";
                            Query0 += " AND f.NFacture = fc.NFacture";
                            Query0 += " AND fc.NConsultation = tc.NConsultation";
                            Query0 += " AND tc.CodeAppel = ta.Num";
                            Query0 += " AND ta.IndicePatient = @indicePatient";
                            Query0 += " AND fp.Indice = @Position";
                            Query0 += " AND ta.DAP >= DateAdd(Month, -3, getdate())";

                            cmd.CommandText = Query0;

                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("indicePatient", IndicePatient);
                            cmd.Parameters.AddWithValue("Position", Position);

                            DataTable DtResult = new DataTable();

                            //on execute
                            DtResult.Load(cmd.ExecuteReader());

                            //Si on a qqchose...
                            if (DtResult.Rows.Count > 0)
                            {
                                //Et si le nombre renvoyé de prestations est <= au nombre mis dans cette facture + nb max par trimestre  
                                if(int.Parse(DtResult.Rows[0][0].ToString()) > 0)
                                {
                                    if (int.Parse(DtResult.Rows[0][1].ToString()) + Quantite <= (NbPsceance + NbP3Mois))
                                    {
                                        NbRetour = 0;        //on retourne 0, c'est ok.
                                    }
                                    else
                                    {
                                        //Pas bon....on renvoi le dépassement (en négatif)
                                        NbRetour = NbPsceance + NbP3Mois - (int.Parse(DtResult.Rows[0][1].ToString()) + Quantite);
                                    }
                                }
                                else NbRetour = 0;        //on retourne 0, c'est ok.                               
                            }
                        }
                        catch (Exception a)
                        {
                            Console.WriteLine("Erreur : " + a.Message);
                        }
                        finally
                        {
                            // Fermeture de la connexion
                            if (dbConnection.State == System.Data.ConnectionState.Open)
                                dbConnection.Close();
                        }
                    }
                    else NbRetour = 0;   //C'est Ok pas de nb limite pour 3 Mois
                }
                return NbRetour;
            }            
            return NbRetour;     //aucun traitement à faire
        }


        //Vérif de l'âge par rapport à positions. 
        private string VerifPositionAge(string Position, string Patient, DateTime DateConsult)
        {
            string Retour = "OK";

            if (Position != "" && Patient != "" && TarmedVersion == "LAMAL")
            {
                if (Position == "00.0070" || Position == "00.0075" || Position == "00.0076" || 
                    Position == "00.0141" || Position == "00.0131" || Position == "00.0120" ||
                    Position == "00.0125" || Position == "00.0126" ||
                    Position == "00.0415" || Position == "00.0416" || Position == "00.0417" ||
                    Position == "00.0610" || Position == "00.0615" ||
                    Position == "00.0136" || Position == "00.0146" )
                {                   
                    string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
                    SqlConnection dbConnection = new SqlConnection(connex);        //Chaine de connection récupérée dans le app.config

                    try
                    {
                        dbConnection.Open();

                        SqlCommand cmd = dbConnection.CreateCommand();
                        cmd.Connection = dbConnection;       //On passe les parametres query et connection

                        string Query0 = "SELECT DATEDIFF(MONTH, p.DateNaissance, @DateDSL) / 12";
                        Query0 += "        - CASE WHEN MONTH(p.DateNaissance) = MONTH(@DateDSL) AND  DAY(p.DateNaissance) > DAY(@DateDSL)";
                        Query0 += "               THEN 1";
                        Query0 += "               ELSE 0";
                        Query0 += "          END AS Age";
                        Query0 += " From tablepersonne p, tablepatient tp";
                        Query0 += " WHERE tp.IdPatient = @indicePatient";
                        Query0 += " AND tp.IdPersonne = p.IdPersonne";

                        cmd.CommandText = Query0;

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("indicePatient", Patient);
                        cmd.Parameters.AddWithValue("DateDSL", DateConsult); 

                        DataTable DtResult = new DataTable();

                        //on execute
                        DtResult.Load(cmd.ExecuteReader());

                        //Si on a qqchose...
                        if (DtResult.Rows.Count > 0)
                        {                        
                            //On charge les positions qui nous interresses
                            switch (Position)
                            {                                                      
                                case "00.0070":
                                case "00.0120":
                                case "00.0126":
                                case "00.0141":                 //Pour les +6 ans et -75 ans
                                case "00.0146":
                                case "00.0415":
                                case "00.0610":
                                case "00.0417": if (int.Parse(DtResult.Rows[0][0].ToString()) >= 6 && int.Parse(DtResult.Rows[0][0].ToString()) < 75)
                                                     Retour = "OK";
                                                else Retour = "KO"; break;
                                case "00.0125":
                                case "00.0136":
                                case "00.0075":                 //pour les -6 ans et + 75 ans 
                                case "00.0131":
                                case "00.0615":
                                case "00.0416": if (int.Parse(DtResult.Rows[0][0].ToString()) < 6 | int.Parse(DtResult.Rows[0][0].ToString()) >= 75)
                                                     Retour = "OK";
                                                else Retour = "KO"; break;
                                default: Retour = "OK"; break;
                            }                     
                        }
                    }
                    catch (Exception a)
                    {
                        Console.WriteLine("Erreur : " + a.Message);
                    }
                    finally
                    {
                        // Fermeture de la connexion
                        if (dbConnection.State == System.Data.ConnectionState.Open)
                            dbConnection.Close();
                    }
                }
            }

            return Retour;
        }

        //détermine si cette facture a déjà été envoyé (notament pour la mise à jour du solde lors de la sauvegarde)
        private bool DejaEnvoye(string NumFacture)
        {
            bool retour = false;

            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);        //Chaine de connection récupérée dans le app.config

            try
            {
                dbConnection.Open();

                SqlCommand cmd = dbConnection.CreateCommand();
                cmd.Connection = dbConnection;       //On passe les parametres query et connection

                string SqlStr0 = "SELECT CommentaireEtat FROM facture_etats";              
                SqlStr0 += " WHERE NFacture = @Facture";
                SqlStr0 += " AND (Etat = 5 OR Etat = 9)";

                cmd.CommandText = SqlStr0;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("Facture", NumFacture);
               
                DataTable DtResult = new DataTable();
                //on execute
                DtResult.Load(cmd.ExecuteReader());

                //Si on a au moins 1 enregistrement, on l'a déjà envoyée
                if (DtResult.Rows.Count > 0)                
                    retour = true;                                    
            }
            catch (Exception a)
            {
                Console.WriteLine("Erreur : " + a.Message);
            }
            finally
            {
                // Fermeture de la connexion
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }

            return retour;            
        }

        //Pour vérifier si la facture à déjà été créée 
        private bool FactureDejaCreee(string NFacture)
        {
            bool retour = false;

            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);        //Chaine de connection récupérée dans le app.config

            try
            {
                dbConnection.Open();

                SqlCommand cmd = dbConnection.CreateCommand();
                cmd.Connection = dbConnection;       //On passe les parametres query et connection

                string SqlStr0 = "SELECT Etat FROM facture_etats";
                SqlStr0 += " WHERE NFacture = @Facture";
                SqlStr0 += " AND Etat = 2";

                cmd.CommandText = SqlStr0;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("Facture", NFacture);

                DataTable DtResult = new DataTable();
                //on execute
                DtResult.Load(cmd.ExecuteReader());

                //Si on a au moins 1 enregistrement, on l'a déjà envoyée
                if (DtResult.Rows.Count > 0)
                    retour = true;
            }
            catch (Exception a)
            {
                Console.WriteLine("Erreur : " + a.Message);
            }
            finally
            {
                // Fermeture de la connexion
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }

            return retour;
        }


        //Retourne le nombre de factures Hors SOS pour le mois en cours
        private int nbFactureIndependant(int CodeMedecins)
        {
            int nbFact = -1;

            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);        //Chaine de connection récupérée dans le app.config

            try
            {
                dbConnection.Open();

                SqlCommand cmd = dbConnection.CreateCommand();
                cmd.Connection = dbConnection;       //On passe les parametres query et connectio               

                string SqlStr0 = "SELECT CASE";
                SqlStr0 += "               WHEN tm.Independant = 0 THEN -1";
                SqlStr0 += "               WHEN tm.DateMajCpt < DATEFROMPARTS(DatePart(YEAR, GETDATE()), DatePart(MONTH, GETDATE()), '01') THEN 0";
                SqlStr0 += "             ELSE tm.CptFactM";
                SqlStr0 += "             END";
                SqlStr0 += " FROM tablemedecin tm";
                SqlStr0 += " WHERE tm.CodeIntervenant = @CodeIntervenant";
                SqlStr0 += " AND tm.CodeIntervenant not in (2890)";     //On exclu de Senarclans

                cmd.CommandText = SqlStr0;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("CodeIntervenant", CodeMedecins);

                DataTable DtResult = new DataTable();
                //on execute
                DtResult.Load(cmd.ExecuteReader());

                //Si on a au moins 1 enregistrement
                if (DtResult.Rows.Count > 0)
                    nbFact = int.Parse(DtResult.Rows[0][0].ToString());
                else nbFact = -1;
            }
            catch (Exception a)
            {
                Console.WriteLine("Erreur : " + a.Message);
            }
            finally
            {
                // Fermeture de la connexion
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }

            return nbFact;
        }


        //  private void btnModif_Click(object sender, EventArgs e)
        /* {
             // Cas où c'est une nouvelle facture  :
             if (txtNConsultation.Text != "" && txtNFacture.Text != "" && !txtNConsultation.Enabled && !txtNFacture.Enabled && m_FactureActuelle != null && m_FactureActuelle.Length > 0)
             {
                 this.Cursor = Cursors.WaitCursor;
                 string[][] Lignes = SaveElementOfFrame();

                 // Procédure de sauvegarde :
                 bool reussite = OutilsExt.OutilsSql.SauvegardeFacture(m_FactureActuelle, Lignes);
                 OutilsExt.OutilsSql.EnregistreModification(long.Parse(m_FactureActuelle[0]["NConsultation"].ToString()), "", DateTime.Now, Constantes.MODIFICATION_FACTURE, "");
                 //this._frmgeneral.ChargementHistoriqueFiche(long.Parse(m_FactureActuelle[0]["NConsultation"].ToString()));
                 this.Cursor = Cursors.Default;

                 InitializeControls();
                 InitializeData();
                 PreparationPourRechercheConsult();


                 if (reussite)
                 {
                     lbStatusOp.Text = "Facture n°" + m_FactureActuelle[0]["NFacture"].ToString() + " sauvegardée avec succès";
                 }
                 else
                 {
                     MessageBox.Show("Erreur lors de la sauvegarde");
                 }
             }			
         }*/

    }
}


//A faire:
