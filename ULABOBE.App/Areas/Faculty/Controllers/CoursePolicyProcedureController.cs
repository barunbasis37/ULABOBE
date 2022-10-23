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
    [Authorize]
    public class CoursePolicyProcedureController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private UniqueSetup uniqueSetup;
        private string userName;
        public CoursePolicyProcedureController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            uniqueSetup = new UniqueSetup(_unitOfWork);
        }
        [Authorize(Roles = SD.Role_Faculty)]
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

        [Authorize(Roles = SD.Role_Faculty)]
        public IActionResult Upsert(int? id)
        {
            GetLatestSemester();
            CoursePolicyProcedureVM coursePolicyProcedureVM = new CoursePolicyProcedureVM()
            {

                CoursePolicyProcedure = new CoursePolicyProcedure(),
                CourseHistoryLists = _unitOfWork.CourseHistory
                    .GetAll(includeProperties: "Course,Semester,Section,Instructor", filter: ch => ch.SemesterId == uniqueSetup.GetCurrentSemester().Id && ch.InstructorId == uniqueSetup.GetInstructor(User.Identity.Name).Id)
                    .Select(i => new SelectListItem
                    {
                        Text = i.Course.CourseCode + "(" + i.Section.SectionCode + ")-" + i.Instructor.ShortCode + ")",
                        Value = i.Id.ToString()
                    }),

                CoursePolicyTypeLists = _unitOfWork.CoursePolicyType.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),


            };

            if (id == null)
            {
                //this is for create
                return View(coursePolicyProcedureVM);
            }
            //this is for edit

            if (coursePolicyProcedureVM.CoursePolicyProcedure != null)
            {
                coursePolicyProcedureVM.CoursePolicyProcedure = _unitOfWork.CoursePolicyProcedure.Get(id.GetValueOrDefault());
            }

            if (coursePolicyProcedureVM.CoursePolicyProcedure == null)
            {
                return NotFound();
            }
            return View(coursePolicyProcedureVM);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_Faculty)]
        public IActionResult Upsert(CoursePolicyProcedureVM coursePolicyProcedureVM)
        {
            GetLatestSemester();
            MasterSetup aMasterSetup = uniqueSetup.GetMaxMasterSetup(coursePolicyProcedureVM.CoursePolicyProcedure.CourseHistoryId);
            if (DateTime.Now <= aMasterSetup.StartDateTime && DateTime.Now >= aMasterSetup.EndDateTime)
            {
                
                coursePolicyProcedureVM.CourseHistoryLists = _unitOfWork.CourseHistory
                    .GetAll(includeProperties: "Course,Semester,Section,Instructor", filter: ch => ch.SemesterId == uniqueSetup.GetCurrentSemester().Id && ch.InstructorId == uniqueSetup.GetInstructor(User.Identity.Name).Id)
                    .Select(i => new SelectListItem
                    {
                        Text = i.Course.CourseCode + "(" + i.Section.SectionCode + ")-" + i.Instructor.ShortCode + ")",
                        Value = i.Id.ToString()
                    }); ;


                coursePolicyProcedureVM.CoursePolicyTypeLists = _unitOfWork.CoursePolicyType.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });

            }
            else
            {
                if (ModelState.IsValid)
                {
                    if (coursePolicyProcedureVM.CoursePolicyProcedure.Id == 0)
                    {

                        coursePolicyProcedureVM.CoursePolicyProcedure.QueryId = Guid.NewGuid();
                        coursePolicyProcedureVM.CoursePolicyProcedure.CreatedDate = DateTime.Now;

                        coursePolicyProcedureVM.CoursePolicyProcedure.CreatedDate = DateTime.Now;
                        coursePolicyProcedureVM.CoursePolicyProcedure.CreatedBy = User.Identity.Name;
                        coursePolicyProcedureVM.CoursePolicyProcedure.CreatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                        coursePolicyProcedureVM.CoursePolicyProcedure.UpdatedDate = DateTime.Now;
                        coursePolicyProcedureVM.CoursePolicyProcedure.UpdatedBy = "N/A";
                        coursePolicyProcedureVM.CoursePolicyProcedure.UpdatedIp = "0.0.0.0";
                        coursePolicyProcedureVM.CoursePolicyProcedure.IsDeleted = false;
                        _unitOfWork.CoursePolicyProcedure.Add(coursePolicyProcedureVM.CoursePolicyProcedure);

                    }
                    else
                    {

                        coursePolicyProcedureVM.CoursePolicyProcedure.UpdatedDate = DateTime.Now;
                        coursePolicyProcedureVM.CoursePolicyProcedure.UpdatedBy = User.Identity.Name;
                        coursePolicyProcedureVM.CoursePolicyProcedure.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                        coursePolicyProcedureVM.CoursePolicyProcedure.IsDeleted = false;
                        _unitOfWork.CoursePolicyProcedure.Update(coursePolicyProcedureVM.CoursePolicyProcedure);
                    }
                    _unitOfWork.Save();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(coursePolicyProcedureVM);
        }




        #region API Calls

        [HttpGet]
        [Authorize(Roles = SD.Role_Faculty)]
        public IActionResult GetAll()
        {
            int maxSemester = _unitOfWork.Semester.GetAll().Max(mS => mS.Id);
            var allObj = _unitOfWork.CoursePolicyProcedure.GetAll(filter: cPPro => cPPro.CourseHistory.SemesterId == maxSemester && cPPro.CourseHistory.InstructorId == uniqueSetup.GetInstructor(User.Identity.Name).Id, includeProperties: "CourseHistory,CourseHistory.Course,CourseHistory.Section,CourseHistory.Instructor,CourseHistory.Semester,CoursePolicyType");
            return Json(new { data = allObj });
        }

        //private Instructor GetInstructor()
        //{
        //    var userInfoCheck = _unitOfWork.UserInfoCheck.GetFirstOrDefault(user => user.UserInfoId == User.Identity.Name);
        //    var instructor = _unitOfWork.Instructor.GetFirstOrDefault(user => user.ShortCode == userInfoCheck.ShortCode);
        //    return instructor;
        //}
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
