using System;
namespace RgSupportWofApi.Application.Helpers
{
    public static class DateTimeUtils
    {
      
        public static DateTime Yesterday
        {
            get 
            {
                return DateTime.Now.SubtractDays(1).ResetTime();
            }
        }

        public static DateTime Today 
        {
            get
            {
                return DateTime.Now.ResetTime();    
            }
        }
    }
}
