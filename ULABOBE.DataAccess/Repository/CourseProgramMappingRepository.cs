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
    public class CourseProgramMappingRepository : Repository<CourseProgramMapping>, ICourseProgramMappingRepository
    {
        private readonly ApplicationDbContext _db;
        public CourseProgramMappingRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

        public void Update(CourseProgramMapping courseProgramMapping)
        {
            _db.Update(courseProgramMapping);

        }
    }
}
