
namespace ImportSosGeneve.Commun
{
    partial class FormRapport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRapport));
            this.label1 = new System.Windows.Forms.Label();
            this.lblNumVisite = new System.Windows.Forms.Label();
            this.lNumRapport = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.tboxConcerne = new System.Windows.Forms.TextBox();
            this.tBoxDestine = new System.Windows.Forms.TextBox();
            this.tboxContenu = new System.Windows.Forms.TextBox();
            this.groupBox40 = new System.Windows.Forms.GroupBox();
            this.lDateR = new System.Windows.Forms.Label();
            this.groupBox40.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Leelawadee UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(794, 32);
            this.label1.TabIndex = 23;
            this.label1.Text = "RAPPORT";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNumVisite
            // 
            this.lblNumVisite.AutoSize = true;
            this.lblNumVisite.BackColor = System.Drawing.SystemColors.ControlDark;
            this.lblNumVisite.Font = new System.Drawing.Font("Leelawadee UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumVisite.Location = new System.Drawing.Point(9, 5);
            this.lblNumVisite.Name = "lblNumVisite";
            this.lblNumVisite.Size = new System.Drawing.Size(138, 21);
            this.lblNumVisite.TabIndex = 0;
            this.lblNumVisite.Text = "N° de consultation";
            // 
            // lNumRapport
            // 
            this.lNumRapport.AutoSize = true;
            this.lNumRapport.BackColor = System.Drawing.SystemColors.ControlDark;
            this.lNumRapport.Font = new System.Drawing.Font("Leelawadee UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lNumRapport.Location = new System.Drawing.Point(584, 8);
            this.lNumRapport.Name = "lNumRapport";
            this.lNumRapport.Size = new System.Drawing.Size(27, 21);
            this.lNumRapport.TabIndex = 66;
            this.lNumRapport.Text = "R: ";
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.SystemColors.ControlDark;
            this.label15.Font = new System.Drawing.Font("Leelawadee UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(0, 32);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(794, 32);
            this.label15.TabIndex = 67;
            this.label15.Text = "Concerne/Destinataire";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tboxConcerne
            // 
            this.tboxConcerne.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tboxConcerne.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.tboxConcerne.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tboxConcerne.ForeColor = System.Drawing.SystemColors.Window;
            this.tboxConcerne.Location = new System.Drawing.Point(5, 67);
            this.tboxConcerne.MaxLength = 10000;
            this.tboxConcerne.Multiline = true;
            this.tboxConcerne.Name = "tboxConcerne";
            this.tboxConcerne.ReadOnly = true;
            this.tboxConcerne.Size = new System.Drawing.Size(374, 117);
            this.tboxConcerne.TabIndex = 68;
            // 
            // tBoxDestine
            // 
            this.tBoxDestine.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.tBoxDestine.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tBoxDestine.ForeColor = System.Drawing.SystemColors.Window;
            this.tBoxDestine.Location = new System.Drawing.Point(385, 67);
            this.tBoxDestine.MaxLength = 10000;
            this.tBoxDestine.Multiline = true;
            this.tBoxDestine.Name = "tBoxDestine";
            this.tBoxDestine.ReadOnly = true;
            this.tBoxDestine.Size = new System.Drawing.Size(409, 117);
            this.tBoxDestine.TabIndex = 69;
            // 
            // tboxContenu
            // 
            this.tboxContenu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tboxContenu.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.tboxContenu.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tboxContenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tboxContenu.ForeColor = System.Drawing.SystemColors.Window;
            this.tboxContenu.Location = new System.Drawing.Point(10, 17);
            this.tboxContenu.MaxLength = 10000;
            this.tboxContenu.Multiline = true;
            this.tboxContenu.Name = "tboxContenu";
            this.tboxContenu.ReadOnly = true;
            this.tboxContenu.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tboxContenu.Size = new System.Drawing.Size(773, 620);
            this.tboxContenu.TabIndex = 12;
            // 
            // groupBox40
            // 
            this.groupBox40.Controls.Add(this.tboxContenu);
            this.groupBox40.Font = new System.Drawing.Font("Leelawadee UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox40.ForeColor = System.Drawing.SystemColors.Window;
            this.groupBox40.Location = new System.Drawing.Point(5, 220);
            this.groupBox40.Name = "groupBox40";
            this.groupBox40.Size = new System.Drawing.Size(789, 645);
            this.groupBox40.TabIndex = 71;
            this.groupBox40.TabStop = false;
            // 
            // lDateR
            // 
            this.lDateR.BackColor = System.Drawing.SystemColors.ControlDark;
            this.lDateR.Font = new System.Drawing.Font("Leelawadee UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lDateR.Location = new System.Drawing.Point(0, 187);
            this.lDateR.Name = "lDateR";
            this.lDateR.Size = new System.Drawing.Size(794, 32);
            this.lDateR.TabIndex = 70;
            this.lDateR.Text = "DateR";
            this.lDateR.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormRapport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(794, 869);
            this.Controls.Add(this.groupBox40);
            this.Controls.Add(this.lDateR);
            this.Controls.Add(this.tBoxDestine);
            this.Controls.Add(this.tboxConcerne);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.lNumRapport);
            this.Controls.Add(this.lblNumVisite);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.Window;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "FormRapport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rapport de la consultation";
            this.Load += new System.EventHandler(this.FormRapport_Load);
            this.groupBox40.ResumeLayout(false);
            this.groupBox40.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblNumVisite;
        private System.Windows.Forms.Label lNumRapport;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox tboxConcerne;
        private System.Windows.Forms.TextBox tBoxDestine;
        private System.Windows.Forms.TextBox tboxContenu;
        private System.Windows.Forms.GroupBox groupBox40;
        private System.Windows.Forms.Label lDateR;
    }
}