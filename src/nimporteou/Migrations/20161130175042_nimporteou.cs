using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace nimporteou.Migrations
{
    public partial class nimporteou : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adresse_Ville_Villeid",
                table: "Adresse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ville",
                table: "Ville");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Villes",
                table: "Ville",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Adresse_Villes_Villeid",
                table: "Adresse",
                column: "Villeid",
                principalTable: "Ville",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.RenameTable(
                name: "Ville",
                newName: "Villes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adresse_Villes_Villeid",
                table: "Adresse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Villes",
                table: "Villes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ville",
                table: "Villes",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Adresse_Ville_Villeid",
                table: "Adresse",
                column: "Villeid",
                principalTable: "Villes",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.RenameTable(
                name: "Villes",
                newName: "Ville");
        }
    }
}
