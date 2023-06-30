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
    public TestController(ILogger<TestController> logger, IMapper mapper) : base(logger, mapper)
    {
    }

    [HttpGet]
    public async Task<ActionResult> GetHelloWorld(UsernameAndPassworModel model, [FromServices] IService<Jwt> service)
    {
        return Ok(await service.HandleAsync());
    }
    [Authorize]
    [HttpGet("teste/auth")]
    public async Task<ActionResult> GetHelloWorldAuth()
    {
        return Ok("Hello World - Authorized");
    }

    [HttpPost("CreateUser")]
    public async Task<ActionResult> GetCreatedUser(UsernameAndPassworModel model, [FromServices] IService<UsernameAndPassword, User> service)
    {
        var input = _mapper.Map<UsernameAndPassword>(model);
        var result = await service.HandleAsync(input);
        return Ok(result);
    }
}
