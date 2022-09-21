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
    public class DepartmentLARController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentLARController(IUnitOfWork unitOfWork)
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
            DepartmentLARVM departmentLARVM = new DepartmentLARVM()
            {
                DepartmentLAR = new DepartmentLAR(),
                LearningARLists = _unitOfWork.LearningAssessmentRubric.GetAll().Select(i => new SelectListItem
                {
                    Text = i.LARCode,
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
                return View(departmentLARVM);
            }
            //this is for edit
            departmentLARVM.DepartmentLAR = _unitOfWork.DepartmentLAR.Get(id.GetValueOrDefault());
            if (departmentLARVM.DepartmentLAR == null)
            {
                return NotFound();
            }
            return View(departmentLARVM);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Upsert(DepartmentLARVM DepartmentLARVM)
        {

            if (ModelState.IsValid)
            {
                if (DepartmentLARVM.DepartmentLAR.Id == 0)
                {
                    DepartmentLARVM.DepartmentLAR.QueryId = Guid.NewGuid();
                    
                    DepartmentLARVM.DepartmentLAR.CreatedDate = DateTime.Now;
                    DepartmentLARVM.DepartmentLAR.CreatedBy = User.Identity.Name;
                    DepartmentLARVM.DepartmentLAR.CreatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    DepartmentLARVM.DepartmentLAR.UpdatedDate = DateTime.Now;
                    DepartmentLARVM.DepartmentLAR.UpdatedBy = "N/A";
                    DepartmentLARVM.DepartmentLAR.UpdatedIp = "0.0.0.0";
                    DepartmentLARVM.DepartmentLAR.IsDeleted = false;
                    _unitOfWork.DepartmentLAR.Add(DepartmentLARVM.DepartmentLAR);

                }
                else
                {
                    DepartmentLARVM.DepartmentLAR.UpdatedDate = DateTime.Now;
                    //DepartmentLAR.UpdatedBy = User.Identity.Name;
                    //DepartmentLAR.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    DepartmentLARVM.DepartmentLAR.UpdatedBy = User.Identity.Name;
                    DepartmentLARVM.DepartmentLAR.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    DepartmentLARVM.DepartmentLAR.IsDeleted = false;
                    _unitOfWork.DepartmentLAR.Update(DepartmentLARVM.DepartmentLAR);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            DepartmentLARVM.DepartmentLists = _unitOfWork.Department.GetAll().Select(i => new SelectListItem
            {
                Text = i.DepartmentCode,
                Value = i.Id.ToString()
            });

            DepartmentLARVM.SemesterLists = _unitOfWork.Semester.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            DepartmentLARVM.LearningARLists = _unitOfWork.LearningAssessmentRubric.GetAll().Select(i => new SelectListItem
            {
                Text = i.LARCode,
                Value = i.Id.ToString()
            });
            return View(DepartmentLARVM);
        }

        #region API Calls

        [HttpGet]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.DepartmentLAR.GetAll(includeProperties: "Department,Semester,LearningAssessmentRubric");
            return Json(new { data = allObj });
        }
        

        [HttpDelete]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.DepartmentLAR.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.DepartmentLAR.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion
    }
}
