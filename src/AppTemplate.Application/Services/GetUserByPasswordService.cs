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
    public class GetUserByPasswordService : AbstractService, IGetUserByPasswordService
    {
        private readonly IUserRepository _userRepository;
        public GetUserByPasswordService(INotifier notifier, IUserRepository userRepository) : base(notifier)
        {
            _userRepository = userRepository;
        }

        public async Task<User> HandleAsync(UsernameAndPassword input)
        {
            var user = await _userRepository.GetByUsername(input.UserName);
            if(user == null || !PasswordHasher.VerifyPassword(user.PasswordHash, input.Password))
            {
                Notify("Usuário ou senha incorretos");
                return null;
            }
            return user;
        }

    }
}
