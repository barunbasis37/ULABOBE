using Microsoft.EntityFrameworkCore.Migrations;

namespace ULABOBE.DataAccess.Migrations
{
    public partial class ModfiedCourseGS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseGenericSkills_GenericSkills_CLOId",
                table: "CourseGenericSkills");

            migrationBuilder.DropIndex(
                name: "IX_CourseGenericSkills_CLOId",
                table: "CourseGenericSkills");

            migrationBuilder.DropColumn(
                name: "CLOId",
                table: "CourseGenericSkills");

            migrationBuilder.CreateIndex(
                name: "IX_CourseGenericSkills_GSId",
                table: "CourseGenericSkills",
                column: "GSId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseGenericSkills_GenericSkills_GSId",
                table: "CourseGenericSkills",
                column: "GSId",
                principalTable: "GenericSkills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseGenericSkills_GenericSkills_GSId",
                table: "CourseGenericSkills");

            migrationBuilder.DropIndex(
                name: "IX_CourseGenericSkills_GSId",
                table: "CourseGenericSkills");

            migrationBuilder.AddColumn<int>(
                name: "CLOId",
                table: "CourseGenericSkills",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseGenericSkills_CLOId",
                table: "CourseGenericSkills",
                column: "CLOId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseGenericSkills_GenericSkills_CLOId",
                table: "CourseGenericSkills",
                column: "CLOId",
                principalTable: "GenericSkills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
