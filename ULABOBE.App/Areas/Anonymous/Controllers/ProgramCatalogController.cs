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

namespace ULABOBE.AppOnline.Areas.Anonymous.Controllers
{
    [Area("Anonymous")]
    
    public class ProgramCatalogController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        
        public ProgramCatalogController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
            
        }

        
        public IActionResult Index()
        {
            return View();
        }

        //private UniqueSetup uniqueSetup;
           
      


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
        
        public IActionResult GetAll()
        {       
            var allObj = _unitOfWork.ProgramCatalog.GetAll(includeProperties: "ProgramData,ProgramData.Department").ToList();
            return Json(new { data = allObj });
        }
        
        

        #endregion
    }
}
