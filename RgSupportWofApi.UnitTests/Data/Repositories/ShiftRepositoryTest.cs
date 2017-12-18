using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using RgSupportWofApi.Application.Data;
using RgSupportWofApi.Application.Data.Repositories;
using RgSupportWofApi.Application.Model;
using RgSupportWofApi.UnitTests.TestHelpers;
using Xunit;

namespace RgSupportWofApi.UnitTests.Data.Repositories
{
    public class ShiftRepositoryTest
    {
        [Fact]
        public void ShouldReturnShiftsByDate() {
            
            // Although this test is testing the basic logic behind the repository, it is fundamental that we have integration tests
            // To cover situations where queryiing a database behaves differently from querying in-memory objects, like, nested collections
            // That will be loaded immediately when in memory but will depend on the style of loading chosen when obtained from the DB.

            var data = new List<Shift> {
                new Shift { Date = new DateTime(2017,10,10), ShiftOrder = 1, Id = 123 },
                new Shift { Date = new DateTime(2017,10,10), ShiftOrder = 2, Id = 456 },
                new Shift { Date = new DateTime(2017,10,20), ShiftOrder = 1, Id = 789 }
            };

            var mockDbContext = new Mock<DatabaseContext>();
            mockDbContext.Setup(x => x.Shifts).ReturnsDbSet(data);

            var shiftRepository = new ShiftRepository(mockDbContext.Object);
            var shifts = shiftRepository.GetByDate(new DateTime(2017, 10, 10));

            Assert.Equal(2, shifts.Count());
        }

    }
}
