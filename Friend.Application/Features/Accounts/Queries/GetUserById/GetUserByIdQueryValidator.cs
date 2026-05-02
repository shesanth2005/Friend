using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friend.Application.Features.Accounts.Queries.GetUserById
{
    public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("User ID is required");
        }
    }
}
