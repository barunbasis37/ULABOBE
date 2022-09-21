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
    public class GenericSkillRepository : Repository<GenericSkill>, IGenericSkillRepository
    {
        private readonly ApplicationDbContext _db;
        public GenericSkillRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

        public void Update(GenericSkill genericSkill)
        {
            _db.Update(genericSkill);

        }
    }
}
