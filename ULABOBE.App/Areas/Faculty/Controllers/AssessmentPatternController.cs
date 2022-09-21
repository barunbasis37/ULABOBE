using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using ULABOBE.App.Areas.Faculty.Controllers;
using ULABOBE.DataAccess.Repository.IRepository;
using ULABOBE.Models;
using ULABOBE.Models.ViewModels;
using ULABOBE.Utility;

namespace ULABOBE.AppOnline.Areas.Faculty.Controllers
{
    [Area("Faculty")]
    [Authorize(Roles = "Faculty")]
    public class AssessmentPatternController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private UniqueSetup uniqueSetup;
        private string userName;
        public AssessmentPatternController(IUnitOfWork unitOfWork)
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
            AssessmentPatternVM assessmentPatternCLOVM = new AssessmentPatternVM()
            {

                AssessmentPattern = new AssessmentPattern(),
                
                BloomsCategoryLists = _unitOfWork.BloomsCategory.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                AssessmentTechWeightagesLists = _unitOfWork.AssessmentTechniqueWeightage.GetAll(includeProperties: "CourseHistory,CourseHistory.Course,CourseHistory.Semester,CourseHistory.Section,CourseHistory.Instructor,AssessmentType", filter: ch => ch.CourseHistory.SemesterId == uniqueSetup.GetCurrentSemester().Id && ch.CourseHistory.InstructorId == uniqueSetup.GetInstructor(User.Identity.Name).Id)
                .Select(i => new SelectListItem
                {
                Text = i.CourseHistory.Course.CourseCode + "(" + i.CourseHistory.Section.SectionCode + "-" + i.CourseHistory.Instructor.ShortCode + ")-" + i.Strategy + "-" + i.AssessmentType.Code,
                Value = i.Id.ToString()
            }),


            };

            if (id == null)
            {
                //this is for create
                return View(assessmentPatternCLOVM);
            }
            //this is for edit

            if (assessmentPatternCLOVM.AssessmentPattern != null)
            {
                assessmentPatternCLOVM.AssessmentPattern = _unitOfWork.AssessmentPattern.Get(id.GetValueOrDefault());
            }

            if (assessmentPatternCLOVM.AssessmentPattern == null)
            {
                return NotFound();
            }
            return View(assessmentPatternCLOVM);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(AssessmentPatternVM assessmentPatternVM)
        {
            GetLatestSemester();
           
            AssessmentTechniqueWeightage assessmentTechniqueWeightage= _unitOfWork.AssessmentTechniqueWeightage.GetFirstOrDefault(assTW =>
                assTW.Id == assessmentPatternVM.AssessmentPattern.AssessmentTechWeightagesId);
            MasterSetup aMasterSetup= uniqueSetup.GetMaxMasterSetup(assessmentTechniqueWeightage.CourseHistoryId);
            if (DateTime.Now <= aMasterSetup.StartDateTime && DateTime.Now >= aMasterSetup.EndDateTime)
            {
                assessmentPatternVM.BloomsCategoryLists = _unitOfWork.BloomsCategory.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
                assessmentPatternVM.AssessmentTechWeightagesLists = _unitOfWork.AssessmentTechniqueWeightage.GetAll(includeProperties: "CourseHistory,CourseHistory.Course,CourseHistory.Semester,CourseHistory.Section,CourseHistory.Instructor,AssessmentType", filter: ch => ch.CourseHistory.SemesterId == uniqueSetup.GetCurrentSemester().Id && ch.CourseHistory.InstructorId == uniqueSetup.GetInstructor(User.Identity.Name).Id)
                    .Select(i => new SelectListItem
                    {
                        Text = i.CourseHistory.Course.CourseCode + "(" + i.CourseHistory.Section.SectionCode + "-" + i.CourseHistory.Instructor.ShortCode + ")-"+i.Strategy+"-"+i.AssessmentType.Code,
                        Value = i.Id.ToString()
                    });
            }
            else
            {

                if (ModelState.IsValid)
                {
                    

                    if (assessmentPatternVM.AssessmentPattern.Id == 0)
                    {

                        assessmentPatternVM.AssessmentPattern.QueryId = Guid.NewGuid();
                        assessmentPatternVM.AssessmentPattern.CreatedDate = DateTime.Now;

                        assessmentPatternVM.AssessmentPattern.CreatedDate = DateTime.Now;
                        assessmentPatternVM.AssessmentPattern.CreatedBy = User.Identity.Name;
                        assessmentPatternVM.AssessmentPattern.CreatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                        assessmentPatternVM.AssessmentPattern.UpdatedDate = DateTime.Now;
                        assessmentPatternVM.AssessmentPattern.UpdatedBy = "N/A";
                        assessmentPatternVM.AssessmentPattern.UpdatedIp = "0.0.0.0";
                        assessmentPatternVM.AssessmentPattern.IsDeleted = false;
                        _unitOfWork.AssessmentPattern.Add(assessmentPatternVM.AssessmentPattern);

                    }
                    else
                    {

                        assessmentPatternVM.AssessmentPattern.UpdatedDate = DateTime.Now;
                        assessmentPatternVM.AssessmentPattern.UpdatedBy = User.Identity.Name;
                        assessmentPatternVM.AssessmentPattern.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                        assessmentPatternVM.AssessmentPattern.IsDeleted = false;
                        _unitOfWork.AssessmentPattern.Update(assessmentPatternVM.AssessmentPattern);
                    }
                    _unitOfWork.Save();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(assessmentPatternVM);
        }


        

        #region API Calls

        [HttpGet]
        public IActionResult GetAll()
        {
            int maxSemester = _unitOfWork.Semester.GetAll().Max(mS => mS.Id);
            var allObj = _unitOfWork.AssessmentPattern.GetAll(filter: assP => assP.AssessmentTechniqueWeightage.CourseHistory.SemesterId == maxSemester && assP.AssessmentTechniqueWeightage.CourseHistory.InstructorId == uniqueSetup.GetInstructor(User.Identity.Name).Id, includeProperties: "AssessmentTechniqueWeightage,AssessmentTechniqueWeightage.CourseHistory,AssessmentTechniqueWeightage.CourseHistory.Course,AssessmentTechniqueWeightage.CourseHistory.Section,AssessmentTechniqueWeightage.CourseHistory.Instructor,AssessmentTechniqueWeightage.CourseHistory.Semester,BloomsCategory,AssessmentTechniqueWeightage.AssessmentType");
            return Json(new { data = allObj });
        }


        

        #endregion
    }
}
