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

namespace ULABOBE.AppOnline.Areas.Faculty.Controllers
{
    [Area("Faculty")]
    //[Authorize(Roles = "Faculty")]
    public class MappingCourseProgramLOController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private UniqueSetup uniqueSetup;
        private string userName;
        public MappingCourseProgramLOController(IUnitOfWork unitOfWork)
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
            ViewBag.InstructorInfo = uniqueSetup.GetInstructor(User.Identity.Name).Name + "(" + uniqueSetup.GetInstructor(User.Identity.Name).ShortCode + ")";
            ViewBag.Semester = uniqueSetup.GetCurrentSemester().Name + "(" + uniqueSetup.GetCurrentSemester().Code + ")";
        }

        public IActionResult Upsert(int? id)
        {
            GetLatestSemester();
            MappingCourseProgramLOVM mappingCourseProgramLOVM = new MappingCourseProgramLOVM()
            {

                MappingCourseProgramLO = new MappingCourseProgramLO(),
                ProgramLists = _unitOfWork.Program.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name + "(" + i.ProgramCode + ")",
                    Value = i.Id.ToString()
                }),
                CourseHistoryLists = _unitOfWork.CourseHistory
                    .GetAll(includeProperties: "Course,Semester,Section,Instructor", filter: ch => ch.SemesterId == uniqueSetup.GetCurrentSemester().Id && ch.InstructorId == uniqueSetup.GetInstructor(User.Identity.Name).Id)
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
                
                CorrelationLists = _unitOfWork.Correlation.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Stage.ToString(),
                    Value = i.Id.ToString()
                }),
                CourseLearningLists = _unitOfWork.CourseLearning.GetAll().Select(i => new SelectListItem
                {
                    Text = i.CLOCode,
                    Value = i.Id.ToString()
                }),
            ProgramPLOLists = _unitOfWork.ProgramPLO.GetAll(includeProperties: "ProgramLearning").Select(i => new SelectListItem
                {
                    Text = i.ProgramLearning.PLOCode,
                    Value = i.Id.ToString()
                }),
        };
            if (id == null)
            {
                //this is for create
                return View(mappingCourseProgramLOVM);
            }
            //this is for edit
            mappingCourseProgramLOVM.MappingCourseProgramLO = _unitOfWork.MappingCourseProgramLO.Get(id.GetValueOrDefault());
            if (mappingCourseProgramLOVM.MappingCourseProgramLO == null)
            {
                return NotFound();
            }
            return View(mappingCourseProgramLOVM);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(MappingCourseProgramLOVM mappingCourseProgramLOVM)
        {
            GetLatestSemester();

            if (ModelState.IsValid)
            {
                if (mappingCourseProgramLOVM.MappingCourseProgramLO.Id == 0)
                {
                    mappingCourseProgramLOVM.MappingCourseProgramLO.QueryId = Guid.NewGuid();

                    mappingCourseProgramLOVM.MappingCourseProgramLO.CreatedDate = DateTime.Now;
                    mappingCourseProgramLOVM.MappingCourseProgramLO.CreatedBy = User.Identity.Name;
                    mappingCourseProgramLOVM.MappingCourseProgramLO.CreatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    mappingCourseProgramLOVM.MappingCourseProgramLO.UpdatedDate = DateTime.Now;
                    mappingCourseProgramLOVM.MappingCourseProgramLO.UpdatedBy = "N/A";
                    mappingCourseProgramLOVM.MappingCourseProgramLO.UpdatedIp = "0.0.0.0";
                    mappingCourseProgramLOVM.MappingCourseProgramLO.IsDeleted = false;
                    _unitOfWork.MappingCourseProgramLO.Add(mappingCourseProgramLOVM.MappingCourseProgramLO);

                }
                else
                {
                    mappingCourseProgramLOVM.MappingCourseProgramLO.UpdatedDate = DateTime.Now;
                    mappingCourseProgramLOVM.MappingCourseProgramLO.UpdatedBy = User.Identity.Name;
                    mappingCourseProgramLOVM.MappingCourseProgramLO.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    mappingCourseProgramLOVM.MappingCourseProgramLO.IsDeleted = false;
                    _unitOfWork.MappingCourseProgramLO.Update(mappingCourseProgramLOVM.MappingCourseProgramLO);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            ;
            mappingCourseProgramLOVM.CourseHistoryLists = _unitOfWork.CourseHistory
                .GetAll(includeProperties: "Course,Semester,Section,Instructor", filter: ch => ch.SemesterId == uniqueSetup.GetCurrentSemester().Id && ch.InstructorId == uniqueSetup.GetInstructor(User.Identity.Name).Id)
                .Select(i => new SelectListItem
                {
                    Text = i.Course.CourseCode + "(" + i.Section.SectionCode + ")-" + i.Instructor.ShortCode + ")",
                    Value = i.Id.ToString()
                });
            mappingCourseProgramLOVM.CourseLearningLists = _unitOfWork.CourseLearning.GetAll().Select(i =>
                new SelectListItem
                {
                    Text = i.CLOCode,
                    Value = i.Id.ToString()
                });
            mappingCourseProgramLOVM.ProgramLists = _unitOfWork.Program.GetAll().Select(i => new SelectListItem
            {
                Text = i.ProgramCode,
                Value = i.Id.ToString()
            });


            mappingCourseProgramLOVM.CorrelationLists = _unitOfWork.Correlation.GetAll().Select(i => new SelectListItem
            {
                Text = i.Code.ToString(),
                Value = i.Id.ToString()
            });
            return View(mappingCourseProgramLOVM);
        }

        #region API Calls

        [HttpGet]
        public IActionResult GetAll()
        {
            int maxSemester = _unitOfWork.Semester.GetAll().Max(mS => mS.Id);
            var allObj = _unitOfWork.MappingCourseProgramLO.GetAll(filter: mapCPLO => mapCPLO.CourseHistory.SemesterId == maxSemester && mapCPLO.CourseHistory.InstructorId == uniqueSetup.GetInstructor(User.Identity.Name).Id, includeProperties: "CourseHistory,CourseHistory.Course,CourseHistory.Section,CourseHistory.Instructor,CourseLearning,Program,ProgramPLO.ProgramLearning,CourseHistory.Semester,Correlation");
            return Json(new { data = allObj });
        }

        private Instructor GetInstructor()
        {
            var userInfoCheck = _unitOfWork.UserInfoCheck.GetFirstOrDefault(user => user.UserInfoId == User.Identity.Name);
            var instructor = _unitOfWork.Instructor.GetFirstOrDefault(user => user.ShortCode == userInfoCheck.ShortCode);
            return instructor;
        }


        #endregion
    }
}
