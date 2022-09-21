using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ULABOBE.Models.ViewModels
{
    public class ProgramProfessionalSkillVM
    {
        public ProgramProfessionalSkill ProgramProfessionalSkill { get; set; }
        public IEnumerable<SelectListItem> ProgramLists { get; set; }
        public IEnumerable<SelectListItem> ProfessionalSkillLists { get; set; }
    }
}
