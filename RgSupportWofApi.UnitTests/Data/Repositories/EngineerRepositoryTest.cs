using Xunit;
using Moq;
using RgSupportWofApi.Application.Model;
using RgSupportWofApi.Application.Data;
using System.Collections.Generic;
using System;
using RgSupportWofApi.UnitTests.TestHelpers;
using RgSupportWofApi.Application.Data.Repositories;
using RgSupportWofApi.Application.Helpers;
using System.Linq;

namespace RgSupportWofApi.UnitTests.Data.Repositories
{
    public class EngineerRepositoryTest
    {
        List<Engineer> mockEngineersNoShifts;
        List<Engineer> mockEngineersWithShifts;

        DateTime fiveDaysAgo;
        DateTime fourDaysAgo;
        DateTime threeDaysAgo;
        DateTime twoDaysAgo;
        DateTime yesterday;
        DateTime today;

        public EngineerRepositoryTest() {
           
            fiveDaysAgo = DateTime.Now.ResetTime().AddDays(-5);
            fourDaysAgo = DateTime.Now.ResetTime().AddDays(-4);
            threeDaysAgo = DateTime.Now.ResetTime().AddDays(-3);
            twoDaysAgo = DateTime.Now.ResetTime().AddDays(-2);
            yesterday = DateTime.Now.ResetTime().AddDays(-1);
            today = DateTime.Now.ResetTime();

        }

        private List<Engineer> InitializeListOfEngineers(List<Engineer> list) {
            list = new List<Engineer> {
                new Engineer { Id = 1, Name = "John Doe" },
                new Engineer { Id = 2, Name = "Mary Doe" },
                new Engineer { Id = 3, Name = "Joe Doe" },
                new Engineer { Id = 4, Name = "Mike Doe" },
                new Engineer { Id = 5, Name = "Clara Doe" },
                new Engineer { Id = 6, Name = "Play Doe" },
                new Engineer { Id = 7, Name = "More Doe" }
            };
            return list;
        }
        
        // For development speed reasons, I wont write tests for simple/wrapper methods like Add/Update/GetAll methods
        // In a real world situation I would, indeed test those methods too even if they are really simple

        // Although this test is testing the basic logic behind the repository, it is fundamental that we have integration tests
        // To cover situations where querying a database behaves differently from querying in-memory objects, like, nested collections
        // That will be loaded immediately when in memory but will depend on the style of loading chosen when obtained from the DB.

        [Fact]
        public void ShouldGetAvailableEngineersWhenThereAreNoShifts()
        {
            mockEngineersNoShifts = InitializeListOfEngineers(mockEngineersNoShifts);

            var testDate = DateTime.Now.ResetTime();
            var mockDbContext = new Mock<DatabaseContext>();
            mockDbContext.Setup(x => x.Engineers).ReturnsDbSet(mockEngineersNoShifts);
            var engineerRepository = new EngineerRepository(mockDbContext.Object);

            var engineers = engineerRepository.GetAvailableEngineersSince(testDate, 2);
            Assert.Equal(7, engineers.Count);
        }

        [Fact]
        public void ShouldNotReturnEngineersThatHaveWorkedAFullDaySinceBeginingOfPeriod()
        {
            mockEngineersWithShifts = InitializeListOfEngineers(mockEngineersWithShifts);

            mockEngineersWithShifts[0].AddShift(fiveDaysAgo, 1);
            mockEngineersWithShifts[1].AddShift(fiveDaysAgo, 2);

            mockEngineersWithShifts[0].AddShift(fourDaysAgo, 2);
            mockEngineersWithShifts[1].AddShift(fourDaysAgo, 1);

            mockEngineersWithShifts[6].AddShift(threeDaysAgo, 2); // engineer 7
            mockEngineersWithShifts[3].AddShift(threeDaysAgo, 1);

            mockEngineersWithShifts[2].AddShift(twoDaysAgo, 1);
            mockEngineersWithShifts[3].AddShift(twoDaysAgo, 2);

            mockEngineersWithShifts[4].AddShift(yesterday, 1);
            mockEngineersWithShifts[5].AddShift(yesterday, 2);

            mockEngineersWithShifts[5].AddShift(today, 2);

            var testDate = DateTime.Now.ResetTime().AddDays(-5);
            var mockDbContext = new Mock<DatabaseContext>();
            mockDbContext.Setup(x => x.Engineers).ReturnsDbSet(mockEngineersWithShifts);
            var engineerRepository = new EngineerRepository(mockDbContext.Object);

            var engineers = engineerRepository.GetAvailableEngineersSince(testDate, 2);
            Assert.Equal(2, engineers.Count);

            // its going to be engineer id 3 and 7 because they are not assigned to work today, didnt work yesterday and havent't completed a 
            // full day since the begining
            Assert.Contains(engineers, e => e.Id == 3);
            Assert.Contains(engineers, e => e.Id == 7);
        }

        [Fact]
        public void ShouldNotReturnEngineersThatAreAssignedForCurrentDayOrThatWorkedOnThePreviousDay()
        {
            mockEngineersWithShifts = InitializeListOfEngineers(mockEngineersWithShifts);

            mockEngineersWithShifts[0].AddShift(yesterday, 1);
            mockEngineersWithShifts[1].AddShift(yesterday, 2);
            mockEngineersWithShifts[2].AddShift(today, 1);

            var testDate = DateTime.Now;
            var mockDbContext = new Mock<DatabaseContext>();
            mockDbContext.Setup(x => x.Engineers).ReturnsDbSet(mockEngineersWithShifts);
            var engineerRepository = new EngineerRepository(mockDbContext.Object);

            var engineers = engineerRepository.GetAvailableEngineersSince(testDate, 2);
            Assert.Equal(4, engineers.Count);

            Assert.Contains(engineers, e => e.Id == 4);
            Assert.Contains(engineers, e => e.Id == 5);
            Assert.Contains(engineers, e => e.Id == 6);
            Assert.Contains(engineers, e => e.Id == 7);
        }
    }
}
