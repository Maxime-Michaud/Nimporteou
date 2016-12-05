﻿using System;
using System.ComponentModel.DataAnnotations;

namespace nimporteou.Models.EvenementViewModels
{
    abstract public class EvenementViewModel
    {
        public bool BilletNecessaire { get; set; }
        public DateTime? DateLimite { get; set; }
        public DateTime HeureDebut { get; set; }
        public DateTime Duree { get; set; }
        public DateTime Debut { get; set; }
        public DateTime Fin { get; set; }

        [StringLength(1000, ErrorMessage = "Le nom doit contenir moins de 1000 caratères")]
        public string Description { get; set; }

        [StringLength(100, ErrorMessage = "Le nom doit contenir plus de 5 caratères et moins de 100 caratères", MinimumLength = 5)]
        public string Nom { get; set; }
        public string CheminPhoto { get; set; }
        public int? PrixBillet { get; set; }
        public string Categorie { get; set; }
        public string AdresseComplete { get; set; }
    }
}