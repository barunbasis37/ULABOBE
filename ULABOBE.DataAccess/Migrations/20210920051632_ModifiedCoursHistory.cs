using Microsoft.EntityFrameworkCore.Migrations;

namespace ULABOBE.DataAccess.Migrations
{
    public partial class ModifiedCoursHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactHr",
                table: "CourseHistories");

            migrationBuilder.AddColumn<string>(
                name: "ScheduleIDs",
                table: "CourseHistories",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SchedulesNames",
                table: "CourseHistories",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScheduleIDs",
                table: "CourseHistories");

            migrationBuilder.DropColumn(
                name: "SchedulesNames",
                table: "CourseHistories");

            migrationBuilder.AddColumn<string>(
                name: "ContactHr",
                table: "CourseHistories",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);
        }
    }
}
