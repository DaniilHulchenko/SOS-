using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace ImportSosGeneve
{
    public partial class FInfoAssure : Form
    {
        private string NumCarte = "";        
        private string Nom = "", Prenom = "", DateNaissance = "", AVS = "", Genre = "";
        private string Adresse = "", NomAssurance = "", TypeAssurance = "", NumAssure = "", EAN = "", DateEcheance = "";

        
        public FInfoAssure(string NCarte)
        {
            InitializeComponent();

            NumCarte = NCarte;
            AppelService();
            bFermer.Focus();
        }

        private void bFermer_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AppelService()
        {            
            var url = "https://192.168.0.7:7080/assurpatient/RecupDataAssurance.asmx/RecupedataFromCoverCard?numcard=" + NumCarte;

            //Pour passer le blocage du certificat autosigné
            //ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            /* ServicePointManager.ServerCertificateValidationCallback += delegate
             {
                 return true;
             };*/

            // Before making the web request:
            System.Net.ServicePointManager.ServerCertificateValidationCallback +=
                (sender, certificate, chain, sslPolicyErrors) => true;

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            try
            {
                //appel du webservice
                HttpWebRequest requete = (HttpWebRequest)WebRequest.Create(url);
                requete.Timeout = 5000;
                requete.ContentType = "text/xml";

                //On met des ressources raisonnable pour cette requette
                requete.MaximumAutomaticRedirections = 4;
                requete.MaximumResponseHeadersLength = 4;

                //Récuperation de la réponse
                HttpWebResponse reponse = (HttpWebResponse)requete.GetResponse();

                //On le met dans un stream
                Stream responseStream = reponse.GetResponseStream();

                Console.WriteLine(responseStream);

                //recup de la reponse
                XmlTextReader XMLreader = new XmlTextReader(responseStream);

                rTBoxInfos.Text = "";    //on vide le champs text

                //On le charge dans un doc xml
                XmlDocument doc = new XmlDocument();
                doc.Load(XMLreader);    //On enleve les blancs

                XmlNodeList nodes = doc.DocumentElement.SelectNodes("/DataCard/DatasetCard");

                //Puis on parse
                foreach (XmlNode node in nodes)
                {
                    Nom = node.SelectSingleNode("prenom").InnerText; // nom, because on the server it nom -> prenom
                    string text = node.SelectSingleNode("nom").InnerText.ToLower();
                    string formattedText = char.ToUpper(text[0]) + text.Substring(1);
                    Prenom = formattedText; // on the server prenom -> nom

                    //traitement de la date de naissance
                    DateTime dateNaiss;
                    if (DateTime.TryParse(node.SelectSingleNode("DateNaissance").InnerText, out dateNaiss))
                    {
                        DateNaissance = dateNaiss.ToString("dd.MM.yyyy");
                    }
                    else DateNaissance = "";

                    AVS = node.SelectSingleNode("AVS").InnerText;
                    Genre = node.SelectSingleNode("Genre").InnerText;
                    Adresse = node.SelectSingleNode("Adresse").InnerText;

                    NomAssurance = node.SelectSingleNode("NomAssurance").InnerText;
                    TypeAssurance = node.SelectSingleNode("TypeAssurance").InnerText;
                    NumAssure = node.SelectSingleNode("NuméroAssuré").InnerText;
                    EAN = node.SelectSingleNode("EAN-Numbre").InnerText;

                    //traitement de la date d'échéance
                    DateTime echeance;
                    if (DateTime.TryParse(node.SelectSingleNode("DateEchéance").InnerText, out echeance))
                    {
                        DateEcheance = echeance.ToString("dd.MM.yyyy");
                    }
                    else DateEcheance = "";

                    Console.WriteLine(Nom + " " + Prenom + " " + DateNaissance);

                    //On ecrit le tout dans le textlist
                    string nvlleligne = Environment.NewLine;

                    rTBoxInfos.Text += "Nom : " + Nom + nvlleligne + "Prénom : " + Prenom + nvlleligne + "Date de naissance : " + DateNaissance + nvlleligne
                                    + "N° AVS : " + AVS + nvlleligne + "Genre : " + Genre + nvlleligne
                                    + "Adresse : " + Adresse + nvlleligne + nvlleligne + "Nom de l'assurance : " + NomAssurance + nvlleligne + "Type d'assurance : " + TypeAssurance
                                    + nvlleligne + "N° d'assuré : " + NumAssure + nvlleligne
                                    + "EAN de l'assurance : " + EAN + nvlleligne + "Date d'échéance : " + DateEcheance;
                }

                //Fermeture de la réponse
                reponse.Close();

                //Si on a reçu des infos, on active le bouton exporter
                if (Nom != "")
                    bExport.Enabled = true;
                else    //sinon on signal qu'on a rien trouvé
                {
                    bExport.Enabled = false;
                    rTBoxInfos.Text = "aucune donnée n'a été trouvée";
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur " + ex.Message);
                //Si le serveur ne répond pas, on affiche le message d'erreur
                rTBoxInfos.Text = ex.Message;
            }
        }

        private void bExport_Click(object sender, EventArgs e)
        {
            //Export des données Dans la FIP            
            FIP.NomAssure = Nom;
            FIP.PrenomAssure = Prenom;
            FIP.DateNaissanceAssure = DateNaissance;
            FIP.AVSAssure = AVS;
            FIP.GenreAssure = Genre;
            FIP.NumAssure = NumAssure;     
      
            //Puis on ferme la forme
            Close();
        }

    
     
    }
}
