using Friend.Application.Features.Accounts.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friend.Application.Features.Accounts.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<UserDto>
    {
        public RegisterDto RegisterDto { get; set; }
    }
}
