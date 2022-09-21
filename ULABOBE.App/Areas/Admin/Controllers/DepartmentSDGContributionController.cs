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
    public class DepartmentSDGContributionController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentSDGContributionController(IUnitOfWork unitOfWork)
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
            DepartmentSDGContributionVM departmentSDGContributionVM = new DepartmentSDGContributionVM()
            {
                DepartmentSDGContribution = new DepartmentSDGContribution(),
                SDGContributionLists = _unitOfWork.SDGContribution.GetAll().Select(i => new SelectListItem
                {
                    Text = i.SDGCode,
                    Value = i.Id.ToString()
                }),
                //TermLevelLists = _unitOfWork.LevelTerm.GetAll().Select(i => new SelectListItem
                //{
                //    Text = "Level-" + i.Level,
                //    Value = i.Id.ToString()
                //}),
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
                return View(departmentSDGContributionVM);
            }
            //this is for edit
            departmentSDGContributionVM.DepartmentSDGContribution = _unitOfWork.DepartmentSDGContribution.Get(id.GetValueOrDefault());
            if (departmentSDGContributionVM.DepartmentSDGContribution == null)
            {
                return NotFound();
            }
            return View(departmentSDGContributionVM);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Upsert(DepartmentSDGContributionVM DepartmentSDGContributionVM)
        {

            if (ModelState.IsValid)
            {
                if (DepartmentSDGContributionVM.DepartmentSDGContribution.Id == 0)
                {
                    DepartmentSDGContributionVM.DepartmentSDGContribution.QueryId = Guid.NewGuid();
                    
                    DepartmentSDGContributionVM.DepartmentSDGContribution.CreatedDate = DateTime.Now;
                    DepartmentSDGContributionVM.DepartmentSDGContribution.CreatedBy = User.Identity.Name;
                    DepartmentSDGContributionVM.DepartmentSDGContribution.CreatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    DepartmentSDGContributionVM.DepartmentSDGContribution.UpdatedDate = DateTime.Now;
                    DepartmentSDGContributionVM.DepartmentSDGContribution.UpdatedBy = "N/A";
                    DepartmentSDGContributionVM.DepartmentSDGContribution.UpdatedIp = "0.0.0.0";
                    DepartmentSDGContributionVM.DepartmentSDGContribution.IsDeleted = false;
                    _unitOfWork.DepartmentSDGContribution.Add(DepartmentSDGContributionVM.DepartmentSDGContribution);

                }
                else
                {
                    DepartmentSDGContributionVM.DepartmentSDGContribution.UpdatedDate = DateTime.Now;
                    //DepartmentSDGContribution.UpdatedBy = User.Identity.Name;
                    //DepartmentSDGContribution.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    DepartmentSDGContributionVM.DepartmentSDGContribution.UpdatedBy = User.Identity.Name;
                    DepartmentSDGContributionVM.DepartmentSDGContribution.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    DepartmentSDGContributionVM.DepartmentSDGContribution.IsDeleted = false;
                    _unitOfWork.DepartmentSDGContribution.Update(DepartmentSDGContributionVM.DepartmentSDGContribution);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            DepartmentSDGContributionVM.DepartmentLists = _unitOfWork.Department.GetAll().Select(i => new SelectListItem
            {
                Text = i.DepartmentCode,
                Value = i.Id.ToString()
            });
            //DepartmentSDGContributionVM.TermLevelLists = _unitOfWork.LevelTerm.GetAll().Select(i => new SelectListItem
            //{
            //    Text = "Level-" + i.Level,
            //    Value = i.Id.ToString()
            //});
            DepartmentSDGContributionVM.SemesterLists = _unitOfWork.Semester.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            DepartmentSDGContributionVM.SDGContributionLists = _unitOfWork.SDGContribution.GetAll().Select(i => new SelectListItem
            {
                Text = i.SDGCode,
                Value = i.Id.ToString()
            });
            return View(DepartmentSDGContributionVM);
        }

        #region API Calls

        [HttpGet]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.DepartmentSDGContribution.GetAll(includeProperties: "Department,Semester,SDGContribution");
            return Json(new { data = allObj });
        }
        

        [HttpDelete]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.DepartmentSDGContribution.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.DepartmentSDGContribution.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion
    }
}
