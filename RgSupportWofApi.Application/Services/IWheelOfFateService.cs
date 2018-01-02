using System;
using System.Collections.Generic;
using RgSupportWofApi.Application.Model;

namespace RgSupportWofApi.Application.Services
{
    public interface IWheelOfFateService
    {
        IList<Shift> SpinTheWheel();
    }
}
