namespace ImportSosGeneve
{
    partial class FInfoAssure
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FInfoAssure));
            this.rTBoxInfos = new System.Windows.Forms.RichTextBox();
            this.bExport = new System.Windows.Forms.Button();
            this.bFermer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rTBoxInfos
            // 
            this.rTBoxInfos.Dock = System.Windows.Forms.DockStyle.Top;
            this.rTBoxInfos.Location = new System.Drawing.Point(0, 0);
            this.rTBoxInfos.Name = "rTBoxInfos";
            this.rTBoxInfos.Size = new System.Drawing.Size(418, 281);
            this.rTBoxInfos.TabIndex = 35;
            this.rTBoxInfos.Text = "";
            // 
            // bExport
            // 
            this.bExport.BackgroundImage = global::ImportSosGeneve.Properties.Resources.bExportBleu;
            this.bExport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bExport.Enabled = false;
            this.bExport.FlatAppearance.BorderSize = 0;
            this.bExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bExport.Location = new System.Drawing.Point(23, 287);
            this.bExport.Name = "bExport";
            this.bExport.Size = new System.Drawing.Size(75, 75);
            this.bExport.TabIndex = 36;
            this.bExport.UseVisualStyleBackColor = true;
            this.bExport.Click += new System.EventHandler(this.bExport_Click);
            // 
            // bFermer
            // 
            this.bFermer.BackgroundImage = global::ImportSosGeneve.Properties.Resources.exit;
            this.bFermer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bFermer.FlatAppearance.BorderSize = 0;
            this.bFermer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bFermer.Location = new System.Drawing.Point(332, 287);
            this.bFermer.Name = "bFermer";
            this.bFermer.Size = new System.Drawing.Size(75, 75);
            this.bFermer.TabIndex = 34;
            this.bFermer.UseVisualStyleBackColor = true;
            this.bFermer.Click += new System.EventHandler(this.bFermer_Click);
            // 
            // FInfoAssure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.ClientSize = new System.Drawing.Size(418, 364);
            this.ControlBox = false;
            this.Controls.Add(this.bExport);
            this.Controls.Add(this.rTBoxInfos);
            this.Controls.Add(this.bFermer);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FInfoAssure";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Informations sur l\'assuré";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bFermer;
        private System.Windows.Forms.RichTextBox rTBoxInfos;
        private System.Windows.Forms.Button bExport;
    }
}