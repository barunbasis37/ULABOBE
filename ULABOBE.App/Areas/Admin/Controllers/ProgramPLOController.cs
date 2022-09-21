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
    public class ProgramPLOController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProgramPLOController(IUnitOfWork unitOfWork)
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
            ProgramPLOVM ProgramPLOVM = new ProgramPLOVM()
            {
                ProgramPLO = new ProgramPLO(),
                ProgramLists = _unitOfWork.Program.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name + "("+i.ProgramCode+")",
                    Value = i.Id.ToString()
                }),
                ProgramLearningLists = _unitOfWork.ProgramLearning.GetAll().Select(i => new SelectListItem
                {
                    Text = i.PLOCode,
                    Value = i.Id.ToString()
                }),
            };
            if (id == null)
            {
                //this is for create
                return View(ProgramPLOVM);
            }
            //this is for edit
            ProgramPLOVM.ProgramPLO = _unitOfWork.ProgramPLO.Get(id.GetValueOrDefault());
            if (ProgramPLOVM.ProgramPLO == null)
            {
                return NotFound();
            }
            return View(ProgramPLOVM);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Upsert(ProgramPLOVM ProgramPLOVM)
        {

            if (ModelState.IsValid)
            {
                if (ProgramPLOVM.ProgramPLO.Id == 0)
                {
                    ProgramPLOVM.ProgramPLO.QueryId = Guid.NewGuid();

                    ProgramPLOVM.ProgramPLO.CreatedDate = DateTime.Now;
                    ProgramPLOVM.ProgramPLO.CreatedBy = User.Identity.Name;
                    ProgramPLOVM.ProgramPLO.CreatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    ProgramPLOVM.ProgramPLO.UpdatedDate = DateTime.Now;
                    ProgramPLOVM.ProgramPLO.UpdatedBy = "-";
                    ProgramPLOVM.ProgramPLO.UpdatedIp = "0.0.0.0";
                    ProgramPLOVM.ProgramPLO.IsDeleted = false;
                    _unitOfWork.ProgramPLO.Add(ProgramPLOVM.ProgramPLO);

                }
                else
                {
                    ProgramPLOVM.ProgramPLO.UpdatedDate = DateTime.Now;
                    //ProgramCLO.UpdatedBy = User.Identity.Name;
                    //ProgramCLO.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    ProgramPLOVM.ProgramPLO.UpdatedBy = User.Identity.Name;
                    ProgramPLOVM.ProgramPLO.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    ProgramPLOVM.ProgramPLO.IsDeleted = false;
                    _unitOfWork.ProgramPLO.Update(ProgramPLOVM.ProgramPLO);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            ProgramPLOVM.ProgramLists = _unitOfWork.Program.GetAll().Select(i => new SelectListItem
            {
                Text = i.ProgramCode,
                Value = i.Id.ToString()
            });
            ProgramPLOVM.ProgramLearningLists = _unitOfWork.ProgramLearning.GetAll().Select(i => new SelectListItem
            {
                Text = i.PLOCode,
                Value = i.Id.ToString()
            });
            return View(ProgramPLOVM);
        }

        #region API Calls

        [HttpGet]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.ProgramPLO.GetAll(includeProperties: "Program,ProgramLearning");
            return Json(new { data = allObj });
        }
        [HttpGet]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public JsonResult GetDAll(int? programId)
        {

            var ProgramCLOLists = _unitOfWork.ProgramPLO.GetAll(cid => cid.ProgramId == programId, includeProperties: "ProgramLearning").Select(i => new SelectListItem
            {
                Text = i.ProgramLearning.PLOCode,
                Value = i.Id.ToString()
            });
            return Json(ProgramCLOLists);
        }

        [HttpDelete]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.ProgramPLO.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.ProgramPLO.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion
    }
}
