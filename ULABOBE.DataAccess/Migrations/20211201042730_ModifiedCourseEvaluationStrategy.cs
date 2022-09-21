using Microsoft.EntityFrameworkCore.Migrations;

namespace ULABOBE.DataAccess.Migrations
{
    public partial class ModifiedCourseEvaluationStrategy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EvaluationPolicy",
                table: "Courses",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeachingLearningStrategy",
                table: "Courses",
                type: "nvarchar(700)",
                maxLength: 700,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EvaluationPolicy",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "TeachingLearningStrategy",
                table: "Courses");
        }
    }
}
