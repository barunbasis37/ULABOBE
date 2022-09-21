using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ULABOBE.Models
{
    public class CourseClassDocument : Entity
    {
        [Required]
        [DisplayName("Course Info")]
        public int CourseHistoryId { get; set; }
        [ForeignKey("CourseHistoryId")]
        public CourseHistory CourseHistory { get; set; }

        [StringLength(30)]
        public string ClassMonitoringFileName { get; set; }
        [StringLength(20)]
        public string ClassMonitoringFileExtension { get; set; }
        [StringLength(150)]
        public string ClassMonitoringFileUploadUrl { get; set; }

        [StringLength(30)]
        public string CourseSessionFileName { get; set; }
        [StringLength(20)]
        public string CourseSessionExtension { get; set; }
        [StringLength(150)]
        public string CourseSessionFileUploadUrl { get; set; }

        [StringLength(30)]
        public string SemesterCourseFileName { get; set; }
        [StringLength(20)]
        public string SemesterCourseExtension { get; set; }
        [StringLength(150)]
        public string SemesterCourseFileUploadUrl { get; set; }

        [StringLength(30)]
        public string LessonPlanFileName { get; set; }
        [StringLength(20)]
        public string LessonPlanExtension { get; set; }
        [StringLength(150)]
        public string LessonPlanFileUploadUrl { get; set; }

        [StringLength(30)]
        public string CourseProgramFileName { get; set; }
        [StringLength(20)]
        public string CourseProgramExtension { get; set; }
        [StringLength(150)]
        public string CourseProgramFileUploadUrl { get; set; }

        [StringLength(30)]
        public string AttendanceSheetFileName { get; set; }
        [StringLength(20)]
        public string AttendanceSheetExtension { get; set; }
        [StringLength(150)]
        public string AttendanceSheetFileUploadUrl { get; set; }

    }
}
