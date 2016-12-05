using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace nimporteou.Models
{
    /// <summary>
    /// Un utilisateur de l'application
    /// </summary>
    public class ApplicationUser : IdentityUser<int>
    {
        public ApplicationUser()
        {
            CategoriesPreferees = new List<CategorieUtilisateur>();
        }

        /// <summary>
        /// Le nom de l'utilisateur
        /// </summary>
        public string Nom { get; set; }

        /// <summary>
        /// Le prenom de l'utilisateur
        /// </summary>
        public string Prenom { get; set; }

        /// <summary>
        /// La date de naissance de l'utilisateur. Elle est optionelle, mais permet de valider qu'un utilisateur à au moins l'age minimum pour un evenement
        /// </summary>
        public DateTime? DateNaissance { get; set; }

        /// <summary>
        /// Si l'utilisateur est un administrateur.
        /// </summary>
        public bool Admin { get; set; }

        /// <summary>
        /// Les catégories d'événements préférés de l'utilisateur
        /// </summary>
        public IEnumerable<CategorieUtilisateur> CategoriesPreferees { get; set; }

        [NotMapped]
        public int? Age
        {
            get
            {
                return DateTime.Now.Year - DateNaissance?.Year;
            }
        }
    }
}
