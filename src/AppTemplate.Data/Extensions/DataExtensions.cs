using AppTemplate.Application.Interfaces;
using AppTemplate.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace AppTemplate.Data.Extensions
{
    public static class DataExtensions
    {
        public static IServiceCollection AddMockRepository(this IServiceCollection service)
        {
            service.AddScoped<IUserRepository, MockUserRepository>();
            service.AddScoped<IRoleRepository, MockRoleRepository>();
            service.AddScoped<IClaimRepository, MockClaimRepository>();

            return service;
        }
    }
}
