using System.IdentityModel.Tokens.Jwt;
using SecurityClaim = System.Security.Claims.Claim;
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
        result.Add(new SecurityClaim(JwtRegisteredClaimNames.Sid, sessionGuid.ToString()));
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
        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_jwtConfig.Secret));
        SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);
        JwtSecurityToken token = new(
                      _jwtConfig.Issuer,
                      _jwtConfig.ValidAudience,
                      claims,
                      expires: DateTime.UtcNow.AddMinutes(_jwtConfig.ExpirationTime),
                      signingCredentials: credentials);
        string tokenWrited = new JwtSecurityTokenHandler().WriteToken(token);
        return new Jwt
        {
            Id = sessionGuid,
            Token = tokenWrited
        };
    }
}