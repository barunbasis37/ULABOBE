using Microsoft.EntityFrameworkCore.Migrations;

namespace ULABOBE.DataAccess.Migrations
{
    public partial class ModofoedSectionAndCourseHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SchoolCode",
                table: "Sections",
                newName: "SectionCode");

            migrationBuilder.RenameColumn(
                name: "PriorityLevel",
                table: "Sections",
                newName: "Priority");

            migrationBuilder.AddColumn<int>(
                name: "SectionId",
                table: "CourseHistories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CourseHistories_SectionId",
                table: "CourseHistories",
                column: "SectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseHistories_Sections_SectionId",
                table: "CourseHistories",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseHistories_Sections_SectionId",
                table: "CourseHistories");

            migrationBuilder.DropIndex(
                name: "IX_CourseHistories_SectionId",
                table: "CourseHistories");

            migrationBuilder.DropColumn(
                name: "SectionId",
                table: "CourseHistories");

            migrationBuilder.RenameColumn(
                name: "SectionCode",
                table: "Sections",
                newName: "SchoolCode");

            migrationBuilder.RenameColumn(
                name: "Priority",
                table: "Sections",
                newName: "PriorityLevel");
        }
    }
}
