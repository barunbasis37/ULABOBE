using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ULABOBE.DataAccess.Migrations;
using ULABOBE.DataAccess.Repository.IRepository;
using ULABOBE.Models;
using ULABOBE.Models.ViewModels;

namespace ULABOBE.App.Models
{
    public class AccessInformation
    {
        //private readonly IUnitOfWork _unitOfWork;

        //public AccessInformation(IUnitOfWork unitOfWork)
        //{
        //    _unitOfWork = unitOfWork;

        //}
        //public MasterSetup GetMaxMasterSetup(CourseHistoryVM courseHistoryVM)
        //{
        //    int maxSemester = _unitOfWork.Semester.GetAll().Max(mS => mS.Id);
        //    Course aCourse = _unitOfWork.Course.GetFirstOrDefault(c => c.Id == courseHistoryVM.CourseHistory.CourseId);
        //    MasterSetup aMasterSetup =
        //        _unitOfWork.MasterSetup.GetFirstOrDefault(sc =>
        //            sc.SemesterId == maxSemester && sc.ProgramId == aCourse.ProgramId);
        //    return aMasterSetup;
        //}
    }
}
