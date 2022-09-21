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
    public class ProgramController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProgramController(IUnitOfWork unitOfWork)
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
            ProgramVM programVM = new ProgramVM()
            {
                Program = new ProgramData(),
                DepartmentList = _unitOfWork.Department.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
            };
            if (id == null)
            {
                //this is for create
                return View(programVM);
            }
            //this is for edit
            programVM.Program = _unitOfWork.Program.Get(id.GetValueOrDefault());
            if (programVM.Program == null)
            {
                return NotFound();
            }
            return View(programVM);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Upsert(ProgramVM programVM)
        {

            if (ModelState.IsValid)
            {
                if (programVM.Program.Id == 0)
                {
                    programVM.Program.QueryId = Guid.NewGuid();
                    programVM.Program.IsActive = true;
                    programVM.Program.CreatedDate = DateTime.Now;
                    programVM.Program.CreatedBy = User.Identity.Name;
                    programVM.Program.CreatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    programVM.Program.UpdatedDate = DateTime.Now;
                    programVM.Program.UpdatedBy = "-";
                    programVM.Program.UpdatedIp = "0.0.0.0";
                    programVM.Program.IsDeleted = false;
                    _unitOfWork.Program.Add(programVM.Program);

                }
                else
                {
                    programVM.Program.UpdatedDate = DateTime.Now;
                    programVM.Program.UpdatedBy = User.Identity.Name;
                    programVM.Program.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    //programVM.Program.UpdatedBy = "423036";
                    //programVM.Program.UpdatedIp = "172.16.25.30";
                    programVM.Program.IsDeleted = false;
                    _unitOfWork.Program.Update(programVM.Program);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            programVM.DepartmentList = _unitOfWork.Department.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            return View(programVM);
        }

        #region API Calls

        [HttpGet]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Program.GetAll(includeProperties: "Department");
            return Json(new { data = allObj });
        }


        [HttpDelete]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Program.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.Program.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion
    }
}
