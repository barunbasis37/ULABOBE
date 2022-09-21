using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ULABOBE.DataAccess.Migrations
{
    public partial class AddProfessionalSkill : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GenericSkills_GenericSkillTypes_GKTypeId",
                table: "GenericSkills");

            migrationBuilder.DropIndex(
                name: "IX_GenericSkills_GKTypeId",
                table: "GenericSkills");

            migrationBuilder.DropColumn(
                name: "GKTypeId",
                table: "GenericSkills");

            migrationBuilder.DropColumn(
                name: "GSTypeId",
                table: "GenericSkills");

            migrationBuilder.CreateTable(
                name: "ProfessionalSkills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PSCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_ProfessionalSkills", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfessionalSkills");

            migrationBuilder.AddColumn<int>(
                name: "GKTypeId",
                table: "GenericSkills",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GSTypeId",
                table: "GenericSkills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_GenericSkills_GKTypeId",
                table: "GenericSkills",
                column: "GKTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_GenericSkills_GenericSkillTypes_GKTypeId",
                table: "GenericSkills",
                column: "GKTypeId",
                principalTable: "GenericSkillTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
