using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using ULABOBE.DataAccess.Repository.IRepository;
using ULABOBE.Models;
using ULABOBE.Models.ViewModels;
using ULABOBE.Utility;

namespace ULABITOHelpDesk.AppOnline.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class DesignationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DesignationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Upsert(int? id)
        {
            Designation designation = new Designation();
            if (id == null)
            {
                //this is for create
                return View(designation);
            }
            //this is for edit
            var parameter = new DynamicParameters();
            parameter.Add("@Id", id);
            designation = _unitOfWork.SP_Call.OneRecord<Designation>(SD.Proc_Designation_Get,parameter);
            if (designation == null)
            {
                return NotFound();
            }
            return View(designation);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Upsert(Designation designation)
        {
            designation.QueryId = Guid.NewGuid();
            designation.IsActive = true;
            designation.CreatedDate = DateTime.Now;
            designation.CreatedBy = User.Identity.Name;
            designation.CreatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
            designation.UpdatedDate = DateTime.MinValue.AddYears(1900);
            designation.UpdatedBy = "-";
            designation.UpdatedIp = "0.0.0.0";
            //designation.IsDeleted = false;

            if (ModelState.IsValid)
            {
                var parameter = new DynamicParameters();
                parameter.Add("@Name", designation.Name);
                parameter.Add("@Priority", designation.Priority);
                parameter.Add("@IsActive", designation.IsActive);
                parameter.Add("@QueryId", designation.QueryId);
                
                parameter.Add("@UpdatedBy", designation.UpdatedBy);
                parameter.Add("@UpdatedIp", designation.UpdatedIp);
                parameter.Add("@UpdatedDate", designation.UpdatedDate);
                parameter.Add("@IsDeleted", designation.IsDeleted);

                if (designation.Id == 0)
                {
                    parameter.Add("@CreatedBy", designation.CreatedBy);
                    parameter.Add("@CreatedIp", designation.CreatedIp);
                    parameter.Add("@CreatedDate", designation.CreatedDate);
                    _unitOfWork.SP_Call.Execute(SD.Proc_Designation_Create,parameter);

                }
                else
                {
                    designation.UpdatedDate = DateTime.Now;
                    designation.UpdatedBy = User.Identity.Name;
                    designation.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    parameter.Add("@Id", designation.Id);
                    parameter.Add("@UpdatedBy", designation.UpdatedBy);
                    parameter.Add("@UpdatedIp", designation.UpdatedIp);
                    parameter.Add("@UpdatedDate", designation.UpdatedDate);
                    
                    _unitOfWork.SP_Call.Execute(SD.Proc_Designation_Update,parameter);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(designation);
        }

        #region API Calls

        [HttpGet]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.SP_Call.List<Designation>(SD.Proc_Designation_GetAll, null);
            return Json(new { data = allObj });
        }


        [HttpDelete]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Delete(int id)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@Id",id);
            var objFromDb = _unitOfWork.SP_Call.Single<Designation>(SD.Proc_Designation_Get,parameter);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.SP_Call.Execute(SD.Proc_Designation_Delete,parameter);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion
    }
}
