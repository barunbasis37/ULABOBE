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
    public class CourseGenericSkill: Entity
    {
        [Required]
        [DisplayName("Course Code")]
        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        public Course Course { get; set; }
        [Required]
        [DisplayName("Semester")]
        public int SemesterId { get; set; }
        [ForeignKey("SemesterId")]
        public virtual Semester Semester { get; set; }
        [Required]
        [DisplayName("Generic Skill Code")]
        public int GSId { get; set; }
        [ForeignKey("GSId")]
        public GenericSkill GenericSkill { get; set; }
        [Required]
        [StringLength(1000,ErrorMessage = "Description length cannot be longer than {0} characters.", MinimumLength = 1),DisplayName("Description")]
        public string Description { get; set; }

    }
}
