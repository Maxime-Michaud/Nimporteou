namespace nimporteou.Models
{
    public class UtilisateurGroupe
    {
        public int id { get; set; }
        public ApplicationUser Utilisateur { get; set; }
        public Groupe Groupe { get; set; }
        public Roles Role { get; set; }

        public enum Roles
        {
            Membre,
            Administrateur,
            Createur
        };
    }
}