using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using ULABOBE.App.Areas.Admin.Controllers;
using ULABOBE.DataAccess.Repository.IRepository;
using ULABOBE.Models;
using ULABOBE.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using ULABOBE.Utility;

namespace ULABOBE.AppOnline.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CourseLearningResourceController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseLearningResourceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Index()
        {
            return View();
        }
        private UniqueSetup uniqueSetup;
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Upsert(int? id)
        {
            uniqueSetup = new UniqueSetup(_unitOfWork);


            ViewBag.Semester = uniqueSetup.GetCurrentSemester().Name + "(" + uniqueSetup.GetCurrentSemester().Code + ")";
            CourseLearningResourceVM courseLearningResourceVM = new CourseLearningResourceVM()
            {
                CourseLearningResource = new CourseLearningResource(),
                CourseHistoryLists = _unitOfWork.CourseHistory.GetAll(includeProperties: "Course,Semester,Section,Instructor", filter: ch => ch.SemesterId == uniqueSetup.GetCurrentSemester().Id).Select(i => new SelectListItem
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
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Upsert(CourseLearningResourceVM courseLearningResourceVM)
        {

            if (ModelState.IsValid)
            {
                if (courseLearningResourceVM.CourseLearningResource.Id == 0)
                {
                    courseLearningResourceVM.CourseLearningResource.QueryId = Guid.NewGuid();
                    
                    courseLearningResourceVM.CourseLearningResource.CreatedDate = DateTime.Now;
                    courseLearningResourceVM.CourseLearningResource.CreatedBy = User.Identity.Name;
                    courseLearningResourceVM.CourseLearningResource.CreatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    courseLearningResourceVM.CourseLearningResource.UpdatedDate = DateTime.Now;
                    courseLearningResourceVM.CourseLearningResource.UpdatedBy = "-";
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
                    filter: ch => ch.SemesterId == uniqueSetup.GetCurrentSemester().Id).Select(i => new SelectListItem
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
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.CourseLearningResource.GetAll(includeProperties: "CourseHistory,CourseHistory.Course,CourseHistory.Semester,CourseHistory.Section,CourseHistory.Instructor,LearningResourceType");
            return Json(new { data = allObj });
        }

        //[HttpGet]
        //public IActionResult GetDAll(int? courseId)
        //{
        //    var allObj = _unitOfWork.CourseLearningResource.GetAll(c => c.CourseId == courseId);
        //    return Json(new { data = allObj });
        //}

        [HttpDelete]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.CourseLearningResource.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.CourseLearningResource.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion
    }
}
