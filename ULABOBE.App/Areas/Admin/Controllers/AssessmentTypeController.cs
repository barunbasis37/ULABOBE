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
    //[Route("admin/assessmentType")]
    [Authorize]
    public class AssessmentTypeController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public AssessmentTypeController(IUnitOfWork unitOfWork)
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
            AssessmentType assessmentType = new AssessmentType();
            if (id == null)
            {
                //this is for create
                return View(assessmentType);
            }
            //this is for edit
            assessmentType = _unitOfWork.AssessmentType.Get(id.GetValueOrDefault());
            if (assessmentType == null)
            {
                return NotFound();
            }
            return View(assessmentType);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Upsert(AssessmentType assessmentType)
        {
            
            if (ModelState.IsValid)
            {
                if (assessmentType.Id == 0)
                {
                    assessmentType.QueryId = Guid.NewGuid();
                    
                    assessmentType.CreatedDate = DateTime.Now;
                    assessmentType.CreatedBy = User.Identity.Name;
                    assessmentType.CreatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    assessmentType.UpdatedDate = DateTime.MinValue;
                    assessmentType.UpdatedBy = "-";
                    assessmentType.UpdatedIp = "0.0.0.0";
                    assessmentType.IsDeleted = false;
                    _unitOfWork.AssessmentType.Add(assessmentType);

                }
                else
                {
                    assessmentType.UpdatedDate = DateTime.Now;
                    //assessmentType.UpdatedBy = User.Identity.Name;
                    //assessmentType.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    assessmentType.UpdatedBy = "423036";
                    assessmentType.UpdatedIp = "172.16.25.30";
                    assessmentType.IsDeleted = false;
                    _unitOfWork.AssessmentType.Update(assessmentType);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(assessmentType);
        }

        #region API Calls
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.AssessmentType.GetAll().ToList();
            return Json(new {data = allObj});
        }


        [HttpDelete]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.AssessmentType.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.AssessmentType.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion
    }
}
