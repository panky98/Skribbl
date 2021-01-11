using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class D1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReciPoKategorijama_Kategorije_KategorijaId",
                table: "ReciPoKategorijama");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReciPoKategorijama_Kategorije_KategorijaId",
                table: "ReciPoKategorijama");

            migrationBuilder.AlterColumn<int>(
                name: "KategorijaId",
                table: "ReciPoKategorijama",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ReciPoKategorijama_Kategorije_KategorijaId",
                table: "ReciPoKategorijama",
                column: "KategorijaId",
                principalTable: "Kategorije",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
