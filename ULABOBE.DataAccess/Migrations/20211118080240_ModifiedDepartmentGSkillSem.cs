using Microsoft.EntityFrameworkCore.Migrations;

namespace ULABOBE.DataAccess.Migrations
{
    public partial class ModifiedDepartmentGSkillSem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SemesterId",
                table: "DepartmentGSkills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentGSkills_SemesterId",
                table: "DepartmentGSkills",
                column: "SemesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentGSkills_Semesters_SemesterId",
                table: "DepartmentGSkills",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentGSkills_Semesters_SemesterId",
                table: "DepartmentGSkills");

            migrationBuilder.DropIndex(
                name: "IX_DepartmentGSkills_SemesterId",
                table: "DepartmentGSkills");

            migrationBuilder.DropColumn(
                name: "SemesterId",
                table: "DepartmentGSkills");
        }
    }
}
