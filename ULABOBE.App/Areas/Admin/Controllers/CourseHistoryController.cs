using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using ULABOBE.DataAccess.Migrations;
using ULABOBE.DataAccess.Repository.IRepository;
using ULABOBE.Models;
using ULABOBE.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using ULABOBE.Utility;

namespace ULABOBE.AppOnline.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CourseHistoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseHistoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Upsert(int? id)
        {
            CourseHistoryVM courseHistoryVM = new CourseHistoryVM()
            {
                CourseHistory = new CourseHistory(),
                CourseLists = _unitOfWork.Course.GetAll().Select(i => new SelectListItem
                {
                    Text = i.CourseCode + " (" + i.Title + ") ",
                    Value = i.Id.ToString()
                }),
                //SemesterLists = _unitOfWork.Semester.GetAll().Select(i => new SelectListItem
                //{
                //    Text = i.Name,
                //    Value = i.Id.ToString()
                //}),

                InstructorLists = _unitOfWork.Instructor.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                ScheduleLists = _unitOfWork.Schedule.GetAll(includeProperties: "WeekDay1,WeekDay2,FTime,TTime").Select(i => new SelectListItem
                {
                    Text = i.WeekDay1.ShortName + "-" + i.WeekDay2.ShortName + "(Time-" + i.FTime.Name + "-" + i.TTime.Name+")",
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
                return View(courseHistoryVM);
            }
            //this is for edit

            if (courseHistoryVM.CourseHistory != null)
            {
                courseHistoryVM.CourseHistory = _unitOfWork.CourseHistory.Get(id.GetValueOrDefault());
                courseHistoryVM.ScheduleSelectedIdArray =
                    courseHistoryVM.CourseHistory.ScheduleIDs.Split(',').ToArray();
            }
            if (courseHistoryVM.CourseHistory == null)
            {
                return NotFound();
            }
            return View(courseHistoryVM);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Upsert(CourseHistoryVM courseHistoryVM)
        {
            try
            {
                var aMasterSetup = GetMaxMasterSetup(courseHistoryVM);
                if (aMasterSetup == null)
                {
                    ViewBag.Message = "Schedule needs to set for this Course. Please Contact Departmental Admin.";
                    //GetCourseBasicInfo(courseHistoryVM);
                    //return View(courseHistoryVM);
                }
                else if (DateTime.Now <= aMasterSetup.StartDateTime)
                {
                    ViewBag.Message = "Date is not start yet. Please wait for the start date.";

                    //return View(courseHistoryVM);
                }
                else if (DateTime.Now >= aMasterSetup.EndDateTime)
                {
                    ViewBag.Message = "Deadline is Over. Please Contact Departmental Admin.";

                    //return View(courseHistoryVM);
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

                    if (ModelState.IsValid)
                    {
                        courseHistoryVM.CourseHistory.ScheduleIDs = string.Join(",", courseHistoryVM.ScheduleSelectedIdArray);
                        courseHistoryVM.CourseHistory.SchedulesNames = String.Join(",", stringArray);

                        if (courseHistoryVM.CourseHistory.Id == 0)
                        {
                            courseHistoryVM.CourseHistory.SemesterId = aMasterSetup.SemesterId;
                            courseHistoryVM.CourseHistory.QueryId = Guid.NewGuid();
                            //var claimsIdentity = (ClaimsIdentity)User.Identity;
                            //var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                            courseHistoryVM.CourseHistory.CreatedDate = DateTime.Now;
                            courseHistoryVM.CourseHistory.CreatedBy = User.Identity.Name;
                            courseHistoryVM.CourseHistory.CreatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                            courseHistoryVM.CourseHistory.UpdatedDate = DateTime.Now;
                            courseHistoryVM.CourseHistory.UpdatedBy = "-";
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
                
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            GetCourseBasicInfo(courseHistoryVM);
            return View(courseHistoryVM);
        }

        protected virtual void GetCourseBasicInfo(CourseHistoryVM courseHistoryVM)
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

        private MasterSetup GetMaxMasterSetup(CourseHistoryVM courseHistoryVM)
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
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult GetAll()
        {
            int maxSemester = _unitOfWork.Semester.GetAll().Max(mS => mS.Id);
            var allObj = _unitOfWork.CourseHistory.GetAll(filter: ch=>ch.SemesterId== maxSemester, includeProperties: "Course,Course.CourseType,Instructor,Semester,Section");
            return Json(new { data = allObj });
        }


        [HttpDelete]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.CourseHistory.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.CourseHistory.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion
    }
}
