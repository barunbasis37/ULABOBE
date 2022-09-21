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
    public class CourseLearningController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseLearningController(IUnitOfWork unitOfWork)
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
            CourseLearningVM courseLearningVM = new CourseLearningVM()
            {
                CourseLearning = new CourseLearning(),
                CourseLearningTypeLists = _unitOfWork.CourseLearningType.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
            };
            if (id == null)
            {
                //this is for create
                return View(courseLearningVM);
            }
            //this is for edit
            courseLearningVM.CourseLearning = _unitOfWork.CourseLearning.Get(id.GetValueOrDefault());
            if (courseLearningVM.CourseLearning == null)
            {
                return NotFound();
            }
            return View(courseLearningVM);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Upsert(CourseLearningVM courseLearningVM)
        {

            if (ModelState.IsValid)
            {
                if (courseLearningVM.CourseLearning.Id == 0)
                {
                    courseLearningVM.CourseLearning.QueryId = Guid.NewGuid();
                    
                    courseLearningVM.CourseLearning.CreatedDate = DateTime.Now;
                    courseLearningVM.CourseLearning.CreatedBy = User.Identity.Name;
                    courseLearningVM.CourseLearning.CreatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    courseLearningVM.CourseLearning.UpdatedDate = DateTime.Now;
                    courseLearningVM.CourseLearning.UpdatedBy = "-";
                    courseLearningVM.CourseLearning.UpdatedIp = "0.0.0.0";
                    courseLearningVM.CourseLearning.IsDeleted = false;
                    _unitOfWork.CourseLearning.Add(courseLearningVM.CourseLearning);

                }
                else
                {
                    courseLearningVM.CourseLearning.UpdatedDate = DateTime.Now;
                    //courseLearning.UpdatedBy = User.Identity.Name;
                    //courseLearning.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    courseLearningVM.CourseLearning.UpdatedBy = User.Identity.Name;
                    courseLearningVM.CourseLearning.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    courseLearningVM.CourseLearning.IsDeleted = false;
                    _unitOfWork.CourseLearning.Update(courseLearningVM.CourseLearning);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            courseLearningVM.CourseLearningTypeLists = _unitOfWork.CourseLearningType.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            return View(courseLearningVM);
        }

        #region API Calls

        [HttpGet]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.CourseLearning.GetAll(includeProperties: "CourseLearningType");
            return Json(new { data = allObj });
        }

        //[HttpGet]
        //public IActionResult GetDAll(int? courseId)
        //{
        //    var allObj = _unitOfWork.CourseLearning.GetAll(c => c.CourseId == courseId);
        //    return Json(new { data = allObj });
        //}

        [HttpDelete]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.CourseLearning.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.CourseLearning.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion
    }
}
