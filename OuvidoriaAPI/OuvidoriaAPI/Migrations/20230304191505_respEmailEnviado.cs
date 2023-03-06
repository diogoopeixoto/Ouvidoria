using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OuvidoriaAPI.Migrations
{
    public partial class respEmailEnviado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EmailEnviado",
                table: "Respostas",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailEnviado",
                table: "Respostas");
        }
    }
}
