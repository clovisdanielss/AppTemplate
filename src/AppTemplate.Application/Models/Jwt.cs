using AppTemplate.Application.Interfaces;

namespace AppTemplate.Application.Models;

public class Jwt : IJwt
{
    public string Id { get; set; }
    public string Token { get; set; }
}