using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using ImportSosGeneve.Properties;
using SosMedecins.SmartRapport.DAL;
using System.Globalization;
using SosMedecins.SmartRapport.EtatsCrystal;
using Codecrete.SwissQRBill.Generator;

namespace ImportSosGeneve.Facture
{
    public partial class FArrangementPaiement : Form
    {
        private decimal soldeFacture = 0;
        private decimal MontantBulletins = 0;
        private decimal MontantDernierBulletin = 0;
        private int NbBulletin = 0;                     //Nombre de bulletin SANS le dernier
        private string reference_number = "";       
        dstBulletinLibre dtsBulletinlibre = new dstBulletinLibre();                
        DataTable dtFacture = new DataTable();

        public FArrangementPaiement()
        {
            InitializeComponent();
        }
     
        private void tBoxNumFact_TextChanged(object sender, EventArgs e)
        {
            string NFacture = tBoxNumFact.Text;

            //on vérifie que c'est bien un nombre
            if (NFacture != "" && NFacture.All(char.IsDigit))
                btnCherche.Enabled = true;
            else
                btnCherche.Enabled = false;
        }

        private void btnCherche_Click(object sender, EventArgs e)
        {
            //On réinitialise les chaines et valeurs
            LMontantFact.Text = "Montant du solde de la facture : ---";
            lMontantbulletin.Text = "Montant par bulletins : ---";
            ldernierBulletin.Text = "Montant du dernier bulletin : ---";
            numericUpDownNbBulletins.Value = 1;
            soldeFacture = 0;
            cBoxLogo.Checked = false;

            //Chaine de connection           
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);

            dbConnection.Open();
            SqlCommand cmd = new SqlCommand();                      

            cmd.Connection = dbConnection;

            try
            {                
                string sqlstr1 = "SELECT f.NFacture, f.AdresseDestinataire, f.DateCreation, f.Solde, ta.CodeIntervenant";
                sqlstr1 += " FROM facture f INNER JOIN factureconsultation fc On fc.NFacture = f.NFacture";
                sqlstr1 += "                INNER JOIN tableconsultations tc On tc.NConsultation = fc.NConsultation";
                sqlstr1 += "                INNER JOIN tableactes ta On ta.Num = tc.CodeAppel";
                sqlstr1 += " WHERE f.NFacture = " + tBoxNumFact.Text;

                //on execute les requête
                cmd.CommandText = sqlstr1;
                dtFacture.Clear();
                dtFacture.Load(cmd.ExecuteReader());

                //On affiche le solde de la facture et on l'affecte
                if (dtFacture.Rows.Count > 0)
                {                                        
                    if (decimal.Parse(dtFacture.Rows[0]["Solde"].ToString()) == 0)
                    {
                        LMontantFact.Text = "Cette facture est déjà soldée !";

                        //On désactive l'incrément
                        numericUpDownNbBulletins.Enabled = false;
                    }
                    else
                    {
                        LMontantFact.Text = "Montant du solde de la facture : " + dtFacture.Rows[0]["Solde"].ToString();
                        soldeFacture = decimal.Round(decimal.Parse(dtFacture.Rows[0]["Solde"].ToString()), 2, MidpointRounding.AwayFromZero);

                        lMontantbulletin.Text = "1 bulletin d'un montant de : " + soldeFacture.ToString() + " - .";

                        //On calcule une première fois avec 1 bulletin
                        CalculeBulletins(1);

                        //On active le bouton imprimer et l'incrément
                        numericUpDownNbBulletins.Enabled = true;
                        bImprime.Enabled = true;
                    }            
                }               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Impossible de récupérer la facture " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //fermeture des connexions
                if (dbConnection.State == ConnectionState.Open)
                {
                    dbConnection.Close();
                }
            }
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            Close();
        }       

        private void numericUpDownNbBulletins_ValueChanged(object sender, EventArgs e)
        {
            //On recalcule la valeur de chaque bultins
            CalculeBulletins((int)numericUpDownNbBulletins.Value);
        }

        private void CalculeBulletins(int Nb)
        {
            //On réiniitalise les valeurs
            MontantBulletins = 0;
            MontantDernierBulletin = 0;
            NbBulletin = 0;


            if (Nb == 1) //Nb = 1
            {
                MontantBulletins = soldeFacture;
                MontantDernierBulletin = 0;
                NbBulletin = Nb;
            }
            else if (Nb <= 3)
            {
                decimal Val1 = soldeFacture / Nb;
                decimal Modulo = Val1 % 10;

                if (Modulo != 0)
                {
                    //MontantDernierBulletin = Modulo * Nb;
                    decimal Val2 = soldeFacture - (Modulo * Nb);
                    
                    MontantBulletins = Val2 / Nb;
                    MontantDernierBulletin = soldeFacture - (MontantBulletins * (Nb -1));
                    NbBulletin = Nb - 1;  //Nombre de bulletin SANS le dernier
                }
                else
                {
                    MontantDernierBulletin = Val1;
                    MontantBulletins = Val1;
                    NbBulletin = Nb - 1;
                }
            }
            else if (Nb > 3)
            {
                decimal Val1 = soldeFacture / Nb;

                decimal Modulo = Val1 % 10;

                //On créer une règle d'arrondi (0 <= 1-2; 3-4 => 5 <= 6-7; 8-9 => 0 (+10))
                switch ((int)Modulo)
                {
                    case 0: MontantBulletins = Val1 - Modulo; break;
                    case 1: MontantBulletins = Val1 - Modulo; break;
                    case 2: MontantBulletins = Val1 - Modulo; break;
                    case 3: MontantBulletins = Val1 - Modulo + 5; break;
                    case 4: MontantBulletins = Val1 - Modulo + 5; break;
                    case 5: MontantBulletins = Val1 - Modulo + 5; break;
                    case 6: MontantBulletins = Val1 - Modulo + 5; break;
                    case 7: MontantBulletins = Val1 - Modulo + 5; break;
                    case 8: MontantBulletins = Val1 - Modulo + 10; break;
                    case 9: MontantBulletins = Val1 - Modulo + 10; break;
                }
                
                MontantDernierBulletin = soldeFacture - (MontantBulletins * (Nb - 1));  //nb -1 bulletin

                NbBulletin = Nb - 1;

                //Tant que le dernier bulletin est négatif, on décrémente le nombre de bulletins
                int i = 0;
                while (MontantDernierBulletin < 0)
                {
                    i++;
                    MontantDernierBulletin = soldeFacture - (MontantBulletins * (Nb - i));   //nb - 2 bulletins                        

                    NbBulletin = Nb - i;
                }       
            }
           

            //Affichage et arrondi
            if (MontantBulletins != 0)
            {
                //On arrondi le montant du dernier bulletin
                MontantBulletins = Math.Round(MontantBulletins, 2, MidpointRounding.AwayFromZero);               

                //Pour l'orthographe
                if (NbBulletin == 1)
                    lMontantbulletin.Text = "1 bulletin d'un montant de : " + MontantBulletins.ToString() + " - .";               
                else
                    lMontantbulletin.Text = NbBulletin + " bulletins d'un montant de : " + MontantBulletins.ToString() + " - chacun.";
            }

            if (MontantDernierBulletin != 0)
            {               
                //On arrondi le montant du dernier bulletin
                MontantDernierBulletin = Math.Round(MontantDernierBulletin, 2, MidpointRounding.AwayFromZero);                

                ldernierBulletin.Text = "1 bulletin d'un montant de : " + MontantDernierBulletin.ToString() + " - .";
            }
            else
            {
                ldernierBulletin.Text = "Montant du dernier bulletin : ---";
            }
        }

        private void bImprime_Click(object sender, EventArgs e)
        {
            //On prepare les bulletins
            PrepareBulletins(dtFacture);

            QR_BVR_Libre qr_BVR_Libre = new QR_BVR_Libre();
            qr_BVR_Libre.SetDataSource(dtsBulletinlibre);           
            crystalReportViewer1.ReportSource = qr_BVR_Libre;
        
            crystalReportViewer1.Zoom(2);
        }


        private void PrepareBulletins(DataTable _dtFacture)
        {
            //On affecte au dtsBulletinLibre les infos
            dtsBulletinlibre.DtFacture.Clear();

            //On calcule le ref number
            reference_number = CalculRefNumber(_dtFacture.Rows[0]["NFacture"].ToString(), _dtFacture.Rows[0]["CodeIntervenant"].ToString());
           
            //On formate la date
            DateTime DateFact = DateTime.Parse(_dtFacture.Rows[0]["DateCreation"].ToString());

            byte[] QRCodeBulletin = PrepareQRCode(MontantBulletins);

            string AvecLogo = "";
            if (cBoxLogo.Checked)
                AvecLogo = "Affiche logo";
            else
                AvecLogo = "";


            //On rempli les bulletins "Normaux"
            for (int i = 0; i < NbBulletin; i++)
            {
                dtsBulletinlibre.DtFacture.AddDtFactureRow(i + 1, int.Parse(_dtFacture.Rows[0]["NFacture"].ToString()), "", _dtFacture.Rows[0]["AdresseDestinataire"].ToString(),
                                                 DateFact, MontantBulletins, reference_number, QRCodeBulletin, AvecLogo);
            }

            if ((int)numericUpDownNbBulletins.Value > 1)
            {
                //On rempli le dernier bulletin
                byte[] QRCodeDernierBulletin = PrepareQRCode(MontantDernierBulletin);
                dtsBulletinlibre.DtFacture.AddDtFactureRow(NbBulletin + 1, int.Parse(_dtFacture.Rows[0]["NFacture"].ToString()), "", _dtFacture.Rows[0]["AdresseDestinataire"].ToString(),
                                                     DateFact, MontantDernierBulletin, reference_number, QRCodeDernierBulletin, AvecLogo);
            }            
        }


        //************************************Pour la ligne de codage ***************************************************
      
        // Methode pour completer les chaines avec des 0 ( exemple 135 donnera 00135 si longueur est à 5)
        private string Complete(string Chaine, int longueur)
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
        public string CalculRefNumber(string RefFacture, string Code_intervenant)
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


        //Parametres du QRCode
        private byte[] PrepareQRCode(decimal MontantBulletins)
        {            
            string DestFact = dtFacture.Rows[0]["AdresseDestinataire"].ToString();
            string NomDestinataire = "";
            string AdresseDestinataire = " ";
            string CPVille = " ";

            string[] m_tabAdr = DestFact.Split('\n');

            if (m_tabAdr.Length > 1)            
                NomDestinataire = m_tabAdr[0];

            if (m_tabAdr.Length > 2)
                AdresseDestinataire = m_tabAdr[1];
            if (m_tabAdr.Length == 3)
                CPVille = m_tabAdr[2];            
            
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
                Amount = MontantBulletins,
                Currency = "CHF",

                //Débiteur
                Debtor = new Address
                {
                    Name = NomDestinataire,
                    AddressLine1 = AdresseDestinataire,
                    AddressLine2 = CPVille,
                    CountryCode = "CH"
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

            //Génération de la facture dans un [] de byte
            byte[] QRCodeRetour = null;
            
            try
            {
                QRCodeRetour = QRBill.Generate(bill);               
            }
            catch (Exception e)
            {
                MessageBox.Show("Erreur dans le QRCode : " + e.Message);
            }
          
            return QRCodeRetour;            
        }

              
        private void tBoxNumFact_KeyDown(object sender, KeyEventArgs e)
        {
            //Pour le copier/coller
            if (e.KeyCode == Keys.Enter)
                btnCherche_Click(sender, e);
        }
    }
}


// A faire
