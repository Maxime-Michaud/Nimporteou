using System.ComponentModel.DataAnnotations.Schema;

namespace nimporteou.Models
{
    /// <summary>
    /// Classe dassociation entre les catégories et les utilisateurs
    /// </summary>
    public class CategorieUtilisateur
    {
        public int Utilisateur_id { get; set; }

        [ForeignKey("Utilisateur_id")]
        public ApplicationUser Utilisateur { get; set; }

        public int Categorie_id { get; set; }

        [ForeignKey("Categorie_id")]
        public Categorie Categorie { get; set; }
    }
}