using Microsoft.EntityFrameworkCore.Migrations;

namespace ULABOBE.DataAccess.Migrations
{
    public partial class ModifiedCourseAndCourseHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Marks",
                table: "Courses");

            migrationBuilder.AlterColumn<decimal>(
                name: "CreditHour",
                table: "Courses",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CIEMarks",
                table: "CourseHistories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SEEMarks",
                table: "CourseHistories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalMarks",
                table: "CourseHistories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CIEMarks",
                table: "CourseHistories");

            migrationBuilder.DropColumn(
                name: "SEEMarks",
                table: "CourseHistories");

            migrationBuilder.DropColumn(
                name: "TotalMarks",
                table: "CourseHistories");

            migrationBuilder.AlterColumn<int>(
                name: "CreditHour",
                table: "Courses",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "Marks",
                table: "Courses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
