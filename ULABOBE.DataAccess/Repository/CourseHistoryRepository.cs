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
    public class CourseHistoryRepository : Repository<CourseHistory>, ICourseHistoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CourseHistoryRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

        public void Update(CourseHistory courseHistory)
        {
            _db.Update(courseHistory);

        }
    }
}
