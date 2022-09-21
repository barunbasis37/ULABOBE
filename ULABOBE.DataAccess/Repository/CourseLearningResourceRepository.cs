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
    public class CourseLearningResourceRepository : Repository<CourseLearningResource>, ICourseLearningResourceRepository
    {
        private readonly ApplicationDbContext _db;
        public CourseLearningResourceRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

        public void Update(CourseLearningResource courseLearningResource)
        {
            _db.Update(courseLearningResource);

        }
    }
}
