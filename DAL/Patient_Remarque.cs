using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SosMedecins.Connexion;

namespace SosMedecins.SmartRapport.DAL
{
    public class Patient_Remarque
    {
        public DataTable Select(string p_strIdPatient)
        {
            DataTable z_dtb;
            
            Boolean z_blnConnect = Variables.ConnexionBase.OpenBDD();
            try
            {
                string z_strSql = "SELECT * FROM patient_remarque WHERE IdPatient = " + p_strIdPatient;
                // mise a jour
                z_dtb = Variables.ConnexionBase.ExecuteSql(null, z_strSql);
            }
            finally
            {
                if (z_blnConnect)
                {
                    Variables.ConnexionBase.CloseBDD();
                }
            }
            return z_dtb;
        }

        public void UpdateRemarque(DataRow p_drwPatientRemarque, string p_strIdUtilisateur)
        {
            string z_strRequete = "UPDATE patient_remarque SET";
            z_strRequete += " Medical = " + FormatSql.Format_String(p_drwPatientRemarque["Medical"].ToString());
            z_strRequete += ", Economique = " + FormatSql.Format_String(p_drwPatientRemarque["Economique"].ToString());
            z_strRequete += ", Encaisse = " + FormatSql.Format_Nombre(p_drwPatientRemarque["Encaisse"].ToString());
            z_strRequete += ", Cession = " + FormatSql.Format_Nombre(p_drwPatientRemarque["Cession"].ToString());
            z_strRequete += ", Export = 0";
            z_strRequete += ", DateValidite = getDate()";
            z_strRequete += ", IdUtilisateur = " + FormatSql.Format_String(p_strIdUtilisateur);
            z_strRequete += " WHERE IdPAtient = " + FormatSql.Format_Nombre(p_drwPatientRemarque["IdPAtient"].ToString());

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

        public void InsertRemarque(DataRow p_drwPatientRemarque, string p_strIdUtilisateur)
        {
            string z_strRequete = "INSERT INTO patient_remarque";
            z_strRequete += " ( IdPatient, Encaisse, Cession, Medical, Economique, Export, Archive, DateValidite, IdUtilisateur ) VALUES ( ";

            z_strRequete += FormatSql.Format_Nombre(p_drwPatientRemarque["IdPAtient"].ToString());
            z_strRequete += ", " + FormatSql.Format_Nombre(p_drwPatientRemarque["Encaisse"].ToString());
            z_strRequete += ", " + FormatSql.Format_Nombre(p_drwPatientRemarque["Cession"].ToString());
            z_strRequete += ", " + FormatSql.Format_String(p_drwPatientRemarque["Medical"].ToString());
            z_strRequete += ", " + FormatSql.Format_String(p_drwPatientRemarque["Economique"].ToString());
            z_strRequete += ", 0";
            z_strRequete += ", 0";
            z_strRequete += ", getDate()";
            z_strRequete += ", " + FormatSql.Format_String(p_strIdUtilisateur);
            z_strRequete += " )";

            Boolean z_blnConnect = Variables.ConnexionBase.OpenBDD();
            try
            {
                Variables.ConnexionBase.ExecuteSqlSansRetour(z_strRequete);
                p_drwPatientRemarque.AcceptChanges();
            }
            finally
            {
                if (z_blnConnect)
                {
                    Variables.ConnexionBase.CloseBDD();
                }
            }
        }

        public void UpdateEconomique(string p_strIdPatient, string p_strIdUtilisateur)
        {
            string z_strRequete = "UPDATE patient_remarque SET";
            z_strRequete += " Encaisse = 0";
            z_strRequete += ", Cession = 0";
            z_strRequete += ", DateValidite = getDate()";
            z_strRequete += ", IdUtilisateur = " + FormatSql.Format_String(p_strIdUtilisateur);
            z_strRequete += " WHERE IdPAtient = " + FormatSql.Format_Nombre(p_strIdPatient);

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
