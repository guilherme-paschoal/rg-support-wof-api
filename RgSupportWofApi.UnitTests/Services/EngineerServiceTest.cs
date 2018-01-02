using Moq;
using RgSupportWofApi.Application.Data;
using RgSupportWofApi.Application.Data.Repositories;
using RgSupportWofApi.UnitTests.Fixtures;
using Xunit;
using System.Linq;
using RgSupportWofApi.Application.Services;

namespace RgSupportWofApi.UnitTests.Services
{
    public class EngineerServiceTest
    {
        readonly Mock<DatabaseContext> mockDbContext;
        readonly Mock<EngineerRepository> engineerRepository;

        public EngineerServiceTest()
        {
            mockDbContext = new Mock<DatabaseContext>();
            engineerRepository = new Mock<EngineerRepository>(mockDbContext.Object);
            engineerRepository.Setup(x => x.CountAll()).Returns(4);
            engineerRepository.Setup(x => x.GetAll()).Returns(new EngineerFixture().GetNewListOfEnginers());
        }

        [Fact]
        public void ShouldGetEngineerCount() 
        {
            var service = new EngineerService(engineerRepository.Object);
            var count = service.GetEngineerCount();
            Assert.Equal(4, count);
        }

        [Fact]
        public void ShouldGetAvailableEngineersFromListOfShifts() 
        {
            var service = new EngineerService(engineerRepository.Object);
            var shifts = new ShiftFixture().GetAllShifts();

            shifts.RemoveAt(0);
            shifts.RemoveAt(1);

            var availableEngineers = service.GetAvailableEngineers(shifts, 2);

            Assert.Equal(2, availableEngineers.Count());
        }
    }
}
