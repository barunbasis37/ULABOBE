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
    public class SemesterController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public SemesterController(IUnitOfWork unitOfWork)
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
            SemesterVM semesterVM = new SemesterVM()
            {
                Semester = new Semester(),
                SessionLists = _unitOfWork.SessionYear.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                TermLists = _unitOfWork.LevelTerm.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Level,
                    Value = i.Id.ToString()
                }),
            };
            if (id == null)
            {
                //this is for create
                return View(semesterVM);
            }
            //this is for edit
            semesterVM.Semester = _unitOfWork.Semester.Get(id.GetValueOrDefault());
            if (semesterVM.Semester == null)
            {
                return NotFound();
            }
            return View(semesterVM);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Upsert(SemesterVM semesterVM)
        {

            if (ModelState.IsValid)
            {
                if (semesterVM.Semester.Id == 0)
                {
                    semesterVM.Semester.QueryId = Guid.NewGuid();
                    
                    semesterVM.Semester.CreatedDate = DateTime.Now;
                    semesterVM.Semester.CreatedBy = User.Identity.Name;
                    semesterVM.Semester.CreatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    semesterVM.Semester.UpdatedDate = DateTime.Now;
                    semesterVM.Semester.UpdatedBy = "-";
                    semesterVM.Semester.UpdatedIp = "0.0.0.0";
                    semesterVM.Semester.IsDeleted = false;
                    _unitOfWork.Semester.Add(semesterVM.Semester);

                }
                else
                {
                    semesterVM.Semester.UpdatedDate = DateTime.Now;
                    semesterVM.Semester.UpdatedBy = User.Identity.Name;
                    semesterVM.Semester.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    //semesterVM.Semester.UpdatedBy = "423036";
                    //semesterVM.Semester.UpdatedIp = "172.16.25.30";
                    semesterVM.Semester.IsDeleted = false;
                    _unitOfWork.Semester.Update(semesterVM.Semester);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            semesterVM.SessionLists = _unitOfWork.SessionYear.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            semesterVM.TermLists = _unitOfWork.LevelTerm.GetAll().Select(i => new SelectListItem
            {
                Text = i.Level,
                Value = i.Id.ToString()
            });
            return View(semesterVM);
        }

        #region API Calls

        [HttpGet]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Semester.GetAll(includeProperties: "Term,Session");
            return Json(new { data = allObj });
        }


        [HttpDelete]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Semester.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.Semester.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion
    }
}
