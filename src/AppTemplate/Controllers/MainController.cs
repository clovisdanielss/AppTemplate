using AppTemplate.Shared.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AppTemplate.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class MainController : ControllerBase
    {
        protected readonly ILogger<TestController> _logger;
        protected readonly IMapper _mapper;
        protected readonly INotifier _notifier;

        public MainController(ILogger<TestController> logger, IMapper mapper, INotifier notifier)
        {
            _logger = logger;
            _mapper = mapper;
            _notifier = notifier;
        }

        public bool HasAnyError => _notifier.GetNotifications().Count > 0;

    }
}