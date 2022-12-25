using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Credenciais.Infra.Dados.Migrations
{
    /// <inheritdoc />
    public partial class CriandoEstruturaInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Login = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Senha = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Credenciais",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Titulo = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Login = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Senha = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Assinatura = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Url = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Agencia = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Conta = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Pix = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    UsuarioAplicativo = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    SenhaAplicativo = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    SenhaCartao = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    Observacao = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    IdUsuario = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credenciais", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Credenciais_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Credenciais_IdUsuario",
                table: "Credenciais",
                column: "IdUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Credenciais");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
