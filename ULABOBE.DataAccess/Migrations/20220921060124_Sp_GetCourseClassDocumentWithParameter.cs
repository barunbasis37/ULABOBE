using Microsoft.EntityFrameworkCore.Migrations;

namespace ULABOBE.DataAccess.Migrations
{
    public partial class Sp_GetCourseClassDocumentWithParameter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROC usp_GetCourseClassDocumentWithParameter   
                                        
                                    AS 
                                    BEGIN 
                                        SELECT CCDoc.Id, CCDoc.CourseHistoryId,UInfo.UserInfoId As UserInfoId, Ch.InstructorId,Cou.CourseCode, Pro.ProgramCode, Pro.ProgramURMSId,Sec.SectionCode,
                                        CuT.Name As CourseTypeName,Sem.Id As SemesterId,Sem.Code As SemesterCode, Sem.Name As SemesterName,
                                        Ins.Name As InstructorName,Ins.ShortCode As InstructorCode,
                                        CCDoc.ClassMonitoringFileName, CCDoc.QueryId, CCDoc.CreatedBy, 
                                        CCDoc.CreatedIp, CCDoc.CreatedDate, CCDoc.UpdatedBy, CCDoc.UpdatedIp, CCDoc.UpdatedDate, CCDoc.IsDeleted, 
                                        CCDoc.ClassMonitoringFileExtension, CCDoc.ClassMonitoringFileName, 
                                        CCDoc.CourseSessionExtension, CCDoc.CourseSessionFileName, 
                                        CCDoc.SemesterCourseExtension, CCDoc.SemesterCourseFileName, 
                                        CCDoc.LessonPlanExtension, CCDoc.LessonPlanFileName, 
                                        CCDoc.CourseProgramExtension, CCDoc.CourseProgramFileName, 
                                        CCDoc.AttendanceSheetExtension, CCDoc.AttendanceSheetFileName
                                        FROM CourseClassDocuments As CCDoc
                                        Inner Join CourseHistories As Ch on CCDoc.CourseHistoryId=Ch.Id
                                        Inner Join Courses As Cou on Ch.CourseId=Cou.Id
										Inner Join Programs As Pro on Cou.ProgramId=Pro.Id
                                        Inner Join CourseTypes As CuT on Cou.CourseTypeId=CuT.Id
                                        Inner Join Sections As Sec on Ch.SectionId=Sec.Id
                                        Inner Join Instructors As Ins on Ch.InstructorId=Ins.Id
                                        Inner Join UserInfoChecks As UInfo on Ins.ShortCode=UInfo.ShortCode
                                        Inner Join Semesters As Sem on Ch.SemesterId=Sem.Id
                                        where CCDoc.IsDeleted=0	
																			
                                    End");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE usp_GetCourseClassDocumentWithParameter");
        }
    }
}
