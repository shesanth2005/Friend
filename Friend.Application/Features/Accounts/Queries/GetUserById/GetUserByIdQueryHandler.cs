using AutoMapper;
using Friend.Application.Contracts.Persistence;
using Friend.Application.Contracts.Security;
using Friend.Application.Features.Accounts.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friend.Application.Features.Accounts.Queries.GetUserById
{
    public class GetUserByIdQueryHandler(IUserRepository _userRepository, IMapper _mapper, ITokenService _tokenService): IRequestHandler<GetUserByIdQuery, UserDto>
    {
        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByIdAsync(request.Id);

            if (user == null) return null!;

            var dto = _mapper.Map<UserDto>(user);
            //dto.Token = _tokenService.CreateToken(user);
            return dto;
        }
    }
}
