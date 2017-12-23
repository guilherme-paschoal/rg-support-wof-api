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
            //this.engineerRepository = engineerRepository;
            this.shiftService = shiftService;
            this.engineerService = engineerService;
            this.shiftsPerDay = shiftsPerDay;
        }

        /// <summary>
        /// Spins the Wheel obtaning the Engineers that are going to work in the current day.
        /// </summary>
        /// <returns> List of Engineers that are going to work in the current day </returns>
        public IList<Engineer> SpinTheWheel()
        {
            List<Engineer> engineersWorkingToday;

            // Validate if the wheel has been spun today, if so, returns enginers working today
            engineersWorkingToday = shiftService.GetTodaysShifts().Select(x => x.Engineer).ToList();


            if (!engineersWorkingToday.Any())
            {

                var availableEngineers = engineerService.GetAvailableEngineers(shiftService.GetShiftsInCurrentPeriod(), shiftsPerDay);

                for (int i = 1; i <= shiftsPerDay; i++)
                {
                    var randomIndex = new Random(DateTime.Now.Millisecond).Next(0, availableEngineers.Count());
                    var randomEngineer = availableEngineers[randomIndex];

                    shiftService.InsertShift(new Shift
                    {
                        Date = DateTimeUtils.Today,
                        Engineer = randomEngineer,
                        ShiftOrder = i
                    });

                    availableEngineers.RemoveAt(randomIndex);
                    engineersWorkingToday.Add(randomEngineer);
                }

            }

            return engineersWorkingToday;

        }

    }
}
