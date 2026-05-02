using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friend.Application.Features.Accounts.Commands.RegisterUser
{
    /// Validates the RegisterUserCommand using FluentValidation.
    /// Ensures all required fields meet business and data rules before handling registration.
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.RegisterDto.DisplayName)
                .NotEmpty().WithMessage("Display name is required.")
                .MaximumLength(50).WithMessage("Display name cannot exceed 50 characters.");

            RuleFor(x => x.RegisterDto.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email address is required.");

            RuleFor(x => x.RegisterDto.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"\d").WithMessage("Password must contain at least one number.");
        }
    }
}
