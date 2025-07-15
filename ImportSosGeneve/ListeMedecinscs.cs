using System;
using System.Data;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Globalization;
using SosMedecins.SmartRapport.DAL;
using System.Configuration;
using System.Data.SqlClient;
using SosMedecins.SmartRapport.EtatsCrystal;
using SosMedecins.SmartRapport.GestionApplication;
using System.Collections.Generic;

namespace ImportSosGeneve
{

    public partial class ListeMedecinscs : Form
    {

        private string[][] m_ListeMedecins = null;
        public SalaireMedecin DSSalaireMedecin = new SalaireMedecin();
        public DataSet DSResult = new DataSet();
        Dictionary<int, string[]> subItemsDictionary = new Dictionary<int, string[]>();

        public ListeMedecinscs()
        {
            InitializeComponent();

            //On initialise les dates            
            //Factures encaissées/Stats
            dtDebut.Value = DateTime.Parse("01." + DateTime.Now.AddMonths(-1).Month.ToString() + "." + DateTime.Now.Year.ToString());
            dtFin.Value = DateTime.Parse("01." + DateTime.Now.Month.ToString() + "." + DateTime.Now.Year.ToString());

            //etats debiteurs
            dtdebDepuis.Value = DateTime.Parse("01.01." + DateTime.Now.Year.ToString()).AddYears(-5);
            dtdebFin.Value = DateTime.Parse("01." + DateTime.Now.Month.ToString() + "." + DateTime.Now.Year.ToString()).AddDays(-1);
            dtSoldeAu.Value = DateTime.Parse("01." + DateTime.Now.Month.ToString() + "." + DateTime.Now.Year.ToString()).AddDays(-1);
        }
        protected void InitializeListeMedecins()
        {
            clbmedecins.Items.Clear();
        }

        private void btChargerMedecins_Click(object sender, EventArgs e)
        {
            ChargerMedecinsEncaiss();
        }

        //Envoi des états débiteurs par email
        private void buttonEtatDebiteur_Click(object sender, EventArgs e)
        {
            progressBar1.Maximum = clbmedecins.CheckedIndices.Count;

            DateTime DateDebDepuis = DateTime.Parse(dtdebDepuis.Text.ToString());
            DateTime DateDebFin = DateTime.Parse(dtdebFin.Text.ToString());
            DateTime DateSoldeAu = DateTime.Parse(dtSoldeAu.Text.ToString());

            string jusque = DateSoldeAu.ToShortDateString();
            string FacEnvoye_de = DateDebDepuis.ToShortDateString();
            string FacEnvoye_Au = DateDebFin.ToShortDateString();
            int i = 0;
            Regex modèle = new Regex(@"\d{2}.\d{2}.\d{4}");
            MatchCollection résultat_dbt = modèle.Matches(jusque);
            MatchCollection résultat_DAP1 = modèle.Matches(FacEnvoye_de);
            MatchCollection résultat_DAP2 = modèle.Matches(FacEnvoye_Au);
            jusque = résultat_dbt[i].Value;
            FacEnvoye_de = résultat_DAP1[i].Value;
            FacEnvoye_Au = résultat_DAP2[i].Value;

            string mois = "";
            switch (DateSoldeAu.Month)
            {
                case 1:
                    mois = "Janvier";
                    break;
                case 2:
                    mois = "Février";
                    break;
                case 3:
                    mois = "Mars";
                    break;
                case 4:
                    mois = "Avril";
                    break;
                case 5:
                    mois = "Mai";
                    break;
                case 6:
                    mois = "Juin";
                    break;
                case 7:
                    mois = "Juillet";
                    break;
                case 8:
                    mois = "Août";
                    break;
                case 9:
                    mois = "Septembre";
                    break;
                case 10:
                    mois = "Octobre";
                    break;
                case 11:
                    mois = "Novembre";
                    break;
                case 12:
                    mois = "Décembre";
                    break;
            }

            string year = DateSoldeAu.Year.ToString();

            string ordre = "";

            switch (cbTri.Text)
            {
                case "Date de consultation croissante": ordre = "FacDateEnvoyee"; break;
                case "Date de consultation décroissante": ordre = "FacDateEnvoyee DESC"; break;
                case "Nom patient": ordre = "NomPatient"; break;
                case "N° de facture": ordre = "f.NFacture"; break;
                case "Assurance": ordre = "f.AdresseDestinataire"; break;
                default: ordre = "FacDateEnvoyee"; break;
            }

            //Pour medecin 
            int tag = 0;
            string nom = "";

            this.Refresh();
            try
            {
                Cursor = Cursors.WaitCursor;
                buttonEtatDebiteur.Enabled = false;
                buttonFacEcpMed.Enabled = false;
                buttonPrestationMed.Enabled = false;

                for (int k = 0; k < clbmedecins.CheckedIndices.Count; k++)
                {
                    progressBar1.Value = k + 1;
                    nom = " AND tm.Nom = '" + clbmedecins.CheckedItems[k].ToString() + "'";

                    //sql debiteur
                    Donnees.MesFactures = new dstFacturesEncMed();
                    
                    //Etats débiteurs
                    //************************Etats débiteurs sans les medicaments/matériel
                    string sql = " ; WITH TotalPrix AS (SELECT NFacture, SUM(Prix) AS TotalFacture";
                    sql += "                            FROM facture_prest";
                    sql += "                            WHERE TypePrest <> 2";
                    sql += "                            GROUP BY NFacture)";
                    sql += " SELECT tm.Nom AS NomMED, tm.Mail, f.DateCreation AS FacDateEnvoyee, f.NFacture, tp.TotalFacture, (pe.Nom + ' ' + pe.Prenom) AS NomPatient, f.AdresseDestinataire, pe.Tel";
                    sql += " FROM facture f INNER JOIN facture_etats fe ON fe.NFacture = f.NFacture";
                    sql += " INNER JOIN factureConsultation fc ON fc.NFacture = f.NFacture";
                    sql += " INNER JOIN facture_status fs ON fs.NFacture = f.NFacture";
                    sql += " INNER JOIN TotalPrix tp ON tp.NFacture = f.NFacture";
                    sql += " INNER JOIN tableconsultations tc ON tc.NConsultation = fc.NConsultation";
                    sql += " INNER JOIN tableactes ta ON ta.Num = tc.CodeAppel";
                    sql += " INNER JOIN tablemedecin tm ON tm.CodeIntervenant = ta.CodeIntervenant";
                    sql += " INNER JOIN tablepatient pa ON pa.IdPatient = tc.IndicePatient";
                    sql += " INNER JOIN tablepersonne pe ON pe.IdPersonne = pa.IdPersonne";
                    sql += " WHERE fs.FacDateAnnulee IS NULL";
                    sql += " AND f.TotalFacture > 0";
                    sql += " and  (fs.FacDateAcquittee >'" + jusque + "'  or fs.FacDateAcquittee is null)";
                    sql += " and fe.DatePaye <= '" + jusque + "' ";
                    sql += " and f.DateCreation >= '" + FacEnvoye_de + "' and f.DateCreation <= '" + FacEnvoye_Au + "' ";
                    sql += nom;
                    //Blocage Helsana/Progres 16.05.2018 au 10.07.2019 inclus (pour enlever l'affichage des soldes négatifs...les médecins comprennent rien!!!)
                    sql += " and f.NFacture not in (Select fa.NFacture FROM facture fa inner join factureConsultation fac ON fac.NFacture = fa.NFacture";
                    sql += "                        inner join tableconsultations tac ON tac.NConsultation = fac.NConsultation ";
                    sql += "                        inner join tableactes taa ON tac.CodeAppel = taa.Num ";
                    sql += "                        where fa.DateCreation >= '16.05.2018' and fa.DateCreation <= '10.07.2019' and fa.CodeDestinataire in (130,194))";
                    //************************************************Fin blocage*****************************************
                    //Blocage avance Groupe Mutuel Le 09.07.2024 pour avance salaire
                    sql += " and f.NFacture not in (Select fb.NFacture FROM facture fb inner join factureConsultation fac ON fac.NFacture = fb.NFacture";
                    sql += "                        inner join tableconsultations tac ON tac.NConsultation = fac.NConsultation ";
                    sql += "                        inner join tableactes taa ON tac.CodeAppel = taa.Num ";          
                    sql += "                        where fb.DateCreation >= '2024-01-01' and fb.DateCreation <= '31.05.2024' and fb.CodeDestinataire in (59))";
                    sql += " GROUP BY tm.Nom, tm.Mail, f.NFacture, f.AdresseDestinataire, pe.Tel, pe.Nom, pe.Prenom, f.DateCreation, tp.TotalFacture";
                    sql += " ORDER BY tm.Nom, " + ordre.ToString();

                    OutilsExt.OutilsSql.RemplitDataTable(Donnees.MesFactures.Tables[0], sql);

                    if (Donnees.MesFactures.Tables.Count > 0 && Donnees.MesFactures.Tables[0].Rows.Count > 0)
                    {
                        // Envoie des encaissement medecins par mail :
                        string m_strDestinataire = "";
                        m_strDestinataire = Donnees.MesFactures.FacturesEncMed[0].Mail;
                        string FileName = "Etat Débiteurs - " + Donnees.MesFactures.FacturesEncMed[0].NomMED + "au" + jusque.ToString() + "appels facturees du" + FacEnvoye_de.ToString() + "au" + FacEnvoye_Au.ToString() + " " + DateTime.Now.Millisecond.ToString();

                        Donnees.MesFactures.FacturesEncMed.DefaultView.Sort = "NFacture";

                        Donnees.EtatDebiteursMedecins = new EtatDebiteursTrie();
                        Donnees.EtatDebiteursMedecins.SetDataSource(Donnees.MesFactures);

                        Donnees.EtatDebiteursMedecins.SummaryInfo.ReportTitle = "Etat Débiteurs au " + jusque + "\nAppels facturés du " + FacEnvoye_de + " au " + FacEnvoye_Au;

                        CrystalUtility.ExportReport(Donnees.EtatDebiteursMedecins, "pdf", Application.StartupPath + SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.Cache, FileName);
                        SosMedecins.Utilitaires.Mail objMail = new SosMedecins.Utilitaires.Mail(VariablesApplicatives.Utilisateurs.EMail);

                        objMail.Sujet = "Etat Débiteurs - " + Donnees.MesFactures.FacturesEncMed[0].NomMED + " au " + jusque.ToString() + " Appels factures du " + FacEnvoye_de.ToString() + " au " + FacEnvoye_Au.ToString();
                        objMail.Message = "";
                        objMail.JoindrePiece(Application.StartupPath + SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.Cache + FileName + ".pdf");
                        //objMail.AjouteDestinataire("dmercier@sos-medecins.ch");
                        objMail.AjouteDestinataire(m_strDestinataire);
                        objMail.AjouteDestinataireCc("retributions@sos-medecins.ch");

                        if (objMail.Envoi())
                        {
                            tag++;
                            if (Donnees.EtatDebiteursMedecins != null)
                            {
                                Donnees.EtatDebiteursMedecins.Dispose();      //On oubli pas de détruire l'etat apres l'utilisation du rapport, ou Crystal va planter au bout de 75
                                Donnees.EtatDebiteursMedecins.Close();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Message Non envoyé" + m_strDestinataire);
                            if (Donnees.EtatDebiteursMedecins != null)
                            {
                                Donnees.EtatDebiteursMedecins.Dispose();      //On oubli pas de détruire l'etat apres l'utilisation du rapport, ou Crystal va planter au bout de 75
                                Donnees.EtatDebiteursMedecins.Close();
                            }
                        }

                        objMail = null;
                    }
                }

                MessageBox.Show("Traitement terminé avec envoie de " + tag + " Mail");
                this.Cursor = Cursors.Default;

                this.buttonEtatDebiteur.Enabled = true;
                this.buttonFacEcpMed.Enabled = true;
                this.buttonPrestationMed.Enabled = true;
            }

            catch (Exception ex)
            {
                Console.WriteLine("{0} Exception caught.", ex);
                MessageBox.Show(ex.ToString());
            }
        }


        private void buttonAllselec_Click(object sender, EventArgs e)
        {
            if (buttonAllselec.Text == "Tout sélectioner")
            {
                // Parcours de tous les éléments de la CheckedListBox
                for (int i = 0; i < clbmedecins.Items.Count; i++)
                {
                    // Récupération de l'élément courant
                    var item = clbmedecins.Items[i];

                    // Recherche de l'index de cet élément dans la CheckedListBox
                    int y = clbmedecins.Items.IndexOf(item);

                    // Si l'élément est trouvé, cochez-le
                    if (y != -1)
                    {
                        clbmedecins.SetItemCheckState(y, CheckState.Checked);
                    }
                }
                buttonAllselec.Text = "Désélectionner";
            }
            else
            {
                // Parcours de tous les éléments de la CheckedListBox
                for (int i = 0; i < clbmedecins.Items.Count; i++)
                {
                    // Récupération de l'élément courant
                    var item = clbmedecins.Items[i];

                    // Recherche de l'index de cet élément dans la CheckedListBox
                    int y = clbmedecins.Items.IndexOf(item);

                    // Si l'élément est trouvé, on le décoche
                    if (y != -1)
                    {
                        clbmedecins.SetItemCheckState(y, CheckState.Unchecked);
                    }
                }
                buttonAllselec.Text = "Tout sélectioner";
            }

            //On remet la progressbar à 0
            progressBar1.Value = 0;

        }

        private void ButtonFacEcPMed_Click(object sender, EventArgs e)
        {
            /*this.progressBar1.Maximum = clbmedecins.CheckedIndices.Count;
            
            DateTime DtFin = DateTime.Parse(dtFin.Text.ToString());
            string DateFin = DtFin.ToString("yyyy.MM.dd");
            
            Refresh();
            int tag = 0;
            
            try
            {
                Cursor = Cursors.WaitCursor;  //curseur attente

                buttonPrestationMed.Enabled = false;
                buttonEtatDebiteur.Enabled = false;
                buttonFacEcpMed.Enabled = false;
                buttonPrestationMed.Enabled = false;

                for (int k = 0; k < clbmedecins.CheckedIndices.Count; k++)
                {
                    progressBar1.Value = k + 1;
                                       
                    Facture_Encaissement_MedecinsMail_V3 z_rpt = new Facture_Encaissement_MedecinsMail_V3();
                    Fonctions.ChangeConnection(z_rpt, Variables.ConnexionBase);
                    frmCrystalReportViewer z_frm = new frmCrystalReportViewer();
                    

                    z_frm.ReportSource = z_rpt;
                    
                    string[][] CODEINTERV  = OutilsExt.OutilsSql.CodeIntervenant( clbmedecins.CheckedItems[k].ToString());
                  
                    z_rpt.SetParameterValue("DateDebut", DateTime.Parse(dtDebut.Text.ToString()));
                    z_rpt.SetParameterValue("DateFin", DateTime.Parse(dtFin.Text.ToString()));
                    z_rpt.SetParameterValue("CodeIntervenant", CODEINTERV[0][0].ToString());
                   
                    string m_strDestinataire = "";
                    m_strDestinataire = CODEINTERV[0][1];
                    string FileName = "Factures encaissées " + clbmedecins.CheckedItems[k].ToString() + "-" + DateFin.Replace(".", "-").ToString() + " " + DateTime.Now.Millisecond.ToString();
                    CrystalUtility.ExportReport(z_rpt, "pdf", Application.StartupPath + SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.Cache, FileName);
                    SosMedecins.Utilitaires.Mail objMail = new SosMedecins.Utilitaires.Mail(VariablesApplicatives.Utilisateurs.EMail);
                    objMail.Sujet = "Envoi encaissements medecin - " + clbmedecins.CheckedItems[k].ToString() + "-" + DateFin.ToString();
                    objMail.Message = "";
                    objMail.JoindrePiece(Application.StartupPath + SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.Cache + FileName + ".pdf");
                    //objMail.AjouteDestinataire("dmercier@sos-medecins.ch");
                    objMail.AjouteDestinataire(m_strDestinataire);                  
                    objMail.AjouteDestinataireCc("retributions@sos-medecins.ch");

                    if (objMail.Envoi())
                    {
                        tag++;
                        if (z_rpt != null)
                        {
                            z_rpt.Dispose();      //On oubli pas de détruire l'etat apres l'utilisation du rapport, ou Crystal va planter au bout de 75
                            z_rpt.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Message Non envoyé" + m_strDestinataire);
                        if (z_rpt != null)
                        {
                            z_rpt.Dispose();      //On oubli pas de détruire l'etat apres l'utilisation du rapport, ou Crystal va planter au bout de 75
                            z_rpt.Close();
                        }
                    }

                    objMail = null;
                   
                }

                MessageBox.Show("Traitement terminé avec envoie de " + tag + " Mail");
                
                Cursor = Cursors.Default;
                buttonEtatDebiteur.Enabled = true;
                buttonFacEcpMed.Enabled = true;
                buttonPrestationMed.Enabled = true;

            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} Exception caught.", ex);
                MessageBox.Show(ex.ToString());
            }*/
        }

        //Envoi par email des statistiques sur la qté et le type de prestations facturées pour la période
        private void ButtonPrestationMed_Click(object sender, EventArgs e)
        {
            DateTime DtDebut = DateTime.Parse(dtDebut.Text.ToString());
            DateTime DtFin = DateTime.Parse(dtFin.Text.ToString());
            string fin2 = DtFin.ToString("yyyy.MM.dd");
            progressBar1.Maximum = clbmedecins.CheckedIndices.Count;

            //set month name and year
            string mois = "";
            switch (DtDebut.Month)
            {
                case 1:
                    mois = "Janvier";
                    break;
                case 2:
                    mois = "Février";
                    break;
                case 3:
                    mois = "Mars";
                    break;
                case 4:
                    mois = "Avril";
                    break;
                case 5:
                    mois = "Mai";
                    break;
                case 6:
                    mois = "Juin";
                    break;
                case 7:
                    mois = "Juillet";
                    break;
                case 8:
                    mois = "Août";
                    break;
                case 9:
                    mois = "Septembre";
                    break;
                case 10:
                    mois = "Octobre";
                    break;
                case 11:
                    mois = "Novembre";
                    break;
                case 12:
                    mois = "Décembre";
                    break;
            }

            int tag = 0;

            this.Refresh();

            //Factures encaissées par médecin                      
            /*string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);      //Chaine de connection récupérée dans le app.config   

            //on ouvre la connexion
            dbConnection.Open();

            SqlCommand cmd = dbConnection.CreateCommand();
            cmd.Connection = dbConnection;  */     //On passe les parametres query et connection
            try
            {
                Cursor = Cursors.WaitCursor;
                buttonEtatDebiteur.Enabled = false;
                buttonFacEcpMed.Enabled = false;
                buttonPrestationMed.Enabled = false;

                for (int k = 0; k < clbmedecins.CheckedIndices.Count; k++)
                {
                    progressBar1.Value = k + 1;
                    string year = DtDebut.Year.ToString();                    

                    string sqlstr0 = " SELECT fp.TypeTarif, CASE fp.TypePrest";
                    sqlstr0 += "                                WHEN 1 THEN 'Tarmed'";
                    sqlstr0 += "                                WHEN 2 THEN 'Pharmacie'";
                    sqlstr0 += "                            END AS Type_Prestation,";
                    sqlstr0 += "                            CASE fp.TypePrest";
                    sqlstr0 += "                                WHEN 1 THEN t.NPrestation";
                    sqlstr0 += "                                WHEN 2 THEN 'Pharmacie'";
                    sqlstr0 += "                            END AS NPrestation,";
                    sqlstr0 += " t.PrestLibelle, Sum(fp.Qte) AS SumQua, Sum(fp.Prix) AS SumPrix, tm.CodeIntervenant,tm.Mail, ft.libelle AS Tarif";
                    sqlstr0 += " FROM factureconsultation fc INNER JOIN tableconsultations tc ON fc.NConsultation = tc.NConsultation";
                    sqlstr0 += "                             INNER JOIN tableactes ta ON tc.CodeAppel = ta.Num";
                    sqlstr0 += "                             INNER JOIN tablemedecin tm ON ta.CodeIntervenant = tm.CodeIntervenant";
                    sqlstr0 += "                             INNER JOIN facture f ON fc.NFacture = f.NFacture";
                    sqlstr0 += "                             INNER JOIN facture_prest fp ON fp.NFacture = f.NFacture";
                    sqlstr0 += "                             LEFT JOIN Tarmed t ON t.NPrestation = fp.Indice";
                    sqlstr0 += "                             INNER JOIN fac_tarif ft ON ft.id = fp.TypeTarif";
                    sqlstr0 += " WHERE f.DateCreation >= '" + DtDebut + "' AND f.DateCreation <= '" + DtFin + "'";
                    sqlstr0 += " AND tm.Nom = '" + clbmedecins.CheckedItems[k].ToString() + "'";
                    sqlstr0 += " AND fp.TypeTarif <> 5";
                    sqlstr0 += " AND(t.TarmedVersion is null OR t.TarmedVersion = 'LAMAL')";
                    sqlstr0 += " GROUP BY tm.Mail, fp.TypePrest, fp.TypeTarif, t.NPrestation, t.PrestLibelle, ft.libelle, tm.CodeIntervenant";
                    sqlstr0 += " UNION";
                    sqlstr0 += " SELECT fp.TypeTarif, CASE fp.TypePrest";
                    sqlstr0 += "                            WHEN 1 THEN 'Tarmed'";
                    sqlstr0 += "                            WHEN 2 THEN 'Pharmacie'";
                    sqlstr0 += "                            END AS Type_Prestation,";
                    sqlstr0 += "                      CASE fp.TypePrest";
                    sqlstr0 += "                            WHEN 1 THEN t.NPrestation";
                    sqlstr0 += "                            WHEN 2 THEN 'Pharmacie'";
                    sqlstr0 += "                      END AS NPrestation,";
                    sqlstr0 += " t.PrestLibelle, Sum(fp.Qte) AS SumQua, Sum(fp.Prix) AS SumPrix, tm.CodeIntervenant,tm.Mail, ft.libelle AS Tarif";
                    sqlstr0 += " FROM factureconsultation fc INNER JOIN tableconsultations tc ON fc.NConsultation = tc.NConsultation";
                    sqlstr0 += "                             INNER JOIN tableactes ta ON tc.CodeAppel = ta.Num";
                    sqlstr0 += "                             INNER JOIN tablemedecin tm ON ta.CodeIntervenant = tm.CodeIntervenant";
                    sqlstr0 += "                             INNER JOIN facture f ON fc.NFacture = f.NFacture";
                    sqlstr0 += "                             INNER JOIN facture_prest fp ON fp.NFacture = f.NFacture";
                    sqlstr0 += "                             LEFT JOIN Tarmed t ON t.NPrestation = fp.Indice";
                    sqlstr0 += "                             INNER JOIN fac_tarif ft ON ft.id = fp.TypeTarif";
                    sqlstr0 += " WHERE f.DateCreation >= '" + DtDebut + "' AND f.DateCreation <= '" + DtFin + "'";
                    sqlstr0 += " AND tm.Nom = '" + clbmedecins.CheckedItems[k].ToString() + "'";
                    sqlstr0 += " AND fp.TypeTarif = 5";
                    sqlstr0 += " AND(t.TarmedVersion is null OR t.TarmedVersion = 'LAA-AM-AI')";
                    sqlstr0 += " GROUP BY tm.Mail, fp.TypePrest, fp.TypeTarif, t.NPrestation, t.PrestLibelle, ft.libelle, tm.CodeIntervenant";

                    Donnees.MesPres = new dstPrestations();
                  
                    DataSet dt1 = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet(sqlstr0);
                    int cont = dt1.Tables[0].Rows.Count;

                    //enter the info to the data set and send the information to report
                    OutilsExt.OutilsSql.RemplitDataTable(Donnees.MesPres.Tables[0], sqlstr0);

                    int count = Donnees.MesPres.Tables[0].Rows.Count;
                    if (Donnees.MesPres.Tables.Count > 0 && Donnees.MesPres.Tables[0].Rows.Count > 0)
                    {
                        // string m_strDestinataire = Donnees.MesPres.FacPres[0].Mail;
                        frmPrestations imprPrestation = new frmPrestations(Donnees.MesPres, mois, year);

                        string m_strDestinataire = Donnees.MesPres.FacPres[0].Mail;
                        string FileName = "Prestations_facturees_" + clbmedecins.CheckedItems[k].ToString() + "-" + fin2.ToString() + " " + DateTime.Now.Millisecond.ToString();
                        Donnees.Etatprestation = new EtatPrestation();
                        Donnees.Etatprestation.SetDataSource(Donnees.MesPres);
                        TextInfo FormatNom = new CultureInfo("fr-FR", false).TextInfo;     //Pour mettre les 1ères lettres en Majuscules
                        Donnees.Etatprestation.SummaryInfo.ReportTitle = "Prestations facturées au mois de " + mois + " " + year + " pour " + FormatNom.ToTitleCase(clbmedecins.CheckedItems[k].ToString().ToLower());

                        CrystalUtility.ExportReport(Donnees.Etatprestation, "pdf", Application.StartupPath + SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.Cache, FileName);
                        SosMedecins.Utilitaires.Mail objMail = new SosMedecins.Utilitaires.Mail(VariablesApplicatives.Utilisateurs.EMail);
                        objMail.Sujet = "Prestations facturées au mois de " + mois + " " + year + " pour " + FormatNom.ToTitleCase(clbmedecins.CheckedItems[k].ToString().ToLower());

                        objMail.Message = "";
                        objMail.JoindrePiece(Application.StartupPath + SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.Cache + FileName + ".pdf");
                        //objMail.AjouteDestinataire("dmercier@sos-medecins.ch");
                        objMail.AjouteDestinataire(m_strDestinataire);
                        objMail.AjouteDestinataireCc("retributions@sos-medecins.ch");

                        if (objMail.Envoi())
                        {
                            tag++;
                            if (Donnees.Etatprestation != null)
                            {
                                Donnees.Etatprestation.Dispose();      //On oubli pas de détruire l'etat apres l'utilisation du rapport, ou Crystal va planter au bout de 75
                                Donnees.Etatprestation.Close();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Message Non envoyé" + m_strDestinataire);
                            if (Donnees.Etatprestation != null)
                            {
                                Donnees.Etatprestation.Dispose();      //On oubli pas de détruire l'etat apres l'utilisation du rapport, ou Crystal va planter au bout de 75
                                Donnees.Etatprestation.Close();
                            }
                        }
                        objMail = null;

                    }
                }

                MessageBox.Show("Traitement terminé avec envoie de " + tag + " Mail");
                Cursor = Cursors.Default;
                buttonEtatDebiteur.Enabled = true;
                buttonFacEcpMed.Enabled = true;
                buttonPrestationMed.Enabled = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} erreur: ", ex);
                MessageBox.Show(ex.ToString());
            }
        }


        private void clbmedecins_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //Pour compter le nombre de cases cochées
            int nbCoche = clbmedecins.CheckedItems.Count;
            if (e.NewValue == CheckState.Checked)
                ++nbCoche;
            if (e.NewValue == CheckState.Unchecked)
                --nbCoche;

            //Puis on met à jour le label
            lcompteur.Text = nbCoche.ToString();
        }

        //Salaires Médecins
        private void bSalaireMed_Click(object sender, EventArgs e)
        {
            ImprimeReleve(DSSalaireMedecin);
        }

        //Envoi par email des factures encaissées par médecins
        private void ImprimeReleve(SalaireMedecin DSsalaireMed)
        {
            this.Cursor = Cursors.WaitCursor;

            progressBar1.Maximum = clbmedecins.CheckedIndices.Count;

            DateTime DtFin = DateTime.Parse(dtFin.Text.ToString());
            string DateFin = DtFin.ToString("yyyy.MM.dd");

            Refresh();
        
            try
            {
                Cursor = Cursors.WaitCursor;  //curseur attente

                bSalaireMed.Enabled = false;
                buttonPrestationMed.Enabled = false;
                buttonEtatDebiteur.Enabled = false;
                buttonFacEcpMed.Enabled = false;
                buttonPrestationMed.Enabled = false;

                int k = 1;
                int tag = 0;

                foreach (int index in clbmedecins.CheckedIndices)
                {
                    //On effectue la requete de recherche                                       
                    ListItem selectedItem = (ListItem)clbmedecins.Items[index];
                    int codeIntervenant = int.Parse(selectedItem.objValue.ToString());
                    try
                    {
                        SelectionneParMedecin(codeIntervenant);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("SelectionneParMedecin(): " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    progressBar1.Value = k++;

                    Facture_Encaissement_MedecinsMail_V4 z_rpt = new Facture_Encaissement_MedecinsMail_V4();
                    Fonctions.ChangeConnection(z_rpt, Variables.ConnexionBase);
                    frmCrystalReportViewer z_frm = new frmCrystalReportViewer();
                    z_rpt.SetDataSource(DSsalaireMed);  //source de donnée du rapport

                    z_frm.ReportSource = z_rpt;    //On attache le rpt à la form du rapport

                    //int CodeMedecin = int.Parse(subItemsDictionary[index][1].ToString());                    
                    string[][] CODEINTERV = OutilsExt.OutilsSql.MailMedecinCodeIntervenant(codeIntervenant);

                    z_rpt.SetParameterValue("DateDebut", DateTime.Parse(dtDebut.Text.ToString()));
                    z_rpt.SetParameterValue("DateFin", DateTime.Parse(dtFin.Text.ToString()));
                    z_rpt.SetParameterValue("CodeIntervenant", codeIntervenant);

                    string m_strDestinataire = "";
                    m_strDestinataire = CODEINTERV[0][1];

                    string FileName = "Factures encaissées " + clbmedecins.Items[index].ToString() + "-" + DateFin.Replace(".", "-").ToString() + " " + DateTime.Now.Millisecond.ToString();
                    CrystalUtility.ExportReport(z_rpt, "pdf", Application.StartupPath + SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.Cache, FileName);
                    SosMedecins.Utilitaires.Mail objMail = new SosMedecins.Utilitaires.Mail(VariablesApplicatives.Utilisateurs.EMail);
                    objMail.Sujet = "Envoi encaissements medecin - " + clbmedecins.Items[index].ToString() + "-" + DateFin.ToString();
                    objMail.Message = "Bonjour, \r\n\r\nVeuillez trouver ci-joint les factures encaissées du " + dtDebut.Text.ToString() + " au " + dtFin.Text.ToString() + " \r\n\r\nCordialement,\r\n\r\nLe service Comptabilité";
                    objMail.JoindrePiece(Application.StartupPath + SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.Cache + FileName + ".pdf");
                    //objMail.AjouteDestinataire("dmercier@sos-medecins.ch");
                    objMail.AjouteDestinataire(m_strDestinataire);
                    objMail.AjouteDestinataireCc("retributions@sos-medecins.ch");

                    //####### A réactiver pour l'envoi ######
                    if (objMail.Envoi())
                    {
                        tag++;
                        if (z_rpt != null)
                        {
                            z_rpt.Dispose();      //On oubli pas de détruire l'etat apres l'utilisation du rapport, ou Crystal va planter au bout de 75
                            z_rpt.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Message Non envoyé" + m_strDestinataire);
                        if (z_rpt != null)
                        {
                            z_rpt.Dispose();      //On oubli pas de détruire l'etat apres l'utilisation du rapport, ou Crystal va planter au bout de 75
                            z_rpt.Close();
                        }
                    }

                    objMail = null;
                }

                MessageBox.Show("Traitement terminé avec envoie de " + tag++ + " Mail");
                Cursor = Cursors.Default;
                bSalaireMed.Enabled = true;
                buttonEtatDebiteur.Enabled = true;
                buttonFacEcpMed.Enabled = true;
                buttonPrestationMed.Enabled = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} erreur: ", ex);
                MessageBox.Show(ex.ToString());
            }

            DSsalaireMed.Dispose();
        }

        //Factures encaissées par médecins
        private void SelectionneParMedecin(int CodeMedecin)
        {
            //Factures encaissées par médecin                      
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);      //Chaine de connection récupérée dans le app.config            
           
            try
            {
                //on ouvre la connexion
                dbConnection.Open();

                SqlCommand cmd = dbConnection.CreateCommand();
                cmd.Connection = dbConnection;       //On passe les parametres query et connection

                //on définit la requette Nombre de jours travaillés depuis le début de l'année            
                /*string sqlstr0 = @" SELECT tm.Nom as Nom, count(DISTINCT(Cast(DATEDIFF(day, 0, ta.DAP) As DateTime))) as NbJours, colonne1 = 1
                                    FROM tableconsultations tc,  factureconsultation fc, tableactes ta, tablemedecin tm, facture f, facture_etats fe, facture_status fs
                                    WHERE tc.NConsultation = fc.NConsultation
                                    AND tc.CodeAppel = ta.Num
                                    AND ta.CodeIntervenant = tm.CodeIntervenant
                                    AND f.NFacture = fc.NFacture
                                    AND f.NFacture = fe.NFacture
                                    AND f.NFacture = fs.NFacture
                                    AND ta.CodeIntervenant <> 0
                                    AND fs.FacDateAnnulee is null
                                    AND f.TotalFacture >= 0
                                    AND fe.Etat = 6
                                    AND fe.DatePaye >= @DateDeb AND fe.DatePaye < @DateFin
                                    group by tm.Nom
                                    order by tm.Nom";

                cmd.CommandText = sqlstr0;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("DateDeb", DateTime.Parse("01.01." + DateTime.Parse(dtDebut.Text.ToString()).Year.ToString()));
                cmd.Parameters.AddWithValue("DateFin", DateTime.Parse(dtFin.Text.ToString()));

                DSResult.Tables.Clear();   //On nettoye le DataSet
                DSResult.Tables.Add("TableNbJoursTvl");      //on déclare une table pour cet ensemble de donnée               
                DSResult.Tables["TableNbJoursTvl"].Load(cmd.ExecuteReader());       //on execute*/


                //Puis la liste des médecins ayant eu des encaissements dans le mois avec le montant de ces encaissements                
                string sqlstr1 = "SELECT tm.CodeIntervenant, tm.NomGeneve, tm.PrenomGeneve, fe.NFacture, fe.Montant, f.TotalFacture, fe.Moyen, fe.DatePaye, ta.DAP, fe.DateEtat,";
                sqlstr1 += "             fs.FacDateCession , (tpers.Nom + ' ' + tpers.Prenom) as Personne";
                sqlstr1 += "      FROM facture_etats fe INNER JOIN factureconsultation fc ON fc.NFacture = fe.NFacture";
                sqlstr1 += "                            INNER JOIN facture f ON f.NFacture = fe.NFacture ";
                sqlstr1 += "                            INNER JOIN tableconsultations tc ON tc.NConsultation = fc.NConsultation ";
                sqlstr1 += "                            INNER JOIN tableactes ta ON ta.Num = tc.CodeAppel ";
                sqlstr1 += "                            INNER JOIN tablemedecin tm ON tm.CodeIntervenant = ta.CodeIntervenant";
                sqlstr1 += "                            INNER JOIN tablepatient tp ON tp.IdPatient = ta.IndicePatient";
                sqlstr1 += "                            INNER JOIN tablepersonne tpers ON tpers.Idpersonne = tp.IdPersonne";
                sqlstr1 += "                            LEFT JOIN facture_status fs ON fs.NFacture = fe.NFacture";
                sqlstr1 += " WHERE fe.Etat = 6";
                sqlstr1 += " AND tm.CodeIntervenant = @CodeMedecin";
                sqlstr1 += " AND fe.DateEtat >= @DateDeb AND fe.DateEtat < @DateFin";                
                sqlstr1 += " GROUP BY tm.CodeIntervenant, tm.NomGeneve, tm.PrenomGeneve, fe.NFacture, fe.Montant, f.TotalFacture, fe.Moyen, fe.DatePaye, ta.DAP, fe.DateEtat,";
                sqlstr1 += "          fs.FacDateCession, tpers.Nom, tpers.Prenom";
                sqlstr1 += " ORDER BY tm.NomGeneve , fe.NFacture ";


                cmd.CommandText = sqlstr1;
                cmd.Parameters.Clear();

                try
                {
                    cmd.Parameters.AddWithValue("DateDeb", DateTime.Parse(dtDebut.Text.ToString()));
                    cmd.Parameters.AddWithValue("DateFin", DateTime.Parse(dtFin.Text.ToString()));
                    cmd.Parameters.AddWithValue("CodeMedecin", CodeMedecin);

                }

                catch (Exception ex)
                {
                    MessageBox.Show("SelectionneParMedecin(): " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }



                DSResult.Tables.Clear();   //On nettoye le DataSet
                DSResult.Tables.Add("ListeMedEncaiss");      //on déclare une table pour cet ensemble de donnée               
                DSResult.Tables["ListeMedEncaiss"].Load(cmd.ExecuteReader());       //on execute

                DSSalaireMedecin.Clear();    //Avant, on nettoye le DataSet

                // Pour le médecin ayant eu des encaissements, on regarde s'il y a du matériel qu'il faut déduire du montant total de la facture
                foreach (DataRow row in DSResult.Tables["ListeMedEncaiss"].Rows)
                {
                    int moyen = int.Parse(row["Moyen"].ToString());
                    int nFacture = int.Parse(row["NFacture"].ToString());
                    double montant = double.Parse(row["Montant"].ToString());
                    double totalFacture = double.Parse(row["TotalFacture"].ToString());

                    // Si c'est un encaissé sur place, on ne déduit pas le matériel
                    if (moyen != 7 && moyen != 8 && moyen != 15 && moyen != 16)
                    {                        
                        double PourcentMat = DeductionMateriel(nFacture);   //% de matériel

                        DataRow dr = DSSalaireMedecin.SalaireMed.NewRow();
                        dr["Id"] = row.Table.Rows.IndexOf(row);
                        dr["CodeMedecin"] = int.Parse(row["CodeIntervenant"].ToString());
                        dr["NomGeneve"] = row["NomGeneve"].ToString();
                        dr["PrenomGeneve"] = row["PrenomGeneve"].ToString();
                        dr["NFacture"] = nFacture;
                        dr["DateEtat"] = DateTime.Parse(row["DateEtat"].ToString());
                        dr["DatePaye"] = row["DatePaye"] != DBNull.Value ? (object)DateTime.Parse(row["DatePaye"].ToString()) : DBNull.Value;
                        dr["DAP"] = row["DAP"] != DBNull.Value ? (object)DateTime.Parse(row["DAP"].ToString()) : DBNull.Value;
                        dr["FacDateCession"] = row["FacDateCession"] != DBNull.Value ? (object)DateTime.Parse(row["FacDateCession"].ToString()) : DBNull.Value;
                        dr["Patient"] = row["Personne"].ToString();
                        dr["Moyen"] = moyen;
                        dr["TotalFacture"] = totalFacture;

                        if (PourcentMat > 0)  // On facture du matériel ?
                        {
                            dr["Montant"] = montant - (montant * PourcentMat / 100);
                        }
                        else
                        {
                            // Rien à déduire dans tous les cas car pas de matériel
                            dr["Montant"] = montant;
                        }

                        DSSalaireMedecin.SalaireMed.Rows.Add(dr);
                    }
                    else
                    {
                        // Rien à déduire dans tous les cas car pas de matériel
                        DataRow dr = DSSalaireMedecin.SalaireMed.NewRow();
                        dr["Id"] = row.Table.Rows.IndexOf(row);
                        dr["CodeMedecin"] = int.Parse(row["CodeIntervenant"].ToString());
                        dr["NomGeneve"] = row["NomGeneve"].ToString();
                        dr["PrenomGeneve"] = row["PrenomGeneve"].ToString();
                        dr["NFacture"] = nFacture;
                        dr["DateEtat"] = DateTime.Parse(row["DateEtat"].ToString());
                        dr["DatePaye"] = row["DatePaye"] != DBNull.Value ? (object)DateTime.Parse(row["DatePaye"].ToString()) : DBNull.Value;
                        dr["DAP"] = row["DAP"] != DBNull.Value ? (object)DateTime.Parse(row["DAP"].ToString()) : DBNull.Value;
                        dr["FacDateCession"] = row["FacDateCession"] != DBNull.Value ? (object)DateTime.Parse(row["FacDateCession"].ToString()) : DBNull.Value;
                        dr["Montant"] = montant;
                        dr["Patient"] = row["Personne"].ToString();
                        dr["Moyen"] = moyen;
                        dr["TotalFacture"] = totalFacture;

                        DSSalaireMedecin.SalaireMed.Rows.Add(dr);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                this.Cursor = Cursors.Default;
            }
            finally
            {
                dbConnection.Close();       //On ferme les connctions                                               
            }
        }

        private double DeductionMateriel(int NFacture)
        {
            double retour = 0;

            //Factures encaissées par médecin                      
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);      //Chaine de connection récupérée dans le app.config            

            try
            {
                //on ouvre la connexion
                dbConnection.Open();

                SqlCommand cmd = dbConnection.CreateCommand();
                cmd.Connection = dbConnection;       //On passe les parametres query et connection

                //Puis pour chaque facture on regarde s'il y a des Matériels
                string sqlstr0 = @"SELECT SUM(fp.Prix)
                                   FROM facture_prest fp  				
                                   WHERE fp.TypePrest = 2
                                   AND fp.NFacture =@NumFacture";

                cmd.CommandText = sqlstr0;

                cmd.Parameters.AddWithValue("NumFacture", NFacture);

                DataTable DtMat = new DataTable();
                DtMat.Load(cmd.ExecuteReader());       //on execute

                if (DtMat.Rows[0][0] != DBNull.Value && DtMat.Rows.Count > 0)
                    retour = double.Parse(DtMat.Rows[0][0].ToString());
                else retour = 0;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                this.Cursor = Cursors.Default;
            }
            finally
            {
                dbConnection.Close();       //On ferme les connctions                                               
            }

            return retour;
        }

        private string PlusieursEncaissements(int NFacture)
        {
            string retour = "Non";

            //Factures encaissées par médecin                      
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);      //Chaine de connection récupérée dans le app.config            

            try
            {
                //on ouvre la connexion
                dbConnection.Open();

                SqlCommand cmd = dbConnection.CreateCommand();
                cmd.Connection = dbConnection;       //On passe les parametres query et connection

                //Puis pour chaque facture on regarde s'il y a des Matériels
                string sqlstr0 = @"SELECT fe.NFacture, fe.Montant  
                                   FROM facture_etats fe  				
                                   WHERE fe.Etat = 6
                                   AND fe.NFacture = @NumFacture";

                cmd.CommandText = sqlstr0;

                cmd.Parameters.AddWithValue("NumFacture", NFacture);

                DataTable DtMat = new DataTable();
                DtMat.Load(cmd.ExecuteReader());       //on execute

                if (DtMat.Rows.Count > 1)       //Plusieurs encaissements...on ne déduit pas les eventuel matériels
                    retour = "Oui";
                else retour = "Non";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                this.Cursor = Cursors.Default;
            }
            finally
            {
                dbConnection.Close();       //On ferme les connctions                                               
            }

            return retour;
        }

        private void bChargerMedDebiteurs_Click(object sender, EventArgs e)
        {
            ChargeMedecinsDebiteurs();                                    
        }

        private void bStatistiques_Click(object sender, EventArgs e)
        {
            ChargeMedecinsStats();
        }

        public void ChargerMedecinsEncaiss()
        {
            //Factures encaissées par médecin                      
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);      //Chaine de connection récupérée dans le app.config            

            try
            {
                //on ouvre la connexion
                dbConnection.Open();

                SqlCommand cmd = dbConnection.CreateCommand();
                cmd.Connection = dbConnection;       //On passe les parametres query et connection

                //Puis la liste des médecins ayant eu des encaissements dans le mois
                string sqlstr0 = @"SELECT tm.CodeIntervenant, tm.NomGeneve + ' ' + tm.PrenomGeneve as NomMED                                           
                                   FROM facture_etats fe INNER JOIN factureconsultation fc ON fc.NFacture = fe.NFacture
                                                         INNER JOIN facture f ON f.NFacture = fe.NFacture 		
					                                     INNER JOIN tableconsultations tc ON tc.NConsultation = fc.NConsultation 
					                                     INNER JOIN tableactes ta ON ta.Num = tc.CodeAppel 
					                                     INNER JOIN tablemedecin tm ON tm.CodeIntervenant = ta.CodeIntervenant
                                                         INNER JOIN tablepatient tp ON tp.IdPatient = ta.IndicePatient
                                                         INNER JOIN tablepersonne tpers ON tpers.Idpersonne = tp.IdPersonne
                                                         LEFT JOIN facture_status fs ON fs.NFacture = fe.NFacture
                                   WHERE fe.Etat = 6                                  
                                   AND fe.DateEtat >= @DateDeb AND fe.DateEtat < @DateFin
                                   GROUP BY tm.CodeIntervenant, tm.NomGeneve, tm.PrenomGeneve                                            
                                   ORDER BY tm.NomGeneve";

                cmd.CommandText = sqlstr0;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("DateDeb", DateTime.Parse(dtDebut.Text.ToString()));
                cmd.Parameters.AddWithValue("DateFin", DateTime.Parse(dtFin.Text.ToString()));

                DataTable dtMedecins = new DataTable();
                dtMedecins.Load(cmd.ExecuteReader());       //on execute

                //On vide la liste
                clbmedecins.Items.Clear();

                foreach (DataRow rows in dtMedecins.Rows)
                {
                    ListItem item = new ListItem(rows["CodeIntervenant"], rows["NomMED"].ToString());
                    clbmedecins.Items.Add(item);
                }

                //Pour le nombre de médecins cochés
                lcompteur.Text = "0";

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                this.Cursor = Cursors.Default;
            }
            finally
            {
                dbConnection.Close();       //On ferme les connctions                                               
            }
        }

        private void ChargeMedecinsStats()
        {
            //Chargement des médecins pour les statistiques
            DateTime DtDebut = DateTime.Parse(dtDebut.Text.ToString());
            DateTime DtFin = DateTime.Parse(dtFin.Text.ToString());

            progressBar1.Maximum = clbmedecins.CheckedIndices.Count;

            int tag = 0;

            Refresh();
            //Factures encaissées par médecin                      
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);      //Chaine de connection récupérée dans le app.config            

            try
            {
                //on ouvre la connexion
                dbConnection.Open();

                SqlCommand cmd = dbConnection.CreateCommand();
                cmd.Connection = dbConnection;       //On passe les parametres query et connection                
               
                string sqlstr0 = @"SELECT tm.CodeIntervenant, tm.nom                                         
                                   FROM facture_etats fe INNER JOIN factureconsultation fc ON fc.NFacture = fe.NFacture
                                                         INNER JOIN facture f ON f.NFacture = fe.NFacture 		
					                                     INNER JOIN tableconsultations tc ON tc.NConsultation = fc.NConsultation 
					                                     INNER JOIN tableactes ta ON ta.Num = tc.CodeAppel 
					                                     INNER JOIN tablemedecin tm ON tm.CodeIntervenant = ta.CodeIntervenant                                                                                                                  
                                   WHERE f.DateCreation >= @DateDeb and f.DateCreation <= @DateFin                                   
                                   GROUP BY tm.CodeIntervenant, tm.Nom                                           
                                   ORDER BY tm.Nom ";

                cmd.CommandText = sqlstr0;                
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("DateDeb", DtDebut);
                cmd.Parameters.AddWithValue("DateFin", DtFin);

                DataTable dtMedecins = new DataTable();
                dtMedecins.Load(cmd.ExecuteReader());       //on execute

                //On vide la liste
                clbmedecins.Items.Clear();

                foreach (DataRow rows in dtMedecins.Rows)
                {
                    ListItem item = new ListItem(rows["CodeIntervenant"], rows["Nom"].ToString());
                    clbmedecins.Items.Add(item);
                }

                //Pour le nombre de médecins cochés
                lcompteur.Text = "0";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                this.Cursor = Cursors.Default;
            }
            finally
            {
                dbConnection.Close();       //On ferme les connctions                                               
            }
        }

        private void ChargeMedecinsDebiteurs()
        {
            //etats debiteurs            
            DateTime DateDebDepuis = DateTime.Parse(dtdebDepuis.Text.ToString());
            DateTime DateDebFin = DateTime.Parse(dtdebFin.Text.ToString());
            DateTime DateSoldeAu = DateTime.Parse(dtSoldeAu.Text.ToString());

            string jusque = DateSoldeAu.ToShortDateString();
            string FacEnvoye_de = DateDebDepuis.ToShortDateString();
            string FacEnvoye_Au = DateDebFin.ToShortDateString();

            int i = 0;
            Regex modèle = new Regex(@"\d{2}.\d{2}.\d{4}");
            MatchCollection résultat_dbt = modèle.Matches(jusque);
            //MatchCollection résultat_dbtplusun = modèle.Matches(jusqueplusun);
            MatchCollection résultat_DAP1 = modèle.Matches(FacEnvoye_de);
            MatchCollection résultat_DAP2 = modèle.Matches(FacEnvoye_Au);

            jusque = résultat_dbt[i].Value;
            //jusqueplusun = résultat_dbtplusun[i].Value;
            FacEnvoye_de = résultat_DAP1[i].Value;
            FacEnvoye_Au = résultat_DAP2[i].Value;

            //Factures encaissées par médecin                      
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);      //Chaine de connection récupérée dans le app.config            

            try
            {
                //on ouvre la connexion
                dbConnection.Open();

                SqlCommand cmd = dbConnection.CreateCommand();
                cmd.Connection = dbConnection;       //On passe les parametres query et connection

                //Requette   
                string sqlstr0 = " select tm.CodeIntervenant, tm.Nom as NomMED";
                sqlstr0 += " from facture f, facture_etats fe, factureConsultation fc, facture_status fs, tableconsultations tc, tableactes ta, tablemedecin tm, tablepersonne pe, tablepatient pa ";
                sqlstr0 += " where f.NFacture = fe.NFacture ";
                sqlstr0 += " and fc.NFacture = f.NFacture ";
                sqlstr0 += " and fs.NFacture = f.NFacture ";
                sqlstr0 += " and fc.NConsultation = tc.NConsultation ";
                sqlstr0 += " and tc.CodeAppel = ta.Num ";
                sqlstr0 += " and ta.CodeIntervenant = tm.CodeIntervenant ";
                sqlstr0 += " and tc.IndicePatient = pa.IdPatient ";
                sqlstr0 += " and pa.IdPersonne = pe.IdPersonne ";
                sqlstr0 += " and fs.FacDateAnnulee is Null";
                sqlstr0 += " and f.TotalFacture > 0";                     //Ajout le 20.11.2015
                sqlstr0 += " and  (fs.FacDateAcquittee >'" + jusque + "'  or fs.FacDateAcquittee is null)";                
                sqlstr0 += " and fe.DatePaye <= '" + jusque + "' ";
                sqlstr0 += " and f.DateCreation >= '" + FacEnvoye_de + "' and f.DateCreation <= '" + FacEnvoye_Au + "' ";
                //Blocage Helsana/Progres 16.05.2018 au 10.07.2019 inclus (pour enlever l'affichage des soldes négatifs...les médecins comprennent rien!!!)
                sqlstr0 += " and f.NFacture not in (Select fa.NFacture FROM facture fa inner join factureConsultation fac ON fac.NFacture = fa.NFacture";
                sqlstr0 += "                        inner join tableconsultations tac ON tac.NConsultation = fac.NConsultation ";
                sqlstr0 += "                        inner join tableactes taa ON tac.CodeAppel = taa.Num ";
                sqlstr0 += "                        where fa.DateCreation >= '16.05.2018' and fa.DateCreation <= '10.07.2019' and fa.CodeDestinataire in (130,194))";
                //************************************************Fin blocage*****************************************
                //Blocage avance Groupe Mutuel Le 09.07.2024 pour avance salaire
                sqlstr0 += " and f.NFacture not in (Select fb.NFacture FROM facture fb inner join factureConsultation fac ON fac.NFacture = fb.NFacture";
                sqlstr0 += "                                                           inner join tableconsultations tac ON tac.NConsultation = fac.NConsultation ";
                sqlstr0 += "                          inner join tableactes taa ON tac.CodeAppel = taa.Num ";
                sqlstr0 += "                        where (fb.DateCreation >= '2024-01-01' and fb.DateCreation <= '31.05.2024') ";
                sqlstr0 += "                        and fb.CodeDestinataire in (59))";
                //*****************************************Fin blocage************************************************
                sqlstr0 += " group by tm.CodeIntervenant, tm.Nom ";
                sqlstr0 += " ORDER BY tm.Nom";

                cmd.CommandText = sqlstr0;
                DataTable dtMedecin = new DataTable();
                dtMedecin.Load(cmd.ExecuteReader());       //on execute

                //On vide la liste
                clbmedecins.Items.Clear();

                foreach (DataRow rows in dtMedecin.Rows)
                {
                    ListItem item = new ListItem(rows["CodeIntervenant"], rows["NomMED"].ToString());
                    clbmedecins.Items.Add(item);
                }

                //Pour le nombre de médecins cochés
                lcompteur.Text = "0";

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                this.Cursor = Cursors.Default;
            }
            finally
            {
                dbConnection.Close();       //On ferme les connctions                                               
            }
        }
    }
}