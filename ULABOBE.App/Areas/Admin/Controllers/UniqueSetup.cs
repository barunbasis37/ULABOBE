using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using ULABOBE.DataAccess.Repository.IRepository;
using ULABOBE.Models;
using ULABOBE.Models.ViewModels;

namespace ULABOBE.App.Areas.Admin.Controllers
{
    public class UniqueSetup
    {
        private readonly IUnitOfWork _unitOfWork;

        public UniqueSetup(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Semester GetCurrentSemester()
        {
            int maxSemester = _unitOfWork.Semester.GetAll().Max(mS => mS.Id);
            Semester semester = _unitOfWork.Semester.Get(maxSemester);
            return semester;
        }

        public MasterSetup GetMaxMasterSetup(int courseHistoryId)
        {
            int maxSemester = _unitOfWork.Semester.GetAll().Max(mS => mS.Id);
            CourseHistory aCourseHistory =
                _unitOfWork.CourseHistory.GetFirstOrDefault(cH =>
                    cH.Id == courseHistoryId);
            Course aCourse = _unitOfWork.Course.GetFirstOrDefault(c => c.Id == aCourseHistory.CourseId);
            MasterSetup aMasterSetup =
                _unitOfWork.MasterSetup.GetFirstOrDefault(sc =>
                    sc.SemesterId == maxSemester && sc.ProgramId == aCourse.ProgramId);
            return aMasterSetup;
        }
        public Instructor GetInstructor(string userName)
        {
            var userInfoCheck = _unitOfWork.UserInfoCheck.GetFirstOrDefault(user => user.UserInfoId == userName);
            Instructor instructor = _unitOfWork.Instructor.GetFirstOrDefault(user => user.ShortCode == userInfoCheck.ShortCode);
            return instructor;
        }
    }
}
