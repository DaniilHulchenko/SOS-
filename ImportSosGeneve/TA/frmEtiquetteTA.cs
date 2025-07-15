using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SosMedecins.SmartRapport.EtatsCrystal;

namespace ImportSosGeneve
{
    public class frmEtiquetteTA : System.Windows.Forms.Form
    {
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer2;
        private SosMedecins.SmartRapport.EtatsCrystal.EtiquetteTA rpsource = null;
        private System.ComponentModel.IContainer components = null;
        public frmEtiquetteTA(SosMedecins.SmartRapport.DAL.dsEtiquettes dstetiq)
        {
            InitializeComponent();
            EtiquetteTA etiquetteTA = new SosMedecins.SmartRapport.EtatsCrystal.EtiquetteTA();
            etiquetteTA.SetDataSource(dstetiq);
            crystalReportViewer2.ReportSource = etiquetteTA;
            crystalReportViewer2.Zoom(2);
            
        }
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
            this.crystalReportViewer2 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // crystalReportViewer2
            // 
            this.crystalReportViewer2.ActiveViewIndex = -1;
            this.crystalReportViewer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer2.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewer2.Location = new System.Drawing.Point(0, 0);
            this.crystalReportViewer2.Name = "crystalReportViewer2";
            this.crystalReportViewer2.Size = new System.Drawing.Size(865, 600);
            this.crystalReportViewer2.TabIndex = 0;
            // 
            // frmEtiquetteTA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.ClientSize = new System.Drawing.Size(865, 600);
            this.Controls.Add(this.crystalReportViewer2);
            this.Name = "frmEtiquetteTA";
            this.Text = "frmEtiquetteTA";
            this.ResumeLayout(false);

        }

        #endregion

       // private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer2;
    }
}