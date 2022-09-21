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
    public class CourseLearningResource : Entity
    {
        [Required]
        [DisplayName("Course Info")]
        public int CourseHistoryId { get; set; }
        [ForeignKey("CourseHistoryId")]
        public CourseHistory CourseHistory { get; set; }

        [Required]
        [DisplayName("Learning Resource Type")]
        public int LearningResourceTypeId { get; set; }
        [ForeignKey("LearningResourceTypeId")]
        public LearningResourceType LearningResourceType { get; set; }

        [Required]
        [StringLength(1000,ErrorMessage = "Book length cannot be longer than {0} characters.", MinimumLength = 1),DisplayName("Book Details")]
        public string BookInfo { get; set; }

        [Display(Name = "IsActive")]
        public bool IsActive { get; set; }


    }
}
