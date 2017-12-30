using Microsoft.AspNetCore.Mvc;
using RgSupportWofApi.Application.Data.Repositories;

namespace RgSupportWofApi.Application.Controllers
{
    // This controller is not in use yet. It will be used in the next push
    public class EngineersController : Controller
    {
        readonly IEngineerRepository engineerRepository;
        public EngineersController(IEngineerRepository engineerRepository)
        {
            this.engineerRepository = engineerRepository;
        }

        [HttpGet]
        public IActionResult Index() {
            return new JsonResult(engineerRepository.GetAll());
        }
    }
}
