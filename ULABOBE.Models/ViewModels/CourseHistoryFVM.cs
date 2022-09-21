using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ULABOBE.Models.ViewModels
{
    public class CourseHistoryFVM
    {
        public CourseHistory CourseHistory { get; set; }
        public IEnumerable<SelectListItem> CourseLists { get; set; }
        
        public IEnumerable<SelectListItem> ScheduleLists { get; set; }
        public IEnumerable<SelectListItem> SectionLists { get; set; }
        public string[] ScheduleSelectedIdArray { get; set; }

    }
}
