using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ULABOBE.Models
{
    public class Session : Entity
    {
        [Required]
        [Display(Name = "Session"), StringLength(4, ErrorMessage = "Session not more than {0}",MinimumLength = 4)]
        public string Name { get; set; }
        //[Required]
        //[Display(Name = "Year")]
        //public int Year { get; set; }


        

    }
}
