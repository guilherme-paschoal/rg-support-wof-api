using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RgSupportWofApi.Application.Model;

namespace RgSupportWofApi.Application.Data.Repositories
{
    public class ShiftRepository : IShiftRepository
    {
        readonly DatabaseContext db;

        public ShiftRepository(DatabaseContext db)
        {
            this.db = db;
        }

        public Shift Add(Shift shift) 
        {
            return db.Add(shift).Entity;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public virtual IList<Shift> Filter(DateTime date, int engineerId) {
            var query = db.Shifts.Include(s => s.Engineer).Where(x => true);

            if(date != DateTime.MinValue) {
                query = query.Where(s => s.Date >= date);
            }

            if(engineerId > 0) {
                query = query.Where(s => s.Engineer.Id == engineerId);
            }

            return query.ToList();
        }

        public virtual IList<Shift> GetShiftsSince(DateTime date)
        {
            return db.Shifts.Include(s => s.Engineer).Where(s => s.Date >= date).ToList();
        }
    }
}
