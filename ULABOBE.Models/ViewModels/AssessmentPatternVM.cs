using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ULABOBE.Models.ViewModels
{
    public class AssessmentPatternVM
    {
        public AssessmentPattern AssessmentPattern { get; set; }
        public IEnumerable<SelectListItem> CourseHistoryLists { get; set; }
        public IEnumerable<SelectListItem> BloomsCategoryLists { get; set; }
        public IEnumerable<SelectListItem> AssessmentTechWeightagesLists { get; set; }
        

    }
}
