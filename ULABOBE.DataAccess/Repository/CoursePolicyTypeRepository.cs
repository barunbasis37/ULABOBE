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
    public class CoursePolicyTypeRepository : Repository<CoursePolicyType>, ICoursePolicyTypeRepository
    {
        private readonly ApplicationDbContext _db;
        public CoursePolicyTypeRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

        public void Update(CoursePolicyType coursePolicyType)
        {
            _db.Update(coursePolicyType);

        }
    }
}
