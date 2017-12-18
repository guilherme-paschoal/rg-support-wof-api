using System;
using System.Collections.Generic;
using RgSupportWofApi.Application.Model;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using RgSupportWofApi.Application.Helpers;

namespace RgSupportWofApi.Application.Data.Repositories
{
    public class EngineerRepository : IEngineerRepository
    {
        readonly DatabaseContext db;

        public EngineerRepository(DatabaseContext dbContext)
        {
            db = dbContext;
        }

        public Engineer Add(Engineer model)
        {
            var entity = db.Engineers.Add(model);
            db.SaveChanges();
            return entity.Entity;
        }

        public void Update(Engineer model) {
            db.Update(model);
            db.SaveChanges();
        }

        public IList<Engineer> GetAll()
        {
            return db.Engineers.Include(e => e.Shifts).Select(e => e).ToList();
        }

        public int CountAll() {
            return db.Engineers.Count();
        }

        public IList<Engineer> GetByShiftDate(DateTime date) 
        {
            var engineerIds = db.Shifts.Where(x => x.Date == date.ResetTime()).Select(s=>s.Engineer.Id);

            return db.Engineers.Include(e => e.Shifts)
                     .Where(e => engineerIds.Contains(e.Id)).Select(e => e).ToList();
        }

        public IList<Engineer> GetAvailableEngineersSince(DateTime sinceDate, int shiftsPerDay)
        {

            // Entity Framework Core isn't cooperating when I try to make this an optimized query by doing the whole "available engineers"
            // filter in a single query, specially when it comes to joining tables. For that reason, although it pains my heart I have to select
            // all engineers with all shifts and only then apply the filters. This proves the importance of using a repository pattern even when
            // using EF (because it exposes a repository itself), meaning I can, in the future, replace the version of EF, the ORM itself or even
            // write plain SQL queries.

            // Also, I want to point that although they are business rules (and I usually write these in a service layer), I wanted to keep the
            // filters in the repository because they are just query filters

            var yesterday = DateTime.Now.ResetTime().AddDays(-1);
            var availableEngineers = GetAll().Where(x => x.Shifts.Count(z => z.Date.Date >= sinceDate.Date) < shiftsPerDay)
                                             .Where(x => !x.Shifts.Any(z => z.Date.Date >= yesterday.Date))
                                             .ToList();
            return availableEngineers;

        }
    }
}
