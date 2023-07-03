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

    [HttpGet("get-token-first-user")]
    public async Task<ActionResult> GetTokenFromFirstUser([FromServices] IService<User,Jwt> service, [FromServices] IUserRepository repository)
    {
        var users = await repository.GetAll();
        var user = users.FirstOrDefault();
        return CustomResponse(await service.HandleAsync(user));
    }
    
    [HttpPost("insert-claim-to-first-user")]
    public async Task<ActionResult> InserClaimToFirstUser(CreateClaimModel model, [FromServices] IUserRepository repository, [FromServices] IProcedure<Claim> service)
    {
        var users = await repository.GetAll();
        var input = _mapper.Map<Claim>(model);
        input.UserId = users.FirstOrDefault()?.Id;
        await service.HandleAsync(input);
        return CustomResponse(success: (r) => Created("",r));
    }

    [Authorize]
    [HttpGet("auth")]
    public async Task<ActionResult> GetHelloWorldAuth()
    {
        return CustomResponse("Hello World - Authorized");
    }

    [HttpPost("create-user")]
    public async Task<ActionResult> GetCreatedUser(UsernameAndPassworModel model, [FromServices] IProcedure<UsernameAndPassword> service)
    {
        var input = _mapper.Map<UsernameAndPassword>(model);
        await service.HandleAsync(input);
        return CustomResponse(success: (result) => Created("", result));
    }
    [HttpGet("all-users")]
    public async Task<ActionResult> GetAllUsers([FromServices] IUserRepository service)
    {
        return CustomResponse(await service.GetAll());
    }
}
