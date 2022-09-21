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
    public class CorrelationRepository : Repository<Correlation>, ICorrelationRepository
    {
        private readonly ApplicationDbContext _db;
        public CorrelationRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

        public void Update(Correlation correlation)
        {
            //var objFromDb = _db.Correlations.FirstOrDefault(s => s.Id == school.Id);
            //if (objFromDb!=null)
            //{
            //    objFromDb.Name = school.Name;
            //    objFromDb.CorrelationCode = school.CorrelationCode;
            //    objFromDb.PriorityLevel = school.PriorityLevel;
            //    objFromDb.UpdatedBy = school.UpdatedBy;
            //    objFromDb.UpdatedIp = school.UpdatedIp;
            //    objFromDb.UpdatedDate = school.UpdatedDate;
            //    _db.SaveChanges();
            //}
            _db.Update(correlation);
        }
    }
}
