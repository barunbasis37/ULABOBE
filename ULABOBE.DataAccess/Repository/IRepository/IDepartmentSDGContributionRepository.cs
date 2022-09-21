using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULABOBE.Models;

namespace ULABOBE.DataAccess.Repository.IRepository
{
    public interface IDepartmentSDGContributionRepository : IRepository<DepartmentSDGContribution>
    {
        void Update(DepartmentSDGContribution departmentSDGContribution);
    }
}
