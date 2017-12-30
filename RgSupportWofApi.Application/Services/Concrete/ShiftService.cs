using System;
using System.Collections.Generic;
using System.Linq;
using RgSupportWofApi.Application.Data.Repositories;
using RgSupportWofApi.Application.Helpers;
using RgSupportWofApi.Application.Model;

namespace RgSupportWofApi.Application.Services
{
    public class ShiftService : IShiftService
    {
        readonly IShiftRepository shiftRepository;

        public ShiftService(IShiftRepository shiftRepository)
        {
            this.shiftRepository = shiftRepository;
        }

        public IList<Shift> GetShiftsSinceDate(DateTime date)
        {
            return shiftRepository.GetShiftsSince(date.ResetTime()).OrderBy(s => s.Date).ThenBy(s => s.ShiftOrder).ToList();
        }

        public IList<Shift> GetTodaysShifts()
        {
            return shiftRepository.GetShiftsSince(DateTimeUtils.Today).OrderBy(s => s.ShiftOrder).ToList();
        }

        public IList<Shift> GetShiftsInCurrentPeriod(int daysInPeriod) 
        {
            // Having E = amount of engineers,
            // The number of days in a work period is the same as E because we are limiting the shifts per day to be a dividend of E
            // This way, we can guarantee that every E days is a work period meaning the search period to search for engineers for today is E - 1 
            var startDate = DateTime.Now.SubtractDays(daysInPeriod).ResetTime();

            return shiftRepository.GetShiftsSince(startDate).OrderByDescending(s => s.Date).ThenBy(s=>s.ShiftOrder).ToList();
        }

        // I didn't want to let Wheel Of Fate Service have a dependency on shift repository so I wrapped these repository calls this service class
        public Shift InsertShift(Shift shift) 
        {
            return shiftRepository.Add(shift);
        }

        public void SaveShifts() 
        {
            shiftRepository.Save();
        }
    }
}
