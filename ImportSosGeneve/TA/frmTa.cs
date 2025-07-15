using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace ImportSosGeneve
{
	/// <summary>
	/// Description résumée de frmTa.
	/// </summary>
	public class frmTa : System.Windows.Forms.Form
	{
		private CtrlTA m_CtrlTa=null;
		public frmGeneral _frmgeneral=null;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem mnuAbonnement;
		private System.Windows.Forms.MenuItem mnuNouveau;
		private System.Windows.Forms.MenuItem mnuQuitter;
		private System.Windows.Forms.MenuItem mnuModif;
		private System.Windows.Forms.MenuItem mnuSupprim;
        private System.Windows.Forms.MenuItem menuItem1;
        private SplitContainer splitContainer1;
        private Button bQuitterTA;
        private ImageList imageList1;
        private Button bNvlAbonnement;
        private ToolTip toolTip1;
        private Button bModifier;
        private Button bArchiver;
        private Button bDesarchiver;
        private IContainer components;

		public frmTa(frmGeneral frm)
		{
			//
			// Requis pour la prise en charge du Concepteur Windows Forms
			//
			InitializeComponent();
			this._frmgeneral = frm;
			
			m_CtrlTa = new CtrlTA(frm, this);
            this.splitContainer1.Panel2.Controls.Add(m_CtrlTa);    //On ajoute CtrlTa au panel2
            //this.Controls.Add(m_CtrlTa);
			m_CtrlTa.Top = 0;
			m_CtrlTa.Left = 0;
			//this.Left = m_CtrlTa.Width + 40;
			//this.Height =  m_CtrlTa.Height + 40;

			m_CtrlTa.SetOnglet(0);			
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTa));
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.mnuAbonnement = new System.Windows.Forms.MenuItem();
            this.mnuNouveau = new System.Windows.Forms.MenuItem();
            this.mnuModif = new System.Windows.Forms.MenuItem();
            this.mnuSupprim = new System.Windows.Forms.MenuItem();
            this.mnuQuitter = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.bDesarchiver = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.bArchiver = new System.Windows.Forms.Button();
            this.bModifier = new System.Windows.Forms.Button();
            this.bNvlAbonnement = new System.Windows.Forms.Button();
            this.bQuitterTA = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuAbonnement});
            // 
            // mnuAbonnement
            // 
            this.mnuAbonnement.Index = 0;
            this.mnuAbonnement.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuNouveau,
            this.mnuModif,
            this.mnuSupprim,
            this.mnuQuitter,
            this.menuItem1});
            this.mnuAbonnement.Text = "Abonnement";
            // 
            // mnuNouveau
            // 
            this.mnuNouveau.Index = 0;
            this.mnuNouveau.Text = "Nouveau";
            this.mnuNouveau.Click += new System.EventHandler(this.mnuNouveau_Click);
            // 
            // mnuModif
            // 
            this.mnuModif.Index = 1;
            this.mnuModif.Text = "Modifier";
            this.mnuModif.Click += new System.EventHandler(this.mnuModif_Click);
            // 
            // mnuSupprim
            // 
            this.mnuSupprim.Index = 2;
            this.mnuSupprim.Text = "Archiver";
            this.mnuSupprim.Click += new System.EventHandler(this.mnuSupprim_Click);
            // 
            // mnuQuitter
            // 
            this.mnuQuitter.Index = 3;
            this.mnuQuitter.Text = "Quitter";
            this.mnuQuitter.Click += new System.EventHandler(this.mnuQuitter_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 4;
            this.menuItem1.Text = "DesArchiver";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.CadetBlue;
            this.splitContainer1.Panel1.Controls.Add(this.bDesarchiver);
            this.splitContainer1.Panel1.Controls.Add(this.bArchiver);
            this.splitContainer1.Panel1.Controls.Add(this.bModifier);
            this.splitContainer1.Panel1.Controls.Add(this.bNvlAbonnement);
            this.splitContainer1.Panel1.Controls.Add(this.bQuitterTA);
            this.splitContainer1.Size = new System.Drawing.Size(1290, 931);
            this.splitContainer1.SplitterDistance = 73;
            this.splitContainer1.TabIndex = 0;
            // 
            // bDesarchiver
            // 
            this.bDesarchiver.BackColor = System.Drawing.Color.Transparent;
            this.bDesarchiver.BackgroundImage = global::ImportSosGeneve.Properties.Resources.bEnvoi;
            this.bDesarchiver.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bDesarchiver.FlatAppearance.BorderSize = 0;
            this.bDesarchiver.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bDesarchiver.ImageIndex = 13;
            this.bDesarchiver.ImageList = this.imageList1;
            this.bDesarchiver.Location = new System.Drawing.Point(254, 4);
            this.bDesarchiver.Name = "bDesarchiver";
            this.bDesarchiver.Size = new System.Drawing.Size(60, 60);
            this.bDesarchiver.TabIndex = 62;
            this.toolTip1.SetToolTip(this.bDesarchiver, "Dé-archiver le dossier");
            this.bDesarchiver.UseVisualStyleBackColor = false;
            this.bDesarchiver.Click += new System.EventHandler(this.bDesarchiver_Click);
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
            // 
            // bArchiver
            // 
            this.bArchiver.BackColor = System.Drawing.Color.Transparent;
            this.bArchiver.BackgroundImage = global::ImportSosGeneve.Properties.Resources.bEnvoi;
            this.bArchiver.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bArchiver.FlatAppearance.BorderSize = 0;
            this.bArchiver.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bArchiver.ImageIndex = 12;
            this.bArchiver.ImageList = this.imageList1;
            this.bArchiver.Location = new System.Drawing.Point(165, 4);
            this.bArchiver.Name = "bArchiver";
            this.bArchiver.Size = new System.Drawing.Size(60, 60);
            this.bArchiver.TabIndex = 61;
            this.toolTip1.SetToolTip(this.bArchiver, "Archiver le dossier");
            this.bArchiver.UseVisualStyleBackColor = false;
            this.bArchiver.Click += new System.EventHandler(this.bArchiver_Click);
            // 
            // bModifier
            // 
            this.bModifier.BackColor = System.Drawing.Color.Transparent;
            this.bModifier.BackgroundImage = global::ImportSosGeneve.Properties.Resources.bEnvoi;
            this.bModifier.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bModifier.FlatAppearance.BorderSize = 0;
            this.bModifier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bModifier.ImageIndex = 15;
            this.bModifier.ImageList = this.imageList1;
            this.bModifier.Location = new System.Drawing.Point(81, 4);
            this.bModifier.Name = "bModifier";
            this.bModifier.Size = new System.Drawing.Size(60, 60);
            this.bModifier.TabIndex = 60;
            this.toolTip1.SetToolTip(this.bModifier, "Modifier l\'abonnement");
            this.bModifier.UseVisualStyleBackColor = false;
            this.bModifier.Click += new System.EventHandler(this.bModifier_Click);
            // 
            // bNvlAbonnement
            // 
            this.bNvlAbonnement.BackColor = System.Drawing.Color.Transparent;
            this.bNvlAbonnement.BackgroundImage = global::ImportSosGeneve.Properties.Resources.bEnvoi;
            this.bNvlAbonnement.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bNvlAbonnement.FlatAppearance.BorderSize = 0;
            this.bNvlAbonnement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bNvlAbonnement.ImageIndex = 16;
            this.bNvlAbonnement.ImageList = this.imageList1;
            this.bNvlAbonnement.Location = new System.Drawing.Point(3, 4);
            this.bNvlAbonnement.Name = "bNvlAbonnement";
            this.bNvlAbonnement.Size = new System.Drawing.Size(60, 60);
            this.bNvlAbonnement.TabIndex = 59;
            this.toolTip1.SetToolTip(this.bNvlAbonnement, "Ajouter un nouvel abonnement");
            this.bNvlAbonnement.UseVisualStyleBackColor = false;
            this.bNvlAbonnement.Click += new System.EventHandler(this.bNvlAbonnement_Click);
            // 
            // bQuitterTA
            // 
            this.bQuitterTA.BackColor = System.Drawing.Color.Transparent;
            this.bQuitterTA.BackgroundImage = global::ImportSosGeneve.Properties.Resources.bEnvoi;
            this.bQuitterTA.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bQuitterTA.FlatAppearance.BorderSize = 0;
            this.bQuitterTA.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bQuitterTA.ImageIndex = 17;
            this.bQuitterTA.ImageList = this.imageList1;
            this.bQuitterTA.Location = new System.Drawing.Point(1224, 4);
            this.bQuitterTA.Name = "bQuitterTA";
            this.bQuitterTA.Size = new System.Drawing.Size(60, 60);
            this.bQuitterTA.TabIndex = 58;
            this.toolTip1.SetToolTip(this.bQuitterTA, "Quitter les TA");
            this.bQuitterTA.UseVisualStyleBackColor = false;
            this.bQuitterTA.Click += new System.EventHandler(this.bQuitterTA_Click);
            // 
            // frmTa
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1290, 931);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.mainMenu1;
            this.Name = "frmTa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Télé-Alarme";
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void mnuNouveau_Click(object sender, System.EventArgs e)
		{
			m_CtrlTa.NouveauAbonnement();
		}

		private void mnuQuitter_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void mnuSupprim_Click(object sender, System.EventArgs e)
		{
			m_CtrlTa.SupprimeAbonnement();
		}

		private void mnuModif_Click(object sender, System.EventArgs e)
		{
			//On change l'état du bouton
            //On dé-vérouille les contrôles                   
            bModifier.ImageIndex = 14;
            m_CtrlTa.ModifieAbonnement();
		}

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
			m_CtrlTa.DeSupprimeAbonnement();
		}
                               

        private void bNvlAbonnement_Click(object sender, EventArgs e)
        {
            //On signale que le cadenas est dévérouillé...Sinon ça perturbe les utilisateurs
            bModifier.ImageIndex = 14;
                         
            m_CtrlTa.NouveauAbonnement();
        }

        private void bModifier_Click(object sender, EventArgs e)
        {
            //On vérouille ou dé-vérouille les contrôles
            if (bModifier.ImageIndex == 15)
            {
                bModifier.ImageIndex = 14;
                m_CtrlTa.ModifieAbonnement();
            }
            else
            {
                bModifier.ImageIndex = 15;
                m_CtrlTa.VerrouilleControls();
                //AfficheAbonnement(null);
            }
        }

        private void bArchiver_Click(object sender, EventArgs e)
        {
            m_CtrlTa.SupprimeAbonnement();   //En fait on l'archive
        }

        private void bDesarchiver_Click(object sender, EventArgs e)
        {
            m_CtrlTa.DeSupprimeAbonnement();  //On le dé-archive
        }

        private void bQuitterTA_Click(object sender, EventArgs e)
        {
            this.Close();
        }



	}
}
