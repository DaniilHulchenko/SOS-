namespace ImportSosGeneve
{
    partial class ListeMedecinscs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListeMedecinscs));
            this.clbmedecins = new System.Windows.Forms.CheckedListBox();
            this.btChargerMedecins = new System.Windows.Forms.Button();
            this.buttonEtatDebiteur = new System.Windows.Forms.Button();
            this.dtDebut = new System.Windows.Forms.DateTimePicker();
            this.dtFin = new System.Windows.Forms.DateTimePicker();
            this.buttonAllselec = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.labelCountMed = new System.Windows.Forms.Label();
            this.buttonFacEcpMed = new System.Windows.Forms.Button();
            this.buttonPrestationMed = new System.Windows.Forms.Button();
            this.dtSoldeAu = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.dtdebFin = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dtdebDepuis = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cbTri = new System.Windows.Forms.ComboBox();
            this.lblTri = new System.Windows.Forms.Label();
            this.lcompteur = new System.Windows.Forms.Label();
            this.bSalaireMed = new System.Windows.Forms.Button();
            this.bChargerMedDebiteurs = new System.Windows.Forms.Button();
            this.bchargeStatistiques = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // clbmedecins
            // 
            this.clbmedecins.AccessibleRole = System.Windows.Forms.AccessibleRole.RadioButton;
            this.clbmedecins.BackColor = System.Drawing.SystemColors.ControlDark;
            this.clbmedecins.CheckOnClick = true;
            this.clbmedecins.FormattingEnabled = true;
            this.clbmedecins.Location = new System.Drawing.Point(130, 3);
            this.clbmedecins.Name = "clbmedecins";
            this.clbmedecins.Size = new System.Drawing.Size(204, 589);
            this.clbmedecins.TabIndex = 3;
            this.clbmedecins.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbmedecins_ItemCheck);
            // 
            // btChargerMedecins
            // 
            this.btChargerMedecins.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btChargerMedecins.Location = new System.Drawing.Point(74, 140);
            this.btChargerMedecins.Name = "btChargerMedecins";
            this.btChargerMedecins.Size = new System.Drawing.Size(113, 48);
            this.btChargerMedecins.TabIndex = 1;
            this.btChargerMedecins.Text = "Charger médecins pour les encaiss.";
            this.btChargerMedecins.UseVisualStyleBackColor = true;
            this.btChargerMedecins.Click += new System.EventHandler(this.btChargerMedecins_Click);
            // 
            // buttonEtatDebiteur
            // 
            this.buttonEtatDebiteur.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEtatDebiteur.Location = new System.Drawing.Point(206, 189);
            this.buttonEtatDebiteur.Name = "buttonEtatDebiteur";
            this.buttonEtatDebiteur.Size = new System.Drawing.Size(176, 48);
            this.buttonEtatDebiteur.TabIndex = 8;
            this.buttonEtatDebiteur.Text = "Envoi des états débiteurs par médecin";
            this.buttonEtatDebiteur.UseVisualStyleBackColor = true;
            this.buttonEtatDebiteur.Click += new System.EventHandler(this.buttonEtatDebiteur_Click);
            // 
            // dtDebut
            // 
            this.dtDebut.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtDebut.Location = new System.Drawing.Point(99, 91);
            this.dtDebut.Name = "dtDebut";
            this.dtDebut.Size = new System.Drawing.Size(88, 20);
            this.dtDebut.TabIndex = 9;
            // 
            // dtFin
            // 
            this.dtFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFin.Location = new System.Drawing.Point(217, 91);
            this.dtFin.Name = "dtFin";
            this.dtFin.Size = new System.Drawing.Size(88, 20);
            this.dtFin.TabIndex = 10;
            // 
            // buttonAllselec
            // 
            this.buttonAllselec.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAllselec.Location = new System.Drawing.Point(168, 610);
            this.buttonAllselec.Name = "buttonAllselec";
            this.buttonAllselec.Size = new System.Drawing.Size(113, 28);
            this.buttonAllselec.TabIndex = 2;
            this.buttonAllselec.Text = "Tout sélectioner";
            this.buttonAllselec.UseVisualStyleBackColor = true;
            this.buttonAllselec.Click += new System.EventHandler(this.buttonAllselec_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(172, 675);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(506, 23);
            this.progressBar1.TabIndex = 8;
            // 
            // labelCountMed
            // 
            this.labelCountMed.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCountMed.ForeColor = System.Drawing.Color.Green;
            this.labelCountMed.Location = new System.Drawing.Point(8, 10);
            this.labelCountMed.Name = "labelCountMed";
            this.labelCountMed.Size = new System.Drawing.Size(104, 67);
            this.labelCountMed.TabIndex = 9;
            this.labelCountMed.Text = "Nombre de médecins selectionés : ";
            this.labelCountMed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonFacEcpMed
            // 
            this.buttonFacEcpMed.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonFacEcpMed.Location = new System.Drawing.Point(140, 313);
            this.buttonFacEcpMed.Name = "buttonFacEcpMed";
            this.buttonFacEcpMed.Size = new System.Drawing.Size(165, 36);
            this.buttonFacEcpMed.TabIndex = 11;
            this.buttonFacEcpMed.Text = "Factures encaissées par médecin old";
            this.buttonFacEcpMed.UseVisualStyleBackColor = true;
            this.buttonFacEcpMed.Visible = false;
            this.buttonFacEcpMed.Click += new System.EventHandler(this.ButtonFacEcPMed_Click);
            // 
            // buttonPrestationMed
            // 
            this.buttonPrestationMed.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPrestationMed.Location = new System.Drawing.Point(217, 228);
            this.buttonPrestationMed.Name = "buttonPrestationMed";
            this.buttonPrestationMed.Size = new System.Drawing.Size(165, 48);
            this.buttonPrestationMed.TabIndex = 12;
            this.buttonPrestationMed.Text = "Statistique prestation";
            this.buttonPrestationMed.UseVisualStyleBackColor = true;
            this.buttonPrestationMed.Click += new System.EventHandler(this.ButtonPrestationMed_Click);
            // 
            // dtSoldeAu
            // 
            this.dtSoldeAu.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtSoldeAu.Location = new System.Drawing.Point(310, 80);
            this.dtSoldeAu.Name = "dtSoldeAu";
            this.dtSoldeAu.Size = new System.Drawing.Size(88, 20);
            this.dtSoldeAu.TabIndex = 6;
            this.dtSoldeAu.Value = new System.DateTime(2013, 6, 19, 0, 0, 0, 0);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(190, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 33;
            this.label5.Text = "au (inclu)";
            // 
            // dtdebFin
            // 
            this.dtdebFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtdebFin.Location = new System.Drawing.Point(172, 81);
            this.dtdebFin.Name = "dtdebFin";
            this.dtdebFin.Size = new System.Drawing.Size(88, 20);
            this.dtdebFin.TabIndex = 5;
            this.dtdebFin.Value = new System.DateTime(2013, 6, 19, 0, 0, 0, 0);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(50, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 31;
            this.label3.Text = "Date d\'appel du";
            // 
            // dtdebDepuis
            // 
            this.dtdebDepuis.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtdebDepuis.Location = new System.Drawing.Point(53, 80);
            this.dtdebDepuis.Name = "dtdebDepuis";
            this.dtdebDepuis.Size = new System.Drawing.Size(88, 20);
            this.dtdebDepuis.TabIndex = 4;
            this.dtdebDepuis.Value = new System.DateTime(2008, 1, 1, 0, 0, 0, 0);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(327, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 29;
            this.label4.Text = "Solde : au";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(286, 75);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 13);
            this.label7.TabIndex = 34;
            this.label7.Text = "Exclu";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(221, 75);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(62, 13);
            this.label9.TabIndex = 39;
            this.label9.Text = "Date de Fin";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(105, 75);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(75, 13);
            this.label10.TabIndex = 40;
            this.label10.Text = "Date de début";
            // 
            // cbTri
            // 
            this.cbTri.Items.AddRange(new object[] {
            "Date de consultation croissante",
            "Date de consultation décroissante",
            "Nom patient",
            "N° de facture",
            "Assurance"});
            this.cbTri.Location = new System.Drawing.Point(138, 132);
            this.cbTri.Name = "cbTri";
            this.cbTri.Size = new System.Drawing.Size(195, 21);
            this.cbTri.TabIndex = 7;
            this.cbTri.Text = "Nom patient";
            // 
            // lblTri
            // 
            this.lblTri.AutoSize = true;
            this.lblTri.Location = new System.Drawing.Point(85, 135);
            this.lblTri.Name = "lblTri";
            this.lblTri.Size = new System.Drawing.Size(47, 13);
            this.lblTri.TabIndex = 42;
            this.lblTri.Text = "Trier Par";
            // 
            // lcompteur
            // 
            this.lcompteur.AutoSize = true;
            this.lcompteur.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lcompteur.ForeColor = System.Drawing.Color.Green;
            this.lcompteur.Location = new System.Drawing.Point(46, 90);
            this.lcompteur.Name = "lcompteur";
            this.lcompteur.Size = new System.Drawing.Size(25, 25);
            this.lcompteur.TabIndex = 43;
            this.lcompteur.Text = "0";
            // 
            // bSalaireMed
            // 
            this.bSalaireMed.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bSalaireMed.Location = new System.Drawing.Point(217, 140);
            this.bSalaireMed.Name = "bSalaireMed";
            this.bSalaireMed.Size = new System.Drawing.Size(165, 48);
            this.bSalaireMed.TabIndex = 44;
            this.bSalaireMed.Text = "Factures encaissées par médecin";
            this.bSalaireMed.UseVisualStyleBackColor = true;
            this.bSalaireMed.Click += new System.EventHandler(this.bSalaireMed_Click);
            // 
            // bChargerMedDebiteurs
            // 
            this.bChargerMedDebiteurs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bChargerMedDebiteurs.Location = new System.Drawing.Point(68, 189);
            this.bChargerMedDebiteurs.Name = "bChargerMedDebiteurs";
            this.bChargerMedDebiteurs.Size = new System.Drawing.Size(113, 48);
            this.bChargerMedDebiteurs.TabIndex = 45;
            this.bChargerMedDebiteurs.Text = "Charger Médecins pour les débiteurs";
            this.bChargerMedDebiteurs.UseVisualStyleBackColor = true;
            this.bChargerMedDebiteurs.Click += new System.EventHandler(this.bChargerMedDebiteurs_Click);
            // 
            // bchargeStatistiques
            // 
            this.bchargeStatistiques.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bchargeStatistiques.Location = new System.Drawing.Point(74, 228);
            this.bchargeStatistiques.Name = "bchargeStatistiques";
            this.bchargeStatistiques.Size = new System.Drawing.Size(113, 48);
            this.bchargeStatistiques.TabIndex = 46;
            this.bchargeStatistiques.Text = "Charger médecins pour les statistiques";
            this.bchargeStatistiques.UseVisualStyleBackColor = true;
            this.bchargeStatistiques.Click += new System.EventHandler(this.bStatistiques_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Location = new System.Drawing.Point(2, 1);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.splitContainer1.Panel1.Controls.Add(this.clbmedecins);
            this.splitContainer1.Panel1.Controls.Add(this.buttonAllselec);
            this.splitContainer1.Panel1.Controls.Add(this.lcompteur);
            this.splitContainer1.Panel1.Controls.Add(this.labelCountMed);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(824, 656);
            this.splitContainer1.SplitterDistance = 348;
            this.splitContainer1.TabIndex = 47;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            this.splitContainer2.Panel1.Controls.Add(this.buttonEtatDebiteur);
            this.splitContainer2.Panel1.Controls.Add(this.dtSoldeAu);
            this.splitContainer2.Panel1.Controls.Add(this.bChargerMedDebiteurs);
            this.splitContainer2.Panel1.Controls.Add(this.label4);
            this.splitContainer2.Panel1.Controls.Add(this.dtdebDepuis);
            this.splitContainer2.Panel1.Controls.Add(this.label3);
            this.splitContainer2.Panel1.Controls.Add(this.lblTri);
            this.splitContainer2.Panel1.Controls.Add(this.dtdebFin);
            this.splitContainer2.Panel1.Controls.Add(this.cbTri);
            this.splitContainer2.Panel1.Controls.Add(this.label5);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.splitContainer2.Panel2.Controls.Add(this.label2);
            this.splitContainer2.Panel2.Controls.Add(this.buttonFacEcpMed);
            this.splitContainer2.Panel2.Controls.Add(this.bchargeStatistiques);
            this.splitContainer2.Panel2.Controls.Add(this.dtDebut);
            this.splitContainer2.Panel2.Controls.Add(this.btChargerMedecins);
            this.splitContainer2.Panel2.Controls.Add(this.dtFin);
            this.splitContainer2.Panel2.Controls.Add(this.bSalaireMed);
            this.splitContainer2.Panel2.Controls.Add(this.buttonPrestationMed);
            this.splitContainer2.Panel2.Controls.Add(this.label7);
            this.splitContainer2.Panel2.Controls.Add(this.label10);
            this.splitContainer2.Panel2.Controls.Add(this.label9);
            this.splitContainer2.Size = new System.Drawing.Size(472, 656);
            this.splitContainer2.SplitterDistance = 266;
            this.splitContainer2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(161, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 24);
            this.label1.TabIndex = 46;
            this.label1.Text = "Etats débiteurs";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(84, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(272, 24);
            this.label2.TabIndex = 47;
            this.label2.Text = "Salaires médecins / Statistiques";
            // 
            // ListeMedecinscs
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.Caret;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(845, 710);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.progressBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ListeMedecinscs";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Envoi par email";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox clbmedecins;
        private System.Windows.Forms.Button btChargerMedecins;
        private System.Windows.Forms.Button buttonEtatDebiteur;
        private System.Windows.Forms.DateTimePicker dtDebut;
        private System.Windows.Forms.DateTimePicker dtFin;
        private System.Windows.Forms.Button buttonAllselec;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label labelCountMed;
        private System.Windows.Forms.Button buttonFacEcpMed;
        private System.Windows.Forms.Button buttonPrestationMed;
        private System.Windows.Forms.DateTimePicker dtSoldeAu;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtdebFin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtdebDepuis;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbTri;
        private System.Windows.Forms.Label lblTri;
        private System.Windows.Forms.Label lcompteur;
        private System.Windows.Forms.Button bSalaireMed;
        private System.Windows.Forms.Button bChargerMedDebiteurs;
        private System.Windows.Forms.Button bchargeStatistiques;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}