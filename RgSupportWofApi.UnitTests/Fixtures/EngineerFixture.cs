using System;
using System.Collections.Generic;
using RgSupportWofApi.Application.Helpers;
using RgSupportWofApi.Application.Model;

namespace RgSupportWofApi.UnitTests.Fixtures
{
    public class EngineerFixture : IDisposable
    {

        // The number of days ago is the same as the amount of engineers
        DateTime fourDaysAgo, threeDaysAgo, twoDaysAgo, yesterday, today;

        public List<Engineer> EngineersWithoutShifts { get; private set; }
        public List<Engineer> EngineersWithShifts { get; private set; }
        public List<Engineer> EngineersWorkingYesterdayAndToday { get; private set; }

        //List<Shift> Shifts { get; set; }

        public EngineerFixture()
        {
            InitializeDates();
            InitializeEngineersWithoutShifts();
            InitializeEngineersWithShifts();
            InitializeEngineersWorkingToday();
        }

        private void InitializeDates() {
            fourDaysAgo = DateTime.Now.ResetTime().AddDays(-4);
            threeDaysAgo = DateTime.Now.ResetTime().AddDays(-3);
            twoDaysAgo = DateTime.Now.ResetTime().AddDays(-2);
            yesterday = DateTime.Now.ResetTime().AddDays(-1);
            today = DateTime.Now.ResetTime();
        }

        public List<Engineer> GetNewListOfEnginers() {
            return new List<Engineer> {
                new Engineer { Id = 1, Name = "John Doe" },
                new Engineer { Id = 2, Name = "Mary Doe" },
                new Engineer { Id = 3, Name = "Joe Doe" },
                new Engineer { Id = 4, Name = "Mike Doe" }
            };
        }

        private void InitializeEngineersWithoutShifts() {
            EngineersWithoutShifts = GetNewListOfEnginers();
        }

        private void InitializeEngineersWithShifts()
        {
            EngineersWithShifts = GetNewListOfEnginers();

            EngineersWithShifts[2].AddShift(fourDaysAgo, 1);
            EngineersWithShifts[1].AddShift(fourDaysAgo, 2);

            EngineersWithShifts[3].AddShift(threeDaysAgo, 2);
            EngineersWithShifts[0].AddShift(threeDaysAgo, 1);

            EngineersWithShifts[1].AddShift(twoDaysAgo, 2);
            EngineersWithShifts[2].AddShift(twoDaysAgo, 1);

            EngineersWithShifts[0].AddShift(yesterday, 1);
            EngineersWithShifts[3].AddShift(yesterday, 2);
        }


        private void InitializeEngineersWorkingToday()
        {
            EngineersWorkingYesterdayAndToday = GetNewListOfEnginers();
            EngineersWorkingYesterdayAndToday[0].AddShift(yesterday, 1);
            EngineersWorkingYesterdayAndToday[1].AddShift(today, 1);
        }

        public void Dispose()
        {
        }
    }
}
