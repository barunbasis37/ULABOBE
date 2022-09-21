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
    public class AssessmentTechniqueWeightageRepository : Repository<AssessmentTechniqueWeightage>, IAssessmentTechniqueWeightageRepository
    {
        private readonly ApplicationDbContext _db;
        public AssessmentTechniqueWeightageRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

        public void Update(AssessmentTechniqueWeightage assessmentTechniqueWeightage)
        {
            _db.Update(assessmentTechniqueWeightage);
        }
    }
}
