using System.Collections.Generic;
using System.Linq;
using RgSupportWofApi.Application.Helpers;
using RgSupportWofApi.Application.Model;

namespace RgSupportWofApi.Application.Services
{
    public class EngineerService : IEngineerService
    {
        public IList<Engineer> GetAvailableEngineers(IList<Shift> shifts, int shiftsPerDay) 
        {
            var control = true;
            var availableEngineers = new List<Engineer>();
            foreach(Shift s in shifts)
            {
                control &= (s.Date.Date < DateTimeUtils.Yesterday || shifts.Count(x => x.Engineer.Id == s.Id) < shiftsPerDay);
                if(control) { availableEngineers.Add(s.Engineer); }
            }

            return availableEngineers;
            
            // Gets all shifts since begining of period - this is the only database query
            //var shifts = shiftRepository.GetShiftsSince(startDate);

            // 3 business rules are covered in this linq query
            // 1 - Can't get engineers that are working today 
            // 2 - Can't get engineers that worked yesterday (Because they can't work shits on consecutive days)
            // 3 - Can't get engineers that worked a full day since the begining of the period
            //return (from shift in shifts
                    //where shift.Date.Date < DateTimeUtils.Yesterday 
                    //&& (shifts.GroupBy(x => x.Engineer.Id).Where(x => x.Count() < shiftsPerDay).Select(x => x.Key)).Contains(shift.Engineer.Id)
                    //select shift.Engineer).ToList();
            
            //var engineersThatWorkedYesterday = shifts.Where(x => x.Date.Date == DateTimeUtils.Yesterday.Date).Select(x => x.Engineer);
            //var engineersThatWorkedAFullDay = shifts.GroupBy(x => x.Engineer.Id).Where(x => x.Count() > 1).Select(x => x.Key);

        }

    }
}
