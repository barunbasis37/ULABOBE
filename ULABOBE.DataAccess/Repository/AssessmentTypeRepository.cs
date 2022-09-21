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
    public class AssessmentTypeRepository : Repository<AssessmentType>, IAssessmentTypeRepository
    {
        private readonly ApplicationDbContext _db;
        public AssessmentTypeRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

        public void Update(AssessmentType assessmentType)
        {
            _db.Update(assessmentType);

        }
    }
}
