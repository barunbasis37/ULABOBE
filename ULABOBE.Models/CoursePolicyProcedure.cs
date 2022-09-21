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
    public class CoursePolicyProcedure : Entity
    {
        [Required]
        [DisplayName("Course Info")]
        public int CourseHistoryId { get; set; }
        [ForeignKey("CourseHistoryId")]
        public CourseHistory CourseHistory { get; set; }

        [Required]
        [DisplayName("Type")]
        public int CPolicyTypeIdId { get; set; }
        [ForeignKey("CPolicyTypeIdId")]
        public virtual CoursePolicyType CoursePolicyType { get; set; }

        [Required]
        [StringLength(800,ErrorMessage = "Description cannot be longer than {0} characters.", MinimumLength = 1),DisplayName("Description")]
        public string Description { get; set; }
        

    }
}
