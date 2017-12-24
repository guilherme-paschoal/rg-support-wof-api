using System;
using System.Collections.Generic;
using RgSupportWofApi.Application.Model;

namespace RgSupportWofApi.Application.Services
{
    public interface IEngineerService
    {
        int GetEngineerCount();

        IList<Engineer> GetAvailableEngineers(IList<Shift> shifts, int shiftsPerDay);
    }
}