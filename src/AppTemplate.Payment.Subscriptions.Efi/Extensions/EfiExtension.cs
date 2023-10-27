using AppTemplate.Payment.Subscriptions.Efi.Configurations;
using AppTemplate.Payment.Subscriptions.Efi.Services;
using AppTemplate.Payment.Subscriptions.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTemplate.Payment.Subscriptions.Efi.Extensions
{
    public static class EfiExtension
    {
        public static IServiceCollection AddEfi(this IServiceCollection services, EfiConfiguration configuration)
        {
            services.AddSingleton(configuration);
            services.AddScoped<IPlanClientService, EfiPlanClientService>();

            return services;
        }
    }
}
