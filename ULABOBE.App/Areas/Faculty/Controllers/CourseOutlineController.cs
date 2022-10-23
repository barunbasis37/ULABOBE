﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using ULABOBE.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Hosting;
using ULABOBE.Models.ViewModels;
using ULABOBE.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using ULABOBE.Models.ReportViewModels;
using ULABOBE.Utility;

namespace ULABOBE.App.Areas.Faculty.Controllers
{
    [Area("Faculty")]
    [Authorize]
    public class CourseOutlineController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public CourseOutlineController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }
        [Authorize(Roles = SD.Role_Faculty)]
        public IActionResult Index()
        {
            return View();
        }

        private UniqueSetup uniqueSetup;
        [Authorize(Roles = SD.Role_Faculty)]
        public IActionResult Upsert(int? id)
        {

            uniqueSetup = new UniqueSetup(_unitOfWork);
            ViewBag.Semester = uniqueSetup.GetCurrentSemester().Name + "(" + uniqueSetup.GetCurrentSemester().Code + ")";
            CourseOutlineVM courseOutlineVM = new CourseOutlineVM()
            {
                CourseOutline = new CourseOutline(),
                CourseHistoryLists = _unitOfWork.CourseHistory
                    .GetAll(includeProperties: "Course,Semester,Section,Instructor", filter: ch => ch.SemesterId == uniqueSetup.GetCurrentSemester().Id && ch.Instructor == uniqueSetup.GetInstructor(User.Identity.Name))
                    .Select(i => new SelectListItem
                    {
                        Text = i.Course.CourseCode + "(" + i.Section.SectionCode + ")-" + i.Instructor.ShortCode + ")",
                        Value = i.Id.ToString()
                    }),
            };
            if (id == null)
            {
                //this is for create
                return View(courseOutlineVM);
            }
            //this is for edit
            courseOutlineVM.CourseOutline = _unitOfWork.CourseOutline.Get(id.GetValueOrDefault());
            if (courseOutlineVM.CourseOutline == null)
            {
                return NotFound();
            }
            return View(courseOutlineVM);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_Faculty)]
        public IActionResult Upsert(CourseOutlineVM courseOutlineVM)
        {

            if (ModelState.IsValid)
            {
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                if (files.Count > 0)
                {
                    CourseHistory aCourseHistory = _unitOfWork.CourseHistory.GetFirstOrDefault(filter: ch => ch.Id == courseOutlineVM.CourseOutline.CourseHistoryId, includeProperties: "Course,Semester,Section,Instructor");
                    //string fileName = Guid.NewGuid().ToString();
                    string fileName = aCourseHistory.Semester.Code + "-" + aCourseHistory.Course.CourseCode + "-" + aCourseHistory.Section.SectionCode + "-" + aCourseHistory.Instructor.ShortCode;
                    var uploads = Path.Combine(webRootPath, @"Course_Outline\");
                    var extenstion = Path.GetExtension(files[0].FileName);

                    if (courseOutlineVM.CourseOutline.FileUploadUrl != null)
                    {
                        //this is an edit and we need to remove old image
                        var filePath = Path.Combine(webRootPath, courseOutlineVM.CourseOutline.FileUploadUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }
                    }
                    using (var filesStreams = new FileStream(Path.Combine(uploads, fileName + extenstion), FileMode.Create))
                    {
                        files[0].CopyTo(filesStreams);
                    }
                    courseOutlineVM.CourseOutline.FileUploadUrl = @"\Course_Outline\" + fileName + extenstion;
                    courseOutlineVM.CourseOutline.FileExtension = extenstion;
                    courseOutlineVM.CourseOutline.FileName = fileName + extenstion;
                }
                else
                {
                    //update when they do not change the image
                    if (courseOutlineVM.CourseOutline.Id != 0)
                    {
                        CourseOutline objFromDb = _unitOfWork.CourseOutline.Get(courseOutlineVM.CourseOutline.Id);
                        courseOutlineVM.CourseOutline.FileUploadUrl = objFromDb.FileUploadUrl;
                    }
                }

                if (courseOutlineVM.CourseOutline.Id == 0)
                {
                    courseOutlineVM.CourseOutline.QueryId = Guid.NewGuid();

                    courseOutlineVM.CourseOutline.CreatedDate = DateTime.Now;
                    courseOutlineVM.CourseOutline.CreatedBy = User.Identity.Name;
                    courseOutlineVM.CourseOutline.CreatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    courseOutlineVM.CourseOutline.UpdatedDate = DateTime.Now;
                    courseOutlineVM.CourseOutline.UpdatedBy = User.Identity.Name;
                    courseOutlineVM.CourseOutline.UpdatedIp = "0.0.0.0";
                    courseOutlineVM.CourseOutline.IsDeleted = false;
                    _unitOfWork.CourseOutline.Add(courseOutlineVM.CourseOutline);

                }
                else
                {
                    courseOutlineVM.CourseOutline.UpdatedDate = DateTime.Now;
                    //courseLearning.UpdatedBy = User.Identity.Name;
                    //courseLearning.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    courseOutlineVM.CourseOutline.UpdatedBy = User.Identity.Name;
                    courseOutlineVM.CourseOutline.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    courseOutlineVM.CourseOutline.IsDeleted = false;
                    _unitOfWork.CourseOutline.Update(courseOutlineVM.CourseOutline);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            courseOutlineVM.CourseHistoryLists = _unitOfWork.CourseHistory
                    .GetAll(includeProperties: "Course,Semester,Section,Instructor", filter: ch => ch.SemesterId == uniqueSetup.GetCurrentSemester().Id)
                    .Select(i => new SelectListItem
                    {
                        Text = i.Course.CourseCode + "(" + i.Section.SectionCode + ")-" + i.Instructor.ShortCode + ")",
                        Value = i.Id.ToString()
                    });
            return View(courseOutlineVM);
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
        [Authorize(Roles = SD.Role_Faculty)]
        public IActionResult GetAll()
        {
            int maxSemester = _unitOfWork.Semester.GetAll().Max(mS => mS.Id);
            UserInfoCheck userInfoCheck = _unitOfWork.UserInfoCheck.GetFirstOrDefault(user => user.UserInfoId == User.Identity.Name);
            string instructorId = userInfoCheck.UserInfoId;
            var allObj = _unitOfWork.SP_Call.List<CourseOutlineRVM>(SD.Proc_CourseOutline_GetWithParam, null).Where(cO => cO.UserInfoId == instructorId && cO.SemesterId == maxSemester);
            return Json(new { data = allObj });
        }

        //protected virtual void GetLatestSemester()
        //{
        //    ViewBag.InstructorInfo = uniqueSetup.GetInstructor(User.Identity.Name).Name + "(" + uniqueSetup.GetInstructor(User.Identity.Name).ShortCode + ")";
        //    ViewBag.Semester = uniqueSetup.GetCurrentSemester().Name + "(" + uniqueSetup.GetCurrentSemester().Code + ")";
        //}

        #endregion
    }
}
