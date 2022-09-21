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
    public class ProfessionalSkillRepository : Repository<ProfessionalSkill>, IProfessionalSkillRepository
    {
        private readonly ApplicationDbContext _db;
        public ProfessionalSkillRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

        public void Update(ProfessionalSkill professionalSkill)
        {
            //var objFromDb = _db.Instructors.FirstOrDefault(s => s.Id == program.Id);
            //if (objFromDb!=null)
            //{
            //    objFromDb.Name = program.Name;
            //    objFromDb.InstructorCode = program.InstructorCode;
            //    objFromDb.Priority = program.Priority;
            //    objFromDb.UpdatedBy = program.UpdatedBy;
            //    objFromDb.UpdatedIp = program.UpdatedIp;
            //    objFromDb.UpdatedDate = program.UpdatedDate;
            //    _db.SaveChanges();
            //}
            _db.Update(professionalSkill);
        }
    }
}
