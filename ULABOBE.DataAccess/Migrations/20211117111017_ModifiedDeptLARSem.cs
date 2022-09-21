using Microsoft.EntityFrameworkCore.Migrations;

namespace ULABOBE.DataAccess.Migrations
{
    public partial class ModifiedDeptLARSem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentLars_Semesters_SessionYearId",
                table: "DepartmentLars");

            migrationBuilder.DropIndex(
                name: "IX_DepartmentLars_SessionYearId",
                table: "DepartmentLars");

            migrationBuilder.DropColumn(
                name: "SessionYearId",
                table: "DepartmentLars");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentLars_SemesterId",
                table: "DepartmentLars",
                column: "SemesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentLars_Semesters_SemesterId",
                table: "DepartmentLars",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentLars_Semesters_SemesterId",
                table: "DepartmentLars");

            migrationBuilder.DropIndex(
                name: "IX_DepartmentLars_SemesterId",
                table: "DepartmentLars");

            migrationBuilder.AddColumn<int>(
                name: "SessionYearId",
                table: "DepartmentLars",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentLars_SessionYearId",
                table: "DepartmentLars",
                column: "SessionYearId");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentLars_Semesters_SessionYearId",
                table: "DepartmentLars",
                column: "SessionYearId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
