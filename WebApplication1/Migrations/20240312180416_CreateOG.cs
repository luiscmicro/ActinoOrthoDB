using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    public partial class CreateOG : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrthoGroupId",
                table: "CodingRegions",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OrthoGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrthoGroup", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CodingRegions_OrthoGroupId",
                table: "CodingRegions",
                column: "OrthoGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_CodingRegions_OrthoGroup_OrthoGroupId",
                table: "CodingRegions",
                column: "OrthoGroupId",
                principalTable: "OrthoGroup",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CodingRegions_OrthoGroup_OrthoGroupId",
                table: "CodingRegions");

            migrationBuilder.DropTable(
                name: "OrthoGroup");

            migrationBuilder.DropIndex(
                name: "IX_CodingRegions_OrthoGroupId",
                table: "CodingRegions");

            migrationBuilder.DropColumn(
                name: "OrthoGroupId",
                table: "CodingRegions");
        }
    }
}
