using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ULABOBE.Models
{
    public class Correlation: Entity
    {
        [Required, DisplayName("Mark")]
        public int Code { get; set; }
        [Required, DisplayName("Stage")]
        public string Stage { get; set; }
        [Required, DisplayName("Priority")]
        public int Priority { get; set; }
        [Required, DisplayName("Is Active")]
        public bool IsActive { get; set; }
    }
}
