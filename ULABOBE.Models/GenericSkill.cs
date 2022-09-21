using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace ULABOBE.Models
{
    public class GenericSkill :Entity
    {
        [Required]
        [StringLength(10, ErrorMessage = "GK Code cannot be longer than {0} characters.", MinimumLength = 1)]
        [System.ComponentModel.DataAnnotations.Schema.Index("IX_Code", IsUnique = true)]
        [Display(Name = "Generic Skill Code")]
        public string GSCode { get; set; }
        [Required]
        [DisplayName("Type")]
        public int GSkillTypeId { get; set; }
        [ForeignKey("GSkillTypeId")]
        public GenericSkillType GenericSkillType { get; set; }
        [Required]
        [Display(Name = "Priority Level")]
        public int Priority { get; set; }
        [Required]
        [Display(Name = "Active")]
        public bool IsActive { get; set; }
       
    }
}
