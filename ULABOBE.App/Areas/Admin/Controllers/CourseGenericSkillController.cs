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
    public class CourseGenericSkillController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseGenericSkillController(IUnitOfWork unitOfWork)
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
            CourseGenericSkillVM courseGenericSkillVM = new CourseGenericSkillVM()
            {
                CourseGenericSkill = new CourseGenericSkill(),
                CourseLists = _unitOfWork.Course.GetAll().Select(i => new SelectListItem
                {
                    Text = i.CourseCode,
                    Value = i.Id.ToString()
                }),
                SemesterLists = _unitOfWork.Semester.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                GenericSkillLists = _unitOfWork.GenericSkill.GetAll().Select(i => new SelectListItem
                {
                    Text = i.GSCode,
                    Value = i.Id.ToString()
                }),
            };
            if (id == null)
            {
                //this is for create
                return View(courseGenericSkillVM);
            }
            //this is for edit
            courseGenericSkillVM.CourseGenericSkill = _unitOfWork.CourseGenericSkill.Get(id.GetValueOrDefault());
            if (courseGenericSkillVM.CourseGenericSkill == null)
            {
                return NotFound();
            }
            return View(courseGenericSkillVM);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Upsert(CourseGenericSkillVM courseGenericSkillVM)
        {

            if (ModelState.IsValid)
            {
                if (courseGenericSkillVM.CourseGenericSkill.Id == 0)
                {
                    courseGenericSkillVM.CourseGenericSkill.QueryId = Guid.NewGuid();

                    courseGenericSkillVM.CourseGenericSkill.CreatedDate = DateTime.Now;
                    courseGenericSkillVM.CourseGenericSkill.CreatedBy = User.Identity.Name;
                    courseGenericSkillVM.CourseGenericSkill.CreatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString(); ;
                    courseGenericSkillVM.CourseGenericSkill.UpdatedDate = DateTime.Now;
                    courseGenericSkillVM.CourseGenericSkill.UpdatedBy = "-";
                    courseGenericSkillVM.CourseGenericSkill.UpdatedIp = "0.0.0.0";
                    courseGenericSkillVM.CourseGenericSkill.IsDeleted = false;
                    _unitOfWork.CourseGenericSkill.Add(courseGenericSkillVM.CourseGenericSkill);

                }
                else
                {
                    courseGenericSkillVM.CourseGenericSkill.UpdatedDate = DateTime.Now;
                    //CourseGenericSkill.UpdatedBy = User.Identity.Name;
                    //CourseGenericSkill.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    courseGenericSkillVM.CourseGenericSkill.UpdatedBy = User.Identity.Name;
                    courseGenericSkillVM.CourseGenericSkill.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    courseGenericSkillVM.CourseGenericSkill.IsDeleted = false;
                    _unitOfWork.CourseGenericSkill.Update(courseGenericSkillVM.CourseGenericSkill);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            courseGenericSkillVM.CourseLists = _unitOfWork.Course.GetAll().Select(i => new SelectListItem
            {
                Text = i.CourseCode,
                Value = i.Id.ToString()
            });
            
            courseGenericSkillVM.SemesterLists = _unitOfWork.Semester.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            courseGenericSkillVM.GenericSkillLists = _unitOfWork.GenericSkill.GetAll().Select(i => new SelectListItem
            {
                Text = i.GSCode,
                Value = i.Id.ToString()
            });
            return View(courseGenericSkillVM);
        }

        #region API Calls

        [HttpGet]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult GetAll()
        {
            int maxSemester = _unitOfWork.Semester.GetAll().Max(mS => mS.Id);
            var allObj = _unitOfWork.CourseGenericSkill.GetAll(filter: ch => ch.SemesterId == maxSemester, includeProperties: "Course,Semester,GenericSkill,GenericSkill.GenericSkillType");
            return Json(new { data = allObj });
        }
        //[HttpGet]
        //public JsonResult GetDAll(int? courseId)
        //{

        //    var CourseGenericSkillLists = _unitOfWork.CourseGenericSkill.GetAll(cid => cid.CourseId == courseId, includeProperties: "CourseLearning").Select(i => new SelectListItem
        //    {
        //        Text = i.CourseLearning.CLOCode,
        //        Value = i.Id.ToString()
        //    });
        //    return Json(CourseGenericSkillLists);
        //}

        [HttpDelete]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.CourseGenericSkill.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.CourseGenericSkill.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion
    }
}
