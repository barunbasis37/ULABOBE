using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ULABOBE.Models.ViewModels
{
    public class MappingCourseProgramLOVM
    {
        public MappingCourseProgramLO MappingCourseProgramLO { get; set; }
        public IEnumerable<SelectListItem> CourseHistoryLists { get; set; }
        public IEnumerable<SelectListItem> ProgramLists { get; set; }
        public IEnumerable<SelectListItem> CourseLearningLists { get; set; }
        public IEnumerable<SelectListItem> ProgramPLOLists { get; set; }
        //public IEnumerable<SelectListItem> SemesterLists { get; set; }
        public IEnumerable<SelectListItem> CorrelationLists { get; set; }
    }
}
