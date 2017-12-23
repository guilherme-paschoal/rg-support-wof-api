using System.Collections.Generic;
using RgSupportWofApi.Application.Model;

namespace RgSupportWofApi.Application.Services
{
    public interface IShiftService
    {
        IList<Shift> GetTodaysShifts();
        IList<Shift> GetShiftsInCurrentPeriod();
        void InsertShift(Shift shift);
        void SaveShifts();
    }
}