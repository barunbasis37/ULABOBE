using Microsoft.EntityFrameworkCore.Migrations;

namespace ULABOBE.DataAccess.Migrations
{
    public partial class ModifiedGenericSAddGSType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GSkillTypeId",
                table: "GenericSkills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_GenericSkills_GSkillTypeId",
                table: "GenericSkills",
                column: "GSkillTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_GenericSkills_GenericSkillTypes_GSkillTypeId",
                table: "GenericSkills",
                column: "GSkillTypeId",
                principalTable: "GenericSkillTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GenericSkills_GenericSkillTypes_GSkillTypeId",
                table: "GenericSkills");

            migrationBuilder.DropIndex(
                name: "IX_GenericSkills_GSkillTypeId",
                table: "GenericSkills");

            migrationBuilder.DropColumn(
                name: "GSkillTypeId",
                table: "GenericSkills");
        }
    }
}
