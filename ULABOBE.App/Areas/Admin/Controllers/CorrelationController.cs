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
    //[Route("admin/Correlation")]
    [Authorize]
    public class CorrelationController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public CorrelationController(IUnitOfWork unitOfWork)
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
            Correlation correlation = new Correlation();
            if (id == null)
            {
                //this is for create
                return View(correlation);
            }
            //this is for edit
            correlation = _unitOfWork.Correlation.Get(id.GetValueOrDefault());
            if (correlation == null)
            {
                return NotFound();
            }
            return View(correlation);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Upsert(Correlation correlation)
        {
            
            if (ModelState.IsValid)
            {
                if (correlation.Id == 0)
                {
                    correlation.QueryId = Guid.NewGuid();
                    correlation.CreatedDate = DateTime.Now;
                    correlation.CreatedBy = User.Identity.Name;
                    correlation.CreatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    correlation.UpdatedDate = DateTime.MinValue;
                    correlation.UpdatedBy = "-";
                    correlation.UpdatedIp = "0.0.0.0";
                    correlation.IsDeleted = false;
                    _unitOfWork.Correlation.Add(correlation);

                }
                else
                {
                    correlation.UpdatedDate = DateTime.Now;
                    correlation.UpdatedBy = User.Identity.Name;
                    correlation.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    correlation.IsDeleted = false;
                    _unitOfWork.Correlation.Update(correlation);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(correlation);
        }

        #region API Calls
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Correlation.GetAll().ToList();
            return Json(new {data = allObj});
        }


        [HttpDelete]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Correlation.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.Correlation.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion
    }
}
