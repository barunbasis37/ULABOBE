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
    public class CourseTypeRepository : Repository<CourseType>, ICourseTypeRepository
    {
        private readonly ApplicationDbContext _db;
        public CourseTypeRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

        public void Update(CourseType courseType)
        {
            _db.Update(courseType);

        }
    }
}
