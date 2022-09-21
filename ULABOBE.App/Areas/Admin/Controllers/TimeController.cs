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
    //[Route("admin/time")]
    [Authorize]
    public class TimeController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public TimeController(IUnitOfWork unitOfWork)
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
            Time time = new Time();
            if (id == null)
            {
                //this is for create
                return View(time);
            }
            //this is for edit
            time = _unitOfWork.Time.Get(id.GetValueOrDefault());
            if (time == null)
            {
                return NotFound();
            }
            return View(time);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Upsert(Time time)
        {
            
            if (ModelState.IsValid)
            {
                if (time.Id == 0)
                {
                    time.QueryId = Guid.NewGuid();
                    
                    time.CreatedDate = DateTime.Now;
                    time.CreatedBy = User.Identity.Name;
                    time.CreatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    time.UpdatedDate = DateTime.MinValue;
                    time.UpdatedBy = "-";
                    time.UpdatedIp = "0.0.0.0";
                    time.IsDeleted = false;
                    _unitOfWork.Time.Add(time);

                }
                else
                {
                    time.UpdatedDate = DateTime.Now;
                    //time.UpdatedBy = User.Identity.Name;
                    //time.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    time.UpdatedBy = User.Identity.Name;
                    time.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    time.IsDeleted = false;
                    _unitOfWork.Time.Update(time);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(time);
        }

        #region API Calls
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Time.GetAll().ToList();
            return Json(new {data = allObj});
        }


        [HttpDelete]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Time.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.Time.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion
    }
}
