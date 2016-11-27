using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nimporteou.Models.EvenementViewModels
{
    public class ListeEvenementViewModel
    {
        public IEnumerable<ConsultationEvenementViewModel> Evenements { get; set; }
    }
}
