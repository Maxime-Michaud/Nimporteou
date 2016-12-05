using System;

namespace nimporteou.Models.EvenementViewModels
{
    public class BaseEvenementViewModel
    {
        public int EvenementID { get; set; }

        public string AdresseComplete { get; set; }
        public bool BilletNecessaire { get; set; }
        public string CheminPhoto { get; set; }
        public DateTime Debut { get; set; }
        public string Nom { get; set; }
        public int? PrixBillet { get; set; }
    }
}