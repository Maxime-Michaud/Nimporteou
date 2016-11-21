using System.Collections.Generic;

namespace nimporteou.Models
{
    public class Groupe
    {
        public int id { get; set; }
        public string Nom { get; set; }
        public List<UtilisateurGroupe> Membres { get; set; }
    }
}