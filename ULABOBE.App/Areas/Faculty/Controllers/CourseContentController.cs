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
    //[Authorize(Roles = "Faculty")]
    public class CourseContentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private UniqueSetup uniqueSetup;
        private string userName;
        public CourseContentController(IUnitOfWork unitOfWork)
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
            CourseContentVM courseContentVm = new CourseContentVM()
            {

                CourseContent = new CourseContent(),
                CourseHistoryLists = _unitOfWork.CourseHistory
                    .GetAll(includeProperties: "Course,Semester,Section,Instructor", filter: ch => ch.SemesterId == uniqueSetup.GetCurrentSemester().Id && ch.InstructorId == GetInstructor().Id)
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
        public IActionResult Upsert(CourseContentVM courseContentVm)
        {
            GetLatestSemester();
            MasterSetup aMasterSetup= uniqueSetup.GetMaxMasterSetup(courseContentVm.CourseContent.CourseHistoryId);
            if (aMasterSetup == null)
            {
                ViewBag.Message = "Schedule needs to set for this Course. Please contact departmental Admin";
                GetCourseBasicInfo(courseContentVm);
                return View(courseContentVm);
            }
            else if (DateTime.Now < aMasterSetup.StartDateTime && DateTime.Now > aMasterSetup.EndDateTime)
            {
                ViewBag.Message = "Deadline is Over. PLease contact Site Administrator";
                GetCourseBasicInfo(courseContentVm);
                return View(courseContentVm);
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
                        courseContentVm.CourseContent.UpdatedBy = "N/A";
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

        protected virtual void GetCourseBasicInfo(CourseContentVM courseContentVm)
        {
            courseContentVm.CourseHistoryLists = _unitOfWork.CourseHistory
                .GetAll(includeProperties: "Course,Semester,Section,Instructor", filter: ch => ch.SemesterId == uniqueSetup.GetCurrentSemester().Id && ch.InstructorId == uniqueSetup.GetInstructor(User.Identity.Name).Id)
                .Select(i => new SelectListItem
                {
                    Text = i.Course.CourseCode + "(" + i.Section.SectionCode + ")-" + i.Instructor.ShortCode + ")",
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


        #region API Calls

        [HttpGet]
        public IActionResult GetAll()
        {
            int maxSemester = _unitOfWork.Semester.GetAll().Max(mS => mS.Id);
            var userInfoCheck = _unitOfWork.UserInfoCheck.GetFirstOrDefault(user => user.UserInfoId == User.Identity.Name);
            var instructor = _unitOfWork.Instructor.GetFirstOrDefault(user => user.ShortCode == userInfoCheck.ShortCode);
            var allObj = _unitOfWork.CourseContent.GetAll(filter: cContent => cContent.CourseHistory.SemesterId == maxSemester && cContent.CourseHistory.InstructorId == uniqueSetup.GetInstructor(User.Identity.Name).Id, includeProperties: "CourseHistory,CourseHistory.Course,CourseHistory.Section,CourseHistory.Instructor,CourseHistory.Semester");
            return Json(new { data = allObj });
        }



        private Instructor GetInstructor()
        {
            var userInfoCheck = _unitOfWork.UserInfoCheck.GetFirstOrDefault(user => user.UserInfoId == User.Identity.Name);
            var instructor = _unitOfWork.Instructor.GetFirstOrDefault(user => user.ShortCode == userInfoCheck.ShortCode);
            return instructor;
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


        #endregion
    }
}
