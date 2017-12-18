using System;
using System.Collections.Generic;
using Moq;
using RgSupportWofApi.Application.Data.Repositories;
using RgSupportWofApi.Application.Helpers;
using RgSupportWofApi.Application.Model;
using RgSupportWofApi.Application.Services;
using Xunit;

namespace RgSupportWofApi.UnitTests.Services
{
    public class WheelOfFateServiceTest
    {
        Mock<EngineerRepository> engineerRepository;
        Mock<ShiftRepository> shiftRepository;

        List<Engineer> mockEngineers;
        List<Shift> mockShifts;

        DateTime today;

        public WheelOfFateServiceTest() {
            engineerRepository = new Mock<EngineerRepository>();
            shiftRepository = new Mock<ShiftRepository>();
            today = DateTime.Now.ResetTime();
        }

        List<Engineer> InitializeListOfEngineers(List<Engineer> list) {
            list = new List<Engineer> {
                new Engineer { Id = 1, Name = "John Doe" },
                new Engineer { Id = 2, Name = "Mary Doe" }
            };
            return list;
        }

        List<Shift> InitializeListOfShifts(List<Shift> list)
        {
            list = new List<Shift> {
                new Shift {Id = 1, Date = DateTime.Now.Date, ShiftOrder = 1, Engineer = new Engineer { Id = 1, Name = "John Doe" }},
                new Shift {Id = 2, Date = DateTime.Now.Date, ShiftOrder = 2, Engineer = new Engineer { Id = 2, Name = "Mary Doe" }} 
            };
            return list;
        }

        [Fact]
        public void ShouldReturnEngineersWorkingTodayIfTheWheelHasAlreadyBeenSpun() {
           
            mockShifts = InitializeListOfShifts(mockShifts);
            shiftRepository.Setup(x => x.GetByDate(today)).Returns(mockShifts);

            var engineers = new WheelOfFateService(engineerRepository.Object, shiftRepository.Object).SpinTheWheel();

            Assert.Equal(2, engineers.Count);
        }

        [Fact]
        public void ShouldGetListOfAvailableEngineers() {

            //var engineer1 = new Engineer() { Name = "John Doe" };
            //var engineer2 = new Engineer() { Name = "Jack Fast" };

            //var shift1 = new Shift() { Date = new DateTime(2017, 10, 10), ShiftOrder = 1, Engineer = engineer1 };
            //var shift2 = new Shift() { Date = new DateTime(2017, 10, 10), ShiftOrder = 2, Engineer = engineer2 };
            //var shift3 = new Shift() { Date = new DateTime(2017, 10, 11), ShiftOrder = 1 };
            //var shift4 = new Shift() { Date = new DateTime(2017, 10, 11), ShiftOrder = 2 };

            //Mock<EngineerRepository> engineerRepository = new Mock<EngineerRepository>();
            //Mock<ShiftRepository> shiftRepository = new Mock<ShiftRepository>();

            ////engineerRepository.Setup();

            //var subject = new WheelOfFateService(engineerRepository.Object, shiftRepository.Object);

            //subject.GetAvailableEngineers();

        }
    }
}
