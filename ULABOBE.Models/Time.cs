using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ULABOBE.Models
{
    public class Time: Entity
    {
        [Required]
        [DisplayName("Time"), StringLength(10)]
        public string Name { get; set; }
    }
}
