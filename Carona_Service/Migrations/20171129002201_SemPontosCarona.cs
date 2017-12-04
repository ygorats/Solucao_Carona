using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Carona_Service.Migrations
{
    public partial class SemPontosCarona : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PontoChegada",
                table: "Carona");

            migrationBuilder.DropColumn(
                name: "PontoPartida",
                table: "Carona");

            migrationBuilder.DropColumn(
                name: "PontosIntermediarios",
                table: "Carona");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PontoChegada",
                table: "Carona",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PontoPartida",
                table: "Carona",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PontosIntermediarios",
                table: "Carona",
                nullable: true);
        }
    }
}
