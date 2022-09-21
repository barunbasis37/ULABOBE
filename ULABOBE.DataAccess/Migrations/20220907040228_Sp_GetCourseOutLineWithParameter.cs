using Microsoft.EntityFrameworkCore.Migrations;

namespace ULABOBE.DataAccess.Migrations
{
    public partial class Sp_GetCourseOutLineWithParameter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROC usp_GetCourseOutLineWithParameter   
                                        
                                    AS 
                                    BEGIN 
                                        SELECT COut.Id, COut.CourseHistoryId,UInfo.UserInfoId As UserInfoId, Ch.InstructorId,Cou.CourseCode,Sec.SectionCode,
                                        CuT.Name As CourseTypeName,Sem.Id As SemesterId,Sem.Code As SemesterCode, Sem.Name As SemesterName,
                                        Ins.Name As InstructorName,Ins.ShortCode As InstructorCode,
                                        COut.FileUploadUrl, COut.QueryId, COut.CreatedBy, 
                                        COut.CreatedIp, COut.CreatedDate, COut.UpdatedBy, COut.UpdatedIp, COut.UpdatedDate, COut.IsDeleted, 
                                        COut.FileExtension, COut.FileName
                                        FROM CourseOutlines As COut
                                        Inner Join CourseHistories As Ch on COut.CourseHistoryId=Ch.Id
                                        Inner Join Courses As Cou on Ch.CourseId=Cou.Id
                                        Inner Join CourseTypes As CuT on Cou.CourseTypeId=CuT.Id
                                        Inner Join Sections As Sec on Ch.SectionId=Sec.Id
                                        Inner Join Instructors As Ins on Ch.InstructorId=Ins.Id
                                        Inner Join UserInfoChecks As UInfo on Ins.ShortCode=UInfo.ShortCode
                                        Inner Join Semesters As Sem on Ch.SemesterId=Sem.Id
                                        where COut.IsDeleted=0	
																			
                                    End");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE usp_GetCourseOutLineWithParameter");
        }
    }
}
