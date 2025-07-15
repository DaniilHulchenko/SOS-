using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace ImportSosGeneve
{
	/// <summary>
	/// Description résumée de frmAjoutDestinataire.
	/// </summary>
	public class frmAjoutDestinataire : System.Windows.Forms.Form
	{
		private frmGeneral _frmGeneral=null;
		private int  m_intTypeRapport;
		private bool m_bModif = false;
		private bool PourTeleAlarme=false;
		private CtrlTA m_Ta=null;
	
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbTypeDestinataire;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox tBoxNom;
        private System.Windows.Forms.TextBox Prenom;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ListView lwDestinataire;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ComboBox cbSexe;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.TextBox txtNom;
		private System.Windows.Forms.TextBox txtPrenom;
		private System.Windows.Forms.TextBox txtSpecialite;
		private System.Windows.Forms.TextBox txtNumAdresse;
		private System.Windows.Forms.TextBox txtAdresse;
		private System.Windows.Forms.TextBox txtNp;
		private System.Windows.Forms.TextBox txtLocalite;
		private System.Windows.Forms.TextBox txtTelephone;
		private System.Windows.Forms.TextBox txtConcordat;
		private System.Windows.Forms.TextBox txtCommentaire;
		private System.Windows.Forms.TextBox txtNatel;
		private System.Windows.Forms.TextBox txtFax;
		private System.Windows.Forms.TextBox txtEan;
		private System.Windows.Forms.TextBox txtDateNaissance;
		private System.Windows.Forms.TextBox txtDateDeces;
		private System.Windows.Forms.GroupBox GpMedecin;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.RadioButton radioButton1;
		private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Label label20;
		private System.Windows.Forms.ComboBox cbModeEnvoi;
		private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbCivilite;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.TextBox txtMail;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.CheckBox chkCopie;
		private System.Windows.Forms.TextBox txtDestinataire;
		private System.Windows.Forms.Label label22;
		
		private System.Windows.Forms.Label label23;
        private System.Windows.Forms.CheckBox cbActive;
        private ImageList imageList1;
        private Label label24;
        private ToolTip toolTip1;
        private Button bRechercher;
        private Button bValide;
        private Button bCancel;
        private Button bQuitterAjoutDest;
        private Button bSuppr;
        private Button bNvxDestinataire;
        private Button bMaj;
        private Button bAjoutDest;
        private CheckBox cBoxActif;
        private IContainer components;

		public frmAjoutDestinataire(frmGeneral _frm,int TypeRapport)
		{
			//
			// Requis pour la prise en charge du Concepteur Windows Forms
			//
			InitializeComponent();
	
			this._frmGeneral = _frm;
			m_intTypeRapport = TypeRapport;

			cbModeEnvoi.SelectedIndex = 0;
			if(m_intTypeRapport==1)
				cbTypeDestinataire.SelectedIndex = 0;
			//else if(m_intTypeRapport==2)
			//	cbTypeDestinataire.SelectedIndex = 3;

			tBoxNom.Focus();
		
		}
		public frmAjoutDestinataire(frmGeneral _frm,int TypeRapport,bool Modif)
		{
			//
			// Requis pour la prise en charge du Concepteur Windows Forms
			//
			InitializeComponent();
	
			this.m_bModif = Modif;
			this._frmGeneral = _frm;
			m_intTypeRapport = TypeRapport;

			cbModeEnvoi.SelectedIndex = 0;

			groupBox1.Visible = false;

            bNvxDestinataire.ImageIndex = 7;
            bNvxDestinataire.Enabled = false;

            bSuppr.Enabled = false;
            bSuppr.ImageIndex = 1;
			
			//linkLabel1.Text  = "Modifier le destinataire";
		}
		public frmAjoutDestinataire(CtrlTA Ta,int TypeRapport,bool Modif,bool PourTeleAlarme)
		{
			//
			// Requis pour la prise en charge du Concepteur Windows Forms
			//
			InitializeComponent();
	
			this.PourTeleAlarme = PourTeleAlarme;
			this.m_Ta = Ta;
            this.m_bModif = Modif;  //Pour savoir si on est en modif.

            //CtrlTA => Si le n° du medecins traitant est renseigné et qu'on est en modif, on passe en modif de la fiche
            if (m_Ta.MedecinsTraitant != "" && Modif == true)
            {
                this.Text = "Modifier ce médecins Traitant";

                groupBox1.Visible = false;
                groupBox2.Visible = false;

                RechercheMedTTTParNum(m_Ta.MedecinsTraitant);

                bQuitterAjoutDest.Enabled = true;
                bQuitterAjoutDest.ImageIndex = 19;

                bMaj.ImageIndex = 17;
                bMaj.Enabled = true;

                bNvxDestinataire.ImageIndex = 7;
                bNvxDestinataire.Enabled = false;

                bSuppr.Enabled = false;
                bSuppr.ImageIndex = 1;

                bAjoutDest.ImageIndex = 21;
                bAjoutDest.Enabled = false;

                bCancel.Enabled = false;
                bCancel.ImageIndex = 5;

                bValide.Enabled = false;
                bValide.ImageIndex = 3; 
            }
            else
            {
                //On passe en recherche d'un médecin
                this.Text = "Ajouter un médecin destinataire";

                groupBox2.Visible = false;  //Recherche Médecin
                cbTypeDestinataire.SelectedIndex = 0;
                cbTypeDestinataire.Enabled = false;

                FicheMedecinVierge();

                groupBox1.Visible = true;

                bQuitterAjoutDest.Enabled = true;
                bQuitterAjoutDest.ImageIndex = 19;

                bMaj.ImageIndex = 18;
                bMaj.Enabled = false;

                bNvxDestinataire.ImageIndex = 6;
                bNvxDestinataire.Enabled = true;

                bSuppr.Enabled = false;
                bSuppr.ImageIndex = 1;

                bAjoutDest.ImageIndex = 21;
                bAjoutDest.Enabled = false;

                bCancel.Enabled = false;
                bCancel.ImageIndex = 5;

                bValide.Enabled = false;
                bValide.ImageIndex = 3;               
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

		#region Code généré par le Concepteur Windows Form
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAjoutDestinataire));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cBoxActif = new System.Windows.Forms.CheckBox();
            this.bRechercher = new System.Windows.Forms.Button();
            this.label23 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lwDestinataire = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tBoxNom = new System.Windows.Forms.TextBox();
            this.Prenom = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbTypeDestinataire = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.GpMedecin = new System.Windows.Forms.GroupBox();
            this.label24 = new System.Windows.Forms.Label();
            this.cbActive = new System.Windows.Forms.CheckBox();
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkCopie = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.cbCivilite = new System.Windows.Forms.ComboBox();
            this.cbModeEnvoi = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.bValide = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.bQuitterAjoutDest = new System.Windows.Forms.Button();
            this.bSuppr = new System.Windows.Forms.Button();
            this.bNvxDestinataire = new System.Windows.Forms.Button();
            this.bMaj = new System.Windows.Forms.Button();
            this.bAjoutDest = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.GpMedecin.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.cBoxActif);
            this.groupBox1.Controls.Add(this.bRechercher);
            this.groupBox1.Controls.Add(this.label23);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lwDestinataire);
            this.groupBox1.Controls.Add(this.tBoxNom);
            this.groupBox1.Controls.Add(this.Prenom);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbTypeDestinataire);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(649, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(426, 237);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Destinataire";
            // 
            // cBoxActif
            // 
            this.cBoxActif.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cBoxActif.Checked = true;
            this.cBoxActif.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cBoxActif.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cBoxActif.Location = new System.Drawing.Point(264, 23);
            this.cBoxActif.Name = "cBoxActif";
            this.cBoxActif.Size = new System.Drawing.Size(148, 20);
            this.cBoxActif.TabIndex = 55;
            this.cBoxActif.Text = "Médecin en activité";
            // 
            // bRechercher
            // 
            this.bRechercher.BackColor = System.Drawing.Color.Transparent;
            this.bRechercher.BackgroundImage = global::ImportSosGeneve.Properties.Resources.boutonjumelles;
            this.bRechercher.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bRechercher.FlatAppearance.BorderSize = 0;
            this.bRechercher.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bRechercher.ImageIndex = 0;
            this.bRechercher.Location = new System.Drawing.Point(352, 163);
            this.bRechercher.Name = "bRechercher";
            this.bRechercher.Size = new System.Drawing.Size(60, 60);
            this.bRechercher.TabIndex = 54;
            this.toolTip1.SetToolTip(this.bRechercher, "Rechercher le destinataire");
            this.bRechercher.UseVisualStyleBackColor = false;
            this.bRechercher.Click += new System.EventHandler(this.bRechercher_Click);
            // 
            // label23
            // 
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(64, 126);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(71, 17);
            this.label23.TabIndex = 5;
            this.label23.Text = "Prenom :";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(347, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 38);
            this.label4.TabIndex = 4;
            this.label4.Text = "(2 lettres minimum)";
            // 
            // lwDestinataire
            // 
            this.lwDestinataire.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lwDestinataire.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lwDestinataire.FullRowSelect = true;
            this.lwDestinataire.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lwDestinataire.HideSelection = false;
            this.lwDestinataire.Location = new System.Drawing.Point(12, 162);
            this.lwDestinataire.Name = "lwDestinataire";
            this.lwDestinataire.Size = new System.Drawing.Size(293, 66);
            this.lwDestinataire.TabIndex = 7;
            this.lwDestinataire.UseCompatibleStateImageBehavior = false;
            this.lwDestinataire.View = System.Windows.Forms.View.Details;
            this.lwDestinataire.SelectedIndexChanged += new System.EventHandler(this.lwDestinataire_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 150;
            // 
            // tBoxNom
            // 
            this.tBoxNom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tBoxNom.Location = new System.Drawing.Point(144, 92);
            this.tBoxNom.Name = "tBoxNom";
            this.tBoxNom.Size = new System.Drawing.Size(197, 22);
            this.tBoxNom.TabIndex = 3;
            this.tBoxNom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // Prenom
            // 
            this.Prenom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Prenom.Location = new System.Drawing.Point(144, 123);
            this.Prenom.Name = "Prenom";
            this.Prenom.Size = new System.Drawing.Size(197, 22);
            this.Prenom.TabIndex = 6;
            this.Prenom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Prenom_KeyDown);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(11, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 22);
            this.label3.TabIndex = 2;
            this.label3.Text = "Recherche par nom :";
            // 
            // cbTypeDestinataire
            // 
            this.cbTypeDestinataire.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTypeDestinataire.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTypeDestinataire.Items.AddRange(new object[] {
            "Médecins et Permanances/Groupes médicaux",
            "Interne"});
            this.cbTypeDestinataire.Location = new System.Drawing.Point(69, 55);
            this.cbTypeDestinataire.Name = "cbTypeDestinataire";
            this.cbTypeDestinataire.Size = new System.Drawing.Size(250, 24);
            this.cbTypeDestinataire.TabIndex = 1;
            this.cbTypeDestinataire.SelectedIndexChanged += new System.EventHandler(this.cbTypeDestinataire_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Type : ";
            // 
            // GpMedecin
            // 
            this.GpMedecin.BackColor = System.Drawing.Color.Transparent;
            this.GpMedecin.Controls.Add(this.label24);
            this.GpMedecin.Controls.Add(this.cbActive);
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
            this.GpMedecin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GpMedecin.Location = new System.Drawing.Point(12, 12);
            this.GpMedecin.Name = "GpMedecin";
            this.GpMedecin.Size = new System.Drawing.Size(615, 448);
            this.GpMedecin.TabIndex = 3;
            this.GpMedecin.TabStop = false;
            this.GpMedecin.Text = "Informations Médecin Ville";
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.Transparent;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(172, 152);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(48, 16);
            this.label24.TabIndex = 36;
            this.label24.Text = "Rue";
            // 
            // cbActive
            // 
            this.cbActive.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbActive.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbActive.Location = new System.Drawing.Point(325, 55);
            this.cbActive.Name = "cbActive";
            this.cbActive.Size = new System.Drawing.Size(146, 20);
            this.cbActive.TabIndex = 4;
            this.cbActive.Text = "Medecin en activité";
            // 
            // txtDestinataire
            // 
            this.txtDestinataire.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDestinataire.Location = new System.Drawing.Point(110, 21);
            this.txtDestinataire.Name = "txtDestinataire";
            this.txtDestinataire.Size = new System.Drawing.Size(347, 22);
            this.txtDestinataire.TabIndex = 1;
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.Transparent;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(11, 26);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(86, 16);
            this.label22.TabIndex = 0;
            this.label22.Text = "Destinataire :";
            // 
            // txtMail
            // 
            this.txtMail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMail.Location = new System.Drawing.Point(341, 325);
            this.txtMail.Name = "txtMail";
            this.txtMail.Size = new System.Drawing.Size(254, 22);
            this.txtMail.TabIndex = 34;
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(270, 325);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(48, 16);
            this.label21.TabIndex = 33;
            this.label21.Text = "email :";
            // 
            // txtEan
            // 
            this.txtEan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEan.Location = new System.Drawing.Point(223, 298);
            this.txtEan.Name = "txtEan";
            this.txtEan.Size = new System.Drawing.Size(193, 22);
            this.txtEan.TabIndex = 31;
            // 
            // txtFax
            // 
            this.txtFax.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFax.Location = new System.Drawing.Point(456, 268);
            this.txtFax.Name = "txtFax";
            this.txtFax.Size = new System.Drawing.Size(142, 22);
            this.txtFax.TabIndex = 27;
            // 
            // txtNatel
            // 
            this.txtNatel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNatel.Location = new System.Drawing.Point(223, 268);
            this.txtNatel.Name = "txtNatel";
            this.txtNatel.Size = new System.Drawing.Size(176, 22);
            this.txtNatel.TabIndex = 25;
            // 
            // txtCommentaire
            // 
            this.txtCommentaire.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCommentaire.Location = new System.Drawing.Point(9, 356);
            this.txtCommentaire.Multiline = true;
            this.txtCommentaire.Name = "txtCommentaire";
            this.txtCommentaire.Size = new System.Drawing.Size(589, 71);
            this.txtCommentaire.TabIndex = 35;
            // 
            // txtConcordat
            // 
            this.txtConcordat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConcordat.Location = new System.Drawing.Point(418, 221);
            this.txtConcordat.Name = "txtConcordat";
            this.txtConcordat.Size = new System.Drawing.Size(177, 22);
            this.txtConcordat.TabIndex = 29;
            // 
            // txtTelephone
            // 
            this.txtTelephone.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTelephone.Location = new System.Drawing.Point(110, 221);
            this.txtTelephone.Name = "txtTelephone";
            this.txtTelephone.Size = new System.Drawing.Size(148, 22);
            this.txtTelephone.TabIndex = 23;
            // 
            // txtDateDeces
            // 
            this.txtDateDeces.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDateDeces.Location = new System.Drawing.Point(468, 109);
            this.txtDateDeces.Name = "txtDateDeces";
            this.txtDateDeces.Size = new System.Drawing.Size(118, 22);
            this.txtDateDeces.TabIndex = 14;
            // 
            // txtDateNaissance
            // 
            this.txtDateNaissance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDateNaissance.Location = new System.Drawing.Point(468, 80);
            this.txtDateNaissance.Name = "txtDateNaissance";
            this.txtDateNaissance.Size = new System.Drawing.Size(118, 22);
            this.txtDateNaissance.TabIndex = 10;
            // 
            // txtLocalite
            // 
            this.txtLocalite.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLocalite.Location = new System.Drawing.Point(273, 181);
            this.txtLocalite.Name = "txtLocalite";
            this.txtLocalite.Size = new System.Drawing.Size(322, 22);
            this.txtLocalite.TabIndex = 21;
            // 
            // txtNp
            // 
            this.txtNp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNp.Location = new System.Drawing.Point(107, 182);
            this.txtNp.Name = "txtNp";
            this.txtNp.Size = new System.Drawing.Size(74, 22);
            this.txtNp.TabIndex = 19;
            // 
            // txtAdresse
            // 
            this.txtAdresse.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAdresse.Location = new System.Drawing.Point(223, 146);
            this.txtAdresse.Name = "txtAdresse";
            this.txtAdresse.Size = new System.Drawing.Size(372, 22);
            this.txtAdresse.TabIndex = 17;
            // 
            // txtNumAdresse
            // 
            this.txtNumAdresse.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumAdresse.Location = new System.Drawing.Point(102, 149);
            this.txtNumAdresse.Name = "txtNumAdresse";
            this.txtNumAdresse.Size = new System.Drawing.Size(54, 22);
            this.txtNumAdresse.TabIndex = 16;
            // 
            // txtSpecialite
            // 
            this.txtSpecialite.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSpecialite.Location = new System.Drawing.Point(84, 105);
            this.txtSpecialite.Name = "txtSpecialite";
            this.txtSpecialite.Size = new System.Drawing.Size(217, 22);
            this.txtSpecialite.TabIndex = 12;
            // 
            // txtPrenom
            // 
            this.txtPrenom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrenom.Location = new System.Drawing.Point(75, 77);
            this.txtPrenom.Name = "txtPrenom";
            this.txtPrenom.Size = new System.Drawing.Size(226, 22);
            this.txtPrenom.TabIndex = 8;
            // 
            // txtNom
            // 
            this.txtNom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNom.Location = new System.Drawing.Point(75, 52);
            this.txtNom.Name = "txtNom";
            this.txtNom.Size = new System.Drawing.Size(226, 22);
            this.txtNom.TabIndex = 3;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(12, 337);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(101, 16);
            this.label19.TabIndex = 32;
            this.label19.Text = "Commentaire :";
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(167, 301);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(48, 16);
            this.label18.TabIndex = 30;
            this.label18.Text = "EAN :";
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(331, 224);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(84, 22);
            this.label17.TabIndex = 28;
            this.label17.Text = "Concordat :";
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(415, 271);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(42, 19);
            this.label16.TabIndex = 26;
            this.label16.Text = "Fax :";
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(163, 271);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(54, 19);
            this.label15.TabIndex = 24;
            this.label15.Text = "Natel :";
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(19, 224);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(85, 19);
            this.label14.TabIndex = 22;
            this.label14.Text = "Téléphone :";
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(220, 185);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(47, 18);
            this.label13.TabIndex = 20;
            this.label13.Text = "Ville :";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(18, 185);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(95, 16);
            this.label12.TabIndex = 18;
            this.label12.Text = "Code Postal :";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(61, 149);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(35, 19);
            this.label11.TabIndex = 15;
            this.label11.Text = "N°";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(12, 109);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(85, 19);
            this.label10.TabIndex = 11;
            this.label10.Text = "Spécialité :";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(12, 80);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 18);
            this.label9.TabIndex = 7;
            this.label9.Text = "Prénom :";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(12, 56);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 16);
            this.label8.TabIndex = 2;
            this.label8.Text = "Nom :";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(345, 112);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(112, 16);
            this.label7.TabIndex = 13;
            this.label7.Text = "Date de déces :";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(322, 83);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(135, 20);
            this.label6.TabIndex = 9;
            this.label6.Text = "Date de naissance :";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(535, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 16);
            this.label5.TabIndex = 5;
            this.label5.Text = "Sexe :";
            // 
            // cbSexe
            // 
            this.cbSexe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSexe.Items.AddRange(new object[] {
            "F",
            "M",
            "H"});
            this.cbSexe.Location = new System.Drawing.Point(518, 50);
            this.cbSexe.Name = "cbSexe";
            this.cbSexe.Size = new System.Drawing.Size(68, 24);
            this.cbSexe.TabIndex = 6;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.chkCopie);
            this.groupBox2.Controls.Add(this.checkBox1);
            this.groupBox2.Controls.Add(this.cbCivilite);
            this.groupBox2.Controls.Add(this.cbModeEnvoi);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label20);
            this.groupBox2.Controls.Add(this.radioButton2);
            this.groupBox2.Controls.Add(this.radioButton1);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(649, 255);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(427, 203);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Style";
            // 
            // chkCopie
            // 
            this.chkCopie.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCopie.Location = new System.Drawing.Point(13, 157);
            this.chkCopie.Name = "chkCopie";
            this.chkCopie.Size = new System.Drawing.Size(128, 20);
            this.chkCopie.TabIndex = 7;
            this.chkCopie.Text = "Intitulé COPIE";
            // 
            // checkBox1
            // 
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.Location = new System.Drawing.Point(133, 71);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(167, 20);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "Affichage du logo";
            // 
            // cbCivilite
            // 
            this.cbCivilite.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCivilite.Location = new System.Drawing.Point(241, 22);
            this.cbCivilite.Name = "cbCivilite";
            this.cbCivilite.Size = new System.Drawing.Size(150, 24);
            this.cbCivilite.TabIndex = 2;
            // 
            // cbModeEnvoi
            // 
            this.cbModeEnvoi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbModeEnvoi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbModeEnvoi.Items.AddRange(new object[] {
            "Aucun",
            "Mail",
            "Fax",
            "Courrier"});
            this.cbModeEnvoi.Location = new System.Drawing.Point(133, 110);
            this.cbModeEnvoi.Name = "cbModeEnvoi";
            this.cbModeEnvoi.Size = new System.Drawing.Size(265, 24);
            this.cbModeEnvoi.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(22, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 19);
            this.label2.TabIndex = 5;
            this.label2.Text = "Mode d\'envoi :";
            // 
            // label20
            // 
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(167, 25);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(57, 20);
            this.label20.TabIndex = 1;
            this.label20.Text = "Civilité :";
            // 
            // radioButton2
            // 
            this.radioButton2.BackColor = System.Drawing.Color.Transparent;
            this.radioButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton2.Location = new System.Drawing.Point(12, 57);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(68, 19);
            this.radioButton2.TabIndex = 3;
            this.radioButton2.Text = "Tu";
            this.radioButton2.UseVisualStyleBackColor = false;
            // 
            // radioButton1
            // 
            this.radioButton1.BackColor = System.Drawing.Color.Transparent;
            this.radioButton1.Checked = true;
            this.radioButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton1.Location = new System.Drawing.Point(12, 30);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(96, 19);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Vous";
            this.radioButton1.UseVisualStyleBackColor = false;
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
            // bValide
            // 
            this.bValide.BackColor = System.Drawing.Color.Transparent;
            this.bValide.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bValide.FlatAppearance.BorderSize = 0;
            this.bValide.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bValide.ImageIndex = 2;
            this.bValide.ImageList = this.imageList1;
            this.bValide.Location = new System.Drawing.Point(253, 479);
            this.bValide.Name = "bValide";
            this.bValide.Size = new System.Drawing.Size(60, 60);
            this.bValide.TabIndex = 53;
            this.toolTip1.SetToolTip(this.bValide, "Enregistre le médecin traitant");
            this.bValide.UseVisualStyleBackColor = false;
            this.bValide.Click += new System.EventHandler(this.bValide_Click);
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
            this.bCancel.Location = new System.Drawing.Point(782, 479);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(60, 60);
            this.bCancel.TabIndex = 54;
            this.toolTip1.SetToolTip(this.bCancel, "Annuler");
            this.bCancel.UseVisualStyleBackColor = false;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // bQuitterAjoutDest
            // 
            this.bQuitterAjoutDest.BackColor = System.Drawing.Color.Transparent;
            this.bQuitterAjoutDest.BackgroundImage = global::ImportSosGeneve.Properties.Resources.bEnvoi;
            this.bQuitterAjoutDest.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bQuitterAjoutDest.FlatAppearance.BorderSize = 0;
            this.bQuitterAjoutDest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bQuitterAjoutDest.ImageIndex = 19;
            this.bQuitterAjoutDest.ImageList = this.imageList1;
            this.bQuitterAjoutDest.Location = new System.Drawing.Point(1029, 479);
            this.bQuitterAjoutDest.Name = "bQuitterAjoutDest";
            this.bQuitterAjoutDest.Size = new System.Drawing.Size(60, 60);
            this.bQuitterAjoutDest.TabIndex = 59;
            this.toolTip1.SetToolTip(this.bQuitterAjoutDest, "Quitter les destinataires");
            this.bQuitterAjoutDest.UseVisualStyleBackColor = false;
            this.bQuitterAjoutDest.Click += new System.EventHandler(this.bQuitterAjoutDest_Click);
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
            this.bSuppr.Location = new System.Drawing.Point(550, 479);
            this.bSuppr.Name = "bSuppr";
            this.bSuppr.Size = new System.Drawing.Size(60, 60);
            this.bSuppr.TabIndex = 60;
            this.toolTip1.SetToolTip(this.bSuppr, "Supprimer");
            this.bSuppr.UseVisualStyleBackColor = false;
            this.bSuppr.Click += new System.EventHandler(this.bSuppr_Click);
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
            this.bNvxDestinataire.Location = new System.Drawing.Point(172, 479);
            this.bNvxDestinataire.Name = "bNvxDestinataire";
            this.bNvxDestinataire.Size = new System.Drawing.Size(60, 60);
            this.bNvxDestinataire.TabIndex = 61;
            this.toolTip1.SetToolTip(this.bNvxDestinataire, "Nouveau destinataire");
            this.bNvxDestinataire.UseVisualStyleBackColor = false;
            this.bNvxDestinataire.Click += new System.EventHandler(this.bNvxDestinataire_Click);
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
            this.bMaj.Location = new System.Drawing.Point(430, 479);
            this.bMaj.Name = "bMaj";
            this.bMaj.Size = new System.Drawing.Size(60, 60);
            this.bMaj.TabIndex = 62;
            this.toolTip1.SetToolTip(this.bMaj, "Mise à jour du médecin traitant");
            this.bMaj.UseVisualStyleBackColor = false;
            this.bMaj.Click += new System.EventHandler(this.bMaj_Click);
            // 
            // bAjoutDest
            // 
            this.bAjoutDest.BackColor = System.Drawing.Color.Transparent;
            this.bAjoutDest.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bAjoutDest.FlatAppearance.BorderSize = 0;
            this.bAjoutDest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bAjoutDest.ImageIndex = 16;
            this.bAjoutDest.ImageList = this.imageList1;
            this.bAjoutDest.Location = new System.Drawing.Point(26, 479);
            this.bAjoutDest.Name = "bAjoutDest";
            this.bAjoutDest.Size = new System.Drawing.Size(60, 60);
            this.bAjoutDest.TabIndex = 63;
            this.toolTip1.SetToolTip(this.bAjoutDest, "Ajoute comme destinataire");
            this.bAjoutDest.UseVisualStyleBackColor = false;
            this.bAjoutDest.Click += new System.EventHandler(this.bAjoutDest_Click);
            // 
            // frmAjoutDestinataire
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.ClientSize = new System.Drawing.Size(1101, 555);
            this.ControlBox = false;
            this.Controls.Add(this.bAjoutDest);
            this.Controls.Add(this.bMaj);
            this.Controls.Add(this.bNvxDestinataire);
            this.Controls.Add(this.bSuppr);
            this.Controls.Add(this.bQuitterAjoutDest);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bValide);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.GpMedecin);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmAjoutDestinataire";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ajout d\'un destinataire";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.GpMedecin.ResumeLayout(false);
            this.GpMedecin.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion


        private void bQuitterAjoutDest_Click(object sender, EventArgs e)
        {       
			this.Close();
		}

        //Recherche du destinataire
        private void bRechercher_Click(object sender, EventArgs e)
        {
			lwDestinataire.Items.Clear();

			DataSet ds;

			if(cbTypeDestinataire.SelectedIndex==0) //pas en interne (= 1)
			{				                                                
                ds = OutilsExt.OutilsSql.ListeMedVille(tBoxNom.Text , Prenom.Text, cBoxActif.Checked);
				for(int i=0;i<ds.Tables[0].Rows.Count;i++)
				{
					ListViewItem item = new ListViewItem(ds.Tables[0].Rows[i]["Nom"].ToString() + " " + ds.Tables[0].Rows[i]["Prenom"].ToString());
					item.Tag  =ds.Tables[0].Rows[i];
					lwDestinataire.Items.Add(item);
				}
			}

			FicheMedecinVierge();
		}

		private void lwDestinataire_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			FicheMedecinVierge();

			if(lwDestinataire.SelectedIndices.Count>0)
			{				
				if(cbTypeDestinataire.SelectedIndex == 0)
				{
					ListViewItem item = (ListViewItem)lwDestinataire.SelectedItems[0];
					AfficheMedecinTraitant((System.Data.DataRow)item.Tag);

                    groupBox1.Visible = true;

                    bQuitterAjoutDest.Enabled = true;
                    bQuitterAjoutDest.ImageIndex = 19;

                    bAjoutDest.Enabled = true;
                    bAjoutDest.ImageIndex = 16;

                    bMaj.ImageIndex = 17;
                    bMaj.Enabled = true;

                    bNvxDestinataire.ImageIndex = 6;
                    bNvxDestinataire.Enabled = true;

                    bSuppr.Enabled = true;
                    bSuppr.ImageIndex = 0;

                    bCancel.Enabled = false;
                    bCancel.ImageIndex = 5;

                    bValide.Enabled = false;
                    bValide.ImageIndex = 3;
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
			cbCivilite.Text = "";
			cbModeEnvoi.Text = "";
			txtMail.Text = "";
			txtDestinataire.Text="";
			cbActive.Checked = true;
		}

		private void AfficheMedecinTraitant(DataRow row)
		{
			GpMedecin.Tag = row;
			txtNom.Text = row["Nom"].ToString();
			txtPrenom.Text = row["Prenom"].ToString();
			cbSexe.Text = row["Sexe"].ToString();
			txtSpecialite.Text = row["Specialite"].ToString();
			txtDateNaissance.Text = row["DateNaissance"].ToString();
			txtDateDeces.Text = row["DateDeces"].ToString();
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
			cbCivilite.Text = row["Civilite"].ToString();
			cbModeEnvoi.Text = row["ModeEnvoi"].ToString();
			txtMail.Text = row["email"].ToString();
			txtDestinataire.Text = row["Destinataire"].ToString();

            if (row["Active"].ToString() == "1")
                cbActive.Checked = true;
            else
                cbActive.Checked = false;
		}

        public void AfficheMedecin(SosMedecins.SmartRapport.DAL.dstDestination.DestinationRow Destrow)
		{
			switch(Destrow.TypeDestinataire)
			{
				case "MedecinTraitant":
					AfficheMedecinTraitant(OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet("SELECT * from medecinsville WHERE num = " + Destrow.CodeDestinataire).Tables[0].Rows[0]);
					cbModeEnvoi.Text = Destrow.RapModeEnvoi;
					cbTypeDestinataire.SelectedIndex=0;
					if(!Destrow.IsIntimeNull() && Destrow.Intime==0)
						radioButton1.Checked = true;
					else
						radioButton2.Checked =false;
					if(!Destrow.IsIntimeNull() && Destrow.Logo==1)
						checkBox1.Checked = true;
					else
						checkBox1.Checked =false;				

                    break;
				default:
					break;
			}
		}

		//Ajout d'un destinataire :
        private void bAjoutDest_Click(object sender, EventArgs e)
        {      
			//Dans le cadre d'une saisie de Rapport
			if(!PourTeleAlarme)
			{			
				//Envoi pour un médecin traitant :
				if(cbTypeDestinataire.SelectedIndex==0 && lwDestinataire.SelectedIndices.Count>0 && cbModeEnvoi.SelectedIndex>-1)
				{
					if(txtFax.Text=="" && cbModeEnvoi.Text=="Fax") 
					{
						MessageBox.Show("Attention le médecin de possède pas de numéro de fax!");
						return;
					}

					ListViewItem item = lwDestinataire.SelectedItems[0];					
					DataRow row = (DataRow)item.Tag;
					string prenom = WorkedString.FormatePreNom(row["prenom"].ToString());
					if(prenom.Length>=1) prenom = prenom.Substring(0,1).ToUpper() + prenom.Substring(1);
					Destinataire dest = new Destinataire(int.Parse(row["Num"].ToString()),prenom + " " + row["Nom"].ToString().ToUpper());
				
					if(cbModeEnvoi.SelectedIndex==0)
						dest.mode = Destinataire.ModeEnvoi.Aucun;
					else if(cbModeEnvoi.SelectedIndex==1)
						dest.mode = Destinataire.ModeEnvoi.Mail;
					else if(cbModeEnvoi.SelectedIndex==2)
						dest.mode = Destinataire.ModeEnvoi.Fax;
					else if(cbModeEnvoi.SelectedIndex==3)
						dest.mode = Destinataire.ModeEnvoi.Courrier;

					if(row["Nom"].ToString().ToUpper()=="VIDE")
					{
						dest.Nom="";
						cbModeEnvoi.SelectedIndex=3;
						dest.mode = Destinataire.ModeEnvoi.Courrier;
					}
					
					dest.type = Destinataire.TypeDestinataire.MedecinTraitant;					

					//De quel sexe est le médecin traitant?
					string chrSexe = row["Sexe"].ToString();
					if(row["Nom"].ToString().ToUpper()=="VIDE")
						chrSexe = cbSexe.Text;

					//On prépare l'objet destinataire :
                    SosMedecins.SmartRapport.DAL.dstDestination.DestinationRow Destrow = Donnees.MonDtDestination.Destination.NewDestinationRow();
					Destrow.NRapport = Donnees.MonDtRapport.Rapport[0].NRapport;
					Destrow.mail = row["email"].ToString();
					Destrow.CodeDestinataire = dest.Code;
					Destrow.RapDestinataire = cbCivilite.Text;
					if(cbCivilite.Text!="")
						Destrow.RapDestinataire += "\r\n";
					Destrow.RapDestinataire+= dest.Nom.Trim() + "\r\n";
					if(txtDestinataire.Text!="")
						Destrow.RapDestinataire+=txtDestinataire.Text + "\r\n";
					Destrow.RapDestinataire += txtAdresse.Text + " " + txtNumAdresse.Text + "\r\n" + txtNp.Text + " " + txtLocalite.Text;
					Destrow.Nom = dest.Nom;
					Destrow.RapModeEnvoi = dest.mode.ToString();
					Destrow.TypeDestinataire = dest.type.ToString();

					// Faire apparaître le logo sur le rapport ?
					if(checkBox1.Checked)
						Destrow.Logo =1;
					else
						Destrow.Logo =0;
                    
					// Faire apparaître la notion de COPIE ?
					if(chkCopie.Checked)
						Destrow.Copie =1;
					else
						Destrow.Copie =0;

					// Est-ce une lettre pour qq1 d'intime ou pour qq1 que l'on vouvoie?
					if(radioButton2.Checked)
					{
						Destrow.Intime = 1;
						if(chrSexe=="F")
						{
							Destrow.RapBonjour = "Chère Amie,";
							Destrow.RapSalutation = "Reçois, chère Amie, mes salutations les plus confraternelles.";
						}
						else
						{
							Destrow.RapBonjour = "Cher Ami,";
							Destrow.RapSalutation = "Reçois, cher Ami, mes salutations les plus confraternelles.";
						}


                        SosMedecins.SmartRapport.DAL.dstRapport.RapportRow rap = Donnees.MonDtRapport.Rapport[0];
						string age ="";
						if(rap.DateNaissance!=System.DBNull.Value.ToString())
							age = " né le " + DateTime.Parse(rap.DateNaissance).ToLongDateString() + " ";
						else
							age = rap.Age + " " + WorkedString.GetAgeFormate(rap.UniteAge) + " ";

						// Date à faire apparaitre = Date d'arrivée sur les lieux
						// sinon Date d'acquittement
						// sinon Date d'appel
						string strDateToShow = "";

						try
						{
							if(!rap.IsDSLNull())
							{
								strDateToShow = WorkedString.GetDateFormatee(rap.DSL);
							}
							else
							{
								strDateToShow = WorkedString.GetDateFormatee(rap.DAP);
							}
						}
						catch
						{
							strDateToShow = WorkedString.GetDateFormatee(rap.DAP);
						}

                        if (Donnees.MonDtRapport.Rapport[0].Deces == 1)
                            Destrow.RapIntroduction = "J'ai le regret de te faire part du décès de " + WorkedString.GetPatientFormate(rap.Sexe, true, false) + " survenu " + strDateToShow + ".";
                        else
                        {
                            //Si c'est une infirmière qui a vu le patient dans le cadre d'une téléconsultation
                            if (Donnees.MonDtRapport.Rapport[0].PriseEnChargePatient == "Infirmière")
                                Destrow.RapIntroduction = "Téléconsultation réalisée dans le cadre du projet Medlink, " + strDateToShow + ".";
                            else
                                Destrow.RapIntroduction = "J'ai vu " + WorkedString.GetPatientFormate(rap.Sexe, true, false) + " en urgence " + strDateToShow + ".";
                        }
					}
					else
					{
						Destrow.Intime = 0;
                        if (chrSexe == "F")
                        {
                            Destrow.RapBonjour = "Madame et chère Consoeur,";


                            if ((DateTime.Now.ToString("MM") == "12") && (int.Parse(DateTime.Now.ToString("dd")) > 15))
                            {
                                //Fête de Noël
                                Destrow.RapSalutation = "Je reste à votre entière disposition pour tout renseignement que vous jugeriez utile de me demander notamment à travers mon courriel " + Donnees.MonDtRapport.Rapport[0].Mail + ", et vous souhaite, Madame et chère Consoeur, de bonnes Fêtes de fin d'année.";
                            }
                            else if ((DateTime.Now.ToString("MM") == "01") && (int.Parse(DateTime.Now.ToString("dd")) < 15))
                            {
                                //Jour de l'an
                                Destrow.RapSalutation = "Je reste à votre entière disposition pour tout renseignement que vous jugeriez utile de me demander notamment à travers mon courriel " + Donnees.MonDtRapport.Rapport[0].Mail + ", et vous souhaite, Madame et chère Consoeur, une très bonne année " + DateTime.Now.ToString("yyyy") + ".";
                            }
                            else
                            {
                                //Normal
                                Destrow.RapSalutation = "Je reste à votre entière disposition pour tout renseignement que vous jugeriez utile de me demander notamment à travers mon courriel " + Donnees.MonDtRapport.Rapport[0].Mail + ", et je vous prie de recevoir, Madame et chère Consoeur, mes salutations confraternelles.";
                            }
                        }
                        else
                        {
                            Destrow.RapBonjour = "Monsieur et cher Confrère,";

                            if ((DateTime.Now.ToString("MM") == "12") && (int.Parse(DateTime.Now.ToString("dd")) > 15))
                            {
                                //Fête de Noël
                                Destrow.RapSalutation = "Je reste à votre entière disposition pour tout renseignement que vous jugeriez utile de me demander notamment à travers mon courriel " + Donnees.MonDtRapport.Rapport[0].Mail + ", et vous souhaite, Monsieur et cher Confrère, de bonnes Fêtes de fin d'année.";
                            }
                            else if ((DateTime.Now.ToString("MM") == "01") && (int.Parse(DateTime.Now.ToString("dd")) < 15))
                            {
                                //Jour de l'an
                                Destrow.RapSalutation = "Je reste à votre entière disposition pour tout renseignement que vous jugeriez utile de me demander notamment à travers mon courriel " + Donnees.MonDtRapport.Rapport[0].Mail + ", et vous souhaite, Monsieur et cher Confrère, une très bonne année " + DateTime.Now.ToString("yyyy") + ".";
                            }
                            else
                            {
                                //Normal
                                Destrow.RapSalutation = "Je reste à votre entière disposition pour tout renseignement que vous jugeriez utile de me demander notamment à travers mon courriel " + Donnees.MonDtRapport.Rapport[0].Mail + ", et je vous prie de recevoir, Monsieur et cher Confrère, mes salutations confraternelles.";
                            }

                        }            

                        SosMedecins.SmartRapport.DAL.dstRapport.RapportRow rap = Donnees.MonDtRapport.Rapport[0];
						string age ="";
						if(rap.DateNaissance!=System.DBNull.Value.ToString())
							age = " né le " + DateTime.Parse(rap.DateNaissance).ToLongDateString() + " ";
						else
							age = rap.Age + " " + WorkedString.GetAgeFormate(rap.UniteAge) + " ";

						// Date à faire apparaitre = Date d'arrivée sur les lieux
						// sinon Date d'acquittement
						// sinon Date d'appel
						string strDateToShow = "";

						try
						{
							if(!rap.IsDSLNull())
							{
								strDateToShow = WorkedString.GetDateFormatee(rap.DSL);
							}
							else
							{
								strDateToShow = WorkedString.GetDateFormatee(rap.DAP);
							}
						}
						catch
						{
							strDateToShow = WorkedString.GetDateFormatee(rap.DAP);
						}

                        if (Donnees.MonDtRapport.Rapport[0].Deces == 1)
                            Destrow.RapIntroduction = "J'ai le regret de vous faire part du décès de " + WorkedString.GetPatientFormate(rap.Sexe, false, false) + " survenu " + strDateToShow + ".";
                        else
                        {
                            //Si c'est une infirmière qui a vu le patient dans le cadre d'une téléconsultation
                            if (Donnees.MonDtRapport.Rapport[0].PriseEnChargePatient == "Infirmière")
                                Destrow.RapIntroduction = "Téléconsultation réalisée dans le cadre du projet Medlink, " + strDateToShow + ".";
                            else
                                Destrow.RapIntroduction = "J'ai vu " + WorkedString.GetPatientFormate(rap.Sexe, false, false) + " en urgence " + strDateToShow + ".";
                        }
					}						
			
					// Comme on a choisi un médecin traitant, on supprime le rapport en interne :
					// Suppression de la liste également.
					_frmGeneral.SupprimeDestinataires(Destinataire.TypeDestinataire.Interne);
					for(int i=0;i<Donnees.MonDtDestination.Destination.Count;i++)
						if(Donnees.MonDtDestination.Destination[i].TypeDestinataire==Destinataire.TypeDestinataire.Interne.ToString())
							Donnees.MonDtDestination.Destination[i].Delete();
					

					// on ajoute l'enregistrement à la liste des destinataires
					Donnees.MonDtDestination.Destination.AddDestinationRow(Destrow);
					_frmGeneral.AjouteDestinataires(Destrow);
					_frmGeneral.AffichageRapportAvecDestinataire(Destrow);
				}
				// Envoi du rapport en interne
				else if(cbTypeDestinataire.SelectedIndex==1)
				{
					Destinataire dest = new Destinataire(-1,"interne");
					dest.mode = Destinataire.ModeEnvoi.Aucun;
					dest.type = Destinataire.TypeDestinataire.Interne;
				
					// Création de l'objet destinataire
                    SosMedecins.SmartRapport.DAL.dstDestination.DestinationRow Destrow = Donnees.MonDtDestination.Destination.NewDestinationRow();
					Destrow.NRapport = Donnees.MonDtRapport.Rapport[0].NRapport;
					Destrow.CodeDestinataire = dest.Code;
					Destrow.RapDestinataire = dest.Nom;
					Destrow.Nom = dest.Nom;
					Destrow.RapModeEnvoi = dest.mode.ToString();
					Destrow.TypeDestinataire = dest.type.ToString();

					// Logo?
					if(checkBox1.Checked)
						Destrow.Logo =1;
					else
						Destrow.Logo =0;

					// Symbole COPIE?
					if(chkCopie.Checked)
						Destrow.Copie =1;
					else
						Destrow.Copie =0;

					// Date à faire apparaitre = Date d'arrivée sur les lieux
					// sinon Date d'acquittement
					// sinon Date d'appel
					string strDateToShow = "";
                    SosMedecins.SmartRapport.DAL.dstRapport.RapportRow rapD = Donnees.MonDtRapport.Rapport[0];

					try
					{
						if(!rapD.IsDSLNull())
						{
							strDateToShow = WorkedString.GetDateFormatee(rapD.DSL);
						}
						else
						{
							strDateToShow = WorkedString.GetDateFormatee(rapD.DAP);
						}
					}
					catch
					{
						strDateToShow = WorkedString.GetDateFormatee(rapD.DAP);
					}

					// Type de rapport :
					switch(m_intTypeRapport)
					{
						// Rapport médical :
						case 1:				
							Destrow.RapBonjour = "";
							Destrow.RapSalutation = "";
                            SosMedecins.SmartRapport.DAL.dstRapport.RapportRow rap = Donnees.MonDtRapport.Rapport[0];
							string age ="";
							if(rap.DateNaissance!=System.DBNull.Value.ToString())
								age = " né le " + DateTime.Parse(rap.DateNaissance).ToLongDateString() + " ";
							else
								age = rap.Age + " " + WorkedString.GetAgeFormate(rap.UniteAge) + " " ;

                            //Si c'est une infirmière qui a vu le patient dans le cadre d'une téléconsultation
                            if (Donnees.MonDtRapport.Rapport[0].PriseEnChargePatient == "Infirmière")
                                Destrow.RapIntroduction = "Téléconsultation réalisée dans le cadre du projet Medlink, " + strDateToShow + ".";
                            else
                                Destrow.RapIntroduction = "Patient(e) vu(e) en urgence " + strDateToShow + ".";						
							break;
						//Constat de lésions
						case 2:
							Destrow.RapBonjour = "";
							Destrow.RapSalutation = "";
                            SosMedecins.SmartRapport.DAL.dstRapport.RapportRow rap1 = Donnees.MonDtRapport.Rapport[0];
							Destrow.RapIntroduction = "Patient(e) vu(e) en urgence " + strDateToShow + ".";						
							break;
						default:
							break;
					}
			
					// Ajout de l'enregistrement
					Donnees.MonDtDestination.Destination.AddDestinationRow(Destrow);
					_frmGeneral.AjouteDestinataires(Destrow);
					_frmGeneral.AffichageRapportAvecDestinataire(Destrow);
				}
				
				// Envoi du constat à l'hotel de police
			/*	else if (cbTypeDestinataire.SelectedIndex==3)
				{
                    SosMedecins.SmartRapport.DAL.dstDestination.DestinationRow Destrow = AjouteHotelPolice();	
					_frmGeneral.AjouteDestinataires(Destrow);	
					_frmGeneral.AffichageRapportAvecDestinataire(Destrow);
				}*/
			}
			else
			{
				// C'est dans le cadre d'une Télé-Alarme on n'affiche pas les styles d'envois
				if(lwDestinataire.SelectedIndices.Count>0)
				{
					ListViewItem item = lwDestinataire.SelectedItems[0];
					DataRow row = (DataRow)item.Tag;
					string prenom = row["prenom"].ToString().ToLower();
					if(prenom.Length>=1) prenom = prenom.Substring(0,1).ToUpper() + prenom.Substring(1);
					string nom = row["nom"].ToString().ToUpper();
					int code = int.Parse(row["Num"].ToString());
					m_Ta.AjouteMedTTT(code,nom + " " + prenom);
				}
			}		

			this.Close();
		}

        public SosMedecins.SmartRapport.DAL.dstDestination.DestinationRow SelectionAutomatiquePourConstat()
		{
			return AjouteHotelPolice();				
		}
        public SosMedecins.SmartRapport.DAL.dstDestination.DestinationRow SelectionAutomatiquePourRapport()
		{
			return AjouteInterne();				
		}
        public SosMedecins.SmartRapport.DAL.dstDestination.DestinationRow SelectionAutomatiqueMedTTTPourRapport(int IdMedecin)
		{
			return AjouteMedTTT(IdMedecin);			
		}

        private SosMedecins.SmartRapport.DAL.dstDestination.DestinationRow AjouteHotelPolice()
		{
			// Création du destinataire police
			Destinataire dest = new Destinataire(-2,"Hotel de police");
			//dest.mode = Destinataire.ModeEnvoi.Fax;
            dest.mode = Destinataire.ModeEnvoi.Courrier;	
			dest.type = Destinataire.TypeDestinataire.HotelPolice;

			// Propriétés de l'envoi
            SosMedecins.SmartRapport.DAL.dstDestination.DestinationRow Destrow = Donnees.MonDtDestination.Destination.NewDestinationRow();
			Destrow.NRapport = Donnees.MonDtRapport.Rapport[0].NRapport;
			Destrow.CodeDestinataire = dest.Code;
			DataSet commissariat = OutilsExt.OutilsSql.HotelPolice();
			Destrow.RapDestinataire =  commissariat.Tables[0].Rows[0]["Civilite"].ToString() + "\r\n" + commissariat.Tables[0].Rows[0]["Prenom"].ToString() + " " +  commissariat.Tables[0].Rows[0]["Nom"].ToString() + "\r\n" +  commissariat.Tables[0].Rows[0]["Rue"].ToString() + "\r\n" + commissariat.Tables[0].Rows[0]["Np"].ToString() + " " + commissariat.Tables[0].Rows[0]["Commune"].ToString();
			Destrow.Nom = dest.Nom;
			Destrow.RapModeEnvoi = dest.mode.ToString();
			Destrow.TypeDestinataire = dest.type.ToString();
			
			// Logo?
			if(checkBox1.Checked)
				Destrow.Logo =1;
			else
				Destrow.Logo =0;

			// Copie?
			if(chkCopie.Checked)
				Destrow.Copie =1;
			else
				Destrow.Copie =0;

			// Corps du constat
			Destrow.RapBonjour = "";
			Destrow.RapSalutation = "Recevez, " + commissariat.Tables[0].Rows[0]["Civilite"].ToString() + ", mes salutations les meilleures.";
            SosMedecins.SmartRapport.DAL.dstRapport.RapportRow rap = Donnees.MonDtRapport.Rapport[0];
			string age ="";
			if(rap.DateNaissance!=System.DBNull.Value.ToString())
				age = " né le " + DateTime.Parse(rap.DateNaissance).ToLongDateString() + " ";
			else
				age = rap.Age + " " + WorkedString.GetAgeFormate(rap.UniteAge) + " ";
			
			string adresse = "";
			if(rap.Rue.Split(',').Length==2)
				adresse+= WorkedString.FirstLetterUpper(rap.Rue.Split(',')[1]) + " " + WorkedString.FirstLetterUpper(rap.Rue.Split(',')[0]) ;
			else
				adresse+= WorkedString.FirstLetterUpper(rap.Rue) ;
			if(rap.NumRue!="")
				adresse+= " " + rap.NumRue;
			if(rap.CodePostal!="" || rap.Commune!="")
				adresse+= ", " + rap.CodePostal + " " + WorkedString.FirstLetterUpper(rap.Commune);
			if(adresse!="")
				adresse=", habitant " + adresse;

			string heuresl = "";
			if(!rap.IsDSLNull())
				heuresl = " à " + rap.DSL.ToShortTimeString().Replace(":","h");

			Destrow.RapIntroduction = "Le médecin soussigné certifie avoir examiné le " + rap.DSL.ToString().Split(' ')[0] + heuresl + " suite à un appel reçu à " + rap.DAP.ToShortTimeString().Replace(":","h") + ", " + WorkedString.GetSexeFormate(rap.Sexe) + " " + rap.PrenomPatient + " " + rap.NomPatient +  adresse + ".";		
			
			Donnees.MonDtDestination.Destination.AddDestinationRow(Destrow);
			return Destrow;
		}

        private SosMedecins.SmartRapport.DAL.dstDestination.DestinationRow AjouteInterne()
		{
			// Objet destinataire
			Destinataire dest = new Destinataire(-1,"interne");
			dest.mode = Destinataire.ModeEnvoi.Aucun;
			dest.type = Destinataire.TypeDestinataire.Interne;

            SosMedecins.SmartRapport.DAL.dstDestination.DestinationRow Destrow = Donnees.MonDtDestination.Destination.NewDestinationRow();
			Destrow.NRapport = Donnees.MonDtRapport.Rapport[0].NRapport;
			Destrow.CodeDestinataire = dest.Code;
			Destrow.RapDestinataire = dest.Nom;
			Destrow.Nom = dest.Nom;
			Destrow.RapModeEnvoi = dest.mode.ToString();
			Destrow.TypeDestinataire = dest.type.ToString();

			// Logo?
			if(checkBox1.Checked)
				Destrow.Logo =1;
			else
				Destrow.Logo =0;

			// Copie?
			if(chkCopie.Checked)
				Destrow.Copie =1;
			else
				Destrow.Copie =0;

			// Corps du rapport
			Destrow.RapBonjour = "";
			Destrow.RapSalutation = "";
            SosMedecins.SmartRapport.DAL.dstRapport.RapportRow rap = Donnees.MonDtRapport.Rapport[0];

			// Date à faire apparaitre = Date d'arrivée sur les lieux
			// sinon Date d'acquittement
			// sinon Date d'appelPatiente vue en urgence
			string strDateToShow = "";

			try
			{
				if(!rap.IsDSLNull())
				{
					strDateToShow = WorkedString.GetDateFormatee(rap.DSL);
				}
				else
				{
					strDateToShow = WorkedString.GetDateFormatee(rap.DAP);
				}
			}
			catch
			{
				strDateToShow = WorkedString.GetDateFormatee(rap.DAP);
			}

			string age ="";
			if(rap.DateNaissance!=System.DBNull.Value.ToString())
				age = " né le " + DateTime.Parse(rap.DateNaissance).ToLongDateString() + " ";
			else
				age = rap.Age + " " + WorkedString.GetAgeFormate(rap.UniteAge) + " " ;

            //Si c'est une infirmière qui a vu le patient dans le cadre d'une téléconsultation
            if (Donnees.MonDtRapport.Rapport[0].PriseEnChargePatient == "Infirmière")
                Destrow.RapIntroduction = "Téléconsultation réalisée dans le cadre du projet Medlink, " + strDateToShow + ".";
            else
            {
                if (rap.Sexe == "M" || rap.Sexe == "H")
                    Destrow.RapIntroduction = "Patient vu en urgence " + strDateToShow + ".";
                else if (rap.Sexe == "F")
                    Destrow.RapIntroduction = "Patiente vue en urgence " + strDateToShow + ".";
                else
                    Destrow.RapIntroduction = "Patient(e) vu(e) en urgence " + strDateToShow + ".";
            }

			Donnees.MonDtDestination.Destination.AddDestinationRow(Destrow);
			return Destrow;
		}

        private SosMedecins.SmartRapport.DAL.dstDestination.DestinationRow AjouteMedTTT(int IdMedecin)
		{
			DataSet ds = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet("SELECT * from medecinsville WHERE Num = " + IdMedecin);
			DataRow row = null;
			if(ds!=null && ds.Tables.Count>0 && ds.Tables[0].Rows.Count>0)
			{
				row = ds.Tables[0].Rows[0];
			}
			if(row==null) return null;

			string prenom = WorkedString.FormatePreNom(row["prenom"].ToString());
			if(prenom.Length>=1) prenom = prenom.Substring(0,1).ToUpper() + prenom.Substring(1);
			Destinataire dest = new Destinataire(int.Parse(row["Num"].ToString()),prenom + " " + row["Nom"].ToString().ToUpper());
			dest.mode = Destinataire.ModeEnvoi.Aucun;
			if(row["ModeEnvoi"].ToString()==Destinataire.ModeEnvoi.Fax.ToString())
				dest.mode = Destinataire.ModeEnvoi.Fax;
			else if(row["ModeEnvoi"].ToString()==Destinataire.ModeEnvoi.Courrier.ToString())
				dest.mode = Destinataire.ModeEnvoi.Courrier;
			if(row["ModeEnvoi"].ToString()==Destinataire.ModeEnvoi.Mail.ToString())
				dest.mode = Destinataire.ModeEnvoi.Mail;						
			dest.type = Destinataire.TypeDestinataire.MedecinTraitant;					

			// De quel sexe est le médecin traitant?
			string chrSexe = row["Sexe"].ToString();
		

			// on prépare l'objet destinataire :
            SosMedecins.SmartRapport.DAL.dstDestination.DestinationRow Destrow = Donnees.MonDtDestination.Destination.NewDestinationRow();
			Destrow.NRapport = Donnees.MonDtRapport.Rapport[0].NRapport;
			Destrow.mail = row["email"].ToString();
			Destrow.CodeDestinataire = dest.Code;
			Destrow.RapDestinataire = row["Civilite"].ToString();
			if(row["Civilite"].ToString()!="")
				Destrow.RapDestinataire += "\r\n";
			Destrow.RapDestinataire+= dest.Nom.Trim() + "\r\n";			
			Destrow.RapDestinataire+= row["Rue"].ToString() + " " + row["NumeroRue"].ToString() + "\r\n" + row["Np"].ToString() + " " + row["Commune"].ToString();
			Destrow.Nom = dest.Nom;
			Destrow.RapModeEnvoi = dest.mode.ToString();
			Destrow.TypeDestinataire = dest.type.ToString();

			// Faire apparaître le logo sur le rapport ?
			if(dest.mode==Destinataire.ModeEnvoi.Fax)
				Destrow.Logo=1;
			else if(dest.mode==Destinataire.ModeEnvoi.Courrier)
				Destrow.Logo=0;
			else
				Destrow.Logo = 1;			
                    
			Destrow.Copie =0;
			Destrow.Intime = 0;

            if (chrSexe == "F")
            {
                Destrow.RapBonjour = "Madame et chère Consoeur,";


                if ((DateTime.Now.ToString("MM") == "12") && (int.Parse(DateTime.Now.ToString("dd")) > 15))
                {
                    //Fête de Noël
                    Destrow.RapSalutation = "Je reste à votre entière disposition pour tout renseignement que vous jugeriez utile de me demander notamment à travers mon courriel " + Donnees.MonDtRapport.Rapport[0].Mail + ", et vous souhaite, Madame et chère Consoeur, de bonnes Fêtes de fin d'année.";
                }
                else if ((DateTime.Now.ToString("MM") == "01") && (int.Parse(DateTime.Now.ToString("dd")) < 15))
                {
                    //Jour de l'an
                    Destrow.RapSalutation = "Je reste à votre entière disposition pour tout renseignement que vous jugeriez utile de me demander notamment à travers mon courriel " + Donnees.MonDtRapport.Rapport[0].Mail + ", et vous souhaite, Madame et chère Consoeur, une très bonne année " + DateTime.Now.ToString("yyyy") + ".";
                }
                else
                {
                    //Normal
                    Destrow.RapSalutation = "Je reste à votre entière disposition pour tout renseignement que vous jugeriez utile de me demander notamment à travers mon courriel " + Donnees.MonDtRapport.Rapport[0].Mail + ", et je vous prie de recevoir, Madame et chère Consoeur, mes salutations confraternelles.";
                }
            }
            else
            {
                Destrow.RapBonjour = "Monsieur et cher Confrère,";

                if ((DateTime.Now.ToString("MM") == "12") && (int.Parse(DateTime.Now.ToString("dd")) > 15))
                {
                    //Fête de Noël
                    Destrow.RapSalutation = "Je reste à votre entière disposition pour tout renseignement que vous jugeriez utile de me demander notamment à travers mon courriel " + Donnees.MonDtRapport.Rapport[0].Mail + ", et vous souhaite, Monsieur et cher Confrère, de bonnes Fêtes de fin d'année.";
                }
                else if ((DateTime.Now.ToString("MM") == "01") && (int.Parse(DateTime.Now.ToString("dd")) < 15))
                {
                    //Jour de l'an
                    Destrow.RapSalutation = "Je reste à votre entière disposition pour tout renseignement que vous jugeriez utile de me demander notamment à travers mon courriel " + Donnees.MonDtRapport.Rapport[0].Mail + ", et vous souhaite, Monsieur et cher Confrère, une très bonne année " + DateTime.Now.ToString("yyyy") + ".";
                }
                else
                {
                    //Normal
                    Destrow.RapSalutation = "Je reste à votre entière disposition pour tout renseignement que vous jugeriez utile de me demander notamment à travers mon courriel " + Donnees.MonDtRapport.Rapport[0].Mail + ", et je vous prie de recevoir, Monsieur et cher Confrère, mes salutations confraternelles.";
                }

            }            

            SosMedecins.SmartRapport.DAL.dstRapport.RapportRow rap = Donnees.MonDtRapport.Rapport[0];
			string age ="";
			if(rap.DateNaissance!=System.DBNull.Value.ToString())
				age = " né le " + DateTime.Parse(rap.DateNaissance).ToLongDateString() + " ";
			else
				age = rap.Age + " " + WorkedString.GetAgeFormate(rap.UniteAge) + " ";

			// Date à faire apparaitre = Date d'arrivée sur les lieux
			// sinon Date d'acquittement
			// sinon Date d'appel
			string strDateToShow = "";

			try
			{
				if(!rap.IsDSLNull())
				{
					strDateToShow = WorkedString.GetDateFormatee(rap.DSL);
				}
				else
				{
					strDateToShow = WorkedString.GetDateFormatee(rap.DAP);
				}
			}
			catch
			{
				strDateToShow = WorkedString.GetDateFormatee(rap.DAP);
			}

            if (Donnees.MonDtRapport.Rapport[0].Deces == 1)
                Destrow.RapIntroduction = "J'ai le regret de vous faire part du décès de " + WorkedString.GetPatientFormate(rap.Sexe, false, false) + " survenu " + strDateToShow + ".";
            else
            {
                //Si c'est une infirmière qui a vu le patient dans le cadre d'une téléconsultation
                if (Donnees.MonDtRapport.Rapport[0].PriseEnChargePatient == "Infirmière")
                    Destrow.RapIntroduction = "Téléconsultation réalisée dans le cadre du projet Medlink, " + strDateToShow + ".";
                else
                    Destrow.RapIntroduction = "J'ai vu " + WorkedString.GetPatientFormate(rap.Sexe, false, false) + " en urgence " + strDateToShow + ".";
            }
			// on ajoute l'enregistrement à la liste des destinataires
			Donnees.MonDtDestination.Destination.AddDestinationRow(Destrow);
			return Destrow;			
		}

		private void cbTypeDestinataire_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(cbTypeDestinataire.SelectedIndex!=0)
				GpMedecin.Visible = false;
			else
				GpMedecin.Visible = true;
		}


        //On met à jour le médecin traitant
        private void bMaj_Click(object sender, EventArgs e)
        {       
			if(GpMedecin.Tag==null) return;

            EnregistreMedecinTraitant();

            groupBox1.Visible = true;

            bQuitterAjoutDest.Enabled = true;
            bQuitterAjoutDest.ImageIndex = 19;

            bMaj.ImageIndex = 17;
            bMaj.Enabled = true;

            bNvxDestinataire.ImageIndex = 6;
            bNvxDestinataire.Enabled = true;

            bSuppr.Enabled = true;
            bSuppr.ImageIndex = 0;

            bAjoutDest.ImageIndex = 16;
            bAjoutDest.Enabled = true;

            bCancel.Enabled = false;
            bCancel.ImageIndex = 5;

            bValide.Enabled = false;
            bValide.ImageIndex = 3;

		}


        //on supprime le médecin de ville
        private void bSuppr_Click(object sender, EventArgs e)
        {      
			if(GpMedecin.Tag==null) return;
			DataRow row = (DataRow)GpMedecin.Tag;
			if(row==null) return;          

            if (MessageBox.Show("Voulez-vous supprimer ce médecin ?", "Destinataire", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				//On vérifie si ce médecin n'est pas utilisé par d'autre personnes
                if (VerifMedecinAvantSuppr(row["num"].ToString()) == false)
                {
                    MessageBox.Show("Impossible de supprimer ce médecin car il est utilisé par d'autres patients.", "Destinataire", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    OutilsExt.OutilsSql.SupprimerMedecinVille(long.Parse(row["num"].ToString()));

                    MessageBox.Show("Médecin supprimé !", "Destinataire", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    tBoxNom.Text = "";
                    lwDestinataire.Items.Clear();
                    FicheMedecinVierge();

                    //gestion des boutons
                    groupBox1.Visible = true;

                    bQuitterAjoutDest.Enabled = true;
                    bQuitterAjoutDest.ImageIndex = 19;

                    bMaj.ImageIndex = 17;
                    bMaj.Enabled = true;

                    bNvxDestinataire.ImageIndex = 6;
                    bNvxDestinataire.Enabled = true;

                    bSuppr.Enabled = false;
                    bSuppr.ImageIndex = 1;

                    bAjoutDest.ImageIndex = 21;
                    bAjoutDest.Enabled = false;

                    bCancel.Enabled = false;
                    bCancel.ImageIndex = 5;

                    bValide.Enabled = false;
                    bValide.ImageIndex = 3;
                }
			}
		}


        //On créer un nouveau médecin de ville
        private void bNvxDestinataire_Click(object sender, EventArgs e)
        {      
			// Nouveau médecin de ville :
			if(cbTypeDestinataire.SelectedIndex==0)
			{
				// on prépare la fiche vierge 
				FicheMedecinVierge();	
				DataRow row = OutilsExt.OutilsSql.AjouteMedecinVille();
				GpMedecin.Tag = row;
				groupBox1.Visible = false;
								
                bQuitterAjoutDest.Enabled = false;
                bQuitterAjoutDest.ImageIndex = 20;

                bAjoutDest.Enabled = false;
                bAjoutDest.ImageIndex = 21;
                				
                bMaj.ImageIndex = 18;
                bMaj.Enabled = false;

                bNvxDestinataire.ImageIndex = 7;
                bNvxDestinataire.Enabled = false;
				                
                bSuppr.Enabled = false;
                bSuppr.ImageIndex = 1;
				                
                bCancel.Enabled = true;
                bCancel.ImageIndex = 4;

                bValide.Visible = true;
                bValide.Enabled = true;
                bValide.ImageIndex = 2;
			}
		}


        //On enregistre le médecin traitant
        private void bValide_Click(object sender, EventArgs e)
        {        
            string num = EnregistreMedecinTraitant();
            if (num != null)
            {
                groupBox1.Visible = true;
                
                bQuitterAjoutDest.Enabled = true;
                bQuitterAjoutDest.ImageIndex = 19;
                
                bAjoutDest.Enabled = true;
                bAjoutDest.ImageIndex = 16;
               
                bMaj.ImageIndex = 17;
                bMaj.Enabled = true;

                bNvxDestinataire.ImageIndex = 6;
                bNvxDestinataire.Enabled = true;

                bSuppr.Enabled = true;
                bSuppr.ImageIndex = 0;
                                
                bCancel.Enabled = false;
                bCancel.ImageIndex = 5;
               
                bValide.Enabled = false;
                bValide.ImageIndex = 3;
                                
                DataSet ds = OutilsExt.OutilsSql.ListeMedVille(txtNom.Text, txtPrenom.Text, cbActive.Checked);
                lwDestinataire.Items.Clear();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i]["Num"].ToString() == num)
                    {
                        ListViewItem item = new ListViewItem(ds.Tables[0].Rows[i]["Nom"].ToString() + " " + ds.Tables[0].Rows[i]["Prenom"].ToString());
                        item.Tag = ds.Tables[0].Rows[i];
                        lwDestinataire.Items.Add(item);
                    }
                }
            }
		}
        private string EnregistreMedecinTraitant()
        {
            if (txtNom.Text == "" || cbSexe.Text == "" )
            {
                MessageBox.Show("Les informations Nom, ou Sexe sont obligatoires.", "Destinataire", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            DataRow row = (DataRow)GpMedecin.Tag;
            row["Nom"] = txtNom.Text;
            row["Prenom"] = txtPrenom.Text;
            row["Sexe"] = cbSexe.Text;
            row["Specialite"] = txtSpecialite.Text;
           
            if (txtDateNaissance.Text != "")
                row["DateNaissance"] = txtDateNaissance.Text;            
            else            
                row["DateNaissance"] = DBNull.Value;

            if (txtDateDeces.Text != "")                        
                row["DateDeces"] = txtDateDeces.Text;
            else
                row["DateDeces"] = DBNull.Value;

            row["NumeroRue"] = txtNumAdresse.Text;
            row["Rue"] = txtAdresse.Text;
            row["Np"] = txtNp.Text;
            row["Commune"] = txtLocalite.Text;
            row["Telephone"] = txtTelephone.Text;
            row["Natel"] = txtNatel.Text;
            row["Fax"] = txtFax.Text;
            row["NConcordat"] = txtConcordat.Text;
            row["EAN"] = txtEan.Text;
            row["Civilite"] = cbCivilite.Text;
            row["ModeEnvoi"] = cbModeEnvoi.Text;
            row["email"] = txtMail.Text;
            row["Destinataire"] = txtDestinataire.Text;

            if (cbActive.Checked == true)
                row["Active"] = 1;
            else
                row["Active"] = 0;

            SosMedecins.SmartRapport.DAL.MedecinsVille z_objMedecinsTraitant = new SosMedecins.SmartRapport.DAL.MedecinsVille();

            if (z_objMedecinsTraitant.Update(row))
                MessageBox.Show("Fiche médecin enregistrée!");
            else
                MessageBox.Show("Erreur enregistrement médecin!");

            z_objMedecinsTraitant = null;
            return row["num"].ToString();
        }


        private void bCancel_Click(object sender, EventArgs e)
        {
        	FicheMedecinVierge();	

			groupBox1.Visible = true;
						
            bQuitterAjoutDest.Enabled = true;
            bQuitterAjoutDest.ImageIndex = 19;

            bMaj.ImageIndex = 18;
            bMaj.Enabled = false;

            bNvxDestinataire.ImageIndex = 6;
            bNvxDestinataire.Enabled = true;

            bSuppr.Enabled = false;
            bSuppr.ImageIndex = 1;
          			
            bAjoutDest.ImageIndex = 21;
            bAjoutDest.Enabled = false;
			
            bCancel.Enabled = false;
            bCancel.ImageIndex = 5;
			        
            bValide.Enabled = false;
            bValide.ImageIndex = 3;
		}

		private void textBox1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Enter)
			{
                bRechercher_Click(null, null);
				if(lwDestinataire.Items.Count>0)
				{
					lwDestinataire.Focus();
				}
			}
			else if(e.KeyCode==Keys.Tab)
			{
				Prenom.Focus();
			}
		}
		private void Prenom_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Enter)
			{
                bRechercher_Click(null, null);
				if(lwDestinataire.Items.Count>0)
				{
					lwDestinataire.Focus();
				}
			}
		}


        //Recherche du médecin traitant à partir de son n°
        private void RechercheMedTTTParNum(string Num)
        {
            //Connexion à la base
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);

            dbConnection.Open();

            SqlCommand cmd = dbConnection.CreateCommand();

            try
            {
                //On recherche le médecin
                string sqlstr0 = "SELECT * FROM medecinsville WHERE Num =  @NumMedecin";
                cmd.CommandText = sqlstr0;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("NumMedecin", Num);

                DataTable dtMedecin = new DataTable();
                dtMedecin.Load(cmd.ExecuteReader());

                //Si on l'a trouvé
                if(dtMedecin.Rows.Count > 0)
                {                                       
                    GpMedecin.Tag = dtMedecin.Rows[0];   //on affecte les infos au GroupBox Medecin
                    AfficheMedecinTraitant(dtMedecin.Rows[0]);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur Lors de la recherche du Médecin. Le message est: " + e.Message);
            }
            finally
            {
                if (dbConnection.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Close();
                }
            }
        }


        private bool VerifMedecinAvantSuppr(string Num)
        {
            bool OnEfface = false;

            //Connexion à la base                        
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);

            dbConnection.Open();

            SqlCommand cmd = dbConnection.CreateCommand();

            try
            {
                //On recherche si le médecin n'est pas utilisé par d'autres patients
                string sqlstr0 = "SELECT COUNT(num) FROM medecinsville";
                sqlstr0 += " WHERE num = @NumMedecinVille";
                sqlstr0 += " AND num IN (SELECT CodeDestinataire as Num FROM tablerapportdestine";
                sqlstr0 += "             WHERE CodeDestinataire = @NumMedecinVille";
                sqlstr0 += "             UNION"; 
                sqlstr0 += "             SELECT TD_ListeMedecinsTTT as Num FROM ta_abonnementdossier";
                sqlstr0 += "             WHERE TD_ListeMedecinsTTT = @NumMedecinVille)";
                                                
                cmd.CommandText = sqlstr0;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("NumMedecinVille", Num);

                DataTable dtMedecin = new DataTable();
                dtMedecin.Load(cmd.ExecuteReader());

                //Si on a au moins 1 
                if(int.Parse(dtMedecin.Rows[0][0].ToString()) > 0)                                                       
                    OnEfface = false;                
                else
                    OnEfface = true;

            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur Lors de la recherche du Médecin. Le message est: " + e.Message);
            }
            finally
            {
                if (dbConnection.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Close();                    
                }                
            }

            return OnEfface;
        }
       
    }
}


//A faire 
//Avant de supprimer un médecin de ville, vérifier si des patients ne l'on pas!!!