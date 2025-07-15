namespace ImportSosGeneve
{
    partial class frmRelances
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRelances));
            this.cbxImprimantes = new System.Windows.Forms.ComboBox();
            this.LblTexte = new System.Windows.Forms.Label();
            this.grp = new System.Windows.Forms.GroupBox();
            this.lblImprimante = new System.Windows.Forms.Label();
            this.prgTache = new System.Windows.Forms.ProgressBar();
            this.grpParametres = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RB5 = new System.Windows.Forms.RadioButton();
            this.RB4 = new System.Windows.Forms.RadioButton();
            this.RB3 = new System.Windows.Forms.RadioButton();
            this.RB2 = new System.Windows.Forms.RadioButton();
            this.RB1 = new System.Windows.Forms.RadioButton();
            this.RB0 = new System.Windows.Forms.RadioButton();
            this.txtNumFactureUnique = new System.Windows.Forms.TextBox();
            this.lblFactureUnique = new System.Windows.Forms.Label();
            this.btnActualiseFactureUnique = new System.Windows.Forms.Button();
            this.btnFactureActualise = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDateRelance = new System.Windows.Forms.Label();
            this.dtpDateRelance = new System.Windows.Forms.DateTimePicker();
            this.txtNumFacture = new System.Windows.Forms.TextBox();
            this.lblNumFacture = new System.Windows.Forms.Label();
            this.chkIncident = new System.Windows.Forms.CheckBox();
            this.grpProgression = new System.Windows.Forms.GroupBox();
            this.lblProgression = new System.Windows.Forms.Label();
            this.chkApercu = new System.Windows.Forms.CheckBox();
            this.grpImpression = new System.Windows.Forms.GroupBox();
            this.btnFermer = new System.Windows.Forms.Button();
            this.btnValider = new System.Windows.Forms.Button();
            this.grp.SuspendLayout();
            this.grpParametres.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grpProgression.SuspendLayout();
            this.grpImpression.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbxImprimantes
            // 
            this.cbxImprimantes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxImprimantes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxImprimantes.FormattingEnabled = true;
            this.cbxImprimantes.Location = new System.Drawing.Point(90, 42);
            this.cbxImprimantes.Name = "cbxImprimantes";
            this.cbxImprimantes.Size = new System.Drawing.Size(771, 21);
            this.cbxImprimantes.TabIndex = 2;
            // 
            // LblTexte
            // 
            this.LblTexte.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LblTexte.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LblTexte.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTexte.Location = new System.Drawing.Point(6, 16);
            this.LblTexte.Name = "LblTexte";
            this.LblTexte.Size = new System.Drawing.Size(855, 48);
            this.LblTexte.TabIndex = 3;
            this.LblTexte.Text = "label1";
            this.LblTexte.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grp
            // 
            this.grp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grp.Controls.Add(this.LblTexte);
            this.grp.Location = new System.Drawing.Point(12, 334);
            this.grp.Name = "grp";
            this.grp.Size = new System.Drawing.Size(867, 73);
            this.grp.TabIndex = 4;
            this.grp.TabStop = false;
            // 
            // lblImprimante
            // 
            this.lblImprimante.AutoSize = true;
            this.lblImprimante.Location = new System.Drawing.Point(6, 45);
            this.lblImprimante.Name = "lblImprimante";
            this.lblImprimante.Size = new System.Drawing.Size(58, 13);
            this.lblImprimante.TabIndex = 4;
            this.lblImprimante.Text = "Imprimante";
            // 
            // prgTache
            // 
            this.prgTache.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.prgTache.Location = new System.Drawing.Point(6, 47);
            this.prgTache.Name = "prgTache";
            this.prgTache.Size = new System.Drawing.Size(855, 15);
            this.prgTache.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.prgTache.TabIndex = 5;
            // 
            // grpParametres
            // 
            this.grpParametres.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpParametres.Controls.Add(this.groupBox1);
            this.grpParametres.Controls.Add(this.txtNumFactureUnique);
            this.grpParametres.Controls.Add(this.lblFactureUnique);
            this.grpParametres.Controls.Add(this.btnActualiseFactureUnique);
            this.grpParametres.Controls.Add(this.btnFactureActualise);
            this.grpParametres.Controls.Add(this.label1);
            this.grpParametres.Controls.Add(this.lblDateRelance);
            this.grpParametres.Controls.Add(this.dtpDateRelance);
            this.grpParametres.Controls.Add(this.txtNumFacture);
            this.grpParametres.Controls.Add(this.lblNumFacture);
            this.grpParametres.Controls.Add(this.chkIncident);
            this.grpParametres.Location = new System.Drawing.Point(12, 12);
            this.grpParametres.Name = "grpParametres";
            this.grpParametres.Size = new System.Drawing.Size(867, 237);
            this.grpParametres.TabIndex = 7;
            this.grpParametres.TabStop = false;
            this.grpParametres.Text = "Parametres";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RB5);
            this.groupBox1.Controls.Add(this.RB4);
            this.groupBox1.Controls.Add(this.RB3);
            this.groupBox1.Controls.Add(this.RB2);
            this.groupBox1.Controls.Add(this.RB1);
            this.groupBox1.Controls.Add(this.RB0);
            this.groupBox1.Location = new System.Drawing.Point(230, 110);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(380, 114);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Type d\'impressions";
            // 
            // RB5
            // 
            this.RB5.AutoSize = true;
            this.RB5.Location = new System.Drawing.Point(174, 77);
            this.RB5.Name = "RB5";
            this.RB5.Size = new System.Drawing.Size(202, 17);
            this.RB5.TabIndex = 5;
            this.RB5.TabStop = true;
            this.RB5.Text = "Cession non reçue Relance 100% (R)";
            this.RB5.UseVisualStyleBackColor = true;
            // 
            // RB4
            // 
            this.RB4.AutoSize = true;
            this.RB4.Location = new System.Drawing.Point(174, 54);
            this.RB4.Name = "RB4";
            this.RB4.Size = new System.Drawing.Size(164, 17);
            this.RB4.TabIndex = 4;
            this.RB4.TabStop = true;
            this.RB4.Text = "Relance Assurance 100% (R)";
            this.RB4.UseVisualStyleBackColor = true;
            // 
            // RB3
            // 
            this.RB3.AutoSize = true;
            this.RB3.Location = new System.Drawing.Point(174, 31);
            this.RB3.Name = "RB3";
            this.RB3.Size = new System.Drawing.Size(189, 17);
            this.RB3.TabIndex = 3;
            this.RB3.TabStop = true;
            this.RB3.Text = "Relance 100% Patient indélicat (R)";
            this.RB3.UseVisualStyleBackColor = true;
            // 
            // RB2
            // 
            this.RB2.AutoSize = true;
            this.RB2.Location = new System.Drawing.Point(6, 77);
            this.RB2.Name = "RB2";
            this.RB2.Size = new System.Drawing.Size(162, 17);
            this.RB2.TabIndex = 2;
            this.RB2.TabStop = true;
            this.RB2.Text = "Relance Patient en franchise";
            this.RB2.UseVisualStyleBackColor = true;
            // 
            // RB1
            // 
            this.RB1.AutoSize = true;
            this.RB1.Location = new System.Drawing.Point(6, 54);
            this.RB1.Name = "RB1";
            this.RB1.Size = new System.Drawing.Size(124, 17);
            this.RB1.TabIndex = 1;
            this.RB1.TabStop = true;
            this.RB1.Text = "Relance 10% Patient";
            this.RB1.UseVisualStyleBackColor = true;
            // 
            // RB0
            // 
            this.RB0.AutoSize = true;
            this.RB0.Checked = true;
            this.RB0.Location = new System.Drawing.Point(6, 31);
            this.RB0.Name = "RB0";
            this.RB0.Size = new System.Drawing.Size(125, 17);
            this.RB0.TabIndex = 0;
            this.RB0.TabStop = true;
            this.RB0.Text = "Cessions de Créance";
            this.RB0.UseVisualStyleBackColor = true;
            // 
            // txtNumFactureUnique
            // 
            this.txtNumFactureUnique.Enabled = false;
            this.txtNumFactureUnique.Location = new System.Drawing.Point(294, 16);
            this.txtNumFactureUnique.Name = "txtNumFactureUnique";
            this.txtNumFactureUnique.Size = new System.Drawing.Size(117, 20);
            this.txtNumFactureUnique.TabIndex = 18;
            // 
            // lblFactureUnique
            // 
            this.lblFactureUnique.AutoSize = true;
            this.lblFactureUnique.Enabled = false;
            this.lblFactureUnique.Location = new System.Drawing.Point(158, 20);
            this.lblFactureUnique.Name = "lblFactureUnique";
            this.lblFactureUnique.Size = new System.Drawing.Size(128, 13);
            this.lblFactureUnique.TabIndex = 17;
            this.lblFactureUnique.Text = "numéro de facture unique";
            // 
            // btnActualiseFactureUnique
            // 
            this.btnActualiseFactureUnique.Enabled = false;
            this.btnActualiseFactureUnique.Image = global::ImportSosGeneve.Properties.Resources.Eclaire2;
            this.btnActualiseFactureUnique.Location = new System.Drawing.Point(417, 16);
            this.btnActualiseFactureUnique.Name = "btnActualiseFactureUnique";
            this.btnActualiseFactureUnique.Size = new System.Drawing.Size(19, 20);
            this.btnActualiseFactureUnique.TabIndex = 16;
            this.btnActualiseFactureUnique.UseVisualStyleBackColor = true;
            this.btnActualiseFactureUnique.Click += new System.EventHandler(this.btnActualiseFactureUnique_Click);
            // 
            // btnFactureActualise
            // 
            this.btnFactureActualise.Enabled = false;
            this.btnFactureActualise.Image = ((System.Drawing.Image)(resources.GetObject("btnFactureActualise.Image")));
            this.btnFactureActualise.Location = new System.Drawing.Point(417, 42);
            this.btnFactureActualise.Name = "btnFactureActualise";
            this.btnFactureActualise.Size = new System.Drawing.Size(19, 20);
            this.btnFactureActualise.TabIndex = 15;
            this.btnFactureActualise.UseVisualStyleBackColor = true;
            this.btnFactureActualise.Click += new System.EventHandler(this.btnActualise_Click);
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(6, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(858, 2);
            this.label1.TabIndex = 11;
            // 
            // lblDateRelance
            // 
            this.lblDateRelance.AutoSize = true;
            this.lblDateRelance.Enabled = false;
            this.lblDateRelance.Location = new System.Drawing.Point(492, 24);
            this.lblDateRelance.Name = "lblDateRelance";
            this.lblDateRelance.Size = new System.Drawing.Size(30, 13);
            this.lblDateRelance.TabIndex = 10;
            this.lblDateRelance.Text = "Date";
            // 
            // dtpDateRelance
            // 
            this.dtpDateRelance.CustomFormat = "dd MMMM yyyy";
            this.dtpDateRelance.Enabled = false;
            this.dtpDateRelance.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateRelance.Location = new System.Drawing.Point(528, 20);
            this.dtpDateRelance.Name = "dtpDateRelance";
            this.dtpDateRelance.Size = new System.Drawing.Size(142, 20);
            this.dtpDateRelance.TabIndex = 9;
            // 
            // txtNumFacture
            // 
            this.txtNumFacture.Enabled = false;
            this.txtNumFacture.Location = new System.Drawing.Point(294, 42);
            this.txtNumFacture.Name = "txtNumFacture";
            this.txtNumFacture.Size = new System.Drawing.Size(117, 20);
            this.txtNumFacture.TabIndex = 7;
            // 
            // lblNumFacture
            // 
            this.lblNumFacture.AutoSize = true;
            this.lblNumFacture.Enabled = false;
            this.lblNumFacture.Location = new System.Drawing.Point(158, 45);
            this.lblNumFacture.Name = "lblNumFacture";
            this.lblNumFacture.Size = new System.Drawing.Size(130, 13);
            this.lblNumFacture.TabIndex = 6;
            this.lblNumFacture.Text = "Dernier numéro de facture";
            // 
            // chkIncident
            // 
            this.chkIncident.AutoSize = true;
            this.chkIncident.Location = new System.Drawing.Point(6, 19);
            this.chkIncident.Name = "chkIncident";
            this.chkIncident.Size = new System.Drawing.Size(131, 17);
            this.chkIncident.TabIndex = 5;
            this.chkIncident.Text = "Reprise après incident";
            this.chkIncident.UseVisualStyleBackColor = true;
            this.chkIncident.CheckedChanged += new System.EventHandler(this.chkIncident_CheckedChanged);
            // 
            // grpProgression
            // 
            this.grpProgression.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpProgression.Controls.Add(this.lblProgression);
            this.grpProgression.Controls.Add(this.prgTache);
            this.grpProgression.Location = new System.Drawing.Point(12, 413);
            this.grpProgression.Name = "grpProgression";
            this.grpProgression.Size = new System.Drawing.Size(867, 70);
            this.grpProgression.TabIndex = 8;
            this.grpProgression.TabStop = false;
            this.grpProgression.Text = "Progression ";
            // 
            // lblProgression
            // 
            this.lblProgression.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProgression.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblProgression.Location = new System.Drawing.Point(6, 23);
            this.lblProgression.Name = "lblProgression";
            this.lblProgression.Size = new System.Drawing.Size(855, 18);
            this.lblProgression.TabIndex = 6;
            // 
            // chkApercu
            // 
            this.chkApercu.AutoSize = true;
            this.chkApercu.Checked = true;
            this.chkApercu.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkApercu.Location = new System.Drawing.Point(6, 19);
            this.chkApercu.Name = "chkApercu";
            this.chkApercu.Size = new System.Drawing.Size(142, 17);
            this.chkApercu.TabIndex = 6;
            this.chkApercu.Text = "Apercu avant impression";
            this.chkApercu.UseVisualStyleBackColor = true;
            // 
            // grpImpression
            // 
            this.grpImpression.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpImpression.Controls.Add(this.chkApercu);
            this.grpImpression.Controls.Add(this.cbxImprimantes);
            this.grpImpression.Controls.Add(this.lblImprimante);
            this.grpImpression.Location = new System.Drawing.Point(12, 255);
            this.grpImpression.Name = "grpImpression";
            this.grpImpression.Size = new System.Drawing.Size(867, 73);
            this.grpImpression.TabIndex = 9;
            this.grpImpression.TabStop = false;
            this.grpImpression.Text = "Impression";
            // 
            // btnFermer
            // 
            this.btnFermer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFermer.BackgroundImage = global::ImportSosGeneve.Properties.Resources.Fermer;
            this.btnFermer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnFermer.Location = new System.Drawing.Point(845, 492);
            this.btnFermer.Name = "btnFermer";
            this.btnFermer.Size = new System.Drawing.Size(34, 34);
            this.btnFermer.TabIndex = 1;
            this.btnFermer.UseVisualStyleBackColor = true;
            this.btnFermer.Click += new System.EventHandler(this.btnFermer_Click);
            // 
            // btnValider
            // 
            this.btnValider.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnValider.Image = global::ImportSosGeneve.Properties.Resources.Valider;
            this.btnValider.Location = new System.Drawing.Point(805, 492);
            this.btnValider.Name = "btnValider";
            this.btnValider.Size = new System.Drawing.Size(34, 34);
            this.btnValider.TabIndex = 0;
            this.btnValider.UseVisualStyleBackColor = true;
            this.btnValider.Click += new System.EventHandler(this.btnValider_Click);
            // 
            // frmRelances
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.ClientSize = new System.Drawing.Size(891, 538);
            this.ControlBox = false;
            this.Controls.Add(this.grpImpression);
            this.Controls.Add(this.grpProgression);
            this.Controls.Add(this.grpParametres);
            this.Controls.Add(this.grp);
            this.Controls.Add(this.btnFermer);
            this.Controls.Add(this.btnValider);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmRelances";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = " ";
            this.grp.ResumeLayout(false);
            this.grpParametres.ResumeLayout(false);
            this.grpParametres.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpProgression.ResumeLayout(false);
            this.grpImpression.ResumeLayout(false);
            this.grpImpression.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnValider;
        private System.Windows.Forms.Button btnFermer;
        private System.Windows.Forms.ComboBox cbxImprimantes;
        private System.Windows.Forms.Label LblTexte;
        private System.Windows.Forms.GroupBox grp;
        private System.Windows.Forms.Label lblImprimante;
        private System.Windows.Forms.ProgressBar prgTache;
        private System.Windows.Forms.GroupBox grpParametres;
        private System.Windows.Forms.GroupBox grpProgression;
        private System.Windows.Forms.Label lblProgression;
        private System.Windows.Forms.CheckBox chkIncident;
        private System.Windows.Forms.Label lblDateRelance;
        private System.Windows.Forms.DateTimePicker dtpDateRelance;
        private System.Windows.Forms.TextBox txtNumFacture;
        private System.Windows.Forms.Label lblNumFacture;
        private System.Windows.Forms.CheckBox chkApercu;
        private System.Windows.Forms.GroupBox grpImpression;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnFactureActualise;
        private System.Windows.Forms.TextBox txtNumFactureUnique;
        private System.Windows.Forms.Label lblFactureUnique;
        private System.Windows.Forms.Button btnActualiseFactureUnique;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton RB1;
        private System.Windows.Forms.RadioButton RB0;
        private System.Windows.Forms.RadioButton RB5;
        private System.Windows.Forms.RadioButton RB4;
        private System.Windows.Forms.RadioButton RB3;
        private System.Windows.Forms.RadioButton RB2;
    }
}