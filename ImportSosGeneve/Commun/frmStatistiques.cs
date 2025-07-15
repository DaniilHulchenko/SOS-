using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Data;
using SosMedecins.SmartRapport.GestionApplication;
using System.Data.SqlClient;

namespace ImportSosGeneve
{
	/// <summary>
	/// Description résumée de frmStatistiques.
	/// </summary>
	public class frmStatistiques : System.Windows.Forms.Form
	{
		#region Déclration des variables

		// Variables générées par Cn

		private GradientCellType m_Gradient1;
		private GradientCellType m_Gradient2;

		private Color m_CouleurAlternative1 = Color.White;
		private Color m_CouleurAlternative2 = Color.White;
		private Point[] m_Points;
		private bool Alternate=false;
		//private string m_strLegende;
		private int m_intTempsEcoule = 0;
		private bool EnRoute = false;

		// Variables controles de la form

		private System.Windows.Forms.CheckBox chkOrigine;
		private System.Windows.Forms.ComboBox cbOrigine;
		private System.Windows.Forms.CheckBox chkDate;
		private System.Windows.Forms.CheckBox chkMotif;
		private System.Windows.Forms.CheckBox ChkMedecin;
		private System.Windows.Forms.DateTimePicker dateTimePicker2;
		private System.Windows.Forms.DateTimePicker dateTimePicker1;
		private System.Windows.Forms.ComboBox cbMotif;
		private System.Windows.Forms.ComboBox cbMedecin;
		private System.Windows.Forms.PictureBox btnGraphique;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton radioButton1;
		private System.Windows.Forms.RadioButton radioButton2;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.RadioButton radioButton3;
		private System.Windows.Forms.RadioButton radioButton4;
		private System.Windows.Forms.RadioButton radioButton5;
		private System.Windows.Forms.RadioButton radioButton6;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radioButton7;
		private System.Windows.Forms.RadioButton radioButton9;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.RadioButton radioButton10;
		private System.Windows.Forms.RadioButton radioButton11;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox cbFiltreValeur;
		private System.Windows.Forms.TextBox txtValeur;
		private System.Windows.Forms.RadioButton rdTriLegende;
		private System.Windows.Forms.RadioButton rdTriValeurs;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.RadioButton rdTriAsc;
		private System.Windows.Forms.RadioButton rdTriDesc;
		private System.Windows.Forms.RadioButton rdSansTri;
		private System.Windows.Forms.CheckBox chkLegende;
		private System.Windows.Forms.Panel panel6;
		private System.Windows.Forms.RadioButton rdBaton;
		private System.Windows.Forms.RadioButton rdBarre;
		private System.Windows.Forms.RadioButton rdPoints;
		private System.Windows.Forms.RadioButton rdSecteur;
		private System.Windows.Forms.RadioButton rdCourbe;
		private System.Windows.Forms.CheckBox chkValeurs;
		private System.Windows.Forms.PictureBox picImprGraphique;
		private System.Windows.Forms.PictureBox picImprTableau;
		private System.Drawing.Printing.PrintDocument printDocument1;
		private System.Windows.Forms.RadioButton radioButton12;
		private System.Windows.Forms.Timer timer1;
		private FarPoint.Win.Spread.FpSpread fpResultat;
		private FarPoint.Win.Spread.SheetView fpResultat_Sheet1;
		private System.Windows.Forms.Button button1;
		private System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter1;
		private System.Data.OleDb.OleDbCommand oleDbSelectCommand1;
		private System.Data.OleDb.OleDbConnection oleDbConnection1;
		private System.Data.DataSet dataSet1;
		private System.Windows.Forms.PictureBox pictureBox1;
        private RadioButton radioButton13;
        private RadioButton radioButton8;
        private RadioButton radioButton14;
        private RadioButton radioButton15;
		private System.ComponentModel.IContainer components;

		#endregion

		#region Construction / Destruction de la form

		// Constructeur de la classe

		public frmStatistiques()
		{
			//
			// Requis pour la prise en charge du Concepteur Windows Forms
			//
			InitializeComponent();

			ChargementListeMedecins();
			ChargementListeMotifs();
			ChargementListeOrigine();
			InitialiseFiltreDate();

			m_Gradient1 = new GradientCellType();
			m_Gradient1.BottomColor = Color.Gray;
			m_Gradient1.TopColor = Color.White;
			m_Gradient2 = new GradientCellType();
			m_Gradient2.BottomColor = Color.Orange;
			m_Gradient1.TopColor = Color.White;
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

		#region On construit les différents listes e filtre...

		private void ChargementListeMedecins()
		{
			cbMedecin.Items.Clear();
			string[][] ListeNoms = OutilsExt.OutilsSql.ListeMedecins();
			foreach(string[] s in ListeNoms)
			{
				ListItem item = new ListItem(s[0],s[1]);
				cbMedecin.Items.Add(item);
			}
		}

		private void ChargementListeMotifs()
		{
			cbMotif.Items.Clear();
			string[] ListeMotifs = OutilsExt.OutilsSql.ListeMotifs();
			foreach(string s in ListeMotifs)
				cbMotif.Items.Add(s);
		}

		private void ChargementListeOrigine()
		{
			cbOrigine.Items.Clear();
			string[] ListeOrigine = OutilsExt.OutilsSql.ListeOrigine();
			foreach(string s in ListeOrigine)
				cbOrigine.Items.Add(s);
		}

		private void InitialiseFiltreDate()
		{
			dateTimePicker1.Value = DateTime.Parse(DateTime.Now.AddDays(-1).ToShortDateString() + " 00:00:00");
            dateTimePicker2.Value = DateTime.Parse(DateTime.Now.ToShortDateString() + " 23:59:59");
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStatistiques));
            this.chkOrigine = new System.Windows.Forms.CheckBox();
            this.cbOrigine = new System.Windows.Forms.ComboBox();
            this.chkDate = new System.Windows.Forms.CheckBox();
            this.chkMotif = new System.Windows.Forms.CheckBox();
            this.ChkMedecin = new System.Windows.Forms.CheckBox();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.cbMotif = new System.Windows.Forms.ComboBox();
            this.cbMedecin = new System.Windows.Forms.ComboBox();
            this.btnGraphique = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton15 = new System.Windows.Forms.RadioButton();
            this.radioButton12 = new System.Windows.Forms.RadioButton();
            this.picImprTableau = new System.Windows.Forms.PictureBox();
            this.picImprGraphique = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtValeur = new System.Windows.Forms.TextBox();
            this.cbFiltreValeur = new System.Windows.Forms.ComboBox();
            this.radioButton11 = new System.Windows.Forms.RadioButton();
            this.radioButton10 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButton6 = new System.Windows.Forms.RadioButton();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.radioButton14 = new System.Windows.Forms.RadioButton();
            this.radioButton8 = new System.Windows.Forms.RadioButton();
            this.radioButton13 = new System.Windows.Forms.RadioButton();
            this.radioButton9 = new System.Windows.Forms.RadioButton();
            this.radioButton7 = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.chkValeurs = new System.Windows.Forms.CheckBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.rdCourbe = new System.Windows.Forms.RadioButton();
            this.rdSecteur = new System.Windows.Forms.RadioButton();
            this.rdPoints = new System.Windows.Forms.RadioButton();
            this.rdBarre = new System.Windows.Forms.RadioButton();
            this.rdBaton = new System.Windows.Forms.RadioButton();
            this.chkLegende = new System.Windows.Forms.CheckBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.rdTriAsc = new System.Windows.Forms.RadioButton();
            this.rdTriDesc = new System.Windows.Forms.RadioButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.rdSansTri = new System.Windows.Forms.RadioButton();
            this.rdTriLegende = new System.Windows.Forms.RadioButton();
            this.rdTriValeurs = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.fpResultat = new FarPoint.Win.Spread.FpSpread();
            this.fpResultat_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.button1 = new System.Windows.Forms.Button();
            this.oleDbDataAdapter1 = new System.Data.OleDb.OleDbDataAdapter();
            this.oleDbSelectCommand1 = new System.Data.OleDb.OleDbCommand();
            this.oleDbConnection1 = new System.Data.OleDb.OleDbConnection();
            this.dataSet1 = new System.Data.DataSet();
            ((System.ComponentModel.ISupportInitialize)(this.btnGraphique)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picImprTableau)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picImprGraphique)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpResultat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpResultat_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // chkOrigine
            // 
            this.chkOrigine.Location = new System.Drawing.Point(11, 112);
            this.chkOrigine.Name = "chkOrigine";
            this.chkOrigine.Size = new System.Drawing.Size(61, 16);
            this.chkOrigine.TabIndex = 28;
            this.chkOrigine.Text = "Origine";
            // 
            // cbOrigine
            // 
            this.cbOrigine.Location = new System.Drawing.Point(78, 110);
            this.cbOrigine.Name = "cbOrigine";
            this.cbOrigine.Size = new System.Drawing.Size(181, 21);
            this.cbOrigine.TabIndex = 27;
            // 
            // chkDate
            // 
            this.chkDate.Location = new System.Drawing.Point(11, 92);
            this.chkDate.Name = "chkDate";
            this.chkDate.Size = new System.Drawing.Size(64, 16);
            this.chkDate.TabIndex = 26;
            this.chkDate.Text = "Date";
            // 
            // chkMotif
            // 
            this.chkMotif.Location = new System.Drawing.Point(11, 71);
            this.chkMotif.Name = "chkMotif";
            this.chkMotif.Size = new System.Drawing.Size(61, 16);
            this.chkMotif.TabIndex = 25;
            this.chkMotif.Text = "Motif";
            // 
            // ChkMedecin
            // 
            this.ChkMedecin.Location = new System.Drawing.Point(11, 51);
            this.ChkMedecin.Name = "ChkMedecin";
            this.ChkMedecin.Size = new System.Drawing.Size(67, 16);
            this.ChkMedecin.TabIndex = 24;
            this.ChkMedecin.Text = "Médecin";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker2.Location = new System.Drawing.Point(167, 90);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(92, 20);
            this.dateTimePicker2.TabIndex = 23;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(78, 90);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(90, 20);
            this.dateTimePicker1.TabIndex = 22;
            // 
            // cbMotif
            // 
            this.cbMotif.Location = new System.Drawing.Point(78, 69);
            this.cbMotif.Name = "cbMotif";
            this.cbMotif.Size = new System.Drawing.Size(181, 21);
            this.cbMotif.TabIndex = 21;
            // 
            // cbMedecin
            // 
            this.cbMedecin.Location = new System.Drawing.Point(78, 47);
            this.cbMedecin.Name = "cbMedecin";
            this.cbMedecin.Size = new System.Drawing.Size(181, 21);
            this.cbMedecin.TabIndex = 20;
            // 
            // btnGraphique
            // 
            this.btnGraphique.Image = ((System.Drawing.Image)(resources.GetObject("btnGraphique.Image")));
            this.btnGraphique.Location = new System.Drawing.Point(289, 62);
            this.btnGraphique.Name = "btnGraphique";
            this.btnGraphique.Size = new System.Drawing.Size(45, 33);
            this.btnGraphique.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnGraphique.TabIndex = 29;
            this.btnGraphique.TabStop = false;
            this.btnGraphique.Click += new System.EventHandler(this.btnGraphique_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(6, 304);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(779, 496);
            this.panel1.TabIndex = 30;
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(276, 49);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(433, 306);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.CadetBlue;
            this.groupBox1.Controls.Add(this.radioButton15);
            this.groupBox1.Controls.Add(this.radioButton12);
            this.groupBox1.Controls.Add(this.picImprTableau);
            this.groupBox1.Controls.Add(this.picImprGraphique);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtValeur);
            this.groupBox1.Controls.Add(this.cbFiltreValeur);
            this.groupBox1.Controls.Add(this.radioButton11);
            this.groupBox1.Controls.Add(this.radioButton10);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.radioButton6);
            this.groupBox1.Controls.Add(this.radioButton5);
            this.groupBox1.Controls.Add(this.radioButton4);
            this.groupBox1.Controls.Add(this.radioButton3);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.btnGraphique);
            this.groupBox1.Location = new System.Drawing.Point(324, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(347, 290);
            this.groupBox1.TabIndex = 31;
            this.groupBox1.TabStop = false;
            // 
            // radioButton15
            // 
            this.radioButton15.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton15.Location = new System.Drawing.Point(14, 152);
            this.radioButton15.Name = "radioButton15";
            this.radioButton15.Size = new System.Drawing.Size(207, 17);
            this.radioButton15.TabIndex = 39;
            this.radioButton15.Text = "Calcul délais de trajet/Patient";
            // 
            // radioButton12
            // 
            this.radioButton12.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton12.Location = new System.Drawing.Point(14, 92);
            this.radioButton12.Name = "radioButton12";
            this.radioButton12.Size = new System.Drawing.Size(207, 17);
            this.radioButton12.TabIndex = 38;
            this.radioButton12.Text = "Nombre d\'appels par mois";
            // 
            // picImprTableau
            // 
            this.picImprTableau.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picImprTableau.Image = ((System.Drawing.Image)(resources.GetObject("picImprTableau.Image")));
            this.picImprTableau.Location = new System.Drawing.Point(290, 157);
            this.picImprTableau.Name = "picImprTableau";
            this.picImprTableau.Size = new System.Drawing.Size(43, 36);
            this.picImprTableau.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picImprTableau.TabIndex = 37;
            this.picImprTableau.TabStop = false;
            this.toolTip1.SetToolTip(this.picImprTableau, "Impression du tableau");
            this.picImprTableau.Click += new System.EventHandler(this.picImprTableau_Click);
            // 
            // picImprGraphique
            // 
            this.picImprGraphique.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picImprGraphique.Image = ((System.Drawing.Image)(resources.GetObject("picImprGraphique.Image")));
            this.picImprGraphique.Location = new System.Drawing.Point(290, 109);
            this.picImprGraphique.Name = "picImprGraphique";
            this.picImprGraphique.Size = new System.Drawing.Size(43, 36);
            this.picImprGraphique.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picImprGraphique.TabIndex = 36;
            this.picImprGraphique.TabStop = false;
            this.toolTip1.SetToolTip(this.picImprGraphique, "Impression du graphique");
            this.picImprGraphique.Click += new System.EventHandler(this.picImprGraphique_Click);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(92, 262);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 19);
            this.label4.TabIndex = 35;
            this.label4.Text = "Nombre d\'appels :";
            this.label4.Visible = false;
            // 
            // txtValeur
            // 
            this.txtValeur.Location = new System.Drawing.Point(296, 259);
            this.txtValeur.Name = "txtValeur";
            this.txtValeur.Size = new System.Drawing.Size(40, 20);
            this.txtValeur.TabIndex = 34;
            this.txtValeur.Visible = false;
            // 
            // cbFiltreValeur
            // 
            this.cbFiltreValeur.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFiltreValeur.Items.AddRange(new object[] {
            "(tous)",
            ">",
            "<"});
            this.cbFiltreValeur.Location = new System.Drawing.Point(219, 259);
            this.cbFiltreValeur.Name = "cbFiltreValeur";
            this.cbFiltreValeur.Size = new System.Drawing.Size(72, 21);
            this.cbFiltreValeur.TabIndex = 33;
            this.cbFiltreValeur.Visible = false;
            // 
            // radioButton11
            // 
            this.radioButton11.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton11.Location = new System.Drawing.Point(14, 171);
            this.radioButton11.Name = "radioButton11";
            this.radioButton11.Size = new System.Drawing.Size(207, 16);
            this.radioButton11.TabIndex = 32;
            this.radioButton11.Text = "Calcul délais d\'attente/Patient";
            // 
            // radioButton10
            // 
            this.radioButton10.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton10.Location = new System.Drawing.Point(14, 225);
            this.radioButton10.Name = "radioButton10";
            this.radioButton10.Size = new System.Drawing.Size(218, 27);
            this.radioButton10.TabIndex = 31;
            this.radioButton10.Text = "Nombre d\'appels par age";
            this.radioButton10.Visible = false;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(14, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(324, 39);
            this.label1.TabIndex = 30;
            this.label1.Text = "Rubrique";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // radioButton6
            // 
            this.radioButton6.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton6.Location = new System.Drawing.Point(14, 211);
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.Size = new System.Drawing.Size(218, 15);
            this.radioButton6.TabIndex = 5;
            this.radioButton6.Text = "Nombre d\'appels annulés par motif";
            this.radioButton6.Visible = false;
            // 
            // radioButton5
            // 
            this.radioButton5.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton5.Location = new System.Drawing.Point(14, 190);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(206, 26);
            this.radioButton5.TabIndex = 4;
            this.radioButton5.Text = "Nombre d\'appels par motif";
            this.radioButton5.Visible = false;
            // 
            // radioButton4
            // 
            this.radioButton4.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton4.Location = new System.Drawing.Point(14, 131);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(206, 23);
            this.radioButton4.TabIndex = 3;
            this.radioButton4.Text = "Nombre d\'appels par origine";
            // 
            // radioButton3
            // 
            this.radioButton3.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton3.Location = new System.Drawing.Point(14, 105);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(206, 33);
            this.radioButton3.TabIndex = 2;
            this.radioButton3.Text = "Nombre d\'appels par médecin";
            // 
            // radioButton2
            // 
            this.radioButton2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton2.Location = new System.Drawing.Point(14, 71);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(205, 22);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "Nombre d\'appels par jour";
            // 
            // radioButton1
            // 
            this.radioButton1.Checked = true;
            this.radioButton1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton1.Location = new System.Drawing.Point(14, 53);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(205, 18);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Nombre d\'appels par heure";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.CadetBlue;
            this.panel2.Controls.Add(this.radioButton14);
            this.panel2.Controls.Add(this.radioButton8);
            this.panel2.Controls.Add(this.radioButton13);
            this.panel2.Controls.Add(this.radioButton9);
            this.panel2.Controls.Add(this.radioButton7);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.cbMedecin);
            this.panel2.Controls.Add(this.cbMotif);
            this.panel2.Controls.Add(this.dateTimePicker1);
            this.panel2.Controls.Add(this.dateTimePicker2);
            this.panel2.Controls.Add(this.ChkMedecin);
            this.panel2.Controls.Add(this.chkMotif);
            this.panel2.Controls.Add(this.chkDate);
            this.panel2.Controls.Add(this.cbOrigine);
            this.panel2.Controls.Add(this.chkOrigine);
            this.panel2.Location = new System.Drawing.Point(6, 7);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(313, 291);
            this.panel2.TabIndex = 32;
            // 
            // radioButton14
            // 
            this.radioButton14.Location = new System.Drawing.Point(12, 179);
            this.radioButton14.Name = "radioButton14";
            this.radioButton14.Size = new System.Drawing.Size(135, 28);
            this.radioButton14.TabIndex = 35;
            this.radioButton14.Text = "Autres";
            // 
            // radioButton8
            // 
            this.radioButton8.Location = new System.Drawing.Point(47, 226);
            this.radioButton8.Name = "radioButton8";
            this.radioButton8.Size = new System.Drawing.Size(161, 34);
            this.radioButton8.TabIndex = 34;
            this.radioButton8.Text = "Dont Consultations Annulés";
            // 
            // radioButton13
            // 
            this.radioButton13.Location = new System.Drawing.Point(47, 203);
            this.radioButton13.Name = "radioButton13";
            this.radioButton13.Size = new System.Drawing.Size(212, 34);
            this.radioButton13.TabIndex = 33;
            this.radioButton13.Text = "Dont Conseils téléphoniques";
            // 
            // radioButton9
            // 
            this.radioButton9.Location = new System.Drawing.Point(12, 159);
            this.radioButton9.Name = "radioButton9";
            this.radioButton9.Size = new System.Drawing.Size(156, 21);
            this.radioButton9.TabIndex = 32;
            this.radioButton9.Text = "Consultations Facturées";
            // 
            // radioButton7
            // 
            this.radioButton7.Checked = true;
            this.radioButton7.Location = new System.Drawing.Point(12, 138);
            this.radioButton7.Name = "radioButton7";
            this.radioButton7.Size = new System.Drawing.Size(156, 21);
            this.radioButton7.TabIndex = 30;
            this.radioButton7.TabStop = true;
            this.radioButton7.Text = "Total des Consultations";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(13, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(275, 28);
            this.label2.TabIndex = 29;
            this.label2.Text = "Filtre";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.CadetBlue;
            this.panel3.Controls.Add(this.chkValeurs);
            this.panel3.Controls.Add(this.panel6);
            this.panel3.Controls.Add(this.chkLegende);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Location = new System.Drawing.Point(674, 7);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(264, 265);
            this.panel3.TabIndex = 33;
            // 
            // chkValeurs
            // 
            this.chkValeurs.Checked = true;
            this.chkValeurs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkValeurs.Location = new System.Drawing.Point(7, 203);
            this.chkValeurs.Name = "chkValeurs";
            this.chkValeurs.Size = new System.Drawing.Size(165, 16);
            this.chkValeurs.TabIndex = 38;
            this.chkValeurs.Text = "Valeurs sur le graphique";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.rdCourbe);
            this.panel6.Controls.Add(this.rdSecteur);
            this.panel6.Controls.Add(this.rdPoints);
            this.panel6.Controls.Add(this.rdBarre);
            this.panel6.Controls.Add(this.rdBaton);
            this.panel6.Location = new System.Drawing.Point(160, 48);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(95, 126);
            this.panel6.TabIndex = 37;
            // 
            // rdCourbe
            // 
            this.rdCourbe.Location = new System.Drawing.Point(7, 74);
            this.rdCourbe.Name = "rdCourbe";
            this.rdCourbe.Size = new System.Drawing.Size(82, 22);
            this.rdCourbe.TabIndex = 4;
            this.rdCourbe.Text = "Courbe";
            // 
            // rdSecteur
            // 
            this.rdSecteur.Location = new System.Drawing.Point(7, 100);
            this.rdSecteur.Name = "rdSecteur";
            this.rdSecteur.Size = new System.Drawing.Size(82, 22);
            this.rdSecteur.TabIndex = 3;
            this.rdSecteur.Text = "Secteur";
            // 
            // rdPoints
            // 
            this.rdPoints.Location = new System.Drawing.Point(7, 52);
            this.rdPoints.Name = "rdPoints";
            this.rdPoints.Size = new System.Drawing.Size(82, 22);
            this.rdPoints.TabIndex = 2;
            this.rdPoints.Text = "Points";
            // 
            // rdBarre
            // 
            this.rdBarre.Location = new System.Drawing.Point(7, 28);
            this.rdBarre.Name = "rdBarre";
            this.rdBarre.Size = new System.Drawing.Size(83, 22);
            this.rdBarre.TabIndex = 1;
            this.rdBarre.Text = "Barre";
            // 
            // rdBaton
            // 
            this.rdBaton.Checked = true;
            this.rdBaton.Location = new System.Drawing.Point(6, 6);
            this.rdBaton.Name = "rdBaton";
            this.rdBaton.Size = new System.Drawing.Size(84, 22);
            this.rdBaton.TabIndex = 0;
            this.rdBaton.TabStop = true;
            this.rdBaton.Text = "Bâtons";
            // 
            // chkLegende
            // 
            this.chkLegende.Location = new System.Drawing.Point(7, 182);
            this.chkLegende.Name = "chkLegende";
            this.chkLegende.Size = new System.Drawing.Size(165, 16);
            this.chkLegende.TabIndex = 36;
            this.chkLegende.Text = "Légende sur le graphique";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.rdTriAsc);
            this.panel5.Controls.Add(this.rdTriDesc);
            this.panel5.Location = new System.Drawing.Point(6, 125);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(149, 49);
            this.panel5.TabIndex = 35;
            // 
            // rdTriAsc
            // 
            this.rdTriAsc.Checked = true;
            this.rdTriAsc.Location = new System.Drawing.Point(14, 7);
            this.rdTriAsc.Name = "rdTriAsc";
            this.rdTriAsc.Size = new System.Drawing.Size(122, 17);
            this.rdTriAsc.TabIndex = 32;
            this.rdTriAsc.TabStop = true;
            this.rdTriAsc.Text = "Tri ascendant";
            // 
            // rdTriDesc
            // 
            this.rdTriDesc.Location = new System.Drawing.Point(14, 26);
            this.rdTriDesc.Name = "rdTriDesc";
            this.rdTriDesc.Size = new System.Drawing.Size(122, 17);
            this.rdTriDesc.TabIndex = 33;
            this.rdTriDesc.Text = "Tri descendant";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.rdSansTri);
            this.panel4.Controls.Add(this.rdTriLegende);
            this.panel4.Controls.Add(this.rdTriValeurs);
            this.panel4.Location = new System.Drawing.Point(6, 48);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(149, 70);
            this.panel4.TabIndex = 34;
            // 
            // rdSansTri
            // 
            this.rdSansTri.Location = new System.Drawing.Point(14, 48);
            this.rdSansTri.Name = "rdSansTri";
            this.rdSansTri.Size = new System.Drawing.Size(122, 17);
            this.rdSansTri.TabIndex = 34;
            this.rdSansTri.Text = "Sans tri";
            // 
            // rdTriLegende
            // 
            this.rdTriLegende.Checked = true;
            this.rdTriLegende.Location = new System.Drawing.Point(14, 7);
            this.rdTriLegende.Name = "rdTriLegende";
            this.rdTriLegende.Size = new System.Drawing.Size(122, 17);
            this.rdTriLegende.TabIndex = 32;
            this.rdTriLegende.TabStop = true;
            this.rdTriLegende.Text = "Tri par légende";
            // 
            // rdTriValeurs
            // 
            this.rdTriValeurs.Location = new System.Drawing.Point(14, 26);
            this.rdTriValeurs.Name = "rdTriValeurs";
            this.rdTriValeurs.Size = new System.Drawing.Size(122, 17);
            this.rdTriValeurs.TabIndex = 33;
            this.rdTriValeurs.Text = "Tri par valeurs";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(6, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(249, 33);
            this.label3.TabIndex = 31;
            this.label3.Text = "Options";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // fpResultat
            // 
            this.fpResultat.AccessibleDescription = "";
            this.fpResultat.BackColor = System.Drawing.Color.PeachPuff;
            this.fpResultat.Location = new System.Drawing.Point(818, 304);
            this.fpResultat.Name = "fpResultat";
            this.fpResultat.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpResultat_Sheet1});
            this.fpResultat.Size = new System.Drawing.Size(410, 506);
            this.fpResultat.TabIndex = 34;
            this.fpResultat.SetActiveViewport(0, -1, -1);
            // 
            // fpResultat_Sheet1
            // 
            this.fpResultat_Sheet1.Reset();
            this.fpResultat_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpResultat_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            fpResultat_Sheet1.ColumnCount = 3;
            fpResultat_Sheet1.RowCount = 0;
            this.fpResultat_Sheet1.ActiveColumnIndex = -1;
            this.fpResultat_Sheet1.ActiveRowIndex = -1;
            this.fpResultat_Sheet1.ColumnHeader.Visible = false;
            this.fpResultat_Sheet1.Models = ((FarPoint.Win.Spread.SheetView.DocumentModels)(resources.GetObject("fpResultat_Sheet1.Models")));
            this.fpResultat_Sheet1.RowHeader.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(966, 49);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 37);
            this.button1.TabIndex = 35;
            this.button1.Text = "button1";
            this.button1.Visible = false;
            // 
            // oleDbDataAdapter1
            // 
            this.oleDbDataAdapter1.SelectCommand = this.oleDbSelectCommand1;
            // 
            // oleDbSelectCommand1
            // 
            this.oleDbSelectCommand1.CommandText = "SELECT FROM [FacturesEMD à imprimer]";
            this.oleDbSelectCommand1.Connection = this.oleDbConnection1;
            // 
            // oleDbConnection1
            // 
            this.oleDbConnection1.ConnectionString = resources.GetString("oleDbConnection1.ConnectionString");
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "NewDataSet";
            this.dataSet1.Locale = new System.Globalization.CultureInfo("fr-CH");
            // 
            // frmStatistiques
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.ClientSize = new System.Drawing.Size(1284, 812);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.fpResultat);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmStatistiques";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Statistiques";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.btnGraphique)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picImprTableau)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picImprGraphique)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpResultat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpResultat_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		#region Création du graphique

		private void btnGraphique_Click(object sender, System.EventArgs e)
		{
			try
			{
				fpResultat_Sheet1.RowCount = 0;
				if(panel1.BackgroundImage!=null) panel1.BackgroundImage.Dispose();
				
				// Creation de la requete en fonction des choix

				string[] filtres = new string[5];
				string strFiltre="";

                //pour les filtres
                //par médecin
                if (VariablesApplicatives.Utilisateurs.Droits == VariablesApplicatives.Utilisateurs.CodeDroits.Medecin)
				{
					ChkMedecin.Checked = true;
					cbMedecin.SelectedIndex = 0;
				}
            
				if(ChkMedecin.Checked)
				{
					if(cbMedecin.SelectedIndex>-1)
					{
						ListItem item = (ListItem)cbMedecin.SelectedItem;
						filtres[0] = " ta.CodeIntervenant  = '" + item.objValue.ToString() + "' ";
					}
				}
				
                //par Motifs
                if(chkMotif.Checked)
				{
                    filtres[1] = " ta.Motif1  = " + SosMedecins.Connexion.FormatSql.Format_String(cbMotif.Text.Replace("'", "''"));				
				}

                //par fourchette de date
				if(chkDate.Checked)
				{                    
                    filtres[2] = " ta.DAP Between " + SosMedecins.Connexion.FormatSql.Format_Date(dateTimePicker1.Value.ToString()) + " and " + SosMedecins.Connexion.FormatSql.Format_Date(dateTimePicker2.Value.ToString());                
                }

                //par origine
				if(chkOrigine.Checked)
				{
                    filtres[3] = " ta.OrigineAppel  = " + SosMedecins.Connexion.FormatSql.Format_String(cbOrigine.Text);		
				}
				
                //chois des types d'appels
                if (radioButton7.Checked)    //Toutes les consultations
                {
                    filtres[4] = " 1=1 ";
                }
                else if (radioButton9.Checked)   //consultations facturés (donc montant > 0)
				     {               
                        filtres[4]  = " cs.NConsultation in (select cs.NConsultation From tableactes ta, tableconsultations cs, factureconsultation fc, facture f"; 
                        filtres[4] += "                           where cs.CodeAppel = ta.Num"; 
						filtres[4] += "                           and cs.NConsultation = fc.NConsultation";
						filtres[4] += "                           and fc.NFacture = f.NFacture";
						filtres[4] += "                           and f.TotalFacture > 0)";
                      }
                      else if (radioButton14.Checked)  //Autre 
                            {
                                filtres[4] = " cs.NConsultation not in (select cs.NConsultation From tableactes ta, tableconsultations cs, factureconsultation fc, facture f";
                                filtres[4] += "                           where cs.CodeAppel = ta.Num";
                                filtres[4] += "                           and cs.NConsultation = fc.NConsultation";
                                filtres[4] += "                           and fc.NFacture = f.NFacture";
                                filtres[4] += "                           and f.TotalFacture > 0)";
                            }      
                            else if (radioButton13.Checked)  //Dont Conseils téléphoniques 
                                 {
                                    filtres[4] = " ta.Motif1 = 'ConsTel'";
                                    filtres[4] += " and cs.NConsultation not in (select cs.NConsultation From tableactes ta, tableconsultations cs, factureconsultation fc, facture f ";
                                    filtres[4] += "                    where cs.CodeAppel = ta.Num   ";
                                    filtres[4] += "                    and cs.NConsultation = fc.NConsultation and fc.NFacture = f.NFacture and f.TotalFacture > 0) "; 
                                 }
                                 else if (radioButton8.Checked)  //Dont Consultations annulées
                                            {
                                                filtres[4] = " ta.AnnulationAppel <> 0";
                                                filtres[4] += " and ta.Motif1 <> 'ConsTel' ";
                                                filtres[4] += " and cs.NConsultation not in (select cs.NConsultation From tableactes ta, tableconsultations cs, factureconsultation fc, facture f ";
                                                filtres[4] += "                              where cs.CodeAppel = ta.Num  ";
                                                filtres[4] += "                              and cs.NConsultation = fc.NConsultation and fc.NFacture = f.NFacture and f.TotalFacture > 0)";
                                            }
                                

				if(filtres[0]==null && filtres[1]==null && filtres[2]==null && filtres[3]==null)
				{
                    MessageBox.Show("Au moins un critère de filtre est nécessaire.", "Affichage des appels", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				if(filtres[0]==null) filtres[0] = " 1=1 ";
				if(filtres[1]==null) filtres[1] = " 1=1 ";
				if(filtres[2]==null) filtres[2] = " 1=1 ";
				if(filtres[3]==null) filtres[3] = " 1=1 ";		
				if(filtres[4]==null) filtres[4] = " 1=1 ";			

				strFiltre+=filtres[0] + "and" + filtres[1] + "and" + filtres[2] + " and" + filtres[3] + "and" + filtres[4];
            
				this.Cursor = Cursors.WaitCursor;

				DataSet ds=null;
                DataSet ds1 = null;
			
				// Choix du graphique à générer :
                //nb de consultations/tranche d'heures
				if(radioButton1.Checked)
				{
                    //En fonction du chois des types d'appels
                    if (radioButton9.Checked)   //consultations facturés (donc montant > 0)
                    {
                        ds = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet("SELECT DATENAME(hh, ta.DAP) + ':00' as 'Heure', COUNT(cs.NConsultation) as NbConsultation, DATENAME(hh, ta.DAP) + ':00' as 'Legende', AVG(dateDiff(mi,ta.DRC,ta.DSL))as delaiArrive, AVG(dateDiff(mi,ta.DSL,ta.DFI))as DureeConsult FROM tableactes ta, tableconsultations cs WHERE " + strFiltre + " and cs.CodeAppel = ta.Num GROUP BY DATENAME(hh, ta.DAP) + ':00'");
                    }
                    else   //Toutes les consultations, constel, appels annulés
                    {
                        ds = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet("SELECT DATENAME(hh, ta.DAP) + ':00' as 'Heure', COUNT(cs.NConsultation) as NbConsultation, DATENAME(hh, ta.DAP) + ':00' as 'Legende', CASE WHEN AVG(dateDiff(mi,ta.DRC,ta.DSL)) is null THEN 0 ELSE AVG(dateDiff(mi,ta.DRC,ta.DSL)) END AS delaiArrive, CASE WHEN AVG(dateDiff(mi,ta.DSL,ta.DFI)) is null THEN 0 ELSE AVG(dateDiff(mi,ta.DSL,ta.DFI)) END AS DureeConsult FROM tableactes ta, tableconsultations cs WHERE " + strFiltre + " and cs.CodeAppel = ta.Num GROUP BY DATENAME(hh, ta.DAP) + ':00'");
                    }
                    
                    if (ds != null)
                    {
                        //on formate pour le trie
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            if (ds.Tables[0].Rows[i][0].ToString() == "0:00") ds.Tables[0].Rows[i][2] = "00:00";
                            if (ds.Tables[0].Rows[i][0].ToString() == "1:00") ds.Tables[0].Rows[i][2] = "01:00";
                            if (ds.Tables[0].Rows[i][0].ToString() == "2:00") ds.Tables[0].Rows[i][2] = "02:00";
                            if (ds.Tables[0].Rows[i][0].ToString() == "3:00") ds.Tables[0].Rows[i][2] = "03:00";
                            if (ds.Tables[0].Rows[i][0].ToString() == "4:00") ds.Tables[0].Rows[i][2] = "04:00";
                            if (ds.Tables[0].Rows[i][0].ToString() == "5:00") ds.Tables[0].Rows[i][2] = "05:00";
                            if (ds.Tables[0].Rows[i][0].ToString() == "6:00") ds.Tables[0].Rows[i][2] = "06:00";
                            if (ds.Tables[0].Rows[i][0].ToString() == "7:00") ds.Tables[0].Rows[i][2] = "07:00";
                            if (ds.Tables[0].Rows[i][0].ToString() == "8:00") ds.Tables[0].Rows[i][2] = "08:00";
                            if (ds.Tables[0].Rows[i][0].ToString() == "9:00") ds.Tables[0].Rows[i][2] = "09:00";
                        }
                    }

                    if (ds != null)
                        CreationGraphique(ds, "Nombre de consultations par tranche d'heures");
                    else MessageBox.Show("Aucune donnée à Afficher");
				}

                //nb consult/Jour
				if(radioButton2.Checked)
				{

                    //En fonction du chois des types d'appels
                    if (radioButton9.Checked)   //Appels facturés
                    {
                        ds = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet("SELECT DAY(ta.DAP) as Jour, COUNT(cs.NConsultation) as NbConsultation, MAX(CONVERT(nvarchar,ta.DAP,103)) as Legende, AVG(dateDiff(mi,ta.DRC,ta.DSL))as delaiArrive, AVG(dateDiff(mi,ta.DSL,ta.DFI))as DureeConsult FROM tableactes ta, tableconsultations cs WHERE " + strFiltre + " and cs.CodeAppel = ta.Num GROUP BY DAY(ta.DAP), MONTH(ta.DAP) ORDER BY MONTH(ta.DAP), DAY(ta.DAP)");
                    }
                    else   //Tous les appels, constel, appels annulés
                    {
                        ds = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet("SELECT DAY(ta.DAP) as Jour, COUNT(cs.NConsultation) as NbConsultation, MAX(CONVERT(nvarchar,ta.DAP,103)) as Legende, CASE WHEN AVG(dateDiff(mi,ta.DRC,ta.DSL)) is null THEN 0 ELSE AVG(dateDiff(mi,ta.DRC,ta.DSL)) END AS delaiArrive, CASE WHEN AVG(dateDiff(mi,ta.DSL,ta.DFI)) is null THEN 0 ELSE AVG(dateDiff(mi,ta.DSL,ta.DFI)) END AS DureeConsult FROM tableactes ta, tableconsultations cs WHERE " + strFiltre + " and cs.CodeAppel = ta.Num GROUP BY DAY(ta.DAP), MONTH(ta.DAP) ORDER BY MONTH(ta.DAP), DAY(ta.DAP)");
                    }

                    if (ds != null)
                        CreationGraphique(ds, "Nombre de consultations par Jour");
                    else MessageBox.Show("Aucune donnée à Afficher");

				}

                //nb de consultations/médecin
				if(radioButton3.Checked)
				{
                    //En fonction du chois des types d'appels
                    if (radioButton9.Checked)   //Appels facturés
                    {
                        if (ChkMedecin.Checked)  //si on choisi un médecin en particuler
                        {
                            ds = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet("SELECT MONTH(ta.DAP) as 'Mois', COUNT(cs.NConsultation) as NbConsultation, YEAR(ta.DAP) as 'Legende', YEAR(ta.DAP) as 'an' ,AVG(dateDiff(mi,ta.DRC,ta.DSL))as delaiArrive, AVG(dateDiff(mi,ta.DSL,ta.DFI))as DureeConsult FROM tableactes ta, tablemedecin m, tableconsultations cs WHERE " + strFiltre + " and cs.CodeAppel = ta.Num AND m.CodeIntervenant = ta.CodeIntervenant AND ta.CodeIntervenant is not null AND ta.CodeIntervenant<>0 GROUP BY YEAR(ta.DAP), MONTH(ta.DAP), ta.CodeIntervenant ORDER BY YEAR(ta.DAP), MONTH(ta.DAP)");
                        }
                        else   //sinon pour tous les médecins
                        {
                            ds = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet("SELECT YEAR(ta.DAP) as 'an', COUNT(cs.NConsultation) as NbConsultation, m.Nom as 'Legende', AVG(dateDiff(mi,ta.DRC,ta.DSL))as delaiArrive, AVG(dateDiff(mi,ta.DSL,ta.DFI))as DureeConsult FROM tableactes ta, tablemedecin m, tableconsultations cs WHERE " + strFiltre + " and cs.CodeAppel = ta.Num AND m.CodeIntervenant = ta.CodeIntervenant AND ta.CodeIntervenant is not null AND ta.CodeIntervenant<>0 GROUP BY YEAR(ta.DAP), MONTH(ta.DAP), m.Nom, ta.CodeIntervenant ORDER BY YEAR(ta.DAP), MONTH(ta.DAP), m.Nom");
                        }
                    }
                    else   //Tous les appels, constel, appels annulés
                    {
                        if (ChkMedecin.Checked)  //si on choisi un médecin en particuler
                        {
                            ds = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet("SELECT MONTH(ta.DAP) as 'Mois', COUNT(cs.NConsultation) as NbConsultation, YEAR(ta.DAP) as 'Legende', YEAR(ta.DAP) as 'an', AVG(dateDiff(mi,ta.DRC,ta.DSL))as delaiArrive, AVG(dateDiff(mi,ta.DSL,ta.DFI))as DureeConsult FROM tableactes ta, tablemedecin m, tableconsultations cs WHERE " + strFiltre + "and cs.CodeAppel = ta.Num AND m.CodeIntervenant = ta.CodeIntervenant AND ta.CodeIntervenant is not null AND ta.CodeIntervenant<>0 GROUP BY YEAR(ta.DAP), MONTH(ta.DAP), ta.CodeIntervenant ORDER BY YEAR(ta.DAP), MONTH(ta.DAP)");
                        }
                        else   //sinon pour tous les médecins
                        {
                            ds = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet("SELECT YEAR(ta.DAP) as 'an', COUNT(cs.NConsultation) as NbConsultation, m.Nom as 'Legende', AVG(dateDiff(mi,ta.DRC,ta.DSL))as delaiArrive, AVG(dateDiff(mi,ta.DSL,ta.DFI))as DureeConsult FROM tableactes ta, tablemedecin m, tableconsultations cs WHERE " + strFiltre + " and cs.CodeAppel = ta.Num AND m.CodeIntervenant = ta.CodeIntervenant AND ta.CodeIntervenant is not null AND ta.CodeIntervenant<>0 GROUP BY YEAR(ta.DAP), ta.CodeIntervenant, m.Nom ORDER BY YEAR(ta.DAP), m.Nom");
                        }
                    }


                    if (ChkMedecin.Checked)  //si on choisi un médecin en particuler
                    {
                        //on arrange le libellé 
                        if (ds != null)
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                               
                                if (ds.Tables[0].Rows[i][0].ToString() == "1") ds.Tables[0].Rows[i][2] = ds.Tables[0].Rows[i][2].ToString() + "-01";
                                if (ds.Tables[0].Rows[i][0].ToString() == "2") ds.Tables[0].Rows[i][2] = ds.Tables[0].Rows[i][2].ToString() + "-02";
                                if (ds.Tables[0].Rows[i][0].ToString() == "3") ds.Tables[0].Rows[i][2] = ds.Tables[0].Rows[i][2].ToString() + "-03";
                                if (ds.Tables[0].Rows[i][0].ToString() == "4") ds.Tables[0].Rows[i][2] = ds.Tables[0].Rows[i][2].ToString() + "-04";
                                if (ds.Tables[0].Rows[i][0].ToString() == "5") ds.Tables[0].Rows[i][2] = ds.Tables[0].Rows[i][2].ToString() + "-05";
                                if (ds.Tables[0].Rows[i][0].ToString() == "6") ds.Tables[0].Rows[i][2] = ds.Tables[0].Rows[i][2].ToString() + "-06";
                                if (ds.Tables[0].Rows[i][0].ToString() == "7") ds.Tables[0].Rows[i][2] = ds.Tables[0].Rows[i][2].ToString() + "-07";
                                if (ds.Tables[0].Rows[i][0].ToString() == "8") ds.Tables[0].Rows[i][2] = ds.Tables[0].Rows[i][2].ToString() + "-08";
                                if (ds.Tables[0].Rows[i][0].ToString() == "9") ds.Tables[0].Rows[i][2] = ds.Tables[0].Rows[i][2].ToString() + "-09";
                                if (ds.Tables[0].Rows[i][0].ToString() == "10") ds.Tables[0].Rows[i][2] = ds.Tables[0].Rows[i][2].ToString() + "-10";
                                if (ds.Tables[0].Rows[i][0].ToString() == "11") ds.Tables[0].Rows[i][2] = ds.Tables[0].Rows[i][2].ToString() + "-11";
                                if (ds.Tables[0].Rows[i][0].ToString() == "12") ds.Tables[0].Rows[i][2] = ds.Tables[0].Rows[i][2].ToString() + "-12";
                            }
                        }
                    }
                    else
                    {
                           
                         //on arrange le libellé 
                        if (ds != null)
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                //on rajoute l'année au nom du médecin
                                ds.Tables[0].Rows[i][2] += "-" + ds.Tables[0].Rows[i][0].ToString();
                            }
                        }
                    }

                    if (ds != null)
                        CreationGraphique(ds, "Nombre de consultations par médecin");
                    else MessageBox.Show("Aucune donnée à Afficher");
				}

                //nb consultations/Origine
				if(radioButton4.Checked)
				{

                    //En fonction du chois des types d'appels
                    if (radioButton9.Checked)   //Appels facturés
                    {
                        ds = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet("SELECT MONTH(ta.DAP) as 'Mois', COUNT(cs.NConsultation) as NbConsultation, OrigineAppel as Legende, AVG(dateDiff(mi,ta.DRC,ta.DSL))as delaiArrive, AVG(dateDiff(mi,ta.DSL,ta.DFI))as DureeConsult FROM tableactes ta, tableconsultations cs WHERE " + strFiltre + " and cs.CodeAppel = ta.Num GROUP BY MONTH(ta.DAP) ORDER BY MONTH(ta.DAP)");
                    }
                    else   //Tous les appels, constel, appels annulés
                    {
                        ds = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet("SELECT MONTH(ta.DAP) as 'Mois', COUNT(cs.NConsultation) as NbConsultation, OrigineAppel as Legende, AVG(dateDiff(mi,ta.DRC,ta.DSL))as delaiArrive, AVG(dateDiff(mi,ta.DSL,ta.DFI))as DureeConsult FROM tableactes ta, tableconsultations cs WHERE " + strFiltre + " and cs.CodeAppel = ta.Num AND OrigineAppel is not null AND OrigineAppel<>'' GROUP BY OrigineAppel, MONTH(ta.DAP) ORDER BY OrigineAppel");
                    }
                    if (ds != null)
                        CreationGraphique(ds, "Nombre de consultations par Origine");
                    else MessageBox.Show("Aucune donnée à Afficher");
				}

                //nb appels/Motif     -----> Non affiché pour le momment
				if(radioButton5.Checked)
				{

                    //En fonction du chois des types d'appels
                    if (radioButton9.Checked)   //Appels facturés
                    {
                        ds = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet("SELECT MONTH(ta.DAP) as 'Mois', COUNT(cs.NConsultation) as NbConsultation, ta.Motif1 as Legende, AVG(dateDiff(mi,ta.DRC,ta.DSL))as delaiArrive, AVG(dateDiff(mi,ta.DSL,ta.DFI))as DureeConsult FROM tableactes ta, tableconsultations cs WHERE " + strFiltre + " and cs.CodeAppel = ta.Num GROUP BY MONTH(ta.DAP), ta.Motif1 ORDER BY ta.Motif1");
                    }
                    else   //Tous les appels, constel, appels annulés
                    {
                        ds = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet("SELECT YEAR(ta.DAP) as 'an', COUNT(cs.NConsultation) as NbConsultation, ta.Motif1 as Legende, AVG(dateDiff(mi,ta.DRC,ta.DSL))as delaiArrive, AVG(dateDiff(mi,ta.DSL,ta.DFI))as DureeConsult FROM tableactes ta, tableconsultations cs WHERE " + strFiltre + " and cs.CodeAppel = ta.Num AND ta.Motif1 is not null AND ta.Motif1 <> '' GROUP BY YEAR(ta.DAP), ta.Motif1 ORDER BY ta.Motif1");
                    }
                    if (ds != null)
                        CreationGraphique(ds, "Nombre d'appels par Motif");
                    else MessageBox.Show("Aucune donnée à Afficher");
                    
                    
                    //ds = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet("SELECT left(Motif1,0), COUNT(Motif1) as NbAppel, Motif1 as Legende, AVG(dateDiff(mi,a.DRC,a.DSL))as delaiArrive, AVG(dateDiff(mi,a.DSL,a.DFI))as DureeConsult FROM tableactes a WHERE " + strFiltre + " AND Motif1 is not null AND Motif1<>'' GROUP BY Motif1 ORDER BY Motif1");
					//CreationGraphique(ds,"Nombre d'appels par Motif");
				}

                //nb appels/Motif d'annulation   -----> Non affiché pour le moment
				if(radioButton6.Checked)
				{
                    /*ds = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet("SELECT left(MotifAnnulation,0) , COUNT(MotifAnnulation) as 'NbAppel',MotifAnnulation as 'Legende', AVG(dateDiff(mi,a.DRC,a.DSL))as delaiArrive, AVG(dateDiff(mi,a.DSL,a.DFI))as DureeConsult FROM tableactes a WHERE " + strFiltre + " AND MotifAnnulation is not null AND MotifAnnulation<>''  GROUP BY MotifAnnulation ORDER BY MotifAnnulation");
					CreationGraphique(ds,"Nombre d'appels par Motif d'annulation");*/

                  /*  sqlstr = "select nom ,prenom , tableactes.DAP";
                    sqlstr += " from   tablepersonne ";
                    sqlstr += " Inner join  tablepatient";
                    sqlstr += " ON tablepersonne.IdPersonne=tablepatient.IdPersonne";
                    sqlstr += " Inner join tableactes";
                    sqlstr += " on tablepatient.IdPatient=tableactes.indicePatient";
                    sqlstr += " Inner join tableconsultations";
                    sqlstr += " on tableactes.IndicePatient=tableconsultations.IndicePatient";
                    sqlstr += " WHERE  tableactes.DAP  BETWEEN ";
                    //changer -6 par le nombre de jour désiré
                    sqlstr += " convert(varchar,GetDate()-6, 100) AND convert(varchar, GetDate(), 100)";
                    sqlstr += sqlstr1;
                    sqlstr += "GROUP BY nom,prenom,tableactes.DAP";  */


				}
				
                //nb de consultations/mois
                if(radioButton12.Checked)
				{
                    //En fonction du chois des types d'appels
                    if (radioButton9.Checked)   //Appels facturés
                    {
                        ds = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet("SELECT MONTH(ta.DAP) as 'Mois', COUNT(cs.NConsultation) as NbConsultation, YEAR(ta.DAP) as 'Legende', YEAR(ta.DAP) as 'an', AVG(dateDiff(mi,ta.DRC,ta.DSL))as 'delaiArrive', AVG(dateDiff(mi,ta.DSL,ta.DFI))as 'DureeConsult' FROM tableactes ta, tableconsultations cs WHERE  " + strFiltre + " and cs.CodeAppel = ta.Num GROUP BY YEAR(ta.DAP), MONTH(ta.DAP)");
                    }
                    else   //Tous les appels, constel, appels annulés
                    {
                        ds = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet("SELECT MONTH(ta.DAP) as 'Mois', COUNT(cs.NConsultation) as NbConsultation, YEAR(ta.DAP) as 'Legende', YEAR(ta.DAP) as 'an', AVG(dateDiff(mi,ta.DRC,ta.DSL))as 'delaiArrive', AVG(dateDiff(mi,ta.DSL,ta.DFI))as 'DureeConsult' FROM tableactes ta, tableconsultations cs WHERE  " + strFiltre + " and cs.CodeAppel = ta.Num GROUP BY YEAR(ta.DAP), MONTH(ta.DAP)");
                    }
                    
                   
                   //on arrange le libellé 
                   if(ds!=null)
					{
                        for(int i=0;i<ds.Tables[0].Rows.Count;i++)
                        {   
                            if (ds.Tables[0].Rows[i][0].ToString() =="1") ds.Tables[0].Rows[i][2] = ds.Tables[0].Rows[i][2].ToString() + "-01";
                            if (ds.Tables[0].Rows[i][0].ToString() == "2") ds.Tables[0].Rows[i][2] = ds.Tables[0].Rows[i][2].ToString() + "-02";
                            if (ds.Tables[0].Rows[i][0].ToString() == "3") ds.Tables[0].Rows[i][2] = ds.Tables[0].Rows[i][2].ToString() + "-03";
                            if (ds.Tables[0].Rows[i][0].ToString() == "4") ds.Tables[0].Rows[i][2] = ds.Tables[0].Rows[i][2].ToString() + "-04";
                            if (ds.Tables[0].Rows[i][0].ToString() == "5") ds.Tables[0].Rows[i][2] = ds.Tables[0].Rows[i][2].ToString() + "-05";
                            if (ds.Tables[0].Rows[i][0].ToString() == "6") ds.Tables[0].Rows[i][2] = ds.Tables[0].Rows[i][2].ToString() + "-06";
                            if (ds.Tables[0].Rows[i][0].ToString() == "7") ds.Tables[0].Rows[i][2] = ds.Tables[0].Rows[i][2].ToString() + "-07";
                            if (ds.Tables[0].Rows[i][0].ToString() == "8") ds.Tables[0].Rows[i][2] = ds.Tables[0].Rows[i][2].ToString() + "-08";
                            if (ds.Tables[0].Rows[i][0].ToString() == "9") ds.Tables[0].Rows[i][2] = ds.Tables[0].Rows[i][2].ToString() + "-09";
                            if (ds.Tables[0].Rows[i][0].ToString() == "10") ds.Tables[0].Rows[i][2] = ds.Tables[0].Rows[i][2].ToString() + "-10";
                            if (ds.Tables[0].Rows[i][0].ToString() == "11") ds.Tables[0].Rows[i][2] = ds.Tables[0].Rows[i][2].ToString() + "-11";
                            if (ds.Tables[0].Rows[i][0].ToString() == "12") ds.Tables[0].Rows[i][2] = ds.Tables[0].Rows[i][2].ToString() + "-12";
                        }
                    }
					CreationGraphique(ds,"Nombre de consultations par mois pour l'année " + ds.Tables[0].Rows[0]["an"].ToString());
					
				}

                //****Domi 24.02.2012
                //Calcul délais d'attente/patient
				if(radioButton11.Checked)
				{
                    ds = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet("SELECT pe.idpersonne, AVG(dateDiff(mi,ta.DAP,ta.DSL))as PourValMax, (pe.Nom + '....' + tm.NomGeneve) as 'Legende', AVG(dateDiff(mi,ta.DAP,ta.DSL))as delaiArrive, AVG(dateDiff(mi,ta.DSL,ta.DFI))as DureeConsult FROM tableactes ta, tablepatient pa, tablepersonne pe, tablemedecin tm, tableconsultations cs WHERE pa.IdPatient = ta.IndicePatient and pe.IdPersonne = pa.IdPersonne and ta.codeIntervenant = tm.codeIntervenant and cs.CodeAppel = ta.Num and " + strFiltre + " and ta.DSL is not null and ta.DRC is not null GROUP BY pe.idpersonne,pe.nom, tm.NomGeneve ORDER BY pe.nom");

                    //Pour le titre et moy general
                    ds1 = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet("SELECT AVG(dateDiff(mi,ta.DAP,ta.DSL))as delaiArrive, AVG(dateDiff(mi,ta.DSL,ta.DFI))as DureeConsult FROM tableactes ta, tablemedecin tm, tableconsultations cs WHERE ta.codeIntervenant = tm.codeIntervenant and cs.CodeAppel = ta.Num and ta.Motif1 <> 'ConsTel' and " + strFiltre + " and ta.DSL is not null and ta.DRC is not null");
                    CreationGraphique(ds, "Délais d'attente/Patient: Moy: " + ds1.Tables[0].Rows[0][0].ToString() + "', Moy/consult: " + ds1.Tables[0].Rows[0][1].ToString() + "'");					
				}
               

                //****Domi 21.02.2012
                //Calcul délais de trajet/patient
                if (radioButton15.Checked)
                {
                    ds = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet("SELECT pe.idpersonne, AVG(dateDiff(mi,ta.DRC,ta.DSL))as PourValMax, (pe.Nom + '....' + tm.NomGeneve) as 'Legende', AVG(dateDiff(mi,ta.DRC,ta.DSL))as delaiArrive, AVG(dateDiff(mi,ta.DSL,ta.DFI))as DureeConsult FROM tableactes ta, tablepatient pa, tablepersonne pe, tablemedecin tm, tableconsultations cs WHERE pa.IdPatient = ta.IndicePatient and pe.IdPersonne = pa.IdPersonne and ta.codeIntervenant = tm.codeIntervenant and cs.CodeAppel = ta.Num and " + strFiltre + " and ta.DSL is not null and ta.DRC is not null GROUP BY pe.idpersonne,pe.nom, tm.NomGeneve ORDER BY pe.nom");

                    //Pour le titre et moy general
                    ds1 = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet("SELECT AVG(dateDiff(mi,ta.DRC,ta.DSL))as delaiArrive, AVG(dateDiff(mi,ta.DSL,ta.DFI))as DureeConsult FROM tableactes ta, tablemedecin tm, tableconsultations cs WHERE ta.codeIntervenant = tm.codeIntervenant and cs.CodeAppel = ta.Num and ta.Motif1 <> 'ConsTel' and " + strFiltre + " and ta.DSL is not null and ta.DRC is not null");
                    CreationGraphique(ds, "Durée de trajet/Patient: Moy: " + ds1.Tables[0].Rows[0][0].ToString() + "', Moy/consult: " + ds1.Tables[0].Rows[0][1].ToString() + "'");
                }


                //nb appels/tranche d'age       -----> Non affiché pour le momment
				if(radioButton10.Checked)
				{
                             ds = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet("SELECT ' ', COUNT(*) as 'NbAppel','< 1an' as 'Legende',AVG(dateDiff(mi,a.DRC,a.DSL))as delaiArrive, AVG(dateDiff(mi,a.DSL,a.DFI))as DureeConsult FROM tableactes a inner join tableconsultations c on c.CodeAppel = a.Num inner join tablepatient pa on pa.IdPatient = c.IndicePatient inner join tablepersonne pe on pe.IdPersonne = pa.IdPersonne WHERE " + strFiltre + " AND (pe.UniteAge='M' or UniteAge='S')");
                    DataSet ds2 = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet("SELECT ' ', COUNT(*) as 'NbAppel','1 - 9 ans' as 'Legende',AVG(dateDiff(mi,a.DRC,a.DSL))as delaiArrive, AVG(dateDiff(mi,a.DSL,a.DFI))as DureeConsult FROM tableactes a inner join tableconsultations c on c.CodeAppel = a.Num inner join tablepatient pa on pa.IdPatient = c.IndicePatient inner join tablepersonne pe on pe.IdPersonne = pa.IdPersonne WHERE " + strFiltre + " AND (UniteAge='A' AND Age>=1 AND Age<10)");
                    DataSet ds3 = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet("SELECT ' ', COUNT(*) as 'NbAppel','10 - 19 ans' as 'Legende',AVG(dateDiff(mi,a.DRC,a.DSL))as delaiArrive, AVG(dateDiff(mi,a.DSL,a.DFI))as DureeConsult FROM tableactes a inner join tableconsultations c on c.CodeAppel = a.Num inner join tablepatient pa on pa.IdPatient = c.IndicePatient inner join tablepersonne pe on pe.IdPersonne = pa.IdPersonne WHERE " + strFiltre + " AND (UniteAge='A' AND Age>=10 AND Age<20)");
                    DataSet ds4 = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet("SELECT ' ', COUNT(*) as 'NbAppel','20 - 29 ans' as 'Legende',AVG(dateDiff(mi,a.DRC,a.DSL))as delaiArrive, AVG(dateDiff(mi,a.DSL,a.DFI))as DureeConsult FROM tableactes a inner join tableconsultations c on c.CodeAppel = a.Num inner join tablepatient pa on pa.IdPatient = c.IndicePatient inner join tablepersonne pe on pe.IdPersonne = pa.IdPersonne WHERE " + strFiltre + " AND (UniteAge='A' AND Age>=20 AND Age<30)");
                    DataSet ds5 = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet("SELECT ' ', COUNT(*) as 'NbAppel','30 - 39 ans' as 'Legende',AVG(dateDiff(mi,a.DRC,a.DSL))as delaiArrive, AVG(dateDiff(mi,a.DSL,a.DFI))as DureeConsult FROM tableactes a inner join tableconsultations c on c.CodeAppel = a.Num inner join tablepatient pa on pa.IdPatient = c.IndicePatient inner join tablepersonne pe on pe.IdPersonne = pa.IdPersonne WHERE " + strFiltre + " AND (UniteAge='A' AND Age>=30 AND Age<40)");
                    DataSet ds6 = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet("SELECT ' ', COUNT(*) as 'NbAppel','40 - 49 ans' as 'Legende',AVG(dateDiff(mi,a.DRC,a.DSL))as delaiArrive, AVG(dateDiff(mi,a.DSL,a.DFI))as DureeConsult FROM tableactes a inner join tableconsultations c on c.CodeAppel = a.Num inner join tablepatient pa on pa.IdPatient = c.IndicePatient inner join tablepersonne pe on pe.IdPersonne = pa.IdPersonne WHERE " + strFiltre + " AND (UniteAge='A' AND Age>=40 AND Age<50)");
                    DataSet ds7 = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet("SELECT ' ', COUNT(*) as 'NbAppel','50 - 59 ans' as 'Legende',AVG(dateDiff(mi,a.DRC,a.DSL))as delaiArrive, AVG(dateDiff(mi,a.DSL,a.DFI))as DureeConsult FROM tableactes a inner join tableconsultations c on c.CodeAppel = a.Num inner join tablepatient pa on pa.IdPatient = c.IndicePatient inner join tablepersonne pe on pe.IdPersonne = pa.IdPersonne WHERE " + strFiltre + " AND (UniteAge='A' AND Age>=50 AND Age<60)");
                    DataSet ds8 = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet("SELECT ' ', COUNT(*) as 'NbAppel','60 - 69 ans' as 'Legende',AVG(dateDiff(mi,a.DRC,a.DSL))as delaiArrive, AVG(dateDiff(mi,a.DSL,a.DFI))as DureeConsult FROM tableactes a inner join tableconsultations c on c.CodeAppel = a.Num inner join tablepatient pa on pa.IdPatient = c.IndicePatient inner join tablepersonne pe on pe.IdPersonne = pa.IdPersonne WHERE " + strFiltre + " AND (UniteAge='A' AND Age>=60 AND Age<70)");
                    DataSet ds9 = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet("SELECT ' ', COUNT(*) as 'NbAppel','70 - 79 ans' as 'Legende',AVG(dateDiff(mi,a.DRC,a.DSL))as delaiArrive, AVG(dateDiff(mi,a.DSL,a.DFI))as DureeConsult FROM tableactes a inner join tableconsultations c on c.CodeAppel = a.Num inner join tablepatient pa on pa.IdPatient = c.IndicePatient inner join tablepersonne pe on pe.IdPersonne = pa.IdPersonne WHERE " + strFiltre + " AND (UniteAge='A' AND Age>=70 AND Age<80)");
                    DataSet ds10 = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet("SELECT ' ', COUNT(*) as 'NbAppel','80 - 89 ans' as 'Legende',AVG(dateDiff(mi,a.DRC,a.DSL))as delaiArrive, AVG(dateDiff(mi,a.DSL,a.DFI))as DureeConsult FROM tableactes a inner join tableconsultations c on c.CodeAppel = a.Num inner join tablepatient pa on pa.IdPatient = c.IndicePatient inner join tablepersonne pe on pe.IdPersonne = pa.IdPersonne WHERE " + strFiltre + " AND (UniteAge='A' AND Age>=80 AND Age<90)");
                    DataSet ds11 = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet("SELECT ' ', COUNT(*) as 'NbAppel','90 - 99 ans' as 'Legende',AVG(dateDiff(mi,a.DRC,a.DSL))as delaiArrive, AVG(dateDiff(mi,a.DSL,a.DFI))as DureeConsult FROM tableactes a inner join tableconsultations c on c.CodeAppel = a.Num inner join tablepatient pa on pa.IdPatient = c.IndicePatient inner join tablepersonne pe on pe.IdPersonne = pa.IdPersonne WHERE " + strFiltre + " AND (UniteAge='A' AND Age>=90 AND Age<100)");
                    DataSet ds12 = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet("SELECT ' ', COUNT(*) as 'NbAppel','100 ans et +' as 'Legende',AVG(dateDiff(mi,a.DRC,a.DSL))as delaiArrive, AVG(dateDiff(mi,a.DSL,a.DFI))as DureeConsult FROM tableactes a inner join tableconsultations c on c.CodeAppel = a.Num inner join tablepatient pa on pa.IdPatient = c.IndicePatient inner join tablepersonne pe on pe.IdPersonne = pa.IdPersonne WHERE " + strFiltre + " AND (UniteAge='A' AND Age>=100)");
					
					DataRow row2 = ds.Tables[0].NewRow();
					row2.ItemArray = ds2.Tables[0].Rows[0].ItemArray;
					ds.Tables[0].Rows.Add(row2);

					DataRow row3 = ds.Tables[0].NewRow();
					row3.ItemArray = ds3.Tables[0].Rows[0].ItemArray;
					ds.Tables[0].Rows.Add(row3);

					DataRow row4 = ds.Tables[0].NewRow();
					row4.ItemArray = ds4.Tables[0].Rows[0].ItemArray;
					ds.Tables[0].Rows.Add(row4);

					DataRow row5 = ds.Tables[0].NewRow();
					row5.ItemArray = ds5.Tables[0].Rows[0].ItemArray;
					ds.Tables[0].Rows.Add(row5);

					DataRow row6 = ds.Tables[0].NewRow();
					row6.ItemArray = ds6.Tables[0].Rows[0].ItemArray;
					ds.Tables[0].Rows.Add(row6);

					DataRow row7 = ds.Tables[0].NewRow();
					row7.ItemArray = ds7.Tables[0].Rows[0].ItemArray;
					ds.Tables[0].Rows.Add(row7);

					DataRow row8 = ds.Tables[0].NewRow();
					row8.ItemArray = ds8.Tables[0].Rows[0].ItemArray;
					ds.Tables[0].Rows.Add(row8);

					DataRow row9 = ds.Tables[0].NewRow();
					row9.ItemArray = ds9.Tables[0].Rows[0].ItemArray;
					ds.Tables[0].Rows.Add(row9);

					DataRow row10 = ds.Tables[0].NewRow();
					row10.ItemArray = ds10.Tables[0].Rows[0].ItemArray;
					ds.Tables[0].Rows.Add(row10);

					DataRow row11 = ds.Tables[0].NewRow();
					row11.ItemArray = ds11.Tables[0].Rows[0].ItemArray;
					ds.Tables[0].Rows.Add(row11);

					DataRow row12 = ds.Tables[0].NewRow();
					row12.ItemArray = ds12.Tables[0].Rows[0].ItemArray;
					ds.Tables[0].Rows.Add(row12);

					CreationGraphique(ds,"Nombre d'appels par tranche d'âge");
					
				}
				this.Cursor = Cursors.Default;
			}
			catch(Exception ex)
			{
				this.Cursor = Cursors.Default;
				MessageBox.Show(ex.Message);
			}
		}	

		private void CreationGraphiqueVierge()
		{
			Bitmap map = new Bitmap(panel1.Width,panel1.Height,System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			Graphics g = Graphics.FromImage(map);
			g.Clear(Color.White);
			panel1.BackgroundImage = map;
		}

		private void CreationGraphique(DataSet ds, string titre)
		{
			// Filtre sur les différentes valeurs?

			bool FiltreSurValeur = true;
			if(txtValeur.Text=="") FiltreSurValeur = false;
			else
			{
				foreach(char c in txtValeur.Text)
					if(!char.IsDigit(c))
						FiltreSurValeur = false;
			}
			if(!FiltreSurValeur || cbFiltreValeur.Text=="(tous)" || cbFiltreValeur.Text=="")
				FiltreSurValeur=false;
			else
			{
				FiltreSurValeur = true;
			}

			//En fonction des filtres:
            string filtre = "";
			string sort="";
			if(FiltreSurValeur)
				filtre = "NbAppel" + cbFiltreValeur.Text + txtValeur.Text;
			
            
            if(rdTriLegende.Checked)
            {   //si c'est le nb appel/mois
                if (radioButton12.Checked)
                    sort = "an, Legende ";

                if (radioButton3.Checked)
                    if (ChkMedecin.Checked)
                        sort = "an, Legende ";
                    else sort = "Legende ";

                if (radioButton1.Checked)
                         sort = "Legende ";
                    else sort = "Legende ";
            }
 

            //si c'est un tri/valeur
			if(rdTriValeurs.Checked)
				sort="NbAppel ";
            
            //si on veut on tri ascendant
			if(rdTriAsc.Checked)
				sort+="ASC";

            //si on veut on tri descendant
            if(rdTriDesc.Checked)
				sort+="DESC";
            
            //Sans tri
			if(rdSansTri.Checked)
				sort="";
			
			DataRow[] rows = ds.Tables[0].Select(filtre,sort);	
		
			

			String Filename = Application.StartupPath + "\\" + "Graph" + DateTime.Now.Second + DateTime.Now.Millisecond + ".png";
			if(rows!=null && rows.Length>0)
			{
				
                for(int i=0;i<rows.Length;i++)
				{
                    int nb = fpResultat_Sheet1.RowCount++;
					if(nb%2==0)
						fpResultat_Sheet1.Rows[nb].CellType = m_Gradient1;
					else
						fpResultat_Sheet1.Rows[nb].CellType = m_Gradient2;

                    //en fonction des stats demandés on determine les champs de requette a afficher
                    //nb appel / heure
                    if (radioButton1.Checked)
                    {
                        fpResultat_Sheet1.Cells[nb, 0].Text = rows[i][2].ToString();
                        fpResultat_Sheet1.Cells[nb, 1].Text = rows[i][1].ToString();
                        fpResultat_Sheet1.Cells[nb, 2].Text = rows[i][0].ToString();
                    }
                    //nb appel / jour
                    if (radioButton2.Checked)
                    {
                        fpResultat_Sheet1.Cells[nb, 0].Text = rows[i][2].ToString();
                        fpResultat_Sheet1.Cells[nb, 1].Text = rows[i][1].ToString();
                        fpResultat_Sheet1.Cells[nb, 2].Text = rows[i][0].ToString();
                    }
                    //nb appel / médecins
                    if (radioButton3.Checked)
                    {
                        fpResultat_Sheet1.Cells[nb, 0].Text = rows[i][0].ToString();
                        fpResultat_Sheet1.Cells[nb, 1].Text = rows[i][1].ToString();
                        fpResultat_Sheet1.Cells[nb, 2].Text = rows[i][2].ToString();
                    }
                    //nb appel / origine
                    if (radioButton4.Checked)
                    {
                        fpResultat_Sheet1.Cells[nb, 0].Text = rows[i][0].ToString();
                        fpResultat_Sheet1.Cells[nb, 1].Text = rows[i][2].ToString();
                        fpResultat_Sheet1.Cells[nb, 2].Text = rows[i][1].ToString();
                    }
                    //nb appel / motif
                    if (radioButton5.Checked)
                    {
                        fpResultat_Sheet1.Cells[nb, 0].Text = rows[i][2].ToString();
                        fpResultat_Sheet1.Cells[nb, 1].Text = rows[i][1].ToString();
                        fpResultat_Sheet1.Cells[nb, 2].Text = rows[i][0].ToString();
                    }
                    //nb appel / annule par       -----> Non affiché pour le momment
                    if (radioButton6.Checked)
                    {
                        fpResultat_Sheet1.Cells[nb, 0].Text = rows[i][0].ToString();
                        fpResultat_Sheet1.Cells[nb, 1].Text = rows[i][2].ToString();
                        fpResultat_Sheet1.Cells[nb, 2].Text = rows[i][1].ToString();
                    }
                    //nb appel / age        -----> Non affiché pour le momment
                    if (radioButton10.Checked)
                    {
                        fpResultat_Sheet1.Cells[nb, 0].Text = rows[i][0].ToString();
                        fpResultat_Sheet1.Cells[nb, 1].Text = rows[i][2].ToString();
                        fpResultat_Sheet1.Cells[nb, 2].Text = rows[i][1].ToString();
                    }
                    //délais d'Attente/patient
                    if (radioButton11.Checked)
                    {
                        fpResultat_Sheet1.Cells[nb, 0].Text = rows[i][2].ToString();  //x
                        fpResultat_Sheet1.Cells[nb, 1].Text = rows[i][0].ToString();  //Libellé
                        fpResultat_Sheet1.Cells[nb, 2].Text = rows[i][3].ToString();  //Y
                    }
                    //nb appel / mois
                    if (radioButton12.Checked)
                    {
                        fpResultat_Sheet1.Cells[nb, 0].Text = rows[i][2].ToString();
                        fpResultat_Sheet1.Cells[nb, 1].Text = rows[i][1].ToString();
                        fpResultat_Sheet1.Cells[nb, 2].Text = rows[i][3].ToString();
                    }

                    //délais de trajet/patient
                    if (radioButton15.Checked)
                    {
                        fpResultat_Sheet1.Cells[nb, 0].Text = rows[i][2].ToString();  //x
                        fpResultat_Sheet1.Cells[nb, 1].Text = rows[i][0].ToString();  //Libellé
                        fpResultat_Sheet1.Cells[nb, 2].Text = rows[i][3].ToString();  //Y
                    }   


				}

				Graphique ret=null;
				
				Stats.AffichageLegende = chkLegende.Checked;
				Stats.AffichageValeurs = chkValeurs.Checked;
				Stats.TitreGraphique = titre;

				if(rdBaton.Checked)
				{
					ret= Stats.CreationGraphiqueBaton(panel1.Size,rows);
				}
				else if(rdBarre.Checked)
				{
					ret= Stats.CreationGraphiqueBarre(panel1.Size,rows);
				}
				else if(rdPoints.Checked)
				{
					ret= Stats.CreationGraphiquePoint(panel1.Size,rows);
				}
				else if(rdCourbe.Checked)
				{
					ret= Stats.CreationGraphiqueCourbe(panel1.Size,rows);
				}
				else if(rdSecteur.Checked)
				{
					ret= Stats.CreationGraphiqueSecteur(panel1.Size,rows);
				}

				if(ret==null)
				{
                    MessageBox.Show("Statistiques en erreur.", "Statistiques", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				else
					panel1.Tag = ret;
				panel1.BackgroundImage = ret.Img;
			}		
			else
			{	
				CreationGraphiqueVierge();
			}				
		}

		#endregion

		#region Lecture de la légende au passage de la souris

		private void panel1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(panel1.Tag!=null)
			{
				Graphique g = (Graphique)panel1.Tag;
				if(g.MonTypeGraphique!=Graphique.TypeGraphique.Secteur)
				{
					for(int i=0;i<g.ZonesInfo.Length;i++)
					{
						if(e.X>=g.ZonesInfo[i].X && e.X<g.ZonesInfo[i].X + g.ZonesInfo[i].Width)
						{
							toolTip1.SetToolTip(panel1,g.Infos[i]);
							return;
						}
					}
				}					
			}
		}
		private void panel1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(panel1.Tag!=null)
			{
				Graphique g = (Graphique)panel1.Tag;
				if(g.MonTypeGraphique==Graphique.TypeGraphique.Secteur)
				{
					Bitmap map = new Bitmap(panel1.BackgroundImage);
					Color Pixel = map.GetPixel(e.X,e.Y);
					for(int i=0;i<g.Couleurs.Length;i++)
					{
						if(Pixel.ToArgb()==g.Couleurs[i].ToArgb())
						{
							toolTip1.SetToolTip(panel1,g.Infos[i]);
							return;
						}
					}
				}				
			}
		}

		#endregion		

		private void picImprGraphique_Click(object sender, System.EventArgs e)
		{
			if(panel1.Tag!=null)
			{
				Graphique g = (Graphique)panel1.Tag;
				string FileName = Application.StartupPath + "\\" + "Export\\Graphiques\\" + "Graph.png";
				g.Img.Save(FileName,System.Drawing.Imaging.ImageFormat.Png);
				if(System.IO.File.Exists(FileName))
				{
					System.Diagnostics.Process proc = new System.Diagnostics.Process();
					proc.StartInfo = new System.Diagnostics.ProcessStartInfo(FileName);
					proc.Start();
				}
			}
		}

		private void picImprTableau_Click(object sender, System.EventArgs e)
		{
			string FileName = Application.StartupPath + "\\" + "Export\\Graphiques\\" + "Graph.xls";
			fpResultat.SaveExcel(FileName);
			if(System.IO.File.Exists(FileName))
			{
				System.Diagnostics.Process proc = new System.Diagnostics.Process();
				proc.StartInfo = new System.Diagnostics.ProcessStartInfo(FileName);
				proc.Start();
			}

		}

		private void fpResultat_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(panel1.Tag==null) return;
			Graphique graph = (Graphique)panel1.Tag;
			if(graph.Couleurs==null) return;

			FarPoint.Win.Spread.Model.CellRange range = fpResultat.GetCellFromPixel(0,0,e.X,e.Y);
			if(range.Row>-1)
			{
				int TempsEcoule = 0;
				while(EnRoute && TempsEcoule<=2000)
				{
					TempsEcoule+=100;
					System.Threading.Thread.Sleep(100);
				}
				timer1.Enabled = false;
				EnRoute = false;

				bool Cbon = false;
				ArrayList Points = new ArrayList();
				string Legende = fpResultat_Sheet1.Cells[range.Row,1].Text;
				Color m_c = Color.White;
				for(int i=0;i<graph.Infos.Length;i++)
				{
					

					if(graph.Infos[i]==Legende)
					{
						m_c = graph.Couleurs[i];
						// on fait surbriller la zone du sectoriel:
						GraphicsUnit aPixel = GraphicsUnit.Pixel;
						RectangleF imageBoundsF = graph.Img.GetBounds(ref aPixel);

						int imageWidth = Convert.ToInt32(imageBoundsF.Width/1.8);
						int imageHeight = Convert.ToInt32(imageBoundsF.Height);
				
						
						
						for(int intY = 0; intY < imageHeight; intY++) 
						{
							for (int intX = 0; intX < imageWidth; intX++) 
							{ 					
								if (graph.Img.GetPixel(intX, intY) == graph.Couleurs[i]) 
								{
									Points.Add(new Point(intX,intY));								
								}								
							}
						}		
						
						Cbon = true;
						break;
					}
				}

				if(Cbon)
				{					
					m_Points = (Point[])Points.ToArray(typeof(Point));

					EnRoute = true;
					m_CouleurAlternative1 = m_c;
					m_CouleurAlternative2 = Color.White;
					m_intTempsEcoule = 0;
					Alternate = false;
					timer1.Enabled = true;
				}
			}
		}

		private void timer1_Tick(object sender, System.EventArgs e)
		{
			int col = Color.FromArgb(0,0,1).ToArgb();

			
				timer1.Enabled = false;
				m_intTempsEcoule += 100;
				if((m_intTempsEcoule>=1000 || panel1.Tag==null) && !Alternate) 
				{
					EnRoute = false;
					Alternate = false;
					m_intTempsEcoule = 0;
					timer1.Enabled = false;
					return;
				}
				Alternate = !Alternate	;
				
				Graphique graph = (Graphique)panel1.Tag;		
				Graphics g = Graphics.FromImage(graph.Img);

				if(Alternate)
				{
					for(int i=0;i<m_Points.Length;i++)
					{
						SolidBrush brush = new SolidBrush(Color.FromArgb(col));
						g.FillRectangle(brush,m_Points[i].X,m_Points[i].Y,1,1);
					}
				}
				else
				{
					for(int i=0;i<m_Points.Length;i++)
					{
						SolidBrush brush = new SolidBrush(m_CouleurAlternative1);
						g.FillRectangle(brush,m_Points[i].X,m_Points[i].Y,1,1);
					}
				}				
			
				panel1.Invalidate();

				timer1.Enabled = true;
		}

		private void pictureBox1_Click(object sender, System.EventArgs e)
		{
			/*Graphique ret=null;

			string strBaseMedecin = "DRIVER={MySQL ODBC 3.51 Driver};SERVER=192.6.35.125;DATABASE=basemedecin;UID=pchignon;PASSWORD=shosoi";
			Stats.TitreGraphique = "Traitement des appels";

			OdbcConnection cnMedecin = new OdbcConnection(strBaseMedecin);
			cnMedecin.Open();

			DataTable TableFinale = new DataTable();
			TableFinale.Columns.Add(new DataColumn("Date",typeof(string)));
			TableFinale.Columns.Add(new DataColumn("Heure",typeof(string)));
			TableFinale.Columns.Add(new DataColumn("Nb_Appels",typeof(long)));
			TableFinale.Columns.Add(new DataColumn("Nb_Appels_Pris",typeof(long)));
			TableFinale.Columns.Add(new DataColumn("Nb_Appels_Traites",typeof(long)));
			
			DateTime dtDebut = DateTime.Now.AddDays(-9);
			
			System.Data.Odbc.OdbcCommand cmd = new System.Data.Odbc.OdbcCommand();
			
			DataTable TblRequete1 = ExecuteCommandeAvecDataTable(cnMedecin, "select service,date_format(dateappel,'%d/%m') as 'DAP',time_format(dateappel,'%H:%00') as 'HAP',count(dateappel) from appelparservice where service = 102 and dateappel >='" + DateFormatMySql(dtDebut) + "' and dateappel < '" + DateFormatMySql(DateTime.Now) + "' group by DAP,HAP");
			DataTable TblRequete2 = ExecuteCommandeAvecDataTable(cnMedecin,"select service,date_format(dateappel,'%d/%m') as 'DAP',time_format(heureappel,'%H:%00') as 'HAP',count(dateappel) from tableappel where service = 102 and dateappel >='" + DateFormatMySql(dtDebut).Split(' ')[0] + "' and dateappel <= '" + DateFormatMySql(DateTime.Now).Split(' ')[0] + "' group by DAP,HAP");
			DataTable TblRequete3 = ExecuteCommandeAvecDataTable(cnMedecin,"select service,date_format(dateappel,'%d/%m') as 'DAP',time_format(heureappel,'%H:%00') as 'HAP',count(dateappel) from tableappel where service = 102 and dateappel >='" + DateFormatMySql(dtDebut).Split(' ')[0] + "' and dateappel <= '" + DateFormatMySql(DateTime.Now).Split(' ')[0] + "' and annulationappel = 0 and codeintervenant is not null group by DAP,HAP");

			long max = -1;

			for(int j=0;j<10;j++)
			{
				string dt = dtDebut.AddDays(j).Day + "/" + dtDebut.AddDays(j).Month;

				for (int i=0;i<24;i++)
				{
					DataRow MyRow = TableFinale.NewRow();
				
					MyRow["Date"]=dt;
					MyRow["Heure"]=i+":00";

					DataRow r1 = RetourneLigneDateHeure(TblRequete1,dt,i);
					if(r1==null)
						MyRow["Nb_Appels"]=0;
					else
						MyRow["Nb_Appels"]=r1[3];

					if(long.Parse(MyRow["Nb_Appels"].ToString())>max)
						max = long.Parse(MyRow["Nb_Appels"].ToString());

					DataRow r2 = RetourneLigneDateHeure(TblRequete2,dt,i);
					if(r2==null)
						MyRow["Nb_Appels_Pris"]=0;
					else
						MyRow["Nb_Appels_Pris"]=r2[3];

					if(long.Parse(MyRow["Nb_Appels_Pris"].ToString())>max)
						max = long.Parse(MyRow["Nb_Appels_Pris"].ToString());

					DataRow r3 = RetourneLigneDateHeure(TblRequete3,dt,i);
					if(r3==null)
						MyRow["Nb_Appels_Traites"]=0;
					else
						MyRow["Nb_Appels_Traites"]=r3[3];

					if(long.Parse(MyRow["Nb_Appels_Traites"].ToString())>max)
						max = long.Parse(MyRow["Nb_Appels_Traites"].ToString());
					
					TableFinale.Rows.Add(MyRow);
				}
			}

			cnMedecin.Close();			

			ret= Stats.CreationGraphiqueMultipleBaton(panel1.Size, TableFinale,dtDebut,max);			
			panel1.BackgroundImage = ret.Img;*/
		}	

		private DataRow RetourneLigneDateHeure(DataTable tb,string Dt,int Heure)
		{
			int nLigne = -1;
			for(int i=0;i<tb.Rows.Count;i++)
			{
				if(int.Parse(tb.Rows[i][2].ToString().Split(':')[0])==Heure && (int.Parse(Dt.Split('/')[0])== int.Parse(tb.Rows[i][1].ToString().Split('/')[0]) && int.Parse(Dt.Split('/')[1])== int.Parse(tb.Rows[i][1].ToString().Split('/')[1])))
				{
					nLigne=i;
					break;
				}
			}
			if(nLigne==-1)
				return null;
			else
				return tb.Rows[nLigne];
        }

      
        private DataTable ExecuteCommandeAvecDataTable(SqlConnection Cn, string Requete)
        {
            //OdbcCommand commande = new OdbcCommand(Requete,Cn);
            //OdbcDataReader reader = commande.ExecuteReader();

            DataTable dt = new DataTable();
			
            SqlCommand commande = new SqlCommand(Requete, Cn);
            SqlDataReader reader = commande.ExecuteReader();

            for (int i=0;i<reader.FieldCount;i++)
				if(reader.GetName(i).ToLower()=="daterapport")
					dt.Columns.Add(new DataColumn(reader.GetName(i),typeof(DateTime)));
				else
					dt.Columns.Add(new DataColumn(reader.GetName(i),typeof(string)));
			while(reader.Read())
			{
				DataRow row = dt.NewRow();
				for(int i=0;i<reader.FieldCount;i++)
				{
					if(!reader[i].ToString().Equals(System.DBNull.Value) && !reader[i].ToString().Equals(""))
					{
						row[i] = reader[i].ToString().Replace("\r\n","|¤").Replace("\n","|¤").Replace("|¤","\r\n");
					}
					else
						row[i] = "";
				}
				dt.Rows.Add(row);
			}

			reader.Close();
			reader=null;	
			return dt;
		}

		public string DateFormatMySql(DateTime date)
		{
            if (SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.db_Type == "MYSQL")
				return date.Year + "-" + date.Month + "-" + date.Day + " " + date.Hour + ":" + date.Minute + ":" + date.Second;
			else
				return date.Day + "/" + date.Month + "/" + date.Year + " " + date.Hour + ":" + date.Minute + ":" + date.Second;
		}
	}
}
