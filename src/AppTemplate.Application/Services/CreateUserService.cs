using AppTemplate.Application.Interfaces;
using AppTemplate.Application.Models;
using AppTemplate.Application.Validators;
using AppTemplate.Shared.AbstractClasses;
using AppTemplate.Shared.Interfaces;

namespace AppTemplate.Application.Services;

public class CreateUserService : AbstractService, IProcedure<UsernameAndPassword>
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;

    public CreateUserService(IUserRepository userRepository, INotifier notifier,
        IRoleRepository roleRepository) : base(notifier)
    {
        _userRepository=userRepository;
        _roleRepository=roleRepository;
    }
    public async Task HandleAsync(UsernameAndPassword input)
    {
        if (!await IsValid(new UsernameAndPasswordValidator(), input)) return;
        var hashString = PasswordHasher.GeneratePasswordHashString(input.Password);
        var user = new User
        {
            Email = input.UserName,
            UserName = input.UserName,
            PasswordHash = hashString,
        };
        await AddUserRole(user);
        if (await IsValid(new UserValidator(), user))
        {
            await _userRepository.Add(user);
        }
    }

    private async Task AddUserRole(User user)
    {
        var role = await _roleRepository.GetRoleByName("User");
        user.RoleId = role.Id;
    }

}