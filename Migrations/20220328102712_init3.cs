using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentManagementSystem2.Migrations
{
    public partial class init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Passport",
                table: "Students",
                newName: "ByteArrayofPassport");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ByteArrayofPassport",
                table: "Students",
                newName: "Passport");
        }
    }
}
