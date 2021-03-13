using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class ModifikacijaPoteza1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Potezi_Korisnici_KorisnikId",
                table: "Potezi");

            migrationBuilder.DropForeignKey(
                name: "FK_Potezi_TokoviIgre_TokIgreId",
                table: "Potezi");

            migrationBuilder.DropColumn(
                name: "VremePoteza",
                table: "Potezi");

            migrationBuilder.AlterColumn<int>(
                name: "TokIgreId",
                table: "Potezi",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "KorisnikId",
                table: "Potezi",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Potezi_Korisnici_KorisnikId",
                table: "Potezi",
                column: "KorisnikId",
                principalTable: "Korisnici",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Potezi_TokoviIgre_TokIgreId",
                table: "Potezi",
                column: "TokIgreId",
                principalTable: "TokoviIgre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Potezi_Korisnici_KorisnikId",
                table: "Potezi");

            migrationBuilder.DropForeignKey(
                name: "FK_Potezi_TokoviIgre_TokIgreId",
                table: "Potezi");

            migrationBuilder.AlterColumn<int>(
                name: "TokIgreId",
                table: "Potezi",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "KorisnikId",
                table: "Potezi",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "VremePoteza",
                table: "Potezi",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Potezi_Korisnici_KorisnikId",
                table: "Potezi",
                column: "KorisnikId",
                principalTable: "Korisnici",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Potezi_TokoviIgre_TokIgreId",
                table: "Potezi",
                column: "TokIgreId",
                principalTable: "TokoviIgre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
