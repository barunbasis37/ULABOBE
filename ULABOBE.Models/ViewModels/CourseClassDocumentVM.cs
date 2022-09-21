using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ULABOBE.Models.ViewModels
{
    public class CourseClassDocumentVM
    {
        public CourseClassDocument CourseClassDocument { get; set; }
        public IEnumerable<SelectListItem> CourseHistoryLists { get; set; }

        public IFormFile MonitorImage { set; get; }
        public IFormFile SessionImage { set; get; }
        public IFormFile SemesterCourseImage { set; get; }
        public IFormFile LessionImage { set; get; }
        public IFormFile CourseProMapImage { set; get; }
        public IFormFile AttendanceImage { set; get; }

    }
}
