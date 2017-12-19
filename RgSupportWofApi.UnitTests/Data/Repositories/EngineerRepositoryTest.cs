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
using RgSupportWofApi.UnitTests.Fixtures;

namespace RgSupportWofApi.UnitTests.Data.Repositories
{
    public class EngineerRepositoryTest : IClassFixture<EngineerFixture>
    {
        EngineerFixture fixture;
        Mock<DatabaseContext> mockDbContext;
        EngineerRepository engineerRepository;
  
        public EngineerRepositoryTest(EngineerFixture fixture) {
            this.fixture = fixture;
            mockDbContext = new Mock<DatabaseContext>();
            engineerRepository = new EngineerRepository(mockDbContext.Object);
        }

        // For development speed reasons, I wont write tests for simple/wrapper methods like Add/Update/GetAll methods
        // In a real world situation I would, indeed test those methods too even if they are really simple

        private List<Engineer> ActToGetAvailableEngineers(DateTime date, List<Engineer> fixtureData, int shiftsPerDay = 2) {
            
            mockDbContext.Setup(x => x.Engineers).ReturnsDbSet(fixtureData);
            return engineerRepository.GetAvailableEngineersSince(date, shiftsPerDay).ToList();

        }

        [Fact]
        public void ShouldGetAvailableEngineersWhenThereAreNoShifts()
        {
            Assert.Equal(4, ActToGetAvailableEngineers(
                DateTime.Now.ResetTime(), 
                fixture.EngineersWithoutShifts
            ).Count);
        }

        [Fact]
        public void ShouldGetAvailableEngineersWhenThereAreShifts()
        {
            Assert.Equal(2, ActToGetAvailableEngineers(
               DateTime.Now.ResetTime(),
               fixture.EngineersWithShifts
           ).Count);
        }

        [Fact]
        public void ShouldNotReturnEngineersThatHaveWorkedAFullDayInSearchPeriod()
        {
            var engineers = ActToGetAvailableEngineers(
               DateTime.Now.ResetTime(),
               fixture.EngineersWithShifts
            );
            Assert.Equal(2,engineers.Count);
        }

        [Fact]
        public void ShouldNotReturnEngineersThatAreAssignedForCurrentDayOrThatWorkedOnThePreviousDay()
        {
            var engineers = ActToGetAvailableEngineers(
               DateTime.Now.ResetTime(),
                fixture.EngineersWorkingYesterdayAndToday
            );
            Assert.Equal(2, engineers.Count);
            Assert.Equal(3, engineers[0].Id);
            Assert.Equal(4, engineers[1].Id);
        }

        [Fact]
        public void ShouldReturnNothingIfShiftsPerDayIs0()
        {
            var engineers = ActToGetAvailableEngineers(
               DateTime.Now.ResetTime(),
               fixture.EngineersWithShifts,0);
            Assert.Empty(engineers);
        }
    }
}
