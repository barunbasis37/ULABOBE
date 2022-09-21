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
    //[Route("admin/courseLearningType")]
    [Authorize]
    public class CourseLearningTypeController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public CourseLearningTypeController(IUnitOfWork unitOfWork)
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
            CourseLearningType courseLearningType = new CourseLearningType();
            if (id == null)
            {
                //this is for create
                return View(courseLearningType);
            }
            //this is for edit
            courseLearningType = _unitOfWork.CourseLearningType.Get(id.GetValueOrDefault());
            if (courseLearningType == null)
            {
                return NotFound();
            }
            return View(courseLearningType);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Upsert(CourseLearningType courseLearningType)
        {
            
            if (ModelState.IsValid)
            {
                if (courseLearningType.Id == 0)
                {
                    courseLearningType.QueryId = Guid.NewGuid();
                    courseLearningType.IsActive = true;
                    courseLearningType.CreatedDate = DateTime.Now;
                    courseLearningType.CreatedBy = User.Identity.Name;
                    courseLearningType.CreatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    courseLearningType.UpdatedDate = DateTime.MinValue;
                    courseLearningType.UpdatedBy = "-";
                    courseLearningType.UpdatedIp = "0.0.0.0";
                    courseLearningType.IsDeleted = false;
                    _unitOfWork.CourseLearningType.Add(courseLearningType);

                }
                else
                {
                    courseLearningType.UpdatedDate = DateTime.Now;
                    //courseLearningType.UpdatedBy = User.Identity.Name;
                    //courseLearningType.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    courseLearningType.UpdatedBy = User.Identity.Name;
                    courseLearningType.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    courseLearningType.IsDeleted = false;
                    _unitOfWork.CourseLearningType.Update(courseLearningType);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(courseLearningType);
        }

        #region API Calls
        [HttpGet]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.CourseLearningType.GetAll().ToList();
            return Json(new {data = allObj});
        }


        [HttpDelete]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.CourseLearningType.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.CourseLearningType.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion
    }
}
