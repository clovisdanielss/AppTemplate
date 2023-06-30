using AppTemplate.Application.Interfaces;
using AppTemplate.Application.Models;
using AppTemplate.Application.Services;
using AppTemplate.Data.Repositories;
using AppTemplate.Extensions;
using AppTemplate.Models;
using AppTemplate.Shared.Interfaces;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
IJwtConfiguration configuration = new JwtConfiguration();
builder.Configuration.GetSection("AppSettings").Bind(configuration);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddScoped<IUserRepository, MockUserRepository>();
builder.Services.AddScoped<IRoleRepository, MockRoleRepository>();
builder.Services.AddSingleton<IJwtConfiguration>(configuration);
//builder.Services.AddTransient<IUserStore<User>, UserStoreService>();
//builder.Services.AddTransient<IRoleStore<Role>, RoleStoreService>();
//builder.Services.AddIdentity<User, Role>().AddDefaultTokenProviders();
builder.Services.AddScoped<IService<Jwt>, GenerateJwtService>();
builder.Services.AddJwt(configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
