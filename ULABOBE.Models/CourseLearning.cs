using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace ULABOBE.Models
{
    public class CourseLearning :Entity
    {
        [Required]
        [StringLength(10, ErrorMessage = "CLO Code cannot be longer than {0} characters.", MinimumLength = 1)]
        [System.ComponentModel.DataAnnotations.Schema.Index("IX_Code", IsUnique = true)]
        [Display(Name = "CLO Code")]
        public string CLOCode { get; set; }
        [Required]
        [DisplayName("Type")]
        public int CLOTypeId { get; set; }
        [ForeignKey("CLOTypeId")]
        public CourseLearningType CourseLearningType { get; set; }
        [Required]
        [Display(Name = "Priority Level")]
        public int PriorityLevel { get; set; }
        [Required]
        [Display(Name = "Active")]
        public bool IsActive { get; set; }
       
    }
}
