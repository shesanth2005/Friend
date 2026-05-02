using AutoMapper;
using Friend.Application.Contracts.Persistence;
using Friend.Application.Features.Accounts.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friend.Application.Features.Accounts.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler(IUserRepository _userRepository, IMapper _mapper): IRequestHandler<GetAllUsersQuery, List<UserDto>>
    {
        public async Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllUsersAsync();
            return _mapper.Map<List<UserDto>>(users);
        }
    }
}
