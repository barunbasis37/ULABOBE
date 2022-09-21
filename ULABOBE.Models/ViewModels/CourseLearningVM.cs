using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ULABOBE.Models.ViewModels
{
    public class CourseLearningVM
    {
        public CourseLearning CourseLearning { get; set; }
        public IEnumerable<SelectListItem> CourseLearningTypeLists { get; set; }
    }
}
