using AppTemplate.Application.Interfaces;
using AppTemplate.Application.Models;
using AppTemplate.Application.Services;
using AppTemplate.Shared.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTemplate.Application.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddAuthServices(this IServiceCollection service, IJwtConfiguration config = null)
        {
            service.AddHttpContextAccessor();
            if(config != null)
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
