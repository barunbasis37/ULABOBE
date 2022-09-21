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
    public class MappingCourseProgramLO: Entity
    {
        [Required]
        [DisplayName("Course Info")]
        public int CourseHistoryId { get; set; }
        [ForeignKey("CourseHistoryId")]
        public CourseHistory CourseHistory { get; set; }
        [DisplayName("CLO Code")]
        public int CourseLearningId { get; set; }
        [ForeignKey("CourseLearningId")]
        public CourseLearning CourseLearning { get; set; }
        [Required]
        [DisplayName("Program Code")]
        public int ProgramId { get; set; }
        [ForeignKey("ProgramId")]
        public ProgramData Program { get; set; }
        [DisplayName("PLO Code")]
        public int ProgramPLOId { get; set; }
        [ForeignKey("ProgramPLOId")]
        public ProgramPLO ProgramPLO { get; set; }
        [Required]
        [DisplayName("Correlation")]
        public int CorrelationId { get; set; }
        [ForeignKey("CorrelationId")]
        public Correlation Correlation { get; set; }

    }
}
