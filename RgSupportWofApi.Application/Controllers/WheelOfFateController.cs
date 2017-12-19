using System;
using Microsoft.AspNetCore.Mvc;
using RgSupportWofApi.Application.Services;

namespace RgSupportWofApi.Application.Controllers
{
    public class WheelOfFateController
    {
        readonly IWheelOfFateService wheelOfFateService;

        public WheelOfFateController(IWheelOfFateService wheelOfFateService)
        {
            this.wheelOfFateService = wheelOfFateService;
        }

        [HttpGet]
        public IActionResult Get() 
        {
            wheelOfFateService.ValidateDatabase();
            return new JsonResult(wheelOfFateService.SpinTheWheel());
        }
    }
}
