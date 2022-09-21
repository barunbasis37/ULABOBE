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
    public class CourseOutline : Entity
    {
        [Required]
        [DisplayName("Course Info")]
        public int CourseHistoryId { get; set; }
        [ForeignKey("CourseHistoryId")]
        public CourseHistory CourseHistory { get; set; }
        [StringLength(30)]
        public string FileName { get; set; }
        [StringLength(20)]
        public string FileExtension { get; set; }
        [StringLength(150)]
        public string FileUploadUrl { get; set; }


    }
}
