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
    //[Authorize(Roles = "Faculty")]
    public class AssessmentTechniqueWeightageController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private UniqueSetup uniqueSetup;
        private string userName;
        public AssessmentTechniqueWeightageController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            uniqueSetup = new UniqueSetup(_unitOfWork);
        }

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

        public IActionResult Upsert(int? id)
        {
            GetLatestSemester();
            AssessmentTechniqueWeightageVM assessmentTechniqueWeightageVM = new AssessmentTechniqueWeightageVM()
            {
                AssessmentTechniqueWeightage = new AssessmentTechniqueWeightage(),
                AssessmentTypeLists = _unitOfWork.AssessmentType.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Code+"("+ i.Name+")",
                    Value = i.Id.ToString()
                }),
                CourseHistoryLists = _unitOfWork.CourseHistory
                    .GetAll(includeProperties: "Course,Semester,Section,Instructor", filter: ch => ch.SemesterId == uniqueSetup.GetCurrentSemester().Id && ch.InstructorId == uniqueSetup.GetInstructor(User.Identity.Name).Id)
                    .Select(i => new SelectListItem
                    {
                        Text = i.Course.CourseCode + "(" + i.Section.SectionCode + ")-" + i.Instructor.ShortCode + ")",
                        Value = i.Id.ToString()
                    }),
            };
            if (id == null)
            {
                //this is for create
                return View(assessmentTechniqueWeightageVM);
            }
            //this is for edit
            assessmentTechniqueWeightageVM.AssessmentTechniqueWeightage = _unitOfWork.AssessmentTechniqueWeightage.Get(id.GetValueOrDefault());
            if (assessmentTechniqueWeightageVM.AssessmentTechniqueWeightage == null)
            {
                return NotFound();
            }
            return View(assessmentTechniqueWeightageVM);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(AssessmentTechniqueWeightageVM assessmentTechniqueWeightageVM)
        {
            GetLatestSemester();
            if (ModelState.IsValid)
            {
                if (assessmentTechniqueWeightageVM.AssessmentTechniqueWeightage.Id == 0)
                {
                    assessmentTechniqueWeightageVM.AssessmentTechniqueWeightage.QueryId = Guid.NewGuid();
                    
                    assessmentTechniqueWeightageVM.AssessmentTechniqueWeightage.CreatedDate = DateTime.Now;
                    assessmentTechniqueWeightageVM.AssessmentTechniqueWeightage.CreatedBy = User.Identity.Name;
                    assessmentTechniqueWeightageVM.AssessmentTechniqueWeightage.CreatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    assessmentTechniqueWeightageVM.AssessmentTechniqueWeightage.UpdatedDate = DateTime.Now;
                    assessmentTechniqueWeightageVM.AssessmentTechniqueWeightage.UpdatedBy = "N/A";
                    assessmentTechniqueWeightageVM.AssessmentTechniqueWeightage.UpdatedIp = "0.0.0.0";
                    assessmentTechniqueWeightageVM.AssessmentTechniqueWeightage.IsDeleted = false;
                    _unitOfWork.AssessmentTechniqueWeightage.Add(assessmentTechniqueWeightageVM.AssessmentTechniqueWeightage);

                }
                else
                {
                    assessmentTechniqueWeightageVM.AssessmentTechniqueWeightage.UpdatedDate = DateTime.Now;
                    assessmentTechniqueWeightageVM.AssessmentTechniqueWeightage.UpdatedBy = User.Identity.Name;
                    assessmentTechniqueWeightageVM.AssessmentTechniqueWeightage.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    assessmentTechniqueWeightageVM.AssessmentTechniqueWeightage.IsDeleted = false;
                    _unitOfWork.AssessmentTechniqueWeightage.Update(assessmentTechniqueWeightageVM.AssessmentTechniqueWeightage);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            assessmentTechniqueWeightageVM.AssessmentTypeLists = _unitOfWork.AssessmentType.GetAll().Select(i => new SelectListItem
            {
                Text = i.Code + "(" + i.Name + ")",
                Value = i.Id.ToString()
            });
            assessmentTechniqueWeightageVM.CourseHistoryLists = _unitOfWork.CourseHistory
                .GetAll(includeProperties: "Course,Semester,Section,Instructor", filter: ch => ch.SemesterId == uniqueSetup.GetCurrentSemester().Id && ch.InstructorId == uniqueSetup.GetInstructor(User.Identity.Name).Id)
                .Select(i => new SelectListItem
                {
                    Text = i.Course.CourseCode + "(" + i.Section.SectionCode + ")-" + i.Instructor.ShortCode + ")",
                    Value = i.Id.ToString()
                });
            return View(assessmentTechniqueWeightageVM);
        }

        #region API Calls

        [HttpGet]
        public IActionResult GetAll()
        {
            int maxSemester = _unitOfWork.Semester.GetAll().Max(mS => mS.Id);
            var allObj = _unitOfWork.AssessmentTechniqueWeightage.GetAll(filter: assTechW => assTechW.CourseHistory.SemesterId == maxSemester && assTechW.CourseHistory.InstructorId == uniqueSetup.GetInstructor(User.Identity.Name).Id, includeProperties: "CourseHistory,CourseHistory.Course,CourseHistory.Section,CourseHistory.Instructor,CourseHistory.Semester,,AssessmentType");
            return Json(new { data = allObj });
        }

        


        #endregion
    }
}
