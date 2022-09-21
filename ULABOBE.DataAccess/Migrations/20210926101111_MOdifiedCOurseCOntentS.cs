using Microsoft.EntityFrameworkCore.Migrations;

namespace ULABOBE.DataAccess.Migrations
{
    public partial class MOdifiedCOurseCOntentS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseContents_LevelTerms_LevelTermId",
                table: "CourseContents");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseContents_SessionYears_SessionYearId",
                table: "CourseContents");

            migrationBuilder.DropIndex(
                name: "IX_CourseContents_LevelTermId",
                table: "CourseContents");

            migrationBuilder.DropColumn(
                name: "LevelTermId",
                table: "CourseContents");

            migrationBuilder.RenameColumn(
                name: "SessionYearId",
                table: "CourseContents",
                newName: "SemesterId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseContents_SessionYearId",
                table: "CourseContents",
                newName: "IX_CourseContents_SemesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseContents_Semesters_SemesterId",
                table: "CourseContents",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseContents_Semesters_SemesterId",
                table: "CourseContents");

            migrationBuilder.RenameColumn(
                name: "SemesterId",
                table: "CourseContents",
                newName: "SessionYearId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseContents_SemesterId",
                table: "CourseContents",
                newName: "IX_CourseContents_SessionYearId");

            migrationBuilder.AddColumn<int>(
                name: "LevelTermId",
                table: "CourseContents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CourseContents_LevelTermId",
                table: "CourseContents",
                column: "LevelTermId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseContents_LevelTerms_LevelTermId",
                table: "CourseContents",
                column: "LevelTermId",
                principalTable: "LevelTerms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseContents_SessionYears_SessionYearId",
                table: "CourseContents",
                column: "SessionYearId",
                principalTable: "SessionYears",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
