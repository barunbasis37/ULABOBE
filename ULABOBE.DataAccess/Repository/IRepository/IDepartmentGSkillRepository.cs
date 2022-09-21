using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULABOBE.Models;

namespace ULABOBE.DataAccess.Repository.IRepository
{
    public interface IDepartmentGSkillRepository : IRepository<DepartmentGSkill>
    {
        void Update(DepartmentGSkill departmentGSkill);
    }
}
