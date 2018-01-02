using System;
using Microsoft.AspNetCore.Mvc;
using RgSupportWofApi.Application.Services;

namespace RgSupportWofApi.Application.Controllers
{
    public class WheelOfFateController
    {
        readonly IWheelOfFateService wheelOfFateService;
        readonly IDbValidationService dbValidationService;

        public WheelOfFateController(IWheelOfFateService wheelOfFateService, IDbValidationService dbValidationService)
        {
            this.wheelOfFateService = wheelOfFateService;
            this.dbValidationService = dbValidationService;
        }

        [HttpGet]
        public IActionResult Get() 
        {
            dbValidationService.ValidateDatabase();
            return new JsonResult(wheelOfFateService.SpinTheWheel());
        }
    }
}
