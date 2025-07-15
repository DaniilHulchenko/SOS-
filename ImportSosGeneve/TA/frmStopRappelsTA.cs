using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SosMedecins.SmartRapport.EtatsCrystal;

namespace ImportSosGeneve.TA
{
    public  class frmStopRappelsTA : System.Windows.Forms.Form
    {
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private System.ComponentModel.IContainer components = null;
        public frmStopRappelsTA(SosMedecins.SmartRapport.DAL.dstStopRappels DSStopRappelsTA)
        {
            InitializeComponent();
            EtatStopRapTA EtatStopRappels = new SosMedecins.SmartRapport.EtatsCrystal.EtatStopRapTA();
             EtatStopRappels.SetDataSource(DSStopRappelsTA);
             crystalReportViewer1.ReportSource = EtatStopRappels;
             crystalReportViewer1.Zoom(2);
           
        }
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        // private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = -1;
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewer1.Location = new System.Drawing.Point(0, 0);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.Size = new System.Drawing.Size(778, 706);
            this.crystalReportViewer1.TabIndex = 0;
            // 
            // frmStopRappelsTA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 706);
            this.Controls.Add(this.crystalReportViewer1);
            this.Name = "frmStopRappelsTA";
            this.Text = "frmStopRappelsTA";
            this.ResumeLayout(false);

        }

        #endregion

       // private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
    }
}