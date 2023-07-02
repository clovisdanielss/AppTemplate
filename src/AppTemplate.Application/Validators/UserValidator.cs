using AppTemplate.Application.Models;
using FluentValidation;

namespace AppTemplate.Application.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.UserName).Length(3, 10);
        }

    }
}
