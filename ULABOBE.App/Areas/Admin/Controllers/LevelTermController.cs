using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ULABOBE.DataAccess.Repository.IRepository;
using ULABOBE.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using ULABOBE.Utility;

namespace ULABOBE.AppOnline.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Route("admin/school")]
    [Authorize]
    public class LevelTermController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public LevelTermController(IUnitOfWork unitOfWork)
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
            Term school = new Term();
            if (id == null)
            {
                //this is for create
                return View(school);
            }
            //this is for edit
            school = _unitOfWork.LevelTerm.Get(id.GetValueOrDefault());
            if (school == null)
            {
                return NotFound();
            }
            return View(school);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Upsert(Term term)
        {
            
            if (ModelState.IsValid)
            {
                if (term.Id == 0)
                {
                    term.QueryId = Guid.NewGuid();
                    term.CreatedDate = DateTime.Now;
                    term.CreatedBy = User.Identity.Name;
                    term.CreatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    term.UpdatedDate = DateTime.MinValue;
                    term.UpdatedBy = "-";
                    term.UpdatedIp = "0.0.0.0";
                    term.IsDeleted = false;
                    _unitOfWork.LevelTerm.Add(term);

                }
                else
                {
                    term.UpdatedDate = DateTime.Now;
                    term.UpdatedBy = User.Identity.Name;
                    term.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    //term.UpdatedBy = "423036";
                    //term.UpdatedIp = "172.16.25.30";
                    term.IsDeleted = false;
                    _unitOfWork.LevelTerm.Update(term);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(term);
        }

        #region API Calls
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.LevelTerm.GetAll().ToList();
            return Json(new {data = allObj});
        }


        [HttpDelete]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.LevelTerm.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.LevelTerm.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion
    }
}
