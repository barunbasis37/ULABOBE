using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ULABOBE.Models
{
    public class SDGContribution : Entity
    {
        [Required]
        [StringLength(50, ErrorMessage = "School Name cannot be longer than 50 characters.", MinimumLength = 1)]
        [System.ComponentModel.DataAnnotations.Schema.Index("IX_Code", IsUnique = true)]
        [Display(Name = "School Code")]
        public string SDGCode { get; set; }
        [Required]
        [Display(Name = "Priority Level")]
        public int Priority { get; set; }
        [Required]
        [Display(Name = "Active")]
        public bool IsActive { get; set; }
        
    }
}
