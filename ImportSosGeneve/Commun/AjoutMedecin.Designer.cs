
namespace ImportSosGeneve.Commun
{
    partial class AjoutMedecin
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
            this.btAjouter = new System.Windows.Forms.Button();
            this.tbCodeMedecin = new System.Windows.Forms.TextBox();
            this.tbMail = new System.Windows.Forms.TextBox();
            this.tbNom = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbPrenom = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbEAN = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.cbDesactive = new System.Windows.Forms.CheckBox();
            this.cbMedInterne = new System.Windows.Forms.CheckBox();
            this.cbIndependant = new System.Windows.Forms.CheckBox();
            this.tbInitiale = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.rtbCommentaire = new System.Windows.Forms.RichTextBox();
            this.cbArchive = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // btAjouter
            // 
            this.btAjouter.Location = new System.Drawing.Point(102, 216);
            this.btAjouter.Name = "btAjouter";
            this.btAjouter.Size = new System.Drawing.Size(102, 23);
            this.btAjouter.TabIndex = 0;
            this.btAjouter.Text = "Ajouter";
            this.btAjouter.UseVisualStyleBackColor = true;
            this.btAjouter.Click += new System.EventHandler(this.btAjouter_Click);
            // 
            // tbCodeMedecin
            // 
            this.tbCodeMedecin.Location = new System.Drawing.Point(102, 40);
            this.tbCodeMedecin.MinimumSize = new System.Drawing.Size(50, 20);
            this.tbCodeMedecin.Name = "tbCodeMedecin";
            this.tbCodeMedecin.Size = new System.Drawing.Size(201, 20);
            this.tbCodeMedecin.TabIndex = 1;
            this.tbCodeMedecin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbCodeMedecin_KeyPress);
            this.tbCodeMedecin.Validating += new System.ComponentModel.CancelEventHandler(this.tbCodeMedecin_Validating);
            // 
            // tbMail
            // 
            this.tbMail.Location = new System.Drawing.Point(102, 145);
            this.tbMail.MaxLength = 255;
            this.tbMail.MinimumSize = new System.Drawing.Size(50, 20);
            this.tbMail.Name = "tbMail";
            this.tbMail.Size = new System.Drawing.Size(201, 20);
            this.tbMail.TabIndex = 1;
            this.tbMail.Validating += new System.ComponentModel.CancelEventHandler(this.tbMail_Validating);
            // 
            // tbNom
            // 
            this.tbNom.Location = new System.Drawing.Point(102, 66);
            this.tbNom.MaxLength = 50;
            this.tbNom.MinimumSize = new System.Drawing.Size(50, 20);
            this.tbNom.Name = "tbNom";
            this.tbNom.Size = new System.Drawing.Size(201, 20);
            this.tbNom.TabIndex = 1;
            this.tbNom.Validating += new System.ComponentModel.CancelEventHandler(this.tbNom_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "CodeIntervenant";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 148);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Mail";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 69);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Nom";
            // 
            // tbPrenom
            // 
            this.tbPrenom.Location = new System.Drawing.Point(102, 91);
            this.tbPrenom.MaxLength = 50;
            this.tbPrenom.MinimumSize = new System.Drawing.Size(50, 20);
            this.tbPrenom.Name = "tbPrenom";
            this.tbPrenom.Size = new System.Drawing.Size(201, 20);
            this.tbPrenom.TabIndex = 1;
            this.tbPrenom.Validating += new System.ComponentModel.CancelEventHandler(this.tbPrenom_Validating);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 94);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Prenom";
            // 
            // tbEAN
            // 
            this.tbEAN.Location = new System.Drawing.Point(102, 171);
            this.tbEAN.MaxLength = 20;
            this.tbEAN.MinimumSize = new System.Drawing.Size(50, 20);
            this.tbEAN.Name = "tbEAN";
            this.tbEAN.Size = new System.Drawing.Size(201, 20);
            this.tbEAN.TabIndex = 1;
            this.tbEAN.Validating += new System.ComponentModel.CancelEventHandler(this.tbEAN_Validating);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 174);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "EAN";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(341, 123);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(63, 13);
            this.label16.TabIndex = 15;
            this.label16.Text = "DateMajCpt";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(341, 43);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(68, 13);
            this.label19.TabIndex = 21;
            this.label19.Text = "Commentaire";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(431, 117);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(225, 20);
            this.dateTimePicker1.TabIndex = 24;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // cbDesactive
            // 
            this.cbDesactive.AutoSize = true;
            this.cbDesactive.Location = new System.Drawing.Point(344, 148);
            this.cbDesactive.Name = "cbDesactive";
            this.cbDesactive.Size = new System.Drawing.Size(74, 17);
            this.cbDesactive.TabIndex = 25;
            this.cbDesactive.Text = "Desactive";
            this.cbDesactive.UseVisualStyleBackColor = true;
            // 
            // cbMedInterne
            // 
            this.cbMedInterne.AutoSize = true;
            this.cbMedInterne.Location = new System.Drawing.Point(431, 148);
            this.cbMedInterne.Name = "cbMedInterne";
            this.cbMedInterne.Size = new System.Drawing.Size(80, 17);
            this.cbMedInterne.TabIndex = 25;
            this.cbMedInterne.Text = "MedInterne";
            this.cbMedInterne.UseVisualStyleBackColor = true;
            // 
            // cbIndependant
            // 
            this.cbIndependant.AutoSize = true;
            this.cbIndependant.Location = new System.Drawing.Point(517, 148);
            this.cbIndependant.Name = "cbIndependant";
            this.cbIndependant.Size = new System.Drawing.Size(86, 17);
            this.cbIndependant.TabIndex = 25;
            this.cbIndependant.Text = "Independant";
            this.cbIndependant.UseVisualStyleBackColor = true;
            // 
            // tbInitiale
            // 
            this.tbInitiale.Location = new System.Drawing.Point(102, 117);
            this.tbInitiale.MaxLength = 3;
            this.tbInitiale.MinimumSize = new System.Drawing.Size(50, 20);
            this.tbInitiale.Name = "tbInitiale";
            this.tbInitiale.Size = new System.Drawing.Size(201, 20);
            this.tbInitiale.TabIndex = 1;
            this.tbInitiale.Validating += new System.ComponentModel.CancelEventHandler(this.tbInitiale_Validating);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 120);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(37, 13);
            this.label11.TabIndex = 2;
            this.label11.Text = "Initiale";
            // 
            // rtbCommentaire
            // 
            this.rtbCommentaire.Location = new System.Drawing.Point(431, 43);
            this.rtbCommentaire.Name = "rtbCommentaire";
            this.rtbCommentaire.Size = new System.Drawing.Size(225, 56);
            this.rtbCommentaire.TabIndex = 26;
            this.rtbCommentaire.Text = "";
            // 
            // cbArchive
            // 
            this.cbArchive.AutoSize = true;
            this.cbArchive.Location = new System.Drawing.Point(517, 171);
            this.cbArchive.Name = "cbArchive";
            this.cbArchive.Size = new System.Drawing.Size(62, 17);
            this.cbArchive.TabIndex = 27;
            this.cbArchive.Text = "Archive";
            this.cbArchive.UseVisualStyleBackColor = true;
            // 
            // AjoutMedecin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(722, 300);
            this.Controls.Add(this.cbArchive);
            this.Controls.Add(this.rtbCommentaire);
            this.Controls.Add(this.cbIndependant);
            this.Controls.Add(this.cbMedInterne);
            this.Controls.Add(this.cbDesactive);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.tbEAN);
            this.Controls.Add(this.tbPrenom);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbNom);
            this.Controls.Add(this.tbMail);
            this.Controls.Add(this.tbInitiale);
            this.Controls.Add(this.tbCodeMedecin);
            this.Controls.Add(this.btAjouter);
            this.Name = "AjoutMedecin";
            this.Text = "Ajout médecin";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btAjouter;
        private System.Windows.Forms.TextBox tbCodeMedecin;
        private System.Windows.Forms.TextBox tbMail;
        private System.Windows.Forms.TextBox tbNom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbPrenom;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbEAN;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.CheckBox cbIndependant;
        private System.Windows.Forms.CheckBox cbMedInterne;
        private System.Windows.Forms.CheckBox cbDesactive;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbInitiale;
        private System.Windows.Forms.RichTextBox rtbCommentaire;
        private System.Windows.Forms.CheckBox cbArchive;
    }
}