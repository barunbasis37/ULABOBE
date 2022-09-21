using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ULABOBE.Models.ViewModels
{
    public class DepartmentGSkillVM
    {
        public DepartmentGSkill DepartmentGSkill { get; set; }
        public IEnumerable<SelectListItem> DepartmentLists { get; set; }
        public IEnumerable<SelectListItem> GenericSkillLists { get; set; }
        public IEnumerable<SelectListItem> SemesterLists { get; set; }
        //public IEnumerable<SelectListItem> TermLevelLists { get; set; }
    }
}
