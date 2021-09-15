using Microsoft.EntityFrameworkCore.Migrations;

namespace Training_Courses.Migrations
{
    public partial class ChangeModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Students_StudentsStudentId",
                table: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Classes_StudentsStudentId",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "StudentsStudentId",
                table: "Classes");

            migrationBuilder.AddColumn<int>(
                name: "ClassesId",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_ClassesId",
                table: "Students",
                column: "ClassesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Classes_ClassesId",
                table: "Students",
                column: "ClassesId",
                principalTable: "Classes",
                principalColumn: "ClassesId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Classes_ClassesId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_ClassesId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ClassesId",
                table: "Students");

            migrationBuilder.AddColumn<int>(
                name: "StudentsStudentId",
                table: "Classes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Classes_StudentsStudentId",
                table: "Classes",
                column: "StudentsStudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Students_StudentsStudentId",
                table: "Classes",
                column: "StudentsStudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
