using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace ULABOBE.Models
{
    public class ProfessionalSkill :Entity
    {
        [Required]
        [StringLength(10, ErrorMessage = "PS Code cannot be longer than {0} characters.", MinimumLength = 1)]
        [System.ComponentModel.DataAnnotations.Schema.Index("IX_Code", IsUnique = true)]
        [Display(Name = "Professional Skill Code")]
        public string PSCode { get; set; }
        //[Required]
        //[DisplayName("Type")]
        //public int CLOTypeId { get; set; }
        //[ForeignKey("CLOTypeId")]
        //public CourseLearningType CourseLearningType { get; set; }
        [Required]
        [Display(Name = "Priority")]
        public int Priority { get; set; }
        [Required]
        [Display(Name = "Active")]
        public bool IsActive { get; set; }
       
    }
}
