using AppTemplate.Application.Interfaces;
using AppTemplate.Application.Models;
using AppTemplate.Models;
using AppTemplate.Shared.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppTemplate.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : MainController
{
    public TestController(ILogger<TestController> logger, IMapper mapper, INotifier notifier) : base(logger, mapper, notifier)
    {
    }

    [HttpGet]
    public async Task<ActionResult> GetHelloWorld(UsernameAndPassworModel model, [FromServices] IService<Jwt> service)
    {
        return CustomResponse(await service.HandleAsync());
    }
    [Authorize]
    [HttpGet("teste/auth")]
    public async Task<ActionResult> GetHelloWorldAuth()
    {
        return CustomResponse("Hello World - Authorized");
    }

    [HttpPost("CreateUser")]
    public async Task<ActionResult> GetCreatedUser(UsernameAndPassworModel model, [FromServices] IProcedure<UsernameAndPassword> service)
    {
        var input = _mapper.Map<UsernameAndPassword>(model);
        await service.HandleAsync(input);
        return CustomResponse(success: (result) => Created("", result));
    }
    [HttpGet("AllUsers")]
    public async Task<ActionResult> GetAllUsers([FromServices] IUserRepository service)
    {
        return CustomResponse(await service.GetAll());
    }
}
