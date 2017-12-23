using System;
using System.Collections.Generic;
using RgSupportWofApi.Application.Model;

namespace RgSupportWofApi.Application.Data.Repositories
{
    public interface IShiftRepository
    {
        IList<Shift> GetShiftsSince(DateTime date);
        void Add(Shift shift);
        void Save();
    }
}
