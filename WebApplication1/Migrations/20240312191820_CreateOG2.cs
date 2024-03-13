using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    public partial class CreateOG2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CodingRegions_Genomes_GenomeId",
                table: "CodingRegions");

            migrationBuilder.AlterColumn<int>(
                name: "GenomeId",
                table: "CodingRegions",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_CodingRegions_Genomes_GenomeId",
                table: "CodingRegions",
                column: "GenomeId",
                principalTable: "Genomes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CodingRegions_Genomes_GenomeId",
                table: "CodingRegions");

            migrationBuilder.AlterColumn<int>(
                name: "GenomeId",
                table: "CodingRegions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CodingRegions_Genomes_GenomeId",
                table: "CodingRegions",
                column: "GenomeId",
                principalTable: "Genomes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
