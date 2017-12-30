using System;
using System.Collections.Generic;
using RgSupportWofApi.Application.Model;

namespace RgSupportWofApi.Application.Services
{
    public interface IShiftService
    {
        IList<Shift> GetTodaysShifts();
        IList<Shift> GetShiftsInCurrentPeriod(int daysInPeriod);
        IList<Shift> GetShiftsSinceDate(DateTime date);
        Shift InsertShift(Shift shift);
        void SaveShifts();
    }
}