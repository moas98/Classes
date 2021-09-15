using Microsoft.EntityFrameworkCore.Migrations;

namespace Training_Courses.Migrations
{
    public partial class imges1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Images_ImagesId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_ImagesId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Images_StudentId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ImagesId",
                table: "Students");

            migrationBuilder.CreateIndex(
                name: "IX_Images_StudentId",
                table: "Images",
                column: "StudentId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Images_StudentId",
                table: "Images");

            migrationBuilder.AddColumn<int>(
                name: "ImagesId",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_ImagesId",
                table: "Students",
                column: "ImagesId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_StudentId",
                table: "Images",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Images_ImagesId",
                table: "Students",
                column: "ImagesId",
                principalTable: "Images",
                principalColumn: "ImagesId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
