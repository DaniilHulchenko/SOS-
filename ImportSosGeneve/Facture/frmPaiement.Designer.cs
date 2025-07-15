namespace ImportSosGeneve.Facture
{
    partial class frmPaiement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPaiement));
            this.grpEntete = new System.Windows.Forms.GroupBox();
            this.dbxDateSal = new SosMedecins.Controls.sosDateBox();
            this.dbxDate = new SosMedecins.Controls.sosDateBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtMontant = new SosMedecins.Controls.sosTextBox();
            this.lblType = new System.Windows.Forms.Label();
            this.cbxType = new System.Windows.Forms.ComboBox();
            this.lblMontant = new System.Windows.Forms.Label();
            this.cbxMoyen = new System.Windows.Forms.ComboBox();
            this.lblMoyen = new System.Windows.Forms.Label();
            this.txtPayeCommentaire = new System.Windows.Forms.TextBox();
            this.lblCommentaire = new System.Windows.Forms.Label();
            this.btnAnnuler = new System.Windows.Forms.Button();
            this.btnValider = new System.Windows.Forms.Button();
            this.grpEntete.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpEntete
            // 
            this.grpEntete.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpEntete.Controls.Add(this.dbxDateSal);
            this.grpEntete.Controls.Add(this.dbxDate);
            this.grpEntete.Controls.Add(this.label1);
            this.grpEntete.Controls.Add(this.label2);
            this.grpEntete.Location = new System.Drawing.Point(12, 12);
            this.grpEntete.Name = "grpEntete";
            this.grpEntete.Size = new System.Drawing.Size(432, 57);
            this.grpEntete.TabIndex = 6;
            this.grpEntete.TabStop = false;
            // 
            // dbxDateSal
            // 
            this.dbxDateSal.Location = new System.Drawing.Point(301, 19);
            this.dbxDateSal.Mask = "00/00/0000";
            this.dbxDateSal.Name = "dbxDateSal";
            this.dbxDateSal.Size = new System.Drawing.Size(73, 20);
            this.dbxDateSal.TabIndex = 3;
            this.dbxDateSal.ValidatingType = typeof(System.DateTime);
            this.dbxDateSal.Value = "";
            // 
            // dbxDate
            // 
            this.dbxDate.Location = new System.Drawing.Point(87, 19);
            this.dbxDate.Mask = "00/00/0000";
            this.dbxDate.Name = "dbxDate";
            this.dbxDate.Size = new System.Drawing.Size(71, 20);
            this.dbxDate.TabIndex = 1;
            this.dbxDate.ValidatingType = typeof(System.DateTime);
            this.dbxDate.Value = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Date Paiment :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(199, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "Date Pour Salaire :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtMontant);
            this.groupBox1.Controls.Add(this.lblType);
            this.groupBox1.Controls.Add(this.cbxType);
            this.groupBox1.Controls.Add(this.lblMontant);
            this.groupBox1.Controls.Add(this.cbxMoyen);
            this.groupBox1.Controls.Add(this.lblMoyen);
            this.groupBox1.Controls.Add(this.txtPayeCommentaire);
            this.groupBox1.Controls.Add(this.lblCommentaire);
            this.groupBox1.Location = new System.Drawing.Point(13, 79);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(431, 194);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // txtMontant
            // 
            this.txtMontant.Espace = false;
            this.txtMontant.Location = new System.Drawing.Point(86, 73);
            this.txtMontant.Name = "txtMontant";
            this.txtMontant.Negatif = true;
            this.txtMontant.PartieDecimale = 2;
            this.txtMontant.PartieEntiere = 10;
            this.txtMontant.Saisie = SosMedecins.Controls.sosTextBox.TypeSaisie.SaisieDecimal;
            this.txtMontant.Size = new System.Drawing.Size(71, 20);
            this.txtMontant.TabIndex = 5;
            this.txtMontant.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(6, 22);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(31, 13);
            this.lblType.TabIndex = 0;
            this.lblType.Text = "Type";
            // 
            // cbxType
            // 
            this.cbxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxType.FormattingEnabled = true;
            this.cbxType.Location = new System.Drawing.Point(86, 19);
            this.cbxType.Name = "cbxType";
            this.cbxType.Size = new System.Drawing.Size(156, 21);
            this.cbxType.TabIndex = 1;
            // 
            // lblMontant
            // 
            this.lblMontant.AutoSize = true;
            this.lblMontant.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMontant.Location = new System.Drawing.Point(6, 76);
            this.lblMontant.Name = "lblMontant";
            this.lblMontant.Size = new System.Drawing.Size(51, 14);
            this.lblMontant.TabIndex = 4;
            this.lblMontant.Text = "Montant :";
            // 
            // cbxMoyen
            // 
            this.cbxMoyen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxMoyen.Items.AddRange(new object[] {
            "CDM",
            "SBVR",
            "CCP",
            "CS",
            "BM",
            "AT",
            "AUCUN"});
            this.cbxMoyen.Location = new System.Drawing.Point(86, 46);
            this.cbxMoyen.Name = "cbxMoyen";
            this.cbxMoyen.Size = new System.Drawing.Size(156, 21);
            this.cbxMoyen.TabIndex = 3;
            // 
            // lblMoyen
            // 
            this.lblMoyen.AutoSize = true;
            this.lblMoyen.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMoyen.Location = new System.Drawing.Point(6, 47);
            this.lblMoyen.Name = "lblMoyen";
            this.lblMoyen.Size = new System.Drawing.Size(45, 14);
            this.lblMoyen.TabIndex = 2;
            this.lblMoyen.Text = "Moyen :";
            this.lblMoyen.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtPayeCommentaire
            // 
            this.txtPayeCommentaire.Location = new System.Drawing.Point(86, 99);
            this.txtPayeCommentaire.Multiline = true;
            this.txtPayeCommentaire.Name = "txtPayeCommentaire";
            this.txtPayeCommentaire.Size = new System.Drawing.Size(308, 56);
            this.txtPayeCommentaire.TabIndex = 7;
            // 
            // lblCommentaire
            // 
            this.lblCommentaire.AutoSize = true;
            this.lblCommentaire.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCommentaire.Location = new System.Drawing.Point(5, 102);
            this.lblCommentaire.Name = "lblCommentaire";
            this.lblCommentaire.Size = new System.Drawing.Size(75, 14);
            this.lblCommentaire.TabIndex = 6;
            this.lblCommentaire.Text = "Commentaire :";
            // 
            // btnAnnuler
            // 
            this.btnAnnuler.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAnnuler.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAnnuler.BackgroundImage")));
            this.btnAnnuler.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnAnnuler.Location = new System.Drawing.Point(338, 283);
            this.btnAnnuler.Name = "btnAnnuler";
            this.btnAnnuler.Size = new System.Drawing.Size(76, 34);
            this.btnAnnuler.TabIndex = 9;
            this.btnAnnuler.UseVisualStyleBackColor = false;
            this.btnAnnuler.Click += new System.EventHandler(this.btnAnnuler_Click);
            // 
            // btnValider
            // 
            this.btnValider.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnValider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnValider.Image = global::ImportSosGeneve.Properties.Resources.Valider;
            this.btnValider.Location = new System.Drawing.Point(256, 283);
            this.btnValider.Name = "btnValider";
            this.btnValider.Size = new System.Drawing.Size(76, 34);
            this.btnValider.TabIndex = 8;
            this.btnValider.UseVisualStyleBackColor = false;
            this.btnValider.Click += new System.EventHandler(this.btnValider_Click);
            // 
            // frmPaiement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.ClientSize = new System.Drawing.Size(479, 329);
            this.Controls.Add(this.btnAnnuler);
            this.Controls.Add(this.btnValider);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpEntete);
            this.Name = "frmPaiement";
            this.Text = "Paiement";
            this.Load += new System.EventHandler(this.frmPaiement_Load);
            this.grpEntete.ResumeLayout(false);
            this.grpEntete.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpEntete;
        internal SosMedecins.Controls.sosDateBox dbxDateSal;
        internal SosMedecins.Controls.sosDateBox dbxDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        internal SosMedecins.Controls.sosTextBox txtMontant;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ComboBox cbxType;
        private System.Windows.Forms.Label lblMontant;
        private System.Windows.Forms.ComboBox cbxMoyen;
        private System.Windows.Forms.Label lblMoyen;
        private System.Windows.Forms.TextBox txtPayeCommentaire;
        private System.Windows.Forms.Label lblCommentaire;
        private System.Windows.Forms.Button btnAnnuler;
        private System.Windows.Forms.Button btnValider;

    }
}