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
    public class AssessmentPattern : Entity
    {
        [Required]
        [DisplayName("Blooms Category")]
        public int BloomsCategoryId { get; set; }
        [ForeignKey("BloomsCategoryId")]
        public BloomsCategory BloomsCategory { get; set; }

        [Required]
        [DisplayName("Assessment Technique Weightages")]
        public int AssessmentTechWeightagesId { get; set; }
        [ForeignKey("AssessmentTechWeightagesId")]
        public AssessmentTechniqueWeightage AssessmentTechniqueWeightage { get; set; }

    }
}
