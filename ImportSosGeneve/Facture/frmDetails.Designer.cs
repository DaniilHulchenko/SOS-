namespace ImportSosGeneve.Facture
{
    partial class frmDetails
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDetails));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbTypeFacture = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbDestinataire = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtFin = new System.Windows.Forms.DateTimePicker();
            this.dtDebut = new System.Windows.Forms.DateTimePicker();
            this.dtbDateNaissance = new SosMedecins.Controls.sosDateBox();
            this.lblDateNaissance = new System.Windows.Forms.Label();
            this.lblPrenom = new System.Windows.Forms.Label();
            this.LblNom = new System.Windows.Forms.Label();
            this.txtPrenom = new System.Windows.Forms.TextBox();
            this.txtNom = new System.Windows.Forms.TextBox();
            this.grpListe = new System.Windows.Forms.GroupBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblSolde = new System.Windows.Forms.Label();
            this.txtSolde = new System.Windows.Forms.TextBox();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.lblNbFacture = new System.Windows.Forms.Label();
            this.dgvListe = new System.Windows.Forms.DataGridView();
            this.grpDetails = new System.Windows.Forms.GroupBox();
            this.dgvDetail = new System.Windows.Forms.DataGridView();
            this.txtTotalDetail = new System.Windows.Forms.TextBox();
            this.txtSoldeDetail = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ttpPrincipal = new System.Windows.Forms.ToolTip(this.components);
            this.btnModificationSolde = new System.Windows.Forms.Button();
            this.btnSupprimerDetail = new System.Windows.Forms.Button();
            this.btnModifierDetail = new System.Windows.Forms.Button();
            this.btnPaiement = new System.Windows.Forms.Button();
            this.btnDecompte = new System.Windows.Forms.Button();
            this.bQuitter = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.grpListe.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListe)).BeginInit();
            this.grpDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cbTypeFacture);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cbDestinataire);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dtFin);
            this.groupBox1.Controls.Add(this.dtDebut);
            this.groupBox1.Controls.Add(this.dtbDateNaissance);
            this.groupBox1.Controls.Add(this.lblDateNaissance);
            this.groupBox1.Controls.Add(this.lblPrenom);
            this.groupBox1.Controls.Add(this.LblNom);
            this.groupBox1.Controls.Add(this.btnDecompte);
            this.groupBox1.Controls.Add(this.txtPrenom);
            this.groupBox1.Controls.Add(this.txtNom);
            this.groupBox1.Location = new System.Drawing.Point(40, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1125, 126);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(744, 71);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(102, 16);
            this.label7.TabIndex = 16;
            this.label7.Text = "Type de facture";
            // 
            // cbTypeFacture
            // 
            this.cbTypeFacture.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTypeFacture.FormattingEnabled = true;
            this.cbTypeFacture.Items.AddRange(new object[] {
            "Impayees",
            "Payees",
            "Toutes"});
            this.cbTypeFacture.Location = new System.Drawing.Point(740, 90);
            this.cbTypeFacture.Name = "cbTypeFacture";
            this.cbTypeFacture.Size = new System.Drawing.Size(121, 24);
            this.cbTypeFacture.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(588, 71);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(132, 16);
            this.label6.TabIndex = 14;
            this.label6.Text = "Type de destinataire";
            // 
            // cbDestinataire
            // 
            this.cbDestinataire.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDestinataire.FormattingEnabled = true;
            this.cbDestinataire.Items.AddRange(new object[] {
            "Privé",
            "Assurance",
            "Tiers"});
            this.cbDestinataire.Location = new System.Drawing.Point(591, 90);
            this.cbDestinataire.Name = "cbDestinataire";
            this.cbDestinataire.Size = new System.Drawing.Size(129, 24);
            this.cbDestinataire.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(1011, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 16);
            this.label5.TabIndex = 12;
            this.label5.Text = "Etat Décompte";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(780, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 16);
            this.label4.TabIndex = 11;
            this.label4.Text = "Date Fin";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(629, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "Date Debut ";
            // 
            // dtFin
            // 
            this.dtFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFin.Location = new System.Drawing.Point(754, 37);
            this.dtFin.Name = "dtFin";
            this.dtFin.Size = new System.Drawing.Size(106, 22);
            this.dtFin.TabIndex = 9;
            // 
            // dtDebut
            // 
            this.dtDebut.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtDebut.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtDebut.Location = new System.Drawing.Point(620, 37);
            this.dtDebut.Name = "dtDebut";
            this.dtDebut.Size = new System.Drawing.Size(100, 22);
            this.dtDebut.TabIndex = 8;
            // 
            // dtbDateNaissance
            // 
            this.dtbDateNaissance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtbDateNaissance.Location = new System.Drawing.Point(439, 39);
            this.dtbDateNaissance.Mask = "00/00/0000";
            this.dtbDateNaissance.Name = "dtbDateNaissance";
            this.dtbDateNaissance.ReadOnly = true;
            this.dtbDateNaissance.Size = new System.Drawing.Size(146, 22);
            this.dtbDateNaissance.TabIndex = 7;
            this.dtbDateNaissance.ValidatingType = typeof(System.DateTime);
            this.dtbDateNaissance.Value = "";
            // 
            // lblDateNaissance
            // 
            this.lblDateNaissance.AutoSize = true;
            this.lblDateNaissance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateNaissance.Location = new System.Drawing.Point(448, 20);
            this.lblDateNaissance.Name = "lblDateNaissance";
            this.lblDateNaissance.Size = new System.Drawing.Size(124, 16);
            this.lblDateNaissance.TabIndex = 6;
            this.lblDateNaissance.Text = "Date de Naissance";
            // 
            // lblPrenom
            // 
            this.lblPrenom.AutoSize = true;
            this.lblPrenom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrenom.Location = new System.Drawing.Point(248, 21);
            this.lblPrenom.Name = "lblPrenom";
            this.lblPrenom.Size = new System.Drawing.Size(55, 16);
            this.lblPrenom.TabIndex = 5;
            this.lblPrenom.Text = "Prenom";
            // 
            // LblNom
            // 
            this.LblNom.AutoSize = true;
            this.LblNom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblNom.Location = new System.Drawing.Point(26, 20);
            this.LblNom.Name = "LblNom";
            this.LblNom.Size = new System.Drawing.Size(37, 16);
            this.LblNom.TabIndex = 4;
            this.LblNom.Text = "Nom";
            // 
            // txtPrenom
            // 
            this.txtPrenom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrenom.Location = new System.Drawing.Point(245, 39);
            this.txtPrenom.Name = "txtPrenom";
            this.txtPrenom.ReadOnly = true;
            this.txtPrenom.Size = new System.Drawing.Size(167, 22);
            this.txtPrenom.TabIndex = 1;
            // 
            // txtNom
            // 
            this.txtNom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNom.Location = new System.Drawing.Point(29, 39);
            this.txtNom.Name = "txtNom";
            this.txtNom.ReadOnly = true;
            this.txtNom.Size = new System.Drawing.Size(197, 22);
            this.txtNom.TabIndex = 0;
            // 
            // grpListe
            // 
            this.grpListe.Controls.Add(this.lblTotal);
            this.grpListe.Controls.Add(this.lblSolde);
            this.grpListe.Controls.Add(this.txtSolde);
            this.grpListe.Controls.Add(this.txtTotal);
            this.grpListe.Controls.Add(this.lblNbFacture);
            this.grpListe.Controls.Add(this.dgvListe);
            this.grpListe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpListe.Location = new System.Drawing.Point(40, 155);
            this.grpListe.Name = "grpListe";
            this.grpListe.Size = new System.Drawing.Size(884, 270);
            this.grpListe.TabIndex = 1;
            this.grpListe.TabStop = false;
            this.grpListe.Text = "Liste des Factures";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(413, 238);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(39, 16);
            this.lblTotal.TabIndex = 7;
            this.lblTotal.Text = "Total";
            // 
            // lblSolde
            // 
            this.lblSolde.AutoSize = true;
            this.lblSolde.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSolde.Location = new System.Drawing.Point(664, 238);
            this.lblSolde.Name = "lblSolde";
            this.lblSolde.Size = new System.Drawing.Size(44, 16);
            this.lblSolde.TabIndex = 6;
            this.lblSolde.Text = "Solde";
            // 
            // txtSolde
            // 
            this.txtSolde.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSolde.Location = new System.Drawing.Point(725, 235);
            this.txtSolde.Name = "txtSolde";
            this.txtSolde.ReadOnly = true;
            this.txtSolde.Size = new System.Drawing.Size(135, 22);
            this.txtSolde.TabIndex = 5;
            // 
            // txtTotal
            // 
            this.txtTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotal.Location = new System.Drawing.Point(468, 235);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(129, 22);
            this.txtTotal.TabIndex = 4;
            // 
            // lblNbFacture
            // 
            this.lblNbFacture.Location = new System.Drawing.Point(10, 235);
            this.lblNbFacture.Name = "lblNbFacture";
            this.lblNbFacture.Size = new System.Drawing.Size(380, 26);
            this.lblNbFacture.TabIndex = 3;
            this.lblNbFacture.Text = "label1";
            // 
            // dgvListe
            // 
            this.dgvListe.AllowUserToAddRows = false;
            this.dgvListe.AllowUserToDeleteRows = false;
            this.dgvListe.AllowUserToResizeRows = false;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dgvListe.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle13;
            this.dgvListe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListe.Location = new System.Drawing.Point(6, 19);
            this.dgvListe.MultiSelect = false;
            this.dgvListe.Name = "dgvListe";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.PowderBlue;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvListe.RowHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.dgvListe.RowHeadersVisible = false;
            this.dgvListe.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvListe.Size = new System.Drawing.Size(854, 209);
            this.dgvListe.TabIndex = 2;
            this.dgvListe.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvListe_CellContentClick_1);
            this.dgvListe.SelectionChanged += new System.EventHandler(this.dgvDetail_SelectionChanged);
            // 
            // grpDetails
            // 
            this.grpDetails.Controls.Add(this.dgvDetail);
            this.grpDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDetails.Location = new System.Drawing.Point(40, 431);
            this.grpDetails.Name = "grpDetails";
            this.grpDetails.Size = new System.Drawing.Size(884, 256);
            this.grpDetails.TabIndex = 2;
            this.grpDetails.TabStop = false;
            this.grpDetails.Text = "Details";
            // 
            // dgvDetail
            // 
            this.dgvDetail.AllowUserToAddRows = false;
            this.dgvDetail.AllowUserToDeleteRows = false;
            this.dgvDetail.AllowUserToResizeColumns = false;
            this.dgvDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetail.Location = new System.Drawing.Point(6, 19);
            this.dgvDetail.MultiSelect = false;
            this.dgvDetail.Name = "dgvDetail";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.Bisque;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDetail.RowHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this.dgvDetail.RowHeadersVisible = false;
            this.dgvDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetail.Size = new System.Drawing.Size(857, 223);
            this.dgvDetail.TabIndex = 2;
            this.dgvDetail.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDetail_CellContentDoubleClick);
            this.dgvDetail.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvDetail_CellFormatting);
            this.dgvDetail.SelectionChanged += new System.EventHandler(this.dgvDetail_SelectionChanged);
            // 
            // txtTotalDetail
            // 
            this.txtTotalDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalDetail.Location = new System.Drawing.Point(508, 697);
            this.txtTotalDetail.Name = "txtTotalDetail";
            this.txtTotalDetail.ReadOnly = true;
            this.txtTotalDetail.Size = new System.Drawing.Size(129, 22);
            this.txtTotalDetail.TabIndex = 8;
            // 
            // txtSoldeDetail
            // 
            this.txtSoldeDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSoldeDetail.Location = new System.Drawing.Point(765, 697);
            this.txtSoldeDetail.Name = "txtSoldeDetail";
            this.txtSoldeDetail.ReadOnly = true;
            this.txtSoldeDetail.Size = new System.Drawing.Size(135, 22);
            this.txtSoldeDetail.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(704, 700);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "Solde";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(453, 700);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 16);
            this.label2.TabIndex = 8;
            this.label2.Text = "Total";
            // 
            // btnModificationSolde
            // 
            this.btnModificationSolde.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnModificationSolde.BackgroundImage")));
            this.btnModificationSolde.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnModificationSolde.Enabled = false;
            this.btnModificationSolde.Location = new System.Drawing.Point(315, 693);
            this.btnModificationSolde.Name = "btnModificationSolde";
            this.btnModificationSolde.Size = new System.Drawing.Size(44, 42);
            this.btnModificationSolde.TabIndex = 13;
            this.ttpPrincipal.SetToolTip(this.btnModificationSolde, "Modification du solde");
            this.btnModificationSolde.UseVisualStyleBackColor = true;
            this.btnModificationSolde.Click += new System.EventHandler(this.btnModificationSolde_Click);
            // 
            // btnSupprimerDetail
            // 
            this.btnSupprimerDetail.BackgroundImage = global::ImportSosGeneve.Properties.Resources.Poubelle;
            this.btnSupprimerDetail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSupprimerDetail.Enabled = false;
            this.btnSupprimerDetail.Location = new System.Drawing.Point(168, 693);
            this.btnSupprimerDetail.Name = "btnSupprimerDetail";
            this.btnSupprimerDetail.Size = new System.Drawing.Size(44, 42);
            this.btnSupprimerDetail.TabIndex = 12;
            this.ttpPrincipal.SetToolTip(this.btnSupprimerDetail, "Suppression de la ligne");
            this.btnSupprimerDetail.UseVisualStyleBackColor = true;
            this.btnSupprimerDetail.Click += new System.EventHandler(this.btnSupprimerDetail_Click);
            // 
            // btnModifierDetail
            // 
            this.btnModifierDetail.Enabled = false;
            this.btnModifierDetail.Image = ((System.Drawing.Image)(resources.GetObject("btnModifierDetail.Image")));
            this.btnModifierDetail.Location = new System.Drawing.Point(104, 693);
            this.btnModifierDetail.Name = "btnModifierDetail";
            this.btnModifierDetail.Size = new System.Drawing.Size(44, 42);
            this.btnModifierDetail.TabIndex = 11;
            this.ttpPrincipal.SetToolTip(this.btnModifierDetail, "Modifier le détail");
            this.btnModifierDetail.UseVisualStyleBackColor = true;
            this.btnModifierDetail.Click += new System.EventHandler(this.btnModifierDetail_Click);
            // 
            // btnPaiement
            // 
            this.btnPaiement.Image = global::ImportSosGeneve.Properties.Resources.plus;
            this.btnPaiement.Location = new System.Drawing.Point(40, 693);
            this.btnPaiement.Name = "btnPaiement";
            this.btnPaiement.Size = new System.Drawing.Size(44, 42);
            this.btnPaiement.TabIndex = 10;
            this.ttpPrincipal.SetToolTip(this.btnPaiement, "Ajouter un paiement");
            this.btnPaiement.UseVisualStyleBackColor = true;
            this.btnPaiement.Click += new System.EventHandler(this.btnPaiement_Click);
            // 
            // btnDecompte
            // 
            this.btnDecompte.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDecompte.BackgroundImage")));
            this.btnDecompte.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDecompte.Location = new System.Drawing.Point(1014, 16);
            this.btnDecompte.Name = "btnDecompte";
            this.btnDecompte.Size = new System.Drawing.Size(76, 51);
            this.btnDecompte.TabIndex = 3;
            this.btnDecompte.UseVisualStyleBackColor = true;
            this.btnDecompte.Click += new System.EventHandler(this.btnDecompte_Click);
            // 
            // bQuitter
            // 
            this.bQuitter.BackgroundImage = global::ImportSosGeneve.Properties.Resources.exit;
            this.bQuitter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bQuitter.FlatAppearance.BorderSize = 0;
            this.bQuitter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bQuitter.Location = new System.Drawing.Point(1031, 655);
            this.bQuitter.Name = "bQuitter";
            this.bQuitter.Size = new System.Drawing.Size(64, 64);
            this.bQuitter.TabIndex = 14;
            this.bQuitter.UseVisualStyleBackColor = true;
            this.bQuitter.Click += new System.EventHandler(this.bQuitter_Click);
            // 
            // frmDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.ClientSize = new System.Drawing.Size(1177, 742);
            this.Controls.Add(this.bQuitter);
            this.Controls.Add(this.btnModificationSolde);
            this.Controls.Add(this.btnSupprimerDetail);
            this.Controls.Add(this.btnModifierDetail);
            this.Controls.Add(this.btnPaiement);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSoldeDetail);
            this.Controls.Add(this.txtTotalDetail);
            this.Controls.Add(this.grpDetails);
            this.Controls.Add(this.grpListe);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Details Factures";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpListe.ResumeLayout(false);
            this.grpListe.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListe)).EndInit();
            this.grpDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox grpListe;
        private System.Windows.Forms.GroupBox grpDetails;
        private System.Windows.Forms.Button btnDecompte;
        private System.Windows.Forms.TextBox txtPrenom;
        private System.Windows.Forms.TextBox txtNom;
        private System.Windows.Forms.Label lblDateNaissance;
        private System.Windows.Forms.Label lblPrenom;
        private System.Windows.Forms.Label LblNom;
        private SosMedecins.Controls.sosDateBox dtbDateNaissance;
        public System.Windows.Forms.DataGridView dgvListe;
        private System.Windows.Forms.Label lblNbFacture;
        private System.Windows.Forms.TextBox txtSolde;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblSolde;
        public System.Windows.Forms.DataGridView dgvDetail;
        private System.Windows.Forms.TextBox txtTotalDetail;
        private System.Windows.Forms.TextBox txtSoldeDetail;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Button btnPaiement;
        internal System.Windows.Forms.Button btnModifierDetail;
        internal System.Windows.Forms.Button btnSupprimerDetail;
        internal System.Windows.Forms.Button btnModificationSolde;
        public System.Windows.Forms.ToolTip ttpPrincipal;
        private System.Windows.Forms.DateTimePicker dtDebut;
        private System.Windows.Forms.DateTimePicker dtFin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbDestinataire;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbTypeFacture;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button bQuitter;
    }
}