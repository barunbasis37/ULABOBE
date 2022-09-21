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
    public class UserInfoCheckRepository : Repository<UserInfoCheck>, IUserInfoCheckRepository
    {
        private readonly ApplicationDbContext _db;
        public UserInfoCheckRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(UserInfoCheck userInfoCheck)
        {

            _db.Update(userInfoCheck);

            //var objFromDb = _db.UserInfoChecks.FirstOrDefault(s => s.Id == userInfoCheck.Id);
            //if (objFromDb != null)
            //{
            //    objFromDb.Name = userInfoCheck.Name;
            //    objFromDb.ProgramId = userInfoCheck.ProgramId;
            //    objFromDb.UpdatedBy = userInfoCheck.UpdatedBy;
            //    objFromDb.UpdatedIp = userInfoCheck.UpdatedIp;
            //    objFromDb.UpdatedDate = userInfoCheck.UpdatedDate;
            //    _db.SaveChanges();
            //}
        }
    }
}
