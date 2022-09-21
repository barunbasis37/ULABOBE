using Microsoft.EntityFrameworkCore.Migrations;

namespace ULABOBE.DataAccess.Migrations
{
    public partial class ModifiedCourseContentRemoveContentCLO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseContentCLOs_CourseContents_CourseContentId",
                table: "CourseContentCLOs");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseContentCLOs_CourseLearnings_CLOId",
                table: "CourseContentCLOs");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseContents_CourseContents_CourseContentId",
                table: "CourseContents");

            migrationBuilder.DropIndex(
                name: "IX_CourseContents_CourseContentId",
                table: "CourseContents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseContentCLOs",
                table: "CourseContentCLOs");

            migrationBuilder.DropIndex(
                name: "IX_CourseContentCLOs_CLOId",
                table: "CourseContentCLOs");

            migrationBuilder.DropIndex(
                name: "IX_CourseContentCLOs_CourseContentId",
                table: "CourseContentCLOs");

            migrationBuilder.DropColumn(
                name: "CourseContentId",
                table: "CourseContents");

            migrationBuilder.DropColumn(
                name: "CLOId",
                table: "CourseContentCLOs");

            migrationBuilder.DropColumn(
                name: "CourseContentId",
                table: "CourseContentCLOs");

            migrationBuilder.RenameTable(
                name: "CourseContentCLOs",
                newName: "CourseContentCLO");

            migrationBuilder.AddColumn<string>(
                name: "CLoSelectedIDs",
                table: "CourseContents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "CourseContentCLO",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseContentCLO",
                table: "CourseContentCLO",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CourseContentCLO_CourseId",
                table: "CourseContentCLO",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseContentCLO_Courses_CourseId",
                table: "CourseContentCLO",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseContentCLO_Courses_CourseId",
                table: "CourseContentCLO");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseContentCLO",
                table: "CourseContentCLO");

            migrationBuilder.DropIndex(
                name: "IX_CourseContentCLO_CourseId",
                table: "CourseContentCLO");

            migrationBuilder.DropColumn(
                name: "CLoSelectedIDs",
                table: "CourseContents");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "CourseContentCLO");

            migrationBuilder.RenameTable(
                name: "CourseContentCLO",
                newName: "CourseContentCLOs");

            migrationBuilder.AddColumn<int>(
                name: "CourseContentId",
                table: "CourseContents",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CLOId",
                table: "CourseContentCLOs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CourseContentId",
                table: "CourseContentCLOs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseContentCLOs",
                table: "CourseContentCLOs",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CourseContents_CourseContentId",
                table: "CourseContents",
                column: "CourseContentId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseContentCLOs_CLOId",
                table: "CourseContentCLOs",
                column: "CLOId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseContentCLOs_CourseContentId",
                table: "CourseContentCLOs",
                column: "CourseContentId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseContentCLOs_CourseContents_CourseContentId",
                table: "CourseContentCLOs",
                column: "CourseContentId",
                principalTable: "CourseContents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseContentCLOs_CourseLearnings_CLOId",
                table: "CourseContentCLOs",
                column: "CLOId",
                principalTable: "CourseLearnings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseContents_CourseContents_CourseContentId",
                table: "CourseContents",
                column: "CourseContentId",
                principalTable: "CourseContents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
