namespace ImportSosGeneve.Facture
{
    partial class frmModificationSolde
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
            this.grpSolde = new System.Windows.Forms.GroupBox();
            this.txtNouveauSolde = new SosMedecins.Controls.sosTextBox();
            this.txtAncienSolde = new System.Windows.Forms.TextBox();
            this.LblNouveauSolde = new System.Windows.Forms.Label();
            this.lblAncienSolde = new System.Windows.Forms.Label();
            this.lblAvertissement = new System.Windows.Forms.Label();
            this.btnAnnuler = new System.Windows.Forms.Button();
            this.btnValider = new System.Windows.Forms.Button();
            this.grpSolde.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpSolde
            // 
            this.grpSolde.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.grpSolde.Controls.Add(this.txtNouveauSolde);
            this.grpSolde.Controls.Add(this.txtAncienSolde);
            this.grpSolde.Controls.Add(this.LblNouveauSolde);
            this.grpSolde.Controls.Add(this.lblAncienSolde);
            this.grpSolde.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpSolde.Location = new System.Drawing.Point(14, 94);
            this.grpSolde.Name = "grpSolde";
            this.grpSolde.Size = new System.Drawing.Size(273, 121);
            this.grpSolde.TabIndex = 2;
            this.grpSolde.TabStop = false;
            this.grpSolde.Text = "Soldes";
            // 
            // txtNouveauSolde
            // 
            this.txtNouveauSolde.Espace = false;
            this.txtNouveauSolde.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNouveauSolde.Location = new System.Drawing.Point(104, 68);
            this.txtNouveauSolde.Name = "txtNouveauSolde";
            this.txtNouveauSolde.PartieDecimale = 2;
            this.txtNouveauSolde.PartieEntiere = 10;
            this.txtNouveauSolde.Saisie = SosMedecins.Controls.sosTextBox.TypeSaisie.SaisieDecimal;
            this.txtNouveauSolde.Size = new System.Drawing.Size(112, 22);
            this.txtNouveauSolde.TabIndex = 3;
            this.txtNouveauSolde.TextChanged += new System.EventHandler(this.txtNouveauSolde_TextChanged);
            // 
            // txtAncienSolde
            // 
            this.txtAncienSolde.Enabled = false;
            this.txtAncienSolde.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAncienSolde.Location = new System.Drawing.Point(104, 26);
            this.txtAncienSolde.Name = "txtAncienSolde";
            this.txtAncienSolde.Size = new System.Drawing.Size(112, 22);
            this.txtAncienSolde.TabIndex = 1;
            // 
            // LblNouveauSolde
            // 
            this.LblNouveauSolde.AutoSize = true;
            this.LblNouveauSolde.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblNouveauSolde.Location = new System.Drawing.Point(35, 74);
            this.LblNouveauSolde.Name = "LblNouveauSolde";
            this.LblNouveauSolde.Size = new System.Drawing.Size(63, 16);
            this.LblNouveauSolde.TabIndex = 2;
            this.LblNouveauSolde.Text = "Nouveau";
            // 
            // lblAncienSolde
            // 
            this.lblAncienSolde.AutoSize = true;
            this.lblAncienSolde.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAncienSolde.Location = new System.Drawing.Point(35, 32);
            this.lblAncienSolde.Name = "lblAncienSolde";
            this.lblAncienSolde.Size = new System.Drawing.Size(49, 16);
            this.lblAncienSolde.TabIndex = 0;
            this.lblAncienSolde.Text = "Ancien";
            // 
            // lblAvertissement
            // 
            this.lblAvertissement.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAvertissement.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAvertissement.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvertissement.ForeColor = System.Drawing.Color.DarkRed;
            this.lblAvertissement.Location = new System.Drawing.Point(46, 21);
            this.lblAvertissement.Name = "lblAvertissement";
            this.lblAvertissement.Size = new System.Drawing.Size(200, 55);
            this.lblAvertissement.TabIndex = 3;
            this.lblAvertissement.Text = "Label1";
            this.lblAvertissement.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnAnnuler
            // 
            this.btnAnnuler.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAnnuler.BackgroundImage = global::ImportSosGeneve.Properties.Resources.exit;
            this.btnAnnuler.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAnnuler.FlatAppearance.BorderSize = 0;
            this.btnAnnuler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnnuler.Location = new System.Drawing.Point(191, 221);
            this.btnAnnuler.Name = "btnAnnuler";
            this.btnAnnuler.Size = new System.Drawing.Size(64, 64);
            this.btnAnnuler.TabIndex = 5;
            this.btnAnnuler.UseVisualStyleBackColor = true;
            this.btnAnnuler.Click += new System.EventHandler(this.btnAnnuler_Click);
            // 
            // btnValider
            // 
            this.btnValider.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnValider.BackgroundImage = global::ImportSosGeneve.Properties.Resources.brondValider;
            this.btnValider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnValider.Enabled = false;
            this.btnValider.FlatAppearance.BorderSize = 0;
            this.btnValider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnValider.Location = new System.Drawing.Point(74, 221);
            this.btnValider.Name = "btnValider";
            this.btnValider.Size = new System.Drawing.Size(64, 64);
            this.btnValider.TabIndex = 4;
            this.btnValider.UseVisualStyleBackColor = true;
            this.btnValider.Click += new System.EventHandler(this.btnValider_Click);
            // 
            // frmModificationSolde
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.ClientSize = new System.Drawing.Size(313, 297);
            this.Controls.Add(this.btnAnnuler);
            this.Controls.Add(this.btnValider);
            this.Controls.Add(this.lblAvertissement);
            this.Controls.Add(this.grpSolde);
            this.Name = "frmModificationSolde";
            this.Text = "Modification du solde de la facture";
            this.grpSolde.ResumeLayout(false);
            this.grpSolde.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        internal SosMedecins.Controls.sosTextBox txtNouveauSolde;
        internal System.Windows.Forms.TextBox txtAncienSolde;
        internal System.Windows.Forms.Label LblNouveauSolde;
        internal System.Windows.Forms.Label lblAncienSolde;
        internal System.Windows.Forms.Label lblAvertissement;
        internal System.Windows.Forms.Button btnAnnuler;
        internal System.Windows.Forms.Button btnValider;
        public System.Windows.Forms.GroupBox grpSolde;
    }
}