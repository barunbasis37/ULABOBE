using Microsoft.EntityFrameworkCore.Migrations;

namespace ULABOBE.DataAccess.Migrations
{
    public partial class ModifiedCoursHistorySem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseHistories_LevelTerms_LevelTermId",
                table: "CourseHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseHistories_SessionYears_SessionYearId",
                table: "CourseHistories");

            migrationBuilder.DropIndex(
                name: "IX_CourseHistories_LevelTermId",
                table: "CourseHistories");

            migrationBuilder.DropColumn(
                name: "LevelTermId",
                table: "CourseHistories");

            migrationBuilder.RenameColumn(
                name: "SessionYearId",
                table: "CourseHistories",
                newName: "SemesterId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseHistories_SessionYearId",
                table: "CourseHistories",
                newName: "IX_CourseHistories_SemesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseHistories_Semesters_SemesterId",
                table: "CourseHistories",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseHistories_Semesters_SemesterId",
                table: "CourseHistories");

            migrationBuilder.RenameColumn(
                name: "SemesterId",
                table: "CourseHistories",
                newName: "SessionYearId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseHistories_SemesterId",
                table: "CourseHistories",
                newName: "IX_CourseHistories_SessionYearId");

            migrationBuilder.AddColumn<int>(
                name: "LevelTermId",
                table: "CourseHistories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CourseHistories_LevelTermId",
                table: "CourseHistories",
                column: "LevelTermId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseHistories_LevelTerms_LevelTermId",
                table: "CourseHistories",
                column: "LevelTermId",
                principalTable: "LevelTerms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseHistories_SessionYears_SessionYearId",
                table: "CourseHistories",
                column: "SessionYearId",
                principalTable: "SessionYears",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
