﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULABOBE.DataAccess.Data;
using ULABOBE.DataAccess.Repository.IRepository;
using ULABOBE.Models;

namespace ULABOBE.DataAccess.Repository
{
    public class WeekDayRepository : Repository<WeekDay>, IWeekDayRepository
    {
        private readonly ApplicationDbContext _db;
        public WeekDayRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

        public void Update(WeekDay weekDay)
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
            _db.Update(weekDay);
        }
    }
}
