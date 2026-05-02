using AutoMapper;
using Friend.Application.Contracts.Persistence;
using Friend.Application.Contracts.Security;
using Friend.Application.Features.Accounts.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Friend.Application.Features.Accounts.Commands.LoginUser
{
    public class LoginUserCommandHandler(IUserRepository _userRepository, ITokenService _tokenService, IMapper _mapper) : IRequestHandler<LoginUserCommand, UserDto>
    {
      
        public async Task<UserDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.LoginDto.Email);
            if (user == null)
                throw new Exception("Invalid email");

            using var hmac = new HMACSHA256(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.LoginDto.Password));

            if (!computedHash.SequenceEqual(user.PasswordHash))
                throw new Exception("Invalid password");

            var dto = _mapper.Map<UserDto>(user);
            //dto.Token = _tokenService.CreateToken(user);
            return dto;
        }
    }
}
