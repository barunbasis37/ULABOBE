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
    public class ScheduleRepository : Repository<Schedule>, IScheduleRepository
    {
        private readonly ApplicationDbContext _db;
        public ScheduleRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

        public void Update(Schedule schedule)
        {
            _db.Update(schedule);

        }
    }
}
