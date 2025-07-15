using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SosMedecins.Connexion;

namespace SosMedecins.SmartRapport.DAL
{
    public class TablePatient
    {
        public void Update(DataRow p_drw)
        {
            if (p_drw != null)
            {
                // Enregistrement de la personne :
                string z_strRequete = "update tablepatient set";
                z_strRequete += " SuiviPatient = " + FormatSql.Format_String(p_drw["SuiviPatient"].ToString());
                z_strRequete += ", Approuve = 0";
                z_strRequete += " WHERE IdPAtient = " + FormatSql.Format_Nombre(p_drw["IndicePatient"].ToString());

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

        public string ListePatientIdentique(long p_lngIdPatient)
        {
            // creation du dataset
            DataTable z_dtbtempCle = new DataTable("Cle");
            DataColumn[] z_dcn = new DataColumn[]{new DataColumn("idPatient")};
            z_dtbtempCle.Columns.AddRange(z_dcn);
            z_dtbtempCle.PrimaryKey = z_dcn;

            // Ajout d'une ligne
            DataRow z_drwTemp = z_dtbtempCle.NewRow();
            z_drwTemp["idPatient"] = p_lngIdPatient;
            z_dtbtempCle.Rows.Add(z_drwTemp);

            //
            Int32 z_intI = 0;
            Boolean z_blnConnect = Variables.ConnexionBase.OpenBDD();
            try
            {
                do
                {
                    DataTable z_dtb;

                    string z_strRequete = "SELECT IdPatient_Enfant, IdPatient_Parent from tablerapprochementpatient Where ";
                    z_strRequete += "IdPatient_Enfant = " + z_dtbtempCle.Rows[z_intI]["idPatient"];
                    z_strRequete += " Or IdPatient_Parent = " + z_dtbtempCle.Rows[z_intI]["idPatient"];
                    // mise a jour
                    z_dtb = Variables.ConnexionBase.ExecuteSql(null, z_strRequete);

                    foreach (DataRow z_drw in z_dtb.Rows)
                    {
                        //
                        if ( ! z_dtbtempCle.Rows.Contains(z_drw.ItemArray[0].ToString()) ) 
                        {
                            z_drwTemp = z_dtbtempCle.NewRow();
                            z_drwTemp["idPatient"] = z_drw.ItemArray[0].ToString();
                            z_dtbtempCle.Rows.Add(z_drwTemp);
                        }

                        if ( ! z_dtbtempCle.Rows.Contains(z_drw.ItemArray[1].ToString()) ) 
                        {
                            z_drwTemp = z_dtbtempCle.NewRow();
                            z_drwTemp["idPatient"] = z_drw.ItemArray[1].ToString();
                            z_dtbtempCle.Rows.Add(z_drwTemp);
                        }
                    }
                    z_intI += 1;
                }
                while (z_intI < z_dtbtempCle.Rows.Count);
            }
            finally
            {
                if (z_blnConnect)
                {
                    Variables.ConnexionBase.CloseBDD();
                }
            }

            string z_strRetour = "";
            foreach (DataRow z_drw in z_dtbtempCle.Rows)
            {
                if (z_strRetour.Length <= 0) 
                {
                    z_strRetour += z_drw["idPatient"].ToString();
                }
                else
                {
                    z_strRetour += "," + z_drw["idPatient"].ToString();
                }
            }

            return z_strRetour;
        }
    }
}
