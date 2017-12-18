using System;
namespace RgSupportWofApi.Application.Helpers
{
    public static class DateTimeExtensions
    {
        public static DateTime ResetTime(this DateTime dateTime){
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0);
        }
    }
}
