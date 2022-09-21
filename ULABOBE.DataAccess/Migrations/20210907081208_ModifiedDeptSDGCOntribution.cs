using Microsoft.EntityFrameworkCore.Migrations;

namespace ULABOBE.DataAccess.Migrations
{
    public partial class ModifiedDeptSDGCOntribution : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentSdgContributions_LearningAssessmentRubrics_LARId",
                table: "DepartmentSdgContributions");

            migrationBuilder.DropIndex(
                name: "IX_DepartmentSdgContributions_LARId",
                table: "DepartmentSdgContributions");

            migrationBuilder.DropColumn(
                name: "LARId",
                table: "DepartmentSdgContributions");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentSdgContributions_SDGConId",
                table: "DepartmentSdgContributions",
                column: "SDGConId");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentSdgContributions_SDGContributions_SDGConId",
                table: "DepartmentSdgContributions",
                column: "SDGConId",
                principalTable: "SDGContributions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentSdgContributions_SDGContributions_SDGConId",
                table: "DepartmentSdgContributions");

            migrationBuilder.DropIndex(
                name: "IX_DepartmentSdgContributions_SDGConId",
                table: "DepartmentSdgContributions");

            migrationBuilder.AddColumn<int>(
                name: "LARId",
                table: "DepartmentSdgContributions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentSdgContributions_LARId",
                table: "DepartmentSdgContributions",
                column: "LARId");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentSdgContributions_LearningAssessmentRubrics_LARId",
                table: "DepartmentSdgContributions",
                column: "LARId",
                principalTable: "LearningAssessmentRubrics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
