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
    public class CourseClassDocumentRepository : Repository<CourseClassDocument>, ICourseClassDocumentRepository
    {
        private readonly ApplicationDbContext _db;
        public CourseClassDocumentRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

        public void Update(CourseClassDocument courseClassDocument)
        {
            _db.Update(courseClassDocument);

        }
    }
}
