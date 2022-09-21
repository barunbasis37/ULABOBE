using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ULABOBE.Models.ViewModels
{
    public class ScheduleVM
    {
        public Schedule Schedule { get; set; }
        public IEnumerable<SelectListItem> WeekDayLists { get; set; }
        public IEnumerable<SelectListItem> TimeLists { get; set; }
    }
}
