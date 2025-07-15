
namespace ImportSosGeneve.Facture
{
    partial class FormSalaireMedecins
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
            this.bFactEncaiss = new System.Windows.Forms.Button();
            this.dTDateDeb = new System.Windows.Forms.DateTimePicker();
            this.dTDateFin = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.bEnvoiMail = new System.Windows.Forms.Button();
            this.bEtatsDebiteurs = new System.Windows.Forms.Button();
            this.lblMedecin = new System.Windows.Forms.Label();
            this.lblTri = new System.Windows.Forms.Label();
            this.cbMedecin = new System.Windows.Forms.ComboBox();
            this.cbTri = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dTPSDateSolde = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dTPDateFin = new System.Windows.Forms.DateTimePicker();
            this.dTPDateDebut = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bFactEncaiss
            // 
            this.bFactEncaiss.Location = new System.Drawing.Point(123, 130);
            this.bFactEncaiss.Name = "bFactEncaiss";
            this.bFactEncaiss.Size = new System.Drawing.Size(119, 57);
            this.bFactEncaiss.TabIndex = 0;
            this.bFactEncaiss.Text = "Impression Factures encaissées par médecin";
            this.bFactEncaiss.UseVisualStyleBackColor = true;
            this.bFactEncaiss.Click += new System.EventHandler(this.bFactEncaiss_Click);
            // 
            // dTDateDeb
            // 
            this.dTDateDeb.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dTDateDeb.Location = new System.Drawing.Point(77, 51);
            this.dTDateDeb.Name = "dTDateDeb";
            this.dTDateDeb.Size = new System.Drawing.Size(92, 20);
            this.dTDateDeb.TabIndex = 1;
            this.dTDateDeb.Value = new System.DateTime(2024, 4, 1, 0, 0, 0, 0);
            // 
            // dTDateFin
            // 
            this.dTDateFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dTDateFin.Location = new System.Drawing.Point(208, 51);
            this.dTDateFin.Name = "dTDateFin";
            this.dTDateFin.Size = new System.Drawing.Size(92, 20);
            this.dTDateFin.TabIndex = 2;
            this.dTDateFin.Value = new System.DateTime(2024, 5, 1, 0, 0, 0, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(85, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Date de début";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(226, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Date de fin";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.bEnvoiMail);
            this.splitContainer1.Panel1.Controls.Add(this.bFactEncaiss);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.dTDateDeb);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.dTDateFin);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.bEtatsDebiteurs);
            this.splitContainer1.Panel2.Controls.Add(this.lblMedecin);
            this.splitContainer1.Panel2.Controls.Add(this.lblTri);
            this.splitContainer1.Panel2.Controls.Add(this.cbMedecin);
            this.splitContainer1.Panel2.Controls.Add(this.cbTri);
            this.splitContainer1.Panel2.Controls.Add(this.label5);
            this.splitContainer1.Panel2.Controls.Add(this.dTPSDateSolde);
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.dTPDateFin);
            this.splitContainer1.Panel2.Controls.Add(this.dTPDateDebut);
            this.splitContainer1.Size = new System.Drawing.Size(770, 358);
            this.splitContainer1.SplitterDistance = 327;
            this.splitContainer1.TabIndex = 5;
            // 
            // bEnvoiMail
            // 
            this.bEnvoiMail.Location = new System.Drawing.Point(121, 258);
            this.bEnvoiMail.Name = "bEnvoiMail";
            this.bEnvoiMail.Size = new System.Drawing.Size(121, 41);
            this.bEnvoiMail.TabIndex = 0;
            this.bEnvoiMail.Text = "Envoi des états par email";
            this.bEnvoiMail.UseVisualStyleBackColor = true;
            this.bEnvoiMail.Click += new System.EventHandler(this.bEnvoiMail_Click);
            // 
            // bEtatsDebiteurs
            // 
            this.bEtatsDebiteurs.Location = new System.Drawing.Point(154, 227);
            this.bEtatsDebiteurs.Name = "bEtatsDebiteurs";
            this.bEtatsDebiteurs.Size = new System.Drawing.Size(124, 37);
            this.bEtatsDebiteurs.TabIndex = 22;
            this.bEtatsDebiteurs.Text = "Impression des états débiteurs";
            this.bEtatsDebiteurs.UseVisualStyleBackColor = true;
            this.bEtatsDebiteurs.Click += new System.EventHandler(this.bEtatsDebiteurs_Click);
            // 
            // lblMedecin
            // 
            this.lblMedecin.AutoSize = true;
            this.lblMedecin.Location = new System.Drawing.Point(72, 112);
            this.lblMedecin.Name = "lblMedecin";
            this.lblMedecin.Size = new System.Drawing.Size(48, 13);
            this.lblMedecin.TabIndex = 21;
            this.lblMedecin.Text = "Médecin";
            // 
            // lblTri
            // 
            this.lblTri.AutoSize = true;
            this.lblTri.Location = new System.Drawing.Point(76, 152);
            this.lblTri.Name = "lblTri";
            this.lblTri.Size = new System.Drawing.Size(44, 13);
            this.lblTri.TabIndex = 19;
            this.lblTri.Text = "Trié Par";
            // 
            // cbMedecin
            // 
            this.cbMedecin.Location = new System.Drawing.Point(126, 104);
            this.cbMedecin.Name = "cbMedecin";
            this.cbMedecin.Size = new System.Drawing.Size(207, 21);
            this.cbMedecin.TabIndex = 9;
            this.cbMedecin.DropDown += new System.EventHandler(this.cbMedecin_DropDown);
            // 
            // cbTri
            // 
            this.cbTri.Items.AddRange(new object[] {
            "Date de consultation croissante",
            "Date de consultation décroissante",
            "Nom patient",
            "N° de facture",
            "Assurance"});
            this.cbTri.Location = new System.Drawing.Point(126, 152);
            this.cbTri.Name = "cbTri";
            this.cbTri.Size = new System.Drawing.Size(207, 21);
            this.cbTri.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(313, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Solde au";
            // 
            // dTPSDateSolde
            // 
            this.dTPSDateSolde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dTPSDateSolde.Location = new System.Drawing.Point(290, 51);
            this.dTPSDateSolde.Name = "dTPSDateSolde";
            this.dTPSDateSolde.Size = new System.Drawing.Size(92, 20);
            this.dTPSDateSolde.TabIndex = 6;
            this.dTPSDateSolde.Value = new System.DateTime(2024, 4, 1, 0, 0, 0, 0);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(177, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "au (inclu)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(38, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Date d\'appel du";
            // 
            // dTPDateFin
            // 
            this.dTPDateFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dTPDateFin.Location = new System.Drawing.Point(163, 51);
            this.dTPDateFin.Name = "dTPDateFin";
            this.dTPDateFin.Size = new System.Drawing.Size(92, 20);
            this.dTPDateFin.TabIndex = 3;
            this.dTPDateFin.Value = new System.DateTime(2024, 5, 1, 0, 0, 0, 0);
            // 
            // dTPDateDebut
            // 
            this.dTPDateDebut.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dTPDateDebut.Location = new System.Drawing.Point(28, 51);
            this.dTPDateDebut.Name = "dTPDateDebut";
            this.dTPDateDebut.Size = new System.Drawing.Size(92, 20);
            this.dTPDateDebut.TabIndex = 2;
            this.dTPDateDebut.Value = new System.DateTime(2024, 4, 1, 0, 0, 0, 0);
            // 
            // FormSalaireMedecins
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 358);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FormSalaireMedecins";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Salaires Medecins";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bFactEncaiss;
        private System.Windows.Forms.DateTimePicker dTDateDeb;
        private System.Windows.Forms.DateTimePicker dTDateFin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button bEnvoiMail;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dTPSDateSolde;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dTPDateFin;
        private System.Windows.Forms.DateTimePicker dTPDateDebut;
        private System.Windows.Forms.ComboBox cbTri;
        private System.Windows.Forms.ComboBox cbMedecin;
        private System.Windows.Forms.Label lblTri;
        private System.Windows.Forms.Label lblMedecin;
        private System.Windows.Forms.Button bEtatsDebiteurs;
    }
}