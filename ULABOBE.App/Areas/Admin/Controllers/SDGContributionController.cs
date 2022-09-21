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
    //[Route("admin/sDGContribution")]
    [Authorize]
    public class SDGContributionController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public SDGContributionController(IUnitOfWork unitOfWork)
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
            SDGContribution sDGContribution = new SDGContribution();
            if (id == null)
            {
                //this is for create
                return View(sDGContribution);
            }
            //this is for edit
            sDGContribution = _unitOfWork.SDGContribution.Get(id.GetValueOrDefault());
            if (sDGContribution == null)
            {
                return NotFound();
            }
            return View(sDGContribution);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Upsert(SDGContribution sDGContribution)
        {
            
            if (ModelState.IsValid)
            {
                if (sDGContribution.Id == 0)
                {
                    sDGContribution.QueryId = Guid.NewGuid();
                    sDGContribution.IsActive = true;
                    sDGContribution.CreatedDate = DateTime.Now;
                    sDGContribution.CreatedBy = User.Identity.Name;
                    sDGContribution.CreatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    sDGContribution.UpdatedDate = DateTime.MinValue;
                    sDGContribution.UpdatedBy = "-";
                    sDGContribution.UpdatedIp = "0.0.0.0";
                    sDGContribution.IsDeleted = false;
                    _unitOfWork.SDGContribution.Add(sDGContribution);

                }
                else
                {
                    sDGContribution.UpdatedDate = DateTime.Now;
                    //sDGContribution.UpdatedBy = User.Identity.Name;
                    //sDGContribution.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    sDGContribution.UpdatedBy = User.Identity.Name;
                    sDGContribution.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    sDGContribution.IsDeleted = false;
                    _unitOfWork.SDGContribution.Update(sDGContribution);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(sDGContribution);
        }

        #region API Calls
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.SDGContribution.GetAll().ToList();
            return Json(new {data = allObj});
        }


        [HttpDelete]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.SDGContribution.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.SDGContribution.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion
    }
}
