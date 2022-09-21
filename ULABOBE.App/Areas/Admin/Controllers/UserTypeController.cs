using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ULABOBE.DataAccess.Repository.IRepository;
using ULABOBE.Models;
using ULABOBE.Utility;
using Microsoft.AspNetCore.Authorization;

namespace ULABOBE.AppOnline.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class UserTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserTypeController(IUnitOfWork unitOfWork)
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
            UserType userType = new UserType();
            if (id == null)
            {
                //this is for create
                return View(userType);
            }
            //this is for edit
            var parameter = new DynamicParameters();
            parameter.Add("@Id", id);
            userType = _unitOfWork.SP_Call.OneRecord<UserType>(SD.Proc_UserType_Get, parameter);
            if (userType == null)
            {
                return NotFound();
            }
            return View(userType);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Upsert(UserType userType)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@Name", userType.Name);
            parameter.Add("@PriorityLevel", userType.PriorityLevel);
            userType.IsActive = true;
            parameter.Add("@IsActive", userType.IsActive);
            

            userType.IsDeleted = false;
            parameter.Add("@IsDeleted", userType.IsDeleted);

            if (ModelState.IsValid)
            {
                if (userType.Id == 0)
                {

                    userType.QueryId = Guid.NewGuid();
                    parameter.Add("@QueryId", userType.QueryId);
                    userType.CreatedDate = DateTime.Now;
                    userType.CreatedBy = User.Identity.Name;
                    userType.CreatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    parameter.Add("@CreatedBy", userType.CreatedBy);
                    parameter.Add("@CreatedIp", userType.CreatedIp);
                    parameter.Add("@CreatedDate", userType.CreatedDate);
                    userType.UpdatedDate = DateTime.Now;
                    userType.UpdatedBy = "-";
                    userType.UpdatedIp = "0.0.0.0";
                    
                    parameter.Add("@UpdatedBy", userType.UpdatedBy);
                    parameter.Add("@UpdatedIp", userType.UpdatedIp);
                    parameter.Add("@UpdatedDate", userType.UpdatedDate);
                    parameter.Add("@IsDeleted", userType.IsDeleted);
                    
                    _unitOfWork.SP_Call.Execute(SD.Proc_UserType_Create,parameter);

                }
                else
                {
                    parameter.Add("@Id", userType.Id);
                    parameter.Add("@QueryId", userType.QueryId);
                    parameter.Add("@CreatedBy", userType.CreatedBy);
                    parameter.Add("@CreatedIp", userType.CreatedIp);
                    parameter.Add("@CreatedDate", userType.CreatedDate);
                    userType.UpdatedDate = DateTime.Now;
                    //userType.UpdatedBy = User.Identity.Name;
                    //userType.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    userType.UpdatedBy = User.Identity.Name;
                    userType.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    parameter.Add("@UpdatedBy", userType.UpdatedBy);
                    parameter.Add("@UpdatedIp", userType.UpdatedIp);
                    parameter.Add("@UpdatedDate", userType.UpdatedDate);
                    parameter.Add("@IsDeleted", userType.IsDeleted);
                    _unitOfWork.SP_Call.Execute(SD.Proc_UserType_Update,parameter);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(userType);
        }

        #region API Calls
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.SP_Call.List<UserType>(SD.Proc_UserType_GetAll,null);
            return Json(new { data = allObj });
        }


        [HttpDelete]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Delete(int id)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@Id",id);
            var objFromDb = _unitOfWork.SP_Call.OneRecord<UserType>(SD.Proc_UserType_Get,parameter);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.SP_Call.Execute(SD.Proc_UserType_Delete,parameter);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion
    }
}
