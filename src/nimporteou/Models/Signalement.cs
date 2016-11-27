using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nimporteou.Models
{
    /// <summary>
    /// Indique qu'un évènement peut aller a l'encontre des conditions de l'app
    /// </summary>
    public class Signalement
    {
        public int id { get; set; }

        /// <summary>
        /// La raison pour laquelle
        /// </summary>
        public string Commentaire { get; set; }
    }
}
