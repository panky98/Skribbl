using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class ime2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KorisnikPoSobi_Korisnici_KorisnikId",
                table: "KorisnikPoSobi");

            migrationBuilder.DropForeignKey(
                name: "FK_KorisnikPoSobi_Soba_SobaId",
                table: "KorisnikPoSobi");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KorisnikPoSobi",
                table: "KorisnikPoSobi");

            migrationBuilder.RenameTable(
                name: "KorisnikPoSobi",
                newName: "KorisniciPoSobi");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Soba",
                newName: "Naziv");

            migrationBuilder.RenameIndex(
                name: "IX_KorisnikPoSobi_SobaId",
                table: "KorisniciPoSobi",
                newName: "IX_KorisniciPoSobi_SobaId");

            migrationBuilder.RenameIndex(
                name: "IX_KorisnikPoSobi_KorisnikId",
                table: "KorisniciPoSobi",
                newName: "IX_KorisniciPoSobi_KorisnikId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KorisniciPoSobi",
                table: "KorisniciPoSobi",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KorisniciPoSobi_Korisnici_KorisnikId",
                table: "KorisniciPoSobi",
                column: "KorisnikId",
                principalTable: "Korisnici",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KorisniciPoSobi_Soba_SobaId",
                table: "KorisniciPoSobi",
                column: "SobaId",
                principalTable: "Soba",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KorisniciPoSobi_Korisnici_KorisnikId",
                table: "KorisniciPoSobi");

            migrationBuilder.DropForeignKey(
                name: "FK_KorisniciPoSobi_Soba_SobaId",
                table: "KorisniciPoSobi");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KorisniciPoSobi",
                table: "KorisniciPoSobi");

            migrationBuilder.RenameTable(
                name: "KorisniciPoSobi",
                newName: "KorisnikPoSobi");

            migrationBuilder.RenameColumn(
                name: "Naziv",
                table: "Soba",
                newName: "Username");

            migrationBuilder.RenameIndex(
                name: "IX_KorisniciPoSobi_SobaId",
                table: "KorisnikPoSobi",
                newName: "IX_KorisnikPoSobi_SobaId");

            migrationBuilder.RenameIndex(
                name: "IX_KorisniciPoSobi_KorisnikId",
                table: "KorisnikPoSobi",
                newName: "IX_KorisnikPoSobi_KorisnikId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KorisnikPoSobi",
                table: "KorisnikPoSobi",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KorisnikPoSobi_Korisnici_KorisnikId",
                table: "KorisnikPoSobi",
                column: "KorisnikId",
                principalTable: "Korisnici",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KorisnikPoSobi_Soba_SobaId",
                table: "KorisnikPoSobi",
                column: "SobaId",
                principalTable: "Soba",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
