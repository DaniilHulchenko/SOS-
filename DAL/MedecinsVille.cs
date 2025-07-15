using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SosMedecins.Connexion;

namespace SosMedecins.SmartRapport.DAL
{
    public class MedecinsVille
    {
        public bool Update(DataRow p_drw)
        {
            Boolean z_blnRetour = false;
            if (p_drw != null)
            {
                string z_strRequete = "update medecinsville set ";
                z_strRequete += "Nom = " + FormatSql.Format_String(p_drw["Nom"].ToString());
                z_strRequete += ", Prenom = " + FormatSql.Format_String(p_drw["Prenom"].ToString());
                z_strRequete += ", Sexe = " + FormatSql.Format_String(p_drw["Sexe"].ToString());
                z_strRequete += ", Specialite = " + FormatSql.Format_String(p_drw["Specialite"].ToString());
                z_strRequete += ", DateNaissance = " + FormatSql.Format_Date(p_drw["DateNaissance"].ToString());
                z_strRequete += ", DateDeces = " + FormatSql.Format_Date(p_drw["DateDeces"].ToString());
                z_strRequete += ", NumeroRue = " + FormatSql.Format_String(p_drw["NumeroRue"].ToString());
                z_strRequete += ", Rue = " + FormatSql.Format_String(p_drw["Rue"].ToString());
                z_strRequete += ", Np = " + FormatSql.Format_String(p_drw["Np"].ToString());
                z_strRequete += ", Commune = " + FormatSql.Format_String(p_drw["Commune"].ToString());
                z_strRequete += ", Telephone = " + FormatSql.Format_String(p_drw["Telephone"].ToString());
                z_strRequete += ", Natel = " + FormatSql.Format_String(p_drw["Natel"].ToString());
                z_strRequete += ", Fax = " + FormatSql.Format_String(p_drw["Fax"].ToString());
                z_strRequete += ", NConcordat = " + FormatSql.Format_String(p_drw["NConcordat"].ToString());
                z_strRequete += ", EAN = " + FormatSql.Format_String(p_drw["EAN"].ToString());
                z_strRequete += ", email = " + FormatSql.Format_String(p_drw["email"].ToString());
                z_strRequete += ", Destinataire = " + FormatSql.Format_String(p_drw["Destinataire"].ToString());
                z_strRequete += ", ModeEnvoi = " + FormatSql.Format_String(p_drw["ModeEnvoi"].ToString());
                z_strRequete += ", Civilite = " + FormatSql.Format_String(p_drw["Civilite"].ToString());
                
                z_strRequete += ", Active = " + FormatSql.Format_String(p_drw["Active"].ToString());
                
                z_strRequete += " WHERE Num = " + FormatSql.Format_Nombre(p_drw["num"].ToString());

                Boolean z_blnConnect = Variables.ConnexionBase.OpenBDD();
                try
                {
                    Variables.ConnexionBase.ExecuteSqlSansRetour(z_strRequete);
                    z_blnRetour = true;
                }
                finally
                {
                    if (z_blnConnect)
                    {
                        Variables.ConnexionBase.CloseBDD();
                    }
                }
            }
            return z_blnRetour;
        }
    }
}
