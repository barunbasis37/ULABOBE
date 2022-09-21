using Microsoft.EntityFrameworkCore.Migrations;

namespace ULABOBE.DataAccess.Migrations
{
    public partial class ModifiedCGSS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseGenericSkills_LevelTerms_LevelTermId",
                table: "CourseGenericSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseGenericSkills_SessionYears_SessionYearId",
                table: "CourseGenericSkills");

            migrationBuilder.DropIndex(
                name: "IX_CourseGenericSkills_LevelTermId",
                table: "CourseGenericSkills");

            migrationBuilder.DropColumn(
                name: "LevelTermId",
                table: "CourseGenericSkills");

            migrationBuilder.RenameColumn(
                name: "SessionYearId",
                table: "CourseGenericSkills",
                newName: "SemesterId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseGenericSkills_SessionYearId",
                table: "CourseGenericSkills",
                newName: "IX_CourseGenericSkills_SemesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseGenericSkills_Semesters_SemesterId",
                table: "CourseGenericSkills",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseGenericSkills_Semesters_SemesterId",
                table: "CourseGenericSkills");

            migrationBuilder.RenameColumn(
                name: "SemesterId",
                table: "CourseGenericSkills",
                newName: "SessionYearId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseGenericSkills_SemesterId",
                table: "CourseGenericSkills",
                newName: "IX_CourseGenericSkills_SessionYearId");

            migrationBuilder.AddColumn<int>(
                name: "LevelTermId",
                table: "CourseGenericSkills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CourseGenericSkills_LevelTermId",
                table: "CourseGenericSkills",
                column: "LevelTermId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseGenericSkills_LevelTerms_LevelTermId",
                table: "CourseGenericSkills",
                column: "LevelTermId",
                principalTable: "LevelTerms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseGenericSkills_SessionYears_SessionYearId",
                table: "CourseGenericSkills",
                column: "SessionYearId",
                principalTable: "SessionYears",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
