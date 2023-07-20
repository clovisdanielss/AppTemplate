using AppTemplate.Application.Interfaces;
using AppTemplate.Application.Services;
using AppTemplate.Shared.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AppTemplate.Application.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddAuthServices(this IServiceCollection service, IJwtConfiguration config = null)
        {
            service.AddHttpContextAccessor();
            if (config != null)
            {
                service.AddSingleton(config);
                service.AddScoped<IGenerateJwtService, GenerateJwtService>();
                service.AddScoped<ISignInApiService, SignInApiService>();
            }
            service.AddScoped<ICreateUserService, CreateUserService>();
            service.AddScoped<IGetUserByPasswordService, GetUserByPasswordService>();
            service.AddScoped<ISignInService, SignInService>();
            service.AddScoped<INotifier, NotifierService>();
            service.AddScoped<ICreateClaimService, CreateClaimService>();

            return service;
        }
    }
}
