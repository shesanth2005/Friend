using Friend.Application.Features.Accounts.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friend.Application.Features.Accounts.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<UserDto>
    {
        public LoginDto LoginDto { get; set; }
    }
}
