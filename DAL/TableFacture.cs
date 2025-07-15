using System;
using System.Collections.Generic;
using System.Text;
using SosMedecins.Connexion;
using System.Data;

namespace SosMedecins.SmartRapport.DAL
{
    public class TableFacture
    {
        public Double SoustraitSolde(long p_lngNFacture, Double p_dblMontant, string p_dteDateOperation)
        {
            Double z_dblNouveauSolde = 0;

            p_dblMontant = Math.Round(p_dblMontant, 2);    //On arrondi à 2 décimales

            string z_strRequete = "SELECT Solde FROM facture";
            z_strRequete += " WHERE NFacture = " + FormatSql.Format_Nombre(p_lngNFacture.ToString());
            
            Boolean z_blnConnect = Variables.ConnexionBase.OpenBDD();
            try
            {
                z_dblNouveauSolde = (Double) Variables.ConnexionBase.ExecuteScalar(z_strRequete);
                z_dblNouveauSolde -= p_dblMontant;

                UpdateSolde(p_lngNFacture, z_dblNouveauSolde, p_dteDateOperation);
            }
            finally
            {
                if (z_blnConnect)
                {
                    Variables.ConnexionBase.CloseBDD();
                }
            }
            return z_dblNouveauSolde;
        }

        public Double UpdateSolde(long p_lngNFacture,Double p_dblNouveauSolde, string p_dteDateOperation)
        {
            // 
            if (p_dblNouveauSolde < 1 && p_dblNouveauSolde > -1 )
            {
                p_dblNouveauSolde = 0;
            }

            string z_strRequete = "UPDATE facture SET";
            z_strRequete += " Solde = " + FormatSql.Format_Nombre(p_dblNouveauSolde.ToString());
            z_strRequete += " WHERE NFacture = " + FormatSql.Format_Nombre(p_lngNFacture.ToString());

            Boolean z_blnConnect = Variables.ConnexionBase.OpenBDD();
            try
            {
                Variables.ConnexionBase.ExecuteSqlSansRetour(z_strRequete);
                // si solde = 0 alors acquittement
                if (p_dblNouveauSolde <= 0) 
                {
                    // Table Facture status -----------------------------------------------------------------------------------------------------
                    z_strRequete = "UPDATE facture_status SET FacDateAcquittee = " + FormatSql.Format_Date(p_dteDateOperation) + " WHERE NFacture = " + p_lngNFacture.ToString();

                    Variables.ConnexionBase.ExecuteSqlSansRetour(z_strRequete);
                    // Rectification des encaissé sur place
                    Fonction z_objFonction = new Fonction();
                    z_objFonction.EncaissementSurPlace(p_lngNFacture);
                }
                else
                {
                    // Table Facture status -----------------------------------------------------------------------------------------------------
                    z_strRequete = "UPDATE facture_status SET FacDateAcquittee = Null WHERE NFacture = " + p_lngNFacture.ToString();

                    Variables.ConnexionBase.ExecuteSqlSansRetour(z_strRequete);
                }
            }
            finally
            {
                if (z_blnConnect)
                {
                    Variables.ConnexionBase.CloseBDD();
                }
            }
            return p_dblNouveauSolde;
        }

        public DataTable SelectFactureByIdPatient(DataTable p_dtb, string p_strIdPatient)
        {
            Boolean z_blnConnect = Variables.ConnexionBase.OpenBDD();
            try
            {
               // string z_strRequete = "SELECT  facture.NFacture, facture.DateCreation, facture.TypeEnvoi, facture.Tarif, facture.TTT, facture.TypeAssurance, facture.TypeSortie, facture.NAccident, ";
               // z_strRequete += "facture.DateAccident, facture.RefPatient, facture.FlagConcerne, facture.Commentaire, facture.TotalFacture, facture.Solde, facture.TypeDestinataire, ";
               // z_strRequete += "facture.CodeDestinataire, facture.AdresseDestinataire, facture.TypeDocJoint, facture.UrlDocJoint, facture.AdresseDestinataire2, tablemedecin.Nom as Medecin, ";
               // z_strRequete += "tableconsultations.NConsultation, tableactes.DSL as DateConsultation";
               // z_strRequete += " FROM tablemedecin INNER JOIN";
              //  z_strRequete += " tableactes INNER JOIN";
              // z_strRequete += " facture INNER JOIN";
              //  z_strRequete += " factureconsultation ON facture.NFacture = factureconsultation.NFacture INNER JOIN";
               //z_strRequete += " tableconsultations ON factureconsultation.NConsultation = tableconsultations.NConsultation ON tableactes.Num = tableconsultations.CodeAppel ON ";
               // z_strRequete += " tablemedecin.CodeIntervenant = tableactes.CodeIntervenant";
              //  z_strRequete += " WHERE tableconsultations.IndicePatient in ( " + p_strIdPatient + ")";
                ////////////////////////////////////
                string z_strRequete = " SELECT facture.NFacture, facture.DateCreation, facture.TypeEnvoi, facture.Tarif, facture.TTT, facture.TypeAssurance, facture.TypeSortie,tableconsultations.IndicePatient, ";
                z_strRequete += " facture.NAccident, facture.DateAccident, facture.RefPatient, facture.FlagConcerne, facture.Commentaire, facture.TotalFacture, facture.Solde,";
                z_strRequete += " facture.TypeDestinataire, facture.CodeDestinataire, facture.AdresseDestinataire, facture.TypeDocJoint, facture.UrlDocJoint, facture.AdresseDestinataire2, facture.FactNum_AVS,";
                z_strRequete += " tablemedecin.Nom as NomMedecinSos ,tableactes.DSL ,tableactes.DAP, tablepersonne.DateNaissance , tablepersonne.Nom as NomPatient, tablepersonne.Prenom as PrenomPatient, tableactes.DAP as DateConsultation , tableconsultations.NConsultation";
                z_strRequete += " FROM tablemedecin ";
                z_strRequete += " INNER JOIN tableactes ON  tablemedecin.CodeIntervenant = tableactes.CodeIntervenant ";
                z_strRequete += " inner join tableconsultations ON tableactes.Num = tableconsultations.CodeAppel";
                z_strRequete += " INNER JOIN tablepatient ON tableconsultations.IndicePatient = tablepatient.IdPatient";
                z_strRequete += " inner join tablepersonne on tablepersonne.IdPersonne = tablepatient.IdPersonne";
                z_strRequete += " inner join factureconsultation ON tableconsultations.NConsultation = factureconsultation.NConsultation";
                z_strRequete += " innER join facture ON facture.NFacture = factureconsultation.NFacture";
                z_strRequete += " WHERE tableconsultations.IndicePatient in ( " + p_strIdPatient + ")";
                
                // mise a jour
                p_dtb = Variables.ConnexionBase.ExecuteSql(p_dtb, z_strRequete);
            }
            finally
            {
                if (z_blnConnect)
                {
                    Variables.ConnexionBase.CloseBDD();
                }
            }
            return p_dtb;
        }
       
    }
}
