using System;
using System.Collections.Generic;
using RgSupportWofApi.Application.Helpers;
using RgSupportWofApi.Application.Model;

namespace RgSupportWofApi.UnitTests.Fixtures
{
    public class ShiftFixture
    {

        public ShiftFixture()
        {
         
        }

        public List<Shift> GetEmptyListOfShifts() {
            return new List<Shift>();
        }

        public List<Shift> GetShiftsUntilYesterdayExceptFirstDayOfPeriod() {
            
            var engineers = new EngineerFixture().GetNewListOfEnginers();

            var shifts = new List<Shift>();

            //shifts.Add(new Shift { Date = DateFixture.FourDaysAgo, ShiftOrder = 1, Engineer = engineers[2] });
            //shifts.Add(new Shift { Date = DateFixture.FourDaysAgo, ShiftOrder = 2, Engineer = engineers[1] });

            shifts.Add(new Shift { Date = DateFixture.ThreeDaysAgo, ShiftOrder = 2, Engineer = engineers[3] });
            shifts.Add(new Shift { Date = DateFixture.ThreeDaysAgo, ShiftOrder = 1, Engineer = engineers[0] });

            shifts.Add(new Shift { Date = DateFixture.TwoDaysAgo, ShiftOrder = 2, Engineer = engineers[1] });
            shifts.Add(new Shift { Date = DateFixture.TwoDaysAgo, ShiftOrder = 1, Engineer = engineers[2] });

            shifts.Add(new Shift { Date = DateFixture.Yesterday, ShiftOrder = 1, Engineer = engineers[0] });
            shifts.Add(new Shift { Date = DateFixture.Yesterday, ShiftOrder = 2, Engineer = engineers[3] });

            return shifts;
        }

    }
}
