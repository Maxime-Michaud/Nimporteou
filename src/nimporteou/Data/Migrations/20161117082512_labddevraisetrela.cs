using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace nimporteou.Data.Migrations
{
    public partial class labddevraisetrela : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorie",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    Description = table.Column<string>(nullable: true),
                    Nom = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorie", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Groupe",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    Nom = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groupe", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Ville",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    Nom = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ville", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "UtilisateurGroupe",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    Groupeid = table.Column<int>(nullable: true),
                    Role = table.Column<int>(nullable: false),
                    UtilisateurId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UtilisateurGroupe", x => x.id);
                    table.ForeignKey(
                        name: "FK_UtilisateurGroupe_Groupe_Groupeid",
                        column: x => x.Groupeid,
                        principalTable: "Groupe",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UtilisateurGroupe_AspNetUsers_UtilisateurId",
                        column: x => x.UtilisateurId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Adresse",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    NumeroCivique = table.Column<int>(nullable: false),
                    Rue = table.Column<string>(nullable: true),
                    Villeid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adresse", x => x.id);
                    table.ForeignKey(
                        name: "FK_Adresse_Ville_Villeid",
                        column: x => x.Villeid,
                        principalTable: "Ville",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Evenement",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    Adresseid = table.Column<int>(nullable: true),
                    BilletsNecessaire = table.Column<bool>(nullable: false),
                    Categorieid = table.Column<int>(nullable: true),
                    DateLimite = table.Column<DateTime>(nullable: true),
                    Debut = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Fin = table.Column<DateTime>(nullable: false),
                    Nom = table.Column<string>(nullable: true),
                    Organisateursid = table.Column<int>(nullable: true),
                    Photo = table.Column<string>(nullable: true),
                    PrixBillet = table.Column<int>(nullable: false),
                    Publique = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evenement", x => x.id);
                    table.ForeignKey(
                        name: "FK_Evenement_Adresse_Adresseid",
                        column: x => x.Adresseid,
                        principalTable: "Adresse",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evenement_Categorie_Categorieid",
                        column: x => x.Categorieid,
                        principalTable: "Categorie",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evenement_Groupe_Organisateursid",
                        column: x => x.Organisateursid,
                        principalTable: "Groupe",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Billet",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    AcheteurId = table.Column<string>(nullable: true),
                    DateAchat = table.Column<DateTime>(nullable: false),
                    Evenementid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Billet", x => x.id);
                    table.ForeignKey(
                        name: "FK_Billet_AspNetUsers_AcheteurId",
                        column: x => x.AcheteurId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Billet_Evenement_Evenementid",
                        column: x => x.Evenementid,
                        principalTable: "Evenement",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Signalement",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    Description = table.Column<string>(nullable: true),
                    Evenementid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Signalement", x => x.id);
                    table.ForeignKey(
                        name: "FK_Signalement_Evenement_Evenementid",
                        column: x => x.Evenementid,
                        principalTable: "Evenement",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.AddColumn<bool>(
                name: "Admin",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateNaissance",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nom",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Adresse_Villeid",
                table: "Adresse",
                column: "Villeid");

            migrationBuilder.CreateIndex(
                name: "IX_Billet_AcheteurId",
                table: "Billet",
                column: "AcheteurId");

            migrationBuilder.CreateIndex(
                name: "IX_Billet_Evenementid",
                table: "Billet",
                column: "Evenementid");

            migrationBuilder.CreateIndex(
                name: "IX_Evenement_Adresseid",
                table: "Evenement",
                column: "Adresseid");

            migrationBuilder.CreateIndex(
                name: "IX_Evenement_Categorieid",
                table: "Evenement",
                column: "Categorieid");

            migrationBuilder.CreateIndex(
                name: "IX_Evenement_Organisateursid",
                table: "Evenement",
                column: "Organisateursid");

            migrationBuilder.CreateIndex(
                name: "IX_Signalement_Evenementid",
                table: "Signalement",
                column: "Evenementid");

            migrationBuilder.CreateIndex(
                name: "IX_UtilisateurGroupe_Groupeid",
                table: "UtilisateurGroupe",
                column: "Groupeid");

            migrationBuilder.CreateIndex(
                name: "IX_UtilisateurGroupe_UtilisateurId",
                table: "UtilisateurGroupe",
                column: "UtilisateurId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Admin",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DateNaissance",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Nom",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Billet");

            migrationBuilder.DropTable(
                name: "Signalement");

            migrationBuilder.DropTable(
                name: "UtilisateurGroupe");

            migrationBuilder.DropTable(
                name: "Evenement");

            migrationBuilder.DropTable(
                name: "Adresse");

            migrationBuilder.DropTable(
                name: "Categorie");

            migrationBuilder.DropTable(
                name: "Groupe");

            migrationBuilder.DropTable(
                name: "Ville");
        }
    }
}
