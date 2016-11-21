using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using nimporteou.Data;

namespace nimporteou.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20161117082512_labddevraisetrela")]
    partial class labddevraisetrela
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id");

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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

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

                    b.Property<int>("NumeroCivique");

                    b.Property<string>("Rue");

                    b.Property<int?>("Villeid");

                    b.HasKey("id");

                    b.HasIndex("Villeid");

                    b.ToTable("Adresse");
                });

            modelBuilder.Entity("nimporteou.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id");

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

                    b.Property<string>("AcheteurId");

                    b.Property<DateTime>("DateAchat");

                    b.Property<int?>("Evenementid");

                    b.HasKey("id");

                    b.HasIndex("AcheteurId");

                    b.HasIndex("Evenementid");

                    b.ToTable("Billet");
                });

            modelBuilder.Entity("nimporteou.Models.Categorie", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Nom");

                    b.HasKey("id");

                    b.ToTable("Categorie");
                });

            modelBuilder.Entity("nimporteou.Models.Evenement", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("Adresseid");

                    b.Property<bool>("BilletsNecessaire");

                    b.Property<int?>("Categorieid");

                    b.Property<DateTime?>("DateLimite");

                    b.Property<DateTime>("Debut");

                    b.Property<string>("Description");

                    b.Property<DateTime>("Fin");

                    b.Property<string>("Nom");

                    b.Property<int?>("Organisateursid");

                    b.Property<string>("Photo");

                    b.Property<int>("PrixBillet");

                    b.Property<bool>("Publique");

                    b.HasKey("id");

                    b.HasIndex("Adresseid");

                    b.HasIndex("Categorieid");

                    b.HasIndex("Organisateursid");

                    b.ToTable("Evenement");
                });

            modelBuilder.Entity("nimporteou.Models.Groupe", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nom");

                    b.HasKey("id");

                    b.ToTable("Groupe");
                });

            modelBuilder.Entity("nimporteou.Models.Signalement", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<int?>("Evenementid");

                    b.HasKey("id");

                    b.HasIndex("Evenementid");

                    b.ToTable("Signalement");
                });

            modelBuilder.Entity("nimporteou.Models.UtilisateurGroupe", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("Groupeid");

                    b.Property<int>("Role");

                    b.Property<string>("UtilisateurId");

                    b.HasKey("id");

                    b.HasIndex("Groupeid");

                    b.HasIndex("UtilisateurId");

                    b.ToTable("UtilisateurGroupe");
                });

            modelBuilder.Entity("nimporteou.Models.Ville", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nom");

                    b.HasKey("id");

                    b.ToTable("Ville");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("nimporteou.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("nimporteou.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
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

            modelBuilder.Entity("nimporteou.Models.Billet", b =>
                {
                    b.HasOne("nimporteou.Models.ApplicationUser", "Acheteur")
                        .WithMany("Billets")
                        .HasForeignKey("AcheteurId");

                    b.HasOne("nimporteou.Models.Evenement", "Evenement")
                        .WithMany("Billets")
                        .HasForeignKey("Evenementid");
                });

            modelBuilder.Entity("nimporteou.Models.Evenement", b =>
                {
                    b.HasOne("nimporteou.Models.Adresse", "Adresse")
                        .WithMany()
                        .HasForeignKey("Adresseid");

                    b.HasOne("nimporteou.Models.Categorie", "Categorie")
                        .WithMany()
                        .HasForeignKey("Categorieid");

                    b.HasOne("nimporteou.Models.Groupe", "Organisateurs")
                        .WithMany()
                        .HasForeignKey("Organisateursid");
                });

            modelBuilder.Entity("nimporteou.Models.Signalement", b =>
                {
                    b.HasOne("nimporteou.Models.Evenement")
                        .WithMany("Signalements")
                        .HasForeignKey("Evenementid");
                });

            modelBuilder.Entity("nimporteou.Models.UtilisateurGroupe", b =>
                {
                    b.HasOne("nimporteou.Models.Groupe", "Groupe")
                        .WithMany("Membres")
                        .HasForeignKey("Groupeid");

                    b.HasOne("nimporteou.Models.ApplicationUser", "Utilisateur")
                        .WithMany("Groupes")
                        .HasForeignKey("UtilisateurId");
                });
        }
    }
}
