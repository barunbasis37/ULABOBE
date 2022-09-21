using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ULABOBE.Models.ViewModels
{
    public class DepartmentSDGContributionVM
    {
        public DepartmentSDGContribution DepartmentSDGContribution { get; set; }
        public IEnumerable<SelectListItem> DepartmentLists { get; set; }
        public IEnumerable<SelectListItem> SDGContributionLists { get; set; }
        public IEnumerable<SelectListItem> SemesterLists { get; set; }
        //public IEnumerable<SelectListItem> TermLevelLists { get; set; }
    }
}
