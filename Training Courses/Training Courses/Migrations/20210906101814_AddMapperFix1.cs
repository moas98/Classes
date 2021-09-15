using Microsoft.EntityFrameworkCore.Migrations;

namespace Training_Courses.Migrations
{
    public partial class AddMapperFix1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentPayment",
                table: "Students");

            migrationBuilder.AddColumn<bool>(
                name: "InstallmentStatus",
                table: "Students",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstallmentStatus",
                table: "Students");

            migrationBuilder.AddColumn<int>(
                name: "StudentPayment",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
