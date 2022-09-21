using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using ULABOBE.DataAccess.Repository.IRepository;
using ULABOBE.Models;
using ULABOBE.Models.ViewModels;
using ULABOBE.Utility;

namespace ULABOBE.AppOnline.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_SuperAdmin)]
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Upsert(int? id)
        {
            DepartmentVM departmentVM = new DepartmentVM()
            {
                Department = new Department(),
                SchoolList = _unitOfWork.School.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
            };
            if (id == null)
            {
                //this is for create
                return View(departmentVM);
            }
            //this is for edit
            departmentVM.Department = _unitOfWork.Department.Get(id.GetValueOrDefault());
            if (departmentVM.Department == null)
            {
                return NotFound();
            }
            return View(departmentVM);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Upsert(DepartmentVM departmentVM)
        {

            if (ModelState.IsValid)
            {
                if (departmentVM.Department.Id == 0)
                {
                    departmentVM.Department.QueryId = Guid.NewGuid();
                    departmentVM.Department.IsActive = true;
                    departmentVM.Department.CreatedDate = DateTime.Now;
                    departmentVM.Department.CreatedBy = User.Identity.Name;
                    departmentVM.Department.CreatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    departmentVM.Department.UpdatedDate = DateTime.Now;
                    departmentVM.Department.UpdatedBy = "N/A";
                    departmentVM.Department.UpdatedIp = "N/A";
                    departmentVM.Department.IsDeleted = false;
                    _unitOfWork.Department.Add(departmentVM.Department);

                }
                else
                {
                    departmentVM.Department.UpdatedDate = DateTime.Now;
                    //department.UpdatedBy = User.Identity.Name;
                    //department.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    departmentVM.Department.UpdatedBy = User.Identity.Name;
                    departmentVM.Department.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    departmentVM.Department.IsDeleted = false;
                    _unitOfWork.Department.Update(departmentVM.Department);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            departmentVM.SchoolList = _unitOfWork.School.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            return View(departmentVM);
        }

        #region API Calls

        [HttpGet]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Department.GetAll(includeProperties: "School");
            return Json(new { data = allObj });
        }


        [HttpDelete]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Department.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.Department.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion
    }
}
