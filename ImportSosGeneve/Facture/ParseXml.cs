using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ImportSosGeneve
{
   public class ListeBlocs
    {
        public string NtryRef { get; set; }
        public string AcctSvcrRef { get; set; }
        public string DateEcriture { get; set; }
        public string Amt { get; set; }
        public string CdtDbtInd_Ntry { get; set; }
        public string DateValeur { get; set; }
        public string NbEcritures { get; set; }
        public List<Operations> Ope { get; set; }

        public ListeBlocs()
        {
            NtryRef = "";
            AcctSvcrRef = "";
            DateEcriture = "";
            Amt = "";
            CdtDbtInd_Ntry = "";
            DateValeur = "";
            NbEcritures = "";
            Ope = new List<Operations>();
        }
    }


    public class Operations
    {
        public string Amt { get; set; }
        public string CdtDbtInd { get; set; }
        public string RmtInfRef { get; set; }
        public string AcctSvcrRef { get; set; }
        public string Nm { get; set; }
    }

    static public class ParseXml
    {

        // #######  Obsolète ########
        static public List<ListeBlocs> GetListOperations(string filename, string NumAdherentBVR_SOS, string NumAdherentBVR_TA, string NumAdherentBVR_SOS_CAISSEMEDECINS,
                                                         string QRIBAN_TA, string QRIBAN_SOS)
        {
            XmlDocument xmlDoc = new XmlDocument();

            //Ne pas oublier le name space (balise xmlns du fichier)
            // XNamespace ns = "urn:iso:std:iso:20022:tech:xsd:camt.054.001.04";     

            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
            nsmgr.AddNamespace("ns", "urn:iso:std:iso:20022:tech:xsd:camt.054.001.04");
            xmlDoc.Load(filename);

            XmlNode node = xmlDoc.DocumentElement;

            List<ListeBlocs> listeBooks = new List<ListeBlocs>();


            //On selectionne l'enregistrement si le n° de BVR correspond à SOS TA et l'opé = PMNT->RCDT->VCOM (BVR) 
            XmlNode BookList = node.SelectSingleNode("//ns:Ntfctn", nsmgr);

            XmlNodeList ListBook;

            ListBook = BookList.SelectNodes("ns:Ntry[(ns:NtryRef=" + NumAdherentBVR_TA + " or ns:NtryRef=" + NumAdherentBVR_SOS
                + " or ns:NtryRef=" + NumAdherentBVR_SOS_CAISSEMEDECINS + " or ns:NtryRef='" + QRIBAN_TA + "' or ns:NtryRef='" + QRIBAN_SOS
                + "') and (ns:BkTxCd/ns:Domn/ns:Cd='PMNT') and (ns:BkTxCd/ns:Domn/ns:Fmly/ns:Cd='RCDT') and (ns:BkTxCd/ns:Domn/ns:Fmly/ns:SubFmlyCd='VCOM')]", nsmgr);
          
            if (ListBook.Count > 0)
            {
                //C'est pas un fichier Caisse des médecins (qui est moins précis)   
                foreach (XmlNode book in ListBook)
                {
                    ListeBlocs LBlocs = new ListeBlocs();

                    LBlocs.AcctSvcrRef += book.SelectSingleNode("ns:AcctSvcrRef", nsmgr).InnerText;
                    LBlocs.Amt += book.SelectSingleNode("ns:Amt", nsmgr).InnerText;
                    LBlocs.CdtDbtInd_Ntry += book.SelectSingleNode("ns:CdtDbtInd", nsmgr).InnerText;
                    LBlocs.DateEcriture += book.SelectSingleNode("ns:BookgDt/ns:Dt", nsmgr).InnerText;
                    LBlocs.DateValeur += book.SelectSingleNode("ns:ValDt/ns:Dt", nsmgr).InnerText;
                    LBlocs.NbEcritures += book.SelectSingleNode("ns:NtryDtls/ns:Btch/ns:NbOfTxs", nsmgr).InnerText;
                    LBlocs.NtryRef += book.SelectSingleNode("ns:NtryRef", nsmgr).InnerText;

                    string RefPaiement = book.SelectSingleNode("ns:AcctSvcrRef", nsmgr).InnerText;

                    try
                    {
                        XmlNodeList OpeNodeList = book.SelectNodes("//ns:Ntry[(ns:NtryRef=" + book.SelectSingleNode("ns:NtryRef", nsmgr).InnerText + ") and (ns:AcctSvcrRef=" + RefPaiement + ")]/ns:NtryDtls/ns:TxDtls", nsmgr);

                        foreach (XmlNode opera in OpeNodeList)
                        {
                            Operations ope = new Operations();

                            ope.Amt = opera.SelectSingleNode("ns:Amt", nsmgr).InnerText;
                            ope.CdtDbtInd = opera.SelectSingleNode("ns:CdtDbtInd", nsmgr).InnerText;                                     //Sens de l'opération
                            ope.RmtInfRef = opera.SelectSingleNode("ns:RmtInf/ns:Strd/ns:CdtrRefInf/ns:Ref", nsmgr).InnerText;  //Nos Refs                                                                                                                                             
                            ope.AcctSvcrRef = opera.SelectSingleNode("ns:Refs/ns:AcctSvcrRef", nsmgr).InnerText;   //Id de la transaction
                            LBlocs.Ope.Add(ope);
                        }

                        listeBooks.Add(LBlocs);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        System.Windows.Forms.MessageBox.Show(e.Message, "Erreur dans le Fichier");
                    }
                }
            }
            else
            {
                //c'est un Caisse des Médecins donc on ne va pas chercher trop d'infos
                //On commence par récupérer la date du fichier ntfctn/CreDtTm Car on a pas les date de valeurs dans ce fichier
                DateTime DateValidite = DateTime.Parse(BookList.SelectSingleNode("ns:CreDtTm", nsmgr).InnerText);

                //on reformule la requete
                ListBook = BookList.SelectNodes("ns:Ntry[(ns:NtryRef=" + NumAdherentBVR_SOS_CAISSEMEDECINS + " or ns:NtryRef='" + QRIBAN_SOS + "')]", nsmgr);

                foreach (XmlNode book in ListBook)
                {
                    ListeBlocs LBlocs = new ListeBlocs();

                    LBlocs.AcctSvcrRef += "Caisse des Médecins";   //On rajoute un libellé dans la ref, car la caisse n'utilise pas ce tag
                    LBlocs.Amt += book.SelectSingleNode("ns:Amt", nsmgr).InnerText;
                    LBlocs.CdtDbtInd_Ntry += book.SelectSingleNode("ns:CdtDbtInd", nsmgr).InnerText;
                    LBlocs.DateEcriture += DateValidite;
                    LBlocs.DateValeur += DateValidite;
                    LBlocs.NbEcritures += book.SelectSingleNode("ns:NtryDtls/ns:Btch/ns:NbOfTxs", nsmgr).InnerText;
                    LBlocs.NtryRef += book.SelectSingleNode("ns:NtryRef", nsmgr).InnerText;

                    try
                    {
                        XmlNodeList OpeNodeList = book.SelectNodes("//ns:Ntry[(ns:NtryRef=" + book.SelectSingleNode("ns:NtryRef", nsmgr).InnerText + ")]/ns:NtryDtls/ns:TxDtls", nsmgr);

                        foreach (XmlNode opera in OpeNodeList)
                        {
                            Operations ope = new Operations();

                            ope.Amt = opera.SelectSingleNode("ns:Amt", nsmgr).InnerText;
                            ope.CdtDbtInd = opera.SelectSingleNode("ns:CdtDbtInd", nsmgr).InnerText;                                     //Sens de l'opération
                            ope.RmtInfRef = opera.SelectSingleNode("ns:RmtInf/ns:Strd/ns:CdtrRefInf/ns:Ref", nsmgr).InnerText;  //Nos Refs                                                                                                                                                                         
                            ope.AcctSvcrRef = "0";   //Id de la transaction
                            LBlocs.Ope.Add(ope);
                        }

                        listeBooks.Add(LBlocs);

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        System.Windows.Forms.MessageBox.Show(e.Message + "\r\n\n Erreur dans le fichier.", "Erreur dans le Fichier");
                    }
                }

            }

            return listeBooks;
        }


        static public List<ListeBlocs> RecupListOperations(string filename, string NumAdherentBVR_SOS, string NumAdherentBVR_TA, string NumAdherentBVR_SOS_CAISSEMEDECINS,
                                                           string QRIBAN_TA, string QRIBAN_SOS)
        {
            XmlSerializerFactory factory = new XmlSerializerFactory();
            XmlSerializer serializer = factory.CreateSerializer(typeof(ImportSosGeneve.Document));

            StreamReader sr1 = new StreamReader(filename);
            Document Doc1 = (Document)serializer.Deserialize(sr1);

            List<ListeBlocs> listeBooks = new List<ListeBlocs>();

            foreach (ImportSosGeneve.AccountNotification7 Liste1 in Doc1.BkToCstmrDbtCdtNtfctn.Ntfctn)
            {               
                for (int i = 0; i < Liste1.Ntry.Count(); i++)
                {
                    ListeBlocs LBlocs = new ListeBlocs();

                    //En ce sont nos BVR
                    if (Liste1.Ntry[i].NtryRef == NumAdherentBVR_TA || Liste1.Ntry[i].NtryRef == NumAdherentBVR_SOS
                       || Liste1.Ntry[i].NtryRef == NumAdherentBVR_SOS_CAISSEMEDECINS || Liste1.Ntry[i].NtryRef == QRIBAN_TA || Liste1.Ntry[i].NtryRef == QRIBAN_SOS
                       && (Liste1.Ntry[i].BkTxCd.Domn.Cd == "PMNT" || Liste1.Ntry[i].BkTxCd.Domn.Fmly.Cd == "RCDT" || Liste1.Ntry[i].BkTxCd.Domn.Fmly.SubFmlyCd == "VCOM"))
                    {
                        //On test l'existance de la balise (à cause de la CDM)
                        try
                        {
                            LBlocs.AcctSvcrRef += Liste1.Ntry[i].AcctSvcrRef.ToString();
                        }
                        catch
                        {
                            LBlocs.AcctSvcrRef += "Caisse des Médecins";   //On rajoute un libellé dans la ref, car la caisse n'utilise pas ce tag
                        }

                        //LBlocs.AcctSvcrRef += Liste1.Ntry[i].AcctSvcrRef.ToString();
                        LBlocs.Amt += Liste1.Ntry[i].Amt.Value.ToString();
                        LBlocs.CdtDbtInd_Ntry += Liste1.Ntry[i].CdtDbtInd.ToString();
                        LBlocs.DateEcriture += DateTime.Parse(Liste1.Ntry[i].BookgDt.Item.ToString());
                        LBlocs.DateValeur += DateTime.Parse(Liste1.Ntry[i].ValDt.Item.ToString());
                        LBlocs.NbEcritures += Liste1.Ntry[i].NtryDtls[0].Btch.NbOfTxs.ToString();
                        LBlocs.NtryRef += Liste1.Ntry[i].NtryRef.ToString();

                        for (int j = 0; j < Liste1.Ntry[i].NtryDtls[0].TxDtls.Count(); j++)
                        {
                            Operations ope = new Operations();

                            ope.Amt = Liste1.Ntry[i].NtryDtls[0].TxDtls[j].Amt.Value.ToString();
                            ope.CdtDbtInd = Liste1.Ntry[i].NtryDtls[0].TxDtls[j].CdtDbtInd.ToString();       //Sens de l'opération
                            ope.RmtInfRef = Liste1.Ntry[i].NtryDtls[0].TxDtls[j].RmtInf.Strd[0].CdtrRefInf.Ref.ToString();            //Nos Refs                                                                                                                                             

                            //On test l'existance de la balise (tjs à cause de la CDM)
                            try
                            {
                                ope.AcctSvcrRef = Liste1.Ntry[i].NtryDtls[0].TxDtls[j].Refs.AcctSvcrRef.ToString();   //Id de la transaction
                            }
                            catch
                            {
                                ope.AcctSvcrRef = "";   //Id de la transaction
                            }
                           
                            //On test l'existance de la balise
                            try
                            {
                                ope.Nm = Liste1.Ntry[i].NtryDtls[0].TxDtls[j].RltdPties.Dbtr.Nm.ToString();
                            }
                            catch
                            {
                                ope.Nm = "";
                            }                          

                            LBlocs.Ope.Add(ope);
                        }                        
                    }

                    listeBooks.Add(LBlocs);

                }  //Fin for Ntry

            }   //Fin  foreach AccountNotification7

            return listeBooks;
        }
    }
    
}
