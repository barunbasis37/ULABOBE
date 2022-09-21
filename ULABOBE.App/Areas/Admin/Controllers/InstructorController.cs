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
    public class InstructorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public InstructorController(IUnitOfWork unitOfWork)
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
            InstructorVM instructorVM = new InstructorVM()
            {
                Instructor = new Instructor(),
                DesignationList = _unitOfWork.Designation.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
            };
            if (id == null)
            {
                //this is for create
                return View(instructorVM);
            }
            //this is for edit
            instructorVM.Instructor = _unitOfWork.Instructor.Get(id.GetValueOrDefault());
            if (instructorVM.Instructor == null)
            {
                return NotFound();
            }
            return View(instructorVM);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Upsert(InstructorVM instructorVM)
        {

            if (ModelState.IsValid)
            {
                if (instructorVM.Instructor.Id == 0)
                {
                    instructorVM.Instructor.QueryId = Guid.NewGuid();
                    //instructorVM.Instructor.IsActive = true;
                    instructorVM.Instructor.CreatedDate = DateTime.Now;
                    instructorVM.Instructor.CreatedBy = User.Identity.Name;
                    instructorVM.Instructor.CreatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    instructorVM.Instructor.UpdatedDate = DateTime.Now;
                    instructorVM.Instructor.UpdatedBy = "-";
                    instructorVM.Instructor.UpdatedIp = "0.0.0.0";
                    instructorVM.Instructor.IsDeleted = false;
                    _unitOfWork.Instructor.Add(instructorVM.Instructor);

                }
                else
                {
                    instructorVM.Instructor.UpdatedDate = DateTime.Now;
                    instructorVM.Instructor.UpdatedBy = User.Identity.Name;
                    instructorVM.Instructor.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    //instructorVM.Instructor.UpdatedBy = "423036";
                    //instructorVM.Instructor.UpdatedIp = "172.16.25.30";
                    instructorVM.Instructor.IsDeleted = false;
                    _unitOfWork.Instructor.Update(instructorVM.Instructor);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            instructorVM.DesignationList = _unitOfWork.Designation.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            return View(instructorVM);
        }

        #region API Calls

        [HttpGet]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Instructor.GetAll(includeProperties: "Designation");
            return Json(new { data = allObj });
        }


        [HttpDelete]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Instructor.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.Instructor.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion
    }
}
