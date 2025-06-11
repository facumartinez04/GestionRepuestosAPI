using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionRespuestosAPI.Migrations
{
    /// <inheritdoc />
    public partial class cargarolespermisos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsuariosPermisos",
                columns: table => new
                {
                    idUsuario = table.Column<int>(type: "int", nullable: false),
                    idPermiso = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosPermisos", x => new { x.idUsuario, x.idPermiso });
                });

            migrationBuilder.CreateTable(
                name: "UsuariosRoles",
                columns: table => new
                {
                    idUsuario = table.Column<int>(type: "int", nullable: false),
                    idRol = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosRoles", x => new { x.idUsuario, x.idRol });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuariosPermisos");

            migrationBuilder.DropTable(
                name: "UsuariosRoles");
        }
    }
}
