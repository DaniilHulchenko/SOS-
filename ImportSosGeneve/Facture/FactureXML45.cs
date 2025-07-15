using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;

namespace ImportSosGeneve
{

    /// <summary>
    /// Description résumée de FactureXML.
    /// Cette classe permet de générer un fichier XML, imprimer ou récuperer des informations d'une facture    
    /// </summary>

    public class FactureXML45
    {        
        private MySql DB = null;
        private String fichier;       
        // Données globale à la facture
        private String IdFacture;
        //private DateTime DateFacture;
        private String EanMedecin;
        private string AssuranceEAN = "";
        //private String DateFactureF;
        private String DAP;
        private string Seance;
        public bool Erreurs = false;
        private int Destinataire = -1;     //0 CDM, 1 Medidata
        private int CreerRappelNum = 0;    //Pour créer le rappel => 0: pas de rappel, 1: 1er rappel, 2: 2eme rappel, 3:3eme rappel, 4:Poursuites;
        private int RenvFact10p = 0;
        private string DocJoint = "Non";
        private string CopyPourPatient = "0";
        private string Email = "KO";
        private string NumPortable = "KO";
        private int premierRappelAss = 45;   //Pour les Assurances (TP, Type_envoi = 3) 1er rappel à 45 jours (changé de 45 à 60 jour le 21.12.2021 reppassé à 45 le 13.07.2022)
        private int premierRappelTG = 60;   //1er rappel à 60 jours (TG, Type envoi = 1)
        private int deuxiemeRappel = 30;    //2eme rappel à 30 jour après
        private float MontantFrais = 0;
        //private int troixiemeRappel = 30;  //3eme rappel à 30 jour après (uniquement pour le TP, Type_envoi = 3)        

        private string Texte1erRappel = "Après controle de votre compte, nous avons constaté que la facture suivante est impayee.\r\n" +
            //"Les frais de rappel sont de 20 Frs.\r\n" +
            " Si entre temps, vous deviez avoir reglé ce montant, nous vous serions reconnaissants de nous faire parvenir un justificatif de paiement.\r\n" +
            "En vous remerciant pour votre confiance, nous vous presentons, ";

        private string Texte2emeRappel = "Après controle de votre compte, nous avons constaté que la facture suivante est impayee.\r\n" +
           // "Les frais de rappel sont de 30 Frs, auquels s'ajoutent le montant des frais du premier rappel, 20 Frs.\r\n" +
            " Si entre temps, vous deviez avoir reglé ce montant, nous vous serions reconnaissants de nous faire parvenir un justificatif de paiement.\r\n" +
            "En vous remerciant pour votre confiance, nous vous presentons, ";

        //public FactureXML45(String IdFac, String fichier)
        public FactureXML45(MySql DB, String IdFac, String fichier)
        {           
            this.DB = DB;
            this.IdFacture = IdFac;
            this.fichier = fichier;

            try
            {
                XmlTextWriter xml = new XmlTextWriter(fichier, System.Text.Encoding.UTF8);

                EditeFacture(xml);
                xml.Flush(); //On vide le buffer 
                xml.Close(); //On ferme de Fichier

                if (Erreurs == true)
                    throw new System.ArgumentException("Une erreur c'est produite...Arrêt de la procédure", "Erreur");
                

                //On valide l'envoi de la facture avec le type d'envoi                              
                if (Destinataire == 0)
                    OutilsExt.OutilsSql.EnregistreEnvoiFacture(long.Parse(IdFac), "Fichier xml..");
                else if (Destinataire == 1)  //Medidata
                    OutilsExt.OutilsSql.EnregEnvoiFactMedidata(long.Parse(IdFac), CreerRappelNum, RenvFact10p, "Fichier xml..Medidata");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la création du fichier XML: " + fichier + " " + ex.Message.ToString());
            }            
        }


        //Pour Medidata/Caisse des médecins
        public FactureXML45(MySql DB, String IdFac, String fichier, int Dest)
        {          
            this.DB = DB;
            this.IdFacture = IdFac;
            this.fichier = fichier;          

            //-1 si c'est pas la CDM(0) ou Medidata(1)
            Destinataire = Dest;

            try
            {
                XmlTextWriter xml = new XmlTextWriter(fichier, System.Text.Encoding.UTF8);

                EditeFacture(xml);
                xml.Flush(); //On vide le buffer 
                xml.Close(); //On ferme de Fichier

                if (Erreurs == true)
                    throw new System.ArgumentException("Une erreur c'est produite...Arrêt de la procédure", "Erreur");

                //On valide l'envoi de la facture avec le type d'envoi                         
                if (Destinataire == 0)
                    OutilsExt.OutilsSql.EnregistreEnvoiFacture(long.Parse(IdFac), "Fichier xml..");
                else if (Destinataire == 1)
                    //OutilsExt.OutilsSql.EnregistreEnvoiFacture(long.Parse(IdFac), "Fichier xml..Medidata");
                    OutilsExt.OutilsSql.EnregEnvoiFactMedidata(long.Parse(IdFac), CreerRappelNum, RenvFact10p, "Fichier xml..Medidata");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la création du fichier XML: " + fichier + " " + ex.Message.ToString());
            }
        }


        //Pour sortie imprimante
        //public FactureXML45(String IdFac, String fichier, string print)
        public FactureXML45(MySql DB, String IdFac, String fichier, string print)
        {
            this.DB = DB;
            this.IdFacture = IdFac;
            this.fichier = fichier;           

            try
            {
                //On ne crée pas de Fichier sur le disque, on travaille en mémoire avec un buffer
                Stream streamData = new MemoryStream();

                //On crée un objet XML
                XmlTextWriter xml = new XmlTextWriter(streamData, System.Text.Encoding.UTF8);
                // xml.Formatting = Formatting.Indented;  //Si on veut indender notre xml

                EditeFacture(xml);

                xml.Flush(); //On vide le buffer 
                //xml.Close(); //On ferme de Fichier
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la création du fichier XML: " + fichier + " " + ex.Message.ToString());
            }
        }

        // Edite l'entete d'une facture
        private void EditeEnTete(XmlTextWriter xml)
        {
            // bool bStatus;
            // Recuperation des infos générale
            String ChEnTete = @"SELECT f.NFacture, c.NConsultation, pe.nom, pe.prenom, pe.Adm_Pays as Pays,
									pe.Adm_Commune as Commune, pe.Adm_Rue as Rue, pe.Adm_NumeroDansRue as NumeroDansRue, pe.Chez as AdrChez,
                                    pe.DateNaissance, pe.Sexe, pe.Num_Assure, pe.Email, pe.Tel, f.DateCreation, f.TypeEnvoi, f.Tarif, f.TTT,
									f.TypeAssurance, f.TypeSortie, f.NAccident, f.DateAccident, f.RefPatient, f.FlagConcerne,
									f.Commentaire, f.TotalFacture, f.Solde, f.TypeDestinataire, f.CodeDestinataire,
									f.AdresseDestinataire, f.AdresseDestinataire2,
                                    f.FactNum_AVS, TypeDocJoint, UrlDocJoint,
									m.CodeIntervenant, m.Nom as 'MedNom', m.NomGeneve,
                                    m.PrenomGeneve, m.EAN, m.Concordat, m.Independant,
									m.NIF, a.DSL, a.DFI, pe.Adm_CodePostal as CodePostal,
									ass.NAssurance, ass.AssEAN, ass.GLNTransport, ass.AssNom, ass.AssTelephone,
                                    ass.AssFax, adr.NumDansRue as AssNumDansRue, adr.Np as AssNp,
                                    adr.Rue as AssRue, adr.Commune as AssCommune,
									a.DAP, f.Num_Session,
                                    fs.FacDateEnvoyee, fs.FacDate1Rappel, fs.FacDate2Rappel, fs.FacDate3Rappel, fs.RenvFact10p ";  //, fs.FacDateRappel10		A REACTIVER EN PROD					

            ChEnTete += @" from facture f 
								left join facture_status fs on fs.NFacture = f.NFacture 
								left join factureconsultation c on c.NFacture = f.NFacture 
								left join tableconsultations co on co.NConsultation = c.NConsultation
								left join tablepatient pa on pa.IdPatient = co.IndicePatient 
								left join tablepersonne pe on pe.IdPersonne=  pa.IdPersonne
								left join tableactes a on co.CodeAppel = a.Num
								left join tablemedecin m on m.CodeIntervenant = a.CodeIntervenant
								left join assurances ass on f.CodeDestinataire = ass.NAssurance
                                left join adresses adr on ass.NAdresse = adr.NAdresse

								where f.NFacture = '" + IdFacture + "'";

            // Recupere les données           
            SqlDataReader Entete = DB.getEnregistrement(ChEnTete);  //1

            if (!Entete.Read())
            {
                Entete.Close();
                throw new Exception("Le numero de facture est incorrect");
            }

            //######Traitement des différentes valeurs pour le fichier xml#########
            //REcupere l'EAN du medecin
            EanMedecin = Entete["EAN"].ToString();

            //Pour le titre du destinataire
            string title = "";
            string sexeG = "";

            sexeG = Entete["Sexe"].ToString().ToLower();
            if (sexeG == "h")
                title = "Monsieur";
            else if (sexeG == "m")
                title = "Monsieur";
            else if (sexeG == "f")
                title = "Madame";
            else if (sexeG == "e")
                //return "L'enfant";
                title = "Aux parents de";
            else
                title = "Madame,Monsieur";


            //Informations relatives au Patient
            string prenom = Entete["prenom"].ToString();
            string nom = Entete["nom"].ToString();

            if (prenom == "" || prenom == DBNull.Value.ToString())
            {
                MessageBox.Show("vide" + Entete["NFacture"].ToString());
            }

            string sexeP;
            string rue = "";
            string retour = "";
            string commune = "";
            string codepostal = "";
            string Pays = "";
            string AdrChez = "";

            //Formatage patient et adresse et remplacement de certains caractères
            prenom = prenom.Replace("(TA)", "");
            nom = nom.Replace("(TA)", "");
            prenom = prenom.Replace("TÃ–", "");
            nom = nom.Replace("TÃ–", "");

            prenom = RemplaceCaractere(prenom);
            nom = RemplaceCaractere(nom);

            //bStatus = address.SetPerson(nom, prenom, "", "");

            //Chez
            if (Entete["AdrChez"] != DBNull.Value || Entete["AdrChez"].ToString() != "")
            {
                AdrChez = Entete["AdrChez"].ToString();
            }

            //if (!Entete.IsDBNull(6) || !Entete.IsDBNull(7))
            if (Entete["NumeroDansRue"] != DBNull.Value || Entete["Rue"] != DBNull.Value)
            {
                retour = Entete["Rue"].ToString();
                string[] TabRue = retour.Split(',');
                if (TabRue.Length == 1)
                    rue += retour + " " + Entete["NumeroDansRue"].ToString();
                else if (TabRue.Length == 2)
                    rue += TabRue[1] + " " + TabRue[0] + " " + Entete["NumeroDansRue"].ToString();
                else
                    rue += retour + " " + Entete["NumeroDansRue"].ToString();
            }


            //if (!Entete.IsDBNull(5))
            if (Entete["Commune"] != DBNull.Value)
                commune = Entete["Commune"].ToString();

            //if (!Entete.IsDBNull(41)) 
            if (Entete["CodePostal"] != DBNull.Value)
                codepostal = Entete["CodePostal"].ToString();

            if (Entete["CodePostal"].ToString() == "") codepostal = "XXX";
            if (Entete["Commune"].ToString() == "") commune = "XXX";
            if (Entete["rue"].ToString() == "") rue = "XXX";

            rue = RemplaceCaractere(rue);
            commune = RemplaceCaractere(commune);

            if (commune.ToUpper() == "LANCY")
            {
                if (codepostal == "1213")
                    commune = "PETIT LANCY";
                else if (codepostal == "1212")
                    commune = "GRAND LANCY";
            }

            if (commune.ToUpper() == "GRAND SACONNEX,LE")
                commune = "LE GRAND SACONNEX";
            else if (commune.ToUpper() == "CAROUGE,GE")
                commune = "CAROUGE";

            if (Entete["Pays"].ToString() == "" || Entete["Pays"].ToString() == " " || Entete["Pays"].ToString().Length > 2)
                Pays = "CH";
            else
                Pays = Entete["Pays"].ToString();


            //formatage de la date de naissance (yyyy-mm-ddT00:00:00)
            DateTime Datenaiss;
            String DatenaissP = "";

            //On teste la validité de la date de naissance
            try
            {
                Datenaiss = DateTime.Parse(String.Format("{0:d/M/yyyy}", DateTime.Parse(Entete["DateNaissance"].ToString())));
                DatenaissP = String.Format("{0:s}", Datenaiss);
            }
            catch (FormatException)
            {
                MessageBox.Show("Erreur dans la date de naissance du patient " + nom + " " + prenom + "\r\n cette erreur STOP l'envoi.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Erreurs = true;
            }

            if (Entete["Sexe"].ToString().Equals("H"))
                sexeP = "male";
            else
                sexeP = "female";

            //Pour le nombre de séance
            Seance = Entete["Num_Session"].ToString();
            //###### FIN Traitement des différentes valeurs pour le fichier xml #########
           
            //Ajout des paramètres invoice:header
            xml.WriteStartElement("invoice:processing");
            xml.WriteAttributeString("print_at_intermediate", "1");     //pour TG
            xml.WriteAttributeString("print_copy_to_guarantor", "1");   //Pour TP

            xml.WriteStartElement("invoice:transport");

            /* if (Destinataire != 1)    //CDM
             {
                 xml.WriteAttributeString("from", "7601002083034");      //GLN Sos médecins
                 xml.WriteAttributeString("to", EanMedecin);

                 xml.WriteStartElement("invoice:via");
                 xml.WriteAttributeString("sequence_id", "1");
                 xml.WriteAttributeString("via", "7611910000047");     //GLN Caisse des médecins

                 DocJoint = "Oui";
             }
             else*/   //MEDIDATA

            //############### Pour Test ############################# 
            /*xml.WriteAttributeString("from", "2099988886582");      //GLN TEST Sos médecins à travers Médidata

             if (Entete["TypeEnvoi"].ToString() != "3")     //TP
             {
                 //Tiers Garant;
                 xml.WriteAttributeString("to", "2000000000008");     //GLN Passe-partout  

                 DocJoint = "Non";
             }
             else
             {
                 xml.WriteAttributeString("to", "2099988876514");        //GLN TEST Serveur Médidata 
                 DocJoint = "Non";
             }*/

            //################### Pour Production ################:
            xml.WriteAttributeString("from", "7601002083034");    //GLN SOS Médecins

            //TypeEnvoi: Tiers Garant (1), Tier(2), Tiers Payant (3)           
            if (Entete["TypeEnvoi"].ToString() != "3")      //TG ou Tier
            {
                //Tiers Garant ou Tier;
                xml.WriteAttributeString("to", "2000000000008");     //GLN Passe-partout  

                DocJoint = "Non";
            }
            else
            {
                //Tiers Payant
                //On vérifie qu'on a bien un GLN sur 13 caractères
                if ((Entete["GLNTransport"] != DBNull.Value) && (Entete["GLNTransport"].ToString() != ""))
                {
                    if (Entete["GLNTransport"].ToString().Substring(0, 3) == "760")
                    {
                        //xml.WriteAttributeString("to", Entete["AssEAN"].ToString());  //EAN de l'assurance
                        xml.WriteAttributeString("to", Entete["GLNTransport"].ToString());  //EAN de l'assurance

                        AssuranceEAN = Entete["GLNTransport"].ToString();

                        //Si c'est Assura, on envoi les docs 
                        if (Entete["GLNTransport"].ToString() == "7601003001303")
                            DocJoint = "Oui";
                        else
                            DocJoint = "Non";
                    }
                    else
                        xml.WriteAttributeString("to", "2000000000008");   //GLN Passe partout => erreur
                }
                else xml.WriteAttributeString("to", "2000000000008");   //GLN Passe partout => erreur                    
            }

            //#################### Fin Prod ###########################################

            xml.WriteStartElement("invoice:via");
            xml.WriteAttributeString("sequence_id", "1");
            xml.WriteAttributeString("via", "7601001304307");  //GLN PROD de Medidata                


            xml.WriteEndElement();          //Fin du via
            xml.WriteEndElement();          //Fin du transport            

            DateTime DateEditionFacture = DateTime.Parse(String.Format("{0:d/M/yyyy}", DateTime.Parse(DateTime.Now.ToString())));
            string DateEditionFactureF = String.Format("{0:s}", DateEditionFacture);

            //formatage de la date de la facture (yyyy-mm-ddT00:00:00)
            DateTime DateFacture = DateTime.Parse(String.Format("{0:d/M/yyyy}", DateTime.Parse(Entete["DateCreation"].ToString())));
            String DateFactureF = String.Format("{0:s}", DateFacture);

            //...et de la date d'appel
            DateTime Appel = DateTime.Parse(String.Format("{0:d/M/yyyy}", DateTime.Parse(Entete["DAP"].ToString())));
            DAP = String.Format("{0:s}", Appel);

            //recupération du temps époche 
            Int64 epoche = ToUnixTime(DateTime.Now);

            xml.WriteStartElement("invoice:demand");
            xml.WriteAttributeString("tc_demand_id", "0");
            xml.WriteAttributeString("tc_token", "69");
            xml.WriteAttributeString("insurance_demand_date", DateEditionFactureF);
            xml.WriteEndElement();          //Fin demand
            xml.WriteEndElement();          //Fin du processing        

            //#################### GESTION COPIE PAR EMAIL #############################

            //Copie pour patient.... on vérifie s'il a un email et n° de portable pour envoi de la copie electronique
            string PortablePatient = "";
            string EmailPatient = "";

            if (Entete["Tel"].ToString().Length == 12 && Entete["Tel"].ToString().Substring(0, 1) == "+" && Entete["Tel"].ToString().Substring(0, 4) != "+412")
                NumPortable = "OK";
            else
                NumPortable = "KO";

            //On vérifie le format de l'email
            if (Entete["Email"] != DBNull.Value && Entete["Email"].ToString() != "")
            {
                // Expression régulière pour valider une adresse e-mail
                string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                Regex regex = new Regex(pattern);

                // Vérifier si l'adresse e-mail correspond au modèle
                if (regex.IsMatch(Entete["Email"].ToString()))
                    Email = "OK";
                else
                    Email = "KO";
            }

            if (NumPortable == "OK" && Email == "OK")
            {
                //CopyPourPatient = "1";
                CopyPourPatient = "0";

                //puis on passe également les paramètres Tel et email
                PortablePatient = Entete["Tel"].ToString();
                EmailPatient = Entete["Email"].ToString();
            }
            else
                CopyPourPatient = "0";

            xml.WriteStartElement("invoice:payload");
            xml.WriteAttributeString("type", "invoice");           

            //Si la facture a déjà été envoyé....Pas de copie       ### Le tag copy est destiné à mettre copy dans le titre de la facture
            if (Entete["FacDateEnvoyee"] != DBNull.Value)
                xml.WriteAttributeString("copy", "0");
            else
                //xml.WriteAttributeString("copy", CopyPourPatient);    //A activer pour l'envoi de la copie electronique             
                xml.WriteAttributeString("copy", "0");
            //#################### FIN GESTION COPIE PAR EMAIL #############################

            xml.WriteAttributeString("storno", "0");

            /*xml.WriteStartElement("invoice:credit "); 
            xml.WriteAttributeString("request_timestamp", epoche.ToString());
            xml.WriteAttributeString("request_date", DateFactureF);
            xml.WriteAttributeString("request_id", "23_45.01"); xml.WriteEndElement();//credit*/

            xml.WriteStartElement("invoice:invoice ");
            xml.WriteAttributeString("request_timestamp", epoche.ToString());
            xml.WriteAttributeString("request_date", DateFactureF);
            xml.WriteAttributeString("request_id", Entete["NFacture"].ToString());
            xml.WriteEndElement();      //Fin de invoice

            //Gestion des rappels
            try
            {
                if (Destinataire == 1)    //Médidata: Donc gestion des rappels
                {
                    //On regarde s'il y a une date d'envoi de la facture
                    if (Entete["FacDateEnvoyee"] != DBNull.Value)
                    {
                        //La date d'envoie est elle > à 45 jours?    //Changé le 21.12.2021 de 45 à 60 puis rechangé à 45 le 13.07.2022
                        DateTime FacDateEnvoyee;
                        DateTime.TryParse(Entete["FacDateEnvoyee"].ToString(), out FacDateEnvoyee);

                        int nbJourdeRappel = 0;     //Nb jour de rappels en fonction du type d'envoi

                        if (Entete["TypeEnvoi"].ToString() != "3")   //c'est un TG ou un Tier                       
                            nbJourdeRappel = premierRappelTG;
                        else
                            nbJourdeRappel = premierRappelAss;

                        if (FacDateEnvoyee.AddDays(nbJourdeRappel) < DateTime.Now)
                        {
                            //si les factures sont postérieure au 17.12.2020 on traite les rappels
                            if (DateTime.Parse(Entete["DAP"].ToString()) > DateTime.Parse("17.12.2020"))
                            {
                                //On regarde si on a une date de 1er rappel
                                DateTime FacDate1Rappel;
                                if (DateTime.TryParse(Entete["FacDate1Rappel"].ToString(), out FacDate1Rappel))
                                {
                                    //La date d'envoie depuis le 1er rappel est elle > à 30 jours?
                                    if (FacDate1Rappel.AddDays(deuxiemeRappel) < DateTime.Now)
                                    {
                                        //On regarde s'il y a un 2eme rappel...
                                        DateTime FacDate2Rappel;

                                        if (DateTime.TryParse(Entete["FacDate2Rappel"].ToString(), out FacDate2Rappel))
                                        {
                                            //Domi le 10.06.2024 Mise en remarque pour eviter les 3eme rappels
                                            //3eme rappel uniquement pour les TP (les assurances)
                                            /*if (Entete["TypeEnvoi"].ToString() == "3")
                                            {
                                                //La date d'envoie depuis le 2eme rappel est elle > à 30 jours?
                                                if (FacDate2Rappel.AddDays(troixiemeRappel) < DateTime.Now)
                                                {
                                                    //On regarde s'il y a un 3eme rappel...
                                                    DateTime FacDate3Rappel;

                                                    if (DateTime.TryParse(Entete["FacDate3Rappel"].ToString(), out FacDate3Rappel))
                                                    {
                                                        //Dans ce cas on ne fait rien c'est PP
                                                        Console.WriteLine("On ne fait rien après un 3eme rappel => PP");
                                                    }
                                                    else
                                                        CreerRappelNum = 3;  //On fait un 3eme rappel
                                                }
                                            }
                                            else  //Tier Garant (Patient)
                                            {
                                                //On met en poursuite
                                                CreerRappelNum = 4;
                                                Console.WriteLine("On met en poursuite (seulement pour les TG)");
                                            }*/
                                        }
                                        else
                                        {
                                            CreerRappelNum = 2;  //On fait un 2eme rappel                                    
                                        }
                                    }
                                    //sinon on est dans la periode du 1er rappel, on attend on ne fait rien                                
                                }
                                else
                                {
                                    CreerRappelNum = 1;     //On fait un 1er rappel
                                                            
                                    //Si TG ajouter et gérer les frais
                                    if (Entete["TypeEnvoi"].ToString() != "3")
                                    {
                                       //AjoutFraisRappel(int.Parse(Entete["NFacture"].ToString()), 1);   //############### Ajouter qd activation frais de rappels  #####
                                    }                                    
                                }
                            }
                            else
                            {
                                //Pas de dates de rappels, on met en poursuite les TG
                                if (Entete["TypeEnvoi"].ToString() != "3")
                                {
                                    CreerRappelNum = 4;
                                    Console.WriteLine("On met en poursuite (seulement pour les TG)");
                                }
                            }
                        }
                        //Sinon Traitement normal                        
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            //Balises Pour les rappels
            if (CreerRappelNum != 0)
            {
                //formatage de la date du rappel (yyyy-mm-ddT00:00:00)
                DateTime DateRappel = DateTime.Parse(String.Format("{0:d/M/yyyy}", DateTime.Now));
                string DateRappelF = String.Format("{0:s}", DateRappel);
                string TexteRappel = "";

                xml.WriteStartElement("invoice:reminder ");
                xml.WriteAttributeString("request_timestamp", epoche.ToString());
                xml.WriteAttributeString("request_date", DateRappelF);
                xml.WriteAttributeString("request_id", Entete["NFacture"].ToString());
                
                if(CreerRappelNum >= 3)
                xml.WriteAttributeString("reminder_level", "3");

                //En fonction du rappel on change le texte
                switch (CreerRappelNum)
                {
                    case 1: TexteRappel = Texte1erRappel; break;
                    case 2: TexteRappel = Texte2emeRappel; break;
                    case 3: TexteRappel = "DERNIER RAPPEL AVANT CONTENTIEUX\r\n" + Texte2emeRappel; break;
                }
               
                xml.WriteAttributeString("reminder_text", TexteRappel + title + " nos meilleurs salutations.");
                xml.WriteEndElement();//reminder*/
            }

            xml.WriteStartElement("invoice:body");
            xml.WriteAttributeString("role_title", "SOS MEDECINS Cite Calvin SA");
            xml.WriteAttributeString("role", "physician");
            xml.WriteAttributeString("place", "practice");

            xml.WriteStartElement("invoice:prolog");

            xml.WriteStartElement("invoice:package");
            xml.WriteAttributeString("name", "SmartRapport");
            xml.WriteAttributeString("copyright", "SOS MEDECINS Cite Calvin SA");
            xml.WriteAttributeString("description", "SmartRapport");
            xml.WriteAttributeString("version", "5");
            xml.WriteAttributeString("id", "5001");

            /*xml.WriteStartElement("invoice:depends_on");
            xml.WriteAttributeString("name", ""); 
            xml.WriteAttributeString("copyright", ""); 
            xml.WriteAttributeString("description", ""); 
            xml.WriteAttributeString("version", ""); 
            xml.WriteAttributeString("id", ""); 
            xml.WriteEndElement();*/
            xml.WriteEndElement();       //Fin de package

            xml.WriteStartElement("invoice:generator");
            xml.WriteAttributeString("name", "SmartRapport");
            xml.WriteAttributeString("copyright", "SOS MEDECINS Cite Calvin SA");
            xml.WriteAttributeString("description", "SmartRapport");
            xml.WriteAttributeString("version", "5");
            xml.WriteAttributeString("id", "5001");

            /*xml.WriteStartElement("invoice:depends_on");
            xml.WriteAttributeString("name", ""); 
            xml.WriteAttributeString("copyright", ""); 
            xml.WriteAttributeString("description", ""); 
            xml.WriteAttributeString("version", ""); 
            xml.WriteAttributeString("id", ""); 
            xml.WriteEndElement(); */
            xml.WriteEndElement();      //Fin de generator;
            xml.WriteEndElement();      //Fin de prolog

            xml.WriteStartElement("invoice:remark");
            xml.WriteString("Dr. " + Entete["MedNom"].ToString());
            xml.WriteEndElement();       //Fin de remark                               

            //Définition de la facture

            //********** Type de paiment Garant ou Tiers Payant (1 Tier Garant, 2 Tier, 3 Assurance)
            //Debut tiers_garant ou tiers_payant
            //if (Entete["TypeDestinataire"].ToString() != "2")
            if (Entete["TypeEnvoi"].ToString() != "3")
            {
                //Tiers Garant ou Tier;
                xml.WriteStartElement("invoice:tiers_garant");
                xml.WriteAttributeString("payment_period", "P30D");
            }
            else
            {
                //Tiers Payant
                xml.WriteStartElement("invoice:tiers_payant");
                xml.WriteAttributeString("payment_period", "P30D");
            }

            //Début biller
            xml.WriteStartElement("invoice:biller");
            xml.WriteAttributeString("ean_party", "7601002083034");   //GLN SOS

            // Informations relatives au provider
            String Concordat;
            if (Entete["Concordat"].ToString() == "")
                Concordat = "C147625";
            else
                Concordat = Entete["Concordat"].ToString().Replace(" ", "").Replace(".", "");     //on enlève les espaces et le point

            xml.WriteAttributeString("zsr", Concordat);

            xml.WriteStartElement("invoice:company");
            xml.WriteStartElement("invoice:companyname");
            xml.WriteString("SOS MEDECINS Cite Calvin SA");
            xml.WriteEndElement();
            xml.WriteStartElement("invoice:department");
            xml.WriteString("Geneve");
            xml.WriteEndElement();
            //xml.WriteStartElement("invoice:subaddressing"); 
            //xml.WriteEndElement();
            xml.WriteStartElement("invoice:postal");
            //xml.WriteStartElement("invoice:pobox"); 
            //xml.WriteEndElement();
            xml.WriteStartElement("invoice:street");
            xml.WriteString("rue Louis Favre 43");
            xml.WriteEndElement();
            xml.WriteStartElement("invoice:zip");
            xml.WriteAttributeString("countrycode", "CH");
            xml.WriteString("1201");
            xml.WriteEndElement();
            xml.WriteStartElement("invoice:city");
            xml.WriteString("Geneve");
            xml.WriteEndElement();
            xml.WriteEndElement();   //Fin postal

            xml.WriteStartElement("invoice:telecom");
            xml.WriteStartElement("invoice:phone");
            xml.WriteString("0227484950");
            xml.WriteEndElement();
            xml.WriteStartElement("invoice:fax");
            xml.WriteString("0227484900");
            xml.WriteEndElement();
            xml.WriteEndElement();  //Fin  telecom

            xml.WriteStartElement("invoice:online");
            xml.WriteStartElement("invoice:email");
            xml.WriteString("admin@sos-medecins.ch");
            xml.WriteEndElement();

            xml.WriteStartElement("invoice:url");
            xml.WriteString("www.sos-medecins.ch");
            xml.WriteEndElement();
            xml.WriteEndElement();  //Fin online
            xml.WriteEndElement();  //Fin de compagny        

            xml.WriteEndElement();  //Fin de biller
                                    //Fin biller

            //#### Debut debitor #### 3 Possiblilités 1 TG, 2 Tier, 3 TP           
            if (Entete["TypeEnvoi"].ToString() == "1")   //Si c'est un TG (Patient)
            {
                //Tiers Garant (Debitor = Patient);
                xml.WriteStartElement("invoice:debitor");                

                //On vérifie qu'on a bien un GLN sur 13 caractères
                if ((Entete["AssEAN"] != DBNull.Value) && (Entete["AssEAN"].ToString() != "") && Entete["AssEAN"].ToString() != "0000000000000")
                {
                    if (Entete["AssEAN"].ToString().Substring(0, 3) == "760")
                    {
                        xml.WriteAttributeString("ean_party", Entete["AssEAN"].ToString());
                    }
                }
                else xml.WriteAttributeString("ean_party", "2000000000008");   //GLN Passe partout

                xml.WriteStartElement("invoice:person");
                xml.WriteAttributeString("title", title);
                xml.WriteStartElement("invoice:familyname");
                xml.WriteString(nom);
                xml.WriteEndElement();
                xml.WriteStartElement("invoice:givenname");
                xml.WriteString(prenom);
                xml.WriteEndElement();

                if (AdrChez != "")
                {
                    xml.WriteStartElement("invoice:subaddressing");
                    xml.WriteString("C/o " + AdrChez);
                    xml.WriteEndElement();
                }

                xml.WriteStartElement("invoice:postal");

                //On est limité à 35 de longueur pour la rue si ça dépasse on ne met pas la rue
                if (rue.Length <= 35)
                {
                    xml.WriteStartElement("invoice:street");
                    xml.WriteString(rue);
                    xml.WriteEndElement();
                }
                xml.WriteStartElement("invoice:zip");

                if (Pays == "" || Pays == " " || Pays.Length > 2)
                    Pays = "CH";

                xml.WriteAttributeString("countrycode", Pays);
                xml.WriteString(codepostal);
                xml.WriteEndElement();
                xml.WriteStartElement("invoice:city");
                xml.WriteString(commune);
                xml.WriteEndElement();
                xml.WriteEndElement();   //Fin postal

                //Si c'est un portable et un email valide
                if (PortablePatient != "" && EmailPatient != "")
                {
                    xml.WriteStartElement("invoice:telecom");
                    xml.WriteStartElement("invoice:phone");
                    xml.WriteString(PortablePatient);
                    xml.WriteEndElement();
                    xml.WriteEndElement();   //Fin telecom

                    xml.WriteStartElement("invoice:online");
                    xml.WriteStartElement("invoice:email");
                    xml.WriteString(EmailPatient);
                    xml.WriteEndElement();
                    xml.WriteEndElement();   //Fin online
                }

                xml.WriteEndElement();   //Fin person
                xml.WriteEndElement();    //Fin de debitor   
            }
            else if (Entete["TypeEnvoi"].ToString() == "2")   //Si c'est un Tier (Tuteur etc...)
            {
                xml.WriteStartElement("invoice:debitor");

                //On vérifie qu'on a bien un GLN sur 13 caractères
                xml.WriteAttributeString("ean_party", "2000000000008");   //GLN Passe partout                   

                //On décompose l'adresse
                string Tier = Entete["AdresseDestinataire"].ToString();
                string Tier_nom = "";
                string Tier_adr = "";
                string Tier_comm = "";
                string Tier_cp = "";
                string Tier_city = "";
                string Tier_BoitePostale = "";
                int i;

                Tier = RemplaceCaractere(Entete["AdresseDestinataire"].ToString());
                Tier = Tier.Replace(",", " ");
                Tier = Tier.Replace("\r\n", ",");
                string[] TabTier = Tier.Split(',');

                Tier_nom = TabTier[0];
                Tier_adr = TabTier[1];
                Tier_comm = TabTier[2];

                if (Tier_comm != "")
                {
                    i = Tier_comm.IndexOf(" ", 0, Tier_comm.Length - 1);
                    int l = Tier_comm.Length;

                    if (i > 3)
                    {
                        Tier_cp = Tier_comm.Substring(0, i);
                        Tier_city = Tier_comm.Substring(i + 1, l - i - 1);
                    }
                }

                xml.WriteStartElement("invoice:company");
                xml.WriteStartElement("invoice:companyname");
                xml.WriteString(Tier_nom);
                xml.WriteEndElement();
                /*xml.WriteStartElement("invoice:department");
                xml.WriteString(Tier_city);
                xml.WriteEndElement();*/

                //verifier que l'adresse lengh < 25, if >25 , couper le mot rue, Ave, etc.
                int k = 0;
                int x = Tier_adr.Length;
                string first;
                Tier_adr.ToUpper();

                if (Tier_adr.Length > 25)
                {
                    try
                    {
                        k = Tier_adr.Trim().IndexOf(" ", 1, 12);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("{0} Exception caught.", ex);
                        MessageBox.Show("Erreur sur l''adresse de la facture " + IdFacture.ToString());
                    }

                    //i = guarantor_adr.IndexOf(" ",1,10);
                    first = Tier_adr.Substring(0, (k));
                    if (first == "AVENUE")
                        first = "AV";
                    else if (first == "RUE")
                        first = "R";
                    else if (first == "RUE")
                        first = "R";
                    else if (first == "LOTISSEMENT")
                        first = "LOT";
                    else if (first == "ROUTE")
                        first = "RTE";
                    else if (first == "BOULEVARD")
                        first = "BD";
                    else if (first == "ROND-POINT")
                        first = "RP";

                    Tier_adr = first + " " + Tier_adr.Substring((k + 1), (x - k - 1));
                }

                while (Tier_adr.Length > 25)
                {
                    x = Tier_adr.Length;
                    k = Tier_adr.IndexOf(" ", 0, x - 1);
                    Tier_BoitePostale = Tier_adr.Substring(0, (k));
                    first = Tier_adr.Substring(k + 1, x - k - 1);
                    Tier_adr = first;
                }

                if (Tier_city.ToUpper() == "LANCY")
                {
                    if (Tier_cp == "1213")
                        Tier_city = "PETIT LANCY";
                    else if (Tier_cp == "1212")
                        Tier_city = "GRAND LANCY";
                }

                xml.WriteStartElement("invoice:postal");
                //On est limité à 35 de longueur pour la rue si ça dépasse on ne met pas la rue

                if ((Tier_adr + " " + Tier_BoitePostale).Length <= 35)
                {
                    xml.WriteStartElement("invoice:street");
                    xml.WriteString(Tier_adr + " " + Tier_BoitePostale);
                    xml.WriteEndElement();
                }

                xml.WriteStartElement("invoice:zip");
                xml.WriteAttributeString("countrycode", "CH");
                xml.WriteString(Tier_cp);
                xml.WriteEndElement();
                xml.WriteStartElement("invoice:city");
                xml.WriteString(Tier_city);
                xml.WriteEndElement();
                xml.WriteEndElement();  //Fin postal                                                                                       
                xml.WriteEndElement();  //Fin de company      
                xml.WriteEndElement();  //Fin debitor                
            }
            else if (Entete["TypeEnvoi"].ToString() == "3")     //C'est donc un Tier Payant (Assurance)
            {
                //Tiers Payant (Debitor = Assurance)
                xml.WriteStartElement("invoice:debitor");

                //On vérifie qu'on a bien un GLN sur 13 caractères
                if ((Entete["AssEAN"] != DBNull.Value) && (Entete["AssEAN"].ToString() != "") && Entete["AssEAN"].ToString() != "0000000000000")
                {
                    if (Entete["AssEAN"].ToString().Substring(0, 3) == "760")
                    {
                        xml.WriteAttributeString("ean_party", Entete["AssEAN"].ToString());
                        AssuranceEAN = Entete["AssEAN"].ToString();
                    }
                    else
                        xml.WriteAttributeString("ean_party", "2000000000008");   //GLN Passe partout    
                }
                else xml.WriteAttributeString("ean_party", "2000000000008");   //GLN Passe partout               

                xml.WriteStartElement("invoice:company");
                xml.WriteStartElement("invoice:companyname");
                xml.WriteString(Entete["AssNom"].ToString());
                xml.WriteEndElement();
                /*xml.WriteStartElement("invoice:department");
                xml.WriteString(Entete["AssCommune"].ToString());
                xml.WriteEndElement();*/
                xml.WriteStartElement("invoice:postal");

                //On est limité à 35 de longueur pour la rue si ça dépasse on ne met pas la rue
                if ((Entete["AssRue"].ToString() + " " + Entete["AssNumDansRue"].ToString()).Length <= 35)
                {
                    xml.WriteStartElement("invoice:street");
                    xml.WriteString(Entete["AssRue"].ToString() + " " + Entete["AssNumDansRue"].ToString());
                    xml.WriteEndElement();
                }

                xml.WriteStartElement("invoice:zip");
                xml.WriteAttributeString("countrycode", "CH");
                xml.WriteString(Entete["AssNp"].ToString());
                xml.WriteEndElement();
                xml.WriteStartElement("invoice:city");
                xml.WriteString(Entete["AssCommune"].ToString());
                xml.WriteEndElement();
                xml.WriteEndElement();   //Fin de postal

                if (Entete["AssTelephone"] != DBNull.Value && Entete["AssTelephone"].ToString() != "")
                {
                    xml.WriteStartElement("invoice:telecom");
                    xml.WriteStartElement("invoice:phone");
                    xml.WriteString(Entete["AssTelephone"].ToString());
                    xml.WriteEndElement();

                    Console.WriteLine("fax: " + Entete["AssFax"].ToString());

                    if (Entete["AssFax"] != DBNull.Value && Entete["AssFax"].ToString() != "")
                    {
                        xml.WriteStartElement("invoice:fax");
                        xml.WriteString(Entete["AssFax"].ToString());
                        xml.WriteEndElement();
                    }
                    xml.WriteEndElement(); //("invoice:telecom")
                }

                xml.WriteEndElement();     //Fin de company      
                xml.WriteEndElement();  //Fin debitor  
            }
            //Fin debitor


            //Debut provider
            xml.WriteStartElement("invoice:provider");
            xml.WriteAttributeString("ean_party", "7601002083034");     //EAN SOS
            xml.WriteAttributeString("zsr", Concordat);

            xml.WriteStartElement("invoice:person");
            //xml.WriteAttributeString("title", "Dr.med");
            //xml.WriteAttributeString("title", "");
            //xml.WriteAttributeString("salutation", ""); 
            xml.WriteStartElement("invoice:familyname");
            //xml.WriteString(Entete["NomGeneve"].ToString());
            xml.WriteString("Cite Calvin SA");
            xml.WriteEndElement();
            xml.WriteStartElement("invoice:givenname");
            //xml.WriteString(Entete["PrenomGeneve"].ToString());
            xml.WriteString("SOS MEDECINS");
            xml.WriteEndElement();
            //xml.WriteStartElement("invoice:subaddressing"); 
            //xml.WriteEndElement();
            xml.WriteStartElement("invoice:postal");
            //xml.WriteStartElement("invoice:pobox"); 
            //xml.WriteEndElement();
            xml.WriteStartElement("invoice:street");
            xml.WriteString("rue Louis Favre 43");
            xml.WriteEndElement();
            xml.WriteStartElement("invoice:zip");
            xml.WriteAttributeString("countrycode", "CH");
            xml.WriteString("1201");
            xml.WriteEndElement();
            xml.WriteStartElement("invoice:city");
            xml.WriteString("Geneve");
            xml.WriteEndElement();
            xml.WriteEndElement();    //Fin postal

            /*xml.WriteStartElement("invoice:telecom");
           xml.WriteStartElement("invoice:phone"); xml.WriteString(""); xml.WriteEndElement();
           xml.WriteStartElement("invoice:fax"); xml.WriteString(""); xml.WriteEndElement();
           xml.WriteEndElement();
           xml.WriteStartElement("invoice:online");
           xml.WriteStartElement("invoice:email"); xml.WriteString(""); xml.WriteEndElement();
           xml.WriteStartElement("invoice:url"); xml.WriteString(""); xml.WriteEndElement();*/

            xml.WriteEndElement();    //Fin personne
            xml.WriteEndElement();    //Fin de provider     
                                      //Fin provider

            //Debut insurance 
            //On vérifie qu'on a bien un GLN sur 13 caractères
            if ((Entete["AssEAN"] != DBNull.Value) && (Entete["AssEAN"].ToString() != ""))
            {
                if (Entete["AssEAN"].ToString().Substring(0, 3) == "760")
                {
                    //BODY-->insurance
                    //Assurance
                    xml.WriteStartElement("invoice:insurance");
                    xml.WriteAttributeString("ean_party", Entete["AssEAN"].ToString());


                    xml.WriteStartElement("invoice:company");
                    xml.WriteStartElement("invoice:companyname");
                    xml.WriteString(Entete["AssNom"].ToString());
                    xml.WriteEndElement();
                    xml.WriteStartElement("invoice:department");
                    xml.WriteString(Entete["AssCommune"].ToString());
                    xml.WriteEndElement();
                    //xml.WriteStartElement("invoice:subaddressing");
                    //xml.WriteString(assurancee.Ass_Commentaire); 
                    //xml.WriteEndElement();
                    xml.WriteStartElement("invoice:postal");
                    // xml.WriteStartElement("invoice:pobox");
                    //xml.WriteString(assurancee.Adresse);
                    //xml.WriteEndElement();

                    //On est limité à 35 de longueur pour la rue si ça dépasse on ne met pas la rue
                    if ((Entete["AssRue"].ToString() + " " + Entete["AssNumDansRue"].ToString()).Length <= 35)
                    {
                        xml.WriteStartElement("invoice:street");
                        xml.WriteString(Entete["AssRue"].ToString() + " " + Entete["AssNumDansRue"].ToString());
                        xml.WriteEndElement();
                    }

                    xml.WriteStartElement("invoice:zip");
                    xml.WriteAttributeString("countrycode", "CH");
                    xml.WriteString(Entete["AssNp"].ToString());
                    xml.WriteEndElement();
                    xml.WriteStartElement("invoice:city");
                    xml.WriteString(Entete["AssCommune"].ToString());
                    xml.WriteEndElement();
                    xml.WriteEndElement();   //Fin de postal


                    if (Entete["AssTelephone"] != DBNull.Value && Entete["AssTelephone"].ToString() != "")
                    {
                        xml.WriteStartElement("invoice:telecom");
                        xml.WriteStartElement("invoice:phone");
                        xml.WriteString(Entete["AssTelephone"].ToString());
                        xml.WriteEndElement();

                        Console.WriteLine("fax: " + Entete["AssFax"].ToString());

                        if (Entete["AssFax"] != DBNull.Value && Entete["AssFax"].ToString() != "")
                        {
                            xml.WriteStartElement("invoice:fax");
                            xml.WriteString(Entete["AssFax"].ToString());
                            xml.WriteEndElement();
                        }
                        xml.WriteEndElement(); //("invoice:telecom")
                    }

                    xml.WriteEndElement();     //Fin de company      
                    xml.WriteEndElement();  //Fin d'assurance  
                }
            }
            //Fin insurance


            //Debut invoice:Patient
            xml.WriteStartElement("invoice:patient");
            xml.WriteAttributeString("gender", sexeP);
            xml.WriteAttributeString("birthdate", DatenaissP);

            //N° AVS du patient, seulement s'il y en a un
            if (Entete["FactNum_AVS"].ToString() != "" && Entete["FactNum_AVS"] != DBNull.Value)
            {
                string AVS = Entete["FactNum_AVS"].ToString();

                //On enlève les blancs
                AVS = AVS.Replace(" ", "");

                if (AVS.Length == 13 && AVS.Substring(0, 3) == "756")
                    xml.WriteAttributeString("ssn", AVS);
                else MessageBox.Show("Erreur dans le n° d'avs du patient " + nom + " " + prenom + " N° AVS " + AVS + " L: " + Entete["FactNum_AVS"].ToString().Length.ToString() + "\r\n cette erreur ne bloque pas la facture.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //if ((!Entete.IsDBNull(26)) && (Entete["FactNum_AVS"].ToString() != ""))
            //    xml.WriteAttributeString("ssn", Entete["FactNum_AVS"].ToString());

            xml.WriteStartElement("invoice:person");
            xml.WriteAttributeString("salutation", title);
            //xml.WriteAttributeString("title", "");
            xml.WriteStartElement("invoice:familyname");
            xml.WriteString(nom);
            xml.WriteEndElement();
            xml.WriteStartElement("invoice:givenname");
            xml.WriteString(prenom);
            xml.WriteEndElement();

            if (AdrChez != "")
            {
                xml.WriteStartElement("invoice:subaddressing");
                xml.WriteString("C/o " + AdrChez);
                xml.WriteEndElement();
            }

            xml.WriteStartElement("invoice:postal");
            //xml.WriteStartElement("invoice:pobox");
            //xml.WriteEndElement();
            //On est limité à 35 de longueur pour la rue si ça dépasse on ne met pas la rue
            if (rue.Length <= 35)
            {
                xml.WriteStartElement("invoice:street");
                xml.WriteString(rue);
                xml.WriteEndElement();
            }
            xml.WriteStartElement("invoice:zip");
            xml.WriteAttributeString("countrycode", "CH");
            xml.WriteString(codepostal);
            xml.WriteEndElement();
            xml.WriteStartElement("invoice:city");
            xml.WriteString(commune);
            xml.WriteEndElement();
            xml.WriteEndElement();   //Fin postal                    

            //Si c'est un portable et un email valide
            if (PortablePatient != "" && EmailPatient != "")
            {
                xml.WriteStartElement("invoice:telecom");
                xml.WriteStartElement("invoice:phone");
                xml.WriteString(PortablePatient);
                xml.WriteEndElement();
                xml.WriteEndElement();   //Fin telecom

                xml.WriteStartElement("invoice:online");
                xml.WriteStartElement("invoice:email");
                xml.WriteString(EmailPatient);
                xml.WriteEndElement();
                xml.WriteEndElement();   //Fin online
            }

            xml.WriteEndElement();   //Fin person
            xml.WriteEndElement();   //Fin invoice:patient  
                                     //Fin patient


            #region Garantor
            //#######  Debut Invoice:garantor ...C'est le patient ou le tier
            xml.WriteStartElement("invoice:guarantor");

            //Informations relatives au Guarantor
            string guarantor = Entete["AdresseDestinataire"].ToString();
            string guarantor_nom = "";
            string guarantor_adr = "";
            string guarantor_comm = "";
            string guarantor_chez = "";
            string code_postal = "";
            string city = "";

            string PoBox = "";
            int j;


            if (Entete["TypeEnvoi"].ToString() != "2")      //TG ou TP on met dans les 2 cas les coordonnées du patient
            {
                xml.WriteStartElement("invoice:person");
                xml.WriteAttributeString("salutation", title);
                //xml.WriteAttributeString("title", title);
                xml.WriteStartElement("invoice:familyname");
                xml.WriteString(nom);
                xml.WriteEndElement();
                xml.WriteStartElement("invoice:givenname");
                xml.WriteString(prenom);
                xml.WriteEndElement();

                if (AdrChez != "")
                {
                    xml.WriteStartElement("invoice:subaddressing");
                    xml.WriteString("C/o " + AdrChez);
                    xml.WriteEndElement();
                }

                xml.WriteStartElement("invoice:postal");
                //xml.WriteStartElement("invoice:pobox");
                //xml.WriteEndElement();
                //On est limité à 35 de longueur pour la rue si ça dépasse on ne met pas la rue
                if (rue.Length <= 35)
                {
                    xml.WriteStartElement("invoice:street");
                    xml.WriteString(rue);
                    xml.WriteEndElement();
                }
                xml.WriteStartElement("invoice:zip");
                xml.WriteAttributeString("countrycode", "CH");
                xml.WriteString(codepostal);
                xml.WriteEndElement();
                xml.WriteStartElement("invoice:city");
                xml.WriteString(commune);
                xml.WriteEndElement();
                xml.WriteEndElement();   //Fin postal                    

                //Si c'est un portable et un email valide
                if (PortablePatient != "" && EmailPatient != "")
                {
                    xml.WriteStartElement("invoice:telecom");
                    xml.WriteStartElement("invoice:phone");
                    xml.WriteString(PortablePatient);
                    xml.WriteEndElement();
                    xml.WriteEndElement();   //Fin telecom

                    xml.WriteStartElement("invoice:online");
                    xml.WriteStartElement("invoice:email");
                    xml.WriteString(EmailPatient);
                    xml.WriteEndElement();
                    xml.WriteEndElement();   //Fin online
                }

                xml.WriteEndElement();  //fin invoice:person ou company  
            }
            else    //C'est un Tier
            {
                //On décompose le Garantor dans depuis l'adresse de destination
                guarantor = RemplaceCaractere(Entete["AdresseDestinataire"].ToString());
                guarantor = guarantor.Replace(",", " ");
                guarantor = guarantor.Replace("\r\n", ",");
                string[] TabGuarantor = guarantor.Split(',');

                if (TabGuarantor.Length == 3)   //3 lignes dans l'adresse
                {
                    guarantor_nom = TabGuarantor[0];
                    guarantor_adr = TabGuarantor[1];
                    guarantor_comm = TabGuarantor[2];

                    if (guarantor_comm != "")
                    {
                        j = guarantor_comm.IndexOf(" ", 0, guarantor_comm.Length - 1);
                        int ii = guarantor_comm.Length;
                        if (j > 3)
                        {
                            code_postal = guarantor_comm.Substring(0, j);
                            city = guarantor_comm.Substring(j + 1, ii - j - 1);
                        }
                    }

                    xml.WriteStartElement("invoice:company");
                    xml.WriteStartElement("invoice:companyname");
                    xml.WriteString(guarantor_nom);
                    xml.WriteEndElement();
                    xml.WriteStartElement("invoice:department");
                    xml.WriteString(city);
                    xml.WriteEndElement();


                    //verifier que l'adresse lengh < 25, if >25 , couper le mot rue, Ave, etc.
                    int i = 0;
                    int x = guarantor_adr.Length;
                    string first;
                    guarantor_adr.ToUpper();
                    if (guarantor_adr.Length > 25)
                    {
                        try
                        {
                            i = guarantor_adr.Trim().IndexOf(" ", 1, 12);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("{0} Exception caught.", ex);
                            MessageBox.Show("Erreur sur l''adresse de la facture " + IdFacture.ToString());
                        }

                        //i = guarantor_adr.IndexOf(" ",1,10);
                        first = guarantor_adr.Substring(0, (i));
                        if (first == "AVENUE")
                            first = "AV";
                        else if (first == "RUE")
                            first = "R";
                        else if (first == "RUE")
                            first = "R";
                        else if (first == "LOTISSEMENT")
                            first = "LOT";
                        else if (first == "ROUTE")
                            first = "RTE";
                        else if (first == "BOULEVARD")
                            first = "BD";
                        else if (first == "ROND-POINT")
                            first = "RP";

                        guarantor_adr = first + " " + guarantor_adr.Substring((i + 1), (x - i - 1));

                    }
                    while (guarantor_adr.Length > 25)
                    {
                        x = guarantor_adr.Length;
                        i = guarantor_adr.IndexOf(" ", 0, x - 1);
                        PoBox = guarantor_adr.Substring(0, (i));
                        first = guarantor_adr.Substring(i + 1, x - i - 1);
                        guarantor_adr = first;
                    }

                    if (city.ToUpper() == "LANCY")
                    {
                        if (code_postal == "1213")
                            city = "PETIT LANCY";
                        else if (code_postal == "1212")
                            city = "GRAND LANCY";
                    }


                    xml.WriteStartElement("invoice:postal");
                    //On est limité à 35 de longueur pour la rue si ça dépasse on ne met pas la rue
                    if ((guarantor_adr + " " + PoBox).Length <= 35)
                    {
                        xml.WriteStartElement("invoice:street");
                        xml.WriteString(guarantor_adr + " " + PoBox);
                        xml.WriteEndElement();
                    }
                    xml.WriteStartElement("invoice:zip");
                    xml.WriteAttributeString("countrycode", "CH");
                    xml.WriteString(code_postal);
                    xml.WriteEndElement();
                    xml.WriteStartElement("invoice:city");
                    xml.WriteString(city);
                    xml.WriteEndElement();
                    xml.WriteEndElement();  //Fin postal             

                    xml.WriteEndElement();  //fin invoice:person ou company                
                }
                else if (TabGuarantor.Length == 4)   //adresse de 4 lignes
                {
                    guarantor_nom = TabGuarantor[0];
                    guarantor_chez = TabGuarantor[1];
                    guarantor_adr = TabGuarantor[2];
                    guarantor_comm = TabGuarantor[3];

                    j = guarantor_comm.Length;
                    code_postal = guarantor_comm.Substring(0, 4);
                    city = guarantor_comm.Substring(5, j - 5);


                    xml.WriteStartElement("invoice:person");
                    //xml.WriteAttributeString("salutation", guarantor_chez);
                    xml.WriteStartElement("invoice:familyname");
                    xml.WriteString(guarantor_nom);
                    xml.WriteEndElement();
                    xml.WriteStartElement("invoice:givenname");
                    xml.WriteString(" ");
                    xml.WriteEndElement();

                    if (guarantor_chez != "")
                    {
                        xml.WriteStartElement("invoice:subaddressing");
                        xml.WriteString(guarantor_chez);
                        xml.WriteEndElement();
                    }

                    //verifier que l'adresse lengh < 25, if >25 , couper le mot rue, Ave, etc.
                    int i = 0;
                    int x = guarantor_adr.Length;
                    string first;
                    guarantor_adr.ToUpper();
                    if (guarantor_adr.Length > 25)
                    {
                        try
                        {
                            i = guarantor_adr.Trim().IndexOf(" ", 1, 12);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("{0} Exception caught.", ex);
                            MessageBox.Show("Erreur sur l''adresse de la facture " + IdFacture.ToString());
                        }

                        //i = guarantor_adr.IndexOf(" ",1,10);
                        first = guarantor_adr.Substring(0, (i));
                        if (first == "AVENUE")
                            first = "AV";
                        else if (first == "RUE")
                            first = "R";
                        else if (first == "RUE")
                            first = "R";
                        else if (first == "LOTISSEMENT")
                            first = "LOT";
                        else if (first == "ROUTE")
                            first = "RTE";
                        else if (first == "BOULEVARD")
                            first = "BD";
                        else if (first == "ROND-POINT")
                            first = "RP";

                        guarantor_adr = first + " " + guarantor_adr.Substring((i + 1), (x - i - 1));

                    }
                    while (guarantor_adr.Length > 25)
                    {
                        x = guarantor_adr.Length;
                        i = guarantor_adr.IndexOf(" ", 0, x - 1);
                        PoBox = guarantor_adr.Substring(0, (i));
                        first = guarantor_adr.Substring(i + 1, x - i - 1);
                        guarantor_adr = first;

                    }

                    if (city.ToUpper() == "LANCY")
                    {
                        if (code_postal == "1213")
                            city = "PETIT LANCY";
                        else if (code_postal == "1212")
                            city = "GRAND LANCY";
                    }

                    xml.WriteStartElement("invoice:postal");

                    //On est limité à 35 de longueur pour la rue si ça dépasse on ne met pas la rue
                    if ((guarantor_adr + " " + PoBox).Length <= 35)
                    {
                        xml.WriteStartElement("invoice:street");
                        xml.WriteString(guarantor_adr + " " + PoBox);
                        xml.WriteEndElement();
                    }
                    xml.WriteStartElement("invoice:zip");
                    xml.WriteAttributeString("countrycode", "CH");
                    xml.WriteString(codepostal);
                    xml.WriteEndElement();
                    xml.WriteStartElement("invoice:city");
                    xml.WriteString(city);
                    xml.WriteEndElement();
                    xml.WriteEndElement();  //fin postal

                    xml.WriteEndElement();  //fin invoice:person ou company   
                }
            }
            xml.WriteEndElement();  //Fin garantor 

            #region anc garantor
            /*guarantor = RemplaceCaractere(Entete["AdresseDestinataire"].ToString());
            guarantor = guarantor.Replace(",", " ");
            guarantor = guarantor.Replace("\r\n", ",");
            string[] TabGuarantor = guarantor.Split(',');


            if (TabGuarantor.Length == 3)
            {
                guarantor_nom = TabGuarantor[0];

                guarantor_adr = TabGuarantor[1];
                guarantor_comm = TabGuarantor[2];
                if (guarantor_comm != "")
                {
                    j = guarantor_comm.IndexOf(" ", 0, guarantor_comm.Length - 1);
                    int ii = guarantor_comm.Length;
                    if (j > 3)
                    {
                        code_postal = guarantor_comm.Substring(0, j);
                        city = guarantor_comm.Substring(j + 1, ii - j - 1);
                    }
                }


                if (Entete["TypeEnvoi"].ToString() == "1")      //TG
                {
                    xml.WriteStartElement("invoice:person");
                    xml.WriteAttributeString("salutation", title);
                    //xml.WriteAttributeString("title", title);
                    xml.WriteStartElement("invoice:familyname");
                    xml.WriteString(nom);
                    xml.WriteEndElement();
                    xml.WriteStartElement("invoice:givenname");
                    xml.WriteString(prenom);
                    xml.WriteEndElement();
                }
                else
                {
                    xml.WriteStartElement("invoice:company");
                    xml.WriteStartElement("invoice:companyname");
                    xml.WriteString(guarantor_nom);
                    xml.WriteEndElement();
                    xml.WriteStartElement("invoice:department");
                    xml.WriteString(city);
                    xml.WriteEndElement();
                }

                //verifier que l'adresse lengh < 25, if >25 , couper le mot rue, Ave, etc.
                /*int i = 0;
                int x = guarantor_adr.Length;
                string first;
                guarantor_adr.ToUpper();
                if (guarantor_adr.Length > 25)
                {
                    try
                    {
                        i = guarantor_adr.Trim().IndexOf(" ", 1, 12);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("{0} Exception caught.", ex);
                        MessageBox.Show("Erreur sur l''adresse de la facture " + IdFacture.ToString());
                    }

                    //i = guarantor_adr.IndexOf(" ",1,10);
                    first = guarantor_adr.Substring(0, (i));
                    if (first == "AVENUE")
                        first = "AV";
                    else if (first == "RUE")
                        first = "R";
                    else if (first == "RUE")
                        first = "R";
                    else if (first == "LOTISSEMENT")
                        first = "LOT";
                    else if (first == "ROUTE")
                        first = "RTE";
                    else if (first == "BOULEVARD")
                        first = "BD";
                    else if (first == "ROND-POINT")
                        first = "RP";

                    guarantor_adr = first + " " + guarantor_adr.Substring((i + 1), (x - i - 1));

                }
                while (guarantor_adr.Length > 25)
                {
                    x = guarantor_adr.Length;
                    i = guarantor_adr.IndexOf(" ", 0, x - 1);
                    PoBox = guarantor_adr.Substring(0, (i));
                    first = guarantor_adr.Substring(i + 1, x - i - 1);
                    guarantor_adr = first;
                }

                if (city.ToUpper() == "LANCY")
                {
                    if (code_postal == "1213")
                        city = "PETIT LANCY";
                    else if (code_postal == "1212")
                        city = "GRAND LANCY";
                }



                xml.WriteStartElement("invoice:postal");
                //On est limité à 35 de longueur pour la rue si ça dépasse on ne met pas la rue
                if ((guarantor_adr + " " + PoBox).Length <= 35)
                {
                    xml.WriteStartElement("invoice:street");
                    xml.WriteString(guarantor_adr + " " + PoBox);
                    xml.WriteEndElement();
                }
                xml.WriteStartElement("invoice:zip");
                xml.WriteAttributeString("countrycode", "CH");
                xml.WriteString(code_postal);
                xml.WriteEndElement();
                xml.WriteStartElement("invoice:city");
                xml.WriteString(city);
                xml.WriteEndElement();
                xml.WriteEndElement();  //Fin postal             

                xml.WriteEndElement();  //fin invoice:person ou company                
            }
            else if (TabGuarantor.Length == 4)
            {
                guarantor_nom = TabGuarantor[0];
                guarantor_chez = TabGuarantor[1];
                guarantor_adr = TabGuarantor[2];
                guarantor_comm = TabGuarantor[3];
                j = guarantor_comm.Length;
                code_postal = guarantor_comm.Substring(0, 4);
                city = guarantor_comm.Substring(5, j - 5);

                if (Entete["TypeDestinataire"].ToString() == "0")
                {
                    xml.WriteStartElement("invoice:person");
                   // xml.WriteAttributeString("salutation", guarantor_chez);
                    xml.WriteAttributeString("title", title);
                    xml.WriteStartElement("invoice:familyname");
                    xml.WriteString(nom);
                    xml.WriteEndElement();
                    xml.WriteStartElement("invoice:givenname");
                    xml.WriteString(prenom);
                    xml.WriteEndElement();

                    if (guarantor_chez != "")
                    {
                        xml.WriteStartElement("invoice:subaddressing");
                        xml.WriteString(guarantor_chez);
                        xml.WriteEndElement();
                    }
                }
                else
                {
                    xml.WriteStartElement("invoice:person");
                    //xml.WriteAttributeString("salutation", guarantor_chez);
                    xml.WriteStartElement("invoice:familyname");
                    xml.WriteString(guarantor_nom);
                    xml.WriteEndElement();
                    xml.WriteStartElement("invoice:givenname");
                    xml.WriteString(" ");
                    xml.WriteEndElement();

                    if (guarantor_chez != "")
                    {
                        xml.WriteStartElement("invoice:subaddressing");
                        xml.WriteString(guarantor_chez);
                        xml.WriteEndElement();
                    }
                }


                //verifier que l'adresse lengh < 25, if >25 , couper le mot rue, Ave, etc.
                int i = 0;
                int x = guarantor_adr.Length;
                string first;
                guarantor_adr.ToUpper();
                if (guarantor_adr.Length > 25)
                {
                    try
                    {
                        i = guarantor_adr.Trim().IndexOf(" ", 1, 12);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("{0} Exception caught.", ex);
                        MessageBox.Show("Erreur sur l''adresse de la facture " + IdFacture.ToString());
                    }

                    //i = guarantor_adr.IndexOf(" ",1,10);
                    first = guarantor_adr.Substring(0, (i));
                    if (first == "AVENUE")
                        first = "AV";
                    else if (first == "RUE")
                        first = "R";
                    else if (first == "RUE")
                        first = "R";
                    else if (first == "LOTISSEMENT")
                        first = "LOT";
                    else if (first == "ROUTE")
                        first = "RTE";
                    else if (first == "BOULEVARD")
                        first = "BD";
                    else if (first == "ROND-POINT")
                        first = "RP";

                    guarantor_adr = first + " " + guarantor_adr.Substring((i + 1), (x - i - 1));

                }
                while (guarantor_adr.Length > 25)
                {
                    x = guarantor_adr.Length;
                    i = guarantor_adr.IndexOf(" ", 0, x - 1);
                    PoBox = guarantor_adr.Substring(0, (i));
                    first = guarantor_adr.Substring(i + 1, x - i - 1);
                    guarantor_adr = first;

                }

                if (city.ToUpper() == "LANCY")
                {
                    if (code_postal == "1213")
                        city = "PETIT LANCY";
                    else if (code_postal == "1212")
                        city = "GRAND LANCY";
                }

                xml.WriteStartElement("invoice:postal");

                //On est limité à 35 de longueur pour la rue si ça dépasse on ne met pas la rue
                if ((guarantor_adr + " " + PoBox).Length <= 35)
                {
                    xml.WriteStartElement("invoice:street");
                    xml.WriteString(guarantor_adr + " " + PoBox);
                    xml.WriteEndElement();
                }
                xml.WriteStartElement("invoice:zip");
                xml.WriteAttributeString("countrycode", "CH");
                xml.WriteString(codepostal);
                xml.WriteEndElement();
                xml.WriteStartElement("invoice:city");
                xml.WriteString(city);
                xml.WriteEndElement();
                xml.WriteEndElement();  //fin postal

                xml.WriteEndElement();  //fin invoice:person ou company              
            }*/
            #endregion


            //### Fin garantor ###
            #endregion

            //Début balance
            string amount_obligations = PrepareBalance(xml);     //Montant total de la facture
            Double amount_prepaid = Math.Round(Convert.ToDouble(amount_obligations) - Convert.ToDouble(Entete["Solde"].ToString()), 2);  //Déjà payé = TotalF - Solde

            //parfois on obtient -0.01 donc on met 0
            if (amount_prepaid < 1)
                amount_prepaid = 0;

            Double amount_due = Math.Round(Convert.ToDouble(Entete["Solde"].ToString())*20, MidpointRounding.AwayFromZero)/20;  //Due = obligation - prepaid;   Arrondi suisse à 0.05

            xml.WriteStartElement("invoice:balance");

            //Est un renvoi de facture 10%?
            RenvFact10p = int.Parse(Entete["RenvFact10p"].ToString());

            /*if (Entete["TypeDestinataire"].ToString() != "2")
            {
                //Tiers Garant;
                //Si c'est un 10% (sous entendu, on renvoi le solde, car il peut y avoir de la franchise avec les 10%)
                if (RenvFact10p == 1)
                    amount_due = Math.Round((Convert.ToDouble(amount_obligations) / 10f)*20,MidpointRounding.AwayFromZero)/20; // Arrondi suisse à 0.05
                    
            }*/
                        
            xml.WriteAttributeString("amount_obligations", amount_obligations);   //Total facture ou rappel (si = montant facture)
            xml.WriteAttributeString("amount_due", amount_due.ToString());           //Due = obligation - prepaid = (Solde)
            xml.WriteAttributeString("currency", "CHF");
            xml.WriteAttributeString("amount", amount_obligations);             //Total facture ou rappel                                    

            //if (Entete["TypeDestinataire"].ToString() != "2")   //Tier garant ou Tier => Patient ou Tier
                if (Entete["TypeEnvoi"].ToString() != "3")
                {
                xml.WriteAttributeString("amount_prepaid", amount_prepaid.ToString());           //Montant déjà payé (obligation - solde) uniquement pour Tier Garant           

                xml.WriteAttributeString("amount_reminder", "0");
                
                //Est ce un rappel?    ######### A activer pour la mise en place des frais de rappel #######
                /*switch (CreerRappelNum)
                {
                    case 0: xml.WriteAttributeString("amount_reminder", "0"); break;        //C'est pas un rappel
                    case 1: xml.WriteAttributeString("amount_reminder", "20"); break;        //1er rappel
                    case 2: xml.WriteAttributeString("amount_reminder", "50"); break;        //2eme Rappel
                } */              
            }
            else
                xml.WriteAttributeString("amount_reminder", "0");    //Pas de frais de rappel pour les assurances 
            
            //invoice:vat et vat_rate
            xml.WriteStartElement("invoice:vat");
            xml.WriteAttributeString("vat", "0.00");

            xml.WriteStartElement("invoice:vat_rate");

            xml.WriteAttributeString("vat_rate", "0");
            xml.WriteAttributeString("amount", amount_due.ToString());
            xml.WriteAttributeString("vat", "0.00");
            xml.WriteEndElement();    //Fin vat_rate
            xml.WriteEndElement();    //Fin vat
            xml.WriteEndElement();    //Fin invoice:balance
            //Fin balance

            xml.WriteEndElement();  //fin invoice:Tier_payant ou invoice:tier_garant
            //Fin tiers_garant ou tiers_payant

            //string TotalFacture = PrepareBalance(xml);

            //Edite les éléments pour créer le Coding Line et le reference_number
            string reference_number = CalculRefNumber(IdFacture, Entete["CodeIntervenant"].ToString());

            string Coding_Line = CodingLine(reference_number, amount_due.ToString());

            //#######################################################################################################
            //Debut esr9            
            xml.WriteStartElement("invoice:esrQR");
            xml.WriteAttributeString("type", "esrQR");
            xml.WriteAttributeString("iban", "CH7630000002120014992");
            xml.WriteAttributeString("reference_number", reference_number.Replace(" ", string.Empty)); //str2

            xml.WriteStartElement("invoice:bank");
            xml.WriteStartElement("invoice:company");
            xml.WriteStartElement("invoice:companyname");
            xml.WriteString("PostFiance SA");
            xml.WriteEndElement();
            xml.WriteStartElement("invoice:postal");
            xml.WriteStartElement("invoice:street");
            xml.WriteString("Mingerstrasse 20");
            xml.WriteEndElement();
            xml.WriteStartElement("invoice:zip");
            xml.WriteAttributeString("countrycode", "CH");
            xml.WriteString("3030");
            xml.WriteEndElement();
            xml.WriteStartElement("invoice:city");
            xml.WriteString("Berne");
            xml.WriteEndElement();
            xml.WriteEndElement();   //Fin postal
            xml.WriteEndElement();   //Fin company
            xml.WriteEndElement();   //Fin Bank


            xml.WriteStartElement("invoice:creditor");
            xml.WriteStartElement("invoice:company");
            xml.WriteStartElement("invoice:companyname");
            xml.WriteString("SOS MEDECINS Cite Calvin SA");
            xml.WriteEndElement();
            xml.WriteStartElement("invoice:postal");
            xml.WriteStartElement("invoice:street");
            xml.WriteString("rue Louis Favre 43");
            xml.WriteEndElement();
            xml.WriteStartElement("invoice:zip");
            xml.WriteAttributeString("countrycode", "CH");
            xml.WriteString("1201");
            xml.WriteEndElement();
            xml.WriteStartElement("invoice:city");
            xml.WriteString("Geneve");
            xml.WriteEndElement();
            xml.WriteEndElement();   //Fin postal
            xml.WriteEndElement();   //Fin company
            xml.WriteEndElement();   //Fin creditor

            xml.WriteEndElement();    //Fin invoice:esr9  
            //Fin esr9

            //***************************Entête des traitements***********************                       
            //Type Tarif Loi: LAA, LAMAL etc....et le traitement(accident, maladie, prevention)
            string tarif = Entete["Tarif"].ToString(); //5 Fédéral, 6 Cantonal GE, 9  Cantonal VS, 10 Cantonal GE 0.94
            String MotifTraitement = Entete["TTT"].ToString();

            if (MotifTraitement == "1")         //maladie kvg
            {
                xml.WriteStartElement("invoice:kvg");
                                
                if (Entete["Num_Assure"].ToString() != "" && Entete["Num_Assure"] != DBNull.Value)
                    xml.WriteAttributeString("insured_id", Entete["Num_Assure"].ToString()); 

                xml.WriteEndElement();   //fin invoice:kvg    

                xml.WriteStartElement("invoice:treatment");
                xml.WriteAttributeString("date_begin", DAP);
                xml.WriteAttributeString("date_end", DAP);

                //A activer si on facture un jour à Verbier                
                //if (tarif == "9")
                //    xml.WriteAttributeString("canton", "VS");
                //    else xml.WriteAttributeString("canton", "GE");
                
                xml.WriteAttributeString("canton", "GE");
                xml.WriteAttributeString("reason", "disease");
                xml.WriteEndElement();    //Fin treatment

                //  xml.WriteAttributeString("service_locality", "practice");                                
                //xml.WriteAttributeString("patient_id", Entete["RefPatient"].ToString());
                //xml.WriteAttributeString("case_date", DAP);             
            }
            else if (MotifTraitement == "2")     //Accident
            {
                if (tarif == "5")    //Si c'est le tarif Fédéral... => Laa, LAM, LAI ...SetUVG
                {
                    if (Entete["DateAccident"].ToString() == "")
                        frmEnvoi.ActiveForm.Tag = frmEnvoi.ActiveForm.Tag + "\r\n" + Entete["NFacture"].ToString();
                    else
                    {
                        //formatage de la date accident (yyyy-mm-ddT00:00:00)
                        DateTime DateAccident = DateTime.Parse(String.Format("{0:d/M/yyyy}", DateTime.Parse(Entete["DateAccident"].ToString())));
                        String DateAccidentString = String.Format("{0:s}", DateAccident);

                        xml.WriteStartElement("invoice:uvg");

                        if (Entete["Num_Assure"].ToString() != "" && Entete["Num_Assure"] != DBNull.Value)
                            xml.WriteAttributeString("insured_id", Entete["Num_Assure"].ToString()); 

                        if (Entete["FactNum_AVS"].ToString() != "" && Entete["FactNum_AVS"] != DBNull.Value)
                        {
                            string AVS = Entete["FactNum_AVS"].ToString();

                            if (AVS.Length == 13 && AVS.Substring(0, 3) == "756")
                                xml.WriteAttributeString("ssn", AVS);
                            else MessageBox.Show("Erreur dans le n° d'avs du patient " + nom + " " + prenom + " N° AVS" + AVS + " L: " + Entete["FactNum_AVS"].ToString().Length.ToString() + "\r\n cette erreur ne bloque pas la facture.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        if (Entete["NAccident"] != DBNull.Value && Entete["NAccident"].ToString() != "")
                        {
                            xml.WriteAttributeString("case_id", Entete["NAccident"].ToString());
                        }

                        xml.WriteAttributeString("case_date", DateAccidentString);
                        xml.WriteEndElement();   //Fin UVG

                        xml.WriteStartElement("invoice:treatment");
                        xml.WriteAttributeString("date_begin", DAP);
                        xml.WriteAttributeString("date_end", DAP);                                    

                        xml.WriteAttributeString("canton", "GE");
                        xml.WriteAttributeString("reason", "accident");
                        xml.WriteEndElement();    //Fin treatment                                                                                                                                               
                    }
                }               
                else if (tarif == "6" || tarif == "9")  //Si c'est le tarif Cantonal...=> LAMal ...KVG
                {
                    if (Entete["DateAccident"].ToString() == "")
                        frmEnvoi.ActiveForm.Tag = frmEnvoi.ActiveForm.Tag + "\r\n" + Entete["NFacture"].ToString();
                    else
                    {
                        //formatage de la date accident (yyyy-mm-ddT00:00:00)
                        DateTime DateAccident = DateTime.Parse(String.Format("{0:d/M/yyyy}", DateTime.Parse(Entete["DateAccident"].ToString())));
                        String DateAccidentString = String.Format("{0:s}", DateAccident);

                        xml.WriteStartElement("invoice:kvg");

                        if (Entete["Num_Assure"].ToString() != "" && Entete["Num_Assure"] != DBNull.Value)
                            xml.WriteAttributeString("insured_id", Entete["Num_Assure"].ToString()); 
                       
                        if (Entete["NAccident"] != DBNull.Value && Entete["NAccident"].ToString() != "")
                        {
                            xml.WriteAttributeString("case_id", Entete["NAccident"].ToString());
                        }

                        xml.WriteAttributeString("case_date", DateAccidentString);
                        xml.WriteEndElement();   //Fin kvg

                        xml.WriteStartElement("invoice:treatment");
                        xml.WriteAttributeString("date_begin", DAP);
                        xml.WriteAttributeString("date_end", DAP);                                

                        xml.WriteAttributeString("canton", "GE");
                        xml.WriteAttributeString("reason", "accident");
                        xml.WriteEndElement();    //Fin treatment                              
                    }
                }
            }
            else if (MotifTraitement == "3")    //kvg
            {
                xml.WriteStartElement("invoice:kvg");

                if (Entete["Num_Assure"].ToString() != "" && Entete["Num_Assure"] != DBNull.Value)
                    xml.WriteAttributeString("insured_id", Entete["Num_Assure"].ToString()); 

                xml.WriteEndElement();       //fin invoice:kvg      
                xml.WriteStartElement("invoice:treatment");
                xml.WriteAttributeString("case_date", DAP);               
                
                xml.WriteAttributeString("canton", "GE");
                xml.WriteAttributeString("reason", "prevention");
                xml.WriteEndElement();    //Fin treatment                                                                                           
            }

            Entete.Close();
        }

        private string RemplaceCaractere(string p_strChaine)
        {
            string z_strChaine = p_strChaine;

            z_strChaine = z_strChaine.ToLower();
            z_strChaine = z_strChaine.Replace("é", "e");
            z_strChaine = z_strChaine.Replace("è", "e");
            z_strChaine = z_strChaine.Replace("ë", "e");
            z_strChaine = z_strChaine.Replace("ê", "e");

            z_strChaine = z_strChaine.Replace("ô", "o");
            z_strChaine = z_strChaine.Replace("ö", "o");

            z_strChaine = z_strChaine.Replace("â", "a");
            z_strChaine = z_strChaine.Replace("ä", "a");
            z_strChaine = z_strChaine.Replace("à", "a");

            z_strChaine = z_strChaine.Replace("ï", "i");
            z_strChaine = z_strChaine.Replace("î", "i");

            z_strChaine = z_strChaine.Replace("ü", "u");
            z_strChaine = z_strChaine.Replace("ù", "u");

            z_strChaine = z_strChaine.Replace("ç", "c");           
            
            if (z_strChaine != "")
                z_strChaine = z_strChaine.Substring(0, 1).ToUpper() + z_strChaine.Substring(1);

            return z_strChaine;
        }


        //Les Services (Lignes de la facture)
        public void EditeService(XmlTextWriter xml)
        {
            // Déclaration des variables         
            String ean_provider;
            String ean_responsible;

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
            String code;                        //Code Tarmed
            string ref_code;                    //Code de référence
            String Libelle;

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
                /*string sqlstr0 = @"SELECT CASE WHEN CHARINDEX( 'Médecin praticien', tm.Commentaire) <> 0 
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
                                               ELSE 'LAA-AM-AI' END AS TypeTarmed                 
                                    FROM facture f, factureconsultation fc, tableconsultations tc, tableactes ta, tablemedecin tm
                                    WHERE f.NFacture = fc.NFacture
                                    AND fc.NConsultation = tc.NConsultation
                                    AND tc.CodeAppel = ta.Num
                                    AND tm.CodeIntervenant = ta.CodeIntervenant
                                    AND f.NFacture =" + IdFacture;*/

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
                                               ELSE 'LAA-AM-AI' END AS TypeTarmed                 
                                    FROM facture f, factureconsultation fc, tableconsultations tc, tableactes ta, tablemedecin tm
                                    WHERE f.NFacture = fc.NFacture
                                    AND fc.NConsultation = tc.NConsultation
                                    AND tc.CodeAppel = ta.Num
                                    AND tm.CodeIntervenant = ta.CodeIntervenant
                                    AND f.NFacture =" + IdFacture;

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
                if (DateTime.Parse(DAP) < DateTime.Parse("01.01.2018"))
                {
                    sqlstr0 = "SELECT * FROM facture_prest fp ";
                    sqlstr0 += "        left join (SELECT * FROM Tarmed  ";
                    sqlstr0 += "                    WHERE DateDebut <= '" + @DAP + "'";
                    sqlstr0 += "                          AND isNull(DateFin,'01.01.2030') >= '" + @DAP + "'";
                    sqlstr0 += "                          AND TarmedVersion = 'LAA-AM-AI' ) AS t on fp.indice = t.Nprestation ";
                    sqlstr0 += "        left join fac_tarif ft on ft.id = fp.TypeTarif ";
                    sqlstr0 += "        left join fac_tablemateriel fm on fm.Nt_materiel = fp.Indice ";
                    sqlstr0 += " WHERE fp.NFacture =  " + IdFacture;
                    sqlstr0 += " ORDER BY Ordre";
                }
                else
                {
                    if (TypeTarmed == "LAA-AM-AI")
                    {
                        sqlstr0 = "SELECT * FROM facture_prest fp ";
                        sqlstr0 += "        left join (SELECT * FROM Tarmed  ";
                        sqlstr0 += "                    WHERE DateDebut <= '" + @DAP + "'";
                        sqlstr0 += "                          AND isNull(DateFin,'31.12.2017') = '31.12.2017'";
                        sqlstr0 += "                          AND TarmedVersion = 'LAA-AM-AI' ) AS t on fp.indice = t.Nprestation ";
                        sqlstr0 += "        left join fac_tarif ft on ft.id = fp.TypeTarif ";
                        sqlstr0 += "        left join fac_tablemateriel fm on fm.Nt_materiel = fp.Indice ";
                        sqlstr0 += " WHERE fp.NFacture =  " + IdFacture;
                        sqlstr0 += " ORDER BY Ordre";
                    }
                    else
                    {
                        sqlstr0 = "SELECT * FROM facture_prest fp ";
                        sqlstr0 += "        left join (SELECT * FROM Tarmed  ";
                        sqlstr0 += "                    WHERE DateDebut <= '" + @DAP + "'";
                        sqlstr0 += "                          AND isNull(DateFin,'01.01.2030') >= '" + @DAP + "'";
                        sqlstr0 += "                          AND TarmedVersion = 'LAMAL' ) AS t on fp.indice = t.Nprestation ";
                        sqlstr0 += "        left join fac_tarif ft on ft.id = fp.TypeTarif ";
                        sqlstr0 += "        left join fac_tablemateriel fm on fm.Nt_materiel = fp.Indice ";
                        sqlstr0 += " WHERE fp.NFacture =  " + IdFacture;
                        sqlstr0 += " ORDER BY Ordre";
                    }
                }
                                
                Query0.CommandText = sqlstr0;
                
                DSResult.Tables.Add("LignesPrestations");      //on déclare une table pour cet ensemble de donnée
                DSResult.Tables["LignesPrestations"].Load(Query0.ExecuteReader());       //on execute

                //Puis on recherche le médecin pour connaitre s'il est "medecins praticien" et appliquer un coeff en fonction sur le PM
                /*sqlstr0 = @"SELECT CASE WHEN CHARINDEX( 'Médecin praticien', tm.Commentaire) <> 0 
                                        THEN CASE WHEN ta.DAP < '01.01.2018' then '1' else '0.93' END
                                        ELSE '1' END AS Coefficient    
                                FROM facture f, factureconsultation fc, tableconsultations tc, tableactes ta, tablemedecin tm
                                WHERE f.NFacture = fc.NFacture
                                AND fc.NConsultation = tc.NConsultation
                                AND tc.CodeAppel = ta.Num
                                AND tm.CodeIntervenant = ta.CodeIntervenant
                                AND f.NFacture =" + IdFacture;

                Query0.CommandText = sqlstr0;
                
                DSResult.Tables.Add("Coeff");      //on déclare une table pour cet ensemble de donnée
                DSResult.Tables["Coeff"].Load(Query0.ExecuteReader());       //on execute*/                

                //On affiche le premier enregistrement
                if (DSResult.Tables["LignesPrestations"].Rows.Count > 0)
                {
                    int i = 1;
                    //int a = 1;
                    String Type;

                    xml.WriteStartElement("invoice:services");

                    foreach (DataRow row in DSResult.Tables["LignesPrestations"].Rows)
                    {
                        ean_provider = EanMedecin;
                        ean_responsible = "7601002083034";

                        //Pour distinguer les types de prestation ( tarmed 1 ou pharmacie 2)
                        Type = row["TypePrest"].ToString();

                        if (Type == "1")
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

                                    unit_factor_tt = Math.Round(Convert.ToDouble(row1["PrixPoint"].ToString()), 2);
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

                                    //Ajout d'un service_ex      (ligne de prestation)                              
                                    xml.WriteStartElement("invoice:service_ex");  //Pour XML 4.5
                                    
                                    xml.WriteAttributeString("session", Seance);
                                    xml.WriteAttributeString("tariff_type", "001");
                                    xml.WriteAttributeString("provider_id", ean_provider);
                                    xml.WriteAttributeString("responsible_id", ean_responsible);

                                    //En fonction de l'assurance (Swica)
                                    if (AssuranceEAN == "7601003002041")    
                                        xml.WriteAttributeString("unit_mt", unit_mt.ToString());  //Swica : Modification faite suite à leur email
                                    else                                    
                                        xml.WriteAttributeString("unit_mt", unit_amount_mt.ToString());                                    
                                                                       
                                    xml.WriteAttributeString("unit_factor_mt", unit_factor_mt.ToString());
                                    xml.WriteAttributeString("scale_factor_mt", scale_factor_mt_Maj.ToString());    
                                    xml.WriteAttributeString("amount_mt", amount.ToString());
                                    xml.WriteAttributeString("unit_tt", unit_tt.ToString());
                                    xml.WriteAttributeString("unit_factor_tt", unit_factor_tt.ToString());
                                    xml.WriteAttributeString("scale_factor_tt", scale_factor_tt.ToString());
                                    //xml.WriteAttributeString("amount_tt", unit_amount_tt.ToString());
                                    xml.WriteAttributeString("amount", amount.ToString());
                                    xml.WriteAttributeString("record_id", i.ToString());

                                    xml.WriteAttributeString("date_begin", DAP);

                                    xml.WriteAttributeString("code", code);
                                    xml.WriteAttributeString("ref_code", row1["Indice"].ToString());

                                    //En fonction de l'assurance (Swica)
                                    if (AssuranceEAN == "7601003002041")                                                                           
                                        xml.WriteAttributeString("quantity", row1["Qte"].ToString());      //Swica Qté de la position à majorer...Pas la Qté de la position majoration!!!                                    
                                    else                                                                         
                                        xml.WriteAttributeString("quantity", row["Qte"].ToString());       //Pour les autres: Qté de majoration (1)                                    

                                    xml.WriteAttributeString("name", Libelle);

                                    //***Ajout du 13.11.2018
                                    xml.WriteAttributeString("medical_role", "employee");
                                    xml.WriteAttributeString("billing_role", "both");
                                    xml.WriteAttributeString("body_location", "none");
                                    xml.WriteAttributeString("obligation", "1");                                                                       
                                                                                                         
                                    xml.WriteEndElement();      //Fin service_ex
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

                                unit_factor_tt = Math.Round(Convert.ToDouble(row["PrixPoint"].ToString()), 2);
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

                                //Ajout d'un service_ex     (ligne de prestation)
                                xml.WriteStartElement("invoice:service_ex");     //XML 4.5

                                xml.WriteAttributeString("session", Seance);
                                xml.WriteAttributeString("tariff_type", "001");
                                xml.WriteAttributeString("provider_id", ean_provider);
                                xml.WriteAttributeString("responsible_id", ean_responsible);
                                xml.WriteAttributeString("unit_mt", unit_mt.ToString());
                                xml.WriteAttributeString("unit_factor_mt", unit_factor_mt.ToString());
                                xml.WriteAttributeString("scale_factor_mt", scale_factor_mt.ToString());    //Le 05.10.2017
                                xml.WriteAttributeString("amount_mt", unit_amount_mt.ToString());
                                xml.WriteAttributeString("unit_tt", unit_tt.ToString());
                                xml.WriteAttributeString("unit_factor_tt", unit_factor_tt.ToString());
                                xml.WriteAttributeString("scale_factor_tt", scale_factor_tt.ToString());
                                xml.WriteAttributeString("amount_tt", unit_amount_tt.ToString());
                                xml.WriteAttributeString("amount", amount.ToString());
                                xml.WriteAttributeString("record_id", i.ToString());

                                //Si ce sont des postions du sous chapitre 00.06, on met la date J+1 de la consult, PAS celle de la consult
                                //Même chose pour les ordonances hors consult 00.0136 et 00.0146 et 00.0141
                                //SAUF pour Swica, kpt, AEKK, EGK
                                if (code == "00.2285" || code == "00.2295" || code == "00.0136" || code == "00.0146" || code == "00.0141")                                     
                                {
                                    if (AssuranceEAN != "7601003002041" && AssuranceEAN != "7601003000382" && AssuranceEAN != "7601003000894" && AssuranceEAN != "7601003000924" )     //!= de Swica ou de KPT ou OEKK, EGK
                                    {
                                        DateTime DateDAPJPlus1 = DateTime.Parse(String.Format("{0:d/M/yyyy}", DateTime.Parse(DAP))).AddDays(+1);
                                        String DateDAPjplus1 = String.Format("{0:s}", DateDAPJPlus1);
                                        xml.WriteAttributeString("date_begin", DateDAPjplus1);
                                    }
                                    else xml.WriteAttributeString("date_begin", DAP);
                                }
                                else xml.WriteAttributeString("date_begin", DAP);
                            

                                xml.WriteAttributeString("code", code);

                                ref_code = recupRefcode(code, DSResult.Tables["LignesPrestations"]);
                                
                                if (ref_code != "")
                                    xml.WriteAttributeString("ref_code", ref_code);

                                xml.WriteAttributeString("quantity", row["Qte"].ToString());
                                xml.WriteAttributeString("name", Libelle);
                            
                                xml.WriteAttributeString("medical_role", "employee");
                                xml.WriteAttributeString("billing_role", "both");
                                xml.WriteAttributeString("body_location", "none");
                                xml.WriteAttributeString("obligation", "1");                                                          
                             
                                xml.WriteEndElement();      //Fin service_ex

                                i++;
                            }
                        }
                        else if (Type == "2")
                        {
                            if (row["MatPharmacode"].ToString() == "")
                               // code = row["Indice"].ToString();
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

                            //Ajout d'un invoice:service en fonction du type tarif (drug: XX400XX  402, 406, lab: 317)
                            xml.WriteStartElement("invoice:service");
                            xml.WriteAttributeString("record_id", i.ToString());
                            xml.WriteAttributeString("tariff_type", row["MatCommentaire"].ToString());      //Domi 17.12.2014  On met la position dans les commentaires de la table Materiel
                            xml.WriteAttributeString("code", code);                                                       
                            xml.WriteAttributeString("name", Libelle);
                            xml.WriteAttributeString("session", Seance);
                            xml.WriteAttributeString("quantity", row["Qte"].ToString());
                            xml.WriteAttributeString("date_begin", DAP);
                            xml.WriteAttributeString("provider_id", ean_provider);
                            xml.WriteAttributeString("responsible_id", ean_responsible);
                            
                            xml.WriteAttributeString("unit", row["MatPrix"].ToString());
                            xml.WriteAttributeString("amount", unit_amount_mt.ToString());
                                                                                                                                                                       
                            xml.WriteEndElement();      //Fin invoice:service

                            i++;
                        }
                    }
                    xml.WriteEndElement();      //Fin invoice:services          
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

        }


        //Permet de joindre des documents à la facture
        public void JointDoc(XmlTextWriter xml)
        {
            //Récup du chemin des docs            
            string PathDoc = SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.Path_DocFacture;
            string CheminDoc = "";

            // Recupere les données de la requete           
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);      //Chaine de connection récupérée dans le app.config

            try
            {
                //on ouvre la connexion
                dbConnection.Open();

                SqlCommand Query0 = dbConnection.CreateCommand();
                Query0.Connection = dbConnection;       //On passe les parametres query et connection

                //on définit la requette             
                //string sqlstr0 = @"select UrlJointDoc from facturejointdoc where NFacture = " + IdFacture;
                string sqlstr0 = @"SELECT UrlJointDoc FROM facturejointdoc d INNER JOIN facture f ON f.NFacture = d.NFacture
                                   AND f.TypeDocJoint <> 0 AND d.NFacture = " + IdFacture;

                Query0.CommandText = sqlstr0;

                DataSet DSResult = new DataSet();
                DSResult.Tables.Add("LignesDoc");      //on déclare une table pour cet ensemble de donnée
                DSResult.Tables["LignesDoc"].Load(Query0.ExecuteReader());       //on execute

                //Si on a au moins 1 document
                if (DSResult.Tables["LignesDoc"].Rows.Count > 0)
                {
                    xml.WriteStartElement("invoice:documents");
                    xml.WriteAttributeString("number", DSResult.Tables["LignesDoc"].Rows.Count.ToString());

                    foreach (DataRow row in DSResult.Tables["LignesDoc"].Rows)
                    {
                        CheminDoc = PathDoc + row[0].ToString();
                        Byte[] bytes = File.ReadAllBytes(CheminDoc);
                        String file = Convert.ToBase64String(bytes);
                        string extension = Path.GetExtension(CheminDoc).Replace(".", "");
                        string NomFichier = Path.GetFileNameWithoutExtension(CheminDoc);

                        xml.WriteStartElement("invoice:document");
                        xml.WriteAttributeString("title", "session"); 
                        xml.WriteAttributeString("filename", NomFichier);
                        xml.WriteAttributeString("mimeType", "application/pdf");                                            

                        xml.WriteStartElement("invoice:base64");
                        xml.WriteString(file);      //Données du fichier
                        xml.WriteEndElement();  //Fin :base64
                        xml.WriteEndElement();   //Fin document

                        // xml.WriteStartElement("invoice:url"); 
                        //xml.WriteEndElement();//("/invoice:url");                        
                    }

                    xml.WriteEndElement();    //Fin documents
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'ajout d'un doc pour la facture " + IdFacture + "  " + ex.Message.ToString());
            }
            finally
            {
                dbConnection.Close();       //On ferme les connctions                                
                dbConnection = null;        //on remet à blanc la chaine de connection
            }

        }


        // Edition globale de la facture ( initialisation des objets )       
        public void EditeFacture(XmlTextWriter xml)    
        {
            //declaration d'un document XML 
            xml.WriteStartDocument(false);
            //Ajout des paramètres de invoice:request....
            xml.WriteStartElement("invoice:request");
            xml.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
            xml.WriteAttributeString("xmlns:xenc", "http://www.w3.org/2001/04/xmlenc#");
            xml.WriteAttributeString("xmlns:ds", "http://www.w3.org/2000/09/xmldsig#");           
            xml.WriteAttributeString("xsi:schemaLocation", "http://www.forum-datenaustausch.ch/invoice generalInvoiceRequest_450.xsd");   //XML 4.5
            xml.WriteAttributeString("xmlns", "http://www.forum-datenaustausch.ch/invoice");
            xml.WriteAttributeString("xmlns:invoice", "http://www.forum-datenaustausch.ch/invoice");
            xml.WriteAttributeString("language", "fr");
                        
            xml.WriteAttributeString("modus", "production");
            //xml.WriteAttributeString("modus", "test");
            xml.WriteAttributeString("validation_status", "0");
            //1 endElement            

            EditeEnTete(xml);
            EditeService(xml);
            
            if (DocJoint == "Oui")                        
                JointDoc(xml);

            xml.WriteEndElement();      //Fin invoice:body                                    
            xml.WriteEndElement();      //Fin invoice:payload
            xml.WriteEndElement();      //Fin invoice:request
        }


        //******************   calcul du temps epoch ou UnixTime (temps écoulé en seconde depuis le 01.01.1970)
        public static long ToUnixTime(DateTime date)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt64((date - epoch).TotalSeconds);
        }

        //Edite les service d'une facture et retourne le montant total
        private string PrepareBalance(XmlTextWriter xml)
        {                       
            // Déclaration des variables         
            double unit_mt = 0;                 //Point tarifaire de la prestation médicale
            double unit_factor_mt = 0;          //Valeur du point de la prestation médicale
            double unit_tt = 0;                 //Point tarifaire de la prestation technique
            double unit_factor_tt = 0;          //Valeur du point de la prestation technique
            double amount_due = 0;
            double amount_tarmed = 0;
            double amount_tarmed_mt = 0;          
            double scale_factor_mt = 0;         //Facteur scalaire interne de la prestation médicale   Domi 06.10.2017
            double scale_factor_tt = 0;         //Facteur scalaire interne de la prestation technique
            double amount_tarmed_tt = 0;
            double unit_tarmed_mt = 0;
            double unit_tarmed_tt = 0;
            double amount_drug = 0;
            double quantite = 0;

            double unit_amount_mt = 0;
            double SommeAmount = 0;

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
                /*string sqlstr0 = @"SELECT CASE WHEN CHARINDEX( 'Médecin praticien', tm.Commentaire) <> 0 
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
                                               ELSE 'LAA-AM-AI' END AS TypeTarmed                 
                                    FROM facture f, factureconsultation fc, tableconsultations tc, tableactes ta, tablemedecin tm
                                    WHERE f.NFacture = fc.NFacture
                                    AND fc.NConsultation = tc.NConsultation
                                    AND tc.CodeAppel = ta.Num
                                    AND tm.CodeIntervenant = ta.CodeIntervenant
                                    AND f.NFacture =" + IdFacture;*/

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
                                               ELSE 'LAA-AM-AI' END AS TypeTarmed                 
                                    FROM facture f, factureconsultation fc, tableconsultations tc, tableactes ta, tablemedecin tm
                                    WHERE f.NFacture = fc.NFacture
                                    AND fc.NConsultation = tc.NConsultation
                                    AND tc.CodeAppel = ta.Num
                                    AND tm.CodeIntervenant = ta.CodeIntervenant
                                    AND f.NFacture =" + IdFacture;

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

                scale_factor_tt = 1;   //Pour le momment

              //******************REVOIR ICI LA REQUETE POUR UTILISER TARMED**********************************              
                //on définit la requete                                          
                if (DateTime.Parse(DAP) < DateTime.Parse("01.01.2018"))
                {
                    sqlstr0 = "SELECT * FROM facture_prest fp ";
                    sqlstr0 += "        left join (SELECT * FROM Tarmed  ";
                    sqlstr0 += "                    WHERE DateDebut <= '" + @DAP + "'";
                    sqlstr0 += "                          AND isNull(DateFin,'01.01.2030') >= '" + @DAP + "'";
                    sqlstr0 += "                          AND TarmedVersion = 'LAA-AM-AI' ) AS t on fp.indice = t.Nprestation ";
                    sqlstr0 += "        left join fac_tarif ft on ft.id = fp.TypeTarif ";
                    sqlstr0 += "        left join fac_tablemateriel fm on fm.Nt_materiel = fp.Indice ";
                    sqlstr0 += " WHERE fp.NFacture =  " + IdFacture;
                }
                else
                {
                    if (TypeTarmed == "LAA-AM-AI")
                    {
                        sqlstr0 = "SELECT * FROM facture_prest fp ";
                        sqlstr0 += "        left join (SELECT * FROM Tarmed  ";
                        sqlstr0 += "                    WHERE DateDebut <= '" + @DAP + "'";
                        sqlstr0 += "                          AND isNull(DateFin,'31.12.2017') = '31.12.2017'";
                        sqlstr0 += "                          AND TarmedVersion = 'LAA-AM-AI' ) AS t on fp.indice = t.Nprestation ";
                        sqlstr0 += "        left join fac_tarif ft on ft.id = fp.TypeTarif ";
                        sqlstr0 += "        left join fac_tablemateriel fm on fm.Nt_materiel = fp.Indice ";
                        sqlstr0 += " WHERE fp.NFacture =  " + IdFacture;
                    }
                    else
                    {
                        sqlstr0 = "SELECT * FROM facture_prest fp ";
                        sqlstr0 += "        left join (SELECT * FROM Tarmed  ";
                        sqlstr0 += "                    WHERE DateDebut <= '" + @DAP + "'";
                        sqlstr0 += "                          AND isNull(DateFin,'01.01.2030') >= '" + @DAP + "'";
                        sqlstr0 += "                          AND TarmedVersion = 'LAMAL' ) AS t on fp.indice = t.Nprestation ";
                        sqlstr0 += "        left join fac_tarif ft on ft.id = fp.TypeTarif ";
                        sqlstr0 += "        left join fac_tablemateriel fm on fm.Nt_materiel = fp.Indice ";
                        sqlstr0 += " WHERE fp.NFacture =  " + IdFacture;
                    }
                }


                Query0.CommandText = sqlstr0;
                
                DSResult.Tables.Add("BalanceFacture");      //on déclare une table pour cet ensemble de donnée
                DSResult.Tables["BalanceFacture"].Load(Query0.ExecuteReader());       //on execute
                               
                //On affiche le premier enregistrement
                if (DSResult.Tables["BalanceFacture"].Rows.Count > 0)
                {                    
                    String Type;
                    foreach (DataRow row in DSResult.Tables["BalanceFacture"].Rows)
                    {
                        //Pour distinguer les types de prestation ( tarmed 1 ou pharmacie 2)                       
                        Type = row["TypePrest"].ToString();

                        if (Type == "1")
                        {
                             //Si c'est une majoration en %
                            if (row["Indice"].ToString() == "00.2530" || row["Indice"].ToString() == "00.2550" || row["Indice"].ToString() == "00.2570" || row["Indice"].ToString() == "00.2590")
                            {
                                //Pour chaque enregistrements majorable, on écrit une ligne!
                                foreach (DataRow row1 in DSResult.Tables["BalanceFacture"].Select("PrestHorsMajor = 0"))
                                {
                                    //on prend les valeurs de la position à majorer (unit_mt * scale_factor_mt = unit_mt Major), 
                                    //mais le prixpoint de la position majoration en tant que scale_factor_mt

                                    quantite = Math.Round(Convert.ToDouble(row1["Qte"].ToString()), 2);
                                    unit_mt = Math.Round(Convert.ToDouble(row1["Points"].ToString()), 2);
                                    unit_factor_mt = Math.Round(Convert.ToDouble(row1["PrixPoint"].ToString()), 2);

                                    unit_amount_mt = unit_mt * quantite * scale_factor_mt;    //!!!
                                    unit_amount_mt = Math.Round(unit_amount_mt, 2);

                                    double scale_factor_mt_Maj = Math.Round(Convert.ToDouble(row["PrestPointM"].ToString()), 2);
                                 
                                    //Somme des operations majorées
                                    SommeAmount += Math.Round((unit_amount_mt * unit_factor_mt * scale_factor_mt_Maj), 2);                                   
                                }                               
                            }
                            else
                            {
                                //On est pas dans une majoration
                                quantite = Math.Round(Convert.ToDouble(row["Qte"].ToString()), 2);

                                unit_mt = Math.Round(Convert.ToDouble(row["Points"].ToString()), 2);
                                unit_factor_mt = Math.Round(Convert.ToDouble(row["PrixPoint"].ToString()), 2);
                                //unit_tarmed_mt = Math.Round(unit_tarmed_mt + unit_mt * quantite, 2);
                                //amount_tarmed_mt = Math.Round(amount_tarmed_mt + (unit_mt * quantite * unit_factor_mt * scale_factor_mt), 2);
                                unit_tarmed_mt = unit_tarmed_mt + unit_mt * quantite;                               
                                amount_tarmed_mt = amount_tarmed_mt + (unit_mt * quantite * unit_factor_mt * scale_factor_mt);

                                if (row["PrestPointT"] != DBNull.Value)
                                    unit_tt = Math.Round(Convert.ToDouble(row["PrestPointT"].ToString()), 2);
                                else
                                    unit_tt = 0.00;

                                unit_factor_tt = Math.Round(Convert.ToDouble(row["PrixPoint"].ToString()), 2);
                                //unit_tarmed_tt = Math.Round(unit_tarmed_tt + unit_tt * quantite, 2);
                                //amount_tarmed_tt = Math.Round(amount_tarmed_tt + (unit_tt * quantite * unit_factor_tt * scale_factor_tt), 2);
                                unit_tarmed_tt = unit_tarmed_tt + unit_tt * quantite;                               
                                amount_tarmed_tt = amount_tarmed_tt + (unit_tt * quantite * unit_factor_tt * scale_factor_tt);                             
                            }
                        }
                        else if (Type == "2")
                        {
                            quantite = Math.Round(Convert.ToDouble(row["Qte"].ToString()), 2);

                            unit_mt = Math.Round(Convert.ToDouble(row["MatPrix"].ToString()), 2);
                            //unit_factor_mt = 1;
                            //amount_drug = amount_drug + unit_mt * quantite * unit_factor_mt;
                            amount_drug = Math.Round(amount_drug + unit_mt * quantite,2);     //A verifier avec coeff médecin praticien                         
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
                dbConnection.Close();       //On ferme les connections                                               
            }

            //amount_tarmed = Math.Round(amount_tarmed_mt + amount_tarmed_tt + SommeAmount,2);
            amount_tarmed = amount_tarmed_mt + amount_tarmed_tt + SommeAmount;
            amount_due = Math.Round((amount_tarmed + amount_drug)*20,MidpointRounding.AwayFromZero)/20;                 //   Arrondi suisse à 0.05

            return amount_due.ToString();
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
        public String CodingLine(string reference_number, string MontantFacture)
        {
            reference_number = reference_number.ToString().Replace(" ", "");

            double TotalFactureF = double.Parse(MontantFacture);
            TotalFactureF = TotalFactureF * 100;
            string str1 = TotalFactureF.ToString("000000");
           
            str1 = "010000" + str1;
            str1 = str1 + Modulo10(str1);
            str1 = str1 + ">" + reference_number + "+ 010306272>";    //SOS
            //str1 = str1 + "&gt;" + Complete16caracteres(reference_number) + "+ 010306272&gt;";

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

        //On récupère le ref_code de la position, s'il y en a une (Position maitre)
        private string recupRefcode(string code, DataTable dtLignesFactures)
        {
            string Code_Maitre = "";
            DataTable dtResult = new DataTable();

            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);      //Chaine de connection récupérée dans le app.config

            try
            {
                //on ouvre la connexion
                dbConnection.Open();

                SqlCommand Query0 = dbConnection.CreateCommand();
                Query0.Connection = dbConnection;       //On passe les parametres query et connection

                //on définit la requette                           
                string sqlstr0 = @"SELECT PositionMaitre FROM Tarmed_Maitre WHERE Position = '" + code + "'";
                                  
                Query0.CommandText = sqlstr0;

                          
                dtResult.Load(Query0.ExecuteReader());       //on execute
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la recupération de la position maitre position: " + code + "pour la facture " + IdFacture + "  " + ex.Message.ToString());
            }
            finally
            {
                dbConnection.Close();       //On ferme les connctions                                
               // dbConnection = null;        //on remet à blanc la chaine de connection
            }


            if(code == "00.0095")
            {
                string test = "Ok";
            }

            //Si on a au moins 1 resultat
            if (dtResult.Rows.Count > 0)
            {
                //et si il y a plus d'un resultat
                if (dtResult.Rows.Count > 1)
                {
                    //On recherche dans les lignes de la facture la position maitre
                    for (int i = 0; i < dtResult.Rows.Count; i++)
                    {
                        for (int j = 0; j < dtLignesFactures.Rows.Count; j++)
                        {
                            if (dtResult.Rows[i]["PositionMaitre"].ToString() == dtLignesFactures.Rows[j]["Indice"].ToString())
                                Code_Maitre = dtLignesFactures.Rows[j]["Indice"].ToString();
                        }
                    }
                }
                else
                {
                    //Code_Maitre = dtLignesFactures.Rows[0]["Indice"].ToString();
                    Code_Maitre = dtResult.Rows[0]["PositionMaitre"].ToString();
                }                       
            }

            return Code_Maitre;
        }

        //Gestion des rappels
        private void AjoutFraisRappel(int NFacture, int NbRappel)
        {
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);      //Chaine de connection récupérée dans le app.config            

            try
            {
                //on ouvre la connexion
                dbConnection.Open();

                SqlCommand cmd = dbConnection.CreateCommand();
                cmd.Connection = dbConnection;       //On passe les parametres query et connection

                //Premier rappel
                if (NbRappel == 1)
                {
                    MontantFrais = 20;

                    //Création d'un nvl enregistrement dans la table FraisRappel
                    string sqlstr0 = "INSERT INTO FraisRappel (NFacture, NumRappel, Montant, Regle, Annule, DateEtat)";
                    sqlstr0 += " VALUES (@NFacture, @NumRappel, 20, 0, 0, @DateEtat)";

                    cmd.CommandText = sqlstr0;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("NFacture", NFacture);
                    cmd.Parameters.AddWithValue("NumRappel", NbRappel);
                    cmd.Parameters.AddWithValue("DateEtat", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
                else if (NbRappel == 2)
                {
                    MontantFrais = 50;
                    //2eme rappel donc on modifie l'enregistrement du rappel (s'il n'existe pas, on le créée)
                    //recherche de l'enregistrement
                    string sqlstr0 = "SELECT * FROM FraisRappel ";
                    sqlstr0 += " WHERE NFacture = @NFacture";

                    cmd.CommandText = sqlstr0;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("NFacture", NFacture);

                    DataTable Result = new DataTable();
                    Result.Load(cmd.ExecuteReader());

                    if(Result.Rows.Count > 0)   //Si on l'a trouvé on le modifie
                    {
                        string sqlstr1 = "UPDATE FraisRappel SET NumRappel = 2, Montant = 50, DateEtat = @DateEtat";
                        sqlstr0 += " WHERE NFacture = @NFacture";

                        cmd.CommandText = sqlstr1;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("NFacture", NFacture);                        
                        cmd.Parameters.AddWithValue("DateEtat", DateTime.Now);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        //il n'existe pas, on le créé
                        //Création d'un nvl enregistrement dans la table FraisRappel
                        string sqlstr1 = "INSERT INTO FraisRappel (NFacture, NumRappel, Montant, Regle, Annule, DateEtat)";
                        sqlstr0 += " VALUES (@NFacture, 2, 50, 0, 0, @DateEtat)";

                        cmd.CommandText = sqlstr1;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("NFacture", NFacture);                        
                        cmd.Parameters.AddWithValue("DateEtat", DateTime.Now);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);                
            }
            finally
            {
                dbConnection.Close();       //On ferme les connctions                                               
            }

        }


    }
}


//A FAIRE: