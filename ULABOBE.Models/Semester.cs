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
    public class Semester : Entity
    {
        [StringLength(20, ErrorMessage = "Semester cannot be longer than {0} characters.", MinimumLength = 1), Display(Name = "Semester")]
        public string Name { get; set; }
        [DisplayName("Term")]
        public int? TermId { get; set; }
        [ForeignKey("TermId")]
        public Term Term { get; set; }
        [DisplayName("Session")]
        public int? SessionId { get; set; }
        [ForeignKey("SessionId")]
        public Session Session { get; set; }
        [Required]
        [Display(Name = "Code")]
        public int Code { get; set; }
        [Required]
        [Display(Name = "Start DateTime")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime StartDateTime { get; set; }
        [Required]
        [Display(Name = "End DateTime")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime EndDateTime { get; set; }

    }
}
