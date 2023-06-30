using Microsoft.OpenApi.Models;

namespace AppTemplate.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwagger(this IServiceCollection service)
    {

        service.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new() { Title = "MyApiAppTemplate", Version = "1" });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Insira o Bearer Token",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });
            var security = new OpenApiSecurityRequirement();
            security.Add(new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            }, new string[] { });
            options.AddSecurityRequirement(security);
        });
        return service;
    }
}