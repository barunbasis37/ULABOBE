using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ULABOBE.DataAccess.Migrations
{
    public partial class AddTime_ModifiedSchedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScTime1",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "ScTime2",
                table: "Schedules");

            migrationBuilder.AddColumn<int>(
                name: "FromTime",
                table: "Schedules",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ToTime",
                table: "Schedules",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Times",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
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
                    table.PrimaryKey("PK_Times", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_FromTime",
                table: "Schedules",
                column: "FromTime");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_ToTime",
                table: "Schedules",
                column: "ToTime");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Times_FromTime",
                table: "Schedules",
                column: "FromTime",
                principalTable: "Times",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Times_ToTime",
                table: "Schedules",
                column: "ToTime",
                principalTable: "Times",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Times_FromTime",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Times_ToTime",
                table: "Schedules");

            migrationBuilder.DropTable(
                name: "Times");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_FromTime",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_ToTime",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "FromTime",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "ToTime",
                table: "Schedules");

            migrationBuilder.AddColumn<string>(
                name: "ScTime1",
                table: "Schedules",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ScTime2",
                table: "Schedules",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
