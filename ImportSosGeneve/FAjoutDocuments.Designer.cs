namespace ImportSosGeneve
{
    partial class FAjoutDocuments
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FAjoutDocuments));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listBoxDoc = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ListTypeDoc = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bVerifier = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.bValideAjout = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.bAjout = new System.Windows.Forms.Button();
            this.bFermer = new System.Windows.Forms.Button();
            this.bSupprimer = new System.Windows.Forms.Button();
            this.bViualiser = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(11, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(11, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "label2";
            // 
            // listBoxDoc
            // 
            this.listBoxDoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxDoc.FormattingEnabled = true;
            this.listBoxDoc.ItemHeight = 16;
            this.listBoxDoc.Location = new System.Drawing.Point(15, 84);
            this.listBoxDoc.Name = "listBoxDoc";
            this.listBoxDoc.Size = new System.Drawing.Size(272, 228);
            this.listBoxDoc.TabIndex = 2;
            this.listBoxDoc.SelectedIndexChanged += new System.EventHandler(this.listBoxDoc_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(15, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(150, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "Liste des documents";
            // 
            // ListTypeDoc
            // 
            this.ListTypeDoc.Items.AddRange(new object[] {
            "Avis",
            "Analyse",
            "Certificat",
            "Constat",
            "ECG",
            "ExamensComp",
            "Prescription",
            "Radiographie",
            "Rapport",
            "Photo",
            "Autre"});
            this.ListTypeDoc.Location = new System.Drawing.Point(31, 54);
            this.ListTypeDoc.Name = "ListTypeDoc";
            this.ListTypeDoc.Size = new System.Drawing.Size(188, 24);
            this.ListTypeDoc.TabIndex = 29;
            this.ListTypeDoc.TextChanged += new System.EventHandler(this.ListTypeDoc_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(265, 16);
            this.label4.TabIndex = 30;
            this.label4.Text = "Choisir le type de document à ajouter";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bVerifier);
            this.groupBox1.Controls.Add(this.bValideAjout);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.ListTypeDoc);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.bAjout);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(401, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(356, 303);
            this.groupBox1.TabIndex = 31;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " Ajout d\'un document";
            // 
            // bVerifier
            // 
            this.bVerifier.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bVerifier.Enabled = false;
            this.bVerifier.FlatAppearance.BorderSize = 0;
            this.bVerifier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bVerifier.ImageIndex = 3;
            this.bVerifier.ImageList = this.imageList1;
            this.bVerifier.Location = new System.Drawing.Point(273, 120);
            this.bVerifier.Name = "bVerifier";
            this.bVerifier.Size = new System.Drawing.Size(75, 75);
            this.bVerifier.TabIndex = 34;
            this.bVerifier.UseVisualStyleBackColor = true;
            this.bVerifier.Click += new System.EventHandler(this.bVerifier_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "brondValider.png");
            this.imageList1.Images.SetKeyName(1, "brondValiderOff.png");
            this.imageList1.Images.SetKeyName(2, "bvoir.png");
            this.imageList1.Images.SetKeyName(3, "bvoirOff.png");
            this.imageList1.Images.SetKeyName(4, "bsupprimer.png");
            this.imageList1.Images.SetKeyName(5, "bsupprimerOff.png");
            this.imageList1.Images.SetKeyName(6, "bajout.png");
            this.imageList1.Images.SetKeyName(7, "bajoutOff.png");
            // 
            // bValideAjout
            // 
            this.bValideAjout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bValideAjout.Enabled = false;
            this.bValideAjout.FlatAppearance.BorderSize = 0;
            this.bValideAjout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bValideAjout.ImageIndex = 1;
            this.bValideAjout.ImageList = this.imageList1;
            this.bValideAjout.Location = new System.Drawing.Point(273, 222);
            this.bValideAjout.Name = "bValideAjout";
            this.bValideAjout.Size = new System.Drawing.Size(75, 75);
            this.bValideAjout.TabIndex = 33;
            this.bValideAjout.UseVisualStyleBackColor = true;
            this.bValideAjout.Click += new System.EventHandler(this.bValideAjout_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(28, 163);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 16);
            this.label6.TabIndex = 32;
            this.label6.Text = "Nom du fichier";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(28, 136);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(198, 16);
            this.label5.TabIndex = 31;
            this.label5.Text = "Vous avez choisi d\'ajouter :";
            // 
            // bAjout
            // 
            this.bAjout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bAjout.Enabled = false;
            this.bAjout.FlatAppearance.BorderSize = 0;
            this.bAjout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bAjout.ImageIndex = 7;
            this.bAjout.ImageList = this.imageList1;
            this.bAjout.Location = new System.Drawing.Point(273, 21);
            this.bAjout.Name = "bAjout";
            this.bAjout.Size = new System.Drawing.Size(75, 75);
            this.bAjout.TabIndex = 4;
            this.bAjout.UseVisualStyleBackColor = true;
            this.bAjout.Click += new System.EventHandler(this.bAjout_Click);
            // 
            // bFermer
            // 
            this.bFermer.BackgroundImage = global::ImportSosGeneve.Properties.Resources.exit;
            this.bFermer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bFermer.FlatAppearance.BorderSize = 0;
            this.bFermer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bFermer.Location = new System.Drawing.Point(682, 318);
            this.bFermer.Name = "bFermer";
            this.bFermer.Size = new System.Drawing.Size(75, 75);
            this.bFermer.TabIndex = 32;
            this.bFermer.UseVisualStyleBackColor = true;
            this.bFermer.Click += new System.EventHandler(this.bFermer_Click);
            // 
            // bSupprimer
            // 
            this.bSupprimer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bSupprimer.Enabled = false;
            this.bSupprimer.FlatAppearance.BorderSize = 0;
            this.bSupprimer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bSupprimer.ImageIndex = 5;
            this.bSupprimer.ImageList = this.imageList1;
            this.bSupprimer.Location = new System.Drawing.Point(293, 237);
            this.bSupprimer.Name = "bSupprimer";
            this.bSupprimer.Size = new System.Drawing.Size(75, 75);
            this.bSupprimer.TabIndex = 6;
            this.bSupprimer.UseVisualStyleBackColor = true;
            this.bSupprimer.Click += new System.EventHandler(this.bSupprimer_Click);
            // 
            // bViualiser
            // 
            this.bViualiser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bViualiser.Enabled = false;
            this.bViualiser.FlatAppearance.BorderSize = 0;
            this.bViualiser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bViualiser.ImageIndex = 3;
            this.bViualiser.ImageList = this.imageList1;
            this.bViualiser.Location = new System.Drawing.Point(293, 84);
            this.bViualiser.Name = "bViualiser";
            this.bViualiser.Size = new System.Drawing.Size(75, 75);
            this.bViualiser.TabIndex = 5;
            this.bViualiser.UseVisualStyleBackColor = true;
            this.bViualiser.Click += new System.EventHandler(this.bViualiser_Click);
            // 
            // FAjoutDocuments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.ClientSize = new System.Drawing.Size(770, 405);
            this.Controls.Add(this.bFermer);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.bSupprimer);
            this.Controls.Add(this.bViualiser);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listBoxDoc);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FAjoutDocuments";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " ";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listBoxDoc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button bAjout;
        private System.Windows.Forms.Button bViualiser;
        private System.Windows.Forms.Button bSupprimer;
        private System.Windows.Forms.ComboBox ListTypeDoc;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button bFermer;
        private System.Windows.Forms.Button bValideAjout;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button bVerifier;
    }
}