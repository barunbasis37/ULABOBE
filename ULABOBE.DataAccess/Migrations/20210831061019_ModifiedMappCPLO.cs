using Microsoft.EntityFrameworkCore.Migrations;

namespace ULABOBE.DataAccess.Migrations
{
    public partial class ModifiedMappCPLO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LevelTermId",
                table: "MappingCourseProgramLos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SessionYearId",
                table: "MappingCourseProgramLos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MappingCourseProgramLos_LevelTermId",
                table: "MappingCourseProgramLos",
                column: "LevelTermId");

            migrationBuilder.CreateIndex(
                name: "IX_MappingCourseProgramLos_SessionYearId",
                table: "MappingCourseProgramLos",
                column: "SessionYearId");

            migrationBuilder.AddForeignKey(
                name: "FK_MappingCourseProgramLos_LevelTerms_LevelTermId",
                table: "MappingCourseProgramLos",
                column: "LevelTermId",
                principalTable: "LevelTerms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MappingCourseProgramLos_SessionYears_SessionYearId",
                table: "MappingCourseProgramLos",
                column: "SessionYearId",
                principalTable: "SessionYears",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MappingCourseProgramLos_LevelTerms_LevelTermId",
                table: "MappingCourseProgramLos");

            migrationBuilder.DropForeignKey(
                name: "FK_MappingCourseProgramLos_SessionYears_SessionYearId",
                table: "MappingCourseProgramLos");

            migrationBuilder.DropIndex(
                name: "IX_MappingCourseProgramLos_LevelTermId",
                table: "MappingCourseProgramLos");

            migrationBuilder.DropIndex(
                name: "IX_MappingCourseProgramLos_SessionYearId",
                table: "MappingCourseProgramLos");

            migrationBuilder.DropColumn(
                name: "LevelTermId",
                table: "MappingCourseProgramLos");

            migrationBuilder.DropColumn(
                name: "SessionYearId",
                table: "MappingCourseProgramLos");
        }
    }
}
