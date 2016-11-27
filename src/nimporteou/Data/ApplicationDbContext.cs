using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using nimporteou.Models;

namespace nimporteou.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int> 
    {
        public DbSet<Participation> Participations { get; set; }
        public DbSet<Evenement> Evenements { get; set; }
        public DbSet<Categorie> Categories { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Participation>().HasKey(p => new { p.Participant_id, p.Evenement_id });
            builder.Entity<CategorieUtilisateur>().HasKey(c => new { c.Categorie_id, c.Utilisateur_id });
        }
    }
}
