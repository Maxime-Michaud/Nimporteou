using System;

namespace nimporteou.Models.EvenementViewModels
{
    abstract public class EvenementViewModel : BaseEvenementViewModel
    {
        public DateTime? DateLimite { get; set; }
        public DateTime Fin { get; set; }
        public string Description { get; set; }
        public string Categorie { get; set; }
    }
}