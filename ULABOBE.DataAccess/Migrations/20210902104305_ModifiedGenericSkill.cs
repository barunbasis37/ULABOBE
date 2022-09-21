using Microsoft.EntityFrameworkCore.Migrations;

namespace ULABOBE.DataAccess.Migrations
{
    public partial class ModifiedGenericSkill : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GenericSkills_CourseLearningTypes_GKTypeId",
                table: "GenericSkills");

            migrationBuilder.AddForeignKey(
                name: "FK_GenericSkills_GenericSkillTypes_GKTypeId",
                table: "GenericSkills",
                column: "GKTypeId",
                principalTable: "GenericSkillTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GenericSkills_GenericSkillTypes_GKTypeId",
                table: "GenericSkills");

            migrationBuilder.AddForeignKey(
                name: "FK_GenericSkills_CourseLearningTypes_GKTypeId",
                table: "GenericSkills",
                column: "GKTypeId",
                principalTable: "CourseLearningTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
