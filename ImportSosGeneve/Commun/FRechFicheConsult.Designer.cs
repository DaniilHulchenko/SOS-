
namespace ImportSosGeneve.Commun
{
    partial class FRechFicheConsult
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRechFicheConsult));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dTimePFin = new System.Windows.Forms.DateTimePicker();
            this.dTimePDeb = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lCodeMedecin = new System.Windows.Forms.Label();
            this.tBoxMedecin = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.gBox1 = new System.Windows.Forms.GroupBox();
            this.rB3 = new System.Windows.Forms.RadioButton();
            this.rB2 = new System.Windows.Forms.RadioButton();
            this.rB0 = new System.Windows.Forms.RadioButton();
            this.bRecherche = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.bFermer = new System.Windows.Forms.Button();
            this.tBNumConsult = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.gBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dTimePFin);
            this.splitContainer1.Panel1.Controls.Add(this.dTimePDeb);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.lCodeMedecin);
            this.splitContainer1.Panel1.Controls.Add(this.tBoxMedecin);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.gBox1);
            this.splitContainer1.Panel1.Controls.Add(this.bRecherche);
            this.splitContainer1.Panel1.Controls.Add(this.bFermer);
            this.splitContainer1.Panel1.Controls.Add(this.tBNumConsult);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listView1);
            this.splitContainer1.Size = new System.Drawing.Size(936, 649);
            this.splitContainer1.SplitterDistance = 154;
            this.splitContainer1.TabIndex = 0;
            // 
            // dTimePFin
            // 
            this.dTimePFin.CalendarForeColor = System.Drawing.SystemColors.Window;
            this.dTimePFin.CalendarMonthBackground = System.Drawing.SystemColors.ControlDarkDark;
            this.dTimePFin.CalendarTitleForeColor = System.Drawing.SystemColors.Window;
            this.dTimePFin.Location = new System.Drawing.Point(468, 114);
            this.dTimePFin.Name = "dTimePFin";
            this.dTimePFin.Size = new System.Drawing.Size(200, 20);
            this.dTimePFin.TabIndex = 91;
            // 
            // dTimePDeb
            // 
            this.dTimePDeb.CalendarForeColor = System.Drawing.SystemColors.Window;
            this.dTimePDeb.CalendarMonthBackground = System.Drawing.SystemColors.ControlDarkDark;
            this.dTimePDeb.CalendarTitleForeColor = System.Drawing.SystemColors.Window;
            this.dTimePDeb.Location = new System.Drawing.Point(123, 113);
            this.dTimePDeb.Name = "dTimePDeb";
            this.dTimePDeb.Size = new System.Drawing.Size(200, 20);
            this.dTimePDeb.TabIndex = 90;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(384, 119);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 16);
            this.label6.TabIndex = 93;
            this.label6.Text = "Date de fin :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(24, 118);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 16);
            this.label5.TabIndex = 92;
            this.label5.Text = "Date de début :";
            // 
            // lCodeMedecin
            // 
            this.lCodeMedecin.AutoSize = true;
            this.lCodeMedecin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lCodeMedecin.Location = new System.Drawing.Point(329, 53);
            this.lCodeMedecin.Name = "lCodeMedecin";
            this.lCodeMedecin.Size = new System.Drawing.Size(19, 16);
            this.lCodeMedecin.TabIndex = 89;
            this.lCodeMedecin.Text = "-1";
            // 
            // tBoxMedecin
            // 
            this.tBoxMedecin.BackColor = System.Drawing.SystemColors.ControlDark;
            this.tBoxMedecin.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tBoxMedecin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tBoxMedecin.ForeColor = System.Drawing.SystemColors.Window;
            this.tBoxMedecin.Location = new System.Drawing.Point(144, 53);
            this.tBoxMedecin.Name = "tBoxMedecin";
            this.tBoxMedecin.Size = new System.Drawing.Size(177, 15);
            this.tBoxMedecin.TabIndex = 87;
            this.tBoxMedecin.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tBoxMedecin_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(40, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 16);
            this.label3.TabIndex = 88;
            this.label3.Text = "Médecin <F5> :";
            // 
            // gBox1
            // 
            this.gBox1.Controls.Add(this.rB3);
            this.gBox1.Controls.Add(this.rB2);
            this.gBox1.Controls.Add(this.rB0);
            this.gBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gBox1.ForeColor = System.Drawing.Color.White;
            this.gBox1.Location = new System.Drawing.Point(468, 18);
            this.gBox1.Name = "gBox1";
            this.gBox1.Size = new System.Drawing.Size(190, 81);
            this.gBox1.TabIndex = 72;
            this.gBox1.TabStop = false;
            this.gBox1.Text = "Etat de la fiche";
            // 
            // rB3
            // 
            this.rB3.AutoSize = true;
            this.rB3.Location = new System.Drawing.Point(100, 49);
            this.rB3.Name = "rB3";
            this.rB3.Size = new System.Drawing.Size(76, 20);
            this.rB3.TabIndex = 3;
            this.rB3.Text = "Terminé";
            this.rB3.UseVisualStyleBackColor = true;
            // 
            // rB2
            // 
            this.rB2.AutoSize = true;
            this.rB2.Location = new System.Drawing.Point(100, 22);
            this.rB2.Name = "rB2";
            this.rB2.Size = new System.Drawing.Size(85, 20);
            this.rB2.TabIndex = 2;
            this.rB2.Text = "En attente";
            this.rB2.UseVisualStyleBackColor = true;
            // 
            // rB0
            // 
            this.rB0.AutoSize = true;
            this.rB0.Checked = true;
            this.rB0.Location = new System.Drawing.Point(7, 19);
            this.rB0.Name = "rB0";
            this.rB0.Size = new System.Drawing.Size(71, 20);
            this.rB0.TabIndex = 0;
            this.rB0.TabStop = true;
            this.rB0.Text = "A traiter";
            this.rB0.UseVisualStyleBackColor = true;
            // 
            // bRecherche
            // 
            this.bRecherche.FlatAppearance.BorderSize = 0;
            this.bRecherche.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bRecherche.ImageIndex = 1;
            this.bRecherche.ImageList = this.imageList1;
            this.bRecherche.Location = new System.Drawing.Point(722, 29);
            this.bRecherche.Name = "bRecherche";
            this.bRecherche.Size = new System.Drawing.Size(64, 64);
            this.bRecherche.TabIndex = 69;
            this.bRecherche.UseVisualStyleBackColor = true;
            this.bRecherche.Click += new System.EventHandler(this.bRecherche_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "bExitOn.png");
            this.imageList1.Images.SetKeyName(1, "bRecherche.png");
            // 
            // bFermer
            // 
            this.bFermer.FlatAppearance.BorderSize = 0;
            this.bFermer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bFermer.ImageIndex = 0;
            this.bFermer.ImageList = this.imageList1;
            this.bFermer.Location = new System.Drawing.Point(852, 28);
            this.bFermer.Name = "bFermer";
            this.bFermer.Size = new System.Drawing.Size(64, 64);
            this.bFermer.TabIndex = 70;
            this.bFermer.UseVisualStyleBackColor = true;
            this.bFermer.Click += new System.EventHandler(this.bFermer_Click);
            // 
            // tBNumConsult
            // 
            this.tBNumConsult.BackColor = System.Drawing.SystemColors.ControlDark;
            this.tBNumConsult.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tBNumConsult.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tBNumConsult.ForeColor = System.Drawing.SystemColors.Window;
            this.tBNumConsult.Location = new System.Drawing.Point(144, 19);
            this.tBNumConsult.Name = "tBNumConsult";
            this.tBNumConsult.Size = new System.Drawing.Size(179, 15);
            this.tBNumConsult.TabIndex = 68;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(20, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 16);
            this.label2.TabIndex = 71;
            this.label2.Text = "N° de consultation :";
            // 
            // listView1
            // 
            this.listView1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView1.ForeColor = System.Drawing.SystemColors.Window;
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(936, 491);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.List;
            this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick);
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            // 
            // FRechFicheConsult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(936, 649);
            this.ControlBox = false;
            this.Controls.Add(this.splitContainer1);
            this.ForeColor = System.Drawing.SystemColors.Window;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "FRechFicheConsult";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Recherche de Fiche de Consultation";
            this.Load += new System.EventHandler(this.FRechFicheConsult_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.gBox1.ResumeLayout(false);
            this.gBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox gBox1;
        private System.Windows.Forms.Button bRecherche;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button bFermer;
        private System.Windows.Forms.TextBox tBNumConsult;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rB3;
        private System.Windows.Forms.RadioButton rB2;
        private System.Windows.Forms.RadioButton rB0;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label lCodeMedecin;
        private System.Windows.Forms.TextBox tBoxMedecin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dTimePFin;
        private System.Windows.Forms.DateTimePicker dTimePDeb;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
    }
}