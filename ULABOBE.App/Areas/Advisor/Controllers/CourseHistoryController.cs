using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ULABOBE.DataAccess.Repository.IRepository;

namespace ULABOBE.App.Areas.Advisor.Controllers
{
    [Area("Advisor")]
    public class CourseHistoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private UniqueSetup uniqueSetup;
        private string userName;

        public CourseHistoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //userName = User.Identity.Name;
            uniqueSetup = new UniqueSetup(_unitOfWork);
        }
        [HttpGet]
        [Route("~/Advisor/[controller]/[action]")]
        public IActionResult Index()
        {

            GetLatestSemester();
            return View();
        }

        protected virtual void GetLatestSemester()
        {
            ViewBag.InstructorInfo = uniqueSetup.GetInstructor(User.Identity.Name).Name + "(" + uniqueSetup.GetInstructor(User.Identity.Name).ShortCode + ")";
            ViewBag.Semester = uniqueSetup.GetCurrentSemester().Name + "(" + uniqueSetup.GetCurrentSemester().Code + ")";
        }
       
    }
}
