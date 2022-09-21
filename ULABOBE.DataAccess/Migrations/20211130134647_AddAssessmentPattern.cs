using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ULABOBE.DataAccess.Migrations
{
    public partial class AddAssessmentPattern : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssessmentPatterns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BloomsCategoryId = table.Column<int>(type: "int", nullable: false),
                    AssessmentTechWeightagesId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_AssessmentPatterns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssessmentPatterns_AssessmentTechniqueWeightages_AssessmentTechWeightagesId",
                        column: x => x.AssessmentTechWeightagesId,
                        principalTable: "AssessmentTechniqueWeightages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssessmentPatterns_BloomsCategories_BloomsCategoryId",
                        column: x => x.BloomsCategoryId,
                        principalTable: "BloomsCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentPatterns_AssessmentTechWeightagesId",
                table: "AssessmentPatterns",
                column: "AssessmentTechWeightagesId");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentPatterns_BloomsCategoryId",
                table: "AssessmentPatterns",
                column: "BloomsCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssessmentPatterns");
        }
    }
}
