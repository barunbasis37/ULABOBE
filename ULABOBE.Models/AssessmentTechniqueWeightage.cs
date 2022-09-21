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
    public class AssessmentTechniqueWeightage : Entity
    {
        [Required]
        [StringLength(100, ErrorMessage = "Strategy length cannot be longer than {0} characters.", MinimumLength = 1)]
        [DisplayName("Strategy")]
        public string Strategy { get; set; }
        [Required]
        [DisplayName("Percentage")]
        public int Percentage { get; set; }
        [Required]
        [DisplayName("Priority")]
        public int Priority { get; set; }
        [Required]
        [DisplayName("Course Info")]
        public int CourseHistoryId { get; set; }
        [ForeignKey("CourseHistoryId")]
        public CourseHistory CourseHistory { get; set; }
        [DisplayName("Type")]
        public int AssessmentTypeId { get; set; }
        [ForeignKey("AssessmentTypeId")]
        public AssessmentType AssessmentType { get; set; }
    }
}
