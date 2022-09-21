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
    public class CourseContent : Entity
    {
        [Required]
        [DisplayName("Course Info")]
        public int CourseHistoryId { get; set; }
        [ForeignKey("CourseHistoryId")]
        public CourseHistory CourseHistory { get; set; }

        //[Required]
        //[DisplayName("Semester")]
        //public int SemesterId { get; set; }
        //[ForeignKey("SemesterId")]
        //public virtual Semester Semester { get; set; }

        [Required]
        [StringLength(1000,ErrorMessage = "Topic length cannot be longer than {0} characters.", MinimumLength = 1),DisplayName("Description")]
        public string Topic { get; set; }
        [Required]
        [DisplayName("Number of Session")]
        public int SessionQuantity { get; set; }
        [StringLength(50)]
        public string CLoSelectedIDs { get; set; }
        [DisplayName("CLO Covered")]
        [StringLength(100)]
        public string CLoSelectedIDNames { get; set; }

        [StringLength(50)]
        public string ARSelectedIDs { get; set; }
        [DisplayName("AR Covered")]
        [StringLength(100)]
        public string ARSelectedIDNames { get; set; }
        //public virtual ICollection<CourseContentCLO> CourseContentCLOs { get; set; }

    }
}
