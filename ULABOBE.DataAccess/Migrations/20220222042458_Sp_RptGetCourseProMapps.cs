using Microsoft.EntityFrameworkCore.Migrations;

namespace ULABOBE.DataAccess.Migrations
{
    public partial class Sp_RptGetCourseProMapps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"Create PROC usp_RptCourseProgramMappings
                                    As 
                                        BEGIN
                                            Select C.CourseCode, CLO.CLOCode, CCLO.Description, Ins.Name, CPM.PLoSelectedIDNames, CPM.GSSelectedIDNames, CPM.PSSelectedIDNames, CPM.SDGSelectedIDNames, CPM.ARSelectedIDNames, CPM.IsApproved
                                                From dbo.CourseProgramMappings AS CPM
                                                Inner Join CourseLearnings AS CLO on CPM.CourseLearningId= CLO.Id
                                                Left Join CourseHistories AS CHis on CPM.CourseHistoryId= CHis.Id
                                                Left Join Instructors As Ins on CHis.InstructorId= Ins.Id
                                                Left Join Courses As C on CHis.CourseId= C.Id
                                                Left Join CourseClos CCLO on CLO.Id= CCLO.CLOId
                                                Where CPM.IsDeleted= 0 
                                        END");
            migrationBuilder.Sql(@"CREATE PROC usp_RptCourseProgramMapping
                                    @Id int 
                                    AS 
                                    BEGIN 
                                        Select C.CourseCode, CLO.CLOCode, CCLO.Description, Ins.Name, CPM.PLoSelectedIDNames, CPM.GSSelectedIDNames, CPM.PSSelectedIDNames, CPM.SDGSelectedIDNames, CPM.ARSelectedIDNames, CPM.IsApproved
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
            migrationBuilder.Sql(@"DROP PROCEDURE usp_RptCourseProgramMappings");
            migrationBuilder.Sql(@"DROP PROCEDURE usp_RptCourseProgramMapping");
        }
    }
}
