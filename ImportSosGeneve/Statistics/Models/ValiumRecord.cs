using System;

namespace ImportSosGeneve.Statistics.Models
{
    public class ValiumRecord
    {
        public string NomPatient { get; set; }
        public string PrenomPatient { get; set; }
        public string MedecinNom { get; set; }
        public string MedecinPrenom { get; set; }
        public DateTime? DateC { get; set; }
        public string NumAppel { get; set; }
        public string Libelle { get; set; }
        public string Categorie { get; set; }
        public int Qte { get; set; }
    }
}
