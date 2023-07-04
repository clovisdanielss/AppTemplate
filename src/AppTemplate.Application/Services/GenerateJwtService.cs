using System.IdentityModel.Tokens.Jwt;
using SecurityClaim = System.Security.Claims.Claim;
using ClaimValueTypes = System.Security.Claims.ClaimValueTypes;
using System.Text;
using AppTemplate.Application.Models;
using AppTemplate.Shared.Interfaces;
using Microsoft.IdentityModel.Tokens;
using AppTemplate.Application.Interfaces;

namespace AppTemplate.Application.Services;


public class GenerateJwtService : IService<User,Jwt>
{
    private readonly IJwtConfiguration _jwtConfig;
    private readonly IClaimRepository _claimRepository;
    public GenerateJwtService(IJwtConfiguration jwtConfig, IClaimRepository claimRepository)
    {
        _jwtConfig = jwtConfig ?? throw new ArgumentNullException(nameof(jwtConfig));
        _claimRepository = claimRepository;
    }

    public async Task<Jwt> HandleAsync(User user)
    {
        var sessionGuid = Guid.NewGuid();
        var claims = await GetClaimsAsync(user, sessionGuid);
        return Generate(claims.AsEnumerable(), sessionGuid);
    }

    private async Task<IEnumerable<SecurityClaim>> GetClaimsAsync(User user, Guid sessionGuid)
    {
        List<SecurityClaim> result = new List<SecurityClaim>();
        var userClaims = await _claimRepository.GetClaimsFromUser(user.Id);
        if (userClaims != null)
        {
            CopyClaims(result, userClaims);
        }
        var roleClaims = await _claimRepository.GetClaimsFromRole(user.RoleId);
        if(roleClaims != null)
        {
            CopyClaims(result, roleClaims);
        }
        result.Add(new SecurityClaim(JwtRegisteredClaimNames.Email, user.Email));
        result.Add(new SecurityClaim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
        result.Add(new SecurityClaim(JwtRegisteredClaimNames.Jti, sessionGuid.ToString()));
        result.Add(new SecurityClaim(JwtRegisteredClaimNames.Nbf, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64));
        result.Add(new SecurityClaim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64));
        return result;
    }

    private static void CopyClaims(List<SecurityClaim> claimsDest, IEnumerable<Claim> claimsSrc)
    {
        foreach (var claim in claimsSrc)
        {
            claimsDest.Add(new SecurityClaim(claim.ClaimName, claim.ClaimValue));
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