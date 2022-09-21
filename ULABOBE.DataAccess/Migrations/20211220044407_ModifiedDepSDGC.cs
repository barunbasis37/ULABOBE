using Microsoft.EntityFrameworkCore.Migrations;

namespace ULABOBE.DataAccess.Migrations
{
    public partial class ModifiedDepSDGC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentSdgContributions_LevelTerms_LevelTermId",
                table: "DepartmentSdgContributions");

            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentSdgContributions_SessionYears_SessionYearId",
                table: "DepartmentSdgContributions");

            migrationBuilder.DropIndex(
                name: "IX_DepartmentSdgContributions_LevelTermId",
                table: "DepartmentSdgContributions");

            migrationBuilder.DropColumn(
                name: "LevelTermId",
                table: "DepartmentSdgContributions");

            migrationBuilder.RenameColumn(
                name: "SessionYearId",
                table: "DepartmentSdgContributions",
                newName: "SemesterId");

            migrationBuilder.RenameIndex(
                name: "IX_DepartmentSdgContributions_SessionYearId",
                table: "DepartmentSdgContributions",
                newName: "IX_DepartmentSdgContributions_SemesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentSdgContributions_Semesters_SemesterId",
                table: "DepartmentSdgContributions",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentSdgContributions_Semesters_SemesterId",
                table: "DepartmentSdgContributions");

            migrationBuilder.RenameColumn(
                name: "SemesterId",
                table: "DepartmentSdgContributions",
                newName: "SessionYearId");

            migrationBuilder.RenameIndex(
                name: "IX_DepartmentSdgContributions_SemesterId",
                table: "DepartmentSdgContributions",
                newName: "IX_DepartmentSdgContributions_SessionYearId");

            migrationBuilder.AddColumn<int>(
                name: "LevelTermId",
                table: "DepartmentSdgContributions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentSdgContributions_LevelTermId",
                table: "DepartmentSdgContributions",
                column: "LevelTermId");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentSdgContributions_LevelTerms_LevelTermId",
                table: "DepartmentSdgContributions",
                column: "LevelTermId",
                principalTable: "LevelTerms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentSdgContributions_SessionYears_SessionYearId",
                table: "DepartmentSdgContributions",
                column: "SessionYearId",
                principalTable: "SessionYears",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
