﻿using System;
using System.ComponentModel.DataAnnotations;

namespace nimporteou.Models.EvenementViewModels
{
    abstract public class EvenementViewModel : BaseEvenementViewModel
    {
        public DateTime? DateLimite { get; set; }
        public TimeSpan HeureDebut { get; set; }
        public string Duree { get; set; }
        public DateTime Debut { get; set; }
        public DateTime Fin { get; set; }
        public string Ville { get; set; }

        [StringLength(1000, ErrorMessage = "Le nom doit contenir moins de 1000 caratères")]
        public string Description { get; set; }

        public string Categorie { get; set; }
    }
}