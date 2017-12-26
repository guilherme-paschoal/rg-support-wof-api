using System;
using Moq;
using RgSupportWofApi.Application.Data;
using RgSupportWofApi.Application.Data.Repositories;
using RgSupportWofApi.Application.Services;
using RgSupportWofApi.UnitTests.Fixtures;
using Xunit;

namespace RgSupportWofApi.UnitTests.Services
{
    public class ShiftServiceTest : IClassFixture<ShiftFixture>
    {
        readonly ShiftFixture fixture;
        readonly Mock<DatabaseContext> mockDbContext;
        readonly Mock<ShiftRepository> shiftRepository;

        public ShiftServiceTest(ShiftFixture fixture)
        {
            this.fixture = fixture;
            mockDbContext = new Mock<DatabaseContext>();
            shiftRepository = new Mock<ShiftRepository>(mockDbContext.Object);
        }

        [Fact]
        public void ShouldGetTodayShifts()
        {
            var shifts = fixture.GetShiftsForToday();
            shiftRepository.Setup(x => x.GetShiftsSince(It.Is<DateTime>(d => DateTime.Compare(d.Date, DateTime.Now.Date) == 0))).Returns(shifts);
            var todayShifts = new ShiftService(shiftRepository.Object).GetTodaysShifts();
            Assert.Equal(todayShifts, shifts);
        }
    }
}
