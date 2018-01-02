using System;
using Microsoft.AspNetCore.Mvc;
using RgSupportWofApi.Application.Data.Repositories;
using RgSupportWofApi.Application.Services;

namespace RgSupportWofApi.Application.Controllers
{
    public class ShiftsController : Controller
    {
        IShiftService shiftService;
        IShiftRepository shiftRepository;

        public ShiftsController(IShiftService shiftService, IShiftRepository shiftRepository)
        {
            this.shiftService = shiftService;
            this.shiftRepository = shiftRepository;
        }

        [HttpGet]
        public IActionResult Since(DateTime date) 
        {
            return new JsonResult(shiftService.GetShiftsSinceDate(date));
        }

        public IActionResult Filter(DateTime date, int engineerId) {
                return new JsonResult(shiftRepository.Filter(date, engineerId));
        }
    }
}
