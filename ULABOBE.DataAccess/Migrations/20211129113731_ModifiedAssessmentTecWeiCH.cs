using Microsoft.EntityFrameworkCore.Migrations;

namespace ULABOBE.DataAccess.Migrations
{
    public partial class ModifiedAssessmentTecWeiCH : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseHistoryId",
                table: "AssessmentTechniqueWeightages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentTechniqueWeightages_CourseHistoryId",
                table: "AssessmentTechniqueWeightages",
                column: "CourseHistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssessmentTechniqueWeightages_CourseHistories_CourseHistoryId",
                table: "AssessmentTechniqueWeightages",
                column: "CourseHistoryId",
                principalTable: "CourseHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssessmentTechniqueWeightages_CourseHistories_CourseHistoryId",
                table: "AssessmentTechniqueWeightages");

            migrationBuilder.DropIndex(
                name: "IX_AssessmentTechniqueWeightages_CourseHistoryId",
                table: "AssessmentTechniqueWeightages");

            migrationBuilder.DropColumn(
                name: "CourseHistoryId",
                table: "AssessmentTechniqueWeightages");
        }
    }
}
