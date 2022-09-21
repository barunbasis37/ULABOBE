using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ULABOBE.Models
{
    public class ProgramCatalog : Entity
    {
        [Required]
        [DisplayName("Program")]
        public int ProgramId { get; set; }
        [ForeignKey("ProgramId")]
        public ProgramData ProgramData { get; set; }

        [StringLength(30)]
        public string FileName { get; set; }
        [StringLength(20)]
        public string FileExtension { get; set; }
        [StringLength(150)]
        public string FileUploadUrl { get; set; }
        [Required]
        [DisplayName("Priority")]
        public int Priority { get; set; }
        [DisplayName("Is Active")]
        public bool IsActive { get; set; }

    }
}
