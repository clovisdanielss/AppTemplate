using AppTemplate.Shared.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AppTemplate.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        protected readonly ILogger<TestController> _logger;
        protected readonly IMapper _mapper;
        protected readonly INotifier _notifier;

        protected MainController(ILogger<TestController> logger, IMapper mapper, INotifier notifier)
        {
            _logger = logger;
            _mapper = mapper;
            _notifier = notifier;
        }

        public bool HasAnyError => _notifier.GetNotifications().Count > 0 || ModelState.Count > 0;

        protected ActionResult CustomResponse(object result = null, Func<object, ActionResult> success = null)
        {
            if (HasAnyError)
            {
                var notifierMessages = new List<string>();
                var modelStateMessages = new List<string>();
                notifierMessages.AddRange(_notifier.GetNotifications()?.Select(i => i.Message));
                modelStateMessages.AddRange(ModelState.Select(i => i.Value).SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return BadRequest(new ValidationProblemDetails(
                    new Dictionary<string, string[]>
                    {
                        { "NotifierMessages", notifierMessages.ToArray() },
                        { "ModelStateMessages", modelStateMessages.ToArray() },
                    }));
            }
            if(success == null)
            {
                return Ok(result);
            }
            return success(result);
        }

    }
}