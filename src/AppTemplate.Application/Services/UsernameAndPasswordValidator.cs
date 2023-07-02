using AppTemplate.Application.Models;
using FluentValidation;

namespace AppTemplate.Application.Services
{
    public class UsernameAndPasswordValidator : AbstractValidator<UsernameAndPassword>
    {
        public UsernameAndPasswordValidator()
        {
            RuleFor(x => x.Password)
                    .NotEmpty()
                    .WithMessage("Senha não pode ser vazia")
                    .MinimumLength(8).WithMessage("A senha deve ter ao menos 8 caracteres")
                    .MaximumLength(16).WithMessage("A senha deve ter no máximo 16 caracteres")
                    .Matches(@"[A-Z]+").WithMessage("A senha deve conter letras maiúsculas")
                    .Matches(@"[a-z]+").WithMessage("A senha deve conter letras minúsculas")
                    .Matches(@"[0-9]+").WithMessage("A senha deve conter números")
                    .Matches(@"[\!\?\*\.]+").WithMessage("Sua senha precisa conter pelo menos um (!? *.)");
        }
    }
}
