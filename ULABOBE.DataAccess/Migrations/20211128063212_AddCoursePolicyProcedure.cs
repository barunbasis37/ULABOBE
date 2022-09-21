using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ULABOBE.DataAccess.Migrations
{
    public partial class AddCoursePolicyProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoursePolicyProcedures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseHistoryId = table.Column<int>(type: "int", nullable: false),
                    CPolicyTypeIdId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false),
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
                    table.PrimaryKey("PK_CoursePolicyProcedures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoursePolicyProcedures_CourseHistories_CourseHistoryId",
                        column: x => x.CourseHistoryId,
                        principalTable: "CourseHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoursePolicyProcedures_CoursePolicyTypes_CPolicyTypeIdId",
                        column: x => x.CPolicyTypeIdId,
                        principalTable: "CoursePolicyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoursePolicyProcedures_CourseHistoryId",
                table: "CoursePolicyProcedures",
                column: "CourseHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CoursePolicyProcedures_CPolicyTypeIdId",
                table: "CoursePolicyProcedures",
                column: "CPolicyTypeIdId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoursePolicyProcedures");
        }
    }
}
