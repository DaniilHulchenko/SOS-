
namespace ImportSosGeneve.Statistics
{
    partial class FMaterielsStats
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
            this.dtDebutDate = new System.Windows.Forms.DateTimePicker();
            this.dtFinalDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbMateriel = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.bGetStatistics = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dtDebutDate
            // 
            this.dtDebutDate.Location = new System.Drawing.Point(71, 77);
            this.dtDebutDate.Name = "dtDebutDate";
            this.dtDebutDate.Size = new System.Drawing.Size(145, 20);
            this.dtDebutDate.TabIndex = 0;
            this.dtDebutDate.Value = new System.DateTime(2024, 4, 1, 0, 0, 0, 0);
            // 
            // dtFinalDate
            // 
            this.dtFinalDate.Location = new System.Drawing.Point(242, 77);
            this.dtFinalDate.Name = "dtFinalDate";
            this.dtFinalDate.Size = new System.Drawing.Size(145, 20);
            this.dtFinalDate.TabIndex = 1;
            this.dtFinalDate.Value = new System.DateTime(2024, 5, 1, 0, 0, 0, 0);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(282, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Date de fin";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(112, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Date de début";
            // 
            // cbMateriel
            // 
            this.cbMateriel.FormattingEnabled = true;
            this.cbMateriel.Location = new System.Drawing.Point(71, 126);
            this.cbMateriel.Name = "cbMateriel";
            this.cbMateriel.Size = new System.Drawing.Size(145, 21);
            this.cbMateriel.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(112, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Materiel";
            // 
            // bGetStatistics
            // 
            this.bGetStatistics.Location = new System.Drawing.Point(71, 176);
            this.bGetStatistics.Name = "bGetStatistics";
            this.bGetStatistics.Size = new System.Drawing.Size(145, 40);
            this.bGetStatistics.TabIndex = 4;
            this.bGetStatistics.Text = "Obtenir des statistiques";
            this.bGetStatistics.UseVisualStyleBackColor = true;
            this.bGetStatistics.Click += new System.EventHandler(this.bGetStatistics_Click);
            // 
            // FMaterielsStats
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.bGetStatistics);
            this.Controls.Add(this.cbMateriel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtFinalDate);
            this.Controls.Add(this.dtDebutDate);
            this.Name = "FMaterielsStats";
            this.Text = "FMaterielsStats";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtDebutDate;
        private System.Windows.Forms.DateTimePicker dtFinalDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbMateriel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button bGetStatistics;
    }
}