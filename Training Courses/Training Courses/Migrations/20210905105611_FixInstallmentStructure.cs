using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Training_Courses.Migrations
{
    public partial class FixInstallmentStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Installments_Classes_ClassId",
                table: "Installments");

            migrationBuilder.DropForeignKey(
                name: "FK_Installments_Students_StudentId",
                table: "Installments");

            migrationBuilder.DropIndex(
                name: "IX_Installments_StudentId",
                table: "Installments");

            migrationBuilder.RenameColumn(
                name: "InstallmentState",
                table: "Installments",
                newName: "InstallmentStatus");

            migrationBuilder.AddColumn<int>(
                name: "InstallmentId",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Tuition_Fees",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ClassId",
                table: "Installments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentDate",
                table: "Installments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Students_InstallmentId",
                table: "Students",
                column: "InstallmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Installments_Classes_ClassId",
                table: "Installments",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "ClassesId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Installments_InstallmentId",
                table: "Students",
                column: "InstallmentId",
                principalTable: "Installments",
                principalColumn: "InstallmentsId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Installments_Classes_ClassId",
                table: "Installments");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Installments_InstallmentId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_InstallmentId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "InstallmentId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Tuition_Fees",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "PaymentDate",
                table: "Installments");

            migrationBuilder.RenameColumn(
                name: "InstallmentStatus",
                table: "Installments",
                newName: "InstallmentState");

            migrationBuilder.AlterColumn<int>(
                name: "ClassId",
                table: "Installments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Installments_StudentId",
                table: "Installments",
                column: "StudentId");

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
    }
}
