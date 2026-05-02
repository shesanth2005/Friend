using AutoMapper;
using Friend.Application.Features.Accounts.Commands.RegisterUser;
using Friend.Application.Features.Accounts.Dtos;
using Friend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friend.Application.Mappings
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {

            // Map Command → Domain
            CreateMap<RegisterUserCommand, AppUser>();

            // Map Domain → DTO
            CreateMap<AppUser, UserDto>();

            // Map AppUser → UserRegisteredDto
            CreateMap<AppUser, UserRegisteredDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));


        }


    }
}
