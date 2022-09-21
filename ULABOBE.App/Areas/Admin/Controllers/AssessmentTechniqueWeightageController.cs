﻿using Microsoft.AspNetCore.Mvc;
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
    public class AssessmentTechniqueWeightageController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AssessmentTechniqueWeightageController(IUnitOfWork unitOfWork)
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
            AssessmentTechniqueWeightageVM assessmentTechniqueWeightageVM = new AssessmentTechniqueWeightageVM()
            {
                AssessmentTechniqueWeightage = new AssessmentTechniqueWeightage(),
                AssessmentTypeLists = _unitOfWork.AssessmentType.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Code+"("+ i.Name+")",
                    Value = i.Id.ToString()
                }),
                CourseHistoryLists = _unitOfWork.CourseHistory
                    .GetAll(includeProperties: "Course,Semester,Section,Instructor", filter: ch => ch.SemesterId == uniqueSetup.GetCurrentSemester().Id)
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
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Upsert(AssessmentTechniqueWeightageVM assessmentTechniqueWeightageVM)
        {

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
                .GetAll(includeProperties: "Course,Semester,Section,Instructor", filter: ch => ch.SemesterId == uniqueSetup.GetCurrentSemester().Id)
                .Select(i => new SelectListItem
                {
                    Text = i.Course.CourseCode + "(" + i.Section.SectionCode + ")-" + i.Instructor.ShortCode + ")",
                    Value = i.Id.ToString()
                });
            return View(assessmentTechniqueWeightageVM);
        }

        #region API Calls

        [HttpGet]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.AssessmentTechniqueWeightage.GetAll(includeProperties: "CourseHistory,CourseHistory.Course,CourseHistory.Section,CourseHistory.Instructor,CourseHistory.Semester,,AssessmentType");
            return Json(new { data = allObj });
        }


        [HttpDelete]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.AssessmentTechniqueWeightage.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.AssessmentTechniqueWeightage.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion
    }
}
