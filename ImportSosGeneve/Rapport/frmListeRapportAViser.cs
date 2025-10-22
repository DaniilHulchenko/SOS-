using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Net.Mail;

namespace ImportSosGeneve
{
	/// <summary>
	/// Description résumée de frmListeRapportAViser.
	/// </summary>
	public class frmListeRapportAViser : System.Windows.Forms.UserControl
	{
		#region Déclaration des variables

		private string[][] m_ListeRapports =null;
		private frmGeneral m_frmgeneral = null;
		public enum TypeListe {AVISER,POURENVOI,AREPRENDRE,SANSRAPPORT,ACORRIGER};
		public bool m_EnvoiAuto = false;
		public frmListeRapportAViser.TypeListe MonTypeDeListe;



		private System.Windows.Forms.Label label1;
		public System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ComboBox cbRapport_Imprimante;
		private System.Windows.Forms.Label lblStatusEnvoi;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
		private System.Windows.Forms.Button btnTout;
		private System.Windows.Forms.ComboBox cbModeEnvoi;
		private System.Windows.Forms.DateTimePicker dateTimePicker1;
		private System.Windows.Forms.DateTimePicker dateTimePicker2;
		private System.Windows.Forms.CheckBox chkLogo;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.Button btnCharger;
		private System.Windows.Forms.Button btnImprimer;
		private System.Windows.Forms.GroupBox grpCritere;
		private System.Windows.Forms.Label lblMode;
		private System.Windows.Forms.Label lblIntervalle;
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		#endregion

		#region Construction / Destruction du formulaire

		public frmListeRapportAViser(frmGeneral frm)
		{
			//
			// Requis pour la prise en charge du Concepteur Windows Forms
			//
			InitializeComponent();

			this.m_frmgeneral = frm;

			// Chargement des imprimantes dans la liste des Imprimantes : Sélection de l'imprimante par défaut :
			System.Drawing.Printing.PrintDocument prtdoc = new System.Drawing.Printing.PrintDocument();
			foreach(String strPrinter in System.Drawing.Printing.PrinterSettings.InstalledPrinters) 
			{
				cbRapport_Imprimante.Items.Add(strPrinter);
                if (strPrinter.ToLower() == SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.StrReportPrinter.ToLower())
				{
					cbRapport_Imprimante.SelectedIndex = cbRapport_Imprimante.Items.IndexOf(strPrinter);
				}
			}	
			prtdoc.Dispose();
			prtdoc = null;	
			this.Visible = true;		
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListeRapportAViser));
            this.label1 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cbRapport_Imprimante = new System.Windows.Forms.ComboBox();
            this.lblStatusEnvoi = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.btnTout = new System.Windows.Forms.Button();
            this.cbModeEnvoi = new System.Windows.Forms.ComboBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.chkLogo = new System.Windows.Forms.CheckBox();
            this.btnCharger = new System.Windows.Forms.Button();
            this.btnImprimer = new System.Windows.Forms.Button();
            this.grpCritere = new System.Windows.Forms.GroupBox();
            this.lblIntervalle = new System.Windows.Forms.Label();
            this.lblMode = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.grpCritere.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(248, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Liste des rapports à viser";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listView1
            // 
            this.listView1.CheckBoxes = true;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8});
            this.listView1.FullRowSelect = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listView1.Location = new System.Drawing.Point(8, 136);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(288, 168);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 20;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Width = 125;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Width = 109;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Width = 208;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Width = 0;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Width = 0;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Width = 100;
            // 
            // cbRapport_Imprimante
            // 
            this.cbRapport_Imprimante.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRapport_Imprimante.Location = new System.Drawing.Point(8, 337);
            this.cbRapport_Imprimante.Name = "cbRapport_Imprimante";
            this.cbRapport_Imprimante.Size = new System.Drawing.Size(208, 21);
            this.cbRapport_Imprimante.TabIndex = 6;
            // 
            // lblStatusEnvoi
            // 
            this.lblStatusEnvoi.BackColor = System.Drawing.Color.Transparent;
            this.lblStatusEnvoi.ForeColor = System.Drawing.Color.Red;
            this.lblStatusEnvoi.Location = new System.Drawing.Point(8, 368);
            this.lblStatusEnvoi.Name = "lblStatusEnvoi";
            this.lblStatusEnvoi.Size = new System.Drawing.Size(272, 16);
            this.lblStatusEnvoi.TabIndex = 8;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(272, 8);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(23, 24);
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.BackColor = System.Drawing.Color.Transparent;
            this.checkBox1.Location = new System.Drawing.Point(152, 312);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(64, 19);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "Fax";
            this.checkBox1.UseVisualStyleBackColor = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // btnTout
            // 
            this.btnTout.Location = new System.Drawing.Point(8, 308);
            this.btnTout.Name = "btnTout";
            this.btnTout.Size = new System.Drawing.Size(104, 26);
            this.btnTout.TabIndex = 3;
            this.btnTout.Text = "Tout";
            this.btnTout.Click += new System.EventHandler(this.btnTout_Click);
            // 
            // cbModeEnvoi
            // 
            this.cbModeEnvoi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbModeEnvoi.Items.AddRange(new object[] {
            "Aucun",
            "Mail",
            "Fax",
            "Courrier"});
            this.cbModeEnvoi.Location = new System.Drawing.Point(80, 56);
            this.cbModeEnvoi.Name = "cbModeEnvoi";
            this.cbModeEnvoi.Size = new System.Drawing.Size(96, 21);
            this.cbModeEnvoi.TabIndex = 4;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(80, 24);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(96, 20);
            this.dateTimePicker1.TabIndex = 1;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker2.Location = new System.Drawing.Point(184, 24);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(96, 20);
            this.dateTimePicker2.TabIndex = 2;
            // 
            // chkLogo
            // 
            this.chkLogo.BackColor = System.Drawing.Color.Transparent;
            this.chkLogo.Location = new System.Drawing.Point(223, 312);
            this.chkLogo.Name = "chkLogo";
            this.chkLogo.Size = new System.Drawing.Size(72, 16);
            this.chkLogo.TabIndex = 5;
            this.chkLogo.Text = "Logo";
            this.chkLogo.UseVisualStyleBackColor = false;
            // 
            // btnCharger
            // 
            this.btnCharger.Location = new System.Drawing.Point(184, 55);
            this.btnCharger.Name = "btnCharger";
            this.btnCharger.Size = new System.Drawing.Size(88, 23);
            this.btnCharger.TabIndex = 5;
            this.btnCharger.Text = "charger";
            this.btnCharger.Click += new System.EventHandler(this.btnCharger_Click);
            // 
            // btnImprimer
            // 
            this.btnImprimer.Location = new System.Drawing.Point(224, 336);
            this.btnImprimer.Name = "btnImprimer";
            this.btnImprimer.Size = new System.Drawing.Size(75, 23);
            this.btnImprimer.TabIndex = 7;
            this.btnImprimer.Text = "Imprimer";
            this.btnImprimer.Click += new System.EventHandler(this.btnImprimer_Click);
            // 
            // grpCritere
            // 
            this.grpCritere.Controls.Add(this.lblIntervalle);
            this.grpCritere.Controls.Add(this.lblMode);
            this.grpCritere.Controls.Add(this.cbModeEnvoi);
            this.grpCritere.Controls.Add(this.dateTimePicker1);
            this.grpCritere.Controls.Add(this.dateTimePicker2);
            this.grpCritere.Controls.Add(this.btnCharger);
            this.grpCritere.Location = new System.Drawing.Point(8, 40);
            this.grpCritere.Name = "grpCritere";
            this.grpCritere.Size = new System.Drawing.Size(288, 88);
            this.grpCritere.TabIndex = 1;
            this.grpCritere.TabStop = false;
            this.grpCritere.Text = "Critére de séléction";
            // 
            // lblIntervalle
            // 
            this.lblIntervalle.Location = new System.Drawing.Point(8, 28);
            this.lblIntervalle.Name = "lblIntervalle";
            this.lblIntervalle.Size = new System.Drawing.Size(56, 16);
            this.lblIntervalle.TabIndex = 0;
            this.lblIntervalle.Text = "Intervalle";
            // 
            // lblMode
            // 
            this.lblMode.Location = new System.Drawing.Point(8, 60);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(48, 16);
            this.lblMode.TabIndex = 3;
            this.lblMode.Text = "Mode :";
            // 
            // frmListeRapportAViser
            // 
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.Controls.Add(this.grpCritere);
            this.Controls.Add(this.btnImprimer);
            this.Controls.Add(this.chkLogo);
            this.Controls.Add(this.btnTout);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblStatusEnvoi);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbRapport_Imprimante);
            this.Name = "frmListeRapportAViser";
            this.Size = new System.Drawing.Size(304, 384);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.grpCritere.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		#region Propriétés publiques

		public string Label
		{
			set
			{
				label1.Text = value;
			}
		}

		public String[][] ListeRapports
		{
			set
			{
				if(MonTypeDeListe==frmListeRapportAViser.TypeListe.POURENVOI || MonTypeDeListe==frmListeRapportAViser.TypeListe.SANSRAPPORT)
				{
					btnImprimer.Visible = true;
					if(MonTypeDeListe==frmListeRapportAViser.TypeListe.POURENVOI)
					{
						cbModeEnvoi.Visible = true;
						dateTimePicker1.Visible = true;
						dateTimePicker2.Visible = true;
						btnCharger.Visible = true;
					}
					else
					{
						cbModeEnvoi.Visible = false;
						dateTimePicker1.Visible = false;
						dateTimePicker2.Visible = false;
						btnCharger.Visible = false;
					}
				}
				else
				{
					btnImprimer.Visible = false;
					cbModeEnvoi.Visible = false;
					dateTimePicker1.Visible = false;
					dateTimePicker2.Visible = false;
					btnCharger.Visible = false;
				}

				m_ListeRapports = value;
				MAJListe();
			}
		}

		#endregion

		#region Méthodes privées

		private void MAJListe()
		{
			listView1.Items.Clear();
			foreach(string[] s in m_ListeRapports)
			{
				if(s.Length>=4)
				{
					ListViewItem item = new ListViewItem(s[0]);
					item.SubItems.Add(s[1]);
					item.SubItems.Add(s[2]);
					item.SubItems.Add(s[3]);
					item.SubItems.Add(s[4]);
					if(s.Length==7)
					{
						item.SubItems.Add(s[5]);
						item.SubItems.Add(s[6]);						
					}
					if(MonTypeDeListe==TypeListe.SANSRAPPORT)
					{
						if(s[5]!="")
						{
							item.SubItems.Add("");
							item.SubItems.Add(DateTime.Parse(s[5]).ToString("dd/MM/yyyy"));
						}
					}
					listView1.Items.Add(item);
				}
			}
		}

		#endregion

		#region Evenements du formulaire

		public void listView1_DoubleClick(object sender, System.EventArgs e)

		{
			// Sélection d'un rapport :
			if(listView1.SelectedIndices.Count>0)
			{
				this.Cursor = Cursors.WaitCursor;

				try
				{

					ListViewItem item = (ListViewItem)listView1.SelectedItems[0];
					long IdRapport = long.Parse(item.Text);
					long NConsultation = long.Parse(item.SubItems[4].Text);

					Donnees.MonDataSetAppels = this.m_frmgeneral.RecuperationConsultationByNConsult(NConsultation);
					if(Donnees.MonDataSetAppels!=null && Donnees.MonDataSetAppels.Tables[0].Rows.Count>0)
					{
						this.m_frmgeneral.InitialiseBoutonsFiltre(false);
						this.m_frmgeneral.MiseAJourListeAppels(Donnees.MonDataSetAppels);
						this.m_frmgeneral.InitialiseBoutonsFiltre(true);
						this.m_frmgeneral.PrepareFicheStatiqueVierge();
						this.m_frmgeneral.PrepareFicheDynamiqueVierge();
						this.m_frmgeneral.AfficheAppel(Donnees.MonDataSetAppels.Tables[0].Rows[0]);
					}

					this.m_frmgeneral.AffichageRapport(IdRapport);

					if(item.SubItems.Count>=6 && item.SubItems[5].Text!="")
					{
						int CodeDestinaire = int.Parse(item.SubItems[5].Text);
                        SosMedecins.SmartRapport.DAL.dstDestination.DestinationRow row = this.m_frmgeneral.RetrouveDestinataireRapport(CodeDestinaire);
						if(row!=null)	this.m_frmgeneral.AffichageRapportAvecDestinataire(row);
					}

					this.m_frmgeneral.ActiveFenetreRapport();	
				}
				catch
				{
				}

				this.Cursor = Cursors.Default;				
			}		
		}
		public void EnvoiCommande(Keys keyBtn)
		{
			if(keyBtn==Keys.F1)
			{
				listView1_DoubleClick(null,null);
			}
		}

		private void pictureBox1_Click(object sender, System.EventArgs e)
		{
			this.Dispose();
		}

		private void btnTout_Click(object sender, System.EventArgs e)
		{
			if(btnTout.Text == "Tout")
			{
				for(int i=0;i<listView1.Items.Count;i++)
					listView1.Items[i].Checked = true;
				btnTout.Text = "Désélectionner";
			}
			else
			{
				for(int i=0;i<listView1.Items.Count;i++)
					listView1.Items[i].Checked = false;
				btnTout.Text = "Tout";
			}
		}

		private void checkBox1_CheckedChanged(object sender, System.EventArgs e)
		{
			chkLogo.Checked = checkBox1.Checked;
			cbRapport_Imprimante.Visible = !checkBox1.Checked;
		}	

		#endregion

		#region Gestion des envois automatiques

		public void EnvoiFaxAuto()
		{	
			// Selection pour fax,Mail (Par activeFax)
			//voir ici 
            
            cbModeEnvoi.SelectedIndex=2;
			dateTimePicker1.Value  = DateTime.Now.AddMonths(-6);
			dateTimePicker2.Value = DateTime.Now;
			btnCharger_Click(null,null);
			for(int i=0;i<listView1.Items.Count;i++)
				listView1.Items[i].Checked=true;
			chkLogo.Checked = true;
			checkBox1.Checked = true;
			btnImprimer_Click(null,null);
		}
		public void EnvoiCourrierAuto()
		{	
			// Selection pour courrier,
			cbModeEnvoi.SelectedIndex=3;
			dateTimePicker1.Value  = DateTime.Now.AddMonths(-6);
			dateTimePicker2.Value = DateTime.Now;
			btnImprimer_Click(null,null);
			btnTout_Click(null,null);
			for(int i=0;i<listView1.Items.Count;i++)
				listView1.Items[i].Checked=true;
			chkLogo.Checked = false;
			checkBox1.Checked = false;
			btnImprimer_Click(null,null);
		}

		#endregion

		private void btnCharger_Click(object sender, System.EventArgs e)
		{
			if(cbModeEnvoi.Text!="")
			{
                m_ListeRapports = OutilsExt.OutilsSql.ListeRapportPourEnvoi(dateTimePicker1.Value, dateTimePicker2.Value, cbModeEnvoi.Text);
				if(cbModeEnvoi.Text  == "Fax")
				{
					checkBox1.Checked = true;
                    checkBox1.Visible = false;
					cbRapport_Imprimante.Text = "ActiveFax";
					cbRapport_Imprimante.Visible = false;
				}
				else
                    if (cbModeEnvoi.Text=="Mail")
                    {
                        checkBox1.Visible = false;
                        checkBox1.Enabled = false;
                        cbRapport_Imprimante.Visible = false;
                        cbRapport_Imprimante.Enabled = false;
                    }
                    else
				{
					checkBox1.Checked = false;
					cbRapport_Imprimante.Visible = true;
                    cbRapport_Imprimante.Enabled = true;
				}
			}
			else
			{
				
				MessageBox.Show("Veuillez sélectionner un mode d'envoi");
				return;
			}
			
			MAJListe();
		}

		private void btnImprimer_Click(object sender, System.EventArgs e)
		{
			if(cbRapport_Imprimante.SelectedIndex==-1)
			{
				MessageBox.Show("Veuillez sélectionner une imprimante");
				return;
			}

           // MessageBox.Show(cbRapport_Imprimante.SelectedText.ToString());     //**************************
            
            if(!m_EnvoiAuto)
			{
				DialogResult result =  MessageBox.Show("Confirmer l'envoi de " + listView1.CheckedIndices.Count + " rapports?","Envoi de rapports",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button1);
				if(result==DialogResult.No) return;
			}

			this.Cursor = Cursors.WaitCursor;
            try
            {
			    for(int i=0;i<listView1.CheckedIndices.Count;i++)
			    {
				    int NDocument =i + 1;
				    lblStatusEnvoi.Text = "Impression du document " + NDocument + " / " + listView1.CheckedIndices.Count;

				    // Tentative d'impression du rapport :
				    ListViewItem item = (ListViewItem)listView1.CheckedItems[i];
				    long IdRapport = long.Parse(item.Text);
				    this.m_frmgeneral.AffichageRapport(IdRapport);
				    if(item.SubItems.Count>=6 && item.SubItems[5].Text!="" && MonTypeDeListe==frmListeRapportAViser.TypeListe.POURENVOI)
				    {
					    int CodeDestinaire = int.Parse(item.SubItems[5].Text);
                        SosMedecins.SmartRapport.DAL.dstDestination.DestinationRow row = this.m_frmgeneral.RetrouveDestinataireRapport(CodeDestinaire);
					    if(row!=null)
					    {
						    this.m_frmgeneral.AffichageRapportAvecDestinataire(row);

                            //if (checkBox1.Checked)
                           // {

                                string NumFax = "";
                                string Email = "";
                                string NomDestinataire = "";
                                if (row.TypeDestinataire == "MedecinTra")
                                {
                                    if (cbModeEnvoi.SelectedIndex == 2) //Fax
                                    {
                                        NumFax = OutilsExt.OutilsSql.GetFaxFromMedecin(CodeDestinaire);
                                        NomDestinataire = OutilsExt.OutilsSql.GetNomFromMedecin(CodeDestinaire);
                                    }
                                    else if (cbModeEnvoi.SelectedIndex == 1)  //Mail
                                    {
                                        Email = OutilsExt.OutilsSql.GetMailFromMedecin(CodeDestinaire);
                                        NomDestinataire = OutilsExt.OutilsSql.GetNomFromMedecin(CodeDestinaire);                                        
                                    }
                                }
                                else if (row.TypeDestinataire == "HotelPolic")  
                                {
                                    NumFax = OutilsExt.OutilsSql.GetFaxFromCommissariat();
                                    NomDestinataire = "Commissariat";
                                }


                                if (cbModeEnvoi.SelectedIndex == 1)  //Mail
                                {
                                    try
                                    {
                                        string FileName = "Rapport patient - " + Donnees.MonDtRapport.Rapport[0].NomPatient + "" + Donnees.MonDtRapport.Rapport[0].PrenomPatient + "-" + DateTime.Today.ToString("yyyy.MM.dd");
                                        CrystalUtility.ExportReport(Donnees.MonEtatRapport, "pdf", Application.StartupPath + SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.Cache, FileName);
                                        SosMedecins.Utilitaires.Mail objMail = new SosMedecins.Utilitaires.Mail(SosMedecins.SmartRapport.GestionApplication.VariablesApplicatives.Utilisateurs.EMail);
                                        objMail.Sujet = "Rapport Patient N° " + IdRapport + "Nom" + Donnees.MonDtRapport.Rapport[0].NomPatient + "" + Donnees.MonDtRapport.Rapport[0].PrenomPatient;

                                        objMail.Message = "";
                                        objMail.JoindrePiece(Application.StartupPath + SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.Cache + FileName + ".pdf");
                                        objMail.AjouteDestinataire(Email);
                                        if (objMail.Envoi())
                                        {
                                             if (OutilsExt.OutilsSql.SetRapportEnvoye(IdRapport, CodeDestinaire, true))
                                            item.BackColor = Color.PaleGreen;

                                            else item.BackColor = Color.MistyRose;
                                        }
                                        else item.BackColor = Color.MistyRose;
                                    }
                                    catch(SmtpFailedRecipientException ex)

                                    {
                                        MessageBox.Show(ex.Message.ToString());
                                    }
                                    
                                }
                                else
                                {
                                    if (cbModeEnvoi.SelectedIndex == 3)  //courrier
                                    {
                                        if (CrystalUtility.PrintReport1(Donnees.MonEtatRapport, 1, cbRapport_Imprimante.Text, chkLogo.Checked))
                                        {
                                            if (OutilsExt.OutilsSql.SetRapportEnvoye(IdRapport, CodeDestinaire, true))
                                                item.BackColor = Color.PaleGreen;
                                            else
                                                item.BackColor = Color.MistyRose;
                                        }
                                        else
                                            item.BackColor = Color.MistyRose;
                                    }
                                    else  //c'est le fax
                                    {

                                        if (CrystalUtility.FaxReport(IdRapport, Donnees.MonEtatRapport, NumFax, cbRapport_Imprimante.Text, NomDestinataire, chkLogo.Checked))
                                        {
                                            if (OutilsExt.OutilsSql.SetRapportEnvoye(IdRapport, CodeDestinaire, true))
                                                item.BackColor = Color.PaleGreen;
                                            else item.BackColor = Color.MistyRose;
                                        }
                                        else item.BackColor = Color.MistyRose;
                                    }
                                }
                       
					    }

				    }
				    else if(MonTypeDeListe==frmListeRapportAViser.TypeListe.SANSRAPPORT)
				    {
					    if(CrystalUtility.PrintReport(Donnees.MonSansRapport,1,cbRapport_Imprimante.Text))
					    {
						    OutilsExt.OutilsSql.EnvoiDeSansRapport(IdRapport);
						    item.BackColor = Color.PaleGreen;					
					    }
					    else
						    item.BackColor = Color.MistyRose;
				    }
    				
			    }
            }
            catch (Exception ex)
            {
                // Erreur ignorée a reprendre
                MessageBox.Show(ex.Message.ToString());
               
            }
			this.Cursor = Cursors.Default;
		}
	}
}
