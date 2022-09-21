using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ULABOBE.DataAccess.Migrations
{
    public partial class AddCourseClassDocument : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseClassDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseHistoryId = table.Column<int>(type: "int", nullable: false),
                    ClassMonitoringFileName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ClassMonitoringFileExtension = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ClassMonitoringFileUploadUrl = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    CourseSessionFileName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CourseSessionExtension = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CourseSessionFileUploadUrl = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    SemesterCourseFileName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    SemesterCourseExtension = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SemesterCourseFileUploadUrl = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    LessonPlanFileName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    LessonPlanExtension = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    LessonPlanFileUploadUrl = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    CourseProgramFileName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CourseProgramExtension = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CourseProgramFileUploadUrl = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    AttendanceSheetFileName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    AttendanceSheetExtension = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    AttendanceSheetFileUploadUrl = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    QueryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedIp = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdatedIp = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseClassDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseClassDocuments_CourseHistories_CourseHistoryId",
                        column: x => x.CourseHistoryId,
                        principalTable: "CourseHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseClassDocuments_CourseHistoryId",
                table: "CourseClassDocuments",
                column: "CourseHistoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseClassDocuments");
        }
    }
}
