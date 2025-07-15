using System;
using System.Collections.Generic;
using System.Text;
using SosMedecins.Connexion;
using System.Data;

namespace SosMedecins.SmartRapport.DAL
{
    public class TableFactureEtat
    {

        public DataTable SelectFactureByIdPatient(DataTable p_dtb, string p_strIdPatient)
        {
            Boolean z_blnConnect = Variables.ConnexionBase.OpenBDD();
            try
            {
                string z_strRequete = "SELECT facture_etats.CodeUtilisateur, facture_etats.CommentaireEtat, facture_etats.Compteur, facture_etats.DateEtat, facture_etats.DateOp, facture_etats.DatePaye, facture_etats.Etat, facture_etats.Montant, facture_etats.Moyen, facture_etats.NFacture, facture_etats.Param1, facture_etats.Param2 ";
                z_strRequete += " FROM facture_etats INNER JOIN factureconsultation ON ";
                z_strRequete += " facture_etats.NFacture = factureconsultation.NFacture INNER JOIN tableconsultations";
                z_strRequete += " ON factureconsultation.NConsultation = tableconsultations.NConsultation";
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

        public void Insert(long p_lngNFacture, string p_strType, string p_strDateOperation, string p_strCommentaire, string p_strMoyen, string p_strUtilisateur, string p_strMontant, string p_strDateSalaire)
        {
            string z_strRequete;
            Boolean z_blnConnect = Variables.ConnexionBase.OpenBDD();
            try
            {
                z_strRequete = "INSERT INTO facture_etats ( NFacture, Etat, DateEtat, DateOp, CommentaireEtat, Moyen, Param1, Param2, CodeUtilisateur, Montant, DatePaye ) VALUES (";
                z_strRequete += FormatSql.Format_Nombre(p_lngNFacture.ToString());
                z_strRequete += ", " + FormatSql.Format_Nombre(p_strType);
                z_strRequete += ", " + FormatSql.Format_Date(p_strDateOperation);
                z_strRequete += ", " + FormatSql.Format_Date(DateTime.Now.ToString());
                z_strRequete += ", " + FormatSql.Format_String(p_strCommentaire);
                z_strRequete += ", " + FormatSql.Format_Nombre(p_strMoyen);
                z_strRequete += ", " + FormatSql.Format_String("");
                z_strRequete += ", " + FormatSql.Format_String("");
                z_strRequete += ", " + FormatSql.Format_String(p_strUtilisateur);
                z_strRequete += ", " + FormatSql.Format_Nombre(p_strMontant);
                z_strRequete += ", " + FormatSql.Format_Date(p_strDateSalaire);
                z_strRequete += " )";
                Variables.ConnexionBase.ExecuteSqlSansRetour(z_strRequete);
            }
            finally
            {
                if (z_blnConnect)
                {
                    Variables.ConnexionBase.CloseBDD();
                }
            }
        }
    }
}
