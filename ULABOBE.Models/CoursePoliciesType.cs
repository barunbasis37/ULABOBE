using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ULABOBE.Models
{
    public class CoursePolicyType: Entity
    {
        [Required]
        [StringLength(150, ErrorMessage = "COurse Policy Type cannot be longer than {0} characters.", MinimumLength = 1), Display(Name = "Type")]
        public string Name { get; set; }
        [Display(Name = "Priority")]
        public int Priority { get; set; }
    }
}
