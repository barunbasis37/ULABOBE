using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace ULABOBE.Models.ViewModels
{
    public class InstructorVM
    {
        public Instructor Instructor { get; set; }
        public IEnumerable<SelectListItem> DesignationList { get; set; }
    }
}
