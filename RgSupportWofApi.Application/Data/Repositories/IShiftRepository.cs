using System;
using System.Collections.Generic;
using RgSupportWofApi.Application.Model;

namespace RgSupportWofApi.Application.Data.Repositories
{
    public interface IShiftRepository
    {
        IList<Shift> Filter(DateTime date, int engineerId);
        IList<Shift> GetShiftsSince(DateTime date);
        Shift Add(Shift shift);
        void Save();
    }
}
