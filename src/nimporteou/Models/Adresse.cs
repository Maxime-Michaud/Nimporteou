using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nimporteou.Models
{
    public class Adresse
    {
        public int id { get; set; }
        public string Rue { get; set; }
        public int NumeroCivique { get; set; }
        public Ville Ville { get; set; }
    }
}
