using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using ULABOBE.App.Areas.Faculty.Controllers;
using ULABOBE.DataAccess.Repository.IRepository;
using ULABOBE.Models;
using ULABOBE.Models.ViewModels;
using ULABOBE.Utility;

namespace ULABOBE.AppOnline.Areas.Faculty.Controllers
{
    [Area("Faculty")]
    [Authorize]
    public class CourseLearningResourceController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private UniqueSetup uniqueSetup;
        private string userName;
        public CourseLearningResourceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            uniqueSetup = new UniqueSetup(_unitOfWork);
        }
        [Authorize(Roles = SD.Role_Faculty)]
        public IActionResult Index()
        {
            GetLatestSemester();
            return View();
        }

        protected virtual void GetLatestSemester()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole(SD.Role_Faculty))
            {
                UserInfoCheck userInfoCheck = _unitOfWork.UserInfoCheck.GetFirstOrDefault(user => user.UserInfoId == User.Identity.Name);
                ViewBag.InstructorInfo = userInfoCheck.Name + " (" + userInfoCheck.ShortCode + ")";
                int maxSemester = _unitOfWork.Semester.GetAll().Max(mS => mS.Id);
                Semester semester = _unitOfWork.Semester.Get(maxSemester);
                ViewBag.Semester = "Semester: " + semester.Name + "(" + semester.Code + ")";
            }
        }
        [Authorize(Roles = SD.Role_Faculty)]
        public IActionResult Upsert(int? id)
        {
            GetLatestSemester();
            CourseLearningResourceVM courseLearningResourceVM = new CourseLearningResourceVM()
            {
                CourseLearningResource = new CourseLearningResource(),
                CourseHistoryLists = _unitOfWork.CourseHistory.GetAll(includeProperties: "Course,Semester,Section,Instructor", filter: ch => ch.SemesterId == uniqueSetup.GetCurrentSemester().Id && ch.InstructorId == uniqueSetup.GetInstructor(User.Identity.Name).Id).Select(i => new SelectListItem
                {
                    Text = i.Course.CourseCode + "(" + i.Section.SectionCode + ")-" + i.Instructor.ShortCode,
                    Value = i.Id.ToString()
                }),
                LearningResourceTypeLists = _unitOfWork.LearningResourceType.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
            };
            if (id == null)
            {
                //this is for create
                return View(courseLearningResourceVM);
            }
            //this is for edit
            courseLearningResourceVM.CourseLearningResource = _unitOfWork.CourseLearningResource.Get(id.GetValueOrDefault());
            if (courseLearningResourceVM.CourseLearningResource == null)
            {
                return NotFound();
            }
            return View(courseLearningResourceVM);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_Faculty)]
        public IActionResult Upsert(CourseLearningResourceVM courseLearningResourceVM)
        {
            GetLatestSemester();
            if (ModelState.IsValid)
            {
                if (courseLearningResourceVM.CourseLearningResource.Id == 0)
                {
                    courseLearningResourceVM.CourseLearningResource.QueryId = Guid.NewGuid();
                    
                    courseLearningResourceVM.CourseLearningResource.CreatedDate = DateTime.Now;
                    courseLearningResourceVM.CourseLearningResource.CreatedBy = User.Identity.Name;
                    courseLearningResourceVM.CourseLearningResource.CreatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    courseLearningResourceVM.CourseLearningResource.UpdatedDate = DateTime.Now;
                    courseLearningResourceVM.CourseLearningResource.UpdatedBy = "000000";
                    courseLearningResourceVM.CourseLearningResource.UpdatedIp = "0.0.0.0";
                    courseLearningResourceVM.CourseLearningResource.IsDeleted = false;
                    _unitOfWork.CourseLearningResource.Add(courseLearningResourceVM.CourseLearningResource);

                }
                else
                {
                    courseLearningResourceVM.CourseLearningResource.UpdatedDate = DateTime.Now;
                    //courseLearningResource.UpdatedBy = User.Identity.Name;
                    //courseLearningResource.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    courseLearningResourceVM.CourseLearningResource.UpdatedBy = User.Identity.Name;
                    courseLearningResourceVM.CourseLearningResource.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    courseLearningResourceVM.CourseLearningResource.IsDeleted = false;
                    _unitOfWork.CourseLearningResource.Update(courseLearningResourceVM.CourseLearningResource);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            courseLearningResourceVM.CourseHistoryLists = _unitOfWork.CourseHistory
                .GetAll(includeProperties: "Course,Semester,Section,Instructor",
                    filter: ch => ch.SemesterId == uniqueSetup.GetCurrentSemester().Id && ch.InstructorId == uniqueSetup.GetInstructor(User.Identity.Name).Id).Select(i => new SelectListItem
                {
                    Text = i.Course.CourseCode + "(" + i.Section.SectionCode + ")-" + i.Instructor.ShortCode + ")",
                        Value = i.Id.ToString()
                });
            courseLearningResourceVM.LearningResourceTypeLists = _unitOfWork.LearningResourceType.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            return View(courseLearningResourceVM);
        }

        #region API Calls

        [HttpGet]
        [Authorize(Roles = SD.Role_Faculty)]
        public IActionResult GetAll()
        {
            int maxSemester = _unitOfWork.Semester.GetAll().Max(mS => mS.Id);
            var userInfoCheck = _unitOfWork.UserInfoCheck.GetFirstOrDefault(user => user.UserInfoId == User.Identity.Name);
            var instructor = _unitOfWork.Instructor.GetFirstOrDefault(user => user.ShortCode == userInfoCheck.ShortCode);
            var allObj = _unitOfWork.CourseLearningResource.GetAll(filter: cLRes => cLRes.CourseHistory.SemesterId == maxSemester && cLRes.CourseHistory.InstructorId == uniqueSetup.GetInstructor(User.Identity.Name).Id, includeProperties: "CourseHistory,CourseHistory.Course,CourseHistory.Semester,CourseHistory.Section,CourseHistory.Instructor,LearningResourceType");
            return Json(new { data = allObj });
        }
        
        //[HttpGet]
        //public IActionResult GetDAll(int? courseId)
        //{
        //    var allObj = _unitOfWork.CourseLearningResource.GetAll(c => c.CourseId == courseId);
        //    return Json(new { data = allObj });
        //}



        #endregion
    }
}
