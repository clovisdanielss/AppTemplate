using AppTemplate.Application.Models;
using FluentValidation;

namespace AppTemplate.Application.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("O nome de usuário não pode ser vazio.")
                .EmailAddress()
                .WithMessage("O nome de usuário precisa ser um email.")
                .Length(5, 100)
                .WithMessage("O nome de usuário deve ter entre 5 e 100 caracteres");
        }

    }
}
