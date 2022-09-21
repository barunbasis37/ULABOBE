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
    public class ScheduleController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ScheduleController(IUnitOfWork unitOfWork)
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
            ScheduleVM scheduleVM = new ScheduleVM()
            {
                Schedule = new Schedule(),
                WeekDayLists = _unitOfWork.WeekDay.GetAll().Select(i => new SelectListItem
                {
                    Text = i.ShortName+"("+ i.Name+")",
                    Value = i.Id.ToString()
                }),
                TimeLists = _unitOfWork.Time.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),

            };
            if (id == null)
            {
                //this is for create
                return View(scheduleVM);
            }
            //this is for edit
            scheduleVM.Schedule = _unitOfWork.Schedule.Get(id.GetValueOrDefault());
            if (scheduleVM.Schedule == null)
            {
                return NotFound();
            }
            return View(scheduleVM);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Upsert(ScheduleVM scheduleVM)
        {

            if (ModelState.IsValid)
            {
                if (scheduleVM.Schedule.Id == 0)
                {
                    scheduleVM.Schedule.QueryId = Guid.NewGuid();

                    scheduleVM.Schedule.CreatedDate = DateTime.Now;
                    scheduleVM.Schedule.CreatedBy = "423036";
                    scheduleVM.Schedule.CreatedIp = "172.16.25.30";
                    scheduleVM.Schedule.UpdatedDate = DateTime.Now;
                    scheduleVM.Schedule.UpdatedBy = "423036";
                    scheduleVM.Schedule.UpdatedIp = "172.16.25.30";
                    scheduleVM.Schedule.IsDeleted = false;
                    _unitOfWork.Schedule.Add(scheduleVM.Schedule);

                }
                else
                {
                    scheduleVM.Schedule.UpdatedDate = DateTime.Now;
                    scheduleVM.Schedule.UpdatedBy = User.Identity.Name;
                    scheduleVM.Schedule.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    scheduleVM.Schedule.IsDeleted = false;
                    _unitOfWork.Schedule.Update(scheduleVM.Schedule);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            scheduleVM.WeekDayLists = _unitOfWork.WeekDay.GetAll().Select(i => new SelectListItem
            {
                Text = i.ShortName + "(" + i.Name + ")",
                Value = i.Id.ToString()
            });
            scheduleVM.TimeLists = _unitOfWork.Time.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            return View(scheduleVM);
        }

        #region API Calls

        [HttpGet]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.Schedule.GetAll(includeProperties: "WeekDay1,WeekDay2,FTime,TTime");
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
