using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class D3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Korisnici",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnici", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TokoviIgre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PocetakIgre = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RecZaPogadjanjeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TokoviIgre", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TokoviIgre_Reci_RecZaPogadjanjeId",
                        column: x => x.RecZaPogadjanjeId,
                        principalTable: "Reci",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Potezi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VremePoteza = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Crtanje = table.Column<bool>(type: "bit", nullable: false),
                    Poruka = table.Column<bool>(type: "bit", nullable: false),
                    TekstPoruke = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BojaLinije = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParametarLinije = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KorisnikId = table.Column<int>(type: "int", nullable: true),
                    TokIgreId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Potezi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Potezi_Korisnici_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Potezi_TokoviIgre_TokIgreId",
                        column: x => x.TokIgreId,
                        principalTable: "TokoviIgre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TokoviIgrePoKorisniku",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnikId = table.Column<int>(type: "int", nullable: true),
                    TokIgreId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TokoviIgrePoKorisniku", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TokoviIgrePoKorisniku_Korisnici_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TokoviIgrePoKorisniku_TokoviIgre_TokIgreId",
                        column: x => x.TokIgreId,
                        principalTable: "TokoviIgre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Potezi_KorisnikId",
                table: "Potezi",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Potezi_TokIgreId",
                table: "Potezi",
                column: "TokIgreId");

            migrationBuilder.CreateIndex(
                name: "IX_TokoviIgre_RecZaPogadjanjeId",
                table: "TokoviIgre",
                column: "RecZaPogadjanjeId");

            migrationBuilder.CreateIndex(
                name: "IX_TokoviIgrePoKorisniku_KorisnikId",
                table: "TokoviIgrePoKorisniku",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_TokoviIgrePoKorisniku_TokIgreId",
                table: "TokoviIgrePoKorisniku",
                column: "TokIgreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Potezi");

            migrationBuilder.DropTable(
                name: "TokoviIgrePoKorisniku");

            migrationBuilder.DropTable(
                name: "Korisnici");

            migrationBuilder.DropTable(
                name: "TokoviIgre");
        }
    }
}
