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
    public class MasterSetupRepository : Repository<MasterSetup>, IMasterSetupRepository
    {
        private readonly ApplicationDbContext _db;
        public MasterSetupRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

        public void Update(MasterSetup masterSetup)
        {
            _db.Update(masterSetup);

        }
    }
}
