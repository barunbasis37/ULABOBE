using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace ULABOBE.Models
{
    public class GenericSkillType : Entity
    {
        [Required]
        [StringLength(30, ErrorMessage = "Name cannot be longer than {0} characters.", MinimumLength = 1)]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Priority Level")]
        public int Priority { get; set; }
        [Required]
        [Display(Name = "Active")]
        public bool IsActive { get; set; }
        
    }
}
