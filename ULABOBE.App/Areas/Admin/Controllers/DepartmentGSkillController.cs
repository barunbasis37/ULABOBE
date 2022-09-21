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
    public class DepartmentGSkillController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentGSkillController(IUnitOfWork unitOfWork)
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
            DepartmentGSkillVM departmentGSkillVM = new DepartmentGSkillVM()
            {
                DepartmentGSkill = new DepartmentGSkill(),
                GenericSkillLists = _unitOfWork.GenericSkill.GetAll().Select(i => new SelectListItem
                {
                    Text = i.GSCode,
                    Value = i.Id.ToString()
                }),
                
                SemesterLists = _unitOfWork.Semester.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                DepartmentLists = _unitOfWork.Department.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name +" (" +i.DepartmentCode+")",
                    Value = i.Id.ToString()
                }),
            };
            if (id == null)
            {
                //this is for create
                return View(departmentGSkillVM);
            }
            //this is for edit
            departmentGSkillVM.DepartmentGSkill = _unitOfWork.DepartmentGSkill.Get(id.GetValueOrDefault());
            if (departmentGSkillVM.DepartmentGSkill == null)
            {
                return NotFound();
            }
            return View(departmentGSkillVM);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Upsert(DepartmentGSkillVM DepartmentGSkillVM)
        {

            if (ModelState.IsValid)
            {
                if (DepartmentGSkillVM.DepartmentGSkill.Id == 0)
                {
                    DepartmentGSkillVM.DepartmentGSkill.QueryId = Guid.NewGuid();
                    
                    DepartmentGSkillVM.DepartmentGSkill.CreatedDate = DateTime.Now;
                    DepartmentGSkillVM.DepartmentGSkill.CreatedBy = User.Identity.Name;
                    DepartmentGSkillVM.DepartmentGSkill.CreatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    DepartmentGSkillVM.DepartmentGSkill.UpdatedDate = DateTime.Now;
                    DepartmentGSkillVM.DepartmentGSkill.UpdatedBy = "N/A";
                    DepartmentGSkillVM.DepartmentGSkill.UpdatedIp = "0.0.0.0";
                    DepartmentGSkillVM.DepartmentGSkill.IsDeleted = false;
                    _unitOfWork.DepartmentGSkill.Add(DepartmentGSkillVM.DepartmentGSkill);

                }
                else
                {
                    DepartmentGSkillVM.DepartmentGSkill.UpdatedDate = DateTime.Now;
                    //DepartmentGSkill.UpdatedBy = User.Identity.Name;
                    //DepartmentGSkill.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    DepartmentGSkillVM.DepartmentGSkill.UpdatedBy = User.Identity.Name;
                    DepartmentGSkillVM.DepartmentGSkill.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    DepartmentGSkillVM.DepartmentGSkill.IsDeleted = false;
                    _unitOfWork.DepartmentGSkill.Update(DepartmentGSkillVM.DepartmentGSkill);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            DepartmentGSkillVM.DepartmentLists = _unitOfWork.Department.GetAll().Select(i => new SelectListItem
            {
                Text = i.DepartmentCode,
                Value = i.Id.ToString()
            });

            DepartmentGSkillVM.SemesterLists = _unitOfWork.Semester.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            DepartmentGSkillVM.GenericSkillLists = _unitOfWork.GenericSkill.GetAll().Select(i => new SelectListItem
            {
                Text = i.GSCode,
                Value = i.Id.ToString()
            });
            return View(DepartmentGSkillVM);
        }

        #region API Calls

        [HttpGet]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.DepartmentGSkill.GetAll(includeProperties: "Department,Semester,GenericSkill");
            return Json(new { data = allObj });
        }
        

        [HttpDelete]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.DepartmentGSkill.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.DepartmentGSkill.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion
    }
}
