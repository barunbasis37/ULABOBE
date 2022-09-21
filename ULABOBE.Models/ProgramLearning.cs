using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace ULABOBE.Models
{
    public class ProgramLearning :Entity
    {
        [Required]
        [StringLength(10, ErrorMessage = "PLO Code cannot be longer than {0} characters.", MinimumLength = 1)]
        [System.ComponentModel.DataAnnotations.Schema.Index("IX_Code", IsUnique = true)]
        [Display(Name = "PLO Code")]
        public string PLOCode { get; set; }
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
