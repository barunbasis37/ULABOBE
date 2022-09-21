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
    public class DepartmentLAR : Entity
    {
        [Required]
        [DisplayName("Department")]
        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }
        //[Required]
        //[DisplayName("Level/Term")]
        //public int LevelTermId { get; set; }
        //[ForeignKey("LevelTermId")]
        //public virtual Term Term { get; set; }
        [Required]
        [DisplayName("Semesterr")]
        public int SemesterId { get; set; }
        [ForeignKey("SemesterId")]
        public virtual Semester Semester { get; set; }
        [Required]
        [DisplayName("LAR Code")]
        public int LARId { get; set; }
        [ForeignKey("LARId")]
        public LearningAssessmentRubric LearningAssessmentRubric { get; set; }
        [Required]
        [StringLength(1000,ErrorMessage = "Description length cannot be longer than {0} characters.", MinimumLength = 1),DisplayName("Description")]
        public string Description { get; set; }

    }
}
