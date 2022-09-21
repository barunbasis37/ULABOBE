using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace ULABOBE.Models.ViewModels
{
    public class DepartmentVM
    {
        public Department Department { get; set; }
        public IEnumerable<SelectListItem> SchoolList { get; set; }
    }
}
