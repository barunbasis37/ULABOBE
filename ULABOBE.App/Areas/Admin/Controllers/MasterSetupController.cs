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
    public class MasterSetupController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public MasterSetupController(IUnitOfWork unitOfWork)
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
            MasterSetupVM masterSetupVM = new MasterSetupVM()
            {
                MasterSetup = new MasterSetup(),
                ProgramLists = _unitOfWork.Program.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name+" ("+ i.ProgramCode+")",
                    Value = i.Id.ToString()
                }),
                SemesterLists = _unitOfWork.Semester.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name + " (" + i.Code + ")",
                    Value = i.Id.ToString()
                }),
            };
            if (id == null)
            {
                //this is for create
                return View(masterSetupVM);
            }
            //this is for edit
            masterSetupVM.MasterSetup = _unitOfWork.MasterSetup.Get(id.GetValueOrDefault());
            if (masterSetupVM.MasterSetup == null)
            {
                return NotFound();
            }
            return View(masterSetupVM);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Upsert(MasterSetupVM masterSetupVM)
        {
            Guid newGuidID = Guid.NewGuid();
            DateTime currentDate=DateTime.Now;
            string userName = User.Identity.Name;
            string userIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
            if (ModelState.IsValid)
            {
                if (masterSetupVM.MasterSetup.Id == 0)
                {
                    masterSetupVM.MasterSetup.QueryId = newGuidID;
                    masterSetupVM.MasterSetup.CreatedDate = currentDate;
                    masterSetupVM.MasterSetup.CreatedBy = userName;
                    masterSetupVM.MasterSetup.CreatedIp = userIp;
                    masterSetupVM.MasterSetup.UpdatedDate = DateTime.Now;
                    masterSetupVM.MasterSetup.UpdatedBy = "N/A";
                    masterSetupVM.MasterSetup.UpdatedIp = "0.0.0.0";
                    masterSetupVM.MasterSetup.IsDeleted = false;
                    _unitOfWork.MasterSetup.Add(masterSetupVM.MasterSetup);

                }
                else
                {
                    masterSetupVM.MasterSetup.UpdatedDate = currentDate;
                    masterSetupVM.MasterSetup.UpdatedBy = userName;
                    masterSetupVM.MasterSetup.UpdatedIp = userIp;
                    _unitOfWork.MasterSetup.Update(masterSetupVM.MasterSetup);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            masterSetupVM.ProgramLists = _unitOfWork.Program.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name + " (" + i.ProgramCode + ")",
                Value = i.Id.ToString()
            });
            masterSetupVM.SemesterLists = _unitOfWork.Semester.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name + " (" + i.Code + ")",
                Value = i.Id.ToString()
            });
            return View(masterSetupVM);
        }

        #region API Calls

        [HttpGet]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.MasterSetup.GetAll(includeProperties: "ProgramData,Semester,Semester.Session,Semester.Term");
            return Json(new { data = allObj });
        }


        [HttpDelete]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.MasterSetup.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.MasterSetup.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion
    }
}
