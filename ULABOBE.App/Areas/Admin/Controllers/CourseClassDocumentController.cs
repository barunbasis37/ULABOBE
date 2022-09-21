using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ULABOBE.DataAccess.Repository.IRepository;
using ULABOBE.Models.ViewModels;
using ULABOBE.Utility;
using ULABOBE.Models;
using System.IO;
using ULABOBE.Models.ReportViewModels;

namespace ULABOBE.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CourseClassDocumentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        private UniqueSetup uniqueSetup;
        public CourseClassDocumentController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = webHostEnvironment;
            uniqueSetup = new UniqueSetup(_unitOfWork);
        }
        [HttpGet]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Upsert(int? id)
        {

            //uniqueSetup = new UniqueSetup(_unitOfWork);
            ViewBag.Semester = uniqueSetup.GetCurrentSemester().Name + "(" + uniqueSetup.GetCurrentSemester().Code + ")";
            CourseClassDocumentVM CourseClassDocumentVM = new CourseClassDocumentVM()
            {
                CourseClassDocument = new CourseClassDocument(),
                CourseHistoryLists = _unitOfWork.CourseHistory
                    .GetAll(includeProperties: "Course,Semester,Section,Instructor", filter: ch => ch.SemesterId == uniqueSetup.GetCurrentSemester().Id)
                    .Select(i => new SelectListItem
                    {
                        Text = i.Course.CourseCode + "(" + i.Section.SectionCode + ")-" + i.Instructor.ShortCode + ")",
                        Value = i.Id.ToString()
                    }),
            };
            if (id == null)
            {
                //this is for create
                return View(CourseClassDocumentVM);
            }
            //this is for edit
            CourseClassDocumentVM.CourseClassDocument = _unitOfWork.CourseClassDocument.Get(id.GetValueOrDefault());
            if (CourseClassDocumentVM.CourseClassDocument == null)
            {
                return NotFound();
            }
            return View(CourseClassDocumentVM);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Upsert(CourseClassDocumentVM CourseClassDocumentVM)
        {

            if (ModelState.IsValid)
            {
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                string foldermonitorCName=null;
                string monitorfileName=null;
                string monitorextenstion=null;

                //if (files[0]..="".Equals(0) || files[1].Equals(0) || files[2].Equals(0) || files[3].Equals(0) || files[4].Equals(0) || files[5].Equals(0))
                //{
                    if (files.Count > 0)
                    {
                        CourseHistory aCourseHistory = _unitOfWork.CourseHistory.GetFirstOrDefault(filter: ch => ch.Id == CourseClassDocumentVM.CourseClassDocument.CourseHistoryId, includeProperties: "Course,Semester,Section,Instructor,Course.Program");
                        //string fileName = Guid.NewGuid().ToString();           
                        //if (CourseClassDocumentVM.MonitorImage!=null)
                        //{
                        monitorfileName = "CMR-" + aCourseHistory.Semester.Code + "-" + aCourseHistory.Course.CourseCode + "-" + aCourseHistory.Section.SectionCode + "-" + aCourseHistory.Instructor.ShortCode;
                        string foldermonitorDynamic = aCourseHistory.Semester.Code + @"Monitor\" + aCourseHistory.Course.Program.ProgramCode + @"\" + aCourseHistory.Instructor.Name + " (" + aCourseHistory.Instructor.ShortCode + ")";
                        foldermonitorCName = @"Document\CourseClassDocument\" + foldermonitorDynamic;
                        var monitoruploads = Path.Combine(webRootPath, foldermonitorCName);
                        // If directory does not exist, create it
                        if (!Directory.Exists(monitoruploads))
                        {
                            Directory.CreateDirectory(monitoruploads);
                        }
                        
                    if (CourseClassDocumentVM.SessionImage == null || CourseClassDocumentVM.SessionImage == null || CourseClassDocumentVM.SemesterCourseImage == null
                        || CourseClassDocumentVM.LessionImage == null || CourseClassDocumentVM.CourseProMapImage == null || CourseClassDocumentVM.AttendanceImage == null)
                    {
                        ModelState.AddModelError(string.Empty, "Upload All Class Document");
                        CourseClassDocumentVM.CourseHistoryLists = _unitOfWork.CourseHistory
                    .GetAll(includeProperties: "Course,Semester,Section,Instructor", filter: ch => ch.SemesterId == uniqueSetup.GetCurrentSemester().Id)
                    .Select(i => new SelectListItem
                    {
                        Text = i.Course.CourseCode + "(" + i.Section.SectionCode + ")-" + i.Instructor.ShortCode + ")",
                        Value = i.Id.ToString()
                    });
                        return View(CourseClassDocumentVM);

                    }
                    else
                    {
                        monitorextenstion = Path.GetExtension(CourseClassDocumentVM.SessionImage.FileName);
                    }

                        if (CourseClassDocumentVM.CourseClassDocument.ClassMonitoringFileUploadUrl != null)
                        {
                            //this is an edit and we need to remove old image
                            var filePath = Path.Combine(webRootPath, CourseClassDocumentVM.CourseClassDocument.ClassMonitoringFileUploadUrl.TrimStart('\\'));
                            if (System.IO.File.Exists(filePath))
                            {
                                System.IO.File.Delete(filePath);
                            }
                        }
                        using (var filesStreams = new FileStream(Path.Combine(monitoruploads, monitorfileName + monitorextenstion), FileMode.Create))
                        {
                        CourseClassDocumentVM.MonitorImage.CopyTo(filesStreams);
                        }
                        //}
                        //else
                        //{
                        //    ViewBag.Message = "Upload Class Monitor Report.";
                        //}




                        string sessionfileName = "CSR-" + aCourseHistory.Semester.Code + "-" + aCourseHistory.Course.CourseCode + "-" + aCourseHistory.Section.SectionCode + "-" + aCourseHistory.Instructor.ShortCode;
                        string foldesessionDynamic = aCourseHistory.Semester.Code + @"Session\" + aCourseHistory.Course.Program.ProgramCode + @"\" + aCourseHistory.Instructor.Name + " (" + aCourseHistory.Instructor.ShortCode + ")";
                        string sessionCName = @"Document\CourseClassDocument\" + foldesessionDynamic;
                        var sessionuploads = Path.Combine(webRootPath, sessionCName);
                        // If directory does not exist, create it
                        if (!Directory.Exists(sessionuploads))
                        {
                            Directory.CreateDirectory(sessionuploads);
                        }

                        var sessionextenstion = Path.GetExtension(CourseClassDocumentVM.SessionImage.FileName);

                        if (CourseClassDocumentVM.CourseClassDocument.CourseSessionFileUploadUrl != null)
                        {
                            //this is an edit and we need to remove old image
                            var filePath = Path.Combine(webRootPath, CourseClassDocumentVM.CourseClassDocument.CourseSessionFileUploadUrl.TrimStart('\\'));
                            if (System.IO.File.Exists(filePath))
                            {
                                System.IO.File.Delete(filePath);
                            }
                        }
                        using (var filessessionStreams = new FileStream(Path.Combine(sessionuploads, sessionfileName + sessionextenstion), FileMode.Create))
                        {
                        CourseClassDocumentVM.SessionImage.CopyTo(filessessionStreams);
                        }



                        string semestercoursefileName = "SCR-" + aCourseHistory.Semester.Code + "-" + aCourseHistory.Course.CourseCode + "-" + aCourseHistory.Section.SectionCode + "-" + aCourseHistory.Instructor.ShortCode;
                        string foldesemestercourseDynamic = aCourseHistory.Semester.Code + @"SemesterCourse\" + aCourseHistory.Course.Program.ProgramCode + @"\" + aCourseHistory.Instructor.Name + " (" + aCourseHistory.Instructor.ShortCode + ")";
                        string foldersemestercourseCName = @"Document\CourseClassDocument\" + foldesemestercourseDynamic;
                        var semestercourseuploads = Path.Combine(webRootPath, foldersemestercourseCName);
                        // If directory does not exist, create it
                        if (!Directory.Exists(semestercourseuploads))
                        {
                            Directory.CreateDirectory(semestercourseuploads);
                        }

                        string lessonfileName = "LPT-" + aCourseHistory.Semester.Code + "-" + aCourseHistory.Course.CourseCode + "-" + aCourseHistory.Section.SectionCode + "-" + aCourseHistory.Instructor.ShortCode;
                        string foldelerlessionDynamic = aCourseHistory.Semester.Code + @"Lession\" + @"\" + aCourseHistory.Course.Program.ProgramCode + @"\" + aCourseHistory.Instructor.Name + " (" + aCourseHistory.Instructor.ShortCode + ")";
                        string folderlessionCName = @"Document\CourseClassDocument\" + foldelerlessionDynamic;
                        var lessionuploads = Path.Combine(webRootPath, folderlessionCName);
                        // If directory does not exist, create it
                        if (!Directory.Exists(lessionuploads))
                        {
                            Directory.CreateDirectory(lessionuploads);
                        }

                        string crouseProgramMapfileName = "CPM-" + aCourseHistory.Semester.Code + "-" + aCourseHistory.Course.CourseCode + "-" + aCourseHistory.Section.SectionCode + "-" + aCourseHistory.Instructor.ShortCode;
                        string foldecrouseProMapDynamic = aCourseHistory.Semester.Code + @"Monitor\" + aCourseHistory.Course.Program.ProgramCode + @"\" + aCourseHistory.Instructor.Name + " (" + aCourseHistory.Instructor.ShortCode + ")";
                        string foldercrouseProMapCName = @"Document\CourseClassDocument\" + foldecrouseProMapDynamic;
                        var crouseProMapuploads = Path.Combine(webRootPath, foldercrouseProMapCName);
                        // If directory does not exist, create it
                        if (!Directory.Exists(crouseProMapuploads))
                        {
                            Directory.CreateDirectory(crouseProMapuploads);
                        }


                        string attendancefileName = "ATD-" + aCourseHistory.Semester.Code + "-" + aCourseHistory.Course.CourseCode + "-" + aCourseHistory.Section.SectionCode + "-" + aCourseHistory.Instructor.ShortCode;
                        string folderattendanceDynamic = aCourseHistory.Semester.Code + @"Attendance\" + aCourseHistory.Course.Program.ProgramCode + @"\" + aCourseHistory.Instructor.Name + " (" + aCourseHistory.Instructor.ShortCode + ")";
                        string folderattendanceCName = @"Document\CourseClassDocument\" + folderattendanceDynamic;
                        var attendanceuploads = Path.Combine(webRootPath, folderattendanceCName);
                        // If directory does not exist, create it
                        if (!Directory.Exists(attendanceuploads))
                        {
                            Directory.CreateDirectory(attendanceuploads);
                        }



                        CourseClassDocumentVM.CourseClassDocument.ClassMonitoringFileUploadUrl = foldermonitorCName + @"\" + monitorfileName + monitorextenstion;
                        CourseClassDocumentVM.CourseClassDocument.ClassMonitoringFileExtension = monitorextenstion;
                        CourseClassDocumentVM.CourseClassDocument.ClassMonitoringFileName = monitorfileName + monitorextenstion;

                        CourseClassDocumentVM.CourseClassDocument.CourseSessionFileUploadUrl = sessionCName + @"\" + sessionfileName + sessionextenstion;
                        CourseClassDocumentVM.CourseClassDocument.CourseSessionExtension = sessionextenstion;
                        CourseClassDocumentVM.CourseClassDocument.CourseSessionFileName = sessionfileName + sessionextenstion;


                    if (CourseClassDocumentVM.CourseClassDocument.Id == 0)
                    {
                        CourseClassDocumentVM.CourseClassDocument.QueryId = Guid.NewGuid();

                        CourseClassDocumentVM.CourseClassDocument.CreatedDate = DateTime.Now;
                        CourseClassDocumentVM.CourseClassDocument.CreatedBy = User.Identity.Name;
                        CourseClassDocumentVM.CourseClassDocument.CreatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                        CourseClassDocumentVM.CourseClassDocument.UpdatedDate = DateTime.Now;
                        CourseClassDocumentVM.CourseClassDocument.UpdatedBy = "-";
                        CourseClassDocumentVM.CourseClassDocument.UpdatedIp = "0.0.0.0";
                        CourseClassDocumentVM.CourseClassDocument.IsDeleted = false;
                        _unitOfWork.CourseClassDocument.Add(CourseClassDocumentVM.CourseClassDocument);

                    }
                    else
                    {
                        CourseClassDocumentVM.CourseClassDocument.UpdatedDate = DateTime.Now;
                        //courseLearning.UpdatedBy = User.Identity.Name;
                        //courseLearning.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                        CourseClassDocumentVM.CourseClassDocument.UpdatedBy = User.Identity.Name;
                        CourseClassDocumentVM.CourseClassDocument.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                        CourseClassDocumentVM.CourseClassDocument.IsDeleted = false;
                        _unitOfWork.CourseClassDocument.Update(CourseClassDocumentVM.CourseClassDocument);
                    }
                    _unitOfWork.Save();
                    return RedirectToAction(nameof(Index));
                }
                    else
                    {

                    //update when they do not change the image
                    //if (CourseClassDocumentVM.CourseClassDocument.Id != 0)
                    //{
                    //    CourseClassDocument objFromDb = _unitOfWork.CourseClassDocument.Get(CourseClassDocumentVM.CourseClassDocument.Id);
                    //    CourseClassDocumentVM.CourseClassDocument.ClassMonitoringFileUploadUrl = objFromDb.ClassMonitoringFileUploadUrl;
                    //}
                    ViewBag.Message = "Upload Class Document.";
                }

                    
                //}
                //else
                //{
                //    ViewBag.Message = "Upload Class Document.";
                //}

                
            }

            CourseClassDocumentVM.CourseHistoryLists = _unitOfWork.CourseHistory
                    .GetAll(includeProperties: "Course,Semester,Section,Instructor", filter: ch => ch.SemesterId == uniqueSetup.GetCurrentSemester().Id)
                    .Select(i => new SelectListItem
                    {
                        Text = i.Course.CourseCode + "(" + i.Section.SectionCode + ")-" + i.Instructor.ShortCode + ")",
                        Value = i.Id.ToString()
                    });
            return View(CourseClassDocumentVM);
        }




        public async Task<IActionResult> DownloadFile(int id)
        {
            var objFromDb = _unitOfWork.CourseClassDocument.Get(id);
            string webRootPath = _hostEnvironment.WebRootPath;
            string fileName = objFromDb.ClassMonitoringFileUploadUrl;
            if (string.IsNullOrEmpty(fileName) || fileName == null)
            {
                return Content("File Name is Empty...");
            }

            // get the filePath

            var imagePath = Path.Combine(webRootPath, objFromDb.ClassMonitoringFileUploadUrl.TrimStart('\\'));



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
            var allObj = _unitOfWork.SP_Call.List<CourseClassDocumentRVM>(SD.Proc_CourseClassDocument_GetWithParam, null);
            return Json(new { data = allObj });
        }
        protected virtual void GetLatestSemester()
        {
            ViewBag.InstructorInfo = uniqueSetup.GetInstructor(User.Identity.Name).Name + "(" + uniqueSetup.GetInstructor(User.Identity.Name).ShortCode + ")";
            ViewBag.Semester = uniqueSetup.GetCurrentSemester().Name + "(" + uniqueSetup.GetCurrentSemester().Code + ")";
        }
        //[HttpGet]
        //public IActionResult GetDAll(int? courseId)
        //{
        //    var allObj = _unitOfWork.CourseLearning.GetAll(c => c.CourseId == courseId);
        //    return Json(new { data = allObj });
        //}

        [HttpDelete]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.CourseLearning.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.CourseLearning.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion
    }
}

