using System;

namespace nimporteou.Models
{
    public class Billet
    {
        public int id { get; set; }
        public DateTime DateAchat { get; set; }
        public Evenement Evenement { get; set; }
        public ApplicationUser Acheteur { get; set; }
    }
}