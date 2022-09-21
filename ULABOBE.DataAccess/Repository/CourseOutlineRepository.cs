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
    public class CourseOutlineRepository : Repository<CourseOutline>, ICourseOutlineRepository
    {
        private readonly ApplicationDbContext _db;
        public CourseOutlineRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

        public void Update(CourseOutline courseOutline)
        {
            _db.Update(courseOutline);

        }
    }
}
