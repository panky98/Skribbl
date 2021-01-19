using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kategorije",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategorije", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reci",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reci", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReciPoKategorijama",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KategorijaId = table.Column<int>(type: "int", nullable: false),
                    RecId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReciPoKategorijama", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReciPoKategorijama_Kategorije_KategorijaId",
                        column: x => x.KategorijaId,
                        principalTable: "Kategorije",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReciPoKategorijama_Reci_RecId",
                        column: x => x.RecId,
                        principalTable: "Reci",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReciPoKategorijama_KategorijaId",
                table: "ReciPoKategorijama",
                column: "KategorijaId");

            migrationBuilder.CreateIndex(
                name: "IX_ReciPoKategorijama_RecId",
                table: "ReciPoKategorijama",
                column: "RecId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReciPoKategorijama");

            migrationBuilder.DropTable(
                name: "Kategorije");

            migrationBuilder.DropTable(
                name: "Reci");
        }
    }
}
