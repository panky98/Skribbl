using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class ime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReciPoKategorijama_Kategorije_KategorijaId",
                table: "ReciPoKategorijama");

            migrationBuilder.AddColumn<int>(
                name: "SobaId",
                table: "TokoviIgre",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "KategorijaId",
                table: "ReciPoKategorijama",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Soba",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    KategorijaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Soba", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Soba_Kategorije_KategorijaId",
                        column: x => x.KategorijaId,
                        principalTable: "Kategorije",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KorisnikPoSobi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SobaId = table.Column<int>(type: "int", nullable: false),
                    KorisnikId = table.Column<int>(type: "int", nullable: true),
                    Poeni = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KorisnikPoSobi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KorisnikPoSobi_Korisnici_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KorisnikPoSobi_Soba_SobaId",
                        column: x => x.SobaId,
                        principalTable: "Soba",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TokoviIgre_SobaId",
                table: "TokoviIgre",
                column: "SobaId");

            migrationBuilder.CreateIndex(
                name: "IX_KorisnikPoSobi_KorisnikId",
                table: "KorisnikPoSobi",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_KorisnikPoSobi_SobaId",
                table: "KorisnikPoSobi",
                column: "SobaId");

            migrationBuilder.CreateIndex(
                name: "IX_Soba_KategorijaId",
                table: "Soba",
                column: "KategorijaId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReciPoKategorijama_Kategorije_KategorijaId",
                table: "ReciPoKategorijama",
                column: "KategorijaId",
                principalTable: "Kategorije",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TokoviIgre_Soba_SobaId",
                table: "TokoviIgre",
                column: "SobaId",
                principalTable: "Soba",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReciPoKategorijama_Kategorije_KategorijaId",
                table: "ReciPoKategorijama");

            migrationBuilder.DropForeignKey(
                name: "FK_TokoviIgre_Soba_SobaId",
                table: "TokoviIgre");

            migrationBuilder.DropTable(
                name: "KorisnikPoSobi");

            migrationBuilder.DropTable(
                name: "Soba");

            migrationBuilder.DropIndex(
                name: "IX_TokoviIgre_SobaId",
                table: "TokoviIgre");

            migrationBuilder.DropColumn(
                name: "SobaId",
                table: "TokoviIgre");

            migrationBuilder.AlterColumn<int>(
                name: "KategorijaId",
                table: "ReciPoKategorijama",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ReciPoKategorijama_Kategorije_KategorijaId",
                table: "ReciPoKategorijama",
                column: "KategorijaId",
                principalTable: "Kategorije",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
