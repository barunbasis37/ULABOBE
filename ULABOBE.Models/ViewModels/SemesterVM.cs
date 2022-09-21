using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ULABOBE.Models.ViewModels
{
    public class SemesterVM
    {
        public Semester Semester { get; set; }
        public IEnumerable<SelectListItem> TermLists { get; set; }
        public IEnumerable<SelectListItem> SessionLists { get; set; }
    }
}
