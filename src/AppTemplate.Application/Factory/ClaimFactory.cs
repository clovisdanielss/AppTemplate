using System.IdentityModel.Tokens.Jwt;
using ClaimSecurity = System.Security.Claims.Claim;
using ClaimsPrincipal = System.Security.Claims.ClaimsPrincipal;
using ClaimsIdentity = System.Security.Claims.ClaimsIdentity;
using ClaimValueTypes = System.Security.Claims.ClaimValueTypes;
using AppTemplate.Application.Models;
using AppTemplate.Application.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace AppTemplate.Application.Factory;

internal class ClaimFactory
{
    private readonly IClaimRepository _claimRepository;
    public ClaimFactory(IClaimRepository claimRepository)
    {
        _claimRepository = claimRepository;
    }
    public async Task<ClaimsPrincipal> CreateClaimsPrincipal(User user, Guid sessionGuid, bool useCookie = true)
    {
        return new(await CreateClaimsIdentity(user, sessionGuid, useCookie));
    }
    public async Task<ClaimsIdentity> CreateClaimsIdentity(User user, Guid sessionGuid, bool useCookie)
    {
        if (!useCookie)
            return new(await CreateClaims(user, sessionGuid));
        else
            return new(await CreateClaims(user, sessionGuid), CookieAuthenticationDefaults.AuthenticationScheme);
    }
    public async Task<List<ClaimSecurity>> CreateClaims(User user, Guid sessionGuid)
    {
        List<ClaimSecurity> result = new List<ClaimSecurity>();
        var userClaims = await _claimRepository.GetClaimsFromUser(user.Id);
        if (userClaims != null)
        {
            CopyClaims(result, userClaims);
        }
        var roleClaims = await _claimRepository.GetClaimsFromRole(user.RoleId);
        if (roleClaims != null)
        {
            CopyClaims(result, roleClaims);
        }
        result.Add(new ClaimSecurity(JwtRegisteredClaimNames.Email, user.Email));
        result.Add(new ClaimSecurity(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
        result.Add(new ClaimSecurity(JwtRegisteredClaimNames.Jti, sessionGuid.ToString()));
        result.Add(new ClaimSecurity(JwtRegisteredClaimNames.Nbf, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64));
        result.Add(new ClaimSecurity(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64));
        return result;
    }
    private static void CopyClaims(List<ClaimSecurity> claimsDest, IEnumerable<Claim> claimsSrc)
    {
        foreach (var claim in claimsSrc)
        {
            claimsDest.Add(new ClaimSecurity(claim.ClaimName, claim.ClaimValue));
        }
    }
}
