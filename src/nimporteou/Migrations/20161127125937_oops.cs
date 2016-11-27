using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace nimporteou.Migrations
{
    public partial class oops : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evenements_Categories_Categorieid",
                table: "Evenements");

            migrationBuilder.DropIndex(
                name: "IX_Evenements_Categorieid",
                table: "Evenements");

            migrationBuilder.DropColumn(
                name: "Categorieid",
                table: "Evenements");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Categorieid",
                table: "Evenements",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Evenements_Categorieid",
                table: "Evenements",
                column: "Categorieid");

            migrationBuilder.AddForeignKey(
                name: "FK_Evenements_Categories_Categorieid",
                table: "Evenements",
                column: "Categorieid",
                principalTable: "Categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
