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
    public class GenericSkillTypeRepository : Repository<GenericSkillType>, IGenericSkillTypeRepository
    {
        private readonly ApplicationDbContext _db;
        public GenericSkillTypeRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

        public void Update(GenericSkillType genericSkillType)
        {
            _db.Update(genericSkillType);

        }
    }
}
