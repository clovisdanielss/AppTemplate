using AppTemplate.Application.Models;
using AppTemplate.Shared.Interfaces;

namespace AppTemplate.Application.Interfaces;

public interface ICreateUserService : IProcedure<UsernameAndPassword> { }
