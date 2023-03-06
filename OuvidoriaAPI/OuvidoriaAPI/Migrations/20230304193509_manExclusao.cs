using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OuvidoriaAPI.Migrations
{
    public partial class manExclusao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataExclusao",
                table: "Manifestacoes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Excluido",
                table: "Manifestacoes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "OuvidorIdExclusao",
                table: "Manifestacoes",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataExclusao",
                table: "Manifestacoes");

            migrationBuilder.DropColumn(
                name: "Excluido",
                table: "Manifestacoes");

            migrationBuilder.DropColumn(
                name: "OuvidorIdExclusao",
                table: "Manifestacoes");
        }
    }
}
