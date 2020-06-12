using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend_Seguridad.Migrations.ApplicationSoftwareDb
{
    public partial class ListaNegra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Placas_Camaras_CamaraDetectoraId",
                table: "Placas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Placas",
                table: "Placas");

            migrationBuilder.RenameTable(
                name: "Placas",
                newName: "Placa");

            migrationBuilder.RenameIndex(
                name: "IX_Placas_CamaraDetectoraId",
                table: "Placa",
                newName: "IX_Placa_CamaraDetectoraId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Placa",
                table: "Placa",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Personas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombres = table.Column<string>(nullable: true),
                    ApellidoPaterno = table.Column<string>(nullable: true),
                    ApellidoMaterno = table.Column<string>(nullable: true),
                    Direccion = table.Column<string>(nullable: true),
                    FechaDeNacimiento = table.Column<string>(nullable: true),
                    NumeroDeCelular = table.Column<string>(nullable: true),
                    DNI = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ListaNegra",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NumeroDePlaca = table.Column<string>(nullable: false),
                    Estado = table.Column<string>(nullable: true),
                    PersonaId = table.Column<int>(nullable: true),
                    DNI = table.Column<string>(nullable: true),
                    Nombres = table.Column<string>(nullable: true),
                    ApellidoPaterno = table.Column<string>(nullable: true),
                    ApellidoMaterno = table.Column<string>(nullable: true),
                    Direccion = table.Column<string>(nullable: true),
                    FechaDeNacimiento = table.Column<string>(nullable: true),
                    NumeroDeCelular = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListaNegra", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListaNegra_Personas_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ListaNegra_PersonaId",
                table: "ListaNegra",
                column: "PersonaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Placa_Camaras_CamaraDetectoraId",
                table: "Placa",
                column: "CamaraDetectoraId",
                principalTable: "Camaras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Placa_Camaras_CamaraDetectoraId",
                table: "Placa");

            migrationBuilder.DropTable(
                name: "ListaNegra");

            migrationBuilder.DropTable(
                name: "Personas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Placa",
                table: "Placa");

            migrationBuilder.RenameTable(
                name: "Placa",
                newName: "Placas");

            migrationBuilder.RenameIndex(
                name: "IX_Placa_CamaraDetectoraId",
                table: "Placas",
                newName: "IX_Placas_CamaraDetectoraId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Placas",
                table: "Placas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Placas_Camaras_CamaraDetectoraId",
                table: "Placas",
                column: "CamaraDetectoraId",
                principalTable: "Camaras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
