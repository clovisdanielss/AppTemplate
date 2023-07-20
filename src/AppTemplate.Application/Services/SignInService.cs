using AppTemplate.Application.Factory;
using AppTemplate.Application.Interfaces;
using AppTemplate.Application.Models;
using AppTemplate.Shared.AbstractClasses;
using AppTemplate.Shared.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace AppTemplate.Application.Services
{
    public class SignInService : AbstractService, ISignInService
    {
        private readonly IClaimRepository _claimRepository;
        private readonly IGetUserByPasswordService _getUserByPasswordService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const int EXPIRATION_TIME = 2;
        public SignInService(INotifier notifier, IClaimRepository claimRepository,
            IGetUserByPasswordService getUserByPasswordService, IHttpContextAccessor httpContextAccessor) : base(notifier)
        {
            _claimRepository = claimRepository;
            _getUserByPasswordService = getUserByPasswordService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task HandleAsync(UsernameAndPassword input)
        {
            var factory = new ClaimFactory(_claimRepository);
            var user = await _getUserByPasswordService.HandleAsync(input);
            if (user == null)
            {
                return;
            }
            var claim = await factory.CreateClaimsPrincipal(user, Guid.NewGuid());
            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                claim,
                new()
                {
                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(EXPIRATION_TIME),
                    IsPersistent = true
                });
        }
    }
}
