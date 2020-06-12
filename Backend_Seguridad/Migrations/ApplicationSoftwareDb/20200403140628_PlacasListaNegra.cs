using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend_Seguridad.Migrations.ApplicationSoftwareDb
{
    public partial class PlacasListaNegra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Placa_Camaras_CamaraDetectoraId",
                table: "Placa");

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

            migrationBuilder.CreateTable(
                name: "PlacasListaNegra",
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
                    table.PrimaryKey("PK_PlacasListaNegra", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlacasListaNegra_Camaras_CamaraDetectoraId",
                        column: x => x.CamaraDetectoraId,
                        principalTable: "Camaras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlacasListaNegra_CamaraDetectoraId",
                table: "PlacasListaNegra",
                column: "CamaraDetectoraId");

            migrationBuilder.AddForeignKey(
                name: "FK_Placas_Camaras_CamaraDetectoraId",
                table: "Placas",
                column: "CamaraDetectoraId",
                principalTable: "Camaras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Placas_Camaras_CamaraDetectoraId",
                table: "Placas");

            migrationBuilder.DropTable(
                name: "PlacasListaNegra");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Placa_Camaras_CamaraDetectoraId",
                table: "Placa",
                column: "CamaraDetectoraId",
                principalTable: "Camaras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
