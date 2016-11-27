namespace nimporteou.Models
{
    /// <summary>
    /// Représente une catégorie d'évènements 
    /// </summary>
    public class Categorie
    {
        public int id { get; set; }

        /// <summary>
        /// Le nom de la catégorie (ex: Humour, Fête, Groupe de support pour nain de jardin, etc) 
        /// </summary>
        public string Nom { get; set; }
    }
}