using System;
using System.Collections.Generic;
using System.Linq;
using RgSupportWofApi.Application.Data.Repositories;
using RgSupportWofApi.Application.Helpers;
using RgSupportWofApi.Application.Model;
using RgSupportWofApi.Application.Services.Exceptions;

namespace RgSupportWofApi.Application.Services
{
    public class WheelOfFateService : IWheelOfFateService
    {
        //readonly IEngineerRepository engineerRepository;
        readonly int shiftsPerDay;

        readonly IShiftService shiftService;
        readonly IEngineerService engineerService;

        public WheelOfFateService(IShiftService shiftService, IEngineerService engineerService, int shiftsPerDay)
        {
            this.shiftService = shiftService;
            this.engineerService = engineerService;
            this.shiftsPerDay = shiftsPerDay;
        }

        /// <summary>
        /// Spins the Wheel obtaning the Engineers that are going to work in the current day.
        /// </summary>
        /// <returns> List of Engineers that are going to work in the current day </returns>
        public IList<Shift> SpinTheWheel()
        {
            List<Shift> todaysShifts;

            // Validate if the wheel has been spun today, if so, returns enginers working today
            todaysShifts = shiftService.GetTodaysShifts().ToList();

            if (!todaysShifts.Any())
            {
                var availableEngineers = engineerService.GetAvailableEngineers(shiftService.GetShiftsInCurrentPeriod(GetDaysInCurrentPeriod()), shiftsPerDay);

                for (int i = 1; i <= shiftsPerDay; i++)
                {
                    var randomIndex = new Random(DateTime.Now.Millisecond).Next(0, availableEngineers.Count());
                    var randomEngineer = availableEngineers[randomIndex];
                    availableEngineers.RemoveAt(randomIndex);

                    todaysShifts.Add(
                        shiftService.InsertShift(new Shift {
                            Date = DateTimeUtils.Today,
                            Engineer = randomEngineer,
                            ShiftOrder = i
                        })
                    );
                }

                shiftService.SaveShifts();
            }

            return todaysShifts;

        }

        int GetDaysInCurrentPeriod()
        {
            return engineerService.GetEngineerCount() - 1;
        }
    }
}
