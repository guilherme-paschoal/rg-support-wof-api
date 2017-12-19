using System;
using System.Collections.Generic;
using Moq;
using RgSupportWofApi.Application.Data;
using RgSupportWofApi.Application.Data.Repositories;
using RgSupportWofApi.Application.Helpers;
using RgSupportWofApi.Application.Model;
using RgSupportWofApi.Application.Services;
using RgSupportWofApi.UnitTests.Fixtures;
using Xunit;

namespace RgSupportWofApi.UnitTests.Services
{
    public class WheelOfFateServiceTest : IClassFixture<EngineerFixture>
    {
        Mock<DatabaseContext> mockDbContext;
        Mock<EngineerRepository> engineerRepository;
        EngineerFixture fixture;

        public WheelOfFateServiceTest(EngineerFixture fixture) {
            mockDbContext = new Mock<DatabaseContext>();
            engineerRepository = new Mock<EngineerRepository>(mockDbContext.Object);
            this.fixture = fixture;
        }

        [Fact]
        public void ShouldReturnEngineersAlreadyWorkingToday() {
            var engineers = fixture.GetNewListOfEnginers();
            engineerRepository.Setup(x => x.GetByShiftDate(It.Is<DateTime>(d => d.CompareTo(DateTime.Now.ResetTime()) == 0))).Returns(engineers);
            var result = new WheelOfFateService(engineerRepository.Object, 2).SpinTheWheel();
            Assert.Equal(engineers, result);
        }

        [Fact]
        public void ShouldGetListOfEngineersThatWillWorkToday() {

            // it can be any list of engineers
            var engineers = fixture.GetNewListOfEnginers();

            // when the service checks if there are engineers with shifts today, it needs to return an empty string so the service can proceed
            engineerRepository.Setup(x => x.GetByShiftDate(It.Is<DateTime>(d => d.CompareTo(DateTime.Now.ResetTime()) == 0))).Returns(new List<Engineer>());

            // return whatever list of engineers
            engineerRepository.Setup(x => x.GetAvailableEngineersSince(It.IsAny<DateTime>(), It.IsAny<int>())).Returns(engineers);

            // the service should return a List of engineers with 2 (shifts per day) engineers selected randomly
            var result = new WheelOfFateService(engineerRepository.Object, 2).SpinTheWheel();

            // Check if there are really 2
            Assert.Equal(2, result.Count);
          
            foreach(var item in result) {
                // for each one returned check if the date is today
                Assert.Equal(0, DateTime.Now.ResetTime().CompareTo(item.Shifts[0].Date));
            }

        }
    }
}
