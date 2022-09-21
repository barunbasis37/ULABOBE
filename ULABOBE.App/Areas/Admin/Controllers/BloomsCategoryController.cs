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
    //[Route("admin/bloomsCategory")]
    [Authorize]
    public class BloomsCategoryController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public BloomsCategoryController(IUnitOfWork unitOfWork)
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
            BloomsCategory bloomsCategory = new BloomsCategory();
            if (id == null)
            {
                //this is for create
                return View(bloomsCategory);
            }
            //this is for edit
            bloomsCategory = _unitOfWork.BloomsCategory.Get(id.GetValueOrDefault());
            if (bloomsCategory == null)
            {
                return NotFound();
            }
            return View(bloomsCategory);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Upsert(BloomsCategory bloomsCategory)
        {
            
            if (ModelState.IsValid)
            {
                if (bloomsCategory.Id == 0)
                {
                    bloomsCategory.QueryId = Guid.NewGuid();
                    
                    bloomsCategory.CreatedDate = DateTime.Now;
                    bloomsCategory.CreatedBy = User.Identity.Name;
                    bloomsCategory.CreatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    bloomsCategory.UpdatedDate = DateTime.MinValue;
                    bloomsCategory.UpdatedBy = "-";
                    bloomsCategory.UpdatedIp = "0.0.0.0";
                    bloomsCategory.IsDeleted = false;
                    _unitOfWork.BloomsCategory.Add(bloomsCategory);

                }
                else
                {
                    bloomsCategory.UpdatedDate = DateTime.Now;
                    //bloomsCategory.UpdatedBy = User.Identity.Name;
                    //bloomsCategory.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    bloomsCategory.UpdatedBy = "000000";
                    bloomsCategory.UpdatedIp = "0.0.0.0";
                    bloomsCategory.IsDeleted = false;
                    _unitOfWork.BloomsCategory.Update(bloomsCategory);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(bloomsCategory);
        }

        #region API Calls
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.BloomsCategory.GetAll().ToList();
            return Json(new {data = allObj});
        }


        [HttpDelete]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.BloomsCategory.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.BloomsCategory.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion
    }
}
