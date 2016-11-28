using System;

namespace nimporteou.Models.EvenementViewModels
{
    abstract public class EvenementViewModel
    {
        public bool BilletNecessaire { get; set; }
        public DateTime? DateLimite { get; set; }
        public DateTime Debut { get; set; }
        public DateTime Fin { get; set; }
        public string Description { get; set; }
        public string Nom { get; set; }
        public string CheminPhoto { get; set; }
        public int? PrixBillet { get; set; }
        public string Categorie { get; set; }
        public string AdresseComplete { get; set; }
    }
}