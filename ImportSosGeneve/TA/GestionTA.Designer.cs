namespace ImportSosGeneve.TA
{
    partial class InterfGestionTA
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InterfGestionTA));
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.buttonNouveauAbonnement = new System.Windows.Forms.Button();
            this.buttonAbnAnnule = new System.Windows.Forms.Button();
            this.labelDateDGestio = new System.Windows.Forms.Label();
            this.labelDFGestion = new System.Windows.Forms.Label();
            this.btnNoFax = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(106, 32);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(170, 20);
            this.dateTimePicker1.TabIndex = 0;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(106, 70);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(170, 20);
            this.dateTimePicker2.TabIndex = 1;
            // 
            // buttonNouveauAbonnement
            // 
            this.buttonNouveauAbonnement.BackColor = System.Drawing.Color.Linen;
            this.buttonNouveauAbonnement.Location = new System.Drawing.Point(393, 134);
            this.buttonNouveauAbonnement.Name = "buttonNouveauAbonnement";
            this.buttonNouveauAbonnement.Size = new System.Drawing.Size(114, 36);
            this.buttonNouveauAbonnement.TabIndex = 2;
            this.buttonNouveauAbonnement.Text = "Nouveaux Abonnements TA";
            this.buttonNouveauAbonnement.UseVisualStyleBackColor = false;
            this.buttonNouveauAbonnement.Click += new System.EventHandler(this.buttonNouveauAbonnement_Click);
            // 
            // buttonAbnAnnule
            // 
            this.buttonAbnAnnule.BackColor = System.Drawing.Color.Linen;
            this.buttonAbnAnnule.Location = new System.Drawing.Point(393, 26);
            this.buttonAbnAnnule.Name = "buttonAbnAnnule";
            this.buttonAbnAnnule.Size = new System.Drawing.Size(114, 36);
            this.buttonAbnAnnule.TabIndex = 3;
            this.buttonAbnAnnule.Text = "Abonnements TA Annulés";
            this.buttonAbnAnnule.UseVisualStyleBackColor = false;
            this.buttonAbnAnnule.Click += new System.EventHandler(this.buttonAbnAnnule_Click);
            // 
            // labelDateDGestio
            // 
            this.labelDateDGestio.AutoSize = true;
            this.labelDateDGestio.Location = new System.Drawing.Point(24, 32);
            this.labelDateDGestio.Name = "labelDateDGestio";
            this.labelDateDGestio.Size = new System.Drawing.Size(59, 13);
            this.labelDateDGestio.TabIndex = 4;
            this.labelDateDGestio.Text = "DateDebut";
            // 
            // labelDFGestion
            // 
            this.labelDFGestion.AutoSize = true;
            this.labelDFGestion.Location = new System.Drawing.Point(39, 70);
            this.labelDFGestion.Name = "labelDFGestion";
            this.labelDFGestion.Size = new System.Drawing.Size(44, 13);
            this.labelDFGestion.TabIndex = 5;
            this.labelDFGestion.Text = "DateFin";
            // 
            // btnNoFax
            // 
            this.btnNoFax.BackColor = System.Drawing.Color.Linen;
            this.btnNoFax.Location = new System.Drawing.Point(393, 81);
            this.btnNoFax.Name = "btnNoFax";
            this.btnNoFax.Size = new System.Drawing.Size(114, 36);
            this.btnNoFax.TabIndex = 6;
            this.btnNoFax.Text = "TA sans envoi de fax";
            this.btnNoFax.UseVisualStyleBackColor = false;
            this.btnNoFax.Click += new System.EventHandler(this.btnNoFax_Click);
            // 
            // InterfGestionTA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.ClientSize = new System.Drawing.Size(536, 215);
            this.Controls.Add(this.btnNoFax);
            this.Controls.Add(this.labelDFGestion);
            this.Controls.Add(this.labelDateDGestio);
            this.Controls.Add(this.buttonAbnAnnule);
            this.Controls.Add(this.buttonNouveauAbonnement);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "InterfGestionTA";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestion TA";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Button buttonNouveauAbonnement;
        private System.Windows.Forms.Button buttonAbnAnnule;
        private System.Windows.Forms.Label labelDateDGestio;
        private System.Windows.Forms.Label labelDFGestion;
        private System.Windows.Forms.Button btnNoFax;
    }
}