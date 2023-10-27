using AppTemplate.Application.Extensions;
using AppTemplate.Data.Extensions;
using AppTemplate.Extensions;
using AppTemplate.Models;
using AppTemplate.Payment.Subscriptions.Efi.Extensions;
using AppTemplate.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
IJwtConfiguration configuration = new JwtConfiguration();
builder.Configuration.GetSection("AppSettings").Bind(configuration);
builder.Services.AddControllers();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddJwt(configuration);
builder.Services.AddMockRepository();
builder.Services.AddAuthServices(configuration);
builder.Services.AddProfiles();
builder.Services.AddEfi(new()
{
    ClientId = builder.Configuration.GetSection("EfiClientId").Value,
    ClientSecret = builder.Configuration.GetSection("EfiClientSecret").Value,
    IsSandbox = true
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
