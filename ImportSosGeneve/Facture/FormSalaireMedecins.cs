using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using SosMedecins.SmartRapport.DAL;
using SosMedecins.SmartRapport.EtatsCrystal;
using System.Text.RegularExpressions;
using System.Globalization;

namespace ImportSosGeneve.Facture
{
    public partial class FormSalaireMedecins : Form
    {
        public DataSet DSResult = new DataSet();
        //public DataTable FacturesEncaissees = new DataTable();
        public SalaireMedecin DSSalaireMedecin = new SalaireMedecin();

        public FormSalaireMedecins()
        {
            InitializeComponent();
           
            //On initialise les dates            
            dTDateDeb.Value = DateTime.Parse("01." + DateTime.Now.AddMonths(-1).Month.ToString() + "." + DateTime.Now.Year.ToString());
            dTDateFin.Value = DateTime.Parse("01." + DateTime.Now.Month.ToString() + "." + DateTime.Now.Year.ToString()).AddDays(-1);

            dTPDateDebut.Value = DateTime.Parse("01." + DateTime.Now.AddMonths(-1).Month.ToString() + "." + DateTime.Now.Year.ToString()).AddYears(-5);
            dTPDateFin.Value = DateTime.Parse("01." + DateTime.Now.Month.ToString() + "." + DateTime.Now.Year.ToString()).AddDays(-1);
            dTPSDateSolde.Value = DateTime.Parse("01." + DateTime.Now.Month.ToString() + "." + DateTime.Now.Year.ToString()).AddDays(-1);           
        }

       
        private void bFactEncaiss_Click(object sender, EventArgs e)
        {
            //Factures encaissées par médecin                      
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            string dotFormat  = "dd.MM.yyyy";
            string slashFormat = "dd/MM/yyyy";

            SqlConnection dbConnection = new SqlConnection(connex);      //Chaine de connection récupérée dans le app.config            

            try
            {
                DateTime parsedDateDeb, parsedDateFin;

                //on ouvre la connexion
                dbConnection.Open();

                SqlCommand cmd = dbConnection.CreateCommand();
                cmd.Connection = dbConnection;       //On passe les parametres query et connection

                //on définit la requette Nombre de jours travaillés depuis le début de l'année            
                string sqlstr0 = @" SELECT tm.Nom as Nom, count(DISTINCT(Cast(DATEDIFF(day, 0, ta.DAP) As DateTime))) as NbJours, colonne1 = 1
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
                                    AND fe.DatePaye >= @DateDeb AND fe.DatePaye <= @DateFin
                                    group by tm.Nom
                                    order by tm.Nom";

                cmd.CommandText = sqlstr0;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("DateDeb", DateTime.Parse("01.01." + DateTime.Parse(dTDateDeb.Text.ToString()).Year.ToString()));
                cmd.Parameters.AddWithValue("DateFin", DateTime.Parse(dTDateFin.Text.ToString()));

                

                DSResult.Tables.Clear();   //On nettoye le DataSet
                DataTable TableJTvl = new DataTable("TableNbJoursTvl"); //On ajoute une table
                DSResult.Tables.Add(TableJTvl);      //on déclare une table pour cet ensemble de donnée               
                DSResult.Tables["TableNbJoursTvl"].Load(cmd.ExecuteReader());       //on execute


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
                sqlstr1 += " WHERE fe.Etat = 6                      ";
                sqlstr1 += " AND fe.DateEtat >= @DateDeb AND fe.DateEtat <= @DateFin";
                sqlstr1 += " GROUP BY tm.CodeIntervenant, tm.NomGeneve, tm.PrenomGeneve, fe.NFacture, fe.Montant, f.TotalFacture, fe.Moyen, fe.DatePaye, ta.DAP, fe.DateEtat,";
                sqlstr1 += "          fs.FacDateCession, tpers.Nom, tpers.Prenom";
                sqlstr1 += " ORDER BY tm.NomGeneve , fe.NFacture ";

                //string sqlstr1 = "SELECT tm.CodeIntervenant, tm.PrenomGeneve, tm.NomGeneve, fe.NFacture, fe.Montant, f.TotalFacture,fe.Moyen, fe.DatePaye, ta.DAP, fe.DateEtat,";
                //sqlstr1 += "  fs.FacDateCession, (tpers.Nom + ' ' + tpers.Prenom) as Personne";
                //sqlstr1 += "  FROM facture f INNER JOIN factureconsultation fc ON f.NFacture = fc.NFacture";
                //sqlstr1 += "               INNER JOIN facture_status fs ON f.NFacture = fs.NFacture";
                //sqlstr1 += "               INNER JOIN facture_etats fe ON f.NFacture = fe.NFacture";
                //sqlstr1 += "               INNER JOIN tableconsultations tc ON fc.NConsultation = tc.NConsultation";
                //sqlstr1 += "               INNER JOIN tablepatient tp ON tc.IndicePatient = tp.IdPatient";
                //sqlstr1 += "               INNER JOIN tableactes ta ON tc.CodeAppel = ta.Num";
                //sqlstr1 += "               INNER JOIN tablepersonne tpers ON tp.IdPersonne = tpers.IdPersonne";
                //sqlstr1 += "               INNER JOIN tablemedecin tm ON ta.CodeIntervenant = tm.CodeIntervenant";
                //sqlstr1 += "  WHERE fe.Etat = 6                      ";
                //sqlstr1 += "  AND fe.DateEtat >= @DateDeb AND fe.DateEtat <= @DateFin";
                //sqlstr1 += "  GROUP BY tm.CodeIntervenant, tm.NomGeneve, tm.PrenomGeneve, fe.NFacture, fe.Montant, f.TotalFacture, fe.Moyen, fe.DatePaye, ta.DAP, fe.DateEtat,";
                //sqlstr1 += "           fs.FacDateCession, tpers.Nom, tpers.Prenom";
                //sqlstr1 += "  ORDER BY tm.NomGeneve, fe.NFacture ";


                cmd.CommandText = sqlstr1;
                cmd.Parameters.Clear();

                try
                {
                    cmd.Parameters.AddWithValue("DateDeb", DateTime.Parse(dTDateDeb.Text.ToString()));

                }

                catch (Exception ex)
                {
                    MessageBox.Show("bFactEncaiss_Click() DateDeb2: DateDeb" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Cursor = Cursors.Default;
                }

                try
                {
                    cmd.Parameters.AddWithValue("DateFin", DateTime.Parse(dTDateFin.Text.ToString()));

                }

                catch (Exception ex)
                {
                    MessageBox.Show("bFactEncaiss_Click() DateFin2: DateFin can`t be parsed" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Cursor = Cursors.Default;
                }

                //Original
                //cmd.Parameters.AddWithValue("DateDeb", DateTime.Parse(dTDateDeb.Text.ToString()));
                //cmd.Parameters.AddWithValue("DateFin", DateTime.Parse(dTDateFin.Text.ToString()));

                DataTable tableEncaiss = new DataTable("ListeMedEncaiss");  //Ajout d'une table
                DSResult.Tables.Add(tableEncaiss);      //on déclare une table pour cet ensemble de donnée               
                DSResult.Tables["ListeMedEncaiss"].Load(cmd.ExecuteReader());       //on execute

                DSSalaireMedecin.Clear();   //Avant, on nettoye le DataSet
                // Pour chaque médecin ayant des encaissements, on regarde s'il y a du matériel qu'il faut déduire du montant total de la facture
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
                            dr["Montant"] = montant;
                        }

                        DSSalaireMedecin.SalaireMed.Rows.Add(dr);
                    }
                    else
                    {
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

                ImprimeRelevé(DSSalaireMedecin);
            }

            catch (Exception ex)
            {
                MessageBox.Show("bFactEncaiss_Click: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Cursor = Cursors.Default;
            }
            finally
            {
                dbConnection.Close();       //On ferme les connctions                                               
            }
        }

        //Déduction du matériel : on détermine le pourcentage de matériel par rapport au total de la facture        
        private double DeductionMateriel(int NFacture)
        {
            double retour = 0;
                          
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);      //Chaine de connection récupérée dans le app.config            

            try
            {
                //on ouvre la connexion
                dbConnection.Open();

                SqlCommand cmd = dbConnection.CreateCommand();
                cmd.Connection = dbConnection;       //On passe les parametres query et connection

                //Puis pour chaque facture on regarde s'il y a des Matériels
                string sqlstr0 = @"SELECT CASE         
                                            WHEN Total = 0 THEN 0	                                        
                                            ELSE (COALESCE(Mat, 0) * 100.0 / Total)
                                          END AS PourcentageMat
                                   FROM ( SELECT 
                                                (SELECT SUM(fp1.Prix) FROM facture_prest fp1 WHERE fp1.NFacture = @NumFacture) AS Total,
                                                (SELECT SUM(fp2.Prix) FROM facture_prest fp2 WHERE fp2.NFacture = @NumFacture AND fp2.TypePrest = 1) AS Medic,
                                                (SELECT SUM(fp3.Prix) FROM facture_prest fp3 WHERE fp3.NFacture = @NumFacture AND fp3.TypePrest = 2) AS Mat
                                         ) AS SubQuery";

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
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Cursor = Cursors.Default;
            }
            finally
            {
                dbConnection.Close();       //On ferme les connctions                                               
            }

            return retour;
        }

        private void ImprimeRelevé(SalaireMedecin DSsalaireMed)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                //On créer les 2 Etats (sous état ne fonctionne pas avec un autre ensemble de données)            
                Facture_Encaissement_Medecins_V4 z_rpt = new Facture_Encaissement_Medecins_V4();
                z_rpt.SetDataSource(DSsalaireMed);  //source de donnée du rapport

                Fonctions.ChangeConnection(z_rpt, Variables.ConnexionBase);
                frmCrystalReportViewer z_frm = new frmCrystalReportViewer();
                z_frm.ReportSource = z_rpt;     //On attache le rpt à la form du rapport

                //On passe les paramètres de l'état
                z_rpt.SetParameterValue("DateDebut", DateTime.Parse(dTDateDeb.Text.ToString()));
                z_rpt.SetParameterValue("DateFin", DateTime.Parse(dTDateFin.Text.ToString()));

                this.Cursor = Cursors.Default;

                // Affichage
                z_frm.ShowDialog();
                // liberation des elements
                z_rpt.Dispose();
                z_frm.Dispose();
                z_frm = null;

                //Pour le 2eme Etat
                this.Cursor = Cursors.WaitCursor;

                TableauNbjoursTravallees NbJoursTvl_rpt = new TableauNbjoursTravallees();
                frmCrystalReportViewer z_frmNbJoursTvl = new frmCrystalReportViewer();

                NbJoursTvl_rpt.SetDataSource(DSResult.Tables[0]);
                z_frmNbJoursTvl.ReportSource = NbJoursTvl_rpt;                   //On attache le rpt à la form du rapport

                this.Cursor = Cursors.Default;

                //On affecte un champs text pour la période.
                TextObject PeriodeNbJours = (TextObject)NbJoursTvl_rpt.Section1.ReportObjects["TPeriodeNbJours"];
                PeriodeNbJours.Text = "Pour la période du 01.01." + DateTime.Parse(dTDateDeb.Text.ToString()).Year.ToString() + " au " + dTDateFin.Text.ToString();

                TextObject Annee = (TextObject)NbJoursTvl_rpt.Section4.ReportObjects["TAnnee"];
                Annee.Text = DateTime.Parse(dTDateDeb.Text.ToString()).Year.ToString();

                // Affichage
                z_frmNbJoursTvl.ShowDialog();
                // liberation des elements
                NbJoursTvl_rpt.Dispose();
                z_frmNbJoursTvl.Dispose();
            }

            catch(Exception ex)
            {
                MessageBox.Show("ImprimeRelevé: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Cursor = Cursors.Default;
            }
                       
        }

        private void bEnvoiMail_Click(object sender, EventArgs e)
        {
            ListeMedecinscs lm = new ListeMedecinscs();
            lm.ShowDialog();
        }

        private void bEtatsDebiteurs_Click(object sender, EventArgs e)
        {            		            
            DateTime DateDateAppel = DateTime.Parse(dTPDateDebut.Text.ToString());
            DateTime DateDateAppe2 = DateTime.Parse(dTPDateFin.Text.ToString());
            DateTime DateJusqueAu = DateTime.Parse(dTPSDateSolde.Text.ToString());

            string jusque = DateJusqueAu.ToShortDateString();
            string jusqueplusun = DateJusqueAu.AddDays(1).ToShortDateString();
            string FacEnvoye_de = DateDateAppel.ToShortDateString();
            string FacEnvoye_Au = DateDateAppe2.ToShortDateString();

            int i = 0;
            Regex modèle = new Regex(@"\d{2}.\d{2}.\d{4}");
            MatchCollection résultat_dbt = modèle.Matches(jusque);
            MatchCollection résultat_dbtplusun = modèle.Matches(jusqueplusun);
            MatchCollection résultat_DAP1 = modèle.Matches(FacEnvoye_de);
            MatchCollection résultat_DAP2 = modèle.Matches(FacEnvoye_Au);

            jusque = résultat_dbt[i].Value;
            jusqueplusun = résultat_dbtplusun[i].Value;
            //DAP_de = résultat_DAP1[i].Value;
            //DAP_Au = résultat_DAP2[i].Value;
            FacEnvoye_de = résultat_DAP1[i].Value;
            FacEnvoye_Au = résultat_DAP2[i].Value;

            //set month name and year
            string mois = "";
            switch (DateJusqueAu.Month)
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

            string year = DateJusqueAu.Year.ToString();
            // tri les factures par            
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
            string nom = "";
            if (cbMedecin.Text != "")
                nom = "AND tm.Nom = '" + cbMedecin.Text + "'";

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
            sql += "                        where fa.DateCreation >= '16.05.2018' and fa.DateCreation <= '10.07.2019' and fa.CodeDestinataire in (130, 194))";
            //************************************************Fin blocage*****************************************
            //Blocage avance Groupe Mutuel Le 09.07.2024 pour avance salaire
            sql += " and f.NFacture not in (Select fb.NFacture FROM facture fb inner join factureConsultation fac ON fac.NFacture = fb.NFacture";
            sql += "                        inner join tableconsultations tac ON tac.NConsultation = fac.NConsultation ";
            sql += "                        inner join tableactes taa ON tac.CodeAppel = taa.Num ";          
            sql += "                        where fb.DateCreation >= '2024-01-01' and fb.DateCreation <= '31.05.2024' and fb.CodeDestinataire in (59))";
            //*****************************************Fin blocage************************************************
            sql += " GROUP BY tm.Nom, tm.Mail, f.NFacture, f.AdresseDestinataire, pe.Tel, pe.Nom, pe.Prenom, f.DateCreation, tp.TotalFacture";
            sql += " ORDER BY tm.Nom, " + ordre.ToString();


            //**************************
            OutilsExt.OutilsSql.RemplitDataTable(Donnees.MesFactures.Tables[0], sql);

            if (Donnees.MesFactures.Tables.Count > 0 && Donnees.MesFactures.Tables[0].Rows.Count > 0)
            {
                frmFacturesEncMed imprDebiteurs = new frmFacturesEncMed(Donnees.MesFactures, mois, year, jusque, FacEnvoye_de, FacEnvoye_Au, "Debiteurs");
                imprDebiteurs.ShowDialog();
                imprDebiteurs.Dispose();               
            }
        }

        private void ChargeMedecinsAvDebiteurs()
        {
            DateTime DateDateAppel = DateTime.Parse(dTPDateDebut.Text.ToString());
            DateTime DateDateAppe2 = DateTime.Parse(dTPDateFin.Text.ToString());
            DateTime DateJusqueAu = DateTime.Parse(dTPSDateSolde.Text.ToString());

            string jusque = DateJusqueAu.ToShortDateString();
            string jusqueplusun = DateJusqueAu.AddDays(1).ToShortDateString();
            string FacEnvoye_de = DateDateAppel.ToShortDateString();
            string FacEnvoye_Au = DateDateAppe2.ToShortDateString();

            int i = 0;
            Regex modèle = new Regex(@"\d{2}.\d{2}.\d{4}");
            MatchCollection résultat_dbt = modèle.Matches(jusque);
            MatchCollection résultat_dbtplusun = modèle.Matches(jusqueplusun);
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
                  
                //Chargement de la liste des médecins ayant des états débiteurs
                string sqlstr0 = "select Distinct tm.CodeIntervenant, tm.Nom as NomMed";
                sqlstr0 += " FROM tableconsultations tc INNER JOIN tableactes ta ON ta.Num = tc.CodeAppel";
                sqlstr0 += "                            INNER JOIN tablemedecin tm ON tm.CodeIntervenant = ta.CodeIntervenant";
                sqlstr0 += "                            INNER JOIN tablepatient pa ON pa.IdPatient = ta.IndicePatient";
                sqlstr0 += "                            INNER JOIN tablepersonne pe ON pe.IdPersonne = pa.IdPersonne";
                sqlstr0 += "                            LEFT JOIN factureConsultation fc ON fc.NConsultation = tc.NConsultation";
                sqlstr0 += "                            LEFT JOIN facture f ON f.NFacture = fc.NFacture";
                sqlstr0 += "                            LEFT JOIN facture_status fs ON fs.NFacture = f.NFacture";
                sqlstr0 += "                            LEFT JOIN facture_etats fe ON fe.NFacture = f.NFacture";
                sqlstr0 += " WHERE fs.FacDateAnnulee is null";
                sqlstr0 += " AND f.TotalFacture > 0";
                sqlstr0 += " AND(fs.FacDateAcquittee > '" + jusque + "' or fs.FacDateAcquittee is null)";
                sqlstr0 += " AND fe.DatePaye <= '" + jusque + "'";                
                sqlstr0 += " AND f.DateCreation >= '" + FacEnvoye_de + "'";
                sqlstr0 += " AND f.DateCreation <= '" + FacEnvoye_Au + "'";
                //Blocage Helsana/Progres 16.05.2018 au 10.07.2019 inclus (pour enlever l'affichage des soldes négatifs...les médecins comprennent rien!!!)
                sqlstr0 += " AND f.NFacture NOT IN(Select fa.NFacture FROM facture fa INNER JOIN factureConsultation fac ON fac.NFacture = fa.NFacture";
                sqlstr0 += "                                                          INNER JOIN tableconsultations tac ON tac.NConsultation = fac.NConsultation";
                sqlstr0 += "                                                          INNER JOIN tableactes taa ON tac.CodeAppel = taa.Num";
                sqlstr0 += "                       WHERE fa.DateCreation >= '16.05.2018' AND fa.DateCreation <= '10.07.2019'";
                sqlstr0 += "                       AND fa.CodeDestinataire in (130, 194))";
                //Blocage Groupe Mutuel le 09.07.2024 pour avance salaire**************************************
                sqlstr0 += " AND f.NFacture NOT IN(Select fb.NFacture FROM facture fb INNER JOIN factureConsultation fac ON fac.NFacture = fb.NFacture";
                sqlstr0 += "                                                          INNER JOIN tableconsultations tac ON tac.NConsultation = fac.NConsultation";
                sqlstr0 += "                                                          INNER JOIN tableactes taa ON tac.CodeAppel = taa.Num";
                sqlstr0 += "                       WHERE fb.DateCreation >= '2024-01-01' AND fb.DateCreation <= '31.05.2024'";
                sqlstr0 += "                       AND fb.CodeDestinataire in (59))";
                //***************************************Fin blocage*******************************************
                sqlstr0 += " ORDER BY tm.Nom";               

                cmd.CommandText = sqlstr0;
                DataTable DtMedecin = new DataTable();
                DtMedecin.Load(cmd.ExecuteReader());       //on execute

                //Chargement des médecins
                cbMedecin.Items.Clear();
                                
                foreach (DataRow rows in DtMedecin.Rows)
                {
                    ListItem item = new ListItem(rows["CodeIntervenant"], rows["NomMED"].ToString());
                    cbMedecin.Items.Add(item);
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

        private void cbMedecin_DropDown(object sender, EventArgs e)
        {
            ChargeMedecinsAvDebiteurs();
        }
    }
}
