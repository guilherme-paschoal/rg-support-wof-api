using System;
using Moq;
using RgSupportWofApi.Application.Data;
using RgSupportWofApi.Application.Data.Repositories;
using RgSupportWofApi.UnitTests.Fixtures;
using Xunit;
using System.Linq;
using RgSupportWofApi.Application.Services;
using RgSupportWofApi.Application.Model;
using System.Collections.Generic;
using RgSupportWofApi.Application.Helpers;

namespace RgSupportWofApi.UnitTests.Services
{
    public class EngineerServiceTest
    {
        readonly Mock<DatabaseContext> mockDbContext;
        readonly Mock<ShiftRepository> shiftRepository;
        readonly Mock<EngineerRepository> engineerRepository;

        public EngineerServiceTest()
        {
            mockDbContext = new Mock<DatabaseContext>();
            shiftRepository = new Mock<ShiftRepository>(mockDbContext.Object);
            engineerRepository = new Mock<EngineerRepository>(mockDbContext.Object, shiftRepository.Object);
            engineerRepository.Setup(x => x.CountAll()).Returns(4);
        }

        //[Fact]
        //public void ShouldGetAvailableEngineers()
        //{
        //    var engineers = new EngineerFixture().GetNewListOfEnginers();
        //    var shifts = new List<Shift>
        //    {
        //        new Shift { Date = DateFixture.ThreeDaysAgo, ShiftOrder = 2, Engineer = engineers[3] },
        //        new Shift { Date = DateFixture.ThreeDaysAgo, ShiftOrder = 1, Engineer = engineers[0] },
        //        new Shift { Date = DateFixture.TwoDaysAgo, ShiftOrder = 2, Engineer = engineers[1] },
        //        new Shift { Date = DateFixture.TwoDaysAgo, ShiftOrder = 1, Engineer = engineers[2] },
        //        new Shift { Date = DateFixture.Yesterday, ShiftOrder = 1, Engineer = engineers[0] },
        //        new Shift { Date = DateFixture.Yesterday, ShiftOrder = 2, Engineer = engineers[3] }
        //    };

        //    var startDateMustBe = DateTime.Now.SubtractDays(3);

        //    shiftRepository.Setup(x => x.GetShiftsSince(It.Is<DateTime>(y => y.Date == startDateMustBe))).Returns(shifts);
            
        //    var availables = new EngineerService(shiftRepository.Object, engineerRepository.Object).GetAvailableEngineers(DateFixture.ThreeDaysAgo, 2);

        //    Assert.Equal(2, engineers.Count());
        //    Assert.True(engineers.Any(x => x.Id == 2));
        //    Assert.True(engineers.Any(x => x.Id == 3));
        //}
    }
}
