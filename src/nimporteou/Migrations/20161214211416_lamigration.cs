using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace nimporteou.Migrations
{
    public partial class lamigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adresse_Villes_Villeid",
                table: "Adresse");

            migrationBuilder.DropForeignKey(
                name: "FK_Evenements_Adresse_Endroitid",
                table: "Evenements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Adresse",
                table: "Adresse");

            migrationBuilder.DropColumn(
                name: "NumeroCivique",
                table: "Adresse");

            migrationBuilder.DropColumn(
                name: "Rue",
                table: "Adresse");

            migrationBuilder.AddColumn<string>(
                name: "Ad",
                table: "Adresse",
                nullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "HeureDebut",
                table: "Evenements",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Duree",
                table: "Evenements",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Adresses",
                table: "Adresse",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Adresses_Villes_Villeid",
                table: "Adresse",
                column: "Villeid",
                principalTable: "Villes",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Evenements_Adresses_Endroitid",
                table: "Evenements",
                column: "Endroitid",
                principalTable: "Adresse",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.RenameIndex(
                name: "IX_Adresse_Villeid",
                table: "Adresse",
                newName: "IX_Adresses_Villeid");

            migrationBuilder.RenameTable(
                name: "Adresse",
                newName: "Adresses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adresses_Villes_Villeid",
                table: "Adresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Evenements_Adresses_Endroitid",
                table: "Evenements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Adresses",
                table: "Adresses");

            migrationBuilder.DropColumn(
                name: "Ad",
                table: "Adresses");

            migrationBuilder.AddColumn<int>(
                name: "NumeroCivique",
                table: "Adresses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Rue",
                table: "Adresses",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "HeureDebut",
                table: "Evenements",
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Duree",
                table: "Evenements",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Adresse",
                table: "Adresses",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Adresse_Villes_Villeid",
                table: "Adresses",
                column: "Villeid",
                principalTable: "Villes",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Evenements_Adresse_Endroitid",
                table: "Evenements",
                column: "Endroitid",
                principalTable: "Adresses",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.RenameIndex(
                name: "IX_Adresses_Villeid",
                table: "Adresses",
                newName: "IX_Adresse_Villeid");

            migrationBuilder.RenameTable(
                name: "Adresses",
                newName: "Adresse");
        }
    }
}
