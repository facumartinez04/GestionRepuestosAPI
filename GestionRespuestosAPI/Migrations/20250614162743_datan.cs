using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionRepuestosAPI.Migrations
{
    /// <inheritdoc />
    public partial class datan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosPermisos_Usuarios_UsuarioId",
                table: "UsuariosPermisos");

            migrationBuilder.DropIndex(
                name: "IX_UsuariosPermisos_UsuarioId",
                table: "UsuariosPermisos");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "UsuariosPermisos");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosPermisos_Usuarios_idUsuario",
                table: "UsuariosPermisos",
                column: "idUsuario",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosPermisos_Usuarios_idUsuario",
                table: "UsuariosPermisos");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "UsuariosPermisos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosPermisos_UsuarioId",
                table: "UsuariosPermisos",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosPermisos_Usuarios_UsuarioId",
                table: "UsuariosPermisos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }
    }
}
