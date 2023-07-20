using System.IdentityModel.Tokens.Jwt;
using SecurityClaim = System.Security.Claims.Claim;
using System.Text;
using AppTemplate.Application.Models;
using AppTemplate.Shared.Interfaces;
using Microsoft.IdentityModel.Tokens;
using AppTemplate.Application.Interfaces;
using AppTemplate.Shared.AbstractClasses;
using AppTemplate.Application.Factory;

namespace AppTemplate.Application.Services;
public class GenerateJwtService : AbstractService, IGenerateJwtService
{
    private readonly IJwtConfiguration _jwtConfig;
    private readonly IClaimRepository _claimRepository;
    public GenerateJwtService(IJwtConfiguration jwtConfig, IClaimRepository claimRepository, INotifier notifier) : base(notifier)
    {
        _jwtConfig = jwtConfig ?? throw new ArgumentNullException(nameof(jwtConfig));
        _claimRepository = claimRepository;
    }

    public async Task<Jwt> HandleAsync(User user)
    {
        try
        {
            var sessionGuid = Guid.NewGuid();
            var factory = new ClaimFactory(_claimRepository);
            var claims = await factory.CreateClaims(user, sessionGuid);
            return Generate(claims.AsEnumerable(), sessionGuid);
        }
        catch
        {
            Notify("Erro interno ao criar token");
            return null;
        }
    }
    private Jwt Generate(IEnumerable<SecurityClaim> claims, Guid sessionGuid)
    {
        var handler = new JwtSecurityTokenHandler();
        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_jwtConfig.Secret));
        SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);
        var token = handler.CreateToken(new()
        {
            Issuer = _jwtConfig.Issuer,
            Audience = _jwtConfig.ValidAudience,
            Subject = new(claims),
            Expires = DateTime.UtcNow.AddMinutes(_jwtConfig.ExpirationTime),
            SigningCredentials = credentials
        });

        string tokenWrited = handler.WriteToken(token);

        return new Jwt
        {
            Id = sessionGuid,
            Token = tokenWrited
        };
    }
}