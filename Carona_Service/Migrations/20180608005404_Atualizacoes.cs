using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Carona_Service.Migrations
{
    public partial class Atualizacoes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EmailVerificado",
                table: "Usuario",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "FamilyName",
                table: "Usuario",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Usuario",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GivenName",
                table: "Usuario",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GoogleId",
                table: "Usuario",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "Usuario",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UrlFoto",
                table: "Usuario",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailVerificado",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "FamilyName",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "GivenName",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "GoogleId",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Link",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "UrlFoto",
                table: "Usuario");
        }
    }
}
