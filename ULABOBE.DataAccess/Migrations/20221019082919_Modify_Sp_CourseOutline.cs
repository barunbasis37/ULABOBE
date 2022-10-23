using Microsoft.EntityFrameworkCore.Migrations;

namespace ULABOBE.DataAccess.Migrations
{
    public partial class Modify_Sp_CourseOutline : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql(@"ALTER PROC [dbo].[usp_GetCourseOutLineWithParameter]  
                                        
                                    AS 
                                    BEGIN 
                                        SELECT COut.Id, COut.CourseHistoryId,UInfo.UserInfoId As UserInfoId, Ch.InstructorId,Cou.CourseCode,
										Cou.Title As Course_Name,Pro.Name As Program_Name, Pro.ProgramURMSId As Pro_URMS,
										Sec.SectionCode, Dept.Name As Department_Name, Dept.DepartmentCode As Depertment_Code,
										Sch.Name As School_Name, Sch.SchoolCode As School_Code,
										CuT.Name As CourseTypeName,Sem.Id As SemesterId,Sem.Code As SemesterCode, Sem.Name As SemesterName,
										Ins.Name As InstructorName,Ins.ShortCode As InstructorCode,
										COut.FileUploadUrl, COut.QueryId, COut.CreatedBy, 
										COut.CreatedIp, COut.CreatedDate, COut.UpdatedBy, COut.UpdatedIp, COut.UpdatedDate, COut.IsDeleted, 
										COut.FileExtension, COut.FileName
										FROM CourseOutlines As COut
										Inner Join CourseHistories As Ch on COut.CourseHistoryId=Ch.Id
										Inner Join Courses As Cou on Ch.CourseId=Cou.Id
										Inner Join Programs As Pro on Cou.ProgramId=Pro.Id
										Inner Join Departments As Dept on Pro.DepartmentId=Dept.Id
										Inner Join Schools As Sch on Dept.SchoolId=Sch.Id
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
