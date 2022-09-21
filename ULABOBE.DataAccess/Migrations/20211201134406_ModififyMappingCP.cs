using Microsoft.EntityFrameworkCore.Migrations;

namespace ULABOBE.DataAccess.Migrations
{
    public partial class ModififyMappingCP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MappingCourseProgramLos_CourseHistories_CourseHistoryId",
                table: "MappingCourseProgramLos");

            migrationBuilder.DropIndex(
                name: "IX_MappingCourseProgramLos_CourseHistoryId",
                table: "MappingCourseProgramLos");

            migrationBuilder.DropColumn(
                name: "CourseHistoryId",
                table: "MappingCourseProgramLos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseHistoryId",
                table: "MappingCourseProgramLos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MappingCourseProgramLos_CourseHistoryId",
                table: "MappingCourseProgramLos",
                column: "CourseHistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_MappingCourseProgramLos_CourseHistories_CourseHistoryId",
                table: "MappingCourseProgramLos",
                column: "CourseHistoryId",
                principalTable: "CourseHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
