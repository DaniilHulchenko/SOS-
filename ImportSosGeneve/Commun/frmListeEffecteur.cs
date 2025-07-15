using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace ImportSosGeneve
{
	public class frmListeEffecteur : System.Windows.Forms.Form
	{
        private System.Windows.Forms.RadioButton rdMed;
		private System.Windows.Forms.TextBox txtFindNom;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.GroupBox GpMedecin;
		private System.Windows.Forms.TextBox txtDestinataire;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.TextBox txtMail;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.TextBox txtEan;
		private System.Windows.Forms.TextBox txtFax;
		private System.Windows.Forms.TextBox txtNatel;
		private System.Windows.Forms.TextBox txtCommentaire;
		private System.Windows.Forms.TextBox txtConcordat;
		private System.Windows.Forms.TextBox txtTelephone;
		private System.Windows.Forms.TextBox txtDateDeces;
		private System.Windows.Forms.TextBox txtDateNaissance;
		private System.Windows.Forms.TextBox txtLocalite;
		private System.Windows.Forms.TextBox txtNp;
		private System.Windows.Forms.TextBox txtAdresse;
		private System.Windows.Forms.TextBox txtNumAdresse;
		private System.Windows.Forms.TextBox txtSpecialite;
		private System.Windows.Forms.TextBox txtPrenom;
		private System.Windows.Forms.TextBox txtNom;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ComboBox cbSexe;
		private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFindCommune;
        private IContainer components;
        private System.Windows.Forms.ListBox lstResultat;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.ComboBox cbCivilite;
		private System.Windows.Forms.ComboBox cbModeEnvoi;

		public enum TypeListe {MedecinVille,Hopital};

		private DataRow m_RowSelected = null;
		private System.Windows.Forms.TextBox txtFindPrenom;
		private System.Windows.Forms.Label label24;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private CheckBox cBoxActif;
        private Button bRechercher;
        private ImageList imageList1;
        private Button bQuitter;
        private Button bCancel;
        private Button bNvxDestinataire;
        private Button bValide;
        private Button bMaj;
        private Button bAnnulModif;
        private Button bSuppr;
        private ToolTip toolTip1;

        public DataRow RowSelected
		{
			get
			{
				return m_RowSelected;
			}
		}


		private frmListeEffecteur.TypeListe m_TypeDeListe = frmListeEffecteur.TypeListe.MedecinVille;

        //Fiche vierge
		public frmListeEffecteur()
		{			
			Initialisation();

            //On initialise les boutons et certains controles
            cBoxActif.Checked = true;

            bMaj.Enabled = false;
            bNvxDestinataire.Enabled = true;
            bSuppr.Enabled = false;
            bAnnulModif.Enabled = false;
            bValide.Enabled = false;
        }


        //On vient d'une autre fiche
        public frmListeEffecteur(frmListeEffecteur.TypeListe TypeDeListe, DataRow Effecteur)
        {
            m_TypeDeListe = TypeDeListe;
            Initialisation();
            AfficheMedecinTraitant(Effecteur);
        }


        public void Initialisation()
		{
			InitializeComponent();

			// Si médecin traitant, on charge les différentes valeurs possibles pour la civilité:
			cbCivilite.Items.Clear();

			if(m_TypeDeListe==frmListeEffecteur.TypeListe.MedecinVille)
			{
				string[][] ret = OutilsExt.OutilsSql.ExecuteCommandeAvecTabString("SELECT distinct civilite from medecinsville ORDER by civilite asc");
				if(ret!=null)
				{
					foreach(string[] s in ret)
						cbCivilite.Items.Add(s[0]);
				}
			}
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListeEffecteur));
            this.rdMed = new System.Windows.Forms.RadioButton();
            this.label24 = new System.Windows.Forms.Label();
            this.txtFindPrenom = new System.Windows.Forms.TextBox();
            this.lstResultat = new System.Windows.Forms.ListBox();
            this.txtFindCommune = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFindNom = new System.Windows.Forms.TextBox();
            this.GpMedecin = new System.Windows.Forms.GroupBox();
            this.bAnnulModif = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.bSuppr = new System.Windows.Forms.Button();
            this.bMaj = new System.Windows.Forms.Button();
            this.bNvxDestinataire = new System.Windows.Forms.Button();
            this.bValide = new System.Windows.Forms.Button();
            this.cbCivilite = new System.Windows.Forms.ComboBox();
            this.cbModeEnvoi = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.txtDestinataire = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txtMail = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txtEan = new System.Windows.Forms.TextBox();
            this.txtFax = new System.Windows.Forms.TextBox();
            this.txtNatel = new System.Windows.Forms.TextBox();
            this.txtCommentaire = new System.Windows.Forms.TextBox();
            this.txtConcordat = new System.Windows.Forms.TextBox();
            this.txtTelephone = new System.Windows.Forms.TextBox();
            this.txtDateDeces = new System.Windows.Forms.TextBox();
            this.txtDateNaissance = new System.Windows.Forms.TextBox();
            this.txtLocalite = new System.Windows.Forms.TextBox();
            this.txtNp = new System.Windows.Forms.TextBox();
            this.txtAdresse = new System.Windows.Forms.TextBox();
            this.txtNumAdresse = new System.Windows.Forms.TextBox();
            this.txtSpecialite = new System.Windows.Forms.TextBox();
            this.txtPrenom = new System.Windows.Forms.TextBox();
            this.txtNom = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbSexe = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cBoxActif = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.bRechercher = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.bQuitter = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.GpMedecin.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // rdMed
            // 
            this.rdMed.Checked = true;
            this.rdMed.Location = new System.Drawing.Point(10, 20);
            this.rdMed.Name = "rdMed";
            this.rdMed.Size = new System.Drawing.Size(136, 16);
            this.rdMed.TabIndex = 0;
            this.rdMed.TabStop = true;
            this.rdMed.Text = "Médecin de ville";
            // 
            // label24
            // 
            this.label24.Location = new System.Drawing.Point(6, 48);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(62, 18);
            this.label24.TabIndex = 2;
            this.label24.Text = "Prénom :";
            // 
            // txtFindPrenom
            // 
            this.txtFindPrenom.Location = new System.Drawing.Point(76, 45);
            this.txtFindPrenom.Name = "txtFindPrenom";
            this.txtFindPrenom.Size = new System.Drawing.Size(184, 20);
            this.txtFindPrenom.TabIndex = 3;
            // 
            // lstResultat
            // 
            this.lstResultat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstResultat.HorizontalScrollbar = true;
            this.lstResultat.Location = new System.Drawing.Point(3, 16);
            this.lstResultat.Name = "lstResultat";
            this.lstResultat.Size = new System.Drawing.Size(268, 562);
            this.lstResultat.TabIndex = 0;
            this.lstResultat.SelectedIndexChanged += new System.EventHandler(this.lstResultat_SelectedIndexChanged);
            // 
            // txtFindCommune
            // 
            this.txtFindCommune.Location = new System.Drawing.Point(76, 71);
            this.txtFindCommune.Name = "txtFindCommune";
            this.txtFindCommune.Size = new System.Drawing.Size(184, 20);
            this.txtFindCommune.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(6, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Commune :";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "Nom :";
            // 
            // txtFindNom
            // 
            this.txtFindNom.Location = new System.Drawing.Point(76, 19);
            this.txtFindNom.Name = "txtFindNom";
            this.txtFindNom.Size = new System.Drawing.Size(184, 20);
            this.txtFindNom.TabIndex = 1;
            // 
            // GpMedecin
            // 
            this.GpMedecin.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GpMedecin.BackColor = System.Drawing.Color.Transparent;
            this.GpMedecin.Controls.Add(this.bAnnulModif);
            this.GpMedecin.Controls.Add(this.bSuppr);
            this.GpMedecin.Controls.Add(this.bMaj);
            this.GpMedecin.Controls.Add(this.bNvxDestinataire);
            this.GpMedecin.Controls.Add(this.bValide);
            this.GpMedecin.Controls.Add(this.cbCivilite);
            this.GpMedecin.Controls.Add(this.cbModeEnvoi);
            this.GpMedecin.Controls.Add(this.label20);
            this.GpMedecin.Controls.Add(this.label23);
            this.GpMedecin.Controls.Add(this.txtDestinataire);
            this.GpMedecin.Controls.Add(this.label22);
            this.GpMedecin.Controls.Add(this.txtMail);
            this.GpMedecin.Controls.Add(this.label21);
            this.GpMedecin.Controls.Add(this.txtEan);
            this.GpMedecin.Controls.Add(this.txtFax);
            this.GpMedecin.Controls.Add(this.txtNatel);
            this.GpMedecin.Controls.Add(this.txtCommentaire);
            this.GpMedecin.Controls.Add(this.txtConcordat);
            this.GpMedecin.Controls.Add(this.txtTelephone);
            this.GpMedecin.Controls.Add(this.txtDateDeces);
            this.GpMedecin.Controls.Add(this.txtDateNaissance);
            this.GpMedecin.Controls.Add(this.txtLocalite);
            this.GpMedecin.Controls.Add(this.txtNp);
            this.GpMedecin.Controls.Add(this.txtAdresse);
            this.GpMedecin.Controls.Add(this.txtNumAdresse);
            this.GpMedecin.Controls.Add(this.txtSpecialite);
            this.GpMedecin.Controls.Add(this.txtPrenom);
            this.GpMedecin.Controls.Add(this.txtNom);
            this.GpMedecin.Controls.Add(this.label19);
            this.GpMedecin.Controls.Add(this.label18);
            this.GpMedecin.Controls.Add(this.label17);
            this.GpMedecin.Controls.Add(this.label16);
            this.GpMedecin.Controls.Add(this.label15);
            this.GpMedecin.Controls.Add(this.label14);
            this.GpMedecin.Controls.Add(this.label13);
            this.GpMedecin.Controls.Add(this.label12);
            this.GpMedecin.Controls.Add(this.label11);
            this.GpMedecin.Controls.Add(this.label10);
            this.GpMedecin.Controls.Add(this.label9);
            this.GpMedecin.Controls.Add(this.label8);
            this.GpMedecin.Controls.Add(this.label7);
            this.GpMedecin.Controls.Add(this.label6);
            this.GpMedecin.Controls.Add(this.label5);
            this.GpMedecin.Controls.Add(this.cbSexe);
            this.GpMedecin.Location = new System.Drawing.Point(12, 161);
            this.GpMedecin.Name = "GpMedecin";
            this.GpMedecin.Size = new System.Drawing.Size(524, 484);
            this.GpMedecin.TabIndex = 3;
            this.GpMedecin.TabStop = false;
            this.GpMedecin.Text = "Informations Médecin Ville";
            // 
            // bAnnulModif
            // 
            this.bAnnulModif.BackColor = System.Drawing.Color.Transparent;
            this.bAnnulModif.BackgroundImage = global::ImportSosGeneve.Properties.Resources.bondCancel;
            this.bAnnulModif.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bAnnulModif.FlatAppearance.BorderSize = 0;
            this.bAnnulModif.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bAnnulModif.ImageIndex = 4;
            this.bAnnulModif.ImageList = this.imageList1;
            this.bAnnulModif.Location = new System.Drawing.Point(431, 416);
            this.bAnnulModif.Name = "bAnnulModif";
            this.bAnnulModif.Size = new System.Drawing.Size(50, 50);
            this.bAnnulModif.TabIndex = 66;
            this.toolTip1.SetToolTip(this.bAnnulModif, "Annule les modifications en cour.");
            this.bAnnulModif.UseVisualStyleBackColor = false;
            this.bAnnulModif.Click += new System.EventHandler(this.bAnnulModif_Click);
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
            this.imageList1.Images.SetKeyName(17, "boutonsEnregistrer.png");
            this.imageList1.Images.SetKeyName(18, "boutonsEnregistrerOff.png");
            this.imageList1.Images.SetKeyName(19, "bExitOn.png");
            this.imageList1.Images.SetKeyName(20, "bExitOff.png");
            this.imageList1.Images.SetKeyName(21, "BNvxDossierOff.png");
            // 
            // bSuppr
            // 
            this.bSuppr.BackColor = System.Drawing.Color.Transparent;
            this.bSuppr.BackgroundImage = global::ImportSosGeneve.Properties.Resources.bsupprimer;
            this.bSuppr.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bSuppr.FlatAppearance.BorderSize = 0;
            this.bSuppr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bSuppr.ImageIndex = 0;
            this.bSuppr.ImageList = this.imageList1;
            this.bSuppr.Location = new System.Drawing.Point(347, 416);
            this.bSuppr.Name = "bSuppr";
            this.bSuppr.Size = new System.Drawing.Size(50, 50);
            this.bSuppr.TabIndex = 65;
            this.toolTip1.SetToolTip(this.bSuppr, "Supprime le médecin (si jamais utilisé)");
            this.bSuppr.UseVisualStyleBackColor = false;
            this.bSuppr.Click += new System.EventHandler(this.bSuppr_Click);
            // 
            // bMaj
            // 
            this.bMaj.BackColor = System.Drawing.Color.Transparent;
            this.bMaj.BackgroundImage = global::ImportSosGeneve.Properties.Resources.boutonsEnregistrer;
            this.bMaj.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bMaj.FlatAppearance.BorderSize = 0;
            this.bMaj.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bMaj.ImageIndex = 17;
            this.bMaj.ImageList = this.imageList1;
            this.bMaj.Location = new System.Drawing.Point(248, 416);
            this.bMaj.Name = "bMaj";
            this.bMaj.Size = new System.Drawing.Size(50, 50);
            this.bMaj.TabIndex = 64;
            this.toolTip1.SetToolTip(this.bMaj, "Enregistre les modifications");
            this.bMaj.UseVisualStyleBackColor = false;
            this.bMaj.Click += new System.EventHandler(this.bMaj_Click);
            // 
            // bNvxDestinataire
            // 
            this.bNvxDestinataire.BackColor = System.Drawing.Color.Transparent;
            this.bNvxDestinataire.BackgroundImage = global::ImportSosGeneve.Properties.Resources.bajout;
            this.bNvxDestinataire.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bNvxDestinataire.FlatAppearance.BorderSize = 0;
            this.bNvxDestinataire.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bNvxDestinataire.ImageIndex = 6;
            this.bNvxDestinataire.ImageList = this.imageList1;
            this.bNvxDestinataire.Location = new System.Drawing.Point(32, 416);
            this.bNvxDestinataire.Name = "bNvxDestinataire";
            this.bNvxDestinataire.Size = new System.Drawing.Size(50, 50);
            this.bNvxDestinataire.TabIndex = 63;
            this.toolTip1.SetToolTip(this.bNvxDestinataire, "Ajout d\'un nouveau médecin");
            this.bNvxDestinataire.UseVisualStyleBackColor = false;
            this.bNvxDestinataire.Click += new System.EventHandler(this.bNvxDestinataire_Click);
            // 
            // bValide
            // 
            this.bValide.BackColor = System.Drawing.Color.Transparent;
            this.bValide.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bValide.FlatAppearance.BorderSize = 0;
            this.bValide.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bValide.ImageIndex = 2;
            this.bValide.ImageList = this.imageList1;
            this.bValide.Location = new System.Drawing.Point(113, 416);
            this.bValide.Name = "bValide";
            this.bValide.Size = new System.Drawing.Size(50, 50);
            this.bValide.TabIndex = 62;
            this.toolTip1.SetToolTip(this.bValide, "Enregistre le nouveau médecin");
            this.bValide.UseVisualStyleBackColor = false;
            this.bValide.Click += new System.EventHandler(this.bValide_Click);
            // 
            // cbCivilite
            // 
            this.cbCivilite.Location = new System.Drawing.Point(114, 105);
            this.cbCivilite.Name = "cbCivilite";
            this.cbCivilite.Size = new System.Drawing.Size(107, 21);
            this.cbCivilite.TabIndex = 9;
            // 
            // cbModeEnvoi
            // 
            this.cbModeEnvoi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbModeEnvoi.Items.AddRange(new object[] {
            "Aucun",
            "Mail",
            "Fax",
            "Courrier"});
            this.cbModeEnvoi.Location = new System.Drawing.Point(114, 24);
            this.cbModeEnvoi.Name = "cbModeEnvoi";
            this.cbModeEnvoi.Size = new System.Drawing.Size(234, 21);
            this.cbModeEnvoi.TabIndex = 1;
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Location = new System.Drawing.Point(6, 27);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(80, 16);
            this.label20.TabIndex = 0;
            this.label20.Text = "Mode d\'envoi :";
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.Transparent;
            this.label23.Location = new System.Drawing.Point(6, 108);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(64, 16);
            this.label23.TabIndex = 8;
            this.label23.Text = "Civilité :";
            // 
            // txtDestinataire
            // 
            this.txtDestinataire.Location = new System.Drawing.Point(114, 53);
            this.txtDestinataire.Name = "txtDestinataire";
            this.txtDestinataire.Size = new System.Drawing.Size(234, 20);
            this.txtDestinataire.TabIndex = 3;
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.Transparent;
            this.label22.Location = new System.Drawing.Point(6, 56);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(86, 16);
            this.label22.TabIndex = 2;
            this.label22.Text = "Destinataire :";
            // 
            // txtMail
            // 
            this.txtMail.Location = new System.Drawing.Point(114, 288);
            this.txtMail.Name = "txtMail";
            this.txtMail.Size = new System.Drawing.Size(380, 20);
            this.txtMail.TabIndex = 30;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Location = new System.Drawing.Point(6, 291);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(37, 13);
            this.label21.TabIndex = 29;
            this.label21.Text = "email :";
            // 
            // txtEan
            // 
            this.txtEan.Location = new System.Drawing.Point(265, 79);
            this.txtEan.Name = "txtEan";
            this.txtEan.Size = new System.Drawing.Size(83, 20);
            this.txtEan.TabIndex = 7;
            // 
            // txtFax
            // 
            this.txtFax.Location = new System.Drawing.Point(381, 314);
            this.txtFax.Name = "txtFax";
            this.txtFax.Size = new System.Drawing.Size(112, 20);
            this.txtFax.TabIndex = 36;
            // 
            // txtNatel
            // 
            this.txtNatel.Location = new System.Drawing.Point(265, 314);
            this.txtNatel.Name = "txtNatel";
            this.txtNatel.Size = new System.Drawing.Size(83, 20);
            this.txtNatel.TabIndex = 34;
            // 
            // txtCommentaire
            // 
            this.txtCommentaire.Location = new System.Drawing.Point(114, 340);
            this.txtCommentaire.Multiline = true;
            this.txtCommentaire.Name = "txtCommentaire";
            this.txtCommentaire.Size = new System.Drawing.Size(379, 57);
            this.txtCommentaire.TabIndex = 38;
            // 
            // txtConcordat
            // 
            this.txtConcordat.Location = new System.Drawing.Point(114, 79);
            this.txtConcordat.Name = "txtConcordat";
            this.txtConcordat.Size = new System.Drawing.Size(107, 20);
            this.txtConcordat.TabIndex = 5;
            // 
            // txtTelephone
            // 
            this.txtTelephone.Location = new System.Drawing.Point(114, 314);
            this.txtTelephone.Name = "txtTelephone";
            this.txtTelephone.Size = new System.Drawing.Size(107, 20);
            this.txtTelephone.TabIndex = 32;
            // 
            // txtDateDeces
            // 
            this.txtDateDeces.Location = new System.Drawing.Point(382, 210);
            this.txtDateDeces.Name = "txtDateDeces";
            this.txtDateDeces.Size = new System.Drawing.Size(111, 20);
            this.txtDateDeces.TabIndex = 21;
            // 
            // txtDateNaissance
            // 
            this.txtDateNaissance.Location = new System.Drawing.Point(114, 210);
            this.txtDateNaissance.Name = "txtDateNaissance";
            this.txtDateNaissance.Size = new System.Drawing.Size(107, 20);
            this.txtDateNaissance.TabIndex = 19;
            // 
            // txtLocalite
            // 
            this.txtLocalite.Location = new System.Drawing.Point(265, 262);
            this.txtLocalite.Name = "txtLocalite";
            this.txtLocalite.Size = new System.Drawing.Size(229, 20);
            this.txtLocalite.TabIndex = 28;
            // 
            // txtNp
            // 
            this.txtNp.Location = new System.Drawing.Point(114, 262);
            this.txtNp.Name = "txtNp";
            this.txtNp.Size = new System.Drawing.Size(45, 20);
            this.txtNp.TabIndex = 26;
            // 
            // txtAdresse
            // 
            this.txtAdresse.Location = new System.Drawing.Point(165, 236);
            this.txtAdresse.Name = "txtAdresse";
            this.txtAdresse.Size = new System.Drawing.Size(328, 20);
            this.txtAdresse.TabIndex = 24;
            // 
            // txtNumAdresse
            // 
            this.txtNumAdresse.Location = new System.Drawing.Point(114, 236);
            this.txtNumAdresse.Name = "txtNumAdresse";
            this.txtNumAdresse.Size = new System.Drawing.Size(45, 20);
            this.txtNumAdresse.TabIndex = 23;
            // 
            // txtSpecialite
            // 
            this.txtSpecialite.Location = new System.Drawing.Point(114, 184);
            this.txtSpecialite.Name = "txtSpecialite";
            this.txtSpecialite.Size = new System.Drawing.Size(379, 20);
            this.txtSpecialite.TabIndex = 17;
            // 
            // txtPrenom
            // 
            this.txtPrenom.Location = new System.Drawing.Point(114, 158);
            this.txtPrenom.Name = "txtPrenom";
            this.txtPrenom.Size = new System.Drawing.Size(379, 20);
            this.txtPrenom.TabIndex = 15;
            // 
            // txtNom
            // 
            this.txtNom.Location = new System.Drawing.Point(114, 132);
            this.txtNom.Name = "txtNom";
            this.txtNom.Size = new System.Drawing.Size(380, 20);
            this.txtNom.TabIndex = 13;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Location = new System.Drawing.Point(6, 343);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(74, 13);
            this.label19.TabIndex = 37;
            this.label19.Text = "Commentaire :";
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.Location = new System.Drawing.Point(227, 82);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(48, 16);
            this.label18.TabIndex = 6;
            this.label18.Text = "EAN :";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Location = new System.Drawing.Point(6, 82);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(62, 13);
            this.label17.TabIndex = 4;
            this.label17.Text = "Concordat :";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Location = new System.Drawing.Point(346, 317);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(30, 13);
            this.label16.TabIndex = 35;
            this.label16.Text = "Fax :";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Location = new System.Drawing.Point(221, 317);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(38, 13);
            this.label15.TabIndex = 33;
            this.label15.Text = "Natel :";
            this.label15.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Location = new System.Drawing.Point(6, 317);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(64, 13);
            this.label14.TabIndex = 31;
            this.label14.Text = "Téléphone :";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Location = new System.Drawing.Point(211, 265);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(50, 13);
            this.label13.TabIndex = 27;
            this.label13.Text = "Localité :";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Location = new System.Drawing.Point(6, 266);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(27, 13);
            this.label12.TabIndex = 25;
            this.label12.Text = "Np :";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Location = new System.Drawing.Point(6, 239);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(51, 13);
            this.label11.TabIndex = 22;
            this.label11.Text = "Adresse :";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Location = new System.Drawing.Point(6, 187);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(64, 16);
            this.label10.TabIndex = 16;
            this.label10.Text = "Spécialité :";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Location = new System.Drawing.Point(6, 161);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 16);
            this.label9.TabIndex = 14;
            this.label9.Text = "Prénom :";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(6, 135);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 16);
            this.label8.TabIndex = 12;
            this.label8.Text = "Nom :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(293, 213);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Date de déces :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(6, 213);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Date de naissance :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(227, 110);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Sexe :";
            // 
            // cbSexe
            // 
            this.cbSexe.Items.AddRange(new object[] {
            "F",
            "M",
            "H"});
            this.cbSexe.Location = new System.Drawing.Point(265, 105);
            this.cbSexe.Name = "cbSexe";
            this.cbSexe.Size = new System.Drawing.Size(83, 21);
            this.cbSexe.TabIndex = 11;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cBoxActif);
            this.groupBox1.Controls.Add(this.rdMed);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(524, 46);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sélection du type de personne ";
            // 
            // cBoxActif
            // 
            this.cBoxActif.Location = new System.Drawing.Point(248, 17);
            this.cBoxActif.Name = "cBoxActif";
            this.cBoxActif.Size = new System.Drawing.Size(125, 24);
            this.cBoxActif.TabIndex = 2;
            this.cBoxActif.Text = "Medecin en activité";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.bRechercher);
            this.groupBox2.Controls.Add(this.label24);
            this.groupBox2.Controls.Add(this.txtFindNom);
            this.groupBox2.Controls.Add(this.txtFindPrenom);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtFindCommune);
            this.groupBox2.Location = new System.Drawing.Point(12, 59);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(362, 102);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Critére de recherche";
            // 
            // bRechercher
            // 
            this.bRechercher.BackColor = System.Drawing.Color.Transparent;
            this.bRechercher.BackgroundImage = global::ImportSosGeneve.Properties.Resources.boutonjumelles;
            this.bRechercher.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bRechercher.FlatAppearance.BorderSize = 0;
            this.bRechercher.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bRechercher.ImageIndex = 0;
            this.bRechercher.Location = new System.Drawing.Point(287, 28);
            this.bRechercher.Name = "bRechercher";
            this.bRechercher.Size = new System.Drawing.Size(50, 50);
            this.bRechercher.TabIndex = 55;
            this.toolTip1.SetToolTip(this.bRechercher, "Rechercher");
            this.bRechercher.UseVisualStyleBackColor = false;
            this.bRechercher.Click += new System.EventHandler(this.bRechercher_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lstResultat);
            this.groupBox3.Location = new System.Drawing.Point(547, 17);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(274, 581);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Résultats de la recherche";
            // 
            // bQuitter
            // 
            this.bQuitter.BackColor = System.Drawing.Color.Transparent;
            this.bQuitter.BackgroundImage = global::ImportSosGeneve.Properties.Resources.bEnvoi;
            this.bQuitter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bQuitter.FlatAppearance.BorderSize = 0;
            this.bQuitter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bQuitter.ImageIndex = 16;
            this.bQuitter.ImageList = this.imageList1;
            this.bQuitter.Location = new System.Drawing.Point(688, 621);
            this.bQuitter.Name = "bQuitter";
            this.bQuitter.Size = new System.Drawing.Size(50, 50);
            this.bQuitter.TabIndex = 60;
            this.toolTip1.SetToolTip(this.bQuitter, "Ajouter ce médecin et Quitter");
            this.bQuitter.UseVisualStyleBackColor = false;
            this.bQuitter.Click += new System.EventHandler(this.bQuitter_Click);
            // 
            // bCancel
            // 
            this.bCancel.BackColor = System.Drawing.Color.Transparent;
            this.bCancel.BackgroundImage = global::ImportSosGeneve.Properties.Resources.bondCancel;
            this.bCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bCancel.FlatAppearance.BorderSize = 0;
            this.bCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bCancel.ImageIndex = 4;
            this.bCancel.ImageList = this.imageList1;
            this.bCancel.Location = new System.Drawing.Point(771, 621);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(50, 50);
            this.bCancel.TabIndex = 61;
            this.toolTip1.SetToolTip(this.bCancel, "Cancel");
            this.bCancel.UseVisualStyleBackColor = false;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 300;
            this.toolTip1.IsBalloon = true;
            // 
            // frmListeEffecteur
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.ClientSize = new System.Drawing.Size(851, 693);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bQuitter);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.GpMedecin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmListeEffecteur";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Liste des médecins de ville";
            this.GpMedecin.ResumeLayout(false);
            this.GpMedecin.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion
	

		private void ListeMedecins(DataSet ds)
		{
			lstResultat.Items.Clear();
			if(ds!=null)
			{
				for(int i=0;i<ds.Tables[0].Rows.Count;i++)
				{
					ListItem item = new ListItem(ds.Tables[0].Rows[i],ds.Tables[0].Rows[i]["Nom"].ToString() + " " + ds.Tables[0].Rows[i]["Prenom"].ToString() + " [" + ds.Tables[0].Rows[i]["Commune"].ToString() + "]");
					lstResultat.Items.Add(item);
				}
			}
		}

		private void FicheMedecinVierge()
		{
			GpMedecin.Tag=null;
			txtNom.Text = "";
			txtPrenom.Text = "";
			cbSexe.Text = "";
			txtSpecialite.Text = "";
			txtDateDeces.Text = "";
			txtDateNaissance.Text = "";
			txtNumAdresse.Text = "";
			txtAdresse.Text = "";
			txtNp.Text = "";
			txtLocalite.Text = "";
			txtTelephone.Text = "";
			txtNatel.Text = "";
			txtFax.Text =  "";
			txtConcordat.Text = "";
			txtEan.Text ="";
			txtCommentaire.Text  = "";		
			txtMail.Text = "";
			txtDestinataire.Text="";
			cbCivilite.Text  = "";
			cbModeEnvoi.Text = "";            
            cBoxActif.Checked = true;
		}

		private void AfficheMedecinTraitant(DataRow row)
		{
			GpMedecin.Tag = row;
			txtNom.Text = row["Nom"].ToString();
			txtPrenom.Text = row["Prenom"].ToString();
			cbSexe.Text = row["Sexe"].ToString();
			txtSpecialite.Text = row["Specialite"].ToString();
			txtDateDeces.Text = row["DateNaissance"].ToString();
			txtDateNaissance.Text = row["DateDeces"].ToString();
			txtNumAdresse.Text = row["NumeroRue"].ToString();
			txtAdresse.Text = row["Rue"].ToString();
			txtNp.Text = row["Np"].ToString();
			txtLocalite.Text = row["Commune"].ToString();
			txtTelephone.Text = row["Telephone"].ToString();
			txtNatel.Text = row["Natel"].ToString();
			txtFax.Text =  row["Fax"].ToString();
			txtConcordat.Text = row["NConcordat"].ToString();
			txtEan.Text =row["EAN"].ToString();
			txtCommentaire.Text  = "";			
			txtMail.Text = row["email"].ToString();
			txtDestinataire.Text = row["Destinataire"].ToString();
			cbCivilite.Text = row["Civilite"].ToString();  
            cbModeEnvoi.Text = row["ModeEnvoi"].ToString();
          
            if (row["Active"].ToString() == "1")
                cBoxActif.Checked = true;
            else
                cBoxActif.Checked = false;

			m_RowSelected = row;


            //On initialise les boutons
            bMaj.Enabled = true;
            bNvxDestinataire.Enabled = true;
            bSuppr.Enabled = true;
            bAnnulModif.Enabled = false;
            bValide.Enabled = false;
        }

		private void lstResultat_SelectedIndexChanged(object sender, EventArgs e)
		{
			if(lstResultat.SelectedIndex>-1)
			{
				FicheMedecinVierge();
				ListItem item = (ListItem)lstResultat.SelectedItem;
				DataRow row = (DataRow)item.objValue;
				AfficheMedecinTraitant(row);
			}
		}
				

        private void EnregistreMedecinTraitant()
        {
            if (txtNom.Text == "" || cbSexe.Text == "")
            {
                MessageBox.Show("Les champs Nom et Sexe sont obligatoires.", "Destinataire", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DataRow row = (DataRow)GpMedecin.Tag;
            
            row["Nom"] = txtNom.Text;
            row["Prenom"] = txtPrenom.Text;
            row["Sexe"] = cbSexe.Text;
            row["Specialite"] = txtSpecialite.Text;
            row["DateNaissance"] = txtDateDeces.Text;
            row["DateDeces"] = txtDateNaissance.Text;
            row["NumeroRue"] = txtNumAdresse.Text;
            row["Rue"] = txtAdresse.Text;
            row["Np"] = txtNp.Text;
            row["Commune"] = txtLocalite.Text;
            row["Telephone"] = txtTelephone.Text;
            row["Natel"] = txtNatel.Text;
            row["Fax"] = txtFax.Text;
            row["NConcordat"] = txtConcordat.Text;
            row["EAN"] = txtEan.Text;
            row["email"] = txtMail.Text;
            row["Destinataire"] = txtDestinataire.Text;
            row["ModeEnvoi"] = cbModeEnvoi.Text;
            row["Civilite"] = cbCivilite.Text;
           
            if(cBoxActif.Checked == true)
                row["Active"] = "1";
            else
                row["Active"] = "0";

            SosMedecins.SmartRapport.DAL.MedecinsVille z_objMedecinsTraitant = new SosMedecins.SmartRapport.DAL.MedecinsVille();

            if (z_objMedecinsTraitant.Update(row))
                MessageBox.Show("Fiche médecin enregistrée!");
            else
                MessageBox.Show("Erreur enregistrement médecin!");

            z_objMedecinsTraitant = null;
        }
	
		
        private void bRechercher_Click(object sender, EventArgs e)
        {
            DataSet ds = null;

            if (m_TypeDeListe == frmListeEffecteur.TypeListe.MedecinVille)
            {
                //requete en fonction des critères de recherche
                string Sqlstr0 = "SELECT * from medecinsville";

                if (cBoxActif.Checked)
                    Sqlstr0 += " WHERE Active = 1";
                else
                    Sqlstr0 += " WHERE Active = 0";
                
                if (txtFindNom.Text != "")
                {
                    Sqlstr0 += " AND Nom LIKE '" + txtFindNom.Text.Replace("'", "''") + "%' OR destinataire LIKE '" + txtFindNom.Text.Replace("'", "''") + "%' ";
                }

                if (txtFindPrenom.Text != "")
                {
                    Sqlstr0 += " AND Prenom LIKE '" + txtFindPrenom.Text.Replace("'", "''") + "%' ";
                }

                if (txtFindCommune.Text != "")
                {
                    Sqlstr0 += " AND Commune LIKE '" + txtFindNom.Text.Replace("'", "''") + "%' ";
                }

                if (txtFindNom.Text == "" && txtFindPrenom.Text == "" && txtFindCommune.Text == "")                  
                {
                    MessageBox.Show("Sélectionner au moins un critère de recherche");
                    return;
                }
                else
                {                    
                    ds = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet(Sqlstr0 += " ORDER BY Nom, Prenom");
                    ListeMedecins(ds);
                }

            }
        }

        private void bQuitter_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            //Aucune selection
            m_RowSelected = null;
            Close();
        }

        private void bNvxDestinataire_Click(object sender, EventArgs e)
        {
            // on prépare la fiche vierge 
            FicheMedecinVierge();
            DataRow row = OutilsExt.OutilsSql.AjouteMedecinVille();
            GpMedecin.Tag = row;
            
            bMaj.Enabled = false;            
            bNvxDestinataire.Enabled = false;            
            bSuppr.Enabled = false;
            bAnnulModif.Enabled = true;
            bValide.Enabled = true;            
        }

        private void bValide_Click(object sender, EventArgs e)
        {
            EnregistreMedecinTraitant();

            bMaj.Enabled = true;
            bNvxDestinataire.Enabled = true;
            bSuppr.Enabled = true;
            bAnnulModif.Enabled = false;
            bValide.Enabled = false;
        }

        private void bMaj_Click(object sender, EventArgs e)
        {
            if (GpMedecin.Tag == null) return;
            EnregistreMedecinTraitant();
        }

        private void bSuppr_Click(object sender, EventArgs e)
        {
            if (GpMedecin.Tag == null)
            {
                return;
            }
            DataRow row = (DataRow)GpMedecin.Tag;
            if (row == null)
            {
                return;
            }

            if (MessageBox.Show("Voulez-vous supprimer ce médecin ?", "Destinataire", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                OutilsExt.OutilsSql.SupprimerMedecinVille(long.Parse(row["num"].ToString()));

                MessageBox.Show("Médecin supprimé.", "Destinataire", MessageBoxButtons.OK, MessageBoxIcon.Information);

                FicheMedecinVierge();
            }
        }

        private void bAnnulModif_Click(object sender, EventArgs e)
        {
            FicheMedecinVierge();          

            bMaj.Enabled = true;
            bNvxDestinataire.Enabled = true;
            bSuppr.Enabled = true;
            bAnnulModif.Enabled = false;
            bValide.Enabled = false;
        }
    }
}
