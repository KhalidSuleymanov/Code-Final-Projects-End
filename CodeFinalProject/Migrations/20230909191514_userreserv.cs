using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeFinalProject.Migrations
{
    public partial class userreserv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Reservation",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_AppUserId",
                table: "Reservation",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_AspNetUsers_AppUserId",
                table: "Reservation",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_AspNetUsers_AppUserId",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_AppUserId",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Reservation");
        }
    }
}
