using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OuvidoriaAPI.Migrations
{
    public partial class manRefact3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LinksUteis",
                table: "Respostas",
                newName: "EmailSetorEncaminhar");

            migrationBuilder.AddColumn<int>(
                name: "Acao",
                table: "Respostas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "SetorIdEncaminhar",
                table: "Respostas",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Acao",
                table: "Respostas");

            migrationBuilder.DropColumn(
                name: "SetorIdEncaminhar",
                table: "Respostas");

            migrationBuilder.RenameColumn(
                name: "EmailSetorEncaminhar",
                table: "Respostas",
                newName: "LinksUteis");
        }
    }
}
