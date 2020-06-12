using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend_Seguridad.Migrations.ApplicationSoftwareDb
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Camaras",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: false),
                    URL = table.Column<string>(nullable: false),
                    Estado = table.Column<string>(nullable: false),
                    Latitud = table.Column<double>(nullable: false),
                    Longitud = table.Column<double>(nullable: false),
                    Direccion = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Camaras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Placas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NumeroDePlaca = table.Column<string>(nullable: false),
                    CamaraDetectoraId = table.Column<int>(nullable: true),
                    NombreCamara = table.Column<string>(nullable: true),
                    Direccion = table.Column<string>(nullable: true),
                    Latitud = table.Column<double>(nullable: false),
                    Longitud = table.Column<double>(nullable: false),
                    Codigo = table.Column<string>(nullable: true),
                    Hora = table.Column<string>(nullable: true),
                    Fecha = table.Column<string>(nullable: true),
                    ImagenCarro = table.Column<byte[]>(nullable: true),
                    ImagenPlaca = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Placas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Placas_Camaras_CamaraDetectoraId",
                        column: x => x.CamaraDetectoraId,
                        principalTable: "Camaras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Placas_CamaraDetectoraId",
                table: "Placas",
                column: "CamaraDetectoraId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Placas");

            migrationBuilder.DropTable(
                name: "Camaras");
        }
    }
}
