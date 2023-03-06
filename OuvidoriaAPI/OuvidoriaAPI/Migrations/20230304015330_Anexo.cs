using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OuvidoriaAPI.Migrations
{
    public partial class Anexo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AnexoBase64",
                table: "Manifestacoes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnexoBase64",
                table: "Manifestacoes");
        }
    }
}
