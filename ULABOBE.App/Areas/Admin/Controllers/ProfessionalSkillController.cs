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
    //[Route("admin/professionalSkill")]
    [Authorize]
    public class ProfessionalSkillController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public ProfessionalSkillController(IUnitOfWork unitOfWork)
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
            ProfessionalSkill professionalSkill = new ProfessionalSkill();
            if (id == null)
            {
                //this is for create
                return View(professionalSkill);
            }
            //this is for edit
            professionalSkill = _unitOfWork.ProfessionalSkill.Get(id.GetValueOrDefault());
            if (professionalSkill == null)
            {
                return NotFound();
            }
            return View(professionalSkill);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Upsert(ProfessionalSkill professionalSkill)
        {
            
            if (ModelState.IsValid)
            {
                if (professionalSkill.Id == 0)
                {
                    professionalSkill.QueryId = Guid.NewGuid();
                    professionalSkill.IsActive = true;
                    professionalSkill.CreatedDate = DateTime.Now;
                    professionalSkill.CreatedBy = User.Identity.Name;
                    professionalSkill.CreatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    professionalSkill.UpdatedDate = DateTime.MinValue;
                    professionalSkill.UpdatedBy = "-";
                    professionalSkill.UpdatedIp = "0.0.0.0";
                    professionalSkill.IsDeleted = false;
                    _unitOfWork.ProfessionalSkill.Add(professionalSkill);

                }
                else
                {
                    professionalSkill.UpdatedDate = DateTime.Now;
                    //professionalSkill.UpdatedBy = User.Identity.Name;
                    //professionalSkill.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    professionalSkill.UpdatedBy = User.Identity.Name;
                    professionalSkill.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    professionalSkill.IsDeleted = false;
                    _unitOfWork.ProfessionalSkill.Update(professionalSkill);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(professionalSkill);
        }

        #region API Calls
        [HttpGet]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.ProfessionalSkill.GetAll().ToList();
            return Json(new {data = allObj});
        }


        [HttpDelete]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.ProfessionalSkill.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.ProfessionalSkill.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion
    }
}
