using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeFinalProject.Migrations
{
    public partial class datsdedabegdsakjBcnjaskd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserContacts_AspNetUsers_AppUserId",
                table: "UserContacts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserContacts",
                table: "UserContacts");

            migrationBuilder.DropColumn(
                name: "Subject",
                table: "UserContacts");

            migrationBuilder.RenameTable(
                name: "UserContacts",
                newName: "UserContact");

            migrationBuilder.RenameIndex(
                name: "IX_UserContacts_AppUserId",
                table: "UserContact",
                newName: "IX_UserContact_AppUserId");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "UserContact",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(80)",
                oldMaxLength: 80);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserContact",
                table: "UserContact",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Sliders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order = table.Column<int>(type: "int", nullable: false),
                    FirstTitle = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    SecondTitle = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ButtonText = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    ButtonUrl = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sliders", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_UserContact_AspNetUsers_AppUserId",
                table: "UserContact",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserContact_AspNetUsers_AppUserId",
                table: "UserContact");

            migrationBuilder.DropTable(
                name: "Sliders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserContact",
                table: "UserContact");

            migrationBuilder.RenameTable(
                name: "UserContact",
                newName: "UserContacts");

            migrationBuilder.RenameIndex(
                name: "IX_UserContact_AppUserId",
                table: "UserContacts",
                newName: "IX_UserContacts_AppUserId");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "UserContacts",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "UserContacts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserContacts",
                table: "UserContacts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserContacts_AspNetUsers_AppUserId",
                table: "UserContacts",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
