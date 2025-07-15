
namespace ImportSosGeneve.Facture
{
    partial class ImpressionDebiteurs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImpressionDebiteurs));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.bQuitter = new System.Windows.Forms.Button();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.bGenerer = new System.Windows.Forms.Button();
            this.rB2 = new System.Windows.Forms.RadioButton();
            this.rB1 = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.dTPickerFin = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dTPickerDeb = new System.Windows.Forms.DateTimePicker();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.splitContainer1.Panel1.Controls.Add(this.bQuitter);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.progressBar1);
            this.splitContainer1.Panel1.Controls.Add(this.pictureBox1);
            this.splitContainer1.Panel1.Controls.Add(this.bGenerer);
            this.splitContainer1.Panel1.Controls.Add(this.rB2);
            this.splitContainer1.Panel1.Controls.Add(this.rB1);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.dTPickerFin);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.dTPickerDeb);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.splitContainer1.Size = new System.Drawing.Size(727, 542);
            this.splitContainer1.SplitterDistance = 527;
            this.splitContainer1.TabIndex = 0;
            // 
            // bQuitter
            // 
            this.bQuitter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bQuitter.FlatAppearance.BorderSize = 0;
            this.bQuitter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bQuitter.ImageIndex = 0;
            this.bQuitter.ImageList = this.imageList2;
            this.bQuitter.Location = new System.Drawing.Point(363, 455);
            this.bQuitter.Name = "bQuitter";
            this.bQuitter.Size = new System.Drawing.Size(75, 75);
            this.bQuitter.TabIndex = 35;
            this.bQuitter.UseVisualStyleBackColor = true;
            this.bQuitter.Click += new System.EventHandler(this.bQuitter_Click);
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "bondCancel.png");
            this.imageList2.Images.SetKeyName(1, "bExporter.png");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 259);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 34;
            this.label3.Text = "action :";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(22, 294);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(380, 23);
            this.progressBar1.TabIndex = 33;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ImportSosGeneve.Properties.Resources.smiley;
            this.pictureBox1.Location = new System.Drawing.Point(141, 356);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(118, 78);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 32;
            this.pictureBox1.TabStop = false;
            // 
            // bGenerer
            // 
            this.bGenerer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bGenerer.FlatAppearance.BorderSize = 0;
            this.bGenerer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bGenerer.ImageIndex = 1;
            this.bGenerer.ImageList = this.imageList2;
            this.bGenerer.Location = new System.Drawing.Point(164, 152);
            this.bGenerer.Name = "bGenerer";
            this.bGenerer.Size = new System.Drawing.Size(75, 75);
            this.bGenerer.TabIndex = 6;
            this.bGenerer.UseVisualStyleBackColor = true;
            this.bGenerer.Click += new System.EventHandler(this.bGenerer_Click);
            // 
            // rB2
            // 
            this.rB2.AutoSize = true;
            this.rB2.Location = new System.Drawing.Point(50, 129);
            this.rB2.Name = "rB2";
            this.rB2.Size = new System.Drawing.Size(47, 17);
            this.rB2.TabIndex = 5;
            this.rB2.Text = "XML";
            this.rB2.UseVisualStyleBackColor = true;
            // 
            // rB1
            // 
            this.rB1.AutoSize = true;
            this.rB1.Checked = true;
            this.rB1.Location = new System.Drawing.Point(50, 106);
            this.rB1.Name = "rB1";
            this.rB1.Size = new System.Drawing.Size(46, 17);
            this.rB1.TabIndex = 4;
            this.rB1.TabStop = true;
            this.rB1.Text = "PDF";
            this.rB1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(229, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "jusqu\'au :";
            // 
            // dTPickerFin
            // 
            this.dTPickerFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dTPickerFin.Location = new System.Drawing.Point(227, 57);
            this.dTPickerFin.Name = "dTPickerFin";
            this.dTPickerFin.Size = new System.Drawing.Size(129, 20);
            this.dTPickerFin.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Depuis le :";
            // 
            // dTPickerDeb
            // 
            this.dTPickerDeb.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dTPickerDeb.Location = new System.Drawing.Point(50, 57);
            this.dTPickerDeb.Name = "dTPickerDeb";
            this.dTPickerDeb.Size = new System.Drawing.Size(129, 20);
            this.dTPickerDeb.TabIndex = 0;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "smiley 1ok.png");
            this.imageList1.Images.SetKeyName(1, "Smiley_2ok.png");
            this.imageList1.Images.SetKeyName(2, "Smiley_Embete1.png");
            this.imageList1.Images.SetKeyName(3, "Smiley_pasOk.png");
            this.imageList1.Images.SetKeyName(4, "Smiley_reflechi1.png");
            // 
            // ImpressionDebiteurs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(727, 542);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ImpressionDebiteurs";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Export des factures pour mise en contentieux";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dTPickerFin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dTPickerDeb;
        private System.Windows.Forms.RadioButton rB2;
        private System.Windows.Forms.RadioButton rB1;
        private System.Windows.Forms.Button bGenerer;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button bQuitter;
        private System.Windows.Forms.ImageList imageList2;
    }
}