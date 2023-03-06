using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OuvidoriaAPI.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ouvidores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ouvidores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Setores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Inativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Respostas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Texto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinksUteis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Anexo = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    OuvidorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Respostas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Respostas_Ouvidores_OuvidorId",
                        column: x => x.OuvidorId,
                        principalTable: "Ouvidores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Manifestacoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataVisualizacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Perfil = table.Column<int>(type: "int", maxLength: 60, nullable: false),
                    TipoSolicitacao = table.Column<int>(type: "int", maxLength: 60, nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Celular = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Campus = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Curso = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Assunto = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Conteudo = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    SetorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RespostaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VisOuvidorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manifestacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Manifestacoes_Ouvidores_VisOuvidorId",
                        column: x => x.VisOuvidorId,
                        principalTable: "Ouvidores",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Manifestacoes_Respostas_RespostaId",
                        column: x => x.RespostaId,
                        principalTable: "Respostas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Manifestacoes_Setores_SetorId",
                        column: x => x.SetorId,
                        principalTable: "Setores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Manifestacoes_RespostaId",
                table: "Manifestacoes",
                column: "RespostaId");

            migrationBuilder.CreateIndex(
                name: "IX_Manifestacoes_SetorId",
                table: "Manifestacoes",
                column: "SetorId");

            migrationBuilder.CreateIndex(
                name: "IX_Manifestacoes_VisOuvidorId",
                table: "Manifestacoes",
                column: "VisOuvidorId");

            migrationBuilder.CreateIndex(
                name: "IX_Respostas_OuvidorId",
                table: "Respostas",
                column: "OuvidorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Manifestacoes");

            migrationBuilder.DropTable(
                name: "Respostas");

            migrationBuilder.DropTable(
                name: "Setores");

            migrationBuilder.DropTable(
                name: "Ouvidores");
        }
    }
}
