namespace ImportSosGeneve.Facture
{
    partial class FImpFacture
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cBoxFactInd = new System.Windows.Forms.CheckBox();
            this.bFacture = new System.Windows.Forms.Button();
            this.tBoxCommentaire = new System.Windows.Forms.TextBox();
            this.bImprime = new System.Windows.Forms.Button();
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
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
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.splitContainer1.Panel1.Controls.Add(this.cBoxFactInd);
            this.splitContainer1.Panel1.Controls.Add(this.bFacture);
            this.splitContainer1.Panel1.Controls.Add(this.tBoxCommentaire);
            this.splitContainer1.Panel1.Controls.Add(this.bImprime);
            this.splitContainer1.Panel1.ForeColor = System.Drawing.SystemColors.Window;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.crystalReportViewer1);
            this.splitContainer1.Size = new System.Drawing.Size(1131, 593);
            this.splitContainer1.SplitterDistance = 182;
            this.splitContainer1.TabIndex = 0;
            // 
            // cBoxFactInd
            // 
            this.cBoxFactInd.AutoSize = true;
            this.cBoxFactInd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cBoxFactInd.Location = new System.Drawing.Point(12, 156);
            this.cBoxFactInd.Name = "cBoxFactInd";
            this.cBoxFactInd.Size = new System.Drawing.Size(149, 20);
            this.cBoxFactInd.TabIndex = 5;
            this.cBoxFactInd.Text = "Facture indépendant";
            this.cBoxFactInd.UseVisualStyleBackColor = true;
            // 
            // bFacture
            // 
            this.bFacture.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bFacture.ForeColor = System.Drawing.SystemColors.InfoText;
            this.bFacture.Location = new System.Drawing.Point(38, 215);
            this.bFacture.Name = "bFacture";
            this.bFacture.Size = new System.Drawing.Size(75, 43);
            this.bFacture.TabIndex = 4;
            this.bFacture.Text = "Générer la facture";
            this.bFacture.UseVisualStyleBackColor = true;
            this.bFacture.Click += new System.EventHandler(this.bFacture_Click);
            // 
            // tBoxCommentaire
            // 
            this.tBoxCommentaire.Location = new System.Drawing.Point(12, 12);
            this.tBoxCommentaire.Multiline = true;
            this.tBoxCommentaire.Name = "tBoxCommentaire";
            this.tBoxCommentaire.Size = new System.Drawing.Size(149, 92);
            this.tBoxCommentaire.TabIndex = 1;
            // 
            // bImprime
            // 
            this.bImprime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bImprime.ForeColor = System.Drawing.SystemColors.WindowText;
            this.bImprime.Location = new System.Drawing.Point(38, 299);
            this.bImprime.Name = "bImprime";
            this.bImprime.Size = new System.Drawing.Size(75, 56);
            this.bImprime.TabIndex = 0;
            this.bImprime.Text = "Marquer comme Envoyée";
            this.bImprime.UseVisualStyleBackColor = true;
            this.bImprime.Click += new System.EventHandler(this.bImprime_Click);
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = -1;
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewer1.Location = new System.Drawing.Point(0, 0);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.Size = new System.Drawing.Size(945, 593);
            this.crystalReportViewer1.TabIndex = 0;
            this.crystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // FImpFacture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1131, 593);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FImpFacture";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Impression des factures";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FImpFacture_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button bImprime;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private System.Windows.Forms.TextBox tBoxCommentaire;
        private System.Windows.Forms.Button bFacture;
        private System.Windows.Forms.CheckBox cBoxFactInd;
    }
}