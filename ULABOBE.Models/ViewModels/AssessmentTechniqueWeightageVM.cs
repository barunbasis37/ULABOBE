using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ULABOBE.Models.ViewModels
{
    public class AssessmentTechniqueWeightageVM
    {
        public AssessmentTechniqueWeightage AssessmentTechniqueWeightage { get; set; }
        public IEnumerable<SelectListItem> CourseHistoryLists { get; set; }
        public IEnumerable<SelectListItem> AssessmentTypeLists { get; set; }
    }
}
