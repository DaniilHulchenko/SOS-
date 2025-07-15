using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SosMedecins.Connexion;

namespace SosMedecins.SmartRapport.DAL
{
    public class TablePersonne
    {
        public void Update(DataRow p_drw)
        {
            if (p_drw != null)
            {
                // Enregistrement de la personne :
                string z_strRequete = "update tablepersonne set";
                z_strRequete += " Tel = " + FormatSql.Format_String(p_drw["TelPatient"].ToString());
                z_strRequete += ", Nom = " + FormatSql.Format_String(p_drw["NomPatient"].ToString());
                z_strRequete += ", Prenom = " + FormatSql.Format_String(p_drw["PrenomPatient"].ToString());
                z_strRequete += ", Commune = " + FormatSql.Format_String(p_drw["Commune"].ToString());
                z_strRequete += ", Rue = " + FormatSql.Format_String(p_drw["Rue"].ToString());
                z_strRequete += ", NumeroDansRue = " + FormatSql.Format_String(p_drw["NumeroDansRue"].ToString());
                z_strRequete += ", Batiment = " + FormatSql.Format_String(p_drw["Batiment"].ToString()); 
                z_strRequete += ", CodePostal = " + FormatSql.Format_String(p_drw["CodePostal"].ToString()); 
                z_strRequete += ", Escalier = " + FormatSql.Format_String(p_drw["Escalier"].ToString()); 
                z_strRequete += ", Etage = " + FormatSql.Format_String(p_drw["Etage"].ToString()); 
                z_strRequete += ", Digicode = " + FormatSql.Format_String(p_drw["Digicode"].ToString()); 
                z_strRequete += ", InterNom = " + FormatSql.Format_String(p_drw["InterNom"].ToString()); 
                z_strRequete += ", Porte = " + FormatSql.Format_String(p_drw["Porte"].ToString());
                z_strRequete += ", Longitude = " + FormatSql.Format_String(p_drw["Longitude"].ToString()); 
                z_strRequete += ", Latitude = " + FormatSql.Format_String(p_drw["Latitude"].ToString());
                z_strRequete += ", DateNaissance = " + FormatSql.Format_Date(p_drw["DateNaissance"].ToString());
                z_strRequete += ", Sexe = " + FormatSql.Format_String(p_drw["Sexe"].ToString());
                z_strRequete += ", Age = " + FormatSql.Format_String(p_drw["Age"].ToString()); 
                z_strRequete += ", UniteAge = " + FormatSql.Format_String(p_drw["UniteAge"].ToString()); 
                z_strRequete += ", TexteSup = " + FormatSql.Format_String(p_drw["TexteSup"].ToString());
                z_strRequete += ", ListeNoire = " + FormatSql.Format_String(p_drw["ListeNoire"].ToString()); 
                z_strRequete += ", Adm_Batiment = " + FormatSql.Format_String(p_drw["Adm_Batiment"].ToString()); 
                z_strRequete += ", Adm_CodePostal = " + FormatSql.Format_String(p_drw["Adm_CodePostal"].ToString()); 
                z_strRequete += ", Adm_NumeroDansRue = " + FormatSql.Format_String(p_drw["Adm_NumeroDansRue"].ToString());
                z_strRequete += ", Adm_Rue = " + FormatSql.Format_String(p_drw["Adm_Rue"].ToString());
                z_strRequete += ", Adm_Commune = " + FormatSql.Format_String(p_drw["Adm_Commune"].ToString());
                // Date de deces
                if (p_drw["Deces"].ToString() == "1")
                {
                    z_strRequete += ", DateDeces = " + FormatSql.Format_Date(p_drw["DAP"].ToString());
                }

                z_strRequete += ", Email = " + FormatSql.Format_String(p_drw["Email"].ToString());      //Domi 13.10.2017

                // Where
                z_strRequete += " WHERE IdPersonne = " + FormatSql.Format_Nombre(p_drw["IdPersonne"].ToString());

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
