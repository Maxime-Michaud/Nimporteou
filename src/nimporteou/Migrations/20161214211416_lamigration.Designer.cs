using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using nimporteou.Data;

namespace nimporteou.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20161214211416_lamigration")]
    partial class lamigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<int>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("nimporteou.Models.Adresse", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Ad");

                    b.Property<int?>("Villeid");

                    b.HasKey("id");

                    b.HasIndex("Villeid");

                    b.ToTable("Adresses");
                });

            modelBuilder.Entity("nimporteou.Models.ApplicationUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<bool>("Admin");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime?>("DateNaissance");

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Nom");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("Prenom");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("nimporteou.Models.Billet", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateAchat");

                    b.Property<int>("Prix");

                    b.HasKey("id");

                    b.ToTable("Billet");
                });

            modelBuilder.Entity("nimporteou.Models.Categorie", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nom");

                    b.HasKey("id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("nimporteou.Models.CategorieUtilisateur", b =>
                {
                    b.Property<int>("Categorie_id");

                    b.Property<int>("Utilisateur_id");

                    b.HasKey("Categorie_id", "Utilisateur_id");

                    b.HasIndex("Categorie_id");

                    b.HasIndex("Utilisateur_id");

                    b.ToTable("CategorieUtilisateur");
                });

            modelBuilder.Entity("nimporteou.Models.Evenement", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AgeMinimum");

                    b.Property<bool>("Annulé");

                    b.Property<bool>("BilletNecessaire");

                    b.Property<int>("Categorie_id");

                    b.Property<string>("CheminPhoto");

                    b.Property<DateTime?>("DateLimite");

                    b.Property<DateTime>("Debut");

                    b.Property<string>("Description")
                        .HasAnnotation("MaxLength", 1000);

                    b.Property<string>("Duree");

                    b.Property<int?>("Endroitid");

                    b.Property<DateTime>("Fin");

                    b.Property<TimeSpan>("HeureDebut");

                    b.Property<string>("Nom")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<int?>("PrixBillet");

                    b.Property<bool>("Public");

                    b.HasKey("id");

                    b.HasIndex("Categorie_id");

                    b.HasIndex("Endroitid");

                    b.ToTable("Evenements");
                });

            modelBuilder.Entity("nimporteou.Models.Participation", b =>
                {
                    b.Property<int>("Participant_id");

                    b.Property<int>("Evenement_id");

                    b.Property<int?>("Billetid");

                    b.Property<int>("NombreParticipants");

                    b.Property<int>("Role");

                    b.Property<int?>("Signalementid");

                    b.HasKey("Participant_id", "Evenement_id");

                    b.HasIndex("Billetid");

                    b.HasIndex("Evenement_id");

                    b.HasIndex("Participant_id");

                    b.HasIndex("Signalementid");

                    b.ToTable("Participations");
                });

            modelBuilder.Entity("nimporteou.Models.Signalement", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Commentaire");

                    b.HasKey("id");

                    b.ToTable("Signalement");
                });

            modelBuilder.Entity("nimporteou.Models.Ville", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nom");

                    b.HasKey("id");

                    b.ToTable("Villes");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole<int>")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("nimporteou.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("nimporteou.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole<int>")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("nimporteou.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("nimporteou.Models.Adresse", b =>
                {
                    b.HasOne("nimporteou.Models.Ville", "Ville")
                        .WithMany()
                        .HasForeignKey("Villeid");
                });

            modelBuilder.Entity("nimporteou.Models.CategorieUtilisateur", b =>
                {
                    b.HasOne("nimporteou.Models.Categorie", "Categorie")
                        .WithMany()
                        .HasForeignKey("Categorie_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("nimporteou.Models.ApplicationUser", "Utilisateur")
                        .WithMany("CategoriesPreferees")
                        .HasForeignKey("Utilisateur_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("nimporteou.Models.Evenement", b =>
                {
                    b.HasOne("nimporteou.Models.Categorie", "Categorie")
                        .WithMany()
                        .HasForeignKey("Categorie_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("nimporteou.Models.Adresse", "Endroit")
                        .WithMany()
                        .HasForeignKey("Endroitid");
                });

            modelBuilder.Entity("nimporteou.Models.Participation", b =>
                {
                    b.HasOne("nimporteou.Models.Billet", "Billet")
                        .WithMany()
                        .HasForeignKey("Billetid");

                    b.HasOne("nimporteou.Models.Evenement", "Evenement")
                        .WithMany()
                        .HasForeignKey("Evenement_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("nimporteou.Models.ApplicationUser", "Participant")
                        .WithMany()
                        .HasForeignKey("Participant_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("nimporteou.Models.Signalement", "Signalement")
                        .WithMany()
                        .HasForeignKey("Signalementid");
                });
        }
    }
}
