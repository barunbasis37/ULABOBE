using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ULABOBE.DataAccess.Repository.IRepository;
using ULABOBE.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using ULABOBE.Utility;

namespace ULABOBE.AppOnline.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Route("admin/learningAssessmentRubric")]
    [Authorize]
    public class LearningAssessmentRubricController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public LearningAssessmentRubricController(IUnitOfWork unitOfWork)
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
            LearningAssessmentRubric learningAssessmentRubric = new LearningAssessmentRubric();
            if (id == null)
            {
                //this is for create
                return View(learningAssessmentRubric);
            }
            //this is for edit
            learningAssessmentRubric = _unitOfWork.LearningAssessmentRubric.Get(id.GetValueOrDefault());
            if (learningAssessmentRubric == null)
            {
                return NotFound();
            }
            return View(learningAssessmentRubric);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Upsert(LearningAssessmentRubric learningAssessmentRubric)
        {
            
            if (ModelState.IsValid)
            {
                if (learningAssessmentRubric.Id == 0)
                {
                    learningAssessmentRubric.QueryId = Guid.NewGuid();
                    learningAssessmentRubric.IsActive = true;
                    learningAssessmentRubric.CreatedDate = DateTime.Now;
                    learningAssessmentRubric.CreatedBy = User.Identity.Name;
                    learningAssessmentRubric.CreatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    learningAssessmentRubric.UpdatedDate = DateTime.MinValue;
                    learningAssessmentRubric.UpdatedBy = "-";
                    learningAssessmentRubric.UpdatedIp = "0.0.0.0";
                    learningAssessmentRubric.IsDeleted = false;
                    _unitOfWork.LearningAssessmentRubric.Add(learningAssessmentRubric);

                }
                else
                {
                    learningAssessmentRubric.UpdatedDate = DateTime.Now;
                    learningAssessmentRubric.UpdatedBy = User.Identity.Name;
                    learningAssessmentRubric.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    learningAssessmentRubric.IsDeleted = false;
                    _unitOfWork.LearningAssessmentRubric.Update(learningAssessmentRubric);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(learningAssessmentRubric);
        }

        #region API Calls
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.LearningAssessmentRubric.GetAll().ToList();
            return Json(new {data = allObj});
        }


        [HttpDelete]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.LearningAssessmentRubric.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.LearningAssessmentRubric.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion
    }
}
