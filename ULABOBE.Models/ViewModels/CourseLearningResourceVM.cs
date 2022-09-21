using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ULABOBE.Models.ViewModels
{
    public class CourseLearningResourceVM
    {
        public CourseLearningResource CourseLearningResource { get; set; }
        public IEnumerable<SelectListItem> CourseHistoryLists { get; set; }
        public IEnumerable<SelectListItem> LearningResourceTypeLists { get; set; }
    }
}
