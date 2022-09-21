using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ULABOBE.Models;

namespace ULABOBE.Models
{
    public class Department : Entity
    {
        
        [Required]
        [StringLength(50, ErrorMessage = "Department Name cannot be longer than 50 characters.", MinimumLength = 1)]
        [Display(Name = "Department Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "Department Code cannot be longer than 10 characters.", MinimumLength = 1)]
        //[Index("IX_Code", IsUnique = true)]
        [Display(Name = "Department Code")]
        public string DepartmentCode { get; set; }

        [Required]
        [Display(Name = "School Name")]
        public int SchoolId { get; set; }

        [ForeignKey("SchoolId")] public virtual School School { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Type cannot be longer than 30 characters.", MinimumLength = 1)]
        //[Index("IX_Type")]
        [Display(Name = "Department Type")]
        public string Type { get; set; }

        [Required]
        [Display(Name = "Priority Level")]
        public int Priority { get; set; }

        [StringLength(100, ErrorMessage = "Description can be maximum 100 length")]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        [Required] [Display(Name = "Active")] public bool IsActive { get; set; }
        public virtual ICollection<ProgramData> Programs { get; set; }
        
    }
}
