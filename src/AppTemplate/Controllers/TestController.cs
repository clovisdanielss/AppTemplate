using AppTemplate.Application.Models;
using AppTemplate.Shared.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppTemplate.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : MainController
{
    public TestController(ILogger<TestController> logger) : base(logger)
    {
    }

    [HttpGet]
    public async Task<ActionResult> GetHelloWorld([FromServices] IService<Jwt> service)
    {
        return Ok(service.Handle());
    }
    [Authorize]
    [HttpGet("teste/auth")]
    public async Task<ActionResult> GetHelloWorldAuth()
    {
        return Ok("Hello World - Authorized");
    }
    [HttpGet("CreateUser")]
    public async Task<ActionResult> GetCreatedUser()
    {

        return Ok();
    }
}
