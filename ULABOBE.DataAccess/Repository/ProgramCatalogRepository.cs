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
    public class ProgramCatalogRepository : Repository<ProgramCatalog>, IProgramCatalogRepository
    {
        private readonly ApplicationDbContext _db;
        public ProgramCatalogRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

        public void Update(ProgramCatalog programCatalog)
        {
            _db.Update(programCatalog);

        }
    }
}
