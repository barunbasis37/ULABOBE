using Microsoft.EntityFrameworkCore.Migrations;

namespace ULABOBE.DataAccess.Migrations
{
    public partial class ModifiedMappingCLOPLOCH : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MappingCourseProgramLos_Courses_CourseId",
                table: "MappingCourseProgramLos");

            migrationBuilder.DropForeignKey(
                name: "FK_MappingCourseProgramLos_Semesters_SemesterId",
                table: "MappingCourseProgramLos");

            migrationBuilder.DropIndex(
                name: "IX_MappingCourseProgramLos_CourseId",
                table: "MappingCourseProgramLos");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "MappingCourseProgramLos");

            migrationBuilder.RenameColumn(
                name: "SemesterId",
                table: "MappingCourseProgramLos",
                newName: "CourseHistoryId");

            migrationBuilder.RenameIndex(
                name: "IX_MappingCourseProgramLos_SemesterId",
                table: "MappingCourseProgramLos",
                newName: "IX_MappingCourseProgramLos_CourseHistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_MappingCourseProgramLos_CourseHistories_CourseHistoryId",
                table: "MappingCourseProgramLos",
                column: "CourseHistoryId",
                principalTable: "CourseHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MappingCourseProgramLos_CourseHistories_CourseHistoryId",
                table: "MappingCourseProgramLos");

            migrationBuilder.RenameColumn(
                name: "CourseHistoryId",
                table: "MappingCourseProgramLos",
                newName: "SemesterId");

            migrationBuilder.RenameIndex(
                name: "IX_MappingCourseProgramLos_CourseHistoryId",
                table: "MappingCourseProgramLos",
                newName: "IX_MappingCourseProgramLos_SemesterId");

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "MappingCourseProgramLos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MappingCourseProgramLos_CourseId",
                table: "MappingCourseProgramLos",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_MappingCourseProgramLos_Courses_CourseId",
                table: "MappingCourseProgramLos",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MappingCourseProgramLos_Semesters_SemesterId",
                table: "MappingCourseProgramLos",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
