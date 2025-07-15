using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using System.Runtime.InteropServices;
using ImportSosGeneve;
using System.Configuration;

namespace ImportSosGeneve
{
	/// <summary>
	/// Description résumée de frmRapport_Objet.
	/// </summary>
	public class frmRapport_Objet : System.Windows.Forms.Form
	{
		//private string _strPathScript = "C:\\Program Files\\NCH Software\\Scribe\\scribe.exe";
		//private string _strPathRepertoire = "\\\\192.168.0.166\\EPOS\\FichiersSon\\";
		private string _strNomRapport = "";

        #region Propriétés publiques

        public string Corps
        {
            set
            {
                m_strCorps = value;
            }
            get
            {
                return m_strCorps;
            }
        }

        #endregion		

		#region Déclaration des variables

		// Variables perso
		private string m_strCorps= "";
		private RubriqueRapport[] TabEnCours=null;


		// Variables générées par VS

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TreeView twCategories;
		private System.Windows.Forms.Button btnValider;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.Button btnAnnuler;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnDictaphone;
        private Panel panel1;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label6;
        private Label label5;
        private TextBox tb_metha_qte;
        private TextBox tb_dorm_qte;
        private TextBox tb_peth_qte;
        private TextBox tb_Morph_qte;
        private TextBox tb_autre_qte;
        private TextBox tb_autre_libelle;
        private Button button3;
        private GroupBox GBoxAuteur;
        private RadioButton RB3;
        private RadioButton RB2;
        private RadioButton RB1;
        private GroupBox GBoxTypeRapport;
        private RadioButton RB6;
        private RadioButton RB5;
        private RadioButton RB4;
        private RadioButton RB3a;
        private RadioButton RB3b;
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		#endregion

		#region Construction DEstruction de la classe

		public frmRapport_Objet()
		{
			//
			// Requis pour la prise en charge du Concepteur Windows Forms
			//
			InitializeComponent();

			//
			// TODO : ajoutez le code du constructeur après l'appel à InitializeComponent
			//
			FabricationTreeNode();
			DynamicRubriques(null);

			richTextBox1.Font = new Font("Arial",12,FontStyle.Regular);
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
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Corps");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRapport_Objet));
            this.label1 = new System.Windows.Forms.Label();
            this.twCategories = new System.Windows.Forms.TreeView();
            this.btnValider = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btnAnnuler = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnDictaphone = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tb_autre_qte = new System.Windows.Forms.TextBox();
            this.tb_autre_libelle = new System.Windows.Forms.TextBox();
            this.tb_metha_qte = new System.Windows.Forms.TextBox();
            this.tb_dorm_qte = new System.Windows.Forms.TextBox();
            this.tb_peth_qte = new System.Windows.Forms.TextBox();
            this.tb_Morph_qte = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.GBoxAuteur = new System.Windows.Forms.GroupBox();
            this.RB3b = new System.Windows.Forms.RadioButton();
            this.RB3a = new System.Windows.Forms.RadioButton();
            this.RB3 = new System.Windows.Forms.RadioButton();
            this.RB2 = new System.Windows.Forms.RadioButton();
            this.RB1 = new System.Windows.Forms.RadioButton();
            this.GBoxTypeRapport = new System.Windows.Forms.GroupBox();
            this.RB6 = new System.Windows.Forms.RadioButton();
            this.RB5 = new System.Windows.Forms.RadioButton();
            this.RB4 = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.GBoxAuteur.SuspendLayout();
            this.GBoxTypeRapport.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(56, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(392, 40);
            this.label1.TabIndex = 1;
            this.label1.Text = "Génération du corps du rapport";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // twCategories
            // 
            this.twCategories.CheckBoxes = true;
            this.twCategories.Location = new System.Drawing.Point(8, 54);
            this.twCategories.Name = "twCategories";
            treeNode3.Name = "";
            treeNode3.Text = "Corps";
            this.twCategories.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3});
            this.twCategories.Size = new System.Drawing.Size(216, 689);
            this.twCategories.TabIndex = 5;
            this.twCategories.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.twCategories_AfterCheck);
            // 
            // btnValider
            // 
            this.btnValider.BackColor = System.Drawing.SystemColors.Control;
            this.btnValider.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnValider.Location = new System.Drawing.Point(656, 8);
            this.btnValider.Name = "btnValider";
            this.btnValider.Size = new System.Drawing.Size(192, 40);
            this.btnValider.TabIndex = 3;
            this.btnValider.Text = "Valider le texte (F1)";
            this.btnValider.UseVisualStyleBackColor = false;
            this.btnValider.Click += new System.EventHandler(this.btnValider_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(798, 32);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(104, 72);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "richTextBox1";
            this.richTextBox1.Visible = false;
            // 
            // btnAnnuler
            // 
            this.btnAnnuler.BackColor = System.Drawing.SystemColors.Control;
            this.btnAnnuler.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnnuler.Location = new System.Drawing.Point(456, 8);
            this.btnAnnuler.Name = "btnAnnuler";
            this.btnAnnuler.Size = new System.Drawing.Size(192, 40);
            this.btnAnnuler.TabIndex = 2;
            this.btnAnnuler.Text = "Annuler";
            this.btnAnnuler.UseVisualStyleBackColor = false;
            this.btnAnnuler.Click += new System.EventHandler(this.btnAnnuler_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Control;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(8, 749);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(216, 32);
            this.button1.TabIndex = 6;
            this.button1.Text = "Copier";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.Control;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(8, 792);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(216, 32);
            this.button2.TabIndex = 7;
            this.button2.Text = "Coller";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnDictaphone
            // 
            this.btnDictaphone.BackColor = System.Drawing.SystemColors.Control;
            this.btnDictaphone.Enabled = false;
            this.btnDictaphone.Image = ((System.Drawing.Image)(resources.GetObject("btnDictaphone.Image")));
            this.btnDictaphone.Location = new System.Drawing.Point(8, 8);
            this.btnDictaphone.Name = "btnDictaphone";
            this.btnDictaphone.Size = new System.Drawing.Size(40, 40);
            this.btnDictaphone.TabIndex = 0;
            this.btnDictaphone.UseVisualStyleBackColor = false;
            this.btnDictaphone.Click += new System.EventHandler(this.btnDictaphone_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.tb_autre_qte);
            this.panel1.Controls.Add(this.tb_autre_libelle);
            this.panel1.Controls.Add(this.tb_metha_qte);
            this.panel1.Controls.Add(this.tb_dorm_qte);
            this.panel1.Controls.Add(this.tb_peth_qte);
            this.panel1.Controls.Add(this.tb_Morph_qte);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(299, 749);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(449, 96);
            this.panel1.TabIndex = 14;
            // 
            // tb_autre_qte
            // 
            this.tb_autre_qte.Location = new System.Drawing.Point(221, 53);
            this.tb_autre_qte.Name = "tb_autre_qte";
            this.tb_autre_qte.Size = new System.Drawing.Size(30, 20);
            this.tb_autre_qte.TabIndex = 10;
            // 
            // tb_autre_libelle
            // 
            this.tb_autre_libelle.Location = new System.Drawing.Point(46, 53);
            this.tb_autre_libelle.Name = "tb_autre_libelle";
            this.tb_autre_libelle.Size = new System.Drawing.Size(169, 20);
            this.tb_autre_libelle.TabIndex = 9;
            // 
            // tb_metha_qte
            // 
            this.tb_metha_qte.Location = new System.Drawing.Point(407, 27);
            this.tb_metha_qte.Name = "tb_metha_qte";
            this.tb_metha_qte.Size = new System.Drawing.Size(30, 20);
            this.tb_metha_qte.TabIndex = 8;
            // 
            // tb_dorm_qte
            // 
            this.tb_dorm_qte.Location = new System.Drawing.Point(407, 2);
            this.tb_dorm_qte.Name = "tb_dorm_qte";
            this.tb_dorm_qte.Size = new System.Drawing.Size(30, 20);
            this.tb_dorm_qte.TabIndex = 7;
            // 
            // tb_peth_qte
            // 
            this.tb_peth_qte.Location = new System.Drawing.Point(164, 27);
            this.tb_peth_qte.Name = "tb_peth_qte";
            this.tb_peth_qte.Size = new System.Drawing.Size(30, 20);
            this.tb_peth_qte.TabIndex = 6;
            // 
            // tb_Morph_qte
            // 
            this.tb_Morph_qte.Location = new System.Drawing.Point(164, 1);
            this.tb_Morph_qte.Name = "tb_Morph_qte";
            this.tb_Morph_qte.Size = new System.Drawing.Size(30, 20);
            this.tb_Morph_qte.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(2, 56);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Autre :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(232, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(167, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Méthadone comprimé 1 cpr/5 mg:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(248, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(152, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Dormicum comprimé de 15 mg:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(159, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Pethidine ampoule 2 ml/100 mg:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(156, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Morphine  ampoule 1 ml/10 mg:";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.Control;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(244, 794);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(36, 29);
            this.button3.TabIndex = 15;
            this.button3.Text = "S";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // GBoxAuteur
            // 
            this.GBoxAuteur.BackColor = System.Drawing.Color.CadetBlue;
            this.GBoxAuteur.Controls.Add(this.RB3b);
            this.GBoxAuteur.Controls.Add(this.RB3a);
            this.GBoxAuteur.Controls.Add(this.RB3);
            this.GBoxAuteur.Controls.Add(this.RB2);
            this.GBoxAuteur.Controls.Add(this.RB1);
            this.GBoxAuteur.ForeColor = System.Drawing.SystemColors.ControlText;
            this.GBoxAuteur.Location = new System.Drawing.Point(765, 749);
            this.GBoxAuteur.Name = "GBoxAuteur";
            this.GBoxAuteur.Size = new System.Drawing.Size(119, 157);
            this.GBoxAuteur.TabIndex = 71;
            this.GBoxAuteur.TabStop = false;
            this.GBoxAuteur.Text = "Auteur";
            // 
            // RB3b
            // 
            this.RB3b.AutoSize = true;
            this.RB3b.ForeColor = System.Drawing.SystemColors.ControlText;
            this.RB3b.Location = new System.Drawing.Point(8, 91);
            this.RB3b.Name = "RB3b";
            this.RB3b.Size = new System.Drawing.Size(82, 17);
            this.RB3b.TabIndex = 75;
            this.RB3b.Text = "Sec appoint";
            this.RB3b.UseVisualStyleBackColor = true;
            // 
            // RB3a
            // 
            this.RB3a.AutoSize = true;
            this.RB3a.ForeColor = System.Drawing.SystemColors.ControlText;
            this.RB3a.Location = new System.Drawing.Point(8, 73);
            this.RB3a.Name = "RB3a";
            this.RB3a.Size = new System.Drawing.Size(67, 17);
            this.RB3a.TabIndex = 74;
            this.RB3a.Text = "Suzanne";
            this.RB3a.UseVisualStyleBackColor = true;
            // 
            // RB3
            // 
            this.RB3.AutoSize = true;
            this.RB3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.RB3.Location = new System.Drawing.Point(8, 54);
            this.RB3.Name = "RB3";
            this.RB3.Size = new System.Drawing.Size(70, 17);
            this.RB3.TabIndex = 73;
            this.RB3.Text = "Catherine";
            this.RB3.UseVisualStyleBackColor = true;
            // 
            // RB2
            // 
            this.RB2.AutoSize = true;
            this.RB2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.RB2.Location = new System.Drawing.Point(8, 35);
            this.RB2.Name = "RB2";
            this.RB2.Size = new System.Drawing.Size(45, 17);
            this.RB2.TabIndex = 72;
            this.RB2.Text = "Lina";
            this.RB2.UseVisualStyleBackColor = true;
            // 
            // RB1
            // 
            this.RB1.AutoSize = true;
            this.RB1.Checked = true;
            this.RB1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.RB1.Location = new System.Drawing.Point(8, 15);
            this.RB1.Name = "RB1";
            this.RB1.Size = new System.Drawing.Size(58, 17);
            this.RB1.TabIndex = 71;
            this.RB1.TabStop = true;
            this.RB1.Text = "Audrey";
            this.RB1.UseVisualStyleBackColor = true;
            // 
            // GBoxTypeRapport
            // 
            this.GBoxTypeRapport.BackColor = System.Drawing.Color.CadetBlue;
            this.GBoxTypeRapport.Controls.Add(this.RB6);
            this.GBoxTypeRapport.Controls.Add(this.RB5);
            this.GBoxTypeRapport.Controls.Add(this.RB4);
            this.GBoxTypeRapport.ForeColor = System.Drawing.SystemColors.ControlText;
            this.GBoxTypeRapport.Location = new System.Drawing.Point(908, 749);
            this.GBoxTypeRapport.Name = "GBoxTypeRapport";
            this.GBoxTypeRapport.Size = new System.Drawing.Size(121, 96);
            this.GBoxTypeRapport.TabIndex = 72;
            this.GBoxTypeRapport.TabStop = false;
            this.GBoxTypeRapport.Text = "Type de rapport";
            this.GBoxTypeRapport.Visible = false;
            // 
            // RB6
            // 
            this.RB6.AutoSize = true;
            this.RB6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.RB6.Location = new System.Drawing.Point(8, 58);
            this.RB6.Name = "RB6";
            this.RB6.Size = new System.Drawing.Size(54, 17);
            this.RB6.TabIndex = 73;
            this.RB6.Text = "Grand";
            this.RB6.UseVisualStyleBackColor = true;
            // 
            // RB5
            // 
            this.RB5.AutoSize = true;
            this.RB5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.RB5.Location = new System.Drawing.Point(8, 35);
            this.RB5.Name = "RB5";
            this.RB5.Size = new System.Drawing.Size(57, 17);
            this.RB5.TabIndex = 72;
            this.RB5.Text = "Moyen";
            this.RB5.UseVisualStyleBackColor = true;
            // 
            // RB4
            // 
            this.RB4.AutoSize = true;
            this.RB4.Checked = true;
            this.RB4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.RB4.Location = new System.Drawing.Point(8, 15);
            this.RB4.Name = "RB4";
            this.RB4.Size = new System.Drawing.Size(46, 17);
            this.RB4.TabIndex = 71;
            this.RB4.TabStop = true;
            this.RB4.Text = "Petit";
            this.RB4.UseVisualStyleBackColor = true;
            // 
            // frmRapport_Objet
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.ClientSize = new System.Drawing.Size(1092, 918);
            this.ControlBox = false;
            this.Controls.Add(this.GBoxTypeRapport);
            this.Controls.Add(this.GBoxAuteur);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnDictaphone);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnAnnuler);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.btnValider);
            this.Controls.Add(this.twCategories);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frmRapport_Objet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmRapport_Objet_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmRapport_Objet_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.GBoxAuteur.ResumeLayout(false);
            this.GBoxAuteur.PerformLayout();
            this.GBoxTypeRapport.ResumeLayout(false);
            this.GBoxTypeRapport.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		#region Méthodes publiques

		#region Initialisation de la fenetre

        public bool IsNumeric(string Nombre)
        {
            try
            {
                int.Parse(Nombre);
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        
        // Fabrication des noeuds de catégories dans le corps
		private void FabricationTreeNode()
		{
			twCategories.Nodes.Clear();	
			twCategories.Font = new Font("Arial",10,FontStyle.Bold);

			TabEnCours = OutilsExt.OutilsSql.ListeRubriqueRapport(Donnees.MonDtRapport.Rapport[0].TypeRapport);
			foreach(RubriqueRapport r in TabEnCours)
			{
				String Valeur = "";
				for(int i=0;i<Donnees.MonDtCorps.Corps.Count;i++)
				{
					if(Donnees.MonDtCorps.Corps[i].IdCategorie==r.IdCategorie)
					{
						Valeur = Donnees.MonDtCorps.Corps[i].ValeurCategorie;
						r.Valeur = Valeur;
						break;
					}
				}
				
				// Noeud racine
				if(r.Dependance==0)
				{
					Font font = new Font("Arial",10,FontStyle.Regular);
					TreeNode tn = new TreeNode(r.Libelle);
					tn.Tag  = r;
					// recherche des noeuds dépendants
					RubriqueRapport[] rub = RubriqueRapport.RetrouveRubriqueDependantes(TabEnCours,r.IdCategorie);
					// affichage des noeuds dépendants
					foreach(RubriqueRapport r1 in rub)
					{
						Valeur = "";
						for(int i=0;i<Donnees.MonDtCorps.Corps.Count;i++)
						{
							if(Donnees.MonDtCorps.Corps[i].IdCategorie==r1.IdCategorie)
							{
								Valeur = Donnees.MonDtCorps.Corps[i].ValeurCategorie;
								r1.Valeur = Valeur;
								break;
							}
						}

						//si noeud fixe, on affiche en gras => Fixe: Anamnese, Diagnostique etc.. de 1ere niveau avec dependance = 0
						TreeNode tn1 = new TreeNode(r1.Libelle);
						if(r1.Fixe==1 || (r1.Fixe==0 && r1.Valeur!=null && r1.Valeur!="")) 
						{
							tn1.Checked = true;
							tn1.NodeFont = new Font(font.FontFamily,font.Size,FontStyle.Bold);
						}
						else
						{
							tn1.Checked  = false;
							tn1.NodeFont = new Font(font.FontFamily,font.Size,FontStyle.Regular);
						}
						tn1.Tag  = r1;
						tn.Nodes.Add(tn1);
					}
					// si c'est un noeud fixe, en gras
					if(r.Fixe==1 || (r.Fixe==0 && r.Valeur!=null && r.Valeur!="")) 
					{
						tn.Checked = true;
						tn.NodeFont = new Font(font.FontFamily,font.Size,FontStyle.Bold);
					}
					else
					{
						tn.Checked = false;
						tn.NodeFont = new Font(font.FontFamily,font.Size,FontStyle.Regular);
					}
					twCategories.Nodes.Add(tn);					
				}
			}		
			twCategories.ExpandAll();
		}

		private int NbCtrl()
		{
			int nb=0;
			foreach(Control c in this.Controls)
				if(c!=null && c.GetType()==typeof(CtrlCategorie))
					nb++;	
			return nb;
		}

        //Création des rubriques
		private void DynamicRubriques(RubriqueRapport rubrik)
		{		
			int PosCtrl = twCategories.Top ;

			CtrlCategorie CtrlWithFocus=null;

            //On vide d'eventuelles rubriques
			while(NbCtrl()>0)
			foreach(Control c in this.Controls)
			{
				if(c!=null && c.GetType()==typeof(CtrlCategorie))
				{
					this.Controls.Remove(c);			
					c.Dispose();							
				}
			}

			int zz = 0;

            //On créer les rubriques ici avec d'éventuelles valeurs pré-rempli.
			for(int i=0;i<twCategories.Nodes.Count;i++)
			{
				TreeNode tn = twCategories.Nodes[i];
				if(tn.Checked)
				{					
					zz++;
					RubriqueRapport r = (RubriqueRapport)tn.Tag;
                                       
                    //Si c'est une consultation prise en charge par une infirmière dans le cadre d'une teleconsult, on rajoute un texte à l'exam clinique
                    if (Donnees.MonDtRapport.Rapport[0].PriseEnChargePatient == "Infirmière" && r.Libelle == "Examen clinique" && (r.Valeur == "" || r.Valeur == null))    
                        r.Valeur = "Examen réalisé par une infirmière au domicile du patient à l'aide d'un dispositif connecté et consultation effectuée par un médecin distant.";
                  
					CtrlCategorie ctrl = new CtrlCategorie(r.Libelle,r.Valeur);
					this.Controls.Add(ctrl);
					this.Controls.SetChildIndex(ctrl,0);
					ctrl.Left = twCategories.Left + twCategories.Width + 30;
					ctrl.Top = PosCtrl;
					ctrl.Tag = r;
					PosCtrl+=ctrl.Height + 0;
					if(rubrik!=null && rubrik.IdCategorie==r.IdCategorie)
						CtrlWithFocus = ctrl;
				}
				for(int j=0;j<tn.Nodes.Count;j++)
				{
					TreeNode tn1 = tn.Nodes[j];
					if(tn1.Checked)
					{
						RubriqueRapport r1 = (RubriqueRapport)tn1.Tag;
						CtrlCategorie ctrl = new CtrlCategorie(r1.Libelle,r1.Valeur);
						this.Controls.Add(ctrl);
						this.Controls.SetChildIndex(ctrl,0);
						ctrl.Height -=40;
						ctrl.Left = twCategories.Left + twCategories.Width + 30;
						ctrl.Top = PosCtrl;
						ctrl.Tag = r1;
						PosCtrl+=ctrl.Height + 0;
						if(rubrik!=null && rubrik.IdCategorie==r1.IdCategorie)
							CtrlWithFocus = ctrl;
					}
				}
			}	

			btnValider.Top = PosCtrl + 20;
		
			if(CtrlWithFocus!=null)
				CtrlWithFocus.Focus();
			else
				twCategories.Focus();
		}

        #endregion

        #endregion

        #region Evenements de la form

        private void frmRapport_Objet_Load(object sender, System.EventArgs e)
        {
            //On regarde si il existe un rapport audio
            int z_intNConsultation = Donnees.MonDtRapport.Rapport[0].NConsultation;
            try
            {
                string Repertoire = SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.Dest_Path_Dictee;

                string[] dirs = System.IO.Directory.GetFiles(Repertoire, z_intNConsultation.ToString() + "*.wav");
                string[] dirs1 = System.IO.Directory.GetFiles(Repertoire, z_intNConsultation.ToString() + "*.3gp");

                if (dirs.Length > 0)   //Pour les Wav
                {
                    _strNomRapport = dirs[0];
                    btnDictaphone.Enabled = true;
                }
                else
                {
                    if (dirs1.Length > 0)   //Pour les 3gp
                    {
                        _strNomRapport = dirs1[0];
                        btnDictaphone.Enabled = true;
                    }
                    else btnDictaphone.Enabled = false;
                }
            }
            catch
            {
                btnDictaphone.Enabled = false;
            }

            foreach (Control c in this.Controls)
            {
                if (c != null && c.GetType() == typeof(CtrlCategorie))
                {
                    ((CtrlCategorie)c).DonneFocusToText();
                }
            }

            //on initialise les textbox par défaut
            tb_Morph_qte.Text = "0";
            tb_peth_qte.Text = "0";
            tb_dorm_qte.Text = "0";
            tb_metha_qte.Text = "0";
            tb_autre_libelle.Text = "";
            tb_autre_qte.Text = "0";

            //Puis on récupère les données
            tb_Morph_qte.Text = Donnees.MonDtRapport.Rapport[0].Morphine.ToString();
            tb_peth_qte.Text = Donnees.MonDtRapport.Rapport[0].Pethidine.ToString();
            tb_dorm_qte.Text = Donnees.MonDtRapport.Rapport[0].Dormicum.ToString();
            tb_metha_qte.Text = Donnees.MonDtRapport.Rapport[0].Methadone.ToString();
            tb_autre_libelle.Text = Donnees.MonDtRapport.Rapport[0].Autre_stup.ToString();
            tb_autre_qte.Text = Donnees.MonDtRapport.Rapport[0].Autre_stup_qte.ToString();

            //Pour les boutons radio
            //Les auteurs du rapport

            //MessageBox.Show(Donnees.MonDtRapport.Rapport[0].Auteur.ToString());

            switch (Donnees.MonDtRapport.Rapport[0].Auteur)
            {
                case 7:
                    RB1.Checked = true;     //Audrey ---> Anne avait le 0
                    break;

                case 8:
                    RB2.Checked = true;     //Lina --> Chantal avait le 3, Aurore avait le 1
                    break;

                case 2:
                    RB3.Checked = true;     //Catherine
                    break;

                case 4:
                    RB3a.Checked = true;    //Suzanne
                    break;

                case 9:
                    RB3b.Checked = true;    //sec appoint --> Adrienne avait le 6--->Roseline avait le 5
                    break;

                default:
                    RB1.Checked = true;
                    break;
            }

            //La longeur du rapport

            switch (Donnees.MonDtRapport.Rapport[0].Type_long_rapport)
            {
                case 0:
                    RB4.Checked = true;
                    break;

                case 1:
                    RB5.Checked = true;
                    break;

                case 2:
                    RB6.Checked = true;
                    break;

                default:
                    RB4.Checked = true;
                    break;
            }


        }

        private void twCategories_AfterCheck(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			if(e.Node!=null)
			{
				RubriqueRapport r = (RubriqueRapport)e.Node.Tag;
				if(r.Fixe==1 && e.Node.Checked==false) 
                    e.Node.Checked = true;

				foreach(Control c in this.Controls)
				{
					if(c.GetType()==typeof(CtrlCategorie))
					{
						CtrlCategorie ctrl = (CtrlCategorie)c;
						RubriqueRapport rr = (RubriqueRapport)ctrl.Tag;
						bool JeContinue = true;
						
                        for(int i=0;i<twCategories.Nodes.Count;i++)
						{
							if(!JeContinue) break;
							RubriqueRapport rrr = (RubriqueRapport)twCategories.Nodes[i].Tag;
							if(rrr.IdCategorie==rr.IdCategorie)
							{
								rrr.Valeur = ctrl.StrValeur;
								break;
							}
							for(int j=0;j<twCategories.Nodes[i].Nodes.Count;j++)
							{
								RubriqueRapport rrrr = (RubriqueRapport)twCategories.Nodes[i].Nodes[j].Tag;
								if(rrrr.IdCategorie==rr.IdCategorie)
								{
									rrrr.Valeur = ctrl.StrValeur;
									JeContinue = false;
									break;
								}
							}
						}
					}
				}
				DynamicRubriques(r);
			}
		}

        //Touches de raccourcis pour sous rubriques
		private void frmRapport_Objet_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.F1)
			{
				
				btnValider_Click(null,null);
				return;
			}
			else if (e.KeyCode==Keys.E && e.Alt)
			{
				//Nodes[2]... => Examen clinique
				twCategories.SelectedNode = twCategories.Nodes[2].Nodes[4];
				twCategories.Select();
				twCategories.SelectedNode.Checked = true;
				return;
			}
			else if (e.KeyCode==Keys.T && e.Alt)
			{
				
				twCategories.SelectedNode = twCategories.Nodes[2].Nodes[2];
				twCategories.Select();
				twCategories.SelectedNode.Checked = true;
				return;
			}
			else if (e.KeyCode==Keys.G && e.Alt)
			{
				
				twCategories.SelectedNode = twCategories.Nodes[2].Nodes[1];
				twCategories.Select();
				twCategories.SelectedNode.Checked = true;
				return;
			}
			else if (e.KeyCode==Keys.R && e.Alt)
			{
				
				twCategories.SelectedNode = twCategories.Nodes[2].Nodes[0];
				twCategories.Select();
				twCategories.SelectedNode.Checked = true;
				return;
			}
			else if (e.KeyCode==Keys.C && e.Alt)
			{
				
				twCategories.SelectedNode = twCategories.Nodes[2].Nodes[5];
				twCategories.Select();
				twCategories.SelectedNode.Checked = true;
				return;
			}

			if(!e.Control)
			{				
				return;
			}
			
		}

		private void btnAnnuler_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			ArrayList tab = new ArrayList();
			
			Donnees.MonDtCorps.Clear();

			for(int i=this.Controls.Count-1;i>=0;i--)
			{
				Control c = this.Controls[i];
				if(c.GetType()==typeof(CtrlCategorie))
				{
					CtrlCategorie ctrl = (CtrlCategorie)c;
					if(ctrl.StrValeur!="")
					{
						RubriqueRapport r = (RubriqueRapport)ctrl.Tag;
						r.Valeur = ctrl.StrValeur;
						tab.Add(r);						
					}
				}
			}

			for(int i=0;i<tab.Count;i++)
			{
				RubriqueRapport r = (RubriqueRapport)tab[i];

                SosMedecins.SmartRapport.DAL.dstCorps.CorpsRow row = Donnees.MonDtCorps.Corps.NewCorpsRow();
				row.Active = true;
				row.IdCategorie = r.IdCategorie;
				row.LibelleCategorie = r.Libelle;
				row.ValeurCategorie = r.Valeur;
				row.NRapport = Donnees.MonDtRapport.Rapport[0].NRapport;
				Donnees.MonDtCorps.Corps.AddCorpsRow(row);			
			}
	
			BoardClip.TypeRapport = Donnees.MonDtRapport.Rapport[0].TypeRapport;
			BoardClip.TableCorps = Donnees.MonDtCorps.Corps;
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			if(BoardClip.TypeRapport>-1 && BoardClip.TypeRapport==Donnees.MonDtRapport.Rapport[0].TypeRapport && BoardClip.TableCorps!=null)
			{
				Donnees.MonDtCorps.Corps.Rows.Clear();
				for(int i=0;i<BoardClip.TableCorps.Rows.Count;i++)
				{
                    SosMedecins.SmartRapport.DAL.dstCorps.CorpsRow row = Donnees.MonDtCorps.Corps.NewCorpsRow();
					row.ItemArray = BoardClip.TableCorps.Rows[i].ItemArray;
					Donnees.MonDtCorps.Corps.Rows.Add(row);
				}
				FabricationTreeNode();
				DynamicRubriques(null);
			}
		}

		private void btnValider_Click(object sender, System.EventArgs e)
		{
			ArrayList tab = new ArrayList();
			richTextBox1.Text = "";

			Donnees.MonDtCorps.Clear();

			richTextBox1.Font = new Font("Times New Roman",12,FontStyle.Regular);
            
            //On sauvegarde les données
            //Mais avant on test si les données des quantité stup sont des entiers.
            if (IsNumeric(tb_Morph_qte.Text))
                Donnees.MonDtRapport.Rapport[0].Morphine = int.Parse(tb_Morph_qte.Text);
            else 
            { 
                MessageBox.Show("La Quantité de Morphine est incorrecte");
                return;
            }

            if (IsNumeric(tb_peth_qte.Text))
                Donnees.MonDtRapport.Rapport[0].Pethidine = int.Parse(tb_peth_qte.Text);
            else
            {
                MessageBox.Show("La Quantité de Pethidine est incorrecte");
                return;
            }

            if (IsNumeric(tb_dorm_qte.Text))
                Donnees.MonDtRapport.Rapport[0].Dormicum = int.Parse(tb_dorm_qte.Text);
            else
            {
                MessageBox.Show("La Quantité de Dormicum est incorrecte");
                return;
            }

            if (IsNumeric(tb_metha_qte.Text))
                Donnees.MonDtRapport.Rapport[0].Methadone = int.Parse(tb_metha_qte.Text);
            else
            {
                MessageBox.Show("La Quantité de Méthadone est incorrecte");
                return;
            }

            Donnees.MonDtRapport.Rapport[0].Autre_stup = tb_autre_libelle.Text.ToString();

            if (IsNumeric(tb_autre_qte.Text))
                Donnees.MonDtRapport.Rapport[0].Autre_stup_qte = int.Parse(tb_autre_qte.Text);
            else
            {
                MessageBox.Show("La Quantité de autre produit est incorrecte");
                return;
            }

            //Pour les boutons radio
            //Les auteurs du rapport
            if (RB1.Checked == true)
                Donnees.MonDtRapport.Rapport[0].Auteur = 7;         //Audrey -----> Anne a 0
            else if (RB2.Checked == true)                     
                    Donnees.MonDtRapport.Rapport[0].Auteur = 8;     //Lina
            else if (RB3.Checked == true)
                     Donnees.MonDtRapport.Rapport[0].Auteur = 2;    //Catherine
            else if (RB3a.Checked == true) 
                 Donnees.MonDtRapport.Rapport[0].Auteur = 4;        //Suzanne
            else if (RB3b.Checked == true)
                 Donnees.MonDtRapport.Rapport[0].Auteur = 9;        //Appoint --> Adrienne 6, Roseline 5

            for (int i=this.Controls.Count-1;i>=0;i--)
			{
				Control c = this.Controls[i];
				if(c.GetType()==typeof(CtrlCategorie))
				{
					CtrlCategorie ctrl = (CtrlCategorie)c;
					if(ctrl.StrValeur!="")
					{
						RubriqueRapport r = (RubriqueRapport)ctrl.Tag;
						r.Valeur = ctrl.StrValeur;
						tab.Add(r);

						if(Donnees.MonDtRapport.Rapport[0].TypeRapport==1)
						richTextBox1.Text+=ctrl.StrLibelle + ctrl.StrValeur + "\r\n\r\n";						
						else if(Donnees.MonDtRapport.Rapport[0].TypeRapport==2)
							richTextBox1.Text+=ctrl.StrLibelle + "\r\n" + ctrl.StrValeur + "\r\n\r\n";	
					}
				}
			}

			
            //Variable pour déterminer le nombre de ligne du rapport
            int NbLignesTotal = 0;
                       
            //On enregistre les données du corps du rapport
            for(int i=0;i<tab.Count;i++)
			{
				RubriqueRapport r = (RubriqueRapport)tab[i];

                SosMedecins.SmartRapport.DAL.dstCorps.CorpsRow row = Donnees.MonDtCorps.Corps.NewCorpsRow();
				row.Active = true;
				row.IdCategorie = r.IdCategorie;
				row.LibelleCategorie = r.Libelle;

                //******************Mettre ici le texte supplémentaire dans la catégorie
                // if (r.IdCategorie == 1)  //1 pour Anamnèse ou 2 etc...                
                //     row.ValeurCategorie = TexteaAjouter + r.Valeur;                
                // else
                //     row.ValeurCategorie = r.Valeur;    
               
                row.ValeurCategorie = r.Valeur;
				row.NRapport = Donnees.MonDtRapport.Rapport[0].NRapport;
				Donnees.MonDtCorps.Corps.AddCorpsRow(row);               

				string ss = r.Libelle + " : ";
				int bb = richTextBox1.Text.IndexOf(ss);
				if(bb!=-1)
				{
					richTextBox1.SelectionStart = bb;
					richTextBox1.SelectionLength = ss.Length;
					richTextBox1.SelectionFont = new Font("Times New Roman",12,FontStyle.Bold);
				}

                //Si le modulo (reste) est > 0 (donc un bout de ligne), on compte 1 ligne.
                //On considère qu'une ligne = 100 caractères
                int NbLignes = 0;

                if ((r.Valeur.Length % 100) > 0)
                    NbLignes = r.Valeur.Length / 100 + 1;
                else
                    NbLignes = r.Valeur.Length / 100;

                NbLignesTotal = NbLignesTotal + NbLignes;
			}


            if (NbLignesTotal < 11)
                Donnees.MonDtRapport.Rapport[0].Type_long_rapport = 0;    //Petit rapport
            else if (NbLignesTotal < 35)
                 Donnees.MonDtRapport.Rapport[0].Type_long_rapport = 1;    //Moyen rapport
            else
                Donnees.MonDtRapport.Rapport[0].Type_long_rapport = 2;    //grand rapport

			richTextBox1.SelectionStart=0;
			richTextBox1.SelectionLength = richTextBox1.Text.Length;
			this.Corps = richTextBox1.SelectedRtf;
			
			this.Close();
		}

		#endregion

		private void btnDictaphone_Click(object sender, System.EventArgs e)
		{
			System.Diagnostics.Process myProcess = new System.Diagnostics.Process();

            string _strPathScript = "";
            string PathExpressScribe1 = SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.Path_ExpressScribe1;
            string PathExpressScribe2 = SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.Path_ExpressScribe2;

            //On vérifie ou se trouve l'executable d'express scribe           
            if (System.IO.File.Exists(PathExpressScribe1))
                _strPathScript = PathExpressScribe1;      //le fichier existe
            else
                _strPathScript = PathExpressScribe2;      //le fichier n'existe pas
                                            
            // Get the path that stores user documents.
			myProcess.StartInfo.FileName = _strPathScript; 
			myProcess.StartInfo.Arguments = _strNomRapport  + " -back";

			myProcess.StartInfo.CreateNoWindow = true;
			myProcess.Start();
			myProcess.Close();

			System.Threading.Thread.Sleep(500);

			myProcess = new System.Diagnostics.Process();
			myProcess.StartInfo.FileName = _strPathScript; 
			myProcess.StartInfo.Arguments = " -selectfile " + _strNomRapport + " -back";
			myProcess.Start();
			myProcess.Close();
		}

        private void button3_Click(object sender, EventArgs e)
        {
            //on affiche ou on cache le panneau pour les stups
            if (panel1.Visible == true)
                panel1.Visible = false;
            else panel1.Visible = true;
        }
      
    }
}

