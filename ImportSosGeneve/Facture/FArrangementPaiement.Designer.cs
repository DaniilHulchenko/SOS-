
namespace ImportSosGeneve.Facture
{
    partial class FArrangementPaiement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FArrangementPaiement));
            this.label1 = new System.Windows.Forms.Label();
            this.tBoxNumFact = new System.Windows.Forms.TextBox();
            this.btnCherche = new System.Windows.Forms.Button();
            this.LMontantFact = new System.Windows.Forms.Label();
            this.numericUpDownNbBulletins = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.lMontantbulletin = new System.Windows.Forms.Label();
            this.ldernierBulletin = new System.Windows.Forms.Label();
            this.bImprime = new System.Windows.Forms.Button();
            this.btnFermer = new System.Windows.Forms.Button();
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.cBoxLogo = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNbBulletins)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "N° de facture";
            // 
            // tBoxNumFact
            // 
            this.tBoxNumFact.Location = new System.Drawing.Point(136, 32);
            this.tBoxNumFact.Name = "tBoxNumFact";
            this.tBoxNumFact.Size = new System.Drawing.Size(135, 22);
            this.tBoxNumFact.TabIndex = 1;
            this.tBoxNumFact.TextChanged += new System.EventHandler(this.tBoxNumFact_TextChanged);
            this.tBoxNumFact.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tBoxNumFact_KeyDown);
            // 
            // btnCherche
            // 
            this.btnCherche.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnCherche.BackgroundImage = global::ImportSosGeneve.Properties.Resources.boutonjumelles;
            this.btnCherche.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCherche.Enabled = false;
            this.btnCherche.FlatAppearance.BorderSize = 0;
            this.btnCherche.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCherche.Font = new System.Drawing.Font("Leelawadee UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCherche.Location = new System.Drawing.Point(333, 23);
            this.btnCherche.Name = "btnCherche";
            this.btnCherche.Size = new System.Drawing.Size(45, 45);
            this.btnCherche.TabIndex = 50;
            this.btnCherche.UseVisualStyleBackColor = false;
            this.btnCherche.Click += new System.EventHandler(this.btnCherche_Click);
            // 
            // LMontantFact
            // 
            this.LMontantFact.Location = new System.Drawing.Point(31, 95);
            this.LMontantFact.Name = "LMontantFact";
            this.LMontantFact.Size = new System.Drawing.Size(286, 20);
            this.LMontantFact.TabIndex = 51;
            this.LMontantFact.Text = "Montant du solde de la facture : ---";
            // 
            // numericUpDownNbBulletins
            // 
            this.numericUpDownNbBulletins.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownNbBulletins.Location = new System.Drawing.Point(172, 143);
            this.numericUpDownNbBulletins.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.numericUpDownNbBulletins.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownNbBulletins.Name = "numericUpDownNbBulletins";
            this.numericUpDownNbBulletins.Size = new System.Drawing.Size(51, 26);
            this.numericUpDownNbBulletins.TabIndex = 52;
            this.numericUpDownNbBulletins.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownNbBulletins.ValueChanged += new System.EventHandler(this.numericUpDownNbBulletins_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 149);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 16);
            this.label2.TabIndex = 53;
            this.label2.Text = "Nombre de bulletins : ";
            // 
            // lMontantbulletin
            // 
            this.lMontantbulletin.AutoSize = true;
            this.lMontantbulletin.Location = new System.Drawing.Point(34, 215);
            this.lMontantbulletin.Name = "lMontantbulletin";
            this.lMontantbulletin.Size = new System.Drawing.Size(144, 16);
            this.lMontantbulletin.TabIndex = 54;
            this.lMontantbulletin.Text = "Montant par bulletin : ---";
            // 
            // ldernierBulletin
            // 
            this.ldernierBulletin.AutoSize = true;
            this.ldernierBulletin.Location = new System.Drawing.Point(34, 260);
            this.ldernierBulletin.Name = "ldernierBulletin";
            this.ldernierBulletin.Size = new System.Drawing.Size(184, 16);
            this.ldernierBulletin.TabIndex = 55;
            this.ldernierBulletin.Text = "Montant du dernier bulletin : ---";
            // 
            // bImprime
            // 
            this.bImprime.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.bImprime.BackgroundImage = global::ImportSosGeneve.Properties.Resources.impr;
            this.bImprime.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bImprime.FlatAppearance.BorderSize = 0;
            this.bImprime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bImprime.Location = new System.Drawing.Point(174, 451);
            this.bImprime.Name = "bImprime";
            this.bImprime.Size = new System.Drawing.Size(49, 50);
            this.bImprime.TabIndex = 80;
            this.bImprime.UseVisualStyleBackColor = false;
            this.bImprime.Click += new System.EventHandler(this.bImprime_Click);
            // 
            // btnFermer
            // 
            this.btnFermer.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnFermer.BackgroundImage = global::ImportSosGeneve.Properties.Resources.close;
            this.btnFermer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnFermer.FlatAppearance.BorderSize = 0;
            this.btnFermer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFermer.Location = new System.Drawing.Point(388, 551);
            this.btnFermer.Name = "btnFermer";
            this.btnFermer.Size = new System.Drawing.Size(50, 50);
            this.btnFermer.TabIndex = 81;
            this.btnFermer.UseVisualStyleBackColor = false;
            this.btnFermer.Click += new System.EventHandler(this.btnFermer_Click);
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = -1;
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Right;
            this.crystalReportViewer1.Location = new System.Drawing.Point(471, 0);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.Size = new System.Drawing.Size(790, 624);
            this.crystalReportViewer1.TabIndex = 82;
            this.crystalReportViewer1.ToolPanelWidth = 100;
            // 
            // cBoxLogo
            // 
            this.cBoxLogo.AutoSize = true;
            this.cBoxLogo.Location = new System.Drawing.Point(37, 348);
            this.cBoxLogo.Name = "cBoxLogo";
            this.cBoxLogo.Size = new System.Drawing.Size(211, 20);
            this.cBoxLogo.TabIndex = 83;
            this.cBoxLogo.Text = "Afficher le logo (Page normale)";
            this.cBoxLogo.UseVisualStyleBackColor = true;
            // 
            // FArrangementPaiement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(1261, 624);
            this.ControlBox = false;
            this.Controls.Add(this.cBoxLogo);
            this.Controls.Add(this.crystalReportViewer1);
            this.Controls.Add(this.btnFermer);
            this.Controls.Add(this.bImprime);
            this.Controls.Add(this.ldernierBulletin);
            this.Controls.Add(this.lMontantbulletin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericUpDownNbBulletins);
            this.Controls.Add(this.LMontantFact);
            this.Controls.Add(this.btnCherche);
            this.Controls.Add(this.tBoxNumFact);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.Window;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FArrangementPaiement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Arrangement de paiement";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNbBulletins)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tBoxNumFact;
        private System.Windows.Forms.Button btnCherche;
        private System.Windows.Forms.Label LMontantFact;
        private System.Windows.Forms.NumericUpDown numericUpDownNbBulletins;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lMontantbulletin;
        private System.Windows.Forms.Label ldernierBulletin;
        private System.Windows.Forms.Button bImprime;
        private System.Windows.Forms.Button btnFermer;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private System.Windows.Forms.CheckBox cBoxLogo;
    }
}