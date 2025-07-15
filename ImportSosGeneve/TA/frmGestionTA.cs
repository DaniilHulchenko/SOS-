using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SosMedecins.SmartRapport.EtatsCrystal;
using CrystalDecisions.Shared;  //pour l'export des rapports
using CrystalDecisions.CrystalReports.Engine;  //pour l'export des rapports
using System.IO;                //Pour la gestion des flux

namespace ImportSosGeneve
{
    public  class frmGestionTA :  System.Windows.Forms.Form
    {
        
        private System.ComponentModel.IContainer components = null;
        public frmGestionTA(SosMedecins.SmartRapport.DAL.GestionTA dstGestionTA)
        {
            InitializeComponent();
            EtatGestionTA etatGestion = new EtatGestionTA();
            etatGestion.SetDataSource(dstGestionTA);
            crystalReportViewerGestion.ReportSource = etatGestion;

            //Pour le nom du fichier
            String HeureFichier = DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Second.ToString();

            //Etat Excel
            string FichierHoraire = exportReport(etatGestion, "Etat_TA_" + HeureFichier, "xls");

            crystalReportViewerGestion.Zoom(2);            
        }


        //********************Fonction pour exporter un etat crystal en divers format de fichier dans le temp de l'utilisateur *************************
        protected string exportReport(CrystalDecisions.CrystalReports.Engine.ReportClass RapportChoisi, string NomDuFichier, string Extension)
        {
            //string tempDir = System.IO.Path.GetTempPath();  //Répertoire de l'utilisateur
            //string tempDir = System.IO.Path.GetPathRoot(MyDocuments);  //Répertoire de l'utilisateur
            string Rep = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);  //Répertoire Mes Docs de l'utilisateur
            string RepFileName = Rep + "\\"+ NomDuFichier + ".";

            ExportFormatType FormatExport = ExportFormatType.PortableDocFormat;  //On defini un valeur par défaut pour la variable FormatExport

            switch (Extension)
            {
                case "pdf": FormatExport = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat; RepFileName += "pdf"; break;
                case "doc": FormatExport = CrystalDecisions.Shared.ExportFormatType.WordForWindows; RepFileName += "doc"; break;
                case "xls": FormatExport = CrystalDecisions.Shared.ExportFormatType.Excel; RepFileName += "xls"; break;
                case "htm": FormatExport = CrystalDecisions.Shared.ExportFormatType.HTML40; RepFileName += "htm"; break;
                case "xml": FormatExport = CrystalDecisions.Shared.ExportFormatType.Xml; RepFileName += "xml"; break;
            }

            //MemoryStream FluxMem =  (MemoryStream)RapportChoisi.ExportToStream(FormatExport);
            Stream FluxMem = RapportChoisi.ExportToStream(FormatExport);           

            if (FluxMem.Length == 0) return ("Erreur");

            try
            {               
                //Créer un FileStream object pour écrir un flux dans un fichier           
                using (FileStream fileStream = System.IO.File.Create(RepFileName, (int)FluxMem.Length))
                {
                    //Charge le tableau de bytes[] avec le flux de donnée
                    byte[] bytesInStream = new byte[FluxMem.Length];
                    FluxMem.Read(bytesInStream, 0, (int)bytesInStream.Length);

                    //Utilise le FileStream object pour écrire dans le fichier spécifié
                    fileStream.Write(bytesInStream, 0, bytesInStream.Length);
                }
            }
            catch (System.IO.IOException e)
            {
                Console.WriteLine("Erreur à la création du fichier :" + e.Message);
                MessageBox.Show("Erreur à la création du fichier :" + e.Message);
                return ("Erreur");
            }

            return (NomDuFichier + "." + Extension);         //Tout est ok retourne 0
        }


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
            this.crystalReportViewerGestion = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // crystalReportViewerGestion
            // 
            this.crystalReportViewerGestion.ActiveViewIndex = -1;
            this.crystalReportViewerGestion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewerGestion.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewerGestion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewerGestion.Location = new System.Drawing.Point(0, 0);
            this.crystalReportViewerGestion.Name = "crystalReportViewerGestion";
            this.crystalReportViewerGestion.Size = new System.Drawing.Size(696, 548);
            this.crystalReportViewerGestion.TabIndex = 0;
            // 
            // frmGestionTA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.ClientSize = new System.Drawing.Size(696, 548);
            this.Controls.Add(this.crystalReportViewerGestion);
            this.Name = "frmGestionTA";
            this.Text = "frmGestionTA";
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewerGestion;
        
    }
}