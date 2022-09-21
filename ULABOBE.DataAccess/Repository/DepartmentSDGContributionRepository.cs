using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULABOBE.DataAccess.Data;
using ULABOBE.DataAccess.Repository.IRepository;
using ULABOBE.Models;

namespace ULABOBE.DataAccess.Repository
{
    public class DepartmentSDGContributionRepository : Repository<DepartmentSDGContribution>, IDepartmentSDGContributionRepository
    {
        private readonly ApplicationDbContext _db;
        public DepartmentSDGContributionRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

        public void Update(DepartmentSDGContribution departmentSDGContribution)
        {
            _db.Update(departmentSDGContribution);

        }
    }
}
