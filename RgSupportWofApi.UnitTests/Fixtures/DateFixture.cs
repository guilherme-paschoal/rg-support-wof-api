using System;
using RgSupportWofApi.Application.Helpers;

namespace RgSupportWofApi.UnitTests.Fixtures
{
    public static class DateFixture
    {
        static DateTime Calculate(int days) 
        {
            return DateTime.Now.ResetTime().AddDays(days * -1);
        } 
        public static DateTime FourDaysAgo 
        {
            get => Calculate(4);
        }

        public static DateTime ThreeDaysAgo
        {
            get => Calculate(3);
        }

        public static DateTime TwoDaysAgo
        {
            get => Calculate(2);
        }

        public static DateTime Yesterday
        {
            get => Calculate(1);
        }
       
        public static DateTime Today
        {
            get => DateTime.Now.ResetTime();
        }
    }
}
