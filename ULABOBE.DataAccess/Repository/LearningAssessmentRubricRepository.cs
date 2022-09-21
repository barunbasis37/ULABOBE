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
    public class LearningAssessmentRubricRepository : Repository<LearningAssessmentRubric>, ILearningAssessmentRubricRepository
    {
        private readonly ApplicationDbContext _db;
        public LearningAssessmentRubricRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

        public void Update(LearningAssessmentRubric learningAssessmentRubric)
        {
            _db.Update(learningAssessmentRubric);

        }
    }
}
