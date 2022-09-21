using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ULABOBE.Models.ViewModels
{
    public class CourseContentVM
    {
        public CourseContent CourseContent { get; set; }
        public IEnumerable<SelectListItem> CourseHistoryLists { get; set; }
        //public IEnumerable<SelectListItem> SemesterLists { get; set; }
        public IEnumerable<SelectListItem> CourseLearningLists { get; set; }
        public IEnumerable<SelectListItem> LearningAssessmentRubricLists { get; set; }
        public string[] CourseLearningSelectedIdArray { get; set; }
        public string[] ARSelectedIDArray { get; set; }
    }
}
