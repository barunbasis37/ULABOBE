using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ULABOBE.Models.ViewModels
{
    public class CourseGenericSkillVM
    {
        public CourseGenericSkill CourseGenericSkill { get; set; }
        public IEnumerable<SelectListItem> CourseLists { get; set; }
        public IEnumerable<SelectListItem> SemesterLists { get; set; }
        public IEnumerable<SelectListItem> GenericSkillLists { get; set; }
    }
}
