using AppTemplate.Shared.Interfaces;

namespace AppTemplate.Application.Interfaces;
public interface IJwt : IEntity
{
    string Token { get; set; }
}