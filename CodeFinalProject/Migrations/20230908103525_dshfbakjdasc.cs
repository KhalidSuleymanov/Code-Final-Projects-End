using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeFinalProject.Migrations
{
    public partial class dshfbakjdasc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Rate",
                table: "Rooms",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rate",
                table: "Rooms");
        }
    }
}
