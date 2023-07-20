using AppTemplate.Application.Interfaces;
using AppTemplate.Application.Models;

namespace AppTemplate.Data.Repositories
{
    public class MockClaimRepository : MockRepository<Claim>, IClaimRepository
    {
        public async Task<IEnumerable<Claim>> GetClaimsFromRole(Guid roleId)
        {
            return repository.Where(x => x.RoleId == roleId);
        }

        public async Task<IEnumerable<Claim>> GetClaimsFromUser(Guid userId)
        {
            return repository.Where(x => x.UserId == userId);
        }
    }
}
