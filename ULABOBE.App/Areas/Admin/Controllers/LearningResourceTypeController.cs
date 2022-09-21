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
    //[Route("admin/learningResourceType")]
    [Authorize]
    public class LearningResourceTypeController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public LearningResourceTypeController(IUnitOfWork unitOfWork)
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
            LearningResourceType learningResourceType = new LearningResourceType();
            if (id == null)
            {
                //this is for create
                return View(learningResourceType);
            }
            //this is for edit
            learningResourceType = _unitOfWork.LearningResourceType.Get(id.GetValueOrDefault());
            if (learningResourceType == null)
            {
                return NotFound();
            }
            return View(learningResourceType);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Upsert(LearningResourceType learningResourceType)
        {
            
            if (ModelState.IsValid)
            {
                if (learningResourceType.Id == 0)
                {
                    learningResourceType.QueryId = Guid.NewGuid();
                    
                    learningResourceType.CreatedDate = DateTime.Now;
                    learningResourceType.CreatedBy = User.Identity.Name;
                    learningResourceType.CreatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    learningResourceType.UpdatedDate = DateTime.MinValue;
                    learningResourceType.UpdatedBy = "-";
                    learningResourceType.UpdatedIp = "0.0.0.0";
                    learningResourceType.IsDeleted = false;
                    _unitOfWork.LearningResourceType.Add(learningResourceType);

                }
                else
                {
                    learningResourceType.UpdatedDate = DateTime.Now;
                    learningResourceType.UpdatedBy = User.Identity.Name;
                    learningResourceType.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    //learningResourceType.UpdatedBy = "423036";
                    //learningResourceType.UpdatedIp = "172.16.25.30";
                    learningResourceType.IsDeleted = false;
                    _unitOfWork.LearningResourceType.Update(learningResourceType);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(learningResourceType);
        }

        #region API Calls
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.LearningResourceType.GetAll().ToList();
            return Json(new {data = allObj});
        }


        [HttpDelete]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.LearningResourceType.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.LearningResourceType.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion
    }
}
