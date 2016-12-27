using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace nimporteou.Migrations
{
    public partial class jaichangerdeschosesdanslabdpislafautjefasseunemigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Duree",
                table: "Evenements",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "HeureDebut",
                table: "Evenements",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Nom",
                table: "Evenements",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Evenements",
                maxLength: 1000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duree",
                table: "Evenements");

            migrationBuilder.DropColumn(
                name: "HeureDebut",
                table: "Evenements");

            migrationBuilder.AlterColumn<string>(
                name: "Nom",
                table: "Evenements",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Evenements",
                nullable: true);
        }
    }
}
