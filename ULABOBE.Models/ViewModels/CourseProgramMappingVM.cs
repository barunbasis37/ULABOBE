using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ULABOBE.Models.ViewModels
{
    public class CourseProgramMappingVM
    {
        public CourseProgramMapping CourseProgramMapping { get; set; }
        public IEnumerable<SelectListItem> CourseHistoryLists { get; set; }
        public IEnumerable<SelectListItem> CourseLearningLists { get; set; }
        public IEnumerable<SelectListItem> ProgramLearningLists { get; set; }
        public IEnumerable<SelectListItem> GenericSkillLists { get; set; }
        public IEnumerable<SelectListItem> ProfessionalSkillLists { get; set; }
        public IEnumerable<SelectListItem> SDGLists { get; set; }
        public IEnumerable<SelectListItem> LearningAssessmentRubricLists { get; set; }
        public string[] PLoSelectedIDArray { get; set; }
        public string[] GSSelectedIDArray { get; set; }
        public string[] PSSelectedIDArray { get; set; }
        public string[] SDGSelectedIDArray { get; set; }
        public string[] ARSelectedIDArray { get; set; }
    }
}
