using Microsoft.EntityFrameworkCore.Migrations;

namespace ULABOBE.DataAccess.Migrations
{
    public partial class ModifiedCourseCLO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_CourseClos_CourseHistories_CourseId",
            //    table: "CourseClos");

            migrationBuilder.AddColumn<int>(
                name: "LevelTermId",
                table: "CourseClos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SessionYearId",
                table: "CourseClos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CourseClos_LevelTermId",
                table: "CourseClos",
                column: "LevelTermId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseClos_SessionYearId",
                table: "CourseClos",
                column: "SessionYearId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_CourseClos_Courses_CourseId",
            //    table: "CourseClos",
            //    column: "CourseId",
            //    principalTable: "Courses",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseClos_Courses_CourseId",
                table: "CourseClos");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseClos_LevelTerms_LevelTermId",
                table: "CourseClos");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseClos_SessionYears_SessionYearId",
                table: "CourseClos");

            migrationBuilder.DropIndex(
                name: "IX_CourseClos_LevelTermId",
                table: "CourseClos");

            migrationBuilder.DropIndex(
                name: "IX_CourseClos_SessionYearId",
                table: "CourseClos");

            migrationBuilder.DropColumn(
                name: "LevelTermId",
                table: "CourseClos");

            migrationBuilder.DropColumn(
                name: "SessionYearId",
                table: "CourseClos");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseClos_CourseHistories_CourseId",
                table: "CourseClos",
                column: "CourseId",
                principalTable: "CourseHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
