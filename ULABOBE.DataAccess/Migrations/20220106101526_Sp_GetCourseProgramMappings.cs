using Microsoft.EntityFrameworkCore.Migrations;

namespace ULABOBE.DataAccess.Migrations
{
    public partial class Sp_GetCourseProgramMappings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"Create PROC usp_CourseProgramMappings
                                    As 
                                        BEGIN
                                            Select CourseCode, CLOCode, Description, Name, PLoSelectedIDNames, GSSelectedIDNames, PSSelectedIDNames, SDGSelectedIDNames, ARSelectedIDNames, ApprovedBy, ApprovedDate, ApprovedIp, IsApproved
                                                From dbo.CourseProgramMappings AS CPM
                                                Inner Join CourseLearnings AS CLO on CPM.CourseLearningId= CLO.Id
                                                Left Join CourseHistories AS CHis on CPM.CourseHistoryId= CHis.Id
                                                Left Join Instructors As Ins on CHis.InstructorId= Ins.Id
                                                Left Join Courses As C on CHis.CourseId= C.Id
                                                Left Join CourseClos CCLO on CLO.Id= CCLO.CLOId
                                                Where CPM.IsDeleted= 0 
                                        END");
            migrationBuilder.Sql(@"CREATE PROC usp_CourseProgramMapping
                                    @Id int 
                                    AS 
                                    BEGIN 
                                        Select CourseCode, CLOCode, Description, Name, PLoSelectedIDNames, GSSelectedIDNames, PSSelectedIDNames, SDGSelectedIDNames, ARSelectedIDNames, ApprovedBy, ApprovedDate, ApprovedIp, IsApproved
                                                From dbo.CourseProgramMappings AS CPM
                                                Inner Join CourseLearnings AS CLO on CPM.CourseLearningId= CLO.Id
                                                Left Join CourseHistories AS CHis on CPM.CourseHistoryId= CHis.Id
                                                Left Join Instructors As Ins on CHis.InstructorId= Ins.Id
                                                Left Join Courses As C on CHis.CourseId= C.Id
                                                Left Join CourseClos CCLO on CLO.Id= CCLO.CLOId
                                                Where CPM.IsDeleted= 0 And (Ins.Id= @Id) 
                                    END ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE usp_CourseProgramMappings");
            migrationBuilder.Sql(@"DROP PROCEDURE usp_CourseProgramMapping");
        }
    }
}
