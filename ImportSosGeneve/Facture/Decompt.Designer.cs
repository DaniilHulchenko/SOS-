namespace ImportSosGeneve.Facture
{
    partial class Decompt
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
            this.GBDate = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnPaiement = new System.Windows.Forms.Button();
            this.dtDebut = new System.Windows.Forms.DateTimePicker();
            this.dtFin = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.GBDate.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // GBDate
            // 
            this.GBDate.Controls.Add(this.label2);
            this.GBDate.Controls.Add(this.label1);
            this.GBDate.Controls.Add(this.dtFin);
            this.GBDate.Controls.Add(this.dtDebut);
            this.GBDate.Location = new System.Drawing.Point(39, 24);
            this.GBDate.Name = "GBDate";
            this.GBDate.Size = new System.Drawing.Size(498, 97);
            this.GBDate.TabIndex = 0;
            this.GBDate.TabStop = false;
            this.GBDate.Text = "TrancheDate";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnPaiement);
            this.groupBox2.Location = new System.Drawing.Point(39, 141);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(498, 97);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // btnPaiement
            // 
            this.btnPaiement.Enabled = false;
            this.btnPaiement.Image = global::ImportSosGeneve.Properties.Resources.plus;
            this.btnPaiement.Location = new System.Drawing.Point(423, 32);
            this.btnPaiement.Name = "btnPaiement";
            this.btnPaiement.Size = new System.Drawing.Size(44, 42);
            this.btnPaiement.TabIndex = 11;
            this.btnPaiement.UseVisualStyleBackColor = true;
            this.btnPaiement.Click += new System.EventHandler(this.btnPaiement_Click);
            // 
            // dtDebut
            // 
            this.dtDebut.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtDebut.Location = new System.Drawing.Point(290, 19);
            this.dtDebut.Name = "dtDebut";
            this.dtDebut.Size = new System.Drawing.Size(88, 20);
            this.dtDebut.TabIndex = 1;
            // 
            // dtFin
            // 
            this.dtFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFin.Location = new System.Drawing.Point(290, 54);
            this.dtFin.Name = "dtFin";
            this.dtFin.Size = new System.Drawing.Size(88, 20);
            this.dtFin.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(61, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Date Debut ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(61, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Date Fin";
            // 
            // Decompt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 250);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.GBDate);
            this.Name = "Decompt";
            this.Text = "Decompt";
            this.GBDate.ResumeLayout(false);
            this.GBDate.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GBDate;
        private System.Windows.Forms.GroupBox groupBox2;
        internal System.Windows.Forms.Button btnPaiement;
        private System.Windows.Forms.DateTimePicker dtDebut;
        private System.Windows.Forms.DateTimePicker dtFin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}