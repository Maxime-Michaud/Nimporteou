namespace nimporteou.Models
{
    public class Adresse
    {
        public int id { get; set; }

        public string Ad { get; set; }

        public Ville Ville { get; set; }

        /// <summary>
        /// Methode qui construit une string qui represente l'adresse
        /// </summary>
        /// <returns>L'adresse en un string</returns>
        public override string ToString()
        {
            string adresseComplete = Ad + " " + Ville.Nom;
            return adresseComplete;
        }
    }
}