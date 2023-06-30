using Microsoft.AspNetCore.Mvc;

namespace AppTemplate.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class MainController: ControllerBase
    {
        protected readonly ILogger<TestController> _logger;

        public MainController(ILogger<TestController> logger)
        {
            _logger = logger;
        }
    }
}