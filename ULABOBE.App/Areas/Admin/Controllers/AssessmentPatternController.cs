using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
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
    public class AssessmentPatternController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AssessmentPatternController(IUnitOfWork unitOfWork)
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
            AssessmentPatternVM assessmentPatternCLOVM = new AssessmentPatternVM()
            {

                AssessmentPattern = new AssessmentPattern(),
                //CourseHistoryLists = _unitOfWork.CourseHistory
                //    .GetAll(includeProperties: "Course,Semester,Section,Instructor", filter: ch => ch.SemesterId == uniqueSetup.GetCurrentSemester().Id)
                //    .Select(i => new SelectListItem
                //    {
                //        Text = i.Course.CourseCode + "(" + i.Section.SectionCode + ")-" + i.Instructor.ShortCode + ")",
                //        Value = i.Id.ToString()
                //    }),
                BloomsCategoryLists = _unitOfWork.BloomsCategory.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                AssessmentTechWeightagesLists = _unitOfWork.AssessmentTechniqueWeightage.GetAll(includeProperties: "CourseHistory,CourseHistory.Course,CourseHistory.Semester,CourseHistory.Section,CourseHistory.Instructor,AssessmentType", filter: ch => ch.CourseHistory.SemesterId == uniqueSetup.GetCurrentSemester().Id)
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
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Upsert(AssessmentPatternVM assessmentPatternVM)
        {
            uniqueSetup = new UniqueSetup(_unitOfWork);
            AssessmentTechniqueWeightage assessmentTechniqueWeightage= _unitOfWork.AssessmentTechniqueWeightage.GetFirstOrDefault(assTW =>
                assTW.Id == assessmentPatternVM.AssessmentPattern.AssessmentTechWeightagesId);
            MasterSetup aMasterSetup= uniqueSetup.GetMaxMasterSetup(assessmentTechniqueWeightage.CourseHistoryId);
            if (DateTime.Now <= aMasterSetup.StartDateTime && DateTime.Now >= aMasterSetup.EndDateTime)
            {
                //assessmentPatternVM.CourseHistoryLists = _unitOfWork.CourseHistory
                //    .GetAll(includeProperties: "Course,Semester,Section,Instructor", filter: ch => ch.SemesterId == uniqueSetup.GetCurrentSemester().Id)
                //    .Select(i => new SelectListItem
                //    {
                //        Text = i.Course.CourseCode + "("+i.Section.SectionCode+")-"+i.Instructor.ShortCode+")",
                //        Value = i.Id.ToString()
                //    }); ;


                assessmentPatternVM.BloomsCategoryLists = _unitOfWork.BloomsCategory.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
                assessmentPatternVM.AssessmentTechWeightagesLists = _unitOfWork.AssessmentTechniqueWeightage.GetAll(includeProperties: "CourseHistory,CourseHistory.Course,CourseHistory.Semester,CourseHistory.Section,CourseHistory.Instructor,AssessmentType", filter: ch => ch.CourseHistory.SemesterId == uniqueSetup.GetCurrentSemester().Id)
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
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.AssessmentPattern.GetAll(includeProperties: "AssessmentTechniqueWeightage,AssessmentTechniqueWeightage.CourseHistory,AssessmentTechniqueWeightage.CourseHistory.Course,AssessmentTechniqueWeightage.CourseHistory.Section,AssessmentTechniqueWeightage.CourseHistory.Instructor,AssessmentTechniqueWeightage.CourseHistory.Semester,BloomsCategory,AssessmentTechniqueWeightage.AssessmentType");
            return Json(new { data = allObj });
        }
        //[HttpGet]
        //public JsonResult GetDAll(int? courseId)
        //{

        //     var CourseCLOLists = _unitOfWork.CourseCLO.GetAll(cid=>cid.CourseId==courseId, includeProperties: "CourseLearning").Select(i => new SelectListItem
        //     {
        //         Text = i.CourseLearning.CLOCode,
        //         Value = i.Id.ToString()
        //     });
        //    return Json(CourseCLOLists);
        //}

        [HttpDelete]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.AssessmentPattern.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.AssessmentPattern.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion
    }
}
