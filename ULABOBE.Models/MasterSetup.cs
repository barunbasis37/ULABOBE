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
    public class MasterSetup: Entity
    {
        [DisplayName("Program")]
        public int ProgramId { get; set; }
        [ForeignKey("ProgramId")]
        public ProgramData ProgramData { get; set; }
        [Required]
        [Display(Name = "Start DateTime")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime StartDateTime { get; set; }
        [Required]
        [Display(Name = "End DateTime")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime EndDateTime { get; set; }
        [DisplayName("Semester")]
        public int SemesterId { get; set; }
        [ForeignKey("SemesterId")]
        public Semester Semester { get; set; }


    }
}
