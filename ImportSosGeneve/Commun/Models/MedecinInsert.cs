using System;

namespace ImportSosGeneve.Commun.Models
{
    class MedecinInsert
    {
        public int CodeIntervenant { get; set; }
        public string Nom { get; set; }
        public string Initiale { get; set; }
        public int Service { get; set; }
        public int Archive { get; set; }
        public string Mail { get; set; }
        public string NomGeneve { get; set; }
        public string PrenomGeneve { get; set; }
        public string Concordat { get; set; }
        public string EAN { get; set; }
        public string NIF { get; set; }
        public char Independant { get; set; }
        public string Commentaire { get; set; }
        public int Desactive { get; set; }
        public int MedInterne { get; set; }
        public DateTime DateMajCpt { get; set; }
        public int CptFacM { get; set; }
        public string RCC { get; set; }

    }
}
