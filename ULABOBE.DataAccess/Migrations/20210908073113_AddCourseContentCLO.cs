using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ULABOBE.DataAccess.Migrations
{
    public partial class AddCourseContentCLO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseLearnings_CourseContents_CourseContentId",
                table: "CourseLearnings");

            migrationBuilder.DropIndex(
                name: "IX_CourseLearnings_CourseContentId",
                table: "CourseLearnings");

            migrationBuilder.DropColumn(
                name: "CourseContentId",
                table: "CourseLearnings");

            migrationBuilder.AddColumn<int>(
                name: "CourseContentId",
                table: "CourseContents",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CourseContentCLOs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseContentId = table.Column<int>(type: "int", nullable: false),
                    CLOId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_CourseContentCLOs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseContentCLOs_CourseContents_CourseContentId",
                        column: x => x.CourseContentId,
                        principalTable: "CourseContents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseContentCLOs_CourseLearnings_CLOId",
                        column: x => x.CLOId,
                        principalTable: "CourseLearnings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "FK_CourseContents_CourseContents_CourseContentId",
                table: "CourseContents",
                column: "CourseContentId",
                principalTable: "CourseContents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseContents_CourseContents_CourseContentId",
                table: "CourseContents");

            migrationBuilder.DropTable(
                name: "CourseContentCLOs");

            migrationBuilder.DropIndex(
                name: "IX_CourseContents_CourseContentId",
                table: "CourseContents");

            migrationBuilder.DropColumn(
                name: "CourseContentId",
                table: "CourseContents");

            migrationBuilder.AddColumn<int>(
                name: "CourseContentId",
                table: "CourseLearnings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseLearnings_CourseContentId",
                table: "CourseLearnings",
                column: "CourseContentId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseLearnings_CourseContents_CourseContentId",
                table: "CourseLearnings",
                column: "CourseContentId",
                principalTable: "CourseContents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
