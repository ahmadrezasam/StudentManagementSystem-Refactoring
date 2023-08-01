using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class update1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseInstructor_Courses_CoursesCourseId",
                table: "CourseInstructor");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseInstructor_Instructors_InstructorsInstructorId",
                table: "CourseInstructor");

            migrationBuilder.RenameColumn(
                name: "InstructorsInstructorId",
                table: "CourseInstructor",
                newName: "FK_InstructorId");

            migrationBuilder.RenameColumn(
                name: "CoursesCourseId",
                table: "CourseInstructor",
                newName: "FK_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseInstructor_InstructorsInstructorId",
                table: "CourseInstructor",
                newName: "IX_CourseInstructor_FK_InstructorId");

            migrationBuilder.AddColumn<int>(
                name: "Section",
                table: "Courses",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseInstructor_Courses_FK_InstructorId",
                table: "CourseInstructor",
                column: "FK_InstructorId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseInstructor_Instructors_FK_CourseId",
                table: "CourseInstructor",
                column: "FK_CourseId",
                principalTable: "Instructors",
                principalColumn: "InstructorId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseInstructor_Courses_FK_InstructorId",
                table: "CourseInstructor");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseInstructor_Instructors_FK_CourseId",
                table: "CourseInstructor");

            migrationBuilder.DropColumn(
                name: "Section",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "FK_InstructorId",
                table: "CourseInstructor",
                newName: "InstructorsInstructorId");

            migrationBuilder.RenameColumn(
                name: "FK_CourseId",
                table: "CourseInstructor",
                newName: "CoursesCourseId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseInstructor_FK_InstructorId",
                table: "CourseInstructor",
                newName: "IX_CourseInstructor_InstructorsInstructorId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseInstructor_Courses_CoursesCourseId",
                table: "CourseInstructor",
                column: "CoursesCourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseInstructor_Instructors_InstructorsInstructorId",
                table: "CourseInstructor",
                column: "InstructorsInstructorId",
                principalTable: "Instructors",
                principalColumn: "InstructorId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
