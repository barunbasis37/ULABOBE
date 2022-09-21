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
    public class CourseCLOController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseCLOController(IUnitOfWork unitOfWork)
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
            CourseCLOVM courseCLOVM = new CourseCLOVM()
            {
                CourseCLO = new CourseCLO(),
                CourseHistoryLists = _unitOfWork.CourseHistory.GetAll(includeProperties: "Course,Semester,Section,Instructor", filter: ch => ch.SemesterId == uniqueSetup.GetCurrentSemester().Id).Select(i => new SelectListItem
                {
                    Text = i.Course.CourseCode + "(" + i.Section.SectionCode + ")-" + i.Instructor.ShortCode,
                    Value = i.Id.ToString()
                }),
                SemesterLists = _unitOfWork.Semester.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                
                CourseLearningLists = _unitOfWork.CourseLearning.GetAll().Select(i => new SelectListItem
                {
                    Text = i.CLOCode,
                    Value = i.Id.ToString()
                }),
            };
            if (id == null)
            {
                //this is for create
                return View(courseCLOVM);
            }
            //this is for edit
            courseCLOVM.CourseCLO = _unitOfWork.CourseCLO.Get(id.GetValueOrDefault());
            if (courseCLOVM.CourseCLO == null)
            {
                return NotFound();
            }
            return View(courseCLOVM);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Upsert(CourseCLOVM courseCLOVM)
        {
            //int maxSemester = _unitOfWork.Semester.GetAll().Max(mS => mS.Id);
            if (ModelState.IsValid)
            {
                if (courseCLOVM.CourseCLO.Id == 0)
                {
                    courseCLOVM.CourseCLO.QueryId = Guid.NewGuid();
                    
                    courseCLOVM.CourseCLO.CreatedDate = DateTime.Now;
                    courseCLOVM.CourseCLO.CreatedBy = User.Identity.Name;
                    courseCLOVM.CourseCLO.CreatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    courseCLOVM.CourseCLO.UpdatedDate = DateTime.Now;
                    courseCLOVM.CourseCLO.UpdatedBy = "-";
                    courseCLOVM.CourseCLO.UpdatedIp = "0.0.0.0";
                    courseCLOVM.CourseCLO.IsDeleted = false;
                    _unitOfWork.CourseCLO.Add(courseCLOVM.CourseCLO);

                }
                else
                {
                    courseCLOVM.CourseCLO.UpdatedDate = DateTime.Now;
                    //courseCLO.UpdatedBy = User.Identity.Name;
                    //courseCLO.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    courseCLOVM.CourseCLO.UpdatedBy = User.Identity.Name;
                    courseCLOVM.CourseCLO.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    courseCLOVM.CourseCLO.IsDeleted = false;
                    _unitOfWork.CourseCLO.Update(courseCLOVM.CourseCLO);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            courseCLOVM.CourseHistoryLists = _unitOfWork.CourseHistory
                .GetAll(includeProperties: "Course,Semester,Section,Instructor", filter: ch => ch.SemesterId == uniqueSetup.GetCurrentSemester().Id)
                .Select(i => new SelectListItem
                {
                    Text = i.Course.CourseCode + "("+i.Section.SectionCode+")-"+i.Instructor.ShortCode+")",
                    Value = i.Id.ToString()
                });
            courseCLOVM.SemesterLists = _unitOfWork.Semester.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            courseCLOVM.CourseLearningLists = _unitOfWork.CourseLearning.GetAll().Select(i => new SelectListItem
            {
                Text = i.CLOCode,
                Value = i.Id.ToString()
            });
            return View(courseCLOVM);
        }

        #region API Calls

        [HttpGet]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.CourseCLO.GetAll(includeProperties: "CourseHistory,CourseHistory.Course,CourseHistory.Section,CourseHistory.Instructor,CourseHistory.Semester,CourseLearning");
            return Json(new { data = allObj });
        }

        [HttpGet]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public JsonResult GetDAll(int? courseHistoryId)
        {

             var CourseCLOLists = _unitOfWork.CourseCLO.GetAll(cid=>cid.CourseHistoryId== courseHistoryId, includeProperties: "CourseLearning").Select(i => new SelectListItem
             {
                 Text = i.CourseLearning.CLOCode,
                 Value = i.Id.ToString()
             });
            return Json(CourseCLOLists);
        }

        [HttpDelete]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.CourseCLO.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.CourseCLO.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion
    }
}
