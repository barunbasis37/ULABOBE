using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ULABOBE.Models.ViewModels
{
    public class CoursePolicyProcedureVM
    {
        public CoursePolicyProcedure CoursePolicyProcedure { get; set; }
        public IEnumerable<SelectListItem> CourseHistoryLists { get; set; }
        //public IEnumerable<SelectListItem> SemesterLists { get; set; }
        public IEnumerable<SelectListItem> CoursePolicyTypeLists { get; set; }
        
    }
}
