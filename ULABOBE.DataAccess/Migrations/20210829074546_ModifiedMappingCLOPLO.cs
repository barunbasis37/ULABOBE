using Microsoft.EntityFrameworkCore.Migrations;

namespace ULABOBE.DataAccess.Migrations
{
    public partial class ModifiedMappingCLOPLO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CLOId",
                table: "MappingCourseProgramLos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PLOId",
                table: "MappingCourseProgramLos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MappingCourseProgramLos_CLOId",
                table: "MappingCourseProgramLos",
                column: "CLOId");

            migrationBuilder.CreateIndex(
                name: "IX_MappingCourseProgramLos_PLOId",
                table: "MappingCourseProgramLos",
                column: "PLOId");

            migrationBuilder.AddForeignKey(
                name: "FK_MappingCourseProgramLos_CourseLearnings_CLOId",
                table: "MappingCourseProgramLos",
                column: "CLOId",
                principalTable: "CourseLearnings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MappingCourseProgramLos_ProgramLearnings_PLOId",
                table: "MappingCourseProgramLos",
                column: "PLOId",
                principalTable: "ProgramLearnings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MappingCourseProgramLos_CourseLearnings_CLOId",
                table: "MappingCourseProgramLos");

            migrationBuilder.DropForeignKey(
                name: "FK_MappingCourseProgramLos_ProgramLearnings_PLOId",
                table: "MappingCourseProgramLos");

            migrationBuilder.DropIndex(
                name: "IX_MappingCourseProgramLos_CLOId",
                table: "MappingCourseProgramLos");

            migrationBuilder.DropIndex(
                name: "IX_MappingCourseProgramLos_PLOId",
                table: "MappingCourseProgramLos");

            migrationBuilder.DropColumn(
                name: "CLOId",
                table: "MappingCourseProgramLos");

            migrationBuilder.DropColumn(
                name: "PLOId",
                table: "MappingCourseProgramLos");
        }
    }
}
