using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Data;
using SosMedecins.SmartRapport.DAL;
using System.Configuration;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ImportSosGeneve
{
    /// <summary>
    /// Description résumée de frmEncaissement.
    /// </summary>
    public class frmEncaissement : Form
    {
        private Label label1;

        private dstElementsFacture _dstElementsFacture = new dstElementsFacture();
        private Button bFermer;
        private ImageList imageList1;
        private Button bFichier;
        private ProgressBar progressBar1;
        private BackgroundWorker backgroundWorker1;
        private IContainer components;


        private string fichierDest = "";
        private string TraiteFichier = "";
        private Label label2;
        private Label label3;
        private string BVR_SOS = "";
        private string BVR_TA = "";
        private string BVR_CDM = "";
        private string QRIBAN_TA = "";
        private string QRIBAN_SOS = "";


        public frmEncaissement()
        {
            //
            // Requis pour la prise en charge du Concepteur Windows Forms
            //
            InitializeComponent();
            //
            // TODO : ajoutez le code du constructeur après l'appel à InitializeComponent
            //
            SosMedecins.SmartRapport.DAL.dstElementsFactureTableAdapters.facture_moyenTableAdapter z_tarFactureMoyen = new SosMedecins.SmartRapport.DAL.dstElementsFactureTableAdapters.facture_moyenTableAdapter();
            z_tarFactureMoyen.Fill(_dstElementsFacture.facture_moyen);

            DataView z_dvw = new DataView(_dstElementsFacture.facture_moyen);
            z_dvw.Sort = _dstElementsFacture.facture_moyen.OrdreColumn.ColumnName;
        }

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEncaissement));
            this.label1 = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.bFichier = new System.Windows.Forms.Button();
            this.bFermer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkGreen;
            this.label1.Location = new System.Drawing.Point(27, 178);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(505, 152);
            this.label1.TabIndex = 2;
            this.label1.Text = "Fin de traitement :";
            this.label1.Visible = false;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "bfichier.png");
            this.imageList1.Images.SetKeyName(1, "bfichierOff.png");
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(19, 138);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(513, 23);
            this.progressBar1.TabIndex = 35;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(87, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(359, 20);
            this.label2.TabIndex = 36;
            this.label2.Text = "Veuillez selectionner le fichier à importer =>";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(129, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(288, 20);
            this.label3.TabIndex = 37;
            this.label3.Text = "Import des fichiers d\'encaissement";
            // 
            // bFichier
            // 
            this.bFichier.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bFichier.FlatAppearance.BorderSize = 0;
            this.bFichier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bFichier.ImageIndex = 0;
            this.bFichier.ImageList = this.imageList1;
            this.bFichier.Location = new System.Drawing.Point(457, 52);
            this.bFichier.Name = "bFichier";
            this.bFichier.Size = new System.Drawing.Size(75, 75);
            this.bFichier.TabIndex = 34;
            this.bFichier.UseVisualStyleBackColor = true;
            this.bFichier.Click += new System.EventHandler(this.bFichier_Click);
            // 
            // bFermer
            // 
            this.bFermer.BackgroundImage = global::ImportSosGeneve.Properties.Resources.exit;
            this.bFermer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bFermer.FlatAppearance.BorderSize = 0;
            this.bFermer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bFermer.Location = new System.Drawing.Point(502, 333);
            this.bFermer.Name = "bFermer";
            this.bFermer.Size = new System.Drawing.Size(75, 75);
            this.bFermer.TabIndex = 33;
            this.bFermer.UseVisualStyleBackColor = true;
            this.bFermer.Click += new System.EventHandler(this.bFermer_Click);
            // 
            // frmEncaissement
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.ClientSize = new System.Drawing.Size(589, 420);
            this.ControlBox = false;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.bFichier);
            this.Controls.Add(this.bFermer);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmEncaissement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Encaissement";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        /*public string DateFormatMySql(DateTime date)
		{
            string z_strRetour = "NULL";
            if (date != null)
            {
                z_strRetour = date.ToString("yyyyMMdd HH:mm:ss");
            }
            return z_strRetour;
        }*/


        private void bFichier_Click(object sender, EventArgs e)
        {
            //On va chercher le fichier à ajouter                                   
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Relevés versements bancaire (*.xml, *.XML) | *.xml; *.XML";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filename = openFileDialog1.FileName;
                fichierDest = Path.GetFileName(filename);

                //Chemin des documents Facturation ou Facturation_TA sur le SDBSOS (192.168.0.8)                
                string chemin = SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.Path_Facturation;   //System Parametrage.cs                                  

                fichierDest = chemin + fichierDest;

                //Récup des Refs BVR
                BVR_SOS = ConfigurationManager.AppSettings["Ref_BVR_SOS"];
                BVR_TA = ConfigurationManager.AppSettings["Ref_BVR_TA"];
                BVR_CDM = ConfigurationManager.AppSettings["Ref_BVR_CDM"];
                QRIBAN_TA = ConfigurationManager.AppSettings["Ref_QRIBAN_TA"];
                QRIBAN_SOS = ConfigurationManager.AppSettings["Ref_QRIBAN_SOS"];

                //             //Si le fichier existe déjà, on ne le réimporte pas                
                if (File.Exists(fichierDest))
                    MessageBox.Show("Ce fichier a déjà été enregistré!", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    string TypeFichier = Path.GetExtension(filename);
                    if (TypeFichier == ".xml" || TypeFichier == ".XML")
                    {
                        int compteurMax = 0;

                        //XML ------ SOS, TA ou CM
                        TraiteFichier = "XML";

                        //Puis on copie le fichier dans la destination
                        File.Copy(filename, fichierDest, false);

                        //On a modifié le fichier? si oui, on le recharge
                        //if (modif == 1)

                        //Récup des infos du fichiers                                              
                        List<ListeBlocs> blocs = ParseXml.RecupListOperations(filename, BVR_SOS, BVR_TA, BVR_CDM, QRIBAN_TA, QRIBAN_SOS);

                        foreach (ListeBlocs Item in blocs)
                        {
                            compteurMax += int.Parse(Item.NbEcritures.ToString());   //le nombre total d'opérations   
                        }

                        progressBar1.Minimum = 0;
                        progressBar1.Maximum = compteurMax;

                        //On démarre le thread en tache de fond
                        backgroundWorker1.RunWorkerAsync();
                    }
                    else
                    {
                        MessageBox.Show("C'est quoi ce fichier? en tout cas c'est pas du camt.054", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //On supprime le fichier de format "inconnu"
                        //File.Delete(fichierDest);
                    }
                }
            }
        }


        private void bFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void AfficheLog()
        {
            //string Repertoirelog = ConfigurationManager.AppSettings["Path_Log_Postfinance"];
            string PathLog = SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.Path_Log_Postfinance;
            DirectoryInfo Repertoirelog = new DirectoryInfo(PathLog);

            FileInfo[] files = Repertoirelog.GetFiles();
            DateTime lastWrite = DateTime.MinValue;
            FileInfo lastWritenFile = null;

            foreach (FileInfo file in files)
            {
                if (file.LastWriteTime > lastWrite)
                {
                    lastWrite = file.LastWriteTime;
                    lastWritenFile = file;
                }
            }

            //le dernier fichier sera lastWritenFile                 
            if (lastWritenFile.Exists)
            {
                //On affiche le doc
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo.FileName = lastWritenFile.FullName;
                proc.Start();
            }
        }

        //******************Pour l'affichage de la jauge  Traitement principal******************************
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (TraiteFichier == "XML")
            {
                //#############################Traitement du fichier XML#####################                    
                int compteur = 0;
                int NbTotalFacturesXML = 0;
                string NbTotalFactures = "0";
                string NFacture = "";
                decimal TotalFacXML = 0;
                decimal TotalFac = 0;
                int Erreurs = 0;
                string SOS_OU_TA_OU_CDM = "";   //Pour afficher en fin de traitement si c'est du TA ou SOS (police)

                //Pour l'enregistrement
                //TableFactureEtat dalTableFactureEtat = new TableFactureEtat();
                // TableFacture dalTableFacture = new TableFacture();

                //Ecriture dans le fichier log 
                ImportSosGeneve.LogMod logmod = new LogMod();
                logmod.EcrireLog(DateTime.Now.ToString() + " --------------- Facture ------------------  ", "");
                logmod.EcrireLog(" --------------- Encaissement des factures --------- ", "");

                //Création d'un dictionnaire au cas ou on doit traiter des doublon dans la transaction....Elle aime pas du tout!
                var dictionaire = new Dictionary<int, string>();
                int i = 0;

                //Connexion à la base
                string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
                SqlConnection dbConnection = new SqlConnection(connex);

                dbConnection.Open();

                SqlCommand cmd = dbConnection.CreateCommand();
                SqlTransaction transaction;             //Pour l'insert avec transaction

                transaction = dbConnection.BeginTransaction("transac");     //Démarre une transaction locale
                try
                {
                    cmd.Connection = dbConnection;
                    cmd.Transaction = transaction;    //On affecte la transaction

                    List<ListeBlocs> blocs = ParseXml.RecupListOperations(fichierDest, BVR_SOS, BVR_TA, BVR_CDM, QRIBAN_TA, QRIBAN_SOS);

                    foreach (ListeBlocs Item in blocs)
                    {
                        NbTotalFacturesXML += int.Parse(Item.NbEcritures.ToString());   //le nombre total d'opérations
                        TotalFacXML += decimal.Parse(Item.Amt.ToString());     //Le montant total des opérations

                        foreach (Operations detail in Item.Ope)
                        {
                            string NMed = "";
                            string IdAbonnement = "";
                            string Montant = "";
                            string txtPayeCommentaire = "Encaissement automatique Cam54";
                            string DatePaiment = "";
                            DateTime DtPaiement1 = DateTime.Now;
                            string Moyen = "";



                            if ((BVR_SOS == Item.NtryRef.ToString() || QRIBAN_SOS == Item.NtryRef.ToString())
                                && Item.AcctSvcrRef != "Caisse des Médecins" && detail.RmtInfRef.TrimEnd(' ').Substring(6, 1) == "1")
                            {
                                SOS_OU_TA_OU_CDM = "SOS BVR";
                                Moyen = "4";     //facture_Moyen....ici SBVR

                                string LigneNosRefs = detail.RmtInfRef.ToString().TrimEnd(' ');

                                //Ligne BVR 16 ou 27 et sans TA?
                                if (LigneNosRefs.Length == 27 && LigneNosRefs.Substring(6, 1) != "0")
                                {
                                    //BVR 27                                                                       
                                    NFacture = LigneNosRefs.Substring(12, 8);
                                    NMed = LigneNosRefs.Substring(20, 6);
                                    Montant = detail.Amt.ToString();
                                }
                                else
                                {   //BVR 16 complété avec des 0 devant                                   
                                    NFacture = LigneNosRefs.Substring(10, 8);
                                    NMed = LigneNosRefs.Substring(21, 5);
                                    Montant = detail.Amt.ToString();
                                }

                                //Ajout au dictionnaire
                                if (!dictionaire.ContainsValue(NFacture))
                                {
                                    dictionaire.Add(i, NFacture);
                                    i++;

                                    //pas de doublon =>Faire le traitement

                                    //DatePaiment = Item.DateValeur.Substring(8, 2) + "." + Item.DateValeur.Substring(5, 2) + "." + Item.DateValeur.Substring(0, 4);                  
                                    DatePaiment = Item.DateEcriture.ToString();
                                    DtPaiement1 = DateTime.Parse(DatePaiment);

                                    //On recherche la facture dans la base
                                    DataRow[] rows = OutilsExt.OutilsSql.RecuperationFacturesByNFacture(long.Parse(NFacture.ToString()));
                                    long fac_med = long.Parse(NFacture.ToString());
                                    DataRow[] meds = OutilsExt.OutilsSql.RecuperationMedByNFacture(fac_med);
                                    
                                    //On vérifie que c'est bien la bonne avec le bon médecin
                                    if (rows != null && rows.Length == 1)
                                    {
                                        if (long.Parse(meds[0]["CodeIntervenant"].ToString()) == long.Parse(NMed))
                                        {
                                            fac_med = long.Parse(meds[0]["CodeIntervenant"].ToString());

                                            //On regarde si elle n'est pas déjà soldée (solde entre -1 et 1)
                                            if (Decimal.Parse(rows[0]["Solde"].ToString()) > -1 && Decimal.Parse(rows[0]["Solde"].ToString()) < 1)
                                            {
                                                //Facture soldée on met juste un message
                                                //On écrit dans le log
                                                logmod.EcrireLog(DatePaiment + ": la facture  n° ", NFacture.ToString() + ", a déjà été payée le " + rows[0]["FacDateAcquittee"].ToString() + ", le montant de ce paiement en trop est de " + Montant);

                                                compteur++;
                                            }
                                            else
                                            {
                                                //On met à jour le montant de l'enregistrement                                                                                   
                                                Decimal decMontant = Decimal.Parse(Montant);

                                                DataSet ds = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet("select * from facture where NFacture =" + long.Parse(NFacture.ToString()));

                                                //Vérification du solde de la facture
                                                Decimal NvxSolde = Decimal.Parse(ds.Tables[0].Rows[0][13].ToString()) - decMontant;

                                                //Insertion d'une ligne dans facture Etat
                                                string sqlstr0 = "INSERT INTO facture_etats";
                                                sqlstr0 += " (NFacture, Etat, DateEtat, DateOp, CommentaireEtat, Param1, Param2, CodeUtilisateur, Montant, DatePaye, Moyen)";
                                                sqlstr0 += " VALUES(@NFacture, 6, @DtPaiement, @DateOp, @Commentaire, @Param1, @Param2, @User, @Montant, @DtPaiement, @Moyen)";

                                                cmd.CommandText = sqlstr0;

                                                cmd.Parameters.Clear();
                                                cmd.Parameters.AddWithValue("NFacture", NFacture.ToString());
                                                cmd.Parameters.AddWithValue("DateEtat", DtPaiement1.ToString());
                                                cmd.Parameters.AddWithValue("DateOp", DateTime.Now.ToString("dd.MM.yyyy 00:00:00"));
                                                cmd.Parameters.AddWithValue("Commentaire", txtPayeCommentaire);
                                                cmd.Parameters.AddWithValue("Param1", "");
                                                cmd.Parameters.AddWithValue("Param2", "");
                                                cmd.Parameters.AddWithValue("User", SosMedecins.SmartRapport.GestionApplication.VariablesApplicatives.Utilisateurs.Identifiant);

                                                if (NvxSolde >= 0)
                                                    cmd.Parameters.AddWithValue("Montant", decMontant.ToString());
                                                else if (NvxSolde < 0 && NvxSolde > -1)    //Si solde négatif                                                                                                   
                                                    cmd.Parameters.AddWithValue("Montant", decMontant.ToString());
                                                else if (NvxSolde <= -1)
                                                    cmd.Parameters.AddWithValue("Montant", ds.Tables[0].Rows[0][13].ToString());

                                                cmd.Parameters.AddWithValue("DtPaiement", DtPaiement1.ToString());
                                                cmd.Parameters.AddWithValue("Moyen", Moyen);

                                                cmd.ExecuteNonQuery();

                                                if (NvxSolde < 1)    //On considère que la facture est soldée
                                                {
                                                    //Mise à jour de Facture Status                                                                               
                                                    string sqlstr1 = "UPDATE facture_status";
                                                    sqlstr1 += " SET FacDateAcquittee = @DtPaiement";
                                                    sqlstr1 += " WHERE NFacture = @NumFacture";

                                                    cmd.CommandText = sqlstr1;

                                                    cmd.Parameters.Clear();
                                                    cmd.Parameters.AddWithValue("NumFacture", NFacture.ToString());
                                                    cmd.Parameters.AddWithValue("DtPaiement", DtPaiement1.ToString());

                                                    cmd.ExecuteNonQuery();

                                                    //Mise à jour du Solde = 0 de la facture                                       
                                                    string sqlstr2 = "UPDATE facture";
                                                    sqlstr2 += " SET solde = 0";
                                                    sqlstr2 += " WHERE NFacture = @NumFacture";

                                                    cmd.CommandText = sqlstr2;

                                                    cmd.Parameters.Clear();
                                                    cmd.Parameters.AddWithValue("NumFacture", NFacture.ToString());

                                                    cmd.ExecuteNonQuery();
                                                }
                                                else
                                                {
                                                    //Sinon pas soldée mise à jour du Solde de la facture                                        
                                                    string sqlstr2 = "UPDATE facture";
                                                    sqlstr2 += " SET solde = solde - @Montant";
                                                    sqlstr2 += " WHERE NFacture = @NumFacture";

                                                    cmd.CommandText = sqlstr2;

                                                    cmd.Parameters.Clear();
                                                    cmd.Parameters.AddWithValue("NumFacture", NFacture.ToString());
                                                    cmd.Parameters.AddWithValue("Montant", decMontant.ToString());

                                                    cmd.ExecuteNonQuery();

                                                    //On écrit dans le log
                                                    //logmod.EcrireLog(DatePaiment + ": la facture  n° ", NFacture.ToString() + ", d'un montant initial de : " + ds.Tables[0].Rows[0][12].ToString() + ", a un solde restant de " + NvxSolde + ", après le règlement d'un montant de " + decMontant);
                                                }

                                                //Pour les logs: Solde négatif?    ###### Activer quand gestion des frais de rappel   ########
                                                if (NvxSolde <= -1)
                                                {
                                                    //on recupère l'état des frais de rappel                                                   
                                                    /*string sqlstr2a = "select * from FraisRappel where NFacture = @NumFacture";
                                                    cmd.CommandText = sqlstr2a;

                                                    cmd.Parameters.Clear();
                                                    cmd.Parameters.AddWithValue("NumFacture", NFacture);

                                                    DataTable dtFrais = new DataTable();
                                                    dtFrais.Load(cmd.ExecuteReader());

                                                    if (dtFrais.Rows.Count > 0)
                                                    {
                                                        //On prend le 1er et on le met à jour                                                                                            
                                                        string sqlstr2b = "UPDATE FraisRappel";
                                                        sqlstr2b += " SET Regle =1, DateEtat = @DateEtat";
                                                        sqlstr2b += " WHERE NFacture = @NumFacture";

                                                        cmd.CommandText = sqlstr2b;

                                                        cmd.Parameters.Clear();
                                                        cmd.Parameters.AddWithValue("NumFacture", NFacture);
                                                        cmd.Parameters.AddWithValue("DateEtat", DateTime.Now);

                                                        cmd.ExecuteNonQuery();

                                                        //On écrit dans le log
                                                        logmod.EcrireLog(DatePaiment + ": la facture  n° ", NFacture.ToString() + ", Règlement des frais de rappel d'un montant de : " + NvxSolde);
                                                    }
                                                    else
                                                    {
                                                        //Pas de rappel
                                                        logmod.EcrireLog(DatePaiment + ": Erreur.... facture  n° ", NFacture.ToString() + " trop perçu. Rembourser " + NvxSolde + ".");
                                                        Erreurs = 1;
                                                    }*/

                                                    //Pas de rappel
                                                    logmod.EcrireLog(DatePaiment + ": Erreur.... facture  n° ", NFacture.ToString() + " trop perçu. Rembourser " + NvxSolde + ".");
                                                    Erreurs = 1;
                                                }

                                                compteur++;

                                                TotalFac = TotalFac + decimal.Parse(Montant);

                                                //On avance la jauge
                                                backgroundWorker1.ReportProgress(compteur);
                                            }
                                        }
                                        else
                                        {
                                            logmod.EcrireLog(DatePaiment + ": Erreur... facture n° ", NFacture.ToString() + " Erreur sur ligne : " + detail.AcctSvcrRef.ToString() + " " + Moyen + ", le médecin ne correspond pas. Vérifiez la provenance du fichier (CDM etc...)");
                                            Erreurs = 1;
                                        }
                                    }
                                    else
                                    {
                                        logmod.EcrireLog(DatePaiment + ": Erreur... facture n° ", NFacture.ToString() + " Erreur sur ligne : " + detail.AcctSvcrRef.ToString() + " " + Moyen + ", facture non trouvée");
                                        Erreurs = 1;
                                    }
                                }
                                else
                                {
                                    //Doublon => message erreur dans log
                                    DatePaiment = Item.DateEcriture.ToString();

                                    logmod.EcrireLog(DatePaiment + ": Erreur... facture n° ", NFacture.ToString() + " est Doublon : " + detail.AcctSvcrRef.ToString() + " " + Moyen + ", Un bulletin de " + Montant + " est en double. Il n'a pas été affectée.");
                                    Erreurs = 1;
                                }

                            }    //Fin de SOS BVR
                            else if ((BVR_TA == Item.NtryRef.ToString() || QRIBAN_TA == Item.NtryRef.ToString()) && detail.RmtInfRef.TrimEnd(' ').Substring(0, 1) == "0")   //Ce sont des TA (Pas TA MAT)
                            {
                                //ICI distinguer TA Abonnement et Matériel....
                                //TA ou Matériel?
                                //  if (detail.RmtInfRef.TrimEnd(' ').Length == 27 && detail.RmtInfRef.TrimEnd(' ').Substring(6, 1) == "2")   //Ce sont des TA MAT

                                SOS_OU_TA_OU_CDM = "TA";

                                string TypeFacture = "Inconnue";

                                //On parse la ref BVR (nos refs)   
                                //Ligne BVR 16 ou 27?
                                string LigneNosRefs = detail.RmtInfRef.ToString().TrimEnd(' ');

                                if (LigneNosRefs.Length == 27 && LigneNosRefs.Substring(6, 1) != "0")  //BVR 27 TA (1 Facture SOS)
                                {
                                    //BVR 27                                    
                                    NFacture = LigneNosRefs.Substring(12, 8);
                                    IdAbonnement = LigneNosRefs.Substring(20, 6);
                                    Montant = detail.Amt.ToString();

                                    DatePaiment = Item.DateEcriture.ToString();
                                    DtPaiement1 = DateTime.Parse(DatePaiment);

                                    if (LigneNosRefs.Substring(6, 1) == "2")  //TA abonnement
                                    {
                                        TypeFacture = "TA Abonnement";
                                    }
                                    else if (LigneNosRefs.Substring(6, 1) == "3")  //TA Matériel
                                    {
                                        TypeFacture = "TA Matériel";
                                    }
                                }
                                else
                                {   //BVR 16 TA Abonnement seulement                                   
                                    NFacture = LigneNosRefs.Substring(11, 8);         //On affecte le n° de facture
                                    IdAbonnement = LigneNosRefs.Substring(20, 6);     //Sur 6 caractères
                                    Montant = detail.Amt.ToString();

                                    DatePaiment = Item.DateEcriture.ToString();
                                    DtPaiement1 = DateTime.Parse(DatePaiment);

                                    TypeFacture = "TA Abonnement";
                                }

                                //Ajout au dictionnaire
                                if (!dictionaire.ContainsValue(NFacture))
                                {
                                    dictionaire.Add(i, NFacture);
                                    i++;

                                    //pas de doublon =>Faire le traitement

                                    //si c'est pas une facture bizare
                                    if (TypeFacture != "Inconnue")
                                    {
                                        DataSet ds = new DataSet();

                                        if (TypeFacture == "TA Abonnement")
                                        {
                                            ds = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet("select * from ta_factures where NFacture='" + NFacture + "'");
                                        }
                                        else
                                        {
                                            //On recherche la facture Materiel dans la base
                                            ds = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet("select * from TA_FactMat where NumFacture='" + NFacture + "'");
                                        }

                                        //On met à jour le montant de l'enregistrement                                    
                                        decimal decMontant = decimal.Parse(Montant);
                                        decimal decTotal = decimal.Parse(Montant);

                                        //Si on a quelque chose...
                                        if (ds != null && ds.Tables[0].Rows.Count >= 1)
                                        {
                                            string sqlstr2 = "";
                                            string Acquitement = "";

                                            if (TypeFacture == "TA Abonnement")
                                                Acquitement = ds.Tables[0].Rows[0][12].ToString();
                                            else
                                            {
                                                if (ds.Tables[0].Rows[0]["DateAcquitementFact"] == DBNull.Value)
                                                    Acquitement = "0";
                                                else Acquitement = "1";
                                            }

                                            if (Acquitement == "1")     //Si elle est déjà acquittée
                                            {
                                                if (TypeFacture == "TA Abonnement")
                                                {
                                                    //Facture Montant = Solde et non acquité.....On en recherche d'autres factures non acquitées pour ce patient
                                                    // (Abonnement)                                    
                                                    sqlstr2 = "SELECT f.NFacture, f.Idabonnement, f.Montant, f.Solde ";
                                                    sqlstr2 += " FROM ta_factures f, ta_abonnement ab";
                                                    sqlstr2 += " WHERE f.Idabonnement = ab.IdAbonnement";
                                                    sqlstr2 += " AND ab.Archive = 0";
                                                    sqlstr2 += " AND f.Idabonnement = @IDabonnement";
                                                    sqlstr2 += " AND f.Acquité = 0 AND f.Date_facture >= DateAdd(Year, -2, getdate())";
                                                    sqlstr2 += " AND f.DateAnnulation is null ";
                                                    // sqlstr2 += " AND f.Montant = @Montant ";
                                                    sqlstr2 += " ORDER BY f.Date_facture asc";
                                                }
                                                else
                                                {
                                                    //Facture Montant = Solde et non acquité.....On en recherche d'autres factures non acquitées pour ce patient
                                                    //(Matériel)                                   
                                                    sqlstr2 = "SELECT f.NumFacture, f.IdAbonnement, f.TotalFacture, f.Solde ";
                                                    sqlstr2 += " FROM TA_FactMat f, ta_abonnement ab";
                                                    sqlstr2 += " WHERE f.Idabonnement = ab.IdAbonnement";
                                                    sqlstr2 += " AND ab.Archive = 0";
                                                    sqlstr2 += " AND f.IdAbonnement = @IDabonnement";
                                                    sqlstr2 += " AND f.DateAcquitementFact IS Null AND f.DateFacture >= DateAdd(Year, -2, getdate())";
                                                    sqlstr2 += " AND f.DateAnnulation is null ";
                                                    //sqlstr2 += " AND f.TotalFacture = @Montant ";
                                                    sqlstr2 += " ORDER BY f.DateFacture asc";
                                                }

                                                cmd.CommandText = sqlstr2;

                                                cmd.Parameters.Clear();
                                                cmd.Parameters.AddWithValue("IDabonnement", ds.Tables[0].Rows[0][1].ToString());
                                                //cmd.Parameters.AddWithValue("Montant", Montant);

                                                DataSet FactureDispo = new DataSet();
                                                FactureDispo.Tables.Add("Resultats");
                                                FactureDispo.Tables["Resultats"].Load(cmd.ExecuteReader());       //on execute

                                                //Si on en a au moins 1!   
                                                if (FactureDispo != null && FactureDispo.Tables[0].Rows.Count >= 1)
                                                {
                                                    //Ajout au dictionnaire de traitement des doublons 
                                                    if (!dictionaire.ContainsValue(FactureDispo.Tables[0].Rows[0][0].ToString()))
                                                    {
                                                        dictionaire.Add(i, FactureDispo.Tables[0].Rows[0][0].ToString());
                                                        i++;

                                                        //pas de doublon =>Faire le traitement                                                                                                    
                                                        decimal MontantImput = decMontant;
                                                        decimal MontantImputRestant = 0;
                                                        decimal Solde = 0;
                                                        string Filtre = "";

                                                        //On met à jour cet autre facture TA non soldés trouvée
                                                        decimal SoldeFacture = decimal.Parse(FactureDispo.Tables[0].Rows[0]["Solde"].ToString());
                                                        decimal MontantOp = 0;

                                                        //Mise à jour du Solde de la facture                                        
                                                        if (MontantImput >= SoldeFacture)
                                                        {
                                                            //Montant à imputer >= au solde de la facture donc on solde la facture                                                    
                                                            MontantImputRestant = MontantImput - SoldeFacture;
                                                            MontantOp = SoldeFacture;
                                                            Solde = 0;

                                                            if (TypeFacture == "TA Abonnement")
                                                                //facture soldée (TA Abonnement)
                                                                Filtre = " SET Acquité = 1, Moyen = 'BVR', SBVR = 1, Solde = @Solde, Payé = '" + DtPaiement1.ToString() + "'";
                                                            else
                                                                //facture soldée (TA Mat)
                                                                Filtre = " SET DateAcquitementFact = '" + DtPaiement1.ToString() + "', Solde = @Solde";
                                                        }
                                                        else //Donc pas assez pour solder la facture
                                                        {
                                                            MontantImputRestant = 0;
                                                            MontantOp = MontantImput;
                                                            Solde = SoldeFacture - MontantOp;

                                                            if (TypeFacture == "TA Abonnement")
                                                                //Facture Non soldée   Solde = SoldeFacture - MontantOp  (TA Abonnement)                                                 
                                                                Filtre = " SET Acquité = 0, Moyen = 'BVR', SBVR = 1, Solde = @Solde";
                                                            else
                                                                //Facture Non soldée   Solde = SoldeFacture - MontantOp  (TA Materiel)                                                 
                                                                Filtre = " SET Solde = @Solde";
                                                        }

                                                        //Maj des tables
                                                        string sqlstr0 = "";

                                                        if (TypeFacture == "TA Abonnement")
                                                        {
                                                            sqlstr0 = "UPDATE ta_factures";
                                                            sqlstr0 += Filtre;
                                                            sqlstr0 += " WHERE NFacture = @NumFacture";
                                                        }
                                                        else
                                                        {
                                                            sqlstr0 = "UPDATE TA_FactMat";
                                                            sqlstr0 += Filtre;
                                                            sqlstr0 += " WHERE NumFacture = @NumFacture";
                                                        }

                                                        cmd.CommandText = sqlstr0;

                                                        cmd.Parameters.Clear();
                                                        cmd.Parameters.AddWithValue("NumFacture", FactureDispo.Tables[0].Rows[0][0].ToString());
                                                        cmd.Parameters.AddWithValue("Solde", Solde);

                                                        cmd.ExecuteNonQuery();

                                                        //Puis ajout d'une ligne dans les op
                                                        string sqlstr1 = "";
                                                        if (TypeFacture == "TA Abonnement")
                                                        {
                                                            sqlstr1 = "INSERT INTO TA_Factures_Op";
                                                            sqlstr1 += " (NumFacture, DateEncaissement, Montant, MoyenPaiement)";
                                                            sqlstr1 += " VALUES(@NFacture, @DtEncaissement, @Montant, @Moyen)";
                                                        }
                                                        else
                                                        {
                                                            sqlstr1 = "INSERT INTO TA_FactMat_Op";
                                                            sqlstr1 += " (NumFacture, DateEncaissement, Montant, MoyenPaiement)";
                                                            sqlstr1 += " VALUES(@NFacture, @DtEncaissement, @Montant, @Moyen)";
                                                        }

                                                        cmd.CommandText = sqlstr1;

                                                        cmd.Parameters.Clear();
                                                        cmd.Parameters.AddWithValue("NFacture", FactureDispo.Tables[0].Rows[0][0].ToString());
                                                        cmd.Parameters.AddWithValue("DtEncaissement", DtPaiement1.ToString());
                                                        cmd.Parameters.AddWithValue("Montant", MontantOp.ToString());
                                                        cmd.Parameters.AddWithValue("Moyen", "BVR");

                                                        cmd.ExecuteNonQuery();

                                                        string MessageLog = "";

                                                        //A la fin si malgrès tout il reste du solde à imputer, on le signale 
                                                        if (MontantImputRestant > 0)
                                                        {
                                                            //On écrit dans le log une erreur                                                    
                                                            MessageLog = " Ventilation sur une ancienne facture n° " + FactureDispo.Tables[0].Rows[0][0].ToString() + ", dont le montant est de " + FactureDispo.Tables[0].Rows[0][2].ToString() + ". Le montant payé est de " + MontantOp + ".";
                                                            MessageLog += " \r\n Mais il reste un montant de " + MontantImputRestant + ", à imputer après recherche de facture non payées.";
                                                        }
                                                        else
                                                        {
                                                            MessageLog = " Ventilation sur une autre facture n° " + FactureDispo.Tables[0].Rows[0][0].ToString();
                                                        }

                                                        //logmod.EcrireLogTA(DatePaiment + ": Attention...la facture TA n° " + NFacture.ToString() + " est déjà acquittée.", MessageLog);
                                                        logmod.EcrireLog(DatePaiment + ": Attention...la facture TA n° " + NFacture.ToString() + " est déjà acquittée.", MessageLog);
                                                        Erreurs = 1;

                                                        //On avance la jauge
                                                        compteur++;
                                                        TotalFac = TotalFac + MontantOp;

                                                        //On avance la jauge
                                                        backgroundWorker1.ReportProgress(compteur);
                                                    }   //Fin ajout traitement des doublons
                                                    else
                                                    {
                                                        //Doublon => message erreur dans log
                                                        DatePaiment = Item.DateEcriture.ToString();
                                                        logmod.EcrireLogTA(DatePaiment + ": Erreur... FAC n° ", NFacture.ToString() + " est un doublon, " + detail.AcctSvcrRef.ToString() + " " + Moyen + ". Un bulletin de " + Montant + " est en double. Le montant n'a pas été affectée.");
                                                        Erreurs = 1;
                                                    }
                                                }    //Fin d'on a une facture non soldé dispo                                                                                                                                                                         
                                                else
                                                {
                                                    //Pas d'autres facture à imputer avec cette somme, on le signale                                                
                                                    string MessageLog = " Il n'y a pas d'autre facture dispo pour une ventilation. Le montant payé est de " + decMontant + ".";
                                                    //logmod.EcrireLogTA(DatePaiment + ": Attention...facture TA n° " + NFacture.ToString() + " est déjà acquittée.", MessageLog);
                                                    logmod.EcrireLog(DatePaiment + ": Attention...facture TA n° " + NFacture.ToString() + " est déjà acquittée.", MessageLog);
                                                    Erreurs = 1;
                                                }  //Fin de pas d'autre facture pour imputer ce montant                                                                                                                                                                                                                                                                                    

                                            }   //Fin de facture TA déjà payée
                                            else   //Pas soldée
                                            {
                                                //Mise à jour de la facture TA                                                                            
                                                decimal SoldeFacture = decimal.Parse(ds.Tables[0].Rows[0]["Solde"].ToString());

                                                decimal MontantImput = decMontant;
                                                decimal MontantImputRestant = 0;

                                                decimal Solde = 0;
                                                string Filtre = "";
                                                decimal MontantOp = 0;

                                                //Mise à jour du Solde de la facture                                        
                                                if (MontantImput >= SoldeFacture)
                                                {
                                                    //Montant à imputer >= au solde de la facture donc on solde la facture                                               
                                                    MontantImputRestant = MontantImput - SoldeFacture;
                                                    MontantOp = SoldeFacture;
                                                    Solde = 0;

                                                    //facture soldée                                           
                                                    if (TypeFacture == "TA Abonnement")
                                                        //Facture Non soldée   Solde = SoldeFacture - MontantOp  (TA Abonnement)                                                 
                                                        Filtre = " SET Acquité = 1, Moyen = 'BVR', SBVR = 1, Solde = @Solde, Payé = '" + DtPaiement1.ToString() + "'";
                                                    else
                                                        //Facture Non soldée   Solde = SoldeFacture - MontantOp  (TA Matériel)                                                 
                                                        Filtre = " SET Solde = @Solde, DateAcquitementFact = '" + DtPaiement1.ToString() + "'";
                                                }
                                                else //< donc pas assez pour solder la facture
                                                {
                                                    MontantImputRestant = 0;
                                                    MontantOp = MontantImput;
                                                    Solde = SoldeFacture - MontantOp;

                                                    if (TypeFacture == "TA Abonnement")
                                                        //Facture Non soldée   Solde = SoldeFacture - MontantOp  (TA Abonnement)                                                 
                                                        Filtre = " SET Acquité = 0, Moyen = 'BVR', SBVR = 1, Solde = @Solde";
                                                    else
                                                        //Facture Non soldée   Solde = SoldeFacture - MontantOp  (TA Matériel)                                                 
                                                        Filtre = " SET Solde = @Solde";
                                                }

                                                //Maj des tables
                                                string sqlstr0 = "";

                                                if (TypeFacture == "TA Abonnement")
                                                {
                                                    sqlstr0 = "UPDATE ta_factures";
                                                    sqlstr0 += Filtre;
                                                    sqlstr0 += " WHERE NFacture = @NumFacture";
                                                }
                                                else
                                                {
                                                    sqlstr0 = "UPDATE TA_FactMat";
                                                    sqlstr0 += Filtre;
                                                    sqlstr0 += " WHERE NumFacture = @NumFacture";
                                                }

                                                cmd.CommandText = sqlstr0;

                                                cmd.Parameters.Clear();
                                                cmd.Parameters.AddWithValue("NumFacture", ds.Tables[0].Rows[0][0].ToString());
                                                cmd.Parameters.AddWithValue("Solde", Solde);

                                                cmd.ExecuteNonQuery();

                                                //Puis ajout d'une ligne dans les op
                                                string sqlstr1 = "";
                                                if (TypeFacture == "TA Abonnement")
                                                {
                                                    sqlstr1 = "INSERT INTO TA_Factures_Op";
                                                    sqlstr1 += " (NumFacture, DateEncaissement, Montant, MoyenPaiement)";
                                                    sqlstr1 += " VALUES(@NFacture, @DtEncaissement, @Montant, @Moyen)";
                                                }
                                                else
                                                {
                                                    sqlstr1 = "INSERT INTO TA_FactMat_Op";
                                                    sqlstr1 += " (NumFacture, DateEncaissement, Montant, MoyenPaiement)";
                                                    sqlstr1 += " VALUES(@NFacture, @DtEncaissement, @Montant, @Moyen)";
                                                }

                                                cmd.CommandText = sqlstr1;

                                                cmd.Parameters.Clear();
                                                cmd.Parameters.AddWithValue("NFacture", ds.Tables[0].Rows[0][0].ToString());
                                                cmd.Parameters.AddWithValue("DtEncaissement", DtPaiement1.ToString());
                                                cmd.Parameters.AddWithValue("Montant", MontantOp.ToString());
                                                cmd.Parameters.AddWithValue("Moyen", "BVR");

                                                cmd.ExecuteNonQuery();

                                                //A la fin si malgrès tout il reste du solde à imputer, on le signale 
                                                if (MontantImputRestant > 0)
                                                {
                                                    //On écrit dans le log une erreur                                                    
                                                    string MessageLog = " Il y a un montant restant à imputer après avoir soldé la facture. Le montant restant est de " + MontantImputRestant + ", le montant réglé est de " + MontantOp.ToString();
                                                    //logmod.EcrireLogTA(DatePaiment + ": Attention...facture TA n° " + NFacture + ".", MessageLog);
                                                    logmod.EcrireLog(DatePaiment + ": Attention...facture TA n° " + NFacture + ".", MessageLog);
                                                    Erreurs = 1;
                                                }

                                                //On avance la jauge
                                                compteur++;

                                                TotalFac = TotalFac + MontantOp;

                                                //On avance la jauge
                                                backgroundWorker1.ReportProgress(compteur);
                                            }   //Fin d'elle n'est pas soldée
                                        }   //Fin d'on a trouvé la facture
                                        else
                                        {
                                            //On regarde si c'est pas une facture SOS
                                            string NFactureSOS = "";

                                            // string LigneNosRefs = detail.RmtInfRef.ToString().TrimEnd(' ');

                                            if (LigneNosRefs.Length == 27 && LigneNosRefs.Substring(6, 1) != "0")
                                                NFactureSOS = LigneNosRefs.Substring(12, 8);   //BVR 27                                                                         
                                            else
                                                NFactureSOS = LigneNosRefs.Substring(10, 8);    //BVR 16                                        

                                            DataRow[] RowFact = OutilsExt.OutilsSql.RecuperationFacturesByNFacture(long.Parse(NFactureSOS.ToString()));

                                            //Si on l'a trouvé
                                            if (RowFact != null && RowFact.Length == 1)
                                            {
                                                //logmod.EcrireLogTA(DatePaiment + ": Erreur... Facture TA n° ", NFactureSOS.ToString() + ", cette facture est une facture SOS médecins!!!");
                                                logmod.EcrireLog(DatePaiment + ": Erreur... Facture TA n° ", NFactureSOS.ToString() + ", cette facture est une facture SOS médecins!!!");
                                                Erreurs = 1;
                                            }
                                            else
                                            {
                                                //logmod.EcrireLogTA(DatePaiment + ": Erreur... Facture TA n° ", NFacture.ToString() + ", cette facture est inconnue, et ce n'est ni une facture TA ni SOS : " + detail.AcctSvcrRef.ToString());
                                                logmod.EcrireLog(DatePaiment + ": Erreur... Facture TA n° ", NFacture.ToString() + ", cette facture est inconnue, et ce n'est ni une facture TA ni SOS : " + detail.AcctSvcrRef.ToString());
                                                Erreurs = 1;
                                            }
                                        }
                                    }  //Fin de facture pas bizare
                                    else
                                    {
                                        //logmod.EcrireLogTA(DatePaiment + ": Erreur... Facture TA n° ", " La facture dont nos refs sont " + LigneNosRefs + ", est étrange...A regarder de plus près....");
                                        logmod.EcrireLog(DatePaiment + ": Erreur... Facture TA n° ", " La facture dont nos refs sont " + LigneNosRefs + ", est étrange...A regarder de plus près....");
                                        Erreurs = 1;
                                    }
                                }
                                else
                                {
                                    //Doublon => message erreur dans log
                                    DatePaiment = Item.DateEcriture.ToString();
                                    logmod.EcrireLogTA(DatePaiment + ": Erreur... FAC n° ", NFacture.ToString() + " est un doublon, " + detail.AcctSvcrRef.ToString() + " " + Moyen + ". Un bulletin de " + Montant + " est en double. Il n'a pas été affectée.");
                                    Erreurs = 1;
                                }
                            }   //Fin de TA
                            else if (QRIBAN_SOS == Item.NtryRef.ToString() && detail.RmtInfRef.TrimEnd(' ').Substring(6, 1) != "0"
                                                                           && detail.RmtInfRef.TrimEnd(' ').Substring(6, 1) != "1")     //TA mais avec BVR SOS!!!
                            {

                                SOS_OU_TA_OU_CDM = "TA";

                                string TypeFacture = "Inconnue";

                                //On parse la ref BVR (nos refs)   
                                //Ligne BVR 16 ou 27?
                                string LigneNosRefs = detail.RmtInfRef.ToString().TrimEnd(' ');

                                if (LigneNosRefs.Length == 27 && LigneNosRefs.Substring(6, 1) != "0")  //BVR 27 TA (1 Facture SOS)
                                {
                                    //BVR 27                                    
                                    NFacture = LigneNosRefs.Substring(12, 8);
                                    IdAbonnement = LigneNosRefs.Substring(20, 6);
                                    Montant = detail.Amt.ToString();

                                    DatePaiment = Item.DateEcriture.ToString();
                                    DtPaiement1 = DateTime.Parse(DatePaiment);

                                    if (LigneNosRefs.Substring(6, 1) == "2")  //TA abonnement
                                    {
                                        TypeFacture = "TA Abonnement";
                                    }
                                    else if (LigneNosRefs.Substring(6, 1) == "3")  //TA Matériel
                                    {
                                        TypeFacture = "TA Matériel";
                                    }
                                }
                                else
                                {   //BVR 16 TA Abonnement seulement                                   
                                    NFacture = LigneNosRefs.Substring(11, 8);         //On affecte le n° de facture
                                    IdAbonnement = LigneNosRefs.Substring(20, 6);     //Sur 6 caractères
                                    Montant = detail.Amt.ToString();

                                    DatePaiment = Item.DateEcriture.ToString();
                                    DtPaiement1 = DateTime.Parse(DatePaiment);

                                    TypeFacture = "TA Abonnement";
                                }

                                //Ajout au dictionnaire
                                if (!dictionaire.ContainsValue(NFacture))
                                {
                                    dictionaire.Add(i, NFacture);
                                    i++;

                                    //pas de doublon =>Faire le traitement

                                    //si c'est pas une facture bizare
                                    if (TypeFacture != "Inconnue")
                                    {
                                        DataSet ds = new DataSet();

                                        if (TypeFacture == "TA Abonnement")
                                        {
                                            ds = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet("select * from ta_factures where NFacture='" + NFacture + "'");
                                        }
                                        else
                                        {
                                            //On recherche la facture Materiel dans la base
                                            ds = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet("select * from TA_FactMat where NumFacture='" + NFacture + "'");
                                        }

                                        //On met à jour le montant de l'enregistrement                                    
                                        decimal decMontant = decimal.Parse(Montant);
                                        decimal decTotal = decimal.Parse(Montant);

                                        //Si on a quelque chose...
                                        if (ds != null && ds.Tables[0].Rows.Count >= 1)
                                        {
                                            string sqlstr2 = "";
                                            string Acquitement = "";

                                            if (TypeFacture == "TA Abonnement")
                                                Acquitement = ds.Tables[0].Rows[0][12].ToString();
                                            else
                                            {
                                                if (ds.Tables[0].Rows[0]["DateAcquitementFact"] == DBNull.Value)
                                                    Acquitement = "0";
                                                else Acquitement = "1";
                                            }

                                            if (Acquitement == "1")     //Si elle est déjà acquittée
                                            {
                                                if (TypeFacture == "TA Abonnement")
                                                {
                                                    //Facture Montant = Solde et non acquité.....On en recherche d'autres factures non acquitées pour ce patient
                                                    // (Abonnement)                                    
                                                    sqlstr2 = "SELECT f.NFacture, f.Idabonnement, f.Montant, f.Solde ";
                                                    sqlstr2 += " FROM ta_factures f, ta_abonnement ab";
                                                    sqlstr2 += " WHERE f.Idabonnement = ab.IdAbonnement";
                                                    sqlstr2 += " AND ab.Archive = 0";
                                                    sqlstr2 += " AND f.Idabonnement = @IDabonnement";
                                                    sqlstr2 += " AND f.Acquité = 0 AND f.Date_facture >= DateAdd(Year, -2, getdate())";
                                                    sqlstr2 += " AND f.DateAnnulation is null ";
                                                    // sqlstr2 += " AND f.Montant = @Montant ";
                                                    sqlstr2 += " ORDER BY f.Date_facture asc";
                                                }
                                                else
                                                {
                                                    //Facture Montant = Solde et non acquité.....On en recherche d'autres factures non acquitées pour ce patient
                                                    //(Matériel)                                   
                                                    sqlstr2 = "SELECT f.NumFacture, f.IdAbonnement, f.TotalFacture, f.Solde ";
                                                    sqlstr2 += " FROM TA_FactMat f, ta_abonnement ab";
                                                    sqlstr2 += " WHERE f.Idabonnement = ab.IdAbonnement";
                                                    sqlstr2 += " AND ab.Archive = 0";
                                                    sqlstr2 += " AND f.IdAbonnement = @IDabonnement";
                                                    sqlstr2 += " AND f.DateAcquitementFact IS Null AND f.DateFacture >= DateAdd(Year, -2, getdate())";
                                                    sqlstr2 += " AND f.DateAnnulation is null ";
                                                    //sqlstr2 += " AND f.TotalFacture = @Montant ";
                                                    sqlstr2 += " ORDER BY f.DateFacture asc";
                                                }

                                                cmd.CommandText = sqlstr2;

                                                cmd.Parameters.Clear();
                                                cmd.Parameters.AddWithValue("IDabonnement", ds.Tables[0].Rows[0][1].ToString());
                                                //cmd.Parameters.AddWithValue("Montant", Montant);

                                                DataSet FactureDispo = new DataSet();
                                                FactureDispo.Tables.Add("Resultats");
                                                FactureDispo.Tables["Resultats"].Load(cmd.ExecuteReader());       //on execute

                                                //Si on en a au moins 1!   
                                                if (FactureDispo != null && FactureDispo.Tables[0].Rows.Count >= 1)
                                                {
                                                    //Ajout au dictionnaire de traitement des doublons 
                                                    if (!dictionaire.ContainsValue(FactureDispo.Tables[0].Rows[0][0].ToString()))
                                                    {
                                                        dictionaire.Add(i, FactureDispo.Tables[0].Rows[0][0].ToString());
                                                        i++;

                                                        //pas de doublon =>Faire le traitement                                                                                                    
                                                        decimal MontantImput = decMontant;
                                                        decimal MontantImputRestant = 0;
                                                        decimal Solde = 0;
                                                        string Filtre = "";

                                                        //On met à jour cet autre facture TA non soldés trouvée
                                                        decimal SoldeFacture = decimal.Parse(FactureDispo.Tables[0].Rows[0]["Solde"].ToString());
                                                        decimal MontantOp = 0;

                                                        //Mise à jour du Solde de la facture                                        
                                                        if (MontantImput >= SoldeFacture)
                                                        {
                                                            //Montant à imputer >= au solde de la facture donc on solde la facture                                                    
                                                            MontantImputRestant = MontantImput - SoldeFacture;
                                                            MontantOp = SoldeFacture;
                                                            Solde = 0;

                                                            if (TypeFacture == "TA Abonnement")
                                                                //facture soldée (TA Abonnement)
                                                                Filtre = " SET Acquité = 1, Moyen = 'BVR SOS', SBVR = 1, Solde = @Solde, Payé = '" + DtPaiement1.ToString() + "'";
                                                            else
                                                                //facture soldée (TA Mat)
                                                                Filtre = " SET DateAcquitementFact = '" + DtPaiement1.ToString() + "', Solde = @Solde";
                                                        }
                                                        else //Donc pas assez pour solder la facture
                                                        {
                                                            MontantImputRestant = 0;
                                                            MontantOp = MontantImput;
                                                            Solde = SoldeFacture - MontantOp;

                                                            if (TypeFacture == "TA Abonnement")
                                                                //Facture Non soldée   Solde = SoldeFacture - MontantOp  (TA Abonnement)                                                 
                                                                Filtre = " SET Acquité = 0, Moyen = 'BVR SOS', SBVR = 1, Solde = @Solde";
                                                            else
                                                                //Facture Non soldée   Solde = SoldeFacture - MontantOp  (TA Materiel)                                                 
                                                                Filtre = " SET Solde = @Solde";
                                                        }

                                                        //Maj des tables
                                                        string sqlstr0 = "";

                                                        if (TypeFacture == "TA Abonnement")
                                                        {
                                                            sqlstr0 = "UPDATE ta_factures";
                                                            sqlstr0 += Filtre;
                                                            sqlstr0 += " WHERE NFacture = @NumFacture";
                                                        }
                                                        else
                                                        {
                                                            sqlstr0 = "UPDATE TA_FactMat";
                                                            sqlstr0 += Filtre;
                                                            sqlstr0 += " WHERE NumFacture = @NumFacture";
                                                        }

                                                        cmd.CommandText = sqlstr0;

                                                        cmd.Parameters.Clear();
                                                        cmd.Parameters.AddWithValue("NumFacture", FactureDispo.Tables[0].Rows[0][0].ToString());
                                                        cmd.Parameters.AddWithValue("Solde", Solde);

                                                        cmd.ExecuteNonQuery();

                                                        //Puis ajout d'une ligne dans les op
                                                        string sqlstr1 = "";
                                                        if (TypeFacture == "TA Abonnement")
                                                        {
                                                            sqlstr1 = "INSERT INTO TA_Factures_Op";
                                                            sqlstr1 += " (NumFacture, DateEncaissement, Montant, MoyenPaiement)";
                                                            sqlstr1 += " VALUES(@NFacture, @DtEncaissement, @Montant, @Moyen)";
                                                        }
                                                        else
                                                        {
                                                            sqlstr1 = "INSERT INTO TA_FactMat_Op";
                                                            sqlstr1 += " (NumFacture, DateEncaissement, Montant, MoyenPaiement)";
                                                            sqlstr1 += " VALUES(@NFacture, @DtEncaissement, @Montant, @Moyen)";
                                                        }

                                                        cmd.CommandText = sqlstr1;

                                                        cmd.Parameters.Clear();
                                                        cmd.Parameters.AddWithValue("NFacture", FactureDispo.Tables[0].Rows[0][0].ToString());
                                                        cmd.Parameters.AddWithValue("DtEncaissement", DtPaiement1.ToString());
                                                        cmd.Parameters.AddWithValue("Montant", MontantOp.ToString());
                                                        cmd.Parameters.AddWithValue("Moyen", "BVR SOS");

                                                        cmd.ExecuteNonQuery();

                                                        string MessageLog = "";

                                                        //A la fin si malgrès tout il reste du solde à imputer, on le signale 
                                                        if (MontantImputRestant > 0)
                                                        {
                                                            //On écrit dans le log une erreur                                                    
                                                            MessageLog = " Ventilation sur une ancienne facture n° " + FactureDispo.Tables[0].Rows[0][0].ToString() + ", dont le montant est de " + FactureDispo.Tables[0].Rows[0][2].ToString() + ". Le montant payé est de " + MontantOp + ".";
                                                            MessageLog += " \r\n Mais il reste un montant de " + MontantImputRestant + ", à imputer après recherche de facture non payées.";
                                                        }
                                                        else
                                                        {
                                                            MessageLog = " Ventilation sur une autre facture n° " + FactureDispo.Tables[0].Rows[0][0].ToString();
                                                        }

                                                        //logmod.EcrireLogTA(DatePaiment + ": Attention...la facture TA n° " + NFacture.ToString() + " est déjà acquittée.", MessageLog);
                                                        logmod.EcrireLog(DatePaiment + ": Attention...la facture TA n° " + NFacture.ToString() + " est déjà acquittée.", MessageLog);
                                                        Erreurs = 1;

                                                        //On avance la jauge
                                                        compteur++;
                                                        TotalFac = TotalFac + MontantOp;

                                                        //On avance la jauge
                                                        backgroundWorker1.ReportProgress(compteur);
                                                    }   //Fin ajout traitement des doublons
                                                    else
                                                    {
                                                        //Doublon => message erreur dans log
                                                        DatePaiment = Item.DateEcriture.ToString();
                                                        logmod.EcrireLogTA(DatePaiment + ": Erreur... FAC n° ", NFacture.ToString() + " est un doublon, " + detail.AcctSvcrRef.ToString() + " " + Moyen + ". Un bulletin de " + Montant + " est en double. Le montant n'a pas été affectée.");
                                                        Erreurs = 1;
                                                    }
                                                }    //Fin d'on a une facture non soldé dispo                                                                                                                                                                         
                                                else
                                                {
                                                    //Pas d'autres facture à imputer avec cette somme, on le signale                                                
                                                    string MessageLog = " Il n'y a pas d'autre facture dispo pour une ventilation. Le montant payé est de " + decMontant + ".";
                                                    //logmod.EcrireLogTA(DatePaiment + ": Attention...facture TA n° " + NFacture.ToString() + " est déjà acquittée.", MessageLog);
                                                    logmod.EcrireLog(DatePaiment + ": Attention...facture TA n° " + NFacture.ToString() + " est déjà acquittée.", MessageLog);
                                                    Erreurs = 1;
                                                }  //Fin de pas d'autre facture pour imputer ce montant                                                                                                                                                                                                                                                                                    

                                            }   //Fin de facture TA déjà payée
                                            else   //Pas soldée
                                            {
                                                //Mise à jour de la facture TA                                                                            
                                                decimal SoldeFacture = decimal.Parse(ds.Tables[0].Rows[0]["Solde"].ToString());

                                                decimal MontantImput = decMontant;
                                                decimal MontantImputRestant = 0;

                                                decimal Solde = 0;
                                                string Filtre = "";
                                                decimal MontantOp = 0;

                                                //Mise à jour du Solde de la facture                                        
                                                if (MontantImput >= SoldeFacture)
                                                {
                                                    //Montant à imputer >= au solde de la facture donc on solde la facture                                               
                                                    MontantImputRestant = MontantImput - SoldeFacture;
                                                    MontantOp = SoldeFacture;
                                                    Solde = 0;

                                                    //facture soldée                                           
                                                    if (TypeFacture == "TA Abonnement")
                                                        //Facture Non soldée   Solde = SoldeFacture - MontantOp  (TA Abonnement)                                                 
                                                        Filtre = " SET Acquité = 1, Moyen = 'BVR SOS', SBVR = 1, Solde = @Solde, Payé = '" + DtPaiement1.ToString() + "'";
                                                    else
                                                        //Facture Non soldée   Solde = SoldeFacture - MontantOp  (TA Matériel)                                                 
                                                        Filtre = " SET Solde = @Solde, DateAcquitementFact = '" + DtPaiement1.ToString() + "'";
                                                }
                                                else //< donc pas assez pour solder la facture
                                                {
                                                    MontantImputRestant = 0;
                                                    MontantOp = MontantImput;
                                                    Solde = SoldeFacture - MontantOp;

                                                    if (TypeFacture == "TA Abonnement")
                                                        //Facture Non soldée   Solde = SoldeFacture - MontantOp  (TA Abonnement)                                                 
                                                        Filtre = " SET Acquité = 0, Moyen = 'BVR SOS', SBVR = 1, Solde = @Solde";
                                                    else
                                                        //Facture Non soldée   Solde = SoldeFacture - MontantOp  (TA Matériel)                                                 
                                                        Filtre = " SET Solde = @Solde";
                                                }

                                                //Maj des tables
                                                string sqlstr0 = "";

                                                if (TypeFacture == "TA Abonnement")
                                                {
                                                    sqlstr0 = "UPDATE ta_factures";
                                                    sqlstr0 += Filtre;
                                                    sqlstr0 += " WHERE NFacture = @NumFacture";
                                                }
                                                else
                                                {
                                                    sqlstr0 = "UPDATE TA_FactMat";
                                                    sqlstr0 += Filtre;
                                                    sqlstr0 += " WHERE NumFacture = @NumFacture";
                                                }

                                                cmd.CommandText = sqlstr0;

                                                cmd.Parameters.Clear();
                                                cmd.Parameters.AddWithValue("NumFacture", ds.Tables[0].Rows[0][0].ToString());
                                                cmd.Parameters.AddWithValue("Solde", Solde);

                                                cmd.ExecuteNonQuery();

                                                //Puis ajout d'une ligne dans les op
                                                string sqlstr1 = "";
                                                if (TypeFacture == "TA Abonnement")
                                                {
                                                    sqlstr1 = "INSERT INTO TA_Factures_Op";
                                                    sqlstr1 += " (NumFacture, DateEncaissement, Montant, MoyenPaiement)";
                                                    sqlstr1 += " VALUES(@NFacture, @DtEncaissement, @Montant, @Moyen)";
                                                }
                                                else
                                                {
                                                    sqlstr1 = "INSERT INTO TA_FactMat_Op";
                                                    sqlstr1 += " (NumFacture, DateEncaissement, Montant, MoyenPaiement)";
                                                    sqlstr1 += " VALUES(@NFacture, @DtEncaissement, @Montant, @Moyen)";
                                                }

                                                cmd.CommandText = sqlstr1;

                                                cmd.Parameters.Clear();
                                                cmd.Parameters.AddWithValue("NFacture", ds.Tables[0].Rows[0][0].ToString());
                                                cmd.Parameters.AddWithValue("DtEncaissement", DtPaiement1.ToString());
                                                cmd.Parameters.AddWithValue("Montant", MontantOp.ToString());
                                                cmd.Parameters.AddWithValue("Moyen", "BVR SOS");

                                                cmd.ExecuteNonQuery();

                                                //A la fin si malgrès tout il reste du solde à imputer, on le signale 
                                                if (MontantImputRestant > 0)
                                                {
                                                    //On écrit dans le log une erreur                                                    
                                                    string MessageLog = " Il y a un montant restant à imputer après avoir soldé la facture. Le montant restant est de " + MontantImputRestant + ", le montant réglé est de " + MontantOp.ToString();
                                                    //logmod.EcrireLogTA(DatePaiment + ": Attention...facture TA n° " + NFacture + ".", MessageLog);
                                                    logmod.EcrireLog(DatePaiment + ": Attention...facture TA n° " + NFacture + ".", MessageLog);
                                                    Erreurs = 1;
                                                }

                                                //On avance la jauge
                                                compteur++;

                                                TotalFac = TotalFac + MontantOp;

                                                //On avance la jauge
                                                backgroundWorker1.ReportProgress(compteur);
                                            }   //Fin d'elle n'est pas soldée
                                        }   //Fin d'on a trouvé la facture
                                        else
                                        {
                                            //On regarde si c'est pas une facture SOS
                                            string NFactureSOS = "";

                                            // string LigneNosRefs = detail.RmtInfRef.ToString().TrimEnd(' ');

                                            if (LigneNosRefs.Length == 27 && LigneNosRefs.Substring(6, 1) != "0")
                                                NFactureSOS = LigneNosRefs.Substring(12, 8);   //BVR 27                                                                         
                                            else
                                                NFactureSOS = LigneNosRefs.Substring(10, 8);    //BVR 16                                        

                                            DataRow[] RowFact = OutilsExt.OutilsSql.RecuperationFacturesByNFacture(long.Parse(NFactureSOS.ToString()));

                                            //Si on l'a trouvé
                                            if (RowFact != null && RowFact.Length == 1)
                                            {
                                                //logmod.EcrireLogTA(DatePaiment + ": Erreur... Facture TA n° ", NFactureSOS.ToString() + ", cette facture est une facture SOS médecins!!!");
                                                logmod.EcrireLog(DatePaiment + ": Erreur... Facture TA n° ", NFactureSOS.ToString() + ", cette facture est une facture SOS médecins!!!");
                                                Erreurs = 1;
                                            }
                                            else
                                            {
                                                //logmod.EcrireLogTA(DatePaiment + ": Erreur... Facture TA n° ", NFacture.ToString() + ", cette facture est inconnue, et ce n'est ni une facture TA ni SOS : " + detail.AcctSvcrRef.ToString());
                                                logmod.EcrireLog(DatePaiment + ": Erreur... Facture TA n° ", NFacture.ToString() + ", cette facture est inconnue, et ce n'est ni une facture TA ni SOS : " + detail.AcctSvcrRef.ToString());
                                                Erreurs = 1;
                                            }
                                        }
                                    }  //Fin de facture pas bizare
                                    else
                                    {
                                        //logmod.EcrireLogTA(DatePaiment + ": Erreur... Facture TA n° ", " La facture dont nos refs sont " + LigneNosRefs + ", est étrange...A regarder de plus près....");
                                        logmod.EcrireLog(DatePaiment + ": Erreur... Facture TA n° ", " La facture dont nos refs sont " + LigneNosRefs + ", est étrange...A regarder de plus près....");
                                        Erreurs = 1;
                                    }
                                }
                                else
                                {
                                    //Doublon => message erreur dans log
                                    DatePaiment = Item.DateEcriture.ToString();
                                    logmod.EcrireLogTA(DatePaiment + ": Erreur... FAC n° ", NFacture.ToString() + " est un doublon, " + detail.AcctSvcrRef.ToString() + " " + Moyen + ". Un bulletin de " + Montant + " est en double. Il n'a pas été affectée.");
                                    Erreurs = 1;
                                }

                            }   //Fin TA sur BVR SOS
                            else if ((BVR_CDM == Item.NtryRef.ToString() || QRIBAN_SOS == Item.NtryRef.ToString()) && Item.AcctSvcrRef == "Caisse des Médecins")  //CDM
                            {
                                SOS_OU_TA_OU_CDM = "CDM SOS";
                                Moyen = "5";        //facture_Moyen....ici CDM

                                //Ligne BVR 16 ou 27?                                                                                               
                                string LigneNosRefs = detail.RmtInfRef.ToString().TrimEnd(' ');

                                if (LigneNosRefs.Length == 27 && LigneNosRefs.Substring(6, 1) != "0")
                                {
                                    //BVR 27                                    
                                    NFacture = LigneNosRefs.Substring(12, 8);
                                    NMed = LigneNosRefs.Substring(20, 6);
                                    Montant = detail.Amt.ToString();
                                }
                                else
                                {   //BVR 16
                                    if (int.Parse(LigneNosRefs.Substring(0, 1)) > 6)
                                        NFacture = LigneNosRefs.Substring(0, 6);
                                    else NFacture = LigneNosRefs.Substring(0, 7);

                                    NMed = LigneNosRefs.Substring(11, 4);
                                    Montant = detail.Amt.ToString();
                                }

                                //Ajout au dictionnaire
                                if (!dictionaire.ContainsValue(NFacture))
                                {
                                    dictionaire.Add(i, NFacture);
                                    i++;

                                    //pas de doublon =>Faire le traitement           
                                    DatePaiment = Item.DateValeur.ToString();
                                    DtPaiement1 = DateTime.Parse(DatePaiment);

                                    //On recherche la facture dans la base
                                    DataRow[] rows = OutilsExt.OutilsSql.RecuperationFacturesByNFacture(long.Parse(NFacture.ToString()));
                                    long fac_med = long.Parse(NFacture.ToString());
                                    DataRow[] meds = OutilsExt.OutilsSql.RecuperationMedByNFacture(fac_med);

                                    //Console.WriteLine(NFacture.ToString());  
                                    //On vérifie que c'est bien la bonne avec le bon médecin
                                    if (rows != null && rows.Length == 1)
                                    {
                                        if (long.Parse(meds[0]["CodeIntervenant"].ToString()) == long.Parse(NMed))
                                        {
                                            fac_med = long.Parse(meds[0]["CodeIntervenant"].ToString());

                                            //On regarde si elle n'est pas déjà soldée (solde entre -1 et 1)
                                            if (Decimal.Parse(rows[0]["Solde"].ToString()) > -1 && Decimal.Parse(rows[0]["Solde"].ToString()) < 1)
                                            {
                                                //Facture soldée on met juste un message
                                                //On écrit dans le log
                                                logmod.EcrireLog(DatePaiment + ": facture n° ", NFacture.ToString() + ", a déjà été payée le " + rows[0]["FacDateAcquittee"].ToString() + ". Le montant réglé est de " + Montant + ".");

                                                compteur++;
                                            }
                                            else
                                            {
                                                //On met à jour le montant de l'enregistrement                                               
                                                decimal decMontant = decimal.Parse(Montant);

                                                DataSet ds = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet("select * from facture where NFacture =" + long.Parse(NFacture.ToString()));

                                                //Vérification du solde de la facture
                                                Decimal NvxSolde = Decimal.Parse(ds.Tables[0].Rows[0][13].ToString()) - decMontant;

                                                //Insertion d'une ligne dans facture Etat
                                                string sqlstr0 = "INSERT INTO facture_etats";
                                                sqlstr0 += " (NFacture, Etat, DateEtat, DateOp, CommentaireEtat, Param1, Param2, CodeUtilisateur, Montant, DatePaye, Moyen)";
                                                sqlstr0 += " VALUES(@NFacture, 6, @DtPaiement, @DateOp, @Commentaire, @Param1, @Param2, @User, @Montant, @DtPaiement, @Moyen)";

                                                cmd.CommandText = sqlstr0;

                                                cmd.Parameters.Clear();
                                                cmd.Parameters.AddWithValue("NFacture", NFacture.ToString());
                                                cmd.Parameters.AddWithValue("DateEtat", DtPaiement1.ToString());
                                                cmd.Parameters.AddWithValue("DateOp", DateTime.Now.ToString("dd.MM.yyyy 00:00:00"));
                                                cmd.Parameters.AddWithValue("Commentaire", txtPayeCommentaire);
                                                cmd.Parameters.AddWithValue("Param1", "");
                                                cmd.Parameters.AddWithValue("Param2", "");
                                                cmd.Parameters.AddWithValue("User", SosMedecins.SmartRapport.GestionApplication.VariablesApplicatives.Utilisateurs.Identifiant);

                                                if (NvxSolde >= 0)
                                                    cmd.Parameters.AddWithValue("Montant", decMontant.ToString());
                                                else if (NvxSolde < 0 && NvxSolde > -1)    //Si solde négatif                                                                                                   
                                                    cmd.Parameters.AddWithValue("Montant", decMontant.ToString());
                                                else if (NvxSolde <= -1)
                                                    cmd.Parameters.AddWithValue("Montant", ds.Tables[0].Rows[0][13].ToString());

                                                // cmd.Parameters.AddWithValue("Montant", decMontant.ToString());
                                                cmd.Parameters.AddWithValue("DtPaiement", DtPaiement1.ToString());
                                                cmd.Parameters.AddWithValue("Moyen", Moyen);

                                                cmd.ExecuteNonQuery();

                                                //if (NvxSolde > -1 && NvxSolde < 1)    //On considère que la facture est soldée
                                                if (NvxSolde < 1)    //On considère que la facture est soldée
                                                {
                                                    //Mise à jour de Facture Status                                                                               
                                                    string sqlstr1 = "UPDATE facture_status";
                                                    sqlstr1 += " SET FacDateAcquittee = @DtPaiement";
                                                    sqlstr1 += " WHERE NFacture = @NumFacture";

                                                    cmd.CommandText = sqlstr1;

                                                    cmd.Parameters.Clear();
                                                    cmd.Parameters.AddWithValue("NumFacture", NFacture.ToString());
                                                    cmd.Parameters.AddWithValue("DtPaiement", DtPaiement1.ToString());

                                                    cmd.ExecuteNonQuery();

                                                    //Mise à jour du Solde = 0 de la facture                                       
                                                    string sqlstr2 = "UPDATE facture";
                                                    sqlstr2 += " SET solde = 0";
                                                    sqlstr2 += " WHERE NFacture = @NumFacture";

                                                    cmd.CommandText = sqlstr2;

                                                    cmd.Parameters.Clear();
                                                    cmd.Parameters.AddWithValue("NumFacture", NFacture.ToString());

                                                    cmd.ExecuteNonQuery();
                                                }
                                                else
                                                {
                                                    //Sinon pas soldée mise à jour du Solde de la facture                                        
                                                    string sqlstr2 = "UPDATE facture";
                                                    sqlstr2 += " SET solde = solde - @Montant";
                                                    sqlstr2 += " WHERE NFacture = @NumFacture";

                                                    cmd.CommandText = sqlstr2;

                                                    cmd.Parameters.Clear();
                                                    cmd.Parameters.AddWithValue("NumFacture", NFacture.ToString());
                                                    cmd.Parameters.AddWithValue("Montant", decMontant.ToString());

                                                    cmd.ExecuteNonQuery();

                                                    //On écrit dans le log
                                                    //logmod.EcrireLog(DatePaiment + ": facture n° ", NFacture.ToString() + ", Le montant initial de la facture est " + ds.Tables[0].Rows[0][12].ToString() + ", Montant payé est " + Montant + ", le solde est maintenant de " + NvxSolde + "." );
                                                }

                                                //Pour les logs: Solde négatif?
                                                if (NvxSolde < -1)
                                                {
                                                    logmod.EcrireLog(DatePaiment + ": Erreur.... facture n° ", NFacture.ToString() + ", trop perçu. Rembourser " + NvxSolde + ".");
                                                    Erreurs = 1;
                                                }

                                                compteur++;

                                                TotalFac = TotalFac + decimal.Parse(Montant);

                                                //On avance la jauge
                                                backgroundWorker1.ReportProgress(compteur);
                                            }
                                        }
                                        else
                                        {
                                            logmod.EcrireLog(DatePaiment + ": Erreur... facture n° ", NFacture.ToString() + ", erreur sur la ligne : " + detail.AcctSvcrRef.ToString() + " " + Moyen + ", le médecin ne correspond pas. Vérifiez la provenance du fichier (CDM etc...)");
                                            Erreurs = 1;
                                        }
                                    }
                                    else
                                    {
                                        logmod.EcrireLog(DatePaiment + ": Erreur... facture n° ", NFacture.ToString() + ", erreur sur ligne : " + detail.AcctSvcrRef.ToString() + " " + Moyen + ", la facture n'a pas été trouvée.");
                                        Erreurs = 1;
                                    }

                                }
                                else
                                {
                                    //Doublon => message erreur dans log
                                    logmod.EcrireLog(DatePaiment + ": Erreur... facture n° ", NFacture.ToString() + " est un doublon : " + detail.AcctSvcrRef.ToString() + " " + Moyen + ", un bulletin de " + Montant + " est en double. Il n'a pas été affectée.");
                                    Erreurs = 1;
                                }
                            }       //Fin CDM

                        }   //Fin de foreach ListeD
                    }   //Fin de foreach ListeC

                    //On commit la transaction
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    logmod.EcrireLog("Erreur " + ex, NFacture.ToString());
                    Erreurs = 1;

                    //On essaie de faire un Rollback
                    try
                    {
                        transaction.Rollback();      //On fait un rollback
                    }
                    catch (Exception ex2)
                    {
                        //On gère ici toute les erreurs qui ont pu survenir pour empêcher le Rollback...
                        //comme par exemple une connexion fermée...
                        Console.WriteLine("Rollback Exeption Type: {0}", ex2.GetType());
                        Console.WriteLine("   Message: {0}", ex2.Message);
                    }
                }
                finally
                {
                    if (dbConnection.State == System.Data.ConnectionState.Open)
                        dbConnection.Close();
                }

                //recup du NbTotalFactures traitées
                NbTotalFactures = compteur.ToString();

                //On affiche le total une fois le traitement terminé (Utiliser un Invoke pour modifier un objet crée depuis un autre thread)
                this.Invoke((MethodInvoker)delegate
                {
                    label1.Visible = true;
                    label1.Text = "Traitement Terminé des factures : " + SOS_OU_TA_OU_CDM + ", \r\n" + NbTotalFacturesXML.ToString() + " factures dans le fichier XML pour un montant total de " + TotalFacXML.ToString() + "\r\n" + NbTotalFactures + " factures, trouvées dans la base, ont été traitées pour un montant total de " + TotalFac + " CHF ";

                    if (TotalFacXML < TotalFac)
                        label1.Text += "\r\n Remarque: Le montant total traité est supérieur au montant du fichier XMl car des factures n'ont pas été intégralement payées.";

                    DialogResult result1;

                    if (Erreurs == 1)
                        result1 = MessageBox.Show("Il existe des erreurs. Voulez vous afficher le log?", "Erreur affichage du log", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    else
                        result1 = MessageBox.Show("Il n'y a pas d'erreur. MAIS....Voulez vous quand même afficher le log?", "Affichage du log", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result1 == DialogResult.Yes)
                    {
                        //On affiche le fichier des logs
                        AfficheLog();
                    }
                });

            }   //#################################Fin de traitement XML
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //Change la valeur de la ProgressBar
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private int indexPremierChiffreFacture(string Reference)
        {
            //on boucle pour compter les 0 devant
            int i = 0;
            int PremierChiffreFacture = 0;

            while (i < Reference.Length)
            {
                if (Reference[i].ToString() != "0")
                {
                    PremierChiffreFacture = i;
                    i = Reference.Length;
                }
                else
                {
                    i++;
                }
            }

            return PremierChiffreFacture;
        }

    }
}


//A faire:
//En cas de problème (double payent dont 1 sur facture déjà aquitée), reactiver la ligne 769 (console.writeline(nfacture)) pour localisé l'une des facture
//....affectée 2 fois.....celle avant le plantage (payer en manuel cette facture et repasser le fichier)

//Revoir tout ça: tester => lengt 27?  Oui => 6,1 = 1(SOS), 2(TA Abon), 3 (TA Mat), 0(Medicentre?)
//Si c'est 16, on change rien
