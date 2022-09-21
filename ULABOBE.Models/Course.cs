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
    public class Course : Entity
    {
        
        [Required]
        [StringLength(35, ErrorMessage = "Course Title cannot be longer than {0} characters.", MinimumLength = 1), Display(Name = "Title")]
        public string Title { get; set; }
        [Required]
        [StringLength(500, ErrorMessage = "Course Code cannot be longer than {0} characters.", MinimumLength = 1), Display(Name = "Course Code")]
        public string CourseCode { get; set; }
        [Required]
        [DisplayName("Course Type")]
        public int CourseTypeId { get; set; }
        [ForeignKey("CourseTypeId")]
        public virtual CourseType CourseType { get; set; }
        [DisplayName("Program")]
        public int? ProgramId { get; set; }
        [ForeignKey("ProgramId")]
        public virtual ProgramData Program { get; set; }
        //[Required]
        //[DisplayName("Session/Year")]
        //public int SessionYearId { get; set; }
        //[ForeignKey("SessionYearId")]
        //public virtual SessionYear SessionYear { get; set; }
        [Required]
        [Display(Name = "Credit Hour")]
        [Column(TypeName = "decimal(18,4)")]
        public decimal CreditHour { get; set; }
        [Display(Name = "Prerequisite")]
        [StringLength(50, ErrorMessage = "Prerequisite cannot be longer than {0} characters.", MinimumLength = 1)]
        public string Prerequisite { get; set; }
        [Display(Name = "Evaluation Policy (Grading System)")]
        [StringLength(1000, ErrorMessage = "Evaluation Policy cannot be longer than {0} characters.", MinimumLength = 1)]
        public string EvaluationPolicy { get; set; }

        [Display(Name = "Teaching & Learning Strategy")]
        [StringLength(700, ErrorMessage = "Teaching & Learning Strategy cannot be longer than {0} characters.", MinimumLength = 1)]
        public string TeachingLearningStrategy { get; set; }

        [Display(Name = "Summary and Objective")]
        [StringLength(1000, ErrorMessage = "Course Summary cannot be longer than {0} characters.", MinimumLength = 1)]
        public string Summary { get; set; }

        //public virtual ICollection<CourseContentCLO> CourseContentCLOs { get; set; }
        //public virtual ICollection<CourseHistory> CourseHistory { get; set; }
    }
}
