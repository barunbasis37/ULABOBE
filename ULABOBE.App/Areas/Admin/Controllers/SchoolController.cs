using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Reporting;
using Microsoft.AspNetCore.Hosting;
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
    public class SchoolController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvirnoment;

        public SchoolController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            this._webHostEnvirnoment = webHostEnvironment;
            this._unitOfWork = unitOfWork;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Upsert(int? id)
        {
            School school = new School();
            if (id == null)
            {
                //this is for create
                return View(school);
            }
            //this is for edit
            school = _unitOfWork.School.Get(id.GetValueOrDefault());
            if (school == null)
            {
                return NotFound();
            }
            return View(school);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Upsert(School school)
        {
            
            if (ModelState.IsValid)
            {
                if (school.Id == 0)
                {
                    school.QueryId = Guid.NewGuid();
                    school.IsActive = true;
                    school.CreatedDate = DateTime.Now;
                    school.CreatedBy = User.Identity.Name;
                    school.CreatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    school.UpdatedDate = DateTime.MinValue;
                    school.UpdatedBy = "-";
                    school.UpdatedIp = "0.0.0.0";
                    school.IsDeleted = false;
                    _unitOfWork.School.Add(school);

                }
                else
                {
                    school.UpdatedDate = DateTime.Now;
                    //school.UpdatedBy = User.Identity.Name;
                    //school.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    school.UpdatedBy = User.Identity.Name;
                    school.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    school.IsDeleted = false;
                    _unitOfWork.School.Update(school);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(school);
        }


        public IActionResult Print()
        {
            string mimtype = "";
            int extension = 1;
            var path = $"{this._webHostEnvirnoment.WebRootPath}\\Reporting\\rptSchool.rdlc";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            //parameters.Add("rp1", "ASP.NET CORE RDLC Report");
            //get products from product table 
            LocalReport localReport = new LocalReport(path);
            var allObj = _unitOfWork.School.GetAll().ToList();
            localReport.AddDataSource("SchoolDS", allObj);
            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimtype);
            return File(result.MainStream, "application/pdf");
        }

        #region API Calls
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.School.GetAll().ToList();
            return Json(new {data = allObj});
        }


        [HttpDelete]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.School.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.School.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion
    }
}
