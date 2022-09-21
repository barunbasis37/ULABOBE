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
    public class Entity
    {
        [Required(ErrorMessage = "Id Required"), Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Column(Order = 2)]
        public Guid QueryId { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Created IP")]
        public string CreatedIp { get; set; }
        [Required]
        [Display(Name = "Created Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime CreatedDate { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Updated By")]
        public string UpdatedBy { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Updated IP")]
        public string UpdatedIp { get; set; }
        [Required]
        [Display(Name = "Updated Date")]
        public DateTime UpdatedDate { get; set; }
        [Required]
        [Display(Name = "Delete Status")]
        public bool IsDeleted { get; set; }

    }
}
