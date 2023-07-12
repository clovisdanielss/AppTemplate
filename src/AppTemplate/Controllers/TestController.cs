using AppTemplate.Application.Interfaces;
using AppTemplate.Application.Models;
using AppTemplate.Models;
using AppTemplate.Shared.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
    public async Task<ActionResult> GetTokenFromFirstUser([FromServices] IGenerateJwtService service, [FromServices] IUserRepository repository)
    {
        var users = await repository.GetAll();
        var user = users.FirstOrDefault();
        return CustomResponse(await service.HandleAsync(user));
    }
    
    [HttpPost("insert-claim-to-first-user")]
    public async Task<ActionResult> InserClaimToFirstUser(CreateClaimModel model, [FromServices] IUserRepository repository, [FromServices] ICreateClaimService service)
    {
        var users = await repository.GetAll();
        var input = _mapper.Map<Claim>(model);
        input.UserId = users.FirstOrDefault()?.Id;
        await service.HandleAsync(input);
        return CustomResponse(success: (r) => Created("",r));
    }

    [Authorize]
    [HttpGet("get-token-in-authentication")]
    public async Task<ActionResult> GetTokenInAuthentication()
    {
        var test = HttpContext.GetTokenAsync("access_token");
        return CustomResponse(test);
    }

    [HttpPost("create-user")]
    public async Task<ActionResult> CreateUser(UsernameAndPassworModel model, [FromServices] ICreateUserService service)
    {
        var input = _mapper.Map<UsernameAndPassword>(model);
        await service.HandleAsync(input);
        return CustomResponse(success: (result) => Created("", result));
    }

    [HttpPost("login-user-api")]
    public async Task<ActionResult> LoginUserApi(UsernameAndPassworModel model, [FromServices] ISignInApiService service)
    {
        var input = _mapper.Map<UsernameAndPassword>(model);
        var result = await service.HandleAsync(input);
        return CustomResponse(result, success: (result) => Ok(result));
    }

    [HttpPost("login-user")]
    public async Task<ActionResult> LoginUser(UsernameAndPassworModel model, [FromServices] ISignInService service)
    {
        var input = _mapper.Map<UsernameAndPassword>(model);
        await service.HandleAsync(input);
        return CustomResponse(success: (result) => Ok(result));
    }

    [HttpGet("logout-user")]
    public async Task<ActionResult> LogoutUser()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Ok();
    }

    [HttpGet("test-cookie-auth")]
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public async Task<IActionResult> TestAuthorized()
    {
        return CustomResponse("Teste de autorização");
    }

    [HttpGet("all-users")]
    public async Task<ActionResult> GetAllUsers([FromServices] IUserRepository service)
    {
        return CustomResponse(await service.GetAll());
    }
}
