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
    public class ProgramLearningController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProgramLearningController(IUnitOfWork unitOfWork)
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
            ProgramLearning programLearning = new ProgramLearning();
            if (id == null)
            {
                //this is for create
                return View(programLearning);
            }
            //this is for edit
            programLearning = _unitOfWork.ProgramLearning.Get(id.GetValueOrDefault());
            if (programLearning == null)
            {
                return NotFound();
            }
            return View(programLearning);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Upsert(ProgramLearning programLearning)
        {

            if (ModelState.IsValid)
            {
                if (programLearning.Id == 0)
                {
                    programLearning.QueryId = Guid.NewGuid();
                    
                    programLearning.CreatedDate = DateTime.Now;
                    programLearning.CreatedBy = User.Identity.Name;
                    programLearning.CreatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    programLearning.UpdatedDate = DateTime.Now;
                    programLearning.UpdatedBy = "-";
                    programLearning.UpdatedIp = "0.0.0.0";
                    programLearning.IsDeleted = false;
                    _unitOfWork.ProgramLearning.Add(programLearning);

                }
                else
                {
                    programLearning.UpdatedDate = DateTime.Now;
                    programLearning.UpdatedBy = User.Identity.Name;
                    programLearning.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    //programLearning.UpdatedBy = "423036";
                    //programLearning.UpdatedIp = "172.16.25.30";
                    programLearning.IsDeleted = false;
                    _unitOfWork.ProgramLearning.Update(programLearning);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

           
            return View(programLearning);
        }

        #region API Calls

        [HttpGet]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.ProgramLearning.GetAll().ToList();
            return Json(new { data = allObj });
        }


        [HttpDelete]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.ProgramLearning.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.ProgramLearning.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion
    }
}
