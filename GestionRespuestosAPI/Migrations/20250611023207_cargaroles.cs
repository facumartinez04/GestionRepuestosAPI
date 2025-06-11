using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionRespuestosAPI.Migrations
{
    /// <inheritdoc />
    public partial class cargaroles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProveedorId",
                table: "Repuestos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Repuestos_ProveedorId",
                table: "Repuestos",
                column: "ProveedorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Repuestos_Proveedores_ProveedorId",
                table: "Repuestos",
                column: "ProveedorId",
                principalTable: "Proveedores",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Repuestos_Proveedores_ProveedorId",
                table: "Repuestos");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.DropIndex(
                name: "IX_Repuestos_ProveedorId",
                table: "Repuestos");

            migrationBuilder.DropColumn(
                name: "ProveedorId",
                table: "Repuestos");
        }
    }
}
