using AppTemplate.Application.Interfaces;
using AppTemplate.Application.Models;
using AppTemplate.Shared.AbstractClasses;
using AppTemplate.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTemplate.Application.Services
{
    public class CreateClaimService : AbstractService, IProcedure<Claim>
    {
        private readonly IClaimRepository _claimRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        public CreateClaimService(INotifier notifier, IClaimRepository claimRepository,
            IUserRepository userRepository, IRoleRepository roleRepository) : base(notifier)
        {
            _claimRepository = claimRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task HandleAsync(Claim input)
        {
            if (input.UserId.HasValue)
            {
                var user = _userRepository.GetById(input.UserId.Value);
                if (user == null)
                {
                    Notify("Usuário inexistente");
                    return;
                }
                var userClaims = await _claimRepository.GetClaimsFromUser(input.UserId.Value);
                await UpdateClaims(input, userClaims);
                return;
            }
            else if (input.RoleId.HasValue)
            {
                var role = _roleRepository.GetById(input.RoleId.Value);
                if (role == null)
                {
                    Notify("Papel inexistente");
                    return;
                }
                var roleClaims = await _claimRepository.GetClaimsFromRole(input.RoleId.Value);
                await UpdateClaims(input, roleClaims);
                return;
            }
            Notify("Para criar uma nova claim, é preciso relacionar um usuário ou papel");
        }

        private async Task UpdateClaims(Claim input, IEnumerable<Claim> userClaims)
        {
            var existingClaim = userClaims.FirstOrDefault(x => x.ClaimName == input.ClaimName);
            if (existingClaim != null)
            {
                existingClaim.ClaimValue = input.ClaimValue;
                await _claimRepository.Update(existingClaim);
            }
            else
            {
                await _claimRepository.Add(input);
            }
        }
    }
}
