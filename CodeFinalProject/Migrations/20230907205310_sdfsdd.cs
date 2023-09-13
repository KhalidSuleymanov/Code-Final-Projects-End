using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeFinalProject.Migrations
{
    public partial class sdfsdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "RestaurantModals",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "RestaurantModals");
        }
    }
}
