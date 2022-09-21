using Microsoft.EntityFrameworkCore.Migrations;

namespace ULABOBE.DataAccess.Migrations
{
    public partial class ModifiedCContentCH : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseContents_Courses_CourseId",
                table: "CourseContents");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseContents_Semesters_SemesterId",
                table: "CourseContents");

            migrationBuilder.DropIndex(
                name: "IX_CourseContents_CourseId",
                table: "CourseContents");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "CourseContents");

            migrationBuilder.RenameColumn(
                name: "SemesterId",
                table: "CourseContents",
                newName: "CourseHistoryId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseContents_SemesterId",
                table: "CourseContents",
                newName: "IX_CourseContents_CourseHistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseContents_CourseHistories_CourseHistoryId",
                table: "CourseContents",
                column: "CourseHistoryId",
                principalTable: "CourseHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseContents_CourseHistories_CourseHistoryId",
                table: "CourseContents");

            migrationBuilder.RenameColumn(
                name: "CourseHistoryId",
                table: "CourseContents",
                newName: "SemesterId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseContents_CourseHistoryId",
                table: "CourseContents",
                newName: "IX_CourseContents_SemesterId");

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "CourseContents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CourseContents_CourseId",
                table: "CourseContents",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseContents_Courses_CourseId",
                table: "CourseContents",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseContents_Semesters_SemesterId",
                table: "CourseContents",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
