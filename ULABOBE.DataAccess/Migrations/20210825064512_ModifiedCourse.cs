using Microsoft.EntityFrameworkCore.Migrations;

namespace ULABOBE.DataAccess.Migrations
{
    public partial class ModifiedCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_LevelTerms_LevelTermId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_SessionYears_SessionYearId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_LevelTermId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_SessionYearId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "ContactHr",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "LevelTermId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "SessionYearId",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Courses",
                newName: "Title");

            migrationBuilder.AddColumn<string>(
                name: "Marks",
                table: "Courses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Marks",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Courses",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "ContactHr",
                table: "Courses",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LevelTermId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SessionYearId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_LevelTermId",
                table: "Courses",
                column: "LevelTermId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_SessionYearId",
                table: "Courses",
                column: "SessionYearId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_LevelTerms_LevelTermId",
                table: "Courses",
                column: "LevelTermId",
                principalTable: "LevelTerms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_SessionYears_SessionYearId",
                table: "Courses",
                column: "SessionYearId",
                principalTable: "SessionYears",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
