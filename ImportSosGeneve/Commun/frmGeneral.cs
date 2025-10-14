using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using SosMedecins.SmartRapport.EtatsCrystal;
using SosMedecins.SmartRapport.GestionApplication;
using System.Threading;
using SosMedecins.SmartRapport.DAL;

using Vlc.DotNet.Core.Interops;
using Vlc.DotNet.Forms;
using Vlc.DotNet.Core;
using ImportSosGeneve.Rapport;
using ImportSosGeneve.TA;
using System.Net;
using ImportSosGeneve.Commun;
using System.Net.Sockets;
using ImportSosGeneve.Facture;
using MySql.Data.MySqlClient;
using System.Configuration;
using ImportSosGeneve.Statistics;

namespace ImportSosGeneve
{

	/// <summary>
	/// Description résumée de Form1.
	/// </summary>
	public class frmGeneral : Form
	{
        static Thread _oThread = null;
        /// <summary>
		/// Point d'entrée principal de l'application.
		
		/// </summary>
		[STAThread]
		static void Main()
        {         
            SosMedecins.Utilitaires.GestionErreur eh = new SosMedecins.Utilitaires.GestionErreur();

            // Adds the event handler to to the event.
            Application.ThreadException += new ThreadExceptionEventHandler(eh.OnThreadException);

            // Runs the application.

            VariablesApplicatives.Chargement();
            // Ouverture du paramétrage
            if (System.IO.File.Exists(Application.StartupPath + "\\" + "Config.xml"))
            {
                SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli = Parametrage.ChargeParametrage(Application.StartupPath + "\\" + "Config.xml");
            }
            else
            {
                SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli = new Parametrage();
            }

#if DEBUG
            Variables.ConnexionBase = new SosMedecins.Connexion.AccesSqlServer("Data Source=(localdb)\\Local;Initial Catalog=SmartRapport;Persist Security Info=true;User ID=sa;Password=root");
#else
            Variables.ConnexionBase = new SosMedecins.Connexion.AccesSqlServer("Data Source=" + Variables.InfoConnexion.NomServeur + ";Initial Catalog=" + Variables.InfoConnexion.BaseDonnees + ";User Id=" + Variables.InfoConnexion.Utilisateur + ";Password=" + Variables.InfoConnexion.MotDePasse);

#endif

            /*
#if DEBUG
                        if (MessageBox.Show("Base Test", " Choix base", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {

                            Variables.ConnexionBase = new SosMedecins.Connexion.AccesSqlServer("Data Source=" + Variables.InfoConnexion.NomServeur + ";Initial Catalog=" + Variables.InfoConnexion.BaseDonnees + ";User Id=" + Variables.InfoConnexion.Utilisateur + ";Password=" + Variables.InfoConnexion.MotDePasse);               
                        }
                        else
                            //Variables.ConnexionBase = new SosMedecins.Connexion.AccesSqlServer("Data Source=" + Variables.InfoConnexion.NomServeur + ";Initial Catalog=SmartRapport;User Id=sa;Password=Gimp38%31416");
                            Variables.ConnexionBase = new SosMedecins.Connexion.AccesSqlServer("Data Source=" + Variables.InfoConnexion.NomServeur + ";Initial Catalog=" + Variables.InfoConnexion.BaseDonnees + ";User Id=" + Variables.InfoConnexion.Utilisateur + ";Password=" + Variables.InfoConnexion.MotDePasse);

#else

                         Variables.ConnexionBase = new SosMedecins.Connexion.AccesSqlServer("Data Source=" + Variables.InfoConnexion.NomServeur + ";Initial Catalog=" + Variables.InfoConnexion.BaseDonnees + ";User Id=" + Variables.InfoConnexion.Utilisateur + ";Password=" + Variables.InfoConnexion.MotDePasse);
#endif
            */
            SosMedecins.Connexion.frmLogIn z_frmLogIn = new SosMedecins.Connexion.frmLogIn();

            z_frmLogIn.Selection = RequetesSelect.tableutilisateur.logon;
            z_frmLogIn.Connexion = Variables.ConnexionBase;

            if (z_frmLogIn.ShowDialog() == DialogResult.Yes)
            {
               /* _oThread = new Thread(new ThreadStart(DoSplash));
                _oThread.IsBackground = true;
                _oThread.Start();*/

                // recuperation de l'identifaint connecté
                VariablesApplicatives.Utilisateurs.Identifiant = z_frmLogIn.DonneesRetour["CodeUtilisateur"].ToString();
                VariablesApplicatives.Utilisateurs.NomUtilisateur = z_frmLogIn.DonneesRetour["Nom"].ToString();
                VariablesApplicatives.Utilisateurs.Droits = (VariablesApplicatives.Utilisateurs.CodeDroits)Convert.ToUInt32(z_frmLogIn.DonneesRetour["droits"]);
                VariablesApplicatives.Utilisateurs.Initiale = z_frmLogIn.DonneesRetour["Initiale"].ToString();
                VariablesApplicatives.Utilisateurs.EMail = z_frmLogIn.DonneesRetour["Mail"].ToString();

                z_frmLogIn.Dispose();
                // Ajout em base
                Boolean z_blnConnect = Variables.ConnexionBase.OpenBDD();
                try
                {
                    String z_strSql = "Update tableutilisateur Set DateDerConnexion = " + SosMedecins.Connexion.FormatSql.Format_DateHeure(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")) + " Where CodeUtilisateur = " + SosMedecins.Connexion.FormatSql.Format_String(VariablesApplicatives.Utilisateurs.Identifiant);
                    // mise a jour
                    Variables.ConnexionBase.ExecuteSqlSansRetour(z_strSql);
                }
                finally
                {
                    if (z_blnConnect)
                    {
                        Variables.ConnexionBase.CloseBDD();
                    }
                }
                
                OutilsExt.OutilsSql = new MySql(SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli, Application.StartupPath + "\\Error.log");
                // Ouverture de la fenetre principal
                frmGeneral z_frmMain = new frmGeneral();

                z_frmMain.ShowDialog();
                z_frmMain.Dispose();
            }
            else
            {
                // Annulation de la connexion
                z_frmLogIn.Dispose();
            }


            //*****Player ***** fermeture de l'application            
           // VlcContext.CloseAll();  //fermeture de VlcContext            
            Application.Exit();
        }

        static void DoSplash()
        {
            _frmSplash = new frmSplash();
            Application.Run(_frmSplash);
        }

        private const string TexteRefusVisa = "Le rapport a t lu par le mdecin responsable, le visa n'a pas t accord.";

		private bool m_rapportOuvertDepuisListe = false;
		// ************************************************************************************
		// Variables Perso
		// ************************************************************************************
		public GradientCellType Gradient1 = new GradientCellType();
		public GradientCellType Gradient2 = new GradientCellType();
        private const string TexteRefusVisa = "Le rapport a t lu par le mdecin responsable, le visa n`a pas t accord.";

        private TextBox TxtEnCours = null;
		private bool TxtChoixMultiple = false;
		private string TxtValDefaut = "";

		private DataRow LigneEnCours=null;
		public frmFacturation m_frmActualFactu=null;
	
		private Hashtable tblOfForm=new Hashtable();

		private frmListeRapportAViser m_frmLstRapportToSend=null;
        private bool m_rapportOuvertDepuisListe = false;



        //*************************************************************************************
        //   Variables pour le lecteur de dictée
        //*************************************************************************************
        String FichierEncours = null;
        public string media = null;   
		
		// ************************************************************************************
		// Variables controles de la form

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TabControl tab;
        private System.Windows.Forms.TabPage tbTraitement;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.Label lblRaisonSociale;
		private System.Windows.Forms.Panel pan_Statiques;
		private System.Windows.Forms.Label lblDateAppel;
		private System.Windows.Forms.Label lblMotif1;
		private System.Windows.Forms.Label lblMotif2;
		private System.Windows.Forms.Label lblUrgence;
		private System.Windows.Forms.Label lblAnnulation;
		private System.Windows.Forms.Label lblMotifAnnulation;
		private System.Windows.Forms.Label lblDevenirAnnulation;
		private System.Windows.Forms.Label lblMedecin;
		private System.Windows.Forms.Label LblHTR;
		private System.Windows.Forms.Label LblSLL;
		private System.Windows.Forms.Label lblHFI;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label lbMotif1;
		private System.Windows.Forms.Label lbUrgence;
		private System.Windows.Forms.Label lbMotif2;
		private System.Windows.Forms.Label lbFIN;
        private System.Windows.Forms.Label lbSLL;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Panel pan_Dynamique;
		private System.Windows.Forms.Panel pan_Patient;
        private System.Windows.Forms.TextBox txtPatient_Adresse2;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.TextBox txtPatient_Longitude;
		private System.Windows.Forms.TextBox txtPatient_Latitude;
		private System.Windows.Forms.TextBox txtPatient_Porte;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox txtPatient_Internom;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.TextBox txtPatient_Digicode;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.TextBox txtPatient_Etage;
        private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox txtPatient_NumRue;
		private System.Windows.Forms.TextBox txtPatient_Localite;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox txtPatient_Adresse1;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TextBox txtPatient_Sexe;
		private System.Windows.Forms.TextBox txtPatient_UniteAge;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox txtPatient_Age;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox txtPatient_Prenom;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtPatient_Nom;
        private System.Windows.Forms.Label lblPatient_Nom;
		private System.Windows.Forms.TabControl tabTravail;
		private System.Windows.Forms.TabPage tbFiche;
		private System.Windows.Forms.TabPage tbFacturation;
		private System.Windows.Forms.TabPage tbRapport;
		//private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private System.Windows.Forms.RichTextBox txtSaisieRapport;
		private System.Windows.Forms.Button btnRapport_Couleur;
		private System.Windows.Forms.Button btnRapport_Font;
		private System.Windows.Forms.Button cmdRapport_Concerne;
		private System.Windows.Forms.Button cmdRapport_Destinataire;
        private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem mnuFichier;
        private System.Windows.Forms.MenuItem mnuQuitter;
		private System.Windows.Forms.MenuItem mnuParametres;
        private System.Windows.Forms.MenuItem mnuDonnees;
		private System.Windows.Forms.TextBox TxtNPA;
        private System.Windows.Forms.Label label38;
		private System.Windows.Forms.ListBox LstAide;
		private FarPoint.Win.Spread.FpSpread fpRapport;
		private FarPoint.Win.Spread.SheetView fpRapport_Sheet1;
		private System.Windows.Forms.Label label39;
		private System.Windows.Forms.TextBox txtDateNaissance;
        private System.Windows.Forms.PictureBox picSaveFiche;
		private System.Windows.Forms.PictureBox pic_ValideRapport;
		private System.Windows.Forms.Button button7;
		private System.Windows.Forms.Button cmdRapport_Salutations;
		private System.Windows.Forms.Button cmdRapport_Signature;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.GroupBox groupBox6;
		private System.Windows.Forms.Label label42;
		private System.Windows.Forms.Label label44;
		private System.Windows.Forms.Label label45;
		private System.Windows.Forms.ComboBox cbRapport_Format;
		private System.Windows.Forms.ComboBox cbRapport_Imprimante;
		private System.Windows.Forms.TextBox txtRapport_NbCopies;
		private System.Windows.Forms.PictureBox picRapport_Export1;
		private System.Windows.Forms.PictureBox picRapport_Export2;
		private System.Windows.Forms.PictureBox picRapport_Print2;
		private System.Windows.Forms.PictureBox picRapport_Print1;
		private System.Windows.Forms.GroupBox groupBox7;
		private System.Windows.Forms.PictureBox picRapport_Mail2;
		private System.Windows.Forms.PictureBox picRapport_Mail1;
		private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label lblTest;
		private System.Windows.Forms.TabPage tbRecherche;
		private System.Windows.Forms.Label label46;
		private System.Windows.Forms.Label label47;
		private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Label label50;
		private System.Windows.Forms.CheckBox ChkFiltre1;
		private System.Windows.Forms.CheckBox ChkFiltre2;
		private System.Windows.Forms.CheckBox ChkFiltre3;
		private System.Windows.Forms.TextBox TxtFiltre1;
		private System.Windows.Forms.TextBox TxtFiltre3;
		private System.Windows.Forms.Panel panel8;
		private System.Windows.Forms.Label label49;
		private System.Windows.Forms.GroupBox groupBox8;
		private System.Windows.Forms.TextBox TxtRapport_Commentaire;
        private System.Windows.Forms.Button BtnRapport_Copier;
        private System.Windows.Forms.Button BtnRapport_Visa;
		private System.Windows.Forms.PictureBox btnOnglet1;
		private System.Windows.Forms.PictureBox btnOnglet2;
		private System.Windows.Forms.PictureBox btnOnglet3;
		private System.Windows.Forms.Button picRapport_Actualiser;
		private System.Windows.Forms.TabControl TabActionRapport;
		private System.Windows.Forms.TabPage tbCreation;
		private System.Windows.Forms.TabPage tbVisa;
		private System.Windows.Forms.TextBox TxtRapport_CommentaireVisa;
		private System.Windows.Forms.TabPage tbCommunication;
		private System.Windows.Forms.Panel panel9;
		private System.Windows.Forms.Button btnRapport_Onglet1;
		private System.Windows.Forms.Button btnRapport_Onglet2;
		private System.Windows.Forms.Button btnRapport_Onglet3;
		private FarPoint.Win.Spread.FpSpread fpFiche_Historique;
		private FarPoint.Win.Spread.SheetView fpFiche_Historique_Sheet1;
		private System.Windows.Forms.Label label51;
		private System.Windows.Forms.TextBox txtRapport_CommentaireSauvegarde;
		private System.Windows.Forms.Label label52;
		private System.Windows.Forms.Panel panel10;
		private System.Windows.Forms.TextBox txtFiche_CommentairSauvegarde;
		private System.Windows.Forms.Label label53;
		private System.Windows.Forms.Label label54;
		private System.Windows.Forms.Label LblRapportCree;
		private System.Windows.Forms.Label LblRapportModifie;
		private System.Windows.Forms.Label LblRapportVise;
		private System.Windows.Forms.PictureBox picRapport_OptConstat;
		private System.Windows.Forms.PictureBox picRapport_OptRapport;
		private System.Windows.Forms.PictureBox picRapport_OptSans;
		private System.Windows.Forms.Label label55;
		private System.Windows.Forms.Label label56;
		private System.Windows.Forms.Label label57;
		private System.Windows.Forms.LinkLabel lnkRapport_AjoutDestinataire;
		private FarPoint.Win.Spread.FpSpread fpRapport_Destinataires;
		private System.Windows.Forms.Button cmdRapport_Bonjour;
		private System.Windows.Forms.Button cmdRapport_Intro;
		private System.Windows.Forms.Button cmdRapport_EnTete;
		private System.Windows.Forms.Button cmdRapport_Corps;
		private System.Windows.Forms.RichTextBox rtfConvert;
        private System.Windows.Forms.MenuItem mnuTA;
		private System.Windows.Forms.Button BtnRapport_RefusVisa;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem mnuRapports;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem mnuStatistiques;
		private FarPoint.Win.Spread.SheetView fpRapport_Destinataires_Sheet1;
        private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.TextBox TxtFiltre2;
        private System.Windows.Forms.TextBox txtRecherchePrenom;
		private System.Windows.Forms.Label LblStatusSauvegardeFiche;
		private System.Windows.Forms.GroupBox groupBox9;
		private System.Windows.Forms.Label label64;
		private System.Windows.Forms.Label label66;
		private System.Windows.Forms.Label label67;
		private System.Windows.Forms.TextBox txtPatient_AdmNPA;
		private System.Windows.Forms.TextBox txtPatient_AdmNumRue;
		private System.Windows.Forms.TextBox txtPatient_AdmLocalite;
		private System.Windows.Forms.TextBox txtPatient_AdmAdresse1;
		private System.Windows.Forms.TextBox txtPatient_AdmBatiment;
        private System.Windows.Forms.Label label65;
		private System.Windows.Forms.Label label41;
		private System.Windows.Forms.Label label68;
        private System.Windows.Forms.Button btnRapportCourant;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.Label LblSauvegardeRapport;
        private System.Windows.Forms.Label lblConnecte;
		private System.Windows.Forms.Button btnCorriger;
		private System.Windows.Forms.Button btnSupprimerRapport;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.Button btnEnlevCorrection;
		private System.Windows.Forms.MenuItem mnuFip;
        private System.Windows.Forms.MenuItem mnuFenetres;
		private System.Windows.Forms.MenuItem mnuMedTT;
		private System.Windows.Forms.MenuItem mnuFiches;
		private System.Windows.Forms.ListBox lstEnvois;
		private System.Windows.Forms.CheckBox chkLogo;
		private System.Windows.Forms.CheckBox chkFax;
		private System.Windows.Forms.Label lbAge;
		private System.Windows.Forms.Label label70;
		private System.Windows.Forms.Label lbDuree;
		private System.Windows.Forms.Label lbDelai;
		private System.Windows.Forms.Label label72;
		private System.Windows.Forms.Label label73;
		private System.Windows.Forms.Label lbRCP;
		private System.Windows.Forms.MenuItem mnuFacturation;
		private System.Windows.Forms.MenuItem mnuFactures;
		private System.Windows.Forms.MenuItem mnuDocJoint;
		private System.Windows.Forms.MenuItem mnuRechercheFacture;
		private System.Windows.Forms.MenuItem mnuCollabo;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem mnuAide;
        private System.Windows.Forms.MenuItem mnuFac_Impression;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.Button btModif;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.MenuItem menuItem10;
        private System.Windows.Forms.MenuItem menuItem11;
        private System.Windows.Forms.MenuItem menuItem13;
		private System.Windows.Forms.MenuItem menuItem19;
        private System.Windows.Forms.MenuItem menuItem20;
        private Button btnRechercher;
		private System.ComponentModel.IContainer components;


        #endregion

        private Button btnCreationConsultation;
        private Button btnParametrages;
        private Button btnFermeture;
        private ctrlConsultationEtat ctrlConsultationEtat = new ctrlConsultationEtat();

        private ctrlFichePatient _ctrlFichePatient = new ctrlFichePatient();
        private ZoomImageViewer zoomImageViewer1;
        private Button btnVoirAppels;
        private TextBox txtRechercheRapport;
        private CheckBox chkRapport;
        private TextBox txtRechercheIndex;
        private CheckBox chkIndex;
        private CheckBox chkOrigine;
        private ComboBox cbOrigine;
        private CheckBox chkDate;
        private CheckBox chkMotif;
        private CheckBox ChkMedecin;
        private DateTimePicker dateTimePicker2;
        private DateTimePicker dateTimePicker1;
        private ComboBox cbMotif;
        private ComboBox cbMedecin;
        private Label label6;
        private MenuItem mnuUtilisateurs;
        private Button btnEchangeMedicall;
        private Label LblBaseTest;
        private MenuItem mnuFacturation_Etats;
        private MenuItem mnuFacturation_Etats_Relance;
        private MenuItem mnuFacturation_Etats_VerificationSolde;
        private MenuItem mnuFacturation_Etats_Arrangement;
        private BindingSource _bseDonneesAppel = new BindingSource();
        private MenuItem mnuFacturation_Etats_Relance_Assurances;
        private ComboBox CBRoute1;
        private Label Lroute1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox txtPatient_AdmAdresse2;
        private Label label5;
        private Label label15;
        private ComboBox CBRoute_adm1;
        private Label label20;
        private MaskedTextBox EMaskTel1;
        private MenuItem menuImportEpos;
        private PictureBox pictureBox1;
        private MenuItem menuItem12;
        private MenuItem TA_Abonnement;
        private PictureBox PBoxAudio;
        private MenuItem menuItem22;
        private MenuItem menuItem21;
        private Panel panel4;
        private TrackBar tBarVol;
        private TrackBar tBarTps;
        private Label LAvancement;
        private Label LDuree;
        private Button Bplay;
        private ImageList imageList1;
        private Button Bstop;
        private Button Bpause;
        private VlcControl vlcControl1;
        private Label Lpasdictee;
        private BackgroundWorker backgroundWorker1;
        private CheckBox checkBoxESP;
        private Button bCarteAvs;
        private MenuItem menuAttestationTA;
        private TextBox txtEmail;
        private Label label14;
        private MenuItem menuItem14;
        private PictureBox pBFiches;
        private Panel panelFond;
        private MenuItem menuItem15;
        private Panel panel6;
        private TextBox tBoxComVisite;
        private Label label19;
        private MenuItem menuFacturesImpayees;
        private MenuItem menuListe2emeRappel;
        private MenuItem menuPoursuite;
        private MenuItem menuSalairesMed;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private FarPoint.Win.Spread.FpSpread fpAppels;
        private FarPoint.Win.Spread.SheetView fpAppels_Sheet1;
        private Label labelInfoReport;
        private Button bRotationImage;
        private MenuItem menuItem16;
        private MenuItem menuItem17;

        #region Contruction / Destruction de la classe

        static SosMedecins.SmartRapport.GestionApplication.frmSplash _frmSplash;  
		// ************************************************************************************
		// Constructeur de la classe sans arguments
		// ************************************************************************************
		public frmGeneral()
		{
			//
			// Requis pour la prise en charge du Concepteur Windows Forms
			//
			InitializeComponent();
            this.zoomImageViewer1.MouseWheel += new MouseEventHandler(zoomImageViewer1_MouseWheel);
            // Prevention base test
            VerificationBaseTest();

			//string test = WorkedString.GetFactureCodeB(487753,3318);
			//string code2 = WorkedString.GetFactureCode2(test);

			this.Location = new Point(0,0);
			this.Size =new Size(1612,990);
			// Répertoire de sauvegarde des erreurs
			if(!System.IO.Directory.Exists(Application.StartupPath + "\\" + "Sauvegardes")) System.IO.Directory.CreateDirectory(Application.StartupPath + "\\" + "Sauvegardes");
			// Répertoire de sauvegarde des exports
			if(!System.IO.Directory.Exists(Application.StartupPath + "\\" + "Export")) System.IO.Directory.CreateDirectory(Application.StartupPath + "\\" + "Export");
			if(!System.IO.Directory.Exists(Application.StartupPath + "\\" + "Export\\doc")) System.IO.Directory.CreateDirectory(Application.StartupPath + "\\" + "Export\\doc");
			if(!System.IO.Directory.Exists(Application.StartupPath + "\\" + "Export\\rtf")) System.IO.Directory.CreateDirectory(Application.StartupPath + "\\" + "Export\\rtf");
			if(!System.IO.Directory.Exists(Application.StartupPath + "\\" + "Export\\xls")) System.IO.Directory.CreateDirectory(Application.StartupPath + "\\" + "Export\\xls");
			if(!System.IO.Directory.Exists(Application.StartupPath + "\\" + "Export\\pdf")) System.IO.Directory.CreateDirectory(Application.StartupPath + "\\" + "Export\\pdf");
			if(!System.IO.Directory.Exists(Application.StartupPath + "\\" + "Export\\htm")) System.IO.Directory.CreateDirectory(Application.StartupPath + "\\" + "Export\\htm");
			if(!System.IO.Directory.Exists(Application.StartupPath + "\\" + "Export\\Graphiques")) System.IO.Directory.CreateDirectory(Application.StartupPath + "\\" + "Export\\Graphiques");

			// Numero de version : 
			this.Text+=" - [Version : " + Application.ProductVersion + "]";

			// Mise en place des controles
			MiseEnPlaceControles();
			MiseEnPlaceMenus();

			// Sélection de l'onglet d'identification et l'onglet fiche d'appel par défaut
			tab.SelectedIndex = 3;
			tabTravail.SelectedIndex  = 0;

            lblRaisonSociale.Text = "Bienvenue " + SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.RaisonSociale;

            // Chargement des imprimantes dans la liste des Imprimantes : Sélection de l'imprimante par défaut :
            System.Drawing.Printing.PrintDocument prtdoc = new System.Drawing.Printing.PrintDocument();
            string strDefaultPrinter = prtdoc.PrinterSettings.PrinterName;
            foreach (String strPrinter in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                cbRapport_Imprimante.Items.Add(strPrinter);
                if (strPrinter.ToLower() == SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.StrReportPrinter.ToLower())
                {
                    cbRapport_Imprimante.SelectedIndex = cbRapport_Imprimante.Items.IndexOf(strPrinter);
                }
            }
            prtdoc.Dispose();
            prtdoc = null;
            

            //
            if (!System.IO.Directory.Exists(System.Windows.Forms.Application.StartupPath + SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.Cache))
			{
                System.IO.Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.Cache);
			}

            if (VariablesApplicatives.blnVersionDev)
            {
                cacheActuelle();
                panel1.Controls.Add(_ctrlFichePatient);
                panel1.Controls[_ctrlFichePatient.Name].Dock = DockStyle.Fill;

                ctrlConsultationEtat.Visible = true;
            }
            else
            {
                ctrlConsultationEtat.Visible = false;
            }
		}
		// ************************************************************************************
		private void frmGeneral_Load(object sender,EventArgs e)
		{
           
            GestionDroits(); 
			// Ouverture en tant quer service pour envoi automatique des fax???
			string strCommandLine = Environment.CommandLine;
			if(strCommandLine!="" && strCommandLine.IndexOf("auto")>-1)
			{
				LancementProcedureAutomatique();
			}
		}

        private void MiseEnPlaceControles()
        {
            // Effet dégradé des cellules
            Gradient1 = new GradientCellType();
            Gradient2 = new GradientCellType();
            //Gradient1.BottomColor = Color.WhiteSmoke;
            //Gradient1.TopColor = Color.White;

            Gradient2.TopColor = Color.LightGray;
            Gradient2.BottomColor = Color.LightGray;

            LstAide.Visible = false;
            // initialisation du tableau des appels
            fpAppels_Sheet1.ColumnCount = 7;
            fpAppels_Sheet1.Rows.Default.Height = 18;
            fpAppels_Sheet1.Columns[0].Width = fpAppels.Width * 7 / 100;
            fpAppels_Sheet1.Columns[1].Width = fpAppels.Width * 14 / 100;
            fpAppels_Sheet1.Columns[2].Width = fpAppels.Width * 14 / 100;
            fpAppels_Sheet1.Columns[3].Width = fpAppels.Width * 18 / 100;
            fpAppels_Sheet1.Columns[4].Width = fpAppels.Width * 28 / 100;
            fpAppels_Sheet1.Columns[5].Width = fpAppels.Width * 15 / 100;
            fpAppels_Sheet1.Columns[6].Width = fpAppels.Width * 6 / 100 - 10;

            // initialisation du tableau des rapports
            fpRapport_Sheet1.ColumnCount = 2;
            fpRapport_Sheet1.Rows.Default.Height = 18;
            fpRapport_Sheet1.Columns[0].Width = fpRapport.Width * 45 / 100;
            fpRapport_Sheet1.Columns[1].Width = fpRapport.Width * 45 / 100;

            tabTravail.Enabled = false;
            btnOnglet1.Enabled = false;
            btnOnglet2.Enabled = false;
            btnOnglet3.Enabled = false;

            this.crystalReportViewer1.DisplayToolbar = true;
        }

        private void MiseEnPlaceMenus()
        {
            foreach (MenuItem mnu in this.mainMenu1.MenuItems)
            {
                mnu.Enabled = false;
            }
            mnuFichier.Enabled = true;
            mnuQuitter.Enabled = true;
        }

		/// <summary>
		/// Nettoyage des ressources utilisées.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGeneral));
            this.zoomImageViewer1 = new ImportSosGeneve.ZoomImageViewer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.fpAppels = new FarPoint.Win.Spread.FpSpread();
            this.fpAppels_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.tabTravail = new System.Windows.Forms.TabControl();
            this.tbFiche = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pan_Dynamique = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.tBoxComVisite = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.btnCreationConsultation = new System.Windows.Forms.Button();
            this.pan_Patient = new System.Windows.Forms.Panel();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.checkBoxESP = new System.Windows.Forms.CheckBox();
            this.PBoxAudio = new System.Windows.Forms.PictureBox();
            this.EMaskTel1 = new System.Windows.Forms.MaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Lroute1 = new System.Windows.Forms.Label();
            this.CBRoute1 = new System.Windows.Forms.ComboBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.CBRoute_adm1 = new System.Windows.Forms.ComboBox();
            this.txtPatient_AdmAdresse2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPatient_AdmBatiment = new System.Windows.Forms.TextBox();
            this.label65 = new System.Windows.Forms.Label();
            this.txtPatient_AdmNPA = new System.Windows.Forms.TextBox();
            this.label64 = new System.Windows.Forms.Label();
            this.txtPatient_AdmNumRue = new System.Windows.Forms.TextBox();
            this.txtPatient_AdmLocalite = new System.Windows.Forms.TextBox();
            this.label66 = new System.Windows.Forms.Label();
            this.txtPatient_AdmAdresse1 = new System.Windows.Forms.TextBox();
            this.label67 = new System.Windows.Forms.Label();
            this.LstAide = new System.Windows.Forms.ListBox();
            this.label39 = new System.Windows.Forms.Label();
            this.txtDateNaissance = new System.Windows.Forms.TextBox();
            this.TxtNPA = new System.Windows.Forms.TextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.txtPatient_Adresse2 = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtPatient_Longitude = new System.Windows.Forms.TextBox();
            this.txtPatient_Latitude = new System.Windows.Forms.TextBox();
            this.txtPatient_Porte = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtPatient_Internom = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtPatient_Digicode = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtPatient_Etage = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtPatient_NumRue = new System.Windows.Forms.TextBox();
            this.txtPatient_Localite = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtPatient_Adresse1 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtPatient_Sexe = new System.Windows.Forms.TextBox();
            this.txtPatient_UniteAge = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPatient_Age = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPatient_Prenom = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPatient_Nom = new System.Windows.Forms.TextBox();
            this.lblPatient_Nom = new System.Windows.Forms.Label();
            this.panel10 = new System.Windows.Forms.Panel();
            this.LblStatusSauvegardeFiche = new System.Windows.Forms.Label();
            this.txtFiche_CommentairSauvegarde = new System.Windows.Forms.TextBox();
            this.label53 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.picSaveFiche = new System.Windows.Forms.PictureBox();
            this.tbRapport = new System.Windows.Forms.TabPage();
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.bCarteAvs = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.Lpasdictee = new System.Windows.Forms.Label();
            this.vlcControl1 = new Vlc.DotNet.Forms.VlcControl();
            this.Bstop = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.Bpause = new System.Windows.Forms.Button();
            this.Bplay = new System.Windows.Forms.Button();
            this.LAvancement = new System.Windows.Forms.Label();
            this.LDuree = new System.Windows.Forms.Label();
            this.tBarTps = new System.Windows.Forms.TrackBar();
            this.tBarVol = new System.Windows.Forms.TrackBar();
            this.btnRapportCourant = new System.Windows.Forms.Button();
            this.label68 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.btnRapport_Onglet3 = new System.Windows.Forms.Button();
            this.btnRapport_Onglet2 = new System.Windows.Forms.Button();
            this.btnRapport_Onglet1 = new System.Windows.Forms.Button();
            this.TabActionRapport = new System.Windows.Forms.TabControl();
            this.tbCreation = new System.Windows.Forms.TabPage();
            this.btModif = new System.Windows.Forms.Button();
            this.btnCorriger = new System.Windows.Forms.Button();
            this.btnSupprimerRapport = new System.Windows.Forms.Button();
            this.rtfConvert = new System.Windows.Forms.RichTextBox();
            this.label57 = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.picRapport_OptSans = new System.Windows.Forms.PictureBox();
            this.picRapport_OptConstat = new System.Windows.Forms.PictureBox();
            this.picRapport_OptRapport = new System.Windows.Forms.PictureBox();
            this.tbVisa = new System.Windows.Forms.TabPage();
            this.btnEnlevCorrection = new System.Windows.Forms.Button();
            this.label52 = new System.Windows.Forms.Label();
            this.TxtRapport_CommentaireVisa = new System.Windows.Forms.TextBox();
            this.BtnRapport_RefusVisa = new System.Windows.Forms.Button();
            this.BtnRapport_Copier = new System.Windows.Forms.Button()
            this.BtnRapport_Visa = new System.Windows.Forms.Button();
            this.tbCommunication = new System.Windows.Forms.TabPage();
            this.fpRapport_Destinataires = new FarPoint.Win.Spread.FpSpread();
            this.fpRapport_Destinataires_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.lnkRapport_AjoutDestinataire = new System.Windows.Forms.LinkLabel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.picRapport_Export2 = new System.Windows.Forms.PictureBox();
            this.picRapport_Export1 = new System.Windows.Forms.PictureBox();
            this.cbRapport_Format = new System.Windows.Forms.ComboBox();
            this.label42 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.picRapport_Mail2 = new System.Windows.Forms.PictureBox();
            this.picRapport_Mail1 = new System.Windows.Forms.PictureBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.chkFax = new System.Windows.Forms.CheckBox();
            this.chkLogo = new System.Windows.Forms.CheckBox();
            this.picRapport_Print2 = new System.Windows.Forms.PictureBox();
            this.picRapport_Print1 = new System.Windows.Forms.PictureBox();
            this.cbRapport_Imprimante = new System.Windows.Forms.ComboBox();
            this.txtRapport_NbCopies = new System.Windows.Forms.TextBox();
            this.label45 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.lstEnvois = new System.Windows.Forms.ListBox();
            this.TxtRapport_Commentaire = new System.Windows.Forms.TextBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.LblSauvegardeRapport = new System.Windows.Forms.Label();
            this.LblRapportVise = new System.Windows.Forms.Label();
            this.LblRapportModifie = new System.Windows.Forms.Label();
            this.LblRapportCree = new System.Windows.Forms.Label();
            this.txtRapport_CommentaireSauvegarde = new System.Windows.Forms.TextBox();
            this.label51 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.pic_ValideRapport = new System.Windows.Forms.PictureBox();
            this.lblTest = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.fpRapport = new FarPoint.Win.Spread.FpSpread();
            this.fpRapport_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btnRapport_Font = new System.Windows.Forms.Button();
            this.btnRapport_Couleur = new System.Windows.Forms.Button();
            this.txtSaisieRapport = new System.Windows.Forms.RichTextBox();
            this.cmdRapport_Bonjour = new System.Windows.Forms.Button();
            this.cmdRapport_Corps = new System.Windows.Forms.Button();
            this.picRapport_Actualiser = new System.Windows.Forms.Button();
            this.cmdRapport_Signature = new System.Windows.Forms.Button();
            this.cmdRapport_Salutations = new System.Windows.Forms.Button();
            this.cmdRapport_Intro = new System.Windows.Forms.Button();
            this.cmdRapport_EnTete = new System.Windows.Forms.Button();
            this.cmdRapport_Concerne = new System.Windows.Forms.Button();
            this.cmdRapport_Destinataire = new System.Windows.Forms.Button();
            this.tbFacturation = new System.Windows.Forms.TabPage();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.bRotationImage = new System.Windows.Forms.Button();
            this.labelInfoReport = new System.Windows.Forms.Label();
            this.pBFiches = new System.Windows.Forms.PictureBox();
            this.LblBaseTest = new System.Windows.Forms.Label();
            this.pan_Statiques = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lbDuree = new System.Windows.Forms.Label();
            this.lbDelai = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblDevenirAnnulation = new System.Windows.Forms.Label();
            this.lblMotifAnnulation = new System.Windows.Forms.Label();
            this.lblAnnulation = new System.Windows.Forms.Label();
            this.label72 = new System.Windows.Forms.Label();
            this.label73 = new System.Windows.Forms.Label();
            this.lbFIN = new System.Windows.Forms.Label();
            this.lbSLL = new System.Windows.Forms.Label();
            this.lbRCP = new System.Windows.Forms.Label();
            this.lblHFI = new System.Windows.Forms.Label();
            this.LblSLL = new System.Windows.Forms.Label();
            this.LblHTR = new System.Windows.Forms.Label();
            this.lblMedecin = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lbAge = new System.Windows.Forms.Label();
            this.label70 = new System.Windows.Forms.Label();
            this.lbMotif1 = new System.Windows.Forms.Label();
            this.lbUrgence = new System.Windows.Forms.Label();
            this.lbMotif2 = new System.Windows.Forms.Label();
            this.lblMotif1 = new System.Windows.Forms.Label();
            this.lblDateAppel = new System.Windows.Forms.Label();
            this.lblUrgence = new System.Windows.Forms.Label();
            this.lblMotif2 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnEchangeMedicall = new System.Windows.Forms.Button();
            this.btnFermeture = new System.Windows.Forms.Button();
            this.btnParametrages = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.fpFiche_Historique = new FarPoint.Win.Spread.FpSpread();
            this.fpFiche_Historique_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.lblConnecte = new System.Windows.Forms.Label();
            this.tab = new System.Windows.Forms.TabControl();
            this.tbTraitement = new System.Windows.Forms.TabPage();
            this.btnVoirAppels = new System.Windows.Forms.Button();
            this.txtRechercheRapport = new System.Windows.Forms.TextBox();
            this.chkRapport = new System.Windows.Forms.CheckBox();
            this.txtRechercheIndex = new System.Windows.Forms.TextBox();
            this.chkIndex = new System.Windows.Forms.CheckBox();
            this.chkOrigine = new System.Windows.Forms.CheckBox();
            this.cbOrigine = new System.Windows.Forms.ComboBox();
            this.chkDate = new System.Windows.Forms.CheckBox();
            this.chkMotif = new System.Windows.Forms.CheckBox();
            this.ChkMedecin = new System.Windows.Forms.CheckBox();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.cbMotif = new System.Windows.Forms.ComboBox();
            this.cbMedecin = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbRecherche = new System.Windows.Forms.TabPage();
            this.btnRechercher = new System.Windows.Forms.Button();
            this.txtRecherchePrenom = new System.Windows.Forms.TextBox();
            this.TxtFiltre3 = new System.Windows.Forms.TextBox();
            this.TxtFiltre2 = new System.Windows.Forms.TextBox();
            this.TxtFiltre1 = new System.Windows.Forms.TextBox();
            this.ChkFiltre3 = new System.Windows.Forms.CheckBox();
            this.ChkFiltre2 = new System.Windows.Forms.CheckBox();
            this.ChkFiltre1 = new System.Windows.Forms.CheckBox();
            this.label50 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.btnOnglet3 = new System.Windows.Forms.PictureBox();
            this.btnOnglet2 = new System.Windows.Forms.PictureBox();
            this.btnOnglet1 = new System.Windows.Forms.PictureBox();
            this.lblRaisonSociale = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.mnuFichier = new System.Windows.Forms.MenuItem();
            this.mnuParametres = new System.Windows.Forms.MenuItem();
            this.mnuQuitter = new System.Windows.Forms.MenuItem();
            this.mnuDonnees = new System.Windows.Forms.MenuItem();
            this.menuImportEpos = new System.Windows.Forms.MenuItem();
            this.mnuFiches = new System.Windows.Forms.MenuItem();
            this.mnuFip = new System.Windows.Forms.MenuItem();
            this.mnuMedTT = new System.Windows.Forms.MenuItem();
            this.mnuCollabo = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.mnuUtilisateurs = new System.Windows.Forms.MenuItem();
            this.menuItem17 = new System.Windows.Forms.MenuItem();
            this.mnuTA = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.menuItem10 = new System.Windows.Forms.MenuItem();
            this.menuItem13 = new System.Windows.Forms.MenuItem();
            this.menuAttestationTA = new System.Windows.Forms.MenuItem();
            this.menuItem14 = new System.Windows.Forms.MenuItem();
            this.menuItem12 = new System.Windows.Forms.MenuItem();
            this.TA_Abonnement = new System.Windows.Forms.MenuItem();
            this.mnuRapports = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.mnuStatistiques = new System.Windows.Forms.MenuItem();
            this.menuItem22 = new System.Windows.Forms.MenuItem();
            this.menuItem21 = new System.Windows.Forms.MenuItem();
            this.menuItem16 = new System.Windows.Forms.MenuItem();
            this.menuItem19 = new System.Windows.Forms.MenuItem();
            this.menuItem20 = new System.Windows.Forms.MenuItem();
            this.menuSalairesMed = new System.Windows.Forms.MenuItem();
            this.mnuFacturation = new System.Windows.Forms.MenuItem();
            this.mnuFactures = new System.Windows.Forms.MenuItem();
            this.mnuDocJoint = new System.Windows.Forms.MenuItem();
            this.mnuRechercheFacture = new System.Windows.Forms.MenuItem();
            this.mnuFac_Impression = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuItem11 = new System.Windows.Forms.MenuItem();
            this.mnuFacturation_Etats = new System.Windows.Forms.MenuItem();
            this.mnuFacturation_Etats_Relance = new System.Windows.Forms.MenuItem();
            this.mnuFacturation_Etats_Relance_Assurances = new System.Windows.Forms.MenuItem();
            this.mnuFacturation_Etats_VerificationSolde = new System.Windows.Forms.MenuItem();
            this.mnuFacturation_Etats_Arrangement = new System.Windows.Forms.MenuItem();
            this.menuItem15 = new System.Windows.Forms.MenuItem();
            this.menuFacturesImpayees = new System.Windows.Forms.MenuItem();
            this.menuListe2emeRappel = new System.Windows.Forms.MenuItem();
            this.menuPoursuite = new System.Windows.Forms.MenuItem();
            this.mnuFenetres = new System.Windows.Forms.MenuItem();
            this.mnuAide = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panelFond = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpAppels)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpAppels_Sheet1)).BeginInit();
            this.tabTravail.SuspendLayout();
            this.tbFiche.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pan_Dynamique.SuspendLayout();
            this.panel6.SuspendLayout();
            this.pan_Patient.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PBoxAudio)).BeginInit();
            this.groupBox9.SuspendLayout();
            this.panel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSaveFiche)).BeginInit();
            this.tbRapport.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vlcControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tBarTps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tBarVol)).BeginInit();
            this.panel9.SuspendLayout();
            this.TabActionRapport.SuspendLayout();
            this.tbCreation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picRapport_OptSans)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRapport_OptConstat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRapport_OptRapport)).BeginInit();
            this.tbVisa.SuspendLayout();
            this.tbCommunication.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpRapport_Destinataires)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpRapport_Destinataires_Sheet1)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picRapport_Export2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRapport_Export1)).BeginInit();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picRapport_Mail2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRapport_Mail1)).BeginInit();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picRapport_Print2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRapport_Print1)).BeginInit();
            this.groupBox8.SuspendLayout();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_ValideRapport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpRapport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpRapport_Sheet1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBFiches)).BeginInit();
            this.pan_Statiques.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpFiche_Historique)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpFiche_Historique_Sheet1)).BeginInit();
            this.tab.SuspendLayout();
            this.tbTraitement.SuspendLayout();
            this.tbRecherche.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnglet3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnglet2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnglet1)).BeginInit();
            this.SuspendLayout();
            // 
            // zoomImageViewer1
            // 
            this.zoomImageViewer1.AutoScroll = true;
            this.zoomImageViewer1.AutoScrollMargin = new System.Drawing.Size(240, 164);
            this.zoomImageViewer1.BackColor = System.Drawing.Color.CadetBlue;
            this.zoomImageViewer1.Image = null;
            this.zoomImageViewer1.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            this.zoomImageViewer1.Location = new System.Drawing.Point(9, 769);
            this.zoomImageViewer1.Name = "zoomImageViewer1";
            this.zoomImageViewer1.Size = new System.Drawing.Size(240, 164);
            this.zoomImageViewer1.TabIndex = 73;
            this.zoomImageViewer1.Text = "zoomImageViewer1";
            this.zoomImageViewer1.Visible = false;
            this.zoomImageViewer1.Zoom = 1F;
            this.zoomImageViewer1.Click += new System.EventHandler(this.zoomImageViewer1_Click);
            this.zoomImageViewer1.MouseEnter += new System.EventHandler(this.zoomImageViewer1_MouseEnter);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.CadetBlue;
            this.panel1.Controls.Add(this.fpAppels);
            this.panel1.Controls.Add(this.button7);
            this.panel1.Controls.Add(this.button6);
            this.panel1.Controls.Add(this.tabTravail);
            this.panel1.Controls.Add(this.button5);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Location = new System.Drawing.Point(421, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1189, 979);
            this.panel1.TabIndex = 0;
            // 
            // fpAppels
            // 
            this.fpAppels.AccessibleDescription = "fpAppels, Sheet1, Row 0, Column 0, ";
            this.fpAppels.BackColor = System.Drawing.Color.Gainsboro;
            this.fpAppels.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders;
            this.fpAppels.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
            this.fpAppels.Location = new System.Drawing.Point(0, 27);
            this.fpAppels.Name = "fpAppels";
            this.fpAppels.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpAppels_Sheet1});
            this.fpAppels.Size = new System.Drawing.Size(1136, 463);
            this.fpAppels.TabIndex = 4;
            tipAppearance1.BackColor = System.Drawing.Color.CadetBlue;
            tipAppearance1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.fpAppels.TextTipAppearance = tipAppearance1;
            this.fpAppels.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpAppels.MouseMove += new System.Windows.Forms.MouseEventHandler(this.fpAppels_MouseMove);
            this.fpAppels.MouseUp += new System.Windows.Forms.MouseEventHandler(this.fpAppels_MouseUp);
            // 
            // fpAppels_Sheet1
            // 
            this.fpAppels_Sheet1.Reset();
            this.fpAppels_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpAppels_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            fpAppels_Sheet1.ColumnCount = 2;
            fpAppels_Sheet1.ColumnHeader.RowCount = 0;
            fpAppels_Sheet1.RowCount = 1;
            this.fpAppels_Sheet1.ActiveSkin = FarPoint.Win.Spread.DefaultSkins.Classic2;
            this.fpAppels_Sheet1.Models = ((FarPoint.Win.Spread.SheetView.DocumentModels)(resources.GetObject("fpAppels_Sheet1.Models")));
            this.fpAppels_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.ReadOnly;
            this.fpAppels_Sheet1.RowHeader.Visible = false;
            // 
            // button7
            // 
            this.button7.Enabled = false;
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button7.ForeColor = System.Drawing.Color.Black;
            this.button7.Image = ((System.Drawing.Image)(resources.GetObject("button7.Image")));
            this.button7.Location = new System.Drawing.Point(1071, 2);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(64, 20);
            this.button7.TabIndex = 13;
            this.button7.Text = "Etat";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button3_Click);
            // 
            // button6
            // 
            this.button6.Enabled = false;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button6.ForeColor = System.Drawing.Color.Black;
            this.button6.Image = ((System.Drawing.Image)(resources.GetObject("button6.Image")));
            this.button6.Location = new System.Drawing.Point(901, 3);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(164, 20);
            this.button6.TabIndex = 12;
            this.button6.Text = "Origine";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button3_Click);
            // 
            // tabTravail
            // 
            this.tabTravail.Controls.Add(this.tbFiche);
            this.tabTravail.Controls.Add(this.tbRapport);
            this.tabTravail.Controls.Add(this.tbFacturation);
            this.tabTravail.Location = new System.Drawing.Point(0, 98);
            this.tabTravail.Name = "tabTravail";
            this.tabTravail.SelectedIndex = 0;
            this.tabTravail.Size = new System.Drawing.Size(1156, 890);
            this.tabTravail.TabIndex = 1;
            this.tabTravail.SelectedIndexChanged += new System.EventHandler(this.tabTravail_SelectedIndexChanged);
            // 
            // tbFiche
            // 
            this.tbFiche.BackColor = System.Drawing.Color.CadetBlue;
            this.tbFiche.Controls.Add(this.panel3);
            this.tbFiche.Location = new System.Drawing.Point(4, 22);
            this.tbFiche.Name = "tbFiche";
            this.tbFiche.Size = new System.Drawing.Size(1148, 864);
            this.tbFiche.TabIndex = 0;
            this.tbFiche.Text = "Fiche";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.pan_Dynamique);
            this.panel3.Location = new System.Drawing.Point(3, 368);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1123, 377);
            this.panel3.TabIndex = 4;
            // 
            // pan_Dynamique
            // 
            this.pan_Dynamique.BackColor = System.Drawing.Color.CadetBlue;
            this.pan_Dynamique.Controls.Add(this.panel6);
            this.pan_Dynamique.Controls.Add(this.btnCreationConsultation);
            this.pan_Dynamique.Controls.Add(this.pan_Patient);
            this.pan_Dynamique.Controls.Add(this.panel10);
            this.pan_Dynamique.Location = new System.Drawing.Point(0, 3);
            this.pan_Dynamique.Name = "pan_Dynamique";
            this.pan_Dynamique.Size = new System.Drawing.Size(1122, 371);
            this.pan_Dynamique.TabIndex = 4;
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel6.Controls.Add(this.tBoxComVisite);
            this.panel6.Controls.Add(this.label19);
            this.panel6.Location = new System.Drawing.Point(851, 162);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(262, 120);
            this.panel6.TabIndex = 89;
            // 
            // tBoxComVisite
            // 
            this.tBoxComVisite.Location = new System.Drawing.Point(6, 26);
            this.tBoxComVisite.Multiline = true;
            this.tBoxComVisite.Name = "tBoxComVisite";
            this.tBoxComVisite.ReadOnly = true;
            this.tBoxComVisite.Size = new System.Drawing.Size(246, 82);
            this.tBoxComVisite.TabIndex = 71;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(3, 7);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(164, 16);
            this.label19.TabIndex = 70;
            this.label19.Text = "Commentaire de la visite:";
            // 
            // btnCreationConsultation
            // 
            this.btnCreationConsultation.Enabled = false;
            this.btnCreationConsultation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreationConsultation.Image = ((System.Drawing.Image)(resources.GetObject("btnCreationConsultation.Image")));
            this.btnCreationConsultation.Location = new System.Drawing.Point(1058, 318);
            this.btnCreationConsultation.Name = "btnCreationConsultation";
            this.btnCreationConsultation.Size = new System.Drawing.Size(60, 49);
            this.btnCreationConsultation.TabIndex = 88;
            this.btnCreationConsultation.UseVisualStyleBackColor = true;
            this.btnCreationConsultation.Visible = false;
            this.btnCreationConsultation.Click += new System.EventHandler(this.btnCreationConsultation_Click);
            // 
            // pan_Patient
            // 
            this.pan_Patient.BackColor = System.Drawing.Color.CadetBlue;
            this.pan_Patient.Controls.Add(this.txtEmail);
            this.pan_Patient.Controls.Add(this.label14);
            this.pan_Patient.Controls.Add(this.checkBoxESP);
            this.pan_Patient.Controls.Add(this.PBoxAudio);
            this.pan_Patient.Controls.Add(this.EMaskTel1);
            this.pan_Patient.Controls.Add(this.label4);
            this.pan_Patient.Controls.Add(this.label3);
            this.pan_Patient.Controls.Add(this.label2);
            this.pan_Patient.Controls.Add(this.Lroute1);
            this.pan_Patient.Controls.Add(this.CBRoute1);
            this.pan_Patient.Controls.Add(this.groupBox9);
            this.pan_Patient.Controls.Add(this.LstAide);
            this.pan_Patient.Controls.Add(this.label39);
            this.pan_Patient.Controls.Add(this.txtDateNaissance);
            this.pan_Patient.Controls.Add(this.TxtNPA);
            this.pan_Patient.Controls.Add(this.label38);
            this.pan_Patient.Controls.Add(this.txtPatient_Adresse2);
            this.pan_Patient.Controls.Add(this.label18);
            this.pan_Patient.Controls.Add(this.txtPatient_Longitude);
            this.pan_Patient.Controls.Add(this.txtPatient_Latitude);
            this.pan_Patient.Controls.Add(this.txtPatient_Porte);
            this.pan_Patient.Controls.Add(this.label10);
            this.pan_Patient.Controls.Add(this.txtPatient_Internom);
            this.pan_Patient.Controls.Add(this.label16);
            this.pan_Patient.Controls.Add(this.txtPatient_Digicode);
            this.pan_Patient.Controls.Add(this.label17);
            this.pan_Patient.Controls.Add(this.txtPatient_Etage);
            this.pan_Patient.Controls.Add(this.label13);
            this.pan_Patient.Controls.Add(this.label9);
            this.pan_Patient.Controls.Add(this.txtPatient_NumRue);
            this.pan_Patient.Controls.Add(this.txtPatient_Localite);
            this.pan_Patient.Controls.Add(this.label11);
            this.pan_Patient.Controls.Add(this.txtPatient_Adresse1);
            this.pan_Patient.Controls.Add(this.label12);
            this.pan_Patient.Controls.Add(this.txtPatient_Sexe);
            this.pan_Patient.Controls.Add(this.txtPatient_UniteAge);
            this.pan_Patient.Controls.Add(this.label8);
            this.pan_Patient.Controls.Add(this.txtPatient_Age);
            this.pan_Patient.Controls.Add(this.label7);
            this.pan_Patient.Controls.Add(this.txtPatient_Prenom);
            this.pan_Patient.Controls.Add(this.label1);
            this.pan_Patient.Controls.Add(this.txtPatient_Nom);
            this.pan_Patient.Controls.Add(this.lblPatient_Nom);
            this.pan_Patient.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pan_Patient.Location = new System.Drawing.Point(3, 3);
            this.pan_Patient.Name = "pan_Patient";
            this.pan_Patient.Size = new System.Drawing.Size(836, 364);
            this.pan_Patient.TabIndex = 0;
            // 
            // txtEmail
            // 
            this.txtEmail.BackColor = System.Drawing.Color.White;
            this.txtEmail.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.ForeColor = System.Drawing.Color.SlateBlue;
            this.txtEmail.Location = new System.Drawing.Point(460, 315);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(263, 21);
            this.txtEmail.TabIndex = 78;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(422, 319);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(47, 19);
            this.label14.TabIndex = 79;
            this.label14.Text = "Email";
            // 
            // checkBoxESP
            // 
            this.checkBoxESP.AutoSize = true;
            this.checkBoxESP.Location = new System.Drawing.Point(540, 290);
            this.checkBoxESP.Name = "checkBoxESP";
            this.checkBoxESP.Size = new System.Drawing.Size(50, 17);
            this.checkBoxESP.TabIndex = 77;
            this.checkBoxESP.Text = "ESP";
            this.checkBoxESP.UseVisualStyleBackColor = true;
            // 
            // PBoxAudio
            // 
            this.PBoxAudio.BackColor = System.Drawing.Color.Transparent;
            this.PBoxAudio.Image = ((System.Drawing.Image)(resources.GetObject("PBoxAudio.Image")));
            this.PBoxAudio.InitialImage = null;
            this.PBoxAudio.Location = new System.Drawing.Point(540, 214);
            this.PBoxAudio.Name = "PBoxAudio";
            this.PBoxAudio.Size = new System.Drawing.Size(95, 43);
            this.PBoxAudio.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PBoxAudio.TabIndex = 76;
            this.PBoxAudio.TabStop = false;
            this.PBoxAudio.Visible = false;
            // 
            // EMaskTel1
            // 
            this.EMaskTel1.BackColor = System.Drawing.Color.White;
            this.EMaskTel1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EMaskTel1.ForeColor = System.Drawing.Color.SlateBlue;
            this.EMaskTel1.Location = new System.Drawing.Point(105, 138);
            this.EMaskTel1.Mask = "###############";
            this.EMaskTel1.Name = "EMaskTel1";
            this.EMaskTel1.Size = new System.Drawing.Size(118, 21);
            this.EMaskTel1.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 281);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 13);
            this.label4.TabIndex = 75;
            this.label4.Text = "Complément";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(325, 227);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 13);
            this.label3.TabIndex = 74;
            this.label3.Text = "N°";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(167, 227);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 73;
            this.label2.Text = "Nom ";
            // 
            // Lroute1
            // 
            this.Lroute1.AutoSize = true;
            this.Lroute1.Location = new System.Drawing.Point(13, 228);
            this.Lroute1.Name = "Lroute1";
            this.Lroute1.Size = new System.Drawing.Size(79, 13);
            this.Lroute1.TabIndex = 72;
            this.Lroute1.Text = "Route, rue...";
            // 
            // CBRoute1
            // 
            this.CBRoute1.BackColor = System.Drawing.Color.White;
            this.CBRoute1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CBRoute1.ForeColor = System.Drawing.Color.SlateBlue;
            this.CBRoute1.FormattingEnabled = true;
            this.CBRoute1.Items.AddRange(new object[] {
            "",
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
            this.CBRoute1.Location = new System.Drawing.Point(3, 246);
            this.CBRoute1.Name = "CBRoute1";
            this.CBRoute1.Size = new System.Drawing.Size(96, 23);
            this.CBRoute1.TabIndex = 9;
            // 
            // groupBox9
            // 
            this.groupBox9.BackColor = System.Drawing.Color.CadetBlue;
            this.groupBox9.Controls.Add(this.label20);
            this.groupBox9.Controls.Add(this.label15);
            this.groupBox9.Controls.Add(this.CBRoute_adm1);
            this.groupBox9.Controls.Add(this.txtPatient_AdmAdresse2);
            this.groupBox9.Controls.Add(this.label5);
            this.groupBox9.Controls.Add(this.txtPatient_AdmBatiment);
            this.groupBox9.Controls.Add(this.label65);
            this.groupBox9.Controls.Add(this.txtPatient_AdmNPA);
            this.groupBox9.Controls.Add(this.label64);
            this.groupBox9.Controls.Add(this.txtPatient_AdmNumRue);
            this.groupBox9.Controls.Add(this.txtPatient_AdmLocalite);
            this.groupBox9.Controls.Add(this.label66);
            this.groupBox9.Controls.Add(this.txtPatient_AdmAdresse1);
            this.groupBox9.Controls.Add(this.label67);
            this.groupBox9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox9.Location = new System.Drawing.Point(425, 9);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(389, 156);
            this.groupBox9.TabIndex = 69;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Adresse de facturation";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(29, 62);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(75, 13);
            this.label20.TabIndex = 93;
            this.label20.Text = "Complément";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(25, 13);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(79, 13);
            this.label15.TabIndex = 92;
            this.label15.Text = "Route, rue...";
            // 
            // CBRoute_adm1
            // 
            this.CBRoute_adm1.BackColor = System.Drawing.Color.White;
            this.CBRoute_adm1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CBRoute_adm1.ForeColor = System.Drawing.Color.SlateBlue;
            this.CBRoute_adm1.FormattingEnabled = true;
            this.CBRoute_adm1.Items.AddRange(new object[] {
            "",
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
            this.CBRoute_adm1.Location = new System.Drawing.Point(11, 29);
            this.CBRoute_adm1.Name = "CBRoute_adm1";
            this.CBRoute_adm1.Size = new System.Drawing.Size(96, 23);
            this.CBRoute_adm1.TabIndex = 21;
            // 
            // txtPatient_AdmAdresse2
            // 
            this.txtPatient_AdmAdresse2.BackColor = System.Drawing.Color.White;
            this.txtPatient_AdmAdresse2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatient_AdmAdresse2.ForeColor = System.Drawing.Color.SlateBlue;
            this.txtPatient_AdmAdresse2.Location = new System.Drawing.Point(115, 56);
            this.txtPatient_AdmAdresse2.Name = "txtPatient_AdmAdresse2";
            this.txtPatient_AdmAdresse2.Size = new System.Drawing.Size(270, 21);
            this.txtPatient_AdmAdresse2.TabIndex = 24;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(343, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 13);
            this.label5.TabIndex = 89;
            this.label5.Text = "N°";
            // 
            // txtPatient_AdmBatiment
            // 
            this.txtPatient_AdmBatiment.BackColor = System.Drawing.Color.White;
            this.txtPatient_AdmBatiment.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatient_AdmBatiment.ForeColor = System.Drawing.Color.SlateBlue;
            this.txtPatient_AdmBatiment.Location = new System.Drawing.Point(47, 129);
            this.txtPatient_AdmBatiment.Name = "txtPatient_AdmBatiment";
            this.txtPatient_AdmBatiment.Size = new System.Drawing.Size(60, 21);
            this.txtPatient_AdmBatiment.TabIndex = 27;
            this.txtPatient_AdmBatiment.Enter += new System.EventHandler(this.txtPatient_Enter);
            this.txtPatient_AdmBatiment.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPatient_KeyDown);
            // 
            // label65
            // 
            this.label65.Location = new System.Drawing.Point(8, 129);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(57, 19);
            this.label65.TabIndex = 88;
            this.label65.Text = "Bat :";
            // 
            // txtPatient_AdmNPA
            // 
            this.txtPatient_AdmNPA.BackColor = System.Drawing.Color.White;
            this.txtPatient_AdmNPA.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatient_AdmNPA.ForeColor = System.Drawing.Color.SlateBlue;
            this.txtPatient_AdmNPA.Location = new System.Drawing.Point(11, 101);
            this.txtPatient_AdmNPA.Name = "txtPatient_AdmNPA";
            this.txtPatient_AdmNPA.Size = new System.Drawing.Size(96, 21);
            this.txtPatient_AdmNPA.TabIndex = 25;
            this.txtPatient_AdmNPA.Enter += new System.EventHandler(this.txtPatient_Enter);
            this.txtPatient_AdmNPA.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPatient_KeyDown);
            // 
            // label64
            // 
            this.label64.Location = new System.Drawing.Point(40, 85);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(58, 19);
            this.label64.TabIndex = 86;
            this.label64.Text = "NPA :";
            // 
            // txtPatient_AdmNumRue
            // 
            this.txtPatient_AdmNumRue.BackColor = System.Drawing.Color.White;
            this.txtPatient_AdmNumRue.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatient_AdmNumRue.ForeColor = System.Drawing.Color.SlateBlue;
            this.txtPatient_AdmNumRue.Location = new System.Drawing.Point(323, 29);
            this.txtPatient_AdmNumRue.Name = "txtPatient_AdmNumRue";
            this.txtPatient_AdmNumRue.Size = new System.Drawing.Size(60, 21);
            this.txtPatient_AdmNumRue.TabIndex = 23;
            this.txtPatient_AdmNumRue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPatient_AdmNumRue.Enter += new System.EventHandler(this.txtPatient_Enter);
            this.txtPatient_AdmNumRue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPatient_KeyDown);
            // 
            // txtPatient_AdmLocalite
            // 
            this.txtPatient_AdmLocalite.BackColor = System.Drawing.Color.White;
            this.txtPatient_AdmLocalite.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatient_AdmLocalite.ForeColor = System.Drawing.Color.SlateBlue;
            this.txtPatient_AdmLocalite.Location = new System.Drawing.Point(113, 101);
            this.txtPatient_AdmLocalite.Name = "txtPatient_AdmLocalite";
            this.txtPatient_AdmLocalite.Size = new System.Drawing.Size(270, 21);
            this.txtPatient_AdmLocalite.TabIndex = 26;
            this.txtPatient_AdmLocalite.Enter += new System.EventHandler(this.txtPatient_Enter);
            this.txtPatient_AdmLocalite.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPatient_KeyDown);
            // 
            // label66
            // 
            this.label66.Location = new System.Drawing.Point(213, 85);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(66, 19);
            this.label66.TabIndex = 83;
            this.label66.Text = "Localité :";
            // 
            // txtPatient_AdmAdresse1
            // 
            this.txtPatient_AdmAdresse1.BackColor = System.Drawing.Color.White;
            this.txtPatient_AdmAdresse1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatient_AdmAdresse1.ForeColor = System.Drawing.Color.SlateBlue;
            this.txtPatient_AdmAdresse1.Location = new System.Drawing.Point(113, 29);
            this.txtPatient_AdmAdresse1.Name = "txtPatient_AdmAdresse1";
            this.txtPatient_AdmAdresse1.Size = new System.Drawing.Size(204, 21);
            this.txtPatient_AdmAdresse1.TabIndex = 22;
            this.txtPatient_AdmAdresse1.Enter += new System.EventHandler(this.txtPatient_Enter);
            this.txtPatient_AdmAdresse1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPatient_KeyDown);
            // 
            // label67
            // 
            this.label67.Location = new System.Drawing.Point(213, 13);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(47, 19);
            this.label67.TabIndex = 81;
            this.label67.Text = "Nom :";
            // 
            // LstAide
            // 
            this.LstAide.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LstAide.Location = new System.Drawing.Point(283, 65);
            this.LstAide.Name = "LstAide";
            this.LstAide.Size = new System.Drawing.Size(128, 67);
            this.LstAide.TabIndex = 66;
            this.LstAide.KeyUp += new System.Windows.Forms.KeyEventHandler(this.LstAide_KeyUp);
            this.LstAide.MouseUp += new System.Windows.Forms.MouseEventHandler(this.LstAide_MouseUp);
            // 
            // label39
            // 
            this.label39.Location = new System.Drawing.Point(4, 105);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(68, 29);
            this.label39.TabIndex = 66;
            this.label39.Text = "Date de naissance :";
            // 
            // txtDateNaissance
            // 
            this.txtDateNaissance.BackColor = System.Drawing.Color.White;
            this.txtDateNaissance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDateNaissance.ForeColor = System.Drawing.Color.SlateBlue;
            this.txtDateNaissance.Location = new System.Drawing.Point(105, 106);
            this.txtDateNaissance.Name = "txtDateNaissance";
            this.txtDateNaissance.Size = new System.Drawing.Size(118, 21);
            this.txtDateNaissance.TabIndex = 6;
            this.txtDateNaissance.Enter += new System.EventHandler(this.txtPatient_Enter);
            this.txtDateNaissance.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPatient_KeyDown);
            this.txtDateNaissance.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPatient_KeyPress);
            // 
            // TxtNPA
            // 
            this.TxtNPA.BackColor = System.Drawing.Color.White;
            this.TxtNPA.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtNPA.ForeColor = System.Drawing.Color.SlateBlue;
            this.TxtNPA.Location = new System.Drawing.Point(3, 330);
            this.TxtNPA.Name = "TxtNPA";
            this.TxtNPA.Size = new System.Drawing.Size(94, 21);
            this.TxtNPA.TabIndex = 13;
            this.TxtNPA.TextChanged += new System.EventHandler(this.txtBilan_TextChanged);
            this.TxtNPA.Enter += new System.EventHandler(this.txtPatient_Enter);
            this.TxtNPA.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPatient_KeyDown);
            // 
            // label38
            // 
            this.label38.Location = new System.Drawing.Point(24, 310);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(39, 19);
            this.label38.TabIndex = 33;
            this.label38.Text = "NPA :";
            // 
            // txtPatient_Adresse2
            // 
            this.txtPatient_Adresse2.BackColor = System.Drawing.Color.White;
            this.txtPatient_Adresse2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatient_Adresse2.ForeColor = System.Drawing.Color.SlateBlue;
            this.txtPatient_Adresse2.Location = new System.Drawing.Point(105, 277);
            this.txtPatient_Adresse2.Multiline = true;
            this.txtPatient_Adresse2.Name = "txtPatient_Adresse2";
            this.txtPatient_Adresse2.Size = new System.Drawing.Size(260, 21);
            this.txtPatient_Adresse2.TabIndex = 12;
            this.txtPatient_Adresse2.TextChanged += new System.EventHandler(this.txtBilan_TextChanged);
            this.txtPatient_Adresse2.Enter += new System.EventHandler(this.txtPatient_Enter);
            this.txtPatient_Adresse2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPatient_KeyDown);
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(417, 180);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(113, 19);
            this.label18.TabIndex = 30;
            this.label18.Text = "Longitude/Latitude";
            // 
            // txtPatient_Longitude
            // 
            this.txtPatient_Longitude.BackColor = System.Drawing.Color.White;
            this.txtPatient_Longitude.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatient_Longitude.ForeColor = System.Drawing.Color.SlateBlue;
            this.txtPatient_Longitude.Location = new System.Drawing.Point(540, 177);
            this.txtPatient_Longitude.Name = "txtPatient_Longitude";
            this.txtPatient_Longitude.Size = new System.Drawing.Size(95, 21);
            this.txtPatient_Longitude.TabIndex = 19;
            this.txtPatient_Longitude.TabStop = false;
            this.txtPatient_Longitude.TextChanged += new System.EventHandler(this.txtBilan_TextChanged);
            // 
            // txtPatient_Latitude
            // 
            this.txtPatient_Latitude.BackColor = System.Drawing.Color.White;
            this.txtPatient_Latitude.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatient_Latitude.ForeColor = System.Drawing.Color.SlateBlue;
            this.txtPatient_Latitude.Location = new System.Drawing.Point(638, 177);
            this.txtPatient_Latitude.Name = "txtPatient_Latitude";
            this.txtPatient_Latitude.Size = new System.Drawing.Size(93, 21);
            this.txtPatient_Latitude.TabIndex = 20;
            this.txtPatient_Latitude.TabStop = false;
            this.txtPatient_Latitude.TextChanged += new System.EventHandler(this.txtBilan_TextChanged);
            // 
            // txtPatient_Porte
            // 
            this.txtPatient_Porte.BackColor = System.Drawing.Color.White;
            this.txtPatient_Porte.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatient_Porte.ForeColor = System.Drawing.Color.SlateBlue;
            this.txtPatient_Porte.Location = new System.Drawing.Point(460, 258);
            this.txtPatient_Porte.Name = "txtPatient_Porte";
            this.txtPatient_Porte.Size = new System.Drawing.Size(62, 21);
            this.txtPatient_Porte.TabIndex = 17;
            this.txtPatient_Porte.TabStop = false;
            this.txtPatient_Porte.TextChanged += new System.EventHandler(this.txtBilan_TextChanged);
            this.txtPatient_Porte.Enter += new System.EventHandler(this.txtPatient_Enter);
            this.txtPatient_Porte.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPatient_KeyDown);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(422, 260);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(40, 19);
            this.label10.TabIndex = 26;
            this.label10.Text = "Porte :";
            // 
            // txtPatient_Internom
            // 
            this.txtPatient_Internom.BackColor = System.Drawing.Color.White;
            this.txtPatient_Internom.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatient_Internom.ForeColor = System.Drawing.Color.SlateBlue;
            this.txtPatient_Internom.Location = new System.Drawing.Point(460, 236);
            this.txtPatient_Internom.Name = "txtPatient_Internom";
            this.txtPatient_Internom.Size = new System.Drawing.Size(62, 21);
            this.txtPatient_Internom.TabIndex = 16;
            this.txtPatient_Internom.TabStop = false;
            this.txtPatient_Internom.TextChanged += new System.EventHandler(this.txtBilan_TextChanged);
            this.txtPatient_Internom.Enter += new System.EventHandler(this.txtPatient_Enter);
            this.txtPatient_Internom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPatient_KeyDown);
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(422, 238);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(31, 19);
            this.label16.TabIndex = 24;
            this.label16.Text = "Inter :";
            // 
            // txtPatient_Digicode
            // 
            this.txtPatient_Digicode.BackColor = System.Drawing.Color.White;
            this.txtPatient_Digicode.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatient_Digicode.ForeColor = System.Drawing.Color.SlateBlue;
            this.txtPatient_Digicode.Location = new System.Drawing.Point(460, 214);
            this.txtPatient_Digicode.Name = "txtPatient_Digicode";
            this.txtPatient_Digicode.Size = new System.Drawing.Size(62, 21);
            this.txtPatient_Digicode.TabIndex = 15;
            this.txtPatient_Digicode.TabStop = false;
            this.txtPatient_Digicode.TextChanged += new System.EventHandler(this.txtBilan_TextChanged);
            this.txtPatient_Digicode.Enter += new System.EventHandler(this.txtPatient_Enter);
            this.txtPatient_Digicode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPatient_KeyDown);
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(422, 216);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(30, 19);
            this.label17.TabIndex = 22;
            this.label17.Text = "Digi :";
            // 
            // txtPatient_Etage
            // 
            this.txtPatient_Etage.BackColor = System.Drawing.Color.White;
            this.txtPatient_Etage.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatient_Etage.ForeColor = System.Drawing.Color.SlateBlue;
            this.txtPatient_Etage.Location = new System.Drawing.Point(470, 286);
            this.txtPatient_Etage.Name = "txtPatient_Etage";
            this.txtPatient_Etage.Size = new System.Drawing.Size(52, 21);
            this.txtPatient_Etage.TabIndex = 18;
            this.txtPatient_Etage.TabStop = false;
            this.txtPatient_Etage.TextChanged += new System.EventHandler(this.txtBilan_TextChanged);
            this.txtPatient_Etage.Enter += new System.EventHandler(this.txtPatient_Enter);
            this.txtPatient_Etage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPatient_KeyDown);
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(422, 288);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(46, 19);
            this.label13.TabIndex = 20;
            this.label13.Text = "Etage :";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(32, 142);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(57, 19);
            this.label9.TabIndex = 14;
            this.label9.Text = "Tel :";
            // 
            // txtPatient_NumRue
            // 
            this.txtPatient_NumRue.BackColor = System.Drawing.Color.White;
            this.txtPatient_NumRue.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatient_NumRue.ForeColor = System.Drawing.Color.SlateBlue;
            this.txtPatient_NumRue.Location = new System.Drawing.Point(305, 246);
            this.txtPatient_NumRue.Name = "txtPatient_NumRue";
            this.txtPatient_NumRue.Size = new System.Drawing.Size(60, 21);
            this.txtPatient_NumRue.TabIndex = 11;
            this.txtPatient_NumRue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPatient_NumRue.TextChanged += new System.EventHandler(this.txtBilan_TextChanged);
            this.txtPatient_NumRue.Enter += new System.EventHandler(this.txtPatient_Enter);
            this.txtPatient_NumRue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPatient_KeyDown);
            // 
            // txtPatient_Localite
            // 
            this.txtPatient_Localite.BackColor = System.Drawing.Color.White;
            this.txtPatient_Localite.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatient_Localite.ForeColor = System.Drawing.Color.SlateBlue;
            this.txtPatient_Localite.Location = new System.Drawing.Point(105, 330);
            this.txtPatient_Localite.Name = "txtPatient_Localite";
            this.txtPatient_Localite.Size = new System.Drawing.Size(260, 21);
            this.txtPatient_Localite.TabIndex = 14;
            this.txtPatient_Localite.TextChanged += new System.EventHandler(this.txtBilan_TextChanged);
            this.txtPatient_Localite.Enter += new System.EventHandler(this.txtPatient_Enter);
            this.txtPatient_Localite.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPatient_KeyDown);
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(167, 311);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(66, 19);
            this.label11.TabIndex = 11;
            this.label11.Text = "Localité :";
            // 
            // txtPatient_Adresse1
            // 
            this.txtPatient_Adresse1.BackColor = System.Drawing.Color.White;
            this.txtPatient_Adresse1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatient_Adresse1.ForeColor = System.Drawing.Color.SlateBlue;
            this.txtPatient_Adresse1.Location = new System.Drawing.Point(105, 246);
            this.txtPatient_Adresse1.Name = "txtPatient_Adresse1";
            this.txtPatient_Adresse1.Size = new System.Drawing.Size(194, 21);
            this.txtPatient_Adresse1.TabIndex = 10;
            this.txtPatient_Adresse1.TextChanged += new System.EventHandler(this.txtBilan_TextChanged);
            this.txtPatient_Adresse1.Enter += new System.EventHandler(this.txtPatient_Enter);
            this.txtPatient_Adresse1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPatient_KeyDown);
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label12.Location = new System.Drawing.Point(129, 208);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(143, 19);
            this.label12.TabIndex = 9;
            this.label12.Text = "Adresse d\'intervention :";
            // 
            // txtPatient_Sexe
            // 
            this.txtPatient_Sexe.BackColor = System.Drawing.Color.White;
            this.txtPatient_Sexe.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatient_Sexe.ForeColor = System.Drawing.Color.SlateBlue;
            this.txtPatient_Sexe.Location = new System.Drawing.Point(67, 75);
            this.txtPatient_Sexe.Name = "txtPatient_Sexe";
            this.txtPatient_Sexe.Size = new System.Drawing.Size(52, 21);
            this.txtPatient_Sexe.TabIndex = 5;
            this.txtPatient_Sexe.TextChanged += new System.EventHandler(this.txtBilan_TextChanged);
            this.txtPatient_Sexe.Enter += new System.EventHandler(this.txtPatient_Enter);
            this.txtPatient_Sexe.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPatient_KeyDown);
            // 
            // txtPatient_UniteAge
            // 
            this.txtPatient_UniteAge.BackColor = System.Drawing.Color.White;
            this.txtPatient_UniteAge.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatient_UniteAge.ForeColor = System.Drawing.Color.SlateBlue;
            this.txtPatient_UniteAge.Location = new System.Drawing.Point(122, 53);
            this.txtPatient_UniteAge.Name = "txtPatient_UniteAge";
            this.txtPatient_UniteAge.Size = new System.Drawing.Size(50, 21);
            this.txtPatient_UniteAge.TabIndex = 4;
            this.txtPatient_UniteAge.TextChanged += new System.EventHandler(this.txtBilan_TextChanged);
            this.txtPatient_UniteAge.Enter += new System.EventHandler(this.txtPatient_Enter);
            this.txtPatient_UniteAge.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPatient_KeyDown);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(8, 77);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 19);
            this.label8.TabIndex = 6;
            this.label8.Text = "Sexe :";
            // 
            // txtPatient_Age
            // 
            this.txtPatient_Age.BackColor = System.Drawing.Color.White;
            this.txtPatient_Age.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatient_Age.ForeColor = System.Drawing.Color.SlateBlue;
            this.txtPatient_Age.Location = new System.Drawing.Point(67, 53);
            this.txtPatient_Age.Name = "txtPatient_Age";
            this.txtPatient_Age.Size = new System.Drawing.Size(52, 21);
            this.txtPatient_Age.TabIndex = 3;
            this.txtPatient_Age.TextChanged += new System.EventHandler(this.txtBilan_TextChanged);
            this.txtPatient_Age.Enter += new System.EventHandler(this.txtPatient_Enter);
            this.txtPatient_Age.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPatient_KeyDown);
            this.txtPatient_Age.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPatient_KeyPress);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(8, 55);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 19);
            this.label7.TabIndex = 4;
            this.label7.Text = "Age :";
            // 
            // txtPatient_Prenom
            // 
            this.txtPatient_Prenom.BackColor = System.Drawing.Color.White;
            this.txtPatient_Prenom.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatient_Prenom.ForeColor = System.Drawing.Color.SlateBlue;
            this.txtPatient_Prenom.Location = new System.Drawing.Point(67, 31);
            this.txtPatient_Prenom.Name = "txtPatient_Prenom";
            this.txtPatient_Prenom.Size = new System.Drawing.Size(230, 21);
            this.txtPatient_Prenom.TabIndex = 2;
            this.txtPatient_Prenom.TextChanged += new System.EventHandler(this.txtBilan_TextChanged);
            this.txtPatient_Prenom.Enter += new System.EventHandler(this.txtPatient_Enter);
            this.txtPatient_Prenom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPatient_KeyDown);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "Prénom :";
            // 
            // txtPatient_Nom
            // 
            this.txtPatient_Nom.BackColor = System.Drawing.Color.White;
            this.txtPatient_Nom.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPatient_Nom.ForeColor = System.Drawing.Color.SlateBlue;
            this.txtPatient_Nom.Location = new System.Drawing.Point(67, 9);
            this.txtPatient_Nom.Name = "txtPatient_Nom";
            this.txtPatient_Nom.Size = new System.Drawing.Size(230, 21);
            this.txtPatient_Nom.TabIndex = 1;
            this.txtPatient_Nom.TextChanged += new System.EventHandler(this.txtBilan_TextChanged);
            this.txtPatient_Nom.Enter += new System.EventHandler(this.txtPatient_Enter);
            this.txtPatient_Nom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPatient_KeyDown);
            // 
            // lblPatient_Nom
            // 
            this.lblPatient_Nom.BackColor = System.Drawing.Color.Transparent;
            this.lblPatient_Nom.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblPatient_Nom.Location = new System.Drawing.Point(8, 11);
            this.lblPatient_Nom.Name = "lblPatient_Nom";
            this.lblPatient_Nom.Size = new System.Drawing.Size(46, 19);
            this.lblPatient_Nom.TabIndex = 0;
            this.lblPatient_Nom.Text = "Nom :";
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.CadetBlue;
            this.panel10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel10.Controls.Add(this.LblStatusSauvegardeFiche);
            this.panel10.Controls.Add(this.txtFiche_CommentairSauvegarde);
            this.panel10.Controls.Add(this.label53);
            this.panel10.Controls.Add(this.label54);
            this.panel10.Controls.Add(this.picSaveFiche);
            this.panel10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel10.Location = new System.Drawing.Point(845, 3);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(268, 134);
            this.panel10.TabIndex = 85;
            // 
            // LblStatusSauvegardeFiche
            // 
            this.LblStatusSauvegardeFiche.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblStatusSauvegardeFiche.ForeColor = System.Drawing.Color.Red;
            this.LblStatusSauvegardeFiche.Location = new System.Drawing.Point(6, 69);
            this.LblStatusSauvegardeFiche.Name = "LblStatusSauvegardeFiche";
            this.LblStatusSauvegardeFiche.Size = new System.Drawing.Size(229, 27);
            this.LblStatusSauvegardeFiche.TabIndex = 71;
            // 
            // txtFiche_CommentairSauvegarde
            // 
            this.txtFiche_CommentairSauvegarde.Location = new System.Drawing.Point(4, 23);
            this.txtFiche_CommentairSauvegarde.Multiline = true;
            this.txtFiche_CommentairSauvegarde.Name = "txtFiche_CommentairSauvegarde";
            this.txtFiche_CommentairSauvegarde.Size = new System.Drawing.Size(257, 44);
            this.txtFiche_CommentairSauvegarde.TabIndex = 70;
            // 
            // label53
            // 
            this.label53.BackColor = System.Drawing.Color.Transparent;
            this.label53.Location = new System.Drawing.Point(6, 6);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(265, 17);
            this.label53.TabIndex = 69;
            this.label53.Text = "Commentaire de sauvegarde :";
            // 
            // label54
            // 
            this.label54.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label54.Location = new System.Drawing.Point(5, 99);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(212, 24);
            this.label54.TabIndex = 68;
            this.label54.Text = "Sauvegarde de fiche (F1) :";
            // 
            // picSaveFiche
            // 
            this.picSaveFiche.Image = ((System.Drawing.Image)(resources.GetObject("picSaveFiche.Image")));
            this.picSaveFiche.Location = new System.Drawing.Point(223, 92);
            this.picSaveFiche.Name = "picSaveFiche";
            this.picSaveFiche.Size = new System.Drawing.Size(35, 33);
            this.picSaveFiche.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picSaveFiche.TabIndex = 66;
            this.picSaveFiche.TabStop = false;
            this.toolTip1.SetToolTip(this.picSaveFiche, "Sauvegarde");
            this.picSaveFiche.Click += new System.EventHandler(this.picSaveFiche_Click);
            // 
            // tbRapport
            // 
            this.tbRapport.Controls.Add(this.crystalReportViewer1);
            this.tbRapport.Controls.Add(this.bCarteAvs);
            this.tbRapport.Controls.Add(this.panel4);
            this.tbRapport.Controls.Add(this.btnRapportCourant);
            this.tbRapport.Controls.Add(this.label68);
            this.tbRapport.Controls.Add(this.label41);
            this.tbRapport.Controls.Add(this.panel9);
            this.tbRapport.Controls.Add(this.TabActionRapport);
            this.tbRapport.Controls.Add(this.groupBox8);
            this.tbRapport.Controls.Add(this.panel8);
            this.tbRapport.Controls.Add(this.lblTest);
            this.tbRapport.Controls.Add(this.label43);
            this.tbRapport.Controls.Add(this.fpRapport);
            this.tbRapport.Controls.Add(this.richTextBox1);
            this.tbRapport.Controls.Add(this.btnRapport_Font);
            this.tbRapport.Controls.Add(this.btnRapport_Couleur);
            this.tbRapport.Controls.Add(this.txtSaisieRapport);
            this.tbRapport.Controls.Add(this.cmdRapport_Bonjour);
            this.tbRapport.Controls.Add(this.cmdRapport_Corps);
            this.tbRapport.Controls.Add(this.picRapport_Actualiser);
            this.tbRapport.Controls.Add(this.cmdRapport_Signature);
            this.tbRapport.Controls.Add(this.cmdRapport_Salutations);
            this.tbRapport.Controls.Add(this.cmdRapport_Intro);
            this.tbRapport.Controls.Add(this.cmdRapport_EnTete);
            this.tbRapport.Controls.Add(this.cmdRapport_Concerne);
            this.tbRapport.Controls.Add(this.cmdRapport_Destinataire);
            this.tbRapport.Location = new System.Drawing.Point(4, 22);
            this.tbRapport.Name = "tbRapport";
            this.tbRapport.Size = new System.Drawing.Size(1148, 864);
            this.tbRapport.TabIndex = 2;
            this.tbRapport.Text = "Rapport";
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = -1;
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer1.Location = new System.Drawing.Point(3, 0);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.Size = new System.Drawing.Size(651, 625);
            this.crystalReportViewer1.TabIndex = 99;
            // 
            // bCarteAvs
            // 
            this.bCarteAvs.BackgroundImage = global::ImportSosGeneve.Properties.Resources.bCarteAvs;
            this.bCarteAvs.Location = new System.Drawing.Point(788, 754);
            this.bCarteAvs.Name = "bCarteAvs";
            this.bCarteAvs.Size = new System.Drawing.Size(94, 64);
            this.bCarteAvs.TabIndex = 98;
            this.toolTip1.SetToolTip(this.bCarteAvs, "Affichage de la cate AVS");
            this.bCarteAvs.UseVisualStyleBackColor = true;
            this.bCarteAvs.Click += new System.EventHandler(this.bCarteAvs_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.Lpasdictee);
            this.panel4.Controls.Add(this.vlcControl1);
            this.panel4.Controls.Add(this.Bstop);
            this.panel4.Controls.Add(this.Bpause);
            this.panel4.Controls.Add(this.Bplay);
            this.panel4.Controls.Add(this.LAvancement);
            this.panel4.Controls.Add(this.LDuree);
            this.panel4.Controls.Add(this.tBarTps);
            this.panel4.Controls.Add(this.tBarVol);
            this.panel4.Location = new System.Drawing.Point(664, 635);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(454, 108);
            this.panel4.TabIndex = 97;
            // 
            // Lpasdictee
            // 
            this.Lpasdictee.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lpasdictee.ForeColor = System.Drawing.Color.Red;
            this.Lpasdictee.Location = new System.Drawing.Point(233, 66);
            this.Lpasdictee.Name = "Lpasdictee";
            this.Lpasdictee.Size = new System.Drawing.Size(87, 13);
            this.Lpasdictee.TabIndex = 8;
            this.Lpasdictee.Text = "Pas de dictée";
            // 
            // vlcControl1
            // 
            this.vlcControl1.BackColor = System.Drawing.Color.Black;
            this.vlcControl1.Location = new System.Drawing.Point(338, 75);
            this.vlcControl1.Name = "vlcControl1";
            this.vlcControl1.Size = new System.Drawing.Size(47, 23);
            this.vlcControl1.Spu = -1;
            this.vlcControl1.TabIndex = 7;
            this.vlcControl1.Text = "vlcControl1";
            this.vlcControl1.VlcLibDirectory = null;
            this.vlcControl1.VlcMediaplayerOptions = null;
            this.vlcControl1.VlcLibDirectoryNeeded += new System.EventHandler<Vlc.DotNet.Forms.VlcLibDirectoryNeededEventArgs>(this.vlcControl1_VlcLibDirectoryNeeded);
            this.vlcControl1.EndReached += new System.EventHandler<Vlc.DotNet.Core.VlcMediaPlayerEndReachedEventArgs>(this.vlcControl1_EndReached);
            this.vlcControl1.PositionChanged += new System.EventHandler<Vlc.DotNet.Core.VlcMediaPlayerPositionChangedEventArgs>(this.vlcControl1_PositionChanged);
            // 
            // Bstop
            // 
            this.Bstop.FlatAppearance.BorderSize = 0;
            this.Bstop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Bstop.ImageIndex = 4;
            this.Bstop.ImageList = this.imageList1;
            this.Bstop.Location = new System.Drawing.Point(143, 46);
            this.Bstop.Name = "Bstop";
            this.Bstop.Size = new System.Drawing.Size(49, 52);
            this.Bstop.TabIndex = 6;
            this.Bstop.UseVisualStyleBackColor = true;
            this.Bstop.Click += new System.EventHandler(this.Bstop_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "play.png");
            this.imageList1.Images.SetKeyName(1, "play_on.png");
            this.imageList1.Images.SetKeyName(2, "pause.png");
            this.imageList1.Images.SetKeyName(3, "pause_on.png");
            this.imageList1.Images.SetKeyName(4, "stop2.png");
            this.imageList1.Images.SetKeyName(5, "stop2_on.png");
            // 
            // Bpause
            // 
            this.Bpause.FlatAppearance.BorderSize = 0;
            this.Bpause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Bpause.ImageIndex = 2;
            this.Bpause.ImageList = this.imageList1;
            this.Bpause.Location = new System.Drawing.Point(90, 46);
            this.Bpause.Name = "Bpause";
            this.Bpause.Size = new System.Drawing.Size(49, 52);
            this.Bpause.TabIndex = 5;
            this.Bpause.UseVisualStyleBackColor = true;
            this.Bpause.Click += new System.EventHandler(this.Bpause_Click);
            // 
            // Bplay
            // 
            this.Bplay.FlatAppearance.BorderSize = 0;
            this.Bplay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Bplay.ImageIndex = 0;
            this.Bplay.ImageList = this.imageList1;
            this.Bplay.Location = new System.Drawing.Point(41, 46);
            this.Bplay.Name = "Bplay";
            this.Bplay.Size = new System.Drawing.Size(49, 52);
            this.Bplay.TabIndex = 4;
            this.Bplay.UseVisualStyleBackColor = true;
            this.Bplay.Click += new System.EventHandler(this.Bplay_Click);
            // 
            // LAvancement
            // 
            this.LAvancement.BackColor = System.Drawing.Color.Black;
            this.LAvancement.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LAvancement.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LAvancement.ForeColor = System.Drawing.Color.White;
            this.LAvancement.Location = new System.Drawing.Point(324, 17);
            this.LAvancement.Name = "LAvancement";
            this.LAvancement.Size = new System.Drawing.Size(35, 17);
            this.LAvancement.TabIndex = 3;
            this.LAvancement.Text = "0";
            this.LAvancement.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LDuree
            // 
            this.LDuree.BackColor = System.Drawing.Color.Black;
            this.LDuree.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LDuree.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LDuree.ForeColor = System.Drawing.Color.White;
            this.LDuree.Location = new System.Drawing.Point(365, 17);
            this.LDuree.Name = "LDuree";
            this.LDuree.Size = new System.Drawing.Size(35, 17);
            this.LDuree.TabIndex = 2;
            this.LDuree.Text = "0";
            this.LDuree.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tBarTps
            // 
            this.tBarTps.Location = new System.Drawing.Point(8, 14);
            this.tBarTps.Maximum = 300;
            this.tBarTps.Name = "tBarTps";
            this.tBarTps.Size = new System.Drawing.Size(287, 45);
            this.tBarTps.TabIndex = 1;
            this.tBarTps.TickFrequency = 20;
            this.tBarTps.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.tBarTps.Scroll += new System.EventHandler(this.tBarTps_Scroll);
            // 
            // tBarVol
            // 
            this.tBarVol.Location = new System.Drawing.Point(406, 3);
            this.tBarVol.Maximum = 150;
            this.tBarVol.Name = "tBarVol";
            this.tBarVol.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tBarVol.Size = new System.Drawing.Size(45, 102);
            this.tBarVol.SmallChange = 0;
            this.tBarVol.TabIndex = 0;
            this.tBarVol.TickFrequency = 20;
            this.tBarVol.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.tBarVol.Value = 50;
            this.tBarVol.Scroll += new System.EventHandler(this.tBarVol_Scroll);
            // 
            // btnRapportCourant
            // 
            this.btnRapportCourant.BackColor = System.Drawing.Color.PaleGreen;
            this.btnRapportCourant.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRapportCourant.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRapportCourant.Location = new System.Drawing.Point(740, 28);
            this.btnRapportCourant.Name = "btnRapportCourant";
            this.btnRapportCourant.Size = new System.Drawing.Size(272, 48);
            this.btnRapportCourant.TabIndex = 96;
            this.btnRapportCourant.Text = "Consulter le rapport";
            this.btnRapportCourant.UseVisualStyleBackColor = false;
            this.btnRapportCourant.Click += new System.EventHandler(this.btnRapportCourant_Click);
            // 
            // label68
            // 
            this.label68.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label68.Location = new System.Drawing.Point(807, 3);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(154, 22);
            this.label68.TabIndex = 95;
            this.label68.Text = "Consultation courante :";
            // 
            // label41
            // 
            this.label41.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label41.Location = new System.Drawing.Point(661, 79);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(280, 16);
            this.label41.TabIndex = 94;
            this.label41.Text = "Autres rapports :";
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.btnRapport_Onglet3);
            this.panel9.Controls.Add(this.btnRapport_Onglet2);
            this.panel9.Controls.Add(this.btnRapport_Onglet1);
            this.panel9.Location = new System.Drawing.Point(646, 301);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(296, 24);
            this.panel9.TabIndex = 91;
            // 
            // btnRapport_Onglet3
            // 
            this.btnRapport_Onglet3.BackColor = System.Drawing.Color.Transparent;
            this.btnRapport_Onglet3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRapport_Onglet3.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRapport_Onglet3.ForeColor = System.Drawing.Color.Black;
            this.btnRapport_Onglet3.Image = ((System.Drawing.Image)(resources.GetObject("btnRapport_Onglet3.Image")));
            this.btnRapport_Onglet3.Location = new System.Drawing.Point(121, 0);
            this.btnRapport_Onglet3.Name = "btnRapport_Onglet3";
            this.btnRapport_Onglet3.Size = new System.Drawing.Size(104, 32);
            this.btnRapport_Onglet3.TabIndex = 92;
            this.btnRapport_Onglet3.Text = "Communication";
            this.btnRapport_Onglet3.UseVisualStyleBackColor = false;
            this.btnRapport_Onglet3.Click += new System.EventHandler(this.OngletRapport_Click);
            // 
            // btnRapport_Onglet2
            // 
            this.btnRapport_Onglet2.BackColor = System.Drawing.Color.Transparent;
            this.btnRapport_Onglet2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRapport_Onglet2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRapport_Onglet2.ForeColor = System.Drawing.Color.Black;
            this.btnRapport_Onglet2.Image = ((System.Drawing.Image)(resources.GetObject("btnRapport_Onglet2.Image")));
            this.btnRapport_Onglet2.Location = new System.Drawing.Point(224, 0);
            this.btnRapport_Onglet2.Name = "btnRapport_Onglet2";
            this.btnRapport_Onglet2.Size = new System.Drawing.Size(72, 32);
            this.btnRapport_Onglet2.TabIndex = 91;
            this.btnRapport_Onglet2.Text = "Visa";
            this.btnRapport_Onglet2.UseVisualStyleBackColor = false;
            this.btnRapport_Onglet2.Click += new System.EventHandler(this.OngletRapport_Click);
            // 
            // btnRapport_Onglet1
            // 
            this.btnRapport_Onglet1.BackColor = System.Drawing.Color.Transparent;
            this.btnRapport_Onglet1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRapport_Onglet1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRapport_Onglet1.ForeColor = System.Drawing.Color.Black;
            this.btnRapport_Onglet1.Image = ((System.Drawing.Image)(resources.GetObject("btnRapport_Onglet1.Image")));
            this.btnRapport_Onglet1.Location = new System.Drawing.Point(16, 0);
            this.btnRapport_Onglet1.Name = "btnRapport_Onglet1";
            this.btnRapport_Onglet1.Size = new System.Drawing.Size(104, 32);
            this.btnRapport_Onglet1.TabIndex = 90;
            this.btnRapport_Onglet1.Text = "Création/Reprise";
            this.btnRapport_Onglet1.UseVisualStyleBackColor = false;
            this.btnRapport_Onglet1.Click += new System.EventHandler(this.OngletRapport_Click);
            // 
            // TabActionRapport
            // 
            this.TabActionRapport.Controls.Add(this.tbCreation);
            this.TabActionRapport.Controls.Add(this.tbVisa);
            this.TabActionRapport.Controls.Add(this.tbCommunication);
            this.TabActionRapport.Location = new System.Drawing.Point(662, 301);
            this.TabActionRapport.Name = "TabActionRapport";
            this.TabActionRapport.SelectedIndex = 0;
            this.TabActionRapport.Size = new System.Drawing.Size(280, 328);
            this.TabActionRapport.TabIndex = 89;
            // 
            // tbCreation
            // 
            this.tbCreation.Controls.Add(this.btModif);
            this.tbCreation.Controls.Add(this.btnCorriger);
            this.tbCreation.Controls.Add(this.btnSupprimerRapport);
            this.tbCreation.Controls.Add(this.rtfConvert);
            this.tbCreation.Controls.Add(this.label57);
            this.tbCreation.Controls.Add(this.label56);
            this.tbCreation.Controls.Add(this.label55);
            this.tbCreation.Controls.Add(this.picRapport_OptSans);
            this.tbCreation.Controls.Add(this.picRapport_OptConstat);
            this.tbCreation.Controls.Add(this.picRapport_OptRapport);
            this.tbCreation.Location = new System.Drawing.Point(4, 22);
            this.tbCreation.Name = "tbCreation";
            this.tbCreation.Size = new System.Drawing.Size(272, 302);
            this.tbCreation.TabIndex = 0;
            this.tbCreation.Text = "Création / Reprise";
            // 
            // btModif
            // 
            this.btModif.Location = new System.Drawing.Point(144, 184);
            this.btModif.Name = "btModif";
            this.btModif.Size = new System.Drawing.Size(120, 32);
            this.btModif.TabIndex = 8;
            this.btModif.Text = "Modifier Rapport";
            this.btModif.Click += new System.EventHandler(this.button9_Click);
            // 
            // btnCorriger
            // 
            this.btnCorriger.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCorriger.Location = new System.Drawing.Point(8, 232);
            this.btnCorriger.Name = "btnCorriger";
            this.btnCorriger.Size = new System.Drawing.Size(120, 56);
            this.btnCorriger.TabIndex = 7;
            this.btnCorriger.Text = "Demande correction du médecin chef";
            this.btnCorriger.UseVisualStyleBackColor = false;
            this.btnCorriger.Click += new System.EventHandler(this.btnCorriger_Click);
            // 
            // btnSupprimerRapport
            // 
            this.btnSupprimerRapport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSupprimerRapport.Location = new System.Drawing.Point(134, 232);
            this.btnSupprimerRapport.Name = "btnSupprimerRapport";
            this.btnSupprimerRapport.Size = new System.Drawing.Size(130, 56);
            this.btnSupprimerRapport.TabIndex = 7;
            this.btnSupprimerRapport.Text = "SUPPRIMER";
            this.btnSupprimerRapport.UseVisualStyleBackColor = false;
            this.btnSupprimerRapport.Click += new System.EventHandler(this.btnSupprimerRapport_Click);
            // 
            // rtfConvert
            // 
            this.rtfConvert.Location = new System.Drawing.Point(32, 168);
            this.rtfConvert.Name = "rtfConvert";
            this.rtfConvert.Size = new System.Drawing.Size(32, 24);
            this.rtfConvert.TabIndex = 6;
            this.rtfConvert.Text = "rtfConvert";
            this.rtfConvert.Visible = false;
            // 
            // label57
            // 
            this.label57.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label57.Location = new System.Drawing.Point(184, 120);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(80, 40);
            this.label57.TabIndex = 5;
            this.label57.Text = "Sans Rapport";
            this.label57.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label56
            // 
            this.label56.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label56.Location = new System.Drawing.Point(96, 120);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(80, 24);
            this.label56.TabIndex = 4;
            this.label56.Text = "Constat";
            this.label56.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label55
            // 
            this.label55.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label55.Location = new System.Drawing.Point(8, 120);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(80, 24);
            this.label55.TabIndex = 3;
            this.label55.Text = "Rapport";
            this.label55.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picRapport_OptSans
            // 
            this.picRapport_OptSans.BackColor = System.Drawing.Color.LightCyan;
            this.picRapport_OptSans.Image = ((System.Drawing.Image)(resources.GetObject("picRapport_OptSans.Image")));
            this.picRapport_OptSans.Location = new System.Drawing.Point(184, 24);
            this.picRapport_OptSans.Name = "picRapport_OptSans";
            this.picRapport_OptSans.Size = new System.Drawing.Size(80, 88);
            this.picRapport_OptSans.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picRapport_OptSans.TabIndex = 2;
            this.picRapport_OptSans.TabStop = false;
            this.picRapport_OptSans.Click += new System.EventHandler(this.picRapport_OptSans_Click);
            // 
            // picRapport_OptConstat
            // 
            this.picRapport_OptConstat.BackColor = System.Drawing.Color.LightCyan;
            this.picRapport_OptConstat.Image = ((System.Drawing.Image)(resources.GetObject("picRapport_OptConstat.Image")));
            this.picRapport_OptConstat.Location = new System.Drawing.Point(96, 24);
            this.picRapport_OptConstat.Name = "picRapport_OptConstat";
            this.picRapport_OptConstat.Size = new System.Drawing.Size(80, 88);
            this.picRapport_OptConstat.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picRapport_OptConstat.TabIndex = 1;
            this.picRapport_OptConstat.TabStop = false;
            this.picRapport_OptConstat.Click += new System.EventHandler(this.picRapport_OptConstat_Click);
            // 
            // picRapport_OptRapport
            // 
            this.picRapport_OptRapport.BackColor = System.Drawing.Color.LightCyan;
            this.picRapport_OptRapport.Image = ((System.Drawing.Image)(resources.GetObject("picRapport_OptRapport.Image")));
            this.picRapport_OptRapport.Location = new System.Drawing.Point(8, 24);
            this.picRapport_OptRapport.Name = "picRapport_OptRapport";
            this.picRapport_OptRapport.Size = new System.Drawing.Size(80, 88);
            this.picRapport_OptRapport.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picRapport_OptRapport.TabIndex = 0;
            this.picRapport_OptRapport.TabStop = false;
            this.picRapport_OptRapport.Click += new System.EventHandler(this.picRapport_OptRapport_Click);
            // 
            // tbVisa
            // 
            this.tbVisa.Controls.Add(this.btnEnlevCorrection);
            this.tbVisa.Controls.Add(this.label52);
            this.tbVisa.Controls.Add(this.TxtRapport_CommentaireVisa);
            this.tbVisa.Controls.Add(this.BtnRapport_Copier)
            this.tbVisa.Controls.Add(this.BtnRapport_RefusVisa);
            this.tbVisa.Controls.Add(this.BtnRapport_Visa);
            this.tbVisa.Location = new System.Drawing.Point(4, 22);
            this.tbVisa.Name = "tbVisa";
            this.tbVisa.Size = new System.Drawing.Size(272, 302);
            this.tbVisa.TabIndex = 2;
            this.tbVisa.Text = "Visa";
            // 
            // btnEnlevCorrection
            // 
            this.btnEnlevCorrection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnlevCorrection.Location = new System.Drawing.Point(16, 240);
            this.btnEnlevCorrection.Name = "btnEnlevCorrection";
            this.btnEnlevCorrection.Size = new System.Drawing.Size(232, 40);
            this.btnEnlevCorrection.TabIndex = 5;
            // BtnRapport_Copier
            // 
            this.BtnRapport_Copier.BackColor = System.Drawing.Color.Transparent;
            this.BtnRapport_Copier.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnRapport_Copier.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnRapport_Copier.ForeColor = System.Drawing.Color.Black;
            this.BtnRapport_Copier.Location = new System.Drawing.Point(16, 160);
            this.BtnRapport_Copier.Name = "BtnRapport_Copier";
            this.BtnRapport_Copier.Size = new System.Drawing.Size(80, 72);
            this.BtnRapport_Copier.TabIndex = 3;
            this.BtnRapport_Copier.Text = "Copier";
            this.BtnRapport_Copier.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.BtnRapport_Copier.UseVisualStyleBackColor = false;
            this.BtnRapport_Copier.Click += new System.EventHandler(this.BtnRapport_Copier_Click);

            this.BtnRapport_RefusVisa.Location = new System.Drawing.Point(104, 160);
            this.BtnRapport_RefusVisa.Size = new System.Drawing.Size(80, 72);
            // 
            this.BtnRapport_Visa.Location = new System.Drawing.Point(192, 160);
            this.BtnRapport_Visa.Size = new System.Drawing.Size(80, 72);
            this.label52.Location = new System.Drawing.Point(8, 8);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(256, 24);
            this.label52.TabIndex = 3;
            this.label52.Text = "Commentaire lors du VISA :";
            // 
            // TxtRapport_CommentaireVisa
            // 
            this.TxtRapport_CommentaireVisa.Location = new System.Drawing.Point(8, 32);
            this.TxtRapport_CommentaireVisa.Multiline = true;
            this.TxtRapport_CommentaireVisa.Name = "TxtRapport_CommentaireVisa";
            this.TxtRapport_CommentaireVisa.Size = new System.Drawing.Size(256, 88);
            this.TxtRapport_CommentaireVisa.TabIndex = 2;
            // 
            // BtnRapport_Copier
            // 
            this.BtnRapport_Copier.BackColor = System.Drawing.Color.Transparent;
            this.BtnRapport_Copier.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnRapport_Copier.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnRapport_Copier.ForeColor = System.Drawing.Color.Black;
            this.BtnRapport_Copier.Location = new System.Drawing.Point(16, 160);
            this.BtnRapport_Copier.Name = "BtnRapport_Copier";
            this.BtnRapport_Copier.Size = new System.Drawing.Size(80, 72);
            this.BtnRapport_Copier.TabIndex = 3;
            this.BtnRapport_Copier.Text = "Copier";
            this.BtnRapport_Copier.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.BtnRapport_Copier.UseVisualStyleBackColor = false;
            this.BtnRapport_Copier.Click += new System.EventHandler(this.BtnRapport_Copier_Click);
            // 
            // BtnRapport_RefusVisa
            // 
            this.BtnRapport_RefusVisa.BackColor = System.Drawing.Color.Transparent;
            this.BtnRapport_RefusVisa.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnRapport_RefusVisa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnRapport_RefusVisa.ForeColor = System.Drawing.Color.Black;
            this.BtnRapport_RefusVisa.Image = ((System.Drawing.Image)(resources.GetObject("BtnRapport_RefusVisa.Image")));
            this.BtnRapport_RefusVisa.Location = new System.Drawing.Point(104, 160);
            this.BtnRapport_RefusVisa.Name = "BtnRapport_RefusVisa";
            this.BtnRapport_RefusVisa.Size = new System.Drawing.Size(80, 72);
            this.BtnRapport_RefusVisa.TabIndex = 4;
            this.BtnRapport_RefusVisa.Text = "Refuser le visa";
            this.BtnRapport_RefusVisa.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.BtnRapport_RefusVisa.UseVisualStyleBackColor = false;
            this.BtnRapport_RefusVisa.Click += new System.EventHandler(this.BtnRapport_RefusVisa_Click);
            // 
            // BtnRapport_Visa
            // 
            this.BtnRapport_Visa.BackColor = System.Drawing.Color.Transparent;
            this.BtnRapport_Visa.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnRapport_Visa.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnRapport_Visa.ForeColor = System.Drawing.Color.Black;
            this.BtnRapport_Visa.Image = ((System.Drawing.Image)(resources.GetObject("BtnRapport_Visa.Image")));
            this.BtnRapport_Visa.Location = new System.Drawing.Point(144, 160);
            this.BtnRapport_Visa.Name = "BtnRapport_Visa";
            this.BtnRapport_Visa.Size = new System.Drawing.Size(80, 72);
            this.BtnRapport_Visa.TabIndex = 1;
            this.BtnRapport_Visa.UseVisualStyleBackColor = false;
            this.BtnRapport_Visa.Click += new System.EventHandler(this.BtnRapport_Visa_Click);
            // 
            // tbCommunication
            // 
            this.tbCommunication.Controls.Add(this.fpRapport_Destinataires);
            this.tbCommunication.Controls.Add(this.lnkRapport_AjoutDestinataire);
            this.tbCommunication.Controls.Add(this.groupBox5);
            this.tbCommunication.Controls.Add(this.groupBox7);
            this.tbCommunication.Controls.Add(this.groupBox6);
            this.tbCommunication.Location = new System.Drawing.Point(4, 22);
            this.tbCommunication.Name = "tbCommunication";
            this.tbCommunication.Size = new System.Drawing.Size(272, 302);
            this.tbCommunication.TabIndex = 3;
            this.tbCommunication.Text = "Communication";
            // 
            // fpRapport_Destinataires
            // 
            this.fpRapport_Destinataires.AccessibleDescription = "";
            this.fpRapport_Destinataires.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.fpRapport_Destinataires.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fpRapport_Destinataires.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
            this.fpRapport_Destinataires.Location = new System.Drawing.Point(8, 24);
            this.fpRapport_Destinataires.Name = "fpRapport_Destinataires";
            this.fpRapport_Destinataires.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpRapport_Destinataires_Sheet1});
            this.fpRapport_Destinataires.Size = new System.Drawing.Size(256, 64);
            this.fpRapport_Destinataires.TabIndex = 83;
            this.fpRapport_Destinataires.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never;
            this.fpRapport_Destinataires.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpRapport_Destinataires.MouseUp += new System.Windows.Forms.MouseEventHandler(this.fpRapport_Destinataires_MouseUp);
            // 
            // fpRapport_Destinataires_Sheet1
            // 
            this.fpRapport_Destinataires_Sheet1.Reset();
            this.fpRapport_Destinataires_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpRapport_Destinataires_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpRapport_Destinataires_Sheet1.ColumnHeader.Visible = false;
            this.fpRapport_Destinataires_Sheet1.GrayAreaBackColor = System.Drawing.Color.Gainsboro;
            this.fpRapport_Destinataires_Sheet1.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.None);
            this.fpRapport_Destinataires_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
            this.fpRapport_Destinataires_Sheet1.RowHeader.Visible = false;
            this.fpRapport_Destinataires_Sheet1.VerticalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.None);
            this.fpRapport_Destinataires_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // lnkRapport_AjoutDestinataire
            // 
            this.lnkRapport_AjoutDestinataire.Location = new System.Drawing.Point(8, 8);
            this.lnkRapport_AjoutDestinataire.Name = "lnkRapport_AjoutDestinataire";
            this.lnkRapport_AjoutDestinataire.Size = new System.Drawing.Size(248, 16);
            this.lnkRapport_AjoutDestinataire.TabIndex = 82;
            this.lnkRapport_AjoutDestinataire.TabStop = true;
            this.lnkRapport_AjoutDestinataire.Text = "Ajouter un destinataire";
            this.lnkRapport_AjoutDestinataire.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkRapport_AjoutDestinataire_LinkClicked);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.picRapport_Export2);
            this.groupBox5.Controls.Add(this.picRapport_Export1);
            this.groupBox5.Controls.Add(this.cbRapport_Format);
            this.groupBox5.Controls.Add(this.label42);
            this.groupBox5.Location = new System.Drawing.Point(8, 136);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(256, 56);
            this.groupBox5.TabIndex = 78;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Exportation";
            // 
            // picRapport_Export2
            // 
            this.picRapport_Export2.Image = ((System.Drawing.Image)(resources.GetObject("picRapport_Export2.Image")));
            this.picRapport_Export2.Location = new System.Drawing.Point(229, 23);
            this.picRapport_Export2.Name = "picRapport_Export2";
            this.picRapport_Export2.Size = new System.Drawing.Size(25, 22);
            this.picRapport_Export2.TabIndex = 6;
            this.picRapport_Export2.TabStop = false;
            // 
            // picRapport_Export1
            // 
            this.picRapport_Export1.Image = ((System.Drawing.Image)(resources.GetObject("picRapport_Export1.Image")));
            this.picRapport_Export1.Location = new System.Drawing.Point(198, 23);
            this.picRapport_Export1.Name = "picRapport_Export1";
            this.picRapport_Export1.Size = new System.Drawing.Size(25, 21);
            this.picRapport_Export1.TabIndex = 5;
            this.picRapport_Export1.TabStop = false;
            this.picRapport_Export1.Click += new System.EventHandler(this.picRapport_Export1_Click);
            // 
            // cbRapport_Format
            // 
            this.cbRapport_Format.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRapport_Format.Items.AddRange(new object[] {
            "pdf",
            "doc",
            "htm",
            "xls",
            "rtf"});
            this.cbRapport_Format.Location = new System.Drawing.Point(67, 22);
            this.cbRapport_Format.Name = "cbRapport_Format";
            this.cbRapport_Format.Size = new System.Drawing.Size(56, 21);
            this.cbRapport_Format.TabIndex = 3;
            // 
            // label42
            // 
            this.label42.Location = new System.Drawing.Point(8, 24);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(56, 16);
            this.label42.TabIndex = 0;
            this.label42.Text = "Format :";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.picRapport_Mail2);
            this.groupBox7.Controls.Add(this.picRapport_Mail1);
            this.groupBox7.Location = new System.Drawing.Point(8, 96);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(256, 40);
            this.groupBox7.TabIndex = 80;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Envoi du rapport par mail";
            // 
            // picRapport_Mail2
            // 
            this.picRapport_Mail2.Image = ((System.Drawing.Image)(resources.GetObject("picRapport_Mail2.Image")));
            this.picRapport_Mail2.Location = new System.Drawing.Point(227, 11);
            this.picRapport_Mail2.Name = "picRapport_Mail2";
            this.picRapport_Mail2.Size = new System.Drawing.Size(22, 26);
            this.picRapport_Mail2.TabIndex = 6;
            this.picRapport_Mail2.TabStop = false;
            // 
            // picRapport_Mail1
            // 
            this.picRapport_Mail1.Image = ((System.Drawing.Image)(resources.GetObject("picRapport_Mail1.Image")));
            this.picRapport_Mail1.Location = new System.Drawing.Point(197, 12);
            this.picRapport_Mail1.Name = "picRapport_Mail1";
            this.picRapport_Mail1.Size = new System.Drawing.Size(22, 25);
            this.picRapport_Mail1.TabIndex = 5;
            this.picRapport_Mail1.TabStop = false;
            this.picRapport_Mail1.Click += new System.EventHandler(this.picRapport_Mail1_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.chkFax);
            this.groupBox6.Controls.Add(this.chkLogo);
            this.groupBox6.Controls.Add(this.picRapport_Print2);
            this.groupBox6.Controls.Add(this.picRapport_Print1);
            this.groupBox6.Controls.Add(this.cbRapport_Imprimante);
            this.groupBox6.Controls.Add(this.txtRapport_NbCopies);
            this.groupBox6.Controls.Add(this.label45);
            this.groupBox6.Controls.Add(this.label44);
            this.groupBox6.Location = new System.Drawing.Point(8, 192);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(256, 104);
            this.groupBox6.TabIndex = 79;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Impression du rapport";
            // 
            // chkFax
            // 
            this.chkFax.Location = new System.Drawing.Point(93, 78);
            this.chkFax.Name = "chkFax";
            this.chkFax.Size = new System.Drawing.Size(65, 18);
            this.chkFax.TabIndex = 10;
            this.chkFax.Text = "Fax";
            // 
            // chkLogo
            // 
            this.chkLogo.Location = new System.Drawing.Point(10, 78);
            this.chkLogo.Name = "chkLogo";
            this.chkLogo.Size = new System.Drawing.Size(65, 18);
            this.chkLogo.TabIndex = 9;
            this.chkLogo.Text = "Logo";
            // 
            // picRapport_Print2
            // 
            this.picRapport_Print2.Image = ((System.Drawing.Image)(resources.GetObject("picRapport_Print2.Image")));
            this.picRapport_Print2.Location = new System.Drawing.Point(227, 75);
            this.picRapport_Print2.Name = "picRapport_Print2";
            this.picRapport_Print2.Size = new System.Drawing.Size(25, 25);
            this.picRapport_Print2.TabIndex = 8;
            this.picRapport_Print2.TabStop = false;
            // 
            // picRapport_Print1
            // 
            this.picRapport_Print1.Image = ((System.Drawing.Image)(resources.GetObject("picRapport_Print1.Image")));
            this.picRapport_Print1.Location = new System.Drawing.Point(197, 75);
            this.picRapport_Print1.Name = "picRapport_Print1";
            this.picRapport_Print1.Size = new System.Drawing.Size(27, 25);
            this.picRapport_Print1.TabIndex = 7;
            this.picRapport_Print1.TabStop = false;
            this.picRapport_Print1.Click += new System.EventHandler(this.picRapport_Print1_Click);
            // 
            // cbRapport_Imprimante
            // 
            this.cbRapport_Imprimante.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRapport_Imprimante.Location = new System.Drawing.Point(80, 21);
            this.cbRapport_Imprimante.Name = "cbRapport_Imprimante";
            this.cbRapport_Imprimante.Size = new System.Drawing.Size(176, 21);
            this.cbRapport_Imprimante.TabIndex = 5;
            // 
            // txtRapport_NbCopies
            // 
            this.txtRapport_NbCopies.Location = new System.Drawing.Point(80, 45);
            this.txtRapport_NbCopies.Name = "txtRapport_NbCopies";
            this.txtRapport_NbCopies.Size = new System.Drawing.Size(35, 20);
            this.txtRapport_NbCopies.TabIndex = 4;
            this.txtRapport_NbCopies.Text = "1";
            // 
            // label45
            // 
            this.label45.Location = new System.Drawing.Point(8, 48);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(64, 16);
            this.label45.TabIndex = 2;
            this.label45.Text = "Copies :";
            // 
            // label44
            // 
            this.label44.Location = new System.Drawing.Point(8, 24);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(64, 16);
            this.label44.TabIndex = 1;
            this.label44.Text = "Imprimante :";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.lstEnvois);
            this.groupBox8.Controls.Add(this.TxtRapport_Commentaire);
            this.groupBox8.Location = new System.Drawing.Point(947, 96);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(173, 175);
            this.groupBox8.TabIndex = 85;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Liste des envois";
            // 
            // lstEnvois
            // 
            this.lstEnvois.Location = new System.Drawing.Point(8, 16);
            this.lstEnvois.Name = "lstEnvois";
            this.lstEnvois.Size = new System.Drawing.Size(161, 82);
            this.lstEnvois.TabIndex = 1;
            // 
            // TxtRapport_Commentaire
            // 
            this.TxtRapport_Commentaire.Location = new System.Drawing.Point(8, 16);
            this.TxtRapport_Commentaire.Multiline = true;
            this.TxtRapport_Commentaire.Name = "TxtRapport_Commentaire";
            this.TxtRapport_Commentaire.ReadOnly = true;
            this.TxtRapport_Commentaire.Size = new System.Drawing.Size(161, 96);
            this.TxtRapport_Commentaire.TabIndex = 0;
            // 
            // panel8
            // 
            this.panel8.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel8.Controls.Add(this.LblSauvegardeRapport);
            this.panel8.Controls.Add(this.LblRapportVise);
            this.panel8.Controls.Add(this.LblRapportModifie);
            this.panel8.Controls.Add(this.LblRapportCree);
            this.panel8.Controls.Add(this.txtRapport_CommentaireSauvegarde);
            this.panel8.Controls.Add(this.label51);
            this.panel8.Controls.Add(this.label49);
            this.panel8.Controls.Add(this.pic_ValideRapport);
            this.panel8.Location = new System.Drawing.Point(944, 277);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(176, 352);
            this.panel8.TabIndex = 84;
            // 
            // LblSauvegardeRapport
            // 
            this.LblSauvegardeRapport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSauvegardeRapport.ForeColor = System.Drawing.Color.Red;
            this.LblSauvegardeRapport.Location = new System.Drawing.Point(3, 268);
            this.LblSauvegardeRapport.Name = "LblSauvegardeRapport";
            this.LblSauvegardeRapport.Size = new System.Drawing.Size(159, 16);
            this.LblSauvegardeRapport.TabIndex = 74;
            // 
            // LblRapportVise
            // 
            this.LblRapportVise.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblRapportVise.ForeColor = System.Drawing.Color.DarkGray;
            this.LblRapportVise.Location = new System.Drawing.Point(3, 136);
            this.LblRapportVise.Name = "LblRapportVise";
            this.LblRapportVise.Size = new System.Drawing.Size(155, 53);
            this.LblRapportVise.TabIndex = 73;
            // 
            // LblRapportModifie
            // 
            this.LblRapportModifie.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblRapportModifie.ForeColor = System.Drawing.Color.DarkGray;
            this.LblRapportModifie.Location = new System.Drawing.Point(4, 73);
            this.LblRapportModifie.Name = "LblRapportModifie";
            this.LblRapportModifie.Size = new System.Drawing.Size(158, 63);
            this.LblRapportModifie.TabIndex = 72;
            // 
            // LblRapportCree
            // 
            this.LblRapportCree.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblRapportCree.ForeColor = System.Drawing.Color.DarkGray;
            this.LblRapportCree.Location = new System.Drawing.Point(7, 7);
            this.LblRapportCree.Name = "LblRapportCree";
            this.LblRapportCree.Size = new System.Drawing.Size(151, 64);
            this.LblRapportCree.TabIndex = 71;
            // 
            // txtRapport_CommentaireSauvegarde
            // 
            this.txtRapport_CommentaireSauvegarde.Location = new System.Drawing.Point(3, 207);
            this.txtRapport_CommentaireSauvegarde.Multiline = true;
            this.txtRapport_CommentaireSauvegarde.Name = "txtRapport_CommentaireSauvegarde";
            this.txtRapport_CommentaireSauvegarde.Size = new System.Drawing.Size(159, 58);
            this.txtRapport_CommentaireSauvegarde.TabIndex = 70;
            // 
            // label51
            // 
            this.label51.Location = new System.Drawing.Point(4, 189);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(158, 15);
            this.label51.TabIndex = 69;
            this.label51.Text = "Commentaire de sauvegarde :";
            // 
            // label49
            // 
            this.label49.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label49.Location = new System.Drawing.Point(2, 284);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(129, 54);
            this.label49.TabIndex = 68;
            this.label49.Text = "Sauvegarde du rapport :";
            // 
            // pic_ValideRapport
            // 
            this.pic_ValideRapport.Image = ((System.Drawing.Image)(resources.GetObject("pic_ValideRapport.Image")));
            this.pic_ValideRapport.Location = new System.Drawing.Point(137, 295);
            this.pic_ValideRapport.Name = "pic_ValideRapport";
            this.pic_ValideRapport.Size = new System.Drawing.Size(32, 31);
            this.pic_ValideRapport.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_ValideRapport.TabIndex = 67;
            this.pic_ValideRapport.TabStop = false;
            this.toolTip1.SetToolTip(this.pic_ValideRapport, "Quitter l\'application");
            this.pic_ValideRapport.Click += new System.EventHandler(this.pic_ValideRapport_Click);
            // 
            // lblTest
            // 
            this.lblTest.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTest.Location = new System.Drawing.Point(94, 802);
            this.lblTest.Name = "lblTest";
            this.lblTest.Size = new System.Drawing.Size(176, 24);
            this.lblTest.TabIndex = 83;
            this.lblTest.Text = "TEXTE SAISI";
            this.lblTest.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label43
            // 
            this.label43.Font = new System.Drawing.Font("Arial", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label43.ForeColor = System.Drawing.Color.White;
            this.label43.Location = new System.Drawing.Point(390, 802);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(160, 24);
            this.label43.TabIndex = 82;
            this.label43.Text = "(F12 pour actualiser)";
            this.label43.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fpRapport
            // 
            this.fpRapport.AccessibleDescription = "fpRapport, Sheet1, Row 0, Column 0, ";
            this.fpRapport.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
            this.fpRapport.Location = new System.Drawing.Point(664, 96);
            this.fpRapport.Name = "fpRapport";
            this.fpRapport.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpRapport_Sheet1});
            this.fpRapport.Size = new System.Drawing.Size(280, 199);
            this.fpRapport.TabIndex = 11;
            this.fpRapport.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpRapport.MouseUp += new System.Windows.Forms.MouseEventHandler(this.fpRapport_MouseUp);
            // 
            // fpRapport_Sheet1
            // 
            this.fpRapport_Sheet1.Reset();
            this.fpRapport_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpRapport_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpRapport_Sheet1.ColumnHeader.Visible = false;
            this.fpRapport_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.fpRapport_Sheet1.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.None);
            this.fpRapport_Sheet1.RowHeader.Visible = false;
            this.fpRapport_Sheet1.VerticalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.None);
            this.fpRapport_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(1096, 56);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(8, 8);
            this.richTextBox1.TabIndex = 10;
            this.richTextBox1.Text = "richTextBox1";
            this.richTextBox1.Visible = false;
            // 
            // btnRapport_Font
            // 
            this.btnRapport_Font.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRapport_Font.Location = new System.Drawing.Point(46, 802);
            this.btnRapport_Font.Name = "btnRapport_Font";
            this.btnRapport_Font.Size = new System.Drawing.Size(32, 24);
            this.btnRapport_Font.TabIndex = 9;
            this.btnRapport_Font.Text = "A";
            this.btnRapport_Font.UseVisualStyleBackColor = false;
            this.btnRapport_Font.Click += new System.EventHandler(this.btnRapport_Font_Click);
            // 
            // btnRapport_Couleur
            // 
            this.btnRapport_Couleur.BackColor = System.Drawing.Color.Black;
            this.btnRapport_Couleur.Location = new System.Drawing.Point(6, 802);
            this.btnRapport_Couleur.Name = "btnRapport_Couleur";
            this.btnRapport_Couleur.Size = new System.Drawing.Size(32, 24);
            this.btnRapport_Couleur.TabIndex = 5;
            this.btnRapport_Couleur.UseVisualStyleBackColor = false;
            this.btnRapport_Couleur.Click += new System.EventHandler(this.btnRapport_Couleur_Click);
            // 
            // txtSaisieRapport
            // 
            this.txtSaisieRapport.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSaisieRapport.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSaisieRapport.Location = new System.Drawing.Point(8, 699);
            this.txtSaisieRapport.Name = "txtSaisieRapport";
            this.txtSaisieRapport.Size = new System.Drawing.Size(552, 96);
            this.txtSaisieRapport.TabIndex = 4;
            this.txtSaisieRapport.Text = "";
            this.txtSaisieRapport.TextChanged += new System.EventHandler(this.TxtSaisieRapport_TextChanged);
            this.txtSaisieRapport.Leave += new System.EventHandler(this.txtSaisieRapport_Leave);
            // 
            // cmdRapport_Bonjour
            // 
            this.cmdRapport_Bonjour.BackColor = System.Drawing.Color.Transparent;
            this.cmdRapport_Bonjour.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmdRapport_Bonjour.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdRapport_Bonjour.ForeColor = System.Drawing.Color.Black;
            this.cmdRapport_Bonjour.Image = ((System.Drawing.Image)(resources.GetObject("cmdRapport_Bonjour.Image")));
            this.cmdRapport_Bonjour.Location = new System.Drawing.Point(264, 652);
            this.cmdRapport_Bonjour.Name = "cmdRapport_Bonjour";
            this.cmdRapport_Bonjour.Size = new System.Drawing.Size(72, 32);
            this.cmdRapport_Bonjour.TabIndex = 93;
            this.cmdRapport_Bonjour.Text = "Bonjour     (F4)";
            this.cmdRapport_Bonjour.UseVisualStyleBackColor = false;
            this.cmdRapport_Bonjour.Click += new System.EventHandler(this.cmdRapport_ItemClick);
            // 
            // cmdRapport_Corps
            // 
            this.cmdRapport_Corps.BackColor = System.Drawing.Color.Transparent;
            this.cmdRapport_Corps.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmdRapport_Corps.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdRapport_Corps.ForeColor = System.Drawing.Color.Black;
            this.cmdRapport_Corps.Image = ((System.Drawing.Image)(resources.GetObject("cmdRapport_Corps.Image")));
            this.cmdRapport_Corps.Location = new System.Drawing.Point(566, 754);
            this.cmdRapport_Corps.Name = "cmdRapport_Corps";
            this.cmdRapport_Corps.Size = new System.Drawing.Size(88, 40);
            this.cmdRapport_Corps.TabIndex = 92;
            this.cmdRapport_Corps.Text = "Corps (F8)";
            this.cmdRapport_Corps.UseVisualStyleBackColor = false;
            this.cmdRapport_Corps.Click += new System.EventHandler(this.cmdRapport_ItemClick);
            // 
            // picRapport_Actualiser
            // 
            this.picRapport_Actualiser.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.picRapport_Actualiser.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.picRapport_Actualiser.ForeColor = System.Drawing.Color.RoyalBlue;
            this.picRapport_Actualiser.Image = ((System.Drawing.Image)(resources.GetObject("picRapport_Actualiser.Image")));
            this.picRapport_Actualiser.Location = new System.Drawing.Point(278, 802);
            this.picRapport_Actualiser.Name = "picRapport_Actualiser";
            this.picRapport_Actualiser.Size = new System.Drawing.Size(112, 24);
            this.picRapport_Actualiser.TabIndex = 88;
            this.picRapport_Actualiser.Text = "RAFFRAICHIR";
            this.picRapport_Actualiser.Click += new System.EventHandler(this.PicRapport_Actualiser_Click);
            // 
            // cmdRapport_Signature
            // 
            this.cmdRapport_Signature.BackColor = System.Drawing.Color.Transparent;
            this.cmdRapport_Signature.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmdRapport_Signature.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdRapport_Signature.ForeColor = System.Drawing.Color.Black;
            this.cmdRapport_Signature.Image = ((System.Drawing.Image)(resources.GetObject("cmdRapport_Signature.Image")));
            this.cmdRapport_Signature.Location = new System.Drawing.Point(528, 652);
            this.cmdRapport_Signature.Name = "cmdRapport_Signature";
            this.cmdRapport_Signature.Size = new System.Drawing.Size(72, 32);
            this.cmdRapport_Signature.TabIndex = 71;
            this.cmdRapport_Signature.Text = "Signature (F7)";
            this.cmdRapport_Signature.UseVisualStyleBackColor = false;
            this.cmdRapport_Signature.Click += new System.EventHandler(this.cmdRapport_ItemClick);
            // 
            // cmdRapport_Salutations
            // 
            this.cmdRapport_Salutations.BackColor = System.Drawing.Color.Transparent;
            this.cmdRapport_Salutations.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmdRapport_Salutations.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdRapport_Salutations.ForeColor = System.Drawing.Color.Black;
            this.cmdRapport_Salutations.Image = ((System.Drawing.Image)(resources.GetObject("cmdRapport_Salutations.Image")));
            this.cmdRapport_Salutations.Location = new System.Drawing.Point(440, 652);
            this.cmdRapport_Salutations.Name = "cmdRapport_Salutations";
            this.cmdRapport_Salutations.Size = new System.Drawing.Size(80, 32);
            this.cmdRapport_Salutations.TabIndex = 70;
            this.cmdRapport_Salutations.Text = "Salutations (F6)";
            this.cmdRapport_Salutations.UseVisualStyleBackColor = false;
            this.cmdRapport_Salutations.Click += new System.EventHandler(this.cmdRapport_ItemClick);
            // 
            // cmdRapport_Intro
            // 
            this.cmdRapport_Intro.BackColor = System.Drawing.Color.Transparent;
            this.cmdRapport_Intro.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmdRapport_Intro.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdRapport_Intro.ForeColor = System.Drawing.Color.Black;
            this.cmdRapport_Intro.Image = ((System.Drawing.Image)(resources.GetObject("cmdRapport_Intro.Image")));
            this.cmdRapport_Intro.Location = new System.Drawing.Point(344, 652);
            this.cmdRapport_Intro.Name = "cmdRapport_Intro";
            this.cmdRapport_Intro.Size = new System.Drawing.Size(88, 32);
            this.cmdRapport_Intro.TabIndex = 69;
            this.cmdRapport_Intro.Text = "Introduction          (F5)";
            this.cmdRapport_Intro.UseVisualStyleBackColor = false;
            this.cmdRapport_Intro.Click += new System.EventHandler(this.cmdRapport_ItemClick);
            // 
            // cmdRapport_EnTete
            // 
            this.cmdRapport_EnTete.BackColor = System.Drawing.Color.Transparent;
            this.cmdRapport_EnTete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmdRapport_EnTete.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdRapport_EnTete.ForeColor = System.Drawing.Color.Black;
            this.cmdRapport_EnTete.Image = ((System.Drawing.Image)(resources.GetObject("cmdRapport_EnTete.Image")));
            this.cmdRapport_EnTete.Location = new System.Drawing.Point(8, 652);
            this.cmdRapport_EnTete.Name = "cmdRapport_EnTete";
            this.cmdRapport_EnTete.Size = new System.Drawing.Size(72, 32);
            this.cmdRapport_EnTete.TabIndex = 68;
            this.cmdRapport_EnTete.Text = "En-Tete     (F1)";
            this.cmdRapport_EnTete.UseVisualStyleBackColor = false;
            this.cmdRapport_EnTete.Click += new System.EventHandler(this.cmdRapport_ItemClick);
            // 
            // cmdRapport_Concerne
            // 
            this.cmdRapport_Concerne.BackColor = System.Drawing.Color.Transparent;
            this.cmdRapport_Concerne.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmdRapport_Concerne.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdRapport_Concerne.ForeColor = System.Drawing.Color.Black;
            this.cmdRapport_Concerne.Image = ((System.Drawing.Image)(resources.GetObject("cmdRapport_Concerne.Image")));
            this.cmdRapport_Concerne.Location = new System.Drawing.Point(184, 652);
            this.cmdRapport_Concerne.Name = "cmdRapport_Concerne";
            this.cmdRapport_Concerne.Size = new System.Drawing.Size(72, 32);
            this.cmdRapport_Concerne.TabIndex = 8;
            this.cmdRapport_Concerne.Text = "Concerne (F3)";
            this.cmdRapport_Concerne.UseVisualStyleBackColor = false;
            this.cmdRapport_Concerne.Click += new System.EventHandler(this.cmdRapport_ItemClick);
            // 
            // cmdRapport_Destinataire
            // 
            this.cmdRapport_Destinataire.BackColor = System.Drawing.Color.Transparent;
            this.cmdRapport_Destinataire.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmdRapport_Destinataire.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdRapport_Destinataire.ForeColor = System.Drawing.Color.Black;
            this.cmdRapport_Destinataire.Image = ((System.Drawing.Image)(resources.GetObject("cmdRapport_Destinataire.Image")));
            this.cmdRapport_Destinataire.Location = new System.Drawing.Point(88, 652);
            this.cmdRapport_Destinataire.Name = "cmdRapport_Destinataire";
            this.cmdRapport_Destinataire.Size = new System.Drawing.Size(88, 32);
            this.cmdRapport_Destinataire.TabIndex = 7;
            this.cmdRapport_Destinataire.Text = "Destinataire (F2)";
            this.cmdRapport_Destinataire.UseVisualStyleBackColor = false;
            this.cmdRapport_Destinataire.Click += new System.EventHandler(this.cmdRapport_ItemClick);
            // 
            // tbFacturation
            // 
            this.tbFacturation.Location = new System.Drawing.Point(4, 22);
            this.tbFacturation.Name = "tbFacturation";
            this.tbFacturation.Size = new System.Drawing.Size(1148, 864);
            this.tbFacturation.TabIndex = 1;
            this.tbFacturation.Text = "Facturation";
            // 
            // button5
            // 
            this.button5.Enabled = false;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button5.ForeColor = System.Drawing.Color.Black;
            this.button5.Image = ((System.Drawing.Image)(resources.GetObject("button5.Image")));
            this.button5.Location = new System.Drawing.Point(601, 2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(306, 20);
            this.button5.TabIndex = 11;
            this.button5.Text = "Adresse";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Enabled = false;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button4.ForeColor = System.Drawing.Color.Black;
            this.button4.Image = ((System.Drawing.Image)(resources.GetObject("button4.Image")));
            this.button4.Location = new System.Drawing.Point(381, 2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(166, 20);
            this.button4.TabIndex = 10;
            this.button4.Text = "Patient";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button3_Click);
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.ForeColor = System.Drawing.Color.Black;
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.Location = new System.Drawing.Point(221, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(130, 20);
            this.button3.TabIndex = 9;
            this.button3.Text = "Médecin";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(0, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(73, 20);
            this.button1.TabIndex = 7;
            this.button1.Text = "Index";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.ForeColor = System.Drawing.Color.Black;
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.Location = new System.Drawing.Point(72, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(131, 20);
            this.button2.TabIndex = 8;
            this.button2.Text = "Date Appel";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button3_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.CadetBlue;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.zoomImageViewer1);
            this.panel2.Controls.Add(this.bRotationImage);
            this.panel2.Controls.Add(this.labelInfoReport);
            this.panel2.Controls.Add(this.pBFiches);
            this.panel2.Controls.Add(this.LblBaseTest);
            this.panel2.Controls.Add(this.pan_Statiques);
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Controls.Add(this.groupBox4);
            this.panel2.Controls.Add(this.lblConnecte);
            this.panel2.Controls.Add(this.tab);
            this.panel2.Controls.Add(this.btnOnglet3);
            this.panel2.Controls.Add(this.btnOnglet2);
            this.panel2.Controls.Add(this.btnOnglet1);
            this.panel2.Controls.Add(this.lblRaisonSociale);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(419, 888);
            this.panel2.TabIndex = 1;
            // 
            // bRotationImage
            // 
            this.bRotationImage.FlatAppearance.BorderSize = 0;
            this.bRotationImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bRotationImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bRotationImage.Image = global::ImportSosGeneve.Properties.Resources.icone_Rotation;
            this.bRotationImage.Location = new System.Drawing.Point(322, 822);
            this.bRotationImage.Name = "bRotationImage";
            this.bRotationImage.Size = new System.Drawing.Size(62, 59);
            this.bRotationImage.TabIndex = 74;
            this.bRotationImage.Tag = "";
            this.bRotationImage.Text = "90°";
            this.toolTip1.SetToolTip(this.bRotationImage, "Rotation de l\'image");
            this.bRotationImage.UseVisualStyleBackColor = true;
            this.bRotationImage.Visible = false;
            this.bRotationImage.Click += new System.EventHandler(this.bRotationImage_Click);
            // 
            // labelInfoReport
            // 
            this.labelInfoReport.AutoSize = true;
            this.labelInfoReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInfoReport.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.labelInfoReport.Location = new System.Drawing.Point(8, 738);
            this.labelInfoReport.Name = "labelInfoReport";
            this.labelInfoReport.Size = new System.Drawing.Size(0, 13);
            this.labelInfoReport.TabIndex = 91;
            this.labelInfoReport.Click += new System.EventHandler(this.label21_Click);
            // 
            // pBFiches
            // 
            this.pBFiches.Image = ((System.Drawing.Image)(resources.GetObject("pBFiches.Image")));
            this.pBFiches.Location = new System.Drawing.Point(365, 541);
            this.pBFiches.Name = "pBFiches";
            this.pBFiches.Size = new System.Drawing.Size(46, 106);
            this.pBFiches.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pBFiches.TabIndex = 90;
            this.pBFiches.TabStop = false;
            this.pBFiches.Click += new System.EventHandler(this.pBFiches_Click);
            // 
            // LblBaseTest
            // 
            this.LblBaseTest.BackColor = System.Drawing.Color.CadetBlue;
            this.LblBaseTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblBaseTest.ForeColor = System.Drawing.Color.Red;
            this.LblBaseTest.Location = new System.Drawing.Point(5, 6);
            this.LblBaseTest.Name = "LblBaseTest";
            this.LblBaseTest.Size = new System.Drawing.Size(273, 51);
            this.LblBaseTest.TabIndex = 29;
            this.LblBaseTest.Text = "VERSION TEST";
            this.LblBaseTest.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pan_Statiques
            // 
            this.pan_Statiques.Controls.Add(this.groupBox3);
            this.pan_Statiques.Controls.Add(this.groupBox1);
            this.pan_Statiques.Location = new System.Drawing.Point(6, 313);
            this.pan_Statiques.Name = "pan_Statiques";
            this.pan_Statiques.Size = new System.Drawing.Size(300, 304);
            this.pan_Statiques.TabIndex = 6;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.CadetBlue;
            this.groupBox3.Controls.Add(this.lbDuree);
            this.groupBox3.Controls.Add(this.lbDelai);
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Controls.Add(this.label72);
            this.groupBox3.Controls.Add(this.label73);
            this.groupBox3.Controls.Add(this.lbFIN);
            this.groupBox3.Controls.Add(this.lbSLL);
            this.groupBox3.Controls.Add(this.lbRCP);
            this.groupBox3.Controls.Add(this.lblHFI);
            this.groupBox3.Controls.Add(this.LblSLL);
            this.groupBox3.Controls.Add(this.LblHTR);
            this.groupBox3.Controls.Add(this.lblMedecin);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(3, 165);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(294, 210);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Prise en charge";
            // 
            // lbDuree
            // 
            this.lbDuree.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDuree.ForeColor = System.Drawing.Color.Black;
            this.lbDuree.Location = new System.Drawing.Point(230, 117);
            this.lbDuree.Name = "lbDuree";
            this.lbDuree.Size = new System.Drawing.Size(58, 17);
            this.lbDuree.TabIndex = 19;
            this.lbDuree.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbDelai
            // 
            this.lbDelai.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDelai.ForeColor = System.Drawing.Color.Black;
            this.lbDelai.Location = new System.Drawing.Point(74, 118);
            this.lbDelai.Name = "lbDelai";
            this.lbDelai.Size = new System.Drawing.Size(58, 17);
            this.lbDelai.TabIndex = 18;
            this.lbDelai.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.CadetBlue;
            this.groupBox2.Controls.Add(this.lblDevenirAnnulation);
            this.groupBox2.Controls.Add(this.lblMotifAnnulation);
            this.groupBox2.Controls.Add(this.lblAnnulation);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(0, 138);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(294, 99);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Annulation";
            // 
            // lblDevenirAnnulation
            // 
            this.lblDevenirAnnulation.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDevenirAnnulation.ForeColor = System.Drawing.Color.Black;
            this.lblDevenirAnnulation.Location = new System.Drawing.Point(6, 68);
            this.lblDevenirAnnulation.Name = "lblDevenirAnnulation";
            this.lblDevenirAnnulation.Size = new System.Drawing.Size(279, 23);
            this.lblDevenirAnnulation.TabIndex = 7;
            this.lblDevenirAnnulation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMotifAnnulation
            // 
            this.lblMotifAnnulation.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMotifAnnulation.ForeColor = System.Drawing.Color.Black;
            this.lblMotifAnnulation.Location = new System.Drawing.Point(6, 44);
            this.lblMotifAnnulation.Name = "lblMotifAnnulation";
            this.lblMotifAnnulation.Size = new System.Drawing.Size(279, 18);
            this.lblMotifAnnulation.TabIndex = 6;
            this.lblMotifAnnulation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAnnulation
            // 
            this.lblAnnulation.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAnnulation.ForeColor = System.Drawing.Color.Black;
            this.lblAnnulation.Location = new System.Drawing.Point(6, 20);
            this.lblAnnulation.Name = "lblAnnulation";
            this.lblAnnulation.Size = new System.Drawing.Size(279, 20);
            this.lblAnnulation.TabIndex = 5;
            this.lblAnnulation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label72
            // 
            this.label72.AutoSize = true;
            this.label72.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label72.ForeColor = System.Drawing.Color.Black;
            this.label72.Location = new System.Drawing.Point(153, 118);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(64, 16);
            this.label72.TabIndex = 17;
            this.label72.Text = "Durée :";
            this.label72.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label73
            // 
            this.label73.AutoSize = true;
            this.label73.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label73.ForeColor = System.Drawing.Color.Black;
            this.label73.Location = new System.Drawing.Point(8, 118);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(64, 16);
            this.label73.TabIndex = 16;
            this.label73.Text = "Délai :";
            this.label73.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbFIN
            // 
            this.lbFIN.BackColor = System.Drawing.Color.Transparent;
            this.lbFIN.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFIN.ForeColor = System.Drawing.Color.Black;
            this.lbFIN.Location = new System.Drawing.Point(106, 94);
            this.lbFIN.Name = "lbFIN";
            this.lbFIN.Size = new System.Drawing.Size(119, 18);
            this.lbFIN.TabIndex = 15;
            this.lbFIN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbSLL
            // 
            this.lbSLL.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSLL.ForeColor = System.Drawing.Color.Black;
            this.lbSLL.Location = new System.Drawing.Point(106, 70);
            this.lbSLL.Name = "lbSLL";
            this.lbSLL.Size = new System.Drawing.Size(119, 16);
            this.lbSLL.TabIndex = 14;
            this.lbSLL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbRCP
            // 
            this.lbRCP.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRCP.ForeColor = System.Drawing.Color.Black;
            this.lbRCP.Location = new System.Drawing.Point(106, 46);
            this.lbRCP.Name = "lbRCP";
            this.lbRCP.Size = new System.Drawing.Size(119, 16);
            this.lbRCP.TabIndex = 13;
            this.lbRCP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblHFI
            // 
            this.lblHFI.AutoSize = true;
            this.lblHFI.BackColor = System.Drawing.Color.Transparent;
            this.lblHFI.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHFI.ForeColor = System.Drawing.Color.Black;
            this.lblHFI.Location = new System.Drawing.Point(8, 94);
            this.lblHFI.Name = "lblHFI";
            this.lblHFI.Size = new System.Drawing.Size(48, 16);
            this.lblHFI.TabIndex = 12;
            this.lblHFI.Text = "Fin :";
            this.lblHFI.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LblSLL
            // 
            this.LblSLL.AutoSize = true;
            this.LblSLL.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSLL.ForeColor = System.Drawing.Color.Black;
            this.LblSLL.Location = new System.Drawing.Point(8, 70);
            this.LblSLL.Name = "LblSLL";
            this.LblSLL.Size = new System.Drawing.Size(96, 16);
            this.LblSLL.TabIndex = 11;
            this.LblSLL.Text = "Sur place :";
            this.LblSLL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LblHTR
            // 
            this.LblHTR.AutoSize = true;
            this.LblHTR.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblHTR.ForeColor = System.Drawing.Color.Black;
            this.LblHTR.Location = new System.Drawing.Point(8, 46);
            this.LblHTR.Name = "LblHTR";
            this.LblHTR.Size = new System.Drawing.Size(120, 16);
            this.LblHTR.TabIndex = 9;
            this.LblHTR.Text = "Acquittement :";
            this.LblHTR.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMedecin
            // 
            this.lblMedecin.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMedecin.ForeColor = System.Drawing.Color.Black;
            this.lblMedecin.Location = new System.Drawing.Point(6, 22);
            this.lblMedecin.Name = "lblMedecin";
            this.lblMedecin.Size = new System.Drawing.Size(282, 24);
            this.lblMedecin.TabIndex = 8;
            this.lblMedecin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.CadetBlue;
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.lbAge);
            this.groupBox1.Controls.Add(this.label70);
            this.groupBox1.Controls.Add(this.lbMotif1);
            this.groupBox1.Controls.Add(this.lbUrgence);
            this.groupBox1.Controls.Add(this.lbMotif2);
            this.groupBox1.Controls.Add(this.lblMotif1);
            this.groupBox1.Controls.Add(this.lblDateAppel);
            this.groupBox1.Controls.Add(this.lblUrgence);
            this.groupBox1.Controls.Add(this.lblMotif2);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(294, 158);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Général";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(240, 105);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 48);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 18;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // lbAge
            // 
            this.lbAge.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAge.ForeColor = System.Drawing.Color.Black;
            this.lbAge.Location = new System.Drawing.Point(48, 135);
            this.lbAge.Name = "lbAge";
            this.lbAge.Size = new System.Drawing.Size(88, 20);
            this.lbAge.TabIndex = 9;
            this.lbAge.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label70
            // 
            this.label70.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label70.ForeColor = System.Drawing.Color.Black;
            this.label70.Location = new System.Drawing.Point(7, 137);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(48, 16);
            this.label70.TabIndex = 8;
            this.label70.Text = "Age :";
            this.label70.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbMotif1
            // 
            this.lbMotif1.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMotif1.ForeColor = System.Drawing.Color.Black;
            this.lbMotif1.Location = new System.Drawing.Point(68, 62);
            this.lbMotif1.Name = "lbMotif1";
            this.lbMotif1.Size = new System.Drawing.Size(220, 17);
            this.lbMotif1.TabIndex = 5;
            this.lbMotif1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbUrgence
            // 
            this.lbUrgence.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUrgence.ForeColor = System.Drawing.Color.Black;
            this.lbUrgence.Location = new System.Drawing.Point(68, 112);
            this.lbUrgence.Name = "lbUrgence";
            this.lbUrgence.Size = new System.Drawing.Size(155, 20);
            this.lbUrgence.TabIndex = 7;
            this.lbUrgence.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbMotif2
            // 
            this.lbMotif2.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMotif2.ForeColor = System.Drawing.Color.Black;
            this.lbMotif2.Location = new System.Drawing.Point(68, 87);
            this.lbMotif2.Name = "lbMotif2";
            this.lbMotif2.Size = new System.Drawing.Size(170, 17);
            this.lbMotif2.TabIndex = 6;
            this.lbMotif2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMotif1
            // 
            this.lblMotif1.AutoSize = true;
            this.lblMotif1.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMotif1.ForeColor = System.Drawing.Color.Black;
            this.lblMotif1.Location = new System.Drawing.Point(6, 63);
            this.lblMotif1.Name = "lblMotif1";
            this.lblMotif1.Size = new System.Drawing.Size(80, 16);
            this.lblMotif1.TabIndex = 2;
            this.lblMotif1.Text = "Motif 1 :";
            this.lblMotif1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDateAppel
            // 
            this.lblDateAppel.BackColor = System.Drawing.Color.CadetBlue;
            this.lblDateAppel.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateAppel.ForeColor = System.Drawing.Color.Black;
            this.lblDateAppel.Location = new System.Drawing.Point(6, 19);
            this.lblDateAppel.Name = "lblDateAppel";
            this.lblDateAppel.Size = new System.Drawing.Size(282, 37);
            this.lblDateAppel.TabIndex = 1;
            this.lblDateAppel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUrgence
            // 
            this.lblUrgence.AutoSize = true;
            this.lblUrgence.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUrgence.ForeColor = System.Drawing.Color.Black;
            this.lblUrgence.Location = new System.Drawing.Point(3, 113);
            this.lblUrgence.Name = "lblUrgence";
            this.lblUrgence.Size = new System.Drawing.Size(80, 16);
            this.lblUrgence.TabIndex = 4;
            this.lblUrgence.Text = "Urgence :";
            this.lblUrgence.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMotif2
            // 
            this.lblMotif2.AutoSize = true;
            this.lblMotif2.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMotif2.ForeColor = System.Drawing.Color.Black;
            this.lblMotif2.Location = new System.Drawing.Point(6, 88);
            this.lblMotif2.Name = "lblMotif2";
            this.lblMotif2.Size = new System.Drawing.Size(80, 16);
            this.lblMotif2.TabIndex = 3;
            this.lblMotif2.Text = "Motif 2 :";
            this.lblMotif2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnEchangeMedicall);
            this.panel5.Controls.Add(this.btnFermeture);
            this.panel5.Controls.Add(this.btnParametrages);
            this.panel5.Location = new System.Drawing.Point(6, 60);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(269, 53);
            this.panel5.TabIndex = 5;
            // 
            // btnEchangeMedicall
            // 
            this.btnEchangeMedicall.BackColor = System.Drawing.Color.CadetBlue;
            this.btnEchangeMedicall.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnEchangeMedicall.BackgroundImage")));
            this.btnEchangeMedicall.FlatAppearance.BorderSize = 0;
            this.btnEchangeMedicall.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEchangeMedicall.Location = new System.Drawing.Point(6, 0);
            this.btnEchangeMedicall.Margin = new System.Windows.Forms.Padding(0);
            this.btnEchangeMedicall.Name = "btnEchangeMedicall";
            this.btnEchangeMedicall.Size = new System.Drawing.Size(155, 51);
            this.btnEchangeMedicall.TabIndex = 28;
            this.btnEchangeMedicall.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEchangeMedicall.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEchangeMedicall.UseVisualStyleBackColor = false;
            this.btnEchangeMedicall.Click += new System.EventHandler(this.btnEchangeMedicall_Click);
            // 
            // btnFermeture
            // 
            this.btnFermeture.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnFermeture.BackgroundImage")));
            this.btnFermeture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnFermeture.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFermeture.Location = new System.Drawing.Point(224, 7);
            this.btnFermeture.Margin = new System.Windows.Forms.Padding(0);
            this.btnFermeture.Name = "btnFermeture";
            this.btnFermeture.Size = new System.Drawing.Size(45, 44);
            this.btnFermeture.TabIndex = 12;
            this.toolTip1.SetToolTip(this.btnFermeture, "Quitter l\'application");
            this.btnFermeture.UseVisualStyleBackColor = true;
            this.btnFermeture.Click += new System.EventHandler(this.btnFermeture_Click);
            // 
            // btnParametrages
            // 
            this.btnParametrages.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnParametrages.BackgroundImage")));
            this.btnParametrages.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnParametrages.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnParametrages.Location = new System.Drawing.Point(178, 8);
            this.btnParametrages.Name = "btnParametrages";
            this.btnParametrages.Size = new System.Drawing.Size(42, 42);
            this.btnParametrages.TabIndex = 0;
            this.btnParametrages.UseVisualStyleBackColor = true;
            this.btnParametrages.Click += new System.EventHandler(this.btnParametrages_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.CadetBlue;
            this.groupBox4.Controls.Add(this.fpFiche_Historique);
            this.groupBox4.Location = new System.Drawing.Point(4, 624);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(300, 103);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Historique ";
            // 
            // fpFiche_Historique
            // 
            this.fpFiche_Historique.AccessibleDescription = "";
            this.fpFiche_Historique.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.fpFiche_Historique.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
            this.fpFiche_Historique.Location = new System.Drawing.Point(5, 19);
            this.fpFiche_Historique.Name = "fpFiche_Historique";
            this.fpFiche_Historique.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpFiche_Historique_Sheet1});
            this.fpFiche_Historique.Size = new System.Drawing.Size(286, 76);
            this.fpFiche_Historique.TabIndex = 0;
            this.fpFiche_Historique.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpFiche_Historique.MouseMove += new System.Windows.Forms.MouseEventHandler(this.fpFiche_Historique_MouseMove);
            // 
            // fpFiche_Historique_Sheet1
            // 
            this.fpFiche_Historique_Sheet1.Reset();
            this.fpFiche_Historique_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpFiche_Historique_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpFiche_Historique_Sheet1.ColumnHeader.Visible = false;
            this.fpFiche_Historique_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.fpFiche_Historique_Sheet1.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.None);
            this.fpFiche_Historique_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.ReadOnly;
            this.fpFiche_Historique_Sheet1.RowHeader.Visible = false;
            this.fpFiche_Historique_Sheet1.VerticalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.None);
            this.fpFiche_Historique_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // lblConnecte
            // 
            this.lblConnecte.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConnecte.ForeColor = System.Drawing.Color.DarkRed;
            this.lblConnecte.Location = new System.Drawing.Point(12, 26);
            this.lblConnecte.Name = "lblConnecte";
            this.lblConnecte.Size = new System.Drawing.Size(261, 20);
            this.lblConnecte.TabIndex = 13;
            this.lblConnecte.Text = "Non connecté(e)";
            this.lblConnecte.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tab
            // 
            this.tab.Controls.Add(this.tbTraitement);
            this.tab.Controls.Add(this.tbRecherche);
            this.tab.Location = new System.Drawing.Point(3, 119);
            this.tab.Name = "tab";
            this.tab.SelectedIndex = 0;
            this.tab.Size = new System.Drawing.Size(408, 190);
            this.tab.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tab.TabIndex = 4;
            // 
            // tbTraitement
            // 
            this.tbTraitement.BackColor = System.Drawing.Color.CadetBlue;
            this.tbTraitement.Controls.Add(this.btnVoirAppels);
            this.tbTraitement.Controls.Add(this.txtRechercheRapport);
            this.tbTraitement.Controls.Add(this.chkRapport);
            this.tbTraitement.Controls.Add(this.txtRechercheIndex);
            this.tbTraitement.Controls.Add(this.chkIndex);
            this.tbTraitement.Controls.Add(this.chkOrigine);
            this.tbTraitement.Controls.Add(this.cbOrigine);
            this.tbTraitement.Controls.Add(this.chkDate);
            this.tbTraitement.Controls.Add(this.chkMotif);
            this.tbTraitement.Controls.Add(this.ChkMedecin);
            this.tbTraitement.Controls.Add(this.dateTimePicker2);
            this.tbTraitement.Controls.Add(this.dateTimePicker1);
            this.tbTraitement.Controls.Add(this.cbMotif);
            this.tbTraitement.Controls.Add(this.cbMedecin);
            this.tbTraitement.Controls.Add(this.label6);
            this.tbTraitement.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTraitement.ForeColor = System.Drawing.Color.Black;
            this.tbTraitement.Location = new System.Drawing.Point(4, 22);
            this.tbTraitement.Name = "tbTraitement";
            this.tbTraitement.Size = new System.Drawing.Size(400, 164);
            this.tbTraitement.TabIndex = 1;
            this.tbTraitement.Text = "Recherche";
            // 
            // btnVoirAppels
            // 
            this.btnVoirAppels.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnVoirAppels.BackgroundImage")));
            this.btnVoirAppels.FlatAppearance.BorderSize = 0;
            this.btnVoirAppels.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVoirAppels.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVoirAppels.ForeColor = System.Drawing.Color.Black;
            this.btnVoirAppels.Location = new System.Drawing.Point(315, 42);
            this.btnVoirAppels.Name = "btnVoirAppels";
            this.btnVoirAppels.Size = new System.Drawing.Size(80, 80);
            this.btnVoirAppels.TabIndex = 27;
            this.btnVoirAppels.Text = "Voir les appels";
            this.btnVoirAppels.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnVoirAppels.UseVisualStyleBackColor = true;
            this.btnVoirAppels.Click += new System.EventHandler(this.btnVoirAppels_Click);
            // 
            // txtRechercheRapport
            // 
            this.txtRechercheRapport.Location = new System.Drawing.Point(106, 138);
            this.txtRechercheRapport.Name = "txtRechercheRapport";
            this.txtRechercheRapport.Size = new System.Drawing.Size(190, 20);
            this.txtRechercheRapport.TabIndex = 26;
            this.txtRechercheRapport.TextChanged += new System.EventHandler(this.txtRechercheRapport_TextChanged);
            // 
            // chkRapport
            // 
            this.chkRapport.Location = new System.Drawing.Point(3, 138);
            this.chkRapport.Name = "chkRapport";
            this.chkRapport.Size = new System.Drawing.Size(74, 21);
            this.chkRapport.TabIndex = 25;
            this.chkRapport.Text = "Rapport";
            // 
            // txtRechercheIndex
            // 
            this.txtRechercheIndex.Location = new System.Drawing.Point(106, 112);
            this.txtRechercheIndex.Name = "txtRechercheIndex";
            this.txtRechercheIndex.Size = new System.Drawing.Size(190, 20);
            this.txtRechercheIndex.TabIndex = 24;
            this.txtRechercheIndex.TextChanged += new System.EventHandler(this.txtRechercheIndex_TextChanged);
            // 
            // chkIndex
            // 
            this.chkIndex.Location = new System.Drawing.Point(3, 116);
            this.chkIndex.Name = "chkIndex";
            this.chkIndex.Size = new System.Drawing.Size(100, 16);
            this.chkIndex.TabIndex = 23;
            this.chkIndex.Text = "Consultation";
            // 
            // chkOrigine
            // 
            this.chkOrigine.Location = new System.Drawing.Point(3, 90);
            this.chkOrigine.Name = "chkOrigine";
            this.chkOrigine.Size = new System.Drawing.Size(71, 24);
            this.chkOrigine.TabIndex = 19;
            this.chkOrigine.Text = "Origine";
            // 
            // cbOrigine
            // 
            this.cbOrigine.Location = new System.Drawing.Point(80, 90);
            this.cbOrigine.Name = "cbOrigine";
            this.cbOrigine.Size = new System.Drawing.Size(216, 21);
            this.cbOrigine.TabIndex = 18;
            this.cbOrigine.SelectedIndexChanged += new System.EventHandler(this.cbOrigine_SelectedIndexChanged);
            // 
            // chkDate
            // 
            this.chkDate.Location = new System.Drawing.Point(3, 67);
            this.chkDate.Name = "chkDate";
            this.chkDate.Size = new System.Drawing.Size(64, 16);
            this.chkDate.TabIndex = 17;
            this.chkDate.Text = "Date";
            // 
            // chkMotif
            // 
            this.chkMotif.Location = new System.Drawing.Point(4, 42);
            this.chkMotif.Name = "chkMotif";
            this.chkMotif.Size = new System.Drawing.Size(61, 16);
            this.chkMotif.TabIndex = 16;
            this.chkMotif.Text = "Motif";
            // 
            // ChkMedecin
            // 
            this.ChkMedecin.Location = new System.Drawing.Point(4, 21);
            this.ChkMedecin.Name = "ChkMedecin";
            this.ChkMedecin.Size = new System.Drawing.Size(75, 15);
            this.ChkMedecin.TabIndex = 15;
            this.ChkMedecin.Text = "Médecin";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker2.Location = new System.Drawing.Point(206, 63);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(91, 20);
            this.dateTimePicker2.TabIndex = 14;
            this.dateTimePicker2.ValueChanged += new System.EventHandler(this.dateTimePicker2_ValueChanged);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(80, 64);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(90, 20);
            this.dateTimePicker1.TabIndex = 13;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // cbMotif
            // 
            this.cbMotif.Location = new System.Drawing.Point(80, 37);
            this.cbMotif.Name = "cbMotif";
            this.cbMotif.Size = new System.Drawing.Size(216, 21);
            this.cbMotif.TabIndex = 12;
            this.cbMotif.SelectedIndexChanged += new System.EventHandler(this.cbMotif_SelectedIndexChanged);
            // 
            // cbMedecin
            // 
            this.cbMedecin.Location = new System.Drawing.Point(80, 15);
            this.cbMedecin.Name = "cbMedecin";
            this.cbMedecin.Size = new System.Drawing.Size(216, 21);
            this.cbMedecin.TabIndex = 11;
            this.cbMedecin.SelectedIndexChanged += new System.EventHandler(this.cbMedecin_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(91, 1);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(159, 18);
            this.label6.TabIndex = 10;
            this.label6.Text = "Appels sur critère :";
            // 
            // tbRecherche
            // 
            this.tbRecherche.BackColor = System.Drawing.Color.CadetBlue;
            this.tbRecherche.Controls.Add(this.btnRechercher);
            this.tbRecherche.Controls.Add(this.txtRecherchePrenom);
            this.tbRecherche.Controls.Add(this.TxtFiltre3);
            this.tbRecherche.Controls.Add(this.TxtFiltre2);
            this.tbRecherche.Controls.Add(this.TxtFiltre1);
            this.tbRecherche.Controls.Add(this.ChkFiltre3);
            this.tbRecherche.Controls.Add(this.ChkFiltre2);
            this.tbRecherche.Controls.Add(this.ChkFiltre1);
            this.tbRecherche.Controls.Add(this.label50);
            this.tbRecherche.Controls.Add(this.label48);
            this.tbRecherche.Controls.Add(this.label47);
            this.tbRecherche.Controls.Add(this.label46);
            this.tbRecherche.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbRecherche.Location = new System.Drawing.Point(4, 22);
            this.tbRecherche.Name = "tbRecherche";
            this.tbRecherche.Size = new System.Drawing.Size(400, 164);
            this.tbRecherche.TabIndex = 4;
            this.tbRecherche.Text = "Avancé";
            // 
            // btnRechercher
            // 
            this.btnRechercher.Location = new System.Drawing.Point(281, 84);
            this.btnRechercher.Name = "btnRechercher";
            this.btnRechercher.Size = new System.Drawing.Size(80, 26);
            this.btnRechercher.TabIndex = 15;
            this.btnRechercher.Text = "Rechercher";
            this.btnRechercher.UseVisualStyleBackColor = true;
            this.btnRechercher.Click += new System.EventHandler(this.btnRechercher_Click);
            // 
            // txtRecherchePrenom
            // 
            this.txtRecherchePrenom.Location = new System.Drawing.Point(226, 55);
            this.txtRecherchePrenom.Name = "txtRecherchePrenom";
            this.txtRecherchePrenom.Size = new System.Drawing.Size(108, 20);
            this.txtRecherchePrenom.TabIndex = 11;
            // 
            // TxtFiltre3
            // 
            this.TxtFiltre3.Location = new System.Drawing.Point(158, 85);
            this.TxtFiltre3.Name = "TxtFiltre3";
            this.TxtFiltre3.Size = new System.Drawing.Size(108, 20);
            this.TxtFiltre3.TabIndex = 12;
            this.TxtFiltre3.TextChanged += new System.EventHandler(this.TxtFiltre3_TextChanged);
            // 
            // TxtFiltre2
            // 
            this.TxtFiltre2.Location = new System.Drawing.Point(105, 55);
            this.TxtFiltre2.Name = "TxtFiltre2";
            this.TxtFiltre2.Size = new System.Drawing.Size(108, 20);
            this.TxtFiltre2.TabIndex = 10;
            this.TxtFiltre2.TextChanged += new System.EventHandler(this.TxtFiltre2_TextChanged);
            // 
            // TxtFiltre1
            // 
            this.TxtFiltre1.Location = new System.Drawing.Point(105, 28);
            this.TxtFiltre1.Name = "TxtFiltre1";
            this.TxtFiltre1.Size = new System.Drawing.Size(108, 20);
            this.TxtFiltre1.TabIndex = 9;
            this.TxtFiltre1.TextChanged += new System.EventHandler(this.TxtFiltre1_TextChanged);
            // 
            // ChkFiltre3
            // 
            this.ChkFiltre3.Location = new System.Drawing.Point(6, 84);
            this.ChkFiltre3.Name = "ChkFiltre3";
            this.ChkFiltre3.Size = new System.Drawing.Size(18, 21);
            this.ChkFiltre3.TabIndex = 8;
            this.ChkFiltre3.Text = "checkBox4";
            // 
            // ChkFiltre2
            // 
            this.ChkFiltre2.Location = new System.Drawing.Point(7, 59);
            this.ChkFiltre2.Name = "ChkFiltre2";
            this.ChkFiltre2.Size = new System.Drawing.Size(17, 16);
            this.ChkFiltre2.TabIndex = 6;
            this.ChkFiltre2.Text = "checkBox2";
            // 
            // ChkFiltre1
            // 
            this.ChkFiltre1.Location = new System.Drawing.Point(7, 30);
            this.ChkFiltre1.Name = "ChkFiltre1";
            this.ChkFiltre1.Size = new System.Drawing.Size(17, 16);
            this.ChkFiltre1.TabIndex = 5;
            this.ChkFiltre1.Text = "checkBox1";
            // 
            // label50
            // 
            this.label50.Location = new System.Drawing.Point(30, 88);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(122, 17);
            this.label50.TabIndex = 4;
            this.label50.Text = "Date de naissance :";
            // 
            // label48
            // 
            this.label48.Location = new System.Drawing.Point(27, 60);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(65, 15);
            this.label48.TabIndex = 2;
            this.label48.Text = "Nom :";
            // 
            // label47
            // 
            this.label47.Location = new System.Drawing.Point(27, 29);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(83, 17);
            this.label47.TabIndex = 1;
            this.label47.Text = "Téléphone :";
            // 
            // label46
            // 
            this.label46.Location = new System.Drawing.Point(16, 9);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(225, 16);
            this.label46.TabIndex = 0;
            this.label46.Text = "Recherche sur le patient :";
            this.label46.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnOnglet3
            // 
            this.btnOnglet3.Image = ((System.Drawing.Image)(resources.GetObject("btnOnglet3.Image")));
            this.btnOnglet3.Location = new System.Drawing.Point(366, 6);
            this.btnOnglet3.Name = "btnOnglet3";
            this.btnOnglet3.Size = new System.Drawing.Size(45, 106);
            this.btnOnglet3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnOnglet3.TabIndex = 12;
            this.btnOnglet3.TabStop = false;
            this.btnOnglet3.Click += new System.EventHandler(this.btnOngletTravail);
            // 
            // btnOnglet2
            // 
            this.btnOnglet2.Image = ((System.Drawing.Image)(resources.GetObject("btnOnglet2.Image")));
            this.btnOnglet2.Location = new System.Drawing.Point(366, 322);
            this.btnOnglet2.Name = "btnOnglet2";
            this.btnOnglet2.Size = new System.Drawing.Size(45, 106);
            this.btnOnglet2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnOnglet2.TabIndex = 11;
            this.btnOnglet2.TabStop = false;
            this.btnOnglet2.Click += new System.EventHandler(this.btnOngletTravail);
            // 
            // btnOnglet1
            // 
            this.btnOnglet1.Image = ((System.Drawing.Image)(resources.GetObject("btnOnglet1.Image")));
            this.btnOnglet1.Location = new System.Drawing.Point(365, 429);
            this.btnOnglet1.Name = "btnOnglet1";
            this.btnOnglet1.Size = new System.Drawing.Size(46, 106);
            this.btnOnglet1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnOnglet1.TabIndex = 10;
            this.btnOnglet1.TabStop = false;
            this.btnOnglet1.Click += new System.EventHandler(this.btnOngletTravail);
            // 
            // lblRaisonSociale
            // 
            this.lblRaisonSociale.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRaisonSociale.ForeColor = System.Drawing.Color.DarkRed;
            this.lblRaisonSociale.Location = new System.Drawing.Point(11, 6);
            this.lblRaisonSociale.Name = "lblRaisonSociale";
            this.lblRaisonSociale.Size = new System.Drawing.Size(264, 20);
            this.lblRaisonSociale.TabIndex = 1;
            this.lblRaisonSociale.Text = "Bienvenue SOS Médecins Genève";
            this.lblRaisonSociale.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuFichier,
            this.mnuDonnees,
            this.mnuFiches,
            this.mnuTA,
            this.mnuRapports,
            this.mnuStatistiques,
            this.menuItem19,
            this.mnuFacturation,
            this.mnuFenetres,
            this.mnuAide});
            // 
            // mnuFichier
            // 
            this.mnuFichier.Index = 0;
            this.mnuFichier.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuParametres,
            this.mnuQuitter});
            this.mnuFichier.Text = "Fichier";
            // 
            // mnuParametres
            // 
            this.mnuParametres.Index = 0;
            this.mnuParametres.Text = "Paramètres";
            this.mnuParametres.Click += new System.EventHandler(this.mnuParametres_Click);
            // 
            // mnuQuitter
            // 
            this.mnuQuitter.Index = 1;
            this.mnuQuitter.Text = "Quitter";
            this.mnuQuitter.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // mnuDonnees
            // 
            this.mnuDonnees.Index = 1;
            this.mnuDonnees.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuImportEpos});
            this.mnuDonnees.Text = "Données";
            // 
            // menuImportEpos
            // 
            this.menuImportEpos.Index = 0;
            this.menuImportEpos.Text = "Import données de la régulation";
            this.menuImportEpos.Click += new System.EventHandler(this.menuImportEpos_Click);
            // 
            // mnuFiches
            // 
            this.mnuFiches.Index = 2;
            this.mnuFiches.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuFip,
            this.mnuMedTT,
            this.mnuCollabo,
            this.menuItem8,
            this.mnuUtilisateurs,
            this.menuItem17});
            this.mnuFiches.Text = "Fiches";
            // 
            // mnuFip
            // 
            this.mnuFip.Index = 0;
            this.mnuFip.Text = "FIP";
            this.mnuFip.Click += new System.EventHandler(this.mnuFip_Click);
            // 
            // mnuMedTT
            // 
            this.mnuMedTT.Index = 1;
            this.mnuMedTT.Text = "Médecins et permanances/Groupes Médicaux";
            this.mnuMedTT.Click += new System.EventHandler(this.mnuMedTT_Click);
            // 
            // mnuCollabo
            // 
            this.mnuCollabo.Index = 2;
            this.mnuCollabo.Text = "Médecins SOS";
            this.mnuCollabo.Click += new System.EventHandler(this.mnuCollabo_Click);
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 3;
            this.menuItem8.Text = "Assurances";
            this.menuItem8.Click += new System.EventHandler(this.menuItem8_Click);
            // 
            // mnuUtilisateurs
            // 
            this.mnuUtilisateurs.Index = 4;
            this.mnuUtilisateurs.Text = "Utilisateurs";
            this.mnuUtilisateurs.Click += new System.EventHandler(this.mnuUtilisateurs_Click);
            // 
            // menuItem17
            // 
            this.menuItem17.Index = 5;
            this.menuItem17.Text = "Ajout médecin";
            this.menuItem17.Click += new System.EventHandler(this.menuItem17_Click);
            // 
            // mnuTA
            // 
            this.mnuTA.Index = 3;
            this.mnuTA.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem9,
            this.menuItem10,
            this.menuItem12});
            this.mnuTA.Text = "Télé-Alarme";
            // 
            // menuItem9
            // 
            this.menuItem9.Index = 0;
            this.menuItem9.Text = "FIP TA";
            this.menuItem9.Click += new System.EventHandler(this.menuItem9_Click);
            // 
            // menuItem10
            // 
            this.menuItem10.Index = 1;
            this.menuItem10.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem13,
            this.menuAttestationTA,
            this.menuItem14});
            this.menuItem10.Text = "Facturation";
            // 
            // menuItem13
            // 
            this.menuItem13.Index = 0;
            this.menuItem13.Text = "Opérations diverses sur les factures";
            this.menuItem13.Click += new System.EventHandler(this.menuItem13_Click);
            // 
            // menuAttestationTA
            // 
            this.menuAttestationTA.Index = 1;
            this.menuAttestationTA.Text = "Attestation TA";
            this.menuAttestationTA.Click += new System.EventHandler(this.menuAttestationTA_Click);
            // 
            // menuItem14
            // 
            this.menuItem14.Index = 2;
            this.menuItem14.Text = "Gestion du matériel";
            this.menuItem14.Click += new System.EventHandler(this.menuItem14_Click);
            // 
            // menuItem12
            // 
            this.menuItem12.Index = 2;
            this.menuItem12.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.TA_Abonnement});
            this.menuItem12.Text = "Gestion TA";
            // 
            // TA_Abonnement
            // 
            this.TA_Abonnement.Index = 0;
            this.TA_Abonnement.Text = "Nouvel abonnement";
            this.TA_Abonnement.Click += new System.EventHandler(this.TA_Abonnement_Click_1);
            // 
            // mnuRapports
            // 
            this.mnuRapports.Index = 4;
            this.mnuRapports.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem4,
            this.menuItem5,
            this.menuItem2,
            this.menuItem3,
            this.menuItem1});
            this.mnuRapports.Text = "Rapports";
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 0;
            this.menuItem4.Text = "Liste Sans Rapport";
            this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 1;
            this.menuItem5.Text = "Liste Rapport a corriger";
            this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 2;
            this.menuItem2.Text = "Liste Rapport a viser";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click_1);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 3;
            this.menuItem3.Text = "Liste Rapport a reprendre";
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 4;
            this.menuItem1.Text = "Liste Rapport a envoyer";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click_1);
            // 
            // mnuStatistiques
            // 
            this.mnuStatistiques.Index = 5;
            this.mnuStatistiques.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem22,
            this.menuItem21,
            this.menuItem16});
            this.mnuStatistiques.Text = "Statistiques";
            // 
            // menuItem22
            // 
            this.menuItem22.Index = 0;
            this.menuItem22.Text = "Stats 144";
            this.menuItem22.Click += new System.EventHandler(this.menuItem22_Click);
            // 
            // menuItem21
            // 
            this.menuItem21.Index = 1;
            this.menuItem21.Text = "Autres Stats";
            this.menuItem21.Click += new System.EventHandler(this.menuItem21_Click);
            // 
            // menuItem16
            // 
            this.menuItem16.Index = 2;
            this.menuItem16.Text = "Materiels";
            this.menuItem16.Click += new System.EventHandler(this.menuItem16_Click);
            // 
            // menuItem19
            // 
            this.menuItem19.Index = 6;
            this.menuItem19.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem20,
            this.menuSalairesMed});
            this.menuItem19.Text = "Compta";
            // 
            // menuItem20
            // 
            this.menuItem20.Enabled = false;
            this.menuItem20.Index = 0;
            this.menuItem20.Text = "Factures";
            this.menuItem20.Visible = false;
            this.menuItem20.Click += new System.EventHandler(this.menuItem20_Click);
            // 
            // menuSalairesMed
            // 
            this.menuSalairesMed.Index = 1;
            this.menuSalairesMed.Text = "Salaires Médecins";
            this.menuSalairesMed.Click += new System.EventHandler(this.menuSalairesMed_Click);
            // 
            // mnuFacturation
            // 
            this.mnuFacturation.Index = 7;
            this.mnuFacturation.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuFactures,
            this.mnuDocJoint,
            this.mnuRechercheFacture,
            this.mnuFac_Impression,
            this.menuItem6,
            this.menuItem11,
            this.mnuFacturation_Etats,
            this.menuFacturesImpayees});
            this.mnuFacturation.Text = "Facturation";
            // 
            // mnuFactures
            // 
            this.mnuFactures.Index = 0;
            this.mnuFactures.Text = "Factures";
            this.mnuFactures.Click += new System.EventHandler(this.mnuFactures_Click);
            // 
            // mnuDocJoint
            // 
            this.mnuDocJoint.Index = 1;
            this.mnuDocJoint.Text = "Documents Joints";
            this.mnuDocJoint.Click += new System.EventHandler(this.mnuDocJoint_Click);
            // 
            // mnuRechercheFacture
            // 
            this.mnuRechercheFacture.Index = 2;
            this.mnuRechercheFacture.Text = "Recherche de factures";
            this.mnuRechercheFacture.Click += new System.EventHandler(this.mnuRechercheFacture_Click);
            // 
            // mnuFac_Impression
            // 
            this.mnuFac_Impression.Index = 3;
            this.mnuFac_Impression.Text = "Impression des factures";
            this.mnuFac_Impression.Click += new System.EventHandler(this.mnuFac_Impression_Click);
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 4;
            this.menuItem6.Text = "Encaissement";
            this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
            // 
            // menuItem11
            // 
            this.menuItem11.Enabled = false;
            this.menuItem11.Index = 5;
            this.menuItem11.Text = "Factures Par Medecins";
            this.menuItem11.Visible = false;
            this.menuItem11.Click += new System.EventHandler(this.menuItem11_Click);
            // 
            // mnuFacturation_Etats
            // 
            this.mnuFacturation_Etats.Index = 6;
            this.mnuFacturation_Etats.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuFacturation_Etats_Relance,
            this.mnuFacturation_Etats_Relance_Assurances,
            this.mnuFacturation_Etats_VerificationSolde,
            this.mnuFacturation_Etats_Arrangement,
            this.menuItem15});
            this.mnuFacturation_Etats.Text = "Etats";
            // 
            // mnuFacturation_Etats_Relance
            // 
            this.mnuFacturation_Etats_Relance.Index = 0;
            this.mnuFacturation_Etats_Relance.Text = "Relance";
            this.mnuFacturation_Etats_Relance.Click += new System.EventHandler(this.mnuFacturation_Etats_Relance_Click);
            // 
            // mnuFacturation_Etats_Relance_Assurances
            // 
            this.mnuFacturation_Etats_Relance_Assurances.Index = 1;
            this.mnuFacturation_Etats_Relance_Assurances.Text = "Relance Assurances";
            this.mnuFacturation_Etats_Relance_Assurances.Click += new System.EventHandler(this.mnuFacturation_Etats_Relance_Assurances_Click);
            // 
            // mnuFacturation_Etats_VerificationSolde
            // 
            this.mnuFacturation_Etats_VerificationSolde.Index = 2;
            this.mnuFacturation_Etats_VerificationSolde.Text = "Vérification des soldes en fonction des encaissements";
            this.mnuFacturation_Etats_VerificationSolde.Click += new System.EventHandler(this.mnuFacturation_Etats_VerificationSolde_Click);
            // 
            // mnuFacturation_Etats_Arrangement
            // 
            this.mnuFacturation_Etats_Arrangement.Index = 3;
            this.mnuFacturation_Etats_Arrangement.Text = "Liste des arrangements";
            this.mnuFacturation_Etats_Arrangement.Click += new System.EventHandler(this.mnuFacturation_Etats_Arrangement_Click);
            // 
            // menuItem15
            // 
            this.menuItem15.Index = 4;
            this.menuItem15.Text = "Bulletins multiples arrangements";
            this.menuItem15.Click += new System.EventHandler(this.menuItem15_Click);
            // 
            // menuFacturesImpayees
            // 
            this.menuFacturesImpayees.Index = 7;
            this.menuFacturesImpayees.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuListe2emeRappel,
            this.menuPoursuite});
            this.menuFacturesImpayees.Text = "Factures impayées";
            // 
            // menuListe2emeRappel
            // 
            this.menuListe2emeRappel.Index = 0;
            this.menuListe2emeRappel.Text = "Liste 2ème rappel";
            this.menuListe2emeRappel.Click += new System.EventHandler(this.menuListe2emeRappel_Click);
            // 
            // menuPoursuite
            // 
            this.menuPoursuite.Index = 1;
            this.menuPoursuite.Text = "Mise en poursuite";
            this.menuPoursuite.Click += new System.EventHandler(this.menuPoursuite_Click);
            // 
            // mnuFenetres
            // 
            this.mnuFenetres.Index = 8;
            this.mnuFenetres.Text = "Fenetres";
            this.mnuFenetres.Visible = false;
            // 
            // mnuAide
            // 
            this.mnuAide.Index = 9;
            this.mnuAide.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem7});
            this.mnuAide.Text = "?";
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 0;
            this.menuItem7.Text = "A propos";
            this.menuItem7.Click += new System.EventHandler(this.menuItem7_Click);
            // 
            // panelFond
            // 
            this.panelFond.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFond.Location = new System.Drawing.Point(0, 0);
            this.panelFond.Name = "panelFond";
            this.panelFond.Size = new System.Drawing.Size(1584, 888);
            this.panelFond.TabIndex = 3;
            // 
            // frmGeneral
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.ClientSize = new System.Drawing.Size(1584, 888);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelFond);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.Name = "frmGeneral";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Smart Rapport V5";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmGeneral_FormClosing);
            this.Load += new System.EventHandler(this.frmGeneral_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmGeneral_KeyDown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpAppels)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpAppels_Sheet1)).EndInit();
            this.tabTravail.ResumeLayout(false);
            this.tbFiche.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.pan_Dynamique.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.pan_Patient.ResumeLayout(false);
            this.pan_Patient.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PBoxAudio)).EndInit();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSaveFiche)).EndInit();
            this.tbRapport.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vlcControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tBarTps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tBarVol)).EndInit();
            this.panel9.ResumeLayout(false);
            this.TabActionRapport.ResumeLayout(false);
            this.tbCreation.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picRapport_OptSans)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRapport_OptConstat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRapport_OptRapport)).EndInit();
            this.tbVisa.ResumeLayout(false);
            this.tbVisa.PerformLayout();
            this.tbCommunication.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpRapport_Destinataires)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpRapport_Destinataires_Sheet1)).EndInit();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picRapport_Export2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRapport_Export1)).EndInit();
            this.groupBox7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picRapport_Mail2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRapport_Mail1)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picRapport_Print2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRapport_Print1)).EndInit();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_ValideRapport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpRapport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpRapport_Sheet1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBFiches)).EndInit();
            this.pan_Statiques.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpFiche_Historique)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpFiche_Historique_Sheet1)).EndInit();
            this.tab.ResumeLayout(false);
            this.tbTraitement.ResumeLayout(false);
            this.tbTraitement.PerformLayout();
            this.tbRecherche.ResumeLayout(false);
            this.tbRecherche.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnglet3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnglet2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnglet1)).EndInit();
            this.ResumeLayout(false);

		}
        #endregion

        #region Recuperation des consultations

		// on récupere les consultations par rapport à l'index d'appel  (Domi 13.10.2017 ajout Email ci dessous x5)
        private DataSet RecuperationConsultationByAppel(long Num)
        {
            DataSet ds = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet("SELECT c.CodeAppel,m.Nom as 'NomMedecinSos',c.Modifie,c.RapportGenere,c.FactureGeneree,c.Deces, c.Esp," +
                " a.Num,c.IndicePatient,a.Tel,a.DAP,a.DTR,a.DRC,a.DSL,a.DFI,a.CodeMotif1,a.Urgence,a.DelaiIndique,a.CommentaireTransmis,a.CommentaireFichier,a.AnnulationAppel," +
                " a.DAN,a.MotifAnnulation,a.DevenirAnnulation,a.ComplementInfo,a.Motif1,a.Motif2,a.OrigineAppel,a.CodeIntervenant,pa.IdPersonne,pa.SuiviPatient,pa.IdAbonnement," +
                " pa.TypeAbonnement,pa.TexteAbonnement,pe.Nom as 'NomPatient' ,pe.Prenom as 'PrenomPatient',pe.Tel as 'TelPatient',pe.NumAdresse,pe.CodePostal,pe.Departement," +
                " pe.Commune,pe.Rue,pe.NumeroDansRue,pe.Batiment,pe.Etage,pe.Escalier,pe.Digicode,pe.Internom,pe.Porte,pe.longitude,pe.Latitude,pe.DateNaissance,pe.sexe,pe.Age," +
                " pe.UniteAge,pe.TexteSup,pe.ListeNoire,pe.Adm_Batiment,pe.Adm_Commune,pe.Adm_NumeroDansRue,pe.Adm_Rue,pe.Adm_CodePostal, pe.Num_Assure, pe.Num_AVS, pe.Email," +
                " c.NConsultation,c.Diag1,c.Diag2,c.Hono,c.Reglement,c.Actes,c.Devenir,c.PriseEnChargePatient,c.LibCisp,c.Traitements,c.TraitementLibre,c.Gestes," +
                " c.CommentaireLibre,c.EnvoiDocument,c.ListeIndexServiceExt,c.ListeIndexMt,c.TensionHaute,c.TensionBasse,c.Temperature,c.O2,c.Pulsations,f.FaxDestinataire," +
                " f.FaxType, f.FaxCommentaire, ca.CommentaireAppel  " +
                " FROM tableactes a left join tableconsultations c on c.CodeAppel = a.Num " +
                "                   left join tablefaxfsasd f on f.FaxConsultation = c.NConsultation " +
                "                   left join tablemedecin m on m.CodeIntervenant = a.CodeIntervenant " +
                "                   left join tablepatient pa on pa.IdPatient = c.IndicePatient " +
                "                   left join tablepersonne pe on pe.IdPersonne = pa.IdPersonne " +
                "                   left join CommentAppel ca on ca.Num_Appel = c.NConsultation " +
                " Where a.Num = " + Num);
            return ds;
        }
		// Consultation par rapport à son index
        public DataSet RecuperationConsultationByNConsult(long NConsult)
        {
            DataSet ds = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet("SELECT c.CodeAppel,m.Nom as 'NomMedecinSos',c.Modifie,c.RapportGenere,c.FactureGeneree,c.Deces, c.Esp, " +
                " a.Num,c.IndicePatient,a.Tel,a.DAP,a.DTR,a.DRC,a.DSL,a.DFI,a.CodeMotif1,a.Urgence,a.DelaiIndique,a.CommentaireTransmis,a.CommentaireFichier,a.AnnulationAppel," +
                " a.DAN,a.MotifAnnulation,a.DevenirAnnulation,a.ComplementInfo,a.Motif1,a.Motif2,a.OrigineAppel,a.CodeIntervenant,pa.IdPersonne,pa.SuiviPatient,pa.IdAbonnement," +
                " pa.TypeAbonnement,pa.TexteAbonnement,pe.Nom as 'NomPatient' ,pe.Prenom as 'PrenomPatient',pe.Tel as 'TelPatient',pe.NumAdresse,pe.CodePostal,pe.Departement," +
                " pe.Commune,pe.Rue,pe.NumeroDansRue,pe.Batiment,pe.Etage,pe.Escalier,pe.Digicode,pe.Internom,pe.Porte,pe.longitude,pe.Latitude,pe.DateNaissance,pe.sexe,pe.Age," +
                " pe.UniteAge,pe.TexteSup,pe.ListeNoire,pe.Adm_Batiment,pe.Adm_Commune,pe.Adm_NumeroDansRue,pe.Adm_Rue,pe.Adm_CodePostal, pe.Num_Assure, pe.Num_AVS, pe.Email," +
                " c.NConsultation,c.Diag1,c.Diag2,c.Hono,c.Reglement,c.Actes,c.Devenir,c.PriseEnChargePatient,c.LibCisp,c.Traitements,c.TraitementLibre,c.Gestes,c.CommentaireLibre," +
                " c.EnvoiDocument,c.ListeIndexServiceExt,c.ListeIndexMt,c.TensionHaute,c.TensionBasse,c.Temperature,c.O2,c.Pulsations,f.FaxDestinataire, f.FaxType," +
                " f.FaxCommentaire, ca.CommentaireAppel  " +
                " FROM tableactes a left join tableconsultations c on c.CodeAppel = a.Num " +
                "                   left join tablefaxfsasd f on f.FaxConsultation = c.NConsultation " +
                "                   left join tablemedecin m on m.CodeIntervenant = a.CodeIntervenant " +
                "                   left join tablepatient pa on pa.IdPatient = c.IndicePatient " +
                "                   left join tablepersonne pe on pe.IdPersonne = pa.IdPersonne " +
                "                   left join CommentAppel ca on ca.Num_Appel = c.NConsultation " +
                " Where c.NConsultation = " + NConsult);
            return ds;
        }
        //// Toutes les consultations sur une fourchette d'appels
        private DataSet RecuperationConsultationByAppelsRange(long NumDepart, long NumFin)
        {
            DataSet ds = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet("SELECT c.CodeAppel,m.Nom as 'NomMedecinSos',c.Modifie,c.RapportGenere,c.FactureGeneree,c.Deces, c.Esp," +
                " a.Num,c.IndicePatient,a.Tel,a.DAP,a.DTR,a.DRC,a.DSL,a.DFI,a.CodeMotif1,a.Urgence,a.DelaiIndique,a.CommentaireTransmis,a.CommentaireFichier,a.AnnulationAppel," +
                " a.DAN,a.MotifAnnulation,a.DevenirAnnulation,a.ComplementInfo,a.Motif1,a.Motif2,a.OrigineAppel,a.CodeIntervenant,pa.IdPersonne,pa.SuiviPatient,pa.IdAbonnement," +
                " pa.TypeAbonnement,pa.TexteAbonnement,pe.Nom as 'NomPatient' ,pe.Prenom as 'PrenomPatient',pe.Tel as 'TelPatient',pe.NumAdresse,pe.CodePostal,pe.Departement," +
                " pe.Commune,pe.Rue,pe.NumeroDansRue,pe.Batiment,pe.Etage,pe.Escalier,pe.Digicode,pe.Internom,pe.Porte,pe.longitude,pe.Latitude,pe.DateNaissance,pe.sexe,pe.Age," +
                " pe.UniteAge,pe.TexteSup,pe.ListeNoire,pe.Adm_Batiment,pe.Adm_Commune,pe.Adm_NumeroDansRue,pe.Adm_Rue,pe.Adm_CodePostal, pe.Num_Assure, pe.Num_AVS, pe.Email," +
                " c.Diag1,c.Diag2,c.Hono,c.Reglement,c.NConsultation,c.Actes,c.Devenir,c.PriseEnChargePatient,c.LibCisp,c.Traitements,c.TraitementLibre,c.Gestes," +
                " c.CommentaireLibre,c.EnvoiDocument,c.ListeIndexServiceExt,c.ListeIndexMt,c.TensionHaute,c.TensionBasse,c.Temperature,c.O2,c.Pulsations ,f.FaxDestinataire," +
                " f.FaxType, f.FaxCommentaire, ca.CommentaireAppel  " +
                " FROM tableactes a left join tableconsultations c on c.CodeAppel = a.Num" +
                "                   left join tablefaxfsasd f on f.FaxConsultation = c.NConsultation " +
                "                   left join tablemedecin m on m.CodeIntervenant = a.CodeIntervenant " +
                "                   left join tablepatient pa on pa.IdPatient = c.IndicePatient " +
                "                   left join tablepersonne pe on pe.IdPersonne = pa.IdPersonne " +
                "                   left join CommentAppel ca on ca.Num_Appel = c.NConsultation " +
                " Where (a.Num >= " + NumDepart + " and a.Num <= " + NumFin + ")");
            return ds;
        }
        // Toutes les consultations avec filtre arbitraire
        private DataSet RecuperationConsultationByParam(string Param, Boolean p_blnTop)
        {
            DataSet ds;
            if (p_blnTop)
            {
                ds = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet("SELECT TOP(1) c.CodeAppel,m.Nom as 'NomMedecinSos',c.Modifie,c.RapportGenere,c.FactureGeneree,c.Deces," +
                    " c.Esp, a.Num,c.IndicePatient,a.Tel,a.DAP,a.DTR,a.DRC,a.DSL,a.DFI,a.CodeMotif1,a.Urgence,a.DelaiIndique,a.CommentaireTransmis,a.CommentaireFichier," +
                    " a.AnnulationAppel,a.DAN,a.MotifAnnulation,a.DevenirAnnulation,a.ComplementInfo,a.Motif1,a.Motif2,a.OrigineAppel,a.CodeIntervenant,pa.IdPersonne," +
                    " pa.SuiviPatient,pa.IdAbonnement,pa.TypeAbonnement,pa.TexteAbonnement,pe.Nom as 'NomPatient' ,pe.Prenom as 'PrenomPatient',pe.Tel as 'TelPatient'," +
                    " pe.NumAdresse,pe.CodePostal,pe.Departement,pe.Commune,pe.Rue,pe.NumeroDansRue,pe.Batiment,pe.Etage,pe.Escalier,pe.Digicode,pe.Internom,pe.Porte," +
                    " pe.longitude,pe.Latitude,pe.DateNaissance,pe.sexe,pe.Age,pe.UniteAge,pe.TexteSup,pe.ListeNoire,pe.Adm_Batiment,pe.Adm_Commune,pe.Adm_NumeroDansRue," +
                    " pe.Adm_Rue,pe.Adm_CodePostal, pe.Num_Assure, pe.Num_AVS, pe.Email, c.Diag1,c.Diag2,c.Hono,c.Reglement,c.Actes,c.Devenir,c.PriseEnChargePatient," +
                    " c.NConsultation,c.LibCisp,c.Traitements,c.TraitementLibre,c.Gestes,c.CommentaireLibre,c.EnvoiDocument,c.ListeIndexServiceExt,c.ListeIndexMt," +
                    " c.TensionHaute,c.TensionBasse,c.Temperature,c.O2,c.Pulsations ,f.FaxDestinataire, f.FaxType, f.FaxCommentaire, ca.CommentaireAppel  " +
                    " FROM tableactes a left join tableconsultations c on c.CodeAppel = a.Num" +
                    "                   left join tablefaxfsasd f on f.FaxConsultation = c.NConsultation " +
                    "                   left join tablemedecin m on m.CodeIntervenant = a.CodeIntervenant " +
                    "                   left join tablepatient pa on pa.IdPatient = c.IndicePatient " +
                    "                   left join tablepersonne pe on pe.IdPersonne = pa.IdPersonne " +
                    "                   left join CommentAppel ca on ca.Num_Appel = c.NConsultation " + Param);
            }
            else
            {
                ds = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet("SELECT c.CodeAppel,m.Nom as 'NomMedecinSos',c.Modifie,c.RapportGenere,c.FactureGeneree,c.Deces, c.Esp," +
                    " a.Num,c.IndicePatient,a.Tel,a.DAP,a.DTR,a.DRC,a.DSL,a.DFI,a.CodeMotif1,a.Urgence,a.DelaiIndique,a.CommentaireTransmis,a.CommentaireFichier," +
                    " a.AnnulationAppel,a.DAN,a.MotifAnnulation,a.DevenirAnnulation,a.ComplementInfo,a.Motif1,a.Motif2,a.OrigineAppel,a.CodeIntervenant,pa.IdPersonne," +
                    " pa.SuiviPatient,pa.IdAbonnement,pa.TypeAbonnement,pa.TexteAbonnement,pe.Nom as 'NomPatient' ,pe.Prenom as 'PrenomPatient',pe.Tel as 'TelPatient'," +
                    " pe.NumAdresse,pe.CodePostal,pe.Departement,pe.Commune,pe.Rue,pe.NumeroDansRue,pe.Batiment,pe.Etage,pe.Escalier,pe.Digicode,pe.Internom,pe.Porte," +
                    " pe.longitude,pe.Latitude,pe.DateNaissance,pe.sexe,pe.Age,pe.UniteAge,pe.TexteSup,pe.ListeNoire,pe.Adm_Batiment,pe.Adm_Commune,pe.Adm_NumeroDansRue," +
                    " pe.Adm_Rue,pe.Adm_CodePostal, pe.Num_Assure, pe.Num_AVS, pe.Email, c.Diag1,c.Diag2,c.Hono,c.Reglement,c.Actes,c.Devenir,c.PriseEnChargePatient," +
                    " c.NConsultation,c.LibCisp,c.Traitements,c.TraitementLibre,c.Gestes,c.CommentaireLibre,c.EnvoiDocument,c.ListeIndexServiceExt,c.ListeIndexMt," +
                    " c.TensionHaute,c.TensionBasse,c.Temperature,c.O2,c.Pulsations ,f.FaxDestinataire, f.FaxType, f.FaxCommentaire, ca.CommentaireAppel  " +
                    " FROM tableactes a left join tableconsultations c on c.CodeAppel = a.Num" +
                    "                   left join tablefaxfsasd f on f.FaxConsultation = c.NConsultation " +
                    "                   left join tablemedecin m on m.CodeIntervenant = a.CodeIntervenant " +
                    "                   left join tablepatient pa on pa.IdPatient = c.IndicePatient " +
                    "                   left join tablepersonne pe on pe.IdPersonne = pa.IdPersonne " +
                    "                   left join CommentAppel ca on ca.Num_Appel = c.NConsultation " + Param);
            }
            return ds;
        }

        public void AfficheAppelsByPatient(int IdPatient)
        {
            Donnees.MonDataSetAppels = RecuperationConsultationByParam(" WHERE IdPatient = " + IdPatient + " order by DAP DESC" , false);

            AfficheResultat();

            this.tabTravail.SelectedIndex = 0;
        }
        public void AfficheAppelsByPatient(int[] IdPatient)
        {
            string Clause = " WHERE ";

            for (int i = 0; i < IdPatient.Length; i++)
                Clause += " IdPatient = " + IdPatient[i] + " OR";
            if (IdPatient.Length > 0)
                Clause = Clause.Remove(Clause.Length - 2, 2);
            Clause += " ORDER BY DAP DESC";

            Donnees.MonDataSetAppels = RecuperationConsultationByParam(Clause, false);

            AfficheResultat();

            this.tabTravail.SelectedIndex = 0;
        }

        public void AfficheAppelsByConsult(int NConsult)
        {
            Donnees.MonDataSetAppels = RecuperationConsultationByParam(" WHERE NConsultation = " + NConsult, false);

            if (Donnees.MonDataSetAppels != null && Donnees.MonDataSetAppels.Tables[0].Rows.Count > 0)
            {
                AfficheResultat();
                AfficheAppel(Donnees.MonDataSetAppels.Tables[0].Rows[0]);
            }

            this.tabTravail.SelectedIndex = 0;
        }

        public void AfficheRapportsByPatient(int[] IdPatient)
        {
            string Clause = " WHERE ";

            for (int i = 0; i < IdPatient.Length; i++)
                Clause += " IdPatient = " + IdPatient[i] + " OR";
            if (IdPatient.Length > 0)
                Clause = Clause.Remove(Clause.Length - 2, 2);
            Clause += " ORDER BY DAP DESC";

            if (Donnees.MonDataSetAppels != null && Donnees.MonDataSetAppels.Tables.Count > 0 && Donnees.MonDataSetAppels.Tables[0].Rows.Count > 0)
            {
                AfficheResultat();

                AfficheAppel(Donnees.MonDataSetAppels.Tables[0].Rows[0]);
            }

            this.tabTravail.SelectedIndex = 1;
        }

        public void AfficheRapportsByPatient(int IdPatient)
        {
            Donnees.MonDataSetAppels = RecuperationConsultationByParam(" WHERE IdPatient = " + IdPatient + " order by DAP DESC" , true);

            if (Donnees.MonDataSetAppels != null && Donnees.MonDataSetAppels.Tables.Count > 0 && Donnees.MonDataSetAppels.Tables[0].Rows.Count > 0)
            {
                AfficheResultat();
                AfficheAppel(Donnees.MonDataSetAppels.Tables[0].Rows[0]);
            }

            this.tabTravail.SelectedIndex = 1;
        }

        #endregion

        #region Evenements divers de la form

		// ******************************************************************
		// Déroulement de la fenetre d'attente de l'application
		public void Timer_Tick(object sender,EventArgs e)
		{
			if(OutilsExt.AttentActuelle.getValeur()>=100)
				OutilsExt.AttentActuelle.setValeur(0);
			else
				OutilsExt.AttentActuelle.setValeur(OutilsExt.AttentActuelle.getValeur()+10);
		}
		// Changement d'onglet dans le controle détail fiche d'appel
		private void tabTravail_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(tabTravail.SelectedIndex==1)
			{
				tab.Enabled = false;
                if (VariablesApplicatives.blnVersionDev)
                {
                    _ctrlFichePatient.Visible = false;
                }
                else
                {
                    tabTravail.Top = -20;
                    tabTravail.Height = 860;
                   
                    fpAppels.Visible = false;
                    button1.Visible = false;
                    button2.Visible = false;
                    button3.Visible = false;
                    button4.Visible = false;
                    button5.Visible = false;
                    button6.Visible = false;
                    button7.Visible = false;
                    tabTravail.Visible = true;
                }
            }
			else if(tabTravail.SelectedIndex==0)
			{
				tab.Enabled = true;
				crystalReportViewer1.ResetText();

                if (VariablesApplicatives.blnVersionDev)
                {
                    _ctrlFichePatient.Visible = true;
                }
                else
                {
                   // tabTravail.Top = 319;    
                    tabTravail.Top = 100;
                    tabTravail.Height = 860;                  
                    fpAppels.Visible = true;
                    button1.Visible = true;
                    button2.Visible = true;
                    button3.Visible = true;
                    button4.Visible = true;
                    button5.Visible = true;
                    button6.Visible = true;
                    button7.Visible = true;
                    tabTravail.Visible = true;
                }
            }
		}
        private void cacheActuelle()
        {
            fpAppels.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
            button7.Visible = false;
            tabTravail.Visible = false;
        }

		// Evenement de saisie d'une touche dans l'application
		private void frmGeneral_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(m_frmActualFactu!=null)
			{
				if(e.KeyCode==Keys.F9 || e.KeyCode==Keys.F10 || e.KeyCode==Keys.F1 || e.KeyCode==Keys.F12)
				{
					m_frmActualFactu.EnvoiCommande(e.KeyCode);
					return;
				}
			}

			if(e.Control && e.KeyCode==Keys.F)
			{
				mnuFip_Click(null,null);
			}
			// si l'on est dans l'onglet des rapports :
			if(tabTravail.SelectedIndex == 1)
			{
				if(e.KeyCode==Keys.F1)
					SelectionBoutonRapport(cmdRapport_EnTete);
				if(e.KeyCode==Keys.F2)
					SelectionBoutonRapport(cmdRapport_Destinataire);
				if(e.KeyCode==Keys.F3)
					SelectionBoutonRapport(cmdRapport_Concerne);
				if(e.KeyCode==Keys.F4)
					SelectionBoutonRapport(cmdRapport_Bonjour);
				if(e.KeyCode==Keys.F5)
					SelectionBoutonRapport(cmdRapport_Intro);
				if(e.KeyCode==Keys.F6)
					SelectionBoutonRapport(cmdRapport_Salutations);
				if(e.KeyCode==Keys.F7)
					SelectionBoutonRapport(cmdRapport_Signature);
				if(e.KeyCode==Keys.F8)
					SelectionBoutonRapport(cmdRapport_Corps);
				if(e.KeyCode==Keys.F9)
					btnRapportCourant_Click(null,null);
				if(e.KeyCode==Keys.F10)
					tabTravail.SelectedIndex = 0;
				if(e.KeyCode==Keys.F11)
                    //BtnRapport_RefusVisa_Click(null,null);
                    lnkRapport_AjoutDestinataire_LinkClicked(null,null);
				if(e.KeyCode==Keys.F12)					
					PicRapport_Actualiser_Click(null,null);
                if(e.Control && e.KeyCode == Keys.P)        //CTRL + P pour valider le rapport
                    BtnRapport_Visa_Click(null, null);
                if (e.Control && e.KeyCode == Keys.O)        //CTRL + O pour renvoyer en correction 
                     BtnRapport_RefusVisa_Click(null,null);


            }
            // si l'on est dans l'onglet de fiche d'appel
            else if(tabTravail.SelectedIndex==0)
			{
				if(e.KeyCode==Keys.F1)
				{
					picSaveFiche_Click(null,null);
				}
				else if (e.KeyCode==Keys.F2)
				{
					if(m_frmActualFactu!=null)
					{
						m_frmActualFactu.Hide();
					}

					tabTravail.SelectedIndex = 1;
					
					if(btnRapportCourant.Tag!=null && btnRapportCourant.BackColor == Color.MistyRose)
					{
						btnRapportCourant_Click(null,null);
					}
				}
			}
			// si l'on est dans l'onglet de rapport a visé
			if(m_frmLstRapportToSend != null)
			{
				if(e.KeyCode==Keys.Enter)
				{
					m_frmLstRapportToSend.listView1_DoubleClick(null,null);
				}
			}
		}

        #endregion

        #region Evenements sur les menus

		// Impression d'une liste de facture autmatique
        private void mnuFac_Impression_Click(object sender, System.EventArgs e)
        {
            // Quelles sont les factures à imprimer?

            long NFacDebut = OutilsExt.OutilsSql.RecuperationNFacturesEnvoiDebut();
            long NFacFin = OutilsExt.OutilsSql.RecuperationNFacturesEnvoiFin();

            frmEnvoi m_frmenvoi = new frmEnvoi(NFacDebut, NFacFin);
            m_frmenvoi.ShowDialog();
            m_frmenvoi.Dispose();
            m_frmenvoi = null;
        }

        private void menuItem2_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show("Voulez vous quitter l'application ?", "Fermeture", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                FermetureApplication();
            }
        }
		// Menu a propos
        private void menuItem7_Click(object sender, System.EventArgs e)
        {
            frmApropos apropos = new frmApropos();
            apropos.ShowDialog();
            apropos.Dispose();
            apropos = null;
        }

        private void mnuDocJoint_Click(object sender, System.EventArgs e)
        {
                /*frmFacture_DocJoint docJ = new frmFacture_DocJoint();
                docJ.ShowDialog();
                docJ.Dispose();
                docJ = null;*/

            //-1 signifie qu'on vient du menu et pas de la facture
            FAjoutDocumentsFacture docJ = new FAjoutDocumentsFacture("-1");
            docJ.ShowDialog();

            docJ.Dispose();
            docJ = null;
        }

        private void mnuRechercheFacture_Click(object sender, System.EventArgs e)
        {
            frmFacHisto histo = new frmFacHisto(this);
            histo.ShowDialog();
        }

        private void mnuCollabo_Click(object sender, System.EventArgs e)
        {
            frmMedecins frm = new frmMedecins();
            frm.ShowDialog();
            frm.Dispose();
            frm = null;
        }
      

        #endregion
		
		// *************************************************
		// Les onglets de l'application
		// *************************************************

        #region Rubrique Fiche d'appel

        #region Evenements sur une fiche d'appel


        private void picSaveFiche_Click(object sender, System.EventArgs e)
        {
            if (pan_Dynamique.Tag != null)
            {
                if (!WorkedString.ValiditeDate(txtDateNaissance.Text) && txtDateNaissance.Text != "")
                {
                    MessageBox.Show("Champs Date de naissance non valide");
                    txtDateNaissance.Focus();
                    return;
                }


                if (txtPatient_Nom.Text == "" && txtPatient_Prenom.Text == "")
                {
                    MessageBox.Show("Vous ne pouvez pas effacer un patient !!!.",
                                    "Stop!", 
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Stop,
                                    MessageBoxDefaultButton.Button1);

                    txtPatient_Nom.Focus();
                    return;
                }

                DataRow row = (DataRow)pan_Dynamique.Tag;

                //******** Affectation des tous les champs au dataset *********

                // Fiche patient :
                row["NomPatient"] = txtPatient_Nom.Text;
                row["PrenomPatient"] = txtPatient_Prenom.Text;
                row["Age"] = txtPatient_Age.Text;
                row["UniteAge"] = txtPatient_UniteAge.Text;
                row["Sexe"] = txtPatient_Sexe.Text;
                row["NumeroDansRue"] = txtPatient_NumRue.Text;
                
                //on test le contenu de la chaine Adresse1
                //Si elle contient route, rue... (le contenu de CBRoute1),
                if ((txtPatient_Adresse1.Text.ToLower().IndexOf("route") > -1) ||
                   (txtPatient_Adresse1.Text.ToLower().IndexOf("rue") > -1) ||
                   (txtPatient_Adresse1.Text.ToLower().IndexOf("avenue") > -1) ||
                   (txtPatient_Adresse1.Text.ToLower().IndexOf("boulevard") > -1) ||
                   (txtPatient_Adresse1.Text.ToLower().IndexOf("place") > -1) ||
                   (txtPatient_Adresse1.Text.ToLower().IndexOf("passage") > -1) ||
                   (txtPatient_Adresse1.Text.ToLower().IndexOf("sentier") > -1) ||
                   (txtPatient_Adresse1.Text.ToLower().IndexOf("square") > -1) ||
                   (txtPatient_Adresse1.Text.ToLower().IndexOf("chemin") > -1) ||
                   (txtPatient_Adresse1.Text.ToLower().IndexOf("allée") > -1) ||
                   (txtPatient_Adresse1.Text.ToLower().IndexOf("cité") > -1) ||
                   (txtPatient_Adresse1.Text.ToLower().IndexOf("cours") > -1) ||
                   (txtPatient_Adresse1.Text.ToLower().IndexOf("impasse") > -1) || 
                   (txtPatient_Adresse1.Text.ToLower().IndexOf("quai") > -1))   
                    {
                        row["Rue"] = txtPatient_Adresse1.Text;     //on ne réaffecte pas le contenu de CBRoute
                    }
                else   //Sinon on l'affecte
                    {
                     row["Rue"] = CBRoute1.Text + " " + txtPatient_Adresse1.Text;                     
                    }

                //Réaffectation du champ TexteSup pour l'adresse2
                row["TexteSup"] = txtPatient_Adresse2.Text;
                
                row["Commune"] = txtPatient_Localite.Text;

                //tel avec masque de saisie
                if (EMaskTel1.Text.IndexOf('+') == -1)
                    row["TelPatient"] = "+" + EMaskTel1.Text.Replace("-", "").Replace(" ", "");
                else
                    row["TelPatient"] = EMaskTel1.Text.Replace("-", "").Replace(" ", "");
                                
                row["Email"] = txtEmail.Text;           //Domi 13.10.2017

                row["Longitude"] = txtPatient_Longitude.Text;
                row["Latitude"] = txtPatient_Latitude.Text;
          
                row["Etage"] = txtPatient_Etage.Text;
                //row["Batiment"] = txtPatient_Bat.Text;
              
                row["Digicode"] = txtPatient_Digicode.Text;
                row["InterNom"] = txtPatient_Internom.Text;
                row["Porte"] = txtPatient_Porte.Text;
              
                row["DateNaissance"] = txtDateNaissance.Text;
                row["CodePostal"] = TxtNPA.Text;
                row["Adm_Batiment"] = txtPatient_AdmBatiment.Text;

                         
                //on test le contenu de la chaine AdmAdresse1
                //Si elle contient route, rue... (le contenu de CBRoute1),
                if ((txtPatient_AdmAdresse1.Text.ToLower().IndexOf("route") > -1) ||
                   (txtPatient_AdmAdresse1.Text.ToLower().IndexOf("rue") > -1) ||
                   (txtPatient_AdmAdresse1.Text.ToLower().IndexOf("avenue") > -1) ||
                   (txtPatient_AdmAdresse1.Text.ToLower().IndexOf("boulevard") > -1) ||
                   (txtPatient_AdmAdresse1.Text.ToLower().IndexOf("place") > -1) ||
                   (txtPatient_AdmAdresse1.Text.ToLower().IndexOf("passage") > -1) ||
                   (txtPatient_AdmAdresse1.Text.ToLower().IndexOf("sentier") > -1) ||
                   (txtPatient_AdmAdresse1.Text.ToLower().IndexOf("square") > -1) ||
                   (txtPatient_AdmAdresse1.Text.ToLower().IndexOf("chemin") > -1) ||
                   (txtPatient_AdmAdresse1.Text.ToLower().IndexOf("allée") > -1) ||
                   (txtPatient_AdmAdresse1.Text.ToLower().IndexOf("cité") > -1) ||
                   (txtPatient_AdmAdresse1.Text.ToLower().IndexOf("cours") > -1) ||
                   (txtPatient_AdmAdresse1.Text.ToLower().IndexOf("impasse") > -1) ||
                   (txtPatient_AdmAdresse1.Text.ToLower().IndexOf("quai") > -1))
                {
                    row["Adm_Rue"] = txtPatient_AdmAdresse1.Text.TrimEnd();  //on ne réaffecte pas le contenu de CBRoute
                }
                else   //Sinon on l'affecte
                {

                    row["Adm_Rue"] = CBRoute_adm1.Text + " " + txtPatient_AdmAdresse1.Text.TrimEnd();
                    //MessageBox.Show("on passe");  
                }
                    
                row["Adm_NumeroDansRue"] = txtPatient_AdmNumRue.Text.TrimEnd();
                //Réaffectation du champ ListeNoire pour l'Admadresse2
                row["ListeNoire"] = txtPatient_AdmAdresse2.Text;
                row["Adm_CodePostal"] = txtPatient_AdmNPA.Text.TrimEnd();
                row["Adm_Commune"] = txtPatient_AdmLocalite.Text.TrimEnd();
                // Fiche compte-rendu :
                row["Actes"] = "";
                    
                row["Traitements"] = "";
               
                //Consultation
                //Affichage la case encaissé sur place est cochée alors on met 1 
                if (checkBoxESP.Checked == true)
                    row["Esp"] = 1;  //on coche la case Encaissé sur place                
                else row["Esp"] = 0; 
               
                //Enregistrement du dataset
                Boolean z_blnConnect = Variables.ConnexionBase.OpenBDD();
                try
                {
                    Variables.ConnexionBase.BeginTransaction();

                    // Personne
                    TablePersonne z_objPersonne = new TablePersonne();
                    z_objPersonne.Update(row);
                    // Patient
                    TablePatient z_objPatient = new TablePatient();
                    z_objPatient.Update(row);
                    // Consultations
                    TableConsultations z_objConsultations = new TableConsultations();
                    z_objConsultations.Update(row);
                    // Modification
                    Fonction z_objFonctionDal = new Fonction();
                    z_objFonctionDal.EnregistreModification(row["NConsultation"].ToString(), VariablesApplicatives.Utilisateurs.Identifiant, DateTime.Now, Constantes.MODIF_CONSULT, txtFiche_CommentairSauvegarde.Text);

                    Variables.ConnexionBase.Commit();
                    LblStatusSauvegardeFiche.Text = "Sauvegarde de la fiche avec succès!";
                    // Mise a jour affichage
                    ChargementHistoriqueFiche(long.Parse(row["Num"].ToString()));
                    txtFiche_CommentairSauvegarde.Text = "";
                    pan_Dynamique.Tag = row;
                    fpAppels_Sheet1.Rows[fpAppels_Sheet1.ActiveRowIndex].Tag = row;

                    mouchard.evenement("Modification de la FIP à partir de la main courante pour " + txtPatient_Nom.Text.ToString() + " " + txtPatient_Prenom.Text.ToString(), VariablesApplicatives.Utilisateurs.NomUtilisateur.ToString());  //log
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    throw new Exception(ex.Message);
                    //MessageBox.Show(ex.Message.ToString());
                }
                catch (Exception ex)
                {
                    Variables.ConnexionBase.RollBack();
                    LblStatusSauvegardeFiche.Text = "Erreur à la sauvegarde de la fiche!";

                    throw new Exception(ex.Message);
                }
                finally
                {
                    if (z_blnConnect)
                    {
                        Variables.ConnexionBase.CloseBDD();
                    }
                }
            }
        }

        #endregion

        #region Aide à la saisie

		// Aide à la saisie pour la partie patient:
        private void InitialiseAidePatient()
        {
            TextBox txt = TxtEnCours;
            LstAide.Visible = false;
            TxtEnCours = txt;
            LstAide.Items.Clear();
            pan_Patient.Controls.Add(LstAide);
            pan_Patient.Controls.SetChildIndex(LstAide, 0);
        }
        private void txtPatient_Enter(object sender, System.EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (TxtEnCours != null && txt.Name == TxtEnCours.Name) return;
            TxtEnCours = txt;
            TxtValDefaut = txt.Text;
            InitialiseAidePatient();
            LstAide.Width = txt.Width;
            LstAide.Top = txt.Top + txt.Height + 1;
            LstAide.Left = txt.Left;
        }
		private void txtPatient_KeyDown(object sender,System.Windows.Forms.KeyEventArgs e)
		{
			TextBox txt = (TextBox)sender;

			if(e.KeyCode==Keys.Tab)
			{
				InitialiseAidePatient();
				return;
			}
			if(e.KeyCode==Keys.Escape)
			{
				InitialiseAidePatient();
				return;
			}
			if(e.KeyCode==Keys.Return)
			{
				
				foreach(Control c in txt.Parent.Controls)
				{
					if(c.TabIndex == txt.TabIndex+1 && c.TabStop)
					{
						InitialiseAidePatient();
						c.Focus();
						return;
					}
				}
				return;
			}
			if(e.KeyCode==Keys.Down)
			{	
				switch(txt.Name)
				{
					case "txtPatient_UniteAge":
						TxtChoixMultiple = false;
						RemplirAide("uniteage",txt.Text,TxtValDefaut);
						LstAide.Visible = true;
						break;
					case "txtPatient_Sexe":
						TxtChoixMultiple = false;
						RemplirAide("sexe",txt.Text,TxtValDefaut);
						LstAide.Visible = true;
						break;
					case "txtPatient_Commune":
						TxtChoixMultiple = false;
						RemplirAide("commune",txt.Text,TxtValDefaut);
						LstAide.Visible = true;
						break;
					default:
						break;
				}

				if(LstAide.Items.Count>0 && LstAide.Visible)
				{
					LstAide.SelectedIndex = 0;
					LstAide.Focus();
				}
				return;
			}
		}
		private void txtPatient_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{			
			TextBox txt = (TextBox)sender;

			switch(txt.Name)
			{
				case "txtPatient_Age":
					e.Handled = !WorkedString.FormatNumerique(false,e.KeyChar);
					break;
				case "txtDateNaissance":
					e.Handled = !WorkedString.FormatDate(e.KeyChar);
					break;
				case "txtPatient_Tel":
					e.Handled = !WorkedString.FormatNumerique(true,e.KeyChar);
					break;
				case "txtPatient_Longitude":
					e.Handled = !WorkedString.FormatNumerique(false,e.KeyChar);
					break;
				case "txtPatient_Latitude":
					e.Handled = !WorkedString.FormatNumerique(false,e.KeyChar);
					break;								
				default:
					break;
			}				
		}

		
		private void txtBilan_Enter(object sender, System.EventArgs e)
		{
			TextBox txt = (TextBox)sender;
			if(TxtEnCours!=null && txt.Name==TxtEnCours.Name) return;
			TxtEnCours = txt;	
			TxtValDefaut = txt.Text;		
		}		
		
		private void txtBilan_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			TextBox txt = (TextBox)sender;

			switch(txt.Name)
			{
				case "txtBilan_Hono":					
					e.Handled = !WorkedString.FormatFlottant(false,e.KeyChar);
					break;									
				default:
					break;
			}		
		}
		private void txtBilan_TextChanged(object sender, System.EventArgs e)
		{
			TextBox txt = (TextBox)sender;
			
			LstAide.Width  = txt.Width;
			LstAide.Top = txt.Top + txt.Height + 1;
			LstAide.Left = txt.Left;
			switch(txt.Name)
			{
				
				case "txtPatient_Commune":
					TxtChoixMultiple = false;
					RemplirAide("commune",txt.Text,TxtValDefaut);
					LstAide.Visible = true;
					break;
				
				default:
					break;
			}		
		}

        // Liste d'aide à la saisie - Commune
		private void RemplirAide(string type,string texte,string Default)
		{
			LstAide.Items.Clear();

			switch(type)
			{
				case "commune":
					if(TxtValDefaut!="") 
					{
						ListItem item = new ListItem(null,TxtValDefaut);
						LstAide.Items.Add(item);
					}
					foreach(string s in Statiques_Data.TabCommune)
					{
						ListItem item = new ListItem(s,s);
						LstAide.Items.Add(item);
					}
					break;
				
				
				
				case "uniteage":
					string[] resage = new string[] {"A","M","S"};
					if(TxtValDefaut!="") 
					{
						ListItem item = new ListItem(null,TxtValDefaut);
						LstAide.Items.Add(item);
					}
					foreach(string s in resage)
					{
						ListItem item = new ListItem(s,s);
						LstAide.Items.Add(item);
					}
					break;
				case "sexe":
					string[] ressexe = new string[] {"H","M","F","E"};
					if(TxtValDefaut!="") 
					{
						ListItem item = new ListItem(null,TxtValDefaut);
						LstAide.Items.Add(item);
					}
					foreach(string s in ressexe)
					{
						ListItem item = new ListItem(s,s);
						LstAide.Items.Add(item);
					}
					break;
				
				default:
					break;
			}
		}
		private void LstAide_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Return)
			{
				SelectionDansAssistant();
				if(LstAide.Parent.Name=="pan_Patient")
					InitialiseAidePatient();
				TxtEnCours.Focus();
			}
			else if(e.KeyCode==Keys.Escape)
			{
				if(LstAide.Parent.Name=="pan_Patient")
					InitialiseAidePatient();				
				TxtEnCours.Focus();
			}			
		}
		private void LstAide_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			SelectionDansAssistant();
		}
		private void SelectionDansAssistant()
		{
			if(TxtEnCours!=null && LstAide.SelectedIndex>-1)
			{			
				ListItem item = (ListItem)LstAide.SelectedItem;
				if(!TxtChoixMultiple)
				{
					TxtEnCours.Text = item.strText;
					TxtEnCours.Tag = item.objValue;
				}
				else
				{
					if(TxtEnCours.Text=="")
					{					
						TxtEnCours.Text = LstAide.SelectedItem.ToString();
					}
					else
						TxtEnCours.Text += "¤|¤" + LstAide.SelectedItem.ToString();
				}
			}	
		}		

        #endregion

        #region Affichage des appels

		// On initialise les boutons de filtre du tableau des appels
        public void InitialiseBoutonsFiltre(bool Enable)
        {
            button1.Enabled = Enable;
            button2.Enabled = Enable;
            button3.Enabled = Enable;
            button4.Enabled = Enable;
            button5.Enabled = Enable;
            button6.Enabled = Enable;
            button7.Enabled = Enable;

            button1.Tag = -1;
            button2.Tag = -1;
            button3.Tag = -1;
            button4.Tag = -1;
            button5.Tag = -1;
            button6.Tag = -1;
            button7.Tag = -1;
        }

        public void MiseAJourListeAppels(DataSet dt)
        {
            DataRow[] rows = new DataRow[dt.Tables[0].Rows.Count];
            for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
                rows[i] = dt.Tables[0].Rows[i];

            MiseAJourListeAppels(rows);
        }

        public void MiseAJourListeAppels(DataRow[] m_Rows)
        {
            tabTravail.SelectedIndex = 0;
            tabTravail.Enabled = false;
            btnOnglet1.Enabled = false;
            btnOnglet2.Enabled = false;
            btnOnglet3.Enabled = false;

            fpAppels_Sheet1.RowCount = 0;

            if (m_Rows != null)
            {
                bool Pos = false;
                foreach (DataRow row in m_Rows)
                {
                    int nb = fpAppels_Sheet1.RowCount++;
                    fpAppels_Sheet1.Rows[nb].Tag = row;
                    fpAppels.Sheets[0].Rows[nb].Locked = true;

                    FarPoint.Win.Spread.CellType.GeneralCellType gen = new FarPoint.Win.Spread.CellType.GeneralCellType();

                    if (Pos)
                    {
                        fpAppels.Sheets[0].Cells[nb, 0, nb, 6].CellType = Gradient1;
                    }
                    else
                    {
                        fpAppels.Sheets[0].Cells[nb, 0, nb, 6].CellType = Gradient2;
                    }
                    Pos = !Pos;

                    fpAppels_Sheet1.Cells[nb, 0, nb, 6].Font = new Font("Arial", 10);
                    fpAppels_Sheet1.Cells[nb, 0].Text = row["NConsultation"].ToString();
                    fpAppels_Sheet1.Cells[nb, 1].Text = DateTime.Parse(row["DAP"].ToString()).ToString();
                    fpAppels_Sheet1.Cells[nb, 2].Text = row["NomMedecinSos"].ToString();
                    fpAppels_Sheet1.Cells[nb, 3].Text = row["NomPatient"].ToString() + " " + row["PrenomPatient"].ToString();
                    fpAppels_Sheet1.Cells[nb, 4].Text = row["Commune"].ToString() + " " + row["Rue"].ToString() + " " + row["NumeroDansRue"].ToString();
                    fpAppels_Sheet1.Cells[nb, 5].Text = row["OrigineAppel"].ToString();

                    

                    //gestion des etats de la consultation
                    int Etat1 = int.Parse(row["Modifie"].ToString());
                    int Etat2 = int.Parse(row["RapportGenere"].ToString());
                    int Etat3 = int.Parse(row["FactureGeneree"].ToString());
                    int Etat4 = int.Parse(row["AnnulationAppel"].ToString());
                    string Etat5 = row["Motif1"].ToString();
                    int Etat6 = int.Parse(row["Esp"].ToString());
                    string NConsultationTemp = row["NConsultation"].ToString();

                    if (Etat3 == 0 && NConsultationTemp != "")
                    {
                        if(CheckReadOnlyConsultations(NConsultationTemp) == true)
                        {
                            Etat3 = 1;
                        }
                    }


                    if ((Etat4 != 0) && (Etat2 != 0))    //Appels annulés + Rapport
                    {
                        gen.BackgroundImage = new FarPoint.Win.Picture(Image.FromFile(Application.StartupPath + "\\images\\ledblack_o.gif"));   //Led noire
                    }
                    else if (Etat4 != 0)                 //Appels annulés
                    {
                        gen.BackgroundImage = new FarPoint.Win.Picture(Image.FromFile(Application.StartupPath + "\\images\\ledblack.gif"));     //Led noire
                    }
                    else if (Etat6 == 1)                 //Encaissé sur place...Pas de facture
                    {
                        gen.BackgroundImage = new FarPoint.Win.Picture(Image.FromFile(Application.StartupPath + "\\images\\ledbleu.gif"));         //Led bleu
                    }
                    else if (Etat3 == 1)                 //Facture générée
                    {
                        gen.BackgroundImage = new FarPoint.Win.Picture(Image.FromFile(Application.StartupPath + "\\images\\ledv.gif"));         //led Verte
                    }
                    else if ((Etat5 == "ConsTel") && (Etat2 != 0))      //Consultations Tel + Rapport
                    {
                        gen.BackgroundImage = new FarPoint.Win.Picture(Image.FromFile(Application.StartupPath + "\\images\\ledviolet_o.gif"));  //led violet/jaune
                    }
                    else if (Etat5 == "ConsTel")            //Consultation Tel
                    {
                        gen.BackgroundImage = new FarPoint.Win.Picture(Image.FromFile(Application.StartupPath + "\\images\\ledviolet.gif"));    //Led violet
                    }
                    else if (Etat2 == 1)            //Rapport généré
                    {
                        gen.BackgroundImage = new FarPoint.Win.Picture(Image.FromFile(Application.StartupPath + "\\images\\ledo.gif"));         //Led jaune
                    }
                    else if (Etat1 == 1)            //Modifié
                    {
                        gen.BackgroundImage = new FarPoint.Win.Picture(Image.FromFile(Application.StartupPath + "\\images\\ledr.gif"));         //Led rouge
                    }                   
                    else                         //par défaut
                    {
                        gen.BackgroundImage = new FarPoint.Win.Picture(Image.FromFile(Application.StartupPath + "\\images\\ledr.gif"));         //Led rouge
                    }
                    fpAppels_Sheet1.Cells[nb, 6].CellType = gen;
                }

                fpAppels_Sheet1.Rows.Add(0, 1);
                fpAppels_Sheet1.Models.Span.Add(0, 0, 1, fpAppels_Sheet1.ColumnCount);
                fpAppels_Sheet1.Cells[0, 0].Text = "Résultat de la dernière requête : " + m_Rows.Length;
            }
        }


        /// <summary>
        /// Checks whether a read-only consultation exists based on the specified consultation number.
        /// </summary>
        /// <param name="NumConsultation">The consultation number to check for existence in the database.</param>
        /// <returns>
        /// Returns <c>true</c> if the consultation exists with the specified number, 
        /// and both <c>Etat_Facturation</c> equals 2 and <c>LecteurSeule</c> equals 1; 
        /// otherwise, returns <c>false</c>.
        /// </returns>
        /// <exception cref="MySqlException">
        /// Thrown when an error occurs while accessing the database.
        /// </exception>
        /// <remarks>
        /// This method opens a database connection, executes a SQL query to count the number 
        /// of visits that match the specified conditions, and determines if the consultation 
        /// exists. If an error occurs during the database operation, it shows an error message 
        /// and rolls back any transaction that might have been started.
        /// </remarks>
        private bool CheckReadOnlyConsultations(string NumConsultation)
        {
            bool exists = false; // To track if the consultation exists
            string connectionString = ConfigurationManager.ConnectionStrings["Connection_Base_Fiche"].ToString();

            using (MySqlConnection dbConnection = new MySqlConnection(connectionString))
            {
                dbConnection.Open(); // Open the database connection
                using (MySqlTransaction trans = dbConnection.BeginTransaction())
                {
                    try
                    {
                        // SQL query to select visits based on the provided conditions
                        string requete1 = "SELECT COUNT(*) FROM visite WHERE Num_Appel = @NumConsultation " +
                                          "AND Etat_Facturation = 2 AND LectureSeule = 1";

                        using (MySqlCommand cmd = new MySqlCommand(requete1, dbConnection, trans))
                        {
                            cmd.Parameters.AddWithValue("@NumConsultation", NumConsultation);

                            // Execute the command and check if any record exists
                            int count = Convert.ToInt32(cmd.ExecuteScalar());

                            if (count > 0)
                            {
                                exists = true; // Set exists to true if at least one record is found
                            }
                        }

                        trans.Commit(); // Commit the transaction
                    }
                    catch (Exception e)
                    {
                        trans.Rollback(); // Rollback the transaction in case of error
                        MessageBox.Show("Erreur lors de la vérification de la consultation. " + e.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            return exists;
        }

        #endregion

        #region Chargement des Données statiques

        private void ChargementDonneesStatiques()
        {
            
            Statiques_Data.TabDiag1 = OutilsExt.OutilsSql.ListeDiag1();           
            Statiques_Data.TabDiag2 = OutilsExt.OutilsSql.ListeDiag2();           
            Statiques_Data.TabPriseEnCharge = OutilsExt.OutilsSql.ListePriseEnCharge();            
            Statiques_Data.TabDevenir = OutilsExt.OutilsSql.ListeDevenir();            
            Statiques_Data.TabProvenance = OutilsExt.OutilsSql.ListeProvenance();           
            Statiques_Data.TabCommune = OutilsExt.OutilsSql.ListeCommunes();            

            // ***** Rubrique Facturation :
            // Chargement des prestations :          
            string[][] retour = OutilsExt.OutilsSql.ExecuteCommandeAvecTabString("SELECT NPrestation,PrestLibelle,PrestPointM,PrestPointT,PrestMajor,PrestHorsMajor from Tarmed order by NPrestation");
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

            // Chargement du matériel
            //OutilsExt.AttentActuelle.setLibelle2("Materiel");
            //OutilsExt.AttentActuelle.setValeur(40);
            retour = OutilsExt.OutilsSql.ExecuteCommandeAvecTabString("SELECT Nt_materiel,MatLibelle,MatPrix, Num_Materiel from fac_tablemateriel order by Nt_materiel");
            Statiques_Data.TabMateriel = new Facture_Materiel[retour.Length];
            for (int i = 0; i < retour.Length; i++)
            {
                string[] tab = retour[i];
                Statiques_Data.TabMateriel[i] = new Facture_Materiel(tab[0], tab[1], float.Parse(tab[2]), tab[3]);
            }
            // *****************************************************************************
            //OutilsExt.AttentActuelle.setValeur(100);
            //// -----------------------------------------
            //OutilsExt.AttentActuelle.Close();
            //OutilsExt.AttentActuelle.Dispose();
        }

        #endregion

        #region Rubrique Filtre par appels

		// **************************************************************
		// Initialisation des rubriques de filtres
		// **************************************************************
        private void ChargementListeMedecins()
        {
            cbMedecin.Items.Clear();
            string[][] ListeNoms = OutilsExt.OutilsSql.ListeMedecins();
            foreach (string[] s in ListeNoms)
            {
                ListItem item = new ListItem(s[0], s[1]);
                cbMedecin.Items.Add(item);
            }
        }

        private void ChargementListeMotifs()
        {
            cbMotif.Items.Clear();
            string[] ListeMotifs = OutilsExt.OutilsSql.ListeMotifs();
            foreach (string s in ListeMotifs)
                cbMotif.Items.Add(s);
        }

        private void ChargementListeOrigine()
        {
            cbOrigine.Items.Clear();
            string[] ListeOrigine = OutilsExt.OutilsSql.ListeOrigine();
            foreach (string s in ListeOrigine)
                cbOrigine.Items.Add(s);
        }

        private void InitialiseFiltreDate()
        {
            dateTimePicker1.Value = DateTime.Parse(DateTime.Now.AddDays(-1).ToShortDateString() + " 00:00:00");
            dateTimePicker2.Value = DateTime.Parse(DateTime.Now.ToShortDateString() + " 23:59:59");
        }

		// **************************************************************
		// Affichage des appels filtrés
		// **************************************************************
		// **************************************************************

        #endregion

        #region Rubrique tri des appels

		// ***************************************************************
		// clic sur un des bouton de tri
		// ***************************************************************
        private void button3_Click(object sender, System.EventArgs e)
        {
            Button btn = (Button)sender;

            int ValToAttrib = 0;

            if (btn.Tag != null && (btn.Tag.ToString() == "-1" || btn.Tag.ToString() == "1"))
            {
                InitialiseBoutonsFiltre(true);
                btn.BackColor = Color.MediumSlateBlue;
                ValToAttrib = 0;
            }
            else
            {
                InitialiseBoutonsFiltre(true);
                btn.BackColor = Color.Violet;
                ValToAttrib = 1;
            }

            switch (btn.Name)
            {
                case "button1":
                    ClasseAppelsParIndex(1, btn.BackColor);
                    btn.Tag = ValToAttrib;
                    break;
                case "button2":
                    ClasseAppelsParIndex(2, btn.BackColor);
                    btn.Tag = ValToAttrib;
                    break;
                case "button3":
                    ClasseAppelsParIndex(3, btn.BackColor);
                    btn.Tag = ValToAttrib;
                    break;
                case "button4":
                    ClasseAppelsParIndex(4, btn.BackColor);
                    btn.Tag = ValToAttrib;
                    break;
                case "button5":
                    ClasseAppelsParIndex(5, btn.BackColor);
                    btn.Tag = ValToAttrib;
                    break;
                case "button6":
                    ClasseAppelsParIndex(6, btn.BackColor);
                    btn.Tag = ValToAttrib;
                    break;
                case "button7":
                    ClasseAppelsParIndex(7, btn.BackColor);
                    btn.Tag = ValToAttrib;
                    break;
                default:
                    break;
            }
        }
		// ***************************************************************

		// ***************************************************************
		// Tri par type 
		// ***************************************************************
        private void ClasseAppelsParIndex(int index, Color couleur)
        {
            string order = "";
            if (couleur == Color.MediumSlateBlue)
            {
                order = " asc";
            }
            else
            {
                order = " desc";
            }

            if (Donnees.MonDataSetAppels != null)
            {
                DataRow[] rows = new DataRow[0];

                switch (index)
                {
                    case 1:
                        rows = Donnees.MonDataSetAppels.Tables[0].Select("num>0", "num" + order);
                        break;
                    case 2:
                        rows = Donnees.MonDataSetAppels.Tables[0].Select("num>0", "DAP" + order);
                        break;
                    case 3:
                        rows = Donnees.MonDataSetAppels.Tables[0].Select("num>0", "NomMedecinSos" + order);
                        break;
                    case 4:
                        rows = Donnees.MonDataSetAppels.Tables[0].Select("num>0", "nompatient" + order);
                        break;
                    case 5:
                        rows = Donnees.MonDataSetAppels.Tables[0].Select("num>0", "Adm_Commune" + order);
                        break;
                    case 6:
                        rows = Donnees.MonDataSetAppels.Tables[0].Select("num>0", "origineappel" + order);
                        break;
                    case 7:
                        DataRow[] rows1 = Donnees.MonDataSetAppels.Tables[0].Select("FactureGeneree = 1", "DAP desc");
                        DataRow[] rows2 = Donnees.MonDataSetAppels.Tables[0].Select("FactureGeneree = 0 and RapportGenere = 1", "DAP desc");
                        DataRow[] rows3 = Donnees.MonDataSetAppels.Tables[0].Select("FactureGeneree = 0 and RapportGenere = 0", "DAP desc");
                        rows = new DataRow[rows1.Length + rows2.Length + rows3.Length];
                        rows1.CopyTo(rows, 0);
                        rows2.CopyTo(rows, rows1.Length);
                        rows3.CopyTo(rows, rows1.Length + rows2.Length);
                        break;
                    default:
                        break;
                }
                MiseAJourListeAppels(rows);
            }
        }
		// ***************************************************************		

        #endregion

        #region Chargement et Affichage d'un appel de la liste

		// clic dans la liste des appels
		private void fpAppels_MouseUp(object sender, MouseEventArgs e)
		{
			tabTravail.Enabled = false;
			btnOnglet1.Enabled = false;
			btnOnglet2.Enabled  = false;
			btnOnglet3.Enabled  = false;
			FarPoint.Win.Spread.Model.CellRange range = fpAppels.GetCellFromPixel(0,0,e.X,e.Y);

			if(range.Column >-1 && range.Row > 0)
			{
				DataRow row = (DataRow)fpAppels_Sheet1.Rows[range.Row].Tag;
				if(row!=null)
				{		
					if(e.Button == MouseButtons.Left)
					{
						fpAppels_Sheet1.SetActiveCell(range.Row,range.Column);
						AfficheAppel(row);
						InitialiseAidePatient();
                        // Added for aditional description of report
                        InitializeLabelInfoReport(row);
                        // Added for aditional image viewer of insurance
                        InitializeZoomImageViewer1(row);
                    }
					else
					{						                                                
                        LigneEnCours = row;						
						ContextMenu CtMenu = new ContextMenu();
                        MenuItem item = new MenuItem("Fiche de la visite");
                        item.Click += new EventHandler(CtMenu_Click);
                        CtMenu.MenuItems.Add(item);

                        item = new MenuItem("Rapport de la visite");
                        item.Click += new EventHandler(CtMenu_Click);
                        CtMenu.MenuItems.Add(item);

                        item = new MenuItem("Factures du patient");
						item.Click+=new EventHandler(CtMenu_Click);						
						CtMenu.MenuItems.Add(item);
						item = new MenuItem("Voir Historique du patient");
						item.Click+=new EventHandler(CtMenu_Click);						
						CtMenu.MenuItems.Add(item);
						item = new MenuItem("FIP du patient");
						item.Click+=new EventHandler(CtMenu_Click);						
						CtMenu.MenuItems.Add(item);
						CtMenu.Show(fpAppels,new Point(e.X,e.Y));
					}
				}
			}			
		}

        /// <summary>
        /// Initializes the label that displays information about the report, including the author and report type.
        /// Retrieves data from the dataset and database, checks for a valid consultation number,
        /// and constructs the label text based on the consultation author and report type.
        /// </summary>
        /// <remarks>
        /// This method checks if the dataset contains consultation data. If it does, it fetches the report
        /// information from the database and updates the label accordingly. If no consultation data is available,
        /// an error message is shown.
        /// </remarks>
        /// <exception cref="System.Data.DataException">
        /// Thrown if the dataset is empty or no consultation data is found.
        /// </exception>
        private void InitializeLabelInfoReport(DataRow row)
        {
            try
            {
                string tempTxtIdConsultation = row["Nconsultation"].ToString();

                if (tempTxtIdConsultation == "")
                {
                    labelInfoReport.Text = "";
                    return;
                }

                string[][] inforapport = OutilsExt.OutilsSql.ExecuteCommandeAvecTabString("SELECT Auteur,Type_long_rapport from tableconsultations WHERE NConsultation = " + tempTxtIdConsultation);

                string Auteur = "", Type_rapport = "";

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

                    labelInfoReport.Text = Type_rapport + Auteur;
                }

                else
                {
                    labelInfoReport.Text = "Aucune information sur le rapport"; // If information about report is not existed
                }

                labelInfoReport.Visible = true;

            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                Console.WriteLine(ex.Message);
                labelInfoReport.Visible = false;
            }
        }
        #region zoomImageViewer

        /// <summary>
        /// Initializes the ZoomImageViewer control with the appropriate image or a placeholder if the image is not found.
        /// </summary>
        /// <remarks>
        /// This method checks if there is consultation data available. If the data is present, it constructs the file path
        /// for the corresponding image (based on consultation ID) and loads it into the ZoomImageViewer control. If the image
        /// file doesn't exist or has zero size, a default placeholder image is loaded instead.
        /// </remarks>
        ///
        /// <exception cref="Exception">Thrown if there is an issue loading the image.</exception>
        private void InitializeZoomImageViewer1(DataRow row)
        {
            if (zoomImageViewer1.Visible == false || bRotationImage.Visible == false)
            {
                zoomImageViewer1.Visible = true;
                bRotationImage.Visible = true;
            }

            string PathSource = SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.Dest_Path_carteAVS;

            try
            {
                string IdConsultation = row["Nconsultation"].ToString();

                //on defini le chemin avec le nom complet du fichier 
                String ImageCarteAVS = PathSource + IdConsultation + ".jpg";

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

        private void bRotationImage_Click(object sender, EventArgs e)
        {
            if (zoomImageViewer1.Image != null)
            {
                Image img = zoomImageViewer1.Image;
                img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                zoomImageViewer1.Image = img;
            }
        }
        #endregion

        public void AfficheAppel(DataRow row)
		{
			this.Cursor = Cursors.WaitCursor;
			
			InitialisationHistoriqueFiche();
			// Préparation et remplissage des fiches
			PrepareFicheStatiqueVierge();
			RemplitFicheStatique(row);
			PrepareFicheDynamiqueVierge();
			RemplitFicheDynamique(row);
			// Chargement des rapports d'interventions
			ChargeRapports(row);
			tabTravail.Enabled = true;
			btnOnglet1.Enabled = true;
			btnOnglet2.Enabled  = true;
			btnOnglet3.Enabled  = true;


            ChargementHistoriqueFiche(long.Parse(row["NConsultation"].ToString()));


            this.Cursor = Cursors.Default;

			this.LstAide.Visible = false;
		}

        private void CtMenu_Click(object sender, EventArgs e)
        {
            MenuItem mnu = (MenuItem)sender;
            switch (mnu.Text)
            {                
                case "Fiche de la visite": int Num_Appel = int.Parse(LigneEnCours["NConsultation"].ToString()); 
                                           AfficheFicheConsult(Num_Appel);    //Affichage de la fiche correspondante
                                           break;

                case "Rapport de la visite": int Num_Visite = int.Parse(LigneEnCours["NConsultation"].ToString());
                                             AfficheRapport(Num_Visite);    //Affichage de la fiche correspondante
                                             break;

                case "Voir Historique du patient":
                              string tel = LigneEnCours["TelPatient"].ToString();
                              long indice = long.Parse(LigneEnCours["IndicePatient"].ToString());
                              string nom = LigneEnCours["NomPatient"].ToString();
                              if (nom.Length > 3) nom = nom.Substring(0, 3);

                              Donnees.MonDataSetAppels = RecuperationConsultationByParam(" Where (a.IndicePatient= '" + indice + "' or (pe.Tel = '" + tel + "' and pe.Nom Like '" + nom.Replace("'", "''") + "%')) order by a.DAP DESC", false);
                              AfficheResultat();
                              break;

                case "FIP du patient":
                    long indicepat = long.Parse(LigneEnCours["IndicePatient"].ToString());
                    FIP m_fip = new FIP(this, indicepat, ImportSosGeneve.FIP.TypeOuverture.Patient);
                    m_fip.ShowDialog();
                    break;

                case "Factures du patient":
                    // on affiche la consultation
                    AfficheAppel(LigneEnCours);
                    InitialiseAidePatient();
                    // on simule le clic sur le bouton "Factures"
                    btnOngletTravail(btnOnglet3, null);
                    break;

                case "Supprimer ce destinataire":
                    dstDestination.DestinationRow destrow = (dstDestination.DestinationRow)fpRapport_Destinataires_Sheet1.Cells[fpRapport_Destinataires_Sheet1.ActiveRowIndex, 0].Tag;
                    Donnees.MonDtDestination.Destination.RemoveDestinationRow(destrow);
                    fpRapport_Destinataires_Sheet1.ActiveRow.Remove();
                    break;
                case "Modifier ce destinataire":
                    if (fpRapport_Destinataires_Sheet1.ActiveRowIndex != -1 && fpRapport_Destinataires_Sheet1.Cells[fpRapport_Destinataires_Sheet1.ActiveRowIndex, 0].Tag != null)
                    {
                        dstDestination.DestinationRow DestRow = (dstDestination.DestinationRow)fpRapport_Destinataires_Sheet1.Cells[fpRapport_Destinataires_Sheet1.ActiveRowIndex, 0].Tag;
                        frmAjoutDestinataire frm = new frmAjoutDestinataire(this, Donnees.MonDtRapport.Rapport[0].TypeRapport, true);
                        frm.AfficheMedecin(DestRow);
                        frm.ShowDialog();
                        frm.Dispose();
                        frm = null;
                    }
                    break;
                case "Supprimer Tous les destinataires":
                    fpRapport_Destinataires_Sheet1.RowCount = 0;
                    break;
                default:
                    break;
            }
        }

        private void btnOngletTravail(object sender, EventArgs e)
        {
            PictureBox btn = (PictureBox)sender;

            switch (btn.Name)
            {
                case "btnOnglet1":      //Fiche

                    if (Donnees.MonDtRapport != null && Donnees.SaveRapport == false)
                    {
                        DialogResult result = MessageBox.Show("Le rapport en cours n'a pas encore été sauvegardé. Le faire maintenant?", "Rapport", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (result == DialogResult.No)
                        {
                            MethodeSauvegarde(false);
                        }
                    }

                    tabTravail.SelectedIndex = 0;
                    break;
                case "btnOnglet2":                  //Rapport
                    if (m_frmActualFactu != null)
                    {
                        m_frmActualFactu.Hide();
                    }

                    tabTravail.SelectedIndex = 1;

                    if (btnRapportCourant.Tag != null && btnRapportCourant.BackColor == Color.PaleGreen)
                    {
                        btnRapportCourant_Click(null, null);
                    }

                    break;
                case "btnOnglet3":                //Facturation
                    if (m_frmActualFactu != null)
                    {
                        m_frmActualFactu.Hide();
                    }

                    tabTravail.SelectedIndex = 1;

                    if (VariablesApplicatives.Utilisateurs.Droits == VariablesApplicatives.Utilisateurs.CodeDroits.Comptable
                        || VariablesApplicatives.Utilisateurs.Droits == VariablesApplicatives.Utilisateurs.CodeDroits.Chef
                        || VariablesApplicatives.Utilisateurs.Droits == VariablesApplicatives.Utilisateurs.CodeDroits.Admin)
                    {
                        bool EnCours = false;
                        if (pan_Dynamique.Tag != null)
                            EnCours = true;

                        mnuFacturation.Enabled = false;

                        if (EnCours)
                            m_frmActualFactu = new frmFacturation(this, (DataRow)pan_Dynamique.Tag);
                        else
                            m_frmActualFactu = new frmFacturation(this);

                        m_frmActualFactu.MdiParent = this;
                        panel1.Controls.Add(m_frmActualFactu);
                        panel1.Controls.SetChildIndex(m_frmActualFactu, 0);
                        m_frmActualFactu.Left = 0;
                        m_frmActualFactu.Top = 0;
                        m_frmActualFactu.Show();                       
                    }
                    break;
                default:
                    break;
            }
        }

        public void PrepareFicheStatiqueVierge()
        {
            groupBox1.Text = "Général";
            //pan_Statiques.BackColor = Color.LightCyan;
            groupBox1.BackColor = Color.CadetBlue;
            groupBox2.BackColor = Color.CadetBlue;
            groupBox3.BackColor = Color.CadetBlue;

            lblDateAppel.Text = "";
            lbMotif1.Text = "";
            lbMotif2.Text = "";
            lbUrgence.Text = "";
            lblAnnulation.Text = "";
            lblMotifAnnulation.Text = "";
            lblDevenirAnnulation.Text = "";
            lblMedecin.Text = "";
            lbRCP.Text = "";
            lbSLL.Text = "";
            lbFIN.Text = "";
            lbAge.Text = "";
            lbDelai.Text = "";
            lbDuree.Text = "";

            pan_Statiques.Enabled = false;
        }

        public void PrepareFicheDynamiqueVierge()
        {
            LblStatusSauvegardeFiche.Text = "";

            txtPatient_Nom.Text = "";
            txtPatient_Prenom.Text = "";
            txtPatient_Age.Text = "";
            txtPatient_UniteAge.Text = "";
            txtPatient_Sexe.Text = "";
            CBRoute1.Text = "";
            txtPatient_Adresse1.Text = "";
            txtPatient_NumRue.Text = "";
            txtPatient_Adresse2.Text = "";
            txtPatient_Localite.Text = "";
                     
            EMaskTel1.Text = "";
            
            txtPatient_Longitude.Text = "";
            txtPatient_Latitude.Text = "";
            //txtPatient_Escalier.Text = "";
            txtPatient_Etage.Text = "";
            //txtPatient_Bat.Text = "";
            
            txtPatient_Digicode.Text = "";
            txtPatient_Internom.Text = "";
            txtPatient_Porte.Text = "";
            

            txtPatient_AdmBatiment.Text = "";
            CBRoute_adm1.Text = "";            
            txtPatient_AdmAdresse1.Text = "";
            txtPatient_AdmNumRue.Text = "";
            txtPatient_AdmAdresse2.Text = "";
            txtPatient_AdmNPA.Text = "";
            txtPatient_AdmLocalite.Text = "";

            TxtNPA.Text = "";
            txtDateNaissance.Text = "";

            tBoxComVisite.Text = "";

            TxtEnCours = null;
            TxtChoixMultiple = false;
            LstAide.Visible = false;
            pan_Dynamique.Enabled = false;
        }

		private void RemplitFicheStatique(DataRow row)
		{
			string valeurNulle = "______";

            if(row["AnnulationAppel"].ToString()=="0")
            {
                groupBox1.BackColor = Color.CadetBlue;
                groupBox2.BackColor = Color.CadetBlue;
                groupBox3.BackColor = Color.CadetBlue;
            }
            else
            {
                groupBox1.BackColor = Color.CadetBlue;
                groupBox2.BackColor = Color.CadetBlue;
                groupBox3.BackColor = Color.CadetBlue;
            }

			groupBox1.Text += " : Index " + row["Num"].ToString();
			lblDateAppel.Text = "Appel du : " + DateTime.Parse(row["DAP"].ToString()).ToLongDateString() + " à " + DateTime.Parse(row["DAP"].ToString()).ToShortTimeString();
			if(row["Motif1"].ToString()!="")
				lbMotif1.Text = row["Motif1"].ToString();
			else
				lbMotif1.Text = valeurNulle;
			if(row["Motif2"].ToString()!="")
				lbMotif2.Text = row["Motif2"].ToString();
			else
				lbMotif2.Text = valeurNulle;

			lbUrgence.Text = row["Urgence"].ToString();

            //Si c'est un feu bleu sirene, on affiche l'image
            if (row["CommentaireFichier"].ToString() == "Feux Bleu - Sirènes")
            {
                pictureBox1.Visible = true;
                //MessageBox.Show(row["CommentaireFichier"].ToString());
            }
            else pictureBox1.Visible = false;

			if(row["AnnulationAppel"].ToString()!="0")
			{
				lblAnnulation.Text =  "Annulé à  " + DateTime.Parse(row["DAN"].ToString()).ToShortTimeString();
				lblMotifAnnulation.Text = row["MotifAnnulation"].ToString();
				lblDevenirAnnulation.Text = row["DevenirAnnulation"].ToString();
			}		
			if(row["NomMedecinSos"].ToString()!=System.DBNull.Value.ToString())
				lblMedecin.Text = row["NomMedecinSos"].ToString();
			else
				lblMedecin.Text = "Aucun médecin affecté";
			if(row["DRC"].ToString()!=System.DBNull.Value.ToString())
				lbRCP.Text = DateTime.Parse(row["DRC"].ToString()).ToShortTimeString();
			else
				lbRCP.Text = valeurNulle;
			if(row["DSL"].ToString()!=System.DBNull.Value.ToString())
				lbSLL.Text = DateTime.Parse(row["DSL"].ToString()).ToShortTimeString();
			else
				lbSLL.Text = valeurNulle;
			if(row["DFI"].ToString()!=System.DBNull.Value.ToString())
				lbFIN.Text = DateTime.Parse(row["DFI"].ToString()).ToShortTimeString();
			else
				lbFIN.Text = valeurNulle;
			if(row["Age"].ToString()!=System.DBNull.Value.ToString() && row["UniteAge"].ToString()!=System.DBNull.Value.ToString())
				lbAge.Text = row["Age"].ToString() + " " + row["UniteAge"].ToString();
			else
				lbAge.Text = "???";

			if(row["DSL"].ToString()!=System.DBNull.Value.ToString() && row["DRC"].ToString()!=System.DBNull.Value.ToString())
			{
				TimeSpan delai = DateTime.Parse(row["DSL"].ToString()) - DateTime.Parse(row["DRC"].ToString());
				string nbHour="";
				string nbMinute="";
				if(delai.Hours<10)
					nbHour = "0" + delai.Hours;
				else
					nbHour = delai.Hours.ToString();
				if(delai.Minutes<10)
					nbMinute = "0" + delai.Minutes;
				else
					nbMinute = delai.Minutes.ToString();
				lbDelai.Text = nbHour + ":" + nbMinute;
			}
			if(row["DFI"].ToString()!=System.DBNull.Value.ToString() && row["DSL"].ToString()!=System.DBNull.Value.ToString())
			{
				TimeSpan duree = DateTime.Parse(row["DFI"].ToString()) - DateTime.Parse(row["DSL"].ToString());
				string nbHour="";
				string nbMinute="";
				if(duree.Hours<10)
					nbHour = "0" + duree.Hours;
				else
					nbHour = duree.Hours.ToString();
				if(duree.Minutes<10)
					nbMinute = "0" + duree.Minutes;
				else
					nbMinute = duree.Minutes.ToString();
				lbDuree.Text = nbHour + ":" + nbMinute;
			}

			pan_Statiques.Enabled = true;
		}

        //affectation des champs de la fiche d'après la selection 
		private void RemplitFicheDynamique(DataRow row)
		{
			if(row!=null)
			{	
				txtPatient_Nom.Text = row["NomPatient"].ToString();
				txtPatient_Prenom.Text = row["PrenomPatient"].ToString();
				txtPatient_Age.Text = row["Age"].ToString();
				txtPatient_UniteAge.Text = row["UniteAge"].ToString();
				txtPatient_Sexe.Text = row["Sexe"].ToString();

                //Ajout d'une Combobox pour les routes, rue etc...	
                 //on test le contenu du champs Adresse1
                        
                //Si la rue en minuscule contient route, rue etc...
                //alors on affiche Route ou rue dans le CBRoute1
                if ((row["Rue"].ToString().ToLower().IndexOf("route"))> -1)
                 {
                 CBRoute1.Text = "Route";
                 }
                else if ((row["Rue"].ToString().ToLower().IndexOf("rue"))> -1)
                 {
                    // MessageBox.Show("on est dedans");
                    CBRoute1.Text = "Rue";
                 }
                else if ((row["Rue"].ToString().ToLower().IndexOf("avenue"))> -1)
                 {
                     CBRoute1.Text = "Avenue";
                 }
                 else if ((row["Rue"].ToString().ToLower().IndexOf("boulevard"))> -1)
                 {
                     CBRoute1.Text = "Boulevard";
                 }
                else if ((row["Rue"].ToString().ToLower().IndexOf("place"))> -1)
                 {
                     CBRoute1.Text = "Place";
                 }
                else if ((row["Rue"].ToString().ToLower().IndexOf("passage"))> -1)
                 {
                     CBRoute1.Text = "Passage";
                 }
                 else if ((row["Rue"].ToString().ToLower().IndexOf("sentier"))> -1)
                 {
                     CBRoute1.Text = "Sentier";
                 }
                 else if ((row["Rue"].ToString().ToLower().IndexOf("square"))> -1)
                 {
                     CBRoute1.Text = "Square";
                 }
                 else if ((row["Rue"].ToString().ToLower().IndexOf("chemin"))> -1)
                 {
                     CBRoute1.Text = "Chemin";
                 }
                 else if ((row["Rue"].ToString().ToLower().IndexOf("allée"))> -1)
                 {
                     CBRoute1.Text = "Allée";
                 }
                 else if ((row["Rue"].ToString().ToLower().IndexOf("cité"))> -1)
                 {
                     CBRoute1.Text = "Cité";
                 }
                 else if ((row["Rue"].ToString().ToLower().IndexOf("cours"))> -1)
                 {
                     CBRoute1.Text = "Cours";
                 }
                 else if ((row["Rue"].ToString().ToLower().IndexOf("impasse"))> -1)
                 {
                     CBRoute1.Text = "Impasse";
                 }
                 else if ((row["Rue"].ToString().ToLower().IndexOf("quai")) > -1)
                 {
                     CBRoute1.Text = "Quai";
                 }
                 else
                 {
                     CBRoute1.Text = "";        //c'est aucun de ces choix, donc on met CBRoute1 à blanc
                 }
                 

                txtPatient_Adresse1.Text = row["Rue"].ToString();
                txtPatient_NumRue.Text = row["NumeroDansRue"].ToString();
                txtPatient_Adresse2.Text = row["TexteSup"].ToString();  //Utilisation du champ TexteSup non utilisé
                                                                        //pour Adr2
                txtPatient_Localite.Text = row["Commune"].ToString();
                EMaskTel1.Text = row["TelPatient"].ToString();

                txtEmail.Text = row["Email"].ToString();

				txtPatient_Longitude.Text = row["Longitude"].ToString();
				txtPatient_Latitude.Text = row["Latitude"].ToString();
				txtPatient_Etage.Text = row["Etage"].ToString();
				
                txtPatient_Digicode.Text = row["Digicode"].ToString();
				txtPatient_Internom.Text = row["InterNom"].ToString();
				txtPatient_Porte.Text = row["Porte"].ToString();

               
                //Idem que CBRoute ci-dessus
                if ((row["Adm_Rue"].ToString().ToLower().IndexOf("route")) > -1)
                {
                    CBRoute_adm1.Text = "Route";
                }
                else if (row["Adm_Rue"].ToString().ToLower().IndexOf("rue") > -1)
                {
                    // MessageBox.Show("on est dedans");
                    CBRoute_adm1.Text = "Rue";
                }
                else if ((row["Adm_Rue"].ToString().ToLower().IndexOf("avenue")) > -1)
                {
                    CBRoute_adm1.Text = "Avenue";
                }
                else if ((row["Adm_Rue"].ToString().ToLower().IndexOf("boulevard")) > -1)
                {
                    CBRoute_adm1.Text = "Boulevard";
                }
                else if ((row["Adm_Rue"].ToString().ToLower().IndexOf("place")) > -1)
                {
                    CBRoute_adm1.Text = "Place";
                }
                else if ((row["Adm_Rue"].ToString().ToLower().IndexOf("passage")) > -1)
                {
                    CBRoute_adm1.Text = "Passage";
                }
                else if ((row["Adm_Rue"].ToString().ToLower().IndexOf("sentier")) > -1)
                {
                    CBRoute_adm1.Text = "Sentier";
                }
                else if ((row["Adm_Rue"].ToString().ToLower().IndexOf("square")) > -1)
                {
                    CBRoute_adm1.Text = "Square";
                }
                else if ((row["Adm_Rue"].ToString().ToLower().IndexOf("chemin")) > -1)
                {
                    CBRoute_adm1.Text = "Chemin";
                }
                else if ((row["Adm_Rue"].ToString().ToLower().IndexOf("allée")) > -1)
                {
                    CBRoute_adm1.Text = "Allée";
                }
                else if ((row["Adm_Rue"].ToString().ToLower().IndexOf("cité")) > -1)
                {
                    CBRoute_adm1.Text = "Cité";
                }
                else if ((row["Adm_Rue"].ToString().ToLower().IndexOf("cours")) > -1)
                {
                    CBRoute_adm1.Text = "Cours";
                }
                else if ((row["Adm_Rue"].ToString().ToLower().IndexOf("impasse")) > -1)
                {
                    CBRoute_adm1.Text = "Impasse";
                }
                else if ((row["Adm_Rue"].ToString().ToLower().IndexOf("quai")) > -1)
                {
                    CBRoute_adm1.Text = "Quai";
                }
                else
                {
                    CBRoute_adm1.Text = "";        //c'est aucun de ces choix, donc on met CBRoute_adm1 à blanc
                }

                //pour le type de voie (route, rue etc...)
				txtPatient_AdmAdresse1.Text  = row["Adm_Rue"].ToString();
                txtPatient_AdmNumRue.Text = row["Adm_NumeroDansRue"].ToString();
                txtPatient_AdmAdresse2.Text = row["ListeNoire"].ToString();   //Utilisation du champ ListeNoire non utilisé
                                                                              //pour Adr2_adm 
				txtPatient_AdmNPA.Text  = row["Adm_CodePostal"].ToString();
				txtPatient_AdmLocalite.Text  = row["Adm_Commune"].ToString();
                txtPatient_AdmBatiment.Text = row["Adm_Batiment"].ToString();

				
				if(row["DateNaissance"].ToString()!="")
                  txtDateNaissance.Text = DateTime.Parse( row["DateNaissance"].ToString()).ToString("dd/MM/yyyy");
				else
                  txtDateNaissance.Text = "";
				
				TxtNPA.Text = row["CodePostal"].ToString();

                tBoxComVisite.Text = row["CommentaireAppel"].ToString();

                //affichage de l'icone son, si un fichier son est sur le serveur EPOS              
                string PathFichierSon = SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.Dest_Path_Dictee;
              
                try
                {
                    //on defini le chemin avec le nom complet du fichier
                    string[] dirs = System.IO.Directory.GetFiles(PathFichierSon, row["NConsultation"].ToString() + "*.wav");
                    string[] dirs1 = System.IO.Directory.GetFiles(PathFichierSon, row["NConsultation"].ToString() + "*.3gp");

                    //Si on a quelque chose
                    if ((dirs.Length > 0) || (dirs1.Length > 0))                    
                        PBoxAudio.Visible = true;  //on rend visible l'icone Audio                    
                    else                    
                        PBoxAudio.Visible = false; //sinon on rend invisible l'icone                    
                }
                catch
                {
                    PBoxAudio.Visible = false;
                }

                //Affichage si c'est un encaissé sur place
                if (row["Esp"].ToString() == "1")
                    checkBoxESP.Checked = true;  //on coche la case Encaissé sur place                
                else checkBoxESP.Checked = false; 
                


				pan_Dynamique.Enabled = true;
				pan_Dynamique.Tag = row;
			}			
		}
        #endregion

        #region Historique d'une fiche 

		// Initialisation Historique d'une fiche
        private void InitialisationHistoriqueFiche()
        {
            fpFiche_Historique_Sheet1.RowCount = 0;
            fpFiche_Historique_Sheet1.ColumnCount = 4;

            fpFiche_Historique_Sheet1.Columns[3].Width = 0;
            fpFiche_Historique_Sheet1.Columns[0].Width = 40;
            fpFiche_Historique_Sheet1.Columns[1].Width = 120;
            fpFiche_Historique_Sheet1.Columns[2].Width = 100;

            fpFiche_Historique_Sheet1.GrayAreaBackColor = Color.PapayaWhip;
        }

		// Récupération et chargement de l'historique d'une fiche
        public void ChargementHistoriqueFiche(long NumFiche)
        {
            fpFiche_Historique_Sheet1.RowCount = 0;

            string Requete = "select u.Nom, m.DateModif, m.Type, m.Commentaire " +
                            " from tablemodifications m inner join tableutilisateur u on u.CodeUtilisateur = m.CodeUtilisateur" +
                            " where m.NConsultation = " + NumFiche + " ORDER BY DateModif DESC";
            
            string[][] retour = OutilsExt.OutilsSql.ExecuteCommandeAvecTabString(Requete);
            if (retour != null)
            {
                for (int i = 0; i < retour.Length; i++)
                {
                    string type = "";

                    bool ApparaitHisto = false;

                    switch (int.Parse(retour[i][2]))
                    {
                        case Constantes.MODIF_FICHE:
                            type = "Modification de fiche";
                            break;
                        case Constantes.CREATION_RAPPORT:
                            type = "Création de rapport";
                            ApparaitHisto = true;
                            break;
                        case Constantes.SUPP_RAPPORT:
                            type = "Suppression de rapport";
                            ApparaitHisto = true;
                            break;
                        case Constantes.MODIF_RAPPORT:
                            type = "Modification de rapport";
                            break;
                        case Constantes.ACCORDE_VISA:
                            ApparaitHisto = true;
                            type = "Accord de Visa";
                            break;
                        case Constantes.REFUSE_VISA:
                            ApparaitHisto = true;
                            type = "Refus de Visa";
                            break;
                        case Constantes.MODIF_PATIENT:
                            type = "Modif Patient";
                            break;
                        case Constantes.MODIF_CONSULT:
                            type = "Modif Consult";
                            break;
                        case Constantes.CREATION_FACTURE:
                            ApparaitHisto = true;
                            type = "Création de facture";
                            break;
                        case Constantes.MODIFICATION_FACTURE:
                            ApparaitHisto = true;
                            type = "Modification de facture";
                            break;
                        default:
                            type = "Inconnu";
                            break;
                    }
                    if (ApparaitHisto)
                        AjoutHistoriqueFiche(retour[i][0], type, DateTime.Parse(retour[i][1]), retour[i][3]);
                }
            }
        }

        // Ajout d'une opération dans l'historique d'une fiche
        private void AjoutHistoriqueFiche(string NomUtilisateur, string TypeOperation, DateTime DateOperation, string Commentaire)
        {
            int nb = fpFiche_Historique_Sheet1.RowCount++;
            fpFiche_Historique_Sheet1.Cells[nb, 0].Text = DateOperation.Day + "/" + DateOperation.Month;
            fpFiche_Historique_Sheet1.Cells[nb, 1].Text = TypeOperation;
            fpFiche_Historique_Sheet1.Cells[nb, 2].Text = NomUtilisateur;
            fpFiche_Historique_Sheet1.Cells[nb, 3].Text = Commentaire;
            if (Commentaire != System.DBNull.Value.ToString() && Commentaire != "")
                fpFiche_Historique_Sheet1.Rows[nb].BackColor = Color.MistyRose;
            else
                fpFiche_Historique_Sheet1.Rows[nb].BackColor = Color.PapayaWhip;
        }

		
        //Affichage du commentaire d'un évenement dans la boite commentaire Visa au passage de la souris
        private void fpFiche_Historique_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            FarPoint.Win.Spread.Model.CellRange range = fpFiche_Historique.GetCellFromPixel(0, 0, e.X, e.Y);
            if (range.Row > -1 && range.Column > -1)
            {
                if (fpFiche_Historique_Sheet1.Cells[range.Row, 3].Text != "")
                {
                    //Affichage d'un hint au passage de la souris sur un evennement
                    toolTip1.SetToolTip(fpFiche_Historique,fpFiche_Historique_Sheet1.Cells[range.Row,3].Text);
                    //TxtRapport_Commentaire.Text = fpFiche_Historique_Sheet1.Cells[range.Row, 3].Text;
                    
                    //TxtRapport_CommentaireVisa.Text = fpFiche_Historique_Sheet1.Cells[range.Row, 3].Text;
                }
            }
        }
        #endregion

        #endregion

        #region Rubrique Rapports

        #region Affichage des rapports relatifs à la fiche d'appel sélectionnée

        private void ClearDonneesRapport()
        {
            if (Donnees.MonDtRapport != null)
                Donnees.MonDtRapport.Dispose();
            if (Donnees.MonEtatRapport != null)
                Donnees.MonEtatRapport.Dispose();
            if (Donnees.MonDtDestination != null)
                Donnees.MonDtDestination.Dispose();
            if (Donnees.MonDtCorps != null)
                Donnees.MonDtCorps.Dispose();
            Donnees.MonDtRapport = null;
            Donnees.MonEtatRapport = null;
            Donnees.MesDestinataires = null;
            Donnees.MonDtDestination = null;
            Donnees.MonDtCorps = null;
            Donnees.MonCorpsDeRapport = null;
            lstEnvois.Items.Clear();
        }

        // Affichage des rapports dans la liste avec comme critère de sélection
        // le patient cliqué dans la liste des appels
        private void ChargeRapports(DataRow row)
        {
            ClearDonneesRapport();

            btnRapportCourant.Tag = null;

            if (crystalReportViewer1.ViewCount != -1) crystalReportViewer1.ReportSource = null;
            crystalReportViewer1.RefreshReport();

            fpRapport_Sheet1.RowCount = 0;
            string Nom = row["NomPatient"].ToString();
            while (Nom.Length > 3)
                Nom = Nom.Substring(0, 3);
            string[][] result = OutilsExt.OutilsSql.ExecuteCommandeAvecTabString("SELECT c.NConsultation,r.NRapport,a.Num,pe.Nom as 'NomPatient',a.Tel as 'TelPatient',a.Motif1,a.DAP,m.Nom as 'NomMedecinSos',pe.Prenom as 'PrenomPatient' from tableconsultations c inner join tableactes a on a.Num = c.CodeAppel inner join tablemedecin m on m.CodeIntervenant = a.CodeIntervenant inner join tablepatient pa on c.IndicePatient = pa.IdPatient inner join tablepersonne pe on pe.IdPersonne = pa.IdPersonne left join tablerapports r on r.NConsultation = c.NConsultation WHERE c.IndicePatient = " + row["IndicePatient"] + " order by a.num desc");

            foreach (string[] ligne in result)
            {
                // Affichage des rapports ayant une reliation avec la consultation courante :
                if (ligne[0] != row["NConsultation"].ToString())
                {
                    int nb = fpRapport_Sheet1.RowCount;
                    fpRapport_Sheet1.RowCount += 2;
                    fpRapport_Sheet1.Rows[nb, nb + 1].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                    fpRapport_Sheet1.Cells[nb, 0].Border = new FarPoint.Win.LineBorder(Color.Black, 1, true, true, false, false);
                    fpRapport_Sheet1.Cells[nb, 1].Border = new FarPoint.Win.LineBorder(Color.Black, 1, false, true, true, false);
                    fpRapport_Sheet1.Cells[nb + 1, 0].Border = new FarPoint.Win.LineBorder(Color.Black, 1, true, false, false, true);
                    fpRapport_Sheet1.Cells[nb + 1, 1].Border = new FarPoint.Win.LineBorder(Color.Black, 1, false, false, true, true);
                    if (ligne[1] == null || ligne[1] == System.DBNull.Value.ToString() || ligne[1] == "")
                    {
                        fpRapport_Sheet1.Cells[nb, 0].Tag = ligne[0];
                        fpRapport_Sheet1.Cells[nb + 1, 0].Tag = ligne[0];
                        fpRapport_Sheet1.Cells[nb, 1].Tag = null;
                        fpRapport_Sheet1.Cells[nb + 1, 1].Tag = null;
                        fpRapport_Sheet1.Rows[nb, nb + 1].BackColor = Color.MistyRose;
                    }
                    else
                    {
                        fpRapport_Sheet1.Cells[nb, 0].Tag = ligne[0];
                        fpRapport_Sheet1.Cells[nb + 1, 0].Tag = ligne[0];
                        fpRapport_Sheet1.Cells[nb, 1].Tag = ligne[1];
                        fpRapport_Sheet1.Cells[nb + 1, 1].Tag = ligne[1];
                        fpRapport_Sheet1.Rows[nb, nb + 1].BackColor = Color.PaleGreen;
                    }
                    fpRapport_Sheet1.Cells[nb + 1, 1].Text = ligne[7];
                    fpRapport_Sheet1.Cells[nb, 1].Text = ligne[3] + " " + ligne[8];
                    fpRapport_Sheet1.Cells[nb + 1, 0].Text = ligne[5];
                    fpRapport_Sheet1.Cells[nb, 0].Text = DateTime.Parse(ligne[6]).ToString();

                    fpRapport_Sheet1.Tag = row;
                }
                else
                {
                    // Affichage du rapport courant :

                    // s'il n'a jamais été généré :
                    if (ligne[1] == null || ligne[1] == System.DBNull.Value.ToString() || ligne[1] == "")
                    {
                        btnRapportCourant.BackColor = Color.MistyRose;
                        btnRapportCourant.Text = "Générer ce rapport\r\n" + ligne[3] + " " + ligne[8] + " par " + ligne[7] + "\r\n du " + DateTime.Parse(ligne[6]).ToString();
                        btnRapportCourant.Tag = new long[] { long.Parse(ligne[0]), -1 };
                    }
                    else
                    {
                        btnRapportCourant.BackColor = Color.PaleGreen;
                        btnRapportCourant.Text = "Consultation courante\r\n" + ligne[3] + " " + ligne[8] + " par " + ligne[7] + "\r\n du " + DateTime.Parse(ligne[6]).ToString();
                        btnRapportCourant.Tag = new long[] { long.Parse(ligne[0]), long.Parse(ligne[1]) };
                    }
                    fpRapport_Sheet1.Tag = row;

                    initialise_Lecteur(int.Parse(row["NConsultation"].ToString()));

                    //initialisation du lecteur de dictée intégré uniquement pour Chef (évite le plantage pour les autres)
                    /* if(VariablesApplicatives.Utilisateurs.Droits == VariablesApplicatives.Utilisateurs.CodeDroits.Chef)                    
                         initialise_Lecteur(int.Parse(row["NConsultation"].ToString()));
                     else
                     {
                         Lpasdictee.Visible = false;
                         Bplay.Enabled = false;
                         Bpause.Enabled = false;
                         Bstop.Enabled = false;
                     }*/
                }
            }

            TabActionRapport.Visible = false;
            txtSaisieRapport.Visible = false;          
        }

        #endregion

        #region Sélection d'un rapport de la liste

		// Chargement/Génération d'un rapport sélectionné dans la liste
		private void fpRapport_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			FarPoint.Win.Spread.Model.CellRange range = fpRapport.GetCellFromPixel(0,0,e.X,e.Y);
			if(range.Column>-1 && range.Row>-1)
			{
				fpRapport_Sheet1.SetActiveCell(range.Row,range.Column);

				if(fpRapport_Sheet1.Cells[range.Row,1].Tag!=null)
				{
					this.Cursor = Cursors.WaitCursor;
					AffichageRapport(fpRapport_Sheet1.Cells[range.Row,1].Tag);
                    this.Cursor = Cursors.Default;
				}
				else
				{
                    if (VariablesApplicatives.Utilisateurs.Droits == VariablesApplicatives.Utilisateurs.CodeDroits.Medecin)
					{
                        MessageBox.Show("Ce rapport n'a pas encore été créé.", "Rapport", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}
					
					long NConsult =  long.Parse(fpRapport_Sheet1.Cells[range.Row,0].Tag.ToString());
                    OutilsExt.OutilsSql.CreationRapport(VariablesApplicatives.Utilisateurs.NomUtilisateur, NConsult);
						
					if(fpRapport_Sheet1.Tag!=null)
					{
						DataRow row = (DataRow)fpRapport_Sheet1.Tag;
						this.Cursor = Cursors.WaitCursor;
						ChargeRapports(row);
						this.Cursor = Cursors.Default;

						if(fpRapport_Sheet1.Cells[range.Row,1].Tag!=null)
						{
							this.Cursor = Cursors.WaitCursor;
							AffichageRapport(fpRapport_Sheet1.Cells[range.Row,1].Tag);      
                            this.Cursor = Cursors.Default;
						}
					}
					
					picRapport_OptRapport_Click(null,null);
					TabActionRapport.SelectedIndex = 0;
				}
			}
		}

		//Affichage du rapport dont la fiche est celle sélectionnée
		private void btnRapportCourant_Click(object sender, System.EventArgs e)
		{
			// Si le Tag du bouton est nul, aucune fiche courante :
			if(btnRapportCourant.Tag==null) return;

			// On extrait les deux valeurs : NConsultation et NRapport
			long[] Index = (long[])btnRapportCourant.Tag;
		public void AffichageRapport(object valeur, bool ouvertDepuisListe = false)
            m_rapportOuvertDepuisListe = ouvertDepuisListe;

			if(Index[1]==-1)
			{
                if (VariablesApplicatives.Utilisateurs.Droits == VariablesApplicatives.Utilisateurs.CodeDroits.Medecin)
				{
                    MessageBox.Show("Ce rapport n'a pas encore été créé.", "Rapport", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
				}

                OutilsExt.OutilsSql.CreationRapport(VariablesApplicatives.Utilisateurs.NomUtilisateur, Index[0]);						
				ChargeRapports((DataRow)fpRapport_Sheet1.Tag);
				btnRapportCourant_Click(null,null);	
				picRapport_OptRapport_Click(null,null);
				TabActionRapport.SelectedIndex = 0;           
			}
			// sinon on affiche le rapport simplement
			else
			{
				this.Cursor = Cursors.WaitCursor;
				AffichageRapport(Index[1]);
				this.Cursor = Cursors.Default;         
			}
		}

		// Affichage d'un rapport gйnйrй
		public void AffichageRapport(object valeur, bool ouvertDepuisListe = false)
		{
			LblSauvegardeRapport.Text = "";

			this.Cursor = Cursors.WaitCursor;

            m_rapportOuvertDepuisListe = ouvertDepuisListe;


            try
            {
			    long IdRapport = long.Parse(valeur.ToString());				

			    // On Ré-initialise la liste des destinataires
			    InitialiseListeDestinataires(null);

			    // on Réinitialise tous les dataset et Etats CrystalReport
                Donnees.MonDtRapport = new dstRapport();
			    Donnees.MesDestinataires = new Destinataire[0];
                Donnees.MonDtDestination = new dstDestination();
                Donnees.MonDtCorps = new dstCorps();

			    if(Donnees.MonEtatRapport!=null)
			    {
				    Donnees.MonEtatRapport.Close();
				    Donnees.MonEtatRapport.Dispose();
			    }
			    if(Donnees.MonSansRapport!=null)
			    {
				    Donnees.MonSansRapport.Close();
				    Donnees.MonSansRapport.Dispose();
			    }


			    // On récupere les données liées au rapport sélectionné
                OutilsExt.OutilsSql.RemplitDataTable(Donnees.MonDtRapport.Rapport, @"SELECT c.Morphine, c.Pethidine, c.Fentanyl, c.Methadone, c.Dormicum,
                                                     c.Autre_stup, c.Autre_stup_qte, c.Auteur, c.Type_long_rapport, AnnulationAppel, c.RegulationCorrecte ,
                                                     c.AdresseCorrecte , r.NRapport,r.NConsultation,c.CodeAppel,c.IndicePatient as 'CodePatient',r.DateRapport,
                                                     r.TYpeRapport,r.Commentaire,r.RapScribe,r.RapEnTete,r.RapCopie,r.RapConcerne,r.RapSignature,r.RapReference,
                                                     r.medecin_viseur,m.Nom as 'NomMedecinSos',a.CodeIntervenant,pe.Prenom as 'PrenomPatient',pe.Nom as 'NomPatient',
                                                     pe.Sexe,pe.Age,pe.UniteAge,a.Motif1,pe.DateNaissance,pe.Adm_NumeroDansRue as 'NumRue',pe.Adm_Rue as 'Rue',
                                                     pe.Adm_Commune as 'Commune',pe.Tel,pe.Adm_CodePostal as 'CodePostal',a.DAP,a.DFI,c.Deces,a.DSL,r.Vise,r.ACorriger,
                                                     m.Mail, c.PriseEnChargePatient 
                                                     from tablerapports r inner join tableconsultations c on c.NConsultation = r.NConsultation
                                                                          inner join tableactes a on a.Num =c.CodeAppel 
                                                                          inner join tablepatient pa on pa.IdPatient = c.IndicePatient 
                                                                          inner join tablepersonne pe on pe.IdPersonne = pa.IdPersonne 
                                                                          inner join tablemedecin m on m.CodeIntervenant = a.CodeIntervenant
                                                    WHERE r.NRapport = " + IdRapport);
			    int TypeRapport = Donnees.MonDtRapport.Rapport[0].TypeRapport;

			    // ------------------------------------------
			    // Affichage d'un rapport médical ou d'un constat
			    if(TypeRapport==1 || TypeRapport==2)
			    {                    
				    Donnees.MonEtatRapport = new RapportPatient();
				    // corps du rapport + liste des destinataires :
				    OutilsExt.OutilsSql.RemplitDataTable(Donnees.MonDtCorps.Corps,"SELECT r.NRapport,r.IdCategorie,r.Valeur as 'ValeurCategorie',c.LibelleCategorie, c.Fixe as Active from tablerapportcorps r inner join tablecategoriedansrapport c on c.IdCategorie = r.IdCategorie and c.TypeRapport = " + TypeRapport +  "  where r.NRapport = " + IdRapport + " order by c.Ordre ASC");
				    OutilsExt.OutilsSql.RemplitDataTable(Donnees.MonDtDestination.Destination,"SELECT r.mail, r.RapModeEnvoi, 1 as Intime, r.Nom,r.NRapport,r.TypeDestinataire,r.CodeDestinataire,r.RapDestinataire,r.RapBonjour,r.RapIntroduction,RapSalutation,r.Logo,r.Copie,r.RapEnvoye,r.DateEnvoi from tablerapportdestine r where r.NRapport = " + IdRapport);

				    rtfConvert.Text = "";
                    rtfConvert.Font = new Font(SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.PoliceDefault().FontFamily, SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.PoliceDefault().Size, SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.PoliceDefault().Style);

				    // On fabrique le corps du rapport :
				    for(int i=0;i<Donnees.MonDtCorps.Corps.Count;i++)
				    {
					    if(Donnees.MonDtCorps.Corps[i].ValeurCategorie!="")
					    {
                            string ss = "";

                            if (Donnees.MonDtCorps.Corps[i].IdCategorie == 13)                            
                                ss = "\r\n" + Donnees.MonDtCorps.Corps[i].LibelleCategorie + " : ";                            
                            else                            
                                ss = Donnees.MonDtCorps.Corps[i].LibelleCategorie + " : ";
                                                                                                                                           
                            string ss1 = Donnees.MonDtCorps.Corps[i].ValeurCategorie;
                            rtfConvert.Text += ss + ss1 + "\r\n\r\n";                            
					    }
				    }
				    for(int i=0;i<Donnees.MonDtCorps.Corps.Count;i++)
				    {
					    if(Donnees.MonDtCorps.Corps[i].LibelleCategorie!="")
					    {
                            string ss = Donnees.MonDtCorps.Corps[i].LibelleCategorie + " : ";

                            int start = rtfConvert.Text.IndexOf(ss);
						    if(start>-1)
						    {
							    rtfConvert.SelectionStart=start;
							    rtfConvert.SelectionLength = ss.Length;
                                rtfConvert.SelectionFont = new Font(SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.PoliceDefault().FontFamily, SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.PoliceDefault().Size, FontStyle.Bold);
						    }
					    }
				    }
    			
				    //Selon le nombre de lignes, on change la taille de la police
                    float PoliceSize = SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.PoliceDefault().Size;
				    string[] TableauLignes = rtfConvert.Text.Split('\n');
    			
				    int nbLigne = TableauLignes.Length;
				    if(nbLigne>26 && nbLigne <30)
					    PoliceSize=10;
    				
				    // on est obligés de recommencer la manip pour attribuer la bonne taille
				    // On fabrique le corps du rapport :
				    rtfConvert.Text = "";
                    rtfConvert.Font = new Font(SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.PoliceDefault().FontFamily, PoliceSize, SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.PoliceDefault().Style);
				    for(int i=0;i<Donnees.MonDtCorps.Corps.Count;i++)
				    {
					    if(Donnees.MonDtCorps.Corps[i].ValeurCategorie!="")
					    {
                            string ss = "";

                            if (Donnees.MonDtCorps.Corps[i].IdCategorie == 13)
                                ss = "\r\n" + Donnees.MonDtCorps.Corps[i].LibelleCategorie + " : " ;
                            else
                                ss = Donnees.MonDtCorps.Corps[i].LibelleCategorie + " : ";

                            string ss1 = Donnees.MonDtCorps.Corps[i].ValeurCategorie;
						    rtfConvert.Text+=ss + ss1 + "\r\n\r\n";					
					    }
				    }
				    for(int i=0;i<Donnees.MonDtCorps.Corps.Count;i++)
				    {
					    if(Donnees.MonDtCorps.Corps[i].LibelleCategorie!="")
					    {
                            string ss = Donnees.MonDtCorps.Corps[i].LibelleCategorie + " : ";
                            
						    int start = rtfConvert.Text.IndexOf(ss);
						    if(start>-1)
						    {
							    rtfConvert.SelectionStart=start;
							    rtfConvert.SelectionLength = ss.Length;
                                rtfConvert.SelectionFont = new Font(SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.PoliceDefault().FontFamily, PoliceSize, FontStyle.Bold);
						    }
					    }
				    }

				    if(rtfConvert.Text=="") rtfConvert.Text = " ";
				    rtfConvert.SelectionStart  = 0;
				    rtfConvert.SelectionLength = rtfConvert.Text.Length;	
				    Donnees.MonDtRapport.Rapport[0].RapCorps = rtfConvert.SelectedRtf;	
    				
				    if(TypeRapport==2) Donnees.MonDtRapport.Rapport[0].RapBonjour="";
				    if(Donnees.MonDtRapport.Rapport[0].RapSignature=="") Donnees.MonDtRapport.Rapport[0].RapSignature = Donnees.MonDtRapport.Rapport[0].NomMedecinSos;	

				    // Travail sur les données formatées :
				    Donnees.MonDtRapport.Rapport[0].NomPatient = Donnees.MonDtRapport.Rapport[0].NomPatient.ToUpper();
				    if(Donnees.MonDtRapport.Rapport[0].PrenomPatient!="") 
					    Donnees.MonDtRapport.Rapport[0].PrenomPatient = Donnees.MonDtRapport.Rapport[0].PrenomPatient.Substring(0,1).ToUpper() + Donnees.MonDtRapport.Rapport[0].PrenomPatient.Substring(1).ToLower();

				    InitialiseListeDestinataires(Donnees.MesDestinataires);
				    // on reconstruit la liste des destinataires
				    for(int i=0;i<Donnees.MonDtDestination.Destination.Count;i++)
				    {
					    AjouteDestinataires(Donnees.MonDtDestination.Destination[i]);
				    }

				    if(Donnees.MonDtDestination.Destination.Count>0)
				    {
					    AffichageRapportAvecDestinataire(Donnees.MonDtDestination.Destination[0]);
				    }
    					
				    InitialiseBoutonsRapport(Donnees.MonDtRapport.Rapport[0].TypeRapport);
    			
				    txtSaisieRapport.Text="";
				    txtSaisieRapport.Tag = null;
				    txtSaisieRapport.ForeColor = Color.Black;
				    txtSaisieRapport.Font = new Font("Arial",12,FontStyle.Regular);

				    Donnees.SaveRapport = false;

				    // Onglet d'action selon le profil utilisateur
				    TabActionRapport.Visible = true;
				    txtSaisieRapport.Visible = true;
                    if (VariablesApplicatives.Utilisateurs.Droits == VariablesApplicatives.Utilisateurs.CodeDroits.Secretaire)
					    TabActionRapport.SelectedIndex = 0;
                    else if (VariablesApplicatives.Utilisateurs.Droits == VariablesApplicatives.Utilisateurs.CodeDroits.Medecin)
					    TabActionRapport.SelectedIndex = 2;
                    else if (VariablesApplicatives.Utilisateurs.Droits == VariablesApplicatives.Utilisateurs.CodeDroits.Chef
                              || VariablesApplicatives.Utilisateurs.Droits == VariablesApplicatives.Utilisateurs.CodeDroits.Comptable)
					    TabActionRapport.SelectedIndex = 1;


				    // on attribue le dataset au viewer

                    if (VariablesApplicatives.Utilisateurs.Droits != VariablesApplicatives.Utilisateurs.CodeDroits.Secretaire)
				    {
					    this.crystalReportViewer1.Size = new System.Drawing.Size(640, 625);     //640,830
                        this.crystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
                        //this.cmdRapport_Intro.Visible = false;
                        //this.cmdRapport_EnTete.Visible = false;
                        //this.cmdRapport_Corps.Visible = false;
                        //this.cmdRapport_Bonjour.Visible = false;
                        //this.cmdRapport_Concerne.Visible = false;
                        //this.cmdRapport_Salutations.Visible = false;
                        //this.cmdRapport_Signature.Visible = false;
                        //this.cmdRapport_Destinataire.Visible = false;
                        //this.txtSaisieRapport.Visible = false;
                        //this.lblTest.Visible = false;
                        //this.picRapport_Actualiser.Visible = false;
                        //this.label43.Visible = false;
                        //this.btnRapport_Font.Visible = false;
                        //this.btnRapport_Couleur.Visible = false;

                        this.cmdRapport_Intro.Visible = true;
                        this.cmdRapport_EnTete.Visible = true;
                        this.cmdRapport_Corps.Visible = true;
                        this.cmdRapport_Bonjour.Visible = true;
                        this.cmdRapport_Concerne.Visible = true;
                        this.cmdRapport_Salutations.Visible = true;
                        this.cmdRapport_Signature.Visible = true;
                        this.cmdRapport_Destinataire.Visible = true;
                        this.txtSaisieRapport.Visible = true;
                        this.lblTest.Visible = true;
                        this.picRapport_Actualiser.Visible = true;
                        this.label43.Visible = true;
                        this.btnRapport_Font.Visible = true;
                        this.btnRapport_Couleur.Visible = true;

                    }

                    Donnees.MonEtatRapport.SetDataSource(Donnees.MonDtRapport);////
				    crystalReportViewer1.ReportSource = Donnees.MonEtatRapport;
                    CrystalUtility.FontInReport(Donnees.MonEtatRapport, "Rap", SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.PoliceDefault());	
				    // Variantes selon le constat ou le rapport médical 
				    if(Donnees.MonDtRapport.Rapport[0].TypeRapport==1)      //Normal
				    {
					    CrystalUtility.UpdateObject(Donnees.MonEtatRapport,"RapC","Concerne :");
					    CrystalUtility.StyleObject(Donnees.MonEtatRapport, "RapC", true, SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.PoliceDefault().Bold, SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.PoliceDefault().Italic);
                        CrystalUtility.StyleObject(Donnees.MonEtatRapport, "RapConcerne", false, SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.PoliceDefault().Bold, SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.PoliceDefault().Italic);
                        cmdRapport_Concerne.Tag = new Font(SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.PoliceDefault().FontFamily, SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.PoliceDefault().Size, SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.PoliceDefault().Style);
				    }
				    else if(Donnees.MonDtRapport.Rapport[0].TypeRapport==2)     //Constat
				    {
					    CrystalUtility.UpdateObject(Donnees.MonEtatRapport,"RapC","");					                         
                        CrystalUtility.StyleObject(Donnees.MonEtatRapport, "RapConcerne", true, true, SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.PoliceDefault().Italic);
                        cmdRapport_Concerne.Tag = new Font(SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.PoliceDefault().FontFamily, SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.PoliceDefault().Size, (FontStyle)5);
				    }

				    AffichageEtatRapport();
				    AffichageEnvoisDuRapport();

				    
                    //on initialise les signatures à invisible (true)
                    CrystalUtility.SetVisibleImage(Donnees.MonEtatRapport, "Picture1", true);
                    CrystalUtility.SetVisibleImage(Donnees.MonEtatRapport, "Picture2", true);
                    CrystalUtility.SetVisibleImage(Donnees.MonEtatRapport, "Picture3", true);
                    //CrystalUtility.SetVisibleImage(Donnees.MonEtatRapport, "Picture4", true);
                    CrystalUtility.SetVisibleImage(Donnees.MonEtatRapport, "Picture5", true);
                    
                    // Si le rapport est visé on fait apparaitre la signature numérique :
				    bool bVise = false;
				   
                    // if(Donnees.MonDtRapport.Rapport[0].Vise==1) bVise=true;
                   // CrystalUtility.SetVisibleImage(Donnees.MonEtatRapport,"Picture1",!bVise);

                    if (Donnees.MonDtRapport.Rapport[0].Vise == 1)
                    {
                        bVise = true;

                        //en fonction de la personne qui a signé, on affiche sa signature
                        switch (Donnees.MonDtRapport.Rapport[0].Medecin_viseur.ToString())
                        {
                            case "340":      //FDX
                                CrystalUtility.SetVisibleImage(Donnees.MonEtatRapport, "Picture1", !bVise);
                                break;
                            case "2861":     //Benedicte
                                CrystalUtility.SetVisibleImage(Donnees.MonEtatRapport, "Picture2", !bVise);
                                break;
                            case "2872":     //Xavier Chung Minh
                                CrystalUtility.SetVisibleImage(Donnees.MonEtatRapport, "Picture3", !bVise);
                                break;                           
                            case "121":      //Korine
                                CrystalUtility.SetVisibleImage(Donnees.MonEtatRapport, "Picture5", !bVise);
                                break;
                            case "2908":      //Pelet François
                                CrystalUtility.SetVisibleImage(Donnees.MonEtatRapport, "Picture6", !bVise);
                                break;
                            case "D614":      //MBAYO Paul
                                CrystalUtility.SetVisibleImage(Donnees.MonEtatRapport, "Picture7", !bVise);
                                break;
                            default:         //MBAYO Paul
                                CrystalUtility.SetVisibleImage(Donnees.MonEtatRapport, "Picture7", !bVise);
                                break;
                        }
                    }
                }
			    // Affichage d'un 'Sans Rapport'
			    else if(TypeRapport==3)
			    {
				    Donnees.SaveRapport = false;

                    dstRapport.RapportRow tb = Donnees.MonDtRapport.Rapport[0];

				    tb.Sans1 = "Médecin n° " + tb.CodeIntervenant + " : " + tb.NomMedecinSos;
                    tb.Sans2 = "Secrétaire : " + VariablesApplicatives.Utilisateurs.NomUtilisateur;
				    tb.Sans3 = "Consultation n° " + tb.NConsultation + ", Appel " + tb.CodeAppel + " du ";
				    if(!tb.IsDSLNull())
					    tb.Sans3 += tb.DSL.ToLongDateString() + " à " + tb.DSL.ToShortTimeString();
				    else if(!tb.IsDFINull())
					    tb.Sans3 += tb.DFI;
				    else if(!tb.IsDAPNull())
					    tb.Sans3 += tb.DAP.ToLongDateString() + " à " + tb.DAP.ToShortTimeString();

				    //tb.Sans4 = WorkedString.FirstLetterUpper(tb.PrenomPatient) + " " + tb.NomPatient.ToUpper();
                    tb.Sans4 = tb.PrenomPatient + " " + tb.NomPatient.ToUpper();
				    if(tb.Sexe=="F")
					    tb.Sans5="Femme";
				    else if(tb.Sexe=="E")
					    tb.Sans5 ="Enfant";
				    else
					    tb.Sans5="Homme";
				    tb.Sans5+= "\t	" + tb.Rue + " " + tb.NumRue + ", " + tb.CodePostal + " " + tb.Commune;
				    tb.Sans5+="\r\n"  + tb.Age + " " + WorkedString.GetAgeFormate(tb.UniteAge) + "\t	" + "Tel : " + tb.Tel;
				    tb.Sans6 = "Symptômes : " + tb.Motif1 + "\r\nMédecin Traitant";

				    TabActionRapport.Visible = true;
				    txtSaisieRapport.Visible = true;
                    if (VariablesApplicatives.Utilisateurs.Droits == VariablesApplicatives.Utilisateurs.CodeDroits.Secretaire)
					    TabActionRapport.SelectedIndex = 0;
                    else if (VariablesApplicatives.Utilisateurs.Droits == VariablesApplicatives.Utilisateurs.CodeDroits.Medecin)
					    TabActionRapport.SelectedIndex = 2;
                    else if (VariablesApplicatives.Utilisateurs.Droits == VariablesApplicatives.Utilisateurs.CodeDroits.Chef
                         || VariablesApplicatives.Utilisateurs.Droits == VariablesApplicatives.Utilisateurs.CodeDroits.Comptable)
					    TabActionRapport.SelectedIndex = 1;

				    Donnees.MonSansRapport = new SansRapport();
    				
				    Donnees.MonSansRapport.SetDataSource(Donnees.MonDtRapport);
				    crystalReportViewer1.ReportSource = Donnees.MonSansRapport;				
				    InitialiseBoutonsRapport(3);
                    if (VariablesApplicatives.Utilisateurs.Droits != VariablesApplicatives.Utilisateurs.CodeDroits.Secretaire)
				    {
					    this.crystalReportViewer1.Size = new System.Drawing.Size(640, 625);
                        //this.cmdRapport_Intro.Visible = false;
                        //this.cmdRapport_EnTete.Visible = false;
                        //this.cmdRapport_Corps.Visible = false;
                        //this.cmdRapport_Bonjour.Visible = false;
                        //this.cmdRapport_Concerne.Visible = false;
                        //this.cmdRapport_Salutations.Visible = false;
                        //this.cmdRapport_Signature.Visible = false;
                        //this.cmdRapport_Destinataire.Visible = false;

                        this.cmdRapport_Intro.Visible = true;
                        this.cmdRapport_EnTete.Visible = true;
                        this.cmdRapport_Corps.Visible = true;
                        this.cmdRapport_Bonjour.Visible = true;
                        this.cmdRapport_Concerne.Visible = true;
                        this.cmdRapport_Salutations.Visible = true;
                        this.cmdRapport_Signature.Visible = true;
                        this.cmdRapport_Destinataire.Visible = true;
                    }
                }
			    // Bouton Fin de Correction non dispo si le rapport n'est pas à corriger
			    if(Donnees.MonDtRapport.Rapport[0].ACorriger==1)
				    btnEnlevCorrection.Visible = true;
			    else
				    btnEnlevCorrection.Visible = false;

			    // Raffrachissement des données sur l'écran			
			    crystalReportViewer1.RefreshReport();
			    crystalReportViewer1.Zoom(85);	
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

			this.Cursor = Cursors.Default;

		}

        #endregion

        #region Création du rapport = Rapport // Constat // Sans Rapport

		// Création d'un rapport normal
		private void picRapport_OptRapport_Click(object sender, System.EventArgs e)
		{	
			LblSauvegardeRapport.Text = "";

		    if(Donnees.MonDtRapport==null ||  crystalReportViewer1.ActiveViewIndex==-1) return;
			
			if(Donnees.MonDtCorps.Corps.Count>0 || Donnees.MonDtDestination.Destination.Count>0)
			{
                if (MessageBox.Show("Attention, si vous continuez le corps du rapport et la liste des destinataires vont être ré-initialisés ! \r\n\r\nVoulez vous continuer ?", "Nouvelle création", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
			}

			Donnees.MonEtatRapport = new RapportPatient();
			
			// Fabrication des champs du rapport
            dstRapport.RapportRow tb = Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex];

			tb.RapBonjour="";
			tb.RapDestinataire="";
			tb.RapSalutation="";

            tb.RapEnTete = SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.CommuneEditionRapports + ", le " + tb.DateRapport.ToShortDateString();
			tb.RapDestinataire = "";
			tb.RapConcerne = "";
			tb.RapConcerne+= tb.PrenomPatient + " " + tb.NomPatient;
			string age = "";
			if(tb.DateNaissance!=System.DBNull.Value.ToString())
			{
				age = ", né";
				if(tb.Sexe=="F")
					age+="e";
				age+= " le " + DateTime.Parse(tb.DateNaissance).ToShortDateString();
			}
			else if (tb.Age.ToString()!="" && tb.Age!=0)
			{
				age = tb.Age.ToString() + " " + WorkedString.GetAgeFormate(tb.UniteAge);
			}
			tb.RapConcerne+=age;
			string adresse = "\r\n";
			
			if(tb.Rue!="")
			{
				/*if(tb.Rue.Split(',').Length==2)
					adresse+= WorkedString.FirstLetterUpper(tb.Rue.Split(',')[1]) + " " + WorkedString.FirstLetterUpper(tb.Rue.Split(',')[0]);
				else
					adresse+= WorkedString.FirstLetterUpper(tb.Rue);*/
	            adresse+= tb.Rue;
			}
			if(tb.Rue!="" && tb.NumRue!="")	adresse+= " " + tb.NumRue;
			adresse+="\r\n";
			if(tb.CodePostal!="")
				adresse+=tb.CodePostal + " ";
			
            /*if(tb.Commune.Split(',').Length==2)
			{
				adresse+= WorkedString.FirstLetterUpper(tb.Commune.Split(',')[1].TrimEnd() ) + " " + WorkedString.FirstLetterUpper(tb.Commune.Split(',')[0]);
			}
			else
			{
				adresse+=WorkedString.FirstLetterUpper(tb.Commune);
			}*/

            adresse += tb.Commune;
			            
			tb.RapConcerne+=adresse;

            //Ajout du tel à la suite de l'adresse *****Domi 02.04.2014
            tb.RapConcerne+="\r\n";
            tb.RapConcerne += "Tel: " + tb.Tel;
            //***

			fpRapport_Destinataires_Sheet1.RowCount = 0;
			Donnees.MonDtCorps.Clear();
			Donnees.MonDtDestination.Clear();
			tb.RapCorps = "";
			tb.RapBonjour = "";
			tb.RapIntroduction = "";
			tb.RapSignature = "";

			// Sélection d'office en interne : 
			frmAjoutDestinataire frm = new frmAjoutDestinataire(this,1);
            dstDestination.DestinationRow DestRow = null;

			// ***
			// Est-ce qu'il y a des médecins traitants assignés à ce patient?
			// Si oui on les sléectionne par défaut,
			// Si non on met automatiquement interne
			string[][] medTT = OutilsExt.OutilsSql.ExecuteCommandeAvecTabString("SELECT IdMedecin from tablepatientmedttt where IdPatient = " + tb.CodePatient);
			if(medTT==null || medTT.Length==0)
			{
				DestRow = frm.SelectionAutomatiquePourRapport();
				AjouteDestinataires(DestRow);
			}
			else
			{
				for(int i=0;i<medTT.Length;i++)
				{
					DestRow = frm.SelectionAutomatiqueMedTTTPourRapport(int.Parse(medTT[i][0]));
					AjouteDestinataires(DestRow);
				}
			}
			// ***
			
			frm.Close();
			frm.Dispose();
			frm=null;

			// signature en fin de rapport : 
			if(tb.NomMedecinSos.Split(' ').Length == 1)
			{
				tb.RapSignature = tb.NomMedecinSos.ToUpper();
			}
			else if(tb.NomMedecinSos.Split(' ').Length == 2)
			{
				tb.RapSignature = WorkedString.FirstLetterUpper(tb.NomMedecinSos.Split(' ')[1]) + " " + tb.NomMedecinSos.Split(' ')[0].ToUpper();
			}
			else
				tb.RapSignature = tb.NomMedecinSos.ToUpper();

			// Libelles statiques différents selon rapport médical ou constat 
			CrystalUtility.UpdateObject(Donnees.MonEtatRapport,"RapC","Concerne :");			
			//CrystalUtility.ReLocateObject(Donnees.MonEtatRapport,"RapIntroduction",-1,6480);
          //  CrystalUtility.ReLocateObject(Donnees.MonEtatRapport, "RapIntroduction", -1, 5800);
            CrystalUtility.StyleObject(Donnees.MonEtatRapport, "RapC", true, SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.PoliceDefault().Bold, SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.PoliceDefault().Italic);
            CrystalUtility.StyleObject(Donnees.MonEtatRapport, "RapConcerne", false, SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.PoliceDefault().Bold, SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.PoliceDefault().Italic);
            cmdRapport_Concerne.Tag = new Font(SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.PoliceDefault().FontFamily, SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.PoliceDefault().Size, SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.PoliceDefault().Style);

			Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex].TypeRapport = 1;
			InitialiseBoutonsRapport(1);	
		
			AffichageRapportAvecDestinataire(DestRow);
		}

		private void picRapport_OptConstat_Click(object sender, System.EventArgs e)
		{
			LblSauvegardeRapport.Text = "";

			if(Donnees.MonDtRapport==null || crystalReportViewer1.ActiveViewIndex==-1) return;
			
			if(Donnees.MonDtCorps.Corps.Count>0 || Donnees.MonDtDestination.Destination.Count>0)
			{
                if (MessageBox.Show("Attention, si vous continuez le corps du rapport et la liste des destinataires vont être ré-initialisés ! \r\n\r\nVoulez vous continuer ?", "Nouvelle création", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
			}

			Donnees.MonEtatRapport = new RapportPatient();

            dstRapport.RapportRow tb = Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex];

			tb.RapBonjour="";
			tb.RapDestinataire="";
			tb.RapSalutation="";

            tb.RapEnTete = SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.CommuneEditionRapports + ", le " + tb.DateRapport.ToShortDateString();
			tb.RapDestinataire ="";
			tb.RapConcerne = "Constat de lésions traumatiques concernant ";
			tb.RapConcerne+=WorkedString.GetSexeFormate(tb.Sexe);
            //tb.RapConcerne+= " " + WorkedString.FirstLetterUpper(tb.PrenomPatient) + " " + tb.NomPatient.ToUpper() + ", ";
            tb.RapConcerne += " " + tb.PrenomPatient + " " + tb.NomPatient.ToUpper() + ", ";
			string age = "";
			if(tb.DateNaissance!=System.DBNull.Value.ToString())
			{
				age = "né";
				if(tb.Sexe=="F")
					age+="e";
				age+= " le " + DateTime.Parse(tb.DateNaissance).ToShortDateString();
			}
			else if (tb.Age.ToString()!="" && tb.Age!=0)
			{
				age = tb.Age.ToString() + " " + WorkedString.GetAgeFormate(tb.UniteAge);
			}
			tb.RapConcerne+=age + ".";

			string adresse = "";
			if(tb.Rue!="" && tb.NumRue!="")
				adresse+=tb.NumRue + ", ";
			
            /*if(tb.Rue!="")
			{
				if(tb.Rue.Split(',').Length==2)
					adresse+= WorkedString.FirstLetterUpper(tb.Rue.Split(',')[1]) + " " + WorkedString.FirstLetterUpper(tb.Rue.Split(',')[0]);
				else
					adresse+=  WorkedString.FirstLetterUpper(tb.Rue);
			}*/
            adresse += tb.Rue;

			adresse+= " à ";
			if(tb.CodePostal!="")
				adresse+=tb.CodePostal + " ";
			
			/*if(tb.Commune.Split(',').Length==2)
				adresse+=WorkedString.FirstLetterUpper(tb.Commune.Split(',')[1].TrimEnd()) + " " + WorkedString.FirstLetterUpper(tb.Commune.Split(',')[0]);
			else
				adresse+=WorkedString.FirstLetterUpper(tb.Commune);*/
				
			adresse+=tb.Commune;

			string heurefi = "";
			if(tb.DFI!=System.DBNull.Value.ToString())
				heurefi = " à " + DateTime.Parse(tb.DFI).ToShortTimeString();

			fpRapport_Destinataires_Sheet1.RowCount = 0;
			tb.RapBonjour = "";
			tb.RapIntroduction = "";
			Donnees.MonDtCorps.Clear();
			Donnees.MonDtDestination.Clear();
			tb.RapCorps="";
			tb.RapSalutation = "";
		
			// Sélection d'office de l'hôtel de police en destinataire : 
            frmAjoutDestinataire frm = new frmAjoutDestinataire(this,2);
            dstDestination.DestinationRow DestRow = frm.SelectionAutomatiquePourConstat();
			AjouteDestinataires(DestRow);
			frm.Close();
			frm.Dispose();
			frm=null;

			// signature en fin de rapport : 
			if(tb.NomMedecinSos.Split(' ').Length == 1)
			{
				tb.RapSignature =  tb.NomMedecinSos.ToUpper();
			}
			else if(tb.NomMedecinSos.Split(' ').Length == 2)
			{
				tb.RapSignature =   WorkedString.FirstLetterUpper(tb.NomMedecinSos.Split(' ')[1]) + " " + tb.NomMedecinSos.Split(' ')[0].ToUpper();
			}
			else
				tb.RapSignature =  tb.NomMedecinSos.ToUpper();


            Donnees.MonEtatRapport.SetDataSource(Donnees.MonDtRapport);
            if (VariablesApplicatives.Utilisateurs.Droits != VariablesApplicatives.Utilisateurs.CodeDroits.Secretaire)
			{
				this.crystalReportViewer1.Size = new System.Drawing.Size(640, 625);
                //this.cmdRapport_Intro.Visible = false;
                //this.cmdRapport_EnTete.Visible = false;
                //this.cmdRapport_Corps.Visible = false;
                //this.cmdRapport_Bonjour.Visible = false;
                //this.cmdRapport_Concerne.Visible = false;
                //this.cmdRapport_Salutations.Visible = false;
                //this.cmdRapport_Signature.Visible = false;
                //this.cmdRapport_Destinataire.Visible = false;
                this.cmdRapport_Intro.Visible = true;
                this.cmdRapport_EnTete.Visible = true;
                this.cmdRapport_Corps.Visible = true;
                this.cmdRapport_Bonjour.Visible = true;
                this.cmdRapport_Concerne.Visible = true;
                this.cmdRapport_Salutations.Visible = true;
                this.cmdRapport_Signature.Visible = true;
                this.cmdRapport_Destinataire.Visible = true;
            }
            crystalReportViewer1.ReportSource = Donnees.MonEtatRapport;

			CrystalUtility.UpdateObject(Donnees.MonEtatRapport,"RapC","");	          
            CrystalUtility.StyleObject(Donnees.MonEtatRapport, "RapConcerne", true, true, SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.PoliceDefault().Italic);
            cmdRapport_Concerne.Tag = new Font(SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.PoliceDefault().FontFamily, SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.PoliceDefault().Size, (FontStyle)5);
		
			Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex].TypeRapport = 2;
			InitialiseBoutonsRapport(2);		
			AffichageRapportAvecDestinataire(DestRow);
		}

		private void picRapport_OptSans_Click(object sender, System.EventArgs e)
		{
			LblSauvegardeRapport.Text = "";

			if(Donnees.MonDtRapport==null  || crystalReportViewer1.ActiveViewIndex==-1) return;
			
			if(Donnees.MonDtCorps.Corps!=null && (Donnees.MonDtCorps.Corps.Count>0 || Donnees.MonDtDestination.Destination.Count>0))
			{
                if (MessageBox.Show("Attention, si vous continuez le corps du rapport et la liste des destinataires vont être ré-initialisés ! \r\n\r\nVoulez vous continuer ?", "Nouvelle création", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }
			
			// On supprime l'ancien rapport patient
			fpRapport_Destinataires_Sheet1.RowCount = 0;
			Donnees.MonDtCorps.Clear();
			Donnees.MonDtDestination.Clear();
			if(Donnees.MonEtatRapport!=null)
			{
				Donnees.MonEtatRapport.Close();
				Donnees.MonEtatRapport.Dispose();
			}

            dstRapport.RapportRow tb = Donnees.MonDtRapport.Rapport[0];

			tb.Sans1 = "Médecin n° " + tb.CodeIntervenant + " : " + tb.NomMedecinSos;
            tb.Sans2 = "Secrétaire : " + VariablesApplicatives.Utilisateurs.NomUtilisateur;
			tb.Sans3 = "Consultation n° " + tb.NConsultation + ", Appel " + tb.CodeAppel + " du ";
			
			if(!tb.IsDSLNull())
				tb.Sans3 += tb.DSL.ToLongDateString() + " à " + tb.DSL.ToShortTimeString();				
			else if(!tb.IsDFINull())
				tb.Sans3+=tb.DFI;
				
			else if(!tb.IsDFINull())
				tb.Sans3+=tb.DAP;
				
			//tb.Sans4 = WorkedString.FirstLetterUpper(tb.PrenomPatient) + " " + tb.NomPatient.ToUpper();
            tb.Sans4 = tb.PrenomPatient + " " + tb.NomPatient.ToUpper();
			if(tb.Sexe=="F")
				tb.Sans5="Femme";
			else if(tb.Sexe=="E")
				tb.Sans5 ="Enfant";
			else
				tb.Sans5="Homme";
			tb.Sans5+="\t	" + tb.Rue + " " + tb.NumRue + ", " + tb.CodePostal + " " + tb.Commune;
			tb.Sans5+="\r\n"  + tb.Age + " " + WorkedString.GetAgeFormate(tb.UniteAge) + "\t	" + "Tel : " + tb.Tel;
			tb.Sans6 = "Symptômes : " + tb.Motif1 + "\r\nMédecin Traitant";


			// Nouveau Sans Rapport
			Donnees.MonSansRapport = new SansRapport();
			// on lui attribue le sans rapport
			Donnees.MonSansRapport.SetDataSource(Donnees.MonDtRapport);
            if (VariablesApplicatives.Utilisateurs.Droits != VariablesApplicatives.Utilisateurs.CodeDroits.Secretaire)
			{
				this.crystalReportViewer1.Size = new System.Drawing.Size(640, 625);
                //this.cmdRapport_Intro.Visible = false;
                //this.cmdRapport_EnTete.Visible = false;
                //this.cmdRapport_Corps.Visible = false;
                //this.cmdRapport_Bonjour.Visible = false;
                //this.cmdRapport_Concerne.Visible = false;
                //this.cmdRapport_Salutations.Visible = false;
                //this.cmdRapport_Signature.Visible = false;
                //this.cmdRapport_Destinataire.Visible = false;
                this.cmdRapport_Intro.Visible = true;
                this.cmdRapport_EnTete.Visible = true;
                this.cmdRapport_Corps.Visible = true;
                this.cmdRapport_Bonjour.Visible = true;
                this.cmdRapport_Concerne.Visible = true;
                this.cmdRapport_Salutations.Visible = true;
                this.cmdRapport_Signature.Visible = true;
                this.cmdRapport_Destinataire.Visible = true;


            }
			crystalReportViewer1.ReportSource = Donnees.MonSansRapport;	
			crystalReportViewer1.Zoom(1);	
			Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex].TypeRapport = 3;
			crystalReportViewer1.RefreshReport();
			InitialiseBoutonsRapport(3);				
		}

		private void CreationRubriqueConcerne()
		{
			if(Donnees.MonDtRapport==null || Donnees.MonEtatRapport==null || crystalReportViewer1.ActiveViewIndex==-1) return;

            dstRapport.RapportRow tb = Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex];
			
			string[][] strDonnees = OutilsExt.OutilsSql.ExecuteCommandeAvecTabString("SELECT pe.Nom,pe.Prenom,pe.Age,pe.DateNaissance,pe.UniteAge,pe.Sexe,pe.Adm_NumeroDansRue,pe.Adm_Rue,pe.Adm_CodePostal,pe.Adm_Commune from tablepersonne pe inner join tablepatient pa on pa.IdPersonne = pe.IdPersonne WHERE pa.IdPatient = " + tb.CodePatient);
			
			if(strDonnees!=null && strDonnees.Length==1)
			{

				tb.NomPatient = strDonnees[0][0];
				tb.PrenomPatient = strDonnees[0][1];
				tb.Age = int.Parse(strDonnees[0][2]);
				tb.Sexe = strDonnees[0][5];
				tb.UniteAge = strDonnees[0][4];
				if(strDonnees[0][3]!="")
				{
					tb.DateNaissance = strDonnees[0][3];
				}
				tb.NumRue = strDonnees[0][6];
				tb.Rue = strDonnees[0][7];
				tb.CodePostal = strDonnees[0][8];
				tb.Commune = strDonnees[0][9];

				// Travail sur les données formatées :
				tb.NomPatient = tb.NomPatient.ToUpper();
				//if(tb.PrenomPatient!="") 
				//	tb.PrenomPatient = tb.PrenomPatient.Substring(0,1).ToUpper() + tb.PrenomPatient.Substring(1).ToLower();

			}
			tb.RapConcerne = "";
			tb.RapConcerne+= tb.PrenomPatient + " " + tb.NomPatient + ", ";
			string age = "";
			if(tb.DateNaissance!=System.DBNull.Value.ToString())
			{
				age = "né";
				if(tb.Sexe=="F")
					age+="e";
				age+= " le " + DateTime.Parse(tb.DateNaissance).ToShortDateString();
			}
			else
			{
				age = tb.Age.ToString() + " " + WorkedString.GetAgeFormate(tb.UniteAge);
			}
			tb.RapConcerne+=age;
			string adresse = "\r\n";
			
			/*if(tb.Rue!="")
			{
				if(tb.Rue.Split(',').Length==2)
					adresse+= WorkedString.FirstLetterUpper(tb.Rue.Split(',')[1]) + " " + WorkedString.FirstLetterUpper(tb.Rue.Split(',')[0]);
				else
					adresse+= WorkedString.FirstLetterUpper(tb.Rue);
			}*/
            adresse += tb.Rue;

			if(tb.Rue!="" && tb.NumRue!="")	adresse+= " " + tb.NumRue;
			adresse+="\r\n";
			if(tb.CodePostal!="")
				adresse+=tb.CodePostal + " ";

			/*if(tb.Commune.Split(',').Length==2)
				adresse+=WorkedString.FirstLetterUpper(tb.Commune.Split(',')[1].TrimEnd()) + " " + WorkedString.FirstLetterUpper(tb.Commune.Split(',')[0]);
			else
				adresse+=WorkedString.FirstLetterUpper(tb.Commune);*/
            adresse+=tb.Commune;

			tb.RapConcerne+=adresse;
		}

        #endregion

        #region Traitement Communication des rapports

        #region Traitement immédiat

		// Export du rapport
		private void picRapport_Export1_Click(object sender, System.EventArgs e)
		{
			if(Donnees.MonEtatRapport==null)
			{
                MessageBox.Show("Aucun rapport n'est sélectionné.", "Rapport", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}			
			if(cbRapport_Format.Text=="")
			{
                MessageBox.Show("Format d'export non valide.", "Rapport", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

            if (MessageBox.Show("Voulez-vous exporter ce rapport ?", "Rapport", MessageBoxButtons.OK, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

			if(CrystalUtility.ExportReport(Donnees.MonEtatRapport,cbRapport_Format.Text,Application.StartupPath + "\\Export\\" + cbRapport_Format.Text + "\\" , Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex].NRapport.ToString()))
			{
                MessageBox.Show("Export réussi.", "Rapport", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (System.IO.File.Exists(Application.StartupPath + "\\Export\\" + cbRapport_Format.Text + "\\" + Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex].NRapport.ToString() + "." + cbRapport_Format.Text))
                {
                    System.Diagnostics.Process.Start(Application.StartupPath + "\\Export\\" + cbRapport_Format.Text + "\\" + Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex].NRapport.ToString() + "." + cbRapport_Format.Text);
                }
			}
			else
			{
                MessageBox.Show("Erreur lors de l'export.", "Rapport", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		// Impression du rapport
		private void picRapport_Print1_Click(object sender, System.EventArgs e)
		{

            CrystalDecisions.CrystalReports.Engine.ReportClass z_rcsEtatEnCours;
            if (Donnees.MonEtatRapport != null && Donnees.MonSansRapport == null)
            {
                z_rcsEtatEnCours = Donnees.MonEtatRapport;
            }
            else
            {
                z_rcsEtatEnCours = Donnees.MonSansRapport;
            }

            
            if (z_rcsEtatEnCours == null)
			{
                MessageBox.Show("Vous devez séléctionner un rapport !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if(txtRapport_NbCopies.Text=="")
			{
                MessageBox.Show("Le nombre de copies incorrect !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if(cbRapport_Imprimante.Text=="")
			{
                MessageBox.Show("L'imprimante est non valide !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

            if (MessageBox.Show("Voulez-vous imprimer ce rapport ?", "Rapport", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            bool Reussite = false;
            try
            {
                //CrystalUtility.SetVisibleImage(z_rcsEtatEnCours, "Picture4", !chkLogo.Checked);
                CrystalUtility.SetVisibleImage(z_rcsEtatEnCours, "Picture4", chkLogo.Checked);
                CrystalUtility.SetVisibleTexte(z_rcsEtatEnCours, "Text5", !chkLogo.Checked);


                if (Donnees.MonDtRapport.Rapport[0].TypeRapport == 1 || Donnees.MonDtRapport.Rapport[0].TypeRapport == 2)
                {
                    if (chkFax.Checked)
                    {
                       
                        frmPrintersParam frm = new frmPrintersParam(Donnees.MonDtRapport.Rapport[0].NRapport, z_rcsEtatEnCours);
                        frm.ShowDialog();
                        frm.Dispose();
                        frm = null;
                        return;
                    }
                    else Reussite = CrystalUtility.PrintReport(z_rcsEtatEnCours, int.Parse(txtRapport_NbCopies.Text), cbRapport_Imprimante.Text);
                    
                }
                else Reussite = CrystalUtility.PrintReport(z_rcsEtatEnCours, int.Parse(txtRapport_NbCopies.Text), cbRapport_Imprimante.Text);

                
                }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
          
			if(Reussite)
			{
                MessageBox.Show("Impression réussie.", "Rapport", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
                MessageBox.Show("Erreur lors de l'impression.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		// Envoi du rapport par mail
		private void picRapport_Mail1_Click(object sender, System.EventArgs e)
		{
			if(Donnees.MonEtatRapport==null)
			{
                MessageBox.Show("Vous devez séléctionner un rapport.", "Rapport", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}	
		
			if(fpRapport_Destinataires_Sheet1.ActiveRowIndex==-1)
			{
                MessageBox.Show("Vous devez séléctionner un destinataire.", "Rapport", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}					

			// Destinataire du mail :
			string m_strDestinataire="";
			if(Donnees.MonDtRapport!=null && Donnees.MonDtRapport.Rapport.Count>0)
                m_strDestinataire = Donnees.MonDtRapport.Rapport[0].Mail;
           
            string FileName = "Rapport patient " + Donnees.MonDtRapport.Rapport[0].NomPatient + " " + Donnees.MonDtRapport.Rapport[0].PrenomPatient + " " + Donnees.MonDtRapport.Rapport[0].DAP.ToShortDateString()+ "-" + DateTime.Now.ToString("mmss");
            
            //CrystalUtility.ExportReport(Donnees.MonEtatRapport, "doc", Application.StartupPath + SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.Cache, FileName);
            CrystalUtility.ExportReport(Donnees.MonEtatRapport, "pdf", Application.StartupPath + SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.Cache, FileName);

            SosMedecins.Utilitaires.Mail z_objMail = new SosMedecins.Utilitaires.Mail(VariablesApplicatives.Utilisateurs.EMail);
            z_objMail.Sujet = "Envoi d'un rapport patient";
            z_objMail.Message = "";
            //z_objMail.JoindrePiece(Application.StartupPath + SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.Cache + FileName + ".doc");
            z_objMail.JoindrePiece(Application.StartupPath + SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.Cache + FileName + ".pdf");
            z_objMail.AjouteDestinataire(m_strDestinataire);

            z_objMail.Show();

            z_objMail = null;
		}

        #endregion

        #region Sélection des destinataires

		// Sélection des destinataires
		private void lnkRapport_AjoutDestinataire_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			frmAjoutDestinataire frm = new frmAjoutDestinataire(this,Donnees.MonDtRapport.Rapport[0].TypeRapport);
			frm.ShowDialog();
			frm.Dispose();
			frm=null;
		}	

		public void InitialiseListeDestinataires(Destinataire[] TabDestinataires)
		{
			fpRapport_Destinataires_Sheet1.ColumnCount = 2;
			fpRapport_Destinataires_Sheet1.Columns[0].Width = fpRapport_Destinataires.Width * 70 / 100;
			fpRapport_Destinataires_Sheet1.Columns[1].Width = fpRapport_Destinataires.Width * 25 / 100;
			fpRapport_Destinataires_Sheet1.RowCount=0;
		}

        public void AjouteDestinataires(dstDestination.DestinationRow DestRow)
		{
			if(DestRow==null) return;
			int nb = fpRapport_Destinataires_Sheet1.RowCount++;
			fpRapport_Destinataires_Sheet1.Cells[nb,0].Tag = DestRow;
			fpRapport_Destinataires_Sheet1.Cells[nb,0].Text = DestRow.Nom;
			fpRapport_Destinataires_Sheet1.Cells[nb,1].Text = DestRow.RapModeEnvoi.ToString();
		}
		public void SupprimeDestinataires(Destinataire.TypeDestinataire typeDest)
		{
			for(int i=0;i<fpRapport_Destinataires_Sheet1.RowCount;i++)
			{
                dstDestination.DestinationRow destRow = (dstDestination.DestinationRow)fpRapport_Destinataires_Sheet1.Cells[i, 0].Tag;
				if(destRow.TypeDestinataire==typeDest.ToString())
				{
					fpRapport_Destinataires_Sheet1.RemoveRows(i,1);
					break;
				}
			}				
		}

		private void fpRapport_Destinataires_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			FarPoint.Win.Spread.Model.CellRange range = fpRapport_Destinataires.GetCellFromPixel(0,0,e.X,e.Y);
			if(range.Row>-1 && range.Column>-1)
			{
				if(e.Button == MouseButtons.Left)
				{
                    dstDestination.DestinationRow row = (dstDestination.DestinationRow)fpRapport_Destinataires_Sheet1.Cells[range.Row, 0].Tag;
					AffichageRapportAvecDestinataire(row);
				}
				else if(e.Button == MouseButtons.Right)
				{
					fpRapport_Destinataires_Sheet1.SetActiveCell(range.Row,range.Column);
					ContextMenu ctmenu = new ContextMenu();
					MenuItem menu0 = new MenuItem("Modifier ce destinataire");
					menu0.Click+=new EventHandler(CtMenu_Click);		
					ctmenu.MenuItems.Add(menu0);
					MenuItem menu2 = new MenuItem("Supprimer ce destinataire");
					menu2.Click+=new EventHandler(CtMenu_Click);		
					ctmenu.MenuItems.Add(menu2);
					MenuItem menu1 = new MenuItem("Supprimer Tous les destinataires");
					menu1.Click+=new EventHandler(CtMenu_Click);		
					ctmenu.MenuItems.Add(menu1);
					ctmenu.Show(fpRapport_Destinataires,new Point(e.X,e.Y));					
				}
			}
		}

        public void AffichageRapportAvecDestinataire(dstDestination.DestinationRow row)
		{
			if(Donnees.MonDtRapport==null || Donnees.MonDtRapport.Rapport.Count==0) return;

            try
            {
                if (!row.IsRapDestinataireNull())
                    Donnees.MonDtRapport.Rapport[0].RapDestinataire = row.RapDestinataire;
                if (!row.IsRapBonjourNull())
                    Donnees.MonDtRapport.Rapport[0].RapBonjour = row.RapBonjour;
                if (!row.IsRapIntroductionNull())
                    Donnees.MonDtRapport.Rapport[0].RapIntroduction = row.RapIntroduction;
                if (!row.IsRapSalutationNull())
                    Donnees.MonDtRapport.Rapport[0].RapSalutation = row.RapSalutation;

                bool visible = false;
                if (row.Logo == 1) visible = true;

                if (row.Copie == 1)
                {
                    Donnees.MonDtRapport.Rapport[0].RapCopie = "1";
                    CrystalUtility.UpdateObject(Donnees.MonEtatRapport, "Text2", "COPIE");
                }
                else
                {
                    Donnees.MonDtRapport.Rapport[0].RapCopie = "0";
                    CrystalUtility.UpdateObject(Donnees.MonEtatRapport, "Text2", "");
                }

                CrystalUtility.SetVisibleImage(Donnees.MonEtatRapport, "Picture4", !visible);
                CrystalUtility.SetVisibleTexte(Donnees.MonEtatRapport, "Text5", !visible);

                Donnees.MonEtatRapport.SetDataSource(Donnees.MonDtRapport);
                if (VariablesApplicatives.Utilisateurs.Droits != VariablesApplicatives.Utilisateurs.CodeDroits.Secretaire)
                {
                    this.crystalReportViewer1.Size = new System.Drawing.Size(640, 625);
                    //this.cmdRapport_Intro.Visible = false;
                    //this.cmdRapport_EnTete.Visible = false;
                    //this.cmdRapport_Corps.Visible = false;
                    //this.cmdRapport_Bonjour.Visible = false;
                    //this.cmdRapport_Concerne.Visible = false;
                    //this.cmdRapport_Salutations.Visible = false;
                    //this.cmdRapport_Signature.Visible = false;
                    //this.cmdRapport_Destinataire.Visible = false;

                    this.cmdRapport_Intro.Visible = true;
                    this.cmdRapport_EnTete.Visible = true;
                    this.cmdRapport_Corps.Visible = true;
                    this.cmdRapport_Bonjour.Visible = true;
                    this.cmdRapport_Concerne.Visible = true;
                    this.cmdRapport_Salutations.Visible = true;
                    this.cmdRapport_Signature.Visible = true;
                    this.cmdRapport_Destinataire.Visible = true;
                }

                crystalReportViewer1.ReportSource = Donnees.MonEtatRapport;
                crystalReportViewer1.RefreshReport();
                crystalReportViewer1.Zoom(1);
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} Exception caught.", ex);
                MessageBox.Show(ex.Message.ToString());
            }

		}
        public dstDestination.DestinationRow RetrouveDestinataireRapport(int Code)
		{
			for(int i=0;i<fpRapport_Destinataires_Sheet1.RowCount;i++)
			{
                dstDestination.DestinationRow r = (dstDestination.DestinationRow)fpRapport_Destinataires_Sheet1.Cells[i, 0].Tag;
				if(r.CodeDestinataire==Code)
					return r;
			}
			return null;
		}

        #endregion
		
        #endregion

        #region Saisie dans le rapport

		// on remet chaque bouton à son état initial
		private void InitialiseBoutonsRapport(int type)
		{
			txtSaisieRapport.Tag = null;
			txtSaisieRapport.Text = "";
		}

		// Clic sur un bouton de rubrique du rapport
		private void cmdRapport_ItemClick(object sender, System.EventArgs e)
		{
			Button btn = (Button)sender;
			SelectionBoutonRapport(btn);
		}		

		// Sélection d'une rubrique sur le rapport
		private void SelectionBoutonRapport(Button btn)
		{			
			if(Donnees.MonDtRapport==null || Donnees.MonEtatRapport==null || crystalReportViewer1.ActiveViewIndex==-1) return;
		
			InitialiseBoutonsRapport(Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex].TypeRapport);

			btn.BackColor = Color.MistyRose;	
			txtSaisieRapport.ForeColor = btn.ForeColor;
			if(btn.Tag!=null)
				txtSaisieRapport.Font = (Font)btn.Tag;
			else
                txtSaisieRapport.Font = SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.PoliceDefault();

			txtSaisieRapport.Tag = btn.Name;

			switch(btn.Name)
			{
				case "cmdRapport_EnTete":					
					txtSaisieRapport.Text = Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex].RapEnTete;
					break;
				case "cmdRapport_Destinataire":
					//txtSaisieRapport.Text = Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex].RapDestinataire;
					lnkRapport_AjoutDestinataire_LinkClicked(null,null);
					break;
				case "cmdRapport_Concerne":
					if (Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex].RapConcerne == "")
					{
						CreationRubriqueConcerne();
						Donnees.MonEtatRapport.SetDataSource(Donnees.MonDtRapport);
						crystalReportViewer1.RefreshReport();
					}
					else
                        txtSaisieRapport.Text = Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex].RapConcerne;
					break;
				case "cmdRapport_Bonjour":
					txtSaisieRapport.Text = Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex].RapBonjour;
					break;
				case "cmdRapport_Intro":
					txtSaisieRapport.Text = Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex].RapIntroduction;
					break;
				case "cmdRapport_Salutations":
					txtSaisieRapport.Text = Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex].RapSalutation;
					break;
				case "cmdRapport_Signature":
					txtSaisieRapport.Text = Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex].RapSignature;
					break;
				case "cmdRapport_Corps":
					// Ouvre la fenêtre de fabrication du corps du rapport
					frmRapport_Objet frm = new frmRapport_Objet();
					frm.Corps = Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex].RapCorps;
					frm.ShowDialog();
					Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex].RapCorps = frm.Corps;
					frm.Dispose();
					frm=null;
					MethodeSauvegarde(false);
					PicRapport_Actualiser_Click(null,null);
					break;
				default:
					break;
			}		
	
			txtSaisieRapport.Focus();
		}

		// Lorsque l'on quitte une zone de saisie, on provoque le raffraichissement
		private void txtSaisieRapport_Leave(object sender, System.EventArgs e)
		{
			if(txtSaisieRapport.Tag==null) return;
			if(Donnees.MonDtRapport==null || Donnees.MonEtatRapport==null || crystalReportViewer1.ActiveViewIndex==-1) return;
		
			switch(txtSaisieRapport.Tag.ToString())
			{
				case "cmdRapport_EnTete":	
					MiseEnFormeObject("RapEnTete",cmdRapport_EnTete.Tag,cmdRapport_EnTete.ForeColor);
					Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex].RapEnTete = txtSaisieRapport.Text;
					break;
				case "cmdRapport_Destinataire":
					MiseEnFormeObject("RapDestinataire",cmdRapport_Destinataire.Tag,cmdRapport_Destinataire.ForeColor);
					Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex].RapDestinataire = txtSaisieRapport.Text;
					break;
				case "cmdRapport_Concerne":
					MiseEnFormeObject("RapConcerne",cmdRapport_Concerne.Tag,cmdRapport_Concerne.ForeColor);
					Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex].RapConcerne = txtSaisieRapport.Text;
					break;
				case "cmdRapport_Bonjour":
					MiseEnFormeObject("RapBonjour",cmdRapport_Bonjour.Tag,cmdRapport_Bonjour.ForeColor);
					Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex].RapBonjour = txtSaisieRapport.Text;			
					break;
				case "cmdRapport_Intro":
					Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex].RapIntroduction = txtSaisieRapport.Text;					
					break;
				case "cmdRapport_Salutations":
					Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex].RapSalutation = txtSaisieRapport.Text;				
					break;
				case "cmdRapport_Signature":
					Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex].RapSignature = txtSaisieRapport.Text;				
					break;				
				default:
					break;
			}

			//PicRapport_Actualiser_Click(null,null);
		}

		// Saisie du texte selon l'index :
		private void TxtSaisieRapport_TextChanged(object sender, System.EventArgs e)
		{		
			if(txtSaisieRapport.Tag==null) return;
			if(Donnees.MonDtRapport==null || Donnees.MonEtatRapport==null || crystalReportViewer1.ActiveViewIndex==-1) return;
		
			switch(txtSaisieRapport.Tag.ToString())
			{
				case "cmdRapport_EnTete":					
					Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex].RapEnTete = txtSaisieRapport.Text;
					break;
				case "cmdRapport_Destinataire":
					Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex].RapDestinataire = txtSaisieRapport.Text;
					break;
				case "cmdRapport_Concerne":
					Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex].RapConcerne = txtSaisieRapport.Text;
					break;
				case "cmdRapport_Bonjour":
					Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex].RapBonjour = txtSaisieRapport.Text;			
					break;
				case "cmdRapport_Intro":
					Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex].RapIntroduction = txtSaisieRapport.Text;					
					break;
				case "cmdRapport_Salutations":
					Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex].RapSalutation = txtSaisieRapport.Text;				
					break;
				case "cmdRapport_Signature":
					Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex].RapSignature = txtSaisieRapport.Text;				
					break;				
				default:
					break;
			}
		}

		// Raffraichissement du rapport
		private void PicRapport_Actualiser_Click(object sender, System.EventArgs e)
		{
			if(txtSaisieRapport.Tag==null) return;
			if(Donnees.MonDtRapport==null || Donnees.MonEtatRapport==null || crystalReportViewer1.ActiveViewIndex==-1) return;

            try
            {
                MiseEnFormeObject("RapEntete", cmdRapport_EnTete.Tag, cmdRapport_EnTete.ForeColor);
                MiseEnFormeObject("RapDestinataire", cmdRapport_Destinataire.Tag, cmdRapport_Destinataire.ForeColor);
                MiseEnFormeObject("RapConcerne", cmdRapport_Concerne.Tag, cmdRapport_Concerne.ForeColor);
                MiseEnFormeObject("RapBonjour", cmdRapport_Bonjour.Tag, cmdRapport_Bonjour.ForeColor);
                MiseEnFormeObject("RapIntroduction", cmdRapport_Intro.Tag, cmdRapport_Intro.ForeColor);
                MiseEnFormeObject("RapSalutation", cmdRapport_Salutations.Tag, cmdRapport_Salutations.ForeColor);
                MiseEnFormeObject("RapSignature", cmdRapport_Signature.Tag, cmdRapport_Signature.ForeColor);

                Donnees.MonEtatRapport.SetDataSource(Donnees.MonDtRapport);
                crystalReportViewer1.RefreshReport();
            }
            catch(Exception ex)
            {
                Console.WriteLine("{0} Exception caught.", ex);
                MessageBox.Show(ex.Message.ToString());
            }

		}	

		private void MiseEnFormeObject(string FieldName,object fontobj,Color couleur)
		{
			Font font=null;
			if(fontobj==null)
                font = SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.PoliceDefault();
			else
				font = (Font)fontobj;
			CrystalUtility.MiseEnFormeObject(Donnees.MonEtatRapport,FieldName,font,couleur);
		}
		
        #endregion

        #region Divers
		
		private Button BtnSelected()
		{
			if(cmdRapport_EnTete.BackColor == Color.MistyRose) return cmdRapport_EnTete;
			if(cmdRapport_Destinataire.BackColor == Color.MistyRose) return cmdRapport_Destinataire;
			if(cmdRapport_Bonjour.BackColor == Color.MistyRose) return cmdRapport_Bonjour;
			if(cmdRapport_Concerne.BackColor == Color.MistyRose) return cmdRapport_Concerne;
			if(cmdRapport_Intro.BackColor == Color.MistyRose) return cmdRapport_Intro;
			if(cmdRapport_Salutations.BackColor == Color.MistyRose) return cmdRapport_Salutations;
			if(cmdRapport_Signature.BackColor == Color.MistyRose) return cmdRapport_Signature;
			return null;
		}

		// Changement de couleur dans la zone de saisie
		private void btnRapport_Couleur_Click(object sender, System.EventArgs e)
		{
			ColorDialog dialog = new ColorDialog();
			dialog.FullOpen = true;
			dialog.Color = txtSaisieRapport.ForeColor;
			dialog.ShowDialog();
			txtSaisieRapport.ForeColor = dialog.Color;		
			btnRapport_Couleur.BackColor = dialog.Color;
			lblTest.ForeColor = dialog.Color;
			
			Button btn = BtnSelected();
			
			if(btn!=null) btn.ForeColor = dialog.Color;
			
			dialog.Dispose();
			dialog=null;
		}

		// Changement de Police dans la zone de saisie
		private void btnRapport_Font_Click(object sender, System.EventArgs e)
		{
			FontDialog dialog = new FontDialog();
			dialog.FontMustExist = true;
			dialog.Font = new Font(txtSaisieRapport.Font.Name,txtSaisieRapport.Font.Size,txtSaisieRapport.Font.Style);
			dialog.ShowDialog();
			txtSaisieRapport.Font = dialog.Font;
			btnRapport_Font.Font = dialog.Font;
			lblTest.Font = dialog.Font;

			Button btn = BtnSelected();
			if(btn!=null)	btn.Tag = dialog.Font;
			
			dialog.Dispose();
			dialog=null;
		}

        #endregion

        #region Visa, Reprises, Enregistrement...

		// Validation et sauvegarde d'un rapport suite à modification.
		private void pic_ValideRapport_Click(object sender, System.EventArgs e)
		{
			MethodeSauvegarde(true);
		}

		public void MethodeSauvegarde(bool AvecResultat)
		{
			if(Donnees.MonDtRapport==null || (Donnees.MonEtatRapport==null && Donnees.MonSansRapport==null) || crystalReportViewer1.ActiveViewIndex==-1) return;

            try
            {
			    string xml = "";
			    Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex].Commentaire = TxtRapport_Commentaire.Text;
    					

			    SetDestinataireDuRapport();

                if (OutilsExt.OutilsSql.SauvegardeRapport(Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex], Donnees.MonDtCorps, Donnees.MonDtDestination, VariablesApplicatives.Utilisateurs.Identifiant, xml, txtRapport_CommentaireSauvegarde.Text))
			    {
    				
				    if(Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex].Vise==1)
				    {
					    DialogResult result =  MessageBox.Show("Le rapport garde-t-il son VISA?","Rapport",MessageBoxButtons.YesNo);
    						
					    if(result==DialogResult.No)
					    {													
						    OutilsExt.OutilsSql.SetRapportAViser(Donnees.MonDtRapport.Rapport[0].NRapport,true);
					    }						
				    }
				    else
				    {
					    OutilsExt.OutilsSql.SetRapportAViser(Donnees.MonDtRapport.Rapport[0].NRapport,true);
				    }					

                    ChargementHistoriqueFiche(Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex].NConsultation);
				    txtRapport_CommentaireSauvegarde.Text ="";
				    AffichageEtatRapport();

				    if(Donnees.MonDtRapport.Rapport[0].TypeRapport==1 || Donnees.MonDtRapport.Rapport[0].TypeRapport==2)
					    Donnees.MonEtatRapport.SetDataSource(Donnees.MonDtRapport);
				    else if (Donnees.MonDtRapport.Rapport[0].TypeRapport==3)
					    Donnees.MonSansRapport.SetDataSource(Donnees.MonDtRapport);

                    Donnees.SaveRapport = true;
				    LblSauvegardeRapport.Text = "Sauvegarde réussie";
			    }
			    else
			    {
				    LblSauvegardeRapport.Text = "Sauvegarde échouée!!!";
			    }
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} Exception caught.", ex);
                MessageBox.Show(ex.Message.ToString());
            }

		}

		private void SetDestinataireDuRapport()
		{
            dstDestination dest = new dstDestination();
			
			for(int i=0;i<fpRapport_Destinataires_Sheet1.RowCount;i++)
			{
                dstDestination.DestinationRow row = (dstDestination.DestinationRow)fpRapport_Destinataires_Sheet1.Cells[i, 0].Tag;
                dstDestination.DestinationRow rowbis = dest.Destination.NewDestinationRow();
				rowbis.ItemArray = row.ItemArray;
				dest.Destination.AddDestinationRow(rowbis);
			}

			Donnees.MonDtDestination.Clear();
			for(int i=0;i<dest.Destination.Count;i++)
			{
                dstDestination.DestinationRow row = Donnees.MonDtDestination.Destination.NewDestinationRow();
				row.ItemArray = dest.Destination[i].ItemArray;
				Donnees.MonDtDestination.Destination.AddDestinationRow(row);
			}

			InitialiseListeDestinataires(Donnees.MesDestinataires);
			// on reconstruit la liste des destinataires
			for(int i=0;i<Donnees.MonDtDestination.Destination.Count;i++)
			{
				AjouteDestinataires(Donnees.MonDtDestination.Destination[i]);
			}
		}


        private void BtnRapport_Copier_Click(object sender, System.EventArgs e)
        {
            if (Donnees.MonDtRapport == null || Donnees.MonDtRapport.Rapport.Count == 0 || crystalReportViewer1.ActiveViewIndex == -1) return;

            try
            {
                dstRapport.RapportRow rapportRow = Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex];
                string texte = string.Empty;

                if (!rapportRow.IsRapCorpsNull())
                {
                    using (RichTextBox temp = new RichTextBox())
                    {
                        temp.Rtf = rapportRow.RapCorps;
                        texte = temp.Text;
                    }
                }

                if (string.IsNullOrEmpty(texte) && !rapportRow.IsCommentaireNull())
                    texte = rapportRow.Commentaire;

                if (!string.IsNullOrEmpty(texte))
                    Clipboard.SetText(texte);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void OuvrirRapportSuivantDepuisListe()
        {
            if (!m_rapportOuvertDepuisListe || m_frmLstRapportToSend == null)
                return;

            frmListeRapportAViser.TypeListe typeListe = m_frmLstRapportToSend.MonTypeDeListe;
            bool listeActualisee = true;

            switch (typeListe)
            {
                case frmListeRapportAViser.TypeListe.AVISER:
                    this.menuItem2_Click_1(null, null);
                    break;
                case frmListeRapportAViser.TypeListe.ACORRIGER:
                    this.menuItem5_Click(null, null);
                    break;
                case frmListeRapportAViser.TypeListe.AREPRENDRE:
                    this.menuItem3_Click(null, null);
                    break;
                default:
                    listeActualisee = false;
                    break;
            }

            if (!listeActualisee || m_frmLstRapportToSend == null || m_frmLstRapportToSend.listView1 == null)
                return;

            if (m_frmLstRapportToSend.listView1.Items.Count > 0)
            {
                if (m_frmLstRapportToSend.listView1.SelectedIndices.Count == 0)
                {
                    m_frmLstRapportToSend.listView1.Items[0].Selected = true;
                }

                m_frmLstRapportToSend.listView1_DoubleClick(null, null);
            }
        }

        //Accord d'un visa par un mйdecin
        private void BtnRapport_Visa_Click(object sender, System.EventArgs e)
		{
			if(Donnees.MonDtRapport==null || Donnees.MonEtatRapport==null || crystalReportViewer1.ActiveViewIndex==-1) return;
			//Visa Accordé
            try
            {

                dstRapport.RapportRow rapportRow = Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex];
                if (RapportDejaRefuse(rapportRow))
                {
                    if (Donnees.MonDtDestination != null)
                    {
                        foreach (dstDestination.DestinationRow destRow in Donnees.MonDtDestination.Destination.Rows)
                        {
                            if (!destRow.IsRapSalutationNull() && destRow.RapSalutation == TexteRefusVisa)
                                destRow.SetRapSalutationNull();
                        }
                    }

                    if (!rapportRow.IsRapSalutationNull() && rapportRow.RapSalutation == TexteRefusVisa)
                        rapportRow.SetRapSalutationNull();

                    if (Donnees.MonDtRapport.Rapport.Count > 0)
                    {
                        dstRapport.RapportRow firstRow = Donnees.MonDtRapport.Rapport[0];
                        if (!firstRow.IsRapSalutationNull() && firstRow.RapSalutation == TexteRefusVisa)
                            firstRow.SetRapSalutationNull();
                    }
                }
                
                SetDestinataireDuRapport();

                OutilsExt.OutilsSql.SauvegardeRapport(Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex], Donnees.MonDtCorps, Donnees.MonDtDestination, VariablesApplicatives.Utilisateurs.Identifiant, "", txtRapport_CommentaireSauvegarde.Text);

                Fonction z_objFonctionDal = new Fonction();
                z_objFonctionDal.EnregistreModification(Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex].NConsultation.ToString(), VariablesApplicatives.Utilisateurs.Identifiant, DateTime.Now, Constantes.ACCORDE_VISA, TxtRapport_CommentaireVisa.Text);

			    OutilsExt.OutilsSql.VisaSurRapport(Donnees.MonDtRapport.Rapport[0],true);
			    ChargementHistoriqueFiche(Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex].NConsultation);
			    TxtRapport_CommentaireVisa.Text ="";

			    Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex].RapSignature = "Dr " + Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex].NomMedecinSos;
			    Donnees.MonEtatRapport.SetDataSource(Donnees.MonDtRapport);
			    crystalReportViewer1.RefreshReport();
                crystalReportViewer1.Zoom(85);

			    ChargementHistoriqueFiche(Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex].NConsultation);
			    Donnees.SaveRapport = true;

			    AffichageEtatRapport();

			    // Destinataire du mail :
			    string m_strDestinataire="";
			    if(Donnees.MonDtRapport!=null && Donnees.MonDtRapport.Rapport.Count>0)
				    m_strDestinataire = Donnees.MonDtRapport.Rapport[0].Mail;

                string FileName = "Rapport patient " + Donnees.MonDtRapport.Rapport[0].NomPatient + " " + Donnees.MonDtRapport.Rapport[0].PrenomPatient + " " + Donnees.MonDtRapport.Rapport[0].DAP.ToShortDateString() + "-" + DateTime.Now.ToString("mmss");
                //CrystalUtility.ExportReport(Donnees.MonEtatRapport, "doc", Application.StartupPath + SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.Cache, FileName);
                CrystalUtility.ExportReport(Donnees.MonEtatRapport, "pdf", Application.StartupPath + SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.Cache, FileName);


                SosMedecins.Utilitaires.Mail z_objMail = new SosMedecins.Utilitaires.Mail(VariablesApplicatives.Utilisateurs.EMail);
                z_objMail.AjouteDestinataire(m_strDestinataire);
                //z_objMail.JoindrePiece(Application.StartupPath + SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.Cache + FileName + ".doc");
                z_objMail.JoindrePiece(Application.StartupPath + SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.Cache + FileName + ".pdf");
                z_objMail.Message = "";
                z_objMail.Sujet = "Rapport patient " + Donnees.MonDtRapport.Rapport[0].NomPatient + " " + Donnees.MonDtRapport.Rapport[0].PrenomPatient + " " + Donnees.MonDtRapport.Rapport[0].DAP.ToShortDateString();
                
                //On envoi le mail au médecin
                z_objMail.Envoi ();              
    								
			    OuvrirRapportSuivantDepuisListe();

                //recup des infos de la machine
                //Nom de la machine
                var userHost = Dns.GetHostName();                
                //Ip de la machine
               // var userIPV6 = Dns.GetHostEntry(userHost).AddressList[0].ToString();    //ip V6
                var userIPV4 = Dns.GetHostEntry(userHost).AddressList[1].ToString();    //ip V4

            return (!rapportRow.IsRapSalutationNull() && string.Equals(rapportRow.RapSalutation, TexteRefusVisa, StringComparison.Ordinal));

                    MessageBox.Show("Ce rapport est vis, vous ne pouvez pas refuser le visa.");
                    if (MessageBox.Show("Ce rapport est dj marqu comme refus. Voulez-vous craser la dcision prcdente ?", "Visa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                rapportRow.RapSignature = string.Empty;

                OutilsExt.OutilsSql.VisaSurRapport(Donnees.MonDtRapport.Rapport[0], false);
                MessageBox.Show("Le visa a t refus.", "Rapport", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }//Refus du visa => retour en correction
        private bool RapportDejaRefuse(dstRapport.RapportRow rapportRow)
        {
            if (rapportRow == null) return false;
            return (!rapportRow.IsRapSalutationNull() && string.Equals(rapportRow.RapSalutation, TexteRefusVisa, StringComparison.Ordinal));
        }

        
        private void BtnRapport_RefusVisa_Click(object sender, System.EventArgs e)
        {
            if (Donnees.MonDtRapport==null || Donnees.MonEtatRapport==null || crystalReportViewer1.ActiveViewIndex==-1) return;

            try
            {
                dstRapport.RapportRow rapportRow = Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex];
                if (rapportRow.Vise == 1)
                {
                    MessageBox.Show("Ce rapport est vis, vous ne pouvez pas refuser le visa.");

                    return;
                }
                bool dejaRefuse = RapportDejaRefuse(rapportRow);
                if (dejaRefuse)
                {
                    if (MessageBox.Show("Ce rapport est dj marqu comme refus. Voulez-vous craser la dcision prcdente ?", "Visa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                        return;
                }

                if (MessageBox.Show("Confirmez-vous le refus du visa pour ce rapport ?", "Visa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;

                SetDestinataireDuRapport();

                string commentaireVisa = TexteRefusVisa;
                TxtRapport_CommentaireVisa.Text = commentaireVisa;

                if (Donnees.MonDtDestination != null)
                {
                    foreach (dstDestination.DestinationRow destRow in Donnees.MonDtDestination.Destination.Rows)
                    {
                        destRow.RapSalutation = commentaireVisa;
                    }
                }


               
                rapportRow.RapSignature = string.Empty;

                rapportRow.RapSalutation = commentaireVisa;

				OutilsExt.OutilsSql.SauvegardeRapport(rapportRow, Donnees.MonDtCorps, Donnees.MonDtDestination, VariablesApplicatives.Utilisateurs.Identifiant, "", txtRapport_CommentaireSauvegarde.Text);

                Fonction z_objFonctionDal = new Fonction();
                z_objFonctionDal.EnregistreModification(rapportRow.NConsultation.ToString(), VariablesApplicatives.Utilisateurs.Identifiant, DateTime.Now, Constantes.REFUSE_VISA, commentaireVisa);

                OutilsExt.OutilsSql.VisaSurRapport(Donnees.MonDtRapport.Rapport[0], false);

                OutilsExt.OutilsSql.BonPourReprise(Donnees.MonDtRapport.Rapport[0].NRapport, true);

                Donnees.MonDtRapport.Rapport[0].RapSignature = rapportRow.RapSignature;
                Donnees.MonDtRapport.Rapport[0].RapSalutation = rapportRow.RapSalutation;
                Donnees.MonDtRapport.Rapport[0].Medecin_viseur = VariablesApplicatives.Utilisateurs.Identifiant;
                Donnees.MonDtRapport.Rapport[0].Vise = 0;

                ChargementHistoriqueFiche(rapportRow.NConsultation);
                Donnees.SaveRapport = true;

                AffichageEtatRapport();

                if (Donnees.MonEtatRapport != null)
                {
                    Donnees.MonEtatRapport.SetDataSource(Donnees.MonDtRapport);
                    crystalReportViewer1.RefreshReport();
                }

                TxtRapport_Commentaire.Text = rapportRow.IsCommentaireNull() ? "" : rapportRow.Commentaire.ToString();

                string userHost = string.Empty;
                string userIPV4 = string.Empty;
                try
                {
                    userHost = Dns.GetHostName();
                    IPHostEntry entry = Dns.GetHostEntry(userHost);
                    foreach (System.Net.IPAddress address in entry.AddressList)
                    {
                        if (address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            userIPV4 = address.ToString();
                            break;
                        }
                    }
                    if (string.IsNullOrEmpty(userIPV4) && entry.AddressList.Length > 0)
                        userIPV4 = entry.AddressList[0].ToString();
                }
                catch
                {
                    if (string.IsNullOrEmpty(userHost))
                        userHost = Environment.MachineName;
                    if (string.IsNullOrEmpty(userIPV4))
                        userIPV4 = "N/A";
                }

                mouchard.evenement("Rapport n " + Donnees.MonDtRapport.Rapport[0].NRapport.ToString() + " refus sur le poste " + userHost + " ayant pour adresse ip: " + userIPV4, VariablesApplicatives.Utilisateurs.NomUtilisateur.ToString());

                MessageBox.Show("Le visa a t refus.", "Rapport", MessageBoxButtons.OK, MessageBoxIcon.Information);

                OuvrirRapportSuivantDepuisListe();

                BtnRapport_Visa.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

		// Liste des rapports à viser
		private void menuItem2_Click_1(object sender, System.EventArgs e)
		{
			if(m_frmLstRapportToSend!=null)
			{
				this.Controls.Remove(m_frmLstRapportToSend);
				m_frmLstRapportToSend.Dispose();
				m_frmLstRapportToSend=null;
			}
			frmListeRapportAViser frm = new frmListeRapportAViser(this);
			frm.MonTypeDeListe = frmListeRapportAViser.TypeListe.AVISER;
			frm.Label = "Liste des rapports à viser";
			frm.ListeRapports = OutilsExt.OutilsSql.ListeRapportAViser();
			this.Controls.Add(frm);
			frm.Left = pan_Statiques.Left;
			frm.Top = pan_Statiques.Top;	
			this.Controls.SetChildIndex(frm,0);
			frm.listView1.Focus();
			if (frm.listView1.Items.Count>0)
                frm.listView1.TopItem.Selected = true;
			m_frmLstRapportToSend = frm;
		}

		// Liste des rapports à corriger
		private void menuItem5_Click(object sender, System.EventArgs e)
		{
			if(m_frmLstRapportToSend!=null)
			{
				this.Controls.Remove(m_frmLstRapportToSend);
				m_frmLstRapportToSend.Dispose();
				m_frmLstRapportToSend=null;
			}
			frmListeRapportAViser frm = new frmListeRapportAViser(this);
			frm.MonTypeDeListe = frmListeRapportAViser.TypeListe.ACORRIGER;
			frm.Label = "Liste des rapports à corriger";
			frm.ListeRapports = OutilsExt.OutilsSql.ListeRapportACorriger();
			this.Controls.Add(frm);
			frm.Left = pan_Statiques.Left;
			frm.Top = pan_Statiques.Top;	
			this.Controls.SetChildIndex(frm,0);
			frm.listView1.Focus();
			if (frm.listView1.Items.Count>0)
				frm.listView1.TopItem.Selected = true;
			m_frmLstRapportToSend = frm;
		}		

		// Demande de correction sur le rapport au médecin chef : 
		private void btnCorriger_Click(object sender, System.EventArgs e)
		{
			if(Donnees.MonDtRapport==null || Donnees.MonEtatRapport==null) return;

			DialogResult result = MessageBox.Show("Etes-vous certain de vouloir envoyer ce rapport en correction?","Correction",MessageBoxButtons.YesNo,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button1);
			if(result==DialogResult.Yes)
			{
				OutilsExt.OutilsSql.DemandeCorrection(Donnees.MonDtRapport.Rapport[0].NRapport,true);
			}
		}
		// le médecin chef enleve la demande de correction
		private void btnEnlevCorrection_Click(object sender, System.EventArgs e)
		{
			if(Donnees.MonDtRapport==null || Donnees.MonEtatRapport==null) return;

            if (MessageBox.Show("Ce rapport repartira dans le cycle des rapports à viser s'il ne l'est pas encore. Continuer ?", "Correction", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
			{
				OutilsExt.OutilsSql.DemandeCorrection(Donnees.MonDtRapport.Rapport[0].NRapport,false);
			}
		}
		private void btnSupprimerRapport_Click(object sender, System.EventArgs e)
		{
			if(Donnees.MonDtRapport==null || Donnees.MonEtatRapport==null) return;

			if(Donnees.MonDtRapport.Rapport[0].Vise==1)
			{
				MessageBox.Show("Ce rapport est visé, vous ne pouvez pas le supprimer");
				return;
			}

			if(MessageBox.Show("Souhaitez-vous réellement supprimer ce rapport?","Suppression",MessageBoxButtons.YesNo,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button1) ==DialogResult.Yes)
			{
				OutilsExt.OutilsSql.SuppressionRapport(Donnees.MonDtRapport.Rapport[0].NConsultation, Donnees.MonDtRapport.Rapport[0].NRapport);
				tabTravail.SelectedIndex=0;
				btnOnglet2.Enabled = false;
				btnOnglet3.Enabled = false;
			}
		}
		// *************************************************
		public void ActiveFenetreRapport()
		{
			tabTravail.Enabled = true;
			btnOnglet1.Enabled = true;
			btnOnglet2.Enabled  = true;
			tabTravail.SelectedIndex = 1;
		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			if(m_frmLstRapportToSend!=null)
			{
				this.Controls.Remove(m_frmLstRapportToSend);
				m_frmLstRapportToSend.Dispose();
				m_frmLstRapportToSend=null;
			}
			frmListeRapportAViser frm = new frmListeRapportAViser(this);
			frm.MonTypeDeListe = frmListeRapportAViser.TypeListe.AREPRENDRE;
			frm.Label = "Liste des rapports à reprendre";
			frm.ListeRapports = OutilsExt.OutilsSql.ListeRapportReprise();
			this.Controls.Add(frm);
			frm.Left = pan_Statiques.Left;
			frm.Top = pan_Statiques.Top;	
			this.Controls.SetChildIndex(frm,0);
			frm.listView1.Focus();
			if (frm.listView1.Items.Count>0)
				frm.listView1.TopItem.Selected = true;
			m_frmLstRapportToSend = frm;
		}

		private void menuItem1_Click_1(object sender, System.EventArgs e)
		{
			if(m_frmLstRapportToSend!=null)
			{
				this.Controls.Remove(m_frmLstRapportToSend);
				m_frmLstRapportToSend.Dispose();
				m_frmLstRapportToSend=null;
			}

			m_frmLstRapportToSend = new frmListeRapportAViser(this);
			m_frmLstRapportToSend.MonTypeDeListe = frmListeRapportAViser.TypeListe.POURENVOI;
			m_frmLstRapportToSend.Label = "Liste des rapports à envoyer";
			m_frmLstRapportToSend.ListeRapports = new string[0][];
			this.Controls.Add(m_frmLstRapportToSend);
			m_frmLstRapportToSend.Left = pan_Statiques.Left;
			m_frmLstRapportToSend.Top = pan_Statiques.Top;	
			this.Controls.SetChildIndex(m_frmLstRapportToSend,0);
		}		

        #endregion

        #region A Travailler

				bool rapportVise = Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex].Vise == 1;
				bool rapportRefuse = !rapportVise && RapportDejaRefuse(Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex]);
				if (rapportVise)
					LblRapportVise.Text = "VISA ACCORD";
				else if (rapportRefuse)
					LblRapportVise.Text = "VISA REFUS";
		{
			if(Donnees.MonDtRapport==null || Donnees.MonEtatRapport==null || crystalReportViewer1.ActiveViewIndex==-1) return;

           TxtRapport_Commentaire.Text = Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex].Commentaire;
                       
            TxtRapport_CommentaireVisa.Text = "";

			ArrayList EtatRapport = OutilsExt.OutilsSql.EtatRapport(Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex].NRapport);
			if(EtatRapport!=null)
			{
				if(EtatRapport[0]!=null) LblRapportCree.Text = "Rapport créé par " + ((string[])EtatRapport[0])[0] + " le " + DateTime.Parse(((string[])EtatRapport[0])[1]).Day + "/" + DateTime.Parse(((string[])EtatRapport[0])[1]).Month;
				if(EtatRapport[1]!=null) LblRapportModifie.Text = "Rapport modifié par " + ((string[])EtatRapport[1])[0] + " le " + DateTime.Parse(((string[])EtatRapport[1])[1]).Day + "/" + DateTime.Parse(((string[])EtatRapport[1])[1]).Month;
				if(EtatRapport[2]!=null) 
					LblRapportVise.Text = "VISA ACCORDE";
				else
					LblRapportVise.Text = "";
			}			
		}

        private void AffichageEnvoisDuRapport()
        {
            lstEnvois.Items.Clear();
            if (Donnees.MonDtRapport == null || Donnees.MonEtatRapport == null || crystalReportViewer1.ActiveViewIndex == -1) return;

            if (Donnees.MonDtDestination == null) return;

            for (int i = 0; i < Donnees.MonDtDestination.Destination.Count; i++)
            {
                // Le rapport est-il envoyé et à quelle date?
                if (Donnees.MonDtDestination.Destination[i].RapEnvoye == 1)
                {
                    if (!Donnees.MonDtDestination.Destination[i].IsDateEnvoiNull())
                        lstEnvois.Items.Add(Donnees.MonDtDestination.Destination[i].Nom + "/" + Donnees.MonDtDestination.Destination[i].RapModeEnvoi + "/Envoyé le " + Donnees.MonDtDestination.Destination[i].DateEnvoi.ToString("dd/MM/yyyy"));
                    else
                        lstEnvois.Items.Add(Donnees.MonDtDestination.Destination[i].Nom + "/" + Donnees.MonDtDestination.Destination[i].RapModeEnvoi + "/Envoyé le ?");
                }
                else
                    lstEnvois.Items.Add(Donnees.MonDtDestination.Destination[i].Nom + "/" + Donnees.MonDtDestination.Destination[i].RapModeEnvoi);
            }
        }

        private void menuItem4_Click(object sender, System.EventArgs e)
        {
            if (m_frmLstRapportToSend != null)
            {
                this.Controls.Remove(m_frmLstRapportToSend);
                m_frmLstRapportToSend.Dispose();
                m_frmLstRapportToSend = null;
            }
            frmListeRapportAViser frm = new frmListeRapportAViser(this);
            frm.MonTypeDeListe = frmListeRapportAViser.TypeListe.SANSRAPPORT;
            frm.Label = "Liste des Sans Rapport";
            frm.ListeRapports = OutilsExt.OutilsSql.ListeSansRapport();
            this.Controls.Add(frm);
            frm.Left = pan_Statiques.Left;
            frm.Top = pan_Statiques.Top;
            m_frmLstRapportToSend = frm;
            this.Controls.SetChildIndex(frm, 0);
        }

        private void cbMedecin_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (cbMedecin.Text != "")
                ChkMedecin.Checked = true;
            else
                ChkMedecin.Checked = false;
        }

        private void dateTimePicker1_ValueChanged(object sender, System.EventArgs e)
        {
            chkDate.Checked = true;
        }

        private void dateTimePicker2_ValueChanged(object sender, System.EventArgs e)
        {
            chkDate.Checked = true;
        }


        private void cbMotif_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (cbMotif.Text != "")
                chkMotif.Checked = true;
            else
                chkMotif.Checked = false;
        }

        private void cbOrigine_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (cbOrigine.Text != "")
                chkOrigine.Checked = true;
            else
                chkOrigine.Checked = false;
        }

        private void txtRechercheIndex_TextChanged(object sender, System.EventArgs e)
        {
            if (txtRechercheIndex.Text != "")
                chkIndex.Checked = true;
            else
                chkIndex.Checked = false;
        }

        private void txtRechercheRapport_TextChanged(object sender, System.EventArgs e)
        {
            if (txtRechercheRapport.Text != "")
                chkRapport.Checked = true;
            else
                chkRapport.Checked = false;
        }

        private void TxtFiltre1_TextChanged(object sender, System.EventArgs e)
        {
            if (TxtFiltre1.Text != "")
                ChkFiltre1.Checked = true;
            else
                ChkFiltre1.Checked = false;
        }

        private void TxtFiltre2_TextChanged(object sender, System.EventArgs e)
        {
            if (TxtFiltre2.Text != "")
                ChkFiltre2.Checked = true;
            else
                ChkFiltre2.Checked = false;
        }

        private void TxtFiltre3_TextChanged(object sender, System.EventArgs e)
        {
            if (TxtFiltre3.Text != "")
                ChkFiltre3.Checked = true;
            else
                ChkFiltre3.Checked = false;
        }

        private void OngletRapport_Click(object sender, System.EventArgs e)
        {
            Button btn = (Button)sender;
            switch (btn.Name)
            {
                case "btnRapport_Onglet1":
                    TabActionRapport.SelectedIndex = 0;
                    break;
                case "btnRapport_Onglet2":
                    TabActionRapport.SelectedIndex = 1;
                    break;
                case "btnRapport_Onglet3":
                    TabActionRapport.SelectedIndex = 2;
                    break;
                default:
                    break;
            }
        }

        #endregion

		// *************************************************
		
        #endregion

        #region Autres...

		private void fpAppels_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			FarPoint.Win.Spread.Model.CellRange range = fpAppels.GetCellFromPixel(0,0,e.X,e.Y);

			if(range.Column ==4 && range.Row > 0)
			{
				toolTip1.SetToolTip(fpAppels, fpAppels_Sheet1.Cells[range.Row,4].Text);
			}
		}

		public void AjouteFenetreCachee(Form frmToHide)
		{
			Random rand = new Random();
			int nb = rand.Next();
			
			MenuItem mnu = new MenuItem(frmToHide.Text + " rand "  + nb);
			tblOfForm.Add(frmToHide.Text + " rand "  + nb,frmToHide);
			mnu.Click+=new EventHandler(mnuFenetre_Click);
			mnuFenetres.MenuItems.Add(mnu);
			mnuFenetres.Visible  = true;
			mnuFenetres.Enabled = true;
		}

		private void mnuFenetre_Click(object sender,System.EventArgs e)
		{
			MenuItem item = (MenuItem)sender;
			Form frm = (Form)tblOfForm[item.Text];
			frm.Show();
			tblOfForm.Remove(item.Text);
			mnuFenetres.MenuItems.Remove(item);
			mnuFenetres.Visible = (tblOfForm.Count>0);
		}

		private void mnuMedTT_Click(object sender, System.EventArgs e)
		{
			frmListeEffecteur frm = new frmListeEffecteur();
			frm.ShowDialog();
			frm.Dispose();
			frm=null;
		}

		private void mnuFactures_Click(object sender, System.EventArgs e)
		{
			mnuFacturation.Enabled = false;
			m_frmActualFactu= new frmFacturation(this);
			m_frmActualFactu.MdiParent = this;
			panel1.Controls.Add( m_frmActualFactu);
			panel1.Controls.SetChildIndex(m_frmActualFactu,0);			
			m_frmActualFactu.Left = 0;
			m_frmActualFactu.Top = 0;
			m_frmActualFactu.Show();			
		}	
	
        #endregion

        #region Rubrique Facture

		public void CloseFacture()
		{
			if(m_frmActualFactu!=null)
			{
				panel1.Controls.Remove(m_frmActualFactu);
				m_frmActualFactu.Dispose();
				m_frmActualFactu=null;
				mnuFacturation.Enabled  =true;
			}
		}

		public void OuvreFactureFromHistoFac(frmFacHisto frm, long NConsult)
		{
			AjouteFenetreCachee(frm);
			AfficheAppelsByConsult((int)NConsult);			
			DataRow m_datarowAppel = Donnees.MonDataSetAppels.Tables[0].Rows[0];			
			CloseFacture();

            // Ouvre la fenetre Facturation           
            m_frmActualFactu = new frmFacturation(this, m_datarowAppel);
            m_frmActualFactu.MdiParent = this;
            panel1.Controls.Add(m_frmActualFactu);
            panel1.Controls.SetChildIndex(m_frmActualFactu, 0);
            m_frmActualFactu.Left = 0;
            m_frmActualFactu.Top = 0;
            m_frmActualFactu.Show();	
	    }

        #endregion

        #region Procédure automatique d'envoi des rapports

		private void LancementProcedureAutomatique()
		{
			// Chargement de la liste des rapports à envoyer  :
			menuItem1_Click_1(null,null);
			
			Application.DoEvents();
			m_frmLstRapportToSend.m_EnvoiAuto=true;

			// Envoi des fax : 
			m_frmLstRapportToSend.EnvoiFaxAuto();
			Application.DoEvents();

			this.Controls.Remove(m_frmLstRapportToSend);
			m_frmLstRapportToSend.Dispose();
			Application.DoEvents();
			this.FermetureApplication();
		}

        #endregion

        private void menuItem6_Click(object sender, System.EventArgs e)
        {
            frmEncaissement docJ = new frmEncaissement();
            docJ.ShowDialog();
            docJ.Dispose();
            docJ = null;
        }

        private void button9_Click(object sender, System.EventArgs e)
        {
            if (Donnees.MonDtDestination.Destination[crystalReportViewer1.ActiveViewIndex].IsDateEnvoiNull())
            {
                //Transfer the data from frmGeneral to frmModifRapport pour faire les modification
                long Nrapport = Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex].NRapport;
                string Tete = Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex].RapEnTete;
                string Dest = Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex].RapDestinataire;
                string Conc = Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex].RapConcerne;
                string Bonjour = Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex].RapBonjour;
                string Intro = Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex].RapIntroduction;
                string Salut = Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex].RapSalutation;
                string Sing = Donnees.MonDtRapport.Rapport[crystalReportViewer1.ActiveViewIndex].RapSignature;
                //open the frmModifRapport with the information
                frmModifRapport docJ = new frmModifRapport(Tete, Dest, Conc, Bonjour, Intro, Salut, Sing, Nrapport);
                docJ.ShowDialog();
                docJ.Dispose();
                docJ = null;
            }
            else
            {
                MessageBox.Show("Changement de Rapport interdit, Le Rapport a déja envoyé");
            }
        }

        private void menuItem8_Click(object sender, System.EventArgs e)
        {
            Assutances m_fip = new Assutances();
            m_fip.ShowDialog();
        }

        private void menuItem9_Click(object sender, System.EventArgs e)
        {
            frmTa ta = new frmTa(this);
            ta.ShowDialog();
            ta.Dispose();
            ta = null;
        }

        private void menuItem11_Click(object sender, System.EventArgs e)
        {
            SalaireMed m_fac = new SalaireMed();
            m_fac.ShowDialog();
        }

        private void menuItem13_Click(object sender, System.EventArgs e)
        {
            //Affichage de la form facturation des TA
            TA.TA_Facturation ta_fac = new TA.TA_Facturation();
            ta_fac.ShowDialog();
            ta_fac.Dispose();
            ta_fac = null;
        }

       

        private void menuItem20_Click(object sender, System.EventArgs e)
        {
            //sql debiteur
            ImportSosGeneve.Donnees.MesFactures = new dstFacturesEncMed();

            string sql = "SELECT DISTINCT facture.NFacture, facture_etats.Etat, Sum(facture_etats.Montant) AS Montant, Max(facture_etats.DateOp) AS DateOp, facture_status.FacDateAnnulee, facture_status.FacDateEnvoyee, facture.TotalFacture, facture.Solde AS SoldeFacture, tablemedecin.Nom AS NomMED, tableactes.DAP, tablepersonne.Tel, ";
            sql = sql + "(tablepersonne.Prenom + ' ' + tablepersonne.Nom) AS NomPatient, tablemedecin.CodeIntervenant ";
            sql = sql + "FROM ((tableconsultations INNER JOIN factureconsultation ON tableconsultations.NConsultation = factureconsultation.NConsultation) INNER JOIN (facture INNER JOIN (facture_etats INNER JOIN facture_status ON facture_etats.NFacture = facture_status.NFacture) ON facture.NFacture = facture_etats.NFacture) ON factureconsultation.NFacture = facture_status.NFacture) INNER JOIN (tablemedecin INNER JOIN ((tablepersonne INNER JOIN tablepatient ON tablepersonne.IdPersonne = tablepatient.IdPersonne) INNER JOIN tableactes ON tablepatient.IdPatient = tableactes.IndicePatient) ON tablemedecin.CodeIntervenant = tableactes.CodeIntervenant) ON tableconsultations.CodeAppel = tableactes.Num ";
            sql = sql + "GROUP BY facture.NFacture, facture_etats.Etat, facture_status.FacDateAnnulee, facture_status.FacDateEnvoyee, facture.TotalFacture, facture.Solde, tablemedecin.Nom, tableactes.DAP, tablepersonne.Tel, tablemedecin.CodeIntervenant, (tablepersonne.Prenom + ' ' + tablepersonne.Nom) ";
            sql = sql + "HAVING (((facture.NFacture)>44) AND ((facture_etats.Etat)=2) AND ((facture_status.FacDateAnnulee) Is Null) AND ((facture.TotalFacture)>0) AND ((tablemedecin.CodeIntervenant)<>2536)) OR (((facture.NFacture)>44) AND ((facture_etats.Etat)=6) AND ((facture_status.FacDateAnnulee) Is Null) AND ((facture.TotalFacture)>0) AND ((tablemedecin.CodeIntervenant)<>2536)) ";
            sql = sql + "ORDER BY facture.NFacture";

            OutilsExt.OutilsSql.RemplitDataTable(ImportSosGeneve.Donnees.MesFactures.Tables[0], sql);
            int count = ImportSosGeneve.Donnees.MesFactures.Tables[0].Rows.Count;
        }

        private void btnRechercher_Click(object sender, EventArgs e)
        {
            // mise en attente de l'ecran
            Cursor.Current = Cursors.WaitCursor;
            btnRechercher.Enabled = false;

            //
            string[] filtres = new string[3];
            string strFiltre = "";

            if (ChkFiltre1.Checked)
            {
                filtres[0] = " pe.Tel  = '" + TxtFiltre1.Text.Replace("'", "''") + "' ";
            }
            if (ChkFiltre2.Checked)
            {
                if (txtRecherchePrenom.Text == "")
                    filtres[1] = " pe.Nom LIKE '%" + TxtFiltre2.Text.Replace("'", "''") + "%' ";
                else
                    filtres[1] = " pe.Nom  LIKE '%" + TxtFiltre2.Text.Replace("'", "''") + "%' AND pe.Prenom LIKE '%" + txtRecherchePrenom.Text.Replace("'", "''") + "%' ";
            }
            if (ChkFiltre3.Checked)
            {
                filtres[2] = " pe.DateNaissance = '" + OutilsExt.OutilsSql.DateFormatMySql(DateTime.Parse(TxtFiltre3.Text)) + "' ";
            }

            if (filtres[0] == null && filtres[1] == null && filtres[2] == null)
            {
                MessageBox.Show("Vous devez saisir au moins un critère.", "Affichage des appels", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (filtres[0] == null) filtres[0] = " 1=1 ";
            if (filtres[1] == null) filtres[1] = " 1=1 ";
            if (filtres[2] == null) filtres[2] = " 1=1 ";

            strFiltre += " WHERE " + filtres[0] + "and" + filtres[1] + "and" + filtres[2];

            Donnees.MonDataSetAppels = RecuperationConsultationByParam(strFiltre + " order by DAP DESC", false);

            AfficheResultat();

            Cursor.Current = Cursors.Default;
            btnRechercher.Enabled = true;
        }

        // Creation d'une consultation
        private void btnCreationConsultation_Click(object sender, EventArgs e)
        {
            /*DataRow row = (DataRow)pan_Dynamique.Tag;
            frmNouveauPatient frm = new frmNouveauPatient(row);
            frm.ShowDialog();
            frm.Dispose();
            frm = null;

            Donnees.MonDataSetAppels = RecuperationConsultationByAppel(long.Parse(row["Num"].ToString()));

            AfficheResultat();*/
        }
                
        
        private void btnVoirAppels_Click(object sender, EventArgs e)
        {
            string[] filtres = new string[4];
            string strFiltre = "";

            if (VariablesApplicatives.Utilisateurs.Droits == VariablesApplicatives.Utilisateurs.CodeDroits.Medecin)
            {
                ChkMedecin.Checked = true;
                cbMedecin.SelectedIndex = 0;
            }
            // Verification de la saisie des criteres
            if (ChkMedecin.Checked && cbMedecin.SelectedIndex > -1 && chkMotif.Checked && chkDate.Checked && chkOrigine.Checked && !chkIndex.Checked && !chkRapport.Checked)
            {
                MessageBox.Show("Vous devez saisir au moins un critère.", "Affichage des appels", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Mise en attente du controle
            //btnVoirAppels.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;

            if (chkRapport.Checked)
            {
                strFiltre = " inner join tablerapports r on r.NConsultation = c.NConsultation WHERE r.NRapport=" + txtRechercheRapport.Text + " ";
            }
            else
            {
                if (chkIndex.Checked)
                {
                    strFiltre = " WHERE c.NConsultation=" + txtRechercheIndex.Text + " ";
                }
                else
                {
                    if (ChkMedecin.Checked)
                    {
                        if (cbMedecin.SelectedIndex > -1)
                        {
                            ListItem item = (ListItem)cbMedecin.SelectedItem;
                            filtres[0] = " a.CodeIntervenant  = " + item.objValue.ToString() + " ";
                        }
                    }

                    if (chkMotif.Checked)
                    {
                        filtres[1] = " a.Motif1  = '" + cbMotif.Text.Replace("'", "''") + "' ";
                    }
                    if (chkDate.Checked)
                    {
                        filtres[2] = " a.DAP  >= '" + OutilsExt.OutilsSql.DateFormatMySql(dateTimePicker1.Value) + "' and a.DAP <= '" + OutilsExt.OutilsSql.DateFormatMySql(dateTimePicker2.Value) + "' ";
                    }

                    if (chkOrigine.Checked)
                    {
                        filtres[3] = " a.OrigineAppel  = '" + cbOrigine.Text.Replace("'", "''") + "' ";
                    }
                    if (filtres[0] == null) filtres[0] = " 1=1 ";
                    if (filtres[1] == null) filtres[1] = " 1=1 ";
                    if (filtres[2] == null) filtres[2] = " 1=1 ";
                    if (filtres[3] == null) filtres[3] = " 1=1 ";

                    strFiltre += " WHERE " + filtres[0] + "and" + filtres[1] + "and" + filtres[2] + "and" + filtres[3];
                }
            }
            // Recuperations des donnees
            Donnees.MonDataSetAppels = RecuperationConsultationByParam(strFiltre + " ORDER BY a.DAP DESC", false);

            if (Donnees.MonDataSetAppels != null)
            {
                if (VariablesApplicatives.blnVersionDev)
                {
                    _bseDonneesAppel.DataSource = Donnees.MonDataSetAppels;
                    _bseDonneesAppel.DataMember = Donnees.MonDataSetAppels.Tables[0].TableName.ToString();

                    _ctrlFichePatient.bseDonneesAppel = _bseDonneesAppel;

                    ctrlConsultationEtat.bseDonneesAppel = _bseDonneesAppel;

                    btnOnglet1.Enabled = true;
                    btnOnglet2.Enabled = true;
                    btnOnglet3.Enabled = true;
                }
                else
                {
                    InitialiseBoutonsFiltre(false);
                    MiseAJourListeAppels(Donnees.MonDataSetAppels);
                    InitialiseBoutonsFiltre(true);
                }
            }
            // reactive l'acces utilisateur
            Cursor.Current = Cursors.Default;
            //btnVoirAppels.Enabled = true;

            PrepareFicheStatiqueVierge();
            PrepareFicheDynamiqueVierge();
        }

        private void AfficheResultat()
        {
            if (Donnees.MonDataSetAppels != null)
            {
                InitialiseBoutonsFiltre(false);
                MiseAJourListeAppels(Donnees.MonDataSetAppels);
                InitialiseBoutonsFiltre(true);
                PrepareFicheStatiqueVierge();
                PrepareFicheDynamiqueVierge();
            }
        }

        #region Connexion à l'application --------------------------------------------------------------------------------------------------------------------------

        private void GestionDroits()
        {
            lblConnecte.Text = "Connecté(e) : " + VariablesApplicatives.Utilisateurs.NomUtilisateur;

            mnuAide.Enabled = true;
            // Selon les droits : 
            switch (VariablesApplicatives.Utilisateurs.Droits)
            {
                case VariablesApplicatives.Utilisateurs.CodeDroits.Secretaire:      //1    Regul et secrétaires
                    mnuDonnees.Enabled = true;
                    mnuTA.Enabled = true;
                    mnuRapports.Enabled = true;
                    mnuStatistiques.Enabled = true;
                    btnRapport_Onglet2.Visible = false;
                    checkBoxESP.Visible = false;
                    mnuFiches.Enabled = true;
                    menuAttestationTA.Enabled = true;
                    menuItem13.Enabled = false;  //Opérations sur les factures
                    menuItem14.Enabled = false;  //gestion Matériel TA   
                    menuFacturesImpayees.Enabled = false;  //poursuites, rappels
                    break;               
                case VariablesApplicatives.Utilisateurs.CodeDroits.Medecin:     //2
                    mnuDonnees.Enabled = false;
                    mnuTA.Enabled = false;
                    mnuRapports.Enabled = false;
                    btnRapport_Onglet2.Visible = false;
                    //btnAfficheNouveauxAppels.Visible = false;
                    btnEchangeMedicall.Visible = false;
                    btnRapport_Onglet1.Visible = false;
                    btnRapport_Onglet2.Visible = false;
                    checkBoxESP.Visible = false;
                    mnuFiches.Enabled = true;
                    TabActionRapport.SelectedIndex = 2;
                    menuAttestationTA.Enabled = false;
                    menuItem13.Enabled = false;  //Opérations sur les factures
                    menuItem14.Enabled = false;  //gestion Matériel TA   
                    menuFacturesImpayees.Enabled = false;  //poursuites, rappels
                    break;                
                case VariablesApplicatives.Utilisateurs.CodeDroits.Chef:        //4 pour l'admin de tout ou 5 pour comptable
                case VariablesApplicatives.Utilisateurs.CodeDroits.Comptable:   //5
                    mnuDonnees.Enabled = true;
                    mnuTA.Enabled = true;
                    mnuRapports.Enabled = true;
                    mnuStatistiques.Enabled = true;
                    mnuFiches.Enabled = true;
                    btnRapport_Onglet2.Visible = true;
                    checkBoxESP.Visible = true;
                    mnuFacturation.Enabled = true;
                    menuAttestationTA.Enabled = true;
                    menuItem13.Enabled = true;  //Opérations sur les factures
                    menuItem14.Enabled = true;  //gestion Matériel TA
                    menuItem19.Enabled = true;  //Salaires Médecins
                    menuFacturesImpayees.Enabled = true;  //poursuites, rappels
                    break;
                case VariablesApplicatives.Utilisateurs.CodeDroits.Admin:       //10 Mais n'apporte rien
                    mnuDonnees.Enabled = true;
                    mnuTA.Enabled = true;
                    mnuRapports.Enabled = true;
                    mnuStatistiques.Enabled = true;
                    mnuFiches.Enabled = true;
                    btnRapport_Onglet2.Visible = true;
                    checkBoxESP.Visible = true;                    
                    mnuFacturation.Enabled = true;
                    menuAttestationTA.Enabled = true;
                    menuItem19.Enabled = true;  //Salaires Médecins
                    menuItem13.Enabled = true;  //Opérations sur les factures
                    menuItem14.Enabled = true;  //gestion Matériel TA   
                    menuFacturesImpayees.Enabled = true;  //poursuites, rappels
                    break;

                default:
                    break;
            }

            // On active les bons menus :
            ChargementListeMedecins();
            ChargementListeMotifs();
            ChargementListeOrigine();

            ChargementDonneesStatiques();

            InitialiseFiltreDate();

            this.btnParametrages.Visible = true;
            tab.SelectedIndex = 2;
        }
        #endregion

        #region Fermeture de l'application --------------------------------------------------------------------------------------------------------------------------
        private void btnFermeture_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Voulez vous quitter l'application ?", "Fermeture", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                FermetureApplication();
            }
        }

        public void FermetureApplication()
        {

            //On efface le contenu du repertoire Cache
            /*foreach (string f in Directory.GetFiles(Application.StartupPath + "\\Cache", "*.*", SearchOption.TopDirectoryOnly))
            {
                File.Delete(f);
            }*/
            
            try
            {
                Parametrage.SauvegardeParametrage(SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli, Application.StartupPath + "\\Config.xml");

                if (OutilsExt.OutilsSql != null)
                {
                    OutilsExt.OutilsSql.FermetureBase();
                    OutilsExt.OutilsSql.Dispose();
                    OutilsExt.OutilsSql = null;
                }

                this.Dispose();
                Application.Exit();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                Environment.Exit(0);
            }
        }
        #endregion

        #region Connexion à l'application --------------------------------------------------------------------------------------------------------------------------

        // Gestion de la connexion et des parametres imprimanetes
        private void OuvertureParametrages()
        {
            frmParametrages frm = new frmParametrages();
            frm.ShowDialog();
            frm.Dispose();
            frm = null;
        }
        private void btnParametrages_Click(object sender, EventArgs e)
        {
            OuvertureParametrages();
            VerificationBaseTest();
        }
        private void mnuParametres_Click(object sender, System.EventArgs e)
        {
            OuvertureParametrages();
        }

        private void VerificationBaseTest()
        {
            if (Variables.InfoConnexion.NomServeur != @"192.168.0.8")
            {
               // panel2.BackColor = Color.Tan;
                LblBaseTest.Visible = true;
            }
            else
            {
               // panel2.BackColor = System.Drawing.SystemColors.Control;
              //  panel2.BackColor = Color.Tan;
                LblBaseTest.Visible = false;
            }
        }
        
        #endregion

        #region Menu ---------------------------------------------------------------------------------------------------------------------------------------

            #region Fiche
                private void mnuFip_Click(object sender, System.EventArgs e)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    FIP z_frmFip = new FIP(this);
                    z_frmFip.ShowDialog();
                    //z_frmFip.Dispose();
                    //z_frmFip = null;
                    Cursor.Current = Cursors.Default;
                }
            
                private void mnuUtilisateurs_Click(object sender, EventArgs e)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    SosMedecins.SmartRapport.GestionApplication.frmUtilisateurs z_frmUtilisateur = new SosMedecins.SmartRapport.GestionApplication.frmUtilisateurs();
                    z_frmUtilisateur.ShowDialog();
                    z_frmUtilisateur.Dispose();
                    z_frmUtilisateur = null;
                    Cursor.Current = Cursors.Default;
                }
            #endregion

            #region Données
               
                private void btnEchangeMedicall_Click(object sender, EventArgs e)
                {
                    
                    //Recuperation des donnees
                    menuImportEpos_Click(null, null);
                    
                    // mise a jour des listes
                    ChargementListeMedecins();
                    ChargementListeMotifs();
                    ChargementListeOrigine();
                    InitialiseFiltreDate();

                    Cursor.Current = Cursors.Default;
                                                          
                }
            #endregion

            #region Facturation
                private void mnuFacturation_Etats_Relance_Click(object sender, EventArgs e)
                {
                    this.Cursor = Cursors.WaitCursor;

                    frmRelances z_frm = new frmRelances();
                    z_frm.ShowDialog();
                    z_frm.Dispose();
                    z_frm = null;

                    this.Cursor = Cursors.Default;
                }

                private void mnuFacturation_Etats_VerificationSolde_Click(object sender, EventArgs e)
                {
                    this.Cursor = Cursors.WaitCursor;
                    SosMedecins.SmartRapport.EtatsCrystal.Verification_des_soldes_en_fonction_des_encaissements z_rpt = new SosMedecins.SmartRapport.EtatsCrystal.Verification_des_soldes_en_fonction_des_encaissements();
                    SosMedecins.SmartRapport.EtatsCrystal.Fonctions.ChangeConnection(z_rpt, Variables.ConnexionBase);
                    SosMedecins.SmartRapport.EtatsCrystal.frmCrystalReportViewer z_frm = new SosMedecins.SmartRapport.EtatsCrystal.frmCrystalReportViewer();
                    z_frm.ReportSource = z_rpt;
                    this.Cursor = Cursors.Default;
                    // Affichage
                    z_frm.ShowDialog();
                    // liberation des elements
                    z_rpt.Dispose();
                    z_frm.Dispose();
                    z_frm = null;
                }

                private void mnuFacturation_Etats_Arrangement_Click(object sender, EventArgs e)
                {
                    this.Cursor = Cursors.WaitCursor;
                    SosMedecins.SmartRapport.EtatsCrystal.Liste_des_arrangements z_rpt = new SosMedecins.SmartRapport.EtatsCrystal.Liste_des_arrangements();
                    SosMedecins.SmartRapport.EtatsCrystal.Fonctions.ChangeConnection(z_rpt, Variables.ConnexionBase);
                    SosMedecins.SmartRapport.EtatsCrystal.frmCrystalReportViewer z_frm = new SosMedecins.SmartRapport.EtatsCrystal.frmCrystalReportViewer();
                    z_frm.ReportSource = z_rpt;
                    this.Cursor = Cursors.Default;
                    // Affichage
                    z_frm.ShowDialog();
                    // liberation des elements
                    z_rpt.Dispose();
                    z_frm.Dispose();
                    z_frm = null;
                }
 
                private void mnuFacturation_Etats_Relance_Assurances_Click(object sender, EventArgs e)
                {
                    this.Cursor = Cursors.WaitCursor;
                    SosMedecins.SmartRapport.EtatsCrystal.Relances_Assurances z_rpt = new SosMedecins.SmartRapport.EtatsCrystal.Relances_Assurances();
                    SosMedecins.SmartRapport.EtatsCrystal.Fonctions.ChangeConnection(z_rpt, Variables.ConnexionBase);
                    SosMedecins.SmartRapport.EtatsCrystal.frmCrystalReportViewer z_frm = new SosMedecins.SmartRapport.EtatsCrystal.frmCrystalReportViewer();
                    z_frm.ReportSource = z_rpt;
                    this.Cursor = Cursors.Default;
                    // Affichage
                    z_frm.ShowDialog();
                    // liberation des elements
                    z_rpt.Dispose();
                    z_frm.Dispose();
                    z_frm = null;
                }
        #endregion

        #endregion

        private void btnAfficheNouveauxAppels_Click(object sender, EventArgs e)
        {
           
        }

        private void mnEssai_Click(object sender, EventArgs e)
        {
           // SosMedecins.SmartRapport.Facturation.frmDetails z_frmDetails = new SosMedecins.SmartRapport.Facturation.frmDetails(SosMedecins.SmartRapport.Facturation.frmDetails.Mode.Patient, 3459560);
           // z_frmDetails.ShowDialog();
        }

        private void menuImportEpos_Click(object sender, EventArgs e)
        {
            RecupDataEpos1.FImportEpos FimportEpos1 = new RecupDataEpos1.FImportEpos();
            FimportEpos1.ShowDialog();
            FimportEpos1.Dispose();
            FimportEpos1 = null;
        }

        private void RappelsTA_Click(object sender, EventArgs e)
        {
          
        }


        private void TA_Abonnement_Click_1(object sender, EventArgs e)
        {
            TA.InterfGestionTA inertfg = new InterfGestionTA();
            inertfg.ShowDialog();
            inertfg.Dispose();
            inertfg = null;
        }

        private void frmGeneral_FormClosing(object sender, FormClosingEventArgs e)
        {           
            // Utilisation dans votre boucle pour débloquer les fichiers avant suppression
            foreach (string f in Directory.GetFiles(Application.StartupPath + @"\Cache", "*.*", SearchOption.TopDirectoryOnly))
            {
                try
                {
                    DeverouilleFichier.UnlockFile(f);
                    File.Delete(f);
                }
                catch (Exception ex)
                {
                    // Gérez l'exception de manière appropriée
                    Console.WriteLine("Erreur lors de la suppression du fichier : " + ex.Message);
                }
            }

            try
            {
                Parametrage.SauvegardeParametrage(SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli, Application.StartupPath + "\\Config.xml");

                if (OutilsExt.OutilsSql != null)
                {
                    OutilsExt.OutilsSql.FermetureBase();
                    OutilsExt.OutilsSql.Dispose();
                    OutilsExt.OutilsSql = null;
                }

                this.Dispose();
                Application.Exit();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                Environment.Exit(0);
            }

        }

        private void menuItem21_Click(object sender, EventArgs e)
        {
            frmStatistiques frm = new frmStatistiques();
            frm.ShowDialog();
            frm.Dispose();
            frm = null;
        }

        private void menuItem22_Click(object sender, EventArgs e)
        {
            ImportSosGeneve.Commun.viewerStats144 viewer144 = new ImportSosGeneve.Commun.viewerStats144();
            viewer144.ShowDialog();
            viewer144.Dispose();
            viewer144 = null;
        }


        //************************LECTEUR DE DICTEE INTEGRE***************************Domi 04.01.2013
        public delegate void UpdateControlsDelegate();
       
        //Pour récupérer le chemin des dlls de VLC
        private void vlcControl1_VlcLibDirectoryNeeded(object sender, VlcLibDirectoryNeededEventArgs e)
        {
            string CheminDll;

            if (Environment.Is64BitOperatingSystem)
                CheminDll = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), @"VideoLAN\VLC");
            else                
                CheminDll = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), @"VideoLAN\VLC");
            e.VlcLibDirectory = new DirectoryInfo(CheminDll);

            //On en profite pour passer quelques options durant l'initialisation
            vlcControl1.VlcMediaplayerOptions = new[]
           {
                 "-vv",
                 "--file-caching=3500",
                 "--network-caching=3500"
            };
        }

        private void initialise_Lecteur(int Num_visite)
        {
            //et des boutons
            Bplay.ImageIndex = 0;
            Bpause.ImageIndex = 2;
            Bstop.ImageIndex = 4;


            //recupération du fichier audio de la consultation                                 
            string PathFichierSon = SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.Dest_Path_Dictee;

            String FichierEncours = PathFichierSon + Num_visite + ".3gp";           

            if (File.Exists(FichierEncours))
            {
               // PathMedia mediaEncours = new PathMedia(FichierEncours);
                media = FichierEncours;

                Lpasdictee.Visible = false;
                Bplay.Enabled = true;
                Bpause.Enabled = true;
                Bstop.Enabled = true;

                //initialisation de la jauge de lecture et des compteurs                   
                tBarTps.Minimum = 0;
                tBarTps.Value = 0;
                //tBarTps.Maximum = dureeTotal;
                tBarTps.Maximum = 0;
                LAvancement.Text = "0";
                tBarVol.Value = 50;     //Pour le son                                              
            }
            else
            {
                Lpasdictee.Visible = true;
                Bplay.Enabled = false;
                Bpause.Enabled = false;
                Bstop.Enabled = false;

                //initialisation de la jauge de lecture et des compteurs          
                tBarTps.Minimum = 0;
                tBarTps.Value = 0;
                LAvancement.Text = "0";
                tBarVol.Value = 50;     //Pour le son
            }           
        }
       

        public void InvokeUpdateControls()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new UpdateControlsDelegate(currentTrackTime));
            }
            else
            {
                currentTrackTime();
            }
        }


        public void InvokeFinControls()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new UpdateControlsDelegate(VlcStop));
            }
            else
            {
                VlcStop();
            }
        }

        private void currentTrackTime()
        {
            long PositionMilli = vlcControl1.Time;
           
            LAvancement.Text = (vlcControl1.Time / 1000).ToString();
            tBarTps.Value = (Int32)(vlcControl1.Time / 1000);       
           
            //La durée totale
            int Max = (int)vlcControl1.Length / 1000;
           
            if (Max == 0)
            {
                tBarTps.Maximum = 300;   //Il faut mettre quelque chose par défaut > 0
                LDuree.Text = "Inconnue";
            }
            else
            {                
                tBarTps.Maximum = Max;
                LDuree.Text = (vlcControl1.Length / 1000).ToString();
            }

            tBarTps.TickFrequency = Convert.ToInt32(tBarTps.Maximum / 15);  //on met 15 unités sur la ligne de temps
           
            if ((vlcControl1.GetCurrentMedia().Duration.TotalMilliseconds - PositionMilli) < 1000)  //Si on a atteind la fin
            {
                if (vlcControl1.IsPlaying)
                {
                    //On Pause le controle, puis on le stop le VlcControle dans une tache de fond. 
                    //En effet la méthode: VlcControl.Stop doit être appelée dans un autre thread (sinon plantage)...                  
                    vlcControl1.Pause();     
                    ThreadPool.QueueUserWorkItem(_ => vlcControl1.Stop());
                    
                    //Réinitialise les controles
                    LAvancement.Text = "0";
                    tBarTps.Value = 0;                  

                    Bstop.ImageIndex = 4;
                    Bplay.ImageIndex = 0;
                    Bpause.ImageIndex = 2;
                }
            }          
        }

        private void VlcStop()
        {
            if (vlcControl1.IsPlaying)
            {
                //Réinitialise les controles
                LAvancement.Text = "0";
                tBarTps.Value = 0;

                Bstop.ImageIndex = 4;
                Bplay.ImageIndex = 0;
                Bpause.ImageIndex = 2;

                //on pause avant de stoper
                vlcControl1.Pause();
                //var thread = new System.Threading.Thread(delegate() { vlcControl1.Stop(); });
                //thread.Start();
                ThreadPool.QueueUserWorkItem(_ => vlcControl1.Stop());
            }           

        }

        private void tBarVol_Scroll(object sender, EventArgs e)
        {
            //On varie le volume           
            vlcControl1.Audio.Volume = tBarVol.Value;
        }

        //Quand on bouge la barre de défilement
        private void tBarTps_Scroll(object sender, EventArgs e)
        {
            //Quand on bouge le curseur on met en pause et on met à jour la durée
            Bpause_Click(sender, e);
           
            //On se positionne au bont endroit sur le lecteur           
            vlcControl1.Time = tBarTps.Value * 1000;
            LAvancement.Text = (vlcControl1.Time / 1000).ToString();           
        }

        private void Bplay_Click(object sender, EventArgs e)
        {
            if ((vlcControl1.State.ToString() == "NothingSpecial") || (vlcControl1.State.ToString() == "Stopped"))          //Si il est stopé
            {
                if (media != null)   //securité si on arrive au lecteur par on ne sais ou et qu'il n'est pas initialisé...
                {
                    vlcControl1.SetMedia(new FileInfo(media));  //Affecte le nouveau media
                 
                    vlcControl1.Play();

                    Bstop.ImageIndex = 4;
                    Bplay.ImageIndex = 1;
                    Bpause.ImageIndex = 2;
                }
                else
                {
                    Lpasdictee.Visible = true;
                    Bplay.Enabled = false;
                    Bpause.Enabled = false;
                    Bstop.Enabled = false;
                }                                                                                                                                 
            }
            else if (vlcControl1.State.ToString() == "Paused")       //s'il est en pause
            {
                //on regarde la position du curseur et on l'affecte au media (si on l'a bougé manuellement...)                                                                 
                vlcControl1.Time = tBarTps.Value * 1000;
                
                vlcControl1.Play();

                Bstop.ImageIndex = 4;
                Bplay.ImageIndex = 1;
                Bpause.ImageIndex = 2;
            }            
        }

        private void Bpause_Click(object sender, EventArgs e)
        {
            //On met en pause
            if ((vlcControl1.State.ToString() == "Playing"))
            {
                vlcControl1.Pause();

                //look du bouton
                Bstop.ImageIndex = 4;
                Bplay.ImageIndex = 0;
                Bpause.ImageIndex = 3;
            }
        }

        private void Bstop_Click(object sender, EventArgs e)
        {
            VlcStop();         
        }
      
        //Quand il arrive à la fin
        private void vlcControl1_EndReached(object sender, VlcMediaPlayerEndReachedEventArgs e)
        {
            VlcStop();                    
        }


        private void vlcControl1_PositionChanged(object sender, VlcMediaPlayerPositionChangedEventArgs e)
        {
            InvokeUpdateControls();                                  
        }
      

        //**********************************FIN LECTEUR AUDIO VLC************************************

        private void bCarteAvs_Click(object sender, EventArgs e)
        {
            //Domi 29.07.2014
            //On affiche la carte AVS dans une fenêtre s'il y a une photo            
            //Recherche d'une image carte AVS (fichier image sur le serveur EPOS) à partir de la consultation                                        
            string PathCarteAVS = SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.Dest_Path_carteAVS;

            //Si le Tag du bouton est nul, aucune fiche courante:
			if(btnRapportCourant.Tag!=null)
            {
                //Récup du n° de consult (pour le chemin)
                //On extrait les deux valeurs : NConsultation et NRapport
			    long[] Index = (long[])btnRapportCourant.Tag;
                              
                //on defini le chemin avec le nom complet du fichier 
                String ImageCarteAVS = PathCarteAVS + Index[0].ToString() + ".jpg";

                if (File.Exists(ImageCarteAVS))     //Si le fichier existe
                {
                    FileInfo fInfo = new FileInfo(ImageCarteAVS);
                    if (fInfo.Length > 0)       //...et si sa taille est > 0 octet
                    {
                        FCarteAVS FormCarteAVS = new FCarteAVS(ImageCarteAVS);
                        FormCarteAVS.ShowDialog();
                        FormCarteAVS.Dispose();                      
                    }
                }                             
            }
        }

        private void menuAttestationTA_Click(object sender, EventArgs e)
        {
            Fattestation FAttestation = new Fattestation();
            FAttestation.ShowDialog();
            FAttestation.Dispose();
            FAttestation = null;
        }

        private void menuItem14_Click(object sender, EventArgs e)
        {
            //Gestion du matériel TA
            frmMateriel Materiel = new frmMateriel();
            Materiel.ShowDialog();
            Materiel.Dispose();
            Materiel = null;
        }
              

        //On ouvre la boite de recherche des fiches
        private void pBFiches_Click(object sender, EventArgs e)
        {
            //on regarde si la form fRechFicheConsult n'est pas déjà ouverte
            var fRechFicheConsult = Application.OpenForms["FRechFicheConsult"];

            //Elle n'est pas ouverte, on la créer et on l'ouvre
            if (fRechFicheConsult == null)
            {
                fRechFicheConsult = new FRechFicheConsult();
                fRechFicheConsult.Show();
            }
            else   //Sinon on lui donne le focus
                fRechFicheConsult.Activate();
        }

        private void AfficheFicheConsult(int Num_Appel)
        {
            //on regarde si on a des Fiches consult ouverte et si oui, s'il y en a une avec ce n° de consult                     
            int Existe = 0;

            foreach (Form form in Application.OpenForms)      //Liste les formes ouvertes
            {
                if (form.Name == "FicheConsult")  //S'il y en a une qui s'appelle FicheConsult
                {
                    //On regarde si elle a un control nommé lblNumVisite
                    Control[] lbl = form.Controls.Find("lblNumVisite", true);

                    string NumVisite = "";
                    
                    if (lbl.Length > 0)   //Si on en a un (Et il doit y en avoir un!)
                    {
                        NumVisite = lbl[0].Text;   //Récup du text du label qui a le n° de visite                    
                    }

                    //On compare pour voir si ça correspond à une déjà ouverte
                    if (NumVisite == Num_Appel.ToString())
                    {
                        Existe = 1;
                        //Elle est déjà ouverte, on la ramère au premier plan et on lui donne le focus
                        form.Activate();
                    }
                }
            }

            //Si la fiche n'existe pas déjà, on la créer
            if (Existe == 0)
            {
                FicheConsult ficheConsult = new FicheConsult();
                ficheConsult.numVisite = Num_Appel;
                ficheConsult.Show();
            }
        }


        private void AfficheRapport(int Num_Appel)
        {
            //on regarde si on a des fenetres Rapport ouverte et si oui, s'il y en a une avec ce n° de consult                     
            int Existe = 0;

            foreach (Form form in Application.OpenForms)      //Liste les formes ouvertes
            {
                if (form.Name == "FormRapport")  //S'il y en a une qui s'appelle FormRapport
                {
                    //On regarde si elle a un control nommé lblNumVisite
                    Control[] lbl = form.Controls.Find("lblNumVisite", true);

                    string NumVisite = "";

                    if (lbl.Length > 0)   //Si on en a un (Et il doit y en avoir un!)
                    {
                        NumVisite = lbl[0].Text;   //Récup du text du label qui a le n° de visite                    
                    }

                    //On compare pour voir si ça correspond à une déjà ouverte
                    if (NumVisite == Num_Appel.ToString())
                    {
                        Existe = 1;
                        //Elle est déjà ouverte, on la ramère au premier plan et on lui donne le focus
                        form.Activate();
                    }
                }
            }

            //Si la fiche n'existe pas déjà, on la créer
            if (Existe == 0)
            {
                FormRapport formRapport = new FormRapport();
                formRapport.numVisite = Num_Appel;
                formRapport.Show();
            }
        }

        private void menuItem15_Click(object sender, EventArgs e)
        {
            FArrangementPaiement fArrangementPaiement  = new FArrangementPaiement();
            fArrangementPaiement.ShowDialog();
            fArrangementPaiement.Dispose();          
        }
       

        private void menuPoursuite_Click(object sender, EventArgs e)
        {
            //On génère des fichiers avec les factures que l'on désire mettre en poursuite
            ImpressionDebiteurs impressionDebiteurs = new ImpressionDebiteurs();
            impressionDebiteurs.ShowDialog();
            impressionDebiteurs.Dispose();           
        }

        private void menuListe2emeRappel_Click(object sender, EventArgs e)
        {
            //On affiche la liste des 2emes rappels

        }

        private void menuSalairesMed_Click(object sender, EventArgs e)
        {
            FormSalaireMedecins formSalaireMedecins = new FormSalaireMedecins();
            formSalaireMedecins.ShowDialog();
            formSalaireMedecins.Dispose();
        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void zoomImageViewer1_Click(object sender, EventArgs e)
        {

        }

        private void menuItem16_Click(object sender, EventArgs e)
        {
            FMaterielsStats fMaterielsStats = new FMaterielsStats();
            fMaterielsStats.ShowDialog();
            fMaterielsStats.Dispose();
        }

        private void menuItem17_Click(object sender, EventArgs e)
        {
            AjoutMedecin ajoutMedecin = new AjoutMedecin();
            ajoutMedecin.ShowDialog();
            ajoutMedecin.Dispose();
        }
    }
}


//A faire
