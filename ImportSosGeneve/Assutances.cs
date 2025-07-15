using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace ImportSosGeneve
{
	/// <summary>
	/// Description résumée de Assutances.
	/// </summary>
	public class Assutances : System.Windows.Forms.Form
	{
        private SosMedecins.SmartRapport.DAL.dstAssurances objDtAssurance;
		private System.Windows.Forms.Button btnLoad;
		private System.Windows.Forms.Label lblNAssurance;
		private System.Windows.Forms.Label lblNAdresse;
		private System.Windows.Forms.Label lblAssAdresseTexte;
		private System.Windows.Forms.Label lblAssNom;
		private System.Windows.Forms.Label lblAssService;
		private System.Windows.Forms.Label lblAssTelephone;
		private System.Windows.Forms.Label lblAssFax;
		private System.Windows.Forms.TextBox editNAssurance;
		private System.Windows.Forms.TextBox editNAdresse;
		private System.Windows.Forms.TextBox editAssAdresseTexte;
		private System.Windows.Forms.TextBox editAssNom;
		private System.Windows.Forms.TextBox editAssService;
		private System.Windows.Forms.TextBox editAssTelephone;
		private System.Windows.Forms.TextBox editAssFax;
		private System.Windows.Forms.Label lblAssCpostale;
		private System.Windows.Forms.Label lblAssExtLocalite;
		private System.Windows.Forms.Label lblAssContact;
		private System.Windows.Forms.Label lblAssApprouve;
		private System.Windows.Forms.Label lblAssCommentaire;
		private System.Windows.Forms.Label lblNCaisse;
		private System.Windows.Forms.TextBox editAssCpostale;
		private System.Windows.Forms.TextBox editAssExtLocalite;
		private System.Windows.Forms.TextBox editAssContact;
		private System.Windows.Forms.TextBox editAssApprouve;
		private System.Windows.Forms.TextBox editAssCommentaire;
		private System.Windows.Forms.TextBox editNCaisse;
		private System.Windows.Forms.Button btnNavFirst;
		private System.Windows.Forms.Button btnNavPrev;
		private System.Windows.Forms.Label lblNavLocation;
		private System.Windows.Forms.Button btnNavNext;
		private System.Windows.Forms.Button btnLast;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btSave;
		private System.Windows.Forms.Button btExit;
		int New = 0;
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Assutances()
		{
			//
			// Requis pour la prise en charge du Concepteur Windows Forms
			//
			InitializeComponent();
			

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Assutances));
            this.objDtAssurance = new SosMedecins.SmartRapport.DAL.dstAssurances();
            this.btnLoad = new System.Windows.Forms.Button();
            this.lblNAssurance = new System.Windows.Forms.Label();
            this.lblNAdresse = new System.Windows.Forms.Label();
            this.lblAssAdresseTexte = new System.Windows.Forms.Label();
            this.lblAssNom = new System.Windows.Forms.Label();
            this.lblAssService = new System.Windows.Forms.Label();
            this.lblAssTelephone = new System.Windows.Forms.Label();
            this.lblAssFax = new System.Windows.Forms.Label();
            this.editNAssurance = new System.Windows.Forms.TextBox();
            this.editNAdresse = new System.Windows.Forms.TextBox();
            this.editAssAdresseTexte = new System.Windows.Forms.TextBox();
            this.editAssNom = new System.Windows.Forms.TextBox();
            this.editAssService = new System.Windows.Forms.TextBox();
            this.editAssTelephone = new System.Windows.Forms.TextBox();
            this.editAssFax = new System.Windows.Forms.TextBox();
            this.lblAssCpostale = new System.Windows.Forms.Label();
            this.lblAssExtLocalite = new System.Windows.Forms.Label();
            this.lblAssContact = new System.Windows.Forms.Label();
            this.lblAssApprouve = new System.Windows.Forms.Label();
            this.lblAssCommentaire = new System.Windows.Forms.Label();
            this.lblNCaisse = new System.Windows.Forms.Label();
            this.editAssCpostale = new System.Windows.Forms.TextBox();
            this.editAssExtLocalite = new System.Windows.Forms.TextBox();
            this.editAssContact = new System.Windows.Forms.TextBox();
            this.editAssApprouve = new System.Windows.Forms.TextBox();
            this.editAssCommentaire = new System.Windows.Forms.TextBox();
            this.editNCaisse = new System.Windows.Forms.TextBox();
            this.btnNavFirst = new System.Windows.Forms.Button();
            this.btnNavPrev = new System.Windows.Forms.Button();
            this.lblNavLocation = new System.Windows.Forms.Label();
            this.btnNavNext = new System.Windows.Forms.Button();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btSave = new System.Windows.Forms.Button();
            this.btExit = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.objDtAssurance)).BeginInit();
            this.SuspendLayout();
            // 
            // objDtAssurance
            // 
            this.objDtAssurance.DataSetName = "DtAssurance";
            this.objDtAssurance.Locale = new System.Globalization.CultureInfo("fr-CH");
            this.objDtAssurance.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(344, 472);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(84, 23);
            this.btnLoad.TabIndex = 2;
            this.btnLoad.Text = "&Charger";
            this.btnLoad.Visible = false;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // lblNAssurance
            // 
            this.lblNAssurance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNAssurance.Location = new System.Drawing.Point(16, 32);
            this.lblNAssurance.Name = "lblNAssurance";
            this.lblNAssurance.Size = new System.Drawing.Size(100, 23);
            this.lblNAssurance.TabIndex = 3;
            this.lblNAssurance.Text = "NAssurance";
            // 
            // lblNAdresse
            // 
            this.lblNAdresse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNAdresse.Location = new System.Drawing.Point(16, 64);
            this.lblNAdresse.Name = "lblNAdresse";
            this.lblNAdresse.Size = new System.Drawing.Size(100, 23);
            this.lblNAdresse.TabIndex = 4;
            this.lblNAdresse.Text = "NAdresse";
            // 
            // lblAssAdresseTexte
            // 
            this.lblAssAdresseTexte.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAssAdresseTexte.Location = new System.Drawing.Point(16, 96);
            this.lblAssAdresseTexte.Name = "lblAssAdresseTexte";
            this.lblAssAdresseTexte.Size = new System.Drawing.Size(100, 23);
            this.lblAssAdresseTexte.TabIndex = 5;
            this.lblAssAdresseTexte.Text = "AdresseTexte";
            // 
            // lblAssNom
            // 
            this.lblAssNom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAssNom.Location = new System.Drawing.Point(16, 128);
            this.lblAssNom.Name = "lblAssNom";
            this.lblAssNom.Size = new System.Drawing.Size(100, 23);
            this.lblAssNom.TabIndex = 6;
            this.lblAssNom.Text = "Nom";
            // 
            // lblAssService
            // 
            this.lblAssService.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAssService.Location = new System.Drawing.Point(16, 160);
            this.lblAssService.Name = "lblAssService";
            this.lblAssService.Size = new System.Drawing.Size(100, 23);
            this.lblAssService.TabIndex = 7;
            this.lblAssService.Text = "Service";
            // 
            // lblAssTelephone
            // 
            this.lblAssTelephone.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAssTelephone.Location = new System.Drawing.Point(16, 192);
            this.lblAssTelephone.Name = "lblAssTelephone";
            this.lblAssTelephone.Size = new System.Drawing.Size(100, 23);
            this.lblAssTelephone.TabIndex = 8;
            this.lblAssTelephone.Text = "Telephone";
            // 
            // lblAssFax
            // 
            this.lblAssFax.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAssFax.Location = new System.Drawing.Point(16, 224);
            this.lblAssFax.Name = "lblAssFax";
            this.lblAssFax.Size = new System.Drawing.Size(100, 23);
            this.lblAssFax.TabIndex = 9;
            this.lblAssFax.Text = "Fax";
            // 
            // editNAssurance
            // 
            this.editNAssurance.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.objDtAssurance, "assurances.NAssurance", true));
            this.editNAssurance.Location = new System.Drawing.Point(120, 32);
            this.editNAssurance.Name = "editNAssurance";
            this.editNAssurance.ReadOnly = true;
            this.editNAssurance.Size = new System.Drawing.Size(48, 20);
            this.editNAssurance.TabIndex = 10;
            // 
            // editNAdresse
            // 
            this.editNAdresse.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.objDtAssurance, "assurances.NAdresse", true));
            this.editNAdresse.Location = new System.Drawing.Point(120, 64);
            this.editNAdresse.Name = "editNAdresse";
            this.editNAdresse.Size = new System.Drawing.Size(312, 20);
            this.editNAdresse.TabIndex = 11;
            // 
            // editAssAdresseTexte
            // 
            this.editAssAdresseTexte.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.objDtAssurance, "assurances.AssAdresseTexte", true));
            this.editAssAdresseTexte.Location = new System.Drawing.Point(120, 96);
            this.editAssAdresseTexte.Name = "editAssAdresseTexte";
            this.editAssAdresseTexte.Size = new System.Drawing.Size(312, 20);
            this.editAssAdresseTexte.TabIndex = 12;
            // 
            // editAssNom
            // 
            this.editAssNom.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.objDtAssurance, "assurances.AssNom", true));
            this.editAssNom.Location = new System.Drawing.Point(120, 128);
            this.editAssNom.Name = "editAssNom";
            this.editAssNom.Size = new System.Drawing.Size(312, 20);
            this.editAssNom.TabIndex = 13;
            // 
            // editAssService
            // 
            this.editAssService.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.objDtAssurance, "assurances.AssService", true));
            this.editAssService.Location = new System.Drawing.Point(120, 160);
            this.editAssService.Name = "editAssService";
            this.editAssService.Size = new System.Drawing.Size(312, 20);
            this.editAssService.TabIndex = 14;
            // 
            // editAssTelephone
            // 
            this.editAssTelephone.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.objDtAssurance, "assurances.AssTelephone", true));
            this.editAssTelephone.Location = new System.Drawing.Point(120, 192);
            this.editAssTelephone.Name = "editAssTelephone";
            this.editAssTelephone.Size = new System.Drawing.Size(312, 20);
            this.editAssTelephone.TabIndex = 15;
            // 
            // editAssFax
            // 
            this.editAssFax.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.objDtAssurance, "assurances.AssFax", true));
            this.editAssFax.Location = new System.Drawing.Point(120, 224);
            this.editAssFax.Name = "editAssFax";
            this.editAssFax.Size = new System.Drawing.Size(120, 20);
            this.editAssFax.TabIndex = 16;
            // 
            // lblAssCpostale
            // 
            this.lblAssCpostale.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAssCpostale.Location = new System.Drawing.Point(16, 256);
            this.lblAssCpostale.Name = "lblAssCpostale";
            this.lblAssCpostale.Size = new System.Drawing.Size(100, 23);
            this.lblAssCpostale.TabIndex = 17;
            this.lblAssCpostale.Text = "Code postale";
            // 
            // lblAssExtLocalite
            // 
            this.lblAssExtLocalite.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAssExtLocalite.Location = new System.Drawing.Point(16, 288);
            this.lblAssExtLocalite.Name = "lblAssExtLocalite";
            this.lblAssExtLocalite.Size = new System.Drawing.Size(100, 23);
            this.lblAssExtLocalite.TabIndex = 18;
            this.lblAssExtLocalite.Text = "Localite";
            // 
            // lblAssContact
            // 
            this.lblAssContact.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAssContact.Location = new System.Drawing.Point(16, 320);
            this.lblAssContact.Name = "lblAssContact";
            this.lblAssContact.Size = new System.Drawing.Size(100, 23);
            this.lblAssContact.TabIndex = 19;
            this.lblAssContact.Text = "Contact";
            // 
            // lblAssApprouve
            // 
            this.lblAssApprouve.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAssApprouve.Location = new System.Drawing.Point(264, 248);
            this.lblAssApprouve.Name = "lblAssApprouve";
            this.lblAssApprouve.Size = new System.Drawing.Size(100, 23);
            this.lblAssApprouve.TabIndex = 20;
            this.lblAssApprouve.Text = "AssApprouve";
            // 
            // lblAssCommentaire
            // 
            this.lblAssCommentaire.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAssCommentaire.Location = new System.Drawing.Point(16, 352);
            this.lblAssCommentaire.Name = "lblAssCommentaire";
            this.lblAssCommentaire.Size = new System.Drawing.Size(100, 23);
            this.lblAssCommentaire.TabIndex = 21;
            this.lblAssCommentaire.Text = "Commentaire";
            // 
            // lblNCaisse
            // 
            this.lblNCaisse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNCaisse.Location = new System.Drawing.Point(264, 288);
            this.lblNCaisse.Name = "lblNCaisse";
            this.lblNCaisse.Size = new System.Drawing.Size(100, 23);
            this.lblNCaisse.TabIndex = 22;
            this.lblNCaisse.Text = "NCaisse";
            // 
            // editAssCpostale
            // 
            this.editAssCpostale.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.objDtAssurance, "assurances.AssCpostale", true));
            this.editAssCpostale.Location = new System.Drawing.Point(120, 256);
            this.editAssCpostale.Name = "editAssCpostale";
            this.editAssCpostale.Size = new System.Drawing.Size(120, 20);
            this.editAssCpostale.TabIndex = 23;
            // 
            // editAssExtLocalite
            // 
            this.editAssExtLocalite.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.objDtAssurance, "assurances.AssExtLocalite", true));
            this.editAssExtLocalite.Location = new System.Drawing.Point(120, 288);
            this.editAssExtLocalite.Name = "editAssExtLocalite";
            this.editAssExtLocalite.Size = new System.Drawing.Size(120, 20);
            this.editAssExtLocalite.TabIndex = 24;
            // 
            // editAssContact
            // 
            this.editAssContact.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.objDtAssurance, "assurances.AssContact", true));
            this.editAssContact.Location = new System.Drawing.Point(120, 320);
            this.editAssContact.Name = "editAssContact";
            this.editAssContact.Size = new System.Drawing.Size(120, 20);
            this.editAssContact.TabIndex = 25;
            // 
            // editAssApprouve
            // 
            this.editAssApprouve.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.objDtAssurance, "assurances.AssApprouve", true));
            this.editAssApprouve.Location = new System.Drawing.Point(368, 248);
            this.editAssApprouve.Name = "editAssApprouve";
            this.editAssApprouve.Size = new System.Drawing.Size(120, 20);
            this.editAssApprouve.TabIndex = 26;
            // 
            // editAssCommentaire
            // 
            this.editAssCommentaire.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.objDtAssurance, "assurances.AssCommentaire", true));
            this.editAssCommentaire.Location = new System.Drawing.Point(120, 352);
            this.editAssCommentaire.Name = "editAssCommentaire";
            this.editAssCommentaire.Size = new System.Drawing.Size(376, 20);
            this.editAssCommentaire.TabIndex = 27;
            // 
            // editNCaisse
            // 
            this.editNCaisse.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.objDtAssurance, "assurances.NCaisse", true));
            this.editNCaisse.Location = new System.Drawing.Point(368, 288);
            this.editNCaisse.Name = "editNCaisse";
            this.editNCaisse.Size = new System.Drawing.Size(120, 20);
            this.editNCaisse.TabIndex = 28;
            // 
            // btnNavFirst
            // 
            this.btnNavFirst.Location = new System.Drawing.Point(120, 392);
            this.btnNavFirst.Name = "btnNavFirst";
            this.btnNavFirst.Size = new System.Drawing.Size(40, 23);
            this.btnNavFirst.TabIndex = 29;
            this.btnNavFirst.Text = "<<";
            this.btnNavFirst.Click += new System.EventHandler(this.btnNavFirst_Click);
            // 
            // btnNavPrev
            // 
            this.btnNavPrev.Location = new System.Drawing.Point(160, 392);
            this.btnNavPrev.Name = "btnNavPrev";
            this.btnNavPrev.Size = new System.Drawing.Size(35, 23);
            this.btnNavPrev.TabIndex = 30;
            this.btnNavPrev.Text = "<";
            this.btnNavPrev.Click += new System.EventHandler(this.btnNavPrev_Click);
            // 
            // lblNavLocation
            // 
            this.lblNavLocation.BackColor = System.Drawing.Color.White;
            this.lblNavLocation.Location = new System.Drawing.Point(200, 392);
            this.lblNavLocation.Name = "lblNavLocation";
            this.lblNavLocation.Size = new System.Drawing.Size(95, 23);
            this.lblNavLocation.TabIndex = 31;
            this.lblNavLocation.Text = "Aucun enregistrement";
            this.lblNavLocation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnNavNext
            // 
            this.btnNavNext.Location = new System.Drawing.Point(288, 392);
            this.btnNavNext.Name = "btnNavNext";
            this.btnNavNext.Size = new System.Drawing.Size(35, 23);
            this.btnNavNext.TabIndex = 32;
            this.btnNavNext.Text = ">";
            this.btnNavNext.Click += new System.EventHandler(this.btnNavNext_Click);
            // 
            // btnLast
            // 
            this.btnLast.Location = new System.Drawing.Point(320, 392);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(40, 23);
            this.btnLast.TabIndex = 33;
            this.btnLast.Text = ">>";
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(32, 440);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 34;
            this.btnAdd.Text = "Aj&outer";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(144, 440);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 35;
            this.btnDelete.Text = "&Supprimer";
            this.btnDelete.Visible = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(248, 440);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 36;
            this.btnCancel.Text = "Annuler Edit";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(192, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 24);
            this.label1.TabIndex = 37;
            this.label1.Text = "Liste Assurances";
            // 
            // btSave
            // 
            this.btSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btSave.Image = ((System.Drawing.Image)(resources.GetObject("btSave.Image")));
            this.btSave.Location = new System.Drawing.Point(448, 392);
            this.btSave.Margin = new System.Windows.Forms.Padding(0);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(56, 48);
            this.btSave.TabIndex = 38;
            this.btSave.Click += new System.EventHandler(this.button1_Click);
            // 
            // btExit
            // 
            this.btExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btExit.Image = ((System.Drawing.Image)(resources.GetObject("btExit.Image")));
            this.btExit.Location = new System.Drawing.Point(448, 456);
            this.btExit.Name = "btExit";
            this.btExit.Size = new System.Drawing.Size(56, 48);
            this.btExit.TabIndex = 39;
            this.btExit.Click += new System.EventHandler(this.btExit_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(32, 480);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(232, 24);
            this.label2.TabIndex = 40;
            // 
            // Assutances
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.ClientSize = new System.Drawing.Size(544, 518);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btExit);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.lblNAssurance);
            this.Controls.Add(this.lblNAdresse);
            this.Controls.Add(this.lblAssAdresseTexte);
            this.Controls.Add(this.lblAssNom);
            this.Controls.Add(this.lblAssService);
            this.Controls.Add(this.lblAssTelephone);
            this.Controls.Add(this.lblAssFax);
            this.Controls.Add(this.editNAssurance);
            this.Controls.Add(this.editNAdresse);
            this.Controls.Add(this.editAssAdresseTexte);
            this.Controls.Add(this.editAssNom);
            this.Controls.Add(this.editAssService);
            this.Controls.Add(this.editAssTelephone);
            this.Controls.Add(this.editAssFax);
            this.Controls.Add(this.editAssCpostale);
            this.Controls.Add(this.editAssExtLocalite);
            this.Controls.Add(this.editAssContact);
            this.Controls.Add(this.editAssApprouve);
            this.Controls.Add(this.editAssCommentaire);
            this.Controls.Add(this.editNCaisse);
            this.Controls.Add(this.lblAssCpostale);
            this.Controls.Add(this.lblAssExtLocalite);
            this.Controls.Add(this.lblAssContact);
            this.Controls.Add(this.lblAssApprouve);
            this.Controls.Add(this.lblAssCommentaire);
            this.Controls.Add(this.lblNCaisse);
            this.Controls.Add(this.btnNavFirst);
            this.Controls.Add(this.btnNavPrev);
            this.Controls.Add(this.lblNavLocation);
            this.Controls.Add(this.btnNavNext);
            this.Controls.Add(this.btnLast);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Assutances";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Assurances";
            this.Load += new System.EventHandler(this.Assutances_Load);
            ((System.ComponentModel.ISupportInitialize)(this.objDtAssurance)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		public void LoadDataSet(string Requete)
		{
			//ImportSosGeneve.MySql objService;
			//objService = new ImportSosGeneve.MySql();
			// Créez un nouveau groupe de données destiné à contenir les enregistrements retournés par l'appel à FillDataSet.
			// C'est un groupe de données temporaire qui est utilisé, car si le groupe de données existant était rempli,
			// il faudrait relier les liaisons de données.
            SosMedecins.SmartRapport.DAL.dstAssurances objDataSetTemp;
            objDataSetTemp = new SosMedecins.SmartRapport.DAL.dstAssurances();
			// Essayez de remplir le groupe de données temporaire.
			OutilsExt.OutilsSql.RemplitDataTable(objDataSetTemp.Tables[0], "select * from assurances order by NAssurance asc");
			int i = objDataSetTemp.Tables[0].Rows.Count;
			// Videz le groupe de données de ses anciens enregistrements.
			objDtAssurance.Clear();
			// Fusionnez les enregistrements dans le groupe de données principal.
			objDtAssurance.Merge(objDataSetTemp);
		}

		private void objDtAssurance_PositionChanged()
		{
			this.lblNavLocation.Text = ((((this.BindingContext[objDtAssurance,"assurances"].Position + 1)).ToString() + " sur  ") 
				+ this.BindingContext[objDtAssurance,"assurances"].Count.ToString());

		}

		private void btnNavNext_Click(object sender, System.EventArgs e)
		{
			this.BindingContext[objDtAssurance,"assurances"].Position = (this.BindingContext[objDtAssurance,"assurances"].Position + 1);
			this.objDtAssurance_PositionChanged();

		}

		private void btnNavPrev_Click(object sender, System.EventArgs e)
		{
			this.BindingContext[objDtAssurance,"assurances"].Position = (this.BindingContext[objDtAssurance,"assurances"].Position - 1);
			this.objDtAssurance_PositionChanged();

		}

		private void btnLast_Click(object sender, System.EventArgs e)
		{
			this.BindingContext[objDtAssurance,"assurances"].Position = (this.objDtAssurance.Tables["assurances"].Rows.Count - 1);
			this.objDtAssurance_PositionChanged();

		}

		private void btnNavFirst_Click(object sender, System.EventArgs e)
		{
			this.BindingContext[objDtAssurance,"assurances"].Position = 0;
			this.objDtAssurance_PositionChanged();

		}

		private void btnLoad_Click(object sender, System.EventArgs e)
		{
			// Essayez de charger le groupe de données.
			this.LoadDataSet("select * from assurances order by NAssurance asc");
			this.objDtAssurance_PositionChanged();
		}

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			// Annule les modifications en cours
			this.BindingContext[objDtAssurance,"assurances"].EndCurrentEdit();
			this.BindingContext[objDtAssurance,"assurances"].AddNew();
			
			New = 1;
			this.objDtAssurance_PositionChanged();

		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			if ((this.BindingContext[objDtAssurance,"assurances"].Count > 0)) 
			{
				this.BindingContext[objDtAssurance,"assurances"].RemoveAt(this.BindingContext[objDtAssurance,"assurances"].Position);
				this.objDtAssurance_PositionChanged();
			}

		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.BindingContext[objDtAssurance,"assurances"].CancelCurrentEdit();
			this.objDtAssurance_PositionChanged();

		}

		private void Assutances_Load(object sender, System.EventArgs e)
		{
			
			// Essayez de charger le groupe de données.
			this.LoadDataSet("select * from assurances order by NAssurance asc");
				
			this.objDtAssurance_PositionChanged();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			//Assutances.ActiveForm.Tag = this.BindingContext[objDtAssurance,"assurances"].Current;
			DataRow rowAssurances = objDtAssurance.Tables[0].Rows[0];
			
			if(editAssNom.Text=="")
			{
				MessageBox.Show("Entrez Le NOM d'Assurance!");
				return;
			}

			// Sauvegarde de la fip / données administratives
			int count = this.objDtAssurance.Tables["assurances"].Rows.Count;
			int Num = 0;
			if (New == 1)
				Num = int.Parse(objDtAssurance.Tables["assurances"].Rows[count-1]["NAssurance"].ToString())+1;
			else
				Num = int.Parse(editNAssurance.Text);

			rowAssurances["NAssurance"] = double.Parse(Num.ToString());
			if(editNAdresse.Text!="")
				rowAssurances["NAdresse"] = double.Parse(editNAdresse.Text.ToString());
			else rowAssurances["NAdresse"] = 0;
			if(editAssService.Text!="")
				rowAssurances["AssService"] = editAssService.Text.ToString();
			else rowAssurances["AssService"] = "";

			if(editAssAdresseTexte.Text!="")
				rowAssurances["AssAdresseTexte"] = editAssAdresseTexte.Text.ToString();
			else rowAssurances["AssAdresseTexte"] ="";
			if(editAssNom.Text!="")
				rowAssurances["AssNom"] = editAssNom.Text.ToString();
			else rowAssurances["AssNom"] = "";
			if(editAssTelephone.Text!="")
				rowAssurances["AssTelephone"] = editAssTelephone.Text.ToString();
			else rowAssurances["AssTelephone"] ="";
			if(editAssFax.Text!="")
				rowAssurances["AssFax"] = editAssFax.Text.ToString();
			else rowAssurances["AssFax"] ="";
			if(editAssCpostale.Text!="")
				rowAssurances["AssCpostale"] = editAssCpostale.Text.ToString();
			else rowAssurances["AssCpostale"] ="";
			if(editAssExtLocalite.Text!="")
				rowAssurances["AssExtLocalite"] = editAssExtLocalite.Text.ToString();
			else rowAssurances["AssExtLocalite"] = "";
			if(editAssContact.Text!="")
				rowAssurances["AssContact"] = editAssContact.Text.ToString();
			else rowAssurances["AssContact"] ="";
			if(editAssApprouve.Text!="")
				rowAssurances["AssApprouve"] = double.Parse(editAssApprouve.Text.ToString());
			else rowAssurances["AssApprouve"] = 1;
			if(editAssCommentaire.Text!="")
				rowAssurances["AssCommentaire"] = editAssCommentaire.Text.ToString();
			else rowAssurances["AssCommentaire"] ="";

		
			rowAssurances["NCaisse"] = int.Parse(editNCaisse.Text).ToString("D7");
			//save the insurance info to the database

			bool reussite = OutilsExt.OutilsSql.SauvegardeAssurance(rowAssurances,New);

			if(reussite)
			{
				label2.Text = "Sauvegarde réussie";
				New =0;
			}
			else
				label2.Text = "Erreur lors de la sauvegarde";
		}
		private void btExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}
