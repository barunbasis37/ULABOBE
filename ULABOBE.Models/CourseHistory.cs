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
    public class CourseHistory : Entity
    {
        
        [Required]
        [Display(Name = "Course")]
        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        public Course Course { get; set; }
        public int SectionId { get; set; }
        [ForeignKey("SectionId")]
        public Section Section { get; set; }
        [Required]
        [DisplayName("Semester")]
        public int SemesterId { get; set; }
        [ForeignKey("SemesterId")]
        public virtual Semester Semester { get; set; }
        [DisplayName("Instructor")]
        public int InstructorId { get; set; }
        [ForeignKey("InstructorId")]
        public Instructor Instructor { get; set; }
        [Display(Name = "Contact Hours")]
        [StringLength(300, ErrorMessage = "Contact Hours cannot be longer than {0} characters.", MinimumLength = 1)]
        public string SchedulesNames { get; set; }
        [StringLength(20)]
        public string ScheduleIDs { get; set; }
        [Required]
        [Display(Name = "CIE Marks")]
        public int CIEMarks { get; set; }
        [Required]
        [Display(Name = "SEE Marks")]
        public int SEEMarks { get; set; }
        [Display(Name = "Total Marks")]
        public int TotalMarks { get; set; }



    }
}
