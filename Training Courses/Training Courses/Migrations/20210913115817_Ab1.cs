using Microsoft.EntityFrameworkCore.Migrations;

namespace Training_Courses.Migrations
{
    public partial class Ab1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Absences_Classes_ClassId",
                table: "Absences");

            migrationBuilder.AlterColumn<int>(
                name: "ClassId",
                table: "Absences",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Absences_Classes_ClassId",
                table: "Absences",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "ClassesId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Absences_Classes_ClassId",
                table: "Absences");

            migrationBuilder.AlterColumn<int>(
                name: "ClassId",
                table: "Absences",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Absences_Classes_ClassId",
                table: "Absences",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "ClassesId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
