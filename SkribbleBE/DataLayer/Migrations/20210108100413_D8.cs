using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class D8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reci_TokoviIgre_TokIgreId",
                table: "Reci");

            migrationBuilder.DropIndex(
                name: "IX_TokoviIgre_RecZaPogadjanjeId",
                table: "TokoviIgre");

            migrationBuilder.DropIndex(
                name: "IX_Reci_TokIgreId",
                table: "Reci");

            migrationBuilder.DropColumn(
                name: "TokIgreId",
                table: "Reci");

            migrationBuilder.CreateIndex(
                name: "IX_TokoviIgre_RecZaPogadjanjeId",
                table: "TokoviIgre",
                column: "RecZaPogadjanjeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TokoviIgre_RecZaPogadjanjeId",
                table: "TokoviIgre");

            migrationBuilder.AddColumn<int>(
                name: "TokIgreId",
                table: "Reci",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TokoviIgre_RecZaPogadjanjeId",
                table: "TokoviIgre",
                column: "RecZaPogadjanjeId",
                unique: true,
                filter: "[RecZaPogadjanjeId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Reci_TokIgreId",
                table: "Reci",
                column: "TokIgreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reci_TokoviIgre_TokIgreId",
                table: "Reci",
                column: "TokIgreId",
                principalTable: "TokoviIgre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
