using Xunit;
using Moq;
using RgSupportWofApi.Application.Data;
using RgSupportWofApi.UnitTests.TestHelpers;
using RgSupportWofApi.Application.Data.Repositories;
using System.Linq;
using RgSupportWofApi.UnitTests.Fixtures;

namespace RgSupportWofApi.UnitTests.Data.Repositories
{
    public class EngineerRepositoryTest : IClassFixture<EngineerFixture>
    {
        EngineerFixture fixture;
        readonly Mock<DatabaseContext> mockDbContext;

        public EngineerRepositoryTest(EngineerFixture fixture) {
            this.fixture = fixture;
            mockDbContext = new Mock<DatabaseContext>();
            mockDbContext.Setup(x => x.Engineers).ReturnsDbSet(fixture.GetNewListOfEnginers());
        }

        [Fact]
        public void ShouldReturnAllEngineers() 
        {
            var repository = new EngineerRepository(mockDbContext.Object);
            var engineers = repository.GetAll();
            Assert.Equal(4, engineers.Count());
        }

        [Fact]
        public void ShouldReturnCountAllEngineers() 
        {
            var repository = new EngineerRepository(mockDbContext.Object);
            var count = repository.CountAll();
            Assert.Equal(4, count);
        }

        //private List<Engineer> ActToGetAvailableEngineers(DateTime date, List<Engineer> fixtureData, int shiftsPerDay = 2) {
            
        //    mockDbContext.Setup(x => x.Engineers).ReturnsDbSet(fixtureData);



        //    mockDbContext.Setup(x => x.Shifts).ReturnsDbSet(fixture.EngineersWithShifts);

        //    return engineerRepository.GetAvailableEngineersSince(date, shiftsPerDay).ToList();

        //}

        //[Fact]
        //public void ShouldGetAvailableEngineersWhenThereAreNoShifts()
        //{
        //    Assert.Equal(4, ActToGetAvailableEngineers(
        //        DateTime.Now.AddDays(-4).ResetTime(), 
        //        fixture.EngineersWithoutShifts
        //    ).Count);
        //}

        //[Fact]
        //public void ShouldGetAvailableEngineersWhenThereAreShifts()
        //{
        //    Assert.Equal(2, ActToGetAvailableEngineers(
        //       DateTime.Now.ResetTime(),
        //       fixture.EngineersWithShifts
        //   ).Count);
        //}

        //[Fact]
        //public void ShouldNotReturnEngineersThatHaveWorkedAFullDayInSearchPeriod()
        //{
        //    var engineers = ActToGetAvailableEngineers(
        //       DateTime.Now.ResetTime(),
        //       fixture.EngineersWithShifts
        //    );
        //    Assert.Equal(2,engineers.Count);
        //}

        //[Fact]
        //public void ShouldNotReturnEngineersThatAreAssignedForCurrentDayOrThatWorkedOnThePreviousDay()
        //{
        //    var engineers = ActToGetAvailableEngineers(
        //       DateTime.Now.ResetTime(),
        //        fixture.EngineersWorkingYesterdayAndToday
        //    );
        //    Assert.Equal(2, engineers.Count);
        //    Assert.Equal(3, engineers[0].Id);
        //    Assert.Equal(4, engineers[1].Id);
        //}

        //[Fact]
        //public void ShouldReturnNothingIfShiftsPerDayIs0()
        //{
        //    var engineers = ActToGetAvailableEngineers(
        //       DateTime.Now.ResetTime(),
        //       fixture.EngineersWithShifts,0);
        //    Assert.Empty(engineers);
        //}
    }
}
