using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using ULABOBE.DataAccess.Repository.IRepository;
using ULABOBE.Models;
using ULABOBE.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using ULABOBE.Utility;

namespace ULABOBE.AppOnline.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class GenericSkillController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenericSkillController(IUnitOfWork unitOfWork)
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
            GenericSkillVM genericSkillVM = new GenericSkillVM()
            {

                GenericSkill = new GenericSkill(),
                GenericSkillTypeLists = _unitOfWork.GenericSkillType.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
            };
            if (id == null)
            {
                //this is for create
                return View(genericSkillVM);
            }
            //this is for edit
            genericSkillVM.GenericSkill = _unitOfWork.GenericSkill.Get(id.GetValueOrDefault());
            if (genericSkillVM.GenericSkill == null)
            {
                return NotFound();
            }
            return View(genericSkillVM);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Upsert(GenericSkillVM genericSkillVM)
        {

            if (ModelState.IsValid)
            {
                if (genericSkillVM.GenericSkill.Id == 0)
                {
                    genericSkillVM.GenericSkill.QueryId = Guid.NewGuid();

                    genericSkillVM.GenericSkill.CreatedDate = DateTime.Now;
                    genericSkillVM.GenericSkill.CreatedBy = User.Identity.Name;
                    genericSkillVM.GenericSkill.CreatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    genericSkillVM.GenericSkill.UpdatedDate = DateTime.Now;
                    genericSkillVM.GenericSkill.UpdatedBy = "-";
                    genericSkillVM.GenericSkill.UpdatedIp = "0.0.0.0";
                    genericSkillVM.GenericSkill.IsDeleted = false;
                    _unitOfWork.GenericSkill.Add(genericSkillVM.GenericSkill);

                }
                else
                {
                    genericSkillVM.GenericSkill.UpdatedDate = DateTime.Now;
                    //genericSkillVM.UpdatedBy = User.Identity.Name;
                    //genericSkillVM.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    genericSkillVM.GenericSkill.UpdatedBy = User.Identity.Name;
                    genericSkillVM.GenericSkill.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    genericSkillVM.GenericSkill.IsDeleted = false;
                    _unitOfWork.GenericSkill.Update(genericSkillVM.GenericSkill);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            genericSkillVM.GenericSkillTypeLists = _unitOfWork.GenericSkillType.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
           
            return View(genericSkillVM);
        }

        #region API Calls

        [HttpGet]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.GenericSkill.GetAll(includeProperties: "GenericSkillType");
            return Json(new { data = allObj });
        }


        [HttpDelete]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.GenericSkill.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.GenericSkill.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion
    }
}
