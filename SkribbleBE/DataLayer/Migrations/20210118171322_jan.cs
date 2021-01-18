using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class jan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KorisniciPoSobi_Korisnici_KorisnikId",
                table: "KorisniciPoSobi");

            migrationBuilder.DropForeignKey(
                name: "FK_Soba_Kategorije_KategorijaId",
                table: "Soba");

            migrationBuilder.AlterColumn<int>(
                name: "KategorijaId",
                table: "Soba",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "KorisnikId",
                table: "KorisniciPoSobi",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_KorisniciPoSobi_Korisnici_KorisnikId",
                table: "KorisniciPoSobi",
                column: "KorisnikId",
                principalTable: "Korisnici",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Soba_Kategorije_KategorijaId",
                table: "Soba",
                column: "KategorijaId",
                principalTable: "Kategorije",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KorisniciPoSobi_Korisnici_KorisnikId",
                table: "KorisniciPoSobi");

            migrationBuilder.DropForeignKey(
                name: "FK_Soba_Kategorije_KategorijaId",
                table: "Soba");

            migrationBuilder.AlterColumn<int>(
                name: "KategorijaId",
                table: "Soba",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "KorisnikId",
                table: "KorisniciPoSobi",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_KorisniciPoSobi_Korisnici_KorisnikId",
                table: "KorisniciPoSobi",
                column: "KorisnikId",
                principalTable: "Korisnici",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Soba_Kategorije_KategorijaId",
                table: "Soba",
                column: "KategorijaId",
                principalTable: "Kategorije",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
