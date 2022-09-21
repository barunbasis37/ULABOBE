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
    public class LearningResourceTypeRepository : Repository<LearningResourceType>, ILearningResourceTypeRepository
    {
        private readonly ApplicationDbContext _db;
        public LearningResourceTypeRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

        public void Update(LearningResourceType learningResourceType)
        {
            _db.Update(learningResourceType);
        }
    }
}
