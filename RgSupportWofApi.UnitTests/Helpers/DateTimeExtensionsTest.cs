using System;
using RgSupportWofApi.Application.Helpers;
using Xunit;
namespace RgSupportWofApi.UnitTests.Helpers
{
    public class DateTimeExtensionsTest
    {
        [Fact]
        public void ShouldReturnSameDateWith0Time() {
            var date = new DateTime(2017, 10, 10, 12, 25, 30);

            var resultingdate = date.ResetTime();

            Assert.Equal(0, resultingdate.Hour);
            Assert.Equal(0, resultingdate.Minute);
            Assert.Equal(0, resultingdate.Second);
        }
    }
}
