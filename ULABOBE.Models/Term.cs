using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ULABOBE.Models
{
    public class Term : Entity
    {
        [Required]
        [Display(Name = "Level"),StringLength(10,ErrorMessage = "Level not more than {0}",MinimumLength = 1)]
        public string Level { get; set; }
        //[Required]
        //[Display(Name = "Term")]
        //public int Term { get; set; }
    }
}
