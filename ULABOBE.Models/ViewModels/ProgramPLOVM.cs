using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ULABOBE.Models.ViewModels
{
    public class ProgramPLOVM
    {
        public ProgramPLO ProgramPLO { get; set; }
        public IEnumerable<SelectListItem> ProgramLists { get; set; }
        public IEnumerable<SelectListItem> ProgramLearningLists { get; set; }
    }
}
