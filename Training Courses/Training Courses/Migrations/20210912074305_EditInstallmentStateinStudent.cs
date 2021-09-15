using Microsoft.EntityFrameworkCore.Migrations;

namespace Training_Courses.Migrations
{
    public partial class EditInstallmentStateinStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CombeleteInstallment",
                table: "Annual_Evaluations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "Annual_Evaluations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InstallmentsId",
                table: "Annual_Evaluations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentName",
                table: "Annual_Evaluations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Annual_Evaluations_InstallmentsId",
                table: "Annual_Evaluations",
                column: "InstallmentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Annual_Evaluations_Installments_InstallmentsId",
                table: "Annual_Evaluations",
                column: "InstallmentsId",
                principalTable: "Installments",
                principalColumn: "InstallmentsId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Annual_Evaluations_Installments_InstallmentsId",
                table: "Annual_Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Annual_Evaluations_InstallmentsId",
                table: "Annual_Evaluations");

            migrationBuilder.DropColumn(
                name: "CombeleteInstallment",
                table: "Annual_Evaluations");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Annual_Evaluations");

            migrationBuilder.DropColumn(
                name: "InstallmentsId",
                table: "Annual_Evaluations");

            migrationBuilder.DropColumn(
                name: "StudentName",
                table: "Annual_Evaluations");
        }
    }
}
