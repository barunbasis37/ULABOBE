using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ULABOBE.Models.ViewModels
{
    public class ProgramCatalogVM
    {
        public ProgramCatalog ProgramCatalog { get; set; }
        public IEnumerable<SelectListItem> ProgramLists { get; set; }
    }
}
