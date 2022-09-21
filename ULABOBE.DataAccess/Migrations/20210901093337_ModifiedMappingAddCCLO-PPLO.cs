using Microsoft.EntityFrameworkCore.Migrations;

namespace ULABOBE.DataAccess.Migrations
{
    public partial class ModifiedMappingAddCCLOPPLO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MappingCourseProgramLos_CourseLearnings_CLOId",
                table: "MappingCourseProgramLos");

            migrationBuilder.DropForeignKey(
                name: "FK_MappingCourseProgramLos_ProgramLearnings_PLOId",
                table: "MappingCourseProgramLos");

            migrationBuilder.RenameColumn(
                name: "PLOId",
                table: "MappingCourseProgramLos",
                newName: "ProgramPLOId");

            migrationBuilder.RenameColumn(
                name: "CLOId",
                table: "MappingCourseProgramLos",
                newName: "CourseCLOId");

            migrationBuilder.RenameIndex(
                name: "IX_MappingCourseProgramLos_PLOId",
                table: "MappingCourseProgramLos",
                newName: "IX_MappingCourseProgramLos_ProgramPLOId");

            migrationBuilder.RenameIndex(
                name: "IX_MappingCourseProgramLos_CLOId",
                table: "MappingCourseProgramLos",
                newName: "IX_MappingCourseProgramLos_CourseCLOId");

            migrationBuilder.AddForeignKey(
                name: "FK_MappingCourseProgramLos_CourseClos_CourseCLOId",
                table: "MappingCourseProgramLos",
                column: "CourseCLOId",
                principalTable: "CourseClos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MappingCourseProgramLos_ProgramPLOs_ProgramPLOId",
                table: "MappingCourseProgramLos",
                column: "ProgramPLOId",
                principalTable: "ProgramPLOs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MappingCourseProgramLos_CourseClos_CourseCLOId",
                table: "MappingCourseProgramLos");

            migrationBuilder.DropForeignKey(
                name: "FK_MappingCourseProgramLos_ProgramPLOs_ProgramPLOId",
                table: "MappingCourseProgramLos");

            migrationBuilder.RenameColumn(
                name: "ProgramPLOId",
                table: "MappingCourseProgramLos",
                newName: "PLOId");

            migrationBuilder.RenameColumn(
                name: "CourseCLOId",
                table: "MappingCourseProgramLos",
                newName: "CLOId");

            migrationBuilder.RenameIndex(
                name: "IX_MappingCourseProgramLos_ProgramPLOId",
                table: "MappingCourseProgramLos",
                newName: "IX_MappingCourseProgramLos_PLOId");

            migrationBuilder.RenameIndex(
                name: "IX_MappingCourseProgramLos_CourseCLOId",
                table: "MappingCourseProgramLos",
                newName: "IX_MappingCourseProgramLos_CLOId");

            migrationBuilder.AddForeignKey(
                name: "FK_MappingCourseProgramLos_CourseLearnings_CLOId",
                table: "MappingCourseProgramLos",
                column: "CLOId",
                principalTable: "CourseLearnings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MappingCourseProgramLos_ProgramLearnings_PLOId",
                table: "MappingCourseProgramLos",
                column: "PLOId",
                principalTable: "ProgramLearnings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
