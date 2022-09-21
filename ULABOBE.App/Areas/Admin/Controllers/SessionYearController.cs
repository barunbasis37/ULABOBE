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
    //[Route("admin/sessionYear")]
    [Authorize]
    public class SessionYearController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public SessionYearController(IUnitOfWork unitOfWork)
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
            Session session = new Session();
            if (id == null)
            {
                //this is for create
                return View(session);
            }
            //this is for edit
            session = _unitOfWork.SessionYear.Get(id.GetValueOrDefault());
            if (session == null)
            {
                return NotFound();
            }
            return View(session);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Upsert(Session session)
        {
            
            if (ModelState.IsValid)
            {
                if (session.Id == 0)
                {
                    session.QueryId = Guid.NewGuid();
                    session.CreatedDate = DateTime.Now;
                    session.CreatedBy = User.Identity.Name;
                    session.CreatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    session.UpdatedDate = DateTime.MinValue;
                    session.UpdatedBy = "-";
                    session.UpdatedIp = "0.0.0.0";
                    session.IsDeleted = false;
                    _unitOfWork.SessionYear.Add(session);

                }
                else
                {
                    session.UpdatedDate = DateTime.Now;
                    session.UpdatedBy = User.Identity.Name;
                    session.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    //session.UpdatedBy = "423036";
                    //session.UpdatedIp = "172.16.25.30";
                    session.IsDeleted = false;
                    _unitOfWork.SessionYear.Update(session);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(session);
        }

        #region API Calls
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.SessionYear.GetAll().ToList();
            return Json(new {data = allObj});
        }


        [HttpDelete]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.SessionYear.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.SessionYear.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion
    }
}
