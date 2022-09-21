using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ULABOBE.Models
{
    public class Section : Entity
    {
        [Required]
        
        [Display(Name = "Section Code")]
        public int SectionCode { get; set; }
        [Required]
        [Display(Name = "Priority Level")]
        public int Priority { get; set; }
        [Required]
        [Display(Name = "Active")]
        public bool IsActive { get; set; }
    }
}
