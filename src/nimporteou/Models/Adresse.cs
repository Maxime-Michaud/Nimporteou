namespace nimporteou.Models
{
    public class Adresse
    {
        public int id { get; set; }

        public int NumeroCivique { get; set; }

        public string Rue { get; set; }

        public Ville Ville { get; set; }

        /// <summary>
        /// Methode qui construit une string qui represente l'adresse
        /// </summary>
        /// <returns>L'adresse en un string</returns>
        public override string ToString()
        {
            string adresseComplete = NumeroCivique.ToString() + " " + Rue + " " + Ville.Nom;
            return adresseComplete;
        }
    }
}