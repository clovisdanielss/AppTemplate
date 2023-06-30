using System.IdentityModel.Tokens.Jwt;
using System.Text;
using AppTemplate.Application.Interfaces;
using AppTemplate.Application.Models;
using AppTemplate.Shared.Interfaces;
using AppTemplate.Shared.Models;
using Microsoft.IdentityModel.Tokens;

namespace AppTemplate.Application.Services;


public class GenerateJwtService : IService<Jwt>
{
    private readonly IJwtConfiguration appSettings;
    public GenerateJwtService(IJwtConfiguration appSettings)
    {
        this.appSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));
    }

    public IServiceOutput<Jwt> Handle()
    {
        return new ServiceOutput<Jwt>(Generate());
    }

    private Jwt Generate()
    {
        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(appSettings.Secret));
        SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);
        JwtSecurityToken token = new(
                      appSettings.Issuer,
                      appSettings.ValidAudience,
                      expires: DateTime.UtcNow.AddMinutes(appSettings.ExpirationTime),
                      signingCredentials: credentials);
        string tokenWrited = new JwtSecurityTokenHandler().WriteToken(token);
        return new Jwt{
            Id = Guid.NewGuid(),
            Token = tokenWrited
        };
    }
}