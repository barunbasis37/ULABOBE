using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using ULABOBE.DataAccess.Repository.IRepository;
using ULABOBE.Models;
using ULABOBE.Models.ViewModels;
using ULABOBE.App.Areas.Admin.Controllers;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using ULABOBE.Utility;
using ULABOBE.Models.ReportViewModels;
using Dapper;
using Microsoft.AspNetCore.Authorization;

namespace ULABOBE.AppOnline.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProgramCatalogController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        
        public ProgramCatalogController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
            
        }

        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Index()
        {
            return View();
        }

        //private UniqueSetup uniqueSetup;
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Upsert(int? id)
        {

            
            ProgramCatalogVM ProgramCatalogVM = new ProgramCatalogVM()
            {
                ProgramCatalog = new ProgramCatalog(),
                ProgramLists = _unitOfWork.Program
                    .GetAll()
                    .Select(i => new SelectListItem
                    {
                        Text = i.Name + " (" + i.ProgramCode + ")",
                        Value = i.Id.ToString()
                    }),
            };
            if (id == null)
            {
                //this is for create
                return View(ProgramCatalogVM);
            }
            //this is for edit
            ProgramCatalogVM.ProgramCatalog = _unitOfWork.ProgramCatalog.Get(id.GetValueOrDefault());
            if (ProgramCatalogVM.ProgramCatalog == null)
            {
                return NotFound();
            }
            return View(ProgramCatalogVM);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Upsert(ProgramCatalogVM ProgramCatalogVM)
        {

            if (ModelState.IsValid)
            {
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                if (files.Count > 0)
                {
                    ProgramData aProgramData = _unitOfWork.Program.GetFirstOrDefault(p=>p.Id== ProgramCatalogVM.ProgramCatalog.ProgramId);
                    //string fileName = Guid.NewGuid().ToString();
                    string fileName = aProgramData.ProgramCode + "-"+ aProgramData.ProgramURMSId + "-"+ DateTime.Now.Year;
                    var uploads = Path.Combine(webRootPath, @"Document\ProgramCatalog\");
                    var extenstion = Path.GetExtension(files[0].FileName);

                    if (ProgramCatalogVM.ProgramCatalog.FileUploadUrl != null)
                    {
                        //this is an edit and we need to remove old image
                        var filePath = Path.Combine(webRootPath, ProgramCatalogVM.ProgramCatalog.FileUploadUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }
                    }
                    using (var filesStreams = new FileStream(Path.Combine(uploads, fileName + extenstion), FileMode.Create))
                    {
                        files[0].CopyTo(filesStreams);
                    }
                    ProgramCatalogVM.ProgramCatalog.FileUploadUrl = @"\Document\ProgramCatalog\" + fileName + extenstion;
                    ProgramCatalogVM.ProgramCatalog.FileExtension = extenstion;
                    ProgramCatalogVM.ProgramCatalog.FileName = fileName+ extenstion;
                }
                else
                {
                    //update when they do not change the image
                    if (ProgramCatalogVM.ProgramCatalog.Id != 0)
                    {
                        ProgramCatalog objFromDb = _unitOfWork.ProgramCatalog.Get(ProgramCatalogVM.ProgramCatalog.Id);
                        ProgramCatalogVM.ProgramCatalog.FileUploadUrl = objFromDb.FileUploadUrl;
                    }
                }

                if (ProgramCatalogVM.ProgramCatalog.Id == 0)
                {
                    ProgramCatalogVM.ProgramCatalog.QueryId = Guid.NewGuid();

                    ProgramCatalogVM.ProgramCatalog.CreatedDate = DateTime.Now;
                    ProgramCatalogVM.ProgramCatalog.CreatedBy = User.Identity.Name;
                    ProgramCatalogVM.ProgramCatalog.CreatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    ProgramCatalogVM.ProgramCatalog.UpdatedDate = DateTime.Now;
                    ProgramCatalogVM.ProgramCatalog.UpdatedBy = User.Identity.Name;
                    ProgramCatalogVM.ProgramCatalog.UpdatedIp = "0.0.0.0";
                    ProgramCatalogVM.ProgramCatalog.IsDeleted = false;
                    _unitOfWork.ProgramCatalog.Add(ProgramCatalogVM.ProgramCatalog);

                }
                else
                {
                    ProgramCatalogVM.ProgramCatalog.UpdatedDate = DateTime.Now;
                    //courseLearning.UpdatedBy = User.Identity.Name;
                    //courseLearning.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    ProgramCatalogVM.ProgramCatalog.UpdatedBy = User.Identity.Name;
                    ProgramCatalogVM.ProgramCatalog.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    ProgramCatalogVM.ProgramCatalog.IsDeleted = false;
                    _unitOfWork.ProgramCatalog.Update(ProgramCatalogVM.ProgramCatalog);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            ProgramCatalogVM.ProgramLists = _unitOfWork.Program
                    .GetAll()
                    .Select(i => new SelectListItem
                    {
                        Text = i.Name + "(" + i.ProgramCode + ")",
                        Value = i.Id.ToString()
                    });
            return View(ProgramCatalogVM);
        }        
      


        public async Task<IActionResult> DownloadFile(int id)
        {
            var objFromDb = _unitOfWork.ProgramCatalog.Get(id);
            string webRootPath = _hostEnvironment.WebRootPath;
            string fileName = objFromDb.FileUploadUrl;
            if (string.IsNullOrEmpty(fileName) || fileName == null)
            {
                return Content("File Name is Empty...");
            }

            // get the filePath

            var imagePath = Path.Combine(webRootPath, objFromDb.FileUploadUrl.TrimStart('\\'));

            

            // create a memorystream
            var memoryStream = new MemoryStream();

            using (var stream = new FileStream(imagePath, FileMode.Open))
            {
                await stream.CopyToAsync(memoryStream);
            }
            // set the position to return the file from
            memoryStream.Position = 0;

            

            return File(memoryStream, "application/x-msdownload", Path.GetFileName(imagePath));
        }


        #region API Calls

         
        [HttpGet]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult GetAll()
        {       
            var allObj = _unitOfWork.ProgramCatalog.GetAll(includeProperties: "ProgramData,ProgramData.Department").ToList();
            return Json(new { data = allObj });
        }
        
        

        #endregion
    }
}
