namespace nimporteou.Models
{
    public class Adresse
    {
        public int id { get; set; }

        public int NumeroCivique { get; set; }

        public string Rue { get; set; }

        public Ville Ville { get; set; }
    }
}