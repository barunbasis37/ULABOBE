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
    public class AssessmentPatternRepository : Repository<AssessmentPattern>, IAssessmentPatternRepository
    {
        private readonly ApplicationDbContext _db;
        public AssessmentPatternRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

        public void Update(AssessmentPattern assessmentPattern)
        {
            _db.Update(assessmentPattern);

        }
    }
}
