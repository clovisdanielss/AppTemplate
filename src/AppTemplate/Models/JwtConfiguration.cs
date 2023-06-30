using AppTemplate.Shared.Interfaces;

namespace AppTemplate.Models;

public class JwtConfiguration : IJwtConfiguration
{
    public string Secret { get; set; }
    public int ExpirationTime { get; set; }
    public string Issuer { get; set; }
    public string ValidAudience { get; set; }
}