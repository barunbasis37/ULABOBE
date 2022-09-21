using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ULABOBE.DataAccess.Migrations
{
    public partial class AddCourseContentD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseContentId",
                table: "CourseLearnings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CourseContents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    LevelTermId = table.Column<int>(type: "int", nullable: false),
                    SessionYearId = table.Column<int>(type: "int", nullable: false),
                    Topic = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    SessionQuantity = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_CourseContents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseContents_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseContents_LevelTerms_LevelTermId",
                        column: x => x.LevelTermId,
                        principalTable: "LevelTerms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseContents_SessionYears_SessionYearId",
                        column: x => x.SessionYearId,
                        principalTable: "SessionYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseLearnings_CourseContentId",
                table: "CourseLearnings",
                column: "CourseContentId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseContents_CourseId",
                table: "CourseContents",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseContents_LevelTermId",
                table: "CourseContents",
                column: "LevelTermId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseContents_SessionYearId",
                table: "CourseContents",
                column: "SessionYearId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseLearnings_CourseContents_CourseContentId",
                table: "CourseLearnings",
                column: "CourseContentId",
                principalTable: "CourseContents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseLearnings_CourseContents_CourseContentId",
                table: "CourseLearnings");

            migrationBuilder.DropTable(
                name: "CourseContents");

            migrationBuilder.DropIndex(
                name: "IX_CourseLearnings_CourseContentId",
                table: "CourseLearnings");

            migrationBuilder.DropColumn(
                name: "CourseContentId",
                table: "CourseLearnings");
        }
    }
}
