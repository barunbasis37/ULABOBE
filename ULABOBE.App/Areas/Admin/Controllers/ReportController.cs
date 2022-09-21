using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Reporting;
using Microsoft.AspNetCore.Hosting;
using ULABOBE.DataAccess.Repository.IRepository;

namespace ULABOBE.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReportController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvirnoment;
        private readonly IUnitOfWork _unitOfWork;

        public ReportController(IWebHostEnvironment webHostEnvironment, IUnitOfWork unitOfWork)
        {
            this._webHostEnvirnoment = webHostEnvironment;
            this._unitOfWork = unitOfWork;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}

        
    }
}
