using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ULABOBE.DataAccess.Migrations
{
    public partial class AddCourseProgramMapping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseProgramMappings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseCLOId = table.Column<int>(type: "int", nullable: false),
                    PLoSelectedIDs = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PLoSelectedIDNames = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CLOSelectedIDs = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CLOSelectedIDNames = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    GSSelectedIDs = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    GSSelectedIDNames = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PSSelectedIDs = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PSSelectedIDNames = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SDGSelectedIDs = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SDGSelectedIDNames = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ARSelectedIDs = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ARSelectedIDNames = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
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
                    table.PrimaryKey("PK_CourseProgramMappings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseProgramMappings_CourseClos_CourseCLOId",
                        column: x => x.CourseCLOId,
                        principalTable: "CourseClos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseProgramMappings_CourseCLOId",
                table: "CourseProgramMappings",
                column: "CourseCLOId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseProgramMappings");
        }
    }
}
