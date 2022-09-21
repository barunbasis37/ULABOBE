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
    //[Route("admin/section")]
    [Authorize]
    public class SectionController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public SectionController(IUnitOfWork unitOfWork)
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
            Section section = new Section();
            if (id == null)
            {
                //this is for create
                return View(section);
            }
            //this is for edit
            section = _unitOfWork.Section.Get(id.GetValueOrDefault());
            if (section == null)
            {
                return NotFound();
            }
            return View(section);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Upsert(Section section)
        {
            
            if (ModelState.IsValid)
            {
                if (section.Id == 0)
                {
                    section.QueryId = Guid.NewGuid();
                    section.IsActive = true;
                    section.CreatedDate = DateTime.Now;
                    section.CreatedBy = User.Identity.Name;
                    section.CreatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    section.UpdatedDate = DateTime.MinValue;
                    section.UpdatedBy = "-";
                    section.UpdatedIp = "0.0.0.0";
                    section.IsDeleted = false;
                    _unitOfWork.Section.Add(section);

                }
                else
                {
                    section.UpdatedDate = DateTime.Now;
                    //section.UpdatedBy = User.Identity.Name;
                    //section.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    section.UpdatedBy = User.Identity.Name;
                    section.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    section.IsDeleted = false;
                    _unitOfWork.Section.Update(section);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(section);
        }

        #region API Calls
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Section.GetAll().ToList();
            return Json(new {data = allObj});
        }


        [HttpDelete]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Section.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.Section.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion
    }
}
