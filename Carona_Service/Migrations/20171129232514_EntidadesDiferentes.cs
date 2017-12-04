using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Carona_Service.Migrations
{
    public partial class EntidadesDiferentes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Carona");

            migrationBuilder.CreateTable(
                name: "CaronaBusca",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    HorarioChegada = table.Column<DateTime>(nullable: false),
                    HorarioPartida = table.Column<DateTime>(nullable: false),
                    IdUsuario = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaronaBusca", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CaronaOferta",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    HorarioChegada = table.Column<DateTime>(nullable: false),
                    HorarioPartida = table.Column<DateTime>(nullable: false),
                    IdUsuario = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaronaOferta", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CaronaBusca");

            migrationBuilder.DropTable(
                name: "CaronaOferta");

            migrationBuilder.CreateTable(
                name: "Carona",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    HorarioChegada = table.Column<DateTime>(nullable: false),
                    IdUsuario = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carona", x => x.Id);
                });
        }
    }
}
