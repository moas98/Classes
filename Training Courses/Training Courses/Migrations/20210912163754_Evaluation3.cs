using Microsoft.EntityFrameworkCore.Migrations;

namespace Training_Courses.Migrations
{
    public partial class Evaluation3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TheFinalGrade",
                table: "Students",
                newName: "Mark");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Mark",
                table: "Students",
                newName: "TheFinalGrade");
        }
    }
}
