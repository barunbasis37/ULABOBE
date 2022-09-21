using Microsoft.EntityFrameworkCore.Migrations;

namespace ULABOBE.DataAccess.Migrations
{
    public partial class ModifiedDeptLAR : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentLars_LevelTerms_LevelTermId",
                table: "DepartmentLars");

            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentLars_SessionYears_SessionYearId",
                table: "DepartmentLars");

            migrationBuilder.DropIndex(
                name: "IX_DepartmentLars_LevelTermId",
                table: "DepartmentLars");

            migrationBuilder.RenameColumn(
                name: "LevelTermId",
                table: "DepartmentLars",
                newName: "SemesterId");

            migrationBuilder.AlterColumn<int>(
                name: "SessionYearId",
                table: "DepartmentLars",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentLars_Semesters_SessionYearId",
                table: "DepartmentLars",
                column: "SessionYearId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentLars_Semesters_SessionYearId",
                table: "DepartmentLars");

            migrationBuilder.RenameColumn(
                name: "SemesterId",
                table: "DepartmentLars",
                newName: "LevelTermId");

            migrationBuilder.AlterColumn<int>(
                name: "SessionYearId",
                table: "DepartmentLars",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentLars_LevelTermId",
                table: "DepartmentLars",
                column: "LevelTermId");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentLars_LevelTerms_LevelTermId",
                table: "DepartmentLars",
                column: "LevelTermId",
                principalTable: "LevelTerms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentLars_SessionYears_SessionYearId",
                table: "DepartmentLars",
                column: "SessionYearId",
                principalTable: "SessionYears",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
