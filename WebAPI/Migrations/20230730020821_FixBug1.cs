using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class FixBug1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseStudent_Courses_StudentId",
                table: "CourseStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseStudent_Students_CourseId",
                table: "CourseStudent");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "CourseStudent",
                newName: "FK_StudentId");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "CourseStudent",
                newName: "FK_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseStudent_StudentId",
                table: "CourseStudent",
                newName: "IX_CourseStudent_FK_StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseStudent_Courses_FK_StudentId",
                table: "CourseStudent",
                column: "FK_StudentId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseStudent_Students_FK_CourseId",
                table: "CourseStudent",
                column: "FK_CourseId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseStudent_Courses_FK_StudentId",
                table: "CourseStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseStudent_Students_FK_CourseId",
                table: "CourseStudent");

            migrationBuilder.RenameColumn(
                name: "FK_StudentId",
                table: "CourseStudent",
                newName: "StudentId");

            migrationBuilder.RenameColumn(
                name: "FK_CourseId",
                table: "CourseStudent",
                newName: "CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseStudent_FK_StudentId",
                table: "CourseStudent",
                newName: "IX_CourseStudent_StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseStudent_Courses_StudentId",
                table: "CourseStudent",
                column: "StudentId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseStudent_Students_CourseId",
                table: "CourseStudent",
                column: "CourseId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
