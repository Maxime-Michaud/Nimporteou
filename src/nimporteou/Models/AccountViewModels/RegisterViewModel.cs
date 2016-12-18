using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using nimporteou.Data;

namespace nimporteou.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(20, ErrorMessage = "Le {0} doit faire entre {2} et {1} charactères", MinimumLength = 6)]
        [Display(Name = "Pseudonyme")]
        public string Username { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Le {0} doit faire entre {2} et {1} charactères", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmation du mot de passe")]
        [Compare("Password", ErrorMessage = "Le Mot de passe et la confirmation ne sont pas pareil")]
        public string ConfirmPassword { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Courriel")]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date de naissance")]
        public DateTime? DateNaissance { get; set; }

        [Display(Name = "Categorie préférée")]
        public string Categorie1 { get; set; }

        [Display(Name = "Autre categorie préférée")]
        public string Categorie2 { get; set; }

        [Display(Name = "Autre categorie préférée")]
        public string Categorie3 { get; set; }

        [Display(AutoGenerateField = false)]
        public IEnumerable<string> CategoriesPossible{ get; set; }

        public ApplicationUser ToApplicationUser(ApplicationDbContext db)
        {
            IEnumerable<Categorie> cats = db.Categories.Where(c => c.Nom == Categorie1 || c.Nom == Categorie2 || c.Nom == Categorie3);
            var usr = new ApplicationUser
            {
                UserName = Username,
                Email = Email,
                DateNaissance = DateNaissance
            };

            usr.CategoriesPreferees = cats.Select(c => new CategorieUtilisateur { Categorie = c, Utilisateur = usr }).ToList();
            return usr;
        }
    }
}
