namespace ImportSosGeneve.Rapport
{
    partial class FCarteAVS
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
            this.bFermer = new System.Windows.Forms.Button();
            this.bRotationImage = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.zoomImageViewer1 = new ImportSosGeneve.ZoomImageViewer();
            this.SuspendLayout();
            // 
            // bFermer
            // 
            this.bFermer.BackColor = System.Drawing.Color.Transparent;
            this.bFermer.FlatAppearance.BorderSize = 0;
            this.bFermer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bFermer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bFermer.Image = global::ImportSosGeneve.Properties.Resources.icone_exit;
            this.bFermer.Location = new System.Drawing.Point(228, 222);
            this.bFermer.Name = "bFermer";
            this.bFermer.Size = new System.Drawing.Size(64, 64);
            this.bFermer.TabIndex = 76;
            this.bFermer.Tag = "";
            this.toolTip1.SetToolTip(this.bFermer, "Fermer");
            this.bFermer.UseVisualStyleBackColor = false;
            this.bFermer.Click += new System.EventHandler(this.bFermer_Click);
            // 
            // bRotationImage
            // 
            this.bRotationImage.FlatAppearance.BorderSize = 0;
            this.bRotationImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bRotationImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bRotationImage.Image = global::ImportSosGeneve.Properties.Resources.icone_Rotation;
            this.bRotationImage.Location = new System.Drawing.Point(62, 225);
            this.bRotationImage.Name = "bRotationImage";
            this.bRotationImage.Size = new System.Drawing.Size(62, 59);
            this.bRotationImage.TabIndex = 75;
            this.bRotationImage.Tag = "";
            this.bRotationImage.Text = "180°";
            this.toolTip1.SetToolTip(this.bRotationImage, "Rotation de l\'image");
            this.bRotationImage.UseVisualStyleBackColor = true;
            this.bRotationImage.Click += new System.EventHandler(this.bRotationImage_Click);
            // 
            // zoomImageViewer1
            // 
            this.zoomImageViewer1.AutoScroll = true;
            this.zoomImageViewer1.AutoScrollMargin = new System.Drawing.Size(373, 215);
            this.zoomImageViewer1.Image = null;
            this.zoomImageViewer1.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            this.zoomImageViewer1.Location = new System.Drawing.Point(1, 1);
            this.zoomImageViewer1.Name = "zoomImageViewer1";
            this.zoomImageViewer1.Size = new System.Drawing.Size(373, 215);
            this.zoomImageViewer1.TabIndex = 74;
            this.zoomImageViewer1.Text = "zoomImageViewer1";
            this.zoomImageViewer1.Zoom = 1F;
            // 
            // FCarteAVS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.ClientSize = new System.Drawing.Size(375, 292);
            this.Controls.Add(this.bFermer);
            this.Controls.Add(this.bRotationImage);
            this.Controls.Add(this.zoomImageViewer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FCarteAVS";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Carte AVS";
            this.Load += new System.EventHandler(this.FCarteAVS_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ZoomImageViewer zoomImageViewer1;
        private System.Windows.Forms.Button bRotationImage;
        private System.Windows.Forms.Button bFermer;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}