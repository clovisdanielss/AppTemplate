using AppTemplate.Application.Interfaces;
using AppTemplate.Application.Models;

namespace AppTemplate.Application.Services
{
    public class SignInApiService : ISignInApiService
    {
        private readonly IGenerateJwtService _generateJwtService;
        private readonly IGetUserByPasswordService _getUserByPasswordService;
        public SignInApiService(
            IGenerateJwtService generateJwtService,
            IGetUserByPasswordService getUserByPasswordService)
        {
            _generateJwtService = generateJwtService;
            _getUserByPasswordService=getUserByPasswordService;
        }

        public async Task<Jwt> HandleAsync(UsernameAndPassword input)
        {
            var user = await _getUserByPasswordService.HandleAsync(input);
            if (user == null)
            {
                return null;
            }
            var token = await _generateJwtService.HandleAsync(user);
            return token;
        }
    }
}
