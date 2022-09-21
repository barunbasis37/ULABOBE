using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ULABOBE.Models.ViewModels
{
    public class CourseVM
    {
        public Course Course { get; set; }
        public IEnumerable<SelectListItem> CourseTypeLists { get; set; }
        public IEnumerable<SelectListItem> ProgramLists { get; set; }
    }
}
