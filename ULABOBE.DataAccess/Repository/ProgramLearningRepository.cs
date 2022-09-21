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
    public class ProgramLearningRepository : Repository<ProgramLearning>, IProgramLearningRepository
    {
        private readonly ApplicationDbContext _db;
        public ProgramLearningRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

        public void Update(ProgramLearning programLearning)
        {
            //var objFromDb = _db.Schools.FirstOrDefault(s => s.Id == school.Id);
            //if (objFromDb!=null)
            //{
            //    objFromDb.Name = school.Name;
            //    objFromDb.SchoolCode = school.SchoolCode;
            //    objFromDb.PriorityLevel = school.PriorityLevel;
            //    objFromDb.UpdatedBy = school.UpdatedBy;
            //    objFromDb.UpdatedIp = school.UpdatedIp;
            //    objFromDb.UpdatedDate = school.UpdatedDate;
            //    _db.SaveChanges();
            //}
            _db.Update(programLearning);
        }
    }
}
