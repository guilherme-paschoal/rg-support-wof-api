using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RgSupportWofApi.Application.Helpers;
using RgSupportWofApi.Application.Model;

namespace RgSupportWofApi.Application.Data.Repositories
{
    public class ShiftRepository : IShiftRepository
    {
        readonly DatabaseContext db;

        public ShiftRepository(DatabaseContext dbContext)
        {
            db = dbContext;
        }

        public IList<Shift> GetByDate(DateTime date)
        {
            return db.Shifts.Include(x => x.Engineer).Where(x => x.Date == date.ResetTime()).ToList();
        }
    }
}
