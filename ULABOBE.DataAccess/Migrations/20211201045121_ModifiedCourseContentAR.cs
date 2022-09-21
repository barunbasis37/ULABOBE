using Microsoft.EntityFrameworkCore.Migrations;

namespace ULABOBE.DataAccess.Migrations
{
    public partial class ModifiedCourseContentAR : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ARSelectedIDNames",
                table: "CourseContents",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ARSelectedIDs",
                table: "CourseContents",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ARSelectedIDNames",
                table: "CourseContents");

            migrationBuilder.DropColumn(
                name: "ARSelectedIDs",
                table: "CourseContents");
        }
    }
}
