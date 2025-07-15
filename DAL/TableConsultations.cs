using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SosMedecins.Connexion;

namespace SosMedecins.SmartRapport.DAL
{
    public class TableConsultations
    {
        public void Update(DataRow p_drw)
        {
            if (p_drw != null)
            {
                // Enregistrement de la personne :
                string z_strRequete = "update tableconsultations set";
                z_strRequete += " Modifie = 1";
                z_strRequete += ", CommentaireLibre = " + FormatSql.Format_String(p_drw["CommentaireLibre"].ToString());
                z_strRequete += ", Deces = " + FormatSql.Format_Nombre(p_drw["Deces"].ToString());
                z_strRequete += ", TraitementLibre = " + FormatSql.Format_String(p_drw["TraitementLibre"].ToString());
                z_strRequete += ", Traitements = " + FormatSql.Format_String(p_drw["Traitements"].ToString());
                z_strRequete += ", Esp = " + FormatSql.Format_Nombre(p_drw["Esp"].ToString());
                // Where
                z_strRequete += " WHERE CodeAppel = " + FormatSql.Format_Nombre(p_drw["Num"].ToString());
                z_strRequete += " AND IndicePatient = " + FormatSql.Format_Nombre(p_drw["IndicePatient"].ToString());
                
                Boolean z_blnConnect = Variables.ConnexionBase.OpenBDD();
                try
                {
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
}
