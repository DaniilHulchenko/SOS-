using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using SosMedecins.Connexion;

namespace SosMedecins.SmartRapport.DAL
{
    public class Fonction
    {
        #region Compteur
       
        public enum DirectionCompteur
        {
            Minimum,
            Maximum
        }
        public long? Compteur(string p_strNomTable, string p_strIdentifiant, DirectionCompteur p_objDirection)
        {
            long? z_lngRetour = 1;

            Boolean z_blnConnect = Variables.ConnexionBase.OpenBDD();
            try
            {
                if (p_objDirection == DirectionCompteur.Maximum)
                {
                    z_lngRetour = (long?)Variables.ConnexionBase.ExecuteScalar("SELECT max(" + p_strIdentifiant + ") from " + p_strNomTable);
                    if (z_lngRetour == null)
                    {
                        z_lngRetour = 1;
                    }
                    else
                    {
                        z_lngRetour++;
                    }
                }
                else
                {
                    z_lngRetour = (long?)Variables.ConnexionBase.ExecuteScalar("SELECT min(" + p_strIdentifiant + ") from " + p_strNomTable);
                    if (z_lngRetour == null)
                    {
                        z_lngRetour = -1;
                    }
                    else
                    {
                        z_lngRetour--;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur d'application - DAL.Fonction.Compteur /r/n" + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return z_lngRetour;
            }
            finally
            {
                if (z_blnConnect)
                {
                    Variables.ConnexionBase.CloseBDD();
                }
            }
            return z_lngRetour;
        }
        #endregion

        #region Verification
        public bool CodeAppelExiste(long IdAppel)
        {
            Boolean z_blnConnect = Variables.ConnexionBase.OpenBDD();
            try
            {
                string z_strSql = RequetesSelect.tableactes.Num.Replace("%Num", IdAppel.ToString());

                //System.Data.DataTable z_drr = _strConnexion.ExecuteSql(null, z_strSql);
                if (Variables.ConnexionBase.ExecuteScalar(z_strSql) == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur d'application - DAL.Fonction.CodeAppelExiste /r/n" + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                if (z_blnConnect)
                {
                    Variables.ConnexionBase.CloseBDD();
                }
            }
        }

        public bool NumeroConsultationExiste(long NConsult)
        {
            Boolean z_blnConnect = Variables.ConnexionBase.OpenBDD();
            try
            {
                string z_strSql = RequetesSelect.tableconsultations.NConsultation.Replace("%NConsultation", NConsult.ToString());

                if (Variables.ConnexionBase.ExecuteScalar(z_strSql) == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur d'application - DAL.Fonction.NumeroConsultationExiste /r/n" + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                if (z_blnConnect)
                {
                    Variables.ConnexionBase.CloseBDD();
                }
            }
        }
        #endregion

        #region Patient - Personne
        public void CreationPatient(DataRow row, DataRow Appel)
        {
            long? IdPersonne = CreationPersonne(row);
            long? IdPatient = Compteur("tablepatient", "IdPatient", DirectionCompteur.Minimum);
            long? NConsult = Compteur("tableconsultations", "NConsultation", DirectionCompteur.Minimum);
            // Insertion du patient 
            Boolean z_blnConnect = Variables.ConnexionBase.OpenBDD();
            try
            {
                string z_strSql = "INSERT INTO tablepatient (IdPatient,IdPersonne,SuiviPatient,Approuve) ";
                z_strSql += " values ('" + IdPatient.ToString() + "','" + IdPersonne + "','" + row["SuiviPatient"].ToString().Replace("'", "''") + "',0)";
                Variables.ConnexionBase.ExecuteSqlSansRetour(z_strSql);

                z_strSql = "INSERT INTO tableconsultations (NConsultation,CodeAppel,IndicePatient,Approuve) ";
                z_strSql += " values ('" + NConsult.ToString() + "','" + Appel["Num"].ToString() + "','" + IdPatient.ToString() + "',0)";
                Variables.ConnexionBase.ExecuteSqlSansRetour(z_strSql);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur d'application - DAL.Fonction.NumeroConsultationExiste /r/n" + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (z_blnConnect)
                {
                    Variables.ConnexionBase.CloseBDD();
                }
            }
        }

        public long? CreationPersonne(DataRow row)
        {
            long? NumPersonne = null;
            // Création de la personne :
            Boolean z_blnConnect = Variables.ConnexionBase.OpenBDD();
            try
            {
                string DtNaissance = FormatSql.Format_DateHeure(row["DateNaissance"].ToString());

                NumPersonne = Compteur("tablepersonne", "IdPersonne", DirectionCompteur.Minimum);

                string z_strSql = "INSERT INTO tablepersonne ";
                z_strSql += " (IdPersonne,Tel,Nom,Prenom,NumAdresse,CodePostal,Departement,Commune,Rue,NumeroDansRue,Batiment,Escalier,Etage,Digicode,Internom,Porte,Longitude,Latitude,DateNaissance,Sexe,Age,UniteAge,TexteSup)";
                z_strSql += " values ('" + NumPersonne + "','" + row["Tel"].ToString() + "','" + row["Nom"].ToString().Replace("'", "''") + "','" + row["Prenom"].ToString().Replace("'", "''") + "','" + row["NumAdresse"].ToString().Replace("'", "''") + "',";
                z_strSql += " '" + row["CodePostal"].ToString().Replace("'", "''") + "','" + row["Departement"].ToString().Replace("'", "''") + "','" + row["Commune"].ToString().Replace("'", "''") + "','" + row["Rue"].ToString().Replace("'", "''") + "','" + row["NumeroDansRue"].ToString().Replace("'", "''") + "','" + row["Batiment"].ToString().Replace("'", "''") + "','" + row["Escalier"].ToString().Replace("'", "''") + "','" + row["Etage"].ToString().Replace("'", "''") + "',";
                z_strSql += " '" + row["Digicode"].ToString().Replace("'", "''") + "','" + row["InterNom"].ToString().Replace("'", "''") + "','" + row["Porte"].ToString().Replace("'", "''") + "','" + row["Longitude"].ToString().Replace("'", "''") + "','" + row["Latitude"].ToString().Replace("'", "''") + "'," + DtNaissance + ",'" + row["Sexe"].ToString().Replace("'", "''") + "',";
                z_strSql += " '" + row["Age"].ToString().Replace("'", "''") + "','" + row["UniteAge"].ToString().Replace("'", "''") + "','" + row["TexteSup"].ToString().Replace("'", "''") + "')";

                Variables.ConnexionBase.ExecuteSqlSansRetour(z_strSql);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur d'application - DAL.Fonction.CreationPersonne /r/n" + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (z_blnConnect)
                {
                    Variables.ConnexionBase.CloseBDD();
                }
            }
            return NumPersonne;
        }

        // Traiement encaissement sur place
        public void EncaissementSurPlace(long p_lFacture)
        {
            object objMontant;
            string z_strSql = "Select IndicePatient from factureconsultation f, tableconsultations t where t.NConsultation=f.NConsultation and NFacture = " + p_lFacture.ToString();
            // recherche le nom du numero de patient
            Boolean z_blnConnect = Variables.ConnexionBase.OpenBDD();
            try
            {
                long lPatient = (long)(Variables.ConnexionBase.ExecuteScalar(z_strSql));

                // si tout est encaissé sur plus de 3 mois - on enleve les encaissements sur place
                z_strSql = "SELECT IndicePatient , SUM( Solde ) , COUNT(t.NConsultation)";
                z_strSql += " FROM tableconsultations t, factureconsultation fc, facture f";
                z_strSql += " WHERE fc.NFacture=f.NFacture AND t.NConsultation=fc.NConsultation and solde > 10";
                z_strSql += " and DateCreation <= " + FormatSql.Format_Date(DateTime.Now.AddMonths(-3).ToString());
                z_strSql += " and IndicePatient = " + lPatient;
                z_strSql += " group by IndicePatient";

                objMontant = Variables.ConnexionBase.ExecuteScalar(z_strSql);
                long lMontant;
                if (objMontant == null)
                {
                    lMontant = 0;
                }
                else
                {
                    lMontant = (long)objMontant;
                }

                if (lMontant <= 10)
                {
                    // on enleve encaissement sur place
                    //Patient_Remarque z_dalPatientRemarque = new Patient_Remarque();
                    //z_dalPatientRemarque.UpdateEconomique(lPatient.ToString(), "");
                }
            }
            finally
            {
                if (z_blnConnect)
                {
                    Variables.ConnexionBase.CloseBDD();
                }
            }
        }
        #endregion

        #region Facture

        public void SupprimeFacture(string p_strNConsultation, string p_strNFacture)
        {   
            Boolean z_blnConnect = Variables.ConnexionBase.OpenBDD();
            try
            {
                Variables.ConnexionBase.ExecuteSqlSansRetour("update tableconsultations set FactureGeneree = 0 WHERE NConsultation = " + p_strNConsultation);
                Variables.ConnexionBase.ExecuteSqlSansRetour("delete from facture_prest where NFacture = " + p_strNFacture);
                Variables.ConnexionBase.ExecuteSqlSansRetour("delete from fac_pres_police where NFacture = " + p_strNFacture);
                Variables.ConnexionBase.ExecuteSqlSansRetour("delete from factureconsultation where NFacture = " + p_strNFacture);
                Variables.ConnexionBase.ExecuteSqlSansRetour("delete from facture_status where NFacture = " + p_strNFacture);
                Variables.ConnexionBase.ExecuteSqlSansRetour("delete from facture_etats where NFacture = " + p_strNFacture);
                Variables.ConnexionBase.ExecuteSqlSansRetour("delete from facture where NFacture = " + p_strNFacture);
            }
            finally
            {
                if (z_blnConnect)
                {
                    Variables.ConnexionBase.CloseBDD();
                }
            }
        }

        public void EnregistreModification(string p_strNConsultation, string p_strCodeUtilisateur, DateTime p_dteDateModifification, int p_intTypeModif, string p_strCommentaire)
        {
            Boolean z_blnConnect = Variables.ConnexionBase.OpenBDD();
            try
            {
                string z_strSql = "INSERT INTO tablemodifications (NConsultation, CodeUtilisateur, DateModif, Type, Commentaire) VALUES ( ";
                z_strSql += FormatSql.Format_Nombre(p_strNConsultation);
                z_strSql += "," + FormatSql.Format_String(p_strCodeUtilisateur);
                z_strSql += "," + FormatSql.Format_Date(p_dteDateModifification.ToString());
                z_strSql += "," + FormatSql.Format_Nombre(p_intTypeModif.ToString());
                z_strSql += "," + FormatSql.Format_String(p_strCommentaire);
                z_strSql += " )";

                Variables.ConnexionBase.ExecuteSqlSansRetour(z_strSql);
            }
            finally
            {
                if (z_blnConnect)
                {
                    Variables.ConnexionBase.CloseBDD();
                }
            }
        }
        #endregion
    }
}
