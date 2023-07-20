using AppTemplate.Application.Models;
using AppTemplate.Shared.Interfaces;

namespace AppTemplate.Application.Interfaces
{
    public interface IClaimRepository : IRepository<Claim>
    {
        Task<IEnumerable<Claim>> GetClaimsFromUser(Guid userId);
        Task<IEnumerable<Claim>> GetClaimsFromRole(Guid roleId);
    }
}
