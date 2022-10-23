using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ULABOBE.App.Models;
using ULABOBE.DataAccess.Repository.IRepository;
using ULABOBE.Models.ReportViewModels;
using ULABOBE.Utility;

namespace ULABOBE.App.Areas.Anonymous.Controllers
{
    [Area("Anonymous")]
    //[AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
            _logger = logger;
        }

        //[AllowAnonymous]
        public IActionResult Index()
        {
            try
            {
                _logger.LogInformation("Index Load From Anonymous");
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
            }
            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> DownloadFile(int id)
        {
            var objFromDb = _unitOfWork.CourseOutline.Get(id);
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
        //[AllowAnonymous]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.SP_Call.List<CourseOutlineRVM>(SD.Proc_CourseOutline_GetWithParam, null);
            return Json(new { data = allObj });
        }
        #endregion
    }
}
