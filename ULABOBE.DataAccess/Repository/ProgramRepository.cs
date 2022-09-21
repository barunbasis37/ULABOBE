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
    public class ProgramRepository : Repository<ProgramData>, IProgramRepository
    {
        private readonly ApplicationDbContext _db;
        public ProgramRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

        public void Update(ProgramData program)
        {
            //var objFromDb = _db.Programs.FirstOrDefault(s => s.Id == program.Id);
            //if (objFromDb!=null)
            //{
            //    objFromDb.Name = program.Name;
            //    objFromDb.ProgramCode = program.ProgramCode;
            //    objFromDb.Priority = program.Priority;
            //    objFromDb.UpdatedBy = program.UpdatedBy;
            //    objFromDb.UpdatedIp = program.UpdatedIp;
            //    objFromDb.UpdatedDate = program.UpdatedDate;
            //    _db.SaveChanges();
            //}
            _db.Update(program);
        }
    }
}
