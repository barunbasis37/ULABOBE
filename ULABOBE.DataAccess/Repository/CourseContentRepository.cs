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
    public class CourseContentRepository : Repository<CourseContent>, ICourseContentRepository
    {
        private readonly ApplicationDbContext _db;
        public CourseContentRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

        public void Update(CourseContent courseContent)
        {
            _db.Update(courseContent);

        }
    }
}
