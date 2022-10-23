using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using ULABOBE.App.Areas.Faculty.Controllers;
using ULABOBE.DataAccess.Migrations;
using ULABOBE.DataAccess.Repository.IRepository;
using ULABOBE.Models;
using ULABOBE.Models.ViewModels;
using ULABOBE.Utility;

namespace ULABOBE.AppOnline.Areas.Faculty.Controllers
{
    [Area("Faculty")]
    [Authorize]

    //[Area(nameof(Faculty))]  // CRFacultyM is the another-folder name inside Areas folder
    //[Route("~/Faculty/[controller]/[action]")]
    //[Authorize(Roles = "Faculty")]
    //[ServiceFilter(typeof(ActionFilter))]
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
        [Authorize(Roles = SD.Role_Faculty)]
        //[Route("~/Faculty/[controller]/[action]")]
        public IActionResult Index()
        {
            GetLatestSemester();
            return View();
        }

        private void GetLatestSemester()
        {
            //ViewBag.InstructorInfo = uniqueSetup.GetInstructor(User.Identity.Name).Name + "(" + uniqueSetup.GetInstructor(User.Identity.Name).ShortCode + ")";
            //ViewBag.Semester = uniqueSetup.GetCurrentSemester().Name + "(" + uniqueSetup.GetCurrentSemester().Code + ")";
            if (User.Identity.IsAuthenticated && User.IsInRole(SD.Role_Faculty))
            {
                UserInfoCheck userInfoCheck = _unitOfWork.UserInfoCheck.GetFirstOrDefault(user => user.UserInfoId == User.Identity.Name);
                ViewBag.InstructorInfo = userInfoCheck.Name + " (" + userInfoCheck.ShortCode + ")";
                //Instructor instructorId = _unitOfWork.Instructor.GetFirstOrDefault(ins => ins.ShortCode == userInfoCheck.ShortCode);
                //ViewBag.InstructorId = instructorId.Id;
                int maxSemester = _unitOfWork.Semester.GetAll().Max(mS => mS.Id);
                Semester semester = _unitOfWork.Semester.Get(maxSemester);
                //ViewBag.SemesterId = semester.Id;
                ViewBag.Semester = "Semester: " + semester.Name + "(" + semester.Code + ")";
            }
        }

        [HttpGet]
        [Authorize(Roles = SD.Role_Faculty)]
        //[Route(("~/Faculty/[controller]/[action]/{id?}"))]
        public IActionResult Upsert(int? id)
        {
            CourseHistoryFVM courseHistoryFVM = null;
            if (User.Identity.IsAuthenticated && User.IsInRole(SD.Role_Faculty))
            {

                GetLatestSemester();

                courseHistoryFVM = new CourseHistoryFVM()
                {
                    CourseHistory = new CourseHistory(),
                    CourseLists = _unitOfWork.Course.GetAll().Select(i => new SelectListItem
                    {
                        Text = i.CourseCode + " (" + i.Title + ") ",
                        Value = i.Id.ToString()
                    }),

                    ScheduleLists = _unitOfWork.Schedule.GetAll(includeProperties: "WeekDay1,WeekDay2,FTime,TTime").Select(i => new SelectListItem
                    {
                        Text = i.WeekDay1.ShortName + "-" + i.WeekDay2.ShortName + "(Time-" + i.FTime.Name + "-" + i.TTime.Name + ")",
                        Value = i.Id.ToString()
                    }),
                    SectionLists = _unitOfWork.Section.GetAll().Select(i => new SelectListItem
                    {
                        Text = i.SectionCode.ToString(),
                        Value = i.Id.ToString()
                    }),
                };
                if (id == null)
                {
                    //this is for create
                    return View(courseHistoryFVM);
                }
                //this is for edit

                if (courseHistoryFVM.CourseHistory != null)
                {

                    courseHistoryFVM.CourseHistory = _unitOfWork.CourseHistory.Get(id.GetValueOrDefault());
                    courseHistoryFVM.ScheduleSelectedIdArray =
                        courseHistoryFVM.CourseHistory.ScheduleIDs.Split(',').ToArray();
                }



                if (courseHistoryFVM.CourseHistory == null)
                {
                    return NotFound();
                }
            }
            return View(courseHistoryFVM);


        }

        //[HttpPost("~/Faculty/[controller]/[action]")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_Faculty)]
        public IActionResult Upsert(CourseHistoryFVM courseHistoryVM)
        {
            GetLatestSemester();
            int maxSemester = _unitOfWork.Semester.GetAll().Max(mS => mS.Id);
            UserInfoCheck userInfoCheck = _unitOfWork.UserInfoCheck.GetFirstOrDefault(user => user.UserInfoId == User.Identity.Name);
            var aMasterSetup = GetMaxMasterSetup(courseHistoryVM);



            if (aMasterSetup == null)
            {
                ViewBag.Message = "Schedule needs to set for this Course. Please Contact Departmental Admin.";

            }
            else if (DateTime.Now <= aMasterSetup.StartDateTime)
            {
                ViewBag.Message = "Date is not start yet. Please wait for the start date.";

            }
            else if (DateTime.Now >= aMasterSetup.EndDateTime)
            {
                ViewBag.Message = "Deadline is Over. Please Contact Departmental Admin.";

            }
            else
            {

                List<string> stringArray = new List<string>();
                foreach (var courseScheduleId in courseHistoryVM.ScheduleSelectedIdArray)
                {
                    Schedule aSchedule = _unitOfWork.Schedule.GetFirstOrDefault(sc => sc.Id == Convert.ToInt32(courseScheduleId), includeProperties: "WeekDay1,WeekDay2,FTime,TTime");
                    string singlrSc = aSchedule.WeekDay1.ShortName + "-" + aSchedule.WeekDay2.ShortName + "-" +
                                      aSchedule.FTime.Name + "-" + aSchedule.TTime.Name;
                    stringArray.Add(singlrSc);
                }

                try
                {
                    if (ModelState.IsValid)
                    {
                        courseHistoryVM.CourseHistory.ScheduleIDs = string.Join(",", courseHistoryVM.ScheduleSelectedIdArray);
                        courseHistoryVM.CourseHistory.SchedulesNames = String.Join(",", stringArray);



                        if (courseHistoryVM.CourseHistory.Id == 0)
                        {
                            courseHistoryVM.CourseHistory.SemesterId = maxSemester;
                            courseHistoryVM.CourseHistory.TotalMarks = courseHistoryVM.CourseHistory.CIEMarks +
                                                                       courseHistoryVM.CourseHistory.SEEMarks;

                            Instructor instructor = _unitOfWork.Instructor.GetFirstOrDefault(user => user.ShortCode == userInfoCheck.ShortCode);
                            courseHistoryVM.CourseHistory.InstructorId = instructor.Id;
                            courseHistoryVM.CourseHistory.QueryId = Guid.NewGuid();
                            //var claimsIdentity = (ClaimsIdentity)User.Identity;
                            //var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                            courseHistoryVM.CourseHistory.CreatedDate = DateTime.Now;
                            courseHistoryVM.CourseHistory.CreatedBy = User.Identity.Name;
                            courseHistoryVM.CourseHistory.CreatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                            courseHistoryVM.CourseHistory.UpdatedDate = DateTime.Now;
                            courseHistoryVM.CourseHistory.UpdatedBy = "N/A";
                            courseHistoryVM.CourseHistory.UpdatedIp = "0.0.0.0";
                            courseHistoryVM.CourseHistory.IsDeleted = false;
                            _unitOfWork.CourseHistory.Add(courseHistoryVM.CourseHistory);

                        }
                        else
                        {
                            courseHistoryVM.CourseHistory.UpdatedDate = DateTime.Now;
                            courseHistoryVM.CourseHistory.UpdatedBy = User.Identity.Name;
                            courseHistoryVM.CourseHistory.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                            courseHistoryVM.CourseHistory.IsDeleted = false;
                            _unitOfWork.CourseHistory.Update(courseHistoryVM.CourseHistory);
                        }
                        _unitOfWork.Save();
                        return RedirectToAction(nameof(Index));

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }


            GetCourseBasicInfo(courseHistoryVM);
            return View(courseHistoryVM);
        }

        protected virtual void GetCourseBasicInfo(CourseHistoryFVM courseHistoryVM)
        {
            courseHistoryVM.CourseLists = _unitOfWork.Course.GetAll().Select(i => new SelectListItem
            {
                Text = i.CourseCode + " (" + i.Title + ") ",
                Value = i.Id.ToString()
            });
            courseHistoryVM.ScheduleLists = _unitOfWork.Schedule
                .GetAll(includeProperties: "WeekDay1,WeekDay2,FTime,TTime").Select(i => new SelectListItem
                {
                    Text = i.WeekDay1.ShortName + "-" + i.WeekDay2.ShortName + "(Time-" + i.FTime.Name + "-" +
                           i.TTime.Name + ")",
                    Value = i.Id.ToString()
                });
            courseHistoryVM.SectionLists = _unitOfWork.Section.GetAll().Select(i => new SelectListItem
            {
                Text = i.SectionCode.ToString(),
                Value = i.Id.ToString()
            });
        }

        private MasterSetup GetMaxMasterSetup(CourseHistoryFVM courseHistoryVM)
        {
            int maxSemester = _unitOfWork.Semester.GetAll().Max(mS => mS.Id);
            Course aCourse = _unitOfWork.Course.GetFirstOrDefault(c => c.Id == courseHistoryVM.CourseHistory.CourseId);
            MasterSetup aMasterSetup =
                _unitOfWork.MasterSetup.GetFirstOrDefault(sc =>
                    sc.SemesterId == maxSemester && sc.ProgramId == aCourse.ProgramId);
            return aMasterSetup;
        }

        #region API Calls

        [HttpGet]
        [Authorize(Roles = SD.Role_Faculty)]
        public IActionResult GetAll()
        {
            UserInfoCheck userInfoCheck = _unitOfWork.UserInfoCheck.GetFirstOrDefault(user => user.UserInfoId == User.Identity.Name);
            int maxSemester = _unitOfWork.Semester.GetAll().Max(mS => mS.Id);
            Instructor instructor = _unitOfWork.Instructor.GetFirstOrDefault(user => user.ShortCode == userInfoCheck.ShortCode);
            var allObj = _unitOfWork.CourseHistory.GetAll(filter: ch => ch.InstructorId== instructor.Id &&  ch.SemesterId == maxSemester, includeProperties: "Course,Course.CourseType,Instructor,Semester,Section");
            return Json(new { data = allObj });

        }





        #endregion
    }
}
