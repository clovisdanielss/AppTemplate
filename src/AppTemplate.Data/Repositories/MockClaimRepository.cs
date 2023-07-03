using AppTemplate.Application.Interfaces;
using AppTemplate.Application.Models;
using AppTemplate.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
