using Microsoft.EntityFrameworkCore.Migrations;

namespace ULABOBE.DataAccess.Migrations
{
    public partial class ModifiedMappCPLOS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "LevelTermId",
                table: "MappingCourseProgramLos");

            migrationBuilder.RenameColumn(
                name: "SessionYearId",
                table: "MappingCourseProgramLos",
                newName: "SemesterId");

            migrationBuilder.RenameIndex(
                name: "IX_MappingCourseProgramLos_SessionYearId",
                table: "MappingCourseProgramLos",
                newName: "IX_MappingCourseProgramLos_SemesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_MappingCourseProgramLos_Semesters_SemesterId",
                table: "MappingCourseProgramLos",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MappingCourseProgramLos_Semesters_SemesterId",
                table: "MappingCourseProgramLos");

            migrationBuilder.RenameColumn(
                name: "SemesterId",
                table: "MappingCourseProgramLos",
                newName: "SessionYearId");

            migrationBuilder.RenameIndex(
                name: "IX_MappingCourseProgramLos_SemesterId",
                table: "MappingCourseProgramLos",
                newName: "IX_MappingCourseProgramLos_SessionYearId");

            migrationBuilder.AddColumn<int>(
                name: "LevelTermId",
                table: "MappingCourseProgramLos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MappingCourseProgramLos_LevelTermId",
                table: "MappingCourseProgramLos",
                column: "LevelTermId");

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
    }
}
