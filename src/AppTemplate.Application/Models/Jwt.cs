using AppTemplate.Shared.Interfaces;

namespace AppTemplate.Application.Models;

public class Jwt : IEntity
{
    public Guid Id { get; set; }
    public string Token { get; set; }
}