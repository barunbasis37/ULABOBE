using Microsoft.EntityFrameworkCore.Migrations;

namespace ULABOBE.DataAccess.Migrations
{
    public partial class ModififyMappingCPCLearning : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MappingCourseProgramLos_CourseClos_CourseCLOId",
                table: "MappingCourseProgramLos");

            migrationBuilder.RenameColumn(
                name: "CourseCLOId",
                table: "MappingCourseProgramLos",
                newName: "CourseLearningId");

            migrationBuilder.RenameIndex(
                name: "IX_MappingCourseProgramLos_CourseCLOId",
                table: "MappingCourseProgramLos",
                newName: "IX_MappingCourseProgramLos_CourseLearningId");

            migrationBuilder.AddColumn<int>(
                name: "CourseHistoryId",
                table: "MappingCourseProgramLos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MappingCourseProgramLos_CourseHistoryId",
                table: "MappingCourseProgramLos",
                column: "CourseHistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_MappingCourseProgramLos_CourseHistories_CourseHistoryId",
                table: "MappingCourseProgramLos",
                column: "CourseHistoryId",
                principalTable: "CourseHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MappingCourseProgramLos_CourseLearnings_CourseLearningId",
                table: "MappingCourseProgramLos",
                column: "CourseLearningId",
                principalTable: "CourseLearnings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MappingCourseProgramLos_CourseHistories_CourseHistoryId",
                table: "MappingCourseProgramLos");

            migrationBuilder.DropForeignKey(
                name: "FK_MappingCourseProgramLos_CourseLearnings_CourseLearningId",
                table: "MappingCourseProgramLos");

            migrationBuilder.DropIndex(
                name: "IX_MappingCourseProgramLos_CourseHistoryId",
                table: "MappingCourseProgramLos");

            migrationBuilder.DropColumn(
                name: "CourseHistoryId",
                table: "MappingCourseProgramLos");

            migrationBuilder.RenameColumn(
                name: "CourseLearningId",
                table: "MappingCourseProgramLos",
                newName: "CourseCLOId");

            migrationBuilder.RenameIndex(
                name: "IX_MappingCourseProgramLos_CourseLearningId",
                table: "MappingCourseProgramLos",
                newName: "IX_MappingCourseProgramLos_CourseCLOId");

            migrationBuilder.AddForeignKey(
                name: "FK_MappingCourseProgramLos_CourseClos_CourseCLOId",
                table: "MappingCourseProgramLos",
                column: "CourseCLOId",
                principalTable: "CourseClos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
