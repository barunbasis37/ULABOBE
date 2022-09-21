using Microsoft.EntityFrameworkCore.Migrations;

namespace ULABOBE.DataAccess.Migrations
{
    public partial class ModifiedCourseCLOCH : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseClos_Courses_CourseId",
                table: "CourseClos");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseClos_Semesters_SemesterId",
                table: "CourseClos");

            migrationBuilder.DropIndex(
                name: "IX_CourseClos_CourseId",
                table: "CourseClos");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "CourseClos");

            migrationBuilder.RenameColumn(
                name: "SemesterId",
                table: "CourseClos",
                newName: "CourseHistoryId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseClos_SemesterId",
                table: "CourseClos",
                newName: "IX_CourseClos_CourseHistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseClos_CourseHistories_CourseHistoryId",
                table: "CourseClos",
                column: "CourseHistoryId",
                principalTable: "CourseHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseClos_CourseHistories_CourseHistoryId",
                table: "CourseClos");

            migrationBuilder.RenameColumn(
                name: "CourseHistoryId",
                table: "CourseClos",
                newName: "SemesterId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseClos_CourseHistoryId",
                table: "CourseClos",
                newName: "IX_CourseClos_SemesterId");

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "CourseClos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CourseClos_CourseId",
                table: "CourseClos",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseClos_Courses_CourseId",
                table: "CourseClos",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseClos_Semesters_SemesterId",
                table: "CourseClos",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
