using System;
using System.Collections.Generic;
using System.Text;

namespace SosMedecins.SmartRapport.DAL
{
    public class Variables
    {
        private static SosMedecins.Connexion.AccesDonnees _ConnexionBase;
        public static SosMedecins.Connexion.AccesDonnees ConnexionBase
        {
            get
            {
                return _ConnexionBase;
            }
            set
            {
                _ConnexionBase = value;
                Properties.Settings.Default["SmartRapportConnectionString"] = _ConnexionBase.ConnectionString;
            }
        }

        public class InfoConnexion
        {
            public static String NomServeur;
            public static String BaseDonnees;
            public static String Utilisateur;
            public static String MotDePasse;
        }
    }
}
