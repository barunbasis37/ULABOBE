using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ULABOBE.DataAccess.Migrations
{
    public partial class AddCourseGS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GenericSkills_GenericSkillTypes_GKTypeId",
                table: "GenericSkills");

            migrationBuilder.RenameColumn(
                name: "GKCode",
                table: "GenericSkills",
                newName: "GSCode");

            migrationBuilder.AlterColumn<int>(
                name: "GKTypeId",
                table: "GenericSkills",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "GSTypeId",
                table: "GenericSkills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CourseGenericSkills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    LevelTermId = table.Column<int>(type: "int", nullable: false),
                    SessionYearId = table.Column<int>(type: "int", nullable: false),
                    GSId = table.Column<int>(type: "int", nullable: false),
                    CLOId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
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
                    table.PrimaryKey("PK_CourseGenericSkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseGenericSkills_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseGenericSkills_GenericSkills_CLOId",
                        column: x => x.CLOId,
                        principalTable: "GenericSkills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseGenericSkills_LevelTerms_LevelTermId",
                        column: x => x.LevelTermId,
                        principalTable: "LevelTerms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseGenericSkills_SessionYears_SessionYearId",
                        column: x => x.SessionYearId,
                        principalTable: "SessionYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseGenericSkills_CLOId",
                table: "CourseGenericSkills",
                column: "CLOId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseGenericSkills_CourseId",
                table: "CourseGenericSkills",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseGenericSkills_LevelTermId",
                table: "CourseGenericSkills",
                column: "LevelTermId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseGenericSkills_SessionYearId",
                table: "CourseGenericSkills",
                column: "SessionYearId");

            migrationBuilder.AddForeignKey(
                name: "FK_GenericSkills_GenericSkillTypes_GKTypeId",
                table: "GenericSkills",
                column: "GKTypeId",
                principalTable: "GenericSkillTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GenericSkills_GenericSkillTypes_GKTypeId",
                table: "GenericSkills");

            migrationBuilder.DropTable(
                name: "CourseGenericSkills");

            migrationBuilder.DropColumn(
                name: "GSTypeId",
                table: "GenericSkills");

            migrationBuilder.RenameColumn(
                name: "GSCode",
                table: "GenericSkills",
                newName: "GKCode");

            migrationBuilder.AlterColumn<int>(
                name: "GKTypeId",
                table: "GenericSkills",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GenericSkills_GenericSkillTypes_GKTypeId",
                table: "GenericSkills",
                column: "GKTypeId",
                principalTable: "GenericSkillTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
