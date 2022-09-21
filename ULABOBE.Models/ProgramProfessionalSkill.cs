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
    public class ProgramProfessionalSkill: Entity
    {
        [Required]
        [DisplayName("Program Code")]
        public int ProgramId { get; set; }
        [ForeignKey("ProgramId")]
        public ProgramData Program { get; set; }
        [Required]
        [DisplayName("PS Code")]
        public int PPSkillId { get; set; }
        [ForeignKey("PPSkillId")]
        public ProfessionalSkill ProfessionalSkill { get; set; }
        [Required]
        [StringLength(1000,ErrorMessage = "Description length cannot be longer than {0} characters.", MinimumLength = 1),DisplayName("Description")]
        public string Description { get; set; }

    }
}
