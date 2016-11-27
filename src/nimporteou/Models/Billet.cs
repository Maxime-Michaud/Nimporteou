using System;

namespace nimporteou.Models
{
    /// <summary>
    /// Représente un billet acheter par un utilisateur
    /// </summary>
    public class Billet
    {
        public int id { get; set; }

        /// <summary>
        /// La date de l'achat du billet
        /// </summary>
        public DateTime DateAchat { get; set; }

        /// <summary>
        /// Le montant payé par l'utilisateur pour le billet, en ¢
        /// </summary>
        public int Prix { get; set; }
    }
}