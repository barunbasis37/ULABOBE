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
using Microsoft.AspNetCore.Http;

namespace ULABOBE.App.Areas.Faculty.Controllers
{
    [Area("Faculty")]
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
        [Authorize(Roles = SD.Role_Faculty)]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = SD.Role_Faculty)]
        public IActionResult Upsert(int? id)
        {

            //uniqueSetup = new UniqueSetup(_unitOfWork);
            ViewBag.Semester = uniqueSetup.GetCurrentSemester().Name + "(" + uniqueSetup.GetCurrentSemester().Code + ")";
            CourseClassDocumentVM CourseClassDocumentVM = new CourseClassDocumentVM()
            {
                CourseClassDocument = new CourseClassDocument(),
                CourseHistoryLists = _unitOfWork.CourseHistory
                    .GetAll(includeProperties: "Course,Semester,Section,Instructor", filter: ch => ch.SemesterId == uniqueSetup.GetCurrentSemester().Id && ch.Instructor==uniqueSetup.GetInstructor(User.Identity.Name))
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
        [Authorize(Roles = SD.Role_Faculty)]
        public IActionResult Upsert(CourseClassDocumentVM CourseClassDocumentVM)
        {

            if (ModelState.IsValid)
            {
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                CourseHistory aCourseHistory = _unitOfWork.CourseHistory.GetFirstOrDefault(filter: ch => ch.Id == CourseClassDocumentVM.CourseClassDocument.CourseHistoryId, includeProperties: "Course,Semester,Section,Instructor,Course.Program");



                if (CourseClassDocumentVM.CourseClassDocument.Id == 0)
                {
                    if (files.Count < 4)
                    {
                        ViewBag.Message = "Please Upload Class Documents(Monitor/Session/Semester/Lession/Mapping/Attendance Report).";
                    }
                    else
                    {
                        foreach (var file in files)
                        {

                            var fileContainerName = Path.GetFileName(file.Name);

                            if (fileContainerName == "monitoruploadfiles")
                            {
                                string monitorfileName, foldermonitorCName, monitorextenstion;
                                StoreMonitorReport(CourseClassDocumentVM, webRootPath, file, aCourseHistory, out monitorfileName, out foldermonitorCName, out monitorextenstion);
                                CourseClassDocumentVM.CourseClassDocument.ClassMonitoringFileUploadUrl = foldermonitorCName + @"\" + monitorfileName + monitorextenstion;
                                CourseClassDocumentVM.CourseClassDocument.ClassMonitoringFileExtension = monitorextenstion;
                                CourseClassDocumentVM.CourseClassDocument.ClassMonitoringFileName = monitorfileName + monitorextenstion;
                            }
                            else if (fileContainerName == "sessionuploadfiles")
                            {
                                string sessionfileName, foldersessionCName, sessionextenstion;
                                StoreSessionReport(CourseClassDocumentVM, webRootPath, file, aCourseHistory, out sessionfileName, out foldersessionCName, out sessionextenstion);
                                CourseClassDocumentVM.CourseClassDocument.CourseSessionFileUploadUrl = foldersessionCName + @"\" + sessionfileName + sessionextenstion;
                                CourseClassDocumentVM.CourseClassDocument.CourseSessionExtension = sessionextenstion;
                                CourseClassDocumentVM.CourseClassDocument.CourseSessionFileName = sessionfileName + sessionextenstion;
                            }
                            else if (fileContainerName == "semesterCoursefiles")
                            {
                                string semestercoursefileName, foldersemestercourseCName, semesterextenstion;
                                StoreSemesterCoursereport(CourseClassDocumentVM, webRootPath, file, aCourseHistory, out semestercoursefileName, out foldersemestercourseCName, out semesterextenstion);

                                CourseClassDocumentVM.CourseClassDocument.SemesterCourseFileUploadUrl = foldersemestercourseCName + @"\" + semestercoursefileName + semesterextenstion;
                                CourseClassDocumentVM.CourseClassDocument.SemesterCourseExtension = semesterextenstion;
                                CourseClassDocumentVM.CourseClassDocument.SemesterCourseFileName = semestercoursefileName + semesterextenstion;
                            }
                            else if (fileContainerName == "lessonPlanTempfiles")
                            {
                                string lessonfileName, folderlessionCName, lessionextenstion;
                                StoreLessionReport(CourseClassDocumentVM, webRootPath, file, aCourseHistory, out lessonfileName, out folderlessionCName, out lessionextenstion);

                                CourseClassDocumentVM.CourseClassDocument.LessonPlanFileUploadUrl = folderlessionCName + @"\" + lessonfileName + lessionextenstion;
                                CourseClassDocumentVM.CourseClassDocument.LessonPlanExtension = lessionextenstion;
                                CourseClassDocumentVM.CourseClassDocument.LessonPlanFileName = lessonfileName + lessionextenstion;
                            }
                            else if (fileContainerName == "courseProMapfiles")
                            {
                                string courseProgramMapfileName, foldercourseProMapCName, crouseProgramMapextenstion;
                                StoreCourseProMapReport(CourseClassDocumentVM, webRootPath, file, aCourseHistory, out courseProgramMapfileName, out foldercourseProMapCName, out crouseProgramMapextenstion);

                                CourseClassDocumentVM.CourseClassDocument.CourseProgramFileUploadUrl = foldercourseProMapCName + @"\" + courseProgramMapfileName + crouseProgramMapextenstion;
                                CourseClassDocumentVM.CourseClassDocument.CourseProgramExtension = crouseProgramMapextenstion;
                                CourseClassDocumentVM.CourseClassDocument.CourseProgramFileName = courseProgramMapfileName + crouseProgramMapextenstion;


                            }
                            else if (fileContainerName == "attendanceSheetfiles")
                            {
                                string attendancefileName, folderattendanceCName, attendanceextenstion;
                                StoreAttendanceReport(CourseClassDocumentVM, webRootPath, file, aCourseHistory, out attendancefileName, out folderattendanceCName, out attendanceextenstion);


                                CourseClassDocumentVM.CourseClassDocument.AttendanceSheetFileUploadUrl = folderattendanceCName + @"\" + attendancefileName + attendanceextenstion;
                                CourseClassDocumentVM.CourseClassDocument.AttendanceSheetExtension = attendanceextenstion;
                                CourseClassDocumentVM.CourseClassDocument.AttendanceSheetFileName = attendancefileName + attendanceextenstion;
                            }
                            
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
                        //_unitOfWork.Save();
                    }
                }
                else
                {
                             
                    foreach (IFormFile file in files)
                    {

                        var fileContainerName = Path.GetFileName(file.Name);

                        if (fileContainerName == "monitoruploadfiles")
                        {
                            string monitorfileName, foldermonitorCName, monitorextenstion;
                            StoreMonitorReport(CourseClassDocumentVM, webRootPath, file, aCourseHistory, out monitorfileName, out foldermonitorCName, out monitorextenstion);
                            CourseClassDocumentVM.CourseClassDocument.ClassMonitoringFileUploadUrl = foldermonitorCName + @"\" + monitorfileName + DateTime.Now.Millisecond + monitorextenstion;
                            CourseClassDocumentVM.CourseClassDocument.ClassMonitoringFileExtension = monitorextenstion;
                            CourseClassDocumentVM.CourseClassDocument.ClassMonitoringFileName = monitorfileName + DateTime.Now.Millisecond + monitorextenstion;
                        }
                        else if (fileContainerName == "sessionuploadfiles")
                        {
                            string sessionfileName, foldersessionCName, sessionextenstion;
                            StoreSessionReport(CourseClassDocumentVM, webRootPath, file, aCourseHistory, out sessionfileName, out foldersessionCName, out sessionextenstion);
                            CourseClassDocumentVM.CourseClassDocument.CourseSessionFileUploadUrl = foldersessionCName + @"\" + sessionfileName + DateTime.Now.Millisecond + sessionextenstion;
                            CourseClassDocumentVM.CourseClassDocument.CourseSessionExtension = sessionextenstion;
                            CourseClassDocumentVM.CourseClassDocument.CourseSessionFileName = sessionfileName + DateTime.Now.Millisecond + sessionextenstion;
                        }
                        else if (fileContainerName == "semesterCoursefiles")
                        {
                            string semestercoursefileName, foldersemestercourseCName, semesterextenstion;
                            StoreSemesterCoursereport(CourseClassDocumentVM, webRootPath, file, aCourseHistory, out semestercoursefileName, out foldersemestercourseCName, out semesterextenstion);

                            CourseClassDocumentVM.CourseClassDocument.SemesterCourseFileUploadUrl = foldersemestercourseCName + @"\" + semestercoursefileName + DateTime.Now.Millisecond + semesterextenstion;
                            CourseClassDocumentVM.CourseClassDocument.SemesterCourseExtension = semesterextenstion;
                            CourseClassDocumentVM.CourseClassDocument.SemesterCourseFileName = semestercoursefileName + DateTime.Now.Millisecond + semesterextenstion;
                        }
                        else if (fileContainerName == "lessonPlanTempfiles")
                        {
                            string lessonfileName, folderlessionCName, lessionextenstion;
                            StoreLessionReport(CourseClassDocumentVM, webRootPath, file, aCourseHistory, out lessonfileName, out folderlessionCName, out lessionextenstion);

                            CourseClassDocumentVM.CourseClassDocument.LessonPlanFileUploadUrl = folderlessionCName + @"\" + lessonfileName + DateTime.Now.Millisecond + lessionextenstion;
                            CourseClassDocumentVM.CourseClassDocument.LessonPlanExtension = lessionextenstion;
                            CourseClassDocumentVM.CourseClassDocument.LessonPlanFileName = lessonfileName + DateTime.Now.Millisecond + lessionextenstion;
                        }
                        else if (fileContainerName == "courseProMapfiles")
                        {
                            string courseProgramMapfileName, foldercourseProMapCName, crouseProgramMapextenstion;
                            StoreCourseProMapReport(CourseClassDocumentVM, webRootPath, file, aCourseHistory, out courseProgramMapfileName, out foldercourseProMapCName, out crouseProgramMapextenstion);

                            CourseClassDocumentVM.CourseClassDocument.CourseProgramFileUploadUrl = foldercourseProMapCName + @"\" + courseProgramMapfileName + DateTime.Now.Millisecond + crouseProgramMapextenstion;
                            CourseClassDocumentVM.CourseClassDocument.CourseProgramExtension = crouseProgramMapextenstion;
                            CourseClassDocumentVM.CourseClassDocument.CourseProgramFileName = courseProgramMapfileName + DateTime.Now.Millisecond + crouseProgramMapextenstion;
                        }
                        else if (fileContainerName == "attendanceSheetfiles")
                        {
                            string attendancefileName, folderattendanceCName, attendanceextenstion;
                            StoreAttendanceReport(CourseClassDocumentVM, webRootPath, file, aCourseHistory, out attendancefileName, out folderattendanceCName, out attendanceextenstion);


                            CourseClassDocumentVM.CourseClassDocument.AttendanceSheetFileUploadUrl = folderattendanceCName + @"\" + attendancefileName + DateTime.Now.Millisecond + attendanceextenstion;
                            CourseClassDocumentVM.CourseClassDocument.AttendanceSheetExtension = attendanceextenstion;
                            CourseClassDocumentVM.CourseClassDocument.AttendanceSheetFileName = attendancefileName + DateTime.Now.Millisecond + attendanceextenstion;
                        }                      

                    }                    

                    CourseClassDocumentVM.CourseClassDocument.UpdatedDate = DateTime.Now;
                    CourseClassDocumentVM.CourseClassDocument.UpdatedBy = User.Identity.Name;
                    CourseClassDocumentVM.CourseClassDocument.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    CourseClassDocumentVM.CourseClassDocument.IsDeleted = false;

                    _unitOfWork.CourseClassDocument.Update(CourseClassDocumentVM.CourseClassDocument);

                }
                
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));


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

        private void StoreAttendanceReport(CourseClassDocumentVM CourseClassDocumentVM, string webRootPath, IFormFile file, CourseHistory aCourseHistory, out string attendancefileName, out string folderattendanceCName, out string attendanceextenstion)
        {
            string modifyFileTag = DateTime.Now.Month + "-" + DateTime.Now.Day + "-" + DateTime.Now.Millisecond;
            if (CourseClassDocumentVM.CourseClassDocument.Id != 0)
            {
                attendancefileName = "ATD-" + aCourseHistory.Semester.Code + "-" + aCourseHistory.Course.CourseCode + "-" + aCourseHistory.Section.SectionCode + "-" + aCourseHistory.Instructor.ShortCode + "-" + modifyFileTag;
            }
            else
            {
                attendancefileName = "ATD-" + aCourseHistory.Semester.Code + "-" + aCourseHistory.Course.CourseCode + "-" + aCourseHistory.Section.SectionCode + "-" + aCourseHistory.Instructor.ShortCode;
            }

            string folderattendanceDynamic = aCourseHistory.Semester.Code + @"\" + aCourseHistory.Course.Program.ProgramCode + @"\" + aCourseHistory.Instructor.Name + " (" + aCourseHistory.Instructor.ShortCode + ")";
            folderattendanceCName = @"Document\CourseClass\" + folderattendanceDynamic;
            var attendanceuploads = Path.Combine(webRootPath, folderattendanceCName);
            // If directory does not exist, create it
            if (!Directory.Exists(attendanceuploads))
            {
                Directory.CreateDirectory(attendanceuploads);
            }


            attendanceextenstion = null;
            if (file.Name == "attendanceSheetfiles")
            {
                attendanceextenstion = Path.GetExtension(file.FileName);
                if (CourseClassDocumentVM.CourseClassDocument.AttendanceSheetFileUploadUrl != null)
                {
                    //this is an edit and we need to remove old image
                    var filePath = Path.Combine(webRootPath, CourseClassDocumentVM.CourseClassDocument.AttendanceSheetFileUploadUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }
                using (var filesattendanceStreams = new FileStream(Path.Combine(attendanceuploads, attendancefileName + attendanceextenstion), FileMode.Create))
                {
                    file.CopyTo(filesattendanceStreams);
                }
            }
            else
            {
                ViewBag.Message = "Please Upload Class Documents(Monitor/Session/Semester/Lession/Mapping/Attendance Report).";
            }
        }

        private void StoreCourseProMapReport(CourseClassDocumentVM CourseClassDocumentVM, string webRootPath, IFormFile file, CourseHistory aCourseHistory, out string courseProgramMapfileName, out string foldercourseProMapCName, out string crouseProgramMapextenstion)
        {
            string modifyFileTag = DateTime.Now.Month + "-" + DateTime.Now.Day + "-" + DateTime.Now.Millisecond;
            if (CourseClassDocumentVM.CourseClassDocument.Id != 0)
            {
                courseProgramMapfileName = "CPM-" + aCourseHistory.Semester.Code + "-" + aCourseHistory.Course.CourseCode + "-" + aCourseHistory.Section.SectionCode + "-" + aCourseHistory.Instructor.ShortCode + "-" + modifyFileTag;
            }
            else
            {
                courseProgramMapfileName = "CPM-" + aCourseHistory.Semester.Code + "-" + aCourseHistory.Course.CourseCode + "-" + aCourseHistory.Section.SectionCode + "-" + aCourseHistory.Instructor.ShortCode;
            }

            string foldecourseProMapDynamic = aCourseHistory.Semester.Code + @"\" + aCourseHistory.Course.Program.ProgramCode + @"\" + aCourseHistory.Instructor.Name + " (" + aCourseHistory.Instructor.ShortCode + ")";
            foldercourseProMapCName = @"Document\CourseClass\" + foldecourseProMapDynamic;
            var courseProMapuploads = Path.Combine(webRootPath, foldercourseProMapCName);
            // If directory does not exist, create it
            if (!Directory.Exists(courseProMapuploads))
            {
                Directory.CreateDirectory(courseProMapuploads);
            }

            crouseProgramMapextenstion = null;
            if (file.Name == "courseProMapfiles")
            {
                crouseProgramMapextenstion = Path.GetExtension(file.FileName);
                if (CourseClassDocumentVM.CourseClassDocument.CourseProgramFileUploadUrl != null)
                {
                    //this is an edit and we need to remove old image
                    var filePath = Path.Combine(webRootPath, CourseClassDocumentVM.CourseClassDocument.CourseProgramFileUploadUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }
                using (var filescourseProMapStreams = new FileStream(Path.Combine(courseProMapuploads, courseProgramMapfileName + crouseProgramMapextenstion), FileMode.Create))
                {
                    file.CopyTo(filescourseProMapStreams);
                }
            }
            else
            {
                ViewBag.Message = "Please Upload Class Documents(Monitor/Session/Semester/Lession/Mapping/Attendance Report).";
            }
        }

        private void StoreLessionReport(CourseClassDocumentVM CourseClassDocumentVM, string webRootPath, IFormFile file, CourseHistory aCourseHistory, out string lessonfileName, out string folderlessionCName, out string lessionextenstion)
        {
            string modifyFileTag = DateTime.Now.Month + "-" + DateTime.Now.Day + "-" + DateTime.Now.Millisecond;
            if (CourseClassDocumentVM.CourseClassDocument.Id != 0)
            {
                lessonfileName = "LPT-" + aCourseHistory.Semester.Code + "-" + aCourseHistory.Course.CourseCode + "-" + aCourseHistory.Section.SectionCode + "-" + aCourseHistory.Instructor.ShortCode + "-" + modifyFileTag;
            }
            else
            {
                lessonfileName = "LPT-" + aCourseHistory.Semester.Code + "-" + aCourseHistory.Course.CourseCode + "-" + aCourseHistory.Section.SectionCode + "-" + aCourseHistory.Instructor.ShortCode;
            }

            string foldelerlessionDynamic = aCourseHistory.Semester.Code + @"\" + aCourseHistory.Course.Program.ProgramCode + @"\" + aCourseHistory.Instructor.Name + " (" + aCourseHistory.Instructor.ShortCode + ")";
            folderlessionCName = @"Document\CourseClass\" + foldelerlessionDynamic;
            var lessionuploads = Path.Combine(webRootPath, folderlessionCName);
            // If directory does not exist, create it
            if (!Directory.Exists(lessionuploads))
            {
                Directory.CreateDirectory(lessionuploads);
            }

            lessionextenstion = null;
            if (file.Name == "lessonPlanTempfiles")
            {
                lessionextenstion = Path.GetExtension(file.FileName);
                if (CourseClassDocumentVM.CourseClassDocument.LessonPlanFileUploadUrl != null)
                {
                    //this is an edit and we need to remove old image
                    var filePath = Path.Combine(webRootPath, CourseClassDocumentVM.CourseClassDocument.LessonPlanFileUploadUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }
                using (var fileslessionStreams = new FileStream(Path.Combine(lessionuploads, lessonfileName + lessionextenstion), FileMode.Create))
                {
                    file.CopyTo(fileslessionStreams);
                }
            }
            else
            {
                ViewBag.Message = "Please Upload Class Documents(Monitor/Session/Semester/Lession/Mapping/Attendance Report).";
            }
        }

        private void StoreSemesterCoursereport(CourseClassDocumentVM CourseClassDocumentVM, string webRootPath, IFormFile file, CourseHistory aCourseHistory, out string semestercoursefileName, out string foldersemestercourseCName, out string semesterextenstion)
        {
            string modifyFileTag = DateTime.Now.Month + "-" + DateTime.Now.Day + "-" + DateTime.Now.Millisecond;
            if (CourseClassDocumentVM.CourseClassDocument.Id != 0)
            {
                semestercoursefileName = "SCR-" + aCourseHistory.Semester.Code + "-" + aCourseHistory.Course.CourseCode + "-" + aCourseHistory.Section.SectionCode + "-" + aCourseHistory.Instructor.ShortCode + "-" + modifyFileTag;
            }
            else
            {
                semestercoursefileName = "SCR-" + aCourseHistory.Semester.Code + "-" + aCourseHistory.Course.CourseCode + "-" + aCourseHistory.Section.SectionCode + "-" + aCourseHistory.Instructor.ShortCode;
            }

            string foldesemestercourseDynamic = aCourseHistory.Semester.Code + @"\" + aCourseHistory.Course.Program.ProgramCode + @"\" + aCourseHistory.Instructor.Name + " (" + aCourseHistory.Instructor.ShortCode + ")";
            foldersemestercourseCName = @"Document\CourseClass\" + foldesemestercourseDynamic;
            var semestercourseuploads = Path.Combine(webRootPath, foldersemestercourseCName);
            // If directory does not exist, create it
            if (!Directory.Exists(semestercourseuploads))
            {
                Directory.CreateDirectory(semestercourseuploads);
            }

            semesterextenstion = null;
            if (file.Name == "semesterCoursefiles")
            {
                semesterextenstion = Path.GetExtension(file.FileName);
                if (CourseClassDocumentVM.CourseClassDocument.SemesterCourseFileUploadUrl != null)
                {
                    //this is an edit and we need to remove old image
                    var filePath = Path.Combine(webRootPath, CourseClassDocumentVM.CourseClassDocument.SemesterCourseFileUploadUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }
                using (var filessemesterStreams = new FileStream(Path.Combine(semestercourseuploads, semestercoursefileName + semesterextenstion), FileMode.Create))
                {
                    file.CopyTo(filessemesterStreams);
                }
            }
            else
            {
                ViewBag.Message = "Please Upload Class Documents(Monitor/Session/Semester/Lession/Mapping/Attendance Report).";
            }
        }

        private void StoreSessionReport(CourseClassDocumentVM CourseClassDocumentVM, string webRootPath, IFormFile file, CourseHistory aCourseHistory, out string sessionfileName, out string foldersessionCName, out string sessionextenstion)
        {
            string modifyFileTag = DateTime.Now.Month + "-" + DateTime.Now.Day + "-" + DateTime.Now.Millisecond;
            if (CourseClassDocumentVM.CourseClassDocument.Id != 0)
            {
                sessionfileName = "CSR-" + aCourseHistory.Semester.Code + "-" + aCourseHistory.Course.CourseCode + "-" + aCourseHistory.Section.SectionCode + "-" + aCourseHistory.Instructor.ShortCode + "-" + modifyFileTag;
            }
            else
            {
                sessionfileName = "CSR-" + aCourseHistory.Semester.Code + "-" + aCourseHistory.Course.CourseCode + "-" + aCourseHistory.Section.SectionCode + "-" + aCourseHistory.Instructor.ShortCode;
            }

            string foldesessionDynamic = aCourseHistory.Semester.Code + @"\" + aCourseHistory.Course.Program.ProgramCode + @"\" + aCourseHistory.Instructor.Name + " (" + aCourseHistory.Instructor.ShortCode + ")";
            foldersessionCName = @"Document\CourseClass\" + foldesessionDynamic;
            var sessionuploads = Path.Combine(webRootPath, foldersessionCName);
            // If directory does not exist, create it
            if (!Directory.Exists(sessionuploads))
            {
                Directory.CreateDirectory(sessionuploads);
            }


            sessionextenstion = null;
            if (file.Name == "sessionuploadfiles")
            {
                sessionextenstion = Path.GetExtension(file.FileName);
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
                    file.CopyTo(filessessionStreams);
                }
            }
            else
            {
                ViewBag.Message = "Please Upload Class Documents(Monitor/Session/Semester/Lession/Mapping/Attendance Report).";
            }
        }

        private void StoreMonitorReport(CourseClassDocumentVM CourseClassDocumentVM, string webRootPath, IFormFile file, CourseHistory aCourseHistory, out string monitorfileName, out string foldermonitorCName, out string monitorextenstion)
        {
            string modifyFileTag = DateTime.Now.Month + "-" + DateTime.Now.Day + "-" + DateTime.Now.Millisecond;
            if (CourseClassDocumentVM.CourseClassDocument.Id != 0)
            {
                monitorfileName = "CMR-" + aCourseHistory.Semester.Code + "-" + aCourseHistory.Course.CourseCode + "-" + aCourseHistory.Section.SectionCode + "-" + aCourseHistory.Instructor.ShortCode + "-" + modifyFileTag;
            }
            else
            {
                monitorfileName = "CMR-" + aCourseHistory.Semester.Code + "-" + aCourseHistory.Course.CourseCode + "-" + aCourseHistory.Section.SectionCode + "-" + aCourseHistory.Instructor.ShortCode;
            }

            string foldermonitorDynamic = aCourseHistory.Semester.Code + @"\" + aCourseHistory.Course.Program.ProgramCode + @"\" + aCourseHistory.Instructor.Name + " (" + aCourseHistory.Instructor.ShortCode + ")";
            foldermonitorCName = @"Document\CourseClass\" + foldermonitorDynamic;
            var monitoruploads = Path.Combine(webRootPath, foldermonitorCName);
            // If directory does not exist, create it
            if (!Directory.Exists(monitoruploads))
            {
                Directory.CreateDirectory(monitoruploads);
            }

            monitorextenstion = null;
            if (file.Name == "monitoruploadfiles")
            {
                monitorextenstion = Path.GetExtension(file.FileName);
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
                    file.CopyTo(filesStreams);
                }
            }
            else
            {
                ViewBag.Message = "Please Upload Class Documents(Monitor/Session/Semester/Lession/Mapping/Attendance Report).";
            }
        }



        public async Task<IActionResult> DownloadFile(int id)
        {
            var objFromDb = _unitOfWork.CourseClassDocument.Get(id);
            string webRootPath = _hostEnvironment.WebRootPath;
            string fileName = objFromDb.ClassMonitoringFileUploadUrl;
            if (string.IsNullOrEmpty(fileName) || fileName == null)
            {
                return Content("File Does Not Uploaded...");
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
        [Authorize(Roles = SD.Role_Faculty)]
        public IActionResult GetAll()
        {
            int maxSemester = _unitOfWork.Semester.GetAll().Max(mS => mS.Id);
            UserInfoCheck userInfoCheck = _unitOfWork.UserInfoCheck.GetFirstOrDefault(user => user.UserInfoId == User.Identity.Name);
            string instructorId = userInfoCheck.UserInfoId;
            var allObj = _unitOfWork.SP_Call.List<CourseClassDocumentRVM>(SD.Proc_CourseClassDocument_GetWithParam, null).Where(cO => cO.UserInfoId == instructorId && cO.SemesterId == maxSemester);
            return Json(new { data = allObj });
        }
        protected virtual void GetLatestSemester()
        {
            ViewBag.InstructorInfo = uniqueSetup.GetInstructor(User.Identity.Name).Name + "(" + uniqueSetup.GetInstructor(User.Identity.Name).ShortCode + ")";
            ViewBag.Semester = uniqueSetup.GetCurrentSemester().Name + "(" + uniqueSetup.GetCurrentSemester().Code + ")";
        }
      

        

        #endregion
    }
}

