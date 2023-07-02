using AppTemplate.Application.Interfaces;
using AppTemplate.Application.Models;
using AppTemplate.Application.Validators;
using AppTemplate.Shared.AbstractClasses;
using AppTemplate.Shared.Interfaces;

namespace AppTemplate.Application.Services;

public class CreateUserService : AbstractService, IProcedure<UsernameAndPassword>
{
    private readonly IUserRepository _userRepository;

    public CreateUserService(IUserRepository userRepository, INotifier notifier) : base(notifier)
    {
        _userRepository=userRepository;
    }
    public async Task HandleAsync(UsernameAndPassword input)
    {
        if (!await IsValid(new UsernameAndPasswordValidator(), input)) return;
        var hashString = PasswordHasher.GeneratePasswordHashString(input.Password);
        var user = new User
        {
            Email = input.UserName,
            UserName = input.UserName,
            PasswordHash = hashString
        };
        if (await IsValid(new UserValidator(), user))
        {
            await _userRepository.Add(user);
        }
    }

}