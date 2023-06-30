namespace AppTemplate.Shared.Interfaces;

public interface IJwtConfiguration
{
    string Secret { get; set; }
    int ExpirationTime { get; set; }
    string Issuer { get; set; }
    string ValidAudience { get; set; }
}