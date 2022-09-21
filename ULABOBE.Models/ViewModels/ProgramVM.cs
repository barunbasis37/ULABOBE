using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace ULABOBE.Models.ViewModels
{
    public class ProgramVM
    {
        public ProgramData Program { get; set; }
        public IEnumerable<SelectListItem> DepartmentList { get; set; }
    }
}
