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
    public class DepartmentGSkillRepository : Repository<DepartmentGSkill>, IDepartmentGSkillRepository
    {
        private readonly ApplicationDbContext _db;
        public DepartmentGSkillRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

        public void Update(DepartmentGSkill departmentGSkill)
        {
            _db.Update(departmentGSkill);

        }
    }
}
