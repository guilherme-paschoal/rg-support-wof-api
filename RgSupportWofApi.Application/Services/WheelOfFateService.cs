using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using RgSupportWofApi.Application.Data.Repositories;
using RgSupportWofApi.Application.Helpers;
using RgSupportWofApi.Application.Model;

namespace RgSupportWofApi.Application.Services
{
    public class WheelOfFateService : IWheelOfFateService
    {
        readonly IEngineerRepository engineerRepository;
        readonly IShiftRepository shiftRepository;
        readonly int shiftsPerDay;


        public WheelOfFateService(IEngineerRepository engineerRepository, IShiftRepository shiftRepository)
        {
            this.engineerRepository = engineerRepository;
            this.shiftRepository = shiftRepository;

            //improve: replace this for a configuration
            shiftsPerDay = 2;
        }

        public IList<Engineer> SpinTheWheel() {
            var date = DateTime.Now.ResetTime();
            // Validate if the wheel has been spun today, if so, returns enginers working today
            var engineersWorkingToday = GetEngineersWorkingToday(date);
            if (engineersWorkingToday.Count() == shiftsPerDay)
                return engineersWorkingToday;

            // I assumed that the amount of shifts per day and the amount of engineers is variable, for that reason, 
            // calculations need to be done accordingly:

            // The number of days in the period is "Current Date minus the amount of Engineers in the company".
            // By doing it this way we can guarantee that every engineer works for at least a full day 
            // in a fair distribution, no matter how big the period is or how many shifts engineers work per day 

            //var startDate = DateTime.Now.AddDays(engineerRepository.CountAll() * -1);
            var startDate = date.AddDays(engineerRepository.CountAll() * -1);

            var availableEngineers = engineerRepository.GetAvailableEngineersSince(startDate, shiftsPerDay);
            var listOfSelectedEngineers = new List<Engineer>();

            for (int i = 1; i <= shiftsPerDay; i++) {
                
                var randomIndex = new Random(DateTime.Now.Millisecond).Next(0, availableEngineers.Count());
                var randomEngineer = availableEngineers[randomIndex];
                availableEngineers.RemoveAt(randomIndex);

                randomEngineer.AddShift(date, i);

                engineerRepository.Update(randomEngineer);
                listOfSelectedEngineers.Add(randomEngineer);
            }

            return listOfSelectedEngineers;
        }

        private List<Engineer> GetEngineersWorkingToday(DateTime date) {
            return engineerRepository.GetByShiftDate(date).ToList();
        }
    }
}
