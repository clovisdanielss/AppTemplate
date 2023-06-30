using AppTemplate.Application.Interfaces;
using AppTemplate.Application.Models;
using AppTemplate.Application.Services;
using AppTemplate.Data.Repositories;
using AppTemplate.Shared.Interfaces;

namespace AppTemplate.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection service)
        {
            service.AddScoped<IUserRepository, MockUserRepository>();
            service.AddScoped<IRoleRepository, MockRoleRepository>();
            service.AddScoped<IProcedure<UsernameAndPassword>, CreateUserService>();
            service.AddScoped<IService<Jwt>, GenerateJwtService>();

            return service;
        }
    }
}
