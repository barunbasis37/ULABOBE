using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ULABOBE.Models.ReportViewModels
{
    public class CourseOutlineRVM
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
        public string FileName { get; set; }
        
        public string FileExtension { get; set; }
        
        public string FileUploadUrl { get; set; }



    }
}
