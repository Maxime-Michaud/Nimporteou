﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nimporteou.Models
{
    public class Evenement
    {
        public int id { get; set; }
        
        /// <summary>
        /// Si un billet est nécéssaire
        /// </summary>
        public bool BilletNecessaire { get; set; }

        /// <summary>
        /// La date limite pour s'inscrire
        /// </summary>
        public DateTime? DateLimite { get; set; }

        /// <summary>
        /// Le moment ou l'évènement commence
        /// </summary>
        public DateTime Debut { get; set; }

        /// <summary>
        /// Le moment ou l'évènement fini
        /// </summary>
        public DateTime Fin { get; set; }

        /// <summary>
        /// La description détaillée de l'évènement
        /// </summary>
        [StringLength(1000, ErrorMessage = "Le nom doit contenir plus de 5 caratères et moins de 100 caratères")]
        public string Description { get; set; }

        /// <summary>
        /// Le nom de l'évènement (ex: Festival de nain de jardin)
        /// </summary>
        [StringLength(100, ErrorMessage = "Le nom doit contenir plus de 5 caratères et moins de 100 caratères", MinimumLength = 5)]
        public string Nom { get; set; }

        /// <summary>
        /// Le chemin pour accéder a la photo de l,évènement
        /// </summary>
        public string CheminPhoto { get; set; }

        /// <summary>
        /// Le prix d'un billet, en ¢
        /// </summary>
        public int? PrixBillet { get; set; }

        /// <summary>
        /// Si un evenement est public ou non
        /// </summary>
        public bool Public { get; set; }

        /// <summary>
        /// Si un évènement a été annulé
        /// </summary>
        public bool Annulé { get; set; }

        /// <summary>
        /// L'age minimum pour pouvoir participer a l'évènement
        /// </summary>
        public int? AgeMinimum { get; set; }

        /// <summary>
        /// L'endroit ou l'évènement a lieu
        /// </summary>
        public Adresse Endroit { get; set; }

        /// <summary>
        /// La catégorie de l'évènement
        /// </summary>
        [ForeignKey("Categorie_id")]
        public Categorie Categorie { get; set; }

        /// <summary>
        /// ID de la catégorie
        /// </summary>
        public int Categorie_id { get; set; }

        /// <summary>
        /// L'heure ou l'événement débute
        /// </summary>
        public TimeSpan HeureDebut { get; set; }

        /// <summary>
        /// La durée de l'événement
        /// </summary>
        public string Duree { get; set; }
    }
}