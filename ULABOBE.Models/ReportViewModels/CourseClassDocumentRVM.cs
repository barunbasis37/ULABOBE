using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ULABOBE.Models.ReportViewModels
{
    public class CourseClassDocumentRVM
    {
        public int Id { get; set; }
        public int CourseHistoryId { get; set; }
        public string UserInfoId { get; set; }
        public int InstructorId { get; set; }
        public string CourseCode { get; set; }
        public string SectionCode { get; set; }
        public int SemesterId { get; set; }
        public int SemesterCode { get; set; }
        public string SemesterName { get; set; }
        public string InstructorName { get; set; }
        public string InstructorCode { get; set; }
        public Guid QueryId { get; set; }

        public string CreatedBy { get; set; }
        public string CreatedIp { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedIp { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public int IsDeleted { get; set; }

        public string ClassMonitoringFileName { get; set; }        
        public string ClassMonitoringFileExtension { get; set; }        
        public string ClassMonitoringFileUploadUrl { get; set; }

        public string CourseSessionFileName { get; set; }
        public string CourseSessionExtension { get; set; }
        public string CourseSessionFileUploadUrl { get; set; }


        public string SemesterCourseFileName { get; set; }
        public string SemesterCourseExtension { get; set; }        
        public string SemesterCourseFileUploadUrl { get; set; }

        
        public string LessonPlanFileName { get; set; }        
        public string LessonPlanExtension { get; set; }        
        public string LessonPlanFileUploadUrl { get; set; }

        
        public string CourseProgramFileName { get; set; }        
        public string CourseProgramExtension { get; set; }        
        public string CourseProgramFileUploadUrl { get; set; }

       
        public string AttendanceSheetFileName { get; set; }
        public string AttendanceSheetExtension { get; set; }
        public string AttendanceSheetFileUploadUrl { get; set; }



    }
}
