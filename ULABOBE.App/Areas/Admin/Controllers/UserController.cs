using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using ULABOBE.DataAccess.Repository.IRepository;
using ULABOBE.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using ULABOBE.DataAccess.Data;
using ULABOBE.Models.ViewModels;
using ULABOBE.Utility;


namespace ULABOBE.AppOnline.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Route("admin/school")]
    public class UserController : Controller
    {

        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<UserController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        //private readonly IUnitOfWork _unitOfWork;

        public UserController(ApplicationDbContext db,
            UserManager<IdentityUser> userManager,
            ILogger<UserController> logger,
            RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _logger = logger;
            _roleManager = roleManager;
            //_unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult ResetUserPassword()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetUserPassword(ResetUserPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByNameAsync(model.UserId);
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                //return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await _userManager.ResetPasswordAsync(user, code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetUserPassword", "User");
            }
            //AddErrors(result);
            return View();
        }

        [HttpGet]
        public IActionResult ImportUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ImportUser(IFormFile batchUsers)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (batchUsers?.Length > 0)
                    {
                        // convert to a stream
                        var stream = batchUsers.OpenReadStream();

                        List<UserInfoCheck> users = new List<UserInfoCheck>();


                        using (var package = new ExcelPackage(stream))
                        {
                            var worksheet = package.Workbook.Worksheets.First();
                            var rowCount = worksheet.Dimension.Rows;

                            for (var row = 2; row <= rowCount; row++)
                            {

                                var facultyId = worksheet.Cells[row, 1].Value?.ToString();
                                var facultyShortCode = worksheet.Cells[row, 2].Value?.ToString();
                                var name = worksheet.Cells[row, 3].Value?.ToString();
                                var email = worksheet.Cells[row, 4].Value?.ToString();
                                var phone = worksheet.Cells[row, 5].Value?.ToString();
                                var designation = worksheet.Cells[row, 6].Value?.ToString();
                                //var programId = worksheet.Cells[row, 7].Value?.ToString();
                                var programId = worksheet.Cells[row, 7].Value?.ToString();
                                var programCode = worksheet.Cells[row, 8].Value?.ToString();
                                var departmentId = worksheet.Cells[row, 9].Value?.ToString();
                                var departmentCode = worksheet.Cells[row, 10].Value?.ToString();
                                var status = worksheet.Cells[row, 11].Value?.ToString();
                                var isActive = worksheet.Cells[row, 12].Value?.ToString();

                                //Upload Faculty Information Column Name: UserId, Short_Code, Name, Email, Contact, Designation, Program_Id, Pro_Code, Dept_Id, Dept_Code, Status, Active

                                var useracc = new ApplicationUser()
                                {
                                    UserName = facultyId,
                                    Name = name,
                                    Email = email,
                                    ProgramId = Convert.ToInt32(programId),
                                    PhoneNumber = phone,
                                    EmailConfirmed = true,

                                };
                                var result = await _userManager.CreateAsync(useracc, "Ulab@123");
                                if (result.Succeeded)
                                {
                                    _logger.LogInformation("User created a new account with password.");



                                    await _userManager.AddToRoleAsync(useracc, SD.Role_Faculty);
                                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(useracc);
                                    var resultemail = await _userManager.ConfirmEmailAsync(useracc, code);
                                    //users.Add(user);
                                    if (resultemail.Succeeded)
                                    {
                                        var user = new Models.UserInfoCheck()
                                        {
                                            UserInfoId = facultyId,
                                            ShortCode = facultyShortCode,
                                            Email = email,
                                            Name = name,
                                            Designation = designation,
                                            ContactNo = phone,
                                            ProgramSName = programCode,
                                            ProgramId = Convert.ToInt32(programId),
                                            DepartmentSName = departmentCode,
                                            DepartmentId = Convert.ToInt32(departmentId),
                                            Status = "status",
                                            IsActive = "Y",
                                            UserType = "Faculty",
                                            QueryId = Guid.NewGuid(),
                                            CreatedDate = DateTime.Now,
                                            CreatedBy = User.Identity.Name,
                                            CreatedIp = Request.HttpContext.Connection.LocalIpAddress.ToString(),
                                            UpdatedDate = DateTime.Now,
                                            UpdatedBy = "N/A",
                                            UpdatedIp = "0.0.0.0",
                                            IsDeleted = false,
                                        };
                                        _db.UserInfoChecks.Add(user);
                                        _db.SaveChanges();
                                    }

                                }


                            }

                            return View("Index", users);
                        }


                    }

                    return View();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            return RedirectToAction("Index", "User", new { Area = "Admin" });
        }

        #region API CALLS

        [HttpGet]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult GetAll()
        {
            var userList = _db.ApplicationUsers.Include(u => u.ProgramType).ToList();
            var userRole = _db.UserRoles.ToList();
            var roles = _db.Roles.ToList();
            foreach (var user in userList)
            {
                var roleId = userRole.FirstOrDefault(u => u.UserId == user.Id).RoleId;
                user.Role = roles.FirstOrDefault(u => u.Id == roleId).Name;
                if (user.ProgramType == null)
                {
                    user.ProgramType = new ProgramData()
                    {
                        Name = ""
                    };
                }
            }

            return Json(new { data = userList });
        }

        [HttpPost]
        [Authorize(Roles = SD.Role_SuperAdmin)]
        public IActionResult LockUnlock([FromBody] string id)
        {
            var objFromDb = _db.ApplicationUsers.FirstOrDefault(u => u.Id == id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while Locking/Unlocking" });
            }
            if (objFromDb.LockoutEnd != null && objFromDb.LockoutEnd > DateTime.Now)
            {
                //user is currently locked, we will unlock them
                objFromDb.LockoutEnd = DateTime.Now;
            }
            else
            {
                objFromDb.LockoutEnd = DateTime.Now.AddYears(1000);
            }
            _db.SaveChanges();
            return Json(new { success = true, message = "Operation Successful." });
        }

        #endregion
    }
}