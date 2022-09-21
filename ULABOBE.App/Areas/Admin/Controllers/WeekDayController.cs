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
    //[Route("admin/weekDay")]
    [Authorize]
    public class WeekDayController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public WeekDayController(IUnitOfWork unitOfWork)
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
            WeekDay weekDay = new WeekDay();
            if (id == null)
            {
                //this is for create
                return View(weekDay);
            }
            //this is for edit
            weekDay = _unitOfWork.WeekDay.Get(id.GetValueOrDefault());
            if (weekDay == null)
            {
                return NotFound();
            }
            return View(weekDay);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Upsert(WeekDay weekDay)
        {
            
            if (ModelState.IsValid)
            {
                if (weekDay.Id == 0)
                {
                    weekDay.QueryId = Guid.NewGuid();
                    
                    weekDay.CreatedDate = DateTime.Now;
                    weekDay.CreatedBy = User.Identity.Name;
                    weekDay.CreatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    weekDay.UpdatedDate = DateTime.MinValue;
                    weekDay.UpdatedBy = "-";
                    weekDay.UpdatedIp = "0.0.0.0";
                    weekDay.IsDeleted = false;
                    _unitOfWork.WeekDay.Add(weekDay);

                }
                else
                {
                    weekDay.UpdatedDate = DateTime.Now;
                    //weekDay.UpdatedBy = User.Identity.Name;
                    //weekDay.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    weekDay.UpdatedBy = User.Identity.Name;
                    weekDay.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    weekDay.IsDeleted = false;
                    _unitOfWork.WeekDay.Update(weekDay);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(weekDay);
        }

        #region API Calls
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.WeekDay.GetAll().ToList();
            return Json(new {data = allObj});
        }


        [HttpDelete]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.WeekDay.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.WeekDay.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion
    }
}
