using AppTemplate.Extensions;
using AppTemplate.Models;
using AppTemplate.Shared.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
IJwtConfiguration configuration = new JwtConfiguration();
builder.Configuration.GetSection("AppSettings").Bind(configuration);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddSingleton(configuration);
builder.Services.AddJwt(configuration);
builder.Services.AddServices();
builder.Services.AddProfiles();

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
