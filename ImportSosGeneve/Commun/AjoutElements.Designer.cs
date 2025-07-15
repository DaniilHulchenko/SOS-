
namespace ImportSosGeneve.Commun
{
    partial class AjoutElements
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
            this.btnAnnuler = new System.Windows.Forms.Button();
            this.btnValider = new System.Windows.Forms.Button();
            this.listViewAjout = new System.Windows.Forms.ListView();
            this.listViewListe = new System.Windows.Forms.ListView();
            this.btnRetirer = new System.Windows.Forms.Button();
            this.btnAjouter = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAnnuler
            // 
            this.btnAnnuler.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnAnnuler.BackgroundImage = global::ImportSosGeneve.Properties.Resources.close;
            this.btnAnnuler.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAnnuler.FlatAppearance.BorderSize = 0;
            this.btnAnnuler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnnuler.Location = new System.Drawing.Point(329, 166);
            this.btnAnnuler.Name = "btnAnnuler";
            this.btnAnnuler.Size = new System.Drawing.Size(45, 45);
            this.btnAnnuler.TabIndex = 2;
            this.btnAnnuler.UseVisualStyleBackColor = false;
            this.btnAnnuler.Click += new System.EventHandler(this.btnAnnuler_Click);
            // 
            // btnValider
            // 
            this.btnValider.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnValider.BackgroundImage = global::ImportSosGeneve.Properties.Resources.check;
            this.btnValider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnValider.FlatAppearance.BorderSize = 0;
            this.btnValider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnValider.Location = new System.Drawing.Point(329, 222);
            this.btnValider.Name = "btnValider";
            this.btnValider.Size = new System.Drawing.Size(45, 45);
            this.btnValider.TabIndex = 3;
            this.btnValider.UseVisualStyleBackColor = false;
            this.btnValider.Click += new System.EventHandler(this.btnValider_Click);
            // 
            // listViewAjout
            // 
            this.listViewAjout.Dock = System.Windows.Forms.DockStyle.Right;
            this.listViewAjout.Font = new System.Drawing.Font("Leelawadee UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewAjout.FullRowSelect = true;
            this.listViewAjout.HideSelection = false;
            this.listViewAjout.Location = new System.Drawing.Point(397, 0);
            this.listViewAjout.MultiSelect = false;
            this.listViewAjout.Name = "listViewAjout";
            this.listViewAjout.Size = new System.Drawing.Size(291, 293);
            this.listViewAjout.TabIndex = 1;
            this.listViewAjout.UseCompatibleStateImageBehavior = false;
            this.listViewAjout.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewAjout_MouseDoubleClick);
            // 
            // listViewListe
            // 
            this.listViewListe.Dock = System.Windows.Forms.DockStyle.Left;
            this.listViewListe.Font = new System.Drawing.Font("Leelawadee UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewListe.FullRowSelect = true;
            this.listViewListe.HideSelection = false;
            this.listViewListe.Location = new System.Drawing.Point(0, 0);
            this.listViewListe.MultiSelect = false;
            this.listViewListe.Name = "listViewListe";
            this.listViewListe.Size = new System.Drawing.Size(309, 293);
            this.listViewListe.TabIndex = 0;
            this.listViewListe.UseCompatibleStateImageBehavior = false;
            this.listViewListe.View = System.Windows.Forms.View.List;
            this.listViewListe.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewListe_MouseDoubleClick);
            // 
            // btnRetirer
            // 
            this.btnRetirer.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnRetirer.BackgroundImage = global::ImportSosGeneve.Properties.Resources.fleche_gauche_rouge;
            this.btnRetirer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRetirer.FlatAppearance.BorderSize = 0;
            this.btnRetirer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRetirer.Location = new System.Drawing.Point(329, 54);
            this.btnRetirer.Name = "btnRetirer";
            this.btnRetirer.Size = new System.Drawing.Size(45, 45);
            this.btnRetirer.TabIndex = 4;
            this.btnRetirer.UseVisualStyleBackColor = false;
            this.btnRetirer.Click += new System.EventHandler(this.btnRetirer_Click);
            // 
            // btnAjouter
            // 
            this.btnAjouter.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnAjouter.BackgroundImage = global::ImportSosGeneve.Properties.Resources.fleche_droite_verte;
            this.btnAjouter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAjouter.FlatAppearance.BorderSize = 0;
            this.btnAjouter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAjouter.Location = new System.Drawing.Point(329, 3);
            this.btnAjouter.Name = "btnAjouter";
            this.btnAjouter.Size = new System.Drawing.Size(45, 45);
            this.btnAjouter.TabIndex = 5;
            this.btnAjouter.UseVisualStyleBackColor = false;
            this.btnAjouter.Click += new System.EventHandler(this.btnAjouter_Click);
            // 
            // AjoutElements
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(688, 293);
            this.Controls.Add(this.listViewListe);
            this.Controls.Add(this.listViewAjout);
            this.Controls.Add(this.btnAnnuler);
            this.Controls.Add(this.btnValider);
            this.Controls.Add(this.btnRetirer);
            this.Controls.Add(this.btnAjouter);
            this.ForeColor = System.Drawing.SystemColors.Window;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AjoutElements";
            this.ShowIcon = false;
            this.Text = "Ajouter";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AjoutElements_FormClosing);
            this.Load += new System.EventHandler(this.AjouterActesTech_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnAjouter;
        private System.Windows.Forms.Button btnRetirer;
        private System.Windows.Forms.Button btnValider;
        private System.Windows.Forms.Button btnAnnuler;
        private System.Windows.Forms.ListView listViewAjout;
        private System.Windows.Forms.ListView listViewListe;
    }
}