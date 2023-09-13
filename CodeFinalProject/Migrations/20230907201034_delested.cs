using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeFinalProject.Migrations
{
    public partial class delested : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SimilarImage1",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "SimilarImage2",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "SimilarImage3",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "SimilarImage4",
                table: "Blogs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SimilarImage1",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SimilarImage2",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SimilarImage3",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SimilarImage4",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
