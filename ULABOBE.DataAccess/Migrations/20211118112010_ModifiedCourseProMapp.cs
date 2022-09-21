using Microsoft.EntityFrameworkCore.Migrations;

namespace ULABOBE.DataAccess.Migrations
{
    public partial class ModifiedCourseProMapp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseProgramMappings_CourseClos_CourseCLOId",
                table: "CourseProgramMappings");

            migrationBuilder.DropColumn(
                name: "CLOSelectedIDNames",
                table: "CourseProgramMappings");

            migrationBuilder.DropColumn(
                name: "CLOSelectedIDs",
                table: "CourseProgramMappings");

            migrationBuilder.RenameColumn(
                name: "CourseCLOId",
                table: "CourseProgramMappings",
                newName: "CourseLearningId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseProgramMappings_CourseCLOId",
                table: "CourseProgramMappings",
                newName: "IX_CourseProgramMappings_CourseLearningId");

            migrationBuilder.AddColumn<int>(
                name: "CourseHistoryId",
                table: "CourseProgramMappings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CourseProgramMappings_CourseHistoryId",
                table: "CourseProgramMappings",
                column: "CourseHistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseProgramMappings_CourseHistories_CourseHistoryId",
                table: "CourseProgramMappings",
                column: "CourseHistoryId",
                principalTable: "CourseHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseProgramMappings_CourseLearnings_CourseLearningId",
                table: "CourseProgramMappings",
                column: "CourseLearningId",
                principalTable: "CourseLearnings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseProgramMappings_CourseHistories_CourseHistoryId",
                table: "CourseProgramMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseProgramMappings_CourseLearnings_CourseLearningId",
                table: "CourseProgramMappings");

            migrationBuilder.DropIndex(
                name: "IX_CourseProgramMappings_CourseHistoryId",
                table: "CourseProgramMappings");

            migrationBuilder.DropColumn(
                name: "CourseHistoryId",
                table: "CourseProgramMappings");

            migrationBuilder.RenameColumn(
                name: "CourseLearningId",
                table: "CourseProgramMappings",
                newName: "CourseCLOId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseProgramMappings_CourseLearningId",
                table: "CourseProgramMappings",
                newName: "IX_CourseProgramMappings_CourseCLOId");

            migrationBuilder.AddColumn<string>(
                name: "CLOSelectedIDNames",
                table: "CourseProgramMappings",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CLOSelectedIDs",
                table: "CourseProgramMappings",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseProgramMappings_CourseClos_CourseCLOId",
                table: "CourseProgramMappings",
                column: "CourseCLOId",
                principalTable: "CourseClos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
