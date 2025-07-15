namespace ImportSosGeneve.Commun
{
    partial class viewerStats144
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
            this.crystalReportViewerStats1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // crystalReportViewerStats1
            // 
            this.crystalReportViewerStats1.ActiveViewIndex = -1;
            this.crystalReportViewerStats1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewerStats1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewerStats1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewerStats1.Location = new System.Drawing.Point(0, 0);
            this.crystalReportViewerStats1.Name = "crystalReportViewerStats1";
            this.crystalReportViewerStats1.Size = new System.Drawing.Size(1377, 826);
            this.crystalReportViewerStats1.TabIndex = 0;
            this.crystalReportViewerStats1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // viewerStats144
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1377, 826);
            this.Controls.Add(this.crystalReportViewerStats1);
            this.Name = "viewerStats144";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "viewerStats144";
            this.Load += new System.EventHandler(this.viewerStats144_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewerStats1;
    }
}