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
    //[Route("admin/coursePolicyType")]
    [Authorize]
    public class CoursePolicyTypeController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public CoursePolicyTypeController(IUnitOfWork unitOfWork)
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
            CoursePolicyType coursePolicyType = new CoursePolicyType();
            if (id == null)
            {
                //this is for create
                return View(coursePolicyType);
            }
            //this is for edit
            coursePolicyType = _unitOfWork.CoursePolicyType.Get(id.GetValueOrDefault());
            if (coursePolicyType == null)
            {
                return NotFound();
            }
            return View(coursePolicyType);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Upsert(CoursePolicyType coursePolicyType)
        {
            
            if (ModelState.IsValid)
            {
                if (coursePolicyType.Id == 0)
                {
                    coursePolicyType.QueryId = Guid.NewGuid();
                    
                    coursePolicyType.CreatedDate = DateTime.Now;
                    coursePolicyType.CreatedBy = User.Identity.Name;
                    coursePolicyType.CreatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    coursePolicyType.UpdatedDate = DateTime.MinValue;
                    coursePolicyType.UpdatedBy = "-";
                    coursePolicyType.UpdatedIp = "0.0.0.0";
                    coursePolicyType.IsDeleted = false;
                    _unitOfWork.CoursePolicyType.Add(coursePolicyType);

                }
                else
                {
                    coursePolicyType.UpdatedDate = DateTime.Now;
                    //coursePolicyType.UpdatedBy = User.Identity.Name;
                    //coursePolicyType.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    coursePolicyType.UpdatedBy = User.Identity.Name;
                    coursePolicyType.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    coursePolicyType.IsDeleted = false;
                    _unitOfWork.CoursePolicyType.Update(coursePolicyType);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(coursePolicyType);
        }

        #region API Calls
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.CoursePolicyType.GetAll().ToList();
            return Json(new {data = allObj});
        }


        [HttpDelete]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.CoursePolicyType.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.CoursePolicyType.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion
    }
}
