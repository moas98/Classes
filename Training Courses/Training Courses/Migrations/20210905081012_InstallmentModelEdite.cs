using Microsoft.EntityFrameworkCore.Migrations;

namespace Training_Courses.Migrations
{
    public partial class InstallmentModelEdite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Installments_Students_StudentId",
                table: "Installments");

            migrationBuilder.DropColumn(
                name: "Installment_payment",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "gender",
                table: "Students",
                newName: "Gender");

            migrationBuilder.RenameColumn(
                name: "Received_amount",
                table: "Installments",
                newName: "StudentPay");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "Installments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClassCost",
                table: "Installments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ClassId",
                table: "Installments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "InstallmentState",
                table: "Installments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Installments_ClassId",
                table: "Installments",
                column: "ClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_Installments_Classes_ClassId",
                table: "Installments",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "ClassesId",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_Installments_Classes_ClassId",
                table: "Installments");

            migrationBuilder.DropForeignKey(
                name: "FK_Installments_Students_StudentId",
                table: "Installments");

            migrationBuilder.DropIndex(
                name: "IX_Installments_ClassId",
                table: "Installments");

            migrationBuilder.DropColumn(
                name: "ClassCost",
                table: "Installments");

            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "Installments");

            migrationBuilder.DropColumn(
                name: "InstallmentState",
                table: "Installments");

            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "Students",
                newName: "gender");

            migrationBuilder.RenameColumn(
                name: "StudentPay",
                table: "Installments",
                newName: "Received_amount");

            migrationBuilder.AddColumn<int>(
                name: "Installment_payment",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "Installments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Installments_Students_StudentId",
                table: "Installments",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
