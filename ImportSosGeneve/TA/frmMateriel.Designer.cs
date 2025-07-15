namespace ImportSosGeneve.TA
{
    partial class frmMateriel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMateriel));
            this.lbltop = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblInfoVIDmedaillon = new System.Windows.Forms.Label();
            this.tbxVIDm = new System.Windows.Forms.TextBox();
            this.lblVIDm = new System.Windows.Forms.Label();
            this.lbl0003 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnModifier = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnAjouter = new System.Windows.Forms.Button();
            this.cbxDateHS = new System.Windows.Forms.CheckBox();
            this.tbxPrixAchat = new System.Windows.Forms.TextBox();
            this.cbTypeTarif = new System.Windows.Forms.ComboBox();
            this.tbxTel = new System.Windows.Forms.TextBox();
            this.tbxContactID = new System.Windows.Forms.TextBox();
            this.tbxVID = new System.Windows.Forms.TextBox();
            this.cbLibelle = new System.Windows.Forms.ComboBox();
            this.lblPrixAchat = new System.Windows.Forms.Label();
            this.lblTypeTarif = new System.Windows.Forms.Label();
            this.lblTel = new System.Windows.Forms.Label();
            this.lblContactID = new System.Windows.Forms.Label();
            this.lblVID = new System.Windows.Forms.Label();
            this.lblLibelle = new System.Windows.Forms.Label();
            this.btnValider = new System.Windows.Forms.Button();
            this.btnAnnuler = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.rBContactID = new System.Windows.Forms.RadioButton();
            this.rBLibelle = new System.Windows.Forms.RadioButton();
            this.rBVID = new System.Windows.Forms.RadioButton();
            this.listView1 = new System.Windows.Forms.ListView();
            this.Libellé = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ContactID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lbltop2 = new System.Windows.Forms.Label();
            this.tbxRecherche = new System.Windows.Forms.TextBox();
            this.btnRecherche = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.LStock = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbltop
            // 
            this.lbltop.AutoSize = true;
            this.lbltop.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltop.Location = new System.Drawing.Point(320, 18);
            this.lbltop.Name = "lbltop";
            this.lbltop.Size = new System.Drawing.Size(386, 29);
            this.lbltop.TabIndex = 19;
            this.lbltop.Text = "Gestion du matériel Médicalerte";
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.BackgroundImage = global::ImportSosGeneve.Properties.Resources.exit;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.ForeColor = System.Drawing.Color.CadetBlue;
            this.btnExit.Location = new System.Drawing.Point(1161, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(69, 70);
            this.btnExit.TabIndex = 20;
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnExit);
            this.splitContainer1.Panel1.Controls.Add(this.lbltop);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer1.Size = new System.Drawing.Size(1244, 689);
            this.splitContainer1.SplitterDistance = 87;
            this.splitContainer1.TabIndex = 21;
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
            this.splitContainer3.Panel1.Controls.Add(this.label5);
            this.splitContainer3.Panel1.Controls.Add(this.label2);
            this.splitContainer3.Panel1.Controls.Add(this.label1);
            this.splitContainer3.Panel1.Controls.Add(this.lblInfoVIDmedaillon);
            this.splitContainer3.Panel1.Controls.Add(this.tbxVIDm);
            this.splitContainer3.Panel1.Controls.Add(this.lblVIDm);
            this.splitContainer3.Panel1.Controls.Add(this.lbl0003);
            this.splitContainer3.Panel1.Controls.Add(this.label3);
            this.splitContainer3.Panel1.Controls.Add(this.btnModifier);
            this.splitContainer3.Panel1.Controls.Add(this.btnAjouter);
            this.splitContainer3.Panel1.Controls.Add(this.cbxDateHS);
            this.splitContainer3.Panel1.Controls.Add(this.tbxPrixAchat);
            this.splitContainer3.Panel1.Controls.Add(this.cbTypeTarif);
            this.splitContainer3.Panel1.Controls.Add(this.tbxTel);
            this.splitContainer3.Panel1.Controls.Add(this.tbxContactID);
            this.splitContainer3.Panel1.Controls.Add(this.tbxVID);
            this.splitContainer3.Panel1.Controls.Add(this.cbLibelle);
            this.splitContainer3.Panel1.Controls.Add(this.lblPrixAchat);
            this.splitContainer3.Panel1.Controls.Add(this.lblTypeTarif);
            this.splitContainer3.Panel1.Controls.Add(this.lblTel);
            this.splitContainer3.Panel1.Controls.Add(this.lblContactID);
            this.splitContainer3.Panel1.Controls.Add(this.lblVID);
            this.splitContainer3.Panel1.Controls.Add(this.lblLibelle);
            this.splitContainer3.Panel1.Controls.Add(this.btnValider);
            this.splitContainer3.Panel1.Controls.Add(this.btnAnnuler);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer3.Size = new System.Drawing.Size(1244, 598);
            this.splitContainer3.SplitterDistance = 560;
            this.splitContainer3.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(300, 289);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(173, 16);
            this.label2.TabIndex = 24;
            this.label2.Text = "+33 et (Uniquement boitiers)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(261, 204);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "(Uniquement boitiers et médaillons)";
            // 
            // lblInfoVIDmedaillon
            // 
            this.lblInfoVIDmedaillon.AutoSize = true;
            this.lblInfoVIDmedaillon.Location = new System.Drawing.Point(228, 378);
            this.lblInfoVIDmedaillon.Name = "lblInfoVIDmedaillon";
            this.lblInfoVIDmedaillon.Size = new System.Drawing.Size(322, 16);
            this.lblInfoVIDmedaillon.TabIndex = 22;
            this.lblInfoVIDmedaillon.Text = "(Uniquement lors de l\'ajout d\'un boitier) et sans le tiret";
            // 
            // tbxVIDm
            // 
            this.tbxVIDm.Enabled = false;
            this.tbxVIDm.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxVIDm.Location = new System.Drawing.Point(263, 351);
            this.tbxVIDm.Name = "tbxVIDm";
            this.tbxVIDm.Size = new System.Drawing.Size(200, 27);
            this.tbxVIDm.TabIndex = 7;
            // 
            // lblVIDm
            // 
            this.lblVIDm.AutoSize = true;
            this.lblVIDm.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVIDm.Location = new System.Drawing.Point(301, 327);
            this.lblVIDm.Name = "lblVIDm";
            this.lblVIDm.Size = new System.Drawing.Size(121, 20);
            this.lblVIDm.TabIndex = 20;
            this.lblVIDm.Text = "VID Médaillon";
            // 
            // lbl0003
            // 
            this.lbl0003.AutoSize = true;
            this.lbl0003.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl0003.Location = new System.Drawing.Point(260, 177);
            this.lbl0003.Name = "lbl0003";
            this.lbl0003.Size = new System.Drawing.Size(50, 22);
            this.lbl0003.TabIndex = 19;
            this.lbl0003.Text = "0003";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(152, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(188, 25);
            this.label3.TabIndex = 18;
            this.label3.Text = "Infos du Matériel";
            // 
            // btnModifier
            // 
            this.btnModifier.BackColor = System.Drawing.Color.Transparent;
            this.btnModifier.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnModifier.FlatAppearance.BorderSize = 0;
            this.btnModifier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModifier.ForeColor = System.Drawing.Color.Transparent;
            this.btnModifier.ImageIndex = 2;
            this.btnModifier.ImageList = this.imageList1;
            this.btnModifier.Location = new System.Drawing.Point(228, 451);
            this.btnModifier.Name = "btnModifier";
            this.btnModifier.Size = new System.Drawing.Size(73, 70);
            this.btnModifier.TabIndex = 16;
            this.btnModifier.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnModifier.UseVisualStyleBackColor = false;
            this.btnModifier.Click += new System.EventHandler(this.btnModifier_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.CadetBlue;
            this.imageList1.Images.SetKeyName(0, "bajout.png");
            this.imageList1.Images.SetKeyName(1, "bondCancel.png");
            this.imageList1.Images.SetKeyName(2, "boutonsModifier.png");
            this.imageList1.Images.SetKeyName(3, "brondValider.png");
            this.imageList1.Images.SetKeyName(4, "exit.png");
            this.imageList1.Images.SetKeyName(5, "boutonjumelles.png");
            // 
            // btnAjouter
            // 
            this.btnAjouter.BackColor = System.Drawing.Color.Transparent;
            this.btnAjouter.FlatAppearance.BorderSize = 0;
            this.btnAjouter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAjouter.ForeColor = System.Drawing.Color.Transparent;
            this.btnAjouter.ImageIndex = 0;
            this.btnAjouter.ImageList = this.imageList1;
            this.btnAjouter.Location = new System.Drawing.Point(105, 451);
            this.btnAjouter.Name = "btnAjouter";
            this.btnAjouter.Size = new System.Drawing.Size(69, 70);
            this.btnAjouter.TabIndex = 14;
            this.btnAjouter.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAjouter.UseVisualStyleBackColor = false;
            this.btnAjouter.Click += new System.EventHandler(this.btnAjouter_Click);
            // 
            // cbxDateHS
            // 
            this.cbxDateHS.AutoSize = true;
            this.cbxDateHS.Enabled = false;
            this.cbxDateHS.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxDateHS.Location = new System.Drawing.Point(52, 327);
            this.cbxDateHS.Name = "cbxDateHS";
            this.cbxDateHS.Size = new System.Drawing.Size(122, 24);
            this.cbxDateHS.TabIndex = 13;
            this.cbxDateHS.Text = "Materiel HS";
            this.cbxDateHS.UseVisualStyleBackColor = true;
            this.cbxDateHS.CheckedChanged += new System.EventHandler(this.cbxDateHS_CheckedChanged);
            // 
            // tbxPrixAchat
            // 
            this.tbxPrixAchat.Enabled = false;
            this.tbxPrixAchat.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxPrixAchat.Location = new System.Drawing.Point(41, 262);
            this.tbxPrixAchat.Name = "tbxPrixAchat";
            this.tbxPrixAchat.Size = new System.Drawing.Size(103, 27);
            this.tbxPrixAchat.TabIndex = 5;
            // 
            // cbTypeTarif
            // 
            this.cbTypeTarif.Enabled = false;
            this.cbTypeTarif.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTypeTarif.FormattingEnabled = true;
            this.cbTypeTarif.Location = new System.Drawing.Point(41, 175);
            this.cbTypeTarif.Name = "cbTypeTarif";
            this.cbTypeTarif.Size = new System.Drawing.Size(120, 28);
            this.cbTypeTarif.TabIndex = 3;
            this.cbTypeTarif.DropDown += new System.EventHandler(this.cbTypeTarif_DropDown);
            // 
            // tbxTel
            // 
            this.tbxTel.Enabled = false;
            this.tbxTel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxTel.Location = new System.Drawing.Point(241, 262);
            this.tbxTel.Name = "tbxTel";
            this.tbxTel.Size = new System.Drawing.Size(222, 27);
            this.tbxTel.TabIndex = 6;
            // 
            // tbxContactID
            // 
            this.tbxContactID.Enabled = false;
            this.tbxContactID.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxContactID.Location = new System.Drawing.Point(311, 175);
            this.tbxContactID.Name = "tbxContactID";
            this.tbxContactID.Size = new System.Drawing.Size(120, 27);
            this.tbxContactID.TabIndex = 4;
            // 
            // tbxVID
            // 
            this.tbxVID.Enabled = false;
            this.tbxVID.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxVID.Location = new System.Drawing.Point(9, 81);
            this.tbxVID.Name = "tbxVID";
            this.tbxVID.Size = new System.Drawing.Size(199, 27);
            this.tbxVID.TabIndex = 1;
            // 
            // cbLibelle
            // 
            this.cbLibelle.Enabled = false;
            this.cbLibelle.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbLibelle.FormattingEnabled = true;
            this.cbLibelle.Location = new System.Drawing.Point(243, 81);
            this.cbLibelle.Name = "cbLibelle";
            this.cbLibelle.Size = new System.Drawing.Size(200, 28);
            this.cbLibelle.TabIndex = 2;
            this.cbLibelle.DropDown += new System.EventHandler(this.cbLibelle_DropDown);
            this.cbLibelle.TextChanged += new System.EventHandler(this.cbLibelle_TextChanged);
            // 
            // lblPrixAchat
            // 
            this.lblPrixAchat.AutoSize = true;
            this.lblPrixAchat.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrixAchat.Location = new System.Drawing.Point(54, 239);
            this.lblPrixAchat.Name = "lblPrixAchat";
            this.lblPrixAchat.Size = new System.Drawing.Size(97, 20);
            this.lblPrixAchat.TabIndex = 5;
            this.lblPrixAchat.Text = "Prix Achat*";
            // 
            // lblTypeTarif
            // 
            this.lblTypeTarif.AutoSize = true;
            this.lblTypeTarif.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTypeTarif.Location = new System.Drawing.Point(65, 151);
            this.lblTypeTarif.Name = "lblTypeTarif";
            this.lblTypeTarif.Size = new System.Drawing.Size(95, 20);
            this.lblTypeTarif.TabIndex = 4;
            this.lblTypeTarif.Text = "Type Tarif*";
            // 
            // lblTel
            // 
            this.lblTel.AutoSize = true;
            this.lblTel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTel.Location = new System.Drawing.Point(319, 239);
            this.lblTel.Name = "lblTel";
            this.lblTel.Size = new System.Drawing.Size(95, 20);
            this.lblTel.TabIndex = 3;
            this.lblTel.Text = "N°Tel SIM*";
            // 
            // lblContactID
            // 
            this.lblContactID.AutoSize = true;
            this.lblContactID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContactID.Location = new System.Drawing.Point(284, 151);
            this.lblContactID.Name = "lblContactID";
            this.lblContactID.Size = new System.Drawing.Size(103, 20);
            this.lblContactID.TabIndex = 2;
            this.lblContactID.Text = "Contact ID*";
            // 
            // lblVID
            // 
            this.lblVID.AutoSize = true;
            this.lblVID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVID.Location = new System.Drawing.Point(88, 58);
            this.lblVID.Name = "lblVID";
            this.lblVID.Size = new System.Drawing.Size(47, 20);
            this.lblVID.TabIndex = 1;
            this.lblVID.Text = "VID*";
            // 
            // lblLibelle
            // 
            this.lblLibelle.AutoSize = true;
            this.lblLibelle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLibelle.Location = new System.Drawing.Point(319, 57);
            this.lblLibelle.Name = "lblLibelle";
            this.lblLibelle.Size = new System.Drawing.Size(68, 20);
            this.lblLibelle.TabIndex = 0;
            this.lblLibelle.Text = "Libellé*";
            // 
            // btnValider
            // 
            this.btnValider.BackColor = System.Drawing.Color.Transparent;
            this.btnValider.FlatAppearance.BorderSize = 0;
            this.btnValider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnValider.ForeColor = System.Drawing.Color.Transparent;
            this.btnValider.ImageIndex = 3;
            this.btnValider.ImageList = this.imageList1;
            this.btnValider.Location = new System.Drawing.Point(105, 451);
            this.btnValider.Name = "btnValider";
            this.btnValider.Size = new System.Drawing.Size(69, 70);
            this.btnValider.TabIndex = 15;
            this.btnValider.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnValider.UseVisualStyleBackColor = false;
            this.btnValider.Click += new System.EventHandler(this.btnValider_Click);
            // 
            // btnAnnuler
            // 
            this.btnAnnuler.BackColor = System.Drawing.Color.Transparent;
            this.btnAnnuler.FlatAppearance.BorderSize = 0;
            this.btnAnnuler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnnuler.ForeColor = System.Drawing.Color.Transparent;
            this.btnAnnuler.ImageIndex = 1;
            this.btnAnnuler.ImageList = this.imageList1;
            this.btnAnnuler.Location = new System.Drawing.Point(228, 451);
            this.btnAnnuler.Name = "btnAnnuler";
            this.btnAnnuler.Size = new System.Drawing.Size(73, 70);
            this.btnAnnuler.TabIndex = 17;
            this.btnAnnuler.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAnnuler.UseVisualStyleBackColor = false;
            this.btnAnnuler.Click += new System.EventHandler(this.btnAnnuler_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.rBContactID);
            this.splitContainer2.Panel1.Controls.Add(this.rBLibelle);
            this.splitContainer2.Panel1.Controls.Add(this.rBVID);
            this.splitContainer2.Panel1.Controls.Add(this.listView1);
            this.splitContainer2.Panel1.Controls.Add(this.lbltop2);
            this.splitContainer2.Panel1.Controls.Add(this.tbxRecherche);
            this.splitContainer2.Panel1.Controls.Add(this.btnRecherche);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.label4);
            this.splitContainer2.Panel2.Controls.Add(this.LStock);
            this.splitContainer2.Size = new System.Drawing.Size(680, 598);
            this.splitContainer2.SplitterDistance = 439;
            this.splitContainer2.TabIndex = 20;
            // 
            // rBContactID
            // 
            this.rBContactID.AutoSize = true;
            this.rBContactID.BackColor = System.Drawing.Color.Transparent;
            this.rBContactID.Checked = true;
            this.rBContactID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rBContactID.Location = new System.Drawing.Point(32, 57);
            this.rBContactID.Name = "rBContactID";
            this.rBContactID.Size = new System.Drawing.Size(87, 20);
            this.rBContactID.TabIndex = 24;
            this.rBContactID.TabStop = true;
            this.rBContactID.Text = "Contact ID";
            this.rBContactID.UseVisualStyleBackColor = false;
            // 
            // rBLibelle
            // 
            this.rBLibelle.AutoSize = true;
            this.rBLibelle.BackColor = System.Drawing.Color.Transparent;
            this.rBLibelle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rBLibelle.Location = new System.Drawing.Point(138, 57);
            this.rBLibelle.Name = "rBLibelle";
            this.rBLibelle.Size = new System.Drawing.Size(66, 20);
            this.rBLibelle.TabIndex = 23;
            this.rBLibelle.Text = "Libellé";
            this.rBLibelle.UseVisualStyleBackColor = false;
            // 
            // rBVID
            // 
            this.rBVID.AutoSize = true;
            this.rBVID.BackColor = System.Drawing.Color.Transparent;
            this.rBVID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rBVID.Location = new System.Drawing.Point(236, 56);
            this.rBVID.Name = "rBVID";
            this.rBVID.Size = new System.Drawing.Size(48, 20);
            this.rBVID.TabIndex = 22;
            this.rBVID.Text = "VID";
            this.rBVID.UseVisualStyleBackColor = false;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Libellé,
            this.ContactID});
            this.listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(3, 159);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(430, 437);
            this.listView1.TabIndex = 21;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // Libellé
            // 
            this.Libellé.Text = "Libellé";
            this.Libellé.Width = 170;
            // 
            // ContactID
            // 
            this.ContactID.Text = "ContactID";
            this.ContactID.Width = 115;
            // 
            // lbltop2
            // 
            this.lbltop2.AutoSize = true;
            this.lbltop2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltop2.Location = new System.Drawing.Point(70, 12);
            this.lbltop2.Name = "lbltop2";
            this.lbltop2.Size = new System.Drawing.Size(249, 25);
            this.lbltop2.TabIndex = 20;
            this.lbltop2.Text = "Recherche de materiel";
            // 
            // tbxRecherche
            // 
            this.tbxRecherche.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxRecherche.Location = new System.Drawing.Point(32, 90);
            this.tbxRecherche.Name = "tbxRecherche";
            this.tbxRecherche.Size = new System.Drawing.Size(226, 27);
            this.tbxRecherche.TabIndex = 9;
            this.tbxRecherche.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbxRecherche_KeyPress);
            // 
            // btnRecherche
            // 
            this.btnRecherche.BackColor = System.Drawing.Color.Transparent;
            this.btnRecherche.FlatAppearance.BorderSize = 0;
            this.btnRecherche.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRecherche.ForeColor = System.Drawing.Color.Transparent;
            this.btnRecherche.ImageIndex = 5;
            this.btnRecherche.ImageList = this.imageList1;
            this.btnRecherche.Location = new System.Drawing.Point(323, 74);
            this.btnRecherche.Name = "btnRecherche";
            this.btnRecherche.Size = new System.Drawing.Size(68, 61);
            this.btnRecherche.TabIndex = 19;
            this.btnRecherche.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRecherche.UseVisualStyleBackColor = false;
            this.btnRecherche.Click += new System.EventHandler(this.btnRecherche_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(13, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(189, 25);
            this.label4.TabIndex = 19;
            this.label4.Text = "Stock Disponible";
            // 
            // LStock
            // 
            this.LStock.AutoSize = true;
            this.LStock.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LStock.Location = new System.Drawing.Point(14, 115);
            this.LStock.Name = "LStock";
            this.LStock.Size = new System.Drawing.Size(61, 20);
            this.LStock.TabIndex = 2;
            this.LStock.Text = "Libellé";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(38, 558);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(140, 16);
            this.label5.TabIndex = 25;
            this.label5.Text = "* Champs obligatoires";
            // 
            // frmMateriel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.ClientSize = new System.Drawing.Size(1244, 689);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmMateriel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestion du matériel Médicalerte";
            this.Load += new System.EventHandler(this.frmMateriel_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
       
        private System.Windows.Forms.Label lbltop;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblInfoVIDmedaillon;
        private System.Windows.Forms.TextBox tbxVIDm;
        private System.Windows.Forms.Label lblVIDm;
        private System.Windows.Forms.Label lbl0003;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnModifier;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button btnAjouter;
        private System.Windows.Forms.CheckBox cbxDateHS;
        private System.Windows.Forms.TextBox tbxPrixAchat;
        private System.Windows.Forms.ComboBox cbTypeTarif;
        private System.Windows.Forms.TextBox tbxTel;
        private System.Windows.Forms.TextBox tbxContactID;
        private System.Windows.Forms.TextBox tbxVID;
        private System.Windows.Forms.ComboBox cbLibelle;
        private System.Windows.Forms.Label lblPrixAchat;
        private System.Windows.Forms.Label lblTypeTarif;
        private System.Windows.Forms.Label lblTel;
        private System.Windows.Forms.Label lblContactID;
        private System.Windows.Forms.Label lblVID;
        private System.Windows.Forms.Label lblLibelle;
        private System.Windows.Forms.Button btnValider;
        private System.Windows.Forms.Button btnAnnuler;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.RadioButton rBContactID;
        private System.Windows.Forms.RadioButton rBLibelle;
        private System.Windows.Forms.RadioButton rBVID;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader Libellé;
        private System.Windows.Forms.ColumnHeader ContactID;
        private System.Windows.Forms.Label lbltop2;
        private System.Windows.Forms.TextBox tbxRecherche;
        private System.Windows.Forms.Button btnRecherche;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label LStock;
        private System.Windows.Forms.Label label5;
    }
}