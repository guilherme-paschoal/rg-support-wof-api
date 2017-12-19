using System;
using RgSupportWofApi.Application.Helpers;
using RgSupportWofApi.Application.Model;
using Xunit;

namespace RgSupportWofApi.UnitTests.Model
{
    public class EngineerTest
    {

        [Fact]
        public void ShoulAddShiftWithCurrentDateIfNoDateIsPassed() {
            var engineer = new Engineer();
            engineer.AddShift(1);
            var date = engineer.Shifts[0].Date;
            Assert.Equal(0, date.CompareTo(DateTime.Now.ResetTime()));
        }

        [Fact]
        public void ShoulAddShiftWithPassedDate()
        {
            var engineer = new Engineer();
            var dateToAdd = new DateTime(2017, 10, 10, 0, 0, 0);
            engineer.AddShift(dateToAdd, 1);
            var dateToCompare = engineer.Shifts[0].Date;
            Assert.Equal(0, dateToCompare.CompareTo(dateToAdd));
        }
    }
}
