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
    public class CoursePolicyProcedureRepository : Repository<CoursePolicyProcedure>, ICoursePolicyProcedureRepository
    {
        private readonly ApplicationDbContext _db;
        public CoursePolicyProcedureRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

        public void Update(CoursePolicyProcedure coursePolicyProcedure)
        {
            _db.Update(coursePolicyProcedure);

        }
    }
}
