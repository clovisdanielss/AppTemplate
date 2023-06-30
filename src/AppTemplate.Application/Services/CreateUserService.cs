using AppTemplate.Application.Interfaces;
using AppTemplate.Application.Models;
using AppTemplate.Shared.Interfaces;

namespace AppTemplate.Application.Services;

public class CreateUserService : IProcedure<UsernameAndPassword>
{
    private readonly IUserRepository _userRepository;

    public CreateUserService(IUserRepository userRepository)
    {
        _userRepository=userRepository;
    }
    public async Task HandleAsync(UsernameAndPassword input)
    {
        var hashString = PasswordHasher.GeneratePasswordHashString(input.Password);
        var user = new User
        {
            Email = input.UserName,
            UserName = input.UserName,
            PasswordHash = hashString
        };
        await _userRepository.Add(user);
    }


}