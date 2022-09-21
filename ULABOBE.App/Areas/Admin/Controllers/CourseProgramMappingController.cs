using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Reporting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
//using NLog.Web.LayoutRenderers;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using ULABOBE.App.Areas.Admin.Controllers;
using ULABOBE.DataAccess.Repository.IRepository;
using ULABOBE.Models;
using ULABOBE.Models.ReportViewModels;
using ULABOBE.Models.ViewModels;
using ULABOBE.Utility;

namespace ULABOBE.AppOnline.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Super-Admin")]
    [Authorize]
    public class CourseProgramMappingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvirnoment;

        public CourseProgramMappingController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
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

        private UniqueSetup uniqueSetup;
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Upsert(int? id)
        {
            int maxSemester = _unitOfWork.Semester.GetAll().Max(mS => mS.Id);
            Semester semester = _unitOfWork.Semester.Get(maxSemester);
            //uniqueSetup = new UniqueSetup(_unitOfWork);
            ViewBag.Semester = semester.Name + "(" + semester.Code + ")";
            CourseProgramMappingVM courseProgramMappingCLOVM = new CourseProgramMappingVM()
            {
                CourseProgramMapping = new CourseProgramMapping(),
                CourseHistoryLists = _unitOfWork.CourseHistory.GetAll(filter: ch => ch.SemesterId == semester.Id, includeProperties: "Course,Semester,Section,Instructor").Select(i => new SelectListItem
                {
                    Text = i.Course.CourseCode +"("+i.Section.SectionCode+")-" + i.Instructor.ShortCode,
                    Value = i.Id.ToString()
                }),

                CourseLearningLists = _unitOfWork.CourseLearning.GetAll().Select(i => new SelectListItem
                {
                    Text = i.CLOCode,
                    Value = i.Id.ToString()
                }),

                ProgramLearningLists = _unitOfWork.ProgramLearning.GetAll().Select(i => new SelectListItem
                {
                    Text = i.PLOCode,
                    Value = i.Id.ToString()
                }),
                GenericSkillLists = _unitOfWork.GenericSkill.GetAll().Select(i => new SelectListItem
                {
                    Text = i.GSCode,
                    Value = i.Id.ToString()
                }),
                ProfessionalSkillLists = _unitOfWork.ProfessionalSkill.GetAll().Select(i => new SelectListItem
                {
                    Text = i.PSCode,
                    Value = i.Id.ToString()
                }),
                SDGLists = _unitOfWork.SDGContribution.GetAll().Select(i => new SelectListItem
                {
                    Text = i.SDGCode,
                    Value = i.Id.ToString()
                }),
                LearningAssessmentRubricLists = _unitOfWork.LearningAssessmentRubric.GetAll().Select(i => new SelectListItem
                {
                    Text = i.LARCode,
                    Value = i.Id.ToString()
                }),


            };

            if (id == null)
            {
                //this is for create
                return View(courseProgramMappingCLOVM);
            }
            //this is for edit

            if (courseProgramMappingCLOVM.CourseProgramMapping != null)
            {
                courseProgramMappingCLOVM.CourseProgramMapping = _unitOfWork.CourseProgramMapping.Get(id.GetValueOrDefault());
                courseProgramMappingCLOVM.PLoSelectedIDArray =
                    courseProgramMappingCLOVM.CourseProgramMapping.PLoSelectedIDs.Split(',').ToArray();

                courseProgramMappingCLOVM.GSSelectedIDArray =
                    courseProgramMappingCLOVM.CourseProgramMapping.GSSelectedIDs.Split(',').ToArray();
                courseProgramMappingCLOVM.PSSelectedIDArray =
                    courseProgramMappingCLOVM.CourseProgramMapping.PSSelectedIDs.Split(',').ToArray();
                courseProgramMappingCLOVM.SDGSelectedIDArray =
                    courseProgramMappingCLOVM.CourseProgramMapping.SDGSelectedIDs.Split(',').ToArray();
                courseProgramMappingCLOVM.ARSelectedIDArray =
                    courseProgramMappingCLOVM.CourseProgramMapping.ARSelectedIDs.Split(',').ToArray();

            }

            if (courseProgramMappingCLOVM.CourseProgramMapping == null)
            {
                return NotFound();
            }
            return View(courseProgramMappingCLOVM);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Upsert(CourseProgramMappingVM courseProgramMappingVM)
        {
            int maxSemester = _unitOfWork.Semester.GetAll().Max(mS => mS.Id);
            Semester semester = _unitOfWork.Semester.Get(maxSemester);
            List<string> plostringArray = new List<string>();
            foreach (var ploID in courseProgramMappingVM.PLoSelectedIDArray)
            {
                ProgramLearning programLearning = _unitOfWork.ProgramLearning.Get(Convert.ToInt32(ploID));
                plostringArray.Add(programLearning.PLOCode);
            }

            List<string> gsstringArray = new List<string>();
            foreach (var gsID in courseProgramMappingVM.GSSelectedIDArray)
            {
                GenericSkill genericSkill = _unitOfWork.GenericSkill.Get(Convert.ToInt32(gsID));
                gsstringArray.Add(genericSkill.GSCode);
            }

            List<string> psstringArray = new List<string>();
            foreach (var psID in courseProgramMappingVM.PSSelectedIDArray)
            {
                ProfessionalSkill professionalSkill = _unitOfWork.ProfessionalSkill.Get(Convert.ToInt32(psID));
                psstringArray.Add(professionalSkill.PSCode);
            }

            List<string> sdgstringArray = new List<string>();
            foreach (var sdgID in courseProgramMappingVM.SDGSelectedIDArray)
            {
                SDGContribution sdgcontribution = _unitOfWork.SDGContribution.Get(Convert.ToInt32(sdgID));
                sdgstringArray.Add(sdgcontribution.SDGCode);
            }

            List<string> arstringArray = new List<string>();
            foreach (var arID in courseProgramMappingVM.ARSelectedIDArray)
            {
                LearningAssessmentRubric learningAssessmentRubric = _unitOfWork.LearningAssessmentRubric.Get(Convert.ToInt32(arID));
                arstringArray.Add(learningAssessmentRubric.LARCode);
            }

            if (ModelState.IsValid)
            {
                courseProgramMappingVM.CourseProgramMapping.PLoSelectedIDs = string.Join(",", courseProgramMappingVM.PLoSelectedIDArray);
                courseProgramMappingVM.CourseProgramMapping.PLoSelectedIDNames = String.Join(",", plostringArray);

                courseProgramMappingVM.CourseProgramMapping.GSSelectedIDs = string.Join(",", courseProgramMappingVM.GSSelectedIDArray);
                courseProgramMappingVM.CourseProgramMapping.GSSelectedIDNames = String.Join(",", gsstringArray);

                courseProgramMappingVM.CourseProgramMapping.PSSelectedIDs = string.Join(",", courseProgramMappingVM.PSSelectedIDArray);
                courseProgramMappingVM.CourseProgramMapping.PSSelectedIDNames = String.Join(",", psstringArray);

                courseProgramMappingVM.CourseProgramMapping.SDGSelectedIDs = string.Join(",", courseProgramMappingVM.SDGSelectedIDArray);
                courseProgramMappingVM.CourseProgramMapping.SDGSelectedIDNames = String.Join(",", sdgstringArray);

                courseProgramMappingVM.CourseProgramMapping.ARSelectedIDs = string.Join(",", courseProgramMappingVM.ARSelectedIDArray);
                courseProgramMappingVM.CourseProgramMapping.ARSelectedIDNames = String.Join(",", arstringArray);

                if (courseProgramMappingVM.CourseProgramMapping.Id == 0)
                {

                    courseProgramMappingVM.CourseProgramMapping.QueryId = Guid.NewGuid();

                    courseProgramMappingVM.CourseProgramMapping.CreatedDate = DateTime.Now;
                    courseProgramMappingVM.CourseProgramMapping.CreatedBy = User.Identity.Name;
                    courseProgramMappingVM.CourseProgramMapping.CreatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    courseProgramMappingVM.CourseProgramMapping.UpdatedDate = DateTime.Now;
                    courseProgramMappingVM.CourseProgramMapping.UpdatedBy = "N/A";
                    courseProgramMappingVM.CourseProgramMapping.UpdatedIp = "0.0.0.0";
                    courseProgramMappingVM.CourseProgramMapping.IsDeleted = false;
                    courseProgramMappingVM.CourseProgramMapping.ApprovedDate = DateTime.Now;
                    courseProgramMappingVM.CourseProgramMapping.ApprovedBy = "N/A";
                    courseProgramMappingVM.CourseProgramMapping.ApprovedIp = "0.0.0.0";
                    courseProgramMappingVM.CourseProgramMapping.IsApproved = false;
                    _unitOfWork.CourseProgramMapping.Add(courseProgramMappingVM.CourseProgramMapping);

                }
                else
                {

                    courseProgramMappingVM.CourseProgramMapping.UpdatedDate = DateTime.Now;
                    courseProgramMappingVM.CourseProgramMapping.UpdatedBy = User.Identity.Name;
                    courseProgramMappingVM.CourseProgramMapping.UpdatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    courseProgramMappingVM.CourseProgramMapping.ApprovedDate = DateTime.Now;
                    courseProgramMappingVM.CourseProgramMapping.ApprovedBy = User.Identity.Name;
                    courseProgramMappingVM.CourseProgramMapping.ApprovedIp = Request.HttpContext.Connection.LocalIpAddress.ToString();
                    courseProgramMappingVM.CourseProgramMapping.IsDeleted = false;
                    _unitOfWork.CourseProgramMapping.Update(courseProgramMappingVM.CourseProgramMapping);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));

            }
            courseProgramMappingVM.CourseHistoryLists = _unitOfWork.CourseHistory
                .GetAll(includeProperties: "Course,Semester,Section,Instructor", filter: ch => ch.SemesterId == semester.Id)
                .Select(i => new SelectListItem
                {
                    Text = i.Course.CourseCode + "(" + i.Section.SectionCode + ")-" + i.Instructor.ShortCode + ")",
                    Value = i.Id.ToString()
                });
            
            courseProgramMappingVM.CourseLearningLists = _unitOfWork.CourseLearning.GetAll().Select(i => new SelectListItem
            {
                Text = i.CLOCode,
                Value = i.Id.ToString()
            });
            courseProgramMappingVM.ProgramLearningLists = _unitOfWork.ProgramLearning.GetAll().Select(i =>
                new SelectListItem
                {
                    Text = i.PLOCode,
                    Value = i.Id.ToString()
                });
            courseProgramMappingVM.GenericSkillLists = _unitOfWork.GenericSkill.GetAll().Select(i => new SelectListItem
            {
                Text = i.GSCode,
                Value = i.Id.ToString()
            });
            courseProgramMappingVM.ProfessionalSkillLists = _unitOfWork.ProfessionalSkill.GetAll().Select(i =>
                new SelectListItem
                {
                    Text = i.PSCode,
                    Value = i.Id.ToString()
                });
            courseProgramMappingVM.SDGLists = _unitOfWork.SDGContribution.GetAll().Select(i => new SelectListItem
            {
                Text = i.SDGCode,
                Value = i.Id.ToString()
            });
            courseProgramMappingVM.LearningAssessmentRubricLists = _unitOfWork.LearningAssessmentRubric.GetAll().Select(
                i => new SelectListItem
                {
                    Text = i.LARCode,
                    Value = i.Id.ToString()
                });
            return View(courseProgramMappingVM);
        }


        //private MasterSetup GetMaxMasterSetup(CourseProgramMappingVM courseProgramMappingVM)
        //{
        //    int maxSemester = _unitOfWork.Semester.GetAll().Max(mS => mS.Id);
        //    Course aCourse = _unitOfWork.Course.GetFirstOrDefault(c => c.Id == courseProgramMappingVM.CourseProgramMapping.CourseId);
        //    MasterSetup aMasterSetup =
        //        _unitOfWork.MasterSetup.GetFirstOrDefault(sc =>
        //            sc.SemesterId == maxSemester && sc.ProgramId == aCourse.ProgramId);
        //    return aMasterSetup;
        //}

        #region API Calls

        [HttpGet]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult GetAll()
        {
            int maxSemester = _unitOfWork.Semester.GetAll().Max(mS => mS.Id);
            var allObj = _unitOfWork.CourseProgramMapping.GetAll(filter: cPMapping=> cPMapping.CourseHistory.SemesterId== maxSemester, includeProperties: "CourseHistory,CourseLearning,CourseHistory.Course,CourseHistory.Semester,CourseHistory.Section,CourseHistory.Instructor");
            return Json(new { data = allObj });
        }
        //[HttpGet]
        //public JsonResult GetDAll(int? courseId)
        //{

        //     var CourseCLOLists = _unitOfWork.CourseCLO.GetAll(cid=>cid.CourseId==courseId, includeProperties: "CourseLearning").Select(i => new SelectListItem
        //     {
        //         Text = i.CourseLearning.CLOCode,
        //         Value = i.Id.ToString()
        //     });
        //    return Json(CourseCLOLists);
        //}

        [HttpDelete]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.CourseProgramMapping.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.CourseProgramMapping.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion
        

        #region ReportPrint

        public IActionResult PrintCourseProgramMappingAll()
        {
            string mimtype = "";
            int extension = 1;
            var path = $"{this._webHostEnvirnoment.WebRootPath}\\Reporting\\rptCourseProMapViewAll.rdlc";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            //parameters.Add("UserId", User.Identity.Name);
            //get products from product table 
            LocalReport localReport = new LocalReport(path);
            var allObj = _unitOfWork.SP_Call.List<CourseProgramMappingRVM>(SD.Proc_CourseProMap_ViewGetAll, null);
            localReport.AddDataSource("CourseProMapViewDS", allObj);
            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimtype);
            return File(result.MainStream, "application/pdf");

            //IEnumerable<CourseProgramMapping> courseProgramMappings = _unitOfWork.SP_Call.List<CourseProgramMapping>(SD.Proc_CourseProMap_GetAll);
            //List<CourseProgramMapping> allObj = new List<CourseProgramMapping>();
            //CourseProgramMappingRVM aCourseProgramMappingRVM = new CourseProgramMappingRVM();
            //foreach (CourseProgramMapping aCourseProMapp in courseProgramMappings)
            //{
            //    aCourseProgramMappingRVM.CourseCode = aCourseProMapp.CourseHistory.Course.CourseCode;
            //    aCourseProgramMappingRVM.CLOName = aCourseProMapp.CourseLearning.CLOCode;
            //    //aCourseProgramMappingRVM.Description = aCourseProMapp.CourseLearning..;
            //    aCourseProgramMappingRVM.ARSelectedName = aCourseProMapp.ARSelectedIDNames;
            //    allObj.Add(aCourseProgramMappingRVM);
            //}
        }




        public IActionResult PrintCourseProgramMapping()
        {
            string mimtype = "";
            int extension = 1;
            var path = $"{this._webHostEnvirnoment.WebRootPath}\\Reporting\\rptCourseProMapViewAll.rdlc";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            //parameters.Add("UserId", User.Identity.Name);
            //get products from product table 
            LocalReport localReport = new LocalReport(path);
            var allObj = _unitOfWork.SP_Call
                .List<CourseProgramMapping>(SD.Proc_CourseProMap_GetAll,null);
            localReport.AddDataSource("CourseProMapViewDS", allObj);
            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimtype);
            return File(result.MainStream, "application/pdf");
        }

        #endregion
    }
}
