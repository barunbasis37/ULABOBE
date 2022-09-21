using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ULABOBE.Models
{
    public class CourseProgramMapping: Entity
    {
        [DisplayName("Course")]
        public int CourseHistoryId { get; set; }
        [ForeignKey("CourseHistoryId")]
        public CourseHistory CourseHistory { get; set; }
        public int CourseLearningId { get; set; }
        [ForeignKey("CourseLearningId")]
        public CourseLearning CourseLearning { get; set; }
        [StringLength(50)]
        public string PLoSelectedIDs { get; set; }
        [DisplayName("PLO Covered")]
        [StringLength(100)]
        public string PLoSelectedIDNames { get; set; }
        //[StringLength(50)]
        //public string CLOSelectedIDs { get; set; }
        //[DisplayName("CLO Covered")]
        //[StringLength(100)]
        //public string CLOSelectedIDNames { get; set; }
        [StringLength(50)]
        public string GSSelectedIDs { get; set; }
        [DisplayName("GS Covered")]
        [StringLength(100)]
        public string GSSelectedIDNames { get; set; }
        [StringLength(50)]
        public string PSSelectedIDs { get; set; }
        [DisplayName("PS Covered")]
        [StringLength(100)]
        public string PSSelectedIDNames { get; set; }
        [StringLength(50)]
        public string SDGSelectedIDs { get; set; }
        [DisplayName("SDG Covered")]
        [StringLength(100)]
        public string SDGSelectedIDNames { get; set; }
        [StringLength(50)]
        public string ARSelectedIDs { get; set; }
        [DisplayName("AR Covered")]
        [StringLength(100)]
        public string ARSelectedIDNames { get; set; }
        [DisplayName("Is-Approved")]
        public bool IsApproved { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Approved By")]
        public string ApprovedBy { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Approved IP")]
        public string ApprovedIp { get; set; }
        [Required]
        [Display(Name = "Approved Date")]
        [DisplayFormat(DataFormatString = "{0:ddd, MMM d, yyyy}")]
        public DateTime ApprovedDate { get; set; }
        [DisplayName("Is-Submitted")]
        public bool IsSubmitted { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Submitted By")]
        public string SubmittedBy { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Submitted IP")]
        public string SubmittedIp { get; set; }
        [Required]
        [Display(Name = "Submitted Date")]
        [DisplayFormat(DataFormatString = "{0:ddd, MMM d, yyyy}")]
        public DateTime SubmittedDate { get; set; }


    }
}
