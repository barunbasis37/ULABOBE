using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ULABOBE.Models
{
    public class Instructor: Entity
    {
        
        [Required]
        [StringLength(300, ErrorMessage = "Instruction cannot be longer than {0} characters.", MinimumLength = 1), Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "Short Code cannot be longer than {0} characters.", MinimumLength = 1), Display(Name = "Short-Code")]
        public string ShortCode { get; set; }
        [Display(Name = "Designation")]
        public int DesignationId { get; set; }
        [ForeignKey("DesignationId")]
        public virtual Designation Designation { get; set; }
        
    }
}
