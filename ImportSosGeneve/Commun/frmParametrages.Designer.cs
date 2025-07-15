namespace ImportSosGeneve
{
    partial class frmParametrages
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmParametrages));
            this.lblRapports = new System.Windows.Forms.Label();
            this.cbInvoicePrinter = new System.Windows.Forms.ComboBox();
            this.lblFacture = new System.Windows.Forms.Label();
            this.cbReportPrinter = new System.Windows.Forms.ComboBox();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.txtBase = new System.Windows.Forms.TextBox();
            this.txtServeur = new System.Windows.Forms.TextBox();
            this.lblNomBase = new System.Windows.Forms.Label();
            this.lblServeur = new System.Windows.Forms.Label();
            this.lblMotDePasse = new System.Windows.Forms.Label();
            this.lblIdentifiant = new System.Windows.Forms.Label();
            this.btnValider = new System.Windows.Forms.Button();
            this.btnAnnuler = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblRapports
            // 
            this.lblRapports.Location = new System.Drawing.Point(6, 22);
            this.lblRapports.Name = "lblRapports";
            this.lblRapports.Size = new System.Drawing.Size(53, 16);
            this.lblRapports.TabIndex = 0;
            this.lblRapports.Text = "Rapports";
            // 
            // cbInvoicePrinter
            // 
            this.cbInvoicePrinter.Location = new System.Drawing.Point(102, 46);
            this.cbInvoicePrinter.Name = "cbInvoicePrinter";
            this.cbInvoicePrinter.Size = new System.Drawing.Size(173, 21);
            this.cbInvoicePrinter.TabIndex = 3;
            // 
            // lblFacture
            // 
            this.lblFacture.Location = new System.Drawing.Point(6, 49);
            this.lblFacture.Name = "lblFacture";
            this.lblFacture.Size = new System.Drawing.Size(55, 16);
            this.lblFacture.TabIndex = 2;
            this.lblFacture.Text = "Factures";
            // 
            // cbReportPrinter
            // 
            this.cbReportPrinter.Location = new System.Drawing.Point(102, 19);
            this.cbReportPrinter.Name = "cbReportPrinter";
            this.cbReportPrinter.Size = new System.Drawing.Size(173, 21);
            this.cbReportPrinter.TabIndex = 1;
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(102, 97);
            this.txtPass.Name = "txtPass";
            this.txtPass.PasswordChar = '*';
            this.txtPass.Size = new System.Drawing.Size(119, 20);
            this.txtPass.TabIndex = 7;
            // 
            // txtLogin
            // 
            this.txtLogin.Location = new System.Drawing.Point(102, 71);
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(119, 20);
            this.txtLogin.TabIndex = 5;
            // 
            // txtBase
            // 
            this.txtBase.Location = new System.Drawing.Point(102, 45);
            this.txtBase.Name = "txtBase";
            this.txtBase.Size = new System.Drawing.Size(119, 20);
            this.txtBase.TabIndex = 3;
            // 
            // txtServeur
            // 
            this.txtServeur.Location = new System.Drawing.Point(102, 17);
            this.txtServeur.Name = "txtServeur";
            this.txtServeur.Size = new System.Drawing.Size(119, 20);
            this.txtServeur.TabIndex = 1;
            // 
            // lblNomBase
            // 
            this.lblNomBase.AutoSize = true;
            this.lblNomBase.Location = new System.Drawing.Point(6, 48);
            this.lblNomBase.Name = "lblNomBase";
            this.lblNomBase.Size = new System.Drawing.Size(90, 13);
            this.lblNomBase.TabIndex = 2;
            this.lblNomBase.Text = "Base de données";
            // 
            // lblServeur
            // 
            this.lblServeur.Location = new System.Drawing.Point(6, 20);
            this.lblServeur.Name = "lblServeur";
            this.lblServeur.Size = new System.Drawing.Size(64, 16);
            this.lblServeur.TabIndex = 0;
            this.lblServeur.Text = "Serveur";
            // 
            // lblMotDePasse
            // 
            this.lblMotDePasse.AutoSize = true;
            this.lblMotDePasse.Location = new System.Drawing.Point(6, 100);
            this.lblMotDePasse.Name = "lblMotDePasse";
            this.lblMotDePasse.Size = new System.Drawing.Size(72, 13);
            this.lblMotDePasse.TabIndex = 6;
            this.lblMotDePasse.Text = "Mot de Passe";
            // 
            // lblIdentifiant
            // 
            this.lblIdentifiant.AutoSize = true;
            this.lblIdentifiant.Location = new System.Drawing.Point(6, 74);
            this.lblIdentifiant.Name = "lblIdentifiant";
            this.lblIdentifiant.Size = new System.Drawing.Size(53, 13);
            this.lblIdentifiant.TabIndex = 4;
            this.lblIdentifiant.Text = "Identifiant";
            // 
            // btnValider
            // 
            this.btnValider.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnValider.BackgroundImage = global::ImportSosGeneve.Properties.Resources.Ok;
            this.btnValider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnValider.Location = new System.Drawing.Point(130, 234);
            this.btnValider.Name = "btnValider";
            this.btnValider.Size = new System.Drawing.Size(80, 34);
            this.btnValider.TabIndex = 2;
            this.btnValider.UseVisualStyleBackColor = true;
            this.btnValider.Click += new System.EventHandler(this.btnValider_Click);
            // 
            // btnAnnuler
            // 
            this.btnAnnuler.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAnnuler.BackgroundImage = global::ImportSosGeneve.Properties.Resources.Fermer;
            this.btnAnnuler.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnAnnuler.Location = new System.Drawing.Point(216, 234);
            this.btnAnnuler.Name = "btnAnnuler";
            this.btnAnnuler.Size = new System.Drawing.Size(80, 34);
            this.btnAnnuler.TabIndex = 3;
            this.btnAnnuler.UseVisualStyleBackColor = true;
            this.btnAnnuler.Click += new System.EventHandler(this.btnAnnuler_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtBase);
            this.groupBox1.Controls.Add(this.lblIdentifiant);
            this.groupBox1.Controls.Add(this.lblMotDePasse);
            this.groupBox1.Controls.Add(this.lblServeur);
            this.groupBox1.Controls.Add(this.lblNomBase);
            this.groupBox1.Controls.Add(this.txtServeur);
            this.groupBox1.Controls.Add(this.txtLogin);
            this.groupBox1.Controls.Add(this.txtPass);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(284, 129);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Base de données";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.cbReportPrinter);
            this.groupBox2.Controls.Add(this.lblFacture);
            this.groupBox2.Controls.Add(this.cbInvoicePrinter);
            this.groupBox2.Controls.Add(this.lblRapports);
            this.groupBox2.Location = new System.Drawing.Point(12, 147);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(284, 80);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Imprimantes";
            // 
            // frmParametrages
            // 
            this.AcceptButton = this.btnValider;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.CancelButton = this.btnAnnuler;
            this.ClientSize = new System.Drawing.Size(308, 280);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnAnnuler);
            this.Controls.Add(this.btnValider);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmParametrages";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Parametrages";
            this.Load += new System.EventHandler(this.frmParametrages_onLoad);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblRapports;
        private System.Windows.Forms.ComboBox cbInvoicePrinter;
        private System.Windows.Forms.Label lblFacture;
        private System.Windows.Forms.ComboBox cbReportPrinter;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.TextBox txtBase;
        private System.Windows.Forms.TextBox txtServeur;
        private System.Windows.Forms.Label lblNomBase;
        private System.Windows.Forms.Label lblServeur;
        private System.Windows.Forms.Label lblMotDePasse;
        private System.Windows.Forms.Label lblIdentifiant;
        private System.Windows.Forms.Button btnValider;
        private System.Windows.Forms.Button btnAnnuler;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}