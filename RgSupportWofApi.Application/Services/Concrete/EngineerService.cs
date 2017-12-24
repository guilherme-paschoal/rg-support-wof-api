using System.Collections.Generic;
using System.Linq;
using RgSupportWofApi.Application.Data.Repositories;
using RgSupportWofApi.Application.Helpers;
using RgSupportWofApi.Application.Model;

namespace RgSupportWofApi.Application.Services
{
    public class EngineerService : IEngineerService
    {
        readonly IEngineerRepository engineerRepository;

        public EngineerService(IEngineerRepository engineerRepository) 
        {
            this.engineerRepository = engineerRepository;
        }

        public int GetEngineerCount() 
        {
            return engineerRepository.CountAll();
        }

        public IList<Engineer> GetAvailableEngineers(IList<Shift> shifts, int shiftsPerDay) 
        {
            var control = true;
            var removeEngineers = new List<int>();

            // calculate engineer ids that AREN'T available
            foreach(Shift s in shifts)
            {
                control &= (s.Date.Date > DateTimeUtils.Yesterday || shifts.Count(x => x.Engineer.Id == s.Engineer.Id) >= shiftsPerDay);
                if(control) { removeEngineers.Add(s.Engineer.Id); }
            }

            return engineerRepository.GetAll().Where(x => !removeEngineers.Contains(x.Id)).ToList();
        }
    }
}
