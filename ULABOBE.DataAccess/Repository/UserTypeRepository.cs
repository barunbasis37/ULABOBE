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
    public class UserTypeRepository : Repository<UserType>, IUserTypeRepository
    {
        private readonly ApplicationDbContext _db;
        public UserTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(UserType userType)
        {
            var objFromDb = _db.UserTypes.FirstOrDefault(s => s.Id == userType.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = userType.Name;
                objFromDb.PriorityLevel = userType.PriorityLevel;
                objFromDb.UpdatedBy = userType.UpdatedBy;
                objFromDb.UpdatedIp = userType.UpdatedIp;
                objFromDb.UpdatedDate = userType.UpdatedDate;
                _db.SaveChanges();
            }
        }
    }
}
