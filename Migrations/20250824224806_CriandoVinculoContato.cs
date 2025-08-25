using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleContatos.Migrations
{
    /// <inheritdoc />
    public partial class CriandoVinculoContato : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Contato",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contato_UsuarioId",
                table: "Contato",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contato_Usuario_UsuarioId",
                table: "Contato",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contato_Usuario_UsuarioId",
                table: "Contato");

            migrationBuilder.DropIndex(
                name: "IX_Contato_UsuarioId",
                table: "Contato");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Contato");
        }
    }
}
