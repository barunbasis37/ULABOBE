using Microsoft.EntityFrameworkCore.Migrations;

namespace ULABOBE.DataAccess.Migrations
{
    public partial class ModifiedFacultyInfoCheck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProgramName",
                table: "UserInfoChecks");

            migrationBuilder.AlterColumn<string>(
                name: "UserType",
                table: "UserInfoChecks",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserInfoId",
                table: "UserInfoChecks",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ContactNo",
                table: "UserInfoChecks",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "UserInfoChecks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DepartmentSName",
                table: "UserInfoChecks",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Designation",
                table: "UserInfoChecks",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IsActive",
                table: "UserInfoChecks",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProgramId",
                table: "UserInfoChecks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProgramSName",
                table: "UserInfoChecks",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShortCode",
                table: "UserInfoChecks",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "UserInfoChecks",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "UserInfoChecks");

            migrationBuilder.DropColumn(
                name: "DepartmentSName",
                table: "UserInfoChecks");

            migrationBuilder.DropColumn(
                name: "Designation",
                table: "UserInfoChecks");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "UserInfoChecks");

            migrationBuilder.DropColumn(
                name: "ProgramId",
                table: "UserInfoChecks");

            migrationBuilder.DropColumn(
                name: "ProgramSName",
                table: "UserInfoChecks");

            migrationBuilder.DropColumn(
                name: "ShortCode",
                table: "UserInfoChecks");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "UserInfoChecks");

            migrationBuilder.AlterColumn<string>(
                name: "UserType",
                table: "UserInfoChecks",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserInfoId",
                table: "UserInfoChecks",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<string>(
                name: "ContactNo",
                table: "UserInfoChecks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProgramName",
                table: "UserInfoChecks",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
