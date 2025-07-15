using Codecrete.SwissQRBill.Generator;
using CrystalDecisions.Shared;
using SosMedecins.SmartRapport.EtatsCrystal;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace ImportSosGeneve.Facture
{
    public partial class ImpressionDebiteurs : Form
    {
        public SosMedecins.SmartRapport.DAL.dstFactureImpr dsFacture = new SosMedecins.SmartRapport.DAL.dstFactureImpr();
        private string AssuranceEAN = "";
        private string NomFacture = "";
        private DataTable dtfacture = new DataTable();

        public ImpressionDebiteurs()
        {
            InitializeComponent();
            pictureBox1.Visible = false;
            label3.Text = "";
        }

        private void bGenerer_Click(object sender, EventArgs e)
        {
            //on initialise une progressBar
            progressBar1.Minimum = 0;
            progressBar1.Value = 0;

            //on modifie le label3
            label3.Text = "Récupération des données...";

            //Récup des factures impayés
            dtfacture.Clear();
            dtfacture = ChargeFactures();

            if (dtfacture.Rows.Count > 0)
            {
                //Il y a des factures donc on initialise la valeur max de la progressBar1
                label3.Text = "Récupération des données..." + dtfacture.Rows.Count + " factures à traiter.";
                progressBar1.Maximum = dtfacture.Rows.Count;
            }

            try
            {
                //PDF en passant par crystal report
                if (rB1.Checked)
                {
                    //Pour chaques factures on génère ou un xml ou un pdf
                    for (int i = 0; i < dtfacture.Rows.Count; i++)
                    {
                        //on avance la progressBar1
                        progressBar1.Value = i + 1;

                        dsFacture.Clear();
                        NomFacture = "Facture" + dtfacture.Rows[i]["NFacture"].ToString();
                        GenereFacture(int.Parse(dtfacture.Rows[i]["NFacture"].ToString()));
                    }

                    label3.Text = "Traitement des PDFs terminé";
                }
                else      //XML standard
                {
                    //Pour chaques factures on génère ou un xml ou un pdf
                    for (int i = 0; i < dtfacture.Rows.Count; i++)
                    {
                        //on avance la progressBar1
                        progressBar1.Value = i + 1;

                        dsFacture.Clear();
                        //On défini le nom de la facture
                        NomFacture = "Facture" + dtfacture.Rows[i]["NFacture"].ToString() + ".xml";
                        string chemin = SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.Path_XMLfactureImpayees;

                        FactureXML45 m_FacXml = new FactureXML45(OutilsExt.OutilsSql, dtfacture.Rows[i]["NFacture"].ToString(), chemin + NomFacture, 1);
                    }

                    label3.Text = "Traitement des XML terminé";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("erreur : " + ex.Message);
                pictureBox1.Visible = true;
                pictureBox1.Image = imageList1.Images[3];   //C'est pas ok
            }

            //Affichage de l'émoji
            pictureBox1.Visible = true;
            pictureBox1.Image = imageList1.Images[0];   //Tout est ok
        }


        private DataTable ChargeFactures()
        {
            DateTime DateDebut = dTPickerDeb.Value;
            DateTime DateFin = dTPickerFin.Value;

            //Pour la 1ere requete
            //DateTime DateDebCaissMed = DateTime.Parse("01.06.2019");
            //DateTime DateFinCaissMed = DateTime.Parse("17.12.2020");

            //DateTime DateDebMedi = DateTime.Parse("18.12.2020");
            //DateTime DateFinMedi = DateTime.Parse("01.03.2024");

            //DateTime DateDebut = DateTime.Parse("18.12.2020");
            //DateTime DateFin = DateTime.Parse("01.01.2024");

            //On ajoute une table
            DataTable dtFacture = new DataTable();

            //On affiche les factures
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);

            //On ouvre la connexion
            dbConnection.Open();

            SqlCommand cmd = dbConnection.CreateCommand();
            cmd.Connection = dbConnection;

            try
            {
                //Definition de la requete de l'entête
                /*string sqlStr0 = "SELECT tm.Nom as NomMED, CASE WHEN f.typeDestinataire = 2 ";
                sqlStr0 += "                                    THEN 'TP'";
                sqlStr0 += "                                    ELSE 'TG'";
                sqlStr0 += "                               END as Tier, CONVERT(VARCHAR, ta.DAP, 34) AS FacDateEnvoyee, f.NFacture,";
                sqlStr0 += " (f.TotalFacture - sum(fe.Montant)) as TotalFacture, f.Solde,";
                sqlStr0 += " (pe.Nom + ' ' + pe.prenom) as NomPatient, f.AdresseDestinataire, pe.Tel";
                sqlStr0 += " FROM facture f INNER JOIN facture_etats fe ON f.NFacture = fe.NFacture";
                sqlStr0 += "                INNER JOIN factureConsultation fc ON fc.NFacture = f.NFacture";
                sqlStr0 += "                INNER JOIN facture_status fs ON fs.NFacture = f.NFacture";
                sqlStr0 += "                INNER JOIN tableconsultations tc ON fc.NConsultation = tc.NConsultation";
                sqlStr0 += "                INNER JOIN tableactes ta ON tc.CodeAppel = ta.Num";
                sqlStr0 += "                INNER JOIN tablemedecin tm ON ta.CodeIntervenant = tm.CodeIntervenant";
                sqlStr0 += "                INNER JOIN tablepatient pa ON tc.IndicePatient = pa.IdPatient";
                sqlStr0 += "                INNER JOIN tablepersonne pe ON pa.IdPersonne = pe.IdPersonne";
                sqlStr0 += " WHERE fs.FacDateAnnulee is Null";
                sqlStr0 += " AND f.TotalFacture > 0";
                sqlStr0 += " AND f.Solde > 10";
                sqlStr0 += " AND f.NFacture not in (SELECT fact.NFacture FROM facture fact inner join facture_status fs1";
                sqlStr0 += "                                                               on fact.Nfacture = fs1.NFacture";
                sqlStr0 += "                     WHERE fact.TotalFacture = 0 AND fact.Solde <> 0 and fs1.FacDateAcquittee is null)";
                sqlStr0 += " AND(fs.FacDateAcquittee is null)";
                sqlStr0 += " AND fs.LimiteStopRappel is null";
                //sqlStr0 += " AND ta.DAP >= @DateDebCaissMed and ta.DAP <= @DateFinCaissMed";
                sqlStr0 += " AND fs.FacDateEnvoyee >= @DateDebCaissMed and fs.FacDateEnvoyee <= @DateFinCaissMed";
                sqlStr0 += " GROUP BY tm.Nom, f.typeDestinataire, f.NFacture, f.AdresseDestinataire, pe.Tel,f.TotalFacture, f.Solde, pe.Nom, pe.prenom,ta.DAP, pe.IdPersonne, pa.IdPatient";
                sqlStr0 += " UNION";*/
                string sqlStr0 = " SELECT tm.Nom as NomMED, CASE WHEN f.typeDestinataire = 2";                
                sqlStr0 += "                               THEN 'TP'";
                sqlStr0 += "                               ELSE 'TG'";
                sqlStr0 += "                               END as Tier, CONVERT(VARCHAR, ta.DAP, 34) AS FacDateEnvoyee, f.NFacture,";
                sqlStr0 += " (f.TotalFacture - sum(fe.Montant)) as TotalFacture, f.Solde,";
                sqlStr0 += " (pe.Nom + ' ' + pe.prenom) as NomPatient, f.AdresseDestinataire, pe.Tel";
                sqlStr0 += " FROM facture f INNER JOIN facture_etats fe ON f.NFacture = fe.NFacture";
                sqlStr0 += "                INNER JOIN factureConsultation fc ON fc.NFacture = f.NFacture";
                sqlStr0 += "                INNER JOIN facture_status fs ON fs.NFacture = f.NFacture";
                sqlStr0 += "                INNER JOIN tableconsultations tc ON fc.NConsultation = tc.NConsultation";
                sqlStr0 += "                INNER JOIN tableactes ta ON tc.CodeAppel = ta.Num";
                sqlStr0 += "                INNER JOIN tablemedecin tm ON ta.CodeIntervenant = tm.CodeIntervenant";
                sqlStr0 += "                INNER JOIN tablepatient pa ON tc.IndicePatient = pa.IdPatient";
                sqlStr0 += "                INNER JOIN tablepersonne pe ON pa.IdPersonne = pe.IdPersonne";               
                sqlStr0 += " WHERE fs.FacDateAnnulee is Null";
                sqlStr0 += " AND f.TotalFacture > 0";
                sqlStr0 += " AND f.Solde > 10";
                //sqlStr0 += " AND f.Solde = 0";
               // sqlStr0 += " AND tm.CodeIntervenant = 2937";

                sqlStr0 += " AND f.NFacture not in (SELECT fact.NFacture FROM facture fact inner join facture_status fs1";
                sqlStr0 += "                                                              on fact.Nfacture = fs1.NFacture";
                sqlStr0 += "                        WHERE fact.TotalFacture = 0 AND fact.Solde <> 0 and fs1.FacDateAcquittee is null)";
                sqlStr0 += " AND fs.FacDateAcquittee is null";
                sqlStr0 += " AND fs.LimiteStopRappel is null";
                sqlStr0 += " AND fs.FacDate2Rappel is not null";                
                //sqlStr0 += " AND ta.DAP >= @DateDebMedi and ta.DAP <= @DateFinMedi";
                //sqlStr0 += " AND ta.DAP >= @DateDebut and ta.DAP <= @DateFin";
                sqlStr0 += " AND fs.FacDateEnvoyee >= @DateDebut and fs.FacDateEnvoyee <= @DateFin";
                //sqlStr0 += " AND fs.FacDateEnvoyee >= @DateDebMedi and fs.FacDateEnvoyee <= @DateFinMedi";
                sqlStr0 += " GROUP BY tm.Nom, f.typeDestinataire, f.NFacture, f.AdresseDestinataire, pe.Tel,f.TotalFacture, f.Solde, pe.Nom, pe.prenom,ta.DAP, pe.IdPersonne, pa.IdPatient";
                sqlStr0 += " ORDER BY Tier desc, tm.Nom";

                cmd.CommandText = sqlStr0;

                //Ajout des paramètres
                cmd.Parameters.Clear();
                //cmd.Parameters.AddWithValue("DateDebCaissMed", DateDebCaissMed);
                //cmd.Parameters.AddWithValue("DateFinCaissMed", DateFinCaissMed);
                cmd.Parameters.AddWithValue("DateDebut", DateDebut);
                cmd.Parameters.AddWithValue("DateFin", DateFin);

                dtFacture.Load(cmd.ExecuteReader());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                dbConnection.Close();       //On ferme les connctions                                
                dbConnection = null;        //on remet à blanc la chaine de connection
            }

            return dtFacture;
        }

        //On génère un fichier pdf
        private void GenereFacture(int NumFacture)
        {
            PrepareFacture(NumFacture);

            EnregistreFacturePdf();
        }

        public void PrepareFacture(int NumFacture)
        {
            //On affiche la facture
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);

            //On ouvre la connexion
            dbConnection.Open();

            SqlCommand cmd = dbConnection.CreateCommand();
            cmd.Connection = dbConnection;

            try
            {
                //Definition de la requete de l'entête
                string sqlStr0 = " SELECT f.NFacture, c.NConsultation, pe.nom, pe.prenom, pe.Adm_Pays as Pays, pe.Adm_Commune as Commune, pe.Adm_Rue as Rue,";
                sqlStr0 += " pe.Adm_NumeroDansRue as NumeroDansRue, pe.DateNaissance, pe.Sexe, pe.Num_Assure, f.DateCreation, f.TypeEnvoi,";
                sqlStr0 += " f.Tarif, f.TTT, f.TypeAssurance, f.TypeSortie, f.NAccident, f.DateAccident, f.RefPatient, f.FlagConcerne, f.Commentaire,";
                sqlStr0 += " f.TotalFacture, f.Solde, f.TypeDestinataire, f.CodeDestinataire, f.AdresseDestinataire, f.AdresseDestinataire2,";
                sqlStr0 += " f.FactNum_AVS, TypeDocJoint, UrlDocJoint, m.CodeIntervenant, m.Nom as 'MedNom', m.NomGeneve, m.PrenomGeneve,";
                sqlStr0 += " m.EAN, m.Concordat, m.Independant, m.NIF, m.RCC, a.DSL, a.DFI, pe.Adm_CodePostal as CodePostal, ass.NAssurance, ass.AssEAN, ass.AssNom,";
                sqlStr0 += " ass.AssTelephone, ass.AssFax, adr.NumDansRue as AssNumDansRue, adr.Np as AssNp, adr.Rue as AssRue, adr.Commune as AssCommune,";
                sqlStr0 += " adr.Pays as AssPays, a.DAP, f.Num_Session, co.IndicePatient";
                sqlStr0 += " FROM facture f LEFT JOIN facture_status fs ON fs.NFacture = f.NFacture ";
                sqlStr0 += "                LEFT JOIN factureconsultation c ON c.NFacture = f.NFacture ";
                sqlStr0 += "                LEFT JOIN tableconsultations co ON co.NConsultation = c.NConsultation";
                sqlStr0 += "                LEFT JOIN tablepatient pa ON pa.IdPatient = co.IndicePatient ";
                sqlStr0 += "                LEFT JOIN tablepersonne pe ON pe.IdPersonne=  pa.IdPersonne";
                sqlStr0 += "                LEFT JOIN tableactes a ON co.CodeAppel = a.Num";
                sqlStr0 += "                LEFT JOIN tablemedecin m ON m.CodeIntervenant = a.CodeIntervenant";
                sqlStr0 += "                LEFT JOIN assurances ass ON f.CodeDestinataire = ass.NAssurance";
                sqlStr0 += "                LEFT JOIN adresses adr ON ass.NAdresse = adr.NAdresse";
                sqlStr0 += " WHERE f.NFacture = @NFacture";

                cmd.CommandText = sqlStr0;

                //Ajout des paramètres
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("NFacture", NumFacture);

                //On ajoute une table
                DataTable dtFacture = new DataTable();
                dtFacture.Load(cmd.ExecuteReader());

                //On charge l'entete de la facture
                ChargeEntete(dtFacture);

                DataTable dtLignes = LignePrestation(NumFacture);

                //Puis on les copie dans le DataSet dstFactureImpr
                CopieLignePrestations(dtLignes);

            }
            catch (Exception a)
            {
                Console.WriteLine("Erreur sql : " + a.Message);
            }
        }

        //Chargement de l'entete de la facture
        public void ChargeEntete(DataTable dtFacture)
        {
            //Chargement des lignes dans le DataSet dstFactureImpr
            string AdresseDest = "";

            //Pour le QRCode
            string Civilite = "";
            string Destinataire = "";
            string Adr1 = " ";
            string DestVille = " ";
            string DestCP = " ";
            string DestPays = " ";

            for (int i = 0; i < dtFacture.Rows.Count; i++)
            {
                string typeFact = "";

                //Ajout d'un nvl enregistrement vide
                DataRow row = dsFacture.Facture.NewRow();

                //on affecte les valeurs
                row["NFacture"] = Int64.Parse(dtFacture.Rows[i]["NFacture"].ToString());
                row["DateFacture"] = DateTime.Parse(dtFacture.Rows[i]["DateCreation"].ToString());

                //Le destinataire
                if (dtFacture.Rows[i]["TypeDestinataire"].ToString() != DBNull.Value.ToString() &&
                               int.Parse(dtFacture.Rows[i]["TypeDestinataire"].ToString()) == (int)CtrlDest.TypeDestinataire.Idem)
                {
                    row["CiviliteDestinataire"] = WorkedString.GetSexeFormate(dtFacture.Rows[i]["Sexe"].ToString());
                    Civilite = WorkedString.GetSexeFormate(dtFacture.Rows[i]["Sexe"].ToString());
                }
                else
                {
                    row["CiviliteDestinataire"] = "";
                    Civilite = "";
                }

                AdresseDest = dtFacture.Rows[i]["AdresseDestinataire"].ToString();

                string[] m_tabAdr = AdresseDest.Split('\n');

                if (m_tabAdr.Length > 1)
                {
                    row["NomDestinataire"] = m_tabAdr[0];
                    for (int k = 1; k < m_tabAdr.Length; k++)
                        row["AdresseDestinataire"] += m_tabAdr[k] + "\n";
                }
                else
                {
                    row["NomDestinataire"] = "";
                    row["AdresseDestinataire"] = m_tabAdr[0];
                }


                //N° EAN de l'assurance
                if (dtFacture.Rows[i]["AssEAN"] != DBNull.Value)
                {
                    AssuranceEAN = dtFacture.Rows[i]["AssEAN"].ToString();
                }

                //Informations du destinataire
                if (dtFacture.Rows[i]["NAssurance"].ToString() != null && dtFacture.Rows[i]["NAssurance"].ToString() != "0")
                {
                    Destinataire = dtFacture.Rows[i]["AssNom"].ToString();
                    Adr1 = dtFacture.Rows[i]["AssRue"].ToString();
                    DestCP = dtFacture.Rows[i]["AssNp"].ToString();
                    DestVille = dtFacture.Rows[i]["AssCommune"].ToString();
                    DestPays = dtFacture.Rows[i]["AssPays"].ToString();
                    if (DestPays == "" || DestPays == " " || DestPays.Length > 2)
                        DestPays = "CH";
                }
                else   //C'est une personne
                {
                    Destinataire = Civilite + " " + dtFacture.Rows[i]["Nom"].ToString() + " " + dtFacture.Rows[i]["Prenom"].ToString();
                    Adr1 = dtFacture.Rows[i]["Rue"].ToString() + " " + dtFacture.Rows[i]["NumeroDansRue"].ToString();
                    DestCP = dtFacture.Rows[i]["CodePostal"].ToString();
                    DestVille = dtFacture.Rows[i]["Commune"].ToString();
                    DestPays = dtFacture.Rows[i]["Pays"].ToString();
                    if (DestPays == "" || DestPays == " " || DestPays.Length > 2)
                        DestPays = "CH";
                };


                //********** Informations du médecin ********               
                //Sinon c'est le concordat de SOS médecin

                if (dtFacture.Rows[i]["Concordat"] != DBNull.Value && dtFacture.Rows[i]["Concordat"].ToString() != "")
                    row["NConcordat"] = "Concordat : " + dtFacture.Rows[i]["Concordat"].ToString();
                else
                    row["NConcordat"] = "";


                row["StrMedecin"] = "Dr " + WorkedString.FormatePreNom(dtFacture.Rows[i]["NomGeneve"].ToString()) + " "
                                                        + WorkedString.FormateNom(dtFacture.Rows[i]["PrenomGeneve"].ToString());

                if (dtFacture.Rows[i]["EAN"] != DBNull.Value)
                {
                    row["EANMedecin"] = "Four. de prestation: " + dtFacture.Rows[i]["EAN"].ToString();
                }
                else
                    row["EANMedecin"] = "";

                //****************************Fin infos médecin ***********************

                //Informations sur la facture:
                row["StrPlus3"] = dtFacture.Rows[i]["NFacture"].ToString() + "/" + dtFacture.Rows[i]["NConsultation"].ToString() + "/"
                                                + dtFacture.Rows[i]["CodeIntervenant"].ToString();

                if (dtFacture.Rows[i]["Sexe"] != DBNull.Value)
                    row["CivilitePatient"] = WorkedString.GetSexeFormate(dtFacture.Rows[i]["Sexe"].ToString()) + " ";

                row["Concerne"] = WorkedString.GetSexeFormate(dtFacture.Rows[i]["Sexe"].ToString()) + " " +
                                                WorkedString.FormatePreNom(dtFacture.Rows[i]["prenom"].ToString()) + " " +
                                                WorkedString.FormateNom(dtFacture.Rows[i]["nom"].ToString());

                if (dtFacture.Rows[i]["DateNaissance"].ToString() != "")
                    row["Concerne"] += ", " + DateTime.Parse(dtFacture.Rows[i]["DateNaissance"].ToString()).Year;

                // Si c'est un accident , 
                if (dtFacture.Rows[i]["NAccident"] != DBNull.Value)
                    row["StrPlus2"] = "Accident n° " + dtFacture.Rows[i]["NAccident"].ToString();

                //Mise en place des compliments
                string strDate = "";
                if (dtFacture.Rows[i]["DSL"] != DBNull.Value)
                    strDate = DateTime.Parse(dtFacture.Rows[i]["DSL"].ToString()).ToShortDateString();
                else
                    strDate = DateTime.Parse(dtFacture.Rows[i]["DAP"].ToString()).ToShortDateString();

                row["Compliments"] = "Le docteur " + dtFacture.Rows[i]["NomGeneve"].ToString() +
                                                   " vous présente ses compliments et vous adresse sa note d'honoraires\n\rpour le traitement (" +
                                                   WorkedString.GetTTTFormatte(int.Parse(dtFacture.Rows[i]["TTT"].ToString())) + ") du " +
                                                   strDate + ", en remplacement de votre médecin traitant.";

                //Affichage du total de la facture
                double Total = 0.00;
                if (dtFacture.Rows[i]["TotalFacture"] != DBNull.Value)
                    Total += double.Parse(dtFacture.Rows[i]["TotalFacture"].ToString());

                row["StrTotal"] = "Total :   CHF         " + Math.Round(Total, 2).ToString("0.00");

                //Numéro en bas du bordereau + Numéro de référence :
                double dmontant = Total * 100;
                long lmontant = (long)dmontant;


                if (typeFact == "" || typeFact != "10")
                {
                    FactureXML45 MyFact = new FactureXML45(OutilsExt.OutilsSql, dtFacture.Rows[i]["NFacture"].ToString(), "Buffer", "Printer");

                    //formattage des montants:                   
                    row["Montant1"] = Math.Truncate(Total);
                    row["Montant2"] = Math.Truncate((Total - Math.Truncate(Total)) * 100);
                    row["TotalFacture"] = Total;

                }
                else
                {
                    FactureXML45 MyFact = new FactureXML45(OutilsExt.OutilsSql, dtFacture.Rows[i]["NFacture"].ToString(), "Buffer", "Printer");

                    //formattage des montants : 
                    //float Total10 = Total / 10;
                    float Total10 = 0;
                    if (dtFacture.Rows[i]["Solde"] != DBNull.Value)
                        Total10 = Single.Parse(dtFacture.Rows[i]["TotalFacture"].ToString());

                    row["Montant1"] = Math.Truncate(Total10);
                    row["Montant2"] = Math.Truncate((Total10 - Math.Truncate(Total10)) * 100);
                    row["TotalFacture"] = Total;
                }

                //Edite les éléments pour créer le Coding Line & Reference Number
                string reference_number = CalculRefNumber(dtFacture.Rows[i]["NFacture"].ToString(), dtFacture.Rows[i]["CodeIntervenant"].ToString());

                string MontantTotal = row["Montant1"].ToString() + "." + row["Montant2"].ToString();

                string Coding_Line = CodingLine(reference_number, MontantTotal);

                row["StrCode1"] = reference_number;
                row["StrCode2"] = Coding_Line;

                //Si c'est envoi par email, on affiche le logo
                if (dtFacture.Rows[i]["TypeSortie"].ToString() == "Email")
                {
                    row["StrPlus6"] = "Affiche Logo";   //Champs du Dts pour afficher/cacher le logo                                       
                }
                else   //On envoi pas par Email...Donc pas de logo et pas de payé...Pour l'instant....
                {
                    row["StrPlus6"] = "";
                }


                //On passe les paramettre du bulletin (dont le QRCode)
                Bill bill = new Bill
                {
                    //Créditeur
                    Account = "CH7630000002120014992",
                    Creditor = new Address
                    {
                        Name = "Sos Medecins Cite Calvin SA",
                        AddressLine1 = "Rue Louis Favre, 43",
                        AddressLine2 = "1201 Genève",
                        CountryCode = "CH"
                    },

                    //Paiement
                    Amount = decimal.Parse(MontantTotal),
                    Currency = "CHF",

                    //Débiteur
                    Debtor = new Address
                    {
                        Name = Destinataire,
                        AddressLine1 = Adr1,
                        AddressLine2 = DestCP + " " + DestVille,
                        CountryCode = DestPays
                    },

                    //Référence du paiement
                    Reference = reference_number.Replace(" ", ""),
                    UnstructuredMessage = "",


                    //On défini le format de sortie, ici png, et suelement le QRCode, pas le bulletin
                    Format = new BillFormat
                    {
                        Language = Language.FR,
                        GraphicsFormat = GraphicsFormat.PNG,
                        OutputSize = OutputSize.QrCodeOnly
                    }
                };


                //Génération de la facture dans un [] de byte que l'on met dans StrPlus7
                try
                {
                    byte[] QRCodeRetour = QRBill.Generate(bill);
                    row["StrPlus7"] = QRCodeRetour;
                }
                catch (Exception e)
                {
                    MessageBox.Show("Erreur dans le QRCode : " + e.Message);
                }

                //On ajoute l'enregistrement
                dsFacture.Facture.Rows.Add(row);
            }  //Fin boucle for
        }

        //Les Services (Lignes de la facture)
        public DataTable LignePrestation(long NumFacture)
        {
            // Déclaration des variables                    
            double unit_mt = 0;                 //Point tarifaire de la prestation médicale
            double unit_factor_mt = 0;          //Valeur du point de la prestation médicale
            double scale_factor_mt = 0;         //Facteur scalaire interne de la prestation médicale  Domi 06.10.2017
            double unit_amount_mt = 0;          //Montant de la position tarifaire de la prestation médicale
            double unit_tt = 0;                 //Point tarifaire de la prestation technique
            double unit_factor_tt = 0;          //Valeur du point de la prestation technique
            double scale_factor_tt = 0;         //Facteur scalaire interne de la prestation technique
            double unit_amount_tt = 0;          //Montant de la position tarifaire de la prestation technique

            double amount = 0;                  //Montant de la position : Médicale + technique
            double quantite;
            string code;
            string Libelle;

            DataTable dtLigne = new DataTable();
            dtLigne.Columns.Add("NumFacture", Type.GetType("System.Int64"));
            dtLigne.Columns.Add("session", Type.GetType("System.Int16"));
            dtLigne.Columns.Add("tariff_type", Type.GetType("System.String"));
            dtLigne.Columns.Add("unit_mt", Type.GetType("System.Decimal"));
            dtLigne.Columns.Add("unit_factor_mt", Type.GetType("System.Decimal"));
            dtLigne.Columns.Add("scale_factor_mt", Type.GetType("System.Decimal"));
            dtLigne.Columns.Add("amount_mt", Type.GetType("System.Decimal"));
            dtLigne.Columns.Add("unit_tt", Type.GetType("System.Decimal"));
            dtLigne.Columns.Add("unit_factor_tt", Type.GetType("System.Decimal"));
            dtLigne.Columns.Add("scale_factor_tt", Type.GetType("System.Decimal"));
            dtLigne.Columns.Add("amount_tt", Type.GetType("System.Decimal"));
            dtLigne.Columns.Add("amount", Type.GetType("System.Decimal"));
            dtLigne.Columns.Add("record_id", Type.GetType("System.Int16"));
            dtLigne.Columns.Add("date_begin", Type.GetType("System.DateTime"));
            dtLigne.Columns.Add("code", Type.GetType("System.String"));
            dtLigne.Columns.Add("quantity", Type.GetType("System.Int16"));
            dtLigne.Columns.Add("name", Type.GetType("System.String"));
            dtLigne.Columns.Add("ref_code", Type.GetType("System.String"));


            //formatage de la date de la facture (yyyy-mm-ddT00:00:00)
            // DateTime datedebut = DateTime.Parse(String.Format("{0:d/M/yyyy}", DateFacture));
            // String datedebutF = String.Format("{0:s}", datedebut);

            // Recupere les données de la requete           
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);      //Chaine de connection récupérée dans le app.config

            try
            {
                //on ouvre la connexion
                dbConnection.Open();

                SqlCommand Query0 = dbConnection.CreateCommand();
                Query0.Connection = dbConnection;       //On passe les parametres query et connection

                //On determine le type de facture pour savoir quel tarmed utiliser, ainsi que le coeff en fonction du Médecin(Lamal ou autre)               
                string sqlstr0 = @"SELECT CASE WHEN MedInterne = 0 
                                THEN CASE WHEN f.Tarif = 5 and (f.TTT = 1 OR f.TTT = 2)
                                          THEN '1'
                                          ELSE CASE WHEN ta.DAP < '01.01.2018' 
                                                    THEN '1' 
                                                    ELSE '0.93' END
                                          END
                                ELSE '1' END AS Coefficient,
                           CASE WHEN ta.DAP > '31.12.2017' 
                                THEN CASE WHEN f.Tarif = 5 and (f.TTT = 1 OR f.TTT = 2)
                                          THEN 'LAA-AM-AI' 
                                          ELSE 'LAMAL' END                                                         
                                ELSE 'LAA-AM-AI' END AS TypeTarmed, ta.DAP, f.Num_Session                 
                     FROM facture f, factureconsultation fc, tableconsultations tc, tableactes ta, tablemedecin tm
                     WHERE f.NFacture = fc.NFacture
                     AND fc.NConsultation = tc.NConsultation
                     AND tc.CodeAppel = ta.Num
                     AND tm.CodeIntervenant = ta.CodeIntervenant
                     AND f.NFacture =" + NumFacture;

                Query0.CommandText = sqlstr0;

                DataSet DSResult = new DataSet();
                DSResult.Tables.Add("Coeff");      //on déclare une table pour cet ensemble de donnée
                DSResult.Tables["Coeff"].Load(Query0.ExecuteReader());       //on execute

                String TypeTarmed = "LAMAL";

                //En fonction des résultats, On choisi le Tarmed à utiliser et le coeff à appliquer
                //On affiche le premier enregistrement               
                if (DSResult.Tables["Coeff"].Rows.Count > 0)
                {
                    scale_factor_mt = Convert.ToDouble(DSResult.Tables["Coeff"].Rows[0]["Coefficient"].ToString());
                    TypeTarmed = DSResult.Tables["Coeff"].Rows[0]["TypeTarmed"].ToString();
                }
                else
                {
                    scale_factor_mt = 0.93;   //Si ça n'a rien renvoyé, on prend 0.93 par défaut
                }

                //Pour la partie technique....pour l'instant 1
                scale_factor_tt = 1;

                //on définit la requete   
                DateTime DAP = DateTime.Parse(DSResult.Tables["Coeff"].Rows[0]["DAP"].ToString());

                if (DAP < DateTime.Parse("01.01.2018"))
                {
                    sqlstr0 = "SELECT * FROM facture_prest fp ";
                    sqlstr0 += "        left join (SELECT * FROM Tarmed  ";
                    sqlstr0 += "                    WHERE DateDebut <= '" + DAP + "'";
                    sqlstr0 += "                          AND isNull(DateFin,'01.01.2030') >= '" + DAP + "'";
                    sqlstr0 += "                          AND TarmedVersion = 'LAA-AM-AI' ) AS t on fp.indice = t.Nprestation ";
                    sqlstr0 += "        left join fac_tarif ft on ft.id = fp.TypeTarif ";
                    sqlstr0 += "        left join fac_tablemateriel fm on fm.Nt_materiel = fp.Indice ";
                    sqlstr0 += " WHERE fp.NFacture =  " + NumFacture;
                    sqlstr0 += " ORDER BY fp.Ordre";
                }
                else
                {
                    if (TypeTarmed == "LAA-AM-AI")
                    {
                        sqlstr0 = "SELECT * FROM facture_prest fp ";
                        sqlstr0 += "        left join (SELECT * FROM Tarmed  ";
                        sqlstr0 += "                    WHERE DateDebut <= '" + DAP + "'";
                        sqlstr0 += "                          AND isNull(DateFin,'31.12.2017') = '31.12.2017'";
                        sqlstr0 += "                          AND TarmedVersion = 'LAA-AM-AI' ) AS t on fp.indice = t.Nprestation ";
                        sqlstr0 += "        left join fac_tarif ft on ft.id = fp.TypeTarif ";
                        sqlstr0 += "        left join fac_tablemateriel fm on fm.Nt_materiel = fp.Indice ";
                        sqlstr0 += " WHERE fp.NFacture =  " + NumFacture;
                        sqlstr0 += " ORDER BY fp.Ordre";
                    }
                    else
                    {
                        sqlstr0 = "SELECT * FROM facture_prest fp ";
                        sqlstr0 += "        left join (SELECT * FROM Tarmed  ";
                        sqlstr0 += "                    WHERE DateDebut <= '" + DAP + "'";
                        sqlstr0 += "                          AND isNull(DateFin,'01.01.2030') >= '" + DAP + "'";
                        sqlstr0 += "                          AND TarmedVersion = 'LAMAL' ) AS t on fp.indice = t.Nprestation ";
                        sqlstr0 += "        left join fac_tarif ft on ft.id = fp.TypeTarif ";
                        sqlstr0 += "        left join fac_tablemateriel fm on fm.Nt_materiel = fp.Indice ";
                        sqlstr0 += " WHERE fp.NFacture =  " + NumFacture;
                        sqlstr0 += " ORDER BY fp.Ordre";
                    }
                }

                Query0.CommandText = sqlstr0;

                DSResult.Tables.Add("LignesPrestations");      //on déclare une table pour cet ensemble de donnée
                DSResult.Tables["LignesPrestations"].Load(Query0.ExecuteReader());       //on execute

                //On affiche le premier enregistrement
                if (DSResult.Tables["LignesPrestations"].Rows.Count > 0)
                {
                    int i = 1;
                    //int a = 1;
                    string TypeP;

                    foreach (DataRow row in DSResult.Tables["LignesPrestations"].Rows)
                    {
                        // Pour distinguer les types de prestation ( tarmed 1 ou pharmacie 2)
                        TypeP = row["TypePrest"].ToString();

                        if (TypeP == "1")
                        {
                            code = row["Indice"].ToString();

                            //Si c'est une majoration en %
                            if (code == "00.2530" || code == "00.2550" || code == "00.2570" || code == "00.2590")
                            {
                                //Pour chaque enregistrements majorable, on écrit une ligne!
                                foreach (DataRow row1 in DSResult.Tables["LignesPrestations"].Select("PrestHorsMajor = 0"))
                                {
                                    //on prend les valeurs de la position à majorer (unit_mt * scale_factor_mt = unit_mt Major), 
                                    //mais le prixpoint de la position majoration en tant que scale_factor_mt

                                    quantite = Math.Round(Convert.ToDouble(row1["Qte"].ToString()), 2);
                                    unit_mt = Math.Round(Convert.ToDouble(row1["Points"].ToString()), 2);
                                    unit_factor_mt = Math.Round(Convert.ToDouble(row1["PrixPoint"].ToString()), 2);

                                    unit_amount_mt = unit_mt * quantite * scale_factor_mt;    //!!!
                                    unit_amount_mt = Math.Round(unit_amount_mt, 2);

                                    double scale_factor_mt_Maj = Math.Round(Convert.ToDouble(row["PrestPointM"].ToString()), 2);

                                    if (row1["PrestPointT"] != DBNull.Value)
                                        unit_tt = Math.Round(Convert.ToDouble(row1["PrestPointT"].ToString()), 2);
                                    else
                                        unit_tt = 0.00;

                                    unit_factor_tt = Math.Round(Convert.ToDouble(row1["PrixPoint"].ToString()), 2); ;
                                    scale_factor_tt = 0;    //!!!

                                    //unit_amount_tt = unit_tt * quantite * unit_factor_tt * scale_factor_tt;
                                    //unit_amount_tt = Math.Round(unit_amount_tt, 2);

                                    amount = Math.Round((unit_amount_mt * unit_factor_mt * scale_factor_mt_Maj), 2);

                                    //vat_rate = 0;
                                    Libelle = row["PrestLibelle"].ToString();

                                    Libelle = Libelle.Replace("é", "e");
                                    Libelle = Libelle.Replace("è", "e");
                                    Libelle = Libelle.Replace("ô", "o");
                                    Libelle = Libelle.Replace("â", "a");

                                    //Ajout d'une ligne dans la table
                                    DataRow dtLigneRow = dtLigne.NewRow();

                                    dtLigneRow["NumFacture"] = NumFacture;
                                    dtLigneRow["session"] = int.Parse(DSResult.Tables["Coeff"].Rows[0]["Num_Session"].ToString());
                                    dtLigneRow["tariff_type"] = "001";
                                    dtLigneRow["unit_mt"] = unit_amount_mt.ToString();  //!!!
                                    dtLigneRow["unit_factor_mt"] = unit_factor_mt.ToString();
                                    dtLigneRow["scale_factor_mt"] = scale_factor_mt_Maj.ToString();
                                    dtLigneRow["amount_mt"] = amount.ToString();
                                    dtLigneRow["unit_tt"] = unit_tt.ToString();
                                    dtLigneRow["unit_factor_tt"] = unit_factor_tt.ToString();
                                    dtLigneRow["scale_factor_tt"] = scale_factor_tt.ToString();
                                    dtLigneRow["amount_tt"] = unit_amount_tt.ToString();
                                    dtLigneRow["amount"] = amount.ToString();
                                    dtLigneRow["record_id"] = i.ToString();
                                    dtLigneRow["date_begin"] = DAP;
                                    dtLigneRow["code"] = code;
                                    dtLigneRow["quantity"] = row1["Qte"].ToString();    //Qté de la position à majorer...Pas la Qté de la position majoration!!!
                                    dtLigneRow["name"] = Libelle;
                                    dtLigneRow["ref_code"] = row1["Indice"].ToString();

                                    //Puis on ajoute la ligne dans la table
                                    dtLigne.Rows.Add(dtLigneRow);

                                    i++;
                                }
                            }
                            else
                            {
                                quantite = Math.Round(Convert.ToDouble(row["Qte"].ToString()), 2);

                                unit_mt = Math.Round(Convert.ToDouble(row["Points"].ToString()), 2);
                                unit_factor_mt = Math.Round(Convert.ToDouble(row["PrixPoint"].ToString()), 2);
                                // scale_factor_mt = 1;
                                unit_amount_mt = unit_mt * quantite * unit_factor_mt * scale_factor_mt;
                                unit_amount_mt = Math.Round(unit_amount_mt, 2);

                                if (row["PrestPointT"] != DBNull.Value)
                                    unit_tt = Math.Round(Convert.ToDouble(row["PrestPointT"].ToString()), 2);
                                else
                                    unit_tt = 0.00;

                                unit_factor_tt = Math.Round(Convert.ToDouble(row["PrixPoint"].ToString()), 2); ;
                                scale_factor_tt = 1;
                                unit_amount_tt = unit_tt * quantite * unit_factor_tt * scale_factor_tt;
                                unit_amount_tt = Math.Round(unit_amount_tt, 2);

                                amount = Math.Round((unit_amount_mt + unit_amount_tt), 2);

                                //vat_rate = 0;
                                Libelle = row["PrestLibelle"].ToString();

                                Libelle = Libelle.Replace("é", "e");
                                Libelle = Libelle.Replace("è", "e");
                                Libelle = Libelle.Replace("ô", "o");
                                Libelle = Libelle.Replace("â", "a");

                                //Ajout d'une ligne dans la table
                                DataRow dtLigneRow = dtLigne.NewRow();

                                dtLigneRow["NumFacture"] = NumFacture;
                                dtLigneRow["session"] = int.Parse(DSResult.Tables["Coeff"].Rows[0]["Num_Session"].ToString());
                                dtLigneRow["tariff_type"] = "001";
                                dtLigneRow["unit_mt"] = unit_mt.ToString();
                                dtLigneRow["unit_factor_mt"] = unit_factor_mt.ToString();
                                dtLigneRow["scale_factor_mt"] = scale_factor_mt.ToString();
                                dtLigneRow["amount_mt"] = unit_amount_mt.ToString();
                                dtLigneRow["unit_tt"] = unit_tt.ToString();
                                dtLigneRow["unit_factor_tt"] = unit_factor_tt.ToString();
                                dtLigneRow["scale_factor_tt"] = scale_factor_tt.ToString();
                                dtLigneRow["amount_tt"] = unit_amount_tt.ToString();
                                dtLigneRow["amount"] = amount.ToString();
                                dtLigneRow["record_id"] = i.ToString();
                                dtLigneRow["code"] = code;
                                dtLigneRow["quantity"] = row["Qte"].ToString();
                                dtLigneRow["name"] = Libelle;
                                dtLigneRow["ref_code"] = row["Indice"].ToString();

                                //Si ce sont des postions du sous chapitre 00.06, on met la date J+1 de la consult, PAS celle de la consult
                                //SAUF pour Swica (....encore elle!) et KPT/CPT
                                if (code == "00.2285" || code == "00.2295")
                                {
                                    if (AssuranceEAN != "7601003002041" && AssuranceEAN != "7601003000382")     //!= de Swica, KPT/CPT
                                    {
                                        DateTime DateDAPJPlus1 = DateTime.Parse(String.Format("{0:d/M/yyyy}", DAP)).AddDays(+1);
                                        String DateDAPjplus1 = String.Format("{0:s}", DateDAPJPlus1);
                                        dtLigneRow["date_begin"] = DateDAPjplus1;
                                    }
                                    else dtLigneRow["date_begin"] = DAP;
                                }
                                else dtLigneRow["date_begin"] = DAP;

                                //Puis on ajoute la ligne dans la table
                                dtLigne.Rows.Add(dtLigneRow);

                                i++;
                            }
                        }
                        else if (TypeP == "2")
                        {
                            if (row["MatPharmacode"].ToString() == "")
                                //code = row["Indice"].ToString();
                                code = row["Num_Materiel"].ToString();
                            else
                                code = row["MatPharmacode"].ToString();

                            quantite = Convert.ToDouble(row["Qte"].ToString());

                            unit_mt = Math.Round(Convert.ToDouble(row["MatPrix"].ToString()), 2);
                            unit_factor_mt = 1;
                            unit_amount_mt = unit_mt * quantite * unit_factor_mt;
                            //vat_rate = 0;

                            Libelle = row["MatLibelle"].ToString();

                            Libelle = Libelle.Replace("é", "e");
                            Libelle = Libelle.Replace("è", "e");
                            Libelle = Libelle.Replace("ô", "o");
                            Libelle = Libelle.Replace("â", "a");

                            //Ajout d'une ligne dans la table
                            DataRow dtLigneRow = dtLigne.NewRow();

                            dtLigneRow["NumFacture"] = NumFacture;
                            dtLigneRow["session"] = int.Parse(DSResult.Tables["Coeff"].Rows[0]["Num_Session"].ToString());
                            dtLigneRow["tariff_type"] = row["MatCommentaire"].ToString();
                            dtLigneRow["unit_mt"] = row["MatPrix"].ToString();
                            dtLigneRow["unit_factor_mt"] = 1;
                            dtLigneRow["scale_factor_mt"] = 1;
                            dtLigneRow["amount_mt"] = 0;
                            dtLigneRow["unit_tt"] = 0;
                            dtLigneRow["unit_factor_tt"] = 0;
                            dtLigneRow["scale_factor_tt"] = 0;
                            dtLigneRow["amount_tt"] = 0;
                            dtLigneRow["amount"] = unit_amount_mt.ToString();
                            dtLigneRow["date_begin"] = DAP;
                            dtLigneRow["record_id"] = i.ToString();
                            dtLigneRow["code"] = code;
                            dtLigneRow["quantity"] = row["Qte"].ToString();
                            dtLigneRow["name"] = Libelle;
                            dtLigneRow["ref_code"] = row["Num_Materiel"].ToString();

                            //Puis on ajoute la ligne dans la table
                            dtLigne.Rows.Add(dtLigneRow);

                            i++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                dbConnection.Close();       //On ferme les connctions                                
                dbConnection = null;        //on remet à blanc la chaine de connection
            }

            return dtLigne;
        }



        //Copie chaque lignes de prestations du DataTable dans le DataSet dstFactureImpr, table LignesPretations
        public void CopieLignePrestations(DataTable dtLignes)
        {
            for (int i = 0; i < dtLignes.Rows.Count; i++)
            {
                //Ajout d'un nvl enregistrment vide
                DataRow row = dsFacture.LignesPrestations.NewRow();

                row["NumFacture"] = Int64.Parse(dtLignes.Rows[i]["NumFacture"].ToString());
                row["Session"] = short.Parse(dtLignes.Rows[i]["session"].ToString());
                row["tariff_type"] = dtLignes.Rows[i]["tariff_type"].ToString();
                row["unit_mt"] = Decimal.Parse(dtLignes.Rows[i]["unit_mt"].ToString());
                row["unit_factor_mt"] = Decimal.Parse(dtLignes.Rows[i]["unit_factor_mt"].ToString());
                row["scale_factor_mt"] = Decimal.Parse(dtLignes.Rows[i]["scale_factor_mt"].ToString());
                row["amount_mt"] = Decimal.Parse(dtLignes.Rows[i]["amount_mt"].ToString());
                row["unit_tt"] = Decimal.Parse(dtLignes.Rows[i]["unit_tt"].ToString());
                row["unit_factor_tt"] = Decimal.Parse(dtLignes.Rows[i]["unit_factor_tt"].ToString());
                row["scale_factor_tt"] = Decimal.Parse(dtLignes.Rows[i]["scale_factor_tt"].ToString());
                row["amount_tt"] = Decimal.Parse(dtLignes.Rows[i]["amount_tt"].ToString());
                row["amount"] = Decimal.Parse(dtLignes.Rows[i]["amount"].ToString());
                row["date_begin"] = DateTime.Parse(dtLignes.Rows[i]["date_begin"].ToString());
                row["record_id"] = short.Parse(dtLignes.Rows[i]["record_id"].ToString());
                row["code"] = dtLignes.Rows[i]["code"].ToString();
                row["quantity"] = short.Parse(dtLignes.Rows[i]["quantity"].ToString());
                row["name"] = dtLignes.Rows[i]["name"].ToString();
                row["ref_code"] = dtLignes.Rows[i]["ref_code"].ToString();

                //On ajoute l'enregistrement
                dsFacture.LignesPrestations.Rows.Add(row);
            }
        }


        public void EnregistreFacturePdf()
        {
            //On passe les paramètres à l'état crystal
            string Imprimante = SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.StrInvoicePrinter;
            try
            {
                //En fonction du type de facture, on choisi l'état
                FactureBVR_QR rptFacture = new FactureBVR_QR();

                rptFacture.SetDataSource(dsFacture);
                //crystalReportViewer1.ReportSource = rptFacture;
                rptFacture.PrintOptions.PrinterName = Imprimante;
                //rptFacture.ExportToDisk(ExportFormatType.PortableDocFormat, "Facture" + dsFacture.Facture[0]["NFacture"].ToString());

                exportReport(rptFacture, NomFacture, "pdf");
                rptFacture.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur export : " + e.Message);
            }
            //crystalReportViewer1.Show();
            //crystalReportViewer1.Zoom(2);
        }

        //###################################### TRAITEMENT DE LA LIGNE DE CODAGE #################################
        // Methode pour completer les chaines avec des 0 ( exemple 135 donnera 00135 si longueur est à 5)
        private String Complete(String Chaine, int longueur)
        {
            int nbCara = longueur - Chaine.Length;
            String ChaineFinale = "";
            if (nbCara >= 0)
            {
                for (int i = 1; i < nbCara + 1; i++)
                {
                    ChaineFinale = ChaineFinale + "0";
                }
                ChaineFinale = ChaineFinale + Chaine;
            }
            return ChaineFinale;
        }


        static public string Modulo10(string P_serie)
        {
            int[][] tableau = new int[10][];

            for (int t = 0; t < 10; t++)
                tableau[t] = new int[10];

            int k = P_serie.Length + 1;
            int[] report = new int[k];

            report[0] = 0;

            tableau[0][0] = 0;
            tableau[0][1] = 9;
            tableau[0][2] = 4;
            tableau[0][3] = 6;
            tableau[0][4] = 8;
            tableau[0][5] = 2;
            tableau[0][6] = 7;
            tableau[0][7] = 1;
            tableau[0][8] = 3;
            tableau[0][9] = 5;

            for (int i = 1; i < 10; i++)
                for (int j = 0; j < 10; j++)
                {
                    tableau[i][j] = tableau[i - 1][(j + 1) % 10];
                }

            for (int c = 0; c < k - 1; c++)
            {
                int chiffre = Convert.ToInt32(String.Format("{0}", P_serie[c]));
                report[c + 1] = tableau[report[c]][chiffre];
            }

            return String.Format("{0}", (10 - report[k - 1]) % 10);
        }


        //Recupere le reference number d'une facture (utile pour la ligne de codage)
        public String CalculRefNumber(string RefFacture, string Code_intervenant)
        {
            string CodeFinal = "";
            string FactCompl = Complete(RefFacture, 8);
            string IntervCompl = Complete(Code_intervenant, 6);

            CodeFinal = "1" + Complete(FactCompl + IntervCompl, 19);  //1 pour Sos; 2 pour Ab TA; 3 pour Ta Mat;                   
            string str2 = CodeFinal + Modulo10(CodeFinal);

            str2 = FormatgrandNombre(str2, 5);

            str2 = "00 0000" + str2;    //N° d'identification BVR du client....Rien pour PosteFinance, seulement pour les banques

            return str2;
        }


        //Calcule de la ligne de codage
        public String CodingLine(string reference_number, string TotalFacture)
        {
            reference_number = reference_number.ToString().Replace(" ", "");

            double TotalFactureF = double.Parse(TotalFacture);
            TotalFactureF = TotalFactureF * 100;
            string str1 = TotalFactureF.ToString("000000");

            str1 = "010000" + str1;
            str1 = str1 + Modulo10(str1);
            str1 = str1 + ">" + reference_number + "+ 010306272>";    //SOS

            return str1;
        }


        //On format pour l'affichage (int64 est trop petit!)
        private string FormatgrandNombre(string GrandNombre, int GrouperPar)
        {
            string NombreFinal = "";

            for (int i = GrandNombre.Length; i >= 0; i -= GrouperPar)
            {
                if (i < GrouperPar)
                    NombreFinal = GrandNombre.Substring(0, i) + " " + NombreFinal;
                else
                    NombreFinal = GrandNombre.Substring(i - GrouperPar, GrouperPar) + " " + NombreFinal;    //On part de la fin vers le début
            }

            NombreFinal = NombreFinal.TrimEnd();   //On enleve le blanc à la fin

            return NombreFinal;
        }

        //###########################################################################################################

        //********************Fonction pour exporter un etat crystal en divers format de fichier dans le temp de l'utilisateur *************************
        protected string exportReport(CrystalDecisions.CrystalReports.Engine.ReportClass RapportChoisi, string NomDuFichier, string Extension)
        {
            try
            {
                // string tempDir = System.IO.Path.GetTempPath();  //Répertoire de l'utilisateur
                // string tempFileName = tempDir + NomDuFichier + ".";
                string tempDir = SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.Path_PDFfactureImpayees;
                string tempFileName = tempDir + NomDuFichier + ".";

                ExportFormatType FormatExport = ExportFormatType.PortableDocFormat;  //On defini un valeur par défaut pour la variable FormatExport

                switch (Extension)
                {
                    case "pdf": FormatExport = ExportFormatType.PortableDocFormat; tempFileName += "pdf"; break;
                    case "doc": FormatExport = ExportFormatType.WordForWindows; tempFileName += "doc"; break;
                    case "xls": FormatExport = ExportFormatType.Excel; tempFileName += "xls"; break;
                    case "htm": FormatExport = ExportFormatType.HTML40; tempFileName += "htm"; break;
                    case "xml": FormatExport = ExportFormatType.Xml; tempFileName += "xml"; break;
                }

                //MemoryStream FluxMem = (MemoryStream)RapportChoisi.ExportToStream(FormatExport);

                //Si plantage utiliser la ligne suivante
                Stream FluxMem = RapportChoisi.ExportToStream(FormatExport);

                if (FluxMem.Length == 0) return ("Erreur");

                try
                {
                    //Créer un FileStream object pour écrir un flux dans un fichier           
                    using (FileStream fileStream = File.Create(tempFileName, (int)FluxMem.Length))
                    {
                        //Charge le tableau de bytes[] avec le flux de donnée
                        byte[] bytesInStream = new byte[FluxMem.Length];
                        FluxMem.Read(bytesInStream, 0, (int)bytesInStream.Length);

                        //Utilise le FileStream object pour écrire dans le fichier spécifié
                        fileStream.Write(bytesInStream, 0, bytesInStream.Length);
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine("Erreur à la création du fichier :" + e.Message);
                    MessageBox.Show("Erreur à la création du fichier :" + e.Message);
                    return ("Erreur");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreurà l'exportation du fichier :" + e.Message);
            }

            return (NomDuFichier + "." + Extension);         //Tout est ok
        }

        private void bQuitter_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
