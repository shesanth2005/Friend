using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friend.Application.Features.Accounts.Commands.LoginUser
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(x => x.LoginDto.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.LoginDto.Password).NotEmpty();
        }
    }
}
