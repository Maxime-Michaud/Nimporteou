using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace nimporteou.Models.EvenementViewModels
{
    public class BaseEvenementViewModel
    {
        public int EvenementID { get; set; }
        public string Nom { get; set; }
        public string CheminPhoto { get; set; }
        public int? PrixBillet { get; set; }
        public string AdresseComplete { get; set; }
        [Display(Name = "Billet nécéssaire")]
        public bool BilletNecessaire { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime Debut { get; set; }
        public TimeSpan HeureDebut { get; set; }

        public string PrixToString
        {
            get
            {
                return PrixBillet == null ?
                    "Gratuit" :
                    (PrixBillet.Value / 100.0).ToString("C", new CultureInfo("fr-CA"));
            }
        }
    }
}