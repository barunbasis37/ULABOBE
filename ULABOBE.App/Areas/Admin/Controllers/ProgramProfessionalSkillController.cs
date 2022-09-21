using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using ULABOBE.DataAccess.Repository.IRepository;
using ULABOBE.Models;
using ULABOBE.Models.ViewModels;
using ULABOBE.Utility;
using Microsoft.AspNetCore.Authorization;

namespace ULABOBE.AppOnline.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProgramProfessionalSkillController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProgramProfessionalSkillController(IUnitOfWork unitOfWork)
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
            ProgramProfessionalSkillVM programProfessionalSkillVM = new ProgramProfessionalSkillVM()
            {
                ProgramProfessionalSkill = new ProgramProfessionalSkill(),
                ProgramLists = _unitOfWork.Program.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name + "("+i.ProgramCode+")",
                    Value = i.Id.ToString()
                }),
                ProfessionalSkillLists = _unitOfWork.ProfessionalSkill.GetAll().Select(i => new SelectListItem
                {
                    Text = i.PSCode,
                    Value = i.Id.ToString()
                }),
            };
            if (id == null)
            {
                //this is for create
                return View(programProfessionalSkillVM);
            }
            //this is for edit
            programProfessionalSkillVM.ProgramProfessionalSkill = _unitOfWork.ProgramProfessionalSkill.Get(id.GetValueOrDefault());
            if (programProfessionalSkillVM.ProgramProfessionalSkill == null)
            {
                return NotFound();
            }
            return View(programProfessionalSkillVM);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Upsert(ProgramProfessionalSkillVM programProfessionalSkillVM)
        {

            if (ModelState.IsValid)
            {
                if (programProfessionalSkillVM.ProgramProfessionalSkill.Id == 0)
                {
                    programProfessionalSkillVM.ProgramProfessionalSkill.QueryId = Guid.NewGuid();

                    programProfessionalSkillVM.ProgramProfessionalSkill.CreatedDate = DateTime.Now;
                    programProfessionalSkillVM.ProgramProfessionalSkill.CreatedBy = User.Identity.Name;
                    programProfessionalSkillVM.ProgramProfessionalSkill.CreatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    programProfessionalSkillVM.ProgramProfessionalSkill.UpdatedDate = DateTime.Now;
                    programProfessionalSkillVM.ProgramProfessionalSkill.UpdatedBy = "-";
                    programProfessionalSkillVM.ProgramProfessionalSkill.UpdatedIp = "0.0.0.0";
                    programProfessionalSkillVM.ProgramProfessionalSkill.IsDeleted = false;
                    _unitOfWork.ProgramProfessionalSkill.Add(programProfessionalSkillVM.ProgramProfessionalSkill);

                }
                else
                {
                    programProfessionalSkillVM.ProgramProfessionalSkill.UpdatedDate = DateTime.Now;
                    //ProgramCLO.UpdatedBy = User.Identity.Name;
                    //ProgramCLO.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    programProfessionalSkillVM.ProgramProfessionalSkill.UpdatedBy = User.Identity.Name;
                    programProfessionalSkillVM.ProgramProfessionalSkill.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    programProfessionalSkillVM.ProgramProfessionalSkill.IsDeleted = false;
                    _unitOfWork.ProgramProfessionalSkill.Update(programProfessionalSkillVM.ProgramProfessionalSkill);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            programProfessionalSkillVM.ProgramLists = _unitOfWork.Program.GetAll().Select(i => new SelectListItem
            {
                Text = i.ProgramCode,
                Value = i.Id.ToString()
            });
            programProfessionalSkillVM.ProfessionalSkillLists = _unitOfWork.ProfessionalSkill.GetAll().Select(i => new SelectListItem
            {
                Text = i.PSCode,
                Value = i.Id.ToString()
            });
            return View(programProfessionalSkillVM);
        }

        #region API Calls

        [HttpGet]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.ProgramProfessionalSkill.GetAll(includeProperties: "Program,ProfessionalSkill");
            return Json(new { data = allObj });
        }
        //[HttpGet]
        //public JsonResult GetDAll(int? programId)
        //{

        //    var ProgramCLOLists = _unitOfWork.ProfessionalSkill.GetAll(cid => cid.ProgramId == programId, includeProperties: "ProgramLearning").Select(i => new SelectListItem
        //    {
        //        Text = i.PSCode,
        //        Value = i.Id.ToString()
        //    });
        //    return Json(ProgramCLOLists);
        //}

        [HttpDelete]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.ProgramProfessionalSkill.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.ProgramProfessionalSkill.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion
    }
}
