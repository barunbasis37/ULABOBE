using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using ULABOBE.DataAccess.Repository.IRepository;
using ULABOBE.Models;
using ULABOBE.Models.ViewModels;

namespace ULABOBE.AppOnline.Areas.Faculty.Controllers
{
    [Area("Faculty")]
    [Authorize(Roles = "Faculty")]
    public class ProgramPLOController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProgramPLOController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        #region API Calls

    
        [HttpGet]
        public JsonResult GetDAll(int? programId)
        {

            var ProgramCLOLists = _unitOfWork.ProgramPLO.GetAll(cid => cid.ProgramId == programId, includeProperties: "ProgramLearning").Select(i => new SelectListItem
            {
                Text = i.ProgramLearning.PLOCode,
                Value = i.Id.ToString()
            });
            return Json(ProgramCLOLists);
        }

    
        #endregion
    }
}
