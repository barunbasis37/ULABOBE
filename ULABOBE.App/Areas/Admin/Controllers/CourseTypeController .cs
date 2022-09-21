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
    //[Route("admin/courseType")]
    [Authorize]
    public class CourseTypeController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public CourseTypeController(IUnitOfWork unitOfWork)
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
            CourseType courseType = new CourseType();
            if (id == null)
            {
                //this is for create
                return View(courseType);
            }
            //this is for edit
            courseType = _unitOfWork.CourseType.Get(id.GetValueOrDefault());
            if (courseType == null)
            {
                return NotFound();
            }
            return View(courseType);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Upsert(CourseType courseType)
        {
            
            if (ModelState.IsValid)
            {
                if (courseType.Id == 0)
                {
                    courseType.QueryId = Guid.NewGuid();
                    
                    courseType.CreatedDate = DateTime.Now;
                    courseType.CreatedBy = User.Identity.Name;
                    courseType.CreatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    courseType.UpdatedDate = DateTime.MinValue;
                    courseType.UpdatedBy = "-";
                    courseType.UpdatedIp = "0.0.0.0";
                    courseType.IsDeleted = false;
                    _unitOfWork.CourseType.Add(courseType);

                }
                else
                {
                    courseType.UpdatedDate = DateTime.Now;
                    //courseType.UpdatedBy = User.Identity.Name;
                    //courseType.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    courseType.UpdatedBy = User.Identity.Name;
                    courseType.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    courseType.IsDeleted = false;
                    _unitOfWork.CourseType.Update(courseType);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(courseType);
        }

        #region API Calls
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.CourseType.GetAll().ToList();
            return Json(new {data = allObj});
        }


        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.CourseType.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.CourseType.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion
    }
}
