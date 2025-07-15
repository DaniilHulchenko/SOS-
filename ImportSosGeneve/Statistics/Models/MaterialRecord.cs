using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportSosGeneve.Statistics.Models
{
    public class MaterialRecord
    {
        public string Medecin { get; set; }
        public string Patient { get; set; }
        public int Quantity { get; set; }
    }
}
