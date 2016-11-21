using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nimporteou.Models
{
    public class Evenement
    {
        public int id { get; set; }
        public string Description { get; set; }
        public bool Publique { get; set; }
        public bool BilletsNecessaire { get; set; }
        public DateTime? DateLimite { get; set; }
        public int PrixBillet { get; set; }
        public string Nom { get; set; }
        public string Photo { get; set; }
        public DateTime Debut { get; set; }
        public DateTime Fin { get; set; }
        public Categorie Categorie { get; set; }
        public List<Signalement> Signalements { get; set; }
        public List<Billet> Billets { get; set; }
        public Groupe Organisateurs { get; set; }
        public Adresse Adresse { get; set; }
    }
}
