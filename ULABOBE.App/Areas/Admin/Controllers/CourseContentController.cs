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
    public class CourseContentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseContentController(IUnitOfWork unitOfWork)
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
            CourseContentVM courseContentVm = new CourseContentVM()
            {

                CourseContent = new CourseContent(),
                CourseHistoryLists = _unitOfWork.CourseHistory
                    .GetAll(includeProperties: "Course,Semester,Section,Instructor", filter: ch => ch.SemesterId == uniqueSetup.GetCurrentSemester().Id)
                    .Select(i => new SelectListItem
                    {
                        Text = i.Course.CourseCode + "(" + i.Section.SectionCode + ")-" + i.Instructor.ShortCode + ")",
                        Value = i.Id.ToString()
                    }),
            //SemesterLists = _unitOfWork.Semester.GetAll().Select(i => new SelectListItem
            //{
            //    Text = i.Name,
            //    Value = i.Id.ToString()
            //}),
            CourseLearningLists = _unitOfWork.CourseLearning.GetAll().Select(i => new SelectListItem
                {
                    Text = i.CLOCode,
                    Value = i.Id.ToString()
                }),
            LearningAssessmentRubricLists = _unitOfWork.LearningAssessmentRubric.GetAll().Select(i => new SelectListItem
            {
                Text = i.LARCode,
                Value = i.Id.ToString()
            }),

            };

            if (id == null)
            {
                //this is for create
                return View(courseContentVm);
            }
            //this is for edit

            if (courseContentVm.CourseContent != null)
            {



                courseContentVm.CourseContent = _unitOfWork.CourseContent.Get(id.GetValueOrDefault());
                courseContentVm.CourseLearningSelectedIdArray =
                    courseContentVm.CourseContent.CLoSelectedIDs.Split(',').ToArray();
                courseContentVm.ARSelectedIDArray =
                    courseContentVm.CourseContent.ARSelectedIDs.Split(',').ToArray();


            }

            if (courseContentVm.CourseContent == null)
            {
                return NotFound();
            }
            return View(courseContentVm);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Upsert(CourseContentVM courseContentVm)
        {
            uniqueSetup = new UniqueSetup(_unitOfWork);
            MasterSetup aMasterSetup= uniqueSetup.GetMaxMasterSetup(courseContentVm.CourseContent.CourseHistoryId);
            if (DateTime.Now <= aMasterSetup.StartDateTime && DateTime.Now >= aMasterSetup.EndDateTime)
            {
                courseContentVm.CourseHistoryLists = _unitOfWork.CourseHistory
                    .GetAll(includeProperties: "Course,Semester,Section,Instructor", filter: ch => ch.SemesterId == uniqueSetup.GetCurrentSemester().Id)
                    .Select(i => new SelectListItem
                    {
                        Text = i.Course.CourseCode + "("+i.Section.SectionCode+")-"+i.Instructor.ShortCode+")",
                        Value = i.Id.ToString()
                    }); ;
                

                courseContentVm.CourseLearningLists = _unitOfWork.CourseLearning.GetAll().Select(i => new SelectListItem
                {
                    Text = i.CLOCode,
                    Value = i.Id.ToString()
                });
                courseContentVm.LearningAssessmentRubricLists = _unitOfWork.LearningAssessmentRubric.GetAll().Select(
                    i => new SelectListItem
                    {
                        Text = i.LARCode,
                        Value = i.Id.ToString()
                    });
            }
            else
            {


                List<string> stringArray = new List<string>();
                foreach (var cloID in courseContentVm.CourseLearningSelectedIdArray)
                {
                    CourseLearning aCourseLearning = _unitOfWork.CourseLearning.Get(Convert.ToInt32(cloID));
                    stringArray.Add(aCourseLearning.CLOCode);
                }

                List<string> arstringArray = new List<string>();
                foreach (var arID in courseContentVm.ARSelectedIDArray)
                {
                    LearningAssessmentRubric learningAssessmentRubric = _unitOfWork.LearningAssessmentRubric.Get(Convert.ToInt32(arID));
                    arstringArray.Add(learningAssessmentRubric.LARCode);
                }

                if (ModelState.IsValid)
                {
                    courseContentVm.CourseContent.CLoSelectedIDs = string.Join(",", courseContentVm.CourseLearningSelectedIdArray);
                    courseContentVm.CourseContent.CLoSelectedIDNames = String.Join(",", stringArray);

                    courseContentVm.CourseContent.ARSelectedIDs = string.Join(",", courseContentVm.ARSelectedIDArray);
                    courseContentVm.CourseContent.ARSelectedIDNames = String.Join(",", arstringArray);

                    if (courseContentVm.CourseContent.Id == 0)
                    {

                        courseContentVm.CourseContent.QueryId = Guid.NewGuid();
                        courseContentVm.CourseContent.CreatedDate = DateTime.Now;
                        
                        courseContentVm.CourseContent.CreatedDate = DateTime.Now;
                        courseContentVm.CourseContent.CreatedBy = User.Identity.Name;
                        courseContentVm.CourseContent.CreatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                        courseContentVm.CourseContent.UpdatedDate = DateTime.Now;
                        courseContentVm.CourseContent.UpdatedBy = "-";
                        courseContentVm.CourseContent.UpdatedIp = "0.0.0.0";
                        courseContentVm.CourseContent.IsDeleted = false;
                        _unitOfWork.CourseContent.Add(courseContentVm.CourseContent);

                    }
                    else
                    {

                        courseContentVm.CourseContent.UpdatedDate = DateTime.Now;
                        courseContentVm.CourseContent.UpdatedBy = User.Identity.Name;
                        courseContentVm.CourseContent.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                        courseContentVm.CourseContent.IsDeleted = false;
                        _unitOfWork.CourseContent.Update(courseContentVm.CourseContent);
                    }
                    _unitOfWork.Save();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(courseContentVm);
        }


        

        #region API Calls

        [HttpGet]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.CourseContent.GetAll(includeProperties: "CourseHistory,CourseHistory.Course,CourseHistory.Section,CourseHistory.Instructor,CourseHistory.Semester");
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
            var objFromDb = _unitOfWork.CourseContent.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.CourseContent.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion
    }
}
