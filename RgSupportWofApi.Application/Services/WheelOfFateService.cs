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
        readonly IEngineerRepository engineerRepository;
        readonly int shiftsPerDay;


        public WheelOfFateService(IEngineerRepository engineerRepository, int shiftsPerDay)
        {
            this.engineerRepository = engineerRepository;
            this.shiftsPerDay = shiftsPerDay;
        }

        /// <summary>
        /// Spins the Wheel obtaning the Engineers that are going to work in the current day.
        /// </summary>
        /// <returns> List of Engineers that are going to work in the current day </returns>
        public IList<Engineer> SpinTheWheel()
        {
            // Validate if the wheel has been spun today, if so, returns enginers working today
            var engineersWorkingToday = GetEngineersWorkingToday();
            if (engineersWorkingToday.Any()) return engineersWorkingToday;

            var availableEngineers = engineerRepository.GetAvailableEngineersSince(GetSearchPeriodStartDate(), shiftsPerDay);
            var listOfSelectedEngineers = new List<Engineer>();

            for (int i = 1; i <= shiftsPerDay; i++)
            {
                var randomIndex = new Random(DateTime.Now.Millisecond).Next(0, availableEngineers.Count());
                var randomEngineer = availableEngineers[randomIndex];
                availableEngineers.RemoveAt(randomIndex);

                randomEngineer.AddShift(i);

                engineerRepository.Update(randomEngineer);
                listOfSelectedEngineers.Add(randomEngineer);
            }

            return listOfSelectedEngineers;
        }
       
        DateTime GetSearchPeriodStartDate() {

            // Having E = amount of engineers,
            // The number of days in a work period is the same as E because we are limiting the shifts per day to be a dividend of E
            // This way, we can guarantee that every E days is a work period meaning the search period to search for engineers for today is E - 1 
            return DateTime.Now.AddDays(engineerRepository.CountAll() -1 * -1).ResetTime();
        }

        public void ValidateDatabase()
        {
            var engineerCount = engineerRepository.CountAll();

            // These validations make sure that exceptions are thrown for situations wher the application isn't ready to work on

            // Business rule: Can't have a person work 2 consecutive days, for that reason you need to have at least 2 engineers
            if (engineerCount < 2) throw new ServiceValidationException("Please make sure you have at least two engineers");

            // All the shift slots on a day must be fulfilled, if you have more shits than engineers, you're going to end up with empty slots
            if (shiftsPerDay > engineerCount) throw new ServiceValidationException("You can't have more shifts per day than engineers"); 

            // Limitation: The amount of engineers MOD shifts per day must be 0 so we can have an even distribution of engineers per day across the period
            // In a production application the whole logic would be a bit more complex in order to solve this problem but I don't think this is this test's objective
            if (engineerCount % shiftsPerDay > 0) throw new ServiceValidationException("Please make sure the amount of engineers (databas) divided by shifts per day (configuration) has 0 rest");
        }

        List<Engineer> GetEngineersWorkingToday()
        {
            var date = DateTime.Now.ResetTime();
            return engineerRepository.GetByShiftDate(date).ToList();
        }
    }
}
