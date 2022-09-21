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
    public class DepartmentLARRepository : Repository<DepartmentLAR>, IDepartmentLARRepository
    {
        private readonly ApplicationDbContext _db;
        public DepartmentLARRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

        public void Update(DepartmentLAR departmentLAR)
        {
            _db.Update(departmentLAR);

        }
    }
}
