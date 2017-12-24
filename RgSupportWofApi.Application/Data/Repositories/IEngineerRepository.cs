using System;
using System.Collections.Generic;
using RgSupportWofApi.Application.Model;

namespace RgSupportWofApi.Application.Data.Repositories
{
    public interface IEngineerRepository
    {
        //void Update(Engineer model);
        IList<Engineer> GetAll();
        int CountAll();
        //IList<Engineer> GetAvailableEngineersSince(DateTime sinceDate, int shiftsPerDay);
        //IList<Engineer> GetByShiftDate(DateTime date);
    }
}
