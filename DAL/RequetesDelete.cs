using System;
using System.Collections.Generic;
using System.Text;

namespace SosMedecins.SmartRapport.DAL
{
    public class RequetesDelete
    {
        public class ta_abonnementjournal
        {
            public static string Ligne = "delete from ta_abonnementjournal Where Id = %Ligne";
        }

        public class facture_arrangement
        {
            public static string Ligne = "delete from facture_arrangement Where NFacture = %NFacture%";
        }
    }
}
