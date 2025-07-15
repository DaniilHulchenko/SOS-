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
	/// Description résumée de frmRecherchePersonne.
	/// </summary>
	public class frmRecherchePersonne : System.Windows.Forms.Form
	{
		private long m_lngPatient = -1;
		private long m_lngPersonne = -1;
		private DataRow m_Row;
		public bool PatientAccepte = false;


        private System.Windows.Forms.TextBox txtRecherchePrenom;
		private System.Windows.Forms.TextBox TxtFiltre3;
		private System.Windows.Forms.TextBox TxtFiltre2;
		private System.Windows.Forms.TextBox TxtFiltre1;
		private System.Windows.Forms.CheckBox ChkFiltre3;
		private System.Windows.Forms.CheckBox ChkFiltre2;
        private System.Windows.Forms.CheckBox ChkFiltre1;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox tBoxSuiviPatient;
        private System.Windows.Forms.Label label4;
        private Label label11;
        private TextBox tBoxAge;
        private TextBox tBoxSex;
        private TextBox tBoxDateNaiss;
        private TextBox tBoxTel;
        private TextBox tBoxPrenom;
        private TextBox tBoxNom;
        private Label label12;
        private TextBox tBoxSupinfo;
        private TextBox tBoxVille;
        private TextBox tBoxCP;
        private TextBox tBoxRue;
        private TextBox tBoxNumRue;
        private Button bRechercher;
        private Button bImportePersonne;
        private ImageList imageList1;
        private ToolTip toolTip1;
        private Button bQuitterAjoutDest;
        private IContainer components;

		public frmRecherchePersonne(DateTime DateNaiss)
		{
			//
			// Requis pour la prise en charge du Concepteur Windows Forms
			//
			InitializeComponent();

			TxtFiltre3.Text = DateNaiss.ToString();
			ChkFiltre3.Checked = true;
            
			//
			// TODO : ajoutez le code du constructeur après l'appel à InitializeComponent
			//
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRecherchePersonne));
            this.txtRecherchePrenom = new System.Windows.Forms.TextBox();
            this.TxtFiltre3 = new System.Windows.Forms.TextBox();
            this.TxtFiltre2 = new System.Windows.Forms.TextBox();
            this.TxtFiltre1 = new System.Windows.Forms.TextBox();
            this.ChkFiltre3 = new System.Windows.Forms.CheckBox();
            this.ChkFiltre2 = new System.Windows.Forms.CheckBox();
            this.ChkFiltre1 = new System.Windows.Forms.CheckBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.tBoxSupinfo = new System.Windows.Forms.TextBox();
            this.tBoxVille = new System.Windows.Forms.TextBox();
            this.tBoxCP = new System.Windows.Forms.TextBox();
            this.tBoxRue = new System.Windows.Forms.TextBox();
            this.tBoxNumRue = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tBoxAge = new System.Windows.Forms.TextBox();
            this.tBoxSex = new System.Windows.Forms.TextBox();
            this.tBoxDateNaiss = new System.Windows.Forms.TextBox();
            this.tBoxTel = new System.Windows.Forms.TextBox();
            this.tBoxPrenom = new System.Windows.Forms.TextBox();
            this.tBoxNom = new System.Windows.Forms.TextBox();
            this.tBoxSuiviPatient = new System.Windows.Forms.TextBox();
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
            this.bRechercher = new System.Windows.Forms.Button();
            this.bImportePersonne = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.bQuitterAjoutDest = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtRecherchePrenom
            // 
            this.txtRecherchePrenom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRecherchePrenom.Location = new System.Drawing.Point(137, 72);
            this.txtRecherchePrenom.Name = "txtRecherchePrenom";
            this.txtRecherchePrenom.Size = new System.Drawing.Size(223, 22);
            this.txtRecherchePrenom.TabIndex = 7;
            // 
            // TxtFiltre3
            // 
            this.TxtFiltre3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtFiltre3.Location = new System.Drawing.Point(166, 109);
            this.TxtFiltre3.Name = "TxtFiltre3";
            this.TxtFiltre3.Size = new System.Drawing.Size(153, 22);
            this.TxtFiltre3.TabIndex = 10;
            // 
            // TxtFiltre2
            // 
            this.TxtFiltre2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtFiltre2.Location = new System.Drawing.Point(137, 44);
            this.TxtFiltre2.Name = "TxtFiltre2";
            this.TxtFiltre2.Size = new System.Drawing.Size(223, 22);
            this.TxtFiltre2.TabIndex = 6;
            // 
            // TxtFiltre1
            // 
            this.TxtFiltre1.Enabled = false;
            this.TxtFiltre1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtFiltre1.Location = new System.Drawing.Point(137, 10);
            this.TxtFiltre1.Name = "TxtFiltre1";
            this.TxtFiltre1.Size = new System.Drawing.Size(223, 22);
            this.TxtFiltre1.TabIndex = 2;
            // 
            // ChkFiltre3
            // 
            this.ChkFiltre3.BackColor = System.Drawing.Color.Transparent;
            this.ChkFiltre3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkFiltre3.Location = new System.Drawing.Point(8, 107);
            this.ChkFiltre3.Name = "ChkFiltre3";
            this.ChkFiltre3.Size = new System.Drawing.Size(152, 22);
            this.ChkFiltre3.TabIndex = 8;
            this.ChkFiltre3.Text = "Date de naissance:";
            this.ChkFiltre3.UseVisualStyleBackColor = false;
            // 
            // ChkFiltre2
            // 
            this.ChkFiltre2.BackColor = System.Drawing.Color.Transparent;
            this.ChkFiltre2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkFiltre2.Location = new System.Drawing.Point(8, 44);
            this.ChkFiltre2.Name = "ChkFiltre2";
            this.ChkFiltre2.Size = new System.Drawing.Size(111, 26);
            this.ChkFiltre2.TabIndex = 4;
            this.ChkFiltre2.Text = "Nom/prénom:";
            this.ChkFiltre2.UseVisualStyleBackColor = false;
            // 
            // ChkFiltre1
            // 
            this.ChkFiltre1.BackColor = System.Drawing.Color.Transparent;
            this.ChkFiltre1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkFiltre1.Location = new System.Drawing.Point(8, 16);
            this.ChkFiltre1.Name = "ChkFiltre1";
            this.ChkFiltre1.Size = new System.Drawing.Size(102, 22);
            this.ChkFiltre1.TabIndex = 0;
            this.ChkFiltre1.Text = "Téléphone:";
            this.ChkFiltre1.UseVisualStyleBackColor = false;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listView1.Location = new System.Drawing.Point(12, 223);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(361, 203);
            this.listView1.TabIndex = 13;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 120;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Width = 100;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Width = 100;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.tBoxSupinfo);
            this.panel1.Controls.Add(this.tBoxVille);
            this.panel1.Controls.Add(this.tBoxCP);
            this.panel1.Controls.Add(this.tBoxRue);
            this.panel1.Controls.Add(this.tBoxNumRue);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.tBoxAge);
            this.panel1.Controls.Add(this.tBoxSex);
            this.panel1.Controls.Add(this.tBoxDateNaiss);
            this.panel1.Controls.Add(this.tBoxTel);
            this.panel1.Controls.Add(this.tBoxPrenom);
            this.panel1.Controls.Add(this.tBoxNom);
            this.panel1.Controls.Add(this.tBoxSuiviPatient);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(398, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(697, 425);
            this.panel1.TabIndex = 3;
            this.panel1.Visible = false;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(11, 265);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(111, 22);
            this.label12.TabIndex = 23;
            this.label12.Text = "Suivi du patient :";
            // 
            // tBoxSupinfo
            // 
            this.tBoxSupinfo.Enabled = false;
            this.tBoxSupinfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tBoxSupinfo.Location = new System.Drawing.Point(119, 223);
            this.tBoxSupinfo.Name = "tBoxSupinfo";
            this.tBoxSupinfo.ReadOnly = true;
            this.tBoxSupinfo.Size = new System.Drawing.Size(385, 22);
            this.tBoxSupinfo.TabIndex = 22;
            // 
            // tBoxVille
            // 
            this.tBoxVille.Enabled = false;
            this.tBoxVille.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tBoxVille.Location = new System.Drawing.Point(254, 119);
            this.tBoxVille.Name = "tBoxVille";
            this.tBoxVille.ReadOnly = true;
            this.tBoxVille.Size = new System.Drawing.Size(342, 22);
            this.tBoxVille.TabIndex = 21;
            // 
            // tBoxCP
            // 
            this.tBoxCP.Enabled = false;
            this.tBoxCP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tBoxCP.Location = new System.Drawing.Point(97, 119);
            this.tBoxCP.Name = "tBoxCP";
            this.tBoxCP.ReadOnly = true;
            this.tBoxCP.Size = new System.Drawing.Size(97, 22);
            this.tBoxCP.TabIndex = 20;
            // 
            // tBoxRue
            // 
            this.tBoxRue.Enabled = false;
            this.tBoxRue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tBoxRue.Location = new System.Drawing.Point(147, 89);
            this.tBoxRue.Name = "tBoxRue";
            this.tBoxRue.ReadOnly = true;
            this.tBoxRue.Size = new System.Drawing.Size(449, 22);
            this.tBoxRue.TabIndex = 19;
            // 
            // tBoxNumRue
            // 
            this.tBoxNumRue.Enabled = false;
            this.tBoxNumRue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tBoxNumRue.Location = new System.Drawing.Point(37, 88);
            this.tBoxNumRue.Name = "tBoxNumRue";
            this.tBoxNumRue.ReadOnly = true;
            this.tBoxNumRue.Size = new System.Drawing.Size(48, 22);
            this.tBoxNumRue.TabIndex = 18;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(463, 57);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 19);
            this.label11.TabIndex = 17;
            this.label11.Text = "Age :";
            // 
            // tBoxAge
            // 
            this.tBoxAge.Enabled = false;
            this.tBoxAge.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tBoxAge.Location = new System.Drawing.Point(510, 54);
            this.tBoxAge.Name = "tBoxAge";
            this.tBoxAge.ReadOnly = true;
            this.tBoxAge.Size = new System.Drawing.Size(68, 22);
            this.tBoxAge.TabIndex = 16;
            // 
            // tBoxSex
            // 
            this.tBoxSex.Enabled = false;
            this.tBoxSex.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tBoxSex.Location = new System.Drawing.Point(373, 51);
            this.tBoxSex.Name = "tBoxSex";
            this.tBoxSex.ReadOnly = true;
            this.tBoxSex.Size = new System.Drawing.Size(65, 22);
            this.tBoxSex.TabIndex = 15;
            // 
            // tBoxDateNaiss
            // 
            this.tBoxDateNaiss.Enabled = false;
            this.tBoxDateNaiss.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tBoxDateNaiss.Location = new System.Drawing.Point(147, 48);
            this.tBoxDateNaiss.Name = "tBoxDateNaiss";
            this.tBoxDateNaiss.ReadOnly = true;
            this.tBoxDateNaiss.Size = new System.Drawing.Size(160, 22);
            this.tBoxDateNaiss.TabIndex = 14;
            // 
            // tBoxTel
            // 
            this.tBoxTel.Enabled = false;
            this.tBoxTel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tBoxTel.Location = new System.Drawing.Point(97, 171);
            this.tBoxTel.Name = "tBoxTel";
            this.tBoxTel.ReadOnly = true;
            this.tBoxTel.Size = new System.Drawing.Size(182, 22);
            this.tBoxTel.TabIndex = 13;
            // 
            // tBoxPrenom
            // 
            this.tBoxPrenom.Enabled = false;
            this.tBoxPrenom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tBoxPrenom.Location = new System.Drawing.Point(466, 12);
            this.tBoxPrenom.Name = "tBoxPrenom";
            this.tBoxPrenom.ReadOnly = true;
            this.tBoxPrenom.Size = new System.Drawing.Size(182, 22);
            this.tBoxPrenom.TabIndex = 12;
            // 
            // tBoxNom
            // 
            this.tBoxNom.Enabled = false;
            this.tBoxNom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tBoxNom.Location = new System.Drawing.Point(63, 12);
            this.tBoxNom.Name = "tBoxNom";
            this.tBoxNom.ReadOnly = true;
            this.tBoxNom.Size = new System.Drawing.Size(279, 22);
            this.tBoxNom.TabIndex = 11;
            // 
            // tBoxSuiviPatient
            // 
            this.tBoxSuiviPatient.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tBoxSuiviPatient.Location = new System.Drawing.Point(14, 299);
            this.tBoxSuiviPatient.Multiline = true;
            this.tBoxSuiviPatient.Name = "tBoxSuiviPatient";
            this.tBoxSuiviPatient.ReadOnly = true;
            this.tBoxSuiviPatient.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tBoxSuiviPatient.Size = new System.Drawing.Size(490, 113);
            this.tBoxSuiviPatient.TabIndex = 10;
            this.tBoxSuiviPatient.Text = "SuiviPatient";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(5, 223);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(111, 22);
            this.label10.TabIndex = 9;
            this.label10.Text = "Suplement info :";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(217, 121);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(45, 19);
            this.label9.TabIndex = 8;
            this.label9.Text = "ville:";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(3, 124);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 16);
            this.label8.TabIndex = 7;
            this.label8.Text = "Code Postal :";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(95, 95);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 15);
            this.label7.TabIndex = 6;
            this.label7.Text = "Rue :";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(5, 92);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 18);
            this.label6.TabIndex = 5;
            this.label6.Text = "N° :";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(5, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(162, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "Date Naissance :";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(329, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 16);
            this.label4.TabIndex = 1;
            this.label4.Text = "Sexe :";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(3, 174);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 19);
            this.label3.TabIndex = 3;
            this.label3.Text = "Téléphone :";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(386, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "Prénom :";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(3, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nom :";
            // 
            // bRechercher
            // 
            this.bRechercher.BackColor = System.Drawing.Color.Transparent;
            this.bRechercher.BackgroundImage = global::ImportSosGeneve.Properties.Resources.boutonjumelles;
            this.bRechercher.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bRechercher.FlatAppearance.BorderSize = 0;
            this.bRechercher.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bRechercher.ImageIndex = 0;
            this.bRechercher.Location = new System.Drawing.Point(166, 147);
            this.bRechercher.Name = "bRechercher";
            this.bRechercher.Size = new System.Drawing.Size(60, 60);
            this.bRechercher.TabIndex = 55;
            this.toolTip1.SetToolTip(this.bRechercher, "Rechercher selon les critères");
            this.bRechercher.UseVisualStyleBackColor = false;
            this.bRechercher.Click += new System.EventHandler(this.bRechercher_Click);
            // 
            // bImportePersonne
            // 
            this.bImportePersonne.BackColor = System.Drawing.Color.Transparent;
            this.bImportePersonne.BackgroundImage = global::ImportSosGeneve.Properties.Resources.bEnvoi;
            this.bImportePersonne.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bImportePersonne.Enabled = false;
            this.bImportePersonne.FlatAppearance.BorderSize = 0;
            this.bImportePersonne.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bImportePersonne.ImageIndex = 1;
            this.bImportePersonne.ImageList = this.imageList1;
            this.bImportePersonne.Location = new System.Drawing.Point(166, 449);
            this.bImportePersonne.Name = "bImportePersonne";
            this.bImportePersonne.Size = new System.Drawing.Size(60, 60);
            this.bImportePersonne.TabIndex = 60;
            this.toolTip1.SetToolTip(this.bImportePersonne, "Importer cette personne");
            this.bImportePersonne.UseVisualStyleBackColor = false;
            this.bImportePersonne.Click += new System.EventHandler(this.bImportePersonne_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "BNvxDossierOn.png");
            this.imageList1.Images.SetKeyName(1, "BNvxDossierOff.png");
            this.imageList1.Images.SetKeyName(2, "bExitOn.png");
            this.imageList1.Images.SetKeyName(3, "bExitOff.png");
            // 
            // bQuitterAjoutDest
            // 
            this.bQuitterAjoutDest.BackColor = System.Drawing.Color.Transparent;
            this.bQuitterAjoutDest.BackgroundImage = global::ImportSosGeneve.Properties.Resources.bEnvoi;
            this.bQuitterAjoutDest.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bQuitterAjoutDest.FlatAppearance.BorderSize = 0;
            this.bQuitterAjoutDest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bQuitterAjoutDest.ImageIndex = 2;
            this.bQuitterAjoutDest.ImageList = this.imageList1;
            this.bQuitterAjoutDest.Location = new System.Drawing.Point(1025, 451);
            this.bQuitterAjoutDest.Name = "bQuitterAjoutDest";
            this.bQuitterAjoutDest.Size = new System.Drawing.Size(60, 60);
            this.bQuitterAjoutDest.TabIndex = 60;
            this.toolTip1.SetToolTip(this.bQuitterAjoutDest, "Quitter");
            this.bQuitterAjoutDest.UseVisualStyleBackColor = false;
            this.bQuitterAjoutDest.Click += new System.EventHandler(this.bQuitterAjoutDest_Click);
            // 
            // frmRecherchePersonne
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.ClientSize = new System.Drawing.Size(1107, 521);
            this.Controls.Add(this.bQuitterAjoutDest);
            this.Controls.Add(this.bImportePersonne);
            this.Controls.Add(this.bRechercher);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.txtRecherchePrenom);
            this.Controls.Add(this.TxtFiltre3);
            this.Controls.Add(this.TxtFiltre2);
            this.Controls.Add(this.TxtFiltre1);
            this.Controls.Add(this.ChkFiltre3);
            this.Controls.Add(this.ChkFiltre2);
            this.Controls.Add(this.ChkFiltre1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmRecherchePersonne";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Recherche d\'une personne";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
     		

		private void listView1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(listView1.SelectedIndices.Count>0)
			{
				DataRow row = (DataRow)listView1.SelectedItems[0].Tag;
				if(row!=null)
				{
                    tBoxNom.Text = row["NomPatient"].ToString();
                    tBoxPrenom.Text = row["PreNomPatient"].ToString();
                    tBoxSex.Text = row["Sexe"].ToString();

                    //On re-détermine l'âge
                    int age = 0;
                    if (row["DateNaissance"].ToString() != "")
                    {
                        DateTime DateNaiss;
                       
                        if (DateTime.TryParse(row["DateNaissance"].ToString(), out DateNaiss))
                        {
                            age = CalculeAge(DateNaiss);
                        }
                        else age = 0;
                    }
                                        
                    tBoxAge.Text = age.ToString();
                    tBoxTel.Text = row["TelPatient"].ToString();
                    tBoxDateNaiss.Text = row["DateNaissance"].ToString();
                    tBoxNumRue.Text = row["NumeroDansRue"].ToString();
                    tBoxRue.Text = row["Rue"].ToString();
                    tBoxCP.Text = row["CodePostal"].ToString();
                    tBoxVille.Text = row["Commune"].ToString();
                    tBoxSupinfo.Text = row["TexteSup"].ToString();
					tBoxSuiviPatient.Text = row["SuiviPatient"].ToString();
					panel1.Visible = true;
					m_Row = row;
					                                       
                    //On active le bouton
                    bImportePersonne.ImageIndex = 0;
                    bImportePersonne.Enabled = true;

					try
					{
						m_lngPatient = long.Parse(row["IdPatient"].ToString());
					}
					catch
					{
						m_lngPatient= -1;
					}
					try
					{
						m_lngPersonne = long.Parse(row["IdPersonne"].ToString());
					}
					catch
					{
						m_lngPersonne= -1;
					}
				}
                else
                {
                    //On dé-active le bouton
                    bImportePersonne.ImageIndex = 1;
                    bImportePersonne.Enabled = false;
                }
			}
		}

        

        public DataSet GetPersonne(string Q_Nom, string Q_Prenom, string Q_DateNaiss, string Q_Tel, int Q_TypeRech)
        {
           // string sqlstr = "";          //initialisation de la chaine sql
            string selonchoix = "";        //idem pour la partie selon le choix 
            string selonchoix1 = "";       //idem pour la partie selon le choix             

            if(Q_Tel != "")
            {
                Q_Tel = Q_Tel.Replace(" ", "");       //On commence par enlever les espaces

                if (Q_Tel.IndexOf("+") == -1)
                {
                    if (Q_Tel.Substring(0, 1) == "0")
                        Q_Tel = "+41" + Q_Tel.Remove(0, 1);
                    else Q_Tel = "+" + Q_Tel;
                }
                else
                {   //On le reformate
                    Q_Tel = "+" + Q_Tel.Replace("+", "");
                }
            }

            //en fonction du choix
            switch (Q_TypeRech)
            {
                case 0:     //Rien de coché
                    break;

                case 1:   //Tel
                    selonchoix = " AND pe.Tel = '" + Q_Tel + "'";
                    selonchoix += @" UNION
                                     SELECT pe.IdPersonne,pe.Nom as NomPatient, pe.Prenom as PrenomPatient, tp.NumTel as TelPatient,
                                     pe.NumAdresse,pe.CodePostal,pe.Departement,pe.Commune,pe.Rue,pe.NumeroDansRue,pe.Batiment,
                                     pe.Etage,pe.Escalier,pe.Digicode,pe.Internom,pe.Porte,pe.longitude,pe.Latitude,pe.DateNaissance,
                                     pe.sexe,pe.Age,pe.UniteAge,pe.TexteSup,pa.IdPatient,pa.TypeAbonnement, pe.StopRappelTA, pa.SuiviPatient 
                                     FROM tablepersonne pe, Tel_Personne tp, tablepatient pa                                         
                                     WHERE pe.IdPersonne = pa.IdPersonne
                                     AND pe.IdPersonne = tp.NumPersonne 
                                     AND tp.NumTel =  '" + Q_Tel + "'";                   
                    break;


                case 2:   //Nom Prenom
                    if (Q_Nom != "" && Q_Nom != null)
                    {
                        Q_Nom = Q_Nom.Replace("'", "''");
                        selonchoix = " AND pe.Nom LIKE LOWER('" + Q_Nom.ToLower() + "%')";
                    }

                    if (Q_Prenom != "" && Q_Prenom != null)
                        selonchoix += " AND pe.Prenom LIKE LOWER ('%" + Q_Prenom.ToLower() + "%')";
                    //if (Q_DateNaiss != "")
                      //  selonchoix += " AND pe.DateNaissance = '" + Q_DateNaiss + "'";
                    break;

                case 3:   //Tel + Nom Prenom 
                    selonchoix = " AND pe.Tel = '" + Q_Tel + "'";

                    if (Q_Nom != "" && Q_Nom != null)
                    {
                        Q_Nom = Q_Nom.Replace("'", "''");
                        selonchoix += " AND pe.Nom LIKE LOWER('" + Q_Nom.ToLower() + "%')";
                    }

                    if (Q_Prenom != "" && Q_Prenom != null)
                        selonchoix += " AND pe.Prenom LIKE LOWER ('%" + Q_Prenom.ToLower() + "%')";

                    selonchoix += @" UNION
                                     SELECT pe.IdPersonne,pe.Nom as NomPatient, pe.Prenom as PrenomPatient, tp.NumTel as TelPatient,
                                     pe.NumAdresse,pe.CodePostal,pe.Departement,pe.Commune,pe.Rue,pe.NumeroDansRue,pe.Batiment,
                                     pe.Etage,pe.Escalier,pe.Digicode,pe.Internom,pe.Porte,pe.longitude,pe.Latitude,pe.DateNaissance,
                                     pe.sexe,pe.Age,pe.UniteAge,pe.TexteSup,pa.IdPatient,pa.TypeAbonnement, pe.StopRappelTA, pa.SuiviPatient 
                                     FROM tablepersonne pe, Tel_Personne tp, tablepatient pa                                         
                                     WHERE pe.IdPersonne = pa.IdPersonne
                                     AND pe.IdPersonne = tp.NumPersonne 
                                     AND tp.NumTel =  '" + Q_Tel + "'";        
                    
                    
                    if (Q_Nom != "" && Q_Nom != null)
                    {
                        Q_Nom = Q_Nom.Replace("'", "''");
                        selonchoix += " AND pe.Nom LIKE LOWER('" + Q_Nom.ToLower() + "%')";
                    }

                    if (Q_Prenom != "" && Q_Prenom != null)
                        selonchoix += " AND pe.Prenom LIKE LOWER ('%" + Q_Prenom.ToLower() + "%')";
                   
                    break;


                case 4:   //DateNaiss
                    if (Q_DateNaiss != "" && Q_DateNaiss != null)
                    {
                        //On verrifie la date
                        DateTime Testdate;

                        if (DateTime.TryParse(Q_DateNaiss, out Testdate))
                        {
                            //Si elle est valide....
                            selonchoix = " AND pe.DateNaissance = '" + Q_DateNaiss + "'";
                        }
                    }
                    break;


                case 5:     //Tel + DateNaiss
                    selonchoix = " AND pe.Tel = '" + Q_Tel + "'";

                    if (Q_DateNaiss != "" && Q_DateNaiss != null)
                    {
                        //On verrifie la date
                        DateTime Testdate;

                        if (DateTime.TryParse(Q_DateNaiss, out Testdate))
                        {
                            //Si elle est valide....
                            selonchoix += " AND pe.DateNaissance = '" + Q_DateNaiss + "'";
                        }
                    }

                    selonchoix += @" UNION
                                     SELECT pe.IdPersonne,pe.Nom as NomPatient, pe.Prenom as PrenomPatient, tp.NumTel as TelPatient,
                                     pe.NumAdresse,pe.CodePostal,pe.Departement,pe.Commune,pe.Rue,pe.NumeroDansRue,pe.Batiment,
                                     pe.Etage,pe.Escalier,pe.Digicode,pe.Internom,pe.Porte,pe.longitude,pe.Latitude,pe.DateNaissance,
                                     pe.sexe,pe.Age,pe.UniteAge,pe.TexteSup,pa.IdPatient,pa.TypeAbonnement, pe.StopRappelTA, pa.SuiviPatient 
                                     FROM tablepersonne pe, Tel_Personne tp, tablepatient pa                                         
                                     WHERE pe.IdPersonne = pa.IdPersonne
                                     AND pe.IdPersonne = tp.NumPersonne 
                                     AND tp.NumTel =  '" + Q_Tel + "'";


                    if (Q_DateNaiss != "" && Q_DateNaiss != null)
                    {
                        //On verrifie la date
                        DateTime Testdate;

                        if (DateTime.TryParse(Q_DateNaiss, out Testdate))
                        {
                            //Si elle est valide....
                            selonchoix += " AND pe.DateNaissance = '" + Q_DateNaiss + "'";
                        }
                    }
                    break;

                case 6:     //Nom Prenom + DateNaiss
                    if (Q_Nom != "" && Q_Nom != null)
                    {
                        Q_Nom = Q_Nom.Replace("'", "''");
                        selonchoix = " AND pe.Nom LIKE LOWER('" + Q_Nom.ToLower() + "%')";
                    }

                    if (Q_Prenom != "" && Q_Prenom != null)
                        selonchoix += " AND pe.Prenom LIKE LOWER ('%" + Q_Prenom.ToLower() + "%')";

                    if (Q_DateNaiss != "" && Q_DateNaiss != null)
                    {
                        //On verrifie la date
                        DateTime Testdate;

                        if (DateTime.TryParse(Q_DateNaiss, out Testdate))
                        {
                            //Si elle est valide....
                            selonchoix += " AND pe.DateNaissance = '" + Q_DateNaiss + "'";
                        }
                    }
                    break;

                case 7:     //Tout
                    selonchoix = " AND pe.Tel = '" + Q_Tel + "'";
                    
                    if (Q_Nom != "" && Q_Nom != null)
                    {
                        Q_Nom = Q_Nom.Replace("'", "''");
                        selonchoix += " AND pe.Nom LIKE LOWER('" + Q_Nom.ToLower() + "%')";
                    }

                    if (Q_Prenom != "" && Q_Prenom != null)
                        selonchoix += " AND pe.Prenom LIKE LOWER ('%" + Q_Prenom.ToLower() + "%')";

                    if (Q_DateNaiss != "" && Q_DateNaiss != null)
                    {
                        //On verrifie la date
                        DateTime Testdate;

                        if (DateTime.TryParse(Q_DateNaiss, out Testdate))
                        {
                            //Si elle est valide....
                            selonchoix += " AND pe.DateNaissance = '" + Q_DateNaiss + "'";
                        }
                    }

                    selonchoix += @" UNION
                                     SELECT pe.IdPersonne,pe.Nom as NomPatient, pe.Prenom as PrenomPatient, tp.NumTel as TelPatient,
                                     pe.NumAdresse,pe.CodePostal,pe.Departement,pe.Commune,pe.Rue,pe.NumeroDansRue,pe.Batiment,
                                     pe.Etage,pe.Escalier,pe.Digicode,pe.Internom,pe.Porte,pe.longitude,pe.Latitude,pe.DateNaissance,
                                     pe.sexe,pe.Age,pe.UniteAge,pe.TexteSup,pa.IdPatient,pa.TypeAbonnement, pe.StopRappelTA, pa.SuiviPatient 
                                     FROM tablepersonne pe, Tel_Personne tp, tablepatient pa                                         
                                     WHERE pe.IdPersonne = pa.IdPersonne
                                     AND pe.IdPersonne = tp.NumPersonne 
                                     AND tp.NumTel =  '" + Q_Tel + "'";        
                    
                    
                    if (Q_Nom != "" && Q_Nom != null)
                    {
                        Q_Nom = Q_Nom.Replace("'", "''");
                        selonchoix += " AND pe.Nom LIKE LOWER('" + Q_Nom.ToLower() + "%')";
                    }

                    if (Q_Prenom != "" && Q_Prenom != null)
                        selonchoix += " AND pe.Prenom LIKE LOWER ('%" + Q_Prenom.ToLower() + "%')";

                    if (Q_DateNaiss != "" && Q_DateNaiss != null)
                    {
                        //On verrifie la date
                        DateTime Testdate;

                        if (DateTime.TryParse(Q_DateNaiss, out Testdate))
                        {
                            //Si elle est valide....
                            selonchoix += " AND pe.DateNaissance = '" + Q_DateNaiss + "'";
                        }
                    }
                    break;
            }


            //SELECT pe.IdPersonne,pe.Nom as 'NomPatient' ,pe.Prenom as 'PrenomPatient',pe.Tel as 'TelPatient',
            //pe.NumAdresse,pe.CodePostal,pe.Departement,pe.Commune,pe.Rue,pe.NumeroDansRue,pe.Batiment,
            //pe.Etage,pe.Escalier,pe.Digicode,pe.Internom,pe.Porte,pe.longitude,pe.Latitude,pe.DateNaissance,
            //pe.sexe,pe.Age,pe.UniteAge,pe.TexteSup,pa.IdPatient,pa.TypeAbonnement, pe.StopRappelTA, pa.SuiviPatient 
            //from tablepersonne pe left join tablepatient pa on pa.IdPersonne = pe.IdPersonne " + strFiltre

            
            //Connexion à la base
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);

            dbConnection.Open();

            SqlCommand cmd = dbConnection.CreateCommand();

            //on déclare le DataSet pour recevoir les données
            DataSet DS = new DataSet();

            try
            {

            string sqlstr = selonchoix1;

            sqlstr += @"SELECT pe.IdPersonne,pe.Nom as NomPatient, pe.Prenom as PrenomPatient, pe.Tel as TelPatient,
                        pe.NumAdresse,pe.CodePostal,pe.Departement,pe.Commune,pe.Rue,pe.NumeroDansRue,pe.Batiment,
                        pe.Etage,pe.Escalier,pe.Digicode,pe.Internom,pe.Porte,pe.longitude,pe.Latitude,pe.DateNaissance,
                        pe.sexe,pe.Age,pe.UniteAge,pe.TexteSup,pa.IdPatient,pa.TypeAbonnement, pe.StopRappelTA, pa.SuiviPatient 
                        FROM tablepersonne pe, tablepatient pa                              
                        WHERE pe.IdPersonne = pa.IdPersonne ";

            sqlstr += selonchoix;

            //sqlstr += " order by TeleAlarme desc";

            cmd.CommandText = sqlstr;
                    
            DS.Tables.Add("Recherche");      //on déclare une table pour cet ensemble de donnée           
            DS.Tables["Recherche"].Load(cmd.ExecuteReader());
           
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

            //On retourne le DataSet;
            return DS;
        }



		public long IdPatient
		{
			get
			{
				return m_lngPatient;
			}
		}
		public long IdPersonne
		{
			get
			{
				return m_lngPersonne;
			}
		}
		public DataRow Personne
		{
			get
			{
				return m_Row;
			}
		}
	

        //Fonction qui calcule l'âge
        public int CalculeAge(DateTime anniversaire)
        {
            DateTime now = DateTime.Today;
            int age = now.Year - anniversaire.Year;
            if (anniversaire > now.AddYears(-age))
                age--;
            return age;
        }

        private void bRechercher_Click(object sender, EventArgs e)
        {           
            int Recherche = 0;
            if (ChkFiltre1.Checked)  //Tel
            {
                Recherche = 1;
            }
            if (ChkFiltre2.Checked)  //Nom Prenom
            {
                Recherche += 2;
            }
            if (ChkFiltre3.Checked)  //Date Naissance
            {
                Recherche += 4;
            }
          
            //On lance la recherche
            DataSet ds = GetPersonne(TxtFiltre2.Text, txtRecherchePrenom.Text, TxtFiltre3.Text, TxtFiltre1.Text, Recherche);

            listView1.Items.Clear();

            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ListViewItem item = new ListViewItem(ds.Tables[0].Rows[i][1].ToString() + " " + ds.Tables[0].Rows[i][2].ToString());
                    item.Tag = ds.Tables[0].Rows[i];
                    item.SubItems.Add(ds.Tables[0].Rows[i]["DateNaissance"].ToString());
                    listView1.Items.Add(item);
                }
            }
        }

        private void bImportePersonne_Click(object sender, EventArgs e)
        {
            if (m_Row != null)
            {
                if (m_lngPersonne > -1 && m_Row["TypeAbonnement"].ToString() == "TA")
                {
                    MessageBox.Show("Ce patient est déjà référencé comme Télé-Alarme dans la base. Impossible de l'importer!");
                    m_Row = null;
                    m_lngPatient = -1;
                    m_lngPersonne = -1;
                    return;
                }
            }

            PatientAccepte = true;

            this.Close();
        }

        private void bQuitterAjoutDest_Click(object sender, EventArgs e)
        {
            Close();
        }
	}
}
