using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ULABOBE.Models
{
    public class UserInfoCheck : Entity
    {
        
        [Required, Column(Order = 2)]
        //[Index("IX_UserId", IsUnique = true)]
        [System.ComponentModel.DataAnnotations.Schema.Index("IX_UserId",IsUnique = true)]
        [Display(Name = "User ID")]
        [StringLength(60)]
        public string UserInfoId { get; set; }
        [Required]
        [StringLength(150, ErrorMessage = "Name cannot be longer than 150 characters.", MinimumLength = 1)]
        //[Index("IX_Name", IsUnique = true)]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Short Code")]
        [StringLength(15)]
        [System.ComponentModel.DataAnnotations.Schema.Index("IX_ShortCode",IsUnique = true)]
        public string ShortCode { get; set; }
        [Display(Name = "Contact No")]
        [StringLength(15)]
        public string ContactNo { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Display(Name = "Program Code")]
        [StringLength(15)]
        public string ProgramSName { get; set; }
        [Display(Name = "Program Id")]
        public int ProgramId { get; set; }
        [Display(Name = "Department Code")]
        [StringLength(25)]
        public string DepartmentSName { get; set; }
        [Display(Name = "Department Id")]
        public int DepartmentId { get; set; }
        [Display(Name = "User Type")]
        [StringLength(15)]
        public string UserType { get; set; }
        [Display(Name = "User Type")]
        [StringLength(100)]
        public string Designation { get; set; }
        [Display(Name = "IS Active")]
        [StringLength(1)]
        public string IsActive { get; set; }
        [Display(Name = "Status")]
        [StringLength(15)]
        public string Status { get; set; }

    }
}
