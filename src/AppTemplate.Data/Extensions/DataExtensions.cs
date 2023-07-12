using AppTemplate.Application.Interfaces;
using AppTemplate.Application.Models;
using AppTemplate.Application.Services;
using AppTemplate.Data.Repositories;
using AppTemplate.Shared.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
