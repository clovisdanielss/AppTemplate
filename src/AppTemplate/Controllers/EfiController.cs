using AppTemplate.Payment.Subscriptions.Interfaces;
using AppTemplate.Payment.Subscriptions.Models;
using AppTemplate.Shared.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AppTemplate.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EfiController : MainController
    {
        public EfiController(ILogger<TestController> logger, IMapper mapper, INotifier notifier) : base(logger, mapper, notifier)
        {
        }

        [HttpPost]
        public IActionResult PostPlan(Plan p, [FromServices] IPlanClientService service)
        {
            var test = service.Create(p);
            return CustomResponse(test);
        }
        [HttpGet]
        public IActionResult GetAllPlans([FromServices] IPlanClientService service) {
            var test = service.GetAll();
            return CustomResponse(test);
        }

        [HttpGet("{id}")]
        public IActionResult GetPlan(string id, [FromServices] IPlanClientService service)
        {
            var test = service.Get(id);
            return CustomResponse(test);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePlan([FromRoute] string id, [FromServices] IPlanClientService service)
        {
            service.Delete(id);
            return CustomResponse();
        }

    }
}
