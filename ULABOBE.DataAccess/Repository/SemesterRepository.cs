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
    public class SemesterRepository : Repository<Semester>, ISemesterRepository
    {
        private readonly ApplicationDbContext _db;
        public SemesterRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

        public void Update(Semester semester)
        {
            _db.Update(semester);

        }
    }
}
