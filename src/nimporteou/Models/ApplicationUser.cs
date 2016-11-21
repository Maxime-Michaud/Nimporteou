using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace nimporteou.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public List<Billet> Billets { get; set; }
        public List<UtilisateurGroupe> Groupes { get; set; }
        public string Nom { get; set; }
        public DateTime? DateNaissance { get; set; }
        public bool Admin { get; set; }
    }
}
