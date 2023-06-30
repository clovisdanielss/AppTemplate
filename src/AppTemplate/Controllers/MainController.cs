using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AppTemplate.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class MainController : ControllerBase
    {
        protected readonly ILogger<TestController> _logger;
        protected readonly IMapper _mapper;

        public MainController(ILogger<TestController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }
    }
}