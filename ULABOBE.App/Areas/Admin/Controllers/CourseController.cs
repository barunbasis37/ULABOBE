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
    public class CourseController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseController(IUnitOfWork unitOfWork)
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
            CourseVM courseVM = new CourseVM()
            {
                Course = new Course(),
                CourseTypeLists = _unitOfWork.CourseType.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Code+"("+ i.Name+")",
                    Value = i.Id.ToString()
                }),
                ProgramLists = _unitOfWork.Program.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name + "(" + i.ProgramCode + ")",
                    Value = i.Id.ToString()
                }),
            };
            if (id == null)
            {
                //this is for create
                return View(courseVM);
            }
            //this is for edit
            courseVM.Course = _unitOfWork.Course.Get(id.GetValueOrDefault());
            if (courseVM.Course == null)
            {
                return NotFound();
            }
            return View(courseVM);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Upsert(CourseVM courseVM)
        {

            if (ModelState.IsValid)
            {
                if (courseVM.Course.Id == 0)
                {
                    courseVM.Course.QueryId = Guid.NewGuid();
                    
                    courseVM.Course.CreatedDate = DateTime.Now;
                    courseVM.Course.CreatedBy = User.Identity.Name;
                    courseVM.Course.CreatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    courseVM.Course.UpdatedDate = DateTime.Now;
                    courseVM.Course.UpdatedBy = "N/A";
                    courseVM.Course.UpdatedIp = "0.0.0.0";
                    courseVM.Course.IsDeleted = false;
                    _unitOfWork.Course.Add(courseVM.Course);

                }
                else
                {
                    courseVM.Course.UpdatedDate = DateTime.Now;
                    courseVM.Course.UpdatedBy = User.Identity.Name;
                    courseVM.Course.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    courseVM.Course.IsDeleted = false;
                    _unitOfWork.Course.Update(courseVM.Course);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            courseVM.ProgramLists = _unitOfWork.Program.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name + "(" + i.ProgramCode + ")",
                Value = i.Id.ToString()
            });
            courseVM.CourseTypeLists = _unitOfWork.CourseType.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            return View(courseVM);
        }

        #region API Calls

        [HttpGet]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Course.GetAll(includeProperties: "Program,CourseType");
            return Json(new { data = allObj });
        }


        [HttpDelete]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Course.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.Course.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion
    }
}
