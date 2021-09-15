using Microsoft.EntityFrameworkCore.Migrations;

namespace Training_Courses.Migrations
{
    public partial class MakeAbsence : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Absences_Classes_ClassesId",
                table: "Absences");

            migrationBuilder.DropForeignKey(
                name: "FK_Absences_Students_StudentId",
                table: "Absences");

            migrationBuilder.DropIndex(
                name: "IX_Absences_ClassesId",
                table: "Absences");

            migrationBuilder.DropColumn(
                name: "ClassesId",
                table: "Absences");

            migrationBuilder.RenameColumn(
                name: "Absence_Date",
                table: "Absences",
                newName: "DateTime");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "Absences",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClassId",
                table: "Absences",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Absences_ClassId",
                table: "Absences",
                column: "ClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_Absences_Classes_ClassId",
                table: "Absences",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "ClassesId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Absences_Students_StudentId",
                table: "Absences",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Absences_Classes_ClassId",
                table: "Absences");

            migrationBuilder.DropForeignKey(
                name: "FK_Absences_Students_StudentId",
                table: "Absences");

            migrationBuilder.DropIndex(
                name: "IX_Absences_ClassId",
                table: "Absences");

            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "Absences");

            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Absences",
                newName: "Absence_Date");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "Absences",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ClassesId",
                table: "Absences",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Absences_ClassesId",
                table: "Absences",
                column: "ClassesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Absences_Classes_ClassesId",
                table: "Absences",
                column: "ClassesId",
                principalTable: "Classes",
                principalColumn: "ClassesId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Absences_Students_StudentId",
                table: "Absences",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
