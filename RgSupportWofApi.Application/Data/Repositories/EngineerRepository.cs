using System.Collections.Generic;
using System.Linq;
using RgSupportWofApi.Application.Model;

namespace RgSupportWofApi.Application.Data.Repositories
{
    public class EngineerRepository : IEngineerRepository
    {

        readonly DatabaseContext db;

        public EngineerRepository(DatabaseContext dbContext)
        {
            db = dbContext;
        }

        public virtual IList<Engineer> GetAll()
        {
            return db.Engineers.OrderBy(x=>x.Name).ToList();
        }

        public virtual int CountAll()
        {
            return db.Engineers.Count();
        }
    }
}
