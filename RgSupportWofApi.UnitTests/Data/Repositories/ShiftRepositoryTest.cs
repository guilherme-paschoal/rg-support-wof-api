using System;
using Moq;
using RgSupportWofApi.Application.Data;
using RgSupportWofApi.Application.Data.Repositories;
using RgSupportWofApi.UnitTests.Fixtures;
using RgSupportWofApi.UnitTests.TestHelpers;
using Xunit;

namespace RgSupportWofApi.UnitTests.Data.Repositories
{
    public class ShiftRepositoryTest : IClassFixture<ShiftFixture>
    {
        ShiftFixture fixture;
        readonly Mock<DatabaseContext> mockDbContext;

        public ShiftRepositoryTest(ShiftFixture fixture)
        {
            this.fixture = fixture;
            mockDbContext = new Mock<DatabaseContext>();
            mockDbContext.Setup(x => x.Shifts).ReturnsDbSet(fixture.GetAllShifts());
        }

        [Fact]
        public void ShouldReturnShiftsSince()
        {
            var repository = new ShiftRepository(mockDbContext.Object);
            var shifts = repository.GetShiftsSince(DateFixture.ThreeDaysAgo);
            Assert.Equal(6, shifts.Count);
        }

        [Fact]
        public void ShouldNotReturnShiftsForDateInTheFuture()
        {
            var repository = new ShiftRepository(mockDbContext.Object);
            var shifts = repository.GetShiftsSince(DateTime.Now.AddDays(10));
            Assert.Equal(0, shifts.Count);
        }
    }
}
