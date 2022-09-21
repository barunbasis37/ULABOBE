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
    public class TimeRepository : Repository<Time>, ITimeRepository
    {
        private readonly ApplicationDbContext _db;
        public TimeRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

        public void Update(Time time)
        {
            _db.Update(time);

        }
    }
}
