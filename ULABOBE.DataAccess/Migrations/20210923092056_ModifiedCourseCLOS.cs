using Microsoft.EntityFrameworkCore.Migrations;

namespace ULABOBE.DataAccess.Migrations
{
    public partial class ModifiedCourseCLOS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseClos_LevelTerms_LevelTermId",
                table: "CourseClos");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseClos_SessionYears_SessionYearId",
                table: "CourseClos");

            migrationBuilder.DropIndex(
                name: "IX_CourseClos_LevelTermId",
                table: "CourseClos");

            migrationBuilder.DropColumn(
                name: "LevelTermId",
                table: "CourseClos");

            migrationBuilder.RenameColumn(
                name: "SessionYearId",
                table: "CourseClos",
                newName: "SemesterId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseClos_SessionYearId",
                table: "CourseClos",
                newName: "IX_CourseClos_SemesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseClos_Semesters_SemesterId",
                table: "CourseClos",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseClos_Semesters_SemesterId",
                table: "CourseClos");

            migrationBuilder.RenameColumn(
                name: "SemesterId",
                table: "CourseClos",
                newName: "SessionYearId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseClos_SemesterId",
                table: "CourseClos",
                newName: "IX_CourseClos_SessionYearId");

            migrationBuilder.AddColumn<int>(
                name: "LevelTermId",
                table: "CourseClos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CourseClos_LevelTermId",
                table: "CourseClos",
                column: "LevelTermId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseClos_LevelTerms_LevelTermId",
                table: "CourseClos",
                column: "LevelTermId",
                principalTable: "LevelTerms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseClos_SessionYears_SessionYearId",
                table: "CourseClos",
                column: "SessionYearId",
                principalTable: "SessionYears",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
