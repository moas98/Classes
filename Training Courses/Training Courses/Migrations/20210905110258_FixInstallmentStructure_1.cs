using Microsoft.EntityFrameworkCore.Migrations;

namespace Training_Courses.Migrations
{
    public partial class FixInstallmentStructure_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Installments_InstallmentId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_InstallmentId",
                table: "Students");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstallmentsStudents");

            migrationBuilder.CreateIndex(
                name: "IX_Students_InstallmentId",
                table: "Students",
                column: "InstallmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Installments_InstallmentId",
                table: "Students",
                column: "InstallmentId",
                principalTable: "Installments",
                principalColumn: "InstallmentsId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
