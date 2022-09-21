using Microsoft.EntityFrameworkCore.Migrations;

namespace ULABOBE.DataAccess.Migrations
{
    public partial class Sp_RptViewGetCourseProMappings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"Create PROC usp_RptViewCourseProgramMappings
                                    As 
                                        BEGIN
                                            SELECT CourseCode, CLOCode, Description, Name, InstructorId, InstructorShortCode, PLoSelectedIDNames, GSSelectedIDNames, PSSelectedIDNames, SDGSelectedIDNames, ARSelectedIDNames, IsApproved
                                            FROM     view_RptCourseProgramMappings                                            
                                        END");
            migrationBuilder.Sql(@"CREATE PROC usp_RptViewCourseProgramMapping
                                    @Id int 
                                    AS 
                                    BEGIN 
                                        SELECT CourseCode, CLOCode, Description, Name, InstructorId, InstructorShortCode,PLoSelectedIDNames, GSSelectedIDNames, PSSelectedIDNames, SDGSelectedIDNames, ARSelectedIDNames, IsApproved
                                            FROM     view_RptCourseProgramMappings
                                        Where (InstructorId= @Id) 
                                    END ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE usp_RptViewCourseProgramMappings");
            migrationBuilder.Sql(@"DROP PROCEDURE usp_RptViewCourseProgramMapping");
        }
    }
}
