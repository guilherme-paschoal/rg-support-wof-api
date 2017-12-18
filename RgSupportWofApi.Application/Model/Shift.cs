using System;
using System.ComponentModel.DataAnnotations;
using RgSupportWofApi.Application.Helpers;

namespace RgSupportWofApi.Application.Model
{
    public class Shift
    {
        [Key]
        public int Id { get; set; }
        public Engineer Engineer { get; set; }
        DateTime date;

        public DateTime Date
        {
            get { return date; }

            set
            {
                date = value.ResetTime();  // I don't like adding this dependency to the extension method but it's best for performance and EF Core for SQlte doesn't have a DbFunction to filter by the date part
            }
        }

        public int ShiftOrder { get; set; }
    }
}
