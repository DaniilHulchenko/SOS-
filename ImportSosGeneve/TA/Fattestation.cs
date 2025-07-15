using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using SosMedecins.SmartRapport.EtatsCrystal;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using SosMedecins.SmartRapport.GestionApplication;

namespace ImportSosGeneve.TA
{
    public partial class Fattestation : Form
    {

        DataSet DtsRecherchePatient = new DataSet();
        private Int64 Idpersonne = 0;
        private string IdAbonnementTA = null;
        //private string Nompersonne = "";
        //private string Prenompersonne = "";
        private DateTime DateDebut = DateTime.Parse("31.12." + DateTime.Today.AddYears(-2).Year);
        private DateTime DateFin = DateTime.Parse("01.01." + DateTime.Today.Year);       
        Attestation_TA attestationTA = new Attestation_TA();

        public Fattestation()
        {
            InitializeComponent();            
        }

        //Recherche par nom prénom
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //Si on a un debut de nom (au moins 3 caractères) on cherche....
            if (textBox1.Text.Length > 2)
            {
                Cursor.Current = Cursors.WaitCursor;     //Curseur d'attente

                //On reinitialise l'IdAbonnement
                IdAbonnementTA = null;
                
                DataSet RetourRecherche = new DataSet();

                RetourRecherche = RechercheNomPatient(textBox1.Text);    //Recherche par personne

                //on renseigne la liste personne
                listBoxTA.BeginUpdate();     //Pour stopper le rafraichissement
                listBoxTA.Items.Clear();     //On vide tout

                int index = 0;

                foreach (DataRow Row in RetourRecherche.Tables[0].Rows)
                {
                    DateTime DateNaiss;
                    string stDateNaisss = "";

                    if (DateTime.TryParse(RetourRecherche.Tables[0].Rows[index][2].ToString(), out DateNaiss))
                        stDateNaisss = String.Format("{0:dd-MM-yyyy}", DateNaiss);
                    else stDateNaisss = "Pas de date de naissance";

                    listBoxTA.Items.Add(RetourRecherche.Tables[0].Rows[index][1].ToString() + " ...|... " +
                                                stDateNaisss + " ...| " +
                                                RetourRecherche.Tables[0].Rows[index][0].ToString());

                    index++;
                }
                listBoxTA.EndUpdate();       //Fin de la mise à jour...on reactive le rafraîchissement  

                Cursor.Current = Cursors.Default;    //Remise en place du curseur par défaut
            }
        }


        //Recherche par date de naissance
        private void dTDateNaissance_ValueChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;     //Curseur d'attente

            //On reinitialise l'IdAbonnement
            IdAbonnementTA = null;
            
            DataSet RetourRecherche = new DataSet();
            string stDateNaiss = String.Format("{0:dd-MM-yyyy}", dTDateNaissance.Value);

            RetourRecherche = RechercheDateNaissPatient(stDateNaiss);    //Recherche par personne

            //on renseigne la liste personne
            listBoxTA.BeginUpdate();     //Pour stopper le rafraichissement
            listBoxTA.Items.Clear();     //On vide tout

            int index = 0;

            foreach (DataRow Row in RetourRecherche.Tables[0].Rows)
            {
                DateTime DateNaiss;
                string stDateNaisss = "";

                if (DateTime.TryParse(RetourRecherche.Tables[0].Rows[index][2].ToString(), out DateNaiss))
                    stDateNaisss = String.Format("{0:dd-MM-yyyy}", DateNaiss);
                else stDateNaisss = "Pas de date de naissance";

                listBoxTA.Items.Add(RetourRecherche.Tables[0].Rows[index][1].ToString() + " ...|... " +
                                            stDateNaisss + " ...| " +
                                            RetourRecherche.Tables[0].Rows[index][0].ToString());

                index++;
            }
            listBoxTA.EndUpdate();       //Fin de la mise à jour...on reactive le rafraîchissement  

            Cursor.Current = Cursors.Default;    //Remise en place du curseur par défaut        
        }


        public DataSet RechercheNomPatient(string Patient)
        {
            //On vide le dataSet
            DtsRecherchePatient.Tables.Clear();

            //On recherche les personnes coorespondantes
            string connex = System.Configuration.ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);      //Chaine de connection récupérée dans le app.config

            //on ouvre la connexion
            dbConnection.Open();

            SqlCommand cmd = dbConnection.CreateCommand();
            cmd.Connection = dbConnection;       //On passe les parametres query et connection

            try
            {
                //on définit la requette             
                string sqlstr0 = "SELECT pe.Idpersonne, pe.Nom + ' ' + pe.Prenom as personne, pe.DateNaissance, ta.IdAbonnement, ta.periodicite FROM TablePersonne pe, TablePatient tp, ta_Abonnement ta";
                sqlstr0 += " WHERE pe.Nom + ' ' + pe.Prenom like @Rech";
                sqlstr0 += " AND pe.Idpersonne = tp.Idpersonne";
                sqlstr0 += " AND tp.TypeAbonnement = 'TA'";
                sqlstr0 += " AND tp.IdAbonnement = ta.IdAbonnement ";


                // Ajout des paramètres
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("Rech", Patient + "%");
                cmd.CommandText = sqlstr0;

                DtsRecherchePatient.Tables.Clear();
                DtsRecherchePatient.Tables.Add("RecherchePatient");
                DtsRecherchePatient.Tables[0].Load(cmd.ExecuteReader());       //on execute                           
            }
            catch (Exception a)
            {
                Console.WriteLine("Erreur : " + a.Message);
            }
            finally
            {
                //Fermeture de la connexion
                if (dbConnection.State == System.Data.ConnectionState.Open)
                    dbConnection.Close();
            }

            return (DtsRecherchePatient);
        }


        public DataSet RechercheDateNaissPatient(string DateNaissPatient)
        {
            //On vide le dataSet
            DtsRecherchePatient.Tables.Clear();

            //On recherche les personnes coorespondantes
            string connex = System.Configuration.ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);      //Chaine de connection récupérée dans le app.config

            //on ouvre la connexion
            dbConnection.Open();

            SqlCommand cmd = dbConnection.CreateCommand();
            cmd.Connection = dbConnection;       //On passe les parametres query et connection

            try
            {
                //on définit la requette                            
                string sqlstr0 = "SELECT pe.Idpersonne, pe.Nom + ' ' + pe.Prenom as personne, pe.DateNaissance, ta.IdAbonnement, ta.periodicite FROM TablePersonne pe, TablePatient tp, ta_Abonnement ta";
                sqlstr0 += " WHERE DateNaissance = @Rech";
                sqlstr0 += " AND pe.Idpersonne = tp.Idpersonne";
                sqlstr0 += " AND tp.TypeAbonnement = 'TA'";
                sqlstr0 += " AND tp.IdAbonnement = ta.IdAbonnement ";


                // Ajout des paramètres
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("Rech", DateNaissPatient);
                cmd.CommandText = sqlstr0;

                DtsRecherchePatient.Tables.Clear();
                DtsRecherchePatient.Tables.Add("RecherchePatient");
                DtsRecherchePatient.Tables[0].Load(cmd.ExecuteReader());       //on execute                           
            }
            catch (Exception a)
            {
                Console.WriteLine("Erreur : " + a.Message);
            }
            finally
            {
                //Fermeture de la connexion
                if (dbConnection.State == System.Data.ConnectionState.Open)
                    dbConnection.Close();
            }

            return (DtsRecherchePatient);
        }

        private void listBoxTA_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Déjà on regarde si on a pas cliqué dans le vide de la liste !!!!Débile ce controle            
            int index = listBoxTA.SelectedIndex;

            if (index != -1)
            {
                //On récupère l'id de la personne
                string[] Morceaux = listBoxTA.SelectedItem.ToString().Split('|');       //on coupe la chaine en fonction du séparateur |
                Idpersonne = int.Parse(Morceaux[2].TrimStart(' '));      //On récupère la derniere occurence en supprimant l'espace au début car "| xxx"

                string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
                SqlConnection dbConnection = new SqlConnection(connex);      //Chaine de connection récupérée dans le app.config

                //on ouvre la connexion
                dbConnection.Open();

                SqlCommand cmd = dbConnection.CreateCommand();
                cmd.Connection = dbConnection;       //On passe les parametres query et connection

                try
                {
                    //on définit la requette                                                    
                    string sqlstr0 = "SELECT pe.Idpersonne, pe.Prenom + ' ' + pe.Nom as personne, pe.DateNaissance, pe.Rue, pe.NumeroDansRue, pe.CodePostal, pe.Commune, ";
                    sqlstr0 += " CASE pe.Sexe ";
                    sqlstr0 += " WHEN 'H' THEN 'M.' ";
                    sqlstr0 += " WHEN 'F' THEN 'Mme' ";
                    sqlstr0 += " ELSE '' ";
                    sqlstr0 += " END AS Sexe, ta.IdAbonnement,  ";
                    sqlstr0 += " CASE";
                    sqlstr0 += " WHEN ta.periodicite  = 'A' OR ta.periodicite  = 'MeA' THEN 'annuellement'"; 
                    sqlstr0 += " WHEN ta.periodicite  = 'T' OR ta.periodicite  = 'MeT' THEN 'trimestriellement'";
                    sqlstr0 += " WHEN ta.periodicite  = 'M' OR ta.periodicite  = 'MeM' THEN 'mensuellement'";                                                           
                    sqlstr0 += " ELSE 'trimestriellement' ";
                    sqlstr0 += " END AS periodicite, ";
                    sqlstr0 += " CASE ";
                    sqlstr0 += " WHEN ta.periodicite  = 'A' OR ta.periodicite  = 'T' OR ta.periodicite  = 'M' THEN 'TéléAlarme'";
                    sqlstr0 += " WHEN ta.periodicite  = 'MeA' OR ta.periodicite  = 'MeT' OR ta.periodicite  = 'MeM' THEN 'Médicalerte'";
                    sqlstr0 += " ELSE 'TéléAlarme'";
                    sqlstr0 += " END AS TypeAbonnement"; 
                    sqlstr0 += " FROM TablePersonne pe, TablePatient tp, ta_Abonnement ta ";
                    sqlstr0 += " WHERE pe.Idpersonne = @Idpersonne ";
                    sqlstr0 += " AND pe.Idpersonne = tp.Idpersonne ";
                    sqlstr0 += " AND tp.TypeAbonnement = 'TA' ";
                    sqlstr0 += " AND tp.IdAbonnement = ta.IdAbonnement  ";


                    // Ajout des paramètres
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("Idpersonne", Idpersonne);
                    cmd.CommandText = sqlstr0;

                    DataTable dtPersonne = new DataTable();                   
                    dtPersonne.Load(cmd.ExecuteReader());       //on execute                           

                    if ((dtPersonne.Rows.Count >= 0) && (dtPersonne.Rows[0][0].ToString() != ""))
                    {
                        //On affecte les champs                                                    
                        IdAbonnementTA = dtPersonne.Rows[0]["IdAbonnement"].ToString();

                        string sexe = dtPersonne.Rows[0]["Sexe"].ToString();
                        string domicile = "";

                        if (sexe == "Mme")
                            domicile = ", domiciliée au ";
                        else domicile = ", domicilié au ";

                        string personne = dtPersonne.Rows[0]["personne"].ToString();
                        string Adr1 = dtPersonne.Rows[0]["Rue"].ToString() + " " + dtPersonne.Rows[0]["NumeroDansRue"].ToString();
                        string Adr2 = dtPersonne.Rows[0]["CodePostal"].ToString() + " " + dtPersonne.Rows[0]["Commune"].ToString();
                        string Periodicite = dtPersonne.Rows[0]["periodicite"].ToString();                        
                       
                        //Puis on calcule les montants payés pour la période
                        //Récup des périodes concernés
                        sqlstr0 = "SELECT Début_Période as Ddp, Fin_Période as Dfp, Tarif_mensuel FROM ta_factures";
                        sqlstr0 += " WHERE Idabonnement = @IdAbonnement";
                        sqlstr0 += " AND Acquité = 1";
                        sqlstr0 += " AND ((Début_Période < @dateD AND Fin_Période >= @DateD)";
                        sqlstr0 += "       OR (Début_Période <= @dateF AND Fin_Période >= @dateF)";
                        sqlstr0 += "       OR (Fin_Période >= @dateD AND Fin_Période < @datef))";

                        // Ajout des paramètres
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("IdAbonnement",dtPersonne.Rows[0]["IdAbonnement"].ToString());
                        cmd.Parameters.AddWithValue("dateD", DateDebut);
                        cmd.Parameters.AddWithValue("dateF", DateFin);
                        cmd.CommandText = sqlstr0;

                        DataTable dtFactures = new DataTable();                       
                        dtFactures.Load(cmd.ExecuteReader());       //on execute     
        
                        //Ainsi Que de le début de periode Min et fin de période Max
                        sqlstr0 = "SELECT Min(Début_Période) as Min, Max(Fin_Période) as Max FROM ta_factures";
                        sqlstr0 += " WHERE Idabonnement = @IdAbonnement";
                        sqlstr0 += " AND Acquité = 1";
                        sqlstr0 += " AND ((Début_Période < @dateD AND Fin_Période >= @DateD)";
                        sqlstr0 += "       OR (Début_Période <= @dateF AND Fin_Période >= @dateF)";
                        sqlstr0 += "       OR (Fin_Période >= @dateD AND Fin_Période < @datef))";

                        // Ajout des paramètres
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("IdAbonnement", dtPersonne.Rows[0]["IdAbonnement"].ToString());
                        cmd.Parameters.AddWithValue("dateD", DateDebut);
                        cmd.Parameters.AddWithValue("dateF", DateFin);
                        cmd.CommandText = sqlstr0;

                        DataTable dtExtrem = new DataTable();
                        dtExtrem.Load(cmd.ExecuteReader());       //on execute     

                        //Si on a au moins 1 enregistrement facture
                        if ((dtFactures.Rows.Count > 0) && (dtFactures.Rows[0][0].ToString() != ""))
                        {
                            int nbmoisTotal = 0;
                            int nbmois = 0;
                            Decimal montant = 0;
                            string DebutDePeriode = "";
                            string FinDePeriode = "";
                            
                            //On calcule le nombre de mois cotisés
                            for (int i = 0; i < dtFactures.Rows.Count; i++)
                            {
                                if (DateDebut >= DateTime.Parse(dtFactures.Rows[i]["Ddp"].ToString())
                                     && DateDebut <= DateTime.Parse(dtFactures.Rows[i]["Dfp"].ToString()))
                                {
                                    //Compter le nombre de mois (entre date de debut et date de fin periode)
                                    nbmois = GetMonthDifference(DateTime.Parse(dtFactures.Rows[i]["Dfp"].ToString()), DateDebut);
                                    nbmoisTotal = nbmoisTotal + nbmois;
                                    montant = montant + (nbmois * Convert.ToDecimal(dtFactures.Rows[i]["Tarif_mensuel"]));                                   
                                }
                                else if (DateFin >= DateTime.Parse(dtFactures.Rows[i]["Ddp"].ToString())
                                     && DateFin <= DateTime.Parse(dtFactures.Rows[i]["Dfp"].ToString()))
                                {
                                    //Compter le nombre de mois (entre date debut de periode et date fin)
                                    nbmois = GetMonthDifference(DateFin, DateTime.Parse(dtFactures.Rows[i]["Ddp"].ToString()));
                                    nbmoisTotal = nbmoisTotal + nbmois;
                                    montant = montant + (nbmois * Convert.ToDecimal(dtFactures.Rows[i]["Tarif_mensuel"]));                                   
                                }
                                else
                                {
                                    //Compter le nombre de mois (entre debut periode et fin periode)
                                    nbmois = GetMonthDifference(DateTime.Parse(dtFactures.Rows[i]["Dfp"].ToString()).AddDays(+1), DateTime.Parse(dtFactures.Rows[i]["Ddp"].ToString()));
                                    nbmoisTotal = nbmoisTotal + nbmois;
                                    montant = montant + (nbmois * Convert.ToDecimal(dtFactures.Rows[i]["Tarif_mensuel"]));
                                }
                            }

                            //Pour le début et fin de la période d'abonnement
                            if (DateTime.Parse(dtExtrem.Rows[0]["Min"].ToString()) <= DateDebut)
                                DebutDePeriode = "1er janvier au ";
                            else DebutDePeriode = "1er " + DateTime.Parse(dtExtrem.Rows[0]["Min"].ToString()).ToString("MMMM") + " au ";

                            if (DateTime.Parse(dtExtrem.Rows[0]["Max"].ToString()) >= DateFin)
                                FinDePeriode = DateFin.AddDays(-1).ToString("d MMMM yyyy");
                            else FinDePeriode = DateTime.Parse(dtExtrem.Rows[0]["Max"].ToString()).ToString("d MMMM yyyy");
                            
                            string MontantPeriode = "0";

                            switch (Periodicite)
                                {
                                    case "annuellement": MontantPeriode = Math.Round(Convert.ToDecimal(dtFactures.Rows[0]["Tarif_mensuel"]) * 12, 2).ToString("F"); break;   //F = format 0.00
                                    case "trimestriellement": MontantPeriode = Math.Round(Convert.ToDecimal(dtFactures.Rows[0]["Tarif_mensuel"]) * 3, 2).ToString("F"); break;
                                    case "mensuellement": MontantPeriode = Math.Round(Convert.ToDecimal(dtFactures.Rows[0]["Tarif_mensuel"]), 2).ToString("F"); break;
                                }

                            //Puis on parametre l'imression                    
                            //Attestation_TA attestationTA = new Attestation_TA();
                           
                            attestationTA.SetParameterValue("NomPatient", sexe + " " + personne);
                            attestationTA.SetParameterValue("Adr1", Adr1);
                            attestationTA.SetParameterValue("Adr2", Adr2);

                            //Pour le Type d'abonnement (TéléAlarme ou Médicalerte)
                            string TypeAbonnement = "";

                            if (dtPersonne.Rows[0]["TypeAbonnement"].ToString() == "TéléAlarme")
                                TypeAbonnement = "TéléAlarme";
                            else  //Medicalerte
                                TypeAbonnement = "Médicalerte";

                            attestationTA.SetParameterValue("Texte1", "Nous soussignés, SOS Médecins Cité Calvin SA, certifions que " + sexe + " " + personne + domicile + Adr1 + ", " + Adr2 +
                                                                ", a souscrit un abonnement " + TypeAbonnement + " du " + DebutDePeriode + FinDePeriode + ".");
                            attestationTA.SetParameterValue("Texte2", "Prestation facturée " + Periodicite + " pour un montant de CHF " + MontantPeriode + " :");                           
                            attestationTA.SetParameterValue("Texte3", "Montant annuel encaissé par nos soins: CHF " + montant.ToString("F"));
                            //attestationTA.SetParameterValue("LieuDate", "Fait à Genève, le " + DateTime.Today.ToLongDateString());
                            attestationTA.SetParameterValue("LieuDate", "Fait à Genève, le " + DateTime.Today.ToString("dddd dd MM yyyy"));

                            attestationTA.SetParameterValue("Signature", VariablesApplicatives.Utilisateurs.NomUtilisateur);

                            crystalReportViewer1.ReportSource = attestationTA;

                            crystalReportViewer1.Zoom(1);
                            crystalReportViewer1.Show();

                            //On active le bouton imprimer
                            bImprime.Enabled = true;
                        }
                        else
                        {
                            //Aucune facture Pour cette personne
                            MessageBox.Show("Il n'y a aucune facture pour cette personne");
                        }
                    }
                }
                catch (Exception a)
                {
                    Console.WriteLine("Erreur : " + a.Message);
                }
                finally
                {
                    //Fermeture de la connexion
                    if (dbConnection.State == System.Data.ConnectionState.Open)
                        dbConnection.Close();
                }
            }
        }

        //Fonction qui calcul le nombre de mois entre 2 dates 
        public static int GetMonthDifference(DateTime DateD, DateTime DateF)
        {
            int NombreDeMois = 12 * (DateD.Year - DateF.Year) + DateD.Month - DateF.Month;
            return Math.Abs(NombreDeMois);
        }

        private void bImprime_Click(object sender, EventArgs e)
        {
            //on ouvre la boite de dialogue imprimante                   
            printDialog1.PrinterSettings.Copies = 1;        //on veut 2 exemplaires
            printDialog1.Document = new System.Drawing.Printing.PrintDocument();

            DialogResult result = printDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                //recup des paramètres et on imprime                                             
                attestationTA.PrintOptions.PrinterName = printDialog1.PrinterSettings.PrinterName;      //retourne l'imprimante choisie    
                attestationTA.PrintOptions.CustomPaperSource = printDialog1.Document.DefaultPageSettings.PaperSource;   //Bac par défaut
                attestationTA.PrintToPrinter(printDialog1.PrinterSettings.Copies, true, 1, 1);  //On veut 1 exemplaires de la pages 1 à 1  
                
                //On met à jour le journal               
                OutilsExt.OutilsSql.InsereLigneJournal(int.Parse(IdAbonnementTA), "Attestation", "attest", "", DateTime.Today, VariablesApplicatives.Utilisateurs.NomUtilisateur.ToString(), "", "Envoyé Attestation " + DateTime.Now.AddYears(-1).Year.ToString() + " pour impôts");                 
            }
        }       

        private void bFermer_Click(object sender, EventArgs e)
        {
            Close();
        }
       

        
    }
}
