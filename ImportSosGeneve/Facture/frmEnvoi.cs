using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using CrystalDecisions.Shared;
using System.Net.Http;
using System.Xml;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;

namespace ImportSosGeneve
{
	/// <summary>
	/// Description résumée de frmEnvoi.
	/// </summary>
	public class frmEnvoi : Form
	{
		private Label label1;
		private Label label2;
		private GroupBox groupFac;
		private TextBox txtFinEnvoi;
        private TextBox txtDebutEnvoi;
        private GroupBox groupEtat;
		private Label LbNbFacture;
		private ProgressBar Bar;
		private Label LbStatus;
		private ToolTip toolTip1;
		private IContainer components;
		private CheckBox chk_xml;
		private CheckBox chk_imprimante;
		private Label labelNfac;
		private GroupBox groupBox1;
        private TreeView twInfosMed;
		private ListBox cbNFacAss;
        private Button bApercu;
        private ImageList imageList1;
        private Button bQuitter;
        private Button bStop;
        private Button bEnvoi;
        private CheckBox cBoxEmail;


        private bool AnnulationEnCours = false;
        private int Destinataire = -1;         //CDM 0 ou Médidata 1
        
        //Pour envoi à Medidata
        private string idFacture = "";         
        private string to_organization = "";
        private string transmissionReference = "";

        public frmEnvoi()
		{
			InitializeComponent();
		}
		public frmEnvoi(long NDebut,long NFin)
		{
			InitializeComponent();

			if(NDebut>-1)
				this.txtDebutEnvoi.Text = NDebut.ToString();
			if(NFin>-1)
				this.txtFinEnvoi.Text = NFin.ToString();
		}
		
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Code généré par le Concepteur Windows Form
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEnvoi));
            this.groupFac = new System.Windows.Forms.GroupBox();
            this.cBoxEmail = new System.Windows.Forms.CheckBox();
            this.bApercu = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.chk_imprimante = new System.Windows.Forms.CheckBox();
            this.chk_xml = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFinEnvoi = new System.Windows.Forms.TextBox();
            this.txtDebutEnvoi = new System.Windows.Forms.TextBox();
            this.labelNfac = new System.Windows.Forms.Label();
            this.groupEtat = new System.Windows.Forms.GroupBox();
            this.LbStatus = new System.Windows.Forms.Label();
            this.Bar = new System.Windows.Forms.ProgressBar();
            this.LbNbFacture = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.twInfosMed = new System.Windows.Forms.TreeView();
            this.cbNFacAss = new System.Windows.Forms.ListBox();
            this.bQuitter = new System.Windows.Forms.Button();
            this.bStop = new System.Windows.Forms.Button();
            this.bEnvoi = new System.Windows.Forms.Button();
            this.groupFac.SuspendLayout();
            this.groupEtat.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupFac
            // 
            this.groupFac.BackColor = System.Drawing.Color.Transparent;
            this.groupFac.Controls.Add(this.cBoxEmail);
            this.groupFac.Controls.Add(this.bApercu);
            this.groupFac.Controls.Add(this.chk_imprimante);
            this.groupFac.Controls.Add(this.chk_xml);
            this.groupFac.Controls.Add(this.label2);
            this.groupFac.Controls.Add(this.label1);
            this.groupFac.Controls.Add(this.txtFinEnvoi);
            this.groupFac.Controls.Add(this.txtDebutEnvoi);
            this.groupFac.Controls.Add(this.labelNfac);
            this.groupFac.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupFac.Location = new System.Drawing.Point(8, 8);
            this.groupFac.Name = "groupFac";
            this.groupFac.Size = new System.Drawing.Size(593, 172);
            this.groupFac.TabIndex = 0;
            this.groupFac.TabStop = false;
            this.groupFac.Text = "Factures";
            // 
            // cBoxEmail
            // 
            this.cBoxEmail.Location = new System.Drawing.Point(233, 97);
            this.cBoxEmail.Name = "cBoxEmail";
            this.cBoxEmail.Size = new System.Drawing.Size(99, 24);
            this.cBoxEmail.TabIndex = 8;
            this.cBoxEmail.Text = "Email";
            this.cBoxEmail.CheckedChanged += new System.EventHandler(this.cBoxEmail_CheckedChanged);
            // 
            // bApercu
            // 
            this.bApercu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bApercu.FlatAppearance.BorderSize = 0;
            this.bApercu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bApercu.ImageIndex = 0;
            this.bApercu.ImageList = this.imageList1;
            this.bApercu.Location = new System.Drawing.Point(498, 86);
            this.bApercu.Name = "bApercu";
            this.bApercu.Size = new System.Drawing.Size(75, 75);
            this.bApercu.TabIndex = 7;
            this.bApercu.UseVisualStyleBackColor = true;
            this.bApercu.Click += new System.EventHandler(this.bApercu_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "bRecherche.png");
            this.imageList1.Images.SetKeyName(1, "bEnvoi.png");
            this.imageList1.Images.SetKeyName(2, "bondCancel.png");
            this.imageList1.Images.SetKeyName(3, "bStop.png");
            // 
            // chk_imprimante
            // 
            this.chk_imprimante.Location = new System.Drawing.Point(233, 68);
            this.chk_imprimante.Name = "chk_imprimante";
            this.chk_imprimante.Size = new System.Drawing.Size(99, 24);
            this.chk_imprimante.TabIndex = 4;
            this.chk_imprimante.Text = "Imprimante";
            // 
            // chk_xml
            // 
            this.chk_xml.Checked = true;
            this.chk_xml.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_xml.Location = new System.Drawing.Point(233, 34);
            this.chk_xml.Name = "chk_xml";
            this.chk_xml.Size = new System.Drawing.Size(194, 24);
            this.chk_xml.TabIndex = 3;
            this.chk_xml.Text = "Envoi électronique (XML)";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(11, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Au n°:";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(11, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Du n°:";
            // 
            // txtFinEnvoi
            // 
            this.txtFinEnvoi.Location = new System.Drawing.Point(59, 79);
            this.txtFinEnvoi.Name = "txtFinEnvoi";
            this.txtFinEnvoi.Size = new System.Drawing.Size(118, 22);
            this.txtFinEnvoi.TabIndex = 2;
            // 
            // txtDebutEnvoi
            // 
            this.txtDebutEnvoi.Location = new System.Drawing.Point(59, 42);
            this.txtDebutEnvoi.Name = "txtDebutEnvoi";
            this.txtDebutEnvoi.Size = new System.Drawing.Size(118, 22);
            this.txtDebutEnvoi.TabIndex = 1;
            // 
            // labelNfac
            // 
            this.labelNfac.Font = new System.Drawing.Font("Arial Black", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNfac.Location = new System.Drawing.Point(43, 18);
            this.labelNfac.Name = "labelNfac";
            this.labelNfac.Size = new System.Drawing.Size(136, 16);
            this.labelNfac.TabIndex = 3;
            // 
            // groupEtat
            // 
            this.groupEtat.BackColor = System.Drawing.Color.Transparent;
            this.groupEtat.Controls.Add(this.LbStatus);
            this.groupEtat.Controls.Add(this.Bar);
            this.groupEtat.Controls.Add(this.LbNbFacture);
            this.groupEtat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupEtat.Location = new System.Drawing.Point(8, 417);
            this.groupEtat.Name = "groupEtat";
            this.groupEtat.Size = new System.Drawing.Size(593, 88);
            this.groupEtat.TabIndex = 2;
            this.groupEtat.TabStop = false;
            this.groupEtat.Text = "Etat";
            // 
            // LbStatus
            // 
            this.LbStatus.Font = new System.Drawing.Font("Comic Sans MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LbStatus.ForeColor = System.Drawing.Color.Green;
            this.LbStatus.Location = new System.Drawing.Point(10, 56);
            this.LbStatus.Name = "LbStatus";
            this.LbStatus.Size = new System.Drawing.Size(567, 24);
            this.LbStatus.TabIndex = 1;
            this.LbStatus.Text = "Génération réussie";
            this.LbStatus.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.LbStatus.Visible = false;
            // 
            // Bar
            // 
            this.Bar.Location = new System.Drawing.Point(8, 24);
            this.Bar.Name = "Bar";
            this.Bar.Size = new System.Drawing.Size(569, 24);
            this.Bar.TabIndex = 0;
            // 
            // LbNbFacture
            // 
            this.LbNbFacture.Font = new System.Drawing.Font("Comic Sans MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LbNbFacture.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.LbNbFacture.Location = new System.Drawing.Point(24, 56);
            this.LbNbFacture.Name = "LbNbFacture";
            this.LbNbFacture.Size = new System.Drawing.Size(96, 24);
            this.LbNbFacture.TabIndex = 2;
            this.LbNbFacture.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.twInfosMed);
            this.groupBox1.Location = new System.Drawing.Point(8, 186);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(593, 224);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // twInfosMed
            // 
            this.twInfosMed.Location = new System.Drawing.Point(8, 16);
            this.twInfosMed.Name = "twInfosMed";
            this.twInfosMed.Size = new System.Drawing.Size(569, 200);
            this.twInfosMed.TabIndex = 0;
            // 
            // cbNFacAss
            // 
            this.cbNFacAss.Location = new System.Drawing.Point(19, 531);
            this.cbNFacAss.Name = "cbNFacAss";
            this.cbNFacAss.Size = new System.Drawing.Size(194, 43);
            this.cbNFacAss.TabIndex = 3;
            this.cbNFacAss.Visible = false;
            // 
            // bQuitter
            // 
            this.bQuitter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bQuitter.FlatAppearance.BorderSize = 0;
            this.bQuitter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bQuitter.ImageIndex = 2;
            this.bQuitter.ImageList = this.imageList1;
            this.bQuitter.Location = new System.Drawing.Point(501, 520);
            this.bQuitter.Name = "bQuitter";
            this.bQuitter.Size = new System.Drawing.Size(75, 75);
            this.bQuitter.TabIndex = 10;
            this.bQuitter.UseVisualStyleBackColor = true;
            this.bQuitter.Click += new System.EventHandler(this.bQuitter_Click);
            // 
            // bStop
            // 
            this.bStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bStop.FlatAppearance.BorderSize = 0;
            this.bStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bStop.ImageIndex = 3;
            this.bStop.ImageList = this.imageList1;
            this.bStop.Location = new System.Drawing.Point(360, 520);
            this.bStop.Name = "bStop";
            this.bStop.Size = new System.Drawing.Size(75, 75);
            this.bStop.TabIndex = 9;
            this.bStop.UseVisualStyleBackColor = true;
            this.bStop.Click += new System.EventHandler(this.bStop_Click);
            // 
            // bEnvoi
            // 
            this.bEnvoi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bEnvoi.FlatAppearance.BorderSize = 0;
            this.bEnvoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bEnvoi.ImageIndex = 1;
            this.bEnvoi.ImageList = this.imageList1;
            this.bEnvoi.Location = new System.Drawing.Point(229, 520);
            this.bEnvoi.Name = "bEnvoi";
            this.bEnvoi.Size = new System.Drawing.Size(75, 75);
            this.bEnvoi.TabIndex = 8;
            this.bEnvoi.UseVisualStyleBackColor = true;
            this.bEnvoi.Click += new System.EventHandler(this.bEnvoi_Click);
            // 
            // frmEnvoi
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.ClientSize = new System.Drawing.Size(619, 613);
            this.Controls.Add(this.bEnvoi);
            this.Controls.Add(this.bStop);
            this.Controls.Add(this.bQuitter);
            this.Controls.Add(this.cbNFacAss);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupEtat);
            this.Controls.Add(this.groupFac);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmEnvoi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Impression Factures";
            this.groupFac.ResumeLayout(false);
            this.groupFac.PerformLayout();
            this.groupEtat.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion
    

        private void Envois()
        {
            string CheminDest = SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.CheminXmlFacture;
            string CheminSauvDest = SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.CheminSauvXmlFacture;

            long nFacDebut = long.Parse(txtDebutEnvoi.Text);
            long nFacFin = long.Parse(txtFinEnvoi.Text);
            //string TypeDestinataire = "";     //TG ou TP
            string TypeEnvoi = "";   //TG ou TP ou Tier           

            int TypeSortie = 0;
            if (chk_imprimante.Checked && chk_xml.Checked)
                TypeSortie = -1;
            else if (chk_imprimante.Checked)
                TypeSortie = 1;
            else if (chk_xml.Checked)
                TypeSortie = 2;
            else if (cBoxEmail.Checked)         //Domi 17.10.2017
                TypeSortie = 3;            

            Destinataire = 1;    //Medidata
           
            long[] NFactures = null;
                     
            if (Destinataire == 1)  //Medidata
            {
                NFactures = OutilsExt.OutilsSql.RecupNFactByRangeAvRappel(nFacDebut, nFacFin);   //Gestion des rappels si Médidata
                CheminDest += "Medidata";

//#if !DEBUG

                //On commence par copier les fichiers du dossier de création dans un dossier de sauvegarde               
                if (Directory.Exists(CheminDest))
                {
                    //On copie le contenu du dossier sur le serveur
                    foreach (string file in Directory.GetFiles(CheminDest))
                    {
                        string fileNameDest = Path.Combine(CheminSauvDest, Path.GetFileName(file));
                       
                        File.Copy(file, fileNameDest, true);
                    }

                    //Puis on efface le dossier 
                    Directory.Delete(CheminDest, true);
                }
//#endif
            }
  
            Console.WriteLine(NFactures.ToString());
            Bar.Minimum = 0;
            Bar.Maximum = NFactures.Length;
            Bar.Value = 0;

            //rapport
            labelNfac.Text = " Total : " + Bar.Maximum + "  Factures";

            twInfosMed.Nodes.Clear();
            System.Data.DataRow[] rowsInfosMed = OutilsExt.OutilsSql.InfosMedecinsByTableauFactures(NFactures);
            if (rowsInfosMed != null)
            {
                TreeNode tn1 = new TreeNode("");
                twInfosMed.Nodes.Add(tn1);
                int NbFacMedecinsSos = 0;
                
                if (!Directory.Exists(CheminDest)) 
                    Directory.CreateDirectory(CheminDest);

                for (int i = 0; i < rowsInfosMed.Length; i++)
                {
                    if (rowsInfosMed[i]["Numeros"].ToString() != "")
                    {
                        string[] lngFac = rowsInfosMed[i]["Numeros"].ToString().Split(';');
                    }
                    else
                        continue;

                  //  if (rowsInfosMed[i]["Independant"].ToString() == "0")
                   // {
                        NbFacMedecinsSos += int.Parse(rowsInfosMed[i]["NbFacture"].ToString());
                        TreeNode tn = new TreeNode(rowsInfosMed[i]["NomGeneve"].ToString() + " - " + rowsInfosMed[i]["NbFacture"].ToString() + " Factures");
                        tn1.Nodes.Add(tn);
                 /*   }                                        
                    else
                    {
                        TreeNode tn = new TreeNode(rowsInfosMed[i]["NomGeneve"].ToString() + " " + rowsInfosMed[i]["PrenomGeneve"].ToString() + " : " + rowsInfosMed[i]["NbFacture"].ToString() + " Factures");
                        twInfosMed.Nodes.Add(tn);
                    }*/
                }

                tn1.Text = "Médecins Sos : " + NbFacMedecinsSos + " Factures";

                int occ = 0;

                //Récup de la date et heure
                string DateHeure = DateTime.Now.ToString("ddMMyyHHmm");

                //pour chaque médecin
                for (int i = 0; i < rowsInfosMed.Length; i++)
                {
                    if (rowsInfosMed[i]["Numeros"].ToString() != "")
                    {
                        if (rowsInfosMed[i]["Numeros"].ToString().Length > 0) rowsInfosMed[i]["Numeros"] = rowsInfosMed[i]["Numeros"].ToString().Remove(rowsInfosMed[i]["Numeros"].ToString().Length - 1, 1);

                        string[] lngFac = rowsInfosMed[i]["Numeros"].ToString().Split(';');
                        // pour chaque numéro de facture du médecin
                        foreach (string nFac in lngFac)
                        {

                            if (nFac != "")                              
                            {
                                // En cas d'annulation on arrete la procédure
                                if (AnnulationEnCours)
                                {
                                    LbStatus.Text = "Envois interrompus";
                                    LbStatus.ForeColor = Color.Red;
                                    LbStatus.Visible = true;
                                    return;
                                }

                                // mise à jour de la barre de statut des envois
                                occ++;
                                Bar.Value++;
                                LbNbFacture.Text = occ + "/" + NFactures.Length;
                                Application.DoEvents();

                                // on récupère la facture
                                System.Data.DataRow[] Factures = OutilsExt.OutilsSql.RecuperationFacturesByNFacture(long.Parse(nFac));
                                if (Factures != null && Factures.Length > 0)
                                {
                                    //Pour déterminer si c'est du Tier Payant ou du Tier Garant ou Tier (Tout Cour)
                                    /* if (Factures[0]["TypeDestinataire"].ToString() != "2")
                                         TypeDestinataire = "TG";    //Tier Garant
                                     else TypeDestinataire = "TP";*/

                                    switch(Factures[0]["TypeEnvoi"].ToString())
                                    {
                                        case "1": TypeEnvoi = "TG";break;    //Tier Garant
                                        case "2": TypeEnvoi = "T"; break;    //Tier (Tuteur etc...)
                                        case "3": TypeEnvoi = "TP"; break;   //Tier Payant
                                        default: TypeEnvoi = "TP"; break;
                                    }
                                 
                                    // Insertion dans fichier xml
                                    if (int.Parse(Factures[0]["TypeSortie"].ToString()) == (int)Facturation.Sortie.Xml && chk_xml.Checked == true)
                                    {
                                        this.Tag = "";

                                        string filename = "";

                                        if (Destinataire == 0)   //Pour l'envoi à la CDM
                                        {                                                                             
                                            if (rowsInfosMed[i]["Independant"].ToString() != "0")
                                            {
                                                
                                                if (!Directory.Exists(CheminDest + "\\" + rowsInfosMed[i]["codeIntervenant"].ToString() + "_" + DateHeure + "_" + lngFac.Length))
                                                        Directory.CreateDirectory(CheminDest + "\\" + rowsInfosMed[i]["codeIntervenant"].ToString() + "_" + DateHeure + "_" +
                                                        lngFac.Length);
                                             
                                                filename = CheminDest + "\\" + rowsInfosMed[i]["CodeIntervenant"].ToString() + "_" + DateHeure + "_" +
                                                            lngFac.Length + "\\Facture" + Factures[0]["NFacture"].ToString() + "_" + DateTime.Now.ToString("ddMMyyyy") + ".xml";                                                                                                                                                
                                            }
                                            else
                                            {
                                                if (!Directory.Exists(CheminDest + "\\" + "SOS_" + NbFacMedecinsSos + "_" + DateHeure))
                                                        Directory.CreateDirectory(CheminDest + "\\" + "SOS_" + NbFacMedecinsSos + "_" + DateHeure);


                                                filename = CheminDest + "\\" + "SOS_" + NbFacMedecinsSos + "_" + DateHeure + "\\Facture" + Factures[0]["NFacture"].ToString() +
                                                            "_" + DateTime.Now.ToString("ddMMyyyy") + ".xml";                                                
                                            }
                                        }
                                        else   //Pour l'envoi à Médidata
                                        {                                                                                      
                                           filename = CheminDest + "\\Medidata_Sos_" + TypeEnvoi + "_" + Factures[0]["NFacture"].ToString() + "_" + DateTime.Now.ToString("ddMMyyyy") + ".xml";
                                        }
                                      
                                       
                                       /* if(Factures[0]["NFacture"].ToString() == "1039707")
                                        {
                                            Console.WriteLine("OK");
                                        }*/

                                        FactureXML45 m_FacXml = new FactureXML45(OutilsExt.OutilsSql, Factures[0]["NFacture"].ToString(), filename, Destinataire);

                                        //Si la préparation XML de la facture a échouée on annule tout l'envoi
                                        if (m_FacXml.Erreurs == true)
                                        {
                                            AnnulationEnCours = true;
                                        }
                                                                                
                                        // En cas d'annulation on arrete la procédure
                                        if (AnnulationEnCours)
                                        {
                                            LbStatus.Text = "Envois interrompus";
                                            LbStatus.ForeColor = Color.Red;
                                            LbStatus.Visible = true;
                                            return;
                                        }
                                    }

                                    // impression sur imprimante
                                    if (int.Parse(Factures[0]["TypeSortie"].ToString()) == (int)Facturation.Sortie.Imprimante && chk_imprimante.Checked == true)
                                    {
                                        // Impression par imprimante
                                        frmImpressionFacture imprFacture = new frmImpressionFacture(Factures[0], 0);
                                        imprFacture.ChargementEtat("imprimante");
                                        imprFacture.AutoPrint();
                                        imprFacture.Dispose();
                                        imprFacture = null;
                                    }

                                    //Envoi par Email     Domi 17.10.2017
                                    if (int.Parse(Factures[0]["TypeSortie"].ToString()) == (int)Facturation.Sortie.Email && cBoxEmail.Checked == true)
                                    {
                                        //on envoi par Email
                                        frmImpressionFacture imprFacture = new frmImpressionFacture(Factures[0], 0);
                                        imprFacture.ChargementEtat("Email");
                                        imprFacture.EnvoiEmail();
                                        imprFacture.Dispose();
                                        imprFacture = null;
                                    }
                                }
                            }
                        }
                    }
                }


                //Si c'est pour la caisse des médecins
/* if (chk_xml.Checked && rBCDM.Checked)
 {
     if (Directory.Exists(CheminDest))
     {
         ZipClass.ZipFile(CheminDest, "", 4);

         //Puis on l'expédie à la caisse des médecins
         DirectoryInfo chemin = new DirectoryInfo(CheminDest + @"\ZIP\");

         //Récup du fichier le plus récent du répertoire
         FileInfo fichierAEnvoyer = chemin.GetFiles().OrderByDescending(f => f.LastWriteTime).First();

         if (ExpedieCDM(fichierAEnvoyer.FullName.ToString()) == "OK")
         {
             LbStatus.ForeColor = Color.Green;
             LbStatus.Text = "Envoi réussi";
             LbStatus.Visible = true;
             return;
         }
         else
         {
             LbStatus.ForeColor = Color.Red;
             LbStatus.Text = "Echec de l'envoi à la caisse des médecins";
             LbStatus.Visible = true;
             return;
         }
     }
 }
 else*/

//Envoi chez medidata   
//#if !DEBUG
                if (chk_xml.Checked)  //Sinon si c'est pour Médidata
                {
                    //On envoi ici chez Médidata
                    string[] fichiers = Directory.GetFiles(CheminDest, "*.xml");
                   
                    foreach (string fichier in fichiers)
                    {
                        //Récupération du nom court du fichier
                        string FichierNomCourt = Path.GetFileName(fichier);                       
                        SendFileToMediDataServer(fichier, FichierNomCourt);     //pour tests sur XML désactiver ici
                    }
                  
                    LbStatus.ForeColor = Color.Green;
                    LbStatus.Text = "Génération réussie";
                    LbStatus.Visible = true;
                    return;
                }              
//#endif
           }
        }


		private bool VerifNFacture()
		{
			if(txtDebutEnvoi.Text=="" || txtFinEnvoi.Text=="") return false;
			if(!WorkedString.IsLong(txtDebutEnvoi.Text) || !WorkedString.IsLong(txtFinEnvoi.Text)) return false;
			return true;
		}
						

        private void bApercu_Click(object sender, EventArgs e)
        {
            string CheminDest = SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.CheminXmlFacture;

            /*if (chk_xml.Checked && rBCDM.Checked)
                Destinataire = 0;
            else if (chk_xml.Checked && rBMedidata.Checked)
                Destinataire = 1;  */

            Destinataire = 1;

            if (txtDebutEnvoi.Text == "" || txtFinEnvoi.Text == "") return;
            long nFacDebut = long.Parse(txtDebutEnvoi.Text);
            long nFacFin = long.Parse(txtFinEnvoi.Text);

            int TypeSortie = 0;
            if (chk_imprimante.Checked && chk_xml.Checked)
                TypeSortie = -1;
            else if (chk_imprimante.Checked)
                TypeSortie = 1;
            else if (chk_xml.Checked)
                TypeSortie = 2;
            else if (cBoxEmail.Checked)
                TypeSortie = 3;

            //long[] NFactures = OutilsExt.OutilsSql.RecuperationNFacturesByRange(nFacDebut, nFacFin, TypeSortie);
            long[] NFactures = null;
            if (Destinataire == 0)   //CDM
            {
                NFactures = OutilsExt.OutilsSql.RecuperationNFacturesByRange(nFacDebut, nFacFin, TypeSortie);              
            }
            else if (Destinataire == 1)  //Medidata
            {
                NFactures = OutilsExt.OutilsSql.RecupNFactByRangeAvRappel(nFacDebut, nFacFin);   //Gestion des rappels si Médidata             
            }

            Bar.Minimum = 0;
            Bar.Maximum = NFactures.Length;
            Bar.Value = 0;

            //rapport
            labelNfac.Text = " Total : " + Bar.Maximum + "  Factures";

            try
            {
                twInfosMed.Nodes.Clear();
                System.Data.DataRow[] rowsInfosMed = OutilsExt.OutilsSql.InfosMedecinsByTableauFactures(NFactures);
                if (rowsInfosMed != null)
                {
                    TreeNode tn1 = new TreeNode("");
                    twInfosMed.Nodes.Add(tn1);
                    int NbFacMedecinsSos = 0;                 

                    for (int i = 0; i < rowsInfosMed.Length; i++)
                    {
                        if (rowsInfosMed[i]["Numeros"].ToString() != "")
                        {
                            string[] lngFac = rowsInfosMed[i]["Numeros"].ToString().Split(';');
                        }
                        else
                            continue;

                      //  if (rowsInfosMed[i]["Independant"].ToString() == "0")
                       // {
                            NbFacMedecinsSos += int.Parse(rowsInfosMed[i]["NbFacture"].ToString());
                            TreeNode tn = new TreeNode(rowsInfosMed[i]["NomGeneve"].ToString() + " - " + rowsInfosMed[i]["NbFacture"].ToString() + " Factures");
                            tn1.Nodes.Add(tn);
                       /* }
                        
                       
                        else
                        {
                            TreeNode tn = new TreeNode(rowsInfosMed[i]["NomGeneve"].ToString() + " " + rowsInfosMed[i]["PrenomGeneve"].ToString() + " : " + rowsInfosMed[i]["NbFacture"].ToString() + " Factures");
                            twInfosMed.Nodes.Add(tn);
                        }*/
                    }

                    tn1.Text = "Médecins Sos : " + NbFacMedecinsSos + " Factures";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
            }			
        }

        private void bEnvoi_Click(object sender, EventArgs e)
        {
            AnnulationEnCours = false;
            LbStatus.Visible = false;

            // Verification des numéros de facture saisi
            bool correct = VerifNFacture();
            if (!correct)
            {
                MessageBox.Show("Formats de numéros de factures invalides");
                return;
            }

            Envois();
        }

        private void bStop_Click(object sender, EventArgs e)
        {
            AnnulationEnCours = true;
        }

        private void bQuitter_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cBoxEmail_CheckedChanged(object sender, EventArgs e)
        {
            //On envoi les factures par Email

        }
      

        //Envoi du fichier ZIP à la CDM
        private string ExpedieCDM(string FichierAEnvoyer)
        {
            string Retour = "KO";
            
            string URL = "https://xmlfacture.cdm.smis.ch/Upload/UploadProgram.aspx?ProgramID=UploadV";
                        
            //string fileName = FichierAEnvoyer;
            
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            MultipartFormDataContent form = new MultipartFormDataContent();
            form.Add(new StringContent("off"), "chkDoubleOK");
            form.Add(new StringContent("off"), "docs");
            form.Add(new StringContent("F"), "lang");
            form.Add(new StringContent("sosmedecins"), "user");          
            form.Add(new StringContent("bhassan"), "pwd");
            form.Add(new StringContent("Transférer"), "btnTransfert");

            StreamContent stringContent = new StreamContent(File.OpenRead(FichierAEnvoyer));
            stringContent.Headers.Add("Content-Disposition", string.Format("form-data; name=\"txtDocument\"; filename=\"{0}\"", Path.GetFileName(FichierAEnvoyer)));
            form.Add(stringContent);
           
            //Pour la réponse
            HttpResponseMessage reponse = client.PostAsync(URL, form).Result;
            if (reponse.IsSuccessStatusCode == true)
            {
                var readstring = reponse.Content.ReadAsStringAsync();
                readstring.Wait();

                string rspond = readstring.Result;

                XmlDocument formXml = new XmlDocument();

                formXml.LoadXml(rspond);

                XmlElement root = formXml.DocumentElement;
                XmlNodeList elemList = root.GetElementsByTagName("uploadResponse");
                
                //string erreur = root.InnerXml.ToString();                   
                string Status = root.Attributes[1].Value;

                if (Status == "success")
                {
                    Retour = "OK";
                }
                else  //"failure"
                {
                    //récup de l'erreur                                      
                    string MsgErreur = "";

                    string TypeErreur = root.FirstChild.Name.ToString();
                    string CodeErreur = root.FirstChild.Attributes[1].Value;

                    if (TypeErreur == "generalError")
                    {
                        switch (CodeErreur)
                        {
                            case "-1": MsgErreur = "Annomalie transfert: Erreur inconnue."; break;
                            case "0": MsgErreur = "Annomalie transfert: Autre erreur."; break;
                            case "1": MsgErreur = "Annomalie transfert: Fichier à double."; break;
                            case "2": MsgErreur = "Annomalie transfert: Problème interne Caisse des Médecins."; break;
                            case "3": MsgErreur = "Annomalie transfert: Absence de données (documents, user, pwd)."; break;
                            case "4": MsgErreur = "Annomalie transfert: Utilisateur ou mot de passe invalide"; break;
                            default: MsgErreur = root.FirstChild.Attributes[1].Value.ToString(); break;
                        }
                    }
                    else if (TypeErreur == "error")
                    {
                        switch (CodeErreur)
                        {
                            case "0": MsgErreur = "Annomalie facture: Erreur inconnue."; break;
                            case "10": MsgErreur = "Annomalie facture: Format invalide."; break;
                            case "11": MsgErreur = "Annomalie facture: Numero de concordat (RCC) inconnu."; break;
                            case "12": MsgErreur = "Annomalie facture: Erreur inattendue lors du traitement de la facture."; break;
                            case "13": MsgErreur = "Annomalie facture: Montant à 0 pour une position unclassified."; break;
                            case "14": MsgErreur = "Annomalie facture: Position de référence absente pour une position Tarmed en pourcentage"; break;
                        }
                    }

                    MessageBox.Show("Erreur à la caisse des médecins, le message est:\r\n " + MsgErreur, "Erreur de transfert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Retour = "KO";
                }
            }

            return Retour;
        }



        //***** Envoi des fichiers à Médidata       
        //Envoi vers Medidata: Chemin complet du fichier XML, Nom Court du fichier XML
       public void SendFileToMediDataServer(string xmlFile, string file_name)
        {           
            string errorFiles = "";
           // string InMediData = "";
            string json_file = "";
            string toOrganisation = "";
            string fromOrganisation = "";
            bool ToPatient = false;
            //bool ToPatient = true;   //a activer quand envoi copie facture patient
            string type = "";
            string role_title_ = "";
            string DossierDesFichiers = DateTime.Now.ToString("ddMMyy");
            Dictionary<string, string> dictXML = GetAttributesFromXMLFile(xmlFile, "Facture");
           
            if (dictXML.ContainsKey("_transport_to"))
                toOrganisation = dictXML["_transport_to"];

            if (dictXML.ContainsKey("_transport_from")) //_transport_from
                fromOrganisation = dictXML["_transport_from"];

            if (dictXML.ContainsKey("request_invoice_request_id"))
                idFacture = dictXML["request_invoice_request_id"];
            //payload_body_role_title
            if (dictXML.ContainsKey("payload_body_role_title"))
                role_title_ = dictXML["payload_body_role_title"];

            if (toOrganisation == "2000000000008")
                type = "TG";
            else type = "TP";
          
            
            if (role_title_ == "SOS MEDECINS Cite Calvin SA")
            {
                errorFiles = @"C:\Factures\Erreures";
                //InMediData = @"C:\Factures\Transferer\";
                json_file = @"C:\Factures\info.json";
                //idFacture = GetNumFactureSOS(file_name);                                
            }

            try
            {               
                string requestURL = "https://192.168.0.14:8100/md/ela/uploads";   //Prod
                //string requestURL = "https://192.168.0.15:8100/md/ela/uploads";   //Test                

                if (type == "TG")
                {
                    to_organization = "0000000000000";
                    ToPatient = true;    
                }
                else
                {
                    to_organization = toOrganisation;
                    ToPatient = true;
                }

                /*************json file********/
                Elauploadinfo ela_UplaodInf = new Elauploadinfo();
                ela_UplaodInf.toOrganization = to_organization;
                ela_UplaodInf.toPatient = ToPatient;
                ela_UplaodInf.documentReference = file_name;
                ela_UplaodInf.correlationReference = idFacture;
                ela_UplaodInf.printLanguage = "fr";
                ela_UplaodInf.postalDelivery = "B";
                ela_UplaodInf.toTrustCenter = "2000000000008";
                string json_ela_UplaodInf = JsonConvert.SerializeObject(ela_UplaodInf);
                /********************/
                WebClient wc = new WebClient();
                byte[] XML_bytes = File.ReadAllBytes(xmlFile);//XML_file
                byte[] bArray = Encoding.UTF8.GetBytes(json_ela_UplaodInf);
                
                /************Codage no BOM **************/
                byte[] sortie_XML = ConvertFromUtf8(XML_bytes);
                Dictionary<string, object> postParameters = new Dictionary<string, object>();
                // Add your parameters here                                  
                postParameters.Add("elauploadinfo", new EnvoiVersMedidata.FileParameter(bArray, Path.GetFileName(json_file), "application/json;"));
                postParameters.Add("elauploadstream", new EnvoiVersMedidata.FileParameter(sortie_XML, Path.GetFileName(xmlFile), "application/octet-stream;")); //XML_file
                string userAgent = role_title_; 
                
                //Récup des paramètres Médidata (Compte SOS)
                string XCLIENTISOS = SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.X_CLIENT_ID_SOS;
                string Authorization = SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.Authorization_SOS;

                HttpWebResponse webResponse = EnvoiVersMedidata.MultipartFormPost(requestURL, userAgent, postParameters, "X-CLIENT-ID", XCLIENTISOS, Authorization);
                // Process response  
                StreamReader responseReader = new StreamReader(webResponse.GetResponseStream());
                string returnResponseText = responseReader.ReadToEnd();
                JObject jsonRepo = JObject.Parse(returnResponseText);
                transmissionReference = jsonRepo.First.Last.ToString();
                //If transmission ok copy file from folder ATRAITER to foler "FichiersDejaTransmisToMD"                
                // bool fileExists = (File.Exists(InMediData + DossierDesFichiers + @"\" + Path.GetFileName(xmlFile)) ? true : false);
                
                /*if (!fileExists)
                {
                    Directory.CreateDirectory(InMediData + DossierDesFichiers + @"\");
                    File.Copy(xmlFile, InMediData + DossierDesFichiers + @"\" + Path.GetFileName(xmlFile));
                }*/
               /* if (transmissionReference != "")
                    //Querying the Facture status
                    QueryingFactureStatus(transmissionReference);*/
                webResponse.Close();
            }
            catch (Exception exp)
            {
                bool fileExists = (File.Exists(errorFiles + DossierDesFichiers + @"\") ? true : false);
                if (!fileExists)
                {
                    Directory.CreateDirectory(errorFiles + DossierDesFichiers + @"\");

                    //Si le fichier existe déjà, on le supprime
                    bool FichierDejaLa = File.Exists(errorFiles + DateTime.Now.ToString("ddMMyy") + @"\" + Path.GetFileName(xmlFile));
                    if (fileExists)  
                    {
                        File.Delete(errorFiles + DateTime.Now.ToString("ddMMyy") + @"\" + Path.GetFileName(xmlFile));                                                
                    }

                    File.Copy(xmlFile, errorFiles + DateTime.Now.ToString("ddMMyy") + @"\" + Path.GetFileName(xmlFile));
                }
                // File.Move(xmlFile, errorFiles + DateTime.Now.ToString("ddMMyy") + @"\" + Path.GetFileName(xmlFile));
                MessageBox.Show(exp.Message, " Facture n° : " + idFacture); //avec les même checksum exist déja sur le serveur
            }
        }


        //parse le fichier XML
        public Dictionary<string, string> GetAttributesFromXMLFile(string xml, string typefichier)
        {
            Dictionary<string, string> dictionaryMXLFile = new Dictionary<string, string>();
            string strXML = xml;
            if (typefichier == "reponse")
            {
                if (!Directory.Exists(@"C:\reponseXML\"))
                     Directory.CreateDirectory(@"C:\reponseXML\");
                                    
                string namexmlfile = string.Format("reponse-{0:yyyy-MM-dd_hh-mm-ss-tt}.xml", DateTime.Now);
                File.WriteAllText(@"C:\reponseXML\" + namexmlfile, xml);
                strXML = @"C:\reponseXML\" + namexmlfile;
            }

            XElement xelement = XElement.Load(strXML);
            string parent = "";
            foreach (XElement el in xelement.DescendantsAndSelf())
            {
                if (el.HasAttributes)
                {
                    for (int j = 0; j < el.Attributes().Count(); j++)
                    {
                        if (el.NodesBeforeSelf().Count() > 0)
                            parent = el.Parent.Name.LocalName;
                        string key = parent + "_" + el.Name.LocalName + "_" + el.Attributes().ElementAt(j).Name.LocalName;
                        string value = el.Attributes().ElementAt(j).Value;
                        //Console.WriteLine(parent + "_" + el.Name.LocalName + "_" + el.Attributes().ElementAt(j).Name.LocalName + "     " + el.Attributes().ElementAt(j).Value);
                        if (dictionaryMXLFile.ContainsKey(key))
                        {
                            parent = el.Parent.Parent.Name.LocalName;
                            key = parent + "_" + el.Name.LocalName + "_" + el.Attributes().ElementAt(j).Name.LocalName;
                            if (dictionaryMXLFile.ContainsKey(key))
                            {
                                parent = el.Parent.Parent.Parent.Name.LocalName;
                                key = parent + "_" + el.Name.LocalName + "_" + el.Attributes().ElementAt(j).Name.LocalName;
                            }
                            else
                                dictionaryMXLFile.Add(key, value);
                        }
                        else
                            dictionaryMXLFile.Add(key, value);
                    }
                }
                if (el.HasElements)
                {
                    for (int j = 0; j < el.Elements().Count(); j++)
                    {
                        string key = "";
                        if (el.NodesBeforeSelf().Count() > 0)
                            key = el.Parent.Name.LocalName + "_" + el.Name.LocalName + "_" + el.Elements().ElementAt(j).Name.LocalName;
                        else
                            key = parent + "_" + el.Name.LocalName + "_" + el.Elements().ElementAt(j).Name.LocalName;
                        string value = el.Elements().ElementAt(j).Value;
                     
                        if (dictionaryMXLFile.ContainsKey(key))
                        {
                            parent = el.Parent.Parent.Name.LocalName;
                            key = parent + "_" + el.Name.LocalName + "_" + el.Elements().ElementAt(j).Name.LocalName;
                            if (dictionaryMXLFile.ContainsKey(key))
                            {
                                parent = el.Parent.Parent.Parent.Name.LocalName;
                                key = parent + "_" + el.Name.LocalName + "_" + el.Elements().ElementAt(j).Name.LocalName;
                            }
                            else
                                dictionaryMXLFile.Add(key, value);
                        }
                        else
                            dictionaryMXLFile.Add(key, value);
                    }
                }
            }
         
            return dictionaryMXLFile;
        }


        public byte[] ConvertFromUtf8(byte[] bytes)
        {
            var enc = new UTF8Encoding(true);
            var preamble = enc.GetPreamble();
            if (preamble.Where((p, i) => p != bytes[i]).Any()) { }
            string vv = enc.GetString(bytes.Skip(preamble.Length).ToArray());
            byte[] by = Encoding.ASCII.GetBytes(vv);
            return by;
        }
       
    }
}
