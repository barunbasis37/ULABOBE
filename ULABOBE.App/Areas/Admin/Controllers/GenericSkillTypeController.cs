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
    //[Route("admin/genericSkillType")]
    [Authorize]
    public class GenericSkillTypeController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public GenericSkillTypeController(IUnitOfWork unitOfWork)
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
            GenericSkillType genericSkillType = new GenericSkillType();
            if (id == null)
            {
                //this is for create
                return View(genericSkillType);
            }
            //this is for edit
            genericSkillType = _unitOfWork.GenericSkillType.Get(id.GetValueOrDefault());
            if (genericSkillType == null)
            {
                return NotFound();
            }
            return View(genericSkillType);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Upsert(GenericSkillType genericSkillType)
        {
            
            if (ModelState.IsValid)
            {
                if (genericSkillType.Id == 0)
                {
                    genericSkillType.QueryId = Guid.NewGuid();
                    
                    genericSkillType.CreatedDate = DateTime.Now;
                    genericSkillType.CreatedBy = User.Identity.Name;
                    genericSkillType.CreatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    genericSkillType.UpdatedDate = DateTime.MinValue;
                    genericSkillType.UpdatedBy = "-";
                    genericSkillType.UpdatedIp = "0.0.0.0";
                    genericSkillType.IsDeleted = false;
                    _unitOfWork.GenericSkillType.Add(genericSkillType);

                }
                else
                {
                    genericSkillType.UpdatedDate = DateTime.Now;
                    genericSkillType.UpdatedBy = User.Identity.Name;
                    genericSkillType.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    //genericSkillType.UpdatedBy = "423036";
                    //genericSkillType.UpdatedIp = "172.16.25.30";
                    genericSkillType.IsDeleted = false;
                    _unitOfWork.GenericSkillType.Update(genericSkillType);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(genericSkillType);
        }

        #region API Calls
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.GenericSkillType.GetAll().ToList();
            return Json(new {data = allObj});
        }


        [HttpDelete]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.GenericSkillType.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.GenericSkillType.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion
    }
}
