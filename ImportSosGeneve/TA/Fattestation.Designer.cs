namespace ImportSosGeneve.TA
{
    partial class Fattestation
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Fattestation));
            this.listBoxTA = new System.Windows.Forms.ListBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.bFermer = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dTDateNaissance = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.bcherche = new System.Windows.Forms.Button();
            this.bImprime = new System.Windows.Forms.Button();
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.SuspendLayout();
            // 
            // listBoxTA
            // 
            this.listBoxTA.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxTA.FormattingEnabled = true;
            this.listBoxTA.ItemHeight = 16;
            this.listBoxTA.Location = new System.Drawing.Point(12, 137);
            this.listBoxTA.Name = "listBoxTA";
            this.listBoxTA.Size = new System.Drawing.Size(357, 228);
            this.listBoxTA.TabIndex = 3;
            this.listBoxTA.SelectedIndexChanged += new System.EventHandler(this.listBoxTA_SelectedIndexChanged);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "bRecherche.png");
            this.imageList1.Images.SetKeyName(1, "bImprime.png");
            // 
            // bFermer
            // 
            this.bFermer.BackgroundImage = global::ImportSosGeneve.Properties.Resources.exit;
            this.bFermer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bFermer.FlatAppearance.BorderSize = 0;
            this.bFermer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bFermer.Location = new System.Drawing.Point(394, 461);
            this.bFermer.Name = "bFermer";
            this.bFermer.Size = new System.Drawing.Size(75, 75);
            this.bFermer.TabIndex = 33;
            this.bFermer.UseVisualStyleBackColor = true;
            this.bFermer.Click += new System.EventHandler(this.bFermer_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(14, 36);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(355, 22);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(239, 16);
            this.label1.TabIndex = 35;
            this.label1.Text = "Recherche par nom du patient TA";
            // 
            // dTDateNaissance
            // 
            this.dTDateNaissance.CustomFormat = "dd.MM.yyyy";
            this.dTDateNaissance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dTDateNaissance.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dTDateNaissance.Location = new System.Drawing.Point(99, 89);
            this.dTDateNaissance.Name = "dTDateNaissance";
            this.dTDateNaissance.Size = new System.Drawing.Size(98, 22);
            this.dTDateNaissance.TabIndex = 1;
            this.dTDateNaissance.ValueChanged += new System.EventHandler(this.dTDateNaissance_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(52, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(196, 16);
            this.label2.TabIndex = 37;
            this.label2.Text = "...ou par date de naissance";
            // 
            // bcherche
            // 
            this.bcherche.BackgroundImage = global::ImportSosGeneve.Properties.Resources.exit;
            this.bcherche.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bcherche.FlatAppearance.BorderSize = 0;
            this.bcherche.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bcherche.ImageIndex = 0;
            this.bcherche.ImageList = this.imageList1;
            this.bcherche.Location = new System.Drawing.Point(394, 36);
            this.bcherche.Name = "bcherche";
            this.bcherche.Size = new System.Drawing.Size(75, 75);
            this.bcherche.TabIndex = 38;
            this.bcherche.UseVisualStyleBackColor = true;
            // 
            // bImprime
            // 
            this.bImprime.BackgroundImage = global::ImportSosGeneve.Properties.Resources.exit;
            this.bImprime.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bImprime.Enabled = false;
            this.bImprime.FlatAppearance.BorderSize = 0;
            this.bImprime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bImprime.ImageIndex = 1;
            this.bImprime.ImageList = this.imageList1;
            this.bImprime.Location = new System.Drawing.Point(394, 231);
            this.bImprime.Name = "bImprime";
            this.bImprime.Size = new System.Drawing.Size(75, 75);
            this.bImprime.TabIndex = 39;
            this.bImprime.UseVisualStyleBackColor = true;
            this.bImprime.Click += new System.EventHandler(this.bImprime_Click);
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = -1;
            this.crystalReportViewer1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer1.Location = new System.Drawing.Point(489, -1);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.ShowGroupTreeButton = false;
            this.crystalReportViewer1.Size = new System.Drawing.Size(555, 569);
            this.crystalReportViewer1.TabIndex = 40;
            this.crystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // Fattestation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.ClientSize = new System.Drawing.Size(1048, 569);
            this.Controls.Add(this.crystalReportViewer1);
            this.Controls.Add(this.bImprime);
            this.Controls.Add(this.bcherche);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dTDateNaissance);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.bFermer);
            this.Controls.Add(this.listBoxTA);
            this.Name = "Fattestation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Attestations TA";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxTA;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button bFermer;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dTDateNaissance;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bcherche;
        private System.Windows.Forms.Button bImprime;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private System.Windows.Forms.PrintDialog printDialog1;
    }
}