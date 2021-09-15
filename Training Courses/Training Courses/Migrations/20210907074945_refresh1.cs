using Microsoft.EntityFrameworkCore.Migrations;

namespace Training_Courses.Migrations
{
    public partial class refresh1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstallmentsStudents");

            migrationBuilder.CreateIndex(
                name: "IX_Installments_StudentId",
                table: "Installments",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Installments_Students_StudentId",
                table: "Installments",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Installments_Students_StudentId",
                table: "Installments");

            migrationBuilder.DropIndex(
                name: "IX_Installments_StudentId",
                table: "Installments");

            migrationBuilder.CreateTable(
                name: "InstallmentsStudents",
                columns: table => new
                {
                    InstallmentsId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstallmentsStudents", x => new { x.InstallmentsId, x.StudentId });
                    table.ForeignKey(
                        name: "FK_InstallmentsStudents_Installments_InstallmentsId",
                        column: x => x.InstallmentsId,
                        principalTable: "Installments",
                        principalColumn: "InstallmentsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstallmentsStudents_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InstallmentsStudents_StudentId",
                table: "InstallmentsStudents",
                column: "StudentId");
        }
    }
}
