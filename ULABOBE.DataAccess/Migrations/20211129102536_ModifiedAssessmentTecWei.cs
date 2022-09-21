using Microsoft.EntityFrameworkCore.Migrations;

namespace ULABOBE.DataAccess.Migrations
{
    public partial class ModifiedAssessmentTecWei : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssessmentTypeId",
                table: "AssessmentTechniqueWeightages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentTechniqueWeightages_AssessmentTypeId",
                table: "AssessmentTechniqueWeightages",
                column: "AssessmentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssessmentTechniqueWeightages_AssessmentTypes_AssessmentTypeId",
                table: "AssessmentTechniqueWeightages",
                column: "AssessmentTypeId",
                principalTable: "AssessmentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssessmentTechniqueWeightages_AssessmentTypes_AssessmentTypeId",
                table: "AssessmentTechniqueWeightages");

            migrationBuilder.DropIndex(
                name: "IX_AssessmentTechniqueWeightages_AssessmentTypeId",
                table: "AssessmentTechniqueWeightages");

            migrationBuilder.DropColumn(
                name: "AssessmentTypeId",
                table: "AssessmentTechniqueWeightages");
        }
    }
}
