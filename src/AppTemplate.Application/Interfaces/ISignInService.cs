using AppTemplate.Application.Models;
using AppTemplate.Shared.Interfaces;
using System.Security.Claims;

namespace AppTemplate.Application.Interfaces
{
    public interface ISignInService : IProcedure<UsernameAndPassword> { }
}
