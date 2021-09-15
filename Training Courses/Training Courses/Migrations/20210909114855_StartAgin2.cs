using Microsoft.EntityFrameworkCore.Migrations;

namespace Training_Courses.Migrations
{
    public partial class StartAgin2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClassesId",
                table: "Annual_Evaluations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StudentsId",
                table: "Annual_Evaluations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Annual_Evaluations_ClassesId",
                table: "Annual_Evaluations",
                column: "ClassesId");

            migrationBuilder.CreateIndex(
                name: "IX_Annual_Evaluations_StudentsId",
                table: "Annual_Evaluations",
                column: "StudentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Annual_Evaluations_Classes_ClassesId",
                table: "Annual_Evaluations",
                column: "ClassesId",
                principalTable: "Classes",
                principalColumn: "ClassesId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Annual_Evaluations_Students_StudentsId",
                table: "Annual_Evaluations",
                column: "StudentsId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Annual_Evaluations_Classes_ClassesId",
                table: "Annual_Evaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Annual_Evaluations_Students_StudentsId",
                table: "Annual_Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Annual_Evaluations_ClassesId",
                table: "Annual_Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Annual_Evaluations_StudentsId",
                table: "Annual_Evaluations");

            migrationBuilder.DropColumn(
                name: "ClassesId",
                table: "Annual_Evaluations");

            migrationBuilder.DropColumn(
                name: "StudentsId",
                table: "Annual_Evaluations");
        }
    }
}
