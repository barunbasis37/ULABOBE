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
    public class WeekDay : Entity
    {
        [DisplayName("Day")]
        [Column(Order = 1)]
        public string Name { get; set; }
        [DisplayName("Short Day")]
        [StringLength(3, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        public string ShortName { get; set; }
        [DisplayName("Priority")]
        public int Priority { get; set; }
    }
}
