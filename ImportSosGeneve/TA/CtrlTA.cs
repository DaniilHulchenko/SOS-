using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.IO;
using SosMedecins.SmartRapport;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using SosMedecins.SmartRapport.GestionApplication;
using System.Net.Mail;
using System.Configuration;
using System.Globalization;         //pour l'envoi de mails
using SosMedecins.SmartRapport.DAL;

namespace ImportSosGeneve
{
	/// <summary>
	/// Description résumée de CtrlTA.
	/// </summary>
    public class CtrlTA : System.Windows.Forms.UserControl
    {
        #region Déclaration des variables

        public frmGeneral m_frmgeneral = null;
        public ImportSosGeneve.frmTa m_frmTa = null;
        // Variables controles de la form

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tbAbonnement;
        private System.Windows.Forms.TabPage tbContacts;
        private System.Windows.Forms.TabPage tbDossierMedical;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtTa_Nom;
        private System.Windows.Forms.TextBox txtTa_Prenom;
        private System.Windows.Forms.TextBox txtTa_Naissance;
        private System.Windows.Forms.TextBox txtTa_Adresse;
        private System.Windows.Forms.RadioButton rdTa_Homme;
        private System.Windows.Forms.RadioButton rdTa_Femme;
        private System.Windows.Forms.TextBox txtTa_Np;
        private System.Windows.Forms.TextBox txtTa_Localite;
        private System.Windows.Forms.TextBox txtTa_Etage;
        private System.Windows.Forms.TextBox txtTa_Porte;
        private System.Windows.Forms.TextBox txtTa_FacNom;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtTa_FacPrenom;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtTa_FacLocalite;
        private System.Windows.Forms.TextBox txtTa_FacNP;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label LtitreRecherche;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox txtTa_No;
        private System.Windows.Forms.TextBox txtTa_Escalier;
        private System.Windows.Forms.TextBox txtTa_Batiment;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.TextBox txtTa_Interphone;
        private System.Windows.Forms.TextBox txtTa_Digicode;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.TextBox txtFind_Nom;
        private System.Windows.Forms.ListView lwAbonne;
        private System.Windows.Forms.TextBox txtFind_Cle;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ListView lwMemoire;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.TextBox txtOnglet2Adresse1;
        private System.Windows.Forms.TextBox txtOnglet2Nom1;
        private System.Windows.Forms.ComboBox cbOnglet2Lien1;
        private System.Windows.Forms.TextBox txtOnglet2PreNom1;
        private System.Windows.Forms.TextBox txtOnglet2NRue1;
        private System.Windows.Forms.TextBox txtOnglet2Np1;
        private System.Windows.Forms.TextBox txtOnglet2Localite1;
        private System.Windows.Forms.TextBox txtOnglet2Localite2;
        private System.Windows.Forms.TextBox txtOnglet2Np2;
        private System.Windows.Forms.TextBox txtOnglet2NRue2;
        private System.Windows.Forms.TextBox txtOnglet2PreNom2;
        private System.Windows.Forms.TextBox txtOnglet2Adresse2;
        private System.Windows.Forms.TextBox txtOnglet2Nom2;
        private System.Windows.Forms.ComboBox cbOnglet2Lien2;
        private System.Windows.Forms.TextBox txtOnglet3Poids;
        private System.Windows.Forms.TextBox txtOnglet3Attitudes;
        private System.Windows.Forms.TextBox txtOnglet3Pb;
        private System.Windows.Forms.ListView lwMedTTT;
        private System.Windows.Forms.CheckBox chkOnglet3Risque;
        private System.Windows.Forms.CheckBox chkOnglet3Fsasd;
        private System.Windows.Forms.CheckBox chkOnglet3AutreServices;
        private System.Windows.Forms.TextBox txtOnglet3Tel;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.TabPage tbCle;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.TextBox txtNumCle;
        private System.Windows.Forms.TextBox txtCommentaireCle;
        private System.Windows.Forms.Button btnVerifCle;
        private System.Windows.Forms.TextBox txtTa_FacAdresse;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.TextBox txtIdContrat;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.TextBox txtFindContrat;
        private System.Windows.Forms.RadioButton rdTriArchive1;
        private System.Windows.Forms.RadioButton rdTriArchive2;
        private System.Windows.Forms.RadioButton rdTriArchive3;
        private System.Windows.Forms.Button btnCopyAdresse;
        private System.Windows.Forms.CheckBox chkFaxFSASD;
        private System.Windows.Forms.CheckBox chkDossierBleu;
        private System.Windows.Forms.CheckBox chkClePresente;
        private System.Windows.Forms.TabPage tbJournal;
        private System.Windows.Forms.GroupBox gpJournal;
        private System.Windows.Forms.TextBox txtOnglet5Commentaire;
        private System.Windows.Forms.TextBox txtOnglet5ICE;
        private System.Windows.Forms.DateTimePicker dtOnglet5Le;
        private System.Windows.Forms.TextBox txtOnglet5EnvoiA;
        private System.Windows.Forms.TextBox txtOnglet5EnvoiDe;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.RadioButton rdOnglet5Annulation;
        private System.Windows.Forms.RadioButton rdOnglet5Dossier;
        private System.Windows.Forms.RadioButton rdOnglet5Cle;
        private FarPoint.Win.Spread.FpSpread fpOnglet5;
        private FarPoint.Win.Spread.SheetView fpOnglet5_Sheet1;
        private System.Windows.Forms.TextBox txtOnglet5NbCle;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.ListView lwUrgence;
        private System.Windows.Forms.TextBox txtOnglet3Medic;
        private System.Windows.Forms.ListBox lstCommunes;
        private System.Windows.Forms.TabPage tbFactures;
        private System.Windows.Forms.CheckBox cbExport;
        private System.Windows.Forms.CheckBox cbExporter;
        private System.Windows.Forms.Label lblCle;
        private System.Windows.Forms.Label lblContrat;
        private System.Windows.Forms.Label lblNom;
        private System.Windows.Forms.TextBox txtN_TA;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.Button btFIP;
        private System.Windows.Forms.CheckBox cbModifFiche;
        private GroupBox groupBox6;
        private MaskedTextBox EMaskTel1;
        private CheckBox CB_Sourd;
        private TextBox txtFind_Abonnement;
        private Label label43;
        private MaskedTextBox txtFind_Tel;
        private Label label58;
        private TextBox txtFind_DateNaiss;
        private TextBox textFindByNFacture;
        private Label labelNFacture;
        private RadioButton rdOnglet5Retourcontrat;       

        string stopRappel = "";
        int OrdrePermanent = 0;
        int Bloquer = 0;
        private TabPage tbBoitier;
        private SplitContainer splitContainer1;
        private Label LTitre;
        private RadioButton rBSexFactA;
        private RadioButton rBSexFactF;
        private RadioButton rBSexFactH;
        private SplitContainer splitContainer2;
        private Label label16;
        private Label label61;
        private Label label60;
        private Label label59;
        private Label label24;
        private Label label66;
        private Label label65;
        private Label label64;
        private Label label63;
        private Label label62;
        private MaskedTextBox txtOnglet2Tel1ter;
        private MaskedTextBox txtOnglet2Tel1bis;
        private MaskedTextBox txtOnglet2Tel1;
        private MaskedTextBox txtOnglet2Tel2ter;
        private MaskedTextBox txtOnglet2Tel2bis;
        private MaskedTextBox txtOnglet2Tel2;
        private Button bAnnuler1;
        private Button bValider1;
        private Button bNouveau1;
        private Button bSupprimer1;
        private Button bModifierEn1;
        private ImageList imageList1;
        private ToolTip toolTip1;
        private Button bAnnuler;
        private Button bEnregistrer;
        private Button bEnvoiMail;
        private Button bEtiquette;
        private Button bRechercher;
        private Button bNouveau2;
        private Button bValider2;
        private Button bModifierEn2;
        private Button bAnnuler2;
        private Button bSupprimer2;
        private Button bValideJournal;
        private Button bCancelJournal;
        private Button bSupprLigneJ;
        private Button bAjoutNvlLigneJ;
        private Button bSupprMedecin;
        private Button bAjoutMedecin;
        private Button bVerif;
        private TabPage tbTypeAbonnement;
        private SplitContainer splitContainer3;
        private SplitContainer splitContainer4;
        private GroupBox gBoxPeriode;
        private RadioButton rdPeriodFac4;
        private RadioButton rdPeriodFac3;
        private RadioButton rdPeriodFac0;
        private RadioButton rdPeriodFac2;
        private RadioButton rdPeriodFac1;
        private DateTimePicker dtDebutFacturation;
        private Label label45;
        private CheckBox cbOrdre;
        private CheckBox checkBoxSansRappelTA;
        private GroupBox groupBox5;
        private RadioButton rdOnglet3Type4;
        private RadioButton rdOnglet3Type3;
        private RadioButton rdOnglet3Type2;
        private RadioButton rdOnglet3Type1;
        private Label LIdAbonnement;
        private RadioButton rdOnglet3Type5;
        private Label label67;
        private RadioButton rBTypeBoitier3;
        private RadioButton rBTypeBoitier2;
        private RadioButton rBTypeBoitier1;
        private ComboBox comboBMateriel;
        private IContainer components;
        private Button bAjoutMat1;
        private TextBox tBoxSupprMatos;
        private Button bSupprMatos;
        private Label label69;
        private Label label68;
        private ListView listViewMat1;
        private ColumnHeader columnHeader23;
        private ColumnHeader columnHeader24;
        private ComboBox cBoxMotifChangement;
        private Label label70;
        public DataTable dtNvxMateriel = new DataTable();
        public DataTable dtNvxTel = new DataTable();    //Pour Ajout de n° de Tel
        public DataTable dtDelTel = new DataTable();    //Pour suppression de N° de Tel
        public string[] Desafection = new string[2];
        private ListView listViewTel;
        private ColumnHeader Telephone;
        private Button bAjouteTel1;
        private Label label71;
        private MaskedTextBox EmaskAjoutTel;
        private Label label72;
        private Label label73;
        private Label label74;
        public int AncienCheck = 0;
        private int ModifContact = 0;
        private DataGridView dataGridView1;
        private DataGridView dataGridView2;
        private Button bActiverFacture;
        private Label label75;
        private Button bsupprTel;
        private CheckBox cBoxFactBloque;
        private Label label76;
        private TextBox tBoxNumSerie;
        private int ModifContactUrgent = 0;
        #endregion

        #region Construction / Destruction de la form

        // Constructeur
        public CtrlTA(frmGeneral frm, ImportSosGeneve.frmTa frmTA)
        {
            // Cet appel est requis par le Concepteur de formulaires Windows.Forms.
            InitializeComponent();
            m_frmgeneral = frm;
            m_frmTa = frmTA;
            // TODO : ajoutez les initialisations après l'appel à InitializeComponent
            tbContacts.BackgroundImage = tbAbonnement.BackgroundImage;
            tbDossierMedical.BackgroundImage = tbAbonnement.BackgroundImage;
            tbCle.BackgroundImage = tbAbonnement.BackgroundImage;
            tbJournal.BackgroundImage = tbAbonnement.BackgroundImage;

            //On créer les colonnes de DataTable liste de Matos
            dtNvxMateriel.Columns.Add("VID", typeof(string));
            dtNvxMateriel.Columns.Add("Libelle", typeof(string));
            dtNvxMateriel.Columns.Add("IdAbonnement", typeof(string));

            //Idem pour le DataTable Liste des Telephones
            dtNvxTel.Columns.Add("NumTel", typeof(string));
            dtDelTel.Columns.Add("NumTel", typeof(string));
            
            Desafection[0] = string.Empty;
            Desafection[1] = string.Empty;

            AfficheAbonnement(null);
        }

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #endregion

        #region Code généré par le Concepteur de composants
        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        /// 
        public string MedecinsTraitant = "";        

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CtrlTA));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tbAbonnement = new System.Windows.Forms.TabPage();
            this.bVerif = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.EMaskTel1 = new System.Windows.Forms.MaskedTextBox();
            this.lstCommunes = new System.Windows.Forms.ListBox();
            this.cbModifFiche = new System.Windows.Forms.CheckBox();
            this.txtN_TA = new System.Windows.Forms.TextBox();
            this.label56 = new System.Windows.Forms.Label();
            this.cbExport = new System.Windows.Forms.CheckBox();
            this.txtTa_Interphone = new System.Windows.Forms.TextBox();
            this.txtTa_Digicode = new System.Windows.Forms.TextBox();
            this.label37 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.txtTa_Escalier = new System.Windows.Forms.TextBox();
            this.txtTa_Batiment = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.txtTa_No = new System.Windows.Forms.TextBox();
            this.rdTa_Femme = new System.Windows.Forms.RadioButton();
            this.rdTa_Homme = new System.Windows.Forms.RadioButton();
            this.txtTa_Porte = new System.Windows.Forms.TextBox();
            this.txtTa_Etage = new System.Windows.Forms.TextBox();
            this.txtTa_Localite = new System.Windows.Forms.TextBox();
            this.txtTa_Np = new System.Windows.Forms.TextBox();
            this.txtTa_Adresse = new System.Windows.Forms.TextBox();
            this.txtTa_Naissance = new System.Windows.Forms.TextBox();
            this.txtTa_Prenom = new System.Windows.Forms.TextBox();
            this.txtTa_Nom = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.label73 = new System.Windows.Forms.Label();
            this.btnCopyAdresse = new System.Windows.Forms.Button();
            this.rBSexFactH = new System.Windows.Forms.RadioButton();
            this.rBSexFactA = new System.Windows.Forms.RadioButton();
            this.txtTa_FacAdresse = new System.Windows.Forms.TextBox();
            this.rBSexFactF = new System.Windows.Forms.RadioButton();
            this.txtTa_FacLocalite = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label57 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtTa_FacNP = new System.Windows.Forms.TextBox();
            this.label41 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtTa_FacPrenom = new System.Windows.Forms.TextBox();
            this.txtTa_FacNom = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.bsupprTel = new System.Windows.Forms.Button();
            this.label75 = new System.Windows.Forms.Label();
            this.label74 = new System.Windows.Forms.Label();
            this.EmaskAjoutTel = new System.Windows.Forms.MaskedTextBox();
            this.label72 = new System.Windows.Forms.Label();
            this.label71 = new System.Windows.Forms.Label();
            this.bAjouteTel1 = new System.Windows.Forms.Button();
            this.listViewTel = new System.Windows.Forms.ListView();
            this.Telephone = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbExporter = new System.Windows.Forms.CheckBox();
            this.tbTypeAbonnement = new System.Windows.Forms.TabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.gBoxPeriode = new System.Windows.Forms.GroupBox();
            this.rdPeriodFac4 = new System.Windows.Forms.RadioButton();
            this.rdPeriodFac3 = new System.Windows.Forms.RadioButton();
            this.rdPeriodFac0 = new System.Windows.Forms.RadioButton();
            this.rdPeriodFac2 = new System.Windows.Forms.RadioButton();
            this.rdPeriodFac1 = new System.Windows.Forms.RadioButton();
            this.dtDebutFacturation = new System.Windows.Forms.DateTimePicker();
            this.label45 = new System.Windows.Forms.Label();
            this.cbOrdre = new System.Windows.Forms.CheckBox();
            this.checkBoxSansRappelTA = new System.Windows.Forms.CheckBox();
            this.bActiverFacture = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.rdOnglet3Type5 = new System.Windows.Forms.RadioButton();
            this.rdOnglet3Type4 = new System.Windows.Forms.RadioButton();
            this.rdOnglet3Type3 = new System.Windows.Forms.RadioButton();
            this.rdOnglet3Type2 = new System.Windows.Forms.RadioButton();
            this.rdOnglet3Type1 = new System.Windows.Forms.RadioButton();
            this.label76 = new System.Windows.Forms.Label();
            this.tBoxNumSerie = new System.Windows.Forms.TextBox();
            this.cBoxMotifChangement = new System.Windows.Forms.ComboBox();
            this.label70 = new System.Windows.Forms.Label();
            this.listViewMat1 = new System.Windows.Forms.ListView();
            this.columnHeader23 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader24 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label69 = new System.Windows.Forms.Label();
            this.label68 = new System.Windows.Forms.Label();
            this.bSupprMatos = new System.Windows.Forms.Button();
            this.tBoxSupprMatos = new System.Windows.Forms.TextBox();
            this.bAjoutMat1 = new System.Windows.Forms.Button();
            this.comboBMateriel = new System.Windows.Forms.ComboBox();
            this.rBTypeBoitier3 = new System.Windows.Forms.RadioButton();
            this.rBTypeBoitier2 = new System.Windows.Forms.RadioButton();
            this.rBTypeBoitier1 = new System.Windows.Forms.RadioButton();
            this.tbContacts = new System.Windows.Forms.TabPage();
            this.lwUrgence = new System.Windows.Forms.ListView();
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label55 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.bSupprimer2 = new System.Windows.Forms.Button();
            this.bModifierEn2 = new System.Windows.Forms.Button();
            this.bAnnuler2 = new System.Windows.Forms.Button();
            this.bNouveau2 = new System.Windows.Forms.Button();
            this.bValider2 = new System.Windows.Forms.Button();
            this.txtOnglet2Tel2ter = new System.Windows.Forms.MaskedTextBox();
            this.txtOnglet2Tel2bis = new System.Windows.Forms.MaskedTextBox();
            this.txtOnglet2Tel2 = new System.Windows.Forms.MaskedTextBox();
            this.label66 = new System.Windows.Forms.Label();
            this.label65 = new System.Windows.Forms.Label();
            this.label64 = new System.Windows.Forms.Label();
            this.label63 = new System.Windows.Forms.Label();
            this.label62 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.txtOnglet2Localite2 = new System.Windows.Forms.TextBox();
            this.txtOnglet2Np2 = new System.Windows.Forms.TextBox();
            this.txtOnglet2NRue2 = new System.Windows.Forms.TextBox();
            this.txtOnglet2PreNom2 = new System.Windows.Forms.TextBox();
            this.txtOnglet2Adresse2 = new System.Windows.Forms.TextBox();
            this.txtOnglet2Nom2 = new System.Windows.Forms.TextBox();
            this.cbOnglet2Lien2 = new System.Windows.Forms.ComboBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.bNouveau1 = new System.Windows.Forms.Button();
            this.bModifierEn1 = new System.Windows.Forms.Button();
            this.bSupprimer1 = new System.Windows.Forms.Button();
            this.bAnnuler1 = new System.Windows.Forms.Button();
            this.bValider1 = new System.Windows.Forms.Button();
            this.txtOnglet2Tel1ter = new System.Windows.Forms.MaskedTextBox();
            this.txtOnglet2Tel1bis = new System.Windows.Forms.MaskedTextBox();
            this.txtOnglet2Tel1 = new System.Windows.Forms.MaskedTextBox();
            this.label61 = new System.Windows.Forms.Label();
            this.label60 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.txtOnglet2Localite1 = new System.Windows.Forms.TextBox();
            this.txtOnglet2Np1 = new System.Windows.Forms.TextBox();
            this.txtOnglet2NRue1 = new System.Windows.Forms.TextBox();
            this.label51 = new System.Windows.Forms.Label();
            this.txtOnglet2PreNom1 = new System.Windows.Forms.TextBox();
            this.txtOnglet2Adresse1 = new System.Windows.Forms.TextBox();
            this.txtOnglet2Nom1 = new System.Windows.Forms.TextBox();
            this.cbOnglet2Lien1 = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.lwMemoire = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label19 = new System.Windows.Forms.Label();
            this.tbCle = new System.Windows.Forms.TabPage();
            this.label67 = new System.Windows.Forms.Label();
            this.chkClePresente = new System.Windows.Forms.CheckBox();
            this.chkDossierBleu = new System.Windows.Forms.CheckBox();
            this.chkFaxFSASD = new System.Windows.Forms.CheckBox();
            this.txtIdContrat = new System.Windows.Forms.TextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.btnVerifCle = new System.Windows.Forms.Button();
            this.txtCommentaireCle = new System.Windows.Forms.TextBox();
            this.txtNumCle = new System.Windows.Forms.TextBox();
            this.label40 = new System.Windows.Forms.Label();
            this.tbJournal = new System.Windows.Forms.TabPage();
            this.bSupprLigneJ = new System.Windows.Forms.Button();
            this.bAjoutNvlLigneJ = new System.Windows.Forms.Button();
            this.fpOnglet5 = new FarPoint.Win.Spread.FpSpread();
            this.fpOnglet5_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.gpJournal = new System.Windows.Forms.GroupBox();
            this.bCancelJournal = new System.Windows.Forms.Button();
            this.bValideJournal = new System.Windows.Forms.Button();
            this.rdOnglet5Retourcontrat = new System.Windows.Forms.RadioButton();
            this.txtOnglet5NbCle = new System.Windows.Forms.TextBox();
            this.label50 = new System.Windows.Forms.Label();
            this.txtOnglet5Commentaire = new System.Windows.Forms.TextBox();
            this.txtOnglet5ICE = new System.Windows.Forms.TextBox();
            this.dtOnglet5Le = new System.Windows.Forms.DateTimePicker();
            this.txtOnglet5EnvoiA = new System.Windows.Forms.TextBox();
            this.txtOnglet5EnvoiDe = new System.Windows.Forms.TextBox();
            this.label49 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.rdOnglet5Annulation = new System.Windows.Forms.RadioButton();
            this.rdOnglet5Dossier = new System.Windows.Forms.RadioButton();
            this.rdOnglet5Cle = new System.Windows.Forms.RadioButton();
            this.tbFactures = new System.Windows.Forms.TabPage();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tbDossierMedical = new System.Windows.Forms.TabPage();
            this.bSupprMedecin = new System.Windows.Forms.Button();
            this.bAjoutMedecin = new System.Windows.Forms.Button();
            this.CB_Sourd = new System.Windows.Forms.CheckBox();
            this.txtOnglet3Medic = new System.Windows.Forms.TextBox();
            this.txtOnglet3Tel = new System.Windows.Forms.TextBox();
            this.txtOnglet3Poids = new System.Windows.Forms.TextBox();
            this.txtOnglet3Attitudes = new System.Windows.Forms.TextBox();
            this.txtOnglet3Pb = new System.Windows.Forms.TextBox();
            this.lwMedTTT = new System.Windows.Forms.ListView();
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chkOnglet3AutreServices = new System.Windows.Forms.CheckBox();
            this.chkOnglet3Fsasd = new System.Windows.Forms.CheckBox();
            this.label34 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.chkOnglet3Risque = new System.Windows.Forms.CheckBox();
            this.label31 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.tbBoitier = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.LIdAbonnement = new System.Windows.Forms.Label();
            this.btFIP = new System.Windows.Forms.Button();
            this.lblNom = new System.Windows.Forms.Label();
            this.lblCle = new System.Windows.Forms.Label();
            this.lblContrat = new System.Windows.Forms.Label();
            this.LtitreRecherche = new System.Windows.Forms.Label();
            this.txtFind_Nom = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.lwAbonne = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label28 = new System.Windows.Forms.Label();
            this.txtFind_Cle = new System.Windows.Forms.TextBox();
            this.label44 = new System.Windows.Forms.Label();
            this.txtFindContrat = new System.Windows.Forms.TextBox();
            this.rdTriArchive1 = new System.Windows.Forms.RadioButton();
            this.rdTriArchive2 = new System.Windows.Forms.RadioButton();
            this.rdTriArchive3 = new System.Windows.Forms.RadioButton();
            this.txtFind_Abonnement = new System.Windows.Forms.TextBox();
            this.label43 = new System.Windows.Forms.Label();
            this.txtFind_Tel = new System.Windows.Forms.MaskedTextBox();
            this.label58 = new System.Windows.Forms.Label();
            this.txtFind_DateNaiss = new System.Windows.Forms.TextBox();
            this.textFindByNFacture = new System.Windows.Forms.TextBox();
            this.labelNFacture = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cBoxFactBloque = new System.Windows.Forms.CheckBox();
            this.bRechercher = new System.Windows.Forms.Button();
            this.LTitre = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.bAnnuler = new System.Windows.Forms.Button();
            this.bEnregistrer = new System.Windows.Forms.Button();
            this.bEnvoiMail = new System.Windows.Forms.Button();
            this.bEtiquette = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tbAbonnement.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tbTypeAbonnement.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.gBoxPeriode.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tbContacts.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tbCle.SuspendLayout();
            this.tbJournal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpOnglet5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpOnglet5_Sheet1)).BeginInit();
            this.gpJournal.SuspendLayout();
            this.tbFactures.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tbDossierMedical.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tbAbonnement);
            this.tabControl1.Controls.Add(this.tbTypeAbonnement);
            this.tabControl1.Controls.Add(this.tbContacts);
            this.tabControl1.Controls.Add(this.tbCle);
            this.tabControl1.Controls.Add(this.tbJournal);
            this.tabControl1.Controls.Add(this.tbFactures);
            this.tabControl1.Controls.Add(this.tbDossierMedical);
            this.tabControl1.Controls.Add(this.tbBoitier);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(788, 720);
            this.tabControl1.TabIndex = 13;
            this.tabControl1.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl1_Selecting);
            // 
            // tbAbonnement
            // 
            this.tbAbonnement.BackColor = System.Drawing.Color.CadetBlue;
            this.tbAbonnement.Controls.Add(this.bVerif);
            this.tbAbonnement.Controls.Add(this.EMaskTel1);
            this.tbAbonnement.Controls.Add(this.lstCommunes);
            this.tbAbonnement.Controls.Add(this.cbModifFiche);
            this.tbAbonnement.Controls.Add(this.txtN_TA);
            this.tbAbonnement.Controls.Add(this.label56);
            this.tbAbonnement.Controls.Add(this.cbExport);
            this.tbAbonnement.Controls.Add(this.txtTa_Interphone);
            this.tbAbonnement.Controls.Add(this.txtTa_Digicode);
            this.tbAbonnement.Controls.Add(this.label37);
            this.tbAbonnement.Controls.Add(this.label38);
            this.tbAbonnement.Controls.Add(this.txtTa_Escalier);
            this.tbAbonnement.Controls.Add(this.txtTa_Batiment);
            this.tbAbonnement.Controls.Add(this.label35);
            this.tbAbonnement.Controls.Add(this.label36);
            this.tbAbonnement.Controls.Add(this.txtTa_No);
            this.tbAbonnement.Controls.Add(this.rdTa_Femme);
            this.tbAbonnement.Controls.Add(this.rdTa_Homme);
            this.tbAbonnement.Controls.Add(this.txtTa_Porte);
            this.tbAbonnement.Controls.Add(this.txtTa_Etage);
            this.tbAbonnement.Controls.Add(this.txtTa_Localite);
            this.tbAbonnement.Controls.Add(this.txtTa_Np);
            this.tbAbonnement.Controls.Add(this.txtTa_Adresse);
            this.tbAbonnement.Controls.Add(this.txtTa_Naissance);
            this.tbAbonnement.Controls.Add(this.txtTa_Prenom);
            this.tbAbonnement.Controls.Add(this.txtTa_Nom);
            this.tbAbonnement.Controls.Add(this.groupBox1);
            this.tbAbonnement.Controls.Add(this.label10);
            this.tbAbonnement.Controls.Add(this.label9);
            this.tbAbonnement.Controls.Add(this.label8);
            this.tbAbonnement.Controls.Add(this.label7);
            this.tbAbonnement.Controls.Add(this.label6);
            this.tbAbonnement.Controls.Add(this.label5);
            this.tbAbonnement.Controls.Add(this.label4);
            this.tbAbonnement.Controls.Add(this.label3);
            this.tbAbonnement.Controls.Add(this.label2);
            this.tbAbonnement.Controls.Add(this.label1);
            this.tbAbonnement.Controls.Add(this.cbExporter);
            this.tbAbonnement.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbAbonnement.Location = new System.Drawing.Point(4, 25);
            this.tbAbonnement.Name = "tbAbonnement";
            this.tbAbonnement.Size = new System.Drawing.Size(780, 691);
            this.tbAbonnement.TabIndex = 0;
            this.tbAbonnement.Text = "Abonné";
            // 
            // bVerif
            // 
            this.bVerif.BackColor = System.Drawing.Color.Transparent;
            this.bVerif.BackgroundImage = global::ImportSosGeneve.Properties.Resources.bajout;
            this.bVerif.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bVerif.FlatAppearance.BorderSize = 0;
            this.bVerif.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bVerif.ImageIndex = 18;
            this.bVerif.ImageList = this.imageList1;
            this.bVerif.Location = new System.Drawing.Point(244, 69);
            this.bVerif.Name = "bVerif";
            this.bVerif.Size = new System.Drawing.Size(60, 60);
            this.bVerif.TabIndex = 54;
            this.toolTip1.SetToolTip(this.bVerif, "Vérifier si le patient existe");
            this.bVerif.UseVisualStyleBackColor = false;
            this.bVerif.Click += new System.EventHandler(this.bVerif_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "bSupprRouge.png");
            this.imageList1.Images.SetKeyName(1, "bsupprimerOff.png");
            this.imageList1.Images.SetKeyName(2, "brondValider.png");
            this.imageList1.Images.SetKeyName(3, "brondValiderOff.png");
            this.imageList1.Images.SetKeyName(4, "bondCancel.png");
            this.imageList1.Images.SetKeyName(5, "bCancelOff.png");
            this.imageList1.Images.SetKeyName(6, "bajout.png");
            this.imageList1.Images.SetKeyName(7, "bajoutOff.png");
            this.imageList1.Images.SetKeyName(8, "bEnvoi.png");
            this.imageList1.Images.SetKeyName(9, "bEnvoiOff.png");
            this.imageList1.Images.SetKeyName(10, "boutonsModifier.png");
            this.imageList1.Images.SetKeyName(11, "boutonsModifierOff.png");
            this.imageList1.Images.SetKeyName(12, "BarchiverOn.png");
            this.imageList1.Images.SetKeyName(13, "BdesarchiverOn.png");
            this.imageList1.Images.SetKeyName(14, "cadenasVert.png");
            this.imageList1.Images.SetKeyName(15, "cadenasRouge.png");
            this.imageList1.Images.SetKeyName(16, "BNvxDossierOn.png");
            this.imageList1.Images.SetKeyName(17, "exit.png");
            this.imageList1.Images.SetKeyName(18, "bVerifPatientOn.png");
            this.imageList1.Images.SetKeyName(19, "bVerifPatientOff.png");
            this.imageList1.Images.SetKeyName(20, "bfactureOn.png");
            this.imageList1.Images.SetKeyName(21, "bfactureOff.png");
            // 
            // EMaskTel1
            // 
            this.EMaskTel1.BackColor = System.Drawing.Color.White;
            this.EMaskTel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EMaskTel1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.EMaskTel1.Location = new System.Drawing.Point(623, 18);
            this.EMaskTel1.Mask = "################";
            this.EMaskTel1.Name = "EMaskTel1";
            this.EMaskTel1.Size = new System.Drawing.Size(139, 22);
            this.EMaskTel1.TabIndex = 39;
            this.EMaskTel1.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            // 
            // lstCommunes
            // 
            this.lstCommunes.ItemHeight = 16;
            this.lstCommunes.Items.AddRange(new object[] {
            "Aire-la-Ville",
            "Anières",
            "Avully",
            "Avusy",
            "Bardonnex",
            "Bellevue",
            "Bernex",
            "Carouge",
            "Cartigny",
            "Céligny",
            "Chancy",
            "Chêne-Bougeries",
            "Chêne-Bourg",
            "Choulex",
            "Collex-Bossy",
            "Collonge-Bellerive",
            "Cologny",
            "Confignon",
            "Corsier",
            "Dardagny",
            "Genève",
            "Genthod",
            "Le grand-Saconnex",
            "Gy",
            "Hermance",
            "Jussy",
            "Laconnex",
            "Lancy",
            "Meinier",
            "Meyrin",
            "Onex",
            "Perly-Certoux",
            "Plan-Les-Ouates",
            "Pregny-Chambésy",
            "Presinge",
            "Puplinge",
            "Russin",
            "Satigny",
            "Soral",
            "Thônex",
            "Troinex",
            "Vandoeuvre",
            "Vernier",
            "Versoix",
            "Veyrier"});
            this.lstCommunes.Location = new System.Drawing.Point(404, 151);
            this.lstCommunes.Name = "lstCommunes";
            this.lstCommunes.Size = new System.Drawing.Size(232, 84);
            this.lstCommunes.TabIndex = 22;
            this.lstCommunes.Visible = false;
            this.lstCommunes.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lstCommunes_KeyUp);
            this.lstCommunes.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lstCommunes_MouseUp);
            // 
            // cbModifFiche
            // 
            this.cbModifFiche.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbModifFiche.Location = new System.Drawing.Point(224, 255);
            this.cbModifFiche.Name = "cbModifFiche";
            this.cbModifFiche.Size = new System.Drawing.Size(88, 24);
            this.cbModifFiche.TabIndex = 37;
            this.cbModifFiche.Text = "Export Fiche";
            this.cbModifFiche.Visible = false;
            // 
            // txtN_TA
            // 
            this.txtN_TA.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtN_TA.Location = new System.Drawing.Point(120, 255);
            this.txtN_TA.Name = "txtN_TA";
            this.txtN_TA.Size = new System.Drawing.Size(88, 22);
            this.txtN_TA.TabIndex = 36;
            // 
            // label56
            // 
            this.label56.BackColor = System.Drawing.Color.Transparent;
            this.label56.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label56.Location = new System.Drawing.Point(24, 255);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(104, 24);
            this.label56.TabIndex = 35;
            this.label56.Text = "N TA SOS :";
            // 
            // cbExport
            // 
            this.cbExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbExport.Location = new System.Drawing.Point(608, 69);
            this.cbExport.Name = "cbExport";
            this.cbExport.Size = new System.Drawing.Size(56, 24);
            this.cbExport.TabIndex = 13;
            this.cbExport.Text = "Prêt";
            // 
            // txtTa_Interphone
            // 
            this.txtTa_Interphone.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTa_Interphone.Location = new System.Drawing.Point(328, 231);
            this.txtTa_Interphone.Name = "txtTa_Interphone";
            this.txtTa_Interphone.Size = new System.Drawing.Size(101, 22);
            this.txtTa_Interphone.TabIndex = 34;
            // 
            // txtTa_Digicode
            // 
            this.txtTa_Digicode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTa_Digicode.Location = new System.Drawing.Point(120, 231);
            this.txtTa_Digicode.Name = "txtTa_Digicode";
            this.txtTa_Digicode.Size = new System.Drawing.Size(88, 22);
            this.txtTa_Digicode.TabIndex = 32;
            // 
            // label37
            // 
            this.label37.BackColor = System.Drawing.Color.Transparent;
            this.label37.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.Location = new System.Drawing.Point(241, 231);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(80, 24);
            this.label37.TabIndex = 33;
            this.label37.Text = "Interphone :";
            // 
            // label38
            // 
            this.label38.BackColor = System.Drawing.Color.Transparent;
            this.label38.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.Location = new System.Drawing.Point(24, 231);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(104, 24);
            this.label38.TabIndex = 31;
            this.label38.Text = "Code Porte :";
            // 
            // txtTa_Escalier
            // 
            this.txtTa_Escalier.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTa_Escalier.Location = new System.Drawing.Point(328, 207);
            this.txtTa_Escalier.Name = "txtTa_Escalier";
            this.txtTa_Escalier.Size = new System.Drawing.Size(101, 22);
            this.txtTa_Escalier.TabIndex = 30;
            // 
            // txtTa_Batiment
            // 
            this.txtTa_Batiment.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTa_Batiment.Location = new System.Drawing.Point(120, 207);
            this.txtTa_Batiment.Name = "txtTa_Batiment";
            this.txtTa_Batiment.Size = new System.Drawing.Size(88, 22);
            this.txtTa_Batiment.TabIndex = 28;
            // 
            // label35
            // 
            this.label35.BackColor = System.Drawing.Color.Transparent;
            this.label35.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.Location = new System.Drawing.Point(255, 207);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(69, 24);
            this.label35.TabIndex = 29;
            this.label35.Text = "Escalier :";
            // 
            // label36
            // 
            this.label36.BackColor = System.Drawing.Color.Transparent;
            this.label36.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.Location = new System.Drawing.Point(24, 207);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(104, 24);
            this.label36.TabIndex = 27;
            this.label36.Text = "Batiment :";
            // 
            // txtTa_No
            // 
            this.txtTa_No.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTa_No.Location = new System.Drawing.Point(120, 159);
            this.txtTa_No.Name = "txtTa_No";
            this.txtTa_No.Size = new System.Drawing.Size(56, 22);
            this.txtTa_No.TabIndex = 12;
            // 
            // rdTa_Femme
            // 
            this.rdTa_Femme.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdTa_Femme.Location = new System.Drawing.Point(436, 90);
            this.rdTa_Femme.Name = "rdTa_Femme";
            this.rdTa_Femme.Size = new System.Drawing.Size(41, 24);
            this.rdTa_Femme.TabIndex = 12;
            this.rdTa_Femme.Text = "F";
            // 
            // rdTa_Homme
            // 
            this.rdTa_Homme.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdTa_Homme.Location = new System.Drawing.Point(489, 90);
            this.rdTa_Homme.Name = "rdTa_Homme";
            this.rdTa_Homme.Size = new System.Drawing.Size(41, 24);
            this.rdTa_Homme.TabIndex = 11;
            this.rdTa_Homme.Text = "H";
            // 
            // txtTa_Porte
            // 
            this.txtTa_Porte.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTa_Porte.Location = new System.Drawing.Point(328, 183);
            this.txtTa_Porte.Name = "txtTa_Porte";
            this.txtTa_Porte.Size = new System.Drawing.Size(101, 22);
            this.txtTa_Porte.TabIndex = 26;
            // 
            // txtTa_Etage
            // 
            this.txtTa_Etage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTa_Etage.Location = new System.Drawing.Point(120, 183);
            this.txtTa_Etage.Name = "txtTa_Etage";
            this.txtTa_Etage.Size = new System.Drawing.Size(88, 22);
            this.txtTa_Etage.TabIndex = 24;
            this.txtTa_Etage.Enter += new System.EventHandler(this.txtTa_Etage_Enter);
            // 
            // txtTa_Localite
            // 
            this.txtTa_Localite.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTa_Localite.Location = new System.Drawing.Point(328, 135);
            this.txtTa_Localite.Name = "txtTa_Localite";
            this.txtTa_Localite.Size = new System.Drawing.Size(280, 22);
            this.txtTa_Localite.TabIndex = 11;
            this.txtTa_Localite.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTa_Localite_KeyUp);
            // 
            // txtTa_Np
            // 
            this.txtTa_Np.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTa_Np.Location = new System.Drawing.Point(120, 135);
            this.txtTa_Np.Name = "txtTa_Np";
            this.txtTa_Np.Size = new System.Drawing.Size(88, 22);
            this.txtTa_Np.TabIndex = 10;
            // 
            // txtTa_Adresse
            // 
            this.txtTa_Adresse.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTa_Adresse.Location = new System.Drawing.Point(176, 159);
            this.txtTa_Adresse.Name = "txtTa_Adresse";
            this.txtTa_Adresse.Size = new System.Drawing.Size(432, 22);
            this.txtTa_Adresse.TabIndex = 13;
            this.txtTa_Adresse.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTa_Adresse_KeyUp);
            // 
            // txtTa_Naissance
            // 
            this.txtTa_Naissance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTa_Naissance.Location = new System.Drawing.Point(120, 86);
            this.txtTa_Naissance.Name = "txtTa_Naissance";
            this.txtTa_Naissance.Size = new System.Drawing.Size(96, 22);
            this.txtTa_Naissance.TabIndex = 9;
            // 
            // txtTa_Prenom
            // 
            this.txtTa_Prenom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTa_Prenom.Location = new System.Drawing.Point(120, 42);
            this.txtTa_Prenom.Name = "txtTa_Prenom";
            this.txtTa_Prenom.Size = new System.Drawing.Size(264, 22);
            this.txtTa_Prenom.TabIndex = 6;
            // 
            // txtTa_Nom
            // 
            this.txtTa_Nom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTa_Nom.Location = new System.Drawing.Point(120, 16);
            this.txtTa_Nom.Name = "txtTa_Nom";
            this.txtTa_Nom.Size = new System.Drawing.Size(264, 22);
            this.txtTa_Nom.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.splitContainer2);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(3, 294);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(772, 381);
            this.groupBox1.TabIndex = 38;
            this.groupBox1.TabStop = false;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer2.Location = new System.Drawing.Point(6, 18);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.label73);
            this.splitContainer2.Panel1.Controls.Add(this.btnCopyAdresse);
            this.splitContainer2.Panel1.Controls.Add(this.rBSexFactH);
            this.splitContainer2.Panel1.Controls.Add(this.rBSexFactA);
            this.splitContainer2.Panel1.Controls.Add(this.txtTa_FacAdresse);
            this.splitContainer2.Panel1.Controls.Add(this.rBSexFactF);
            this.splitContainer2.Panel1.Controls.Add(this.txtTa_FacLocalite);
            this.splitContainer2.Panel1.Controls.Add(this.label14);
            this.splitContainer2.Panel1.Controls.Add(this.label57);
            this.splitContainer2.Panel1.Controls.Add(this.label13);
            this.splitContainer2.Panel1.Controls.Add(this.txtTa_FacNP);
            this.splitContainer2.Panel1.Controls.Add(this.label41);
            this.splitContainer2.Panel1.Controls.Add(this.label11);
            this.splitContainer2.Panel1.Controls.Add(this.txtTa_FacPrenom);
            this.splitContainer2.Panel1.Controls.Add(this.txtTa_FacNom);
            this.splitContainer2.Panel1.Controls.Add(this.label12);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.bsupprTel);
            this.splitContainer2.Panel2.Controls.Add(this.label75);
            this.splitContainer2.Panel2.Controls.Add(this.label74);
            this.splitContainer2.Panel2.Controls.Add(this.EmaskAjoutTel);
            this.splitContainer2.Panel2.Controls.Add(this.label72);
            this.splitContainer2.Panel2.Controls.Add(this.label71);
            this.splitContainer2.Panel2.Controls.Add(this.bAjouteTel1);
            this.splitContainer2.Panel2.Controls.Add(this.listViewTel);
            this.splitContainer2.Size = new System.Drawing.Size(760, 363);
            this.splitContainer2.SplitterDistance = 473;
            this.splitContainer2.TabIndex = 27;
            // 
            // label73
            // 
            this.label73.BackColor = System.Drawing.Color.Transparent;
            this.label73.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label73.Location = new System.Drawing.Point(100, 9);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(216, 24);
            this.label73.TabIndex = 36;
            this.label73.Text = "Adresse de facturation";
            // 
            // btnCopyAdresse
            // 
            this.btnCopyAdresse.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCopyAdresse.ForeColor = System.Drawing.Color.Black;
            this.btnCopyAdresse.Location = new System.Drawing.Point(374, 9);
            this.btnCopyAdresse.Name = "btnCopyAdresse";
            this.btnCopyAdresse.Size = new System.Drawing.Size(79, 62);
            this.btnCopyAdresse.TabIndex = 19;
            this.btnCopyAdresse.Text = "Copier l\'adresse";
            this.btnCopyAdresse.UseVisualStyleBackColor = false;
            this.btnCopyAdresse.Click += new System.EventHandler(this.btnCopyAdresse_Click);
            // 
            // rBSexFactH
            // 
            this.rBSexFactH.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rBSexFactH.Location = new System.Drawing.Point(127, 47);
            this.rBSexFactH.Name = "rBSexFactH";
            this.rBSexFactH.Size = new System.Drawing.Size(33, 24);
            this.rBSexFactH.TabIndex = 24;
            this.rBSexFactH.Text = "H";
            // 
            // rBSexFactA
            // 
            this.rBSexFactA.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rBSexFactA.Location = new System.Drawing.Point(181, 47);
            this.rBSexFactA.Name = "rBSexFactA";
            this.rBSexFactA.Size = new System.Drawing.Size(34, 24);
            this.rBSexFactA.TabIndex = 26;
            this.rBSexFactA.Text = "A";
            // 
            // txtTa_FacAdresse
            // 
            this.txtTa_FacAdresse.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTa_FacAdresse.Location = new System.Drawing.Point(10, 224);
            this.txtTa_FacAdresse.Multiline = true;
            this.txtTa_FacAdresse.Name = "txtTa_FacAdresse";
            this.txtTa_FacAdresse.Size = new System.Drawing.Size(416, 118);
            this.txtTa_FacAdresse.TabIndex = 20;
            // 
            // rBSexFactF
            // 
            this.rBSexFactF.Checked = true;
            this.rBSexFactF.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rBSexFactF.Location = new System.Drawing.Point(86, 47);
            this.rBSexFactF.Name = "rBSexFactF";
            this.rBSexFactF.Size = new System.Drawing.Size(32, 24);
            this.rBSexFactF.TabIndex = 25;
            this.rBSexFactF.TabStop = true;
            this.rBSexFactF.Text = "F";
            // 
            // txtTa_FacLocalite
            // 
            this.txtTa_FacLocalite.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTa_FacLocalite.Location = new System.Drawing.Point(86, 159);
            this.txtTa_FacLocalite.Name = "txtTa_FacLocalite";
            this.txtTa_FacLocalite.Size = new System.Drawing.Size(302, 22);
            this.txtTa_FacLocalite.TabIndex = 17;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(12, 131);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(75, 20);
            this.label14.TabIndex = 14;
            this.label14.Text = "N° Postal :";
            // 
            // label57
            // 
            this.label57.BackColor = System.Drawing.Color.Transparent;
            this.label57.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label57.Location = new System.Drawing.Point(12, 51);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(56, 24);
            this.label57.TabIndex = 0;
            this.label57.Text = "Sexe :";
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(21, 162);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(66, 24);
            this.label13.TabIndex = 16;
            this.label13.Text = "Localité :";
            // 
            // txtTa_FacNP
            // 
            this.txtTa_FacNP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTa_FacNP.Location = new System.Drawing.Point(86, 131);
            this.txtTa_FacNP.Name = "txtTa_FacNP";
            this.txtTa_FacNP.Size = new System.Drawing.Size(88, 22);
            this.txtTa_FacNP.TabIndex = 15;
            // 
            // label41
            // 
            this.label41.BackColor = System.Drawing.Color.Transparent;
            this.label41.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label41.Location = new System.Drawing.Point(10, 197);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(123, 24);
            this.label41.TabIndex = 18;
            this.label41.Text = "Adresse complète :";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(39, 80);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(48, 24);
            this.label11.TabIndex = 5;
            this.label11.Text = "Nom :";
            // 
            // txtTa_FacPrenom
            // 
            this.txtTa_FacPrenom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTa_FacPrenom.Location = new System.Drawing.Point(86, 103);
            this.txtTa_FacPrenom.Name = "txtTa_FacPrenom";
            this.txtTa_FacPrenom.Size = new System.Drawing.Size(277, 22);
            this.txtTa_FacPrenom.TabIndex = 10;
            // 
            // txtTa_FacNom
            // 
            this.txtTa_FacNom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTa_FacNom.Location = new System.Drawing.Point(86, 77);
            this.txtTa_FacNom.Name = "txtTa_FacNom";
            this.txtTa_FacNom.Size = new System.Drawing.Size(277, 22);
            this.txtTa_FacNom.TabIndex = 6;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(21, 104);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(64, 24);
            this.label12.TabIndex = 9;
            this.label12.Text = "Prénom :";
            // 
            // bsupprTel
            // 
            this.bsupprTel.BackColor = System.Drawing.Color.Transparent;
            this.bsupprTel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bsupprTel.FlatAppearance.BorderSize = 0;
            this.bsupprTel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bsupprTel.ImageIndex = 0;
            this.bsupprTel.ImageList = this.imageList1;
            this.bsupprTel.Location = new System.Drawing.Point(214, 224);
            this.bsupprTel.Name = "bsupprTel";
            this.bsupprTel.Size = new System.Drawing.Size(60, 60);
            this.bsupprTel.TabIndex = 56;
            this.toolTip1.SetToolTip(this.bsupprTel, "Effacer");
            this.bsupprTel.UseVisualStyleBackColor = false;
            this.bsupprTel.Click += new System.EventHandler(this.bsupprTel_Click);
            // 
            // label75
            // 
            this.label75.BackColor = System.Drawing.Color.Transparent;
            this.label75.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label75.Location = new System.Drawing.Point(16, 106);
            this.label75.Name = "label75";
            this.label75.Size = new System.Drawing.Size(156, 29);
            this.label75.TabIndex = 55;
            this.label75.Text = "(Saisir au moins 10 chiffres pour activer le bouton)";
            // 
            // label74
            // 
            this.label74.BackColor = System.Drawing.Color.Transparent;
            this.label74.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label74.Location = new System.Drawing.Point(37, 9);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(199, 24);
            this.label74.TabIndex = 54;
            this.label74.Text = "Téléphones du patient";
            // 
            // EmaskAjoutTel
            // 
            this.EmaskAjoutTel.BackColor = System.Drawing.Color.White;
            this.EmaskAjoutTel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EmaskAjoutTel.ForeColor = System.Drawing.SystemColors.WindowText;
            this.EmaskAjoutTel.Location = new System.Drawing.Point(19, 78);
            this.EmaskAjoutTel.Mask = "################";
            this.EmaskAjoutTel.Name = "EmaskAjoutTel";
            this.EmaskAjoutTel.Size = new System.Drawing.Size(139, 22);
            this.EmaskAjoutTel.TabIndex = 53;
            this.EmaskAjoutTel.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.EmaskAjoutTel.TextChanged += new System.EventHandler(this.EmaskAjoutTel_TextChanged);
            // 
            // label72
            // 
            this.label72.BackColor = System.Drawing.Color.Transparent;
            this.label72.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label72.Location = new System.Drawing.Point(17, 55);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(156, 19);
            this.label72.TabIndex = 52;
            this.label72.Text = "Téléphone à ajouter : +";
            // 
            // label71
            // 
            this.label71.BackColor = System.Drawing.Color.Transparent;
            this.label71.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label71.Location = new System.Drawing.Point(17, 157);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(213, 19);
            this.label71.TabIndex = 51;
            this.label71.Text = "N°s  de téléphone de ce patient:";
            // 
            // bAjouteTel1
            // 
            this.bAjouteTel1.BackColor = System.Drawing.Color.Transparent;
            this.bAjouteTel1.BackgroundImage = global::ImportSosGeneve.Properties.Resources.bajout;
            this.bAjouteTel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bAjouteTel1.FlatAppearance.BorderSize = 0;
            this.bAjouteTel1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bAjouteTel1.ImageIndex = 6;
            this.bAjouteTel1.ImageList = this.imageList1;
            this.bAjouteTel1.Location = new System.Drawing.Point(200, 51);
            this.bAjouteTel1.Name = "bAjouteTel1";
            this.bAjouteTel1.Size = new System.Drawing.Size(60, 60);
            this.bAjouteTel1.TabIndex = 50;
            this.toolTip1.SetToolTip(this.bAjouteTel1, "Ajouter ");
            this.bAjouteTel1.UseVisualStyleBackColor = false;
            this.bAjouteTel1.Click += new System.EventHandler(this.bAjouteTel1_Click);
            // 
            // listViewTel
            // 
            this.listViewTel.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Telephone});
            this.listViewTel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewTel.FullRowSelect = true;
            this.listViewTel.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewTel.HideSelection = false;
            this.listViewTel.Location = new System.Drawing.Point(19, 186);
            this.listViewTel.MultiSelect = false;
            this.listViewTel.Name = "listViewTel";
            this.listViewTel.Size = new System.Drawing.Size(189, 162);
            this.listViewTel.TabIndex = 49;
            this.listViewTel.UseCompatibleStateImageBehavior = false;
            this.listViewTel.View = System.Windows.Forms.View.Details;
            // 
            // Telephone
            // 
            this.Telephone.Width = 180;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(462, 21);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(156, 19);
            this.label10.TabIndex = 2;
            this.label10.Text = "Téléphone de la box : +";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(272, 183);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 24);
            this.label9.TabIndex = 25;
            this.label9.Text = "Porte :";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(24, 183);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(104, 24);
            this.label8.TabIndex = 23;
            this.label8.Text = "Etage :";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(259, 135);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 24);
            this.label7.TabIndex = 16;
            this.label7.Text = "Localité :";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(382, 94);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 20);
            this.label6.TabIndex = 10;
            this.label6.Text = "Sexe :";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(24, 135);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 24);
            this.label5.TabIndex = 14;
            this.label5.Text = "N° Postal :";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(24, 159);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 24);
            this.label4.TabIndex = 19;
            this.label4.Text = "Adresse:";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(24, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 24);
            this.label3.TabIndex = 8;
            this.label3.Text = "Né(e) le :";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(24, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 24);
            this.label2.TabIndex = 5;
            this.label2.Text = "Prénom :";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(24, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nom :";
            // 
            // cbExporter
            // 
            this.cbExporter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbExporter.Location = new System.Drawing.Point(608, 99);
            this.cbExporter.Name = "cbExporter";
            this.cbExporter.Size = new System.Drawing.Size(107, 22);
            this.cbExporter.TabIndex = 18;
            this.cbExporter.Text = "Déjà Exporté";
            // 
            // tbTypeAbonnement
            // 
            this.tbTypeAbonnement.Controls.Add(this.splitContainer3);
            this.tbTypeAbonnement.Location = new System.Drawing.Point(4, 25);
            this.tbTypeAbonnement.Name = "tbTypeAbonnement";
            this.tbTypeAbonnement.Size = new System.Drawing.Size(780, 691);
            this.tbTypeAbonnement.TabIndex = 8;
            this.tbTypeAbonnement.Text = "Type abonnement";
            this.tbTypeAbonnement.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            this.splitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.splitContainer4);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.BackColor = System.Drawing.Color.CadetBlue;
            this.splitContainer3.Panel2.Controls.Add(this.label76);
            this.splitContainer3.Panel2.Controls.Add(this.tBoxNumSerie);
            this.splitContainer3.Panel2.Controls.Add(this.cBoxMotifChangement);
            this.splitContainer3.Panel2.Controls.Add(this.label70);
            this.splitContainer3.Panel2.Controls.Add(this.listViewMat1);
            this.splitContainer3.Panel2.Controls.Add(this.label69);
            this.splitContainer3.Panel2.Controls.Add(this.label68);
            this.splitContainer3.Panel2.Controls.Add(this.bSupprMatos);
            this.splitContainer3.Panel2.Controls.Add(this.tBoxSupprMatos);
            this.splitContainer3.Panel2.Controls.Add(this.bAjoutMat1);
            this.splitContainer3.Panel2.Controls.Add(this.comboBMateriel);
            this.splitContainer3.Panel2.Controls.Add(this.rBTypeBoitier3);
            this.splitContainer3.Panel2.Controls.Add(this.rBTypeBoitier2);
            this.splitContainer3.Panel2.Controls.Add(this.rBTypeBoitier1);
            this.splitContainer3.Size = new System.Drawing.Size(780, 691);
            this.splitContainer3.SplitterDistance = 298;
            this.splitContainer3.TabIndex = 0;
            // 
            // splitContainer4
            // 
            this.splitContainer4.BackColor = System.Drawing.Color.AntiqueWhite;
            this.splitContainer4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.BackColor = System.Drawing.Color.CadetBlue;
            this.splitContainer4.Panel1.Controls.Add(this.gBoxPeriode);
            this.splitContainer4.Panel1.Controls.Add(this.dtDebutFacturation);
            this.splitContainer4.Panel1.Controls.Add(this.label45);
            this.splitContainer4.Panel1.Controls.Add(this.cbOrdre);
            this.splitContainer4.Panel1.Controls.Add(this.checkBoxSansRappelTA);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.BackColor = System.Drawing.Color.CadetBlue;
            this.splitContainer4.Panel2.Controls.Add(this.bActiverFacture);
            this.splitContainer4.Panel2.Controls.Add(this.groupBox5);
            this.splitContainer4.Size = new System.Drawing.Size(298, 691);
            this.splitContainer4.SplitterDistance = 372;
            this.splitContainer4.TabIndex = 0;
            // 
            // gBoxPeriode
            // 
            this.gBoxPeriode.Controls.Add(this.rdPeriodFac4);
            this.gBoxPeriode.Controls.Add(this.rdPeriodFac3);
            this.gBoxPeriode.Controls.Add(this.rdPeriodFac0);
            this.gBoxPeriode.Controls.Add(this.rdPeriodFac2);
            this.gBoxPeriode.Controls.Add(this.rdPeriodFac1);
            this.gBoxPeriode.Location = new System.Drawing.Point(6, 51);
            this.gBoxPeriode.Name = "gBoxPeriode";
            this.gBoxPeriode.Size = new System.Drawing.Size(138, 170);
            this.gBoxPeriode.TabIndex = 33;
            this.gBoxPeriode.TabStop = false;
            this.gBoxPeriode.Text = "Périodicité";
            // 
            // rdPeriodFac4
            // 
            this.rdPeriodFac4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdPeriodFac4.Location = new System.Drawing.Point(16, 79);
            this.rdPeriodFac4.Name = "rdPeriodFac4";
            this.rdPeriodFac4.Size = new System.Drawing.Size(104, 24);
            this.rdPeriodFac4.TabIndex = 32;
            this.rdPeriodFac4.Text = "Semestrielle";
            // 
            // rdPeriodFac3
            // 
            this.rdPeriodFac3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdPeriodFac3.Location = new System.Drawing.Point(16, 130);
            this.rdPeriodFac3.Name = "rdPeriodFac3";
            this.rdPeriodFac3.Size = new System.Drawing.Size(79, 24);
            this.rdPeriodFac3.TabIndex = 29;
            this.rdPeriodFac3.Text = "Bloquée";
            // 
            // rdPeriodFac0
            // 
            this.rdPeriodFac0.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdPeriodFac0.Location = new System.Drawing.Point(16, 25);
            this.rdPeriodFac0.Name = "rdPeriodFac0";
            this.rdPeriodFac0.Size = new System.Drawing.Size(90, 24);
            this.rdPeriodFac0.TabIndex = 31;
            this.rdPeriodFac0.Text = "Mensuelle";
            // 
            // rdPeriodFac2
            // 
            this.rdPeriodFac2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdPeriodFac2.Location = new System.Drawing.Point(16, 104);
            this.rdPeriodFac2.Name = "rdPeriodFac2";
            this.rdPeriodFac2.Size = new System.Drawing.Size(81, 24);
            this.rdPeriodFac2.TabIndex = 28;
            this.rdPeriodFac2.Text = "Annuelle";
            // 
            // rdPeriodFac1
            // 
            this.rdPeriodFac1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdPeriodFac1.Location = new System.Drawing.Point(16, 49);
            this.rdPeriodFac1.Name = "rdPeriodFac1";
            this.rdPeriodFac1.Size = new System.Drawing.Size(100, 24);
            this.rdPeriodFac1.TabIndex = 27;
            this.rdPeriodFac1.Text = "Trimestrielle";
            // 
            // dtDebutFacturation
            // 
            this.dtDebutFacturation.CalendarMonthBackground = System.Drawing.Color.CadetBlue;
            this.dtDebutFacturation.CalendarTitleBackColor = System.Drawing.Color.CadetBlue;
            this.dtDebutFacturation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtDebutFacturation.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtDebutFacturation.Location = new System.Drawing.Point(175, 11);
            this.dtDebutFacturation.Name = "dtDebutFacturation";
            this.dtDebutFacturation.Size = new System.Drawing.Size(112, 22);
            this.dtDebutFacturation.TabIndex = 26;
            // 
            // label45
            // 
            this.label45.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label45.ForeColor = System.Drawing.Color.Black;
            this.label45.Location = new System.Drawing.Point(3, 14);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(169, 22);
            this.label45.TabIndex = 24;
            this.label45.Text = "Date début de facturation :";
            // 
            // cbOrdre
            // 
            this.cbOrdre.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbOrdre.Location = new System.Drawing.Point(15, 254);
            this.cbOrdre.Name = "cbOrdre";
            this.cbOrdre.Size = new System.Drawing.Size(129, 24);
            this.cbOrdre.TabIndex = 25;
            this.cbOrdre.Text = "Ordre permanent";
            // 
            // checkBoxSansRappelTA
            // 
            this.checkBoxSansRappelTA.AutoSize = true;
            this.checkBoxSansRappelTA.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxSansRappelTA.ForeColor = System.Drawing.Color.Maroon;
            this.checkBoxSansRappelTA.Location = new System.Drawing.Point(15, 293);
            this.checkBoxSansRappelTA.Name = "checkBoxSansRappelTA";
            this.checkBoxSansRappelTA.Size = new System.Drawing.Size(122, 20);
            this.checkBoxSansRappelTA.TabIndex = 30;
            this.checkBoxSansRappelTA.Text = "Stop Rappels";
            this.checkBoxSansRappelTA.UseVisualStyleBackColor = true;
            // 
            // bActiverFacture
            // 
            this.bActiverFacture.BackColor = System.Drawing.Color.Transparent;
            this.bActiverFacture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bActiverFacture.FlatAppearance.BorderSize = 0;
            this.bActiverFacture.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bActiverFacture.ImageIndex = 20;
            this.bActiverFacture.ImageList = this.imageList1;
            this.bActiverFacture.Location = new System.Drawing.Point(112, 216);
            this.bActiverFacture.Name = "bActiverFacture";
            this.bActiverFacture.Size = new System.Drawing.Size(60, 60);
            this.bActiverFacture.TabIndex = 45;
            this.toolTip1.SetToolTip(this.bActiverFacture, "Activer la facturation");
            this.bActiverFacture.UseVisualStyleBackColor = false;
            this.bActiverFacture.Click += new System.EventHandler(this.bActiverFacture_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.Transparent;
            this.groupBox5.Controls.Add(this.rdOnglet3Type5);
            this.groupBox5.Controls.Add(this.rdOnglet3Type4);
            this.groupBox5.Controls.Add(this.rdOnglet3Type3);
            this.groupBox5.Controls.Add(this.rdOnglet3Type2);
            this.groupBox5.Controls.Add(this.rdOnglet3Type1);
            this.groupBox5.Location = new System.Drawing.Point(6, 9);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(187, 173);
            this.groupBox5.TabIndex = 20;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Appareil";
            // 
            // rdOnglet3Type5
            // 
            this.rdOnglet3Type5.Location = new System.Drawing.Point(16, 105);
            this.rdOnglet3Type5.Name = "rdOnglet3Type5";
            this.rdOnglet3Type5.Size = new System.Drawing.Size(122, 24);
            this.rdOnglet3Type5.TabIndex = 4;
            this.rdOnglet3Type5.Text = "Autre";
            this.rdOnglet3Type5.CheckedChanged += new System.EventHandler(this.rdOnglet3Type5_CheckedChanged);
            // 
            // rdOnglet3Type4
            // 
            this.rdOnglet3Type4.Location = new System.Drawing.Point(16, 75);
            this.rdOnglet3Type4.Name = "rdOnglet3Type4";
            this.rdOnglet3Type4.Size = new System.Drawing.Size(144, 24);
            this.rdOnglet3Type4.TabIndex = 3;
            this.rdOnglet3Type4.Text = "SOS (Médicalerte)";
            this.rdOnglet3Type4.CheckedChanged += new System.EventHandler(this.rdOnglet3Type4_CheckedChanged_1);
            // 
            // rdOnglet3Type3
            // 
            this.rdOnglet3Type3.Location = new System.Drawing.Point(16, 135);
            this.rdOnglet3Type3.Name = "rdOnglet3Type3";
            this.rdOnglet3Type3.Size = new System.Drawing.Size(144, 24);
            this.rdOnglet3Type3.TabIndex = 2;
            this.rdOnglet3Type3.Text = "Aucun équipement";
            this.rdOnglet3Type3.CheckedChanged += new System.EventHandler(this.rdOnglet3Type3_CheckedChanged);
            // 
            // rdOnglet3Type2
            // 
            this.rdOnglet3Type2.Location = new System.Drawing.Point(16, 46);
            this.rdOnglet3Type2.Name = "rdOnglet3Type2";
            this.rdOnglet3Type2.Size = new System.Drawing.Size(122, 24);
            this.rdOnglet3Type2.TabIndex = 1;
            this.rdOnglet3Type2.Text = "Swisscom";
            this.rdOnglet3Type2.Click += new System.EventHandler(this.rdOnglet3Type2_Click);
            // 
            // rdOnglet3Type1
            // 
            this.rdOnglet3Type1.Location = new System.Drawing.Point(16, 19);
            this.rdOnglet3Type1.Name = "rdOnglet3Type1";
            this.rdOnglet3Type1.Size = new System.Drawing.Size(144, 24);
            this.rdOnglet3Type1.TabIndex = 0;
            this.rdOnglet3Type1.Text = "IMAD (TéléAlarme)";
            // 
            // label76
            // 
            this.label76.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label76.Location = new System.Drawing.Point(13, 248);
            this.label76.Name = "label76";
            this.label76.Size = new System.Drawing.Size(81, 20);
            this.label76.TabIndex = 52;
            this.label76.Text = "N° de série :";
            // 
            // tBoxNumSerie
            // 
            this.tBoxNumSerie.Enabled = false;
            this.tBoxNumSerie.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tBoxNumSerie.Location = new System.Drawing.Point(100, 245);
            this.tBoxNumSerie.Name = "tBoxNumSerie";
            this.tBoxNumSerie.Size = new System.Drawing.Size(160, 22);
            this.tBoxNumSerie.TabIndex = 51;
            // 
            // cBoxMotifChangement
            // 
            this.cBoxMotifChangement.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBoxMotifChangement.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cBoxMotifChangement.Items.AddRange(new object[] {
            "Boitier HS",
            "Changement de version",
            "Réaffectation de boitier"});
            this.cBoxMotifChangement.Location = new System.Drawing.Point(12, 117);
            this.cBoxMotifChangement.Name = "cBoxMotifChangement";
            this.cBoxMotifChangement.Size = new System.Drawing.Size(248, 24);
            this.cBoxMotifChangement.TabIndex = 50;
            this.cBoxMotifChangement.SelectedIndexChanged += new System.EventHandler(this.cBoxMotifChangement_SelectedIndexChanged);
            // 
            // label70
            // 
            this.label70.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label70.Location = new System.Drawing.Point(13, 95);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(217, 20);
            this.label70.TabIndex = 49;
            this.label70.Text = "Motif de changement de boitier:";
            // 
            // listViewMat1
            // 
            this.listViewMat1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader23,
            this.columnHeader24});
            this.listViewMat1.FullRowSelect = true;
            this.listViewMat1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewMat1.HideSelection = false;
            this.listViewMat1.Location = new System.Drawing.Point(13, 288);
            this.listViewMat1.Name = "listViewMat1";
            this.listViewMat1.Size = new System.Drawing.Size(440, 239);
            this.listViewMat1.TabIndex = 48;
            this.listViewMat1.UseCompatibleStateImageBehavior = false;
            this.listViewMat1.View = System.Windows.Forms.View.Details;
            this.listViewMat1.DoubleClick += new System.EventHandler(this.listViewMat1_DoubleClick);
            // 
            // columnHeader23
            // 
            this.columnHeader23.Width = 180;
            // 
            // columnHeader24
            // 
            this.columnHeader24.Width = 100;
            // 
            // label69
            // 
            this.label69.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label69.Location = new System.Drawing.Point(13, 558);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(158, 20);
            this.label69.TabIndex = 47;
            this.label69.Text = "Supprimer des modules :";
            // 
            // label68
            // 
            this.label68.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label68.Location = new System.Drawing.Point(13, 184);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(158, 20);
            this.label68.TabIndex = 46;
            this.label68.Text = "Ajouter des modules :";
            // 
            // bSupprMatos
            // 
            this.bSupprMatos.BackColor = System.Drawing.Color.Transparent;
            this.bSupprMatos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bSupprMatos.FlatAppearance.BorderSize = 0;
            this.bSupprMatos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bSupprMatos.ImageIndex = 0;
            this.bSupprMatos.ImageList = this.imageList1;
            this.bSupprMatos.Location = new System.Drawing.Point(350, 562);
            this.bSupprMatos.Name = "bSupprMatos";
            this.bSupprMatos.Size = new System.Drawing.Size(60, 60);
            this.bSupprMatos.TabIndex = 44;
            this.toolTip1.SetToolTip(this.bSupprMatos, "Effacer");
            this.bSupprMatos.UseVisualStyleBackColor = false;
            this.bSupprMatos.Click += new System.EventHandler(this.bSupprMatos_Click);
            // 
            // tBoxSupprMatos
            // 
            this.tBoxSupprMatos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tBoxSupprMatos.Location = new System.Drawing.Point(12, 581);
            this.tBoxSupprMatos.Name = "tBoxSupprMatos";
            this.tBoxSupprMatos.Size = new System.Drawing.Size(302, 22);
            this.tBoxSupprMatos.TabIndex = 43;
            // 
            // bAjoutMat1
            // 
            this.bAjoutMat1.BackColor = System.Drawing.Color.Transparent;
            this.bAjoutMat1.BackgroundImage = global::ImportSosGeneve.Properties.Resources.bajout;
            this.bAjoutMat1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bAjoutMat1.FlatAppearance.BorderSize = 0;
            this.bAjoutMat1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bAjoutMat1.ImageIndex = 6;
            this.bAjoutMat1.ImageList = this.imageList1;
            this.bAjoutMat1.Location = new System.Drawing.Point(306, 188);
            this.bAjoutMat1.Name = "bAjoutMat1";
            this.bAjoutMat1.Size = new System.Drawing.Size(60, 60);
            this.bAjoutMat1.TabIndex = 36;
            this.toolTip1.SetToolTip(this.bAjoutMat1, "Nouvelle personne");
            this.bAjoutMat1.UseVisualStyleBackColor = false;
            this.bAjoutMat1.Click += new System.EventHandler(this.bAjoutMat1_Click);
            // 
            // comboBMateriel
            // 
            this.comboBMateriel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBMateriel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBMateriel.Items.AddRange(new object[] {
            "Médaillon radio M4",
            "Luna Porte radio",
            "Luna Présence radio",
            "Detecteur Incendie",
            "Tirette d\'appel",
            "Luna Led",
            "Luna Porte Bluetooth",
            "Luna Présence Bluetooth",
            "Luna Média Wifi",
            "Luna Audio Wifi",
            "Detecteur chute Vibby"});
            this.comboBMateriel.Location = new System.Drawing.Point(12, 207);
            this.comboBMateriel.Name = "comboBMateriel";
            this.comboBMateriel.Size = new System.Drawing.Size(248, 24);
            this.comboBMateriel.TabIndex = 7;
            this.comboBMateriel.SelectedValueChanged += new System.EventHandler(this.comboBMateriel_SelectedValueChanged);
            // 
            // rBTypeBoitier3
            // 
            this.rBTypeBoitier3.Location = new System.Drawing.Point(320, 29);
            this.rBTypeBoitier3.Name = "rBTypeBoitier3";
            this.rBTypeBoitier3.Size = new System.Drawing.Size(90, 24);
            this.rBTypeBoitier3.TabIndex = 6;
            this.rBTypeBoitier3.Text = "LUNA 4";
            this.rBTypeBoitier3.CheckedChanged += new System.EventHandler(this.rBTypeBoitier3_CheckedChanged);
            this.rBTypeBoitier3.Click += new System.EventHandler(this.rBTypeBoitier3_Click);
            // 
            // rBTypeBoitier2
            // 
            this.rBTypeBoitier2.Location = new System.Drawing.Point(170, 29);
            this.rBTypeBoitier2.Name = "rBTypeBoitier2";
            this.rBTypeBoitier2.Size = new System.Drawing.Size(90, 24);
            this.rBTypeBoitier2.TabIndex = 5;
            this.rBTypeBoitier2.Text = "LUNA 3G";
            this.rBTypeBoitier2.CheckedChanged += new System.EventHandler(this.rBTypeBoitier2_CheckedChanged);
            this.rBTypeBoitier2.Click += new System.EventHandler(this.rBTypeBoitier2_Click);
            // 
            // rBTypeBoitier1
            // 
            this.rBTypeBoitier1.Location = new System.Drawing.Point(25, 29);
            this.rBTypeBoitier1.Name = "rBTypeBoitier1";
            this.rBTypeBoitier1.Size = new System.Drawing.Size(107, 24);
            this.rBTypeBoitier1.TabIndex = 4;
            this.rBTypeBoitier1.Text = "LUNA 3G SL";
            this.rBTypeBoitier1.CheckedChanged += new System.EventHandler(this.rBTypeBoitier1_CheckedChanged);
            this.rBTypeBoitier1.Click += new System.EventHandler(this.rBTypeBoitier1_Click);
            // 
            // tbContacts
            // 
            this.tbContacts.BackColor = System.Drawing.Color.CadetBlue;
            this.tbContacts.Controls.Add(this.lwUrgence);
            this.tbContacts.Controls.Add(this.label55);
            this.tbContacts.Controls.Add(this.groupBox3);
            this.tbContacts.Controls.Add(this.groupBox2);
            this.tbContacts.Controls.Add(this.lwMemoire);
            this.tbContacts.Controls.Add(this.label19);
            this.tbContacts.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbContacts.Location = new System.Drawing.Point(4, 25);
            this.tbContacts.Name = "tbContacts";
            this.tbContacts.Size = new System.Drawing.Size(780, 691);
            this.tbContacts.TabIndex = 1;
            this.tbContacts.Text = "Contacts";
            // 
            // lwUrgence
            // 
            this.lwUrgence.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.lwUrgence.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader13});
            this.lwUrgence.FullRowSelect = true;
            this.lwUrgence.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lwUrgence.HideSelection = false;
            this.lwUrgence.Location = new System.Drawing.Point(396, 32);
            this.lwUrgence.Name = "lwUrgence";
            this.lwUrgence.Size = new System.Drawing.Size(367, 115);
            this.lwUrgence.TabIndex = 3;
            this.lwUrgence.UseCompatibleStateImageBehavior = false;
            this.lwUrgence.View = System.Windows.Forms.View.Details;
            this.lwUrgence.Click += new System.EventHandler(this.lwUrgence_Click);
            // 
            // columnHeader11
            // 
            this.columnHeader11.Width = 90;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Width = 180;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Width = 100;
            // 
            // label55
            // 
            this.label55.BackColor = System.Drawing.Color.Transparent;
            this.label55.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label55.ForeColor = System.Drawing.Color.Red;
            this.label55.Location = new System.Drawing.Point(489, 8);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(164, 24);
            this.label55.TabIndex = 1;
            this.label55.Text = "En cas d\'urgence :";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.groupBox3.Controls.Add(this.bSupprimer2);
            this.groupBox3.Controls.Add(this.bModifierEn2);
            this.groupBox3.Controls.Add(this.bAnnuler2);
            this.groupBox3.Controls.Add(this.bNouveau2);
            this.groupBox3.Controls.Add(this.bValider2);
            this.groupBox3.Controls.Add(this.txtOnglet2Tel2ter);
            this.groupBox3.Controls.Add(this.txtOnglet2Tel2bis);
            this.groupBox3.Controls.Add(this.txtOnglet2Tel2);
            this.groupBox3.Controls.Add(this.label66);
            this.groupBox3.Controls.Add(this.label65);
            this.groupBox3.Controls.Add(this.label64);
            this.groupBox3.Controls.Add(this.label63);
            this.groupBox3.Controls.Add(this.label62);
            this.groupBox3.Controls.Add(this.label53);
            this.groupBox3.Controls.Add(this.label54);
            this.groupBox3.Controls.Add(this.txtOnglet2Localite2);
            this.groupBox3.Controls.Add(this.txtOnglet2Np2);
            this.groupBox3.Controls.Add(this.txtOnglet2NRue2);
            this.groupBox3.Controls.Add(this.txtOnglet2PreNom2);
            this.groupBox3.Controls.Add(this.txtOnglet2Adresse2);
            this.groupBox3.Controls.Add(this.txtOnglet2Nom2);
            this.groupBox3.Controls.Add(this.cbOnglet2Lien2);
            this.groupBox3.Controls.Add(this.label25);
            this.groupBox3.Controls.Add(this.label26);
            this.groupBox3.Controls.Add(this.label27);
            this.groupBox3.Controls.Add(this.label39);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.Red;
            this.groupBox3.Location = new System.Drawing.Point(390, 170);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(373, 506);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Personnes à prévenir en cas d\'urgence";
            // 
            // bSupprimer2
            // 
            this.bSupprimer2.BackColor = System.Drawing.Color.Transparent;
            this.bSupprimer2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bSupprimer2.FlatAppearance.BorderSize = 0;
            this.bSupprimer2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bSupprimer2.ImageIndex = 0;
            this.bSupprimer2.ImageList = this.imageList1;
            this.bSupprimer2.Location = new System.Drawing.Point(275, 433);
            this.bSupprimer2.Name = "bSupprimer2";
            this.bSupprimer2.Size = new System.Drawing.Size(60, 60);
            this.bSupprimer2.TabIndex = 42;
            this.toolTip1.SetToolTip(this.bSupprimer2, "Effacer");
            this.bSupprimer2.UseVisualStyleBackColor = false;
            this.bSupprimer2.Click += new System.EventHandler(this.bSupprimer2_Click);
            // 
            // bModifierEn2
            // 
            this.bModifierEn2.BackColor = System.Drawing.Color.Transparent;
            this.bModifierEn2.BackgroundImage = global::ImportSosGeneve.Properties.Resources.boutonsModifier;
            this.bModifierEn2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bModifierEn2.FlatAppearance.BorderSize = 0;
            this.bModifierEn2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bModifierEn2.ImageIndex = 10;
            this.bModifierEn2.ImageList = this.imageList1;
            this.bModifierEn2.Location = new System.Drawing.Point(156, 436);
            this.bModifierEn2.Name = "bModifierEn2";
            this.bModifierEn2.Size = new System.Drawing.Size(60, 60);
            this.bModifierEn2.TabIndex = 40;
            this.toolTip1.SetToolTip(this.bModifierEn2, "Modifier en...");
            this.bModifierEn2.UseVisualStyleBackColor = false;
            this.bModifierEn2.Click += new System.EventHandler(this.bModifierEn2_Click);
            // 
            // bAnnuler2
            // 
            this.bAnnuler2.BackColor = System.Drawing.Color.Transparent;
            this.bAnnuler2.BackgroundImage = global::ImportSosGeneve.Properties.Resources.bondCancel;
            this.bAnnuler2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bAnnuler2.FlatAppearance.BorderSize = 0;
            this.bAnnuler2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bAnnuler2.ImageIndex = 0;
            this.bAnnuler2.Location = new System.Drawing.Point(156, 435);
            this.bAnnuler2.Name = "bAnnuler2";
            this.bAnnuler2.Size = new System.Drawing.Size(60, 60);
            this.bAnnuler2.TabIndex = 41;
            this.toolTip1.SetToolTip(this.bAnnuler2, "Annuler");
            this.bAnnuler2.UseVisualStyleBackColor = false;
            this.bAnnuler2.Click += new System.EventHandler(this.bAnnuler2_Click);
            // 
            // bNouveau2
            // 
            this.bNouveau2.BackColor = System.Drawing.Color.Transparent;
            this.bNouveau2.BackgroundImage = global::ImportSosGeneve.Properties.Resources.bajout;
            this.bNouveau2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bNouveau2.FlatAppearance.BorderSize = 0;
            this.bNouveau2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bNouveau2.ImageIndex = 6;
            this.bNouveau2.ImageList = this.imageList1;
            this.bNouveau2.Location = new System.Drawing.Point(38, 435);
            this.bNouveau2.Name = "bNouveau2";
            this.bNouveau2.Size = new System.Drawing.Size(60, 60);
            this.bNouveau2.TabIndex = 36;
            this.toolTip1.SetToolTip(this.bNouveau2, "Nouvelle personne en cas d\'urgence");
            this.bNouveau2.UseVisualStyleBackColor = false;
            this.bNouveau2.Click += new System.EventHandler(this.bNouveau2_Click);
            // 
            // bValider2
            // 
            this.bValider2.BackColor = System.Drawing.Color.Transparent;
            this.bValider2.BackgroundImage = global::ImportSosGeneve.Properties.Resources.brondValider;
            this.bValider2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bValider2.FlatAppearance.BorderSize = 0;
            this.bValider2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bValider2.ImageIndex = 0;
            this.bValider2.Location = new System.Drawing.Point(38, 436);
            this.bValider2.Name = "bValider2";
            this.bValider2.Size = new System.Drawing.Size(60, 60);
            this.bValider2.TabIndex = 37;
            this.toolTip1.SetToolTip(this.bValider2, "Valider");
            this.bValider2.UseVisualStyleBackColor = false;
            this.bValider2.Click += new System.EventHandler(this.bValider2_Click);
            // 
            // txtOnglet2Tel2ter
            // 
            this.txtOnglet2Tel2ter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOnglet2Tel2ter.Location = new System.Drawing.Point(126, 407);
            this.txtOnglet2Tel2ter.Mask = "+00-00-000-00-00";
            this.txtOnglet2Tel2ter.Name = "txtOnglet2Tel2ter";
            this.txtOnglet2Tel2ter.Size = new System.Drawing.Size(124, 22);
            this.txtOnglet2Tel2ter.TabIndex = 27;
            this.txtOnglet2Tel2ter.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            // 
            // txtOnglet2Tel2bis
            // 
            this.txtOnglet2Tel2bis.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOnglet2Tel2bis.Location = new System.Drawing.Point(126, 375);
            this.txtOnglet2Tel2bis.Mask = "+00-00-000-00-00";
            this.txtOnglet2Tel2bis.Name = "txtOnglet2Tel2bis";
            this.txtOnglet2Tel2bis.Size = new System.Drawing.Size(124, 22);
            this.txtOnglet2Tel2bis.TabIndex = 26;
            this.txtOnglet2Tel2bis.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            // 
            // txtOnglet2Tel2
            // 
            this.txtOnglet2Tel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOnglet2Tel2.Location = new System.Drawing.Point(126, 343);
            this.txtOnglet2Tel2.Mask = "+00-00-000-00-00";
            this.txtOnglet2Tel2.Name = "txtOnglet2Tel2";
            this.txtOnglet2Tel2.Size = new System.Drawing.Size(124, 22);
            this.txtOnglet2Tel2.TabIndex = 24;
            this.txtOnglet2Tel2.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            // 
            // label66
            // 
            this.label66.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label66.Location = new System.Drawing.Point(8, 128);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(63, 20);
            this.label66.TabIndex = 25;
            this.label66.Text = "Prénom :";
            // 
            // label65
            // 
            this.label65.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label65.Location = new System.Drawing.Point(15, 203);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(33, 20);
            this.label65.TabIndex = 24;
            this.label65.Text = "N° :";
            // 
            // label64
            // 
            this.label64.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label64.Location = new System.Drawing.Point(8, 237);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(40, 20);
            this.label64.TabIndex = 23;
            this.label64.Text = "Rue :";
            // 
            // label63
            // 
            this.label63.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label63.Location = new System.Drawing.Point(16, 283);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(32, 20);
            this.label63.TabIndex = 22;
            this.label63.Text = "CP :";
            // 
            // label62
            // 
            this.label62.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label62.Location = new System.Drawing.Point(11, 310);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(46, 20);
            this.label62.TabIndex = 21;
            this.label62.Text = "Ville :";
            // 
            // label53
            // 
            this.label53.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label53.ForeColor = System.Drawing.Color.Red;
            this.label53.Location = new System.Drawing.Point(24, 413);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(103, 20);
            this.label53.TabIndex = 14;
            this.label53.Text = "Téléphone 3 :";
            // 
            // label54
            // 
            this.label54.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label54.ForeColor = System.Drawing.Color.Red;
            this.label54.Location = new System.Drawing.Point(24, 381);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(103, 21);
            this.label54.TabIndex = 12;
            this.label54.Text = "Téléphone 2 :";
            // 
            // txtOnglet2Localite2
            // 
            this.txtOnglet2Localite2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOnglet2Localite2.ForeColor = System.Drawing.Color.Black;
            this.txtOnglet2Localite2.Location = new System.Drawing.Point(63, 307);
            this.txtOnglet2Localite2.Name = "txtOnglet2Localite2";
            this.txtOnglet2Localite2.Size = new System.Drawing.Size(249, 22);
            this.txtOnglet2Localite2.TabIndex = 9;
            this.txtOnglet2Localite2.Click += new System.EventHandler(this.txtOnglet2Nom2_Click);
            // 
            // txtOnglet2Np2
            // 
            this.txtOnglet2Np2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOnglet2Np2.ForeColor = System.Drawing.Color.Black;
            this.txtOnglet2Np2.Location = new System.Drawing.Point(63, 280);
            this.txtOnglet2Np2.Multiline = true;
            this.txtOnglet2Np2.Name = "txtOnglet2Np2";
            this.txtOnglet2Np2.Size = new System.Drawing.Size(76, 21);
            this.txtOnglet2Np2.TabIndex = 8;
            this.txtOnglet2Np2.Click += new System.EventHandler(this.txtOnglet2Nom2_Click);
            // 
            // txtOnglet2NRue2
            // 
            this.txtOnglet2NRue2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOnglet2NRue2.ForeColor = System.Drawing.Color.Black;
            this.txtOnglet2NRue2.Location = new System.Drawing.Point(63, 197);
            this.txtOnglet2NRue2.Multiline = true;
            this.txtOnglet2NRue2.Name = "txtOnglet2NRue2";
            this.txtOnglet2NRue2.Size = new System.Drawing.Size(52, 21);
            this.txtOnglet2NRue2.TabIndex = 6;
            this.txtOnglet2NRue2.Click += new System.EventHandler(this.txtOnglet2Nom2_Click);
            // 
            // txtOnglet2PreNom2
            // 
            this.txtOnglet2PreNom2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOnglet2PreNom2.ForeColor = System.Drawing.Color.Black;
            this.txtOnglet2PreNom2.Location = new System.Drawing.Point(69, 125);
            this.txtOnglet2PreNom2.Name = "txtOnglet2PreNom2";
            this.txtOnglet2PreNom2.Size = new System.Drawing.Size(285, 22);
            this.txtOnglet2PreNom2.TabIndex = 4;
            this.txtOnglet2PreNom2.Click += new System.EventHandler(this.txtOnglet2Nom2_Click);
            // 
            // txtOnglet2Adresse2
            // 
            this.txtOnglet2Adresse2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOnglet2Adresse2.ForeColor = System.Drawing.Color.Black;
            this.txtOnglet2Adresse2.Location = new System.Drawing.Point(63, 224);
            this.txtOnglet2Adresse2.Multiline = true;
            this.txtOnglet2Adresse2.Name = "txtOnglet2Adresse2";
            this.txtOnglet2Adresse2.Size = new System.Drawing.Size(300, 51);
            this.txtOnglet2Adresse2.TabIndex = 7;
            this.txtOnglet2Adresse2.Click += new System.EventHandler(this.txtOnglet2Nom2_Click);
            // 
            // txtOnglet2Nom2
            // 
            this.txtOnglet2Nom2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOnglet2Nom2.ForeColor = System.Drawing.Color.Black;
            this.txtOnglet2Nom2.Location = new System.Drawing.Point(69, 88);
            this.txtOnglet2Nom2.Name = "txtOnglet2Nom2";
            this.txtOnglet2Nom2.Size = new System.Drawing.Size(285, 22);
            this.txtOnglet2Nom2.TabIndex = 3;
            this.txtOnglet2Nom2.Click += new System.EventHandler(this.txtOnglet2Nom2_Click);
            // 
            // cbOnglet2Lien2
            // 
            this.cbOnglet2Lien2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOnglet2Lien2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbOnglet2Lien2.ForeColor = System.Drawing.Color.Black;
            this.cbOnglet2Lien2.Items.AddRange(new object[] {
            "Fils",
            "Fille",
            "Parenté",
            "Voisin",
            "Ami",
            "Autre"});
            this.cbOnglet2Lien2.Location = new System.Drawing.Point(69, 48);
            this.cbOnglet2Lien2.Name = "cbOnglet2Lien2";
            this.cbOnglet2Lien2.Size = new System.Drawing.Size(112, 24);
            this.cbOnglet2Lien2.TabIndex = 1;
            // 
            // label25
            // 
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.ForeColor = System.Drawing.Color.Red;
            this.label25.Location = new System.Drawing.Point(24, 349);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(103, 21);
            this.label25.TabIndex = 10;
            this.label25.Text = "Téléphone 1 :";
            // 
            // label26
            // 
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.ForeColor = System.Drawing.Color.Red;
            this.label26.Location = new System.Drawing.Point(124, 165);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(163, 19);
            this.label26.TabIndex = 5;
            this.label26.Text = "Adresse complète :";
            // 
            // label27
            // 
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.Color.Red;
            this.label27.Location = new System.Drawing.Point(16, 91);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(48, 18);
            this.label27.TabIndex = 2;
            this.label27.Text = "Nom :";
            // 
            // label39
            // 
            this.label39.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.ForeColor = System.Drawing.Color.Red;
            this.label39.Location = new System.Drawing.Point(16, 48);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(48, 24);
            this.label39.TabIndex = 0;
            this.label39.Text = "Lien :";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.groupBox2.Controls.Add(this.bNouveau1);
            this.groupBox2.Controls.Add(this.bModifierEn1);
            this.groupBox2.Controls.Add(this.bSupprimer1);
            this.groupBox2.Controls.Add(this.bAnnuler1);
            this.groupBox2.Controls.Add(this.bValider1);
            this.groupBox2.Controls.Add(this.txtOnglet2Tel1ter);
            this.groupBox2.Controls.Add(this.txtOnglet2Tel1bis);
            this.groupBox2.Controls.Add(this.txtOnglet2Tel1);
            this.groupBox2.Controls.Add(this.label61);
            this.groupBox2.Controls.Add(this.label60);
            this.groupBox2.Controls.Add(this.label59);
            this.groupBox2.Controls.Add(this.label24);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label52);
            this.groupBox2.Controls.Add(this.txtOnglet2Localite1);
            this.groupBox2.Controls.Add(this.txtOnglet2Np1);
            this.groupBox2.Controls.Add(this.txtOnglet2NRue1);
            this.groupBox2.Controls.Add(this.label51);
            this.groupBox2.Controls.Add(this.txtOnglet2PreNom1);
            this.groupBox2.Controls.Add(this.txtOnglet2Adresse1);
            this.groupBox2.Controls.Add(this.txtOnglet2Nom1);
            this.groupBox2.Controls.Add(this.cbOnglet2Lien1);
            this.groupBox2.Controls.Add(this.label22);
            this.groupBox2.Controls.Add(this.label21);
            this.groupBox2.Controls.Add(this.label20);
            this.groupBox2.Controls.Add(this.label23);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(5, 170);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(379, 506);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Personnes mémorisées dans l\'appareil:";
            // 
            // bNouveau1
            // 
            this.bNouveau1.BackColor = System.Drawing.Color.Transparent;
            this.bNouveau1.BackgroundImage = global::ImportSosGeneve.Properties.Resources.bajout;
            this.bNouveau1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bNouveau1.FlatAppearance.BorderSize = 0;
            this.bNouveau1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bNouveau1.ImageIndex = 6;
            this.bNouveau1.ImageList = this.imageList1;
            this.bNouveau1.Location = new System.Drawing.Point(51, 443);
            this.bNouveau1.Name = "bNouveau1";
            this.bNouveau1.Size = new System.Drawing.Size(60, 60);
            this.bNouveau1.TabIndex = 35;
            this.toolTip1.SetToolTip(this.bNouveau1, "Nouvelle personne");
            this.bNouveau1.UseVisualStyleBackColor = false;
            this.bNouveau1.Click += new System.EventHandler(this.bNouveau1_Click);
            // 
            // bModifierEn1
            // 
            this.bModifierEn1.BackColor = System.Drawing.Color.Transparent;
            this.bModifierEn1.BackgroundImage = global::ImportSosGeneve.Properties.Resources.boutonsModifier;
            this.bModifierEn1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bModifierEn1.FlatAppearance.BorderSize = 0;
            this.bModifierEn1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bModifierEn1.ImageIndex = 10;
            this.bModifierEn1.ImageList = this.imageList1;
            this.bModifierEn1.Location = new System.Drawing.Point(166, 441);
            this.bModifierEn1.Name = "bModifierEn1";
            this.bModifierEn1.Size = new System.Drawing.Size(60, 60);
            this.bModifierEn1.TabIndex = 39;
            this.toolTip1.SetToolTip(this.bModifierEn1, "Modifier en...");
            this.bModifierEn1.UseVisualStyleBackColor = false;
            this.bModifierEn1.Click += new System.EventHandler(this.bModifierEn1_Click);
            // 
            // bSupprimer1
            // 
            this.bSupprimer1.BackColor = System.Drawing.Color.Transparent;
            this.bSupprimer1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bSupprimer1.FlatAppearance.BorderSize = 0;
            this.bSupprimer1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bSupprimer1.ImageIndex = 0;
            this.bSupprimer1.ImageList = this.imageList1;
            this.bSupprimer1.Location = new System.Drawing.Point(284, 440);
            this.bSupprimer1.Name = "bSupprimer1";
            this.bSupprimer1.Size = new System.Drawing.Size(60, 60);
            this.bSupprimer1.TabIndex = 38;
            this.toolTip1.SetToolTip(this.bSupprimer1, "Effacer");
            this.bSupprimer1.UseVisualStyleBackColor = false;
            this.bSupprimer1.Click += new System.EventHandler(this.bSupprimer1_Click);
            // 
            // bAnnuler1
            // 
            this.bAnnuler1.BackColor = System.Drawing.Color.Transparent;
            this.bAnnuler1.BackgroundImage = global::ImportSosGeneve.Properties.Resources.bondCancel;
            this.bAnnuler1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bAnnuler1.FlatAppearance.BorderSize = 0;
            this.bAnnuler1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bAnnuler1.ImageIndex = 0;
            this.bAnnuler1.Location = new System.Drawing.Point(166, 441);
            this.bAnnuler1.Name = "bAnnuler1";
            this.bAnnuler1.Size = new System.Drawing.Size(60, 60);
            this.bAnnuler1.TabIndex = 37;
            this.toolTip1.SetToolTip(this.bAnnuler1, "Annuler");
            this.bAnnuler1.UseVisualStyleBackColor = false;
            this.bAnnuler1.Click += new System.EventHandler(this.bAnnuler1_Click);
            // 
            // bValider1
            // 
            this.bValider1.BackColor = System.Drawing.Color.Transparent;
            this.bValider1.BackgroundImage = global::ImportSosGeneve.Properties.Resources.brondValider;
            this.bValider1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bValider1.FlatAppearance.BorderSize = 0;
            this.bValider1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bValider1.ImageIndex = 0;
            this.bValider1.Location = new System.Drawing.Point(51, 443);
            this.bValider1.Name = "bValider1";
            this.bValider1.Size = new System.Drawing.Size(60, 60);
            this.bValider1.TabIndex = 36;
            this.toolTip1.SetToolTip(this.bValider1, "Valider");
            this.bValider1.UseVisualStyleBackColor = false;
            this.bValider1.Click += new System.EventHandler(this.bValider1_Click);
            // 
            // txtOnglet2Tel1ter
            // 
            this.txtOnglet2Tel1ter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOnglet2Tel1ter.Location = new System.Drawing.Point(121, 410);
            this.txtOnglet2Tel1ter.Mask = "+00-00-000-00-00";
            this.txtOnglet2Tel1ter.Name = "txtOnglet2Tel1ter";
            this.txtOnglet2Tel1ter.Size = new System.Drawing.Size(130, 22);
            this.txtOnglet2Tel1ter.TabIndex = 23;
            this.txtOnglet2Tel1ter.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            // 
            // txtOnglet2Tel1bis
            // 
            this.txtOnglet2Tel1bis.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOnglet2Tel1bis.Location = new System.Drawing.Point(121, 378);
            this.txtOnglet2Tel1bis.Mask = "+00-00-000-00-00";
            this.txtOnglet2Tel1bis.Name = "txtOnglet2Tel1bis";
            this.txtOnglet2Tel1bis.Size = new System.Drawing.Size(131, 22);
            this.txtOnglet2Tel1bis.TabIndex = 22;
            this.txtOnglet2Tel1bis.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            // 
            // txtOnglet2Tel1
            // 
            this.txtOnglet2Tel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOnglet2Tel1.Location = new System.Drawing.Point(119, 348);
            this.txtOnglet2Tel1.Mask = "+00-00-000-00-00";
            this.txtOnglet2Tel1.Name = "txtOnglet2Tel1";
            this.txtOnglet2Tel1.Size = new System.Drawing.Size(130, 22);
            this.txtOnglet2Tel1.TabIndex = 21;
            this.txtOnglet2Tel1.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            // 
            // label61
            // 
            this.label61.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label61.Location = new System.Drawing.Point(-2, 312);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(46, 20);
            this.label61.TabIndex = 20;
            this.label61.Text = "Ville :";
            // 
            // label60
            // 
            this.label60.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label60.Location = new System.Drawing.Point(7, 283);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(39, 20);
            this.label60.TabIndex = 19;
            this.label60.Text = "CP :";
            // 
            // label59
            // 
            this.label59.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label59.Location = new System.Drawing.Point(0, 237);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(44, 20);
            this.label59.TabIndex = 18;
            this.label59.Text = "Rue :";
            // 
            // label24
            // 
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(13, 203);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(33, 20);
            this.label24.TabIndex = 17;
            this.label24.Text = "N° :";
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(3, 125);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(66, 20);
            this.label16.TabIndex = 16;
            this.label16.Text = "Prénom :";
            // 
            // label52
            // 
            this.label52.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label52.Location = new System.Drawing.Point(9, 413);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(102, 20);
            this.label52.TabIndex = 14;
            this.label52.Text = "Téléphone 3 :";
            // 
            // txtOnglet2Localite1
            // 
            this.txtOnglet2Localite1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOnglet2Localite1.Location = new System.Drawing.Point(47, 310);
            this.txtOnglet2Localite1.Name = "txtOnglet2Localite1";
            this.txtOnglet2Localite1.Size = new System.Drawing.Size(249, 22);
            this.txtOnglet2Localite1.TabIndex = 9;
            this.txtOnglet2Localite1.Click += new System.EventHandler(this.txtOnglet2Nom1_Click);
            // 
            // txtOnglet2Np1
            // 
            this.txtOnglet2Np1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOnglet2Np1.Location = new System.Drawing.Point(46, 283);
            this.txtOnglet2Np1.Multiline = true;
            this.txtOnglet2Np1.Name = "txtOnglet2Np1";
            this.txtOnglet2Np1.Size = new System.Drawing.Size(76, 21);
            this.txtOnglet2Np1.TabIndex = 8;
            this.txtOnglet2Np1.Click += new System.EventHandler(this.txtOnglet2Nom1_Click);
            // 
            // txtOnglet2NRue1
            // 
            this.txtOnglet2NRue1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOnglet2NRue1.Location = new System.Drawing.Point(46, 199);
            this.txtOnglet2NRue1.Multiline = true;
            this.txtOnglet2NRue1.Name = "txtOnglet2NRue1";
            this.txtOnglet2NRue1.Size = new System.Drawing.Size(56, 21);
            this.txtOnglet2NRue1.TabIndex = 6;
            this.txtOnglet2NRue1.Click += new System.EventHandler(this.txtOnglet2Nom1_Click);
            // 
            // label51
            // 
            this.label51.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label51.Location = new System.Drawing.Point(9, 381);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(102, 21);
            this.label51.TabIndex = 12;
            this.label51.Text = "Téléphone 2 :";
            // 
            // txtOnglet2PreNom1
            // 
            this.txtOnglet2PreNom1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOnglet2PreNom1.Location = new System.Drawing.Point(75, 122);
            this.txtOnglet2PreNom1.Name = "txtOnglet2PreNom1";
            this.txtOnglet2PreNom1.Size = new System.Drawing.Size(288, 22);
            this.txtOnglet2PreNom1.TabIndex = 4;
            this.txtOnglet2PreNom1.Click += new System.EventHandler(this.txtOnglet2Nom1_Click);
            // 
            // txtOnglet2Adresse1
            // 
            this.txtOnglet2Adresse1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOnglet2Adresse1.Location = new System.Drawing.Point(46, 226);
            this.txtOnglet2Adresse1.Multiline = true;
            this.txtOnglet2Adresse1.Name = "txtOnglet2Adresse1";
            this.txtOnglet2Adresse1.Size = new System.Drawing.Size(324, 51);
            this.txtOnglet2Adresse1.TabIndex = 7;
            this.txtOnglet2Adresse1.Click += new System.EventHandler(this.txtOnglet2Nom1_Click);
            // 
            // txtOnglet2Nom1
            // 
            this.txtOnglet2Nom1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOnglet2Nom1.Location = new System.Drawing.Point(76, 89);
            this.txtOnglet2Nom1.Name = "txtOnglet2Nom1";
            this.txtOnglet2Nom1.Size = new System.Drawing.Size(287, 22);
            this.txtOnglet2Nom1.TabIndex = 3;
            this.txtOnglet2Nom1.Click += new System.EventHandler(this.txtOnglet2Nom1_Click);
            // 
            // cbOnglet2Lien1
            // 
            this.cbOnglet2Lien1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOnglet2Lien1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbOnglet2Lien1.Items.AddRange(new object[] {
            "Fils",
            "Fille",
            "Parenté",
            "Voisin",
            "Ami",
            "Autre"});
            this.cbOnglet2Lien1.Location = new System.Drawing.Point(51, 49);
            this.cbOnglet2Lien1.Name = "cbOnglet2Lien1";
            this.cbOnglet2Lien1.Size = new System.Drawing.Size(112, 24);
            this.cbOnglet2Lien1.TabIndex = 1;
            // 
            // label22
            // 
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(143, 165);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(155, 21);
            this.label22.TabIndex = 5;
            this.label22.Text = "Adresse complète :";
            // 
            // label21
            // 
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(7, 91);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(46, 20);
            this.label21.TabIndex = 2;
            this.label21.Text = "Nom :";
            // 
            // label20
            // 
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(7, 51);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(48, 24);
            this.label20.TabIndex = 0;
            this.label20.Text = "Lien :";
            // 
            // label23
            // 
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(9, 351);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(102, 21);
            this.label23.TabIndex = 10;
            this.label23.Text = "Téléphone 1 :";
            // 
            // lwMemoire
            // 
            this.lwMemoire.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lwMemoire.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7});
            this.lwMemoire.FullRowSelect = true;
            this.lwMemoire.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lwMemoire.HideSelection = false;
            this.lwMemoire.Location = new System.Drawing.Point(8, 32);
            this.lwMemoire.Name = "lwMemoire";
            this.lwMemoire.Size = new System.Drawing.Size(376, 115);
            this.lwMemoire.TabIndex = 2;
            this.lwMemoire.UseCompatibleStateImageBehavior = false;
            this.lwMemoire.View = System.Windows.Forms.View.Details;
            this.lwMemoire.Click += new System.EventHandler(this.lwMemoire_Click);
            // 
            // columnHeader5
            // 
            this.columnHeader5.Width = 90;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Width = 180;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Width = 100;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(6, 8);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(410, 24);
            this.label19.TabIndex = 0;
            this.label19.Text = "Liste des personnes mémorisées dans l\'appareil :";
            // 
            // tbCle
            // 
            this.tbCle.BackColor = System.Drawing.Color.CadetBlue;
            this.tbCle.Controls.Add(this.label67);
            this.tbCle.Controls.Add(this.chkClePresente);
            this.tbCle.Controls.Add(this.chkDossierBleu);
            this.tbCle.Controls.Add(this.chkFaxFSASD);
            this.tbCle.Controls.Add(this.txtIdContrat);
            this.tbCle.Controls.Add(this.label42);
            this.tbCle.Controls.Add(this.btnVerifCle);
            this.tbCle.Controls.Add(this.txtCommentaireCle);
            this.tbCle.Controls.Add(this.txtNumCle);
            this.tbCle.Controls.Add(this.label40);
            this.tbCle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbCle.Location = new System.Drawing.Point(4, 25);
            this.tbCle.Name = "tbCle";
            this.tbCle.Size = new System.Drawing.Size(780, 691);
            this.tbCle.TabIndex = 4;
            this.tbCle.Text = "Attribution Clé";
            // 
            // label67
            // 
            this.label67.BackColor = System.Drawing.Color.Transparent;
            this.label67.Location = new System.Drawing.Point(136, 170);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(196, 20);
            this.label67.TabIndex = 9;
            this.label67.Text = "( Mettre 0 si pas IMAD ou SOS)";
            // 
            // chkClePresente
            // 
            this.chkClePresente.BackColor = System.Drawing.Color.Transparent;
            this.chkClePresente.Location = new System.Drawing.Point(21, 268);
            this.chkClePresente.Name = "chkClePresente";
            this.chkClePresente.Size = new System.Drawing.Size(261, 20);
            this.chkClePresente.TabIndex = 8;
            this.chkClePresente.Text = "Clé présente";
            this.chkClePresente.UseVisualStyleBackColor = false;
            // 
            // chkDossierBleu
            // 
            this.chkDossierBleu.BackColor = System.Drawing.Color.Transparent;
            this.chkDossierBleu.Location = new System.Drawing.Point(21, 242);
            this.chkDossierBleu.Name = "chkDossierBleu";
            this.chkDossierBleu.Size = new System.Drawing.Size(260, 20);
            this.chkDossierBleu.TabIndex = 7;
            this.chkDossierBleu.Text = "Dossier bleu";
            this.chkDossierBleu.UseVisualStyleBackColor = false;
            // 
            // chkFaxFSASD
            // 
            this.chkFaxFSASD.BackColor = System.Drawing.Color.Transparent;
            this.chkFaxFSASD.Location = new System.Drawing.Point(21, 217);
            this.chkFaxFSASD.Name = "chkFaxFSASD";
            this.chkFaxFSASD.Size = new System.Drawing.Size(260, 22);
            this.chkFaxFSASD.TabIndex = 6;
            this.chkFaxFSASD.Text = "Fax IMAD";
            this.chkFaxFSASD.UseVisualStyleBackColor = false;
            // 
            // txtIdContrat
            // 
            this.txtIdContrat.Location = new System.Drawing.Point(20, 167);
            this.txtIdContrat.Name = "txtIdContrat";
            this.txtIdContrat.Size = new System.Drawing.Size(110, 22);
            this.txtIdContrat.TabIndex = 5;
            // 
            // label42
            // 
            this.label42.BackColor = System.Drawing.Color.Transparent;
            this.label42.Location = new System.Drawing.Point(18, 144);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(134, 20);
            this.label42.TabIndex = 4;
            this.label42.Text = "N° Contrat (PROM) :";
            // 
            // btnVerifCle
            // 
            this.btnVerifCle.Location = new System.Drawing.Point(209, 21);
            this.btnVerifCle.Name = "btnVerifCle";
            this.btnVerifCle.Size = new System.Drawing.Size(271, 22);
            this.btnVerifCle.TabIndex = 2;
            this.btnVerifCle.Text = "Vérifier que cette clé n\'est pas attribuée.";
            this.btnVerifCle.Click += new System.EventHandler(this.btnVerifCle_Click);
            // 
            // txtCommentaireCle
            // 
            this.txtCommentaireCle.Location = new System.Drawing.Point(20, 58);
            this.txtCommentaireCle.MaxLength = 255;
            this.txtCommentaireCle.Multiline = true;
            this.txtCommentaireCle.Name = "txtCommentaireCle";
            this.txtCommentaireCle.Size = new System.Drawing.Size(460, 67);
            this.txtCommentaireCle.TabIndex = 3;
            // 
            // txtNumCle
            // 
            this.txtNumCle.Location = new System.Drawing.Point(103, 21);
            this.txtNumCle.Name = "txtNumCle";
            this.txtNumCle.Size = new System.Drawing.Size(71, 22);
            this.txtNumCle.TabIndex = 1;
            // 
            // label40
            // 
            this.label40.BackColor = System.Drawing.Color.Transparent;
            this.label40.Location = new System.Drawing.Point(16, 24);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(136, 16);
            this.label40.TabIndex = 0;
            this.label40.Text = "Clé attribuée :";
            // 
            // tbJournal
            // 
            this.tbJournal.BackColor = System.Drawing.Color.CadetBlue;
            this.tbJournal.Controls.Add(this.bSupprLigneJ);
            this.tbJournal.Controls.Add(this.bAjoutNvlLigneJ);
            this.tbJournal.Controls.Add(this.fpOnglet5);
            this.tbJournal.Controls.Add(this.gpJournal);
            this.tbJournal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbJournal.Location = new System.Drawing.Point(4, 25);
            this.tbJournal.Name = "tbJournal";
            this.tbJournal.Size = new System.Drawing.Size(780, 691);
            this.tbJournal.TabIndex = 5;
            this.tbJournal.Text = "Journal";
            // 
            // bSupprLigneJ
            // 
            this.bSupprLigneJ.BackColor = System.Drawing.Color.Transparent;
            this.bSupprLigneJ.BackgroundImage = global::ImportSosGeneve.Properties.Resources.bsupprimer;
            this.bSupprLigneJ.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bSupprLigneJ.FlatAppearance.BorderSize = 0;
            this.bSupprLigneJ.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bSupprLigneJ.ImageIndex = 0;
            this.bSupprLigneJ.ImageList = this.imageList1;
            this.bSupprLigneJ.Location = new System.Drawing.Point(373, 235);
            this.bSupprLigneJ.Name = "bSupprLigneJ";
            this.bSupprLigneJ.Size = new System.Drawing.Size(60, 60);
            this.bSupprLigneJ.TabIndex = 53;
            this.toolTip1.SetToolTip(this.bSupprLigneJ, "Supprimer la ligne du journal");
            this.bSupprLigneJ.UseVisualStyleBackColor = false;
            this.bSupprLigneJ.Click += new System.EventHandler(this.bSupprLigneJ_Click);
            // 
            // bAjoutNvlLigneJ
            // 
            this.bAjoutNvlLigneJ.BackColor = System.Drawing.Color.Transparent;
            this.bAjoutNvlLigneJ.BackgroundImage = global::ImportSosGeneve.Properties.Resources.bajout;
            this.bAjoutNvlLigneJ.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bAjoutNvlLigneJ.FlatAppearance.BorderSize = 0;
            this.bAjoutNvlLigneJ.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bAjoutNvlLigneJ.ImageIndex = 6;
            this.bAjoutNvlLigneJ.ImageList = this.imageList1;
            this.bAjoutNvlLigneJ.Location = new System.Drawing.Point(256, 235);
            this.bAjoutNvlLigneJ.Name = "bAjoutNvlLigneJ";
            this.bAjoutNvlLigneJ.Size = new System.Drawing.Size(60, 60);
            this.bAjoutNvlLigneJ.TabIndex = 52;
            this.toolTip1.SetToolTip(this.bAjoutNvlLigneJ, "Ajouter une nouvelle ligne dans le journal");
            this.bAjoutNvlLigneJ.UseVisualStyleBackColor = false;
            this.bAjoutNvlLigneJ.Click += new System.EventHandler(this.bAjoutNvlLigneJ_Click);
            // 
            // fpOnglet5
            // 
            this.fpOnglet5.AccessibleDescription = "fpOnglet5, Sheet1";
            this.fpOnglet5.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
            this.fpOnglet5.Location = new System.Drawing.Point(8, 16);
            this.fpOnglet5.Name = "fpOnglet5";
            this.fpOnglet5.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpOnglet5_Sheet1});
            this.fpOnglet5.Size = new System.Drawing.Size(746, 213);
            this.fpOnglet5.TabIndex = 0;
            this.fpOnglet5.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.fpOnglet5.MouseUp += new System.Windows.Forms.MouseEventHandler(this.fpOnglet5_MouseUp);
            this.fpOnglet5.SetActiveViewport(0, -1, -1);
            // 
            // fpOnglet5_Sheet1
            // 
            this.fpOnglet5_Sheet1.Reset();
            this.fpOnglet5_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpOnglet5_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            fpOnglet5_Sheet1.ColumnCount = 7;
            fpOnglet5_Sheet1.ColumnHeader.RowCount = 0;
            fpOnglet5_Sheet1.RowCount = 0;
            fpOnglet5_Sheet1.RowHeader.ColumnCount = 0;
            this.fpOnglet5_Sheet1.ActiveColumnIndex = -1;
            this.fpOnglet5_Sheet1.ActiveRowIndex = -1;
            this.fpOnglet5_Sheet1.HorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.None);
            this.fpOnglet5_Sheet1.Models = ((FarPoint.Win.Spread.SheetView.DocumentModels)(resources.GetObject("fpOnglet5_Sheet1.Models")));
            this.fpOnglet5_Sheet1.VerticalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.None);
            // 
            // gpJournal
            // 
            this.gpJournal.BackColor = System.Drawing.Color.Transparent;
            this.gpJournal.Controls.Add(this.bCancelJournal);
            this.gpJournal.Controls.Add(this.bValideJournal);
            this.gpJournal.Controls.Add(this.rdOnglet5Retourcontrat);
            this.gpJournal.Controls.Add(this.txtOnglet5NbCle);
            this.gpJournal.Controls.Add(this.label50);
            this.gpJournal.Controls.Add(this.txtOnglet5Commentaire);
            this.gpJournal.Controls.Add(this.txtOnglet5ICE);
            this.gpJournal.Controls.Add(this.dtOnglet5Le);
            this.gpJournal.Controls.Add(this.txtOnglet5EnvoiA);
            this.gpJournal.Controls.Add(this.txtOnglet5EnvoiDe);
            this.gpJournal.Controls.Add(this.label49);
            this.gpJournal.Controls.Add(this.label48);
            this.gpJournal.Controls.Add(this.label47);
            this.gpJournal.Controls.Add(this.label46);
            this.gpJournal.Controls.Add(this.label15);
            this.gpJournal.Controls.Add(this.rdOnglet5Annulation);
            this.gpJournal.Controls.Add(this.rdOnglet5Dossier);
            this.gpJournal.Controls.Add(this.rdOnglet5Cle);
            this.gpJournal.Location = new System.Drawing.Point(8, 311);
            this.gpJournal.Name = "gpJournal";
            this.gpJournal.Size = new System.Drawing.Size(746, 345);
            this.gpJournal.TabIndex = 3;
            this.gpJournal.TabStop = false;
            // 
            // bCancelJournal
            // 
            this.bCancelJournal.BackColor = System.Drawing.Color.Transparent;
            this.bCancelJournal.BackgroundImage = global::ImportSosGeneve.Properties.Resources.bondCancel;
            this.bCancelJournal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bCancelJournal.FlatAppearance.BorderSize = 0;
            this.bCancelJournal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bCancelJournal.ImageIndex = 4;
            this.bCancelJournal.ImageList = this.imageList1;
            this.bCancelJournal.Location = new System.Drawing.Point(93, 279);
            this.bCancelJournal.Name = "bCancelJournal";
            this.bCancelJournal.Size = new System.Drawing.Size(60, 60);
            this.bCancelJournal.TabIndex = 53;
            this.toolTip1.SetToolTip(this.bCancelJournal, "Annuler");
            this.bCancelJournal.UseVisualStyleBackColor = false;
            this.bCancelJournal.Click += new System.EventHandler(this.bCancelJournal_Click);
            // 
            // bValideJournal
            // 
            this.bValideJournal.BackColor = System.Drawing.Color.Transparent;
            this.bValideJournal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bValideJournal.FlatAppearance.BorderSize = 0;
            this.bValideJournal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bValideJournal.ImageIndex = 2;
            this.bValideJournal.ImageList = this.imageList1;
            this.bValideJournal.Location = new System.Drawing.Point(93, 213);
            this.bValideJournal.Name = "bValideJournal";
            this.bValideJournal.Size = new System.Drawing.Size(60, 60);
            this.bValideJournal.TabIndex = 52;
            this.toolTip1.SetToolTip(this.bValideJournal, "Valider le journal");
            this.bValideJournal.UseVisualStyleBackColor = false;
            this.bValideJournal.Click += new System.EventHandler(this.bValideJournal_Click);
            // 
            // rdOnglet5Retourcontrat
            // 
            this.rdOnglet5Retourcontrat.AutoSize = true;
            this.rdOnglet5Retourcontrat.Location = new System.Drawing.Point(29, 149);
            this.rdOnglet5Retourcontrat.Name = "rdOnglet5Retourcontrat";
            this.rdOnglet5Retourcontrat.Size = new System.Drawing.Size(68, 20);
            this.rdOnglet5Retourcontrat.TabIndex = 19;
            this.rdOnglet5Retourcontrat.Text = "Contrat";
            this.rdOnglet5Retourcontrat.UseVisualStyleBackColor = false;
            // 
            // txtOnglet5NbCle
            // 
            this.txtOnglet5NbCle.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOnglet5NbCle.Location = new System.Drawing.Point(616, 102);
            this.txtOnglet5NbCle.Name = "txtOnglet5NbCle";
            this.txtOnglet5NbCle.Size = new System.Drawing.Size(120, 22);
            this.txtOnglet5NbCle.TabIndex = 11;
            // 
            // label50
            // 
            this.label50.Location = new System.Drawing.Point(469, 107);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(141, 13);
            this.label50.TabIndex = 9;
            this.label50.Text = "Auteur :";
            this.label50.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtOnglet5Commentaire
            // 
            this.txtOnglet5Commentaire.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOnglet5Commentaire.Location = new System.Drawing.Point(212, 223);
            this.txtOnglet5Commentaire.Multiline = true;
            this.txtOnglet5Commentaire.Name = "txtOnglet5Commentaire";
            this.txtOnglet5Commentaire.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOnglet5Commentaire.Size = new System.Drawing.Size(531, 107);
            this.txtOnglet5Commentaire.TabIndex = 14;
            // 
            // txtOnglet5ICE
            // 
            this.txtOnglet5ICE.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOnglet5ICE.Location = new System.Drawing.Point(616, 74);
            this.txtOnglet5ICE.Name = "txtOnglet5ICE";
            this.txtOnglet5ICE.Size = new System.Drawing.Size(120, 22);
            this.txtOnglet5ICE.TabIndex = 4;
            // 
            // dtOnglet5Le
            // 
            this.dtOnglet5Le.CustomFormat = "";
            this.dtOnglet5Le.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtOnglet5Le.Location = new System.Drawing.Point(209, 130);
            this.dtOnglet5Le.Name = "dtOnglet5Le";
            this.dtOnglet5Le.Size = new System.Drawing.Size(138, 22);
            this.dtOnglet5Le.TabIndex = 13;
            // 
            // txtOnglet5EnvoiA
            // 
            this.txtOnglet5EnvoiA.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOnglet5EnvoiA.Location = new System.Drawing.Point(209, 102);
            this.txtOnglet5EnvoiA.Name = "txtOnglet5EnvoiA";
            this.txtOnglet5EnvoiA.Size = new System.Drawing.Size(171, 22);
            this.txtOnglet5EnvoiA.TabIndex = 8;
            // 
            // txtOnglet5EnvoiDe
            // 
            this.txtOnglet5EnvoiDe.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOnglet5EnvoiDe.Location = new System.Drawing.Point(209, 74);
            this.txtOnglet5EnvoiDe.Name = "txtOnglet5EnvoiDe";
            this.txtOnglet5EnvoiDe.Size = new System.Drawing.Size(171, 22);
            this.txtOnglet5EnvoiDe.TabIndex = 2;
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(214, 193);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(94, 16);
            this.label49.TabIndex = 10;
            this.label49.Text = "Commentaire :";
            this.label49.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label48
            // 
            this.label48.Location = new System.Drawing.Point(425, 79);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(185, 19);
            this.label48.TabIndex = 3;
            this.label48.Text = "Identificateur clé opérateur :";
            this.label48.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label47
            // 
            this.label47.Location = new System.Drawing.Point(170, 134);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(33, 18);
            this.label47.TabIndex = 12;
            this.label47.Text = "Le :";
            this.label47.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label46
            // 
            this.label46.Location = new System.Drawing.Point(148, 107);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(55, 13);
            this.label46.TabIndex = 7;
            this.label46.Text = "A :";
            this.label46.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(130, 79);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(73, 19);
            this.label15.TabIndex = 1;
            this.label15.Text = "Envoi de :";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // rdOnglet5Annulation
            // 
            this.rdOnglet5Annulation.AutoSize = true;
            this.rdOnglet5Annulation.Location = new System.Drawing.Point(29, 126);
            this.rdOnglet5Annulation.Name = "rdOnglet5Annulation";
            this.rdOnglet5Annulation.Size = new System.Drawing.Size(88, 20);
            this.rdOnglet5Annulation.TabIndex = 6;
            this.rdOnglet5Annulation.Text = "Annulation";
            this.rdOnglet5Annulation.UseVisualStyleBackColor = false;
            // 
            // rdOnglet5Dossier
            // 
            this.rdOnglet5Dossier.AutoSize = true;
            this.rdOnglet5Dossier.Location = new System.Drawing.Point(29, 102);
            this.rdOnglet5Dossier.Name = "rdOnglet5Dossier";
            this.rdOnglet5Dossier.Size = new System.Drawing.Size(73, 20);
            this.rdOnglet5Dossier.TabIndex = 5;
            this.rdOnglet5Dossier.Text = "Dossier";
            this.rdOnglet5Dossier.UseVisualStyleBackColor = false;
            // 
            // rdOnglet5Cle
            // 
            this.rdOnglet5Cle.AutoSize = true;
            this.rdOnglet5Cle.Location = new System.Drawing.Point(29, 78);
            this.rdOnglet5Cle.Name = "rdOnglet5Cle";
            this.rdOnglet5Cle.Size = new System.Drawing.Size(46, 20);
            this.rdOnglet5Cle.TabIndex = 0;
            this.rdOnglet5Cle.Text = "Clé";
            this.rdOnglet5Cle.UseVisualStyleBackColor = false;
            // 
            // tbFactures
            // 
            this.tbFactures.BackColor = System.Drawing.Color.CadetBlue;
            this.tbFactures.Controls.Add(this.dataGridView2);
            this.tbFactures.Controls.Add(this.dataGridView1);
            this.tbFactures.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbFactures.Location = new System.Drawing.Point(4, 25);
            this.tbFactures.Name = "tbFactures";
            this.tbFactures.Size = new System.Drawing.Size(780, 691);
            this.tbFactures.TabIndex = 6;
            this.tbFactures.Text = "Factures";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.dataGridView2.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.CadetBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView2.Location = new System.Drawing.Point(0, 550);
            this.dataGridView2.MultiSelect = false;
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView2.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.ShowEditingIcon = false;
            this.dataGridView2.Size = new System.Drawing.Size(780, 141);
            this.dataGridView2.TabIndex = 3;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.MidnightBlue;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.CadetBlue;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.ShowEditingIcon = false;
            this.dataGridView1.Size = new System.Drawing.Size(780, 542);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // tbDossierMedical
            // 
            this.tbDossierMedical.BackColor = System.Drawing.Color.CadetBlue;
            this.tbDossierMedical.Controls.Add(this.bSupprMedecin);
            this.tbDossierMedical.Controls.Add(this.bAjoutMedecin);
            this.tbDossierMedical.Controls.Add(this.CB_Sourd);
            this.tbDossierMedical.Controls.Add(this.txtOnglet3Medic);
            this.tbDossierMedical.Controls.Add(this.txtOnglet3Tel);
            this.tbDossierMedical.Controls.Add(this.txtOnglet3Poids);
            this.tbDossierMedical.Controls.Add(this.txtOnglet3Attitudes);
            this.tbDossierMedical.Controls.Add(this.txtOnglet3Pb);
            this.tbDossierMedical.Controls.Add(this.lwMedTTT);
            this.tbDossierMedical.Controls.Add(this.groupBox4);
            this.tbDossierMedical.Controls.Add(this.label34);
            this.tbDossierMedical.Controls.Add(this.label33);
            this.tbDossierMedical.Controls.Add(this.label32);
            this.tbDossierMedical.Controls.Add(this.chkOnglet3Risque);
            this.tbDossierMedical.Controls.Add(this.label31);
            this.tbDossierMedical.Controls.Add(this.label30);
            this.tbDossierMedical.Controls.Add(this.label29);
            this.tbDossierMedical.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDossierMedical.Location = new System.Drawing.Point(4, 25);
            this.tbDossierMedical.Name = "tbDossierMedical";
            this.tbDossierMedical.Size = new System.Drawing.Size(780, 691);
            this.tbDossierMedical.TabIndex = 3;
            this.tbDossierMedical.Text = "Dossier médical";
            // 
            // bSupprMedecin
            // 
            this.bSupprMedecin.BackColor = System.Drawing.Color.Transparent;
            this.bSupprMedecin.BackgroundImage = global::ImportSosGeneve.Properties.Resources.bsupprimer;
            this.bSupprMedecin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bSupprMedecin.FlatAppearance.BorderSize = 0;
            this.bSupprMedecin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bSupprMedecin.ImageIndex = 0;
            this.bSupprMedecin.ImageList = this.imageList1;
            this.bSupprMedecin.Location = new System.Drawing.Point(313, 138);
            this.bSupprMedecin.Name = "bSupprMedecin";
            this.bSupprMedecin.Size = new System.Drawing.Size(60, 60);
            this.bSupprMedecin.TabIndex = 54;
            this.toolTip1.SetToolTip(this.bSupprMedecin, "Supprimer un médecin traitant");
            this.bSupprMedecin.UseVisualStyleBackColor = false;
            this.bSupprMedecin.Click += new System.EventHandler(this.bSupprMedecin_Click);
            // 
            // bAjoutMedecin
            // 
            this.bAjoutMedecin.BackColor = System.Drawing.Color.Transparent;
            this.bAjoutMedecin.BackgroundImage = global::ImportSosGeneve.Properties.Resources.bajout;
            this.bAjoutMedecin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bAjoutMedecin.FlatAppearance.BorderSize = 0;
            this.bAjoutMedecin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bAjoutMedecin.ImageIndex = 6;
            this.bAjoutMedecin.ImageList = this.imageList1;
            this.bAjoutMedecin.Location = new System.Drawing.Point(313, 39);
            this.bAjoutMedecin.Name = "bAjoutMedecin";
            this.bAjoutMedecin.Size = new System.Drawing.Size(60, 60);
            this.bAjoutMedecin.TabIndex = 53;
            this.toolTip1.SetToolTip(this.bAjoutMedecin, "Ajouter un médecin traitant");
            this.bAjoutMedecin.UseVisualStyleBackColor = false;
            this.bAjoutMedecin.Click += new System.EventHandler(this.bAjoutMedecin_Click);
            // 
            // CB_Sourd
            // 
            this.CB_Sourd.Location = new System.Drawing.Point(527, 420);
            this.CB_Sourd.Name = "CB_Sourd";
            this.CB_Sourd.Size = new System.Drawing.Size(157, 24);
            this.CB_Sourd.TabIndex = 24;
            this.CB_Sourd.Text = "Sourd/Malentendant";
            // 
            // txtOnglet3Medic
            // 
            this.txtOnglet3Medic.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOnglet3Medic.Location = new System.Drawing.Point(399, 240);
            this.txtOnglet3Medic.Multiline = true;
            this.txtOnglet3Medic.Name = "txtOnglet3Medic";
            this.txtOnglet3Medic.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOnglet3Medic.Size = new System.Drawing.Size(342, 124);
            this.txtOnglet3Medic.TabIndex = 14;
            // 
            // txtOnglet3Tel
            // 
            this.txtOnglet3Tel.Location = new System.Drawing.Point(115, 204);
            this.txtOnglet3Tel.Name = "txtOnglet3Tel";
            this.txtOnglet3Tel.ReadOnly = true;
            this.txtOnglet3Tel.Size = new System.Drawing.Size(188, 22);
            this.txtOnglet3Tel.TabIndex = 7;
            this.toolTip1.SetToolTip(this.txtOnglet3Tel, "Téléphone du médecin sélectionné");
            // 
            // txtOnglet3Poids
            // 
            this.txtOnglet3Poids.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOnglet3Poids.Location = new System.Drawing.Point(448, 379);
            this.txtOnglet3Poids.Name = "txtOnglet3Poids";
            this.txtOnglet3Poids.Size = new System.Drawing.Size(45, 22);
            this.txtOnglet3Poids.TabIndex = 17;
            // 
            // txtOnglet3Attitudes
            // 
            this.txtOnglet3Attitudes.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOnglet3Attitudes.Location = new System.Drawing.Point(19, 281);
            this.txtOnglet3Attitudes.Multiline = true;
            this.txtOnglet3Attitudes.Name = "txtOnglet3Attitudes";
            this.txtOnglet3Attitudes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOnglet3Attitudes.Size = new System.Drawing.Size(312, 143);
            this.txtOnglet3Attitudes.TabIndex = 15;
            // 
            // txtOnglet3Pb
            // 
            this.txtOnglet3Pb.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOnglet3Pb.Location = new System.Drawing.Point(399, 39);
            this.txtOnglet3Pb.Multiline = true;
            this.txtOnglet3Pb.Name = "txtOnglet3Pb";
            this.txtOnglet3Pb.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOnglet3Pb.Size = new System.Drawing.Size(356, 159);
            this.txtOnglet3Pb.TabIndex = 5;
            // 
            // lwMedTTT
            // 
            this.lwMedTTT.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader8});
            this.lwMedTTT.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lwMedTTT.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lwMedTTT.HideSelection = false;
            this.lwMedTTT.Location = new System.Drawing.Point(8, 39);
            this.lwMedTTT.Name = "lwMedTTT";
            this.lwMedTTT.Size = new System.Drawing.Size(299, 159);
            this.lwMedTTT.TabIndex = 1;
            this.lwMedTTT.UseCompatibleStateImageBehavior = false;
            this.lwMedTTT.View = System.Windows.Forms.View.Details;
            this.lwMedTTT.SelectedIndexChanged += new System.EventHandler(this.lwMedTTT_SelectedIndexChanged);
            this.lwMedTTT.DoubleClick += new System.EventHandler(this.lwMedTTT_DoubleClick);
            // 
            // columnHeader8
            // 
            this.columnHeader8.Width = 290;
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.Transparent;
            this.groupBox4.Controls.Add(this.chkOnglet3AutreServices);
            this.groupBox4.Controls.Add(this.chkOnglet3Fsasd);
            this.groupBox4.Location = new System.Drawing.Point(19, 482);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(250, 118);
            this.groupBox4.TabIndex = 18;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Services à domicile";
            // 
            // chkOnglet3AutreServices
            // 
            this.chkOnglet3AutreServices.Location = new System.Drawing.Point(8, 54);
            this.chkOnglet3AutreServices.Name = "chkOnglet3AutreServices";
            this.chkOnglet3AutreServices.Size = new System.Drawing.Size(133, 24);
            this.chkOnglet3AutreServices.TabIndex = 1;
            this.chkOnglet3AutreServices.Text = "Autres services";
            // 
            // chkOnglet3Fsasd
            // 
            this.chkOnglet3Fsasd.Location = new System.Drawing.Point(8, 24);
            this.chkOnglet3Fsasd.Name = "chkOnglet3Fsasd";
            this.chkOnglet3Fsasd.Size = new System.Drawing.Size(121, 24);
            this.chkOnglet3Fsasd.TabIndex = 0;
            this.chkOnglet3Fsasd.Text = "IMAD";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.BackColor = System.Drawing.Color.Transparent;
            this.label34.Location = new System.Drawing.Point(396, 382);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(49, 16);
            this.label34.TabIndex = 16;
            this.label34.Text = "Poids :";
            // 
            // label33
            // 
            this.label33.BackColor = System.Drawing.Color.Transparent;
            this.label33.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.Location = new System.Drawing.Point(20, 260);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(174, 18);
            this.label33.TabIndex = 10;
            this.label33.Text = "Attitudes préconisées :";
            // 
            // label32
            // 
            this.label32.BackColor = System.Drawing.Color.Transparent;
            this.label32.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(396, 214);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(115, 23);
            this.label32.TabIndex = 9;
            this.label32.Text = "Médicaments :";
            // 
            // chkOnglet3Risque
            // 
            this.chkOnglet3Risque.BackColor = System.Drawing.Color.Transparent;
            this.chkOnglet3Risque.Location = new System.Drawing.Point(527, 385);
            this.chkOnglet3Risque.Name = "chkOnglet3Risque";
            this.chkOnglet3Risque.Size = new System.Drawing.Size(184, 28);
            this.chkOnglet3Risque.TabIndex = 8;
            this.chkOnglet3Risque.Text = "Risques de chutes";
            this.chkOnglet3Risque.UseVisualStyleBackColor = false;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.BackColor = System.Drawing.Color.Transparent;
            this.label31.Location = new System.Drawing.Point(5, 207);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(77, 16);
            this.label31.TabIndex = 6;
            this.label31.Text = "Téléphone:";
            // 
            // label30
            // 
            this.label30.BackColor = System.Drawing.Color.Transparent;
            this.label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.Location = new System.Drawing.Point(485, 17);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(166, 14);
            this.label30.TabIndex = 4;
            this.label30.Text = "Problèmes médicaux :";
            // 
            // label29
            // 
            this.label29.BackColor = System.Drawing.Color.Transparent;
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(70, 17);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(157, 19);
            this.label29.TabIndex = 0;
            this.label29.Text = "Médecins traitants :";
            // 
            // tbBoitier
            // 
            this.tbBoitier.BackColor = System.Drawing.Color.CadetBlue;
            this.tbBoitier.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbBoitier.Location = new System.Drawing.Point(4, 25);
            this.tbBoitier.Name = "tbBoitier";
            this.tbBoitier.Size = new System.Drawing.Size(780, 691);
            this.tbBoitier.TabIndex = 7;
            this.tbBoitier.Text = "Boitier";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.LIdAbonnement);
            this.groupBox6.Controls.Add(this.btFIP);
            this.groupBox6.Controls.Add(this.lblNom);
            this.groupBox6.Controls.Add(this.lblCle);
            this.groupBox6.Controls.Add(this.lblContrat);
            this.groupBox6.Location = new System.Drawing.Point(7, 724);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(780, 45);
            this.groupBox6.TabIndex = 14;
            this.groupBox6.TabStop = false;
            // 
            // LIdAbonnement
            // 
            this.LIdAbonnement.BackColor = System.Drawing.Color.Transparent;
            this.LIdAbonnement.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LIdAbonnement.Location = new System.Drawing.Point(13, 15);
            this.LIdAbonnement.Name = "LIdAbonnement";
            this.LIdAbonnement.Size = new System.Drawing.Size(144, 24);
            this.LIdAbonnement.TabIndex = 4;
            this.LIdAbonnement.Text = "ID Abon. :";
            // 
            // btFIP
            // 
            this.btFIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btFIP.Location = new System.Drawing.Point(702, 13);
            this.btFIP.Name = "btFIP";
            this.btFIP.Size = new System.Drawing.Size(64, 24);
            this.btFIP.TabIndex = 3;
            this.btFIP.Text = "FIP";
            this.btFIP.UseVisualStyleBackColor = false;
            this.btFIP.Click += new System.EventHandler(this.button2_Click);
            // 
            // lblNom
            // 
            this.lblNom.BackColor = System.Drawing.Color.Transparent;
            this.lblNom.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNom.Location = new System.Drawing.Point(163, 15);
            this.lblNom.Name = "lblNom";
            this.lblNom.Size = new System.Drawing.Size(259, 24);
            this.lblNom.TabIndex = 0;
            this.lblNom.Text = "Nom :";
            // 
            // lblCle
            // 
            this.lblCle.BackColor = System.Drawing.Color.Transparent;
            this.lblCle.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCle.Location = new System.Drawing.Point(601, 15);
            this.lblCle.Name = "lblCle";
            this.lblCle.Size = new System.Drawing.Size(95, 24);
            this.lblCle.TabIndex = 2;
            this.lblCle.Text = "Clé :";
            // 
            // lblContrat
            // 
            this.lblContrat.BackColor = System.Drawing.Color.Transparent;
            this.lblContrat.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContrat.Location = new System.Drawing.Point(429, 15);
            this.lblContrat.Name = "lblContrat";
            this.lblContrat.Size = new System.Drawing.Size(166, 24);
            this.lblContrat.TabIndex = 1;
            this.lblContrat.Text = "Contrat :";
            // 
            // LtitreRecherche
            // 
            this.LtitreRecherche.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LtitreRecherche.Location = new System.Drawing.Point(86, 38);
            this.LtitreRecherche.Name = "LtitreRecherche";
            this.LtitreRecherche.Size = new System.Drawing.Size(294, 26);
            this.LtitreRecherche.TabIndex = 0;
            this.LtitreRecherche.Text = "Recherche d\'un patient TA :";
            // 
            // txtFind_Nom
            // 
            this.txtFind_Nom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFind_Nom.Location = new System.Drawing.Point(11, 212);
            this.txtFind_Nom.Name = "txtFind_Nom";
            this.txtFind_Nom.Size = new System.Drawing.Size(119, 22);
            this.txtFind_Nom.TabIndex = 7;
            this.txtFind_Nom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFind_Nom_KeyDown);
            // 
            // label17
            // 
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(35, 193);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(46, 16);
            this.label17.TabIndex = 6;
            this.label17.Text = "Nom :";
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(196, 193);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(96, 16);
            this.label18.TabIndex = 9;
            this.label18.Text = "Téléphone : +";
            // 
            // lwAbonne
            // 
            this.lwAbonne.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader10});
            this.lwAbonne.FullRowSelect = true;
            this.lwAbonne.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lwAbonne.HideSelection = false;
            this.lwAbonne.Location = new System.Drawing.Point(6, 340);
            this.lwAbonne.Name = "lwAbonne";
            this.lwAbonne.Size = new System.Drawing.Size(459, 230);
            this.lwAbonne.TabIndex = 8;
            this.lwAbonne.UseCompatibleStateImageBehavior = false;
            this.lwAbonne.View = System.Windows.Forms.View.Details;
            this.lwAbonne.DoubleClick += new System.EventHandler(this.lwAbonne_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 130;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Width = 140;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Width = 80;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Width = 100;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Width = 0;
            // 
            // label28
            // 
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(256, 129);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(36, 16);
            this.label28.TabIndex = 11;
            this.label28.Text = "Clé :";
            // 
            // txtFind_Cle
            // 
            this.txtFind_Cle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFind_Cle.Location = new System.Drawing.Point(237, 149);
            this.txtFind_Cle.Name = "txtFind_Cle";
            this.txtFind_Cle.Size = new System.Drawing.Size(72, 22);
            this.txtFind_Cle.TabIndex = 12;
            this.txtFind_Cle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFind_Cle_KeyDown);
            // 
            // label44
            // 
            this.label44.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label44.Location = new System.Drawing.Point(131, 129);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(85, 19);
            this.label44.TabIndex = 1;
            this.label44.Text = "Contrat N° :";
            // 
            // txtFindContrat
            // 
            this.txtFindContrat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFindContrat.Location = new System.Drawing.Point(128, 149);
            this.txtFindContrat.Name = "txtFindContrat";
            this.txtFindContrat.Size = new System.Drawing.Size(88, 22);
            this.txtFindContrat.TabIndex = 2;
            this.txtFindContrat.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFindContrat_KeyDown);
            // 
            // rdTriArchive1
            // 
            this.rdTriArchive1.Checked = true;
            this.rdTriArchive1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdTriArchive1.Location = new System.Drawing.Point(62, 84);
            this.rdTriArchive1.Name = "rdTriArchive1";
            this.rdTriArchive1.Size = new System.Drawing.Size(68, 22);
            this.rdTriArchive1.TabIndex = 3;
            this.rdTriArchive1.TabStop = true;
            this.rdTriArchive1.Text = "Tous";
            // 
            // rdTriArchive2
            // 
            this.rdTriArchive2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdTriArchive2.Location = new System.Drawing.Point(157, 84);
            this.rdTriArchive2.Name = "rdTriArchive2";
            this.rdTriArchive2.Size = new System.Drawing.Size(103, 22);
            this.rdTriArchive2.TabIndex = 4;
            this.rdTriArchive2.Text = "non archivés";
            // 
            // rdTriArchive3
            // 
            this.rdTriArchive3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdTriArchive3.Location = new System.Drawing.Point(266, 84);
            this.rdTriArchive3.Name = "rdTriArchive3";
            this.rdTriArchive3.Size = new System.Drawing.Size(72, 22);
            this.rdTriArchive3.TabIndex = 5;
            this.rdTriArchive3.Text = "Archivés";
            // 
            // txtFind_Abonnement
            // 
            this.txtFind_Abonnement.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFind_Abonnement.Location = new System.Drawing.Point(15, 149);
            this.txtFind_Abonnement.Name = "txtFind_Abonnement";
            this.txtFind_Abonnement.Size = new System.Drawing.Size(91, 22);
            this.txtFind_Abonnement.TabIndex = 35;
            this.txtFind_Abonnement.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFind_Abonnement_KeyDown);
            // 
            // label43
            // 
            this.label43.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label43.Location = new System.Drawing.Point(12, 129);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(105, 20);
            this.label43.TabIndex = 36;
            this.label43.Text = "Id Abonnement :";
            // 
            // txtFind_Tel
            // 
            this.txtFind_Tel.BackColor = System.Drawing.Color.White;
            this.txtFind_Tel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFind_Tel.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtFind_Tel.Location = new System.Drawing.Point(182, 212);
            this.txtFind_Tel.Mask = "################";
            this.txtFind_Tel.Name = "txtFind_Tel";
            this.txtFind_Tel.Size = new System.Drawing.Size(136, 22);
            this.txtFind_Tel.TabIndex = 40;
            this.txtFind_Tel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFind_Tel_KeyDown_1);
            // 
            // label58
            // 
            this.label58.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label58.Location = new System.Drawing.Point(344, 193);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(116, 16);
            this.label58.TabIndex = 41;
            this.label58.Text = "Date Naissance :";
            // 
            // txtFind_DateNaiss
            // 
            this.txtFind_DateNaiss.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFind_DateNaiss.Location = new System.Drawing.Point(347, 212);
            this.txtFind_DateNaiss.Name = "txtFind_DateNaiss";
            this.txtFind_DateNaiss.Size = new System.Drawing.Size(102, 22);
            this.txtFind_DateNaiss.TabIndex = 42;
            this.txtFind_DateNaiss.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFind_DateNaiss_KeyDown);
            // 
            // textFindByNFacture
            // 
            this.textFindByNFacture.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textFindByNFacture.Location = new System.Drawing.Point(335, 149);
            this.textFindByNFacture.Name = "textFindByNFacture";
            this.textFindByNFacture.Size = new System.Drawing.Size(91, 22);
            this.textFindByNFacture.TabIndex = 44;
            this.textFindByNFacture.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textFindByNFacture_KeyDown);
            // 
            // labelNFacture
            // 
            this.labelNFacture.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNFacture.Location = new System.Drawing.Point(344, 129);
            this.labelNFacture.Name = "labelNFacture";
            this.labelNFacture.Size = new System.Drawing.Size(105, 20);
            this.labelNFacture.TabIndex = 45;
            this.labelNFacture.Text = "N° de Facture :";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(13, 72);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.cBoxFactBloque);
            this.splitContainer1.Panel1.Controls.Add(this.bRechercher);
            this.splitContainer1.Panel1.Controls.Add(this.LtitreRecherche);
            this.splitContainer1.Panel1.Controls.Add(this.rdTriArchive2);
            this.splitContainer1.Panel1.Controls.Add(this.rdTriArchive1);
            this.splitContainer1.Panel1.Controls.Add(this.labelNFacture);
            this.splitContainer1.Panel1.Controls.Add(this.label28);
            this.splitContainer1.Panel1.Controls.Add(this.txtFind_Cle);
            this.splitContainer1.Panel1.Controls.Add(this.textFindByNFacture);
            this.splitContainer1.Panel1.Controls.Add(this.label18);
            this.splitContainer1.Panel1.Controls.Add(this.rdTriArchive3);
            this.splitContainer1.Panel1.Controls.Add(this.label17);
            this.splitContainer1.Panel1.Controls.Add(this.lwAbonne);
            this.splitContainer1.Panel1.Controls.Add(this.txtFind_Abonnement);
            this.splitContainer1.Panel1.Controls.Add(this.txtFind_DateNaiss);
            this.splitContainer1.Panel1.Controls.Add(this.txtFind_Nom);
            this.splitContainer1.Panel1.Controls.Add(this.label58);
            this.splitContainer1.Panel1.Controls.Add(this.label43);
            this.splitContainer1.Panel1.Controls.Add(this.txtFindContrat);
            this.splitContainer1.Panel1.Controls.Add(this.label44);
            this.splitContainer1.Panel1.Controls.Add(this.txtFind_Tel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.CadetBlue;
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox6);
            this.splitContainer1.Size = new System.Drawing.Size(1273, 772);
            this.splitContainer1.SplitterDistance = 475;
            this.splitContainer1.TabIndex = 47;
            // 
            // cBoxFactBloque
            // 
            this.cBoxFactBloque.AutoSize = true;
            this.cBoxFactBloque.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cBoxFactBloque.Location = new System.Drawing.Point(15, 259);
            this.cBoxFactBloque.Name = "cBoxFactBloque";
            this.cBoxFactBloque.Size = new System.Drawing.Size(146, 20);
            this.cBoxFactBloque.TabIndex = 54;
            this.cBoxFactBloque.Text = "Facturation bloquée";
            this.cBoxFactBloque.UseVisualStyleBackColor = true;
            // 
            // bRechercher
            // 
            this.bRechercher.BackColor = System.Drawing.Color.Transparent;
            this.bRechercher.BackgroundImage = global::ImportSosGeneve.Properties.Resources.boutonjumelles;
            this.bRechercher.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bRechercher.FlatAppearance.BorderSize = 0;
            this.bRechercher.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bRechercher.ImageIndex = 0;
            this.bRechercher.Location = new System.Drawing.Point(200, 259);
            this.bRechercher.Name = "bRechercher";
            this.bRechercher.Size = new System.Drawing.Size(60, 60);
            this.bRechercher.TabIndex = 53;
            this.toolTip1.SetToolTip(this.bRechercher, "Rechercher le patient");
            this.bRechercher.UseVisualStyleBackColor = false;
            this.bRechercher.Click += new System.EventHandler(this.bRechercher_Click);
            // 
            // LTitre
            // 
            this.LTitre.BackColor = System.Drawing.Color.Transparent;
            this.LTitre.Font = new System.Drawing.Font("Arial Black", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LTitre.Location = new System.Drawing.Point(469, 7);
            this.LTitre.Name = "LTitre";
            this.LTitre.Size = new System.Drawing.Size(385, 51);
            this.LTitre.TabIndex = 48;
            this.LTitre.Text = "Gestion des télé-alarme";
            // 
            // bAnnuler
            // 
            this.bAnnuler.BackColor = System.Drawing.Color.Transparent;
            this.bAnnuler.BackgroundImage = global::ImportSosGeneve.Properties.Resources.bondCancel;
            this.bAnnuler.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bAnnuler.FlatAppearance.BorderSize = 0;
            this.bAnnuler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bAnnuler.ImageIndex = 0;
            this.bAnnuler.Location = new System.Drawing.Point(1226, 3);
            this.bAnnuler.Name = "bAnnuler";
            this.bAnnuler.Size = new System.Drawing.Size(60, 60);
            this.bAnnuler.TabIndex = 49;
            this.toolTip1.SetToolTip(this.bAnnuler, "Annuler");
            this.bAnnuler.UseVisualStyleBackColor = false;
            this.bAnnuler.Click += new System.EventHandler(this.bAnnuler_Click);
            // 
            // bEnregistrer
            // 
            this.bEnregistrer.BackColor = System.Drawing.Color.Transparent;
            this.bEnregistrer.BackgroundImage = global::ImportSosGeneve.Properties.Resources.boutonsEnregistrer;
            this.bEnregistrer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bEnregistrer.FlatAppearance.BorderSize = 0;
            this.bEnregistrer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bEnregistrer.ImageIndex = 0;
            this.bEnregistrer.Location = new System.Drawing.Point(1132, 3);
            this.bEnregistrer.Name = "bEnregistrer";
            this.bEnregistrer.Size = new System.Drawing.Size(60, 60);
            this.bEnregistrer.TabIndex = 50;
            this.toolTip1.SetToolTip(this.bEnregistrer, "Enregistrer");
            this.bEnregistrer.UseVisualStyleBackColor = false;
            this.bEnregistrer.Click += new System.EventHandler(this.bEnregistrer_Click);
            // 
            // bEnvoiMail
            // 
            this.bEnvoiMail.BackColor = System.Drawing.Color.Transparent;
            this.bEnvoiMail.BackgroundImage = global::ImportSosGeneve.Properties.Resources.bEnvoi;
            this.bEnvoiMail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bEnvoiMail.FlatAppearance.BorderSize = 0;
            this.bEnvoiMail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bEnvoiMail.ImageIndex = 8;
            this.bEnvoiMail.ImageList = this.imageList1;
            this.bEnvoiMail.Location = new System.Drawing.Point(969, 3);
            this.bEnvoiMail.Name = "bEnvoiMail";
            this.bEnvoiMail.Size = new System.Drawing.Size(60, 60);
            this.bEnvoiMail.TabIndex = 51;
            this.toolTip1.SetToolTip(this.bEnvoiMail, "Envoyer un Email pour les clés");
            this.bEnvoiMail.UseVisualStyleBackColor = false;
            this.bEnvoiMail.Click += new System.EventHandler(this.bEnvoiMail_Click);
            // 
            // bEtiquette
            // 
            this.bEtiquette.BackColor = System.Drawing.Color.Transparent;
            this.bEtiquette.BackgroundImage = global::ImportSosGeneve.Properties.Resources.boutonsEtiquette;
            this.bEtiquette.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bEtiquette.FlatAppearance.BorderSize = 0;
            this.bEtiquette.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bEtiquette.ImageIndex = 0;
            this.bEtiquette.Location = new System.Drawing.Point(1053, 3);
            this.bEtiquette.Name = "bEtiquette";
            this.bEtiquette.Size = new System.Drawing.Size(60, 60);
            this.bEtiquette.TabIndex = 52;
            this.toolTip1.SetToolTip(this.bEtiquette, "Etiquette patient");
            this.bEtiquette.UseVisualStyleBackColor = false;
            this.bEtiquette.Click += new System.EventHandler(this.bEtiquette_Click);
            // 
            // CtrlTA
            // 
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.Controls.Add(this.bEtiquette);
            this.Controls.Add(this.bEnvoiMail);
            this.Controls.Add(this.bEnregistrer);
            this.Controls.Add(this.bAnnuler);
            this.Controls.Add(this.LTitre);
            this.Controls.Add(this.splitContainer1);
            this.Name = "CtrlTA";
            this.Size = new System.Drawing.Size(1306, 855);
            this.Load += new System.EventHandler(this.CtrlTA_Load);
            this.tabControl1.ResumeLayout(false);
            this.tbAbonnement.ResumeLayout(false);
            this.tbAbonnement.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tbTypeAbonnement.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel1.PerformLayout();
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.gBoxPeriode.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.tbContacts.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tbCle.ResumeLayout(false);
            this.tbCle.PerformLayout();
            this.tbJournal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpOnglet5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpOnglet5_Sheet1)).EndInit();
            this.gpJournal.ResumeLayout(false);
            this.gpJournal.PerformLayout();
            this.tbFactures.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tbDossierMedical.ResumeLayout(false);
            this.tbDossierMedical.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        #region Méthodes et propriétés sur les onglets

        public void SetOnglet(int Index)
        {
            this.tabControl1.SelectedIndex = Index;
        }

        #endregion

        #region Opérations sur les abonnement

        public void NouveauAbonnement()
        {                                   
            //On réactive les contrôles
            tbAbonnement.Tag = -1;

            foreach (Control c in tbAbonnement.Controls)
            {
                if (c.GetType() == typeof(TextBox))
                {
                    c.Text = "";
                    ((TextBox)c).ReadOnly = false;
                }
            }
            foreach (Control c in groupBox1.Controls)
            {
                if (c.GetType() == typeof(TextBox))
                {
                    c.Text = "";
                }
            }

            EMaskTel1.Text = "";
            txtTa_FacNom.Text = "";
            txtTa_FacPrenom.Text = "";
            txtTa_FacNP.Text = "";
            txtTa_FacLocalite.Text = "";
            txtTa_FacAdresse.Text = "";
            listViewTel.Items.Clear();

            lwMemoire.Items.Clear();
            lwUrgence.Items.Clear();
            cbOnglet2Lien1.Text = "";
            cbOnglet2Lien2.Text = "";
            txtOnglet2Nom1.Text = "";
            txtOnglet2PreNom1.Text = "";
            txtOnglet2NRue1.Text = "";
            txtOnglet2Adresse1.Text = "";
            txtOnglet2Localite1.Text = "";
            txtOnglet2Np1.Text = "";
            txtOnglet2Tel1.Text = "";
            txtOnglet2Tel1bis.Text = "";
            txtOnglet2Tel1ter.Text = "";
            txtOnglet2Nom2.Text = "";
            txtOnglet2PreNom2.Text = "";
            txtOnglet2NRue2.Text = "";
            txtOnglet2Adresse2.Text = "";
            txtOnglet2Localite2.Text = "";
            txtOnglet2Np2.Text = "";
            txtOnglet2Tel2.Text = "";
            txtOnglet2Tel2bis.Text = "";
            txtOnglet2Tel2ter.Text = "";
            txtN_TA.Text = "";

            lblNom.Text = "";
            lblCle.Text = "";
            lblContrat.Text = "";
            LIdAbonnement.Text = "";

            rdPeriodFac0.Checked = false;
            rdPeriodFac1.Checked = false;
            rdPeriodFac2.Checked = false;
            rdPeriodFac4.Checked = false;

            rBSexFactF.Checked = true;
       
            //Pour contrôles vérouillés (on les réactive pour les décocher puis on les désactive)
            if (checkBoxSansRappelTA.Enabled == false)
            {
                checkBoxSansRappelTA.Enabled = true;        //On réactive les ctrl
                rdPeriodFac3.Enabled = true;
                cbOrdre.Enabled = true;

                checkBoxSansRappelTA.Checked = false;       //On les décoches
                rdPeriodFac3.Checked = false;
                cbOrdre.Checked = false;

                checkBoxSansRappelTA.Enabled = false;       //On les déactives
                rdPeriodFac3.Enabled = false;
                cbOrdre.Enabled = false;
            }
            else
            {
                checkBoxSansRappelTA.Checked = false;       //On les décoches
                rdPeriodFac3.Checked = false;
                cbOrdre.Checked = false;
            }

            //puis on initialise les variables
            stopRappel = "";
            OrdrePermanent = 0;
            Bloquer = 0;

            cbExport.Checked = false;
            cbExporter.Checked = false;
            cbExporter.Enabled = false;
            cbModifFiche.Checked = false;

            //Pour l'état des boutons
            bNouveau1.Visible = true;
            bNouveau1.Enabled = true;
            bNouveau1.ImageIndex = 6;
            bSupprimer1.Enabled = true;
            bSupprimer1.ImageIndex = 0;

            bModifierEn1.Visible = true;
            bModifierEn1.Enabled = true;
            bModifierEn1.ImageIndex = 10;

            bNouveau2.Visible = true;
            bNouveau2.Enabled = true;
            bNouveau2.ImageIndex = 6;

            bSupprimer2.Enabled = true;
            bSupprimer2.ImageIndex = 0;

            bModifierEn2.Visible = true;
            bModifierEn2.Enabled = true;
            bModifierEn2.ImageIndex = 10;

            bVerif.ImageIndex = 18;
            bVerif.Enabled = true;
            bVerif.Tag = null;

            bEnvoiMail.Enabled = true;
            bEnvoiMail.ImageIndex = 8;

            groupBox1.Enabled = true;
            groupBox2.Enabled = true;
            groupBox3.Enabled = true;

            chkDossierBleu.Checked = false;
            chkFaxFSASD.Checked = false;
            chkClePresente.Checked = false;

            bAjoutMedecin.Enabled = true;
            bAjoutMedecin.ImageIndex = 6;
            bSupprMedecin.Enabled = true;
            bSupprMedecin.ImageIndex = 0;

            lwMedTTT.Items.Clear();
            txtOnglet3Pb.Text = "";
            txtOnglet3Poids.Text = "";
            txtOnglet3Tel.Text = "";
            txtOnglet3Attitudes.Text = "";
            txtOnglet3Medic.Text = "";
            dtDebutFacturation.Value = DateTime.Now;

            txtNumCle.Text = "";
            txtCommentaireCle.Text = "";
            txtIdContrat.Text = ""; ;

            foreach (Control c in tbDossierMedical.Controls)
            {
                if (c.GetType() == typeof(RadioButton))
                    ((RadioButton)c).Checked = false;
                if (c.GetType() == typeof(CheckBox))
                    ((CheckBox)c).Checked = false;
            }

            gpJournal.Enabled = false;

            bAjoutNvlLigneJ.Enabled = true;
            bAjoutNvlLigneJ.ImageIndex = 6;
            bSupprLigneJ.Enabled = true;
            bSupprLigneJ.ImageIndex = 0;

            bValideJournal.ImageIndex = 3;
            bValideJournal.Enabled = false;
            bCancelJournal.ImageIndex = 5;
            bCancelJournal.Enabled = false;           
                       
            fpOnglet5_Sheet1.RowCount = 0;
            rdOnglet5Annulation.Checked = false;
            rdOnglet5Cle.Checked = false;
            rdOnglet5Dossier.Checked = false;
            rdOnglet5Retourcontrat.Checked = false;
            txtOnglet5Commentaire.Text = "";
            txtOnglet5EnvoiA.Text = "";
            txtOnglet5EnvoiDe.Text = "";
            dtOnglet5Le.Value = DateTime.Now;
            txtOnglet5ICE.Text = "";
            txtOnglet5NbCle.Text = "";

            // Ajout d'un contact forcé = SOS Médecins
            DataTable dt = OutilsExt.OutilsSql.RecupereStructureContactVierge();
            DataRow row = dt.NewRow();
            row["IdAbonnement"] = -1;
            row["Lien"] = "Autre";
            row["Nom"] = "SOS";
            row["Prenom"] = "Médecins";
            row["Telephone"] = "+41227484949";
            row["Tel2"] = "";
            row["Tel3"] = "";
            row["NumeroRue"] = "";
            row["Rue"] = "";
            row["Np"] = "";
            row["Localite"] = "";

            ListViewItem item = new ListViewItem("Autre");
            item.Tag = row;
            item.SubItems.Add("SOS Médecins");
            item.SubItems.Add(row["Telephone"].ToString());
            lwMemoire.Items.Add(item);

            //Puis on vide dtNvxMateriel
            dtNvxMateriel.Rows.Clear();
            listViewMat1.Items.Clear();

            //Idem pour les n° de Tel
            dtNvxTel.Rows.Clear();
            dtDelTel.Rows.Clear();
            listViewTel.Items.Clear();
            EmaskAjoutTel.Text = "";
            bAjouteTel1.Enabled = false;
            bAjouteTel1.ImageIndex = 7;

            //On désactive les contrôles Materiels
            rBTypeBoitier1.Enabled = false;
            rBTypeBoitier2.Enabled = false;
            rBTypeBoitier3.Enabled = false;
            comboBMateriel.Enabled = false;
            listViewMat1.Enabled = false;
            tBoxSupprMatos.Enabled = false;
            bAjoutMat1.Enabled = false;
            bSupprMatos.Enabled = false;
            cBoxMotifChangement.Enabled = false;

            //Pour activer la facturation
            bActiverFacture.Enabled = false;

            AncienCheck = 0;
            Desafection[0] = string.Empty;
            Desafection[1] = string.Empty;
            
            txtTa_Nom.Focus();
        }

        public void SupprimeAbonnement()
        {
            //En fait on l'archive....
            //On regarde s'il n'est pas déjà archivé                                   
            if (tbAbonnement.Tag != null && tbAbonnement.Tag.ToString() != "-1")
            {
                if (VerifArchive(tbAbonnement.Tag.ToString()) == "Non Archivé")
                {

                    bool reussite = OutilsExt.OutilsSql.SupprimeAbonnement(int.Parse(tbAbonnement.Tag.ToString()));
                    if (reussite)
                    {
                        //on déaffecte le boitier SOS si c'est un médicalarme
                        string[] Ope = { "Archive" };

                        MajMateriel("00000000", tbAbonnement.Tag.ToString(), Ope);

                        mouchard.evenement("Modification de la fiche TA pour  " + txtTa_Nom.Text.ToString() + " " + txtTa_Prenom.Text.ToString() + ": Archivage de l'abonnement.", VariablesApplicatives.Utilisateurs.NomUtilisateur.ToString());  //log
                        MessageBox.Show("Abonnement archivé");
                        AfficheAbonnement(null);
                    }
                    else
                    {
                        MessageBox.Show("Erreur à l'archivage de l'abonnement");
                    }
                }
                else
                {
                    MessageBox.Show("Cet abonnement est DEJA archivé!");
                }
            }
        }

        public void DeSupprimeAbonnement()
        {
            //En fait on le dé-archive...
            //On regarde s'il est archivé au moins         
            if (tbAbonnement.Tag != null && tbAbonnement.Tag.ToString() != "-1")
            {
                if (VerifArchive(tbAbonnement.Tag.ToString()) == "Archivé")
                {
                    bool reussite = OutilsExt.OutilsSql.deSupprimeAbonnement(int.Parse(tbAbonnement.Tag.ToString()));
                    if (reussite)
                    {
                        MessageBox.Show("Abonnement Desarchivé, vérifiez l'attribution d'une box TA");
                        mouchard.evenement("Modification de la fiche TA pour  " + txtTa_Nom.Text.ToString() + " " + txtTa_Prenom.Text.ToString() + ": Désarchivage de l'abonnement.", VariablesApplicatives.Utilisateurs.NomUtilisateur.ToString());  //log
                        AfficheAbonnement(null);
                    }
                    else
                    {
                        MessageBox.Show("Erreur à l'archivage de l'abonnement");
                    }
                }
                else
                {
                    MessageBox.Show("Cet abonnement N'EST PAS archivé...Pas besoin de le dé-archiver!");
                }
            }
        }


        public void ModifieAbonnement()
        {
            //On réactive les contrôles de fiche 
            foreach (Control c in tbAbonnement.Controls)
            {
                if (c.GetType() == typeof(TextBox))
                {
                    ((TextBox)c).ReadOnly = false;
                }
            }

            //Pour l'état des boutons
            bNouveau1.Visible = true;
            bNouveau1.Enabled = true;
            bNouveau1.ImageIndex = 6;
            bSupprimer1.Enabled = true;
            bSupprimer1.ImageIndex = 0;
            bModifierEn1.Visible = true;
            bModifierEn1.ImageIndex = 10;

            bNouveau2.Visible = true;
            bNouveau2.Enabled = true;
            bNouveau2.ImageIndex = 6;
            bSupprimer2.Enabled = true;
            bSupprimer2.ImageIndex = 0;
            bModifierEn2.Visible = true;
            bModifierEn2.ImageIndex = 10;

            bAjoutMedecin.Enabled = true;
            bAjoutMedecin.ImageIndex = 6;
            bSupprMedecin.Enabled = true;
            bSupprMedecin.ImageIndex = 0;

            gpJournal.Enabled = false;

            bAjoutNvlLigneJ.Enabled = true;
            bAjoutNvlLigneJ.ImageIndex = 6;
            bSupprLigneJ.Enabled = true;
            bSupprLigneJ.ImageIndex = 0;

            bValideJournal.ImageIndex = 3;
            bValideJournal.Enabled = false;
            bCancelJournal.ImageIndex = 5;
            bCancelJournal.Enabled = false;

            groupBox1.Enabled = true;
            groupBox2.Enabled = true;
            groupBox3.Enabled = true;

            //En fonction du type de boitier, on active ou pas le panneau materiel
            if (rdOnglet3Type4.Checked)
            {
                rBTypeBoitier1.Enabled = true;
                rBTypeBoitier2.Enabled = true;
                rBTypeBoitier3.Enabled = true;
                comboBMateriel.Enabled = true;
                listViewMat1.Enabled = true;
                tBoxSupprMatos.Enabled = true;
                bAjoutMat1.Enabled = true;
                bSupprMatos.Enabled = true;               
            }
            else   //On déactive les contrôles
            {
                rBTypeBoitier1.Enabled = false;
                rBTypeBoitier2.Enabled = false;
                rBTypeBoitier3.Enabled = false;
                comboBMateriel.Enabled = false;
                listViewMat1.Enabled = false;
                tBoxSupprMatos.Enabled = false;
                bAjoutMat1.Enabled = false;
                bSupprMatos.Enabled = false;
            }

            //Pour les Tel du patient
            listViewTel.Enabled = true;

            //en fonction des droits on désactive certains controles
            if (VariablesApplicatives.Utilisateurs.Droits == VariablesApplicatives.Utilisateurs.CodeDroits.Comptable
                || VariablesApplicatives.Utilisateurs.Droits == VariablesApplicatives.Utilisateurs.CodeDroits.Chef
                || VariablesApplicatives.Utilisateurs.Droits == VariablesApplicatives.Utilisateurs.CodeDroits.Admin)
            {
                checkBoxSansRappelTA.Enabled = true;
                cbOrdre.Enabled = true;
                rdPeriodFac3.Enabled = true;
            }
            else
            {
                checkBoxSansRappelTA.Enabled = false;
                cbOrdre.Enabled = false;
                rdPeriodFac3.Enabled = false;
            }

            txtTa_Nom.Focus();
        }

        public void AfficheAbonnement(DataSet ds)
        {
            //Par défaut, les contrôles sont désactivés            
            tbAbonnement.Tag = null;

            foreach (Control c in tbAbonnement.Controls)
            {
                if (c.GetType() == typeof(TextBox))
                {
                    c.Text = "";
                    ((TextBox)c).ReadOnly = true;
                }
            }
            foreach (Control c in groupBox1.Controls)
            {
                if (c.GetType() == typeof(TextBox))
                {
                    c.Text = "";
                }
            }

            bVerif.ImageIndex = 19;
            bVerif.Enabled = false;
            bVerif.Tag = null;

            lwMemoire.Items.Clear();
            lwUrgence.Items.Clear();
          //  lwfactures.Items.Clear();
            cbOnglet2Lien1.Text = "";
            cbOnglet2Lien2.Text = "";
            txtOnglet2Nom1.Text = "";
            txtOnglet2PreNom1.Text = "";
            txtOnglet2NRue1.Text = "";
            txtOnglet2Adresse1.Text = "";
            txtOnglet2Localite1.Text = "";
            txtOnglet2Np1.Text = "";
            txtOnglet2Tel1.Text = "";
            txtOnglet2Tel1bis.Text = "";
            txtOnglet2Tel1ter.Text = "";
            txtOnglet2Nom2.Text = "";
            txtOnglet2PreNom2.Text = "";
            txtOnglet2NRue2.Text = "";
            txtOnglet2Adresse2.Text = "";
            txtOnglet2Localite2.Text = "";
            txtOnglet2Np2.Text = "";
            txtOnglet2Tel2.Text = "";
            txtOnglet2Tel2bis.Text = "";
            txtOnglet2Tel2ter.Text = "";
            txtN_TA.Text = "";

            txtNumCle.Text = "";
            txtCommentaireCle.Text = "";
            txtIdContrat.Text = "";
            rdPeriodFac0.Checked = false;
            rdPeriodFac1.Checked = false;
            rdPeriodFac2.Checked = false;
            //rdPeriodFac3.Checked = false;
            rdPeriodFac4.Checked = false;

            rBSexFactF.Checked = true;


            //cbOrdre.Checked = false;
            cbExport.Checked = false;

            cbExporter.Checked = false;
            cbExporter.Enabled = false;

            cbModifFiche.Checked = false;

            lblNom.Text = "";
            lblCle.Text = "";
            lblContrat.Text = "";
            LIdAbonnement.Text = "";

            chkDossierBleu.Checked = false;
            chkFaxFSASD.Checked = false;
            chkClePresente.Checked = false;

            dtDebutFacturation.Value = DateTime.Now;

            gpJournal.Enabled = false;

            bAjoutNvlLigneJ.ImageIndex = 7;
            bAjoutNvlLigneJ.Enabled = false;
            bSupprLigneJ.ImageIndex = 1;
            bSupprLigneJ.Enabled = false;

            bValideJournal.ImageIndex = 3;
            bValideJournal.Enabled = false;
            bCancelJournal.ImageIndex = 5;
            bCancelJournal.Enabled = false;

            bEnvoiMail.ImageIndex = 9;
            bEnvoiMail.Enabled = false;

            fpOnglet5_Sheet1.RowCount = 0;
            rdOnglet5Annulation.Checked = false;
            rdOnglet5Cle.Checked = false;
            rdOnglet5Dossier.Checked = false;
            rdOnglet5Retourcontrat.Checked = false;
            txtOnglet5Commentaire.Text = "";
            txtOnglet5EnvoiA.Text = "";
            txtOnglet5EnvoiDe.Text = "";
            dtOnglet5Le.Value = DateTime.Now;
            txtOnglet5ICE.Text = "";
            txtOnglet5NbCle.Text = "";


            //Pour l'état des boutons
            bNouveau1.Visible = true;
            bNouveau1.Enabled = false;
            bNouveau1.ImageIndex = 7;

            bSupprimer1.Enabled = false;
            bSupprimer1.ImageIndex = 1;

            bModifierEn1.Visible = true;
            bModifierEn1.Enabled = false;
            bModifierEn1.ImageIndex = 11;

            bNouveau2.Visible = true;
            bNouveau2.Enabled = false;
            bNouveau2.ImageIndex = 7;

            bSupprimer2.Enabled = false;
            bSupprimer2.ImageIndex = 1;

            bModifierEn2.Visible = true;
            bModifierEn2.Enabled = false;
            bModifierEn2.ImageIndex = 11;

            bAjoutMedecin.ImageIndex = 7;
            bAjoutMedecin.Enabled = false;
            bSupprMedecin.ImageIndex = 1;
            bSupprMedecin.Enabled = false;

            lwMedTTT.Items.Clear();
            txtOnglet3Pb.Text = "";
            txtOnglet3Poids.Text = "";
            txtOnglet3Tel.Text = "";
            txtOnglet3Attitudes.Text = "";
            txtOnglet3Medic.Text = "";

            //puis on initialise les variables
            stopRappel = "";
            OrdrePermanent = 0;
            Bloquer = 0;

            //Pour la liste du materiel
            dtNvxMateriel.Rows.Clear();
            listViewMat1.Items.Clear();

            //Pour la liste des n° de Tel
            dtNvxTel.Rows.Clear();
            dtDelTel.Rows.Clear();
            listViewTel.Items.Clear();
            EmaskAjoutTel.Text = "";
            bAjouteTel1.Enabled = false;
            bAjouteTel1.ImageIndex = 7;
           
            tBoxSupprMatos.Text = "";

            //Pour Activer les factures
            bActiverFacture.Enabled = true;

            AncienCheck = 0;

            foreach (Control c in tbDossierMedical.Controls)
            {
                if (c.GetType() == typeof(RadioButton))
                    ((RadioButton)c).Checked = false;
                if (c.GetType() == typeof(CheckBox))
                    ((CheckBox)c).Checked = false;
            }

            groupBox1.Enabled = false;
            groupBox2.Enabled = false;
            groupBox3.Enabled = false;

            if (ds != null)
            {
                // Table contenant l'abonnement
                tbAbonnement.Tag = ds.Tables[0].Rows[0]["IdAbonnement"].ToString();
                txtTa_Nom.Text = ds.Tables[0].Rows[0]["Nom"].ToString();
                txtTa_Prenom.Text = ds.Tables[0].Rows[0]["PreNom"].ToString();
                EMaskTel1.Text = ds.Tables[0].Rows[0]["Tel"].ToString();

                if (ds.Tables[0].Rows[0]["DateNaissance"].ToString() != "")
                    txtTa_Naissance.Text = DateTime.Parse(ds.Tables[0].Rows[0]["DateNaissance"].ToString()).ToString().Split(' ')[0];
                else
                    txtTa_Naissance.Text = "";

                if ((ds.Tables[0].Rows[0]["StopRappelTA"].ToString() == null) || (ds.Tables[0].Rows[0]["StopRappelTA"].ToString() == ""))
                {
                    if (checkBoxSansRappelTA.Enabled == false)
                    {
                        checkBoxSansRappelTA.Enabled = true;
                        checkBoxSansRappelTA.Checked = false;
                        checkBoxSansRappelTA.Enabled = false;
                    }
                    else checkBoxSansRappelTA.Checked = false;

                    stopRappel = "";
                }
                else
                {
                    if (checkBoxSansRappelTA.Enabled == false)
                    {
                        checkBoxSansRappelTA.Enabled = true;
                        checkBoxSansRappelTA.Checked = true;
                        checkBoxSansRappelTA.Enabled = false;
                    }
                    else checkBoxSansRappelTA.Checked = true;

                    stopRappel = ds.Tables[0].Rows[0]["StopRappelTA"].ToString();
                }


                if (ds.Tables[0].Rows[0]["Sexe"].ToString() == "F")
                    rdTa_Femme.Checked = true;
                else if (ds.Tables[0].Rows[0]["Sexe"].ToString() == "H")
                    rdTa_Homme.Checked = true;

                if (ds.Tables[0].Rows[0]["TF_Sexe"].ToString() == "F")
                    rBSexFactF.Checked = true;
                else if (ds.Tables[0].Rows[0]["TF_Sexe"].ToString() == "H")
                    rBSexFactH.Checked = true;
                else if (ds.Tables[0].Rows[0]["TF_Sexe"].ToString() == "A")
                    rBSexFactA.Checked = true;

                txtIdContrat.Text = ds.Tables[0].Rows[0]["IdContrat"].ToString();

                if (ds.Tables[0].Rows[0]["ClePresente"].ToString() == "1")
                    chkClePresente.Checked = true;
                else
                    chkClePresente.Checked = false;
                if (ds.Tables[0].Rows[0]["DossierBleu"].ToString() == "1")
                    chkDossierBleu.Checked = true;
                else
                    chkDossierBleu.Checked = false;
                if (ds.Tables[0].Rows[0]["FaxFsasd"].ToString() == "1")
                    chkFaxFSASD.Checked = true;
                else
                    chkFaxFSASD.Checked = false;
                txtN_TA.Text = ds.Tables[0].Rows[0]["N_TA"].ToString();


                txtTa_No.Text = ds.Tables[0].Rows[0]["NumeroDansRue"].ToString();
                txtTa_Adresse.Text = ds.Tables[0].Rows[0]["Rue"].ToString();
                txtTa_Np.Text = ds.Tables[0].Rows[0]["CodePostal"].ToString();
                txtTa_Localite.Text = ds.Tables[0].Rows[0]["Commune"].ToString();
                txtTa_Etage.Text = ds.Tables[0].Rows[0]["Etage"].ToString();
                txtTa_Escalier.Text = ds.Tables[0].Rows[0]["Escalier"].ToString();
                txtTa_Batiment.Text = ds.Tables[0].Rows[0]["Batiment"].ToString();
                txtTa_Porte.Text = ds.Tables[0].Rows[0]["Porte"].ToString();
                txtTa_Digicode.Text = ds.Tables[0].Rows[0]["Digicode"].ToString();
                txtTa_Interphone.Text = ds.Tables[0].Rows[0]["Internom"].ToString();

                txtTa_FacNom.Text = ds.Tables[0].Rows[0]["TF_Nom"].ToString();
                txtTa_FacPrenom.Text = ds.Tables[0].Rows[0]["TF_Prenom"].ToString();
                txtTa_FacNP.Text = ds.Tables[0].Rows[0]["TF_NumeroPostal"].ToString();
                txtTa_FacLocalite.Text = ds.Tables[0].Rows[0]["TF_Localite"].ToString();
                txtTa_FacAdresse.Text = ds.Tables[0].Rows[0]["TF_Adresse"].ToString();

                dtDebutFacturation.Value = DateTime.Parse(ds.Tables[0].Rows[0]["DateDebutFacturation"].ToString());

                // Table Contenant les contacts
                for (int c = 0; c < ds.Tables[1].Rows.Count; c++)
                {
                    ListViewItem item = new ListViewItem(ds.Tables[1].Rows[c]["Lien"].ToString());
                    item.Tag = ds.Tables[1].Rows[c];
                    item.SubItems.Add(ds.Tables[1].Rows[c]["Nom"].ToString() + " " + ds.Tables[1].Rows[c]["Prenom"].ToString());
                    item.SubItems.Add(ds.Tables[1].Rows[c]["Telephone"].ToString());
                    lwMemoire.Items.Add(item);
                }
                // Table Contenant les contacts urgents
                for (int c = 0; c < ds.Tables[2].Rows.Count; c++)
                {
                    ListViewItem item = new ListViewItem(ds.Tables[2].Rows[c]["Lien"].ToString());
                    item.Tag = ds.Tables[2].Rows[c];
                    item.SubItems.Add(ds.Tables[2].Rows[c]["Nom"].ToString() + " " + ds.Tables[2].Rows[c]["Prenom"].ToString());
                    item.SubItems.Add(ds.Tables[2].Rows[c]["Telephone"].ToString());
                    lwUrgence.Items.Add(item);
                }
                // Table Contenant les factures 
                if (ds.Tables.Count == 7)
                {
                    //Paramètres du datagridView              
                    dataGridView1.DataSource = ds.Tables[6];
                    dataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dataGridView1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dataGridView1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dataGridView1.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dataGridView1.Columns[8].Visible = false;                    
                    
                    dataGridView1.Columns[0].HeaderText = "N° de facture";
                    dataGridView1.Columns[1].HeaderText = "Date Facture";
                    dataGridView1.Columns[2].HeaderText = "Montant";
                    dataGridView1.Columns[3].HeaderText = "Début de période";
                    dataGridView1.Columns[4].HeaderText = "Fin de période";
                    dataGridView1.Columns[5].HeaderText = "Date paiement";
                    dataGridView1.Columns[6].HeaderText = "Acquité ?";
                    dataGridView1.Columns[7].HeaderText = "Moyen de paiement";

                    //on centre le titre de toute les colonnes
                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                    {
                        col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }

                    dataGridView1.Columns[0].Width = 80;
                    dataGridView1.Columns[1].Width = 100;
                    dataGridView1.Columns[2].Width = 80;
                    dataGridView1.Columns[3].Width = 120;
                    dataGridView1.Columns[4].Width = 100;
                    dataGridView1.Columns[5].Width = 100;
                    dataGridView1.Columns[6].Width = 60;
                    dataGridView1.Columns[7].Width = 80;                  
                }


                if (ds.Tables[0].Rows[0]["Periodicite"].ToString() == "M" || ds.Tables[0].Rows[0]["Periodicite"].ToString() == "MeM")
                    rdPeriodFac0.Checked = true;

                if (ds.Tables[0].Rows[0]["Periodicite"].ToString() == "T" || ds.Tables[0].Rows[0]["Periodicite"].ToString() == "MeT")
                    rdPeriodFac1.Checked = true;
                else if (ds.Tables[0].Rows[0]["Periodicite"].ToString() == "A" || ds.Tables[0].Rows[0]["Periodicite"].ToString() == "MeA")
                    rdPeriodFac2.Checked = true;
                else if (ds.Tables[0].Rows[0]["Periodicite"].ToString() == "S" || ds.Tables[0].Rows[0]["Periodicite"].ToString() == "MeS")
                    rdPeriodFac4.Checked = true;
                else if (ds.Tables[0].Rows[0]["Periodicite"].ToString() == "B")
                {
                    rdPeriodFac3.Checked = true;

                    //puis on initialise les variables
                    Bloquer = 1;
                }


                if (ds.Tables[0].Rows[0]["Ordre"].ToString() == "1")
                {
                    if (cbOrdre.Enabled == false)
                    {
                        cbOrdre.Enabled = true;
                        cbOrdre.Checked = true;
                        cbOrdre.Enabled = false;
                    }
                    else cbOrdre.Checked = true;

                    //puis on initialise les variables
                    OrdrePermanent = 1;
                }
                else
                {
                    if (cbOrdre.Enabled == false)
                    {
                        cbOrdre.Enabled = true;
                        cbOrdre.Checked = false;
                        cbOrdre.Enabled = false;
                    }
                    else cbOrdre.Checked = false;

                    //puis on initialise les variables
                    OrdrePermanent = 0;
                }

                //Pour Activer les factures (si c'est déjà fait, bouton inactif)
                if (ds.Tables[0].Rows[0]["ActiverFacture"].ToString() == "1")
                    bActiverFacture.Enabled = false;
                else
                    bActiverFacture.Enabled = true;

                if (ds.Tables[0].Rows[0]["Export"].ToString() == "1")
                    cbExport.Checked = true;
                if (ds.Tables[0].Rows[0]["ExportMcc"].ToString() == "1")
                {
                    cbExporter.Checked = true;
                    cbExporter.Enabled = true;
                }
                //if(ds.Tables[0].Rows[0]["ExportModif"].ToString()== "1")
                //    cbModifFiche.Checked = true;

                // Table contenant le dossier médical succint
                txtOnglet3Pb.Text = ds.Tables[3].Rows[0]["TD_PbMedicaux"].ToString();
                txtOnglet3Medic.Text = ds.Tables[3].Rows[0]["TD_Traitements"].ToString();
                txtOnglet3Poids.Text = ds.Tables[3].Rows[0]["TD_Poids"].ToString();
                //	txtOnglet3Tel.Text= ds.Tables[3].Rows[0]["TD_Telephone"].ToString();
                txtOnglet3Attitudes.Text = ds.Tables[3].Rows[0]["TD_Attitudes"].ToString();
                if (ds.Tables[3].Rows[0]["TD_RisqueChute"].ToString() == "1")
                    chkOnglet3Risque.Checked = true;

                if (ds.Tables[3].Rows[0]["TD_FSASD"].ToString() == "1")
                    chkOnglet3Fsasd.Checked = true;
                if (ds.Tables[3].Rows[0]["TD_Autres_services"].ToString() == "1")
                    chkOnglet3AutreServices.Checked = true;

                //Utilisation du champ TD_FS_repas (obsolète) pour la gestion des sourds et malentendants
                if (ds.Tables[3].Rows[0]["TD_FS_repas"].ToString() == "1")
                    CB_Sourd.Checked = true;


                //*****Matériel********
                //Type de boitier 1 = Imad; 2 = Swisscom; 3 = Aucun; 4 = SOS Médecins; 5 = Privé
                if (ds.Tables[3].Rows[0]["TD_TypeAppareil"].ToString() == "1")
                    rdOnglet3Type1.Checked = true;
                else if (ds.Tables[3].Rows[0]["TD_TypeAppareil"].ToString() == "2")
                    rdOnglet3Type2.Checked = true;
                if (ds.Tables[3].Rows[0]["TD_TypeAppareil"].ToString() == "3")
                    rdOnglet3Type3.Checked = true;
                if (ds.Tables[3].Rows[0]["TD_TypeAppareil"].ToString() == "4")
                    rdOnglet3Type4.Checked = true;
                if (ds.Tables[3].Rows[0]["TD_TypeAppareil"].ToString() == "5")
                    rdOnglet3Type5.Checked = true;


                //En fonction du type de boitier, on active ou pas le panneau materiel
                if (rdOnglet3Type4.Checked)
                {
                    rBTypeBoitier1.Enabled = true;
                    rBTypeBoitier2.Enabled = true;
                    rBTypeBoitier3.Enabled = true;
                    comboBMateriel.Enabled = true;
                    listViewMat1.Enabled = true;
                    tBoxSupprMatos.Enabled = true;
                    bAjoutMat1.Enabled = true;
                    bSupprMatos.Enabled = true;

                    //Chargement du materiel
                    ChargeListMateriel(ds.Tables[0].Rows[0]["IdAbonnement"].ToString());  
                }
                else   //On déactive les contrôles
                {
                    rBTypeBoitier1.Enabled = false;
                    rBTypeBoitier2.Enabled = false;
                    rBTypeBoitier3.Enabled = false;
                    comboBMateriel.Enabled = false;
                    listViewMat1.Enabled = false;
                    tBoxSupprMatos.Enabled = false;
                    bAjoutMat1.Enabled = false;
                    bSupprMatos.Enabled = false;
                }

                Desafection[0] = string.Empty;
                Desafection[1] = string.Empty;

                cBoxMotifChangement.Enabled = false;
                
                //*****************Fin Matériel*********************       
        
                //Chargement des autres n° de Tel du patient
                listViewTel.Enabled = true;
                
                //Chargement des N° de tel
                ChargeListTel(ds.Tables[0].Rows[0]["IdPersonne"].ToString());  

                //Chargement de la liste des médecins traitant depuis de dossier TA
                string[] TabListe;
                ArrayList listeMed = new ArrayList();
                string ListeMedTTT = ds.Tables[3].Rows[0]["TD_ListeMedecinsTTT"].ToString();
                TabListe = ListeMedTTT.Split('¤');
                listeMed = new ArrayList();

                foreach (string s in TabListe)
                {
                    if (s != "")
                    {
                        string[] medecin = OutilsExt.OutilsSql.GetMedecinTTT(int.Parse(s));
                        if (medecin != null && medecin.Length == 2)
                        {
                            AjouteMedTTT(int.Parse(s), medecin[0] + " " + medecin[1]);
                        }
                    }
                }

                //On selectionne le 1er de la liste
                if (lwMedTTT.Items.Count > 0)
                {
                    lwMedTTT.Items[0].Selected = true;
                    lwMedTTT.Select();
                }

                //Puis on en recherche le n° de tel pour l'afficher
                if (lwMedTTT.SelectedItems.Count > 0)
                {
                    if (lwMedTTT.SelectedItems[0].Tag != null)
                    {
                        txtOnglet3Tel.Text = TelMedecinTraitant(lwMedTTT.SelectedItems[0].Tag.ToString());
                    }
                }

                // Affichage des données sur la clé attribuée :
                txtNumCle.Text = ds.Tables[4].Rows[0]["NumeroCle"].ToString();
                txtCommentaireCle.Text = ds.Tables[4].Rows[0]["Commentaire"].ToString();

                lblNom.Text = "Nom : " + txtTa_Nom.Text + " " + txtTa_Prenom.Text;
                lblCle.Text = "Clé : " + txtNumCle.Text;
                lblContrat.Text = "Contrat : " + txtIdContrat.Text;
                LIdAbonnement.Text = "ID Abon.: " + ds.Tables[0].Rows[0]["IdAbonnement"].ToString();

                // Affichage du journal :
                for (int f = 0; f < ds.Tables[5].Rows.Count; f++)
                {
                    int nb = fpOnglet5_Sheet1.RowCount++;
                    fpOnglet5_Sheet1.Rows[nb].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                    fpOnglet5_Sheet1.Rows[nb].Tag = ds.Tables[5].Rows[f]["Id"].ToString();
                    fpOnglet5_Sheet1.Cells[nb, 0].Text = ds.Tables[5].Rows[f]["TypeOp"].ToString();
                    fpOnglet5_Sheet1.Cells[nb, 1].Text = ds.Tables[5].Rows[f]["EnvoiDe"].ToString();
                    fpOnglet5_Sheet1.Cells[nb, 2].Text = ds.Tables[5].Rows[f]["TexteA"].ToString();
                    fpOnglet5_Sheet1.Cells[nb, 3].Text = DateTime.Parse(ds.Tables[5].Rows[f]["DateOp"].ToString()).ToString();
                    fpOnglet5_Sheet1.Cells[nb, 4].Text = ds.Tables[5].Rows[f]["ICE"].ToString();
                    fpOnglet5_Sheet1.Cells[nb, 5].Text = ds.Tables[5].Rows[f]["NbCle"].ToString();
                    fpOnglet5_Sheet1.Cells[nb, 6].Text = ds.Tables[5].Rows[f]["Commentaire"].ToString();
                }
               
            }
        }

        public void AfficheContact(DataRow row)
        {
            if (row == null)
            {
                cbOnglet2Lien1.Text = "";
                txtOnglet2Nom1.Text = "";
                txtOnglet2PreNom1.Text = "";
                txtOnglet2NRue1.Text = "";
                txtOnglet2Adresse1.Text = "";
                txtOnglet2Localite1.Text = "";
                txtOnglet2Np1.Text = "";
                txtOnglet2Tel1.Text = "";
                txtOnglet2Tel1bis.Text = "";
                txtOnglet2Tel1ter.Text = "";

                //On gère l'état des boutons
                bNouveau1.Visible = true;
                bNouveau1.Enabled = true;
                bNouveau1.ImageIndex = 6;

                bSupprimer1.Enabled = false;
                bSupprimer1.ImageIndex = 1;

                bModifierEn1.Enabled = false;
                bModifierEn1.ImageIndex = 11;

            }
            else
            {
                cbOnglet2Lien1.Text = row["Lien"].ToString();
                txtOnglet2Nom1.Text = row["Nom"].ToString();
                txtOnglet2PreNom1.Text = row["Prenom"].ToString();
                txtOnglet2NRue1.Text = row["NumeroRue"].ToString();
                txtOnglet2Adresse1.Text = row["Rue"].ToString();
                txtOnglet2Localite1.Text = row["Localite"].ToString();
                txtOnglet2Np1.Text = row["Np"].ToString();
                txtOnglet2Tel1.Text = row["Telephone"].ToString();
                txtOnglet2Tel1bis.Text = row["Tel2"].ToString();
                txtOnglet2Tel1ter.Text = row["Tel3"].ToString();

                //On gère l'état des boutons                               
                bNouveau1.Visible = true;
                bNouveau1.Enabled = true;
                bNouveau1.ImageIndex = 6;
                bSupprimer1.Enabled = true;
                bSupprimer1.ImageIndex = 0;

                bModifierEn1.Enabled = true;
                bModifierEn1.ImageIndex = 10;
            }
        }

        public void AfficheUrgence(DataRow row)
        {
            if (row == null)
            {
                cbOnglet2Lien2.Text = "";
                txtOnglet2Nom2.Text = "";
                txtOnglet2PreNom2.Text = "";
                txtOnglet2NRue2.Text = "";
                txtOnglet2Adresse2.Text = "";
                txtOnglet2Localite2.Text = "";
                txtOnglet2Np2.Text = "";
                txtOnglet2Tel2.Text = "";
                txtOnglet2Tel2bis.Text = "";
                txtOnglet2Tel2ter.Text = "";

                //On gère l'état des boutons               
                bNouveau2.Visible = true;
                bNouveau2.Enabled = true;
                bNouveau2.ImageIndex = 6;

                bSupprimer2.Enabled = false;
                bSupprimer2.ImageIndex = 1;

                bModifierEn2.Visible = true;
                bModifierEn2.Enabled = false;
                bModifierEn2.ImageIndex = 11;
            }
            else
            {
                cbOnglet2Lien2.Text = row["Lien"].ToString();
                txtOnglet2Nom2.Text = row["Nom"].ToString();
                txtOnglet2PreNom2.Text = row["Prenom"].ToString();
                txtOnglet2NRue2.Text = row["NumeroRue"].ToString();
                txtOnglet2Adresse2.Text = row["Rue"].ToString();
                txtOnglet2Localite2.Text = row["Localite"].ToString();
                txtOnglet2Np2.Text = row["Np"].ToString();
                txtOnglet2Tel2.Text = row["Telephone"].ToString();
                txtOnglet2Tel2bis.Text = row["Tel2"].ToString();
                txtOnglet2Tel2ter.Text = row["Tel3"].ToString();

                bNouveau2.Visible = true;
                bNouveau2.Enabled = true;
                bNouveau2.ImageIndex = 6;

                bSupprimer2.Enabled = true;
                bSupprimer2.ImageIndex = 0;

                bModifierEn2.Visible = true;
                bModifierEn2.Enabled = true;
                bModifierEn2.ImageIndex = 10;
            }
        }

        #endregion

        #region Evenements

        //On enregistre les modifs
        private void bEnregistrer_Click(object sender, EventArgs e)
        {
            bool flag = true;   //Par defaut tout est ok
            int NvlAbonnement = 0;
            int Medicalerte = 0;

            if (tbAbonnement.Tag != null)
            {
                // Vérification des champs vide
                if (txtTa_Nom.Text == "")
                {
                    flag = false;
                    MessageBox.Show("Champs Nom Obligatoire");
                    return;
                }
                if (txtTa_Prenom.Text == "")
                {
                    flag = false;
                    MessageBox.Show("Champs Prénom Obligatoire");
                    return;
                }
                if (txtTa_Naissance.Text == "")
                {
                    flag = false;
                    MessageBox.Show("Champs Date de Naissance Obligatoire");
                    return;
                }
                if (EMaskTel1.Text.Replace("-", "").Replace("+", "") == "")
                {
                    flag = false;
                    MessageBox.Show("Champs Téléphone Obligatoire");
                    return;
                }
                if (!rdTa_Homme.Checked && !rdTa_Femme.Checked)
                {
                    flag = false;
                    MessageBox.Show("Champs Sexe obligatoire");
                    return;
                }

                if (!rBSexFactH.Checked && !rBSexFactF.Checked && !rBSexFactA.Checked)
                {
                    flag = false;
                    MessageBox.Show("Champs Sexe pour la Facturation obligatoire");
                    return;
                }

                if (txtTa_Adresse.Text == "")
                {
                    flag = false;
                    MessageBox.Show("Champs Rue obligatoire");
                    return;
                }

                int i;
                if (txtTa_Np.Text != "" && int.TryParse(txtTa_Np.Text, out i) == false)
                {
                    flag = false;
                    //Message d'erreur
                    MessageBox.Show("Champs Numéro Postal obligatoire ET SANS LETTRE");
                    return;
                }

                //pour le np de facturation                
                if (txtTa_FacNP.Text != "" && int.TryParse(txtTa_FacNP.Text, out i) == false)
                {
                    flag = false;
                    //Message d'erreur
                    MessageBox.Show("Champs Numéro Postal Facturation obligatoire ET SANS LETTRE");
                    return;
                }

                if (txtTa_Localite.Text == "")
                {
                    flag = false;
                    MessageBox.Show("Champs Localité obligatoire");
                    return;
                }

                if (txtNumCle.Text == "")
                {
                    flag = false;
                    MessageBox.Show("Champs Numéro Clé obligatoire");
                    return;
                }
                else
                {   //On verifie que la cle n'est pas déjà attribuée
                    string nom = OutilsExt.OutilsSql.NomSurCle(txtNumCle.Text);
                    if (nom != null && nom != "")
                    {
                        string nomFiche = txtTa_Nom.Text + ' ' + txtTa_Prenom.Text;
                        if (nom != nomFiche)
                        {
                            MessageBox.Show("Clé déjà attribuée à " + nom);
                            return;
                        }
                    }
                }

                if (txtIdContrat.Text == "")
                {
                    flag = false;
                    MessageBox.Show("Champs Numéro Contrat obligatoire.");
                    return;
                }
                else if (txtIdContrat.Text == "-1")
                {
                    flag = false;
                    MessageBox.Show("Il n'y a plus de matériel disponible...Impossible d'enregistrer cet abonnement.");
                    return;
                }

                //Est-ce un médicalerte?
                if (int.Parse(txtIdContrat.Text) >= 30000)
                {                   
                    Medicalerte = 1;                    
                }

                
                if (!rdPeriodFac0.Checked && !rdPeriodFac1.Checked && !rdPeriodFac2.Checked && !rdPeriodFac3.Checked && !rdPeriodFac4.Checked)
                {
                    flag = false;
                    MessageBox.Show("Choississez une périodicité de facturation");
                    return;
                }

                // Vérification du type de champs               
                try
                {
                    DateTime d = DateTime.Parse(txtTa_Naissance.Text);
                }
                catch
                {
                    flag = false;
                    MessageBox.Show("Champs Date de naissance non valide");
                    return;
                }

                
                DataSet ds;
                // Si c'est un premier enregistrement				
                if (tbAbonnement.Tag.ToString() == "-1")
                {
                    // S'il n'y a rien de stocké dans le bouton "Existant" c'est un tout nouvel enregistrement					
                    if (bVerif.Tag == null)
                        ds = OutilsExt.OutilsSql.NouveauAbonnement(-1, -1);
                    else
                        // Sinon on transmet les index de patient récupérés
                        ds = OutilsExt.OutilsSql.NouveauAbonnement(long.Parse(bVerif.Tag.ToString().Split('/')[1]), long.Parse(bVerif.Tag.ToString().Split('/')[0]));

                    if (ds != null)
                    {
                        tbAbonnement.Tag = ds.Tables[0].Rows[0]["IdAbonnement"].ToString();

                        flag = true;
                        string[] Ajout = { "Ajout" };

                        //Maj du materiel avec l'idAbonnement
                        if (Medicalerte == 1)
                        {
                            MajMateriel(txtIdContrat.Text, ds.Tables[0].Rows[0]["IdAbonnement"].ToString(), Ajout);
                        }
                    }

                    NvlAbonnement = 1;    //C'est un nvl abonnement
                }
                else
                {
                    // Sinon Maj
                    ds = OutilsExt.OutilsSql.RecupereAbonnement(int.Parse(tbAbonnement.Tag.ToString()), 0);       //Domi  07.11.2013 (2ème argument dans la fct)

                    string[] AjoutModules = { "AjoutModules" };

                    //Maj du materiel avec l'idAbonnement
                    if (Medicalerte == 1)
                    {
                        //On regarde s'il y a remplacement de box
                        if (Desafection[0] != string.Empty)
                        {
                            MajMateriel(txtIdContrat.Text, ds.Tables[0].Rows[0]["IdAbonnement"].ToString(), Desafection);
                        }
                        else
                            MajMateriel(txtIdContrat.Text, ds.Tables[0].Rows[0]["IdAbonnement"].ToString(), AjoutModules);

                    }

                    NvlAbonnement = 0;    //C'est une maj
                }

                if (flag == true)
                {
                    if (ds != null)
                    {
                        this.Cursor = Cursors.WaitCursor;

                        DataRow row = ds.Tables[0].Rows[0];
                        row["Nom"] = txtTa_Nom.Text;
                        row["Prenom"] = txtTa_Prenom.Text;
                        row["Tel"] = "+" + EMaskTel1.Text.Replace("-", "").Replace(" ", "").Replace("+","");
                        row["DateNaissance"] = txtTa_Naissance.Text;
                        row["IdContrat"] = txtIdContrat.Text;

                        //On test si nvll abonnement = 1     ou 0 => Maj
                        if (NvlAbonnement == 0)     //maj
                        {
                            if (checkBoxSansRappelTA.Enabled == false)
                            {
                                row["StopRappelTA"] = "'" + stopRappel + "'";
                            }
                            else
                            {
                                if (checkBoxSansRappelTA.Checked == false)
                                    row["StopRappelTA"] = DBNull.Value;
                                else
                                {
                                    if (stopRappel != "")
                                        row["StopRappelTA"] = "'" + stopRappel + "'";
                                    else row["StopRappelTA"] = "'" + DateTime.Now.ToString("dd.MM.yyyy") + "'";
                                }
                            }

                        }
                        else if (NvlAbonnement == 1)     //Nvll abonnement
                        {
                            if (checkBoxSansRappelTA.Enabled == false)
                            {
                                row["StopRappelTA"] = DBNull.Value;
                            }
                            else
                            {
                                if (checkBoxSansRappelTA.Checked == false)
                                    row["StopRappelTA"] = DBNull.Value;
                                else row["StopRappelTA"] = "'" + DateTime.Now.ToString("dd.MM.yyyy") + "'";
                            }

                            //Pour la facture de Médicalerte
                            if (rdOnglet3Type4.Checked)
                            {
                                row["ActiverFacture"] = 0;
                                bActiverFacture.Enabled = true;
                            }
                            else
                            {
                                row["ActiverFacture"] = 1;
                                bActiverFacture.Enabled = false;
                            }
                        }

                       
                        if (chkClePresente.Checked)
                            row["ClePresente"] = 1;
                        else
                            row["ClePresente"] = 0;
                        if (chkDossierBleu.Checked)
                            row["DossierBleu"] = 1;
                        else
                            row["DossierBleu"] = 0;
                        if (chkFaxFSASD.Checked)
                            row["FaxFsasd"] = 1;
                        else
                            row["FaxFsasd"] = 0;

                        if (rdTa_Femme.Checked)
                            row["Sexe"] = "F";
                        else if (rdTa_Homme.Checked)
                            row["Sexe"] = "H";

                        row["NumeroDansRue"] = txtTa_No.Text;
                        row["Rue"] = txtTa_Adresse.Text;
                        row["CodePostal"] = txtTa_Np.Text;
                        row["Commune"] = txtTa_Localite.Text;
                        row["Etage"] = txtTa_Etage.Text;
                        row["Escalier"] = txtTa_Escalier.Text;
                        row["Batiment"] = txtTa_Batiment.Text;
                        row["Porte"] = txtTa_Porte.Text;
                        row["Digicode"] = txtTa_Digicode.Text;
                        row["Internom"] = txtTa_Interphone.Text;
                        row["Commentaire"] = "";
                        row["DateDebutFacturation"] = dtDebutFacturation.Value;
                        row["TF_Nom"] = txtTa_FacNom.Text;
                        row["TF_Prenom"] = txtTa_FacPrenom.Text;
                        row["TF_NumeroPostal"] = txtTa_FacNP.Text;
                        row["TF_Localite"] = txtTa_FacLocalite.Text;
                        row["TF_Adresse"] = txtTa_FacAdresse.Text;
                        if (rBSexFactF.Checked)
                            row["TF_Sexe"] = "F";
                        if (rBSexFactH.Checked)
                            row["TF_Sexe"] = "H";
                        if (rBSexFactA.Checked)
                            row["TF_Sexe"] = "A";
                        row["N_TA"] = txtN_TA.Text;

                        //En fonction du type d'abonnement
                        if (rdOnglet3Type4.Checked)
                        {
                            if (rdPeriodFac0.Checked)
                                row["Periodicite"] = "MeM";
                            else if (rdPeriodFac1.Checked)
                                row["Periodicite"] = "MeT";
                            else if (rdPeriodFac2.Checked)
                                row["Periodicite"] = "MeA";
                            else if (rdPeriodFac4.Checked)
                                row["Periodicite"] = "MeS";
                            else if (rdPeriodFac3.Checked)
                                row["Periodicite"] = "B";    //Bloquée
                        }
                        else
                        {
                            if (rdPeriodFac0.Checked)
                                row["Periodicite"] = "M";
                            else if (rdPeriodFac1.Checked)
                                row["Periodicite"] = "T";
                            else if (rdPeriodFac2.Checked)
                                row["Periodicite"] = "A";
                            else if (rdPeriodFac4.Checked)
                                row["Periodicite"] = "S";
                            else if (rdPeriodFac3.Checked)
                                row["Periodicite"] = "B";    //Bloquée
                        }

                        /* else if (rdPeriodFac3.Enabled == false)
                         {
                             if (Bloquer == 1)
                                 row["Periodicite"] = "B";
                         }     */

                        //Pour la facture de Médicalerte
                        if (bActiverFacture.Enabled == true)
                        {
                            row["ActiverFacture"] = 0;
                        }
                        else
                            row["ActiverFacture"] = 1;          


                        if (cbOrdre.Enabled == false)
                        {
                            row["Ordre"] = OrdrePermanent;
                        }
                        else if (cbOrdre.Checked)
                            row["Ordre"] = 1;
                        else
                            row["Ordre"] = 0;


                        if (cbExport.Checked)
                            row["Export"] = 1;
                        else
                            row["Export"] = 0;

                        //****A supprimer?*****
                        row["ExportMcc"] = 0;
                        cbExporter.Checked = false;
                        cbExporter.Enabled = false;
                        //*************************
                        ds.Tables[1].Rows.Clear();
                        for (int jj = 0; jj < lwMemoire.Items.Count; jj++)
                        {
                            DataRow rowTable1 = ds.Tables[1].NewRow();
                            rowTable1.ItemArray = ((DataRow)lwMemoire.Items[jj].Tag).ItemArray;
                            ds.Tables[1].Rows.Add(rowTable1);
                        }
                        ds.Tables[2].Rows.Clear();
                        for (int jj = 0; jj < lwUrgence.Items.Count; jj++)
                        {
                            DataRow rowTable1 = ds.Tables[2].NewRow();
                            rowTable1.ItemArray = ((DataRow)lwUrgence.Items[jj].Tag).ItemArray;
                            ds.Tables[2].Rows.Add(rowTable1);
                        }

                        // Table contenant le dossier médical succint
                        ds.Tables[3].Rows[0]["TD_PbMedicaux"] = txtOnglet3Pb.Text;
                        ds.Tables[3].Rows[0]["TD_Traitements"] = txtOnglet3Medic.Text;
                        ds.Tables[3].Rows[0]["TD_Poids"] = txtOnglet3Poids.Text;
                        ds.Tables[3].Rows[0]["TD_Telephone"] = txtOnglet3Tel.Text;
                        ds.Tables[3].Rows[0]["TD_Attitudes"] = txtOnglet3Attitudes.Text;

                        if (chkOnglet3Risque.Checked)
                            ds.Tables[3].Rows[0]["TD_RisqueChute"] = "1";
                        else
                            ds.Tables[3].Rows[0]["TD_RisqueChute"] = "0";


                        if (chkOnglet3Fsasd.Checked)
                            ds.Tables[3].Rows[0]["TD_FSASD"] = "1";
                        else
                            ds.Tables[3].Rows[0]["TD_FSASD"] = "0";

                        if (chkOnglet3AutreServices.Checked)
                            ds.Tables[3].Rows[0]["TD_Autres_services"] = "1";
                        else
                            ds.Tables[3].Rows[0]["TD_Autres_services"] = "0";


                        //Utilisation du champ TD_FS_repas (obsolète) pour la gestion des sourds et malentendants
                        if (CB_Sourd.Checked)
                        {
                            ds.Tables[3].Rows[0]["TD_FS_repas"] = "1";

                            //Chaine de connection                            
                            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
                            SqlConnection dbConnection = new SqlConnection(connex);


                            //on ouvre la connexion
                            dbConnection.Open();

                            string sqlstr1 = "SELECT Idpatient, Medical from Patient_remarque";
                            sqlstr1 += " Where IdPatient = " + ds.Tables[0].Rows[0]["IdPatient"].ToString();

                            //On passe les parametres query et connection
                            SqlDataAdapter Query1 = new SqlDataAdapter(sqlstr1, dbConnection);

                            //on déclare le DataSet pour recevoir les diverses données
                            DataSet DSResult = new DataSet();

                            //on déclare une table pour cet ensemble de donnée
                            DSResult.Tables.Add("Patient_remarque");

                            //on execute
                            Query1.Fill(DSResult, "Patient_remarque");

                            //si trouvé
                            if (DSResult.Tables["Patient_remarque"].Rows.Count > 0)
                            {
                                //on rajoute la phrase dans Medical patient_remarque si elle n'y est pas déjà
                                string recherche = DSResult.Tables["Patient_remarque"].Rows[0]["Medical"].ToString();
                                bool reponse = recherche.StartsWith("\nMalentendant: se déplacer dans tous les cas.", System.StringComparison.CurrentCultureIgnoreCase);

                                if (reponse == false)
                                { //s'il n'y est pas, on ajoute le commentaire
                                    string remarqueMedicale = DSResult.Tables["Patient_remarque"].Rows[0]["Medical"].ToString();
                                    remarqueMedicale += "\n" + "Malentendant: se déplacer dans tous les cas.";
                                    remarqueMedicale = remarqueMedicale.ToString().Replace("'", "''");

                                    OutilsExt.OutilsSql.ExecuteCommandeSansRetour("Update Patient_remarque set Medical = '" + remarqueMedicale + "' where Idpatient = " + ds.Tables[0].Rows[0]["IdPatient"].ToString());

                                    mouchard.evenement("Modification de la fiche TA pour  " + txtTa_Nom.Text.ToString() + " " + txtTa_Prenom.Text.ToString() + ": Ajout remarque médicale sourd-malentendant.", VariablesApplicatives.Utilisateurs.NomUtilisateur.ToString());  //log
                                }
                            }
                            else
                            {
                                //sinon on rajoute un enregistrement dans la table patient_remarque
                                string remarqueM = "\nMalentendant: se déplacer dans tous les cas.";

                                //on définie la requette
                                string ajouteRemarque = "INSERT INTO patient_remarque";
                                ajouteRemarque += " ( IdPatient, Encaisse, Cession, Medical, Economique, Export, Archive, DateValidite, IdUtilisateur ) VALUES ( ";

                                ajouteRemarque += "'" + ds.Tables[0].Rows[0]["IdPatient"].ToString() + "'";
                                ajouteRemarque += ", 0";
                                ajouteRemarque += ", 0";
                                ajouteRemarque += ",'" + remarqueM + "'";
                                ajouteRemarque += ",''";
                                ajouteRemarque += ",0";
                                ajouteRemarque += ",0";
                                ajouteRemarque += ", getDate()";
                                ajouteRemarque += ",'" + SosMedecins.SmartRapport.GestionApplication.VariablesApplicatives.Utilisateurs.Identifiant + "'";
                                ajouteRemarque += " )";

                                //On execute la requette
                                OutilsExt.OutilsSql.ExecuteCommandeSansRetour(ajouteRemarque);

                                mouchard.evenement("Modification de la fiche TA pour  " + txtTa_Nom.Text.ToString() + " " + txtTa_Prenom.Text.ToString() + ": Ajout remarque médicale sourd-malentendant.", VariablesApplicatives.Utilisateurs.NomUtilisateur.ToString());  //log
                            }
                        }
                        else
                        {  //Pas coché
                            ds.Tables[3].Rows[0]["TD_FS_repas"] = "0";

                            //On va regardé si la chaine "Malentendant: se déplacer dans tous les cas." est dans 
                            //patient_remarquable

                            //Chaine de connection
                            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
                            SqlConnection dbConnection = new SqlConnection(connex);

                            //on ouvre la connexion
                            dbConnection.Open();

                            string sqlstr1 = "SELECT Idpatient, Medical from Patient_remarque";
                            sqlstr1 += " Where IdPatient = " + ds.Tables[0].Rows[0]["IdPatient"].ToString();

                            //On passe les parametres query et connection
                            SqlDataAdapter Query1 = new SqlDataAdapter(sqlstr1, dbConnection);

                            //on déclare le DataSet pour recevoir les diverses données
                            DataSet DSResult = new DataSet();

                            //on déclare une table pour cet ensemble de donnée
                            DSResult.Tables.Add("Patient_remarque");

                            //on execute
                            Query1.Fill(DSResult, "Patient_remarque");

                            //si trouvé
                            if (DSResult.Tables["Patient_remarque"].Rows.Count > 0)
                            {
                                //on ENLEVE la phrase dans Medical patient_remarque si elle n'y est.
                                string recherche = DSResult.Tables["Patient_remarque"].Rows[0]["Medical"].ToString();
                                bool reponse = recherche.StartsWith("\nMalentendant: se déplacer dans tous les cas.", System.StringComparison.CurrentCultureIgnoreCase);

                                if (reponse == true)
                                { //s'il y est, on vire le commentaire
                                    string remarqueMedicale = DSResult.Tables["Patient_remarque"].Rows[0]["Medical"].ToString();

                                    // Ici on spécifie que l'on ne veut pas tenir compte dela casse du
                                    // texte dans le remplacements.
                                    Regex maRegEx = new Regex("\nMalentendant: se déplacer dans tous les cas.", RegexOptions.IgnoreCase);

                                    // Remplacement de la chaine par des ""
                                    remarqueMedicale = maRegEx.Replace(remarqueMedicale, "");

                                    OutilsExt.OutilsSql.ExecuteCommandeSansRetour("Update Patient_remarque set Medical = '" + remarqueMedicale + "' where Idpatient = " + ds.Tables[0].Rows[0]["IdPatient"].ToString());

                                    mouchard.evenement("Modification de la fiche TA pour  " + txtTa_Nom.Text.ToString() + " " + txtTa_Prenom.Text.ToString() + ": Suppression de la remarque médicale sourd-malentendant.", VariablesApplicatives.Utilisateurs.NomUtilisateur.ToString());  //log
                                }
                            }
                        }


                        //Type de boitier 1 = Imad; 2 = Swisscom; 3 = Aucun; 4 = Medicalerte; 5 = Privé
                        if (rdOnglet3Type1.Checked)
                            ds.Tables[3].Rows[0]["TD_TypeAppareil"] = "1";
                        else if (rdOnglet3Type2.Checked)
                            ds.Tables[3].Rows[0]["TD_TypeAppareil"] = "2";
                        else if (rdOnglet3Type3.Checked)
                            ds.Tables[3].Rows[0]["TD_TypeAppareil"] = "3";
                        else if (rdOnglet3Type4.Checked)
                            ds.Tables[3].Rows[0]["TD_TypeAppareil"] = "4";
                        else if (rdOnglet3Type5.Checked)
                            ds.Tables[3].Rows[0]["TD_TypeAppareil"] = "5";

                        string ListeMedTTT = "";
                        for (int y = 0; y < lwMedTTT.Items.Count; y++)
                        {
                            ListeMedTTT += lwMedTTT.Items[y].Tag.ToString() + "¤";
                        }
                        if (ListeMedTTT.Length > 0) ListeMedTTT = ListeMedTTT.Remove(ListeMedTTT.Length - 1, 1);

                        ds.Tables[3].Rows[0]["TD_ListeMedecinsTTT"] = ListeMedTTT;

                        string ListeMedicaments = "";

                        ds.Tables[3].Rows[0]["TD_ListeMedicaments"] = ListeMedicaments;

                        // Sauvegarde de la clé :
                        ds.Tables[4].Rows[0]["NumeroCle"] = txtNumCle.Text;
                        ds.Tables[4].Rows[0]["Commentaire"] = txtCommentaireCle.Text;

                        if (OutilsExt.OutilsSql.SauvegardeAbonnement(ds, true))
                        {
                            this.Cursor = Cursors.Default;

                            //On ajoute le n° de tel dans Tel_personne s'il n'y est pas déjà
                            AjouteTel(ds.Tables[0].Rows[0]["IdPersonne"].ToString(), "+" + EMaskTel1.Text.Replace("-", "").Replace(" ","").Replace("+",""));

                            AjouteListTel(ds.Tables[0].Rows[0]["IdPersonne"].ToString());

                            //On supprime eventuellement des n° de Tel
                            SupprimeTel(ds.Tables[0].Rows[0]["IdPersonne"].ToString());

                            if (NvlAbonnement == 0)
                            {
                                MessageBox.Show("Modification réussie");
                                mouchard.evenement("Modification de la fiche TA pour  " + txtTa_Nom.Text.ToString() + " " + txtTa_Prenom.Text.ToString(), VariablesApplicatives.Utilisateurs.NomUtilisateur.ToString());  //log
                            }
                            else
                            {
                                MessageBox.Show("Ajout de la fiche TA réussie");
                                mouchard.evenement("Ajout de la fiche TA pour  " + txtTa_Nom.Text.ToString() + " " + txtTa_Prenom.Text.ToString(), VariablesApplicatives.Utilisateurs.NomUtilisateur.ToString());  //log
                            }

                            return;
                        }
                        else
                        {
                            this.Cursor = Cursors.Default;
                            MessageBox.Show("Modification impossible");
                            return;
                        }
                    }
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Sauvegarde impossible Verifiez les champs !!!");
                    return;
                }
            }
        }

        private void lwAbonne_DoubleClick(object sender, System.EventArgs e)
        {
            if (lwAbonne.SelectedIndices.Count > 0)
            {                
                // Affiche l'abonné TA sélectionné :
                long IdAbonnement = long.Parse(lwAbonne.Items[lwAbonne.SelectedIndices[0]].SubItems[4].Text);
                DataSet ds = OutilsExt.OutilsSql.RecupereAbonnement((int)IdAbonnement, 0);                       //Domi  07.11.2013 (2ème argument dans la fct)

                //on vide la liste des materiels
                dtNvxMateriel.Rows.Clear();

                //...et des Tel
                dtNvxTel.Rows.Clear();
                dtDelTel.Rows.Clear();
        
                AfficheAbonnement(ds);               
            }
        }

        // ******************************************************************
        // Déroulement de la fenetre d'attente de l'application
        // ******************************************************************
        public void Timer_Tick(object sender, EventArgs e)
        {
            if (OutilsExt.AttentActuelle.getValeur() >= 100)
                OutilsExt.AttentActuelle.setValeur(0);
            else
                OutilsExt.AttentActuelle.setValeur(OutilsExt.AttentActuelle.getValeur() + 10);
        }
        // ******************************************************************


        private void lwMemoire_Click(object sender, System.EventArgs e)
        {
            if (lwMemoire.SelectedIndices.Count > 0)
            {
                DataRow row = (DataRow)lwMemoire.Items[lwMemoire.SelectedIndices[0]].Tag;

                AfficheContact(row);
            }
        }


        public void AjouteMedTTT(int CodeMedTTT, string NomMed)
        {
            ListViewItem item = new ListViewItem(NomMed);
            item.Tag = CodeMedTTT;
            lwMedTTT.Items.Add(item);
        }

        #endregion

        private void btnVerifCle_Click(object sender, System.EventArgs e)
        {
            string nom = OutilsExt.OutilsSql.NomSurCle(txtNumCle.Text);
            if (nom != null && nom != "")
            {
                MessageBox.Show("Clé déjà attribuée à " + nom);
                return;
            }
            else
                MessageBox.Show("Clé disponible");
        }

        private void btnCopyAdresse_Click(object sender, System.EventArgs e)
        {
            txtTa_FacNom.Text = txtTa_Nom.Text;
            txtTa_FacPrenom.Text = txtTa_Prenom.Text;
            txtTa_FacNP.Text = txtTa_Np.Text;
            txtTa_FacLocalite.Text = txtTa_Localite.Text;
            //if(txtTa_Batiment.Text!="") txtTa_FacAdresse.Text = "Bat : " + txtTa_Batiment.Text + "\r\n";
            txtTa_FacAdresse.Text = txtTa_No.Text + " " + txtTa_Adresse.Text;
        }

        private void fpOnglet5_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            FarPoint.Win.Spread.Model.CellRange range = fpOnglet5.GetCellFromPixel(0, 0, e.X, e.Y);
            if (range.Row > -1)
            {
                fpOnglet5_Sheet1.SetActiveCell(range.Row, 0);

                gpJournal.Enabled = false;

                bValideJournal.ImageIndex = 3;
                bValideJournal.Enabled = false;
                bCancelJournal.ImageIndex = 5;
                bCancelJournal.Enabled = false;

                if (fpOnglet5_Sheet1.Cells[fpOnglet5_Sheet1.ActiveRowIndex, 0].Text == "cle")
                    rdOnglet5Cle.Checked = true;
                if (fpOnglet5_Sheet1.Cells[fpOnglet5_Sheet1.ActiveRowIndex, 0].Text == "dossier")
                    rdOnglet5Dossier.Checked = true;
                if (fpOnglet5_Sheet1.Cells[fpOnglet5_Sheet1.ActiveRowIndex, 0].Text == "annulation")
                    rdOnglet5Annulation.Checked = true;
                if (fpOnglet5_Sheet1.Cells[fpOnglet5_Sheet1.ActiveRowIndex, 0].Text == "Contrat")
                    rdOnglet5Retourcontrat.Checked = true;

                txtOnglet5EnvoiDe.Text = fpOnglet5_Sheet1.Cells[fpOnglet5_Sheet1.ActiveRowIndex, 1].Text;
                txtOnglet5EnvoiA.Text = fpOnglet5_Sheet1.Cells[fpOnglet5_Sheet1.ActiveRowIndex, 2].Text;
                dtOnglet5Le.Value = DateTime.Parse(fpOnglet5_Sheet1.Cells[fpOnglet5_Sheet1.ActiveRowIndex, 3].Text);
                txtOnglet5ICE.Text = fpOnglet5_Sheet1.Cells[fpOnglet5_Sheet1.ActiveRowIndex, 4].Text;
                txtOnglet5NbCle.Text = fpOnglet5_Sheet1.Cells[fpOnglet5_Sheet1.ActiveRowIndex, 5].Text;
                txtOnglet5Commentaire.Text = fpOnglet5_Sheet1.Cells[fpOnglet5_Sheet1.ActiveRowIndex, 6].Text;
            }
        }


        //On ajoute un nvx contact urgence
        private void bNouveau2_Click(object sender, EventArgs e)
        {
            ModifContactUrgent = 2;   //On est en ajout  
            
            cbOnglet2Lien2.Text = "";
            txtOnglet2Nom2.Text = "";
            txtOnglet2PreNom2.Text = "";
            txtOnglet2NRue2.Text = "";
            txtOnglet2Adresse2.Text = "";
            txtOnglet2Localite2.Text = "";
            txtOnglet2Np2.Text = "";
            txtOnglet2Tel2.Text = "";
            txtOnglet2Tel2bis.Text = "";
            txtOnglet2Tel2ter.Text = "";

            //On initialise l'état des boutons
            bNouveau2.Visible = false;
            bSupprimer2.ImageIndex = 1;
            bSupprimer2.Enabled = false;
            bModifierEn2.Visible = false;
        }


        //On supprime le contact Urgence
        private void bSupprimer2_Click(object sender, EventArgs e)
        {
            if (lwUrgence.SelectedIndices.Count > 0)
            {
                lwUrgence.Items.RemoveAt(lwUrgence.SelectedIndices[0]);

                cbOnglet2Lien2.Text = "";
                txtOnglet2Nom2.Text = "";
                txtOnglet2PreNom2.Text = "";
                txtOnglet2NRue2.Text = "";
                txtOnglet2Adresse2.Text = "";
                txtOnglet2Localite2.Text = "";
                txtOnglet2Np2.Text = "";
                txtOnglet2Tel2.Text = "";
                txtOnglet2Tel2bis.Text = "";
                txtOnglet2Tel2ter.Text = "";

                ModifContactUrgent = 0;   //ni en ajout ni en modif 
            }
        }


        //On annule les modifs contact Urgence
        private void bAnnuler2_Click(object sender, EventArgs e)
        {
            bNouveau2.Visible = true;
            bNouveau2.ImageIndex = 6;
            bNouveau2.Enabled = true;

            bSupprimer2.ImageIndex = 0;
            bSupprimer2.Enabled = true;

            bModifierEn2.Visible = true;
            bModifierEn2.Enabled = true;
            bModifierEn2.ImageIndex = 10;

            ModifContactUrgent = 0;   //ni en ajout ni en modif
        }


        //On valide le Nvx contact Urgence
        private void bValider2_Click(object sender, EventArgs e)
        {
            if (tbAbonnement.Tag == null) 
                return;

            // Vérification des champs vide
            if (cbOnglet2Lien2.Text == "")
            {
                MessageBox.Show("Champs Lien Obligatoire");
                return;
            }
            if (txtOnglet2Nom2.Text == "")
            {
                MessageBox.Show("Champs Nom Obligatoire");
                return;
            }
            if (txtOnglet2PreNom2.Text == "")
            {
                MessageBox.Show("Champs Prénom Obligatoire");
                return;
            }
            if (txtOnglet2Tel2.Text == "")
            {
                MessageBox.Show("Champs Téléphone Obligatoire");
                return;
            }

            if (ModifContactUrgent == 1)    //On est en modif
            {
                DataRow row = (DataRow)lwUrgence.Items[lwUrgence.SelectedIndices[0]].Tag;

                row["IdAbonnement"] = tbAbonnement.Tag.ToString();
                row["Lien"] = cbOnglet2Lien2.Text;
                row["Nom"] = txtOnglet2Nom2.Text;
                row["Prenom"] = txtOnglet2PreNom2.Text;
                row["Telephone"] = txtOnglet2Tel2.Text;
                row["Tel2"] = txtOnglet2Tel2bis.Text;
                row["Tel3"] = txtOnglet2Tel2ter.Text;
                row["NumeroRue"] = txtOnglet2NRue2.Text;
                row["Rue"] = txtOnglet2Adresse2.Text;
                row["Np"] = txtOnglet2Np2.Text;
                row["Localite"] = txtOnglet2Localite2.Text;

                ListViewItem item = lwUrgence.Items[lwUrgence.SelectedIndices[0]];
                item.Tag = row;
                item.SubItems[1].Text = txtOnglet2Nom2.Text + " " + txtOnglet2PreNom2.Text;
                item.SubItems[2].Text = txtOnglet2Tel2.Text;
            }
            else if (ModifContactUrgent == 2)
            {                
                DataTable dt = OutilsExt.OutilsSql.RecupereStructureUrgenceVierge();
                DataRow row = dt.NewRow();
                row["IdAbonnement"] = tbAbonnement.Tag.ToString();
                row["Lien"] = cbOnglet2Lien2.Text;
                row["Nom"] = txtOnglet2Nom2.Text;
                row["Prenom"] = txtOnglet2PreNom2.Text;
                row["Telephone"] = txtOnglet2Tel2.Text;
                row["Tel2"] = txtOnglet2Tel2bis.Text;
                row["Tel3"] = txtOnglet2Tel2ter.Text;
                row["NumeroRue"] = txtOnglet2NRue2.Text;
                row["Rue"] = txtOnglet2Adresse2.Text;
                row["Np"] = txtOnglet2Np2.Text;
                row["Localite"] = txtOnglet2Localite2.Text;

                ListViewItem item = new ListViewItem(cbOnglet2Lien2.Text);
                item.Tag = row;
                item.SubItems.Add(txtOnglet2Nom2.Text + " " + txtOnglet2PreNom2.Text);
                item.SubItems.Add(txtOnglet2Tel2.Text);
                lwUrgence.Items.Add(item);
            }
            else   //ni en ajout ni en modif
            {
                ModifContactUrgent = 0;
                return;
            }

            //On remet les boutons en place
            bNouveau2.Visible = true;
            bNouveau2.Enabled = true;
            bNouveau2.ImageIndex = 6;

            bSupprimer2.Enabled = false;
            bSupprimer2.ImageIndex = 1;

            bModifierEn2.Visible = true;
            bModifierEn2.Enabled = true;
            bModifierEn2.ImageIndex = 10;

            bSupprimer2.Enabled = true;
            bSupprimer2.ImageIndex = 0;

            ModifContactUrgent = 0; //ni en ajout ni en modif
        }

        //On modifie le contact Urgence
        private void bModifierEn2_Click(object sender, EventArgs e)
        {
            if (lwUrgence.SelectedIndices.Count > 0)
            {
                ModifContactUrgent = 1;   //On passe en modif
                
                //On gère l'état des boutons                               
                bNouveau2.Enabled = false;
                bNouveau2.Visible = false;

                bSupprimer2.Enabled = false;
                bSupprimer2.ImageIndex = 1;

                bModifierEn2.Enabled = false;
                bModifierEn2.Visible = false;

                bValider2.Enabled = true;
            }
        }

        private void lwUrgence_Click(object sender, System.EventArgs e)
        {
            if (lwUrgence.SelectedIndices.Count > 0)
            {
                DataRow row = (DataRow)lwUrgence.Items[lwUrgence.SelectedIndices[0]].Tag;

                AfficheUrgence(row);
            }
        }

        private void txtTa_Localite_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (lstCommunes.Items.Count > 0 && lstCommunes.Visible)
                {
                    lstCommunes.Focus();
                    lstCommunes.SelectedIndex = 0;
                }
            }
            else
            {
                if (txtTa_Localite.Text.Length >= 2)
                {

                    string sqlstr1;

                    //On charge la liste des localites
                    lstCommunes.Items.Clear();
                    lstCommunes.Visible = false;
                    lstCommunes.Tag = "COMMUNE";

                    //Chaine de connection
                    string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();

                    SqlConnection dbConnection = new SqlConnection(connex);
                    //SqlConnection dbConnection = new SqlConnection("Data Source=INFO-DOMI1\\SQLEXPRESS;Initial Catalog=BaseTest;User Id=sa; Password=prazine");

                    //on ouvre la connexion
                    dbConnection.Open();

                    //on définit la requette
                    sqlstr1 = "SELECT Distinct(Nom_NPA)";
                    sqlstr1 += " From adresses_officielles";
                    sqlstr1 += " where Nom_NPA like '" + txtTa_Localite.Text + "%'";
                    sqlstr1 += " order by Nom_NPA Desc";

                    //On passe les parametres query et connection
                    SqlDataAdapter Query1 = new SqlDataAdapter(sqlstr1, dbConnection);

                    //on déclare le DataSet pour recevoir les diverses données
                    DataSet DSResult = new DataSet();

                    //on déclare une table pour cet ensemble de donnée
                    DSResult.Tables.Add("Adresse");

                    //on execute
                    Query1.Fill(DSResult, "Adresse");

                    //si trouvé
                    if (DSResult.Tables["Adresse"].Rows.Count > 0)
                    {

                        for (int i = 0; i < DSResult.Tables["Adresse"].Rows.Count; i++)
                        {
                            lstCommunes.Items.Add(DSResult.Tables["Adresse"].Rows[i][0].ToString());
                        }

                    }

                    //On ferme les connctions
                    dbConnection.Close();

                    //on remet à blanc la chaine de connection
                    dbConnection = null;

                    if (lstCommunes.Items.Count > 0)
                    {
                        lstCommunes.Visible = true;
                        lstCommunes.Top = txtTa_Localite.Top + txtTa_Localite.Height + 1;
                        lstCommunes.Left = txtTa_Localite.Left;
                    }
                }
            }
        }

        private void txtTa_Adresse_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (lstCommunes.Items.Count > 0 && lstCommunes.Visible)
                {
                    lstCommunes.Focus();
                    lstCommunes.SelectedIndex = 0;
                }
            }
            else
            {
                if (txtTa_Localite.Text != "" && txtTa_Adresse.Text.Length >= 2)
                {

                    string sqlstr1;

                    //On charge la liste des localites
                    lstCommunes.Items.Clear();
                    lstCommunes.Visible = false;
                    lstCommunes.Tag = "RUE";

                    //Chaine de connection                    
                    string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
                    SqlConnection dbConnection = new SqlConnection(connex);

                    //on ouvre la connexion
                    dbConnection.Open();

                    //on définit la requette                 
                    sqlstr1 = "select TOP 1 Adresse from adresses_officielles";
                    // sqlstr1 += " where adresse like '%" + txtTa_Adresse.Text + "%'";
                    sqlstr1 += " where adresse like '%" + txtTa_Adresse.Text.Replace("'", "''") + "%'";
                    sqlstr1 += " order by adresse Desc";

                    //On passe les parametres query et connection
                    SqlDataAdapter Query1 = new SqlDataAdapter(sqlstr1, dbConnection);

                    //on déclare le DataSet pour recevoir les diverses données
                    DataSet DSResult = new DataSet();

                    //on déclare une table pour cet ensemble de donnée
                    DSResult.Tables.Add("Adresse");

                    //on execute
                    Query1.Fill(DSResult, "Adresse");

                    //si trouvé
                    if (DSResult.Tables["Adresse"].Rows.Count > 0)
                    {

                        for (int i = 0; i < DSResult.Tables["Adresse"].Rows.Count; i++)
                        {                           
                            string[] res = DSResult.Tables["Adresse"].Rows[i][0].ToString().Split(' ');
                            
                            string result1 = res[res.Length - 1];

                            string result2 = DSResult.Tables["Adresse"].Rows[i][0].ToString().Substring(0, DSResult.Tables["Adresse"].Rows[i][0].ToString().LastIndexOf(' '));
                            
                            lstCommunes.Items.Add(result2);
                        }

                    }

                    //On ferme les connctions
                    dbConnection.Close();

                    //on remet à blanc la chaine de connection
                    dbConnection = null;

                    if (lstCommunes.Items.Count > 0)
                    {
                        lstCommunes.Visible = true;
                        lstCommunes.Top = txtTa_Adresse.Top + txtTa_Adresse.Height + 1;
                        lstCommunes.Left = txtTa_Adresse.Left;
                    }
                }
            }
        }

        private void txtTa_Etage_Enter(object sender, System.EventArgs e)
        {

            //Si on a quelque chose dans l'adresse et localité...
            if (txtTa_Localite.Text != "" && txtTa_Adresse.Text != "")
            {
                string sqlstr1;

                if (txtTa_No.Text == "")
                {
                    MessageBox.Show("N° de rue vide");
                }

                //Chaine de connection                
                string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
                SqlConnection dbConnection = new SqlConnection(connex);

                //on ouvre la connexion
                dbConnection.Open();


                //on définit la requette
                sqlstr1 = "SELECT Longitude, Latitude,";
                sqlstr1 += "((CASE WHEN type_voie='' THEN '' ELSE type_voie + ' ' END)+";
                sqlstr1 += "(CASE WHEN Liant Like '%-' THEN Liant WHEN Liant='' THEN Liant ELSE Liant + ' ' END)+";
                sqlstr1 += " Nom_Voie + ' ' +Num_adresse) AS adresse";
                sqlstr1 += " From Adresses_officielles";
                sqlstr1 += " where adresse = '" + txtTa_Adresse.Text.Replace("'", "''") + ' ' + txtTa_No.Text + "'";
                sqlstr1 += " and (Nom_npa = '" + txtTa_Localite.Text + "'";
                sqlstr1 += " or commune = '" + txtTa_Localite.Text + "')";

                //On passe les parametres query et connection
                SqlDataAdapter Query1 = new SqlDataAdapter(sqlstr1, dbConnection);

                //on déclare le DataSet pour recevoir les diverses données
                DataSet DSResult = new DataSet();

                //on déclare une table pour cet ensemble de donnée
                DSResult.Tables.Add("Adresse");

                //on execute
                Query1.Fill(DSResult, "Adresse");
             
                //On ferme les connections
                dbConnection.Close();

                //on remet à blanc la chaine de connection
                dbConnection = null;
            }
        }

        private void lstCommunes_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return && lstCommunes.SelectedIndex > -1)
            {
                if (lstCommunes.Tag.ToString() == "COMMUNE")
                {
                    lstCommunes.Visible = false;
                    txtTa_Localite.Text = lstCommunes.SelectedItem.ToString();
                    txtTa_No.Focus();
                }
                if (lstCommunes.Tag.ToString() == "RUE")
                {
                    lstCommunes.Visible = false;
                    txtTa_Adresse.Text = lstCommunes.SelectedItem.ToString();
                    rdTa_Femme.Focus();
                }
            }
        }

        private void lstCommunes_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (lstCommunes.SelectedIndex > -1)
            {
                if (lstCommunes.Tag.ToString() == "COMMUNE")
                {
                    lstCommunes.Visible = false;
                    txtTa_Localite.Text = lstCommunes.SelectedItem.ToString();
                    txtTa_No.Focus();
                }
                if (lstCommunes.Tag.ToString() == "RUE")
                {
                    lstCommunes.Visible = false;
                    txtTa_Adresse.Text = lstCommunes.SelectedItem.ToString();
                    rdTa_Femme.Focus();
                }
            }
        }

        private void button2_Click(object sender, System.EventArgs e)
        {

            if (tbAbonnement.Tag != null)
            {

                int id = int.Parse(tbAbonnement.Tag.ToString());
                string[][] cle = OutilsExt.OutilsSql.ExecuteCommandeAvecTabString(RequetesSelect.ta_abonnement.IdAbonnement.Replace("%IdAbonnement", id.ToString()));

                long indicepat = -1;
                if (cle != null && cle.Length != 0 && cle[0][0] != "")
                {
                    indicepat = long.Parse(cle[0][0]);
                }

                FIP m_fip = new FIP(m_frmgeneral, indicepat, ImportSosGeneve.FIP.TypeOuverture.Patient);
                m_fip.ShowDialog();
                m_frmTa.Cursor = Cursors.Default;
                m_frmTa.Hide();

            }
            else
            {
                MessageBox.Show("Sélectionnez d'abord un abonnement ou bien sauvegardez celui en cours");
            }
        }


        private void bRechercher_Click(object sender, EventArgs e)
        {
            lwAbonne.Items.Clear();

            DataSet ds = null;

            int TypeArchive = -1;
            if (rdTriArchive1.Checked)
                TypeArchive = 0;
            else if (rdTriArchive2.Checked)
                TypeArchive = 1;
            else if (rdTriArchive3.Checked)
                TypeArchive = 2;

            if (txtFindContrat.Text != "")
            {
                ds = OutilsExt.OutilsSql.TrouveAbonnementByContrat(txtFindContrat.Text, TypeArchive);
            }
            else if (txtFind_Cle.Text != "")
            {
                ds = OutilsExt.OutilsSql.TrouveAbonnementByCle(txtFind_Cle.Text, TypeArchive);
            }
            else if (txtFind_Nom.Text != "")
            {
                ds = OutilsExt.OutilsSql.TrouveAbonnementByNom(txtFind_Nom.Text, TypeArchive);
            }           
            else if (textFindByNFacture.Text != "")
            {
                ds = OutilsExt.OutilsSql.TrouveAbonnementByNFacture(textFindByNFacture.Text, TypeArchive);
            }

            //****************Domi 09/03/2011

            else if (txtFind_Abonnement.Text != "" && IsNumeric(txtFind_Abonnement.Text) == true)
            {
                ds = OutilsExt.OutilsSql.RecupereAbonnement(int.Parse(txtFind_Abonnement.Text), TypeArchive);    //Domi  07.11.2013 (2ème argument dans la fct)
            }
            else if (txtFind_DateNaiss.Text != "")
            {
                //Pour la vérif de la date saisie
                bool DateOk = true;

                try
                {
                    DateTime d = DateTime.Parse(txtFind_DateNaiss.Text);
                }
                catch
                {
                    DateOk = false;
                }

                if (!DateOk)
                {
                    MessageBox.Show("Date de naissance de recherche non valide");
                    return;
                }
                else ds = OutilsExt.OutilsSql.TrouveAbonnementByDateNaiss(txtFind_DateNaiss.Text, TypeArchive);
            }
            else if (txtFind_Tel.Text != "")
            {
                ds = OutilsExt.OutilsSql.TrouveAbonnementByTel(txtFind_Tel.Text.Replace("-", "").Replace(" ",""), TypeArchive);
            }
            else if (cBoxFactBloque.Checked)
            {
                ds = OutilsExt.OutilsSql.TrouveAbonnementFactBloque(TypeArchive);
            }


            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ListViewItem item = new ListViewItem(ds.Tables[0].Rows[i]["Nom"].ToString() + " " + ds.Tables[0].Rows[i]["PreNom"].ToString());
                    item.SubItems.Add(ds.Tables[0].Rows[i]["Tel"].ToString());
                    if (ds.Tables[0].Rows[i]["DateNaissance"].ToString() != System.DBNull.Value.ToString())
                        item.SubItems.Add(DateTime.Parse(ds.Tables[0].Rows[i]["DateNaissance"].ToString()).ToString().Split(' ')[0]);
                    else
                        item.SubItems.Add("");
                    item.SubItems.Add(ds.Tables[0].Rows[i]["Commune"].ToString());
                    item.SubItems.Add(ds.Tables[0].Rows[i]["IdAbonnement"].ToString());
                    lwAbonne.Items.Add(item);
                }
            }
        }


        //On supprime la ligne du journal
        private void bSupprLigneJ_Click(object sender, EventArgs e)
        {
            if (fpOnglet5_Sheet1.ActiveRowIndex > -1 && fpOnglet5_Sheet1.ActiveColumnIndex > -1)
            {
                if (MessageBox.Show("Voulez vous supprimer cette opération ?", "Journal", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (fpOnglet5_Sheet1.RowCount == 0 || fpOnglet5_Sheet1.Rows[fpOnglet5_Sheet1.ActiveRowIndex] == null)
                    {
                        return;
                    }

                    string ligneJournal = fpOnglet5_Sheet1.Rows[fpOnglet5_Sheet1.ActiveRowIndex].Tag.ToString();

                    OutilsExt.OutilsSql.SupprimeLigneJournal(int.Parse(fpOnglet5_Sheet1.Rows[fpOnglet5_Sheet1.ActiveRowIndex].Tag.ToString()));

                    mouchard.evenement("Modification de la fiche TA pour  " + txtTa_Nom.Text.ToString() + " " + txtTa_Prenom.Text.ToString() + ": Suppression de la ligne du journal: " + ligneJournal, VariablesApplicatives.Utilisateurs.NomUtilisateur.ToString());  //log

                    fpOnglet5_Sheet1.Rows[fpOnglet5_Sheet1.ActiveRowIndex].Remove();

                    rdOnglet5Annulation.Checked = false;
                    rdOnglet5Cle.Checked = false;
                    rdOnglet5Dossier.Checked = false;
                    rdOnglet5Retourcontrat.Checked = false;
                    txtOnglet5Commentaire.Text = "";
                    txtOnglet5EnvoiA.Text = "";
                    txtOnglet5EnvoiDe.Text = "";
                    dtOnglet5Le.Value = DateTime.Now;
                    txtOnglet5ICE.Text = "";
                    txtOnglet5NbCle.Text = "";

                    gpJournal.Enabled = false;

                    bValideJournal.ImageIndex = 3;
                    bValideJournal.Enabled = false;
                    bCancelJournal.ImageIndex = 5;
                    bCancelJournal.Enabled = false;
                }
            }
        }


        //On ajoute une nouvelle ligne dans le journal
        private void bAjoutNvlLigneJ_Click(object sender, EventArgs e)
        {
            if (tbAbonnement.Tag != null && tbAbonnement.Tag.ToString() != "-1")
            {
                rdOnglet5Annulation.Checked = false;
                rdOnglet5Cle.Checked = false;
                rdOnglet5Dossier.Checked = false;
                rdOnglet5Retourcontrat.Checked = false;
                txtOnglet5Commentaire.Text = "";
                txtOnglet5EnvoiA.Text = "";
                txtOnglet5EnvoiDe.Text = "";
                dtOnglet5Le.Value = DateTime.Now;
                txtOnglet5ICE.Text = "";
                txtOnglet5NbCle.Text = "";

                gpJournal.Enabled = true;

                bValideJournal.ImageIndex = 2;
                bValideJournal.Enabled = true;
                bCancelJournal.ImageIndex = 4;
                bCancelJournal.Enabled = true;
            }
            else
            {
                MessageBox.Show("Sélectionner d'abord un abonnement ou bien sauvegardez celui en cours");
                return;
            }
        }


        //On valide le journal
        private void bValideJournal_Click(object sender, EventArgs e)
        {
            // Insertion de la ligne dans le tableau
            if (!rdOnglet5Annulation.Checked && !rdOnglet5Cle.Checked && !rdOnglet5Dossier.Checked && !rdOnglet5Retourcontrat.Checked)
            {
                MessageBox.Show("Veuillez sélectionner un type d'opération");
                return;
            }

            string typeop = "";
            if (rdOnglet5Cle.Checked)
                typeop = "cle";
            else if (rdOnglet5Dossier.Checked)
                typeop = "dossier";
            else if (rdOnglet5Annulation.Checked)
                typeop = "annulation";
            else if (rdOnglet5Retourcontrat.Checked)
                typeop = "contrat";

            int Id = -1;

            Id = OutilsExt.OutilsSql.InsereLigneJournal(int.Parse(tbAbonnement.Tag.ToString()), typeop, txtOnglet5EnvoiDe.Text, txtOnglet5EnvoiA.Text, dtOnglet5Le.Value, txtOnglet5ICE.Text, txtOnglet5NbCle.Text, txtOnglet5Commentaire.Text);

            if (Id > -1)
            {
                string ligneJournal = typeop + " " + txtOnglet5EnvoiDe.Text + " " + txtOnglet5EnvoiA.Text + " " + dtOnglet5Le.Value.ToString() + " " + txtOnglet5ICE.Text + " " + txtOnglet5NbCle.Text + " " + txtOnglet5Commentaire.Text;

                mouchard.evenement("Modification de la fiche TA pour  " + txtTa_Nom.Text.ToString() + " " + txtTa_Prenom.Text.ToString() + ": Ajout de la ligne du journal: " + ligneJournal, VariablesApplicatives.Utilisateurs.NomUtilisateur.ToString());  //log

                int nb = fpOnglet5_Sheet1.RowCount++;

                fpOnglet5_Sheet1.Rows[nb].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                fpOnglet5_Sheet1.Rows[nb].Tag = Id;
                fpOnglet5_Sheet1.Cells[nb, 0].Text = typeop;
                fpOnglet5_Sheet1.Cells[nb, 1].Text = txtOnglet5EnvoiDe.Text;
                fpOnglet5_Sheet1.Cells[nb, 2].Text = txtOnglet5EnvoiA.Text;
                fpOnglet5_Sheet1.Cells[nb, 3].Text = dtOnglet5Le.Value.ToString();
                fpOnglet5_Sheet1.Cells[nb, 4].Text = txtOnglet5ICE.Text;
                fpOnglet5_Sheet1.Cells[nb, 5].Text = txtOnglet5NbCle.Text;
                fpOnglet5_Sheet1.Cells[nb, 6].Text = txtOnglet5Commentaire.Text;

                rdOnglet5Annulation.Checked = false;
                rdOnglet5Cle.Checked = false;
                rdOnglet5Dossier.Checked = false;
                rdOnglet5Retourcontrat.Checked = false;
                txtOnglet5Commentaire.Text = "";
                txtOnglet5EnvoiA.Text = "";
                txtOnglet5EnvoiDe.Text = "";
                dtOnglet5Le.Value = DateTime.Now;
                txtOnglet5ICE.Text = "";
                txtOnglet5NbCle.Text = "";

                gpJournal.Enabled = false;

                bValideJournal.ImageIndex = 3;
                bValideJournal.Enabled = false;
                bCancelJournal.ImageIndex = 5;
                bCancelJournal.Enabled = false;
            }
            else
            {
                MessageBox.Show("Insertion de la ligne dans le journal impossible");
                return;
            }
        }


        private void bCancelJournal_Click(object sender, EventArgs e)
        {
            rdOnglet5Annulation.Checked = false;
            rdOnglet5Cle.Checked = false;
            rdOnglet5Dossier.Checked = false;
            rdOnglet5Retourcontrat.Checked = false;
            txtOnglet5Commentaire.Text = "";
            txtOnglet5EnvoiA.Text = "";
            txtOnglet5EnvoiDe.Text = "";
            dtOnglet5Le.Value = DateTime.Now;
            txtOnglet5ICE.Text = "";
            txtOnglet5NbCle.Text = "";

            gpJournal.Enabled = false;

            bValideJournal.ImageIndex = 3;
            bValideJournal.Enabled = false;
            bCancelJournal.ImageIndex = 5;
            bCancelJournal.Enabled = false;
        }


        //Ajouter un médecin traitant
        private void bAjoutMedecin_Click(object sender, EventArgs e)
        {
            frmAjoutDestinataire ajout = new frmAjoutDestinataire(this, -1, false, true);
            ajout.ShowDialog();
            ajout.Dispose();
        }

        //Supprimer un médecin traitant
        private void bSupprMedecin_Click(object sender, EventArgs e)
        {
            if (lwMedTTT.SelectedIndices.Count > 0)
            {
                lwMedTTT.Items.RemoveAt(lwMedTTT.SelectedIndices[0]);
            }
        }


        private void txtFindContrat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bRechercher_Click(sender, e);
            }

        }

        private void txtFind_Nom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bRechercher_Click(sender, e);
            }
        }

        private void txtFind_Cle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bRechercher_Click(sender, e);
            }
        }
        //******Domi 09/03/2011
        private void textFindByNFacture_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bRechercher_Click(sender, e);
            }
        }


        private void txtFind_Abonnement_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bRechercher_Click(sender, e);
            }
        }

        private void txtFind_Tel_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bRechercher_Click(sender, e);
            }
        }

        private void txtFind_DateNaiss_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bRechercher_Click(sender, e);
            }
        }
        //******* Hassan 10/03/2011

        private void bEtiquette_Click(object sender, EventArgs e)
        {
            ImportSosGeneve.Donnees.EtiquetteTA = new SosMedecins.SmartRapport.DAL.dsEtiquettes();
            string tt = txtTa_Nom.Text.ToString();
            DataRow rowEtiquette = ImportSosGeneve.Donnees.EtiquetteTA.dtEtiquettes.Rows.Add("+" + EMaskTel1.Text.Replace("-", "").Replace(" ", "").Replace("+",""), txtNumCle.Text, txtTa_Nom.Text, txtTa_Prenom.Text, txtTa_Adresse.Text, txtTa_Np.Text, txtTa_No.Text, txtTa_Localite.Text, txtN_TA.Text, txtTa_Digicode.Text);// = txtTa_Nom.Text;

            frmEtiquetteTA frmEtq = new frmEtiquetteTA(ImportSosGeneve.Donnees.EtiquetteTA);
            frmEtq.ShowDialog();
            frmEtq.Dispose();
            frmEtq = null;
        }

        //**************************
        private void bEnvoiMail_Click(object sender, EventArgs e)
        {
            //On envoi un mail si c'est un contrat IMAD
            if (rdOnglet3Type1.Checked)
            {
                string SujetMail = "Nouveau contrat TéléAlarme... Récupérer clés à l'Imad";
                string MessageMail = "Clé à récupérer pour le n° de PROM " + txtIdContrat.Text + " à l'Imad.";
                string ListeAdresse = "soslogistik@gmail.com; ckabbaj@sos-medecins.ch;" + VariablesApplicatives.Utilisateurs.EMail.ToString();

                if (SendMail(SujetMail, MessageMail, ListeAdresse, "", ""))
                {
                    mouchard.evenement("Envoi d'un mail pour récupérer la clé dont la PROM est " + txtIdContrat.Text, VariablesApplicatives.Utilisateurs.NomUtilisateur.ToString());  //log      
                    bEnvoiMail.ImageIndex = 9;
                    bEnvoiMail.Enabled = false;
                }
            }
            else if (rdOnglet3Type4.Checked)
            {
                string SujetMail = "Nouveau contrat Médicalerte";
                string MessageMail = "Boitier " + txtIdContrat.Text + " à paramétrer au nom de " + txtTa_Nom + " " + txtTa_Prenom.Text + "\n\r";
                MessageMail += " Id Abonnement: " + tbAbonnement.Tag.ToString();
                string ListeAdresse = "nadja.froidevaux@gmail.com; ckabbaj@sos-medecins.ch; informatique@sos-medecins.ch" + VariablesApplicatives.Utilisateurs.EMail.ToString();

                if (SendMail(SujetMail, MessageMail, ListeAdresse, "", ""))
                {
                    mouchard.evenement("Envoi d'un mail pour nvl abonnement médicalerte pour " + txtTa_Nom + " " + txtTa_Prenom.Text + "PROM: " + txtIdContrat.Text + " Id Abonnement: " + tbAbonnement.Tag.ToString(), VariablesApplicatives.Utilisateurs.NomUtilisateur.ToString());  //log      
                    bEnvoiMail.ImageIndex = 9;
                    bEnvoiMail.Enabled = false;
                }
            }
        }


        //******************Pour l'envoi des Mails****************************
        public bool SendMail(string Sujet, string message, string destinataires, string pieceJointe1, string pieceJointe2)
        {
            //Envoi de mail
            try
            {
                MailMessage Message1 = new MailMessage();
                Message1.From = new MailAddress(VariablesApplicatives.Utilisateurs.EMail.ToString());

                foreach (var adresse in destinataires.Split(';'))   //Pour ajouter plusieurs adresses
                {
                    if (adresse != "")
                        Message1.To.Add(new MailAddress(adresse));
                }

                /*Pour TEST*/
                //  Message1.To.Add(new MailAddress("dmercier@sos-Medecins.ch"));
                
                Message1.Subject = Sujet;
                Message1.IsBodyHtml = true;     //Tres important defini le type de corps du message...Ici en HTML                

                Message1.Body = message;
                // Message1.BodyEncoding = Encoding.GetEncoding("iso-8859-1");

                //Message1.Attachments.Add(new Attachment(pieceJointe1));  //Envoi d'une piece jointe (ici les contracts)
                //Message1.Attachments.Add(new Attachment(pieceJointe2));  //Envoi d'une autre piece jointe (ici les règlements)

                SmtpClient client = new SmtpClient("mail.sos-medecins.ch", 25);

                client.EnableSsl = false;
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("serveursms@sos-medecins.ch", "gimp38");  //login et pass du compte SMTP

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

        private void CtrlTA_Load(object sender, EventArgs e)
        {
            //Au chargement on regarde les droits des utilisateurs

            //en fonction des droits on désactive certains controles
            if (VariablesApplicatives.Utilisateurs.Droits == VariablesApplicatives.Utilisateurs.CodeDroits.Comptable
                || VariablesApplicatives.Utilisateurs.Droits == VariablesApplicatives.Utilisateurs.CodeDroits.Chef
                || VariablesApplicatives.Utilisateurs.Droits == VariablesApplicatives.Utilisateurs.CodeDroits.Admin)
            {
                this.checkBoxSansRappelTA.Enabled = true;
                this.cbOrdre.Enabled = true;
                this.rdPeriodFac3.Enabled = true;
                this.bsupprTel.Visible = true;
                this.bsupprTel.Enabled = true;
            }
            else
            {
                this.checkBoxSansRappelTA.Enabled = false;
                this.cbOrdre.Enabled = false;
                this.rdPeriodFac3.Enabled = false;
                this.bsupprTel.Visible = false;
                this.bsupprTel.Enabled = false;
            }

            //On initialise l'état de certains controles par défaut
            //On désactive les contrôles Materiels
            rBTypeBoitier1.Enabled = false;
            rBTypeBoitier2.Enabled = false;
            rBTypeBoitier3.Enabled = false;
            comboBMateriel.Enabled = false;
            listViewMat1.Enabled = false;
            tBoxSupprMatos.Enabled = false;
            bAjoutMat1.Enabled = false;
            bSupprMatos.Enabled = false;

            //et du tel
            listViewTel.Enabled = false;

        }


        //Gestion des Tels
        private void AjouteTel(string IdPersonne, string tel)
        {
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);

            try
            {
                dbConnection.Open();

                SqlCommand cmd = dbConnection.CreateCommand();
                cmd.Connection = dbConnection;

                //Gestion des n° de Tel...On regarde s'il existe dans la table Tel_Personne                                                                       
                string sqlstr0 = "SELECT NumPersonne, NumTel";
                sqlstr0 += " FROM Tel_Personne";
                sqlstr0 += " WHERE NumPersonne = @IdPersonne";
                sqlstr0 += " AND NumTel = @Tel";

                cmd.CommandText = sqlstr0;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("IdPersonne", IdPersonne);
                cmd.Parameters.AddWithValue("Tel", tel);

                DataTable Telephone = new DataTable();
                Telephone.Load(cmd.ExecuteReader());

                //Si on ne l'a pas trouvé, on l'ajoute à la table
                if (Telephone.Rows.Count == 0)
                {
                    sqlstr0 = "INSERT INTO Tel_Personne";
                    sqlstr0 += " VALUES(@NumPersonne, @Tel, GetDate())";

                    cmd.CommandText = sqlstr0;

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("NumPersonne", IdPersonne);
                    cmd.Parameters.AddWithValue("Tel", tel);

                    cmd.ExecuteReader();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(" Erreur : " + e.Message);
            }
        }

        private void bNouveau1_Click(object sender, EventArgs e)
        {
            ModifContact = 2;   //On est en ajout  
            
            //On vide les champs
            cbOnglet2Lien1.Text = "";
            txtOnglet2Nom1.Text = "";
            txtOnglet2PreNom1.Text = "";
            txtOnglet2NRue1.Text = "";
            txtOnglet2Adresse1.Text = "";
            txtOnglet2Localite1.Text = "";
            txtOnglet2Np1.Text = "";
            txtOnglet2Tel1.Text = "";
            txtOnglet2Tel1bis.Text = "";
            txtOnglet2Tel1ter.Text = "";

            //On initialise l'état des boutons
            bNouveau1.Visible = false;
            bSupprimer1.ImageIndex = 1;
            bSupprimer1.Enabled = false;
            bModifierEn1.Visible = false;
        }

        private void bValider1_Click(object sender, EventArgs e)
        {
            if (tbAbonnement.Tag == null) return;

            // Vérification des champs vide
            if (cbOnglet2Lien1.Text == "")
            {
                MessageBox.Show("Champs Lien Obligatoire");
                return;
            }
            if (txtOnglet2Nom1.Text == "")
            {
                MessageBox.Show("Champs Nom Obligatoire");
                return;
            }
            if (txtOnglet2PreNom1.Text == "")
            {
                MessageBox.Show("Champs Prénom Obligatoire");
                return;
            }
            if (txtOnglet2Tel1.Text == "")
            {
                MessageBox.Show("Champs Téléphone Obligatoire");
                return;
            }

            if (ModifContact == 1)   //On est en modif
            {
                DataRow row = (DataRow)lwMemoire.Items[lwMemoire.SelectedIndices[0]].Tag;
                row["IdAbonnement"] = tbAbonnement.Tag.ToString();
                row["Lien"] = cbOnglet2Lien1.Text;
                row["Nom"] = txtOnglet2Nom1.Text;
                row["Prenom"] = txtOnglet2PreNom1.Text;
                row["Telephone"] = "+" + txtOnglet2Tel1.Text.Replace("-", "");
                row["Tel2"] = "+" + txtOnglet2Tel1bis.Text.Replace("-", "");
                row["Tel3"] = "+" + txtOnglet2Tel1ter.Text.Replace("-", "");
                row["NumeroRue"] = txtOnglet2NRue1.Text;
                row["Rue"] = txtOnglet2Adresse1.Text;
                row["Np"] = txtOnglet2Np1.Text;
                row["Localite"] = txtOnglet2Localite1.Text;

                ListViewItem item = lwMemoire.Items[lwMemoire.SelectedIndices[0]];
                item.Tag = row;
                item.SubItems[1].Text = txtOnglet2Nom1.Text + " " + txtOnglet2PreNom1.Text;
                item.SubItems[2].Text = txtOnglet2Tel1.Text;
            }
            else if (ModifContact == 2)  //On est en ajout
            {
                DataTable dt = OutilsExt.OutilsSql.RecupereStructureContactVierge();
                DataRow row = dt.NewRow();
                row["IdAbonnement"] = tbAbonnement.Tag.ToString();
                row["Lien"] = cbOnglet2Lien1.Text;
                row["Nom"] = txtOnglet2Nom1.Text;
                row["Prenom"] = txtOnglet2PreNom1.Text;
                row["Telephone"] = "+" + txtOnglet2Tel1.Text.Replace("-", "");
                row["Tel2"] = "+" + txtOnglet2Tel1bis.Text.Replace("-", "");
                row["Tel3"] = "+" + txtOnglet2Tel1ter.Text.Replace("-", "");
                row["NumeroRue"] = txtOnglet2NRue1.Text;
                row["Rue"] = txtOnglet2Adresse1.Text;
                row["Np"] = txtOnglet2Np1.Text;
                row["Localite"] = txtOnglet2Localite1.Text;

                ListViewItem item = new ListViewItem(cbOnglet2Lien1.Text);
                item.Tag = row;
                item.SubItems.Add(txtOnglet2Nom1.Text + " " + txtOnglet2PreNom1.Text);
                item.SubItems.Add(txtOnglet2Tel1.Text);
                lwMemoire.Items.Add(item);
            }
            else   //ni en ajout ni en modif
            {
                ModifContact = 0;
                return;
            }

            //On remet les boutons en place
            bNouveau1.Visible = true;
            bNouveau1.Enabled = true;
            bNouveau1.ImageIndex = 6;

            bSupprimer1.Enabled = false;
            bSupprimer1.ImageIndex = 1;

            bModifierEn1.Visible = true;
            bModifierEn1.Enabled = true;
            bModifierEn1.ImageIndex = 10;

            bSupprimer1.Enabled = true;
            bSupprimer1.ImageIndex = 0;

            ModifContact = 0;
        }

        private void bModifierEn1_Click(object sender, EventArgs e)
        {
            if (lwMemoire.SelectedIndices.Count > 0)
            {              
                //On gère juste l'état des boutons                                               
                ModifContact = 1;    //On est en modif

                bNouveau1.Enabled = false;
                bNouveau1.Visible = false;

                bSupprimer1.Enabled = false;
                bSupprimer1.ImageIndex = 1;

                bModifierEn1.Enabled = false;
                bModifierEn1.Visible = false;

                bValider1.Enabled = true;
            }

        }

        private void bAnnuler1_Click(object sender, EventArgs e)
        {            
            bNouveau1.Visible = true;
            bNouveau1.ImageIndex = 6;
            bNouveau1.Enabled = true;

            bSupprimer1.ImageIndex = 0;
            bSupprimer1.Enabled = true;

            bModifierEn1.Visible = true;
            bModifierEn1.Enabled = true;
            bModifierEn1.ImageIndex = 10;

            ModifContact = 0;  //ni en ajout ni en modif 
        }

        private void bSupprimer1_Click(object sender, EventArgs e)
        {
            if (lwMemoire.SelectedIndices.Count > 0)
            {
                lwMemoire.Items.RemoveAt(lwMemoire.SelectedIndices[0]);

                cbOnglet2Lien1.Text = "";
                txtOnglet2Nom1.Text = "";
                txtOnglet2PreNom1.Text = "";
                txtOnglet2NRue1.Text = "";
                txtOnglet2Adresse1.Text = "";
                txtOnglet2Localite1.Text = "";
                txtOnglet2Np1.Text = "";
                txtOnglet2Tel1.Text = "";
                txtOnglet2Tel1bis.Text = "";
                txtOnglet2Tel1ter.Text = "";

                ModifContact = 0;   //ni en ajout ni en modif 
            }
        }

        private void bAnnuler_Click(object sender, EventArgs e)
        {
            AfficheAbonnement(null);
        }


        //Petite fonction pour teste IsNumeric
        public static bool IsNumeric(string ValeurATester)
        {
            Regex regex = new Regex(@"^[-+]?\d*[.,]?\d*$");
            return regex.IsMatch(ValeurATester);
        }

        private void lwMedTTT_SelectedIndexChanged(object sender, EventArgs e)
        {
            //On recupère le n° de tel du médecin traitant
            //On verifie qu'on a bien selectionné quelque chose
            if (lwMedTTT.SelectedItems.Count > 0)
            {
                if (lwMedTTT.SelectedItems[0].Tag != null)
                {
                    //Puis on en recherche le n° de tel pour l'afficher
                    txtOnglet3Tel.Text = TelMedecinTraitant(lwMedTTT.SelectedItems[0].Tag.ToString());
                }
            }

        }

        private void bNvlAbonnement_Click(object sender, EventArgs e)
        {
            NouveauAbonnement();
        }

        private void bModifier_Click(object sender, EventArgs e)
        {
            ModifieAbonnement();
        }

        private void bArchiver_Click(object sender, EventArgs e)
        {
            SupprimeAbonnement();   //En fait on l'archive
        }

        private void bDesarchiver_Click(object sender, EventArgs e)
        {
            DeSupprimeAbonnement();  //On le dé-archive
        }

        private string VerifArchive(string Abonnement)
        {
            //On recherche l'abonnement
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);

            dbConnection.Open();

            SqlCommand cmd = dbConnection.CreateCommand();

            string reponse = "Archivé";

            try
            {
                string sqlstr0 = "SELECT Archive FROM ta_abonnement WHERE IdAbonnement = @Abonnement";
                cmd.CommandText = sqlstr0;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("Abonnement", Abonnement);

                DataTable dtAbonnement = new DataTable();
                dtAbonnement.Load(cmd.ExecuteReader());

                //Si on l'a trouvé
                if (dtAbonnement.Rows.Count > 0)
                {
                    if (dtAbonnement.Rows[0][0].ToString() == "1")
                        reponse = "Archivé";
                    else reponse = "Non Archivé";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur Lors de la recherche de l'abonnement. Le message est: " + e.Message);
            }
            finally
            {
                if (dbConnection.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Close();
                }
            }
            return reponse;
        }

        private void bQuitterTA_Click(object sender, EventArgs e)
        {

        }

        private string TelMedecinTraitant(string NumMedecin)
        {
            //On recherche le médecin
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);

            dbConnection.Open();

            SqlCommand cmd = dbConnection.CreateCommand();

            string reponse = "";

            try
            {
                string sqlstr0 = "SELECT Telephone FROM medecinsville WHERE Num = @NumMedecin";
                cmd.CommandText = sqlstr0;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("NumMedecin", NumMedecin);

                DataTable dtMedecin = new DataTable();
                dtMedecin.Load(cmd.ExecuteReader());

                //Si on l'a trouvé
                if (dtMedecin.Rows.Count > 0)
                {
                    reponse = dtMedecin.Rows[0][0].ToString();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur Lors de la recherche de l'abonnement. Le message est: " + e.Message);
            }
            finally
            {
                if (dbConnection.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Close();
                }
            }
            return reponse;
        }


        private void rdOnglet3Type4_CheckedChanged(object sender, EventArgs e)
        {
            //Si c'est coché (SOS MEDECINS), on envoi pas d'Email pour le moment
            if (rdOnglet3Type4.Checked)
            {
                bEnvoiMail.ImageIndex = 9;
                bEnvoiMail.Enabled = false;
            }
        }

        private void lwMedTTT_DoubleClick(object sender, EventArgs e)
        {
            //Quand on clique sur 1 médecin, on l'édite

            //On verifie qu'on a bien selectionné quelque chose (sinon erreur) ET qu'on est en modif
            if (lwMedTTT.SelectedItems.Count > 0 && bAjoutMedecin.Enabled == true && bSupprMedecin.Enabled == true)
            {
                if (lwMedTTT.SelectedItems[0].Tag != null)
                {
                    //MessageBox.Show("on change : " + lwMedTTT.SelectedItems[0].Text.ToString() + " N° " + lwMedTTT.SelectedItems[0].Tag.ToString());
                    //on appelle la fiche médecins Traitant en remontant le n° du médecin
                    MedecinsTraitant = lwMedTTT.SelectedItems[0].Tag.ToString();     //Récup du n° du Médecin Traitant

                    //On appelle la forme pour modifier le médecin traitant
                    frmAjoutDestinataire ModifDest = new frmAjoutDestinataire(this, -1, true, true);
                    ModifDest.ShowDialog();
                    ModifDest.Dispose();

                    //On rafraichi l'abonnement                    
                    long IdAbonnement = long.Parse(lwAbonne.Items[lwAbonne.SelectedIndices[0]].SubItems[4].Text);
                    DataSet ds = OutilsExt.OutilsSql.RecupereAbonnement((int)IdAbonnement, 0);                       //Domi  07.11.2013 (2ème argument dans la fct)				

                    AfficheAbonnement(ds);

                    //On remet les boutons a deverouillés
                    ModifieAbonnement();
                }
            }
        }

        //On recherche la personne
        private void bVerif_Click(object sender, EventArgs e)
        {
            /*if (EMaskTel1.Text.Replace("-", "").Length != 11)
            {
                MessageBox.Show("Attention : Le numéro de téléphone doit comporter exactement 11 chiffres...Sans compter le +.");
                return;
            }*/
            
            
            //test de la date de naissance            
            DateTime dateNaiss;
            if (DateTime.TryParseExact(txtTa_Naissance.Text, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture,
                DateTimeStyles.None, out dateNaiss) == false)
            {
                MessageBox.Show("La Date de naissance n'a pas le bon format. ");
                return;
            }
         
            frmRecherchePersonne recherche = new frmRecherchePersonne(dateNaiss);
            recherche.ShowDialog();

            if (recherche.Personne != null && recherche.PatientAccepte)
            {
                txtTa_Nom.Text = recherche.Personne["NomPatient"].ToString();
                txtTa_Prenom.Text = recherche.Personne["PrenomPatient"].ToString();
                txtTa_Naissance.Text = DateTime.Parse(recherche.Personne["DateNaissance"].ToString()).ToString("dd.MM.yyyy");
                txtTa_No.Text = recherche.Personne["NumeroDansRue"].ToString();
                txtTa_Np.Text = recherche.Personne["CodePostal"].ToString();
                txtTa_Adresse.Text = recherche.Personne["Rue"].ToString();
                txtTa_Localite.Text = recherche.Personne["Commune"].ToString();

                if (recherche.Personne["StopRappelTA"].ToString() != "")
                    checkBoxSansRappelTA.Checked = true;
                if (recherche.Personne["Sexe"].ToString() == "H" || recherche.Personne["Sexe"].ToString() == "M")
                    rdTa_Homme.Checked = true;
                if (recherche.Personne["Sexe"].ToString() == "F")
                    rdTa_Femme.Checked = true;

                if (recherche.Personne["Batiment"].ToString() != "0")
                    txtTa_Batiment.Text = recherche.Personne["Batiment"].ToString();
                txtTa_Escalier.Text = recherche.Personne["Escalier"].ToString();
                txtTa_Etage.Text = recherche.Personne["Etage"].ToString();
                txtTa_Digicode.Text = recherche.Personne["Digicode"].ToString();
                txtTa_Interphone.Text = recherche.Personne["Internom"].ToString();
                txtTa_Porte.Text = recherche.Personne["Porte"].ToString();

                bVerif.Tag = recherche.IdPatient + "/" + recherche.IdPersonne;

                //On charge la liste des N° de Tel du patient
                ChargeListTel(recherche.Personne["IdPersonne"].ToString());

            }
            recherche.Dispose();
            recherche = null;
        }

        private void rdOnglet3Type4_CheckedChanged_1(object sender, EventArgs e)
        {
            //on défini IDContrat (!= IMAD) = PROM
            if (rdOnglet3Type4.Checked)
            {
                rBTypeBoitier1.Enabled = true;
                rBTypeBoitier2.Enabled = true;
                rBTypeBoitier3.Enabled = true;
                comboBMateriel.Enabled = true;
                listViewMat1.Enabled = true;
                tBoxSupprMatos.Enabled = true;
                bAjoutMat1.Enabled = true;
                bSupprMatos.Enabled = true;               
            }
            else   //On déactive les contrôles
            {
                rBTypeBoitier1.Enabled = false;
                rBTypeBoitier2.Enabled = false;
                rBTypeBoitier3.Enabled = false;
                comboBMateriel.Enabled = false;
                listViewMat1.Enabled = false;
                tBoxSupprMatos.Enabled = false;
                bAjoutMat1.Enabled = false;
                bSupprMatos.Enabled = false;
            }
        }

        private void rdOnglet3Type2_Click(object sender, EventArgs e)
        {
            //Si c'est une nouvel abonnement, on défini IDContrat (!= IMAD)
            if (tbAbonnement.Tag != null && tbAbonnement.Tag.ToString() == "-1")
            {
                if (rdOnglet3Type2.Checked)
                {
                    //On met 0
                    txtIdContrat.Text = "0";
                }
            }
        }

        private void rdOnglet3Type5_CheckedChanged(object sender, EventArgs e)
        {
            //Si c'est une nouvel abonnement, on défini IDContrat (!= IMAD)
            if (tbAbonnement.Tag != null && tbAbonnement.Tag.ToString() == "-1")
            {
                if (rdOnglet3Type5.Checked)
                {
                    //On met 0
                    txtIdContrat.Text = "0";
                }
            }
        }

        private void rdOnglet3Type3_CheckedChanged(object sender, EventArgs e)
        {
            //Si c'est une nouvel abonnement, on défini IDContrat (!= IMAD)
            if (tbAbonnement.Tag != null && tbAbonnement.Tag.ToString() == "-1")
            {
                if (rdOnglet3Type3.Checked)
                {
                    //On met 0
                    txtIdContrat.Text = "0";
                }
            }
        }



        public void VerrouilleControls()
        {
            foreach (Control c in tbAbonnement.Controls)
            {
                if (c.GetType() == typeof(TextBox))
                {
                    ((TextBox)c).ReadOnly = true;
                }
            }


            bVerif.ImageIndex = 19;
            bVerif.Enabled = false;
            bVerif.Tag = null;

            bAjoutNvlLigneJ.ImageIndex = 7;
            bAjoutNvlLigneJ.Enabled = false;
            bSupprLigneJ.ImageIndex = 1;
            bSupprLigneJ.Enabled = false;

            bValideJournal.ImageIndex = 3;
            bValideJournal.Enabled = false;
            bCancelJournal.ImageIndex = 5;
            bCancelJournal.Enabled = false;

            bEnvoiMail.ImageIndex = 9;
            bEnvoiMail.Enabled = false;


            //Pour l'état des boutons
            bNouveau1.Visible = true;
            bNouveau1.Enabled = false;
            bNouveau1.ImageIndex = 7;

            bSupprimer1.Enabled = false;
            bSupprimer1.ImageIndex = 1;

            bModifierEn1.Visible = true;
            bModifierEn1.Enabled = false;
            bModifierEn1.ImageIndex = 11;

            bNouveau2.Visible = true;
            bNouveau2.Enabled = false;
            bNouveau2.ImageIndex = 7;

            bSupprimer2.Enabled = false;
            bSupprimer2.ImageIndex = 1;

            bModifierEn2.Visible = true;
            bModifierEn2.Enabled = false;
            bModifierEn2.ImageIndex = 11;

            bAjoutMedecin.ImageIndex = 7;
            bAjoutMedecin.Enabled = false;
            bSupprMedecin.ImageIndex = 1;
            bSupprMedecin.Enabled = false;

            groupBox1.Enabled = false;
            groupBox2.Enabled = false;
            groupBox3.Enabled = false;        

            //Materiel           
            rBTypeBoitier1.Enabled = false;
            rBTypeBoitier2.Enabled = false;
            rBTypeBoitier3.Enabled = false;
            comboBMateriel.Enabled = false;
            listViewMat1.Enabled = false;
            tBoxSupprMatos.Enabled = false;
            bAjoutMat1.Enabled = false;
            bSupprMatos.Enabled = false;
                    
            //liste N° de Tel
            listViewTel.Enabled = false;

        }


        //Mise à jour de la table Materiel avec l'IDAbonnement
        public void MajMateriel(string ContactID, string IdAbonnement, string[] TypeOpe)
        {
            //On complete le ContactID avec des 0
            string CID = Complete(ContactID, 8);
                  
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);

            dbConnection.Open();

            SqlCommand cmd = dbConnection.CreateCommand();

           // SqlTransaction transaction;             //Pour l'insert avec transaction

           // transaction = dbConnection.BeginTransaction("transac");     //Démarre une transaction locale

            string sqlstr0 = "";

            try
            {
                cmd.Connection = dbConnection;
               // cmd.Transaction = transaction;    //On affecte la transaction

                if (TypeOpe[0] == "Ajout")    //Ajout d'une Box ET du médaillon attaché à cette derniere
                {
                    sqlstr0 = "Update TA_Materiel ";
                    sqlstr0 += " Set IdAbonnement = @Abonnement, ";
                    sqlstr0 += " DateMES = case when DateMES is null then getdate() else DateMES end, DateDerniereAttrib = getdate()";
                    sqlstr0 += " WHERE ContactID = @IdContact";

                    cmd.CommandText = sqlstr0;

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("Abonnement", IdAbonnement);
                    cmd.Parameters.AddWithValue("IdContact", CID);

                    cmd.ExecuteNonQuery();
                }
                else if (TypeOpe[0] == "Boitier HS")
                {
                    //Suppression du luna (sans son médaillon) Pour attribution d'un autre du même type
                    sqlstr0 = "Update TA_Materiel ";
                    sqlstr0 += " Set IdAbonnement = Null, ";
                    sqlstr0 += " DateHS = case when DateHS is null then getdate() else DateHS end, DateDerniereAttrib = getdate()";
                    sqlstr0 += " WHERE ContactID = @IdContact";
                    sqlstr0 += " AND Type_tarif in ('L3GSL', 'L3G', 'L4')";

                    cmd.CommandText = sqlstr0;

                    cmd.Parameters.Clear();                    
                    cmd.Parameters.AddWithValue("IdContact", TypeOpe[1]);

                    cmd.ExecuteNonQuery();

                    //Puis ajout de la nouvelle box (sans le médaillon associé)
                    sqlstr0 = "Update TA_Materiel ";
                    sqlstr0 += " Set IdAbonnement = @Abonnement, ";
                    sqlstr0 += " Type_tarif = 'R', DateDerniereAttrib = getdate(), ";
                    sqlstr0 += " DateMES = case when DateMES is null then getdate() else DateMES end";
                    sqlstr0 += " WHERE ContactID = @IdContact";
                    sqlstr0 += " AND Type_tarif in ('L3GSL', 'L3G', 'L4')";

                    cmd.CommandText = sqlstr0;

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("Abonnement", IdAbonnement);
                    cmd.Parameters.AddWithValue("IdContact", CID);

                    cmd.ExecuteNonQuery();

                    //Les médaillons:                   
                    //On déassocie le médaillon de la NVLLE box de tout: ContactId = 00000000)
                    sqlstr0 = "Update TA_Materiel ";
                    sqlstr0 += " Set ContactID = '00000000'";
                    sqlstr0 += " WHERE ContactID = @IdContact";
                    sqlstr0 += " AND Type_tarif in ('M4')";

                    cmd.CommandText = sqlstr0;

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("IdContact", CID);          

                    cmd.ExecuteNonQuery();

                    //On affecte l'ANCIEN médaillon à la nouvelle box
                    sqlstr0 = "Update TA_Materiel ";
                    sqlstr0 += " Set ContactID = @IdContact ";
                    sqlstr0 += " WHERE IdAbonnement = @Abonnement";
                    sqlstr0 += " AND Type_tarif in ('M4')";

                    cmd.CommandText = sqlstr0;

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("IdContact", CID);
                    cmd.Parameters.AddWithValue("Abonnement", IdAbonnement);

                    cmd.ExecuteNonQuery();
                }
                else if (TypeOpe[0] == "Changement de version" || TypeOpe[0] == "Réaffectation de boitier")
                {
                    //Suppression du luna (sans son médaillon) Pour attribution d'un autre modèle de boitier
                    sqlstr0 = "Update TA_Materiel ";
                    sqlstr0 += " Set IdAbonnement = Null, ";
                    sqlstr0 += " DateDerniereAttrib = getdate()";
                    sqlstr0 += " WHERE ContactID = @IdContact";
                    sqlstr0 += " AND Type_tarif in ('L3GSL', 'L3G', 'L4')";

                    cmd.CommandText = sqlstr0;

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("IdContact", TypeOpe[1]);

                    cmd.ExecuteNonQuery();

                    //Puis ajout de la nouvelle box
                    sqlstr0 = "Update TA_Materiel ";
                    sqlstr0 += " Set IdAbonnement = @Abonnement, ";
                    sqlstr0 += " DateMES = case when DateMES is null then getdate() else DateMES end, DateDerniereAttrib = getdate()";
                    sqlstr0 += " WHERE ContactID = @IdContact";
                    sqlstr0 += " AND Type_tarif in ('L3GSL', 'L3G', 'L4')";

                    cmd.CommandText = sqlstr0;

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("Abonnement", IdAbonnement);
                    cmd.Parameters.AddWithValue("IdContact", CID);

                    cmd.ExecuteNonQuery();
                }
                else if (TypeOpe[0] == "Archive")
                {
                    //Suppression du luna...remise en stock avec son médaillon
                    sqlstr0 = "Update TA_Materiel ";
                    sqlstr0 += " Set IdAbonnement = Null ";
                    sqlstr0 += " WHERE IdAbonnement = @Abonnement";                    

                    cmd.CommandText = sqlstr0;

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("Abonnement", IdAbonnement);

                    cmd.ExecuteNonQuery();
                }

                if (TypeOpe[0] != "Archive")
                {
                    //Puis les modules (s'il y en a...)
                    for (int i = 0; i < dtNvxMateriel.Rows.Count; i++)
                    {
                        sqlstr0 = "Update TA_Materiel ";
                        //sqlstr0 += " Set IdAbonnement = @Abonnement, ContactID = @ContactID, ";
                        //sqlstr0 += ", DateHS = case when DateHS is null then @DateJ else DateHS end";
                        sqlstr0 += " Set IdAbonnement = @Abonnement, ";
                        sqlstr0 += " DateMES = case when DateMES is null then getdate() else DateMES end, DateDerniereAttrib = getdate()";                       
                        sqlstr0 += " WHERE VID = @VID";

                        cmd.CommandText = sqlstr0;

                        cmd.Parameters.Clear();
                        if (dtNvxMateriel.Rows[i]["IdAbonnement"].ToString() == "-1")
                        {
                            cmd.Parameters.AddWithValue("Abonnement", DBNull.Value);
                           // cmd.Parameters.AddWithValue("DateJ", DateTime.Now);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("Abonnement", IdAbonnement);
                           // cmd.Parameters.AddWithValue("DateJ", DBNull.Value);
                        }

                       // cmd.Parameters.AddWithValue("ContactID", CID);
                        cmd.Parameters.AddWithValue("VID", dtNvxMateriel.Rows[i]["VID"].ToString());

                        cmd.ExecuteNonQuery();
                    }
                }
                else
                { 
                    //On met aucun au materiel dans le TA
                    sqlstr0 = "Update ta_abonnementdossier ";
                    sqlstr0 += " Set TD_TypeAppareil = 3 ";
                    sqlstr0 += " WHERE IdAbonnement = @Abonnement";

                    cmd.CommandText = sqlstr0;

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("Abonnement", IdAbonnement);

                    cmd.ExecuteNonQuery();
                }
                            
                //On modifie le check Materiel
                if (rBTypeBoitier1.Checked)
                    AncienCheck = 1;
                else if (rBTypeBoitier2.Checked)
                    AncienCheck = 2;
                else if (rBTypeBoitier3.Checked)
                    AncienCheck = 3;
                else AncienCheck = 0;

            }
            catch (Exception ex)
            {               
                //On gère ici toute kes erreurs qui ont pu survenir pour empêcher le Rollback...
                //comme par exemple une connexion fermée...
                Console.WriteLine("Rollback Exeption Type: {0}", ex.GetType());
                Console.WriteLine("   Message: {0}", ex.Message);
                MessageBox.Show("Erreur Lors de la mise à jour de la table matériel. Le message est: " + ex.Message);
                
            }
            finally
            {
                if (dbConnection.State == System.Data.ConnectionState.Open)
                    dbConnection.Close();
            }
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            //En fonction de l'onglet actif
            if (tabControl1.SelectedTab.Name == "tbJournal")
            {
                if (tbAbonnement.Tag != null && tbAbonnement.Tag.ToString() == "-1")
                {
                    bEnregistrer_Click(sender, e);
                }
            }            
        }

        private void bAjoutMat1_Click(object sender, EventArgs e)
        {
            //On ajoute le matériel à la liste
            //Mettre dans le dataset le contenu de la list box (recherche à partir du libellé)
            //Recherche du produit
            if (tBoxNumSerie.Text != "")
            {
                DataTable Matos = new DataTable();
                Matos = RetourneProduit(comboBMateriel.Text, tBoxNumSerie.Text);

                if (Matos.Rows.Count > 0 && Matos.Rows[0][0] != DBNull.Value)
                {
                    //On rempli le DataTable(Pour enregistrer par la suite) et la liste view                
                    dtNvxMateriel.Rows.Add(Matos.Rows[0]["VID"].ToString(), Matos.Rows[0]["Libelle"].ToString(), Matos.Rows[0]["IdAbonnement"].ToString());

                    //On l'ajoute dans la liste View
                    ListViewItem item1 = new ListViewItem(Matos.Rows[0]["Libelle"].ToString());
                    item1.SubItems.Add(Matos.Rows[0]["VID"].ToString());
                    listViewMat1.Items.Add(item1);

                    //On vide et desactive le champs n° de serie
                    tBoxNumSerie.Text = "";
                    tBoxNumSerie.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Désolé ce modèle n'existe pas ou il n'est plus en stock.", "Stock Materiel", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            else
                MessageBox.Show("Vous devez rentrer le n° de serie de l'appareil à affecter", "Affectation d'un appareil", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void comboBMateriel_SelectedValueChanged(object sender, EventArgs e)
        {
            //On ouvre le panneau pour saisir le n° de serie de l'appareil à attribuer
            tBoxNumSerie.Enabled = true;
        }

        public DataTable RetourneProduit(string Libelle, string NumSerie)
        {
            //On commence par lister ce qui est dans la dtNvxMateriel
            string Liste = "";

            foreach (DataRow row in dtNvxMateriel.Rows)
            {
                if (Liste.Length == 0)
                    Liste = "'" + row["VID"].ToString() + "'";  
                else
                    Liste = Liste + ",'" + row["VID"].ToString() + "'";
            }

            
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);

            dbConnection.Open();

            SqlCommand cmd = dbConnection.CreateCommand();

            DataTable Matos = new DataTable();

            try
            {
                string sqlstr0 = "Select * FROM TA_Materiel ";
                sqlstr0 += " WHERE Libelle = @Libelle";
                sqlstr0 += " AND VID = @NumSerie";
                sqlstr0 += " AND IdAbonnement is Null";
                sqlstr0 += " AND DateHS is null";
                
                if (Liste.Length > 0)
                    sqlstr0 += " AND VID NOT IN (" + Liste + ")";

                cmd.CommandText = sqlstr0;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("Libelle", Libelle);
                cmd.Parameters.AddWithValue("NumSerie", NumSerie);

                Matos.Load(cmd.ExecuteReader());

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur Lors de la recherche dans la table matériel. Le message est: " + ex.Message);
            }

            return Matos;
        }

      

        private void bSupprMatos_Click(object sender, EventArgs e)
        {
            //On le marque dans DataTable(Pour enregistrer par la suite)                    
            Console.WriteLine(listViewMat1.Items[listViewMat1.SelectedIndices[0]].SubItems[1].Text);

            foreach (DataRow row in dtNvxMateriel.Select("VID= '" + listViewMat1.Items[listViewMat1.SelectedIndices[0]].SubItems[1].Text + "'"))
            {
                row["IdAbonnement"] = "-1";   //Donc en panne
            }

            dtNvxMateriel.AcceptChanges();  //On valide
                     
            //Puis on le supprime de la liste
            listViewMat1.Items[listViewMat1.SelectedIndices[0]].Remove();

            //On efface le tBox de suppression
            tBoxSupprMatos.Text = "";
        }

        private void rBTypeBoitier1_CheckedChanged(object sender, EventArgs e)
        {
            //Si c'est une nouvel abonnement, on défini IDContrat (!= IMAD) = PROM
            if (tbAbonnement.Tag != null && tbAbonnement.Tag.ToString() == "-1")
            {
                //on défini IDContrat (!= IMAD) = PROM
                if (rBTypeBoitier1.Checked)
                {
                    string[] BoitierDispo;
                    BoitierDispo = RecupContactIDLuna("L3GSL");

                    txtIdContrat.Text = BoitierDispo[0];
                    EMaskTel1.Text = BoitierDispo[1];

                    if (BoitierDispo[0] == "-1")
                    {
                        MessageBox.Show("Il n'y a plus de boitier Disponible en stock pour ce modèle.", "Stock Materiel", MessageBoxButtons.OK ,MessageBoxIcon.Stop);
                    }                   
                }
            }       
        }


        private void rBTypeBoitier2_CheckedChanged(object sender, EventArgs e)
        {
            //Si c'est une nouvel abonnement, on défini IDContrat (!= IMAD) = PROM
            if (tbAbonnement.Tag != null && tbAbonnement.Tag.ToString() == "-1")
            {
                //on défini IDContrat (!= IMAD) = PROM
                if (rBTypeBoitier2.Checked)
                {
                    string[] BoitierDispo;
                    BoitierDispo = RecupContactIDLuna("L3G");

                    txtIdContrat.Text = BoitierDispo[0];
                    EMaskTel1.Text = BoitierDispo[1];

                    if (BoitierDispo[0] == "-1")
                    {
                        MessageBox.Show("Il n'y a plus de boitier Disponible en stock pour ce modèle.", "Stock Materiel", MessageBoxButtons.OK ,MessageBoxIcon.Stop);
                    }                                                        
                }                             
            }           
        }


        private void rBTypeBoitier3_CheckedChanged(object sender, EventArgs e)
        {
            //Si c'est une nouvel abonnement, on défini IDContrat (!= IMAD) = PROM
            if (tbAbonnement.Tag != null && tbAbonnement.Tag.ToString() == "-1")
            {
                //on défini IDContrat (!= IMAD) = PROM
                if (rBTypeBoitier3.Checked)
                {
                    string[] BoitierDispo;
                    BoitierDispo = RecupContactIDLuna("L4");

                    txtIdContrat.Text = BoitierDispo[0];
                    EMaskTel1.Text = BoitierDispo[1];

                    if (BoitierDispo[0] == "-1")
                    {
                        MessageBox.Show("Il n'y a plus de boitier Disponible en stock pour ce modèle.", "Stock Materiel", MessageBoxButtons.OK ,MessageBoxIcon.Stop);
                    }                                                   
                }
            }         
        }


        private string[] RecupContactIDLuna(string TypeLuna)
        {
            //On récupère un materiel dans le stock et on lui attribue le ContactId
            //Recup du plus grand n°                    
            string[] reponse = {"-1",""};
            
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);

            dbConnection.Open();

            SqlCommand cmd = dbConnection.CreateCommand();

            try
            {
                string sqlstr0 = "SELECT ContactID, Num_tel_Sim FROM TA_Materiel ";
                sqlstr0 += " WHERE Type_tarif = @TypeLuna ";
                sqlstr0 += " AND (IDAbonnement IS NULL or IDAbonnement = '') ";
                sqlstr0 += " AND DateHS IS NULL";
                sqlstr0 += " ORDER BY ContactID";
                cmd.CommandText = sqlstr0;

                cmd.Parameters.AddWithValue("TypeLuna", TypeLuna);

                DataTable dtMaxNum = new DataTable();
                dtMaxNum.Load(cmd.ExecuteReader());

                //On attribut ContactID
                if (dtMaxNum.Rows.Count > 0 && dtMaxNum.Rows[0][0] != DBNull.Value)
                {
                    reponse[0] = int.Parse(dtMaxNum.Rows[0][0].ToString()).ToString();   //On enlève les 0 devant
                    reponse[1] = dtMaxNum.Rows[0][1].ToString();
                }
                else
                {
                    //Pas de materiel libre en stock
                    reponse[0] = "-1";
                    reponse[1] = "";
                }                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur Lors de la recherche du n° de contrat Le message est: " + ex.Message);
            }

            return reponse;
        }


        private void ChargeListMateriel(string IdAbonnement)
        {
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);

            dbConnection.Open();

            SqlCommand cmd = dbConnection.CreateCommand();

            try
            {
                string sqlstr0 = "SELECT * FROM TA_Materiel ";
                sqlstr0 += " WHERE IDAbonnement = @IdAbonnement ";                
                cmd.CommandText = sqlstr0;

                cmd.Parameters.AddWithValue("IdAbonnement", IdAbonnement);

                DataTable dtMateriel = new DataTable();
                dtMateriel.Load(cmd.ExecuteReader());

                dtNvxMateriel.Rows.Clear();

                if (dtMateriel.Rows.Count > 0 && dtMateriel.Rows[0][0] != DBNull.Value)
                {
                    //On rempli le DataTable(Pour enregistrer ou modifier par la suite) et la liste view                
                    for (int i = 0; i < dtMateriel.Rows.Count; i++)
                    {
                        //En fonction du Type_tarif, on determine quel boitier est affecté à cet abonnement
                        switch (dtMateriel.Rows[i]["Type_tarif"].ToString())
                        {
                            case "L3GSL": rBTypeBoitier1.Checked = true; AncienCheck = 1; break;
                            case "L3G": rBTypeBoitier2.Checked = true; AncienCheck = 2; break;
                            case "L4": rBTypeBoitier3.Checked = true; AncienCheck = 3; break;
                            default:
                                {
                                    dtNvxMateriel.Rows.Add(dtMateriel.Rows[i]["VID"].ToString(), dtMateriel.Rows[i]["Libelle"].ToString(), dtMateriel.Rows[i]["IdAbonnement"].ToString());
                                    
                                    //On l'ajoute dans la liste View (si c'est pas une Luna)                       
                                    ListViewItem item1 = new ListViewItem(dtMateriel.Rows[i]["Libelle"].ToString());
                                    item1.SubItems.Add(dtMateriel.Rows[i]["VID"].ToString());
                                    listViewMat1.Items.Add(item1);
                                    
                                    break;
                                }
                        }                                               
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur Lors de la recherche du chargement de la liste du materiel. Le message est: " + ex.Message);
            }
                                                      
        }

        private void listViewMat1_DoubleClick(object sender, EventArgs e)
        {
            //On selectionne la ligne que l'on affiche dans tBoxSupprMatos
            tBoxSupprMatos.Text = listViewMat1.Items[listViewMat1.SelectedIndices[0]].Text;          
            tBoxSupprMatos.Text = tBoxSupprMatos.Text + " " + listViewMat1.Items[listViewMat1.SelectedIndices[0]].SubItems[1].Text;
        }


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

       

        private void cBoxMotifChangement_SelectedIndexChanged(object sender, EventArgs e)
        {

            //On fait le changement de boitier
            //on regarde s'il y en a de dispo                                                            
            string[] BoitierDispo;
            string Luna = "";

            if (rBTypeBoitier1.Checked)            
                Luna = "L3GSL";            
            else if (rBTypeBoitier2.Checked)
                Luna = "L3G";            
            else if (rBTypeBoitier3.Checked)
                Luna = "L4";
            
            BoitierDispo = RecupContactIDLuna(Luna);

            if (BoitierDispo[0] == "-1")
            {
                MessageBox.Show("Il n'y a plus de boitier Disponible en stock pour ce modèle.", "Stock Materiel", MessageBoxButtons.OK ,MessageBoxIcon.Stop);
                
                //on remet sur l'ancien check
                cBoxMotifChangement.Text = "";
                cBoxMotifChangement.Enabled = false;
                               
                if (AncienCheck == 1)
                    rBTypeBoitier1.Checked = true;
                else if (AncienCheck == 2)
                    rBTypeBoitier2.Checked = true;
                else if (AncienCheck == 3)
                    rBTypeBoitier3.Checked = true;                                
            }
            else
            {
                //On prépare la déaffectation l'ancien boitier
                Desafection[0] = cBoxMotifChangement.Text;
                Desafection[1] = Complete(txtIdContrat.Text, 8);

                //Affectation du nvx boitier et changement du Tel Patient
                txtIdContrat.Text = BoitierDispo[0];
                EMaskTel1.Text = BoitierDispo[1];
            }
        }


        private void rBTypeBoitier2_Click(object sender, EventArgs e)
        {
            if (AncienCheck != 2)
            {               
                if (tbAbonnement.Tag != null && tbAbonnement.Tag.ToString() != "-1")     //Sinon c'est un abonnement existant....Attention modification de la PROM!!!
                {
                    //on demande le motif du changement
                    DialogResult result1 = MessageBox.Show("Voulez vous changer la box de cette personne? Si oui, veuillez en indiquer le motif. ", "Changement de box",
                                           MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result1 == DialogResult.Yes)
                    {
                        //on active la boite motif
                        cBoxMotifChangement.Enabled = true;
                    }
                    else
                    {
                        cBoxMotifChangement.Text = "";
                        cBoxMotifChangement.Enabled = false;
                        //On remet l'ancien check
                        if (AncienCheck == 3)
                            rBTypeBoitier3.Checked = true;
                        else if (AncienCheck == 1)
                            rBTypeBoitier1.Checked = true;
                    }
                }
            }
        }


        private void rBTypeBoitier3_Click(object sender, EventArgs e)
        {
            if (AncienCheck != 3)
            {                                
                if (tbAbonnement.Tag != null && tbAbonnement.Tag.ToString() != "-1")    //Sinon c'est un abonnement existant....Attention modification de la PROM!!!
                {
                    //on demande le motif du changement
                    DialogResult result1 = MessageBox.Show("Voulez vous changer la box de cette personne? Si oui, veuillez en indiquer le motif. ", "Changement de box",
                                           MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result1 == DialogResult.Yes)
                    {
                        //on active la boite motif
                        cBoxMotifChangement.Enabled = true;
                    }
                    else
                    {
                        cBoxMotifChangement.Text = "";
                        cBoxMotifChangement.Enabled = false;
                        //On remet l'ancien check
                        if (AncienCheck == 2)
                            rBTypeBoitier2.Checked = true;
                        else if (AncienCheck == 1)
                            rBTypeBoitier1.Checked = true;
                    }
                }
            }
        }

        private void rBTypeBoitier1_Click(object sender, EventArgs e)                     
        {
            if (AncienCheck != 1)
            {              
                if (tbAbonnement.Tag != null && tbAbonnement.Tag.ToString() != "-1")     //Sinon c'est un abonnement existant....Attention modification de la PROM!!!
                {
                    //on demande le motif du changement
                    DialogResult result1 = MessageBox.Show("Voulez vous changer la box de cette personne? Si oui, veuillez en indiquer le motif. ", "Changement de box",
                                           MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result1 == DialogResult.Yes)
                    {
                        //on active la boite motif
                        cBoxMotifChangement.Enabled = true;
                    }
                    else
                    {
                        cBoxMotifChangement.Text = "";
                        cBoxMotifChangement.Enabled = false;
                        //On remet l'ancien check
                        if (AncienCheck == 3)
                            rBTypeBoitier3.Checked = true;
                        else if (AncienCheck == 2)
                            rBTypeBoitier2.Checked = true;
                    }
                }
            }

        }


        //Charge la liste des n° de Tel de la personne
        private void ChargeListTel(string IdPersonne)
        {
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);

            dbConnection.Open();

            SqlCommand cmd = dbConnection.CreateCommand();

            try
            {
                string sqlstr0 = "SELECT NumTel FROM Tel_Personne ";
                sqlstr0 += " WHERE NumPersonne = @IdPersonnne ";
                cmd.CommandText = sqlstr0;

                cmd.Parameters.AddWithValue("IdPersonnne", IdPersonne);

                DataTable dtListeTel = new DataTable();
                dtListeTel.Load(cmd.ExecuteReader());

                dtNvxTel.Rows.Clear();
                dtDelTel.Rows.Clear();
                listViewTel.Items.Clear();

                if (dtListeTel.Rows.Count > 0 && dtListeTel.Rows[0][0] != DBNull.Value)
                {
                    //On rempli le DataTable(Pour enregistrer ou modifier par la suite) et la liste view                
                    for (int i = 0; i < dtListeTel.Rows.Count; i++)
                    {                        
                        //On l'ajoute dans la liste View                     
                        ListViewItem itemTel = new ListViewItem(dtListeTel.Rows[i]["NumTel"].ToString());                                  
                        listViewTel.Items.Add(itemTel);                                                                   
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur Lors du chargement de la liste des téléphones du patient. Le message est: " + ex.Message);
            }

        }

        private void bAjouteTel1_Click(object sender, EventArgs e)
        {
            //On ajoute le n° de Tel, après avoir vérifié qu'il n'existe pas déjà
            //Formatage du n°
            if (EmaskAjoutTel.Text.Replace("-", "").Replace("+", "") != "")
            {
                string Tel = EmaskAjoutTel.Text;
                
                Tel = Tel.Replace(" ", "");       //On commence par enlever les espaces

            if (Tel.IndexOf("+") == -1)
            {
                if (Tel.Substring(0, 1) == "0")
                    Tel = "+41" + Tel.Remove(0, 1);
                else Tel = "+" + Tel;
            }
            else
            {   //On le reformate (on vire eventuellement +++)
                Tel = "+" + Tel.Replace("+", "");
            }
           
            //On l'ajoute dans le dtNvxTel ET Dans la listTel
            dtNvxTel.Rows.Add(Tel);

            //On l'ajoute dans la liste View                     
            ListViewItem itemTel = new ListViewItem(Tel);
            listViewTel.Items.Add(itemTel);  

            //Puis on efface le champs de saisie
            EmaskAjoutTel.Text = "";
            }                                
        }


        private void EmaskAjoutTel_TextChanged(object sender, EventArgs e)
        {
            if (EmaskAjoutTel.Text.Length >= 10)
            {
                bAjouteTel1.Enabled = true;
                bAjouteTel1.ImageIndex = 6;
            }
            else
            {
                bAjouteTel1.Enabled = false;
                bAjouteTel1.ImageIndex = 7;
            }
        }


        private void AjouteListTel(string IdPersonne)
        {            
            //On essai d'ajouter les N° de Tel qui ont été ajoutés à la liste         
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);

            try
            {
                dbConnection.Open();

                SqlCommand cmd = dbConnection.CreateCommand();
                cmd.Connection = dbConnection;

                //Gestion des n° de Tel...On regarde s'il existe dans la table Tel_Personne                                                                       
                for (int i = 0; i < dtNvxTel.Rows.Count; i++)
                {
                    string sqlstr0 = "SELECT NumPersonne, NumTel";
                    sqlstr0 += " FROM Tel_Personne";
                    sqlstr0 += " WHERE NumPersonne = @IdPersonne";
                    sqlstr0 += " AND NumTel = @Tel";

                    cmd.CommandText = sqlstr0;

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("IdPersonne", IdPersonne);
                    cmd.Parameters.AddWithValue("Tel", dtNvxTel.Rows[i]["NumTel"].ToString());

                    DataTable Telephone = new DataTable();
                    Telephone.Load(cmd.ExecuteReader());

                    //Si on ne l'a pas trouvé, on l'ajoute à la table
                    if (Telephone.Rows.Count == 0)
                    {
                        sqlstr0 = "INSERT INTO Tel_Personne";
                        sqlstr0 += " (NumPersonne, NumTel, Date_NumTel) ";
                        sqlstr0 += " VALUES(@NumPersonne, @Tel, GetDate())";

                        cmd.CommandText = sqlstr0;

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("NumPersonne", IdPersonne);
                        cmd.Parameters.AddWithValue("Tel", dtNvxTel.Rows[i]["NumTel"].ToString());

                        cmd.ExecuteReader();                        
                    }                  
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(" Erreur : " + e.Message);
            }                         
        }


        //On supprime un n° de Tel de la personne
        private void SupprimeTel(string IdPersonne)
        {
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);
            
            try
            {
                dbConnection.Open();
                SqlCommand cmd = dbConnection.CreateCommand();
                cmd.Connection = dbConnection;

                for (int i = 0; i < dtDelTel.Rows.Count; i++)
                {
                    //On supprime le Tel
                    string sqlstr0 = "DELETE FROM Tel_Personne ";
                    sqlstr0 += " WHERE NumPersonne = @IdPersonnne ";
                    sqlstr0 += " AND NumTel = @Tel";
                    cmd.CommandText = sqlstr0;

                    cmd.Parameters.AddWithValue("IdPersonnne", IdPersonne);
                    cmd.Parameters.AddWithValue("Tel", dtDelTel.Rows[i]["NumTel"].ToString());

                    cmd.ExecuteReader();
                }

                //On vide la liste des Tel à effacer
                dtDelTel.Rows.Clear();                                               
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur Lors de la suppression des telephones du patient. Le message est: " + ex.Message);
            }
        }



        private void txtOnglet2Nom1_Click(object sender, EventArgs e)
        {
            //On passe en modif
            if (lwMemoire.SelectedIndices.Count > 0 && ModifContact == 0)
            {
                bModifierEn1_Click(sender, e);
            }
        }

        private void txtOnglet2Nom2_Click(object sender, EventArgs e)
        {
            //On passe en modif
            if (lwUrgence.SelectedIndices.Count > 0 && ModifContactUrgent == 0)
            {
                bModifierEn2_Click(sender, e);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
           //On recherche les opérations pour cette facture            
            if (dataGridView1.SelectedRows.Count > 0)
            {
                //On vide le dataGridView2
                dataGridView2.Columns.Clear();
                
                //Recherche des opérations de cette facture
                string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
                SqlConnection dbConnection = new SqlConnection(connex);

                try
                {
                    //dbConnection.Open();
                   // SqlCommand cmd = dbConnection.CreateCommand();
                   // cmd.Connection = dbConnection;
                     
                    string sqlstr0 = "";
                    sqlstr0 = "SELECT NumFacture, DateEncaissement, Montant, MoyenPaiement";
                                                            
                    if (dataGridView1.CurrentRow.Cells[8].Value.ToString() == "Abonnement")                    
                        sqlstr0 += " FROM TA_Factures_Op";                                           
                    else                     
                        sqlstr0 += " FROM TA_FactMat_Op";                                                  
                    
                    sqlstr0 += " WHERE NumFacture = @NFacture";
                    sqlstr0 += " ORDER BY DateEncaissement Desc";

                 //   cmd.CommandText = sqlstr0;
                 //   cmd.Parameters.Clear();
                 //   cmd.Parameters.AddWithValue("NFacture", dataGridView1.CurrentRow.Cells[0].Value.ToString());
                    
                    SqlDataAdapter cmd = new SqlDataAdapter(sqlstr0, dbConnection);
                    cmd.SelectCommand.Parameters.AddWithValue("@NFacture", dataGridView1.CurrentRow.Cells[0].Value.ToString());

                    DataTable DetailFactureOp = new DataTable();
                    //DetailFactureOp.Load(cmd.ExecuteReader());
                    //on execute
                    cmd.Fill(DetailFactureOp);                 

                    //Si on a quelque chose...
                    if (DetailFactureOp.Rows.Count != 0)
                    {                                
                        //Paramètres du datagridView... On détermine les colonnes à afficher                                      
                        dataGridView2.DataSource = DetailFactureOp;
                        dataGridView2.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dataGridView2.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dataGridView2.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dataGridView2.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
               
                        dataGridView2.Columns[0].HeaderText = "N° de facture";
                        dataGridView2.Columns[1].HeaderText = "Date Encaissement";
                        dataGridView2.Columns[2].HeaderText = "Montant";           
                        dataGridView2.Columns[3].HeaderText = "Moyen de paiement";

                        //on centre le titre de toute les colonnes
                        foreach (DataGridViewColumn col in dataGridView1.Columns)
                        {
                            col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }

                        dataGridView2.Columns[0].Width = 90;
                        dataGridView2.Columns[1].Width = 120;
                        dataGridView2.Columns[2].Width = 90;
                        dataGridView2.Columns[3].Width = 100;                                                                                                
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("erreur lors de l'execution de la requete: " + ex.Message);
                }
            }            
        }

        private void bActiverFacture_Click(object sender, EventArgs e)
        {
            bActiverFacture.Enabled = false;
            mouchard.evenement("Activation de la facturation pour  " + txtTa_Nom.Text.ToString() + " " + txtTa_Prenom.Text.ToString(), VariablesApplicatives.Utilisateurs.NomUtilisateur.ToString());  //log
        }



        private void bsupprTel_Click(object sender, EventArgs e)
        {
            //si on a selectionné quelque chose
            if (listViewTel.SelectedIndices.Count > 0)
            {
                string Tel = listViewTel.Items[listViewTel.SelectedIndices[0]].Text.Replace(" ", "");

                //On suprime le N° de Tel il en faut au moins 1
                if (listViewTel.Items.Count > 1)
                {
                    if (Tel != EMaskTel1.Text.Replace(" ", ""))
                    {
                        //En fait, on l'ajoute dans le dtDelTel ET on le supprime dans la listTel
                        dtDelTel.Rows.Add(Tel);

                        //On le supprime de la liste de la liste View                     
                        listViewTel.Items.RemoveAt(listViewTel.SelectedIndices[0]);
                    }
                    else
                    {
                        MessageBox.Show("Vous ne pouvez supprimer ce n° car c'est celui de la box!");
                    }                   
                }
                else
                {
                    MessageBox.Show("Vous ne pouvez supprimer ce n° car il en faut au moins 1!");
                }

                //Puis on deselectionne les N°
                listViewTel.SelectedIndices.Clear();

            }
        }

      
    }
}


//A faire 

//Panneau modules

//Voir le fonctionnement Quand TA pour les 2 d'une même famille (débloquer la recherche sur Même n°)


