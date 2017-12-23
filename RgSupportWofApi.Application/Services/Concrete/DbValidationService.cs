using System;
using RgSupportWofApi.Application.Data.Repositories;
using RgSupportWofApi.Application.Services.Exceptions;

namespace RgSupportWofApi.Application.Services
{
    public class DbValidationService : IDbValidationService
    {
        readonly IEngineerRepository engineerRepository;
        readonly int shiftsPerDay;

        public DbValidationService(IEngineerRepository engineerRepository, int shiftsPerDay)
        {
            this.engineerRepository = engineerRepository;
            this.shiftsPerDay = shiftsPerDay;
        }

        public bool ValidateDatabase()
        {
            var engineerCount = engineerRepository.CountAll();

            // These validations make sure that exceptions are thrown for situations wher the application isn't ready to work on

            // Business rule: Can't have a person work 2 consecutive days, for that reason you need to have at least 2 engineers
            if (engineerCount < 2) throw new ServiceValidationException("Please make sure you have at least two engineers");

            // All the shift slots on a day must be fulfilled, if you have more shits than engineers, you're going to end up with empty slots
            if (shiftsPerDay > engineerCount) throw new ServiceValidationException("You can't have more shifts per day than engineers"); 

            // Limitation: The amount of engineers MOD shifts per day must be 0 so we can have an even distribution of engineers per day across the period
            // In a production application the whole logic would be a bit more complex in order to solve this problem but I don't think this is this test's objective
            if (engineerCount % shiftsPerDay > 0) throw new ServiceValidationException("Please make sure the amount of engineers in the database divided by shifts per day (configuration) has 0 rest");

            return true;
        }
    }
}
