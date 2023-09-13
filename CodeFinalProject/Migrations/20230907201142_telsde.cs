using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeFinalProject.Migrations
{
    public partial class telsde : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SimilarImage1",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "SimilarImage2",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "SimilarImage3",
                table: "Places");
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SimilarImage1",
                table: "Places",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SimilarImage2",
                table: "Places",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SimilarImage3",
                table: "Places",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
