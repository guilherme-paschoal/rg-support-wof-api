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
        readonly IEngineerRepository engineerRepository;

        public ShiftService(IShiftRepository shiftRepository, IEngineerRepository engineerRepository)
        {
            this.shiftRepository = shiftRepository;
            this.engineerRepository = engineerRepository;
        }

        public IList<Shift> GetTodaysShifts() => shiftRepository.GetShiftsSince(DateTimeUtils.Today).OrderBy(s => s.ShiftOrder).ToList();

        public IList<Shift> GetShiftsInCurrentPeriod() => shiftRepository.GetShiftsSince(GetSearchPeriodStartDate()).ToList();

        DateTime GetSearchPeriodStartDate()
        {
            // Having E = amount of engineers,
            // The number of days in a work period is the same as E because we are limiting the shifts per day to be a dividend of E
            // This way, we can guarantee that every E days is a work period meaning the search period to search for engineers for today is E - 1 
            return DateTime.Now.AddDays(engineerRepository.CountAll() - 1 * -1).ResetTime();
        }


        // I didn't want to let Wheel Of Fate Service have a dependency on shift repository so I wrapped this add call in this service class
        public void InsertShift(Shift shift) 
        {
            shiftRepository.Add(shift);
        }

        public void SaveShifts() 
        {
            shiftRepository.Save();
        }
    }
}
