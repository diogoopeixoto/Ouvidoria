using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OuvidoriaAPI.Migrations
{
    public partial class Anexo3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnexoBase64",
                table: "Manifestacoes");

            migrationBuilder.AddColumn<byte[]>(
                name: "Anexo",
                table: "Manifestacoes",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Anexo",
                table: "Manifestacoes");

            migrationBuilder.AddColumn<string>(
                name: "AnexoBase64",
                table: "Manifestacoes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
