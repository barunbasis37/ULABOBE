using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ULABOBE.Models
{
    public class CourseType :Entity
    {
        
        [Required]
        [StringLength(150, ErrorMessage = "Course Type cannot be longer than {0} characters.", MinimumLength = 1), Display(Name = "Type")]
        public string Name { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "Course Type Code cannot be longer than {0} characters.", MinimumLength = 1), Display(Name = "Code")]
        public string Code { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Description cannot be longer than {0} characters.", MinimumLength = 1), Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name="Priority")]
        public int Priority { get; set; }

    }
}
