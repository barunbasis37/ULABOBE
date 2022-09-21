using Microsoft.EntityFrameworkCore.Migrations;

namespace ULABOBE.DataAccess.Migrations
{
    public partial class ModifiedSemester : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Semesters",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<int>(
                name: "SessionId",
                table: "Semesters",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TermId",
                table: "Semesters",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Semesters_SessionId",
                table: "Semesters",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Semesters_TermId",
                table: "Semesters",
                column: "TermId");

            migrationBuilder.AddForeignKey(
                name: "FK_Semesters_LevelTerms_TermId",
                table: "Semesters",
                column: "TermId",
                principalTable: "LevelTerms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Semesters_SessionYears_SessionId",
                table: "Semesters",
                column: "SessionId",
                principalTable: "SessionYears",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Semesters_LevelTerms_TermId",
                table: "Semesters");

            migrationBuilder.DropForeignKey(
                name: "FK_Semesters_SessionYears_SessionId",
                table: "Semesters");

            migrationBuilder.DropIndex(
                name: "IX_Semesters_SessionId",
                table: "Semesters");

            migrationBuilder.DropIndex(
                name: "IX_Semesters_TermId",
                table: "Semesters");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "Semesters");

            migrationBuilder.DropColumn(
                name: "TermId",
                table: "Semesters");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Semesters",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);
        }
    }
}
