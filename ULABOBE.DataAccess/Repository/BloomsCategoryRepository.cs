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
    public class BloomsCategoryRepository : Repository<BloomsCategory>, IBloomsCategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public BloomsCategoryRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

        public void Update(BloomsCategory bloomsCategory)
        {
            _db.Update(bloomsCategory);

        }
    }
}
